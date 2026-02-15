using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace System.Windows.Forms
{
	/// <summary>Represents a small rectangular pop-up window that displays a brief description of a control's purpose when the user rests the pointer on the control.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020001F3 RID: 499
	[DefaultEvent("Popup")]
	[ProvideProperty("ToolTip", typeof(Control))]
	[ToolboxItemFilter("System.Windows.Forms", ToolboxItemFilterType.Allow)]
	public class ToolTip : Component, IExtenderProvider
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolTip" /> without a specified container.</summary>
		// Token: 0x06001510 RID: 5392 RVA: 0x0006A36C File Offset: 0x0006856C
		public ToolTip()
		{
			this.is_active = true;
			this.automatic_delay = 500;
			this.autopop_delay = 5000;
			this.initial_delay = 500;
			this.re_show_delay = 100;
			this.show_always = false;
			this.back_color = SystemColors.Info;
			this.fore_color = SystemColors.InfoText;
			this.isBalloon = false;
			this.stripAmpersands = false;
			this.useAnimation = true;
			this.useFading = true;
			this.tooltip_strings = new Hashtable(5);
			this.controls = new ArrayList(5);
			this.tooltip_window = new ToolTip.ToolTipWindow();
			this.tooltip_window.MouseLeave += this.control_MouseLeave;
			this.tooltip_window.Draw += this.tooltip_window_Draw;
			this.tooltip_window.Popup += this.tooltip_window_Popup;
			this.tooltip_window.UnPopup += delegate(object sender, PopupEventArgs args)
			{
				this.OnUnPopup(args);
			};
			this.UnPopup += ToolTip.OnUIAUnPopup;
			this.timer = new Timer();
			this.timer.Enabled = false;
			this.timer.Tick += this.timer_Tick;
		}

		// Token: 0x1400004A RID: 74
		// (add) Token: 0x06001511 RID: 5393 RVA: 0x0006A4A7 File Offset: 0x000686A7
		// (remove) Token: 0x06001512 RID: 5394 RVA: 0x0006A4BA File Offset: 0x000686BA
		internal event PopupEventHandler UnPopup
		{
			add
			{
				base.Events.AddHandler(ToolTip.UnPopupEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolTip.UnPopupEvent, value);
			}
		}

		// Token: 0x06001513 RID: 5395 RVA: 0x0006A4CD File Offset: 0x000686CD
		internal static void OnUIAUnPopup(object sender, PopupEventArgs args)
		{
			if (ToolTip.UIAUnPopup != null)
			{
				ToolTip.UIAUnPopup(sender, args);
			}
		}

		// Token: 0x06001514 RID: 5396 RVA: 0x0006A4E2 File Offset: 0x000686E2
		internal static void OnUIAToolTipHookUp(object sender, ControlEventArgs args)
		{
			if (ToolTip.UIAToolTipHookUp != null)
			{
				ToolTip.UIAToolTipHookUp(sender, args);
			}
		}

		// Token: 0x06001515 RID: 5397 RVA: 0x0006A4F7 File Offset: 0x000686F7
		internal static void OnUIAToolTipUnhookUp(object sender, ControlEventArgs args)
		{
			if (ToolTip.UIAToolTipUnhookUp != null)
			{
				ToolTip.UIAToolTipUnhookUp(sender, args);
			}
		}

		// Token: 0x06001516 RID: 5398 RVA: 0x0006A50C File Offset: 0x0006870C
		~ToolTip()
		{
		}

		/// <summary>Gets or sets a value indicating whether the ToolTip is currently active.</summary>
		/// <returns>true if the ToolTip is currently active; otherwise, false. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700058E RID: 1422
		// (get) Token: 0x06001517 RID: 5399 RVA: 0x0006A534 File Offset: 0x00068734
		// (set) Token: 0x06001518 RID: 5400 RVA: 0x0006A53C File Offset: 0x0006873C
		[DefaultValue(true)]
		public bool Active
		{
			get
			{
				return this.is_active;
			}
			set
			{
				if (this.is_active != value)
				{
					this.is_active = value;
					if (this.tooltip_window.Visible)
					{
						this.tooltip_window.Visible = false;
						this.active_control = null;
					}
				}
			}
		}

		/// <summary>Gets or sets the time that passes before the ToolTip appears.</summary>
		/// <returns>The period of time, in milliseconds, that the pointer must remain stationary on a control before the ToolTip window is displayed.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700058F RID: 1423
		// (set) Token: 0x06001519 RID: 5401 RVA: 0x0006A56E File Offset: 0x0006876E
		[RefreshProperties(RefreshProperties.All)]
		public int InitialDelay
		{
			set
			{
				if (this.initial_delay != value)
				{
					this.initial_delay = value;
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the ToolTip is drawn by the operating system or by code that you provide.</summary>
		/// <returns>true if the <see cref="T:System.Windows.Forms.ToolTip" /> is drawn by code that you provide; false if the <see cref="T:System.Windows.Forms.ToolTip" /> is drawn by the operating system. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Window="AllWindows" />
		/// </PermissionSet>
		// Token: 0x17000590 RID: 1424
		// (get) Token: 0x0600151A RID: 5402 RVA: 0x0006A580 File Offset: 0x00068780
		[DefaultValue(false)]
		public bool OwnerDraw
		{
			get
			{
				return this.owner_draw;
			}
		}

		/// <summary>Gets or sets the length of time that must transpire before subsequent ToolTip windows appear as the pointer moves from one control to another.</summary>
		/// <returns>The length of time, in milliseconds, that it takes subsequent ToolTip windows to appear.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000591 RID: 1425
		// (set) Token: 0x0600151B RID: 5403 RVA: 0x0006A588 File Offset: 0x00068788
		[RefreshProperties(RefreshProperties.All)]
		public int ReshowDelay
		{
			set
			{
				if (this.re_show_delay != value)
				{
					this.re_show_delay = value;
				}
			}
		}

		/// <summary>Returns true if the ToolTip can offer an extender property to the specified target component.</summary>
		/// <returns>true if the <see cref="T:System.Windows.Forms.ToolTip" /> class can offer one or more extender properties; otherwise, false.</returns>
		/// <param name="target">The target object to add an extender property to. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600151C RID: 5404 RVA: 0x00002D70 File Offset: 0x00000F70
		public bool CanExtend(object target)
		{
			return false;
		}

		/// <summary>Associates ToolTip text with the specified control.</summary>
		/// <param name="control">The <see cref="T:System.Windows.Forms.Control" /> to associate the ToolTip text with. </param>
		/// <param name="caption">The ToolTip text to display when the pointer is on the control. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600151D RID: 5405 RVA: 0x0006A59C File Offset: 0x0006879C
		public void SetToolTip(Control control, string caption)
		{
			ToolTip.OnUIAToolTipHookUp(this, new ControlEventArgs(control));
			this.tooltip_strings[control] = caption;
			if (!this.controls.Contains(control))
			{
				control.MouseEnter += this.control_MouseEnter;
				control.MouseMove += this.control_MouseMove;
				control.MouseLeave += this.control_MouseLeave;
				control.MouseDown += this.control_MouseDown;
				this.controls.Add(control);
			}
			if (this.active_control == control && caption != null && this.state == ToolTip.TipState.Show)
			{
				Size size = ThemeEngine.Current.ToolTipSize(this.tooltip_window, caption);
				this.tooltip_window.Width = size.Width;
				this.tooltip_window.Height = size.Height;
				this.tooltip_window.Text = caption;
				this.timer.Stop();
				this.timer.Start();
				return;
			}
			if (control.IsHandleCreated && this.MouseInControl(control, false))
			{
				this.ShowTooltip(control);
			}
		}

		/// <summary>Returns a string representation for this control.</summary>
		/// <returns>A <see cref="T:System.String" /> containing a description of the <see cref="T:System.Windows.Forms.ToolTip" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600151E RID: 5406 RVA: 0x0006A6AC File Offset: 0x000688AC
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				base.ToString(),
				" InitialDelay: ",
				this.initial_delay,
				", ShowAlways: ",
				this.show_always.ToString()
			});
		}

		/// <summary>Hides the specified ToolTip window.</summary>
		/// <param name="win">The <see cref="T:System.Windows.Forms.IWin32Window" /> of the associated window or control that the ToolTip is associated with.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="win" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600151F RID: 5407 RVA: 0x0006A6F9 File Offset: 0x000688F9
		public void Hide(IWin32Window win)
		{
			this.timer.Stop();
			this.state = ToolTip.TipState.Initial;
			this.UnhookFormEvents();
			this.tooltip_window.Visible = false;
		}

		/// <summary>Disposes of the <see cref="T:System.Windows.Forms.ToolTip" /> component.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x06001520 RID: 5408 RVA: 0x0006A720 File Offset: 0x00068920
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			if (disposing)
			{
				this.timer.Stop();
				this.timer.Dispose();
				this.tooltip_window.Dispose();
				this.tooltip_strings.Clear();
				foreach (object obj in this.controls)
				{
					Control control = (Control)obj;
					ToolTip.OnUIAToolTipUnhookUp(this, new ControlEventArgs(control));
				}
				this.controls.Clear();
			}
		}

		// Token: 0x06001521 RID: 5409 RVA: 0x0006A7C0 File Offset: 0x000689C0
		private void UnhookFormEvents()
		{
			if (this.hooked_form == null)
			{
				return;
			}
			this.hooked_form.Deactivate -= this.Form_Deactivate;
			this.hooked_form.Closed -= this.Form_Closed;
			this.hooked_form.Resize -= this.Form_Resize;
			this.hooked_form = null;
		}

		// Token: 0x06001522 RID: 5410 RVA: 0x0006A822 File Offset: 0x00068A22
		private void Form_Resize(object sender, EventArgs e)
		{
			if (((Form)sender).WindowState == FormWindowState.Minimized)
			{
				this.tooltip_window.Visible = false;
			}
		}

		// Token: 0x06001523 RID: 5411 RVA: 0x0006A83E File Offset: 0x00068A3E
		private void Form_Closed(object sender, EventArgs e)
		{
			this.tooltip_window.Visible = false;
		}

		// Token: 0x06001524 RID: 5412 RVA: 0x0006A83E File Offset: 0x00068A3E
		private void Form_Deactivate(object sender, EventArgs e)
		{
			this.tooltip_window.Visible = false;
		}

		// Token: 0x06001525 RID: 5413 RVA: 0x0006A84C File Offset: 0x00068A4C
		internal void Present(Control control, string text)
		{
			this.tooltip_window.Present(control, text);
		}

		// Token: 0x06001526 RID: 5414 RVA: 0x0006A85B File Offset: 0x00068A5B
		private void control_MouseEnter(object sender, EventArgs e)
		{
			this.ShowTooltip(sender as Control);
		}

		// Token: 0x06001527 RID: 5415 RVA: 0x0006A86C File Offset: 0x00068A6C
		private void ShowTooltip(Control control)
		{
			this.last_control = control;
			this.tooltip_window.Visible = false;
			this.timer.Stop();
			this.state = ToolTip.TipState.Initial;
			if (!this.is_active)
			{
				return;
			}
			if (!this.show_always && control.FindForm() != Form.ActiveForm)
			{
				return;
			}
			string text = (string)this.tooltip_strings[control];
			if (text != null && text.Length > 0)
			{
				if (this.active_control == null)
				{
					this.timer.Interval = Math.Max(this.initial_delay, 1);
				}
				else
				{
					this.timer.Interval = Math.Max(this.re_show_delay, 1);
				}
				this.active_control = control;
				this.timer.Start();
			}
		}

		// Token: 0x06001528 RID: 5416 RVA: 0x0006A928 File Offset: 0x00068B28
		private void timer_Tick(object sender, EventArgs e)
		{
			this.timer.Stop();
			ToolTip.TipState tipState = this.state;
			if (tipState != ToolTip.TipState.Initial)
			{
				if (tipState != ToolTip.TipState.Show)
				{
					throw new Exception("Timer shouldn't be running in state: " + this.state);
				}
				this.tooltip_window.Visible = false;
				this.state = ToolTip.TipState.Down;
				return;
			}
			else
			{
				if (this.active_control == null)
				{
					return;
				}
				this.tooltip_window.Present(this.active_control, (string)this.tooltip_strings[this.active_control]);
				this.state = ToolTip.TipState.Show;
				this.timer.Interval = this.autopop_delay;
				this.timer.Start();
				return;
			}
		}

		// Token: 0x06001529 RID: 5417 RVA: 0x0006A9D2 File Offset: 0x00068BD2
		private void tooltip_window_Popup(object sender, PopupEventArgs e)
		{
			e.ToolTipSize = ThemeEngine.Current.ToolTipSize(this.tooltip_window, this.tooltip_window.Text);
			this.OnPopup(e);
		}

		// Token: 0x0600152A RID: 5418 RVA: 0x0006A9FC File Offset: 0x00068BFC
		private void tooltip_window_Draw(object sender, DrawToolTipEventArgs e)
		{
			if (this.OwnerDraw)
			{
				this.OnDraw(e);
				return;
			}
			ThemeEngine.Current.DrawToolTip(e.Graphics, e.Bounds, this.tooltip_window);
		}

		// Token: 0x0600152B RID: 5419 RVA: 0x0006AA2C File Offset: 0x00068C2C
		private bool MouseInControl(Control control, bool fuzzy)
		{
			if (control == null)
			{
				return false;
			}
			Point mousePosition = Control.MousePosition;
			Point point = new Point(control.Bounds.X, control.Bounds.Y);
			if (control.Parent != null)
			{
				point = control.Parent.PointToScreen(point);
			}
			Size clientSize = control.ClientSize;
			Rectangle rectangle = new Rectangle(point, clientSize);
			if (fuzzy)
			{
				rectangle.Inflate(2, 2);
			}
			return rectangle.Contains(mousePosition);
		}

		// Token: 0x0600152C RID: 5420 RVA: 0x0006AAA2 File Offset: 0x00068CA2
		private void control_MouseLeave(object sender, EventArgs e)
		{
			this.timer.Stop();
			this.active_control = null;
			this.tooltip_window.Visible = false;
			if (this.last_control == sender)
			{
				this.last_control = null;
			}
		}

		// Token: 0x0600152D RID: 5421 RVA: 0x0006AAA2 File Offset: 0x00068CA2
		private void control_MouseDown(object sender, MouseEventArgs e)
		{
			this.timer.Stop();
			this.active_control = null;
			this.tooltip_window.Visible = false;
			if (this.last_control == sender)
			{
				this.last_control = null;
			}
		}

		// Token: 0x0600152E RID: 5422 RVA: 0x0006AAD2 File Offset: 0x00068CD2
		private void control_MouseMove(object sender, MouseEventArgs e)
		{
			if (this.state != ToolTip.TipState.Down)
			{
				this.timer.Stop();
				this.timer.Start();
			}
		}

		// Token: 0x0600152F RID: 5423 RVA: 0x0006AAF4 File Offset: 0x00068CF4
		internal void OnDraw(DrawToolTipEventArgs e)
		{
			DrawToolTipEventHandler drawToolTipEventHandler = (DrawToolTipEventHandler)base.Events[ToolTip.DrawEvent];
			if (drawToolTipEventHandler != null)
			{
				drawToolTipEventHandler(this, e);
			}
		}

		// Token: 0x06001530 RID: 5424 RVA: 0x0006AB24 File Offset: 0x00068D24
		internal void OnPopup(PopupEventArgs e)
		{
			PopupEventHandler popupEventHandler = (PopupEventHandler)base.Events[ToolTip.PopupEvent];
			if (popupEventHandler != null)
			{
				popupEventHandler(this, e);
			}
		}

		// Token: 0x06001531 RID: 5425 RVA: 0x0006AB54 File Offset: 0x00068D54
		internal void OnUnPopup(PopupEventArgs e)
		{
			PopupEventHandler popupEventHandler = (PopupEventHandler)base.Events[ToolTip.UnPopupEvent];
			if (popupEventHandler != null)
			{
				popupEventHandler(this, e);
			}
		}

		// Token: 0x17000592 RID: 1426
		// (get) Token: 0x06001532 RID: 5426 RVA: 0x0006AB82 File Offset: 0x00068D82
		internal bool Visible
		{
			get
			{
				return this.tooltip_window.Visible;
			}
		}

		// Token: 0x06001533 RID: 5427 RVA: 0x0006AB8F File Offset: 0x00068D8F
		// Note: this type is marked as 'beforefieldinit'.
		static ToolTip()
		{
			ToolTip.UnPopupEvent = new object();
			ToolTip.PopupEvent = new object();
			ToolTip.DrawEvent = new object();
		}

		// Token: 0x04000CB7 RID: 3255
		internal bool is_active;

		// Token: 0x04000CB8 RID: 3256
		internal int automatic_delay;

		// Token: 0x04000CB9 RID: 3257
		internal int autopop_delay;

		// Token: 0x04000CBA RID: 3258
		internal int initial_delay;

		// Token: 0x04000CBB RID: 3259
		internal int re_show_delay;

		// Token: 0x04000CBC RID: 3260
		internal bool show_always;

		// Token: 0x04000CBD RID: 3261
		internal Color back_color;

		// Token: 0x04000CBE RID: 3262
		internal Color fore_color;

		// Token: 0x04000CBF RID: 3263
		internal ToolTip.ToolTipWindow tooltip_window;

		// Token: 0x04000CC0 RID: 3264
		internal Hashtable tooltip_strings;

		// Token: 0x04000CC1 RID: 3265
		internal ArrayList controls;

		// Token: 0x04000CC2 RID: 3266
		internal Control active_control;

		// Token: 0x04000CC3 RID: 3267
		internal Control last_control;

		// Token: 0x04000CC4 RID: 3268
		internal Timer timer;

		// Token: 0x04000CC5 RID: 3269
		private Form hooked_form;

		// Token: 0x04000CC6 RID: 3270
		private bool isBalloon;

		// Token: 0x04000CC7 RID: 3271
		private bool owner_draw;

		// Token: 0x04000CC8 RID: 3272
		private bool stripAmpersands;

		// Token: 0x04000CC9 RID: 3273
		private bool useAnimation;

		// Token: 0x04000CCA RID: 3274
		private bool useFading;

		// Token: 0x04000CCC RID: 3276
		[CompilerGenerated]
		private static PopupEventHandler UIAUnPopup;

		// Token: 0x04000CCD RID: 3277
		[CompilerGenerated]
		private static ControlEventHandler UIAToolTipHookUp;

		// Token: 0x04000CCE RID: 3278
		[CompilerGenerated]
		private static ControlEventHandler UIAToolTipUnhookUp;

		// Token: 0x04000CCF RID: 3279
		private ToolTip.TipState state;

		// Token: 0x04000CD0 RID: 3280
		private static object PopupEvent;

		// Token: 0x04000CD1 RID: 3281
		private static object DrawEvent;

		// Token: 0x020001F4 RID: 500
		internal class ToolTipWindow : Control
		{
			// Token: 0x06001535 RID: 5429 RVA: 0x0006ABB8 File Offset: 0x00068DB8
			internal ToolTipWindow()
			{
				base.Visible = false;
				base.Size = new Size(100, 20);
				this.ForeColor = ThemeEngine.Current.ColorInfoText;
				this.BackColor = ThemeEngine.Current.ColorInfo;
				base.VisibleChanged += this.ToolTipWindow_VisibleChanged;
				base.VisibleChanged += this.OnUIAToolTip_VisibleChanged;
				base.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
				base.SetStyle(ControlStyles.ResizeRedraw, true);
				if (ThemeEngine.Current.ToolTipTransparentBackground)
				{
					base.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
					this.BackColor = Color.Transparent;
					return;
				}
				base.SetStyle(ControlStyles.Opaque, true);
			}

			// Token: 0x06001536 RID: 5430 RVA: 0x0006AC71 File Offset: 0x00068E71
			protected override void OnCreateControl()
			{
				base.OnCreateControl();
				XplatUI.SetTopmost(this.window.Handle, true);
			}

			// Token: 0x17000593 RID: 1427
			// (get) Token: 0x06001537 RID: 5431 RVA: 0x0006AC8B File Offset: 0x00068E8B
			protected override CreateParams CreateParams
			{
				get
				{
					CreateParams createParams = base.CreateParams;
					createParams.Style = int.MinValue;
					createParams.Style |= 67108864;
					createParams.ExStyle = 136;
					return createParams;
				}
			}

			// Token: 0x06001538 RID: 5432 RVA: 0x0006ACBC File Offset: 0x00068EBC
			protected override void OnPaint(PaintEventArgs pevent)
			{
				base.OnPaint(pevent);
				this.OnDraw(new DrawToolTipEventArgs(pevent.Graphics, this.associated_control, this.associated_control, base.ClientRectangle, this.Text, this.BackColor, this.ForeColor, this.Font));
			}

			// Token: 0x06001539 RID: 5433 RVA: 0x00004898 File Offset: 0x00002A98
			protected override void OnTextChanged(EventArgs args)
			{
				base.Invalidate();
				base.OnTextChanged(args);
			}

			// Token: 0x0600153A RID: 5434 RVA: 0x0006AD0B File Offset: 0x00068F0B
			protected override void WndProc(ref Message m)
			{
				if (m.Msg == 7 && m.WParam != IntPtr.Zero)
				{
					XplatUI.SetFocus(m.WParam);
				}
				base.WndProc(ref m);
			}

			// Token: 0x0600153B RID: 5435 RVA: 0x0006AD3C File Offset: 0x00068F3C
			internal virtual void OnDraw(DrawToolTipEventArgs e)
			{
				DrawToolTipEventHandler drawToolTipEventHandler = (DrawToolTipEventHandler)base.Events[ToolTip.ToolTipWindow.DrawEvent];
				if (drawToolTipEventHandler != null)
				{
					drawToolTipEventHandler(this, e);
					return;
				}
				ThemeEngine.Current.DrawToolTip(e.Graphics, e.Bounds, this);
			}

			// Token: 0x0600153C RID: 5436 RVA: 0x0006AD84 File Offset: 0x00068F84
			internal virtual void OnPopup(PopupEventArgs e)
			{
				PopupEventHandler popupEventHandler = (PopupEventHandler)base.Events[ToolTip.ToolTipWindow.PopupEvent];
				if (popupEventHandler != null)
				{
					popupEventHandler(this, e);
					return;
				}
				e.ToolTipSize = ThemeEngine.Current.ToolTipSize(this, this.Text);
			}

			// Token: 0x0600153D RID: 5437 RVA: 0x0006ADCC File Offset: 0x00068FCC
			private void ToolTipWindow_VisibleChanged(object sender, EventArgs e)
			{
				Control control = (Control)sender;
				if (control.is_visible)
				{
					XplatUI.SetTopmost(control.window.Handle, true);
					return;
				}
				XplatUI.SetTopmost(control.window.Handle, false);
			}

			// Token: 0x0600153E RID: 5438 RVA: 0x0006AE0D File Offset: 0x0006900D
			private void OnUIAToolTip_VisibleChanged(object sender, EventArgs e)
			{
				if (!base.Visible)
				{
					this.OnUnPopup(new PopupEventArgs(this.associated_control, this.associated_control, false, Size.Empty));
				}
			}

			// Token: 0x0600153F RID: 5439 RVA: 0x0006AE34 File Offset: 0x00069034
			private void OnUnPopup(PopupEventArgs e)
			{
				PopupEventHandler popupEventHandler = (PopupEventHandler)base.Events[ToolTip.ToolTipWindow.UnPopupEvent];
				if (popupEventHandler != null)
				{
					popupEventHandler(this, e);
				}
			}

			// Token: 0x17000594 RID: 1428
			// (get) Token: 0x06001540 RID: 5440 RVA: 0x00002D70 File Offset: 0x00000F70
			internal override bool ActivateOnShow
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06001541 RID: 5441 RVA: 0x0006AE64 File Offset: 0x00069064
			public void Present(Control control, string text)
			{
				if (base.IsDisposed)
				{
					return;
				}
				Size size;
				XplatUI.GetDisplaySize(out size);
				this.associated_control = control;
				this.Text = text;
				PopupEventArgs popupEventArgs = new PopupEventArgs(control, control, false, Size.Empty);
				this.OnPopup(popupEventArgs);
				if (popupEventArgs.Cancel)
				{
					return;
				}
				Size toolTipSize = popupEventArgs.ToolTipSize;
				base.Width = toolTipSize.Width;
				base.Height = toolTipSize.Height;
				int num;
				int num2;
				int num3;
				int num4;
				XplatUI.GetCursorInfo(control.Cursor.Handle, out num, out num2, out num3, out num4);
				Point mousePosition = Control.MousePosition;
				mousePosition.Y += num2 - num4;
				if (mousePosition.X + base.Width > size.Width)
				{
					mousePosition.X = size.Width - base.Width;
				}
				if (mousePosition.Y + base.Height > size.Height)
				{
					mousePosition.Y = Control.MousePosition.Y - base.Height - num4;
				}
				base.Location = mousePosition;
				base.Visible = true;
				base.BringToFront();
			}

			// Token: 0x1400004B RID: 75
			// (add) Token: 0x06001542 RID: 5442 RVA: 0x0006AF77 File Offset: 0x00069177
			// (remove) Token: 0x06001543 RID: 5443 RVA: 0x0006AF8A File Offset: 0x0006918A
			public event DrawToolTipEventHandler Draw
			{
				add
				{
					base.Events.AddHandler(ToolTip.ToolTipWindow.DrawEvent, value);
				}
				remove
				{
					base.Events.RemoveHandler(ToolTip.ToolTipWindow.DrawEvent, value);
				}
			}

			// Token: 0x1400004C RID: 76
			// (add) Token: 0x06001544 RID: 5444 RVA: 0x0006AF9D File Offset: 0x0006919D
			// (remove) Token: 0x06001545 RID: 5445 RVA: 0x0006AFB0 File Offset: 0x000691B0
			public event PopupEventHandler Popup
			{
				add
				{
					base.Events.AddHandler(ToolTip.ToolTipWindow.PopupEvent, value);
				}
				remove
				{
					base.Events.RemoveHandler(ToolTip.ToolTipWindow.PopupEvent, value);
				}
			}

			// Token: 0x1400004D RID: 77
			// (add) Token: 0x06001546 RID: 5446 RVA: 0x0006AFC3 File Offset: 0x000691C3
			// (remove) Token: 0x06001547 RID: 5447 RVA: 0x0006AFD6 File Offset: 0x000691D6
			internal event PopupEventHandler UnPopup
			{
				add
				{
					base.Events.AddHandler(ToolTip.ToolTipWindow.UnPopupEvent, value);
				}
				remove
				{
					base.Events.RemoveHandler(ToolTip.ToolTipWindow.UnPopupEvent, value);
				}
			}

			// Token: 0x06001548 RID: 5448 RVA: 0x0006AFE9 File Offset: 0x000691E9
			// Note: this type is marked as 'beforefieldinit'.
			static ToolTipWindow()
			{
				ToolTip.ToolTipWindow.DrawEvent = new object();
				ToolTip.ToolTipWindow.PopupEvent = new object();
				ToolTip.ToolTipWindow.UnPopupEvent = new object();
			}

			// Token: 0x04000CD2 RID: 3282
			private Control associated_control;

			// Token: 0x04000CD3 RID: 3283
			internal Icon icon;

			// Token: 0x04000CD4 RID: 3284
			internal string title = string.Empty;

			// Token: 0x04000CD5 RID: 3285
			internal Rectangle icon_rect;

			// Token: 0x04000CD6 RID: 3286
			internal Rectangle title_rect;

			// Token: 0x04000CD7 RID: 3287
			internal Rectangle text_rect;
		}

		// Token: 0x020001F5 RID: 501
		internal enum TipState
		{
			// Token: 0x04000CDC RID: 3292
			Initial,
			// Token: 0x04000CDD RID: 3293
			Show,
			// Token: 0x04000CDE RID: 3294
			Down
		}
	}
}
