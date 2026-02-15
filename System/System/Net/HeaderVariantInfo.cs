using System;

namespace System.Net
{
	// Token: 0x020003E5 RID: 997
	internal struct HeaderVariantInfo
	{
		// Token: 0x060018C6 RID: 6342 RVA: 0x0006980D File Offset: 0x00067A0D
		internal HeaderVariantInfo(string name, CookieVariant variant)
		{
			this.m_name = name;
			this.m_variant = variant;
		}

		// Token: 0x1700055B RID: 1371
		// (get) Token: 0x060018C7 RID: 6343 RVA: 0x0006981D File Offset: 0x00067A1D
		internal string Name
		{
			get
			{
				return this.m_name;
			}
		}

		// Token: 0x1700055C RID: 1372
		// (get) Token: 0x060018C8 RID: 6344 RVA: 0x00069825 File Offset: 0x00067A25
		internal CookieVariant Variant
		{
			get
			{
				return this.m_variant;
			}
		}

		// Token: 0x04000FD0 RID: 4048
		private string m_name;

		// Token: 0x04000FD1 RID: 4049
		private CookieVariant m_variant;
	}
}
