using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020003DE RID: 990
	[Flags]
	public enum ShaderPropertyFlags
	{
		// Token: 0x04000D00 RID: 3328
		None = 0,
		// Token: 0x04000D01 RID: 3329
		HideInInspector = 1,
		// Token: 0x04000D02 RID: 3330
		PerRendererData = 2,
		// Token: 0x04000D03 RID: 3331
		NoScaleOffset = 4,
		// Token: 0x04000D04 RID: 3332
		Normal = 8,
		// Token: 0x04000D05 RID: 3333
		HDR = 16,
		// Token: 0x04000D06 RID: 3334
		Gamma = 32,
		// Token: 0x04000D07 RID: 3335
		NonModifiableTextureData = 64,
		// Token: 0x04000D08 RID: 3336
		MainTexture = 128,
		// Token: 0x04000D09 RID: 3337
		MainColor = 256
	}
}
