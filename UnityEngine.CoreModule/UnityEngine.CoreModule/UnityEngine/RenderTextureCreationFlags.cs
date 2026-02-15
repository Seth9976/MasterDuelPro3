using System;

namespace UnityEngine
{
	// Token: 0x0200011B RID: 283
	[Flags]
	public enum RenderTextureCreationFlags
	{
		// Token: 0x040003C9 RID: 969
		MipMap = 1,
		// Token: 0x040003CA RID: 970
		AutoGenerateMips = 2,
		// Token: 0x040003CB RID: 971
		SRGB = 4,
		// Token: 0x040003CC RID: 972
		EyeTexture = 8,
		// Token: 0x040003CD RID: 973
		EnableRandomWrite = 16,
		// Token: 0x040003CE RID: 974
		CreatedFromScript = 32,
		// Token: 0x040003CF RID: 975
		AllowVerticalFlip = 128,
		// Token: 0x040003D0 RID: 976
		NoResolvedColorSurface = 256,
		// Token: 0x040003D1 RID: 977
		DynamicallyScalable = 1024,
		// Token: 0x040003D2 RID: 978
		BindMS = 2048,
		// Token: 0x040003D3 RID: 979
		DynamicallyScalableExplicit = 131072
	}
}
