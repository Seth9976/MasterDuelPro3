using System;
using System.Drawing;

namespace System.Windows.Forms.Theming.Default
{
	// Token: 0x02000379 RID: 889
	internal class LabelPainter
	{
		// Token: 0x06001D0F RID: 7439 RVA: 0x000899F4 File Offset: 0x00087BF4
		public virtual void Draw(Graphics dc, Rectangle client_rectangle, Label label)
		{
			Rectangle paddingClientRectangle = label.PaddingClientRectangle;
			label.DrawImage(dc, label.Image, paddingClientRectangle, label.ImageAlign);
			if (label.Enabled)
			{
				dc.DrawString(label.Text, label.Font, ThemeEngine.Current.ResPool.GetSolidBrush(label.ForeColor), paddingClientRectangle, label.string_format);
				return;
			}
			ControlPaint.DrawStringDisabled(dc, label.Text, label.Font, label.BackColor, paddingClientRectangle, label.string_format);
		}

		// Token: 0x17000738 RID: 1848
		// (get) Token: 0x06001D10 RID: 7440 RVA: 0x00089A7C File Offset: 0x00087C7C
		public virtual Size DefaultSize
		{
			get
			{
				return new Size(100, 23);
			}
		}
	}
}
