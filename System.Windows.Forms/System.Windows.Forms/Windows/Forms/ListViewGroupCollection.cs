using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace System.Windows.Forms
{
	/// <summary>Represents the collection of groups within a <see cref="T:System.Windows.Forms.ListView" /> control.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000118 RID: 280
	[ListBindable(false)]
	public class ListViewGroupCollection : IList, ICollection, IEnumerable
	{
		// Token: 0x06000AE9 RID: 2793 RVA: 0x0002EA6D File Offset: 0x0002CC6D
		private ListViewGroupCollection()
		{
			this.list = new List<ListViewGroup>();
			this.default_group = new ListViewGroup("Default Group");
			this.default_group.IsDefault = true;
		}

		// Token: 0x06000AEA RID: 2794 RVA: 0x0002EA9C File Offset: 0x0002CC9C
		internal ListViewGroupCollection(ListView listViewOwner)
			: this()
		{
			this.list_view_owner = listViewOwner;
			this.default_group.ListViewOwner = listViewOwner;
		}

		/// <summary>Returns an enumerator used to iterate through the collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that represents the collection.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000AEB RID: 2795 RVA: 0x0002EAB7 File Offset: 0x0002CCB7
		public IEnumerator GetEnumerator()
		{
			return this.list.GetEnumerator();
		}

		/// <summary>Copies the groups in the collection to a compatible one-dimensional <see cref="T:System.Array" />, starting at the specified index of the target array.</summary>
		/// <param name="array">The <see cref="T:System.Array" /> to which the groups are copied. </param>
		/// <param name="index">The first index within the array to which the groups are copied. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000AEC RID: 2796 RVA: 0x0002EAC9 File Offset: 0x0002CCC9
		public void CopyTo(Array array, int index)
		{
			((ICollection)this.list).CopyTo(array, index);
		}

		/// <summary>Gets the number of groups in the collection.</summary>
		/// <returns>The number of groups in the collection.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x06000AED RID: 2797 RVA: 0x0002EAD8 File Offset: 0x0002CCD8
		public int Count
		{
			get
			{
				return this.list.Count;
			}
		}

		/// <summary>Gets a value indicating whether access to the collection is synchronized (thread safe).</summary>
		/// <returns>true in all cases.</returns>
		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x06000AEE RID: 2798 RVA: 0x00006F54 File Offset: 0x00005154
		bool ICollection.IsSynchronized
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the collection.</summary>
		/// <returns>The object used to synchronize the collection.</returns>
		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x06000AEF RID: 2799 RVA: 0x00002F7A File Offset: 0x0000117A
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>Adds a new <see cref="T:System.Windows.Forms.ListViewGroup" /> to the <see cref="T:System.Windows.Forms.ListViewGroupCollection" />.</summary>
		/// <returns>The index at which the <see cref="T:System.Windows.Forms.ListViewGroup" /> has been added.</returns>
		/// <param name="value">The <see cref="T:System.Windows.Forms.ListViewGroup" /> to add to the <see cref="T:System.Windows.Forms.ListViewGroupCollection" />.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> is not a <see cref="T:System.Windows.Forms.ListViewGroup" />.-or-<paramref name="value" /> contains at least one <see cref="T:System.Windows.Forms.ListViewItem" /> that belongs to a <see cref="T:System.Windows.Forms.ListView" /> control other than the one that owns this <see cref="T:System.Windows.Forms.ListViewGroupCollection" />.</exception>
		// Token: 0x06000AF0 RID: 2800 RVA: 0x0002EAE5 File Offset: 0x0002CCE5
		int IList.Add(object value)
		{
			if (!(value is ListViewGroup))
			{
				throw new ArgumentException("value");
			}
			return this.Add((ListViewGroup)value);
		}

		/// <summary>Adds the specified <see cref="T:System.Windows.Forms.ListViewGroup" /> to the collection.</summary>
		/// <returns>The index of the group within the collection, or -1 if the group is already present in the collection.</returns>
		/// <param name="group">The <see cref="T:System.Windows.Forms.ListViewGroup" /> to add to the collection. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="group" /> contains at least one <see cref="T:System.Windows.Forms.ListViewItem" /> that belongs to a <see cref="T:System.Windows.Forms.ListView" /> control other than the one that owns this <see cref="T:System.Windows.Forms.ListViewGroupCollection" />.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000AF1 RID: 2801 RVA: 0x0002EB06 File Offset: 0x0002CD06
		public int Add(ListViewGroup group)
		{
			if (this.Contains(group))
			{
				return -1;
			}
			this.AddGroup(group);
			if (this.list_view_owner != null)
			{
				this.list_view_owner.Redraw(true);
			}
			return this.list.Count - 1;
		}

		/// <summary>Removes all groups from the collection.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000AF2 RID: 2802 RVA: 0x0002EB3C File Offset: 0x0002CD3C
		public void Clear()
		{
			foreach (ListViewGroup listViewGroup in this.list)
			{
				listViewGroup.ListViewOwner = null;
			}
			this.list.Clear();
			if (this.list_view_owner != null)
			{
				this.list_view_owner.Redraw(true);
			}
		}

		/// <summary>Determines whether the specified value is located in the collection.</summary>
		/// <returns>true if <paramref name="value" /> is a <see cref="T:System.Windows.Forms.ListViewGroup" /> contained in the collection; otherwise, false.</returns>
		/// <param name="value">An object that represents the <see cref="T:System.Windows.Forms.ListViewGroup" /> to locate in the collection.</param>
		// Token: 0x06000AF3 RID: 2803 RVA: 0x0002EBAC File Offset: 0x0002CDAC
		bool IList.Contains(object value)
		{
			return value is ListViewGroup && this.Contains((ListViewGroup)value);
		}

		/// <summary>Determines whether the specified group is located in the collection.</summary>
		/// <returns>true if the group is in the collection; otherwise, false.</returns>
		/// <param name="value">The <see cref="T:System.Windows.Forms.ListViewGroup" /> to locate in the collection. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000AF4 RID: 2804 RVA: 0x0002EBC4 File Offset: 0x0002CDC4
		public bool Contains(ListViewGroup value)
		{
			return this.list.Contains(value);
		}

		/// <summary>Returns the index within the collection of the specified value.</summary>
		/// <returns>The zero-based index of <paramref name="value" /> if it is in the collection; otherwise, -1.</returns>
		/// <param name="value">The <see cref="T:System.Windows.Forms.ListViewGroup" /> to find in the <see cref="T:System.Windows.Forms.ListViewGroupCollection" />.</param>
		// Token: 0x06000AF5 RID: 2805 RVA: 0x0002EBD2 File Offset: 0x0002CDD2
		int IList.IndexOf(object value)
		{
			if (value is ListViewGroup)
			{
				return this.IndexOf((ListViewGroup)value);
			}
			return -1;
		}

		/// <summary>Returns the index of the specified <see cref="T:System.Windows.Forms.ListViewGroup" /> within the collection.</summary>
		/// <returns>The zero-based index of the group within the collection, or -1 if the group is not in the collection.</returns>
		/// <param name="value">The <see cref="T:System.Windows.Forms.ListViewGroup" /> to locate in the collection. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000AF6 RID: 2806 RVA: 0x0002EBEA File Offset: 0x0002CDEA
		public int IndexOf(ListViewGroup value)
		{
			return this.list.IndexOf(value);
		}

		/// <summary>Inserts a <see cref="T:System.Windows.Forms.ListViewGroup" /> into the <see cref="T:System.Windows.Forms.ListViewGroupCollection" />.</summary>
		/// <param name="index">The position at which the <see cref="T:System.Windows.Forms.ListViewGroup" /> is added to the collection.</param>
		/// <param name="value">The <see cref="T:System.Windows.Forms.ListViewGroup" /> to add to the collection.</param>
		// Token: 0x06000AF7 RID: 2807 RVA: 0x0002EBF8 File Offset: 0x0002CDF8
		void IList.Insert(int index, object value)
		{
			if (value is ListViewGroup)
			{
				this.Insert(index, (ListViewGroup)value);
			}
		}

		/// <summary>Inserts the specified <see cref="T:System.Windows.Forms.ListViewGroup" /> into the collection at the specified index.</summary>
		/// <param name="index">The index within the collection at which to insert the group. </param>
		/// <param name="group">The <see cref="T:System.Windows.Forms.ListViewGroup" /> to insert into the collection. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000AF8 RID: 2808 RVA: 0x0002EC0F File Offset: 0x0002CE0F
		public void Insert(int index, ListViewGroup group)
		{
			if (this.Contains(group))
			{
				return;
			}
			this.CheckListViewItemsInGroup(group);
			group.ListViewOwner = this.list_view_owner;
			this.list.Insert(index, group);
			if (this.list_view_owner != null)
			{
				this.list_view_owner.Redraw(true);
			}
		}

		/// <summary>Gets a value indicating whether the collection has a fixed size.</summary>
		/// <returns>false in all cases.</returns>
		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x06000AF9 RID: 2809 RVA: 0x00002D70 File Offset: 0x00000F70
		bool IList.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether the collection is read-only.</summary>
		/// <returns>false in all cases.</returns>
		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x06000AFA RID: 2810 RVA: 0x00002D70 File Offset: 0x00000F70
		bool IList.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Removes the <see cref="T:System.Windows.Forms.ListViewGroup" /> from the <see cref="T:System.Windows.Forms.ListViewGroupCollection" />.</summary>
		/// <param name="value">The <see cref="T:System.Windows.Forms.ListViewGroup" /> to remove from the <see cref="T:System.Windows.Forms.ListViewGroupCollection" />.</param>
		// Token: 0x06000AFB RID: 2811 RVA: 0x0002EC4F File Offset: 0x0002CE4F
		void IList.Remove(object value)
		{
			this.Remove((ListViewGroup)value);
		}

		/// <summary>Removes the specified <see cref="T:System.Windows.Forms.ListViewGroup" /> from the collection.</summary>
		/// <param name="group">The <see cref="T:System.Windows.Forms.ListViewGroup" /> to remove from the collection. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000AFC RID: 2812 RVA: 0x0002EC60 File Offset: 0x0002CE60
		public void Remove(ListViewGroup group)
		{
			int num = this.list.IndexOf(group);
			if (num != -1)
			{
				this.RemoveAt(num);
			}
		}

		/// <summary>Removes the <see cref="T:System.Windows.Forms.ListViewGroup" /> at the specified index within the collection.</summary>
		/// <param name="index">The index within the collection of the <see cref="T:System.Windows.Forms.ListViewGroup" /> to remove. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000AFD RID: 2813 RVA: 0x0002EC88 File Offset: 0x0002CE88
		public void RemoveAt(int index)
		{
			if (this.list.Count <= index || index < 0)
			{
				return;
			}
			this.list[index].ListViewOwner = null;
			this.list.RemoveAt(index);
			if (this.list_view_owner != null)
			{
				this.list_view_owner.Redraw(true);
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.ListViewGroup" /> at the specified index within the collection.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ListViewGroup" /> that represents the item located at the specified index within the collection.</returns>
		/// <param name="index">The zero-based index of the element to get or set.</param>
		// Token: 0x170002C9 RID: 713
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				if (value is ListViewGroup)
				{
					this[index] = (ListViewGroup)value;
				}
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.ListViewGroup" /> at the specified index within the collection.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ListViewGroup" /> at the specified index within the collection.</returns>
		/// <param name="index">The index within the collection of the <see cref="T:System.Windows.Forms.ListViewGroup" /> to get or set. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than 0 or greater than or equal to <see cref="P:System.Windows.Forms.ListViewGroupCollection.Count" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002CA RID: 714
		public ListViewGroup this[int index]
		{
			get
			{
				if (this.list.Count <= index || index < 0)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				return this.list[index];
			}
			set
			{
				if (this.list.Count <= index || index < 0)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				if (this.Contains(value))
				{
					return;
				}
				if (value != null)
				{
					this.CheckListViewItemsInGroup(value);
				}
				this.list[index] = value;
				if (this.list_view_owner != null)
				{
					this.list_view_owner.Redraw(true);
				}
			}
		}

		// Token: 0x06000B02 RID: 2818 RVA: 0x0002ED87 File Offset: 0x0002CF87
		internal ListViewGroup GetInternalGroup(int index)
		{
			if (index == 0)
			{
				return this.default_group;
			}
			return this.list[index - 1];
		}

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06000B03 RID: 2819 RVA: 0x0002EDA1 File Offset: 0x0002CFA1
		internal int InternalCount
		{
			get
			{
				return this.list.Count + 1;
			}
		}

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x06000B04 RID: 2820 RVA: 0x0002EDB0 File Offset: 0x0002CFB0
		internal ListViewGroup DefaultGroup
		{
			get
			{
				return this.default_group;
			}
		}

		// Token: 0x06000B05 RID: 2821 RVA: 0x0002EDB8 File Offset: 0x0002CFB8
		private void AddGroup(ListViewGroup group)
		{
			if (this.Contains(group))
			{
				return;
			}
			this.CheckListViewItemsInGroup(group);
			group.ListViewOwner = this.list_view_owner;
			this.list.Add(group);
		}

		// Token: 0x06000B06 RID: 2822 RVA: 0x0002EDE4 File Offset: 0x0002CFE4
		private void CheckListViewItemsInGroup(ListViewGroup value)
		{
			foreach (object obj in value.Items)
			{
				ListViewItem listViewItem = (ListViewItem)obj;
				if (listViewItem.ListView != null && listViewItem.ListView != this.list_view_owner)
				{
					throw new ArgumentException("ListViewItem belongs to a ListView control other than the one that owns this ListViewGroupCollection.", "ListViewGroup");
				}
			}
		}

		// Token: 0x0400073C RID: 1852
		private List<ListViewGroup> list;

		// Token: 0x0400073D RID: 1853
		private ListView list_view_owner;

		// Token: 0x0400073E RID: 1854
		private ListViewGroup default_group;
	}
}
