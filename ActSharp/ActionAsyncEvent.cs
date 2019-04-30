using System;
using System.Collections.Generic;
using System.Text;
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

            ActorEnqueueDelegateFailFastNoTaskListCheck(() => {

                if(!myActionList.Contains(item))
                    myActionList.Add(item);

            });

        }

        public void UnSubscribe(Action<TSender, TEventArgs> item)
        {

            ActorEnqueueDelegateFailFastNoTaskListCheck(() => {

                myActionList.Remove(item);

            });

        }

        public void Call(TSender sender, TEventArgs eventArgs)
        {

            ActorEnqueueDelegateFailFastNoTaskListCheck(() => {

                //Call each action delegate on a separate thread

                foreach (var item in myActionList)
                    Async_Action_Extensions.AsyncFailFast(() => item(sender, eventArgs));

            });

        }

        public ActorTask<int> Count()
        {

            return ActorEnqueueDelegateNoTaskListCheck(() => {

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
