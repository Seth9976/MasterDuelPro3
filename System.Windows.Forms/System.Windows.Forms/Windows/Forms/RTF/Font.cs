using System;

namespace System.Windows.Forms.RTF
{
	// Token: 0x02000383 RID: 899
	internal class Font
	{
		// Token: 0x06001D2F RID: 7471 RVA: 0x0008AEC0 File Offset: 0x000890C0
		public Font(RTF rtf)
		{
			this.rtf = rtf;
			this.num = -1;
			this.name = string.Empty;
			lock (rtf)
			{
				if (rtf.Fonts == null)
				{
					rtf.Fonts = this;
				}
				else
				{
					Font fonts = rtf.Fonts;
					while (fonts.next != null)
					{
						fonts = fonts.next;
					}
					fonts.next = this;
				}
			}
		}

		// Token: 0x17000745 RID: 1861
		// (get) Token: 0x06001D30 RID: 7472 RVA: 0x0008AF44 File Offset: 0x00089144
		// (set) Token: 0x06001D31 RID: 7473 RVA: 0x0008AF4C File Offset: 0x0008914C
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x17000746 RID: 1862
		// (get) Token: 0x06001D32 RID: 7474 RVA: 0x0008AF55 File Offset: 0x00089155
		// (set) Token: 0x06001D33 RID: 7475 RVA: 0x0008AF5D File Offset: 0x0008915D
		public int Num
		{
			get
			{
				return this.num;
			}
			set
			{
				Font.DeleteFont(this.rtf, value);
				this.num = value;
			}
		}

		// Token: 0x17000747 RID: 1863
		// (set) Token: 0x06001D34 RID: 7476 RVA: 0x0008AF73 File Offset: 0x00089173
		public int Family
		{
			set
			{
				this.family = value;
			}
		}

		// Token: 0x17000748 RID: 1864
		// (set) Token: 0x06001D35 RID: 7477 RVA: 0x0008AF7C File Offset: 0x0008917C
		public CharsetType Charset
		{
			set
			{
				this.charset = value;
			}
		}

		// Token: 0x17000749 RID: 1865
		// (set) Token: 0x06001D36 RID: 7478 RVA: 0x0008AF85 File Offset: 0x00089185
		public int Pitch
		{
			set
			{
				this.pitch = value;
			}
		}

		// Token: 0x1700074A RID: 1866
		// (set) Token: 0x06001D37 RID: 7479 RVA: 0x0008AF8E File Offset: 0x0008918E
		public int Type
		{
			set
			{
				this.type = value;
			}
		}

		// Token: 0x1700074B RID: 1867
		// (set) Token: 0x06001D38 RID: 7480 RVA: 0x0008AF97 File Offset: 0x00089197
		public int Codepage
		{
			set
			{
				this.codepage = value;
			}
		}

		// Token: 0x06001D39 RID: 7481 RVA: 0x0008AFA0 File Offset: 0x000891A0
		public static bool DeleteFont(RTF rtf, int font_number)
		{
			lock (rtf)
			{
				Font fonts = rtf.Fonts;
				Font font = null;
				while (fonts != null && fonts.num != font_number)
				{
					font = fonts;
					fonts = fonts.next;
				}
				if (fonts != null)
				{
					if (fonts == rtf.Fonts)
					{
						rtf.Fonts = fonts.next;
					}
					else if (font != null)
					{
						font.next = fonts.next;
					}
					else
					{
						rtf.Fonts = fonts.next;
					}
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001D3A RID: 7482 RVA: 0x0008B038 File Offset: 0x00089238
		public static Font GetFont(RTF rtf, int font_number)
		{
			Font font;
			lock (rtf)
			{
				font = Font.GetFont(rtf.Fonts, font_number);
			}
			return font;
		}

		// Token: 0x06001D3B RID: 7483 RVA: 0x0008B07C File Offset: 0x0008927C
		public static Font GetFont(Font start, int font_number)
		{
			if (font_number == -1)
			{
				return start;
			}
			Font font = start;
			while (font != null && font.num != font_number)
			{
				font = font.next;
			}
			return font;
		}

		// Token: 0x0400185B RID: 6235
		private string name;

		// Token: 0x0400185C RID: 6236
		private int num;

		// Token: 0x0400185D RID: 6237
		private int family;

		// Token: 0x0400185E RID: 6238
		private CharsetType charset;

		// Token: 0x0400185F RID: 6239
		private int pitch;

		// Token: 0x04001860 RID: 6240
		private int type;

		// Token: 0x04001861 RID: 6241
		private int codepage;

		// Token: 0x04001862 RID: 6242
		private Font next;

		// Token: 0x04001863 RID: 6243
		private RTF rtf;
	}
}
