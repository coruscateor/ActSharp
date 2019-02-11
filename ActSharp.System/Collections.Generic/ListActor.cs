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

            return ActorEnqueueDelegate(() => { return myList.Capacity; });

        }

        public ActorTask Capacity(int value)
        {

            return ActorEnqueueDelegate(() => { myList.Capacity = value; });

        }

        public ActorTask<int> Count()
        {

            return ActorEnqueueDelegate(() => { return myList.Count; });

        }

        public ActorTask<T> this[int index]
        {

            get
            {

                return ActorEnqueueDelegate(() => { return myList[index]; });

            }

        }

        public ActorTask<T> Get(int index)
        {

            return ActorEnqueueDelegate(() => { return myList[index]; });

        }

        public ActorTask Set(int index, T value)
        {

            return ActorEnqueueDelegate(() => { myList[index] = value; });

        }

        //

        public ActorTask<(T, bool)> TryGet(int index)
        {

            return ActorEnqueueDelegate(() => {

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

            return ActorEnqueueDelegate(() => {

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

            return ActorEnqueueDelegate(() => { myList.Add(item); });

        }

        public ActorTask AddRange(IEnumerable<T> collection)
        {

            return ActorEnqueueDelegate(() => { myList.AddRange(collection); });

        }

        public ActorTask<List<T>> CloneList()
        {

            return ActorEnqueueDelegate(() => new List<T>(myList));

        }

        public ActorTask<int> BinarySearch(int index, int count, T item, IComparer<T> comparer)
        {

            return ActorEnqueueDelegate(() => myList.BinarySearch(index, count, item, comparer));

        }

        public ActorTask<int> BinarySearch(T item)
        {

            return ActorEnqueueDelegate(() => myList.BinarySearch(item));

        }

        public ActorTask<int> BinarySearch(T item, IComparer<T> comparer)
        {

            return ActorEnqueueDelegate(() => myList.BinarySearch(item, comparer));

        }

        public ActorTask Clear()
        {

            return ActorEnqueueDelegate(myList.Clear);

        }

        public ActorTask<bool> Contains(T item)
        {

            return ActorEnqueueDelegate(() => myList.Contains(item));

        }

        public ActorTask<List<TOutput>> ConvertAll<TOutput>(Converter<T, TOutput> converter)
        {

            return ActorEnqueueDelegate(() => myList.ConvertAll(converter));

        }

        public ActorTask CopyTo(int index, T[] array, int arrayIndex, int count)
        {

            return ActorEnqueueDelegate(() => myList.CopyTo(index, array, arrayIndex, count));

        }

        public ActorTask CopyTo(T[] array)
        {

            return ActorEnqueueDelegate(() => myList.CopyTo(array));

        }

        public ActorTask CopyTo(T[] array, int arrayIndex)
        {

            return ActorEnqueueDelegate(() => myList.CopyTo(array, arrayIndex));

        }

        public ActorTask<bool> Exists(Predicate<T> match)
        {

            return ActorEnqueueDelegate(() => myList.Exists(match));

        }

        public ActorTask<T> Find(Predicate<T> match)
        {

            return ActorEnqueueDelegate(() => myList.Find(match));

        }

        public ActorTask<List<T>> FindAll(Predicate<T> match)
        {

            return ActorEnqueueDelegate(() => myList.FindAll(match));

        }

        //Find...

        public ActorTask ForEach(Action<T> action)
        {

            return ActorEnqueueDelegate(() => myList.ForEach(action));

        }

        public ActorTask<List<T>> GetRange(int index, int count)
        {

            return ActorEnqueueDelegate(() => myList.GetRange(index, count));

        }

        public ActorTask<int> IndexOf(T item)
        {

            return ActorEnqueueDelegate(() => myList.IndexOf(item));

        }

        public ActorTask<int> IndexOf(T item, int index)
        {

            return ActorEnqueueDelegate(() => myList.IndexOf(item, index));

        }

        public ActorTask<int> IndexOf(T item, int index, int count)
        {

            return ActorEnqueueDelegate(() => myList.IndexOf(item, index, count));

        }

        public ActorTask Insert(int index, T item)
        {

            return ActorEnqueueDelegate(() => myList.Insert(index, item));

        }

        public ActorTask InsertRange(int index, IEnumerable<T> collection)
        {

            return ActorEnqueueDelegate(() => myList.InsertRange(index, collection));

        }

        public ActorTask<bool> Remove(T item)
        {

            return ActorEnqueueDelegate(() => myList.Remove(item));

        }

        public ActorTask<int> RemoveAll(Predicate<T> match)
        {

            return ActorEnqueueDelegate(() => myList.RemoveAll(match));

        }

        public ActorTask RemoveAt(int index)
        {

            return ActorEnqueueDelegate(() => myList.RemoveAt(index));

        }

        public ActorTask RemoveRange(int index, int count)
        {

            return ActorEnqueueDelegate(() => myList.RemoveRange(index, count));

        }

        public ActorTask Reverse()
        {

            return ActorEnqueueDelegate(myList.Reverse);

        }

        public ActorTask Reverse(int index, int count)
        {

            return ActorEnqueueDelegate(() => myList.Reverse(index, count));

        }

        public ActorTask Sort()
        {

            return ActorEnqueueDelegate(myList.Sort);

        }

        public ActorTask Sort(Comparison<T> comparison)
        {

            return ActorEnqueueDelegate(() => myList.Sort(comparison));

        }

        public ActorTask Sort(IComparer<T> comparer)
        {

            return ActorEnqueueDelegate(() => myList.Sort(comparer));

        }

        public ActorTask Sort(int index, int count, IComparer<T> comparer)
        {

            return ActorEnqueueDelegate(() => myList.Sort(index, count, comparer));

        }

        public ActorTask<T[]> ToArray()
        {

            return ActorEnqueueDelegate(() => myList.ToArray());

        }

        public ActorTask TrimExcess()
        {

            return ActorEnqueueDelegate(myList.TrimExcess);

        }

        public ActorTask<bool> TrueForAll(Predicate<T> match)
        {

            return ActorEnqueueDelegate(() => myList.TrueForAll(match));

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
