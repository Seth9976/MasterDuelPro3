using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Globalization;

namespace Mono.Data.Sqlite
{
	// Token: 0x02000014 RID: 20
	public sealed class SqliteDataReader : DbDataReader
	{
		// Token: 0x0600010A RID: 266 RVA: 0x0000944C File Offset: 0x0000764C
		internal SqliteDataReader(SqliteCommand cmd, CommandBehavior behave)
		{
			this._command = cmd;
			this._version = this._command.Connection._version;
			this._commandBehavior = behave;
			this._activeStatementIndex = -1;
			this._activeStatement = null;
			this._rowsAffected = -1;
			this._fieldCount = 0;
			if (this._command != null)
			{
				this.NextResult();
			}
		}

		// Token: 0x0600010B RID: 267 RVA: 0x000094B4 File Offset: 0x000076B4
		public override void Close()
		{
			try
			{
				if (this._command != null)
				{
					try
					{
						try
						{
							if (this._version != 0L)
							{
								try
								{
									while (this.NextResult())
									{
									}
								}
								catch
								{
								}
							}
							this._command.ClearDataReader();
						}
						finally
						{
							if ((this._commandBehavior & CommandBehavior.CloseConnection) != CommandBehavior.Default && this._command.Connection != null)
							{
								this._command.Connection.Close();
							}
						}
					}
					finally
					{
						if (this._disposeCommand)
						{
							this._command.Dispose();
						}
					}
				}
				this._command = null;
				this._activeStatement = null;
				this._fieldTypeArray = null;
			}
			finally
			{
				if (this._keyInfo != null)
				{
					this._keyInfo.Dispose();
					this._keyInfo = null;
				}
			}
		}

		// Token: 0x0600010C RID: 268 RVA: 0x000095BC File Offset: 0x000077BC
		private void CheckClosed()
		{
			if (this._command == null)
			{
				throw new InvalidOperationException("DataReader has been closed");
			}
			if (this._version == 0L)
			{
				throw new SqliteException(4, "Execution was aborted by the user");
			}
			if (this._command.Connection.State != ConnectionState.Open || this._command.Connection._version != this._version)
			{
				throw new InvalidOperationException("Connection was closed, statement was terminated");
			}
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00009632 File Offset: 0x00007832
		private void CheckValidRow()
		{
			if (this._readingState != 0)
			{
				throw new InvalidOperationException("No current row");
			}
		}

		// Token: 0x0600010E RID: 270 RVA: 0x0000964A File Offset: 0x0000784A
		public override IEnumerator GetEnumerator()
		{
			return new DbEnumerator(this, (this._commandBehavior & CommandBehavior.CloseConnection) == CommandBehavior.CloseConnection);
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600010F RID: 271 RVA: 0x00009660 File Offset: 0x00007860
		public override int FieldCount
		{
			get
			{
				this.CheckClosed();
				if (this._keyInfo == null)
				{
					return this._fieldCount;
				}
				return this._fieldCount + this._keyInfo.Count;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000110 RID: 272 RVA: 0x00009697 File Offset: 0x00007897
		public override int VisibleFieldCount
		{
			get
			{
				this.CheckClosed();
				return this._fieldCount;
			}
		}

		// Token: 0x06000111 RID: 273 RVA: 0x000096A8 File Offset: 0x000078A8
		private TypeAffinity VerifyType(int i, DbType typ)
		{
			this.CheckClosed();
			this.CheckValidRow();
			TypeAffinity affinity = this.GetSQLiteType(i).Affinity;
			switch (affinity)
			{
			case TypeAffinity.Int64:
				if (typ == DbType.Int16)
				{
					return affinity;
				}
				if (typ == DbType.Int32)
				{
					return affinity;
				}
				if (typ == DbType.Int64)
				{
					return affinity;
				}
				if (typ == DbType.Boolean)
				{
					return affinity;
				}
				if (typ == DbType.Byte)
				{
					return affinity;
				}
				if (typ == DbType.DateTime)
				{
					return affinity;
				}
				if (typ == DbType.Single)
				{
					return affinity;
				}
				if (typ == DbType.Double)
				{
					return affinity;
				}
				if (typ == DbType.Decimal)
				{
					return affinity;
				}
				break;
			case TypeAffinity.Double:
				if (typ == DbType.Single)
				{
					return affinity;
				}
				if (typ == DbType.Double)
				{
					return affinity;
				}
				if (typ == DbType.Decimal)
				{
					return affinity;
				}
				if (typ == DbType.DateTime)
				{
					return affinity;
				}
				break;
			case TypeAffinity.Text:
				if (typ == DbType.SByte)
				{
					return affinity;
				}
				if (typ == DbType.String)
				{
					return affinity;
				}
				if (typ == DbType.SByte)
				{
					return affinity;
				}
				if (typ == DbType.Guid)
				{
					return affinity;
				}
				if (typ == DbType.DateTime)
				{
					return affinity;
				}
				if (typ == DbType.Decimal)
				{
					return affinity;
				}
				break;
			case TypeAffinity.Blob:
				if (typ == DbType.Guid)
				{
					return affinity;
				}
				if (typ == DbType.String)
				{
					return affinity;
				}
				if (typ == DbType.Binary)
				{
					return affinity;
				}
				break;
			}
			throw new InvalidCastException();
		}

		// Token: 0x06000112 RID: 274 RVA: 0x000097D8 File Offset: 0x000079D8
		public override bool GetBoolean(int i)
		{
			if (i >= this.VisibleFieldCount && this._keyInfo != null)
			{
				return this._keyInfo.GetBoolean(i - this.VisibleFieldCount);
			}
			this.VerifyType(i, DbType.Boolean);
			return Convert.ToBoolean(this.GetValue(i), CultureInfo.CurrentCulture);
		}

		// Token: 0x06000113 RID: 275 RVA: 0x0000982C File Offset: 0x00007A2C
		public override string GetDataTypeName(int i)
		{
			if (i >= this.VisibleFieldCount && this._keyInfo != null)
			{
				return this._keyInfo.GetDataTypeName(i - this.VisibleFieldCount);
			}
			SQLiteType sqliteType = this.GetSQLiteType(i);
			if (sqliteType.Type == DbType.Object)
			{
				return SqliteConvert.SQLiteTypeToType(sqliteType).Name;
			}
			return this._activeStatement._sql.ColumnType(this._activeStatement, i, out sqliteType.Affinity);
		}

		// Token: 0x06000114 RID: 276 RVA: 0x000098A4 File Offset: 0x00007AA4
		public override Type GetFieldType(int i)
		{
			if (i >= this.VisibleFieldCount && this._keyInfo != null)
			{
				return this._keyInfo.GetFieldType(i - this.VisibleFieldCount);
			}
			return SqliteConvert.SQLiteTypeToType(this.GetSQLiteType(i));
		}

		// Token: 0x06000115 RID: 277 RVA: 0x000098E8 File Offset: 0x00007AE8
		public override int GetInt32(int i)
		{
			if (i >= this.VisibleFieldCount && this._keyInfo != null)
			{
				return this._keyInfo.GetInt32(i - this.VisibleFieldCount);
			}
			this.VerifyType(i, DbType.Int32);
			return this._activeStatement._sql.GetInt32(this._activeStatement, i);
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00009944 File Offset: 0x00007B44
		public override long GetInt64(int i)
		{
			if (i >= this.VisibleFieldCount && this._keyInfo != null)
			{
				return this._keyInfo.GetInt64(i - this.VisibleFieldCount);
			}
			this.VerifyType(i, DbType.Int64);
			return this._activeStatement._sql.GetInt64(this._activeStatement, i);
		}

		// Token: 0x06000117 RID: 279 RVA: 0x000099A0 File Offset: 0x00007BA0
		public override string GetName(int i)
		{
			if (i >= this.VisibleFieldCount && this._keyInfo != null)
			{
				return this._keyInfo.GetName(i - this.VisibleFieldCount);
			}
			return this._activeStatement._sql.ColumnName(this._activeStatement, i);
		}

		// Token: 0x06000118 RID: 280 RVA: 0x000099EF File Offset: 0x00007BEF
		public override DataTable GetSchemaTable()
		{
			return this.GetSchemaTable(true, false);
		}

		// Token: 0x06000119 RID: 281 RVA: 0x000099FC File Offset: 0x00007BFC
		internal DataTable GetSchemaTable(bool wantUniqueInfo, bool wantDefaultValue)
		{
			this.CheckClosed();
			DataTable dataTable = new DataTable("SchemaTable");
			DataTable dataTable2 = null;
			string text = string.Empty;
			string text2 = string.Empty;
			string text3 = string.Empty;
			dataTable.Locale = CultureInfo.InvariantCulture;
			dataTable.Columns.Add(SchemaTableColumn.ColumnName, typeof(string));
			dataTable.Columns.Add(SchemaTableColumn.ColumnOrdinal, typeof(int));
			dataTable.Columns.Add(SchemaTableColumn.ColumnSize, typeof(int));
			dataTable.Columns.Add(SchemaTableColumn.NumericPrecision, typeof(short));
			dataTable.Columns.Add(SchemaTableColumn.NumericScale, typeof(short));
			dataTable.Columns.Add(SchemaTableColumn.IsUnique, typeof(bool));
			dataTable.Columns.Add(SchemaTableColumn.IsKey, typeof(bool));
			dataTable.Columns.Add(SchemaTableOptionalColumn.BaseServerName, typeof(string));
			dataTable.Columns.Add(SchemaTableOptionalColumn.BaseCatalogName, typeof(string));
			dataTable.Columns.Add(SchemaTableColumn.BaseColumnName, typeof(string));
			dataTable.Columns.Add(SchemaTableColumn.BaseSchemaName, typeof(string));
			dataTable.Columns.Add(SchemaTableColumn.BaseTableName, typeof(string));
			dataTable.Columns.Add(SchemaTableColumn.DataType, typeof(Type));
			dataTable.Columns.Add(SchemaTableColumn.AllowDBNull, typeof(bool));
			dataTable.Columns.Add(SchemaTableColumn.ProviderType, typeof(int));
			dataTable.Columns.Add(SchemaTableColumn.IsAliased, typeof(bool));
			dataTable.Columns.Add(SchemaTableColumn.IsExpression, typeof(bool));
			dataTable.Columns.Add(SchemaTableOptionalColumn.IsAutoIncrement, typeof(bool));
			dataTable.Columns.Add(SchemaTableOptionalColumn.IsRowVersion, typeof(bool));
			dataTable.Columns.Add(SchemaTableOptionalColumn.IsHidden, typeof(bool));
			dataTable.Columns.Add(SchemaTableColumn.IsLong, typeof(bool));
			dataTable.Columns.Add(SchemaTableOptionalColumn.IsReadOnly, typeof(bool));
			dataTable.Columns.Add(SchemaTableOptionalColumn.ProviderSpecificDataType, typeof(Type));
			dataTable.Columns.Add(SchemaTableOptionalColumn.DefaultValue, typeof(object));
			dataTable.Columns.Add("DataTypeName", typeof(string));
			dataTable.Columns.Add("CollationType", typeof(string));
			dataTable.BeginLoadData();
			for (int i = 0; i < this._fieldCount; i++)
			{
				DataRow dataRow = dataTable.NewRow();
				DbType type = this.GetSQLiteType(i).Type;
				dataRow[SchemaTableColumn.ColumnName] = this.GetName(i);
				dataRow[SchemaTableColumn.ColumnOrdinal] = i;
				dataRow[SchemaTableColumn.ColumnSize] = SqliteConvert.DbTypeToColumnSize(type);
				dataRow[SchemaTableColumn.NumericPrecision] = SqliteConvert.DbTypeToNumericPrecision(type);
				dataRow[SchemaTableColumn.NumericScale] = SqliteConvert.DbTypeToNumericScale(type);
				dataRow[SchemaTableColumn.ProviderType] = this.GetSQLiteType(i).Type;
				dataRow[SchemaTableColumn.IsLong] = false;
				dataRow[SchemaTableColumn.AllowDBNull] = true;
				dataRow[SchemaTableOptionalColumn.IsReadOnly] = false;
				dataRow[SchemaTableOptionalColumn.IsRowVersion] = false;
				dataRow[SchemaTableColumn.IsUnique] = false;
				dataRow[SchemaTableColumn.IsKey] = false;
				dataRow[SchemaTableOptionalColumn.IsAutoIncrement] = false;
				dataRow[SchemaTableColumn.DataType] = this.GetFieldType(i);
				dataRow[SchemaTableOptionalColumn.IsHidden] = false;
				text3 = this._command.Connection._sql.ColumnOriginalName(this._activeStatement, i);
				if (!string.IsNullOrEmpty(text3))
				{
					dataRow[SchemaTableColumn.BaseColumnName] = text3;
				}
				dataRow[SchemaTableColumn.IsExpression] = string.IsNullOrEmpty(text3);
				dataRow[SchemaTableColumn.IsAliased] = string.Compare(this.GetName(i), text3, true, CultureInfo.InvariantCulture) != 0;
				string text4 = this._command.Connection._sql.ColumnTableName(this._activeStatement, i);
				if (!string.IsNullOrEmpty(text4))
				{
					dataRow[SchemaTableColumn.BaseTableName] = text4;
				}
				text4 = this._command.Connection._sql.ColumnDatabaseName(this._activeStatement, i);
				if (!string.IsNullOrEmpty(text4))
				{
					dataRow[SchemaTableOptionalColumn.BaseCatalogName] = text4;
				}
				string text5 = null;
				if (!string.IsNullOrEmpty(text3))
				{
					string text6;
					bool flag;
					bool flag2;
					bool flag3;
					this._command.Connection._sql.ColumnMetaData((string)dataRow[SchemaTableOptionalColumn.BaseCatalogName], (string)dataRow[SchemaTableColumn.BaseTableName], text3, out text5, out text6, out flag, out flag2, out flag3);
					if (flag || flag2)
					{
						dataRow[SchemaTableColumn.AllowDBNull] = false;
					}
					dataRow[SchemaTableColumn.IsKey] = flag2;
					dataRow[SchemaTableOptionalColumn.IsAutoIncrement] = flag3;
					dataRow["CollationType"] = text6;
					string[] array = text5.Split(new char[] { '(' });
					if (array.Length > 1)
					{
						text5 = array[0];
						array = array[1].Split(new char[] { ')' });
						if (array.Length > 1)
						{
							array = array[0].Split(new char[] { ',', '.' });
							if (this.GetSQLiteType(i).Type == DbType.String || this.GetSQLiteType(i).Type == DbType.Binary)
							{
								dataRow[SchemaTableColumn.ColumnSize] = Convert.ToInt32(array[0], CultureInfo.InvariantCulture);
							}
							else
							{
								dataRow[SchemaTableColumn.NumericPrecision] = Convert.ToInt32(array[0], CultureInfo.InvariantCulture);
								if (array.Length > 1)
								{
									dataRow[SchemaTableColumn.NumericScale] = Convert.ToInt32(array[1], CultureInfo.InvariantCulture);
								}
							}
						}
					}
					if (wantDefaultValue)
					{
						using (SqliteCommand sqliteCommand = new SqliteCommand(string.Format(CultureInfo.InvariantCulture, "PRAGMA [{0}].TABLE_INFO([{1}])", new object[]
						{
							dataRow[SchemaTableOptionalColumn.BaseCatalogName],
							dataRow[SchemaTableColumn.BaseTableName]
						}), this._command.Connection))
						{
							using (DbDataReader dbDataReader = sqliteCommand.ExecuteReader())
							{
								while (dbDataReader.Read())
								{
									if (string.Compare((string)dataRow[SchemaTableColumn.BaseColumnName], dbDataReader.GetString(1), true, CultureInfo.InvariantCulture) == 0)
									{
										if (!dbDataReader.IsDBNull(4))
										{
											dataRow[SchemaTableOptionalColumn.DefaultValue] = dbDataReader[4];
										}
										break;
									}
								}
							}
						}
					}
					if (wantUniqueInfo)
					{
						if ((string)dataRow[SchemaTableOptionalColumn.BaseCatalogName] != text || (string)dataRow[SchemaTableColumn.BaseTableName] != text2)
						{
							text = (string)dataRow[SchemaTableOptionalColumn.BaseCatalogName];
							text2 = (string)dataRow[SchemaTableColumn.BaseTableName];
							SqliteConnection connection = this._command.Connection;
							string text7 = "Indexes";
							string[] array2 = new string[4];
							array2[0] = (string)dataRow[SchemaTableOptionalColumn.BaseCatalogName];
							array2[2] = (string)dataRow[SchemaTableColumn.BaseTableName];
							dataTable2 = connection.GetSchema(text7, array2);
						}
						foreach (object obj in dataTable2.Rows)
						{
							DataRow dataRow2 = (DataRow)obj;
							SqliteConnection connection2 = this._command.Connection;
							string text8 = "IndexColumns";
							string[] array3 = new string[5];
							array3[0] = (string)dataRow[SchemaTableOptionalColumn.BaseCatalogName];
							array3[2] = (string)dataRow[SchemaTableColumn.BaseTableName];
							array3[3] = (string)dataRow2["INDEX_NAME"];
							DataTable schema = connection2.GetSchema(text8, array3);
							foreach (object obj2 in schema.Rows)
							{
								DataRow dataRow3 = (DataRow)obj2;
								if (string.Compare((string)dataRow3["COLUMN_NAME"], text3, true, CultureInfo.InvariantCulture) == 0)
								{
									if (schema.Rows.Count == 1 && !(bool)dataRow[SchemaTableColumn.AllowDBNull])
									{
										dataRow[SchemaTableColumn.IsUnique] = dataRow2["UNIQUE"];
									}
									if (schema.Rows.Count != 1 || !(bool)dataRow2["PRIMARY_KEY"] || string.IsNullOrEmpty(text5) || string.Compare(text5, "integer", true, CultureInfo.InvariantCulture) == 0)
									{
									}
									break;
								}
							}
						}
					}
					if (string.IsNullOrEmpty(text5))
					{
						TypeAffinity typeAffinity;
						text5 = this._activeStatement._sql.ColumnType(this._activeStatement, i, out typeAffinity);
					}
					if (!string.IsNullOrEmpty(text5))
					{
						dataRow["DataTypeName"] = text5;
					}
				}
				dataTable.Rows.Add(dataRow);
			}
			if (this._keyInfo != null)
			{
				this._keyInfo.AppendSchemaTable(dataTable);
			}
			dataTable.AcceptChanges();
			dataTable.EndLoadData();
			return dataTable;
		}

		// Token: 0x0600011A RID: 282 RVA: 0x0000A4A8 File Offset: 0x000086A8
		public override string GetString(int i)
		{
			if (i >= this.VisibleFieldCount && this._keyInfo != null)
			{
				return this._keyInfo.GetString(i - this.VisibleFieldCount);
			}
			this.VerifyType(i, DbType.String);
			return this._activeStatement._sql.GetText(this._activeStatement, i);
		}

		// Token: 0x0600011B RID: 283 RVA: 0x0000A504 File Offset: 0x00008704
		public override object GetValue(int i)
		{
			if (i >= this.VisibleFieldCount && this._keyInfo != null)
			{
				return this._keyInfo.GetValue(i - this.VisibleFieldCount);
			}
			SQLiteType sqliteType = this.GetSQLiteType(i);
			return this._activeStatement._sql.GetValue(this._activeStatement, i, sqliteType);
		}

		// Token: 0x0600011C RID: 284 RVA: 0x0000A55C File Offset: 0x0000875C
		public override int GetValues(object[] values)
		{
			int num = this.FieldCount;
			if (values.Length < num)
			{
				num = values.Length;
			}
			for (int i = 0; i < num; i++)
			{
				values[i] = this.GetValue(i);
			}
			return num;
		}

		// Token: 0x0600011D RID: 285 RVA: 0x0000A59C File Offset: 0x0000879C
		public override bool IsDBNull(int i)
		{
			if (i >= this.VisibleFieldCount && this._keyInfo != null)
			{
				return this._keyInfo.IsDBNull(i - this.VisibleFieldCount);
			}
			return this._activeStatement._sql.IsNull(this._activeStatement, i);
		}

		// Token: 0x0600011E RID: 286 RVA: 0x0000A5EC File Offset: 0x000087EC
		public override bool NextResult()
		{
			this.CheckClosed();
			SqliteStatement sqliteStatement = null;
			int num;
			for (;;)
			{
				if (this._activeStatement != null && sqliteStatement == null)
				{
					this._activeStatement._sql.Reset(this._activeStatement);
					if ((this._commandBehavior & CommandBehavior.SingleResult) != CommandBehavior.Default)
					{
						break;
					}
				}
				sqliteStatement = this._command.GetStatement(this._activeStatementIndex + 1);
				if (sqliteStatement == null)
				{
					return false;
				}
				if (this._readingState < 1)
				{
					this._readingState = 1;
				}
				this._activeStatementIndex++;
				num = sqliteStatement._sql.ColumnCount(sqliteStatement);
				if ((this._commandBehavior & CommandBehavior.SchemaOnly) != CommandBehavior.Default && num != 0)
				{
					goto IL_0190;
				}
				if (sqliteStatement._sql.Step(sqliteStatement))
				{
					goto Block_10;
				}
				if (num != 0)
				{
					goto IL_0189;
				}
				if (this._rowsAffected == -1)
				{
					this._rowsAffected = 0;
				}
				this._rowsAffected += sqliteStatement._sql.Changes;
				sqliteStatement._sql.Reset(sqliteStatement);
			}
			for (;;)
			{
				sqliteStatement = this._command.GetStatement(this._activeStatementIndex + 1);
				if (sqliteStatement == null)
				{
					break;
				}
				this._activeStatementIndex++;
				sqliteStatement._sql.Step(sqliteStatement);
				if (sqliteStatement._sql.ColumnCount(sqliteStatement) == 0)
				{
					if (this._rowsAffected == -1)
					{
						this._rowsAffected = 0;
					}
					this._rowsAffected += sqliteStatement._sql.Changes;
				}
				sqliteStatement._sql.Reset(sqliteStatement);
			}
			return false;
			Block_10:
			this._readingState = -1;
			goto IL_0190;
			IL_0189:
			this._readingState = 1;
			IL_0190:
			this._activeStatement = sqliteStatement;
			this._fieldCount = num;
			this._fieldTypeArray = null;
			if ((this._commandBehavior & CommandBehavior.KeyInfo) != CommandBehavior.Default)
			{
				this.LoadKeyInfo();
			}
			return true;
		}

		// Token: 0x0600011F RID: 287 RVA: 0x0000A7B8 File Offset: 0x000089B8
		private SQLiteType GetSQLiteType(int i)
		{
			if (this._fieldTypeArray == null)
			{
				this._fieldTypeArray = new SQLiteType[this.VisibleFieldCount];
			}
			if (this._fieldTypeArray[i] == null)
			{
				this._fieldTypeArray[i] = new SQLiteType();
			}
			SQLiteType sqliteType = this._fieldTypeArray[i];
			if (sqliteType.Affinity == TypeAffinity.Uninitialized)
			{
				sqliteType.Type = SqliteConvert.TypeNameToDbType(this._activeStatement._sql.ColumnType(this._activeStatement, i, out sqliteType.Affinity));
			}
			else
			{
				sqliteType.Affinity = this._activeStatement._sql.ColumnAffinity(this._activeStatement, i);
			}
			return sqliteType;
		}

		// Token: 0x06000120 RID: 288 RVA: 0x0000A85C File Offset: 0x00008A5C
		public override bool Read()
		{
			this.CheckClosed();
			if (this._readingState == -1)
			{
				this._readingState = 0;
				return true;
			}
			if (this._readingState == 0)
			{
				if ((this._commandBehavior & CommandBehavior.SingleRow) == CommandBehavior.Default && this._activeStatement._sql.Step(this._activeStatement))
				{
					if (this._keyInfo != null)
					{
						this._keyInfo.Reset();
					}
					return true;
				}
				this._readingState = 1;
			}
			return false;
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000121 RID: 289 RVA: 0x0000A8D7 File Offset: 0x00008AD7
		public override int RecordsAffected
		{
			get
			{
				return (this._rowsAffected >= 0) ? this._rowsAffected : 0;
			}
		}

		// Token: 0x1700001D RID: 29
		public override object this[int i]
		{
			get
			{
				return this.GetValue(i);
			}
		}

		// Token: 0x06000123 RID: 291 RVA: 0x0000A8FC File Offset: 0x00008AFC
		private void LoadKeyInfo()
		{
			if (this._keyInfo != null)
			{
				this._keyInfo.Dispose();
			}
			this._keyInfo = new SqliteKeyReader(this._command.Connection, this, this._activeStatement);
		}

		// Token: 0x04000053 RID: 83
		private SqliteCommand _command;

		// Token: 0x04000054 RID: 84
		private int _activeStatementIndex;

		// Token: 0x04000055 RID: 85
		private SqliteStatement _activeStatement;

		// Token: 0x04000056 RID: 86
		private int _readingState;

		// Token: 0x04000057 RID: 87
		private int _rowsAffected;

		// Token: 0x04000058 RID: 88
		private int _fieldCount;

		// Token: 0x04000059 RID: 89
		private SQLiteType[] _fieldTypeArray;

		// Token: 0x0400005A RID: 90
		private CommandBehavior _commandBehavior;

		// Token: 0x0400005B RID: 91
		internal bool _disposeCommand;

		// Token: 0x0400005C RID: 92
		private SqliteKeyReader _keyInfo;

		// Token: 0x0400005D RID: 93
		internal long _version;
	}
}
