using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200021A RID: 538
	public static class PointerType
	{
		// Token: 0x06000EA0 RID: 3744 RVA: 0x00040EF8 File Offset: 0x0003F0F8
		internal static string GetPointerType(int pointerId)
		{
			bool flag = pointerId == PointerId.mousePointerId;
			string text;
			if (flag)
			{
				text = PointerType.mouse;
			}
			else
			{
				bool flag2 = pointerId == PointerId.penPointerIdBase;
				if (flag2)
				{
					text = PointerType.pen;
				}
				else
				{
					text = PointerType.touch;
				}
			}
			return text;
		}

		// Token: 0x06000EA1 RID: 3745 RVA: 0x00040F38 File Offset: 0x0003F138
		internal static bool IsDirectManipulationDevice(string pointerType)
		{
			return pointerType == PointerType.touch || pointerType == PointerType.pen;
		}

		// Token: 0x0400088C RID: 2188
		public static readonly string mouse = "mouse";

		// Token: 0x0400088D RID: 2189
		public static readonly string touch = "touch";

		// Token: 0x0400088E RID: 2190
		public static readonly string pen = "pen";

		// Token: 0x0400088F RID: 2191
		public static readonly string unknown = "";
	}
}
