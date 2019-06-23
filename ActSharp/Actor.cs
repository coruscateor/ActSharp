﻿using System;
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

        public override int ActorExecuteQueueCount
        {

            get
            {

                return myTaskQueue.Count;

            }

        }

        public override bool ActorExecuteQueueIsEmpty
        {

            get
            {

                return myTaskQueue.IsEmpty;

            }

        }

        //The actor is active when it has a current task or is about to set the current task

        public override bool ActorIsActive
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

        protected override void __ActorSetInActive()
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

        protected override void __ActorNextTask()
        {

            //__ActorCheckIsSameAssembly();

            //if (assFrameChecker == null)
            //    assFrameChecker = new AssFrameChecker(1);

            Task nextTask;

            if (myTaskQueue.TryDequeue(out nextTask))
            {

                //SetTaskAndStart(nextTask);

                nextTask.Start();

            }
            else
            {

                __ActorSetInActive();

                myOnIdleEvent.Set();

            }

        }

        //Continuations

        //protected override Task ActorContinueWith(Action action)
        //{

        //    Task task = new Task(action);

        //    myTaskQueue.Enqueue(task);

        //    return task;

        //}

        protected override void __ActorEnqueue(Task task)
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

        public static ActorTask Call(Func<Task, ActorTask> func, Task withTask)
        {

            return func(withTask);

        }

        public static ActorTask Call(Func<ActorTask, ActorTask> func, ActorTask withTask)
        {

            return func(withTask);

        }

        public static ActorTask<T> Call<T>(Func<Task<T>, ActorTask<T>> func, Task<T> withTask)
        {

            return func(withTask);

        }

        public static ActorTask<T> Call<T>(Func<ActorTask<T>, ActorTask<T>> func, ActorTask<T> withTask)
        {

            return func(withTask);

        }

        public static ActorTask<TR> Call<T, TR>(Func<Task<T>, ActorTask<TR>> func, Task<T> withTask)
        {

            return func(withTask);

        }

        public static ActorTask<TR> Call<T, TR>(Func<ActorTask<T>, ActorTask<TR>> func, ActorTask<T> withTask)
        {

            return func(withTask);

        }

    }

}
