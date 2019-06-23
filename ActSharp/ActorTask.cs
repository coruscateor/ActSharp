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

        public ActorTask(Task task, IActor actor)
            : base(task, actor)
        {
        }

    }

    //With Result

    public sealed class ActorTask<TResult> : ActorTaskBase<Task<TResult>>, IActorTask<TResult>, IActorTask, IAsyncResult, IDisposable
    {

        public ActorTask(Task<TResult> task, IActor actor)
            : base(task, actor)
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

    }

}
