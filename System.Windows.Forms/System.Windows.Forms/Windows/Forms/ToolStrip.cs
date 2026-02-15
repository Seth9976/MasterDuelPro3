using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms.Layout;

namespace System.Windows.Forms
{
	/// <summary>Provides a container for Windows toolbar objects. </summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020001B7 RID: 439
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[DefaultEvent("ItemClicked")]
	[DefaultProperty("Items")]
	[Designer("System.Windows.Forms.Design.ToolStripDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[DesignerSerializer("System.Windows.Forms.Design.ToolStripCodeDomSerializer, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.Serialization.CodeDomSerializer, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public class ToolStrip : ScrollableControl, IComponent, IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStrip" /> class.</summary>
		// Token: 0x0600129D RID: 4765 RVA: 0x0005F818 File Offset: 0x0005DA18
		public ToolStrip()
			: this(null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStrip" /> class with the specified array of <see cref="T:System.Windows.Forms.ToolStripItem" />s.</summary>
		/// <param name="items">An array of <see cref="T:System.Windows.Forms.ToolStripItem" /> objects.</param>
		// Token: 0x0600129E RID: 4766 RVA: 0x0005F824 File Offset: 0x0005DA24
		public ToolStrip(params ToolStripItem[] items)
		{
			base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			base.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
			base.SetStyle(ControlStyles.Selectable, false);
			base.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
			base.SuspendLayout();
			this.items = new ToolStripItemCollection(this, items, true);
			this.allow_merge = true;
			base.AutoSize = true;
			base.SetAutoSizeMode(AutoSizeMode.GrowAndShrink);
			this.back_color = Control.DefaultBackColor;
			this.can_overflow = true;
			base.CausesValidation = false;
			this.default_drop_down_direction = ToolStripDropDownDirection.BelowRight;
			this.displayed_items = new ToolStripItemCollection(this, null, true);
			this.Dock = this.DefaultDock;
			base.Font = new Font("Tahoma", 8.25f);
			this.fore_color = Control.DefaultForeColor;
			this.grip_margin = this.DefaultGripMargin;
			this.grip_style = ToolStripGripStyle.Visible;
			this.image_scaling_size = new Size(16, 16);
			this.layout_style = ToolStripLayoutStyle.HorizontalStackWithOverflow;
			this.orientation = Orientation.Horizontal;
			if (!(this is ToolStripDropDown))
			{
				this.overflow_button = new ToolStripOverflowButton(this);
			}
			this.renderer = null;
			this.render_mode = ToolStripRenderMode.ManagerRenderMode;
			this.show_item_tool_tips = this.DefaultShowItemToolTips;
			base.TabStop = false;
			this.text_direction = ToolStripTextDirection.Horizontal;
			base.ResumeLayout();
			ToolStripManager.AddToolStrip(this);
		}

		/// <summary>Gets or sets a value indicating whether drag-and-drop and item reordering are handled through events that you implement.</summary>
		/// <returns>true to control drag-and-drop and item reordering through events that you implement; otherwise, false.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <see cref="P:System.Windows.Forms.ToolStrip.AllowDrop" /> and <see cref="P:System.Windows.Forms.ToolStrip.AllowItemReorder" /> are both set to true. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x0600129F RID: 4767 RVA: 0x0002083A File Offset: 0x0001EA3A
		[MonoTODO("Stub, does nothing")]
		public override bool AllowDrop
		{
			get
			{
				return base.AllowDrop;
			}
		}

		/// <summary>Gets or sets a value indicating whether multiple <see cref="T:System.Windows.Forms.MenuStrip" />, <see cref="T:System.Windows.Forms.ToolStripDropDownMenu" />, <see cref="T:System.Windows.Forms.ToolStripMenuItem" />, and other types can be combined. </summary>
		/// <returns>true if combining of types is allowed; otherwise, false. The default is false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x060012A0 RID: 4768 RVA: 0x0005F962 File Offset: 0x0005DB62
		[DefaultValue(true)]
		public bool AllowMerge
		{
			get
			{
				return this.allow_merge;
			}
		}

		/// <summary>Gets or sets the edges of the container to which a <see cref="T:System.Windows.Forms.ToolStrip" /> is bound and determines how a <see cref="T:System.Windows.Forms.ToolStrip" /> is resized with its parent.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.AnchorStyles" /> values.</returns>
		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x060012A1 RID: 4769 RVA: 0x0005F96A File Offset: 0x0005DB6A
		// (set) Token: 0x060012A2 RID: 4770 RVA: 0x0005F972 File Offset: 0x0005DB72
		public override AnchorStyles Anchor
		{
			get
			{
				return base.Anchor;
			}
			set
			{
				base.Anchor = value;
			}
		}

		/// <summary>This property is not relevant for this class.</summary>
		/// <returns>true to automatically scroll; otherwise, false.</returns>
		/// <exception cref="T:System.NotSupportedException">Automatic scrolling is not supported by <see cref="T:System.Windows.Forms.ToolStrip" /> controls.</exception>
		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x060012A3 RID: 4771 RVA: 0x0001D9B5 File Offset: 0x0001BBB5
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override bool AutoScroll
		{
			get
			{
				return base.AutoScroll;
			}
		}

		/// <summary>Gets or sets a value indicating whether the control is automatically resized to display its entire contents.</summary>
		/// <returns>true if the control adjusts its width to closely fit its contents; otherwise, false. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x060012A4 RID: 4772 RVA: 0x000042BD File Offset: 0x000024BD
		// (set) Token: 0x060012A5 RID: 4773 RVA: 0x000042C5 File Offset: 0x000024C5
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		[DefaultValue(true)]
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

		/// <summary>Gets or sets the background color for the <see cref="T:System.Windows.Forms.ToolStrip" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the background color of the <see cref="T:System.Windows.Forms.ToolStrip" />. The default is the value of the <see cref="P:System.Windows.Forms.Control.DefaultBackColor" /> property.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x060012A6 RID: 4774 RVA: 0x0005F97B File Offset: 0x0005DB7B
		public new Color BackColor
		{
			get
			{
				return this.back_color;
			}
		}

		/// <summary>Gets or sets the binding context for the <see cref="T:System.Windows.Forms.ToolStrip" />.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.BindingContext" /> for the <see cref="T:System.Windows.Forms.ToolStrip" />.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x060012A7 RID: 4775 RVA: 0x0005F983 File Offset: 0x0005DB83
		// (set) Token: 0x060012A8 RID: 4776 RVA: 0x000091A4 File Offset: 0x000073A4
		public override BindingContext BindingContext
		{
			get
			{
				return base.BindingContext;
			}
			set
			{
				base.BindingContext = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether items in the <see cref="T:System.Windows.Forms.ToolStrip" /> can be sent to an overflow menu.</summary>
		/// <returns>true to send <see cref="T:System.Windows.Forms.ToolStrip" /> items to an overflow menu; otherwise, false. The default value is true.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x060012A9 RID: 4777 RVA: 0x0005F98B File Offset: 0x0005DB8B
		[DefaultValue(true)]
		public bool CanOverflow
		{
			get
			{
				return this.can_overflow;
			}
		}

		/// <summary>Gets or sets the cursor that is displayed when the mouse pointer is over the <see cref="T:System.Windows.Forms.ToolStrip" />.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Cursor" /> that represents the cursor to display when the mouse pointer is over the <see cref="T:System.Windows.Forms.ToolStrip" />.</returns>
		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x060012AA RID: 4778 RVA: 0x0005F993 File Offset: 0x0005DB93
		// (set) Token: 0x060012AB RID: 4779 RVA: 0x0005F99B File Offset: 0x0005DB9B
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override Cursor Cursor
		{
			get
			{
				return base.Cursor;
			}
			set
			{
				base.Cursor = value;
			}
		}

		/// <summary>Gets or sets a value representing the default direction in which a <see cref="T:System.Windows.Forms.ToolStripDropDown" /> control is displayed relative to the <see cref="T:System.Windows.Forms.ToolStrip" />.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ToolStripDropDownDirection" /> values.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value is not one of the <see cref="T:System.Windows.Forms.ToolStripDropDownDirection" /> values.</exception>
		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x060012AC RID: 4780 RVA: 0x0005F9A4 File Offset: 0x0005DBA4
		// (set) Token: 0x060012AD RID: 4781 RVA: 0x0005F9AC File Offset: 0x0005DBAC
		[Browsable(false)]
		public virtual ToolStripDropDownDirection DefaultDropDownDirection
		{
			get
			{
				return this.default_drop_down_direction;
			}
			set
			{
				if (!Enum.IsDefined(typeof(ToolStripDropDownDirection), value))
				{
					throw new InvalidEnumArgumentException(string.Format("Enum argument value '{0}' is not valid for ToolStripDropDownDirection", value));
				}
				this.default_drop_down_direction = value;
			}
		}

		/// <summary>Retrieves the current display rectangle.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> representing the <see cref="T:System.Windows.Forms.ToolStrip" /> area for item layout.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170004D8 RID: 1240
		// (get) Token: 0x060012AE RID: 4782 RVA: 0x0005F9E4 File Offset: 0x0005DBE4
		public override Rectangle DisplayRectangle
		{
			get
			{
				if (this.orientation == Orientation.Horizontal)
				{
					if (this.grip_style == ToolStripGripStyle.Hidden || this.layout_style == ToolStripLayoutStyle.Flow || this.layout_style == ToolStripLayoutStyle.Table)
					{
						return new Rectangle(base.Padding.Left, base.Padding.Top, base.Width - base.Padding.Horizontal, base.Height - base.Padding.Vertical);
					}
					return new Rectangle(this.GripRectangle.Right + this.GripMargin.Right, base.Padding.Top, base.Width - base.Padding.Horizontal - this.GripRectangle.Right - this.GripMargin.Right, base.Height - base.Padding.Vertical);
				}
				else
				{
					if (this.grip_style == ToolStripGripStyle.Hidden || this.layout_style == ToolStripLayoutStyle.Flow || this.layout_style == ToolStripLayoutStyle.Table)
					{
						return new Rectangle(base.Padding.Left, base.Padding.Top, base.Width - base.Padding.Horizontal, base.Height - base.Padding.Vertical);
					}
					return new Rectangle(base.Padding.Left, this.GripRectangle.Bottom + this.GripMargin.Bottom + base.Padding.Top, base.Width - base.Padding.Horizontal, base.Height - base.Padding.Vertical - this.GripRectangle.Bottom - this.GripMargin.Bottom);
				}
			}
		}

		/// <summary>Gets or sets which <see cref="T:System.Windows.Forms.ToolStrip" /> borders are docked to its parent control and determines how a <see cref="T:System.Windows.Forms.ToolStrip" /> is resized with its parent.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DockStyle" /> values. The default value is <see cref="F:System.Windows.Forms.DockStyle.Top" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170004D9 RID: 1241
		// (get) Token: 0x060012AF RID: 4783 RVA: 0x0005D779 File Offset: 0x0005B979
		// (set) Token: 0x060012B0 RID: 4784 RVA: 0x0005FBC8 File Offset: 0x0005DDC8
		[DefaultValue(DockStyle.Top)]
		public override DockStyle Dock
		{
			get
			{
				return base.Dock;
			}
			set
			{
				if (base.Dock != value)
				{
					base.Dock = value;
					if (value <= DockStyle.Bottom)
					{
						this.LayoutStyle = ToolStripLayoutStyle.HorizontalStackWithOverflow;
						return;
					}
					if (value - DockStyle.Left > 1)
					{
						return;
					}
					this.LayoutStyle = ToolStripLayoutStyle.VerticalStackWithOverflow;
				}
			}
		}

		/// <summary>Gets or sets the font used to display text in the control.</summary>
		/// <returns>The current default font.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x060012B1 RID: 4785 RVA: 0x0002C5BA File Offset: 0x0002A7BA
		// (set) Token: 0x060012B2 RID: 4786 RVA: 0x0005FBF4 File Offset: 0x0005DDF4
		public override Font Font
		{
			get
			{
				return base.Font;
			}
			set
			{
				if (base.Font != value)
				{
					base.Font = value;
					foreach (object obj in this.Items)
					{
						((ToolStripItem)obj).OnOwnerFontChanged(EventArgs.Empty);
					}
				}
			}
		}

		/// <summary>Gets or sets the foreground color of the <see cref="T:System.Windows.Forms.ToolStrip" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing the foreground color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x060012B3 RID: 4787 RVA: 0x0005FC60 File Offset: 0x0005DE60
		[Browsable(false)]
		public new Color ForeColor
		{
			get
			{
				return this.fore_color;
			}
		}

		/// <summary>Gets the orientation of the <see cref="T:System.Windows.Forms.ToolStrip" /> move handle.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ToolStripGripDisplayStyle" /> values. Possible values are <see cref="F:System.Windows.Forms.ToolStripGripDisplayStyle.Horizontal" /> and <see cref="F:System.Windows.Forms.ToolStripGripDisplayStyle.Vertical" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x060012B4 RID: 4788 RVA: 0x0005FC68 File Offset: 0x0005DE68
		[Browsable(false)]
		public ToolStripGripDisplayStyle GripDisplayStyle
		{
			get
			{
				if (this.orientation != Orientation.Vertical)
				{
					return ToolStripGripDisplayStyle.Vertical;
				}
				return ToolStripGripDisplayStyle.Horizontal;
			}
		}

		/// <summary>Gets or sets the space around the <see cref="T:System.Windows.Forms.ToolStrip" /> move handle.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Padding" />, which represents the spacing.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x060012B5 RID: 4789 RVA: 0x0005FC76 File Offset: 0x0005DE76
		public Padding GripMargin
		{
			get
			{
				return this.grip_margin;
			}
		}

		/// <summary>Gets the boundaries of the <see cref="T:System.Windows.Forms.ToolStrip" /> move handle.</summary>
		/// <returns>An object of type <see cref="T:System.Drawing.Rectangle" />, representing the move handle boundaries. If the boundaries are not visible, the <see cref="P:System.Windows.Forms.ToolStrip.GripRectangle" /> property returns <see cref="F:System.Drawing.Rectangle.Empty" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x060012B6 RID: 4790 RVA: 0x0005FC80 File Offset: 0x0005DE80
		[Browsable(false)]
		public Rectangle GripRectangle
		{
			get
			{
				if (this.grip_style == ToolStripGripStyle.Hidden)
				{
					return Rectangle.Empty;
				}
				if (this.orientation == Orientation.Horizontal)
				{
					return new Rectangle(this.grip_margin.Left + base.Padding.Left, base.Padding.Top, 3, base.Height);
				}
				return new Rectangle(base.Padding.Left, this.grip_margin.Top + base.Padding.Top, base.Width, 3);
			}
		}

		/// <summary>Gets or sets whether the <see cref="T:System.Windows.Forms.ToolStrip" /> move handle is visible or hidden.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ToolStripGripStyle" /> values. The default value is <see cref="F:System.Windows.Forms.ToolStripGripStyle.Visible" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value is not one of the <see cref="T:System.Windows.Forms.ToolStripGripStyle" /> values. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170004DF RID: 1247
		// (set) Token: 0x060012B7 RID: 4791 RVA: 0x0005FD0C File Offset: 0x0005DF0C
		[DefaultValue(ToolStripGripStyle.Visible)]
		public ToolStripGripStyle GripStyle
		{
			set
			{
				if (this.grip_style != value)
				{
					if (!Enum.IsDefined(typeof(ToolStripGripStyle), value))
					{
						throw new InvalidEnumArgumentException(string.Format("Enum argument value '{0}' is not valid for ToolStripGripStyle", value));
					}
					this.grip_style = value;
					base.PerformLayout(this, "GripStyle");
				}
			}
		}

		/// <summary>Gets or sets the image list that contains the image displayed on a <see cref="T:System.Windows.Forms.ToolStrip" /> item.</summary>
		/// <returns>An object of type <see cref="T:System.Windows.Forms.ImageList" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x060012B8 RID: 4792 RVA: 0x0005FD62 File Offset: 0x0005DF62
		[Browsable(false)]
		[DefaultValue(null)]
		public ImageList ImageList
		{
			get
			{
				return this.image_list;
			}
		}

		/// <summary>Gets or sets the size, in pixels, of an image used on a <see cref="T:System.Windows.Forms.ToolStrip" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> value representing the size of the image, in pixels. The default is 16 x 16 pixels.</returns>
		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x060012B9 RID: 4793 RVA: 0x0005FD6A File Offset: 0x0005DF6A
		[DefaultValue("{Width=16, Height=16}")]
		public Size ImageScalingSize
		{
			get
			{
				return this.image_scaling_size;
			}
		}

		/// <summary>Gets all the items that belong to a <see cref="T:System.Windows.Forms.ToolStrip" />.</summary>
		/// <returns>An object of type <see cref="T:System.Windows.Forms.ToolStripItemCollection" />, representing all the elements contained by a <see cref="T:System.Windows.Forms.ToolStrip" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170004E2 RID: 1250
		// (get) Token: 0x060012BA RID: 4794 RVA: 0x0005FD72 File Offset: 0x0005DF72
		[MergableProperty(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public virtual ToolStripItemCollection Items
		{
			get
			{
				return this.items;
			}
		}

		/// <summary>Passes a reference to the cached <see cref="P:System.Windows.Forms.Control.LayoutEngine" /> returned by the layout engine interface.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Layout.LayoutEngine" /> that represents the cached layout engine returned by the layout engine interface.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x060012BB RID: 4795 RVA: 0x0005FD7A File Offset: 0x0005DF7A
		public override LayoutEngine LayoutEngine
		{
			get
			{
				if (this.layout_engine == null)
				{
					this.layout_engine = new ToolStripSplitStackLayout();
				}
				return this.layout_engine;
			}
		}

		/// <summary>Gets or sets layout scheme characteristics.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.LayoutSettings" /> representing the layout scheme characteristics.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x060012BC RID: 4796 RVA: 0x0005FD95 File Offset: 0x0005DF95
		[Browsable(false)]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public LayoutSettings LayoutSettings
		{
			get
			{
				return this.layout_settings;
			}
		}

		/// <summary>Gets or sets a value indicating how the <see cref="T:System.Windows.Forms.ToolStrip" /> lays out the items collection.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ToolStripLayoutStyle" /> values. The possible values are <see cref="F:System.Windows.Forms.ToolStripLayoutStyle.Table" />, <see cref="F:System.Windows.Forms.ToolStripLayoutStyle.Flow" />, <see cref="F:System.Windows.Forms.ToolStripLayoutStyle.StackWithOverflow" />, <see cref="F:System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow" />, and <see cref="F:System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value of <see cref="P:System.Windows.Forms.ToolStrip.LayoutStyle" /> is not one of the <see cref="T:System.Windows.Forms.ToolStripLayoutStyle" /> values.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x060012BD RID: 4797 RVA: 0x0005FD9D File Offset: 0x0005DF9D
		// (set) Token: 0x060012BE RID: 4798 RVA: 0x0005FDA8 File Offset: 0x0005DFA8
		[AmbientValue(ToolStripLayoutStyle.StackWithOverflow)]
		public ToolStripLayoutStyle LayoutStyle
		{
			get
			{
				return this.layout_style;
			}
			set
			{
				if (this.layout_style != value)
				{
					if (!Enum.IsDefined(typeof(ToolStripLayoutStyle), value))
					{
						throw new InvalidEnumArgumentException(string.Format("Enum argument value '{0}' is not valid for ToolStripLayoutStyle", value));
					}
					this.layout_style = value;
					if (this.layout_style == ToolStripLayoutStyle.Flow)
					{
						this.layout_engine = new FlowLayout();
					}
					else
					{
						this.layout_engine = new ToolStripSplitStackLayout();
					}
					if (this.layout_style == ToolStripLayoutStyle.StackWithOverflow)
					{
						if (this.Dock == DockStyle.Left || this.Dock == DockStyle.Right)
						{
							this.layout_style = ToolStripLayoutStyle.VerticalStackWithOverflow;
						}
						else
						{
							this.layout_style = ToolStripLayoutStyle.HorizontalStackWithOverflow;
						}
					}
					if (this.layout_style == ToolStripLayoutStyle.HorizontalStackWithOverflow)
					{
						this.orientation = Orientation.Horizontal;
					}
					else if (this.layout_style == ToolStripLayoutStyle.VerticalStackWithOverflow)
					{
						this.orientation = Orientation.Vertical;
					}
					this.layout_settings = this.CreateLayoutSettings(value);
					base.PerformLayout(this, "LayoutStyle");
					this.OnLayoutStyleChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Gets the orientation of the <see cref="T:System.Windows.Forms.ToolStripPanel" />.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.Orientation" /> values. The default is <see cref="F:System.Windows.Forms.Orientation.Horizontal" />.</returns>
		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x060012BF RID: 4799 RVA: 0x0005FE86 File Offset: 0x0005E086
		[Browsable(false)]
		public Orientation Orientation
		{
			get
			{
				return this.orientation;
			}
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.ToolStripItem" /> that is the overflow button for a <see cref="T:System.Windows.Forms.ToolStrip" /> with overflow enabled.</summary>
		/// <returns>An object of type <see cref="T:System.Windows.Forms.ToolStripOverflowButton" /> with its <see cref="T:System.Windows.Forms.ToolStripItemAlignment" /> set to <see cref="F:System.Windows.Forms.ToolStripItemAlignment.Right" /> and its <see cref="T:System.Windows.Forms.ToolStripItemOverflow" /> value set to <see cref="F:System.Windows.Forms.ToolStripItemOverflow.Never" />.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170004E7 RID: 1255
		// (get) Token: 0x060012C0 RID: 4800 RVA: 0x0005FE8E File Offset: 0x0005E08E
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public ToolStripOverflowButton OverflowButton
		{
			get
			{
				return this.overflow_button;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Windows.Forms.ToolStripRenderer" /> used to customize the look and feel of a <see cref="T:System.Windows.Forms.ToolStrip" />.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ToolStripRenderer" /> used to customize the look and feel of a <see cref="T:System.Windows.Forms.ToolStrip" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x060012C1 RID: 4801 RVA: 0x0005FE96 File Offset: 0x0005E096
		// (set) Token: 0x060012C2 RID: 4802 RVA: 0x0005FEAD File Offset: 0x0005E0AD
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public ToolStripRenderer Renderer
		{
			get
			{
				if (this.render_mode == ToolStripRenderMode.ManagerRenderMode)
				{
					return ToolStripManager.Renderer;
				}
				return this.renderer;
			}
			set
			{
				if (this.renderer != value)
				{
					this.renderer = value;
					this.render_mode = ToolStripRenderMode.Custom;
					base.PerformLayout(this, "Renderer");
					this.OnRendererChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Gets or sets a value that indicates which visual styles will be applied to the <see cref="T:System.Windows.Forms.ToolStrip" />.</summary>
		/// <returns>A value that indicates the visual style to apply. The default is <see cref="F:System.Windows.Forms.ToolStripRenderMode.ManagerRenderMode" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value being set is not one of the <see cref="T:System.Windows.Forms.ToolStripRenderMode" /> values.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <see cref="T:System.Windows.Forms.ToolStripRenderMode" /> is set to <see cref="F:System.Windows.Forms.ToolStripRenderMode.Custom" /> without the <see cref="P:System.Windows.Forms.ToolStrip.Renderer" /> property being assigned to a new instance of <see cref="T:System.Windows.Forms.ToolStripRenderer" />.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x060012C3 RID: 4803 RVA: 0x0005FEDD File Offset: 0x0005E0DD
		public ToolStripRenderMode RenderMode
		{
			get
			{
				return this.render_mode;
			}
		}

		/// <summary>Gets or sets the direction in which to draw text on a <see cref="T:System.Windows.Forms.ToolStrip" />.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ToolStripTextDirection" /> values. The default is <see cref="F:System.Windows.Forms.ToolStripTextDirection.Horizontal" />. </returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value is not one of the <see cref="T:System.Windows.Forms.ToolStripTextDirection" /> values.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x060012C4 RID: 4804 RVA: 0x0005FEE5 File Offset: 0x0005E0E5
		[DefaultValue(ToolStripTextDirection.Horizontal)]
		public virtual ToolStripTextDirection TextDirection
		{
			get
			{
				return this.text_direction;
			}
		}

		/// <summary>Gets the docking location of the <see cref="T:System.Windows.Forms.ToolStrip" />, indicating which borders are docked to the container.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DockStyle" /> values. The default is <see cref="F:System.Windows.Forms.DockStyle.Top" />.</returns>
		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x060012C5 RID: 4805 RVA: 0x00006F54 File Offset: 0x00005154
		protected virtual DockStyle DefaultDock
		{
			get
			{
				return DockStyle.Top;
			}
		}

		/// <summary>Gets the default spacing, in pixels, between the sizing grip and the edges of the <see cref="T:System.Windows.Forms.ToolStrip" />.</summary>
		/// <returns>
		///   <see cref="T:System.Windows.Forms.Padding" /> values representing the spacing, in pixels.</returns>
		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x060012C6 RID: 4806 RVA: 0x0005FEED File Offset: 0x0005E0ED
		protected virtual Padding DefaultGripMargin
		{
			get
			{
				return new Padding(2);
			}
		}

		/// <summary>Gets the spacing, in pixels, between the <see cref="T:System.Windows.Forms.ToolStrip" /> and the <see cref="T:System.Windows.Forms.ToolStripContainer" />.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.Padding" /> values. The default is <see cref="F:System.Windows.Forms.Padding.Empty" />.</returns>
		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x060012C7 RID: 4807 RVA: 0x00040EBE File Offset: 0x0003F0BE
		protected override Padding DefaultMargin
		{
			get
			{
				return Padding.Empty;
			}
		}

		/// <summary>Gets the internal spacing, in pixels, of the contents of a <see cref="T:System.Windows.Forms.ToolStrip" />.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Padding" /> value of (0, 0, 1, 0).</returns>
		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x060012C8 RID: 4808 RVA: 0x0005FEF5 File Offset: 0x0005E0F5
		protected override Padding DefaultPadding
		{
			get
			{
				return new Padding(0, 0, 1, 0);
			}
		}

		/// <summary>Gets a value indicating whether ToolTips are shown for the <see cref="T:System.Windows.Forms.ToolStrip" /> by default.</summary>
		/// <returns>true in all cases.</returns>
		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x060012C9 RID: 4809 RVA: 0x00006F54 File Offset: 0x00005154
		protected virtual bool DefaultShowItemToolTips
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets the default size of the <see cref="T:System.Windows.Forms.ToolStrip" />.</summary>
		/// <returns>The default <see cref="T:System.Drawing.Size" /> of the <see cref="T:System.Windows.Forms.ToolStrip" />.</returns>
		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x060012CA RID: 4810 RVA: 0x0005FF00 File Offset: 0x0005E100
		protected override Size DefaultSize
		{
			get
			{
				return new Size(100, 25);
			}
		}

		/// <summary>Gets the subset of items that are currently displayed on the <see cref="T:System.Windows.Forms.ToolStrip" />, including items that are automatically added into the <see cref="T:System.Windows.Forms.ToolStrip" />.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ToolStripItemCollection" /> representing the items that are currently displayed on the <see cref="T:System.Windows.Forms.ToolStrip" />.</returns>
		// Token: 0x170004F1 RID: 1265
		// (get) Token: 0x060012CB RID: 4811 RVA: 0x0005FF0B File Offset: 0x0005E10B
		protected internal virtual ToolStripItemCollection DisplayedItems
		{
			get
			{
				return this.displayed_items;
			}
		}

		/// <summary>Returns the item located at the specified point in the client area of the <see cref="T:System.Windows.Forms.ToolStrip" />.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ToolStripItem" /> at the specified location, or null if the <see cref="T:System.Windows.Forms.ToolStripItem" /> is not found.</returns>
		/// <param name="point">The <see cref="T:System.Drawing.Point" /> at which to search for the <see cref="T:System.Windows.Forms.ToolStripItem" />. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060012CC RID: 4812 RVA: 0x0005FF14 File Offset: 0x0005E114
		public ToolStripItem GetItemAt(Point point)
		{
			foreach (object obj in this.displayed_items)
			{
				ToolStripItem toolStripItem = (ToolStripItem)obj;
				if (toolStripItem.Visible && toolStripItem.Bounds.Contains(point))
				{
					return toolStripItem;
				}
			}
			return null;
		}

		/// <summary>Returns the item located at the specified x- and y-coordinates of the <see cref="T:System.Windows.Forms.ToolStrip" /> client area.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ToolStripItem" /> located at the specified location, or null if the <see cref="T:System.Windows.Forms.ToolStripItem" /> is not found.</returns>
		/// <param name="x">The horizontal coordinate, in pixels, from the left edge of the client area. </param>
		/// <param name="y">The vertical coordinate, in pixels, from the top edge of the client area. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060012CD RID: 4813 RVA: 0x0005FF8C File Offset: 0x0005E18C
		public ToolStripItem GetItemAt(int x, int y)
		{
			return this.GetItemAt(new Point(x, y));
		}

		/// <summary>Retrieves the next <see cref="T:System.Windows.Forms.ToolStripItem" /> from the specified reference point and moving in the specified direction.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ToolStripItem" /> that is specified by the <paramref name="start" /> parameter and is next in the order as specified by the <paramref name="direction" /> parameter.</returns>
		/// <param name="start">The <see cref="T:System.Windows.Forms.ToolStripItem" /> that is the reference point from which to begin the retrieval of the next item.</param>
		/// <param name="direction">One of the values of <see cref="T:System.Windows.Forms.ArrowDirection" /> that specifies the direction to move.</param>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value of the <paramref name="direction" /> parameter is not one of the values of <see cref="T:System.Windows.Forms.ArrowDirection" />.</exception>
		// Token: 0x060012CE RID: 4814 RVA: 0x0005FF9C File Offset: 0x0005E19C
		public virtual ToolStripItem GetNextItem(ToolStripItem start, ArrowDirection direction)
		{
			if (!Enum.IsDefined(typeof(ArrowDirection), direction))
			{
				throw new InvalidEnumArgumentException(string.Format("Enum argument value '{0}' is not valid for ArrowDirection", direction));
			}
			ToolStripItem toolStripItem = null;
			int num;
			if (direction <= ArrowDirection.Up)
			{
				if (direction == ArrowDirection.Left)
				{
					goto IL_0212;
				}
				if (direction != ArrowDirection.Up)
				{
					return toolStripItem;
				}
			}
			else if (direction != ArrowDirection.Right)
			{
				if (direction != ArrowDirection.Down)
				{
					return toolStripItem;
				}
				goto IL_02F3;
			}
			else
			{
				num = int.MaxValue;
				if (start != null)
				{
					foreach (object obj in this.DisplayedItems)
					{
						ToolStripItem toolStripItem2 = (ToolStripItem)obj;
						if (toolStripItem2.Left >= start.Right && toolStripItem2.Left < num && toolStripItem2.Visible && toolStripItem2.CanSelect)
						{
							toolStripItem = toolStripItem2;
							num = toolStripItem2.Left;
						}
					}
				}
				if (toolStripItem != null)
				{
					return toolStripItem;
				}
				using (IEnumerator enumerator = this.DisplayedItems.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj2 = enumerator.Current;
						ToolStripItem toolStripItem3 = (ToolStripItem)obj2;
						if (toolStripItem3.Left < num && toolStripItem3.Visible && toolStripItem3.CanSelect)
						{
							toolStripItem = toolStripItem3;
							num = toolStripItem3.Left;
						}
					}
					return toolStripItem;
				}
			}
			num = int.MinValue;
			if (start != null)
			{
				foreach (object obj3 in this.DisplayedItems)
				{
					ToolStripItem toolStripItem4 = (ToolStripItem)obj3;
					if (toolStripItem4.Bottom <= start.Top && toolStripItem4.Top > num && toolStripItem4.Visible && toolStripItem4.CanSelect)
					{
						toolStripItem = toolStripItem4;
						num = toolStripItem4.Top;
					}
				}
			}
			if (toolStripItem != null)
			{
				return toolStripItem;
			}
			using (IEnumerator enumerator = this.DisplayedItems.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					object obj4 = enumerator.Current;
					ToolStripItem toolStripItem5 = (ToolStripItem)obj4;
					if (toolStripItem5.Top > num && toolStripItem5.Visible && toolStripItem5.CanSelect)
					{
						toolStripItem = toolStripItem5;
						num = toolStripItem5.Top;
					}
				}
				return toolStripItem;
			}
			IL_0212:
			num = int.MinValue;
			if (start != null)
			{
				foreach (object obj5 in this.DisplayedItems)
				{
					ToolStripItem toolStripItem6 = (ToolStripItem)obj5;
					if (toolStripItem6.Right <= start.Left && toolStripItem6.Left > num && toolStripItem6.Visible && toolStripItem6.CanSelect)
					{
						toolStripItem = toolStripItem6;
						num = toolStripItem6.Left;
					}
				}
			}
			if (toolStripItem != null)
			{
				return toolStripItem;
			}
			using (IEnumerator enumerator = this.DisplayedItems.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					object obj6 = enumerator.Current;
					ToolStripItem toolStripItem7 = (ToolStripItem)obj6;
					if (toolStripItem7.Left > num && toolStripItem7.Visible && toolStripItem7.CanSelect)
					{
						toolStripItem = toolStripItem7;
						num = toolStripItem7.Left;
					}
				}
				return toolStripItem;
			}
			IL_02F3:
			num = int.MaxValue;
			if (start != null)
			{
				foreach (object obj7 in this.DisplayedItems)
				{
					ToolStripItem toolStripItem8 = (ToolStripItem)obj7;
					if (toolStripItem8.Top >= start.Bottom && toolStripItem8.Bottom < num && toolStripItem8.Visible && toolStripItem8.CanSelect)
					{
						toolStripItem = toolStripItem8;
						num = toolStripItem8.Top;
					}
				}
			}
			if (toolStripItem == null)
			{
				foreach (object obj8 in this.DisplayedItems)
				{
					ToolStripItem toolStripItem9 = (ToolStripItem)obj8;
					if (toolStripItem9.Top < num && toolStripItem9.Visible && toolStripItem9.CanSelect)
					{
						toolStripItem = toolStripItem9;
						num = toolStripItem9.Top;
					}
				}
			}
			return toolStripItem;
		}

		/// <summary>Returns a string that represents the <see cref="T:System.Windows.Forms.ToolStrip" /> control.</summary>
		/// <returns>A string that represents the <see cref="T:System.Windows.Forms.ToolStrip" /> control.</returns>
		// Token: 0x060012CF RID: 4815 RVA: 0x000603DC File Offset: 0x0005E5DC
		public override string ToString()
		{
			return string.Format("{0}, Name: {1}, Items: {2}", base.ToString(), base.Name, this.items.Count.ToString());
		}

		/// <summary>Creates a new instance of the control collection for the control.</summary>
		/// <returns>A new instance of <see cref="T:System.Windows.Forms.Control.ControlCollection" /> assigned to the control.</returns>
		// Token: 0x060012D0 RID: 4816 RVA: 0x0001EBD1 File Offset: 0x0001CDD1
		protected override Control.ControlCollection CreateControlsInstance()
		{
			return base.CreateControlsInstance();
		}

		/// <summary>Creates a default <see cref="T:System.Windows.Forms.ToolStripItem" /> with the specified text, image, and event handler on a new <see cref="T:System.Windows.Forms.ToolStrip" /> instance.</summary>
		/// <returns>A <see cref="M:System.Windows.Forms.ToolStripButton.#ctor(System.String,System.Drawing.Image,System.EventHandler)" />, or a <see cref="T:System.Windows.Forms.ToolStripSeparator" /> if the <paramref name="text" /> parameter is a hyphen (-).</returns>
		/// <param name="text">The text to use for the <see cref="T:System.Windows.Forms.ToolStripItem" />. If the <paramref name="text" /> parameter is a hyphen (-), this method creates a <see cref="T:System.Windows.Forms.ToolStripSeparator" />.</param>
		/// <param name="image">The <see cref="T:System.Drawing.Image" /> to display on the <see cref="T:System.Windows.Forms.ToolStripItem" />.</param>
		/// <param name="onClick">An event handler that raises the <see cref="E:System.Windows.Forms.Control.Click" /> event when the <see cref="T:System.Windows.Forms.ToolStripItem" /> is clicked.</param>
		// Token: 0x060012D1 RID: 4817 RVA: 0x00060412 File Offset: 0x0005E612
		protected internal virtual ToolStripItem CreateDefaultItem(string text, Image image, EventHandler onClick)
		{
			if (text == "-")
			{
				return new ToolStripSeparator();
			}
			if (this is ToolStripDropDown)
			{
				return new ToolStripMenuItem(text, image, onClick);
			}
			return new ToolStripButton(text, image, onClick);
		}

		/// <summary>Specifies the visual arrangement for the <see cref="T:System.Windows.Forms.ToolStrip" />.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ToolStripLayoutStyle" /> values. The default is null.</returns>
		/// <param name="layoutStyle">The visual arrangement to be applied to the <see cref="T:System.Windows.Forms.ToolStrip" />.</param>
		// Token: 0x060012D2 RID: 4818 RVA: 0x00060440 File Offset: 0x0005E640
		protected virtual LayoutSettings CreateLayoutSettings(ToolStripLayoutStyle layoutStyle)
		{
			switch (layoutStyle)
			{
			case ToolStripLayoutStyle.Flow:
				return new FlowLayoutSettings(this);
			}
			return null;
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.ToolStrip" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x060012D3 RID: 4819 RVA: 0x00060468 File Offset: 0x0005E668
		protected override void Dispose(bool disposing)
		{
			if (!base.IsDisposed)
			{
				if (disposing)
				{
					base.Events.Dispose();
					this.CloseToolTip(null);
					for (int i = this.Items.Count - 1; i >= 0; i--)
					{
						this.Items[i].Dispose();
					}
					if (this.overflow_button != null && this.overflow_button.drop_down != null)
					{
						this.overflow_button.drop_down.Dispose();
					}
					ToolStripManager.RemoveToolStrip(this);
				}
				base.Dispose(disposing);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.DockChanged" /> event. </summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060012D4 RID: 4820 RVA: 0x000604ED File Offset: 0x0005E6ED
		protected override void OnDockChanged(EventArgs e)
		{
			base.OnDockChanged(e);
		}

		/// <summary>Determines whether a character is an input character that the item recognizes.</summary>
		/// <returns>true if the character should be sent directly to the item and not preprocessed; otherwise, false.</returns>
		/// <param name="charCode">The character to test.</param>
		// Token: 0x060012D5 RID: 4821 RVA: 0x000604F6 File Offset: 0x0005E6F6
		protected override bool IsInputChar(char charCode)
		{
			return base.IsInputChar(charCode);
		}

		/// <summary>Determines whether the specified key is a regular input key or a special key that requires preprocessing.</summary>
		/// <returns>true if the specified key is a regular input key; otherwise, false.</returns>
		/// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values.</param>
		// Token: 0x060012D6 RID: 4822 RVA: 0x000604FF File Offset: 0x0005E6FF
		protected override bool IsInputKey(Keys keyData)
		{
			return base.IsInputKey(keyData);
		}

		/// <summary>Raises the <see cref="P:System.Windows.Forms.Control.Enabled" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060012D7 RID: 4823 RVA: 0x00060508 File Offset: 0x0005E708
		protected override void OnEnabledChanged(EventArgs e)
		{
			base.OnEnabledChanged(e);
			foreach (object obj in this.Items)
			{
				((ToolStripItem)obj).OnParentEnabledChanged(EventArgs.Empty);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.FontChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060012D8 RID: 4824 RVA: 0x000040A2 File Offset: 0x000022A2
		protected override void OnFontChanged(EventArgs e)
		{
			base.OnFontChanged(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.HandleCreated" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060012D9 RID: 4825 RVA: 0x00004D0D File Offset: 0x00002F0D
		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.HandleDestroyed" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060012DA RID: 4826 RVA: 0x00006538 File Offset: 0x00004738
		protected override void OnHandleDestroyed(EventArgs e)
		{
			base.OnHandleDestroyed(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Invalidated" /> event.</summary>
		/// <param name="e">An <see cref="T:System.Windows.Forms.InvalidateEventArgs" /> that contains the event data.</param>
		// Token: 0x060012DB RID: 4827 RVA: 0x0006056C File Offset: 0x0005E76C
		protected override void OnInvalidated(InvalidateEventArgs e)
		{
			base.OnInvalidated(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStrip.ItemAdded" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripItemEventArgs" /> that contains the event data.</param>
		// Token: 0x060012DC RID: 4828 RVA: 0x00060578 File Offset: 0x0005E778
		protected internal virtual void OnItemAdded(ToolStripItemEventArgs e)
		{
			if (e.Item.InternalVisible)
			{
				e.Item.Available = true;
			}
			e.Item.SetPlacement(ToolStripItemPlacement.Main);
			if (base.Created)
			{
				base.PerformLayout();
			}
			ToolStripItemEventHandler toolStripItemEventHandler = (ToolStripItemEventHandler)base.Events[ToolStrip.ItemAddedEvent];
			if (toolStripItemEventHandler != null)
			{
				toolStripItemEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStrip.ItemClicked" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripItemClickedEventArgs" /> that contains the event data. </param>
		// Token: 0x060012DD RID: 4829 RVA: 0x000605DC File Offset: 0x0005E7DC
		protected virtual void OnItemClicked(ToolStripItemClickedEventArgs e)
		{
			if (this.KeyboardActive)
			{
				ToolStripManager.SetActiveToolStrip(null, false);
			}
			ToolStripItemClickedEventHandler toolStripItemClickedEventHandler = (ToolStripItemClickedEventHandler)base.Events[ToolStrip.ItemClickedEvent];
			if (toolStripItemClickedEventHandler != null)
			{
				toolStripItemClickedEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStrip.ItemRemoved" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripItemEventArgs" /> that contains the event data.</param>
		// Token: 0x060012DE RID: 4830 RVA: 0x0006061C File Offset: 0x0005E81C
		protected internal virtual void OnItemRemoved(ToolStripItemEventArgs e)
		{
			ToolStripItemEventHandler toolStripItemEventHandler = (ToolStripItemEventHandler)base.Events[ToolStrip.ItemRemovedEvent];
			if (toolStripItemEventHandler != null)
			{
				toolStripItemEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Layout" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.LayoutEventArgs" /> that contains the event data. </param>
		// Token: 0x060012DF RID: 4831 RVA: 0x0006064A File Offset: 0x0005E84A
		protected override void OnLayout(LayoutEventArgs e)
		{
			base.OnLayout(e);
			this.SetDisplayedItems();
			this.OnLayoutCompleted(EventArgs.Empty);
			base.Invalidate();
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStrip.LayoutCompleted" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060012E0 RID: 4832 RVA: 0x0006066C File Offset: 0x0005E86C
		protected virtual void OnLayoutCompleted(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ToolStrip.LayoutCompletedEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStrip.LayoutStyleChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060012E1 RID: 4833 RVA: 0x0006069C File Offset: 0x0005E89C
		protected virtual void OnLayoutStyleChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ToolStrip.LayoutStyleChangedEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Leave" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060012E2 RID: 4834 RVA: 0x000606CA File Offset: 0x0005E8CA
		protected override void OnLeave(EventArgs e)
		{
			base.OnLeave(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.LostFocus" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060012E3 RID: 4835 RVA: 0x000606D3 File Offset: 0x0005E8D3
		protected override void OnLostFocus(EventArgs e)
		{
			base.OnLostFocus(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseCaptureChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060012E4 RID: 4836 RVA: 0x000606DC File Offset: 0x0005E8DC
		protected override void OnMouseCaptureChanged(EventArgs e)
		{
			base.OnMouseCaptureChanged(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseDown" /> event.</summary>
		/// <param name="mea">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data. </param>
		// Token: 0x060012E5 RID: 4837 RVA: 0x000606E8 File Offset: 0x0005E8E8
		protected override void OnMouseDown(MouseEventArgs mea)
		{
			if (this.mouse_currently_over != null)
			{
				ToolStripItem currentlyFocusedItem = this.GetCurrentlyFocusedItem();
				if (currentlyFocusedItem != null && currentlyFocusedItem != this.mouse_currently_over)
				{
					this.FocusInternal(true);
				}
				if (this is MenuStrip && !this.menu_selected)
				{
					(this as MenuStrip).FireMenuActivate();
					this.menu_selected = true;
				}
				this.mouse_currently_over.FireEvent(mea, ToolStripItemEventType.MouseDown);
				if (this is MenuStrip && this.mouse_currently_over is ToolStripMenuItem && !(this.mouse_currently_over as ToolStripMenuItem).HasDropDownItems)
				{
					return;
				}
			}
			else
			{
				this.Dismiss(ToolStripDropDownCloseReason.AppClicked);
			}
			if (this is MenuStrip)
			{
				base.Capture = false;
			}
			base.OnMouseDown(mea);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseLeave" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060012E6 RID: 4838 RVA: 0x0006078C File Offset: 0x0005E98C
		protected override void OnMouseLeave(EventArgs e)
		{
			if (this.mouse_currently_over != null)
			{
				this.MouseLeftItem(this.mouse_currently_over);
				this.mouse_currently_over.FireEvent(e, ToolStripItemEventType.MouseLeave);
				this.mouse_currently_over = null;
			}
			base.OnMouseLeave(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseMove" /> event.</summary>
		/// <param name="mea">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data. </param>
		// Token: 0x060012E7 RID: 4839 RVA: 0x000607C0 File Offset: 0x0005E9C0
		protected override void OnMouseMove(MouseEventArgs mea)
		{
			ToolStripItem itemAt;
			if (this.overflow_button != null && this.overflow_button.Visible && this.overflow_button.Bounds.Contains(mea.Location))
			{
				itemAt = this.overflow_button;
			}
			else
			{
				itemAt = this.GetItemAt(mea.X, mea.Y);
			}
			if (itemAt != null)
			{
				if (itemAt == this.mouse_currently_over)
				{
					itemAt.FireEvent(mea, ToolStripItemEventType.MouseMove);
				}
				else
				{
					if (this.mouse_currently_over != null)
					{
						this.MouseLeftItem(itemAt);
						this.mouse_currently_over.FireEvent(mea, ToolStripItemEventType.MouseLeave);
					}
					this.mouse_currently_over = itemAt;
					itemAt.FireEvent(mea, ToolStripItemEventType.MouseEnter);
					this.MouseEnteredItem(itemAt);
					itemAt.FireEvent(mea, ToolStripItemEventType.MouseMove);
					if (this.menu_selected && this.mouse_currently_over.Enabled && this.mouse_currently_over is ToolStripDropDownItem && (this.mouse_currently_over as ToolStripDropDownItem).HasDropDownItems)
					{
						(this.mouse_currently_over as ToolStripDropDownItem).ShowDropDown();
					}
				}
			}
			else if (this.mouse_currently_over != null)
			{
				this.MouseLeftItem(itemAt);
				this.mouse_currently_over.FireEvent(mea, ToolStripItemEventType.MouseLeave);
				this.mouse_currently_over = null;
			}
			base.OnMouseMove(mea);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseUp" /> event.</summary>
		/// <param name="mea">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data. </param>
		// Token: 0x060012E8 RID: 4840 RVA: 0x000608E0 File Offset: 0x0005EAE0
		protected override void OnMouseUp(MouseEventArgs mea)
		{
			if (this.mouse_currently_over != null && !(this.mouse_currently_over is ToolStripControlHost) && this.mouse_currently_over.Enabled)
			{
				if (mea.Button == MouseButtons.Left)
				{
					this.OnItemClicked(new ToolStripItemClickedEventArgs(this.mouse_currently_over));
				}
				if (this.mouse_currently_over != null)
				{
					this.mouse_currently_over.FireEvent(mea, ToolStripItemEventType.MouseUp);
				}
				if (this.mouse_currently_over == null)
				{
					return;
				}
			}
			base.OnMouseUp(mea);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data. </param>
		// Token: 0x060012E9 RID: 4841 RVA: 0x00060954 File Offset: 0x0005EB54
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			this.OnPaintGrip(e);
			for (int i = 0; i < this.displayed_items.Count; i++)
			{
				ToolStripItem toolStripItem = this.displayed_items[i];
				if (toolStripItem.Visible)
				{
					e.Graphics.TranslateTransform((float)toolStripItem.Bounds.Left, (float)toolStripItem.Bounds.Top);
					toolStripItem.FireEvent(e, ToolStripItemEventType.Paint);
					e.Graphics.ResetTransform();
				}
			}
			if (this.overflow_button != null && this.overflow_button.Visible)
			{
				e.Graphics.TranslateTransform((float)this.overflow_button.Bounds.Left, (float)this.overflow_button.Bounds.Top);
				this.overflow_button.FireEvent(e, ToolStripItemEventType.Paint);
				e.Graphics.ResetTransform();
			}
			Rectangle rectangle = new Rectangle(Point.Empty, base.Size);
			ToolStripRenderEventArgs toolStripRenderEventArgs = new ToolStripRenderEventArgs(e.Graphics, this, rectangle, Color.Empty);
			toolStripRenderEventArgs.InternalConnectedArea = this.CalculateConnectedArea();
			this.Renderer.DrawToolStripBorder(toolStripRenderEventArgs);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event for the <see cref="T:System.Windows.Forms.ToolStrip" /> background.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains information about the control to paint. </param>
		// Token: 0x060012EA RID: 4842 RVA: 0x00060A78 File Offset: 0x0005EC78
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnPaintBackground(PaintEventArgs e)
		{
			base.OnPaintBackground(e);
			Rectangle rectangle = new Rectangle(Point.Empty, base.Size);
			ToolStripRenderEventArgs toolStripRenderEventArgs = new ToolStripRenderEventArgs(e.Graphics, this, rectangle, SystemColors.Control);
			this.Renderer.DrawToolStripBackground(toolStripRenderEventArgs);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStrip.PaintGrip" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data. </param>
		// Token: 0x060012EB RID: 4843 RVA: 0x00060AC0 File Offset: 0x0005ECC0
		protected internal virtual void OnPaintGrip(PaintEventArgs e)
		{
			if (this.layout_style == ToolStripLayoutStyle.Flow || this.layout_style == ToolStripLayoutStyle.Table)
			{
				return;
			}
			PaintEventHandler paintEventHandler = (PaintEventHandler)base.Events[ToolStrip.PaintGripEvent];
			if (paintEventHandler != null)
			{
				paintEventHandler(this, e);
			}
			if (!(this is MenuStrip))
			{
				if (this.orientation == Orientation.Horizontal)
				{
					e.Graphics.TranslateTransform(2f, 0f);
				}
				else
				{
					e.Graphics.TranslateTransform(0f, 2f);
				}
			}
			this.Renderer.DrawGrip(new ToolStripGripRenderEventArgs(e.Graphics, this, this.GripRectangle, this.GripDisplayStyle, this.grip_style));
			e.Graphics.ResetTransform();
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStrip.RendererChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060012EC RID: 4844 RVA: 0x00060B74 File Offset: 0x0005ED74
		protected virtual void OnRendererChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ToolStrip.RendererChangedEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.RightToLeftChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060012ED RID: 4845 RVA: 0x00060BA4 File Offset: 0x0005EDA4
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnRightToLeftChanged(EventArgs e)
		{
			base.OnRightToLeftChanged(e);
			foreach (object obj in this.Items)
			{
				((ToolStripItem)obj).OnParentRightToLeftChanged(e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ScrollableControl.Scroll" /> event.</summary>
		/// <param name="se">A <see cref="T:System.Windows.Forms.ScrollEventArgs" /> that contains the event data.</param>
		// Token: 0x060012EE RID: 4846 RVA: 0x00060C04 File Offset: 0x0005EE04
		protected override void OnScroll(ScrollEventArgs se)
		{
			base.OnScroll(se);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.TabStopChanged" /> event. </summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060012EF RID: 4847 RVA: 0x00060C0D File Offset: 0x0005EE0D
		protected override void OnTabStopChanged(EventArgs e)
		{
			base.OnTabStopChanged(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.VisibleChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060012F0 RID: 4848 RVA: 0x00060C16 File Offset: 0x0005EE16
		protected override void OnVisibleChanged(EventArgs e)
		{
			if (!base.Visible)
			{
				this.CloseToolTip(null);
			}
			base.OnVisibleChanged(e);
		}

		/// <summary>Processes a command key.</summary>
		/// <returns>true if the character was processed by the control; otherwise, false.</returns>
		/// <param name="m">A <see cref="T:System.Windows.Forms.Message" />, passed by reference, that represents the window message to process.</param>
		/// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values that represents the key to process.</param>
		// Token: 0x060012F1 RID: 4849 RVA: 0x00046AEE File Offset: 0x00044CEE
		protected override bool ProcessCmdKey(ref Message m, Keys keyData)
		{
			return base.ProcessCmdKey(ref m, keyData);
		}

		/// <summary>Processes a dialog box key.</summary>
		/// <returns>true if the key was processed by the control; otherwise, false.</returns>
		/// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values that represents the key to process. </param>
		// Token: 0x060012F2 RID: 4850 RVA: 0x00060C30 File Offset: 0x0005EE30
		protected override bool ProcessDialogKey(Keys keyData)
		{
			if (!this.KeyboardActive)
			{
				return false;
			}
			using (IEnumerator enumerator = this.Items.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (((ToolStripItem)enumerator.Current).ProcessDialogKey(keyData))
					{
						return true;
					}
				}
			}
			if (this.ProcessArrowKey(keyData))
			{
				return true;
			}
			ToolStrip toolStrip = null;
			if (keyData <= Keys.Down)
			{
				if (keyData == Keys.Escape)
				{
					this.Dismiss(ToolStripDropDownCloseReason.Keyboard);
					return true;
				}
				if (keyData - Keys.Left <= 3)
				{
					if (this.GetCurrentlySelectedItem() is ToolStripControlHost)
					{
						return false;
					}
				}
			}
			else
			{
				if (keyData == (Keys.LButton | Keys.Back | Keys.Control))
				{
					toolStrip = ToolStripManager.GetNextToolStrip(this, true);
					if (toolStrip != null)
					{
						foreach (object obj in this.Items)
						{
							((ToolStripItem)obj).Dismiss(ToolStripDropDownCloseReason.Keyboard);
						}
						ToolStripManager.SetActiveToolStrip(toolStrip, true);
						toolStrip.SelectNextToolStripItem(null, true);
					}
					return true;
				}
				if (keyData == (Keys.LButton | Keys.Back | Keys.Shift | Keys.Control))
				{
					toolStrip = ToolStripManager.GetNextToolStrip(this, false);
					if (toolStrip != null)
					{
						foreach (object obj2 in this.Items)
						{
							((ToolStripItem)obj2).Dismiss(ToolStripDropDownCloseReason.Keyboard);
						}
						ToolStripManager.SetActiveToolStrip(toolStrip, true);
						toolStrip.SelectNextToolStripItem(null, true);
					}
					return true;
				}
			}
			return base.ProcessDialogKey(keyData);
		}

		/// <summary>Processes a mnemonic character.</summary>
		/// <returns>true if the character was processed as a mnemonic by the control; otherwise, false.</returns>
		/// <param name="charCode">The character to process. </param>
		// Token: 0x060012F3 RID: 4851 RVA: 0x00060DBC File Offset: 0x0005EFBC
		protected override bool ProcessMnemonic(char charCode)
		{
			foreach (object obj in this.Items)
			{
				ToolStripItem toolStripItem = (ToolStripItem)obj;
				if (toolStripItem.Enabled && toolStripItem.Visible && !string.IsNullOrEmpty(toolStripItem.Text) && Control.IsMnemonic(charCode, toolStripItem.Text))
				{
					return toolStripItem.ProcessMnemonic(charCode);
				}
			}
			return base.ProcessMnemonic(charCode);
		}

		/// <summary>Activates a child control. Optionally specifies the direction in the tab order to select the control from.</summary>
		/// <param name="directed">true to specify the direction of the control to select; otherwise, false.</param>
		/// <param name="forward">true to move forward in the tab order; false to move backward in the tab order.</param>
		// Token: 0x060012F4 RID: 4852 RVA: 0x00060E4C File Offset: 0x0005F04C
		protected override void Select(bool directed, bool forward)
		{
			foreach (object obj in this.DisplayedItems)
			{
				ToolStripItem toolStripItem = (ToolStripItem)obj;
				if (toolStripItem.CanSelect)
				{
					toolStripItem.Select();
					break;
				}
			}
		}

		/// <summary>Performs the work of setting the specified bounds of this control.</summary>
		/// <param name="x">The new <see cref="P:System.Windows.Forms.Control.Left" /> property value of the control.</param>
		/// <param name="y">The new <see cref="P:System.Windows.Forms.Control.Top" /> property value of the control.</param>
		/// <param name="width">The new <see cref="P:System.Windows.Forms.Control.Width" /> property value of the control.</param>
		/// <param name="height">The new <see cref="P:System.Windows.Forms.Control.Height" /> property value of the control.</param>
		/// <param name="specified">A bitwise combination of the <see cref="T:System.Windows.Forms.BoundsSpecified" /> values.</param>
		// Token: 0x060012F5 RID: 4853 RVA: 0x000254DA File Offset: 0x000236DA
		protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
		{
			base.SetBoundsCore(x, y, width, height, specified);
		}

		/// <summary>Resets the collection of displayed and overflow items after a layout is done.</summary>
		// Token: 0x060012F6 RID: 4854 RVA: 0x00060EB0 File Offset: 0x0005F0B0
		protected virtual void SetDisplayedItems()
		{
			this.displayed_items.ClearInternal();
			foreach (object obj in this.items)
			{
				ToolStripItem toolStripItem = (ToolStripItem)obj;
				if (toolStripItem.Placement == ToolStripItemPlacement.Main && toolStripItem.Available)
				{
					this.displayed_items.AddNoOwnerOrLayout(toolStripItem);
					toolStripItem.Parent = this;
				}
				else if (toolStripItem.Placement == ToolStripItemPlacement.Overflow)
				{
					toolStripItem.Parent = this.OverflowButton.DropDown;
				}
			}
			if (this.OverflowButton != null)
			{
				this.OverflowButton.DropDown.SetDisplayedItems();
			}
		}

		/// <summary>Enables you to change the parent <see cref="T:System.Windows.Forms.ToolStrip" /> of a <see cref="T:System.Windows.Forms.ToolStripItem" />.</summary>
		/// <param name="item">The <see cref="T:System.Windows.Forms.ToolStripItem" /> whose <see cref="P:System.Windows.Forms.Control.Parent" /> property is to be changed. </param>
		/// <param name="parent">The <see cref="T:System.Windows.Forms.ToolStrip" /> that is the parent of the <see cref="T:System.Windows.Forms.ToolStripItem" /> referred to by the <paramref name="item" /> parameter. </param>
		// Token: 0x060012F7 RID: 4855 RVA: 0x00060F68 File Offset: 0x0005F168
		protected internal static void SetItemParent(ToolStripItem item, ToolStrip parent)
		{
			if (item.Owner != null)
			{
				item.Owner.Items.RemoveNoOwnerOrLayout(item);
				if (item.Owner is ToolStripOverflow)
				{
					(item.Owner as ToolStripOverflow).ParentToolStrip.Items.RemoveNoOwnerOrLayout(item);
				}
			}
			parent.Items.AddNoOwnerOrLayout(item);
			item.Parent = parent;
		}

		/// <summary>Retrieves a value that sets the <see cref="T:System.Windows.Forms.ToolStripItem" /> to the specified visibility state.</summary>
		/// <param name="visible">true if the <see cref="T:System.Windows.Forms.ToolStripItem" /> is visible; otherwise, false. </param>
		// Token: 0x060012F8 RID: 4856 RVA: 0x00060FCA File Offset: 0x0005F1CA
		protected override void SetVisibleCore(bool visible)
		{
			base.SetVisibleCore(visible);
		}

		/// <summary>Processes Windows messages.</summary>
		/// <param name="m">The Windows <see cref="T:System.Windows.Forms.Message" /> to process.</param>
		// Token: 0x060012F9 RID: 4857 RVA: 0x00060FD3 File Offset: 0x0005F1D3
		protected override void WndProc(ref Message m)
		{
			base.WndProc(ref m);
		}

		/// <summary>Occurs when a new <see cref="T:System.Windows.Forms.ToolStripItem" /> is added to the <see cref="T:System.Windows.Forms.ToolStripItemCollection" />.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000046 RID: 70
		// (add) Token: 0x060012FA RID: 4858 RVA: 0x00060FDC File Offset: 0x0005F1DC
		// (remove) Token: 0x060012FB RID: 4859 RVA: 0x00060FEF File Offset: 0x0005F1EF
		public event ToolStripItemEventHandler ItemAdded
		{
			add
			{
				base.Events.AddHandler(ToolStrip.ItemAddedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStrip.ItemAddedEvent, value);
			}
		}

		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x060012FC RID: 4860 RVA: 0x00061002 File Offset: 0x0005F202
		// (set) Token: 0x060012FD RID: 4861 RVA: 0x0006100A File Offset: 0x0005F20A
		internal virtual bool KeyboardActive
		{
			get
			{
				return this.keyboard_active;
			}
			set
			{
				if (this.keyboard_active != value)
				{
					this.keyboard_active = value;
					if (value)
					{
						Application.KeyboardCapture = this;
					}
					else if (Application.KeyboardCapture == this)
					{
						Application.KeyboardCapture = null;
						ToolStripManager.ActivatedByKeyboard = false;
					}
					base.Invalidate();
				}
			}
		}

		// Token: 0x060012FE RID: 4862 RVA: 0x00061041 File Offset: 0x0005F241
		internal virtual Rectangle CalculateConnectedArea()
		{
			return Rectangle.Empty;
		}

		// Token: 0x060012FF RID: 4863 RVA: 0x00061048 File Offset: 0x0005F248
		internal void ChangeSelection(ToolStripItem nextItem)
		{
			if (Application.KeyboardCapture != this)
			{
				ToolStripManager.SetActiveToolStrip(this, ToolStripManager.ActivatedByKeyboard);
			}
			foreach (object obj in this.Items)
			{
				ToolStripItem toolStripItem = (ToolStripItem)obj;
				if (toolStripItem != nextItem)
				{
					toolStripItem.Dismiss(ToolStripDropDownCloseReason.Keyboard);
				}
			}
			ToolStripItem currentlySelectedItem = this.GetCurrentlySelectedItem();
			if (currentlySelectedItem != null && !(currentlySelectedItem is ToolStripControlHost))
			{
				this.FocusInternal(true);
			}
			if (nextItem is ToolStripControlHost)
			{
				(nextItem as ToolStripControlHost).Focus();
			}
			nextItem.Select();
			if (nextItem.Parent is MenuStrip && (nextItem.Parent as MenuStrip).MenuDroppedDown)
			{
				(nextItem as ToolStripMenuItem).HandleAutoExpansion();
			}
		}

		// Token: 0x06001300 RID: 4864 RVA: 0x00061118 File Offset: 0x0005F318
		internal virtual void Dismiss(ToolStripDropDownCloseReason reason)
		{
			this.KeyboardActive = false;
			this.menu_selected = false;
			foreach (object obj in this.Items)
			{
				((ToolStripItem)obj).Dismiss(reason);
			}
			base.Invalidate();
		}

		// Token: 0x06001301 RID: 4865 RVA: 0x00061184 File Offset: 0x0005F384
		internal ToolStripItem GetCurrentlySelectedItem()
		{
			foreach (object obj in this.DisplayedItems)
			{
				ToolStripItem toolStripItem = (ToolStripItem)obj;
				if (toolStripItem.Selected)
				{
					return toolStripItem;
				}
			}
			return null;
		}

		// Token: 0x06001302 RID: 4866 RVA: 0x000611E8 File Offset: 0x0005F3E8
		internal ToolStripItem GetCurrentlyFocusedItem()
		{
			foreach (object obj in this.DisplayedItems)
			{
				ToolStripItem toolStripItem = (ToolStripItem)obj;
				if (toolStripItem is ToolStripControlHost && (toolStripItem as ToolStripControlHost).Control.Focused)
				{
					return toolStripItem;
				}
			}
			return null;
		}

		// Token: 0x06001303 RID: 4867 RVA: 0x0006125C File Offset: 0x0005F45C
		internal override Size GetPreferredSizeCore(Size proposedSize)
		{
			return this.GetToolStripPreferredSize(proposedSize);
		}

		// Token: 0x06001304 RID: 4868 RVA: 0x00061268 File Offset: 0x0005F468
		internal virtual Size GetToolStripPreferredSize(Size proposedSize)
		{
			Size empty = Size.Empty;
			if (this.LayoutStyle == ToolStripLayoutStyle.Flow)
			{
				Point empty2 = Point.Empty;
				int num = 0;
				foreach (object obj in this.items)
				{
					ToolStripItem toolStripItem = (ToolStripItem)obj;
					if (toolStripItem.Available)
					{
						Size preferredSize = toolStripItem.GetPreferredSize(Size.Empty);
						if (this.DisplayRectangle.Width - empty2.X < preferredSize.Width + toolStripItem.Margin.Horizontal)
						{
							empty2.Y += num;
							num = 0;
							empty2.X = this.DisplayRectangle.Left;
						}
						empty2.Offset(toolStripItem.Margin.Left, 0);
						num = Math.Max(num, preferredSize.Height + toolStripItem.Margin.Vertical);
						empty2.X += preferredSize.Width + toolStripItem.Margin.Right;
					}
				}
				empty2.Y += num;
				return new Size(empty2.X + base.Padding.Horizontal, empty2.Y + base.Padding.Vertical);
			}
			if (this.orientation == Orientation.Vertical)
			{
				foreach (object obj2 in this.items)
				{
					ToolStripItem toolStripItem2 = (ToolStripItem)obj2;
					if (toolStripItem2.Available)
					{
						Size preferredSize2 = toolStripItem2.GetPreferredSize(Size.Empty);
						empty.Height += preferredSize2.Height + toolStripItem2.Margin.Top + toolStripItem2.Margin.Bottom;
						if (empty.Width < base.Padding.Horizontal + preferredSize2.Width + toolStripItem2.Margin.Horizontal)
						{
							empty.Width = base.Padding.Horizontal + preferredSize2.Width + toolStripItem2.Margin.Horizontal;
						}
					}
				}
				empty.Height += this.GripRectangle.Height + this.GripMargin.Vertical + base.Padding.Vertical + 4;
				if (empty.Width == 0)
				{
					empty.Width = base.ExplicitBounds.Width;
				}
				return empty;
			}
			foreach (object obj3 in this.items)
			{
				ToolStripItem toolStripItem3 = (ToolStripItem)obj3;
				if (toolStripItem3.Available)
				{
					Size preferredSize3 = toolStripItem3.GetPreferredSize(Size.Empty);
					empty.Width += preferredSize3.Width + toolStripItem3.Margin.Left + toolStripItem3.Margin.Right;
					if (empty.Height < base.Padding.Vertical + preferredSize3.Height + toolStripItem3.Margin.Vertical)
					{
						empty.Height = base.Padding.Vertical + preferredSize3.Height + toolStripItem3.Margin.Vertical;
					}
				}
			}
			empty.Width += this.GripRectangle.Width + this.GripMargin.Horizontal + base.Padding.Horizontal + 4;
			if (empty.Height == 0)
			{
				empty.Height = base.ExplicitBounds.Height;
			}
			if (this is StatusStrip)
			{
				empty.Height = Math.Max(empty.Height, 22);
			}
			return empty;
		}

		// Token: 0x06001305 RID: 4869 RVA: 0x00002F7A File Offset: 0x0000117A
		internal virtual ToolStrip GetTopLevelToolStrip()
		{
			return this;
		}

		// Token: 0x06001306 RID: 4870 RVA: 0x000616D0 File Offset: 0x0005F8D0
		internal virtual void HandleItemClick(ToolStripItem dismissingItem)
		{
			this.GetTopLevelToolStrip().Dismiss(ToolStripDropDownCloseReason.ItemClicked);
		}

		// Token: 0x06001307 RID: 4871 RVA: 0x000616E0 File Offset: 0x0005F8E0
		internal void NotifySelectedChanged(ToolStripItem tsi)
		{
			foreach (object obj in this.DisplayedItems)
			{
				ToolStripItem toolStripItem = (ToolStripItem)obj;
				if (tsi != toolStripItem && toolStripItem is ToolStripDropDownItem)
				{
					(toolStripItem as ToolStripDropDownItem).HideDropDown(ToolStripDropDownCloseReason.Keyboard);
				}
			}
			if (this.OverflowButton != null)
			{
				foreach (object obj2 in this.OverflowButton.DropDown.DisplayedItems)
				{
					ToolStripItem toolStripItem2 = (ToolStripItem)obj2;
					if (tsi != toolStripItem2 && toolStripItem2 is ToolStripDropDownItem)
					{
						(toolStripItem2 as ToolStripDropDownItem).HideDropDown(ToolStripDropDownCloseReason.Keyboard);
					}
				}
				this.OverflowButton.HideDropDown();
			}
			foreach (object obj3 in this.Items)
			{
				ToolStripItem toolStripItem3 = (ToolStripItem)obj3;
				if (tsi != toolStripItem3)
				{
					toolStripItem3.Dismiss(ToolStripDropDownCloseReason.Keyboard);
				}
			}
		}

		// Token: 0x06001308 RID: 4872 RVA: 0x00002D70 File Offset: 0x00000F70
		internal virtual bool OnMenuKey()
		{
			return false;
		}

		// Token: 0x06001309 RID: 4873 RVA: 0x00061810 File Offset: 0x0005FA10
		internal virtual bool ProcessArrowKey(Keys keyData)
		{
			if (keyData <= Keys.Left)
			{
				if (keyData == Keys.Tab)
				{
					ToolStripItem toolStripItem = this.GetCurrentlySelectedItem();
					toolStripItem = this.SelectNextToolStripItem(toolStripItem, true);
					if (toolStripItem is ToolStripControlHost)
					{
						(toolStripItem as ToolStripControlHost).Focus();
					}
					return true;
				}
				if (keyData == Keys.Left)
				{
					ToolStripItem toolStripItem = this.GetCurrentlySelectedItem();
					if (toolStripItem is ToolStripControlHost)
					{
						return false;
					}
					toolStripItem = this.SelectNextToolStripItem(toolStripItem, false);
					if (toolStripItem is ToolStripControlHost)
					{
						(toolStripItem as ToolStripControlHost).Focus();
					}
					return true;
				}
			}
			else if (keyData != Keys.Right)
			{
				if (keyData == (Keys.LButton | Keys.Back | Keys.Shift))
				{
					ToolStripItem toolStripItem = this.GetCurrentlySelectedItem();
					toolStripItem = this.SelectNextToolStripItem(toolStripItem, false);
					if (toolStripItem is ToolStripControlHost)
					{
						(toolStripItem as ToolStripControlHost).Focus();
					}
					return true;
				}
			}
			else
			{
				ToolStripItem toolStripItem = this.GetCurrentlySelectedItem();
				if (toolStripItem is ToolStripControlHost)
				{
					return false;
				}
				toolStripItem = this.SelectNextToolStripItem(toolStripItem, true);
				if (toolStripItem is ToolStripControlHost)
				{
					(toolStripItem as ToolStripControlHost).Focus();
				}
				return true;
			}
			return false;
		}

		// Token: 0x0600130A RID: 4874 RVA: 0x000618F0 File Offset: 0x0005FAF0
		internal virtual ToolStripItem SelectNextToolStripItem(ToolStripItem start, bool forward)
		{
			ToolStripItem nextItem = this.GetNextItem(start, forward ? ArrowDirection.Right : ArrowDirection.Left);
			if (nextItem == null)
			{
				return nextItem;
			}
			this.ChangeSelection(nextItem);
			if (nextItem is ToolStripControlHost)
			{
				(nextItem as ToolStripControlHost).Focus();
			}
			return nextItem;
		}

		// Token: 0x0600130B RID: 4875 RVA: 0x0006192D File Offset: 0x0005FB2D
		private void MouseEnteredItem(ToolStripItem item)
		{
			if (this.show_item_tool_tips && !(item is ToolStripTextBox))
			{
				this.ToolTipTimer.Interval = 500;
				this.tooltip_state = ToolTip.TipState.Initial;
				this.tooltip_currently_showing = item;
				this.ToolTipTimer.Start();
			}
		}

		// Token: 0x0600130C RID: 4876 RVA: 0x00061968 File Offset: 0x0005FB68
		private void CloseToolTip(ToolStripItem item)
		{
			this.ToolTipTimer.Stop();
			this.ToolTipWindow.Hide(this);
			this.tooltip_currently_showing = null;
			this.tooltip_state = ToolTip.TipState.Down;
		}

		// Token: 0x0600130D RID: 4877 RVA: 0x0006198F File Offset: 0x0005FB8F
		private void MouseLeftItem(ToolStripItem item)
		{
			this.CloseToolTip(item);
		}

		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x0600130E RID: 4878 RVA: 0x00061998 File Offset: 0x0005FB98
		private Timer ToolTipTimer
		{
			get
			{
				if (this.tooltip_timer == null)
				{
					this.tooltip_timer = new Timer();
					this.tooltip_timer.Enabled = false;
					this.tooltip_timer.Interval = 500;
					this.tooltip_timer.Tick += this.ToolTipTimer_Tick;
				}
				return this.tooltip_timer;
			}
		}

		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x0600130F RID: 4879 RVA: 0x000619F1 File Offset: 0x0005FBF1
		private ToolTip ToolTipWindow
		{
			get
			{
				if (this.tooltip_window == null)
				{
					this.tooltip_window = new ToolTip();
				}
				return this.tooltip_window;
			}
		}

		// Token: 0x06001310 RID: 4880 RVA: 0x00061A0C File Offset: 0x0005FC0C
		private void ShowToolTip()
		{
			string toolTip = this.tooltip_currently_showing.GetToolTip();
			if (!string.IsNullOrEmpty(toolTip))
			{
				this.ToolTipWindow.Present(this, toolTip);
				this.ToolTipTimer.Interval = 5000;
				this.ToolTipTimer.Start();
				this.tooltip_state = ToolTip.TipState.Show;
			}
			this.tooltip_currently_showing.FireEvent(EventArgs.Empty, ToolStripItemEventType.MouseHover);
		}

		// Token: 0x06001311 RID: 4881 RVA: 0x00061A70 File Offset: 0x0005FC70
		private void ToolTipTimer_Tick(object o, EventArgs args)
		{
			this.ToolTipTimer.Stop();
			ToolTip.TipState tipState = this.tooltip_state;
			if (tipState == ToolTip.TipState.Initial)
			{
				this.ShowToolTip();
				return;
			}
			if (tipState != ToolTip.TipState.Show)
			{
				return;
			}
			this.CloseToolTip(null);
		}

		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x06001312 RID: 4882 RVA: 0x00061AA5 File Offset: 0x0005FCA5
		// (set) Token: 0x06001313 RID: 4883 RVA: 0x00061AAD File Offset: 0x0005FCAD
		internal ToolStrip CurrentlyMergedWith
		{
			get
			{
				return this.currently_merged_with;
			}
			set
			{
				this.currently_merged_with = value;
			}
		}

		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x06001314 RID: 4884 RVA: 0x00061AB6 File Offset: 0x0005FCB6
		internal List<ToolStripItem> HiddenMergedItems
		{
			get
			{
				if (this.hidden_merged_items == null)
				{
					this.hidden_merged_items = new List<ToolStripItem>();
				}
				return this.hidden_merged_items;
			}
		}

		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x06001315 RID: 4885 RVA: 0x00061AD1 File Offset: 0x0005FCD1
		// (set) Token: 0x06001316 RID: 4886 RVA: 0x00061ADC File Offset: 0x0005FCDC
		internal bool IsCurrentlyMerged
		{
			get
			{
				return this.is_currently_merged;
			}
			set
			{
				this.is_currently_merged = value;
				if (!value && this is MenuStrip)
				{
					foreach (object obj in this.Items)
					{
						((ToolStripMenuItem)obj).DropDown.IsCurrentlyMerged = value;
					}
				}
			}
		}

		// Token: 0x06001317 RID: 4887 RVA: 0x00061B4C File Offset: 0x0005FD4C
		internal void BeginMerge()
		{
			if (!this.IsCurrentlyMerged)
			{
				this.IsCurrentlyMerged = true;
				if (this.pre_merge_items == null)
				{
					this.pre_merge_items = new List<ToolStripItem>();
					foreach (object obj in this.Items)
					{
						ToolStripItem toolStripItem = (ToolStripItem)obj;
						this.pre_merge_items.Add(toolStripItem);
					}
				}
			}
		}

		// Token: 0x06001318 RID: 4888 RVA: 0x00061BCC File Offset: 0x0005FDCC
		internal void RevertMergeItem(ToolStripItem item)
		{
			if (item.Parent != null && item.Parent != this)
			{
				if (item.Parent is ToolStripOverflow)
				{
					(item.Parent as ToolStripOverflow).ParentToolStrip.Items.RemoveNoOwnerOrLayout(item);
				}
				else
				{
					item.Parent.Items.RemoveNoOwnerOrLayout(item);
				}
				item.Parent = item.Owner;
			}
			for (int i = item.Owner.pre_merge_items.IndexOf(item); i < this.pre_merge_items.Count; i++)
			{
				if (this.Items.Contains(this.pre_merge_items[i]))
				{
					item.Owner.Items.InsertNoOwnerOrLayout(this.Items.IndexOf(this.pre_merge_items[i]), item);
					return;
				}
			}
			item.Owner.Items.AddNoOwnerOrLayout(item);
		}

		// Token: 0x06001319 RID: 4889 RVA: 0x00061CAC File Offset: 0x0005FEAC
		// Note: this type is marked as 'beforefieldinit'.
		static ToolStrip()
		{
			ToolStrip.ItemAddedEvent = new object();
			ToolStrip.ItemClickedEvent = new object();
			ToolStrip.ItemRemovedEvent = new object();
			ToolStrip.LayoutCompletedEvent = new object();
			ToolStrip.LayoutStyleChangedEvent = new object();
			ToolStrip.PaintGripEvent = new object();
			ToolStrip.RendererChangedEvent = new object();
		}

		// Token: 0x04000B8C RID: 2956
		private bool allow_merge;

		// Token: 0x04000B8D RID: 2957
		private Color back_color;

		// Token: 0x04000B8E RID: 2958
		private bool can_overflow;

		// Token: 0x04000B8F RID: 2959
		private ToolStrip currently_merged_with;

		// Token: 0x04000B90 RID: 2960
		private ToolStripDropDownDirection default_drop_down_direction;

		// Token: 0x04000B91 RID: 2961
		internal ToolStripItemCollection displayed_items;

		// Token: 0x04000B92 RID: 2962
		private Color fore_color;

		// Token: 0x04000B93 RID: 2963
		private Padding grip_margin;

		// Token: 0x04000B94 RID: 2964
		private ToolStripGripStyle grip_style;

		// Token: 0x04000B95 RID: 2965
		private List<ToolStripItem> hidden_merged_items;

		// Token: 0x04000B96 RID: 2966
		private ImageList image_list;

		// Token: 0x04000B97 RID: 2967
		private Size image_scaling_size;

		// Token: 0x04000B98 RID: 2968
		private bool is_currently_merged;

		// Token: 0x04000B99 RID: 2969
		private ToolStripItemCollection items;

		// Token: 0x04000B9A RID: 2970
		private bool keyboard_active;

		// Token: 0x04000B9B RID: 2971
		private LayoutEngine layout_engine;

		// Token: 0x04000B9C RID: 2972
		private LayoutSettings layout_settings;

		// Token: 0x04000B9D RID: 2973
		private ToolStripLayoutStyle layout_style;

		// Token: 0x04000B9E RID: 2974
		private Orientation orientation;

		// Token: 0x04000B9F RID: 2975
		private ToolStripOverflowButton overflow_button;

		// Token: 0x04000BA0 RID: 2976
		private List<ToolStripItem> pre_merge_items;

		// Token: 0x04000BA1 RID: 2977
		private ToolStripRenderer renderer;

		// Token: 0x04000BA2 RID: 2978
		private ToolStripRenderMode render_mode;

		// Token: 0x04000BA3 RID: 2979
		private ToolStripTextDirection text_direction;

		// Token: 0x04000BA4 RID: 2980
		private Timer tooltip_timer;

		// Token: 0x04000BA5 RID: 2981
		private ToolTip tooltip_window;

		// Token: 0x04000BA6 RID: 2982
		private bool show_item_tool_tips;

		// Token: 0x04000BA7 RID: 2983
		private ToolStripItem mouse_currently_over;

		// Token: 0x04000BA8 RID: 2984
		internal bool menu_selected;

		// Token: 0x04000BA9 RID: 2985
		private ToolStripItem tooltip_currently_showing;

		// Token: 0x04000BAA RID: 2986
		private ToolTip.TipState tooltip_state;

		// Token: 0x04000BAB RID: 2987
		private static object BeginDragEvent = new object();

		// Token: 0x04000BAC RID: 2988
		private static object EndDragEvent = new object();

		// Token: 0x04000BAE RID: 2990
		private static object ItemClickedEvent;

		// Token: 0x04000BAF RID: 2991
		private static object ItemRemovedEvent;

		// Token: 0x04000BB0 RID: 2992
		private static object LayoutCompletedEvent;

		// Token: 0x04000BB1 RID: 2993
		private static object LayoutStyleChangedEvent;

		// Token: 0x04000BB2 RID: 2994
		private static object PaintGripEvent;

		// Token: 0x04000BB3 RID: 2995
		private static object RendererChangedEvent;
	}
}
