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
    public abstract class Actor : ActorBase, IActor, IDisposable
    {

        ConcurrentQueue<Task> myTaskQueue = new ConcurrentQueue<Task>();

        public Actor()
        {
        }

        public sealed override int ActorExecuteQueueCount
        {

            get
            {

                return myTaskQueue.Count;

            }

        }

        public sealed override bool ActorExecuteQueueIsEmpty
        {

            get
            {

                return myTaskQueue.IsEmpty;

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

            //if (assFrameChecker == null)
            //    assFrameChecker = new AssFrameChecker(1);

            //__ActorCheckIsSameAssembly();

            //AssFrameChecker assFrameChecker = new AssFrameChecker();

            bool taken = false;

            myStateLock.Enter(ref taken);

            try
            {

                //Make sure the task queue is empty

                //Could be detrimental for threads waiting on myStateLock

                if (!myTaskQueue.IsEmpty)
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

            Task nextTask;

            if (myTaskQueue.TryDequeue(out nextTask))
            {

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

        //    myTaskQueue.Enqueue(task);

        //    return task;

        //}

        protected sealed override void __ActorEnqueue(Task task)
        {

            //if (assFrameChecker == null)
            //    assFrameChecker = new AssFrameChecker(1);

            //__ActorCheckIsSameAssembly();

            __ActorCheckTask(task);

             myTaskQueue.Enqueue(task);

            //Check next task if actor is inactive

            if (__ActorTrySetIsActive())
            {

                __ActorNextTask();

            }

        }

    }

}
