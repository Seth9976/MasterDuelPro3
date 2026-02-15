using System;
using System.Collections.Generic;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements
{
	// Token: 0x020004CD RID: 1229
	[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
	internal class ObjectListPool<T>
	{
		// Token: 0x060022C4 RID: 8900 RVA: 0x0007FEFC File Offset: 0x0007E0FC
		public static List<T> Get()
		{
			return ObjectListPool<T>.pool.Get();
		}

		// Token: 0x060022C5 RID: 8901 RVA: 0x0007FF18 File Offset: 0x0007E118
		public static void Release(List<T> elements)
		{
			elements.Clear();
			ObjectListPool<T>.pool.Release(elements);
		}

		// Token: 0x04000FA7 RID: 4007
		private static ObjectPool<List<T>> pool = new ObjectPool<List<T>>(() => new List<T>(), 20);
	}
}
