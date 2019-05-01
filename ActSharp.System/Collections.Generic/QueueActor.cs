using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ActSharp.System.Collections.Generic
{

    public sealed class QueueActor<T> : Actor, IEnumerable<Task<(T, bool)>> //, IEnumerable<Task<(T, bool)>>
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

        public Task<int> Count()
        {

            return ActorEnqueueNoTaskListCheck(() => myQueue.Count);

        }

        public Task<bool> IsEmpty()
        {

            return ActorEnqueueNoTaskListCheck(() => myQueue.Count < 1);

        }

        public Task Clear()
        {

            return ActorEnqueueNoTaskListCheck(myQueue.Clear);

        }

        public Task<bool> Contains(T item)
        {

            return ActorEnqueueNoTaskListCheck(() => myQueue.Contains(item));

        }

        public Task CopyTo(T[] array, int arrayIndex)
        {

            return ActorEnqueueNoTaskListCheck(() => myQueue.CopyTo(array, arrayIndex));

        }

        public Task<T> Dequeue()
        {

            return ActorEnqueueNoTaskListCheck(myQueue.Dequeue);

        }

        public Task<(T, bool)> TryDequeue()
        {

            return ActorEnqueueNoTaskListCheck(() => {

                var result = (Result: default(T), Found: false);

                if (myQueue.Count > 0)
                {

                    result.Result = myQueue.Dequeue();

                    result.Found = true;

                }

                return result;

            });

        }

        public Task Enqueue(T item)
        {

            return ActorEnqueueNoTaskListCheck(() => myQueue.Enqueue(item));

        }

        public Task<(T, bool)> TryGetEnumerated(int index)
        {

            if (index < 0)
            {

                var task = new global::System.Threading.Tasks.Task<(T, bool)>(() => (default(T), false));

                task.Start();

                //return new Task<(T, bool)>(task);

                return task;

            }//(Result: default(T), Found: false);

            return ActorEnqueueNoTaskListCheck(() => {

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

        public IEnumerator<Task<(T, bool)>> GetEnumerator()
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

        public Task ForEach(Action<T> action)
        {

            return ActorEnqueueNoTaskListCheck(() => {

                foreach (T item in myQueue)
                {

                    action(item);

                }

            });

        }

        public Task ForEach(Action<T, int> action)
        {

            return ActorEnqueueNoTaskListCheck(() => {

                int index = 0;

                foreach (T item in myQueue)
                {

                    action(item, index);

                    index++;

                }

            });

        }

        public Task<Queue<T>> CloneQueue()
        {

            return ActorEnqueueNoTaskListCheck(() => new Queue<T>(myQueue));

        }

        public Task<T> Peek()
        {

            return ActorEnqueueNoTaskListCheck(myQueue.Peek);

        }

        public Task<(T, bool)> TryPeek()
        {

            return ActorEnqueueNoTaskListCheck(() => {

                var result = (Result: default(T), Found: false);

                if (myQueue.Count > 0)
                {

                    result.Result = myQueue.Peek();

                    result.Found = true;

                }

                return result;

            });

        }

        public Task<T[]> ToArray()
        {

            return ActorEnqueueNoTaskListCheck(myQueue.ToArray);

        }

        public Task TrimExcess()
        {

            return ActorEnqueueNoTaskListCheck(myQueue.TrimExcess);

        }

    }

}
