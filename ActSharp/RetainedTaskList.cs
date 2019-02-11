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

    public class RetainedTaskList
    {

        //: IEnumerable<Task>, IEnumerable

        List<ITaskHolder> myTasks;

        //Execute any provided delegates on other threads if relevant

        //readonly bool myContinueOnCurrentThread;

        ConcurrentQueue<Task> myActorExQueue;

        public RetainedTaskList(ConcurrentQueue<Task> actorExQueue) //(bool continueOnCurrentThread = false)
        {

            myTasks = new List<ITaskHolder>(0);

            //myContinueOnCurrentThread = continueOnCurrentThread;

            myActorExQueue = actorExQueue;

        }

        //public void Add(Task task, Action<Task> onCompleted = null, Action<Task> onError = null, bool continueOnCurrentThread = false, IEnumerable<IAsyncResult> prerequisites = null)
        public void Add(Task task, Action<Task> action = null, bool continueInActorContext = false, IEnumerable<IAsyncResult> prerequisites = null)
        {

            CheckTaskIsScheduled(task);

            //myTasks.Add(new TaskHolder(task, myTasks, onCompleted, onError, continueOnCurrentThread, prerequisites));

            myTasks.Add(new TaskHolder(task, action, continueInActorContext, prerequisites));

        }

        //public void Add<T>(Task<T> task, Action<Task<T>> onCompleted = null, Action<Task<T>> onError = null, bool continueOnCurrentThread = false, IEnumerable<IAsyncResult> prerequisites = null)
        public void Add<T>(Task<T> task, Action<Task<T>> action = null, bool continueInActorContext = false, IEnumerable<IAsyncResult> prerequisites = null)
        {

            CheckTaskIsScheduled(task);

            //myTasks.Add(new TaskHolder<T>(task, myTasks, onCompleted, onError, continueOnCurrentThread, prerequisites));

            myTasks.Add(new TaskHolder<T>(task, action, continueInActorContext, prerequisites));

        }

        //public void Add(ActorTask task, Action<ActorTask> onCompleted = null, Action<ActorTask> onError = null, bool continueOnCurrentThread = false, IEnumerable<IAsyncResult> prerequisites = null)
        public void Add(ActorTask task, Action<ActorTask> action = null, bool continueInActorContext = false, IEnumerable<IAsyncResult> prerequisites = null)
        {

            CheckTaskIsScheduled(task);

            //myTasks.Add(new ActorTaskHolder(task, myTasks, onCompleted, onError, continueOnCurrentThread, prerequisites));

            myTasks.Add(new ActorTaskHolder(task, action, continueInActorContext, prerequisites));

        }

        //public void Add<T>(ActorTask<T> task, Action<ActorTask<T>> onCompleted = null, Action<ActorTask<T>> onError = null, bool continueOnCurrentThread = false, IEnumerable<IAsyncResult> prerequisites = null)
        public void Add<T>(ActorTask<T> task, Action<ActorTask<T>> action = null, bool continueInActorContext = false, IEnumerable<IAsyncResult> prerequisites = null)
        {

            CheckTaskIsScheduled(task);

            //myTasks.Add(new ActorTaskHolder<T>(task, myTasks, onCompleted, onError, continueOnCurrentThread, prerequisites));

            myTasks.Add(new ActorTaskHolder<T>(task, action, continueInActorContext, prerequisites));

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

        //public bool ContinueOnCurrentThread
        //{

        //    get
        //    {
        //        return myContinueOnCurrentThread;

        //    }

        //}

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

        //public IEnumerator<Task> GetEnumerator()
        //{
        //    return ((IEnumerable<Task>)myTasks).GetEnumerator();
        //}

        //IEnumerator IEnumerable.GetEnumerator()
        //{
        //    return ((IEnumerable<Task>)myTasks).GetEnumerator();
        //}

        interface ITaskHolder
        {

            bool Check(List<ITaskHolder> tasks, ConcurrentQueue<Task> actorExQueue);

            bool Has(Task task);

            bool Has(IActorTask task);

        }

        abstract class BaseTaskHolder<TTask> : ITaskHolder
            where TTask : IAsyncResult
        {


            protected readonly TTask myTask;

            //Action<TTask> myOnCompleted;

            //Action<TTask> myOnError;

            //List<ITaskHolder> myTasks;

            //readonly bool myContinueOnCurrentThread;

            readonly Action<TTask> myAction;

            readonly bool myContinueInActorContext;

            readonly IEnumerable<IAsyncResult> myPrerequisites;

            /*, List<ITaskHolder> tasks*/

            //public BaseTaskHolder(TTask task, Action<TTask> onCompleted = null, Action<TTask> onError = null, bool continueOnCurrentThread = false, IEnumerable<IAsyncResult> prerequisites = null)
            public BaseTaskHolder(TTask task, Action<TTask> action = null, bool continueInActorContext = false, IEnumerable<IAsyncResult> prerequisites = null)
            {

                myTask = task;

                //myOnCompleted = onCompleted;

                //myOnError = onError;

                ////myTasks = tasks;

                //myContinueOnCurrentThread = continueOnCurrentThread;

                myAction = action;

                myContinueInActorContext = continueInActorContext;

                myPrerequisites = prerequisites;

            }

            //bool IsFaulted(TTask task)
            //{

            //    var standardTask = task as Task;

            //    if(standardTask != null)
            //    {

            //        return standardTask.IsFaulted;

            //    }
            //    else
            //    {

            //        var actorTask = task as IActorTask;

            //        if(actorTask != null)
            //            return actorTask.IsFaulted;

            //    }

            //    return false;

            //}

            bool CheckPrerequisites()
            {

                if(myPrerequisites != null)
                {

                    foreach(var item in myPrerequisites)
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

            public bool Check(List<ITaskHolder> tasks, ConcurrentQueue<Task> actorExQueue)
            {

                if (myTask.IsCompleted && CheckPrerequisites())
                {

                    if (myAction != null)
                    {

                        if (myContinueInActorContext)
                        {

                            //Add to actor execution queue

                            actorExQueue.Enqueue(new Task(() => { myAction(myTask); }));

                        }
                        else
                        {

                            //Run asynchronously

                            tasks.Add(new TaskHolder(myAction.Async(myTask)));

                        }

                    }

                    return true;

                }

                //if (myTask.IsCompleted && CheckPrerequisites())
                //{

                //    if (IsFaulted(myTask))
                //    {

                //        if (myOnError != null)
                //        {

                //            if (myContinueOnCurrentThread)
                //            {

                //                myOnError(myTask);

                //            }
                //            else
                //            {

                //                tasks.Add(new TaskHolder(myOnError.Async(myTask), tasks));

                //            }

                //        }

                //        //Ignore error?

                //    }
                //    else
                //    {

                //        if (myOnCompleted != null)
                //        {

                //            if (myContinueOnCurrentThread)
                //            {

                //                myOnCompleted(myTask);

                //            }
                //            else
                //            {

                //                tasks.Add(new TaskHolder(myOnCompleted.Async(myTask), tasks));

                //            }

                //        }

                //    }

                //    return true;

                //}

                return false;

            }

            public abstract bool Has(Task task);

            public abstract bool Has(IActorTask task);

            //public bool Check()
            //{

            //    if (myTask.IsCompleted)
            //    {

            //        if (myTask.IsFaulted)
            //        {

            //            if (myOnError != null)
            //            {

            //                myTasks.Add(new TaskHolder(myOnError.Async(myTask), myTasks));

            //            }

            //        }
            //        else
            //        {

            //            if (myOnError != null)
            //            {

            //                myTasks.Add(new TaskHolder(myOnCompleted.Async(myTask), myTasks));

            //            }

            //        }

            //        return true;

            //    }

            //    return false;

            //}

        }

        class TaskHolder : BaseTaskHolder<Task>
        {

            //public TaskHolder(Task task, List<ITaskHolder> tasks, Action<Task> onCompleted = null, Action<Task> onError = null, bool continueOnCurrentThread = false, IEnumerable<IAsyncResult> prerequisites = null)
            //    : base(task, tasks, onCompleted, onError, continueOnCurrentThread, prerequisites)
            //{
            //}

            public TaskHolder(Task task, Action<Task> action = null, bool continueInActorContext = false, IEnumerable<IAsyncResult> prerequisites = null)
                : base(task, action, continueInActorContext, prerequisites)
            {
            }

            public override bool Has(Task task)
            {

                return myTask == task;

            }

            public override bool Has(IActorTask task)
            {

                return false;

            }

        }

        class TaskHolder<TResult> : BaseTaskHolder<Task<TResult>>
        {

            //public TaskHolder(Task<TResult> task, List<ITaskHolder> tasks, Action<Task<TResult>> onCompleted = null, Action<Task<TResult>> onError = null, bool continueOnCurrentThread = false, IEnumerable<IAsyncResult> prerequisites = null)
            //    : base(task, tasks, onCompleted, onError, continueOnCurrentThread, prerequisites)
            //{
            //}

            public TaskHolder(Task<TResult> task, Action<Task<TResult>> action = null, bool continueInActorContext = false, IEnumerable<IAsyncResult> prerequisites = null)
                : base(task, action, continueInActorContext, prerequisites)
            {
            }

            public override bool Has(Task task)
            {

                return myTask == task;

            }

            public override bool Has(IActorTask task)
            {

                return false;

            }

        }

        class ActorTaskHolder : BaseTaskHolder<ActorTask>
        {

            //public ActorTaskHolder(ActorTask task, List<ITaskHolder> tasks, Action<ActorTask> onCompleted = null, Action<ActorTask> onError = null, bool continueOnCurrentThread = false, IEnumerable<IAsyncResult> prerequisites = null)
            //    : base(task, tasks, onCompleted, onError, continueOnCurrentThread, prerequisites)
            //{
            //}

            public ActorTaskHolder(ActorTask task, Action<ActorTask> action = null, bool continueInActorContext = false, IEnumerable<IAsyncResult> prerequisites = null)
                : base(task, action, continueInActorContext, prerequisites)
            {
            }

            public override bool Has(Task task)
            {

                return false;

            }

            public override bool Has(IActorTask task)
            {

                return myTask == task;

            }

        }

        class ActorTaskHolder<TResult> : BaseTaskHolder<ActorTask<TResult>>
        {

            //public ActorTaskHolder(ActorTask<TResult> task, List<ITaskHolder> tasks, Action<ActorTask<TResult>> onCompleted = null, Action<ActorTask<TResult>> onError = null, bool continueOnCurrentThread = false, IEnumerable<IAsyncResult> prerequisites = null)
            //    : base(task, tasks, onCompleted, onError, continueOnCurrentThread, prerequisites)
            //{
            //}

            public ActorTaskHolder(ActorTask<TResult> task, Action<ActorTask<TResult>> action = null, bool continueInActorContext = false, IEnumerable<IAsyncResult> prerequisites = null)
                : base(task, action, continueInActorContext, prerequisites)
            {
            }

            public override bool Has(Task task)
            {

                return false;

            }

            public override bool Has(IActorTask task)
            {

                return myTask == task;

            }

        }

    }

}
