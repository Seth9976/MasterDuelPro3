using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020003A4 RID: 932
	[Flags]
	public enum CullingOptions
	{
		// Token: 0x04000BB7 RID: 2999
		None = 0,
		// Token: 0x04000BB8 RID: 3000
		ForceEvenIfCameraIsNotActive = 1,
		// Token: 0x04000BB9 RID: 3001
		OcclusionCull = 2,
		// Token: 0x04000BBA RID: 3002
		NeedsLighting = 4,
		// Token: 0x04000BBB RID: 3003
		NeedsReflectionProbes = 8,
		// Token: 0x04000BBC RID: 3004
		Stereo = 16,
		// Token: 0x04000BBD RID: 3005
		DisablePerObjectCulling = 32,
		// Token: 0x04000BBE RID: 3006
		ShadowCasters = 64
	}
}
