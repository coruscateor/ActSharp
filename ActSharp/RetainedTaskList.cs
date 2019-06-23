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
    public sealed class RetainedTaskList
    {

        readonly List<IRetainedTaskContainer> myTasks;

        //Execute any provided delegates on other threads if relevant

        readonly Action<Task> myActorExQueueAction;

        public RetainedTaskList(Action<Task> actorExQueueAction)
        {

            myTasks = new List<IRetainedTaskContainer>();

            //myContinueOnCurrentThread = continueOnCurrentThread;

            myActorExQueueAction = actorExQueueAction;

        }

        public void Add(Task task, Action<Task, ContinuationContext> action = null, ContinuationContext continuationContext = ContinuationContext.Actor, IEnumerable<IAsyncResult> prerequisites = null)
        {

            CheckTaskIsScheduled(task);

            myTasks.Add(new RetainedTaskContainer(task, action, continuationContext, prerequisites));

        }

        public void Add<T>(Task<T> task, Action<Task<T>, ContinuationContext> action = null, ContinuationContext continuationContext = ContinuationContext.Actor, IEnumerable<IAsyncResult> prerequisites = null)
        {

            CheckTaskIsScheduled(task);

            myTasks.Add(new RetainedTaskContainer<T>(task, action, continuationContext, prerequisites));

        }

        public void Add(ActorTask task, Action<ActorTask, ContinuationContext> action = null, ContinuationContext continuationContext = ContinuationContext.Actor, IEnumerable<IAsyncResult> prerequisites = null)
        {

            CheckTaskIsScheduled(task);

            myTasks.Add(new RetainedActorTaskContainer(task, action, continuationContext, prerequisites));

        }

        public void Add<T>(ActorTask<T> task, Action<ActorTask<T>, ContinuationContext> action = null, ContinuationContext continuationContext = ContinuationContext.Actor, IEnumerable<IAsyncResult> prerequisites = null)
        {

            CheckTaskIsScheduled(task);

            myTasks.Add(new RetainedActorTaskContainer<T>(task, action, continuationContext, prerequisites));

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

        public int Remove(IActorTask task)
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

        void CheckTaskIsScheduled(IActorTask task)
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

            //foreach (var item in myTasks)
            //{

            //    item.Check();

            //}

            //if (!HasTasks)
            //    return;

            for (int i = 0; i < myTasks.Count; i++)
            {

                var item = myTasks[i];

                if (item.Check(myTasks.Add, myActorExQueueAction))
                {

                    //Remove item from list and decrement index

                    myTasks.RemoveAt(i);

                    i--;

                }

            }

        }

    }

}
