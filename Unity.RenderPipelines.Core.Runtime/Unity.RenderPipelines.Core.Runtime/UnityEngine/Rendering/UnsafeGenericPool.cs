using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000055 RID: 85
	public static class UnsafeGenericPool<T> where T : new()
	{
		// Token: 0x060004A1 RID: 1185 RVA: 0x00008D90 File Offset: 0x00006F90
		public static T Get()
		{
			return UnsafeGenericPool<T>.s_Pool.Get();
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x00008D9C File Offset: 0x00006F9C
		public static ObjectPool<T>.PooledObject Get(out T value)
		{
			return UnsafeGenericPool<T>.s_Pool.Get(out value);
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x00008DA9 File Offset: 0x00006FA9
		public static void Release(T toRelease)
		{
			UnsafeGenericPool<T>.s_Pool.Release(toRelease);
		}

		// Token: 0x0400012E RID: 302
		private static readonly ObjectPool<T> s_Pool = new ObjectPool<T>(null, null, false);
	}
}
