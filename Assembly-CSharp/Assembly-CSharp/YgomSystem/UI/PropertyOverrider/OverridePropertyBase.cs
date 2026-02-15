using System;
using YgomSystem.Utility;

namespace YgomSystem.UI.PropertyOverrider
{
	// Token: 0x02000666 RID: 1638
	[Serializable]
	public class OverridePropertyBase<T>
	{
		// Token: 0x0600331D RID: 13085 RVA: 0x000F2A40 File Offset: 0x000F0C40
		public T GetPlatformValue()
		{
			return default(T);
		}

		// Token: 0x0600331E RID: 13086 RVA: 0x000F2A58 File Offset: 0x000F0C58
		public T GetPlatformValue(DeviceInfo.PlatformType platformType)
		{
			return default(T);
		}

		// Token: 0x0600331F RID: 13087 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetPlatformValue(DeviceInfo.PlatformType platformType, T value)
		{
		}

		// Token: 0x04002F67 RID: 12135
		public T m_DefaultValue;

		// Token: 0x04002F68 RID: 12136
		public T m_MobileValue;
	}
}
