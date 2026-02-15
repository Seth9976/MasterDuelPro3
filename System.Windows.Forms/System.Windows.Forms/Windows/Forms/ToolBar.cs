using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Represents a Windows toolbar. Although <see cref="T:System.Windows.Forms.ToolStrip" /> replaces and adds functionality to the <see cref="T:System.Windows.Forms.ToolBar" /> control of previous versions, <see cref="T:System.Windows.Forms.ToolBar" /> is retained for both backward compatibility and future use if you choose.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020001AE RID: 430
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[DefaultEvent("ButtonClick")]
	[DefaultProperty("Buttons")]
	[Designer("System.Windows.Forms.Design.ToolBarDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	public class ToolBar : Control
	{
		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.ToolBar.BackgroundImage" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000044 RID: 68
		// (add) Token: 0x06001203 RID: 4611 RVA: 0x0005D4AD File Offset: 0x0005B6AD
		// (remove) Token: 0x06001204 RID: 4612 RVA: 0x0005D4B6 File Offset: 0x0005B6B6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler BackgroundImageChanged
		{
			add
			{
				base.BackgroundImageChanged += value;
			}
			remove
			{
				base.BackgroundImageChanged -= value;
			}
		}

		/// <summary>Occurs when a <see cref="T:System.Windows.Forms.ToolBarButton" /> on the <see cref="T:System.Windows.Forms.ToolBar" /> is clicked.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000045 RID: 69
		// (add) Token: 0x06001205 RID: 4613 RVA: 0x0005D4BF File Offset: 0x0005B6BF
		// (remove) Token: 0x06001206 RID: 4614 RVA: 0x0005D4D2 File Offset: 0x0005B6D2
		public event ToolBarButtonClickEventHandler ButtonClick
		{
			add
			{
				base.Events.AddHandler(ToolBar.ButtonClickEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolBar.ButtonClickEvent, value);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolBar" /> class.</summary>
		// Token: 0x06001207 RID: 4615 RVA: 0x0005D4E8 File Offset: 0x0005B6E8
		public ToolBar()
		{
			this.background_color = ThemeEngine.Current.DefaultControlBackColor;
			this.foreground_color = ThemeEngine.Current.DefaultControlForeColor;
			this.buttons = new ToolBar.ToolBarButtonCollection(this);
			this.Dock = DockStyle.Top;
			base.GotFocus += this.FocusChanged;
			base.LostFocus += this.FocusChanged;
			base.MouseDown += this.ToolBar_MouseDown;
			base.MouseHover += this.ToolBar_MouseHover;
			base.MouseLeave += this.ToolBar_MouseLeave;
			base.MouseMove += this.ToolBar_MouseMove;
			base.MouseUp += this.ToolBar_MouseUp;
			this.BackgroundImageChanged += this.ToolBar_BackgroundImageChanged;
			this.TabStop = false;
			base.SetStyle(ControlStyles.UserPaint, false);
			base.SetStyle(ControlStyles.FixedHeight, true);
			base.SetStyle(ControlStyles.FixedWidth, false);
		}

		/// <summary>Gets the required creation parameters when the control handle is created.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.CreateParams" /> that contains the required creation parameters when the handle to the control is created.</returns>
		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x06001208 RID: 4616 RVA: 0x0005D610 File Offset: 0x0005B810
		protected override CreateParams CreateParams
		{
			get
			{
				CreateParams createParams = base.CreateParams;
				if (this.appearance == ToolBarAppearance.Flat)
				{
					createParams.Style |= 2048;
				}
				return createParams;
			}
		}

		/// <summary>Gets the default size of the control.</summary>
		/// <returns>The default <see cref="T:System.Drawing.Size" /> of the control.</returns>
		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x06001209 RID: 4617 RVA: 0x0005D640 File Offset: 0x0005B840
		protected override Size DefaultSize
		{
			get
			{
				return ThemeEngine.Current.ToolBarDefaultSize;
			}
		}

		/// <summary>This member is not meaningful for this control.</summary>
		/// <returns>A <see cref="T:System.Boolean" /> value.</returns>
		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x0600120A RID: 4618 RVA: 0x00027890 File Offset: 0x00025A90
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected override bool DoubleBuffered
		{
			get
			{
				return base.DoubleBuffered;
			}
		}

		/// <summary>Gets or set the value that determines the appearance of a toolbar control and its buttons.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ToolBarAppearance" /> values. The default is ToolBarAppearance.Normal.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The assigned value is not one of the <see cref="T:System.Windows.Forms.ToolBarAppearance" /> values. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x0600120B RID: 4619 RVA: 0x0005D64C File Offset: 0x0005B84C
		// (set) Token: 0x0600120C RID: 4620 RVA: 0x0005D654 File Offset: 0x0005B854
		[DefaultValue(ToolBarAppearance.Normal)]
		[Localizable(true)]
		public ToolBarAppearance Appearance
		{
			get
			{
				return this.appearance;
			}
			set
			{
				if (value == this.appearance)
				{
					return;
				}
				this.appearance = value;
				this.Redraw(true);
			}
		}

		/// <summary>Gets or sets a value indicating whether the toolbar adjusts its size automatically, based on the size of the buttons and the dock style.</summary>
		/// <returns>true if the toolbar adjusts its size automatically, based on the size of the buttons and dock style; otherwise, false. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x0600120D RID: 4621 RVA: 0x0005D66E File Offset: 0x0005B86E
		// (set) Token: 0x0600120E RID: 4622 RVA: 0x0005D676 File Offset: 0x0005B876
		[Browsable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		[DefaultValue(true)]
		[Localizable(true)]
		public override bool AutoSize
		{
			get
			{
				return this.autosize;
			}
			set
			{
				if (value == this.autosize)
				{
					return;
				}
				this.autosize = value;
				if (base.IsHandleCreated)
				{
					this.Redraw(true);
				}
			}
		}

		/// <summary>Gets or sets the background color.</summary>
		/// <returns>The background color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x0600120F RID: 4623 RVA: 0x0005D698 File Offset: 0x0005B898
		// (set) Token: 0x06001210 RID: 4624 RVA: 0x0005D6A0 File Offset: 0x0005B8A0
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override Color BackColor
		{
			get
			{
				return this.background_color;
			}
			set
			{
				if (value == this.background_color)
				{
					return;
				}
				this.background_color = value;
				this.OnBackColorChanged(EventArgs.Empty);
				this.Redraw(false);
			}
		}

		/// <summary>Gets or sets the background image.</summary>
		/// <returns>The background image.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x06001211 RID: 4625 RVA: 0x00005B72 File Offset: 0x00003D72
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override Image BackgroundImage
		{
			get
			{
				return base.BackgroundImage;
			}
		}

		/// <summary>Gets or sets the layout for background image.</summary>
		/// <returns>The layout for background image.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x06001212 RID: 4626 RVA: 0x00005B7A File Offset: 0x00003D7A
		// (set) Token: 0x06001213 RID: 4627 RVA: 0x00005B82 File Offset: 0x00003D82
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

		/// <summary>Gets the collection of <see cref="T:System.Windows.Forms.ToolBarButton" /> controls assigned to the toolbar control.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ToolBar.ToolBarButtonCollection" /> that contains a collection of <see cref="T:System.Windows.Forms.ToolBarButton" /> controls.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x06001214 RID: 4628 RVA: 0x0005D6CA File Offset: 0x0005B8CA
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Localizable(true)]
		[MergableProperty(false)]
		public ToolBar.ToolBarButtonCollection Buttons
		{
			get
			{
				return this.buttons;
			}
		}

		/// <summary>Gets or sets the size of the buttons on the toolbar control.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> object that represents the size of the <see cref="T:System.Windows.Forms.ToolBarButton" /> controls on the toolbar. The default size has a width of 24 pixels and a height of 22 pixels, or large enough to accommodate the <see cref="T:System.Drawing.Image" /> and text, whichever is greater.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Drawing.Size.Width" /> or <see cref="P:System.Drawing.Size.Height" /> property of the <see cref="T:System.Drawing.Size" /> object is less than 0. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x06001215 RID: 4629 RVA: 0x0005D6D4 File Offset: 0x0005B8D4
		// (set) Token: 0x06001216 RID: 4630 RVA: 0x0005D727 File Offset: 0x0005B927
		[Localizable(true)]
		[RefreshProperties(RefreshProperties.All)]
		public Size ButtonSize
		{
			get
			{
				if (!this.button_size.IsEmpty)
				{
					return this.button_size;
				}
				if (this.buttons.Count == 0)
				{
					return new Size(39, 36);
				}
				Size size = this.CalcButtonSize();
				if (size.IsEmpty)
				{
					return new Size(24, 22);
				}
				return size;
			}
			set
			{
				this.size_specified = value != Size.Empty;
				if (this.button_size == value)
				{
					return;
				}
				this.button_size = value;
				this.Redraw(true);
			}
		}

		/// <summary>Gets or sets a value indicating whether the toolbar displays a divider.</summary>
		/// <returns>true if the toolbar displays a divider; otherwise, false. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x06001217 RID: 4631 RVA: 0x0005D757 File Offset: 0x0005B957
		// (set) Token: 0x06001218 RID: 4632 RVA: 0x0005D75F File Offset: 0x0005B95F
		[DefaultValue(true)]
		public bool Divider
		{
			get
			{
				return this.divider;
			}
			set
			{
				if (value == this.divider)
				{
					return;
				}
				this.divider = value;
				this.Redraw(false);
			}
		}

		/// <summary>Gets or sets which control borders are docked to its parent control and determines how a control is resized with its parent.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DockStyle" /> values. The default is <see cref="F:System.Windows.Forms.DockStyle.None" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x06001219 RID: 4633 RVA: 0x0005D779 File Offset: 0x0005B979
		// (set) Token: 0x0600121A RID: 4634 RVA: 0x0005D784 File Offset: 0x0005B984
		[DefaultValue(DockStyle.Top)]
		[Localizable(true)]
		public override DockStyle Dock
		{
			get
			{
				return base.Dock;
			}
			set
			{
				if (base.Dock == value)
				{
					if (value != DockStyle.None)
					{
						base.Dock = value;
					}
					return;
				}
				if (this.Vertical)
				{
					base.SetStyle(ControlStyles.FixedWidth, this.AutoSize);
					base.SetStyle(ControlStyles.FixedHeight, false);
				}
				else
				{
					base.SetStyle(ControlStyles.FixedHeight, this.AutoSize);
					base.SetStyle(ControlStyles.FixedWidth, false);
				}
				this.LayoutToolBar();
				base.Dock = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether drop-down buttons on a toolbar display down arrows.</summary>
		/// <returns>true if drop-down toolbar buttons display down arrows; otherwise, false. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x0600121B RID: 4635 RVA: 0x0005D7EB File Offset: 0x0005B9EB
		// (set) Token: 0x0600121C RID: 4636 RVA: 0x0005D7F3 File Offset: 0x0005B9F3
		[DefaultValue(false)]
		[Localizable(true)]
		public bool DropDownArrows
		{
			get
			{
				return this.drop_down_arrows;
			}
			set
			{
				if (value == this.drop_down_arrows)
				{
					return;
				}
				this.drop_down_arrows = value;
				this.Redraw(true);
			}
		}

		/// <summary>Gets or sets the forecolor .</summary>
		/// <returns>The forecolor.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x0600121D RID: 4637 RVA: 0x0005D80D File Offset: 0x0005BA0D
		// (set) Token: 0x0600121E RID: 4638 RVA: 0x0005D815 File Offset: 0x0005BA15
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override Color ForeColor
		{
			get
			{
				return this.foreground_color;
			}
			set
			{
				if (value == this.foreground_color)
				{
					return;
				}
				this.foreground_color = value;
				this.OnForeColorChanged(EventArgs.Empty);
				this.Redraw(false);
			}
		}

		/// <summary>Gets or sets the collection of images available to the toolbar button controls.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.ImageList" /> that contains images available to the <see cref="T:System.Windows.Forms.ToolBarButton" /> controls. The default is null.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x0600121F RID: 4639 RVA: 0x0005D83F File Offset: 0x0005BA3F
		// (set) Token: 0x06001220 RID: 4640 RVA: 0x0005D847 File Offset: 0x0005BA47
		[DefaultValue(null)]
		public ImageList ImageList
		{
			get
			{
				return this.image_list;
			}
			set
			{
				if (this.image_list == value)
				{
					return;
				}
				this.image_list = value;
				this.Redraw(true);
			}
		}

		/// <summary>Gets the size of the images in the image list assigned to the toolbar.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that represents the size of the images (in the <see cref="T:System.Windows.Forms.ImageList" />) assigned to the <see cref="T:System.Windows.Forms.ToolBar" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x06001221 RID: 4641 RVA: 0x0005D861 File Offset: 0x0005BA61
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Size ImageSize
		{
			get
			{
				if (this.ImageList == null)
				{
					return Size.Empty;
				}
				return this.ImageList.ImageSize;
			}
		}

		/// <summary>This member is not meaningful for this control.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.RightToLeft" /> value.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x06001222 RID: 4642 RVA: 0x0003B96F File Offset: 0x00039B6F
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override RightToLeft RightToLeft
		{
			get
			{
				return base.RightToLeft;
			}
		}

		/// <summary>Gets or sets a value indicating whether the toolbar displays a ToolTip for each button.</summary>
		/// <returns>true if the toolbar display a ToolTip for each button; otherwise, false. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170004A6 RID: 1190
		// (set) Token: 0x06001223 RID: 4643 RVA: 0x0005D87C File Offset: 0x0005BA7C
		[DefaultValue(false)]
		[Localizable(true)]
		public bool ShowToolTips
		{
			set
			{
				this.show_tooltips = value;
			}
		}

		/// <summary>This property is not meaningful for this control.</summary>
		/// <returns>This property is not meaningful for this control.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170004A7 RID: 1191
		// (set) Token: 0x06001224 RID: 4644 RVA: 0x000208ED File Offset: 0x0001EAED
		[DefaultValue(false)]
		public new bool TabStop
		{
			set
			{
				base.TabStop = value;
			}
		}

		/// <summary>Gets or sets the text for the toolbar.</summary>
		/// <returns>The text for the toolbar.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x06001225 RID: 4645 RVA: 0x000043B4 File Offset: 0x000025B4
		// (set) Token: 0x06001226 RID: 4646 RVA: 0x0005D885 File Offset: 0x0005BA85
		[Bindable(false)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string Text
		{
			get
			{
				return base.Text;
			}
			set
			{
				if (value == base.Text)
				{
					return;
				}
				base.Text = value;
				this.Redraw(true);
			}
		}

		/// <summary>Gets or sets the alignment of text in relation to each image displayed on the toolbar button controls.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ToolBarTextAlign" /> values. The default is ToolBarTextAlign.Underneath.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The assigned value is not one of the <see cref="T:System.Windows.Forms.ToolBarTextAlign" /> values. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x06001227 RID: 4647 RVA: 0x0005D8A4 File Offset: 0x0005BAA4
		// (set) Token: 0x06001228 RID: 4648 RVA: 0x0005D8AC File Offset: 0x0005BAAC
		[DefaultValue(ToolBarTextAlign.Underneath)]
		[Localizable(true)]
		public ToolBarTextAlign TextAlign
		{
			get
			{
				return this.text_alignment;
			}
			set
			{
				if (value == this.text_alignment)
				{
					return;
				}
				this.text_alignment = value;
				this.Redraw(true);
			}
		}

		/// <summary>Gets or sets a value indicating whether the toolbar buttons wrap to the next line if the toolbar becomes too small to display all the buttons on the same line.</summary>
		/// <returns>true if the toolbar buttons wrap to another line if the toolbar becomes too small to display all the buttons on the same line; otherwise, false. The default value is true.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x06001229 RID: 4649 RVA: 0x0005D8C6 File Offset: 0x0005BAC6
		[DefaultValue(true)]
		[Localizable(true)]
		public bool Wrappable
		{
			get
			{
				return this.wrappable;
			}
		}

		/// <summary>Returns a string that represents the <see cref="T:System.Windows.Forms.ToolBar" /> control.</summary>
		/// <returns>A String that represents the current <see cref="T:System.Windows.Forms.ToolBar" />. </returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600122A RID: 4650 RVA: 0x0005D8D0 File Offset: 0x0005BAD0
		public override string ToString()
		{
			int count = this.Buttons.Count;
			if (count == 0)
			{
				return string.Format("System.Windows.Forms.ToolBar, Buttons.Count: 0", Array.Empty<object>());
			}
			return string.Format("System.Windows.Forms.ToolBar, Buttons.Count: {0}, Buttons[0]: {1}", count, this.Buttons[0].ToString());
		}

		/// <summary>Creates a handle for the control.</summary>
		// Token: 0x0600122B RID: 4651 RVA: 0x0005D91D File Offset: 0x0005BB1D
		protected override void CreateHandle()
		{
			base.CreateHandle();
			this.default_size = this.CalcButtonSize();
			if (this.appearance != ToolBarAppearance.Flat)
			{
				this.Redraw(true);
			}
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.ToolBar" /> and optionally releases the managed resources. </summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x0600122C RID: 4652 RVA: 0x0005D941 File Offset: 0x0005BB41
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.ImageList = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600122D RID: 4653 RVA: 0x0005D954 File Offset: 0x0005BB54
		private void PerformButtonClick(ToolBarButtonClickEventArgs e)
		{
			if (e.Button.Style == ToolBarButtonStyle.ToggleButton)
			{
				if (!e.Button.Pushed)
				{
					e.Button.Pushed = true;
				}
				else
				{
					e.Button.Pushed = false;
				}
			}
			this.current_item.Pressed = false;
			this.current_item.Invalidate();
			this.button_for_focus = this.current_item.Button;
			this.button_for_focus.UIAHasFocus = true;
			this.OnButtonClick(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolBar.ButtonClick" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolBarButtonClickEventArgs" /> that contains the event data. </param>
		// Token: 0x0600122E RID: 4654 RVA: 0x0005D9D4 File Offset: 0x0005BBD4
		protected virtual void OnButtonClick(ToolBarButtonClickEventArgs e)
		{
			ToolBarButtonClickEventHandler toolBarButtonClickEventHandler = (ToolBarButtonClickEventHandler)base.Events[ToolBar.ButtonClickEvent];
			if (toolBarButtonClickEventHandler != null)
			{
				toolBarButtonClickEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolBar.ButtonDropDown" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolBarButtonClickEventArgs" /> that contains the event data. </param>
		// Token: 0x0600122F RID: 4655 RVA: 0x0005DA04 File Offset: 0x0005BC04
		protected virtual void OnButtonDropDown(ToolBarButtonClickEventArgs e)
		{
			ToolBarButtonClickEventHandler toolBarButtonClickEventHandler = (ToolBarButtonClickEventHandler)base.Events[ToolBar.ButtonDropDownEvent];
			if (toolBarButtonClickEventHandler != null)
			{
				toolBarButtonClickEventHandler(this, e);
			}
			if (e.Button.DropDownMenu == null)
			{
				return;
			}
			this.ShowDropDownMenu(this.current_item);
		}

		// Token: 0x06001230 RID: 4656 RVA: 0x0005DA4C File Offset: 0x0005BC4C
		internal void ShowDropDownMenu(ToolBarItem item)
		{
			Point point = new Point(item.Rectangle.X + 1, item.Rectangle.Bottom + 1);
			((ContextMenu)item.Button.DropDownMenu).Show(this, point);
			item.DDPressed = false;
			item.Hilight = false;
			item.Invalidate();
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.FontChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06001231 RID: 4657 RVA: 0x0005DAAB File Offset: 0x0005BCAB
		protected override void OnFontChanged(EventArgs e)
		{
			base.OnFontChanged(e);
			this.Redraw(true);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.HandleCreated" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06001232 RID: 4658 RVA: 0x00004D0D File Offset: 0x00002F0D
		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Resize" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06001233 RID: 4659 RVA: 0x0005DABB File Offset: 0x0005BCBB
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			this.LayoutToolBar();
		}

		/// <summary>Scales a control's location, size, padding and margin.</summary>
		/// <param name="factor">The factor by which the height and width of the control will be scaled.</param>
		/// <param name="specified">A <see cref="T:System.Windows.Forms.BoundsSpecified" /> value that specifies the bounds of the control to use when defining its size and position.</param>
		// Token: 0x06001234 RID: 4660 RVA: 0x0005DACB File Offset: 0x0005BCCB
		protected override void ScaleControl(SizeF factor, BoundsSpecified specified)
		{
			specified &= ~BoundsSpecified.Height;
			base.ScaleControl(factor, specified);
		}

		/// <summary>This method is not relevant for this class.</summary>
		/// <param name="dx">The horizontal scaling factor.</param>
		/// <param name="dy">The vertical scaling factor.</param>
		// Token: 0x06001235 RID: 4661 RVA: 0x0005DADB File Offset: 0x0005BCDB
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected override void ScaleCore(float dx, float dy)
		{
			dy = 1f;
			base.ScaleCore(dx, dy);
		}

		/// <summary>Sets the specified bounds of the <see cref="T:System.Windows.Forms.ToolBar" /> control.</summary>
		/// <param name="x">The new Left property value of the control.</param>
		/// <param name="y">The new Top property value of the control.</param>
		/// <param name="width">The new Width property value of the control.</param>
		/// <param name="height">Not used.</param>
		/// <param name="specified">A bitwise combination of the <see cref="T:System.Windows.Forms.BoundsSpecified" /> values.</param>
		// Token: 0x06001236 RID: 4662 RVA: 0x0005DAEC File Offset: 0x0005BCEC
		protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
		{
			if (this.Vertical)
			{
				if (!this.AutoSize && this.requested_size != width && (specified & BoundsSpecified.Width) != BoundsSpecified.None)
				{
					this.requested_size = width;
				}
			}
			else if (!this.AutoSize && this.requested_size != height && (specified & BoundsSpecified.Height) != BoundsSpecified.None)
			{
				this.requested_size = height;
			}
			base.SetBoundsCore(x, y, width, height, specified);
		}

		/// <summary>Processes Windows messages.</summary>
		/// <param name="m">The Windows <see cref="T:System.Windows.Forms.Message" /> to process.</param>
		// Token: 0x06001237 RID: 4663 RVA: 0x00020975 File Offset: 0x0001EB75
		protected override void WndProc(ref Message m)
		{
			base.WndProc(ref m);
		}

		// Token: 0x06001238 RID: 4664 RVA: 0x0005DB50 File Offset: 0x0005BD50
		internal override bool InternalPreProcessMessage(ref Message msg)
		{
			if (msg.Msg == 256)
			{
				Keys keys = (Keys)msg.WParam.ToInt32();
				if (this.HandleKeyDown(ref msg, keys))
				{
					return true;
				}
			}
			return base.InternalPreProcessMessage(ref msg);
		}

		// Token: 0x06001239 RID: 4665 RVA: 0x0005DB8C File Offset: 0x0005BD8C
		private void FocusChanged(object sender, EventArgs args)
		{
			if (!this.Focused && this.button_for_focus != null)
			{
				this.button_for_focus.UIAHasFocus = false;
			}
			this.button_for_focus = null;
			if (this.Appearance != ToolBarAppearance.Flat || this.Buttons.Count == 0)
			{
				return;
			}
			ToolBarItem toolBarItem = null;
			foreach (ToolBarItem toolBarItem2 in this.items)
			{
				if (toolBarItem2.Hilight)
				{
					toolBarItem = toolBarItem2;
					break;
				}
			}
			if (this.Focused && toolBarItem == null)
			{
				foreach (ToolBarItem toolBarItem3 in this.items)
				{
					if (toolBarItem3.Button.Enabled)
					{
						toolBarItem3.Hilight = true;
						return;
					}
				}
				return;
			}
			if (toolBarItem != null)
			{
				toolBarItem.Hilight = false;
			}
		}

		// Token: 0x0600123A RID: 4666 RVA: 0x0005DC44 File Offset: 0x0005BE44
		private bool HandleKeyDown(ref Message msg, Keys key_data)
		{
			if (this.Appearance != ToolBarAppearance.Flat || this.Buttons.Count == 0)
			{
				return false;
			}
			if (this.HandleKeyOnDropDown(ref msg, key_data))
			{
				return true;
			}
			if (key_data != Keys.Return)
			{
				switch (key_data)
				{
				case Keys.Space:
					break;
				case Keys.PageUp:
				case Keys.PageDown:
				case Keys.End:
				case Keys.Home:
					return false;
				case Keys.Left:
				case Keys.Up:
					this.HighlightButton(-1);
					return true;
				case Keys.Right:
				case Keys.Down:
					this.HighlightButton(1);
					return true;
				default:
					return false;
				}
			}
			if (this.current_item != null)
			{
				this.OnButtonClick(new ToolBarButtonClickEventArgs(this.current_item.Button));
				return true;
			}
			return false;
		}

		// Token: 0x0600123B RID: 4667 RVA: 0x0005DCDC File Offset: 0x0005BEDC
		private bool HandleKeyOnDropDown(ref Message msg, Keys key_data)
		{
			if (this.current_item == null || this.current_item.Button.Style != ToolBarButtonStyle.DropDownButton || this.current_item.Button.DropDownMenu == null)
			{
				return false;
			}
			Menu dropDownMenu = this.current_item.Button.DropDownMenu;
			if (dropDownMenu.Tracker.active)
			{
				dropDownMenu.ProcessCmdKey(ref msg, key_data);
				return true;
			}
			if (key_data == Keys.Up || key_data == Keys.Down)
			{
				this.current_item.DDPressed = true;
				this.current_item.Invalidate();
				this.OnButtonDropDown(new ToolBarButtonClickEventArgs(this.current_item.Button));
				return true;
			}
			return false;
		}

		// Token: 0x0600123C RID: 4668 RVA: 0x0005DD7C File Offset: 0x0005BF7C
		private void HighlightButton(int offset)
		{
			ArrayList arrayList = new ArrayList();
			int num = 0;
			int num2 = -1;
			ToolBarItem toolBarItem = null;
			foreach (ToolBarItem toolBarItem2 in this.items)
			{
				if (toolBarItem2.Hilight)
				{
					num2 = num;
					toolBarItem = toolBarItem2;
				}
				if (toolBarItem2.Button.Enabled)
				{
					arrayList.Add(toolBarItem2);
					num++;
				}
			}
			int num3 = (num2 + offset) % num;
			if (num3 < 0)
			{
				num3 = num - 1;
			}
			if (num3 == num2)
			{
				return;
			}
			if (toolBarItem != null)
			{
				toolBarItem.Hilight = false;
			}
			this.current_item = arrayList[num3] as ToolBarItem;
			this.current_item.Hilight = true;
		}

		// Token: 0x0600123D RID: 4669 RVA: 0x0005DE20 File Offset: 0x0005C020
		private void ToolBar_BackgroundImageChanged(object sender, EventArgs args)
		{
			this.Redraw(false, true);
		}

		// Token: 0x0600123E RID: 4670 RVA: 0x0005DE2C File Offset: 0x0005C02C
		private void ToolBar_MouseDown(object sender, MouseEventArgs me)
		{
			if (!base.Enabled || (me.Button & MouseButtons.Left) == MouseButtons.None)
			{
				return;
			}
			Point point = new Point(me.X, me.Y);
			if (this.ItemAtPoint(point) == null)
			{
				return;
			}
			if (this.tip_window != null && this.tip_window.Visible && (me.Button & MouseButtons.Left) == MouseButtons.Left)
			{
				this.TipDownTimer.Stop();
				this.tip_window.Hide(this);
			}
			foreach (ToolBarItem toolBarItem in this.items)
			{
				if (toolBarItem.Button.Enabled && toolBarItem.Rectangle.Contains(point))
				{
					if (toolBarItem.Button.Style == ToolBarButtonStyle.DropDownButton)
					{
						Rectangle rectangle = toolBarItem.Rectangle;
						if (this.DropDownArrows)
						{
							rectangle.Width = ThemeEngine.Current.ToolBarDropDownWidth;
							rectangle.X = toolBarItem.Rectangle.Right - rectangle.Width;
						}
						if (rectangle.Contains(point))
						{
							if (toolBarItem.Button.DropDownMenu != null)
							{
								toolBarItem.DDPressed = true;
								base.Invalidate(rectangle);
								return;
							}
							break;
						}
					}
					toolBarItem.Pressed = true;
					toolBarItem.Inside = true;
					toolBarItem.Invalidate();
					return;
				}
			}
		}

		// Token: 0x0600123F RID: 4671 RVA: 0x0005DF7C File Offset: 0x0005C17C
		private void ToolBar_MouseUp(object sender, MouseEventArgs me)
		{
			if (!base.Enabled || (me.Button & MouseButtons.Left) == MouseButtons.None)
			{
				return;
			}
			Point point = new Point(me.X, me.Y);
			foreach (object obj in new ArrayList(this.items))
			{
				ToolBarItem toolBarItem = (ToolBarItem)obj;
				if (toolBarItem.Button.Enabled && toolBarItem.Rectangle.Contains(point))
				{
					if (toolBarItem.Button.Style == ToolBarButtonStyle.DropDownButton)
					{
						Rectangle rectangle = toolBarItem.Rectangle;
						rectangle.Width = ThemeEngine.Current.ToolBarDropDownWidth;
						rectangle.X = toolBarItem.Rectangle.Right - rectangle.Width;
						if (rectangle.Contains(point))
						{
							this.current_item = toolBarItem;
							if (toolBarItem.DDPressed)
							{
								this.OnButtonDropDown(new ToolBarButtonClickEventArgs(toolBarItem.Button));
								continue;
							}
							continue;
						}
					}
					this.current_item = toolBarItem;
					if (toolBarItem.Pressed && (me.Button & MouseButtons.Left) == MouseButtons.Left)
					{
						this.PerformButtonClick(new ToolBarButtonClickEventArgs(toolBarItem.Button));
					}
				}
				else if (toolBarItem.Pressed)
				{
					toolBarItem.Pressed = false;
					toolBarItem.Invalidate();
				}
			}
		}

		// Token: 0x06001240 RID: 4672 RVA: 0x0005E0E4 File Offset: 0x0005C2E4
		private ToolBarItem ItemAtPoint(Point pt)
		{
			foreach (ToolBarItem toolBarItem in this.items)
			{
				if (toolBarItem.Rectangle.Contains(pt))
				{
					return toolBarItem;
				}
			}
			return null;
		}

		// Token: 0x06001241 RID: 4673 RVA: 0x0005E11E File Offset: 0x0005C31E
		private void PopDownTip(object o, EventArgs args)
		{
			this.tip_window.Hide(this);
		}

		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x06001242 RID: 4674 RVA: 0x0005E12C File Offset: 0x0005C32C
		private Timer TipDownTimer
		{
			get
			{
				if (this.tipdown_timer == null)
				{
					this.tipdown_timer = new Timer();
					this.tipdown_timer.Enabled = false;
					this.tipdown_timer.Interval = 5000;
					this.tipdown_timer.Tick += this.PopDownTip;
				}
				return this.tipdown_timer;
			}
		}

		// Token: 0x06001243 RID: 4675 RVA: 0x0005E188 File Offset: 0x0005C388
		private void ToolBar_MouseHover(object sender, EventArgs e)
		{
			if (base.Capture)
			{
				return;
			}
			if (this.tip_window == null)
			{
				this.tip_window = new ToolTip();
			}
			ToolBarItem toolBarItem = this.ItemAtPoint(base.PointToClient(Control.MousePosition));
			this.current_item = toolBarItem;
			if (toolBarItem == null || toolBarItem.Button.ToolTipText.Length == 0)
			{
				return;
			}
			this.tip_window.Present(this, toolBarItem.Button.ToolTipText);
			this.TipDownTimer.Start();
		}

		// Token: 0x06001244 RID: 4676 RVA: 0x0005E204 File Offset: 0x0005C404
		private void ToolBar_MouseLeave(object sender, EventArgs e)
		{
			if (this.tipdown_timer != null)
			{
				this.tipdown_timer.Dispose();
			}
			this.tipdown_timer = null;
			if (this.tip_window != null)
			{
				this.tip_window.Dispose();
			}
			this.tip_window = null;
			if (!base.Enabled || this.current_item == null)
			{
				return;
			}
			this.current_item.Hilight = false;
			this.current_item = null;
		}

		// Token: 0x06001245 RID: 4677 RVA: 0x0005E26C File Offset: 0x0005C46C
		private void ToolBar_MouseMove(object sender, MouseEventArgs me)
		{
			if (!base.Enabled)
			{
				return;
			}
			if (this.tip_window != null && this.tip_window.Visible)
			{
				this.TipDownTimer.Stop();
				this.TipDownTimer.Start();
			}
			Point point = new Point(me.X, me.Y);
			if (base.Capture)
			{
				foreach (ToolBarItem toolBarItem in this.items)
				{
					if (toolBarItem.Pressed && toolBarItem.Inside != toolBarItem.Rectangle.Contains(point))
					{
						toolBarItem.Inside = toolBarItem.Rectangle.Contains(point);
						toolBarItem.Hilight = false;
						return;
					}
				}
				return;
			}
			if (this.current_item != null && this.current_item.Rectangle.Contains(point))
			{
				if (ThemeEngine.Current.ToolBarHasHotElementStyles(this))
				{
					if (this.current_item.Hilight || (!ThemeEngine.Current.ToolBarHasHotCheckedElementStyles && this.current_item.Button.Pushed) || !this.current_item.Button.Enabled)
					{
						return;
					}
					this.current_item.Hilight = true;
					return;
				}
			}
			else
			{
				if (this.tip_window != null)
				{
					if (this.tip_window.Visible)
					{
						this.tip_window.Hide(this);
						this.TipDownTimer.Stop();
					}
					this.current_item = this.ItemAtPoint(point);
					if (this.current_item != null && this.current_item.Button.ToolTipText.Length > 0)
					{
						this.tip_window.Present(this, this.current_item.Button.ToolTipText);
						this.TipDownTimer.Start();
					}
				}
				if (ThemeEngine.Current.ToolBarHasHotElementStyles(this))
				{
					foreach (ToolBarItem toolBarItem2 in this.items)
					{
						if (toolBarItem2.Rectangle.Contains(point) && toolBarItem2.Button.Enabled)
						{
							this.current_item = toolBarItem2;
							if (!this.current_item.Hilight && (ThemeEngine.Current.ToolBarHasHotCheckedElementStyles || !this.current_item.Button.Pushed))
							{
								this.current_item.Hilight = true;
							}
						}
						else if (toolBarItem2.Hilight)
						{
							toolBarItem2.Hilight = false;
						}
					}
				}
			}
		}

		// Token: 0x06001246 RID: 4678 RVA: 0x0005E4BA File Offset: 0x0005C6BA
		internal override void OnPaintInternal(PaintEventArgs pevent)
		{
			if (base.GetStyle(ControlStyles.UserPaint))
			{
				return;
			}
			ThemeEngine.Current.DrawToolBar(pevent.Graphics, pevent.ClipRectangle, this);
			pevent.Handled = true;
		}

		// Token: 0x06001247 RID: 4679 RVA: 0x0005E4E4 File Offset: 0x0005C6E4
		internal void Redraw(bool recalculate)
		{
			this.Redraw(recalculate, true);
		}

		// Token: 0x06001248 RID: 4680 RVA: 0x0005E4F0 File Offset: 0x0005C6F0
		internal void Redraw(bool recalculate, bool force)
		{
			bool flag = true;
			if (recalculate)
			{
				flag = this.LayoutToolBar();
			}
			if (force || flag)
			{
				base.Invalidate();
			}
		}

		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x06001249 RID: 4681 RVA: 0x0005E514 File Offset: 0x0005C714
		internal bool SizeSpecified
		{
			get
			{
				return this.size_specified;
			}
		}

		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x0600124A RID: 4682 RVA: 0x0005E51C File Offset: 0x0005C71C
		internal bool Vertical
		{
			get
			{
				return this.Dock == DockStyle.Left || this.Dock == DockStyle.Right;
			}
		}

		// Token: 0x0600124B RID: 4683 RVA: 0x0005E534 File Offset: 0x0005C734
		private Size CalcButtonSize()
		{
			if (this.Buttons.Count == 0)
			{
				return Size.Empty;
			}
			string text = this.Buttons[0].Text;
			for (int i = 1; i < this.Buttons.Count; i++)
			{
				if (this.Buttons[i].Text.Length > text.Length)
				{
					text = this.Buttons[i].Text;
				}
			}
			Size empty = Size.Empty;
			if (text != null && text.Length > 0)
			{
				SizeF sizeF = TextRenderer.MeasureString(text, this.Font);
				if (sizeF != SizeF.Empty)
				{
					empty = new Size((int)Math.Ceiling((double)sizeF.Width) + 6, (int)Math.Ceiling((double)sizeF.Height));
				}
			}
			Size size = ((this.ImageList == null) ? new Size(16, 16) : this.ImageSize);
			Theme theme = ThemeEngine.Current;
			int num = size.Width + 2 * theme.ToolBarImageGripWidth;
			int num2 = size.Height + 2 * theme.ToolBarImageGripWidth;
			if (this.text_alignment == ToolBarTextAlign.Right)
			{
				empty.Width = num + empty.Width;
				empty.Height = ((empty.Height > num2) ? empty.Height : num2);
			}
			else
			{
				empty.Height = num2 + empty.Height;
				empty.Width = ((empty.Width > num) ? empty.Width : num);
			}
			empty.Width += theme.ToolBarImageGripWidth;
			empty.Height += theme.ToolBarImageGripWidth;
			return empty;
		}

		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x0600124C RID: 4684 RVA: 0x0005E6DC File Offset: 0x0005C8DC
		private Size AdjustedButtonSize
		{
			get
			{
				Size size;
				if (this.default_size.IsEmpty || this.Appearance == ToolBarAppearance.Normal)
				{
					size = this.ButtonSize;
				}
				else
				{
					size = this.default_size;
				}
				if (this.size_specified)
				{
					if (this.Appearance == ToolBarAppearance.Flat)
					{
						size = this.CalcButtonSize();
					}
					else
					{
						int toolBarImageGripWidth = ThemeEngine.Current.ToolBarImageGripWidth;
						if (size.Width < this.ImageSize.Width + 2 * toolBarImageGripWidth)
						{
							size.Width = this.ImageSize.Width + 2 * toolBarImageGripWidth;
						}
						if (size.Height < this.ImageSize.Height + 2 * toolBarImageGripWidth)
						{
							size.Height = this.ImageSize.Height + 2 * toolBarImageGripWidth;
						}
					}
				}
				return size;
			}
		}

		// Token: 0x0600124D RID: 4685 RVA: 0x0005E7A0 File Offset: 0x0005C9A0
		private bool LayoutToolBar()
		{
			bool flag = false;
			Theme theme = ThemeEngine.Current;
			int num = theme.ToolBarGripWidth;
			int num2 = theme.ToolBarGripWidth;
			Size adjustedButtonSize = this.AdjustedButtonSize;
			int num3 = (this.Vertical ? adjustedButtonSize.Width : adjustedButtonSize.Height) + theme.ToolBarGripWidth;
			int num4 = -1;
			this.items = new ToolBarItem[this.buttons.Count];
			for (int i = 0; i < this.buttons.Count; i++)
			{
				ToolBarButton toolBarButton = this.buttons[i];
				ToolBarItem toolBarItem = new ToolBarItem(toolBarButton);
				this.items[i] = toolBarItem;
				if (toolBarButton.Visible)
				{
					if (this.size_specified && toolBarButton.Style != ToolBarButtonStyle.Separator)
					{
						flag = toolBarItem.Layout(adjustedButtonSize);
					}
					else
					{
						flag = toolBarItem.Layout(this.Vertical, num3);
					}
					bool flag2 = toolBarButton.Style == ToolBarButtonStyle.Separator;
					if (this.Vertical)
					{
						if (num2 + toolBarItem.Rectangle.Height < base.Height || flag2 || !this.Wrappable)
						{
							if (toolBarItem.Location.X != num || toolBarItem.Location.Y != num2)
							{
								flag = true;
							}
							toolBarItem.Location = new Point(num, num2);
							num2 += toolBarItem.Rectangle.Height;
							if (flag2)
							{
								num4 = i;
							}
						}
						else if (num4 > 0)
						{
							i = num4;
							num4 = -1;
							num2 = theme.ToolBarGripWidth;
							num += num3;
						}
						else
						{
							num2 = theme.ToolBarGripWidth;
							num += num3;
							if (toolBarItem.Location.X != num || toolBarItem.Location.Y != num2)
							{
								flag = true;
							}
							toolBarItem.Location = new Point(num, num2);
							num2 += toolBarItem.Rectangle.Height;
						}
					}
					else if (num + toolBarItem.Rectangle.Width < base.Width || flag2 || !this.Wrappable)
					{
						if (toolBarItem.Location.X != num || toolBarItem.Location.Y != num2)
						{
							flag = true;
						}
						toolBarItem.Location = new Point(num, num2);
						num += toolBarItem.Rectangle.Width;
						if (flag2)
						{
							num4 = i;
						}
					}
					else if (num4 > 0)
					{
						i = num4;
						num4 = -1;
						num = theme.ToolBarGripWidth;
						num2 += num3;
					}
					else
					{
						num = theme.ToolBarGripWidth;
						num2 += num3;
						if (toolBarItem.Location.X != num || toolBarItem.Location.Y != num2)
						{
							flag = true;
						}
						toolBarItem.Location = new Point(num, num2);
						num += toolBarItem.Rectangle.Width;
					}
				}
			}
			if (base.Parent == null)
			{
				return flag;
			}
			if (this.Wrappable)
			{
				num3 += (this.Vertical ? num : num2);
			}
			if (base.IsHandleCreated)
			{
				if (this.Vertical)
				{
					base.Width = num3;
				}
				else
				{
					base.Height = num3;
				}
			}
			return flag;
		}

		// Token: 0x0600124E RID: 4686 RVA: 0x0005EABF File Offset: 0x0005CCBF
		// Note: this type is marked as 'beforefieldinit'.
		static ToolBar()
		{
			ToolBar.ButtonClickEvent = new object();
			ToolBar.ButtonDropDownEvent = new object();
		}

		// Token: 0x04000B4C RID: 2892
		private bool size_specified;

		// Token: 0x04000B4D RID: 2893
		private ToolBarItem current_item;

		// Token: 0x04000B4E RID: 2894
		internal ToolBarItem[] items;

		// Token: 0x04000B4F RID: 2895
		internal Size default_size;

		// Token: 0x04000B51 RID: 2897
		private static object ButtonDropDownEvent;

		// Token: 0x04000B52 RID: 2898
		private ToolBarAppearance appearance;

		// Token: 0x04000B53 RID: 2899
		private bool autosize = true;

		// Token: 0x04000B54 RID: 2900
		private ToolBar.ToolBarButtonCollection buttons;

		// Token: 0x04000B55 RID: 2901
		private Size button_size;

		// Token: 0x04000B56 RID: 2902
		private bool divider = true;

		// Token: 0x04000B57 RID: 2903
		private bool drop_down_arrows = true;

		// Token: 0x04000B58 RID: 2904
		private ImageList image_list;

		// Token: 0x04000B59 RID: 2905
		private ImeMode ime_mode = ImeMode.Disable;

		// Token: 0x04000B5A RID: 2906
		private bool show_tooltips = true;

		// Token: 0x04000B5B RID: 2907
		private ToolBarTextAlign text_alignment;

		// Token: 0x04000B5C RID: 2908
		private bool wrappable = true;

		// Token: 0x04000B5D RID: 2909
		private ToolBarButton button_for_focus;

		// Token: 0x04000B5E RID: 2910
		private int requested_size = -1;

		// Token: 0x04000B5F RID: 2911
		private ToolTip tip_window;

		// Token: 0x04000B60 RID: 2912
		private Timer tipdown_timer;

		/// <summary>Encapsulates a collection of <see cref="T:System.Windows.Forms.ToolBarButton" /> controls for use by the <see cref="T:System.Windows.Forms.ToolBar" /> class.</summary>
		// Token: 0x020001AF RID: 431
		public class ToolBarButtonCollection : IList, ICollection, IEnumerable
		{
			// Token: 0x0600124F RID: 4687 RVA: 0x0005EAD8 File Offset: 0x0005CCD8
			internal void OnUIACollectionChanged(CollectionChangeEventArgs e)
			{
				CollectionChangeEventHandler collectionChangeEventHandler = (CollectionChangeEventHandler)this.owner.Events[ToolBar.ToolBarButtonCollection.UIACollectionChangedEvent];
				if (collectionChangeEventHandler != null)
				{
					collectionChangeEventHandler(this.owner, e);
				}
			}

			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolBar.ToolBarButtonCollection" /> class and assigns it to the specified toolbar.</summary>
			/// <param name="owner">The <see cref="T:System.Windows.Forms.ToolBar" /> that is the parent of the collection of <see cref="T:System.Windows.Forms.ToolBarButton" /> controls. </param>
			// Token: 0x06001250 RID: 4688 RVA: 0x0005EB10 File Offset: 0x0005CD10
			public ToolBarButtonCollection(ToolBar owner)
			{
				this.list = new ArrayList();
				this.owner = owner;
				this.redraw = true;
			}

			/// <summary>Gets the number of buttons in the toolbar button collection.</summary>
			/// <returns>The number of the <see cref="T:System.Windows.Forms.ToolBarButton" /> controls assigned to the toolbar.</returns>
			// Token: 0x170004AF RID: 1199
			// (get) Token: 0x06001251 RID: 4689 RVA: 0x0005EB31 File Offset: 0x0005CD31
			[Browsable(false)]
			public int Count
			{
				get
				{
					return this.list.Count;
				}
			}

			/// <summary>Gets a value indicating whether the collection is read-only.</summary>
			/// <returns>true if the collection is read-only; otherwise, false. The default is false.</returns>
			// Token: 0x170004B0 RID: 1200
			// (get) Token: 0x06001252 RID: 4690 RVA: 0x0005EB3E File Offset: 0x0005CD3E
			public bool IsReadOnly
			{
				get
				{
					return this.list.IsReadOnly;
				}
			}

			/// <summary>Gets or sets the toolbar button at the specified indexed location in the toolbar button collection.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.ToolBarButton" /> that represents the toolbar button at the specified indexed location.</returns>
			/// <param name="index">The indexed location of the <see cref="T:System.Windows.Forms.ToolBarButton" /> in the collection. </param>
			/// <exception cref="T:System.ArgumentNullException">The <paramref name="index" /> value is null. </exception>
			/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> value is less than zero.-or- The <paramref name="index" /> value is greater than the number of buttons in the collection, and the collection of buttons is not null. </exception>
			// Token: 0x170004B1 RID: 1201
			public virtual ToolBarButton this[int index]
			{
				get
				{
					return (ToolBarButton)this.list[index];
				}
				set
				{
					this.OnUIACollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Remove, index));
					value.SetParent(this.owner);
					this.list[index] = value;
					this.owner.Redraw(true);
					this.OnUIACollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Add, index));
				}
			}

			/// <summary>Gets a value indicating whether access to the collection is synchronized (thread safe).</summary>
			/// <returns>false in all cases.</returns>
			// Token: 0x170004B2 RID: 1202
			// (get) Token: 0x06001255 RID: 4693 RVA: 0x0005EBB6 File Offset: 0x0005CDB6
			bool ICollection.IsSynchronized
			{
				get
				{
					return this.list.IsSynchronized;
				}
			}

			/// <summary>Gets an object that can be used to synchronize access to the collection of buttons.</summary>
			// Token: 0x170004B3 RID: 1203
			// (get) Token: 0x06001256 RID: 4694 RVA: 0x0005EBC3 File Offset: 0x0005CDC3
			object ICollection.SyncRoot
			{
				get
				{
					return this.list.SyncRoot;
				}
			}

			/// <summary>Gets a value indicating whether the collection has a fixed size.</summary>
			/// <returns>false in all cases.</returns>
			// Token: 0x170004B4 RID: 1204
			// (get) Token: 0x06001257 RID: 4695 RVA: 0x0005EBD0 File Offset: 0x0005CDD0
			bool IList.IsFixedSize
			{
				get
				{
					return this.list.IsFixedSize;
				}
			}

			/// <summary>Gets or sets the item at a specified index.</summary>
			/// <returns>The element at the specified index.</returns>
			/// <param name="index">The zero-based index of the element to get or set. </param>
			// Token: 0x170004B5 RID: 1205
			object IList.this[int index]
			{
				get
				{
					return this[index];
				}
				set
				{
					if (!(value is ToolBarButton))
					{
						throw new ArgumentException("Not of type ToolBarButton", "value");
					}
					this[index] = (ToolBarButton)value;
				}
			}

			/// <summary>Adds the specified toolbar button to the end of the toolbar button collection.</summary>
			/// <returns>The zero-based index value of the <see cref="T:System.Windows.Forms.ToolBarButton" /> added to the collection.</returns>
			/// <param name="button">The <see cref="T:System.Windows.Forms.ToolBarButton" /> to be added after all existing buttons. </param>
			// Token: 0x0600125A RID: 4698 RVA: 0x0005EC10 File Offset: 0x0005CE10
			public int Add(ToolBarButton button)
			{
				button.SetParent(this.owner);
				int num = this.list.Add(button);
				if (this.redraw)
				{
					this.owner.Redraw(true);
				}
				this.OnUIACollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Add, num));
				return num;
			}

			/// <summary>Adds a collection of toolbar buttons to this toolbar button collection.</summary>
			/// <param name="buttons">The collection of <see cref="T:System.Windows.Forms.ToolBarButton" /> controls to add to this <see cref="T:System.Windows.Forms.ToolBar.ToolBarButtonCollection" /> contained in an array. </param>
			// Token: 0x0600125B RID: 4699 RVA: 0x0005EC60 File Offset: 0x0005CE60
			public void AddRange(ToolBarButton[] buttons)
			{
				try
				{
					this.redraw = false;
					foreach (ToolBarButton toolBarButton in buttons)
					{
						this.Add(toolBarButton);
					}
				}
				finally
				{
					this.redraw = true;
					this.owner.Redraw(true);
				}
			}

			/// <summary>Removes all buttons from the toolbar button collection.</summary>
			// Token: 0x0600125C RID: 4700 RVA: 0x0005ECB8 File Offset: 0x0005CEB8
			public void Clear()
			{
				this.list.Clear();
				this.owner.Redraw(false);
				this.OnUIACollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Refresh, -1));
			}

			/// <summary>Determines if the specified toolbar button is a member of the collection.</summary>
			/// <returns>true if the <see cref="T:System.Windows.Forms.ToolBarButton" /> is a member of the collection; otherwise, false.</returns>
			/// <param name="button">The <see cref="T:System.Windows.Forms.ToolBarButton" /> to locate in the collection. </param>
			// Token: 0x0600125D RID: 4701 RVA: 0x0005ECE3 File Offset: 0x0005CEE3
			public bool Contains(ToolBarButton button)
			{
				return this.list.Contains(button);
			}

			/// <summary>Returns an enumerator that can be used to iterate through the toolbar button collection.</summary>
			/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that represents the tree node collection.</returns>
			// Token: 0x0600125E RID: 4702 RVA: 0x0005ECF1 File Offset: 0x0005CEF1
			public IEnumerator GetEnumerator()
			{
				return this.list.GetEnumerator();
			}

			/// <summary>Copies the elements of the <see cref="T:System.Collections.ICollection" /> to an <see cref="T:System.Array" />, starting at a particular <see cref="T:System.Array" /> index.</summary>
			/// <param name="dest">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.ICollection" />. The <see cref="T:System.Array" /> must have zero-based indexing. </param>
			/// <param name="index">The zero-based index in <paramref name="dest" /> at which copying begins. </param>
			// Token: 0x0600125F RID: 4703 RVA: 0x0005ECFE File Offset: 0x0005CEFE
			void ICollection.CopyTo(Array dest, int index)
			{
				this.list.CopyTo(dest, index);
			}

			/// <summary>Adds the specified toolbar button to the end of the toolbar button collection.</summary>
			/// <returns>The zero-based index value of the <see cref="T:System.Windows.Forms.ToolBarButton" /> added to the collection.</returns>
			/// <param name="button">The <see cref="T:System.Windows.Forms.ToolBarButton" /> to be added after all existing buttons.</param>
			/// <exception cref="T:System.ArgumentException">
			///   <paramref name="button" /> is not a <see cref="T:System.Windows.Forms.ToolBarButton" />.</exception>
			// Token: 0x06001260 RID: 4704 RVA: 0x0005ED0D File Offset: 0x0005CF0D
			int IList.Add(object button)
			{
				if (!(button is ToolBarButton))
				{
					throw new ArgumentException("Not of type ToolBarButton", "button");
				}
				return this.Add((ToolBarButton)button);
			}

			/// <summary>Determines whether the collection contains a specific value.</summary>
			/// <returns>true if the item is found in the collection; otherwise, false.</returns>
			/// <param name="button">The item to locate in the collection. </param>
			// Token: 0x06001261 RID: 4705 RVA: 0x0005ED33 File Offset: 0x0005CF33
			bool IList.Contains(object button)
			{
				if (!(button is ToolBarButton))
				{
					throw new ArgumentException("Not of type ToolBarButton", "button");
				}
				return this.Contains((ToolBarButton)button);
			}

			/// <summary>Determines the index of a specific item in the collection.</summary>
			/// <returns>The index of <paramref name="button" /> if found in the list; otherwise, -1.</returns>
			/// <param name="button">The item to locate in the collection. </param>
			// Token: 0x06001262 RID: 4706 RVA: 0x0005ED59 File Offset: 0x0005CF59
			int IList.IndexOf(object button)
			{
				if (!(button is ToolBarButton))
				{
					throw new ArgumentException("Not of type ToolBarButton", "button");
				}
				return this.IndexOf((ToolBarButton)button);
			}

			/// <summary>Inserts an existing toolbar button in the toolbar button collection at the specified location.</summary>
			/// <param name="index">The indexed location within the collection to insert the toolbar button. </param>
			/// <param name="button">The <see cref="T:System.Windows.Forms.ToolBarButton" /> to insert.</param>
			/// <exception cref="T:System.ArgumentException">
			///   <paramref name="button" /> is not a <see cref="T:System.Windows.Forms.ToolBarButton" />.</exception>
			// Token: 0x06001263 RID: 4707 RVA: 0x0005ED7F File Offset: 0x0005CF7F
			void IList.Insert(int index, object button)
			{
				if (!(button is ToolBarButton))
				{
					throw new ArgumentException("Not of type ToolBarButton", "button");
				}
				this.Insert(index, (ToolBarButton)button);
			}

			/// <summary>Removes the first occurrence of an item from the collection.</summary>
			/// <param name="button">The <see cref="T:System.Object" /> to remove from the <see cref="T:System.Collections.IList" />. </param>
			// Token: 0x06001264 RID: 4708 RVA: 0x0005EDA6 File Offset: 0x0005CFA6
			void IList.Remove(object button)
			{
				if (!(button is ToolBarButton))
				{
					throw new ArgumentException("Not of type ToolBarButton", "button");
				}
				this.Remove((ToolBarButton)button);
			}

			/// <summary>Retrieves the index of the specified toolbar button in the collection.</summary>
			/// <returns>The zero-based index of the item found in the collection; otherwise, -1.</returns>
			/// <param name="button">The <see cref="T:System.Windows.Forms.ToolBarButton" /> to locate in the collection. </param>
			// Token: 0x06001265 RID: 4709 RVA: 0x0005EDCC File Offset: 0x0005CFCC
			public int IndexOf(ToolBarButton button)
			{
				return this.list.IndexOf(button);
			}

			/// <summary>Inserts an existing toolbar button in the toolbar button collection at the specified location.</summary>
			/// <param name="index">The indexed location within the collection to insert the toolbar button. </param>
			/// <param name="button">The <see cref="T:System.Windows.Forms.ToolBarButton" /> to insert. </param>
			// Token: 0x06001266 RID: 4710 RVA: 0x0005EDDA File Offset: 0x0005CFDA
			public void Insert(int index, ToolBarButton button)
			{
				this.list.Insert(index, button);
				this.owner.Redraw(true);
				this.OnUIACollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Add, index));
			}

			/// <summary>Removes a given button from the toolbar button collection.</summary>
			/// <param name="button">The <see cref="T:System.Windows.Forms.ToolBarButton" /> to remove from the collection. </param>
			// Token: 0x06001267 RID: 4711 RVA: 0x0005EE07 File Offset: 0x0005D007
			public void Remove(ToolBarButton button)
			{
				this.list.Remove(button);
				this.owner.Redraw(true);
			}

			/// <summary>Removes a given button from the toolbar button collection.</summary>
			/// <param name="index">The indexed location of the <see cref="T:System.Windows.Forms.ToolBarButton" /> in the collection. </param>
			/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> value is less than 0, or it is greater than the number of buttons in the collection. </exception>
			// Token: 0x06001268 RID: 4712 RVA: 0x0005EE21 File Offset: 0x0005D021
			public void RemoveAt(int index)
			{
				this.list.RemoveAt(index);
				this.owner.Redraw(true);
				this.OnUIACollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Remove, index));
			}

			// Token: 0x04000B61 RID: 2913
			private ArrayList list;

			// Token: 0x04000B62 RID: 2914
			private ToolBar owner;

			// Token: 0x04000B63 RID: 2915
			private bool redraw;

			// Token: 0x04000B64 RID: 2916
			private static object UIACollectionChangedEvent = new object();
		}
	}
}
