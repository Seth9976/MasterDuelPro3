using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020003B5 RID: 949
	[Flags]
	public enum PerObjectData
	{
		// Token: 0x04000C0F RID: 3087
		None = 0,
		// Token: 0x04000C10 RID: 3088
		LightProbe = 1,
		// Token: 0x04000C11 RID: 3089
		ReflectionProbes = 2,
		// Token: 0x04000C12 RID: 3090
		LightProbeProxyVolume = 4,
		// Token: 0x04000C13 RID: 3091
		Lightmaps = 8,
		// Token: 0x04000C14 RID: 3092
		LightData = 16,
		// Token: 0x04000C15 RID: 3093
		MotionVectors = 32,
		// Token: 0x04000C16 RID: 3094
		LightIndices = 64,
		// Token: 0x04000C17 RID: 3095
		ReflectionProbeData = 128,
		// Token: 0x04000C18 RID: 3096
		OcclusionProbe = 256,
		// Token: 0x04000C19 RID: 3097
		OcclusionProbeProxyVolume = 512,
		// Token: 0x04000C1A RID: 3098
		ShadowMask = 1024
	}
}
