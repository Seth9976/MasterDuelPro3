using System;
using System.Collections;
using System.Drawing;

namespace System.Windows.Forms
{
	// Token: 0x0200014A RID: 330
	internal class MimeIconEngine
	{
		// Token: 0x06000D12 RID: 3346 RVA: 0x00039C04 File Offset: 0x00037E04
		static MimeIconEngine()
		{
			MimeIconEngine.SmallIcons.ColorDepth = ColorDepth.Depth32Bit;
			MimeIconEngine.SmallIcons.TransparentColor = Color.Transparent;
			MimeIconEngine.LargeIcons.ColorDepth = ColorDepth.Depth32Bit;
			MimeIconEngine.LargeIcons.TransparentColor = Color.Transparent;
			string text = Environment.GetEnvironmentVariable("DESKTOP_SESSION");
			if (text != null)
			{
				text = text.ToUpper();
				if (text == "DEFAULT" && Environment.GetEnvironmentVariable("GNOME_DESKTOP_SESSION_ID") != null)
				{
					text = "GNOME";
				}
			}
			else
			{
				text = string.Empty;
			}
			if (!Mime.MimeAvailable || !(text == "GNOME"))
			{
				MimeIconEngine.SmallIcons.ImageSize = new Size(16, 16);
				MimeIconEngine.LargeIcons.ImageSize = new Size(48, 48);
				MimeIconEngine.platformMimeHandler = new PlatformDefaultHandler();
				MimeIconEngine.platformMimeHandler.Start();
				return;
			}
			MimeIconEngine.SmallIcons.ImageSize = new Size(24, 24);
			MimeIconEngine.LargeIcons.ImageSize = new Size(48, 48);
			MimeIconEngine.platformMimeHandler = new GnomeHandler();
			if (MimeIconEngine.platformMimeHandler.Start() == MimeExtensionHandlerStatus.OK)
			{
				MimeIconEngine.platform = EPlatformHandler.GNOME;
				return;
			}
			MimeIconEngine.LargeIcons.Images.Clear();
			MimeIconEngine.SmallIcons.Images.Clear();
			MimeIconEngine.platformMimeHandler = new PlatformDefaultHandler();
			MimeIconEngine.platformMimeHandler.Start();
		}

		// Token: 0x06000D13 RID: 3347 RVA: 0x00039D80 File Offset: 0x00037F80
		public static int GetIconIndexForFile(string full_filename)
		{
			object obj = MimeIconEngine.lock_object;
			int num;
			lock (obj)
			{
				if (MimeIconEngine.platform == EPlatformHandler.Default)
				{
					num = (int)MimeIconEngine.MimeIconIndex["unknown/unknown"];
				}
				else
				{
					string mimeTypeForFile = Mime.GetMimeTypeForFile(full_filename);
					object obj2 = MimeIconEngine.GetIconIndex(mimeTypeForFile);
					if (obj2 == null)
					{
						if (full_filename.IndexOf(':') > 1)
						{
							obj2 = MimeIconEngine.MimeIconIndex["unknown/unknown"];
						}
						else
						{
							obj2 = MimeIconEngine.platformMimeHandler.AddAndGetIconIndex(full_filename, mimeTypeForFile);
							if (obj2 == null)
							{
								obj2 = MimeIconEngine.MimeIconIndex["unknown/unknown"];
							}
						}
					}
					num = (int)obj2;
				}
			}
			return num;
		}

		// Token: 0x06000D14 RID: 3348 RVA: 0x00039E30 File Offset: 0x00038030
		public static int GetIconIndexForMimeType(string mime_type)
		{
			object obj = MimeIconEngine.lock_object;
			int num;
			lock (obj)
			{
				if (MimeIconEngine.platform == EPlatformHandler.Default)
				{
					if (mime_type == "inode/directory")
					{
						num = (int)MimeIconEngine.MimeIconIndex["inode/directory"];
					}
					else
					{
						num = (int)MimeIconEngine.MimeIconIndex["unknown/unknown"];
					}
				}
				else
				{
					object obj2 = MimeIconEngine.GetIconIndex(mime_type);
					if (obj2 == null)
					{
						obj2 = MimeIconEngine.platformMimeHandler.AddAndGetIconIndex(mime_type);
						if (obj2 == null)
						{
							obj2 = MimeIconEngine.MimeIconIndex["unknown/unknown"];
						}
					}
					num = (int)obj2;
				}
			}
			return num;
		}

		// Token: 0x06000D15 RID: 3349 RVA: 0x00039EDC File Offset: 0x000380DC
		internal static void AddIconByImage(string mime_type, Image image)
		{
			int num = MimeIconEngine.SmallIcons.Images.Add(image, Color.Transparent);
			MimeIconEngine.LargeIcons.Images.Add(image, Color.Transparent);
			MimeIconEngine.MimeIconIndex.Add(mime_type, num);
		}

		// Token: 0x06000D16 RID: 3350 RVA: 0x00039F28 File Offset: 0x00038128
		private static object GetIconIndex(string mime_type)
		{
			object obj = null;
			if (mime_type != null)
			{
				obj = MimeIconEngine.MimeIconIndex[mime_type];
				if (obj == null)
				{
					string mimeAlias = Mime.GetMimeAlias(mime_type);
					if (mimeAlias != null)
					{
						string[] array = mimeAlias.Split(new char[] { ',' });
						for (int i = 0; i < array.Length; i++)
						{
							obj = MimeIconEngine.MimeIconIndex[array[i]];
							if (obj != null)
							{
								return obj;
							}
						}
					}
					string text = Mime.SubClasses[mime_type];
					if (text != null)
					{
						obj = MimeIconEngine.MimeIconIndex[text];
						if (obj != null)
						{
							return obj;
						}
					}
					string text2 = mime_type.Substring(0, mime_type.IndexOf('/'));
					return MimeIconEngine.MimeIconIndex[text2];
				}
			}
			return obj;
		}

		// Token: 0x0400084D RID: 2125
		public static ImageList SmallIcons = new ImageList();

		// Token: 0x0400084E RID: 2126
		public static ImageList LargeIcons = new ImageList();

		// Token: 0x0400084F RID: 2127
		private static EPlatformHandler platform = EPlatformHandler.Default;

		// Token: 0x04000850 RID: 2128
		internal static Hashtable MimeIconIndex = new Hashtable();

		// Token: 0x04000851 RID: 2129
		private static PlatformMimeIconHandler platformMimeHandler = null;

		// Token: 0x04000852 RID: 2130
		private static object lock_object = new object();
	}
}
