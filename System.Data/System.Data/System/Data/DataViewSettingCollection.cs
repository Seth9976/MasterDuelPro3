using System;
using System.Collections;
using System.ComponentModel;

namespace System.Data
{
	/// <summary>Contains a read-only collection of <see cref="T:System.Data.DataViewSetting" /> objects for each <see cref="T:System.Data.DataTable" /> in a <see cref="T:System.Data.DataSet" />.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200005C RID: 92
	public class DataViewSettingCollection : ICollection, IEnumerable
	{
		// Token: 0x0600059E RID: 1438 RVA: 0x0001A835 File Offset: 0x00018A35
		internal DataViewSettingCollection(DataViewManager dataViewManager)
		{
			if (dataViewManager == null)
			{
				throw ExceptionBuilder.ArgumentNull("dataViewManager");
			}
			this._dataViewManager = dataViewManager;
		}

		/// <summary>Gets the <see cref="T:System.Data.DataViewSetting" /> objects of the specified <see cref="T:System.Data.DataTable" /> from the collection. </summary>
		/// <returns>A collection of <see cref="T:System.Data.DataViewSetting" /> objects.</returns>
		/// <param name="table">The <see cref="T:System.Data.DataTable" /> to find. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x170000FF RID: 255
		public virtual DataViewSetting this[DataTable table]
		{
			get
			{
				if (table == null)
				{
					throw ExceptionBuilder.ArgumentNull("table");
				}
				DataViewSetting dataViewSetting = (DataViewSetting)this._list[table];
				if (dataViewSetting == null)
				{
					dataViewSetting = new DataViewSetting();
					this[table] = dataViewSetting;
				}
				return dataViewSetting;
			}
			set
			{
				if (table == null)
				{
					throw ExceptionBuilder.ArgumentNull("table");
				}
				value.SetDataViewManager(this._dataViewManager);
				value.SetDataTable(table);
				this._list[table] = value;
			}
		}

		/// <summary>Copies the collection objects to a one-dimensional <see cref="T:System.Array" /> instance starting at the specified index.</summary>
		/// <param name="ar">The one-dimensional <see cref="T:System.Array" /> that is the destination of the values copied from the collection. </param>
		/// <param name="index">The index of the array at which to start inserting. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060005A1 RID: 1441 RVA: 0x0001A8D0 File Offset: 0x00018AD0
		public void CopyTo(Array ar, int index)
		{
			foreach (object obj in this)
			{
				ar.SetValue(obj, index++);
			}
		}

		/// <summary>Gets the number of <see cref="T:System.Data.DataViewSetting" /> objects in the <see cref="T:System.Data.DataViewSettingCollection" />.</summary>
		/// <returns>The number of <see cref="T:System.Data.DataViewSetting" /> objects in the collection.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000100 RID: 256
		// (get) Token: 0x060005A2 RID: 1442 RVA: 0x0001A900 File Offset: 0x00018B00
		[Browsable(false)]
		public virtual int Count
		{
			get
			{
				DataSet dataSet = this._dataViewManager.DataSet;
				if (dataSet != null)
				{
					return dataSet.Tables.Count;
				}
				return 0;
			}
		}

		/// <summary>Gets an <see cref="T:System.Collections.IEnumerator" /> for the collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060005A3 RID: 1443 RVA: 0x0001A929 File Offset: 0x00018B29
		public IEnumerator GetEnumerator()
		{
			return new DataViewSettingCollection.DataViewSettingsEnumerator(this._dataViewManager);
		}

		/// <summary>Gets a value that indicates whether access to the <see cref="T:System.Data.DataViewSettingCollection" /> is synchronized (thread-safe).</summary>
		/// <returns>This property is always false, unless overridden by a derived class.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000101 RID: 257
		// (get) Token: 0x060005A4 RID: 1444 RVA: 0x00011ED5 File Offset: 0x000100D5
		[Browsable(false)]
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Data.DataViewSettingCollection" />.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Data.DataViewSettingCollection" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000102 RID: 258
		// (get) Token: 0x060005A5 RID: 1445 RVA: 0x0000207F File Offset: 0x0000027F
		[Browsable(false)]
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x060005A6 RID: 1446 RVA: 0x0001A936 File Offset: 0x00018B36
		internal void Remove(DataTable table)
		{
			this._list.Remove(table);
		}

		// Token: 0x040001C9 RID: 457
		private readonly DataViewManager _dataViewManager;

		// Token: 0x040001CA RID: 458
		private readonly Hashtable _list = new Hashtable();

		// Token: 0x0200005D RID: 93
		private sealed class DataViewSettingsEnumerator : IEnumerator
		{
			// Token: 0x060005A7 RID: 1447 RVA: 0x0001A944 File Offset: 0x00018B44
			public DataViewSettingsEnumerator(DataViewManager dvm)
			{
				if (dvm.DataSet != null)
				{
					this._dataViewSettings = dvm.DataViewSettings;
					this._tableEnumerator = dvm.DataSet.Tables.GetEnumerator();
					return;
				}
				this._dataViewSettings = null;
				this._tableEnumerator = Array.Empty<DataTable>().GetEnumerator();
			}

			// Token: 0x060005A8 RID: 1448 RVA: 0x0001A999 File Offset: 0x00018B99
			public bool MoveNext()
			{
				return this._tableEnumerator.MoveNext();
			}

			// Token: 0x060005A9 RID: 1449 RVA: 0x0001A9A6 File Offset: 0x00018BA6
			public void Reset()
			{
				this._tableEnumerator.Reset();
			}

			// Token: 0x17000103 RID: 259
			// (get) Token: 0x060005AA RID: 1450 RVA: 0x0001A9B3 File Offset: 0x00018BB3
			public object Current
			{
				get
				{
					return this._dataViewSettings[(DataTable)this._tableEnumerator.Current];
				}
			}

			// Token: 0x040001CB RID: 459
			private DataViewSettingCollection _dataViewSettings;

			// Token: 0x040001CC RID: 460
			private IEnumerator _tableEnumerator;
		}
	}
}
