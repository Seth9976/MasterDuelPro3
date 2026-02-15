using System;

namespace System.Collections.Generic
{
	// Token: 0x0200075B RID: 1883
	internal class LowLevelDictionary<TKey, TValue>
	{
		// Token: 0x06003C06 RID: 15366 RVA: 0x000E7DC1 File Offset: 0x000E5FC1
		public LowLevelDictionary()
			: this(17, new LowLevelDictionary<TKey, TValue>.DefaultComparer<TKey>())
		{
		}

		// Token: 0x06003C07 RID: 15367 RVA: 0x000E7DD0 File Offset: 0x000E5FD0
		public LowLevelDictionary(int capacity, IEqualityComparer<TKey> comparer)
		{
			this._comparer = comparer;
			this.Clear(capacity);
		}

		// Token: 0x170009C9 RID: 2505
		public TValue this[TKey key]
		{
			set
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				this._version++;
				LowLevelDictionary<TKey, TValue>.Entry entry = this.Find(key);
				if (entry != null)
				{
					entry._value = value;
					return;
				}
				this.UncheckedAdd(key, value);
			}
		}

		// Token: 0x06003C09 RID: 15369 RVA: 0x000E7E32 File Offset: 0x000E6032
		public void Clear(int capacity = 17)
		{
			this._version++;
			this._buckets = new LowLevelDictionary<TKey, TValue>.Entry[capacity];
			this._numEntries = 0;
		}

		// Token: 0x06003C0A RID: 15370 RVA: 0x000E7E58 File Offset: 0x000E6058
		public bool Remove(TKey key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			int bucket = this.GetBucket(key, 0);
			LowLevelDictionary<TKey, TValue>.Entry entry = null;
			for (LowLevelDictionary<TKey, TValue>.Entry entry2 = this._buckets[bucket]; entry2 != null; entry2 = entry2._next)
			{
				if (this._comparer.Equals(key, entry2._key))
				{
					if (entry == null)
					{
						this._buckets[bucket] = entry2._next;
					}
					else
					{
						entry._next = entry2._next;
					}
					this._version++;
					this._numEntries--;
					return true;
				}
				entry = entry2;
			}
			return false;
		}

		// Token: 0x06003C0B RID: 15371 RVA: 0x000E7EEC File Offset: 0x000E60EC
		private LowLevelDictionary<TKey, TValue>.Entry Find(TKey key)
		{
			int bucket = this.GetBucket(key, 0);
			for (LowLevelDictionary<TKey, TValue>.Entry entry = this._buckets[bucket]; entry != null; entry = entry._next)
			{
				if (this._comparer.Equals(key, entry._key))
				{
					return entry;
				}
			}
			return null;
		}

		// Token: 0x06003C0C RID: 15372 RVA: 0x000E7F30 File Offset: 0x000E6130
		private LowLevelDictionary<TKey, TValue>.Entry UncheckedAdd(TKey key, TValue value)
		{
			LowLevelDictionary<TKey, TValue>.Entry entry = new LowLevelDictionary<TKey, TValue>.Entry();
			entry._key = key;
			entry._value = value;
			int bucket = this.GetBucket(key, 0);
			entry._next = this._buckets[bucket];
			this._buckets[bucket] = entry;
			this._numEntries++;
			if (this._numEntries > this._buckets.Length * 2)
			{
				this.ExpandBuckets();
			}
			return entry;
		}

		// Token: 0x06003C0D RID: 15373 RVA: 0x000E7F98 File Offset: 0x000E6198
		private void ExpandBuckets()
		{
			try
			{
				int num = this._buckets.Length * 2 + 1;
				LowLevelDictionary<TKey, TValue>.Entry[] array = new LowLevelDictionary<TKey, TValue>.Entry[num];
				for (int i = 0; i < this._buckets.Length; i++)
				{
					LowLevelDictionary<TKey, TValue>.Entry next;
					for (LowLevelDictionary<TKey, TValue>.Entry entry = this._buckets[i]; entry != null; entry = next)
					{
						next = entry._next;
						int bucket = this.GetBucket(entry._key, num);
						entry._next = array[bucket];
						array[bucket] = entry;
					}
				}
				this._buckets = array;
			}
			catch (OutOfMemoryException)
			{
			}
		}

		// Token: 0x06003C0E RID: 15374 RVA: 0x000E801C File Offset: 0x000E621C
		private int GetBucket(TKey key, int numBuckets = 0)
		{
			return (this._comparer.GetHashCode(key) & int.MaxValue) % ((numBuckets == 0) ? this._buckets.Length : numBuckets);
		}

		// Token: 0x04001F2F RID: 7983
		private LowLevelDictionary<TKey, TValue>.Entry[] _buckets;

		// Token: 0x04001F30 RID: 7984
		private int _numEntries;

		// Token: 0x04001F31 RID: 7985
		private int _version;

		// Token: 0x04001F32 RID: 7986
		private IEqualityComparer<TKey> _comparer;

		// Token: 0x0200075C RID: 1884
		private sealed class Entry
		{
			// Token: 0x04001F33 RID: 7987
			public TKey _key;

			// Token: 0x04001F34 RID: 7988
			public TValue _value;

			// Token: 0x04001F35 RID: 7989
			public LowLevelDictionary<TKey, TValue>.Entry _next;
		}

		// Token: 0x0200075D RID: 1885
		private sealed class DefaultComparer<T> : IEqualityComparer<T>
		{
			// Token: 0x06003C10 RID: 15376 RVA: 0x000E8040 File Offset: 0x000E6240
			public bool Equals(T x, T y)
			{
				if (x == null)
				{
					return y == null;
				}
				IEquatable<T> equatable = x as IEquatable<T>;
				if (equatable != null)
				{
					return equatable.Equals(y);
				}
				return x.Equals(y);
			}

			// Token: 0x06003C11 RID: 15377 RVA: 0x000E8087 File Offset: 0x000E6287
			public int GetHashCode(T obj)
			{
				return obj.GetHashCode();
			}
		}
	}
}
