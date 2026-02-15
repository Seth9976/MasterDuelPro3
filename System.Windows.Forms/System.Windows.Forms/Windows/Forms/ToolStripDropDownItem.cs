using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

namespace System.Windows.Forms
{
	/// <summary>Provides basic functionality for controls that display a <see cref="T:System.Windows.Forms.ToolStripDropDown" /> when a <see cref="T:System.Windows.Forms.ToolStripDropDownButton" />, <see cref="T:System.Windows.Forms.ToolStripMenuItem" />, or <see cref="T:System.Windows.Forms.ToolStripSplitButton" /> control is clicked.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001C6 RID: 454
	[DefaultProperty("DropDownItems")]
	[Designer("System.Windows.Forms.Design.ToolStripMenuItemDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	public abstract class ToolStripDropDownItem : ToolStripItem
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripDropDownItem" /> class with the specified display text, image, action to take when the drop-down control is clicked, and control name.</summary>
		/// <param name="text">The display text of the drop-down control.</param>
		/// <param name="image">The <see cref="T:System.Drawing.Image" /> to be displayed on the control.</param>
		/// <param name="onClick">The action to take when the drop-down control is clicked.</param>
		/// <param name="name">The name of the control.</param>
		// Token: 0x0600137D RID: 4989 RVA: 0x00062E88 File Offset: 0x00061088
		protected ToolStripDropDownItem(string text, Image image, EventHandler onClick, string name)
			: base(text, image, onClick, name)
		{
		}

		/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> that will be displayed when this <see cref="T:System.Windows.Forms.ToolStripDropDownItem" /> is clicked.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ToolStripDropDown" /> that is associated with the <see cref="T:System.Windows.Forms.ToolStripDropDownItem" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x0600137E RID: 4990 RVA: 0x00062E95 File Offset: 0x00061095
		[TypeConverter(typeof(ReferenceConverter))]
		public ToolStripDropDown DropDown
		{
			get
			{
				if (this.drop_down == null)
				{
					this.drop_down = this.CreateDefaultDropDown();
					this.drop_down.ItemAdded += this.DropDown_ItemAdded;
				}
				return this.drop_down;
			}
		}

		/// <summary>Gets the collection of items in the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> that is associated with this <see cref="T:System.Windows.Forms.ToolStripDropDownItem" />.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ToolStripItemCollection" /> of controls.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000516 RID: 1302
		// (get) Token: 0x0600137F RID: 4991 RVA: 0x00062EC8 File Offset: 0x000610C8
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public ToolStripItemCollection DropDownItems
		{
			get
			{
				return this.DropDown.Items;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Windows.Forms.ToolStripDropDownItem" /> has <see cref="T:System.Windows.Forms.ToolStripDropDown" /> controls associated with it. </summary>
		/// <returns>true if the <see cref="T:System.Windows.Forms.ToolStripDropDownItem" /> has <see cref="T:System.Windows.Forms.ToolStripDropDown" /> controls; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000517 RID: 1303
		// (get) Token: 0x06001380 RID: 4992 RVA: 0x00062ED5 File Offset: 0x000610D5
		[Browsable(false)]
		public virtual bool HasDropDownItems
		{
			get
			{
				return this.drop_down != null && this.DropDown.Items.Count != 0;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Windows.Forms.ToolStripDropDownItem" /> is in the pressed state.</summary>
		/// <returns>true if the <see cref="T:System.Windows.Forms.ToolStripDropDownItem" /> is in the pressed state; otherwise, false. </returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000518 RID: 1304
		// (get) Token: 0x06001381 RID: 4993 RVA: 0x00062EF4 File Offset: 0x000610F4
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override bool Pressed
		{
			get
			{
				return base.Pressed || (this.drop_down != null && this.DropDown.Visible);
			}
		}

		/// <summary>Gets the screen coordinates, in pixels, of the upper-left corner of the <see cref="T:System.Windows.Forms.ToolStripDropDownItem" />.</summary>
		/// <returns>A Point representing the x and y screen coordinates, in pixels.</returns>
		// Token: 0x17000519 RID: 1305
		// (get) Token: 0x06001382 RID: 4994 RVA: 0x00062F18 File Offset: 0x00061118
		protected internal virtual Point DropDownLocation
		{
			get
			{
				Point point;
				if (base.IsOnDropDown)
				{
					point = base.Parent.PointToScreen(new Point(this.Bounds.Left, this.Bounds.Top - 1));
					point.X += this.Bounds.Width;
					point.Y += this.Bounds.Left;
					return point;
				}
				point = new Point(this.Bounds.Left, this.Bounds.Bottom - 1);
				return base.Parent.PointToScreen(point);
			}
		}

		/// <summary>Makes a visible <see cref="T:System.Windows.Forms.ToolStripDropDown" /> hidden.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001383 RID: 4995 RVA: 0x00062FC7 File Offset: 0x000611C7
		public void HideDropDown()
		{
			if (this.drop_down == null || !this.DropDown.Visible)
			{
				return;
			}
			this.OnDropDownHide(EventArgs.Empty);
			this.DropDown.Close(ToolStripDropDownCloseReason.CloseCalled);
			this.is_pressed = false;
			base.Invalidate();
		}

		/// <summary>Displays the <see cref="T:System.Windows.Forms.ToolStripDropDownItem" /> control associated with this <see cref="T:System.Windows.Forms.ToolStripDropDownItem" />.</summary>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Windows.Forms.ToolStripDropDownItem" /> is the same as the parent <see cref="T:System.Windows.Forms.ToolStrip" />.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001384 RID: 4996 RVA: 0x00063003 File Offset: 0x00061203
		public void ShowDropDown()
		{
			if (this.DropDown.Visible)
			{
				return;
			}
			this.OnDropDownShow(EventArgs.Empty);
			if (!this.HasDropDownItems)
			{
				return;
			}
			base.Invalidate();
			this.DropDown.Show(this.DropDownLocation);
		}

		/// <summary>Creates a generic <see cref="T:System.Windows.Forms.ToolStripDropDown" /> for which events can be defined.</summary>
		/// <returns>The created <see cref="T:System.Windows.Forms.ToolStripDropDown" /> object.</returns>
		// Token: 0x06001385 RID: 4997 RVA: 0x0006303E File Offset: 0x0006123E
		protected virtual ToolStripDropDown CreateDefaultDropDown()
		{
			return new ToolStripDropDown
			{
				OwnerItem = this
			};
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.ToolStripDropDownItem" /> and optionally releases the managed resources. </summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x06001386 RID: 4998 RVA: 0x0006304C File Offset: 0x0006124C
		protected override void Dispose(bool disposing)
		{
			if (!base.IsDisposed)
			{
				if (disposing)
				{
					if (this.HasDropDownItems)
					{
						foreach (object obj in this.DropDownItems)
						{
							ToolStripItem toolStripItem = (ToolStripItem)obj;
							if (toolStripItem is ToolStripMenuItem)
							{
								ToolStripManager.RemoveToolStripMenuItem((ToolStripMenuItem)toolStripItem);
							}
						}
					}
					if (this.drop_down != null)
					{
						ToolStripManager.RemoveToolStrip(this.drop_down);
					}
				}
				base.Dispose(disposing);
			}
		}

		// Token: 0x06001387 RID: 4999 RVA: 0x000630E0 File Offset: 0x000612E0
		protected override void OnBoundsChanged()
		{
			base.OnBoundsChanged();
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripDropDownItem.DropDownClosed" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06001388 RID: 5000 RVA: 0x000630E8 File Offset: 0x000612E8
		protected internal virtual void OnDropDownClosed(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ToolStripDropDownItem.DropDownClosedEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raised in response to the <see cref="M:System.Windows.Forms.ToolStripDropDownItem.HideDropDown" /> method.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06001389 RID: 5001 RVA: 0x0000493C File Offset: 0x00002B3C
		protected virtual void OnDropDownHide(EventArgs e)
		{
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripDropDownItem.DropDownItemClicked" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripItemClickedEventArgs" /> that contains the event data.</param>
		// Token: 0x0600138A RID: 5002 RVA: 0x00063118 File Offset: 0x00061318
		protected internal virtual void OnDropDownItemClicked(ToolStripItemClickedEventArgs e)
		{
			ToolStripItemClickedEventHandler toolStripItemClickedEventHandler = (ToolStripItemClickedEventHandler)base.Events[ToolStripDropDownItem.DropDownItemClickedEvent];
			if (toolStripItemClickedEventHandler != null)
			{
				toolStripItemClickedEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripDropDownItem.DropDownOpened" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x0600138B RID: 5003 RVA: 0x00063148 File Offset: 0x00061348
		protected internal virtual void OnDropDownOpened(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ToolStripDropDownItem.DropDownOpenedEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raised in response to the <see cref="M:System.Windows.Forms.ToolStripDropDownItem.ShowDropDown" /> method.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x0600138C RID: 5004 RVA: 0x00063178 File Offset: 0x00061378
		protected virtual void OnDropDownShow(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ToolStripDropDownItem.DropDownOpeningEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripDropDown.FontChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x0600138D RID: 5005 RVA: 0x000631A6 File Offset: 0x000613A6
		protected override void OnFontChanged(EventArgs e)
		{
			base.OnFontChanged(e);
			if (this.drop_down != null)
			{
				this.drop_down.Font = this.Font;
			}
		}

		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x0600138E RID: 5006 RVA: 0x000631C8 File Offset: 0x000613C8
		protected override void OnRightToLeftChanged(EventArgs e)
		{
			base.OnRightToLeftChanged(e);
		}

		/// <summary>Processes a command key.</summary>
		/// <returns>false in all cases.</returns>
		/// <param name="m">A <see cref="T:System.Windows.Forms.Message" />, passed by reference, that represents the window message to process. </param>
		/// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values that represents the key to process. </param>
		// Token: 0x0600138F RID: 5007 RVA: 0x000631D4 File Offset: 0x000613D4
		protected internal override bool ProcessCmdKey(ref Message m, Keys keyData)
		{
			if (this.HasDropDownItems)
			{
				using (IEnumerator enumerator = this.DropDownItems.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (((ToolStripItem)enumerator.Current).ProcessCmdKey(ref m, keyData))
						{
							return true;
						}
					}
				}
			}
			return base.ProcessCmdKey(ref m, keyData);
		}

		/// <summary>Processes a dialog key.</summary>
		/// <returns>true if the key was processed by the item; otherwise, false.</returns>
		/// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values that represents the key to process. </param>
		// Token: 0x06001390 RID: 5008 RVA: 0x00063244 File Offset: 0x00061444
		protected internal override bool ProcessDialogKey(Keys keyData)
		{
			if (!this.Selected || !this.HasDropDownItems)
			{
				return base.ProcessDialogKey(keyData);
			}
			if (!base.IsOnDropDown)
			{
				if (base.Parent.Orientation == Orientation.Horizontal)
				{
					if (keyData == Keys.Down || keyData == Keys.Return)
					{
						if (base.Parent is MenuStrip)
						{
							(base.Parent as MenuStrip).MenuDroppedDown = true;
						}
						this.ShowDropDown();
						this.DropDown.SelectNextToolStripItem(null, true);
						return true;
					}
				}
				else if (keyData == Keys.Right || keyData == Keys.Return)
				{
					if (base.Parent is MenuStrip)
					{
						(base.Parent as MenuStrip).MenuDroppedDown = true;
					}
					this.ShowDropDown();
					this.DropDown.SelectNextToolStripItem(null, true);
					return true;
				}
			}
			else if ((keyData == Keys.Right || keyData == Keys.Return) && this.HasDropDownItems)
			{
				this.ShowDropDown();
				this.DropDown.SelectNextToolStripItem(null, true);
				return true;
			}
			return base.ProcessDialogKey(keyData);
		}

		// Token: 0x06001391 RID: 5009 RVA: 0x0006332F File Offset: 0x0006152F
		internal override void Dismiss(ToolStripDropDownCloseReason reason)
		{
			if (this.HasDropDownItems && this.DropDown.Visible)
			{
				this.DropDown.Dismiss(reason);
			}
			base.Dismiss(reason);
		}

		// Token: 0x06001392 RID: 5010 RVA: 0x00063359 File Offset: 0x00061559
		internal override void HandleClick(int mouse_clicks, EventArgs e)
		{
			this.OnClick(e);
		}

		// Token: 0x06001393 RID: 5011 RVA: 0x00063362 File Offset: 0x00061562
		internal void HideDropDown(ToolStripDropDownCloseReason reason)
		{
			if (this.drop_down == null || !this.DropDown.Visible)
			{
				return;
			}
			this.OnDropDownHide(EventArgs.Empty);
			this.DropDown.Close(reason);
			this.is_pressed = false;
			base.Invalidate();
		}

		// Token: 0x06001394 RID: 5012 RVA: 0x0006339E File Offset: 0x0006159E
		private void DropDown_ItemAdded(object sender, ToolStripItemEventArgs e)
		{
			e.Item.owner_item = this;
		}

		// Token: 0x04000BE5 RID: 3045
		internal ToolStripDropDown drop_down;

		// Token: 0x04000BE6 RID: 3046
		private static object DropDownClosedEvent = new object();

		// Token: 0x04000BE7 RID: 3047
		private static object DropDownItemClickedEvent = new object();

		// Token: 0x04000BE8 RID: 3048
		private static object DropDownOpenedEvent = new object();

		// Token: 0x04000BE9 RID: 3049
		private static object DropDownOpeningEvent = new object();
	}
}
