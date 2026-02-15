using System;
using System.Collections.Generic;

namespace UnityEngine.ResourceManagement.Util
{
	// Token: 0x02000037 RID: 55
	public class LRUCacheAllocationStrategy : IAllocationStrategy
	{
		// Token: 0x0600013C RID: 316 RVA: 0x00006448 File Offset: 0x00004648
		public LRUCacheAllocationStrategy(int poolMaxSize, int poolCapacity, int poolCacheMaxSize, int initialPoolCacheCapacity)
		{
			this.m_poolMaxSize = poolMaxSize;
			this.m_poolInitialCapacity = poolCapacity;
			this.m_poolCacheMaxSize = poolCacheMaxSize;
			for (int i = 0; i < initialPoolCacheCapacity; i++)
			{
				this.m_poolCache.Add(new List<object>(this.m_poolInitialCapacity));
			}
		}

		// Token: 0x0600013D RID: 317 RVA: 0x000064AC File Offset: 0x000046AC
		private List<object> GetPool()
		{
			int count = this.m_poolCache.Count;
			if (count == 0)
			{
				return new List<object>(this.m_poolInitialCapacity);
			}
			List<object> list = this.m_poolCache[count - 1];
			this.m_poolCache.RemoveAt(count - 1);
			return list;
		}

		// Token: 0x0600013E RID: 318 RVA: 0x000064F0 File Offset: 0x000046F0
		private void ReleasePool(List<object> pool)
		{
			if (this.m_poolCache.Count < this.m_poolCacheMaxSize)
			{
				this.m_poolCache.Add(pool);
			}
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00006514 File Offset: 0x00004714
		public object New(Type type, int typeHash)
		{
			List<object> pool;
			if (this.m_cache.TryGetValue(typeHash, out pool))
			{
				int count = pool.Count;
				object obj = pool[count - 1];
				pool.RemoveAt(count - 1);
				if (count == 1)
				{
					this.m_cache.Remove(typeHash);
					this.ReleasePool(pool);
				}
				return obj;
			}
			return Activator.CreateInstance(type);
		}

		// Token: 0x06000140 RID: 320 RVA: 0x0000656C File Offset: 0x0000476C
		public void Release(int typeHash, object obj)
		{
			List<object> pool;
			if (!this.m_cache.TryGetValue(typeHash, out pool))
			{
				this.m_cache.Add(typeHash, pool = this.GetPool());
			}
			if (pool.Count < this.m_poolMaxSize)
			{
				pool.Add(obj);
			}
		}

		// Token: 0x04000085 RID: 133
		private int m_poolMaxSize;

		// Token: 0x04000086 RID: 134
		private int m_poolInitialCapacity;

		// Token: 0x04000087 RID: 135
		private int m_poolCacheMaxSize;

		// Token: 0x04000088 RID: 136
		private List<List<object>> m_poolCache = new List<List<object>>();

		// Token: 0x04000089 RID: 137
		private Dictionary<int, List<object>> m_cache = new Dictionary<int, List<object>>();
	}
}
