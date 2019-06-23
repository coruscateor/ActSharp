using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ActSharp
{

    public abstract class ActorWRTaskList : Actor
    {

        RetainedTaskList myRetainedTaskList;

        public ActorWRTaskList()
        {

            myRetainedTaskList = new RetainedTaskList(__ActorEnqueue);

        }

        protected override void ActorPreDelegate()
        {

            myRetainedTaskList.Check(); //Incorrect

        }

        //private void NextRetainedTask()
        //{

        //    //else if (myRetainedTaskList.HasTasks)
        //    //{

        //    //    nextTask = new Task(CheckRetainedTaskListOnly);

        //    //    SetTaskAndStart(nextTask);

        //    //}

        //}

        //Retained Tasks

        protected void ActorRetain(Task task, Action<Task, ContinuationContext> action = null, ContinuationContext continuationContext = ContinuationContext.Actor, IEnumerable<IAsyncResult> prerequisites = null)
        {

            myRetainedTaskList.Add(task, action, continuationContext, prerequisites);

        }

        protected void ActorRetain<T>(Task<T> task, Action<Task, ContinuationContext> action = null, ContinuationContext continuationContext = ContinuationContext.Actor, IEnumerable<IAsyncResult> prerequisites = null)
        {

            myRetainedTaskList.Add<T>(task, action, continuationContext, prerequisites);

        }

        protected void Add(ActorTask task, Action<ActorTask, ContinuationContext> action = null, ContinuationContext continuationContext = ContinuationContext.Actor, IEnumerable<IAsyncResult> prerequisites = null)
        {

            myRetainedTaskList.Add(task, action, continuationContext, prerequisites);

        }

        protected void Add<T>(ActorTask<T> task, Action<ActorTask<T>, ContinuationContext> action = null, ContinuationContext continuationContext = ContinuationContext.Actor, IEnumerable<IAsyncResult> prerequisites = null)
        {

            myRetainedTaskList.Add<T>(task, action, continuationContext, prerequisites);

        }

    }

}
