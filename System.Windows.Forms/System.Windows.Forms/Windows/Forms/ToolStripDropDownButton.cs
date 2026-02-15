using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms.Design;

namespace System.Windows.Forms
{
	/// <summary>Represents a control that when clicked displays an associated <see cref="T:System.Windows.Forms.ToolStripDropDown" /> from which the user can select a single item.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001BF RID: 447
	[ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.ToolStrip | ToolStripItemDesignerAvailability.StatusStrip)]
	public class ToolStripDropDownButton : ToolStripDropDownItem
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripDropDownButton" /> class. </summary>
		// Token: 0x0600136C RID: 4972 RVA: 0x00062C6A File Offset: 0x00060E6A
		public ToolStripDropDownButton()
			: this(string.Empty, null, null, string.Empty)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripDropDownButton" /> class that has the specified name, displays the specified text and image, and raises the Click event.</summary>
		/// <param name="text">The text to be displayed on the <see cref="T:System.Windows.Forms.ToolStripDropDownButton" />.</param>
		/// <param name="image">An <see cref="T:System.Drawing.Image" /> to be displayed on the <see cref="T:System.Windows.Forms.ToolStripDropDownButton" />.</param>
		/// <param name="onClick">The event handler for the <see cref="E:System.Windows.Forms.Control.Click" /> event.</param>
		/// <param name="name">The name of the <see cref="T:System.Windows.Forms.ToolStripDropDownButton" />.</param>
		// Token: 0x0600136D RID: 4973 RVA: 0x00062C7E File Offset: 0x00060E7E
		public ToolStripDropDownButton(string text, Image image, EventHandler onClick, string name)
			: base(text, image, onClick, name)
		{
		}

		/// <summary>Gets or sets a value indicating whether an arrow is displayed on the <see cref="T:System.Windows.Forms.ToolStripDropDownButton" />, which indicates that further options are available in a drop-down list.</summary>
		/// <returns>true to show an arrow on the <see cref="T:System.Windows.Forms.ToolStripDropDownButton" />; otherwise, false. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000513 RID: 1299
		// (get) Token: 0x0600136E RID: 4974 RVA: 0x00062C92 File Offset: 0x00060E92
		[DefaultValue(true)]
		public bool ShowDropDownArrow
		{
			get
			{
				return this.show_drop_down_arrow;
			}
		}

		/// <summary>Gets a value indicating whether to display the <see cref="T:System.Windows.Forms.ToolTip" /> that is defined as the default.</summary>
		/// <returns>true in all cases.</returns>
		// Token: 0x17000514 RID: 1300
		// (get) Token: 0x0600136F RID: 4975 RVA: 0x00006F54 File Offset: 0x00005154
		protected override bool DefaultAutoToolTip
		{
			get
			{
				return true;
			}
		}

		/// <summary>Creates a generic <see cref="T:System.Windows.Forms.ToolStripDropDown" /> for which events can be defined.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ToolStripDropDown" /> for which events can be defined.</returns>
		// Token: 0x06001370 RID: 4976 RVA: 0x00062C9A File Offset: 0x00060E9A
		protected override ToolStripDropDown CreateDefaultDropDown()
		{
			return new ToolStripDropDownMenu
			{
				OwnerItem = this
			};
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.MouseDown" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
		// Token: 0x06001371 RID: 4977 RVA: 0x00062CA8 File Offset: 0x00060EA8
		protected override void OnMouseDown(MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				if (base.DropDown.Visible)
				{
					base.HideDropDown(ToolStripDropDownCloseReason.ItemClicked);
				}
				else
				{
					base.ShowDropDown();
				}
			}
			base.OnMouseDown(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.MouseLeave" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06001372 RID: 4978 RVA: 0x00062CDA File Offset: 0x00060EDA
		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.MouseUp" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" />  that contains the event data. </param>
		// Token: 0x06001373 RID: 4979 RVA: 0x00062CE3 File Offset: 0x00060EE3
		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.Paint" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
		// Token: 0x06001374 RID: 4980 RVA: 0x00062CEC File Offset: 0x00060EEC
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			if (base.Owner != null)
			{
				Color color = (this.Enabled ? this.ForeColor : SystemColors.GrayText);
				Image image = (this.Enabled ? this.Image : ToolStripRenderer.CreateDisabledImage(this.Image));
				base.Owner.Renderer.DrawDropDownButtonBackground(new ToolStripItemRenderEventArgs(e.Graphics, this));
				Rectangle rectangle;
				Rectangle rectangle2;
				base.CalculateTextAndImageRectangles(out rectangle, out rectangle2);
				if (rectangle != Rectangle.Empty)
				{
					base.Owner.Renderer.DrawItemText(new ToolStripItemTextRenderEventArgs(e.Graphics, this, this.Text, rectangle, color, this.Font, this.TextAlign));
				}
				if (rectangle2 != Rectangle.Empty)
				{
					base.Owner.Renderer.DrawItemImage(new ToolStripItemImageRenderEventArgs(e.Graphics, this, image, rectangle2));
				}
				if (this.ShowDropDownArrow)
				{
					base.Owner.Renderer.DrawArrow(new ToolStripArrowRenderEventArgs(e.Graphics, this, new Rectangle(base.Width - 10, 0, 6, base.Height), Color.Black, ArrowDirection.Down));
				}
				return;
			}
		}

		/// <summary>Retrieves a value indicating whether the drop-down list of the <see cref="T:System.Windows.Forms.ToolStripDropDownButton" /> has items.</summary>
		/// <returns>true if the drop-down list has items; otherwise, false.</returns>
		/// <param name="charCode">The character to process.</param>
		// Token: 0x06001375 RID: 4981 RVA: 0x00062E0D File Offset: 0x0006100D
		protected internal override bool ProcessMnemonic(char charCode)
		{
			if (!this.Selected)
			{
				base.Parent.ChangeSelection(this);
			}
			if (this.HasDropDownItems)
			{
				base.ShowDropDown();
			}
			else
			{
				base.PerformClick();
			}
			return true;
		}

		// Token: 0x06001376 RID: 4982 RVA: 0x00062E3C File Offset: 0x0006103C
		internal override Size CalculatePreferredSize(Size constrainingSize)
		{
			Size size = base.CalculatePreferredSize(constrainingSize);
			if (this.ShowDropDownArrow)
			{
				size.Width += 9;
			}
			return size;
		}

		// Token: 0x04000BD4 RID: 3028
		private bool show_drop_down_arrow = true;
	}
}
