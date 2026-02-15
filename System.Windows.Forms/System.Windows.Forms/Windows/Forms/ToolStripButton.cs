using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms.Design;

namespace System.Windows.Forms
{
	/// <summary>Represents a selectable <see cref="T:System.Windows.Forms.ToolStripItem" /> that can contain text and images. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001BA RID: 442
	[ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.ToolStrip)]
	public class ToolStripButton : ToolStripItem
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripButton" /> class that displays the specified text and image and that raises the <see cref="E:System.Windows.Forms.ToolStripItem.Click" /> event.</summary>
		/// <param name="text">The text to display on the <see cref="T:System.Windows.Forms.ToolStripButton" />.</param>
		/// <param name="image">The image to display on the <see cref="T:System.Windows.Forms.ToolStripButton" />.</param>
		/// <param name="onClick">An event handler that raises the <see cref="E:System.Windows.Forms.ToolStripItem.Click" /> event.</param>
		// Token: 0x06001321 RID: 4897 RVA: 0x00061D60 File Offset: 0x0005FF60
		public ToolStripButton(string text, Image image, EventHandler onClick)
			: this(text, image, onClick, string.Empty)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripButton" /> class with the specified name that displays the specified text and image and that raises the <see cref="E:System.Windows.Forms.ToolStripItem.Click" /> event.</summary>
		/// <param name="text">The text to display on the <see cref="T:System.Windows.Forms.ToolStripButton" />.</param>
		/// <param name="image">The image to display on the <see cref="T:System.Windows.Forms.ToolStripButton" />.</param>
		/// <param name="onClick">An event handler that raises the <see cref="E:System.Windows.Forms.ToolStripItem.Click" /> event.</param>
		/// <param name="name">The name of the <see cref="T:System.Windows.Forms.ToolStripButton" />.</param>
		// Token: 0x06001322 RID: 4898 RVA: 0x00061D70 File Offset: 0x0005FF70
		public ToolStripButton(string text, Image image, EventHandler onClick, string name)
			: base(text, image, onClick, name)
		{
			this.checked_state = CheckState.Unchecked;
			base.ToolTipText = string.Empty;
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Windows.Forms.ToolStripButton" /> can be selected.</summary>
		/// <returns>true if the <see cref="T:System.Windows.Forms.ToolStripButton" /> can be selected; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170004FC RID: 1276
		// (get) Token: 0x06001323 RID: 4899 RVA: 0x00006F54 File Offset: 0x00005154
		public override bool CanSelect
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.ToolStripButton" /> is pressed or not pressed.</summary>
		/// <returns>true if the <see cref="T:System.Windows.Forms.ToolStripButton" /> is pressed in or not pressed in; otherwise, false. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170004FD RID: 1277
		// (get) Token: 0x06001324 RID: 4900 RVA: 0x00061D90 File Offset: 0x0005FF90
		// (set) Token: 0x06001325 RID: 4901 RVA: 0x00061DB0 File Offset: 0x0005FFB0
		[DefaultValue(false)]
		public bool Checked
		{
			get
			{
				CheckState checkState = this.checked_state;
				return checkState != CheckState.Unchecked && checkState - CheckState.Checked <= 1;
			}
			set
			{
				if (this.checked_state != (value ? CheckState.Checked : CheckState.Unchecked))
				{
					this.checked_state = (value ? CheckState.Checked : CheckState.Unchecked);
					this.OnCheckedChanged(EventArgs.Empty);
					this.OnCheckStateChanged(EventArgs.Empty);
					base.Invalidate();
				}
			}
		}

		/// <summary>Gets a value indicating whether to display the ToolTip that is defined as the default. </summary>
		/// <returns>true in all cases.</returns>
		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x06001326 RID: 4902 RVA: 0x00006F54 File Offset: 0x00005154
		protected override bool DefaultAutoToolTip
		{
			get
			{
				return true;
			}
		}

		/// <summary>Retrieves the size of a rectangular area into which a <see cref="T:System.Windows.Forms.ToolStripButton" /> can be fitted.</summary>
		/// <returns>An ordered pair of type <see cref="T:System.Drawing.Size" /> representing the width and height of a rectangle.</returns>
		/// <param name="constrainingSize">The specified area for a <see cref="T:System.Windows.Forms.ToolStripButton" />.</param>
		// Token: 0x06001327 RID: 4903 RVA: 0x00061DEC File Offset: 0x0005FFEC
		public override Size GetPreferredSize(Size constrainingSize)
		{
			Size preferredSize = base.GetPreferredSize(constrainingSize);
			if (preferredSize.Width < 23)
			{
				preferredSize.Width = 23;
			}
			return preferredSize;
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripButton.CheckedChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06001328 RID: 4904 RVA: 0x00061E18 File Offset: 0x00060018
		protected virtual void OnCheckedChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ToolStripButton.CheckedChangedEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripButton.CheckStateChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06001329 RID: 4905 RVA: 0x00061E48 File Offset: 0x00060048
		protected virtual void OnCheckStateChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ToolStripButton.CheckStateChangedEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Click" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x0600132A RID: 4906 RVA: 0x00061E78 File Offset: 0x00060078
		protected override void OnClick(EventArgs e)
		{
			if (this.check_on_click)
			{
				this.Checked = !this.Checked;
			}
			base.OnClick(e);
			ToolStrip topLevelToolStrip = this.GetTopLevelToolStrip();
			if (topLevelToolStrip != null)
			{
				topLevelToolStrip.Dismiss(ToolStripDropDownCloseReason.ItemClicked);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data. </param>
		// Token: 0x0600132B RID: 4907 RVA: 0x00061EB4 File Offset: 0x000600B4
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			if (base.Owner != null)
			{
				Color color = (this.Enabled ? this.ForeColor : SystemColors.GrayText);
				Image image = (this.Enabled ? this.Image : ToolStripRenderer.CreateDisabledImage(this.Image));
				base.Owner.Renderer.DrawButtonBackground(new ToolStripItemRenderEventArgs(e.Graphics, this));
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
				return;
			}
		}

		// Token: 0x04000BB9 RID: 3001
		private CheckState checked_state;

		// Token: 0x04000BBA RID: 3002
		private bool check_on_click;

		// Token: 0x04000BBB RID: 3003
		private static object CheckedChangedEvent = new object();

		// Token: 0x04000BBC RID: 3004
		private static object CheckStateChangedEvent = new object();

		// Token: 0x04000BBD RID: 3005
		private static object UIACheckOnClickChangedEvent = new object();
	}
}
