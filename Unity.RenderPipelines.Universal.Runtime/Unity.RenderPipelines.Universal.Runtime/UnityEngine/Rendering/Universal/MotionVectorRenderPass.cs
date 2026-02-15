using System;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering.RenderGraphModule;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x0200011D RID: 285
	internal sealed class MotionVectorRenderPass : ScriptableRenderPass
	{
		// Token: 0x06000664 RID: 1636 RVA: 0x00018A50 File Offset: 0x00016C50
		internal MotionVectorRenderPass(RenderPassEvent evt, Material cameraMaterial, LayerMask opaqueLayerMask)
		{
			base.profilingSampler = ProfilingSampler.Get<URPProfileId>(URPProfileId.DrawMotionVectors);
			base.renderPassEvent = evt;
			this.m_CameraMaterial = cameraMaterial;
			this.m_FilteringSettings = new FilteringSettings(new RenderQueueRange?(RenderQueueRange.opaque), opaqueLayerMask, uint.MaxValue, 0);
			this.m_PassData = new MotionVectorRenderPass.PassData();
			base.ConfigureInput(ScriptableRenderPassInput.Depth);
		}

		// Token: 0x06000665 RID: 1637 RVA: 0x00018AAD File Offset: 0x00016CAD
		internal void Setup(RTHandle color, RTHandle depth)
		{
			this.m_Color = color;
			this.m_Depth = depth;
		}

		// Token: 0x06000666 RID: 1638 RVA: 0x00018AC0 File Offset: 0x00016CC0
		[Obsolete("This rendering path is for compatibility mode only (when Render Graph is disabled). Use Render Graph API instead.", false)]
		public override void Configure(CommandBuffer cmd, RenderTextureDescriptor cameraTextureDescriptor)
		{
			cmd.SetGlobalTexture(this.m_Color.name, this.m_Color.nameID);
			cmd.SetGlobalTexture(this.m_Depth.name, this.m_Depth.nameID);
			base.ConfigureTarget(this.m_Color, this.m_Depth);
			base.ConfigureClear(ClearFlag.Color | ClearFlag.Depth, Color.black);
			base.ConfigureDepthStoreAction(RenderBufferStoreAction.DontCare);
		}

		// Token: 0x06000667 RID: 1639 RVA: 0x00018B2C File Offset: 0x00016D2C
		private static void ExecutePass(RasterCommandBuffer cmd, MotionVectorRenderPass.PassData passData, RendererList rendererList)
		{
			Material cameraMaterial = passData.cameraMaterial;
			if (cameraMaterial == null)
			{
				return;
			}
			Camera camera = passData.camera;
			if (camera.cameraType == CameraType.Preview)
			{
				return;
			}
			camera.depthTextureMode |= DepthTextureMode.Depth | DepthTextureMode.MotionVectors;
			MotionVectorRenderPass.DrawCameraMotionVectors(cmd, passData.xr, cameraMaterial);
			MotionVectorRenderPass.DrawObjectMotionVectors(cmd, passData.xr, ref rendererList);
		}

		// Token: 0x06000668 RID: 1640 RVA: 0x00018B84 File Offset: 0x00016D84
		[Obsolete("This rendering path is for compatibility mode only (when Render Graph is disabled). Use Render Graph API instead.", false)]
		public unsafe override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
		{
			ContextContainer frameData = renderingData.frameData;
			UniversalRenderingData universalRenderingData = frameData.Get<UniversalRenderingData>();
			UniversalCameraData cameraData = frameData.Get<UniversalCameraData>();
			RasterCommandBuffer cmd = CommandBufferHelpers.GetRasterCommandBuffer(*renderingData.commandBuffer);
			using (new ProfilingScope(cmd, base.profilingSampler))
			{
				this.InitPassData(ref this.m_PassData, cameraData);
				this.InitRendererLists(ref this.m_PassData, ref universalRenderingData.cullResults, universalRenderingData.supportsDynamicBatching, context, null, false);
				MotionVectorRenderPass.ExecutePass(cmd, this.m_PassData, this.m_PassData.rendererList);
			}
		}

		// Token: 0x06000669 RID: 1641 RVA: 0x00018C20 File Offset: 0x00016E20
		private static DrawingSettings GetDrawingSettings(Camera camera, bool supportsDynamicBatching)
		{
			SortingSettings sortingSettings = new SortingSettings(camera)
			{
				criteria = SortingCriteria.CommonOpaque
			};
			DrawingSettings drawingSettings = new DrawingSettings(ShaderTagId.none, sortingSettings)
			{
				perObjectData = PerObjectData.MotionVectors,
				enableDynamicBatching = supportsDynamicBatching,
				enableInstancing = true
			};
			for (int i = 0; i < MotionVectorRenderPass.s_ShaderTags.Length; i++)
			{
				drawingSettings.SetShaderPassName(i, new ShaderTagId(MotionVectorRenderPass.s_ShaderTags[i]));
			}
			return drawingSettings;
		}

		// Token: 0x0600066A RID: 1642 RVA: 0x00018C98 File Offset: 0x00016E98
		private static void DrawCameraMotionVectors(RasterCommandBuffer cmd, XRPass xr, Material cameraMaterial)
		{
			bool supportsFoveatedRendering = xr.supportsFoveatedRendering;
			bool nonUniformFoveatedRendering = supportsFoveatedRendering && XRSystem.foveatedRenderingCaps.HasFlag(FoveatedRenderingCaps.NonUniformRaster);
			if (supportsFoveatedRendering)
			{
				if (nonUniformFoveatedRendering)
				{
					cmd.SetFoveatedRenderingMode(FoveatedRenderingMode.Disabled);
				}
				else
				{
					cmd.SetFoveatedRenderingMode(FoveatedRenderingMode.Enabled);
				}
			}
			cmd.DrawProcedural(Matrix4x4.identity, cameraMaterial, 0, MeshTopology.Triangles, 3, 1);
			if (supportsFoveatedRendering && !nonUniformFoveatedRendering)
			{
				cmd.SetFoveatedRenderingMode(FoveatedRenderingMode.Disabled);
			}
		}

		// Token: 0x0600066B RID: 1643 RVA: 0x00018CF9 File Offset: 0x00016EF9
		private static void DrawObjectMotionVectors(RasterCommandBuffer cmd, XRPass xr, ref RendererList rendererList)
		{
			bool supportsFoveatedRendering = xr.supportsFoveatedRendering;
			if (supportsFoveatedRendering)
			{
				cmd.SetFoveatedRenderingMode(FoveatedRenderingMode.Enabled);
			}
			cmd.DrawRendererList(rendererList);
			if (supportsFoveatedRendering)
			{
				cmd.SetFoveatedRenderingMode(FoveatedRenderingMode.Disabled);
			}
		}

		// Token: 0x0600066C RID: 1644 RVA: 0x00018D20 File Offset: 0x00016F20
		private void InitPassData(ref MotionVectorRenderPass.PassData passData, UniversalCameraData cameraData)
		{
			passData.camera = cameraData.camera;
			passData.xr = cameraData.xr;
			passData.cameraMaterial = this.m_CameraMaterial;
		}

		// Token: 0x0600066D RID: 1645 RVA: 0x00018D4C File Offset: 0x00016F4C
		private void InitRendererLists(ref MotionVectorRenderPass.PassData passData, ref CullingResults cullResults, bool supportsDynamicBatching, ScriptableRenderContext context, RenderGraph renderGraph, bool useRenderGraph)
		{
			DrawingSettings drawingSettings = MotionVectorRenderPass.GetDrawingSettings(passData.camera, supportsDynamicBatching);
			RenderStateBlock renderStateBlock = new RenderStateBlock(RenderStateMask.Nothing);
			if (useRenderGraph)
			{
				RenderingUtils.CreateRendererListWithRenderStateBlock(renderGraph, ref cullResults, drawingSettings, this.m_FilteringSettings, renderStateBlock, ref passData.rendererListHdl);
				return;
			}
			RenderingUtils.CreateRendererListWithRenderStateBlock(context, ref cullResults, drawingSettings, this.m_FilteringSettings, renderStateBlock, ref passData.rendererList);
		}

		// Token: 0x0600066E RID: 1646 RVA: 0x00018DA4 File Offset: 0x00016FA4
		internal void Render(RenderGraph renderGraph, ContextContainer frameData, TextureHandle cameraDepthTexture, TextureHandle motionVectorColor, TextureHandle motionVectorDepth)
		{
			UniversalRenderingData renderingData = frameData.Get<UniversalRenderingData>();
			UniversalCameraData cameraData = frameData.Get<UniversalCameraData>();
			MotionVectorRenderPass.PassData passData;
			using (IRasterRenderGraphBuilder builder = renderGraph.AddRasterRenderPass<MotionVectorRenderPass.PassData>(base.passName, out passData, base.profilingSampler, "./Library/PackageCache/com.unity.render-pipelines.universal/Runtime/Passes/MotionVectorRenderPass.cs", 221))
			{
				builder.UseAllGlobalTextures(true);
				builder.AllowPassCulling(false);
				builder.AllowGlobalStateModification(true);
				if (cameraData.xr.enabled)
				{
					builder.EnableFoveatedRasterization(cameraData.xr.supportsFoveatedRendering && cameraData.xrUniversal.canFoveateIntermediatePasses);
				}
				passData.motionVectorColor = motionVectorColor;
				builder.SetRenderAttachment(motionVectorColor, 0, AccessFlags.Write);
				passData.motionVectorDepth = motionVectorDepth;
				builder.SetRenderAttachmentDepth(motionVectorDepth, AccessFlags.Write);
				this.InitPassData(ref passData, cameraData);
				passData.cameraDepth = cameraDepthTexture;
				builder.UseTexture(in cameraDepthTexture, AccessFlags.Read);
				this.InitRendererLists(ref passData, ref renderingData.cullResults, renderingData.supportsDynamicBatching, default(ScriptableRenderContext), renderGraph, true);
				builder.UseRendererList(in passData.rendererListHdl);
				if (motionVectorColor.IsValid())
				{
					builder.SetGlobalTextureAfterPass(in motionVectorColor, Shader.PropertyToID("_MotionVectorTexture"));
				}
				if (motionVectorDepth.IsValid())
				{
					builder.SetGlobalTextureAfterPass(in motionVectorDepth, Shader.PropertyToID("_MotionVectorDepthTexture"));
				}
				builder.SetRenderFunc<MotionVectorRenderPass.PassData>(delegate(MotionVectorRenderPass.PassData data, RasterGraphContext context)
				{
					if (data.cameraMaterial != null)
					{
						data.cameraMaterial.SetTexture(MotionVectorRenderPass.s_CameraDepthTextureID, data.cameraDepth);
					}
					MotionVectorRenderPass.ExecutePass(context.cmd, data, data.rendererListHdl);
				});
			}
		}

		// Token: 0x0600066F RID: 1647 RVA: 0x00018F0C File Offset: 0x0001710C
		internal static void SetMotionVectorGlobalMatrices(CommandBuffer cmd, UniversalCameraData cameraData)
		{
			UniversalAdditionalCameraData additionalCameraData;
			if (cameraData.camera.TryGetComponent<UniversalAdditionalCameraData>(out additionalCameraData))
			{
				MotionVectorsPersistentData motionVectorsPersistentData = additionalCameraData.motionVectorsPersistentData;
				if (motionVectorsPersistentData == null)
				{
					return;
				}
				motionVectorsPersistentData.SetGlobalMotionMatrices(CommandBufferHelpers.GetRasterCommandBuffer(cmd), cameraData.xr);
			}
		}

		// Token: 0x06000670 RID: 1648 RVA: 0x00018F44 File Offset: 0x00017144
		internal static void SetRenderGraphMotionVectorGlobalMatrices(RenderGraph renderGraph, UniversalCameraData cameraData)
		{
			UniversalAdditionalCameraData additionalCameraData;
			if (cameraData.camera.TryGetComponent<UniversalAdditionalCameraData>(out additionalCameraData))
			{
				MotionVectorRenderPass.MotionMatrixPassData passData;
				using (IRasterRenderGraphBuilder builder = renderGraph.AddRasterRenderPass<MotionVectorRenderPass.MotionMatrixPassData>(MotionVectorRenderPass.s_SetMotionMatrixProfilingSampler.name, out passData, MotionVectorRenderPass.s_SetMotionMatrixProfilingSampler, "./Library/PackageCache/com.unity.render-pipelines.universal/Runtime/Passes/MotionVectorRenderPass.cs", 278))
				{
					passData.motionData = additionalCameraData.motionVectorsPersistentData;
					passData.xr = cameraData.xr;
					builder.AllowPassCulling(false);
					builder.AllowGlobalStateModification(true);
					builder.SetRenderFunc<MotionVectorRenderPass.MotionMatrixPassData>(delegate(MotionVectorRenderPass.MotionMatrixPassData data, RasterGraphContext context)
					{
						data.motionData.SetGlobalMotionMatrices(context.cmd, data.xr);
					});
				}
			}
		}

		// Token: 0x040005D7 RID: 1495
		internal const string k_MotionVectorTextureName = "_MotionVectorTexture";

		// Token: 0x040005D8 RID: 1496
		internal const string k_MotionVectorDepthTextureName = "_MotionVectorDepthTexture";

		// Token: 0x040005D9 RID: 1497
		internal const GraphicsFormat k_TargetFormat = GraphicsFormat.R16G16_SFloat;

		// Token: 0x040005DA RID: 1498
		public const string k_MotionVectorsLightModeTag = "MotionVectors";

		// Token: 0x040005DB RID: 1499
		private static readonly string[] s_ShaderTags = new string[] { "MotionVectors" };

		// Token: 0x040005DC RID: 1500
		private static readonly int s_CameraDepthTextureID = Shader.PropertyToID("_CameraDepthTexture");

		// Token: 0x040005DD RID: 1501
		private static readonly ProfilingSampler s_SetMotionMatrixProfilingSampler = new ProfilingSampler("Set Motion Vector Global Matrices");

		// Token: 0x040005DE RID: 1502
		private RTHandle m_Color;

		// Token: 0x040005DF RID: 1503
		private RTHandle m_Depth;

		// Token: 0x040005E0 RID: 1504
		private readonly Material m_CameraMaterial;

		// Token: 0x040005E1 RID: 1505
		private readonly FilteringSettings m_FilteringSettings;

		// Token: 0x040005E2 RID: 1506
		private MotionVectorRenderPass.PassData m_PassData;

		// Token: 0x0200011E RID: 286
		private class PassData
		{
			// Token: 0x040005E3 RID: 1507
			internal Camera camera;

			// Token: 0x040005E4 RID: 1508
			internal XRPass xr;

			// Token: 0x040005E5 RID: 1509
			internal TextureHandle motionVectorColor;

			// Token: 0x040005E6 RID: 1510
			internal TextureHandle motionVectorDepth;

			// Token: 0x040005E7 RID: 1511
			internal TextureHandle cameraDepth;

			// Token: 0x040005E8 RID: 1512
			internal Material cameraMaterial;

			// Token: 0x040005E9 RID: 1513
			internal RendererListHandle rendererListHdl;

			// Token: 0x040005EA RID: 1514
			internal RendererList rendererList;
		}

		// Token: 0x0200011F RID: 287
		public class MotionMatrixPassData
		{
			// Token: 0x040005EB RID: 1515
			public MotionVectorsPersistentData motionData;

			// Token: 0x040005EC RID: 1516
			public XRPass xr;
		}
	}
}
