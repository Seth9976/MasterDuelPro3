using System;
using System.Collections.Generic;

namespace UnityEngine.Pool
{
	// Token: 0x020002E7 RID: 743
	public class CollectionPool<TCollection, TItem> where TCollection : class, ICollection<TItem>, new()
	{
		// Token: 0x060014DF RID: 5343 RVA: 0x0002C19D File Offset: 0x0002A39D
		public static TCollection Get()
		{
			return CollectionPool<TCollection, TItem>.s_Pool.Get();
		}

		// Token: 0x060014E0 RID: 5344 RVA: 0x0002C1A9 File Offset: 0x0002A3A9
		public static PooledObject<TCollection> Get(out TCollection value)
		{
			return CollectionPool<TCollection, TItem>.s_Pool.Get(out value);
		}

		// Token: 0x060014E1 RID: 5345 RVA: 0x0002C1B6 File Offset: 0x0002A3B6
		public static void Release(TCollection toRelease)
		{
			CollectionPool<TCollection, TItem>.s_Pool.Release(toRelease);
		}

		// Token: 0x040007D0 RID: 2000
		internal static readonly ObjectPool<TCollection> s_Pool = new ObjectPool<TCollection>(() => new TCollection(), null, delegate(TCollection l)
		{
			l.Clear();
		}, null, true, 10, 10000);
	}
}
