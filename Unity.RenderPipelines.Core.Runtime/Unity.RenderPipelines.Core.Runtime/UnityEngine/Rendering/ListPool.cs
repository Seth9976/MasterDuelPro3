using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering
{
	// Token: 0x02000056 RID: 86
	public static class ListPool<T>
	{
		// Token: 0x060004A5 RID: 1189 RVA: 0x00008DC5 File Offset: 0x00006FC5
		public static List<T> Get()
		{
			return ListPool<T>.s_Pool.Get();
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x00008DD1 File Offset: 0x00006FD1
		public static ObjectPool<List<T>>.PooledObject Get(out List<T> value)
		{
			return ListPool<T>.s_Pool.Get(out value);
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x00008DDE File Offset: 0x00006FDE
		public static void Release(List<T> toRelease)
		{
			ListPool<T>.s_Pool.Release(toRelease);
		}

		// Token: 0x0400012F RID: 303
		private static readonly ObjectPool<List<T>> s_Pool = new ObjectPool<List<T>>(null, delegate(List<T> l)
		{
			l.Clear();
		}, true);
	}
}
