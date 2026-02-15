using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Represents a window or dialog box that makes up an application's user interface.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020000B1 RID: 177
	[DesignerCategory("Form")]
	[DesignTimeVisible(false)]
	[Designer("System.Windows.Forms.Design.FormDocumentDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(IRootDesigner))]
	[DefaultEvent("Load")]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[InitializationEvent("Load")]
	[ComVisible(true)]
	[ToolboxItemFilter("System.Windows.Forms.Control.TopLevel")]
	[ToolboxItem(false)]
	public class Form : ContainerControl
	{
		// Token: 0x0600069B RID: 1691 RVA: 0x0001D0A4 File Offset: 0x0001B2A4
		static Form()
		{
			Form.ClosedEvent = new object();
			Form.ClosingEvent = new object();
			Form.DeactivateEvent = new object();
			Form.InputLanguageChangedEvent = new object();
			Form.InputLanguageChangingEvent = new object();
			Form.LoadEvent = new object();
			Form.MaximizedBoundsChangedEvent = new object();
			Form.MaximumSizeChangedEvent = new object();
			Form.MdiChildActivateEvent = new object();
			Form.MenuCompleteEvent = new object();
			Form.MenuStartEvent = new object();
			Form.MinimumSizeChangedEvent = new object();
			Form.FormClosingEvent = new object();
			Form.FormClosedEvent = new object();
			Form.HelpButtonClickedEvent = new object();
			Form.ResizeEndEvent = new object();
			Form.ResizeBeginEvent = new object();
			Form.RightToLeftLayoutChangedEvent = new object();
			Form.ShownEvent = new object();
			Form.UIAMenuChangedEvent = new object();
			Form.UIATopMostChangedEvent = new object();
			Form.UIAWindowStateChangedEvent = new object();
			Form.default_icon = ResourceImageLoader.GetIcon("mono.ico");
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x0600069C RID: 1692 RVA: 0x0001D1B0 File Offset: 0x0001B3B0
		internal bool IsLoaded
		{
			get
			{
				return this.is_loaded;
			}
		}

		// Token: 0x17000196 RID: 406
		// (set) Token: 0x0600069D RID: 1693 RVA: 0x0001D1B8 File Offset: 0x0001B3B8
		internal bool IsActive
		{
			set
			{
				if (this.is_active == value || base.IsRecreating)
				{
					return;
				}
				this.is_active = value;
				if (this.is_active)
				{
					Application.AddForm(this);
					this.OnActivated(EventArgs.Empty);
					return;
				}
				this.OnDeactivate(EventArgs.Empty);
			}
		}

		// Token: 0x0600069E RID: 1694 RVA: 0x0001D1F8 File Offset: 0x0001B3F8
		internal bool FireClosingEvents(CloseReason reason, bool cancel)
		{
			CancelEventArgs cancelEventArgs = new CancelEventArgs(cancel);
			this.OnClosing(cancelEventArgs);
			FormClosingEventArgs formClosingEventArgs = new FormClosingEventArgs(reason, cancelEventArgs.Cancel);
			this.OnFormClosing(formClosingEventArgs);
			return formClosingEventArgs.Cancel;
		}

		// Token: 0x0600069F RID: 1695 RVA: 0x0001D22D File Offset: 0x0001B42D
		private void FireClosedEvents(CloseReason reason)
		{
			this.OnClosed(EventArgs.Empty);
			this.OnFormClosed(new FormClosedEventArgs(reason));
		}

		// Token: 0x060006A0 RID: 1696 RVA: 0x0001D248 File Offset: 0x0001B448
		internal override Size GetPreferredSizeCore(Size proposedSize)
		{
			Size empty = Size.Empty;
			foreach (object obj in base.Controls)
			{
				Control control = (Control)obj;
				Size size;
				if (control.AutoSize)
				{
					size = control.PreferredSize;
				}
				else
				{
					size = control.ExplicitBounds.Size;
				}
				int num = control.Bounds.X + size.Width;
				int num2 = control.Bounds.Y + size.Height;
				if (control.Dock == DockStyle.Fill)
				{
					if (num > empty.Width)
					{
						empty.Width = num;
					}
				}
				else if (control.Dock != DockStyle.Top && control.Dock != DockStyle.Bottom && num > empty.Width)
				{
					empty.Width = num + control.Margin.Right;
				}
				if (control.Dock == DockStyle.Fill)
				{
					if (num2 > empty.Height)
					{
						empty.Height = num2;
					}
				}
				else if (control.Dock != DockStyle.Left && control.Dock != DockStyle.Right && num2 > empty.Height)
				{
					empty.Height = num2 + control.Margin.Bottom;
				}
			}
			if (empty == Size.Empty)
			{
				empty.Height += base.Padding.Top;
				empty.Width += base.Padding.Left;
			}
			empty.Height += base.Padding.Bottom;
			empty.Width += base.Padding.Right;
			return this.SizeFromClientSize(empty);
		}

		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> representing the bounds within which the control is scaled.</returns>
		/// <param name="bounds">A <see cref="T:System.Drawing.Rectangle" /> that specifies the area for which to retrieve the display bounds.</param>
		/// <param name="factor">The height and width of the control's bounds.</param>
		/// <param name="specified">One of the values of <see cref="T:System.Windows.Forms.BoundsSpecified" /> that specifies the bounds of the control to use when defining its size and position.</param>
		// Token: 0x060006A1 RID: 1697 RVA: 0x0001D43C File Offset: 0x0001B63C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override Rectangle GetScaledBounds(Rectangle bounds, SizeF factor, BoundsSpecified specified)
		{
			if ((specified & BoundsSpecified.Width) == BoundsSpecified.Width)
			{
				int num = this.Size.Width - this.ClientSize.Width;
				bounds.Width = (int)Math.Round((double)((float)(bounds.Width - num) * factor.Width)) + num;
			}
			if ((specified & BoundsSpecified.Height) == BoundsSpecified.Height)
			{
				int num2 = this.Size.Height - this.ClientSize.Height;
				bounds.Height = (int)Math.Round((double)((float)(bounds.Height - num2) * factor.Height)) + num2;
			}
			return bounds;
		}

		/// <summary>Processes a mnemonic character.</summary>
		/// <returns>true if the character was processed as a mnemonic by the control; otherwise, false.</returns>
		/// <param name="charCode">The character to process. </param>
		// Token: 0x060006A2 RID: 1698 RVA: 0x0001D4D6 File Offset: 0x0001B6D6
		protected override bool ProcessMnemonic(char charCode)
		{
			return base.ProcessMnemonic(charCode);
		}

		/// <summary>Scales the location, size, padding, and margin of a control.</summary>
		/// <param name="factor">The factor by which the height and width of the control are scaled.</param>
		/// <param name="specified">A <see cref="T:System.Windows.Forms.BoundsSpecified" /> value that specifies the bounds of the control to use when defining its size and position.</param>
		// Token: 0x060006A3 RID: 1699 RVA: 0x0001D4DF File Offset: 0x0001B6DF
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void ScaleControl(SizeF factor, BoundsSpecified specified)
		{
			base.ScaleControl(factor, specified);
		}

		// Token: 0x060006A4 RID: 1700 RVA: 0x0001D4E9 File Offset: 0x0001B6E9
		internal void OnActivatedInternal()
		{
			this.OnActivated(EventArgs.Empty);
		}

		// Token: 0x060006A5 RID: 1701 RVA: 0x0001D4F6 File Offset: 0x0001B6F6
		internal void OnDeactivateInternal()
		{
			this.OnDeactivate(EventArgs.Empty);
		}

		// Token: 0x060006A6 RID: 1702 RVA: 0x0001D504 File Offset: 0x0001B704
		internal override void UpdateWindowText()
		{
			if (!base.IsHandleCreated)
			{
				return;
			}
			if (this.shown_raised)
			{
				XplatUI.SetWindowStyle(this.window.Handle, this.CreateParams);
			}
			XplatUI.Text(base.Handle, this.Text.Replace(Environment.NewLine, string.Empty));
		}

		// Token: 0x060006A7 RID: 1703 RVA: 0x0001D55C File Offset: 0x0001B75C
		internal void SelectActiveControl()
		{
			if (this.IsMdiContainer)
			{
				this.mdi_container.SendFocusToActiveChild();
				return;
			}
			if (base.ActiveControl == null)
			{
				bool is_visible = this.is_visible;
				this.is_visible = true;
				if (!base.SelectNextControl(this, true, true, true, true))
				{
					base.Select(this);
				}
				this.is_visible = is_visible;
				return;
			}
			base.Select(base.ActiveControl);
		}

		// Token: 0x060006A8 RID: 1704 RVA: 0x0001D5C0 File Offset: 0x0001B7C0
		private new void UpdateSizeGripVisible()
		{
			bool flag = false;
			switch (this.size_grip_style)
			{
			case SizeGripStyle.Auto:
				flag = this.is_modal && (this.form_border_style == FormBorderStyle.Sizable || this.form_border_style == FormBorderStyle.SizableToolWindow);
				break;
			case SizeGripStyle.Show:
				flag = this.form_border_style == FormBorderStyle.Sizable || this.form_border_style == FormBorderStyle.SizableToolWindow;
				break;
			case SizeGripStyle.Hide:
				flag = false;
				break;
			}
			if (!flag)
			{
				if (this.size_grip != null && this.size_grip.Visible)
				{
					this.size_grip.Visible = false;
					return;
				}
			}
			else
			{
				if (this.size_grip == null)
				{
					this.size_grip = new SizeGrip(this);
					this.size_grip.Virtual = true;
					this.size_grip.FillBackground = false;
				}
				this.size_grip.Visible = true;
			}
		}

		// Token: 0x060006A9 RID: 1705 RVA: 0x0001D684 File Offset: 0x0001B884
		internal void ChangingParent(Control new_parent)
		{
			if (this.IsMdiChild)
			{
				return;
			}
			bool flag = false;
			if (new_parent == null)
			{
				this.window_manager = null;
			}
			else if (new_parent is MdiClient)
			{
				this.window_manager = new MdiWindowManager(this, (MdiClient)new_parent);
			}
			else
			{
				this.window_manager = new FormWindowManager(this);
				flag = true;
			}
			if (flag)
			{
				if (base.IsHandleCreated)
				{
					if (new_parent != null && new_parent.IsHandleCreated)
					{
						base.RecreateHandle();
					}
					else
					{
						this.DestroyHandle();
					}
				}
			}
			else if (base.IsHandleCreated)
			{
				IntPtr intPtr = IntPtr.Zero;
				if (new_parent != null && new_parent.IsHandleCreated)
				{
					intPtr = new_parent.Handle;
				}
				XplatUI.SetParent(base.Handle, intPtr);
			}
			if (this.window_manager != null)
			{
				this.window_manager.UpdateWindowState(this.window_state, this.window_state, true);
			}
		}

		// Token: 0x060006AA RID: 1706 RVA: 0x0001D745 File Offset: 0x0001B945
		internal override bool FocusInternal(bool skip_check)
		{
			if (this.IsMdiChild && !base.IsHandleCreated)
			{
				this.CreateHandle();
			}
			return base.FocusInternal(skip_check);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Form" /> class.</summary>
		// Token: 0x060006AB RID: 1707 RVA: 0x0001D764 File Offset: 0x0001B964
		public Form()
		{
			SizeF autoScaleSize = Form.GetAutoScaleSize(this.Font);
			this.autoscale = true;
			this.autoscale_base_size = new Size((int)Math.Round((double)autoScaleSize.Width), (int)Math.Round((double)autoScaleSize.Height));
			this.allow_transparency = false;
			this.closing = false;
			this.is_modal = false;
			this.dialog_result = DialogResult.None;
			this.start_position = FormStartPosition.WindowsDefaultLocation;
			this.form_border_style = FormBorderStyle.Sizable;
			this.window_state = FormWindowState.Normal;
			this.key_preview = false;
			this.opacity = 1.0;
			this.menu = null;
			this.icon = Form.default_icon;
			this.minimum_size = Size.Empty;
			this.maximum_size = Size.Empty;
			this.clientsize_set = Size.Empty;
			this.control_box = true;
			this.minimize_box = true;
			this.maximize_box = true;
			this.help_button = false;
			this.show_in_taskbar = true;
			this.is_visible = false;
			this.is_toplevel = true;
			this.size_grip_style = SizeGripStyle.Auto;
			this.maximized_bounds = Rectangle.Empty;
			this.default_maximized_bounds = Rectangle.Empty;
			this.owned_forms = new Form.ControlCollection(this);
			this.transparency_key = Color.Empty;
			base.CreateDockPadding();
			base.InternalClientSize = new Size(base.Width - SystemInformation.FrameBorderSize.Width * 2, base.Height - SystemInformation.FrameBorderSize.Height * 2 - SystemInformation.CaptionHeight);
			this.restore_bounds = base.Bounds;
		}

		/// <summary>Gets the currently active form for this application.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Form" /> that represents the currently active form, or null if there is no active form.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000197 RID: 407
		// (get) Token: 0x060006AC RID: 1708 RVA: 0x0001D8F4 File Offset: 0x0001BAF4
		public static Form ActiveForm
		{
			get
			{
				Control control = Control.FromHandle(XplatUI.GetActive());
				if (control != null)
				{
					if (control is Form)
					{
						return (Form)control;
					}
					for (Control control2 = control.Parent; control2 != null; control2 = control2.Parent)
					{
						if (control2 is Form)
						{
							return (Form)control2;
						}
					}
				}
				return null;
			}
		}

		/// <summary>Gets or sets the button on the form that is clicked when the user presses the ENTER key.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.IButtonControl" /> that represents the button to use as the accept button for the form.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000198 RID: 408
		// (get) Token: 0x060006AD RID: 1709 RVA: 0x0001D943 File Offset: 0x0001BB43
		// (set) Token: 0x060006AE RID: 1710 RVA: 0x0001D94B File Offset: 0x0001BB4B
		[DefaultValue(null)]
		public IButtonControl AcceptButton
		{
			get
			{
				return this.accept_button;
			}
			set
			{
				if (this.accept_button != null)
				{
					this.accept_button.NotifyDefault(false);
				}
				this.accept_button = value;
				if (this.accept_button != null)
				{
					this.accept_button.NotifyDefault(true);
				}
				this.CheckAcceptButton();
			}
		}

		/// <summary>Gets or sets a value indicating whether the form adjusts its size to fit the height of the font used on the form and scales its controls.</summary>
		/// <returns>true if the form will automatically scale itself and its controls based on the current font assigned to the form; otherwise, false. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000199 RID: 409
		// (get) Token: 0x060006AF RID: 1711 RVA: 0x0001D982 File Offset: 0x0001BB82
		// (set) Token: 0x060006B0 RID: 1712 RVA: 0x0001D98A File Offset: 0x0001BB8A
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Obsolete("This property has been deprecated in favor of AutoScaleMode.")]
		[MWFCategory("Layout")]
		public bool AutoScale
		{
			get
			{
				return this.autoscale;
			}
			set
			{
				if (value)
				{
					base.AutoScaleMode = AutoScaleMode.None;
				}
				this.autoscale = value;
			}
		}

		/// <summary>Gets or sets the base size used for autoscaling of the form.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that represents the base size that this form uses for autoscaling.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700019A RID: 410
		// (get) Token: 0x060006B1 RID: 1713 RVA: 0x0001D99D File Offset: 0x0001BB9D
		// (set) Token: 0x060006B2 RID: 1714 RVA: 0x0001D9A5 File Offset: 0x0001BBA5
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Localizable(true)]
		[Browsable(false)]
		public virtual Size AutoScaleBaseSize
		{
			get
			{
				return this.autoscale_base_size;
			}
			[MonoTODO("Setting this is probably unintentional and can cause Forms to be improperly sized.  See http://www.mono-project.com/FAQ:_Winforms#My_forms_are_sized_improperly for details.")]
			set
			{
				this.autoscale_base_size = value;
				this.autoscale_base_size_set = true;
			}
		}

		/// <summary>Gets or sets a value indicating whether the form enables autoscrolling.</summary>
		/// <returns>true to enable autoscrolling on the form; otherwise, false. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700019B RID: 411
		// (get) Token: 0x060006B3 RID: 1715 RVA: 0x0001D9B5 File Offset: 0x0001BBB5
		[Localizable(true)]
		public override bool AutoScroll
		{
			get
			{
				return base.AutoScroll;
			}
		}

		/// <summary>Resize the form according to the setting of <see cref="P:System.Windows.Forms.Form.AutoSizeMode" />.</summary>
		/// <returns>true if the form will automatically resize; false if it must be manually resized.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700019C RID: 412
		// (get) Token: 0x060006B4 RID: 1716 RVA: 0x000042BD File Offset: 0x000024BD
		// (set) Token: 0x060006B5 RID: 1717 RVA: 0x0001D9BD File Offset: 0x0001BBBD
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public override bool AutoSize
		{
			get
			{
				return base.AutoSize;
			}
			set
			{
				if (base.AutoSize != value)
				{
					base.AutoSize = value;
					base.PerformLayout(this, "AutoSize");
				}
			}
		}

		/// <summary>Gets or sets the mode by which the form automatically resizes itself.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.AutoSizeMode" /> enumerated value. The default is <see cref="F:System.Windows.Forms.AutoSizeMode.GrowOnly" />. </returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value is not a valid <see cref="T:System.Windows.Forms.AutoSizeMode" /> value.</exception>
		// Token: 0x1700019D RID: 413
		// (get) Token: 0x060006B6 RID: 1718 RVA: 0x0001D9DB File Offset: 0x0001BBDB
		[Browsable(true)]
		[Localizable(true)]
		[DefaultValue(AutoSizeMode.GrowOnly)]
		public AutoSizeMode AutoSizeMode
		{
			get
			{
				return base.GetAutoSizeMode();
			}
		}

		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the background color of the control. The default is the value of the <see cref="P:System.Windows.Forms.Control.DefaultBackColor" /> property.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700019E RID: 414
		// (get) Token: 0x060006B7 RID: 1719 RVA: 0x0001D9E3 File Offset: 0x0001BBE3
		// (set) Token: 0x060006B8 RID: 1720 RVA: 0x000042D6 File Offset: 0x000024D6
		public override Color BackColor
		{
			get
			{
				if (this.background_color.IsEmpty)
				{
					return Control.DefaultBackColor;
				}
				return this.background_color;
			}
			set
			{
				base.BackColor = value;
			}
		}

		/// <summary>Gets or sets the button control that is clicked when the user presses the ESC key.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.IButtonControl" /> that represents the cancel button for the form.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700019F RID: 415
		// (get) Token: 0x060006B9 RID: 1721 RVA: 0x0001D9FE File Offset: 0x0001BBFE
		// (set) Token: 0x060006BA RID: 1722 RVA: 0x0001DA06 File Offset: 0x0001BC06
		[DefaultValue(null)]
		public IButtonControl CancelButton
		{
			get
			{
				return this.cancel_button;
			}
			set
			{
				this.cancel_button = value;
				if (this.cancel_button != null && this.cancel_button.DialogResult == DialogResult.None)
				{
					this.cancel_button.DialogResult = DialogResult.Cancel;
				}
			}
		}

		/// <summary>Gets or sets the size of the client area of the form.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that represents the size of the form's client area.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x060006BB RID: 1723 RVA: 0x0001DA30 File Offset: 0x0001BC30
		// (set) Token: 0x060006BC RID: 1724 RVA: 0x0001DA38 File Offset: 0x0001BC38
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Localizable(true)]
		public new Size ClientSize
		{
			get
			{
				return base.ClientSize;
			}
			set
			{
				this.is_clientsize_set = true;
				base.ClientSize = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether a control box is displayed in the caption bar of the form.</summary>
		/// <returns>true if the form displays a control box in the upper left corner of the form; otherwise, false. The default is true.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x060006BD RID: 1725 RVA: 0x0001DA48 File Offset: 0x0001BC48
		// (set) Token: 0x060006BE RID: 1726 RVA: 0x0001DA50 File Offset: 0x0001BC50
		[DefaultValue(true)]
		[MWFCategory("Window Style")]
		public bool ControlBox
		{
			get
			{
				return this.control_box;
			}
			set
			{
				if (this.control_box != value)
				{
					this.control_box = value;
					base.UpdateStyles();
				}
			}
		}

		/// <summary>Gets or sets the dialog result for the form.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.DialogResult" /> that represents the result of the form when used as a dialog box.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value specified is outside the range of valid values. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x060006BF RID: 1727 RVA: 0x0001DA68 File Offset: 0x0001BC68
		// (set) Token: 0x060006C0 RID: 1728 RVA: 0x0001DA70 File Offset: 0x0001BC70
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public DialogResult DialogResult
		{
			get
			{
				return this.dialog_result;
			}
			set
			{
				if (value < DialogResult.None || value > DialogResult.No)
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(DialogResult));
				}
				this.dialog_result = value;
				if (this.dialog_result != DialogResult.None && this.is_modal)
				{
					this.RaiseCloseEvents(false, false);
				}
			}
		}

		/// <summary>Gets or sets the border style of the form.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.FormBorderStyle" /> that represents the style of border to display for the form. The default is FormBorderStyle.Sizable.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value specified is outside the range of valid values. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x060006C1 RID: 1729 RVA: 0x0001DAB0 File Offset: 0x0001BCB0
		// (set) Token: 0x060006C2 RID: 1730 RVA: 0x0001DAB8 File Offset: 0x0001BCB8
		[DefaultValue(FormBorderStyle.Sizable)]
		[DispId(-504)]
		[MWFCategory("Appearance")]
		public FormBorderStyle FormBorderStyle
		{
			get
			{
				return this.form_border_style;
			}
			set
			{
				this.form_border_style = value;
				if (this.window_manager == null)
				{
					if (base.IsHandleCreated)
					{
						XplatUI.SetBorderStyle(this.window.Handle, this.form_border_style);
					}
				}
				else
				{
					this.window_manager.UpdateBorderStyle(value);
				}
				Size clientSize = this.ClientSize;
				base.UpdateStyles();
				if (base.IsHandleCreated)
				{
					this.Size = base.InternalSizeFromClientSize(clientSize);
					XplatUI.InvalidateNC(base.Handle);
					return;
				}
				if (this.is_clientsize_set)
				{
					this.Size = base.InternalSizeFromClientSize(clientSize);
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether a Help button should be displayed in the caption box of the form.</summary>
		/// <returns>true to display a Help button in the form's caption bar; otherwise, false. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x060006C3 RID: 1731 RVA: 0x0001DB43 File Offset: 0x0001BD43
		[DefaultValue(false)]
		[MWFCategory("Window Style")]
		public bool HelpButton
		{
			get
			{
				return this.help_button;
			}
		}

		/// <summary>Gets or sets the icon for the form.</summary>
		/// <returns>An <see cref="T:System.Drawing.Icon" /> that represents the icon for the form.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x060006C4 RID: 1732 RVA: 0x0001DB4B File Offset: 0x0001BD4B
		[Localizable(true)]
		[AmbientValue(null)]
		[MWFCategory("Window Style")]
		public Icon Icon
		{
			get
			{
				return this.icon;
			}
		}

		/// <summary>Gets a value indicating whether the form is a multiple-document interface (MDI) child form.</summary>
		/// <returns>true if the form is an MDI child form; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x060006C5 RID: 1733 RVA: 0x0001DB53 File Offset: 0x0001BD53
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool IsMdiChild
		{
			get
			{
				return this.mdi_parent != null;
			}
		}

		/// <summary>Gets or sets a value indicating whether the form is a container for multiple-document interface (MDI) child forms.</summary>
		/// <returns>true if the form is a container for MDI child forms; otherwise, false. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x060006C6 RID: 1734 RVA: 0x0001DB5E File Offset: 0x0001BD5E
		[DefaultValue(false)]
		[MWFCategory("Window Style")]
		public bool IsMdiContainer
		{
			get
			{
				return this.mdi_container != null;
			}
		}

		/// <summary>Gets the currently active multiple-document interface (MDI) child window.</summary>
		/// <returns>Returns a <see cref="T:System.Windows.Forms.Form" /> that represents the currently active MDI child window, or null if there are currently no child windows present.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x060006C7 RID: 1735 RVA: 0x0001DB69 File Offset: 0x0001BD69
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Form ActiveMdiChild
		{
			get
			{
				if (!this.IsMdiContainer)
				{
					return null;
				}
				return this.mdi_container.ActiveMdiChild;
			}
		}

		/// <summary>Gets or sets the primary menu container for the form.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.MenuStrip" /> that represents the container for the menu structure of the form. The default is null.</returns>
		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x060006C8 RID: 1736 RVA: 0x0001DB80 File Offset: 0x0001BD80
		[DefaultValue(null)]
		[TypeConverter(typeof(ReferenceConverter))]
		public MenuStrip MainMenuStrip
		{
			get
			{
				return this.main_menu_strip;
			}
		}

		/// <summary>Gets or sets a value indicating whether the Maximize button is displayed in the caption bar of the form.</summary>
		/// <returns>true to display a Maximize button for the form; otherwise, false. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001AA RID: 426
		// (get) Token: 0x060006C9 RID: 1737 RVA: 0x0001DB88 File Offset: 0x0001BD88
		// (set) Token: 0x060006CA RID: 1738 RVA: 0x0001DB90 File Offset: 0x0001BD90
		[DefaultValue(true)]
		[MWFCategory("Window Style")]
		public bool MaximizeBox
		{
			get
			{
				return this.maximize_box;
			}
			set
			{
				if (this.maximize_box != value)
				{
					this.maximize_box = value;
					base.UpdateStyles();
				}
			}
		}

		/// <summary>Gets the maximum size the form can be resized to.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that represents the maximum size for the form.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The values of the height or width within the <see cref="T:System.Drawing.Size" /> object are less than zero. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001AB RID: 427
		// (get) Token: 0x060006CB RID: 1739 RVA: 0x0001DBA8 File Offset: 0x0001BDA8
		[DefaultValue(typeof(Size), "0, 0")]
		[Localizable(true)]
		[RefreshProperties(RefreshProperties.Repaint)]
		[MWFCategory("Layout")]
		public override Size MaximumSize
		{
			get
			{
				return this.maximum_size;
			}
		}

		/// <summary>Gets an array of forms that represent the multiple-document interface (MDI) child forms that are parented to this form.</summary>
		/// <returns>An array of <see cref="T:System.Windows.Forms.Form" /> objects, each of which identifies one of this form's MDI child forms.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001AC RID: 428
		// (get) Token: 0x060006CC RID: 1740 RVA: 0x0001DBB0 File Offset: 0x0001BDB0
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Form[] MdiChildren
		{
			get
			{
				if (this.mdi_container != null)
				{
					return this.mdi_container.MdiChildren;
				}
				return new Form[0];
			}
		}

		/// <summary>Gets or sets the current multiple-document interface (MDI) parent form of this form.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Form" /> that represents the MDI parent form.</returns>
		/// <exception cref="T:System.Exception">The <see cref="T:System.Windows.Forms.Form" /> assigned to this property is not marked as an MDI container.-or- The <see cref="T:System.Windows.Forms.Form" /> assigned to this property is both a child and an MDI container form.-or- The <see cref="T:System.Windows.Forms.Form" /> assigned to this property is located on a different thread. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001AD RID: 429
		// (get) Token: 0x060006CD RID: 1741 RVA: 0x0001DBCC File Offset: 0x0001BDCC
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Form MdiParent
		{
			get
			{
				return this.mdi_parent;
			}
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x060006CE RID: 1742 RVA: 0x0001DBD4 File Offset: 0x0001BDD4
		internal MdiClient MdiContainer
		{
			get
			{
				return this.mdi_container;
			}
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x060006CF RID: 1743 RVA: 0x0001DBDC File Offset: 0x0001BDDC
		internal InternalWindowManager WindowManager
		{
			get
			{
				return this.window_manager;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.MainMenu" /> that is displayed in the form.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.MainMenu" /> that represents the menu to display in the form.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x060006D0 RID: 1744 RVA: 0x0001DBE4 File Offset: 0x0001BDE4
		[Browsable(false)]
		[TypeConverter(typeof(ReferenceConverter))]
		[DefaultValue(null)]
		[MWFCategory("Window Style")]
		public MainMenu Menu
		{
			get
			{
				return this.menu;
			}
		}

		/// <summary>Gets the merged menu for the form.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.MainMenu" /> that represents the merged menu of the form.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x060006D1 RID: 1745 RVA: 0x0001DBEC File Offset: 0x0001BDEC
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public MainMenu MergedMenu
		{
			get
			{
				if (!this.IsMdiChild || this.window_manager == null)
				{
					return null;
				}
				return ((MdiWindowManager)this.window_manager).MergedMenu;
			}
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x060006D2 RID: 1746 RVA: 0x0001DC10 File Offset: 0x0001BE10
		internal MainMenu ActiveMenu
		{
			get
			{
				if (this.IsMdiChild)
				{
					return null;
				}
				if (this.IsMdiContainer && this.mdi_container.Controls.Count > 0 && ((Form)this.mdi_container.Controls[0]).WindowState == FormWindowState.Maximized)
				{
					return ((MdiWindowManager)((Form)this.mdi_container.Controls[0]).WindowManager).MaximizedMenu;
				}
				Form activeMdiChild = this.ActiveMdiChild;
				if (activeMdiChild == null || activeMdiChild.Menu == null)
				{
					return this.menu;
				}
				return activeMdiChild.MergedMenu;
			}
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x060006D3 RID: 1747 RVA: 0x0001DCA8 File Offset: 0x0001BEA8
		internal MdiWindowManager ActiveMaximizedMdiChild
		{
			get
			{
				Form activeMdiChild = this.ActiveMdiChild;
				if (activeMdiChild == null)
				{
					return null;
				}
				if (activeMdiChild.WindowManager == null || activeMdiChild.window_state != FormWindowState.Maximized)
				{
					return null;
				}
				return (MdiWindowManager)activeMdiChild.WindowManager;
			}
		}

		/// <summary>Gets or sets a value indicating whether the Minimize button is displayed in the caption bar of the form.</summary>
		/// <returns>true to display a Minimize button for the form; otherwise, false. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x060006D4 RID: 1748 RVA: 0x0001DCDF File Offset: 0x0001BEDF
		// (set) Token: 0x060006D5 RID: 1749 RVA: 0x0001DCE7 File Offset: 0x0001BEE7
		[DefaultValue(true)]
		[MWFCategory("Window Style")]
		public bool MinimizeBox
		{
			get
			{
				return this.minimize_box;
			}
			set
			{
				if (this.minimize_box != value)
				{
					this.minimize_box = value;
					base.UpdateStyles();
				}
			}
		}

		/// <summary>Gets or sets the minimum size the form can be resized to.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that represents the minimum size for the form.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The values of the height or width within the <see cref="T:System.Drawing.Size" /> object are less than zero. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x060006D6 RID: 1750 RVA: 0x0001DCFF File Offset: 0x0001BEFF
		// (set) Token: 0x060006D7 RID: 1751 RVA: 0x0001DD08 File Offset: 0x0001BF08
		[Localizable(true)]
		[RefreshProperties(RefreshProperties.Repaint)]
		[MWFCategory("Layout")]
		public override Size MinimumSize
		{
			get
			{
				return this.minimum_size;
			}
			set
			{
				if (this.minimum_size != value)
				{
					this.minimum_size = value;
					if (!this.maximum_size.IsEmpty)
					{
						if (this.minimum_size.Width >= this.maximum_size.Width)
						{
							this.maximum_size.Width = this.minimum_size.Width;
						}
						if (this.minimum_size.Height >= this.maximum_size.Height)
						{
							this.maximum_size.Height = this.minimum_size.Height;
						}
					}
					if (this.Size.Width < value.Width || this.Size.Height < value.Height)
					{
						this.Size = new Size(Math.Max(this.Size.Width, value.Width), Math.Max(this.Size.Height, value.Height));
					}
					this.OnMinimumSizeChanged(EventArgs.Empty);
					if (base.IsHandleCreated)
					{
						XplatUI.SetWindowMinMax(base.Handle, this.maximized_bounds, this.minimum_size, this.maximum_size);
					}
				}
			}
		}

		/// <summary>Gets a value indicating whether this form is displayed modally.</summary>
		/// <returns>true if the form is displayed modally; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x060006D8 RID: 1752 RVA: 0x0001DE33 File Offset: 0x0001C033
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool Modal
		{
			get
			{
				return this.is_modal;
			}
		}

		/// <summary>Gets or sets the form that owns this form.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Form" /> that represents the form that is the owner of this form.</returns>
		/// <exception cref="T:System.Exception">A top-level window cannot have an owner. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x060006D9 RID: 1753 RVA: 0x0001DE3B File Offset: 0x0001C03B
		// (set) Token: 0x060006DA RID: 1754 RVA: 0x0001DE44 File Offset: 0x0001C044
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Form Owner
		{
			get
			{
				return this.owner;
			}
			set
			{
				if (this.owner != value)
				{
					if (this.owner != null)
					{
						this.owner.RemoveOwnedForm(this);
					}
					this.owner = value;
					if (this.owner != null)
					{
						this.owner.AddOwnedForm(this);
					}
					if (base.IsHandleCreated)
					{
						if (this.owner != null && this.owner.IsHandleCreated)
						{
							XplatUI.SetOwner(this.window.Handle, this.owner.window.Handle);
							return;
						}
						XplatUI.SetOwner(this.window.Handle, IntPtr.Zero);
					}
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether an icon is displayed in the caption bar of the form.</summary>
		/// <returns>true if the form displays an icon in the caption bar; otherwise, false. The default is true.</returns>
		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x060006DB RID: 1755 RVA: 0x0001DEE1 File Offset: 0x0001C0E1
		[DefaultValue(true)]
		public bool ShowIcon
		{
			get
			{
				return this.show_icon;
			}
		}

		/// <summary>Gets or sets a value indicating whether the form is displayed in the Windows taskbar.</summary>
		/// <returns>true to display the form in the Windows taskbar at run time; otherwise, false. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x060006DC RID: 1756 RVA: 0x0001DEE9 File Offset: 0x0001C0E9
		// (set) Token: 0x060006DD RID: 1757 RVA: 0x0001DEF1 File Offset: 0x0001C0F1
		[DefaultValue(true)]
		[MWFCategory("Window Style")]
		public bool ShowInTaskbar
		{
			get
			{
				return this.show_in_taskbar;
			}
			set
			{
				if (this.show_in_taskbar != value)
				{
					this.show_in_taskbar = value;
					if (base.IsHandleCreated)
					{
						base.RecreateHandle();
					}
					base.UpdateStyles();
				}
			}
		}

		/// <summary>Gets or sets the size of the form.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that represents the size of the form.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001BA RID: 442
		// (get) Token: 0x060006DE RID: 1758 RVA: 0x0001DF17 File Offset: 0x0001C117
		// (set) Token: 0x060006DF RID: 1759 RVA: 0x0001DF1F File Offset: 0x0001C11F
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Localizable(false)]
		public new Size Size
		{
			get
			{
				return base.Size;
			}
			set
			{
				base.Size = value;
			}
		}

		/// <summary>Gets or sets the style of the size grip to display in the lower-right corner of the form.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.SizeGripStyle" /> that represents the style of the size grip to display. The default is <see cref="F:System.Windows.Forms.SizeGripStyle.Auto" /></returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value specified is outside the range of valid values. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001BB RID: 443
		// (set) Token: 0x060006E0 RID: 1760 RVA: 0x0001DF28 File Offset: 0x0001C128
		[DefaultValue(SizeGripStyle.Auto)]
		[MWFCategory("Window Style")]
		public SizeGripStyle SizeGripStyle
		{
			set
			{
				this.size_grip_style = value;
				this.UpdateSizeGripVisible();
			}
		}

		/// <summary>Gets or sets the starting position of the form at run time.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.FormStartPosition" /> that represents the starting position of the form.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value specified is outside the range of valid values. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001BC RID: 444
		// (get) Token: 0x060006E1 RID: 1761 RVA: 0x0001DF37 File Offset: 0x0001C137
		// (set) Token: 0x060006E2 RID: 1762 RVA: 0x0001DF3F File Offset: 0x0001C13F
		[DefaultValue(FormStartPosition.WindowsDefaultLocation)]
		[Localizable(true)]
		[MWFCategory("Layout")]
		public FormStartPosition StartPosition
		{
			get
			{
				return this.start_position;
			}
			set
			{
				this.start_position = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether to display the form as a top-level window.</summary>
		/// <returns>true to display the form as a top-level window; otherwise, false. The default is true.</returns>
		/// <exception cref="T:System.Exception">A Multiple-document interface (MDI) parent form must be a top-level window. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001BD RID: 445
		// (get) Token: 0x060006E3 RID: 1763 RVA: 0x0001DF48 File Offset: 0x0001C148
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool TopLevel
		{
			get
			{
				return base.GetTopLevel();
			}
		}

		/// <summary>Gets or sets a value indicating whether the form should be displayed as a topmost form.</summary>
		/// <returns>true to display the form as a topmost form; otherwise, false. The default is false.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001BE RID: 446
		// (get) Token: 0x060006E4 RID: 1764 RVA: 0x0001DF50 File Offset: 0x0001C150
		// (set) Token: 0x060006E5 RID: 1765 RVA: 0x0001DF58 File Offset: 0x0001C158
		[DefaultValue(false)]
		[MWFCategory("Window Style")]
		public bool TopMost
		{
			get
			{
				return this.topmost;
			}
			set
			{
				if (this.topmost != value)
				{
					this.topmost = value;
					if (base.IsHandleCreated)
					{
						XplatUI.SetTopmost(this.window.Handle, value);
					}
					this.OnUIATopMostChanged();
				}
			}
		}

		/// <summary>Gets or sets the color that will represent transparent areas of the form.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the color to display transparently on the form.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001BF RID: 447
		// (get) Token: 0x060006E6 RID: 1766 RVA: 0x0001DF8A File Offset: 0x0001C18A
		[MWFCategory("Window Style")]
		public Color TransparencyKey
		{
			get
			{
				return this.transparency_key;
			}
		}

		/// <summary>Gets or sets a value that indicates whether form is minimized, maximized, or normal.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.FormWindowState" /> that represents whether form is minimized, maximized, or normal. The default is FormWindowState.Normal.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value specified is outside the range of valid values. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x060006E7 RID: 1767 RVA: 0x0001DF94 File Offset: 0x0001C194
		// (set) Token: 0x060006E8 RID: 1768 RVA: 0x0001DFE4 File Offset: 0x0001C1E4
		[DefaultValue(FormWindowState.Normal)]
		[MWFCategory("Layout")]
		public FormWindowState WindowState
		{
			get
			{
				if (base.IsHandleCreated && this.shown_raised)
				{
					if (this.window_manager != null)
					{
						return this.window_manager.GetWindowState();
					}
					FormWindowState windowState = XplatUI.GetWindowState(base.Handle);
					if (windowState != (FormWindowState)(-1))
					{
						this.window_state = windowState;
					}
				}
				return this.window_state;
			}
			set
			{
				FormWindowState formWindowState = this.window_state;
				this.window_state = value;
				if (base.IsHandleCreated && this.shown_raised)
				{
					if (this.window_manager != null)
					{
						this.window_manager.SetWindowState(formWindowState, value);
						return;
					}
					XplatUI.SetWindowState(base.Handle, value);
				}
				if (formWindowState != this.window_state)
				{
					this.OnUIAWindowStateChanged();
				}
			}
		}

		/// <returns>A <see cref="T:System.Windows.Forms.CreateParams" /> that contains the required creation parameters when the handle to the control is created.</returns>
		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x060006E9 RID: 1769 RVA: 0x0001E040 File Offset: 0x0001C240
		protected override CreateParams CreateParams
		{
			get
			{
				CreateParams createParams = new CreateParams();
				if (this.Text != null)
				{
					createParams.Caption = this.Text.Replace(Environment.NewLine, string.Empty);
				}
				createParams.ClassName = XplatUI.GetDefaultClassName(base.GetType());
				createParams.ClassStyle = 0;
				createParams.Style = 0;
				createParams.ExStyle = 0;
				createParams.Param = 0;
				createParams.Parent = IntPtr.Zero;
				createParams.menu = this.ActiveMenu;
				createParams.control = this;
				if ((base.Parent != null || !this.TopLevel) && !this.IsMdiChild)
				{
					createParams.X = base.Left;
					createParams.Y = base.Top;
				}
				else
				{
					switch (this.start_position)
					{
					case FormStartPosition.Manual:
						createParams.X = base.Left;
						createParams.Y = base.Top;
						break;
					case FormStartPosition.CenterScreen:
						if (this.IsMdiChild)
						{
							createParams.X = Math.Max((this.MdiParent.mdi_container.ClientSize.Width - base.Width) / 2, 0);
							createParams.Y = Math.Max((this.MdiParent.mdi_container.ClientSize.Height - base.Height) / 2, 0);
						}
						else
						{
							createParams.X = Math.Max((Screen.PrimaryScreen.WorkingArea.Width - base.Width) / 2, 0);
							createParams.Y = Math.Max((Screen.PrimaryScreen.WorkingArea.Height - base.Height) / 2, 0);
						}
						break;
					case FormStartPosition.WindowsDefaultLocation:
					case FormStartPosition.WindowsDefaultBounds:
					case FormStartPosition.CenterParent:
						createParams.X = int.MinValue;
						createParams.Y = int.MinValue;
						break;
					}
				}
				createParams.Width = base.Width;
				createParams.Height = base.Height;
				createParams.Style = 33554432;
				if (!this.Modal)
				{
					createParams.WindowStyle |= WindowStyles.WS_CLIPSIBLINGS;
				}
				if (base.Parent != null && base.Parent.IsHandleCreated)
				{
					createParams.Parent = base.Parent.Handle;
					createParams.Style |= 1073741824;
				}
				if (this.IsMdiChild)
				{
					createParams.Style |= 1086324736;
					if (base.Parent != null)
					{
						createParams.Parent = base.Parent.Handle;
					}
					createParams.ExStyle |= 320;
					FormBorderStyle formBorderStyle = this.FormBorderStyle;
					if (formBorderStyle != FormBorderStyle.None)
					{
						if (formBorderStyle - FormBorderStyle.FixedToolWindow <= 1)
						{
							createParams.ExStyle |= 128;
						}
						createParams.Style |= 13565952;
					}
				}
				else
				{
					switch (this.FormBorderStyle)
					{
					case FormBorderStyle.FixedSingle:
						createParams.Style |= 12582912;
						break;
					case FormBorderStyle.Fixed3D:
						createParams.Style |= 12582912;
						createParams.ExStyle |= 512;
						break;
					case FormBorderStyle.FixedDialog:
						createParams.Style |= 12582912;
						createParams.ExStyle |= 65537;
						break;
					case FormBorderStyle.Sizable:
						createParams.Style |= 12845056;
						break;
					case FormBorderStyle.FixedToolWindow:
						createParams.Style |= 12582912;
						createParams.ExStyle |= 128;
						break;
					case FormBorderStyle.SizableToolWindow:
						createParams.Style |= 12845056;
						createParams.ExStyle |= 128;
						break;
					}
				}
				FormWindowState formWindowState = this.window_state;
				if (formWindowState != FormWindowState.Minimized)
				{
					if (formWindowState == FormWindowState.Maximized)
					{
						createParams.Style |= 16777216;
					}
				}
				else
				{
					createParams.Style |= 536870912;
				}
				if (this.TopMost)
				{
					createParams.ExStyle |= 8;
				}
				if (this.ShowInTaskbar)
				{
					createParams.ExStyle |= 262144;
				}
				if (this.MaximizeBox)
				{
					createParams.Style |= 65536;
				}
				if (this.MinimizeBox)
				{
					createParams.Style |= 131072;
				}
				if (this.ControlBox)
				{
					createParams.Style |= 524288;
				}
				if (!this.show_icon)
				{
					createParams.ExStyle |= 1;
				}
				createParams.ExStyle |= 65536;
				if (this.HelpButton && !this.MaximizeBox && !this.MinimizeBox)
				{
					createParams.ExStyle |= 1024;
				}
				int platform = (int)Environment.OSVersion.Platform;
				bool flag = platform == 128 || platform == 4 || platform == 6;
				if ((base.VisibleInternal && (this.is_changing_visible_state == 0 || flag)) || base.IsRecreating)
				{
					createParams.Style |= 268435456;
				}
				if (this.opacity < 1.0 || this.TransparencyKey != Color.Empty)
				{
					createParams.ExStyle |= 524288;
				}
				if (!this.is_enabled && this.context == null)
				{
					createParams.Style |= 134217728;
				}
				if (!this.ControlBox && this.Text == string.Empty)
				{
					createParams.WindowStyle &= ~WindowStyles.WS_DLGFRAME;
				}
				return createParams;
			}
		}

		/// <returns>The default <see cref="T:System.Drawing.Size" /> of the control.</returns>
		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x060006EA RID: 1770 RVA: 0x0001E5D0 File Offset: 0x0001C7D0
		protected override Size DefaultSize
		{
			get
			{
				return new Size(300, 300);
			}
		}

		/// <summary>Gets a value indicating whether the window will be activated when it is shown.</summary>
		/// <returns>True if the window will not be activated when it is shown; otherwise, false. The default is false.</returns>
		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x060006EB RID: 1771 RVA: 0x00002D70 File Offset: 0x00000F70
		[Browsable(false)]
		[MonoTODO("Implemented for Win32, needs X11 implementation")]
		protected virtual bool ShowWithoutActivation
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets the size when autoscaling the form based on a specified font.</summary>
		/// <returns>A <see cref="T:System.Drawing.SizeF" /> representing the autoscaled size of the form.</returns>
		/// <param name="font">A <see cref="T:System.Drawing.Font" /> representing the font to determine the autoscaled base size of the form. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060006EC RID: 1772 RVA: 0x0001E5E1 File Offset: 0x0001C7E1
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("This method has been deprecated.  Use AutoScaleDimensions instead")]
		public static SizeF GetAutoScaleSize(Font font)
		{
			return XplatUI.GetAutoScaleSize(font);
		}

		/// <summary>Activates the form and gives it focus.</summary>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060006ED RID: 1773 RVA: 0x0001E5EC File Offset: 0x0001C7EC
		public void Activate()
		{
			if (base.IsHandleCreated)
			{
				if (this.IsMdiChild)
				{
					this.MdiParent.ActivateMdiChild(this);
					return;
				}
				if (this.IsMdiContainer)
				{
					this.mdi_container.SendFocusToActiveChild();
					return;
				}
				XplatUI.Activate(this.window.Handle);
			}
		}

		/// <summary>Adds an owned form to this form.</summary>
		/// <param name="ownedForm">The <see cref="T:System.Windows.Forms.Form" /> that this form will own. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060006EE RID: 1774 RVA: 0x0001E63A File Offset: 0x0001C83A
		public void AddOwnedForm(Form ownedForm)
		{
			if (!this.owned_forms.Contains(ownedForm))
			{
				this.owned_forms.Add(ownedForm);
			}
			ownedForm.Owner = this;
		}

		/// <summary>Closes the form.</summary>
		/// <exception cref="T:System.InvalidOperationException">The form was closed while a handle was being created. </exception>
		/// <exception cref="T:System.ObjectDisposedException">You cannot call this method from the <see cref="E:System.Windows.Forms.Form.Activated" /> event when <see cref="P:System.Windows.Forms.Form.WindowState" /> is set to <see cref="F:System.Windows.Forms.FormWindowState.Maximized" />.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060006EF RID: 1775 RVA: 0x0001E660 File Offset: 0x0001C860
		public void Close()
		{
			if (base.IsDisposed)
			{
				return;
			}
			if (!base.IsHandleCreated)
			{
				base.Dispose();
				return;
			}
			if (this.Menu != null)
			{
				XplatUI.SetMenu(this.window.Handle, null);
			}
			XplatUI.SendMessage(base.Handle, Msg.WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
			this.closed = true;
		}

		/// <summary>Removes an owned form from this form.</summary>
		/// <param name="ownedForm">A <see cref="T:System.Windows.Forms.Form" /> representing the form to remove from the list of owned forms for this form. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060006F0 RID: 1776 RVA: 0x0001E6BD File Offset: 0x0001C8BD
		public void RemoveOwnedForm(Form ownedForm)
		{
			this.owned_forms.Remove(ownedForm);
		}

		/// <summary>Shows the form as a modal dialog box.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DialogResult" /> values.</returns>
		/// <exception cref="T:System.InvalidOperationException">The form being shown is already visible.-or- The form being shown is disabled.-or- The form being shown is not a top-level window.-or- The form being shown as a dialog box is already a modal form.-or-The current process is not running in user interactive mode (for more information, see <see cref="P:System.Windows.Forms.SystemInformation.UserInteractive" />).</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060006F1 RID: 1777 RVA: 0x0001E6CB File Offset: 0x0001C8CB
		public DialogResult ShowDialog()
		{
			return this.ShowDialog(null);
		}

		/// <summary>Shows the form as a modal dialog box with the specified owner.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DialogResult" /> values.</returns>
		/// <param name="owner">Any object that implements <see cref="T:System.Windows.Forms.IWin32Window" /> that represents the top-level window that will own the modal dialog box. </param>
		/// <exception cref="T:System.ArgumentException">The form specified in the <paramref name="owner" /> parameter is the same as the form being shown.</exception>
		/// <exception cref="T:System.InvalidOperationException">The form being shown is already visible.-or- The form being shown is disabled.-or- The form being shown is not a top-level window.-or- The form being shown as a dialog box is already a modal form.-or-The current process is not running in user interactive mode (for more information, see <see cref="P:System.Windows.Forms.SystemInformation.UserInteractive" />).</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060006F2 RID: 1778 RVA: 0x0001E6D4 File Offset: 0x0001C8D4
		public DialogResult ShowDialog(IWin32Window owner)
		{
			IWin32Window win32Window = owner;
			Form form = null;
			if (owner == null && Application.MWFThread.Current.Context != null)
			{
				IntPtr active = XplatUI.GetActive();
				if (active != IntPtr.Zero)
				{
					owner = Control.FromHandle(active) as Form;
				}
			}
			if (owner != null)
			{
				Control control = Control.FromHandle(owner.Handle);
				if (control != null)
				{
					form = control.TopLevelControl as Form;
				}
			}
			if (form == this)
			{
				if (win32Window != null)
				{
					throw new ArgumentException("Forms cannot own themselves or their owners.", "owner");
				}
				owner = null;
				form = null;
			}
			if (this.is_modal)
			{
				throw new InvalidOperationException("The form is already displayed as a modal dialog.");
			}
			if (base.Visible)
			{
				throw new InvalidOperationException("Forms that are already  visible cannot be displayed as a modal dialog. Set the form's visible property to false before calling ShowDialog.");
			}
			if (!base.Enabled)
			{
				throw new InvalidOperationException("Forms that are not enabled cannot be displayed as a modal dialog. Set the form's enabled property to true before calling ShowDialog.");
			}
			if (base.TopLevelControl != this)
			{
				throw new InvalidOperationException("Forms that are not top level forms cannot be displayed as a modal dialog. Remove the form from any parent form before calling ShowDialog.");
			}
			if (form != null)
			{
				this.owner = form;
			}
			if (this.owner != null && this.owner.TopMost)
			{
				this.TopMost = true;
			}
			IntPtr intPtr;
			bool flag;
			Rectangle rectangle;
			XplatUI.GrabInfo(out intPtr, out flag, out rectangle);
			if (intPtr != IntPtr.Zero)
			{
				XplatUI.UngrabWindow(intPtr);
			}
			List<Form> list = new List<Form>();
			foreach (object obj in Application.OpenForms)
			{
				Form form2 = (Form)obj;
				if (form2.Enabled)
				{
					list.Add(form2);
				}
			}
			foreach (Form form3 in list)
			{
				this.disabled_by_showdialog.Add(form3);
				form3.Enabled = false;
			}
			Form.modal_dialogs.Add(this);
			Application.RunLoop(true, new ApplicationContext(this));
			if (this.owner != null)
			{
				XplatUI.Activate(this.owner.window.Handle);
			}
			if (base.IsHandleCreated)
			{
				this.DestroyHandle();
			}
			if (this.DialogResult == DialogResult.None)
			{
				this.DialogResult = DialogResult.Cancel;
			}
			return this.DialogResult;
		}

		/// <summary>Gets a string representing the current instance of the form.</summary>
		/// <returns>A string consisting of the fully qualified name of the form object's class, with the <see cref="P:System.Windows.Forms.Form.Text" /> property of the form appended to the end. For example, if the form is derived from the class MyForm in the MyNamespace namespace, and the <see cref="P:System.Windows.Forms.Form.Text" /> property is set to Hello, World, this method will return MyNamespace.MyForm, Text: Hello, World.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060006F3 RID: 1779 RVA: 0x0001E8FC File Offset: 0x0001CAFC
		public override string ToString()
		{
			return base.GetType().FullName + ", Text: " + this.Text;
		}

		/// <returns>true if all of the children validated successfully; otherwise, false. If called from the <see cref="E:System.Windows.Forms.Control.Validating" /> or <see cref="E:System.Windows.Forms.Control.Validated" /> event handlers, this method will always return false.</returns>
		// Token: 0x060006F4 RID: 1780 RVA: 0x0001E919 File Offset: 0x0001CB19
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		public override bool ValidateChildren()
		{
			return base.ValidateChildren();
		}

		/// <returns>true if all of the children validated successfully; otherwise, false. If called from the <see cref="E:System.Windows.Forms.Control.Validating" /> or <see cref="E:System.Windows.Forms.Control.Validated" /> event handlers, this method will always return false.</returns>
		/// <param name="validationConstraints">Places restrictions on which controls have their <see cref="E:System.Windows.Forms.Control.Validating" /> event raised.</param>
		// Token: 0x060006F5 RID: 1781 RVA: 0x0001E921 File Offset: 0x0001CB21
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		public override bool ValidateChildren(ValidationConstraints validationConstraints)
		{
			return base.ValidateChildren(validationConstraints);
		}

		/// <summary>Activates the MDI child of a form.</summary>
		/// <param name="form">The child form to activate.</param>
		// Token: 0x060006F6 RID: 1782 RVA: 0x0001E92A File Offset: 0x0001CB2A
		protected void ActivateMdiChild(Form form)
		{
			if (!this.IsMdiContainer)
			{
				return;
			}
			this.mdi_container.ActivateChild(form);
			this.OnMdiChildActivate(EventArgs.Empty);
		}

		/// <summary>Adjusts the scroll bars on the container based on the current control positions and the control currently selected. </summary>
		/// <param name="displayScrollbars">true to show the scroll bars; otherwise, false. </param>
		// Token: 0x060006F7 RID: 1783 RVA: 0x0001E94C File Offset: 0x0001CB4C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void AdjustFormScrollbars(bool displayScrollbars)
		{
			base.AdjustFormScrollbars(displayScrollbars);
		}

		/// <summary>Resizes the form according to the current value of the <see cref="P:System.Windows.Forms.Form.AutoScaleBaseSize" /> property and the size of the current font.</summary>
		// Token: 0x060006F8 RID: 1784 RVA: 0x0001E958 File Offset: 0x0001CB58
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("This method has been deprecated")]
		protected void ApplyAutoScaling()
		{
			SizeF autoScaleSize = Form.GetAutoScaleSize(this.Font);
			Size size = new Size((int)Math.Round((double)autoScaleSize.Width), (int)Math.Round((double)autoScaleSize.Height));
			if (size == this.autoscale_base_size)
			{
				return;
			}
			if (Environment.GetEnvironmentVariable("MONO_MWF_SCALING") == "disable")
			{
				return;
			}
			float num;
			if (size.Width != this.AutoScaleBaseSize.Width)
			{
				num = (float)size.Width / (float)this.AutoScaleBaseSize.Width + 0.08f;
			}
			else
			{
				num = 1f;
			}
			float num2;
			if (size.Height != this.AutoScaleBaseSize.Height)
			{
				num2 = (float)size.Height / (float)this.AutoScaleBaseSize.Height + 0.08f;
			}
			else
			{
				num2 = 1f;
			}
			base.Scale(num, num2);
			this.AutoScaleBaseSize = size;
		}

		/// <summary>Centers the position of the form within the bounds of the parent form.</summary>
		// Token: 0x060006F9 RID: 1785 RVA: 0x0001EA4C File Offset: 0x0001CC4C
		protected void CenterToParent()
		{
			if (this.TopLevel && !base.IsHandleCreated)
			{
				this.CreateHandle();
			}
			int num;
			if (base.Width > 0)
			{
				num = base.Width;
			}
			else
			{
				num = this.DefaultSize.Width;
			}
			int num2;
			if (base.Height > 0)
			{
				num2 = base.Height;
			}
			else
			{
				num2 = this.DefaultSize.Height;
			}
			Control control = null;
			if (base.Parent != null)
			{
				control = base.Parent;
			}
			else if (this.owner != null)
			{
				control = this.owner;
			}
			if (this.owner != null)
			{
				this.Location = new Point(control.Left + control.Width / 2 - num / 2, control.Top + control.Height / 2 - num2 / 2);
			}
		}

		/// <summary>Centers the form on the current screen.</summary>
		// Token: 0x060006FA RID: 1786 RVA: 0x0001EB0C File Offset: 0x0001CD0C
		protected void CenterToScreen()
		{
			if (this.TopLevel && !base.IsHandleCreated)
			{
				this.CreateHandle();
			}
			int num;
			if (base.Width > 0)
			{
				num = base.Width;
			}
			else
			{
				num = this.DefaultSize.Width;
			}
			int num2;
			if (base.Height > 0)
			{
				num2 = base.Height;
			}
			else
			{
				num2 = this.DefaultSize.Height;
			}
			Rectangle rectangle;
			if (this.Owner == null)
			{
				rectangle = Screen.FromPoint(Control.MousePosition).WorkingArea;
			}
			else
			{
				rectangle = Screen.FromControl(this.Owner).WorkingArea;
			}
			this.Location = new Point(rectangle.Left + rectangle.Width / 2 - num / 2, rectangle.Top + rectangle.Height / 2 - num2 / 2);
		}

		/// <returns>A new instance of <see cref="T:System.Windows.Forms.Control.ControlCollection" /> assigned to the control.</returns>
		// Token: 0x060006FB RID: 1787 RVA: 0x0001EBD1 File Offset: 0x0001CDD1
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override Control.ControlCollection CreateControlsInstance()
		{
			return base.CreateControlsInstance();
		}

		/// <summary>Creates the handle for the form. If a derived class overrides this function, it must call the base implementation.</summary>
		/// <exception cref="T:System.InvalidOperationException">A handle for this <see cref="T:System.Windows.Forms.Form" /> has already been created.</exception>
		// Token: 0x060006FC RID: 1788 RVA: 0x0001EBDC File Offset: 0x0001CDDC
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void CreateHandle()
		{
			base.CreateHandle();
			if (!base.IsHandleCreated)
			{
				return;
			}
			base.UpdateBounds();
			if ((XplatUI.SupportsTransparency() & TransparencySupport.Set) != TransparencySupport.None && this.allow_transparency)
			{
				XplatUI.SetWindowTransparency(base.Handle, this.opacity, this.TransparencyKey);
			}
			XplatUI.SetWindowMinMax(this.window.Handle, this.maximized_bounds, this.minimum_size, this.maximum_size);
			if (this.show_icon && this.FormBorderStyle != FormBorderStyle.FixedDialog && this.icon != null)
			{
				XplatUI.SetIcon(this.window.Handle, this.icon);
			}
			if (this.owner != null && this.owner.IsHandleCreated)
			{
				XplatUI.SetOwner(this.window.Handle, this.owner.window.Handle);
			}
			if (this.topmost)
			{
				XplatUI.SetTopmost(this.window.Handle, this.topmost);
			}
			for (int i = 0; i < this.owned_forms.Count; i++)
			{
				if (this.owned_forms[i].IsHandleCreated)
				{
					XplatUI.SetOwner(this.owned_forms[i].window.Handle, this.window.Handle);
				}
			}
			if (this.window_manager != null)
			{
				if (this.IsMdiChild && base.VisibleInternal)
				{
					MdiWindowManager mdiWindowManager;
					if (this.MdiParent != null)
					{
						foreach (Form form in this.MdiParent.MdiChildren)
						{
							mdiWindowManager = form.window_manager as MdiWindowManager;
							if (mdiWindowManager != null && form != this)
							{
								mdiWindowManager.RaiseDeactivate();
							}
						}
					}
					mdiWindowManager = this.window_manager as MdiWindowManager;
					mdiWindowManager.RaiseActivated();
					if (this.MdiParent != null)
					{
						foreach (Form form2 in this.MdiParent.MdiChildren)
						{
							if (form2 != this && form2.IsHandleCreated)
							{
								XplatUI.InvalidateNC(form2.Handle);
							}
						}
					}
				}
				if (this.window_state != FormWindowState.Normal)
				{
					this.window_manager.SetWindowState((FormWindowState)2147483647, this.window_state);
				}
				XplatUI.RequestNCRecalc(this.window.Handle);
			}
		}

		/// <param name="m">The Windows <see cref="T:System.Windows.Forms.Message" /> to process. </param>
		// Token: 0x060006FD RID: 1789 RVA: 0x0001EE00 File Offset: 0x0001D000
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void DefWndProc(ref Message m)
		{
			base.DefWndProc(ref m);
		}

		/// <summary>Disposes of the resources (other than memory) used by the <see cref="T:System.Windows.Forms.Form" />.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x060006FE RID: 1790 RVA: 0x0001EE0C File Offset: 0x0001D00C
		protected override void Dispose(bool disposing)
		{
			if (this.owned_forms != null)
			{
				for (int i = 0; i < this.owned_forms.Count; i++)
				{
					((Form)this.owned_forms[i]).Owner = null;
				}
				this.owned_forms.Clear();
			}
			this.Owner = null;
			base.Dispose(disposing);
			Application.RemoveForm(this);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Form.Activated" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060006FF RID: 1791 RVA: 0x0001EE70 File Offset: 0x0001D070
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnActivated(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Form.ActivatedEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Form.Closed" /> event.</summary>
		/// <param name="e">The <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000700 RID: 1792 RVA: 0x0001EEA0 File Offset: 0x0001D0A0
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnClosed(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Form.ClosedEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Form.Closing" /> event.</summary>
		/// <param name="e">A <see cref="T:System.ComponentModel.CancelEventArgs" /> that contains the event data. </param>
		// Token: 0x06000701 RID: 1793 RVA: 0x0001EED0 File Offset: 0x0001D0D0
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnClosing(CancelEventArgs e)
		{
			CancelEventHandler cancelEventHandler = (CancelEventHandler)base.Events[Form.ClosingEvent];
			if (cancelEventHandler != null)
			{
				cancelEventHandler(this, e);
			}
		}

		/// <summary>Raises the CreateControl event.</summary>
		// Token: 0x06000702 RID: 1794 RVA: 0x0001EEFE File Offset: 0x0001D0FE
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnCreateControl()
		{
			base.OnCreateControl();
			if (this.menu != null)
			{
				XplatUI.SetMenu(this.window.Handle, this.menu);
			}
			this.OnLoadInternal(EventArgs.Empty);
			this.OnLocationChanged(EventArgs.Empty);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Form.Deactivate" /> event.</summary>
		/// <param name="e">The <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000703 RID: 1795 RVA: 0x0001EF3C File Offset: 0x0001D13C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnDeactivate(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Form.DeactivateEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06000704 RID: 1796 RVA: 0x0001EF6C File Offset: 0x0001D16C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnFontChanged(EventArgs e)
		{
			base.OnFontChanged(e);
			if (!this.autoscale_base_size_set)
			{
				SizeF autoScaleSize = Form.GetAutoScaleSize(this.Font);
				this.autoscale_base_size = new Size((int)Math.Round((double)autoScaleSize.Width), (int)Math.Round((double)autoScaleSize.Height));
			}
		}

		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000705 RID: 1797 RVA: 0x0001EFBB File Offset: 0x0001D1BB
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnHandleCreated(EventArgs e)
		{
			XplatUI.SetBorderStyle(this.window.Handle, this.form_border_style);
			base.OnHandleCreated(e);
		}

		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000706 RID: 1798 RVA: 0x0001EFDA File Offset: 0x0001D1DA
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnHandleDestroyed(EventArgs e)
		{
			Application.RemoveForm(this);
			base.OnHandleDestroyed(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Form.Load" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000707 RID: 1799 RVA: 0x0001EFEC File Offset: 0x0001D1EC
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnLoad(EventArgs e)
		{
			Application.AddForm(this);
			EventHandler eventHandler = (EventHandler)base.Events[Form.LoadEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Form.MdiChildActivate" /> event.</summary>
		/// <param name="e">The <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000708 RID: 1800 RVA: 0x0001F020 File Offset: 0x0001D220
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnMdiChildActivate(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Form.MdiChildActivateEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Form.MenuComplete" /> event.</summary>
		/// <param name="e">The <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000709 RID: 1801 RVA: 0x0001F050 File Offset: 0x0001D250
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected internal virtual void OnMenuComplete(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Form.MenuCompleteEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Form.MinimumSizeChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x0600070A RID: 1802 RVA: 0x0001F080 File Offset: 0x0001D280
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnMinimumSizeChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Form.MinimumSizeChangedEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data. </param>
		// Token: 0x0600070B RID: 1803 RVA: 0x0001F0AE File Offset: 0x0001D2AE
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			if (this.size_grip != null)
			{
				this.size_grip.HandlePaint(this, e);
			}
		}

		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x0600070C RID: 1804 RVA: 0x0001F0CC File Offset: 0x0001D2CC
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
		}

		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x0600070D RID: 1805 RVA: 0x0001F0D5 File Offset: 0x0001D2D5
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnStyleChanged(EventArgs e)
		{
			base.OnStyleChanged(e);
		}

		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x0600070E RID: 1806 RVA: 0x0001F0DE File Offset: 0x0001D2DE
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnTextChanged(EventArgs e)
		{
			base.OnTextChanged(e);
			if (this.mdi_container != null)
			{
				this.mdi_container.SetParentText(true);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.VisibleChanged" /> event.</summary>
		/// <param name="e">The <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x0600070F RID: 1807 RVA: 0x0001F0FC File Offset: 0x0001D2FC
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnVisibleChanged(EventArgs e)
		{
			base.OnVisibleChanged(e);
			if (base.Visible && this.window_manager != null)
			{
				if (this.WindowState == FormWindowState.Normal)
				{
					this.window_manager.SetWindowState(this.WindowState, this.WindowState);
					return;
				}
				this.window_manager.SetWindowState((FormWindowState)(-1), this.WindowState);
			}
		}

		/// <summary>Processes a command key. </summary>
		/// <returns>true if the keystroke was processed and consumed by the control; otherwise, false to allow further processing.</returns>
		/// <param name="msg">A <see cref="T:System.Windows.Forms.Message" />, passed by reference, that represents the Win32 message to process. </param>
		/// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values that represents the key to process. </param>
		// Token: 0x06000710 RID: 1808 RVA: 0x0001F154 File Offset: 0x0001D354
		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			if (base.ProcessCmdKey(ref msg, keyData))
			{
				return true;
			}
			if ((keyData & Keys.Alt) != Keys.None)
			{
				Control topLevelControl = base.TopLevelControl;
				if (topLevelControl != null)
				{
					IntPtr intPtr = Control.MakeParam(2, 2);
					XplatUI.SendMessage(topLevelControl.Handle, Msg.WM_CHANGEUISTATE, intPtr, IntPtr.Zero);
				}
			}
			if (this.ActiveMenu != null && this.ActiveMenu.ProcessCmdKey(ref msg, keyData))
			{
				return true;
			}
			if (base.ActiveTracker != null && base.ActiveTracker.TopMenu is ContextMenu)
			{
				ContextMenu contextMenu = base.ActiveTracker.TopMenu as ContextMenu;
				if (contextMenu.SourceControl != this && contextMenu.ProcessCmdKey(ref msg, keyData))
				{
					return true;
				}
			}
			if (this.IsMdiChild)
			{
				if (keyData <= (Keys.LButton | Keys.Back | Keys.Shift | Keys.Control))
				{
					if (keyData <= (Keys)131187)
					{
						if (keyData != (Keys.LButton | Keys.Back | Keys.Control))
						{
							if (keyData != (Keys)131187)
							{
								return false;
							}
							goto IL_0105;
						}
					}
					else if (keyData != (Keys)131189)
					{
						if (keyData != (Keys.LButton | Keys.Back | Keys.Shift | Keys.Control))
						{
							return false;
						}
						goto IL_011F;
					}
					this.MdiParent.MdiContainer.ActivateNextChild();
					return true;
				}
				if (keyData <= (Keys)196725)
				{
					if (keyData != (Keys)196723)
					{
						if (keyData != (Keys)196725)
						{
							return false;
						}
						goto IL_011F;
					}
				}
				else
				{
					if (keyData != (Keys)262253 && keyData != (Keys.LButton | Keys.MButton | Keys.Back | Keys.ShiftKey | Keys.Space | Keys.F17 | Keys.Alt))
					{
						return false;
					}
					(this.WindowManager as MdiWindowManager).ShowPopup(Point.Empty);
					return true;
				}
				IL_0105:
				this.Close();
				return true;
				IL_011F:
				this.MdiParent.MdiContainer.ActivatePreviousChild();
				return true;
			}
			return false;
		}

		/// <summary>Processes a dialog character.</summary>
		/// <returns>true if the character was processed by the control; otherwise, false.</returns>
		/// <param name="charCode">The character to process. </param>
		// Token: 0x06000711 RID: 1809 RVA: 0x0001F2AA File Offset: 0x0001D4AA
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override bool ProcessDialogChar(char charCode)
		{
			return base.ProcessDialogChar(charCode);
		}

		/// <summary>Processes a dialog box key. </summary>
		/// <returns>true if the keystroke was processed and consumed by the control; otherwise, false to allow further processing.</returns>
		/// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values that represents the key to process. </param>
		// Token: 0x06000712 RID: 1810 RVA: 0x0001F2B4 File Offset: 0x0001D4B4
		protected override bool ProcessDialogKey(Keys keyData)
		{
			if ((keyData & Keys.Modifiers) == Keys.None)
			{
				if (keyData == Keys.Return)
				{
					Control control = Control.FromHandle(XplatUI.GetFocus());
					if (control is Button && control.FindForm() == this)
					{
						((Button)control).PerformClick();
						return true;
					}
					if (this.accept_button != null)
					{
						base.ActiveControl = this.accept_button as Control;
						if (base.ActiveControl == this.accept_button)
						{
							this.accept_button.PerformClick();
						}
						return true;
					}
				}
				else if (keyData == Keys.Escape && this.cancel_button != null)
				{
					this.cancel_button.PerformClick();
					return true;
				}
			}
			return base.ProcessDialogKey(keyData);
		}

		/// <returns>true if the message was processed by the control; otherwise, false.</returns>
		/// <param name="m">A <see cref="T:System.Windows.Forms.Message" />, passed by reference, that represents the window message to process. </param>
		// Token: 0x06000713 RID: 1811 RVA: 0x0001F34D File Offset: 0x0001D54D
		protected override bool ProcessKeyPreview(ref Message m)
		{
			return (this.key_preview && this.ProcessKeyEventArgs(ref m)) || base.ProcessKeyPreview(ref m);
		}

		/// <returns>true if a control is selected; otherwise, false.</returns>
		/// <param name="forward">true to cycle forward through the controls in the <see cref="T:System.Windows.Forms.ContainerControl" />; otherwise, false. </param>
		// Token: 0x06000714 RID: 1812 RVA: 0x0001F36C File Offset: 0x0001D56C
		protected override bool ProcessTabKey(bool forward)
		{
			bool flag = !this.show_focus_cues;
			this.show_focus_cues = true;
			bool flag2 = base.SelectNextControl(base.ActiveControl, forward, true, true, true);
			if (flag && base.ActiveControl != null)
			{
				base.ActiveControl.Invalidate();
			}
			return flag2;
		}

		/// <summary>Performs scaling of the form.</summary>
		/// <param name="x">Percentage to scale the form horizontally </param>
		/// <param name="y">Percentage to scale the form vertically </param>
		// Token: 0x06000715 RID: 1813 RVA: 0x0001F3B0 File Offset: 0x0001D5B0
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected override void ScaleCore(float x, float y)
		{
			base.ScaleCore(x, y);
		}

		/// <summary>Selects this form, and optionally selects the next or previous control.</summary>
		/// <param name="directed">If set to true that the active control is changed </param>
		/// <param name="forward">If directed is true, then this controls the direction in which focus is moved. If this is true, then the next control is selected; otherwise, the previous control is selected. </param>
		// Token: 0x06000716 RID: 1814 RVA: 0x0001F3BC File Offset: 0x0001D5BC
		protected override void Select(bool directed, bool forward)
		{
			if (!base.IsHandleCreated && !base.IsHandleCreated)
			{
				this.CreateHandle();
			}
			if (directed)
			{
				base.SelectNextControl(null, forward, true, true, true);
			}
			Form parentForm = base.ParentForm;
			if (parentForm != null)
			{
				parentForm.ActiveControl = this;
			}
			this.Activate();
		}

		/// <param name="x">The x-coordinate.</param>
		/// <param name="y">The y-coordinate.</param>
		/// <param name="width">The bounds width.</param>
		/// <param name="height">The bounds height.</param>
		/// <param name="specified">A value from the BoundsSpecified enumeration.</param>
		// Token: 0x06000717 RID: 1815 RVA: 0x0001F408 File Offset: 0x0001D608
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
		{
			Size size;
			if (this.WindowState == FormWindowState.Minimized)
			{
				size = SystemInformation.MinimizedWindowSize;
			}
			else
			{
				FormBorderStyle formBorderStyle = this.FormBorderStyle;
				if (formBorderStyle != FormBorderStyle.None)
				{
					if (formBorderStyle != FormBorderStyle.FixedToolWindow)
					{
						if (formBorderStyle != FormBorderStyle.SizableToolWindow)
						{
							size = SystemInformation.MinimumWindowSize;
						}
						else
						{
							size = XplatUI.MinimumSizeableToolWindowSize;
						}
					}
					else
					{
						size = XplatUI.MinimumFixedToolWindowSize;
					}
				}
				else
				{
					size = XplatUI.MinimumNoBorderWindowSize;
				}
			}
			if ((specified & BoundsSpecified.Width) == BoundsSpecified.Width)
			{
				width = Math.Max(width, size.Width);
			}
			if ((specified & BoundsSpecified.Height) == BoundsSpecified.Height)
			{
				height = Math.Max(height, size.Height);
			}
			base.SetBoundsCore(x, y, width, height, specified);
			int num = (((specified & BoundsSpecified.X) == BoundsSpecified.X) ? x : this.restore_bounds.X);
			int num2 = (((specified & BoundsSpecified.Y) == BoundsSpecified.Y) ? y : this.restore_bounds.Y);
			int num3 = (((specified & BoundsSpecified.Width) == BoundsSpecified.Width) ? width : this.restore_bounds.Width);
			int num4 = (((specified & BoundsSpecified.Height) == BoundsSpecified.Height) ? height : this.restore_bounds.Height);
			this.restore_bounds = new Rectangle(num, num2, num3, num4);
		}

		/// <summary>Sets the client size of the form. This will adjust the bounds of the form to make the client size the requested size.</summary>
		/// <param name="x">Requested width of the client region. </param>
		/// <param name="y">Requested height of the client region.</param>
		// Token: 0x06000718 RID: 1816 RVA: 0x0001F500 File Offset: 0x0001D700
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void SetClientSizeCore(int x, int y)
		{
			if (this.minimum_size.Width != 0 && x < this.minimum_size.Width)
			{
				x = this.minimum_size.Width;
			}
			else if (this.maximum_size.Width != 0 && x > this.maximum_size.Width)
			{
				x = this.maximum_size.Width;
			}
			if (this.minimum_size.Height != 0 && y < this.minimum_size.Height)
			{
				y = this.minimum_size.Height;
			}
			else if (this.maximum_size.Height != 0 && y > this.maximum_size.Height)
			{
				y = this.maximum_size.Height;
			}
			Rectangle rectangle = new Rectangle(0, 0, x, y);
			CreateParams createParams = this.CreateParams;
			this.clientsize_set = new Size(x, y);
			Rectangle rectangle2;
			if (XplatUI.CalculateWindowRect(ref rectangle, createParams, createParams.menu, out rectangle2))
			{
				base.SetBounds(this.bounds.X, this.bounds.Y, rectangle2.Width, rectangle2.Height, BoundsSpecified.Size);
			}
		}

		/// <param name="value">true to make the control visible; otherwise, false. </param>
		// Token: 0x06000719 RID: 1817 RVA: 0x0001F610 File Offset: 0x0001D810
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void SetVisibleCore(bool value)
		{
			if (value)
			{
				this.close_raised = false;
			}
			if (this.IsMdiChild && !this.MdiParent.Visible)
			{
				if (value != base.Visible)
				{
					((MdiWindowManager)this.window_manager).IsVisiblePending = value;
					this.OnVisibleChanged(EventArgs.Empty);
					return;
				}
			}
			else
			{
				this.is_changing_visible_state++;
				this.has_been_visible = value || this.has_been_visible;
				base.SetVisibleCore(value);
				if (value)
				{
					Application.AddForm(this);
				}
				if (value && this.WindowState != FormWindowState.Normal)
				{
					XplatUI.SendMessage(base.Handle, Msg.WM_SHOWWINDOW, (IntPtr)1, IntPtr.Zero);
				}
				this.is_changing_visible_state--;
			}
			if (value && this.IsMdiContainer)
			{
				foreach (Form form in this.MdiChildren)
				{
					MdiWindowManager mdiWindowManager = (MdiWindowManager)form.window_manager;
					if (!form.IsHandleCreated && mdiWindowManager.IsVisiblePending)
					{
						mdiWindowManager.IsVisiblePending = false;
						form.Visible = true;
					}
				}
			}
			if (value && this.IsMdiChild)
			{
				base.PerformLayout();
				ThemeEngine.Current.ManagedWindowSetButtonLocations(this.window_manager);
			}
			if (value && !this.shown_raised)
			{
				this.OnShown(EventArgs.Empty);
				this.shown_raised = true;
			}
			if (value && !this.IsMdiChild)
			{
				if (base.ActiveControl == null)
				{
					base.SelectNextControl(null, true, true, true, false);
				}
				if (base.ActiveControl != null)
				{
					base.SendControlFocus(base.ActiveControl);
					return;
				}
				base.Focus();
			}
		}

		/// <param name="m">The Windows <see cref="T:System.Windows.Forms.Message" /> to process. </param>
		// Token: 0x0600071A RID: 1818 RVA: 0x0001F78C File Offset: 0x0001D98C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void WndProc(ref Message m)
		{
			if (this.window_manager != null && this.window_manager.WndProc(ref m))
			{
				return;
			}
			Msg msg = (Msg)m.Msg;
			if (msg <= Msg.WM_NCPAINT)
			{
				if (msg <= Msg.WM_CLOSE)
				{
					switch (msg)
					{
					case Msg.WM_DESTROY:
						this.WmDestroy(ref m);
						return;
					case Msg.WM_MOVE:
					case (Msg)4:
					case Msg.WM_SIZE:
						break;
					case Msg.WM_ACTIVATE:
						this.WmActivate(ref m);
						return;
					case Msg.WM_SETFOCUS:
						this.WmSetFocus(ref m);
						return;
					case Msg.WM_KILLFOCUS:
						this.WmKillFocus(ref m);
						return;
					default:
						if (msg == Msg.WM_CLOSE)
						{
							this.WmClose(ref m);
							return;
						}
						break;
					}
				}
				else
				{
					if (msg == Msg.WM_GETMINMAXINFO)
					{
						this.WmGetMinMaxInfo(ref m);
						return;
					}
					if (msg == Msg.WM_WINDOWPOSCHANGED)
					{
						this.WmWindowPosChanged(ref m);
						return;
					}
					switch (msg)
					{
					case Msg.WM_NCCALCSIZE:
						this.WmNcCalcSize(ref m);
						return;
					case Msg.WM_NCHITTEST:
						this.WmNcHitTest(ref m);
						return;
					case Msg.WM_NCPAINT:
						this.WmNcPaint(ref m);
						return;
					}
				}
			}
			else if (msg <= Msg.WM_SYSCOMMAND)
			{
				switch (msg)
				{
				case Msg.WM_NCMOUSEMOVE:
					this.WmNcMouseMove(ref m);
					return;
				case Msg.WM_NCLBUTTONDOWN:
					this.WmNcLButtonDown(ref m);
					return;
				case Msg.WM_NCLBUTTONUP:
					this.WmNcLButtonUp(ref m);
					return;
				default:
					if (msg == Msg.WM_SYSCOMMAND)
					{
						this.WmSysCommand(ref m);
						return;
					}
					break;
				}
			}
			else
			{
				if (msg == Msg.WM_ENTERSIZEMOVE)
				{
					this.OnResizeBegin(EventArgs.Empty);
					return;
				}
				if (msg == Msg.WM_EXITSIZEMOVE)
				{
					this.OnResizeEnd(EventArgs.Empty);
					return;
				}
				if (msg == Msg.WM_NCMOUSELEAVE)
				{
					this.WmNcMouseLeave(ref m);
					return;
				}
			}
			base.WndProc(ref m);
		}

		// Token: 0x0600071B RID: 1819 RVA: 0x0001F906 File Offset: 0x0001DB06
		private void WmDestroy(ref Message m)
		{
			if (!base.RecreatingHandle)
			{
				this.closing = true;
			}
			base.WndProc(ref m);
		}

		// Token: 0x0600071C RID: 1820 RVA: 0x0001F920 File Offset: 0x0001DB20
		internal bool RaiseCloseEvents(bool last_check, bool cancel)
		{
			if (last_check && base.Visible)
			{
				base.Hide();
			}
			if (this.close_raised || (last_check && this.closed))
			{
				return false;
			}
			this.close_raised = true;
			bool flag = this.FireClosingEvents(CloseReason.UserClosing, cancel);
			if (!flag)
			{
				if (!last_check || this.DialogResult != DialogResult.None)
				{
					if (this.mdi_container != null)
					{
						Form[] mdiChildren = this.mdi_container.MdiChildren;
						for (int i = 0; i < mdiChildren.Length; i++)
						{
							mdiChildren[i].FireClosedEvents(CloseReason.UserClosing);
						}
					}
					this.FireClosedEvents(CloseReason.UserClosing);
				}
				this.closing = true;
				this.shown_raised = false;
			}
			else
			{
				this.DialogResult = DialogResult.None;
				this.closing = false;
				this.close_raised = false;
			}
			return flag;
		}

		// Token: 0x0600071D RID: 1821 RVA: 0x0001F9CC File Offset: 0x0001DBCC
		private void WmClose(ref Message m)
		{
			if (!base.Enabled)
			{
				return;
			}
			Form activeForm = Form.ActiveForm;
			if (activeForm != null && activeForm != this && activeForm.Modal)
			{
				Control control = this;
				while (control != null && control.Parent != activeForm)
				{
					control = control.Parent;
				}
				if (control == null || control.Parent != activeForm)
				{
					return;
				}
			}
			bool flag = false;
			if (this.mdi_container != null)
			{
				Form[] mdiChildren = this.mdi_container.MdiChildren;
				for (int i = 0; i < mdiChildren.Length; i++)
				{
					flag = mdiChildren[i].FireClosingEvents(CloseReason.MdiFormClosing, flag);
				}
			}
			bool flag2 = false;
			if (!this.suppress_closing_events)
			{
				flag2 = !this.ValidateChildren();
			}
			if (this.suppress_closing_events || !this.RaiseCloseEvents(false, flag2 || flag))
			{
				if (this.is_modal)
				{
					base.Hide();
				}
				else
				{
					base.Dispose();
					if (activeForm != null && activeForm != this)
					{
						activeForm.SelectActiveControl();
					}
				}
				this.mdi_parent = null;
				return;
			}
			if (this.is_modal)
			{
				this.DialogResult = DialogResult.None;
			}
			this.closing = false;
		}

		// Token: 0x0600071E RID: 1822 RVA: 0x0001FABC File Offset: 0x0001DCBC
		private void WmWindowPosChanged(ref Message m)
		{
			if (this.window_state != FormWindowState.Minimized && this.WindowState != FormWindowState.Minimized)
			{
				base.WndProc(ref m);
			}
			else if (!this.is_minimizing)
			{
				this.is_minimizing = true;
				this.OnSizeChanged(EventArgs.Empty);
				this.is_minimizing = false;
			}
			if (this.WindowState == FormWindowState.Normal)
			{
				this.restore_bounds = base.Bounds;
			}
		}

		// Token: 0x0600071F RID: 1823 RVA: 0x0001FB19 File Offset: 0x0001DD19
		private void WmSysCommand(ref Message m)
		{
			if (XplatUI.IsEnabled(base.Handle))
			{
				ToolStripManager.FireAppClicked();
			}
			base.WndProc(ref m);
		}

		// Token: 0x06000720 RID: 1824 RVA: 0x0001FB34 File Offset: 0x0001DD34
		private void WmActivate(ref Message m)
		{
			if (!base.Enabled && Form.modal_dialogs.Count > 0)
			{
				(Form.modal_dialogs[Form.modal_dialogs.Count - 1] as Form).Activate();
				return;
			}
			if (m.WParam != (IntPtr)0)
			{
				if (this.is_loaded)
				{
					this.SelectActiveControl();
					if (base.ActiveControl != null && !base.ActiveControl.Focused)
					{
						base.SendControlFocus(base.ActiveControl);
					}
				}
				this.IsActive = true;
				return;
			}
			if (XplatUI.IsEnabled(base.Handle) && XplatUI.GetParent(m.LParam) != base.Handle)
			{
				ToolStripManager.FireAppFocusChanged(this);
			}
			this.IsActive = false;
		}

		// Token: 0x06000721 RID: 1825 RVA: 0x0001FBF3 File Offset: 0x0001DDF3
		private void WmKillFocus(ref Message m)
		{
			base.WndProc(ref m);
		}

		// Token: 0x06000722 RID: 1826 RVA: 0x0001FBFC File Offset: 0x0001DDFC
		private void WmSetFocus(ref Message m)
		{
			if (base.ActiveControl != null && base.ActiveControl != this)
			{
				base.ActiveControl.Focus();
				return;
			}
			if (this.IsMdiContainer)
			{
				this.mdi_container.SendFocusToActiveChild();
				return;
			}
			base.WndProc(ref m);
		}

		// Token: 0x06000723 RID: 1827 RVA: 0x0001FC38 File Offset: 0x0001DE38
		private void WmNcHitTest(ref Message m)
		{
			if (XplatUI.IsEnabled(base.Handle) && this.ActiveMenu != null)
			{
				int num = Control.LowOrder(m.LParam.ToInt32());
				int num2 = Control.HighOrder((long)m.LParam.ToInt32());
				XplatUI.ScreenToMenu(this.ActiveMenu.Wnd.window.Handle, ref num, ref num2);
				if (num > 0 && num2 > 0 && num < this.ActiveMenu.Rect.Width && num2 < this.ActiveMenu.Rect.Height)
				{
					m.Result = new IntPtr(5);
					return;
				}
			}
			base.WndProc(ref m);
		}

		// Token: 0x06000724 RID: 1828 RVA: 0x0001FCF0 File Offset: 0x0001DEF0
		private void WmNcLButtonDown(ref Message m)
		{
			if (XplatUI.IsEnabled(base.Handle) && this.ActiveMenu != null)
			{
				this.ActiveMenu.OnMouseDown(this, new MouseEventArgs(Control.FromParamToMouseButtons((long)m.WParam.ToInt32()), this.mouse_clicks, Control.MousePosition.X, Control.MousePosition.Y, 0));
			}
			if (this.ActiveMaximizedMdiChild != null && this.ActiveMenu != null && this.ActiveMaximizedMdiChild.HandleMenuMouseDown(this.ActiveMenu, Control.LowOrder(m.LParam.ToInt32()), Control.HighOrder((long)m.LParam.ToInt32())))
			{
				return;
			}
			base.WndProc(ref m);
		}

		// Token: 0x06000725 RID: 1829 RVA: 0x0001FDAC File Offset: 0x0001DFAC
		private void WmNcLButtonUp(ref Message m)
		{
			if (this.ActiveMaximizedMdiChild != null && this.ActiveMenu != null)
			{
				this.ActiveMaximizedMdiChild.HandleMenuMouseUp(this.ActiveMenu, Control.LowOrder(m.LParam.ToInt32()), Control.HighOrder((long)m.LParam.ToInt32()));
			}
			base.WndProc(ref m);
		}

		// Token: 0x06000726 RID: 1830 RVA: 0x0001FE08 File Offset: 0x0001E008
		private void WmNcMouseLeave(ref Message m)
		{
			if (this.ActiveMaximizedMdiChild != null && this.ActiveMenu != null)
			{
				this.ActiveMaximizedMdiChild.HandleMenuMouseLeave(this.ActiveMenu, Control.LowOrder(m.LParam.ToInt32()), Control.HighOrder((long)m.LParam.ToInt32()));
			}
			base.WndProc(ref m);
		}

		// Token: 0x06000727 RID: 1831 RVA: 0x0001FE64 File Offset: 0x0001E064
		private void WmNcMouseMove(ref Message m)
		{
			if (XplatUI.IsEnabled(base.Handle) && this.ActiveMenu != null)
			{
				this.ActiveMenu.OnMouseMove(this, new MouseEventArgs(Control.FromParamToMouseButtons((long)m.WParam.ToInt32()), this.mouse_clicks, Control.LowOrder(m.LParam.ToInt32()), Control.HighOrder((long)m.LParam.ToInt32()), 0));
			}
			if (this.ActiveMaximizedMdiChild != null && this.ActiveMenu != null)
			{
				XplatUI.RequestAdditionalWM_NCMessages(base.Handle, false, true);
				this.ActiveMaximizedMdiChild.HandleMenuMouseMove(this.ActiveMenu, Control.LowOrder(m.LParam.ToInt32()), Control.HighOrder((long)m.LParam.ToInt32()));
			}
			base.WndProc(ref m);
		}

		// Token: 0x06000728 RID: 1832 RVA: 0x0001FF38 File Offset: 0x0001E138
		private void WmNcPaint(ref Message m)
		{
			if (this.ActiveMenu != null)
			{
				PaintEventArgs paintEventArgs = XplatUI.PaintEventStart(ref m, base.Handle, false);
				Point menuOrigin = XplatUI.GetMenuOrigin(this.window.Handle);
				Rectangle rectangle = new Rectangle(menuOrigin.X, menuOrigin.Y, this.ClientSize.Width, 0);
				rectangle = Rectangle.Union(rectangle, paintEventArgs.ClipRectangle);
				paintEventArgs.SetClip(rectangle);
				paintEventArgs.Graphics.SetClip(rectangle);
				this.ActiveMenu.Draw(paintEventArgs, new Rectangle(menuOrigin.X, menuOrigin.Y, this.ClientSize.Width, 0));
				if (this.ActiveMaximizedMdiChild != null)
				{
					this.ActiveMaximizedMdiChild.DrawMaximizedButtons(this.ActiveMenu, paintEventArgs);
				}
				XplatUI.PaintEventEnd(ref m, base.Handle, false);
			}
			base.WndProc(ref m);
		}

		// Token: 0x06000729 RID: 1833 RVA: 0x00020010 File Offset: 0x0001E210
		private void WmNcCalcSize(ref Message m)
		{
			if (this.ActiveMenu != null && m.WParam == (IntPtr)1)
			{
				XplatUIWin32.NCCALCSIZE_PARAMS nccalcsize_PARAMS = (XplatUIWin32.NCCALCSIZE_PARAMS)Marshal.PtrToStructure(m.LParam, typeof(XplatUIWin32.NCCALCSIZE_PARAMS));
				nccalcsize_PARAMS.rgrc1.top = nccalcsize_PARAMS.rgrc1.top + ThemeEngine.Current.CalcMenuBarSize(base.DeviceContext, this.ActiveMenu, this.ClientSize.Width);
				Marshal.StructureToPtr<XplatUIWin32.NCCALCSIZE_PARAMS>(nccalcsize_PARAMS, m.LParam, true);
			}
			this.DefWndProc(ref m);
		}

		// Token: 0x0600072A RID: 1834 RVA: 0x0002009C File Offset: 0x0001E29C
		private void WmGetMinMaxInfo(ref Message m)
		{
			if (m.LParam != IntPtr.Zero)
			{
				MINMAXINFO minmaxinfo = (MINMAXINFO)Marshal.PtrToStructure(m.LParam, typeof(MINMAXINFO));
				this.default_maximized_bounds = new Rectangle(minmaxinfo.ptMaxPosition.x, minmaxinfo.ptMaxPosition.y, minmaxinfo.ptMaxSize.x, minmaxinfo.ptMaxSize.y);
				if (this.maximized_bounds != Rectangle.Empty)
				{
					minmaxinfo.ptMaxPosition.x = this.maximized_bounds.Left;
					minmaxinfo.ptMaxPosition.y = this.maximized_bounds.Top;
					minmaxinfo.ptMaxSize.x = this.maximized_bounds.Width;
					minmaxinfo.ptMaxSize.y = this.maximized_bounds.Height;
				}
				if (this.minimum_size != Size.Empty)
				{
					minmaxinfo.ptMinTrackSize.x = this.minimum_size.Width;
					minmaxinfo.ptMinTrackSize.y = this.minimum_size.Height;
				}
				if (this.maximum_size != Size.Empty)
				{
					minmaxinfo.ptMaxTrackSize.x = this.maximum_size.Width;
					minmaxinfo.ptMaxTrackSize.y = this.maximum_size.Height;
				}
				Marshal.StructureToPtr<MINMAXINFO>(minmaxinfo, m.LParam, false);
			}
		}

		// Token: 0x0600072B RID: 1835 RVA: 0x0000493C File Offset: 0x00002B3C
		internal override void FireEnter()
		{
		}

		// Token: 0x0600072C RID: 1836 RVA: 0x0000493C File Offset: 0x00002B3C
		internal override void FireLeave()
		{
		}

		// Token: 0x0600072D RID: 1837 RVA: 0x0002020B File Offset: 0x0001E40B
		internal void RemoveWindowManager()
		{
			this.window_manager = null;
		}

		// Token: 0x0600072E RID: 1838 RVA: 0x00020214 File Offset: 0x0001E414
		internal override void CheckAcceptButton()
		{
			if (this.accept_button != null)
			{
				Button button = this.accept_button as Button;
				if (base.ActiveControl == button)
				{
					return;
				}
				if (button == null)
				{
					return;
				}
				if (base.ActiveControl is Button)
				{
					button.paint_as_acceptbutton = false;
				}
				else
				{
					button.paint_as_acceptbutton = true;
				}
				button.Invalidate();
			}
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x0600072F RID: 1839 RVA: 0x00020266 File Offset: 0x0001E466
		internal override bool ActivateOnShow
		{
			get
			{
				return !this.ShowWithoutActivation;
			}
		}

		// Token: 0x06000730 RID: 1840 RVA: 0x00020274 File Offset: 0x0001E474
		private void OnLoadInternal(EventArgs e)
		{
			if (this.AutoScale)
			{
				this.ApplyAutoScaling();
				this.AutoScale = false;
			}
			if (!base.IsDisposed)
			{
				base.OnSizeInitializedOrChanged();
				try
				{
					this.OnLoad(e);
				}
				catch (Exception ex)
				{
					Application.OnThreadException(ex);
				}
				if (!base.IsDisposed)
				{
					this.is_visible = true;
				}
			}
			if (!this.IsMdiChild && !base.IsDisposed)
			{
				switch (this.StartPosition)
				{
				case FormStartPosition.Manual:
					base.Left = this.CreateParams.X;
					base.Top = this.CreateParams.Y;
					break;
				case FormStartPosition.CenterScreen:
					this.CenterToScreen();
					break;
				case FormStartPosition.CenterParent:
					this.CenterToParent();
					break;
				}
			}
			this.is_loaded = true;
		}

		/// <summary>Occurs when the form is closed. </summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400002E RID: 46
		// (add) Token: 0x06000731 RID: 1841 RVA: 0x00020340 File Offset: 0x0001E540
		// (remove) Token: 0x06000732 RID: 1842 RVA: 0x00020353 File Offset: 0x0001E553
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public event EventHandler Closed
		{
			add
			{
				base.Events.AddHandler(Form.ClosedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Form.ClosedEvent, value);
			}
		}

		/// <summary>Occurs when the form loses focus and is no longer the active form.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400002F RID: 47
		// (add) Token: 0x06000733 RID: 1843 RVA: 0x00020366 File Offset: 0x0001E566
		// (remove) Token: 0x06000734 RID: 1844 RVA: 0x00020379 File Offset: 0x0001E579
		public event EventHandler Deactivate
		{
			add
			{
				base.Events.AddHandler(Form.DeactivateEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Form.DeactivateEvent, value);
			}
		}

		/// <summary>Occurs before a form is displayed for the first time.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000030 RID: 48
		// (add) Token: 0x06000735 RID: 1845 RVA: 0x0002038C File Offset: 0x0001E58C
		// (remove) Token: 0x06000736 RID: 1846 RVA: 0x0002039F File Offset: 0x0001E59F
		public event EventHandler Load
		{
			add
			{
				base.Events.AddHandler(Form.LoadEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Form.LoadEvent, value);
			}
		}

		/// <returns>The text associated with this control.</returns>
		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x06000737 RID: 1847 RVA: 0x000043B4 File Offset: 0x000025B4
		// (set) Token: 0x06000738 RID: 1848 RVA: 0x000043BC File Offset: 0x000025BC
		[SettingsBindable(true)]
		public override string Text
		{
			get
			{
				return base.Text;
			}
			set
			{
				base.Text = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Drawing.Point" /> that represents the upper-left corner of the <see cref="T:System.Windows.Forms.Form" /> in screen coordinates.</summary>
		/// <returns>The <see cref="T:System.Drawing.Point" /> that represents the upper-left corner of the <see cref="T:System.Windows.Forms.Form" /> in screen coordinates.</returns>
		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x06000739 RID: 1849 RVA: 0x000203B2 File Offset: 0x0001E5B2
		// (set) Token: 0x0600073A RID: 1850 RVA: 0x000203BA File Offset: 0x0001E5BA
		[SettingsBindable(true)]
		public new Point Location
		{
			get
			{
				return base.Location;
			}
			set
			{
				base.Location = value;
			}
		}

		/// <summary>Occurs after the form is closed.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000031 RID: 49
		// (add) Token: 0x0600073B RID: 1851 RVA: 0x000203C3 File Offset: 0x0001E5C3
		// (remove) Token: 0x0600073C RID: 1852 RVA: 0x000203D6 File Offset: 0x0001E5D6
		public event FormClosedEventHandler FormClosed
		{
			add
			{
				base.Events.AddHandler(Form.FormClosedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Form.FormClosedEvent, value);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.BackgroundImageLayoutChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x0600073D RID: 1853 RVA: 0x000203E9 File Offset: 0x0001E5E9
		protected override void OnBackgroundImageLayoutChanged(EventArgs e)
		{
			base.OnBackgroundImageLayoutChanged(e);
		}

		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x0600073E RID: 1854 RVA: 0x000046A9 File Offset: 0x000028A9
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnEnabledChanged(EventArgs e)
		{
			base.OnEnabledChanged(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Enter" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x0600073F RID: 1855 RVA: 0x000203F2 File Offset: 0x0001E5F2
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnEnter(EventArgs e)
		{
			base.OnEnter(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Form.FormClosed" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.FormClosedEventArgs" /> that contains the event data. </param>
		// Token: 0x06000740 RID: 1856 RVA: 0x000203FC File Offset: 0x0001E5FC
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnFormClosed(FormClosedEventArgs e)
		{
			Application.RemoveForm(this);
			FormClosedEventHandler formClosedEventHandler = (FormClosedEventHandler)base.Events[Form.FormClosedEvent];
			if (formClosedEventHandler != null)
			{
				formClosedEventHandler(this, e);
			}
			foreach (object obj in this.disabled_by_showdialog)
			{
				((Form)obj).Enabled = true;
			}
			this.disabled_by_showdialog.Clear();
			if (Form.modal_dialogs.Contains(this))
			{
				Form.modal_dialogs.Remove(this);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Form.FormClosing" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.FormClosingEventArgs" /> that contains the event data. </param>
		// Token: 0x06000741 RID: 1857 RVA: 0x000204A0 File Offset: 0x0001E6A0
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnFormClosing(FormClosingEventArgs e)
		{
			FormClosingEventHandler formClosingEventHandler = (FormClosingEventHandler)base.Events[Form.FormClosingEvent];
			if (formClosingEventHandler != null)
			{
				formClosingEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Layout" /> event.</summary>
		/// <param name="levent">The event data.</param>
		// Token: 0x06000742 RID: 1858 RVA: 0x000204D0 File Offset: 0x0001E6D0
		protected override void OnLayout(LayoutEventArgs levent)
		{
			base.OnLayout(levent);
			if (this.AutoSize)
			{
				Size preferredSizeCore = this.GetPreferredSizeCore(Size.Empty);
				if (this.AutoSizeMode == AutoSizeMode.GrowOnly)
				{
					preferredSizeCore.Width = Math.Max(preferredSizeCore.Width, base.Width);
					preferredSizeCore.Height = Math.Max(preferredSizeCore.Height, base.Height);
				}
				if (preferredSizeCore == this.Size)
				{
					return;
				}
				base.SetBoundsInternal(this.bounds.X, this.bounds.Y, preferredSizeCore.Width, preferredSizeCore.Height, BoundsSpecified.None);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Form.ResizeBegin" /> event.</summary>
		/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000743 RID: 1859 RVA: 0x00020570 File Offset: 0x0001E770
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnResizeBegin(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Form.ResizeBeginEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Form.ResizeEnd" /> event.</summary>
		/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000744 RID: 1860 RVA: 0x000205A0 File Offset: 0x0001E7A0
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnResizeEnd(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Form.ResizeEndEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Form.Shown" /> event.</summary>
		/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000745 RID: 1861 RVA: 0x000205D0 File Offset: 0x0001E7D0
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnShown(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Form.ShownEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06000746 RID: 1862 RVA: 0x00020600 File Offset: 0x0001E800
		internal void OnUIATopMostChanged()
		{
			EventHandler eventHandler = (EventHandler)base.Events[Form.UIATopMostChangedEvent];
			if (eventHandler != null)
			{
				eventHandler(this, EventArgs.Empty);
			}
		}

		// Token: 0x06000747 RID: 1863 RVA: 0x00020634 File Offset: 0x0001E834
		internal void OnUIAWindowStateChanged()
		{
			EventHandler eventHandler = (EventHandler)base.Events[Form.UIAWindowStateChangedEvent];
			if (eventHandler != null)
			{
				eventHandler(this, EventArgs.Empty);
			}
		}

		// Token: 0x0400045C RID: 1116
		internal bool closing;

		// Token: 0x0400045D RID: 1117
		private bool closed;

		// Token: 0x0400045E RID: 1118
		private FormBorderStyle form_border_style;

		// Token: 0x0400045F RID: 1119
		private bool is_active;

		// Token: 0x04000460 RID: 1120
		private bool autoscale;

		// Token: 0x04000461 RID: 1121
		private Size clientsize_set;

		// Token: 0x04000462 RID: 1122
		private Size autoscale_base_size;

		// Token: 0x04000463 RID: 1123
		private bool allow_transparency;

		// Token: 0x04000464 RID: 1124
		private static Icon default_icon;

		// Token: 0x04000465 RID: 1125
		internal bool is_modal;

		// Token: 0x04000466 RID: 1126
		internal FormWindowState window_state;

		// Token: 0x04000467 RID: 1127
		private bool control_box;

		// Token: 0x04000468 RID: 1128
		private bool minimize_box;

		// Token: 0x04000469 RID: 1129
		private bool maximize_box;

		// Token: 0x0400046A RID: 1130
		private bool help_button;

		// Token: 0x0400046B RID: 1131
		private bool show_in_taskbar;

		// Token: 0x0400046C RID: 1132
		private bool topmost;

		// Token: 0x0400046D RID: 1133
		private IButtonControl accept_button;

		// Token: 0x0400046E RID: 1134
		private IButtonControl cancel_button;

		// Token: 0x0400046F RID: 1135
		private DialogResult dialog_result;

		// Token: 0x04000470 RID: 1136
		private FormStartPosition start_position;

		// Token: 0x04000471 RID: 1137
		private Form owner;

		// Token: 0x04000472 RID: 1138
		private Form.ControlCollection owned_forms;

		// Token: 0x04000473 RID: 1139
		private MdiClient mdi_container;

		// Token: 0x04000474 RID: 1140
		internal InternalWindowManager window_manager;

		// Token: 0x04000475 RID: 1141
		private Form mdi_parent;

		// Token: 0x04000476 RID: 1142
		private bool key_preview;

		// Token: 0x04000477 RID: 1143
		private MainMenu menu;

		// Token: 0x04000478 RID: 1144
		private Icon icon;

		// Token: 0x04000479 RID: 1145
		private Size maximum_size;

		// Token: 0x0400047A RID: 1146
		private Size minimum_size;

		// Token: 0x0400047B RID: 1147
		private SizeGripStyle size_grip_style;

		// Token: 0x0400047C RID: 1148
		private SizeGrip size_grip;

		// Token: 0x0400047D RID: 1149
		private Rectangle maximized_bounds;

		// Token: 0x0400047E RID: 1150
		private Rectangle default_maximized_bounds;

		// Token: 0x0400047F RID: 1151
		private double opacity;

		// Token: 0x04000480 RID: 1152
		internal ApplicationContext context;

		// Token: 0x04000481 RID: 1153
		private Color transparency_key;

		// Token: 0x04000482 RID: 1154
		private bool is_loaded;

		// Token: 0x04000483 RID: 1155
		internal int is_changing_visible_state;

		// Token: 0x04000484 RID: 1156
		internal bool has_been_visible;

		// Token: 0x04000485 RID: 1157
		private bool shown_raised;

		// Token: 0x04000486 RID: 1158
		private bool close_raised;

		// Token: 0x04000487 RID: 1159
		private bool is_clientsize_set;

		// Token: 0x04000488 RID: 1160
		internal bool suppress_closing_events;

		// Token: 0x04000489 RID: 1161
		internal bool waiting_showwindow;

		// Token: 0x0400048A RID: 1162
		private bool is_minimizing;

		// Token: 0x0400048B RID: 1163
		private bool show_icon = true;

		// Token: 0x0400048C RID: 1164
		private MenuStrip main_menu_strip;

		// Token: 0x0400048D RID: 1165
		private Rectangle restore_bounds;

		// Token: 0x0400048E RID: 1166
		private bool autoscale_base_size_set;

		// Token: 0x0400048F RID: 1167
		internal ArrayList disabled_by_showdialog = new ArrayList();

		// Token: 0x04000490 RID: 1168
		internal static ArrayList modal_dialogs = new ArrayList();

		// Token: 0x04000491 RID: 1169
		private static object ActivatedEvent = new object();

		// Token: 0x04000493 RID: 1171
		private static object ClosingEvent;

		// Token: 0x04000495 RID: 1173
		private static object InputLanguageChangedEvent;

		// Token: 0x04000496 RID: 1174
		private static object InputLanguageChangingEvent;

		// Token: 0x04000498 RID: 1176
		private static object MaximizedBoundsChangedEvent;

		// Token: 0x04000499 RID: 1177
		private static object MaximumSizeChangedEvent;

		// Token: 0x0400049A RID: 1178
		private static object MdiChildActivateEvent;

		// Token: 0x0400049B RID: 1179
		private static object MenuCompleteEvent;

		// Token: 0x0400049C RID: 1180
		private static object MenuStartEvent;

		// Token: 0x0400049D RID: 1181
		private static object MinimumSizeChangedEvent;

		// Token: 0x0400049E RID: 1182
		private static object FormClosingEvent;

		// Token: 0x040004A0 RID: 1184
		private static object HelpButtonClickedEvent;

		// Token: 0x040004A1 RID: 1185
		private static object ResizeEndEvent;

		// Token: 0x040004A2 RID: 1186
		private static object ResizeBeginEvent;

		// Token: 0x040004A3 RID: 1187
		private static object RightToLeftLayoutChangedEvent;

		// Token: 0x040004A4 RID: 1188
		private static object ShownEvent;

		// Token: 0x040004A5 RID: 1189
		private static object UIAMenuChangedEvent;

		// Token: 0x040004A6 RID: 1190
		private static object UIATopMostChangedEvent;

		// Token: 0x040004A7 RID: 1191
		private static object UIAWindowStateChangedEvent;

		/// <summary>Represents a collection of controls on the form.</summary>
		// Token: 0x020000B2 RID: 178
		[ComVisible(false)]
		public new class ControlCollection : Control.ControlCollection
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Form.ControlCollection" /> class.</summary>
			/// <param name="owner">The <see cref="T:System.Windows.Forms.Form" /> to contain the controls added to the control collection. </param>
			// Token: 0x06000748 RID: 1864 RVA: 0x00020666 File Offset: 0x0001E866
			public ControlCollection(Form owner)
				: base(owner)
			{
				this.form_owner = owner;
			}

			/// <summary>Adds a control to the form.</summary>
			/// <param name="value">The <see cref="T:System.Windows.Forms.Control" /> to add to the form. </param>
			/// <exception cref="T:System.Exception">A multiple document interface (MDI) parent form cannot have controls added to it. </exception>
			// Token: 0x06000749 RID: 1865 RVA: 0x00020676 File Offset: 0x0001E876
			public override void Add(Control value)
			{
				if (base.Contains(value))
				{
					return;
				}
				base.AddToList(value);
				((Form)value).owner = this.form_owner;
			}

			/// <summary>Removes a control from the form.</summary>
			/// <param name="value">A <see cref="T:System.Windows.Forms.Control" /> to remove from the form. </param>
			// Token: 0x0600074A RID: 1866 RVA: 0x0002069A File Offset: 0x0001E89A
			public override void Remove(Control value)
			{
				((Form)value).owner = null;
				base.Remove(value);
			}

			// Token: 0x040004A8 RID: 1192
			private Form form_owner;
		}
	}
}
