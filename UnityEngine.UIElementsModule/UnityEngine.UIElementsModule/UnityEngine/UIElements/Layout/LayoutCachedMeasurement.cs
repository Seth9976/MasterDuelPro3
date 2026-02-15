using System;

namespace UnityEngine.UIElements.Layout
{
	// Token: 0x0200057C RID: 1404
	internal struct LayoutCachedMeasurement
	{
		// Token: 0x04001386 RID: 4998
		public static LayoutCachedMeasurement Default = new LayoutCachedMeasurement
		{
			AvailableWidth = 0f,
			AvailableHeight = 0f,
			ParentWidth = 0f,
			ParentHeight = 0f,
			WidthMeasureMode = LayoutMeasureMode.Invalid,
			HeightMeasureMode = LayoutMeasureMode.Invalid,
			ComputedWidth = -1f,
			ComputedHeight = -1f
		};

		// Token: 0x04001387 RID: 4999
		public float AvailableWidth;

		// Token: 0x04001388 RID: 5000
		public float AvailableHeight;

		// Token: 0x04001389 RID: 5001
		public float ParentWidth;

		// Token: 0x0400138A RID: 5002
		public float ParentHeight;

		// Token: 0x0400138B RID: 5003
		public LayoutMeasureMode WidthMeasureMode;

		// Token: 0x0400138C RID: 5004
		public LayoutMeasureMode HeightMeasureMode;

		// Token: 0x0400138D RID: 5005
		public float ComputedWidth;

		// Token: 0x0400138E RID: 5006
		public float ComputedHeight;
	}
}
