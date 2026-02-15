using System;
using System.Drawing;

namespace System.Windows.Forms
{
	// Token: 0x02000129 RID: 297
	internal class MdiControlStrip
	{
		// Token: 0x0200012A RID: 298
		public class SystemMenuItem : ToolStripMenuItem
		{
			// Token: 0x06000BA6 RID: 2982 RVA: 0x0003267C File Offset: 0x0003087C
			public SystemMenuItem(Form ownerForm)
			{
				this.form = ownerForm;
				base.AutoSize = false;
				base.Size = new Size(20, 20);
				base.Image = ownerForm.Icon.ToBitmap();
				base.MergeIndex = int.MinValue;
				base.DisplayStyle = ToolStripItemDisplayStyle.Image;
				base.DropDownItems.Add("&Restore", null, new EventHandler(this.RestoreItemHandler));
				((ToolStripMenuItem)base.DropDownItems.Add("&Move")).Enabled = false;
				((ToolStripMenuItem)base.DropDownItems.Add("&Size")).Enabled = false;
				base.DropDownItems.Add("Mi&nimize", null, new EventHandler(this.MinimizeItemHandler));
				((ToolStripMenuItem)base.DropDownItems.Add("Ma&ximize")).Enabled = false;
				base.DropDownItems.Add("-");
				((ToolStripMenuItem)base.DropDownItems.Add("&Close", null, new EventHandler(this.CloseItemHandler))).ShortcutKeys = (Keys)131187;
				base.DropDownItems.Add("-");
				((ToolStripMenuItem)base.DropDownItems.Add("Nex&t", null, new EventHandler(this.NextItemHandler))).ShortcutKeys = (Keys)131189;
			}

			// Token: 0x06000BA7 RID: 2983 RVA: 0x000327D8 File Offset: 0x000309D8
			protected override void OnPaint(PaintEventArgs e)
			{
				if (base.Owner == null)
				{
					return;
				}
				Image image = this.Image;
				Rectangle rectangle;
				Rectangle rectangle2;
				base.CalculateTextAndImageRectangles(out rectangle, out rectangle2);
				if (rectangle2 != Rectangle.Empty)
				{
					base.Owner.Renderer.DrawItemImage(new ToolStripItemImageRenderEventArgs(e.Graphics, this, image, rectangle2));
				}
			}

			// Token: 0x170002FB RID: 763
			// (get) Token: 0x06000BA8 RID: 2984 RVA: 0x0003282A File Offset: 0x00030A2A
			// (set) Token: 0x06000BA9 RID: 2985 RVA: 0x00032832 File Offset: 0x00030A32
			public Form MdiForm
			{
				get
				{
					return this.form;
				}
				set
				{
					this.form = value;
				}
			}

			// Token: 0x06000BAA RID: 2986 RVA: 0x0003283B File Offset: 0x00030A3B
			private void RestoreItemHandler(object sender, EventArgs e)
			{
				this.form.WindowState = FormWindowState.Normal;
			}

			// Token: 0x06000BAB RID: 2987 RVA: 0x00032849 File Offset: 0x00030A49
			private void MinimizeItemHandler(object sender, EventArgs e)
			{
				this.form.WindowState = FormWindowState.Minimized;
			}

			// Token: 0x06000BAC RID: 2988 RVA: 0x00032857 File Offset: 0x00030A57
			private void CloseItemHandler(object sender, EventArgs e)
			{
				this.form.Close();
			}

			// Token: 0x06000BAD RID: 2989 RVA: 0x00032864 File Offset: 0x00030A64
			private void NextItemHandler(object sender, EventArgs e)
			{
				this.form.MdiParent.MdiContainer.ActivateNextChild();
			}

			// Token: 0x04000789 RID: 1929
			private Form form;
		}

		// Token: 0x0200012B RID: 299
		public class ControlBoxMenuItem : ToolStripMenuItem
		{
			// Token: 0x06000BAE RID: 2990 RVA: 0x0003287C File Offset: 0x00030A7C
			public ControlBoxMenuItem(Form ownerForm, MdiControlStrip.ControlBoxType type)
			{
				this.form = ownerForm;
				this.type = type;
				base.AutoSize = false;
				base.Alignment = ToolStripItemAlignment.Right;
				base.Size = new Size(20, 20);
				base.MergeIndex = int.MaxValue;
				base.DisplayStyle = ToolStripItemDisplayStyle.None;
				switch (type)
				{
				case MdiControlStrip.ControlBoxType.Close:
					base.Click += this.CloseItemHandler;
					return;
				case MdiControlStrip.ControlBoxType.Min:
					base.Click += this.MinimizeItemHandler;
					return;
				case MdiControlStrip.ControlBoxType.Max:
					base.Click += this.RestoreItemHandler;
					return;
				default:
					return;
				}
			}

			// Token: 0x06000BAF RID: 2991 RVA: 0x00032918 File Offset: 0x00030B18
			protected override void OnPaint(PaintEventArgs e)
			{
				base.OnPaint(e);
				Graphics graphics = e.Graphics;
				switch (this.type)
				{
				case MdiControlStrip.ControlBoxType.Close:
					graphics.FillRectangle(Brushes.Black, 8, 8, 4, 4);
					graphics.FillRectangle(Brushes.Black, 6, 6, 2, 2);
					graphics.FillRectangle(Brushes.Black, 6, 12, 2, 2);
					graphics.FillRectangle(Brushes.Black, 12, 6, 2, 2);
					graphics.FillRectangle(Brushes.Black, 12, 12, 2, 2);
					graphics.DrawLine(Pens.Black, 8, 7, 8, 12);
					graphics.DrawLine(Pens.Black, 7, 8, 12, 8);
					graphics.DrawLine(Pens.Black, 11, 7, 11, 12);
					graphics.DrawLine(Pens.Black, 7, 11, 12, 11);
					return;
				case MdiControlStrip.ControlBoxType.Min:
					graphics.DrawLine(Pens.Black, 6, 12, 11, 12);
					graphics.DrawLine(Pens.Black, 6, 13, 11, 13);
					return;
				case MdiControlStrip.ControlBoxType.Max:
					graphics.DrawLines(Pens.Black, new Point[]
					{
						new Point(7, 8),
						new Point(7, 5),
						new Point(13, 5),
						new Point(13, 10),
						new Point(11, 10)
					});
					graphics.DrawLine(Pens.Black, 7, 6, 12, 6);
					graphics.DrawRectangle(Pens.Black, new Rectangle(5, 8, 6, 5));
					graphics.DrawLine(Pens.Black, 5, 9, 11, 9);
					return;
				default:
					return;
				}
			}

			// Token: 0x170002FC RID: 764
			// (get) Token: 0x06000BB0 RID: 2992 RVA: 0x00032A98 File Offset: 0x00030C98
			// (set) Token: 0x06000BB1 RID: 2993 RVA: 0x00032AA0 File Offset: 0x00030CA0
			public Form MdiForm
			{
				get
				{
					return this.form;
				}
				set
				{
					this.form = value;
				}
			}

			// Token: 0x06000BB2 RID: 2994 RVA: 0x00032AA9 File Offset: 0x00030CA9
			private void RestoreItemHandler(object sender, EventArgs e)
			{
				this.form.WindowState = FormWindowState.Normal;
			}

			// Token: 0x06000BB3 RID: 2995 RVA: 0x00032AB7 File Offset: 0x00030CB7
			private void MinimizeItemHandler(object sender, EventArgs e)
			{
				this.form.WindowState = FormWindowState.Minimized;
			}

			// Token: 0x06000BB4 RID: 2996 RVA: 0x00032AC5 File Offset: 0x00030CC5
			private void CloseItemHandler(object sender, EventArgs e)
			{
				this.form.Close();
			}

			// Token: 0x0400078A RID: 1930
			private Form form;

			// Token: 0x0400078B RID: 1931
			private MdiControlStrip.ControlBoxType type;
		}

		// Token: 0x0200012C RID: 300
		public enum ControlBoxType
		{
			// Token: 0x0400078D RID: 1933
			Close,
			// Token: 0x0400078E RID: 1934
			Min,
			// Token: 0x0400078F RID: 1935
			Max
		}
	}
}
