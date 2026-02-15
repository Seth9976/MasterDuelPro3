using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Text;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Implements the basic functionality common to button controls.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000028 RID: 40
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Designer("System.Windows.Forms.Design.ButtonBaseDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	public abstract class ButtonBase : Control
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ButtonBase" /> class.</summary>
		// Token: 0x060000C8 RID: 200 RVA: 0x000041B8 File Offset: 0x000023B8
		protected ButtonBase()
		{
			this.flat_style = FlatStyle.Standard;
			this.flat_button_appearance = new FlatButtonAppearance(this);
			this.image_key = string.Empty;
			this.text_image_relation = TextImageRelation.Overlay;
			this.use_mnemonic = true;
			this.use_visual_style_back_color = true;
			this.image_index = -1;
			this.image = null;
			this.image_list = null;
			this.image_alignment = ContentAlignment.MiddleCenter;
			this.ImeMode = ImeMode.Disable;
			this.text_alignment = ContentAlignment.MiddleCenter;
			this.is_default = false;
			this.is_pressed = false;
			this.text_format = new StringFormat();
			this.text_format.Alignment = StringAlignment.Center;
			this.text_format.LineAlignment = StringAlignment.Center;
			this.text_format.HotkeyPrefix = HotkeyPrefix.Show;
			this.text_format.FormatFlags |= StringFormatFlags.LineLimit;
			this.text_format_flags = TextFormatFlags.HorizontalCenter;
			this.text_format_flags |= TextFormatFlags.VerticalCenter;
			this.text_format_flags |= TextFormatFlags.TextBoxControl;
			base.SetStyle(ControlStyles.Opaque | ControlStyles.ResizeRedraw | ControlStyles.UserMouse | ControlStyles.SupportsTransparentBackColor | ControlStyles.CacheText | ControlStyles.OptimizedDoubleBuffer, true);
			base.SetStyle(ControlStyles.StandardClick, false);
		}

		/// <summary>Gets or sets a value that indicates whether the control resizes based on its contents.</summary>
		/// <returns>true if the control automatically resizes based on its contents; otherwise, false. The default is true.</returns>
		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x000042BD File Offset: 0x000024BD
		// (set) Token: 0x060000CA RID: 202 RVA: 0x000042C5 File Offset: 0x000024C5
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[MWFCategory("Layout")]
		public override bool AutoSize
		{
			get
			{
				return base.AutoSize;
			}
			set
			{
				base.AutoSize = value;
			}
		}

		/// <summary>Gets or sets the background color of the control.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> value representing the background color.</returns>
		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000CB RID: 203 RVA: 0x000042CE File Offset: 0x000024CE
		// (set) Token: 0x060000CC RID: 204 RVA: 0x000042D6 File Offset: 0x000024D6
		public override Color BackColor
		{
			get
			{
				return base.BackColor;
			}
			set
			{
				base.BackColor = value;
			}
		}

		/// <summary>Gets the appearance of the border and the colors used to indicate check state and mouse state.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.FlatButtonAppearance" /> values.</returns>
		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000CD RID: 205 RVA: 0x000042DF File Offset: 0x000024DF
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Browsable(true)]
		[MWFCategory("Appearance")]
		public FlatButtonAppearance FlatAppearance
		{
			get
			{
				return this.flat_button_appearance;
			}
		}

		/// <summary>Gets or sets the flat style appearance of the button control.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.FlatStyle" /> values. The default value is Standard.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="T:System.Windows.Forms.FlatStyle" /> values. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000CE RID: 206 RVA: 0x000042E7 File Offset: 0x000024E7
		// (set) Token: 0x060000CF RID: 207 RVA: 0x000042EF File Offset: 0x000024EF
		[Localizable(true)]
		[DefaultValue(FlatStyle.Standard)]
		[MWFDescription("Determines look of button")]
		[MWFCategory("Appearance")]
		public FlatStyle FlatStyle
		{
			get
			{
				return this.flat_style;
			}
			set
			{
				if (this.flat_style != value)
				{
					this.flat_style = value;
					if (base.Parent != null)
					{
						base.Parent.PerformLayout(this, "FlatStyle");
					}
					base.Invalidate();
				}
			}
		}

		/// <summary>Gets or sets the image that is displayed on a button control.</summary>
		/// <returns>The <see cref="T:System.Drawing.Image" /> displayed on the button control. The default value is null.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x00004320 File Offset: 0x00002520
		[Localizable(true)]
		[MWFDescription("Sets image to be displayed on button face")]
		[MWFCategory("Appearance")]
		public Image Image
		{
			get
			{
				if (this.image != null)
				{
					return this.image;
				}
				if (this.image_index >= 0 && this.image_list != null)
				{
					return this.image_list.Images[this.image_index];
				}
				if (!string.IsNullOrEmpty(this.image_key) && this.image_list != null)
				{
					return this.image_list.Images[this.image_key];
				}
				return null;
			}
		}

		/// <summary>Gets or sets the alignment of the image on the button control.</summary>
		/// <returns>One of the <see cref="T:System.Drawing.ContentAlignment" /> values. The default value is MiddleCenter.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="T:System.Drawing.ContentAlignment" /> values. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x00004391 File Offset: 0x00002591
		[Localizable(true)]
		[DefaultValue(ContentAlignment.MiddleCenter)]
		[MWFDescription("Sets the alignment of the image to be displayed on button face")]
		[MWFCategory("Appearance")]
		public ContentAlignment ImageAlign
		{
			get
			{
				return this.image_alignment;
			}
		}

		/// <summary>Gets or sets the image list index value of the image displayed on the button control.</summary>
		/// <returns>A zero-based index, which represents the image position in an <see cref="T:System.Windows.Forms.ImageList" />. The default is -1.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The assigned value is less than the lower bounds of the <see cref="P:System.Windows.Forms.ButtonBase.ImageIndex" />. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x00004399 File Offset: 0x00002599
		[Localizable(true)]
		[DefaultValue(-1)]
		[Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[TypeConverter(typeof(ImageIndexConverter))]
		[MWFDescription("Index of image to display, if ImageList is used for button face images")]
		[MWFCategory("Appearance")]
		[RefreshProperties(RefreshProperties.Repaint)]
		public int ImageIndex
		{
			get
			{
				if (this.image_list == null)
				{
					return -1;
				}
				return this.image_index;
			}
		}

		/// <summary>Gets or sets the Input Method Editor (IME) mode supported by this control. This property is not relevant for this class.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ImeMode" /> values.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000041 RID: 65
		// (set) Token: 0x060000D3 RID: 211 RVA: 0x000043AB File Offset: 0x000025AB
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new ImeMode ImeMode
		{
			set
			{
				base.ImeMode = value;
			}
		}

		/// <returns>The text associated with this control.</returns>
		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x000043B4 File Offset: 0x000025B4
		// (set) Token: 0x060000D5 RID: 213 RVA: 0x000043BC File Offset: 0x000025BC
		[SettingsBindable(true)]
		[Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
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

		/// <summary>Gets or sets the alignment of the text on the button control.</summary>
		/// <returns>One of the <see cref="T:System.Drawing.ContentAlignment" /> values. The default is MiddleCenter.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="T:System.Drawing.ContentAlignment" /> values. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x000043C5 File Offset: 0x000025C5
		// (set) Token: 0x060000D7 RID: 215 RVA: 0x000043D0 File Offset: 0x000025D0
		[Localizable(true)]
		[DefaultValue(ContentAlignment.MiddleCenter)]
		[MWFDescription("Alignment for button text")]
		[MWFCategory("Appearance")]
		public virtual ContentAlignment TextAlign
		{
			get
			{
				return this.text_alignment;
			}
			set
			{
				if (this.text_alignment != value)
				{
					this.text_alignment = value;
					this.text_format_flags &= ~TextFormatFlags.Bottom;
					this.text_format_flags &= (TextFormatFlags)(-1);
					this.text_format_flags &= (TextFormatFlags)(-1);
					this.text_format_flags &= ~TextFormatFlags.Right;
					this.text_format_flags &= ~TextFormatFlags.HorizontalCenter;
					this.text_format_flags &= ~TextFormatFlags.VerticalCenter;
					ContentAlignment contentAlignment = this.text_alignment;
					if (contentAlignment <= ContentAlignment.MiddleCenter)
					{
						switch (contentAlignment)
						{
						case ContentAlignment.TopLeft:
							this.text_format.Alignment = StringAlignment.Near;
							this.text_format.LineAlignment = StringAlignment.Near;
							break;
						case ContentAlignment.TopCenter:
							this.text_format.Alignment = StringAlignment.Center;
							this.text_format.LineAlignment = StringAlignment.Near;
							this.text_format_flags |= TextFormatFlags.HorizontalCenter;
							break;
						case (ContentAlignment)3:
							break;
						case ContentAlignment.TopRight:
							this.text_format.Alignment = StringAlignment.Far;
							this.text_format.LineAlignment = StringAlignment.Near;
							this.text_format_flags |= TextFormatFlags.Right;
							break;
						default:
							if (contentAlignment != ContentAlignment.MiddleLeft)
							{
								if (contentAlignment == ContentAlignment.MiddleCenter)
								{
									this.text_format.Alignment = StringAlignment.Center;
									this.text_format.LineAlignment = StringAlignment.Center;
									this.text_format_flags |= TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter;
								}
							}
							else
							{
								this.text_format.Alignment = StringAlignment.Near;
								this.text_format.LineAlignment = StringAlignment.Center;
								this.text_format_flags |= TextFormatFlags.VerticalCenter;
							}
							break;
						}
					}
					else if (contentAlignment <= ContentAlignment.BottomLeft)
					{
						if (contentAlignment != ContentAlignment.MiddleRight)
						{
							if (contentAlignment == ContentAlignment.BottomLeft)
							{
								this.text_format.Alignment = StringAlignment.Near;
								this.text_format.LineAlignment = StringAlignment.Far;
								this.text_format_flags |= TextFormatFlags.Bottom;
							}
						}
						else
						{
							this.text_format.Alignment = StringAlignment.Far;
							this.text_format.LineAlignment = StringAlignment.Center;
							this.text_format_flags |= TextFormatFlags.Right | TextFormatFlags.VerticalCenter;
						}
					}
					else if (contentAlignment != ContentAlignment.BottomCenter)
					{
						if (contentAlignment == ContentAlignment.BottomRight)
						{
							this.text_format.Alignment = StringAlignment.Far;
							this.text_format.LineAlignment = StringAlignment.Far;
							this.text_format_flags |= TextFormatFlags.Right | TextFormatFlags.Bottom;
						}
					}
					else
					{
						this.text_format.Alignment = StringAlignment.Center;
						this.text_format.LineAlignment = StringAlignment.Far;
						this.text_format_flags |= TextFormatFlags.HorizontalCenter | TextFormatFlags.Bottom;
					}
					base.Invalidate();
				}
			}
		}

		/// <summary>Gets or sets the position of text and image relative to each other.</summary>
		/// <returns>One of the values of <see cref="T:System.Windows.Forms.TextImageRelation" />. The default is <see cref="F:System.Windows.Forms.TextImageRelation.Overlay" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value is not one of the <see cref="T:System.Windows.Forms.TextImageRelation" /> values.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x0000462B File Offset: 0x0000282B
		[Localizable(true)]
		[DefaultValue(TextImageRelation.Overlay)]
		[MWFCategory("Appearance")]
		public TextImageRelation TextImageRelation
		{
			get
			{
				return this.text_image_relation;
			}
		}

		/// <summary>Gets or sets a value that determines whether to use the <see cref="T:System.Drawing.Graphics" /> class (GDI+) or the <see cref="T:System.Windows.Forms.TextRenderer" /> class (GDI) to render text.</summary>
		/// <returns>true if the <see cref="T:System.Drawing.Graphics" /> class should be used to perform text rendering for compatibility with versions 1.0 and 1.1. of the .NET Framework; otherwise, false. The default is false.</returns>
		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x00004633 File Offset: 0x00002833
		[DefaultValue(false)]
		[MWFCategory("Behavior")]
		public bool UseCompatibleTextRendering
		{
			get
			{
				return this.use_compatible_text_rendering;
			}
		}

		/// <summary>Gets or sets a value indicating whether the first character that is preceded by an ampersand (&amp;) is used as the mnemonic key of the control.</summary>
		/// <returns>true if the first character that is preceded by an ampersand (&amp;) is used as the mnemonic key of the control; otherwise, false. The default is true.</returns>
		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000DA RID: 218 RVA: 0x0000463B File Offset: 0x0000283B
		[DefaultValue(true)]
		[MWFCategory("Appearance")]
		public bool UseMnemonic
		{
			get
			{
				return this.use_mnemonic;
			}
		}

		/// <summary>Gets or sets a value that determines if the background is drawn using visual styles, if supported.</summary>
		/// <returns>true if the background is drawn using visual styles; otherwise, false.</returns>
		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000DB RID: 219 RVA: 0x00004643 File Offset: 0x00002843
		// (set) Token: 0x060000DC RID: 220 RVA: 0x0000464B File Offset: 0x0000284B
		[MWFCategory("Appearance")]
		public bool UseVisualStyleBackColor
		{
			get
			{
				return this.use_visual_style_back_color;
			}
			set
			{
				if (this.use_visual_style_back_color != value)
				{
					this.use_visual_style_back_color = value;
					base.Invalidate();
				}
			}
		}

		/// <returns>A <see cref="T:System.Windows.Forms.CreateParams" /> that contains the required creation parameters when the handle to the control is created.</returns>
		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000DD RID: 221 RVA: 0x00004663 File Offset: 0x00002863
		protected override CreateParams CreateParams
		{
			get
			{
				return base.CreateParams;
			}
		}

		/// <returns>The default <see cref="T:System.Drawing.Size" /> of the control.</returns>
		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000DE RID: 222 RVA: 0x0000466B File Offset: 0x0000286B
		protected override Size DefaultSize
		{
			get
			{
				return ThemeEngine.Current.ButtonBaseDefaultSize;
			}
		}

		/// <summary>Gets or sets a value indicating whether the button control is the default button.</summary>
		/// <returns>true if the button control is the default button; otherwise, false.</returns>
		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000DF RID: 223 RVA: 0x00004677 File Offset: 0x00002877
		// (set) Token: 0x060000E0 RID: 224 RVA: 0x0000467F File Offset: 0x0000287F
		protected internal bool IsDefault
		{
			get
			{
				return this.is_default;
			}
			set
			{
				if (this.is_default != value)
				{
					this.is_default = value;
					base.Invalidate();
				}
			}
		}

		/// <summary>Retrieves the size of a rectangular area into which a control can be fitted.</summary>
		/// <returns>An ordered pair of type <see cref="T:System.Drawing.Size" /> representing the width and height of a rectangle.</returns>
		/// <param name="proposedSize">The custom-sized area for a control.</param>
		// Token: 0x060000E1 RID: 225 RVA: 0x00004697 File Offset: 0x00002897
		public override Size GetPreferredSize(Size proposedSize)
		{
			return base.GetPreferredSize(proposedSize);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.ButtonBase" /> and optionally releases the managed resources. </summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x060000E2 RID: 226 RVA: 0x000046A0 File Offset: 0x000028A0
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}

		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060000E3 RID: 227 RVA: 0x000046A9 File Offset: 0x000028A9
		protected override void OnEnabledChanged(EventArgs e)
		{
			base.OnEnabledChanged(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.GotFocus" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060000E4 RID: 228 RVA: 0x000046B2 File Offset: 0x000028B2
		protected override void OnGotFocus(EventArgs e)
		{
			base.Invalidate();
			base.OnGotFocus(e);
		}

		/// <summary>Raises the <see cref="M:System.Windows.Forms.ButtonBase.OnKeyUp(System.Windows.Forms.KeyEventArgs)" /> event.</summary>
		/// <param name="kevent">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains the event data. </param>
		// Token: 0x060000E5 RID: 229 RVA: 0x000046C1 File Offset: 0x000028C1
		protected override void OnKeyDown(KeyEventArgs kevent)
		{
			if (kevent.KeyData == Keys.Space)
			{
				this.is_pressed = true;
				base.Invalidate();
				kevent.Handled = true;
			}
			base.OnKeyDown(kevent);
		}

		/// <summary>Raises the <see cref="M:System.Windows.Forms.ButtonBase.OnKeyUp(System.Windows.Forms.KeyEventArgs)" /> event.</summary>
		/// <param name="kevent">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains the event data. </param>
		// Token: 0x060000E6 RID: 230 RVA: 0x000046E8 File Offset: 0x000028E8
		protected override void OnKeyUp(KeyEventArgs kevent)
		{
			if (kevent.KeyData == Keys.Space)
			{
				this.is_pressed = false;
				base.Invalidate();
				this.OnClick(EventArgs.Empty);
				kevent.Handled = true;
			}
			base.OnKeyUp(kevent);
		}

		/// <summary>Raises the <see cref="M:System.Windows.Forms.ButtonBase.OnLostFocus(System.EventArgs)" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060000E7 RID: 231 RVA: 0x0000471A File Offset: 0x0000291A
		protected override void OnLostFocus(EventArgs e)
		{
			base.Invalidate();
			base.OnLostFocus(e);
		}

		/// <summary>Raises the <see cref="M:System.Windows.Forms.Control.OnMouseDown(System.Windows.Forms.MouseEventArgs)" /> event.</summary>
		/// <param name="mevent">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data. </param>
		// Token: 0x060000E8 RID: 232 RVA: 0x00004729 File Offset: 0x00002929
		protected override void OnMouseDown(MouseEventArgs mevent)
		{
			if ((mevent.Button & MouseButtons.Left) != MouseButtons.None)
			{
				this.is_pressed = true;
				base.Invalidate();
			}
			base.OnMouseDown(mevent);
		}

		/// <summary>Raises the <see cref="M:System.Windows.Forms.Control.OnMouseEnter(System.EventArgs)" /> event.</summary>
		/// <param name="eventargs">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060000E9 RID: 233 RVA: 0x0000474D File Offset: 0x0000294D
		protected override void OnMouseEnter(EventArgs eventargs)
		{
			this.is_entered = true;
			base.Invalidate();
			base.OnMouseEnter(eventargs);
		}

		/// <summary>Raises the <see cref="M:System.Windows.Forms.Control.OnMouseLeave(System.EventArgs)" /> event.</summary>
		/// <param name="eventargs">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060000EA RID: 234 RVA: 0x00004763 File Offset: 0x00002963
		protected override void OnMouseLeave(EventArgs eventargs)
		{
			this.is_entered = false;
			base.Invalidate();
			base.OnMouseLeave(eventargs);
		}

		/// <summary>Raises the <see cref="M:System.Windows.Forms.Control.OnMouseMove(System.Windows.Forms.MouseEventArgs)" /> event.</summary>
		/// <param name="mevent">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data. </param>
		// Token: 0x060000EB RID: 235 RVA: 0x0000477C File Offset: 0x0000297C
		protected override void OnMouseMove(MouseEventArgs mevent)
		{
			bool flag = false;
			bool flag2 = false;
			if (base.ClientRectangle.Contains(mevent.Location))
			{
				flag = true;
			}
			if ((mevent.Button & MouseButtons.Left) != MouseButtons.None && base.Capture && flag != this.is_pressed)
			{
				this.is_pressed = flag;
				flag2 = true;
			}
			if (this.is_entered != flag)
			{
				this.is_entered = flag;
				flag2 = true;
			}
			if (flag2)
			{
				base.Invalidate();
			}
			base.OnMouseMove(mevent);
		}

		/// <summary>Raises the <see cref="M:System.Windows.Forms.ButtonBase.OnMouseUp(System.Windows.Forms.MouseEventArgs)" /> event.</summary>
		/// <param name="mevent">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data. </param>
		// Token: 0x060000EC RID: 236 RVA: 0x000047F0 File Offset: 0x000029F0
		protected override void OnMouseUp(MouseEventArgs mevent)
		{
			if (base.Capture && (mevent.Button & MouseButtons.Left) != MouseButtons.None)
			{
				base.Capture = false;
				if (this.is_pressed)
				{
					this.is_pressed = false;
					base.Invalidate();
				}
				else if (this.flat_style == FlatStyle.Flat || this.flat_style == FlatStyle.Popup)
				{
					base.Invalidate();
				}
				if (base.ClientRectangle.Contains(mevent.Location) && !base.ValidationFailed)
				{
					this.OnClick(EventArgs.Empty);
					this.OnMouseClick(mevent);
				}
			}
			base.OnMouseUp(mevent);
		}

		/// <summary>Raises the <see cref="M:System.Windows.Forms.ButtonBase.OnPaint(System.Windows.Forms.PaintEventArgs)" /> event.</summary>
		/// <param name="pevent">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data. </param>
		// Token: 0x060000ED RID: 237 RVA: 0x0000487F File Offset: 0x00002A7F
		protected override void OnPaint(PaintEventArgs pevent)
		{
			this.Draw(pevent);
			base.OnPaint(pevent);
		}

		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060000EE RID: 238 RVA: 0x0000488F File Offset: 0x00002A8F
		protected override void OnParentChanged(EventArgs e)
		{
			base.OnParentChanged(e);
		}

		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060000EF RID: 239 RVA: 0x00004898 File Offset: 0x00002A98
		protected override void OnTextChanged(EventArgs e)
		{
			base.Invalidate();
			base.OnTextChanged(e);
		}

		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060000F0 RID: 240 RVA: 0x000048A7 File Offset: 0x00002AA7
		protected override void OnVisibleChanged(EventArgs e)
		{
			if (!base.Visible)
			{
				this.is_pressed = false;
				this.is_entered = false;
			}
			base.OnVisibleChanged(e);
		}

		/// <param name="m">The Windows <see cref="T:System.Windows.Forms.Message" /> to process. </param>
		// Token: 0x060000F1 RID: 241 RVA: 0x000048C8 File Offset: 0x00002AC8
		protected override void WndProc(ref Message m)
		{
			Msg msg = (Msg)m.Msg;
			if (msg != Msg.WM_LBUTTONDBLCLK)
			{
				if (msg != Msg.WM_RBUTTONDBLCLK)
				{
					if (msg == Msg.WM_MBUTTONDBLCLK)
					{
						this.HaveDoubleClick();
					}
				}
				else
				{
					this.HaveDoubleClick();
				}
			}
			else
			{
				this.HaveDoubleClick();
			}
			base.WndProc(ref m);
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x00004913 File Offset: 0x00002B13
		internal bool Pressed
		{
			get
			{
				return this.is_pressed;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x0000491B File Offset: 0x00002B1B
		internal TextFormatFlags TextFormatFlags
		{
			get
			{
				return this.text_format_flags;
			}
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00004923 File Offset: 0x00002B23
		internal virtual void Draw(PaintEventArgs pevent)
		{
			ThemeEngine.Current.DrawButtonBase(pevent.Graphics, pevent.ClipRectangle, this);
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x0000493C File Offset: 0x00002B3C
		internal virtual void HaveDoubleClick()
		{
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x0000493E File Offset: 0x00002B3E
		internal override void OnPaintBackgroundInternal(PaintEventArgs e)
		{
			base.OnPaintBackground(e);
		}

		// Token: 0x040000E9 RID: 233
		private FlatStyle flat_style;

		// Token: 0x040000EA RID: 234
		private int image_index;

		// Token: 0x040000EB RID: 235
		internal Image image;

		// Token: 0x040000EC RID: 236
		internal ImageList image_list;

		// Token: 0x040000ED RID: 237
		private ContentAlignment image_alignment;

		// Token: 0x040000EE RID: 238
		internal ContentAlignment text_alignment;

		// Token: 0x040000EF RID: 239
		private bool is_default;

		// Token: 0x040000F0 RID: 240
		internal bool is_pressed;

		// Token: 0x040000F1 RID: 241
		internal StringFormat text_format;

		// Token: 0x040000F2 RID: 242
		internal bool paint_as_acceptbutton;

		// Token: 0x040000F3 RID: 243
		private FlatButtonAppearance flat_button_appearance;

		// Token: 0x040000F4 RID: 244
		private string image_key;

		// Token: 0x040000F5 RID: 245
		private TextImageRelation text_image_relation;

		// Token: 0x040000F6 RID: 246
		private TextFormatFlags text_format_flags;

		// Token: 0x040000F7 RID: 247
		private bool use_mnemonic;

		// Token: 0x040000F8 RID: 248
		private bool use_visual_style_back_color;
	}
}
