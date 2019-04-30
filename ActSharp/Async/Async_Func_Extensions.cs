using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ActSharp.Async
{

    //Funcs

    public static class Async_Func_Extensions
    {

        public static Task<TResult> Async<TResult>(this Func<TResult> func)
        {

            return Task.Factory.StartNew(func);

        }

        //1 Parameter

        public static Task<TResult> Async<T, TResult>(this Func<T, TResult> func, T p)
        {

            Func<TResult> fun = () => { return func(p); };

            return Task.Factory.StartNew(fun);

        }

        public static Task<TResult> Async<T, TResult>(this Func<T, TResult> func, T p, TaskCreationOptions creationOptions)
        {

            Func<TResult> fun = () => { return func(p); };

            return Task.Factory.StartNew(fun, creationOptions);

        }

        public static Task<TResult> Async<T, TResult>(this Func<T, TResult> func, T p, CancellationToken cancellationToken)
        {

            Func<TResult> fun = () => { return func(p); };

            return Task.Factory.StartNew(fun, cancellationToken);

        }

        public static Task<TResult> Async<T, TResult>(this Func<T, TResult> func, T p, CancellationToken cancellationToken, TaskCreationOptions creationOptions, TaskScheduler scheduler)
        {

            Func<TResult> fun = () => { return func(p); };

            return Task.Factory.StartNew(fun, cancellationToken, creationOptions, scheduler);

        }

        //2 Parameters

        public static Task<TResult> Async<T1, T2, TResult>(this Func<T1, T2, TResult> func, T1 p1, T2 p2)
        {

            Func<TResult> fun = () => { return func(p1, p2); };

            return Task.Factory.StartNew(fun);

        }

        public static Task<TResult> Async<T1, T2, TResult>(this Func<T1, T2, TResult> func, T1 p1, T2 p2, TaskCreationOptions creationOptions)
        {

            Func<TResult> fun = () => { return func(p1, p2); };

            return Task.Factory.StartNew(fun, creationOptions);

        }

        public static Task<TResult> Async<T1, T2, TResult>(this Func<T1, T2, TResult> func, T1 p1, T2 p2, CancellationToken cancellationToken)
        {

            Func<TResult> fun = () => { return func(p1, p2); };

            return Task.Factory.StartNew(fun, cancellationToken);

        }

        public static Task<TResult> Async<T1, T2, TResult>(this Func<T1, T2, TResult> func, T1 p1, T2 p2, CancellationToken cancellationToken, TaskCreationOptions creationOptions, TaskScheduler scheduler)
        {

            Func<TResult> fun = () => { return func(p1, p2); };

            return Task.Factory.StartNew(fun, cancellationToken, creationOptions, scheduler);

        }

        //3 Parameters

        public static Task<TResult> Async<T1, T2, T3, TResult>(this Func<T1, T2, T3, TResult> func, T1 p1, T2 p2, T3 p3)
        {

            Func<TResult> fun = () => { return func(p1, p2, p3); };

            return Task.Factory.StartNew(fun);

        }

        public static Task<TResult> Async<T1, T2, T3, TResult>(this Func<T1, T2, T3, TResult> func, T1 p1, T2 p2, T3 p3, TaskCreationOptions creationOptions)
        {

            Func<TResult> fun = () => { return func(p1, p2, p3); };

            return Task.Factory.StartNew(fun, creationOptions);

        }

        public static Task<TResult> Async<T1, T2, T3, TResult>(this Func<T1, T2, T3, TResult> func, T1 p1, T2 p2, T3 p3, CancellationToken cancellationToken)
        {

            Func<TResult> fun = () => { return func(p1, p2, p3); };

            return Task.Factory.StartNew(fun, cancellationToken);

        }

        public static Task<TResult> Async<T1, T2, T3, TResult>(this Func<T1, T2, T3, TResult> func, T1 p1, T2 p2, T3 p3, CancellationToken cancellationToken, TaskCreationOptions creationOptions, TaskScheduler scheduler)
        {

            Func<TResult> fun = () => { return func(p1, p2, p3); };

            return Task.Factory.StartNew(fun, cancellationToken, creationOptions, scheduler);

        }

        //4 Parameters

        public static Task<TResult> Async<T1, T2, T3, T4, TResult>(this Func<T1, T2, T3, T4, TResult> func, T1 p1, T2 p2, T3 p3, T4 p4)
        {

            Func<TResult> fun = () => { return func(p1, p2, p3, p4); };

            return Task.Factory.StartNew(fun);

        }

        public static Task<TResult> Async<T1, T2, T3, T4, TResult>(this Func<T1, T2, T3, T4, TResult> func, T1 p1, T2 p2, T3 p3, T4 p4, TaskCreationOptions creationOptions)
        {

            Func<TResult> fun = () => { return func(p1, p2, p3, p4); };

            return Task.Factory.StartNew(fun, creationOptions);

        }

        public static Task<TResult> Async<T1, T2, T3, T4, TResult>(this Func<T1, T2, T3, T4, TResult> func, T1 p1, T2 p2, T3 p3, T4 p4, CancellationToken cancellationToken)
        {

            Func<TResult> fun = () => { return func(p1, p2, p3, p4); };

            return Task.Factory.StartNew(fun, cancellationToken);

        }

        public static Task<TResult> Async<T1, T2, T3, T4, TResult>(this Func<T1, T2, T3, T4, TResult> func, T1 p1, T2 p2, T3 p3, T4 p4, CancellationToken cancellationToken, TaskCreationOptions creationOptions, TaskScheduler scheduler)
        {

            Func<TResult> fun = () => { return func(p1, p2, p3, p4); };

            return Task.Factory.StartNew(fun, cancellationToken, creationOptions, scheduler);

        }

    }

}
