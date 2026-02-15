using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering
{
	// Token: 0x02000073 RID: 115
	public class DebugDisplaySettingsUI : IDebugData
	{
		// Token: 0x06000524 RID: 1316 RVA: 0x00009F18 File Offset: 0x00008118
		private void Reset()
		{
			if (this.m_Settings != null)
			{
				this.m_Settings.Reset();
				this.UnregisterDebug();
				this.RegisterDebug(this.m_Settings);
				DebugManager.instance.RefreshEditor();
			}
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x00009F4C File Offset: 0x0000814C
		public void RegisterDebug(IDebugDisplaySettings settings)
		{
			DebugManager debugManager = DebugManager.instance;
			List<IDebugDisplaySettingsPanelDisposable> panels = new List<IDebugDisplaySettingsPanelDisposable>();
			debugManager.RegisterData(this);
			this.m_Settings = settings;
			this.m_DisposablePanels = panels;
			this.m_Settings.Add(new DebugDisplaySettingsRenderGraph());
			Action<IDebugDisplaySettingsData> onExecute = delegate(IDebugDisplaySettingsData data)
			{
				IDebugDisplaySettingsPanelDisposable disposableSettingsPanel = data.CreatePanel();
				DebugUI.Widget[] panelWidgets = disposableSettingsPanel.Widgets;
				DebugManager debugManager2 = debugManager;
				string panelName = disposableSettingsPanel.PanelName;
				bool flag = true;
				DebugDisplaySettingsPanel debugDisplaySettingsPanel = disposableSettingsPanel as DebugDisplaySettingsPanel;
				DebugUI.Panel panel = debugManager2.GetPanel(panelName, flag, (debugDisplaySettingsPanel != null) ? debugDisplaySettingsPanel.Order : 0, false);
				ObservableList<DebugUI.Widget> panelChildren = panel.children;
				panel.flags = disposableSettingsPanel.Flags;
				panels.Add(disposableSettingsPanel);
				panelChildren.Add(panelWidgets);
			};
			this.m_Settings.ForEach(onExecute);
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x00009FC0 File Offset: 0x000081C0
		public void UnregisterDebug()
		{
			DebugManager debugManager = DebugManager.instance;
			if (this.m_DisposablePanels != null)
			{
				foreach (IDebugDisplaySettingsPanelDisposable disposableSettingsPanel in this.m_DisposablePanels)
				{
					DebugUI.Widget[] panelWidgets = disposableSettingsPanel.Widgets;
					string panelId = disposableSettingsPanel.PanelName;
					ObservableList<DebugUI.Widget> children = debugManager.GetPanel(panelId, true, 0, false).children;
					disposableSettingsPanel.Dispose();
					children.Remove(panelWidgets);
				}
				this.m_DisposablePanels = null;
			}
			debugManager.UnregisterData(this);
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x0000A050 File Offset: 0x00008250
		public Action GetReset()
		{
			return new Action(this.Reset);
		}

		// Token: 0x0400015C RID: 348
		private IEnumerable<IDebugDisplaySettingsPanelDisposable> m_DisposablePanels;

		// Token: 0x0400015D RID: 349
		private IDebugDisplaySettings m_Settings;
	}
}
