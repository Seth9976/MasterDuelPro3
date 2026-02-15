using System;

namespace System.Windows.Forms.RTF
{
	// Token: 0x02000380 RID: 896
	internal class Color
	{
		// Token: 0x06001D20 RID: 7456 RVA: 0x0008AD50 File Offset: 0x00088F50
		public Color(RTF rtf)
		{
			this.red = -1;
			this.green = -1;
			this.blue = -1;
			this.num = -1;
			lock (rtf)
			{
				if (rtf.Colors == null)
				{
					rtf.Colors = this;
				}
				else
				{
					Color colors = rtf.Colors;
					while (colors.next != null)
					{
						colors = colors.next;
					}
					colors.next = this;
				}
			}
		}

		// Token: 0x17000740 RID: 1856
		// (get) Token: 0x06001D21 RID: 7457 RVA: 0x0008ADD8 File Offset: 0x00088FD8
		// (set) Token: 0x06001D22 RID: 7458 RVA: 0x0008ADE0 File Offset: 0x00088FE0
		public int Red
		{
			get
			{
				return this.red;
			}
			set
			{
				this.red = value;
			}
		}

		// Token: 0x17000741 RID: 1857
		// (get) Token: 0x06001D23 RID: 7459 RVA: 0x0008ADE9 File Offset: 0x00088FE9
		// (set) Token: 0x06001D24 RID: 7460 RVA: 0x0008ADF1 File Offset: 0x00088FF1
		public int Green
		{
			get
			{
				return this.green;
			}
			set
			{
				this.green = value;
			}
		}

		// Token: 0x17000742 RID: 1858
		// (get) Token: 0x06001D25 RID: 7461 RVA: 0x0008ADFA File Offset: 0x00088FFA
		// (set) Token: 0x06001D26 RID: 7462 RVA: 0x0008AE02 File Offset: 0x00089002
		public int Blue
		{
			get
			{
				return this.blue;
			}
			set
			{
				this.blue = value;
			}
		}

		// Token: 0x17000743 RID: 1859
		// (set) Token: 0x06001D27 RID: 7463 RVA: 0x0008AE0B File Offset: 0x0008900B
		public int Num
		{
			set
			{
				this.num = value;
			}
		}

		// Token: 0x06001D28 RID: 7464 RVA: 0x0008AE14 File Offset: 0x00089014
		public static Color GetColor(RTF rtf, int color_number)
		{
			Color color;
			lock (rtf)
			{
				color = Color.GetColor(rtf.Colors, color_number);
			}
			return color;
		}

		// Token: 0x06001D29 RID: 7465 RVA: 0x0008AE58 File Offset: 0x00089058
		private static Color GetColor(Color start, int color_number)
		{
			if (color_number == -1)
			{
				return start;
			}
			Color color = start;
			while (color != null && color.num != color_number)
			{
				color = color.next;
			}
			return color;
		}

		// Token: 0x04001855 RID: 6229
		private int red;

		// Token: 0x04001856 RID: 6230
		private int green;

		// Token: 0x04001857 RID: 6231
		private int blue;

		// Token: 0x04001858 RID: 6232
		private int num;

		// Token: 0x04001859 RID: 6233
		private Color next;
	}
}
