using System;

namespace System.Net
{
	// Token: 0x020003EB RID: 1003
	internal class FileWebRequestCreator : IWebRequestCreate
	{
		// Token: 0x060018FF RID: 6399 RVA: 0x000026E5 File Offset: 0x000008E5
		internal FileWebRequestCreator()
		{
		}

		// Token: 0x06001900 RID: 6400 RVA: 0x0006AFFC File Offset: 0x000691FC
		public WebRequest Create(Uri uri)
		{
			return new FileWebRequest(uri);
		}
	}
}
