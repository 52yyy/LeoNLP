using System;
using System.Collections;

namespace LDA
{
	[Serializable]
	public class BiMap<K, V>
	{
		[Serializable]
		private class Entry
		{
			private K _key;

			private V _value;

			public Entry(K key, V value)
			{
				this._key = key;
				this._value = value;
			}

			public K GetK()
			{
				return this._key;
			}

			public V GetV()
			{
				return this._value;
			}

			public void SetK(K k)
			{
				this._key = k;
			}

			public void SetV(V v)
			{
				this._value = v;
			}
		}

		private readonly Hashtable _kEntyTable = new Hashtable();

		private readonly Hashtable _vEntyTable = new Hashtable();

		public Boolean ContainsKey(K k)
		{
			return _kEntyTable.ContainsKey(k);
		}

		public Boolean ContainsValue(V v)
		{
			return _vEntyTable.ContainsKey(v);
		}

		public V GetValue(K k)
		{
			var entry = (Entry)_kEntyTable[k];
			if (entry == null)
			{
				throw new ArgumentException("key cannot be null");
			}
			return entry.GetV();
		}

		public object GetKey(V v)
		{
			var entry = (Entry)_vEntyTable[v];
			if (entry == null)
			{
				//throw new ArgumentException("value cannot be null");
				Console.WriteLine("value cannot be null");
				return null;
			}
			return entry.GetK();
		}

		public Boolean Add(K k, V v)
		{
			if (k == null || v == null)
			{
				return false;
			}
			var e = new Entry(k, v);
			if (ContainsKey(k))
			{
				Remove(k);
			}
			if (ContainsValue(v))
			{
				RemoveByValue(v);
			}
			_kEntyTable.Add(k, e);
			_vEntyTable.Add(v, e);
			return true;
		}

		public void Remove(K k)
		{
			var e = (Entry)_kEntyTable[k];
			_kEntyTable.Remove(k);
			if (e == null)
			{
				throw new ArgumentException();
			}
			_vEntyTable.Remove(e.GetV());
		}

		public object RemoveByValue(V v)
		{
			var e = (Entry)_vEntyTable[v];
			_vEntyTable.Remove(v);
			if (e == null)
			{
				return null;
			}
			_vEntyTable.Remove(e.GetK());
			return e.GetK();
		}

		public int Count()
		{
			return _kEntyTable.Count;
		}
	}
}
