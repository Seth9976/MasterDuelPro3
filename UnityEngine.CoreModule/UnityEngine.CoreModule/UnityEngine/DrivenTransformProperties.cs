using System;

namespace UnityEngine
{
	// Token: 0x020001E5 RID: 485
	[Flags]
	public enum DrivenTransformProperties
	{
		// Token: 0x040006EF RID: 1775
		None = 0,
		// Token: 0x040006F0 RID: 1776
		All = -1,
		// Token: 0x040006F1 RID: 1777
		AnchoredPositionX = 2,
		// Token: 0x040006F2 RID: 1778
		AnchoredPositionY = 4,
		// Token: 0x040006F3 RID: 1779
		AnchoredPositionZ = 8,
		// Token: 0x040006F4 RID: 1780
		Rotation = 16,
		// Token: 0x040006F5 RID: 1781
		ScaleX = 32,
		// Token: 0x040006F6 RID: 1782
		ScaleY = 64,
		// Token: 0x040006F7 RID: 1783
		ScaleZ = 128,
		// Token: 0x040006F8 RID: 1784
		AnchorMinX = 256,
		// Token: 0x040006F9 RID: 1785
		AnchorMinY = 512,
		// Token: 0x040006FA RID: 1786
		AnchorMaxX = 1024,
		// Token: 0x040006FB RID: 1787
		AnchorMaxY = 2048,
		// Token: 0x040006FC RID: 1788
		SizeDeltaX = 4096,
		// Token: 0x040006FD RID: 1789
		SizeDeltaY = 8192,
		// Token: 0x040006FE RID: 1790
		PivotX = 16384,
		// Token: 0x040006FF RID: 1791
		PivotY = 32768,
		// Token: 0x04000700 RID: 1792
		AnchoredPosition = 6,
		// Token: 0x04000701 RID: 1793
		AnchoredPosition3D = 14,
		// Token: 0x04000702 RID: 1794
		Scale = 224,
		// Token: 0x04000703 RID: 1795
		AnchorMin = 768,
		// Token: 0x04000704 RID: 1796
		AnchorMax = 3072,
		// Token: 0x04000705 RID: 1797
		Anchors = 3840,
		// Token: 0x04000706 RID: 1798
		SizeDelta = 12288,
		// Token: 0x04000707 RID: 1799
		Pivot = 49152
	}
}
