using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ActSharp.System.Collections.Generic
{

    public sealed class ListActor<T> : Actor, IEnumerable<Task<(T, bool)>>
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

        public Task<int> Capacity()
        {

            return ActorEnqueueNoTaskListCheck(() => { return myList.Capacity; });

        }

        public Task Capacity(int value)
        {

            return ActorEnqueueNoTaskListCheck(() => { myList.Capacity = value; });

        }

        public Task<int> Count()
        {

            return ActorEnqueueNoTaskListCheck(() => { return myList.Count; });

        }

        public Task<T> this[int index]
        {

            get
            {

                return ActorEnqueueNoTaskListCheck(() => { return myList[index]; });

            }

        }

        public Task<T> Get(int index)
        {

            return ActorEnqueueNoTaskListCheck(() => { return myList[index]; });

        }

        public Task Set(int index, T value)
        {

            return ActorEnqueueNoTaskListCheck(() => { myList[index] = value; });

        }

        //

        public Task<(T, bool)> TryGet(int index)
        {

            return ActorEnqueueNoTaskListCheck(() => {

                var result = (Result: default(T), Found: false);

                if (index > -1 && index < myList.Count)
                {

                    result.Result = myList[index];

                    result.Found = true;

                }

                return result;

            });

        }

        public Task<bool> TrySet(int index, T value)
        {

            return ActorEnqueueNoTaskListCheck(() => {

                if (index > -1 && index < myList.Count)
                {

                    myList[index] = value;

                    return true;

                }

                return false;

            });

        }

        //

        public Task Add(T item)
        {

            return ActorEnqueueNoTaskListCheck(() => { myList.Add(item); });

        }

        public Task AddRange(IEnumerable<T> collection)
        {

            return ActorEnqueueNoTaskListCheck(() => { myList.AddRange(collection); });

        }

        public Task<List<T>> CloneList()
        {

            return ActorEnqueueNoTaskListCheck(() => new List<T>(myList));

        }

        public Task<int> BinarySearch(int index, int count, T item, IComparer<T> comparer)
        {

            return ActorEnqueueNoTaskListCheck(() => myList.BinarySearch(index, count, item, comparer));

        }

        public Task<int> BinarySearch(T item)
        {

            return ActorEnqueueNoTaskListCheck(() => myList.BinarySearch(item));

        }

        public Task<int> BinarySearch(T item, IComparer<T> comparer)
        {

            return ActorEnqueueNoTaskListCheck(() => myList.BinarySearch(item, comparer));

        }

        public Task Clear()
        {

            return ActorEnqueueNoTaskListCheck(myList.Clear);

        }

        public Task<bool> Contains(T item)
        {

            return ActorEnqueueNoTaskListCheck(() => myList.Contains(item));

        }

        public Task<List<TOutput>> ConvertAll<TOutput>(Converter<T, TOutput> converter)
        {

            return ActorEnqueueNoTaskListCheck(() => myList.ConvertAll(converter));

        }

        public Task CopyTo(int index, T[] array, int arrayIndex, int count)
        {

            return ActorEnqueueNoTaskListCheck(() => myList.CopyTo(index, array, arrayIndex, count));

        }

        public Task CopyTo(T[] array)
        {

            return ActorEnqueueNoTaskListCheck(() => myList.CopyTo(array));

        }

        public Task CopyTo(T[] array, int arrayIndex)
        {

            return ActorEnqueueNoTaskListCheck(() => myList.CopyTo(array, arrayIndex));

        }

        public Task<bool> Exists(Predicate<T> match)
        {

            return ActorEnqueueNoTaskListCheck(() => myList.Exists(match));

        }

        public Task<T> Find(Predicate<T> match)
        {

            return ActorEnqueueNoTaskListCheck(() => myList.Find(match));

        }

        public Task<List<T>> FindAll(Predicate<T> match)
        {

            return ActorEnqueueNoTaskListCheck(() => myList.FindAll(match));

        }

        //Find...

        public Task ForEach(Action<T> action)
        {

            return ActorEnqueueNoTaskListCheck(() => myList.ForEach(action));

        }

        public Task<List<T>> GetRange(int index, int count)
        {

            return ActorEnqueueNoTaskListCheck(() => myList.GetRange(index, count));

        }

        public Task<int> IndexOf(T item)
        {

            return ActorEnqueueNoTaskListCheck(() => myList.IndexOf(item));

        }

        public Task<int> IndexOf(T item, int index)
        {

            return ActorEnqueueNoTaskListCheck(() => myList.IndexOf(item, index));

        }

        public Task<int> IndexOf(T item, int index, int count)
        {

            return ActorEnqueueNoTaskListCheck(() => myList.IndexOf(item, index, count));

        }

        public Task Insert(int index, T item)
        {

            return ActorEnqueueNoTaskListCheck(() => myList.Insert(index, item));

        }

        public Task InsertRange(int index, IEnumerable<T> collection)
        {

            return ActorEnqueueNoTaskListCheck(() => myList.InsertRange(index, collection));

        }

        public Task<bool> Remove(T item)
        {

            return ActorEnqueueNoTaskListCheck(() => myList.Remove(item));

        }

        public Task<int> RemoveAll(Predicate<T> match)
        {

            return ActorEnqueueNoTaskListCheck(() => myList.RemoveAll(match));

        }

        public Task RemoveAt(int index)
        {

            return ActorEnqueueNoTaskListCheck(() => myList.RemoveAt(index));

        }

        public Task RemoveRange(int index, int count)
        {

            return ActorEnqueueNoTaskListCheck(() => myList.RemoveRange(index, count));

        }

        public Task Reverse()
        {

            return ActorEnqueueNoTaskListCheck(myList.Reverse);

        }

        public Task Reverse(int index, int count)
        {

            return ActorEnqueueNoTaskListCheck(() => myList.Reverse(index, count));

        }

        public Task Sort()
        {

            return ActorEnqueueNoTaskListCheck(myList.Sort);

        }

        public Task Sort(Comparison<T> comparison)
        {

            return ActorEnqueueNoTaskListCheck(() => myList.Sort(comparison));

        }

        public Task Sort(IComparer<T> comparer)
        {

            return ActorEnqueueNoTaskListCheck(() => myList.Sort(comparer));

        }

        public Task Sort(int index, int count, IComparer<T> comparer)
        {

            return ActorEnqueueNoTaskListCheck(() => myList.Sort(index, count, comparer));

        }

        public Task<T[]> ToArray()
        {

            return ActorEnqueueNoTaskListCheck(() => myList.ToArray());

        }

        public Task TrimExcess()
        {

            return ActorEnqueueNoTaskListCheck(myList.TrimExcess);

        }

        public Task<bool> TrueForAll(Predicate<T> match)
        {

            return ActorEnqueueNoTaskListCheck(() => myList.TrueForAll(match));

        }

        public IEnumerator<Task<(T, bool)>> GetEnumerator()
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
