using System;
using System.Drawing;

namespace System.Windows.Forms
{
	// Token: 0x0200014D RID: 333
	internal class GnomeHandler : PlatformMimeIconHandler
	{
		// Token: 0x06000D1D RID: 3357 RVA: 0x0003A076 File Offset: 0x00038276
		public override MimeExtensionHandlerStatus Start()
		{
			this.CreateUIIcons();
			return MimeExtensionHandlerStatus.OK;
		}

		// Token: 0x06000D1E RID: 3358 RVA: 0x0003A080 File Offset: 0x00038280
		private void CreateUIIcons()
		{
			this.AddGnomeIcon("unknown/unknown", "gnome-fs-regular");
			this.AddGnomeIcon("inode/directory", "gnome-fs-directory");
			this.AddGnomeIcon("directory/home", "gnome-fs-home");
			this.AddGnomeIcon("desktop/desktop", "gnome-fs-desktop");
			this.AddGnomeIcon("recently/recently", "gnome-fs-directory-accept");
			this.AddGnomeIcon("workplace/workplace", "gnome-fs-client");
			this.AddGnomeIcon("network/network", "gnome-fs-network");
			this.AddGnomeIcon("nfs/nfs", "gnome-fs-nfs");
			this.AddGnomeIcon("smb/smb", "gnome-fs-smb");
			this.AddGnomeIcon("harddisk/harddisk", "gnome-dev-harddisk");
			this.AddGnomeIcon("cdrom/cdrom", "gnome-dev-cdrom");
			this.AddGnomeIcon("removable/removable", "gnome-dev-removable");
		}

		// Token: 0x06000D1F RID: 3359 RVA: 0x0003A150 File Offset: 0x00038350
		private void AddGnomeIcon(string internal_mime_type, string name)
		{
			if (MimeIconEngine.MimeIconIndex.ContainsKey(internal_mime_type))
			{
				return;
			}
			Image image = GnomeUtil.GetIcon(name, 48);
			if (image == null)
			{
				if (internal_mime_type == "unknown/unknown")
				{
					image = ResourceImageLoader.Get("text-x-generic.png");
				}
				else if (internal_mime_type == "inode/directory")
				{
					image = ResourceImageLoader.Get("folder.png");
				}
				else if (internal_mime_type == "directory/home")
				{
					image = ResourceImageLoader.Get("user-home.png");
				}
				else if (internal_mime_type == "desktop/desktop")
				{
					image = ResourceImageLoader.Get("user-desktop.png");
				}
				else if (internal_mime_type == "recently/recently")
				{
					image = ResourceImageLoader.Get("document-open.png");
				}
				else if (internal_mime_type == "workplace/workplace")
				{
					image = ResourceImageLoader.Get("computer.png");
				}
				else if (internal_mime_type == "network/network" || internal_mime_type == "nfs/nfs" || internal_mime_type == "smb/smb")
				{
					image = ResourceImageLoader.Get("folder-remote.png");
				}
				else if (internal_mime_type == "harddisk/harddisk" || internal_mime_type == "cdrom/cdrom" || internal_mime_type == "removable/removable")
				{
					image = ResourceImageLoader.Get("text-x-generic.png");
				}
			}
			if (image != null)
			{
				int num = MimeIconEngine.SmallIcons.Images.Add(image, Color.Transparent);
				MimeIconEngine.LargeIcons.Images.Add(image, Color.Transparent);
				MimeIconEngine.MimeIconIndex.Add(internal_mime_type, num);
			}
		}

		// Token: 0x06000D20 RID: 3360 RVA: 0x0003A2D0 File Offset: 0x000384D0
		public override object AddAndGetIconIndex(string filename, string mime_type)
		{
			int num = -1;
			Image icon = GnomeUtil.GetIcon(filename, mime_type, 48);
			if (icon != null)
			{
				num = MimeIconEngine.SmallIcons.Images.Add(icon, Color.Transparent);
				MimeIconEngine.LargeIcons.Images.Add(icon, Color.Transparent);
				MimeIconEngine.MimeIconIndex.Add(mime_type, num);
			}
			return num;
		}

		// Token: 0x06000D21 RID: 3361 RVA: 0x0003A330 File Offset: 0x00038530
		public override object AddAndGetIconIndex(string mime_type)
		{
			int num = -1;
			Image icon = GnomeUtil.GetIcon(mime_type, 48);
			if (icon != null)
			{
				num = MimeIconEngine.SmallIcons.Images.Add(icon, Color.Transparent);
				MimeIconEngine.LargeIcons.Images.Add(icon, Color.Transparent);
				MimeIconEngine.MimeIconIndex.Add(mime_type, num);
			}
			return num;
		}
	}
}
