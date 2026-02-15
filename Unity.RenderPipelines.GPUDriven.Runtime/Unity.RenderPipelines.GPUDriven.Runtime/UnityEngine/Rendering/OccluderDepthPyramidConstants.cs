using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UnityEngine.Rendering
{
	// Token: 0x020000AE RID: 174
	[GenerateHLSL(PackingRules.Exact, true, false, false, 1, false, false, false, -1, "./Library/PackageCache/com.unity.render-pipelines.core/Runtime/GPUDriven/OccluderDepthPyramidConstants.cs", needAccessors = false, generateCBuffer = true)]
	internal struct OccluderDepthPyramidConstants
	{
		// Token: 0x04000388 RID: 904
		[FixedBuffer(typeof(float), 96)]
		[HLSLArray(6, typeof(Matrix4x4))]
		public OccluderDepthPyramidConstants.<_InvViewProjMatrix>e__FixedBuffer _InvViewProjMatrix;

		// Token: 0x04000389 RID: 905
		[FixedBuffer(typeof(float), 24)]
		[HLSLArray(6, typeof(Vector4))]
		public OccluderDepthPyramidConstants.<_SilhouettePlanes>e__FixedBuffer _SilhouettePlanes;

		// Token: 0x0400038A RID: 906
		[FixedBuffer(typeof(uint), 24)]
		[HLSLArray(6, typeof(ShaderGenUInt4))]
		public OccluderDepthPyramidConstants.<_SrcOffset>e__FixedBuffer _SrcOffset;

		// Token: 0x0400038B RID: 907
		[FixedBuffer(typeof(uint), 20)]
		[HLSLArray(5, typeof(ShaderGenUInt4))]
		public OccluderDepthPyramidConstants.<_MipOffsetAndSize>e__FixedBuffer _MipOffsetAndSize;

		// Token: 0x0400038C RID: 908
		public uint _OccluderMipLayoutSizeX;

		// Token: 0x0400038D RID: 909
		public uint _OccluderMipLayoutSizeY;

		// Token: 0x0400038E RID: 910
		public uint _OccluderDepthPyramidPad0;

		// Token: 0x0400038F RID: 911
		public uint _OccluderDepthPyramidPad1;

		// Token: 0x04000390 RID: 912
		public uint _SrcSliceIndices;

		// Token: 0x04000391 RID: 913
		public uint _DstSubviewIndices;

		// Token: 0x04000392 RID: 914
		public uint _MipCount;

		// Token: 0x04000393 RID: 915
		public uint _SilhouettePlaneCount;

		// Token: 0x020000AF RID: 175
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 384)]
		public struct <_InvViewProjMatrix>e__FixedBuffer
		{
			// Token: 0x04000394 RID: 916
			public float FixedElementField;
		}

		// Token: 0x020000B0 RID: 176
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 80)]
		public struct <_MipOffsetAndSize>e__FixedBuffer
		{
			// Token: 0x04000395 RID: 917
			public uint FixedElementField;
		}

		// Token: 0x020000B1 RID: 177
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 96)]
		public struct <_SilhouettePlanes>e__FixedBuffer
		{
			// Token: 0x04000396 RID: 918
			public float FixedElementField;
		}

		// Token: 0x020000B2 RID: 178
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 96)]
		public struct <_SrcOffset>e__FixedBuffer
		{
			// Token: 0x04000397 RID: 919
			public uint FixedElementField;
		}
	}
}
