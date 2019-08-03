using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ActSharp.System.Collections.Generic
{
    public sealed class DictionaryContainer<TKey, TValue> : ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable, IDictionary<TKey, TValue>, IReadOnlyCollection<KeyValuePair<TKey, TValue>>, IReadOnlyDictionary<TKey, TValue>, ICollection, IDictionary, IDeserializationCallback, ISerializable, IDisposable
    {

        Dictionary<TKey, TValue> myDictionary;

        public TValue this[TKey key]
        {

            get
            {

                return myDictionary[key];

            }
            set
            {

                myDictionary[key] = value;

            }

        }

        public Dictionary<TKey, TValue>.KeyCollection Keys
        {

            get
            {

                return myDictionary.Keys;

            }

        }

        public Dictionary<TKey, TValue>.ValueCollection Values
        {

            get
            {

                return myDictionary.Values;

            }

        }

        public IEqualityComparer<TKey> Comparer
        {

            get
            {

                return myDictionary.Comparer;

            }

        }

        public int Count
        {

            get
            {

                return myDictionary.Count;

            }

        }

        //Auto-generated

        public bool IsReadOnly => ((ICollection<KeyValuePair<TKey, TValue>>)myDictionary).IsReadOnly;

        ICollection<TKey> IDictionary<TKey, TValue>.Keys => ((IDictionary<TKey, TValue>)myDictionary).Keys;

        ICollection<TValue> IDictionary<TKey, TValue>.Values => ((IDictionary<TKey, TValue>)myDictionary).Values;

        IEnumerable<TKey> IReadOnlyDictionary<TKey, TValue>.Keys => ((IReadOnlyDictionary<TKey, TValue>)myDictionary).Keys;

        IEnumerable<TValue> IReadOnlyDictionary<TKey, TValue>.Values => ((IReadOnlyDictionary<TKey, TValue>)myDictionary).Values;

        public bool IsSynchronized => ((ICollection)myDictionary).IsSynchronized;

        public object SyncRoot => ((ICollection)myDictionary).SyncRoot;

        public bool IsFixedSize => ((IDictionary)myDictionary).IsFixedSize;

        ICollection IDictionary.Keys => ((IDictionary)myDictionary).Keys;

        ICollection IDictionary.Values => ((IDictionary)myDictionary).Values;

        public object this[object key] { get => ((IDictionary)myDictionary)[key]; set => ((IDictionary)myDictionary)[key] = value; }

        //

        public void Add(TKey key, TValue value)
        {

            myDictionary.Add(key, value);

        }

        public void Clear()
        {

            myDictionary.Clear();

        }

        public bool ContainsKey(TKey key)
        {

            return myDictionary.ContainsKey(key);

        }

        public bool ContainsValue(TValue value)
        {

            return myDictionary.ContainsValue(value);

        }
        public int EnsureCapacity(int capacity)
        {

            return myDictionary.EnsureCapacity(capacity);

        }

        //public Dictionary<TKey, TValue>.Enumerator GetEnumerator()
        //{

        //    return myDictionary.GetEnumerator();

        //}

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {

            myDictionary.GetObjectData(info, context);

        }

        public void OnDeserialization(object sender)
        {

            myDictionary.OnDeserialization(sender);

        }

        public bool Remove(TKey key, out TValue value)
        {

            return myDictionary.Remove(key, out value);

        }

        public bool Remove(TKey key)
        {

            return myDictionary.Remove(key);

        }

        public void TrimExcess(int capacity)
        {

            myDictionary.TrimExcess(capacity);

        }

        public void TrimExcess()
        {

            myDictionary.TrimExcess();

        }

        public bool TryAdd(TKey key, TValue value)
        {

            return myDictionary.TryAdd(key, value);

        }

        public bool TryGetValue(TKey key, out TValue value)
        {

            return myDictionary.TryGetValue(key, out value);

        }

        public DictionaryContainer(Dictionary<TKey, TValue> dictionary)
        {

            myDictionary = dictionary;

        }

        public void Dispose()
        {

            myDictionary = null;

        }

        //Auto-generated

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            ((ICollection<KeyValuePair<TKey, TValue>>)myDictionary).Add(item);
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return ((ICollection<KeyValuePair<TKey, TValue>>)myDictionary).Contains(item);
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            ((ICollection<KeyValuePair<TKey, TValue>>)myDictionary).CopyTo(array, arrayIndex);
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            return ((ICollection<KeyValuePair<TKey, TValue>>)myDictionary).Remove(item);
        }

        IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
        {
            return ((ICollection<KeyValuePair<TKey, TValue>>)myDictionary).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((ICollection<KeyValuePair<TKey, TValue>>)myDictionary).GetEnumerator();
        }

        public void CopyTo(Array array, int index)
        {
            ((ICollection)myDictionary).CopyTo(array, index);
        }

        public void Add(object key, object value)
        {
            ((IDictionary)myDictionary).Add(key, value);
        }

        public bool Contains(object key)
        {
            return ((IDictionary)myDictionary).Contains(key);
        }

        IDictionaryEnumerator IDictionary.GetEnumerator()
        {
            return ((IDictionary)myDictionary).GetEnumerator();
        }

        public void Remove(object key)
        {
            ((IDictionary)myDictionary).Remove(key);
        }

        //IEnumerator IEnumerable.GetEnumerator()
        //{
        //    throw new NotImplementedException();
        //}

        //

    }

}
