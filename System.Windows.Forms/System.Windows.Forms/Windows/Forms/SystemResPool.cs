using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace System.Windows.Forms
{
	// Token: 0x020001A6 RID: 422
	internal class SystemResPool
	{
		// Token: 0x06001057 RID: 4183 RVA: 0x00050348 File Offset: 0x0004E548
		public Pen GetPen(Color color)
		{
			int num = color.ToArgb();
			Hashtable hashtable = this.pens;
			Pen pen2;
			lock (hashtable)
			{
				Pen pen = this.pens[num] as Pen;
				if (pen != null)
				{
					pen2 = pen;
				}
				else
				{
					Pen pen3 = new Pen(color);
					this.pens.Add(num, pen3);
					pen2 = pen3;
				}
			}
			return pen2;
		}

		// Token: 0x06001058 RID: 4184 RVA: 0x000503CC File Offset: 0x0004E5CC
		public Pen GetDashPen(Color color, DashStyle dashStyle)
		{
			string text = color.ToString() + dashStyle;
			Hashtable hashtable = this.dashpens;
			Pen pen2;
			lock (hashtable)
			{
				Pen pen = this.dashpens[text] as Pen;
				if (pen != null)
				{
					pen2 = pen;
				}
				else
				{
					Pen pen3 = new Pen(color);
					pen3.DashStyle = dashStyle;
					this.dashpens[text] = pen3;
					pen2 = pen3;
				}
			}
			return pen2;
		}

		// Token: 0x06001059 RID: 4185 RVA: 0x00050460 File Offset: 0x0004E660
		public Pen GetSizedPen(Color color, int size)
		{
			string text = color.ToString() + size;
			Hashtable hashtable = this.sizedpens;
			Pen pen2;
			lock (hashtable)
			{
				Pen pen = this.sizedpens[text] as Pen;
				if (pen != null)
				{
					pen2 = pen;
				}
				else
				{
					Pen pen3 = new Pen(color, (float)size);
					this.sizedpens[text] = pen3;
					pen2 = pen3;
				}
			}
			return pen2;
		}

		// Token: 0x0600105A RID: 4186 RVA: 0x000504EC File Offset: 0x0004E6EC
		public SolidBrush GetSolidBrush(Color color)
		{
			int num = color.ToArgb();
			Hashtable hashtable = this.solidbrushes;
			SolidBrush solidBrush2;
			lock (hashtable)
			{
				SolidBrush solidBrush = this.solidbrushes[num] as SolidBrush;
				if (solidBrush != null)
				{
					solidBrush2 = solidBrush;
				}
				else
				{
					SolidBrush solidBrush3 = new SolidBrush(color);
					this.solidbrushes.Add(num, solidBrush3);
					solidBrush2 = solidBrush3;
				}
			}
			return solidBrush2;
		}

		// Token: 0x0600105B RID: 4187 RVA: 0x00050570 File Offset: 0x0004E770
		public HatchBrush GetHatchBrush(HatchStyle hatchStyle, Color foreColor, Color backColor)
		{
			int num = (int)hatchStyle;
			string text = num.ToString() + foreColor.ToString() + backColor.ToString();
			Hashtable hashtable = this.hatchbrushes;
			HatchBrush hatchBrush2;
			lock (hashtable)
			{
				HatchBrush hatchBrush = (HatchBrush)this.hatchbrushes[text];
				if (hatchBrush == null)
				{
					hatchBrush = new HatchBrush(hatchStyle, foreColor, backColor);
					this.hatchbrushes.Add(text, hatchBrush);
				}
				hatchBrush2 = hatchBrush;
			}
			return hatchBrush2;
		}

		// Token: 0x0600105C RID: 4188 RVA: 0x00050608 File Offset: 0x0004E808
		public void AddUIImage(Image image, string name, int size)
		{
			string text = name + size.ToString();
			Hashtable hashtable = this.uiImages;
			lock (hashtable)
			{
				if (!this.uiImages.Contains(text))
				{
					this.uiImages.Add(text, image);
				}
			}
		}

		// Token: 0x0600105D RID: 4189 RVA: 0x00050670 File Offset: 0x0004E870
		public Image GetUIImage(string name, int size)
		{
			string text = name + size.ToString();
			return this.uiImages[text] as Image;
		}

		// Token: 0x0600105E RID: 4190 RVA: 0x0005069C File Offset: 0x0004E89C
		public CPColor GetCPColor(Color color)
		{
			Hashtable hashtable = this.cpcolors;
			CPColor cpcolor2;
			lock (hashtable)
			{
				object obj = this.cpcolors[color];
				if (obj == null)
				{
					CPColor cpcolor = default(CPColor);
					cpcolor.Dark = ControlPaint.Dark(color);
					cpcolor.DarkDark = ControlPaint.DarkDark(color);
					cpcolor.Light = ControlPaint.Light(color);
					cpcolor.LightLight = ControlPaint.LightLight(color);
					this.cpcolors.Add(color, cpcolor);
					cpcolor2 = cpcolor;
				}
				else
				{
					cpcolor2 = (CPColor)obj;
				}
			}
			return cpcolor2;
		}

		// Token: 0x04000B1F RID: 2847
		private Hashtable pens = new Hashtable();

		// Token: 0x04000B20 RID: 2848
		private Hashtable dashpens = new Hashtable();

		// Token: 0x04000B21 RID: 2849
		private Hashtable sizedpens = new Hashtable();

		// Token: 0x04000B22 RID: 2850
		private Hashtable solidbrushes = new Hashtable();

		// Token: 0x04000B23 RID: 2851
		private Hashtable hatchbrushes = new Hashtable();

		// Token: 0x04000B24 RID: 2852
		private Hashtable uiImages = new Hashtable();

		// Token: 0x04000B25 RID: 2853
		private Hashtable cpcolors = new Hashtable();
	}
}
