using System;
using UnityEngine.Rendering.RenderGraphModule;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x0200014A RID: 330
	public class XRDepthMotionPass : ScriptableRenderPass
	{
		// Token: 0x06000726 RID: 1830 RVA: 0x00022C74 File Offset: 0x00020E74
		public XRDepthMotionPass(RenderPassEvent evt, Shader xrMotionVector)
		{
			base.profilingSampler = new ProfilingSampler("XRDepthMotionPass");
			this.m_PassData = new XRDepthMotionPass.PassData();
			base.renderPassEvent = evt;
			this.ResetMotionData();
			this.m_XRMotionVectorMaterial = CoreUtils.CreateEngineMaterial(xrMotionVector);
			this.xrMotionVectorColor = TextureHandle.nullHandle;
			this.m_XRMotionVectorColor = null;
			this.xrMotionVectorDepth = TextureHandle.nullHandle;
			this.m_XRMotionVectorDepth = null;
		}

		// Token: 0x06000727 RID: 1831 RVA: 0x00022CF8 File Offset: 0x00020EF8
		private static DrawingSettings GetObjectMotionDrawingSettings(Camera camera)
		{
			SortingSettings sortingSettings = new SortingSettings(camera)
			{
				criteria = SortingCriteria.CommonOpaque
			};
			DrawingSettings drawingSettings = new DrawingSettings(XRDepthMotionPass.k_MotionOnlyShaderTagId, sortingSettings)
			{
				perObjectData = PerObjectData.MotionVectors,
				enableDynamicBatching = false,
				enableInstancing = true
			};
			drawingSettings.SetShaderPassName(0, XRDepthMotionPass.k_MotionOnlyShaderTagId);
			return drawingSettings;
		}

		// Token: 0x06000728 RID: 1832 RVA: 0x00022D50 File Offset: 0x00020F50
		private void InitObjectMotionRendererLists(ref XRDepthMotionPass.PassData passData, ref CullingResults cullResults, RenderGraph renderGraph, Camera camera)
		{
			DrawingSettings objectMotionDrawingSettings = XRDepthMotionPass.GetObjectMotionDrawingSettings(camera);
			FilteringSettings filteringSettings = new FilteringSettings(new RenderQueueRange?(RenderQueueRange.opaque), camera.cullingMask, uint.MaxValue, 0);
			filteringSettings.forceAllMotionVectorObjects = true;
			RenderStateBlock renderStateBlock = new RenderStateBlock(RenderStateMask.Nothing);
			RenderingUtils.CreateRendererListWithRenderStateBlock(renderGraph, ref cullResults, objectMotionDrawingSettings, filteringSettings, renderStateBlock, ref passData.objMotionRendererList);
		}

		// Token: 0x06000729 RID: 1833 RVA: 0x00022DA0 File Offset: 0x00020FA0
		private void InitPassData(ref XRDepthMotionPass.PassData passData, UniversalCameraData cameraData)
		{
			passData.previousViewProjectionStereo = this.m_PreviousViewProjection;
			passData.viewProjectionStereo = this.m_ViewProjection;
			passData.xrMotionVector = this.m_XRMotionVectorMaterial;
		}

		// Token: 0x0600072A RID: 1834 RVA: 0x00022DCC File Offset: 0x00020FCC
		private void ImportXRMotionColorAndDepth(RenderGraph renderGraph, UniversalCameraData cameraData)
		{
			RenderTargetIdentifier rtMotionId = cameraData.xr.motionVectorRenderTarget;
			if (this.m_XRMotionVectorColor == null)
			{
				this.m_XRMotionVectorColor = RTHandles.Alloc(rtMotionId);
			}
			else if (this.m_XRMotionVectorColor.nameID != rtMotionId)
			{
				RTHandleStaticHelpers.SetRTHandleUserManagedWrapper(ref this.m_XRMotionVectorColor, rtMotionId);
			}
			RenderTargetIdentifier depthId = cameraData.xr.motionVectorRenderTarget;
			if (this.m_XRMotionVectorDepth == null)
			{
				this.m_XRMotionVectorDepth = RTHandles.Alloc(depthId);
			}
			else if (this.m_XRMotionVectorDepth.nameID != depthId)
			{
				RTHandleStaticHelpers.SetRTHandleUserManagedWrapper(ref this.m_XRMotionVectorDepth, depthId);
			}
			RenderTargetInfo importInfo = default(RenderTargetInfo);
			importInfo.width = cameraData.xr.motionVectorRenderTargetDesc.width;
			importInfo.height = cameraData.xr.motionVectorRenderTargetDesc.height;
			importInfo.volumeDepth = cameraData.xr.motionVectorRenderTargetDesc.volumeDepth;
			importInfo.msaaSamples = cameraData.xr.motionVectorRenderTargetDesc.msaaSamples;
			importInfo.format = cameraData.xr.motionVectorRenderTargetDesc.graphicsFormat;
			RenderTargetInfo importInfoDepth = default(RenderTargetInfo);
			importInfoDepth = importInfo;
			importInfoDepth.format = cameraData.xr.motionVectorRenderTargetDesc.depthStencilFormat;
			ImportResourceParams importMotionColorParams = default(ImportResourceParams);
			importMotionColorParams.clearOnFirstUse = true;
			importMotionColorParams.clearColor = Color.black;
			importMotionColorParams.discardOnLastUse = false;
			ImportResourceParams importMotionDepthParams = default(ImportResourceParams);
			importMotionDepthParams.clearOnFirstUse = true;
			importMotionDepthParams.clearColor = Color.black;
			importMotionDepthParams.discardOnLastUse = false;
			this.xrMotionVectorColor = renderGraph.ImportTexture(this.m_XRMotionVectorColor, importInfo, importMotionColorParams);
			this.xrMotionVectorDepth = renderGraph.ImportTexture(this.m_XRMotionVectorDepth, importInfoDepth, importMotionDepthParams);
		}

		// Token: 0x0600072B RID: 1835 RVA: 0x00022F84 File Offset: 0x00021184
		internal void Render(RenderGraph renderGraph, ContextContainer frameData)
		{
			UniversalRenderingData renderingData = frameData.Get<UniversalRenderingData>();
			UniversalCameraData cameraData = frameData.Get<UniversalCameraData>();
			if (!cameraData.xr.enabled || !cameraData.xr.singlePassEnabled)
			{
				Debug.LogWarning("XRDepthMotionPass::Render is skipped because either XR is not enabled or singlepass rendering is not enabled.");
				return;
			}
			if (!cameraData.xr.hasMotionVectorPass)
			{
				Debug.LogWarning("XRDepthMotionPass::Render is skipped because XR motion vector is not enabled for the current XRPass.");
				return;
			}
			this.ImportXRMotionColorAndDepth(renderGraph, cameraData);
			cameraData.camera.depthTextureMode |= DepthTextureMode.Depth | DepthTextureMode.MotionVectors;
			XRDepthMotionPass.PassData passData;
			using (IRasterRenderGraphBuilder builder = renderGraph.AddRasterRenderPass<XRDepthMotionPass.PassData>("XR Motion Pass", out passData, base.profilingSampler, "./Library/PackageCache/com.unity.render-pipelines.universal/Runtime/Passes/XRDepthMotionPass.cs", 189))
			{
				builder.EnableFoveatedRasterization(cameraData.xr.supportsFoveatedRendering);
				builder.SetRenderAttachment(this.xrMotionVectorColor, 0, AccessFlags.Write);
				builder.SetRenderAttachmentDepth(this.xrMotionVectorDepth, AccessFlags.Write);
				this.InitObjectMotionRendererLists(ref passData, ref renderingData.cullResults, renderGraph, cameraData.camera);
				builder.UseRendererList(in passData.objMotionRendererList);
				builder.AllowGlobalStateModification(true);
				this.InitPassData(ref passData, cameraData);
				builder.SetRenderFunc<XRDepthMotionPass.PassData>(delegate(XRDepthMotionPass.PassData data, RasterGraphContext context)
				{
					context.cmd.SetGlobalMatrixArray(ShaderPropertyId.previousViewProjectionNoJitterStereo, data.previousViewProjectionStereo);
					context.cmd.SetGlobalMatrixArray(ShaderPropertyId.viewProjectionNoJitterStereo, data.viewProjectionStereo);
					context.cmd.DrawRendererList(passData.objMotionRendererList);
					context.cmd.DrawProcedural(Matrix4x4.identity, data.xrMotionVector, 0, MeshTopology.Triangles, 3, 1);
				});
			}
		}

		// Token: 0x0600072C RID: 1836 RVA: 0x000230B8 File Offset: 0x000212B8
		private void ResetMotionData()
		{
			for (int i = 0; i < 2; i++)
			{
				this.m_ViewProjection[i] = Matrix4x4.identity;
				this.m_PreviousViewProjection[i] = Matrix4x4.identity;
			}
			this.m_LastFrameIndex = -1;
		}

		// Token: 0x0600072D RID: 1837 RVA: 0x000230FC File Offset: 0x000212FC
		public void Update(ref UniversalCameraData cameraData)
		{
			if (!cameraData.xr.enabled || !cameraData.xr.singlePassEnabled)
			{
				Debug.LogWarning("XRDepthMotionPass::Update is skipped because either XR is not enabled or singlepass rendering is not enabled.");
				return;
			}
			if (this.m_LastFrameIndex != Time.frameCount)
			{
				Matrix4x4 gpuVP0 = GL.GetGPUProjectionMatrix(cameraData.GetProjectionMatrixNoJitter(0), false) * cameraData.GetViewMatrix(0);
				Matrix4x4 gpuVP = GL.GetGPUProjectionMatrix(cameraData.GetProjectionMatrixNoJitter(1), false) * cameraData.GetViewMatrix(1);
				this.m_PreviousViewProjection[0] = this.m_ViewProjection[0];
				this.m_PreviousViewProjection[1] = this.m_ViewProjection[1];
				this.m_ViewProjection[0] = gpuVP0;
				this.m_ViewProjection[1] = gpuVP;
				this.m_LastFrameIndex = Time.frameCount;
			}
		}

		// Token: 0x0600072E RID: 1838 RVA: 0x000231CD File Offset: 0x000213CD
		public void Dispose()
		{
			RTHandle xrmotionVectorColor = this.m_XRMotionVectorColor;
			if (xrmotionVectorColor != null)
			{
				xrmotionVectorColor.Release();
			}
			RTHandle xrmotionVectorDepth = this.m_XRMotionVectorDepth;
			if (xrmotionVectorDepth != null)
			{
				xrmotionVectorDepth.Release();
			}
			CoreUtils.Destroy(this.m_XRMotionVectorMaterial);
		}

		// Token: 0x040007AD RID: 1965
		private static readonly ShaderTagId k_MotionOnlyShaderTagId = new ShaderTagId("XRMotionVectors");

		// Token: 0x040007AE RID: 1966
		private XRDepthMotionPass.PassData m_PassData;

		// Token: 0x040007AF RID: 1967
		private RTHandle m_XRMotionVectorColor;

		// Token: 0x040007B0 RID: 1968
		private TextureHandle xrMotionVectorColor;

		// Token: 0x040007B1 RID: 1969
		private RTHandle m_XRMotionVectorDepth;

		// Token: 0x040007B2 RID: 1970
		private TextureHandle xrMotionVectorDepth;

		// Token: 0x040007B3 RID: 1971
		private const int k_XRViewCount = 2;

		// Token: 0x040007B4 RID: 1972
		private Matrix4x4[] m_ViewProjection = new Matrix4x4[2];

		// Token: 0x040007B5 RID: 1973
		private Matrix4x4[] m_PreviousViewProjection = new Matrix4x4[2];

		// Token: 0x040007B6 RID: 1974
		private int m_LastFrameIndex;

		// Token: 0x040007B7 RID: 1975
		private Material m_XRMotionVectorMaterial;

		// Token: 0x0200014B RID: 331
		private class PassData
		{
			// Token: 0x040007B8 RID: 1976
			internal RendererListHandle objMotionRendererList;

			// Token: 0x040007B9 RID: 1977
			internal Matrix4x4[] previousViewProjectionStereo = new Matrix4x4[2];

			// Token: 0x040007BA RID: 1978
			internal Matrix4x4[] viewProjectionStereo = new Matrix4x4[2];

			// Token: 0x040007BB RID: 1979
			internal Material xrMotionVector;
		}
	}
}
