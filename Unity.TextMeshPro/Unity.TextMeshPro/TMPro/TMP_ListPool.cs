using System;
using System.Collections.Generic;

namespace TMPro
{
	// Token: 0x0200005C RID: 92
	internal static class TMP_ListPool<T>
	{
		// Token: 0x0600030C RID: 780 RVA: 0x00010015 File Offset: 0x0000E215
		public static List<T> Get()
		{
			return TMP_ListPool<T>.s_ListPool.Get();
		}

		// Token: 0x0600030D RID: 781 RVA: 0x00010021 File Offset: 0x0000E221
		public static void Release(List<T> toRelease)
		{
			TMP_ListPool<T>.s_ListPool.Release(toRelease);
		}

		// Token: 0x0400022A RID: 554
		private static readonly TMP_ObjectPool<List<T>> s_ListPool = new TMP_ObjectPool<List<T>>(null, delegate(List<T> l)
		{
			l.Clear();
		});
	}
}
