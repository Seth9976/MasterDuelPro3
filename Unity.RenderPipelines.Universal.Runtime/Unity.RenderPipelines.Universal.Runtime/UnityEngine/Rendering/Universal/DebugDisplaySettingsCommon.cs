using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x0200002B RID: 43
	internal class DebugDisplaySettingsCommon : IDebugDisplaySettingsData, IDebugDisplaySettingsQuery
	{
		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x00002886 File Offset: 0x00000A86
		public bool AreAnySettingsActive
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060000DA RID: 218 RVA: 0x0000384F File Offset: 0x00001A4F
		public IDebugDisplaySettingsPanelDisposable CreatePanel()
		{
			return new DebugDisplaySettingsCommon.SettingsPanel();
		}

		// Token: 0x0200002C RID: 44
		[DisplayInfo(name = "Frequently Used", order = -1)]
		private class SettingsPanel : DebugDisplaySettingsPanel
		{
			// Token: 0x17000064 RID: 100
			// (get) Token: 0x060000DC RID: 220 RVA: 0x00003856 File Offset: 0x00001A56
			public override DebugUI.Flags Flags
			{
				get
				{
					return DebugUI.Flags.FrequentlyUsed;
				}
			}

			// Token: 0x060000DD RID: 221 RVA: 0x0000385C File Offset: 0x00001A5C
			public SettingsPanel()
			{
				base.AddWidget(new DebugUI.RuntimeDebugShadersMessageBox());
				DebugUI.Widget[] items = DebugManager.instance.GetItems(DebugUI.Flags.FrequentlyUsed);
				for (int i = 0; i < items.Length; i++)
				{
					DebugUI.Widget widget = items[i];
					DebugUI.Foldout foldout = widget as DebugUI.Foldout;
					if (foldout != null)
					{
						if (foldout.contextMenuItems == null)
						{
							foldout.contextMenuItems = new List<DebugUI.Foldout.ContextMenuItem>();
						}
						foldout.contextMenuItems.Add(new DebugUI.Foldout.ContextMenuItem
						{
							displayName = "Go to Section...",
							action = delegate
							{
								int panelIndex = DebugManager.instance.PanelIndex(foldout.panel.displayName);
								if (panelIndex >= 0)
								{
									DebugManager.instance.RequestEditorWindowPanelIndex(panelIndex);
								}
							}
						});
					}
					base.AddWidget(widget);
				}
			}

			// Token: 0x04000100 RID: 256
			private const string k_GoToSectionString = "Go to Section...";
		}
	}
}
