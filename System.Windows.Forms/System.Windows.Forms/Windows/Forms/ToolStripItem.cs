using System;
using System.ComponentModel;
using System.Drawing;

namespace System.Windows.Forms
{
	/// <summary>Represents the abstract base class that manages events and layout for all the elements that a <see cref="T:System.Windows.Forms.ToolStrip" /> or <see cref="T:System.Windows.Forms.ToolStripDropDown" /> can contain.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020001CC RID: 460
	[DefaultEvent("Click")]
	[DefaultProperty("Text")]
	[DesignTimeVisible(false)]
	[ToolboxItem(false)]
	[Designer("System.Windows.Forms.Design.ToolStripItemDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	public abstract class ToolStripItem : Component, IComponent, IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripItem" /> class.</summary>
		// Token: 0x060013A8 RID: 5032 RVA: 0x00063769 File Offset: 0x00061969
		protected ToolStripItem()
			: this(string.Empty, null, null, string.Empty)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripItem" /> class with the specified display text, image, event handler, and name. </summary>
		/// <param name="text">The text to display on the <see cref="T:System.Windows.Forms.ToolStripItem" />.</param>
		/// <param name="image">The Image to display on the <see cref="T:System.Windows.Forms.ToolStripItem" />.</param>
		/// <param name="onClick">The event handler for the <see cref="E:System.Windows.Forms.ToolStripItem.Click" /> event.</param>
		/// <param name="name">The name of the <see cref="T:System.Windows.Forms.ToolStripItem" />.</param>
		// Token: 0x060013A9 RID: 5033 RVA: 0x00063780 File Offset: 0x00061980
		protected ToolStripItem(string text, Image image, EventHandler onClick, string name)
		{
			this.alignment = ToolStripItemAlignment.Left;
			this.anchor = AnchorStyles.Top | AnchorStyles.Left;
			this.auto_size = true;
			this.auto_tool_tip = this.DefaultAutoToolTip;
			this.available = true;
			this.back_color = Color.Empty;
			this.background_image_layout = ImageLayout.Tile;
			this.can_select = true;
			this.display_style = this.DefaultDisplayStyle;
			this.dock = DockStyle.None;
			this.enabled = true;
			this.fore_color = Color.Empty;
			this.image = image;
			this.image_align = ContentAlignment.MiddleCenter;
			this.image_index = -1;
			this.image_key = string.Empty;
			this.image_scaling = ToolStripItemImageScaling.SizeToFit;
			this.image_transparent_color = Color.Empty;
			this.margin = this.DefaultMargin;
			this.merge_action = MergeAction.Append;
			this.merge_index = -1;
			this.name = name;
			this.overflow = ToolStripItemOverflow.AsNeeded;
			this.padding = this.DefaultPadding;
			this.placement = ToolStripItemPlacement.None;
			this.right_to_left = RightToLeft.Inherit;
			this.bounds.Size = this.DefaultSize;
			this.text = text;
			this.text_align = ContentAlignment.MiddleCenter;
			this.text_direction = this.DefaultTextDirection;
			this.text_image_relation = TextImageRelation.ImageBeforeText;
			this.visible = true;
			this.Click += onClick;
			this.OnLayout(new LayoutEventArgs(null, string.Empty));
		}

		/// <summary>Gets or sets a value indicating whether the item aligns towards the beginning or end of the <see cref="T:System.Windows.Forms.ToolStrip" />.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ToolStripItemAlignment" /> values. The default is <see cref="F:System.Windows.Forms.ToolStripItemAlignment.Left" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="T:System.Windows.Forms.ToolStripItemAlignment" /> values. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000522 RID: 1314
		// (get) Token: 0x060013AA RID: 5034 RVA: 0x000638C1 File Offset: 0x00061AC1
		// (set) Token: 0x060013AB RID: 5035 RVA: 0x000638CC File Offset: 0x00061ACC
		[DefaultValue(ToolStripItemAlignment.Left)]
		public ToolStripItemAlignment Alignment
		{
			get
			{
				return this.alignment;
			}
			set
			{
				if (!Enum.IsDefined(typeof(ToolStripItemAlignment), value))
				{
					throw new InvalidEnumArgumentException(string.Format("Enum argument value '{0}' is not valid for ToolStripItemAlignment", value));
				}
				if (this.alignment != value)
				{
					this.alignment = value;
					this.CalculateAutoSize();
				}
			}
		}

		/// <summary>Gets or sets the edges of the container to which a <see cref="T:System.Windows.Forms.ToolStripItem" /> is bound and determines how a <see cref="T:System.Windows.Forms.ToolStripItem" />  is resized with its parent.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.AnchorStyles" /> values.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value is not one of the <see cref="T:System.Windows.Forms.AnchorStyles" /> values.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x060013AC RID: 5036 RVA: 0x0006391C File Offset: 0x00061B1C
		[Browsable(false)]
		[DefaultValue(AnchorStyles.Top | AnchorStyles.Left)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public AnchorStyles Anchor
		{
			get
			{
				return this.anchor;
			}
		}

		/// <summary>Gets or sets a value indicating whether the item is automatically sized.</summary>
		/// <returns>true if the <see cref="T:System.Windows.Forms.ToolStripItem" /> is automatically sized; otherwise, false. The default value is true.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x060013AD RID: 5037 RVA: 0x00063924 File Offset: 0x00061B24
		// (set) Token: 0x060013AE RID: 5038 RVA: 0x0006392C File Offset: 0x00061B2C
		[Localizable(true)]
		[DefaultValue(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[RefreshProperties(RefreshProperties.All)]
		public bool AutoSize
		{
			get
			{
				return this.auto_size;
			}
			set
			{
				this.auto_size = value;
				this.CalculateAutoSize();
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.ToolStripItem" /> should be placed on a <see cref="T:System.Windows.Forms.ToolStrip" />.</summary>
		/// <returns>true if the <see cref="T:System.Windows.Forms.ToolStripItem" /> is placed on a <see cref="T:System.Windows.Forms.ToolStrip" />; otherwise, false.</returns>
		// Token: 0x17000525 RID: 1317
		// (get) Token: 0x060013AF RID: 5039 RVA: 0x0006393B File Offset: 0x00061B3B
		// (set) Token: 0x060013B0 RID: 5040 RVA: 0x00063944 File Offset: 0x00061B44
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool Available
		{
			get
			{
				return this.available;
			}
			set
			{
				if (this.available != value)
				{
					this.available = value;
					this.visible = value;
					if (this.parent != null)
					{
						this.parent.PerformLayout();
					}
					this.OnAvailableChanged(EventArgs.Empty);
					this.OnVisibleChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Gets or sets the background color for the item.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the background color of the item. The default is the value of the <see cref="P:System.Windows.Forms.Control.DefaultBackColor" /> property.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000526 RID: 1318
		// (get) Token: 0x060013B1 RID: 5041 RVA: 0x00063991 File Offset: 0x00061B91
		public virtual Color BackColor
		{
			get
			{
				if (this.back_color != Color.Empty)
				{
					return this.back_color;
				}
				if (this.Parent != null)
				{
					return this.parent.BackColor;
				}
				return Control.DefaultBackColor;
			}
		}

		/// <summary>Gets or sets the background image displayed in the item.</summary>
		/// <returns>An <see cref="T:System.Drawing.Image" /> that represents the image to display in the background of the item.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000527 RID: 1319
		// (get) Token: 0x060013B2 RID: 5042 RVA: 0x000639C5 File Offset: 0x00061BC5
		[Localizable(true)]
		[DefaultValue(null)]
		public virtual Image BackgroundImage
		{
			get
			{
				return this.background_image;
			}
		}

		/// <summary>Gets or sets the background image layout used for the <see cref="T:System.Windows.Forms.ToolStripItem" />.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ImageLayout" /> values. The default value is <see cref="F:System.Windows.Forms.ImageLayout.Tile" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000528 RID: 1320
		// (get) Token: 0x060013B3 RID: 5043 RVA: 0x000639CD File Offset: 0x00061BCD
		[Localizable(true)]
		[DefaultValue(ImageLayout.Tile)]
		public virtual ImageLayout BackgroundImageLayout
		{
			get
			{
				return this.background_image_layout;
			}
		}

		/// <summary>Gets the size and location of the item.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the size and location of the <see cref="T:System.Windows.Forms.ToolStripItem" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000529 RID: 1321
		// (get) Token: 0x060013B4 RID: 5044 RVA: 0x000639D5 File Offset: 0x00061BD5
		[Browsable(false)]
		public virtual Rectangle Bounds
		{
			get
			{
				return this.bounds;
			}
		}

		/// <summary>Gets a value indicating whether the item can be selected.</summary>
		/// <returns>true if the <see cref="T:System.Windows.Forms.ToolStripItem" /> can be selected; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700052A RID: 1322
		// (get) Token: 0x060013B5 RID: 5045 RVA: 0x000639DD File Offset: 0x00061BDD
		[Browsable(false)]
		public virtual bool CanSelect
		{
			get
			{
				return this.can_select;
			}
		}

		/// <summary>Gets the area where content, such as text and icons, can be placed within a <see cref="T:System.Windows.Forms.ToolStripItem" /> without overwriting background borders.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> containing four integers that represent the location and size of <see cref="T:System.Windows.Forms.ToolStripItem" /> contents, excluding its border.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700052B RID: 1323
		// (get) Token: 0x060013B6 RID: 5046 RVA: 0x000639E8 File Offset: 0x00061BE8
		[Browsable(false)]
		public Rectangle ContentRectangle
		{
			get
			{
				if (this is ToolStripLabel || this is ToolStripStatusLabel)
				{
					return new Rectangle(0, 0, this.bounds.Width, this.bounds.Height);
				}
				if (this is ToolStripDropDownButton && (this as ToolStripDropDownButton).ShowDropDownArrow)
				{
					return new Rectangle(2, 2, this.bounds.Width - 13, this.bounds.Height - 4);
				}
				return new Rectangle(2, 2, this.bounds.Width - 4, this.bounds.Height - 4);
			}
		}

		/// <summary>Gets or sets whether text and images are displayed on a <see cref="T:System.Windows.Forms.ToolStripItem" />.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ToolStripItemDisplayStyle" /> values. The default is <see cref="F:System.Windows.Forms.ToolStripItemDisplayStyle.ImageAndText" /> .</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700052C RID: 1324
		// (set) Token: 0x060013B7 RID: 5047 RVA: 0x00063A7C File Offset: 0x00061C7C
		public virtual ToolStripItemDisplayStyle DisplayStyle
		{
			set
			{
				if (this.display_style != value)
				{
					this.display_style = value;
					this.CalculateAutoSize();
					this.OnDisplayStyleChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Gets a value indicating whether the object has been disposed of.</summary>
		/// <returns>true if the control has been disposed of; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700052D RID: 1325
		// (get) Token: 0x060013B8 RID: 5048 RVA: 0x00063A9F File Offset: 0x00061C9F
		[Browsable(false)]
		public bool IsDisposed
		{
			get
			{
				return this.is_disposed;
			}
		}

		/// <summary>Gets or sets which <see cref="T:System.Windows.Forms.ToolStripItem" /> borders are docked to its parent control and determines how a <see cref="T:System.Windows.Forms.ToolStripItem" /> is resized with its parent.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DockStyle" /> values. The default is <see cref="F:System.Windows.Forms.DockStyle.None" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="T:System.Windows.Forms.DockStyle" /> values.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700052E RID: 1326
		// (get) Token: 0x060013B9 RID: 5049 RVA: 0x00063AA7 File Offset: 0x00061CA7
		// (set) Token: 0x060013BA RID: 5050 RVA: 0x00063AB0 File Offset: 0x00061CB0
		[Browsable(false)]
		[DefaultValue(DockStyle.None)]
		public DockStyle Dock
		{
			get
			{
				return this.dock;
			}
			set
			{
				if (this.dock != value)
				{
					if (!Enum.IsDefined(typeof(DockStyle), value))
					{
						throw new InvalidEnumArgumentException(string.Format("Enum argument value '{0}' is not valid for DockStyle", value));
					}
					this.dock = value;
					this.CalculateAutoSize();
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the parent control of the <see cref="T:System.Windows.Forms.ToolStripItem" /> is enabled. </summary>
		/// <returns>true if the parent control of the <see cref="T:System.Windows.Forms.ToolStripItem" /> is enabled; otherwise, false. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700052F RID: 1327
		// (get) Token: 0x060013BB RID: 5051 RVA: 0x00063B00 File Offset: 0x00061D00
		// (set) Token: 0x060013BC RID: 5052 RVA: 0x00063B36 File Offset: 0x00061D36
		[Localizable(true)]
		[DefaultValue(true)]
		public virtual bool Enabled
		{
			get
			{
				return (this.Parent == null || this.Parent.Enabled) && (this.Owner == null || this.Owner.Enabled) && this.enabled;
			}
			set
			{
				if (this.enabled != value)
				{
					this.enabled = value;
					this.OnEnabledChanged(EventArgs.Empty);
					this.Invalidate();
				}
			}
		}

		/// <summary>Gets or sets the font of the text displayed by the item.</summary>
		/// <returns>The <see cref="T:System.Drawing.Font" /> to apply to the text displayed by the <see cref="T:System.Windows.Forms.ToolStripItem" />. The default is the value of the <see cref="P:System.Windows.Forms.Control.DefaultFont" /> property.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000530 RID: 1328
		// (get) Token: 0x060013BD RID: 5053 RVA: 0x00063B59 File Offset: 0x00061D59
		[Localizable(true)]
		public virtual Font Font
		{
			get
			{
				if (this.font != null)
				{
					return this.font;
				}
				if (this.Parent != null)
				{
					return this.Parent.Font;
				}
				return ToolStripItem.DefaultFont;
			}
		}

		/// <summary>Gets or sets the foreground color of the item.</summary>
		/// <returns>The foreground <see cref="T:System.Drawing.Color" /> of the item. The default is the value of the <see cref="P:System.Windows.Forms.Control.DefaultForeColor" /> property.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000531 RID: 1329
		// (get) Token: 0x060013BE RID: 5054 RVA: 0x00063B83 File Offset: 0x00061D83
		public virtual Color ForeColor
		{
			get
			{
				if (this.fore_color != Color.Empty)
				{
					return this.fore_color;
				}
				if (this.Parent != null)
				{
					return this.parent.ForeColor;
				}
				return Control.DefaultForeColor;
			}
		}

		/// <summary>Gets or sets the height, in pixels, of a <see cref="T:System.Windows.Forms.ToolStripItem" />.</summary>
		/// <returns>An <see cref="T:System.Int32" /> representing the height, in pixels.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000532 RID: 1330
		// (get) Token: 0x060013BF RID: 5055 RVA: 0x00063BB8 File Offset: 0x00061DB8
		// (set) Token: 0x060013C0 RID: 5056 RVA: 0x00063BD4 File Offset: 0x00061DD4
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int Height
		{
			get
			{
				return this.Size.Height;
			}
			set
			{
				this.Size = new Size(this.Size.Width, value);
				this.explicit_size.Height = value;
				if (this.Visible)
				{
					this.CalculateAutoSize();
					this.OnBoundsChanged();
					this.Invalidate();
				}
			}
		}

		/// <summary>Gets or sets the image that is displayed on a <see cref="T:System.Windows.Forms.ToolStripItem" />.</summary>
		/// <returns>The <see cref="T:System.Drawing.Image" /> to be displayed.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x060013C1 RID: 5057 RVA: 0x00063C24 File Offset: 0x00061E24
		// (set) Token: 0x060013C2 RID: 5058 RVA: 0x00063CF3 File Offset: 0x00061EF3
		[Localizable(true)]
		public virtual Image Image
		{
			get
			{
				if (this.image != null)
				{
					return this.image;
				}
				if (this.image_index >= 0 && this.owner != null && this.owner.ImageList != null && this.owner.ImageList.Images.Count > this.image_index)
				{
					return this.owner.ImageList.Images[this.image_index];
				}
				if (!string.IsNullOrEmpty(this.image_key) && this.owner != null && this.owner.ImageList != null && this.owner.ImageList.Images.Count > this.image_index)
				{
					return this.owner.ImageList.Images[this.image_key];
				}
				return null;
			}
			set
			{
				if (this.image != value)
				{
					this.StopAnimation();
					this.image = value;
					this.image_index = -1;
					this.image_key = string.Empty;
					this.CalculateAutoSize();
					this.Invalidate();
					this.BeginAnimation();
				}
			}
		}

		/// <summary>Gets or sets the color to treat as transparent in a <see cref="T:System.Windows.Forms.ToolStripItem" /> image.</summary>
		/// <returns>One of the <see cref="T:System.Drawing.Color" /> values.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000534 RID: 1332
		// (get) Token: 0x060013C3 RID: 5059 RVA: 0x00063D2F File Offset: 0x00061F2F
		[Localizable(true)]
		public Color ImageTransparentColor
		{
			get
			{
				return this.image_transparent_color;
			}
		}

		/// <summary>Gets a value indicating whether the container of the current <see cref="T:System.Windows.Forms.Control" /> is a <see cref="T:System.Windows.Forms.ToolStripDropDown" />. </summary>
		/// <returns>true if the container of the current <see cref="T:System.Windows.Forms.Control" /> is a <see cref="T:System.Windows.Forms.ToolStripDropDown" />; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000535 RID: 1333
		// (get) Token: 0x060013C4 RID: 5060 RVA: 0x00063D37 File Offset: 0x00061F37
		[Browsable(false)]
		public bool IsOnDropDown
		{
			get
			{
				return this.parent != null && this.parent is ToolStripDropDown;
			}
		}

		/// <summary>Gets or sets the space between the item and adjacent items.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Padding" /> representing the space between the item and adjacent items.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000536 RID: 1334
		// (get) Token: 0x060013C5 RID: 5061 RVA: 0x00063D51 File Offset: 0x00061F51
		public Padding Margin
		{
			get
			{
				return this.margin;
			}
		}

		/// <summary>Gets or sets how child menus are merged with parent menus. </summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.MergeAction" /> values. The default is <see cref="F:System.Windows.Forms.MergeAction.MatchOnly" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="T:System.Windows.Forms.MergeAction" /> values.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x060013C6 RID: 5062 RVA: 0x00063D59 File Offset: 0x00061F59
		[DefaultValue(MergeAction.Append)]
		public MergeAction MergeAction
		{
			get
			{
				return this.merge_action;
			}
		}

		/// <summary>Gets or sets the position of a merged item within the current <see cref="T:System.Windows.Forms.ToolStrip" />.</summary>
		/// <returns>An integer representing the index of the merged item, if a match is found, or -1 if a match is not found.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x060013C7 RID: 5063 RVA: 0x00063D61 File Offset: 0x00061F61
		// (set) Token: 0x060013C8 RID: 5064 RVA: 0x00063D69 File Offset: 0x00061F69
		[DefaultValue(-1)]
		public int MergeIndex
		{
			get
			{
				return this.merge_index;
			}
			set
			{
				this.merge_index = value;
			}
		}

		/// <summary>Gets or sets whether the item is attached to the <see cref="T:System.Windows.Forms.ToolStrip" /> or <see cref="T:System.Windows.Forms.ToolStripOverflowButton" /> or can float between the two.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ToolStripItemOverflow" /> values. The default is <see cref="F:System.Windows.Forms.ToolStripItemOverflow.AsNeeded" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="T:System.Windows.Forms.ToolStripItemOverflow" /> values. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x060013C9 RID: 5065 RVA: 0x00063D72 File Offset: 0x00061F72
		// (set) Token: 0x060013CA RID: 5066 RVA: 0x00063D7C File Offset: 0x00061F7C
		[DefaultValue(ToolStripItemOverflow.AsNeeded)]
		public ToolStripItemOverflow Overflow
		{
			get
			{
				return this.overflow;
			}
			set
			{
				if (this.overflow != value)
				{
					if (!Enum.IsDefined(typeof(ToolStripItemOverflow), value))
					{
						throw new InvalidEnumArgumentException(string.Format("Enum argument value '{0}' is not valid for ToolStripItemOverflow", value));
					}
					this.overflow = value;
					if (this.owner != null)
					{
						this.owner.PerformLayout();
					}
				}
			}
		}

		/// <summary>Gets or sets the owner of this item.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ToolStrip" /> that owns or is to own the <see cref="T:System.Windows.Forms.ToolStripItem" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700053A RID: 1338
		// (get) Token: 0x060013CB RID: 5067 RVA: 0x00063DD9 File Offset: 0x00061FD9
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public ToolStrip Owner
		{
			get
			{
				return this.owner;
			}
		}

		/// <summary>Gets the parent <see cref="T:System.Windows.Forms.ToolStripItem" /> of this <see cref="T:System.Windows.Forms.ToolStripItem" />.</summary>
		/// <returns>The parent <see cref="T:System.Windows.Forms.ToolStripItem" /> of this <see cref="T:System.Windows.Forms.ToolStripItem" />.</returns>
		// Token: 0x1700053B RID: 1339
		// (get) Token: 0x060013CC RID: 5068 RVA: 0x00063DE1 File Offset: 0x00061FE1
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public ToolStripItem OwnerItem
		{
			get
			{
				return this.owner_item;
			}
		}

		/// <summary>Gets the current layout of the item.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ToolStripItemPlacement" /> values.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700053C RID: 1340
		// (get) Token: 0x060013CD RID: 5069 RVA: 0x00063DE9 File Offset: 0x00061FE9
		[Browsable(false)]
		public ToolStripItemPlacement Placement
		{
			get
			{
				return this.placement;
			}
		}

		/// <summary>Gets a value indicating whether the state of the item is pressed. </summary>
		/// <returns>true if the state of the item is pressed; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700053D RID: 1341
		// (get) Token: 0x060013CE RID: 5070 RVA: 0x00063DF1 File Offset: 0x00061FF1
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual bool Pressed
		{
			get
			{
				return this.is_pressed;
			}
		}

		/// <summary>Gets or sets a value indicating whether items are to be placed from right to left and text is to be written from right to left.</summary>
		/// <returns>true if items are to be placed from right to left and text is to be written from right to left; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700053E RID: 1342
		// (get) Token: 0x060013CF RID: 5071 RVA: 0x00063DF9 File Offset: 0x00061FF9
		[MonoTODO("RTL not implemented")]
		[Localizable(true)]
		public virtual RightToLeft RightToLeft
		{
			get
			{
				return this.right_to_left;
			}
		}

		/// <summary>Mirrors automatically the <see cref="T:System.Windows.Forms.ToolStripItem" /> image when the <see cref="P:System.Windows.Forms.ToolStripItem.RightToLeft" /> property is set to <see cref="F:System.Windows.Forms.RightToLeft.Yes" />.</summary>
		/// <returns>true to automatically mirror the image; otherwise, false. The default is false.</returns>
		// Token: 0x1700053F RID: 1343
		// (get) Token: 0x060013D0 RID: 5072 RVA: 0x00063E01 File Offset: 0x00062001
		[Localizable(true)]
		[DefaultValue(false)]
		public bool RightToLeftAutoMirrorImage
		{
			get
			{
				return this.right_to_left_auto_mirror_image;
			}
		}

		/// <summary>Gets a value indicating whether the item is selected.</summary>
		/// <returns>true if the <see cref="T:System.Windows.Forms.ToolStripItem" /> is selected; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000540 RID: 1344
		// (get) Token: 0x060013D1 RID: 5073 RVA: 0x00063E09 File Offset: 0x00062009
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual bool Selected
		{
			get
			{
				return this.is_selected;
			}
		}

		/// <summary>Gets or sets the size of the item.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" />, representing the width and height of a rectangle.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000541 RID: 1345
		// (get) Token: 0x060013D2 RID: 5074 RVA: 0x00063E11 File Offset: 0x00062011
		// (set) Token: 0x060013D3 RID: 5075 RVA: 0x00063E3F File Offset: 0x0006203F
		[Localizable(true)]
		public virtual Size Size
		{
			get
			{
				if (!this.AutoSize && this.explicit_size != Size.Empty)
				{
					return this.explicit_size;
				}
				return this.bounds.Size;
			}
			set
			{
				this.bounds.Size = value;
				this.explicit_size = value;
				if (this.Visible)
				{
					this.CalculateAutoSize();
					this.OnBoundsChanged();
				}
			}
		}

		/// <summary>Gets or sets the text that is to be displayed on the item.</summary>
		/// <returns>A string representing the item's text. The default value is the empty string ("").</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000542 RID: 1346
		// (get) Token: 0x060013D4 RID: 5076 RVA: 0x00063E68 File Offset: 0x00062068
		// (set) Token: 0x060013D5 RID: 5077 RVA: 0x00063E70 File Offset: 0x00062070
		[Localizable(true)]
		[DefaultValue("")]
		public virtual string Text
		{
			get
			{
				return this.text;
			}
			set
			{
				if (this.text != value)
				{
					this.text = value;
					this.Invalidate();
					this.CalculateAutoSize();
					this.Invalidate();
					this.OnTextChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Gets or sets the alignment of the text on a <see cref="T:System.Windows.Forms.ToolStripLabel" />.</summary>
		/// <returns>One of the <see cref="T:System.Drawing.ContentAlignment" /> values. The default is <see cref="F:System.Drawing.ContentAlignment.MiddleRight" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="T:System.Drawing.ContentAlignment" /> values. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000543 RID: 1347
		// (get) Token: 0x060013D6 RID: 5078 RVA: 0x00063EA4 File Offset: 0x000620A4
		[Localizable(true)]
		[DefaultValue(ContentAlignment.MiddleCenter)]
		public virtual ContentAlignment TextAlign
		{
			get
			{
				return this.text_align;
			}
		}

		/// <summary>Gets the orientation of text used on a <see cref="T:System.Windows.Forms.ToolStripItem" />.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ToolStripTextDirection" /> values.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000544 RID: 1348
		// (get) Token: 0x060013D7 RID: 5079 RVA: 0x00063EAC File Offset: 0x000620AC
		public virtual ToolStripTextDirection TextDirection
		{
			get
			{
				if (this.text_direction != ToolStripTextDirection.Inherit)
				{
					return this.text_direction;
				}
				if (this.Parent != null)
				{
					return this.Parent.TextDirection;
				}
				return ToolStripTextDirection.Horizontal;
			}
		}

		/// <summary>Gets or sets the text that appears as a <see cref="T:System.Windows.Forms.ToolTip" /> for a control.</summary>
		/// <returns>A string representing the ToolTip text.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000545 RID: 1349
		// (set) Token: 0x060013D8 RID: 5080 RVA: 0x00063ED2 File Offset: 0x000620D2
		[Localizable(true)]
		[Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public string ToolTipText
		{
			set
			{
				this.tool_tip_text = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the item is displayed.</summary>
		/// <returns>true if the item is displayed; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000546 RID: 1350
		// (get) Token: 0x060013D9 RID: 5081 RVA: 0x00063EDB File Offset: 0x000620DB
		// (set) Token: 0x060013DA RID: 5082 RVA: 0x00063EFC File Offset: 0x000620FC
		[Localizable(true)]
		public bool Visible
		{
			get
			{
				return this.parent != null && this.visible && this.parent.Visible;
			}
			set
			{
				if (this.visible != value)
				{
					this.available = value;
					this.SetVisibleCore(value);
					if (this.Owner != null)
					{
						this.Owner.PerformLayout();
					}
				}
			}
		}

		/// <summary>Gets or sets the width in pixels of a <see cref="T:System.Windows.Forms.ToolStripItem" />.</summary>
		/// <returns>An <see cref="T:System.Int32" /> representing the width in pixels.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000547 RID: 1351
		// (get) Token: 0x060013DB RID: 5083 RVA: 0x00063F28 File Offset: 0x00062128
		// (set) Token: 0x060013DC RID: 5084 RVA: 0x00063F44 File Offset: 0x00062144
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int Width
		{
			get
			{
				return this.Size.Width;
			}
			set
			{
				this.Size = new Size(value, this.Size.Height);
				this.explicit_size.Width = value;
				if (this.Visible)
				{
					this.CalculateAutoSize();
					this.OnBoundsChanged();
					this.Invalidate();
				}
			}
		}

		/// <summary>Gets a value indicating whether to display the <see cref="T:System.Windows.Forms.ToolTip" /> that is defined as the default.</summary>
		/// <returns>false in all cases.</returns>
		// Token: 0x17000548 RID: 1352
		// (get) Token: 0x060013DD RID: 5085 RVA: 0x00002D70 File Offset: 0x00000F70
		protected virtual bool DefaultAutoToolTip
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating what is displayed on the <see cref="T:System.Windows.Forms.ToolStripItem" />.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ToolStripItemDisplayStyle" /> values. The default is <see cref="F:System.Windows.Forms.ToolStripItemDisplayStyle.ImageAndText" />.</returns>
		// Token: 0x17000549 RID: 1353
		// (get) Token: 0x060013DE RID: 5086 RVA: 0x00050984 File Offset: 0x0004EB84
		protected virtual ToolStripItemDisplayStyle DefaultDisplayStyle
		{
			get
			{
				return ToolStripItemDisplayStyle.ImageAndText;
			}
		}

		/// <summary>Gets the default margin of an item.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Padding" /> representing the margin.</returns>
		// Token: 0x1700054A RID: 1354
		// (get) Token: 0x060013DF RID: 5087 RVA: 0x00063F91 File Offset: 0x00062191
		protected internal virtual Padding DefaultMargin
		{
			get
			{
				return new Padding(0, 1, 0, 2);
			}
		}

		/// <summary>Gets the internal spacing characteristics of the item.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.Padding" /> values.</returns>
		// Token: 0x1700054B RID: 1355
		// (get) Token: 0x060013E0 RID: 5088 RVA: 0x00063F9C File Offset: 0x0006219C
		protected virtual Padding DefaultPadding
		{
			get
			{
				return default(Padding);
			}
		}

		/// <summary>Gets the default size of the item.</summary>
		/// <returns>The default <see cref="T:System.Drawing.Size" /> of the <see cref="T:System.Windows.Forms.ToolStripItem" />.</returns>
		// Token: 0x1700054C RID: 1356
		// (get) Token: 0x060013E1 RID: 5089 RVA: 0x00063FB2 File Offset: 0x000621B2
		protected virtual Size DefaultSize
		{
			get
			{
				return new Size(23, 23);
			}
		}

		/// <summary>Gets or sets the parent container of the <see cref="T:System.Windows.Forms.ToolStripItem" />.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ToolStrip" /> that is the parent container of the <see cref="T:System.Windows.Forms.ToolStripItem" />.</returns>
		// Token: 0x1700054D RID: 1357
		// (get) Token: 0x060013E2 RID: 5090 RVA: 0x00063FBD File Offset: 0x000621BD
		// (set) Token: 0x060013E3 RID: 5091 RVA: 0x00063FC8 File Offset: 0x000621C8
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		protected internal ToolStrip Parent
		{
			get
			{
				return this.parent;
			}
			set
			{
				if (this.parent != value)
				{
					ToolStrip toolStrip = this.parent;
					this.parent = value;
					this.OnParentChanged(toolStrip, this.parent);
				}
			}
		}

		/// <summary>Retrieves the <see cref="T:System.Windows.Forms.ToolStrip" /> that is the container of the current <see cref="T:System.Windows.Forms.ToolStripItem" />.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ToolStrip" /> that is the container of the current <see cref="T:System.Windows.Forms.ToolStripItem" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060013E4 RID: 5092 RVA: 0x00063FBD File Offset: 0x000621BD
		public ToolStrip GetCurrentParent()
		{
			return this.parent;
		}

		/// <summary>Retrieves the size of a rectangular area into which a control can be fit.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> ordered pair, representing the width and height of a rectangle.</returns>
		/// <param name="constrainingSize">The custom-sized area for a control. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060013E5 RID: 5093 RVA: 0x00063FF9 File Offset: 0x000621F9
		public virtual Size GetPreferredSize(Size constrainingSize)
		{
			return this.CalculatePreferredSize(constrainingSize);
		}

		/// <summary>Invalidates the entire surface of the <see cref="T:System.Windows.Forms.ToolStripItem" /> and causes it to be redrawn.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060013E6 RID: 5094 RVA: 0x00064002 File Offset: 0x00062202
		public void Invalidate()
		{
			if (this.parent != null)
			{
				this.parent.Invalidate(this.bounds);
			}
		}

		/// <summary>Activates the <see cref="T:System.Windows.Forms.ToolStripItem" /> when it is clicked with the mouse.</summary>
		// Token: 0x060013E7 RID: 5095 RVA: 0x0006401D File Offset: 0x0006221D
		public void PerformClick()
		{
			this.OnClick(EventArgs.Empty);
		}

		/// <summary>Selects the item.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060013E8 RID: 5096 RVA: 0x0006402C File Offset: 0x0006222C
		public void Select()
		{
			if (!this.is_selected && this.CanSelect)
			{
				this.is_selected = true;
				if (this.Parent != null)
				{
					if (this.Visible && this.Parent.Focused && this is ToolStripControlHost)
					{
						(this as ToolStripControlHost).Focus();
					}
					this.Invalidate();
					this.Parent.NotifySelectedChanged(this);
				}
				this.OnUIASelectionChanged();
			}
		}

		/// <returns>A <see cref="T:System.String" /> containing the name of the <see cref="T:System.ComponentModel.Component" />, if any, or null if the <see cref="T:System.ComponentModel.Component" /> is unnamed.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060013E9 RID: 5097 RVA: 0x00063E68 File Offset: 0x00062068
		public override string ToString()
		{
			return this.text;
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.ToolStripItem" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x060013EA RID: 5098 RVA: 0x00064098 File Offset: 0x00062298
		protected override void Dispose(bool disposing)
		{
			if (!this.is_disposed && disposing)
			{
				this.is_disposed = true;
			}
			if (this.image != null)
			{
				this.StopAnimation();
				this.image = null;
			}
			if (this.owner != null && disposing)
			{
				this.owner.Items.Remove(this);
			}
			base.Dispose(disposing);
		}

		/// <summary>Raises the AvailableChanged event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060013EB RID: 5099 RVA: 0x000640F4 File Offset: 0x000622F4
		protected virtual void OnAvailableChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ToolStripItem.AvailableChangedEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.ToolStripItem.Bounds" /> property changes.</summary>
		// Token: 0x060013EC RID: 5100 RVA: 0x00064122 File Offset: 0x00062322
		protected virtual void OnBoundsChanged()
		{
			this.OnLayout(new LayoutEventArgs(null, string.Empty));
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.Click" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060013ED RID: 5101 RVA: 0x00064138 File Offset: 0x00062338
		protected virtual void OnClick(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ToolStripItem.ClickEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.DisplayStyleChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060013EE RID: 5102 RVA: 0x00064168 File Offset: 0x00062368
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnDisplayStyleChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ToolStripItem.DisplayStyleChangedEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.DoubleClick" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060013EF RID: 5103 RVA: 0x00064198 File Offset: 0x00062398
		protected virtual void OnDoubleClick(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ToolStripItem.DoubleClickEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.EnabledChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060013F0 RID: 5104 RVA: 0x000641C8 File Offset: 0x000623C8
		protected virtual void OnEnabledChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ToolStripItem.EnabledChangedEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.FontChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060013F1 RID: 5105 RVA: 0x0000493C File Offset: 0x00002B3C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnFontChanged(EventArgs e)
		{
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Layout" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.LayoutEventArgs" /> that contains the event data.</param>
		// Token: 0x060013F2 RID: 5106 RVA: 0x0000493C File Offset: 0x00002B3C
		protected virtual void OnLayout(LayoutEventArgs e)
		{
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.LocationChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060013F3 RID: 5107 RVA: 0x000641F8 File Offset: 0x000623F8
		protected virtual void OnLocationChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ToolStripItem.LocationChangedEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.MouseDown" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data. </param>
		// Token: 0x060013F4 RID: 5108 RVA: 0x00064228 File Offset: 0x00062428
		protected virtual void OnMouseDown(MouseEventArgs e)
		{
			if (this.Enabled)
			{
				this.is_pressed = true;
				this.Invalidate();
				MouseEventHandler mouseEventHandler = (MouseEventHandler)base.Events[ToolStripItem.MouseDownEvent];
				if (mouseEventHandler != null)
				{
					mouseEventHandler(this, e);
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.MouseEnter" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060013F5 RID: 5109 RVA: 0x0006426C File Offset: 0x0006246C
		protected virtual void OnMouseEnter(EventArgs e)
		{
			this.Select();
			EventHandler eventHandler = (EventHandler)base.Events[ToolStripItem.MouseEnterEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.MouseHover" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060013F6 RID: 5110 RVA: 0x000642A0 File Offset: 0x000624A0
		protected virtual void OnMouseHover(EventArgs e)
		{
			if (this.Enabled)
			{
				EventHandler eventHandler = (EventHandler)base.Events[ToolStripItem.MouseHoverEvent];
				if (eventHandler != null)
				{
					eventHandler(this, e);
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.MouseLeave" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060013F7 RID: 5111 RVA: 0x000642D8 File Offset: 0x000624D8
		protected virtual void OnMouseLeave(EventArgs e)
		{
			if (this.CanSelect)
			{
				this.is_selected = false;
				this.is_pressed = false;
				this.Invalidate();
				this.OnUIASelectionChanged();
			}
			EventHandler eventHandler = (EventHandler)base.Events[ToolStripItem.MouseLeaveEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.MouseMove" /> event.</summary>
		/// <param name="mea">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data. </param>
		// Token: 0x060013F8 RID: 5112 RVA: 0x00064328 File Offset: 0x00062528
		protected virtual void OnMouseMove(MouseEventArgs mea)
		{
			if (this.Enabled)
			{
				MouseEventHandler mouseEventHandler = (MouseEventHandler)base.Events[ToolStripItem.MouseMoveEvent];
				if (mouseEventHandler != null)
				{
					mouseEventHandler(this, mea);
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.MouseUp" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data. </param>
		// Token: 0x060013F9 RID: 5113 RVA: 0x00064360 File Offset: 0x00062560
		protected virtual void OnMouseUp(MouseEventArgs e)
		{
			if (this.Enabled)
			{
				this.is_pressed = false;
				this.Invalidate();
				if (this.IsOnDropDown && (!(this is ToolStripDropDownItem) || !(this as ToolStripDropDownItem).HasDropDownItems || !(this as ToolStripDropDownItem).DropDown.Visible))
				{
					if ((this.Parent as ToolStripDropDown).OwnerItem != null)
					{
						((this.Parent as ToolStripDropDown).OwnerItem as ToolStripDropDownItem).HideDropDown();
					}
					else
					{
						(this.Parent as ToolStripDropDown).Hide();
					}
				}
				MouseEventHandler mouseEventHandler = (MouseEventHandler)base.Events[ToolStripItem.MouseUpEvent];
				if (mouseEventHandler != null)
				{
					mouseEventHandler(this, e);
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.OwnerChanged" /> event. </summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060013FA RID: 5114 RVA: 0x00064414 File Offset: 0x00062614
		protected virtual void OnOwnerChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ToolStripItem.OwnerChangedEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.FontChanged" /> event when the <see cref="P:System.Windows.Forms.ToolStripItem.Font" /> property has changed on the parent of the <see cref="T:System.Windows.Forms.ToolStripItem" />.</summary>
		/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060013FB RID: 5115 RVA: 0x00064442 File Offset: 0x00062642
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected internal virtual void OnOwnerFontChanged(EventArgs e)
		{
			this.CalculateAutoSize();
			this.OnFontChanged(EventArgs.Empty);
		}

		// Token: 0x060013FC RID: 5116 RVA: 0x00064455 File Offset: 0x00062655
		private void OnPaintInternal(PaintEventArgs e)
		{
			if (this.parent != null)
			{
				this.parent.Renderer.DrawItemBackground(new ToolStripItemRenderEventArgs(e.Graphics, this));
			}
			this.OnPaint(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.Paint" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data. </param>
		// Token: 0x060013FD RID: 5117 RVA: 0x00064484 File Offset: 0x00062684
		protected virtual void OnPaint(PaintEventArgs e)
		{
			PaintEventHandler paintEventHandler = (PaintEventHandler)base.Events[ToolStripItem.PaintEvent];
			if (paintEventHandler != null)
			{
				paintEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.ParentChanged" /> event.</summary>
		/// <param name="oldParent">The original parent of the item. </param>
		/// <param name="newParent">The new parent of the item. </param>
		// Token: 0x060013FE RID: 5118 RVA: 0x000644B4 File Offset: 0x000626B4
		protected virtual void OnParentChanged(ToolStrip oldParent, ToolStrip newParent)
		{
			this.text_size = TextRenderer.MeasureText((this.Text == null) ? string.Empty : this.text, this.Font, Size.Empty, TextFormatFlags.HidePrefix);
			if (oldParent != null)
			{
				oldParent.PerformLayout();
			}
			if (newParent != null)
			{
				newParent.PerformLayout();
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.EnabledChanged" /> event when the <see cref="P:System.Windows.Forms.ToolStripItem.Enabled" /> property value of the item's container changes.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060013FF RID: 5119 RVA: 0x00064503 File Offset: 0x00062703
		protected internal virtual void OnParentEnabledChanged(EventArgs e)
		{
			this.OnEnabledChanged(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.RightToLeftChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06001400 RID: 5120 RVA: 0x0006450C File Offset: 0x0006270C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected internal virtual void OnParentRightToLeftChanged(EventArgs e)
		{
			this.OnRightToLeftChanged(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.RightToLeftChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06001401 RID: 5121 RVA: 0x00064518 File Offset: 0x00062718
		protected virtual void OnRightToLeftChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ToolStripItem.RightToLeftChangedEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.TextChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06001402 RID: 5122 RVA: 0x00064548 File Offset: 0x00062748
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnTextChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ToolStripItem.TextChangedEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.VisibleChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06001403 RID: 5123 RVA: 0x00064578 File Offset: 0x00062778
		protected virtual void OnVisibleChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ToolStripItem.VisibleChangedEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Processes a command key.</summary>
		/// <returns>false in all cases.</returns>
		/// <param name="m">A <see cref="T:System.Windows.Forms.Message" />, passed by reference, that represents the window message to process. </param>
		/// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values that represents the key to process. </param>
		// Token: 0x06001404 RID: 5124 RVA: 0x00002D70 File Offset: 0x00000F70
		protected internal virtual bool ProcessCmdKey(ref Message m, Keys keyData)
		{
			return false;
		}

		/// <summary>Processes a dialog key.</summary>
		/// <returns>true if the key was processed by the item; otherwise, false.</returns>
		/// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values that represents the key to process. </param>
		// Token: 0x06001405 RID: 5125 RVA: 0x000645A6 File Offset: 0x000627A6
		protected internal virtual bool ProcessDialogKey(Keys keyData)
		{
			if (this.Selected && keyData == Keys.Return)
			{
				this.FireEvent(EventArgs.Empty, ToolStripItemEventType.Click);
				return true;
			}
			return false;
		}

		/// <summary>Processes a mnemonic character.</summary>
		/// <returns>true in all cases.</returns>
		/// <param name="charCode">The character to process. </param>
		// Token: 0x06001406 RID: 5126 RVA: 0x000645C4 File Offset: 0x000627C4
		protected internal virtual bool ProcessMnemonic(char charCode)
		{
			ToolStripManager.SetActiveToolStrip(this.Parent, true);
			this.PerformClick();
			return true;
		}

		/// <summary>Sets the size and location of the item.</summary>
		/// <param name="bounds">A <see cref="T:System.Drawing.Rectangle" /> that represents the size and location of the <see cref="T:System.Windows.Forms.ToolStripItem" /></param>
		// Token: 0x06001407 RID: 5127 RVA: 0x000645D9 File Offset: 0x000627D9
		protected internal virtual void SetBounds(Rectangle bounds)
		{
			if (this.bounds != bounds)
			{
				this.bounds = bounds;
				this.OnBoundsChanged();
			}
		}

		/// <summary>Sets the <see cref="T:System.Windows.Forms.ToolStripItem" /> to the specified visible state. </summary>
		/// <param name="visible">true to make the <see cref="T:System.Windows.Forms.ToolStripItem" /> visible; otherwise, false.</param>
		// Token: 0x06001408 RID: 5128 RVA: 0x000645F6 File Offset: 0x000627F6
		protected virtual void SetVisibleCore(bool visible)
		{
			this.visible = visible;
			this.OnVisibleChanged(EventArgs.Empty);
			if (this.visible)
			{
				this.BeginAnimation();
			}
			else
			{
				this.StopAnimation();
			}
			this.Invalidate();
		}

		/// <summary>Occurs when the <see cref="T:System.Windows.Forms.ToolStripItem" /> is clicked.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000047 RID: 71
		// (add) Token: 0x06001409 RID: 5129 RVA: 0x00064626 File Offset: 0x00062826
		// (remove) Token: 0x0600140A RID: 5130 RVA: 0x00064639 File Offset: 0x00062839
		public event EventHandler Click
		{
			add
			{
				base.Events.AddHandler(ToolStripItem.ClickEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripItem.ClickEvent, value);
			}
		}

		// Token: 0x0600140B RID: 5131 RVA: 0x0006464C File Offset: 0x0006284C
		internal Rectangle AlignInRectangle(Rectangle outer, Size inner, ContentAlignment align)
		{
			int num = 0;
			int num2 = 0;
			if (align == ContentAlignment.BottomLeft || align == ContentAlignment.MiddleLeft || align == ContentAlignment.TopLeft)
			{
				num = outer.X;
			}
			else if (align == ContentAlignment.BottomCenter || align == ContentAlignment.MiddleCenter || align == ContentAlignment.TopCenter)
			{
				num = Math.Max(outer.X + (outer.Width - inner.Width) / 2, outer.Left);
			}
			else if (align == ContentAlignment.BottomRight || align == ContentAlignment.MiddleRight || align == ContentAlignment.TopRight)
			{
				num = outer.Right - inner.Width;
			}
			if (align == ContentAlignment.TopCenter || align == ContentAlignment.TopLeft || align == ContentAlignment.TopRight)
			{
				num2 = outer.Y;
			}
			else if (align == ContentAlignment.MiddleCenter || align == ContentAlignment.MiddleLeft || align == ContentAlignment.MiddleRight)
			{
				num2 = outer.Y + (outer.Height - inner.Height) / 2;
			}
			else if (align == ContentAlignment.BottomCenter || align == ContentAlignment.BottomRight || align == ContentAlignment.BottomLeft)
			{
				num2 = outer.Bottom - inner.Height;
			}
			return new Rectangle(num, num2, Math.Min(inner.Width, outer.Width), Math.Min(inner.Height, outer.Height));
		}

		// Token: 0x0600140C RID: 5132 RVA: 0x00064768 File Offset: 0x00062968
		internal void CalculateAutoSize()
		{
			this.text_size = TextRenderer.MeasureText((this.Text == null) ? string.Empty : this.text, this.Font, Size.Empty, TextFormatFlags.HidePrefix);
			ToolStripTextDirection textDirection = this.TextDirection;
			if (textDirection == ToolStripTextDirection.Vertical270 || textDirection == ToolStripTextDirection.Vertical90)
			{
				this.text_size = new Size(this.text_size.Height, this.text_size.Width);
			}
			if (!this.auto_size || this is ToolStripControlHost)
			{
				return;
			}
			Size size = this.CalculatePreferredSize(Size.Empty);
			if (size != this.Size)
			{
				this.bounds.Width = size.Width;
				if (this.parent != null)
				{
					this.parent.PerformLayout();
				}
			}
		}

		// Token: 0x0600140D RID: 5133 RVA: 0x00064828 File Offset: 0x00062A28
		internal virtual Size CalculatePreferredSize(Size constrainingSize)
		{
			if (!this.auto_size)
			{
				return this.explicit_size;
			}
			Size size = this.DefaultSize;
			switch (this.display_style)
			{
			case ToolStripItemDisplayStyle.Text:
			{
				int num = this.text_size.Width + this.padding.Horizontal;
				int num2 = this.text_size.Height + this.padding.Vertical;
				size = new Size(num, num2);
				break;
			}
			case ToolStripItemDisplayStyle.Image:
				if (this.GetImageSize() == Size.Empty)
				{
					size = this.DefaultSize;
				}
				else
				{
					ToolStripItemImageScaling toolStripItemImageScaling = this.image_scaling;
					if (toolStripItemImageScaling != ToolStripItemImageScaling.None)
					{
						if (toolStripItemImageScaling == ToolStripItemImageScaling.SizeToFit)
						{
							if (this.parent == null)
							{
								size = this.GetImageSize();
							}
							else
							{
								size = this.parent.ImageScalingSize;
							}
						}
					}
					else
					{
						size = this.GetImageSize();
					}
				}
				break;
			case ToolStripItemDisplayStyle.ImageAndText:
			{
				int num3 = this.text_size.Width + this.padding.Horizontal;
				int num4 = this.text_size.Height + this.padding.Vertical;
				if (this.GetImageSize() != Size.Empty)
				{
					Size size2 = this.GetImageSize();
					if (this.image_scaling == ToolStripItemImageScaling.SizeToFit && this.parent != null)
					{
						size2 = this.parent.ImageScalingSize;
					}
					switch (this.text_image_relation)
					{
					case TextImageRelation.Overlay:
						num3 = Math.Max(num3, size2.Width);
						num4 = Math.Max(num4, size2.Height);
						break;
					case TextImageRelation.ImageAboveText:
					case TextImageRelation.TextAboveImage:
						num3 = Math.Max(num3, size2.Width);
						num4 += size2.Height;
						break;
					case TextImageRelation.ImageBeforeText:
					case TextImageRelation.TextBeforeImage:
						num4 = Math.Max(num4, size2.Height);
						num3 += size2.Width;
						break;
					}
				}
				size = new Size(num3, num4);
				break;
			}
			}
			if (!(this is ToolStripLabel))
			{
				size.Height += 4;
				size.Width += 4;
			}
			return size;
		}

		// Token: 0x0600140E RID: 5134 RVA: 0x00064A3E File Offset: 0x00062C3E
		internal void CalculateTextAndImageRectangles(out Rectangle text_rect, out Rectangle image_rect)
		{
			this.CalculateTextAndImageRectangles(this.ContentRectangle, out text_rect, out image_rect);
		}

		// Token: 0x0600140F RID: 5135 RVA: 0x00064A50 File Offset: 0x00062C50
		internal void CalculateTextAndImageRectangles(Rectangle contentRectangle, out Rectangle text_rect, out Rectangle image_rect)
		{
			text_rect = Rectangle.Empty;
			image_rect = Rectangle.Empty;
			switch (this.display_style)
			{
			case ToolStripItemDisplayStyle.None:
				break;
			case ToolStripItemDisplayStyle.Text:
				if (this.text != string.Empty)
				{
					text_rect = this.AlignInRectangle(contentRectangle, this.text_size, this.text_align);
					return;
				}
				break;
			case ToolStripItemDisplayStyle.Image:
				if (this.Image != null && this.UseImageMargin)
				{
					image_rect = this.AlignInRectangle(contentRectangle, this.GetImageSize(), this.image_align);
					return;
				}
				break;
			case ToolStripItemDisplayStyle.ImageAndText:
				if (this.text != string.Empty && (this.Image == null || !this.UseImageMargin))
				{
					text_rect = this.AlignInRectangle(contentRectangle, this.text_size, this.text_align);
					return;
				}
				if (!(this.text == string.Empty) || (this.Image != null && this.UseImageMargin))
				{
					if (this.text == string.Empty && this.Image != null)
					{
						image_rect = this.AlignInRectangle(contentRectangle, this.GetImageSize(), this.image_align);
						return;
					}
					switch (this.text_image_relation)
					{
					case TextImageRelation.Overlay:
						text_rect = this.AlignInRectangle(contentRectangle, this.text_size, this.text_align);
						image_rect = this.AlignInRectangle(contentRectangle, this.GetImageSize(), this.image_align);
						return;
					case TextImageRelation.ImageAboveText:
					{
						Rectangle rectangle = new Rectangle(contentRectangle.Left, contentRectangle.Bottom - this.text_size.Height, contentRectangle.Width, this.text_size.Height);
						Rectangle rectangle2 = new Rectangle(contentRectangle.Left, contentRectangle.Top, contentRectangle.Width, contentRectangle.Height - rectangle.Height);
						text_rect = this.AlignInRectangle(rectangle, this.text_size, this.text_align);
						image_rect = this.AlignInRectangle(rectangle2, this.GetImageSize(), this.image_align);
						return;
					}
					case TextImageRelation.TextAboveImage:
					{
						Rectangle rectangle = new Rectangle(contentRectangle.Left, contentRectangle.Top, contentRectangle.Width, this.text_size.Height);
						Rectangle rectangle2 = new Rectangle(contentRectangle.Left, rectangle.Bottom, contentRectangle.Width, contentRectangle.Height - rectangle.Height);
						text_rect = this.AlignInRectangle(rectangle, this.text_size, this.text_align);
						image_rect = this.AlignInRectangle(rectangle2, this.GetImageSize(), this.image_align);
						return;
					}
					case (TextImageRelation)3:
					case (TextImageRelation)5:
					case (TextImageRelation)6:
					case (TextImageRelation)7:
						break;
					case TextImageRelation.ImageBeforeText:
						this.LayoutTextBeforeOrAfterImage(contentRectangle, false, this.text_size, this.GetImageSize(), this.text_align, this.image_align, out text_rect, out image_rect);
						return;
					case TextImageRelation.TextBeforeImage:
						this.LayoutTextBeforeOrAfterImage(contentRectangle, true, this.text_size, this.GetImageSize(), this.text_align, this.image_align, out text_rect, out image_rect);
						break;
					default:
						return;
					}
				}
				break;
			default:
				return;
			}
		}

		// Token: 0x1700054E RID: 1358
		// (get) Token: 0x06001410 RID: 5136 RVA: 0x00064D48 File Offset: 0x00062F48
		private static Font DefaultFont
		{
			get
			{
				return new Font("Tahoma", 8.25f);
			}
		}

		// Token: 0x1700054F RID: 1359
		// (get) Token: 0x06001411 RID: 5137 RVA: 0x00002D70 File Offset: 0x00000F70
		internal virtual ToolStripTextDirection DefaultTextDirection
		{
			get
			{
				return ToolStripTextDirection.Inherit;
			}
		}

		// Token: 0x06001412 RID: 5138 RVA: 0x00064D59 File Offset: 0x00062F59
		internal virtual void Dismiss(ToolStripDropDownCloseReason reason)
		{
			if (this.is_selected)
			{
				this.is_selected = false;
				this.Invalidate();
				this.OnUIASelectionChanged();
			}
		}

		// Token: 0x06001413 RID: 5139 RVA: 0x00064D76 File Offset: 0x00062F76
		internal virtual ToolStrip GetTopLevelToolStrip()
		{
			if (this.Parent != null)
			{
				return this.Parent.GetTopLevelToolStrip();
			}
			return null;
		}

		// Token: 0x06001414 RID: 5140 RVA: 0x00064D90 File Offset: 0x00062F90
		private void LayoutTextBeforeOrAfterImage(Rectangle totalArea, bool textFirst, Size textSize, Size imageSize, ContentAlignment textAlign, ContentAlignment imageAlign, out Rectangle textRect, out Rectangle imageRect)
		{
			int num = 0;
			int num2 = textSize.Width + num + imageSize.Width;
			int num3 = totalArea.Width - num2;
			int num4 = 0;
			HorizontalAlignment horizontalAlignment = this.GetHorizontalAlignment(textAlign);
			HorizontalAlignment horizontalAlignment2 = this.GetHorizontalAlignment(imageAlign);
			if (horizontalAlignment2 == HorizontalAlignment.Left)
			{
				num4 = 0;
			}
			else if (horizontalAlignment2 == HorizontalAlignment.Right && horizontalAlignment == HorizontalAlignment.Right)
			{
				num4 = num3;
			}
			else if (horizontalAlignment2 == HorizontalAlignment.Center && (horizontalAlignment == HorizontalAlignment.Left || horizontalAlignment == HorizontalAlignment.Center))
			{
				num4 += num3 / 3;
			}
			else
			{
				num4 += 2 * (num3 / 3);
			}
			Rectangle rectangle;
			Rectangle rectangle2;
			if (textFirst)
			{
				rectangle = new Rectangle(totalArea.Left + num4, this.AlignInRectangle(totalArea, textSize, textAlign).Top, textSize.Width, textSize.Height);
				rectangle2 = new Rectangle(rectangle.Right + num, this.AlignInRectangle(totalArea, imageSize, imageAlign).Top, imageSize.Width, imageSize.Height);
			}
			else
			{
				rectangle2 = new Rectangle(totalArea.Left + num4, this.AlignInRectangle(totalArea, imageSize, imageAlign).Top, imageSize.Width, imageSize.Height);
				rectangle = new Rectangle(rectangle2.Right + num, this.AlignInRectangle(totalArea, textSize, textAlign).Top, textSize.Width, textSize.Height);
			}
			textRect = rectangle;
			imageRect = rectangle2;
		}

		// Token: 0x06001415 RID: 5141 RVA: 0x00064EE4 File Offset: 0x000630E4
		private HorizontalAlignment GetHorizontalAlignment(ContentAlignment align)
		{
			if (align <= ContentAlignment.MiddleCenter)
			{
				switch (align)
				{
				case ContentAlignment.TopLeft:
					break;
				case ContentAlignment.TopCenter:
					return HorizontalAlignment.Center;
				case (ContentAlignment)3:
					return HorizontalAlignment.Left;
				case ContentAlignment.TopRight:
					return HorizontalAlignment.Right;
				default:
					if (align != ContentAlignment.MiddleLeft)
					{
						if (align != ContentAlignment.MiddleCenter)
						{
							return HorizontalAlignment.Left;
						}
						return HorizontalAlignment.Center;
					}
					break;
				}
			}
			else if (align <= ContentAlignment.BottomLeft)
			{
				if (align == ContentAlignment.MiddleRight)
				{
					return HorizontalAlignment.Right;
				}
				if (align != ContentAlignment.BottomLeft)
				{
					return HorizontalAlignment.Left;
				}
			}
			else
			{
				if (align == ContentAlignment.BottomCenter)
				{
					return HorizontalAlignment.Center;
				}
				if (align != ContentAlignment.BottomRight)
				{
					return HorizontalAlignment.Left;
				}
				return HorizontalAlignment.Right;
			}
			return HorizontalAlignment.Left;
		}

		// Token: 0x06001416 RID: 5142 RVA: 0x00064F4C File Offset: 0x0006314C
		internal Size GetImageSize()
		{
			if (this.image_scaling == ToolStripItemImageScaling.None)
			{
				if (this.image != null)
				{
					return this.image.Size;
				}
				if ((this.image_index >= 0 || !string.IsNullOrEmpty(this.image_key)) && this.owner != null && this.owner.ImageList != null)
				{
					return this.owner.ImageList.ImageSize;
				}
			}
			else
			{
				if (this.Parent == null)
				{
					return Size.Empty;
				}
				if (this.image != null)
				{
					return this.Parent.ImageScalingSize;
				}
				if ((this.image_index >= 0 || !string.IsNullOrEmpty(this.image_key)) && this.owner != null && this.owner.ImageList != null)
				{
					return this.Parent.ImageScalingSize;
				}
			}
			return Size.Empty;
		}

		// Token: 0x06001417 RID: 5143 RVA: 0x0006500F File Offset: 0x0006320F
		internal string GetToolTip()
		{
			if (this.auto_tool_tip && string.IsNullOrEmpty(this.tool_tip_text))
			{
				return this.Text;
			}
			return this.tool_tip_text;
		}

		// Token: 0x06001418 RID: 5144 RVA: 0x00065034 File Offset: 0x00063234
		internal void FireEvent(EventArgs e, ToolStripItemEventType met)
		{
			if (!this.Enabled && met != ToolStripItemEventType.Paint)
			{
				return;
			}
			switch (met)
			{
			case ToolStripItemEventType.MouseDown:
				this.OnMouseDown((MouseEventArgs)e);
				return;
			case ToolStripItemEventType.MouseEnter:
				this.OnMouseEnter(e);
				return;
			case ToolStripItemEventType.MouseHover:
				this.OnMouseHover(e);
				return;
			case ToolStripItemEventType.MouseLeave:
				this.OnMouseLeave(e);
				return;
			case ToolStripItemEventType.MouseMove:
				this.OnMouseMove((MouseEventArgs)e);
				return;
			case ToolStripItemEventType.MouseUp:
				if (((MouseEventArgs)e).Button == MouseButtons.Left)
				{
					this.HandleClick(((MouseEventArgs)e).Clicks, e);
				}
				this.OnMouseUp((MouseEventArgs)e);
				return;
			case ToolStripItemEventType.Paint:
				this.OnPaintInternal((PaintEventArgs)e);
				return;
			case ToolStripItemEventType.Click:
				this.HandleClick(1, e);
				return;
			default:
				return;
			}
		}

		// Token: 0x06001419 RID: 5145 RVA: 0x000650EF File Offset: 0x000632EF
		internal virtual void HandleClick(int mouse_clicks, EventArgs e)
		{
			if (this.Parent == null)
			{
				return;
			}
			this.Parent.HandleItemClick(this);
			if (mouse_clicks == 2 && this.double_click_enabled)
			{
				this.OnDoubleClick(e);
				return;
			}
			this.OnClick(e);
		}

		// Token: 0x0600141A RID: 5146 RVA: 0x00065121 File Offset: 0x00063321
		internal virtual void SetPlacement(ToolStripItemPlacement placement)
		{
			this.placement = placement;
		}

		// Token: 0x0600141B RID: 5147 RVA: 0x0006512A File Offset: 0x0006332A
		private void BeginAnimation()
		{
			if (this.image != null && ImageAnimator.CanAnimate(this.image))
			{
				this.frame_handler = new EventHandler(this.OnAnimateImage);
				ImageAnimator.Animate(this.image, this.frame_handler);
			}
		}

		// Token: 0x0600141C RID: 5148 RVA: 0x00065164 File Offset: 0x00063364
		private void OnAnimateImage(object sender, EventArgs e)
		{
			if (this.Parent == null || !this.Parent.IsHandleCreated)
			{
				return;
			}
			this.Parent.BeginInvoke(new EventHandler(this.UpdateAnimatedImage), new object[] { this, e });
		}

		// Token: 0x0600141D RID: 5149 RVA: 0x000651A2 File Offset: 0x000633A2
		private void StopAnimation()
		{
			if (this.frame_handler == null)
			{
				return;
			}
			ImageAnimator.StopAnimate(this.image, this.frame_handler);
			this.frame_handler = null;
		}

		// Token: 0x0600141E RID: 5150 RVA: 0x000651C5 File Offset: 0x000633C5
		private void UpdateAnimatedImage(object sender, EventArgs e)
		{
			if (this.Parent == null || !this.Parent.IsHandleCreated)
			{
				return;
			}
			ImageAnimator.UpdateFrames(this.image);
			this.Invalidate();
		}

		// Token: 0x17000550 RID: 1360
		// (get) Token: 0x0600141F RID: 5151 RVA: 0x000651F0 File Offset: 0x000633F0
		internal bool ShowMargin
		{
			get
			{
				if (!this.IsOnDropDown)
				{
					return true;
				}
				if (!(this.Owner is ToolStripDropDownMenu))
				{
					return false;
				}
				ToolStripDropDownMenu toolStripDropDownMenu = (ToolStripDropDownMenu)this.Owner;
				return toolStripDropDownMenu.ShowCheckMargin || toolStripDropDownMenu.ShowImageMargin;
			}
		}

		// Token: 0x17000551 RID: 1361
		// (get) Token: 0x06001420 RID: 5152 RVA: 0x00065234 File Offset: 0x00063434
		internal bool UseImageMargin
		{
			get
			{
				if (!this.IsOnDropDown)
				{
					return true;
				}
				if (!(this.Owner is ToolStripDropDownMenu))
				{
					return false;
				}
				ToolStripDropDownMenu toolStripDropDownMenu = (ToolStripDropDownMenu)this.Owner;
				return toolStripDropDownMenu.ShowImageMargin || toolStripDropDownMenu.ShowCheckMargin;
			}
		}

		// Token: 0x17000552 RID: 1362
		// (get) Token: 0x06001421 RID: 5153 RVA: 0x00065276 File Offset: 0x00063476
		internal virtual bool InternalVisible
		{
			get
			{
				return this.visible;
			}
		}

		// Token: 0x17000553 RID: 1363
		// (set) Token: 0x06001422 RID: 5154 RVA: 0x0006527E File Offset: 0x0006347E
		internal ToolStrip InternalOwner
		{
			set
			{
				if (this.owner != value)
				{
					this.owner = value;
					if (this.owner != null)
					{
						this.CalculateAutoSize();
					}
					this.OnOwnerChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x17000554 RID: 1364
		// (get) Token: 0x06001423 RID: 5155 RVA: 0x000652A9 File Offset: 0x000634A9
		// (set) Token: 0x06001424 RID: 5156 RVA: 0x000652B6 File Offset: 0x000634B6
		internal Point Location
		{
			get
			{
				return this.bounds.Location;
			}
			set
			{
				if (this.bounds.Location != value)
				{
					this.bounds.Location = value;
					this.OnLocationChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x17000555 RID: 1365
		// (get) Token: 0x06001425 RID: 5157 RVA: 0x000652E2 File Offset: 0x000634E2
		// (set) Token: 0x06001426 RID: 5158 RVA: 0x000652EF File Offset: 0x000634EF
		internal int Top
		{
			get
			{
				return this.bounds.Y;
			}
			set
			{
				if (this.bounds.Y != value)
				{
					this.bounds.Y = value;
					this.OnLocationChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x17000556 RID: 1366
		// (get) Token: 0x06001427 RID: 5159 RVA: 0x00065316 File Offset: 0x00063516
		// (set) Token: 0x06001428 RID: 5160 RVA: 0x00065323 File Offset: 0x00063523
		internal int Left
		{
			get
			{
				return this.bounds.X;
			}
			set
			{
				if (this.bounds.X != value)
				{
					this.bounds.X = value;
					this.OnLocationChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x17000557 RID: 1367
		// (get) Token: 0x06001429 RID: 5161 RVA: 0x0006534A File Offset: 0x0006354A
		internal int Right
		{
			get
			{
				return this.bounds.Right;
			}
		}

		// Token: 0x17000558 RID: 1368
		// (get) Token: 0x0600142A RID: 5162 RVA: 0x00065357 File Offset: 0x00063557
		internal int Bottom
		{
			get
			{
				return this.bounds.Bottom;
			}
		}

		// Token: 0x0600142B RID: 5163 RVA: 0x00065364 File Offset: 0x00063564
		internal void OnUIASelectionChanged()
		{
			EventHandler eventHandler = (EventHandler)base.Events[ToolStripItem.UIASelectionChangedEvent];
			if (eventHandler != null)
			{
				eventHandler(this, EventArgs.Empty);
			}
		}

		// Token: 0x0600142C RID: 5164 RVA: 0x00065398 File Offset: 0x00063598
		// Note: this type is marked as 'beforefieldinit'.
		static ToolStripItem()
		{
			ToolStripItem.ClickEvent = new object();
			ToolStripItem.DisplayStyleChangedEvent = new object();
			ToolStripItem.DoubleClickEvent = new object();
			ToolStripItem.DragDropEvent = new object();
			ToolStripItem.DragEnterEvent = new object();
			ToolStripItem.DragLeaveEvent = new object();
			ToolStripItem.DragOverEvent = new object();
			ToolStripItem.EnabledChangedEvent = new object();
			ToolStripItem.ForeColorChangedEvent = new object();
			ToolStripItem.GiveFeedbackEvent = new object();
			ToolStripItem.LocationChangedEvent = new object();
			ToolStripItem.MouseDownEvent = new object();
			ToolStripItem.MouseEnterEvent = new object();
			ToolStripItem.MouseHoverEvent = new object();
			ToolStripItem.MouseLeaveEvent = new object();
			ToolStripItem.MouseMoveEvent = new object();
			ToolStripItem.MouseUpEvent = new object();
			ToolStripItem.OwnerChangedEvent = new object();
			ToolStripItem.PaintEvent = new object();
			ToolStripItem.QueryAccessibilityHelpEvent = new object();
			ToolStripItem.QueryContinueDragEvent = new object();
			ToolStripItem.RightToLeftChangedEvent = new object();
			ToolStripItem.TextChangedEvent = new object();
			ToolStripItem.VisibleChangedEvent = new object();
			ToolStripItem.UIASelectionChangedEvent = new object();
		}

		// Token: 0x04000BF6 RID: 3062
		private ToolStripItemAlignment alignment;

		// Token: 0x04000BF7 RID: 3063
		private AnchorStyles anchor;

		// Token: 0x04000BF8 RID: 3064
		private bool available;

		// Token: 0x04000BF9 RID: 3065
		private bool auto_size;

		// Token: 0x04000BFA RID: 3066
		private bool auto_tool_tip;

		// Token: 0x04000BFB RID: 3067
		private Color back_color;

		// Token: 0x04000BFC RID: 3068
		private Image background_image;

		// Token: 0x04000BFD RID: 3069
		private ImageLayout background_image_layout;

		// Token: 0x04000BFE RID: 3070
		private Rectangle bounds;

		// Token: 0x04000BFF RID: 3071
		private bool can_select;

		// Token: 0x04000C00 RID: 3072
		private ToolStripItemDisplayStyle display_style;

		// Token: 0x04000C01 RID: 3073
		private DockStyle dock;

		// Token: 0x04000C02 RID: 3074
		private bool double_click_enabled;

		// Token: 0x04000C03 RID: 3075
		private bool enabled;

		// Token: 0x04000C04 RID: 3076
		private Size explicit_size;

		// Token: 0x04000C05 RID: 3077
		private Font font;

		// Token: 0x04000C06 RID: 3078
		private Color fore_color;

		// Token: 0x04000C07 RID: 3079
		private Image image;

		// Token: 0x04000C08 RID: 3080
		private ContentAlignment image_align;

		// Token: 0x04000C09 RID: 3081
		private int image_index;

		// Token: 0x04000C0A RID: 3082
		private string image_key;

		// Token: 0x04000C0B RID: 3083
		private ToolStripItemImageScaling image_scaling;

		// Token: 0x04000C0C RID: 3084
		private Color image_transparent_color;

		// Token: 0x04000C0D RID: 3085
		private bool is_disposed;

		// Token: 0x04000C0E RID: 3086
		internal bool is_pressed;

		// Token: 0x04000C0F RID: 3087
		private bool is_selected;

		// Token: 0x04000C10 RID: 3088
		private Padding margin;

		// Token: 0x04000C11 RID: 3089
		private MergeAction merge_action;

		// Token: 0x04000C12 RID: 3090
		private int merge_index;

		// Token: 0x04000C13 RID: 3091
		private string name;

		// Token: 0x04000C14 RID: 3092
		private ToolStripItemOverflow overflow;

		// Token: 0x04000C15 RID: 3093
		private ToolStrip owner;

		// Token: 0x04000C16 RID: 3094
		internal ToolStripItem owner_item;

		// Token: 0x04000C17 RID: 3095
		private Padding padding;

		// Token: 0x04000C18 RID: 3096
		private ToolStripItemPlacement placement;

		// Token: 0x04000C19 RID: 3097
		private RightToLeft right_to_left;

		// Token: 0x04000C1A RID: 3098
		private bool right_to_left_auto_mirror_image;

		// Token: 0x04000C1B RID: 3099
		private string text;

		// Token: 0x04000C1C RID: 3100
		private ContentAlignment text_align;

		// Token: 0x04000C1D RID: 3101
		private ToolStripTextDirection text_direction;

		// Token: 0x04000C1E RID: 3102
		private TextImageRelation text_image_relation;

		// Token: 0x04000C1F RID: 3103
		private string tool_tip_text;

		// Token: 0x04000C20 RID: 3104
		private bool visible;

		// Token: 0x04000C21 RID: 3105
		private EventHandler frame_handler;

		// Token: 0x04000C22 RID: 3106
		private ToolStrip parent;

		// Token: 0x04000C23 RID: 3107
		private Size text_size;

		// Token: 0x04000C24 RID: 3108
		private static object AvailableChangedEvent = new object();

		// Token: 0x04000C25 RID: 3109
		private static object BackColorChangedEvent = new object();

		// Token: 0x04000C27 RID: 3111
		private static object DisplayStyleChangedEvent;

		// Token: 0x04000C28 RID: 3112
		private static object DoubleClickEvent;

		// Token: 0x04000C29 RID: 3113
		private static object DragDropEvent;

		// Token: 0x04000C2A RID: 3114
		private static object DragEnterEvent;

		// Token: 0x04000C2B RID: 3115
		private static object DragLeaveEvent;

		// Token: 0x04000C2C RID: 3116
		private static object DragOverEvent;

		// Token: 0x04000C2D RID: 3117
		private static object EnabledChangedEvent;

		// Token: 0x04000C2E RID: 3118
		private static object ForeColorChangedEvent;

		// Token: 0x04000C2F RID: 3119
		private static object GiveFeedbackEvent;

		// Token: 0x04000C30 RID: 3120
		private static object LocationChangedEvent;

		// Token: 0x04000C31 RID: 3121
		private static object MouseDownEvent;

		// Token: 0x04000C32 RID: 3122
		private static object MouseEnterEvent;

		// Token: 0x04000C33 RID: 3123
		private static object MouseHoverEvent;

		// Token: 0x04000C34 RID: 3124
		private static object MouseLeaveEvent;

		// Token: 0x04000C35 RID: 3125
		private static object MouseMoveEvent;

		// Token: 0x04000C36 RID: 3126
		private static object MouseUpEvent;

		// Token: 0x04000C37 RID: 3127
		private static object OwnerChangedEvent;

		// Token: 0x04000C38 RID: 3128
		private static object PaintEvent;

		// Token: 0x04000C39 RID: 3129
		private static object QueryAccessibilityHelpEvent;

		// Token: 0x04000C3A RID: 3130
		private static object QueryContinueDragEvent;

		// Token: 0x04000C3B RID: 3131
		private static object RightToLeftChangedEvent;

		// Token: 0x04000C3C RID: 3132
		private static object TextChangedEvent;

		// Token: 0x04000C3D RID: 3133
		private static object VisibleChangedEvent;

		// Token: 0x04000C3E RID: 3134
		private static object UIASelectionChangedEvent;
	}
}
