using System;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x02000395 RID: 917
	[UsedByNativeCode]
	internal struct GPUDrivenLODGroupDataNative
	{
		// Token: 0x04000B2F RID: 2863
		public unsafe int* lodGroupID;

		// Token: 0x04000B30 RID: 2864
		public unsafe int* lodOffset;

		// Token: 0x04000B31 RID: 2865
		public unsafe int* lodCount;

		// Token: 0x04000B32 RID: 2866
		public unsafe LODFadeMode* fadeMode;

		// Token: 0x04000B33 RID: 2867
		public unsafe Vector3* worldSpaceReferencePoint;

		// Token: 0x04000B34 RID: 2868
		public unsafe float* worldSpaceSize;

		// Token: 0x04000B35 RID: 2869
		public unsafe short* renderersCount;

		// Token: 0x04000B36 RID: 2870
		public unsafe bool* lastLODIsBillboard;

		// Token: 0x04000B37 RID: 2871
		public int lodGroupCount;

		// Token: 0x04000B38 RID: 2872
		public unsafe int* invalidLODGroupID;

		// Token: 0x04000B39 RID: 2873
		public int invalidLODGroupCount;

		// Token: 0x04000B3A RID: 2874
		public unsafe short* lodRenderersCount;

		// Token: 0x04000B3B RID: 2875
		public unsafe float* lodScreenRelativeTransitionHeight;

		// Token: 0x04000B3C RID: 2876
		public unsafe float* lodFadeTransitionWidth;

		// Token: 0x04000B3D RID: 2877
		public int lodDataCount;
	}
}
