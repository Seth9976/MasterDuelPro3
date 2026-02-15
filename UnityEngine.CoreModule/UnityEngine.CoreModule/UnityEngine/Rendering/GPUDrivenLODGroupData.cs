using System;
using Unity.Collections;

namespace UnityEngine.Rendering
{
	// Token: 0x02000399 RID: 921
	internal struct GPUDrivenLODGroupData
	{
		// Token: 0x04000B5C RID: 2908
		public NativeArray<int> lodGroupID;

		// Token: 0x04000B5D RID: 2909
		public NativeArray<int> lodOffset;

		// Token: 0x04000B5E RID: 2910
		public NativeArray<int> lodCount;

		// Token: 0x04000B5F RID: 2911
		public NativeArray<LODFadeMode> fadeMode;

		// Token: 0x04000B60 RID: 2912
		public NativeArray<Vector3> worldSpaceReferencePoint;

		// Token: 0x04000B61 RID: 2913
		public NativeArray<float> worldSpaceSize;

		// Token: 0x04000B62 RID: 2914
		public NativeArray<short> renderersCount;

		// Token: 0x04000B63 RID: 2915
		public NativeArray<bool> lastLODIsBillboard;

		// Token: 0x04000B64 RID: 2916
		public NativeArray<int> invalidLODGroupID;

		// Token: 0x04000B65 RID: 2917
		public NativeArray<short> lodRenderersCount;

		// Token: 0x04000B66 RID: 2918
		public NativeArray<float> lodScreenRelativeTransitionHeight;

		// Token: 0x04000B67 RID: 2919
		public NativeArray<float> lodFadeTransitionWidth;
	}
}
