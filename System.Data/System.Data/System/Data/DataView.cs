using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Globalization;
using System.Text;
using System.Threading;

namespace System.Data
{
	/// <summary>Represents a databindable, customized view of a <see cref="T:System.Data.DataTable" /> for sorting, filtering, searching, editing, and navigation. The <see cref="T:System.Data.DataView" /> does not store data, but instead represents a connected view of its corresponding <see cref="T:System.Data.DataTable" />. Changes to the <see cref="T:System.Data.DataView" />’s data will affect the <see cref="T:System.Data.DataTable" />. Changes to the <see cref="T:System.Data.DataTable" />’s data will affect all <see cref="T:System.Data.DataView" />s associated with it.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000055 RID: 85
	[DefaultProperty("Table")]
	[DefaultEvent("PositionChanged")]
	public class DataView : MarshalByValueComponent, IBindingList, IList, ICollection, IEnumerable, ITypedList
	{
		// Token: 0x060004F7 RID: 1271 RVA: 0x00018D58 File Offset: 0x00016F58
		internal DataView(DataTable table, bool locked)
		{
			GC.SuppressFinalize(this);
			DataCommonEventSource.Log.Trace<int, int, bool>("<ds.DataView.DataView|INFO> {0}, table={1}, locked={2}", this.ObjectID, (table != null) ? table.ObjectID : 0, locked);
			this._dvListener = new DataViewListener(this);
			this._locked = locked;
			this._table = table;
			this._dvListener.RegisterMetaDataEvents(this._table);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DataView" /> class with the specified <see cref="T:System.Data.DataTable" />.</summary>
		/// <param name="table">A <see cref="T:System.Data.DataTable" /> to add to the <see cref="T:System.Data.DataView" />. </param>
		// Token: 0x060004F8 RID: 1272 RVA: 0x00018E24 File Offset: 0x00017024
		public DataView(DataTable table)
			: this(table, false)
		{
			this.SetIndex2("", DataViewRowState.CurrentRows, null, true);
		}

		/// <summary>Sets or gets a value that indicates whether deletes are allowed.</summary>
		/// <returns>true, if deletes are allowed; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060004F9 RID: 1273 RVA: 0x00018E3D File Offset: 0x0001703D
		[DefaultValue(true)]
		public bool AllowDelete
		{
			get
			{
				return this._allowDelete;
			}
		}

		/// <summary>Gets or sets a value that indicates whether edits are allowed.</summary>
		/// <returns>true, if edits are allowed; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060004FA RID: 1274 RVA: 0x00018E45 File Offset: 0x00017045
		[DefaultValue(true)]
		public bool AllowEdit
		{
			get
			{
				return this._allowEdit;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the new rows can be added by using the <see cref="M:System.Data.DataView.AddNew" /> method.</summary>
		/// <returns>true, if new rows can be added; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060004FB RID: 1275 RVA: 0x00018E4D File Offset: 0x0001704D
		[DefaultValue(true)]
		public bool AllowNew
		{
			get
			{
				return this._allowNew;
			}
		}

		/// <summary>Gets the number of records in the <see cref="T:System.Data.DataView" /> after <see cref="P:System.Data.DataView.RowFilter" /> and <see cref="P:System.Data.DataView.RowStateFilter" /> have been applied.</summary>
		/// <returns>The number of records in the <see cref="T:System.Data.DataView" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060004FC RID: 1276 RVA: 0x00018E55 File Offset: 0x00017055
		[Browsable(false)]
		public int Count
		{
			get
			{
				return this._rowViewCache.Count;
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060004FD RID: 1277 RVA: 0x00018E62 File Offset: 0x00017062
		private int CountFromIndex
		{
			get
			{
				return ((this._index != null) ? this._index.RecordCount : 0) + ((this._addNewRow != null) ? 1 : 0);
			}
		}

		/// <summary>Gets the <see cref="T:System.Data.DataViewManager" /> associated with this view.</summary>
		/// <returns>The DataViewManager that created this view. If this is the default <see cref="T:System.Data.DataView" /> for a <see cref="T:System.Data.DataTable" />, the DataViewManager property returns the default DataViewManager for the DataSet. Otherwise, if the DataView was created without a DataViewManager, this property is null.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060004FE RID: 1278 RVA: 0x00018E87 File Offset: 0x00017087
		[Browsable(false)]
		public DataViewManager DataViewManager
		{
			get
			{
				return this._dataViewManager;
			}
		}

		/// <summary>Gets a value that indicates whether the data source is currently open and projecting views of data on the <see cref="T:System.Data.DataTable" />.</summary>
		/// <returns>true, if the source is open; otherwise, false.</returns>
		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060004FF RID: 1279 RVA: 0x00018E8F File Offset: 0x0001708F
		[Browsable(false)]
		protected bool IsOpen
		{
			get
			{
				return this._open;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.IsSynchronized" />.</summary>
		/// <returns>For a description of this member, see <see cref="P:System.Collections.ICollection.IsSynchronized" />.</returns>
		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000500 RID: 1280 RVA: 0x00011ED5 File Offset: 0x000100D5
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets or sets the row state filter used in the <see cref="T:System.Data.DataView" />.</summary>
		/// <returns>One of the <see cref="T:System.Data.DataViewRowState" /> values.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000501 RID: 1281 RVA: 0x00018E97 File Offset: 0x00017097
		[DefaultValue(DataViewRowState.CurrentRows)]
		public DataViewRowState RowStateFilter
		{
			get
			{
				return this._recordStates;
			}
		}

		/// <summary>Gets or sets the sort column or columns, and sort order for the <see cref="T:System.Data.DataView" />.</summary>
		/// <returns>A string that contains the column name followed by "ASC" (ascending) or "DESC" (descending). Columns are sorted ascending by default. Multiple columns can be separated by commas.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000502 RID: 1282 RVA: 0x00018EA0 File Offset: 0x000170A0
		// (set) Token: 0x06000503 RID: 1283 RVA: 0x00018EF8 File Offset: 0x000170F8
		[DefaultValue("")]
		public string Sort
		{
			get
			{
				if (this._sort.Length == 0 && this._applyDefaultSort && this._table != null && this._table._primaryIndex.Length != 0)
				{
					return this._table.FormatSortString(this._table._primaryIndex);
				}
				return this._sort;
			}
			set
			{
				if (value == null)
				{
					value = string.Empty;
				}
				DataCommonEventSource.Log.Trace<int, string>("<ds.DataView.set_Sort|API> {0}, '{1}'", this.ObjectID, value);
				if (this._fInitInProgress)
				{
					this._delayedSort = value;
					return;
				}
				CultureInfo cultureInfo = ((this._table != null) ? this._table.Locale : CultureInfo.CurrentCulture);
				if (string.Compare(this._sort, value, false, cultureInfo) != 0 || this._comparison != null)
				{
					this.CheckSort(value);
					this._comparison = null;
					this.SetIndex(value, this._recordStates, this._rowFilter);
				}
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000504 RID: 1284 RVA: 0x00018F89 File Offset: 0x00017189
		internal Comparison<DataRow> SortComparison
		{
			get
			{
				return this._comparison;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.SyncRoot" />.</summary>
		/// <returns>For a description of this member, see <see cref="P:System.Collections.ICollection.SyncRoot" />.</returns>
		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000505 RID: 1285 RVA: 0x0000207F File Offset: 0x0000027F
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>Gets or sets the source <see cref="T:System.Data.DataTable" />.</summary>
		/// <returns>A <see cref="T:System.Data.DataTable" /> that provides the data for this view.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000506 RID: 1286 RVA: 0x00018F91 File Offset: 0x00017191
		[TypeConverter(typeof(DataTableTypeConverter))]
		[DefaultValue(null)]
		[RefreshProperties(RefreshProperties.All)]
		public DataTable Table
		{
			get
			{
				return this._table;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Collections.IList.Item(System.Int32)" />.</summary>
		/// <returns>For a description of this member, see <see cref="P:System.Collections.IList.Item(System.Int32)" />.</returns>
		/// <param name="recordIndex">A <see cref="System.Int32" /> value.</param>
		// Token: 0x170000DC RID: 220
		object IList.this[int recordIndex]
		{
			get
			{
				return this[recordIndex];
			}
			set
			{
				throw ExceptionBuilder.SetIListObject();
			}
		}

		/// <summary>Gets a row of data from a specified table.</summary>
		/// <returns>A <see cref="T:System.Data.DataRowView" /> of the row that you want.</returns>
		/// <param name="recordIndex">The index of a record in the <see cref="T:System.Data.DataTable" />. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x170000DD RID: 221
		public DataRowView this[int recordIndex]
		{
			get
			{
				return this.GetRowView(this.GetRow(recordIndex));
			}
		}

		/// <summary>Adds a new row to the <see cref="T:System.Data.DataView" />.</summary>
		/// <returns>A new <see cref="T:System.Data.DataRowView" /> object.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600050A RID: 1290 RVA: 0x00018FB8 File Offset: 0x000171B8
		public virtual DataRowView AddNew()
		{
			long num = DataCommonEventSource.Log.EnterScope<int>("<ds.DataView.AddNew|API> {0}", this.ObjectID);
			DataRowView dataRowView2;
			try
			{
				this.CheckOpen();
				if (!this.AllowNew)
				{
					throw ExceptionBuilder.AddNewNotAllowNull();
				}
				if (this._addNewRow != null)
				{
					this._rowViewCache[this._addNewRow].EndEdit();
				}
				this._addNewRow = this._table.NewRow();
				DataRowView dataRowView = new DataRowView(this, this._addNewRow);
				this._rowViewCache.Add(this._addNewRow, dataRowView);
				this.OnListChanged(new ListChangedEventArgs(ListChangedType.ItemAdded, this.IndexOf(dataRowView)));
				dataRowView2 = dataRowView;
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
			return dataRowView2;
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x00019074 File Offset: 0x00017274
		private void CheckOpen()
		{
			if (!this.IsOpen)
			{
				throw ExceptionBuilder.NotOpen();
			}
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x00019084 File Offset: 0x00017284
		private void CheckSort(string sort)
		{
			if (this._table == null)
			{
				throw ExceptionBuilder.CanNotUse();
			}
			if (sort.Length == 0)
			{
				return;
			}
			this._table.ParseSortString(sort);
		}

		/// <summary>Closes the <see cref="T:System.Data.DataView" />.</summary>
		// Token: 0x0600050D RID: 1293 RVA: 0x000190AA File Offset: 0x000172AA
		protected void Close()
		{
			this._shouldOpen = false;
			this.UpdateIndex();
			this._dvListener.UnregisterMetaDataEvents();
		}

		/// <summary>Copies items into an array. Only for Web Forms Interfaces.</summary>
		/// <param name="array">array to copy into. </param>
		/// <param name="index">index to start at. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600050E RID: 1294 RVA: 0x000190C4 File Offset: 0x000172C4
		public void CopyTo(Array array, int index)
		{
			checked
			{
				if (this._index != null)
				{
					RBTree<int>.RBTreeEnumerator enumerator = this._index.GetEnumerator(0);
					while (enumerator.MoveNext())
					{
						int num = enumerator.Current;
						array.SetValue(this.GetRowView(num), index);
						index++;
					}
				}
				if (this._addNewRow != null)
				{
					array.SetValue(this._rowViewCache[this._addNewRow], index);
				}
			}
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x0001912C File Offset: 0x0001732C
		private void CopyTo(DataRowView[] array, int index)
		{
			checked
			{
				if (this._index != null)
				{
					RBTree<int>.RBTreeEnumerator enumerator = this._index.GetEnumerator(0);
					while (enumerator.MoveNext())
					{
						int num = enumerator.Current;
						array[index] = this.GetRowView(num);
						index++;
					}
				}
				if (this._addNewRow != null)
				{
					array[index] = this._rowViewCache[this._addNewRow];
				}
			}
		}

		/// <summary>Deletes a row at the specified index.</summary>
		/// <param name="index">The index of the row to delete. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000510 RID: 1296 RVA: 0x0001918A File Offset: 0x0001738A
		public void Delete(int index)
		{
			this.Delete(this.GetRow(index));
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x0001919C File Offset: 0x0001739C
		internal void Delete(DataRow row)
		{
			if (row != null)
			{
				long num = DataCommonEventSource.Log.EnterScope<int, int>("<ds.DataView.Delete|API> {0}, row={1}", this.ObjectID, row._objectID);
				try
				{
					this.CheckOpen();
					if (row == this._addNewRow)
					{
						this.FinishAddNew(false);
					}
					else
					{
						if (!this.AllowDelete)
						{
							throw ExceptionBuilder.CanNotDelete();
						}
						row.Delete();
					}
				}
				finally
				{
					DataCommonEventSource.Log.ExitScope(num);
				}
			}
		}

		/// <summary>Disposes of the resources (other than memory) used by the <see cref="T:System.Data.DataView" /> object.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x06000512 RID: 1298 RVA: 0x00019214 File Offset: 0x00017414
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.Close();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x00019228 File Offset: 0x00017428
		internal void FinishAddNew(bool success)
		{
			DataCommonEventSource.Log.Trace<int, bool>("<ds.DataView.FinishAddNew|INFO> {0}, success={1}", this.ObjectID, success);
			DataRow addNewRow = this._addNewRow;
			if (success)
			{
				if (DataRowState.Detached == addNewRow.RowState)
				{
					this._table.Rows.Add(addNewRow);
				}
				else
				{
					addNewRow.EndEdit();
				}
			}
			if (addNewRow == this._addNewRow)
			{
				this._rowViewCache.Remove(this._addNewRow);
				this._addNewRow = null;
				if (!success)
				{
					addNewRow.CancelEdit();
				}
				this.OnListChanged(new ListChangedEventArgs(ListChangedType.ItemDeleted, this.Count));
			}
		}

		/// <summary>Gets an enumerator for this <see cref="T:System.Data.DataView" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for navigating through the list.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000514 RID: 1300 RVA: 0x000192B4 File Offset: 0x000174B4
		public IEnumerator GetEnumerator()
		{
			DataRowView[] array = new DataRowView[this.Count];
			this.CopyTo(array, 0);
			return array.GetEnumerator();
		}

		/// <summary>For a description of this member, see <see cref="P:System.Collections.IList.IsReadOnly" />.</summary>
		/// <returns>For a description of this member, see <see cref="P:System.Collections.IList.IsReadOnly" />.</returns>
		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000515 RID: 1301 RVA: 0x00011ED5 File Offset: 0x000100D5
		bool IList.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Collections.IList.IsFixedSize" />.</summary>
		/// <returns>For a description of this member, see <see cref="P:System.Collections.IList.IsFixedSize" />.</returns>
		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000516 RID: 1302 RVA: 0x00011ED5 File Offset: 0x000100D5
		bool IList.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Add(System.Object)" />.</summary>
		/// <returns>For a description of this member, see <see cref="M:System.Collections.IList.Add(System.Object)" />.</returns>
		/// <param name="value">A <see cref="System.Object" /> value.</param>
		// Token: 0x06000517 RID: 1303 RVA: 0x000192DB File Offset: 0x000174DB
		int IList.Add(object value)
		{
			if (value == null)
			{
				this.AddNew();
				return this.Count - 1;
			}
			throw ExceptionBuilder.AddExternalObject();
		}

		/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Clear" />.</summary>
		// Token: 0x06000518 RID: 1304 RVA: 0x000192F5 File Offset: 0x000174F5
		void IList.Clear()
		{
			throw ExceptionBuilder.CanNotClear();
		}

		/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Contains(System.Object)" />.</summary>
		/// <returns>For a description of this member, see <see cref="M:System.Collections.IList.Contains(System.Object)" />.</returns>
		/// <param name="value">A <see cref="System.Object" /> value.</param>
		// Token: 0x06000519 RID: 1305 RVA: 0x000192FC File Offset: 0x000174FC
		bool IList.Contains(object value)
		{
			return 0 <= this.IndexOf(value as DataRowView);
		}

		/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.IndexOf(System.Object)" />.</summary>
		/// <returns>For a description of this member, see <see cref="M:System.Collections.IList.IndexOf(System.Object)" />.</returns>
		/// <param name="value">A <see cref="System.Object" /> value.</param>
		// Token: 0x0600051A RID: 1306 RVA: 0x00019310 File Offset: 0x00017510
		int IList.IndexOf(object value)
		{
			return this.IndexOf(value as DataRowView);
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x00019320 File Offset: 0x00017520
		internal int IndexOf(DataRowView rowview)
		{
			if (rowview != null)
			{
				if (this._addNewRow == rowview.Row)
				{
					return this.Count - 1;
				}
				DataRowView dataRowView;
				if (this._index != null && DataRowState.Detached != rowview.Row.RowState && this._rowViewCache.TryGetValue(rowview.Row, out dataRowView) && dataRowView == rowview)
				{
					return this.IndexOfDataRowView(rowview);
				}
			}
			return -1;
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x0001937F File Offset: 0x0001757F
		private int IndexOfDataRowView(DataRowView rowview)
		{
			return this._index.GetIndex(rowview.Row.GetRecordFromVersion(rowview.Row.GetDefaultRowVersion(this.RowStateFilter) & (DataRowVersion)(-1025)));
		}

		/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Insert(System.Int32,System.Object)" />.</summary>
		/// <param name="index">A <see cref="System.Int32" /> value.</param>
		/// <param name="value">A <see cref="System.Object" /> value to be inserted.</param>
		// Token: 0x0600051D RID: 1309 RVA: 0x000193AE File Offset: 0x000175AE
		void IList.Insert(int index, object value)
		{
			throw ExceptionBuilder.InsertExternalObject();
		}

		/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Remove(System.Object)" />.</summary>
		/// <param name="value">A <see cref="System.Object" /> value.</param>
		// Token: 0x0600051E RID: 1310 RVA: 0x000193B8 File Offset: 0x000175B8
		void IList.Remove(object value)
		{
			int num = this.IndexOf(value as DataRowView);
			if (0 <= num)
			{
				((IList)this).RemoveAt(num);
				return;
			}
			throw ExceptionBuilder.RemoveExternalObject();
		}

		/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.RemoveAt(System.Int32)" />.</summary>
		/// <param name="index">A <see cref="System.Int32" /> value.</param>
		// Token: 0x0600051F RID: 1311 RVA: 0x000193E3 File Offset: 0x000175E3
		void IList.RemoveAt(int index)
		{
			this.Delete(index);
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x000193EC File Offset: 0x000175EC
		internal Index GetFindIndex(string column, bool keepIndex)
		{
			if (this._findIndexes == null)
			{
				this._findIndexes = new Dictionary<string, Index>();
			}
			Index index;
			if (this._findIndexes.TryGetValue(column, out index))
			{
				if (!keepIndex)
				{
					this._findIndexes.Remove(column);
					index.RemoveRef();
					if (index.RefCount == 1)
					{
						index.RemoveRef();
					}
				}
			}
			else if (keepIndex)
			{
				index = this._table.GetIndex(column, this._recordStates, this.GetFilter());
				this._findIndexes[column] = index;
				index.AddRef();
			}
			return index;
		}

		/// <summary>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.AllowNew" />.</summary>
		/// <returns>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.AllowNew" />.</returns>
		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000521 RID: 1313 RVA: 0x00019475 File Offset: 0x00017675
		bool IBindingList.AllowNew
		{
			get
			{
				return this.AllowNew;
			}
		}

		/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.IBindingList.AddNew" />.</summary>
		/// <returns>The item added to the list.</returns>
		// Token: 0x06000522 RID: 1314 RVA: 0x0001947D File Offset: 0x0001767D
		object IBindingList.AddNew()
		{
			return this.AddNew();
		}

		/// <summary>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.AllowEdit" />.</summary>
		/// <returns>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.AllowEdit" />.</returns>
		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000523 RID: 1315 RVA: 0x00019485 File Offset: 0x00017685
		bool IBindingList.AllowEdit
		{
			get
			{
				return this.AllowEdit;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.AllowRemove" />.</summary>
		/// <returns>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.AllowRemove" />.</returns>
		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000524 RID: 1316 RVA: 0x0001948D File Offset: 0x0001768D
		bool IBindingList.AllowRemove
		{
			get
			{
				return this.AllowDelete;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.SupportsChangeNotification" />.</summary>
		/// <returns>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.SupportsChangeNotification" />.</returns>
		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000525 RID: 1317 RVA: 0x0000593A File Offset: 0x00003B3A
		bool IBindingList.SupportsChangeNotification
		{
			get
			{
				return true;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.SupportsSearching" />.</summary>
		/// <returns>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.SupportsSearching" />.</returns>
		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000526 RID: 1318 RVA: 0x0000593A File Offset: 0x00003B3A
		bool IBindingList.SupportsSearching
		{
			get
			{
				return true;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.SupportsSorting" />.</summary>
		/// <returns>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.SupportsSorting" />.</returns>
		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000527 RID: 1319 RVA: 0x0000593A File Offset: 0x00003B3A
		bool IBindingList.SupportsSorting
		{
			get
			{
				return true;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.IsSorted" />.</summary>
		/// <returns>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.IsSorted" />.</returns>
		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000528 RID: 1320 RVA: 0x00019495 File Offset: 0x00017695
		bool IBindingList.IsSorted
		{
			get
			{
				return this.Sort.Length != 0;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.SortProperty" />.</summary>
		/// <returns>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.SortProperty" />.</returns>
		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000529 RID: 1321 RVA: 0x000194A5 File Offset: 0x000176A5
		PropertyDescriptor IBindingList.SortProperty
		{
			get
			{
				return this.GetSortProperty();
			}
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x000194AD File Offset: 0x000176AD
		internal PropertyDescriptor GetSortProperty()
		{
			if (this._table != null && this._index != null && this._index._indexFields.Length == 1)
			{
				return new DataColumnPropertyDescriptor(this._index._indexFields[0].Column);
			}
			return null;
		}

		/// <summary>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.SortDirection" />.</summary>
		/// <returns>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.SortDirection" />.</returns>
		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x0600052B RID: 1323 RVA: 0x000194EC File Offset: 0x000176EC
		ListSortDirection IBindingList.SortDirection
		{
			get
			{
				if (this._index._indexFields.Length != 1 || !this._index._indexFields[0].IsDescending)
				{
					return ListSortDirection.Ascending;
				}
				return ListSortDirection.Descending;
			}
		}

		/// <summary>Occurs when the list managed by the <see cref="T:System.Data.DataView" /> changes.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x14000006 RID: 6
		// (add) Token: 0x0600052C RID: 1324 RVA: 0x00019519 File Offset: 0x00017719
		// (remove) Token: 0x0600052D RID: 1325 RVA: 0x00019547 File Offset: 0x00017747
		public event ListChangedEventHandler ListChanged
		{
			add
			{
				DataCommonEventSource.Log.Trace<int>("<ds.DataView.add_ListChanged|API> {0}", this.ObjectID);
				this._onListChanged = (ListChangedEventHandler)Delegate.Combine(this._onListChanged, value);
			}
			remove
			{
				DataCommonEventSource.Log.Trace<int>("<ds.DataView.remove_ListChanged|API> {0}", this.ObjectID);
				this._onListChanged = (ListChangedEventHandler)Delegate.Remove(this._onListChanged, value);
			}
		}

		/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.IBindingList.AddIndex(System.ComponentModel.PropertyDescriptor)" />.</summary>
		/// <param name="property">A <see cref="System.ComponentModel.PropertyDescriptor" /> object.</param>
		// Token: 0x0600052E RID: 1326 RVA: 0x00019575 File Offset: 0x00017775
		void IBindingList.AddIndex(PropertyDescriptor property)
		{
			this.GetFindIndex(property.Name, true);
		}

		/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.IBindingList.ApplySort(System.ComponentModel.PropertyDescriptor,System.ComponentModel.ListSortDirection)" />.</summary>
		/// <param name="property">A <see cref="System.ComponentModel.PropertyDescriptor" /> object.</param>
		/// <param name="direction">A <see cref="System.ComponentModel.ListSortDirection" /> object.</param>
		// Token: 0x0600052F RID: 1327 RVA: 0x00019585 File Offset: 0x00017785
		void IBindingList.ApplySort(PropertyDescriptor property, ListSortDirection direction)
		{
			this.Sort = this.CreateSortString(property, direction);
		}

		/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.IBindingList.Find(System.ComponentModel.PropertyDescriptor,System.Object)" />.</summary>
		/// <returns>For a description of this member, see <see cref="M:System.ComponentModel.IBindingList.Find(System.ComponentModel.PropertyDescriptor,System.Object)" />.</returns>
		/// <param name="property">A <see cref="System.ComponentModel.PropertyDescriptor" /> object.</param>
		/// <param name="key">A <see cref="System.Object" /> value.</param>
		// Token: 0x06000530 RID: 1328 RVA: 0x00019598 File Offset: 0x00017798
		int IBindingList.Find(PropertyDescriptor property, object key)
		{
			if (property != null)
			{
				bool flag = false;
				Index index = null;
				try
				{
					if (this._findIndexes == null || !this._findIndexes.TryGetValue(property.Name, out index))
					{
						flag = true;
						index = this._table.GetIndex(property.Name, this._recordStates, this.GetFilter());
						index.AddRef();
					}
					Range range = index.FindRecords(key);
					if (!range.IsNull)
					{
						return this._index.GetIndex(index.GetRecord(range.Min));
					}
				}
				finally
				{
					if (flag && index != null)
					{
						index.RemoveRef();
						if (index.RefCount == 1)
						{
							index.RemoveRef();
						}
					}
				}
				return -1;
			}
			return -1;
		}

		/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.IBindingList.RemoveIndex(System.ComponentModel.PropertyDescriptor)" />.</summary>
		/// <param name="property">A <see cref="System.ComponentModel.PropertyDescriptor" /> object.</param>
		// Token: 0x06000531 RID: 1329 RVA: 0x00019654 File Offset: 0x00017854
		void IBindingList.RemoveIndex(PropertyDescriptor property)
		{
			this.GetFindIndex(property.Name, false);
		}

		/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.IBindingList.RemoveSort" />.</summary>
		// Token: 0x06000532 RID: 1330 RVA: 0x00019664 File Offset: 0x00017864
		void IBindingList.RemoveSort()
		{
			DataCommonEventSource.Log.Trace<int>("<ds.DataView.RemoveSort|API> {0}", this.ObjectID);
			this.Sort = string.Empty;
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x00019688 File Offset: 0x00017888
		private string CreateSortString(PropertyDescriptor property, ListSortDirection direction)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append('[');
			stringBuilder.Append(property.Name);
			stringBuilder.Append(']');
			if (ListSortDirection.Descending == direction)
			{
				stringBuilder.Append(" DESC");
			}
			return stringBuilder.ToString();
		}

		/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.ITypedList.GetListName(System.ComponentModel.PropertyDescriptor[])" />.</summary>
		/// <returns>For a description of this member, see <see cref="M:System.ComponentModel.ITypedList.GetListName(System.ComponentModel.PropertyDescriptor[])" />.</returns>
		/// <param name="listAccessors">An array of <see cref="System.ComponentModel.PropertyDescriptor" /> objects.</param>
		// Token: 0x06000534 RID: 1332 RVA: 0x000196D0 File Offset: 0x000178D0
		string ITypedList.GetListName(PropertyDescriptor[] listAccessors)
		{
			if (this._table != null)
			{
				if (listAccessors == null || listAccessors.Length == 0)
				{
					return this._table.TableName;
				}
				DataSet dataSet = this._table.DataSet;
				if (dataSet != null)
				{
					DataTable dataTable = dataSet.FindTable(this._table, listAccessors, 0);
					if (dataTable != null)
					{
						return dataTable.TableName;
					}
				}
			}
			return string.Empty;
		}

		/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.ITypedList.GetItemProperties(System.ComponentModel.PropertyDescriptor[])" />.</summary>
		// Token: 0x06000535 RID: 1333 RVA: 0x00019728 File Offset: 0x00017928
		PropertyDescriptorCollection ITypedList.GetItemProperties(PropertyDescriptor[] listAccessors)
		{
			if (this._table != null)
			{
				if (listAccessors == null || listAccessors.Length == 0)
				{
					return this._table.GetPropertyDescriptorCollection(null);
				}
				DataSet dataSet = this._table.DataSet;
				if (dataSet == null)
				{
					return new PropertyDescriptorCollection(null);
				}
				DataTable dataTable = dataSet.FindTable(this._table, listAccessors, 0);
				if (dataTable != null)
				{
					return dataTable.GetPropertyDescriptorCollection(null);
				}
			}
			return new PropertyDescriptorCollection(null);
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x00019787 File Offset: 0x00017987
		internal virtual IFilter GetFilter()
		{
			return this._rowFilter;
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x0001978F File Offset: 0x0001798F
		private int GetRecord(int recordIndex)
		{
			if (this.Count <= recordIndex)
			{
				throw ExceptionBuilder.RowOutOfRange(recordIndex);
			}
			if (recordIndex != this._index.RecordCount)
			{
				return this._index.GetRecord(recordIndex);
			}
			return this._addNewRow.GetDefaultRecord();
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x000197C8 File Offset: 0x000179C8
		internal DataRow GetRow(int index)
		{
			int count = this.Count;
			if (count <= index)
			{
				throw ExceptionBuilder.GetElementIndex(index);
			}
			if (index == count - 1 && this._addNewRow != null)
			{
				return this._addNewRow;
			}
			return this._table._recordManager[this.GetRecord(index)];
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x00019813 File Offset: 0x00017A13
		private DataRowView GetRowView(int record)
		{
			return this.GetRowView(this._table._recordManager[record]);
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x0001982C File Offset: 0x00017A2C
		private DataRowView GetRowView(DataRow dr)
		{
			return this._rowViewCache[dr];
		}

		/// <summary>Occurs after a <see cref="T:System.Data.DataView" /> has been changed successfully.</summary>
		/// <param name="sender">The source of the event. </param>
		/// <param name="e">A <see cref="T:System.ComponentModel.ListChangedEventArgs" /> that contains the event data. </param>
		// Token: 0x0600053B RID: 1339 RVA: 0x0001983A File Offset: 0x00017A3A
		protected virtual void IndexListChanged(object sender, ListChangedEventArgs e)
		{
			if (e.ListChangedType != ListChangedType.Reset)
			{
				this.OnListChanged(e);
			}
			if (this._addNewRow != null && this._index.RecordCount == 0)
			{
				this.FinishAddNew(false);
			}
			if (e.ListChangedType == ListChangedType.Reset)
			{
				this.OnListChanged(e);
			}
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x00019878 File Offset: 0x00017A78
		internal void IndexListChangedInternal(ListChangedEventArgs e)
		{
			this._rowViewBuffer.Clear();
			if (ListChangedType.ItemAdded == e.ListChangedType && this._addNewMoved != null && this._addNewMoved.NewIndex != this._addNewMoved.OldIndex)
			{
				ListChangedEventArgs addNewMoved = this._addNewMoved;
				this._addNewMoved = null;
				this.IndexListChanged(this, addNewMoved);
			}
			this.IndexListChanged(this, e);
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x000198D8 File Offset: 0x00017AD8
		internal void MaintainDataView(ListChangedType changedType, DataRow row, bool trackAddRemove)
		{
			DataRowView dataRowView = null;
			switch (changedType)
			{
			case ListChangedType.Reset:
				this.ResetRowViewCache();
				break;
			case ListChangedType.ItemAdded:
				if (trackAddRemove && this._rowViewBuffer.TryGetValue(row, out dataRowView))
				{
					this._rowViewBuffer.Remove(row);
				}
				if (row == this._addNewRow)
				{
					int num = this.IndexOfDataRowView(this._rowViewCache[this._addNewRow]);
					this._addNewRow = null;
					this._addNewMoved = new ListChangedEventArgs(ListChangedType.ItemMoved, num, this.Count - 1);
					return;
				}
				if (!this._rowViewCache.ContainsKey(row))
				{
					this._rowViewCache.Add(row, dataRowView ?? new DataRowView(this, row));
					return;
				}
				break;
			case ListChangedType.ItemDeleted:
				if (trackAddRemove)
				{
					this._rowViewCache.TryGetValue(row, out dataRowView);
					if (dataRowView != null)
					{
						this._rowViewBuffer.Add(row, dataRowView);
					}
				}
				this._rowViewCache.Remove(row);
				return;
			case ListChangedType.ItemMoved:
			case ListChangedType.ItemChanged:
			case ListChangedType.PropertyDescriptorAdded:
			case ListChangedType.PropertyDescriptorDeleted:
			case ListChangedType.PropertyDescriptorChanged:
				break;
			default:
				return;
			}
		}

		/// <summary>Raises the <see cref="E:System.Data.DataView.ListChanged" /> event.</summary>
		/// <param name="e">A <see cref="T:System.ComponentModel.ListChangedEventArgs" /> that contains the event data. </param>
		// Token: 0x0600053E RID: 1342 RVA: 0x000199CC File Offset: 0x00017BCC
		protected virtual void OnListChanged(ListChangedEventArgs e)
		{
			DataCommonEventSource.Log.Trace<int, ListChangedType>("<ds.DataView.OnListChanged|INFO> {0}, ListChangedType={1}", this.ObjectID, e.ListChangedType);
			try
			{
				DataColumn dataColumn = null;
				string text = null;
				switch (e.ListChangedType)
				{
				case ListChangedType.ItemMoved:
				case ListChangedType.ItemChanged:
					if (0 <= e.NewIndex)
					{
						DataRow row = this.GetRow(e.NewIndex);
						if (row.HasPropertyChanged)
						{
							dataColumn = row.LastChangedColumn;
							text = ((dataColumn != null) ? dataColumn.ColumnName : string.Empty);
						}
					}
					break;
				}
				if (this._onListChanged != null)
				{
					if (dataColumn != null && e.NewIndex == e.OldIndex)
					{
						ListChangedEventArgs listChangedEventArgs = new ListChangedEventArgs(e.ListChangedType, e.NewIndex, new DataColumnPropertyDescriptor(dataColumn));
						this._onListChanged(this, listChangedEventArgs);
					}
					else
					{
						this._onListChanged(this, e);
					}
				}
				if (text != null)
				{
					this[e.NewIndex].RaisePropertyChangedEvent(text);
				}
			}
			catch (Exception ex) when (ADP.IsCatchableExceptionType(ex))
			{
				ExceptionBuilder.TraceExceptionWithoutRethrow(ex);
			}
		}

		/// <summary>Reserved for internal use only.</summary>
		// Token: 0x0600053F RID: 1343 RVA: 0x00019AF8 File Offset: 0x00017CF8
		protected void Reset()
		{
			if (this.IsOpen)
			{
				this._index.Reset();
			}
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x00019B10 File Offset: 0x00017D10
		internal void ResetRowViewCache()
		{
			Dictionary<DataRow, DataRowView> dictionary = new Dictionary<DataRow, DataRowView>(this.CountFromIndex, DataView.DataRowReferenceComparer.s_default);
			if (this._index != null)
			{
				RBTree<int>.RBTreeEnumerator enumerator = this._index.GetEnumerator(0);
				while (enumerator.MoveNext())
				{
					int num = enumerator.Current;
					DataRow dataRow = this._table._recordManager[num];
					DataRowView dataRowView;
					if (!this._rowViewCache.TryGetValue(dataRow, out dataRowView))
					{
						dataRowView = new DataRowView(this, dataRow);
					}
					dictionary.Add(dataRow, dataRowView);
				}
			}
			if (this._addNewRow != null)
			{
				DataRowView dataRowView;
				this._rowViewCache.TryGetValue(this._addNewRow, out dataRowView);
				dictionary.Add(this._addNewRow, dataRowView);
			}
			this._rowViewCache = dictionary;
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x00019BB8 File Offset: 0x00017DB8
		internal void SetDataViewManager(DataViewManager dataViewManager)
		{
			if (this._table == null)
			{
				throw ExceptionBuilder.CanNotUse();
			}
			if (this._dataViewManager != dataViewManager)
			{
				if (dataViewManager != null)
				{
					dataViewManager._nViews--;
				}
				this._dataViewManager = dataViewManager;
				if (dataViewManager != null)
				{
					dataViewManager._nViews++;
					DataViewSetting dataViewSetting = dataViewManager.DataViewSettings[this._table];
					try
					{
						this._applyDefaultSort = dataViewSetting.ApplyDefaultSort;
						DataExpression dataExpression = new DataExpression(this._table, dataViewSetting.RowFilter);
						this.SetIndex(dataViewSetting.Sort, dataViewSetting.RowStateFilter, dataExpression);
					}
					catch (Exception ex) when (ADP.IsCatchableExceptionType(ex))
					{
						ExceptionBuilder.TraceExceptionWithoutRethrow(ex);
					}
					this._locked = true;
					return;
				}
				this.SetIndex("", DataViewRowState.CurrentRows, null);
			}
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x00019C98 File Offset: 0x00017E98
		internal virtual void SetIndex(string newSort, DataViewRowState newRowStates, IFilter newRowFilter)
		{
			this.SetIndex2(newSort, newRowStates, newRowFilter, true);
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x00019CA4 File Offset: 0x00017EA4
		internal void SetIndex2(string newSort, DataViewRowState newRowStates, IFilter newRowFilter, bool fireEvent)
		{
			DataCommonEventSource.Log.Trace<int, string, DataViewRowState>("<ds.DataView.SetIndex|INFO> {0}, newSort='{1}', newRowStates={2}", this.ObjectID, newSort, newRowStates);
			this._sort = newSort;
			this._recordStates = newRowStates;
			this._rowFilter = newRowFilter;
			if (this._fEndInitInProgress)
			{
				return;
			}
			if (fireEvent)
			{
				this.UpdateIndex(true);
			}
			else
			{
				this.UpdateIndex(true, false);
			}
			if (this._findIndexes != null)
			{
				Dictionary<string, Index> findIndexes = this._findIndexes;
				this._findIndexes = null;
				foreach (KeyValuePair<string, Index> keyValuePair in findIndexes)
				{
					keyValuePair.Value.RemoveRef();
				}
			}
		}

		/// <summary>Reserved for internal use only.</summary>
		// Token: 0x06000544 RID: 1348 RVA: 0x00019D58 File Offset: 0x00017F58
		protected void UpdateIndex()
		{
			this.UpdateIndex(false);
		}

		/// <summary>Reserved for internal use only.</summary>
		/// <param name="force">Reserved for internal use only. </param>
		// Token: 0x06000545 RID: 1349 RVA: 0x00019D61 File Offset: 0x00017F61
		protected virtual void UpdateIndex(bool force)
		{
			this.UpdateIndex(force, true);
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x00019D6C File Offset: 0x00017F6C
		internal void UpdateIndex(bool force, bool fireEvent)
		{
			long num = DataCommonEventSource.Log.EnterScope<int, bool>("<ds.DataView.UpdateIndex|INFO> {0}, force={1}", this.ObjectID, force);
			try
			{
				if (this._open != this._shouldOpen || force)
				{
					this._open = this._shouldOpen;
					Index index = null;
					if (this._open && this._table != null)
					{
						if (this.SortComparison != null)
						{
							index = new Index(this._table, this.SortComparison, this._recordStates, this.GetFilter());
							index.AddRef();
						}
						else
						{
							index = this._table.GetIndex(this.Sort, this._recordStates, this.GetFilter());
						}
					}
					if (this._index != index)
					{
						if (this._index == null)
						{
							DataTable table = index.Table;
						}
						else
						{
							DataTable table2 = this._index.Table;
						}
						if (this._index != null)
						{
							this._dvListener.UnregisterListChangedEvent();
						}
						this._index = index;
						if (this._index != null)
						{
							this._dvListener.RegisterListChangedEvent(this._index);
						}
						this.ResetRowViewCache();
						if (fireEvent)
						{
							this.OnListChanged(DataView.s_resetEventArgs);
						}
					}
				}
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x00019EA0 File Offset: 0x000180A0
		internal void ChildRelationCollectionChanged(object sender, CollectionChangeEventArgs e)
		{
			DataRelationPropertyDescriptor dataRelationPropertyDescriptor = null;
			this.OnListChanged((e.Action == CollectionChangeAction.Add) ? new ListChangedEventArgs(ListChangedType.PropertyDescriptorAdded, new DataRelationPropertyDescriptor((DataRelation)e.Element)) : ((e.Action == CollectionChangeAction.Refresh) ? new ListChangedEventArgs(ListChangedType.PropertyDescriptorChanged, dataRelationPropertyDescriptor) : ((e.Action == CollectionChangeAction.Remove) ? new ListChangedEventArgs(ListChangedType.PropertyDescriptorDeleted, new DataRelationPropertyDescriptor((DataRelation)e.Element)) : null)));
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x00019F0C File Offset: 0x0001810C
		internal void ParentRelationCollectionChanged(object sender, CollectionChangeEventArgs e)
		{
			DataRelationPropertyDescriptor dataRelationPropertyDescriptor = null;
			this.OnListChanged((e.Action == CollectionChangeAction.Add) ? new ListChangedEventArgs(ListChangedType.PropertyDescriptorAdded, new DataRelationPropertyDescriptor((DataRelation)e.Element)) : ((e.Action == CollectionChangeAction.Refresh) ? new ListChangedEventArgs(ListChangedType.PropertyDescriptorChanged, dataRelationPropertyDescriptor) : ((e.Action == CollectionChangeAction.Remove) ? new ListChangedEventArgs(ListChangedType.PropertyDescriptorDeleted, new DataRelationPropertyDescriptor((DataRelation)e.Element)) : null)));
		}

		/// <summary>Occurs after a <see cref="T:System.Data.DataColumnCollection" /> has been changed successfully.</summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">A <see cref="T:System.ComponentModel.ListChangedEventArgs" /> that contains the event data. </param>
		// Token: 0x06000549 RID: 1353 RVA: 0x00019F78 File Offset: 0x00018178
		protected virtual void ColumnCollectionChanged(object sender, CollectionChangeEventArgs e)
		{
			DataColumnPropertyDescriptor dataColumnPropertyDescriptor = null;
			this.OnListChanged((e.Action == CollectionChangeAction.Add) ? new ListChangedEventArgs(ListChangedType.PropertyDescriptorAdded, new DataColumnPropertyDescriptor((DataColumn)e.Element)) : ((e.Action == CollectionChangeAction.Refresh) ? new ListChangedEventArgs(ListChangedType.PropertyDescriptorChanged, dataColumnPropertyDescriptor) : ((e.Action == CollectionChangeAction.Remove) ? new ListChangedEventArgs(ListChangedType.PropertyDescriptorDeleted, new DataColumnPropertyDescriptor((DataColumn)e.Element)) : null)));
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x00019FE2 File Offset: 0x000181E2
		internal void ColumnCollectionChangedInternal(object sender, CollectionChangeEventArgs e)
		{
			this.ColumnCollectionChanged(sender, e);
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x0600054B RID: 1355 RVA: 0x00019FEC File Offset: 0x000181EC
		internal int ObjectID
		{
			get
			{
				return this._objectID;
			}
		}

		// Token: 0x04000190 RID: 400
		private DataViewManager _dataViewManager;

		// Token: 0x04000191 RID: 401
		private DataTable _table;

		// Token: 0x04000192 RID: 402
		private bool _locked;

		// Token: 0x04000193 RID: 403
		private Index _index;

		// Token: 0x04000194 RID: 404
		private Dictionary<string, Index> _findIndexes;

		// Token: 0x04000195 RID: 405
		private string _sort = string.Empty;

		// Token: 0x04000196 RID: 406
		private Comparison<DataRow> _comparison;

		// Token: 0x04000197 RID: 407
		private IFilter _rowFilter;

		// Token: 0x04000198 RID: 408
		private DataViewRowState _recordStates = DataViewRowState.CurrentRows;

		// Token: 0x04000199 RID: 409
		private bool _shouldOpen = true;

		// Token: 0x0400019A RID: 410
		private bool _open;

		// Token: 0x0400019B RID: 411
		private bool _allowNew = true;

		// Token: 0x0400019C RID: 412
		private bool _allowEdit = true;

		// Token: 0x0400019D RID: 413
		private bool _allowDelete = true;

		// Token: 0x0400019E RID: 414
		private bool _applyDefaultSort;

		// Token: 0x0400019F RID: 415
		internal DataRow _addNewRow;

		// Token: 0x040001A0 RID: 416
		private ListChangedEventArgs _addNewMoved;

		// Token: 0x040001A1 RID: 417
		private ListChangedEventHandler _onListChanged;

		// Token: 0x040001A2 RID: 418
		internal static ListChangedEventArgs s_resetEventArgs = new ListChangedEventArgs(ListChangedType.Reset, -1);

		// Token: 0x040001A3 RID: 419
		private string _delayedSort;

		// Token: 0x040001A4 RID: 420
		private DataViewRowState _delayedRecordStates = (DataViewRowState)(-1);

		// Token: 0x040001A5 RID: 421
		private bool _fInitInProgress;

		// Token: 0x040001A6 RID: 422
		private bool _fEndInitInProgress;

		// Token: 0x040001A7 RID: 423
		private Dictionary<DataRow, DataRowView> _rowViewCache = new Dictionary<DataRow, DataRowView>(DataView.DataRowReferenceComparer.s_default);

		// Token: 0x040001A8 RID: 424
		private readonly Dictionary<DataRow, DataRowView> _rowViewBuffer = new Dictionary<DataRow, DataRowView>(DataView.DataRowReferenceComparer.s_default);

		// Token: 0x040001A9 RID: 425
		private DataViewListener _dvListener;

		// Token: 0x040001AA RID: 426
		private static int s_objectTypeCount;

		// Token: 0x040001AB RID: 427
		private readonly int _objectID = Interlocked.Increment(ref DataView.s_objectTypeCount);

		// Token: 0x02000056 RID: 86
		private sealed class DataRowReferenceComparer : IEqualityComparer<DataRow>
		{
			// Token: 0x0600054D RID: 1357 RVA: 0x00002156 File Offset: 0x00000356
			private DataRowReferenceComparer()
			{
			}

			// Token: 0x0600054E RID: 1358 RVA: 0x0001A002 File Offset: 0x00018202
			public bool Equals(DataRow x, DataRow y)
			{
				return x == y;
			}

			// Token: 0x0600054F RID: 1359 RVA: 0x0001A008 File Offset: 0x00018208
			public int GetHashCode(DataRow obj)
			{
				return obj._objectID;
			}

			// Token: 0x040001AC RID: 428
			internal static readonly DataView.DataRowReferenceComparer s_default = new DataView.DataRowReferenceComparer();
		}
	}
}
