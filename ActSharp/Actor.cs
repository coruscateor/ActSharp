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

        ConcurrentQueue<Task> myTaskQueue = new ConcurrentQueue<Task>();

        Task myCurrentTask;

        RetainedTaskList myRetainedTaskList = new RetainedTaskList();

        ManualResetEventSlim myOnIdleEvent = new ManualResetEventSlim();

        int myManagedThreadId = -1;

        SpinLock myStateLock;

        bool myIsActive;

        public Actor()
        {
        }

        public int ActorExecuteQueueCount
        {

            get
            {

                return myTaskQueue.Count;

            }

        }

        public bool ActorExecuteQueueIsEmpty
        {

            get
            {

                return myTaskQueue.IsEmpty;

            }

        }

        //The actor is active when it has a current task or is about to set the current task

        public bool ActorIsActive
        {

            get
            {

                bool taken = false;

                myStateLock.Enter(ref taken);

                try
                {

                    return myIsActive;

                }
                finally
                {

                    if (taken)
                        myStateLock.Exit();

                }

            }

        }

        //Make sure actor is in the active state

        private bool TrySetActorIsActive()
        {

            bool taken = false;

            myStateLock.Enter(ref taken);

            try
            {

                if (!myIsActive)
                {

                    myIsActive = true;

                    return true;

                }

            }
            finally
            {

                if (taken)
                    myStateLock.Exit();

            }

            return false;

        }

        private void SetInActive()
        {

            bool taken = false;

            myStateLock.Enter(ref taken);

            try
            {

                //Make sure the task queue is empty

                //Could be detrimental for threads waiting on myStateLock

                if (!myTaskQueue.IsEmpty)
                    return;

                myCurrentTask = null;

                myManagedThreadId = -1;

                myIsActive = false;

            }
            finally
            {

                if (taken)
                    myStateLock.Exit();

            }

        }

        public bool ActorIsIdle
        {

            get
            {

                return myOnIdleEvent.IsSet;

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

        }

        public static void ActorWaitAll(params Actor[] actors)
        {

            WaitAllImpl(actors);

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

                    if(!actor.ActorIsActive)
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

        public int ActorManagedThreadId
        {

            get
            {

                bool taken = false;

                myStateLock.Enter(ref taken);

                try
                {

                    return myManagedThreadId;

                }
                finally
                {

                    if (taken)
                        myStateLock.Exit();

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

            myStateLock.Enter(ref taken);

            try
            {

                myManagedThreadId = threadId;

            }
            finally
            {

                if (taken)
                    myStateLock.Exit();

            }

        }

        private void ThrowIfExecutingAndCurrentThreadsAreTheSame()
        {

            if (ActorIsCurrentThread)
                throw new ActorSelfWaitException();

        }

        //private void RemoveCurrentTask()
        //{

        //    bool taken = false;

        //    myStateLock.Enter(ref taken);

        //    try
        //    {

        //        myCurrentTask = null;

        //    }
        //    finally
        //    {

        //        if (taken)
        //            myStateLock.Exit();

        //    }

        //}

        private void SetTaskAndStart(Task nextTask)
        {

            myOnIdleEvent.Reset();

            bool taken = false;

            myStateLock.Enter(ref taken);

            try
            {

                myCurrentTask = nextTask;

            }
            finally
            {

                if (taken)
                    myStateLock.Exit();

            }

            nextTask.Start();

        }

        private void NextTask()
        {

            Task nextTask;

            if (myTaskQueue.TryDequeue(out nextTask))
            {

                SetTaskAndStart(nextTask);

            }
            else if(myRetainedTaskList.HasTasks)
            {

                nextTask = new Task(CheckRetainedTaskListOnly);

                SetTaskAndStart(nextTask);

            }
            else
            {
                
                SetInActive();

                myOnIdleEvent.Set();

            }

        }

        void CheckRetainedTaskListOnly()
        {

            ActorCheckRetainedTasks();

            NextTask();

        }

        //Continuations

        protected void ActorContinueWith(Action action)
        {

            Task task = new Task(action);

            myTaskQueue.Enqueue(task);

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

        //    myTaskQueue.Enqueue(task);

        //    if (!ActorIsExecuting)
        //        NextTask();

        //}

        private void SetupTask(Task task)
        {

            myTaskQueue.Enqueue(task);

            //Check next task if actor is inactive

            if (TrySetActorIsActive())
            {

                NextTask();

            }

        }

        //Delegate Queueing

        //Actions

        protected Task ActorEnqueue(Action action)
        {

            Task t = new Task(() => {

                SetManagedThreadId();

                ActorCheckRetainedTasks();

                try
                {

                    action();

                }
                finally
                {

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

                SetManagedThreadId();

                ActorCheckRetainedTasks();

                try
                {

                    action();

                }
                finally
                {

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

                SetManagedThreadId();

                ActorCheckRetainedTasks();

                try
                {

                    action();

                }
                catch (Exception e)
                {

                    Environment.FailFast("Un-caught ActSharp.Actor Exception", e);

                }
                finally
                {

                    NextTask();

                }

            });

            SetupTask(t);

        }

        //Call continuation action if Exception is thrown

        protected void ActorEnqueue(Action action, Action<Task> continuationAction, ContinuationContext continuationContext = ContinuationContext.Immediate)
        {

            Task t = new Task(() => {

                SetManagedThreadId();

                ActorCheckRetainedTasks();

                try
                {

                    action();

                }
                finally
                {

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

                SetManagedThreadId();

                ActorCheckRetainedTasks();

                try
                {

                    return func();

                }
                finally
                {

                    NextTask();

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

                SetManagedThreadId();

                try
                {

                    action();

                }
                finally
                {
                    
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

                SetManagedThreadId();

                try
                {

                    action();

                }
                finally
                {

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

                SetManagedThreadId();

                try
                {

                    action();

                }
                catch(Exception e)
                {

                    Environment.FailFast("Un-caught ActSharp.Actor Exception", e);

                }
                finally
                {

                    NextTask();

                }

            });

            SetupTask(t);

        }

        //Call continuation action if Exception is thrown

        protected void ActorEnqueueNoTaskListCheck(Action action, Action<Task> continuationAction, ContinuationContext continuationContext = ContinuationContext.Immediate)
        {

            Task t = new Task(() => {

                SetManagedThreadId();

                try
                {

                    action();

                }
                finally
                {

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

                SetManagedThreadId();

                try
                {

                    return func();

                }
                finally
                {

                    NextTask();

                }

            });

            SetupTask(t);

            return t;

        }

        //A way to manualy check the reatined task list

        protected void ActorCheckRetainedTasks()
        {

            myRetainedTaskList.Check(myTaskQueue);

        }

        public virtual void Dispose()
        {

            myOnIdleEvent.Wait();

            myOnIdleEvent.Dispose();

        }
        
    }

}
