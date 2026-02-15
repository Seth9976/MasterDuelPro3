using System;

namespace System.Windows.Forms
{
	// Token: 0x0200014C RID: 332
	internal class PlatformDefaultHandler : PlatformMimeIconHandler
	{
		// Token: 0x06000D1B RID: 3355 RVA: 0x00039FD4 File Offset: 0x000381D4
		public override MimeExtensionHandlerStatus Start()
		{
			MimeIconEngine.AddIconByImage("inode/directory", ResourceImageLoader.Get("folder.png"));
			MimeIconEngine.AddIconByImage("unknown/unknown", ResourceImageLoader.Get("text-x-generic.png"));
			MimeIconEngine.AddIconByImage("desktop/desktop", ResourceImageLoader.Get("user-desktop.png"));
			MimeIconEngine.AddIconByImage("directory/home", ResourceImageLoader.Get("user-home.png"));
			MimeIconEngine.AddIconByImage("network/network", ResourceImageLoader.Get("folder-remote.png"));
			MimeIconEngine.AddIconByImage("recently/recently", ResourceImageLoader.Get("document-open.png"));
			MimeIconEngine.AddIconByImage("workplace/workplace", ResourceImageLoader.Get("computer.png"));
			return MimeExtensionHandlerStatus.OK;
		}
	}
}
