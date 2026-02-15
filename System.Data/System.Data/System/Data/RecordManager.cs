using System;
using System.Collections.Generic;
using System.Data.Common;

namespace System.Data
{
	// Token: 0x02000091 RID: 145
	internal sealed class RecordManager
	{
		// Token: 0x06000740 RID: 1856 RVA: 0x000246B0 File Offset: 0x000228B0
		internal RecordManager(DataTable table)
		{
			if (table == null)
			{
				throw ExceptionBuilder.ArgumentNull("table");
			}
			this._table = table;
		}

		// Token: 0x06000741 RID: 1857 RVA: 0x000246E0 File Offset: 0x000228E0
		private void GrowRecordCapacity()
		{
			this.RecordCapacity = ((RecordManager.NewCapacity(this._recordCapacity) < this.NormalizedMinimumCapacity(this._minimumCapacity)) ? this.NormalizedMinimumCapacity(this._minimumCapacity) : RecordManager.NewCapacity(this._recordCapacity));
			DataRow[] array = this._table.NewRowArray(this._recordCapacity);
			if (this._rows != null)
			{
				Array.Copy(this._rows, 0, array, 0, Math.Min(this._lastFreeRecord, this._rows.Length));
			}
			this._rows = array;
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000742 RID: 1858 RVA: 0x00024767 File Offset: 0x00022967
		internal int LastFreeRecord
		{
			get
			{
				return this._lastFreeRecord;
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x06000743 RID: 1859 RVA: 0x0002476F File Offset: 0x0002296F
		// (set) Token: 0x06000744 RID: 1860 RVA: 0x00024777 File Offset: 0x00022977
		internal int MinimumCapacity
		{
			get
			{
				return this._minimumCapacity;
			}
			set
			{
				if (this._minimumCapacity != value)
				{
					if (value < 0)
					{
						throw ExceptionBuilder.NegativeMinimumCapacity();
					}
					this._minimumCapacity = value;
				}
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000745 RID: 1861 RVA: 0x00024793 File Offset: 0x00022993
		// (set) Token: 0x06000746 RID: 1862 RVA: 0x0002479C File Offset: 0x0002299C
		internal int RecordCapacity
		{
			get
			{
				return this._recordCapacity;
			}
			set
			{
				if (this._recordCapacity != value)
				{
					for (int i = 0; i < this._table.Columns.Count; i++)
					{
						this._table.Columns[i].SetCapacity(value);
					}
					this._recordCapacity = value;
				}
			}
		}

		// Token: 0x06000747 RID: 1863 RVA: 0x000247EB File Offset: 0x000229EB
		internal static int NewCapacity(int capacity)
		{
			if (capacity >= 128)
			{
				return capacity + capacity;
			}
			return 128;
		}

		// Token: 0x06000748 RID: 1864 RVA: 0x000247FE File Offset: 0x000229FE
		private int NormalizedMinimumCapacity(int capacity)
		{
			if (capacity >= 1014)
			{
				return (capacity + 10 >> 10) + 1 << 10;
			}
			if (capacity >= 246)
			{
				return 1024;
			}
			if (capacity < 54)
			{
				return 64;
			}
			return 256;
		}

		// Token: 0x06000749 RID: 1865 RVA: 0x00024830 File Offset: 0x00022A30
		internal int NewRecordBase()
		{
			int num;
			if (this._freeRecordList.Count != 0)
			{
				num = this._freeRecordList[this._freeRecordList.Count - 1];
				this._freeRecordList.RemoveAt(this._freeRecordList.Count - 1);
			}
			else
			{
				if (this._lastFreeRecord >= this._recordCapacity)
				{
					this.GrowRecordCapacity();
				}
				num = this._lastFreeRecord;
				this._lastFreeRecord++;
			}
			return num;
		}

		// Token: 0x0600074A RID: 1866 RVA: 0x000248A8 File Offset: 0x00022AA8
		internal void FreeRecord(ref int record)
		{
			if (-1 != record)
			{
				this[record] = null;
				int count = this._table._columnCollection.Count;
				for (int i = 0; i < count; i++)
				{
					this._table._columnCollection[i].FreeRecord(record);
				}
				if (this._lastFreeRecord == record + 1)
				{
					this._lastFreeRecord--;
				}
				else if (record < this._lastFreeRecord)
				{
					this._freeRecordList.Add(record);
				}
				record = -1;
			}
		}

		// Token: 0x0600074B RID: 1867 RVA: 0x00024930 File Offset: 0x00022B30
		internal void Clear(bool clearAll)
		{
			if (clearAll)
			{
				for (int i = 0; i < this._recordCapacity; i++)
				{
					this._rows[i] = null;
				}
				int count = this._table._columnCollection.Count;
				for (int j = 0; j < count; j++)
				{
					DataColumn dataColumn = this._table._columnCollection[j];
					for (int k = 0; k < this._recordCapacity; k++)
					{
						dataColumn.FreeRecord(k);
					}
				}
				this._lastFreeRecord = 0;
				this._freeRecordList.Clear();
				return;
			}
			this._freeRecordList.Capacity = this._freeRecordList.Count + this._table.Rows.Count;
			for (int l = 0; l < this._recordCapacity; l++)
			{
				if (this._rows[l] != null && this._rows[l].rowID != -1L)
				{
					int num = l;
					this.FreeRecord(ref num);
				}
			}
		}

		// Token: 0x1700012B RID: 299
		internal DataRow this[int record]
		{
			get
			{
				return this._rows[record];
			}
			set
			{
				this._rows[record] = value;
			}
		}

		// Token: 0x0600074E RID: 1870 RVA: 0x00024A33 File Offset: 0x00022C33
		internal int ImportRecord(DataTable src, int record)
		{
			return this.CopyRecord(src, record, -1);
		}

		// Token: 0x0600074F RID: 1871 RVA: 0x00024A40 File Offset: 0x00022C40
		internal int CopyRecord(DataTable src, int record, int copy)
		{
			if (record == -1)
			{
				return copy;
			}
			int num = -1;
			try
			{
				num = ((copy == -1) ? this._table.NewUninitializedRecord() : copy);
				int count = this._table.Columns.Count;
				for (int i = 0; i < count; i++)
				{
					DataColumn dataColumn = this._table.Columns[i];
					DataColumn dataColumn2 = src.Columns[dataColumn.ColumnName];
					if (dataColumn2 != null)
					{
						object obj = dataColumn2[record];
						ICloneable cloneable = obj as ICloneable;
						if (cloneable != null)
						{
							dataColumn[num] = cloneable.Clone();
						}
						else
						{
							dataColumn[num] = obj;
						}
					}
					else if (-1 == copy)
					{
						dataColumn.Init(num);
					}
				}
			}
			catch (Exception ex) when (ADP.IsCatchableOrSecurityExceptionType(ex))
			{
				if (-1 == copy)
				{
					this.FreeRecord(ref num);
				}
				throw;
			}
			return num;
		}

		// Token: 0x06000750 RID: 1872 RVA: 0x00024B24 File Offset: 0x00022D24
		internal void SetRowCache(DataRow[] newRows)
		{
			this._rows = newRows;
			this._lastFreeRecord = this._rows.Length;
			this._recordCapacity = this._lastFreeRecord;
		}

		// Token: 0x040002DF RID: 735
		private readonly DataTable _table;

		// Token: 0x040002E0 RID: 736
		private int _lastFreeRecord;

		// Token: 0x040002E1 RID: 737
		private int _minimumCapacity = 50;

		// Token: 0x040002E2 RID: 738
		private int _recordCapacity;

		// Token: 0x040002E3 RID: 739
		private readonly List<int> _freeRecordList = new List<int>();

		// Token: 0x040002E4 RID: 740
		private DataRow[] _rows;
	}
}
