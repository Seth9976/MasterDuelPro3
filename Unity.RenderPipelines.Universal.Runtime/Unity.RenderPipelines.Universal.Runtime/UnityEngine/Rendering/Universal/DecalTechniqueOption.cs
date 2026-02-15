using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000163 RID: 355
	internal enum DecalTechniqueOption
	{
		// Token: 0x04000825 RID: 2085
		[Tooltip("Automatically selects technique based on build platform.")]
		Automatic,
		// Token: 0x04000826 RID: 2086
		[Tooltip("Renders decals into DBuffer and then applied during opaque rendering. Requires DepthNormal prepass which makes not viable solution for the tile based renderers common on mobile.")]
		[InspectorName("DBuffer")]
		DBuffer,
		// Token: 0x04000827 RID: 2087
		[Tooltip("Renders decals after opaque objects with normal reconstructed from depth. The decals are simply rendered as mesh on top of opaque ones, as result does not support blending per single surface data (etc. normal blending only).")]
		ScreenSpace
	}
}
