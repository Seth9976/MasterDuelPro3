using System;

namespace System.Net
{
	// Token: 0x0200039A RID: 922
	internal class FtpWebRequestCreator : IWebRequestCreate
	{
		// Token: 0x0600172B RID: 5931 RVA: 0x000026E5 File Offset: 0x000008E5
		internal FtpWebRequestCreator()
		{
		}

		// Token: 0x0600172C RID: 5932 RVA: 0x00063B01 File Offset: 0x00061D01
		public WebRequest Create(Uri uri)
		{
			return new FtpWebRequest(uri);
		}
	}
}
