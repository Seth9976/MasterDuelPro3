using System;
using UnityEngine.Rendering.RenderGraphModule;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x0200006D RID: 109
	internal class DebugRenderSetup : IDisposable
	{
		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000264 RID: 612 RVA: 0x00008210 File Offset: 0x00006410
		private DebugDisplaySettingsMaterial MaterialSettings
		{
			get
			{
				return this.m_DebugHandler.DebugDisplaySettings.materialSettings;
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000265 RID: 613 RVA: 0x00008222 File Offset: 0x00006422
		private DebugDisplaySettingsRendering RenderingSettings
		{
			get
			{
				return this.m_DebugHandler.DebugDisplaySettings.renderingSettings;
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000266 RID: 614 RVA: 0x00008234 File Offset: 0x00006434
		private DebugDisplaySettingsLighting LightingSettings
		{
			get
			{
				return this.m_DebugHandler.DebugDisplaySettings.lightingSettings;
			}
		}

		// Token: 0x06000267 RID: 615 RVA: 0x00008248 File Offset: 0x00006448
		internal void Begin(RasterCommandBuffer cmd)
		{
			DebugSceneOverrideMode sceneOverrideMode = this.RenderingSettings.sceneOverrideMode;
			if (sceneOverrideMode == DebugSceneOverrideMode.Wireframe)
			{
				cmd.SetWireframe(true);
				return;
			}
			if (sceneOverrideMode - DebugSceneOverrideMode.SolidWireframe > 1)
			{
				return;
			}
			if (this.m_Index == 1)
			{
				cmd.SetWireframe(true);
			}
		}

		// Token: 0x06000268 RID: 616 RVA: 0x00008284 File Offset: 0x00006484
		internal void End(RasterCommandBuffer cmd)
		{
			DebugSceneOverrideMode sceneOverrideMode = this.RenderingSettings.sceneOverrideMode;
			if (sceneOverrideMode == DebugSceneOverrideMode.Wireframe)
			{
				cmd.SetWireframe(false);
				return;
			}
			if (sceneOverrideMode - DebugSceneOverrideMode.SolidWireframe > 1)
			{
				return;
			}
			if (this.m_Index == 1)
			{
				cmd.SetWireframe(false);
			}
		}

		// Token: 0x06000269 RID: 617 RVA: 0x000082C0 File Offset: 0x000064C0
		internal DebugRenderSetup(DebugHandler debugHandler, int index, FilteringSettings filteringSettings)
		{
			this.m_DebugHandler = debugHandler;
			this.m_FilteringSettings = filteringSettings;
			this.m_Index = index;
		}

		// Token: 0x0600026A RID: 618 RVA: 0x000082DD File Offset: 0x000064DD
		internal void CreateRendererList(ScriptableRenderContext context, ref CullingResults cullResults, ref DrawingSettings drawingSettings, ref FilteringSettings filteringSettings, ref RenderStateBlock renderStateBlock, ref RendererList rendererList)
		{
			RenderingUtils.CreateRendererListWithRenderStateBlock(context, ref cullResults, drawingSettings, filteringSettings, renderStateBlock, ref rendererList);
		}

		// Token: 0x0600026B RID: 619 RVA: 0x000082FC File Offset: 0x000064FC
		internal void CreateRendererList(RenderGraph renderGraph, ref CullingResults cullResults, ref DrawingSettings drawingSettings, ref FilteringSettings filteringSettings, ref RenderStateBlock renderStateBlock, ref RendererListHandle rendererListHdl)
		{
			RenderingUtils.CreateRendererListWithRenderStateBlock(renderGraph, ref cullResults, drawingSettings, filteringSettings, renderStateBlock, ref rendererListHdl);
		}

		// Token: 0x0600026C RID: 620 RVA: 0x0000831B File Offset: 0x0000651B
		internal void DrawWithRendererList(RasterCommandBuffer cmd, ref RendererList rendererList)
		{
			if (rendererList.isValid)
			{
				cmd.DrawRendererList(rendererList);
			}
		}

		// Token: 0x0600026D RID: 621 RVA: 0x00008334 File Offset: 0x00006534
		internal DrawingSettings CreateDrawingSettings(DrawingSettings drawingSettings)
		{
			if (this.MaterialSettings.vertexAttributeDebugMode > DebugVertexAttributeMode.None)
			{
				Material replacementMaterial = this.m_DebugHandler.ReplacementMaterial;
				DrawingSettings modifiedDrawingSettings = drawingSettings;
				modifiedDrawingSettings.overrideMaterial = replacementMaterial;
				modifiedDrawingSettings.overrideMaterialPassIndex = 0;
				return modifiedDrawingSettings;
			}
			return drawingSettings;
		}

		// Token: 0x0600026E RID: 622 RVA: 0x00008374 File Offset: 0x00006574
		internal RenderStateBlock GetRenderStateBlock(RenderStateBlock renderStateBlock)
		{
			DebugSceneOverrideMode sceneOverrideMode = this.RenderingSettings.sceneOverrideMode;
			if (sceneOverrideMode != DebugSceneOverrideMode.Overdraw)
			{
				if (sceneOverrideMode - DebugSceneOverrideMode.SolidWireframe <= 1)
				{
					if (this.m_Index == 1)
					{
						renderStateBlock.rasterState = new RasterState(CullMode.Back, -1, -1f, true);
						renderStateBlock.mask = RenderStateMask.Raster;
					}
				}
			}
			else
			{
				bool flag = this.m_FilteringSettings.renderQueueRange == RenderQueueRange.opaque || this.m_FilteringSettings.renderQueueRange == RenderQueueRange.all;
				bool isTransparent = this.m_FilteringSettings.renderQueueRange == RenderQueueRange.transparent || this.m_FilteringSettings.renderQueueRange == RenderQueueRange.all;
				bool overdrawOpaque = this.m_DebugHandler.DebugDisplaySettings.renderingSettings.overdrawMode == DebugOverdrawMode.Opaque || this.m_DebugHandler.DebugDisplaySettings.renderingSettings.overdrawMode == DebugOverdrawMode.All;
				bool overdrawTransparent = this.m_DebugHandler.DebugDisplaySettings.renderingSettings.overdrawMode == DebugOverdrawMode.Transparent || this.m_DebugHandler.DebugDisplaySettings.renderingSettings.overdrawMode == DebugOverdrawMode.All;
				BlendMode destination = (((flag && overdrawOpaque) || (isTransparent && overdrawTransparent)) ? BlendMode.One : BlendMode.Zero);
				RenderTargetBlendState additiveBlend = new RenderTargetBlendState(ColorWriteMask.All, BlendMode.One, destination, BlendMode.One, BlendMode.Zero, BlendOp.Add, BlendOp.Add);
				renderStateBlock.blendState = new BlendState
				{
					blendState0 = additiveBlend
				};
				renderStateBlock.mask = RenderStateMask.Blend;
			}
			return renderStateBlock;
		}

		// Token: 0x0600026F RID: 623 RVA: 0x000084E2 File Offset: 0x000066E2
		internal int GetIndex()
		{
			return this.m_Index;
		}

		// Token: 0x06000270 RID: 624 RVA: 0x0000217F File Offset: 0x0000037F
		public void Dispose()
		{
		}

		// Token: 0x04000201 RID: 513
		private readonly DebugHandler m_DebugHandler;

		// Token: 0x04000202 RID: 514
		private readonly FilteringSettings m_FilteringSettings;

		// Token: 0x04000203 RID: 515
		private readonly int m_Index;
	}
}
