using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ActSharp
{

    /// <summary>
    /// A continuation oriented actor
    /// </summary>
    public abstract class CActor : ActorBase, IActor, IDisposable
    {

        WeakReference<Task> myCurrentTask;

        uint myActorActiveTaskCount;

        public CActor()
        {

            myCurrentTask = new WeakReference<Task>(null);

        }

        public sealed override int ActorExecuteQueueCount
        {

            get
            {

                return -1;

            }

        }

        public sealed override bool ActorExecuteQueueIsEmpty
        {

            get
            {

                return true;

            }

        }

        public uint ActorActiveTaskCount
        {

            get
            {

                bool taken = false;

                myStateLock.Enter(ref taken);

                try
                {

                    return myActorActiveTaskCount;

                }
                finally
                {

                    if (taken)
                        myStateLock.Exit();

                }

            }

        }

        [DoNotCall]
        //protected sealed override bool __ActorTrySetIsActive()
        new void __ActorTrySetIsActive()
        {

            int threadId = Thread.CurrentThread.ManagedThreadId;

            bool taken = false;

            myStateLock.Enter(ref taken);

            try
            {

                myManagedThreadId = threadId;

                if (!myIsActive)
                    myIsActive = true;

                myActorActiveTaskCount++;

                ////Could be dangerous

                //myOnIdleEvent.Reset();

            }
            finally
            {

                if (taken)
                    myStateLock.Exit();

            }

            myOnIdleEvent.Reset();

        }

        //[DoNotCall]
        //protected sealed override void __ActorTrySetInActive()
        void __ActorTrySetInActive()
        {

            uint currentActorActiveTaskCount;

            bool taken = false;

            myStateLock.Enter(ref taken);

            try
            {

                myActorActiveTaskCount--;

                currentActorActiveTaskCount = myActorActiveTaskCount;

                if (currentActorActiveTaskCount < 1u)
                {

                    myManagedThreadId = -1;

                    myIsActive = false;

                    ////Could be dangerous

                    //myOnIdleEvent.Set();

                }

            }
            finally
            {

                if (taken)
                    myStateLock.Exit();

            }

            if (currentActorActiveTaskCount < 1u)
                myOnIdleEvent.Set();

        }

        [DoNotCall]
        protected sealed override void __ActorNextTask()
        {

            throw new NotImplementedException();

        }

        [DoNotCall]
        protected sealed override void __ActorEnqueue(Task task)
        {

            throw new NotImplementedException();

        }

        Task SetupTask(Action a)
        {

            Task t;

            lock (myOnIdleEvent)
            {

                myCurrentTask.TryGetTarget(out t);

                if (t == null)
                    t = new Task(a);
                else
                    t = t.ContinueWith((ct) => a());

                myCurrentTask.SetTarget(t);

            }

            return t;

        }

        Task<TResult> SetupTask<TResult>(Func<TResult> f)
        {

            Task<TResult> t;

            lock (myOnIdleEvent)
            {

                Task mct;

                myCurrentTask.TryGetTarget(out mct);

                if (mct == null)
                    t = new Task<TResult>(f);
                else
                    t = mct.ContinueWith((ct) => f());

                myCurrentTask.SetTarget(t);

            }

            return t;

        }

        //Actions

        protected new ActorTask ActorSetup(Action action)
        {

            void a()
            {

                __ActorTrySetIsActive();

                try
                {

                    ActorPreDelegate();

                    action();

                }
                finally
                {

                    __ActorTrySetInActive();

                }

            }

            ActorTask at = new ActorTask(SetupTask(a));

            return at;

        }

        protected new ActorTask ActorSetup(Action action, Action<Exception> exceptionAction)
        {

            void a()
            {

                __ActorTrySetIsActive();

                try
                {

                    ActorPreDelegate();

                    action();

                }
                catch (Exception e)
                {

                    exceptionAction(e);

                }
                finally
                {

                    __ActorTrySetInActive();

                }

            };

            ActorTask at = new ActorTask(SetupTask(a));

            return at;

        }

        //Ignoring the Exception

        protected new ActorTask ActorSetupIgnore(Action action)
        {

            void a()
            {

                __ActorTrySetIsActive();
                
                try
                {

                    ActorPreDelegate();

                    action();

                }
                finally
                {

                    __ActorTrySetInActive();

                }

            };

            ActorTask at = new ActorTask(SetupTask(a));

            return at;

        }

        //FailFast if Exception is thrown

        protected new ActorTask ActorSetupFailFast(Action action)
        {

            void a()
            {

                __ActorTrySetIsActive();;

                try
                {

                    ActorPreDelegate();

                    action();

                }
                catch (Exception e)
                {

                    Environment.FailFast("Un-caught ActSharp.Actor Exception", e);

                }
                finally
                {

                    __ActorTrySetInActive();

                }

            };

            ActorTask at = new ActorTask(SetupTask(a));

            return at;

        }

        //Funcs

        protected new ActorTask<TResult> ActorSetup<TResult>(Func<TResult> func)
        {

            TResult f()
            {

                __ActorTrySetIsActive();

                try
                {

                    ActorPreDelegate();

                    return func();

                }
                finally
                {

                    __ActorTrySetInActive();

                }

            };

            ActorTask<TResult> at = new ActorTask<TResult>(SetupTask(f));

            return at;

        }

        protected new ActorTask<TResult> ActorSetup<TResult>(Func<TResult> func, Action<Exception> exceptionAction)
        {

            TResult f()
            {

                __ActorTrySetIsActive();

                try
                {

                    ActorPreDelegate();

                    return func();

                }
                catch (Exception e)
                {

                    exceptionAction(e);

                    throw;

                }
                finally
                {

                    __ActorTrySetInActive();

                }

            };

            ActorTask<TResult> at = new ActorTask<TResult>(SetupTask(f));

            return at;

        }

        //ActorSetup with no OnPreDelegate

        protected new ActorTask ActorSetupNoPreDelegate(Action action)
        {

            void a()
            {

                __ActorTrySetIsActive();

                try
                {

                    action();

                }
                finally
                {

                    __ActorTrySetInActive();

                }

            };

            ActorTask at = new ActorTask(SetupTask(a));

            return at;

        }

        //FailFast if Exception is thrown

        protected new ActorTask ActorSetupFailFastNoPreDelegate(Action action)
        {

            void a()
            {

                __ActorTrySetIsActive();

                try
                {

                    action();

                }
                catch (Exception e)
                {

                    Environment.FailFast("Un-caught ActSharp.Actor Exception", e);

                }
                finally
                {

                    __ActorTrySetInActive();

                }

            };

            ActorTask at = new ActorTask(SetupTask(a));

            return at;

        }

        //Funcs

        protected new ActorTask<TResult> ActorSetupNoPreDelegate<TResult>(Func<TResult> func)
        {

            TResult f()
            {

                __ActorTrySetIsActive();

                try
                {

                    return func();

                }
                finally
                {

                    __ActorTrySetInActive();

                }

            };

            ActorTask<TResult> at = new ActorTask<TResult>(SetupTask(f));

            return at;

        }

    }

}
