using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ActSharp.System.Collections.Generic
{

    public sealed class QueueActor<T> : Actor, IEnumerable<ActorTask<(T, bool)>> //, IEnumerable<ActorTask<(T, bool)>>
    {

        Queue<T> myQueue;

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

            return ActorEnqueueDelegate(() => myQueue.Count);

        }

        public ActorTask<bool> IsEmpty()
        {

            return ActorEnqueueDelegate(() => myQueue.Count < 1);

        }

        public ActorTask Clear()
        {

            return ActorEnqueueDelegate(myQueue.Clear);

        }

        public ActorTask<bool> Contains(T item)
        {

            return ActorEnqueueDelegate(() => myQueue.Contains(item));

        }

        public ActorTask CopyTo(T[] array, int arrayIndex)
        {

            return ActorEnqueueDelegate(() => myQueue.CopyTo(array, arrayIndex));

        }

        public ActorTask<T> Dequeue()
        {

            return ActorEnqueueDelegate(myQueue.Dequeue);

        }

        public ActorTask<(T, bool)> TryDequeue()
        {

            return ActorEnqueueDelegate(() => {

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

            return ActorEnqueueDelegate(() => myQueue.Enqueue(item));

        }


        public ActorTask<(T, bool)> TryGetEnumerated(int index)
        {

            if (index < 0)
            {

                var task = new global::System.Threading.Tasks.Task<(T, bool)>(() => (default(T), false));

                task.Start();

                return new ActorTask<(T, bool)>(task);

            }//(Result: default(T), Found: false);

            return ActorEnqueueDelegate(() => {

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

            return ActorEnqueueDelegate(() => {

                foreach (T item in myQueue)
                {

                    action(item);

                }

            });

        }

        public ActorTask ForEach(Action<T, int> action)
        {

            return ActorEnqueueDelegate(() => {

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

            return ActorEnqueueDelegate(() => new Queue<T>(myQueue));

        }

        public ActorTask<T> Peek()
        {

            return ActorEnqueueDelegate(myQueue.Peek);

        }

        public ActorTask<(T, bool)> TryPeek()
        {

            return ActorEnqueueDelegate(() => {

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

            return ActorEnqueueDelegate(myQueue.ToArray);

        }

        public ActorTask TrimExcess()
        {

            return ActorEnqueueDelegate(myQueue.TrimExcess);

        }

    }

}
