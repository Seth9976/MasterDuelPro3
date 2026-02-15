using System;

namespace System.Net
{
	// Token: 0x02000413 RID: 1043
	internal class HttpRequestCreator : IWebRequestCreate
	{
		// Token: 0x06001A15 RID: 6677 RVA: 0x000026E5 File Offset: 0x000008E5
		internal HttpRequestCreator()
		{
		}

		// Token: 0x06001A16 RID: 6678 RVA: 0x00070A03 File Offset: 0x0006EC03
		public WebRequest Create(Uri uri)
		{
			return new HttpWebRequest(uri);
		}
	}
}
