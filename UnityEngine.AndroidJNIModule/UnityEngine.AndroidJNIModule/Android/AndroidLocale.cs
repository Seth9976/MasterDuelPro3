using System;

namespace UnityEngine.Android
{
	// Token: 0x02000013 RID: 19
	public class AndroidLocale
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000105 RID: 261 RVA: 0x00006BD1 File Offset: 0x00004DD1
		public string country { get; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000106 RID: 262 RVA: 0x00006BD9 File Offset: 0x00004DD9
		public string language { get; }

		// Token: 0x06000107 RID: 263 RVA: 0x00006BE1 File Offset: 0x00004DE1
		internal AndroidLocale(string _country, string _language)
		{
			this.country = _country;
			this.language = _language;
		}
	}
}
