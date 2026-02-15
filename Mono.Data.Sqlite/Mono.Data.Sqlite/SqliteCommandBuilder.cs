using System;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Globalization;

namespace Mono.Data.Sqlite
{
	// Token: 0x02000007 RID: 7
	public sealed class SqliteCommandBuilder : DbCommandBuilder
	{
		// Token: 0x0600008A RID: 138 RVA: 0x000031FC File Offset: 0x000013FC
		public SqliteCommandBuilder()
			: this(null)
		{
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00003208 File Offset: 0x00001408
		public SqliteCommandBuilder(SqliteDataAdapter adp)
		{
			this.QuotePrefix = "[";
			this.QuoteSuffix = "]";
			this.DataAdapter = adp;
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00003238 File Offset: 0x00001438
		protected override void ApplyParameterInfo(DbParameter parameter, DataRow row, StatementType statementType, bool whereClause)
		{
			SqliteParameter sqliteParameter = (SqliteParameter)parameter;
			sqliteParameter.DbType = (DbType)((int)row[SchemaTableColumn.ProviderType]);
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00003262 File Offset: 0x00001462
		protected override string GetParameterName(string parameterName)
		{
			return string.Format(CultureInfo.InvariantCulture, "@{0}", new object[] { parameterName });
		}

		// Token: 0x0600008E RID: 142 RVA: 0x0000327D File Offset: 0x0000147D
		protected override string GetParameterName(int parameterOrdinal)
		{
			return string.Format(CultureInfo.InvariantCulture, "@param{0}", new object[] { parameterOrdinal });
		}

		// Token: 0x0600008F RID: 143 RVA: 0x0000329D File Offset: 0x0000149D
		protected override string GetParameterPlaceholder(int parameterOrdinal)
		{
			return this.GetParameterName(parameterOrdinal);
		}

		// Token: 0x06000090 RID: 144 RVA: 0x000032A8 File Offset: 0x000014A8
		protected override void SetRowUpdatingHandler(DbDataAdapter adapter)
		{
			if (adapter == base.DataAdapter)
			{
				((SqliteDataAdapter)adapter).RowUpdating -= this.RowUpdatingEventHandler;
			}
			else
			{
				((SqliteDataAdapter)adapter).RowUpdating += this.RowUpdatingEventHandler;
			}
		}

		// Token: 0x06000091 RID: 145 RVA: 0x000032F4 File Offset: 0x000014F4
		private void RowUpdatingEventHandler(object sender, RowUpdatingEventArgs e)
		{
			base.RowUpdatingHandler(e);
		}

		// Token: 0x17000006 RID: 6
		// (set) Token: 0x06000092 RID: 146 RVA: 0x000032FD File Offset: 0x000014FD
		public new SqliteDataAdapter DataAdapter
		{
			set
			{
				base.DataAdapter = value;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000093 RID: 147 RVA: 0x00003306 File Offset: 0x00001506
		[Browsable(false)]
		public override CatalogLocation CatalogLocation
		{
			get
			{
				return base.CatalogLocation;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000094 RID: 148 RVA: 0x0000330E File Offset: 0x0000150E
		[Browsable(false)]
		public override string CatalogSeparator
		{
			get
			{
				return base.CatalogSeparator;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000095 RID: 149 RVA: 0x00003316 File Offset: 0x00001516
		// (set) Token: 0x06000096 RID: 150 RVA: 0x0000331E File Offset: 0x0000151E
		[Browsable(false)]
		[DefaultValue("[")]
		public override string QuotePrefix
		{
			get
			{
				return base.QuotePrefix;
			}
			set
			{
				base.QuotePrefix = value;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000097 RID: 151 RVA: 0x00003327 File Offset: 0x00001527
		// (set) Token: 0x06000098 RID: 152 RVA: 0x0000332F File Offset: 0x0000152F
		[Browsable(false)]
		public override string QuoteSuffix
		{
			get
			{
				return base.QuoteSuffix;
			}
			set
			{
				base.QuoteSuffix = value;
			}
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00003338 File Offset: 0x00001538
		public override string QuoteIdentifier(string unquotedIdentifier)
		{
			if (string.IsNullOrEmpty(this.QuotePrefix) || string.IsNullOrEmpty(this.QuoteSuffix) || string.IsNullOrEmpty(unquotedIdentifier))
			{
				return unquotedIdentifier;
			}
			return this.QuotePrefix + unquotedIdentifier.Replace(this.QuoteSuffix, this.QuoteSuffix + this.QuoteSuffix) + this.QuoteSuffix;
		}

		// Token: 0x0600009A RID: 154 RVA: 0x000033A0 File Offset: 0x000015A0
		public override string UnquoteIdentifier(string quotedIdentifier)
		{
			if (string.IsNullOrEmpty(this.QuotePrefix) || string.IsNullOrEmpty(this.QuoteSuffix) || string.IsNullOrEmpty(quotedIdentifier))
			{
				return quotedIdentifier;
			}
			if (!quotedIdentifier.StartsWith(this.QuotePrefix, StringComparison.InvariantCultureIgnoreCase) || !quotedIdentifier.EndsWith(this.QuoteSuffix, StringComparison.InvariantCultureIgnoreCase))
			{
				return quotedIdentifier;
			}
			return quotedIdentifier.Substring(this.QuotePrefix.Length, quotedIdentifier.Length - (this.QuotePrefix.Length + this.QuoteSuffix.Length)).Replace(this.QuoteSuffix + this.QuoteSuffix, this.QuoteSuffix);
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600009B RID: 155 RVA: 0x0000344B File Offset: 0x0000164B
		[Browsable(false)]
		public override string SchemaSeparator
		{
			get
			{
				return base.SchemaSeparator;
			}
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00003454 File Offset: 0x00001654
		protected override DataTable GetSchemaTable(DbCommand sourceCommand)
		{
			DataTable dataTable;
			using (IDataReader dataReader = sourceCommand.ExecuteReader(CommandBehavior.SchemaOnly | CommandBehavior.KeyInfo))
			{
				DataTable schemaTable = dataReader.GetSchemaTable();
				if (this.HasSchemaPrimaryKey(schemaTable))
				{
					this.ResetIsUniqueSchemaColumn(schemaTable);
				}
				dataTable = schemaTable;
			}
			return dataTable;
		}

		// Token: 0x0600009D RID: 157 RVA: 0x000034B0 File Offset: 0x000016B0
		private bool HasSchemaPrimaryKey(DataTable schema)
		{
			DataColumn dataColumn = schema.Columns[SchemaTableColumn.IsKey];
			foreach (object obj in schema.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				if ((bool)dataRow[dataColumn])
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600009E RID: 158 RVA: 0x0000353C File Offset: 0x0000173C
		private void ResetIsUniqueSchemaColumn(DataTable schema)
		{
			DataColumn dataColumn = schema.Columns[SchemaTableColumn.IsUnique];
			DataColumn dataColumn2 = schema.Columns[SchemaTableColumn.IsKey];
			foreach (object obj in schema.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				if (!(bool)dataRow[dataColumn2])
				{
					dataRow[dataColumn] = false;
				}
			}
			schema.AcceptChanges();
		}
	}
}
