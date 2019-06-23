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

        public ActorTask Subscribe(Action<TSender, TEventArgs> item)
        {

            return ActorSetupFailFastNoPreDelegate(() => {

                if(!myActionList.Contains(item))
                    myActionList.Add(item);

            });

        }

        public ActorTask UnSubscribe(Action<TSender, TEventArgs> item)
        {

            return ActorSetupFailFastNoPreDelegate(() => {

                myActionList.Remove(item);

            });

        }

        public ActorTask Call(TSender sender, TEventArgs eventArgs)
        {

            return ActorSetupFailFastNoPreDelegate(() => {

                //Call each action delegate on a separate thread

                foreach (var item in myActionList)
                    Async_Action_Extensions.AsyncFailFast(() => item(sender, eventArgs));

            });

        }

        public ActorTask<List<Task>> Call_(TSender sender, TEventArgs eventArgs)
        {

            return ActorSetupNoPreDelegate(() => {

                List<Task> results = new List<Task>();

                //Call each action delegate on a separate thread

                foreach (var item in myActionList)
                    results.Add(item.Async(sender, eventArgs));

                return results;

            });

        }

        public ActorTask Call(TSender sender, TEventArgs eventArgs, List<Task> results)
        {

            return ActorSetupNoPreDelegate(() => {

                results.Capacity = myActionList.Capacity;

                results.Clear();

                //Call each action delegate on a separate thread

                foreach (var item in myActionList)
                    results.Add(item.Async(sender, eventArgs));

            });

        }

        public ActorTask<int> Count()
        {

            return ActorSetupNoPreDelegate(() => {

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
