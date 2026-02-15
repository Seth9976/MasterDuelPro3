using System;

namespace MDPro3.UI.PropertyOverride
{
	// Token: 0x02001438 RID: 5176
	[Serializable]
	public class OverridePropertyBase<T>
	{
		// Token: 0x0600968C RID: 38540 RVA: 0x0015D639 File Offset: 0x0015B839
		public T GetPlatformValue(bool mobile)
		{
			if (mobile)
			{
				return this.m_MobileValue;
			}
			return this.m_DefaultValue;
		}

		// Token: 0x0600968D RID: 38541 RVA: 0x0015D64B File Offset: 0x0015B84B
		public void SetPlatformValue(bool mobile, T value)
		{
			if (mobile)
			{
				this.m_MobileValue = value;
				return;
			}
			this.m_DefaultValue = value;
		}

		// Token: 0x0400D4E8 RID: 54504
		public T m_DefaultValue;

		// Token: 0x0400D4E9 RID: 54505
		public T m_MobileValue;
	}
}
