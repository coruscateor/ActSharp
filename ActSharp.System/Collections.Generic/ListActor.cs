using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ActSharp.System.Collections.Generic
{

    public sealed class ListActor<T> : Actor, IEnumerable<ActorTask<(T, bool)>>
    {

        readonly List<T> myList;

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

            return ActorSetupNoPreDelegate(() => { return myList.Capacity; });

        }

        public ActorTask Capacity(int value)
        {

            return ActorSetupNoPreDelegate(() => { myList.Capacity = value; });

        }

        public ActorTask<int> Count()
        {

            return ActorSetupNoPreDelegate(() => { return myList.Count; });

        }

        public ActorTask<T> this[int index]
        {

            get
            {

                return ActorSetupNoPreDelegate(() => { return myList[index]; });

            }

        }

        public ActorTask<T> Get(int index)
        {

            return ActorSetupNoPreDelegate(() => { return myList[index]; });

        }

        public ActorTask Set(int index, T value)
        {

            return ActorSetupNoPreDelegate(() => { myList[index] = value; });

        }

        //

        public ActorTask<(T, bool)> TryGet(int index)
        {

            return ActorSetupNoPreDelegate(() => {

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

            return ActorSetupNoPreDelegate(() => {

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

            return ActorSetupNoPreDelegate(() => { myList.Add(item); });

        }

        public ActorTask AddRange(IEnumerable<T> collection)
        {

            return ActorSetupNoPreDelegate(() => { myList.AddRange(collection); });

        }

        public ActorTask<List<T>> CloneList()
        {

            return ActorSetupNoPreDelegate(() => new List<T>(myList));

        }

        public ActorTask<int> BinarySearch(int index, int count, T item, IComparer<T> comparer)
        {

            return ActorSetupNoPreDelegate(() => myList.BinarySearch(index, count, item, comparer));

        }

        public ActorTask<int> BinarySearch(T item)
        {

            return ActorSetupNoPreDelegate(() => myList.BinarySearch(item));

        }

        public ActorTask<int> BinarySearch(T item, IComparer<T> comparer)
        {

            return ActorSetupNoPreDelegate(() => myList.BinarySearch(item, comparer));

        }

        public ActorTask Clear()
        {

            return ActorSetupNoPreDelegate(myList.Clear);

        }

        public ActorTask<bool> Contains(T item)
        {

            return ActorSetupNoPreDelegate(() => myList.Contains(item));

        }

        public ActorTask<List<TOutput>> ConvertAll<TOutput>(Converter<T, TOutput> converter)
        {

            return ActorSetupNoPreDelegate(() => myList.ConvertAll(converter));

        }

        public ActorTask CopyTo(int index, T[] array, int arrayIndex, int count)
        {

            return ActorSetupNoPreDelegate(() => myList.CopyTo(index, array, arrayIndex, count));

        }

        public ActorTask CopyTo(T[] array)
        {

            return ActorSetupNoPreDelegate(() => myList.CopyTo(array));

        }

        public ActorTask CopyTo(T[] array, int arrayIndex)
        {

            return ActorSetupNoPreDelegate(() => myList.CopyTo(array, arrayIndex));

        }

        public ActorTask<bool> Exists(Predicate<T> match)
        {

            return ActorSetupNoPreDelegate(() => myList.Exists(match));

        }

        public ActorTask<T> Find(Predicate<T> match)
        {

            return ActorSetupNoPreDelegate(() => myList.Find(match));

        }

        public ActorTask<List<T>> FindAll(Predicate<T> match)
        {

            return ActorSetupNoPreDelegate(() => myList.FindAll(match));

        }

        //Find...

        public ActorTask ForEach(Action<T> action)
        {

            return ActorSetupNoPreDelegate(() => myList.ForEach(action));

        }

        public ActorTask<List<T>> GetRange(int index, int count)
        {

            return ActorSetupNoPreDelegate(() => myList.GetRange(index, count));

        }

        public ActorTask<int> IndexOf(T item)
        {

            return ActorSetupNoPreDelegate(() => myList.IndexOf(item));

        }

        public ActorTask<int> IndexOf(T item, int index)
        {

            return ActorSetupNoPreDelegate(() => myList.IndexOf(item, index));

        }

        public ActorTask<int> IndexOf(T item, int index, int count)
        {

            return ActorSetupNoPreDelegate(() => myList.IndexOf(item, index, count));

        }

        public ActorTask Insert(int index, T item)
        {

            return ActorSetupNoPreDelegate(() => myList.Insert(index, item));

        }

        public ActorTask InsertRange(int index, IEnumerable<T> collection)
        {

            return ActorSetupNoPreDelegate(() => myList.InsertRange(index, collection));

        }

        public ActorTask<bool> Remove(T item)
        {

            return ActorSetupNoPreDelegate(() => myList.Remove(item));

        }

        public ActorTask<int> RemoveAll(Predicate<T> match)
        {

            return ActorSetupNoPreDelegate(() => myList.RemoveAll(match));

        }

        public ActorTask RemoveAt(int index)
        {

            return ActorSetupNoPreDelegate(() => myList.RemoveAt(index));

        }

        public ActorTask RemoveRange(int index, int count)
        {

            return ActorSetupNoPreDelegate(() => myList.RemoveRange(index, count));

        }

        public ActorTask Reverse()
        {

            return ActorSetupNoPreDelegate(myList.Reverse);

        }

        public ActorTask Reverse(int index, int count)
        {

            return ActorSetupNoPreDelegate(() => myList.Reverse(index, count));

        }

        public ActorTask Sort()
        {

            return ActorSetupNoPreDelegate(myList.Sort);

        }

        public ActorTask Sort(Comparison<T> comparison)
        {

            return ActorSetupNoPreDelegate(() => myList.Sort(comparison));

        }

        public ActorTask Sort(IComparer<T> comparer)
        {

            return ActorSetupNoPreDelegate(() => myList.Sort(comparer));

        }

        public ActorTask Sort(int index, int count, IComparer<T> comparer)
        {

            return ActorSetupNoPreDelegate(() => myList.Sort(index, count, comparer));

        }

        public ActorTask<T[]> ToArray()
        {

            return ActorSetupNoPreDelegate(myList.ToArray);

        }

        public ActorTask TrimExcess()
        {

            return ActorSetupNoPreDelegate(myList.TrimExcess);

        }

        public ActorTask<bool> TrueForAll(Predicate<T> match)
        {

            return ActorSetupNoPreDelegate(() => myList.TrueForAll(match));

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
