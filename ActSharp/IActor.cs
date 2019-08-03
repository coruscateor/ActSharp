using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ActSharp
{

    public interface IActor : IDisposable
    {

        int ActorExecuteQueueCount
        {

            get;

        }

        bool ActorExecuteQueueIsEmpty
        {

            get;

        }

        bool ActorIsActive
        {

            get;

        }

        bool ActorIsIdle
        {

            get;

        }

        void ActorWaitForIdle();

        bool ActorWaitForIdle(int millisecondsTimeout);

        bool ActorWaitForIdle(int millisecondsTimeout, CancellationToken cancellationToken);

        bool ActorWaitForIdle(TimeSpan timeout);

        bool ActorWaitForIdle(TimeSpan timeout, CancellationToken cancellationToken);

        int ActorManagedThreadId
        {

            get;

        }

        bool ActorIsCurrentThread
        {

            get;

        }

    }

}
