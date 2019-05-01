using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Concurrent;
using ActSharp.Async;

namespace ActSharp
{

    /// <summary>
    /// For retaining task objects beyond the lifetime of any given method call.
    /// </summary>
    public class RetainedTaskList
    {

        readonly List<TaskHolder> myTasks;
        
        //Execute any provided delegates on other threads if relevant

        readonly ConcurrentQueue<Task> myActorExQueue;

        public RetainedTaskList(ConcurrentQueue<Task> actorExQueue)
        {

            myTasks = new List<TaskHolder>(0);

            //myContinueOnCurrentThread = continueOnCurrentThread;

            myActorExQueue = actorExQueue;

        }

        public void Add(Task task, Action<Task> action = null, ContinuationContext continuationContext = ContinuationContext.Actor, IEnumerable<IAsyncResult> prerequisites = null)
        {

            CheckTaskIsScheduled(task);

            myTasks.Add(new TaskHolder(task, action, continuationContext, prerequisites));

        }

        public void Add<T>(Task<T> task, Action<Task> action = null, ContinuationContext continuationContext = ContinuationContext.Actor, IEnumerable<IAsyncResult> prerequisites = null)
        {

            CheckTaskIsScheduled(task);

            myTasks.Add(new TaskHolder(task, action, continuationContext, prerequisites));

        }

        public int Remove(Task task)
        {

            for (int i = 0; i < myTasks.Count; i++)
            {

                var item = myTasks[i];

                if (item.Has(task))
                {

                    //Remove item from list and decrement index

                    myTasks.RemoveAt(i);

                    return i;

                }

            }

            return -1;

        }

        void CheckTaskIsScheduled(Task task)
        {

            if (task.Status == TaskStatus.Created)
                throw new TaskNotScheduledException();

        }

        public int Count
        {

            get
            {

                return myTasks.Count;

            }

        }

        public bool HasTasks
        {

            get
            {

                return myTasks.Count > 0;

            }

        }

        public void Check()
        {

            for(int i = 0; i < myTasks.Count; i++)
            {

                var item = myTasks[i];

                if(item.Check(myTasks, myActorExQueue))
                {

                    //Remove item from list and decrement index

                    myTasks.RemoveAt(i);

                    i--;

                }

            }

        }

        class TaskHolder
        {

            readonly Task myTask;

            readonly Action<Task> myAction;

            readonly ContinuationContext myContinuationContext;

            readonly IEnumerable<IAsyncResult> myPrerequisites;

            public TaskHolder(Task task, Action<Task> action = null, ContinuationContext continuationContext = ContinuationContext.Actor, IEnumerable<IAsyncResult> prerequisites = null)
            {

                myTask = task;

                myAction = action;

                myContinuationContext = continuationContext;

                myPrerequisites = prerequisites;

            }

            bool CheckPrerequisites()
            {

                if (myPrerequisites != null)
                {

                    foreach (var item in myPrerequisites)
                    {

                        if (!item.IsCompleted)
                            return false;

                    }

                }

                return true;

            }

            public IEnumerable<IAsyncResult> Prerequisites
            {

                get
                {

                    return myPrerequisites;

                }

            }

            public bool Check(List<TaskHolder> tasks, ConcurrentQueue<Task> actorExQueue = null)
            {

                if (myTask.IsCompleted && CheckPrerequisites())
                {

                    if (myAction != null)
                    {

                        switch (myContinuationContext)
                        {

                            case ContinuationContext.Actor:

                                if (actorExQueue != null)
                                {

                                    actorExQueue.Enqueue(new Task(() => { myAction(myTask); }));

                                }
                                else
                                {

                                    tasks.Add(new TaskHolder(myAction.Async(myTask)));

                                }

                                break;

                            case ContinuationContext.Async:

                                tasks.Add(new TaskHolder(myAction.Async(myTask)));

                                break;

                            case ContinuationContext.Immediate:

                                myAction(myTask);

                                break;

                        }

                    }

                    return true;

                }

                return false;

            }

            public bool Has(Task task)
            {

                return myTask == task;

            }

        }

    }

}
