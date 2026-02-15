using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020000D2 RID: 210
	public interface IDebugDisplaySettingsPanel
	{
		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060006E7 RID: 1767
		string PanelName { get; }

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060006E8 RID: 1768
		DebugUI.Widget[] Widgets { get; }

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060006E9 RID: 1769
		DebugUI.Flags Flags { get; }
	}
}
