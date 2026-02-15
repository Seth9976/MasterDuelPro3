using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

namespace System.Windows.Forms
{
	/// <summary>Represents an individual item that is displayed within a <see cref="T:System.Windows.Forms.MainMenu" /> or <see cref="T:System.Windows.Forms.ContextMenu" />. Although <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> replaces and adds functionality to the <see cref="T:System.Windows.Forms.MenuItem" /> control of previous versions, <see cref="T:System.Windows.Forms.MenuItem" /> is retained for both backward compatibility and future use if you choose.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000137 RID: 311
	[DefaultProperty("Text")]
	[DefaultEvent("Click")]
	[DesignTimeVisible(false)]
	[ToolboxItem(false)]
	public class MenuItem : Menu
	{
		/// <summary>Initializes a <see cref="T:System.Windows.Forms.MenuItem" /> with a blank caption.</summary>
		// Token: 0x06000C4B RID: 3147 RVA: 0x00035E38 File Offset: 0x00034038
		public MenuItem()
			: base(null)
		{
			this.CommonConstructor(string.Empty);
			this.shortcut = Shortcut.None;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.MenuItem" /> class with a specified caption for the menu item.</summary>
		/// <param name="text">The caption for the menu item. </param>
		// Token: 0x06000C4C RID: 3148 RVA: 0x00035E53 File Offset: 0x00034053
		public MenuItem(string text)
			: base(null)
		{
			this.CommonConstructor(text);
			this.shortcut = Shortcut.None;
		}

		/// <summary>Initializes a new instance of the class with a specified caption and event handler for the <see cref="E:System.Windows.Forms.MenuItem.Click" /> event of the menu item.</summary>
		/// <param name="text">The caption for the menu item. </param>
		/// <param name="onClick">The <see cref="T:System.EventHandler" /> that handles the <see cref="E:System.Windows.Forms.MenuItem.Click" /> event for this menu item. </param>
		// Token: 0x06000C4D RID: 3149 RVA: 0x00035E6A File Offset: 0x0003406A
		public MenuItem(string text, EventHandler onClick)
			: base(null)
		{
			this.CommonConstructor(text);
			this.shortcut = Shortcut.None;
			this.Click += onClick;
		}

		// Token: 0x06000C4E RID: 3150 RVA: 0x00035E88 File Offset: 0x00034088
		private void CommonConstructor(string text)
		{
			this.defaut_item = false;
			this.separator = false;
			this.break_ = false;
			this.bar_break = false;
			this.checked_ = false;
			this.radiocheck = false;
			this.enabled = true;
			this.showshortcut = true;
			this.visible = true;
			this.ownerdraw = false;
			this.menubar = false;
			this.menuheight = 0;
			this.xtab = 0;
			this.index = -1;
			this.mnemonic = '\0';
			this.menuid = -1;
			this.mergeorder = 0;
			this.mergetype = MenuMerge.Add;
			this.Text = text;
		}

		/// <summary>Occurs when the menu item is clicked or selected using a shortcut key or access key defined for the menu item.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400003B RID: 59
		// (add) Token: 0x06000C4F RID: 3151 RVA: 0x00035F1A File Offset: 0x0003411A
		// (remove) Token: 0x06000C50 RID: 3152 RVA: 0x00035F2D File Offset: 0x0003412D
		public event EventHandler Click
		{
			add
			{
				base.Events.AddHandler(MenuItem.ClickEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(MenuItem.ClickEvent, value);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.MenuItem.OwnerDraw" /> property of a menu item is set to true and a request is made to draw the menu item.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400003C RID: 60
		// (add) Token: 0x06000C51 RID: 3153 RVA: 0x00035F40 File Offset: 0x00034140
		// (remove) Token: 0x06000C52 RID: 3154 RVA: 0x00035F53 File Offset: 0x00034153
		public event DrawItemEventHandler DrawItem
		{
			add
			{
				base.Events.AddHandler(MenuItem.DrawItemEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(MenuItem.DrawItemEvent, value);
			}
		}

		/// <summary>Occurs when the menu needs to know the size of a menu item before drawing it.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400003D RID: 61
		// (add) Token: 0x06000C53 RID: 3155 RVA: 0x00035F66 File Offset: 0x00034166
		// (remove) Token: 0x06000C54 RID: 3156 RVA: 0x00035F79 File Offset: 0x00034179
		public event MeasureItemEventHandler MeasureItem
		{
			add
			{
				base.Events.AddHandler(MenuItem.MeasureItemEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(MenuItem.MeasureItemEvent, value);
			}
		}

		// Token: 0x06000C55 RID: 3157 RVA: 0x00035F8C File Offset: 0x0003418C
		internal void OnUIACheckedChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[MenuItem.UIACheckedChangedEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06000C56 RID: 3158 RVA: 0x00035FBC File Offset: 0x000341BC
		internal void OnUIARadioCheckChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[MenuItem.UIARadioCheckChangedEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06000C57 RID: 3159 RVA: 0x00035FEC File Offset: 0x000341EC
		internal void OnUIAEnabledChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[MenuItem.UIAEnabledChangedEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06000C58 RID: 3160 RVA: 0x0003601C File Offset: 0x0003421C
		internal void OnUIATextChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[MenuItem.UIATextChangedEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.MenuItem" /> is placed on a new line (for a menu item added to a <see cref="T:System.Windows.Forms.MainMenu" /> object) or in a new column (for a submenu item or menu item displayed in a <see cref="T:System.Windows.Forms.ContextMenu" />).</summary>
		/// <returns>true if the menu item is placed on a new line or in a new column; false if the menu item is left in its default placement. The default is false.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700031A RID: 794
		// (get) Token: 0x06000C59 RID: 3161 RVA: 0x0003604A File Offset: 0x0003424A
		// (set) Token: 0x06000C5A RID: 3162 RVA: 0x00036052 File Offset: 0x00034252
		[Browsable(false)]
		[DefaultValue(false)]
		public bool BarBreak
		{
			get
			{
				return this.break_;
			}
			set
			{
				this.break_ = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the item is placed on a new line (for a menu item added to a <see cref="T:System.Windows.Forms.MainMenu" /> object) or in a new column (for a menu item or submenu item displayed in a <see cref="T:System.Windows.Forms.ContextMenu" />).</summary>
		/// <returns>true if the menu item is placed on a new line or in a new column; false if the menu item is left in its default placement. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700031B RID: 795
		// (get) Token: 0x06000C5B RID: 3163 RVA: 0x0003605B File Offset: 0x0003425B
		// (set) Token: 0x06000C5C RID: 3164 RVA: 0x00036063 File Offset: 0x00034263
		[Browsable(false)]
		[DefaultValue(false)]
		public bool Break
		{
			get
			{
				return this.bar_break;
			}
			set
			{
				this.bar_break = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether a check mark appears next to the text of the menu item.</summary>
		/// <returns>true if there is a check mark next to the menu item; otherwise, false. The default is false.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="T:System.Windows.Forms.MenuItem" /> is a top-level menu or has children.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700031C RID: 796
		// (get) Token: 0x06000C5D RID: 3165 RVA: 0x0003606C File Offset: 0x0003426C
		// (set) Token: 0x06000C5E RID: 3166 RVA: 0x00036074 File Offset: 0x00034274
		[DefaultValue(false)]
		public bool Checked
		{
			get
			{
				return this.checked_;
			}
			set
			{
				if (this.checked_ == value)
				{
					return;
				}
				this.checked_ = value;
				this.OnUIACheckedChanged(EventArgs.Empty);
			}
		}

		/// <summary>Gets or sets a value indicating whether the menu item is the default menu item.</summary>
		/// <returns>true if the menu item is the default item in a menu; otherwise, false. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700031D RID: 797
		// (get) Token: 0x06000C5F RID: 3167 RVA: 0x00036092 File Offset: 0x00034292
		// (set) Token: 0x06000C60 RID: 3168 RVA: 0x0003609A File Offset: 0x0003429A
		[DefaultValue(false)]
		public bool DefaultItem
		{
			get
			{
				return this.defaut_item;
			}
			set
			{
				this.defaut_item = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the menu item is enabled.</summary>
		/// <returns>true if the menu item is enabled; otherwise, false. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700031E RID: 798
		// (get) Token: 0x06000C61 RID: 3169 RVA: 0x000360A3 File Offset: 0x000342A3
		// (set) Token: 0x06000C62 RID: 3170 RVA: 0x000360AB File Offset: 0x000342AB
		[DefaultValue(true)]
		[Localizable(true)]
		public bool Enabled
		{
			get
			{
				return this.enabled;
			}
			set
			{
				if (this.enabled == value)
				{
					return;
				}
				this.enabled = value;
				this.OnUIAEnabledChanged(EventArgs.Empty);
				this.Invalidate();
			}
		}

		/// <summary>Gets or sets a value indicating the position of the menu item in its parent menu.</summary>
		/// <returns>The zero-based index representing the position of the menu item in its parent menu.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The assigned value is less than zero or greater than the item count.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700031F RID: 799
		// (get) Token: 0x06000C63 RID: 3171 RVA: 0x000360CF File Offset: 0x000342CF
		// (set) Token: 0x06000C64 RID: 3172 RVA: 0x000360D8 File Offset: 0x000342D8
		[Browsable(false)]
		public int Index
		{
			get
			{
				return this.index;
			}
			set
			{
				if (this.Parent != null && this.Parent.MenuItems != null && (value < 0 || value >= this.Parent.MenuItems.Count))
				{
					throw new ArgumentException("'" + value + "' is not a valid value for 'value'");
				}
				this.index = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the menu item will be populated with a list of the Multiple Document Interface (MDI) child windows that are displayed within the associated form.</summary>
		/// <returns>true if a list of the MDI child windows is displayed in this menu item; otherwise, false. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000320 RID: 800
		// (get) Token: 0x06000C65 RID: 3173 RVA: 0x00036133 File Offset: 0x00034333
		// (set) Token: 0x06000C66 RID: 3174 RVA: 0x0003613C File Offset: 0x0003433C
		[DefaultValue(false)]
		public bool MdiList
		{
			get
			{
				return this.mdilist;
			}
			set
			{
				if (this.mdilist == value)
				{
					return;
				}
				this.mdilist = value;
				if (this.mdilist || this.mdilist_items == null)
				{
					return;
				}
				foreach (object obj in this.mdilist_items.Keys)
				{
					MenuItem menuItem = (MenuItem)obj;
					base.MenuItems.Remove(menuItem);
				}
				this.mdilist_items.Clear();
				this.mdilist_items = null;
			}
		}

		/// <summary>Gets or sets a value indicating the relative position of the menu item when it is merged with another.</summary>
		/// <returns>A zero-based index representing the merge order position for this menu item. The default is 0.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000321 RID: 801
		// (get) Token: 0x06000C67 RID: 3175 RVA: 0x000361D4 File Offset: 0x000343D4
		// (set) Token: 0x06000C68 RID: 3176 RVA: 0x000361DC File Offset: 0x000343DC
		[DefaultValue(0)]
		public int MergeOrder
		{
			get
			{
				return this.mergeorder;
			}
			set
			{
				this.mergeorder = value;
			}
		}

		/// <summary>Gets or sets a value indicating the behavior of this menu item when its menu is merged with another.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.MenuMerge" /> value that represents the menu item's merge type.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The assigned value is not one of the <see cref="T:System.Windows.Forms.MenuMerge" /> values.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000322 RID: 802
		// (get) Token: 0x06000C69 RID: 3177 RVA: 0x000361E5 File Offset: 0x000343E5
		// (set) Token: 0x06000C6A RID: 3178 RVA: 0x000361ED File Offset: 0x000343ED
		[DefaultValue(MenuMerge.Add)]
		public MenuMerge MergeType
		{
			get
			{
				return this.mergetype;
			}
			set
			{
				if (!Enum.IsDefined(typeof(MenuMerge), value))
				{
					throw new InvalidEnumArgumentException(string.Format("Enum argument value '{0}' is not valid for MenuMerge", value));
				}
				this.mergetype = value;
			}
		}

		/// <summary>Gets a value indicating the mnemonic character that is associated with this menu item.</summary>
		/// <returns>A character that represents the mnemonic character associated with this menu item. Returns the NUL character (ASCII value 0) if no mnemonic character is specified in the text of the <see cref="T:System.Windows.Forms.MenuItem" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000323 RID: 803
		// (get) Token: 0x06000C6B RID: 3179 RVA: 0x00036223 File Offset: 0x00034423
		[Browsable(false)]
		public char Mnemonic
		{
			get
			{
				return this.mnemonic;
			}
		}

		/// <summary>Gets or sets a value indicating whether the code that you provide draws the menu item or Windows draws the menu item.</summary>
		/// <returns>true if the menu item is to be drawn using code; false if the menu item is to be drawn by Windows. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000324 RID: 804
		// (get) Token: 0x06000C6C RID: 3180 RVA: 0x0003622B File Offset: 0x0003442B
		// (set) Token: 0x06000C6D RID: 3181 RVA: 0x00036233 File Offset: 0x00034433
		[DefaultValue(false)]
		public bool OwnerDraw
		{
			get
			{
				return this.ownerdraw;
			}
			set
			{
				this.ownerdraw = value;
			}
		}

		/// <summary>Gets a value indicating the menu that contains this menu item.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Menu" /> that represents the menu that contains this menu item.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000325 RID: 805
		// (get) Token: 0x06000C6E RID: 3182 RVA: 0x0003623C File Offset: 0x0003443C
		[Browsable(false)]
		public Menu Parent
		{
			get
			{
				return this.parent_menu;
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.MenuItem" />, if checked, displays a radio-button instead of a check mark.</summary>
		/// <returns>true if a radio-button is to be used instead of a check mark; false if the standard check mark is to be displayed when the menu item is checked. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000326 RID: 806
		// (get) Token: 0x06000C6F RID: 3183 RVA: 0x00036244 File Offset: 0x00034444
		// (set) Token: 0x06000C70 RID: 3184 RVA: 0x0003624C File Offset: 0x0003444C
		[DefaultValue(false)]
		public bool RadioCheck
		{
			get
			{
				return this.radiocheck;
			}
			set
			{
				if (this.radiocheck == value)
				{
					return;
				}
				this.radiocheck = value;
				this.OnUIARadioCheckChanged(EventArgs.Empty);
			}
		}

		/// <summary>Gets or sets a value indicating the shortcut key associated with the menu item.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.Shortcut" /> values. The default is Shortcut.None.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The assigned value is not one of the <see cref="T:System.Windows.Forms.Shortcut" /> values.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000327 RID: 807
		// (get) Token: 0x06000C71 RID: 3185 RVA: 0x0003626A File Offset: 0x0003446A
		// (set) Token: 0x06000C72 RID: 3186 RVA: 0x00036272 File Offset: 0x00034472
		[DefaultValue(Shortcut.None)]
		[Localizable(true)]
		public Shortcut Shortcut
		{
			get
			{
				return this.shortcut;
			}
			set
			{
				if (!Enum.IsDefined(typeof(Shortcut), value))
				{
					throw new InvalidEnumArgumentException(string.Format("Enum argument value '{0}' is not valid for Shortcut", value));
				}
				this.shortcut = value;
				this.UpdateMenuItem();
			}
		}

		/// <summary>Gets or sets a value indicating whether the shortcut key that is associated with the menu item is displayed next to the menu item caption.</summary>
		/// <returns>true if the shortcut key combination is displayed next to the menu item caption; false if the shortcut key combination is not to be displayed. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000328 RID: 808
		// (get) Token: 0x06000C73 RID: 3187 RVA: 0x000362AE File Offset: 0x000344AE
		// (set) Token: 0x06000C74 RID: 3188 RVA: 0x000362B6 File Offset: 0x000344B6
		[DefaultValue(true)]
		[Localizable(true)]
		public bool ShowShortcut
		{
			get
			{
				return this.showshortcut;
			}
			set
			{
				this.showshortcut = value;
			}
		}

		/// <summary>Gets or sets a value indicating the caption of the menu item.</summary>
		/// <returns>The text caption of the menu item.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000329 RID: 809
		// (get) Token: 0x06000C75 RID: 3189 RVA: 0x000362BF File Offset: 0x000344BF
		// (set) Token: 0x06000C76 RID: 3190 RVA: 0x000362C8 File Offset: 0x000344C8
		[Localizable(true)]
		public string Text
		{
			get
			{
				return this.text;
			}
			set
			{
				this.text = value;
				if (this.text == "-")
				{
					this.separator = true;
				}
				else
				{
					this.separator = false;
				}
				this.OnUIATextChanged(EventArgs.Empty);
				this.ProcessMnemonic();
				this.Invalidate();
			}
		}

		/// <summary>Gets or sets a value indicating whether the menu item is visible.</summary>
		/// <returns>true if the menu item will be made visible on the menu; otherwise, false. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700032A RID: 810
		// (get) Token: 0x06000C77 RID: 3191 RVA: 0x00036315 File Offset: 0x00034515
		// (set) Token: 0x06000C78 RID: 3192 RVA: 0x00036320 File Offset: 0x00034520
		[DefaultValue(true)]
		[Localizable(true)]
		public bool Visible
		{
			get
			{
				return this.visible;
			}
			set
			{
				if (value == this.visible)
				{
					return;
				}
				this.visible = value;
				if (this.menu_items != null)
				{
					foreach (object obj in this.menu_items)
					{
						((MenuItem)obj).Visible = value;
					}
				}
				if (this.parent_menu != null)
				{
					this.parent_menu.OnMenuChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x06000C79 RID: 3193 RVA: 0x000363A8 File Offset: 0x000345A8
		// (set) Token: 0x06000C7A RID: 3194 RVA: 0x000363B5 File Offset: 0x000345B5
		internal new int Height
		{
			get
			{
				return this.bounds.Height;
			}
			set
			{
				this.bounds.Height = value;
			}
		}

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x06000C7B RID: 3195 RVA: 0x000363C3 File Offset: 0x000345C3
		internal bool IsPopup
		{
			get
			{
				return this.menu_items.Count > 0;
			}
		}

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x06000C7C RID: 3196 RVA: 0x000363D6 File Offset: 0x000345D6
		internal bool MeasureEventDefined
		{
			get
			{
				return this.ownerdraw && base.Events[MenuItem.MeasureItemEvent] != null;
			}
		}

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x06000C7D RID: 3197 RVA: 0x000363F5 File Offset: 0x000345F5
		// (set) Token: 0x06000C7E RID: 3198 RVA: 0x000363FD File Offset: 0x000345FD
		internal bool MenuBar
		{
			get
			{
				return this.menubar;
			}
			set
			{
				this.menubar = value;
			}
		}

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x06000C7F RID: 3199 RVA: 0x00036406 File Offset: 0x00034606
		// (set) Token: 0x06000C80 RID: 3200 RVA: 0x0003640E File Offset: 0x0003460E
		internal int MenuHeight
		{
			get
			{
				return this.menuheight;
			}
			set
			{
				this.menuheight = value;
			}
		}

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x06000C81 RID: 3201 RVA: 0x00036417 File Offset: 0x00034617
		// (set) Token: 0x06000C82 RID: 3202 RVA: 0x0003641F File Offset: 0x0003461F
		internal bool Selected
		{
			get
			{
				return this.selected;
			}
			set
			{
				this.selected = value;
			}
		}

		// Token: 0x17000331 RID: 817
		// (get) Token: 0x06000C83 RID: 3203 RVA: 0x00036428 File Offset: 0x00034628
		internal bool Separator
		{
			get
			{
				return this.separator;
			}
		}

		// Token: 0x17000332 RID: 818
		// (get) Token: 0x06000C84 RID: 3204 RVA: 0x00036430 File Offset: 0x00034630
		internal DrawItemState Status
		{
			get
			{
				DrawItemState drawItemState = DrawItemState.None;
				MenuTracker tracker = this.Parent.Tracker;
				if (this.Selected)
				{
					drawItemState |= ((tracker.active || tracker.Navigating) ? DrawItemState.Selected : DrawItemState.HotLight);
				}
				if (!this.Enabled)
				{
					drawItemState |= DrawItemState.Grayed | DrawItemState.Disabled;
				}
				if (this.Checked)
				{
					drawItemState |= DrawItemState.Checked;
				}
				if (!tracker.Navigating)
				{
					drawItemState |= DrawItemState.NoAccelerator;
				}
				return drawItemState;
			}
		}

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x06000C85 RID: 3205 RVA: 0x00036494 File Offset: 0x00034694
		internal bool VisibleItems
		{
			get
			{
				if (this.menu_items != null)
				{
					using (IEnumerator enumerator = this.menu_items.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							if (((MenuItem)enumerator.Current).Visible)
							{
								return true;
							}
						}
					}
					return false;
				}
				return false;
			}
		}

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x06000C86 RID: 3206 RVA: 0x000364FC File Offset: 0x000346FC
		// (set) Token: 0x06000C87 RID: 3207 RVA: 0x00036509 File Offset: 0x00034709
		internal new int Width
		{
			get
			{
				return this.bounds.Width;
			}
			set
			{
				this.bounds.Width = value;
			}
		}

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x06000C88 RID: 3208 RVA: 0x00036517 File Offset: 0x00034717
		// (set) Token: 0x06000C89 RID: 3209 RVA: 0x00036524 File Offset: 0x00034724
		internal new int X
		{
			get
			{
				return this.bounds.X;
			}
			set
			{
				this.bounds.X = value;
			}
		}

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x06000C8A RID: 3210 RVA: 0x00036532 File Offset: 0x00034732
		// (set) Token: 0x06000C8B RID: 3211 RVA: 0x0003653A File Offset: 0x0003473A
		internal int XTab
		{
			get
			{
				return this.xtab;
			}
			set
			{
				this.xtab = value;
			}
		}

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x06000C8C RID: 3212 RVA: 0x00036543 File Offset: 0x00034743
		// (set) Token: 0x06000C8D RID: 3213 RVA: 0x00036550 File Offset: 0x00034750
		internal new int Y
		{
			get
			{
				return this.bounds.Y;
			}
			set
			{
				this.bounds.Y = value;
			}
		}

		/// <summary>Creates a copy of the current <see cref="T:System.Windows.Forms.MenuItem" />.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.MenuItem" /> that represents the duplicated menu item.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000C8E RID: 3214 RVA: 0x0003655E File Offset: 0x0003475E
		public virtual MenuItem CloneMenu()
		{
			MenuItem menuItem = new MenuItem();
			menuItem.CloneMenu(this);
			return menuItem;
		}

		/// <summary>Creates a copy of the specified <see cref="T:System.Windows.Forms.MenuItem" />.</summary>
		/// <param name="itemSrc">The <see cref="T:System.Windows.Forms.MenuItem" /> that represents the menu item to copy. </param>
		// Token: 0x06000C8F RID: 3215 RVA: 0x0003656C File Offset: 0x0003476C
		protected void CloneMenu(MenuItem itemSrc)
		{
			base.CloneMenu(itemSrc);
			this.MdiList = itemSrc.MdiList;
			this.is_window_menu_item = itemSrc.is_window_menu_item;
			bool flag = false;
			for (int i = base.MenuItems.Count - 1; i >= 0; i--)
			{
				if (base.MenuItems[i].is_window_menu_item)
				{
					base.MenuItems.RemoveAt(i);
					flag = true;
				}
			}
			if (flag)
			{
				this.PopulateWindowMenu();
			}
			this.BarBreak = itemSrc.BarBreak;
			this.Break = itemSrc.Break;
			this.Checked = itemSrc.Checked;
			this.DefaultItem = itemSrc.DefaultItem;
			this.Enabled = itemSrc.Enabled;
			this.MergeOrder = itemSrc.MergeOrder;
			this.MergeType = itemSrc.MergeType;
			this.OwnerDraw = itemSrc.OwnerDraw;
			this.RadioCheck = itemSrc.RadioCheck;
			this.Shortcut = itemSrc.Shortcut;
			this.ShowShortcut = itemSrc.ShowShortcut;
			this.Text = itemSrc.Text;
			this.Visible = itemSrc.Visible;
			base.Name = itemSrc.Name;
			base.Tag = itemSrc.Tag;
			base.Events[MenuItem.ClickEvent] = itemSrc.Events[MenuItem.ClickEvent];
			base.Events[MenuItem.DrawItemEvent] = itemSrc.Events[MenuItem.DrawItemEvent];
			base.Events[MenuItem.MeasureItemEvent] = itemSrc.Events[MenuItem.MeasureItemEvent];
			base.Events[MenuItem.PopupEvent] = itemSrc.Events[MenuItem.PopupEvent];
			base.Events[MenuItem.SelectEvent] = itemSrc.Events[MenuItem.SelectEvent];
		}

		/// <summary>Disposes of the resources (other than memory) used by the <see cref="T:System.Windows.Forms.MenuItem" />.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x06000C90 RID: 3216 RVA: 0x00036730 File Offset: 0x00034930
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.parent_menu != null)
			{
				this.parent_menu.MenuItems.Remove(this);
			}
			base.Dispose(disposing);
		}

		/// <summary>Merges another menu item with this menu item.</summary>
		/// <param name="itemSrc">A <see cref="T:System.Windows.Forms.MenuItem" /> that specifies the menu item to merge with this one. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000C91 RID: 3217 RVA: 0x00036755 File Offset: 0x00034955
		public void MergeMenu(MenuItem itemSrc)
		{
			base.MergeMenu(itemSrc);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.MenuItem.Click" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000C92 RID: 3218 RVA: 0x00036760 File Offset: 0x00034960
		protected virtual void OnClick(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[MenuItem.ClickEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.MenuItem.DrawItem" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DrawItemEventArgs" /> that contains the event data. </param>
		// Token: 0x06000C93 RID: 3219 RVA: 0x00036790 File Offset: 0x00034990
		protected virtual void OnDrawItem(DrawItemEventArgs e)
		{
			DrawItemEventHandler drawItemEventHandler = (DrawItemEventHandler)base.Events[MenuItem.DrawItemEvent];
			if (drawItemEventHandler != null)
			{
				drawItemEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.MenuItem.MeasureItem" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.MeasureItemEventArgs" /> that contains the event data. </param>
		// Token: 0x06000C94 RID: 3220 RVA: 0x000367C0 File Offset: 0x000349C0
		protected virtual void OnMeasureItem(MeasureItemEventArgs e)
		{
			if (!this.OwnerDraw)
			{
				return;
			}
			MeasureItemEventHandler measureItemEventHandler = (MeasureItemEventHandler)base.Events[MenuItem.MeasureItemEvent];
			if (measureItemEventHandler != null)
			{
				measureItemEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.MenuItem.Popup" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000C95 RID: 3221 RVA: 0x000367F8 File Offset: 0x000349F8
		protected virtual void OnPopup(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[MenuItem.PopupEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.MenuItem.Select" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000C96 RID: 3222 RVA: 0x00036828 File Offset: 0x00034A28
		protected virtual void OnSelect(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[MenuItem.SelectEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Generates a <see cref="E:System.Windows.Forms.Control.Click" /> event for the <see cref="T:System.Windows.Forms.MenuItem" />, simulating a click by a user.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C97 RID: 3223 RVA: 0x00036856 File Offset: 0x00034A56
		public void PerformClick()
		{
			this.OnClick(EventArgs.Empty);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.MenuItem.Select" /> event for this menu item.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C98 RID: 3224 RVA: 0x00036863 File Offset: 0x00034A63
		public virtual void PerformSelect()
		{
			this.OnSelect(EventArgs.Empty);
		}

		/// <summary>Returns a string that represents the <see cref="T:System.Windows.Forms.MenuItem" />.</summary>
		/// <returns>A string that represents the current <see cref="T:System.Windows.Forms.MenuItem" />. The string includes the type and the <see cref="P:System.Windows.Forms.MenuItem.Text" /> property of the control.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000C99 RID: 3225 RVA: 0x00036870 File Offset: 0x00034A70
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				base.ToString(),
				", Items.Count: ",
				base.MenuItems.Count,
				", Text: ",
				this.text
			});
		}

		// Token: 0x06000C9A RID: 3226 RVA: 0x000368C0 File Offset: 0x00034AC0
		internal virtual void Invalidate()
		{
			if (this.Parent == null || !(this.Parent is MainMenu) || this.Parent.Wnd == null)
			{
				return;
			}
			Form form = this.Parent.Wnd.FindForm();
			if (form == null || !form.IsHandleCreated)
			{
				return;
			}
			XplatUI.RequestNCRecalc(form.Handle);
		}

		// Token: 0x06000C9B RID: 3227 RVA: 0x00036918 File Offset: 0x00034B18
		internal void PerformPopup()
		{
			this.OnPopup(EventArgs.Empty);
		}

		// Token: 0x06000C9C RID: 3228 RVA: 0x00036925 File Offset: 0x00034B25
		internal void PerformDrawItem(DrawItemEventArgs e)
		{
			this.PopulateWindowMenu();
			if (this.OwnerDraw)
			{
				this.OnDrawItem(e);
				return;
			}
			ThemeEngine.Current.DrawMenuItem(this, e);
		}

		// Token: 0x06000C9D RID: 3229 RVA: 0x0003694C File Offset: 0x00034B4C
		private void PopulateWindowMenu()
		{
			if (this.mdilist)
			{
				if (this.mdilist_items == null)
				{
					this.mdilist_items = new Hashtable();
					this.mdilist_forms = new Hashtable();
				}
				MainMenu mainMenu = base.GetMainMenu();
				if (mainMenu != null && mainMenu.GetForm() != null)
				{
					Form form = mainMenu.GetForm();
					this.mdicontainer = form.MdiContainer;
					if (this.mdicontainer != null)
					{
						MenuItem[] array = new MenuItem[this.mdilist_items.Count];
						this.mdilist_items.Keys.CopyTo(array, 0);
						foreach (MenuItem menuItem in array)
						{
							Form form2 = (Form)this.mdilist_items[menuItem];
							if (!this.mdicontainer.mdi_child_list.Contains(form2))
							{
								this.mdilist_items.Remove(menuItem);
								this.mdilist_forms.Remove(form2);
								base.MenuItems.Remove(menuItem);
							}
						}
						for (int j = 0; j < this.mdicontainer.mdi_child_list.Count; j++)
						{
							Form form3 = (Form)this.mdicontainer.mdi_child_list[j];
							MenuItem menuItem2;
							if (this.mdilist_forms.Contains(form3))
							{
								menuItem2 = (MenuItem)this.mdilist_forms[form3];
							}
							else
							{
								menuItem2 = new MenuItem();
								menuItem2.is_window_menu_item = true;
								menuItem2.Click += this.MdiWindowClickHandler;
								this.mdilist_items[menuItem2] = form3;
								this.mdilist_forms[form3] = menuItem2;
								base.MenuItems.AddNoEvents(menuItem2);
							}
							menuItem2.Visible = form3.Visible;
							menuItem2.Text = "&" + (j + 1).ToString() + " " + form3.Text;
							menuItem2.Checked = form.ActiveMdiChild == form3;
						}
						return;
					}
				}
			}
			else if (this.mdilist_items != null)
			{
				foreach (object obj in this.mdilist_items.Values)
				{
					MenuItem menuItem3 = (MenuItem)obj;
					base.MenuItems.Remove(menuItem3);
				}
				this.mdilist_forms.Clear();
				this.mdilist_items.Clear();
			}
		}

		// Token: 0x06000C9E RID: 3230 RVA: 0x00036BBC File Offset: 0x00034DBC
		internal void PerformMeasureItem(MeasureItemEventArgs e)
		{
			this.OnMeasureItem(e);
		}

		// Token: 0x06000C9F RID: 3231 RVA: 0x00036BC8 File Offset: 0x00034DC8
		private void ProcessMnemonic()
		{
			if (this.text == null || this.text.Length < 2)
			{
				this.mnemonic = '\0';
				return;
			}
			bool flag = false;
			for (int i = 0; i < this.text.Length - 1; i++)
			{
				if (this.text[i] == '&')
				{
					if (!flag && this.text[i + 1] != '&')
					{
						this.mnemonic = char.ToUpper(this.text[i + 1]);
						return;
					}
					flag = true;
				}
				else
				{
					flag = false;
				}
			}
			this.mnemonic = '\0';
		}

		// Token: 0x06000CA0 RID: 3232 RVA: 0x00036C59 File Offset: 0x00034E59
		private string GetShortCutTextCtrl()
		{
			return "Ctrl";
		}

		// Token: 0x06000CA1 RID: 3233 RVA: 0x00036C60 File Offset: 0x00034E60
		private string GetShortCutTextAlt()
		{
			return "Alt";
		}

		// Token: 0x06000CA2 RID: 3234 RVA: 0x00036C67 File Offset: 0x00034E67
		private string GetShortCutTextShift()
		{
			return "Shift";
		}

		// Token: 0x06000CA3 RID: 3235 RVA: 0x00036C70 File Offset: 0x00034E70
		internal string GetShortCutText()
		{
			if (this.Shortcut >= Shortcut.CtrlA && this.Shortcut <= Shortcut.CtrlZ)
			{
				return this.GetShortCutTextCtrl() + "+" + ((char)(65 + (this.Shortcut - Shortcut.CtrlA))).ToString();
			}
			if (this.Shortcut >= Shortcut.Alt0 && this.Shortcut <= Shortcut.Alt9)
			{
				return this.GetShortCutTextAlt() + "+" + ((char)(48 + (this.Shortcut - Shortcut.Alt0))).ToString();
			}
			if (this.Shortcut >= Shortcut.AltF1 && this.Shortcut <= Shortcut.AltF9)
			{
				return this.GetShortCutTextAlt() + "+F" + ((char)(49 + (this.Shortcut - Shortcut.AltF1))).ToString();
			}
			if (this.Shortcut >= Shortcut.Ctrl0 && this.Shortcut <= Shortcut.Ctrl9)
			{
				return this.GetShortCutTextCtrl() + "+" + ((char)(48 + (this.Shortcut - Shortcut.Ctrl0))).ToString();
			}
			if (this.Shortcut >= Shortcut.CtrlF1 && this.Shortcut <= Shortcut.CtrlF9)
			{
				return this.GetShortCutTextCtrl() + "+F" + ((char)(49 + (this.Shortcut - Shortcut.CtrlF1))).ToString();
			}
			if (this.Shortcut >= Shortcut.CtrlShift0 && this.Shortcut <= Shortcut.CtrlShift9)
			{
				return string.Concat(new string[]
				{
					this.GetShortCutTextCtrl(),
					"+",
					this.GetShortCutTextShift(),
					"+",
					((char)(48 + (this.Shortcut - Shortcut.CtrlShift0))).ToString()
				});
			}
			if (this.Shortcut >= Shortcut.CtrlShiftA && this.Shortcut <= Shortcut.CtrlShiftZ)
			{
				return string.Concat(new string[]
				{
					this.GetShortCutTextCtrl(),
					"+",
					this.GetShortCutTextShift(),
					"+",
					((char)(65 + (this.Shortcut - Shortcut.CtrlShiftA))).ToString()
				});
			}
			if (this.Shortcut >= Shortcut.CtrlShiftF1 && this.Shortcut <= Shortcut.CtrlShiftF9)
			{
				return string.Concat(new string[]
				{
					this.GetShortCutTextCtrl(),
					"+",
					this.GetShortCutTextShift(),
					"+F",
					((char)(49 + (this.Shortcut - Shortcut.CtrlShiftF1))).ToString()
				});
			}
			if (this.Shortcut >= Shortcut.F1 && this.Shortcut <= Shortcut.F9)
			{
				return "F" + ((char)(49 + (this.Shortcut - Shortcut.F1))).ToString();
			}
			if (this.Shortcut >= Shortcut.ShiftF1 && this.Shortcut <= Shortcut.ShiftF9)
			{
				return this.GetShortCutTextShift() + "+F" + ((char)(49 + (this.Shortcut - Shortcut.ShiftF1))).ToString();
			}
			Shortcut shortcut = this.Shortcut;
			if (shortcut <= Shortcut.ShiftDel)
			{
				if (shortcut <= Shortcut.Del)
				{
					if (shortcut == Shortcut.None)
					{
						return "None";
					}
					if (shortcut == Shortcut.Ins)
					{
						return "Ins";
					}
					if (shortcut == Shortcut.Del)
					{
						return "Del";
					}
				}
				else
				{
					switch (shortcut)
					{
					case Shortcut.F10:
						return "F10";
					case Shortcut.F11:
						return "F11";
					case Shortcut.F12:
						return "F12";
					default:
						if (shortcut == Shortcut.ShiftIns)
						{
							return this.GetShortCutTextShift() + "+Ins";
						}
						if (shortcut == Shortcut.ShiftDel)
						{
							return this.GetShortCutTextShift() + "+Del";
						}
						break;
					}
				}
			}
			else if (shortcut <= Shortcut.CtrlDel)
			{
				switch (shortcut)
				{
				case Shortcut.ShiftF10:
					return this.GetShortCutTextShift() + "+F10";
				case Shortcut.ShiftF11:
					return this.GetShortCutTextShift() + "+F11";
				case Shortcut.ShiftF12:
					return this.GetShortCutTextShift() + "+F12";
				default:
					if (shortcut == Shortcut.CtrlIns)
					{
						return this.GetShortCutTextCtrl() + "+Ins";
					}
					if (shortcut == Shortcut.CtrlDel)
					{
						return this.GetShortCutTextCtrl() + "+Del";
					}
					break;
				}
			}
			else if (shortcut <= Shortcut.CtrlShiftF12)
			{
				switch (shortcut)
				{
				case Shortcut.CtrlF10:
					return this.GetShortCutTextCtrl() + "+F10";
				case Shortcut.CtrlF11:
					return this.GetShortCutTextCtrl() + "+F11";
				case Shortcut.CtrlF12:
					return this.GetShortCutTextCtrl() + "+F12";
				default:
					switch (shortcut)
					{
					case Shortcut.CtrlShiftF10:
						return this.GetShortCutTextCtrl() + "+" + this.GetShortCutTextShift() + "+F10";
					case Shortcut.CtrlShiftF11:
						return this.GetShortCutTextCtrl() + "+" + this.GetShortCutTextShift() + "+F11";
					case Shortcut.CtrlShiftF12:
						return this.GetShortCutTextCtrl() + "+" + this.GetShortCutTextShift() + "+F12";
					}
					break;
				}
			}
			else
			{
				if (shortcut == Shortcut.AltBksp)
				{
					return "AltBksp";
				}
				switch (shortcut)
				{
				case Shortcut.AltF10:
					return this.GetShortCutTextAlt() + "+F10";
				case Shortcut.AltF11:
					return this.GetShortCutTextAlt() + "+F11";
				case Shortcut.AltF12:
					return this.GetShortCutTextAlt() + "+F12";
				}
			}
			return "";
		}

		// Token: 0x06000CA4 RID: 3236 RVA: 0x000371C8 File Offset: 0x000353C8
		private void MdiWindowClickHandler(object sender, EventArgs e)
		{
			Form form = (Form)this.mdilist_items[sender];
			if (form == null)
			{
				return;
			}
			this.mdicontainer.ActivateChild(form);
		}

		// Token: 0x06000CA5 RID: 3237 RVA: 0x000371F7 File Offset: 0x000353F7
		private void UpdateMenuItem()
		{
			if (this.parent_menu == null || this.parent_menu.Tracker == null)
			{
				return;
			}
			this.parent_menu.Tracker.RemoveShortcuts(this);
			this.parent_menu.Tracker.AddShortcuts(this);
		}

		// Token: 0x06000CA6 RID: 3238 RVA: 0x00037234 File Offset: 0x00035434
		// Note: this type is marked as 'beforefieldinit'.
		static MenuItem()
		{
			MenuItem.ClickEvent = new object();
			MenuItem.DrawItemEvent = new object();
			MenuItem.MeasureItemEvent = new object();
			MenuItem.PopupEvent = new object();
			MenuItem.SelectEvent = new object();
			MenuItem.UIACheckedChangedEvent = new object();
			MenuItem.UIARadioCheckChangedEvent = new object();
			MenuItem.UIAEnabledChangedEvent = new object();
			MenuItem.UIATextChangedEvent = new object();
		}

		// Token: 0x040007CB RID: 1995
		internal bool separator;

		// Token: 0x040007CC RID: 1996
		internal bool break_;

		// Token: 0x040007CD RID: 1997
		internal bool bar_break;

		// Token: 0x040007CE RID: 1998
		private Shortcut shortcut;

		// Token: 0x040007CF RID: 1999
		private string text;

		// Token: 0x040007D0 RID: 2000
		private bool checked_;

		// Token: 0x040007D1 RID: 2001
		private bool radiocheck;

		// Token: 0x040007D2 RID: 2002
		private bool enabled;

		// Token: 0x040007D3 RID: 2003
		private char mnemonic;

		// Token: 0x040007D4 RID: 2004
		private bool showshortcut;

		// Token: 0x040007D5 RID: 2005
		private int index;

		// Token: 0x040007D6 RID: 2006
		private bool mdilist;

		// Token: 0x040007D7 RID: 2007
		private Hashtable mdilist_items;

		// Token: 0x040007D8 RID: 2008
		private Hashtable mdilist_forms;

		// Token: 0x040007D9 RID: 2009
		private MdiClient mdicontainer;

		// Token: 0x040007DA RID: 2010
		private bool is_window_menu_item;

		// Token: 0x040007DB RID: 2011
		private bool defaut_item;

		// Token: 0x040007DC RID: 2012
		private bool visible;

		// Token: 0x040007DD RID: 2013
		private bool ownerdraw;

		// Token: 0x040007DE RID: 2014
		private int menuid;

		// Token: 0x040007DF RID: 2015
		private int mergeorder;

		// Token: 0x040007E0 RID: 2016
		private int xtab;

		// Token: 0x040007E1 RID: 2017
		private int menuheight;

		// Token: 0x040007E2 RID: 2018
		private bool menubar;

		// Token: 0x040007E3 RID: 2019
		private MenuMerge mergetype;

		// Token: 0x040007E4 RID: 2020
		internal Rectangle bounds;

		// Token: 0x040007E8 RID: 2024
		private static object PopupEvent;

		// Token: 0x040007E9 RID: 2025
		private static object SelectEvent;

		// Token: 0x040007EA RID: 2026
		private static object UIACheckedChangedEvent;

		// Token: 0x040007EB RID: 2027
		private static object UIARadioCheckChangedEvent;

		// Token: 0x040007EC RID: 2028
		private static object UIAEnabledChangedEvent;

		// Token: 0x040007ED RID: 2029
		private static object UIATextChangedEvent;

		// Token: 0x040007EE RID: 2030
		private bool selected;
	}
}
