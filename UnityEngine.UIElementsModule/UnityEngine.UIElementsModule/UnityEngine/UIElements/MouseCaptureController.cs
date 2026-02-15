using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000273 RID: 627
	public static class MouseCaptureController
	{
		// Token: 0x060010E8 RID: 4328 RVA: 0x00048CB8 File Offset: 0x00046EB8
		public static bool HasMouseCapture(this IEventHandler handler)
		{
			VisualElement ve = handler as VisualElement;
			return ve.HasPointerCapture(PointerId.mousePointerId);
		}

		// Token: 0x060010E9 RID: 4329 RVA: 0x00048CDC File Offset: 0x00046EDC
		public static void CaptureMouse(this IEventHandler handler)
		{
			VisualElement ve = handler as VisualElement;
			bool flag = ve != null;
			if (flag)
			{
				ve.CapturePointer(PointerId.mousePointerId);
				ve.panel.ProcessPointerCapture(PointerId.mousePointerId);
			}
		}
	}
}
