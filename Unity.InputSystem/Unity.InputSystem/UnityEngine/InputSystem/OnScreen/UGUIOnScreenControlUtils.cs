using System;

namespace UnityEngine.InputSystem.OnScreen
{
	// Token: 0x02000134 RID: 308
	internal static class UGUIOnScreenControlUtils
	{
		// Token: 0x06000E2E RID: 3630 RVA: 0x000477A0 File Offset: 0x000459A0
		public static RectTransform GetCanvasRectTransform(Transform transform)
		{
			if (!(transform.parent != null))
			{
				return null;
			}
			return transform.parent.GetComponentInParent<RectTransform>();
		}
	}
}
