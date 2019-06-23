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

        //bool IsCompleted
        //{

        //    get;

        //}

        bool IsCanceled
        {

            get;

        }

        //object AsyncState
        //{

        //    get;

        //}

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

        //ActorTask Continue(Task withTask);

        //ActorTask Continue(ActorTask withTask);

        //ActorTask<T> Continue<T>(Task<T> withTask);

        //ActorTask<T> Continue<T>(ActorTask<T> withTask);

        //ActorTask<T> Continue<T>(Task withTask);

        //ActorTask<T> Continue<T>(ActorTask withTask);

        //ActorTask ContinueNoResult<T>(Task<T> withTask);

        //ActorTask ContinueNoResult<T>(ActorTask<T> withTask);

    }

    public interface IActorTask<TResult> : IActorTask, IAsyncResult, IDisposable
    {

        TResult Result
        {

            get;

        }

    }

}