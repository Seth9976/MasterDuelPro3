using System;

namespace System.Data.Common
{
	// Token: 0x020000E4 RID: 228
	internal sealed class DbSchemaTable
	{
		// Token: 0x06000C23 RID: 3107 RVA: 0x00042E47 File Offset: 0x00041047
		internal DbSchemaTable(DataTable dataTable, bool returnProviderSpecificTypes)
		{
			this._dataTable = dataTable;
			this._columns = dataTable.Columns;
			this._returnProviderSpecificTypes = returnProviderSpecificTypes;
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x06000C24 RID: 3108 RVA: 0x00042E7B File Offset: 0x0004107B
		internal DataColumn ColumnName
		{
			get
			{
				return this.CachedDataColumn(DbSchemaTable.ColumnEnum.ColumnName);
			}
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x06000C25 RID: 3109 RVA: 0x00042E84 File Offset: 0x00041084
		internal DataColumn BaseServerName
		{
			get
			{
				return this.CachedDataColumn(DbSchemaTable.ColumnEnum.BaseServerName);
			}
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x06000C26 RID: 3110 RVA: 0x00042E8D File Offset: 0x0004108D
		internal DataColumn BaseColumnName
		{
			get
			{
				return this.CachedDataColumn(DbSchemaTable.ColumnEnum.BaseColumnName);
			}
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x06000C27 RID: 3111 RVA: 0x00042E96 File Offset: 0x00041096
		internal DataColumn BaseTableName
		{
			get
			{
				return this.CachedDataColumn(DbSchemaTable.ColumnEnum.BaseTableName);
			}
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x06000C28 RID: 3112 RVA: 0x00042E9F File Offset: 0x0004109F
		internal DataColumn BaseCatalogName
		{
			get
			{
				return this.CachedDataColumn(DbSchemaTable.ColumnEnum.BaseCatalogName);
			}
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x06000C29 RID: 3113 RVA: 0x00042EA8 File Offset: 0x000410A8
		internal DataColumn BaseSchemaName
		{
			get
			{
				return this.CachedDataColumn(DbSchemaTable.ColumnEnum.BaseSchemaName);
			}
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x06000C2A RID: 3114 RVA: 0x00042EB1 File Offset: 0x000410B1
		internal DataColumn IsAutoIncrement
		{
			get
			{
				return this.CachedDataColumn(DbSchemaTable.ColumnEnum.IsAutoIncrement);
			}
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x06000C2B RID: 3115 RVA: 0x00042EBA File Offset: 0x000410BA
		internal DataColumn IsUnique
		{
			get
			{
				return this.CachedDataColumn(DbSchemaTable.ColumnEnum.IsUnique);
			}
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x06000C2C RID: 3116 RVA: 0x00042EC4 File Offset: 0x000410C4
		internal DataColumn IsKey
		{
			get
			{
				return this.CachedDataColumn(DbSchemaTable.ColumnEnum.IsKey);
			}
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x06000C2D RID: 3117 RVA: 0x00042ECE File Offset: 0x000410CE
		internal DataColumn IsRowVersion
		{
			get
			{
				return this.CachedDataColumn(DbSchemaTable.ColumnEnum.IsRowVersion);
			}
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x06000C2E RID: 3118 RVA: 0x00042ED8 File Offset: 0x000410D8
		internal DataColumn AllowDBNull
		{
			get
			{
				return this.CachedDataColumn(DbSchemaTable.ColumnEnum.AllowDBNull);
			}
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x06000C2F RID: 3119 RVA: 0x00042EE2 File Offset: 0x000410E2
		internal DataColumn IsExpression
		{
			get
			{
				return this.CachedDataColumn(DbSchemaTable.ColumnEnum.IsExpression);
			}
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x06000C30 RID: 3120 RVA: 0x00042EEC File Offset: 0x000410EC
		internal DataColumn IsHidden
		{
			get
			{
				return this.CachedDataColumn(DbSchemaTable.ColumnEnum.IsHidden);
			}
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x06000C31 RID: 3121 RVA: 0x00042EF6 File Offset: 0x000410F6
		internal DataColumn IsLong
		{
			get
			{
				return this.CachedDataColumn(DbSchemaTable.ColumnEnum.IsLong);
			}
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x06000C32 RID: 3122 RVA: 0x00042F00 File Offset: 0x00041100
		internal DataColumn IsReadOnly
		{
			get
			{
				return this.CachedDataColumn(DbSchemaTable.ColumnEnum.IsReadOnly);
			}
		}

		// Token: 0x06000C33 RID: 3123 RVA: 0x00042F0A File Offset: 0x0004110A
		private DataColumn CachedDataColumn(DbSchemaTable.ColumnEnum column)
		{
			return this.CachedDataColumn(column, column);
		}

		// Token: 0x06000C34 RID: 3124 RVA: 0x00042F14 File Offset: 0x00041114
		private DataColumn CachedDataColumn(DbSchemaTable.ColumnEnum column, DbSchemaTable.ColumnEnum column2)
		{
			DataColumn dataColumn = this._columnCache[(int)column];
			if (dataColumn == null)
			{
				int num = this._columns.IndexOf(DbSchemaTable.s_DBCOLUMN_NAME[(int)column]);
				if (-1 == num && column != column2)
				{
					num = this._columns.IndexOf(DbSchemaTable.s_DBCOLUMN_NAME[(int)column2]);
				}
				if (-1 != num)
				{
					dataColumn = this._columns[num];
					this._columnCache[(int)column] = dataColumn;
				}
			}
			return dataColumn;
		}

		// Token: 0x040004D1 RID: 1233
		private static readonly string[] s_DBCOLUMN_NAME = new string[]
		{
			SchemaTableColumn.ColumnName,
			SchemaTableColumn.ColumnOrdinal,
			SchemaTableColumn.ColumnSize,
			SchemaTableOptionalColumn.BaseServerName,
			SchemaTableOptionalColumn.BaseCatalogName,
			SchemaTableColumn.BaseColumnName,
			SchemaTableColumn.BaseSchemaName,
			SchemaTableColumn.BaseTableName,
			SchemaTableOptionalColumn.IsAutoIncrement,
			SchemaTableColumn.IsUnique,
			SchemaTableColumn.IsKey,
			SchemaTableOptionalColumn.IsRowVersion,
			SchemaTableColumn.DataType,
			SchemaTableOptionalColumn.ProviderSpecificDataType,
			SchemaTableColumn.AllowDBNull,
			SchemaTableColumn.ProviderType,
			SchemaTableColumn.IsExpression,
			SchemaTableOptionalColumn.IsHidden,
			SchemaTableColumn.IsLong,
			SchemaTableOptionalColumn.IsReadOnly,
			"SchemaMapping Unsorted Index"
		};

		// Token: 0x040004D2 RID: 1234
		internal DataTable _dataTable;

		// Token: 0x040004D3 RID: 1235
		private DataColumnCollection _columns;

		// Token: 0x040004D4 RID: 1236
		private DataColumn[] _columnCache = new DataColumn[DbSchemaTable.s_DBCOLUMN_NAME.Length];

		// Token: 0x040004D5 RID: 1237
		private bool _returnProviderSpecificTypes;

		// Token: 0x020000E5 RID: 229
		private enum ColumnEnum
		{
			// Token: 0x040004D7 RID: 1239
			ColumnName,
			// Token: 0x040004D8 RID: 1240
			ColumnOrdinal,
			// Token: 0x040004D9 RID: 1241
			ColumnSize,
			// Token: 0x040004DA RID: 1242
			BaseServerName,
			// Token: 0x040004DB RID: 1243
			BaseCatalogName,
			// Token: 0x040004DC RID: 1244
			BaseColumnName,
			// Token: 0x040004DD RID: 1245
			BaseSchemaName,
			// Token: 0x040004DE RID: 1246
			BaseTableName,
			// Token: 0x040004DF RID: 1247
			IsAutoIncrement,
			// Token: 0x040004E0 RID: 1248
			IsUnique,
			// Token: 0x040004E1 RID: 1249
			IsKey,
			// Token: 0x040004E2 RID: 1250
			IsRowVersion,
			// Token: 0x040004E3 RID: 1251
			DataType,
			// Token: 0x040004E4 RID: 1252
			ProviderSpecificDataType,
			// Token: 0x040004E5 RID: 1253
			AllowDBNull,
			// Token: 0x040004E6 RID: 1254
			ProviderType,
			// Token: 0x040004E7 RID: 1255
			IsExpression,
			// Token: 0x040004E8 RID: 1256
			IsHidden,
			// Token: 0x040004E9 RID: 1257
			IsLong,
			// Token: 0x040004EA RID: 1258
			IsReadOnly,
			// Token: 0x040004EB RID: 1259
			SchemaMappingUnsortedIndex
		}
	}
}
