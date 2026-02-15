using System;

namespace UnityEngine.ResourceManagement.ResourceProviders
{
	// Token: 0x02000061 RID: 97
	[Serializable]
	public class ProviderLoadRequestOptions
	{
		// Token: 0x06000223 RID: 547 RVA: 0x00009190 File Offset: 0x00007390
		public ProviderLoadRequestOptions Copy()
		{
			return (ProviderLoadRequestOptions)base.MemberwiseClone();
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000224 RID: 548 RVA: 0x0000919D File Offset: 0x0000739D
		// (set) Token: 0x06000225 RID: 549 RVA: 0x000091A5 File Offset: 0x000073A5
		public bool IgnoreFailures
		{
			get
			{
				return this.m_IgnoreFailures;
			}
			set
			{
				this.m_IgnoreFailures = value;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000226 RID: 550 RVA: 0x000091AE File Offset: 0x000073AE
		// (set) Token: 0x06000227 RID: 551 RVA: 0x000091B6 File Offset: 0x000073B6
		public int WebRequestTimeout
		{
			get
			{
				return this.m_WebRequestTimeout;
			}
			set
			{
				this.m_WebRequestTimeout = value;
			}
		}

		// Token: 0x040000F7 RID: 247
		[SerializeField]
		private bool m_IgnoreFailures;

		// Token: 0x040000F8 RID: 248
		private int m_WebRequestTimeout;
	}
}
