using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200021B RID: 539
	public static class PointerId
	{
		// Token: 0x04000890 RID: 2192
		public static readonly int maxPointers = 32;

		// Token: 0x04000891 RID: 2193
		public static readonly int invalidPointerId = -1;

		// Token: 0x04000892 RID: 2194
		public static readonly int mousePointerId = 0;

		// Token: 0x04000893 RID: 2195
		public static readonly int touchPointerIdBase = 1;

		// Token: 0x04000894 RID: 2196
		public static readonly int touchPointerCount = 20;

		// Token: 0x04000895 RID: 2197
		public static readonly int penPointerIdBase = PointerId.touchPointerIdBase + PointerId.touchPointerCount;

		// Token: 0x04000896 RID: 2198
		public static readonly int penPointerCount = 2;

		// Token: 0x04000897 RID: 2199
		internal static readonly int[] hoveringPointers = new int[] { PointerId.mousePointerId };
	}
}
