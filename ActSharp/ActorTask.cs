using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ActSharp
{

    //abstract protected 

    public class ActorTask : IActorTask, IAsyncResult, IDisposable
    {

        Task myTask;

        //IActor myActor;

        public ActorTask(Task task) //, IActor actor)
        {

            myTask = task;

            //myActor = actor;

        }

        //IActor Actor
        //{

        //    get
        //    {

        //        return myActor;

        //    }

        //}

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

        public WaitHandle AsyncWaitHandle => ((IAsyncResult)myTask).AsyncWaitHandle;

        public bool CompletedSynchronously => ((IAsyncResult)myTask).CompletedSynchronously;

        public void Dispose()
        {

            myTask.Dispose();

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

    //public class ActorTask<TActor> : ActorTask, IActorTask, IDisposable
    //     where TActor : IActor
    //{

    //    TActor myActor;

    //    public ActorTask(Task task, TActor actor)
    //    {

    //        myTask = task;

    //        myActor = actor;

    //    }

    //    TActor Actor
    //    {

    //        get
    //        {

    //            return myActor;

    //        }

    //    }

    //}

    //With Result

    public class ActorTask<TResult> : IActorTask<TResult>, IActorTask, IAsyncResult, IDisposable
    {

        protected Task<TResult> myTask;

        //IActor myActor;

        public ActorTask(Task<TResult> task) //, IActor actor)
        {

            myTask = task;

            //myActor = actor;

        }

        //IActor Actor
        //{

        //    get
        //    {

        //        return myActor;

        //    }

        //}

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

        public void Dispose()
        {

            myTask.Dispose();

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

        //Result

        public TResult Result
        {

            get
            {

                return myTask.Result;

            }

        }

        public WaitHandle AsyncWaitHandle => ((IAsyncResult)myTask).AsyncWaitHandle;

        public bool CompletedSynchronously => ((IAsyncResult)myTask).CompletedSynchronously;
    }

}
