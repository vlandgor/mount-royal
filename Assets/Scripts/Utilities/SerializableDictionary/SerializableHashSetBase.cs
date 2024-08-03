using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

namespace Utilities.SerializableDictionary
{
	public abstract class SerializableHashSetBase
	{
		public abstract class Storage {}

		protected class HashSet<TValue> : System.Collections.Generic.HashSet<TValue>
		{
			public HashSet() {}
			public HashSet(ISet<TValue> set) : base(set) {}
			public HashSet(SerializationInfo info, StreamingContext context) : base(info, context) {}
		}
	}

	[Serializable]
	public abstract class SerializableHashSet<T> : SerializableHashSetBase, ISet<T>, ISerializationCallbackReceiver, IDeserializationCallback, ISerializable
	{
		[SerializeField]
		private T[] m_keys;

		private HashSet<T> mHashSet;
	
		public SerializableHashSet()
		{
			mHashSet = new HashSet<T>();
		}

		public SerializableHashSet(ISet<T> set)
		{	
			mHashSet = new HashSet<T>(set);
		}

		public void CopyFrom(ISet<T> set)
		{
			mHashSet.Clear();
			foreach (var value in set)
			{
				mHashSet.Add(value);
			}
		}

		public void OnAfterDeserialize()
		{
			if(m_keys != null)
			{
				mHashSet.Clear();
				int n = m_keys.Length;
				for(int i = 0; i < n; ++i)
				{
					mHashSet.Add(m_keys[i]);
				}

				m_keys = null;
			}
		}

		public void OnBeforeSerialize()
		{
			int n = mHashSet.Count;
			m_keys = new T[n];

			int i = 0;
			foreach(var value in mHashSet)
			{
				m_keys[i] = value;
				++i;
			}
		}

		#region ISet<TValue>

		public int Count { get { return ((ISet<T>)mHashSet).Count; } }
		public bool IsReadOnly { get { return  ((ISet<T>)mHashSet).IsReadOnly; } }

		public bool Add(T item)
		{
			return ((ISet<T>)mHashSet).Add(item);
		}

		public void ExceptWith(IEnumerable<T> other)
		{
			((ISet<T>)mHashSet).ExceptWith(other);
		}

		public void IntersectWith(IEnumerable<T> other)
		{
			((ISet<T>)mHashSet).IntersectWith(other);
		}

		public bool IsProperSubsetOf(IEnumerable<T> other)
		{
			return ((ISet<T>)mHashSet).IsProperSubsetOf(other);
		}

		public bool IsProperSupersetOf(IEnumerable<T> other)
		{
			return ((ISet<T>)mHashSet).IsProperSupersetOf(other);
		}

		public bool IsSubsetOf(IEnumerable<T> other)
		{
			return ((ISet<T>)mHashSet).IsSubsetOf(other);
		}

		public bool IsSupersetOf(IEnumerable<T> other)
		{
			return ((ISet<T>)mHashSet).IsSupersetOf(other);
		}

		public bool Overlaps(IEnumerable<T> other)
		{
			return ((ISet<T>)mHashSet).Overlaps(other);
		}

		public bool SetEquals(IEnumerable<T> other)
		{
			return ((ISet<T>)mHashSet).SetEquals(other);
		}

		public void SymmetricExceptWith(IEnumerable<T> other)
		{
			((ISet<T>)mHashSet).SymmetricExceptWith(other);
		}

		public void UnionWith(IEnumerable<T> other)
		{
			((ISet<T>)mHashSet).UnionWith(other);
		}

		void ICollection<T>.Add(T item)
		{
			((ISet<T>)mHashSet).Add(item);
		}

		public void Clear()
		{
			((ISet<T>)mHashSet).Clear();
		}

		public bool Contains(T item)
		{
			return ((ISet<T>)mHashSet).Contains(item);
		}

		public void CopyTo(T[] array, int arrayIndex)
		{
			((ISet<T>)mHashSet).CopyTo(array, arrayIndex);
		}

		public bool Remove(T item)
		{
			return ((ISet<T>)mHashSet).Remove(item);
		}

		public IEnumerator<T> GetEnumerator()
		{
			return ((ISet<T>)mHashSet).GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((ISet<T>)mHashSet).GetEnumerator();
		}

		#endregion

		#region IDeserializationCallback

		public void OnDeserialization(object sender)
		{
			((IDeserializationCallback)mHashSet).OnDeserialization(sender);
		}

		#endregion

		#region ISerializable

		protected SerializableHashSet(SerializationInfo info, StreamingContext context) 
		{
			mHashSet = new HashSet<T>(info, context);
		}

		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			((ISerializable)mHashSet).GetObjectData(info, context);
		}

		#endregion
	}
}