using System;
using System.Threading;
using System.Threading.Tasks;

namespace ActSharp.Async
{

    //Actions

    public static class Async_Action_Extensions
    {

        public static Task Async(this Action action)
        {

            return Task.Factory.StartNew(action);

        }

        //1 Parameter

        public static Task Async<T>(this Action<T> action, T p)
        {

            Action act = () => { action(p); };

            return Task.Factory.StartNew(act);

        }

        public static Task Async<T>(this Action<T> action, T p, TaskCreationOptions creationOptions)
        {

            Action act = () => { action(p); };

            return Task.Factory.StartNew(act, creationOptions);

        }

        public static Task Async<T>(this Action<T> action, T p, CancellationToken cancellationToken)
        {

            Action act = () => { action(p); };

            return Task.Factory.StartNew(act, cancellationToken);

        }

        public static Task Async<T>(this Action<T> action, T p, CancellationToken cancellationToken, TaskCreationOptions creationOptions, TaskScheduler scheduler)
        {

            Action act = () => { action(p); };

            return Task.Factory.StartNew(act, cancellationToken, creationOptions, scheduler);

        }

        //2 Parameters

        public static Task Async<T1, T2>(this Action<T1, T2> action, T1 p1, T2 p2)
        {

            Action act = () => { action(p1, p2); };

            return Task.Factory.StartNew(act);

        }

        public static Task Async<T1, T2>(this Action<T1, T2> action, T1 p1, T2 p2, TaskCreationOptions creationOptions)
        {

            Action act = () => { action(p1, p2); };

            return Task.Factory.StartNew(act, creationOptions);

        }

        public static Task Async<T1, T2>(this Action<T1, T2> action, T1 p1, T2 p2, CancellationToken cancellationToken)
        {

            Action act = () => { action(p1, p2); };

            return Task.Factory.StartNew(act, cancellationToken);

        }

        public static Task Async<T1, T2>(this Action<T1, T2> action, T1 p1, T2 p2, CancellationToken cancellationToken, TaskCreationOptions creationOptions, TaskScheduler scheduler)
        {

            Action act = () => { action(p1, p2); };

            return Task.Factory.StartNew(act, cancellationToken, creationOptions, scheduler);

        }

        //3 Parameters

        public static Task Async<T1, T2, T3>(this Action<T1, T2, T3> action, T1 p1, T2 p2, T3 p3)
        {

            Action act = () => { action(p1, p2, p3); };

            return Task.Factory.StartNew(act);

        }

        public static Task Async<T1, T2, T3>(this Action<T1, T2, T3> action, T1 p1, T2 p2, T3 p3, TaskCreationOptions creationOptions)
        {

            Action act = () => { action(p1, p2, p3); };

            return Task.Factory.StartNew(act, creationOptions);

        }

        public static Task Async<T1, T2, T3>(this Action<T1, T2, T3> action, T1 p1, T2 p2, T3 p3, CancellationToken cancellationToken)
        {

            Action act = () => { action(p1, p2, p3); };

            return Task.Factory.StartNew(act, cancellationToken);

        }

        public static Task Async<T1, T2, T3>(this Action<T1, T2, T3> action, T1 p1, T2 p2, T3 p3, CancellationToken cancellationToken, TaskCreationOptions creationOptions, TaskScheduler scheduler)
        {

            Action act = () => { action(p1, p2, p3); };

            return Task.Factory.StartNew(act, cancellationToken, creationOptions, scheduler);

        }

        //4 Parameters

        public static Task Async<T1, T2, T3, T4>(this Action<T1, T2, T3, T4> action, T1 p1, T2 p2, T3 p3, T4 p4)
        {

            Action act = () => { action(p1, p2, p3, p4); };

            return Task.Factory.StartNew(act);

        }

        public static Task Async<T1, T2, T3, T4>(this Action<T1, T2, T3, T4> action, T1 p1, T2 p2, T3 p3, T4 p4, TaskCreationOptions creationOptions)
        {

            Action act = () => { action(p1, p2, p3, p4); };

            return Task.Factory.StartNew(act, creationOptions);

        }

        public static Task Async<T1, T2, T3, T4>(this Action<T1, T2, T3, T4> action, T1 p1, T2 p2, T3 p3, T4 p4, CancellationToken cancellationToken)
        {

            Action act = () => { action(p1, p2, p3, p4); };

            return Task.Factory.StartNew(act, cancellationToken);

        }

        public static Task Async<T1, T2, T3, T4>(this Action<T1, T2, T3, T4> action, T1 p1, T2 p2, T3 p3, T4 p4, CancellationToken cancellationToken, TaskCreationOptions creationOptions, TaskScheduler scheduler)
        {

            Action act = () => { action(p1, p2, p3, p4); };

            return Task.Factory.StartNew(act, cancellationToken, creationOptions, scheduler);

        }

    }

}
