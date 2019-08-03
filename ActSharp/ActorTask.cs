using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ActSharp
{

    //abstract protected 

    public sealed class ActorTask : ActorTaskBase<Task>, IActorTask, IAsyncResult, IDisposable
    {

        public ActorTask(Task task)
            : base(task)
        {
        }

        //ActorTask Continuations

        public Task<ActorTask> ContinueWith(Func<ActorTask, object, ActorTask> continuationFunction, object state)
        {

            return myTask.ContinueWith((t, s) => continuationFunction(this, s), state);

        }

        public Task<ActorTask> ContinueWith(Func<ActorTask, object, ActorTask> continuationFunction, object state, CancellationToken cancellationToken)
        {

            return myTask.ContinueWith((t, s) => continuationFunction(this, s), state, cancellationToken);

        }

        public Task<ActorTask> ContinueWith(Func<ActorTask, object, ActorTask> continuationFunction, object state, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
        {

            return myTask.ContinueWith((t, s) => continuationFunction(this, s), state, cancellationToken, continuationOptions, scheduler);

        }

        public Task<ActorTask> ContinueWith(Func<ActorTask, object, ActorTask> continuationFunction, object state, TaskContinuationOptions continuationOptions)
        {

            return myTask.ContinueWith((t, s) => continuationFunction(this, s), state, continuationOptions);

        }

        public Task<ActorTask> ContinueWith(Func<ActorTask, object, ActorTask> continuationFunction, object state, TaskScheduler scheduler)
        {

            return myTask.ContinueWith((t, s) => continuationFunction(this, s), state, scheduler);

        }

        public Task<ActorTask> ContinueWith(Func<ActorTask, ActorTask> continuationFunction)
        {

            return myTask.ContinueWith((t) => continuationFunction(this));

        }
        
        public Task<ActorTask> ContinueWith(Func<ActorTask, ActorTask> continuationFunction, CancellationToken cancellationToken)
        {

            return myTask.ContinueWith((t) => continuationFunction(this));

        }

        public Task<ActorTask> ContinueWith(Func<ActorTask, ActorTask> continuationFunction, TaskContinuationOptions continuationOptions)
        {

            return myTask.ContinueWith((t) => continuationFunction(this), continuationOptions);

        }
        
        public Task<ActorTask> ContinueWith(Func<ActorTask, ActorTask> continuationFunction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
        {

            return myTask.ContinueWith((t) => continuationFunction(this), cancellationToken, continuationOptions, scheduler);

        }
        
        public Task<ActorTask<TResult>> ContinueWith<TResult>(Func<ActorTask, object, ActorTask<TResult>> continuationFunction, object state, CancellationToken cancellationToken)
        {

            return myTask.ContinueWith((t, s) => continuationFunction(this, s), state, cancellationToken);

        }

        public Task<ActorTask> ContinueWith(Func<ActorTask, ActorTask> continuationFunction, TaskScheduler scheduler)
        {

            return myTask.ContinueWith((t) => continuationFunction(this), scheduler);

        }

        public Task<ActorTask<TResult>> ContinueWith<TResult>(Func<ActorTask, ActorTask<TResult>> continuationFunction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
        {

            return myTask.ContinueWith((t) => continuationFunction(this), cancellationToken, continuationOptions, scheduler);

        }

        public Task<ActorTask<TResult>> ContinueWith<TResult>(Func<ActorTask, ActorTask<TResult>> continuationFunction, TaskContinuationOptions continuationOptions)
        {

            return myTask.ContinueWith((t) => continuationFunction(this), continuationOptions);

        }
        
        public Task<ActorTask<TResult>> ContinueWith<TResult>(Func<ActorTask, ActorTask<TResult>> continuationFunction, TaskScheduler scheduler)
        {

            return myTask.ContinueWith((t) => continuationFunction(this), scheduler);

        }

        public Task<ActorTask<TResult>> ContinueWith<TResult>(Func<ActorTask, object, ActorTask<TResult>> continuationFunction, object state, TaskScheduler scheduler)
        {

            return myTask.ContinueWith((t, s) => continuationFunction(this, s), state, scheduler);

        }

        public Task<ActorTask<TResult>> ContinueWith<TResult>(Func<ActorTask, ActorTask<TResult>> continuationFunction, CancellationToken cancellationToken)
        {

            return myTask.ContinueWith((t) => continuationFunction(this), cancellationToken);

        }
        
        public Task<ActorTask<TResult>> ContinueWith<TResult>(Func<ActorTask, ActorTask<TResult>> continuationFunction)
        {

            return myTask.ContinueWith((t) => continuationFunction(this));

        }

        public Task<ActorTask<TResult>> ContinueWith<TResult>(Func<ActorTask, object, ActorTask<TResult>> continuationFunction, object state, TaskContinuationOptions continuationOptions)
        {

            return myTask.ContinueWith((t, s) => continuationFunction(this, s), state, continuationOptions);

        }

        public Task<ActorTask<TResult>> ContinueWith<TResult>(Func<ActorTask, object, ActorTask<TResult>> continuationFunction, object state)
        {

            return myTask.ContinueWith((t, s) => continuationFunction(this, s), state);

        }

        public Task<ActorTask<TResult>> ContinueWith<TResult>(Func<ActorTask, object, ActorTask<TResult>> continuationFunction, object state, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
        {

            return myTask.ContinueWith((t, s) => continuationFunction(this, s), state, cancellationToken, continuationOptions, scheduler);

        }

    }

    //With Result

    public sealed class ActorTask<TResult> : ActorTaskBase<Task<TResult>>, IActorTask<TResult>, IActorTask, IAsyncResult, IDisposable
    {

        public ActorTask(Task<TResult> task)
            : base(task)
        {
        }

        //Result

        public TResult Result
        {

            get
            {

                return myTask.Result;

            }

        }

        public new TaskAwaiter<TResult> GetAwaiter()
        {

            return myTask.GetAwaiter();

        }

        //ActorTask Continuations

        public Task<ActorTask> ContinueWith(Func<ActorTask<TResult>, object, ActorTask> continuationFunction, object state)
        {

            return myTask.ContinueWith((t, s) => continuationFunction(this, s), state);

        }

        public Task<ActorTask> ContinueWith(Func<ActorTask<TResult>, object, ActorTask> continuationFunction, object state, CancellationToken cancellationToken)
        {

            return myTask.ContinueWith((t, s) => continuationFunction(this, s), state, cancellationToken);

        }

        public Task<ActorTask> ContinueWith(Func<ActorTask<TResult>, object, ActorTask> continuationFunction, object state, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
        {

            return myTask.ContinueWith((t, s) => continuationFunction(this, s), state, cancellationToken, continuationOptions, scheduler);

        }

        public Task<ActorTask> ContinueWith(Func<ActorTask<TResult>, object, ActorTask> continuationFunction, object state, TaskContinuationOptions continuationOptions)
        {

            return myTask.ContinueWith((t, s) => continuationFunction(this, s), state, continuationOptions);

        }

        public Task<ActorTask> ContinueWith(Func<ActorTask<TResult>, object, ActorTask> continuationFunction, object state, TaskScheduler scheduler)
        {

            return myTask.ContinueWith((t, s) => continuationFunction(this, s), state, scheduler);

        }

        public Task<ActorTask> ContinueWith(Func<ActorTask<TResult>, ActorTask> continuationFunction)
        {

            return myTask.ContinueWith((t) => continuationFunction(this));

        }

        public Task<ActorTask> ContinueWith(Func<ActorTask<TResult>, ActorTask> continuationFunction, CancellationToken cancellationToken)
        {

            return myTask.ContinueWith((t) => continuationFunction(this));

        }

        public Task<ActorTask> ContinueWith(Func<ActorTask<TResult>, ActorTask> continuationFunction, TaskContinuationOptions continuationOptions)
        {

            return myTask.ContinueWith((t) => continuationFunction(this), continuationOptions);

        }

        public Task<ActorTask> ContinueWith(Func<ActorTask<TResult>, ActorTask> continuationFunction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
        {

            return myTask.ContinueWith((t) => continuationFunction(this), cancellationToken, continuationOptions, scheduler);

        }

        public Task<ActorTask<TNewResult>> ContinueWith<TNewResult>(Func<ActorTask<TResult>, object, ActorTask<TNewResult>> continuationFunction, object state, CancellationToken cancellationToken)
        {

            return myTask.ContinueWith((t, s) => continuationFunction(this, s), state, cancellationToken);

        }

        public Task<ActorTask> ContinueWith(Func<ActorTask<TResult>, ActorTask> continuationFunction, TaskScheduler scheduler)
        {

            return myTask.ContinueWith((t) => continuationFunction(this), scheduler);

        }

        public Task<ActorTask<TNewResult>> ContinueWith<TNewResult>(Func<ActorTask<TResult>, ActorTask<TNewResult>> continuationFunction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
        {

            return myTask.ContinueWith((t) => continuationFunction(this), cancellationToken, continuationOptions, scheduler);

        }

        public Task<ActorTask<TNewResult>> ContinueWith<TNewResult>(Func<ActorTask<TResult>, ActorTask<TNewResult>> continuationFunction, TaskContinuationOptions continuationOptions)
        {

            return myTask.ContinueWith((t) => continuationFunction(this), continuationOptions);

        }

        public Task<ActorTask<TNewResult>> ContinueWith<TNewResult>(Func<ActorTask<TResult>, ActorTask<TNewResult>> continuationFunction, TaskScheduler scheduler)
        {

            return myTask.ContinueWith((t) => continuationFunction(this), scheduler);

        }

        public Task<ActorTask<TNewResult>> ContinueWith<TNewResult>(Func<ActorTask<TResult>, object, ActorTask<TNewResult>> continuationFunction, object state, TaskScheduler scheduler)
        {

            return myTask.ContinueWith((t, s) => continuationFunction(this, s), state, scheduler);

        }

        public Task<ActorTask<TNewResult>> ContinueWith<TNewResult>(Func<ActorTask<TResult>, ActorTask<TNewResult>> continuationFunction, CancellationToken cancellationToken)
        {

            return myTask.ContinueWith((t) => continuationFunction(this), cancellationToken);

        }

        public Task<ActorTask<TNewResult>> ContinueWith<TNewResult>(Func<ActorTask<TResult>, ActorTask<TNewResult>> continuationFunction)
        {

            return myTask.ContinueWith((t) => continuationFunction(this));

        }

        public Task<ActorTask<TNewResult>> ContinueWith<TNewResult>(Func<ActorTask<TResult>, object, ActorTask<TNewResult>> continuationFunction, object state, TaskContinuationOptions continuationOptions)
        {

            return myTask.ContinueWith((t, s) => continuationFunction(this, s), state, continuationOptions);

        }

        public Task<ActorTask<TNewResult>> ContinueWith<TNewResult>(Func<ActorTask<TResult>, object, ActorTask<TNewResult>> continuationFunction, object state)
        {

            return myTask.ContinueWith((t, s) => continuationFunction(this, s), state);

        }

        public Task<ActorTask<TNewResult>> ContinueWith<TNewResult>(Func<ActorTask<TResult>, object, ActorTask<TNewResult>> continuationFunction, object state, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
        {

            return myTask.ContinueWith((t, s) => continuationFunction(this, s), state, cancellationToken, continuationOptions, scheduler);

        }

        //
        
        public Task<ActorTask<TResult>> ContinueWith(Func<ActorTask<TResult>, ActorTask<TResult>> continuationFunction)
        {

            return myTask.ContinueWith((t) => continuationFunction(this));

        }

        public Task<ActorTask> ContinueWithNoResult(Func<ActorTask<TResult>, ActorTask> continuationFunction)
        {

            return myTask.ContinueWith((t) => continuationFunction(this));

        }

    }

}
