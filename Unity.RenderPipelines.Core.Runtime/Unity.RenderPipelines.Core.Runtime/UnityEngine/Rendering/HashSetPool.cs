using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering
{
	// Token: 0x02000058 RID: 88
	public static class HashSetPool<T>
	{
		// Token: 0x060004AC RID: 1196 RVA: 0x00008E1D File Offset: 0x0000701D
		public static HashSet<T> Get()
		{
			return HashSetPool<T>.s_Pool.Get();
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x00008E29 File Offset: 0x00007029
		public static ObjectPool<HashSet<T>>.PooledObject Get(out HashSet<T> value)
		{
			return HashSetPool<T>.s_Pool.Get(out value);
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x00008E36 File Offset: 0x00007036
		public static void Release(HashSet<T> toRelease)
		{
			HashSetPool<T>.s_Pool.Release(toRelease);
		}

		// Token: 0x04000131 RID: 305
		private static readonly ObjectPool<HashSet<T>> s_Pool = new ObjectPool<HashSet<T>>(null, delegate(HashSet<T> l)
		{
			l.Clear();
		}, true);
	}
}
