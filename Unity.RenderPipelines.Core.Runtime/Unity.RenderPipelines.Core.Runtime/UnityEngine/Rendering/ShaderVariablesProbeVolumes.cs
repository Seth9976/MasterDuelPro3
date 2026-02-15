using System;
using Unity.Mathematics;

namespace UnityEngine.Rendering
{
	// Token: 0x02000145 RID: 325
	[GenerateHLSL(PackingRules.Exact, true, false, false, 1, false, false, false, -1, "./Library/PackageCache/com.unity.render-pipelines.core/Runtime/Lighting/ProbeVolume/ShaderVariablesProbeVolumes.cs", needAccessors = false, generateCBuffer = true, constantRegister = 6)]
	internal struct ShaderVariablesProbeVolumes
	{
		// Token: 0x0400060A RID: 1546
		public Vector4 _Offset_LayerCount;

		// Token: 0x0400060B RID: 1547
		public Vector4 _MinLoadedCellInEntries_IndirectionEntryDim;

		// Token: 0x0400060C RID: 1548
		public Vector4 _MaxLoadedCellInEntries_RcpIndirectionEntryDim;

		// Token: 0x0400060D RID: 1549
		public Vector4 _PoolDim_MinBrickSize;

		// Token: 0x0400060E RID: 1550
		public Vector4 _RcpPoolDim_XY;

		// Token: 0x0400060F RID: 1551
		public Vector4 _MinEntryPos_Noise;

		// Token: 0x04000610 RID: 1552
		public uint4 _EntryCount_X_XY_LeakReduction;

		// Token: 0x04000611 RID: 1553
		public Vector4 _Biases_NormalizationClamp;

		// Token: 0x04000612 RID: 1554
		public Vector4 _FrameIndex_Weights;

		// Token: 0x04000613 RID: 1555
		public uint4 _ProbeVolumeLayerMask;
	}
}
