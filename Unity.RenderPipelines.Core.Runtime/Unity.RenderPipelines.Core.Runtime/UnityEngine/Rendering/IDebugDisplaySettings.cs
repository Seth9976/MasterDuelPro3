using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020000D0 RID: 208
	public interface IDebugDisplaySettings
	{
		// Token: 0x060006E3 RID: 1763
		void Reset();

		// Token: 0x060006E4 RID: 1764
		void ForEach(Action<IDebugDisplaySettingsData> onExecute);

		// Token: 0x060006E5 RID: 1765 RVA: 0x000104E9 File Offset: 0x0000E6E9
		IDebugDisplaySettingsData Add(IDebugDisplaySettingsData newData)
		{
			return null;
		}
	}
}
