using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ActSharp.System.Collections.Generic
{

    public sealed class ListActor<T> : Actor, IEnumerable<ActorTask<(T, bool)>>
    {

        List<T> myList;

        public ListActor()
        {

            myList = new List<T>();

        }

        public ListActor(IEnumerable<T> collection)
        {

            myList = new List<T>(collection);

        }

        public ListActor(int capacity)
        {

            myList = new List<T>(capacity);

        }

        public ActorTask<int> Capacity()
        {

            return ActorEnqueueDelegateNoTaskListCheck(() => { return myList.Capacity; });

        }

        public ActorTask Capacity(int value)
        {

            return ActorEnqueueDelegateNoTaskListCheck(() => { myList.Capacity = value; });

        }

        public ActorTask<int> Count()
        {

            return ActorEnqueueDelegateNoTaskListCheck(() => { return myList.Count; });

        }

        public ActorTask<T> this[int index]
        {

            get
            {

                return ActorEnqueueDelegateNoTaskListCheck(() => { return myList[index]; });

            }

        }

        public ActorTask<T> Get(int index)
        {

            return ActorEnqueueDelegateNoTaskListCheck(() => { return myList[index]; });

        }

        public ActorTask Set(int index, T value)
        {

            return ActorEnqueueDelegateNoTaskListCheck(() => { myList[index] = value; });

        }

        //

        public ActorTask<(T, bool)> TryGet(int index)
        {

            return ActorEnqueueDelegateNoTaskListCheck(() => {

                var result = (Result: default(T), Found: false);

                if (index > -1 && index < myList.Count)
                {

                    result.Result = myList[index];

                    result.Found = true;

                }

                return result;

            });

        }

        public ActorTask<bool> TrySet(int index, T value)
        {

            return ActorEnqueueDelegateNoTaskListCheck(() => {

                if (index > -1 && index < myList.Count)
                {

                    myList[index] = value;

                    return true;

                }

                return false;

            });

        }

        //

        public ActorTask Add(T item)
        {

            return ActorEnqueueDelegateNoTaskListCheck(() => { myList.Add(item); });

        }

        public ActorTask AddRange(IEnumerable<T> collection)
        {

            return ActorEnqueueDelegateNoTaskListCheck(() => { myList.AddRange(collection); });

        }

        public ActorTask<List<T>> CloneList()
        {

            return ActorEnqueueDelegateNoTaskListCheck(() => new List<T>(myList));

        }

        public ActorTask<int> BinarySearch(int index, int count, T item, IComparer<T> comparer)
        {

            return ActorEnqueueDelegateNoTaskListCheck(() => myList.BinarySearch(index, count, item, comparer));

        }

        public ActorTask<int> BinarySearch(T item)
        {

            return ActorEnqueueDelegateNoTaskListCheck(() => myList.BinarySearch(item));

        }

        public ActorTask<int> BinarySearch(T item, IComparer<T> comparer)
        {

            return ActorEnqueueDelegateNoTaskListCheck(() => myList.BinarySearch(item, comparer));

        }

        public ActorTask Clear()
        {

            return ActorEnqueueDelegateNoTaskListCheck(myList.Clear);

        }

        public ActorTask<bool> Contains(T item)
        {

            return ActorEnqueueDelegateNoTaskListCheck(() => myList.Contains(item));

        }

        public ActorTask<List<TOutput>> ConvertAll<TOutput>(Converter<T, TOutput> converter)
        {

            return ActorEnqueueDelegateNoTaskListCheck(() => myList.ConvertAll(converter));

        }

        public ActorTask CopyTo(int index, T[] array, int arrayIndex, int count)
        {

            return ActorEnqueueDelegateNoTaskListCheck(() => myList.CopyTo(index, array, arrayIndex, count));

        }

        public ActorTask CopyTo(T[] array)
        {

            return ActorEnqueueDelegateNoTaskListCheck(() => myList.CopyTo(array));

        }

        public ActorTask CopyTo(T[] array, int arrayIndex)
        {

            return ActorEnqueueDelegateNoTaskListCheck(() => myList.CopyTo(array, arrayIndex));

        }

        public ActorTask<bool> Exists(Predicate<T> match)
        {

            return ActorEnqueueDelegateNoTaskListCheck(() => myList.Exists(match));

        }

        public ActorTask<T> Find(Predicate<T> match)
        {

            return ActorEnqueueDelegateNoTaskListCheck(() => myList.Find(match));

        }

        public ActorTask<List<T>> FindAll(Predicate<T> match)
        {

            return ActorEnqueueDelegateNoTaskListCheck(() => myList.FindAll(match));

        }

        //Find...

        public ActorTask ForEach(Action<T> action)
        {

            return ActorEnqueueDelegateNoTaskListCheck(() => myList.ForEach(action));

        }

        public ActorTask<List<T>> GetRange(int index, int count)
        {

            return ActorEnqueueDelegateNoTaskListCheck(() => myList.GetRange(index, count));

        }

        public ActorTask<int> IndexOf(T item)
        {

            return ActorEnqueueDelegateNoTaskListCheck(() => myList.IndexOf(item));

        }

        public ActorTask<int> IndexOf(T item, int index)
        {

            return ActorEnqueueDelegateNoTaskListCheck(() => myList.IndexOf(item, index));

        }

        public ActorTask<int> IndexOf(T item, int index, int count)
        {

            return ActorEnqueueDelegateNoTaskListCheck(() => myList.IndexOf(item, index, count));

        }

        public ActorTask Insert(int index, T item)
        {

            return ActorEnqueueDelegateNoTaskListCheck(() => myList.Insert(index, item));

        }

        public ActorTask InsertRange(int index, IEnumerable<T> collection)
        {

            return ActorEnqueueDelegateNoTaskListCheck(() => myList.InsertRange(index, collection));

        }

        public ActorTask<bool> Remove(T item)
        {

            return ActorEnqueueDelegateNoTaskListCheck(() => myList.Remove(item));

        }

        public ActorTask<int> RemoveAll(Predicate<T> match)
        {

            return ActorEnqueueDelegateNoTaskListCheck(() => myList.RemoveAll(match));

        }

        public ActorTask RemoveAt(int index)
        {

            return ActorEnqueueDelegateNoTaskListCheck(() => myList.RemoveAt(index));

        }

        public ActorTask RemoveRange(int index, int count)
        {

            return ActorEnqueueDelegateNoTaskListCheck(() => myList.RemoveRange(index, count));

        }

        public ActorTask Reverse()
        {

            return ActorEnqueueDelegateNoTaskListCheck(myList.Reverse);

        }

        public ActorTask Reverse(int index, int count)
        {

            return ActorEnqueueDelegateNoTaskListCheck(() => myList.Reverse(index, count));

        }

        public ActorTask Sort()
        {

            return ActorEnqueueDelegateNoTaskListCheck(myList.Sort);

        }

        public ActorTask Sort(Comparison<T> comparison)
        {

            return ActorEnqueueDelegateNoTaskListCheck(() => myList.Sort(comparison));

        }

        public ActorTask Sort(IComparer<T> comparer)
        {

            return ActorEnqueueDelegateNoTaskListCheck(() => myList.Sort(comparer));

        }

        public ActorTask Sort(int index, int count, IComparer<T> comparer)
        {

            return ActorEnqueueDelegateNoTaskListCheck(() => myList.Sort(index, count, comparer));

        }

        public ActorTask<T[]> ToArray()
        {

            return ActorEnqueueDelegateNoTaskListCheck(() => myList.ToArray());

        }

        public ActorTask TrimExcess()
        {

            return ActorEnqueueDelegateNoTaskListCheck(myList.TrimExcess);

        }

        public ActorTask<bool> TrueForAll(Predicate<T> match)
        {

            return ActorEnqueueDelegateNoTaskListCheck(() => myList.TrueForAll(match));

        }

        public IEnumerator<ActorTask<(T, bool)>> GetEnumerator()
        {

            int index = 0;

            bool hasResult = false;

            do
            {

                var itemTask = TryGet(index);

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

    }

}
