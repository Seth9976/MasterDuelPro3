using System;

namespace UnityEngine.Rendering
{
	// Token: 0x0200034B RID: 843
	[Flags]
	public enum CopyTextureSupport
	{
		// Token: 0x040009D2 RID: 2514
		None = 0,
		// Token: 0x040009D3 RID: 2515
		Basic = 1,
		// Token: 0x040009D4 RID: 2516
		Copy3D = 2,
		// Token: 0x040009D5 RID: 2517
		DifferentTypes = 4,
		// Token: 0x040009D6 RID: 2518
		TextureToRT = 8,
		// Token: 0x040009D7 RID: 2519
		RTToTexture = 16
	}
}
