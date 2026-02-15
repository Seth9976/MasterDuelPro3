using System;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000562 RID: 1378
	internal class VectorImageRenderInfo : LinkedPoolItem<VectorImageRenderInfo>
	{
		// Token: 0x060025EA RID: 9706 RVA: 0x00096E1C File Offset: 0x0009501C
		public void Reset()
		{
			this.useCount = 0;
			this.firstGradientRemap = null;
			this.gradientSettingsAlloc = default(Alloc);
		}

		// Token: 0x04001319 RID: 4889
		public int useCount;

		// Token: 0x0400131A RID: 4890
		public GradientRemap firstGradientRemap;

		// Token: 0x0400131B RID: 4891
		public Alloc gradientSettingsAlloc;
	}
}
