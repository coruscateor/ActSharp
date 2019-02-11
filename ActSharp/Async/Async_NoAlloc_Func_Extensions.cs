using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ActSharp.Parameters;

namespace ActSharp.Async
{

    public static class Async_NoAlloc_Func_Extensions
    {

        //, CancellationToken cancellationToken

        public static Task<TResult> AsyncNoAlloc<T, TResult>(this Func<object, TResult> func, T state)
            where T : class
        {

            return Task.Factory.StartNew(func, state);

        }

        public static Task<TResult> AsyncNoAlloc<T, TResult>(this Func<object, TResult> func, T state, TaskCreationOptions creationOptions)
            where T : class
        {

            return Task.Factory.StartNew(func, state, creationOptions);

        }

        public static Task<TResult> AsyncNoAlloc<T, TResult>(this Func<object, TResult> func, T state, CancellationToken cancellationToken, TaskCreationOptions creationOptions, TaskScheduler scheduler)
            where T : class
        {

            return Task.Factory.StartNew(func, state, cancellationToken, creationOptions, scheduler);

        }

        public static Task<TResult> AsyncNoAlloc<T, TResult>(this Func<object, TResult> func, T state, CancellationToken cancellationToken)
            where T : class
        {

            return Task.Factory.StartNew(func, state, cancellationToken);

        }

        //

        public static Task<TResult> AsyncNoAlloc<T, TResult>(this Func<object, TResult> func, Param<T> state)
        {

            return Task.Factory.StartNew(func, state);

        }

        public static Task<TResult> AsyncNoAlloc<T, TResult>(this Func<object, TResult> func, Param<T> state, TaskCreationOptions creationOptions)
        {

            return Task.Factory.StartNew(func, state, creationOptions);

        }

        public static Task<TResult> AsyncNoAlloc<T, TResult>(this Func<object, TResult> func, Param<T> state, CancellationToken cancellationToken, TaskCreationOptions creationOptions, TaskScheduler scheduler)
        {

            return Task.Factory.StartNew(func, state, cancellationToken, creationOptions, scheduler);

        }

        public static Task<TResult> AsyncNoAlloc<T, TResult>(this Func<object, TResult> func, Param<T> state, CancellationToken cancellationToken)
        {

            return Task.Factory.StartNew(func, state, cancellationToken);

        }

        //

        public static Task<TResult> AsyncNoAlloc<T1, T2, TResult>(this Func<object, TResult> func, Params<T1, T2> state)
        {

            return Task.Factory.StartNew(func, state);

        }

        public static Task<TResult> AsyncNoAlloc<T1, T2, TResult>(this Func<object, TResult> func, Params<T1, T2> state, TaskCreationOptions creationOptions)
        {

            return Task.Factory.StartNew(func, state, creationOptions);

        }

        public static Task<TResult> AsyncNoAlloc<T1, T2, TResult>(this Func<object, TResult> func, Params<T1, T2> state, CancellationToken cancellationToken, TaskCreationOptions creationOptions, TaskScheduler scheduler)
        {

            return Task.Factory.StartNew(func, state, cancellationToken, creationOptions, scheduler);

        }

        public static Task<TResult> AsyncNoAlloc<T1, T2, TResult>(this Func<object, TResult> func, Params<T1, T2> state, CancellationToken cancellationToken)
        {

            return Task.Factory.StartNew(func, state, cancellationToken);

        }

        //

        public static Task<TResult> AsyncNoAlloc<T1, T2, T3, TResult>(this Func<object, TResult> func, Params<T1, T2, T3> state)
        {

            return Task.Factory.StartNew(func, state);

        }

        public static Task<TResult> AsyncNoAlloc<T1, T2, T3, TResult>(this Func<object, TResult> func, Params<T1, T2, T3> state, TaskCreationOptions creationOptions)
        {

            return Task.Factory.StartNew(func, state, creationOptions);

        }

        public static Task<TResult> AsyncNoAlloc<T1, T2, T3, TResult>(this Func<object, TResult> func, Params<T1, T2, T3> state, CancellationToken cancellationToken, TaskCreationOptions creationOptions, TaskScheduler scheduler)
        {

            return Task.Factory.StartNew(func, state, cancellationToken, creationOptions, scheduler);

        }

        public static Task<TResult> AsyncNoAlloc<T1, T2, T3, TResult>(this Func<object, TResult> func, Params<T1, T2, T3> state, CancellationToken cancellationToken)
        {

            return Task.Factory.StartNew(func, state, cancellationToken);

        }

        //

        public static Task<TResult> AsyncNoAlloc<T1, T2, T3, T4, TResult>(this Func<object, TResult> func, Params<T1, T2, T3, T4> state)
        {

            return Task.Factory.StartNew(func, state);

        }

        public static Task<TResult> AsyncNoAlloc<T1, T2, T3, T4, TResult>(this Func<object, TResult> func, Params<T1, T2, T3, T4> state, TaskCreationOptions creationOptions)
        {

            return Task.Factory.StartNew(func, state, creationOptions);

        }

        public static Task<TResult> AsyncNoAlloc<T1, T2, T3, T4, TResult>(this Func<object, TResult> func, Params<T1, T2, T3, T4> state, CancellationToken cancellationToken, TaskCreationOptions creationOptions, TaskScheduler scheduler)
        {

            return Task.Factory.StartNew(func, state, cancellationToken, creationOptions, scheduler);

        }

        public static Task<TResult> AsyncNoAlloc<T1, T2, T3, T4, TResult>(this Func<object, TResult> func, Params<T1, T2, T3, T4> state, CancellationToken cancellationToken)
        {

            return Task.Factory.StartNew(func, state, cancellationToken);

        }

        //

    }

}
