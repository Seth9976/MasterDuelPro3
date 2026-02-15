using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Drawing;
using System.Windows.Forms.Design;

namespace System.Windows.Forms
{
	/// <summary>Represents a selectable option displayed on a <see cref="T:System.Windows.Forms.MenuStrip" /> or <see cref="T:System.Windows.Forms.ContextMenuStrip" />. Although <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> replaces and adds functionality to the <see cref="T:System.Windows.Forms.MenuItem" /> control of previous versions, <see cref="T:System.Windows.Forms.MenuItem" /> is retained for both backward compatibility and future use if you choose.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001E3 RID: 483
	[ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.MenuStrip | ToolStripItemDesignerAvailability.ContextMenuStrip)]
	[DesignerSerializer("System.Windows.Forms.Design.ToolStripMenuItemCodeDomSerializer, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.Serialization.CodeDomSerializer, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public class ToolStripMenuItem : ToolStripDropDownItem
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> class.</summary>
		// Token: 0x0600147E RID: 5246 RVA: 0x0006675F File Offset: 0x0006495F
		public ToolStripMenuItem()
			: this(null, null, null, string.Empty)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> class that displays the specified text and image and that does the specified action when the <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> is clicked.</summary>
		/// <param name="text">The text to display on the menu item.</param>
		/// <param name="image">The <see cref="T:System.Drawing.Image" /> to display on the control.</param>
		/// <param name="onClick">An event handler that raises the <see cref="E:System.Windows.Forms.Control.Click" /> event when the control is clicked.</param>
		// Token: 0x0600147F RID: 5247 RVA: 0x0006676F File Offset: 0x0006496F
		public ToolStripMenuItem(string text, Image image, EventHandler onClick)
			: this(text, image, onClick, string.Empty)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> class with the specified name that displays the specified text and image that does the specified action when the <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> is clicked.</summary>
		/// <param name="text">The text to display on the menu item.</param>
		/// <param name="image">The <see cref="T:System.Drawing.Image" /> to display on the control.</param>
		/// <param name="onClick">An event handler that raises the <see cref="E:System.Windows.Forms.Control.Click" /> event when the control is clicked.</param>
		/// <param name="name">The name of the menu item.</param>
		// Token: 0x06001480 RID: 5248 RVA: 0x0006677F File Offset: 0x0006497F
		public ToolStripMenuItem(string text, Image image, EventHandler onClick, string name)
			: base(text, image, onClick, name)
		{
			base.Overflow = ToolStripItemOverflow.Never;
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> is checked.</summary>
		/// <returns>true if the <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> is checked or is in an indeterminate state; otherwise, false. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700056B RID: 1387
		// (get) Token: 0x06001481 RID: 5249 RVA: 0x0006679C File Offset: 0x0006499C
		// (set) Token: 0x06001482 RID: 5250 RVA: 0x000667BC File Offset: 0x000649BC
		[Bindable(true)]
		[DefaultValue(false)]
		[RefreshProperties(RefreshProperties.All)]
		public bool Checked
		{
			get
			{
				CheckState checkState = this.checked_state;
				return checkState != CheckState.Unchecked && checkState - CheckState.Checked <= 1;
			}
			set
			{
				this.CheckState = (value ? CheckState.Checked : CheckState.Unchecked);
			}
		}

		/// <summary>Gets or sets a value indicating whether a <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> is in the checked, unchecked, or indeterminate state.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.CheckState" /> values. The default is Unchecked.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The <see cref="P:System.Windows.Forms.ToolStripMenuItem.CheckState" /> property is not set to one of the <see cref="T:System.Windows.Forms.CheckState" /> values. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700056C RID: 1388
		// (set) Token: 0x06001483 RID: 5251 RVA: 0x000667CC File Offset: 0x000649CC
		[Bindable(true)]
		[DefaultValue(CheckState.Unchecked)]
		[RefreshProperties(RefreshProperties.All)]
		public CheckState CheckState
		{
			set
			{
				if (!Enum.IsDefined(typeof(CheckState), value))
				{
					throw new InvalidEnumArgumentException(string.Format("Enum argument value '{0}' is not valid for CheckState", value));
				}
				if (value == this.checked_state)
				{
					return;
				}
				this.checked_state = value;
				base.Invalidate();
				this.OnCheckedChanged(EventArgs.Empty);
				this.OnCheckStateChanged(EventArgs.Empty);
			}
		}

		/// <summary>Gets or sets a value indicating whether the control is enabled. </summary>
		/// <returns>true if the control is enabled; otherwise, false. The default is true.</returns>
		// Token: 0x1700056D RID: 1389
		// (get) Token: 0x06001484 RID: 5252 RVA: 0x00066833 File Offset: 0x00064A33
		// (set) Token: 0x06001485 RID: 5253 RVA: 0x0006683B File Offset: 0x00064A3B
		public override bool Enabled
		{
			get
			{
				return base.Enabled;
			}
			set
			{
				base.Enabled = value;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> appears on a multiple document interface (MDI) window list.</summary>
		/// <returns>true if the <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> appears on a MDI window list; otherwise, false.</returns>
		// Token: 0x1700056E RID: 1390
		// (get) Token: 0x06001486 RID: 5254 RVA: 0x00066844 File Offset: 0x00064A44
		[Browsable(false)]
		public bool IsMdiWindowListEntry
		{
			get
			{
				return this.mdi_client_form != null;
			}
		}

		/// <summary>Gets or sets the shortcut keys associated with the <see cref="T:System.Windows.Forms.ToolStripMenuItem" />.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.Keys" /> values. The default is <see cref="F:System.Windows.Forms.Keys.None" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The property was not set to one of the <see cref="T:System.Windows.Forms.Keys" /> values.</exception>
		// Token: 0x1700056F RID: 1391
		// (get) Token: 0x06001487 RID: 5255 RVA: 0x0006684F File Offset: 0x00064A4F
		// (set) Token: 0x06001488 RID: 5256 RVA: 0x00066857 File Offset: 0x00064A57
		[Localizable(true)]
		[DefaultValue(Keys.None)]
		public Keys ShortcutKeys
		{
			get
			{
				return this.shortcut_keys;
			}
			set
			{
				if (this.shortcut_keys != value)
				{
					this.shortcut_keys = value;
					if (base.Parent != null)
					{
						ToolStripManager.AddToolStripMenuItem(this);
					}
				}
			}
		}

		/// <summary>Gets the spacing between the <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> and an adjacent item.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Padding" /> value representing the spacing.</returns>
		// Token: 0x17000570 RID: 1392
		// (get) Token: 0x06001489 RID: 5257 RVA: 0x00066877 File Offset: 0x00064A77
		protected internal override Padding DefaultMargin
		{
			get
			{
				return new Padding(0);
			}
		}

		/// <summary>Gets the internal spacing within the <see cref="T:System.Windows.Forms.ToolStripMenuItem" />.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Padding" /> value representing the spacing.</returns>
		// Token: 0x17000571 RID: 1393
		// (get) Token: 0x0600148A RID: 5258 RVA: 0x0006687F File Offset: 0x00064A7F
		protected override Padding DefaultPadding
		{
			get
			{
				return new Padding(4, 0, 4, 0);
			}
		}

		/// <summary>Gets the default size of the <see cref="T:System.Windows.Forms.ToolStripMenuItem" />.</summary>
		/// <returns>The <see cref="T:System.Drawing.Size" /> of the <see cref="T:System.Windows.Forms.ToolStripMenuItem" />, measured in pixels. The default is 100 pixels horizontally.</returns>
		// Token: 0x17000572 RID: 1394
		// (get) Token: 0x0600148B RID: 5259 RVA: 0x0006688A File Offset: 0x00064A8A
		protected override Size DefaultSize
		{
			get
			{
				return new Size(32, 19);
			}
		}

		/// <summary>Creates a generic <see cref="T:System.Windows.Forms.ToolStripDropDown" /> for which events can be defined.</summary>
		/// <returns>A generic <see cref="T:System.Windows.Forms.ToolStripDropDown" /> for which can be defined.</returns>
		// Token: 0x0600148C RID: 5260 RVA: 0x00062C9A File Offset: 0x00060E9A
		protected override ToolStripDropDown CreateDefaultDropDown()
		{
			return new ToolStripDropDownMenu
			{
				OwnerItem = this
			};
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> and optionally releases the managed resources. </summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x0600148D RID: 5261 RVA: 0x00066895 File Offset: 0x00064A95
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripMenuItem.CheckedChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x0600148E RID: 5262 RVA: 0x000668A0 File Offset: 0x00064AA0
		protected virtual void OnCheckedChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ToolStripMenuItem.CheckedChangedEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripMenuItem.CheckStateChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x0600148F RID: 5263 RVA: 0x000668D0 File Offset: 0x00064AD0
		protected virtual void OnCheckStateChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ToolStripMenuItem.CheckStateChangedEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.Click" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06001490 RID: 5264 RVA: 0x00066900 File Offset: 0x00064B00
		protected override void OnClick(EventArgs e)
		{
			if (!this.Enabled)
			{
				return;
			}
			if (this.HasDropDownItems)
			{
				base.OnClick(e);
				return;
			}
			if (base.OwnerItem is ToolStripDropDownItem)
			{
				(base.OwnerItem as ToolStripDropDownItem).OnDropDownItemClicked(new ToolStripItemClickedEventArgs(this));
			}
			if (base.IsOnDropDown)
			{
				ToolStrip topLevelToolStrip = this.GetTopLevelToolStrip();
				if (topLevelToolStrip != null)
				{
					topLevelToolStrip.Dismiss(ToolStripDropDownCloseReason.ItemClicked);
				}
			}
			if (this.IsMdiWindowListEntry)
			{
				this.mdi_client_form.MdiParent.MdiContainer.ActivateChild(this.mdi_client_form);
				return;
			}
			if (this.check_on_click)
			{
				this.Checked = !this.Checked;
			}
			base.OnClick(e);
			if (!base.IsOnDropDown && !this.HasDropDownItems)
			{
				ToolStrip topLevelToolStrip2 = this.GetTopLevelToolStrip();
				if (topLevelToolStrip2 != null)
				{
					topLevelToolStrip2.Dismiss(ToolStripDropDownCloseReason.ItemClicked);
				}
			}
		}

		/// <summary>Raised in response to the <see cref="M:System.Windows.Forms.ToolStripDropDownItem.HideDropDown" /> method.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06001491 RID: 5265 RVA: 0x000669C5 File Offset: 0x00064BC5
		protected override void OnDropDownHide(EventArgs e)
		{
			base.OnDropDownHide(e);
		}

		/// <summary>Raised in response to the <see cref="M:System.Windows.Forms.ToolStripDropDownItem.ShowDropDown" /> method.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06001492 RID: 5266 RVA: 0x000669CE File Offset: 0x00064BCE
		protected override void OnDropDownShow(EventArgs e)
		{
			base.OnDropDownShow(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.FontChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06001493 RID: 5267 RVA: 0x000669D7 File Offset: 0x00064BD7
		protected override void OnFontChanged(EventArgs e)
		{
			base.OnFontChanged(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseDown" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data. </param>
		// Token: 0x06001494 RID: 5268 RVA: 0x000669E0 File Offset: 0x00064BE0
		protected override void OnMouseDown(MouseEventArgs e)
		{
			if (!base.IsOnDropDown && this.HasDropDownItems && base.DropDown.Visible)
			{
				this.close_on_mouse_release = true;
			}
			if (this.Enabled && !base.DropDown.Visible)
			{
				base.ShowDropDown();
			}
			base.OnMouseDown(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseEnter" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06001495 RID: 5269 RVA: 0x00066A33 File Offset: 0x00064C33
		protected override void OnMouseEnter(EventArgs e)
		{
			if (base.IsOnDropDown && this.HasDropDownItems && this.Enabled)
			{
				base.ShowDropDown();
			}
			base.OnMouseEnter(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseLeave" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06001496 RID: 5270 RVA: 0x00062CDA File Offset: 0x00060EDA
		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.MouseUp" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
		// Token: 0x06001497 RID: 5271 RVA: 0x00066A5A File Offset: 0x00064C5A
		protected override void OnMouseUp(MouseEventArgs e)
		{
			if (this.close_on_mouse_release)
			{
				base.Parent.Dismiss(ToolStripDropDownCloseReason.ItemClicked);
				base.Invalidate();
				this.close_on_mouse_release = false;
			}
			if (!this.HasDropDownItems && this.Enabled)
			{
				base.OnMouseUp(e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.OwnerChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06001498 RID: 5272 RVA: 0x00066A94 File Offset: 0x00064C94
		protected override void OnOwnerChanged(EventArgs e)
		{
			base.OnOwnerChanged(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06001499 RID: 5273 RVA: 0x00066AA0 File Offset: 0x00064CA0
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			if (base.Owner == null)
			{
				return;
			}
			Image image = (base.UseImageMargin ? this.Image : null);
			Color color = this.ForeColor;
			if ((this.Selected || this.Pressed) && base.IsOnDropDown && color == SystemColors.MenuText)
			{
				color = SystemColors.HighlightText;
			}
			if (!this.Enabled && this.ForeColor == SystemColors.ControlText)
			{
				color = SystemColors.GrayText;
			}
			image = (this.Enabled ? image : ToolStripRenderer.CreateDisabledImage(image));
			base.Owner.Renderer.DrawMenuItemBackground(new ToolStripItemRenderEventArgs(e.Graphics, this));
			Rectangle rectangle;
			Rectangle empty;
			base.CalculateTextAndImageRectangles(out rectangle, out empty);
			if (base.IsOnDropDown)
			{
				if (!base.UseImageMargin)
				{
					empty = Rectangle.Empty;
					rectangle = new Rectangle(8, rectangle.Top, rectangle.Width, rectangle.Height);
				}
				else
				{
					rectangle = new Rectangle(35, rectangle.Top, rectangle.Width, rectangle.Height);
					if (empty != Rectangle.Empty)
					{
						empty = new Rectangle(new Point(4, 3), base.GetImageSize());
					}
				}
				if (this.Checked && base.ShowMargin)
				{
					base.Owner.Renderer.DrawItemCheck(new ToolStripItemImageRenderEventArgs(e.Graphics, this, new Rectangle(2, 1, 19, 19)));
				}
			}
			if (rectangle != Rectangle.Empty)
			{
				base.Owner.Renderer.DrawItemText(new ToolStripItemTextRenderEventArgs(e.Graphics, this, this.Text, rectangle, color, this.Font, this.TextAlign));
			}
			string shortcutDisplayString = this.GetShortcutDisplayString();
			if (!string.IsNullOrEmpty(shortcutDisplayString) && !this.HasDropDownItems)
			{
				int num = 15;
				Size size = TextRenderer.MeasureText(shortcutDisplayString, this.Font);
				Rectangle rectangle2 = new Rectangle(base.ContentRectangle.Right - size.Width - num, rectangle.Top, size.Width, rectangle.Height);
				base.Owner.Renderer.DrawItemText(new ToolStripItemTextRenderEventArgs(e.Graphics, this, shortcutDisplayString, rectangle2, color, this.Font, this.TextAlign));
			}
			if (empty != Rectangle.Empty)
			{
				base.Owner.Renderer.DrawItemImage(new ToolStripItemImageRenderEventArgs(e.Graphics, this, image, empty));
			}
			if (base.IsOnDropDown && this.HasDropDownItems && base.Parent is ToolStripDropDownMenu)
			{
				base.Owner.Renderer.DrawArrow(new ToolStripArrowRenderEventArgs(e.Graphics, this, new Rectangle(this.Bounds.Width - 17, 2, 10, 20), Color.Black, ArrowDirection.Right));
			}
		}

		/// <summary>Processes a command key.</summary>
		/// <returns>true if the character was processed by the control; otherwise, false.</returns>
		/// <param name="m">A <see cref="T:System.Windows.Forms.Message" />, passed by reference, which represents the window message to process. </param>
		/// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values that represents the key to process. </param>
		// Token: 0x0600149A RID: 5274 RVA: 0x00066D5C File Offset: 0x00064F5C
		protected internal override bool ProcessCmdKey(ref Message m, Keys keyData)
		{
			Control control = Control.FromHandle(m.HWnd);
			Form form = ((control == null) ? null : ((Form)control.TopLevelControl));
			if (this.Enabled && keyData == this.shortcut_keys && this.GetTopLevelControl() == form)
			{
				base.FireEvent(EventArgs.Empty, ToolStripItemEventType.Click);
				return true;
			}
			return base.ProcessCmdKey(ref m, keyData);
		}

		// Token: 0x0600149B RID: 5275 RVA: 0x00066DB8 File Offset: 0x00064FB8
		private Control GetTopLevelControl()
		{
			ToolStripItem toolStripItem = this;
			while (toolStripItem.OwnerItem != null)
			{
				toolStripItem = toolStripItem.OwnerItem;
			}
			if (toolStripItem.Owner == null)
			{
				return null;
			}
			if (!(toolStripItem.Owner is ContextMenuStrip))
			{
				return toolStripItem.Owner.TopLevelControl;
			}
			Control container = ((ContextMenuStrip)toolStripItem.Owner).container;
			if (container != null)
			{
				return container.TopLevelControl;
			}
			return null;
		}

		/// <summary>Processes a mnemonic character.</summary>
		/// <returns>true if the character was processed as a mnemonic by the control; otherwise, false.</returns>
		/// <param name="charCode">The character to process. </param>
		// Token: 0x0600149C RID: 5276 RVA: 0x00066E18 File Offset: 0x00065018
		protected internal override bool ProcessMnemonic(char charCode)
		{
			if (!this.Selected)
			{
				base.Parent.ChangeSelection(this);
			}
			if (this.HasDropDownItems)
			{
				ToolStripManager.SetActiveToolStrip(base.Parent, true);
				base.ShowDropDown();
				base.DropDown.SelectNextToolStripItem(null, true);
			}
			else
			{
				base.PerformClick();
			}
			return true;
		}

		/// <summary>Sets the size and location of the <see cref="T:System.Windows.Forms.ToolStripMenuItem" />.</summary>
		/// <param name="rect">A <see cref="T:System.Drawing.Rectangle" /> that represents the size and location of the <see cref="T:System.Windows.Forms.ToolStripMenuItem" />.</param>
		// Token: 0x0600149D RID: 5277 RVA: 0x00066E6A File Offset: 0x0006506A
		protected internal override void SetBounds(Rectangle rect)
		{
			base.SetBounds(rect);
		}

		// Token: 0x17000573 RID: 1395
		// (get) Token: 0x0600149E RID: 5278 RVA: 0x00066E73 File Offset: 0x00065073
		// (set) Token: 0x0600149F RID: 5279 RVA: 0x00066E7B File Offset: 0x0006507B
		internal Form MdiClientForm
		{
			get
			{
				return this.mdi_client_form;
			}
			set
			{
				this.mdi_client_form = value;
			}
		}

		// Token: 0x060014A0 RID: 5280 RVA: 0x00066E84 File Offset: 0x00065084
		internal override Size CalculatePreferredSize(Size constrainingSize)
		{
			Size size = base.CalculatePreferredSize(constrainingSize);
			string shortcutDisplayString = this.GetShortcutDisplayString();
			if (string.IsNullOrEmpty(shortcutDisplayString))
			{
				return size;
			}
			Size size2 = TextRenderer.MeasureText(shortcutDisplayString, this.Font);
			return new Size(size.Width + size2.Width - 25, size.Height);
		}

		// Token: 0x060014A1 RID: 5281 RVA: 0x00066ED8 File Offset: 0x000650D8
		internal string GetShortcutDisplayString()
		{
			if (!this.show_shortcut_keys)
			{
				return string.Empty;
			}
			if (base.Parent == null || !(base.Parent is ToolStripDropDownMenu))
			{
				return string.Empty;
			}
			string text = string.Empty;
			if (!string.IsNullOrEmpty(this.shortcut_display_string))
			{
				text = this.shortcut_display_string;
			}
			else if (this.shortcut_keys != Keys.None)
			{
				text = new KeysConverter().ConvertToString(this.shortcut_keys);
			}
			return text;
		}

		// Token: 0x060014A2 RID: 5282 RVA: 0x00066F49 File Offset: 0x00065149
		internal void HandleAutoExpansion()
		{
			if (this.HasDropDownItems)
			{
				base.ShowDropDown();
				base.DropDown.SelectNextToolStripItem(null, true);
			}
		}

		// Token: 0x060014A3 RID: 5283 RVA: 0x00066F67 File Offset: 0x00065167
		internal override void HandleClick(int mouse_clicks, EventArgs e)
		{
			this.OnClick(e);
			if (base.Parent != null)
			{
				base.Parent.Invalidate();
			}
		}

		// Token: 0x04000C7C RID: 3196
		private CheckState checked_state;

		// Token: 0x04000C7D RID: 3197
		private bool check_on_click;

		// Token: 0x04000C7E RID: 3198
		private bool close_on_mouse_release;

		// Token: 0x04000C7F RID: 3199
		private string shortcut_display_string;

		// Token: 0x04000C80 RID: 3200
		private Keys shortcut_keys;

		// Token: 0x04000C81 RID: 3201
		private bool show_shortcut_keys = true;

		// Token: 0x04000C82 RID: 3202
		private Form mdi_client_form;

		// Token: 0x04000C83 RID: 3203
		private static object CheckedChangedEvent = new object();

		// Token: 0x04000C84 RID: 3204
		private static object CheckStateChangedEvent = new object();

		// Token: 0x04000C85 RID: 3205
		private static object UIACheckOnClickChangedEvent = new object();
	}
}
