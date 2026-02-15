using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020003CB RID: 971
	[Flags]
	public enum SortingCriteria
	{
		// Token: 0x04000C86 RID: 3206
		None = 0,
		// Token: 0x04000C87 RID: 3207
		SortingLayer = 1,
		// Token: 0x04000C88 RID: 3208
		RenderQueue = 2,
		// Token: 0x04000C89 RID: 3209
		BackToFront = 4,
		// Token: 0x04000C8A RID: 3210
		QuantizedFrontToBack = 8,
		// Token: 0x04000C8B RID: 3211
		OptimizeStateChanges = 16,
		// Token: 0x04000C8C RID: 3212
		CanvasOrder = 32,
		// Token: 0x04000C8D RID: 3213
		RendererPriority = 64,
		// Token: 0x04000C8E RID: 3214
		CommonOpaque = 59,
		// Token: 0x04000C8F RID: 3215
		CommonTransparent = 23
	}
}
