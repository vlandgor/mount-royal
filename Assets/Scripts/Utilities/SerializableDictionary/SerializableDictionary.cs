using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

namespace Utilities.SerializableDictionary
{
    public abstract class SerializableDictionaryBase
    {
        public abstract class Storage {}

        protected class Dictionary<TKey, TValue> : System.Collections.Generic.Dictionary<TKey, TValue>
        {
            public Dictionary() {}
            public Dictionary(IDictionary<TKey, TValue> dict) : base(dict) {}
            public Dictionary(SerializationInfo info, StreamingContext context) : base(info, context) {}
        }
    }

    [Serializable]
    public abstract class SerializableDictionaryBase<TKey, TValue, TValueStorage> : SerializableDictionaryBase, IDictionary<TKey, TValue>, IDictionary, ISerializationCallbackReceiver, IDeserializationCallback, ISerializable
    {
        [SerializeField]
        private TKey[] m_keys;
        [SerializeField]
        private TValueStorage[] m_values;
        
        private Dictionary<TKey, TValue> mDict;

        public SerializableDictionaryBase()
        {
            mDict = new Dictionary<TKey, TValue>();
        }

        public SerializableDictionaryBase(IDictionary<TKey, TValue> dict)
        {	
            mDict = new Dictionary<TKey, TValue>(dict);
        }

        protected abstract void SetValue(TValueStorage[] storage, int i, TValue value);
        protected abstract TValue GetValue(TValueStorage[] storage, int i);

        public void CopyFrom(IDictionary<TKey, TValue> dict)
        {
            mDict.Clear();
            foreach (var kvp in dict)
            {
                mDict[kvp.Key] = kvp.Value;
            }
        }

        public void OnAfterDeserialize()
        {
            if(m_keys != null && m_values != null && m_keys.Length == m_values.Length)
            {
                mDict.Clear();
                int n = m_keys.Length;
                for(int i = 0; i < n; ++i)
                {
                    mDict[m_keys[i]] = GetValue(m_values, i);
                }

                m_keys = null;
                m_values = null;
            }
        }

        public void OnBeforeSerialize()
        {
            int n = mDict.Count;
            m_keys = new TKey[n];
            m_values = new TValueStorage[n];

            int i = 0;
            foreach(var kvp in mDict)
            {
                m_keys[i] = kvp.Key;
                SetValue(m_values, i, kvp.Value);
                ++i;
            }
        }

        #region IDictionary<TKey, TValue>
	
        public ICollection<TKey> Keys {	get { return ((IDictionary<TKey, TValue>)mDict).Keys; } }
        public ICollection<TValue> Values { get { return ((IDictionary<TKey, TValue>)mDict).Values; } }
        public int Count { get { return ((IDictionary<TKey, TValue>)mDict).Count; } }
        public bool IsReadOnly { get { return ((IDictionary<TKey, TValue>)mDict).IsReadOnly; } }

        public TValue this[TKey key]
        {
            get { return ((IDictionary<TKey, TValue>)mDict)[key]; }
            set { ((IDictionary<TKey, TValue>)mDict)[key] = value; }
        }

        public void Add(TKey key, TValue value)
        {
            ((IDictionary<TKey, TValue>)mDict).Add(key, value);
        }

        public bool ContainsKey(TKey key)
        {
            return ((IDictionary<TKey, TValue>)mDict).ContainsKey(key);
        }

        public bool Remove(TKey key)
        {
            return ((IDictionary<TKey, TValue>)mDict).Remove(key);
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            return ((IDictionary<TKey, TValue>)mDict).TryGetValue(key, out value);
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            ((IDictionary<TKey, TValue>)mDict).Add(item);
        }

        public void Clear()
        {
            ((IDictionary<TKey, TValue>)mDict).Clear();
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return ((IDictionary<TKey, TValue>)mDict).Contains(item);
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            ((IDictionary<TKey, TValue>)mDict).CopyTo(array, arrayIndex);
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            return ((IDictionary<TKey, TValue>)mDict).Remove(item);
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return ((IDictionary<TKey, TValue>)mDict).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IDictionary<TKey, TValue>)mDict).GetEnumerator();
        }

        #endregion

        #region IDictionary

        public bool IsFixedSize { get { return ((IDictionary)mDict).IsFixedSize; } }
        ICollection IDictionary.Keys { get { return ((IDictionary)mDict).Keys; } }
        ICollection IDictionary.Values { get { return ((IDictionary)mDict).Values; } }
        public bool IsSynchronized { get { return ((IDictionary)mDict).IsSynchronized; } }
        public object SyncRoot { get { return ((IDictionary)mDict).SyncRoot; } }

        public object this[object key]
        {
            get { return ((IDictionary)mDict)[key]; }
            set { ((IDictionary)mDict)[key] = value; }
        }

        public void Add(object key, object value)
        {
            ((IDictionary)mDict).Add(key, value);
        }

        public bool Contains(object key)
        {
            return ((IDictionary)mDict).Contains(key);
        }

        IDictionaryEnumerator IDictionary.GetEnumerator()
        {
            return ((IDictionary)mDict).GetEnumerator();
        }

        public void Remove(object key)
        {
            ((IDictionary)mDict).Remove(key);
        }

        public void CopyTo(Array array, int index)
        {
            ((IDictionary)mDict).CopyTo(array, index);
        }

        #endregion

        #region IDeserializationCallback

        public void OnDeserialization(object sender)
        {
            ((IDeserializationCallback)mDict).OnDeserialization(sender);
        }

        #endregion

        #region ISerializable

        protected SerializableDictionaryBase(SerializationInfo info, StreamingContext context) 
        {
            mDict = new Dictionary<TKey, TValue>(info, context);
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            ((ISerializable)mDict).GetObjectData(info, context);
        }

        #endregion
    }

    public static class SerializableDictionary
    {
        public class Storage<T> : SerializableDictionaryBase.Storage
        {
            public T data;
        }
    }

    [Serializable]
    public class SerializableDictionary<TKey, TValue> : SerializableDictionaryBase<TKey, TValue, TValue>
    {
        public SerializableDictionary() {}
        public SerializableDictionary(IDictionary<TKey, TValue> dict) : base(dict) {}
        protected SerializableDictionary(SerializationInfo info, StreamingContext context) : base(info, context) {}

        protected override TValue GetValue(TValue[] storage, int i)
        {
            return storage[i];
        }

        protected override void SetValue(TValue[] storage, int i, TValue value)
        {
            storage[i] = value;
        }
    }

    [Serializable]
    public class SerializableDictionary<TKey, TValue, TValueStorage> : SerializableDictionaryBase<TKey, TValue, TValueStorage> where TValueStorage : SerializableDictionary.Storage<TValue>, new()
    {
        public SerializableDictionary() {}
        public SerializableDictionary(IDictionary<TKey, TValue> dict) : base(dict) {}
        protected SerializableDictionary(SerializationInfo info, StreamingContext context) : base(info, context) {}

        protected override TValue GetValue(TValueStorage[] storage, int i)
        {
            return storage[i].data;
        }

        protected override void SetValue(TValueStorage[] storage, int i, TValue value)
        {
            storage[i] = new TValueStorage();
            storage[i].data = value;
        }
    }
}