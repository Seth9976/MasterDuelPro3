using System;

namespace Newtonsoft.Json.Linq
{
	// Token: 0x0200017F RID: 383
	public class JsonSelectSettings
	{
		// Token: 0x17000211 RID: 529
		// (get) Token: 0x06000CA4 RID: 3236 RVA: 0x000380DD File Offset: 0x000362DD
		// (set) Token: 0x06000CA5 RID: 3237 RVA: 0x000380E5 File Offset: 0x000362E5
		public TimeSpan? RegexMatchTimeout { get; set; }

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x06000CA6 RID: 3238 RVA: 0x000380EE File Offset: 0x000362EE
		// (set) Token: 0x06000CA7 RID: 3239 RVA: 0x000380F6 File Offset: 0x000362F6
		public bool ErrorWhenNoMatch { get; set; }
	}
}
