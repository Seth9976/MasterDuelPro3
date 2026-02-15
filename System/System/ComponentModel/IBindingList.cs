using System;
using System.Collections;

namespace System.ComponentModel
{
	/// <summary>Provides the features required to support both complex and simple scenarios when binding to a data source.</summary>
	// Token: 0x0200027B RID: 635
	public interface IBindingList : IList, ICollection, IEnumerable
	{
		/// <summary>Gets whether you can add items to the list using <see cref="M:System.ComponentModel.IBindingList.AddNew" />.</summary>
		/// <returns>true if you can add items to the list using <see cref="M:System.ComponentModel.IBindingList.AddNew" />; otherwise, false.</returns>
		// Token: 0x17000331 RID: 817
		// (get) Token: 0x06000F24 RID: 3876
		bool AllowNew { get; }

		/// <summary>Adds a new item to the list.</summary>
		/// <returns>The item added to the list.</returns>
		/// <exception cref="T:System.NotSupportedException">
		///   <see cref="P:System.ComponentModel.IBindingList.AllowNew" /> is false. </exception>
		// Token: 0x06000F25 RID: 3877
		object AddNew();

		/// <summary>Gets whether you can update items in the list.</summary>
		/// <returns>true if you can update the items in the list; otherwise, false.</returns>
		// Token: 0x17000332 RID: 818
		// (get) Token: 0x06000F26 RID: 3878
		bool AllowEdit { get; }

		/// <summary>Gets whether you can remove items from the list, using <see cref="M:System.Collections.IList.Remove(System.Object)" /> or <see cref="M:System.Collections.IList.RemoveAt(System.Int32)" />.</summary>
		/// <returns>true if you can remove items from the list; otherwise, false.</returns>
		// Token: 0x17000333 RID: 819
		// (get) Token: 0x06000F27 RID: 3879
		bool AllowRemove { get; }

		/// <summary>Gets whether a <see cref="E:System.ComponentModel.IBindingList.ListChanged" /> event is raised when the list changes or an item in the list changes.</summary>
		/// <returns>true if a <see cref="E:System.ComponentModel.IBindingList.ListChanged" /> event is raised when the list changes or when an item changes; otherwise, false.</returns>
		// Token: 0x17000334 RID: 820
		// (get) Token: 0x06000F28 RID: 3880
		bool SupportsChangeNotification { get; }

		/// <summary>Gets whether the list supports searching using the <see cref="M:System.ComponentModel.IBindingList.Find(System.ComponentModel.PropertyDescriptor,System.Object)" /> method.</summary>
		/// <returns>true if the list supports searching using the <see cref="M:System.ComponentModel.IBindingList.Find(System.ComponentModel.PropertyDescriptor,System.Object)" /> method; otherwise, false.</returns>
		// Token: 0x17000335 RID: 821
		// (get) Token: 0x06000F29 RID: 3881
		bool SupportsSearching { get; }

		/// <summary>Gets whether the list supports sorting.</summary>
		/// <returns>true if the list supports sorting; otherwise, false.</returns>
		// Token: 0x17000336 RID: 822
		// (get) Token: 0x06000F2A RID: 3882
		bool SupportsSorting { get; }

		/// <summary>Gets whether the items in the list are sorted.</summary>
		/// <returns>true if <see cref="M:System.ComponentModel.IBindingList.ApplySort(System.ComponentModel.PropertyDescriptor,System.ComponentModel.ListSortDirection)" /> has been called and <see cref="M:System.ComponentModel.IBindingList.RemoveSort" /> has not been called; otherwise, false.</returns>
		/// <exception cref="T:System.NotSupportedException">
		///   <see cref="P:System.ComponentModel.IBindingList.SupportsSorting" /> is false. </exception>
		// Token: 0x17000337 RID: 823
		// (get) Token: 0x06000F2B RID: 3883
		bool IsSorted { get; }

		/// <summary>Gets the <see cref="T:System.ComponentModel.PropertyDescriptor" /> that is being used for sorting.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.PropertyDescriptor" /> that is being used for sorting.</returns>
		/// <exception cref="T:System.NotSupportedException">
		///   <see cref="P:System.ComponentModel.IBindingList.SupportsSorting" /> is false. </exception>
		// Token: 0x17000338 RID: 824
		// (get) Token: 0x06000F2C RID: 3884
		PropertyDescriptor SortProperty { get; }

		/// <summary>Gets the direction of the sort.</summary>
		/// <returns>One of the <see cref="T:System.ComponentModel.ListSortDirection" /> values.</returns>
		/// <exception cref="T:System.NotSupportedException">
		///   <see cref="P:System.ComponentModel.IBindingList.SupportsSorting" /> is false. </exception>
		// Token: 0x17000339 RID: 825
		// (get) Token: 0x06000F2D RID: 3885
		ListSortDirection SortDirection { get; }

		/// <summary>Occurs when the list changes or an item in the list changes.</summary>
		// Token: 0x14000008 RID: 8
		// (add) Token: 0x06000F2E RID: 3886
		// (remove) Token: 0x06000F2F RID: 3887
		event ListChangedEventHandler ListChanged;

		/// <summary>Adds the <see cref="T:System.ComponentModel.PropertyDescriptor" /> to the indexes used for searching.</summary>
		/// <param name="property">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> to add to the indexes used for searching. </param>
		// Token: 0x06000F30 RID: 3888
		void AddIndex(PropertyDescriptor property);

		/// <summary>Sorts the list based on a <see cref="T:System.ComponentModel.PropertyDescriptor" /> and a <see cref="T:System.ComponentModel.ListSortDirection" />.</summary>
		/// <param name="property">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> to sort by. </param>
		/// <param name="direction">One of the <see cref="T:System.ComponentModel.ListSortDirection" /> values. </param>
		/// <exception cref="T:System.NotSupportedException">
		///   <see cref="P:System.ComponentModel.IBindingList.SupportsSorting" /> is false. </exception>
		// Token: 0x06000F31 RID: 3889
		void ApplySort(PropertyDescriptor property, ListSortDirection direction);

		/// <summary>Returns the index of the row that has the given <see cref="T:System.ComponentModel.PropertyDescriptor" />.</summary>
		/// <returns>The index of the row that has the given <see cref="T:System.ComponentModel.PropertyDescriptor" />.</returns>
		/// <param name="property">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> to search on. </param>
		/// <param name="key">The value of the <paramref name="property" /> parameter to search for. </param>
		/// <exception cref="T:System.NotSupportedException">
		///   <see cref="P:System.ComponentModel.IBindingList.SupportsSearching" /> is false. </exception>
		// Token: 0x06000F32 RID: 3890
		int Find(PropertyDescriptor property, object key);

		/// <summary>Removes the <see cref="T:System.ComponentModel.PropertyDescriptor" /> from the indexes used for searching.</summary>
		/// <param name="property">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> to remove from the indexes used for searching. </param>
		// Token: 0x06000F33 RID: 3891
		void RemoveIndex(PropertyDescriptor property);

		/// <summary>Removes any sort applied using <see cref="M:System.ComponentModel.IBindingList.ApplySort(System.ComponentModel.PropertyDescriptor,System.ComponentModel.ListSortDirection)" />.</summary>
		/// <exception cref="T:System.NotSupportedException">
		///   <see cref="P:System.ComponentModel.IBindingList.SupportsSorting" /> is false. </exception>
		// Token: 0x06000F34 RID: 3892
		void RemoveSort();
	}
}
