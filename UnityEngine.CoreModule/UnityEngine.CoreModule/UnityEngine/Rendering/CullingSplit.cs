using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x02000382 RID: 898
	[UsedByNativeCode]
	[NativeHeader("Runtime/Camera/BatchRendererGroup.h")]
	public struct CullingSplit
	{
		// Token: 0x04000AD4 RID: 2772
		public Vector3 sphereCenter;

		// Token: 0x04000AD5 RID: 2773
		public float sphereRadius;

		// Token: 0x04000AD6 RID: 2774
		public int cullingPlaneOffset;

		// Token: 0x04000AD7 RID: 2775
		public int cullingPlaneCount;

		// Token: 0x04000AD8 RID: 2776
		public float cascadeBlendCullingFactor;

		// Token: 0x04000AD9 RID: 2777
		public float nearPlane;

		// Token: 0x04000ADA RID: 2778
		public Matrix4x4 cullingMatrix;
	}
}
