using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ActSharp.Async;

namespace ActSharp
{

    public sealed class ActionAsyncEvent<TSender, TEventArgs> : Actor
    {

        List<Action<TSender, TEventArgs>> myActionList = new List<Action<TSender, TEventArgs>>(0);

        public ActionAsyncEvent()
        {
        }

        public void Subscribe(Action<TSender, TEventArgs> item)
        {

            ActorEnqueueFailFastNoTaskListCheck(() => {

                if(!myActionList.Contains(item))
                    myActionList.Add(item);

            });

        }

        public void UnSubscribe(Action<TSender, TEventArgs> item)
        {

            ActorEnqueueFailFastNoTaskListCheck(() => {

                myActionList.Remove(item);

            });

        }

        public void Call(TSender sender, TEventArgs eventArgs)
        {

            ActorEnqueueFailFastNoTaskListCheck(() => {

                //Call each action delegate on a separate thread

                foreach (var item in myActionList)
                    Async_Action_Extensions.AsyncFailFast(() => item(sender, eventArgs));

            });

        }

        public Task Call(TSender sender, TEventArgs eventArgs, List<Task> results)
        {

            return ActorEnqueueNoTaskListCheck(() => {

                results.Capacity = myActionList.Capacity;

                results.Clear();

                //Call each func delegate on a separate thread

                foreach (var item in myActionList)
                    results.Add(item.Async(sender, eventArgs));

            });

        }

        public Task<int> Count()
        {

            return ActorEnqueueNoTaskListCheck(() => {

                return myActionList.Count;

            });

        }

        //Get an ActionAsyncEventSubscriber so that external users can subscribe to the async event without being able to call it

        public ActionAsyncEventSubscriber GetActionAsyncEventSubscriber()
        {

            return new ActionAsyncEventSubscriber(this);

        }

        public sealed class ActionAsyncEventSubscriber
        {

            ActionAsyncEvent<TSender, TEventArgs> myEvent;

            public ActionAsyncEventSubscriber(ActionAsyncEvent<TSender, TEventArgs> ent)
            {

                myEvent = ent;

            }

            public void Subscribe(Action<TSender, TEventArgs> item)
            {

                myEvent.Subscribe(item);

            }

            public void UnSubscribe(Action<TSender, TEventArgs> item)
            {

                myEvent.UnSubscribe(item);

            }

        }

    }

}
