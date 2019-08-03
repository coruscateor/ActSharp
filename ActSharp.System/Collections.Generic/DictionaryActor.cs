using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ActSharp.System.Collections.Generic
{

    public sealed class DictionaryActor<TKey, TValue> : Actor, IEnumerable<ActorTask<(bool, KeyValuePair<TKey, TValue>)>>
    {

        readonly Dictionary<TKey, TValue> myDictionary;

        public DictionaryActor()
        {

            myDictionary = new Dictionary<TKey, TValue>();

        }

        public DictionaryActor(IDictionary<TKey, TValue> dictionary)
        {

            myDictionary = new Dictionary<TKey, TValue>(dictionary);

        }

        public DictionaryActor(IEnumerable<KeyValuePair<TKey, TValue>> collection)
        {

            myDictionary = new Dictionary<TKey, TValue>(collection);

        }

        public DictionaryActor(IEqualityComparer<TKey> comparer)
        {

            myDictionary = new Dictionary<TKey, TValue>(comparer);

        }

        public DictionaryActor(int capacity)
        {

            myDictionary = new Dictionary<TKey, TValue>(capacity);

        }

        public DictionaryActor(IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey> comparer)
        {

            myDictionary = new Dictionary<TKey, TValue>(dictionary, comparer);

        }

        public DictionaryActor(IEnumerable<KeyValuePair<TKey, TValue>> collection, IEqualityComparer<TKey> comparer)
        {

            myDictionary = new Dictionary<TKey, TValue>(collection, comparer);

        }

        public DictionaryActor(int capacity, IEqualityComparer<TKey> comparer)
        {

            myDictionary = new Dictionary<TKey, TValue>(capacity, comparer);

        }

        //public Task<TValue> this[TKey key] {

        //    get
        //    {

        //        return ActorSetupNoPreDelegate(() => {


        //            return myDictionary[key];

        //        });

        //    }
        //    set
        //    {

        //        ActorSetupNoPreDelegate(() => {


        //            myDictionary[key] = value.Result;

        //        });

        //    }
            
        //}

        //public TValue this[TKey key]
        //{

        //    get
        //    {

        //        return ActorSetupNoPreDelegate(() => {


        //            return myDictionary[key];

        //        }).Result;

        //    }
        //    set
        //    {

        //        ActorSetupNoPreDelegate(() => {


        //            myDictionary[key] = value;

        //        }).Wait();

        //    }

        //}

        //Get/Set

        public ActorTask<TValue> Get(TKey key)
        {

            return ActorSetupNoPreDelegate(() => {

                return myDictionary[key];

            });

        }

        public ActorTask Set(TKey key, TValue value)
        {

            return ActorSetupNoPreDelegate(() => {

                myDictionary[key] = value;

            });

        }

        //TryGet

        public ActorTask<(bool, KeyValuePair<TKey, TValue>)> TryGet(TKey key)
        {

            return ActorSetupNoPreDelegate(() => {

                foreach(var item in myDictionary)
                {

                    //if (object.Equals(item.Key, key))
                    if(EqualityComparer<TKey>.Default.Equals(item.Key, key))
                        return (true, item);
                    
                }

                return (false, new KeyValuePair<TKey, TValue>());

            });

        }

        public ActorTask<Dictionary<TKey, TValue>.KeyCollection> Keys
        {

            get
            {

                return ActorSetupNoPreDelegate(() => {

                    return myDictionary.Keys;

                });

            }

        }

        public ActorTask<Dictionary<TKey, TValue>.ValueCollection> Values
        {

            get
            {

                return ActorSetupNoPreDelegate(() => {

                    return myDictionary.Values;

                });

            }

        }

        public ActorTask<IEqualityComparer<TKey>> Comparer
        {

            get
            {

                return ActorSetupNoPreDelegate(() => {

                    return myDictionary.Comparer;

                });

            }

        }

        public ActorTask<int> Count
        {

            get
            {

                return ActorSetupNoPreDelegate(() => {

                    return myDictionary.Count;

                });

            }
            
        }

        public ActorTask Add(TKey key, TValue value)
        {

            return ActorSetupNoPreDelegate(() => {

                myDictionary.Add(key, value);

            });

        }

        public ActorTask Clear()
        {

            return ActorSetupNoPreDelegate(() => {

                myDictionary.Clear();

            });

        }

        public ActorTask<bool> ContainsKey(TKey key)
        {

            return ActorSetupNoPreDelegate(() => {

                return myDictionary.ContainsKey(key);

            });

        }

        public ActorTask<bool> ContainsValue(TValue value)
        {

            return ActorSetupNoPreDelegate(() => {

                return myDictionary.ContainsValue(value);

            });

        }
        public ActorTask<int> EnsureCapacity(int capacity)
        {

            return ActorSetupNoPreDelegate(() => {

                return myDictionary.EnsureCapacity(capacity);

            });

        }

        //public Task<Dictionary<TKey, TValue>.Enumerator> GetEnumerator()
        //{

        //    return ActorSetupNoPreDelegate(() => {

        //        return myDictionary.GetEnumerator();

        //    });

        //}

        public ActorTask GetObjectData(SerializationInfo info, StreamingContext context)
        {

            return ActorSetupNoPreDelegate(() => {

                myDictionary.GetObjectData(info, context);

            });

        }

        public ActorTask OnDeserialization(object sender)
        {

            return ActorSetupNoPreDelegate(() => {

                myDictionary.OnDeserialization(sender);

            });

        }

        public ActorTask<(bool, TValue)> RemoveGetValue(TKey key) //, out TValue value)
        {

            return ActorSetupNoPreDelegate(() => {
                
                TValue value;

                bool result = myDictionary.Remove(key, out value);

                var re = (Result: result, Value: value);

                return re;

            });

        }

        public ActorTask<bool> Remove(TKey key)
        {

            return ActorSetupNoPreDelegate(() => {

                return myDictionary.Remove(key);

            });

        }

        public ActorTask TrimExcess(int capacity)
        {

            return ActorSetupNoPreDelegate(() => {

                myDictionary.TrimExcess(capacity);

            });

        }
        public ActorTask TrimExcess()
        {

            return ActorSetupNoPreDelegate(() => {

                myDictionary.TrimExcess();

            });

        }

        public ActorTask<bool> TryAdd(TKey key, TValue value)
        {

            return ActorSetupNoPreDelegate(() => {

                return myDictionary.TryAdd(key, value);

            });

        }
        
        public ActorTask<(bool, TValue)> TryGetValue(TKey key)
        {

            return ActorSetupNoPreDelegate(() => {
                
                TValue value;

                bool result = myDictionary.TryGetValue(key, out value);

                var re = (Result: result, Value: value);

                return re;

            });

        }

        public ActorTask Operate(Action<DictionaryContainer<TKey, TValue>> action)
        {

            return ActorSetupNoPreDelegate(() => {

                var dcont = new DictionaryContainer<TKey, TValue>(myDictionary);

                using(dcont)
                {

                    action(dcont);

                }

            });

        }

        public ActorTask Enumerate(Action<KeyValuePair<TKey, TValue>> action)
        {

            return ActorSetupNoPreDelegate(() => {

                var dcont = new DictionaryContainer<TKey, TValue>(myDictionary);

                using (dcont)
                {

                    foreach(var item in dcont)
                    {

                        action(item);

                    }

                }

            });

        }
        
        public ActorTask Enumerate(Action<KeyValuePair<TKey, TValue>, int> action)
        {

            return ActorSetupNoPreDelegate(() => {

                var dcont = new DictionaryContainer<TKey, TValue>(myDictionary);

                using (dcont)
                {

                    int i = 0;

                    foreach (var item in dcont)
                    {

                        action(item, i);

                        i++;

                    }

                }

            });

        }

        //IEnumerable<Task<(bool, KeyValuePair<TKey, TValue>)>>.

        public IEnumerator<ActorTask<(bool, KeyValuePair<TKey, TValue>)>> GetEnumerator()
        {

            var keysTask = Keys;

            var keys = keysTask.Result; 

            foreach (var key in keys)
            {

                var kvpTask = TryGet(key);

                yield return kvpTask;

            }

        }

        //Delegate interspersal

        public IEnumerator<ActorTask<(bool, KeyValuePair<TKey, TValue>)>> GetEnumerator(Action beforeKeysTaskResult, Action beforeYeildReturn)
        {

            var keysTask = Keys;

            beforeKeysTaskResult();

            var keys = keysTask.Result;

            foreach (var key in keys)
            {

                var kvpTask = TryGet(key);

                beforeYeildReturn();

                yield return kvpTask;

            }

        }

        //public async Task<IEnumerator<Task<(bool, KeyValuePair<TKey, TValue>)>>> GetEnumeratorAsync()
        //{

        //    var keysTask = Keys;

        //    var keys = await keysTask; //.Result;

        //    foreach (var key in keys)
        //    {

        //        var kvpTask = TryGet(key);

        //        yield return kvpTask;

        //    }

        //}

        IEnumerator IEnumerable.GetEnumerator()
        {

            return GetEnumerator();

        }

    }

}
