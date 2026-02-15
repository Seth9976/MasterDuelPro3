using System;
using System.Collections.Generic;
using UnityEngine.Rendering.RenderGraphModule;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x0200006C RID: 108
	internal class DebugRendererLists
	{
		// Token: 0x0600025D RID: 605 RVA: 0x00007ED1 File Offset: 0x000060D1
		public DebugRendererLists(DebugHandler debugHandler, FilteringSettings filteringSettings)
		{
			this.m_DebugHandler = debugHandler;
			this.m_FilteringSettings = filteringSettings;
		}

		// Token: 0x0600025E RID: 606 RVA: 0x00007F0C File Offset: 0x0000610C
		private void CreateDebugRenderSetups(FilteringSettings filteringSettings)
		{
			DebugSceneOverrideMode sceneOverrideMode = this.m_DebugHandler.DebugDisplaySettings.renderingSettings.sceneOverrideMode;
			int numIterations = ((sceneOverrideMode == DebugSceneOverrideMode.SolidWireframe || sceneOverrideMode == DebugSceneOverrideMode.ShadedWireframe) ? 2 : 1);
			for (int i = 0; i < numIterations; i++)
			{
				this.m_DebugRenderSetups.Add(new DebugRenderSetup(this.m_DebugHandler, i, filteringSettings));
			}
		}

		// Token: 0x0600025F RID: 607 RVA: 0x00007F60 File Offset: 0x00006160
		private void DisposeDebugRenderLists()
		{
			foreach (DebugRenderSetup debugRenderSetup in this.m_DebugRenderSetups)
			{
				debugRenderSetup.Dispose();
			}
			this.m_DebugRenderSetups.Clear();
			this.m_ActiveDebugRendererList.Clear();
			this.m_ActiveDebugRendererListHdl.Clear();
		}

		// Token: 0x06000260 RID: 608 RVA: 0x00007FD4 File Offset: 0x000061D4
		internal void CreateRendererListsWithDebugRenderState(ScriptableRenderContext context, ref CullingResults cullResults, ref DrawingSettings drawingSettings, ref FilteringSettings filteringSettings, ref RenderStateBlock renderStateBlock)
		{
			this.CreateDebugRenderSetups(filteringSettings);
			foreach (DebugRenderSetup debugRenderSetup in this.m_DebugRenderSetups)
			{
				DrawingSettings debugDrawingSettings = debugRenderSetup.CreateDrawingSettings(drawingSettings);
				RenderStateBlock debugRenderStateBlock = debugRenderSetup.GetRenderStateBlock(renderStateBlock);
				RendererList rendererList = default(RendererList);
				RenderingUtils.CreateRendererListWithRenderStateBlock(context, ref cullResults, debugDrawingSettings, filteringSettings, debugRenderStateBlock, ref rendererList);
				this.m_ActiveDebugRendererList.Add(rendererList);
			}
		}

		// Token: 0x06000261 RID: 609 RVA: 0x0000806C File Offset: 0x0000626C
		internal void CreateRendererListsWithDebugRenderState(RenderGraph renderGraph, ref CullingResults cullResults, ref DrawingSettings drawingSettings, ref FilteringSettings filteringSettings, ref RenderStateBlock renderStateBlock)
		{
			this.CreateDebugRenderSetups(filteringSettings);
			foreach (DebugRenderSetup debugRenderSetup in this.m_DebugRenderSetups)
			{
				DrawingSettings debugDrawingSettings = debugRenderSetup.CreateDrawingSettings(drawingSettings);
				RenderStateBlock debugRenderStateBlock = debugRenderSetup.GetRenderStateBlock(renderStateBlock);
				RendererListHandle rendererListHdl = default(RendererListHandle);
				RenderingUtils.CreateRendererListWithRenderStateBlock(renderGraph, ref cullResults, debugDrawingSettings, filteringSettings, debugRenderStateBlock, ref rendererListHdl);
				this.m_ActiveDebugRendererListHdl.Add(rendererListHdl);
			}
		}

		// Token: 0x06000262 RID: 610 RVA: 0x00008104 File Offset: 0x00006304
		internal void PrepareRendererListForRasterPass(IRasterRenderGraphBuilder builder)
		{
			foreach (RendererListHandle rendererListHdl in this.m_ActiveDebugRendererListHdl)
			{
				builder.UseRendererList(in rendererListHdl);
			}
		}

		// Token: 0x06000263 RID: 611 RVA: 0x00008158 File Offset: 0x00006358
		internal void DrawWithRendererList(RasterCommandBuffer cmd)
		{
			foreach (DebugRenderSetup debugRenderSetup in this.m_DebugRenderSetups)
			{
				debugRenderSetup.Begin(cmd);
				RendererList rendererList = default(RendererList);
				if (this.m_ActiveDebugRendererList.Count > 0)
				{
					rendererList = this.m_ActiveDebugRendererList[debugRenderSetup.GetIndex()];
				}
				else if (this.m_ActiveDebugRendererListHdl.Count > 0)
				{
					rendererList = this.m_ActiveDebugRendererListHdl[debugRenderSetup.GetIndex()];
				}
				debugRenderSetup.DrawWithRendererList(cmd, ref rendererList);
				debugRenderSetup.End(cmd);
			}
			this.DisposeDebugRenderLists();
		}

		// Token: 0x040001FC RID: 508
		private readonly DebugHandler m_DebugHandler;

		// Token: 0x040001FD RID: 509
		private readonly FilteringSettings m_FilteringSettings;

		// Token: 0x040001FE RID: 510
		private List<DebugRenderSetup> m_DebugRenderSetups = new List<DebugRenderSetup>(2);

		// Token: 0x040001FF RID: 511
		private List<RendererList> m_ActiveDebugRendererList = new List<RendererList>(2);

		// Token: 0x04000200 RID: 512
		private List<RendererListHandle> m_ActiveDebugRendererListHdl = new List<RendererListHandle>(2);
	}
}
