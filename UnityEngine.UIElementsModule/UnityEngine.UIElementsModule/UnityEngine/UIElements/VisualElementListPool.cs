using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x020004CB RID: 1227
	internal static class VisualElementListPool
	{
		// Token: 0x060022BD RID: 8893 RVA: 0x0007FE50 File Offset: 0x0007E050
		public static List<VisualElement> Copy(List<VisualElement> elements)
		{
			List<VisualElement> result = VisualElementListPool.pool.Get();
			result.AddRange(elements);
			return result;
		}

		// Token: 0x060022BE RID: 8894 RVA: 0x0007FE78 File Offset: 0x0007E078
		public static List<VisualElement> Get(int initialCapacity = 0)
		{
			List<VisualElement> result = VisualElementListPool.pool.Get();
			bool flag = initialCapacity > 0 && result.Capacity < initialCapacity;
			if (flag)
			{
				result.Capacity = initialCapacity;
			}
			return result;
		}

		// Token: 0x060022BF RID: 8895 RVA: 0x0007FEB4 File Offset: 0x0007E0B4
		public static void Release(List<VisualElement> elements)
		{
			elements.Clear();
			VisualElementListPool.pool.Release(elements);
		}

		// Token: 0x04000FA5 RID: 4005
		private static ObjectPool<List<VisualElement>> pool = new ObjectPool<List<VisualElement>>(() => new List<VisualElement>(), 20);
	}
}
