using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ActSharp
{

    public class RetainedTaskContainer : RetainedTaskContainerBase<Task>
    {

        public RetainedTaskContainer(Task task, Action<Task, ContinuationContext> action = null, ContinuationContext continuationContext = ContinuationContext.Actor, IEnumerable<IAsyncResult> prerequisites = null)
            : base(task, action, continuationContext, prerequisites)
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

    public class RetainedTaskContainer<TResult> : RetainedTaskContainerBase<Task<TResult>>
    {

        public RetainedTaskContainer(Task<TResult> task, Action<Task<TResult>, ContinuationContext> action = null, ContinuationContext continuationContext = ContinuationContext.Actor, IEnumerable<IAsyncResult> prerequisites = null)
            : base(task, action, continuationContext, prerequisites)
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

    public class RetainedActorTaskContainer : RetainedTaskContainerBase<ActorTask>
    {

        public RetainedActorTaskContainer(ActorTask task, Action<ActorTask, ContinuationContext> action = null, ContinuationContext continuationContext = ContinuationContext.Actor, IEnumerable<IAsyncResult> prerequisites = null)
            : base(task, action, continuationContext, prerequisites)
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

    public class RetainedActorTaskContainer<TResult> : RetainedTaskContainerBase<ActorTask<TResult>>
    {

        public RetainedActorTaskContainer(ActorTask<TResult> task, Action<ActorTask<TResult>, ContinuationContext> action = null, ContinuationContext continuationContext = ContinuationContext.Actor, IEnumerable<IAsyncResult> prerequisites = null)
            : base(task, action, continuationContext, prerequisites)
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
