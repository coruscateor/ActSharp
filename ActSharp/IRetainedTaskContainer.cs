using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ActSharp
{

    public interface IRetainedTaskContainer //<TTask>
    {

        //TTask Task
        //{

        //    get;

        //}

        //<TTask>

        bool Check(Action<IRetainedTaskContainer> taskAddOrEnqueueAction, Action<Task> actorTaskAction = null);

        bool Has(Task task);

        bool Has(IActorTask task);

        bool IsCompleted
        {

            get;

        }

    }

}
