using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020004C7 RID: 1223
	[Flags]
	internal enum VisualElementFlags
	{
		// Token: 0x04000F8D RID: 3981
		WorldTransformDirty = 1,
		// Token: 0x04000F8E RID: 3982
		WorldTransformInverseDirty = 2,
		// Token: 0x04000F8F RID: 3983
		WorldClipDirty = 4,
		// Token: 0x04000F90 RID: 3984
		BoundingBoxDirty = 8,
		// Token: 0x04000F91 RID: 3985
		WorldBoundingBoxDirty = 16,
		// Token: 0x04000F92 RID: 3986
		EventInterestParentCategoriesDirty = 32,
		// Token: 0x04000F93 RID: 3987
		LayoutManual = 64,
		// Token: 0x04000F94 RID: 3988
		CompositeRoot = 128,
		// Token: 0x04000F95 RID: 3989
		RequireMeasureFunction = 256,
		// Token: 0x04000F96 RID: 3990
		EnableViewDataPersistence = 512,
		// Token: 0x04000F97 RID: 3991
		DisableClipping = 1024,
		// Token: 0x04000F98 RID: 3992
		NeedsAttachToPanelEvent = 2048,
		// Token: 0x04000F99 RID: 3993
		HierarchyDisplayed = 4096,
		// Token: 0x04000F9A RID: 3994
		StyleInitialized = 8192,
		// Token: 0x04000F9B RID: 3995
		DisableRendering = 16384,
		// Token: 0x04000F9C RID: 3996
		DetachedDataSource = 32768,
		// Token: 0x04000F9D RID: 3997
		Init = 32831
	}
}
