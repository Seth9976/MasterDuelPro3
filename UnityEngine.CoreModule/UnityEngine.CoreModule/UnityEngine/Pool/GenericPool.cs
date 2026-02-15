using System;

namespace UnityEngine.Pool
{
	// Token: 0x020002E9 RID: 745
	public class GenericPool<T> where T : class, new()
	{
		// Token: 0x060014E7 RID: 5351 RVA: 0x0002C21B File Offset: 0x0002A41B
		public static T Get()
		{
			return GenericPool<T>.s_Pool.Get();
		}

		// Token: 0x060014E8 RID: 5352 RVA: 0x0002C227 File Offset: 0x0002A427
		public static PooledObject<T> Get(out T value)
		{
			return GenericPool<T>.s_Pool.Get(out value);
		}

		// Token: 0x060014E9 RID: 5353 RVA: 0x0002C234 File Offset: 0x0002A434
		public static void Release(T toRelease)
		{
			GenericPool<T>.s_Pool.Release(toRelease);
		}

		// Token: 0x040007D2 RID: 2002
		internal static readonly ObjectPool<T> s_Pool = new ObjectPool<T>(() => new T(), null, null, null, true, 10, 10000);
	}
}
