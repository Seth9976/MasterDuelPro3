using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020000D1 RID: 209
	public interface IDebugDisplaySettingsData : IDebugDisplaySettingsQuery
	{
		// Token: 0x060006E6 RID: 1766
		IDebugDisplaySettingsPanelDisposable CreatePanel();
	}
}
