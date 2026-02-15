using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering
{
	// Token: 0x0200005A RID: 90
	public static class DictionaryPool<TKey, TValue>
	{
		// Token: 0x060004B3 RID: 1203 RVA: 0x00008E75 File Offset: 0x00007075
		public static Dictionary<TKey, TValue> Get()
		{
			return DictionaryPool<TKey, TValue>.s_Pool.Get();
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x00008E81 File Offset: 0x00007081
		public static ObjectPool<Dictionary<TKey, TValue>>.PooledObject Get(out Dictionary<TKey, TValue> value)
		{
			return DictionaryPool<TKey, TValue>.s_Pool.Get(out value);
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x00008E8E File Offset: 0x0000708E
		public static void Release(Dictionary<TKey, TValue> toRelease)
		{
			DictionaryPool<TKey, TValue>.s_Pool.Release(toRelease);
		}

		// Token: 0x04000133 RID: 307
		private static readonly ObjectPool<Dictionary<TKey, TValue>> s_Pool = new ObjectPool<Dictionary<TKey, TValue>>(null, delegate(Dictionary<TKey, TValue> l)
		{
			l.Clear();
		}, true);
	}
}
