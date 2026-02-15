using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Mathematics;

namespace UnityEngine.Rendering
{
	// Token: 0x020000A3 RID: 163
	internal struct LODGroupCullingData
	{
		// Token: 0x04000355 RID: 853
		public float3 worldSpaceReferencePoint;

		// Token: 0x04000356 RID: 854
		public int lodCount;

		// Token: 0x04000357 RID: 855
		[FixedBuffer(typeof(float), 8)]
		public LODGroupCullingData.<sqrDistances>e__FixedBuffer sqrDistances;

		// Token: 0x04000358 RID: 856
		[FixedBuffer(typeof(float), 8)]
		public LODGroupCullingData.<transitionDistances>e__FixedBuffer transitionDistances;

		// Token: 0x04000359 RID: 857
		public float worldSpaceSize;

		// Token: 0x0400035A RID: 858
		[FixedBuffer(typeof(bool), 8)]
		public LODGroupCullingData.<percentageFlags>e__FixedBuffer percentageFlags;

		// Token: 0x020000A4 RID: 164
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 8)]
		public struct <percentageFlags>e__FixedBuffer
		{
			// Token: 0x0400035B RID: 859
			public bool FixedElementField;
		}

		// Token: 0x020000A5 RID: 165
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 32)]
		public struct <sqrDistances>e__FixedBuffer
		{
			// Token: 0x0400035C RID: 860
			public float FixedElementField;
		}

		// Token: 0x020000A6 RID: 166
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 32)]
		public struct <transitionDistances>e__FixedBuffer
		{
			// Token: 0x0400035D RID: 861
			public float FixedElementField;
		}
	}
}
