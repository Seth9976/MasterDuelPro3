using System;

namespace System.Net.Cache
{
	// Token: 0x020004A0 RID: 1184
	internal class RequestCacheBinding
	{
		// Token: 0x1700066C RID: 1644
		// (get) Token: 0x06001CCE RID: 7374 RVA: 0x0007D3E6 File Offset: 0x0007B5E6
		internal RequestCache Cache
		{
			get
			{
				return this.m_RequestCache;
			}
		}

		// Token: 0x1700066D RID: 1645
		// (get) Token: 0x06001CCF RID: 7375 RVA: 0x0007D3EE File Offset: 0x0007B5EE
		internal RequestCacheValidator Validator
		{
			get
			{
				return this.m_CacheValidator;
			}
		}

		// Token: 0x040013D8 RID: 5080
		private RequestCache m_RequestCache;

		// Token: 0x040013D9 RID: 5081
		private RequestCacheValidator m_CacheValidator;
	}
}
