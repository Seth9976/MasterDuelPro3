using System;

namespace System.Text.RegularExpressions
{
	// Token: 0x02000144 RID: 324
	internal readonly struct RegexPrefix
	{
		// Token: 0x0600077C RID: 1916 RVA: 0x00029BA4 File Offset: 0x00027DA4
		internal RegexPrefix(string prefix, bool ci)
		{
			this.Prefix = prefix;
			this.CaseInsensitive = ci;
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x0600077D RID: 1917 RVA: 0x00029BB4 File Offset: 0x00027DB4
		internal bool CaseInsensitive { get; }

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x0600077E RID: 1918 RVA: 0x00029BBC File Offset: 0x00027DBC
		internal static RegexPrefix Empty { get; } = new RegexPrefix(string.Empty, false);

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x0600077F RID: 1919 RVA: 0x00029BC3 File Offset: 0x00027DC3
		internal string Prefix { get; }
	}
}
