using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ActSharp.Parameters;

namespace ActSharp.Async
{

    public static class Async_NoAlloc_Action_Extensions
    {

        public static Task AsyncNoAlloc<T>(this Action<object> action, T state)
            where T : class
        {

            return Task.Factory.StartNew(action, state);

        }

        public static Task AsyncNoAlloc<T>(this Action<object> action, T state, TaskCreationOptions creationOptions)
            where T : class
        {

            return Task.Factory.StartNew(action, state, creationOptions);

        }

        public static Task AsyncNoAlloc<T>(this Action<object> action, T state, CancellationToken cancellationToken, TaskCreationOptions creationOptions, TaskScheduler scheduler)
            where T : class
        {

            return Task.Factory.StartNew(action, state, cancellationToken, creationOptions, scheduler);

        }

        public static Task AsyncNoAlloc<T>(this Action<object> action, T state, CancellationToken cancellationToken)
            where T : class
        {

            return Task.Factory.StartNew(action, state, cancellationToken);

        }

        //

        public static Task AsyncNoAlloc<T>(this Action<object> action, Param<T> state)
        {

            return Task.Factory.StartNew(action, state);

        }

        public static Task AsyncNoAlloc<T>(this Action<object> action, Param<T> state, TaskCreationOptions creationOptions)
        {

            return Task.Factory.StartNew(action, state, creationOptions);

        }

        public static Task AsyncNoAlloc<T>(this Action<object> action, Param<T> state, CancellationToken cancellationToken, TaskCreationOptions creationOptions, TaskScheduler scheduler)
        {

            return Task.Factory.StartNew(action, state, cancellationToken, creationOptions, scheduler);

        }

        public static Task AsyncNoAlloc<T>(this Action<object> action, Param<T> state, CancellationToken cancellationToken)
        {

            return Task.Factory.StartNew(action, state, cancellationToken);

        }

        //

        public static Task AsyncNoAlloc<T1, T2>(this Action<object> action, Params<T1, T2> state)
        {

            return Task.Factory.StartNew(action, state);

        }

        public static Task AsyncNoAlloc<T1, T2>(this Action<object> action, Params<T1, T2> state, TaskCreationOptions creationOptions)
        {

            return Task.Factory.StartNew(action, state, creationOptions);

        }

        public static Task AsyncNoAlloc<T1, T2>(this Action<object> action, Params<T1, T2> state, CancellationToken cancellationToken, TaskCreationOptions creationOptions, TaskScheduler scheduler)
        {

            return Task.Factory.StartNew(action, state, cancellationToken, creationOptions, scheduler);

        }

        public static Task AsyncNoAlloc<T1, T2>(this Action<object> action, Params<T1, T2> state, CancellationToken cancellationToken)
        {

            return Task.Factory.StartNew(action, state, cancellationToken);

        }

        //

        public static Task AsyncNoAlloc<T1, T2, T3>(this Action<object> action, Params<T1, T2, T3> state)
        {

            return Task.Factory.StartNew(action, state);

        }

        public static Task AsyncNoAlloc<T1, T2, T3>(this Action<object> action, Params<T1, T2, T3> state, TaskCreationOptions creationOptions)
        {

            return Task.Factory.StartNew(action, state, creationOptions);

        }

        public static Task AsyncNoAlloc<T1, T2, T3>(this Action<object> action, Params<T1, T2, T3> state, CancellationToken cancellationToken, TaskCreationOptions creationOptions, TaskScheduler scheduler)
        {

            return Task.Factory.StartNew(action, state, cancellationToken, creationOptions, scheduler);

        }

        public static Task AsyncNoAlloc<T1, T2, T3>(this Action<object> action, Params<T1, T2, T3> state, CancellationToken cancellationToken)
        {

            return Task.Factory.StartNew(action, state, cancellationToken);

        }

        //

        public static Task AsyncNoAlloc<T1, T2, T3, T4>(this Action<object> action, Params<T1, T2, T3, T4> state)
        {

            return Task.Factory.StartNew(action, state);

        }

        public static Task AsyncNoAlloc<T1, T2, T3, T4>(this Action<object> action, Params<T1, T2, T3, T4> state, TaskCreationOptions creationOptions)
        {

            return Task.Factory.StartNew(action, state, creationOptions);

        }

        public static Task AsyncNoAlloc<T1, T2, T3, T4>(this Action<object> action, Params<T1, T2, T3, T4> state, CancellationToken cancellationToken, TaskCreationOptions creationOptions, TaskScheduler scheduler)
        {

            return Task.Factory.StartNew(action, state, cancellationToken, creationOptions, scheduler);

        }

        public static Task AsyncNoAlloc<T1, T2, T3, T4>(this Action<object> action, Params<T1, T2, T3, T4> state, CancellationToken cancellationToken)
        {

            return Task.Factory.StartNew(action, state, cancellationToken);

        }

    }

}
