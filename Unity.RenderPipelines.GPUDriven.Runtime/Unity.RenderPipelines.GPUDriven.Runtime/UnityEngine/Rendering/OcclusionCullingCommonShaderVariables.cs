using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections;

namespace UnityEngine.Rendering
{
	// Token: 0x020000C1 RID: 193
	[GenerateHLSL(PackingRules.Exact, true, false, false, 1, false, false, false, -1, "./Library/PackageCache/com.unity.render-pipelines.core/Runtime/GPUDriven/OcclusionCullingCommonShaderVariables.cs", needAccessors = false, generateCBuffer = true)]
	internal struct OcclusionCullingCommonShaderVariables
	{
		// Token: 0x060002E4 RID: 740 RVA: 0x00012F00 File Offset: 0x00011100
		internal unsafe OcclusionCullingCommonShaderVariables(in OccluderContext occluderCtx, in InstanceOcclusionTestSubviewSettings subviewSettings, bool occlusionOverlayCountVisible, bool overrideOcclusionTestToAlwaysPass)
		{
			int i = 0;
			OccluderContext occluderContext;
			for (;;)
			{
				int num = i;
				occluderContext = occluderCtx;
				if (num >= occluderContext.subviewCount)
				{
					break;
				}
				occluderContext = occluderCtx;
				if (occluderContext.IsSubviewValid(i))
				{
					for (int j = 0; j < 16; j++)
					{
						ref float ptr = (ref this._ViewProjMatrix.FixedElementField) + (IntPtr)(16 * i + j) * 4;
						NativeArray<OccluderDerivedData> nativeArray = occluderCtx.subviewData;
						OccluderDerivedData occluderDerivedData = nativeArray[i];
						ptr = occluderDerivedData.viewProjMatrix[j];
					}
					for (int k = 0; k < 4; k++)
					{
						ref float ptr2 = (ref this._ViewOriginWorldSpace.FixedElementField) + (IntPtr)(4 * i + k) * 4;
						NativeArray<OccluderDerivedData> nativeArray = occluderCtx.subviewData;
						OccluderDerivedData occluderDerivedData = nativeArray[i];
						ptr2 = occluderDerivedData.viewOriginWorldSpace[k];
						ref float ptr3 = (ref this._FacingDirWorldSpace.FixedElementField) + (IntPtr)(4 * i + k) * 4;
						nativeArray = occluderCtx.subviewData;
						occluderDerivedData = nativeArray[i];
						ptr3 = occluderDerivedData.facingDirWorldSpace[k];
						ref float ptr4 = (ref this._RadialDirWorldSpace.FixedElementField) + (IntPtr)(4 * i + k) * 4;
						nativeArray = occluderCtx.subviewData;
						occluderDerivedData = nativeArray[i];
						ptr4 = occluderDerivedData.radialDirWorldSpace[k];
					}
				}
				i++;
			}
			Vector2Int vector2Int = occluderCtx.occluderMipLayoutSize;
			this._OccluderMipLayoutSizeX = (uint)vector2Int.x;
			vector2Int = occluderCtx.occluderMipLayoutSize;
			this._OccluderMipLayoutSizeY = (uint)vector2Int.y;
			this._OcclusionTestDebugFlags = (overrideOcclusionTestToAlwaysPass ? 1U : 0U) | (occlusionOverlayCountVisible ? 2U : 0U);
			this._OcclusionCullingCommonPad0 = 0U;
			this._OcclusionTestCount = subviewSettings.testCount;
			this._OccluderSubviewIndices = subviewSettings.occluderSubviewIndices;
			this._CullingSplitIndices = subviewSettings.cullingSplitIndices;
			this._CullingSplitMask = subviewSettings.cullingSplitMask;
			occluderContext = occluderCtx;
			this._DepthSizeInOccluderPixels = occluderContext.depthBufferSizeInOccluderPixels;
			Vector2Int textureSize = occluderCtx.occluderDepthPyramidSize;
			this._OccluderDepthPyramidSize = new Vector4((float)textureSize.x, (float)textureSize.y, 1f / (float)textureSize.x, 1f / (float)textureSize.y);
			int l = 0;
			for (;;)
			{
				int num2 = l;
				NativeArray<OccluderMipBounds> nativeArray2 = occluderCtx.occluderMipBounds;
				if (num2 >= nativeArray2.Length)
				{
					break;
				}
				nativeArray2 = occluderCtx.occluderMipBounds;
				OccluderMipBounds mipBounds = nativeArray2[l];
				*((ref this._OccluderMipBounds.FixedElementField) + (IntPtr)(4 * l) * 4) = (uint)mipBounds.offset.x;
				*((ref this._OccluderMipBounds.FixedElementField) + (IntPtr)(4 * l + 1) * 4) = (uint)mipBounds.offset.y;
				*((ref this._OccluderMipBounds.FixedElementField) + (IntPtr)(4 * l + 2) * 4) = (uint)mipBounds.size.x;
				*((ref this._OccluderMipBounds.FixedElementField) + (IntPtr)(4 * l + 3) * 4) = (uint)mipBounds.size.y;
				l++;
			}
		}

		// Token: 0x040003D7 RID: 983
		[FixedBuffer(typeof(uint), 32)]
		[HLSLArray(8, typeof(ShaderGenUInt4))]
		public OcclusionCullingCommonShaderVariables.<_OccluderMipBounds>e__FixedBuffer _OccluderMipBounds;

		// Token: 0x040003D8 RID: 984
		[FixedBuffer(typeof(float), 96)]
		[HLSLArray(6, typeof(Matrix4x4))]
		public OcclusionCullingCommonShaderVariables.<_ViewProjMatrix>e__FixedBuffer _ViewProjMatrix;

		// Token: 0x040003D9 RID: 985
		[FixedBuffer(typeof(float), 24)]
		[HLSLArray(6, typeof(Vector4))]
		public OcclusionCullingCommonShaderVariables.<_ViewOriginWorldSpace>e__FixedBuffer _ViewOriginWorldSpace;

		// Token: 0x040003DA RID: 986
		[FixedBuffer(typeof(float), 24)]
		[HLSLArray(6, typeof(Vector4))]
		public OcclusionCullingCommonShaderVariables.<_FacingDirWorldSpace>e__FixedBuffer _FacingDirWorldSpace;

		// Token: 0x040003DB RID: 987
		[FixedBuffer(typeof(float), 24)]
		[HLSLArray(6, typeof(Vector4))]
		public OcclusionCullingCommonShaderVariables.<_RadialDirWorldSpace>e__FixedBuffer _RadialDirWorldSpace;

		// Token: 0x040003DC RID: 988
		public Vector4 _DepthSizeInOccluderPixels;

		// Token: 0x040003DD RID: 989
		public Vector4 _OccluderDepthPyramidSize;

		// Token: 0x040003DE RID: 990
		public uint _OccluderMipLayoutSizeX;

		// Token: 0x040003DF RID: 991
		public uint _OccluderMipLayoutSizeY;

		// Token: 0x040003E0 RID: 992
		public uint _OcclusionTestDebugFlags;

		// Token: 0x040003E1 RID: 993
		public uint _OcclusionCullingCommonPad0;

		// Token: 0x040003E2 RID: 994
		public int _OcclusionTestCount;

		// Token: 0x040003E3 RID: 995
		public int _OccluderSubviewIndices;

		// Token: 0x040003E4 RID: 996
		public int _CullingSplitIndices;

		// Token: 0x040003E5 RID: 997
		public int _CullingSplitMask;

		// Token: 0x020000C2 RID: 194
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 96)]
		public struct <_FacingDirWorldSpace>e__FixedBuffer
		{
			// Token: 0x040003E6 RID: 998
			public float FixedElementField;
		}

		// Token: 0x020000C3 RID: 195
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 128)]
		public struct <_OccluderMipBounds>e__FixedBuffer
		{
			// Token: 0x040003E7 RID: 999
			public uint FixedElementField;
		}

		// Token: 0x020000C4 RID: 196
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 96)]
		public struct <_RadialDirWorldSpace>e__FixedBuffer
		{
			// Token: 0x040003E8 RID: 1000
			public float FixedElementField;
		}

		// Token: 0x020000C5 RID: 197
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 96)]
		public struct <_ViewOriginWorldSpace>e__FixedBuffer
		{
			// Token: 0x040003E9 RID: 1001
			public float FixedElementField;
		}

		// Token: 0x020000C6 RID: 198
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 384)]
		public struct <_ViewProjMatrix>e__FixedBuffer
		{
			// Token: 0x040003EA RID: 1002
			public float FixedElementField;
		}
	}
}
