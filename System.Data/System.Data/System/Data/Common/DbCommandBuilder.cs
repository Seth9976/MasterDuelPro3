using System;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace System.Data.Common
{
	/// <summary>Automatically generates single-table commands used to reconcile changes made to a <see cref="T:System.Data.DataSet" /> with the associated database. This is an abstract class that can only be inherited.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000E1 RID: 225
	public abstract class DbCommandBuilder : Component
	{
		/// <summary>Specifies which <see cref="T:System.Data.ConflictOption" /> is to be used by the <see cref="T:System.Data.Common.DbCommandBuilder" />.</summary>
		/// <returns>Returns one of the <see cref="T:System.Data.ConflictOption" /> values describing the behavior of this <see cref="T:System.Data.Common.DbCommandBuilder" />.</returns>
		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x06000BD0 RID: 3024 RVA: 0x0004114D File Offset: 0x0003F34D
		[DefaultValue(ConflictOption.CompareAllSearchableValues)]
		public virtual ConflictOption ConflictOption
		{
			get
			{
				return this._conflictDetection;
			}
		}

		/// <summary>Sets or gets the <see cref="T:System.Data.Common.CatalogLocation" /> for an instance of the <see cref="T:System.Data.Common.DbCommandBuilder" /> class.</summary>
		/// <returns>A <see cref="T:System.Data.Common.CatalogLocation" /> object.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x170001AA RID: 426
		// (get) Token: 0x06000BD1 RID: 3025 RVA: 0x00041155 File Offset: 0x0003F355
		[DefaultValue(CatalogLocation.Start)]
		public virtual CatalogLocation CatalogLocation
		{
			get
			{
				return this._catalogLocation;
			}
		}

		/// <summary>Sets or gets a string used as the catalog separator for an instance of the <see cref="T:System.Data.Common.DbCommandBuilder" /> class.</summary>
		/// <returns>A string indicating the catalog separator for use with an instance of the <see cref="T:System.Data.Common.DbCommandBuilder" /> class.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x170001AB RID: 427
		// (get) Token: 0x06000BD2 RID: 3026 RVA: 0x00041160 File Offset: 0x0003F360
		[DefaultValue(".")]
		public virtual string CatalogSeparator
		{
			get
			{
				string catalogSeparator = this._catalogSeparator;
				if (catalogSeparator == null || 0 >= catalogSeparator.Length)
				{
					return ".";
				}
				return catalogSeparator;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Data.Common.DbDataAdapter" /> object for which Transact-SQL statements are automatically generated.</summary>
		/// <returns>A <see cref="T:System.Data.Common.DbDataAdapter" /> object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001AC RID: 428
		// (get) Token: 0x06000BD3 RID: 3027 RVA: 0x00041187 File Offset: 0x0003F387
		// (set) Token: 0x06000BD4 RID: 3028 RVA: 0x0004118F File Offset: 0x0003F38F
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public DbDataAdapter DataAdapter
		{
			get
			{
				return this._dataAdapter;
			}
			set
			{
				if (this._dataAdapter != value)
				{
					this.RefreshSchema();
					if (this._dataAdapter != null)
					{
						this.SetRowUpdatingHandler(this._dataAdapter);
						this._dataAdapter = null;
					}
					if (value != null)
					{
						this.SetRowUpdatingHandler(value);
						this._dataAdapter = value;
					}
				}
			}
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06000BD5 RID: 3029 RVA: 0x000411CC File Offset: 0x0003F3CC
		internal int ParameterNameMaxLength
		{
			get
			{
				return this._parameterNameMaxLength;
			}
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x06000BD6 RID: 3030 RVA: 0x000411D4 File Offset: 0x0003F3D4
		internal string ParameterNamePattern
		{
			get
			{
				return this._parameterNamePattern;
			}
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x06000BD7 RID: 3031 RVA: 0x000411DC File Offset: 0x0003F3DC
		private string QuotedBaseTableName
		{
			get
			{
				return this._quotedBaseTableName;
			}
		}

		/// <summary>Gets or sets the beginning character or characters to use when specifying database objects (for example, tables or columns) whose names contain characters such as spaces or reserved tokens.</summary>
		/// <returns>The beginning character or characters to use. The default is an empty string.</returns>
		/// <exception cref="T:System.InvalidOperationException">This property cannot be changed after an insert, update, or delete command has been generated. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x06000BD8 RID: 3032 RVA: 0x000411E4 File Offset: 0x0003F3E4
		// (set) Token: 0x06000BD9 RID: 3033 RVA: 0x000411F5 File Offset: 0x0003F3F5
		[DefaultValue("")]
		public virtual string QuotePrefix
		{
			get
			{
				return this._quotePrefix ?? string.Empty;
			}
			set
			{
				if (this._dbSchemaTable != null)
				{
					throw ADP.NoQuoteChange();
				}
				this._quotePrefix = value;
			}
		}

		/// <summary>Gets or sets the ending character or characters to use when specifying database objects (for example, tables or columns) whose names contain characters such as spaces or reserved tokens.</summary>
		/// <returns>The ending character or characters to use. The default is an empty string.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x06000BDA RID: 3034 RVA: 0x0004120C File Offset: 0x0003F40C
		// (set) Token: 0x06000BDB RID: 3035 RVA: 0x0004122A File Offset: 0x0003F42A
		[DefaultValue("")]
		public virtual string QuoteSuffix
		{
			get
			{
				string quoteSuffix = this._quoteSuffix;
				if (quoteSuffix == null)
				{
					return string.Empty;
				}
				return quoteSuffix;
			}
			set
			{
				if (this._dbSchemaTable != null)
				{
					throw ADP.NoQuoteChange();
				}
				this._quoteSuffix = value;
			}
		}

		/// <summary>Gets or sets the character to be used for the separator between the schema identifier and any other identifiers.</summary>
		/// <returns>The character to be used as the schema separator.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x06000BDC RID: 3036 RVA: 0x00041244 File Offset: 0x0003F444
		[DefaultValue(".")]
		public virtual string SchemaSeparator
		{
			get
			{
				string schemaSeparator = this._schemaSeparator;
				if (schemaSeparator == null || 0 >= schemaSeparator.Length)
				{
					return ".";
				}
				return schemaSeparator;
			}
		}

		/// <summary>Specifies whether all column values in an update statement are included or only changed ones.</summary>
		/// <returns>true if the UPDATE statement generated by the <see cref="T:System.Data.Common.DbCommandBuilder" /> includes all columns; false if it includes only changed columns.</returns>
		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x06000BDD RID: 3037 RVA: 0x0004126B File Offset: 0x0003F46B
		[DefaultValue(false)]
		public bool SetAllValues
		{
			get
			{
				return this._setAllValues;
			}
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x06000BDE RID: 3038 RVA: 0x00041273 File Offset: 0x0003F473
		// (set) Token: 0x06000BDF RID: 3039 RVA: 0x0004127B File Offset: 0x0003F47B
		private DbCommand InsertCommand
		{
			get
			{
				return this._insertCommand;
			}
			set
			{
				this._insertCommand = value;
			}
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06000BE0 RID: 3040 RVA: 0x00041284 File Offset: 0x0003F484
		// (set) Token: 0x06000BE1 RID: 3041 RVA: 0x0004128C File Offset: 0x0003F48C
		private DbCommand UpdateCommand
		{
			get
			{
				return this._updateCommand;
			}
			set
			{
				this._updateCommand = value;
			}
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06000BE2 RID: 3042 RVA: 0x00041295 File Offset: 0x0003F495
		// (set) Token: 0x06000BE3 RID: 3043 RVA: 0x0004129D File Offset: 0x0003F49D
		private DbCommand DeleteCommand
		{
			get
			{
				return this._deleteCommand;
			}
			set
			{
				this._deleteCommand = value;
			}
		}

		// Token: 0x06000BE4 RID: 3044 RVA: 0x000412A8 File Offset: 0x0003F4A8
		private void BuildCache(bool closeConnection, DataRow dataRow, bool useColumnsForParameterNames)
		{
			if (this._dbSchemaTable != null && (!useColumnsForParameterNames || this._parameterNames != null))
			{
				return;
			}
			DataTable dataTable = null;
			DbCommand selectCommand = this.GetSelectCommand();
			DbConnection connection = selectCommand.Connection;
			if (connection == null)
			{
				throw ADP.MissingSourceCommandConnection();
			}
			try
			{
				if ((ConnectionState.Open & connection.State) == ConnectionState.Closed)
				{
					connection.Open();
				}
				else
				{
					closeConnection = false;
				}
				if (useColumnsForParameterNames)
				{
					DataTable schema = connection.GetSchema(DbMetaDataCollectionNames.DataSourceInformation);
					if (schema.Rows.Count == 1)
					{
						this._parameterNamePattern = schema.Rows[0][DbMetaDataColumnNames.ParameterNamePattern] as string;
						this._parameterMarkerFormat = schema.Rows[0][DbMetaDataColumnNames.ParameterMarkerFormat] as string;
						object obj = schema.Rows[0][DbMetaDataColumnNames.ParameterNameMaxLength];
						this._parameterNameMaxLength = ((obj is int) ? ((int)obj) : 0);
						if (this._parameterNameMaxLength == 0 || this._parameterNamePattern == null || this._parameterMarkerFormat == null)
						{
							useColumnsForParameterNames = false;
						}
					}
					else
					{
						useColumnsForParameterNames = false;
					}
				}
				dataTable = this.GetSchemaTable(selectCommand);
			}
			finally
			{
				if (closeConnection)
				{
					connection.Close();
				}
			}
			if (dataTable == null)
			{
				throw ADP.DynamicSQLNoTableInfo();
			}
			this.BuildInformation(dataTable);
			this._dbSchemaTable = dataTable;
			DbSchemaRow[] dbSchemaRows = this._dbSchemaRows;
			string[] array = new string[dbSchemaRows.Length];
			for (int i = 0; i < dbSchemaRows.Length; i++)
			{
				if (dbSchemaRows[i] != null)
				{
					array[i] = dbSchemaRows[i].ColumnName;
				}
			}
			this._sourceColumnNames = array;
			if (useColumnsForParameterNames)
			{
				this._parameterNames = new DbCommandBuilder.ParameterNames(this, dbSchemaRows);
			}
			ADP.BuildSchemaTableInfoTableNames(array);
		}

		/// <summary>Returns the schema table for the <see cref="T:System.Data.Common.DbCommandBuilder" />.</summary>
		/// <returns>A <see cref="T:System.Data.DataTable" /> that represents the schema for the specific <see cref="T:System.Data.Common.DbCommand" />.</returns>
		/// <param name="sourceCommand">The <see cref="T:System.Data.Common.DbCommand" /> for which to retrieve the corresponding schema table.</param>
		// Token: 0x06000BE5 RID: 3045 RVA: 0x00041444 File Offset: 0x0003F644
		protected virtual DataTable GetSchemaTable(DbCommand sourceCommand)
		{
			DataTable schemaTable;
			using (IDataReader dataReader = sourceCommand.ExecuteReader(CommandBehavior.SchemaOnly | CommandBehavior.KeyInfo))
			{
				schemaTable = dataReader.GetSchemaTable();
			}
			return schemaTable;
		}

		// Token: 0x06000BE6 RID: 3046 RVA: 0x00041480 File Offset: 0x0003F680
		private void BuildInformation(DataTable schemaTable)
		{
			DbSchemaRow[] sortedSchemaRows = DbSchemaRow.GetSortedSchemaRows(schemaTable, false);
			if (sortedSchemaRows == null || sortedSchemaRows.Length == 0)
			{
				throw ADP.DynamicSQLNoTableInfo();
			}
			string text = string.Empty;
			string text2 = string.Empty;
			string text3 = string.Empty;
			string text4 = null;
			for (int i = 0; i < sortedSchemaRows.Length; i++)
			{
				DbSchemaRow dbSchemaRow = sortedSchemaRows[i];
				string baseTableName = dbSchemaRow.BaseTableName;
				if (baseTableName == null || baseTableName.Length == 0)
				{
					sortedSchemaRows[i] = null;
				}
				else
				{
					string text5 = dbSchemaRow.BaseServerName;
					string text6 = dbSchemaRow.BaseCatalogName;
					string text7 = dbSchemaRow.BaseSchemaName;
					if (text5 == null)
					{
						text5 = string.Empty;
					}
					if (text6 == null)
					{
						text6 = string.Empty;
					}
					if (text7 == null)
					{
						text7 = string.Empty;
					}
					if (text4 == null)
					{
						text = text5;
						text2 = text6;
						text3 = text7;
						text4 = baseTableName;
					}
					else if (ADP.SrcCompare(text4, baseTableName) != 0 || ADP.SrcCompare(text3, text7) != 0 || ADP.SrcCompare(text2, text6) != 0 || ADP.SrcCompare(text, text5) != 0)
					{
						throw ADP.DynamicSQLJoinUnsupported();
					}
				}
			}
			if (text.Length == 0)
			{
				text = null;
			}
			if (text2.Length == 0)
			{
				text = null;
				text2 = null;
			}
			if (text3.Length == 0)
			{
				text = null;
				text2 = null;
				text3 = null;
			}
			if (text4 == null || text4.Length == 0)
			{
				throw ADP.DynamicSQLNoTableInfo();
			}
			CatalogLocation catalogLocation = this.CatalogLocation;
			string catalogSeparator = this.CatalogSeparator;
			string schemaSeparator = this.SchemaSeparator;
			string quotePrefix = this.QuotePrefix;
			string quoteSuffix = this.QuoteSuffix;
			if (!string.IsNullOrEmpty(quotePrefix) && -1 != text4.IndexOf(quotePrefix, StringComparison.Ordinal))
			{
				throw ADP.DynamicSQLNestedQuote(text4, quotePrefix);
			}
			if (!string.IsNullOrEmpty(quoteSuffix) && -1 != text4.IndexOf(quoteSuffix, StringComparison.Ordinal))
			{
				throw ADP.DynamicSQLNestedQuote(text4, quoteSuffix);
			}
			StringBuilder stringBuilder = new StringBuilder();
			if (CatalogLocation.Start == catalogLocation)
			{
				if (text != null)
				{
					stringBuilder.Append(ADP.BuildQuotedString(quotePrefix, quoteSuffix, text));
					stringBuilder.Append(catalogSeparator);
				}
				if (text2 != null)
				{
					stringBuilder.Append(ADP.BuildQuotedString(quotePrefix, quoteSuffix, text2));
					stringBuilder.Append(catalogSeparator);
				}
			}
			if (text3 != null)
			{
				stringBuilder.Append(ADP.BuildQuotedString(quotePrefix, quoteSuffix, text3));
				stringBuilder.Append(schemaSeparator);
			}
			stringBuilder.Append(ADP.BuildQuotedString(quotePrefix, quoteSuffix, text4));
			if (CatalogLocation.End == catalogLocation)
			{
				if (text != null)
				{
					stringBuilder.Append(catalogSeparator);
					stringBuilder.Append(ADP.BuildQuotedString(quotePrefix, quoteSuffix, text));
				}
				if (text2 != null)
				{
					stringBuilder.Append(catalogSeparator);
					stringBuilder.Append(ADP.BuildQuotedString(quotePrefix, quoteSuffix, text2));
				}
			}
			this._quotedBaseTableName = stringBuilder.ToString();
			this._hasPartialPrimaryKey = false;
			foreach (DbSchemaRow dbSchemaRow2 in sortedSchemaRows)
			{
				if (dbSchemaRow2 != null && (dbSchemaRow2.IsKey || dbSchemaRow2.IsUnique) && !dbSchemaRow2.IsLong && !dbSchemaRow2.IsRowVersion && dbSchemaRow2.IsHidden)
				{
					this._hasPartialPrimaryKey = true;
					break;
				}
			}
			this._dbSchemaRows = sortedSchemaRows;
		}

		// Token: 0x06000BE7 RID: 3047 RVA: 0x00041740 File Offset: 0x0003F940
		private DbCommand BuildDeleteCommand(DataTableMapping mappings, DataRow dataRow)
		{
			DbCommand dbCommand = this.InitializeCommand(this.DeleteCommand);
			StringBuilder stringBuilder = new StringBuilder();
			int num = 0;
			stringBuilder.Append("DELETE FROM ");
			stringBuilder.Append(this.QuotedBaseTableName);
			num = this.BuildWhereClause(mappings, dataRow, stringBuilder, dbCommand, num, false);
			dbCommand.CommandText = stringBuilder.ToString();
			DbCommandBuilder.RemoveExtraParameters(dbCommand, num);
			this.DeleteCommand = dbCommand;
			return dbCommand;
		}

		// Token: 0x06000BE8 RID: 3048 RVA: 0x000417A4 File Offset: 0x0003F9A4
		private DbCommand BuildInsertCommand(DataTableMapping mappings, DataRow dataRow)
		{
			DbCommand dbCommand = this.InitializeCommand(this.InsertCommand);
			StringBuilder stringBuilder = new StringBuilder();
			int num = 0;
			string text = " (";
			stringBuilder.Append("INSERT INTO ");
			stringBuilder.Append(this.QuotedBaseTableName);
			DbSchemaRow[] dbSchemaRows = this._dbSchemaRows;
			string[] array = new string[dbSchemaRows.Length];
			for (int i = 0; i < dbSchemaRows.Length; i++)
			{
				DbSchemaRow dbSchemaRow = dbSchemaRows[i];
				if (dbSchemaRow != null && dbSchemaRow.BaseColumnName.Length != 0 && this.IncludeInInsertValues(dbSchemaRow))
				{
					object obj = null;
					string text2 = this._sourceColumnNames[i];
					if (mappings != null && dataRow != null)
					{
						DataColumn dataColumn = this.GetDataColumn(text2, mappings, dataRow);
						if (dataColumn == null || (dbSchemaRow.IsReadOnly && dataColumn.ReadOnly))
						{
							goto IL_011E;
						}
						obj = this.GetColumnValue(dataRow, dataColumn, DataRowVersion.Current);
						if (!dbSchemaRow.AllowDBNull && (obj == null || Convert.IsDBNull(obj)))
						{
							goto IL_011E;
						}
					}
					stringBuilder.Append(text);
					text = ", ";
					stringBuilder.Append(this.QuotedColumn(dbSchemaRow.BaseColumnName));
					array[num] = this.CreateParameterForValue(dbCommand, this.GetBaseParameterName(i), text2, DataRowVersion.Current, num, obj, dbSchemaRow, StatementType.Insert, false);
					num++;
				}
				IL_011E:;
			}
			if (num == 0)
			{
				stringBuilder.Append(" DEFAULT VALUES");
			}
			else
			{
				stringBuilder.Append(")");
				stringBuilder.Append(" VALUES ");
				stringBuilder.Append("(");
				stringBuilder.Append(array[0]);
				for (int j = 1; j < num; j++)
				{
					stringBuilder.Append(", ");
					stringBuilder.Append(array[j]);
				}
				stringBuilder.Append(")");
			}
			dbCommand.CommandText = stringBuilder.ToString();
			DbCommandBuilder.RemoveExtraParameters(dbCommand, num);
			this.InsertCommand = dbCommand;
			return dbCommand;
		}

		// Token: 0x06000BE9 RID: 3049 RVA: 0x00041970 File Offset: 0x0003FB70
		private DbCommand BuildUpdateCommand(DataTableMapping mappings, DataRow dataRow)
		{
			DbCommand dbCommand = this.InitializeCommand(this.UpdateCommand);
			StringBuilder stringBuilder = new StringBuilder();
			string text = " SET ";
			int num = 0;
			stringBuilder.Append("UPDATE ");
			stringBuilder.Append(this.QuotedBaseTableName);
			DbSchemaRow[] dbSchemaRows = this._dbSchemaRows;
			for (int i = 0; i < dbSchemaRows.Length; i++)
			{
				DbSchemaRow dbSchemaRow = dbSchemaRows[i];
				if (dbSchemaRow != null && dbSchemaRow.BaseColumnName.Length != 0 && this.IncludeInUpdateSet(dbSchemaRow))
				{
					object obj = null;
					string text2 = this._sourceColumnNames[i];
					if (mappings != null && dataRow != null)
					{
						DataColumn dataColumn = this.GetDataColumn(text2, mappings, dataRow);
						if (dataColumn == null || (dbSchemaRow.IsReadOnly && dataColumn.ReadOnly))
						{
							goto IL_013F;
						}
						obj = this.GetColumnValue(dataRow, dataColumn, DataRowVersion.Current);
						if (!this.SetAllValues)
						{
							object columnValue = this.GetColumnValue(dataRow, dataColumn, DataRowVersion.Original);
							if (columnValue == obj || (columnValue != null && columnValue.Equals(obj)))
							{
								goto IL_013F;
							}
						}
					}
					stringBuilder.Append(text);
					text = ", ";
					stringBuilder.Append(this.QuotedColumn(dbSchemaRow.BaseColumnName));
					stringBuilder.Append(" = ");
					stringBuilder.Append(this.CreateParameterForValue(dbCommand, this.GetBaseParameterName(i), text2, DataRowVersion.Current, num, obj, dbSchemaRow, StatementType.Update, false));
					num++;
				}
				IL_013F:;
			}
			bool flag = num == 0;
			num = this.BuildWhereClause(mappings, dataRow, stringBuilder, dbCommand, num, true);
			dbCommand.CommandText = stringBuilder.ToString();
			DbCommandBuilder.RemoveExtraParameters(dbCommand, num);
			this.UpdateCommand = dbCommand;
			if (!flag)
			{
				return dbCommand;
			}
			return null;
		}

		// Token: 0x06000BEA RID: 3050 RVA: 0x00041B00 File Offset: 0x0003FD00
		private int BuildWhereClause(DataTableMapping mappings, DataRow dataRow, StringBuilder builder, DbCommand command, int parameterCount, bool isUpdate)
		{
			string text = string.Empty;
			int num = 0;
			builder.Append(" WHERE ");
			builder.Append("(");
			DbSchemaRow[] dbSchemaRows = this._dbSchemaRows;
			for (int i = 0; i < dbSchemaRows.Length; i++)
			{
				DbSchemaRow dbSchemaRow = dbSchemaRows[i];
				if (dbSchemaRow != null && dbSchemaRow.BaseColumnName.Length != 0 && this.IncludeInWhereClause(dbSchemaRow, isUpdate))
				{
					builder.Append(text);
					text = " AND ";
					object obj = null;
					string text2 = this._sourceColumnNames[i];
					string text3 = this.QuotedColumn(dbSchemaRow.BaseColumnName);
					if (mappings != null && dataRow != null)
					{
						obj = this.GetColumnValue(dataRow, text2, mappings, DataRowVersion.Original);
					}
					if (!dbSchemaRow.AllowDBNull)
					{
						builder.Append("(");
						builder.Append(text3);
						builder.Append(" = ");
						builder.Append(this.CreateParameterForValue(command, this.GetOriginalParameterName(i), text2, DataRowVersion.Original, parameterCount, obj, dbSchemaRow, isUpdate ? StatementType.Update : StatementType.Delete, true));
						parameterCount++;
						builder.Append(")");
					}
					else
					{
						builder.Append("(");
						builder.Append("(");
						builder.Append(this.CreateParameterForNullTest(command, this.GetNullParameterName(i), text2, DataRowVersion.Original, parameterCount, obj, dbSchemaRow, isUpdate ? StatementType.Update : StatementType.Delete, true));
						parameterCount++;
						builder.Append(" = 1");
						builder.Append(" AND ");
						builder.Append(text3);
						builder.Append(" IS NULL");
						builder.Append(")");
						builder.Append(" OR ");
						builder.Append("(");
						builder.Append(text3);
						builder.Append(" = ");
						builder.Append(this.CreateParameterForValue(command, this.GetOriginalParameterName(i), text2, DataRowVersion.Original, parameterCount, obj, dbSchemaRow, isUpdate ? StatementType.Update : StatementType.Delete, true));
						parameterCount++;
						builder.Append(")");
						builder.Append(")");
					}
					if (this.IncrementWhereCount(dbSchemaRow))
					{
						num++;
					}
				}
			}
			builder.Append(")");
			if (num != 0)
			{
				return parameterCount;
			}
			if (isUpdate)
			{
				if (ConflictOption.CompareRowVersion == this.ConflictOption)
				{
					throw ADP.DynamicSQLNoKeyInfoRowVersionUpdate();
				}
				throw ADP.DynamicSQLNoKeyInfoUpdate();
			}
			else
			{
				if (ConflictOption.CompareRowVersion == this.ConflictOption)
				{
					throw ADP.DynamicSQLNoKeyInfoRowVersionDelete();
				}
				throw ADP.DynamicSQLNoKeyInfoDelete();
			}
		}

		// Token: 0x06000BEB RID: 3051 RVA: 0x00041D6C File Offset: 0x0003FF6C
		private string CreateParameterForNullTest(DbCommand command, string parameterName, string sourceColumn, DataRowVersion version, int parameterCount, object value, DbSchemaRow row, StatementType statementType, bool whereClause)
		{
			DbParameter nextParameter = DbCommandBuilder.GetNextParameter(command, parameterCount);
			if (parameterName == null)
			{
				nextParameter.ParameterName = this.GetParameterName(1 + parameterCount);
			}
			else
			{
				nextParameter.ParameterName = parameterName;
			}
			nextParameter.Direction = ParameterDirection.Input;
			nextParameter.SourceColumn = sourceColumn;
			nextParameter.SourceVersion = version;
			nextParameter.SourceColumnNullMapping = true;
			nextParameter.Value = value;
			nextParameter.Size = 0;
			this.ApplyParameterInfo(nextParameter, row.DataRow, statementType, whereClause);
			nextParameter.DbType = DbType.Int32;
			nextParameter.Value = (ADP.IsNull(value) ? DbDataAdapter.s_parameterValueNullValue : DbDataAdapter.s_parameterValueNonNullValue);
			if (!command.Parameters.Contains(nextParameter))
			{
				command.Parameters.Add(nextParameter);
			}
			if (parameterName == null)
			{
				return this.GetParameterPlaceholder(1 + parameterCount);
			}
			return string.Format(CultureInfo.InvariantCulture, this._parameterMarkerFormat, parameterName);
		}

		// Token: 0x06000BEC RID: 3052 RVA: 0x00041E3C File Offset: 0x0004003C
		private string CreateParameterForValue(DbCommand command, string parameterName, string sourceColumn, DataRowVersion version, int parameterCount, object value, DbSchemaRow row, StatementType statementType, bool whereClause)
		{
			DbParameter nextParameter = DbCommandBuilder.GetNextParameter(command, parameterCount);
			if (parameterName == null)
			{
				nextParameter.ParameterName = this.GetParameterName(1 + parameterCount);
			}
			else
			{
				nextParameter.ParameterName = parameterName;
			}
			nextParameter.Direction = ParameterDirection.Input;
			nextParameter.SourceColumn = sourceColumn;
			nextParameter.SourceVersion = version;
			nextParameter.SourceColumnNullMapping = false;
			nextParameter.Value = value;
			nextParameter.Size = 0;
			this.ApplyParameterInfo(nextParameter, row.DataRow, statementType, whereClause);
			if (!command.Parameters.Contains(nextParameter))
			{
				command.Parameters.Add(nextParameter);
			}
			if (parameterName == null)
			{
				return this.GetParameterPlaceholder(1 + parameterCount);
			}
			return string.Format(CultureInfo.InvariantCulture, this._parameterMarkerFormat, parameterName);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Data.Common.DbCommandBuilder" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
		// Token: 0x06000BED RID: 3053 RVA: 0x00041EE6 File Offset: 0x000400E6
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.DataAdapter = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000BEE RID: 3054 RVA: 0x00041EF9 File Offset: 0x000400F9
		private string GetBaseParameterName(int index)
		{
			if (this._parameterNames != null)
			{
				return this._parameterNames.GetBaseParameterName(index);
			}
			return null;
		}

		// Token: 0x06000BEF RID: 3055 RVA: 0x00041F11 File Offset: 0x00040111
		private string GetOriginalParameterName(int index)
		{
			if (this._parameterNames != null)
			{
				return this._parameterNames.GetOriginalParameterName(index);
			}
			return null;
		}

		// Token: 0x06000BF0 RID: 3056 RVA: 0x00041F29 File Offset: 0x00040129
		private string GetNullParameterName(int index)
		{
			if (this._parameterNames != null)
			{
				return this._parameterNames.GetNullParameterName(index);
			}
			return null;
		}

		// Token: 0x06000BF1 RID: 3057 RVA: 0x00041F44 File Offset: 0x00040144
		private DbCommand GetSelectCommand()
		{
			DbCommand dbCommand = null;
			DbDataAdapter dataAdapter = this.DataAdapter;
			if (dataAdapter != null)
			{
				if (this._missingMappingAction == (MissingMappingAction)0)
				{
					this._missingMappingAction = dataAdapter.MissingMappingAction;
				}
				dbCommand = dataAdapter.SelectCommand;
			}
			if (dbCommand == null)
			{
				throw ADP.MissingSourceCommand();
			}
			return dbCommand;
		}

		// Token: 0x06000BF2 RID: 3058 RVA: 0x00041F82 File Offset: 0x00040182
		private object GetColumnValue(DataRow row, string columnName, DataTableMapping mappings, DataRowVersion version)
		{
			return this.GetColumnValue(row, this.GetDataColumn(columnName, mappings, row), version);
		}

		// Token: 0x06000BF3 RID: 3059 RVA: 0x00041F98 File Offset: 0x00040198
		private object GetColumnValue(DataRow row, DataColumn column, DataRowVersion version)
		{
			object obj = null;
			if (column != null)
			{
				obj = row[column, version];
			}
			return obj;
		}

		// Token: 0x06000BF4 RID: 3060 RVA: 0x00041FB4 File Offset: 0x000401B4
		private DataColumn GetDataColumn(string columnName, DataTableMapping tablemapping, DataRow row)
		{
			DataColumn dataColumn = null;
			if (!string.IsNullOrEmpty(columnName))
			{
				dataColumn = tablemapping.GetDataColumn(columnName, null, row.Table, this._missingMappingAction, MissingSchemaAction.Error);
			}
			return dataColumn;
		}

		// Token: 0x06000BF5 RID: 3061 RVA: 0x00041FE4 File Offset: 0x000401E4
		private static DbParameter GetNextParameter(DbCommand command, int pcount)
		{
			DbParameter dbParameter;
			if (pcount < command.Parameters.Count)
			{
				dbParameter = command.Parameters[pcount];
			}
			else
			{
				dbParameter = command.CreateParameter();
			}
			return dbParameter;
		}

		// Token: 0x06000BF6 RID: 3062 RVA: 0x00042016 File Offset: 0x00040216
		private bool IncludeInInsertValues(DbSchemaRow row)
		{
			return !row.IsAutoIncrement && !row.IsHidden && !row.IsExpression && !row.IsRowVersion && !row.IsReadOnly;
		}

		// Token: 0x06000BF7 RID: 3063 RVA: 0x00042043 File Offset: 0x00040243
		private bool IncludeInUpdateSet(DbSchemaRow row)
		{
			return !row.IsAutoIncrement && !row.IsRowVersion && !row.IsHidden && !row.IsReadOnly;
		}

		// Token: 0x06000BF8 RID: 3064 RVA: 0x00042068 File Offset: 0x00040268
		private bool IncludeInWhereClause(DbSchemaRow row, bool isUpdate)
		{
			bool flag = this.IncrementWhereCount(row);
			if (!flag || !row.IsHidden)
			{
				if (!flag && ConflictOption.CompareAllSearchableValues == this.ConflictOption)
				{
					flag = !row.IsLong && !row.IsRowVersion && !row.IsHidden;
				}
				return flag;
			}
			if (ConflictOption.CompareRowVersion == this.ConflictOption)
			{
				throw ADP.DynamicSQLNoKeyInfoRowVersionUpdate();
			}
			throw ADP.DynamicSQLNoKeyInfoUpdate();
		}

		// Token: 0x06000BF9 RID: 3065 RVA: 0x000420C8 File Offset: 0x000402C8
		private bool IncrementWhereCount(DbSchemaRow row)
		{
			ConflictOption conflictOption = this.ConflictOption;
			switch (conflictOption)
			{
			case ConflictOption.CompareAllSearchableValues:
			case ConflictOption.OverwriteChanges:
				return (row.IsKey || row.IsUnique) && !row.IsLong && !row.IsRowVersion;
			case ConflictOption.CompareRowVersion:
				return (((row.IsKey || row.IsUnique) && !this._hasPartialPrimaryKey) || row.IsRowVersion) && !row.IsLong;
			default:
				throw ADP.InvalidConflictOptions(conflictOption);
			}
		}

		/// <summary>Resets the <see cref="P:System.Data.Common.DbCommand.CommandTimeout" />, <see cref="P:System.Data.Common.DbCommand.Transaction" />, <see cref="P:System.Data.Common.DbCommand.CommandType" />, and <see cref="T:System.Data.UpdateRowSource" /> properties on the <see cref="T:System.Data.Common.DbCommand" />.</summary>
		/// <returns>A <see cref="T:System.Data.Common.DbCommand" /> instance to use for each insert, update, or delete operation. Passing a null value allows the <see cref="M:System.Data.Common.DbCommandBuilder.InitializeCommand(System.Data.Common.DbCommand)" /> method to create a <see cref="T:System.Data.Common.DbCommand" /> object based on the Select command associated with the <see cref="T:System.Data.Common.DbCommandBuilder" />.</returns>
		/// <param name="command">The <see cref="T:System.Data.Common.DbCommand" /> to be used by the command builder for the corresponding insert, update, or delete command.</param>
		// Token: 0x06000BFA RID: 3066 RVA: 0x00042148 File Offset: 0x00040348
		protected virtual DbCommand InitializeCommand(DbCommand command)
		{
			if (command == null)
			{
				DbCommand selectCommand = this.GetSelectCommand();
				command = selectCommand.Connection.CreateCommand();
				command.CommandTimeout = selectCommand.CommandTimeout;
				command.Transaction = selectCommand.Transaction;
			}
			command.CommandType = CommandType.Text;
			command.UpdatedRowSource = UpdateRowSource.None;
			return command;
		}

		// Token: 0x06000BFB RID: 3067 RVA: 0x00042193 File Offset: 0x00040393
		private string QuotedColumn(string column)
		{
			return ADP.BuildQuotedString(this.QuotePrefix, this.QuoteSuffix, column);
		}

		/// <summary>Given an unquoted identifier in the correct catalog case, returns the correct quoted form of that identifier, including properly escaping any embedded quotes in the identifier.</summary>
		/// <returns>The quoted version of the identifier. Embedded quotes within the identifier are properly escaped.</returns>
		/// <param name="unquotedIdentifier">The original unquoted identifier.</param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000BFC RID: 3068 RVA: 0x000421A7 File Offset: 0x000403A7
		public virtual string QuoteIdentifier(string unquotedIdentifier)
		{
			throw ADP.NotSupported();
		}

		/// <summary>Clears the commands associated with this <see cref="T:System.Data.Common.DbCommandBuilder" />.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000BFD RID: 3069 RVA: 0x000421B0 File Offset: 0x000403B0
		public virtual void RefreshSchema()
		{
			this._dbSchemaTable = null;
			this._dbSchemaRows = null;
			this._sourceColumnNames = null;
			this._quotedBaseTableName = null;
			DbDataAdapter dataAdapter = this.DataAdapter;
			if (dataAdapter != null)
			{
				if (this.InsertCommand == dataAdapter.InsertCommand)
				{
					dataAdapter.InsertCommand = null;
				}
				if (this.UpdateCommand == dataAdapter.UpdateCommand)
				{
					dataAdapter.UpdateCommand = null;
				}
				if (this.DeleteCommand == dataAdapter.DeleteCommand)
				{
					dataAdapter.DeleteCommand = null;
				}
			}
			DbCommand dbCommand;
			if ((dbCommand = this.InsertCommand) != null)
			{
				dbCommand.Dispose();
			}
			if ((dbCommand = this.UpdateCommand) != null)
			{
				dbCommand.Dispose();
			}
			if ((dbCommand = this.DeleteCommand) != null)
			{
				dbCommand.Dispose();
			}
			this.InsertCommand = null;
			this.UpdateCommand = null;
			this.DeleteCommand = null;
		}

		// Token: 0x06000BFE RID: 3070 RVA: 0x00042268 File Offset: 0x00040468
		private static void RemoveExtraParameters(DbCommand command, int usedParameterCount)
		{
			for (int i = command.Parameters.Count - 1; i >= usedParameterCount; i--)
			{
				command.Parameters.RemoveAt(i);
			}
		}

		/// <summary>Adds an event handler for the <see cref="E:System.Data.OleDb.OleDbDataAdapter.RowUpdating" /> event.</summary>
		/// <param name="rowUpdatingEvent">A <see cref="T:System.Data.Common.RowUpdatingEventArgs" /> instance containing information about the event.</param>
		// Token: 0x06000BFF RID: 3071 RVA: 0x0004229C File Offset: 0x0004049C
		protected void RowUpdatingHandler(RowUpdatingEventArgs rowUpdatingEvent)
		{
			if (rowUpdatingEvent == null)
			{
				throw ADP.ArgumentNull("rowUpdatingEvent");
			}
			try
			{
				if (rowUpdatingEvent.Status == UpdateStatus.Continue)
				{
					StatementType statementType = rowUpdatingEvent.StatementType;
					DbCommand dbCommand = (DbCommand)rowUpdatingEvent.Command;
					if (dbCommand != null)
					{
						switch (statementType)
						{
						case StatementType.Select:
							return;
						case StatementType.Insert:
							dbCommand = this.InsertCommand;
							break;
						case StatementType.Update:
							dbCommand = this.UpdateCommand;
							break;
						case StatementType.Delete:
							dbCommand = this.DeleteCommand;
							break;
						default:
							throw ADP.InvalidStatementType(statementType);
						}
						if (dbCommand != rowUpdatingEvent.Command)
						{
							dbCommand = (DbCommand)rowUpdatingEvent.Command;
							if (dbCommand != null && dbCommand.Connection == null)
							{
								DbDataAdapter dataAdapter = this.DataAdapter;
								DbCommand dbCommand2 = ((dataAdapter != null) ? dataAdapter.SelectCommand : null);
								if (dbCommand2 != null)
								{
									dbCommand.Connection = dbCommand2.Connection;
								}
							}
						}
						else
						{
							dbCommand = null;
						}
					}
					if (dbCommand == null)
					{
						this.RowUpdatingHandlerBuilder(rowUpdatingEvent);
					}
				}
			}
			catch (Exception ex) when (ADP.IsCatchableExceptionType(ex))
			{
				ADP.TraceExceptionForCapture(ex);
				rowUpdatingEvent.Status = UpdateStatus.ErrorsOccurred;
				rowUpdatingEvent.Errors = ex;
			}
		}

		// Token: 0x06000C00 RID: 3072 RVA: 0x000423B4 File Offset: 0x000405B4
		private void RowUpdatingHandlerBuilder(RowUpdatingEventArgs rowUpdatingEvent)
		{
			DataRow row = rowUpdatingEvent.Row;
			this.BuildCache(false, row, false);
			DbCommand dbCommand;
			switch (rowUpdatingEvent.StatementType)
			{
			case StatementType.Insert:
				dbCommand = this.BuildInsertCommand(rowUpdatingEvent.TableMapping, row);
				break;
			case StatementType.Update:
				dbCommand = this.BuildUpdateCommand(rowUpdatingEvent.TableMapping, row);
				break;
			case StatementType.Delete:
				dbCommand = this.BuildDeleteCommand(rowUpdatingEvent.TableMapping, row);
				break;
			default:
				throw ADP.InvalidStatementType(rowUpdatingEvent.StatementType);
			}
			if (dbCommand == null)
			{
				if (row != null)
				{
					row.AcceptChanges();
				}
				rowUpdatingEvent.Status = UpdateStatus.SkipCurrentRow;
			}
			rowUpdatingEvent.Command = dbCommand;
		}

		/// <summary>Given a quoted identifier, returns the correct unquoted form of that identifier, including properly un-escaping any embedded quotes in the identifier.</summary>
		/// <returns>The unquoted identifier, with embedded quotes properly un-escaped.</returns>
		/// <param name="quotedIdentifier">The identifier that will have its embedded quotes removed.</param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000C01 RID: 3073 RVA: 0x000421A7 File Offset: 0x000403A7
		public virtual string UnquoteIdentifier(string quotedIdentifier)
		{
			throw ADP.NotSupported();
		}

		/// <summary>Allows the provider implementation of the <see cref="T:System.Data.Common.DbCommandBuilder" /> class to handle additional parameter properties.</summary>
		/// <param name="parameter">A <see cref="T:System.Data.Common.DbParameter" /> to which the additional modifications are applied. </param>
		/// <param name="row">The <see cref="T:System.Data.DataRow" /> from the schema table provided by <see cref="M:System.Data.Common.DbDataReader.GetSchemaTable" />. </param>
		/// <param name="statementType">The type of command being generated; INSERT, UPDATE or DELETE. </param>
		/// <param name="whereClause">true if the parameter is part of the update or delete WHERE clause, false if it is part of the insert or update values. </param>
		// Token: 0x06000C02 RID: 3074
		protected abstract void ApplyParameterInfo(DbParameter parameter, DataRow row, StatementType statementType, bool whereClause);

		/// <summary>Returns the name of the specified parameter in the format of @p#. Use when building a custom command builder.</summary>
		/// <returns>The name of the parameter with the specified number appended as part of the parameter name.</returns>
		/// <param name="parameterOrdinal">The number to be included as part of the parameter's name..</param>
		// Token: 0x06000C03 RID: 3075
		protected abstract string GetParameterName(int parameterOrdinal);

		/// <summary>Returns the full parameter name, given the partial parameter name.</summary>
		/// <returns>The full parameter name corresponding to the partial parameter name requested.</returns>
		/// <param name="parameterName">The partial name of the parameter.</param>
		// Token: 0x06000C04 RID: 3076
		protected abstract string GetParameterName(string parameterName);

		/// <summary>Returns the placeholder for the parameter in the associated SQL statement.</summary>
		/// <returns>The name of the parameter with the specified number appended.</returns>
		/// <param name="parameterOrdinal">The number to be included as part of the parameter's name.</param>
		// Token: 0x06000C05 RID: 3077
		protected abstract string GetParameterPlaceholder(int parameterOrdinal);

		/// <summary>Registers the <see cref="T:System.Data.Common.DbCommandBuilder" /> to handle the <see cref="E:System.Data.OleDb.OleDbDataAdapter.RowUpdating" /> event for a <see cref="T:System.Data.Common.DbDataAdapter" />. </summary>
		/// <param name="adapter">The <see cref="T:System.Data.Common.DbDataAdapter" /> to be used for the update.</param>
		// Token: 0x06000C06 RID: 3078
		protected abstract void SetRowUpdatingHandler(DbDataAdapter adapter);

		// Token: 0x040004AF RID: 1199
		private DbDataAdapter _dataAdapter;

		// Token: 0x040004B0 RID: 1200
		private DbCommand _insertCommand;

		// Token: 0x040004B1 RID: 1201
		private DbCommand _updateCommand;

		// Token: 0x040004B2 RID: 1202
		private DbCommand _deleteCommand;

		// Token: 0x040004B3 RID: 1203
		private MissingMappingAction _missingMappingAction;

		// Token: 0x040004B4 RID: 1204
		private ConflictOption _conflictDetection = ConflictOption.CompareAllSearchableValues;

		// Token: 0x040004B5 RID: 1205
		private bool _setAllValues;

		// Token: 0x040004B6 RID: 1206
		private bool _hasPartialPrimaryKey;

		// Token: 0x040004B7 RID: 1207
		private DataTable _dbSchemaTable;

		// Token: 0x040004B8 RID: 1208
		private DbSchemaRow[] _dbSchemaRows;

		// Token: 0x040004B9 RID: 1209
		private string[] _sourceColumnNames;

		// Token: 0x040004BA RID: 1210
		private DbCommandBuilder.ParameterNames _parameterNames;

		// Token: 0x040004BB RID: 1211
		private string _quotedBaseTableName;

		// Token: 0x040004BC RID: 1212
		private CatalogLocation _catalogLocation = CatalogLocation.Start;

		// Token: 0x040004BD RID: 1213
		private string _catalogSeparator = ".";

		// Token: 0x040004BE RID: 1214
		private string _schemaSeparator = ".";

		// Token: 0x040004BF RID: 1215
		private string _quotePrefix = string.Empty;

		// Token: 0x040004C0 RID: 1216
		private string _quoteSuffix = string.Empty;

		// Token: 0x040004C1 RID: 1217
		private string _parameterNamePattern;

		// Token: 0x040004C2 RID: 1218
		private string _parameterMarkerFormat;

		// Token: 0x040004C3 RID: 1219
		private int _parameterNameMaxLength;

		// Token: 0x020000E2 RID: 226
		private class ParameterNames
		{
			// Token: 0x06000C07 RID: 3079 RVA: 0x00042444 File Offset: 0x00040644
			internal ParameterNames(DbCommandBuilder dbCommandBuilder, DbSchemaRow[] schemaRows)
			{
				this._dbCommandBuilder = dbCommandBuilder;
				this._baseParameterNames = new string[schemaRows.Length];
				this._originalParameterNames = new string[schemaRows.Length];
				this._nullParameterNames = new string[schemaRows.Length];
				this._isMutatedName = new bool[schemaRows.Length];
				this._count = schemaRows.Length;
				this._parameterNameParser = new Regex(this._dbCommandBuilder.ParameterNamePattern, RegexOptions.ExplicitCapture | RegexOptions.Singleline);
				this.SetAndValidateNamePrefixes();
				this._adjustedParameterNameMaxLength = this.GetAdjustedParameterNameMaxLength();
				for (int i = 0; i < schemaRows.Length; i++)
				{
					if (schemaRows[i] != null)
					{
						bool flag = false;
						string text = schemaRows[i].ColumnName;
						if ((this._originalPrefix == null || !text.StartsWith(this._originalPrefix, StringComparison.OrdinalIgnoreCase)) && (this._isNullPrefix == null || !text.StartsWith(this._isNullPrefix, StringComparison.OrdinalIgnoreCase)))
						{
							if (text.IndexOf(' ') >= 0)
							{
								text = text.Replace(' ', '_');
								flag = true;
							}
							if (this._parameterNameParser.IsMatch(text) && text.Length <= this._adjustedParameterNameMaxLength)
							{
								this._baseParameterNames[i] = text;
								this._isMutatedName[i] = flag;
							}
						}
					}
				}
				this.EliminateConflictingNames();
				for (int j = 0; j < schemaRows.Length; j++)
				{
					if (this._baseParameterNames[j] != null)
					{
						if (this._originalPrefix != null)
						{
							this._originalParameterNames[j] = this._originalPrefix + this._baseParameterNames[j];
						}
						if (this._isNullPrefix != null && schemaRows[j].AllowDBNull)
						{
							this._nullParameterNames[j] = this._isNullPrefix + this._baseParameterNames[j];
						}
					}
				}
				this.ApplyProviderSpecificFormat();
				this.GenerateMissingNames(schemaRows);
			}

			// Token: 0x06000C08 RID: 3080 RVA: 0x000425DC File Offset: 0x000407DC
			private void SetAndValidateNamePrefixes()
			{
				if (this._parameterNameParser.IsMatch("IsNull_"))
				{
					this._isNullPrefix = "IsNull_";
				}
				else if (this._parameterNameParser.IsMatch("isnull"))
				{
					this._isNullPrefix = "isnull";
				}
				else if (this._parameterNameParser.IsMatch("ISNULL"))
				{
					this._isNullPrefix = "ISNULL";
				}
				else
				{
					this._isNullPrefix = null;
				}
				if (this._parameterNameParser.IsMatch("Original_"))
				{
					this._originalPrefix = "Original_";
					return;
				}
				if (this._parameterNameParser.IsMatch("original"))
				{
					this._originalPrefix = "original";
					return;
				}
				if (this._parameterNameParser.IsMatch("ORIGINAL"))
				{
					this._originalPrefix = "ORIGINAL";
					return;
				}
				this._originalPrefix = null;
			}

			// Token: 0x06000C09 RID: 3081 RVA: 0x000426B0 File Offset: 0x000408B0
			private void ApplyProviderSpecificFormat()
			{
				for (int i = 0; i < this._baseParameterNames.Length; i++)
				{
					if (this._baseParameterNames[i] != null)
					{
						this._baseParameterNames[i] = this._dbCommandBuilder.GetParameterName(this._baseParameterNames[i]);
					}
					if (this._originalParameterNames[i] != null)
					{
						this._originalParameterNames[i] = this._dbCommandBuilder.GetParameterName(this._originalParameterNames[i]);
					}
					if (this._nullParameterNames[i] != null)
					{
						this._nullParameterNames[i] = this._dbCommandBuilder.GetParameterName(this._nullParameterNames[i]);
					}
				}
			}

			// Token: 0x06000C0A RID: 3082 RVA: 0x00042740 File Offset: 0x00040940
			private void EliminateConflictingNames()
			{
				for (int i = 0; i < this._count - 1; i++)
				{
					string text = this._baseParameterNames[i];
					if (text != null)
					{
						for (int j = i + 1; j < this._count; j++)
						{
							if (ADP.CompareInsensitiveInvariant(text, this._baseParameterNames[j]))
							{
								int num = (this._isMutatedName[j] ? j : i);
								this._baseParameterNames[num] = null;
							}
						}
					}
				}
			}

			// Token: 0x06000C0B RID: 3083 RVA: 0x000427A8 File Offset: 0x000409A8
			internal void GenerateMissingNames(DbSchemaRow[] schemaRows)
			{
				for (int i = 0; i < this._baseParameterNames.Length; i++)
				{
					if (this._baseParameterNames[i] == null)
					{
						this._baseParameterNames[i] = this.GetNextGenericParameterName();
						this._originalParameterNames[i] = this.GetNextGenericParameterName();
						if (schemaRows[i] != null && schemaRows[i].AllowDBNull)
						{
							this._nullParameterNames[i] = this.GetNextGenericParameterName();
						}
					}
				}
			}

			// Token: 0x06000C0C RID: 3084 RVA: 0x00042810 File Offset: 0x00040A10
			private int GetAdjustedParameterNameMaxLength()
			{
				int num = Math.Max((this._isNullPrefix != null) ? this._isNullPrefix.Length : 0, (this._originalPrefix != null) ? this._originalPrefix.Length : 0) + this._dbCommandBuilder.GetParameterName("").Length;
				return this._dbCommandBuilder.ParameterNameMaxLength - num;
			}

			// Token: 0x06000C0D RID: 3085 RVA: 0x00042874 File Offset: 0x00040A74
			private string GetNextGenericParameterName()
			{
				bool flag;
				string parameterName;
				do
				{
					flag = false;
					this._genericParameterCount++;
					parameterName = this._dbCommandBuilder.GetParameterName(this._genericParameterCount);
					for (int i = 0; i < this._baseParameterNames.Length; i++)
					{
						if (ADP.CompareInsensitiveInvariant(this._baseParameterNames[i], parameterName))
						{
							flag = true;
							break;
						}
					}
				}
				while (flag);
				return parameterName;
			}

			// Token: 0x06000C0E RID: 3086 RVA: 0x000428CE File Offset: 0x00040ACE
			internal string GetBaseParameterName(int index)
			{
				return this._baseParameterNames[index];
			}

			// Token: 0x06000C0F RID: 3087 RVA: 0x000428D8 File Offset: 0x00040AD8
			internal string GetOriginalParameterName(int index)
			{
				return this._originalParameterNames[index];
			}

			// Token: 0x06000C10 RID: 3088 RVA: 0x000428E2 File Offset: 0x00040AE2
			internal string GetNullParameterName(int index)
			{
				return this._nullParameterNames[index];
			}

			// Token: 0x040004C4 RID: 1220
			private string _originalPrefix;

			// Token: 0x040004C5 RID: 1221
			private string _isNullPrefix;

			// Token: 0x040004C6 RID: 1222
			private Regex _parameterNameParser;

			// Token: 0x040004C7 RID: 1223
			private DbCommandBuilder _dbCommandBuilder;

			// Token: 0x040004C8 RID: 1224
			private string[] _baseParameterNames;

			// Token: 0x040004C9 RID: 1225
			private string[] _originalParameterNames;

			// Token: 0x040004CA RID: 1226
			private string[] _nullParameterNames;

			// Token: 0x040004CB RID: 1227
			private bool[] _isMutatedName;

			// Token: 0x040004CC RID: 1228
			private int _count;

			// Token: 0x040004CD RID: 1229
			private int _genericParameterCount;

			// Token: 0x040004CE RID: 1230
			private int _adjustedParameterNameMaxLength;
		}
	}
}
