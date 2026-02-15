using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UnityEngine.Rendering
{
	// Token: 0x020000C7 RID: 199
	[GenerateHLSL(PackingRules.Exact, true, false, false, 1, false, false, false, -1, "./Library/PackageCache/com.unity.render-pipelines.core/Runtime/GPUDriven/OcclusionCullingDebugShaderVariables.cs", needAccessors = false, generateCBuffer = true)]
	internal struct OcclusionCullingDebugShaderVariables
	{
		// Token: 0x040003EB RID: 1003
		public Vector4 _DepthSizeInOccluderPixels;

		// Token: 0x040003EC RID: 1004
		[FixedBuffer(typeof(uint), 32)]
		[HLSLArray(8, typeof(ShaderGenUInt4))]
		public OcclusionCullingDebugShaderVariables.<_OccluderMipBounds>e__FixedBuffer _OccluderMipBounds;

		// Token: 0x040003ED RID: 1005
		public uint _OccluderMipLayoutSizeX;

		// Token: 0x040003EE RID: 1006
		public uint _OccluderMipLayoutSizeY;

		// Token: 0x040003EF RID: 1007
		public uint _OcclusionCullingDebugPad0;

		// Token: 0x040003F0 RID: 1008
		public uint _OcclusionCullingDebugPad1;

		// Token: 0x020000C8 RID: 200
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 128)]
		public struct <_OccluderMipBounds>e__FixedBuffer
		{
			// Token: 0x040003F1 RID: 1009
			public uint FixedElementField;
		}
	}
}
