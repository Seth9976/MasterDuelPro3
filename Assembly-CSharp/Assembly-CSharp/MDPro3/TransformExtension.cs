using System;
using System.Collections.Generic;
using UnityEngine;

namespace MDPro3
{
	// Token: 0x02001226 RID: 4646
	public static class TransformExtension
	{
		// Token: 0x06008993 RID: 35219 RVA: 0x0010B084 File Offset: 0x00109284
		public static void DestroyAllChildren(this Transform transform)
		{
			for (int i = 0; i < transform.childCount; i++)
			{
				global::UnityEngine.Object.Destroy(transform.GetChild(i).gameObject);
			}
		}

		// Token: 0x06008994 RID: 35220 RVA: 0x0010B0B4 File Offset: 0x001092B4
		public static Transform GetChildByName(this Transform parent, string childName)
		{
			foreach (Transform t in parent.GetComponentsInChildren<Transform>(true))
			{
				if (t.name == childName)
				{
					return t;
				}
			}
			return null;
		}

		// Token: 0x06008995 RID: 35221 RVA: 0x0010B0EC File Offset: 0x001092EC
		public static List<Transform> GetChildrenByName(this Transform parent, string childrenName)
		{
			List<Transform> value = new List<Transform>();
			foreach (Transform t in parent.GetComponentsInChildren<Transform>(true))
			{
				if (t.name == childrenName)
				{
					value.Add(t);
				}
			}
			return value;
		}

		// Token: 0x06008996 RID: 35222 RVA: 0x0010B12F File Offset: 0x0010932F
		public static void DestroyChildByName(this Transform parent, string childName)
		{
			global::UnityEngine.Object.Destroy(parent.GetChildByName(childName).gameObject);
		}

		// Token: 0x06008997 RID: 35223 RVA: 0x0010B144 File Offset: 0x00109344
		public static void DestroyChildrenByName(this Transform parent, string childrenName)
		{
			foreach (Transform transform in parent.GetChildrenByName(childrenName))
			{
				global::UnityEngine.Object.Destroy(transform.gameObject);
			}
		}
	}
}
