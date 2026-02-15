using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Represents a Windows list view control, which displays a collection of items that can be displayed using one of four different views. </summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000107 RID: 263
	[DefaultEvent("SelectedIndexChanged")]
	[DefaultProperty("Items")]
	[Designer("System.Windows.Forms.Design.ListViewDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Docking(DockingBehavior.Ask)]
	public class ListView : Control
	{
		/// <summary>Occurs when the user clicks a column header within the list view control.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000034 RID: 52
		// (add) Token: 0x06000951 RID: 2385 RVA: 0x000274A6 File Offset: 0x000256A6
		// (remove) Token: 0x06000952 RID: 2386 RVA: 0x000274B9 File Offset: 0x000256B9
		public event ColumnClickEventHandler ColumnClick
		{
			add
			{
				base.Events.AddHandler(ListView.ColumnClickEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ListView.ColumnClickEvent, value);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListView" /> class.</summary>
		// Token: 0x06000953 RID: 2387 RVA: 0x000274CC File Offset: 0x000256CC
		public ListView()
		{
			this.background_color = ThemeEngine.Current.ColorWindow;
			this.groups = new ListViewGroupCollection(this);
			this.items = new ListView.ListViewItemCollection(this);
			this.items.Changed += this.OnItemsChanged;
			this.checked_indices = new ListView.CheckedIndexCollection(this);
			this.checked_items = new ListView.CheckedListViewItemCollection(this);
			this.columns = new ListView.ColumnHeaderCollection(this);
			this.foreground_color = SystemColors.WindowText;
			this.selected_indices = new ListView.SelectedIndexCollection(this);
			this.selected_items = new ListView.SelectedListViewItemCollection(this);
			this.items_location = new Point[16];
			this.items_matrix_location = new ListView.ItemMatrixLocation[16];
			this.reordered_items_indices = new int[16];
			this.item_tooltip = new ToolTip();
			this.item_tooltip.Active = false;
			this.insertion_mark = new ListViewInsertionMark(this);
			base.InternalBorderStyle = BorderStyle.Fixed3D;
			this.header_control = new ListView.HeaderControl(this);
			this.header_control.Visible = false;
			base.Controls.AddImplicit(this.header_control);
			this.item_control = new ListView.ItemControl(this);
			base.Controls.AddImplicit(this.item_control);
			this.h_scroll = new ImplicitHScrollBar();
			base.Controls.AddImplicit(this.h_scroll);
			this.v_scroll = new ImplicitVScrollBar();
			base.Controls.AddImplicit(this.v_scroll);
			this.h_marker = (this.v_marker = 0);
			this.keysearch_tickcnt = 0;
			this.h_scroll.Visible = false;
			this.h_scroll.ValueChanged += this.HorizontalScroller;
			this.v_scroll.Visible = false;
			this.v_scroll.ValueChanged += this.VerticalScroller;
			base.KeyDown += this.ListView_KeyDown;
			base.SizeChanged += this.ListView_SizeChanged;
			base.GotFocus += this.FocusChanged;
			base.LostFocus += this.FocusChanged;
			base.MouseWheel += this.ListView_MouseWheel;
			base.MouseEnter += this.ListView_MouseEnter;
			base.Invalidated += this.ListView_Invalidated;
			this.BackgroundImageTiled = false;
			base.SetStyle(ControlStyles.UserPaint | ControlStyles.StandardClick | ControlStyles.UseTextForAccessibility, false);
		}

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x06000954 RID: 2388 RVA: 0x00027773 File Offset: 0x00025973
		internal Size CheckBoxSize
		{
			get
			{
				if (!this.check_boxes)
				{
					return Size.Empty;
				}
				if (this.state_image_list != null)
				{
					return this.state_image_list.ImageSize;
				}
				return ThemeEngine.Current.ListViewCheckBoxSize;
			}
		}

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x06000955 RID: 2389 RVA: 0x000277A4 File Offset: 0x000259A4
		// (set) Token: 0x06000956 RID: 2390 RVA: 0x00027811 File Offset: 0x00025A11
		internal Size ItemSize
		{
			get
			{
				if (this.view != View.Details)
				{
					return this.item_size;
				}
				Size size = default(Size);
				size.Height = this.item_size.Height;
				for (int i = 0; i < this.columns.Count; i++)
				{
					size.Width += this.columns[i].Wd;
				}
				return size;
			}
			set
			{
				this.item_size = value;
			}
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x06000957 RID: 2391 RVA: 0x0002781A File Offset: 0x00025A1A
		// (set) Token: 0x06000958 RID: 2392 RVA: 0x00027822 File Offset: 0x00025A22
		internal int HotItemIndex
		{
			get
			{
				return this.hot_item_index;
			}
			set
			{
				this.hot_item_index = value;
			}
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x06000959 RID: 2393 RVA: 0x0002782B File Offset: 0x00025A2B
		internal bool UsingGroups
		{
			get
			{
				return this.show_groups && this.groups.Count > 0 && this.view != View.List && Application.VisualStylesEnabled;
			}
		}

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x0600095A RID: 2394 RVA: 0x00002D70 File Offset: 0x00000F70
		internal override bool ScaleChildrenInternal
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x0600095B RID: 2395 RVA: 0x00027853 File Offset: 0x00025A53
		internal bool UseCustomColumnWidth
		{
			get
			{
				return (this.view == View.List || this.view == View.SmallIcon) && this.columns.Count > 0;
			}
		}

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x0600095C RID: 2396 RVA: 0x00027877 File Offset: 0x00025A77
		internal ColumnHeader EnteredColumnHeader
		{
			get
			{
				return this.header_control.EnteredColumnHeader;
			}
		}

		/// <summary>This property is not relevant for this class.</summary>
		/// <returns>null in all cases.</returns>
		// Token: 0x17000258 RID: 600
		// (get) Token: 0x0600095D RID: 2397 RVA: 0x00004663 File Offset: 0x00002863
		protected override CreateParams CreateParams
		{
			get
			{
				return base.CreateParams;
			}
		}

		/// <summary>Gets the default size of the control.</summary>
		/// <returns>The default <see cref="T:System.Drawing.Size" /> of the control.</returns>
		// Token: 0x17000259 RID: 601
		// (get) Token: 0x0600095E RID: 2398 RVA: 0x00027884 File Offset: 0x00025A84
		protected override Size DefaultSize
		{
			get
			{
				return ThemeEngine.Current.ListViewDefaultSize;
			}
		}

		/// <summary>Gets or sets a value indicating whether this control should redraw its surface using a secondary buffer to reduce or prevent flicker.</summary>
		/// <returns>true if the surface of the control should be drawn using double buffering; otherwise, false.</returns>
		// Token: 0x1700025A RID: 602
		// (get) Token: 0x0600095F RID: 2399 RVA: 0x00027890 File Offset: 0x00025A90
		protected override bool DoubleBuffered
		{
			get
			{
				return base.DoubleBuffered;
			}
		}

		/// <summary>Gets or sets the type of action the user must take to activate an item.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ItemActivation" /> values. The default is <see cref="F:System.Windows.Forms.ItemActivation.Standard" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value specified is not one of the <see cref="T:System.Windows.Forms.ItemActivation" /> members. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700025B RID: 603
		// (get) Token: 0x06000960 RID: 2400 RVA: 0x00027898 File Offset: 0x00025A98
		[DefaultValue(ItemActivation.Standard)]
		public ItemActivation Activation
		{
			get
			{
				return this.activation;
			}
		}

		/// <summary>Gets or sets the alignment of items in the control.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ListViewAlignment" /> values. The default is <see cref="F:System.Windows.Forms.ListViewAlignment.Top" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value specified is not one of the <see cref="T:System.Windows.Forms.ListViewAlignment" /> values. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700025C RID: 604
		// (get) Token: 0x06000961 RID: 2401 RVA: 0x000278A0 File Offset: 0x00025AA0
		[DefaultValue(ListViewAlignment.Top)]
		[Localizable(true)]
		public ListViewAlignment Alignment
		{
			get
			{
				return this.alignment;
			}
		}

		/// <summary>Gets or sets a value indicating whether the user can drag column headers to reorder columns in the control.</summary>
		/// <returns>true if drag-and-drop column reordering is allowed; otherwise, false. The default is false.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700025D RID: 605
		// (get) Token: 0x06000962 RID: 2402 RVA: 0x000278A8 File Offset: 0x00025AA8
		// (set) Token: 0x06000963 RID: 2403 RVA: 0x000278B0 File Offset: 0x00025AB0
		[DefaultValue(false)]
		public bool AllowColumnReorder
		{
			get
			{
				return this.allow_column_reorder;
			}
			set
			{
				this.allow_column_reorder = value;
			}
		}

		/// <summary>Gets or sets the background color.</summary>
		/// <returns>The <see cref="T:System.Drawing.Color" /> of the background.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700025E RID: 606
		// (get) Token: 0x06000964 RID: 2404 RVA: 0x000278B9 File Offset: 0x00025AB9
		// (set) Token: 0x06000965 RID: 2405 RVA: 0x000278D9 File Offset: 0x00025AD9
		public override Color BackColor
		{
			get
			{
				if (this.background_color.IsEmpty)
				{
					return ThemeEngine.Current.ColorWindow;
				}
				return this.background_color;
			}
			set
			{
				this.background_color = value;
				this.item_control.BackColor = value;
			}
		}

		/// <summary>Gets or sets an <see cref="T:System.Windows.Forms.ImageLayout" /> value.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ImageLayout" /> values.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value specified is not one of the <see cref="T:System.Windows.Forms.ImageLayout" /> values.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700025F RID: 607
		// (get) Token: 0x06000966 RID: 2406 RVA: 0x00005B7A File Offset: 0x00003D7A
		// (set) Token: 0x06000967 RID: 2407 RVA: 0x00005B82 File Offset: 0x00003D82
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

		/// <summary>Gets or sets a value indicating whether the background image of the <see cref="T:System.Windows.Forms.ListView" /> should be tiled.</summary>
		/// <returns>true if the background image of the <see cref="T:System.Windows.Forms.ListView" /> should be tiled; otherwise, false. The default is false.</returns>
		// Token: 0x17000260 RID: 608
		// (set) Token: 0x06000968 RID: 2408 RVA: 0x000278F0 File Offset: 0x00025AF0
		[DefaultValue(false)]
		public bool BackgroundImageTiled
		{
			set
			{
				ImageLayout imageLayout = (value ? ImageLayout.Tile : ImageLayout.None);
				if (imageLayout == this.item_control.BackgroundImageLayout)
				{
					return;
				}
				this.item_control.BackgroundImageLayout = imageLayout;
			}
		}

		/// <summary>Gets or sets a value indicating whether a check box appears next to each item in the control.</summary>
		/// <returns>true if a check box appears next to each item in the <see cref="T:System.Windows.Forms.ListView" /> control; otherwise, false. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000261 RID: 609
		// (get) Token: 0x06000969 RID: 2409 RVA: 0x00027920 File Offset: 0x00025B20
		[DefaultValue(false)]
		public bool CheckBoxes
		{
			get
			{
				return this.check_boxes;
			}
		}

		/// <summary>Gets the currently checked items in the control.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ListView.CheckedListViewItemCollection" /> that contains the currently checked items. If no items are currently checked, an empty <see cref="T:System.Windows.Forms.ListView.CheckedListViewItemCollection" /> is returned.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000262 RID: 610
		// (get) Token: 0x0600096A RID: 2410 RVA: 0x00027928 File Offset: 0x00025B28
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public ListView.CheckedListViewItemCollection CheckedItems
		{
			get
			{
				return this.checked_items;
			}
		}

		/// <summary>Gets the collection of all column headers that appear in the control.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ListView.ColumnHeaderCollection" /> that represents the column headers that appear when the <see cref="P:System.Windows.Forms.ListView.View" /> property is set to <see cref="F:System.Windows.Forms.View.Details" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000263 RID: 611
		// (get) Token: 0x0600096B RID: 2411 RVA: 0x00027930 File Offset: 0x00025B30
		[Editor("System.Windows.Forms.Design.ColumnHeaderCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Localizable(true)]
		[MergableProperty(false)]
		public ListView.ColumnHeaderCollection Columns
		{
			get
			{
				return this.columns;
			}
		}

		/// <summary>Gets or sets the item in the control that currently has focus.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ListViewItem" /> that represents the item that has focus, or null if no item has the focus in the <see cref="T:System.Windows.Forms.ListView" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000264 RID: 612
		// (get) Token: 0x0600096C RID: 2412 RVA: 0x00027938 File Offset: 0x00025B38
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public ListViewItem FocusedItem
		{
			get
			{
				if (this.focused_item_index == -1)
				{
					return null;
				}
				return this.GetItemAtDisplayIndex(this.focused_item_index);
			}
		}

		/// <summary>Gets or sets the foreground color.</summary>
		/// <returns>The <see cref="T:System.Drawing.Color" /> that is the foreground color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000265 RID: 613
		// (get) Token: 0x0600096D RID: 2413 RVA: 0x00027951 File Offset: 0x00025B51
		// (set) Token: 0x0600096E RID: 2414 RVA: 0x00027971 File Offset: 0x00025B71
		public override Color ForeColor
		{
			get
			{
				if (this.foreground_color.IsEmpty)
				{
					return ThemeEngine.Current.ColorWindowText;
				}
				return this.foreground_color;
			}
			set
			{
				this.foreground_color = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether clicking an item selects all its subitems.</summary>
		/// <returns>true if clicking an item selects the item and all its subitems; false if clicking an item selects only the item itself. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000266 RID: 614
		// (get) Token: 0x0600096F RID: 2415 RVA: 0x0002797A File Offset: 0x00025B7A
		[DefaultValue(false)]
		public bool FullRowSelect
		{
			get
			{
				return this.full_row_select;
			}
		}

		/// <summary>Gets or sets a value indicating whether grid lines appear between the rows and columns containing the items and subitems in the control.</summary>
		/// <returns>true if grid lines are drawn around items and subitems; otherwise, false. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000267 RID: 615
		// (get) Token: 0x06000970 RID: 2416 RVA: 0x00027982 File Offset: 0x00025B82
		[DefaultValue(false)]
		public bool GridLines
		{
			get
			{
				return this.grid_lines;
			}
		}

		/// <summary>Gets or sets the column header style.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ColumnHeaderStyle" /> values. The default is <see cref="F:System.Windows.Forms.ColumnHeaderStyle.Clickable" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value specified is not one of the <see cref="T:System.Windows.Forms.ColumnHeaderStyle" /> values. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000268 RID: 616
		// (get) Token: 0x06000971 RID: 2417 RVA: 0x0002798A File Offset: 0x00025B8A
		[DefaultValue(ColumnHeaderStyle.Clickable)]
		public ColumnHeaderStyle HeaderStyle
		{
			get
			{
				return this.header_style;
			}
		}

		/// <summary>Gets or sets a value indicating whether the selected item in the control remains highlighted when the control loses focus.</summary>
		/// <returns>true if the selected item does not appear highlighted when the control loses focus; false if the selected item still appears highlighted when the control loses focus. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000269 RID: 617
		// (get) Token: 0x06000972 RID: 2418 RVA: 0x00027992 File Offset: 0x00025B92
		[DefaultValue(true)]
		public bool HideSelection
		{
			get
			{
				return this.hide_selection;
			}
		}

		/// <summary>Gets or sets a value indicating whether the text of an item or subitem has the appearance of a hyperlink when the mouse pointer passes over it.</summary>
		/// <returns>true if the item text has the appearance of a hyperlink when the mouse passes over it; otherwise, false. The default is false.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700026A RID: 618
		// (get) Token: 0x06000973 RID: 2419 RVA: 0x0002799A File Offset: 0x00025B9A
		[DefaultValue(false)]
		public bool HotTracking
		{
			get
			{
				return this.hot_tracking;
			}
		}

		/// <summary>Gets or sets a value indicating whether an item is automatically selected when the mouse pointer remains over the item for a few seconds.</summary>
		/// <returns>true if an item is automatically selected when the mouse pointer hovers over it; otherwise, false. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06000974 RID: 2420 RVA: 0x000279A2 File Offset: 0x00025BA2
		[DefaultValue(false)]
		public bool HoverSelection
		{
			get
			{
				return this.hover_selection;
			}
		}

		/// <summary>Gets an object used to indicate the expected drop location when an item is dragged within a <see cref="T:System.Windows.Forms.ListView" /> control.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ListViewInsertionMark" /> object representing the insertion mark.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700026C RID: 620
		// (get) Token: 0x06000975 RID: 2421 RVA: 0x000279AA File Offset: 0x00025BAA
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public ListViewInsertionMark InsertionMark
		{
			get
			{
				return this.insertion_mark;
			}
		}

		/// <summary>Gets a collection containing all items in the control.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ListView.ListViewItemCollection" /> that contains all the items in the <see cref="T:System.Windows.Forms.ListView" /> control.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700026D RID: 621
		// (get) Token: 0x06000976 RID: 2422 RVA: 0x000279B2 File Offset: 0x00025BB2
		[Editor("System.Windows.Forms.Design.ListViewItemCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Localizable(true)]
		[MergableProperty(false)]
		public ListView.ListViewItemCollection Items
		{
			get
			{
				return this.items;
			}
		}

		/// <summary>Gets or sets a value indicating whether the user can edit the labels of items in the control.</summary>
		/// <returns>true if the user can edit the labels of items at run time; otherwise, false. The default is false.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700026E RID: 622
		// (get) Token: 0x06000977 RID: 2423 RVA: 0x000279BA File Offset: 0x00025BBA
		// (set) Token: 0x06000978 RID: 2424 RVA: 0x000279C2 File Offset: 0x00025BC2
		[DefaultValue(false)]
		public bool LabelEdit
		{
			get
			{
				return this.label_edit;
			}
			set
			{
				if (value != this.label_edit)
				{
					this.label_edit = value;
					this.OnUIALabelEditChanged();
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether item labels wrap when items are displayed in the control as icons.</summary>
		/// <returns>true if item labels wrap when items are displayed as icons; otherwise, false. The default is true.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700026F RID: 623
		// (get) Token: 0x06000979 RID: 2425 RVA: 0x000279DA File Offset: 0x00025BDA
		// (set) Token: 0x0600097A RID: 2426 RVA: 0x000279E2 File Offset: 0x00025BE2
		[DefaultValue(true)]
		[Localizable(true)]
		public bool LabelWrap
		{
			get
			{
				return this.label_wrap;
			}
			set
			{
				if (this.label_wrap != value)
				{
					this.label_wrap = value;
					this.Redraw(true);
				}
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.ImageList" /> to use when displaying items as large icons in the control.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.ImageList" /> that contains the icons to use when the <see cref="P:System.Windows.Forms.ListView.View" /> property is set to <see cref="F:System.Windows.Forms.View.LargeIcon" />. The default is null.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000270 RID: 624
		// (get) Token: 0x0600097B RID: 2427 RVA: 0x000279FB File Offset: 0x00025BFB
		// (set) Token: 0x0600097C RID: 2428 RVA: 0x00027A03 File Offset: 0x00025C03
		[DefaultValue(null)]
		public ImageList LargeImageList
		{
			get
			{
				return this.large_image_list;
			}
			set
			{
				this.large_image_list = value;
				this.Redraw(true);
			}
		}

		/// <summary>Gets or sets the sorting comparer for the control.</summary>
		/// <returns>An <see cref="T:System.Collections.IComparer" /> that represents the sorting comparer for the control.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000271 RID: 625
		// (get) Token: 0x0600097D RID: 2429 RVA: 0x00027A13 File Offset: 0x00025C13
		// (set) Token: 0x0600097E RID: 2430 RVA: 0x00027A3B File Offset: 0x00025C3B
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public IComparer ListViewItemSorter
		{
			get
			{
				if (this.View != View.SmallIcon && this.View != View.LargeIcon && this.item_sorter is ListView.ItemComparer)
				{
					return null;
				}
				return this.item_sorter;
			}
			set
			{
				if (this.item_sorter != value)
				{
					this.item_sorter = value;
					this.Sort();
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether multiple items can be selected.</summary>
		/// <returns>true if multiple items in the control can be selected at one time; otherwise, false. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000272 RID: 626
		// (get) Token: 0x0600097F RID: 2431 RVA: 0x00027A53 File Offset: 0x00025C53
		// (set) Token: 0x06000980 RID: 2432 RVA: 0x00027A5B File Offset: 0x00025C5B
		[DefaultValue(true)]
		public bool MultiSelect
		{
			get
			{
				return this.multiselect;
			}
			set
			{
				if (value != this.multiselect)
				{
					this.multiselect = value;
					this.OnUIAMultiSelectChanged();
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.ListView" /> control is drawn by the operating system or by code that you provide.</summary>
		/// <returns>true if the <see cref="T:System.Windows.Forms.ListView" /> control is drawn by code that you provide; false if the <see cref="T:System.Windows.Forms.ListView" /> control is drawn by the operating system. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06000981 RID: 2433 RVA: 0x00027A73 File Offset: 0x00025C73
		[DefaultValue(false)]
		public bool OwnerDraw
		{
			get
			{
				return this.owner_draw;
			}
		}

		/// <summary>Gets or sets a value indicating whether the control is laid out from right to left.</summary>
		/// <returns>true to indicate the <see cref="T:System.Windows.Forms.ListView" /> control is laid out from right to left; otherwise, false. </returns>
		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06000982 RID: 2434 RVA: 0x00027A7B File Offset: 0x00025C7B
		[MonoTODO("RTL not supported")]
		[Localizable(true)]
		[DefaultValue(false)]
		public virtual bool RightToLeftLayout
		{
			get
			{
				return this.right_to_left_layout;
			}
		}

		/// <summary>Gets the indexes of the selected items in the control.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ListView.SelectedIndexCollection" /> that contains the indexes of the selected items. If no items are currently selected, an empty <see cref="T:System.Windows.Forms.ListView.SelectedIndexCollection" /> is returned.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06000983 RID: 2435 RVA: 0x00027A83 File Offset: 0x00025C83
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public ListView.SelectedIndexCollection SelectedIndices
		{
			get
			{
				return this.selected_indices;
			}
		}

		/// <summary>Gets the items that are selected in the control.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ListView.SelectedListViewItemCollection" /> that contains the items that are selected in the control. If no items are currently selected, an empty <see cref="T:System.Windows.Forms.ListView.SelectedListViewItemCollection" /> is returned.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06000984 RID: 2436 RVA: 0x00027A8B File Offset: 0x00025C8B
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public ListView.SelectedListViewItemCollection SelectedItems
		{
			get
			{
				return this.selected_items;
			}
		}

		/// <summary>Gets the collection of <see cref="T:System.Windows.Forms.ListViewGroup" /> objects assigned to the control.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ListViewGroupCollection" /> that contains all the groups in the <see cref="T:System.Windows.Forms.ListView" /> control.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06000985 RID: 2437 RVA: 0x00027A93 File Offset: 0x00025C93
		[Localizable(true)]
		[MergableProperty(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Editor("System.Windows.Forms.Design.ListViewGroupCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		public ListViewGroupCollection Groups
		{
			get
			{
				return this.groups;
			}
		}

		/// <summary>Gets or sets a value indicating whether ToolTips are shown for the <see cref="T:System.Windows.Forms.ListViewItem" /> objects contained in the <see cref="T:System.Windows.Forms.ListView" />.</summary>
		/// <returns>true if <see cref="T:System.Windows.Forms.ListViewItem" /> ToolTips should be shown; otherwise, false. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06000986 RID: 2438 RVA: 0x00027A9B File Offset: 0x00025C9B
		[DefaultValue(false)]
		public bool ShowItemToolTips
		{
			get
			{
				return this.show_item_tooltips;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.ImageList" /> to use when displaying items as small icons in the control.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.ImageList" /> that contains the icons to use when the <see cref="P:System.Windows.Forms.ListView.View" /> property is set to <see cref="F:System.Windows.Forms.View.SmallIcon" />. The default is null.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06000987 RID: 2439 RVA: 0x00027AA3 File Offset: 0x00025CA3
		// (set) Token: 0x06000988 RID: 2440 RVA: 0x00027AAB File Offset: 0x00025CAB
		[DefaultValue(null)]
		public ImageList SmallImageList
		{
			get
			{
				return this.small_image_list;
			}
			set
			{
				this.small_image_list = value;
				this.Redraw(true);
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.ImageList" /> associated with application-defined states in the control.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.ImageList" /> that contains a set of state images that can be used to indicate an application-defined state of an item. The default is null.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700027A RID: 634
		// (get) Token: 0x06000989 RID: 2441 RVA: 0x00027ABB File Offset: 0x00025CBB
		[DefaultValue(null)]
		public ImageList StateImageList
		{
			get
			{
				return this.state_image_list;
			}
		}

		/// <summary>This property is not relevant for this class.</summary>
		/// <returns>The text to display in the <see cref="T:System.Windows.Forms.ListView" /> control.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700027B RID: 635
		// (get) Token: 0x0600098A RID: 2442 RVA: 0x000043B4 File Offset: 0x000025B4
		// (set) Token: 0x0600098B RID: 2443 RVA: 0x00027AC3 File Offset: 0x00025CC3
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
				if (value == base.Text)
				{
					return;
				}
				base.Text = value;
				this.Redraw(true);
			}
		}

		/// <summary>Gets or sets the size of the tiles shown in tile view.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that contains the new tile size.</returns>
		// Token: 0x1700027C RID: 636
		// (get) Token: 0x0600098C RID: 2444 RVA: 0x00027AE2 File Offset: 0x00025CE2
		[Browsable(true)]
		public Size TileSize
		{
			get
			{
				return this.tile_size;
			}
		}

		/// <summary>Gets or sets how items are displayed in the control.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.View" /> values. The default is <see cref="F:System.Windows.Forms.View.LargeIcon" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value specified is not one of the <see cref="T:System.Windows.Forms.View" /> values. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700027D RID: 637
		// (get) Token: 0x0600098D RID: 2445 RVA: 0x00027AEA File Offset: 0x00025CEA
		// (set) Token: 0x0600098E RID: 2446 RVA: 0x00027AF4 File Offset: 0x00025CF4
		[DefaultValue(View.LargeIcon)]
		public View View
		{
			get
			{
				return this.view;
			}
			set
			{
				if (!Enum.IsDefined(typeof(View), value))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(View));
				}
				if (this.view != value)
				{
					if (this.CheckBoxes && value == View.Tile)
					{
						throw new NotSupportedException("CheckBoxes are not supported in Tile view. Choose a different view or set CheckBoxes to false.");
					}
					if (this.VirtualMode && value == View.Tile)
					{
						throw new NotSupportedException("VirtualMode is not supported in Tile view. Choose a different view or set ViewMode to false.");
					}
					this.h_scroll.Value = (this.v_scroll.Value = 0);
					this.view = value;
					this.Redraw(true);
					this.OnUIAViewChanged();
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether you have provided your own data-management operations for the <see cref="T:System.Windows.Forms.ListView" /> control.</summary>
		/// <returns>true if <see cref="T:System.Windows.Forms.ListView" /> uses data-management operations that you provide; otherwise, false. The default is false.</returns>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="P:System.Windows.Forms.ListView.VirtualMode" /> is set to true and one of the following conditions exist:<see cref="P:System.Windows.Forms.ListView.VirtualListSize" /> is greater than 0 and <see cref="E:System.Windows.Forms.ListView.RetrieveVirtualItem" /> is not handled.-or-<see cref="P:System.Windows.Forms.ListView.Items" />, <see cref="P:System.Windows.Forms.ListView.CheckedItems" />, or <see cref="P:System.Windows.Forms.ListView.SelectedItems" /> contains items.-or-Edits are made to <see cref="P:System.Windows.Forms.ListView.Items" />.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700027E RID: 638
		// (get) Token: 0x0600098F RID: 2447 RVA: 0x00027B93 File Offset: 0x00025D93
		[DefaultValue(false)]
		[RefreshProperties(RefreshProperties.Repaint)]
		public bool VirtualMode
		{
			get
			{
				return this.virtual_mode;
			}
		}

		/// <summary>Gets or sets the number of <see cref="T:System.Windows.Forms.ListViewItem" /> objects contained in the list when in virtual mode.</summary>
		/// <returns>The number of <see cref="T:System.Windows.Forms.ListViewItem" /> objects contained in the <see cref="T:System.Windows.Forms.ListView" /> when in virtual mode.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <see cref="P:System.Windows.Forms.ListView.VirtualListSize" /> is set to a value less than 0.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="P:System.Windows.Forms.ListView.VirtualMode" /> is set to true, <see cref="P:System.Windows.Forms.ListView.VirtualListSize" /> is greater than 0, and <see cref="E:System.Windows.Forms.ListView.RetrieveVirtualItem" /> is not handled.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700027F RID: 639
		// (get) Token: 0x06000990 RID: 2448 RVA: 0x00027B9B File Offset: 0x00025D9B
		[DefaultValue(0)]
		[RefreshProperties(RefreshProperties.Repaint)]
		public int VirtualListSize
		{
			get
			{
				return this.virtual_list_size;
			}
		}

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x06000991 RID: 2449 RVA: 0x00027BA4 File Offset: 0x00025DA4
		internal int FirstVisibleIndex
		{
			get
			{
				if (this.items.Count == 0)
				{
					return 0;
				}
				if (this.h_marker == 0 && this.v_marker == 0)
				{
					return 0;
				}
				Size itemSize = this.ItemSize;
				if (this.virtual_mode)
				{
					int num = 0;
					switch (this.view)
					{
					case View.LargeIcon:
					case View.SmallIcon:
						num = this.v_marker / (itemSize.Height + this.y_spacing) * this.cols;
						break;
					case View.Details:
						num = this.v_marker / itemSize.Height;
						break;
					case View.List:
						num = this.h_marker / (itemSize.Width * this.x_spacing) * this.rows;
						break;
					}
					if (num >= this.items.Count)
					{
						num = this.items.Count;
					}
					return num;
				}
				for (int i = 0; i < this.items.Count; i++)
				{
					Rectangle rectangle = new Rectangle(this.GetItemLocation(i), itemSize);
					if (rectangle.Right >= 0 && rectangle.Bottom >= 0)
					{
						return i;
					}
				}
				return 0;
			}
		}

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x06000992 RID: 2450 RVA: 0x00027CAC File Offset: 0x00025EAC
		internal int LastVisibleIndex
		{
			get
			{
				for (int i = this.FirstVisibleIndex; i < this.Items.Count; i++)
				{
					if (this.View == View.List || this.Alignment == ListViewAlignment.Left)
					{
						if (this.GetItemLocation(i).X > this.item_control.ClientRectangle.Right)
						{
							return i - 1;
						}
					}
					else if (this.GetItemLocation(i).Y > this.item_control.ClientRectangle.Bottom)
					{
						return i - 1;
					}
				}
				return this.Items.Count - 1;
			}
		}

		// Token: 0x06000993 RID: 2451 RVA: 0x00027D43 File Offset: 0x00025F43
		internal void OnSelectedIndexChanged()
		{
			if (this.is_selection_available)
			{
				this.OnSelectedIndexChanged(EventArgs.Empty);
			}
		}

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06000994 RID: 2452 RVA: 0x00027D58 File Offset: 0x00025F58
		internal int TotalWidth
		{
			get
			{
				return Math.Max(base.Width, this.layout_wd);
			}
		}

		// Token: 0x06000995 RID: 2453 RVA: 0x00027D6B File Offset: 0x00025F6B
		internal void Redraw(bool recalculate)
		{
			if (this.updating)
			{
				return;
			}
			if (this.virtual_mode && !base.IsHandleCreated)
			{
				return;
			}
			if (recalculate)
			{
				this.CalculateListView(this.alignment);
			}
			base.Invalidate(true);
		}

		// Token: 0x06000996 RID: 2454 RVA: 0x00027DA0 File Offset: 0x00025FA0
		internal Size GetChildColumnSize(int index)
		{
			Size size = Size.Empty;
			ColumnHeader columnHeader = this.columns[index];
			if (columnHeader.Width == -2)
			{
				Size size2 = Size.Ceiling(TextRenderer.MeasureString(columnHeader.Text, this.Font));
				size2.Width += 15;
				size = this.BiggestItem(index);
				if (size2.Width > size.Width)
				{
					size = size2;
				}
			}
			else
			{
				size = this.BiggestItem(index);
				if (size.IsEmpty)
				{
					size.Width = ThemeEngine.Current.ListViewEmptyColumnWidth;
					if (columnHeader.Text.Length > 0)
					{
						size.Height = Size.Ceiling(TextRenderer.MeasureString(columnHeader.Text, this.Font)).Height;
					}
					else
					{
						size.Height = this.Font.Height;
					}
				}
			}
			size.Height += 15;
			if (index == 0)
			{
				size.Width += this.CheckBoxSize.Width + 4;
				if (this.small_image_list != null)
				{
					size.Width += this.small_image_list.ImageSize.Width;
				}
			}
			return size;
		}

		// Token: 0x06000997 RID: 2455 RVA: 0x00027ED4 File Offset: 0x000260D4
		private Size BiggestItem(int col)
		{
			Size size = Size.Empty;
			Size size2 = Size.Empty;
			bool flag = this.small_image_list != null;
			if (this.virtual_mode && this.items.Count > 0)
			{
				ListViewItem listViewItem = this.items[0];
				size2 = Size.Ceiling(TextRenderer.MeasureString(listViewItem.SubItems[col].Text, this.Font));
				if (flag)
				{
					size2.Width += listViewItem.IndentCount * this.small_image_list.ImageSize.Width;
				}
			}
			else
			{
				foreach (object obj in this.items)
				{
					ListViewItem listViewItem2 = (ListViewItem)obj;
					if (col < listViewItem2.SubItems.Count)
					{
						size = Size.Ceiling(TextRenderer.MeasureString(listViewItem2.SubItems[col].Text, this.Font));
						if (flag)
						{
							size.Width += listViewItem2.IndentCount * this.small_image_list.ImageSize.Width;
						}
						if (size.Width > size2.Width)
						{
							size2 = size;
						}
					}
				}
			}
			if (!size2.IsEmpty && this.view == View.Details)
			{
				size2.Width += ThemeEngine.Current.ListViewItemPaddingWidth;
			}
			return size2;
		}

		// Token: 0x06000998 RID: 2456 RVA: 0x0002805C File Offset: 0x0002625C
		private void CalcTextSize()
		{
			this.text_size = Size.Empty;
			if (this.items.Count == 0)
			{
				return;
			}
			this.text_size = this.BiggestItem(0);
			if (this.view == View.LargeIcon && this.label_wrap)
			{
				Size empty = Size.Empty;
				if (this.check_boxes)
				{
					empty.Width += 2 * this.CheckBoxSize.Width;
				}
				int num = ((this.LargeImageList == null) ? 12 : this.LargeImageList.ImageSize.Width);
				empty.Width += num + 30;
				if (this.text_size.Width > empty.Width)
				{
					this.text_size.Width = empty.Width;
					this.text_size.Height = this.text_size.Height * 2;
				}
			}
			else if (this.view == View.List)
			{
				int num2 = base.Width - (this.CheckBoxSize.Width - 2);
				if (this.small_image_list != null)
				{
					num2 -= this.small_image_list.ImageSize.Width;
				}
				if (this.text_size.Width > num2)
				{
					this.text_size.Width = num2;
				}
			}
			if (this.text_size.Height <= 0)
			{
				this.text_size.Height = this.Font.Height;
			}
			if (this.text_size.Width <= 0)
			{
				this.text_size.Width = base.Width;
			}
			this.text_size.Width = this.text_size.Width + 2;
			this.text_size.Height = this.text_size.Height + 2;
		}

		// Token: 0x06000999 RID: 2457 RVA: 0x00028204 File Offset: 0x00026404
		private void SetScrollValue(ScrollBar scrollbar, int val)
		{
			int num;
			if (scrollbar == this.h_scroll)
			{
				num = this.h_scroll.Maximum - this.h_scroll.LargeChange + 1;
			}
			else
			{
				num = this.v_scroll.Maximum - this.v_scroll.LargeChange + 1;
			}
			if (val > num)
			{
				val = num;
			}
			else if (val < scrollbar.Minimum)
			{
				val = scrollbar.Minimum;
			}
			scrollbar.Value = val;
		}

		// Token: 0x0600099A RID: 2458 RVA: 0x00028271 File Offset: 0x00026471
		private void Scroll(ScrollBar scrollbar, int delta)
		{
			if (delta == 0 || !scrollbar.Visible)
			{
				return;
			}
			this.SetScrollValue(scrollbar, scrollbar.Value + delta);
		}

		// Token: 0x0600099B RID: 2459 RVA: 0x00028290 File Offset: 0x00026490
		private void CalculateScrollBars()
		{
			Rectangle clientRectangle = base.ClientRectangle;
			int num = clientRectangle.Height;
			int num2 = clientRectangle.Width;
			if (!this.scrollable)
			{
				this.h_scroll.Visible = false;
				this.v_scroll.Visible = false;
				this.item_control.Size = new Size(num2, num);
				this.header_control.Width = num2;
				return;
			}
			if (clientRectangle.Height < 0 || clientRectangle.Width < 0)
			{
				return;
			}
			if (this.layout_wd > clientRectangle.Right)
			{
				this.h_scroll.Visible = true;
				if (this.layout_ht + this.h_scroll.Height > clientRectangle.Bottom)
				{
					this.v_scroll.Visible = true;
				}
				else
				{
					this.v_scroll.Visible = false;
				}
			}
			else if (this.layout_ht > clientRectangle.Bottom)
			{
				this.v_scroll.Visible = true;
				if (this.layout_wd + this.v_scroll.Width > clientRectangle.Right)
				{
					this.h_scroll.Visible = true;
				}
				else
				{
					this.h_scroll.Visible = false;
				}
			}
			else
			{
				this.h_scroll.Visible = false;
				this.v_scroll.Visible = false;
			}
			Size itemSize = this.ItemSize;
			if (this.h_scroll.is_visible)
			{
				this.h_scroll.Location = new Point(clientRectangle.X, clientRectangle.Bottom - this.h_scroll.Height);
				this.h_scroll.Minimum = 0;
				if (this.v_scroll.Visible)
				{
					this.h_scroll.Maximum = this.layout_wd + this.v_scroll.Width;
					this.h_scroll.Width = clientRectangle.Width - this.v_scroll.Width;
				}
				else
				{
					this.h_scroll.Maximum = this.layout_wd;
					this.h_scroll.Width = clientRectangle.Width;
				}
				if (this.view == View.List)
				{
					this.h_scroll.SmallChange = itemSize.Width + ThemeEngine.Current.ListViewHorizontalSpacing;
				}
				else
				{
					this.h_scroll.SmallChange = this.Font.Height;
				}
				this.h_scroll.LargeChange = clientRectangle.Width;
				num -= this.h_scroll.Height;
			}
			if (this.v_scroll.is_visible)
			{
				this.v_scroll.Location = new Point(clientRectangle.Right - this.v_scroll.Width, clientRectangle.Y);
				this.v_scroll.Minimum = 0;
				if (this.h_scroll.Visible)
				{
					this.v_scroll.Maximum = this.layout_ht + this.h_scroll.Height;
					this.v_scroll.Height = ((clientRectangle.Height > this.h_scroll.Height) ? (clientRectangle.Height - this.h_scroll.Height) : 0);
				}
				else
				{
					this.v_scroll.Maximum = this.layout_ht;
					this.v_scroll.Height = clientRectangle.Height;
				}
				if (this.view == View.Details)
				{
					int num3 = this.header_control.Height + itemSize.Height;
					this.v_scroll.LargeChange = ((this.v_scroll.Height > num3) ? (this.v_scroll.Height - num3) : 0);
					this.v_scroll.Maximum = ((this.v_scroll.Maximum > num3) ? (this.v_scroll.Maximum - num3) : 0);
				}
				else
				{
					this.v_scroll.LargeChange = this.v_scroll.Height;
				}
				this.v_scroll.SmallChange = itemSize.Height;
				num2 -= this.v_scroll.Width;
			}
			this.item_control.Size = new Size(num2, num);
			if (this.header_control.is_visible)
			{
				this.header_control.Width = num2;
			}
		}

		// Token: 0x0600099C RID: 2460 RVA: 0x0002867D File Offset: 0x0002687D
		internal ColumnHeader GetReorderedColumn(int index)
		{
			if (this.reordered_column_indices == null)
			{
				return this.Columns[index];
			}
			return this.Columns[this.reordered_column_indices[index]];
		}

		// Token: 0x0600099D RID: 2461 RVA: 0x000286A8 File Offset: 0x000268A8
		internal void ReorderColumn(ColumnHeader col, int index, bool fireEvent)
		{
			if (fireEvent)
			{
				ColumnReorderedEventHandler columnReorderedEventHandler = (ColumnReorderedEventHandler)base.Events[ListView.ColumnReorderedEvent];
				if (columnReorderedEventHandler != null)
				{
					ColumnReorderedEventArgs columnReorderedEventArgs = new ColumnReorderedEventArgs(col.Index, index, col);
					columnReorderedEventHandler(this, columnReorderedEventArgs);
					if (columnReorderedEventArgs.Cancel)
					{
						this.header_control.Invalidate();
						this.item_control.Invalidate();
						return;
					}
				}
			}
			int count = this.Columns.Count;
			if (this.reordered_column_indices == null)
			{
				this.reordered_column_indices = new int[count];
				for (int i = 0; i < count; i++)
				{
					this.reordered_column_indices[i] = i;
				}
			}
			if (this.reordered_column_indices[index] == col.Index)
			{
				return;
			}
			int[] array = this.reordered_column_indices;
			int[] array2 = new int[count];
			int num = 0;
			for (int j = 0; j < count; j++)
			{
				if (num < count && array[num] == col.Index)
				{
					num++;
				}
				if (j == index)
				{
					array2[j] = col.Index;
				}
				else
				{
					array2[j] = array[num++];
				}
			}
			this.ReorderColumns(array2, true);
		}

		// Token: 0x0600099E RID: 2462 RVA: 0x000287B0 File Offset: 0x000269B0
		internal void ReorderColumns(int[] display_indices, bool redraw)
		{
			this.reordered_column_indices = display_indices;
			for (int i = 0; i < this.Columns.Count; i++)
			{
				this.Columns[i].InternalDisplayIndex = this.reordered_column_indices[i];
			}
			if (redraw && this.view == View.Details && base.IsHandleCreated)
			{
				this.LayoutDetails();
				this.header_control.Invalidate();
				this.item_control.Invalidate();
			}
		}

		// Token: 0x0600099F RID: 2463 RVA: 0x00028824 File Offset: 0x00026A24
		internal void AddColumn(ColumnHeader newCol, int index, bool redraw)
		{
			int count = this.Columns.Count;
			newCol.SetListView(this);
			int[] array = new int[count];
			for (int i = 0; i < count; i++)
			{
				ColumnHeader columnHeader = this.Columns[i];
				if (i == index)
				{
					array[i] = index;
				}
				else
				{
					int internalDisplayIndex = columnHeader.InternalDisplayIndex;
					if (internalDisplayIndex < index)
					{
						array[i] = internalDisplayIndex;
					}
					else
					{
						array[i] = internalDisplayIndex + 1;
					}
				}
			}
			this.ReorderColumns(array, redraw);
			base.Invalidate();
		}

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x060009A0 RID: 2464 RVA: 0x00028898 File Offset: 0x00026A98
		private Size LargeIconItemSize
		{
			get
			{
				int num = ((this.LargeImageList == null) ? 12 : this.LargeImageList.ImageSize.Width);
				int num2 = ((this.LargeImageList == null) ? 2 : this.LargeImageList.ImageSize.Height);
				int num3 = this.text_size.Height + 2 + Math.Max(this.CheckBoxSize.Height, num2);
				int num4 = Math.Max(this.text_size.Width, num);
				if (this.check_boxes)
				{
					num4 += 2 + this.CheckBoxSize.Width;
				}
				return new Size(num4, num3);
			}
		}

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x060009A1 RID: 2465 RVA: 0x00028940 File Offset: 0x00026B40
		private Size SmallIconItemSize
		{
			get
			{
				int num = ((this.SmallImageList == null) ? 0 : this.SmallImageList.ImageSize.Width);
				int num2 = ((this.SmallImageList == null) ? 0 : this.SmallImageList.ImageSize.Height);
				int num3 = Math.Max(this.text_size.Height, Math.Max(this.CheckBoxSize.Height, num2));
				int num4 = this.text_size.Width + num;
				if (this.check_boxes)
				{
					num4 += 2 + this.CheckBoxSize.Width;
				}
				return new Size(num4, num3);
			}
		}

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x060009A2 RID: 2466 RVA: 0x000289E4 File Offset: 0x00026BE4
		private Size TileItemSize
		{
			get
			{
				if (this.tile_size == Size.Empty)
				{
					int num = ((this.LargeImageList == null) ? 0 : this.LargeImageList.ImageSize.Width);
					int num2 = ((this.LargeImageList == null) ? 0 : this.LargeImageList.ImageSize.Height);
					int num3 = (int)this.Font.Size * ThemeEngine.Current.ListViewTileWidthFactor + num + 4;
					int num4 = Math.Max((int)this.Font.Size * ThemeEngine.Current.ListViewTileHeightFactor, num2);
					this.tile_size = new Size(num3, num4);
				}
				return this.tile_size;
			}
		}

		// Token: 0x060009A3 RID: 2467 RVA: 0x00028A94 File Offset: 0x00026C94
		private int GetDetailsItemHeight()
		{
			int num = (this.CheckBoxes ? this.CheckBoxSize.Height : 0);
			int num2 = ((this.SmallImageList == null) ? 0 : this.SmallImageList.ImageSize.Height);
			return Math.Max(Math.Max(num, this.text_size.Height), num2);
		}

		// Token: 0x060009A4 RID: 2468 RVA: 0x00028AF0 File Offset: 0x00026CF0
		private void SetItemLocation(int index, int x, int y, int row, int col)
		{
			Point point = this.items_location[index];
			if (point.X == x && point.Y == y)
			{
				return;
			}
			this.items_location[index] = new Point(x, y);
			this.items_matrix_location[index] = new ListView.ItemMatrixLocation(row, col);
			this.reordered_items_indices[index] = index;
		}

		// Token: 0x060009A5 RID: 2469 RVA: 0x00028B50 File Offset: 0x00026D50
		private int GetDefaultGroupItems()
		{
			int num = 0;
			using (IEnumerator enumerator = this.items.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (((ListViewItem)enumerator.Current).Group == null)
					{
						num++;
					}
				}
			}
			return num;
		}

		// Token: 0x060009A6 RID: 2470 RVA: 0x00028BB0 File Offset: 0x00026DB0
		private void CalculateRowsAndCols(Size item_size, bool left_aligned, int x_spacing, int y_spacing)
		{
			Rectangle clientRectangle = base.ClientRectangle;
			if (this.UseCustomColumnWidth)
			{
				this.CalculateCustomColumnWidth();
			}
			if (this.UsingGroups)
			{
				this.rows = 0;
				this.cols = 0;
				int num = 0;
				this.groups.DefaultGroup.ItemCount = this.GetDefaultGroupItems();
				for (int i = 0; i < this.groups.InternalCount; i++)
				{
					ListViewGroup internalGroup = this.groups.GetInternalGroup(i);
					int actualItemCount = internalGroup.GetActualItemCount();
					if (actualItemCount != 0)
					{
						int num2 = (int)Math.Floor((double)(clientRectangle.Width - this.v_scroll.Width + x_spacing) / (double)(item_size.Width + x_spacing));
						if (num2 <= 0)
						{
							num2 = 1;
						}
						int num3 = (int)Math.Ceiling((double)actualItemCount / (double)num2);
						internalGroup.starting_row = this.rows;
						internalGroup.rows = num3;
						internalGroup.starting_item = num;
						internalGroup.current_item = 0;
						this.cols = Math.Max(num2, this.cols);
						this.rows += num3;
						num += actualItemCount;
					}
				}
			}
			else if (left_aligned)
			{
				this.rows = (int)Math.Floor((double)(clientRectangle.Height - this.h_scroll.Height + y_spacing) / (double)(item_size.Height + y_spacing));
				if (this.rows <= 0)
				{
					this.rows = 1;
				}
				this.cols = (int)Math.Ceiling((double)this.items.Count / (double)this.rows);
			}
			else
			{
				if (this.UseCustomColumnWidth)
				{
					this.cols = (int)Math.Floor((double)(clientRectangle.Width - this.v_scroll.Width) / (double)this.custom_column_width);
				}
				else
				{
					this.cols = (int)Math.Floor((double)(clientRectangle.Width - this.v_scroll.Width + x_spacing) / (double)(item_size.Width + x_spacing));
				}
				if (this.cols < 1)
				{
					this.cols = 1;
				}
				this.rows = (int)Math.Ceiling((double)this.items.Count / (double)this.cols);
			}
			this.item_index_matrix = new int[this.rows, this.cols];
		}

		// Token: 0x060009A7 RID: 2471 RVA: 0x00028DD8 File Offset: 0x00026FD8
		private void CalculateCustomColumnWidth()
		{
			int num = int.MaxValue;
			for (int i = 0; i < this.columns.Count; i++)
			{
				int width = this.columns[i].Width;
				if (width < num)
				{
					num = width;
				}
			}
			this.custom_column_width = num;
		}

		// Token: 0x060009A8 RID: 2472 RVA: 0x00028E20 File Offset: 0x00027020
		private void LayoutIcons(Size item_size, bool left_aligned, int x_spacing, int y_spacing)
		{
			this.header_control.Visible = false;
			this.header_control.Size = Size.Empty;
			this.item_control.Visible = true;
			this.item_control.Location = Point.Empty;
			this.ItemSize = item_size;
			this.x_spacing = x_spacing;
			this.y_spacing = y_spacing;
			if (this.items.Count == 0)
			{
				return;
			}
			Size size = item_size;
			this.CalculateRowsAndCols(size, left_aligned, x_spacing, y_spacing);
			this.layout_wd = (this.UseCustomColumnWidth ? (this.cols * this.custom_column_width) : (this.cols * (size.Width + x_spacing) - x_spacing));
			this.layout_ht = this.rows * (size.Height + y_spacing) - y_spacing;
			if (this.virtual_mode)
			{
				this.item_control.Size = new Size(this.layout_wd, this.layout_ht);
				return;
			}
			bool usingGroups = this.UsingGroups;
			if (usingGroups)
			{
				this.CalculateGroupsLayout(size, y_spacing, 0);
			}
			int num = 0;
			int num2 = 0;
			for (int i = 0; i < this.items.Count; i++)
			{
				ListViewItem listViewItem = this.items[i];
				int num4;
				int num5;
				int num6;
				if (usingGroups)
				{
					ListViewGroup listViewGroup = listViewItem.Group;
					if (listViewGroup == null)
					{
						listViewGroup = this.groups.DefaultGroup;
					}
					Point items_area_location = listViewGroup.items_area_location;
					ListViewGroup listViewGroup2 = listViewGroup;
					int current_item = listViewGroup2.current_item;
					listViewGroup2.current_item = current_item + 1;
					int num3 = current_item;
					int starting_row = listViewGroup.starting_row;
					num4 = listViewGroup.starting_item + num3;
					num = num3 / this.cols;
					num2 = num3 % this.cols;
					num5 = (this.UseCustomColumnWidth ? (num2 * this.custom_column_width) : (num2 * (item_size.Width + x_spacing)));
					num6 = num * (item_size.Height + y_spacing) + items_area_location.Y;
					this.SetItemLocation(num4, num5, num6, num + starting_row, num2);
					this.SetItemAtDisplayIndex(num4, i);
					this.item_index_matrix[num + starting_row, num2] = i;
				}
				else
				{
					num5 = (this.UseCustomColumnWidth ? (num2 * this.custom_column_width) : (num2 * (item_size.Width + x_spacing)));
					num6 = num * (item_size.Height + y_spacing);
					num4 = i;
					this.SetItemLocation(i, num5, num6, num, num2);
					this.item_index_matrix[num, num2] = i;
					if (left_aligned)
					{
						num++;
						if (num == this.rows)
						{
							num = 0;
							num2++;
						}
					}
					else if (++num2 == this.cols)
					{
						num2 = 0;
						num++;
					}
				}
				listViewItem.Layout();
				listViewItem.DisplayIndex = num4;
				listViewItem.SetPosition(new Point(num5, num6));
			}
			this.item_control.Size = new Size(this.layout_wd, this.layout_ht);
		}

		// Token: 0x060009A9 RID: 2473 RVA: 0x000290D8 File Offset: 0x000272D8
		private void CalculateGroupsLayout(Size item_size, int y_spacing, int y_origin)
		{
			int num = y_origin;
			bool flag = this.view == View.Details;
			for (int i = 0; i < this.groups.InternalCount; i++)
			{
				ListViewGroup internalGroup = this.groups.GetInternalGroup(i);
				if (internalGroup.ItemCount != 0)
				{
					num += this.LayoutGroupHeader(internalGroup, num, item_size.Height, y_spacing, flag ? internalGroup.ItemCount : internalGroup.rows);
				}
			}
			this.layout_ht = num;
		}

		// Token: 0x060009AA RID: 2474 RVA: 0x00029148 File Offset: 0x00027348
		private int LayoutGroupHeader(ListViewGroup group, int y_origin, int item_height, int y_spacing, int rows)
		{
			Rectangle clientRectangle = base.ClientRectangle;
			int num = this.Font.Height + 15;
			group.HeaderBounds = new Rectangle(0, y_origin, clientRectangle.Width - this.v_scroll.Width, num);
			group.items_area_location = new Point(0, y_origin + num);
			int num2 = (item_height + y_spacing) * rows;
			return num + num2 + 10;
		}

		// Token: 0x060009AB RID: 2475 RVA: 0x000291AC File Offset: 0x000273AC
		private void CalculateDetailsGroupItemsCount()
		{
			int num = 0;
			this.groups.DefaultGroup.ItemCount = this.GetDefaultGroupItems();
			for (int i = 0; i < this.groups.InternalCount; i++)
			{
				ListViewGroup internalGroup = this.groups.GetInternalGroup(i);
				int actualItemCount = internalGroup.GetActualItemCount();
				if (actualItemCount != 0)
				{
					internalGroup.starting_item = num;
					internalGroup.current_item = 0;
					num += actualItemCount;
				}
			}
		}

		// Token: 0x060009AC RID: 2476 RVA: 0x00029210 File Offset: 0x00027410
		private void LayoutHeader()
		{
			int num = 0;
			for (int i = 0; i < this.Columns.Count; i++)
			{
				ColumnHeader reorderedColumn = this.GetReorderedColumn(i);
				reorderedColumn.X = num;
				reorderedColumn.Y = 0;
				reorderedColumn.CalcColumnHeader();
				num += reorderedColumn.Wd;
			}
			this.layout_wd = num;
			if (num < base.ClientRectangle.Width)
			{
				num = base.ClientRectangle.Width;
			}
			if (this.header_style == ColumnHeaderStyle.None)
			{
				this.header_control.Visible = false;
				this.header_control.Size = Size.Empty;
				this.layout_wd = base.ClientRectangle.Width;
				return;
			}
			this.header_control.Width = num;
			this.header_control.Height = ((this.columns.Count > 0) ? this.columns[0].Ht : ThemeEngine.Current.ListViewGetHeaderHeight(this, this.Font));
			this.header_control.Visible = true;
		}

		// Token: 0x060009AD RID: 2477 RVA: 0x00029310 File Offset: 0x00027510
		private void LayoutDetails()
		{
			this.LayoutHeader();
			if (this.columns.Count == 0)
			{
				this.item_control.Visible = false;
				this.layout_wd = base.ClientRectangle.Width;
				this.layout_ht = base.ClientRectangle.Height;
				return;
			}
			this.item_control.Visible = true;
			this.item_control.Location = Point.Empty;
			this.item_control.Width = base.ClientRectangle.Width;
			this.AdjustChildrenZOrder();
			int detailsItemHeight = this.GetDetailsItemHeight();
			this.ItemSize = new Size(0, detailsItemHeight);
			int num = this.header_control.Height;
			this.layout_ht = num + detailsItemHeight * this.items.Count;
			if (this.items.Count > 0 && this.grid_lines)
			{
				this.layout_ht += 2;
			}
			bool usingGroups = this.UsingGroups;
			if (usingGroups)
			{
				this.CalculateDetailsGroupItemsCount();
				this.CalculateGroupsLayout(this.ItemSize, 2, num);
			}
			if (this.virtual_mode)
			{
				return;
			}
			for (int i = 0; i < this.items.Count; i++)
			{
				ListViewItem listViewItem = this.items[i];
				int num3;
				int num4;
				if (usingGroups)
				{
					ListViewGroup listViewGroup = listViewItem.Group;
					if (listViewGroup == null)
					{
						listViewGroup = this.groups.DefaultGroup;
					}
					ListViewGroup listViewGroup2 = listViewGroup;
					int current_item = listViewGroup2.current_item;
					listViewGroup2.current_item = current_item + 1;
					int num2 = current_item;
					Point items_area_location = listViewGroup.items_area_location;
					num3 = listViewGroup.starting_item + num2;
					num4 = (num = num2 * (detailsItemHeight + 2) + items_area_location.Y);
					this.SetItemLocation(num3, 0, num4, 0, 0);
					this.SetItemAtDisplayIndex(num3, i);
				}
				else
				{
					num3 = i;
					num4 = num;
					this.SetItemLocation(i, 0, num4, 0, 0);
					num += detailsItemHeight;
				}
				listViewItem.Layout();
				listViewItem.DisplayIndex = num3;
				listViewItem.SetPosition(new Point(0, num4));
			}
		}

		// Token: 0x060009AE RID: 2478 RVA: 0x000294FC File Offset: 0x000276FC
		private void AdjustChildrenZOrder()
		{
			base.SuspendLayout();
			base.Controls.ClearImplicit();
			base.Controls.AddImplicit(this.header_control);
			base.Controls.AddImplicit(this.item_control);
			base.Controls.AddImplicit(this.h_scroll);
			base.Controls.AddImplicit(this.v_scroll);
			base.ResumeLayout();
		}

		// Token: 0x060009AF RID: 2479 RVA: 0x00029564 File Offset: 0x00027764
		private void AdjustItemsPositionArray(int count)
		{
			if (this.virtual_mode)
			{
				return;
			}
			if (this.items_location.Length >= count)
			{
				return;
			}
			count = Math.Max(count, this.items_location.Length * 2);
			this.items_location = new Point[count];
			this.items_matrix_location = new ListView.ItemMatrixLocation[count];
			this.reordered_items_indices = new int[count];
		}

		// Token: 0x060009B0 RID: 2480 RVA: 0x000295BC File Offset: 0x000277BC
		private void CalculateListView(ListViewAlignment align)
		{
			this.CalcTextSize();
			this.AdjustItemsPositionArray(this.items.Count);
			switch (this.view)
			{
			case View.LargeIcon:
				break;
			case View.Details:
				this.LayoutDetails();
				goto IL_00DF;
			case View.SmallIcon:
				this.LayoutIcons(this.SmallIconItemSize, this.alignment == ListViewAlignment.Left, ThemeEngine.Current.ListViewHorizontalSpacing, 2);
				goto IL_00DF;
			case View.List:
				this.LayoutIcons(this.SmallIconItemSize, true, ThemeEngine.Current.ListViewHorizontalSpacing, 2);
				goto IL_00DF;
			case View.Tile:
				if (Application.VisualStylesEnabled)
				{
					this.LayoutIcons(this.TileItemSize, this.alignment == ListViewAlignment.Left, ThemeEngine.Current.ListViewHorizontalSpacing, ThemeEngine.Current.ListViewVerticalSpacing);
					goto IL_00DF;
				}
				break;
			default:
				goto IL_00DF;
			}
			this.LayoutIcons(this.LargeIconItemSize, this.alignment == ListViewAlignment.Left, ThemeEngine.Current.ListViewHorizontalSpacing, ThemeEngine.Current.ListViewVerticalSpacing);
			IL_00DF:
			this.CalculateScrollBars();
		}

		// Token: 0x060009B1 RID: 2481 RVA: 0x000296B0 File Offset: 0x000278B0
		internal Point GetItemLocation(int index)
		{
			Point point = Point.Empty;
			if (this.virtual_mode)
			{
				point = this.GetFixedItemLocation(index);
			}
			else
			{
				point = this.items_location[index];
			}
			point.X -= this.h_marker;
			point.Y -= this.v_marker;
			return point;
		}

		// Token: 0x060009B2 RID: 2482 RVA: 0x0002970C File Offset: 0x0002790C
		private Point GetFixedItemLocation(int index)
		{
			Point empty = Point.Empty;
			switch (this.view)
			{
			case View.LargeIcon:
			case View.SmallIcon:
				empty.X = index % this.cols * (this.item_size.Width + this.x_spacing);
				empty.Y = index / this.cols * (this.item_size.Height + this.y_spacing);
				break;
			case View.Details:
				empty.Y = this.header_control.Height + index * this.item_size.Height;
				break;
			case View.List:
				empty.X = index / this.rows * (this.item_size.Width + this.x_spacing);
				empty.Y = index % this.rows * (this.item_size.Height + this.y_spacing);
				break;
			}
			return empty;
		}

		// Token: 0x060009B3 RID: 2483 RVA: 0x000297EE File Offset: 0x000279EE
		internal int GetItemIndex(int display_index)
		{
			if (this.virtual_mode)
			{
				return display_index;
			}
			return this.reordered_items_indices[display_index];
		}

		// Token: 0x060009B4 RID: 2484 RVA: 0x00029802 File Offset: 0x00027A02
		internal ListViewItem GetItemAtDisplayIndex(int display_index)
		{
			if (this.virtual_mode)
			{
				return this.items[display_index];
			}
			return this.items[this.reordered_items_indices[display_index]];
		}

		// Token: 0x060009B5 RID: 2485 RVA: 0x0002982C File Offset: 0x00027A2C
		internal void SetItemAtDisplayIndex(int display_index, int index)
		{
			this.reordered_items_indices[display_index] = index;
		}

		// Token: 0x060009B6 RID: 2486 RVA: 0x00029838 File Offset: 0x00027A38
		private bool KeySearchString(KeyEventArgs ke)
		{
			int tickCount = Environment.TickCount;
			if (this.keysearch_tickcnt > 0 && tickCount - this.keysearch_tickcnt > ListView.keysearch_keydelay)
			{
				this.keysearch_text = string.Empty;
			}
			if (!char.IsLetterOrDigit((char)ke.KeyCode))
			{
				return false;
			}
			this.keysearch_text += ((char)ke.KeyCode).ToString();
			this.keysearch_tickcnt = tickCount;
			int num = ((this.FocusedItem == null) ? 0 : this.FocusedItem.DisplayIndex);
			int num2 = ((num + 1 < this.Items.Count) ? (num + 1) : 0);
			ListViewItem listViewItem = this.FindItemWithText(this.keysearch_text, false, num2, true, true);
			if (listViewItem != null && num != listViewItem.DisplayIndex)
			{
				this.selected_indices.Clear();
				this.SetFocusedItem(listViewItem.DisplayIndex);
				listViewItem.Selected = true;
				this.EnsureVisible(this.GetItemIndex(listViewItem.DisplayIndex));
			}
			return true;
		}

		// Token: 0x060009B7 RID: 2487 RVA: 0x00029922 File Offset: 0x00027B22
		private void OnItemsChanged()
		{
			this.ResetSearchString();
		}

		// Token: 0x060009B8 RID: 2488 RVA: 0x0002992A File Offset: 0x00027B2A
		private void ResetSearchString()
		{
			this.keysearch_text = string.Empty;
		}

		// Token: 0x060009B9 RID: 2489 RVA: 0x00029938 File Offset: 0x00027B38
		private int GetAdjustedIndex(Keys key)
		{
			int num = -1;
			if (this.View == View.Details)
			{
				if (key <= Keys.PageDown)
				{
					if (key != Keys.PageUp)
					{
						if (key == Keys.PageDown)
						{
							int num2 = this.LastVisibleIndex;
							Rectangle rectangle = new Rectangle(this.GetItemLocation(num2), this.ItemSize);
							if (rectangle.Bottom > this.item_control.ClientRectangle.Bottom)
							{
								num2--;
							}
							if (this.FocusedItem.DisplayIndex == num2)
							{
								if (this.FocusedItem.DisplayIndex < this.Items.Count - 1)
								{
									int num3 = this.item_control.Height / this.ItemSize.Height - 1;
									num = this.FocusedItem.DisplayIndex + num3 - 1;
									if (num >= this.Items.Count)
									{
										num = this.Items.Count - 1;
									}
								}
							}
							else
							{
								num = num2;
							}
						}
					}
					else
					{
						int num4 = this.FirstVisibleIndex;
						if (this.GetItemLocation(num4).Y < 0)
						{
							num4++;
						}
						if (this.FocusedItem.DisplayIndex == num4)
						{
							if (num4 > 0)
							{
								int num5 = this.item_control.Height / this.ItemSize.Height - 1;
								num = num4 - num5 + 1;
								if (num < 0)
								{
									num = 0;
								}
							}
						}
						else
						{
							num = num4;
						}
					}
				}
				else if (key != Keys.Up)
				{
					if (key == Keys.Down)
					{
						num = this.FocusedItem.DisplayIndex + 1;
						if (num == this.items.Count)
						{
							num = -1;
						}
					}
				}
				else
				{
					num = this.FocusedItem.DisplayIndex - 1;
				}
				return num;
			}
			if (this.virtual_mode)
			{
				return this.GetFixedAdjustedIndex(key);
			}
			ListView.ItemMatrixLocation itemMatrixLocation = this.items_matrix_location[this.FocusedItem.DisplayIndex];
			int num6 = itemMatrixLocation.Row;
			int num7 = itemMatrixLocation.Col;
			int num8;
			switch (key)
			{
			case Keys.Left:
				if (num7 == 0)
				{
					return -1;
				}
				num8 = this.item_index_matrix[num6, num7 - 1];
				break;
			case Keys.Up:
				if (num6 == 0)
				{
					return -1;
				}
				while (this.item_index_matrix[num6 - 1, num7] == 0 && num6 != 1)
				{
					num7--;
					if (num7 < 0)
					{
						return -1;
					}
				}
				num8 = this.item_index_matrix[num6 - 1, num7];
				break;
			case Keys.Right:
				if (num7 == this.cols - 1)
				{
					return -1;
				}
				while (this.item_index_matrix[num6, num7 + 1] == 0)
				{
					num6--;
					if (num6 < 0)
					{
						return -1;
					}
				}
				num8 = this.item_index_matrix[num6, num7 + 1];
				break;
			case Keys.Down:
				if (num6 == this.rows - 1 || num6 == this.Items.Count - 1)
				{
					return -1;
				}
				while (this.item_index_matrix[num6 + 1, num7] == 0)
				{
					num7--;
					if (num7 < 0)
					{
						return -1;
					}
				}
				num8 = this.item_index_matrix[num6 + 1, num7];
				break;
			default:
				return -1;
			}
			return this.items[num8].DisplayIndex;
		}

		// Token: 0x060009BA RID: 2490 RVA: 0x00029C24 File Offset: 0x00027E24
		private int GetFixedAdjustedIndex(Keys key)
		{
			int num;
			switch (key)
			{
			case Keys.Left:
				if (this.view == View.List)
				{
					num = this.focused_item_index - this.rows;
				}
				else
				{
					num = this.focused_item_index - 1;
				}
				break;
			case Keys.Up:
				if (this.view != View.List)
				{
					num = this.focused_item_index - this.cols;
				}
				else
				{
					num = this.focused_item_index - 1;
				}
				break;
			case Keys.Right:
				if (this.view == View.List)
				{
					num = this.focused_item_index + this.rows;
				}
				else
				{
					num = this.focused_item_index + 1;
				}
				break;
			case Keys.Down:
				if (this.view != View.List)
				{
					num = this.focused_item_index + this.cols;
				}
				else
				{
					num = this.focused_item_index + 1;
				}
				break;
			default:
				return -1;
			}
			if (num < 0 || num >= this.items.Count)
			{
				num = this.focused_item_index;
			}
			return num;
		}

		// Token: 0x060009BB RID: 2491 RVA: 0x00029CFC File Offset: 0x00027EFC
		private bool SelectItems(ArrayList sel_items)
		{
			bool flag = false;
			foreach (object obj in this.SelectedItems)
			{
				ListViewItem listViewItem = (ListViewItem)obj;
				if (!sel_items.Contains(listViewItem))
				{
					listViewItem.Selected = false;
					flag = true;
				}
			}
			foreach (object obj2 in sel_items)
			{
				ListViewItem listViewItem2 = (ListViewItem)obj2;
				if (!listViewItem2.Selected)
				{
					listViewItem2.Selected = true;
					flag = true;
				}
			}
			return flag;
		}

		// Token: 0x060009BC RID: 2492 RVA: 0x00029DB4 File Offset: 0x00027FB4
		private void UpdateMultiSelection(int index, bool reselect)
		{
			bool flag = (XplatUI.State.ModifierKeys & Keys.Shift) > Keys.None;
			bool flag2 = (XplatUI.State.ModifierKeys & Keys.Control) > Keys.None;
			ListViewItem itemAtDisplayIndex = this.GetItemAtDisplayIndex(index);
			if (flag && this.selection_start != null)
			{
				ArrayList arrayList = new ArrayList();
				int displayIndex = this.selection_start.DisplayIndex;
				int num = Math.Min(displayIndex, index);
				int num2 = Math.Max(displayIndex, index);
				if (this.View == View.Details)
				{
					for (int i = num; i <= num2; i++)
					{
						arrayList.Add(this.GetItemAtDisplayIndex(i));
					}
				}
				else
				{
					ListView.ItemMatrixLocation itemMatrixLocation = this.items_matrix_location[num];
					ListView.ItemMatrixLocation itemMatrixLocation2 = this.items_matrix_location[num2];
					int num3 = Math.Min(itemMatrixLocation.Col, itemMatrixLocation2.Col);
					int num4 = Math.Max(itemMatrixLocation.Col, itemMatrixLocation2.Col);
					int num5 = Math.Min(itemMatrixLocation.Row, itemMatrixLocation2.Row);
					int num6 = Math.Max(itemMatrixLocation.Row, itemMatrixLocation2.Row);
					for (int j = 0; j < this.items.Count; j++)
					{
						ListView.ItemMatrixLocation itemMatrixLocation3 = this.items_matrix_location[j];
						if (itemMatrixLocation3.Row >= num5 && itemMatrixLocation3.Row <= num6 && itemMatrixLocation3.Col >= num3 && itemMatrixLocation3.Col <= num4)
						{
							arrayList.Add(this.GetItemAtDisplayIndex(j));
						}
					}
				}
				this.SelectItems(arrayList);
				return;
			}
			if (flag2)
			{
				itemAtDisplayIndex.Selected = !itemAtDisplayIndex.Selected;
				this.selection_start = itemAtDisplayIndex;
				return;
			}
			if (!reselect)
			{
				using (IEnumerator enumerator = this.SelectedIndices.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						int num7 = (int)obj;
						if (index != num7)
						{
							this.items[num7].Selected = false;
						}
					}
					goto IL_01E6;
				}
			}
			this.SelectedItems.Clear();
			itemAtDisplayIndex.Selected = true;
			IL_01E6:
			this.selection_start = itemAtDisplayIndex;
		}

		// Token: 0x060009BD RID: 2493 RVA: 0x00029FC0 File Offset: 0x000281C0
		internal override bool InternalPreProcessMessage(ref Message msg)
		{
			if (msg.Msg == 256)
			{
				Keys keys = (Keys)msg.WParam.ToInt32();
				this.HandleNavKeys(keys);
			}
			return base.InternalPreProcessMessage(ref msg);
		}

		// Token: 0x060009BE RID: 2494 RVA: 0x00029FF8 File Offset: 0x000281F8
		private bool HandleNavKeys(Keys key_data)
		{
			if (this.Items.Count == 0 || !this.item_control.Visible)
			{
				return false;
			}
			if (this.FocusedItem == null)
			{
				this.SetFocusedItem(0);
			}
			if (key_data != Keys.Return)
			{
				switch (key_data)
				{
				case Keys.Space:
					this.SelectIndex(this.focused_item_index);
					this.ToggleItemsCheckState();
					break;
				case Keys.PageUp:
				case Keys.PageDown:
				case Keys.Left:
				case Keys.Up:
				case Keys.Right:
				case Keys.Down:
					this.SelectIndex(this.GetAdjustedIndex(key_data));
					break;
				case Keys.End:
					this.SelectIndex(this.Items.Count - 1);
					break;
				case Keys.Home:
					this.SelectIndex(0);
					break;
				default:
					return false;
				}
			}
			else if (this.selected_indices.Count > 0)
			{
				this.OnItemActivate(EventArgs.Empty);
			}
			return true;
		}

		// Token: 0x060009BF RID: 2495 RVA: 0x0002A0C4 File Offset: 0x000282C4
		private void ToggleItemsCheckState()
		{
			if (!this.CheckBoxes)
			{
				return;
			}
			if (this.StateImageList != null && this.StateImageList.Images.Count < 2)
			{
				return;
			}
			if (this.SelectedIndices.Count > 0)
			{
				for (int i = 0; i < this.SelectedIndices.Count; i++)
				{
					ListViewItem listViewItem = this.Items[this.SelectedIndices[i]];
					listViewItem.Checked = !listViewItem.Checked;
				}
				return;
			}
			if (this.FocusedItem != null)
			{
				this.FocusedItem.Checked = !this.FocusedItem.Checked;
				this.SelectIndex(this.FocusedItem.Index);
			}
		}

		// Token: 0x060009C0 RID: 2496 RVA: 0x0002A174 File Offset: 0x00028374
		private void SelectIndex(int display_index)
		{
			if (display_index == -1)
			{
				return;
			}
			if (this.MultiSelect)
			{
				this.UpdateMultiSelection(display_index, true);
			}
			else if (!this.GetItemAtDisplayIndex(display_index).Selected)
			{
				this.GetItemAtDisplayIndex(display_index).Selected = true;
			}
			this.SetFocusedItem(display_index);
			this.EnsureVisible(this.GetItemIndex(display_index));
		}

		// Token: 0x060009C1 RID: 2497 RVA: 0x0002A1C8 File Offset: 0x000283C8
		private void ListView_KeyDown(object sender, KeyEventArgs ke)
		{
			if (ke.Handled || this.Items.Count == 0 || !this.item_control.Visible)
			{
				return;
			}
			if (ke.Alt || ke.Control)
			{
				return;
			}
			ke.Handled = this.KeySearchString(ke);
		}

		// Token: 0x060009C2 RID: 2498 RVA: 0x0002A218 File Offset: 0x00028418
		private MouseEventArgs TranslateMouseEventArgs(MouseEventArgs args)
		{
			Point point = base.PointToClient(Control.MousePosition);
			return new MouseEventArgs(args.Button, args.Clicks, point.X, point.Y, args.Delta);
		}

		// Token: 0x060009C3 RID: 2499 RVA: 0x0002A256 File Offset: 0x00028456
		internal override void OnPaintInternal(PaintEventArgs pe)
		{
			if (this.updating)
			{
				return;
			}
			this.CalculateScrollBars();
		}

		// Token: 0x060009C4 RID: 2500 RVA: 0x0002A268 File Offset: 0x00028468
		private void FocusChanged(object o, EventArgs args)
		{
			if (this.Items.Count == 0)
			{
				return;
			}
			if (this.FocusedItem == null)
			{
				this.SetFocusedItem(0);
			}
			ListViewItem focusedItem = this.FocusedItem;
			if (focusedItem.ListView != null)
			{
				focusedItem.Invalidate();
				focusedItem.Layout();
				focusedItem.Invalidate();
			}
		}

		// Token: 0x060009C5 RID: 2501 RVA: 0x0002A2B3 File Offset: 0x000284B3
		private void ListView_Invalidated(object sender, InvalidateEventArgs e)
		{
			this.header_control.Invalidate();
			this.item_control.Invalidate();
		}

		// Token: 0x060009C6 RID: 2502 RVA: 0x0002A2CB File Offset: 0x000284CB
		private void ListView_MouseEnter(object sender, EventArgs args)
		{
			this.hover_pending = true;
		}

		// Token: 0x060009C7 RID: 2503 RVA: 0x0002A2D4 File Offset: 0x000284D4
		private void ListView_MouseWheel(object sender, MouseEventArgs me)
		{
			if (this.Items.Count == 0)
			{
				return;
			}
			int num = me.Delta / 120;
			if (num == 0)
			{
				return;
			}
			switch (this.View)
			{
			case View.LargeIcon:
				break;
			case View.Details:
			case View.SmallIcon:
				this.Scroll(this.v_scroll, -this.ItemSize.Height * SystemInformation.MouseWheelScrollLines * num);
				return;
			case View.List:
				this.Scroll(this.h_scroll, -this.ItemSize.Width * num);
				return;
			case View.Tile:
				if (Application.VisualStylesEnabled)
				{
					this.Scroll(this.v_scroll, -(this.ItemSize.Height + ThemeEngine.Current.ListViewVerticalSpacing) * 2 * num);
					return;
				}
				break;
			default:
				return;
			}
			this.Scroll(this.v_scroll, -(this.ItemSize.Height + ThemeEngine.Current.ListViewVerticalSpacing) * num);
		}

		// Token: 0x060009C8 RID: 2504 RVA: 0x0002A3BB File Offset: 0x000285BB
		private void ListView_SizeChanged(object sender, EventArgs e)
		{
			this.Redraw(true);
		}

		// Token: 0x060009C9 RID: 2505 RVA: 0x0002A3C4 File Offset: 0x000285C4
		private void SetFocusedItem(int display_index)
		{
			if (display_index != -1)
			{
				this.GetItemAtDisplayIndex(display_index).Focused = true;
			}
			else if (this.focused_item_index != -1 && this.focused_item_index < this.items.Count)
			{
				this.GetItemAtDisplayIndex(this.focused_item_index).Focused = false;
			}
			this.focused_item_index = display_index;
			if (display_index == -1)
			{
				this.OnUIAFocusedItemChanged();
			}
		}

		// Token: 0x060009CA RID: 2506 RVA: 0x0002A424 File Offset: 0x00028624
		private void HorizontalScroller(object sender, EventArgs e)
		{
			this.item_control.EndEdit(this.item_control.edit_item);
			if (this.h_marker != this.h_scroll.Value)
			{
				int num = this.h_marker - this.h_scroll.Value;
				this.h_marker = this.h_scroll.Value;
				if (this.header_control.Visible)
				{
					XplatUI.ScrollWindow(this.header_control.Handle, num, 0, false);
				}
				XplatUI.ScrollWindow(this.item_control.Handle, num, 0, false);
			}
		}

		// Token: 0x060009CB RID: 2507 RVA: 0x0002A4B4 File Offset: 0x000286B4
		private void VerticalScroller(object sender, EventArgs e)
		{
			this.item_control.EndEdit(this.item_control.edit_item);
			if (this.v_marker != this.v_scroll.Value)
			{
				int num = this.v_marker - this.v_scroll.Value;
				Rectangle clientRectangle = this.item_control.ClientRectangle;
				if (this.header_control.Visible)
				{
					clientRectangle.Y += this.header_control.Height;
					clientRectangle.Height -= this.header_control.Height;
				}
				this.v_marker = this.v_scroll.Value;
				XplatUI.ScrollWindow(this.item_control.Handle, clientRectangle, 0, num, false);
			}
		}

		// Token: 0x060009CC RID: 2508 RVA: 0x00006F54 File Offset: 0x00005154
		internal override bool IsInputCharInternal(char charCode)
		{
			return true;
		}

		/// <summary>Creates a handle for the control.</summary>
		// Token: 0x060009CD RID: 2509 RVA: 0x0002A570 File Offset: 0x00028770
		protected override void CreateHandle()
		{
			base.CreateHandle();
			this.is_selection_available = true;
			for (int i = 0; i < this.SelectedItems.Count; i++)
			{
				this.OnSelectedIndexChanged(EventArgs.Empty);
			}
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.ListView" /> and optionally releases the managed resources. </summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
		// Token: 0x060009CE RID: 2510 RVA: 0x0002A5AC File Offset: 0x000287AC
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.large_image_list = null;
				this.small_image_list = null;
				this.state_image_list = null;
				foreach (object obj in this.columns)
				{
					((ColumnHeader)obj).SetListView(null);
				}
				if (!this.virtual_mode)
				{
					foreach (object obj2 in this.items)
					{
						((ListViewItem)obj2).Owner = null;
					}
				}
			}
			base.Dispose(disposing);
		}

		/// <summary>Determines whether the specified key is a regular input key or a special key that requires preprocessing.</summary>
		/// <returns>true if the specified key is a regular input key; otherwise, false.</returns>
		/// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values.</param>
		// Token: 0x060009CF RID: 2511 RVA: 0x0002A674 File Offset: 0x00028874
		protected override bool IsInputKey(Keys keyData)
		{
			return keyData - Keys.PageUp <= 7 || base.IsInputKey(keyData);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ListView.AfterLabelEdit" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.LabelEditEventArgs" /> that contains the event data. </param>
		// Token: 0x060009D0 RID: 2512 RVA: 0x0002A688 File Offset: 0x00028888
		protected virtual void OnAfterLabelEdit(LabelEditEventArgs e)
		{
			LabelEditEventHandler labelEditEventHandler = (LabelEditEventHandler)base.Events[ListView.AfterLabelEditEvent];
			if (labelEditEventHandler != null)
			{
				labelEditEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ListView.BeforeLabelEdit" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.LabelEditEventArgs" /> that contains the event data. </param>
		// Token: 0x060009D1 RID: 2513 RVA: 0x0002A6B8 File Offset: 0x000288B8
		protected virtual void OnBeforeLabelEdit(LabelEditEventArgs e)
		{
			LabelEditEventHandler labelEditEventHandler = (LabelEditEventHandler)base.Events[ListView.BeforeLabelEditEvent];
			if (labelEditEventHandler != null)
			{
				labelEditEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ListView.ColumnClick" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ColumnClickEventArgs" /> that contains the event data. </param>
		// Token: 0x060009D2 RID: 2514 RVA: 0x0002A6E8 File Offset: 0x000288E8
		protected internal virtual void OnColumnClick(ColumnClickEventArgs e)
		{
			ColumnClickEventHandler columnClickEventHandler = (ColumnClickEventHandler)base.Events[ListView.ColumnClickEvent];
			if (columnClickEventHandler != null)
			{
				columnClickEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ListView.DrawColumnHeader" /> event. </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DrawListViewColumnHeaderEventArgs" /> that contains the event data. </param>
		// Token: 0x060009D3 RID: 2515 RVA: 0x0002A718 File Offset: 0x00028918
		protected internal virtual void OnDrawColumnHeader(DrawListViewColumnHeaderEventArgs e)
		{
			DrawListViewColumnHeaderEventHandler drawListViewColumnHeaderEventHandler = (DrawListViewColumnHeaderEventHandler)base.Events[ListView.DrawColumnHeaderEvent];
			if (drawListViewColumnHeaderEventHandler != null)
			{
				drawListViewColumnHeaderEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ListView.DrawItem" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DrawListViewItemEventArgs" /> that contains the event data. </param>
		// Token: 0x060009D4 RID: 2516 RVA: 0x0002A748 File Offset: 0x00028948
		protected internal virtual void OnDrawItem(DrawListViewItemEventArgs e)
		{
			DrawListViewItemEventHandler drawListViewItemEventHandler = (DrawListViewItemEventHandler)base.Events[ListView.DrawItemEvent];
			if (drawListViewItemEventHandler != null)
			{
				drawListViewItemEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ListView.DrawSubItem" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DrawListViewSubItemEventArgs" /> that contains the event data. </param>
		// Token: 0x060009D5 RID: 2517 RVA: 0x0002A778 File Offset: 0x00028978
		protected internal virtual void OnDrawSubItem(DrawListViewSubItemEventArgs e)
		{
			DrawListViewSubItemEventHandler drawListViewSubItemEventHandler = (DrawListViewSubItemEventHandler)base.Events[ListView.DrawSubItemEvent];
			if (drawListViewSubItemEventHandler != null)
			{
				drawListViewSubItemEventHandler(this, e);
			}
		}

		/// <summary>Raises the FontChanged event.</summary>
		/// <param name="e">The <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060009D6 RID: 2518 RVA: 0x0002A7A6 File Offset: 0x000289A6
		protected override void OnFontChanged(EventArgs e)
		{
			base.OnFontChanged(e);
			this.Redraw(true);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.HandleCreated" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060009D7 RID: 2519 RVA: 0x0002A7B6 File Offset: 0x000289B6
		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
			this.CalculateListView(this.alignment);
			if (!this.virtual_mode)
			{
				this.Sort();
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.HandleDestroyed" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060009D8 RID: 2520 RVA: 0x00006538 File Offset: 0x00004738
		protected override void OnHandleDestroyed(EventArgs e)
		{
			base.OnHandleDestroyed(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ListView.ItemActivate" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060009D9 RID: 2521 RVA: 0x0002A7DC File Offset: 0x000289DC
		protected virtual void OnItemActivate(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ListView.ItemActivateEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ListView.ItemCheck" /> event.</summary>
		/// <param name="ice">An <see cref="T:System.Windows.Forms.ItemCheckEventArgs" /> that contains the event data. </param>
		// Token: 0x060009DA RID: 2522 RVA: 0x0002A80C File Offset: 0x00028A0C
		protected internal virtual void OnItemCheck(ItemCheckEventArgs ice)
		{
			ItemCheckEventHandler itemCheckEventHandler = (ItemCheckEventHandler)base.Events[ListView.ItemCheckEvent];
			if (itemCheckEventHandler != null)
			{
				itemCheckEventHandler(this, ice);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ListView.ItemChecked" /> event.</summary>
		/// <param name="e">An <see cref="T:System.Windows.Forms.ItemCheckedEventArgs" /> that contains the event data.</param>
		// Token: 0x060009DB RID: 2523 RVA: 0x0002A83C File Offset: 0x00028A3C
		protected internal virtual void OnItemChecked(ItemCheckedEventArgs e)
		{
			ItemCheckedEventHandler itemCheckedEventHandler = (ItemCheckedEventHandler)base.Events[ListView.ItemCheckedEvent];
			if (itemCheckedEventHandler != null)
			{
				itemCheckedEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ListView.ItemDrag" /> event.</summary>
		/// <param name="e">An <see cref="T:System.Windows.Forms.ItemDragEventArgs" /> that contains the event data. </param>
		// Token: 0x060009DC RID: 2524 RVA: 0x0002A86C File Offset: 0x00028A6C
		protected virtual void OnItemDrag(ItemDragEventArgs e)
		{
			ItemDragEventHandler itemDragEventHandler = (ItemDragEventHandler)base.Events[ListView.ItemDragEvent];
			if (itemDragEventHandler != null)
			{
				itemDragEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ListView.ItemMouseHover" /> event. </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ListViewItemMouseHoverEventArgs" /> that contains the event data. </param>
		// Token: 0x060009DD RID: 2525 RVA: 0x0002A89C File Offset: 0x00028A9C
		protected virtual void OnItemMouseHover(ListViewItemMouseHoverEventArgs e)
		{
			ListViewItemMouseHoverEventHandler listViewItemMouseHoverEventHandler = (ListViewItemMouseHoverEventHandler)base.Events[ListView.ItemMouseHoverEvent];
			if (listViewItemMouseHoverEventHandler != null)
			{
				listViewItemMouseHoverEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ListView.ItemSelectionChanged" /> event. </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ListViewItemSelectionChangedEventArgs" /> that contains the event data. </param>
		// Token: 0x060009DE RID: 2526 RVA: 0x0002A8CC File Offset: 0x00028ACC
		protected internal virtual void OnItemSelectionChanged(ListViewItemSelectionChangedEventArgs e)
		{
			ListViewItemSelectionChangedEventHandler listViewItemSelectionChangedEventHandler = (ListViewItemSelectionChangedEventHandler)base.Events[ListView.ItemSelectionChangedEvent];
			if (listViewItemSelectionChangedEventHandler != null)
			{
				listViewItemSelectionChangedEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseHover" /> event. </summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060009DF RID: 2527 RVA: 0x0002A8FA File Offset: 0x00028AFA
		protected override void OnMouseHover(EventArgs e)
		{
			base.OnMouseHover(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.ParentChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060009E0 RID: 2528 RVA: 0x0000488F File Offset: 0x00002A8F
		protected override void OnParentChanged(EventArgs e)
		{
			base.OnParentChanged(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ListView.SelectedIndexChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060009E1 RID: 2529 RVA: 0x0002A904 File Offset: 0x00028B04
		protected virtual void OnSelectedIndexChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ListView.SelectedIndexChangedEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.SystemColorsChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060009E2 RID: 2530 RVA: 0x0002A932 File Offset: 0x00028B32
		protected override void OnSystemColorsChanged(EventArgs e)
		{
			base.OnSystemColorsChanged(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ListView.CacheVirtualItems" /> event. </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.CacheVirtualItemsEventArgs" /> that contains the event data. </param>
		// Token: 0x060009E3 RID: 2531 RVA: 0x0002A93C File Offset: 0x00028B3C
		protected internal virtual void OnCacheVirtualItems(CacheVirtualItemsEventArgs e)
		{
			CacheVirtualItemsEventHandler cacheVirtualItemsEventHandler = (CacheVirtualItemsEventHandler)base.Events[ListView.CacheVirtualItemsEvent];
			if (cacheVirtualItemsEventHandler != null)
			{
				cacheVirtualItemsEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ListView.RetrieveVirtualItem" /> event. </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.RetrieveVirtualItemEventArgs" /> that contains the event data. </param>
		// Token: 0x060009E4 RID: 2532 RVA: 0x0002A96C File Offset: 0x00028B6C
		protected virtual void OnRetrieveVirtualItem(RetrieveVirtualItemEventArgs e)
		{
			RetrieveVirtualItemEventHandler retrieveVirtualItemEventHandler = (RetrieveVirtualItemEventHandler)base.Events[ListView.RetrieveVirtualItemEvent];
			if (retrieveVirtualItemEventHandler != null)
			{
				retrieveVirtualItemEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ListView.SearchForVirtualItem" /> event. </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.SearchForVirtualItemEventArgs" /> that contains the event data. </param>
		// Token: 0x060009E5 RID: 2533 RVA: 0x0002A99C File Offset: 0x00028B9C
		protected virtual void OnSearchForVirtualItem(SearchForVirtualItemEventArgs e)
		{
			SearchForVirtualItemEventHandler searchForVirtualItemEventHandler = (SearchForVirtualItemEventHandler)base.Events[ListView.SearchForVirtualItemEvent];
			if (searchForVirtualItemEventHandler != null)
			{
				searchForVirtualItemEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ListView.VirtualItemsSelectionRangeChanged" /> event. </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ListViewVirtualItemsSelectionRangeChangedEventArgs" /> that contains the event data. </param>
		// Token: 0x060009E6 RID: 2534 RVA: 0x0002A9CC File Offset: 0x00028BCC
		protected virtual void OnVirtualItemsSelectionRangeChanged(ListViewVirtualItemsSelectionRangeChangedEventArgs e)
		{
			ListViewVirtualItemsSelectionRangeChangedEventHandler listViewVirtualItemsSelectionRangeChangedEventHandler = (ListViewVirtualItemsSelectionRangeChangedEventHandler)base.Events[ListView.VirtualItemsSelectionRangeChangedEvent];
			if (listViewVirtualItemsSelectionRangeChangedEventHandler != null)
			{
				listViewVirtualItemsSelectionRangeChangedEventHandler(this, e);
			}
		}

		/// <summary>Overrides <see cref="M:System.Windows.Forms.Control.WndProc(System.Windows.Forms.Message@)" />.</summary>
		/// <param name="m">The Windows <see cref="T:System.Windows.Forms.Message" /> to process.</param>
		// Token: 0x060009E7 RID: 2535 RVA: 0x0002A9FC File Offset: 0x00028BFC
		protected override void WndProc(ref Message m)
		{
			Msg msg = (Msg)m.Msg;
			if (msg != Msg.WM_SETFOCUS)
			{
				if (msg == Msg.WM_KILLFOCUS && Control.FromHandle(m.WParam) == this.item_control)
				{
					this.has_focus = false;
					this.refocusing = true;
					return;
				}
			}
			else if (this.refocusing)
			{
				this.has_focus = true;
				this.refocusing = false;
				return;
			}
			base.WndProc(ref m);
		}

		/// <summary>Prevents the control from drawing until the <see cref="M:System.Windows.Forms.ListView.EndUpdate" /> method is called.</summary>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060009E8 RID: 2536 RVA: 0x0002AA58 File Offset: 0x00028C58
		public void BeginUpdate()
		{
			this.updating = true;
		}

		/// <summary>Resumes drawing of the list view control after drawing is suspended by the <see cref="M:System.Windows.Forms.ListView.BeginUpdate" /> method.</summary>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060009E9 RID: 2537 RVA: 0x0002AA61 File Offset: 0x00028C61
		public void EndUpdate()
		{
			this.updating = false;
			this.Redraw(true);
		}

		/// <summary>Ensures that the specified item is visible within the control, scrolling the contents of the control if necessary.</summary>
		/// <param name="index">The zero-based index of the item to scroll into view. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060009EA RID: 2538 RVA: 0x0002AA74 File Offset: 0x00028C74
		public void EnsureVisible(int index)
		{
			if (index < 0 || index >= this.items.Count || !this.scrollable || this.updating)
			{
				return;
			}
			Rectangle clientRectangle = this.item_control.ClientRectangle;
			Rectangle rectangle = (this.virtual_mode ? new Rectangle(this.GetItemLocation(index), this.ItemSize) : this.items[index].Bounds);
			if (this.view == View.Details && this.header_style != ColumnHeaderStyle.None)
			{
				clientRectangle.Y += this.header_control.Height;
				clientRectangle.Height -= this.header_control.Height;
			}
			if (clientRectangle.Contains(rectangle))
			{
				return;
			}
			if (this.View != View.Details)
			{
				if (rectangle.Left < 0)
				{
					this.h_scroll.Value += rectangle.Left;
				}
				else if (this.RightToLeftLayout && rectangle.Right > clientRectangle.Right)
				{
					this.h_scroll.Value += rectangle.Right - clientRectangle.Right;
				}
			}
			if (rectangle.Top < clientRectangle.Y)
			{
				this.v_scroll.Value += rectangle.Top - clientRectangle.Y;
				return;
			}
			if (rectangle.Bottom > clientRectangle.Bottom)
			{
				this.v_scroll.Value += rectangle.Bottom - clientRectangle.Bottom;
			}
		}

		// Token: 0x060009EB RID: 2539 RVA: 0x0002ABF8 File Offset: 0x00028DF8
		internal ListViewItem FindItemWithText(string text, bool includeSubItemsInSearch, int startIndex, bool isPrefixSearch, bool roundtrip)
		{
			if (startIndex < 0 || startIndex >= this.items.Count)
			{
				throw new ArgumentOutOfRangeException("startIndex");
			}
			if (text == null)
			{
				throw new ArgumentNullException("text");
			}
			if (!this.virtual_mode)
			{
				int i = startIndex;
				ListViewItem listViewItem;
				for (;;)
				{
					listViewItem = this.items[i];
					if (isPrefixSearch)
					{
						if (CultureInfo.CurrentCulture.CompareInfo.IsPrefix(listViewItem.Text, text, CompareOptions.IgnoreCase))
						{
							break;
						}
					}
					else if (string.Compare(listViewItem.Text, text, true) == 0)
					{
						return listViewItem;
					}
					if (i + 1 >= this.items.Count)
					{
						if (!roundtrip)
						{
							goto IL_00CF;
						}
						i = 0;
					}
					else
					{
						i++;
					}
					if (i == startIndex)
					{
						goto IL_00CF;
					}
				}
				return listViewItem;
				IL_00CF:
				if (includeSubItemsInSearch)
				{
					for (i = startIndex; i < this.items.Count; i++)
					{
						ListViewItem listViewItem2 = this.items[i];
						foreach (object obj in listViewItem2.SubItems)
						{
							ListViewItem.ListViewSubItem listViewSubItem = (ListViewItem.ListViewSubItem)obj;
							if (isPrefixSearch)
							{
								if (CultureInfo.CurrentCulture.CompareInfo.IsPrefix(listViewSubItem.Text, text, CompareOptions.IgnoreCase))
								{
									return listViewItem2;
								}
							}
							else if (string.Compare(listViewSubItem.Text, text, true) == 0)
							{
								return listViewItem2;
							}
						}
					}
				}
				return null;
			}
			SearchForVirtualItemEventArgs searchForVirtualItemEventArgs = new SearchForVirtualItemEventArgs(true, isPrefixSearch, includeSubItemsInSearch, text, Point.Empty, SearchDirectionHint.Down, startIndex);
			this.OnSearchForVirtualItem(searchForVirtualItemEventArgs);
			int index = searchForVirtualItemEventArgs.Index;
			if (index >= 0 && index < this.virtual_list_size)
			{
				return this.items[index];
			}
			return null;
		}

		/// <summary>Retrieves the item at the specified location.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ListViewItem" /> that represents the item at the specified position. If there is no item at the specified location, the method returns null.</returns>
		/// <param name="x">The x-coordinate of the location to search for an item (expressed in client coordinates). </param>
		/// <param name="y">The y-coordinate of the location to search for an item (expressed in client coordinates). </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060009EC RID: 2540 RVA: 0x0002AD90 File Offset: 0x00028F90
		public ListViewItem GetItemAt(int x, int y)
		{
			Size itemSize = this.ItemSize;
			for (int i = 0; i < this.items.Count; i++)
			{
				if (this.items[i].Bounds.Contains(x, y))
				{
					return this.items[i];
				}
			}
			return null;
		}

		/// <summary>Forces a range of <see cref="T:System.Windows.Forms.ListViewItem" /> objects to be redrawn.</summary>
		/// <param name="startIndex">The index for the first item in the range to be redrawn.</param>
		/// <param name="endIndex">The index for the last item of the range to be redrawn.</param>
		/// <param name="invalidateOnly">true to invalidate the range of items; false to invalidate and repaint the items.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> or <paramref name="endIndex" /> is less than 0, greater than or equal to the number of items in the <see cref="T:System.Windows.Forms.ListView" /> or, if in virtual mode, greater than the value of <see cref="P:System.Windows.Forms.ListView.VirtualListSize" />.-or-The given <paramref name="startIndex" /> is greater than the <paramref name="endIndex." /></exception>
		// Token: 0x060009ED RID: 2541 RVA: 0x0002ADE8 File Offset: 0x00028FE8
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public void RedrawItems(int startIndex, int endIndex, bool invalidateOnly)
		{
			if (startIndex < 0 || startIndex >= this.items.Count)
			{
				throw new ArgumentOutOfRangeException("startIndex");
			}
			if (endIndex < 0 || endIndex >= this.items.Count)
			{
				throw new ArgumentOutOfRangeException("endIndex");
			}
			if (startIndex > endIndex)
			{
				throw new ArgumentException("startIndex");
			}
			if (this.updating)
			{
				return;
			}
			for (int i = startIndex; i <= endIndex; i++)
			{
				this.items[i].Invalidate();
			}
			if (!invalidateOnly)
			{
				base.Update();
			}
		}

		/// <summary>Sorts the items of the list view.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060009EE RID: 2542 RVA: 0x0002AE6D File Offset: 0x0002906D
		public void Sort()
		{
			if (this.virtual_mode)
			{
				throw new InvalidOperationException();
			}
			this.Sort(true);
		}

		// Token: 0x060009EF RID: 2543 RVA: 0x0002AE84 File Offset: 0x00029084
		private void Sort(bool redraw)
		{
			if (!base.IsHandleCreated || this.item_sorter == null)
			{
				return;
			}
			this.items.Sort(this.item_sorter);
			if (redraw)
			{
				this.Redraw(true);
			}
		}

		/// <summary>Returns a string representation of the <see cref="T:System.Windows.Forms.ListView" /> control.</summary>
		/// <returns>A string that states the control type, the count of items in the <see cref="T:System.Windows.Forms.ListView" /> control, and the type of the first item in the <see cref="T:System.Windows.Forms.ListView" />, if the count is not 0.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060009F0 RID: 2544 RVA: 0x0002AEB4 File Offset: 0x000290B4
		public override string ToString()
		{
			int count = this.Items.Count;
			if (count == 0)
			{
				return string.Format("System.Windows.Forms.ListView, Items.Count: 0", Array.Empty<object>());
			}
			return string.Format("System.Windows.Forms.ListView, Items.Count: {0}, Items[0]: {1}", count, this.Items[0].ToString());
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Resize" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060009F1 RID: 2545 RVA: 0x0001F0CC File Offset: 0x0001D2CC
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseLeave" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060009F2 RID: 2546 RVA: 0x00025597 File Offset: 0x00023797
		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ListView.ColumnWidthChanged" /> event. </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ColumnWidthChangedEventArgs" /> that contains the event data. </param>
		// Token: 0x060009F3 RID: 2547 RVA: 0x0002AF04 File Offset: 0x00029104
		protected virtual void OnColumnWidthChanged(ColumnWidthChangedEventArgs e)
		{
			ColumnWidthChangedEventHandler columnWidthChangedEventHandler = (ColumnWidthChangedEventHandler)base.Events[ListView.ColumnWidthChangedEvent];
			if (columnWidthChangedEventHandler != null)
			{
				columnWidthChangedEventHandler(this, e);
			}
		}

		// Token: 0x060009F4 RID: 2548 RVA: 0x0002AF34 File Offset: 0x00029134
		private void RaiseColumnWidthChanged(int resize_column)
		{
			ColumnWidthChangedEventArgs columnWidthChangedEventArgs = new ColumnWidthChangedEventArgs(resize_column);
			this.OnColumnWidthChanged(columnWidthChangedEventArgs);
		}

		// Token: 0x060009F5 RID: 2549 RVA: 0x0002AF50 File Offset: 0x00029150
		private bool CanProceedWithResize(ColumnHeader col, int width)
		{
			ColumnWidthChangingEventHandler columnWidthChangingEventHandler = (ColumnWidthChangingEventHandler)base.Events[ListView.ColumnWidthChangingEvent];
			if (columnWidthChangingEventHandler == null)
			{
				return true;
			}
			ColumnWidthChangingEventArgs columnWidthChangingEventArgs = new ColumnWidthChangingEventArgs(col.Index, width);
			columnWidthChangingEventHandler(this, columnWidthChangingEventArgs);
			return !columnWidthChangingEventArgs.Cancel;
		}

		// Token: 0x060009F6 RID: 2550 RVA: 0x0002AF98 File Offset: 0x00029198
		internal void RaiseColumnWidthChanged(ColumnHeader column)
		{
			int num = this.Columns.IndexOf(column);
			this.RaiseColumnWidthChanged(num);
		}

		// Token: 0x060009F7 RID: 2551 RVA: 0x0002AFBC File Offset: 0x000291BC
		private void OnUIAMultiSelectChanged()
		{
			EventHandler eventHandler = (EventHandler)base.Events[ListView.UIAMultiSelectChangedEvent];
			if (eventHandler != null)
			{
				eventHandler(this, EventArgs.Empty);
			}
		}

		// Token: 0x060009F8 RID: 2552 RVA: 0x0002AFF0 File Offset: 0x000291F0
		private void OnUIALabelEditChanged()
		{
			EventHandler eventHandler = (EventHandler)base.Events[ListView.UIALabelEditChangedEvent];
			if (eventHandler != null)
			{
				eventHandler(this, EventArgs.Empty);
			}
		}

		// Token: 0x060009F9 RID: 2553 RVA: 0x0002B024 File Offset: 0x00029224
		private void OnUIAViewChanged()
		{
			EventHandler eventHandler = (EventHandler)base.Events[ListView.UIAViewChangedEvent];
			if (eventHandler != null)
			{
				eventHandler(this, EventArgs.Empty);
			}
		}

		// Token: 0x060009FA RID: 2554 RVA: 0x0002B058 File Offset: 0x00029258
		internal void OnUIAFocusedItemChanged()
		{
			EventHandler eventHandler = (EventHandler)base.Events[ListView.UIAFocusedItemChangedEvent];
			if (eventHandler != null)
			{
				eventHandler(this, EventArgs.Empty);
			}
		}

		// Token: 0x060009FB RID: 2555 RVA: 0x0002B08C File Offset: 0x0002928C
		// Note: this type is marked as 'beforefieldinit'.
		static ListView()
		{
			ListView.ColumnClickEvent = new object();
			ListView.ItemActivateEvent = new object();
			ListView.ItemCheckEvent = new object();
			ListView.ItemDragEvent = new object();
			ListView.SelectedIndexChangedEvent = new object();
			ListView.DrawColumnHeaderEvent = new object();
			ListView.DrawItemEvent = new object();
			ListView.DrawSubItemEvent = new object();
			ListView.ItemCheckedEvent = new object();
			ListView.ItemMouseHoverEvent = new object();
			ListView.ItemSelectionChangedEvent = new object();
			ListView.CacheVirtualItemsEvent = new object();
			ListView.RetrieveVirtualItemEvent = new object();
			ListView.RightToLeftLayoutChangedEvent = new object();
			ListView.SearchForVirtualItemEvent = new object();
			ListView.VirtualItemsSelectionRangeChangedEvent = new object();
			ListView.ColumnReorderedEvent = new object();
			ListView.ColumnWidthChangedEvent = new object();
			ListView.ColumnWidthChangingEvent = new object();
			ListView.UIALabelEditChangedEvent = new object();
			ListView.UIAShowGroupsChangedEvent = new object();
			ListView.UIAMultiSelectChangedEvent = new object();
			ListView.UIAViewChangedEvent = new object();
			ListView.UIACheckBoxesChangedEvent = new object();
			ListView.UIAFocusedItemChangedEvent = new object();
		}

		// Token: 0x04000695 RID: 1685
		private ItemActivation activation;

		// Token: 0x04000696 RID: 1686
		private ListViewAlignment alignment = ListViewAlignment.Top;

		// Token: 0x04000697 RID: 1687
		private bool allow_column_reorder;

		// Token: 0x04000698 RID: 1688
		private bool auto_arrange = true;

		// Token: 0x04000699 RID: 1689
		private bool check_boxes;

		// Token: 0x0400069A RID: 1690
		private readonly ListView.CheckedIndexCollection checked_indices;

		// Token: 0x0400069B RID: 1691
		private readonly ListView.CheckedListViewItemCollection checked_items;

		// Token: 0x0400069C RID: 1692
		private readonly ListView.ColumnHeaderCollection columns;

		// Token: 0x0400069D RID: 1693
		internal int focused_item_index = -1;

		// Token: 0x0400069E RID: 1694
		private bool full_row_select;

		// Token: 0x0400069F RID: 1695
		private bool grid_lines;

		// Token: 0x040006A0 RID: 1696
		private ColumnHeaderStyle header_style = ColumnHeaderStyle.Clickable;

		// Token: 0x040006A1 RID: 1697
		private bool hide_selection = true;

		// Token: 0x040006A2 RID: 1698
		private bool hover_selection;

		// Token: 0x040006A3 RID: 1699
		private IComparer item_sorter;

		// Token: 0x040006A4 RID: 1700
		private readonly ListView.ListViewItemCollection items;

		// Token: 0x040006A5 RID: 1701
		private readonly ListViewGroupCollection groups;

		// Token: 0x040006A6 RID: 1702
		private bool owner_draw;

		// Token: 0x040006A7 RID: 1703
		private bool show_groups = true;

		// Token: 0x040006A8 RID: 1704
		private bool label_edit;

		// Token: 0x040006A9 RID: 1705
		private bool label_wrap = true;

		// Token: 0x040006AA RID: 1706
		private bool multiselect = true;

		// Token: 0x040006AB RID: 1707
		private bool scrollable = true;

		// Token: 0x040006AC RID: 1708
		private bool hover_pending;

		// Token: 0x040006AD RID: 1709
		private readonly ListView.SelectedIndexCollection selected_indices;

		// Token: 0x040006AE RID: 1710
		private readonly ListView.SelectedListViewItemCollection selected_items;

		// Token: 0x040006AF RID: 1711
		private ImageList state_image_list;

		// Token: 0x040006B0 RID: 1712
		internal bool updating;

		// Token: 0x040006B1 RID: 1713
		private View view;

		// Token: 0x040006B2 RID: 1714
		private int layout_wd;

		// Token: 0x040006B3 RID: 1715
		private int layout_ht;

		// Token: 0x040006B4 RID: 1716
		internal ListView.HeaderControl header_control;

		// Token: 0x040006B5 RID: 1717
		internal ListView.ItemControl item_control;

		// Token: 0x040006B6 RID: 1718
		internal ScrollBar h_scroll;

		// Token: 0x040006B7 RID: 1719
		internal ScrollBar v_scroll;

		// Token: 0x040006B8 RID: 1720
		internal int h_marker;

		// Token: 0x040006B9 RID: 1721
		internal int v_marker;

		// Token: 0x040006BA RID: 1722
		private int keysearch_tickcnt;

		// Token: 0x040006BB RID: 1723
		private string keysearch_text;

		// Token: 0x040006BC RID: 1724
		private static readonly int keysearch_keydelay = 1000;

		// Token: 0x040006BD RID: 1725
		private int[] reordered_column_indices;

		// Token: 0x040006BE RID: 1726
		private int[] reordered_items_indices;

		// Token: 0x040006BF RID: 1727
		private Point[] items_location;

		// Token: 0x040006C0 RID: 1728
		private ListView.ItemMatrixLocation[] items_matrix_location;

		// Token: 0x040006C1 RID: 1729
		private Size item_size;

		// Token: 0x040006C2 RID: 1730
		private int custom_column_width;

		// Token: 0x040006C3 RID: 1731
		private int hot_item_index = -1;

		// Token: 0x040006C4 RID: 1732
		private bool hot_tracking;

		// Token: 0x040006C5 RID: 1733
		private ListViewInsertionMark insertion_mark;

		// Token: 0x040006C6 RID: 1734
		private bool show_item_tooltips;

		// Token: 0x040006C7 RID: 1735
		private ToolTip item_tooltip;

		// Token: 0x040006C8 RID: 1736
		private Size tile_size;

		// Token: 0x040006C9 RID: 1737
		private bool virtual_mode;

		// Token: 0x040006CA RID: 1738
		private int virtual_list_size;

		// Token: 0x040006CB RID: 1739
		private bool right_to_left_layout;

		// Token: 0x040006CC RID: 1740
		private bool is_selection_available;

		// Token: 0x040006CD RID: 1741
		internal ImageList large_image_list;

		// Token: 0x040006CE RID: 1742
		internal ImageList small_image_list;

		// Token: 0x040006CF RID: 1743
		internal Size text_size = Size.Empty;

		// Token: 0x040006D0 RID: 1744
		private static object AfterLabelEditEvent = new object();

		// Token: 0x040006D1 RID: 1745
		private static object BeforeLabelEditEvent = new object();

		// Token: 0x040006D3 RID: 1747
		private static object ItemActivateEvent;

		// Token: 0x040006D4 RID: 1748
		private static object ItemCheckEvent;

		// Token: 0x040006D5 RID: 1749
		private static object ItemDragEvent;

		// Token: 0x040006D6 RID: 1750
		private static object SelectedIndexChangedEvent;

		// Token: 0x040006D7 RID: 1751
		private static object DrawColumnHeaderEvent;

		// Token: 0x040006D8 RID: 1752
		private static object DrawItemEvent;

		// Token: 0x040006D9 RID: 1753
		private static object DrawSubItemEvent;

		// Token: 0x040006DA RID: 1754
		private static object ItemCheckedEvent;

		// Token: 0x040006DB RID: 1755
		private static object ItemMouseHoverEvent;

		// Token: 0x040006DC RID: 1756
		private static object ItemSelectionChangedEvent;

		// Token: 0x040006DD RID: 1757
		private static object CacheVirtualItemsEvent;

		// Token: 0x040006DE RID: 1758
		private static object RetrieveVirtualItemEvent;

		// Token: 0x040006DF RID: 1759
		private static object RightToLeftLayoutChangedEvent;

		// Token: 0x040006E0 RID: 1760
		private static object SearchForVirtualItemEvent;

		// Token: 0x040006E1 RID: 1761
		private static object VirtualItemsSelectionRangeChangedEvent;

		// Token: 0x040006E2 RID: 1762
		private int x_spacing;

		// Token: 0x040006E3 RID: 1763
		private int y_spacing;

		// Token: 0x040006E4 RID: 1764
		private int rows;

		// Token: 0x040006E5 RID: 1765
		private int cols;

		// Token: 0x040006E6 RID: 1766
		private int[,] item_index_matrix;

		// Token: 0x040006E7 RID: 1767
		private ListViewItem selection_start;

		// Token: 0x040006E8 RID: 1768
		private bool refocusing;

		// Token: 0x040006E9 RID: 1769
		private static object ColumnReorderedEvent;

		// Token: 0x040006EA RID: 1770
		private static object ColumnWidthChangedEvent;

		// Token: 0x040006EB RID: 1771
		private static object ColumnWidthChangingEvent;

		// Token: 0x040006EC RID: 1772
		private static object UIALabelEditChangedEvent;

		// Token: 0x040006ED RID: 1773
		private static object UIAShowGroupsChangedEvent;

		// Token: 0x040006EE RID: 1774
		private static object UIAMultiSelectChangedEvent;

		// Token: 0x040006EF RID: 1775
		private static object UIAViewChangedEvent;

		// Token: 0x040006F0 RID: 1776
		private static object UIACheckBoxesChangedEvent;

		// Token: 0x040006F1 RID: 1777
		private static object UIAFocusedItemChangedEvent;

		// Token: 0x02000108 RID: 264
		internal class ItemControl : Control
		{
			// Token: 0x060009FC RID: 2556 RVA: 0x0002B1B4 File Offset: 0x000293B4
			public ItemControl(ListView owner)
			{
				this.owner = owner;
				base.SetStyle(ControlStyles.DoubleBuffer, true);
				base.DoubleClick += this.ItemsDoubleClick;
				base.MouseDown += this.ItemsMouseDown;
				base.MouseMove += this.ItemsMouseMove;
				base.MouseHover += this.ItemsMouseHover;
				base.MouseUp += this.ItemsMouseUp;
			}

			// Token: 0x060009FD RID: 2557 RVA: 0x0002B248 File Offset: 0x00029448
			private void ItemsDoubleClick(object sender, EventArgs e)
			{
				if (this.owner.activation == ItemActivation.Standard)
				{
					this.owner.OnItemActivate(EventArgs.Empty);
				}
			}

			// Token: 0x17000286 RID: 646
			// (get) Token: 0x060009FE RID: 2558 RVA: 0x0002B267 File Offset: 0x00029467
			// (set) Token: 0x060009FF RID: 2559 RVA: 0x0002B26F File Offset: 0x0002946F
			internal Rectangle BoxSelectRectangle
			{
				get
				{
					return this.box_select_rect;
				}
				set
				{
					if (this.box_select_rect == value)
					{
						return;
					}
					this.InvalidateBoxSelectRect();
					this.box_select_rect = value;
					this.InvalidateBoxSelectRect();
				}
			}

			// Token: 0x06000A00 RID: 2560 RVA: 0x0002B294 File Offset: 0x00029494
			private void InvalidateBoxSelectRect()
			{
				if (this.BoxSelectRectangle.Size.IsEmpty)
				{
					return;
				}
				Rectangle boxSelectRectangle = this.BoxSelectRectangle;
				boxSelectRectangle.X--;
				boxSelectRectangle.Y--;
				boxSelectRectangle.Width += 2;
				boxSelectRectangle.Height = 2;
				base.Invalidate(boxSelectRectangle);
				boxSelectRectangle.Y = this.BoxSelectRectangle.Bottom - 1;
				base.Invalidate(boxSelectRectangle);
				boxSelectRectangle.Y = this.BoxSelectRectangle.Y - 1;
				boxSelectRectangle.Width = 2;
				boxSelectRectangle.Height = this.BoxSelectRectangle.Height + 2;
				base.Invalidate(boxSelectRectangle);
				boxSelectRectangle.X = this.BoxSelectRectangle.Right - 1;
				base.Invalidate(boxSelectRectangle);
			}

			// Token: 0x06000A01 RID: 2561 RVA: 0x0002B378 File Offset: 0x00029578
			private Rectangle CalculateBoxSelectRectangle(Point pt)
			{
				int num = Math.Min(this.box_select_start.X, pt.X);
				int num2 = Math.Max(this.box_select_start.X, pt.X);
				int num3 = Math.Min(this.box_select_start.Y, pt.Y);
				int num4 = Math.Max(this.box_select_start.Y, pt.Y);
				return Rectangle.FromLTRB(num, num3, num2, num4);
			}

			// Token: 0x06000A02 RID: 2562 RVA: 0x0002B3EC File Offset: 0x000295EC
			private bool BoxIntersectsItem(int index)
			{
				Rectangle rectangle = new Rectangle(this.owner.GetItemLocation(index), this.owner.ItemSize);
				if (this.owner.View != View.Details)
				{
					rectangle.X += rectangle.Width / 4;
					rectangle.Y += rectangle.Height / 4;
					rectangle.Width /= 2;
					rectangle.Height /= 2;
				}
				return this.BoxSelectRectangle.IntersectsWith(rectangle);
			}

			// Token: 0x06000A03 RID: 2563 RVA: 0x0002B480 File Offset: 0x00029680
			private bool BoxIntersectsText(int index)
			{
				Rectangle textBounds = this.owner.GetItemAtDisplayIndex(index).TextBounds;
				return this.BoxSelectRectangle.IntersectsWith(textBounds);
			}

			// Token: 0x17000287 RID: 647
			// (get) Token: 0x06000A04 RID: 2564 RVA: 0x0002B4B0 File Offset: 0x000296B0
			private ArrayList BoxSelectedItems
			{
				get
				{
					ArrayList arrayList = new ArrayList();
					for (int i = 0; i < this.owner.Items.Count; i++)
					{
						bool flag;
						if (this.owner.View == View.Details && !this.owner.FullRowSelect && !this.owner.VirtualMode)
						{
							flag = this.BoxIntersectsText(i);
						}
						else
						{
							flag = this.BoxIntersectsItem(i);
						}
						if (flag)
						{
							arrayList.Add(this.owner.GetItemAtDisplayIndex(i));
						}
					}
					return arrayList;
				}
			}

			// Token: 0x06000A05 RID: 2565 RVA: 0x0002B530 File Offset: 0x00029730
			private bool PerformBoxSelection(Point pt)
			{
				if (this.box_select_mode == ListView.ItemControl.BoxSelect.None)
				{
					return false;
				}
				this.BoxSelectRectangle = this.CalculateBoxSelectRectangle(pt);
				ArrayList boxSelectedItems = this.BoxSelectedItems;
				ArrayList arrayList;
				switch (this.box_select_mode)
				{
				case ListView.ItemControl.BoxSelect.Normal:
					arrayList = boxSelectedItems;
					goto IL_01CA;
				case ListView.ItemControl.BoxSelect.Shift:
					break;
				case ListView.ItemControl.BoxSelect.Control:
				{
					arrayList = new ArrayList();
					foreach (object obj in this.prev_selection)
					{
						int num = (int)obj;
						if (!boxSelectedItems.Contains(this.owner.Items[num]))
						{
							arrayList.Add(this.owner.Items[num]);
						}
					}
					using (IEnumerator enumerator = boxSelectedItems.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							object obj2 = enumerator.Current;
							ListViewItem listViewItem = (ListViewItem)obj2;
							if (!this.prev_selection.Contains(listViewItem.Index))
							{
								arrayList.Add(listViewItem);
							}
						}
						goto IL_01CA;
					}
					break;
				}
				default:
					goto IL_01AF;
				}
				arrayList = boxSelectedItems;
				foreach (object obj3 in boxSelectedItems)
				{
					ListViewItem listViewItem2 = (ListViewItem)obj3;
					this.prev_selection.Remove(listViewItem2.Index);
				}
				using (IEnumerator enumerator = this.prev_selection.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj4 = enumerator.Current;
						int num2 = (int)obj4;
						arrayList.Add(this.owner.Items[num2]);
					}
					goto IL_01CA;
				}
				IL_01AF:
				throw new Exception("Unexpected Selection mode: " + this.box_select_mode);
				IL_01CA:
				base.SuspendLayout();
				this.owner.SelectItems(arrayList);
				base.ResumeLayout();
				return true;
			}

			// Token: 0x06000A06 RID: 2566 RVA: 0x0002B758 File Offset: 0x00029958
			private void ItemsMouseDown(object sender, MouseEventArgs me)
			{
				this.owner.OnMouseDown(this.owner.TranslateMouseEventArgs(me));
				if (this.owner.items.Count == 0)
				{
					return;
				}
				bool flag = false;
				Size itemSize = this.owner.ItemSize;
				Point point = new Point(me.X, me.Y);
				int i = 0;
				while (i < this.owner.items.Count)
				{
					Rectangle rectangle = new Rectangle(this.owner.GetItemLocation(i), itemSize);
					if (rectangle.Contains(point))
					{
						ListViewItem itemAtDisplayIndex = this.owner.GetItemAtDisplayIndex(i);
						if (itemAtDisplayIndex.CheckRectReal.Contains(point))
						{
							if (this.owner.StateImageList != null && this.owner.StateImageList.Images.Count < 2)
							{
								return;
							}
							if (me.Clicks == 2)
							{
								itemAtDisplayIndex.Checked = !itemAtDisplayIndex.Checked;
							}
							itemAtDisplayIndex.Checked = !itemAtDisplayIndex.Checked;
							this.checking = true;
							return;
						}
						else
						{
							if (this.owner.View != View.Details)
							{
								this.clicked_item = itemAtDisplayIndex;
								break;
							}
							bool flag2 = itemAtDisplayIndex.TextBounds.Contains(point);
							if (this.owner.FullRowSelect)
							{
								this.clicked_item = itemAtDisplayIndex;
								bool flag3 = me.X > this.owner.Columns[0].X && me.X < this.owner.Columns[0].X + this.owner.Columns[0].Width;
								if (!flag2 && flag3 && this.owner.MultiSelect)
								{
									flag = true;
									break;
								}
								break;
							}
							else
							{
								if (flag2)
								{
									this.clicked_item = itemAtDisplayIndex;
									break;
								}
								this.owner.SetFocusedItem(i);
								break;
							}
						}
					}
					else
					{
						i++;
					}
				}
				if (this.clicked_item != null)
				{
					bool flag4 = !this.clicked_item.Selected;
					if (me.Button == MouseButtons.Left || (XplatUI.State.ModifierKeys == Keys.None && flag4))
					{
						this.owner.SetFocusedItem(this.clicked_item.DisplayIndex);
					}
					if (this.owner.MultiSelect)
					{
						bool flag5 = !this.owner.LabelEdit || flag4;
						if (me.Button == MouseButtons.Left || (XplatUI.State.ModifierKeys == Keys.None && flag4))
						{
							this.owner.UpdateMultiSelection(this.clicked_item.DisplayIndex, flag5);
						}
					}
					else
					{
						this.clicked_item.Selected = true;
					}
					if (this.clicked_item == null)
					{
						return;
					}
					if (this.owner.VirtualMode && flag4)
					{
						ListViewVirtualItemsSelectionRangeChangedEventArgs listViewVirtualItemsSelectionRangeChangedEventArgs = new ListViewVirtualItemsSelectionRangeChangedEventArgs(0, this.owner.items.Count - 1, false);
						this.owner.OnVirtualItemsSelectionRangeChanged(listViewVirtualItemsSelectionRangeChangedEventArgs);
					}
					this.clicks = me.Clicks;
					if (me.Clicks > 1)
					{
						if (this.owner.CheckBoxes)
						{
							this.clicked_item.Checked = !this.clicked_item.Checked;
						}
					}
					else if (me.Clicks == 1 && this.owner.LabelEdit && !flag4)
					{
						this.BeginEdit(this.clicked_item);
					}
					this.drag_begin = me.Location;
					this.dragged_item_index = this.clicked_item.Index;
				}
				else if (this.owner.MultiSelect)
				{
					flag = true;
				}
				else if (this.owner.SelectedItems.Count > 0)
				{
					this.owner.SelectedItems.Clear();
				}
				if (flag)
				{
					Keys modifierKeys = XplatUI.State.ModifierKeys;
					if ((modifierKeys & Keys.Shift) != Keys.None)
					{
						this.box_select_mode = ListView.ItemControl.BoxSelect.Shift;
					}
					else if ((modifierKeys & Keys.Control) != Keys.None)
					{
						this.box_select_mode = ListView.ItemControl.BoxSelect.Control;
					}
					else
					{
						this.box_select_mode = ListView.ItemControl.BoxSelect.Normal;
					}
					this.box_select_start = point;
					this.prev_selection = this.owner.SelectedIndices.List.Clone() as IList;
				}
			}

			// Token: 0x06000A07 RID: 2567 RVA: 0x0002BB40 File Offset: 0x00029D40
			private void ItemsMouseMove(object sender, MouseEventArgs me)
			{
				bool flag = this.PerformBoxSelection(new Point(me.X, me.Y));
				this.owner.OnMouseMove(this.owner.TranslateMouseEventArgs(me));
				if (flag)
				{
					return;
				}
				if (me.Button != MouseButtons.Left && me.Button != MouseButtons.Right && !this.hover_processed && this.owner.Activation != ItemActivation.OneClick && !this.owner.ShowItemToolTips)
				{
					return;
				}
				Point point = base.PointToClient(Control.MousePosition);
				ListViewItem itemAt = this.owner.GetItemAt(point.X, point.Y);
				if (this.hover_processed && itemAt != null && itemAt != this.prev_hovered_item)
				{
					this.hover_processed = false;
					XplatUI.ResetMouseHover(base.Handle);
				}
				if (this.owner.Activation == ItemActivation.OneClick)
				{
					if (itemAt == null && this.owner.HotItemIndex != -1)
					{
						if (this.owner.HotTracking)
						{
							base.Invalidate(this.owner.Items[this.owner.HotItemIndex].Bounds);
						}
						this.Cursor = Cursors.Default;
						this.owner.HotItemIndex = -1;
					}
					else if (itemAt != null && this.owner.HotItemIndex == -1)
					{
						if (this.owner.HotTracking)
						{
							base.Invalidate(itemAt.Bounds);
						}
						this.Cursor = Cursors.Hand;
						this.owner.HotItemIndex = itemAt.Index;
					}
				}
				if ((me.Button == MouseButtons.Left || me.Button == MouseButtons.Right) && this.drag_begin != new Point(-1, -1))
				{
					Rectangle rectangle = new Rectangle(this.drag_begin, SystemInformation.DragSize);
					if (!rectangle.Contains(me.X, me.Y))
					{
						ListViewItem listViewItem = this.owner.items[this.dragged_item_index];
						this.owner.OnItemDrag(new ItemDragEventArgs(me.Button, listViewItem));
						this.drag_begin = new Point(-1, -1);
						this.dragged_item_index = -1;
					}
				}
				if (this.owner.ShowItemToolTips)
				{
					if (itemAt == null)
					{
						this.owner.item_tooltip.Active = false;
						this.prev_tooltip_item = null;
						return;
					}
					if (itemAt != this.prev_tooltip_item && itemAt.ToolTipText.Length > 0)
					{
						this.owner.item_tooltip.Active = true;
						this.owner.item_tooltip.SetToolTip(this.owner, itemAt.ToolTipText);
						this.prev_tooltip_item = itemAt;
					}
				}
			}

			// Token: 0x06000A08 RID: 2568 RVA: 0x0002BDC8 File Offset: 0x00029FC8
			private void ItemsMouseHover(object sender, EventArgs e)
			{
				if (this.owner.hover_pending)
				{
					this.owner.OnMouseHover(e);
					this.owner.hover_pending = false;
				}
				if (base.Capture)
				{
					return;
				}
				this.hover_processed = true;
				Point point = base.PointToClient(Control.MousePosition);
				ListViewItem itemAt = this.owner.GetItemAt(point.X, point.Y);
				if (itemAt == null)
				{
					return;
				}
				this.prev_hovered_item = itemAt;
				if (this.owner.HoverSelection)
				{
					if (this.owner.MultiSelect)
					{
						this.owner.UpdateMultiSelection(itemAt.Index, true);
					}
					else
					{
						itemAt.Selected = true;
					}
					this.owner.SetFocusedItem(itemAt.DisplayIndex);
					base.Select();
				}
				this.owner.OnItemMouseHover(new ListViewItemMouseHoverEventArgs(itemAt));
			}

			// Token: 0x06000A09 RID: 2569 RVA: 0x0002BE98 File Offset: 0x0002A098
			private void HandleClicks(MouseEventArgs me)
			{
				if (this.clicks > 1)
				{
					this.owner.OnDoubleClick(EventArgs.Empty);
					this.owner.OnMouseDoubleClick(me);
				}
				else if (this.clicks == 1)
				{
					this.owner.OnClick(EventArgs.Empty);
					this.owner.OnMouseClick(me);
				}
				this.clicks = 0;
			}

			// Token: 0x06000A0A RID: 2570 RVA: 0x0002BEF8 File Offset: 0x0002A0F8
			private void ItemsMouseUp(object sender, MouseEventArgs me)
			{
				MouseEventArgs mouseEventArgs = this.owner.TranslateMouseEventArgs(me);
				this.HandleClicks(mouseEventArgs);
				base.Capture = false;
				if (this.owner.Items.Count == 0)
				{
					this.ResetMouseState();
					this.owner.OnMouseUp(mouseEventArgs);
					return;
				}
				Point point = new Point(me.X, me.Y);
				Rectangle rectangle = Rectangle.Empty;
				if (this.clicked_item != null)
				{
					if (this.owner.view == View.Details && !this.owner.full_row_select)
					{
						rectangle = this.clicked_item.GetBounds(ItemBoundsPortion.Label);
					}
					else
					{
						rectangle = this.clicked_item.Bounds;
					}
					if (rectangle.Contains(point))
					{
						ItemActivation activation = this.owner.activation;
						if (activation != ItemActivation.OneClick)
						{
							if (activation == ItemActivation.TwoClick)
							{
								if (this.last_clicked_item == this.clicked_item)
								{
									this.owner.OnItemActivate(EventArgs.Empty);
									this.last_clicked_item = null;
								}
								else
								{
									this.last_clicked_item = this.clicked_item;
								}
							}
						}
						else
						{
							this.owner.OnItemActivate(EventArgs.Empty);
						}
					}
				}
				else if (!this.checking && this.owner.SelectedItems.Count > 0 && this.BoxSelectRectangle.Size.IsEmpty)
				{
					this.owner.SelectedItems.Clear();
				}
				this.ResetMouseState();
				this.owner.OnMouseUp(mouseEventArgs);
			}

			// Token: 0x06000A0B RID: 2571 RVA: 0x0002C064 File Offset: 0x0002A264
			private void ResetMouseState()
			{
				this.clicked_item = null;
				this.box_select_start = Point.Empty;
				this.BoxSelectRectangle = Rectangle.Empty;
				this.prev_selection = null;
				this.box_select_mode = ListView.ItemControl.BoxSelect.None;
				this.checking = false;
				this.dragged_item_index = -1;
				this.drag_begin = new Point(-1, -1);
			}

			// Token: 0x06000A0C RID: 2572 RVA: 0x0002C0B7 File Offset: 0x0002A2B7
			private void LabelEditFinished(object sender, EventArgs e)
			{
				this.EndEdit(this.edit_item);
			}

			// Token: 0x06000A0D RID: 2573 RVA: 0x0002C0C5 File Offset: 0x0002A2C5
			private void LabelEditCancelled(object sender, EventArgs e)
			{
				this.edit_args.SetLabel(null);
				this.EndEdit(this.edit_item);
			}

			// Token: 0x06000A0E RID: 2574 RVA: 0x0002C0DF File Offset: 0x0002A2DF
			private void LabelTextChanged(object sender, EventArgs e)
			{
				if (this.edit_args != null)
				{
					this.edit_args.SetLabel(this.edit_text_box.Text);
				}
			}

			// Token: 0x06000A0F RID: 2575 RVA: 0x0002C100 File Offset: 0x0002A300
			internal void BeginEdit(ListViewItem item)
			{
				if (this.edit_item != null)
				{
					this.EndEdit(this.edit_item);
				}
				if (this.edit_text_box == null)
				{
					this.edit_text_box = new ListView.ListViewLabelEditTextBox();
					this.edit_text_box.BorderStyle = BorderStyle.FixedSingle;
					this.edit_text_box.EditingCancelled += this.LabelEditCancelled;
					this.edit_text_box.EditingFinished += this.LabelEditFinished;
					this.edit_text_box.TextChanged += this.LabelTextChanged;
					this.edit_text_box.Visible = false;
					base.Controls.Add(this.edit_text_box);
				}
				item.EnsureVisible();
				this.edit_text_box.Reset();
				View view = this.owner.view;
				if (view != View.LargeIcon)
				{
					if (view - View.Details <= 2)
					{
						this.edit_text_box.TextAlign = HorizontalAlignment.Left;
						this.edit_text_box.Bounds = item.GetBounds(ItemBoundsPortion.Label);
						SizeF sizeF = TextRenderer.MeasureString(item.Text, item.Font);
						this.edit_text_box.Width = (int)sizeF.Width + 4;
						this.edit_text_box.MaxWidth = this.owner.ClientRectangle.Width - this.edit_text_box.Bounds.X;
						this.edit_text_box.WordWrap = false;
						this.edit_text_box.Multiline = false;
					}
				}
				else
				{
					this.edit_text_box.TextAlign = HorizontalAlignment.Center;
					this.edit_text_box.Bounds = item.GetBounds(ItemBoundsPortion.Label);
					SizeF sizeF = TextRenderer.MeasureString(item.Text, item.Font);
					this.edit_text_box.Width = (int)sizeF.Width + 4;
					this.edit_text_box.MaxWidth = item.GetBounds(ItemBoundsPortion.Entire).Width;
					this.edit_text_box.MaxHeight = this.owner.ClientRectangle.Height - this.edit_text_box.Bounds.Y;
					this.edit_text_box.WordWrap = true;
					this.edit_text_box.Multiline = true;
				}
				this.edit_item = item;
				this.edit_text_box.Text = item.Text;
				this.edit_text_box.Font = item.Font;
				this.edit_text_box.Visible = true;
				this.edit_text_box.Focus();
				this.edit_text_box.SelectAll();
				this.edit_args = new LabelEditEventArgs(this.owner.Items.IndexOf(this.edit_item));
				this.owner.OnBeforeLabelEdit(this.edit_args);
				if (this.edit_args.CancelEdit)
				{
					this.EndEdit(item);
				}
			}

			// Token: 0x06000A10 RID: 2576 RVA: 0x0002C39D File Offset: 0x0002A59D
			internal void CancelEdit(ListViewItem item)
			{
				if (this.edit_item == null || this.edit_item != item)
				{
					return;
				}
				this.edit_args.SetLabel(null);
				this.EndEdit(item);
			}

			// Token: 0x06000A11 RID: 2577 RVA: 0x0002C3C4 File Offset: 0x0002A5C4
			internal void EndEdit(ListViewItem item)
			{
				if (this.edit_item == null || this.edit_item != item)
				{
					return;
				}
				if (this.edit_text_box != null)
				{
					if (this.edit_text_box.Visible)
					{
						this.edit_text_box.Visible = false;
					}
					this.owner.Focus();
				}
				Application.DoEvents();
				LabelEditEventArgs labelEditEventArgs = new LabelEditEventArgs(item.Index, this.edit_args.Label);
				this.edit_item = null;
				this.owner.OnAfterLabelEdit(labelEditEventArgs);
				if (!labelEditEventArgs.CancelEdit && labelEditEventArgs.Label != null)
				{
					item.Text = labelEditEventArgs.Label;
				}
			}

			// Token: 0x06000A12 RID: 2578 RVA: 0x0002C45B File Offset: 0x0002A65B
			internal override void OnPaintInternal(PaintEventArgs pe)
			{
				ThemeEngine.Current.DrawListViewItems(pe.Graphics, pe.ClipRectangle, this.owner);
			}

			// Token: 0x06000A13 RID: 2579 RVA: 0x0002C47C File Offset: 0x0002A67C
			protected override void WndProc(ref Message m)
			{
				Msg msg = (Msg)m.Msg;
				if (msg <= Msg.WM_KILLFOCUS)
				{
					if (msg != Msg.WM_SETFOCUS)
					{
						if (msg == Msg.WM_KILLFOCUS)
						{
							this.owner.Select(false, true);
						}
					}
					else
					{
						this.owner.Select(false, true);
					}
				}
				else if (msg != Msg.WM_LBUTTONDOWN)
				{
					if (msg == Msg.WM_RBUTTONDOWN)
					{
						if (!this.Focused)
						{
							this.owner.Select(false, true);
						}
					}
				}
				else if (!this.Focused)
				{
					this.owner.Select(false, true);
				}
				base.WndProc(ref m);
			}

			// Token: 0x040006F2 RID: 1778
			private ListView owner;

			// Token: 0x040006F3 RID: 1779
			private ListViewItem clicked_item;

			// Token: 0x040006F4 RID: 1780
			private ListViewItem last_clicked_item;

			// Token: 0x040006F5 RID: 1781
			private bool hover_processed;

			// Token: 0x040006F6 RID: 1782
			private bool checking;

			// Token: 0x040006F7 RID: 1783
			private ListViewItem prev_hovered_item;

			// Token: 0x040006F8 RID: 1784
			private ListViewItem prev_tooltip_item;

			// Token: 0x040006F9 RID: 1785
			private int clicks;

			// Token: 0x040006FA RID: 1786
			private Point drag_begin = new Point(-1, -1);

			// Token: 0x040006FB RID: 1787
			internal int dragged_item_index = -1;

			// Token: 0x040006FC RID: 1788
			private ListView.ListViewLabelEditTextBox edit_text_box;

			// Token: 0x040006FD RID: 1789
			internal ListViewItem edit_item;

			// Token: 0x040006FE RID: 1790
			private LabelEditEventArgs edit_args;

			// Token: 0x040006FF RID: 1791
			private ListView.ItemControl.BoxSelect box_select_mode;

			// Token: 0x04000700 RID: 1792
			private IList prev_selection;

			// Token: 0x04000701 RID: 1793
			private Point box_select_start;

			// Token: 0x04000702 RID: 1794
			private Rectangle box_select_rect;

			// Token: 0x02000109 RID: 265
			private enum BoxSelect
			{
				// Token: 0x04000704 RID: 1796
				None,
				// Token: 0x04000705 RID: 1797
				Normal,
				// Token: 0x04000706 RID: 1798
				Shift,
				// Token: 0x04000707 RID: 1799
				Control
			}
		}

		// Token: 0x0200010A RID: 266
		internal class ListViewLabelEditTextBox : TextBox
		{
			// Token: 0x06000A14 RID: 2580 RVA: 0x0002C504 File Offset: 0x0002A704
			public ListViewLabelEditTextBox()
			{
				this.min_height = this.DefaultSize.Height;
				this.text_size_one_char = TextRenderer.MeasureString("B", this.Font);
			}

			// Token: 0x17000288 RID: 648
			// (set) Token: 0x06000A15 RID: 2581 RVA: 0x0002C564 File Offset: 0x0002A764
			public int MaxWidth
			{
				set
				{
					if (value < this.min_width)
					{
						this.max_width = this.min_width;
						return;
					}
					this.max_width = value;
				}
			}

			// Token: 0x17000289 RID: 649
			// (set) Token: 0x06000A16 RID: 2582 RVA: 0x0002C583 File Offset: 0x0002A783
			public int MaxHeight
			{
				set
				{
					if (value < this.min_height)
					{
						this.max_height = this.min_height;
						return;
					}
					this.max_height = value;
				}
			}

			// Token: 0x1700028A RID: 650
			// (get) Token: 0x06000A17 RID: 2583 RVA: 0x0002C5A2 File Offset: 0x0002A7A2
			// (set) Token: 0x06000A18 RID: 2584 RVA: 0x0002C5AA File Offset: 0x0002A7AA
			public new int Width
			{
				get
				{
					return base.Width;
				}
				set
				{
					this.min_width = value;
					base.Width = value;
				}
			}

			// Token: 0x1700028B RID: 651
			// (get) Token: 0x06000A19 RID: 2585 RVA: 0x0002C5BA File Offset: 0x0002A7BA
			// (set) Token: 0x06000A1A RID: 2586 RVA: 0x0002C5C2 File Offset: 0x0002A7C2
			public override Font Font
			{
				get
				{
					return base.Font;
				}
				set
				{
					base.Font = value;
					this.text_size_one_char = TextRenderer.MeasureString("B", this.Font);
				}
			}

			// Token: 0x06000A1B RID: 2587 RVA: 0x0002C5E4 File Offset: 0x0002A7E4
			protected override void OnTextChanged(EventArgs e)
			{
				int num = (int)TextRenderer.MeasureString(this.Text, this.Font).Width + 8;
				if (!this.Multiline)
				{
					this.ResizeTextBoxWidth(num);
				}
				else
				{
					if (this.Width != this.max_width)
					{
						this.ResizeTextBoxWidth(num);
					}
					int num2 = base.Lines.Length;
					if (num2 != this.old_number_lines)
					{
						int num3 = num2 * (int)this.text_size_one_char.Height + 4;
						this.old_number_lines = num2;
						this.ResizeTextBoxHeight(num3);
					}
				}
				base.OnTextChanged(e);
			}

			// Token: 0x06000A1C RID: 2588 RVA: 0x0002C66C File Offset: 0x0002A86C
			protected override bool IsInputKey(Keys key_data)
			{
				if ((key_data & Keys.Alt) == Keys.None)
				{
					Keys keys = key_data & Keys.KeyCode;
					if (keys == Keys.Return)
					{
						return true;
					}
					if (keys == Keys.Escape)
					{
						return true;
					}
				}
				return base.IsInputKey(key_data);
			}

			// Token: 0x06000A1D RID: 2589 RVA: 0x0002C6A4 File Offset: 0x0002A8A4
			protected override void OnKeyDown(KeyEventArgs e)
			{
				if (!base.Visible)
				{
					return;
				}
				Keys keyCode = e.KeyCode;
				if (keyCode == Keys.Return)
				{
					base.Visible = false;
					e.Handled = true;
					this.OnEditingFinished(e);
					return;
				}
				if (keyCode != Keys.Escape)
				{
					return;
				}
				base.Visible = false;
				e.Handled = true;
				this.OnEditingCancelled(e);
			}

			// Token: 0x06000A1E RID: 2590 RVA: 0x0002C6F7 File Offset: 0x0002A8F7
			protected override void OnLostFocus(EventArgs e)
			{
				if (base.Visible)
				{
					this.OnEditingFinished(e);
				}
			}

			// Token: 0x06000A1F RID: 2591 RVA: 0x0002C708 File Offset: 0x0002A908
			protected void OnEditingCancelled(EventArgs e)
			{
				EventHandler eventHandler = (EventHandler)base.Events[ListView.ListViewLabelEditTextBox.EditingCancelledEvent];
				if (eventHandler != null)
				{
					eventHandler(this, e);
				}
			}

			// Token: 0x06000A20 RID: 2592 RVA: 0x0002C738 File Offset: 0x0002A938
			protected void OnEditingFinished(EventArgs e)
			{
				EventHandler eventHandler = (EventHandler)base.Events[ListView.ListViewLabelEditTextBox.EditingFinishedEvent];
				if (eventHandler != null)
				{
					eventHandler(this, e);
				}
			}

			// Token: 0x06000A21 RID: 2593 RVA: 0x0002C766 File Offset: 0x0002A966
			private void ResizeTextBoxWidth(int new_width)
			{
				if (new_width > this.max_width)
				{
					base.Width = this.max_width;
					return;
				}
				if (new_width >= this.min_width)
				{
					base.Width = new_width;
					return;
				}
				base.Width = this.min_width;
			}

			// Token: 0x06000A22 RID: 2594 RVA: 0x0002C79B File Offset: 0x0002A99B
			private void ResizeTextBoxHeight(int new_height)
			{
				if (new_height > this.max_height)
				{
					base.Height = this.max_height;
					return;
				}
				if (new_height >= this.min_height)
				{
					base.Height = new_height;
					return;
				}
				base.Height = this.min_height;
			}

			// Token: 0x06000A23 RID: 2595 RVA: 0x0002C7D0 File Offset: 0x0002A9D0
			public void Reset()
			{
				this.max_width = -1;
				this.min_width = -1;
				this.max_height = -1;
				this.old_number_lines = 1;
				this.Text = string.Empty;
				base.Size = this.DefaultSize;
			}

			// Token: 0x14000035 RID: 53
			// (add) Token: 0x06000A24 RID: 2596 RVA: 0x0002C805 File Offset: 0x0002AA05
			// (remove) Token: 0x06000A25 RID: 2597 RVA: 0x0002C818 File Offset: 0x0002AA18
			public event EventHandler EditingCancelled
			{
				add
				{
					base.Events.AddHandler(ListView.ListViewLabelEditTextBox.EditingCancelledEvent, value);
				}
				remove
				{
					base.Events.RemoveHandler(ListView.ListViewLabelEditTextBox.EditingCancelledEvent, value);
				}
			}

			// Token: 0x14000036 RID: 54
			// (add) Token: 0x06000A26 RID: 2598 RVA: 0x0002C82B File Offset: 0x0002AA2B
			// (remove) Token: 0x06000A27 RID: 2599 RVA: 0x0002C83E File Offset: 0x0002AA3E
			public event EventHandler EditingFinished
			{
				add
				{
					base.Events.AddHandler(ListView.ListViewLabelEditTextBox.EditingFinishedEvent, value);
				}
				remove
				{
					base.Events.RemoveHandler(ListView.ListViewLabelEditTextBox.EditingFinishedEvent, value);
				}
			}

			// Token: 0x06000A28 RID: 2600 RVA: 0x0002C851 File Offset: 0x0002AA51
			// Note: this type is marked as 'beforefieldinit'.
			static ListViewLabelEditTextBox()
			{
				ListView.ListViewLabelEditTextBox.EditingCancelledEvent = new object();
				ListView.ListViewLabelEditTextBox.EditingFinishedEvent = new object();
			}

			// Token: 0x04000708 RID: 1800
			private int max_width = -1;

			// Token: 0x04000709 RID: 1801
			private int min_width = -1;

			// Token: 0x0400070A RID: 1802
			private int max_height = -1;

			// Token: 0x0400070B RID: 1803
			private int min_height = -1;

			// Token: 0x0400070C RID: 1804
			private int old_number_lines = 1;

			// Token: 0x0400070D RID: 1805
			private SizeF text_size_one_char;
		}

		// Token: 0x0200010B RID: 267
		internal class HeaderControl : Control
		{
			// Token: 0x06000A29 RID: 2601 RVA: 0x0002C868 File Offset: 0x0002AA68
			public HeaderControl(ListView owner)
			{
				this.owner = owner;
				base.SetStyle(ControlStyles.DoubleBuffer, true);
				base.MouseDown += this.HeaderMouseDown;
				base.MouseMove += this.HeaderMouseMove;
				base.MouseUp += this.HeaderMouseUp;
				base.MouseLeave += this.OnMouseLeave;
			}

			// Token: 0x1700028C RID: 652
			// (get) Token: 0x06000A2A RID: 2602 RVA: 0x0002C8DD File Offset: 0x0002AADD
			// (set) Token: 0x06000A2B RID: 2603 RVA: 0x0002C8E8 File Offset: 0x0002AAE8
			internal ColumnHeader EnteredColumnHeader
			{
				get
				{
					return this.entered_column_header;
				}
				private set
				{
					if (this.entered_column_header == value)
					{
						return;
					}
					if (ThemeEngine.Current.ListViewHasHotHeaderStyle)
					{
						Region region = new Region();
						region.MakeEmpty();
						if (this.entered_column_header != null)
						{
							region.Union(this.GetColumnHeaderInvalidateArea(this.entered_column_header));
						}
						this.entered_column_header = value;
						if (this.entered_column_header != null)
						{
							region.Union(this.GetColumnHeaderInvalidateArea(this.entered_column_header));
						}
						base.Invalidate(region);
						region.Dispose();
						return;
					}
					this.entered_column_header = value;
				}
			}

			// Token: 0x06000A2C RID: 2604 RVA: 0x0002C967 File Offset: 0x0002AB67
			private void OnMouseLeave(object sender, EventArgs e)
			{
				this.EnteredColumnHeader = null;
			}

			// Token: 0x06000A2D RID: 2605 RVA: 0x0002C970 File Offset: 0x0002AB70
			private ColumnHeader ColumnAtX(int x)
			{
				Point point = new Point(x, 0);
				ColumnHeader columnHeader = null;
				foreach (object obj in this.owner.Columns)
				{
					ColumnHeader columnHeader2 = (ColumnHeader)obj;
					if (columnHeader2.Rect.Contains(point))
					{
						columnHeader = columnHeader2;
						break;
					}
				}
				return columnHeader;
			}

			// Token: 0x06000A2E RID: 2606 RVA: 0x0002C9EC File Offset: 0x0002ABEC
			private int GetReorderedIndex(ColumnHeader col)
			{
				if (this.owner.reordered_column_indices == null)
				{
					return col.Index;
				}
				for (int i = 0; i < this.owner.Columns.Count; i++)
				{
					if (this.owner.reordered_column_indices[i] == col.Index)
					{
						return i;
					}
				}
				throw new Exception("Column index missing from reordered array");
			}

			// Token: 0x06000A2F RID: 2607 RVA: 0x0002CA4C File Offset: 0x0002AC4C
			private void HeaderMouseDown(object sender, MouseEventArgs me)
			{
				if (this.resize_column != null)
				{
					this.column_resize_active = true;
					base.Capture = true;
					return;
				}
				this.clicked_column = this.ColumnAtX(me.X + this.owner.h_marker);
				if (this.clicked_column != null)
				{
					base.Capture = true;
					if (this.owner.AllowColumnReorder)
					{
						this.drag_x = me.X;
						this.drag_column = (ColumnHeader)((ICloneable)this.clicked_column).Clone();
						this.drag_column.Rect = this.clicked_column.Rect;
						this.drag_to_index = this.GetReorderedIndex(this.clicked_column);
					}
					this.clicked_column.Pressed = true;
					this.Invalidate(this.clicked_column);
					return;
				}
			}

			// Token: 0x06000A30 RID: 2608 RVA: 0x0002CB0D File Offset: 0x0002AD0D
			private void Invalidate(ColumnHeader columnHeader)
			{
				base.Invalidate(this.GetColumnHeaderInvalidateArea(columnHeader));
			}

			// Token: 0x06000A31 RID: 2609 RVA: 0x0002CB1C File Offset: 0x0002AD1C
			private Rectangle GetColumnHeaderInvalidateArea(ColumnHeader columnHeader)
			{
				Rectangle rect = columnHeader.Rect;
				rect.X -= this.owner.h_marker;
				return rect;
			}

			// Token: 0x06000A32 RID: 2610 RVA: 0x0002CB4A File Offset: 0x0002AD4A
			private void StopResize()
			{
				this.column_resize_active = false;
				this.resize_column = null;
				base.Capture = false;
				this.Cursor = Cursors.Default;
			}

			// Token: 0x06000A33 RID: 2611 RVA: 0x0002CB6C File Offset: 0x0002AD6C
			private void HeaderMouseMove(object sender, MouseEventArgs me)
			{
				Point point = new Point(me.X + this.owner.h_marker, me.Y);
				if (this.column_resize_active)
				{
					int num = point.X - this.resize_column.X;
					if (num < 0)
					{
						num = 0;
					}
					if (!this.owner.CanProceedWithResize(this.resize_column, num))
					{
						this.StopResize();
						return;
					}
					this.resize_column.Width = num;
					return;
				}
				else
				{
					this.resize_column = null;
					if (this.clicked_column != null)
					{
						if (this.owner.AllowColumnReorder)
						{
							Rectangle rect = this.drag_column.Rect;
							rect.X = this.clicked_column.Rect.X + me.X - this.drag_x;
							this.drag_column.Rect = rect;
							int num2 = me.X + this.owner.h_marker;
							ColumnHeader columnHeader = this.ColumnAtX(num2);
							if (columnHeader == null)
							{
								this.drag_to_index = this.owner.Columns.Count;
							}
							else if (num2 < columnHeader.X + columnHeader.Width / 2)
							{
								this.drag_to_index = this.GetReorderedIndex(columnHeader);
							}
							else
							{
								this.drag_to_index = this.GetReorderedIndex(columnHeader) + 1;
							}
							base.Invalidate();
							return;
						}
						ColumnHeader columnHeader2 = this.ColumnAtX(me.X + this.owner.h_marker);
						bool pressed = this.clicked_column.Pressed;
						this.clicked_column.Pressed = columnHeader2 == this.clicked_column;
						if (this.clicked_column.Pressed ^ pressed)
						{
							this.Invalidate(this.clicked_column);
						}
						return;
					}
					else
					{
						for (int i = 0; i < this.owner.Columns.Count; i++)
						{
							Rectangle rect2 = this.owner.Columns[i].Rect;
							if (rect2.Contains(point))
							{
								this.EnteredColumnHeader = this.owner.Columns[i];
							}
							rect2.X = rect2.Right - 5;
							rect2.Width = 10;
							if (rect2.Contains(point))
							{
								if (i < this.owner.Columns.Count - 1 && this.owner.Columns[i + 1].Width == 0)
								{
									i++;
								}
								this.resize_column = this.owner.Columns[i];
								break;
							}
						}
						if (this.resize_column == null)
						{
							this.Cursor = Cursors.Default;
							return;
						}
						this.Cursor = Cursors.VSplit;
						return;
					}
				}
			}

			// Token: 0x06000A34 RID: 2612 RVA: 0x0002CE04 File Offset: 0x0002B004
			private void HeaderMouseUp(object sender, MouseEventArgs me)
			{
				base.Capture = false;
				if (this.column_resize_active)
				{
					int index = this.resize_column.Index;
					this.StopResize();
					this.owner.RaiseColumnWidthChanged(index);
					return;
				}
				if (this.clicked_column != null && this.clicked_column.Pressed)
				{
					this.clicked_column.Pressed = false;
					this.Invalidate(this.clicked_column);
					this.owner.OnColumnClick(new ColumnClickEventArgs(this.clicked_column.Index));
				}
				if (this.drag_column != null && this.owner.AllowColumnReorder)
				{
					this.drag_column = null;
					if (this.drag_to_index > this.GetReorderedIndex(this.clicked_column))
					{
						this.drag_to_index--;
					}
					if (this.owner.GetReorderedColumn(this.drag_to_index) != this.clicked_column)
					{
						this.owner.ReorderColumn(this.clicked_column, this.drag_to_index, true);
					}
					this.drag_to_index = -1;
					base.Invalidate();
				}
				this.clicked_column = null;
			}

			// Token: 0x06000A35 RID: 2613 RVA: 0x0002CF0C File Offset: 0x0002B10C
			internal override void OnPaintInternal(PaintEventArgs pe)
			{
				if (this.owner.updating)
				{
					return;
				}
				Theme theme = ThemeEngine.Current;
				theme.DrawListViewHeader(pe.Graphics, pe.ClipRectangle, this.owner);
				if (this.drag_column == null)
				{
					return;
				}
				int num;
				if (this.drag_to_index == this.owner.Columns.Count)
				{
					num = this.owner.GetReorderedColumn(this.drag_to_index - 1).Rect.Right - this.owner.h_marker;
				}
				else
				{
					num = this.owner.GetReorderedColumn(this.drag_to_index).Rect.X - this.owner.h_marker;
				}
				theme.DrawListViewHeaderDragDetails(pe.Graphics, this.owner, this.drag_column, num);
			}

			// Token: 0x06000A36 RID: 2614 RVA: 0x0002CFDC File Offset: 0x0002B1DC
			protected override void WndProc(ref Message m)
			{
				Msg msg = (Msg)m.Msg;
				if (msg == Msg.WM_SETFOCUS)
				{
					this.owner.Focus();
					return;
				}
				base.WndProc(ref m);
			}

			// Token: 0x04000710 RID: 1808
			private ListView owner;

			// Token: 0x04000711 RID: 1809
			private bool column_resize_active;

			// Token: 0x04000712 RID: 1810
			private ColumnHeader resize_column;

			// Token: 0x04000713 RID: 1811
			private ColumnHeader clicked_column;

			// Token: 0x04000714 RID: 1812
			private ColumnHeader drag_column;

			// Token: 0x04000715 RID: 1813
			private int drag_x;

			// Token: 0x04000716 RID: 1814
			private int drag_to_index = -1;

			// Token: 0x04000717 RID: 1815
			private ColumnHeader entered_column_header;
		}

		// Token: 0x0200010C RID: 268
		private class ItemComparer
		{
		}

		/// <summary>Represents the collection containing the indexes to the checked items in a list view control.</summary>
		// Token: 0x0200010D RID: 269
		[ListBindable(false)]
		public class CheckedIndexCollection : IList, ICollection, IEnumerable
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListView.CheckedIndexCollection" /> class.</summary>
			/// <param name="owner">A <see cref="T:System.Windows.Forms.ListView" /> control that owns the collection. </param>
			// Token: 0x06000A37 RID: 2615 RVA: 0x0002D008 File Offset: 0x0002B208
			public CheckedIndexCollection(ListView owner)
			{
				this.owner = owner;
			}

			/// <summary>Gets the number of items in the collection.</summary>
			/// <returns>The number of items in the collection.</returns>
			// Token: 0x1700028D RID: 653
			// (get) Token: 0x06000A38 RID: 2616 RVA: 0x0002D017 File Offset: 0x0002B217
			[Browsable(false)]
			public int Count
			{
				get
				{
					return this.owner.CheckedItems.Count;
				}
			}

			/// <summary>Gets a value indicating whether the collection is read-only.</summary>
			/// <returns>true if the collection is read-only; otherwise, false.</returns>
			// Token: 0x1700028E RID: 654
			// (get) Token: 0x06000A39 RID: 2617 RVA: 0x00006F54 File Offset: 0x00005154
			public bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			/// <summary>Gets the index value at the specified index within the collection.</summary>
			/// <returns>The index value from the <see cref="T:System.Windows.Forms.ListView.ListViewItemCollection" /> that is stored at the specified location.</returns>
			/// <param name="index">The index of the item in the collection to retrieve. </param>
			/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is less than 0 or greater than or equal to the value of the <see cref="P:System.Windows.Forms.ListView.CheckedIndexCollection.Count" /> property of <see cref="T:System.Windows.Forms.ListView.CheckedIndexCollection" />. </exception>
			// Token: 0x1700028F RID: 655
			public int this[int index]
			{
				get
				{
					int[] indices = this.GetIndices();
					if (index < 0 || index >= indices.Length)
					{
						throw new ArgumentOutOfRangeException("index");
					}
					return indices[index];
				}
			}

			/// <summary>Gets a value indicating whether access to the collection is synchronized (thread safe).</summary>
			/// <returns>false in all cases.</returns>
			// Token: 0x17000290 RID: 656
			// (get) Token: 0x06000A3B RID: 2619 RVA: 0x00002D70 File Offset: 0x00000F70
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			/// <summary>Gets an object that can be used to synchronize access to the collection of controls.</summary>
			/// <returns>The object used to synchronize the collection.</returns>
			// Token: 0x17000291 RID: 657
			// (get) Token: 0x06000A3C RID: 2620 RVA: 0x00002F7A File Offset: 0x0000117A
			object ICollection.SyncRoot
			{
				get
				{
					return this;
				}
			}

			/// <summary>Gets a value indicating whether the <see cref="T:System.Windows.Forms.ListView.CheckedIndexCollection" /> has a fixed size.</summary>
			/// <returns>true in all cases.</returns>
			// Token: 0x17000292 RID: 658
			// (get) Token: 0x06000A3D RID: 2621 RVA: 0x00006F54 File Offset: 0x00005154
			bool IList.IsFixedSize
			{
				get
				{
					return true;
				}
			}

			/// <summary>Gets or sets an object in the <see cref="T:System.Windows.Forms.ListView.CheckedIndexCollection" />.</summary>
			/// <returns>The object from the <see cref="T:System.Windows.Forms.ListView.ListViewItemCollection" /> that is stored at the specified location.</returns>
			/// <param name="index">The zero-based index of the element to get or set.</param>
			// Token: 0x17000293 RID: 659
			object IList.this[int index]
			{
				get
				{
					return this[index];
				}
				set
				{
					throw new NotSupportedException("SetItem operation is not supported.");
				}
			}

			/// <summary>Determines whether the specified index is located in the collection.</summary>
			/// <returns>true if the specified index from the <see cref="T:System.Windows.Forms.ListView.ListViewItemCollection" /> for the <see cref="T:System.Windows.Forms.ListView" /> is an item in the collection; otherwise, false.</returns>
			/// <param name="checkedIndex">The index to locate in the collection. </param>
			// Token: 0x06000A40 RID: 2624 RVA: 0x0002D074 File Offset: 0x0002B274
			public bool Contains(int checkedIndex)
			{
				int[] indices = this.GetIndices();
				for (int i = 0; i < indices.Length; i++)
				{
					if (indices[i] == checkedIndex)
					{
						return true;
					}
				}
				return false;
			}

			/// <summary>Returns an enumerator that can be used to iterate through the checked index collection.</summary>
			/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that represents the checked index collection.</returns>
			// Token: 0x06000A41 RID: 2625 RVA: 0x0002D09F File Offset: 0x0002B29F
			public IEnumerator GetEnumerator()
			{
				return this.GetIndices().GetEnumerator();
			}

			/// <summary>Copies the collection of checked-item indexes into an array.</summary>
			/// <param name="dest">An array of type <see cref="T:System.Int32" />.</param>
			/// <param name="index">The zero-based index in the array at which copying begins. </param>
			/// <exception cref="T:System.ArrayTypeMismatchException">The array type cannot be cast to an <see cref="T:System.Int32" />.</exception>
			// Token: 0x06000A42 RID: 2626 RVA: 0x0002D0AC File Offset: 0x0002B2AC
			void ICollection.CopyTo(Array dest, int index)
			{
				int[] indices = this.GetIndices();
				Array.Copy(indices, 0, dest, index, indices.Length);
			}

			/// <summary>Adds an item to the collection.</summary>
			/// <returns>The zero-based index where <paramref name="value" /> is located in the collection.</returns>
			/// <param name="value">The object to add to the <see cref="T:System.Windows.Forms.ListView.CheckedIndexCollection" />.</param>
			/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
			// Token: 0x06000A43 RID: 2627 RVA: 0x0002D0CC File Offset: 0x0002B2CC
			int IList.Add(object value)
			{
				throw new NotSupportedException("Add operation is not supported.");
			}

			/// <summary>Removes all items from the collection.</summary>
			/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
			// Token: 0x06000A44 RID: 2628 RVA: 0x0002D0D8 File Offset: 0x0002B2D8
			void IList.Clear()
			{
				throw new NotSupportedException("Clear operation is not supported.");
			}

			/// <summary>Checks whether the index corresponding with the <see cref="T:System.Windows.Forms.ListViewItem" /> is checked.</summary>
			/// <returns>true if the index is found in the <see cref="T:System.Windows.Forms.ListView.CheckedIndexCollection" />; otherwise, false.</returns>
			/// <param name="checkedIndex">An index to locate in the <see cref="T:System.Windows.Forms.ListView.CheckedIndexCollection" />.</param>
			// Token: 0x06000A45 RID: 2629 RVA: 0x0002D0E4 File Offset: 0x0002B2E4
			bool IList.Contains(object checkedIndex)
			{
				return checkedIndex is int && this.Contains((int)checkedIndex);
			}

			/// <summary>Returns the index of the specified object in the <see cref="T:System.Windows.Forms.ListView.CheckedIndexCollection" />. </summary>
			/// <returns>The zero-based index in the collection where the specified index of the <see cref="T:System.Windows.Forms.ListView.ListViewItemCollection" /> is located if it is in the <see cref="T:System.Windows.Forms.ListView.CheckedIndexCollection" />; otherwise, -1.</returns>
			/// <param name="checkedIndex">The zero-based index from the <see cref="T:System.Windows.Forms.ListView.ListViewItemCollection" /> to locate in the collection.</param>
			// Token: 0x06000A46 RID: 2630 RVA: 0x0002D0FC File Offset: 0x0002B2FC
			int IList.IndexOf(object checkedIndex)
			{
				if (!(checkedIndex is int))
				{
					return -1;
				}
				return this.IndexOf((int)checkedIndex);
			}

			/// <summary>Inserts an item into the collection at a specified index.</summary>
			/// <param name="index">The index at which <paramref name="value" /> should be inserted.</param>
			/// <param name="value">The object to be added to the <see cref="T:System.Windows.Forms.ListView.SelectedIndexCollection" />.</param>
			/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
			// Token: 0x06000A47 RID: 2631 RVA: 0x0002D114 File Offset: 0x0002B314
			void IList.Insert(int index, object value)
			{
				throw new NotSupportedException("Insert operation is not supported.");
			}

			/// <summary>Removes the first occurrence of an item from the collection.</summary>
			/// <param name="value">The object to be removed from the <see cref="T:System.Windows.Forms.ListView.CheckedIndexCollection" />.</param>
			/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
			// Token: 0x06000A48 RID: 2632 RVA: 0x0002D120 File Offset: 0x0002B320
			void IList.Remove(object value)
			{
				throw new NotSupportedException("Remove operation is not supported.");
			}

			/// <summary>Removes an item from the collection at a specified index.</summary>
			/// <param name="index">The zero-based index of the item to remove.</param>
			/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
			// Token: 0x06000A49 RID: 2633 RVA: 0x0002D12C File Offset: 0x0002B32C
			void IList.RemoveAt(int index)
			{
				throw new NotSupportedException("RemoveAt operation is not supported.");
			}

			/// <summary>Returns the index within the <see cref="T:System.Windows.Forms.ListView.CheckedIndexCollection" /> of the specified index from the <see cref="T:System.Windows.Forms.ListView.ListViewItemCollection" /> of the list view control.</summary>
			/// <returns>The zero-based index in the collection where the specified index of the <see cref="T:System.Windows.Forms.ListView.ListViewItemCollection" /> is located within the <see cref="T:System.Windows.Forms.ListView.CheckedIndexCollection" />; otherwise, -1 if the index is not located in the collection.</returns>
			/// <param name="checkedIndex">The zero-based index from the <see cref="T:System.Windows.Forms.ListView.ListViewItemCollection" /> to locate in the collection. </param>
			// Token: 0x06000A4A RID: 2634 RVA: 0x0002D138 File Offset: 0x0002B338
			public int IndexOf(int checkedIndex)
			{
				int[] indices = this.GetIndices();
				for (int i = 0; i < indices.Length; i++)
				{
					if (indices[i] == checkedIndex)
					{
						return i;
					}
				}
				return -1;
			}

			// Token: 0x06000A4B RID: 2635 RVA: 0x0002D164 File Offset: 0x0002B364
			private int[] GetIndices()
			{
				ArrayList list = this.owner.CheckedItems.List;
				int[] array = new int[list.Count];
				for (int i = 0; i < list.Count; i++)
				{
					ListViewItem listViewItem = (ListViewItem)list[i];
					array[i] = listViewItem.Index;
				}
				return array;
			}

			// Token: 0x04000718 RID: 1816
			private readonly ListView owner;
		}

		/// <summary>Represents the collection of checked items in a list view control.</summary>
		// Token: 0x0200010E RID: 270
		[ListBindable(false)]
		public class CheckedListViewItemCollection : IList, ICollection, IEnumerable
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListView.CheckedListViewItemCollection" /> class.</summary>
			/// <param name="owner">The <see cref="T:System.Windows.Forms.ListView" /> control that owns the collection. </param>
			// Token: 0x06000A4C RID: 2636 RVA: 0x0002D1B6 File Offset: 0x0002B3B6
			public CheckedListViewItemCollection(ListView owner)
			{
				this.owner = owner;
				this.owner.Items.Changed += this.ItemsCollection_Changed;
			}

			/// <summary>Gets the number of items in the collection.</summary>
			/// <returns>The number of items in the collection.</returns>
			// Token: 0x17000294 RID: 660
			// (get) Token: 0x06000A4D RID: 2637 RVA: 0x0002D1E1 File Offset: 0x0002B3E1
			[Browsable(false)]
			public int Count
			{
				get
				{
					if (!this.owner.CheckBoxes)
					{
						return 0;
					}
					return this.List.Count;
				}
			}

			/// <summary>Gets a value indicating whether the collection is read-only.</summary>
			/// <returns>true if the collection is read-only; otherwise, false.</returns>
			// Token: 0x17000295 RID: 661
			// (get) Token: 0x06000A4E RID: 2638 RVA: 0x00006F54 File Offset: 0x00005154
			public bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			/// <summary>Gets the item at the specified index within the collection.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.ListViewItem" /> representing the item located at the specified index within the collection.</returns>
			/// <param name="index">The index of the item in the collection to retrieve. </param>
			/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is less than 0 or greater than or equal to the value of the <see cref="P:System.Windows.Forms.ListView.CheckedListViewItemCollection.Count" /> property of <see cref="T:System.Windows.Forms.ListView.CheckedListViewItemCollection" />. </exception>
			/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Windows.Forms.ListView" /> is in virtual mode.</exception>
			// Token: 0x17000296 RID: 662
			public ListViewItem this[int index]
			{
				get
				{
					if (this.owner.VirtualMode)
					{
						throw new InvalidOperationException();
					}
					ArrayList arrayList = this.List;
					if (index < 0 || index >= arrayList.Count)
					{
						throw new ArgumentOutOfRangeException("index");
					}
					return (ListViewItem)arrayList[index];
				}
			}

			/// <summary>Gets a value indicating whether access to the <see cref="T:System.Windows.Forms.ListView.CheckedListViewItemCollection" /> is synchronized (thread safe).</summary>
			/// <returns>false in all cases.</returns>
			// Token: 0x17000297 RID: 663
			// (get) Token: 0x06000A50 RID: 2640 RVA: 0x00002D70 File Offset: 0x00000F70
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			/// <summary>Gets an object that can be used to synchronize access to the collection of controls.</summary>
			/// <returns>The object used to synchronize the collection.</returns>
			// Token: 0x17000298 RID: 664
			// (get) Token: 0x06000A51 RID: 2641 RVA: 0x00002F7A File Offset: 0x0000117A
			object ICollection.SyncRoot
			{
				get
				{
					return this;
				}
			}

			/// <summary>Gets a value indicating whether the collection has a fixed size.</summary>
			/// <returns>true in all cases.</returns>
			// Token: 0x17000299 RID: 665
			// (get) Token: 0x06000A52 RID: 2642 RVA: 0x00006F54 File Offset: 0x00005154
			bool IList.IsFixedSize
			{
				get
				{
					return true;
				}
			}

			/// <summary>Gets or sets an object from the collection.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.ListViewItem" /> that represents the item located at the specified index within the collection.</returns>
			/// <param name="index">The zero-based index of the element to get or set.</param>
			/// <exception cref="T:System.NotSupportedException">This property cannot be set.</exception>
			// Token: 0x1700029A RID: 666
			object IList.this[int index]
			{
				get
				{
					return this[index];
				}
				set
				{
					throw new NotSupportedException("SetItem operation is not supported.");
				}
			}

			/// <summary>Determines whether the specified item is located in the collection.</summary>
			/// <returns>true if the specified item is located in the collection; otherwise, false.</returns>
			/// <param name="item">A <see cref="T:System.Windows.Forms.ListViewItem" /> representing the item to locate in the collection. </param>
			// Token: 0x06000A55 RID: 2645 RVA: 0x0002D254 File Offset: 0x0002B454
			public bool Contains(ListViewItem item)
			{
				return this.owner.CheckBoxes && this.List.Contains(item);
			}

			/// <summary>Copies the entire collection into an existing array at a specified location within the array.</summary>
			/// <param name="dest">An <see cref="T:System.Array" /> representing the array to copy the contents of the collection to. </param>
			/// <param name="index">The location within the destination array to copy the items from the collection to. </param>
			// Token: 0x06000A56 RID: 2646 RVA: 0x0002D271 File Offset: 0x0002B471
			public void CopyTo(Array dest, int index)
			{
				if (this.owner.VirtualMode)
				{
					throw new InvalidOperationException();
				}
				if (!this.owner.CheckBoxes)
				{
					return;
				}
				this.List.CopyTo(dest, index);
			}

			/// <summary>Returns an enumerator that can be used to iterate through the checked item collection.</summary>
			/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that represents the checked item collection.</returns>
			// Token: 0x06000A57 RID: 2647 RVA: 0x0002D2A1 File Offset: 0x0002B4A1
			public IEnumerator GetEnumerator()
			{
				if (this.owner.VirtualMode)
				{
					throw new InvalidOperationException();
				}
				if (!this.owner.CheckBoxes)
				{
					return new ListViewItem[0].GetEnumerator();
				}
				return this.List.GetEnumerator();
			}

			/// <summary>Adds an item to the collection.</summary>
			/// <returns>The zero-based index where value is located in the collection.</returns>
			/// <param name="value">The item to add to the collection.</param>
			/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
			// Token: 0x06000A58 RID: 2648 RVA: 0x0002D0CC File Offset: 0x0002B2CC
			int IList.Add(object value)
			{
				throw new NotSupportedException("Add operation is not supported.");
			}

			/// <summary>Removes all items from the collection.</summary>
			/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
			// Token: 0x06000A59 RID: 2649 RVA: 0x0002D0D8 File Offset: 0x0002B2D8
			void IList.Clear()
			{
				throw new NotSupportedException("Clear operation is not supported.");
			}

			/// <summary>Verifies whether the item is checked.</summary>
			/// <returns>true if item is found in the <see cref="T:System.Windows.Forms.ListView.CheckedListViewItemCollection" />; otherwise, false.</returns>
			/// <param name="item">A <see cref="T:System.Windows.Forms.ListViewItem" /> to locate in the <see cref="T:System.Windows.Forms.ListView.CheckedListViewItemCollection" />.</param>
			// Token: 0x06000A5A RID: 2650 RVA: 0x0002D2DA File Offset: 0x0002B4DA
			bool IList.Contains(object item)
			{
				return item is ListViewItem && this.Contains((ListViewItem)item);
			}

			/// <summary>Returns the index within the collection of the specified item.</summary>
			/// <returns>The zero-based index of the item if it is in the collection; otherwise, -1.</returns>
			/// <param name="item">A <see cref="T:System.Windows.Forms.ListViewItem" /> that represents the item to locate in the collection.</param>
			// Token: 0x06000A5B RID: 2651 RVA: 0x0002D2F2 File Offset: 0x0002B4F2
			int IList.IndexOf(object item)
			{
				if (!(item is ListViewItem))
				{
					return -1;
				}
				return this.IndexOf((ListViewItem)item);
			}

			/// <summary>Inserts an item into the collection at a specified index.</summary>
			/// <param name="index">The index at which value should be inserted.</param>
			/// <param name="value">The object to be added to the <see cref="T:System.Windows.Forms.ListView.SelectedIndexCollection" />.</param>
			/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
			// Token: 0x06000A5C RID: 2652 RVA: 0x0002D114 File Offset: 0x0002B314
			void IList.Insert(int index, object value)
			{
				throw new NotSupportedException("Insert operation is not supported.");
			}

			/// <summary>Removes the first occurrence of an item from the collection.</summary>
			/// <param name="value">The object to be removed from the <see cref="T:System.Windows.Forms.ListView.CheckedListViewItemCollection" />.</param>
			/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
			// Token: 0x06000A5D RID: 2653 RVA: 0x0002D120 File Offset: 0x0002B320
			void IList.Remove(object value)
			{
				throw new NotSupportedException("Remove operation is not supported.");
			}

			/// <summary>Removes an item from the collection at the specified index.</summary>
			/// <param name="index">The zero-based index of the item to remove.</param>
			/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
			// Token: 0x06000A5E RID: 2654 RVA: 0x0002D12C File Offset: 0x0002B32C
			void IList.RemoveAt(int index)
			{
				throw new NotSupportedException("RemoveAt operation is not supported.");
			}

			/// <summary>Returns the index within the collection of the specified item.</summary>
			/// <returns>The zero-based index of the item in the collection; otherwise, -1.</returns>
			/// <param name="item">A <see cref="T:System.Windows.Forms.ListViewItem" /> representing the item to locate in the collection. </param>
			// Token: 0x06000A5F RID: 2655 RVA: 0x0002D30A File Offset: 0x0002B50A
			public int IndexOf(ListViewItem item)
			{
				if (this.owner.VirtualMode)
				{
					throw new InvalidOperationException();
				}
				if (!this.owner.CheckBoxes)
				{
					return -1;
				}
				return this.List.IndexOf(item);
			}

			// Token: 0x1700029B RID: 667
			// (get) Token: 0x06000A60 RID: 2656 RVA: 0x0002D33C File Offset: 0x0002B53C
			internal ArrayList List
			{
				get
				{
					if (this.list == null)
					{
						this.list = new ArrayList();
						foreach (object obj in this.owner.Items)
						{
							ListViewItem listViewItem = (ListViewItem)obj;
							if (listViewItem.Checked)
							{
								this.list.Add(listViewItem);
							}
						}
					}
					return this.list;
				}
			}

			// Token: 0x06000A61 RID: 2657 RVA: 0x0002D3C4 File Offset: 0x0002B5C4
			internal void Reset()
			{
				this.list = null;
			}

			// Token: 0x06000A62 RID: 2658 RVA: 0x0002D3CD File Offset: 0x0002B5CD
			private void ItemsCollection_Changed()
			{
				this.Reset();
			}

			// Token: 0x04000719 RID: 1817
			private readonly ListView owner;

			// Token: 0x0400071A RID: 1818
			private ArrayList list;
		}

		/// <summary>Represents the collection of column headers in a <see cref="T:System.Windows.Forms.ListView" /> control.</summary>
		// Token: 0x0200010F RID: 271
		[ListBindable(false)]
		public class ColumnHeaderCollection : IList, ICollection, IEnumerable
		{
			// Token: 0x06000A63 RID: 2659 RVA: 0x0002D3D8 File Offset: 0x0002B5D8
			internal void OnUIACollectionChangedEvent(CollectionChangeEventArgs args)
			{
				if (this.owner == null)
				{
					return;
				}
				CollectionChangeEventHandler collectionChangeEventHandler = (CollectionChangeEventHandler)this.owner.Events[ListView.ColumnHeaderCollection.UIACollectionChangedEvent];
				if (collectionChangeEventHandler != null)
				{
					collectionChangeEventHandler(this.owner, args);
				}
			}

			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListView.ColumnHeaderCollection" /> class.</summary>
			/// <param name="owner">The <see cref="T:System.Windows.Forms.ListView" /> that owns this collection. </param>
			// Token: 0x06000A64 RID: 2660 RVA: 0x0002D419 File Offset: 0x0002B619
			public ColumnHeaderCollection(ListView owner)
			{
				this.list = new ArrayList();
				this.owner = owner;
			}

			/// <summary>Gets the number of items in the collection.</summary>
			/// <returns>The number of items in the collection.</returns>
			// Token: 0x1700029C RID: 668
			// (get) Token: 0x06000A65 RID: 2661 RVA: 0x0002D433 File Offset: 0x0002B633
			[Browsable(false)]
			public int Count
			{
				get
				{
					return this.list.Count;
				}
			}

			/// <summary>Gets a value indicating whether the collection is read-only.</summary>
			/// <returns>true if the collection is read-only; otherwise, false.</returns>
			// Token: 0x1700029D RID: 669
			// (get) Token: 0x06000A66 RID: 2662 RVA: 0x00002D70 File Offset: 0x00000F70
			public bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			/// <summary>Gets the column header at the specified index within the collection.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.ColumnHeader" /> representing the column header located at the specified index within the collection.</returns>
			/// <param name="index">The index of the column header to retrieve from the collection.</param>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			///   <paramref name="index" /> is less than 0 or greater than or equal to the value of the <see cref="P:System.Windows.Forms.ListView.ColumnHeaderCollection.Count" /> property of the <see cref="T:System.Windows.Forms.ListView.ColumnHeaderCollection" />. </exception>
			// Token: 0x1700029E RID: 670
			public virtual ColumnHeader this[int index]
			{
				get
				{
					if (index < 0 || index >= this.list.Count)
					{
						throw new ArgumentOutOfRangeException("index");
					}
					return (ColumnHeader)this.list[index];
				}
			}

			/// <summary>Gets a value indicating whether access to the <see cref="T:System.Windows.Forms.ListView.ColumnHeaderCollection" /> is synchronized (thread safe).</summary>
			/// <returns>true in all cases.</returns>
			// Token: 0x1700029F RID: 671
			// (get) Token: 0x06000A68 RID: 2664 RVA: 0x00006F54 File Offset: 0x00005154
			bool ICollection.IsSynchronized
			{
				get
				{
					return true;
				}
			}

			/// <summary>Gets an object that can be used to synchronize access to the collection of controls.</summary>
			/// <returns>The object used to synchronize the collection.</returns>
			// Token: 0x170002A0 RID: 672
			// (get) Token: 0x06000A69 RID: 2665 RVA: 0x00002F7A File Offset: 0x0000117A
			object ICollection.SyncRoot
			{
				get
				{
					return this;
				}
			}

			/// <summary>Gets a value indicating whether the <see cref="T:System.Windows.Forms.ListView.ColumnHeaderCollection" /> has a fixed size.</summary>
			/// <returns>false in all cases.</returns>
			// Token: 0x170002A1 RID: 673
			// (get) Token: 0x06000A6A RID: 2666 RVA: 0x0002D470 File Offset: 0x0002B670
			bool IList.IsFixedSize
			{
				get
				{
					return this.list.IsFixedSize;
				}
			}

			/// <summary>Gets or sets the column header at the specified index within the collection.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.ColumnHeader" /> that represents the column header located at the specified index within the collection.</returns>
			/// <param name="index">The zero-based index of the element to get or set.</param>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			///   <paramref name="index" /> is less than 0 or greater than or equal to the value of the <see cref="P:System.Windows.Forms.ListView.ColumnHeaderCollection.Count" /> property of the <see cref="T:System.Windows.Forms.ListView.ColumnHeaderCollection" />.</exception>
			// Token: 0x170002A2 RID: 674
			object IList.this[int index]
			{
				get
				{
					return this[index];
				}
				set
				{
					throw new NotSupportedException("SetItem operation is not supported.");
				}
			}

			/// <summary>Adds an existing <see cref="T:System.Windows.Forms.ColumnHeader" /> to the collection.</summary>
			/// <returns>The zero-based index into the collection where the item was added.</returns>
			/// <param name="value">The <see cref="T:System.Windows.Forms.ColumnHeader" /> to add to the collection. </param>
			// Token: 0x06000A6D RID: 2669 RVA: 0x0002D488 File Offset: 0x0002B688
			public virtual int Add(ColumnHeader value)
			{
				int num = this.list.Add(value);
				this.owner.AddColumn(value, num, true);
				this.OnUIACollectionChangedEvent(new CollectionChangeEventArgs(CollectionChangeAction.Add, value));
				return num;
			}

			/// <summary>Adds an array of column headers to the collection.</summary>
			/// <param name="values">An array of <see cref="T:System.Windows.Forms.ColumnHeader" /> objects to add to the collection. </param>
			// Token: 0x06000A6E RID: 2670 RVA: 0x0002D4C0 File Offset: 0x0002B6C0
			public virtual void AddRange(ColumnHeader[] values)
			{
				foreach (ColumnHeader columnHeader in values)
				{
					int num = this.list.Add(columnHeader);
					this.owner.AddColumn(columnHeader, num, false);
				}
				this.owner.Redraw(true);
			}

			/// <summary>Removes all column headers from the collection.</summary>
			// Token: 0x06000A6F RID: 2671 RVA: 0x0002D508 File Offset: 0x0002B708
			public virtual void Clear()
			{
				foreach (object obj in this.list)
				{
					((ColumnHeader)obj).SetListView(null);
				}
				this.list.Clear();
				this.owner.ReorderColumns(new int[0], true);
				this.OnUIACollectionChangedEvent(new CollectionChangeEventArgs(CollectionChangeAction.Refresh, null));
			}

			/// <summary>Determines whether the specified column header is located in the collection.</summary>
			/// <returns>true if the column header is contained in the collection; otherwise, false.</returns>
			/// <param name="value">A <see cref="T:System.Windows.Forms.ColumnHeader" /> representing the column header to locate in the collection. </param>
			// Token: 0x06000A70 RID: 2672 RVA: 0x0002D58C File Offset: 0x0002B78C
			public bool Contains(ColumnHeader value)
			{
				return this.list.Contains(value);
			}

			/// <summary>Returns an enumerator to use to iterate through the column header collection.</summary>
			/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that represents the column header collection.</returns>
			// Token: 0x06000A71 RID: 2673 RVA: 0x0002D59A File Offset: 0x0002B79A
			public IEnumerator GetEnumerator()
			{
				return this.list.GetEnumerator();
			}

			/// <summary>Copies the <see cref="T:System.Windows.Forms.ColumnHeader" /> objects in the <see cref="T:System.Windows.Forms.ListView.ColumnHeaderCollection" /> to an array, starting at a particular array index.</summary>
			/// <param name="dest">The one-dimensional array that is the destination of the elements copied from the collection. The array must have zero-based indexing. </param>
			/// <param name="index">The zero-based index in the array at which copying begins.</param>
			// Token: 0x06000A72 RID: 2674 RVA: 0x0002D5A7 File Offset: 0x0002B7A7
			void ICollection.CopyTo(Array dest, int index)
			{
				this.list.CopyTo(dest, index);
			}

			/// <summary>Adds a <see cref="T:System.Windows.Forms.ColumnHeader" /> to the <see cref="T:System.Windows.Forms.ListView" />.</summary>
			/// <returns>The zero-based index indicating the location of the object that was added to the collection</returns>
			/// <param name="value">The <see cref="T:System.Windows.Forms.ColumnHeader" /> to be added to the <see cref="T:System.Windows.Forms.ListView" />.</param>
			/// <exception cref="T:System.ArgumentException">
			///   <paramref name="value" /> is not a <see cref="T:System.Windows.Forms.ColumnHeader" />.</exception>
			// Token: 0x06000A73 RID: 2675 RVA: 0x0002D5B6 File Offset: 0x0002B7B6
			int IList.Add(object value)
			{
				if (!(value is ColumnHeader))
				{
					throw new ArgumentException("Not of type ColumnHeader", "value");
				}
				return this.Add((ColumnHeader)value);
			}

			/// <summary>Determines whether the specified column header is located in the collection.</summary>
			/// <returns>true if the object is a column header that is contained in the collection; otherwise, false.</returns>
			/// <param name="value">An object that represents the column header to locate in the collection.</param>
			// Token: 0x06000A74 RID: 2676 RVA: 0x0002D5DC File Offset: 0x0002B7DC
			bool IList.Contains(object value)
			{
				if (!(value is ColumnHeader))
				{
					throw new ArgumentException("Not of type ColumnHeader", "value");
				}
				return this.Contains((ColumnHeader)value);
			}

			/// <summary>Returns the index, within the collection, of the specified column header.</summary>
			/// <param name="value">An object that represents the column header to locate in the collection.</param>
			// Token: 0x06000A75 RID: 2677 RVA: 0x0002D602 File Offset: 0x0002B802
			int IList.IndexOf(object value)
			{
				if (!(value is ColumnHeader))
				{
					throw new ArgumentException("Not of type ColumnHeader", "value");
				}
				return this.IndexOf((ColumnHeader)value);
			}

			/// <summary>Inserts an existing column header into the collection at the specified index.</summary>
			/// <param name="index">The zero-based index location where the column header is inserted.</param>
			/// <param name="value">The <see cref="T:System.Windows.Forms.ColumnHeader" /> to insert into the collection.</param>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			///   <paramref name="index" /> is less than 0 or greater than or equal to the value of the <see cref="P:System.Windows.Forms.ListView.ColumnHeaderCollection.Count" /> property of the <see cref="T:System.Windows.Forms.ListView.ColumnHeaderCollection" />.</exception>
			// Token: 0x06000A76 RID: 2678 RVA: 0x0002D628 File Offset: 0x0002B828
			void IList.Insert(int index, object value)
			{
				if (!(value is ColumnHeader))
				{
					throw new ArgumentException("Not of type ColumnHeader", "value");
				}
				this.Insert(index, (ColumnHeader)value);
			}

			/// <summary>Removes the specified column header from the collection.</summary>
			/// <param name="value">A <see cref="T:System.Windows.Forms.ColumnHeader" /> that represents the column header to remove from the collection.</param>
			// Token: 0x06000A77 RID: 2679 RVA: 0x0002D64F File Offset: 0x0002B84F
			void IList.Remove(object value)
			{
				if (!(value is ColumnHeader))
				{
					throw new ArgumentException("Not of type ColumnHeader", "value");
				}
				this.Remove((ColumnHeader)value);
			}

			/// <summary>Returns the index, within the collection, of the specified column header.</summary>
			/// <returns>The zero-based index of the column header's location in the collection. If the column header is not located in the collection, the return value is -1.</returns>
			/// <param name="value">A <see cref="T:System.Windows.Forms.ColumnHeader" /> representing the column header to locate in the collection. </param>
			// Token: 0x06000A78 RID: 2680 RVA: 0x0002D675 File Offset: 0x0002B875
			public int IndexOf(ColumnHeader value)
			{
				return this.list.IndexOf(value);
			}

			/// <summary>Inserts an existing column header into the collection at the specified index.</summary>
			/// <param name="index">The zero-based index location where the column header is inserted. </param>
			/// <param name="value">The <see cref="T:System.Windows.Forms.ColumnHeader" /> to insert into the collection. </param>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			///   <paramref name="index" /> is less than 0 or greater than or equal to the value of the <see cref="P:System.Windows.Forms.ListView.ColumnHeaderCollection.Count" /> property of the <see cref="T:System.Windows.Forms.ListView.ColumnHeaderCollection" />. </exception>
			// Token: 0x06000A79 RID: 2681 RVA: 0x0002D684 File Offset: 0x0002B884
			public void Insert(int index, ColumnHeader value)
			{
				if (index < 0 || index > this.list.Count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				this.list.Insert(index, value);
				this.owner.AddColumn(value, index, true);
				this.OnUIACollectionChangedEvent(new CollectionChangeEventArgs(CollectionChangeAction.Add, value));
			}

			/// <summary>Removes the specified column header from the collection.</summary>
			/// <param name="column">A <see cref="T:System.Windows.Forms.ColumnHeader" /> representing the column header to remove from the collection. </param>
			// Token: 0x06000A7A RID: 2682 RVA: 0x0002D6D8 File Offset: 0x0002B8D8
			public virtual void Remove(ColumnHeader column)
			{
				if (!this.Contains(column))
				{
					return;
				}
				this.list.Remove(column);
				column.SetListView(null);
				int internalDisplayIndex = column.InternalDisplayIndex;
				int[] array = new int[this.list.Count];
				for (int i = 0; i < array.Length; i++)
				{
					int internalDisplayIndex2 = ((ColumnHeader)this.list[i]).InternalDisplayIndex;
					if (internalDisplayIndex2 < internalDisplayIndex)
					{
						array[i] = internalDisplayIndex2;
					}
					else
					{
						array[i] = internalDisplayIndex2 - 1;
					}
				}
				column.InternalDisplayIndex = -1;
				this.owner.ReorderColumns(array, true);
				this.OnUIACollectionChangedEvent(new CollectionChangeEventArgs(CollectionChangeAction.Remove, column));
			}

			/// <summary>Removes the column header at the specified index within the collection.</summary>
			/// <param name="index">The zero-based index of the column header to remove. </param>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			///   <paramref name="index" /> is less than 0 or greater than or equal to the value of the <see cref="P:System.Windows.Forms.ListView.ColumnHeaderCollection.Count" /> property of the <see cref="T:System.Windows.Forms.ListView.ColumnHeaderCollection" />. </exception>
			// Token: 0x06000A7B RID: 2683 RVA: 0x0002D770 File Offset: 0x0002B970
			public virtual void RemoveAt(int index)
			{
				if (index < 0 || index >= this.list.Count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				ColumnHeader columnHeader = (ColumnHeader)this.list[index];
				this.Remove(columnHeader);
			}

			// Token: 0x0400071B RID: 1819
			internal ArrayList list;

			// Token: 0x0400071C RID: 1820
			private ListView owner;

			// Token: 0x0400071D RID: 1821
			private static object UIACollectionChangedEvent = new object();
		}

		/// <summary>Represents the collection of items in a <see cref="T:System.Windows.Forms.ListView" /> control or assigned to a <see cref="T:System.Windows.Forms.ListViewGroup" />. </summary>
		// Token: 0x02000110 RID: 272
		[ListBindable(false)]
		public class ListViewItemCollection : IList, ICollection, IEnumerable
		{
			// Token: 0x06000A7D RID: 2685 RVA: 0x0002D7C0 File Offset: 0x0002B9C0
			internal void OnUIACollectionChangedEvent(CollectionChangeEventArgs args)
			{
				if (this.owner == null)
				{
					return;
				}
				CollectionChangeEventHandler collectionChangeEventHandler = (CollectionChangeEventHandler)this.owner.Events[ListView.ListViewItemCollection.UIACollectionChangedEvent];
				if (collectionChangeEventHandler != null)
				{
					collectionChangeEventHandler(this.owner, args);
				}
			}

			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListView.ListViewItemCollection" /> class. </summary>
			/// <param name="owner">The <see cref="T:System.Windows.Forms.ListView" /> that owns the collection. </param>
			// Token: 0x06000A7E RID: 2686 RVA: 0x0002D801 File Offset: 0x0002BA01
			public ListViewItemCollection(ListView owner)
			{
				this.list = new ArrayList(0);
				this.owner = owner;
			}

			// Token: 0x06000A7F RID: 2687 RVA: 0x0002D823 File Offset: 0x0002BA23
			internal ListViewItemCollection(ListView owner, ListViewGroup group)
				: this(owner)
			{
				this.group = group;
				this.is_main_collection = false;
			}

			/// <summary>Gets the number of items in the collection.</summary>
			/// <returns>The number of items in the collection.</returns>
			// Token: 0x170002A3 RID: 675
			// (get) Token: 0x06000A80 RID: 2688 RVA: 0x0002D83A File Offset: 0x0002BA3A
			[Browsable(false)]
			public int Count
			{
				get
				{
					if (this.owner != null && this.owner.VirtualMode)
					{
						return this.owner.VirtualListSize;
					}
					return this.list.Count;
				}
			}

			/// <summary>Gets a value indicating whether the collection is read-only.</summary>
			/// <returns>true if the collection is read-only; otherwise, false.</returns>
			// Token: 0x170002A4 RID: 676
			// (get) Token: 0x06000A81 RID: 2689 RVA: 0x00002D70 File Offset: 0x00000F70
			public bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			/// <summary>Gets or sets the item at the specified index within the collection.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.ListViewItem" /> representing the item located at the specified index within the collection.</returns>
			/// <param name="index">The index of the item in the collection to get or set. </param>
			/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is less than 0 or greater than or equal to the value of the <see cref="P:System.Windows.Forms.ListView.ListViewItemCollection.Count" /> property of the <see cref="T:System.Windows.Forms.ListView.ListViewItemCollection" />. </exception>
			// Token: 0x170002A5 RID: 677
			public virtual ListViewItem this[int index]
			{
				get
				{
					if (index < 0 || index >= this.Count)
					{
						throw new ArgumentOutOfRangeException("index");
					}
					if (this.owner != null && this.owner.VirtualMode)
					{
						return this.RetrieveVirtualItemFromOwner(index);
					}
					return (ListViewItem)this.list[index];
				}
				set
				{
					if (index < 0 || index >= this.Count)
					{
						throw new ArgumentOutOfRangeException("index");
					}
					if (this.owner != null && this.owner.VirtualMode)
					{
						throw new InvalidOperationException();
					}
					if (this.list.Contains(value))
					{
						throw new ArgumentException("An item cannot be added more than once. To add an item again, you need to clone it.", "value");
					}
					if (value.ListView != null && value.ListView != this.owner)
					{
						throw new ArgumentException("Cannot add or insert the item '" + value.Text + "' in more than one place. You must first remove it from its current location or clone it.", "value");
					}
					if (this.is_main_collection)
					{
						value.Owner = this.owner;
					}
					else
					{
						if (value.Group != null)
						{
							value.Group.Items.Remove(value);
						}
						value.SetGroup(this.group);
					}
					this.OnUIACollectionChangedEvent(new CollectionChangeEventArgs(CollectionChangeAction.Remove, this.list[index]));
					this.list[index] = value;
					this.CollectionChanged(true);
					this.OnUIACollectionChangedEvent(new CollectionChangeEventArgs(CollectionChangeAction.Add, value));
				}
			}

			/// <summary>Gets a value indicating whether access to the collection is synchronized (thread safe).</summary>
			/// <returns>true in all cases.</returns>
			// Token: 0x170002A6 RID: 678
			// (get) Token: 0x06000A84 RID: 2692 RVA: 0x00006F54 File Offset: 0x00005154
			bool ICollection.IsSynchronized
			{
				get
				{
					return true;
				}
			}

			/// <summary>Gets an object that can be used to synchronize access to the collection of controls.</summary>
			/// <returns>The object used to synchronize the collection.</returns>
			// Token: 0x170002A7 RID: 679
			// (get) Token: 0x06000A85 RID: 2693 RVA: 0x00002F7A File Offset: 0x0000117A
			object ICollection.SyncRoot
			{
				get
				{
					return this;
				}
			}

			/// <summary>Gets a value indicating whether the collection has a fixed size.</summary>
			/// <returns>false in all cases.</returns>
			// Token: 0x170002A8 RID: 680
			// (get) Token: 0x06000A86 RID: 2694 RVA: 0x0002D9C4 File Offset: 0x0002BBC4
			bool IList.IsFixedSize
			{
				get
				{
					return this.list.IsFixedSize;
				}
			}

			/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.ListViewItem" /> at the specified index within the collection.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.ListViewItem" /> that represents the item located at the specified index within the collection.</returns>
			/// <param name="index">The zero-based index of the element to get.</param>
			/// <exception cref="T:System.ArgumentOutOfRangeException">The index parameter is less than 0 or greater than or equal to the value of the <see cref="P:System.Windows.Forms.ListView.ListViewItemCollection.Count" /> property of the <see cref="T:System.Windows.Forms.ListView.ListViewItemCollection" />.</exception>
			// Token: 0x170002A9 RID: 681
			object IList.this[int index]
			{
				get
				{
					return this[index];
				}
				set
				{
					this.OnUIACollectionChangedEvent(new CollectionChangeEventArgs(CollectionChangeAction.Remove, this[index]));
					if (value is ListViewItem)
					{
						this[index] = (ListViewItem)value;
					}
					else
					{
						this[index] = new ListViewItem(value.ToString());
					}
					this.OnChange();
					this.OnUIACollectionChangedEvent(new CollectionChangeEventArgs(CollectionChangeAction.Add, value));
				}
			}

			/// <summary>Adds an existing <see cref="T:System.Windows.Forms.ListViewItem" /> to the collection.</summary>
			/// <returns>The <see cref="T:System.Windows.Forms.ListViewItem" /> that was added to the collection.</returns>
			/// <param name="value">The <see cref="T:System.Windows.Forms.ListViewItem" /> to add to the collection. </param>
			// Token: 0x06000A89 RID: 2697 RVA: 0x0002DA38 File Offset: 0x0002BC38
			public virtual ListViewItem Add(ListViewItem value)
			{
				if (this.owner != null && this.owner.VirtualMode)
				{
					throw new InvalidOperationException();
				}
				this.AddItem(value);
				if (this.is_main_collection || value.ListView != null)
				{
					this.CollectionChanged(true);
				}
				this.OnUIACollectionChangedEvent(new CollectionChangeEventArgs(CollectionChangeAction.Add, value));
				return value;
			}

			/// <summary>Removes all items from the collection.</summary>
			// Token: 0x06000A8A RID: 2698 RVA: 0x0002DA8C File Offset: 0x0002BC8C
			public virtual void Clear()
			{
				if (this.owner != null && this.owner.VirtualMode)
				{
					throw new InvalidOperationException();
				}
				if (this.is_main_collection && this.owner != null)
				{
					this.owner.SetFocusedItem(-1);
					this.owner.h_scroll.Value = (this.owner.v_scroll.Value = 0);
					foreach (object obj in this.owner.groups)
					{
						((ListViewGroup)obj).Items.ClearItemsWithSameListView();
					}
					using (IEnumerator enumerator = this.list.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							object obj2 = enumerator.Current;
							ListViewItem listViewItem = (ListViewItem)obj2;
							this.owner.item_control.CancelEdit(listViewItem);
							listViewItem.Owner = null;
						}
						goto IL_012B;
					}
				}
				foreach (object obj3 in this.list)
				{
					((ListViewItem)obj3).SetGroup(null);
				}
				IL_012B:
				this.list.Clear();
				this.CollectionChanged(false);
				this.OnUIACollectionChangedEvent(new CollectionChangeEventArgs(CollectionChangeAction.Refresh, null));
			}

			// Token: 0x06000A8B RID: 2699 RVA: 0x0002DC0C File Offset: 0x0002BE0C
			private void ClearItemsWithSameListView()
			{
				if (this.is_main_collection)
				{
					return;
				}
				for (int i = this.list.Count - 1; i >= 0; i--)
				{
					ListViewItem listViewItem = this.list[i] as ListViewItem;
					if (listViewItem.ListView == this.group.ListView)
					{
						this.list.RemoveAt(i);
						listViewItem.SetGroup(null);
					}
				}
			}

			/// <summary>Determines whether the specified item is located in the collection.</summary>
			/// <returns>true if the item is contained in the collection; otherwise, false.</returns>
			/// <param name="item">A <see cref="T:System.Windows.Forms.ListViewItem" /> representing the item to locate in the collection. </param>
			// Token: 0x06000A8C RID: 2700 RVA: 0x0002DC72 File Offset: 0x0002BE72
			public bool Contains(ListViewItem item)
			{
				return this.IndexOf(item) != -1;
			}

			/// <summary>Copies the entire collection into an existing array at a specified location within the array.</summary>
			/// <param name="dest">An <see cref="T:System.Array" /> representing the array to copy the contents of the collection to. </param>
			/// <param name="index">The location within the destination array to copy the items from the collection to. </param>
			// Token: 0x06000A8D RID: 2701 RVA: 0x0002DC81 File Offset: 0x0002BE81
			public void CopyTo(Array dest, int index)
			{
				this.list.CopyTo(dest, index);
			}

			/// <summary>Returns an enumerator to use to iterate through the item collection.</summary>
			/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that represents the item collection.</returns>
			/// <exception cref="T:System.InvalidOperationException">The owner <see cref="T:System.Windows.Forms.ListView" /> is in virtual mode.</exception>
			// Token: 0x06000A8E RID: 2702 RVA: 0x0002DC90 File Offset: 0x0002BE90
			public IEnumerator GetEnumerator()
			{
				if (this.owner != null && this.owner.VirtualMode)
				{
					throw new InvalidOperationException();
				}
				return new Control.ControlCollection.ControlCollectionEnumerator(this.list);
			}

			/// <summary>Adds an existing object to the collection.</summary>
			/// <returns>The zero-based index indicating the location of the object if it was added to the collection; otherwise, -1.</returns>
			/// <param name="item">The object to add to the collection.</param>
			// Token: 0x06000A8F RID: 2703 RVA: 0x0002DCB8 File Offset: 0x0002BEB8
			int IList.Add(object item)
			{
				if (this.owner != null && this.owner.VirtualMode)
				{
					throw new InvalidOperationException();
				}
				ListViewItem listViewItem;
				if (item is ListViewItem)
				{
					listViewItem = (ListViewItem)item;
					if (this.list.Contains(listViewItem))
					{
						throw new ArgumentException("An item cannot be added more than once. To add an item again, you need to clone it.", "item");
					}
					if (listViewItem.ListView != null && listViewItem.ListView != this.owner)
					{
						throw new ArgumentException("Cannot add or insert the item '" + listViewItem.Text + "' in more than one place. You must first remove it from its current location or clone it.", "item");
					}
				}
				else
				{
					listViewItem = new ListViewItem(item.ToString());
				}
				listViewItem.Owner = this.owner;
				int num = this.list.Add(listViewItem);
				this.CollectionChanged(true);
				this.OnUIACollectionChangedEvent(new CollectionChangeEventArgs(CollectionChangeAction.Add, listViewItem));
				return num;
			}

			/// <summary>Determines whether the specified item is in the collection.</summary>
			/// <returns>true if the specified item is located in the collection; otherwise, false.</returns>
			/// <param name="item">An object that represents the item to locate in the collection.</param>
			// Token: 0x06000A90 RID: 2704 RVA: 0x0002DD7B File Offset: 0x0002BF7B
			bool IList.Contains(object item)
			{
				return this.Contains((ListViewItem)item);
			}

			/// <summary>Returns the index within the collection of the specified item.</summary>
			/// <returns>The zero-based index of the item if it is in the collection; otherwise, -1.</returns>
			/// <param name="item">A <see cref="T:System.Windows.Forms.ListViewItem" /> that represents the item to locate in the collection.</param>
			// Token: 0x06000A91 RID: 2705 RVA: 0x0002DD89 File Offset: 0x0002BF89
			int IList.IndexOf(object item)
			{
				return this.IndexOf((ListViewItem)item);
			}

			/// <summary>Inserts an object into the collection at the specified index.</summary>
			/// <param name="index">The zero-based index location where the item is inserted.</param>
			/// <param name="item">The object that represents the item to insert.</param>
			/// <exception cref="T:System.ArgumentOutOfRangeException">The index parameter is less than 0 or greater than the value of the <see cref="P:System.Windows.Forms.ListView.ListViewItemCollection.Count" /> property of the <see cref="T:System.Windows.Forms.ListView.ListViewItemCollection" />.</exception>
			// Token: 0x06000A92 RID: 2706 RVA: 0x0002DD97 File Offset: 0x0002BF97
			void IList.Insert(int index, object item)
			{
				if (item is ListViewItem)
				{
					this.Insert(index, (ListViewItem)item);
				}
				else
				{
					this.Insert(index, item.ToString());
				}
				this.OnUIACollectionChangedEvent(new CollectionChangeEventArgs(CollectionChangeAction.Add, this[index]));
			}

			/// <summary>Removes the specified item from the collection.</summary>
			/// <param name="item">A <see cref="T:System.Windows.Forms.ListViewItem" /> that represents the item to remove from the collection.</param>
			// Token: 0x06000A93 RID: 2707 RVA: 0x0002DDD2 File Offset: 0x0002BFD2
			void IList.Remove(object item)
			{
				this.Remove((ListViewItem)item);
			}

			/// <summary>Returns the index within the collection of the specified item.</summary>
			/// <returns>The zero-based index of the item's location in the collection; otherwise, -1 if the item is not located in the collection.</returns>
			/// <param name="item">A <see cref="T:System.Windows.Forms.ListViewItem" /> representing the item to locate in the collection. </param>
			// Token: 0x06000A94 RID: 2708 RVA: 0x0002DDE0 File Offset: 0x0002BFE0
			public int IndexOf(ListViewItem item)
			{
				if (this.owner != null && this.owner.VirtualMode)
				{
					for (int i = 0; i < this.Count; i++)
					{
						if (this.RetrieveVirtualItemFromOwner(i) == item)
						{
							return i;
						}
					}
					return -1;
				}
				return this.list.IndexOf(item);
			}

			/// <summary>Inserts an existing <see cref="T:System.Windows.Forms.ListViewItem" /> into the collection at the specified index.</summary>
			/// <returns>The <see cref="T:System.Windows.Forms.ListViewItem" /> that was inserted into the collection.</returns>
			/// <param name="index">The zero-based index location where the item is inserted. </param>
			/// <param name="item">The <see cref="T:System.Windows.Forms.ListViewItem" /> that represents the item to insert. </param>
			/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is less than 0 or greater than the value of the <see cref="P:System.Windows.Forms.ListView.ListViewItemCollection.Count" /> property of the <see cref="T:System.Windows.Forms.ListView.ListViewItemCollection" />. </exception>
			// Token: 0x06000A95 RID: 2709 RVA: 0x0002DE30 File Offset: 0x0002C030
			public ListViewItem Insert(int index, ListViewItem item)
			{
				if (index < 0 || index > this.list.Count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				if (this.owner != null && this.owner.VirtualMode)
				{
					throw new InvalidOperationException();
				}
				if (this.list.Contains(item))
				{
					throw new ArgumentException("An item cannot be added more than once. To add an item again, you need to clone it.", "item");
				}
				if (item.ListView != null && item.ListView != this.owner)
				{
					throw new ArgumentException("Cannot add or insert the item '" + item.Text + "' in more than one place. You must first remove it from its current location or clone it.", "item");
				}
				if (this.is_main_collection)
				{
					item.Owner = this.owner;
				}
				else
				{
					if (item.Group != null)
					{
						item.Group.Items.Remove(item);
					}
					item.SetGroup(this.group);
				}
				this.list.Insert(index, item);
				if (this.is_main_collection || item.ListView != null)
				{
					this.CollectionChanged(true);
				}
				if (item.Selected)
				{
					item.SetSelectedCore(true);
				}
				this.OnUIACollectionChangedEvent(new CollectionChangeEventArgs(CollectionChangeAction.Add, item));
				return item;
			}

			/// <summary>Creates a new item and inserts it into the collection at the specified index.</summary>
			/// <returns>The <see cref="T:System.Windows.Forms.ListViewItem" /> that was inserted into the collection.</returns>
			/// <param name="index">The zero-based index location where the item is inserted. </param>
			/// <param name="text">The text to display for the item. </param>
			/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is less than 0 or greater than the value of the <see cref="P:System.Windows.Forms.ListView.ListViewItemCollection.Count" /> property of the <see cref="T:System.Windows.Forms.ListView.ListViewItemCollection" />. </exception>
			// Token: 0x06000A96 RID: 2710 RVA: 0x0002DF45 File Offset: 0x0002C145
			public ListViewItem Insert(int index, string text)
			{
				return this.Insert(index, new ListViewItem(text));
			}

			/// <summary>Removes the specified item from the collection.</summary>
			/// <param name="item">A <see cref="T:System.Windows.Forms.ListViewItem" /> representing the item to remove from the collection. </param>
			/// <exception cref="T:System.ArgumentException">The <see cref="T:System.Windows.Forms.ListViewItem" /> assigned to the <paramref name="item" /> parameter is null. </exception>
			// Token: 0x06000A97 RID: 2711 RVA: 0x0002DF54 File Offset: 0x0002C154
			public virtual void Remove(ListViewItem item)
			{
				if (this.owner != null && this.owner.VirtualMode)
				{
					throw new InvalidOperationException();
				}
				int num = this.list.IndexOf(item);
				if (num != -1)
				{
					this.RemoveAt(num);
				}
			}

			/// <summary>Removes the item at the specified index within the collection.</summary>
			/// <param name="index">The zero-based index of the item to remove. </param>
			/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is less than 0 or greater than or equal to the value of the <see cref="P:System.Windows.Forms.ListView.ListViewItemCollection.Count" /> property of the <see cref="T:System.Windows.Forms.ListView.ListViewItemCollection" />. </exception>
			// Token: 0x06000A98 RID: 2712 RVA: 0x0002DF94 File Offset: 0x0002C194
			public virtual void RemoveAt(int index)
			{
				if (index < 0 || index >= this.Count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				if (this.owner != null && this.owner.VirtualMode)
				{
					throw new InvalidOperationException();
				}
				ListViewItem listViewItem = (ListViewItem)this.list[index];
				bool flag = false;
				if (this.is_main_collection && this.owner != null)
				{
					ListViewItem focusedItem = this.owner.FocusedItem;
					if (focusedItem != null && focusedItem.DisplayIndex + 1 >= this.Count)
					{
						this.owner.SetFocusedItem(this.Count - 2);
					}
					flag = this.owner.SelectedIndices.Contains(index);
					this.owner.item_control.CancelEdit(listViewItem);
				}
				this.list.RemoveAt(index);
				if (this.is_main_collection)
				{
					listViewItem.Owner = null;
					if (listViewItem.Group != null)
					{
						listViewItem.Group.Items.Remove(listViewItem);
					}
				}
				else
				{
					listViewItem.SetGroup(null);
				}
				this.CollectionChanged(false);
				if (flag && this.owner != null)
				{
					this.owner.OnSelectedIndexChanged(EventArgs.Empty);
				}
				this.OnUIACollectionChangedEvent(new CollectionChangeEventArgs(CollectionChangeAction.Remove, listViewItem));
			}

			// Token: 0x170002AA RID: 682
			// (set) Token: 0x06000A99 RID: 2713 RVA: 0x0002E0B9 File Offset: 0x0002C2B9
			internal ListView Owner
			{
				set
				{
					this.owner = value;
				}
			}

			// Token: 0x06000A9A RID: 2714 RVA: 0x0002E0C4 File Offset: 0x0002C2C4
			private void AddItem(ListViewItem value)
			{
				if (this.list.Contains(value))
				{
					throw new ArgumentException("An item cannot be added more than once. To add an item again, you need to clone it.", "value");
				}
				if (value.ListView != null && value.ListView != this.owner)
				{
					throw new ArgumentException("Cannot add or insert the item '" + value.Text + "' in more than one place. You must first remove it from its current location or clone it.", "value");
				}
				if (this.is_main_collection)
				{
					value.Owner = this.owner;
				}
				else
				{
					if (value.Group != null)
					{
						value.Group.Items.Remove(value);
					}
					value.SetGroup(this.group);
				}
				value.DisplayIndex = -1;
				this.list.Add(value);
				if (value.Selected)
				{
					value.SetSelectedCore(true);
				}
			}

			// Token: 0x06000A9B RID: 2715 RVA: 0x0002E183 File Offset: 0x0002C383
			private void CollectionChanged(bool sort)
			{
				if (this.owner != null)
				{
					if (sort)
					{
						this.owner.Sort(false);
					}
					this.OnChange();
					this.owner.Redraw(true);
				}
			}

			// Token: 0x06000A9C RID: 2716 RVA: 0x0002E1B0 File Offset: 0x0002C3B0
			private ListViewItem RetrieveVirtualItemFromOwner(int displayIndex)
			{
				RetrieveVirtualItemEventArgs retrieveVirtualItemEventArgs = new RetrieveVirtualItemEventArgs(displayIndex);
				this.owner.OnRetrieveVirtualItem(retrieveVirtualItemEventArgs);
				ListViewItem item = retrieveVirtualItemEventArgs.Item;
				item.Owner = this.owner;
				item.DisplayIndex = displayIndex;
				item.Layout();
				return item;
			}

			// Token: 0x14000037 RID: 55
			// (add) Token: 0x06000A9D RID: 2717 RVA: 0x0002E1F0 File Offset: 0x0002C3F0
			// (remove) Token: 0x06000A9E RID: 2718 RVA: 0x0002E228 File Offset: 0x0002C428
			internal event ListView.CollectionChangedHandler Changed;

			// Token: 0x06000A9F RID: 2719 RVA: 0x0002E25D File Offset: 0x0002C45D
			internal void Sort(IComparer comparer)
			{
				this.list.Sort(comparer);
				this.OnChange();
			}

			// Token: 0x06000AA0 RID: 2720 RVA: 0x0002E271 File Offset: 0x0002C471
			internal void OnChange()
			{
				if (this.Changed != null)
				{
					this.Changed();
				}
			}

			// Token: 0x0400071E RID: 1822
			private readonly ArrayList list;

			// Token: 0x0400071F RID: 1823
			private ListView owner;

			// Token: 0x04000720 RID: 1824
			private ListViewGroup group;

			// Token: 0x04000721 RID: 1825
			private static object UIACollectionChangedEvent = new object();

			// Token: 0x04000722 RID: 1826
			private bool is_main_collection = true;
		}

		/// <summary>Represents the collection that contains the indexes to the selected items in a <see cref="T:System.Windows.Forms.ListView" /> control.</summary>
		// Token: 0x02000111 RID: 273
		[ListBindable(false)]
		public class SelectedIndexCollection : IList, ICollection, IEnumerable
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListView.SelectedIndexCollection" /> class.</summary>
			/// <param name="owner">A <see cref="T:System.Windows.Forms.ListView" /> control that owns the collection. </param>
			// Token: 0x06000AA2 RID: 2722 RVA: 0x0002E292 File Offset: 0x0002C492
			public SelectedIndexCollection(ListView owner)
			{
				this.owner = owner;
				owner.Items.Changed += this.ItemsCollection_Changed;
			}

			/// <summary>Gets the number of items in the collection.</summary>
			/// <returns>The number of items in the collection.</returns>
			// Token: 0x170002AB RID: 683
			// (get) Token: 0x06000AA3 RID: 2723 RVA: 0x0002E2B8 File Offset: 0x0002C4B8
			[Browsable(false)]
			public int Count
			{
				get
				{
					if (!this.owner.is_selection_available)
					{
						return 0;
					}
					return this.List.Count;
				}
			}

			/// <summary>Gets a value indicating whether the collection is read-only.</summary>
			/// <returns>true if the collection is read-only; otherwise, false.</returns>
			// Token: 0x170002AC RID: 684
			// (get) Token: 0x06000AA4 RID: 2724 RVA: 0x00002D70 File Offset: 0x00000F70
			public bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			/// <summary>Gets the index value at the specified index within the collection.</summary>
			/// <returns>The index value from the <see cref="T:System.Windows.Forms.ListView.ListViewItemCollection" /> that is stored at the specified location.</returns>
			/// <param name="index">The index of the item in the collection to retrieve. </param>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			///   <paramref name="index" /> is less than 0 or greater than or equal to the value of the <see cref="P:System.Windows.Forms.ListView.SelectedIndexCollection.Count" /> property of the <see cref="T:System.Windows.Forms.ListView.SelectedIndexCollection" />. </exception>
			// Token: 0x170002AD RID: 685
			public int this[int index]
			{
				get
				{
					if (!this.owner.is_selection_available || index < 0 || index >= this.List.Count)
					{
						throw new ArgumentOutOfRangeException("index");
					}
					return (int)this.List[index];
				}
			}

			/// <summary>Gets a value indicating whether access to the collection is synchronized (thread safe).</summary>
			/// <returns>false in all cases.</returns>
			// Token: 0x170002AE RID: 686
			// (get) Token: 0x06000AA6 RID: 2726 RVA: 0x00002D70 File Offset: 0x00000F70
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			/// <summary>Gets an object that can be used to synchronize access to the collection of controls.</summary>
			/// <returns>The object used to synchronize the collection.</returns>
			// Token: 0x170002AF RID: 687
			// (get) Token: 0x06000AA7 RID: 2727 RVA: 0x00002F7A File Offset: 0x0000117A
			object ICollection.SyncRoot
			{
				get
				{
					return this;
				}
			}

			/// <summary>Gets a value indicating whether the <see cref="T:System.Windows.Forms.ListView.SelectedIndexCollection" /> has a fixed size.</summary>
			/// <returns>true in all cases.</returns>
			// Token: 0x170002B0 RID: 688
			// (get) Token: 0x06000AA8 RID: 2728 RVA: 0x00002D70 File Offset: 0x00000F70
			bool IList.IsFixedSize
			{
				get
				{
					return false;
				}
			}

			/// <summary>Gets or sets an object in the collection.</summary>
			/// <returns>The index value from the <see cref="T:System.Windows.Forms.ListView.ListViewItemCollection" /> that is stored at the specified location.</returns>
			/// <param name="index">The zero-based index of the element to get or set.</param>
			// Token: 0x170002B1 RID: 689
			object IList.this[int index]
			{
				get
				{
					return this[index];
				}
				set
				{
					throw new NotSupportedException("SetItem operation is not supported.");
				}
			}

			/// <summary>Clears the items in the collection.</summary>
			// Token: 0x06000AAB RID: 2731 RVA: 0x0002E320 File Offset: 0x0002C520
			public void Clear()
			{
				if (!this.owner.is_selection_available)
				{
					return;
				}
				foreach (int num in (int[])this.List.ToArray(typeof(int)))
				{
					this.owner.Items[num].Selected = false;
				}
			}

			/// <summary>Determines whether the specified index is located in the collection.</summary>
			/// <returns>true if the specified index from the <see cref="T:System.Windows.Forms.ListView.ListViewItemCollection" /> for the <see cref="T:System.Windows.Forms.ListView" /> is an item in the collection; otherwise, false.</returns>
			/// <param name="selectedIndex">The index to locate in the collection. </param>
			// Token: 0x06000AAC RID: 2732 RVA: 0x0002E37F File Offset: 0x0002C57F
			public bool Contains(int selectedIndex)
			{
				return this.IndexOf(selectedIndex) != -1;
			}

			/// <summary>Copies the entire collection into an existing array at a specified location within the array.</summary>
			/// <param name="dest">An <see cref="T:System.Array" /> representing the array to copy the contents of the collection to. </param>
			/// <param name="index">The location within the destination array to copy the items from the collection to. </param>
			// Token: 0x06000AAD RID: 2733 RVA: 0x0002E38E File Offset: 0x0002C58E
			public void CopyTo(Array dest, int index)
			{
				this.List.CopyTo(dest, index);
			}

			/// <summary>Returns an enumerator that can be used to iterate through the selected index collection.</summary>
			/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that represents the selected index collection.</returns>
			// Token: 0x06000AAE RID: 2734 RVA: 0x0002E39D File Offset: 0x0002C59D
			public IEnumerator GetEnumerator()
			{
				return this.List.GetEnumerator();
			}

			/// <summary>Adds an item to the collection.</summary>
			/// <returns>The location of the added item.</returns>
			/// <param name="value">An object to be added to the <see cref="T:System.Windows.Forms.ListView.SelectedIndexCollection" />.</param>
			/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
			// Token: 0x06000AAF RID: 2735 RVA: 0x0002D0CC File Offset: 0x0002B2CC
			int IList.Add(object value)
			{
				throw new NotSupportedException("Add operation is not supported.");
			}

			/// <summary>Removes all items from the collection.</summary>
			/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
			// Token: 0x06000AB0 RID: 2736 RVA: 0x0002E3AA File Offset: 0x0002C5AA
			void IList.Clear()
			{
				this.Clear();
			}

			/// <summary>Determines whether the specified item is located in the collection.</summary>
			/// <returns>true if the specified index from the <see cref="T:System.Windows.Forms.ListView.ListViewItemCollection" /> for the <see cref="T:System.Windows.Forms.ListView" /> is an item in the collection; otherwise, false.</returns>
			/// <param name="selectedIndex">The index to locate in the collection.</param>
			// Token: 0x06000AB1 RID: 2737 RVA: 0x0002E3B2 File Offset: 0x0002C5B2
			bool IList.Contains(object selectedIndex)
			{
				return selectedIndex is int && this.Contains((int)selectedIndex);
			}

			/// <summary>Returns the index in the <see cref="T:System.Windows.Forms.ListView.SelectedIndexCollection" />. The <see cref="T:System.Windows.Forms.ListView.SelectedIndexCollection" /> contains the indexes of selected items in the <see cref="T:System.Windows.Forms.ListView.ListViewItemCollection" /> of the <see cref="T:System.Windows.Forms.ListView" /> control.</summary>
			/// <param name="selectedIndex">The zero-based index from the <see cref="T:System.Windows.Forms.ListView.ListViewItemCollection" /> to locate in the collection.</param>
			// Token: 0x06000AB2 RID: 2738 RVA: 0x0002E3CA File Offset: 0x0002C5CA
			int IList.IndexOf(object selectedIndex)
			{
				if (!(selectedIndex is int))
				{
					return -1;
				}
				return this.IndexOf((int)selectedIndex);
			}

			/// <summary>Inserts an item into the collection at a specified index.</summary>
			/// <param name="index">The item to be added to the <see cref="T:System.Windows.Forms.ListView.SelectedIndexCollection" />.</param>
			/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
			// Token: 0x06000AB3 RID: 2739 RVA: 0x0002D114 File Offset: 0x0002B314
			void IList.Insert(int index, object value)
			{
				throw new NotSupportedException("Insert operation is not supported.");
			}

			/// <summary>Removes the first occurrence of a specified item from the collection.</summary>
			/// <param name="value">The object to remove from the <see cref="T:System.Windows.Forms.ListView.SelectedIndexCollection" />.</param>
			/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
			// Token: 0x06000AB4 RID: 2740 RVA: 0x0002D120 File Offset: 0x0002B320
			void IList.Remove(object value)
			{
				throw new NotSupportedException("Remove operation is not supported.");
			}

			/// <summary>Removes an item from the collection at a specified index.</summary>
			/// <param name="index">The zero-based index of the item to remove.</param>
			/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
			// Token: 0x06000AB5 RID: 2741 RVA: 0x0002D12C File Offset: 0x0002B32C
			void IList.RemoveAt(int index)
			{
				throw new NotSupportedException("RemoveAt operation is not supported.");
			}

			/// <summary>Returns the index within the <see cref="T:System.Windows.Forms.ListView.SelectedIndexCollection" /> of the specified index from the <see cref="T:System.Windows.Forms.ListView.ListViewItemCollection" /> of the <see cref="T:System.Windows.Forms.ListView" /> control.</summary>
			/// <returns>The zero-based index in the collection where the specified index of the <see cref="T:System.Windows.Forms.ListView.ListViewItemCollection" /> is located within the <see cref="T:System.Windows.Forms.ListView.SelectedIndexCollection" />, or -1 if the index is not located in the collection.</returns>
			/// <param name="selectedIndex">The zero-based index from the <see cref="T:System.Windows.Forms.ListView.ListViewItemCollection" /> to locate in the collection. </param>
			// Token: 0x06000AB6 RID: 2742 RVA: 0x0002E3E2 File Offset: 0x0002C5E2
			public int IndexOf(int selectedIndex)
			{
				if (!this.owner.is_selection_available)
				{
					return -1;
				}
				return this.List.IndexOf(selectedIndex);
			}

			// Token: 0x170002B2 RID: 690
			// (get) Token: 0x06000AB7 RID: 2743 RVA: 0x0002E404 File Offset: 0x0002C604
			internal ArrayList List
			{
				get
				{
					if (this.list == null)
					{
						this.list = new ArrayList();
						if (!this.owner.VirtualMode)
						{
							for (int i = 0; i < this.owner.Items.Count; i++)
							{
								if (this.owner.Items[i].Selected)
								{
									this.list.Add(i);
								}
							}
						}
					}
					return this.list;
				}
			}

			// Token: 0x06000AB8 RID: 2744 RVA: 0x0002E47C File Offset: 0x0002C67C
			internal void Reset()
			{
				this.list = null;
			}

			// Token: 0x06000AB9 RID: 2745 RVA: 0x0002E485 File Offset: 0x0002C685
			private void ItemsCollection_Changed()
			{
				this.Reset();
			}

			// Token: 0x06000ABA RID: 2746 RVA: 0x0002E490 File Offset: 0x0002C690
			internal void RemoveIndex(int index)
			{
				int num = this.List.BinarySearch(index);
				if (num != -1)
				{
					this.List.RemoveAt(num);
				}
			}

			// Token: 0x06000ABB RID: 2747 RVA: 0x0002E4C0 File Offset: 0x0002C6C0
			internal void InsertIndex(int index)
			{
				int i = 0;
				int num = this.List.Count - 1;
				while (i <= num)
				{
					int num2 = (i + num) / 2;
					int num3 = (int)this.List[num2];
					if (num3 == index)
					{
						return;
					}
					if (num3 > index)
					{
						num = num2 - 1;
					}
					else
					{
						i = num2 + 1;
					}
				}
				this.List.Insert(i, index);
			}

			// Token: 0x04000724 RID: 1828
			private readonly ListView owner;

			// Token: 0x04000725 RID: 1829
			private ArrayList list;
		}

		/// <summary>Represents the collection of selected items in a list view control.</summary>
		// Token: 0x02000112 RID: 274
		[ListBindable(false)]
		public class SelectedListViewItemCollection : IList, ICollection, IEnumerable
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListView.SelectedListViewItemCollection" /> class.</summary>
			/// <param name="owner">The <see cref="T:System.Windows.Forms.ListView" /> control that owns the collection. </param>
			// Token: 0x06000ABC RID: 2748 RVA: 0x0002E520 File Offset: 0x0002C720
			public SelectedListViewItemCollection(ListView owner)
			{
				this.owner = owner;
			}

			/// <summary>Gets the number of items in the collection.</summary>
			/// <returns>The number of items in the collection.</returns>
			// Token: 0x170002B3 RID: 691
			// (get) Token: 0x06000ABD RID: 2749 RVA: 0x0002E52F File Offset: 0x0002C72F
			[Browsable(false)]
			public int Count
			{
				get
				{
					return this.owner.SelectedIndices.Count;
				}
			}

			/// <summary>Gets a value indicating whether the collection is read-only.</summary>
			/// <returns>true if the collection is read-only; otherwise, false.</returns>
			// Token: 0x170002B4 RID: 692
			// (get) Token: 0x06000ABE RID: 2750 RVA: 0x00006F54 File Offset: 0x00005154
			public bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			/// <summary>Gets the item at the specified index within the collection.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.ListViewItem" /> representing the item located at the specified index within the collection.</returns>
			/// <param name="index">The index of the item in the collection to retrieve. </param>
			/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is less than zero or greater than or equal to the value of the <see cref="P:System.Windows.Forms.ListView.ListViewItemCollection.Count" /> property of the <see cref="T:System.Windows.Forms.ListView.SelectedListViewItemCollection" />. </exception>
			// Token: 0x170002B5 RID: 693
			public ListViewItem this[int index]
			{
				get
				{
					if (!this.owner.is_selection_available || index < 0 || index >= this.Count)
					{
						throw new ArgumentOutOfRangeException("index");
					}
					int num = this.owner.SelectedIndices[index];
					return this.owner.Items[num];
				}
			}

			/// <summary>Gets a value indicating whether access to the collection is synchronized (thread safe).</summary>
			/// <returns>false in all cases.</returns>
			// Token: 0x170002B6 RID: 694
			// (get) Token: 0x06000AC0 RID: 2752 RVA: 0x00002D70 File Offset: 0x00000F70
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			/// <summary>Gets an object that can be used to synchronize access to the collection of controls.</summary>
			/// <returns>The object used to synchronize the collection.</returns>
			// Token: 0x170002B7 RID: 695
			// (get) Token: 0x06000AC1 RID: 2753 RVA: 0x00002F7A File Offset: 0x0000117A
			object ICollection.SyncRoot
			{
				get
				{
					return this;
				}
			}

			/// <summary>Gets a value indicating whether the collection has a fixed size.</summary>
			/// <returns>true in all cases.</returns>
			// Token: 0x170002B8 RID: 696
			// (get) Token: 0x06000AC2 RID: 2754 RVA: 0x00006F54 File Offset: 0x00005154
			bool IList.IsFixedSize
			{
				get
				{
					return true;
				}
			}

			/// <summary>Gets or sets an an object from the collection.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.ListViewItem" /> that represents the item located at the specified index within the collection.</returns>
			/// <param name="index">The zero-based index of the element to get.</param>
			// Token: 0x170002B9 RID: 697
			object IList.this[int index]
			{
				get
				{
					return this[index];
				}
				set
				{
					throw new NotSupportedException("SetItem operation is not supported.");
				}
			}

			/// <summary>Removes all items from the collection.</summary>
			// Token: 0x06000AC5 RID: 2757 RVA: 0x0002E5A2 File Offset: 0x0002C7A2
			public void Clear()
			{
				this.owner.SelectedIndices.Clear();
			}

			/// <summary>Determines whether the specified item is located in the collection.</summary>
			/// <returns>true if the specified item is located in the collection; otherwise, false.</returns>
			/// <param name="item">A <see cref="T:System.Windows.Forms.ListViewItem" /> representing the item to locate in the collection. </param>
			// Token: 0x06000AC6 RID: 2758 RVA: 0x0002E5B4 File Offset: 0x0002C7B4
			public bool Contains(ListViewItem item)
			{
				return this.IndexOf(item) != -1;
			}

			/// <summary>Copies the entire collection into an existing array at a specified location within the array.</summary>
			/// <param name="dest">An <see cref="T:System.Array" /> representing the array to copy the contents of the collection to. </param>
			/// <param name="index">The location within the destination array to copy the items from the collection to. </param>
			// Token: 0x06000AC7 RID: 2759 RVA: 0x0002E5C4 File Offset: 0x0002C7C4
			public void CopyTo(Array dest, int index)
			{
				if (!this.owner.is_selection_available)
				{
					return;
				}
				if (index > this.Count)
				{
					throw new ArgumentException("index");
				}
				for (int i = 0; i < this.Count; i++)
				{
					dest.SetValue(this[i], index++);
				}
			}

			/// <summary>Returns an enumerator that can be used to iterate through the selected item collection.</summary>
			/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that represents the collection of selected items.</returns>
			// Token: 0x06000AC8 RID: 2760 RVA: 0x0002E618 File Offset: 0x0002C818
			public IEnumerator GetEnumerator()
			{
				if (!this.owner.is_selection_available)
				{
					return new ListViewItem[0].GetEnumerator();
				}
				ListViewItem[] array = new ListViewItem[this.Count];
				for (int i = 0; i < this.Count; i++)
				{
					array[i] = this[i];
				}
				return array.GetEnumerator();
			}

			/// <summary>Adds an item to the collection.</summary>
			/// <returns>The location of the added item.</returns>
			/// <param name="value">An object to be added to the <see cref="T:System.Windows.Forms.ListView.SelectedListViewItemCollection" />.</param>
			/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
			// Token: 0x06000AC9 RID: 2761 RVA: 0x0002D0CC File Offset: 0x0002B2CC
			int IList.Add(object value)
			{
				throw new NotSupportedException("Add operation is not supported.");
			}

			/// <summary>Determines whether the specified item is located in the collection.</summary>
			/// <returns>true if the specified item is located in the collection; otherwise, false.</returns>
			/// <param name="item">An object that represents the item to locate in the collection.</param>
			// Token: 0x06000ACA RID: 2762 RVA: 0x0002E66B File Offset: 0x0002C86B
			bool IList.Contains(object item)
			{
				return item is ListViewItem && this.Contains((ListViewItem)item);
			}

			/// <summary>Returns the index, within the collection, of the specified item.</summary>
			/// <returns>The zero-based index of the item if it is in the collection; otherwise, -1</returns>
			/// <param name="item">An object that represents the item to locate in the collection.</param>
			// Token: 0x06000ACB RID: 2763 RVA: 0x0002E683 File Offset: 0x0002C883
			int IList.IndexOf(object item)
			{
				if (!(item is ListViewItem))
				{
					return -1;
				}
				return this.IndexOf((ListViewItem)item);
			}

			/// <summary>Inserts an item into the collection at a specified index.</summary>
			/// <param name="index">The zero-based index of the item to be inserted.</param>
			/// <param name="value">An object to be added to the <see cref="T:System.Windows.Forms.ListView.SelectedListViewItemCollection" />.</param>
			/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
			// Token: 0x06000ACC RID: 2764 RVA: 0x0002D114 File Offset: 0x0002B314
			void IList.Insert(int index, object value)
			{
				throw new NotSupportedException("Insert operation is not supported.");
			}

			/// <summary>Removes the first occurrence of a specified item from the collection.</summary>
			/// <param name="value">The object to remove from the <see cref="T:System.Windows.Forms.ListView.SelectedListViewItemCollection" />.</param>
			/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
			// Token: 0x06000ACD RID: 2765 RVA: 0x0002D120 File Offset: 0x0002B320
			void IList.Remove(object value)
			{
				throw new NotSupportedException("Remove operation is not supported.");
			}

			/// <summary>Removes an item from the collection at a specified index.</summary>
			/// <param name="index">The zero-based index of the item to remove.</param>
			/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
			// Token: 0x06000ACE RID: 2766 RVA: 0x0002D12C File Offset: 0x0002B32C
			void IList.RemoveAt(int index)
			{
				throw new NotSupportedException("RemoveAt operation is not supported.");
			}

			/// <summary>Returns the index within the collection of the specified item.</summary>
			/// <returns>The zero-based index of the item in the collection. If the item is not located in the collection, the return value is negative one (-1).</returns>
			/// <param name="item">A <see cref="T:System.Windows.Forms.ListViewItem" /> representing the item to locate in the collection. </param>
			// Token: 0x06000ACF RID: 2767 RVA: 0x0002E69C File Offset: 0x0002C89C
			public int IndexOf(ListViewItem item)
			{
				if (!this.owner.is_selection_available)
				{
					return -1;
				}
				for (int i = 0; i < this.Count; i++)
				{
					if (this[i] == item)
					{
						return i;
					}
				}
				return -1;
			}

			// Token: 0x04000726 RID: 1830
			private readonly ListView owner;
		}

		// Token: 0x02000113 RID: 275
		// (Invoke) Token: 0x06000AD1 RID: 2769
		internal delegate void CollectionChangedHandler();

		// Token: 0x02000114 RID: 276
		private struct ItemMatrixLocation
		{
			// Token: 0x06000AD2 RID: 2770 RVA: 0x0002E6D6 File Offset: 0x0002C8D6
			public ItemMatrixLocation(int row, int col)
			{
				this.row = row;
				this.col = col;
			}

			// Token: 0x170002BA RID: 698
			// (get) Token: 0x06000AD3 RID: 2771 RVA: 0x0002E6E6 File Offset: 0x0002C8E6
			public int Col
			{
				get
				{
					return this.col;
				}
			}

			// Token: 0x170002BB RID: 699
			// (get) Token: 0x06000AD4 RID: 2772 RVA: 0x0002E6EE File Offset: 0x0002C8EE
			public int Row
			{
				get
				{
					return this.row;
				}
			}

			// Token: 0x04000727 RID: 1831
			private int row;

			// Token: 0x04000728 RID: 1832
			private int col;
		}
	}
}
