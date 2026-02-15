using System;
using Unity.Jobs;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x02000385 RID: 901
	[NativeHeader("Runtime/Camera/BatchRendererGroup.h")]
	[UsedByNativeCode]
	internal struct BatchRendererCullingOutput
	{
		// Token: 0x04000AEC RID: 2796
		public JobHandle cullingJobsFence;

		// Token: 0x04000AED RID: 2797
		public Matrix4x4 localToWorldMatrix;

		// Token: 0x04000AEE RID: 2798
		public unsafe Plane* cullingPlanes;

		// Token: 0x04000AEF RID: 2799
		public int cullingPlaneCount;

		// Token: 0x04000AF0 RID: 2800
		public int receiverPlaneOffset;

		// Token: 0x04000AF1 RID: 2801
		public int receiverPlaneCount;

		// Token: 0x04000AF2 RID: 2802
		public unsafe CullingSplit* cullingSplits;

		// Token: 0x04000AF3 RID: 2803
		public int cullingSplitCount;

		// Token: 0x04000AF4 RID: 2804
		public BatchCullingViewType viewType;

		// Token: 0x04000AF5 RID: 2805
		public BatchCullingProjectionType projectionType;

		// Token: 0x04000AF6 RID: 2806
		public BatchCullingFlags cullingFlags;

		// Token: 0x04000AF7 RID: 2807
		public ulong viewID;

		// Token: 0x04000AF8 RID: 2808
		public uint cullingLayerMask;

		// Token: 0x04000AF9 RID: 2809
		public byte splitExclusionMask;

		// Token: 0x04000AFA RID: 2810
		public ulong sceneCullingMask;

		// Token: 0x04000AFB RID: 2811
		public unsafe BatchCullingOutputDrawCommands* drawCommands;

		// Token: 0x04000AFC RID: 2812
		public uint brgId;

		// Token: 0x04000AFD RID: 2813
		public IntPtr occlusionBuffer;

		// Token: 0x04000AFE RID: 2814
		public IntPtr customCullingResult;
	}
}
