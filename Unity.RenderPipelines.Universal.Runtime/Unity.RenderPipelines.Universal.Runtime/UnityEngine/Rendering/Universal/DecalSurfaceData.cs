using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000161 RID: 353
	internal enum DecalSurfaceData
	{
		// Token: 0x0400081C RID: 2076
		[Tooltip("Decals will affect only base color and emission.")]
		Albedo,
		// Token: 0x0400081D RID: 2077
		[Tooltip("Decals will affect only base color, normal and emission.")]
		AlbedoNormal,
		// Token: 0x0400081E RID: 2078
		[Tooltip("Decals will affect base color, normal, metallic, ambient occlusion, smoothness and emission.")]
		AlbedoNormalMAOS
	}
}
