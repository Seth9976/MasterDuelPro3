using System;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering.RenderGraphModule;

namespace UnityEngine.Rendering
{
	// Token: 0x02000098 RID: 152
	internal struct OccluderContext : IDisposable
	{
		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600027B RID: 635 RVA: 0x00010258 File Offset: 0x0000E458
		public int subviewCount
		{
			get
			{
				return this.subviewData.Length;
			}
		}

		// Token: 0x0600027C RID: 636 RVA: 0x00010265 File Offset: 0x0000E465
		public bool IsSubviewValid(int subviewIndex)
		{
			return subviewIndex < this.subviewCount && (this.subviewValidMask & (1 << subviewIndex)) != 0;
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x0600027D RID: 637 RVA: 0x00010284 File Offset: 0x0000E484
		public Vector2 depthBufferSizeInOccluderPixels
		{
			get
			{
				int occluderPixelSize = 8;
				return new Vector2((float)this.depthBufferSize.x / (float)occluderPixelSize, (float)this.depthBufferSize.y / (float)occluderPixelSize);
			}
		}

		// Token: 0x0600027E RID: 638 RVA: 0x000102B8 File Offset: 0x0000E4B8
		public void Dispose()
		{
			if (this.subviewData.IsCreated)
			{
				this.subviewData.Dispose();
			}
			if (this.occluderMipBounds.IsCreated)
			{
				this.occluderMipBounds.Dispose();
			}
			if (this.occluderDepthPyramid != null)
			{
				this.occluderDepthPyramid.Release();
				this.occluderDepthPyramid = null;
			}
			if (this.occlusionDebugOverlay != null)
			{
				this.occlusionDebugOverlay.Release();
				this.occlusionDebugOverlay = null;
			}
			if (this.constantBuffer != null)
			{
				this.constantBuffer.Release();
				this.constantBuffer = null;
			}
			if (this.constantBufferData.IsCreated)
			{
				this.constantBufferData.Dispose();
			}
		}

		// Token: 0x0600027F RID: 639 RVA: 0x0001035C File Offset: 0x0000E55C
		private void UpdateMipBounds()
		{
			int occluderPixelSize = 8;
			Vector2Int vector2Int = (this.depthBufferSize + (occluderPixelSize - 1) * Vector2Int.one) / occluderPixelSize;
			Vector2Int totalSize = Vector2Int.zero;
			Vector2Int mipOffset = Vector2Int.zero;
			Vector2Int mipSize = vector2Int;
			if (!this.occluderMipBounds.IsCreated)
			{
				this.occluderMipBounds = new NativeArray<OccluderMipBounds>(8, Allocator.Persistent, NativeArrayOptions.ClearMemory);
			}
			for (int mipIndex = 0; mipIndex < 8; mipIndex++)
			{
				this.occluderMipBounds[mipIndex] = new OccluderMipBounds
				{
					offset = mipOffset,
					size = mipSize
				};
				totalSize.x = Mathf.Max(totalSize.x, mipOffset.x + mipSize.x);
				totalSize.y = Mathf.Max(totalSize.y, mipOffset.y + mipSize.y);
				if (mipIndex == 0)
				{
					mipOffset.x = 0;
					mipOffset.y += mipSize.y;
				}
				else
				{
					mipOffset.x += mipSize.x;
				}
				mipSize.x = (mipSize.x + 1) / 2;
				mipSize.y = (mipSize.y + 1) / 2;
			}
			this.occluderMipLayoutSize = totalSize;
		}

		// Token: 0x06000280 RID: 640 RVA: 0x00010498 File Offset: 0x0000E698
		private void AllocateTexturesIfNecessary(bool debugOverlayEnabled)
		{
			Vector2Int minDepthPyramidSize = new Vector2Int(this.occluderMipLayoutSize.x, this.occluderMipLayoutSize.y * this.subviewCount);
			if (this.occluderDepthPyramidSize.x < minDepthPyramidSize.x || this.occluderDepthPyramidSize.y < minDepthPyramidSize.y)
			{
				if (this.occluderDepthPyramid != null)
				{
					this.occluderDepthPyramid.Release();
				}
				this.occluderDepthPyramidSize = minDepthPyramidSize;
				this.occluderDepthPyramid = RTHandles.Alloc(this.occluderDepthPyramidSize.x, this.occluderDepthPyramidSize.y, GraphicsFormat.R32_SFloat, 1, FilterMode.Point, TextureWrapMode.Clamp, TextureDimension.Tex2D, true, false, true, false, 1, 0f, MSAASamples.None, false, false, false, RenderTextureMemoryless.None, VRTextureUsage.None, "Occluder Depths");
			}
			int newDebugOverlaySize = (debugOverlayEnabled ? (minDepthPyramidSize.x * minDepthPyramidSize.y) : 0);
			if (this.occlusionDebugOverlaySize < newDebugOverlaySize)
			{
				if (this.occlusionDebugOverlay != null)
				{
					this.occlusionDebugOverlay.Release();
				}
				this.occlusionDebugOverlaySize = newDebugOverlaySize;
				this.debugNeedsClear = true;
				this.occlusionDebugOverlay = new GraphicsBuffer(GraphicsBuffer.Target.Structured, GraphicsBuffer.UsageFlags.None, this.occlusionDebugOverlaySize + 4, 4);
			}
			if (newDebugOverlaySize == 0)
			{
				if (this.occlusionDebugOverlay != null)
				{
					this.occlusionDebugOverlay.Release();
					this.occlusionDebugOverlay = null;
				}
				this.occlusionDebugOverlaySize = newDebugOverlaySize;
			}
			if (this.constantBuffer == null)
			{
				this.constantBuffer = new ComputeBuffer(1, UnsafeUtility.SizeOf<OccluderDepthPyramidConstants>(), ComputeBufferType.Constant);
			}
			if (!this.constantBufferData.IsCreated)
			{
				this.constantBufferData = new NativeArray<OccluderDepthPyramidConstants>(1, Allocator.Persistent, NativeArrayOptions.ClearMemory);
			}
		}

		// Token: 0x06000281 RID: 641 RVA: 0x000105F7 File Offset: 0x0000E7F7
		internal static void SetKeyword(ComputeCommandBuffer cmd, ComputeShader cs, in LocalKeyword keyword, bool value)
		{
			if (value)
			{
				cmd.EnableKeyword(cs, in keyword);
				return;
			}
			cmd.DisableKeyword(cs, in keyword);
		}

		// Token: 0x06000282 RID: 642 RVA: 0x00010610 File Offset: 0x0000E810
		private unsafe OccluderDepthPyramidConstants SetupFarDepthPyramidConstants(ReadOnlySpan<OccluderSubviewUpdate> occluderSubviewUpdates, NativeArray<Plane> silhouettePlanes)
		{
			OccluderDepthPyramidConstants cb = default(OccluderDepthPyramidConstants);
			cb._OccluderMipLayoutSizeX = (uint)this.occluderMipLayoutSize.x;
			cb._OccluderMipLayoutSizeY = (uint)this.occluderMipLayoutSize.y;
			int updateCount = occluderSubviewUpdates.Length;
			for (int updateIndex = 0; updateIndex < updateCount; updateIndex++)
			{
				readonly ref OccluderSubviewUpdate update = ref occluderSubviewUpdates[updateIndex];
				int subviewIndex = update.subviewIndex;
				this.subviewData[subviewIndex] = OccluderDerivedData.FromParameters(in update);
				this.subviewValidMask |= 1 << update.subviewIndex;
				Matrix4x4 invViewProjMatrix = (update.gpuProjMatrix * update.viewMatrix * Matrix4x4.Translate(-update.viewOffsetWorldSpace)).inverse;
				for (int i = 0; i < 16; i++)
				{
					*((ref cb._InvViewProjMatrix.FixedElementField) + (IntPtr)(16 * updateIndex + i) * 4) = invViewProjMatrix[i];
				}
				ref int ptr = (ref cb._SrcOffset.FixedElementField) + (IntPtr)(4 * updateIndex) * 4;
				Vector2Int vector2Int = update.depthOffset;
				ptr = vector2Int.x;
				ref int ptr2 = (ref cb._SrcOffset.FixedElementField) + (IntPtr)(4 * updateIndex + 1) * 4;
				vector2Int = update.depthOffset;
				ptr2 = vector2Int.y;
				*((ref cb._SrcOffset.FixedElementField) + (IntPtr)(4 * updateIndex + 2) * 4) = 0U;
				*((ref cb._SrcOffset.FixedElementField) + (IntPtr)(4 * updateIndex + 3) * 4) = 0U;
				cb._SrcSliceIndices |= (uint)((uint)(update.depthSliceIndex & 15) << 4 * updateIndex);
				cb._DstSubviewIndices |= (uint)((uint)subviewIndex << 4 * updateIndex);
			}
			for (int j = 0; j < 6; j++)
			{
				Plane plane = new Plane(Vector3.zero, 0f);
				if (j < silhouettePlanes.Length)
				{
					plane = silhouettePlanes[j];
				}
				*((ref cb._SilhouettePlanes.FixedElementField) + (IntPtr)(4 * j) * 4) = plane.normal.x;
				*((ref cb._SilhouettePlanes.FixedElementField) + (IntPtr)(4 * j + 1) * 4) = plane.normal.y;
				*((ref cb._SilhouettePlanes.FixedElementField) + (IntPtr)(4 * j + 2) * 4) = plane.normal.z;
				*((ref cb._SilhouettePlanes.FixedElementField) + (IntPtr)(4 * j + 3) * 4) = plane.distance;
			}
			cb._SilhouettePlaneCount = (uint)silhouettePlanes.Length;
			return cb;
		}

		// Token: 0x06000283 RID: 643 RVA: 0x00010878 File Offset: 0x0000EA78
		public unsafe void CreateFarDepthPyramid(ComputeCommandBuffer cmd, in OccluderParameters occluderParams, ReadOnlySpan<OccluderSubviewUpdate> occluderSubviewUpdates, in OccluderHandles occluderHandles, NativeArray<Plane> silhouettePlanes, ComputeShader occluderDepthPyramidCS, int occluderDepthDownscaleKernel)
		{
			OccluderDepthPyramidConstants cb = this.SetupFarDepthPyramidConstants(occluderSubviewUpdates, silhouettePlanes);
			LocalKeyword srcKeyword = new LocalKeyword(occluderDepthPyramidCS, "USE_SRC");
			LocalKeyword srcIsArrayKeyword = new LocalKeyword(occluderDepthPyramidCS, "SRC_IS_ARRAY");
			LocalKeyword srcIsMsaaKeyword = new LocalKeyword(occluderDepthPyramidCS, "SRC_IS_MSAA");
			bool srcIsArray = occluderParams.depthIsArray;
			RTHandle rthandle = occluderParams.depthTexture;
			bool srcIsMsaa = rthandle != null && rthandle.isMSAAEnabled;
			int mipCount = 11;
			for (int mipIndexBase = 0; mipIndexBase < mipCount - 1; mipIndexBase += 4)
			{
				cmd.SetComputeTextureParam(occluderDepthPyramidCS, occluderDepthDownscaleKernel, OccluderContext.ShaderIDs._DstDepth, occluderHandles.occluderDepthPyramid);
				bool useSrc = mipIndexBase == 0;
				OccluderContext.SetKeyword(cmd, occluderDepthPyramidCS, in srcKeyword, useSrc);
				OccluderContext.SetKeyword(cmd, occluderDepthPyramidCS, in srcIsArrayKeyword, useSrc && srcIsArray);
				OccluderContext.SetKeyword(cmd, occluderDepthPyramidCS, in srcIsMsaaKeyword, useSrc && srcIsMsaa);
				if (useSrc)
				{
					cmd.SetComputeTextureParam(occluderDepthPyramidCS, occluderDepthDownscaleKernel, OccluderContext.ShaderIDs._SrcDepth, occluderParams.depthTexture);
				}
				cb._MipCount = (uint)Math.Min(mipCount - 1 - mipIndexBase, 4);
				Vector2Int srcSize = Vector2Int.zero;
				for (int i = 0; i < 5; i++)
				{
					Vector2Int offset = Vector2Int.zero;
					Vector2Int size = Vector2Int.zero;
					int mipIndex = mipIndexBase + i;
					if (mipIndex == 0)
					{
						size = occluderParams.depthSize;
					}
					else
					{
						int occMipIndex = mipIndex - 3;
						if (0 <= occMipIndex && occMipIndex < 8)
						{
							offset = this.occluderMipBounds[occMipIndex].offset;
							size = this.occluderMipBounds[occMipIndex].size;
						}
					}
					if (i == 0)
					{
						srcSize = size;
					}
					*((ref cb._MipOffsetAndSize.FixedElementField) + (IntPtr)(4 * i) * 4) = (uint)offset.x;
					*((ref cb._MipOffsetAndSize.FixedElementField) + (IntPtr)(4 * i + 1) * 4) = (uint)offset.y;
					*((ref cb._MipOffsetAndSize.FixedElementField) + (IntPtr)(4 * i + 2) * 4) = (uint)size.x;
					*((ref cb._MipOffsetAndSize.FixedElementField) + (IntPtr)(4 * i + 3) * 4) = (uint)size.y;
				}
				this.constantBufferData[0] = cb;
				cmd.SetBufferData<OccluderDepthPyramidConstants>(this.constantBuffer, this.constantBufferData);
				cmd.SetComputeConstantBufferParam(occluderDepthPyramidCS, OccluderContext.ShaderIDs.OccluderDepthPyramidConstants, this.constantBuffer, 0, this.constantBuffer.stride);
				cmd.DispatchCompute(occluderDepthPyramidCS, occluderDepthDownscaleKernel, (srcSize.x + 15) / 16, (srcSize.y + 15) / 16, occluderSubviewUpdates.Length);
			}
		}

		// Token: 0x06000284 RID: 644 RVA: 0x00010AC4 File Offset: 0x0000ECC4
		public OccluderHandles Import(RenderGraph renderGraph)
		{
			RenderTargetInfo rtInfo = new RenderTargetInfo
			{
				width = this.occluderDepthPyramidSize.x,
				height = this.occluderDepthPyramidSize.y,
				volumeDepth = 1,
				msaaSamples = 1,
				format = GraphicsFormat.R32_SFloat,
				bindMS = false
			};
			OccluderHandles occluderHandles = new OccluderHandles
			{
				occluderDepthPyramid = renderGraph.ImportTexture(this.occluderDepthPyramid, rtInfo, default(ImportResourceParams))
			};
			if (this.occlusionDebugOverlay != null)
			{
				occluderHandles.occlusionDebugOverlay = renderGraph.ImportBuffer(this.occlusionDebugOverlay, false);
			}
			return occluderHandles;
		}

		// Token: 0x06000285 RID: 645 RVA: 0x00010B68 File Offset: 0x0000ED68
		public void PrepareOccluders(in OccluderParameters occluderParams)
		{
			if (this.subviewCount != occluderParams.subviewCount)
			{
				if (this.subviewData.IsCreated)
				{
					this.subviewData.Dispose();
				}
				this.subviewData = new NativeArray<OccluderDerivedData>(occluderParams.subviewCount, Allocator.Persistent, NativeArrayOptions.ClearMemory);
				this.subviewValidMask = 0;
			}
			this.depthBufferSize = occluderParams.depthSize;
			DebugRendererBatcherStats debugStats = GPUResidentDrawer.GetDebugStats();
			bool debugOverlayEnabled = debugStats != null && debugStats.occlusionOverlayEnabled;
			this.UpdateMipBounds();
			this.AllocateTexturesIfNecessary(debugOverlayEnabled);
		}

		// Token: 0x06000286 RID: 646 RVA: 0x00010BE0 File Offset: 0x0000EDE0
		internal unsafe OcclusionCullingDebugOutput GetDebugOutput()
		{
			OcclusionCullingDebugOutput debugOutput = new OcclusionCullingDebugOutput
			{
				occluderDepthPyramid = this.occluderDepthPyramid,
				occlusionDebugOverlay = this.occlusionDebugOverlay
			};
			debugOutput.cb._DepthSizeInOccluderPixels = this.depthBufferSizeInOccluderPixels;
			debugOutput.cb._OccluderMipLayoutSizeX = (uint)this.occluderMipLayoutSize.x;
			debugOutput.cb._OccluderMipLayoutSizeY = (uint)this.occluderMipLayoutSize.y;
			for (int i = 0; i < this.occluderMipBounds.Length; i++)
			{
				OccluderMipBounds mipBounds = this.occluderMipBounds[i];
				*((ref debugOutput.cb._OccluderMipBounds.FixedElementField) + (IntPtr)(4 * i) * 4) = (uint)mipBounds.offset.x;
				*((ref debugOutput.cb._OccluderMipBounds.FixedElementField) + (IntPtr)(4 * i + 1) * 4) = (uint)mipBounds.offset.y;
				*((ref debugOutput.cb._OccluderMipBounds.FixedElementField) + (IntPtr)(4 * i + 2) * 4) = (uint)mipBounds.size.x;
				*((ref debugOutput.cb._OccluderMipBounds.FixedElementField) + (IntPtr)(4 * i + 3) * 4) = (uint)mipBounds.size.y;
			}
			return debugOutput;
		}

		// Token: 0x04000313 RID: 787
		public const int k_FirstDepthMipIndex = 3;

		// Token: 0x04000314 RID: 788
		public const int k_MaxOccluderMips = 8;

		// Token: 0x04000315 RID: 789
		public const int k_MaxSilhouettePlanes = 6;

		// Token: 0x04000316 RID: 790
		public const int k_MaxSubviewsPerView = 6;

		// Token: 0x04000317 RID: 791
		public int version;

		// Token: 0x04000318 RID: 792
		public Vector2Int depthBufferSize;

		// Token: 0x04000319 RID: 793
		public NativeArray<OccluderDerivedData> subviewData;

		// Token: 0x0400031A RID: 794
		public int subviewValidMask;

		// Token: 0x0400031B RID: 795
		public NativeArray<OccluderMipBounds> occluderMipBounds;

		// Token: 0x0400031C RID: 796
		public Vector2Int occluderMipLayoutSize;

		// Token: 0x0400031D RID: 797
		public Vector2Int occluderDepthPyramidSize;

		// Token: 0x0400031E RID: 798
		public RTHandle occluderDepthPyramid;

		// Token: 0x0400031F RID: 799
		public int occlusionDebugOverlaySize;

		// Token: 0x04000320 RID: 800
		public GraphicsBuffer occlusionDebugOverlay;

		// Token: 0x04000321 RID: 801
		public bool debugNeedsClear;

		// Token: 0x04000322 RID: 802
		public ComputeBuffer constantBuffer;

		// Token: 0x04000323 RID: 803
		public NativeArray<OccluderDepthPyramidConstants> constantBufferData;

		// Token: 0x02000099 RID: 153
		private static class ShaderIDs
		{
			// Token: 0x04000324 RID: 804
			public static readonly int _SrcDepth = Shader.PropertyToID("_SrcDepth");

			// Token: 0x04000325 RID: 805
			public static readonly int _DstDepth = Shader.PropertyToID("_DstDepth");

			// Token: 0x04000326 RID: 806
			public static readonly int OccluderDepthPyramidConstants = Shader.PropertyToID("OccluderDepthPyramidConstants");
		}
	}
}
