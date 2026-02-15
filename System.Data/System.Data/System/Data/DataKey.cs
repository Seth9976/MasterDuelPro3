using System;

namespace System.Data
{
	// Token: 0x0200003A RID: 58
	internal readonly struct DataKey
	{
		// Token: 0x060003D0 RID: 976 RVA: 0x00014804 File Offset: 0x00012A04
		internal DataKey(DataColumn[] columns, bool copyColumns)
		{
			if (columns == null)
			{
				throw ExceptionBuilder.ArgumentNull("columns");
			}
			if (columns.Length == 0)
			{
				throw ExceptionBuilder.KeyNoColumns();
			}
			if (columns.Length > 32)
			{
				throw ExceptionBuilder.KeyTooManyColumns(32);
			}
			for (int i = 0; i < columns.Length; i++)
			{
				if (columns[i] == null)
				{
					throw ExceptionBuilder.ArgumentNull("column");
				}
			}
			for (int j = 0; j < columns.Length; j++)
			{
				for (int k = 0; k < j; k++)
				{
					if (columns[j] == columns[k])
					{
						throw ExceptionBuilder.KeyDuplicateColumns(columns[j].ColumnName);
					}
				}
			}
			if (copyColumns)
			{
				this._columns = new DataColumn[columns.Length];
				for (int l = 0; l < columns.Length; l++)
				{
					this._columns[l] = columns[l];
				}
			}
			else
			{
				this._columns = columns;
			}
			this.CheckState();
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060003D1 RID: 977 RVA: 0x000148BF File Offset: 0x00012ABF
		internal DataColumn[] ColumnsReference
		{
			get
			{
				return this._columns;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060003D2 RID: 978 RVA: 0x000148C7 File Offset: 0x00012AC7
		internal bool HasValue
		{
			get
			{
				return this._columns != null;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060003D3 RID: 979 RVA: 0x000148D2 File Offset: 0x00012AD2
		internal DataTable Table
		{
			get
			{
				return this._columns[0].Table;
			}
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x000148E4 File Offset: 0x00012AE4
		internal void CheckState()
		{
			DataTable table = this._columns[0].Table;
			if (table == null)
			{
				throw ExceptionBuilder.ColumnNotInAnyTable();
			}
			for (int i = 1; i < this._columns.Length; i++)
			{
				if (this._columns[i].Table == null)
				{
					throw ExceptionBuilder.ColumnNotInAnyTable();
				}
				if (this._columns[i].Table != table)
				{
					throw ExceptionBuilder.KeyTableMismatch();
				}
			}
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x00014946 File Offset: 0x00012B46
		internal bool ColumnsEqual(DataKey key)
		{
			return DataKey.ColumnsEqual(this._columns, key._columns);
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x0001495C File Offset: 0x00012B5C
		internal static bool ColumnsEqual(DataColumn[] column1, DataColumn[] column2)
		{
			if (column1 == column2)
			{
				return true;
			}
			if (column1 == null || column2 == null)
			{
				return false;
			}
			if (column1.Length != column2.Length)
			{
				return false;
			}
			for (int i = 0; i < column1.Length; i++)
			{
				bool flag = false;
				for (int j = 0; j < column2.Length; j++)
				{
					if (column1[i].Equals(column2[j]))
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x000149B8 File Offset: 0x00012BB8
		internal bool ContainsColumn(DataColumn column)
		{
			for (int i = 0; i < this._columns.Length; i++)
			{
				if (column == this._columns[i])
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x000149E6 File Offset: 0x00012BE6
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x000149F8 File Offset: 0x00012BF8
		public override bool Equals(object value)
		{
			return this.Equals((DataKey)value);
		}

		// Token: 0x060003DA RID: 986 RVA: 0x00014A08 File Offset: 0x00012C08
		internal bool Equals(DataKey value)
		{
			DataColumn[] columns = this._columns;
			DataColumn[] columns2 = value._columns;
			if (columns == columns2)
			{
				return true;
			}
			if (columns == null || columns2 == null)
			{
				return false;
			}
			if (columns.Length != columns2.Length)
			{
				return false;
			}
			for (int i = 0; i < columns.Length; i++)
			{
				if (!columns[i].Equals(columns2[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060003DB RID: 987 RVA: 0x00014A5C File Offset: 0x00012C5C
		internal string[] GetColumnNames()
		{
			string[] array = new string[this._columns.Length];
			for (int i = 0; i < this._columns.Length; i++)
			{
				array[i] = this._columns[i].ColumnName;
			}
			return array;
		}

		// Token: 0x060003DC RID: 988 RVA: 0x00014A9C File Offset: 0x00012C9C
		internal IndexField[] GetIndexDesc()
		{
			IndexField[] array = new IndexField[this._columns.Length];
			for (int i = 0; i < this._columns.Length; i++)
			{
				array[i] = new IndexField(this._columns[i], false);
			}
			return array;
		}

		// Token: 0x060003DD RID: 989 RVA: 0x00014AE0 File Offset: 0x00012CE0
		internal object[] GetKeyValues(int record)
		{
			object[] array = new object[this._columns.Length];
			for (int i = 0; i < this._columns.Length; i++)
			{
				array[i] = this._columns[i][record];
			}
			return array;
		}

		// Token: 0x060003DE RID: 990 RVA: 0x00014B20 File Offset: 0x00012D20
		internal Index GetSortIndex()
		{
			return this.GetSortIndex(DataViewRowState.CurrentRows);
		}

		// Token: 0x060003DF RID: 991 RVA: 0x00014B2C File Offset: 0x00012D2C
		internal Index GetSortIndex(DataViewRowState recordStates)
		{
			IndexField[] indexDesc = this.GetIndexDesc();
			return this._columns[0].Table.GetIndex(indexDesc, recordStates, null);
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x00014B58 File Offset: 0x00012D58
		internal bool RecordsEqual(int record1, int record2)
		{
			for (int i = 0; i < this._columns.Length; i++)
			{
				if (this._columns[i].Compare(record1, record2) != 0)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x00014B8C File Offset: 0x00012D8C
		internal DataColumn[] ToArray()
		{
			DataColumn[] array = new DataColumn[this._columns.Length];
			for (int i = 0; i < this._columns.Length; i++)
			{
				array[i] = this._columns[i];
			}
			return array;
		}

		// Token: 0x0400012B RID: 299
		private readonly DataColumn[] _columns;
	}
}
