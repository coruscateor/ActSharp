using ActSharp.Async;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ActSharp
{

    public abstract class RetainedTaskContainerBase<TTask> : IRetainedTaskContainer //<TTask>
        where TTask : IAsyncResult
    {


        protected readonly TTask myTask;

        //Action<TTask> myOnCompleted;

        //Action<TTask> myOnError;

        //List<ITaskHolder> myTasks;

        //readonly bool myContinueOnCurrentThread;

        readonly Action<TTask, ContinuationContext> myAction;

        //readonly bool myContinueInActorContext;

        readonly ContinuationContext myContinuationContext;

        readonly IEnumerable<IAsyncResult> myPrerequisites;

        /*, List<ITaskHolder> tasks*/

        //public TaskHolderBase(TTask task, Action<TTask> onCompleted = null, Action<TTask> onError = null, bool continueOnCurrentThread = false, IEnumerable<IAsyncResult> prerequisites = null)
        public RetainedTaskContainerBase(TTask task, Action<TTask, ContinuationContext> action = null, ContinuationContext continuationContext = ContinuationContext.Actor, IEnumerable<IAsyncResult> prerequisites = null)
        {

            myTask = task;

            //myOnCompleted = onCompleted;

            //myOnError = onError;

            ////myTasks = tasks;

            //myContinueOnCurrentThread = continueOnCurrentThread;

            myAction = action;

            myContinuationContext = continuationContext;

            myPrerequisites = prerequisites;

        }

        public bool IsCompleted
        {

            get
            {

                return myTask.IsCompleted;

            }

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

        public TTask Task
        {

            get
            {
                return myTask;

            }

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

        public bool Check(Action<IRetainedTaskContainer> taskAddOrEnqueueAction, Action<Task> actorExQueueAction = null)
        {

            if (myTask.IsCompleted && CheckPrerequisites())
            {

                if (myAction != null)
                {

                    switch (myContinuationContext)
                    {

                        case ContinuationContext.Actor:

                            if (actorExQueueAction != null)
                            {

                                actorExQueueAction(new Task(() => { myAction(myTask, ContinuationContext.Actor); }));

                            }
                            else
                            {

                                myAction(myTask, ContinuationContext.Immediate);

                            }

                            break;

                        case ContinuationContext.Async:

                            taskAddOrEnqueueAction(new RetainedTaskContainer(myAction.Async(myTask, ContinuationContext.Async)));

                            break;

                        case ContinuationContext.Immediate:

                            myAction(myTask, ContinuationContext.Immediate);

                            break;

                    }

                }

                return true;

            }

            return false;

        }

        public abstract bool Has(Task task);

        public abstract bool Has(IActorTask task);

    }

}
