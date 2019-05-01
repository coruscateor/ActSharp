using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Concurrent;
using ActSharp.Executors;

namespace ActSharp
{

    /// <summary>
    /// The base class for Actor objects.
    /// 
    /// The idea behind actors in this framework is that all methods that would otherwise be public in decendant actor types are hidden and exposed though proxy methods which basically execute the hidden methods on the thread pool in serial as called on individual actor instances. 
    /// </summary>
    public abstract class Actor : IDisposable
    {

        ConcurrentQueue<Task> myExQueue = new ConcurrentQueue<Task>();

        SpinLock myCTLock;

        Task myCurrentTask;

        RetainedTaskList myRetainedTaskList;

        ManualResetEventSlim myOnIdleEvent = new ManualResetEventSlim();

        int myManagedThreadId = -1;

        SpinLock myManagedThreadIdLock;

        public Actor()
        {

            myRetainedTaskList = new RetainedTaskList(myExQueue);

        }

        public bool ActorIsExecuting
        {

            get
            {

                //spinlock

                bool taken = false;

                myCTLock.Enter(ref taken);

                try
                {

                    return myCurrentTask != null;

                }
                finally
                {

                    if (taken)
                        myCTLock.Exit();

                }

            }

        }

        public int ActorExecuteQueueCount
        {

            get
            {

                return myExQueue.Count;

            }

        }

        public bool ActorExecuteQueueIsEmpty
        {

            get
            {

                return myExQueue.IsEmpty;

            }

        }

        public bool ActorIsIdle
        {

            get
            {

                return ActorExecuteQueueIsEmpty && !ActorIsExecuting;

            }

        }

        public void ActorWaitForIdle()
        {

            ThrowIfExecutingAndCurrentThreadsAreTheSame();

            myOnIdleEvent.Wait();

        }

        public bool ActorWaitForIdle(int millisecondsTimeout)
        {

            ThrowIfExecutingAndCurrentThreadsAreTheSame();

            return myOnIdleEvent.Wait(millisecondsTimeout);

        }

        public bool ActorWaitForIdle(int millisecondsTimeout, CancellationToken cancellationToken)
        {

            ThrowIfExecutingAndCurrentThreadsAreTheSame();

            return myOnIdleEvent.Wait(millisecondsTimeout, cancellationToken);

        }

        public bool ActorWaitForIdle(TimeSpan timeout)
        {

            ThrowIfExecutingAndCurrentThreadsAreTheSame();

            return myOnIdleEvent.Wait(timeout);

        }

        public bool ActorWaitForIdle(TimeSpan timeout, CancellationToken cancellationToken)
        {

            ThrowIfExecutingAndCurrentThreadsAreTheSame();

            return myOnIdleEvent.Wait(timeout, cancellationToken);

        }

        static void WaitAllImpl(IEnumerable<Actor> actors)
        {

            foreach (var actor in actors)
                actor.ActorWaitForIdle();

        }

        public static void ActorWaitAll(IEnumerable<Actor> actors)
        {

            WaitAllImpl(actors);

            //foreach (var actor in actors)
            //    actor.ActorWaitForIdle();

        }

        public static void ActorWaitAll(params Actor[] actors)
        {

            WaitAllImpl(actors);

            //foreach (var actor in actors)
            //    actor.ActorWaitForIdle();

            //WaitAll(actors);

        }

        static int WaitAnyImpl(IEnumerable<Actor> actors)
        {

            int idleActorIndex = -1;

            SpinWait sw = new SpinWait();

            do
            {

                int i = -1;

                foreach (var actor in actors)
                {

                    if(actor.ActorIsIdle)
                    {

                        idleActorIndex = i;

                        break;

                    }

                    i++;

                    sw.SpinOnce();

                }

            } while (idleActorIndex < 0);

            return idleActorIndex;

        }

        public static int ActorWaitAny(IEnumerable<Actor> actors)
        {

            return WaitAnyImpl(actors);

        }

        public static int ActorWaitAny(params Actor[] actors)
        {

            return WaitAnyImpl(actors);

        }

        //public bool ActorWaitForIdleYeildOnce()
        //{

        //    SpinWait sw = new SpinWait();

        //    bool returnNext = false;

        //    while (!returnNext)
        //    {

        //        if (ActorIsIdle())
        //            return true;

        //        returnNext = sw.NextSpinWillYield;

        //        sw.SpinOnce();

        //    }

        //    return false;

        //}

        //public bool ActorIsActive
        //{

        //    get
        //    {

        //        return !ActorQueueIsEmpty && ActorIsExecuting;

        //    }

        //}

        //public bool ActorIsDone
        //{

        //    get
        //    {

        //        return myIsDone;

        //    }

        //}

        //private void CheckIsDone()
        //{

        //    if (myIsDone)
        //        throw new ActorDoneException();

        //}

        public int ActorManagedThreadId
        {

            get
            {

                bool taken = false;

                myManagedThreadIdLock.Enter(ref taken);

                try
                {

                    return myManagedThreadId;

                }
                finally
                {

                    if (taken)
                        myManagedThreadIdLock.Exit();

                }

            }

        }

        public bool ActorIsCurrentThread
        {

            get
            {

                return ActorManagedThreadId == Thread.CurrentThread.ManagedThreadId;

            }

        }

        private void SetManagedThreadId()
        {

            int threadId = Thread.CurrentThread.ManagedThreadId;

            bool taken = false;

            myManagedThreadIdLock.Enter(ref taken);

            try
            {

                myManagedThreadId = threadId;

            }
            finally
            {

                if (taken)
                    myManagedThreadIdLock.Exit();

            }

        }

        private void InvaldateManagedThreadId()
        {

            bool taken = false;

            myManagedThreadIdLock.Enter(ref taken);

            try
            {

                myManagedThreadId = -1;

            }
            finally
            {

                if (taken)
                    myManagedThreadIdLock.Exit();

            }

        }

        private void ThrowIfExecutingAndCurrentThreadsAreTheSame()
        {

            if (ActorIsCurrentThread)
                throw new ActorSelfWaitException();

        }

        private Task CurrentTask
        {

            get
            {

                bool taken = false;

                myCTLock.Enter(ref taken);

                try
                {

                    return myCurrentTask;

                }
                finally
                {

                    if (taken)
                        myCTLock.Exit();

                }

            }
            set
            {

                bool taken = false;

                myCTLock.Enter(ref taken);

                try
                {

                    myCurrentTask = value;

                }
                finally
                {

                    if (taken)
                        myCTLock.Exit();

                }

            }

        }

        private bool HasCurrentTask
        {

            get
            {

                bool taken = false;

                myCTLock.Enter(ref taken);

                try
                {

                    return myCurrentTask == null;

                }
                finally
                {

                    if (taken)
                        myCTLock.Exit();

                }

            }

        }

        private void RemoveCurrentTask()
        {

            CurrentTask = null;

        }

        private bool TrySetNextTask(Task task)
        {

            bool taken = false;

            myCTLock.Enter(ref taken);

            try
            {

                if (myCurrentTask == null)
                {

                    myCurrentTask = task;

                    return true;

                }

            }
            finally
            {

                if (taken)
                    myCTLock.Exit();

            }

            return false;

        }

        private void NextTask()
        {

            if (!myOnIdleEvent.IsSet)
                myOnIdleEvent.Reset();

            //myCurrentTask must be null at this point

            myRetainedTaskList.Check();

            Task nextTask;

            if (myExQueue.TryDequeue(out nextTask))
            {

                if (TrySetNextTask(nextTask))
                {

                    nextTask.Start();

                }
                else
                {

                    //If setting the next task fails put it back on the queue

                    myExQueue.Enqueue(nextTask);

                }

            }

            //If the retained task list has items and no tasks are queued, start a retained task list check task

            //the Actor is never idle if there are retained tasks still to check for completion 

            //bool clHasTasks = myRetainedTaskList.HasTasks;

            if (nextTask == null && myRetainedTaskList.HasTasks && myExQueue.IsEmpty)
            {

                nextTask = new Task(CheckRetainedTaskListOnly);

                if(TrySetNextTask(nextTask))
                {

                    nextTask.Start();

                }

            }

            if (!ActorIsExecuting && !myRetainedTaskList.HasTasks)
            {

                //Actor is now idle

                myOnIdleEvent.Set();

                //Ensure thread id is invalidated if there are no further tasks 

                //InvaldateManagedThreadId();

            }

        }

        void CheckRetainedTaskListOnly()
        {

            myRetainedTaskList.Check();

            RemoveCurrentTask();

            if (myRetainedTaskList.HasTasks && myExQueue.IsEmpty)
            {

                Task nextCheck = new Task(CheckRetainedTaskListOnly);

                if (TrySetNextTask(nextCheck))
                {

                    nextCheck.Start();

                }

            }

        }

        //Continuations

        protected void ActorContinueWith(Action action)
        {

            Task task = new Task(action);

            //ContinueWith(task);

            myExQueue.Enqueue(task);

        }

        //Retained Tasks

        protected void ActorRetain(Task task, Action<Task> action = null, ContinuationContext continuationContext = ContinuationContext.Actor, IEnumerable<IAsyncResult> prerequisites = null)
        {

            myRetainedTaskList.Add(task, action, continuationContext, prerequisites);

        }

        protected void ActorRetain<T>(Task<T> task, Action<Task> action = null, ContinuationContext continuationContext = ContinuationContext.Actor, IEnumerable<IAsyncResult> prerequisites = null)
        {

            myRetainedTaskList.Add(task, action, continuationContext, prerequisites);

        }

        //protected void ActorRetain(Task task, Action<Task> action = null, ContinuationContext continuationContext = ContinuationContext.Actor, IEnumerable<IAsyncResult> prerequisites = null)
        //{

        //    myRetainedTaskList.Add(task, action, continuationContext, prerequisites);

        //}

        //protected void ActorRetain<T>(Task<T> task, Action<Task<T>> action = null, ContinuationContext continuationContext = ContinuationContext.Actor, IEnumerable<IAsyncResult> prerequisites = null)
        //{

        //    myRetainedTaskList.Add(task, action, continuationContext, prerequisites);

        //}

        //protected void ActorRetain(Task task, Action<Task> onCompleted = null, Action<Task> onError = null, bool continueOnCurrentThread = false, IEnumerable<IAsyncResult> prerequisites = null)
        //{

        //    myRetainedTaskList.Add(task, onCompleted, onError, continueOnCurrentThread, prerequisites);

        //}

        //protected void ActorRetain<T>(Task<T> task, Action<Task<T>> onCompleted = null, Action<Task<T>> onError = null, bool continueOnCurrentThread = false, IEnumerable<IAsyncResult> prerequisites = null)
        //{

        //    myRetainedTaskList.Add(task, onCompleted, onError, continueOnCurrentThread, prerequisites);

        //}

        //protected void ActorRetain(Task actorTask, Action<Task> onCompleted = null, Action<Task> onError = null, bool continueOnCurrentThread = false, IEnumerable<IAsyncResult> prerequisites = null)
        //{

        //    myRetainedTaskList.Add(actorTask, onCompleted, onError, continueOnCurrentThread, prerequisites);

        //}

        //protected void ActorRetain<T>(Task<T> actorTask, Action<Task<T>> onCompleted = null, Action<Task<T>> onError = null, bool continueOnCurrentThread = false, IEnumerable<IAsyncResult> prerequisites = null)
        //{

        //    myRetainedTaskList.Add(actorTask, onCompleted, onError, continueOnCurrentThread, prerequisites);

        //}

        //private void SetupTask<TTask>(Task task)
        //    where TTask : class, new(Task, IActor)
        //{

        //    TTask at = new TTask(task, this);

        //    myExQueue.Enqueue(task);

        //    if (!ActorIsExecuting)
        //        NextTask();

        //}

        private void SetupTask(Task task)
        {

            myExQueue.Enqueue(task);

            if (!ActorIsExecuting)
            {

                NextTask();

            }

        }

        //private TTask SetupTask<TTask>(Task task)
        //{

        //    TTask at = new TTask(t, this);

        //    SetupTask(task);

        //}

        private void ResetOnIdleEvent()
        {

            if (!myOnIdleEvent.IsSet)
                myOnIdleEvent.Reset();

        }

        //Method Enqueueing

        //Actions

        protected Task ActorEnqueue(Action action)
        {

            Task t = new Task(() => {

                ResetOnIdleEvent();

                SetManagedThreadId();

                //CheckIsDone();

                myRetainedTaskList.Check();

                try
                {

                    action();

                }
                finally
                {

                    RemoveCurrentTask();

                    //Now seen as not executing

                    //CheckIsDone();

                    NextTask();

                }

            });

            SetupTask(t);

            return t;

        }

        protected Task ActorEnqueue<T>(Action<T> action, T p)
        {

            return ActorEnqueue(() => { action(p); });

        }

        protected Task ActorEnqueue<T1, T2>(Action<T1, T2> action, T1 p1, T2 p2)
        {

            return ActorEnqueue(() => { action(p1, p2); });

        }

        protected Task ActorEnqueue<T1, T2, T3>(Action<T1, T2, T3> action, T1 p1, T2 p2, T3 p3)
        {

            return ActorEnqueue(() => { action(p1, p2, p3); });

        }

        protected Task ActorEnqueue<T1, T2, T3, T4>(Action<T1, T2, T3, T4> action, T1 p1, T2 p2, T3 p3, T4 p4)
        {

            return ActorEnqueue(() => { action(p1, p2, p3, p4); });

        }

        protected Task ActorEnqueue<T1, T2, T3, T4, T5>(Action<T1, T2, T3, T4, T5> action, T1 p1, T2 p2, T3 p3, T4 p4, T5 p5)
        {

            return ActorEnqueue(() => { action(p1, p2, p3, p4, p5); });

        }

        protected Task ActorEnqueue<T1, T2, T3, T4, T5, T6>(Action<T1, T2, T3, T4, T5, T6> action, T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6)
        {

            return ActorEnqueue(() => { action(p1, p2, p3, p4, p5, p6); });

        }

        protected Task ActorEnqueue<T1, T2, T3, T4, T5, T6, T7>(Action<T1, T2, T3, T4, T5, T6, T7> action, T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7)
        {

            return ActorEnqueue(() => { action(p1, p2, p3, p4, p5, p6, p7); });

        }

        protected Task ActorEnqueue<T1, T2, T3, T4, T5, T6, T7, T8>(Action<T1, T2, T3, T4, T5, T6, T7, T8> action, T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8)
        {

            return ActorEnqueue(() => { action(p1, p2, p3, p4, p5, p6, p7, p8); });

        }

        //Returning void

        //Ignoring the Exception

        protected void ActorEnqueueIgnore(Action action)
        {

            Task t = new Task(() => {

                ResetOnIdleEvent();

                SetManagedThreadId();

                //CheckIsDone();

                myRetainedTaskList.Check();

                try
                {

                    action();

                }
                finally
                {

                    RemoveCurrentTask();

                    NextTask();

                }

            });

            SetupTask(t);

            myRetainedTaskList.Add(t);

        }

        //FailFast if Exception is thrown

        protected void ActorEnqueueFailFast(Action action)
        {

            Task t = new Task(() => {

                ResetOnIdleEvent();

                SetManagedThreadId();

                //CheckIsDone();

                myRetainedTaskList.Check();

                try
                {

                    action();

                }
                finally
                {

                    RemoveCurrentTask();

                    NextTask();

                }

            });

            SetupTask(t);

            myRetainedTaskList.Add(t, (task) => {

                if(task.Exception != null)
                {

                    Environment.FailFast("Un-caught ActSharp.Actor Exception", task.Exception);

                }

            }, ContinuationContext.Immediate);

        }

        //Call continuation action if Exception is thrown

        protected void ActorEnqueue(Action action, Action<Task> continuationAction, ContinuationContext continuationContext = ContinuationContext.Immediate)
        {

            Task t = new Task(() => {

                ResetOnIdleEvent();

                SetManagedThreadId();

                //CheckIsDone();

                myRetainedTaskList.Check();

                try
                {

                    action();

                }
                finally
                {

                    RemoveCurrentTask();

                    NextTask();

                }

            });

            SetupTask(t);

            myRetainedTaskList.Add(t, continuationAction, continuationContext);

        }

        //Funcs

        protected Task<TResult> ActorEnqueue<TResult>(Func<TResult> func)
        {

            Task<TResult> t = new Task<TResult>(() => {

                ResetOnIdleEvent();

                SetManagedThreadId();

                myRetainedTaskList.Check();

                try
                {

                    return func();

                }
                finally
                {

                    RemoveCurrentTask();

                    //Now seen as not executing

                    //CheckIsDone();

                    NextTask();

                    //Now possibly seen as executing again

                    //InvaldateManagedThreadId();

                }

            });

            SetupTask(t);

            return t;

        }

        protected Task<TResult> ActorEnqueue<TResult, T>(Func<T, TResult> func, T p)
        {

            return ActorEnqueue(() => { return func(p); });

        }

        protected Task<TResult> ActorEnqueue<TResult, T1, T2>(Func<T1, T2, TResult> func, T1 p1, T2 p2)
        {

            return ActorEnqueue(() => { return func(p1, p2); });

        }

        protected Task<TResult> ActorEnqueue<TResult, T1, T2, T3>(Func<T1, T2, T3, TResult> func, T1 p1, T2 p2, T3 p3)
        {

            return ActorEnqueue(() => { return func(p1, p2, p3); });

        }

        protected Task<TResult> ActorEnqueue<TResult, T1, T2, T3, T4>(Func<T1, T2, T3, T4, TResult> func, T1 p1, T2 p2, T3 p3, T4 p4)
        {

            return ActorEnqueue(() => { return func(p1, p2, p3, p4); });

        }

        protected Task<TResult> ActorEnqueue<TResult, T1, T2, T3, T4, T5>(Func<T1, T2, T3, T4, T5, TResult> func, T1 p1, T2 p2, T3 p3, T4 p4, T5 p5)
        {

            return ActorEnqueue(() => { return func(p1, p2, p3, p4, p5); });

        }

        protected Task<TResult> ActorEnqueue<TResult, T1, T2, T3, T4, T5, T6>(Func<T1, T2, T3, T4, T5, T6, TResult> func, T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6)
        {

            return ActorEnqueue(() => { return func(p1, p2, p3, p4, p5, p6); });

        }

        protected Task<TResult> ActorEnqueue<TResult, T1, T2, T3, T4, T5, T6, T7>(Func<T1, T2, T3, T4, T5, T6, T7, TResult> func, T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7)
        {

            return ActorEnqueue(() => { return func(p1, p2, p3, p4, p5, p6, p7); });

        }

        protected Task<TResult> ActorEnqueue<TResult, T1, T2, T3, T4, T5, T6, T7, T8>(Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> func, T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8)
        {

            return ActorEnqueue(() => { return func(p1, p2, p3, p4, p5, p6, p7, p8); });

        }

        //ActorEnqueue with no RetainedTaskList check

        protected Task ActorEnqueueNoTaskListCheck(Action action)
        {

            Task t = new Task(() => {

                ResetOnIdleEvent();

                SetManagedThreadId();

                try
                {

                    action();

                }
                finally
                {

                    RemoveCurrentTask();

                    //Now seen as not executing

                    //CheckIsDone();

                    NextTask();

                }

            });

            SetupTask(t);

            return t;

        }

        //Returning void

        //Ignoring the Exception

        protected void ActorEnqueueIgnoreNoTaskListCheck(Action action)
        {

            Task t = new Task(() => {

                ResetOnIdleEvent();

                SetManagedThreadId();

                try
                {

                    action();

                }
                finally
                {

                    RemoveCurrentTask();

                    NextTask();

                }

            });

            SetupTask(t);

            myRetainedTaskList.Add(t);

        }

        //FailFast if Exception is thrown

        protected void ActorEnqueueFailFastNoTaskListCheck(Action action)
        {

            Task t = new Task(() => {

                ResetOnIdleEvent();

                SetManagedThreadId();

                try
                {

                    action();

                }
                finally
                {

                    RemoveCurrentTask();

                    NextTask();

                }

            });

            SetupTask(t);

            myRetainedTaskList.Add(t, (task) => {

                if (task.Exception != null)
                {

                    Environment.FailFast("Un-caught ActSharp.Actor Exception", task.Exception);

                }

            }, ContinuationContext.Immediate);

        }

        //Call continuation action if Exception is thrown

        protected void ActorEnqueueNoTaskListCheck(Action action, Action<Task> continuationAction, ContinuationContext continuationContext = ContinuationContext.Immediate)
        {

            Task t = new Task(() => {

                ResetOnIdleEvent();

                SetManagedThreadId();

                try
                {

                    action();

                }
                finally
                {

                    RemoveCurrentTask();

                    NextTask();

                }

            });

            SetupTask(t);

            myRetainedTaskList.Add(t, continuationAction, continuationContext);

        }

        //Funcs

        protected Task<TResult> ActorEnqueueNoTaskListCheck<TResult>(Func<TResult> func)
        {

            Task<TResult> t = new Task<TResult>(() => {

                ResetOnIdleEvent();

                SetManagedThreadId();

                try
                {

                    return func();

                }
                finally
                {

                    RemoveCurrentTask();

                    //Now seen as not executing

                    //CheckIsDone();

                    NextTask();

                    //Now possibly seen as executing again

                    //InvaldateManagedThreadId();

                }

            });

            //Task<TResult> at = new Task<TResult>(t);

            SetupTask(t);

            return t;

        }

        //A way to manualy check the reatined task list

        protected void ActorCheckRetainedTasks()
        {

            myRetainedTaskList.Check();

        }

        public virtual void Dispose()
        {

            //if (!ActorIsIdle)
            //    throw new ActorStillActiveException();

            myOnIdleEvent.Wait();

            myOnIdleEvent.Dispose();

        }

        //Task Executors
        
    }

}
