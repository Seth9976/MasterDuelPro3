using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace System.Windows.Forms
{
	/// <summary>Handles the painting functionality for <see cref="T:System.Windows.Forms.ToolStrip" /> objects, applying a custom palette and a streamlined style.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001E7 RID: 487
	public class ToolStripProfessionalRenderer : ToolStripRenderer
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripProfessionalRenderer" /> class. </summary>
		// Token: 0x060014B5 RID: 5301 RVA: 0x000672E1 File Offset: 0x000654E1
		public ToolStripProfessionalRenderer()
			: this(new ProfessionalColorTable())
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripProfessionalRenderer" /> class. </summary>
		/// <param name="professionalColorTable">A <see cref="T:System.Windows.Forms.ProfessionalColorTable" /> to be used for painting.</param>
		// Token: 0x060014B6 RID: 5302 RVA: 0x000672EE File Offset: 0x000654EE
		public ToolStripProfessionalRenderer(ProfessionalColorTable professionalColorTable)
		{
			this.color_table = professionalColorTable;
			this.rounded_edges = true;
		}

		/// <summary>Gets the color palette used for painting.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ProfessionalColorTable" /> used for painting.</returns>
		// Token: 0x1700057A RID: 1402
		// (get) Token: 0x060014B7 RID: 5303 RVA: 0x00067304 File Offset: 0x00065504
		public ProfessionalColorTable ColorTable
		{
			get
			{
				return this.color_table;
			}
		}

		/// <summary>Gets or sets a value indicating whether edges of controls have a rounded rather than a square or sharp appearance.</summary>
		/// <returns>true to round off control edges; otherwise, false.</returns>
		// Token: 0x1700057B RID: 1403
		// (get) Token: 0x060014B8 RID: 5304 RVA: 0x0006730C File Offset: 0x0006550C
		public bool RoundedEdges
		{
			get
			{
				return this.rounded_edges;
			}
		}

		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripArrowRenderEventArgs" /> that contains the event data.</param>
		// Token: 0x060014B9 RID: 5305 RVA: 0x00067314 File Offset: 0x00065514
		protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e)
		{
			base.OnRenderArrow(e);
		}

		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripRenderEventArgs" /> that contains the event data. </param>
		// Token: 0x060014BA RID: 5306 RVA: 0x00067320 File Offset: 0x00065520
		protected override void OnRenderButtonBackground(ToolStripItemRenderEventArgs e)
		{
			if (!e.Item.Enabled)
			{
				return;
			}
			Rectangle rectangle = new Rectangle(0, 0, e.Item.Width, e.Item.Height);
			if (e.Item is ToolStripButton && (e.Item as ToolStripButton).Checked && !e.Item.Selected)
			{
				if (this.ColorTable.UseSystemColors)
				{
					e.Graphics.FillRectangle(ThemeEngine.Current.ResPool.GetSolidBrush(this.ColorTable.ButtonCheckedHighlight), rectangle);
					goto IL_022C;
				}
				using (Brush brush = new LinearGradientBrush(rectangle, this.ColorTable.ButtonCheckedGradientBegin, this.ColorTable.ButtonCheckedGradientEnd, LinearGradientMode.Vertical))
				{
					e.Graphics.FillRectangle(brush, rectangle);
					goto IL_022C;
				}
			}
			if (e.Item is ToolStripDropDownItem && e.Item.Pressed)
			{
				using (Brush brush2 = new LinearGradientBrush(rectangle, this.ColorTable.ToolStripGradientBegin, this.ColorTable.ToolStripGradientEnd, LinearGradientMode.Vertical))
				{
					e.Graphics.FillRectangle(brush2, rectangle);
					goto IL_022C;
				}
			}
			if (e.Item.Pressed || (e.Item is ToolStripButton && (e.Item as ToolStripButton).Checked))
			{
				using (Brush brush3 = new LinearGradientBrush(rectangle, this.ColorTable.ButtonPressedGradientBegin, this.ColorTable.ButtonPressedGradientEnd, LinearGradientMode.Vertical))
				{
					e.Graphics.FillRectangle(brush3, rectangle);
					goto IL_022C;
				}
			}
			if (e.Item.Selected)
			{
				using (Brush brush4 = new LinearGradientBrush(rectangle, this.ColorTable.ButtonSelectedGradientBegin, this.ColorTable.ButtonSelectedGradientEnd, LinearGradientMode.Vertical))
				{
					e.Graphics.FillRectangle(brush4, rectangle);
					goto IL_022C;
				}
			}
			if (e.Item.BackColor != Control.DefaultBackColor && e.Item.BackColor != Color.Empty)
			{
				using (Brush brush5 = new SolidBrush(e.Item.BackColor))
				{
					e.Graphics.FillRectangle(brush5, rectangle);
				}
			}
			IL_022C:
			rectangle.Width--;
			rectangle.Height--;
			if (e.Item.Selected && !e.Item.Pressed)
			{
				using (Pen pen = new Pen(this.ColorTable.ButtonSelectedBorder))
				{
					e.Graphics.DrawRectangle(pen, rectangle);
					goto IL_031D;
				}
			}
			if (e.Item.Pressed)
			{
				using (Pen pen2 = new Pen(this.ColorTable.ButtonPressedBorder))
				{
					e.Graphics.DrawRectangle(pen2, rectangle);
					goto IL_031D;
				}
			}
			if (e.Item is ToolStripButton && (e.Item as ToolStripButton).Checked)
			{
				using (Pen pen3 = new Pen(this.ColorTable.ButtonPressedBorder))
				{
					e.Graphics.DrawRectangle(pen3, rectangle);
				}
			}
			IL_031D:
			base.OnRenderButtonBackground(e);
		}

		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripItemRenderEventArgs" /> that contains the event data. </param>
		// Token: 0x060014BB RID: 5307 RVA: 0x000676B8 File Offset: 0x000658B8
		protected override void OnRenderDropDownButtonBackground(ToolStripItemRenderEventArgs e)
		{
			Rectangle rectangle = new Rectangle(0, 0, e.Item.Width, e.Item.Height);
			if (e.Item.Selected && !e.Item.Pressed)
			{
				using (Brush brush = new LinearGradientBrush(rectangle, this.ColorTable.ButtonSelectedGradientBegin, this.ColorTable.ButtonSelectedGradientEnd, LinearGradientMode.Vertical))
				{
					e.Graphics.FillRectangle(brush, rectangle);
					goto IL_00B4;
				}
			}
			if (e.Item.Pressed)
			{
				using (Brush brush2 = new LinearGradientBrush(rectangle, this.ColorTable.ImageMarginGradientMiddle, this.ColorTable.ImageMarginGradientEnd, LinearGradientMode.Vertical))
				{
					e.Graphics.FillRectangle(brush2, rectangle);
				}
			}
			IL_00B4:
			rectangle.Width--;
			rectangle.Height--;
			if (e.Item.Selected && !e.Item.Pressed)
			{
				using (Pen pen = new Pen(this.ColorTable.ButtonSelectedBorder))
				{
					e.Graphics.DrawRectangle(pen, rectangle);
					goto IL_0151;
				}
			}
			if (e.Item.Pressed)
			{
				using (Pen pen2 = new Pen(this.ColorTable.MenuBorder))
				{
					e.Graphics.DrawRectangle(pen2, rectangle);
				}
			}
			IL_0151:
			base.OnRenderDropDownButtonBackground(e);
		}

		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripGripRenderEventArgs" /> that contains the event data. </param>
		// Token: 0x060014BC RID: 5308 RVA: 0x00067854 File Offset: 0x00065A54
		protected override void OnRenderGrip(ToolStripGripRenderEventArgs e)
		{
			if (e.GripStyle == ToolStripGripStyle.Hidden)
			{
				return;
			}
			if (e.GripDisplayStyle == ToolStripGripDisplayStyle.Vertical)
			{
				Rectangle rectangle = new Rectangle(e.GripBounds.Left, e.GripBounds.Top + 5, 2, 2);
				for (int i = 0; i < e.GripBounds.Height - 12; i += 4)
				{
					e.Graphics.FillRectangle(ThemeEngine.Current.ResPool.GetSolidBrush(this.ColorTable.GripLight), rectangle);
					rectangle.Offset(0, 4);
				}
				Rectangle rectangle2 = new Rectangle(e.GripBounds.Left - 1, e.GripBounds.Top + 4, 2, 2);
				for (int j = 0; j < e.GripBounds.Height - 12; j += 4)
				{
					e.Graphics.FillRectangle(ThemeEngine.Current.ResPool.GetSolidBrush(this.ColorTable.GripDark), rectangle2);
					rectangle2.Offset(0, 4);
				}
			}
			else
			{
				Rectangle rectangle3 = new Rectangle(e.GripBounds.Left + 5, e.GripBounds.Top, 2, 2);
				for (int k = 0; k < e.GripBounds.Width - 11; k += 4)
				{
					e.Graphics.FillRectangle(ThemeEngine.Current.ResPool.GetSolidBrush(this.ColorTable.GripLight), rectangle3);
					rectangle3.Offset(4, 0);
				}
				Rectangle rectangle4 = new Rectangle(e.GripBounds.Left + 4, e.GripBounds.Top - 1, 2, 2);
				for (int l = 0; l < e.GripBounds.Width - 11; l += 4)
				{
					e.Graphics.FillRectangle(ThemeEngine.Current.ResPool.GetSolidBrush(this.ColorTable.GripDark), rectangle4);
					rectangle4.Offset(4, 0);
				}
			}
			base.OnRenderGrip(e);
		}

		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripRenderEventArgs" /> that contains the event data. </param>
		// Token: 0x060014BD RID: 5309 RVA: 0x00067A5C File Offset: 0x00065C5C
		protected override void OnRenderImageMargin(ToolStripRenderEventArgs e)
		{
			if (!(e.ToolStrip is ToolStripOverflow))
			{
				Rectangle rectangle = new Rectangle(1, 2, 24, e.ToolStrip.Height - 3);
				using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(rectangle, this.ColorTable.ToolStripGradientBegin, this.ColorTable.ToolStripGradientEnd, LinearGradientMode.Horizontal))
				{
					e.Graphics.FillRectangle(linearGradientBrush, rectangle);
				}
			}
			base.OnRenderImageMargin(e);
		}

		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripItemImageRenderEventArgs" /> that contains the event data.</param>
		// Token: 0x060014BE RID: 5310 RVA: 0x00067ADC File Offset: 0x00065CDC
		protected override void OnRenderItemCheck(ToolStripItemImageRenderEventArgs e)
		{
			if (e.Item.Selected)
			{
				e.Graphics.FillRectangle(ThemeEngine.Current.ResPool.GetSolidBrush(this.ColorTable.CheckPressedBackground), e.ImageRectangle);
				e.Graphics.DrawRectangle(ThemeEngine.Current.ResPool.GetPen(this.ColorTable.ButtonPressedBorder), e.ImageRectangle);
			}
			else if (e.Item.Pressed)
			{
				e.Graphics.FillRectangle(ThemeEngine.Current.ResPool.GetSolidBrush(this.ColorTable.CheckSelectedBackground), e.ImageRectangle);
				e.Graphics.DrawRectangle(ThemeEngine.Current.ResPool.GetPen(this.ColorTable.ButtonSelectedBorder), e.ImageRectangle);
			}
			else
			{
				e.Graphics.FillRectangle(ThemeEngine.Current.ResPool.GetSolidBrush(this.ColorTable.CheckSelectedBackground), e.ImageRectangle);
				e.Graphics.DrawRectangle(ThemeEngine.Current.ResPool.GetPen(this.ColorTable.ButtonSelectedBorder), e.ImageRectangle);
			}
			if (e.Item.Image == null)
			{
				ControlPaint.DrawMenuGlyph(e.Graphics, new Rectangle(6, 5, 7, 6), MenuGlyph.Checkmark);
			}
			base.OnRenderItemCheck(e);
		}

		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripItemImageRenderEventArgs" /> that contains the event data. </param>
		// Token: 0x060014BF RID: 5311 RVA: 0x00067C35 File Offset: 0x00065E35
		protected override void OnRenderItemImage(ToolStripItemImageRenderEventArgs e)
		{
			base.OnRenderItemImage(e);
		}

		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripItemTextRenderEventArgs" /> that contains the event data. </param>
		// Token: 0x060014C0 RID: 5312 RVA: 0x00067C3E File Offset: 0x00065E3E
		protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
		{
			base.OnRenderItemText(e);
		}

		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripItemRenderEventArgs" /> that contains the event data.</param>
		// Token: 0x060014C1 RID: 5313 RVA: 0x00067C48 File Offset: 0x00065E48
		protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
		{
			ToolStripMenuItem toolStripMenuItem = (ToolStripMenuItem)e.Item;
			Rectangle rectangle;
			if (toolStripMenuItem.IsOnDropDown)
			{
				rectangle = new Rectangle(1, 0, e.Item.Bounds.Width - 3, e.Item.Bounds.Height - 1);
				if ((e.Item.Selected || e.Item.Pressed) && e.Item.Enabled)
				{
					e.Graphics.FillRectangle(ThemeEngine.Current.ResPool.GetSolidBrush(this.ColorTable.MenuItemSelectedGradientEnd), rectangle);
				}
				if (!toolStripMenuItem.Selected && !toolStripMenuItem.Pressed)
				{
					goto IL_0284;
				}
				using (Pen pen = new Pen(this.ColorTable.MenuItemBorder))
				{
					e.Graphics.DrawRectangle(pen, rectangle);
					goto IL_0284;
				}
			}
			rectangle = new Rectangle(0, 0, e.Item.Width, e.Item.Height);
			if (e.Item.Pressed)
			{
				using (Brush brush = new LinearGradientBrush(rectangle, this.ColorTable.ToolStripGradientBegin, this.ColorTable.ToolStripGradientEnd, LinearGradientMode.Vertical))
				{
					e.Graphics.FillRectangle(brush, rectangle);
					goto IL_01E5;
				}
			}
			if (e.Item.Selected)
			{
				using (Brush brush2 = new LinearGradientBrush(rectangle, this.ColorTable.ButtonSelectedGradientBegin, this.ColorTable.ButtonSelectedGradientEnd, LinearGradientMode.Vertical))
				{
					e.Graphics.FillRectangle(brush2, rectangle);
					goto IL_01E5;
				}
			}
			if (e.Item.BackColor != Control.DefaultBackColor && e.Item.BackColor != Color.Empty)
			{
				using (Brush brush3 = new SolidBrush(e.Item.BackColor))
				{
					e.Graphics.FillRectangle(brush3, rectangle);
				}
			}
			IL_01E5:
			rectangle.Width--;
			rectangle.Height--;
			if (toolStripMenuItem.Selected || toolStripMenuItem.Pressed)
			{
				if (toolStripMenuItem.HasDropDownItems && toolStripMenuItem.DropDown.Visible)
				{
					using (Pen pen2 = new Pen(this.ColorTable.MenuBorder))
					{
						e.Graphics.DrawRectangle(pen2, rectangle);
						goto IL_0284;
					}
				}
				using (Pen pen3 = new Pen(this.ColorTable.MenuItemBorder))
				{
					e.Graphics.DrawRectangle(pen3, rectangle);
				}
			}
			IL_0284:
			base.OnRenderMenuItemBackground(e);
		}

		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripItemRenderEventArgs" /> that contains the event data. </param>
		// Token: 0x060014C2 RID: 5314 RVA: 0x00067F2C File Offset: 0x0006612C
		protected override void OnRenderOverflowButtonBackground(ToolStripItemRenderEventArgs e)
		{
			LinearGradientMode linearGradientMode = ((e.ToolStrip.Orientation == Orientation.Vertical) ? LinearGradientMode.Horizontal : LinearGradientMode.Vertical);
			Rectangle rectangle;
			if (e.ToolStrip.Orientation == Orientation.Horizontal)
			{
				rectangle = new Rectangle(e.Item.Width - 11, 0, 11, e.Item.Height - 1);
			}
			else
			{
				rectangle = new Rectangle(0, e.Item.Height - 11, e.Item.Width - 1, 11);
			}
			if (e.Item.Selected && !e.Item.Pressed)
			{
				using (Brush brush = new LinearGradientBrush(rectangle, this.ColorTable.ButtonSelectedGradientBegin, this.ColorTable.ButtonSelectedGradientEnd, linearGradientMode))
				{
					e.Graphics.FillRectangle(brush, rectangle);
					goto IL_013F;
				}
			}
			if (e.Item.Pressed)
			{
				using (Brush brush2 = new LinearGradientBrush(rectangle, this.ColorTable.ButtonPressedGradientBegin, this.ColorTable.ButtonPressedGradientEnd, linearGradientMode))
				{
					e.Graphics.FillRectangle(brush2, rectangle);
					goto IL_013F;
				}
			}
			using (Brush brush3 = new LinearGradientBrush(rectangle, this.ColorTable.OverflowButtonGradientBegin, this.ColorTable.OverflowButtonGradientEnd, linearGradientMode))
			{
				e.Graphics.FillRectangle(brush3, rectangle);
			}
			IL_013F:
			ToolStripProfessionalRenderer.PaintOverflowArrow(e, rectangle);
			base.OnRenderOverflowButtonBackground(e);
		}

		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripSeparatorRenderEventArgs" /> that contains the event data. </param>
		// Token: 0x060014C3 RID: 5315 RVA: 0x000680B0 File Offset: 0x000662B0
		protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
		{
			if (e.Vertical)
			{
				Rectangle rectangle = new Rectangle(4, 6, 1, e.Item.Height - 10);
				e.Graphics.FillRectangle(ThemeEngine.Current.ResPool.GetSolidBrush(this.ColorTable.SeparatorLight), rectangle);
				Rectangle rectangle2 = new Rectangle(3, 5, 1, e.Item.Height - 10);
				e.Graphics.FillRectangle(ThemeEngine.Current.ResPool.GetSolidBrush(this.ColorTable.SeparatorDark), rectangle2);
			}
			else
			{
				if (!e.Item.IsOnDropDown)
				{
					Rectangle rectangle3 = new Rectangle(6, 4, e.Item.Width - 10, 1);
					e.Graphics.FillRectangle(ThemeEngine.Current.ResPool.GetSolidBrush(this.ColorTable.SeparatorLight), rectangle3);
				}
				Rectangle rectangle4;
				if (e.Item.IsOnDropDown)
				{
					if (e.Item.UseImageMargin)
					{
						rectangle4 = new Rectangle(35, 3, e.Item.Width - 36, 1);
					}
					else
					{
						rectangle4 = new Rectangle(7, 3, e.Item.Width - 7, 1);
					}
				}
				else
				{
					rectangle4 = new Rectangle(5, 3, e.Item.Width - 10, 1);
				}
				e.Graphics.FillRectangle(ThemeEngine.Current.ResPool.GetSolidBrush(this.ColorTable.SeparatorDark), rectangle4);
			}
			base.OnRenderSeparator(e);
		}

		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripRenderEventArgs" /> that contains the event data. </param>
		// Token: 0x060014C4 RID: 5316 RVA: 0x00068228 File Offset: 0x00066428
		protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
		{
			if (e.ToolStrip.BackgroundImage != null)
			{
				if (e.ToolStrip is StatusStrip)
				{
					e.Graphics.DrawLine(Pens.White, e.AffectedBounds.Left, e.AffectedBounds.Top, e.AffectedBounds.Right, e.AffectedBounds.Top);
				}
				return;
			}
			if (e.ToolStrip is ToolStripDropDown)
			{
				e.Graphics.Clear(this.ColorTable.ToolStripDropDownBackground);
				return;
			}
			if (e.ToolStrip is MenuStrip || e.ToolStrip is StatusStrip)
			{
				using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(e.AffectedBounds, this.ColorTable.MenuStripGradientBegin, this.ColorTable.MenuStripGradientEnd, (e.ToolStrip.Orientation == Orientation.Horizontal) ? LinearGradientMode.Horizontal : LinearGradientMode.Vertical))
				{
					e.Graphics.FillRectangle(linearGradientBrush, e.AffectedBounds);
					goto IL_0144;
				}
			}
			using (LinearGradientBrush linearGradientBrush2 = new LinearGradientBrush(e.AffectedBounds, this.ColorTable.ToolStripGradientBegin, this.ColorTable.ToolStripGradientEnd, (e.ToolStrip.Orientation == Orientation.Vertical) ? LinearGradientMode.Horizontal : LinearGradientMode.Vertical))
			{
				e.Graphics.FillRectangle(linearGradientBrush2, e.AffectedBounds);
			}
			IL_0144:
			if (e.ToolStrip is StatusStrip)
			{
				e.Graphics.DrawLine(Pens.White, e.AffectedBounds.Left, e.AffectedBounds.Top, e.AffectedBounds.Right, e.AffectedBounds.Top);
			}
			base.OnRenderToolStripBackground(e);
		}

		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripRenderEventArgs" /> that contains the event data.</param>
		// Token: 0x060014C5 RID: 5317 RVA: 0x000683F4 File Offset: 0x000665F4
		protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
		{
			if (e.ToolStrip is ToolStripDropDown)
			{
				if (e.ToolStrip is ToolStripOverflow)
				{
					e.Graphics.DrawLines(ThemeEngine.Current.ResPool.GetPen(this.ColorTable.MenuBorder), new Point[]
					{
						e.AffectedBounds.Location,
						new Point(e.AffectedBounds.Left, e.AffectedBounds.Bottom - 1),
						new Point(e.AffectedBounds.Right - 1, e.AffectedBounds.Bottom - 1),
						new Point(e.AffectedBounds.Right - 1, e.AffectedBounds.Top),
						new Point(e.AffectedBounds.Left, e.AffectedBounds.Top)
					});
					return;
				}
				e.Graphics.DrawLines(ThemeEngine.Current.ResPool.GetPen(this.ColorTable.MenuBorder), new Point[]
				{
					new Point(e.AffectedBounds.Left + e.ConnectedArea.Left, e.AffectedBounds.Top),
					e.AffectedBounds.Location,
					new Point(e.AffectedBounds.Left, e.AffectedBounds.Bottom - 1),
					new Point(e.AffectedBounds.Right - 1, e.AffectedBounds.Bottom - 1),
					new Point(e.AffectedBounds.Right - 1, e.AffectedBounds.Top),
					new Point(e.AffectedBounds.Left + e.ConnectedArea.Right, e.AffectedBounds.Top)
				});
				return;
			}
			else
			{
				if (e.ToolStrip is MenuStrip || e.ToolStrip is StatusStrip)
				{
					return;
				}
				using (Pen pen = new Pen(this.ColorTable.ToolStripBorder))
				{
					if (this.RoundedEdges)
					{
						e.Graphics.DrawLine(pen, new Point(2, e.ToolStrip.Height - 1), new Point(e.ToolStrip.Width - 3, e.ToolStrip.Height - 1));
						e.Graphics.DrawLine(pen, new Point(e.ToolStrip.Width - 2, e.ToolStrip.Height - 2), new Point(e.ToolStrip.Width - 1, e.ToolStrip.Height - 2));
						e.Graphics.DrawLine(pen, new Point(e.ToolStrip.Width - 1, 2), new Point(e.ToolStrip.Width - 1, e.ToolStrip.Height - 3));
					}
					else
					{
						e.Graphics.DrawLine(pen, new Point(e.ToolStrip.Left, e.ToolStrip.Bottom - 1), new Point(e.ToolStrip.Width, e.ToolStrip.Bottom - 1));
					}
				}
				base.OnRenderToolStripBorder(e);
				return;
			}
		}

		// Token: 0x060014C6 RID: 5318 RVA: 0x000687B0 File Offset: 0x000669B0
		private static void PaintOverflowArrow(ToolStripItemRenderEventArgs e, Rectangle paint_here)
		{
			if (e.ToolStrip.Orientation == Orientation.Horizontal)
			{
				Point point = new Point(paint_here.X + 2, paint_here.Bottom - 9);
				e.Graphics.DrawLine(Pens.White, point.X + 1, point.Y + 1, point.X + 5, point.Y + 1);
				e.Graphics.DrawLine(Pens.Black, point.X, point.Y, point.X + 4, point.Y);
				e.Graphics.DrawLine(Pens.White, point.X + 3, point.Y + 4, point.X + 5, point.Y + 4);
				e.Graphics.DrawLine(Pens.White, point.X + 3, point.Y + 5, point.X + 4, point.Y + 5);
				e.Graphics.DrawLine(Pens.White, point.X + 3, point.Y + 4, point.X + 3, point.Y + 6);
				e.Graphics.DrawLine(Pens.Black, point.X, point.Y + 3, point.X + 4, point.Y + 3);
				e.Graphics.DrawLine(Pens.Black, point.X + 1, point.Y + 4, point.X + 3, point.Y + 4);
				e.Graphics.DrawLine(Pens.Black, point.X + 2, point.Y + 4, point.X + 2, point.Y + 5);
				return;
			}
			Point point2 = new Point(paint_here.Right - 9, paint_here.Y + 2);
			e.Graphics.DrawLine(Pens.White, point2.X + 1, point2.Y + 1, point2.X + 1, point2.Y + 5);
			e.Graphics.DrawLine(Pens.Black, point2.X, point2.Y, point2.X, point2.Y + 4);
			e.Graphics.DrawLine(Pens.White, point2.X + 4, point2.Y + 3, point2.X + 4, point2.Y + 5);
			e.Graphics.DrawLine(Pens.White, point2.X + 5, point2.Y + 3, point2.X + 5, point2.Y + 4);
			e.Graphics.DrawLine(Pens.White, point2.X + 4, point2.Y + 3, point2.X + 6, point2.Y + 3);
			e.Graphics.DrawLine(Pens.Black, point2.X + 3, point2.Y, point2.X + 3, point2.Y + 4);
			e.Graphics.DrawLine(Pens.Black, point2.X + 4, point2.Y + 1, point2.X + 4, point2.Y + 3);
			e.Graphics.DrawLine(Pens.Black, point2.X + 4, point2.Y + 2, point2.X + 5, point2.Y + 2);
		}

		// Token: 0x04000C88 RID: 3208
		private ProfessionalColorTable color_table;

		// Token: 0x04000C89 RID: 3209
		private bool rounded_edges;
	}
}
