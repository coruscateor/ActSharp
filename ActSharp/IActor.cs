using System;
using System.Collections.Generic;
using System.Text;

namespace ActSharp
{

    public interface IActor
    {
        
        bool ActorIsExecuting
        {

            get;

        }

        //void SetActorDone();

        int ActorExecuteQueueCount
        {

            get;

        }

        bool ActorExecuteQueueIsEmpty
        {

            get;

        }

    }

}
