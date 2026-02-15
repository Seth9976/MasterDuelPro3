using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

namespace System.Windows.Forms
{
	/// <summary>Represents the base functionality for all menus. Although <see cref="T:System.Windows.Forms.ToolStripDropDown" /> and <see cref="T:System.Windows.Forms.ToolStripDropDownMenu" /> replace and add functionality to the <see cref="T:System.Windows.Forms.Menu" /> control of previous versions, <see cref="T:System.Windows.Forms.Menu" /> is retained for both backward compatibility and future use if you choose.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000130 RID: 304
	[ToolboxItemFilter("System.Windows.Forms", ToolboxItemFilterType.Allow)]
	[ListBindable(false)]
	public abstract class Menu : Component
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Menu" /> class.</summary>
		/// <param name="items">An array of type <see cref="T:System.Windows.Forms.MenuItem" /> containing the objects to add to the menu.</param>
		// Token: 0x06000BF3 RID: 3059 RVA: 0x00033CDF File Offset: 0x00031EDF
		protected Menu(MenuItem[] items)
		{
			this.menu_items = new Menu.MenuItemCollection(this);
			if (items != null)
			{
				this.menu_items.AddRange(items);
			}
		}

		// Token: 0x06000BF4 RID: 3060 RVA: 0x00033D10 File Offset: 0x00031F10
		internal virtual void OnMenuChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Menu.MenuChangedEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Gets a value indicating the collection of <see cref="T:System.Windows.Forms.MenuItem" /> objects associated with the menu.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Menu.MenuItemCollection" /> that represents the list of <see cref="T:System.Windows.Forms.MenuItem" /> objects stored in the menu.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000306 RID: 774
		// (get) Token: 0x06000BF5 RID: 3061 RVA: 0x00033D3E File Offset: 0x00031F3E
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[MergableProperty(false)]
		public Menu.MenuItemCollection MenuItems
		{
			get
			{
				return this.menu_items;
			}
		}

		/// <summary>Gets or sets the name of the <see cref="T:System.Windows.Forms.Menu" />.</summary>
		/// <returns>A string representing the name.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06000BF6 RID: 3062 RVA: 0x00033D46 File Offset: 0x00031F46
		// (set) Token: 0x06000BF7 RID: 3063 RVA: 0x00033D4E File Offset: 0x00031F4E
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string Name
		{
			get
			{
				return this.control_name;
			}
			set
			{
				this.control_name = value;
			}
		}

		/// <summary>Gets or sets user-defined data associated with the control.</summary>
		/// <returns>An object representing the data.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000308 RID: 776
		// (get) Token: 0x06000BF8 RID: 3064 RVA: 0x00033D57 File Offset: 0x00031F57
		// (set) Token: 0x06000BF9 RID: 3065 RVA: 0x00033D5F File Offset: 0x00031F5F
		[Localizable(false)]
		[Bindable(true)]
		[TypeConverter(typeof(StringConverter))]
		[DefaultValue(null)]
		[MWFCategory("Data")]
		public object Tag
		{
			get
			{
				return this.control_tag;
			}
			set
			{
				this.control_tag = value;
			}
		}

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x06000BFA RID: 3066 RVA: 0x00033D68 File Offset: 0x00031F68
		internal Rectangle Rect
		{
			get
			{
				return this.rect;
			}
		}

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x06000BFB RID: 3067 RVA: 0x00033D70 File Offset: 0x00031F70
		internal MenuItem SelectedItem
		{
			get
			{
				foreach (object obj in this.MenuItems)
				{
					MenuItem menuItem = (MenuItem)obj;
					if (menuItem.Selected)
					{
						return menuItem;
					}
				}
				return null;
			}
		}

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x06000BFC RID: 3068 RVA: 0x00033DD4 File Offset: 0x00031FD4
		// (set) Token: 0x06000BFD RID: 3069 RVA: 0x00033DE1 File Offset: 0x00031FE1
		internal int Height
		{
			get
			{
				return this.rect.Height;
			}
			set
			{
				this.rect.Height = value;
			}
		}

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x06000BFE RID: 3070 RVA: 0x00033DEF File Offset: 0x00031FEF
		// (set) Token: 0x06000BFF RID: 3071 RVA: 0x00033DFC File Offset: 0x00031FFC
		internal int Width
		{
			get
			{
				return this.rect.Width;
			}
			set
			{
				this.rect.Width = value;
			}
		}

		// Token: 0x1700030D RID: 781
		// (set) Token: 0x06000C00 RID: 3072 RVA: 0x00033E0A File Offset: 0x0003200A
		internal int X
		{
			set
			{
				this.rect.X = value;
			}
		}

		// Token: 0x1700030E RID: 782
		// (set) Token: 0x06000C01 RID: 3073 RVA: 0x00033E18 File Offset: 0x00032018
		internal int Y
		{
			set
			{
				this.rect.Y = value;
			}
		}

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x06000C02 RID: 3074 RVA: 0x00033E28 File Offset: 0x00032028
		internal MenuTracker Tracker
		{
			get
			{
				Menu menu = this;
				while (menu.parent_menu != null)
				{
					menu = menu.parent_menu;
				}
				return menu.tracker;
			}
		}

		/// <summary>Copies the <see cref="T:System.Windows.Forms.Menu" /> that is passed as a parameter to the current <see cref="T:System.Windows.Forms.Menu" />.</summary>
		/// <param name="menuSrc">The <see cref="T:System.Windows.Forms.Menu" /> to copy. </param>
		// Token: 0x06000C03 RID: 3075 RVA: 0x00033E50 File Offset: 0x00032050
		protected void CloneMenu(Menu menuSrc)
		{
			this.Dispose(true);
			this.menu_items = new Menu.MenuItemCollection(this);
			for (int i = 0; i < menuSrc.MenuItems.Count; i++)
			{
				this.menu_items.Add(menuSrc.MenuItems[i].CloneMenu());
			}
		}

		/// <summary>Disposes of the resources, other than memory, used by the <see cref="T:System.Windows.Forms.Menu" />.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x06000C04 RID: 3076 RVA: 0x00033EA4 File Offset: 0x000320A4
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.menu_items != null)
				{
					while (this.menu_items.Count > 0)
					{
						this.menu_items[0].Dispose();
					}
				}
				if (this.menu_handle != IntPtr.Zero)
				{
					this.menu_handle = IntPtr.Zero;
				}
			}
		}

		/// <summary>Returns the position at which a menu item should be inserted into the menu.</summary>
		/// <returns>The position at which a menu item should be inserted into the menu.</returns>
		/// <param name="mergeOrder">The merge order position for the menu item to be merged.</param>
		// Token: 0x06000C05 RID: 3077 RVA: 0x00033EFC File Offset: 0x000320FC
		protected int FindMergePosition(int mergeOrder)
		{
			int num = this.MenuItems.Count;
			int i = 0;
			while (i < num)
			{
				int num2 = (i + num) / 2;
				if (this.MenuItems[num2].MergeOrder > mergeOrder)
				{
					num = num2;
				}
				else
				{
					i = num2 + 1;
				}
			}
			return i;
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.MainMenu" /> that contains this menu.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.MainMenu" /> that contains this menu.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C06 RID: 3078 RVA: 0x00033F40 File Offset: 0x00032140
		public MainMenu GetMainMenu()
		{
			for (Menu menu = this; menu != null; menu = menu.parent_menu)
			{
				if (menu is MainMenu)
				{
					return (MainMenu)menu;
				}
			}
			return null;
		}

		// Token: 0x06000C07 RID: 3079 RVA: 0x00033F6B File Offset: 0x0003216B
		internal virtual void InvalidateItem(MenuItem item)
		{
			if (this.Wnd != null)
			{
				this.Wnd.Invalidate(item.bounds);
			}
		}

		/// <summary>Merges the <see cref="T:System.Windows.Forms.MenuItem" /> objects of one menu with the current menu.</summary>
		/// <param name="menuSrc">The <see cref="T:System.Windows.Forms.Menu" /> whose menu items are merged with the menu items of the current menu. </param>
		/// <exception cref="T:System.ArgumentException">It was attempted to merge the menu with itself. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000C08 RID: 3080 RVA: 0x00033F88 File Offset: 0x00032188
		public virtual void MergeMenu(Menu menuSrc)
		{
			if (menuSrc == this)
			{
				throw new ArgumentException("The menu cannot be merged with itself");
			}
			if (menuSrc == null)
			{
				return;
			}
			for (int i = 0; i < menuSrc.MenuItems.Count; i++)
			{
				MenuItem menuItem = menuSrc.MenuItems[i];
				switch (menuItem.MergeType)
				{
				case MenuMerge.Add:
				{
					int num = this.FindMergePosition(menuItem.MergeOrder);
					this.MenuItems.Add(num, menuItem.CloneMenu());
					break;
				}
				case MenuMerge.Replace:
				case MenuMerge.MergeItems:
				{
					int j = this.FindMergePosition(menuItem.MergeOrder - 1);
					while (j <= this.MenuItems.Count)
					{
						if (j >= this.MenuItems.Count || this.MenuItems[j].MergeOrder != menuItem.MergeOrder)
						{
							this.MenuItems.Add(j, menuItem.CloneMenu());
							break;
						}
						MenuItem menuItem2 = this.MenuItems[j];
						if (menuItem2.MergeType != MenuMerge.Add)
						{
							if (menuItem.MergeType == MenuMerge.MergeItems && menuItem2.MergeType == MenuMerge.MergeItems)
							{
								menuItem2.MergeMenu(menuItem);
								break;
							}
							this.MenuItems.Remove(menuItem);
							this.MenuItems.Add(j, menuItem.CloneMenu());
							break;
						}
						else
						{
							j++;
						}
					}
					break;
				}
				}
			}
		}

		/// <summary>Processes a command key.</summary>
		/// <returns>true if the character was processed by the control; otherwise, false.</returns>
		/// <param name="msg">A <see cref="T:System.Windows.Forms.Message" />, passed by reference that represents the window message to process.</param>
		/// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values that represents the key to process.</param>
		// Token: 0x06000C09 RID: 3081 RVA: 0x000340DB File Offset: 0x000322DB
		protected internal virtual bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			return this.tracker != null && this.tracker.ProcessKeys(ref msg, keyData);
		}

		/// <summary>Returns a <see cref="T:System.String" /> that represents the <see cref="T:System.Windows.Forms.Menu" /> control.</summary>
		/// <returns>A <see cref="T:System.String" /> that represents the current <see cref="T:System.Windows.Forms.Menu" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000C0A RID: 3082 RVA: 0x000340F4 File Offset: 0x000322F4
		public override string ToString()
		{
			return base.ToString() + ", Items.Count: " + this.MenuItems.Count;
		}

		// Token: 0x1400003A RID: 58
		// (add) Token: 0x06000C0B RID: 3083 RVA: 0x00034116 File Offset: 0x00032316
		// (remove) Token: 0x06000C0C RID: 3084 RVA: 0x00034129 File Offset: 0x00032329
		internal event EventHandler MenuChanged
		{
			add
			{
				base.Events.AddHandler(Menu.MenuChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Menu.MenuChangedEvent, value);
			}
		}

		// Token: 0x06000C0D RID: 3085 RVA: 0x0003413C File Offset: 0x0003233C
		// Note: this type is marked as 'beforefieldinit'.
		static Menu()
		{
			Menu.MenuChangedEvent = new object();
		}

		// Token: 0x040007A3 RID: 1955
		internal Menu.MenuItemCollection menu_items;

		// Token: 0x040007A4 RID: 1956
		internal IntPtr menu_handle = IntPtr.Zero;

		// Token: 0x040007A5 RID: 1957
		internal Menu parent_menu;

		// Token: 0x040007A6 RID: 1958
		private Rectangle rect;

		// Token: 0x040007A7 RID: 1959
		internal Control Wnd;

		// Token: 0x040007A8 RID: 1960
		internal MenuTracker tracker;

		// Token: 0x040007A9 RID: 1961
		private string control_name;

		// Token: 0x040007AA RID: 1962
		private object control_tag;

		/// <summary>Represents a collection of <see cref="T:System.Windows.Forms.MenuItem" /> objects.</summary>
		// Token: 0x02000131 RID: 305
		[ListBindable(false)]
		public class MenuItemCollection : IList, ICollection, IEnumerable
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Menu.MenuItemCollection" /> class.</summary>
			/// <param name="owner">The <see cref="T:System.Windows.Forms.Menu" /> that owns this collection. </param>
			// Token: 0x06000C0E RID: 3086 RVA: 0x00034148 File Offset: 0x00032348
			public MenuItemCollection(Menu owner)
			{
				this.owner = owner;
			}

			/// <summary>Gets a value indicating the total number of <see cref="T:System.Windows.Forms.MenuItem" /> objects in the collection.</summary>
			/// <returns>The number of <see cref="T:System.Windows.Forms.MenuItem" /> objects in the collection.</returns>
			// Token: 0x17000310 RID: 784
			// (get) Token: 0x06000C0F RID: 3087 RVA: 0x00034162 File Offset: 0x00032362
			public int Count
			{
				get
				{
					return this.items.Count;
				}
			}

			/// <summary>Gets a value indicating whether the collection is read-only.</summary>
			/// <returns>true if the collection is read-only; otherwise, false. The default is false.</returns>
			// Token: 0x17000311 RID: 785
			// (get) Token: 0x06000C10 RID: 3088 RVA: 0x00002D70 File Offset: 0x00000F70
			public bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			/// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.IsSynchronized" />.</summary>
			/// <returns>false in all cases.</returns>
			// Token: 0x17000312 RID: 786
			// (get) Token: 0x06000C11 RID: 3089 RVA: 0x00002D70 File Offset: 0x00000F70
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			/// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.SyncRoot" />.</summary>
			/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Windows.Forms.Menu.MenuItemCollection" />.</returns>
			// Token: 0x17000313 RID: 787
			// (get) Token: 0x06000C12 RID: 3090 RVA: 0x00002F7A File Offset: 0x0000117A
			object ICollection.SyncRoot
			{
				get
				{
					return this;
				}
			}

			/// <summary>For a description of this member, see <see cref="P:System.Collections.IList.IsFixedSize" />.</summary>
			/// <returns>false in all cases.</returns>
			// Token: 0x17000314 RID: 788
			// (get) Token: 0x06000C13 RID: 3091 RVA: 0x00002D70 File Offset: 0x00000F70
			bool IList.IsFixedSize
			{
				get
				{
					return false;
				}
			}

			/// <summary>Retrieves the <see cref="T:System.Windows.Forms.MenuItem" /> at the specified indexed location in the collection.</summary>
			/// <returns>The <see cref="T:System.Windows.Forms.MenuItem" /> at the specified location.</returns>
			/// <param name="index">The indexed location of the <see cref="T:System.Windows.Forms.MenuItem" /> in the collection. </param>
			/// <exception cref="T:System.ArgumentException">The <paramref name="value" /> parameter is null.or The <paramref name="index" /> parameter is less than zero.or The <paramref name="index" /> parameter is greater than the number of menu items in the collection, and the collection of menu items is not null. </exception>
			// Token: 0x17000315 RID: 789
			public virtual MenuItem this[int index]
			{
				get
				{
					if (index < 0 || index >= this.Count)
					{
						throw new ArgumentOutOfRangeException("Index of out range");
					}
					return (MenuItem)this.items[index];
				}
			}

			/// <summary>For a description of this member, see <see cref="P:System.Collections.IList.Item(System.Int32)" />.</summary>
			/// <returns>The <see cref="T:System.Windows.Forms.MenuItem" /> at the specified index.</returns>
			/// <param name="index">The zero-based index of the element to get.</param>
			// Token: 0x17000316 RID: 790
			object IList.this[int index]
			{
				get
				{
					return this.items[index];
				}
				set
				{
					throw new NotSupportedException();
				}
			}

			/// <summary>Adds a previously created <see cref="T:System.Windows.Forms.MenuItem" /> to the end of the current menu.</summary>
			/// <returns>The zero-based index where the item is stored in the collection.</returns>
			/// <param name="item">The <see cref="T:System.Windows.Forms.MenuItem" /> to add. </param>
			// Token: 0x06000C17 RID: 3095 RVA: 0x000341A8 File Offset: 0x000323A8
			public virtual int Add(MenuItem item)
			{
				if (item.Parent != null)
				{
					item.Parent.MenuItems.Remove(item);
				}
				this.items.Add(item);
				item.Index = this.items.Count - 1;
				this.UpdateItem(item);
				this.owner.OnMenuChanged(EventArgs.Empty);
				if (this.owner.parent_menu != null)
				{
					this.owner.parent_menu.OnMenuChanged(EventArgs.Empty);
				}
				return this.items.Count - 1;
			}

			// Token: 0x06000C18 RID: 3096 RVA: 0x00034234 File Offset: 0x00032434
			internal void AddNoEvents(MenuItem mi)
			{
				if (mi.Parent != null)
				{
					mi.Parent.MenuItems.Remove(mi);
				}
				this.items.Add(mi);
				mi.Index = this.items.Count - 1;
				mi.parent_menu = this.owner;
			}

			/// <summary>Adds a previously created <see cref="T:System.Windows.Forms.MenuItem" /> at the specified index within the menu item collection.</summary>
			/// <returns>The zero-based index where the item is stored in the collection.</returns>
			/// <param name="index">The position to add the new item. </param>
			/// <param name="item">The <see cref="T:System.Windows.Forms.MenuItem" /> to add. </param>
			/// <exception cref="T:System.Exception">The <see cref="T:System.Windows.Forms.MenuItem" /> being added is already in use. </exception>
			/// <exception cref="T:System.ArgumentException">The index supplied in the <paramref name="index" /> parameter is larger than the size of the collection. </exception>
			// Token: 0x06000C19 RID: 3097 RVA: 0x00034288 File Offset: 0x00032488
			public virtual int Add(int index, MenuItem item)
			{
				if (index < 0 || index > this.Count)
				{
					throw new ArgumentOutOfRangeException("Index of out range");
				}
				ArrayList arrayList = new ArrayList(this.Count + 1);
				for (int i = 0; i < index; i++)
				{
					arrayList.Add(this.items[i]);
				}
				arrayList.Add(item);
				for (int j = index; j < this.Count; j++)
				{
					arrayList.Add(this.items[j]);
				}
				this.items = arrayList;
				this.UpdateItemsIndices();
				this.UpdateItem(item);
				return index;
			}

			// Token: 0x06000C1A RID: 3098 RVA: 0x0003431C File Offset: 0x0003251C
			private void UpdateItem(MenuItem mi)
			{
				mi.parent_menu = this.owner;
				this.owner.OnMenuChanged(EventArgs.Empty);
				if (this.owner.parent_menu != null)
				{
					this.owner.parent_menu.OnMenuChanged(EventArgs.Empty);
				}
				if (this.owner.Tracker != null)
				{
					this.owner.Tracker.AddShortcuts(mi);
				}
			}

			// Token: 0x06000C1B RID: 3099 RVA: 0x00034385 File Offset: 0x00032585
			internal void Insert(int index, MenuItem mi)
			{
				if (index < 0 || index > this.Count)
				{
					throw new ArgumentOutOfRangeException("Index of out range");
				}
				this.items.Insert(index, mi);
				this.UpdateItemsIndices();
				this.UpdateItem(mi);
			}

			/// <summary>Adds an array of previously created <see cref="T:System.Windows.Forms.MenuItem" /> objects to the collection.</summary>
			/// <param name="items">An array of <see cref="T:System.Windows.Forms.MenuItem" /> objects representing the menu items to add to the collection. </param>
			// Token: 0x06000C1C RID: 3100 RVA: 0x000343BC File Offset: 0x000325BC
			public virtual void AddRange(MenuItem[] items)
			{
				if (items == null)
				{
					throw new ArgumentNullException("items");
				}
				foreach (MenuItem menuItem in items)
				{
					this.Add(menuItem);
				}
			}

			/// <summary>Removes all <see cref="T:System.Windows.Forms.MenuItem" /> objects from the menu item collection.</summary>
			// Token: 0x06000C1D RID: 3101 RVA: 0x000343F4 File Offset: 0x000325F4
			public virtual void Clear()
			{
				MenuTracker tracker = this.owner.Tracker;
				foreach (object obj in this.items)
				{
					MenuItem menuItem = (MenuItem)obj;
					if (tracker != null)
					{
						tracker.RemoveShortcuts(menuItem);
					}
					menuItem.parent_menu = null;
				}
				this.items.Clear();
				this.owner.OnMenuChanged(EventArgs.Empty);
			}

			/// <summary>Determines if the specified <see cref="T:System.Windows.Forms.MenuItem" /> is a member of the collection.</summary>
			/// <returns>true if the <see cref="T:System.Windows.Forms.MenuItem" /> is a member of the collection; otherwise, false.</returns>
			/// <param name="value">The <see cref="T:System.Windows.Forms.MenuItem" /> to locate in the collection. </param>
			// Token: 0x06000C1E RID: 3102 RVA: 0x00034480 File Offset: 0x00032680
			public bool Contains(MenuItem value)
			{
				return this.items.Contains(value);
			}

			/// <summary>Copies the entire collection into an existing array at a specified location within the array.</summary>
			/// <param name="dest">The destination array. </param>
			/// <param name="index">The index in the destination array at which storing begins. </param>
			// Token: 0x06000C1F RID: 3103 RVA: 0x0003448E File Offset: 0x0003268E
			public void CopyTo(Array dest, int index)
			{
				this.items.CopyTo(dest, index);
			}

			/// <summary>Returns an enumerator that can be used to iterate through the menu item collection.</summary>
			/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that represents the menu item collection.</returns>
			// Token: 0x06000C20 RID: 3104 RVA: 0x0003449D File Offset: 0x0003269D
			public IEnumerator GetEnumerator()
			{
				return this.items.GetEnumerator();
			}

			/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Add(System.Object)" />.</summary>
			/// <returns>The position into which the <see cref="T:System.Windows.Forms.MenuItem" /> was inserted.</returns>
			/// <param name="value">The <see cref="T:System.Windows.Forms.MenuItem" /> to add to the collection.</param>
			// Token: 0x06000C21 RID: 3105 RVA: 0x000344AA File Offset: 0x000326AA
			int IList.Add(object value)
			{
				return this.Add((MenuItem)value);
			}

			/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Contains(System.Object)" />.</summary>
			/// <returns>true if the specified object is a <see cref="T:System.Windows.Forms.MenuItem" /> in the collection; otherwise, false.</returns>
			/// <param name="value">The object to locate in the collection.</param>
			// Token: 0x06000C22 RID: 3106 RVA: 0x000344B8 File Offset: 0x000326B8
			bool IList.Contains(object value)
			{
				return this.Contains((MenuItem)value);
			}

			/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.IndexOf(System.Object)" />.</summary>
			/// <returns>The zero-based index if <paramref name="value" /> is a <see cref="T:System.Windows.Forms.MenuItem" /> in the collection; otherwise -1.</returns>
			/// <param name="value">The <see cref="T:System.Windows.Forms.MenuItem" /> to locate in the collection.</param>
			// Token: 0x06000C23 RID: 3107 RVA: 0x000344C6 File Offset: 0x000326C6
			int IList.IndexOf(object value)
			{
				return this.IndexOf((MenuItem)value);
			}

			/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Insert(System.Int32,System.Object)" />.</summary>
			/// <param name="index">The zero-based index at which the <see cref="T:System.Windows.Forms.MenuItem" /> should be inserted.</param>
			/// <param name="value">The <see cref="T:System.Windows.Forms.MenuItem" /> to insert into the <see cref="T:System.Windows.Forms.Menu.MenuItemCollection" />.</param>
			// Token: 0x06000C24 RID: 3108 RVA: 0x000344D4 File Offset: 0x000326D4
			void IList.Insert(int index, object value)
			{
				this.Insert(index, (MenuItem)value);
			}

			/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Remove(System.Object)" />.</summary>
			/// <param name="value">The <see cref="T:System.Windows.Forms.MenuItem" /> to remove.</param>
			// Token: 0x06000C25 RID: 3109 RVA: 0x000344E3 File Offset: 0x000326E3
			void IList.Remove(object value)
			{
				this.Remove((MenuItem)value);
			}

			/// <summary>Retrieves the index of a specific item in the collection.</summary>
			/// <returns>The zero-based index of the item found in the collection; otherwise, -1.</returns>
			/// <param name="value">The <see cref="T:System.Windows.Forms.MenuItem" /> to locate in the collection. </param>
			// Token: 0x06000C26 RID: 3110 RVA: 0x000344F1 File Offset: 0x000326F1
			public int IndexOf(MenuItem value)
			{
				return this.items.IndexOf(value);
			}

			/// <summary>Removes the specified <see cref="T:System.Windows.Forms.MenuItem" /> from the menu item collection.</summary>
			/// <param name="item">The <see cref="T:System.Windows.Forms.MenuItem" /> to remove. </param>
			// Token: 0x06000C27 RID: 3111 RVA: 0x000344FF File Offset: 0x000326FF
			public virtual void Remove(MenuItem item)
			{
				this.RemoveAt(item.Index);
			}

			/// <summary>Removes a <see cref="T:System.Windows.Forms.MenuItem" /> from the menu item collection at a specified index.</summary>
			/// <param name="index">The index of the <see cref="T:System.Windows.Forms.MenuItem" /> to remove. </param>
			// Token: 0x06000C28 RID: 3112 RVA: 0x00034510 File Offset: 0x00032710
			public virtual void RemoveAt(int index)
			{
				if (index < 0 || index >= this.Count)
				{
					throw new ArgumentOutOfRangeException("Index of out range");
				}
				MenuItem menuItem = (MenuItem)this.items[index];
				MenuTracker tracker = this.owner.Tracker;
				if (tracker != null)
				{
					tracker.RemoveShortcuts(menuItem);
				}
				menuItem.parent_menu = null;
				this.items.RemoveAt(index);
				this.UpdateItemsIndices();
				this.owner.OnMenuChanged(EventArgs.Empty);
			}

			// Token: 0x06000C29 RID: 3113 RVA: 0x00034588 File Offset: 0x00032788
			private void UpdateItemsIndices()
			{
				for (int i = 0; i < this.Count; i++)
				{
					((MenuItem)this.items[i]).Index = i;
				}
			}

			// Token: 0x040007AC RID: 1964
			private Menu owner;

			// Token: 0x040007AD RID: 1965
			private ArrayList items = new ArrayList();
		}
	}
}
