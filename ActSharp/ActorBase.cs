using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Reflection;
using System.Diagnostics;
using System.Linq;

namespace ActSharp
{
    public abstract class ActorBase : IActor
    {

        //Task myCurrentTask;

        protected ManualResetEventSlim myOnIdleEvent = new ManualResetEventSlim();

        protected int myManagedThreadId = -1;

        protected SpinLock myStateLock;

        protected bool myIsActive;

        public abstract int ActorExecuteQueueCount
        {

            get;

        }

        public abstract bool ActorExecuteQueueIsEmpty
        {

            get;

        }

        //The actor is active when it has a current task or is about to set the current task

        public abstract bool ActorIsActive
        {

            get;

        }

        //Make sure actor is in the active state

        [DoNotCall]
        protected virtual bool __ActorTrySetIsActive()
        {

            //__ActorCheckIsSameAssembly();

            //if (assFrameChecker == null)
            //    assFrameChecker = new AssFrameChecker(1);

            bool taken = false;

            myStateLock.Enter(ref taken);

            try
            {

                if (!myIsActive)
                {

                    myIsActive = true;

                    return true;

                }

            }
            finally
            {

                if (taken)
                    myStateLock.Exit();

            }

            return false;

        }

        [DoNotCall]
        protected abstract void __ActorSetInActive();

        public bool ActorIsIdle
        {

            get
            {

                return myOnIdleEvent.IsSet;

            }

        }

        public void ActorWaitForIdle()
        {

            ThrowIfExecutingAndCurrentThreadsAreTheSame();

            myOnIdleEvent.Wait();

        }

        public bool ActorWaitForIdle(int millisecondsTimeout)
        {

            ThrowIfExecutingAndCurrentThreadsAreTheSame();

            return myOnIdleEvent.Wait(millisecondsTimeout);

        }

        public bool ActorWaitForIdle(int millisecondsTimeout, CancellationToken cancellationToken)
        {

            ThrowIfExecutingAndCurrentThreadsAreTheSame();

            return myOnIdleEvent.Wait(millisecondsTimeout, cancellationToken);

        }

        public bool ActorWaitForIdle(TimeSpan timeout)
        {

            ThrowIfExecutingAndCurrentThreadsAreTheSame();

            return myOnIdleEvent.Wait(timeout);

        }

        public bool ActorWaitForIdle(TimeSpan timeout, CancellationToken cancellationToken)
        {

            ThrowIfExecutingAndCurrentThreadsAreTheSame();

            return myOnIdleEvent.Wait(timeout, cancellationToken);

        }

        public int ActorManagedThreadId
        {

            get
            {

                bool taken = false;

                myStateLock.Enter(ref taken);

                try
                {

                    return myManagedThreadId;

                }
                finally
                {

                    if (taken)
                        myStateLock.Exit();

                }

            }

        }

        public bool ActorIsCurrentThread
        {

            get
            {

                return ActorManagedThreadId == Thread.CurrentThread.ManagedThreadId;

            }

        }

        protected void SetManagedThreadId()
        {

            int threadId = Thread.CurrentThread.ManagedThreadId;

            bool taken = false;

            myStateLock.Enter(ref taken);

            try
            {

                myManagedThreadId = threadId;

            }
            finally
            {

                if (taken)
                    myStateLock.Exit();

            }

        }

        protected void ThrowIfExecutingAndCurrentThreadsAreTheSame()
        {

            if (ActorIsCurrentThread)
                throw new ActorSelfWaitException();

        }

        //protected void SetTaskAndStart(Task __ActorNextTask)
        //{

        //    myOnIdleEvent.Reset();

        //    bool taken = false;

        //    myStateLock.Enter(ref taken);

        //    try
        //    {

        //        myCurrentTask = __ActorNextTask;

        //    }
        //    finally
        //    {

        //        if (taken)
        //            myStateLock.Exit();

        //    }

        //    __ActorNextTask.Start();

        //}

        //public bool ActorIsActorTask()

        [DoNotCall]
        protected abstract void __ActorNextTask();

        //Continuations

        //protected abstract Task ActorContinueWith(Action action);

        [DoNotCall]
        protected abstract void __ActorEnqueue(Task task);

        //Needs to be optional

        protected virtual void ActorPreDelegate()
        {
        }

        //Delegate Queueing

        //Actions
        
        protected ActorTask ActorSetup(Action action)
        {

            Task t = new Task(() => {

                SetManagedThreadId();

                //ActorCheckRetainedTasks();

                ActorPreDelegate();

                try
                {

                    action();

                }
                finally
                {

                    __ActorNextTask();

                }

            });

            ActorTask at = new ActorTask(t, this);

            __ActorEnqueue(t);

            return at;

        }

        protected ActorTask ActorSetup(Action action, Action<Exception> exceptionAction)
        {

            Task t = new Task(() => {

                SetManagedThreadId();

                ActorPreDelegate();

                try
                {

                    action();

                }
                catch (Exception e)
                {

                    exceptionAction(e);

                }
                finally
                {

                    __ActorNextTask();

                }

            });

            ActorTask at = new ActorTask(t, this);

            __ActorEnqueue(t);

            return at;

        }

        //Ignoring the Exception

        protected ActorTask ActorSetupIgnore(Action action)
        {

            Task t = new Task(() => {

                SetManagedThreadId();

                ActorPreDelegate();

                try
                {

                    action();

                }
                finally
                {

                    __ActorNextTask();

                }

            });

            ActorTask at = new ActorTask(t, this);

            __ActorEnqueue(t);

            return at;

        }

        //FailFast if Exception is thrown

        protected ActorTask ActorSetupFailFast(Action action)
        {

            Task t = new Task(() => {

                SetManagedThreadId();

                ActorPreDelegate();

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

                    __ActorNextTask();

                }

            });

            ActorTask at = new ActorTask(t, this);

            __ActorEnqueue(t);

            return at;

        }

        //Funcs

        protected ActorTask<TResult> ActorSetup<TResult>(Func<TResult> func)
        {

            Task<TResult> t = new Task<TResult>(() => {

                SetManagedThreadId();

                //ActorCheckRetainedTasks();

                ActorPreDelegate();

                try
                {

                    return func();

                }
                finally
                {

                    __ActorNextTask();

                }

            });

            ActorTask<TResult> at = new ActorTask<TResult>(t, this);

            __ActorEnqueue(t);

            return at;

        }
        
        protected ActorTask<TResult> ActorSetup<TResult>(Func<TResult> func, Action<Exception> exceptionAction)
        {

            Task<TResult> t = new Task<TResult>(() => {

                SetManagedThreadId();

                //ActorCheckRetainedTasks();

                ActorPreDelegate();

                try
                {

                    return func();

                }
                catch (Exception e)
                {

                    exceptionAction(e);

                    throw;

                }
                finally
                {

                    __ActorNextTask();

                }

            });

            ActorTask<TResult> at = new ActorTask<TResult>(t, this);

            __ActorEnqueue(t);

            return at;

        }

        //ActorSetup with no OnPreDelegate

        protected ActorTask ActorSetupNoPreDelegate(Action action)
        {

            Task t = new Task(() => {

                SetManagedThreadId();

                try
                {

                    action();

                }
                finally
                {

                    __ActorNextTask();

                }

            });

            ActorTask at = new ActorTask(t, this);

            __ActorEnqueue(t);

            return at;

        }

        //FailFast if Exception is thrown

        protected ActorTask ActorSetupFailFastNoPreDelegate(Action action)
        {

            Task t = new Task(() => {

                SetManagedThreadId();

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

                    __ActorNextTask();

                }

            });

            ActorTask at = new ActorTask(t, this);

            __ActorEnqueue(t);

            return at;

        }

        //Funcs

        protected ActorTask<TResult> ActorSetupNoPreDelegate<TResult>(Func<TResult> func)
        {

            Task<TResult> t = new Task<TResult>(() => {

                SetManagedThreadId();

                try
                {

                    return func();

                }
                finally
                {

                    __ActorNextTask();

                }

            });

            ActorTask<TResult> at = new ActorTask<TResult>(t, this);

            __ActorEnqueue(t);

            return at;

        }

        //

        //Actor Continuations

        //Optional

        //public virtual ActorTask Continue(Task withTask)
        //{

        //    throw new NotImplementedException();

        //}

        protected MethodInfo GetMethodToCall(string methodName, params Type[] parameterTypes)
        {

            Type t = this.GetType();

            var info = t.GetMethod(methodName, BindingFlags.Public | BindingFlags.NonPublic, null, parameterTypes, null);

            CheckCanCall(info);

            return info;

        }

        protected void CheckCanCall(MethodInfo info)
        {

            var doNoCallAttr = info.GetCustomAttribute(typeof(DoNotCallAttribute));

            if (doNoCallAttr != null)
                throw new InvalidOperationException("Do not call a DoNotCallAttribute decoated method");

        }

        //protected MethodInfo GetMethodToCall(string methodName)
        //{

        //    Type t = this.GetType();

        //    var info = t.GetMethod(methodName, BindingFlags.Public | BindingFlags.NonPublic);

        //    return info;

        //}

        public virtual ActorTask Call(string methodName)
        {

            return ActorSetup(() =>
            {

                MethodInfo info = GetMethodToCall(methodName);

                info.Invoke(this, null);

            });

        }

        public virtual ActorTask<T> CallResult<T>(string methodName)
        {

            return ActorSetup(() =>
            {

                MethodInfo info = GetMethodToCall(methodName);

                return (T)info.Invoke(this, null);

            });

        }

        public virtual ActorTask Call(string methodName, Task withTask)
        {

            return ActorSetup(() =>
            {

                MethodInfo info = GetMethodToCall(methodName, new Type[] { withTask.GetType() });

                info.Invoke(this, new object[] { withTask });

            });

        }

        public virtual ActorTask Call(string methodName, ActorTask withTask)
        {

            return ActorSetup(() =>
            {

                MethodInfo info = GetMethodToCall(methodName, new Type[] { withTask.GetType() });

                info.Invoke(this, new object[]{ withTask });

            });

        }

        public virtual ActorTask<T> Call<T>(string methodName, Task<T> withTask)
        {

            return ActorSetup(() =>
            {

                MethodInfo info = GetMethodToCall(methodName, new Type[] { withTask.GetType() });

                return (T)info.Invoke(this, new object[] { withTask });

            });

        }

        public virtual ActorTask<T> Call<T>(string methodName, ActorTask<T> withTask)
        {

            return ActorSetup(() =>
            {

                MethodInfo info = GetMethodToCall(methodName, new Type[] { withTask.GetType() });

                return (T)info.Invoke(this, new object[] { withTask });

            });

        }

        public virtual ActorTask<T> Call<T>(string methodName, Task withTask)
        {

            return ActorSetup(() =>
            {

                MethodInfo info = GetMethodToCall(methodName, new Type[] { withTask.GetType() });

                return (T)info.Invoke(this, new object[] { withTask });

            });

        }

        public virtual ActorTask<T> Call<T>(string methodName, ActorTask withTask)
        {

            return ActorSetup(() =>
            {

                MethodInfo info = GetMethodToCall(methodName, new Type[] { withTask.GetType() });

                return (T)info.Invoke(this, new object[] { withTask });

            });

        }

        public virtual ActorTask CallNoResult<T>(string methodName, Task<T> withTask)
        {

            return ActorSetup(() =>
            {

                MethodInfo info = GetMethodToCall(methodName, new Type[] { withTask.GetType() });

                info.Invoke(this, new object[] { withTask });

            });

        }

        public virtual ActorTask CallNoResult<T>(string methodName, ActorTask<T> withTask)
        {

            return ActorSetup(() =>
            {

                MethodInfo info = GetMethodToCall(methodName, new Type[] { withTask.GetType() });

                info.Invoke(this, new object[] { withTask });

            });

        }

        //

        //protected void __ActorCheckIsSameAssembly()
        //{

        //    var assembly = this.GetType().Assembly;

        //    StackTrace st = new StackTrace();

        //    var frame1 = st.GetFrame(1); //Check previous frame

        //    CheckFrame(frame1, assembly);

        //    var frame2 = st.GetFrame(2); //Check "Outer" frame

        //    CheckFrame(frame2, assembly);

        //    //if (Assembly.GetCallingAssembly() != this.GetType().Assembly)
        //    //    throw new InvalidOperationException("Do not call this method or property");

        //}

        //private void CheckFrame(StackFrame sf, Assembly assembly)
        //{

        //    if (sf.GetMethod().DeclaringType.Assembly != this.GetType().Assembly)
        //        throw new InvalidOperationException("Do not call this method or property");

        //}

        protected void __ActorCheckTask(Task task)
        {

            if (task.Status != TaskStatus.Created)
                throw new InvalidOperationException("Task status must be \"Created\"");

        }

        public virtual void Dispose()
        {

            myOnIdleEvent.Wait();

            myOnIdleEvent.Dispose();

        }

    }

}
