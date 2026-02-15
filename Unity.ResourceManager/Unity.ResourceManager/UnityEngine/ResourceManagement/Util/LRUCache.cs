using System;
using System.Collections.Generic;

namespace UnityEngine.ResourceManagement.Util
{
	// Token: 0x02000027 RID: 39
	internal struct LRUCache<TKey, TValue> where TKey : IEquatable<TKey>
	{
		// Token: 0x060000FD RID: 253 RVA: 0x00005A75 File Offset: 0x00003C75
		public LRUCache(int limit)
		{
			this.entryLimit = limit;
			this.cache = new Dictionary<TKey, LRUCache<TKey, TValue>.Entry>();
			this.lru = new LinkedList<TKey>();
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00005A94 File Offset: 0x00003C94
		public bool TryAdd(TKey id, TValue obj)
		{
			if (obj == null || this.entryLimit <= 0)
			{
				return false;
			}
			if (!this.cache.TryAdd(id, new LRUCache<TKey, TValue>.Entry
			{
				Value = obj,
				lruNode = this.lru.AddFirst(id)
			}))
			{
				return false;
			}
			while (this.lru.Count > this.entryLimit)
			{
				this.cache.Remove(this.lru.Last.Value);
				this.lru.RemoveLast();
			}
			return true;
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00005B24 File Offset: 0x00003D24
		public bool TryGet(TKey offset, out TValue val)
		{
			LRUCache<TKey, TValue>.Entry entry;
			if (this.cache.TryGetValue(offset, out entry))
			{
				val = entry.Value;
				if (entry.lruNode.Previous != null)
				{
					this.lru.Remove(entry.lruNode);
					this.lru.AddFirst(entry.lruNode);
				}
				return true;
			}
			val = default(TValue);
			return false;
		}

		// Token: 0x0400006E RID: 110
		private int entryLimit;

		// Token: 0x0400006F RID: 111
		private Dictionary<TKey, LRUCache<TKey, TValue>.Entry> cache;

		// Token: 0x04000070 RID: 112
		private LinkedList<TKey> lru;

		// Token: 0x02000028 RID: 40
		public struct Entry : IEquatable<LRUCache<TKey, TValue>.Entry>
		{
			// Token: 0x06000100 RID: 256 RVA: 0x00005B86 File Offset: 0x00003D86
			public bool Equals(LRUCache<TKey, TValue>.Entry other)
			{
				return this.Value.Equals(other);
			}

			// Token: 0x06000101 RID: 257 RVA: 0x00005B9F File Offset: 0x00003D9F
			public override int GetHashCode()
			{
				return this.Value.GetHashCode();
			}

			// Token: 0x04000071 RID: 113
			public LinkedListNode<TKey> lruNode;

			// Token: 0x04000072 RID: 114
			public TValue Value;
		}
	}
}
