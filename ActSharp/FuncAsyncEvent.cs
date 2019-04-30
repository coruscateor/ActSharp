using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ActSharp.Async;

namespace ActSharp
{

    public sealed class FuncAsyncEvent<TSender, TEventArgs, TResult> : Actor
    {

        List<Func<TSender, TEventArgs, TResult>> myFuncList = new List<Func<TSender, TEventArgs, TResult>>(0);

        public FuncAsyncEvent()
        {
        }

        public void Subscribe(Func<TSender, TEventArgs, TResult> item)
        {

            ActorEnqueueDelegateFailFastNoTaskListCheck(() => {

                if (!myFuncList.Contains(item))
                    myFuncList.Add(item);

            });

        }

        public void UnSubscribe(Func<TSender, TEventArgs, TResult> item)
        {

            ActorEnqueueDelegateFailFastNoTaskListCheck(() => {

                myFuncList.Remove(item);

            });

        }

        public ActorTask<List<Task<TResult>>> Call(TSender sender, TEventArgs eventArgs)
        {

            return ActorEnqueueDelegateNoTaskListCheck(() => {

                List<Task<TResult>> results = new List<Task<TResult>>();

                //Call each func delegate on a separate thread

                foreach (var item in myFuncList)
                    results.Add(item.Async(sender, eventArgs));

                return results;

            });

        }

        public ActorTask Call(TSender sender, TEventArgs eventArgs, List<Task<TResult>> results)
        {

            return ActorEnqueueDelegateNoTaskListCheck(() => {

                results.Capacity = myFuncList.Capacity;

                results.Clear();

                //Call each func delegate on a separate thread

                foreach (var item in myFuncList)
                    results.Add(item.Async(sender, eventArgs));

            });

        }

        public ActorTask<int> Count()
        {

            return ActorEnqueueDelegateNoTaskListCheck(() => {

                return myFuncList.Count;

            });

        }

        //Get an FuncAsyncEventSubscriber so that external users can subscribe to the async event without being able to call it

        public FuncAsyncEventSubscriber GetFuncAsyncEventSubscriber()
        {

            return new FuncAsyncEventSubscriber(this);

        }

        public sealed class FuncAsyncEventSubscriber
        {

            FuncAsyncEvent<TSender, TEventArgs, TResult> myEvent;

            public FuncAsyncEventSubscriber(FuncAsyncEvent<TSender, TEventArgs, TResult> ent)
            {

                myEvent = ent;

            }

            public void Subscribe(Func<TSender, TEventArgs, TResult> item)
            {

                myEvent.Subscribe(item);

            }

            public void UnSubscribe(Func<TSender, TEventArgs, TResult> item)
            {

                myEvent.UnSubscribe(item);

            }

        }

    }

}
