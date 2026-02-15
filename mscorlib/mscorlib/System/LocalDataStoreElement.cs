using System;

namespace System
{
	// Token: 0x02000190 RID: 400
	internal sealed class LocalDataStoreElement
	{
		// Token: 0x06000E99 RID: 3737 RVA: 0x0003D088 File Offset: 0x0003B288
		public LocalDataStoreElement(long cookie)
		{
			this.m_cookie = cookie;
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x06000E9A RID: 3738 RVA: 0x0003D097 File Offset: 0x0003B297
		// (set) Token: 0x06000E9B RID: 3739 RVA: 0x0003D09F File Offset: 0x0003B29F
		public object Value
		{
			get
			{
				return this.m_value;
			}
			set
			{
				this.m_value = value;
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x06000E9C RID: 3740 RVA: 0x0003D0A8 File Offset: 0x0003B2A8
		public long Cookie
		{
			get
			{
				return this.m_cookie;
			}
		}

		// Token: 0x040005C7 RID: 1479
		private object m_value;

		// Token: 0x040005C8 RID: 1480
		private long m_cookie;
	}
}
