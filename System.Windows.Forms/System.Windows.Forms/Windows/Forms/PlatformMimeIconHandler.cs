using System;

namespace System.Windows.Forms
{
	// Token: 0x0200014B RID: 331
	internal abstract class PlatformMimeIconHandler
	{
		// Token: 0x06000D17 RID: 3351
		public abstract MimeExtensionHandlerStatus Start();

		// Token: 0x06000D18 RID: 3352 RVA: 0x00003C7A File Offset: 0x00001E7A
		public virtual object AddAndGetIconIndex(string filename, string mime_type)
		{
			return null;
		}

		// Token: 0x06000D19 RID: 3353 RVA: 0x00003C7A File Offset: 0x00001E7A
		public virtual object AddAndGetIconIndex(string mime_type)
		{
			return null;
		}
	}
}
