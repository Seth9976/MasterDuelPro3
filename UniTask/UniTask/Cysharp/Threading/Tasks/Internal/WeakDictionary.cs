using System;
using System.Collections.Generic;
using System.Threading;

namespace Cysharp.Threading.Tasks.Internal
{
	// Token: 0x02000251 RID: 593
	internal class WeakDictionary<TKey, TValue> where TKey : class
	{
		// Token: 0x06000D3D RID: 3389 RVA: 0x0002DF88 File Offset: 0x0002C188
		public WeakDictionary(int capacity = 4, float loadFactor = 0.75f, IEqualityComparer<TKey> keyComparer = null)
		{
			int tableSize = WeakDictionary<TKey, TValue>.CalculateCapacity(capacity, loadFactor);
			this.buckets = new WeakDictionary<TKey, TValue>.Entry[tableSize];
			this.loadFactor = loadFactor;
			this.gate = new SpinLock(false);
			this.keyEqualityComparer = keyComparer ?? EqualityComparer<TKey>.Default;
		}

		// Token: 0x06000D3E RID: 3390 RVA: 0x0002DFD4 File Offset: 0x0002C1D4
		public bool TryAdd(TKey key, TValue value)
		{
			bool lockTaken = false;
			bool flag;
			try
			{
				this.gate.Enter(ref lockTaken);
				flag = this.TryAddInternal(key, value);
			}
			finally
			{
				if (lockTaken)
				{
					this.gate.Exit(false);
				}
			}
			return flag;
		}

		// Token: 0x06000D3F RID: 3391 RVA: 0x0002E01C File Offset: 0x0002C21C
		public bool TryGetValue(TKey key, out TValue value)
		{
			bool lockTaken = false;
			bool flag;
			try
			{
				this.gate.Enter(ref lockTaken);
				int num;
				WeakDictionary<TKey, TValue>.Entry entry;
				if (this.TryGetEntry(key, out num, out entry))
				{
					value = entry.Value;
					flag = true;
				}
				else
				{
					value = default(TValue);
					flag = false;
				}
			}
			finally
			{
				if (lockTaken)
				{
					this.gate.Exit(false);
				}
			}
			return flag;
		}

		// Token: 0x06000D40 RID: 3392 RVA: 0x0002E084 File Offset: 0x0002C284
		public bool TryRemove(TKey key)
		{
			bool lockTaken = false;
			bool flag;
			try
			{
				this.gate.Enter(ref lockTaken);
				int hashIndex;
				WeakDictionary<TKey, TValue>.Entry entry;
				if (this.TryGetEntry(key, out hashIndex, out entry))
				{
					this.Remove(hashIndex, entry);
					flag = true;
				}
				else
				{
					flag = false;
				}
			}
			finally
			{
				if (lockTaken)
				{
					this.gate.Exit(false);
				}
			}
			return flag;
		}

		// Token: 0x06000D41 RID: 3393 RVA: 0x0002E0E0 File Offset: 0x0002C2E0
		private bool TryAddInternal(TKey key, TValue value)
		{
			int nextCapacity = WeakDictionary<TKey, TValue>.CalculateCapacity(this.size + 1, this.loadFactor);
			while (this.buckets.Length < nextCapacity)
			{
				WeakDictionary<TKey, TValue>.Entry[] nextBucket = new WeakDictionary<TKey, TValue>.Entry[nextCapacity];
				for (int i = 0; i < this.buckets.Length; i++)
				{
					for (WeakDictionary<TKey, TValue>.Entry e = this.buckets[i]; e != null; e = e.Next)
					{
						this.AddToBuckets(nextBucket, key, e.Value, e.Hash);
					}
				}
				this.buckets = nextBucket;
			}
			bool flag = this.AddToBuckets(this.buckets, key, value, this.keyEqualityComparer.GetHashCode(key));
			if (flag)
			{
				this.size++;
			}
			return flag;
		}

		// Token: 0x06000D42 RID: 3394 RVA: 0x0002E184 File Offset: 0x0002C384
		private bool AddToBuckets(WeakDictionary<TKey, TValue>.Entry[] targetBuckets, TKey newKey, TValue value, int keyHash)
		{
			int hashIndex = keyHash & (targetBuckets.Length - 1);
			IL_000B:
			while (targetBuckets[hashIndex] != null)
			{
				WeakDictionary<TKey, TValue>.Entry entry = targetBuckets[hashIndex];
				while (entry != null)
				{
					TKey target;
					if (entry.Key.TryGetTarget(out target))
					{
						if (this.keyEqualityComparer.Equals(newKey, target))
						{
							return false;
						}
					}
					else
					{
						this.Remove(hashIndex, entry);
						if (targetBuckets[hashIndex] == null)
						{
							goto IL_000B;
						}
					}
					if (entry.Next != null)
					{
						entry = entry.Next;
					}
					else
					{
						entry.Next = new WeakDictionary<TKey, TValue>.Entry
						{
							Key = new WeakReference<TKey>(newKey, false),
							Value = value,
							Hash = keyHash
						};
						entry.Next.Prev = entry;
					}
				}
				return false;
			}
			targetBuckets[hashIndex] = new WeakDictionary<TKey, TValue>.Entry
			{
				Key = new WeakReference<TKey>(newKey, false),
				Value = value,
				Hash = keyHash
			};
			return true;
		}

		// Token: 0x06000D43 RID: 3395 RVA: 0x0002E240 File Offset: 0x0002C440
		private bool TryGetEntry(TKey key, out int hashIndex, out WeakDictionary<TKey, TValue>.Entry entry)
		{
			WeakDictionary<TKey, TValue>.Entry[] table = this.buckets;
			int hash = this.keyEqualityComparer.GetHashCode(key);
			hashIndex = hash & (table.Length - 1);
			for (entry = table[hashIndex]; entry != null; entry = entry.Next)
			{
				TKey target;
				if (entry.Key.TryGetTarget(out target))
				{
					if (this.keyEqualityComparer.Equals(key, target))
					{
						return true;
					}
				}
				else
				{
					this.Remove(hashIndex, entry);
				}
			}
			return false;
		}

		// Token: 0x06000D44 RID: 3396 RVA: 0x0002E2AC File Offset: 0x0002C4AC
		private void Remove(int hashIndex, WeakDictionary<TKey, TValue>.Entry entry)
		{
			if (entry.Prev == null && entry.Next == null)
			{
				this.buckets[hashIndex] = null;
			}
			else
			{
				if (entry.Prev == null)
				{
					this.buckets[hashIndex] = entry.Next;
				}
				if (entry.Prev != null)
				{
					entry.Prev.Next = entry.Next;
				}
				if (entry.Next != null)
				{
					entry.Next.Prev = entry.Prev;
				}
			}
			this.size--;
		}

		// Token: 0x06000D45 RID: 3397 RVA: 0x0002E32C File Offset: 0x0002C52C
		public List<KeyValuePair<TKey, TValue>> ToList()
		{
			List<KeyValuePair<TKey, TValue>> list = new List<KeyValuePair<TKey, TValue>>(this.size);
			this.ToList(ref list, false);
			return list;
		}

		// Token: 0x06000D46 RID: 3398 RVA: 0x0002E350 File Offset: 0x0002C550
		public int ToList(ref List<KeyValuePair<TKey, TValue>> list, bool clear = true)
		{
			if (clear)
			{
				list.Clear();
			}
			int listIndex = 0;
			bool lockTaken = false;
			try
			{
				for (int i = 0; i < this.buckets.Length; i++)
				{
					for (WeakDictionary<TKey, TValue>.Entry entry = this.buckets[i]; entry != null; entry = entry.Next)
					{
						TKey target;
						if (entry.Key.TryGetTarget(out target))
						{
							KeyValuePair<TKey, TValue> item = new KeyValuePair<TKey, TValue>(target, entry.Value);
							if (listIndex < list.Count)
							{
								list[listIndex++] = item;
							}
							else
							{
								list.Add(item);
								listIndex++;
							}
						}
						else
						{
							this.Remove(i, entry);
						}
					}
				}
			}
			finally
			{
				if (lockTaken)
				{
					this.gate.Exit(false);
				}
			}
			return listIndex;
		}

		// Token: 0x06000D47 RID: 3399 RVA: 0x0002E408 File Offset: 0x0002C608
		private static int CalculateCapacity(int collectionSize, float loadFactor)
		{
			int size = (int)((float)collectionSize / loadFactor);
			size--;
			size |= size >> 1;
			size |= size >> 2;
			size |= size >> 4;
			size |= size >> 8;
			size |= size >> 16;
			size++;
			if (size < 8)
			{
				size = 8;
			}
			return size;
		}

		// Token: 0x040006AB RID: 1707
		private WeakDictionary<TKey, TValue>.Entry[] buckets;

		// Token: 0x040006AC RID: 1708
		private int size;

		// Token: 0x040006AD RID: 1709
		private SpinLock gate;

		// Token: 0x040006AE RID: 1710
		private readonly float loadFactor;

		// Token: 0x040006AF RID: 1711
		private readonly IEqualityComparer<TKey> keyEqualityComparer;

		// Token: 0x02000252 RID: 594
		private class Entry
		{
			// Token: 0x06000D48 RID: 3400 RVA: 0x0002E44C File Offset: 0x0002C64C
			public override string ToString()
			{
				TKey target;
				if (this.Key.TryGetTarget(out target))
				{
					TKey tkey = target;
					return ((tkey != null) ? tkey.ToString() : null) + "(" + this.Count().ToString() + ")";
				}
				return "(Dead)";
			}

			// Token: 0x06000D49 RID: 3401 RVA: 0x0002E4A0 File Offset: 0x0002C6A0
			private int Count()
			{
				int count = 1;
				WeakDictionary<TKey, TValue>.Entry i = this;
				while (i.Next != null)
				{
					count++;
					i = i.Next;
				}
				return count;
			}

			// Token: 0x040006B0 RID: 1712
			public WeakReference<TKey> Key;

			// Token: 0x040006B1 RID: 1713
			public TValue Value;

			// Token: 0x040006B2 RID: 1714
			public int Hash;

			// Token: 0x040006B3 RID: 1715
			public WeakDictionary<TKey, TValue>.Entry Prev;

			// Token: 0x040006B4 RID: 1716
			public WeakDictionary<TKey, TValue>.Entry Next;
		}
	}
}
