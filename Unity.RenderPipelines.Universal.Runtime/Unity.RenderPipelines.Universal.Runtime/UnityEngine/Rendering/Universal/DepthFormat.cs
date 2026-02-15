using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020001E2 RID: 482
	public enum DepthFormat
	{
		// Token: 0x04000BD4 RID: 3028
		[RenderPathCompatible(RenderPathCompatibility.All)]
		Default,
		// Token: 0x04000BD5 RID: 3029
		[RenderPathCompatible(RenderPathCompatibility.Forward | RenderPathCompatibility.ForwardPlus)]
		Depth_16 = 90,
		// Token: 0x04000BD6 RID: 3030
		[RenderPathCompatible(RenderPathCompatibility.Forward | RenderPathCompatibility.ForwardPlus)]
		Depth_24,
		// Token: 0x04000BD7 RID: 3031
		[RenderPathCompatible(RenderPathCompatibility.Forward | RenderPathCompatibility.ForwardPlus)]
		Depth_32 = 93,
		// Token: 0x04000BD8 RID: 3032
		[RenderPathCompatible(RenderPathCompatibility.All)]
		Depth_16_Stencil_8 = 151,
		// Token: 0x04000BD9 RID: 3033
		[RenderPathCompatible(RenderPathCompatibility.All)]
		Depth_24_Stencil_8 = 92,
		// Token: 0x04000BDA RID: 3034
		[RenderPathCompatible(RenderPathCompatibility.All)]
		Depth_32_Stencil_8 = 94
	}
}
