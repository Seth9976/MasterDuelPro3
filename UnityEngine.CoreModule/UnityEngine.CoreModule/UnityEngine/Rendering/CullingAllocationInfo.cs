using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020003AB RID: 939
	internal struct CullingAllocationInfo
	{
		// Token: 0x04000BE5 RID: 3045
		public unsafe VisibleLight* visibleLightsPtr;

		// Token: 0x04000BE6 RID: 3046
		public unsafe VisibleLight* visibleOffscreenVertexLightsPtr;

		// Token: 0x04000BE7 RID: 3047
		public unsafe VisibleReflectionProbe* visibleReflectionProbesPtr;

		// Token: 0x04000BE8 RID: 3048
		public int visibleLightCount;

		// Token: 0x04000BE9 RID: 3049
		public int visibleOffscreenVertexLightCount;

		// Token: 0x04000BEA RID: 3050
		public int visibleReflectionProbeCount;
	}
}
