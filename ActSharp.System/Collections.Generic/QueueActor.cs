using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ActSharp.System.Collections.Generic
{

    public sealed class QueueActor<T> : Actor, IEnumerable<ActorTask<(T, bool)>> //, IEnumerable<Task<(T, bool)>>
    {

        readonly Queue<T> myQueue;

        public QueueActor()
        {

            myQueue = new Queue<T>();

        }

        public QueueActor(IEnumerable<T> collection)
        {

            myQueue = new Queue<T>(collection);

        }

        public QueueActor(int capacity)
        {

            myQueue = new Queue<T>(capacity);

        }

        public ActorTask<int> Count()
        {

            return ActorSetupNoPreDelegate(() => myQueue.Count);

        }

        public ActorTask<bool> IsEmpty()
        {

            return ActorSetupNoPreDelegate(() => myQueue.Count < 1);

        }

        public ActorTask Clear()
        {

            return ActorSetupNoPreDelegate(myQueue.Clear);

        }

        public ActorTask<bool> Contains(T item)
        {

            return ActorSetupNoPreDelegate(() => myQueue.Contains(item));

        }

        public ActorTask CopyTo(T[] array, int arrayIndex)
        {

            return ActorSetupNoPreDelegate(() => myQueue.CopyTo(array, arrayIndex));

        }

        public ActorTask<T> Dequeue()
        {

            return ActorSetupNoPreDelegate(myQueue.Dequeue);

        }

        public ActorTask<(T, bool)> TryDequeue()
        {

            return ActorSetupNoPreDelegate(() => {

                var result = (Result: default(T), Found: false);

                if (myQueue.Count > 0)
                {

                    result.Result = myQueue.Dequeue();

                    result.Found = true;

                }

                return result;

            });

        }

        public ActorTask Enqueue(T item)
        {

            return ActorSetupNoPreDelegate(() => myQueue.Enqueue(item));

        }

        public ActorTask<(T, bool)> TryGetEnumerated(int index)
        {

            if (index < 0)
            {

                var task = new Task<(T, bool)>(() => { throw new IndexOutOfRangeException(); });

                task.Start();

                ActorTask<(T, bool)> at = new ActorTask<(T, bool)>(task, this);

                return at;

            }

            return ActorSetupNoPreDelegate(() => {

                var result = (Result: default(T), Found: false);

                int currentIndex = 0;

                foreach(T item in myQueue)
                {

                    if(currentIndex == index)
                    {

                        result.Result = item;

                        result.Found = true;

                    }

                    currentIndex++;

                }

                return result;

            });

        }

        public IEnumerator<ActorTask<(T, bool)>> GetEnumerator()
        {

            int index = 0;

            bool hasResult = false;

            do
            {

                var itemTask = TryGetEnumerated(index);

                //var result = itemTask.Result; //Wait for the result

                //yield return result.Item1;

                yield return itemTask;

                index++;

                hasResult = itemTask.Result.Item2;

            }
            while (hasResult);


        }

        IEnumerator IEnumerable.GetEnumerator()
        {

            return GetEnumerator();

        }

        public ActorTask ForEach(Action<T> action)
        {

            return ActorSetupNoPreDelegate(() => {

                foreach (T item in myQueue)
                {

                    action(item);

                }

            });

        }

        public ActorTask ForEach(Action<T, int> action)
        {

            return ActorSetupNoPreDelegate(() => {

                int index = 0;

                foreach (T item in myQueue)
                {

                    action(item, index);

                    index++;

                }

            });

        }

        public ActorTask<Queue<T>> CloneQueue()
        {

            return ActorSetupNoPreDelegate(() => new Queue<T>(myQueue));

        }

        public ActorTask<T> Peek()
        {

            return ActorSetupNoPreDelegate(myQueue.Peek);

        }

        public ActorTask<(T, bool)> TryPeek()
        {

            return ActorSetupNoPreDelegate(() => {

                var result = (Result: default(T), Found: false);

                if (myQueue.Count > 0)
                {

                    result.Result = myQueue.Peek();

                    result.Found = true;

                }

                return result;

            });

        }

        public ActorTask<T[]> ToArray()
        {

            return ActorSetupNoPreDelegate(myQueue.ToArray);

        }

        public ActorTask TrimExcess()
        {

            return ActorSetupNoPreDelegate(myQueue.TrimExcess);

        }

    }

}
