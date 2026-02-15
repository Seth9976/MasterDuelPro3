using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	// Token: 0x0200014E RID: 334
	internal class GnomeUtil
	{
		// Token: 0x06000D23 RID: 3363
		[DllImport("libgdk_pixbuf-2.0")]
		private static extern bool gdk_pixbuf_save_to_buffer(IntPtr pixbuf, out IntPtr buffer, out UIntPtr buffer_size, string type, out IntPtr error, IntPtr option_dummy);

		// Token: 0x06000D24 RID: 3364
		[DllImport("libglib-2.0")]
		private static extern void g_free(IntPtr mem);

		// Token: 0x06000D25 RID: 3365
		[DllImport("libgdk-x11-2.0")]
		private static extern bool gdk_init_check(IntPtr argc, IntPtr argv);

		// Token: 0x06000D26 RID: 3366
		[DllImport("libgobject-2.0")]
		private static extern void g_object_unref(IntPtr nativeObject);

		// Token: 0x06000D27 RID: 3367
		[DllImport("libgnomeui-2")]
		private static extern string gnome_icon_lookup(IntPtr icon_theme, IntPtr thumbnail_factory, string file_uri, string custom_icon, IntPtr file_info, string mime_type, GnomeUtil.GnomeIconLookupFlags flags, IntPtr result);

		// Token: 0x06000D28 RID: 3368
		[DllImport("libgtk-x11-2.0")]
		private static extern IntPtr gtk_icon_theme_get_default();

		// Token: 0x06000D29 RID: 3369
		[DllImport("libgtk-x11-2.0")]
		private static extern IntPtr gtk_icon_theme_load_icon(IntPtr icon_theme, string icon_name, int size, GnomeUtil.GtkIconLookupFlags flags, out IntPtr error);

		// Token: 0x06000D2A RID: 3370 RVA: 0x0003A38E File Offset: 0x0003858E
		private static void Init()
		{
			GnomeUtil.gdk_init_check(IntPtr.Zero, IntPtr.Zero);
			GnomeUtil.inited = true;
			GnomeUtil.default_icon_theme = GnomeUtil.gtk_icon_theme_get_default();
		}

		// Token: 0x06000D2B RID: 3371 RVA: 0x0003A3B0 File Offset: 0x000385B0
		public static Image GetIcon(string file_name, string mime_type, int size)
		{
			if (!GnomeUtil.inited)
			{
				GnomeUtil.Init();
			}
			Uri uri = new Uri(file_name);
			string text;
			try
			{
				text = GnomeUtil.gnome_icon_lookup(GnomeUtil.default_icon_theme, IntPtr.Zero, uri.AbsoluteUri, null, IntPtr.Zero, mime_type, GnomeUtil.GnomeIconLookupFlags.GNOME_ICON_LOOKUP_FLAGS_NONE, IntPtr.Zero);
			}
			catch
			{
				return null;
			}
			IntPtr zero = IntPtr.Zero;
			IntPtr intPtr = GnomeUtil.gtk_icon_theme_load_icon(GnomeUtil.default_icon_theme, text, size, GnomeUtil.GtkIconLookupFlags.GTK_ICON_LOOKUP_USE_BUILTIN, out zero);
			if (zero != IntPtr.Zero)
			{
				return null;
			}
			return GnomeUtil.GdkPixbufToImage(intPtr);
		}

		// Token: 0x06000D2C RID: 3372 RVA: 0x0003A43C File Offset: 0x0003863C
		public static Image GetIcon(string icon, int size)
		{
			if (!GnomeUtil.inited)
			{
				GnomeUtil.Init();
			}
			IntPtr zero = IntPtr.Zero;
			IntPtr intPtr = GnomeUtil.gtk_icon_theme_load_icon(GnomeUtil.default_icon_theme, icon, size, GnomeUtil.GtkIconLookupFlags.GTK_ICON_LOOKUP_USE_BUILTIN, out zero);
			if (zero != IntPtr.Zero)
			{
				return null;
			}
			return GnomeUtil.GdkPixbufToImage(intPtr);
		}

		// Token: 0x06000D2D RID: 3373 RVA: 0x0003A480 File Offset: 0x00038680
		public static Image GdkPixbufToImage(IntPtr pixbuf)
		{
			IntPtr zero = IntPtr.Zero;
			string text = "png";
			IntPtr intPtr;
			UIntPtr uintPtr;
			if (!GnomeUtil.gdk_pixbuf_save_to_buffer(pixbuf, out intPtr, out uintPtr, text, out zero, IntPtr.Zero))
			{
				return null;
			}
			int num = (int)(uint)uintPtr;
			byte[] array = new byte[num];
			Marshal.Copy(intPtr, array, 0, num);
			GnomeUtil.g_free(intPtr);
			GnomeUtil.g_object_unref(pixbuf);
			return Image.FromStream(new MemoryStream(array));
		}

		// Token: 0x04000853 RID: 2131
		private static bool inited = false;

		// Token: 0x04000854 RID: 2132
		private static IntPtr default_icon_theme = IntPtr.Zero;

		// Token: 0x0200014F RID: 335
		private enum GnomeIconLookupFlags
		{
			// Token: 0x04000856 RID: 2134
			GNOME_ICON_LOOKUP_FLAGS_NONE,
			// Token: 0x04000857 RID: 2135
			GNOME_ICON_LOOKUP_FLAGS_EMBEDDING_TEXT,
			// Token: 0x04000858 RID: 2136
			GNOME_ICON_LOOKUP_FLAGS_SHOW_SMALL_IMAGES_AS_THEMSELVES,
			// Token: 0x04000859 RID: 2137
			GNOME_ICON_LOOKUP_FLAGS_ALLOW_SVG_AS_THEMSELVES = 4
		}

		// Token: 0x02000150 RID: 336
		private enum GtkIconLookupFlags
		{
			// Token: 0x0400085B RID: 2139
			GTK_ICON_LOOKUP_NO_SVG = 1,
			// Token: 0x0400085C RID: 2140
			GTK_ICON_LOOKUP_FORCE_SVG,
			// Token: 0x0400085D RID: 2141
			GTK_ICON_LOOKUP_USE_BUILTIN = 4
		}
	}
}
