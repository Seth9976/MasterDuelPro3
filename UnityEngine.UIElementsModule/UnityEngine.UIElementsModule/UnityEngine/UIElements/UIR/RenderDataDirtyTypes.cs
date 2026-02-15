using System;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x0200054B RID: 1355
	[Flags]
	internal enum RenderDataDirtyTypes
	{
		// Token: 0x04001284 RID: 4740
		None = 0,
		// Token: 0x04001285 RID: 4741
		Transform = 1,
		// Token: 0x04001286 RID: 4742
		ClipRectSize = 2,
		// Token: 0x04001287 RID: 4743
		Clipping = 4,
		// Token: 0x04001288 RID: 4744
		ClippingHierarchy = 8,
		// Token: 0x04001289 RID: 4745
		Visuals = 16,
		// Token: 0x0400128A RID: 4746
		VisualsHierarchy = 32,
		// Token: 0x0400128B RID: 4747
		VisualsOpacityId = 64,
		// Token: 0x0400128C RID: 4748
		Opacity = 128,
		// Token: 0x0400128D RID: 4749
		OpacityHierarchy = 256,
		// Token: 0x0400128E RID: 4750
		Color = 512,
		// Token: 0x0400128F RID: 4751
		AllVisuals = 112
	}
}
