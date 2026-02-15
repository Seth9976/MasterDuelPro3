using System;
using System.Collections.Generic;
using UnityEngine.Rendering.RenderGraphModule;

namespace UnityEngine.Rendering
{
	// Token: 0x0200015A RID: 346
	internal class DebugDisplaySettingsRenderGraph : IDebugDisplaySettingsData, IDebugDisplaySettingsQuery
	{
		// Token: 0x06000A7D RID: 2685 RVA: 0x00024CE4 File Offset: 0x00022EE4
		public DebugDisplaySettingsRenderGraph()
		{
			foreach (RenderGraph renderGraph in RenderGraph.GetRegisteredRenderGraphs())
			{
				renderGraph.debugParams.Reset();
			}
		}

		// Token: 0x06000A7E RID: 2686 RVA: 0x00024D40 File Offset: 0x00022F40
		IDebugDisplaySettingsPanelDisposable IDebugDisplaySettingsData.CreatePanel()
		{
			return new DebugDisplaySettingsRenderGraph.SettingsPanel(this);
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000A7F RID: 2687 RVA: 0x00024D48 File Offset: 0x00022F48
		public bool AreAnySettingsActive
		{
			get
			{
				using (List<RenderGraph>.Enumerator enumerator = RenderGraph.GetRegisteredRenderGraphs().GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.areAnySettingsActive)
						{
							return true;
						}
					}
				}
				return false;
			}
		}

		// Token: 0x0200015B RID: 347
		[DisplayInfo(name = "Render Graph", order = 10)]
		private class SettingsPanel : DebugDisplaySettingsPanel
		{
			// Token: 0x17000131 RID: 305
			// (get) Token: 0x06000A80 RID: 2688 RVA: 0x00024DA0 File Offset: 0x00022FA0
			public override string PanelName
			{
				get
				{
					return "Render Graph";
				}
			}

			// Token: 0x06000A81 RID: 2689 RVA: 0x00024DA8 File Offset: 0x00022FA8
			public SettingsPanel(DebugDisplaySettingsRenderGraph _)
			{
				bool usingRenderGraph = false;
				foreach (RenderGraph renderGraph in RenderGraph.GetRegisteredRenderGraphs())
				{
					usingRenderGraph = true;
					foreach (DebugUI.Widget item in renderGraph.GetWidgetList())
					{
						base.AddWidget(item);
					}
				}
				if (!usingRenderGraph)
				{
					base.AddWidget(new DebugUI.MessageBox
					{
						displayName = "Warning: The current render pipeline does not have Render Graphs Registered",
						style = DebugUI.MessageBox.Style.Warning
					});
				}
			}
		}
	}
}
