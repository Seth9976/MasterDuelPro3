using System;

namespace UnityEngine.UI
{
	// Token: 0x0200005A RID: 90
	internal static class Misc
	{
		// Token: 0x06000361 RID: 865 RVA: 0x000105F6 File Offset: 0x0000E7F6
		public static void Destroy(Object obj)
		{
			if (obj != null)
			{
				if (Application.isPlaying)
				{
					if (obj is GameObject)
					{
						(obj as GameObject).transform.parent = null;
					}
					Object.Destroy(obj);
					return;
				}
				Object.DestroyImmediate(obj);
			}
		}

		// Token: 0x06000362 RID: 866 RVA: 0x0001062E File Offset: 0x0000E82E
		public static void DestroyImmediate(Object obj)
		{
			if (obj != null)
			{
				if (Application.isEditor)
				{
					Object.DestroyImmediate(obj);
					return;
				}
				Object.Destroy(obj);
			}
		}
	}
}
