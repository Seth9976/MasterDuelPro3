using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000026 RID: 38
	internal struct OcclusionCullingDebugOutput
	{
		// Token: 0x04000078 RID: 120
		public RTHandle occluderDepthPyramid;

		// Token: 0x04000079 RID: 121
		public GraphicsBuffer occlusionDebugOverlay;

		// Token: 0x0400007A RID: 122
		public OcclusionCullingDebugShaderVariables cb;
	}
}
