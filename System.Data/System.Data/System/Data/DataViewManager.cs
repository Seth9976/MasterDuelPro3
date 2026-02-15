using System;
using System.Collections;
using System.ComponentModel;
using System.Data.Common;

namespace System.Data
{
	/// <summary>Contains a default <see cref="T:System.Data.DataViewSettingCollection" /> for each <see cref="T:System.Data.DataTable" /> in a <see cref="T:System.Data.DataSet" />.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000058 RID: 88
	public class DataViewManager : MarshalByValueComponent, IBindingList, IList, ICollection, IEnumerable, ITypedList
	{
		// Token: 0x0600055E RID: 1374 RVA: 0x0001A3F4 File Offset: 0x000185F4
		internal DataViewManager(DataSet dataSet, bool locked)
		{
			GC.SuppressFinalize(this);
			this._dataSet = dataSet;
			if (this._dataSet != null)
			{
				this._dataSet.Tables.CollectionChanged += this.TableCollectionChanged;
				this._dataSet.Relations.CollectionChanged += this.RelationCollectionChanged;
			}
			this._locked = locked;
			this._item = new DataViewManagerListItemTypeDescriptor(this);
			this._dataViewSettingsCollection = new DataViewSettingCollection(this);
		}

		/// <summary>Gets or sets the <see cref="T:System.Data.DataSet" /> to use with the <see cref="T:System.Data.DataViewManager" />.</summary>
		/// <returns>The <see cref="T:System.Data.DataSet" /> to use.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x170000EA RID: 234
		// (get) Token: 0x0600055F RID: 1375 RVA: 0x0001A475 File Offset: 0x00018675
		[DefaultValue(null)]
		public DataSet DataSet
		{
			get
			{
				return this._dataSet;
			}
		}

		/// <summary>Gets the <see cref="T:System.Data.DataViewSettingCollection" /> for each <see cref="T:System.Data.DataTable" /> in the <see cref="T:System.Data.DataSet" />.</summary>
		/// <returns>A <see cref="T:System.Data.DataViewSettingCollection" /> for each DataTable.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000560 RID: 1376 RVA: 0x0001A47D File Offset: 0x0001867D
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public DataViewSettingCollection DataViewSettings
		{
			get
			{
				return this._dataViewSettingsCollection;
			}
		}

		/// <summary>For a description of this member, see <see cref="M:System.Collections.IEnumerable.GetEnumerator" />.</summary>
		/// <returns>For a description of this member, see <see cref="M:System.Collections.IEnumerable.GetEnumerator" />.</returns>
		// Token: 0x06000561 RID: 1377 RVA: 0x0001A488 File Offset: 0x00018688
		IEnumerator IEnumerable.GetEnumerator()
		{
			DataViewManagerListItemTypeDescriptor[] array = new DataViewManagerListItemTypeDescriptor[1];
			((ICollection)this).CopyTo(array, 0);
			return array.GetEnumerator();
		}

		/// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.Count" />.</summary>
		/// <returns>For a description of this member, see <see cref="P:System.Collections.ICollection.Count" />.</returns>
		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000562 RID: 1378 RVA: 0x0000593A File Offset: 0x00003B3A
		int ICollection.Count
		{
			get
			{
				return 1;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.SyncRoot" />.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.</returns>
		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000563 RID: 1379 RVA: 0x0000207F File Offset: 0x0000027F
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.IsSynchronized" />.</summary>
		/// <returns>true if access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe); otherwise, false.</returns>
		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000564 RID: 1380 RVA: 0x00011ED5 File Offset: 0x000100D5
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Collections.IList.IsReadOnly" />.</summary>
		/// <returns>For a description of this member, see <see cref="P:System.Collections.IList.IsReadOnly" />.</returns>
		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000565 RID: 1381 RVA: 0x0000593A File Offset: 0x00003B3A
		bool IList.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Collections.IList.IsFixedSize" />.</summary>
		/// <returns>true if the <see cref="T:System.Collections.IList" /> has a fixed size; otherwise, false.</returns>
		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000566 RID: 1382 RVA: 0x0000593A File Offset: 0x00003B3A
		bool IList.IsFixedSize
		{
			get
			{
				return true;
			}
		}

		/// <summary>Copies the elements of the <see cref="T:System.Collections.ICollection" /> to an <see cref="T:System.Array" />, starting at a particular <see cref="T:System.Array" /> index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.ICollection" />. The <see cref="T:System.Array" /> must have zero-based indexing. </param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins. </param>
		// Token: 0x06000567 RID: 1383 RVA: 0x0001A4AA File Offset: 0x000186AA
		void ICollection.CopyTo(Array array, int index)
		{
			array.SetValue(new DataViewManagerListItemTypeDescriptor(this), index);
		}

		/// <summary>Gets or sets the element at the specified index.</summary>
		/// <returns>The element at the specified index.</returns>
		/// <param name="index">The zero-based index of the element to get or set. </param>
		// Token: 0x170000F1 RID: 241
		object IList.this[int index]
		{
			get
			{
				return this._item;
			}
			set
			{
				throw ExceptionBuilder.CannotModifyCollection();
			}
		}

		/// <summary>Adds an item to the <see cref="T:System.Collections.IList" />.</summary>
		/// <returns>The position into which the new element was inserted.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to add to the <see cref="T:System.Collections.IList" />. </param>
		// Token: 0x0600056A RID: 1386 RVA: 0x0001A4C1 File Offset: 0x000186C1
		int IList.Add(object value)
		{
			throw ExceptionBuilder.CannotModifyCollection();
		}

		/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Clear" />.</summary>
		// Token: 0x0600056B RID: 1387 RVA: 0x0001A4C1 File Offset: 0x000186C1
		void IList.Clear()
		{
			throw ExceptionBuilder.CannotModifyCollection();
		}

		/// <summary>Determines whether the <see cref="T:System.Collections.IList" /> contains a specific value.</summary>
		/// <returns>true if the <see cref="T:System.Object" /> is found in the <see cref="T:System.Collections.IList" />; otherwise, false.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to locate in the <see cref="T:System.Collections.IList" />. </param>
		// Token: 0x0600056C RID: 1388 RVA: 0x0001A4C8 File Offset: 0x000186C8
		bool IList.Contains(object value)
		{
			return value == this._item;
		}

		/// <summary>Determines the index of a specific item in the <see cref="T:System.Collections.IList" />.</summary>
		/// <returns>The index of <paramref name="value" /> if found in the list; otherwise, -1.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to locate in the <see cref="T:System.Collections.IList" />. </param>
		// Token: 0x0600056D RID: 1389 RVA: 0x0001A4D3 File Offset: 0x000186D3
		int IList.IndexOf(object value)
		{
			if (value != this._item)
			{
				return -1;
			}
			return 1;
		}

		/// <summary>Inserts an item to the <see cref="T:System.Collections.IList" /> at the specified index.</summary>
		/// <param name="index">The zero-based index at which <paramref name="value" /> should be inserted. </param>
		/// <param name="value">The <see cref="T:System.Object" /> to insert into the <see cref="T:System.Collections.IList" />. </param>
		// Token: 0x0600056E RID: 1390 RVA: 0x0001A4C1 File Offset: 0x000186C1
		void IList.Insert(int index, object value)
		{
			throw ExceptionBuilder.CannotModifyCollection();
		}

		/// <summary>Removes the first occurrence of a specific object from the <see cref="T:System.Collections.IList" />.</summary>
		/// <param name="value">The <see cref="T:System.Object" /> to remove from the <see cref="T:System.Collections.IList" />. </param>
		// Token: 0x0600056F RID: 1391 RVA: 0x0001A4C1 File Offset: 0x000186C1
		void IList.Remove(object value)
		{
			throw ExceptionBuilder.CannotModifyCollection();
		}

		/// <summary>Removes the <see cref="T:System.Collections.IList" /> item at the specified index.</summary>
		/// <param name="index">The zero-based index of the item to remove. </param>
		// Token: 0x06000570 RID: 1392 RVA: 0x0001A4C1 File Offset: 0x000186C1
		void IList.RemoveAt(int index)
		{
			throw ExceptionBuilder.CannotModifyCollection();
		}

		/// <summary>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.AllowNew" />.</summary>
		/// <returns>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.AllowNew" />.</returns>
		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000571 RID: 1393 RVA: 0x00011ED5 File Offset: 0x000100D5
		bool IBindingList.AllowNew
		{
			get
			{
				return false;
			}
		}

		/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.IBindingList.AddNew" />.</summary>
		/// <returns>For a description of this member, see <see cref="M:System.ComponentModel.IBindingList.AddNew" />.</returns>
		// Token: 0x06000572 RID: 1394 RVA: 0x0001A4E1 File Offset: 0x000186E1
		object IBindingList.AddNew()
		{
			throw DataViewManager.s_notSupported;
		}

		/// <summary>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.AllowEdit" />.</summary>
		/// <returns>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.AllowEdit" />.</returns>
		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000573 RID: 1395 RVA: 0x00011ED5 File Offset: 0x000100D5
		bool IBindingList.AllowEdit
		{
			get
			{
				return false;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.AllowRemove" />.</summary>
		/// <returns>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.AllowRemove" />.</returns>
		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000574 RID: 1396 RVA: 0x00011ED5 File Offset: 0x000100D5
		bool IBindingList.AllowRemove
		{
			get
			{
				return false;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.SupportsChangeNotification" />.</summary>
		/// <returns>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.SupportsChangeNotification" />.</returns>
		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000575 RID: 1397 RVA: 0x0000593A File Offset: 0x00003B3A
		bool IBindingList.SupportsChangeNotification
		{
			get
			{
				return true;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.SupportsSearching" />.</summary>
		/// <returns>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.SupportsSearching" />.</returns>
		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06000576 RID: 1398 RVA: 0x00011ED5 File Offset: 0x000100D5
		bool IBindingList.SupportsSearching
		{
			get
			{
				return false;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.SupportsSorting" />.</summary>
		/// <returns>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.SupportsSorting" />.</returns>
		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000577 RID: 1399 RVA: 0x00011ED5 File Offset: 0x000100D5
		bool IBindingList.SupportsSorting
		{
			get
			{
				return false;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.IsSorted" />.</summary>
		/// <returns>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.IsSorted" />.</returns>
		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000578 RID: 1400 RVA: 0x0001A4E1 File Offset: 0x000186E1
		bool IBindingList.IsSorted
		{
			get
			{
				throw DataViewManager.s_notSupported;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.SortProperty" />.</summary>
		/// <returns>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.SortProperty" />.</returns>
		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000579 RID: 1401 RVA: 0x0001A4E1 File Offset: 0x000186E1
		PropertyDescriptor IBindingList.SortProperty
		{
			get
			{
				throw DataViewManager.s_notSupported;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.SortDirection" />.</summary>
		/// <returns>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.SortDirection" />.</returns>
		// Token: 0x170000FA RID: 250
		// (get) Token: 0x0600057A RID: 1402 RVA: 0x0001A4E1 File Offset: 0x000186E1
		ListSortDirection IBindingList.SortDirection
		{
			get
			{
				throw DataViewManager.s_notSupported;
			}
		}

		/// <summary>Occurs after a row is added to or deleted from a <see cref="T:System.Data.DataView" />.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x14000007 RID: 7
		// (add) Token: 0x0600057B RID: 1403 RVA: 0x0001A4E8 File Offset: 0x000186E8
		// (remove) Token: 0x0600057C RID: 1404 RVA: 0x0001A520 File Offset: 0x00018720
		public event ListChangedEventHandler ListChanged;

		/// <summary>Adds the <see cref="T:System.ComponentModel.PropertyDescriptor" /> to the indexes used for searching.</summary>
		/// <param name="property">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> to add to the indexes used for searching. </param>
		// Token: 0x0600057D RID: 1405 RVA: 0x00003FD2 File Offset: 0x000021D2
		void IBindingList.AddIndex(PropertyDescriptor property)
		{
		}

		/// <summary>Sorts the list based on a <see cref="T:System.ComponentModel.PropertyDescriptor" /> and a <see cref="T:System.ComponentModel.ListSortDirection" />.</summary>
		/// <param name="property">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> to sort by. </param>
		/// <param name="direction">One of the <see cref="T:System.ComponentModel.ListSortDirection" /> values. </param>
		// Token: 0x0600057E RID: 1406 RVA: 0x0001A4E1 File Offset: 0x000186E1
		void IBindingList.ApplySort(PropertyDescriptor property, ListSortDirection direction)
		{
			throw DataViewManager.s_notSupported;
		}

		/// <summary>Returns the index of the row that has the given <see cref="T:System.ComponentModel.PropertyDescriptor" />.</summary>
		/// <returns>The index of the row that has the given <see cref="T:System.ComponentModel.PropertyDescriptor" />.</returns>
		/// <param name="property">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> to search on.</param>
		/// <param name="key">The value of the property parameter to search for.</param>
		// Token: 0x0600057F RID: 1407 RVA: 0x0001A4E1 File Offset: 0x000186E1
		int IBindingList.Find(PropertyDescriptor property, object key)
		{
			throw DataViewManager.s_notSupported;
		}

		/// <summary>Removes the <see cref="T:System.ComponentModel.PropertyDescriptor" /> from the indexes used for searching.</summary>
		/// <param name="property">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> to remove from the indexes used for searching. </param>
		// Token: 0x06000580 RID: 1408 RVA: 0x00003FD2 File Offset: 0x000021D2
		void IBindingList.RemoveIndex(PropertyDescriptor property)
		{
		}

		/// <summary>Removes any sort applied using <see cref="M:System.ComponentModel.IBindingList.ApplySort(System.ComponentModel.PropertyDescriptor,System.ComponentModel.ListSortDirection)" />.</summary>
		// Token: 0x06000581 RID: 1409 RVA: 0x0001A4E1 File Offset: 0x000186E1
		void IBindingList.RemoveSort()
		{
			throw DataViewManager.s_notSupported;
		}

		/// <summary>Returns the name of the list.</summary>
		/// <returns>The name of the list.</returns>
		/// <param name="listAccessors">An array of <see cref="T:System.ComponentModel.PropertyDescriptor" /> objects, for which the list name is returned. This can be null. </param>
		// Token: 0x06000582 RID: 1410 RVA: 0x0001A558 File Offset: 0x00018758
		string ITypedList.GetListName(PropertyDescriptor[] listAccessors)
		{
			DataSet dataSet = this.DataSet;
			if (dataSet == null)
			{
				throw ExceptionBuilder.CanNotUseDataViewManager();
			}
			if (listAccessors == null || listAccessors.Length == 0)
			{
				return dataSet.DataSetName;
			}
			DataTable dataTable = dataSet.FindTable(null, listAccessors, 0);
			if (dataTable != null)
			{
				return dataTable.TableName;
			}
			return string.Empty;
		}

		/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.ITypedList.GetItemProperties(System.ComponentModel.PropertyDescriptor[])" />.</summary>
		// Token: 0x06000583 RID: 1411 RVA: 0x0001A59C File Offset: 0x0001879C
		PropertyDescriptorCollection ITypedList.GetItemProperties(PropertyDescriptor[] listAccessors)
		{
			DataSet dataSet = this.DataSet;
			if (dataSet == null)
			{
				throw ExceptionBuilder.CanNotUseDataViewManager();
			}
			if (listAccessors == null || listAccessors.Length == 0)
			{
				return ((ICustomTypeDescriptor)new DataViewManagerListItemTypeDescriptor(this)).GetProperties();
			}
			DataTable dataTable = dataSet.FindTable(null, listAccessors, 0);
			if (dataTable != null)
			{
				return dataTable.GetPropertyDescriptorCollection(null);
			}
			return new PropertyDescriptorCollection(null);
		}

		/// <summary>Creates a <see cref="T:System.Data.DataView" /> for the specified <see cref="T:System.Data.DataTable" />.</summary>
		/// <returns>A <see cref="T:System.Data.DataView" /> object.</returns>
		/// <param name="table">The name of the <see cref="T:System.Data.DataTable" /> to use in the <see cref="T:System.Data.DataView" />. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000584 RID: 1412 RVA: 0x0001A5E7 File Offset: 0x000187E7
		public DataView CreateDataView(DataTable table)
		{
			if (this._dataSet == null)
			{
				throw ExceptionBuilder.CanNotUseDataViewManager();
			}
			DataView dataView = new DataView(table);
			dataView.SetDataViewManager(this);
			return dataView;
		}

		/// <summary>Raises the <see cref="E:System.Data.DataViewManager.ListChanged" /> event.</summary>
		/// <param name="e">A <see cref="T:System.ComponentModel.ListChangedEventArgs" /> that contains the event data. </param>
		// Token: 0x06000585 RID: 1413 RVA: 0x0001A604 File Offset: 0x00018804
		protected virtual void OnListChanged(ListChangedEventArgs e)
		{
			try
			{
				ListChangedEventHandler listChanged = this.ListChanged;
				if (listChanged != null)
				{
					listChanged(this, e);
				}
			}
			catch (Exception ex) when (ADP.IsCatchableExceptionType(ex))
			{
				ExceptionBuilder.TraceExceptionWithoutRethrow(ex);
			}
		}

		/// <summary>Raises a <see cref="E:System.Data.DataTableCollection.CollectionChanged" /> event when a <see cref="T:System.Data.DataTable" /> is added to or removed from the <see cref="T:System.Data.DataTableCollection" />.</summary>
		/// <param name="sender">The source of the event. </param>
		/// <param name="e">A <see cref="T:System.ComponentModel.CollectionChangeEventArgs" /> that contains the event data. </param>
		// Token: 0x06000586 RID: 1414 RVA: 0x0001A658 File Offset: 0x00018858
		protected virtual void TableCollectionChanged(object sender, CollectionChangeEventArgs e)
		{
			PropertyDescriptor propertyDescriptor = null;
			this.OnListChanged((e.Action == CollectionChangeAction.Add) ? new ListChangedEventArgs(ListChangedType.PropertyDescriptorAdded, new DataTablePropertyDescriptor((DataTable)e.Element)) : ((e.Action == CollectionChangeAction.Refresh) ? new ListChangedEventArgs(ListChangedType.PropertyDescriptorChanged, propertyDescriptor) : ((e.Action == CollectionChangeAction.Remove) ? new ListChangedEventArgs(ListChangedType.PropertyDescriptorDeleted, new DataTablePropertyDescriptor((DataTable)e.Element)) : null)));
		}

		/// <summary>Raises a <see cref="E:System.Data.DataRelationCollection.CollectionChanged" /> event when a <see cref="T:System.Data.DataRelation" /> is added to or removed from the <see cref="T:System.Data.DataRelationCollection" />.</summary>
		/// <param name="sender">The source of the event. </param>
		/// <param name="e">A <see cref="T:System.ComponentModel.CollectionChangeEventArgs" /> that contains the event data. </param>
		// Token: 0x06000587 RID: 1415 RVA: 0x0001A6C4 File Offset: 0x000188C4
		protected virtual void RelationCollectionChanged(object sender, CollectionChangeEventArgs e)
		{
			DataRelationPropertyDescriptor dataRelationPropertyDescriptor = null;
			this.OnListChanged((e.Action == CollectionChangeAction.Add) ? new ListChangedEventArgs(ListChangedType.PropertyDescriptorAdded, new DataRelationPropertyDescriptor((DataRelation)e.Element)) : ((e.Action == CollectionChangeAction.Refresh) ? new ListChangedEventArgs(ListChangedType.PropertyDescriptorChanged, dataRelationPropertyDescriptor) : ((e.Action == CollectionChangeAction.Remove) ? new ListChangedEventArgs(ListChangedType.PropertyDescriptorDeleted, new DataRelationPropertyDescriptor((DataRelation)e.Element)) : null)));
		}

		// Token: 0x040001B1 RID: 433
		private DataViewSettingCollection _dataViewSettingsCollection;

		// Token: 0x040001B2 RID: 434
		private DataSet _dataSet;

		// Token: 0x040001B3 RID: 435
		private DataViewManagerListItemTypeDescriptor _item;

		// Token: 0x040001B4 RID: 436
		private bool _locked;

		// Token: 0x040001B5 RID: 437
		internal int _nViews;

		// Token: 0x040001B6 RID: 438
		private static NotSupportedException s_notSupported = new NotSupportedException();
	}
}
