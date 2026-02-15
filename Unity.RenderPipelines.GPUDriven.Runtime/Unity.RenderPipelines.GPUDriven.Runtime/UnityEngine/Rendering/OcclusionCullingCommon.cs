using System;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Rendering.RenderGraphModule;

namespace UnityEngine.Rendering
{
	// Token: 0x020000B8 RID: 184
	internal class OcclusionCullingCommon : IDisposable
	{
		// Token: 0x060002C3 RID: 707 RVA: 0x00012118 File Offset: 0x00010318
		internal void Init(GPUResidentDrawerResources resources)
		{
			this.m_DebugOcclusionTestMaterial = CoreUtils.CreateEngineMaterial(resources.debugOcclusionTestPS);
			this.m_OccluderDebugViewMaterial = CoreUtils.CreateEngineMaterial(resources.debugOccluderPS);
			this.m_OcclusionDebugCS = resources.occlusionCullingDebugKernels;
			this.m_ClearOcclusionDebugKernel = this.m_OcclusionDebugCS.FindKernel("ClearOcclusionDebug");
			this.m_OccluderDepthPyramidCS = resources.occluderDepthPyramidKernels;
			this.m_OccluderDepthDownscaleKernel = this.m_OccluderDepthPyramidCS.FindKernel("OccluderDepthDownscale");
			this.m_SilhouettePlaneCache.Init();
			this.m_ViewIDToIndexMap = new NativeParallelHashMap<int, int>(64, Allocator.Persistent);
			this.m_OccluderContextData = new List<OccluderContext>();
			this.m_OccluderContextSlots = new NativeList<OcclusionCullingCommon.OccluderContextSlot>(64, Allocator.Persistent);
			this.m_FreeOccluderContexts = new NativeList<int>(64, Allocator.Persistent);
			this.m_ProfilingSamplerUpdateOccluders = new ProfilingSampler("UpdateOccluders");
			this.m_ProfilingSamplerOcclusionTestOverlay = new ProfilingSampler("OcclusionTestOverlay");
			this.m_ProfilingSamplerOccluderOverlay = new ProfilingSampler("OccluderOverlay");
			this.m_CommonShaderVariables = new NativeArray<OcclusionCullingCommonShaderVariables>(1, Allocator.Persistent, NativeArrayOptions.ClearMemory);
			this.m_CommonConstantBuffer = new ComputeBuffer(1, UnsafeUtility.SizeOf<OcclusionCullingCommonShaderVariables>(), ComputeBufferType.Constant);
			this.m_DebugShaderVariables = new NativeArray<OcclusionCullingDebugShaderVariables>(1, Allocator.Persistent, NativeArrayOptions.ClearMemory);
			this.m_DebugConstantBuffer = new ComputeBuffer(1, UnsafeUtility.SizeOf<OcclusionCullingDebugShaderVariables>(), ComputeBufferType.Constant);
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x0001224A File Offset: 0x0001044A
		internal static bool UseOcclusionDebug(in OccluderContext occluderCtx)
		{
			return occluderCtx.occlusionDebugOverlaySize != 0;
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x00012258 File Offset: 0x00010458
		internal void PrepareCulling(ComputeCommandBuffer cmd, in OccluderContext occluderCtx, in OcclusionCullingSettings settings, in InstanceOcclusionTestSubviewSettings subviewSettings, in OcclusionTestComputeShader shader, bool useOcclusionDebug)
		{
			OccluderContext.SetKeyword(cmd, shader.cs, in shader.occlusionDebugKeyword, useOcclusionDebug);
			DebugRendererBatcherStats debugStats = GPUResidentDrawer.GetDebugStats();
			this.m_CommonShaderVariables[0] = new OcclusionCullingCommonShaderVariables(in occluderCtx, in subviewSettings, debugStats != null && debugStats.occlusionOverlayCountVisible, debugStats != null && debugStats.overrideOcclusionTestToAlwaysPass);
			cmd.SetBufferData<OcclusionCullingCommonShaderVariables>(this.m_CommonConstantBuffer, this.m_CommonShaderVariables);
			cmd.SetComputeConstantBufferParam(shader.cs, OcclusionCullingCommon.ShaderIDs.OcclusionCullingCommonShaderVariables, this.m_CommonConstantBuffer, 0, this.m_CommonConstantBuffer.stride);
			this.DispatchDebugClear(cmd, settings.viewInstanceID);
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x000122F0 File Offset: 0x000104F0
		internal static void SetDepthPyramid(ComputeCommandBuffer cmd, in OcclusionTestComputeShader shader, int kernel, in OccluderHandles occluderHandles)
		{
			cmd.SetComputeTextureParam(shader.cs, kernel, OcclusionCullingCommon.ShaderIDs._OccluderDepthPyramid, occluderHandles.occluderDepthPyramid);
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x0001230A File Offset: 0x0001050A
		internal static void SetDebugPyramid(ComputeCommandBuffer cmd, in OcclusionTestComputeShader shader, int kernel, in OccluderHandles occluderHandles)
		{
			cmd.SetComputeBufferParam(shader.cs, kernel, OcclusionCullingCommon.ShaderIDs._OcclusionDebugOverlay, occluderHandles.occlusionDebugOverlay);
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x0001232C File Offset: 0x0001052C
		public void RenderDebugOcclusionTestOverlay(RenderGraph renderGraph, DebugDisplayGPUResidentDrawer debugSettings, int viewInstanceID, TextureHandle colorBuffer)
		{
			if (debugSettings == null)
			{
				return;
			}
			if (!debugSettings.occlusionTestOverlayEnable)
			{
				return;
			}
			OcclusionCullingDebugOutput debugOutput = this.GetOcclusionTestDebugOutput(viewInstanceID);
			if (debugOutput.occlusionDebugOverlay == null)
			{
				return;
			}
			OcclusionCullingCommon.OcclusionTestOverlaySetupPassData passData;
			using (IComputeRenderGraphBuilder builder = renderGraph.AddComputePass<OcclusionCullingCommon.OcclusionTestOverlaySetupPassData>("OcclusionTestOverlay", out passData, this.m_ProfilingSamplerOcclusionTestOverlay, "./Library/PackageCache/com.unity.render-pipelines.core/Runtime/GPUDriven/OcclusionCullingCommon.cs", 275))
			{
				builder.AllowPassCulling(false);
				passData.cb = debugOutput.cb;
				builder.SetRenderFunc<OcclusionCullingCommon.OcclusionTestOverlaySetupPassData>(delegate(OcclusionCullingCommon.OcclusionTestOverlaySetupPassData data, ComputeGraphContext ctx)
				{
					OcclusionCullingCommon occ = GPUResidentDrawer.instance.batcher.occlusionCullingCommon;
					occ.m_DebugShaderVariables[0] = data.cb;
					ctx.cmd.SetBufferData<OcclusionCullingDebugShaderVariables>(occ.m_DebugConstantBuffer, occ.m_DebugShaderVariables);
					occ.m_DebugOcclusionTestMaterial.SetConstantBuffer(OcclusionCullingCommon.ShaderIDs.OcclusionCullingDebugShaderVariables, occ.m_DebugConstantBuffer, 0, occ.m_DebugConstantBuffer.stride);
				});
			}
			OcclusionCullingCommon.OcclusionTestOverlayPassData passData2;
			using (IRasterRenderGraphBuilder builder2 = renderGraph.AddRasterRenderPass<OcclusionCullingCommon.OcclusionTestOverlayPassData>("OcclusionTestOverlay", out passData2, this.m_ProfilingSamplerOcclusionTestOverlay, "./Library/PackageCache/com.unity.render-pipelines.core/Runtime/GPUDriven/OcclusionCullingCommon.cs", 297))
			{
				builder2.AllowGlobalStateModification(true);
				passData2.debugPyramid = renderGraph.ImportBuffer(debugOutput.occlusionDebugOverlay, false);
				builder2.SetRenderAttachment(colorBuffer, 0, AccessFlags.Write);
				builder2.UseBuffer(in passData2.debugPyramid, AccessFlags.Read);
				builder2.SetRenderFunc<OcclusionCullingCommon.OcclusionTestOverlayPassData>(delegate(OcclusionCullingCommon.OcclusionTestOverlayPassData data, RasterGraphContext ctx)
				{
					ctx.cmd.SetGlobalBuffer(OcclusionCullingCommon.ShaderIDs._OcclusionDebugOverlay, data.debugPyramid);
					CoreUtils.DrawFullScreen(ctx.cmd, this.m_DebugOcclusionTestMaterial, null, 0);
				});
			}
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x00012448 File Offset: 0x00010648
		public void RenderDebugOccluderOverlay(RenderGraph renderGraph, DebugDisplayGPUResidentDrawer debugSettings, Vector2 screenPos, float maxHeight, TextureHandle colorBuffer)
		{
			if (debugSettings == null)
			{
				return;
			}
			if (!debugSettings.occluderDebugViewEnable)
			{
				return;
			}
			int viewInstanceID;
			if (!debugSettings.GetOccluderViewInstanceID(out viewInstanceID))
			{
				return;
			}
			RTHandle occluderTexture = this.GetOcclusionTestDebugOutput(viewInstanceID).occluderDepthPyramid;
			if (occluderTexture == null)
			{
				return;
			}
			Material debugMaterial = this.m_OccluderDebugViewMaterial;
			int passIndex = debugMaterial.FindPass("DebugOccluder");
			Vector2 outputSize = occluderTexture.referenceSize;
			float scaleFactor = maxHeight / outputSize.y;
			outputSize *= scaleFactor;
			Rect viewport = new Rect(screenPos.x, screenPos.y, outputSize.x, outputSize.y);
			OcclusionCullingCommon.OccluderOverlayPassData passData;
			using (IRasterRenderGraphBuilder builder = renderGraph.AddRasterRenderPass<OcclusionCullingCommon.OccluderOverlayPassData>("OccluderOverlay", out passData, this.m_ProfilingSamplerOccluderOverlay, "./Library/PackageCache/com.unity.render-pipelines.core/Runtime/GPUDriven/OcclusionCullingCommon.cs", 353))
			{
				builder.AllowGlobalStateModification(true);
				builder.SetRenderAttachment(colorBuffer, 0, AccessFlags.Write);
				passData.debugMaterial = debugMaterial;
				passData.occluderTexture = occluderTexture;
				passData.viewport = viewport;
				passData.passIndex = passIndex;
				passData.validRange = debugSettings.occluderDebugViewRange;
				builder.SetRenderFunc<OcclusionCullingCommon.OccluderOverlayPassData>(delegate(OcclusionCullingCommon.OccluderOverlayPassData data, RasterGraphContext ctx)
				{
					MaterialPropertyBlock mpb = ctx.renderGraphPool.GetTempMaterialPropertyBlock();
					mpb.SetTexture("_OccluderTexture", data.occluderTexture);
					mpb.SetVector("_ValidRange", data.validRange);
					ctx.cmd.SetViewport(data.viewport);
					ctx.cmd.DrawProcedural(Matrix4x4.identity, data.debugMaterial, data.passIndex, MeshTopology.Triangles, 3, 1, mpb);
				});
			}
		}

		// Token: 0x060002CA RID: 714 RVA: 0x0001257C File Offset: 0x0001077C
		private void DispatchDebugClear(ComputeCommandBuffer cmd, int viewInstanceID)
		{
			int contextIndex;
			if (!this.m_ViewIDToIndexMap.TryGetValue(viewInstanceID, out contextIndex))
			{
				return;
			}
			OccluderContext occluderCtx = this.m_OccluderContextData[contextIndex];
			if (OcclusionCullingCommon.UseOcclusionDebug(in occluderCtx) && occluderCtx.debugNeedsClear)
			{
				ComputeShader cs = this.m_OcclusionDebugCS;
				int kernel = this.m_ClearOcclusionDebugKernel;
				cmd.SetComputeConstantBufferParam(cs, OcclusionCullingCommon.ShaderIDs.OcclusionCullingCommonShaderVariables, this.m_CommonConstantBuffer, 0, this.m_CommonConstantBuffer.stride);
				cmd.SetComputeBufferParam(cs, kernel, OcclusionCullingCommon.ShaderIDs._OcclusionDebugOverlay, occluderCtx.occlusionDebugOverlay);
				Vector2Int mip0Size = occluderCtx.occluderMipBounds[0].size;
				cmd.DispatchCompute(cs, kernel, (mip0Size.x + 7) / 8, (mip0Size.y + 7) / 8, occluderCtx.subviewCount);
				occluderCtx.debugNeedsClear = false;
				this.m_OccluderContextData[contextIndex] = occluderCtx;
			}
		}

		// Token: 0x060002CB RID: 715 RVA: 0x0001264C File Offset: 0x0001084C
		private OccluderHandles PrepareOccluders(RenderGraph renderGraph, in OccluderParameters occluderParams)
		{
			OccluderHandles occluderHandles = default(OccluderHandles);
			TextureHandle depthTexture = occluderParams.depthTexture;
			if (depthTexture.IsValid())
			{
				int contextIndex;
				if (!this.m_ViewIDToIndexMap.TryGetValue(occluderParams.viewInstanceID, out contextIndex))
				{
					contextIndex = this.NewContext(occluderParams.viewInstanceID);
				}
				OccluderContext ctx = this.m_OccluderContextData[contextIndex];
				ctx.PrepareOccluders(in occluderParams);
				occluderHandles = ctx.Import(renderGraph);
				this.m_OccluderContextData[contextIndex] = ctx;
			}
			else
			{
				this.DeleteContext(occluderParams.viewInstanceID);
			}
			return occluderHandles;
		}

		// Token: 0x060002CC RID: 716 RVA: 0x000126D0 File Offset: 0x000108D0
		private void CreateFarDepthPyramid(ComputeCommandBuffer cmd, in OccluderParameters occluderParams, ReadOnlySpan<OccluderSubviewUpdate> occluderSubviewUpdates, in OccluderHandles occluderHandles)
		{
			int contextIndex;
			if (!this.m_ViewIDToIndexMap.TryGetValue(occluderParams.viewInstanceID, out contextIndex))
			{
				return;
			}
			NativeArray<Plane> silhouettePlanes = this.m_SilhouettePlaneCache.GetSubArray(occluderParams.viewInstanceID);
			OccluderContext ctx = this.m_OccluderContextData[contextIndex];
			ctx.CreateFarDepthPyramid(cmd, in occluderParams, occluderSubviewUpdates, in occluderHandles, silhouettePlanes, this.m_OccluderDepthPyramidCS, this.m_OccluderDepthDownscaleKernel);
			ctx.version++;
			this.m_OccluderContextData[contextIndex] = ctx;
			OcclusionCullingCommon.OccluderContextSlot slot = this.m_OccluderContextSlots[contextIndex];
			slot.lastUsedFrameIndex = this.m_FrameIndex;
			this.m_OccluderContextSlots[contextIndex] = slot;
		}

		// Token: 0x060002CD RID: 717 RVA: 0x0001276C File Offset: 0x0001096C
		public unsafe bool UpdateInstanceOccluders(RenderGraph renderGraph, in OccluderParameters occluderParams, ReadOnlySpan<OccluderSubviewUpdate> occluderSubviewUpdates)
		{
			OccluderHandles occluderHandles = this.PrepareOccluders(renderGraph, in occluderParams);
			if (!occluderHandles.occluderDepthPyramid.IsValid())
			{
				return false;
			}
			OcclusionCullingCommon.UpdateOccludersPassData passData;
			using (IComputeRenderGraphBuilder builder = renderGraph.AddComputePass<OcclusionCullingCommon.UpdateOccludersPassData>("Update Occluders", out passData, this.m_ProfilingSamplerUpdateOccluders, "./Library/PackageCache/com.unity.render-pipelines.core/Runtime/GPUDriven/OcclusionCullingCommon.cs", 454))
			{
				builder.AllowGlobalStateModification(true);
				passData.occluderParams = occluderParams;
				if (passData.occluderSubviewUpdates == null)
				{
					passData.occluderSubviewUpdates = new List<OccluderSubviewUpdate>();
				}
				else
				{
					passData.occluderSubviewUpdates.Clear();
				}
				for (int i = 0; i < occluderSubviewUpdates.Length; i++)
				{
					passData.occluderSubviewUpdates.Add(*occluderSubviewUpdates[i]);
				}
				passData.occluderHandles = occluderHandles;
				builder.UseTexture(in passData.occluderParams.depthTexture, AccessFlags.Read);
				passData.occluderHandles.UseForOccluderUpdate(builder);
				builder.SetRenderFunc<OcclusionCullingCommon.UpdateOccludersPassData>(delegate(OcclusionCullingCommon.UpdateOccludersPassData data, ComputeGraphContext context)
				{
					int count = data.occluderSubviewUpdates.Count;
					Span<OccluderSubviewUpdate> occluderSubviewUpdates2;
					int subviewMask;
					checked
					{
						occluderSubviewUpdates2 = new Span<OccluderSubviewUpdate>(stackalloc byte[unchecked((UIntPtr)count) * (UIntPtr)sizeof(OccluderSubviewUpdate)], count);
						subviewMask = 0;
					}
					for (int j = 0; j < data.occluderSubviewUpdates.Count; j++)
					{
						*occluderSubviewUpdates2[j] = data.occluderSubviewUpdates[j];
						subviewMask |= 1 << data.occluderSubviewUpdates[j].subviewIndex;
					}
					GPUResidentBatcher batcher = GPUResidentDrawer.instance.batcher;
					batcher.occlusionCullingCommon.CreateFarDepthPyramid(context.cmd, in data.occluderParams, occluderSubviewUpdates2, in data.occluderHandles);
					batcher.instanceCullingBatcher.InstanceOccludersUpdated(data.occluderParams.viewInstanceID, subviewMask);
				});
			}
			return true;
		}

		// Token: 0x060002CE RID: 718 RVA: 0x00012874 File Offset: 0x00010A74
		internal void UpdateSilhouettePlanes(int viewInstanceID, NativeArray<Plane> planes)
		{
			this.m_SilhouettePlaneCache.Update(viewInstanceID, planes, this.m_FrameIndex);
		}

		// Token: 0x060002CF RID: 719 RVA: 0x0001288C File Offset: 0x00010A8C
		internal OcclusionCullingDebugOutput GetOcclusionTestDebugOutput(int viewInstanceID)
		{
			int contextIndex;
			if (this.m_ViewIDToIndexMap.TryGetValue(viewInstanceID, out contextIndex) && this.m_OccluderContextSlots[contextIndex].valid)
			{
				return this.m_OccluderContextData[contextIndex].GetDebugOutput();
			}
			return default(OcclusionCullingDebugOutput);
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x000128DC File Offset: 0x00010ADC
		public unsafe void UpdateOccluderStats(DebugRendererBatcherStats debugStats)
		{
			debugStats.occluderStats.Clear();
			foreach (KeyValue<int, int> pair in this.m_ViewIDToIndexMap)
			{
				if (*pair.Value < this.m_OccluderContextSlots.Length && this.m_OccluderContextSlots[*pair.Value].valid)
				{
					DebugOccluderStats debugOccluderStats = default(DebugOccluderStats);
					debugOccluderStats.viewInstanceID = pair.Key;
					debugOccluderStats.subviewCount = this.m_OccluderContextData[*pair.Value].subviewCount;
					debugOccluderStats.occluderMipLayoutSize = this.m_OccluderContextData[*pair.Value].occluderMipLayoutSize;
					debugStats.occluderStats.Add(in debugOccluderStats);
				}
			}
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x000129CC File Offset: 0x00010BCC
		internal bool HasOccluderContext(int viewInstanceID)
		{
			return this.m_ViewIDToIndexMap.ContainsKey(viewInstanceID);
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x000129DC File Offset: 0x00010BDC
		internal bool GetOccluderContext(int viewInstanceID, out OccluderContext occluderContext)
		{
			int contextIndex;
			if (this.m_ViewIDToIndexMap.TryGetValue(viewInstanceID, out contextIndex) && this.m_OccluderContextSlots[contextIndex].valid)
			{
				occluderContext = this.m_OccluderContextData[contextIndex];
				return true;
			}
			occluderContext = default(OccluderContext);
			return false;
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x00012A28 File Offset: 0x00010C28
		internal void UpdateFrame()
		{
			for (int i = 0; i < this.m_OccluderContextData.Count; i++)
			{
				if (this.m_OccluderContextSlots[i].valid)
				{
					OccluderContext occluderCtx = this.m_OccluderContextData[i];
					OcclusionCullingCommon.OccluderContextSlot slot = this.m_OccluderContextSlots[i];
					if (this.m_FrameIndex - slot.lastUsedFrameIndex >= OcclusionCullingCommon.s_MaxContextGCFrame)
					{
						this.DeleteContext(slot.viewInstanceID);
					}
					else
					{
						occluderCtx.debugNeedsClear = true;
						this.m_OccluderContextData[i] = occluderCtx;
					}
				}
			}
			this.m_SilhouettePlaneCache.FreeUnusedSlots(this.m_FrameIndex, OcclusionCullingCommon.s_MaxContextGCFrame);
			this.m_FrameIndex++;
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x00012AD4 File Offset: 0x00010CD4
		private int NewContext(int viewInstanceID)
		{
			OcclusionCullingCommon.OccluderContextSlot newCtxSlot = new OcclusionCullingCommon.OccluderContextSlot
			{
				valid = true,
				viewInstanceID = viewInstanceID,
				lastUsedFrameIndex = this.m_FrameIndex
			};
			OccluderContext newCtx = default(OccluderContext);
			int newSlot;
			if (this.m_FreeOccluderContexts.Length > 0)
			{
				newSlot = this.m_FreeOccluderContexts[this.m_FreeOccluderContexts.Length - 1];
				this.m_FreeOccluderContexts.RemoveAt(this.m_FreeOccluderContexts.Length - 1);
				this.m_OccluderContextData[newSlot] = newCtx;
				this.m_OccluderContextSlots[newSlot] = newCtxSlot;
			}
			else
			{
				newSlot = this.m_OccluderContextData.Count;
				this.m_OccluderContextData.Add(newCtx);
				this.m_OccluderContextSlots.Add(in newCtxSlot);
			}
			this.m_ViewIDToIndexMap.Add(viewInstanceID, newSlot);
			return newSlot;
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x00012BA4 File Offset: 0x00010DA4
		private void DeleteContext(int viewInstanceID)
		{
			int contextIndex;
			if (!this.m_ViewIDToIndexMap.TryGetValue(viewInstanceID, out contextIndex) || !this.m_OccluderContextSlots[contextIndex].valid)
			{
				return;
			}
			this.m_OccluderContextData[contextIndex].Dispose();
			this.m_OccluderContextSlots[contextIndex] = new OcclusionCullingCommon.OccluderContextSlot
			{
				valid = false
			};
			this.m_FreeOccluderContexts.Add(in contextIndex);
			this.m_ViewIDToIndexMap.Remove(viewInstanceID);
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x00012C20 File Offset: 0x00010E20
		public void Dispose()
		{
			CoreUtils.Destroy(this.m_DebugOcclusionTestMaterial);
			CoreUtils.Destroy(this.m_OccluderDebugViewMaterial);
			for (int i = 0; i < this.m_OccluderContextData.Count; i++)
			{
				if (this.m_OccluderContextSlots[i].valid)
				{
					this.m_OccluderContextData[i].Dispose();
				}
			}
			this.m_SilhouettePlaneCache.Dispose();
			this.m_ViewIDToIndexMap.Dispose();
			this.m_FreeOccluderContexts.Dispose();
			this.m_OccluderContextData.Clear();
			this.m_OccluderContextSlots.Dispose();
			this.m_CommonShaderVariables.Dispose();
			this.m_CommonConstantBuffer.Release();
			this.m_DebugShaderVariables.Dispose();
			this.m_DebugConstantBuffer.Release();
		}

		// Token: 0x040003AB RID: 939
		private static readonly int s_MaxContextGCFrame = 8;

		// Token: 0x040003AC RID: 940
		private Material m_DebugOcclusionTestMaterial;

		// Token: 0x040003AD RID: 941
		private Material m_OccluderDebugViewMaterial;

		// Token: 0x040003AE RID: 942
		private ComputeShader m_OcclusionDebugCS;

		// Token: 0x040003AF RID: 943
		private int m_ClearOcclusionDebugKernel;

		// Token: 0x040003B0 RID: 944
		private ComputeShader m_OccluderDepthPyramidCS;

		// Token: 0x040003B1 RID: 945
		private int m_OccluderDepthDownscaleKernel;

		// Token: 0x040003B2 RID: 946
		private int m_FrameIndex;

		// Token: 0x040003B3 RID: 947
		private SilhouettePlaneCache m_SilhouettePlaneCache;

		// Token: 0x040003B4 RID: 948
		private NativeParallelHashMap<int, int> m_ViewIDToIndexMap;

		// Token: 0x040003B5 RID: 949
		private List<OccluderContext> m_OccluderContextData;

		// Token: 0x040003B6 RID: 950
		private NativeList<OcclusionCullingCommon.OccluderContextSlot> m_OccluderContextSlots;

		// Token: 0x040003B7 RID: 951
		private NativeList<int> m_FreeOccluderContexts;

		// Token: 0x040003B8 RID: 952
		private NativeArray<OcclusionCullingCommonShaderVariables> m_CommonShaderVariables;

		// Token: 0x040003B9 RID: 953
		private ComputeBuffer m_CommonConstantBuffer;

		// Token: 0x040003BA RID: 954
		private NativeArray<OcclusionCullingDebugShaderVariables> m_DebugShaderVariables;

		// Token: 0x040003BB RID: 955
		private ComputeBuffer m_DebugConstantBuffer;

		// Token: 0x040003BC RID: 956
		private ProfilingSampler m_ProfilingSamplerUpdateOccluders;

		// Token: 0x040003BD RID: 957
		private ProfilingSampler m_ProfilingSamplerOcclusionTestOverlay;

		// Token: 0x040003BE RID: 958
		private ProfilingSampler m_ProfilingSamplerOccluderOverlay;

		// Token: 0x020000B9 RID: 185
		private struct OccluderContextSlot
		{
			// Token: 0x040003BF RID: 959
			public bool valid;

			// Token: 0x040003C0 RID: 960
			public int lastUsedFrameIndex;

			// Token: 0x040003C1 RID: 961
			public int viewInstanceID;
		}

		// Token: 0x020000BA RID: 186
		private static class ShaderIDs
		{
			// Token: 0x040003C2 RID: 962
			public static readonly int OcclusionCullingCommonShaderVariables = Shader.PropertyToID("OcclusionCullingCommonShaderVariables");

			// Token: 0x040003C3 RID: 963
			public static readonly int _OccluderDepthPyramid = Shader.PropertyToID("_OccluderDepthPyramid");

			// Token: 0x040003C4 RID: 964
			public static readonly int _OcclusionDebugOverlay = Shader.PropertyToID("_OcclusionDebugOverlay");

			// Token: 0x040003C5 RID: 965
			public static readonly int OcclusionCullingDebugShaderVariables = Shader.PropertyToID("OcclusionCullingDebugShaderVariables");
		}

		// Token: 0x020000BB RID: 187
		private class OcclusionTestOverlaySetupPassData
		{
			// Token: 0x040003C6 RID: 966
			public OcclusionCullingDebugShaderVariables cb;
		}

		// Token: 0x020000BC RID: 188
		private class OcclusionTestOverlayPassData
		{
			// Token: 0x040003C7 RID: 967
			public BufferHandle debugPyramid;
		}

		// Token: 0x020000BD RID: 189
		private struct DebugOccluderViewData
		{
			// Token: 0x040003C8 RID: 968
			public int passIndex;

			// Token: 0x040003C9 RID: 969
			public Rect viewport;

			// Token: 0x040003CA RID: 970
			public bool valid;
		}

		// Token: 0x020000BE RID: 190
		private class OccluderOverlayPassData
		{
			// Token: 0x040003CB RID: 971
			public Material debugMaterial;

			// Token: 0x040003CC RID: 972
			public RTHandle occluderTexture;

			// Token: 0x040003CD RID: 973
			public Rect viewport;

			// Token: 0x040003CE RID: 974
			public int passIndex;

			// Token: 0x040003CF RID: 975
			public Vector2 validRange;
		}

		// Token: 0x020000BF RID: 191
		private class UpdateOccludersPassData
		{
			// Token: 0x040003D0 RID: 976
			public OccluderParameters occluderParams;

			// Token: 0x040003D1 RID: 977
			public List<OccluderSubviewUpdate> occluderSubviewUpdates;

			// Token: 0x040003D2 RID: 978
			public OccluderHandles occluderHandles;
		}
	}
}
