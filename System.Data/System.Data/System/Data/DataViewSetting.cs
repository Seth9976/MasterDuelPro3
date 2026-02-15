using System;
using System.ComponentModel;

namespace System.Data
{
	/// <summary>Represents the default settings for <see cref="P:System.Data.DataView.ApplyDefaultSort" />, <see cref="P:System.Data.DataView.DataViewManager" />, <see cref="P:System.Data.DataView.RowFilter" />, <see cref="P:System.Data.DataView.RowStateFilter" />, <see cref="P:System.Data.DataView.Sort" />, and <see cref="P:System.Data.DataView.Table" /> for DataViews created from the <see cref="T:System.Data.DataViewManager" />.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200005B RID: 91
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class DataViewSetting
	{
		// Token: 0x06000597 RID: 1431 RVA: 0x0001A7CB File Offset: 0x000189CB
		internal DataViewSetting()
		{
		}

		/// <summary>Gets or sets a value indicating whether to use the default sort.</summary>
		/// <returns>true if the default sort is used; otherwise false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000598 RID: 1432 RVA: 0x0001A7F1 File Offset: 0x000189F1
		public bool ApplyDefaultSort
		{
			get
			{
				return this._applyDefaultSort;
			}
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x0001A7F9 File Offset: 0x000189F9
		internal void SetDataViewManager(DataViewManager dataViewManager)
		{
			if (this._dataViewManager != dataViewManager)
			{
				this._dataViewManager = dataViewManager;
			}
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x0001A80B File Offset: 0x00018A0B
		internal void SetDataTable(DataTable table)
		{
			if (this._table != table)
			{
				this._table = table;
			}
		}

		/// <summary>Gets or sets the filter to apply in the <see cref="T:System.Data.DataView" />. See <see cref="P:System.Data.DataView.RowFilter" /> for a code sample using RowFilter.</summary>
		/// <returns>A string that contains the filter to apply.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000FC RID: 252
		// (get) Token: 0x0600059B RID: 1435 RVA: 0x0001A81D File Offset: 0x00018A1D
		public string RowFilter
		{
			get
			{
				return this._rowFilter;
			}
		}

		/// <summary>Gets or sets a value indicating whether to display Current, Deleted, Modified Current, ModifiedOriginal, New, Original, Unchanged, or no rows in the <see cref="T:System.Data.DataView" />.</summary>
		/// <returns>A value that indicates which rows to display.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000FD RID: 253
		// (get) Token: 0x0600059C RID: 1436 RVA: 0x0001A825 File Offset: 0x00018A25
		public DataViewRowState RowStateFilter
		{
			get
			{
				return this._rowStateFilter;
			}
		}

		/// <summary>Gets or sets a value indicating the sort to apply in the <see cref="T:System.Data.DataView" />. </summary>
		/// <returns>The sort to apply in the <see cref="T:System.Data.DataView" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000FE RID: 254
		// (get) Token: 0x0600059D RID: 1437 RVA: 0x0001A82D File Offset: 0x00018A2D
		public string Sort
		{
			get
			{
				return this._sort;
			}
		}

		// Token: 0x040001C3 RID: 451
		private DataViewManager _dataViewManager;

		// Token: 0x040001C4 RID: 452
		private DataTable _table;

		// Token: 0x040001C5 RID: 453
		private string _sort = string.Empty;

		// Token: 0x040001C6 RID: 454
		private string _rowFilter = string.Empty;

		// Token: 0x040001C7 RID: 455
		private DataViewRowState _rowStateFilter = DataViewRowState.CurrentRows;

		// Token: 0x040001C8 RID: 456
		private bool _applyDefaultSort;
	}
}
