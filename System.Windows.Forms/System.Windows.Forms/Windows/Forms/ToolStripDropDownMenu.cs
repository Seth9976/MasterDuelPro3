using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms.Layout;

namespace System.Windows.Forms
{
	/// <summary>Provides basic functionality for the <see cref="T:System.Windows.Forms.ContextMenuStrip" /> control. Although <see cref="T:System.Windows.Forms.ToolStripDropDownMenu" /> and <see cref="T:System.Windows.Forms.ToolStripDropDown" /> replace and add functionality to the <see cref="T:System.Windows.Forms.Menu" /> control of previous versions, <see cref="T:System.Windows.Forms.Menu" /> is retained for both backward compatibility and future use if you choose.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001C7 RID: 455
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[Designer("System.Windows.Forms.Design.ToolStripDropDownDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	public class ToolStripDropDownMenu : ToolStripDropDown
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripDropDownMenu" /> class. </summary>
		// Token: 0x06001396 RID: 5014 RVA: 0x000633D6 File Offset: 0x000615D6
		public ToolStripDropDownMenu()
		{
			this.layout_style = ToolStripLayoutStyle.Flow;
			this.show_image_margin = true;
		}

		/// <summary>Gets the rectangle that represents the display area of the <see cref="T:System.Windows.Forms.ToolStripDropDownMenu" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the display area.</returns>
		// Token: 0x1700051A RID: 1306
		// (get) Token: 0x06001397 RID: 5015 RVA: 0x000633EC File Offset: 0x000615EC
		public override Rectangle DisplayRectangle
		{
			get
			{
				return base.DisplayRectangle;
			}
		}

		/// <filterpriority>1</filterpriority>
		// Token: 0x1700051B RID: 1307
		// (get) Token: 0x06001398 RID: 5016 RVA: 0x000633F4 File Offset: 0x000615F4
		public override LayoutEngine LayoutEngine
		{
			get
			{
				return base.LayoutEngine;
			}
		}

		/// <summary>Gets or sets a value indicating whether space for a check mark is shown on the left edge of the <see cref="T:System.Windows.Forms.ToolStripMenuItem" />. </summary>
		/// <returns>true if the check margin is shown; otherwise, false. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700051C RID: 1308
		// (get) Token: 0x06001399 RID: 5017 RVA: 0x000633FC File Offset: 0x000615FC
		[DefaultValue(false)]
		public bool ShowCheckMargin
		{
			get
			{
				return this.show_check_margin;
			}
		}

		/// <summary>Gets or sets a value indicating whether space for an image is shown on the left edge of the <see cref="T:System.Windows.Forms.ToolStripMenuItem" />.</summary>
		/// <returns>true if the image margin is shown; otherwise, false. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700051D RID: 1309
		// (get) Token: 0x0600139A RID: 5018 RVA: 0x00063404 File Offset: 0x00061604
		[DefaultValue(true)]
		public bool ShowImageMargin
		{
			get
			{
				return this.show_image_margin;
			}
		}

		/// <summary>Gets the internal spacing, in pixels, of the control.</summary>
		/// <returns>A Padding object representing the spacing.</returns>
		// Token: 0x1700051E RID: 1310
		// (get) Token: 0x0600139B RID: 5019 RVA: 0x0006340C File Offset: 0x0006160C
		protected override Padding DefaultPadding
		{
			get
			{
				return base.DefaultPadding;
			}
		}

		/// <summary>Creates a default <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> with the specified text, image, and event handler on a new <see cref="T:System.Windows.Forms.ToolStripDropDownMenu" />.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ToolStripMenuItem" />, or a <see cref="T:System.Windows.Forms.ToolStripSeparator" /> if the <paramref name="text" /> parameter is a hyphen (-).</returns>
		/// <param name="text">The text to use for the <see cref="T:System.Windows.Forms.ToolStripMenuItem" />. If the <paramref name="text" /> parameter is a hyphen (-), this method creates a <see cref="T:System.Windows.Forms.ToolStripSeparator" />.</param>
		/// <param name="image">The <see cref="T:System.Drawing.Image" /> to display on the <see cref="T:System.Windows.Forms.ToolStripMenuItem" />.</param>
		/// <param name="onClick">An event handler that raises the <see cref="E:System.Windows.Forms.ToolStripItem.Click" /> event when the <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> is clicked.</param>
		// Token: 0x0600139C RID: 5020 RVA: 0x00063414 File Offset: 0x00061614
		protected internal override ToolStripItem CreateDefaultItem(string text, Image image, EventHandler onClick)
		{
			return base.CreateDefaultItem(text, image, onClick);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripDropDown.FontChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x0600139D RID: 5021 RVA: 0x0006341F File Offset: 0x0006161F
		protected override void OnFontChanged(EventArgs e)
		{
			base.OnFontChanged(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Layout" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.LayoutEventArgs" /> that contains the event data. </param>
		// Token: 0x0600139E RID: 5022 RVA: 0x00063428 File Offset: 0x00061628
		protected override void OnLayout(LayoutEventArgs e)
		{
			int num = 0;
			foreach (object obj in this.Items)
			{
				ToolStripItem toolStripItem = (ToolStripItem)obj;
				if (toolStripItem.Available)
				{
					toolStripItem.SetPlacement(ToolStripItemPlacement.Main);
					num = Math.Max(num, toolStripItem.GetPreferredSize(Size.Empty).Width);
				}
			}
			int left = base.Padding.Left;
			if (this.show_check_margin || this.show_image_margin)
			{
				num += 68 - base.Padding.Horizontal;
			}
			else
			{
				num += 47 - base.Padding.Horizontal;
			}
			int num2 = base.Padding.Top;
			foreach (object obj2 in this.Items)
			{
				ToolStripItem toolStripItem2 = (ToolStripItem)obj2;
				if (toolStripItem2.Available)
				{
					num2 += toolStripItem2.Margin.Top;
					Size preferredSize = toolStripItem2.GetPreferredSize(Size.Empty);
					int num3;
					if (preferredSize.Height > 22)
					{
						num3 = preferredSize.Height;
					}
					else if (toolStripItem2 is ToolStripSeparator)
					{
						num3 = 7;
					}
					else
					{
						num3 = 22;
					}
					toolStripItem2.SetBounds(new Rectangle(left, num2, num, num3));
					num2 += num3 + toolStripItem2.Margin.Bottom;
				}
			}
			base.Size = new Size(num + base.Padding.Horizontal, num2 + base.Padding.Bottom);
			this.SetDisplayedItems();
			this.OnLayoutCompleted(EventArgs.Empty);
			base.Invalidate();
		}

		/// <summary>Paints the background of the control.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
		// Token: 0x0600139F RID: 5023 RVA: 0x00063614 File Offset: 0x00061814
		protected override void OnPaintBackground(PaintEventArgs e)
		{
			Rectangle rectangle = new Rectangle(Point.Empty, base.Size);
			ToolStripRenderEventArgs toolStripRenderEventArgs = new ToolStripRenderEventArgs(e.Graphics, this, rectangle, SystemColors.Control);
			toolStripRenderEventArgs.InternalConnectedArea = this.CalculateConnectedArea();
			base.Renderer.DrawToolStripBackground(toolStripRenderEventArgs);
			if (this.ShowCheckMargin || this.ShowImageMargin)
			{
				toolStripRenderEventArgs = new ToolStripRenderEventArgs(e.Graphics, this, new Rectangle(toolStripRenderEventArgs.AffectedBounds.Location, new Size(25, toolStripRenderEventArgs.AffectedBounds.Height)), SystemColors.Control);
				base.Renderer.DrawImageMargin(toolStripRenderEventArgs);
			}
		}

		/// <summary>Resets the collection of displayed and overflow items after a layout is done.</summary>
		// Token: 0x060013A0 RID: 5024 RVA: 0x000636B4 File Offset: 0x000618B4
		protected override void SetDisplayedItems()
		{
			base.SetDisplayedItems();
		}

		// Token: 0x060013A1 RID: 5025 RVA: 0x000636BC File Offset: 0x000618BC
		internal override Rectangle CalculateConnectedArea()
		{
			if (base.OwnerItem != null && !base.OwnerItem.IsOnDropDown && !(base.OwnerItem is MdiControlStrip.SystemMenuItem))
			{
				return new Rectangle(base.OwnerItem.GetCurrentParent().PointToScreen(base.OwnerItem.Location).X - base.Left, 0, base.OwnerItem.Width - 1, 2);
			}
			return base.CalculateConnectedArea();
		}

		// Token: 0x04000BEA RID: 3050
		private ToolStripLayoutStyle layout_style;

		// Token: 0x04000BEB RID: 3051
		private bool show_check_margin;

		// Token: 0x04000BEC RID: 3052
		private bool show_image_margin;
	}
}
