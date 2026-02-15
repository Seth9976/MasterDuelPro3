using System;
using UnityEngine;

namespace YgomSystem.Utility
{
	// Token: 0x02000556 RID: 1366
	public static class TransformUtil
	{
		// Token: 0x06002BB9 RID: 11193 RVA: 0x000F1CC0 File Offset: 0x000EFEC0
		public static ValueTuple<int, int> GetHierarchyDepth(Transform target, Transform root = null)
		{
			return default(ValueTuple<int, int>);
		}

		// Token: 0x06002BBA RID: 11194 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetHierarchyIndex(Transform target, Transform root)
		{
			return 0;
		}

		// Token: 0x06002BBB RID: 11195 RVA: 0x000029CC File Offset: 0x00000BCC
		private static bool GetChildCount(Transform parent, Transform findTarget, ref int index)
		{
			return false;
		}

		// Token: 0x06002BBC RID: 11196 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetHierarchyPath(Transform target)
		{
			return null;
		}
	}
}
