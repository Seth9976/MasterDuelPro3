using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Represents a Windows combo box control. </summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000041 RID: 65
	[DefaultProperty("Items")]
	[DefaultEvent("SelectedIndexChanged")]
	[Designer("System.Windows.Forms.Design.ComboBoxDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[DefaultBindingProperty("Text")]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	public class ComboBox : ListControl
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ComboBox" /> class.</summary>
		// Token: 0x06000155 RID: 341 RVA: 0x00005990 File Offset: 0x00003B90
		public ComboBox()
		{
			this.items = new ComboBox.ObjectCollection(this);
			this.DropDownStyle = ComboBoxStyle.DropDown;
			this.item_height = base.FontHeight + 2;
			this.background_color = ThemeEngine.Current.ColorControl;
			this.border_style = BorderStyle.None;
			this.drop_down_height = 106;
			this.flat_style = FlatStyle.Standard;
			base.MouseDown += this.OnMouseDownCB;
			base.MouseUp += this.OnMouseUpCB;
			base.MouseMove += this.OnMouseMoveCB;
			base.MouseWheel += this.OnMouseWheelCB;
			base.MouseEnter += this.OnMouseEnter;
			base.MouseLeave += this.OnMouseLeave;
			base.KeyDown += this.OnKeyDownCB;
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.ComboBox.SelectedIndex" /> property has changed.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000156 RID: 342 RVA: 0x00005AA4 File Offset: 0x00003CA4
		// (remove) Token: 0x06000157 RID: 343 RVA: 0x00005AB7 File Offset: 0x00003CB7
		public event EventHandler SelectedIndexChanged
		{
			add
			{
				base.Events.AddHandler(ComboBox.SelectedIndexChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ComboBox.SelectedIndexChangedEvent, value);
			}
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00005ACC File Offset: 0x00003CCC
		private void SetTextBoxAutoCompleteData()
		{
			if (this.textbox_ctrl == null)
			{
				return;
			}
			this.textbox_ctrl.AutoCompleteMode = this.auto_complete_mode;
			if (this.auto_complete_source == AutoCompleteSource.ListItems)
			{
				this.textbox_ctrl.AutoCompleteSource = AutoCompleteSource.CustomSource;
				this.textbox_ctrl.AutoCompleteCustomSource = null;
				this.textbox_ctrl.AutoCompleteInternalSource = this;
				return;
			}
			this.textbox_ctrl.AutoCompleteSource = this.auto_complete_source;
			this.textbox_ctrl.AutoCompleteCustomSource = this.auto_complete_custom_source;
			this.textbox_ctrl.AutoCompleteInternalSource = null;
		}

		/// <summary>Gets or sets the background color for the control.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the background color of the control. The default is the value of the <see cref="P:System.Windows.Forms.Control.DefaultBackColor" /> property.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000159 RID: 345 RVA: 0x000042CE File Offset: 0x000024CE
		// (set) Token: 0x0600015A RID: 346 RVA: 0x00005B54 File Offset: 0x00003D54
		public override Color BackColor
		{
			get
			{
				return base.BackColor;
			}
			set
			{
				if (base.BackColor == value)
				{
					return;
				}
				base.BackColor = value;
				this.Refresh();
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>The background image displayed in the control.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600015B RID: 347 RVA: 0x00005B72 File Offset: 0x00003D72
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override Image BackgroundImage
		{
			get
			{
				return base.BackgroundImage;
			}
		}

		/// <summary>Gets or sets the background image layout as defined in the <see cref="T:System.Windows.Forms.ImageLayout" /> enumeration.</summary>
		/// <returns>One of the values of <see cref="T:System.Windows.Forms.ImageLayout" /> (Center, None, Stretch, Tile, or Zoom).</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value is not one of the values of <see cref="T:System.Windows.Forms.ImageLayout" />. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600015C RID: 348 RVA: 0x00005B7A File Offset: 0x00003D7A
		// (set) Token: 0x0600015D RID: 349 RVA: 0x00005B82 File Offset: 0x00003D82
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

		/// <summary>Gets the required creation parameters when the control handle is created.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.CreateParams" /> that contains the required creation parameters when the handle to the control is created.</returns>
		// Token: 0x17000067 RID: 103
		// (get) Token: 0x0600015E RID: 350 RVA: 0x00004663 File Offset: 0x00002863
		protected override CreateParams CreateParams
		{
			get
			{
				return base.CreateParams;
			}
		}

		/// <summary>Gets the default size of the control.</summary>
		/// <returns>The default <see cref="T:System.Drawing.Size" /> of the control.</returns>
		// Token: 0x17000068 RID: 104
		// (get) Token: 0x0600015F RID: 351 RVA: 0x00005B8B File Offset: 0x00003D8B
		protected override Size DefaultSize
		{
			get
			{
				return new Size(121, 21);
			}
		}

		/// <summary>Gets or sets a value indicating whether your code or the operating system will handle drawing of elements in the list.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DrawMode" /> enumeration values. The default is <see cref="F:System.Windows.Forms.DrawMode.Normal" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value is not a valid <see cref="T:System.Windows.Forms.DrawMode" /> enumeration value. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000160 RID: 352 RVA: 0x00005B96 File Offset: 0x00003D96
		// (set) Token: 0x06000161 RID: 353 RVA: 0x00005BA0 File Offset: 0x00003DA0
		[RefreshProperties(RefreshProperties.Repaint)]
		[DefaultValue(DrawMode.Normal)]
		[MWFCategory("Behavior")]
		public DrawMode DrawMode
		{
			get
			{
				return this.draw_mode;
			}
			set
			{
				if (!Enum.IsDefined(typeof(DrawMode), value))
				{
					throw new InvalidEnumArgumentException(string.Format("Enum argument value '{0}' is not valid for DrawMode", value));
				}
				if (this.draw_mode == value)
				{
					return;
				}
				if (this.draw_mode == DrawMode.OwnerDrawVariable)
				{
					this.item_heights = null;
				}
				this.draw_mode = value;
				if (this.draw_mode == DrawMode.OwnerDrawVariable)
				{
					this.item_heights = new Hashtable();
				}
				this.Refresh();
			}
		}

		/// <summary>Gets or sets the height in pixels of the drop-down portion of the <see cref="T:System.Windows.Forms.ComboBox" />.</summary>
		/// <returns>The height, in pixels, of the drop-down box.</returns>
		/// <exception cref="T:System.ArgumentException">The specified value is less than one. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000162 RID: 354 RVA: 0x00005C15 File Offset: 0x00003E15
		[Browsable(true)]
		[DefaultValue(106)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		[MWFCategory("Behavior")]
		public int DropDownHeight
		{
			get
			{
				return this.drop_down_height;
			}
		}

		/// <summary>Gets or sets a value specifying the style of the combo box.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ComboBoxStyle" /> values. The default is DropDown.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The assigned value is not one of the <see cref="T:System.Windows.Forms.ComboBoxStyle" /> values. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000163 RID: 355 RVA: 0x00005C1D File Offset: 0x00003E1D
		// (set) Token: 0x06000164 RID: 356 RVA: 0x00005C28 File Offset: 0x00003E28
		[DefaultValue(ComboBoxStyle.DropDown)]
		[RefreshProperties(RefreshProperties.Repaint)]
		[MWFCategory("Appearance")]
		public ComboBoxStyle DropDownStyle
		{
			get
			{
				return this.dropdown_style;
			}
			set
			{
				if (!Enum.IsDefined(typeof(ComboBoxStyle), value))
				{
					throw new InvalidEnumArgumentException(string.Format("Enum argument value '{0}' is not valid for ComboBoxStyle", value));
				}
				if (this.dropdown_style == value)
				{
					return;
				}
				base.SuspendLayout();
				if (this.dropdown_style == ComboBoxStyle.Simple && this.listbox_ctrl != null)
				{
					base.Controls.RemoveImplicit(this.listbox_ctrl);
					this.listbox_ctrl.Dispose();
					this.listbox_ctrl = null;
				}
				this.dropdown_style = value;
				if (this.dropdown_style == ComboBoxStyle.DropDownList && this.textbox_ctrl != null)
				{
					base.Controls.RemoveImplicit(this.textbox_ctrl);
					this.textbox_ctrl.Dispose();
					this.textbox_ctrl = null;
				}
				if (this.dropdown_style == ComboBoxStyle.Simple)
				{
					this.show_dropdown_button = false;
					this.CreateComboListBox();
					base.Controls.AddImplicit(this.listbox_ctrl);
					this.listbox_ctrl.Visible = true;
					if (this.requested_height == -1)
					{
						this.requested_height = 150;
					}
				}
				else
				{
					this.show_dropdown_button = true;
					this.button_state = ButtonState.Normal;
				}
				if (this.dropdown_style != ComboBoxStyle.DropDownList && this.textbox_ctrl == null)
				{
					this.textbox_ctrl = new ComboBox.ComboTextBox(this);
					object selectedItem = this.SelectedItem;
					if (selectedItem != null)
					{
						this.textbox_ctrl.Text = base.GetItemText(selectedItem);
					}
					this.textbox_ctrl.BorderStyle = BorderStyle.None;
					this.textbox_ctrl.TextChanged += this.OnTextChangedEdit;
					this.textbox_ctrl.KeyPress += this.OnTextKeyPress;
					this.textbox_ctrl.Click += this.OnTextBoxClick;
					this.textbox_ctrl.ContextMenu = this.ContextMenu;
					this.textbox_ctrl.TopMargin = 1;
					if (base.IsHandleCreated)
					{
						base.Controls.AddImplicit(this.textbox_ctrl);
					}
					this.SetTextBoxAutoCompleteData();
				}
				base.ResumeLayout();
				this.OnDropDownStyleChanged(EventArgs.Empty);
				this.LayoutComboBox();
				this.UpdateComboBoxBounds();
				this.Refresh();
			}
		}

		/// <summary>Gets or sets the width of the of the drop-down portion of a combo box.</summary>
		/// <returns>The width, in pixels, of the drop-down box.</returns>
		/// <exception cref="T:System.ArgumentException">The specified value is less than one. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000165 RID: 357 RVA: 0x00005E25 File Offset: 0x00004025
		[MWFCategory("Behavior")]
		public int DropDownWidth
		{
			get
			{
				if (this.dropdown_width == -1)
				{
					return base.Width;
				}
				return this.dropdown_width;
			}
		}

		/// <summary>Gets or sets a value indicating whether the combo box is displaying its drop-down portion.</summary>
		/// <returns>true if the drop-down portion is displayed; otherwise, false. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000166 RID: 358 RVA: 0x00005E3D File Offset: 0x0000403D
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool DroppedDown
		{
			get
			{
				return this.dropdown_style == ComboBoxStyle.Simple || this.dropped_down;
			}
		}

		/// <summary>Gets or sets the appearance of the <see cref="T:System.Windows.Forms.ComboBox" />.</summary>
		/// <returns>One of the values of <see cref="T:System.Windows.Forms.FlatStyle" />. The options are Flat, Popup, Standard, and System. The default is Standard.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value is not one of the values of <see cref="T:System.Windows.Forms.FlatStyle" />. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000167 RID: 359 RVA: 0x00005E4F File Offset: 0x0000404F
		[DefaultValue(FlatStyle.Standard)]
		[Localizable(true)]
		[MWFCategory("Appearance")]
		public FlatStyle FlatStyle
		{
			get
			{
				return this.flat_style;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Windows.Forms.ComboBox" /> has focus.</summary>
		/// <returns>true if this control has focus; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000168 RID: 360 RVA: 0x00005E57 File Offset: 0x00004057
		public override bool Focused
		{
			get
			{
				return base.Focused;
			}
		}

		/// <summary>Gets or sets the foreground color of the control.</summary>
		/// <returns>The foreground <see cref="T:System.Drawing.Color" /> of the control. The default is the value of the <see cref="P:System.Windows.Forms.Control.DefaultForeColor" /> property.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000169 RID: 361 RVA: 0x00005E5F File Offset: 0x0000405F
		// (set) Token: 0x0600016A RID: 362 RVA: 0x00005E67 File Offset: 0x00004067
		public override Color ForeColor
		{
			get
			{
				return base.ForeColor;
			}
			set
			{
				if (base.ForeColor == value)
				{
					return;
				}
				base.ForeColor = value;
				this.Refresh();
			}
		}

		/// <summary>Gets or sets a value indicating whether the control should resize to avoid showing partial items.</summary>
		/// <returns>true if the list portion can contain only complete items; otherwise, false. The default is true.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600016B RID: 363 RVA: 0x00005E85 File Offset: 0x00004085
		[DefaultValue(true)]
		[Localizable(true)]
		[MWFCategory("Behavior")]
		public bool IntegralHeight
		{
			get
			{
				return this.integral_height;
			}
		}

		/// <summary>Gets or sets the height of an item in the combo box.</summary>
		/// <returns>The height, in pixels, of an item in the combo box.</returns>
		/// <exception cref="T:System.ArgumentException">The item height value is less than zero. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000072 RID: 114
		// (get) Token: 0x0600016C RID: 364 RVA: 0x00005E90 File Offset: 0x00004090
		[Localizable(true)]
		[MWFCategory("Behavior")]
		public int ItemHeight
		{
			get
			{
				if (this.item_height == -1)
				{
					this.item_height = (int)TextRenderer.MeasureString("The quick brown Fox", this.Font).Height;
				}
				return this.item_height;
			}
		}

		/// <summary>Gets an object representing the collection of the items contained in this <see cref="T:System.Windows.Forms.ComboBox" />.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ComboBox.ObjectCollection" /> representing the items in the <see cref="T:System.Windows.Forms.ComboBox" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000073 RID: 115
		// (get) Token: 0x0600016D RID: 365 RVA: 0x00005ECB File Offset: 0x000040CB
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Localizable(true)]
		[Editor("System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[MergableProperty(false)]
		[MWFCategory("Data")]
		public ComboBox.ObjectCollection Items
		{
			get
			{
				return this.items;
			}
		}

		/// <summary>Gets or sets the maximum number of items to be shown in the drop-down portion of the <see cref="T:System.Windows.Forms.ComboBox" />.</summary>
		/// <returns>The maximum number of items of in the drop-down portion. The minimum for this property is 1 and the maximum is 100.</returns>
		/// <exception cref="T:System.ArgumentException">The maximum number is set less than one or greater than 100. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000074 RID: 116
		// (get) Token: 0x0600016E RID: 366 RVA: 0x00005ED3 File Offset: 0x000040D3
		// (set) Token: 0x0600016F RID: 367 RVA: 0x00005EDB File Offset: 0x000040DB
		[DefaultValue(8)]
		[Localizable(true)]
		[MWFCategory("Behavior")]
		public int MaxDropDownItems
		{
			get
			{
				return this.maxdrop_items;
			}
			set
			{
				if (this.maxdrop_items == value)
				{
					return;
				}
				this.maxdrop_items = value;
			}
		}

		/// <summary>Gets or sets the size that is the upper limit that <see cref="M:System.Windows.Forms.Control.GetPreferredSize(System.Drawing.Size)" /> can specify.</summary>
		/// <returns>An ordered pair of type <see cref="T:System.Drawing.Size" /> representing the width and height of a rectangle.</returns>
		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000170 RID: 368 RVA: 0x00005EEE File Offset: 0x000040EE
		public override Size MaximumSize
		{
			get
			{
				return base.MaximumSize;
			}
		}

		/// <summary>Gets or sets the size that is the lower limit that <see cref="M:System.Windows.Forms.Control.GetPreferredSize(System.Drawing.Size)" /> can specify.</summary>
		/// <returns>An ordered pair of type <see cref="T:System.Drawing.Size" /> representing the width and height of a rectangle.</returns>
		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000171 RID: 369 RVA: 0x00005EF6 File Offset: 0x000040F6
		// (set) Token: 0x06000172 RID: 370 RVA: 0x00005EFE File Offset: 0x000040FE
		public override Size MinimumSize
		{
			get
			{
				return base.MinimumSize;
			}
			set
			{
				base.MinimumSize = new Size(value.Width, 0);
			}
		}

		/// <summary>Gets the preferred height of the <see cref="T:System.Windows.Forms.ComboBox" />.</summary>
		/// <returns>The preferred height, in pixels, of the item area of the combo box.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000173 RID: 371 RVA: 0x00005F13 File Offset: 0x00004113
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public int PreferredHeight
		{
			get
			{
				return this.Font.Height + 8;
			}
		}

		/// <summary>Gets or sets the index specifying the currently selected item.</summary>
		/// <returns>A zero-based index of the currently selected item. A value of negative one (-1) is returned if no item is selected.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified index is less than or equal to -2.-or- The specified index is greater than or equal to the number of items in the combo box. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000174 RID: 372 RVA: 0x00005F22 File Offset: 0x00004122
		// (set) Token: 0x06000175 RID: 373 RVA: 0x00005F2A File Offset: 0x0000412A
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override int SelectedIndex
		{
			get
			{
				return this.selected_index;
			}
			set
			{
				this.SetSelectedIndex(value, false);
			}
		}

		/// <summary>Gets or sets currently selected item in the <see cref="T:System.Windows.Forms.ComboBox" />.</summary>
		/// <returns>The object that is the currently selected item or null if there is no currently selected item.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000176 RID: 374 RVA: 0x00005F34 File Offset: 0x00004134
		// (set) Token: 0x06000177 RID: 375 RVA: 0x00005F52 File Offset: 0x00004152
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Bindable(true)]
		public object SelectedItem
		{
			get
			{
				if (this.selected_index != -1)
				{
					return this.Items[this.selected_index];
				}
				return null;
			}
			set
			{
				if (((this.selected_index == -1) ? null : this.Items[this.selected_index]) == value)
				{
					return;
				}
				if (value == null)
				{
					this.SelectedIndex = -1;
					return;
				}
				this.SelectedIndex = this.Items.IndexOf(value);
			}
		}

		/// <summary>Gets or sets a value indicating whether the items in the combo box are sorted.</summary>
		/// <returns>true if the combo box is sorted; otherwise, false. The default is false.</returns>
		/// <exception cref="T:System.ArgumentException">An attempt was made to sort a <see cref="T:System.Windows.Forms.ComboBox" /> that is attached to a data source. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000178 RID: 376 RVA: 0x00005F92 File Offset: 0x00004192
		[DefaultValue(false)]
		[MWFCategory("Behavior")]
		public bool Sorted
		{
			get
			{
				return this.sorted;
			}
		}

		/// <summary>Gets or sets the text associated with this control.</summary>
		/// <returns>The text associated with this control.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000179 RID: 377 RVA: 0x00005F9A File Offset: 0x0000419A
		// (set) Token: 0x0600017A RID: 378 RVA: 0x00005FD4 File Offset: 0x000041D4
		[Bindable(true)]
		[Localizable(true)]
		public override string Text
		{
			get
			{
				if (this.dropdown_style != ComboBoxStyle.DropDownList && this.textbox_ctrl != null)
				{
					return this.textbox_ctrl.Text;
				}
				if (this.SelectedItem != null)
				{
					return base.GetItemText(this.SelectedItem);
				}
				return base.Text;
			}
			set
			{
				if (value == null)
				{
					if (this.SelectedIndex == -1)
					{
						if (this.dropdown_style != ComboBoxStyle.DropDownList)
						{
							this.SetControlText(string.Empty, false);
							return;
						}
					}
					else
					{
						this.SelectedIndex = -1;
					}
					return;
				}
				if (this.SelectedItem == null || string.Compare(value, base.GetItemText(this.SelectedItem), false, CultureInfo.CurrentCulture) != 0)
				{
					int num = this.FindStringExact(value, -1, false);
					if (num == -1)
					{
						num = this.FindStringExact(value, -1, true);
					}
					if (num != -1)
					{
						this.SelectedIndex = num;
						return;
					}
				}
				if (this.dropdown_style != ComboBoxStyle.DropDownList)
				{
					this.textbox_ctrl.Text = value;
				}
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x0600017B RID: 379 RVA: 0x00006066 File Offset: 0x00004266
		internal Rectangle ButtonArea
		{
			get
			{
				return this.button_area;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x0600017C RID: 380 RVA: 0x0000606E File Offset: 0x0000426E
		internal Rectangle TextArea
		{
			get
			{
				return this.text_area;
			}
		}

		/// <summary>Maintains performance when items are added to the <see cref="T:System.Windows.Forms.ComboBox" /> one at a time.</summary>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600017D RID: 381 RVA: 0x00006076 File Offset: 0x00004276
		public void BeginUpdate()
		{
			this.suspend_ctrlupdate = true;
		}

		/// <summary>Creates a handle for the control.</summary>
		// Token: 0x0600017E RID: 382 RVA: 0x0000607F File Offset: 0x0000427F
		protected override void CreateHandle()
		{
			base.CreateHandle();
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.ComboBox" /> and optionally releases the managed resources. </summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x0600017F RID: 383 RVA: 0x00006088 File Offset: 0x00004288
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.listbox_ctrl != null)
				{
					this.listbox_ctrl.Dispose();
					base.Controls.RemoveImplicit(this.listbox_ctrl);
					this.listbox_ctrl = null;
				}
				if (this.textbox_ctrl != null)
				{
					base.Controls.RemoveImplicit(this.textbox_ctrl);
					this.textbox_ctrl.Dispose();
					this.textbox_ctrl = null;
				}
			}
			base.Dispose(disposing);
		}

		/// <summary>Resumes painting the <see cref="T:System.Windows.Forms.ComboBox" /> control after painting is suspended by the <see cref="M:System.Windows.Forms.ComboBox.BeginUpdate" /> method.</summary>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000180 RID: 384 RVA: 0x000060F5 File Offset: 0x000042F5
		public void EndUpdate()
		{
			this.suspend_ctrlupdate = false;
			this.UpdatedItems();
			this.Refresh();
		}

		// Token: 0x06000181 RID: 385 RVA: 0x0000610C File Offset: 0x0000430C
		private int FindStringExact(string s, int startIndex, bool ignoreCase)
		{
			if (s == null || this.Items.Count == 0)
			{
				return -1;
			}
			if (startIndex < -1 || startIndex >= this.Items.Count)
			{
				throw new ArgumentOutOfRangeException("startIndex");
			}
			int num = startIndex;
			if (num == this.Items.Count - 1)
			{
				num = -1;
			}
			for (;;)
			{
				num++;
				if (string.Compare(s, base.GetItemText(this.Items[num]), ignoreCase, CultureInfo.CurrentCulture) == 0)
				{
					break;
				}
				if (num == this.Items.Count - 1)
				{
					num = -1;
				}
				if (num == startIndex)
				{
					return -1;
				}
			}
			return num;
		}

		/// <summary>Returns the height of an item in the <see cref="T:System.Windows.Forms.ComboBox" />.</summary>
		/// <returns>The height, in pixels, of the item at the specified index.</returns>
		/// <param name="index">The index of the item to return the height of. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> is less than zero.-or- The <paramref name="index" /> is greater than count of items in the list. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000182 RID: 386 RVA: 0x0000619C File Offset: 0x0000439C
		public int GetItemHeight(int index)
		{
			if (this.DrawMode != DrawMode.OwnerDrawVariable || !base.IsHandleCreated)
			{
				return this.ItemHeight;
			}
			if (index < 0 || index >= this.Items.Count)
			{
				throw new ArgumentOutOfRangeException("The item height value is less than zero");
			}
			object obj = this.Items[index];
			if (this.item_heights.Contains(obj))
			{
				return (int)this.item_heights[obj];
			}
			MeasureItemEventArgs measureItemEventArgs = new MeasureItemEventArgs(base.DeviceContext, index, this.ItemHeight);
			this.OnMeasureItem(measureItemEventArgs);
			this.item_heights[obj] = measureItemEventArgs.ItemHeight;
			return measureItemEventArgs.ItemHeight;
		}

		/// <summary>Determines whether the specified key is a regular input key or a special key that requires preprocessing.</summary>
		/// <returns>true if the specified key is a regular input key; otherwise, false.</returns>
		/// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values.</param>
		// Token: 0x06000183 RID: 387 RVA: 0x00006248 File Offset: 0x00004448
		protected override bool IsInputKey(Keys keyData)
		{
			Keys keys = keyData & Keys.KeyCode;
			return keys - Keys.PageUp <= 7;
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.BackColorChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06000184 RID: 388 RVA: 0x00006267 File Offset: 0x00004467
		protected override void OnBackColorChanged(EventArgs e)
		{
			base.OnBackColorChanged(e);
			if (this.textbox_ctrl != null)
			{
				this.textbox_ctrl.BackColor = this.BackColor;
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ComboBox.DrawItem" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DrawItemEventArgs" /> that contains the event data. </param>
		// Token: 0x06000185 RID: 389 RVA: 0x0000628C File Offset: 0x0000448C
		protected virtual void OnDrawItem(DrawItemEventArgs e)
		{
			DrawItemEventHandler drawItemEventHandler = (DrawItemEventHandler)base.Events[ComboBox.DrawItemEvent];
			if (drawItemEventHandler != null)
			{
				drawItemEventHandler(this, e);
			}
		}

		// Token: 0x06000186 RID: 390 RVA: 0x000062BC File Offset: 0x000044BC
		internal void HandleDrawItem(DrawItemEventArgs e)
		{
			DrawMode drawMode = this.DrawMode;
			if (drawMode - DrawMode.OwnerDrawFixed <= 1)
			{
				this.OnDrawItem(e);
				return;
			}
			ThemeEngine.Current.DrawComboBoxItem(this, e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ComboBox.DropDown" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000187 RID: 391 RVA: 0x000062EC File Offset: 0x000044EC
		protected virtual void OnDropDown(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ComboBox.DropDownEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ComboBox.DropDownClosed" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000188 RID: 392 RVA: 0x0000631C File Offset: 0x0000451C
		protected virtual void OnDropDownClosed(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ComboBox.DropDownClosedEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ComboBox.DropDownStyleChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000189 RID: 393 RVA: 0x0000634C File Offset: 0x0000454C
		protected virtual void OnDropDownStyleChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ComboBox.DropDownStyleChangedEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.FontChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x0600018A RID: 394 RVA: 0x0000637C File Offset: 0x0000457C
		protected override void OnFontChanged(EventArgs e)
		{
			base.OnFontChanged(e);
			if (this.textbox_ctrl != null)
			{
				this.textbox_ctrl.Font = this.Font;
			}
			if (!this.item_height_specified)
			{
				this.item_height = this.Font.Height + 2;
			}
			if (this.IntegralHeight)
			{
				this.UpdateComboBoxBounds();
			}
			this.LayoutComboBox();
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.ForeColorChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x0600018B RID: 395 RVA: 0x000063D8 File Offset: 0x000045D8
		protected override void OnForeColorChanged(EventArgs e)
		{
			base.OnForeColorChanged(e);
			if (this.textbox_ctrl != null)
			{
				this.textbox_ctrl.ForeColor = this.ForeColor;
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.GotFocus" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x0600018C RID: 396 RVA: 0x000063FC File Offset: 0x000045FC
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnGotFocus(EventArgs e)
		{
			if (this.dropdown_style == ComboBoxStyle.DropDownList)
			{
				base.Invalidate();
			}
			if (this.textbox_ctrl != null)
			{
				this.textbox_ctrl.SetSelectable(false);
				this.textbox_ctrl.ShowSelection = base.Enabled;
				this.textbox_ctrl.ActivateCaret(true);
				this.textbox_ctrl.SelectAll();
			}
			base.OnGotFocus(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.LostFocus" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x0600018D RID: 397 RVA: 0x0000645C File Offset: 0x0000465C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnLostFocus(EventArgs e)
		{
			if (this.dropdown_style == ComboBoxStyle.DropDownList)
			{
				base.Invalidate();
			}
			if (this.listbox_ctrl != null && this.dropped_down)
			{
				this.listbox_ctrl.HideWindow();
			}
			if (this.textbox_ctrl != null)
			{
				this.textbox_ctrl.SetSelectable(true);
				this.textbox_ctrl.ActivateCaret(false);
				this.textbox_ctrl.ShowSelection = false;
				this.textbox_ctrl.SelectionLength = 0;
				this.textbox_ctrl.HideAutoCompleteList();
			}
			base.OnLostFocus(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.HandleCreated" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x0600018E RID: 398 RVA: 0x000064E0 File Offset: 0x000046E0
		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
			base.SetBoundsInternal(base.Left, base.Top, base.Width, this.PreferredHeight, BoundsSpecified.None);
			if (this.textbox_ctrl != null)
			{
				base.Controls.AddImplicit(this.textbox_ctrl);
			}
			this.LayoutComboBox();
			this.UpdateComboBoxBounds();
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.HandleDestroyed" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x0600018F RID: 399 RVA: 0x00006538 File Offset: 0x00004738
		protected override void OnHandleDestroyed(EventArgs e)
		{
			base.OnHandleDestroyed(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.KeyPress" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.KeyPressEventArgs" /> that contains the event data.</param>
		// Token: 0x06000190 RID: 400 RVA: 0x00006544 File Offset: 0x00004744
		protected override void OnKeyPress(KeyPressEventArgs e)
		{
			if (this.dropdown_style == ComboBoxStyle.DropDownList)
			{
				int num = this.FindStringCaseInsensitive(e.KeyChar.ToString(), this.SelectedIndex + 1);
				if (num != -1)
				{
					this.SelectedIndex = num;
					if (this.DroppedDown)
					{
						if (this.SelectedIndex >= this.listbox_ctrl.LastVisibleItem())
						{
							this.listbox_ctrl.Scroll(this.SelectedIndex - this.listbox_ctrl.LastVisibleItem() + 1);
						}
						if (this.SelectedIndex < this.listbox_ctrl.FirstVisibleItem())
						{
							this.listbox_ctrl.Scroll(this.SelectedIndex - this.listbox_ctrl.FirstVisibleItem());
						}
					}
				}
			}
			base.OnKeyPress(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ComboBox.MeasureItem" /> event.</summary>
		/// <param name="e">The <see cref="T:System.Windows.Forms.MeasureItemEventArgs" /> that was raised. </param>
		// Token: 0x06000191 RID: 401 RVA: 0x000065F8 File Offset: 0x000047F8
		protected virtual void OnMeasureItem(MeasureItemEventArgs e)
		{
			MeasureItemEventHandler measureItemEventHandler = (MeasureItemEventHandler)base.Events[ComboBox.MeasureItemEvent];
			if (measureItemEventHandler != null)
			{
				measureItemEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.BackColorChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.  </param>
		// Token: 0x06000192 RID: 402 RVA: 0x00006626 File Offset: 0x00004826
		protected override void OnParentBackColorChanged(EventArgs e)
		{
			base.OnParentBackColorChanged(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Resize" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000193 RID: 403 RVA: 0x0000662F File Offset: 0x0000482F
		protected override void OnResize(EventArgs e)
		{
			this.LayoutComboBox();
			if (this.listbox_ctrl != null)
			{
				this.listbox_ctrl.CalcListBoxArea();
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ComboBox.SelectedIndexChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000194 RID: 404 RVA: 0x0000664C File Offset: 0x0000484C
		protected override void OnSelectedIndexChanged(EventArgs e)
		{
			base.OnSelectedIndexChanged(e);
			EventHandler eventHandler = (EventHandler)base.Events[ComboBox.SelectedIndexChangedEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DomainUpDown.SelectedItemChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000195 RID: 405 RVA: 0x0000493C File Offset: 0x00002B3C
		protected virtual void OnSelectedItemChanged(EventArgs e)
		{
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ListControl.SelectedValueChanged" /> event. </summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000196 RID: 406 RVA: 0x00006681 File Offset: 0x00004881
		protected override void OnSelectedValueChanged(EventArgs e)
		{
			base.OnSelectedValueChanged(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ComboBox.SelectionChangeCommitted" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000197 RID: 407 RVA: 0x0000668C File Offset: 0x0000488C
		protected virtual void OnSelectionChangeCommitted(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ComboBox.SelectionChangeCommittedEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Refreshes the item contained at the specified location.</summary>
		/// <param name="index">The location of the item to refresh.</param>
		// Token: 0x06000198 RID: 408 RVA: 0x000066BA File Offset: 0x000048BA
		protected override void RefreshItem(int index)
		{
			if (index < 0 || index >= this.Items.Count)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (this.draw_mode == DrawMode.OwnerDrawVariable)
			{
				this.item_heights.Remove(this.Items[index]);
			}
		}

		/// <summary>Processes a key message and generates the appropriate control events.</summary>
		/// <returns>true if the message was processed by the control; otherwise, false.</returns>
		/// <param name="m">A <see cref="T:System.Windows.Forms.Message" />, passed by reference, that represents the window message to process.</param>
		// Token: 0x06000199 RID: 409 RVA: 0x000066F9 File Offset: 0x000048F9
		protected override bool ProcessKeyEventArgs(ref Message m)
		{
			return base.ProcessKeyEventArgs(ref m);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.KeyDown" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains the event data. </param>
		// Token: 0x0600019A RID: 410 RVA: 0x00006702 File Offset: 0x00004902
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnKeyDown(KeyEventArgs e)
		{
			base.OnKeyDown(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Validating" /> event.</summary>
		/// <param name="e">A <see cref="T:System.ComponentModel.CancelEventArgs" /> that contains the event data. </param>
		// Token: 0x0600019B RID: 411 RVA: 0x0000670B File Offset: 0x0000490B
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnValidating(CancelEventArgs e)
		{
			base.OnValidating(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.TextChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x0600019C RID: 412 RVA: 0x00006714 File Offset: 0x00004914
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnTextChanged(EventArgs e)
		{
			base.OnTextChanged(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ComboBox.TextUpdate" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x0600019D RID: 413 RVA: 0x00006720 File Offset: 0x00004920
		protected virtual void OnTextUpdate(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ComboBox.TextUpdateEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseLeave" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x0600019E RID: 414 RVA: 0x0000674E File Offset: 0x0000494E
		protected override void OnMouseLeave(EventArgs e)
		{
			if (this.flat_style == FlatStyle.Popup)
			{
				base.Invalidate();
			}
			base.OnMouseLeave(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseEnter" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x0600019F RID: 415 RVA: 0x00006766 File Offset: 0x00004966
		protected override void OnMouseEnter(EventArgs e)
		{
			if (this.flat_style == FlatStyle.Popup)
			{
				base.Invalidate();
			}
			base.OnMouseEnter(e);
		}

		/// <summary>Scales a control's location, size, padding and margin.</summary>
		/// <param name="factor">The factor by which the height and width of the control will be scaled.</param>
		/// <param name="specified">A <see cref="T:System.Windows.Forms.BoundsSpecified" /> value that specifies the bounds of the control to use when defining its size and position.</param>
		// Token: 0x060001A0 RID: 416 RVA: 0x0000677E File Offset: 0x0000497E
		protected override void ScaleControl(SizeF factor, BoundsSpecified specified)
		{
			base.ScaleControl(factor, specified);
		}

		/// <summary>Sets the size and location of the <see cref="T:System.Windows.Forms.ComboBox" />.</summary>
		/// <param name="x">The horizontal location in pixels of the control. </param>
		/// <param name="y">The vertical location in pixels of the control. </param>
		/// <param name="width">The width in pixels of the control. </param>
		/// <param name="height">The height in pixels of the control. </param>
		/// <param name="specified">One of the <see cref="T:System.Windows.Forms.BoundsSpecified" /> values. </param>
		// Token: 0x060001A1 RID: 417 RVA: 0x00006788 File Offset: 0x00004988
		protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
		{
			bool flag = (this.Anchor & AnchorStyles.Top) != AnchorStyles.None && (this.Anchor & AnchorStyles.Bottom) > AnchorStyles.None;
			bool flag2 = this.Dock == DockStyle.Left || this.Dock == DockStyle.Right || this.Dock == DockStyle.Fill;
			if ((specified & BoundsSpecified.Height) != BoundsSpecified.None || (specified == BoundsSpecified.None && (flag || flag2)))
			{
				this.requested_height = height;
				height = this.SnapHeight(height);
			}
			base.SetBoundsCore(x, y, width, height, specified);
		}

		/// <summary>When overridden in a derived class, sets the specified array of objects in a collection in the derived class.</summary>
		/// <param name="value">An array of items.</param>
		// Token: 0x060001A2 RID: 418 RVA: 0x000067FC File Offset: 0x000049FC
		protected override void SetItemsCore(IList value)
		{
			this.BeginUpdate();
			try
			{
				this.Items.Clear();
				this.Items.AddRange(value);
			}
			finally
			{
				this.EndUpdate();
			}
		}

		/// <summary>Returns a string that represents the <see cref="T:System.Windows.Forms.ComboBox" /> control.</summary>
		/// <returns>A <see cref="T:System.String" /> that represents the current <see cref="T:System.Windows.Forms.ComboBox" />. The string includes the type and the number of items in the <see cref="T:System.Windows.Forms.ComboBox" /> control.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060001A3 RID: 419 RVA: 0x00006840 File Offset: 0x00004A40
		public override string ToString()
		{
			return base.ToString() + ", Items.Count:" + this.Items.Count;
		}

		/// <summary>Processes Windows messages.</summary>
		/// <param name="m">The Windows <see cref="T:System.Windows.Forms.Message" /> to process. </param>
		// Token: 0x060001A4 RID: 420 RVA: 0x00006864 File Offset: 0x00004A64
		protected override void WndProc(ref Message m)
		{
			Msg msg = (Msg)m.Msg;
			if (msg - Msg.WM_KEYDOWN > 1)
			{
				if (msg != Msg.WM_CHAR)
				{
					if (msg != Msg.WM_MOUSELEAVE)
					{
						goto IL_00CF;
					}
					Point point = base.PointToClient(Control.MousePosition);
					if (base.ClientRectangle.Contains(point))
					{
						return;
					}
					goto IL_00CF;
				}
			}
			else
			{
				Keys keys = (Keys)m.WParam.ToInt32();
				if (this.textbox_ctrl != null && this.textbox_ctrl.CanNavigateAutoCompleteList)
				{
					XplatUI.SendMessage(this.textbox_ctrl.Handle, (Msg)m.Msg, m.WParam, m.LParam);
					return;
				}
				if (keys == Keys.Up || keys == Keys.Down)
				{
					goto IL_00CF;
				}
			}
			if (!this.ProcessKeyMessage(ref m) && this.textbox_ctrl != null)
			{
				XplatUI.SendMessage(this.textbox_ctrl.Handle, (Msg)m.Msg, m.WParam, m.LParam);
			}
			return;
			IL_00CF:
			base.WndProc(ref m);
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060001A5 RID: 421 RVA: 0x00006947 File Offset: 0x00004B47
		// (set) Token: 0x060001A6 RID: 422 RVA: 0x0000493C File Offset: 0x00002B3C
		internal override bool InternalCapture
		{
			get
			{
				return base.Capture;
			}
			set
			{
			}
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x00006950 File Offset: 0x00004B50
		private void LayoutComboBox()
		{
			int width = ThemeEngine.Current.Border3DSize.Width;
			this.text_area = base.ClientRectangle;
			this.text_area.Height = this.PreferredHeight;
			this.listbox_area = base.ClientRectangle;
			this.listbox_area.Y = this.text_area.Bottom + 3;
			this.listbox_area.Height = this.listbox_area.Height - (this.text_area.Height + 2);
			Rectangle rectangle = this.button_area;
			if (this.DropDownStyle == ComboBoxStyle.Simple)
			{
				this.button_area = Rectangle.Empty;
			}
			else
			{
				this.button_area = this.text_area;
				this.button_area.X = this.text_area.Right - 16 - width;
				this.button_area.Y = this.text_area.Y + width;
				this.button_area.Width = 16;
				this.button_area.Height = this.text_area.Height - 2 * width;
				if (this.flat_style == FlatStyle.Popup || this.flat_style == FlatStyle.Flat)
				{
					this.button_area.Inflate(1, 1);
					this.button_area.X = this.button_area.X + 2;
					this.button_area.Width = this.button_area.Width - 2;
				}
			}
			if (this.button_area != rectangle)
			{
				rectangle.Y -= width;
				rectangle.Width += width;
				rectangle.Height += 2 * width;
				base.Invalidate(rectangle);
				base.Invalidate(this.button_area);
			}
			if (this.textbox_ctrl != null)
			{
				int num = width + 1;
				this.textbox_ctrl.Location = new Point(this.text_area.X + num, this.text_area.Y + num);
				this.textbox_ctrl.Width = this.text_area.Width - this.button_area.Width - num * 2;
				this.textbox_ctrl.Height = this.text_area.Height - num * 2;
			}
			if (this.listbox_ctrl != null && this.dropdown_style == ComboBoxStyle.Simple)
			{
				this.listbox_ctrl.Location = this.listbox_area.Location;
				this.listbox_ctrl.CalcListBoxArea();
			}
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x00006B90 File Offset: 0x00004D90
		private void CreateComboListBox()
		{
			this.listbox_ctrl = new ComboBox.ComboListBox(this);
			this.listbox_ctrl.HighlightedIndex = this.SelectedIndex;
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x00006BB0 File Offset: 0x00004DB0
		internal void Draw(Rectangle clip, Graphics dc)
		{
			Theme theme = ThemeEngine.Current;
			FlatStyle flatStyle = this.FlatStyle;
			bool flag = flatStyle == FlatStyle.Flat || flatStyle == FlatStyle.Popup;
			theme.ComboBoxDrawBackground(this, dc, clip, flatStyle);
			int width = theme.Border3DSize.Width;
			if (this.dropdown_style == ComboBoxStyle.DropDownList)
			{
				DrawItemState drawItemState = DrawItemState.None;
				Color color = this.BackColor;
				Color color2 = this.ForeColor;
				Rectangle rectangle = this.text_area;
				rectangle.X += width;
				rectangle.Y += width;
				rectangle.Width -= this.button_area.Width + 2 * width;
				rectangle.Height -= 2 * width;
				if (this.Focused)
				{
					drawItemState = DrawItemState.Selected;
					drawItemState |= DrawItemState.Focus;
					color = SystemColors.Highlight;
					color2 = SystemColors.HighlightText;
				}
				drawItemState |= DrawItemState.ComboBoxEdit;
				this.HandleDrawItem(new DrawItemEventArgs(dc, this.Font, rectangle, this.SelectedIndex, drawItemState, color2, color));
			}
			if (this.show_dropdown_button)
			{
				ButtonState buttonState;
				if (this.is_enabled)
				{
					buttonState = this.button_state;
				}
				else
				{
					buttonState = ButtonState.Inactive;
				}
				if (flag || theme.ComboBoxNormalDropDownButtonHasTransparentBackground(this, buttonState))
				{
					dc.FillRectangle(theme.ResPool.GetSolidBrush(theme.ColorControl), this.button_area);
				}
				if (flag)
				{
					theme.DrawFlatStyleComboButton(dc, this.button_area, buttonState);
					return;
				}
				theme.ComboBoxDrawNormalDropDownButton(this, dc, clip, this.button_area, buttonState);
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060001AA RID: 426 RVA: 0x00006D20 File Offset: 0x00004F20
		// (set) Token: 0x060001AB RID: 427 RVA: 0x00006D28 File Offset: 0x00004F28
		internal bool DropDownButtonEntered
		{
			get
			{
				return this.drop_down_button_entered;
			}
			private set
			{
				if (this.drop_down_button_entered == value)
				{
					return;
				}
				this.drop_down_button_entered = value;
				if (ThemeEngine.Current.ComboBoxDropDownButtonHasHotElementStyle(this))
				{
					base.Invalidate(this.button_area);
				}
			}
		}

		// Token: 0x060001AC RID: 428 RVA: 0x00006D54 File Offset: 0x00004F54
		internal void DropDownListBox()
		{
			this.DropDownButtonEntered = false;
			if (this.DropDownStyle == ComboBoxStyle.Simple)
			{
				return;
			}
			if (this.listbox_ctrl == null)
			{
				this.CreateComboListBox();
			}
			this.listbox_ctrl.Location = base.PointToScreen(new Point(this.text_area.X, this.text_area.Y + this.text_area.Height));
			this.FindMatchOrSetIndex(this.SelectedIndex);
			if (this.textbox_ctrl != null)
			{
				this.textbox_ctrl.HideAutoCompleteList();
			}
			if (this.listbox_ctrl.ShowWindow())
			{
				this.dropped_down = true;
			}
			this.button_state = ButtonState.Pushed;
			if (this.dropdown_style == ComboBoxStyle.DropDownList)
			{
				base.Invalidate(this.text_area);
			}
		}

		// Token: 0x060001AD RID: 429 RVA: 0x00006E0C File Offset: 0x0000500C
		internal void DropDownListBoxFinished()
		{
			if (this.DropDownStyle == ComboBoxStyle.Simple)
			{
				return;
			}
			this.FindMatchOrSetIndex(this.SelectedIndex);
			this.button_state = ButtonState.Normal;
			base.Invalidate(this.button_area);
			this.dropped_down = false;
			this.OnDropDownClosed(EventArgs.Empty);
			if (this.listbox_ctrl != null)
			{
				this.listbox_ctrl.Dispose();
				this.listbox_ctrl = null;
			}
			if (this.textbox_ctrl != null)
			{
				this.textbox_ctrl.HideAutoCompleteList();
			}
		}

		// Token: 0x060001AE RID: 430 RVA: 0x00006E80 File Offset: 0x00005080
		private int FindStringCaseInsensitive(string search)
		{
			if (search.Length == 0)
			{
				return -1;
			}
			for (int i = 0; i < this.Items.Count; i++)
			{
				if (string.Compare(base.GetItemText(this.Items[i]), 0, search, 0, search.Length, true) == 0)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060001AF RID: 431 RVA: 0x00006ED4 File Offset: 0x000050D4
		internal int FindStringCaseInsensitive(string search, int start_index)
		{
			if (search.Length == 0)
			{
				return -1;
			}
			if (start_index < 0 || start_index > this.Items.Count)
			{
				throw new ArgumentOutOfRangeException("start_index");
			}
			for (int i = 0; i < this.Items.Count; i++)
			{
				int num = (i + start_index) % this.Items.Count;
				if (string.Compare(base.GetItemText(this.Items[num]), 0, search, 0, search.Length, true) == 0)
				{
					return num;
				}
			}
			return -1;
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x00006F54 File Offset: 0x00005154
		internal override bool IsInputCharInternal(char charCode)
		{
			return true;
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x00006F57 File Offset: 0x00005157
		// (set) Token: 0x060001B2 RID: 434 RVA: 0x00006F5F File Offset: 0x0000515F
		internal override ContextMenu ContextMenuInternal
		{
			get
			{
				return base.ContextMenuInternal;
			}
			set
			{
				base.ContextMenuInternal = value;
				if (this.textbox_ctrl != null)
				{
					this.textbox_ctrl.ContextMenu = value;
				}
			}
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x00006F7C File Offset: 0x0000517C
		internal void RestoreContextMenu()
		{
			this.textbox_ctrl.RestoreContextMenu();
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x00006F8C File Offset: 0x0000518C
		private void OnKeyDownCB(object sender, KeyEventArgs e)
		{
			if (this.Items.Count == 0)
			{
				return;
			}
			Keys keyCode = e.KeyCode;
			if (keyCode != Keys.Return && keyCode != Keys.Escape)
			{
				switch (keyCode)
				{
				case Keys.PageUp:
				{
					int num = ((this.listbox_ctrl == null) ? (this.MaxDropDownItems - 1) : (this.listbox_ctrl.page_size - 1));
					if (num < 1)
					{
						num = 1;
					}
					this.SetSelectedIndex(Math.Max(this.SelectedIndex - num, 0), true);
					if (this.DroppedDown && this.SelectedIndex < this.listbox_ctrl.FirstVisibleItem())
					{
						this.listbox_ctrl.Scroll(this.SelectedIndex - this.listbox_ctrl.FirstVisibleItem());
						return;
					}
					break;
				}
				case Keys.PageDown:
				{
					if (this.SelectedIndex == -1)
					{
						this.SelectedIndex = 0;
						if (this.dropdown_style != ComboBoxStyle.Simple)
						{
							return;
						}
					}
					int num = ((this.listbox_ctrl == null) ? (this.MaxDropDownItems - 1) : (this.listbox_ctrl.page_size - 1));
					if (num < 1)
					{
						num = 1;
					}
					this.SetSelectedIndex(Math.Min(this.SelectedIndex + num, this.Items.Count - 1), true);
					if (this.DroppedDown && this.SelectedIndex >= this.listbox_ctrl.LastVisibleItem())
					{
						this.listbox_ctrl.Scroll(this.SelectedIndex - this.listbox_ctrl.LastVisibleItem() + 1);
						return;
					}
					break;
				}
				case Keys.End:
					if (this.dropdown_style == ComboBoxStyle.DropDownList)
					{
						this.SetSelectedIndex(this.Items.Count - 1, true);
						if (this.DroppedDown && this.SelectedIndex >= this.listbox_ctrl.LastVisibleItem())
						{
							this.listbox_ctrl.Scroll(this.SelectedIndex - this.listbox_ctrl.LastVisibleItem() + 1);
						}
					}
					break;
				case Keys.Home:
					if (this.dropdown_style == ComboBoxStyle.DropDownList)
					{
						this.SelectedIndex = 0;
						if (this.DroppedDown && this.SelectedIndex < this.listbox_ctrl.FirstVisibleItem())
						{
							this.listbox_ctrl.Scroll(this.SelectedIndex - this.listbox_ctrl.FirstVisibleItem());
							return;
						}
					}
					break;
				case Keys.Left:
				case Keys.Right:
					break;
				case Keys.Up:
					this.FindMatchOrSetIndex(Math.Max(this.SelectedIndex - 1, 0));
					if (this.DroppedDown && this.SelectedIndex < this.listbox_ctrl.FirstVisibleItem())
					{
						this.listbox_ctrl.Scroll(this.SelectedIndex - this.listbox_ctrl.FirstVisibleItem());
						return;
					}
					break;
				case Keys.Down:
					if ((e.Modifiers & Keys.Alt) == Keys.Alt)
					{
						this.DropDownListBox();
					}
					else
					{
						this.FindMatchOrSetIndex(Math.Min(this.SelectedIndex + 1, this.Items.Count - 1));
					}
					if (this.DroppedDown && this.SelectedIndex >= this.listbox_ctrl.LastVisibleItem())
					{
						this.listbox_ctrl.Scroll(this.SelectedIndex - this.listbox_ctrl.LastVisibleItem() + 1);
						return;
					}
					break;
				default:
					return;
				}
			}
			else if (this.listbox_ctrl != null && this.listbox_ctrl.Visible)
			{
				this.DropDownListBoxFinished();
				return;
			}
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x0000729C File Offset: 0x0000549C
		private void SetSelectedIndex(int value, bool supressAutoScroll)
		{
			if (this.selected_index == value)
			{
				return;
			}
			if (value <= -2 || value >= this.Items.Count)
			{
				throw new ArgumentOutOfRangeException("SelectedIndex");
			}
			this.selected_index = value;
			if (this.dropdown_style != ComboBoxStyle.DropDownList)
			{
				if (value == -1)
				{
					this.SetControlText(string.Empty, false, supressAutoScroll);
				}
				else
				{
					this.SetControlText(base.GetItemText(this.Items[value]), false, supressAutoScroll);
				}
			}
			if (this.DropDownStyle == ComboBoxStyle.DropDownList)
			{
				base.Invalidate();
			}
			if (this.listbox_ctrl != null)
			{
				this.listbox_ctrl.HighlightedIndex = value;
			}
			this.OnSelectedValueChanged(EventArgs.Empty);
			this.OnSelectedIndexChanged(EventArgs.Empty);
			this.OnSelectedItemChanged(EventArgs.Empty);
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x00007354 File Offset: 0x00005554
		private void FindMatchOrSetIndex(int index)
		{
			int num = -1;
			if (this.SelectedIndex == -1 && this.Text.Length != 0)
			{
				num = this.FindStringCaseInsensitive(this.Text);
			}
			if (num != -1)
			{
				this.SetSelectedIndex(num, true);
				return;
			}
			this.SetSelectedIndex(index, true);
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x0000739C File Offset: 0x0000559C
		private void OnMouseDownCB(object sender, MouseEventArgs e)
		{
			Rectangle clientRectangle;
			if (this.DropDownStyle == ComboBoxStyle.DropDownList)
			{
				clientRectangle = base.ClientRectangle;
			}
			else
			{
				clientRectangle = this.button_area;
			}
			if (clientRectangle.Contains(e.X, e.Y))
			{
				if (this.Items.Count > 0)
				{
					this.DropDownListBox();
				}
				else
				{
					this.button_state = ButtonState.Pushed;
					this.OnDropDown(EventArgs.Empty);
				}
				base.Invalidate(this.button_area);
				base.Update();
			}
			base.Capture = true;
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x0000741C File Offset: 0x0000561C
		private void OnMouseEnter(object sender, EventArgs e)
		{
			if (ThemeEngine.Current.CombBoxBackgroundHasHotElementStyle(this))
			{
				base.Invalidate();
			}
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x00007431 File Offset: 0x00005631
		private void OnMouseLeave(object sender, EventArgs e)
		{
			if (ThemeEngine.Current.CombBoxBackgroundHasHotElementStyle(this))
			{
				this.drop_down_button_entered = false;
				base.Invalidate();
				return;
			}
			if (this.show_dropdown_button)
			{
				this.DropDownButtonEntered = false;
			}
		}

		// Token: 0x060001BA RID: 442 RVA: 0x00007460 File Offset: 0x00005660
		private void OnMouseMoveCB(object sender, MouseEventArgs e)
		{
			if (this.show_dropdown_button && !this.dropped_down)
			{
				this.DropDownButtonEntered = this.button_area.Contains(e.Location);
			}
			if (this.DropDownStyle == ComboBoxStyle.Simple)
			{
				return;
			}
			if (this.listbox_ctrl != null && this.listbox_ctrl.Visible)
			{
				Point point = this.listbox_ctrl.PointToClient(Control.MousePosition);
				if (this.listbox_ctrl.ClientRectangle.Contains(point))
				{
					this.listbox_ctrl.Capture = true;
				}
			}
		}

		// Token: 0x060001BB RID: 443 RVA: 0x000074E5 File Offset: 0x000056E5
		private void OnMouseUpCB(object sender, MouseEventArgs e)
		{
			base.Capture = false;
			this.button_state = ButtonState.Normal;
			base.Invalidate(this.button_area);
			this.OnClick(EventArgs.Empty);
			if (this.dropped_down)
			{
				this.listbox_ctrl.Capture = true;
			}
		}

		// Token: 0x060001BC RID: 444 RVA: 0x00007520 File Offset: 0x00005720
		private void OnMouseWheelCB(object sender, MouseEventArgs me)
		{
			if (this.Items.Count == 0)
			{
				return;
			}
			if (this.listbox_ctrl != null && this.listbox_ctrl.Visible)
			{
				int num = me.Delta / 120 * SystemInformation.MouseWheelScrollLines;
				this.listbox_ctrl.Scroll(-num);
				return;
			}
			int num2 = me.Delta / 120;
			int num3 = this.SelectedIndex - num2;
			if (num3 < 0)
			{
				num3 = 0;
			}
			else if (num3 >= this.Items.Count)
			{
				num3 = this.Items.Count - 1;
			}
			this.SelectedIndex = num3;
		}

		// Token: 0x060001BD RID: 445 RVA: 0x000075AC File Offset: 0x000057AC
		private MouseEventArgs TranslateMouseEventArgs(MouseEventArgs args)
		{
			Point point = base.PointToClient(Control.MousePosition);
			return new MouseEventArgs(args.Button, args.Clicks, point.X, point.Y, args.Delta);
		}

		// Token: 0x060001BE RID: 446 RVA: 0x000075EA File Offset: 0x000057EA
		internal override void OnPaintInternal(PaintEventArgs pevent)
		{
			if (this.suspend_ctrlupdate)
			{
				return;
			}
			this.Draw(base.ClientRectangle, pevent.Graphics);
		}

		// Token: 0x060001BF RID: 447 RVA: 0x00007607 File Offset: 0x00005807
		private void OnTextBoxClick(object sender, EventArgs e)
		{
			this.OnClick(e);
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x00007610 File Offset: 0x00005810
		private void OnTextChangedEdit(object sender, EventArgs e)
		{
			if (!this.process_textchanged_event)
			{
				return;
			}
			int num = this.FindStringCaseInsensitive(this.textbox_ctrl.Text);
			if (num == -1)
			{
				this.OnTextChanged(EventArgs.Empty);
				return;
			}
			if (this.listbox_ctrl != null && this.process_texchanged_autoscroll)
			{
				this.listbox_ctrl.EnsureTop(num);
			}
			base.Text = this.textbox_ctrl.Text;
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x00007675 File Offset: 0x00005875
		private void OnTextKeyPress(object sender, KeyPressEventArgs e)
		{
			this.selected_index = -1;
			if (this.listbox_ctrl != null)
			{
				this.listbox_ctrl.HighlightedIndex = -1;
			}
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x00007692 File Offset: 0x00005892
		internal void SetControlText(string s, bool suppressTextChanged)
		{
			this.SetControlText(s, suppressTextChanged, false);
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x0000769D File Offset: 0x0000589D
		internal void SetControlText(string s, bool suppressTextChanged, bool supressAutoScroll)
		{
			if (suppressTextChanged)
			{
				this.process_textchanged_event = false;
			}
			if (supressAutoScroll)
			{
				this.process_texchanged_autoscroll = false;
			}
			this.textbox_ctrl.Text = s;
			this.textbox_ctrl.SelectAll();
			this.process_textchanged_event = true;
			this.process_texchanged_autoscroll = true;
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x000076D8 File Offset: 0x000058D8
		private void UpdateComboBoxBounds()
		{
			if (this.requested_height == -1)
			{
				return;
			}
			int num = this.requested_height;
			base.SetBounds(this.bounds.X, this.bounds.Y, this.bounds.Width, this.SnapHeight(this.requested_height), BoundsSpecified.Height);
			this.requested_height = num;
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x00007734 File Offset: 0x00005934
		private int SnapHeight(int height)
		{
			if (this.DropDownStyle == ComboBoxStyle.Simple && height > this.PreferredHeight)
			{
				if (this.IntegralHeight)
				{
					int height2 = ThemeEngine.Current.Border3DSize.Height;
					int num = height - this.PreferredHeight - 2 - height2 * 2;
					if (num > this.ItemHeight)
					{
						int num2 = num % this.ItemHeight;
						height -= num2;
					}
					else if (num < this.ItemHeight)
					{
						height = this.PreferredHeight;
					}
				}
			}
			else
			{
				height = this.PreferredHeight;
			}
			return height;
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x000077B1 File Offset: 0x000059B1
		private void UpdatedItems()
		{
			if (this.listbox_ctrl != null)
			{
				this.listbox_ctrl.UpdateLastVisibleItem();
				this.listbox_ctrl.CalcListBoxArea();
				this.listbox_ctrl.Refresh();
			}
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x000077DC File Offset: 0x000059DC
		// Note: this type is marked as 'beforefieldinit'.
		static ComboBox()
		{
			ComboBox.SelectedIndexChangedEvent = new object();
			ComboBox.SelectionChangeCommittedEvent = new object();
			ComboBox.DropDownClosedEvent = new object();
			ComboBox.TextUpdateEvent = new object();
		}

		// Token: 0x04000147 RID: 327
		private DrawMode draw_mode;

		// Token: 0x04000148 RID: 328
		private ComboBoxStyle dropdown_style;

		// Token: 0x04000149 RID: 329
		private int dropdown_width = -1;

		// Token: 0x0400014A RID: 330
		private int selected_index = -1;

		// Token: 0x0400014B RID: 331
		private ComboBox.ObjectCollection items;

		// Token: 0x0400014C RID: 332
		private bool suspend_ctrlupdate;

		// Token: 0x0400014D RID: 333
		private int maxdrop_items = 8;

		// Token: 0x0400014E RID: 334
		private bool integral_height = true;

		// Token: 0x0400014F RID: 335
		private bool sorted;

		// Token: 0x04000150 RID: 336
		private ComboBox.ComboListBox listbox_ctrl;

		// Token: 0x04000151 RID: 337
		private ComboBox.ComboTextBox textbox_ctrl;

		// Token: 0x04000152 RID: 338
		private bool process_textchanged_event = true;

		// Token: 0x04000153 RID: 339
		private bool process_texchanged_autoscroll = true;

		// Token: 0x04000154 RID: 340
		private bool item_height_specified;

		// Token: 0x04000155 RID: 341
		private int item_height;

		// Token: 0x04000156 RID: 342
		private int requested_height = -1;

		// Token: 0x04000157 RID: 343
		private Hashtable item_heights;

		// Token: 0x04000158 RID: 344
		private bool show_dropdown_button;

		// Token: 0x04000159 RID: 345
		private ButtonState button_state;

		// Token: 0x0400015A RID: 346
		private bool dropped_down;

		// Token: 0x0400015B RID: 347
		private Rectangle text_area;

		// Token: 0x0400015C RID: 348
		private Rectangle button_area;

		// Token: 0x0400015D RID: 349
		private Rectangle listbox_area;

		// Token: 0x0400015E RID: 350
		private bool drop_down_button_entered;

		// Token: 0x0400015F RID: 351
		private AutoCompleteStringCollection auto_complete_custom_source;

		// Token: 0x04000160 RID: 352
		private AutoCompleteMode auto_complete_mode;

		// Token: 0x04000161 RID: 353
		private AutoCompleteSource auto_complete_source = AutoCompleteSource.None;

		// Token: 0x04000162 RID: 354
		private FlatStyle flat_style;

		// Token: 0x04000163 RID: 355
		private int drop_down_height;

		// Token: 0x04000164 RID: 356
		private static object DrawItemEvent = new object();

		// Token: 0x04000165 RID: 357
		private static object DropDownEvent = new object();

		// Token: 0x04000166 RID: 358
		private static object DropDownStyleChangedEvent = new object();

		// Token: 0x04000167 RID: 359
		private static object MeasureItemEvent = new object();

		// Token: 0x04000169 RID: 361
		private static object SelectionChangeCommittedEvent;

		// Token: 0x0400016A RID: 362
		private static object DropDownClosedEvent;

		// Token: 0x0400016B RID: 363
		private static object TextUpdateEvent;

		/// <summary>Represents the collection of items in a <see cref="T:System.Windows.Forms.ComboBox" />.</summary>
		// Token: 0x02000042 RID: 66
		[ListBindable(false)]
		public class ObjectCollection : IList, ICollection, IEnumerable
		{
			// Token: 0x060001C8 RID: 456 RVA: 0x0000783C File Offset: 0x00005A3C
			internal void OnUIACollectionChangedEvent(CollectionChangeEventArgs args)
			{
				CollectionChangeEventHandler collectionChangeEventHandler = (CollectionChangeEventHandler)this.owner.Events[ComboBox.ObjectCollection.UIACollectionChangedEvent];
				if (collectionChangeEventHandler != null)
				{
					collectionChangeEventHandler(this.owner, args);
				}
			}

			/// <summary>Initializes a new instance of <see cref="T:System.Windows.Forms.ComboBox.ObjectCollection" />.</summary>
			/// <param name="owner">The <see cref="T:System.Windows.Forms.ComboBox" /> that owns this object collection. </param>
			// Token: 0x060001C9 RID: 457 RVA: 0x00007874 File Offset: 0x00005A74
			public ObjectCollection(ComboBox owner)
			{
				this.owner = owner;
			}

			/// <summary>Gets the number of items in the collection.</summary>
			/// <returns>The number of items in the collection.</returns>
			// Token: 0x17000081 RID: 129
			// (get) Token: 0x060001CA RID: 458 RVA: 0x0000788E File Offset: 0x00005A8E
			public int Count
			{
				get
				{
					return this.object_items.Count;
				}
			}

			/// <summary>Gets a value indicating whether this collection can be modified.</summary>
			/// <returns>Always false.</returns>
			// Token: 0x17000082 RID: 130
			// (get) Token: 0x060001CB RID: 459 RVA: 0x00002D70 File Offset: 0x00000F70
			public bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			/// <summary>Retrieves the item at the specified index within the collection.</summary>
			/// <returns>An object representing the item located at the specified index within the collection.</returns>
			/// <param name="index">The index of the item in the collection to retrieve. </param>
			/// <exception cref="T:System.ArgumentOutOfRangeException">The index was less than zero.-or- The <paramref name="index" /> was greater of equal to the count of items in the collection. </exception>
			// Token: 0x17000083 RID: 131
			[Browsable(false)]
			[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
			public virtual object this[int index]
			{
				get
				{
					if (index < 0 || index >= this.Count)
					{
						throw new ArgumentOutOfRangeException("index");
					}
					return this.object_items[index];
				}
				set
				{
					if (index < 0 || index >= this.Count)
					{
						throw new ArgumentOutOfRangeException("index");
					}
					if (value == null)
					{
						throw new ArgumentNullException("value");
					}
					this.OnUIACollectionChangedEvent(new CollectionChangeEventArgs(CollectionChangeAction.Remove, this.object_items[index]));
					this.object_items[index] = value;
					this.OnUIACollectionChangedEvent(new CollectionChangeEventArgs(CollectionChangeAction.Add, value));
					if (this.owner.listbox_ctrl != null)
					{
						this.owner.listbox_ctrl.InvalidateItem(index);
					}
					if (index == this.owner.SelectedIndex)
					{
						if (this.owner.textbox_ctrl == null)
						{
							this.owner.Refresh();
							return;
						}
						this.owner.textbox_ctrl.Text = value.ToString();
						this.owner.textbox_ctrl.SelectAll();
					}
				}
			}

			/// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.IsSynchronized" />.</summary>
			/// <returns>false in all cases.</returns>
			// Token: 0x17000084 RID: 132
			// (get) Token: 0x060001CE RID: 462 RVA: 0x00002D70 File Offset: 0x00000F70
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			/// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.SyncRoot" />.</summary>
			/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Windows.Forms.ComboBox.ObjectCollection" />.</returns>
			// Token: 0x17000085 RID: 133
			// (get) Token: 0x060001CF RID: 463 RVA: 0x00002F7A File Offset: 0x0000117A
			object ICollection.SyncRoot
			{
				get
				{
					return this;
				}
			}

			/// <summary>For a description of this member, see <see cref="P:System.Collections.IList.IsFixedSize" />.</summary>
			/// <returns>false in all cases.</returns>
			// Token: 0x17000086 RID: 134
			// (get) Token: 0x060001D0 RID: 464 RVA: 0x00002D70 File Offset: 0x00000F70
			bool IList.IsFixedSize
			{
				get
				{
					return false;
				}
			}

			/// <summary>Adds an item to the list of items for a <see cref="T:System.Windows.Forms.ComboBox" />.</summary>
			/// <returns>The zero-based index of the item in the collection.</returns>
			/// <param name="item">An object representing the item to add to the collection. </param>
			/// <exception cref="T:System.ArgumentNullException">The <paramref name="item" /> parameter was null. </exception>
			// Token: 0x060001D1 RID: 465 RVA: 0x00007994 File Offset: 0x00005B94
			public int Add(object item)
			{
				int num = this.AddItem(item, false);
				this.owner.UpdatedItems();
				return num;
			}

			/// <summary>Removes all items from the <see cref="T:System.Windows.Forms.ComboBox" />.</summary>
			// Token: 0x060001D2 RID: 466 RVA: 0x000079A9 File Offset: 0x00005BA9
			public void Clear()
			{
				this.owner.selected_index = -1;
				this.object_items.Clear();
				this.owner.UpdatedItems();
				this.owner.Refresh();
				this.OnUIACollectionChangedEvent(new CollectionChangeEventArgs(CollectionChangeAction.Refresh, null));
			}

			/// <summary>Determines if the specified item is located within the collection.</summary>
			/// <returns>true if the item is located within the collection; otherwise, false.</returns>
			/// <param name="value">An object representing the item to locate in the collection. </param>
			// Token: 0x060001D3 RID: 467 RVA: 0x000079E5 File Offset: 0x00005BE5
			public bool Contains(object value)
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				return this.object_items.Contains(value);
			}

			/// <summary>Copies the entire collection into an existing array of objects at a specified location within the array.</summary>
			/// <param name="destination">The object array to copy the collection to. </param>
			/// <param name="arrayIndex">The location in the destination array to copy the collection to. </param>
			// Token: 0x060001D4 RID: 468 RVA: 0x00007A01 File Offset: 0x00005C01
			public void CopyTo(object[] destination, int arrayIndex)
			{
				this.object_items.CopyTo(destination, arrayIndex);
			}

			/// <summary>For a description of this member, see <see cref="M:System.Collections.ICollection.CopyTo(System.Array,System.Int32)" />.</summary>
			/// <param name="destination">The one-dimensional array that is the destination of the elements copied from the collection. The array must have zero-based indexing.</param>
			/// <param name="index">The zero-based index in the array at which copying begins.</param>
			// Token: 0x060001D5 RID: 469 RVA: 0x00007A01 File Offset: 0x00005C01
			void ICollection.CopyTo(Array destination, int index)
			{
				this.object_items.CopyTo(destination, index);
			}

			/// <summary>Returns an enumerator that can be used to iterate through the item collection.</summary>
			/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that represents the item collection.</returns>
			// Token: 0x060001D6 RID: 470 RVA: 0x00007A10 File Offset: 0x00005C10
			public IEnumerator GetEnumerator()
			{
				return this.object_items.GetEnumerator();
			}

			/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Add(System.Object)" />.</summary>
			/// <returns>The zero-based index of the item in the collection.</returns>
			/// <param name="item">An object that represents the item to add to the collection.</param>
			/// <exception cref="T:System.ArgumentNullException">The <paramref name="item" /> parameter is null.</exception>
			/// <exception cref="T:System.SystemException">There is insufficient space available to store the new item.</exception>
			// Token: 0x060001D7 RID: 471 RVA: 0x00007A1D File Offset: 0x00005C1D
			int IList.Add(object item)
			{
				return this.Add(item);
			}

			/// <summary>Retrieves the index within the collection of the specified item.</summary>
			/// <returns>The zero-based index where the item is located within the collection; otherwise, -1.</returns>
			/// <param name="value">An object representing the item to locate in the collection. </param>
			/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter was null. </exception>
			// Token: 0x060001D8 RID: 472 RVA: 0x00007A26 File Offset: 0x00005C26
			public int IndexOf(object value)
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				return this.object_items.IndexOf(value);
			}

			/// <summary>Inserts an item into the collection at the specified index.</summary>
			/// <param name="index">The zero-based index location where the item is inserted. </param>
			/// <param name="item">An object representing the item to insert. </param>
			/// <exception cref="T:System.ArgumentNullException">The <paramref name="item" /> was null. </exception>
			/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> was less than zero.-or- The <paramref name="index" /> was greater than the count of items in the collection. </exception>
			// Token: 0x060001D9 RID: 473 RVA: 0x00007A44 File Offset: 0x00005C44
			public void Insert(int index, object item)
			{
				if (index < 0 || index > this.Count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				if (item == null)
				{
					throw new ArgumentNullException("item");
				}
				this.owner.BeginUpdate();
				if (this.owner.Sorted)
				{
					this.AddItem(item, false);
				}
				else
				{
					this.object_items.Insert(index, item);
					this.OnUIACollectionChangedEvent(new CollectionChangeEventArgs(CollectionChangeAction.Add, item));
				}
				this.owner.EndUpdate();
			}

			/// <summary>Removes the specified item from the <see cref="T:System.Windows.Forms.ComboBox" />.</summary>
			/// <param name="value">The <see cref="T:System.Object" /> to remove from the list. </param>
			// Token: 0x060001DA RID: 474 RVA: 0x00007AC0 File Offset: 0x00005CC0
			public void Remove(object value)
			{
				if (value == null)
				{
					return;
				}
				int num = this.IndexOf(value);
				if (num >= 0)
				{
					this.RemoveAt(num);
				}
			}

			/// <summary>Removes an item from the <see cref="T:System.Windows.Forms.ComboBox" /> at the specified index.</summary>
			/// <param name="index">The index of the item to remove. </param>
			/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="value" /> parameter was less than zero.-or- The <paramref name="value" /> parameter was greater than or equal to the count of items in the collection. </exception>
			// Token: 0x060001DB RID: 475 RVA: 0x00007AE4 File Offset: 0x00005CE4
			public void RemoveAt(int index)
			{
				if (index < 0 || index >= this.Count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				if (index < this.owner.SelectedIndex)
				{
					ComboBox comboBox = this.owner;
					int num = comboBox.SelectedIndex - 1;
					comboBox.SelectedIndex = num;
				}
				else if (index == this.owner.SelectedIndex)
				{
					this.owner.SelectedIndex = -1;
				}
				object obj = this.object_items[index];
				this.object_items.RemoveAt(index);
				this.owner.UpdatedItems();
				this.OnUIACollectionChangedEvent(new CollectionChangeEventArgs(CollectionChangeAction.Remove, obj));
			}

			// Token: 0x060001DC RID: 476 RVA: 0x00007B7C File Offset: 0x00005D7C
			private int AddItem(object item, bool suspend)
			{
				if (item == null)
				{
					throw new ArgumentNullException("item");
				}
				if (this.owner.Sorted && !suspend)
				{
					int num = 0;
					foreach (object obj in this.object_items)
					{
						if (string.Compare(item.ToString(), obj.ToString()) < 0)
						{
							this.object_items.Insert(num, item);
							if (num <= this.owner.selected_index && this.owner.IsHandleCreated)
							{
								this.owner.selected_index++;
							}
							this.OnUIACollectionChangedEvent(new CollectionChangeEventArgs(CollectionChangeAction.Add, item));
							return num;
						}
						num++;
					}
				}
				this.object_items.Add(item);
				this.OnUIACollectionChangedEvent(new CollectionChangeEventArgs(CollectionChangeAction.Add, item));
				return this.object_items.Count - 1;
			}

			// Token: 0x060001DD RID: 477 RVA: 0x00007C80 File Offset: 0x00005E80
			internal void AddRange(IList items)
			{
				foreach (object obj in items)
				{
					this.AddItem(obj, false);
				}
				if (this.owner.sorted)
				{
					this.Sort();
				}
				this.owner.UpdatedItems();
			}

			// Token: 0x060001DE RID: 478 RVA: 0x00007CF0 File Offset: 0x00005EF0
			internal void Sort()
			{
				if (this.object_items.Count > 0 && this.object_items[0] is IComparer)
				{
					this.object_items.Sort();
					return;
				}
				this.object_items.Sort(new ComboBox.ObjectCollection.ObjectComparer(this.owner));
			}

			// Token: 0x0400016C RID: 364
			private ComboBox owner;

			// Token: 0x0400016D RID: 365
			internal ArrayList object_items = new ArrayList();

			// Token: 0x0400016E RID: 366
			private static object UIACollectionChangedEvent = new object();

			// Token: 0x02000043 RID: 67
			private class ObjectComparer : IComparer
			{
				// Token: 0x060001E0 RID: 480 RVA: 0x00007D4C File Offset: 0x00005F4C
				public ObjectComparer(ListControl owner)
				{
					this.owner = owner;
				}

				// Token: 0x060001E1 RID: 481 RVA: 0x00007D5B File Offset: 0x00005F5B
				public int Compare(object x, object y)
				{
					return string.Compare(this.owner.GetItemText(x), this.owner.GetItemText(y));
				}

				// Token: 0x0400016F RID: 367
				private ListControl owner;
			}
		}

		// Token: 0x02000044 RID: 68
		internal class ComboTextBox : TextBox
		{
			// Token: 0x060001E2 RID: 482 RVA: 0x00007D7A File Offset: 0x00005F7A
			public ComboTextBox(ComboBox owner)
			{
				this.owner = owner;
				base.ShowSelection = false;
				owner.EnabledChanged += this.OwnerEnabledChangedHandler;
				owner.LostFocus += this.OwnerLostFocusHandler;
			}

			// Token: 0x060001E3 RID: 483 RVA: 0x00007DB4 File Offset: 0x00005FB4
			private void OwnerEnabledChangedHandler(object o, EventArgs args)
			{
				base.ShowSelection = this.owner.Focused && this.owner.Enabled;
			}

			// Token: 0x060001E4 RID: 484 RVA: 0x00007DD7 File Offset: 0x00005FD7
			private void OwnerLostFocusHandler(object o, EventArgs args)
			{
				if (base.IsAutoCompleteAvailable)
				{
					this.owner.Text = this.Text;
				}
			}

			// Token: 0x060001E5 RID: 485 RVA: 0x00007DF2 File Offset: 0x00005FF2
			protected override void OnKeyDown(KeyEventArgs args)
			{
				if (args.KeyCode == Keys.Return && base.IsAutoCompleteAvailable)
				{
					this.owner.Text = this.Text;
				}
				base.OnKeyDown(args);
			}

			// Token: 0x060001E6 RID: 486 RVA: 0x00007E1E File Offset: 0x0000601E
			internal override void OnAutoCompleteValueSelected(EventArgs args)
			{
				base.OnAutoCompleteValueSelected(args);
				this.owner.Text = this.Text;
			}

			// Token: 0x060001E7 RID: 487 RVA: 0x00007E38 File Offset: 0x00006038
			internal void SetSelectable(bool selectable)
			{
				base.SetStyle(ControlStyles.Selectable, selectable);
			}

			// Token: 0x060001E8 RID: 488 RVA: 0x00007E46 File Offset: 0x00006046
			internal void ActivateCaret(bool active)
			{
				if (active)
				{
					this.document.CaretHasFocus();
					return;
				}
				this.document.CaretLostFocus();
			}

			// Token: 0x060001E9 RID: 489 RVA: 0x00007E62 File Offset: 0x00006062
			internal override void OnTextUpdate()
			{
				base.OnTextUpdate();
				this.owner.OnTextUpdate(EventArgs.Empty);
			}

			// Token: 0x060001EA RID: 490 RVA: 0x00007E7A File Offset: 0x0000607A
			protected override void OnGotFocus(EventArgs e)
			{
				this.owner.Select(false, true);
			}

			// Token: 0x060001EB RID: 491 RVA: 0x00007E7A File Offset: 0x0000607A
			protected override void OnLostFocus(EventArgs e)
			{
				this.owner.Select(false, true);
			}

			// Token: 0x060001EC RID: 492 RVA: 0x00007E89 File Offset: 0x00006089
			protected override void OnMouseDown(MouseEventArgs e)
			{
				base.OnMouseDown(e);
				this.owner.OnMouseDown(this.owner.TranslateMouseEventArgs(e));
			}

			// Token: 0x060001ED RID: 493 RVA: 0x00007EA9 File Offset: 0x000060A9
			protected override void OnMouseUp(MouseEventArgs e)
			{
				base.OnMouseUp(e);
				this.owner.OnMouseUp(this.owner.TranslateMouseEventArgs(e));
			}

			// Token: 0x060001EE RID: 494 RVA: 0x00007EC9 File Offset: 0x000060C9
			protected override void OnMouseClick(MouseEventArgs e)
			{
				base.OnMouseClick(e);
				this.owner.OnMouseClick(this.owner.TranslateMouseEventArgs(e));
			}

			// Token: 0x060001EF RID: 495 RVA: 0x00007EE9 File Offset: 0x000060E9
			protected override void OnMouseDoubleClick(MouseEventArgs e)
			{
				base.OnMouseDoubleClick(e);
				this.owner.OnMouseDoubleClick(this.owner.TranslateMouseEventArgs(e));
			}

			// Token: 0x17000087 RID: 135
			// (get) Token: 0x060001F0 RID: 496 RVA: 0x00007F09 File Offset: 0x00006109
			public override bool Focused
			{
				get
				{
					return this.owner.Focused;
				}
			}

			// Token: 0x060001F1 RID: 497 RVA: 0x00007F16 File Offset: 0x00006116
			protected override void Dispose(bool disposing)
			{
				if (disposing)
				{
					this.owner.EnabledChanged -= this.OwnerEnabledChangedHandler;
					this.owner.LostFocus -= this.OwnerLostFocusHandler;
				}
				base.Dispose(disposing);
			}

			// Token: 0x17000088 RID: 136
			// (get) Token: 0x060001F2 RID: 498 RVA: 0x00002D70 File Offset: 0x00000F70
			internal override bool ActivateOnShow
			{
				get
				{
					return false;
				}
			}

			// Token: 0x04000170 RID: 368
			private ComboBox owner;
		}

		// Token: 0x02000045 RID: 69
		internal class ComboListBox : Control
		{
			// Token: 0x060001F3 RID: 499 RVA: 0x00007F50 File Offset: 0x00006150
			public ComboListBox(ComboBox owner)
			{
				this.owner = owner;
				this.top_item = 0;
				this.last_item = 0;
				this.page_size = 0;
				base.MouseWheel += this.OnMouseWheelCLB;
				base.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
				base.SetStyle(ControlStyles.Opaque | ControlStyles.ResizeRedraw, true);
				this.is_visible = false;
				if (owner.DropDownStyle == ComboBoxStyle.Simple)
				{
					base.InternalBorderStyle = BorderStyle.Fixed3D;
					return;
				}
				base.InternalBorderStyle = BorderStyle.FixedSingle;
			}

			// Token: 0x17000089 RID: 137
			// (get) Token: 0x060001F4 RID: 500 RVA: 0x00007FCC File Offset: 0x000061CC
			protected override CreateParams CreateParams
			{
				get
				{
					CreateParams createParams = base.CreateParams;
					if (this.owner == null || this.owner.DropDownStyle == ComboBoxStyle.Simple)
					{
						return createParams;
					}
					createParams.Style ^= 1073741824;
					createParams.Style ^= 268435456;
					createParams.Style |= int.MinValue;
					createParams.ExStyle |= 136;
					return createParams;
				}
			}

			// Token: 0x1700008A RID: 138
			// (get) Token: 0x060001F5 RID: 501 RVA: 0x00006947 File Offset: 0x00004B47
			// (set) Token: 0x060001F6 RID: 502 RVA: 0x0000493C File Offset: 0x00002B3C
			internal override bool InternalCapture
			{
				get
				{
					return base.Capture;
				}
				set
				{
				}
			}

			// Token: 0x1700008B RID: 139
			// (get) Token: 0x060001F7 RID: 503 RVA: 0x00002D70 File Offset: 0x00000F70
			internal override bool ActivateOnShow
			{
				get
				{
					return false;
				}
			}

			// Token: 0x060001F8 RID: 504 RVA: 0x00008040 File Offset: 0x00006240
			internal void CalcListBoxArea()
			{
				int num;
				int num2;
				bool flag;
				if (this.owner.DropDownStyle == ComboBoxStyle.Simple)
				{
					Rectangle listbox_area = this.owner.listbox_area;
					num = listbox_area.Width;
					num2 = listbox_area.Height;
					flag = this.owner.Items.Count * this.owner.ItemHeight > num2;
					if (num2 <= 0 || num <= 0)
					{
						return;
					}
				}
				else
				{
					num = this.owner.DropDownWidth;
					int num3 = ((this.owner.Items.Count <= this.owner.MaxDropDownItems) ? this.owner.Items.Count : this.owner.MaxDropDownItems);
					if (this.owner.DrawMode == DrawMode.OwnerDrawVariable)
					{
						num2 = 0;
						for (int i = 0; i < num3; i++)
						{
							num2 += this.owner.GetItemHeight(i);
						}
						flag = this.owner.Items.Count > this.owner.MaxDropDownItems;
					}
					else if (this.owner.DropDownHeight == 106)
					{
						num2 = this.owner.ItemHeight * num3;
						flag = this.owner.Items.Count > this.owner.MaxDropDownItems;
					}
					else
					{
						num2 = this.owner.DropDownHeight;
						flag = this.owner.Items.Count * this.owner.ItemHeight > num2;
					}
				}
				this.page_size = Math.Max(num2 / this.owner.ItemHeight, 1);
				ComboBoxStyle dropDownStyle = this.owner.DropDownStyle;
				if (!flag)
				{
					if (this.vscrollbar_ctrl != null)
					{
						this.vscrollbar_ctrl.Visible = false;
					}
					if (dropDownStyle != ComboBoxStyle.Simple)
					{
						num2 = this.owner.ItemHeight * this.owner.items.Count;
					}
				}
				else
				{
					if (this.vscrollbar_ctrl == null)
					{
						this.vscrollbar_ctrl = new ComboBox.ComboListBox.VScrollBarLB();
						this.vscrollbar_ctrl.Minimum = 0;
						this.vscrollbar_ctrl.SmallChange = 1;
						this.vscrollbar_ctrl.LargeChange = 1;
						this.vscrollbar_ctrl.Maximum = 0;
						this.vscrollbar_ctrl.ValueChanged += this.VerticalScrollEvent;
						base.Controls.AddImplicit(this.vscrollbar_ctrl);
					}
					this.vscrollbar_ctrl.Dock = DockStyle.Right;
					this.vscrollbar_ctrl.Maximum = this.owner.Items.Count - 1;
					int num4 = this.page_size;
					if (num4 < 1)
					{
						num4 = 1;
					}
					this.vscrollbar_ctrl.LargeChange = num4;
					this.vscrollbar_ctrl.Visible = true;
					int num5 = this.HighlightedIndex;
					if (num5 > 0)
					{
						num5 = Math.Min(num5, this.vscrollbar_ctrl.Maximum);
						this.vscrollbar_ctrl.Value = num5;
					}
				}
				Hwnd.Borders borderWidth = Hwnd.GetBorderWidth(this.CreateParams);
				Size size = ((dropDownStyle == ComboBoxStyle.Simple) ? new Size(0, 0) : new Size(borderWidth.top + borderWidth.bottom, borderWidth.left + borderWidth.right));
				base.Size = new Size(num, num2 + size.Height);
				this.textarea_drawable = new Rectangle(base.ClientRectangle.Location, new Size(num - size.Width, num2));
				if (this.vscrollbar_ctrl != null && flag)
				{
					this.textarea_drawable.Width = this.textarea_drawable.Width - this.vscrollbar_ctrl.Width;
				}
				this.last_item = this.LastVisibleItem();
			}

			// Token: 0x060001F9 RID: 505 RVA: 0x000083A8 File Offset: 0x000065A8
			private void Draw(Rectangle clip, Graphics dc)
			{
				dc.FillRectangle(ThemeEngine.Current.ResPool.GetSolidBrush(this.owner.BackColor), clip);
				if (this.owner.Items.Count > 0)
				{
					for (int i = this.top_item; i <= this.last_item; i++)
					{
						Rectangle itemDisplayRectangle = this.GetItemDisplayRectangle(i, this.top_item);
						if (clip.IntersectsWith(itemDisplayRectangle))
						{
							DrawItemState drawItemState = DrawItemState.None;
							Color color = this.owner.BackColor;
							Color color2 = this.owner.ForeColor;
							if (i == this.HighlightedIndex)
							{
								drawItemState |= DrawItemState.Selected;
								color = SystemColors.Highlight;
								color2 = SystemColors.HighlightText;
								if (this.owner.DropDownStyle == ComboBoxStyle.DropDownList)
								{
									drawItemState |= DrawItemState.Focus;
								}
							}
							this.owner.HandleDrawItem(new DrawItemEventArgs(dc, this.owner.Font, itemDisplayRectangle, i, drawItemState, color2, color));
						}
					}
				}
			}

			// Token: 0x1700008C RID: 140
			// (get) Token: 0x060001FA RID: 506 RVA: 0x0000848A File Offset: 0x0000668A
			// (set) Token: 0x060001FB RID: 507 RVA: 0x00008494 File Offset: 0x00006694
			public int HighlightedIndex
			{
				get
				{
					return this.highlighted_index;
				}
				set
				{
					if (this.highlighted_index == value)
					{
						return;
					}
					if (this.highlighted_index != -1 && this.highlighted_index < this.owner.Items.Count)
					{
						base.Invalidate(this.GetItemDisplayRectangle(this.highlighted_index, this.top_item));
					}
					this.highlighted_index = value;
					if (this.highlighted_index != -1)
					{
						base.Invalidate(this.GetItemDisplayRectangle(this.highlighted_index, this.top_item));
					}
				}
			}

			// Token: 0x060001FC RID: 508 RVA: 0x0000850C File Offset: 0x0000670C
			private Rectangle GetItemDisplayRectangle(int index, int top_index)
			{
				if (index < 0 || index >= this.owner.Items.Count)
				{
					throw new ArgumentOutOfRangeException("GetItemRectangle index out of range.");
				}
				Rectangle rectangle = default(Rectangle);
				int itemHeight = this.owner.GetItemHeight(index);
				rectangle.X = 0;
				rectangle.Width = this.textarea_drawable.Width;
				if (this.owner.DrawMode == DrawMode.OwnerDrawVariable)
				{
					rectangle.Y = 0;
					for (int i = top_index; i < index; i++)
					{
						rectangle.Y += this.owner.GetItemHeight(i);
					}
				}
				else
				{
					rectangle.Y = itemHeight * (index - top_index);
				}
				rectangle.Height = itemHeight;
				return rectangle;
			}

			// Token: 0x060001FD RID: 509 RVA: 0x000085BD File Offset: 0x000067BD
			public void HideWindow()
			{
				if (this.owner.DropDownStyle == ComboBoxStyle.Simple)
				{
					return;
				}
				base.Capture = false;
				base.Hide();
				this.owner.DropDownListBoxFinished();
			}

			// Token: 0x060001FE RID: 510 RVA: 0x000085E8 File Offset: 0x000067E8
			private int IndexFromPointDisplayRectangle(int x, int y)
			{
				for (int i = this.top_item; i <= this.last_item; i++)
				{
					if (this.GetItemDisplayRectangle(i, this.top_item).Contains(x, y))
					{
						return i;
					}
				}
				return -1;
			}

			// Token: 0x060001FF RID: 511 RVA: 0x00008627 File Offset: 0x00006827
			public void InvalidateItem(int index)
			{
				if (base.Visible)
				{
					base.Invalidate(this.GetItemDisplayRectangle(index, this.top_item));
				}
			}

			// Token: 0x06000200 RID: 512 RVA: 0x00008644 File Offset: 0x00006844
			public int LastVisibleItem()
			{
				int num = this.textarea_drawable.Y + this.textarea_drawable.Height;
				int i;
				for (i = this.top_item; i < this.owner.Items.Count; i++)
				{
					Rectangle itemDisplayRectangle = this.GetItemDisplayRectangle(i, this.top_item);
					if (itemDisplayRectangle.Y + itemDisplayRectangle.Height > num)
					{
						return i;
					}
				}
				return i - 1;
			}

			// Token: 0x06000201 RID: 513 RVA: 0x000086B0 File Offset: 0x000068B0
			public int FirstVisibleItem()
			{
				return this.top_item;
			}

			// Token: 0x06000202 RID: 514 RVA: 0x000086B8 File Offset: 0x000068B8
			public void EnsureTop(int item)
			{
				if (this.owner.Items.Count == 0)
				{
					return;
				}
				if (this.vscrollbar_ctrl == null || !this.vscrollbar_ctrl.Visible)
				{
					return;
				}
				int num = this.vscrollbar_ctrl.Maximum - this.page_size + 1;
				if (item > num)
				{
					item = num;
				}
				else if (item < this.vscrollbar_ctrl.Minimum)
				{
					item = this.vscrollbar_ctrl.Minimum;
				}
				this.vscrollbar_ctrl.Value = item;
			}

			// Token: 0x1700008D RID: 141
			// (get) Token: 0x06000203 RID: 515 RVA: 0x00008734 File Offset: 0x00006934
			private bool InScrollBar
			{
				get
				{
					return this.vscrollbar_ctrl != null && this.vscrollbar_ctrl.is_visible && this.vscrollbar_ctrl.Bounds.Contains(base.PointToClient(Control.MousePosition));
				}
			}

			// Token: 0x06000204 RID: 516 RVA: 0x00008776 File Offset: 0x00006976
			protected override void OnMouseDown(MouseEventArgs e)
			{
				if (this.InScrollBar)
				{
					this.vscrollbar_ctrl.FireMouseDown(e);
					this.scrollbar_grabbed = true;
				}
			}

			// Token: 0x06000205 RID: 517 RVA: 0x00008794 File Offset: 0x00006994
			protected override void OnMouseMove(MouseEventArgs e)
			{
				if (this.owner.DropDownStyle == ComboBoxStyle.Simple)
				{
					return;
				}
				if (this.scrollbar_grabbed || (!base.Capture && this.InScrollBar))
				{
					this.vscrollbar_ctrl.FireMouseMove(e);
					return;
				}
				Point point = base.PointToClient(Control.MousePosition);
				int num = this.IndexFromPointDisplayRectangle(point.X, point.Y);
				if (num != -1)
				{
					this.HighlightedIndex = num;
				}
			}

			// Token: 0x06000206 RID: 518 RVA: 0x00008800 File Offset: 0x00006A00
			protected override void OnMouseUp(MouseEventArgs e)
			{
				int num = this.IndexFromPointDisplayRectangle(e.X, e.Y);
				if (this.scrollbar_grabbed)
				{
					this.vscrollbar_ctrl.FireMouseUp(e);
					this.scrollbar_grabbed = false;
					if (num != -1)
					{
						this.HighlightedIndex = num;
					}
					return;
				}
				if (num == -1)
				{
					this.HideWindow();
					return;
				}
				bool flag = this.owner.SelectedIndex != num;
				this.owner.SetSelectedIndex(num, true);
				this.owner.OnSelectionChangeCommitted(new EventArgs());
				if (!flag)
				{
					this.owner.OnSelectedValueChanged(EventArgs.Empty);
					this.owner.OnSelectedIndexChanged(EventArgs.Empty);
				}
				this.HideWindow();
			}

			// Token: 0x06000207 RID: 519 RVA: 0x000088A8 File Offset: 0x00006AA8
			internal override void OnPaintInternal(PaintEventArgs pevent)
			{
				this.Draw(pevent.ClipRectangle, pevent.Graphics);
			}

			// Token: 0x06000208 RID: 520 RVA: 0x000088BC File Offset: 0x00006ABC
			public bool ShowWindow()
			{
				if (this.owner.DropDownStyle == ComboBoxStyle.Simple && this.owner.Items.Count == 0)
				{
					return false;
				}
				this.HighlightedIndex = this.owner.SelectedIndex;
				this.CalcListBoxArea();
				Rectangle bounds = Screen.FromControl(this.owner).Bounds;
				if (base.Location.Y + base.Height >= bounds.Bottom)
				{
					base.Location = new Point(base.Location.X, base.Location.Y - (base.Height + this.owner.TextArea.Height));
				}
				base.Show();
				this.Refresh();
				this.owner.OnDropDown(EventArgs.Empty);
				return true;
			}

			// Token: 0x06000209 RID: 521 RVA: 0x0000898F File Offset: 0x00006B8F
			public void UpdateLastVisibleItem()
			{
				this.last_item = this.LastVisibleItem();
			}

			// Token: 0x0600020A RID: 522 RVA: 0x000089A0 File Offset: 0x00006BA0
			public void Scroll(int delta)
			{
				if (delta == 0 || this.vscrollbar_ctrl == null || !this.vscrollbar_ctrl.Visible)
				{
					return;
				}
				int num = this.vscrollbar_ctrl.Maximum - this.page_size + 1;
				int num2 = this.vscrollbar_ctrl.Value + delta;
				if (num2 > num)
				{
					num2 = num;
				}
				else if (num2 < this.vscrollbar_ctrl.Minimum)
				{
					num2 = this.vscrollbar_ctrl.Minimum;
				}
				this.vscrollbar_ctrl.Value = num2;
			}

			// Token: 0x0600020B RID: 523 RVA: 0x00008A18 File Offset: 0x00006C18
			private void OnMouseWheelCLB(object sender, MouseEventArgs me)
			{
				if (this.owner.Items.Count == 0)
				{
					return;
				}
				int num = me.Delta / 120 * SystemInformation.MouseWheelScrollLines;
				this.Scroll(-num);
			}

			// Token: 0x0600020C RID: 524 RVA: 0x00008A50 File Offset: 0x00006C50
			private void VerticalScrollEvent(object sender, EventArgs e)
			{
				if (this.top_item == this.vscrollbar_ctrl.Value)
				{
					return;
				}
				this.top_item = this.vscrollbar_ctrl.Value;
				this.UpdateLastVisibleItem();
				base.Invalidate();
			}

			// Token: 0x0600020D RID: 525 RVA: 0x00008A83 File Offset: 0x00006C83
			protected override void WndProc(ref Message m)
			{
				if (m.Msg == 7)
				{
					this.owner.Select(false, true);
				}
				base.WndProc(ref m);
			}

			// Token: 0x04000171 RID: 369
			private ComboBox owner;

			// Token: 0x04000172 RID: 370
			private ComboBox.ComboListBox.VScrollBarLB vscrollbar_ctrl;

			// Token: 0x04000173 RID: 371
			private int top_item;

			// Token: 0x04000174 RID: 372
			private int last_item;

			// Token: 0x04000175 RID: 373
			internal int page_size;

			// Token: 0x04000176 RID: 374
			private Rectangle textarea_drawable;

			// Token: 0x04000177 RID: 375
			private int highlighted_index = -1;

			// Token: 0x04000178 RID: 376
			private bool scrollbar_grabbed;

			// Token: 0x02000046 RID: 70
			private class VScrollBarLB : VScrollBar
			{
				// Token: 0x1700008E RID: 142
				// (get) Token: 0x0600020F RID: 527 RVA: 0x00006947 File Offset: 0x00004B47
				// (set) Token: 0x06000210 RID: 528 RVA: 0x0000493C File Offset: 0x00002B3C
				internal override bool InternalCapture
				{
					get
					{
						return base.Capture;
					}
					set
					{
					}
				}

				// Token: 0x06000211 RID: 529 RVA: 0x00008AAA File Offset: 0x00006CAA
				public void FireMouseDown(MouseEventArgs e)
				{
					if (!base.Visible)
					{
						return;
					}
					e = this.TranslateEvent(e);
					this.OnMouseDown(e);
				}

				// Token: 0x06000212 RID: 530 RVA: 0x00008AC5 File Offset: 0x00006CC5
				public void FireMouseUp(MouseEventArgs e)
				{
					if (!base.Visible)
					{
						return;
					}
					e = this.TranslateEvent(e);
					this.OnMouseUp(e);
				}

				// Token: 0x06000213 RID: 531 RVA: 0x00008AE0 File Offset: 0x00006CE0
				public void FireMouseMove(MouseEventArgs e)
				{
					if (!base.Visible)
					{
						return;
					}
					e = this.TranslateEvent(e);
					this.OnMouseMove(e);
				}

				// Token: 0x06000214 RID: 532 RVA: 0x00008AFC File Offset: 0x00006CFC
				private MouseEventArgs TranslateEvent(MouseEventArgs e)
				{
					Point point = base.PointToClient(Control.MousePosition);
					return new MouseEventArgs(e.Button, e.Clicks, point.X, point.Y, e.Delta);
				}
			}
		}
	}
}
