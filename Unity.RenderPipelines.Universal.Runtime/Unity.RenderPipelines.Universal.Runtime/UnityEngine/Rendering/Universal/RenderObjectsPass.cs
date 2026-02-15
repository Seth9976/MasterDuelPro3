using System;
using System.Collections.Generic;
using UnityEngine.Rendering.RenderGraphModule;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x0200013C RID: 316
	[MovedFrom(true, "UnityEngine.Experimental.Rendering.Universal", null, null)]
	public class RenderObjectsPass : ScriptableRenderPass
	{
		// Token: 0x17000176 RID: 374
		// (get) Token: 0x060006F5 RID: 1781 RVA: 0x00020FEE File Offset: 0x0001F1EE
		// (set) Token: 0x060006F6 RID: 1782 RVA: 0x00020FF6 File Offset: 0x0001F1F6
		public Material overrideMaterial { get; set; }

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x060006F7 RID: 1783 RVA: 0x00020FFF File Offset: 0x0001F1FF
		// (set) Token: 0x060006F8 RID: 1784 RVA: 0x00021007 File Offset: 0x0001F207
		public int overrideMaterialPassIndex { get; set; }

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x060006F9 RID: 1785 RVA: 0x00021010 File Offset: 0x0001F210
		// (set) Token: 0x060006FA RID: 1786 RVA: 0x00021018 File Offset: 0x0001F218
		public Shader overrideShader { get; set; }

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x060006FB RID: 1787 RVA: 0x00021021 File Offset: 0x0001F221
		// (set) Token: 0x060006FC RID: 1788 RVA: 0x00021029 File Offset: 0x0001F229
		public int overrideShaderPassIndex { get; set; }

		// Token: 0x060006FD RID: 1789 RVA: 0x00021032 File Offset: 0x0001F232
		[Obsolete("Use SetDepthState instead", true)]
		public void SetDetphState(bool writeEnabled, CompareFunction function = CompareFunction.Less)
		{
			this.SetDepthState(writeEnabled, function);
		}

		// Token: 0x060006FE RID: 1790 RVA: 0x0002103C File Offset: 0x0001F23C
		public void SetDepthState(bool writeEnabled, CompareFunction function = CompareFunction.Less)
		{
			this.m_RenderStateBlock.mask = this.m_RenderStateBlock.mask | RenderStateMask.Depth;
			this.m_RenderStateBlock.depthState = new DepthState(writeEnabled, function);
		}

		// Token: 0x060006FF RID: 1791 RVA: 0x00021064 File Offset: 0x0001F264
		public void SetStencilState(int reference, CompareFunction compareFunction, StencilOp passOp, StencilOp failOp, StencilOp zFailOp)
		{
			StencilState stencilState = StencilState.defaultValue;
			stencilState.enabled = true;
			stencilState.SetCompareFunction(compareFunction);
			stencilState.SetPassOperation(passOp);
			stencilState.SetFailOperation(failOp);
			stencilState.SetZFailOperation(zFailOp);
			this.m_RenderStateBlock.mask = this.m_RenderStateBlock.mask | RenderStateMask.Stencil;
			this.m_RenderStateBlock.stencilReference = reference;
			this.m_RenderStateBlock.stencilState = stencilState;
		}

		// Token: 0x06000700 RID: 1792 RVA: 0x000210CC File Offset: 0x0001F2CC
		public RenderObjectsPass(string profilerTag, RenderPassEvent renderPassEvent, string[] shaderTags, RenderQueueType renderQueueType, int layerMask, RenderObjects.CustomCameraSettings cameraSettings)
		{
			base.profilingSampler = new ProfilingSampler(profilerTag);
			this.Init(renderPassEvent, shaderTags, renderQueueType, layerMask, cameraSettings);
		}

		// Token: 0x06000701 RID: 1793 RVA: 0x000210F9 File Offset: 0x0001F2F9
		internal RenderObjectsPass(URPProfileId profileId, RenderPassEvent renderPassEvent, string[] shaderTags, RenderQueueType renderQueueType, int layerMask, RenderObjects.CustomCameraSettings cameraSettings)
		{
			base.profilingSampler = ProfilingSampler.Get<URPProfileId>(profileId);
			this.Init(renderPassEvent, shaderTags, renderQueueType, layerMask, cameraSettings);
		}

		// Token: 0x06000702 RID: 1794 RVA: 0x00021128 File Offset: 0x0001F328
		internal void Init(RenderPassEvent renderPassEvent, string[] shaderTags, RenderQueueType renderQueueType, int layerMask, RenderObjects.CustomCameraSettings cameraSettings)
		{
			this.m_PassData = new RenderObjectsPass.PassData();
			base.renderPassEvent = renderPassEvent;
			this.renderQueueType = renderQueueType;
			this.overrideMaterial = null;
			this.overrideMaterialPassIndex = 0;
			this.overrideShader = null;
			this.overrideShaderPassIndex = 0;
			RenderQueueRange renderQueueRange = ((renderQueueType == RenderQueueType.Transparent) ? RenderQueueRange.transparent : RenderQueueRange.opaque);
			this.m_FilteringSettings = new FilteringSettings(new RenderQueueRange?(renderQueueRange), layerMask, uint.MaxValue, 0);
			if (shaderTags != null && shaderTags.Length != 0)
			{
				foreach (string tag in shaderTags)
				{
					this.m_ShaderTagIdList.Add(new ShaderTagId(tag));
				}
			}
			else
			{
				this.m_ShaderTagIdList.Add(new ShaderTagId("SRPDefaultUnlit"));
				this.m_ShaderTagIdList.Add(new ShaderTagId("UniversalForward"));
				this.m_ShaderTagIdList.Add(new ShaderTagId("UniversalForwardOnly"));
			}
			this.m_RenderStateBlock = new RenderStateBlock(RenderStateMask.Nothing);
			this.m_CameraSettings = cameraSettings;
		}

		// Token: 0x06000703 RID: 1795 RVA: 0x00021214 File Offset: 0x0001F414
		[Obsolete("This rendering path is for compatibility mode only (when Render Graph is disabled). Use Render Graph API instead.", false)]
		public unsafe override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
		{
			UniversalRenderingData universalRenderingData = renderingData.frameData.Get<UniversalRenderingData>();
			UniversalCameraData cameraData = renderingData.frameData.Get<UniversalCameraData>();
			UniversalLightData lightData = renderingData.frameData.Get<UniversalLightData>();
			RasterCommandBuffer cmd = CommandBufferHelpers.GetRasterCommandBuffer(*renderingData.commandBuffer);
			using (new ProfilingScope(cmd, base.profilingSampler))
			{
				this.InitPassData(cameraData, ref this.m_PassData);
				this.InitRendererLists(universalRenderingData, lightData, ref this.m_PassData, context, null, false);
				RenderObjectsPass.ExecutePass(this.m_PassData, cmd, this.m_PassData.rendererList, renderingData.cameraData.IsCameraProjectionMatrixFlipped());
			}
		}

		// Token: 0x06000704 RID: 1796 RVA: 0x000212C0 File Offset: 0x0001F4C0
		private static void ExecutePass(RenderObjectsPass.PassData passData, RasterCommandBuffer cmd, RendererList rendererList, bool isYFlipped)
		{
			Camera camera = passData.cameraData.camera;
			Rect pixelRect = passData.cameraData.pixelRect;
			float cameraAspect = pixelRect.width / pixelRect.height;
			if (passData.cameraSettings.overrideCamera)
			{
				if (passData.cameraData.xr.enabled)
				{
					Debug.LogWarning("RenderObjects pass is configured to override camera matrices. While rendering in stereo camera matrices cannot be overridden.");
				}
				else
				{
					Matrix4x4 projectionMatrix = Matrix4x4.Perspective(passData.cameraSettings.cameraFieldOfView, cameraAspect, camera.nearClipPlane, camera.farClipPlane);
					projectionMatrix = GL.GetGPUProjectionMatrix(projectionMatrix, isYFlipped);
					Matrix4x4 viewMatrix = passData.cameraData.GetViewMatrix(0);
					Vector4 cameraTranslation = viewMatrix.GetColumn(3);
					viewMatrix.SetColumn(3, cameraTranslation + passData.cameraSettings.offset);
					RenderingUtils.SetViewAndProjectionMatrices(cmd, viewMatrix, projectionMatrix, false);
				}
			}
			if (ScriptableRenderPass.GetActiveDebugHandler(passData.cameraData) != null)
			{
				passData.debugRendererLists.DrawWithRendererList(cmd);
			}
			else
			{
				cmd.DrawRendererList(rendererList);
			}
			if (passData.cameraSettings.overrideCamera && passData.cameraSettings.restoreCamera && !passData.cameraData.xr.enabled)
			{
				RenderingUtils.SetViewAndProjectionMatrices(cmd, passData.cameraData.GetViewMatrix(0), GL.GetGPUProjectionMatrix(passData.cameraData.GetProjectionMatrix(0), isYFlipped), false);
			}
		}

		// Token: 0x06000705 RID: 1797 RVA: 0x000213FA File Offset: 0x0001F5FA
		private void InitPassData(UniversalCameraData cameraData, ref RenderObjectsPass.PassData passData)
		{
			passData.cameraSettings = this.m_CameraSettings;
			passData.renderPassEvent = base.renderPassEvent;
			passData.cameraData = cameraData;
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x00021420 File Offset: 0x0001F620
		private void InitRendererLists(UniversalRenderingData renderingData, UniversalLightData lightData, ref RenderObjectsPass.PassData passData, ScriptableRenderContext context, RenderGraph renderGraph, bool useRenderGraph)
		{
			SortingCriteria sortingCriteria = ((this.renderQueueType == RenderQueueType.Transparent) ? SortingCriteria.CommonTransparent : passData.cameraData.defaultOpaqueSortFlags);
			DrawingSettings drawingSettings = RenderingUtils.CreateDrawingSettings(this.m_ShaderTagIdList, renderingData, passData.cameraData, lightData, sortingCriteria);
			drawingSettings.overrideMaterial = this.overrideMaterial;
			drawingSettings.overrideMaterialPassIndex = this.overrideMaterialPassIndex;
			drawingSettings.overrideShader = this.overrideShader;
			drawingSettings.overrideShaderPassIndex = this.overrideShaderPassIndex;
			DebugHandler activeDebugHandler = ScriptableRenderPass.GetActiveDebugHandler(passData.cameraData);
			FilteringSettings filteringSettings = this.m_FilteringSettings;
			if (useRenderGraph)
			{
				if (activeDebugHandler != null)
				{
					passData.debugRendererLists = activeDebugHandler.CreateRendererListsWithDebugRenderState(renderGraph, ref renderingData.cullResults, ref drawingSettings, ref this.m_FilteringSettings, ref this.m_RenderStateBlock);
					return;
				}
				RenderingUtils.CreateRendererListWithRenderStateBlock(renderGraph, ref renderingData.cullResults, drawingSettings, this.m_FilteringSettings, this.m_RenderStateBlock, ref passData.rendererListHdl);
				return;
			}
			else
			{
				if (activeDebugHandler != null)
				{
					passData.debugRendererLists = activeDebugHandler.CreateRendererListsWithDebugRenderState(context, ref renderingData.cullResults, ref drawingSettings, ref this.m_FilteringSettings, ref this.m_RenderStateBlock);
					return;
				}
				RenderingUtils.CreateRendererListWithRenderStateBlock(context, ref renderingData.cullResults, drawingSettings, this.m_FilteringSettings, this.m_RenderStateBlock, ref passData.rendererList);
				return;
			}
		}

		// Token: 0x06000707 RID: 1799 RVA: 0x0002153C File Offset: 0x0001F73C
		public override void RecordRenderGraph(RenderGraph renderGraph, ContextContainer frameData)
		{
			UniversalCameraData cameraData = frameData.Get<UniversalCameraData>();
			UniversalRenderingData renderingData = frameData.Get<UniversalRenderingData>();
			UniversalLightData lightData = frameData.Get<UniversalLightData>();
			RenderObjectsPass.PassData passData;
			using (IRasterRenderGraphBuilder builder = renderGraph.AddRasterRenderPass<RenderObjectsPass.PassData>(base.passName, out passData, base.profilingSampler, "./Library/PackageCache/com.unity.render-pipelines.universal/Runtime/Passes/RenderObjectsPass.cs", 274))
			{
				UniversalResourceData resourceData = frameData.Get<UniversalResourceData>();
				this.InitPassData(cameraData, ref passData);
				passData.color = resourceData.activeColorTexture;
				builder.SetRenderAttachment(resourceData.activeColorTexture, 0, AccessFlags.Write);
				builder.SetRenderAttachmentDepth(resourceData.activeDepthTexture, AccessFlags.Write);
				TextureHandle mainShadowsTexture = resourceData.mainShadowsTexture;
				TextureHandle additionalShadowsTexture = resourceData.additionalShadowsTexture;
				if (mainShadowsTexture.IsValid())
				{
					builder.UseTexture(in mainShadowsTexture, AccessFlags.Read);
				}
				if (additionalShadowsTexture.IsValid())
				{
					builder.UseTexture(in additionalShadowsTexture, AccessFlags.Read);
				}
				foreach (TextureHandle dBuffer in resourceData.dBuffer)
				{
					if (dBuffer.IsValid())
					{
						builder.UseTexture(in dBuffer, AccessFlags.Read);
					}
				}
				TextureHandle ssaoTexture = resourceData.ssaoTexture;
				if (ssaoTexture.IsValid())
				{
					builder.UseTexture(in ssaoTexture, AccessFlags.Read);
				}
				this.InitRendererLists(renderingData, lightData, ref passData, default(ScriptableRenderContext), renderGraph, true);
				if (ScriptableRenderPass.GetActiveDebugHandler(passData.cameraData) != null)
				{
					passData.debugRendererLists.PrepareRendererListForRasterPass(builder);
				}
				else
				{
					builder.UseRendererList(in passData.rendererListHdl);
				}
				builder.AllowPassCulling(false);
				builder.AllowGlobalStateModification(true);
				if (cameraData.xr.enabled)
				{
					builder.EnableFoveatedRasterization(cameraData.xr.supportsFoveatedRendering && cameraData.xrUniversal.canFoveateIntermediatePasses);
				}
				builder.SetRenderFunc<RenderObjectsPass.PassData>(delegate(RenderObjectsPass.PassData data, RasterGraphContext rgContext)
				{
					bool isYFlipped = data.cameraData.IsRenderTargetProjectionMatrixFlipped(data.color, null);
					RenderObjectsPass.ExecutePass(data, rgContext.cmd, data.rendererListHdl, isYFlipped);
				});
			}
		}

		// Token: 0x0400072E RID: 1838
		private RenderQueueType renderQueueType;

		// Token: 0x0400072F RID: 1839
		private FilteringSettings m_FilteringSettings;

		// Token: 0x04000730 RID: 1840
		private RenderObjects.CustomCameraSettings m_CameraSettings;

		// Token: 0x04000735 RID: 1845
		private List<ShaderTagId> m_ShaderTagIdList = new List<ShaderTagId>();

		// Token: 0x04000736 RID: 1846
		private RenderObjectsPass.PassData m_PassData;

		// Token: 0x04000737 RID: 1847
		private RenderStateBlock m_RenderStateBlock;

		// Token: 0x0200013D RID: 317
		private class PassData
		{
			// Token: 0x04000738 RID: 1848
			internal RenderObjects.CustomCameraSettings cameraSettings;

			// Token: 0x04000739 RID: 1849
			internal RenderPassEvent renderPassEvent;

			// Token: 0x0400073A RID: 1850
			internal TextureHandle color;

			// Token: 0x0400073B RID: 1851
			internal RendererListHandle rendererListHdl;

			// Token: 0x0400073C RID: 1852
			internal DebugRendererLists debugRendererLists;

			// Token: 0x0400073D RID: 1853
			internal UniversalCameraData cameraData;

			// Token: 0x0400073E RID: 1854
			internal RendererList rendererList;
		}
	}
}
