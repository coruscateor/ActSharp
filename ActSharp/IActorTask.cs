using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ActSharp
{

    public interface IActorTask : IAsyncResult, IDisposable
    {

        TaskCreationOptions CreationOptions
        {

            get;

        }

        AggregateException Exception
        {

            get;

        }

        bool IsCanceled
        {

            get;

        }

        bool IsCompletedSuccessfully
        {

            get;

        }

        int Id
        {

            get;

        }

        bool IsFaulted
        {

            get;

        }

        TaskStatus Status
        {

            get;

        }

        void Wait();

        void Wait(CancellationToken cancellationToken);

        bool Wait(int millisecondsTimeout);

        bool Wait(int millisecondsTimeout, CancellationToken cancellationToken);

        bool Wait(TimeSpan timeout);

    }

    public interface IActorTask<TResult> : IActorTask, IAsyncResult, IDisposable
    {

        TResult Result
        {

            get;

        }

    }

}