using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Represents a Windows picture box control for displaying an image.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000161 RID: 353
	[DefaultProperty("Image")]
	[Designer("System.Windows.Forms.Design.PictureBoxDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[Docking(DockingBehavior.Ask)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[DefaultBindingProperty("Image")]
	public class PictureBox : Control
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.PictureBox" /> class.</summary>
		// Token: 0x06000DA2 RID: 3490 RVA: 0x0003B870 File Offset: 0x00039A70
		public PictureBox()
		{
			this.no_update = 0;
			base.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
			base.SetStyle(ControlStyles.Opaque, false);
			base.SetStyle(ControlStyles.Selectable, false);
			base.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
			base.HandleCreated += this.PictureBox_HandleCreated;
			this.initial_image = ResourceImageLoader.Get("image-x-generic.png");
			this.error_image = ResourceImageLoader.Get("image-missing.png");
		}

		/// <summary>Indicates how the image is displayed.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.PictureBoxSizeMode" /> values. The default is <see cref="F:System.Windows.Forms.PictureBoxSizeMode.Normal" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="T:System.Windows.Forms.PictureBoxSizeMode" /> values. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700036A RID: 874
		// (get) Token: 0x06000DA3 RID: 3491 RVA: 0x0003B8E8 File Offset: 0x00039AE8
		// (set) Token: 0x06000DA4 RID: 3492 RVA: 0x0003B8F0 File Offset: 0x00039AF0
		[DefaultValue(PictureBoxSizeMode.Normal)]
		[Localizable(true)]
		[RefreshProperties(RefreshProperties.Repaint)]
		public PictureBoxSizeMode SizeMode
		{
			get
			{
				return this.size_mode;
			}
			set
			{
				if (this.size_mode == value)
				{
					return;
				}
				this.size_mode = value;
				if (this.size_mode == PictureBoxSizeMode.AutoSize)
				{
					this.AutoSize = true;
					base.SetAutoSizeMode(AutoSizeMode.GrowAndShrink);
				}
				else
				{
					this.AutoSize = false;
					base.SetAutoSizeMode(AutoSizeMode.GrowOnly);
				}
				this.UpdateSize();
				if (this.no_update == 0)
				{
					base.Invalidate();
				}
				this.OnSizeModeChanged(EventArgs.Empty);
			}
		}

		/// <summary>Gets or sets the image that is displayed by <see cref="T:System.Windows.Forms.PictureBox" />.</summary>
		/// <returns>The <see cref="T:System.Drawing.Image" /> to display.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700036B RID: 875
		// (get) Token: 0x06000DA5 RID: 3493 RVA: 0x0003B954 File Offset: 0x00039B54
		// (set) Token: 0x06000DA6 RID: 3494 RVA: 0x0003B95C File Offset: 0x00039B5C
		[Bindable(true)]
		[Localizable(true)]
		public Image Image
		{
			get
			{
				return this.image;
			}
			set
			{
				this.ChangeImage(value, false);
			}
		}

		/// <summary>Indicates the border style for the control.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.BorderStyle" /> enumeration values. The default is <see cref="F:System.Windows.Forms.BorderStyle.None" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="T:System.Windows.Forms.BorderStyle" /> values. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700036C RID: 876
		// (set) Token: 0x06000DA7 RID: 3495 RVA: 0x0003B966 File Offset: 0x00039B66
		[DefaultValue(BorderStyle.None)]
		[DispId(-504)]
		public BorderStyle BorderStyle
		{
			set
			{
				base.InternalBorderStyle = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether control's elements are aligned to support locales using right-to-left languages.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.RightToLeft" /> values.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700036D RID: 877
		// (get) Token: 0x06000DA8 RID: 3496 RVA: 0x0003B96F File Offset: 0x00039B6F
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override RightToLeft RightToLeft
		{
			get
			{
				return base.RightToLeft;
			}
		}

		/// <summary>Gets or sets the tab index value.</summary>
		/// <returns>The tab index value.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700036E RID: 878
		// (set) Token: 0x06000DA9 RID: 3497 RVA: 0x0003B977 File Offset: 0x00039B77
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new int TabIndex
		{
			set
			{
				base.TabIndex = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the user can give the focus to this control by using the TAB key.</summary>
		/// <returns>true if the user can give the focus to the control by using the TAB key; otherwise, false. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700036F RID: 879
		// (set) Token: 0x06000DAA RID: 3498 RVA: 0x000208ED File Offset: 0x0001EAED
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new bool TabStop
		{
			set
			{
				base.TabStop = value;
			}
		}

		/// <summary>Gets or sets the text of the <see cref="T:System.Windows.Forms.PictureBox" />.</summary>
		/// <returns>The text of the <see cref="T:System.Windows.Forms.PictureBox" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000370 RID: 880
		// (get) Token: 0x06000DAB RID: 3499 RVA: 0x000043B4 File Offset: 0x000025B4
		// (set) Token: 0x06000DAC RID: 3500 RVA: 0x000043BC File Offset: 0x000025BC
		[Bindable(false)]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
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

		/// <summary>Overrides the <see cref="P:System.Windows.Forms.Control.CreateParams" /> property.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.CreateParams" /> that contains the required creation parameters when the handle to the control is created.</returns>
		// Token: 0x17000371 RID: 881
		// (get) Token: 0x06000DAD RID: 3501 RVA: 0x00004663 File Offset: 0x00002863
		protected override CreateParams CreateParams
		{
			get
			{
				return base.CreateParams;
			}
		}

		/// <summary>Gets or sets the font of the text displayed by the control.</summary>
		/// <returns>The <see cref="T:System.Drawing.Font" /> to apply to the text displayed by the control. The default is the value of the <see cref="P:System.Windows.Forms.Control.DefaultFont" /> property.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000372 RID: 882
		// (get) Token: 0x06000DAE RID: 3502 RVA: 0x0002C5BA File Offset: 0x0002A7BA
		// (set) Token: 0x06000DAF RID: 3503 RVA: 0x0003B980 File Offset: 0x00039B80
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override Font Font
		{
			get
			{
				return base.Font;
			}
			set
			{
				base.Font = value;
			}
		}

		/// <summary>Overrides the <see cref="P:System.Windows.Forms.Control.ForeColor" /> property.</summary>
		/// <returns>The foreground <see cref="T:System.Drawing.Color" /> of the control. The default is the value of the <see cref="P:System.Windows.Forms.Control.DefaultForeColor" /> property.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000373 RID: 883
		// (get) Token: 0x06000DB0 RID: 3504 RVA: 0x00005E5F File Offset: 0x0000405F
		// (set) Token: 0x06000DB1 RID: 3505 RVA: 0x0003B989 File Offset: 0x00039B89
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override Color ForeColor
		{
			get
			{
				return base.ForeColor;
			}
			set
			{
				base.ForeColor = value;
			}
		}

		/// <summary>Overrides the <see cref="P:System.Windows.Forms.Control.AllowDrop" /> property.</summary>
		/// <returns>true if drag-and-drop operations are allowed in the control; otherwise, false. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000374 RID: 884
		// (get) Token: 0x06000DB2 RID: 3506 RVA: 0x0002083A File Offset: 0x0001EA3A
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool AllowDrop
		{
			get
			{
				return base.AllowDrop;
			}
		}

		/// <returns>The default <see cref="T:System.Drawing.Size" /> of the control.</returns>
		// Token: 0x17000375 RID: 885
		// (get) Token: 0x06000DB3 RID: 3507 RVA: 0x0003B992 File Offset: 0x00039B92
		protected override Size DefaultSize
		{
			get
			{
				return ThemeEngine.Current.PictureBoxDefaultSize;
			}
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.PictureBox" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release managed and unmanaged resources; false to release unmanaged resources only.</param>
		// Token: 0x06000DB4 RID: 3508 RVA: 0x0003B99E File Offset: 0x00039B9E
		protected override void Dispose(bool disposing)
		{
			if (this.image != null)
			{
				this.StopAnimation();
				this.image = null;
			}
			this.initial_image = null;
			base.Dispose(disposing);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.</summary>
		/// <param name="pe">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data. </param>
		// Token: 0x06000DB5 RID: 3509 RVA: 0x0003B9C3 File Offset: 0x00039BC3
		protected override void OnPaint(PaintEventArgs pe)
		{
			ThemeEngine.Current.DrawPictureBox(pe.Graphics, pe.ClipRectangle, this);
			base.OnPaint(pe);
		}

		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000DB6 RID: 3510 RVA: 0x0002549F File Offset: 0x0002369F
		protected override void OnVisibleChanged(EventArgs e)
		{
			base.OnVisibleChanged(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.PictureBox.SizeModeChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000DB7 RID: 3511 RVA: 0x0003B9E4 File Offset: 0x00039BE4
		protected virtual void OnSizeModeChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[PictureBox.SizeModeChangedEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000DB8 RID: 3512 RVA: 0x000046A9 File Offset: 0x000028A9
		protected override void OnEnabledChanged(EventArgs e)
		{
			base.OnEnabledChanged(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.HandleCreated" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000DB9 RID: 3513 RVA: 0x00004D0D File Offset: 0x00002F0D
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.HandleDestroyed" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06000DBA RID: 3514 RVA: 0x00006538 File Offset: 0x00004738
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnHandleDestroyed(EventArgs e)
		{
			base.OnHandleDestroyed(e);
		}

		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000DBB RID: 3515 RVA: 0x0000488F File Offset: 0x00002A8F
		protected override void OnParentChanged(EventArgs e)
		{
			base.OnParentChanged(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Resize" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06000DBC RID: 3516 RVA: 0x0003BA12 File Offset: 0x00039C12
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			base.Invalidate();
		}

		// Token: 0x06000DBD RID: 3517 RVA: 0x0003BA21 File Offset: 0x00039C21
		internal override Size GetPreferredSizeCore(Size proposedSize)
		{
			if (this.image == null)
			{
				return base.GetPreferredSizeCore(proposedSize);
			}
			return this.image.Size;
		}

		// Token: 0x06000DBE RID: 3518 RVA: 0x0003BA40 File Offset: 0x00039C40
		private void ChangeImage(Image value, bool from_url)
		{
			this.StopAnimation();
			this.image_from_url = from_url;
			this.image = value;
			if (base.IsHandleCreated)
			{
				this.UpdateSize();
				if (this.image != null && ImageAnimator.CanAnimate(this.image))
				{
					this.frame_handler = new EventHandler(this.OnAnimateImage);
					ImageAnimator.Animate(this.image, this.frame_handler);
				}
				if (this.no_update == 0)
				{
					base.Invalidate();
				}
			}
		}

		// Token: 0x06000DBF RID: 3519 RVA: 0x0003BAB5 File Offset: 0x00039CB5
		private void StopAnimation()
		{
			if (this.frame_handler == null)
			{
				return;
			}
			ImageAnimator.StopAnimate(this.image, this.frame_handler);
			this.frame_handler = null;
		}

		// Token: 0x06000DC0 RID: 3520 RVA: 0x0003BAD8 File Offset: 0x00039CD8
		private void UpdateSize()
		{
			if (this.image == null)
			{
				return;
			}
			if (base.Parent != null)
			{
				base.Parent.PerformLayout(this, "AutoSize");
			}
		}

		// Token: 0x06000DC1 RID: 3521 RVA: 0x0003BAFC File Offset: 0x00039CFC
		private void OnAnimateImage(object sender, EventArgs e)
		{
			if (!base.IsHandleCreated)
			{
				return;
			}
			base.BeginInvoke(new EventHandler(this.UpdateAnimatedImage), new object[] { this, e });
		}

		// Token: 0x06000DC2 RID: 3522 RVA: 0x0003BB28 File Offset: 0x00039D28
		private void UpdateAnimatedImage(object sender, EventArgs e)
		{
			if (!base.IsHandleCreated)
			{
				return;
			}
			ImageAnimator.UpdateFrames(this.image);
			this.Refresh();
		}

		// Token: 0x06000DC3 RID: 3523 RVA: 0x0003BB44 File Offset: 0x00039D44
		private void PictureBox_HandleCreated(object sender, EventArgs e)
		{
			this.UpdateSize();
			if (this.image != null && ImageAnimator.CanAnimate(this.image))
			{
				this.frame_handler = new EventHandler(this.OnAnimateImage);
				ImageAnimator.Animate(this.image, this.frame_handler);
			}
			if (this.no_update == 0)
			{
				base.Invalidate();
			}
		}

		/// <summary>Returns a string that represents the current <see cref="T:System.Windows.Forms.PictureBox" /> control.</summary>
		/// <returns>A string that represents the current <see cref="T:System.Windows.Forms.PictureBox" />. </returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000DC4 RID: 3524 RVA: 0x0003BB9D File Offset: 0x00039D9D
		public override string ToString()
		{
			return string.Format("{0}, SizeMode: {1}", base.ToString(), this.SizeMode);
		}

		// Token: 0x04000883 RID: 2179
		private Image image;

		// Token: 0x04000884 RID: 2180
		private PictureBoxSizeMode size_mode;

		// Token: 0x04000885 RID: 2181
		private Image error_image;

		// Token: 0x04000886 RID: 2182
		private Image initial_image;

		// Token: 0x04000887 RID: 2183
		private bool image_from_url;

		// Token: 0x04000888 RID: 2184
		private int no_update;

		// Token: 0x04000889 RID: 2185
		private EventHandler frame_handler;

		// Token: 0x0400088A RID: 2186
		private static object LoadCompletedEvent = new object();

		// Token: 0x0400088B RID: 2187
		private static object LoadProgressChangedEvent = new object();

		// Token: 0x0400088C RID: 2188
		private static object SizeModeChangedEvent = new object();
	}
}
