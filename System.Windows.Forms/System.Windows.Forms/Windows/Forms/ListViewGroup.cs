using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.Serialization;

namespace System.Windows.Forms
{
	/// <summary>Represents a group of items displayed within a <see cref="T:System.Windows.Forms.ListView" /> control.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000116 RID: 278
	[ToolboxItem(false)]
	[DesignTimeVisible(false)]
	[DefaultProperty("Header")]
	[TypeConverter(typeof(ListViewGroupConverter))]
	[Serializable]
	public sealed class ListViewGroup : ISerializable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListViewGroup" /> class using the default header text of "ListViewGroup" and the default left header alignment.</summary>
		// Token: 0x06000AD5 RID: 2773 RVA: 0x0002E6F6 File Offset: 0x0002C8F6
		public ListViewGroup()
			: this("ListViewGroup", HorizontalAlignment.Left)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListViewGroup" /> class using the specified value to initialize the <see cref="P:System.Windows.Forms.ListViewGroup.Header" /> property and using the default left header alignment.</summary>
		/// <param name="header">The text to display for the group header. </param>
		// Token: 0x06000AD6 RID: 2774 RVA: 0x0002E704 File Offset: 0x0002C904
		public ListViewGroup(string header)
			: this(header, HorizontalAlignment.Left)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListViewGroup" /> class using the specified header text and the specified header alignment.</summary>
		/// <param name="header">The text to display for the group header. </param>
		/// <param name="headerAlignment">One of the <see cref="T:System.Windows.Forms.HorizontalAlignment" /> values that specifies the alignment of the header text. </param>
		// Token: 0x06000AD7 RID: 2775 RVA: 0x0002E70E File Offset: 0x0002C90E
		public ListViewGroup(string header, HorizontalAlignment headerAlignment)
		{
			this.header = string.Empty;
			this.header_bounds = Rectangle.Empty;
			base..ctor();
			this.header = header;
			this.header_alignment = headerAlignment;
			this.items = new ListView.ListViewItemCollection(this.list_view_owner, this);
		}

		// Token: 0x06000AD8 RID: 2776 RVA: 0x0002E74C File Offset: 0x0002C94C
		private ListViewGroup(SerializationInfo info, StreamingContext context)
		{
			this.header = string.Empty;
			this.header_bounds = Rectangle.Empty;
			base..ctor();
			this.header = info.GetString("Header");
			this.name = info.GetString("Name");
			this.header_alignment = (HorizontalAlignment)info.GetInt32("HeaderAlignment");
			this.tag = info.GetValue("Tag", typeof(object));
			int @int = info.GetInt32("ListViewItemCount");
			if (@int > 0)
			{
				if (this.items == null)
				{
					this.items = new ListView.ListViewItemCollection(this.list_view_owner);
				}
				for (int i = 0; i < @int; i++)
				{
					this.items.Add((ListViewItem)info.GetValue(string.Format("ListViewItem_{0}", i), typeof(ListViewItem)));
				}
			}
		}

		/// <summary>Gets or sets the header text for the group.</summary>
		/// <returns>The text to display for the group header. The default is "ListViewGroup".</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170002BC RID: 700
		// (get) Token: 0x06000AD9 RID: 2777 RVA: 0x0002E829 File Offset: 0x0002CA29
		public string Header
		{
			get
			{
				return this.header;
			}
		}

		/// <summary>Gets or sets the alignment of the group header text.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.HorizontalAlignment" /> values that specifies the alignment of the header text. The default is <see cref="F:System.Windows.Forms.HorizontalAlignment.Left" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value when setting this property is not a valid <see cref="T:System.Windows.Forms.HorizontalAlignment" /> value.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170002BD RID: 701
		// (get) Token: 0x06000ADA RID: 2778 RVA: 0x0002E831 File Offset: 0x0002CA31
		[DefaultValue(HorizontalAlignment.Left)]
		public HorizontalAlignment HeaderAlignment
		{
			get
			{
				return this.header_alignment;
			}
		}

		/// <summary>Gets a collection containing all items associated with this group.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ListView.ListViewItemCollection" /> that contains all the items in the group. If there are no items in the group, an empty <see cref="T:System.Windows.Forms.ListView.ListViewItemCollection" /> object is returned.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002BE RID: 702
		// (get) Token: 0x06000ADB RID: 2779 RVA: 0x0002E839 File Offset: 0x0002CA39
		[Browsable(false)]
		public ListView.ListViewItemCollection Items
		{
			get
			{
				return this.items;
			}
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.ListView" /> control that contains this group. </summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ListView" /> control that contains this group.</returns>
		// Token: 0x170002BF RID: 703
		// (get) Token: 0x06000ADC RID: 2780 RVA: 0x0002E841 File Offset: 0x0002CA41
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public ListView ListView
		{
			get
			{
				return this.list_view_owner;
			}
		}

		// Token: 0x170002C0 RID: 704
		// (set) Token: 0x06000ADD RID: 2781 RVA: 0x0002E849 File Offset: 0x0002CA49
		internal ListView ListViewOwner
		{
			set
			{
				this.list_view_owner = value;
				if (!this.is_default_group)
				{
					this.items.Owner = value;
				}
			}
		}

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x06000ADE RID: 2782 RVA: 0x0002E868 File Offset: 0x0002CA68
		// (set) Token: 0x06000ADF RID: 2783 RVA: 0x0002E8B0 File Offset: 0x0002CAB0
		internal Rectangle HeaderBounds
		{
			get
			{
				Rectangle rectangle = this.header_bounds;
				rectangle.X -= this.list_view_owner.h_marker;
				rectangle.Y -= this.list_view_owner.v_marker;
				return rectangle;
			}
			set
			{
				if (this.list_view_owner != null)
				{
					this.list_view_owner.item_control.Invalidate(this.HeaderBounds);
				}
				this.header_bounds = value;
				if (this.list_view_owner != null)
				{
					this.list_view_owner.item_control.Invalidate(this.HeaderBounds);
				}
			}
		}

		// Token: 0x170002C2 RID: 706
		// (set) Token: 0x06000AE0 RID: 2784 RVA: 0x0002E900 File Offset: 0x0002CB00
		internal bool IsDefault
		{
			set
			{
				this.is_default_group = value;
			}
		}

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x06000AE1 RID: 2785 RVA: 0x0002E909 File Offset: 0x0002CB09
		// (set) Token: 0x06000AE2 RID: 2786 RVA: 0x0002E925 File Offset: 0x0002CB25
		internal int ItemCount
		{
			get
			{
				if (!this.is_default_group)
				{
					return this.items.Count;
				}
				return this.item_count;
			}
			set
			{
				if (!this.is_default_group)
				{
					throw new InvalidOperationException("ItemCount cannot be set for non-default groups.");
				}
				this.item_count = value;
			}
		}

		// Token: 0x06000AE3 RID: 2787 RVA: 0x0002E944 File Offset: 0x0002CB44
		internal int GetActualItemCount()
		{
			if (this.is_default_group)
			{
				return this.item_count;
			}
			int num = 0;
			for (int i = 0; i < this.items.Count; i++)
			{
				if (this.items[i].ListView != null)
				{
					num++;
				}
			}
			return num;
		}

		/// <returns>A string that represents the current object.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000AE4 RID: 2788 RVA: 0x0002E829 File Offset: 0x0002CA29
		public override string ToString()
		{
			return this.header;
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data needed to serialize the target object.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data.</param>
		/// <param name="context">The destination (see <see cref="T:System.Runtime.Serialization.StreamingContext" />) for this serialization.</param>
		// Token: 0x06000AE5 RID: 2789 RVA: 0x0002E990 File Offset: 0x0002CB90
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("Header", this.header);
			info.AddValue("Name", this.name);
			info.AddValue("HeaderAlignment", this.header_alignment);
			info.AddValue("Tag", this.tag);
			info.AddValue("ListViewItemCount", this.items.Count);
			int num = 0;
			foreach (object obj in this.items)
			{
				ListViewItem listViewItem = (ListViewItem)obj;
				info.AddValue(string.Format("ListViewItem_{0}", num), listViewItem);
				num++;
			}
		}

		// Token: 0x0400072E RID: 1838
		internal string header;

		// Token: 0x0400072F RID: 1839
		private string name;

		// Token: 0x04000730 RID: 1840
		private HorizontalAlignment header_alignment;

		// Token: 0x04000731 RID: 1841
		private ListView list_view_owner;

		// Token: 0x04000732 RID: 1842
		private ListView.ListViewItemCollection items;

		// Token: 0x04000733 RID: 1843
		private object tag;

		// Token: 0x04000734 RID: 1844
		private Rectangle header_bounds;

		// Token: 0x04000735 RID: 1845
		internal int starting_row;

		// Token: 0x04000736 RID: 1846
		internal int starting_item;

		// Token: 0x04000737 RID: 1847
		internal int rows;

		// Token: 0x04000738 RID: 1848
		internal int current_item;

		// Token: 0x04000739 RID: 1849
		internal Point items_area_location;

		// Token: 0x0400073A RID: 1850
		private bool is_default_group;

		// Token: 0x0400073B RID: 1851
		private int item_count;
	}
}
