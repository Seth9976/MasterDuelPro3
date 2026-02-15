using System;
using System.Globalization;

namespace System.Data.Common
{
	// Token: 0x020000E3 RID: 227
	internal sealed class DbSchemaRow
	{
		// Token: 0x06000C11 RID: 3089 RVA: 0x000428EC File Offset: 0x00040AEC
		internal static DbSchemaRow[] GetSortedSchemaRows(DataTable dataTable, bool returnProviderSpecificTypes)
		{
			DataColumn dataColumn = dataTable.Columns["SchemaMapping Unsorted Index"];
			if (dataColumn == null)
			{
				dataColumn = new DataColumn("SchemaMapping Unsorted Index", typeof(int));
				dataTable.Columns.Add(dataColumn);
			}
			int count = dataTable.Rows.Count;
			for (int i = 0; i < count; i++)
			{
				dataTable.Rows[i][dataColumn] = i;
			}
			DbSchemaTable dbSchemaTable = new DbSchemaTable(dataTable, returnProviderSpecificTypes);
			DataRow[] array = dataTable.Select(null, "ColumnOrdinal ASC", DataViewRowState.CurrentRows);
			DbSchemaRow[] array2 = new DbSchemaRow[array.Length];
			for (int j = 0; j < array.Length; j++)
			{
				array2[j] = new DbSchemaRow(dbSchemaTable, array[j]);
			}
			return array2;
		}

		// Token: 0x06000C12 RID: 3090 RVA: 0x000429A8 File Offset: 0x00040BA8
		internal DbSchemaRow(DbSchemaTable schemaTable, DataRow dataRow)
		{
			this._schemaTable = schemaTable;
			this._dataRow = dataRow;
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x06000C13 RID: 3091 RVA: 0x000429BE File Offset: 0x00040BBE
		internal DataRow DataRow
		{
			get
			{
				return this._dataRow;
			}
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x06000C14 RID: 3092 RVA: 0x000429C8 File Offset: 0x00040BC8
		internal string ColumnName
		{
			get
			{
				object obj = this._dataRow[this._schemaTable.ColumnName, DataRowVersion.Default];
				if (!Convert.IsDBNull(obj))
				{
					return Convert.ToString(obj, CultureInfo.InvariantCulture);
				}
				return string.Empty;
			}
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x06000C15 RID: 3093 RVA: 0x00042A0C File Offset: 0x00040C0C
		internal string BaseColumnName
		{
			get
			{
				if (this._schemaTable.BaseColumnName != null)
				{
					object obj = this._dataRow[this._schemaTable.BaseColumnName, DataRowVersion.Default];
					if (!Convert.IsDBNull(obj))
					{
						return Convert.ToString(obj, CultureInfo.InvariantCulture);
					}
				}
				return string.Empty;
			}
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x06000C16 RID: 3094 RVA: 0x00042A5C File Offset: 0x00040C5C
		internal string BaseServerName
		{
			get
			{
				if (this._schemaTable.BaseServerName != null)
				{
					object obj = this._dataRow[this._schemaTable.BaseServerName, DataRowVersion.Default];
					if (!Convert.IsDBNull(obj))
					{
						return Convert.ToString(obj, CultureInfo.InvariantCulture);
					}
				}
				return string.Empty;
			}
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x06000C17 RID: 3095 RVA: 0x00042AAC File Offset: 0x00040CAC
		internal string BaseCatalogName
		{
			get
			{
				if (this._schemaTable.BaseCatalogName != null)
				{
					object obj = this._dataRow[this._schemaTable.BaseCatalogName, DataRowVersion.Default];
					if (!Convert.IsDBNull(obj))
					{
						return Convert.ToString(obj, CultureInfo.InvariantCulture);
					}
				}
				return string.Empty;
			}
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x06000C18 RID: 3096 RVA: 0x00042AFC File Offset: 0x00040CFC
		internal string BaseSchemaName
		{
			get
			{
				if (this._schemaTable.BaseSchemaName != null)
				{
					object obj = this._dataRow[this._schemaTable.BaseSchemaName, DataRowVersion.Default];
					if (!Convert.IsDBNull(obj))
					{
						return Convert.ToString(obj, CultureInfo.InvariantCulture);
					}
				}
				return string.Empty;
			}
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x06000C19 RID: 3097 RVA: 0x00042B4C File Offset: 0x00040D4C
		internal string BaseTableName
		{
			get
			{
				if (this._schemaTable.BaseTableName != null)
				{
					object obj = this._dataRow[this._schemaTable.BaseTableName, DataRowVersion.Default];
					if (!Convert.IsDBNull(obj))
					{
						return Convert.ToString(obj, CultureInfo.InvariantCulture);
					}
				}
				return string.Empty;
			}
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x06000C1A RID: 3098 RVA: 0x00042B9C File Offset: 0x00040D9C
		internal bool IsAutoIncrement
		{
			get
			{
				if (this._schemaTable.IsAutoIncrement != null)
				{
					object obj = this._dataRow[this._schemaTable.IsAutoIncrement, DataRowVersion.Default];
					if (!Convert.IsDBNull(obj))
					{
						return Convert.ToBoolean(obj, CultureInfo.InvariantCulture);
					}
				}
				return false;
			}
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x06000C1B RID: 3099 RVA: 0x00042BE8 File Offset: 0x00040DE8
		internal bool IsUnique
		{
			get
			{
				if (this._schemaTable.IsUnique != null)
				{
					object obj = this._dataRow[this._schemaTable.IsUnique, DataRowVersion.Default];
					if (!Convert.IsDBNull(obj))
					{
						return Convert.ToBoolean(obj, CultureInfo.InvariantCulture);
					}
				}
				return false;
			}
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x06000C1C RID: 3100 RVA: 0x00042C34 File Offset: 0x00040E34
		internal bool IsRowVersion
		{
			get
			{
				if (this._schemaTable.IsRowVersion != null)
				{
					object obj = this._dataRow[this._schemaTable.IsRowVersion, DataRowVersion.Default];
					if (!Convert.IsDBNull(obj))
					{
						return Convert.ToBoolean(obj, CultureInfo.InvariantCulture);
					}
				}
				return false;
			}
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x06000C1D RID: 3101 RVA: 0x00042C80 File Offset: 0x00040E80
		internal bool IsKey
		{
			get
			{
				if (this._schemaTable.IsKey != null)
				{
					object obj = this._dataRow[this._schemaTable.IsKey, DataRowVersion.Default];
					if (!Convert.IsDBNull(obj))
					{
						return Convert.ToBoolean(obj, CultureInfo.InvariantCulture);
					}
				}
				return false;
			}
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x06000C1E RID: 3102 RVA: 0x00042CCC File Offset: 0x00040ECC
		internal bool IsExpression
		{
			get
			{
				if (this._schemaTable.IsExpression != null)
				{
					object obj = this._dataRow[this._schemaTable.IsExpression, DataRowVersion.Default];
					if (!Convert.IsDBNull(obj))
					{
						return Convert.ToBoolean(obj, CultureInfo.InvariantCulture);
					}
				}
				return false;
			}
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x06000C1F RID: 3103 RVA: 0x00042D18 File Offset: 0x00040F18
		internal bool IsHidden
		{
			get
			{
				if (this._schemaTable.IsHidden != null)
				{
					object obj = this._dataRow[this._schemaTable.IsHidden, DataRowVersion.Default];
					if (!Convert.IsDBNull(obj))
					{
						return Convert.ToBoolean(obj, CultureInfo.InvariantCulture);
					}
				}
				return false;
			}
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x06000C20 RID: 3104 RVA: 0x00042D64 File Offset: 0x00040F64
		internal bool IsLong
		{
			get
			{
				if (this._schemaTable.IsLong != null)
				{
					object obj = this._dataRow[this._schemaTable.IsLong, DataRowVersion.Default];
					if (!Convert.IsDBNull(obj))
					{
						return Convert.ToBoolean(obj, CultureInfo.InvariantCulture);
					}
				}
				return false;
			}
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x06000C21 RID: 3105 RVA: 0x00042DB0 File Offset: 0x00040FB0
		internal bool IsReadOnly
		{
			get
			{
				if (this._schemaTable.IsReadOnly != null)
				{
					object obj = this._dataRow[this._schemaTable.IsReadOnly, DataRowVersion.Default];
					if (!Convert.IsDBNull(obj))
					{
						return Convert.ToBoolean(obj, CultureInfo.InvariantCulture);
					}
				}
				return false;
			}
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x06000C22 RID: 3106 RVA: 0x00042DFC File Offset: 0x00040FFC
		internal bool AllowDBNull
		{
			get
			{
				if (this._schemaTable.AllowDBNull != null)
				{
					object obj = this._dataRow[this._schemaTable.AllowDBNull, DataRowVersion.Default];
					if (!Convert.IsDBNull(obj))
					{
						return Convert.ToBoolean(obj, CultureInfo.InvariantCulture);
					}
				}
				return true;
			}
		}

		// Token: 0x040004CF RID: 1231
		private DbSchemaTable _schemaTable;

		// Token: 0x040004D0 RID: 1232
		private DataRow _dataRow;
	}
}
