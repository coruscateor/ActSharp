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

            return ActorEnqueueDelegateNoTaskListCheck(() => myQueue.Count);

        }

        public ActorTask<bool> IsEmpty()
        {

            return ActorEnqueueDelegateNoTaskListCheck(() => myQueue.Count < 1);

        }

        public ActorTask Clear()
        {

            return ActorEnqueueDelegateNoTaskListCheck(myQueue.Clear);

        }

        public ActorTask<bool> Contains(T item)
        {

            return ActorEnqueueDelegateNoTaskListCheck(() => myQueue.Contains(item));

        }

        public ActorTask CopyTo(T[] array, int arrayIndex)
        {

            return ActorEnqueueDelegateNoTaskListCheck(() => myQueue.CopyTo(array, arrayIndex));

        }

        public ActorTask<T> Dequeue()
        {

            return ActorEnqueueDelegateNoTaskListCheck(myQueue.Dequeue);

        }

        public ActorTask<(T, bool)> TryDequeue()
        {

            return ActorEnqueueDelegateNoTaskListCheck(() => {

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

            return ActorEnqueueDelegateNoTaskListCheck(() => myQueue.Enqueue(item));

        }


        public ActorTask<(T, bool)> TryGetEnumerated(int index)
        {

            if (index < 0)
            {

                var task = new global::System.Threading.Tasks.Task<(T, bool)>(() => (default(T), false));

                task.Start();

                return new ActorTask<(T, bool)>(task);

            }//(Result: default(T), Found: false);

            return ActorEnqueueDelegateNoTaskListCheck(() => {

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

            return ActorEnqueueDelegateNoTaskListCheck(() => {

                foreach (T item in myQueue)
                {

                    action(item);

                }

            });

        }

        public ActorTask ForEach(Action<T, int> action)
        {

            return ActorEnqueueDelegateNoTaskListCheck(() => {

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

            return ActorEnqueueDelegateNoTaskListCheck(() => new Queue<T>(myQueue));

        }

        public ActorTask<T> Peek()
        {

            return ActorEnqueueDelegateNoTaskListCheck(myQueue.Peek);

        }

        public ActorTask<(T, bool)> TryPeek()
        {

            return ActorEnqueueDelegateNoTaskListCheck(() => {

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

            return ActorEnqueueDelegateNoTaskListCheck(myQueue.ToArray);

        }

        public ActorTask TrimExcess()
        {

            return ActorEnqueueDelegateNoTaskListCheck(myQueue.TrimExcess);

        }

    }

}
