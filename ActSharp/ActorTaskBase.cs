using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ActSharp
{

    public abstract class ActorTaskBase<TTask> : IActorTask
        where TTask : Task
    {

        protected TTask myTask;

        public ActorTaskBase(TTask task)
        {

            myTask = task;

        }

        //properties

        public TaskCreationOptions CreationOptions
        {

            get
            {

                return myTask.CreationOptions;

            }

        }

        public AggregateException Exception
        {

            get
            {

                return myTask.Exception;

            }

        }

        public bool IsCompleted
        {

            get
            {

                return myTask.IsCompleted;

            }

        }

        public bool IsCanceled
        {

            get
            {

                return myTask.IsCanceled;

            }

        }

        public object AsyncState
        {

            get
            {

                return myTask.AsyncState;

            }

        }

        public bool IsCompletedSuccessfully
        {

            get
            {

                return myTask.IsCompletedSuccessfully;

            }

        }

        public int Id
        {

            get
            {

                return myTask.Id;

            }

        }

        public bool IsFaulted
        {

            get
            {

                return myTask.IsFaulted;

            }

        }

        public TaskStatus Status
        {

            get
            {

                return myTask.Status;

            }

        }

        //Methods

        //Continuations

        public ConfiguredTaskAwaitable ConfigureAwait(bool continueOnCapturedContext)
        {

            return myTask.ConfigureAwait(continueOnCapturedContext);

        }

        public Task ContinueWith(Action<Task, object> continuationAction, object state)
        {

            return myTask.ContinueWith(continuationAction, state);

        }

        public Task ContinueWith(Action<Task, object> continuationAction, object state, CancellationToken cancellationToken)
        {

            return myTask.ContinueWith(continuationAction, state);

        }

        public Task ContinueWith(Action<Task, object> continuationAction, object state, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
        {

            return myTask.ContinueWith(continuationAction, state, cancellationToken, continuationOptions, scheduler);

        }

        public Task ContinueWith(Action<Task, object> continuationAction, object state, TaskContinuationOptions continuationOptions)
        {

            return myTask.ContinueWith(continuationAction, state, continuationOptions);

        }

        public Task ContinueWith(Action<Task, object> continuationAction, object state, TaskScheduler scheduler)
        {

            return myTask.ContinueWith(continuationAction, state, scheduler);

        }

        public Task ContinueWith(Action<Task> continuationAction)
        {

            return myTask.ContinueWith(continuationAction);

        }

        public Task ContinueWith(Action<Task> continuationAction, CancellationToken cancellationToken)
        {

            return myTask.ContinueWith(continuationAction, cancellationToken);

        }

        public Task ContinueWith(Action<Task> continuationAction, TaskContinuationOptions continuationOptions)
        {

            return myTask.ContinueWith(continuationAction, continuationOptions);

        }

        public Task ContinueWith(Action<Task> continuationAction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
        {

            return myTask.ContinueWith(continuationAction, cancellationToken, continuationOptions, scheduler);

        }

        public Task<TResult> ContinueWith<TResult>(Func<Task, object, TResult> continuationFunction, object state, CancellationToken cancellationToken)
        {

            return myTask.ContinueWith(continuationFunction, state, cancellationToken);

        }

        public Task ContinueWith(Action<Task> continuationAction, TaskScheduler scheduler)
        {

            return myTask.ContinueWith(continuationAction, scheduler);

        }

        public Task<TResult> ContinueWith<TResult>(Func<Task, TResult> continuationFunction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
        {

            return myTask.ContinueWith(continuationFunction, cancellationToken, continuationOptions, scheduler);

        }

        public Task<TResult> ContinueWith<TResult>(Func<Task, TResult> continuationFunction, TaskContinuationOptions continuationOptions)
        {

            return myTask.ContinueWith(continuationFunction, continuationOptions);

        }

        public Task<TResult> ContinueWith<TResult>(Func<Task, TResult> continuationFunction, TaskScheduler scheduler)
        {

            return myTask.ContinueWith(continuationFunction, scheduler);

        }

        public Task<TResult> ContinueWith<TResult>(Func<Task, object, TResult> continuationFunction, object state, TaskScheduler scheduler)
        {

            return myTask.ContinueWith(continuationFunction, state, scheduler);

        }

        public Task<TResult> ContinueWith<TResult>(Func<Task, TResult> continuationFunction, CancellationToken cancellationToken)
        {

            return myTask.ContinueWith(continuationFunction, cancellationToken);

        }

        public Task<TResult> ContinueWith<TResult>(Func<Task, TResult> continuationFunction)
        {

            return myTask.ContinueWith(continuationFunction);

        }

        public Task<TResult> ContinueWith<TResult>(Func<Task, object, TResult> continuationFunction, object state, TaskContinuationOptions continuationOptions)
        {

            return myTask.ContinueWith(continuationFunction, state, continuationOptions);

        }

        public Task<TResult> ContinueWith<TResult>(Func<Task, object, TResult> continuationFunction, object state)
        {

            return myTask.ContinueWith(continuationFunction, state);

        }
 
        public Task<TResult> ContinueWith<TResult>(Func<Task, object, TResult> continuationFunction, object state, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
        {

            return myTask.ContinueWith(continuationFunction, state, cancellationToken, continuationOptions, scheduler);

        }

        //

        public WaitHandle AsyncWaitHandle => ((IAsyncResult)myTask).AsyncWaitHandle;

        public bool CompletedSynchronously => ((IAsyncResult)myTask).CompletedSynchronously;

        public void Dispose()
        {

            myTask.Dispose();

        }

        public TaskAwaiter GetAwaiter()
        {

            return myTask.GetAwaiter();

        }

        public void Wait()
        {

            myTask.Wait();

        }

        public void Wait(CancellationToken cancellationToken)
        {

            myTask.Wait(cancellationToken);

        }

        public bool Wait(int millisecondsTimeout)
        {

            return myTask.Wait(millisecondsTimeout);

        }

        public bool Wait(int millisecondsTimeout, CancellationToken cancellationToken)
        {

            return myTask.Wait(millisecondsTimeout, cancellationToken);

        }

        public bool Wait(TimeSpan timeout)
        {

            return myTask.Wait(timeout);

        }

    }

}
