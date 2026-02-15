using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering
{
	// Token: 0x02000071 RID: 113
	public class DebugDisplaySettingsStats<TProfileId> : IDebugDisplaySettingsData, IDebugDisplaySettingsQuery where TProfileId : Enum
	{
		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600051D RID: 1309 RVA: 0x00009E59 File Offset: 0x00008059
		public DebugDisplayStats<TProfileId> debugDisplayStats { get; }

		// Token: 0x0600051E RID: 1310 RVA: 0x00009E61 File Offset: 0x00008061
		public DebugDisplaySettingsStats(DebugDisplayStats<TProfileId> debugDisplayStats)
		{
			this.debugDisplayStats = debugDisplayStats;
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600051F RID: 1311 RVA: 0x000090C6 File Offset: 0x000072C6
		public bool AreAnySettingsActive
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x00009E70 File Offset: 0x00008070
		public IDebugDisplaySettingsPanelDisposable CreatePanel()
		{
			return new DebugDisplaySettingsStats<TProfileId>.StatsPanel(this);
		}

		// Token: 0x02000072 RID: 114
		[DisplayInfo(name = "Display Stats", order = -2147483648)]
		private class StatsPanel : DebugDisplaySettingsPanel
		{
			// Token: 0x1700004F RID: 79
			// (get) Token: 0x06000521 RID: 1313 RVA: 0x00009E78 File Offset: 0x00008078
			public override DebugUI.Flags Flags
			{
				get
				{
					return DebugUI.Flags.RuntimeOnly;
				}
			}

			// Token: 0x06000522 RID: 1314 RVA: 0x00009E7C File Offset: 0x0000807C
			public StatsPanel(DebugDisplaySettingsStats<TProfileId> displaySettingsStats)
			{
				this.m_Data = displaySettingsStats;
				this.m_Data.debugDisplayStats.EnableProfilingRecorders();
				List<DebugUI.Widget> list = new List<DebugUI.Widget>();
				this.m_Data.debugDisplayStats.RegisterDebugUI(list);
				foreach (DebugUI.Widget w in list)
				{
					base.AddWidget(w);
				}
			}

			// Token: 0x06000523 RID: 1315 RVA: 0x00009F00 File Offset: 0x00008100
			public override void Dispose()
			{
				this.m_Data.debugDisplayStats.DisableProfilingRecorders();
				base.Dispose();
			}

			// Token: 0x0400015B RID: 347
			private readonly DebugDisplaySettingsStats<TProfileId> m_Data;
		}
	}
}
