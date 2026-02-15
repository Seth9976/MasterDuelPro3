using System;
using System.Threading;

namespace System.Dynamic.Utils
{
	// Token: 0x02000143 RID: 323
	internal sealed class CacheDict<TKey, TValue>
	{
		// Token: 0x06000AAD RID: 2733 RVA: 0x0002A548 File Offset: 0x00028748
		internal CacheDict(int size)
		{
			int num = CacheDict<TKey, TValue>.AlignSize(size);
			this._mask = num - 1;
			this._entries = new CacheDict<TKey, TValue>.Entry[num];
		}

		// Token: 0x06000AAE RID: 2734 RVA: 0x0002A577 File Offset: 0x00028777
		private static int AlignSize(int size)
		{
			size--;
			size |= size >> 1;
			size |= size >> 2;
			size |= size >> 4;
			size |= size >> 8;
			size |= size >> 16;
			size++;
			return size;
		}

		// Token: 0x06000AAF RID: 2735 RVA: 0x0002A5A8 File Offset: 0x000287A8
		internal bool TryGetValue(TKey key, out TValue value)
		{
			int hashCode = key.GetHashCode();
			int num = hashCode & this._mask;
			CacheDict<TKey, TValue>.Entry entry = Volatile.Read<CacheDict<TKey, TValue>.Entry>(ref this._entries[num]);
			if (entry != null && entry._hash == hashCode)
			{
				TKey key2 = entry._key;
				if (key2.Equals(key))
				{
					value = entry._value;
					return true;
				}
			}
			value = default(TValue);
			return false;
		}

		// Token: 0x06000AB0 RID: 2736 RVA: 0x0002A61C File Offset: 0x0002881C
		internal void Add(TKey key, TValue value)
		{
			int hashCode = key.GetHashCode();
			int num = hashCode & this._mask;
			CacheDict<TKey, TValue>.Entry entry = Volatile.Read<CacheDict<TKey, TValue>.Entry>(ref this._entries[num]);
			if (entry != null && entry._hash == hashCode)
			{
				TKey key2 = entry._key;
				if (key2.Equals(key))
				{
					return;
				}
			}
			Volatile.Write<CacheDict<TKey, TValue>.Entry>(ref this._entries[num], new CacheDict<TKey, TValue>.Entry(hashCode, key, value));
		}

		// Token: 0x170001CB RID: 459
		internal TValue this[TKey key]
		{
			set
			{
				this.Add(key, value);
			}
		}

		// Token: 0x04000349 RID: 841
		private readonly int _mask;

		// Token: 0x0400034A RID: 842
		private readonly CacheDict<TKey, TValue>.Entry[] _entries;

		// Token: 0x02000144 RID: 324
		private sealed class Entry
		{
			// Token: 0x06000AB2 RID: 2738 RVA: 0x0002A69D File Offset: 0x0002889D
			internal Entry(int hash, TKey key, TValue value)
			{
				this._hash = hash;
				this._key = key;
				this._value = value;
			}

			// Token: 0x0400034B RID: 843
			internal readonly int _hash;

			// Token: 0x0400034C RID: 844
			internal readonly TKey _key;

			// Token: 0x0400034D RID: 845
			internal readonly TValue _value;
		}
	}
}
