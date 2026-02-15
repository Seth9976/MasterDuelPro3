using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms.Theming;

namespace System.Windows.Forms
{
	/// <summary>Represents a standard Windows label.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020000F9 RID: 249
	[DefaultProperty("Text")]
	[Designer("System.Windows.Forms.Design.LabelDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[ToolboxItem("System.Windows.Forms.Design.AutoSizeToolboxItem,System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[DefaultBindingProperty("Text")]
	public class Label : Control
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Label" /> class.</summary>
		// Token: 0x0600089B RID: 2203 RVA: 0x00024D9C File Offset: 0x00022F9C
		public Label()
		{
			this.autosize = false;
			this.TabStop = false;
			this.string_format = new StringFormat();
			this.string_format.FormatFlags = StringFormatFlags.LineLimit;
			this.TextAlign = ContentAlignment.TopLeft;
			this.image = null;
			this.UseMnemonic = true;
			this.image_list = null;
			this.image_align = ContentAlignment.MiddleCenter;
			this.SetUseMnemonic(this.UseMnemonic);
			this.flat_style = FlatStyle.Standard;
			base.SetStyle(ControlStyles.Selectable, false);
			base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
			base.HandleCreated += this.OnHandleCreatedLB;
		}

		/// <summary>Gets or sets a value indicating whether the control is automatically resized to display its entire contents.</summary>
		/// <returns>true if the control adjusts its width to closely fit its contents; otherwise, false. NoteWhen added to a form using the designer, the default value is true. When instantiated from code, the default value is false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700021E RID: 542
		// (get) Token: 0x0600089C RID: 2204 RVA: 0x00024E4B File Offset: 0x0002304B
		// (set) Token: 0x0600089D RID: 2205 RVA: 0x00024E53 File Offset: 0x00023053
		[Browsable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		[DefaultValue(false)]
		[Localizable(true)]
		[RefreshProperties(RefreshProperties.All)]
		public override bool AutoSize
		{
			get
			{
				return this.autosize;
			}
			set
			{
				if (this.autosize == value)
				{
					return;
				}
				base.SetAutoSizeMode(AutoSizeMode.GrowAndShrink);
				base.AutoSize = value;
				this.autosize = value;
				this.CalcAutoSize();
				base.Invalidate();
				this.OnAutoSizeChanged(EventArgs.Empty);
			}
		}

		/// <summary>Gets or sets the image rendered on the background of the control.</summary>
		/// <returns>An <see cref="T:System.Drawing.Image" /> that represents the background image of the control. The default is null.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700021F RID: 543
		// (get) Token: 0x0600089E RID: 2206 RVA: 0x00005B72 File Offset: 0x00003D72
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override Image BackgroundImage
		{
			get
			{
				return base.BackgroundImage;
			}
		}

		/// <summary>This property is not relevant for this class.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.ImageLayout" /> object.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000220 RID: 544
		// (get) Token: 0x0600089F RID: 2207 RVA: 0x00005B7A File Offset: 0x00003D7A
		// (set) Token: 0x060008A0 RID: 2208 RVA: 0x00005B82 File Offset: 0x00003D82
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override ImageLayout BackgroundImageLayout
		{
			get
			{
				return base.BackgroundImageLayout;
			}
			set
			{
				base.BackgroundImageLayout = value;
			}
		}

		/// <summary>Gets or sets the border style for the control.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.BorderStyle" /> values. The default is BorderStyle.None.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="T:System.Windows.Forms.BorderStyle" /> values. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000221 RID: 545
		// (get) Token: 0x060008A1 RID: 2209 RVA: 0x00024E8B File Offset: 0x0002308B
		[DefaultValue(BorderStyle.None)]
		[DispId(-504)]
		public virtual BorderStyle BorderStyle
		{
			get
			{
				return base.InternalBorderStyle;
			}
		}

		/// <summary>Gets the required creation parameters when the control handle is created.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.CreateParams" /> that contains the required creation parameters when the handle to the control is created.</returns>
		// Token: 0x17000222 RID: 546
		// (get) Token: 0x060008A2 RID: 2210 RVA: 0x00024E94 File Offset: 0x00023094
		protected override CreateParams CreateParams
		{
			get
			{
				CreateParams createParams = base.CreateParams;
				if (this.BorderStyle != BorderStyle.Fixed3D)
				{
					return createParams;
				}
				createParams.ExStyle &= -513;
				createParams.ExStyle |= 131072;
				return createParams;
			}
		}

		/// <summary>Gets the space, in pixels, that is specified by default between controls.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Padding" /> value that represents the default space between controls.</returns>
		// Token: 0x17000223 RID: 547
		// (get) Token: 0x060008A3 RID: 2211 RVA: 0x00024ED8 File Offset: 0x000230D8
		protected override Padding DefaultMargin
		{
			get
			{
				return new Padding(3, 0, 3, 0);
			}
		}

		/// <summary>Gets the default size of the control.</summary>
		/// <returns>The default <see cref="T:System.Drawing.Size" /> of the control.</returns>
		// Token: 0x17000224 RID: 548
		// (get) Token: 0x060008A4 RID: 2212 RVA: 0x00024EE3 File Offset: 0x000230E3
		protected override Size DefaultSize
		{
			get
			{
				return ThemeElements.LabelPainter.DefaultSize;
			}
		}

		/// <summary>Gets or sets the flat style appearance of the label control.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.FlatStyle" /> values. The default value is Standard.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="T:System.Windows.Forms.FlatStyle" /> values. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000225 RID: 549
		// (set) Token: 0x060008A5 RID: 2213 RVA: 0x00024EF0 File Offset: 0x000230F0
		[DefaultValue(FlatStyle.Standard)]
		public FlatStyle FlatStyle
		{
			set
			{
				if (!Enum.IsDefined(typeof(FlatStyle), value))
				{
					throw new InvalidEnumArgumentException(string.Format("Enum argument value '{0}' is not valid for FlatStyle", value));
				}
				if (this.flat_style == value)
				{
					return;
				}
				this.flat_style = value;
				if (base.Parent != null)
				{
					base.Parent.PerformLayout(this, "FlatStyle");
				}
				base.Invalidate();
			}
		}

		/// <summary>Gets or sets the image that is displayed on a <see cref="T:System.Windows.Forms.Label" />.</summary>
		/// <returns>An <see cref="T:System.Drawing.Image" /> displayed on the <see cref="T:System.Windows.Forms.Label" />. The default is null.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000226 RID: 550
		// (get) Token: 0x060008A6 RID: 2214 RVA: 0x00024F5C File Offset: 0x0002315C
		[Localizable(true)]
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

		/// <summary>Gets or sets the alignment of an image that is displayed in the control.</summary>
		/// <returns>One of the <see cref="T:System.Drawing.ContentAlignment" /> values. The default is ContentAlignment.MiddleCenter.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="T:System.Drawing.ContentAlignment" /> values. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000227 RID: 551
		// (get) Token: 0x060008A7 RID: 2215 RVA: 0x00024FCD File Offset: 0x000231CD
		[DefaultValue(ContentAlignment.MiddleCenter)]
		[Localizable(true)]
		public ContentAlignment ImageAlign
		{
			get
			{
				return this.image_align;
			}
		}

		// Token: 0x060008A8 RID: 2216 RVA: 0x00024FD8 File Offset: 0x000231D8
		internal virtual Size InternalGetPreferredSize(Size proposed)
		{
			Size size;
			if (this.Text == string.Empty)
			{
				size = new Size(0, this.Font.Height);
			}
			else
			{
				size = Size.Ceiling(TextRenderer.MeasureString(this.Text, this.Font, Label.req_witdthsize, this.string_format));
				size.Width += 3;
			}
			size.Width += base.Padding.Horizontal;
			size.Height += base.Padding.Vertical;
			if (!this.use_compatible_text_rendering)
			{
				return size;
			}
			if (this.border_style == BorderStyle.None)
			{
				size.Height += 3;
			}
			else
			{
				size.Height += 6;
			}
			return size;
		}

		/// <summary>Retrieves the size of a rectangular area into which a control can be fitted. </summary>
		/// <returns>An ordered pair of type <see cref="T:System.Drawing.Size" /> representing the width and height of a rectangle.</returns>
		/// <param name="proposedSize">The custom-sized area for a control.</param>
		// Token: 0x060008A9 RID: 2217 RVA: 0x000250A8 File Offset: 0x000232A8
		public override Size GetPreferredSize(Size proposedSize)
		{
			return this.InternalGetPreferredSize(proposedSize);
		}

		/// <summary>Gets or sets a value indicating whether the user can tab to the <see cref="T:System.Windows.Forms.Label" />. This property is not used by this class.</summary>
		/// <returns>This property is not used by this class. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000228 RID: 552
		// (set) Token: 0x060008AA RID: 2218 RVA: 0x000208ED File Offset: 0x0001EAED
		[Browsable(false)]
		[DefaultValue(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new bool TabStop
		{
			set
			{
				base.TabStop = value;
			}
		}

		/// <summary>Gets or sets the alignment of text in the label.</summary>
		/// <returns>One of the <see cref="T:System.Drawing.ContentAlignment" /> values. The default is <see cref="F:System.Drawing.ContentAlignment.TopLeft" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="T:System.Drawing.ContentAlignment" /> values. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000229 RID: 553
		// (set) Token: 0x060008AB RID: 2219 RVA: 0x000250B4 File Offset: 0x000232B4
		[DefaultValue(ContentAlignment.TopLeft)]
		[Localizable(true)]
		public virtual ContentAlignment TextAlign
		{
			set
			{
				if (!Enum.IsDefined(typeof(ContentAlignment), value))
				{
					throw new InvalidEnumArgumentException(string.Format("Enum argument value '{0}' is not valid for ContentAlignment", value));
				}
				if (this.text_align != value)
				{
					this.text_align = value;
					if (value <= ContentAlignment.MiddleCenter)
					{
						switch (value)
						{
						case ContentAlignment.TopLeft:
							this.string_format.LineAlignment = StringAlignment.Near;
							this.string_format.Alignment = StringAlignment.Near;
							break;
						case ContentAlignment.TopCenter:
							this.string_format.LineAlignment = StringAlignment.Near;
							this.string_format.Alignment = StringAlignment.Center;
							break;
						case (ContentAlignment)3:
							break;
						case ContentAlignment.TopRight:
							this.string_format.LineAlignment = StringAlignment.Near;
							this.string_format.Alignment = StringAlignment.Far;
							break;
						default:
							if (value != ContentAlignment.MiddleLeft)
							{
								if (value == ContentAlignment.MiddleCenter)
								{
									this.string_format.LineAlignment = StringAlignment.Center;
									this.string_format.Alignment = StringAlignment.Center;
								}
							}
							else
							{
								this.string_format.LineAlignment = StringAlignment.Center;
								this.string_format.Alignment = StringAlignment.Near;
							}
							break;
						}
					}
					else if (value <= ContentAlignment.BottomLeft)
					{
						if (value != ContentAlignment.MiddleRight)
						{
							if (value == ContentAlignment.BottomLeft)
							{
								this.string_format.LineAlignment = StringAlignment.Far;
								this.string_format.Alignment = StringAlignment.Near;
							}
						}
						else
						{
							this.string_format.LineAlignment = StringAlignment.Center;
							this.string_format.Alignment = StringAlignment.Far;
						}
					}
					else if (value != ContentAlignment.BottomCenter)
					{
						if (value == ContentAlignment.BottomRight)
						{
							this.string_format.LineAlignment = StringAlignment.Far;
							this.string_format.Alignment = StringAlignment.Far;
						}
					}
					else
					{
						this.string_format.LineAlignment = StringAlignment.Far;
						this.string_format.Alignment = StringAlignment.Center;
					}
					this.OnTextAlignChanged(EventArgs.Empty);
					base.Invalidate();
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the control interprets an ampersand character (&amp;) in the control's <see cref="P:System.Windows.Forms.Control.Text" /> property to be an access key prefix character.</summary>
		/// <returns>true if the label doesn't display the ampersand character and underlines the character after the ampersand in its displayed text and treats the underlined character as an access key; otherwise, false if the ampersand character is displayed in the text of the control. The default is true.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700022A RID: 554
		// (get) Token: 0x060008AC RID: 2220 RVA: 0x0002526A File Offset: 0x0002346A
		// (set) Token: 0x060008AD RID: 2221 RVA: 0x00025272 File Offset: 0x00023472
		[DefaultValue(true)]
		public bool UseMnemonic
		{
			get
			{
				return this.use_mnemonic;
			}
			set
			{
				if (this.use_mnemonic != value)
				{
					this.use_mnemonic = value;
					this.SetUseMnemonic(this.use_mnemonic);
					base.Invalidate();
				}
			}
		}

		/// <summary>Determines the size and location of an image drawn within the <see cref="T:System.Windows.Forms.Label" /> control based on the alignment of the control.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the size and location of the specified image within the control.</returns>
		/// <param name="image">The <see cref="T:System.Drawing.Image" /> used to determine size and location when drawn within the control. </param>
		/// <param name="r">A <see cref="T:System.Drawing.Rectangle" /> that represents the area to draw the image in. </param>
		/// <param name="align">The alignment of content within the control. </param>
		// Token: 0x060008AE RID: 2222 RVA: 0x00025298 File Offset: 0x00023498
		protected Rectangle CalcImageRenderBounds(Image image, Rectangle r, ContentAlignment align)
		{
			Rectangle rectangle = r;
			rectangle.Inflate(-2, -2);
			int num = r.X;
			int num2 = r.Y;
			if (align == ContentAlignment.TopCenter || align == ContentAlignment.MiddleCenter || align == ContentAlignment.BottomCenter)
			{
				num += (r.Width - image.Width) / 2;
			}
			else if (align == ContentAlignment.TopRight || align == ContentAlignment.MiddleRight || align == ContentAlignment.BottomRight)
			{
				num += r.Width - image.Width;
			}
			if (align == ContentAlignment.BottomCenter || align == ContentAlignment.BottomLeft || align == ContentAlignment.BottomRight)
			{
				num2 += r.Height - image.Height;
			}
			else if (align == ContentAlignment.MiddleCenter || align == ContentAlignment.MiddleLeft || align == ContentAlignment.MiddleRight)
			{
				num2 += (r.Height - image.Height) / 2;
			}
			rectangle.X = num;
			rectangle.Y = num2;
			rectangle.Width = image.Width;
			rectangle.Height = image.Height;
			return rectangle;
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.Label" /> and optionally releases the managed resources. </summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x060008AF RID: 2223 RVA: 0x00025382 File Offset: 0x00023582
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			if (disposing)
			{
				this.string_format.Dispose();
			}
		}

		/// <summary>Draws an <see cref="T:System.Drawing.Image" /> within the specified bounds.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> surface on which to draw. </param>
		/// <param name="image">The <see cref="T:System.Drawing.Image" /> to draw. </param>
		/// <param name="r">The <see cref="T:System.Drawing.Rectangle" /> bounds to draw within. </param>
		/// <param name="align">The alignment of the image to draw within the <see cref="T:System.Windows.Forms.Label" />. </param>
		// Token: 0x060008B0 RID: 2224 RVA: 0x0002539C File Offset: 0x0002359C
		protected internal void DrawImage(Graphics g, Image image, Rectangle r, ContentAlignment align)
		{
			if (image == null || g == null)
			{
				return;
			}
			Rectangle rectangle = this.CalcImageRenderBounds(image, r, align);
			if (base.Enabled)
			{
				g.DrawImage(image, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
				return;
			}
			ControlPaint.DrawImageDisabled(g, image, rectangle.X, rectangle.Y, this.BackColor);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.EnabledChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060008B1 RID: 2225 RVA: 0x000046A9 File Offset: 0x000028A9
		protected override void OnEnabledChanged(EventArgs e)
		{
			base.OnEnabledChanged(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.FontChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060008B2 RID: 2226 RVA: 0x00025402 File Offset: 0x00023602
		protected override void OnFontChanged(EventArgs e)
		{
			base.OnFontChanged(e);
			if (this.autosize)
			{
				this.CalcAutoSize();
			}
			base.Invalidate();
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.PaddingChanged" /> event.</summary>
		/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060008B3 RID: 2227 RVA: 0x0002541F File Offset: 0x0002361F
		protected override void OnPaddingChanged(EventArgs e)
		{
			base.OnPaddingChanged(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
		// Token: 0x060008B4 RID: 2228 RVA: 0x00025428 File Offset: 0x00023628
		protected override void OnPaint(PaintEventArgs e)
		{
			ThemeElements.LabelPainter.Draw(e.Graphics, base.ClientRectangle, this);
			base.OnPaint(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.ParentChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060008B5 RID: 2229 RVA: 0x0000488F File Offset: 0x00002A8F
		protected override void OnParentChanged(EventArgs e)
		{
			base.OnParentChanged(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.RightToLeftChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060008B6 RID: 2230 RVA: 0x00025448 File Offset: 0x00023648
		protected override void OnRightToLeftChanged(EventArgs e)
		{
			base.OnRightToLeftChanged(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Label.TextAlignChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060008B7 RID: 2231 RVA: 0x00025454 File Offset: 0x00023654
		protected virtual void OnTextAlignChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Label.TextAlignChangedEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.TextChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060008B8 RID: 2232 RVA: 0x00025482 File Offset: 0x00023682
		protected override void OnTextChanged(EventArgs e)
		{
			base.OnTextChanged(e);
			if (this.autosize)
			{
				this.CalcAutoSize();
			}
			base.Invalidate();
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.VisibleChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060008B9 RID: 2233 RVA: 0x0002549F File Offset: 0x0002369F
		protected override void OnVisibleChanged(EventArgs e)
		{
			base.OnVisibleChanged(e);
		}

		/// <summary>Processes a mnemonic character.</summary>
		/// <returns>true if the character was processed as a mnemonic by the control; otherwise, false.</returns>
		/// <param name="charCode">The character to process.</param>
		// Token: 0x060008BA RID: 2234 RVA: 0x000254A8 File Offset: 0x000236A8
		protected override bool ProcessMnemonic(char charCode)
		{
			if (Control.IsMnemonic(charCode, this.Text))
			{
				if (base.Parent != null)
				{
					base.Parent.SelectNextControl(this, true, false, false, false);
				}
				return true;
			}
			return base.ProcessMnemonic(charCode);
		}

		/// <summary>Sets the specified bounds of the label.</summary>
		/// <param name="x">The new <see cref="P:System.Windows.Forms.Control.Left" /> property value of the control. </param>
		/// <param name="y">The new <see cref="P:System.Windows.Forms.Control.Top" /> property value of the control. </param>
		/// <param name="width">The new <see cref="P:System.Windows.Forms.Control.Width" /> property value of the control. </param>
		/// <param name="height">The new <see cref="P:System.Windows.Forms.Control.Height" /> property value of the control. </param>
		/// <param name="specified">A bitwise combination of the <see cref="T:System.Windows.Forms.BoundsSpecified" /> values. For any parameter not specified, the current value will be used. </param>
		// Token: 0x060008BB RID: 2235 RVA: 0x000254DA File Offset: 0x000236DA
		protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
		{
			base.SetBoundsCore(x, y, width, height, specified);
		}

		/// <summary>Returns a string that represents the current <see cref="T:System.Windows.Forms.Label" />.</summary>
		/// <returns>A string that represents the current <see cref="T:System.Windows.Forms.Label" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060008BC RID: 2236 RVA: 0x00004056 File Offset: 0x00002256
		public override string ToString()
		{
			return base.ToString() + ", Text: " + this.Text;
		}

		/// <summary>Processes Windows messages.</summary>
		/// <param name="m">The Windows <see cref="T:System.Windows.Forms.Message" /> to process.</param>
		// Token: 0x060008BD RID: 2237 RVA: 0x000254EC File Offset: 0x000236EC
		protected override void WndProc(ref Message m)
		{
			Msg msg = (Msg)m.Msg;
			if (msg == Msg.WM_DRAWITEM)
			{
				m.Result = (IntPtr)1;
				return;
			}
			base.WndProc(ref m);
		}

		// Token: 0x060008BE RID: 2238 RVA: 0x0002551C File Offset: 0x0002371C
		private void CalcAutoSize()
		{
			if (!this.AutoSize)
			{
				return;
			}
			Size size = this.InternalGetPreferredSize(Size.Empty);
			base.SetBounds(base.Left, base.Top, size.Width, size.Height, BoundsSpecified.Size);
		}

		// Token: 0x060008BF RID: 2239 RVA: 0x00025560 File Offset: 0x00023760
		private void OnHandleCreatedLB(object o, EventArgs e)
		{
			if (this.autosize)
			{
				this.CalcAutoSize();
			}
		}

		// Token: 0x060008C0 RID: 2240 RVA: 0x00025570 File Offset: 0x00023770
		private void SetUseMnemonic(bool use)
		{
			if (use)
			{
				this.string_format.HotkeyPrefix = HotkeyPrefix.Show;
				return;
			}
			this.string_format.HotkeyPrefix = HotkeyPrefix.None;
		}

		/// <summary>Gets or sets the text associated with this control.</summary>
		/// <returns>The text associated with this control.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700022B RID: 555
		// (get) Token: 0x060008C1 RID: 2241 RVA: 0x000043B4 File Offset: 0x000025B4
		// (set) Token: 0x060008C2 RID: 2242 RVA: 0x000043BC File Offset: 0x000025BC
		[SettingsBindable(true)]
		[Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
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

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseEnter" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060008C3 RID: 2243 RVA: 0x0002558E File Offset: 0x0002378E
		protected override void OnMouseEnter(EventArgs e)
		{
			base.OnMouseEnter(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseLeave" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060008C4 RID: 2244 RVA: 0x00025597 File Offset: 0x00023797
		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.HandleDestroyed" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060008C5 RID: 2245 RVA: 0x00006538 File Offset: 0x00004738
		protected override void OnHandleDestroyed(EventArgs e)
		{
			base.OnHandleDestroyed(e);
		}

		// Token: 0x0400064C RID: 1612
		private bool autosize;

		// Token: 0x0400064D RID: 1613
		private Image image;

		// Token: 0x0400064E RID: 1614
		private FlatStyle flat_style;

		// Token: 0x0400064F RID: 1615
		private bool use_mnemonic;

		// Token: 0x04000650 RID: 1616
		private int image_index = -1;

		// Token: 0x04000651 RID: 1617
		private string image_key = string.Empty;

		// Token: 0x04000652 RID: 1618
		private ImageList image_list;

		// Token: 0x04000653 RID: 1619
		internal ContentAlignment image_align;

		// Token: 0x04000654 RID: 1620
		internal StringFormat string_format;

		// Token: 0x04000655 RID: 1621
		internal ContentAlignment text_align;

		// Token: 0x04000656 RID: 1622
		private static SizeF req_witdthsize = new SizeF(0f, 0f);

		// Token: 0x04000657 RID: 1623
		private static object AutoSizeChangedEvent = new object();

		// Token: 0x04000658 RID: 1624
		private static object TextAlignChangedEvent = new object();
	}
}
