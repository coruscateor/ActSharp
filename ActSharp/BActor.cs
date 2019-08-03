using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections;
using ActSharp.Executors;

namespace ActSharp
{

    /// <summary>
    /// A bocking queue implementation of the Actor class.
    ///
    /// The queue is synchronised with Monitor locks hence the name "BActor"
    /// 
    /// This implementation may yeild better performance than the default Actor in highly-concurrent situations
    /// </summary>
    public abstract class BActor : ActorBase, IActor, IDisposable
    {

        Queue<Task> myTaskQueue = new Queue<Task>();

        public BActor()
        {
        }

        public sealed override int ActorExecuteQueueCount
        {

            get
            {

                lock (myTaskQueue)
                { 

                    return myTaskQueue.Count;

                }

            }

        }

        public sealed override bool ActorExecuteQueueIsEmpty
        {

            get
            {

                lock (myTaskQueue)
                {

                    return myTaskQueue.Count < 1;

                }

            }

        }

        [DoNotCall]
        protected bool __ActorExecuteQueueIsEmptyNotLocked
        {

            get
            {

                //__ActorCheckIsSameAssembly();

                //AssFrameChecker assFrameChecker = new AssFrameChecker();

                return myTaskQueue.Count < 1;

            }

        }

        //The actor is active when it has a current task or is about to set the current task

        //public override bool ActorIsActive
        //{

        //    get
        //    {

        //        bool taken = false;

        //        myStateLock.Enter(ref taken);

        //        try
        //        {

        //            return myIsActive;

        //        }
        //        finally
        //        {

        //            if (taken)
        //                myStateLock.Exit();

        //        }

        //    }

        //}

        //Make sure actor is in the active state

        protected sealed override void __ActorSetInActive()
        {

            //__ActorCheckIsSameAssembly();

            //if (assFrameChecker == null)
            //    assFrameChecker = new AssFrameChecker(1);

            lock (myTaskQueue)
            {

                bool taken = false;

                myStateLock.Enter(ref taken);

                try
                {

                    //Make sure the task queue is empty

                    //Could be detrimental for threads waiting on myStateLock

                    if (!(myTaskQueue.Count < 1))
                        return;

                    //myCurrentTask = null;

                    myManagedThreadId = -1;

                    myIsActive = false;

                }
                finally
                {

                    if (taken)
                        myStateLock.Exit();

                }

            }

            myOnIdleEvent.Set();

        }

        //private void SetTaskAndStart(Task nextTask)
        //{

        //    myOnIdleEvent.Reset();

        //    bool taken = false;

        //    myStateLock.Enter(ref taken);

        //    try
        //    {

        //        myCurrentTask = nextTask;

        //    }
        //    finally
        //    {

        //        if (taken)
        //            myStateLock.Exit();

        //    }

        //    nextTask.Start();

        //}

        protected sealed override void __ActorNextTask()
        {

            //__ActorCheckIsSameAssembly();

            //if (assFrameChecker == null)
            //    assFrameChecker = new AssFrameChecker(1);

            Task nextTask;

            bool result;

            lock (myTaskQueue)
            {

                result = myTaskQueue.TryDequeue(out nextTask);

            }

            if (result)
            {

                //SetTaskAndStart(nextTask);

                nextTask.Start();

            }
            else
            {

                __ActorSetInActive();

            }

        }

        //Continuations

        //protected override Task ActorContinueWith(Action action)
        //{

        //    Task task = new Task(action);

        //    lock (myTaskQueue)
        //    {

        //        myTaskQueue.Enqueue(task);

        //    }

        //    return task;

        //}

        protected sealed override void __ActorEnqueue(Task task)
        {

            //__ActorCheckIsSameAssembly();

            //if (assFrameChecker == null)
            //    assFrameChecker = new AssFrameChecker(1);

            __ActorCheckTask(task);

            lock (myTaskQueue)
            {

                myTaskQueue.Enqueue(task);

            }

            //Check next task if actor is inactive

            if (__ActorTrySetIsActive())
            {

                __ActorNextTask();

            }

        }
        
    }

}
