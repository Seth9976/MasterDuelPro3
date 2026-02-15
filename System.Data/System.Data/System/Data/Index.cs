using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;

namespace System.Data
{
	// Token: 0x02000099 RID: 153
	internal sealed class Index
	{
		// Token: 0x06000777 RID: 1911 RVA: 0x000265F4 File Offset: 0x000247F4
		public Index(DataTable table, IndexField[] indexFields, DataViewRowState recordStates, IFilter rowFilter)
			: this(table, indexFields, null, recordStates, rowFilter)
		{
		}

		// Token: 0x06000778 RID: 1912 RVA: 0x00026602 File Offset: 0x00024802
		public Index(DataTable table, Comparison<DataRow> comparison, DataViewRowState recordStates, IFilter rowFilter)
			: this(table, Index.GetAllFields(table.Columns), comparison, recordStates, rowFilter)
		{
		}

		// Token: 0x06000779 RID: 1913 RVA: 0x0002661C File Offset: 0x0002481C
		private static IndexField[] GetAllFields(DataColumnCollection columns)
		{
			IndexField[] array = new IndexField[columns.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = new IndexField(columns[i], false);
			}
			return array;
		}

		// Token: 0x0600077A RID: 1914 RVA: 0x00026658 File Offset: 0x00024858
		private Index(DataTable table, IndexField[] indexFields, Comparison<DataRow> comparison, DataViewRowState recordStates, IFilter rowFilter)
		{
			DataCommonEventSource.Log.Trace<int, int, DataViewRowState>("<ds.Index.Index|API> {0}, table={1}, recordStates={2}", this.ObjectID, (table != null) ? table.ObjectID : 0, recordStates);
			if ((recordStates & ~(DataViewRowState.Unchanged | DataViewRowState.Added | DataViewRowState.Deleted | DataViewRowState.ModifiedCurrent | DataViewRowState.ModifiedOriginal)) != DataViewRowState.None)
			{
				throw ExceptionBuilder.RecordStateRange();
			}
			this._table = table;
			this._listeners = new Listeners<DataViewListener>(this.ObjectID, (DataViewListener listener) => listener != null);
			this._indexFields = indexFields;
			this._recordStates = recordStates;
			this._comparison = comparison;
			DataColumnCollection columns = table.Columns;
			this._isSharable = rowFilter == null && comparison == null;
			if (rowFilter != null)
			{
				this._rowFilter = new WeakReference(rowFilter);
				DataExpression dataExpression = rowFilter as DataExpression;
				if (dataExpression != null)
				{
					this._hasRemoteAggregate = dataExpression.HasRemoteAggregate();
				}
			}
			this.InitRecords(rowFilter);
		}

		// Token: 0x0600077B RID: 1915 RVA: 0x00026740 File Offset: 0x00024940
		public bool Equal(IndexField[] indexDesc, DataViewRowState recordStates, IFilter rowFilter)
		{
			if (!this._isSharable || this._indexFields.Length != indexDesc.Length || this._recordStates != recordStates || rowFilter != null)
			{
				return false;
			}
			for (int i = 0; i < this._indexFields.Length; i++)
			{
				if (this._indexFields[i].Column != indexDesc[i].Column || this._indexFields[i].IsDescending != indexDesc[i].IsDescending)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x0600077C RID: 1916 RVA: 0x000267C4 File Offset: 0x000249C4
		internal bool HasRemoteAggregate
		{
			get
			{
				return this._hasRemoteAggregate;
			}
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x0600077D RID: 1917 RVA: 0x000267CC File Offset: 0x000249CC
		internal int ObjectID
		{
			get
			{
				return this._objectID;
			}
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x0600077E RID: 1918 RVA: 0x000267D4 File Offset: 0x000249D4
		public DataViewRowState RecordStates
		{
			get
			{
				return this._recordStates;
			}
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x0600077F RID: 1919 RVA: 0x000267DC File Offset: 0x000249DC
		public IFilter RowFilter
		{
			get
			{
				return (IFilter)((this._rowFilter != null) ? this._rowFilter.Target : null);
			}
		}

		// Token: 0x06000780 RID: 1920 RVA: 0x000267F9 File Offset: 0x000249F9
		public int GetRecord(int recordIndex)
		{
			return this._records[recordIndex];
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000781 RID: 1921 RVA: 0x00026807 File Offset: 0x00024A07
		public bool HasDuplicates
		{
			get
			{
				return this._records.HasDuplicates;
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000782 RID: 1922 RVA: 0x00026814 File Offset: 0x00024A14
		public int RecordCount
		{
			get
			{
				return this._recordCount;
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x06000783 RID: 1923 RVA: 0x0002681C File Offset: 0x00024A1C
		public bool IsSharable
		{
			get
			{
				return this._isSharable;
			}
		}

		// Token: 0x06000784 RID: 1924 RVA: 0x00026824 File Offset: 0x00024A24
		private bool AcceptRecord(int record)
		{
			return this.AcceptRecord(record, this.RowFilter);
		}

		// Token: 0x06000785 RID: 1925 RVA: 0x00026834 File Offset: 0x00024A34
		private bool AcceptRecord(int record, IFilter filter)
		{
			DataCommonEventSource.Log.Trace<int, int>("<ds.Index.AcceptRecord|API> {0}, record={1}", this.ObjectID, record);
			if (filter == null)
			{
				return true;
			}
			DataRow dataRow = this._table._recordManager[record];
			if (dataRow == null)
			{
				return true;
			}
			DataRowVersion dataRowVersion = DataRowVersion.Default;
			if (dataRow._oldRecord == record)
			{
				dataRowVersion = DataRowVersion.Original;
			}
			else if (dataRow._newRecord == record)
			{
				dataRowVersion = DataRowVersion.Current;
			}
			else if (dataRow._tempRecord == record)
			{
				dataRowVersion = DataRowVersion.Proposed;
			}
			return filter.Invoke(dataRow, dataRowVersion);
		}

		// Token: 0x06000786 RID: 1926 RVA: 0x000268B2 File Offset: 0x00024AB2
		internal void ListChangedAdd(DataViewListener listener)
		{
			this._listeners.Add(listener);
		}

		// Token: 0x06000787 RID: 1927 RVA: 0x000268C0 File Offset: 0x00024AC0
		internal void ListChangedRemove(DataViewListener listener)
		{
			this._listeners.Remove(listener);
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x06000788 RID: 1928 RVA: 0x000268CE File Offset: 0x00024ACE
		public int RefCount
		{
			get
			{
				return this._refCount;
			}
		}

		// Token: 0x06000789 RID: 1929 RVA: 0x000268D8 File Offset: 0x00024AD8
		public void AddRef()
		{
			DataCommonEventSource.Log.Trace<int>("<ds.Index.AddRef|API> {0}", this.ObjectID);
			this._table._indexesLock.EnterWriteLock();
			try
			{
				if (this._refCount == 0)
				{
					this._table.ShadowIndexCopy();
					this._table._indexes.Add(this);
				}
				this._refCount++;
			}
			finally
			{
				this._table._indexesLock.ExitWriteLock();
			}
		}

		// Token: 0x0600078A RID: 1930 RVA: 0x00026960 File Offset: 0x00024B60
		public int RemoveRef()
		{
			DataCommonEventSource.Log.Trace<int>("<ds.Index.RemoveRef|API> {0}", this.ObjectID);
			this._table._indexesLock.EnterWriteLock();
			int num2;
			try
			{
				int num = this._refCount - 1;
				this._refCount = num;
				num2 = num;
				if (this._refCount <= 0)
				{
					this._table.ShadowIndexCopy();
					this._table._indexes.Remove(this);
				}
			}
			finally
			{
				this._table._indexesLock.ExitWriteLock();
			}
			return num2;
		}

		// Token: 0x0600078B RID: 1931 RVA: 0x000269F0 File Offset: 0x00024BF0
		private void ApplyChangeAction(int record, int action, int changeRecord)
		{
			if (action != 0)
			{
				if (action > 0)
				{
					if (this.AcceptRecord(record))
					{
						this.InsertRecord(record, true);
						return;
					}
				}
				else
				{
					if (this._comparison != null && -1 != record)
					{
						this.DeleteRecord(this.GetIndex(record, changeRecord));
						return;
					}
					this.DeleteRecord(this.GetIndex(record));
				}
			}
		}

		// Token: 0x0600078C RID: 1932 RVA: 0x00026A3F File Offset: 0x00024C3F
		public bool CheckUnique()
		{
			return !this.HasDuplicates;
		}

		// Token: 0x0600078D RID: 1933 RVA: 0x00026A4C File Offset: 0x00024C4C
		private int CompareRecords(int record1, int record2)
		{
			if (this._comparison != null)
			{
				return this.CompareDataRows(record1, record2);
			}
			if (this._indexFields.Length != 0)
			{
				int i = 0;
				while (i < this._indexFields.Length)
				{
					int num = this._indexFields[i].Column.Compare(record1, record2);
					if (num != 0)
					{
						if (!this._indexFields[i].IsDescending)
						{
							return num;
						}
						return -num;
					}
					else
					{
						i++;
					}
				}
				return 0;
			}
			return this._table.Rows.IndexOf(this._table._recordManager[record1]).CompareTo(this._table.Rows.IndexOf(this._table._recordManager[record2]));
		}

		// Token: 0x0600078E RID: 1934 RVA: 0x00026B06 File Offset: 0x00024D06
		private int CompareDataRows(int record1, int record2)
		{
			return this._comparison(this._table._recordManager[record1], this._table._recordManager[record2]);
		}

		// Token: 0x0600078F RID: 1935 RVA: 0x00026B38 File Offset: 0x00024D38
		private int CompareDuplicateRecords(int record1, int record2)
		{
			if (this._table._recordManager[record1] == null)
			{
				if (this._table._recordManager[record2] != null)
				{
					return -1;
				}
				return 0;
			}
			else
			{
				if (this._table._recordManager[record2] == null)
				{
					return 1;
				}
				int num = this._table._recordManager[record1].rowID.CompareTo(this._table._recordManager[record2].rowID);
				if (num == 0 && record1 != record2)
				{
					num = ((int)this._table._recordManager[record1].GetRecordState(record1)).CompareTo((int)this._table._recordManager[record2].GetRecordState(record2));
				}
				return num;
			}
		}

		// Token: 0x06000790 RID: 1936 RVA: 0x00026BF8 File Offset: 0x00024DF8
		private int CompareRecordToKey(int record1, object[] vals)
		{
			int i = 0;
			while (i < this._indexFields.Length)
			{
				int num = this._indexFields[i].Column.CompareValueTo(record1, vals[i]);
				if (num != 0)
				{
					if (!this._indexFields[i].IsDescending)
					{
						return num;
					}
					return -num;
				}
				else
				{
					i++;
				}
			}
			return 0;
		}

		// Token: 0x06000791 RID: 1937 RVA: 0x00026C4F File Offset: 0x00024E4F
		public void DeleteRecordFromIndex(int recordIndex)
		{
			this.DeleteRecord(recordIndex, false);
		}

		// Token: 0x06000792 RID: 1938 RVA: 0x00026C59 File Offset: 0x00024E59
		private void DeleteRecord(int recordIndex)
		{
			this.DeleteRecord(recordIndex, true);
		}

		// Token: 0x06000793 RID: 1939 RVA: 0x00026C64 File Offset: 0x00024E64
		private void DeleteRecord(int recordIndex, bool fireEvent)
		{
			DataCommonEventSource.Log.Trace<int, int, bool>("<ds.Index.DeleteRecord|INFO> {0}, recordIndex={1}, fireEvent={2}", this.ObjectID, recordIndex, fireEvent);
			if (recordIndex >= 0)
			{
				this._recordCount--;
				int num = this._records.DeleteByIndex(recordIndex);
				this.MaintainDataView(ListChangedType.ItemDeleted, num, !fireEvent);
				if (fireEvent)
				{
					this.OnListChanged(ListChangedType.ItemDeleted, recordIndex);
				}
			}
		}

		// Token: 0x06000794 RID: 1940 RVA: 0x00026CBE File Offset: 0x00024EBE
		public RBTree<int>.RBTreeEnumerator GetEnumerator(int startIndex)
		{
			return new RBTree<int>.RBTreeEnumerator(this._records, startIndex);
		}

		// Token: 0x06000795 RID: 1941 RVA: 0x00026CCC File Offset: 0x00024ECC
		public int GetIndex(int record)
		{
			return this._records.GetIndexByKey(record);
		}

		// Token: 0x06000796 RID: 1942 RVA: 0x00026CDC File Offset: 0x00024EDC
		private int GetIndex(int record, int changeRecord)
		{
			DataRow dataRow = this._table._recordManager[record];
			int newRecord = dataRow._newRecord;
			int oldRecord = dataRow._oldRecord;
			int indexByKey;
			try
			{
				if (changeRecord != 1)
				{
					if (changeRecord == 2)
					{
						dataRow._oldRecord = record;
					}
				}
				else
				{
					dataRow._newRecord = record;
				}
				indexByKey = this._records.GetIndexByKey(record);
			}
			finally
			{
				if (changeRecord != 1)
				{
					if (changeRecord == 2)
					{
						dataRow._oldRecord = oldRecord;
					}
				}
				else
				{
					dataRow._newRecord = newRecord;
				}
			}
			return indexByKey;
		}

		// Token: 0x06000797 RID: 1943 RVA: 0x00026D60 File Offset: 0x00024F60
		public object[] GetUniqueKeyValues()
		{
			if (this._indexFields == null || this._indexFields.Length == 0)
			{
				return Array.Empty<object>();
			}
			List<object[]> list = new List<object[]>();
			this.GetUniqueKeyValues(list, this._records.root);
			return list.ToArray();
		}

		// Token: 0x06000798 RID: 1944 RVA: 0x00026DA4 File Offset: 0x00024FA4
		private int FindNodeByKey(object originalKey)
		{
			if (this._indexFields.Length != 1)
			{
				throw ExceptionBuilder.IndexKeyLength(this._indexFields.Length, 1);
			}
			int num = this._records.root;
			if (num != 0)
			{
				DataColumn column = this._indexFields[0].Column;
				object obj = column.ConvertValue(originalKey);
				num = this._records.root;
				if (this._indexFields[0].IsDescending)
				{
					while (num != 0)
					{
						int num2 = column.CompareValueTo(this._records.Key(num), obj);
						if (num2 == 0)
						{
							break;
						}
						if (num2 < 0)
						{
							num = this._records.Left(num);
						}
						else
						{
							num = this._records.Right(num);
						}
					}
				}
				else
				{
					while (num != 0)
					{
						int num2 = column.CompareValueTo(this._records.Key(num), obj);
						if (num2 == 0)
						{
							break;
						}
						if (num2 > 0)
						{
							num = this._records.Left(num);
						}
						else
						{
							num = this._records.Right(num);
						}
					}
				}
			}
			return num;
		}

		// Token: 0x06000799 RID: 1945 RVA: 0x00026E90 File Offset: 0x00025090
		private int FindNodeByKeys(object[] originalKey)
		{
			int num = ((originalKey != null) ? originalKey.Length : 0);
			if (num == 0 || this._indexFields.Length != num)
			{
				throw ExceptionBuilder.IndexKeyLength(this._indexFields.Length, num);
			}
			int num2 = this._records.root;
			if (num2 != 0)
			{
				object[] array = new object[originalKey.Length];
				for (int i = 0; i < originalKey.Length; i++)
				{
					array[i] = this._indexFields[i].Column.ConvertValue(originalKey[i]);
				}
				num2 = this._records.root;
				while (num2 != 0)
				{
					num = this.CompareRecordToKey(this._records.Key(num2), array);
					if (num == 0)
					{
						break;
					}
					if (num > 0)
					{
						num2 = this._records.Left(num2);
					}
					else
					{
						num2 = this._records.Right(num2);
					}
				}
			}
			return num2;
		}

		// Token: 0x0600079A RID: 1946 RVA: 0x00026F50 File Offset: 0x00025150
		private int FindNodeByKeyRecord(int record)
		{
			int num = this._records.root;
			if (num != 0)
			{
				num = this._records.root;
				while (num != 0)
				{
					int num2 = this.CompareRecords(this._records.Key(num), record);
					if (num2 == 0)
					{
						break;
					}
					if (num2 > 0)
					{
						num = this._records.Left(num);
					}
					else
					{
						num = this._records.Right(num);
					}
				}
			}
			return num;
		}

		// Token: 0x0600079B RID: 1947 RVA: 0x00026FB8 File Offset: 0x000251B8
		private Range GetRangeFromNode(int nodeId)
		{
			if (nodeId == 0)
			{
				return default(Range);
			}
			int indexByNode = this._records.GetIndexByNode(nodeId);
			if (this._records.Next(nodeId) == 0)
			{
				return new Range(indexByNode, indexByNode);
			}
			int num = this._records.SubTreeSize(this._records.Next(nodeId));
			return new Range(indexByNode, indexByNode + num - 1);
		}

		// Token: 0x0600079C RID: 1948 RVA: 0x00027018 File Offset: 0x00025218
		public Range FindRecords(object key)
		{
			int num = this.FindNodeByKey(key);
			return this.GetRangeFromNode(num);
		}

		// Token: 0x0600079D RID: 1949 RVA: 0x00027034 File Offset: 0x00025234
		public Range FindRecords(object[] key)
		{
			int num = this.FindNodeByKeys(key);
			return this.GetRangeFromNode(num);
		}

		// Token: 0x0600079E RID: 1950 RVA: 0x00027050 File Offset: 0x00025250
		internal void FireResetEvent()
		{
			DataCommonEventSource.Log.Trace<int>("<ds.Index.FireResetEvent|API> {0}", this.ObjectID);
			if (this.DoListChanged)
			{
				this.OnListChanged(DataView.s_resetEventArgs);
			}
		}

		// Token: 0x0600079F RID: 1951 RVA: 0x0002707C File Offset: 0x0002527C
		private int GetChangeAction(DataViewRowState oldState, DataViewRowState newState)
		{
			int num = (((this._recordStates & oldState) == DataViewRowState.None) ? 0 : 1);
			return (((this._recordStates & newState) == DataViewRowState.None) ? 0 : 1) - num;
		}

		// Token: 0x060007A0 RID: 1952 RVA: 0x000270A8 File Offset: 0x000252A8
		private static int GetReplaceAction(DataViewRowState oldState)
		{
			if ((DataViewRowState.CurrentRows & oldState) != DataViewRowState.None)
			{
				return 1;
			}
			if ((DataViewRowState.OriginalRows & oldState) == DataViewRowState.None)
			{
				return 0;
			}
			return 2;
		}

		// Token: 0x060007A1 RID: 1953 RVA: 0x000270BB File Offset: 0x000252BB
		public DataRow GetRow(int i)
		{
			return this._table._recordManager[this.GetRecord(i)];
		}

		// Token: 0x060007A2 RID: 1954 RVA: 0x000270D4 File Offset: 0x000252D4
		public DataRow[] GetRows(object[] values)
		{
			return this.GetRows(this.FindRecords(values));
		}

		// Token: 0x060007A3 RID: 1955 RVA: 0x000270E4 File Offset: 0x000252E4
		public DataRow[] GetRows(Range range)
		{
			DataRow[] array = this._table.NewRowArray(range.Count);
			if (array.Length != 0)
			{
				RBTree<int>.RBTreeEnumerator enumerator = this.GetEnumerator(range.Min);
				int num = 0;
				while (num < array.Length && enumerator.MoveNext())
				{
					array[num] = this._table._recordManager[enumerator.Current];
					num++;
				}
			}
			return array;
		}

		// Token: 0x060007A4 RID: 1956 RVA: 0x00027148 File Offset: 0x00025348
		private void InitRecords(IFilter filter)
		{
			DataViewRowState recordStates = this._recordStates;
			bool flag = this._indexFields.Length == 0;
			this._records = new Index.IndexTree(this);
			this._recordCount = 0;
			foreach (object obj in this._table.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				int num = -1;
				if (dataRow._oldRecord == dataRow._newRecord)
				{
					if ((recordStates & DataViewRowState.Unchanged) != DataViewRowState.None)
					{
						num = dataRow._oldRecord;
					}
				}
				else if (dataRow._oldRecord == -1)
				{
					if ((recordStates & DataViewRowState.Added) != DataViewRowState.None)
					{
						num = dataRow._newRecord;
					}
				}
				else if (dataRow._newRecord == -1)
				{
					if ((recordStates & DataViewRowState.Deleted) != DataViewRowState.None)
					{
						num = dataRow._oldRecord;
					}
				}
				else if ((recordStates & DataViewRowState.ModifiedCurrent) != DataViewRowState.None)
				{
					num = dataRow._newRecord;
				}
				else if ((recordStates & DataViewRowState.ModifiedOriginal) != DataViewRowState.None)
				{
					num = dataRow._oldRecord;
				}
				if (num != -1 && this.AcceptRecord(num, filter))
				{
					this._records.InsertAt(-1, num, flag);
					this._recordCount++;
				}
			}
		}

		// Token: 0x060007A5 RID: 1957 RVA: 0x0002726C File Offset: 0x0002546C
		public int InsertRecordToIndex(int record)
		{
			int num = -1;
			if (this.AcceptRecord(record))
			{
				num = this.InsertRecord(record, false);
			}
			return num;
		}

		// Token: 0x060007A6 RID: 1958 RVA: 0x00027290 File Offset: 0x00025490
		private int InsertRecord(int record, bool fireEvent)
		{
			DataCommonEventSource.Log.Trace<int, int, bool>("<ds.Index.InsertRecord|INFO> {0}, record={1}, fireEvent={2}", this.ObjectID, record, fireEvent);
			bool flag = false;
			if (this._indexFields.Length == 0 && this._table != null)
			{
				DataRow dataRow = this._table._recordManager[record];
				flag = this._table.Rows.IndexOf(dataRow) + 1 == this._table.Rows.Count;
			}
			int num = this._records.InsertAt(-1, record, flag);
			this._recordCount++;
			this.MaintainDataView(ListChangedType.ItemAdded, record, !fireEvent);
			if (fireEvent)
			{
				if (this.DoListChanged)
				{
					this.OnListChanged(ListChangedType.ItemAdded, this._records.GetIndexByNode(num));
				}
				return 0;
			}
			return this._records.GetIndexByNode(num);
		}

		// Token: 0x060007A7 RID: 1959 RVA: 0x00027354 File Offset: 0x00025554
		public bool IsKeyInIndex(object key)
		{
			int num = this.FindNodeByKey(key);
			return num != 0;
		}

		// Token: 0x060007A8 RID: 1960 RVA: 0x00027370 File Offset: 0x00025570
		public bool IsKeyInIndex(object[] key)
		{
			int num = this.FindNodeByKeys(key);
			return num != 0;
		}

		// Token: 0x060007A9 RID: 1961 RVA: 0x0002738C File Offset: 0x0002558C
		public bool IsKeyRecordInIndex(int record)
		{
			int num = this.FindNodeByKeyRecord(record);
			return num != 0;
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x060007AA RID: 1962 RVA: 0x000273A5 File Offset: 0x000255A5
		private bool DoListChanged
		{
			get
			{
				return !this._suspendEvents && this._listeners.HasListeners && !this._table.AreIndexEventsSuspended;
			}
		}

		// Token: 0x060007AB RID: 1963 RVA: 0x000273CC File Offset: 0x000255CC
		private void OnListChanged(ListChangedType changedType, int newIndex, int oldIndex)
		{
			if (this.DoListChanged)
			{
				this.OnListChanged(new ListChangedEventArgs(changedType, newIndex, oldIndex));
			}
		}

		// Token: 0x060007AC RID: 1964 RVA: 0x000273E4 File Offset: 0x000255E4
		private void OnListChanged(ListChangedType changedType, int index)
		{
			if (this.DoListChanged)
			{
				this.OnListChanged(new ListChangedEventArgs(changedType, index));
			}
		}

		// Token: 0x060007AD RID: 1965 RVA: 0x000273FC File Offset: 0x000255FC
		private void OnListChanged(ListChangedEventArgs e)
		{
			DataCommonEventSource.Log.Trace<int>("<ds.Index.OnListChanged|INFO> {0}", this.ObjectID);
			this._listeners.Notify<ListChangedEventArgs, bool, bool>(e, false, false, delegate(DataViewListener listener, ListChangedEventArgs args, bool arg2, bool arg3)
			{
				listener.IndexListChanged(args);
			});
		}

		// Token: 0x060007AE RID: 1966 RVA: 0x0002744C File Offset: 0x0002564C
		private void MaintainDataView(ListChangedType changedType, int record, bool trackAddRemove)
		{
			this._listeners.Notify<ListChangedType, DataRow, bool>(changedType, (0 <= record) ? this._table._recordManager[record] : null, trackAddRemove, delegate(DataViewListener listener, ListChangedType type, DataRow row, bool track)
			{
				listener.MaintainDataView(changedType, row, track);
			});
		}

		// Token: 0x060007AF RID: 1967 RVA: 0x0002749C File Offset: 0x0002569C
		public void Reset()
		{
			DataCommonEventSource.Log.Trace<int>("<ds.Index.Reset|API> {0}", this.ObjectID);
			this.InitRecords(this.RowFilter);
			this.MaintainDataView(ListChangedType.Reset, -1, false);
			this.FireResetEvent();
		}

		// Token: 0x060007B0 RID: 1968 RVA: 0x000274D0 File Offset: 0x000256D0
		public void RecordChanged(int record)
		{
			DataCommonEventSource.Log.Trace<int, int>("<ds.Index.RecordChanged|API> {0}, record={1}", this.ObjectID, record);
			if (this.DoListChanged)
			{
				int index = this.GetIndex(record);
				if (index >= 0)
				{
					this.OnListChanged(ListChangedType.ItemChanged, index);
				}
			}
		}

		// Token: 0x060007B1 RID: 1969 RVA: 0x00027510 File Offset: 0x00025710
		public void RecordChanged(int oldIndex, int newIndex)
		{
			DataCommonEventSource.Log.Trace<int, int, int>("<ds.Index.RecordChanged|API> {0}, oldIndex={1}, newIndex={2}", this.ObjectID, oldIndex, newIndex);
			if (oldIndex > -1 || newIndex > -1)
			{
				if (oldIndex == newIndex)
				{
					this.OnListChanged(ListChangedType.ItemChanged, newIndex, oldIndex);
					return;
				}
				if (oldIndex == -1)
				{
					this.OnListChanged(ListChangedType.ItemAdded, newIndex, oldIndex);
					return;
				}
				if (newIndex == -1)
				{
					this.OnListChanged(ListChangedType.ItemDeleted, oldIndex);
					return;
				}
				this.OnListChanged(ListChangedType.ItemMoved, newIndex, oldIndex);
			}
		}

		// Token: 0x060007B2 RID: 1970 RVA: 0x00027570 File Offset: 0x00025770
		public void RecordStateChanged(int record, DataViewRowState oldState, DataViewRowState newState)
		{
			DataCommonEventSource.Log.Trace<int, int, DataViewRowState, DataViewRowState>("<ds.Index.RecordStateChanged|API> {0}, record={1}, oldState={2}, newState={3}", this.ObjectID, record, oldState, newState);
			int changeAction = this.GetChangeAction(oldState, newState);
			this.ApplyChangeAction(record, changeAction, Index.GetReplaceAction(oldState));
		}

		// Token: 0x060007B3 RID: 1971 RVA: 0x000275AC File Offset: 0x000257AC
		public void RecordStateChanged(int oldRecord, DataViewRowState oldOldState, DataViewRowState oldNewState, int newRecord, DataViewRowState newOldState, DataViewRowState newNewState)
		{
			DataCommonEventSource.Log.Trace<int, int, DataViewRowState, DataViewRowState, int, DataViewRowState, DataViewRowState>("<ds.Index.RecordStateChanged|API> {0}, oldRecord={1}, oldOldState={2}, oldNewState={3}, newRecord={4}, newOldState={5}, newNewState={6}", this.ObjectID, oldRecord, oldOldState, oldNewState, newRecord, newOldState, newNewState);
			int changeAction = this.GetChangeAction(oldOldState, oldNewState);
			int changeAction2 = this.GetChangeAction(newOldState, newNewState);
			if (changeAction != -1 || changeAction2 != 1 || !this.AcceptRecord(newRecord))
			{
				this.ApplyChangeAction(oldRecord, changeAction, Index.GetReplaceAction(oldOldState));
				this.ApplyChangeAction(newRecord, changeAction2, Index.GetReplaceAction(newOldState));
				return;
			}
			int num;
			if (this._comparison != null && changeAction < 0)
			{
				num = this.GetIndex(oldRecord, Index.GetReplaceAction(oldOldState));
			}
			else
			{
				num = this.GetIndex(oldRecord);
			}
			if (this._comparison == null && num != -1 && this.CompareRecords(oldRecord, newRecord) == 0)
			{
				this._records.UpdateNodeKey(oldRecord, newRecord);
				int index = this.GetIndex(newRecord);
				this.OnListChanged(ListChangedType.ItemChanged, index, index);
				return;
			}
			this._suspendEvents = true;
			if (num != -1)
			{
				this._records.DeleteByIndex(num);
				this._recordCount--;
			}
			this._records.Insert(newRecord);
			this._recordCount++;
			this._suspendEvents = false;
			int index2 = this.GetIndex(newRecord);
			if (num == index2)
			{
				this.OnListChanged(ListChangedType.ItemChanged, index2, num);
				return;
			}
			if (num == -1)
			{
				this.MaintainDataView(ListChangedType.ItemAdded, newRecord, false);
				this.OnListChanged(ListChangedType.ItemAdded, this.GetIndex(newRecord));
				return;
			}
			this.OnListChanged(ListChangedType.ItemMoved, index2, num);
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x060007B4 RID: 1972 RVA: 0x0002770C File Offset: 0x0002590C
		internal DataTable Table
		{
			get
			{
				return this._table;
			}
		}

		// Token: 0x060007B5 RID: 1973 RVA: 0x00027714 File Offset: 0x00025914
		private void GetUniqueKeyValues(List<object[]> list, int curNodeId)
		{
			if (curNodeId != 0)
			{
				this.GetUniqueKeyValues(list, this._records.Left(curNodeId));
				int num = this._records.Key(curNodeId);
				object[] array = new object[this._indexFields.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = this._indexFields[i].Column[num];
				}
				list.Add(array);
				this.GetUniqueKeyValues(list, this._records.Right(curNodeId));
			}
		}

		// Token: 0x060007B6 RID: 1974 RVA: 0x00027794 File Offset: 0x00025994
		internal static int IndexOfReference<T>(List<T> list, T item) where T : class
		{
			if (list != null)
			{
				for (int i = 0; i < list.Count; i++)
				{
					if (list[i] == item)
					{
						return i;
					}
				}
			}
			return -1;
		}

		// Token: 0x04000303 RID: 771
		private readonly DataTable _table;

		// Token: 0x04000304 RID: 772
		internal readonly IndexField[] _indexFields;

		// Token: 0x04000305 RID: 773
		private readonly Comparison<DataRow> _comparison;

		// Token: 0x04000306 RID: 774
		private readonly DataViewRowState _recordStates;

		// Token: 0x04000307 RID: 775
		private WeakReference _rowFilter;

		// Token: 0x04000308 RID: 776
		private Index.IndexTree _records;

		// Token: 0x04000309 RID: 777
		private int _recordCount;

		// Token: 0x0400030A RID: 778
		private int _refCount;

		// Token: 0x0400030B RID: 779
		private Listeners<DataViewListener> _listeners;

		// Token: 0x0400030C RID: 780
		private bool _suspendEvents;

		// Token: 0x0400030D RID: 781
		private readonly bool _isSharable;

		// Token: 0x0400030E RID: 782
		private readonly bool _hasRemoteAggregate;

		// Token: 0x0400030F RID: 783
		private static int s_objectTypeCount;

		// Token: 0x04000310 RID: 784
		private readonly int _objectID = Interlocked.Increment(ref Index.s_objectTypeCount);

		// Token: 0x0200009A RID: 154
		private sealed class IndexTree : RBTree<int>
		{
			// Token: 0x060007B7 RID: 1975 RVA: 0x000277CC File Offset: 0x000259CC
			internal IndexTree(Index index)
				: base(TreeAccessMethod.KEY_SEARCH_AND_INDEX)
			{
				this._index = index;
			}

			// Token: 0x060007B8 RID: 1976 RVA: 0x000277DC File Offset: 0x000259DC
			protected override int CompareNode(int record1, int record2)
			{
				return this._index.CompareRecords(record1, record2);
			}

			// Token: 0x060007B9 RID: 1977 RVA: 0x000277EB File Offset: 0x000259EB
			protected override int CompareSateliteTreeNode(int record1, int record2)
			{
				return this._index.CompareDuplicateRecords(record1, record2);
			}

			// Token: 0x04000311 RID: 785
			private readonly Index _index;
		}
	}
}
