using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000054 RID: 84
	public static class GenericPool<T> where T : new()
	{
		// Token: 0x0600049D RID: 1181 RVA: 0x00008D5B File Offset: 0x00006F5B
		public static T Get()
		{
			return GenericPool<T>.s_Pool.Get();
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x00008D67 File Offset: 0x00006F67
		public static ObjectPool<T>.PooledObject Get(out T value)
		{
			return GenericPool<T>.s_Pool.Get(out value);
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x00008D74 File Offset: 0x00006F74
		public static void Release(T toRelease)
		{
			GenericPool<T>.s_Pool.Release(toRelease);
		}

		// Token: 0x0400012D RID: 301
		private static readonly ObjectPool<T> s_Pool = new ObjectPool<T>(null, null, true);
	}
}
