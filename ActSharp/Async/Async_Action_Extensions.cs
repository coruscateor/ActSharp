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

        //Returning void

        public static void AsyncIgnore(this Action action)
        {

            ThreadPool.QueueUserWorkItem((obj) =>
            {

                try
                {

                    action();

                }
                catch
                {
                }

            }, null);

        }

        public static void AsyncFailFast(this Action action)
        {

            ThreadPool.QueueUserWorkItem((obj) => {

                try
                {

                    action();

                }
                catch(Exception e)
                {

                    Environment.FailFast("Un-caught ActSharp.Async.Async_Action_Extensions.AsyncFailFast Exception", e);

                }

            }, null);

        }

        public static void Async(this Action action, Action<Exception> exceptionAction)
        {

            ThreadPool.QueueUserWorkItem((obj) =>
            {

                try
                {

                    action();

                }
                catch(Exception e)
                {

                    exceptionAction(e);

                }

            }, null);

        }

        //Returning void UnSafe

        public static void UnSafeAsyncIgnore(this Action action)
        {

            ThreadPool.UnsafeQueueUserWorkItem((obj) =>
            {

                try
                {

                    action();

                }
                catch
                {
                }

            }, null);

        }

        public static void UnSafeAsyncFailFast(this Action action)
        {

            ThreadPool.UnsafeQueueUserWorkItem((obj) => {

                try
                {

                    action();

                }
                catch (Exception e)
                {

                    Environment.FailFast("Un-caught ActSharp.Async.Async_Action_Extensions.UnSafeAsyncFailFast Exception", e);

                }

            }, null);

        }

        public static void UnSafeAsync(this Action action, Action<Exception> exceptionAction)
        {

            ThreadPool.UnsafeQueueUserWorkItem((obj) =>
            {

                try
                {

                    action();

                }
                catch (Exception e)
                {

                    exceptionAction(e);

                }

            }, null);

        }

        //Waiting

        //int

        public static void AsyncWaitIgnore(this Action action, WaitHandle waitObject, int millisecondsTimeOutInterval, bool executeOnlyOnce = true)
        {

            ThreadPool.RegisterWaitForSingleObject(waitObject, (state, timedOut) =>
            {

                try
                {

                    if (timedOut)
                        action();

                }
                catch
                {
                }

            }, null, millisecondsTimeOutInterval, executeOnlyOnce);

        }

        public static void AsyncWaitFailFast(this Action action, WaitHandle waitObject, int millisecondsTimeOutInterval, bool executeOnlyOnce = true)
        {

            ThreadPool.RegisterWaitForSingleObject(waitObject, (state, timedOut) =>
            {

                try
                {

                    if (timedOut)
                        action();

                }
                catch (Exception e)
                {

                    Environment.FailFast("Un-caught ActSharp.Async.Async_Action_Extensions.AsyncWaitFailFast Exception", e);

                }

            }, null, millisecondsTimeOutInterval, executeOnlyOnce);

        }

        public static void AsyncWait(this Action action, WaitHandle waitObject, int millisecondsTimeOutInterval, Action<Exception> exceptionAction, bool executeOnlyOnce = true)
        {

            ThreadPool.RegisterWaitForSingleObject(waitObject, (state, timedOut) =>
            {

                try
                {

                    if (timedOut)
                        action();

                }
                catch (Exception e)
                {

                    exceptionAction(e);

                }

            }, null, millisecondsTimeOutInterval, executeOnlyOnce);

        }

        //uint

        public static void AsyncWaitIgnore(this Action action, WaitHandle waitObject, uint millisecondsTimeOutInterval, bool executeOnlyOnce = true)
        {

            ThreadPool.RegisterWaitForSingleObject(waitObject, (state, timedOut) =>
            {

                try
                {

                    if(timedOut)
                        action();

                }
                catch
                {
                }

            }, null, millisecondsTimeOutInterval, executeOnlyOnce);

        }

        public static void AsyncWaitFailFast(this Action action, WaitHandle waitObject, uint millisecondsTimeOutInterval, bool executeOnlyOnce = true)
        {

            ThreadPool.RegisterWaitForSingleObject(waitObject, (state, timedOut) =>
            {

                try
                {

                    if (timedOut)
                        action();

                }
                catch (Exception e)
                {

                    Environment.FailFast("Un-caught ActSharp.Async.Async_Action_Extensions.AsyncWaitFailFast Exception", e);

                }

            }, null, millisecondsTimeOutInterval, executeOnlyOnce);

        }

        public static void AsyncWait(this Action action, WaitHandle waitObject, uint millisecondsTimeOutInterval, Action<Exception> exceptionAction, bool executeOnlyOnce = true)
        {

            ThreadPool.RegisterWaitForSingleObject(waitObject, (state, timedOut) =>
            {

                try
                {

                    if (timedOut)
                        action();

                }
                catch (Exception e)
                {

                    exceptionAction(e);

                }

            }, null, millisecondsTimeOutInterval, executeOnlyOnce);

        }

        //long

        public static void AsyncWaitIgnore(this Action action, WaitHandle waitObject, long millisecondsTimeOutInterval, bool executeOnlyOnce = true)
        {

            ThreadPool.RegisterWaitForSingleObject(waitObject, (state, timedOut) =>
            {

                try
                {

                    if (timedOut)
                        action();

                }
                catch
                {
                }

            }, null, millisecondsTimeOutInterval, executeOnlyOnce);

        }

        public static void AsyncWaitFailFast(this Action action, WaitHandle waitObject, long millisecondsTimeOutInterval, bool executeOnlyOnce = true)
        {

            ThreadPool.RegisterWaitForSingleObject(waitObject, (state, timedOut) =>
            {

                try
                {

                    if (timedOut)
                        action();

                }
                catch (Exception e)
                {

                    Environment.FailFast("Un-caught ActSharp.Async.Async_Action_Extensions.AsyncWaitFailFast Exception", e);

                }

            }, null, millisecondsTimeOutInterval, executeOnlyOnce);

        }

        public static void AsyncWait(this Action action, WaitHandle waitObject, long millisecondsTimeOutInterval, Action<Exception> exceptionAction, bool executeOnlyOnce = true)
        {

            ThreadPool.RegisterWaitForSingleObject(waitObject, (state, timedOut) =>
            {

                try
                {

                    if (timedOut)
                        action();

                }
                catch (Exception e)
                {

                    exceptionAction(e);

                }

            }, null, millisecondsTimeOutInterval, executeOnlyOnce);

        }

        //TimeSpan

        public static void AsyncWaitIgnore(this Action action, WaitHandle waitObject, TimeSpan timeout, bool executeOnlyOnce = true)
        {

            ThreadPool.RegisterWaitForSingleObject(waitObject, (state, timedOut) =>
            {

                try
                {

                    if (timedOut)
                        action();

                }
                catch
                {
                }

            }, null, timeout, executeOnlyOnce);

        }

        public static void AsyncWaitFailFast(this Action action, WaitHandle waitObject, TimeSpan timeout, bool executeOnlyOnce = true)
        {

            ThreadPool.RegisterWaitForSingleObject(waitObject, (state, timedOut) =>
            {

                try
                {

                    if (timedOut)
                        action();

                }
                catch (Exception e)
                {

                    Environment.FailFast("Un-caught ActSharp.Async.Async_Action_Extensions.AsyncWaitFailFast Exception", e);

                }

            }, null, timeout, executeOnlyOnce);

        }

        public static void AsyncWait(this Action action, WaitHandle waitObject, TimeSpan timeout, Action<Exception> exceptionAction, bool executeOnlyOnce = true)
        {

            ThreadPool.RegisterWaitForSingleObject(waitObject, (state, timedOut) =>
            {

                try
                {

                    if (timedOut)
                        action();

                }
                catch (Exception e)
                {

                    exceptionAction(e);

                }

            }, null, timeout, executeOnlyOnce);

        }

        //Waiting - Unsafe

        //int

        public static void UnsafeAsyncWaitIgnore(this Action action, WaitHandle waitObject, int millisecondsTimeOutInterval, bool executeOnlyOnce = true)
        {

            ThreadPool.UnsafeRegisterWaitForSingleObject(waitObject, (state, timedOut) =>
            {

                try
                {

                    if (timedOut)
                        action();

                }
                catch
                {
                }

            }, null, millisecondsTimeOutInterval, executeOnlyOnce);

        }

        public static void UnsafeAsyncWaitFailFast(this Action action, WaitHandle waitObject, int millisecondsTimeOutInterval, bool executeOnlyOnce = true)
        {

            ThreadPool.UnsafeRegisterWaitForSingleObject(waitObject, (state, timedOut) =>
            {

                try
                {

                    if (timedOut)
                        action();

                }
                catch (Exception e)
                {

                    Environment.FailFast("Un-caught ActSharp.Async.Async_Action_Extensions.UnsafeAsyncWaitFailFast Exception", e);

                }

            }, null, millisecondsTimeOutInterval, executeOnlyOnce);

        }

        public static void UnsafeAsyncWait(this Action action, WaitHandle waitObject, int millisecondsTimeOutInterval, Action<Exception> exceptionAction, bool executeOnlyOnce = true)
        {

            ThreadPool.UnsafeRegisterWaitForSingleObject(waitObject, (state, timedOut) =>
            {

                try
                {

                    if (timedOut)
                        action();

                }
                catch (Exception e)
                {

                    exceptionAction(e);

                }

            }, null, millisecondsTimeOutInterval, executeOnlyOnce);

        }

        //uint

        public static void UnsafeAsyncWaitIgnore(this Action action, WaitHandle waitObject, uint millisecondsTimeOutInterval, bool executeOnlyOnce = true)
        {

            ThreadPool.UnsafeRegisterWaitForSingleObject(waitObject, (state, timedOut) =>
            {

                try
                {

                    if (timedOut)
                        action();

                }
                catch
                {
                }

            }, null, millisecondsTimeOutInterval, executeOnlyOnce);

        }

        public static void UnsafeAsyncWaitFailFast(this Action action, WaitHandle waitObject, uint millisecondsTimeOutInterval, bool executeOnlyOnce = true)
        {

            ThreadPool.UnsafeRegisterWaitForSingleObject(waitObject, (state, timedOut) =>
            {

                try
                {

                    if (timedOut)
                        action();

                }
                catch (Exception e)
                {

                    Environment.FailFast("Un-caught ActSharp.Async.Async_Action_Extensions.UnsafeAsyncWaitFailFast Exception", e);

                }

            }, null, millisecondsTimeOutInterval, executeOnlyOnce);

        }

        public static void UnsafeAsyncWait(this Action action, WaitHandle waitObject, uint millisecondsTimeOutInterval, Action<Exception> exceptionAction, bool executeOnlyOnce = true)
        {

            ThreadPool.UnsafeRegisterWaitForSingleObject(waitObject, (state, timedOut) =>
            {

                try
                {

                    if (timedOut)
                        action();

                }
                catch (Exception e)
                {

                    exceptionAction(e);

                }

            }, null, millisecondsTimeOutInterval, executeOnlyOnce);

        }

        //long

        public static void UnsafeAsyncWaitIgnore(this Action action, WaitHandle waitObject, long millisecondsTimeOutInterval, bool executeOnlyOnce = true)
        {

            ThreadPool.UnsafeRegisterWaitForSingleObject(waitObject, (state, timedOut) =>
            {

                try
                {

                    if (timedOut)
                        action();

                }
                catch
                {
                }

            }, null, millisecondsTimeOutInterval, executeOnlyOnce);

        }

        public static void UnsafeAsyncWaitFailFast(this Action action, WaitHandle waitObject, long millisecondsTimeOutInterval, bool executeOnlyOnce = true)
        {

            ThreadPool.UnsafeRegisterWaitForSingleObject(waitObject, (state, timedOut) =>
            {

                try
                {

                    if (timedOut)
                        action();

                }
                catch (Exception e)
                {

                    Environment.FailFast("Un-caught ActSharp.Async.Async_Action_Extensions.UnsafeAsyncWaitFailFast Exception", e);

                }

            }, null, millisecondsTimeOutInterval, executeOnlyOnce);

        }

        public static void UnsafeAsyncWait(this Action action, WaitHandle waitObject, long millisecondsTimeOutInterval, Action<Exception> exceptionAction, bool executeOnlyOnce = true)
        {

            ThreadPool.UnsafeRegisterWaitForSingleObject(waitObject, (state, timedOut) =>
            {

                try
                {

                    if (timedOut)
                        action();

                }
                catch (Exception e)
                {

                    exceptionAction(e);

                }

            }, null, millisecondsTimeOutInterval, executeOnlyOnce);

        }

        //TimeSpan

        public static void UnsafeAsyncWaitIgnore(this Action action, WaitHandle waitObject, TimeSpan timeout, bool executeOnlyOnce = true)
        {

            ThreadPool.UnsafeRegisterWaitForSingleObject(waitObject, (state, timedOut) =>
            {

                try
                {

                    if (timedOut)
                        action();

                }
                catch
                {
                }

            }, null, timeout, executeOnlyOnce);

        }

        public static void UnsafeAsyncWaitFailFast(this Action action, WaitHandle waitObject, TimeSpan timeout, bool executeOnlyOnce = true)
        {

            ThreadPool.UnsafeRegisterWaitForSingleObject(waitObject, (state, timedOut) =>
            {

                try
                {

                    if (timedOut)
                        action();

                }
                catch (Exception e)
                {

                    Environment.FailFast("Un-caught ActSharp.Async.Async_Action_Extensions.UnsafeAsyncWaitFailFast Exception", e);

                }

            }, null, timeout, executeOnlyOnce);

        }

        public static void UnsafeAsyncWait(this Action action, WaitHandle waitObject, TimeSpan timeout, Action<Exception> exceptionAction, bool executeOnlyOnce = true)
        {

            ThreadPool.UnsafeRegisterWaitForSingleObject(waitObject, (state, timedOut) =>
            {

                try
                {

                    if (timedOut)
                        action();

                }
                catch (Exception e)
                {

                    exceptionAction(e);

                }

            }, null, timeout, executeOnlyOnce);

        }

    }

}
