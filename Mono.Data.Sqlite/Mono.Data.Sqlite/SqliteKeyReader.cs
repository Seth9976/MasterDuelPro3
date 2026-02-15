using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace Mono.Data.Sqlite
{
	// Token: 0x0200001E RID: 30
	internal sealed class SqliteKeyReader : IDisposable
	{
		// Token: 0x06000143 RID: 323 RVA: 0x0000B3B8 File Offset: 0x000095B8
		internal SqliteKeyReader(SqliteConnection cnn, SqliteDataReader reader, SqliteStatement stmt)
		{
			Dictionary<string, int> dictionary = new Dictionary<string, int>();
			Dictionary<string, List<string>> dictionary2 = new Dictionary<string, List<string>>();
			List<SqliteKeyReader.KeyInfo> list = new List<SqliteKeyReader.KeyInfo>();
			this._stmt = stmt;
			using (DataTable schema = cnn.GetSchema("Catalogs"))
			{
				foreach (object obj in schema.Rows)
				{
					DataRow dataRow = (DataRow)obj;
					dictionary.Add((string)dataRow["CATALOG_NAME"], Convert.ToInt32(dataRow["ID"]));
				}
			}
			using (DataTable schemaTable = reader.GetSchemaTable(false, false))
			{
				foreach (object obj2 in schemaTable.Rows)
				{
					DataRow dataRow2 = (DataRow)obj2;
					if (dataRow2[SchemaTableOptionalColumn.BaseCatalogName] != DBNull.Value)
					{
						string text = (string)dataRow2[SchemaTableOptionalColumn.BaseCatalogName];
						string text2 = (string)dataRow2[SchemaTableColumn.BaseTableName];
						List<string> list2;
						if (!dictionary2.ContainsKey(text))
						{
							list2 = new List<string>();
							dictionary2.Add(text, list2);
						}
						else
						{
							list2 = dictionary2[text];
						}
						if (!list2.Contains(text2))
						{
							list2.Add(text2);
						}
					}
				}
				foreach (KeyValuePair<string, List<string>> keyValuePair in dictionary2)
				{
					for (int i = 0; i < keyValuePair.Value.Count; i++)
					{
						string text3 = keyValuePair.Value[i];
						DataRow dataRow3 = null;
						using (DataTable schema2 = cnn.GetSchema("Indexes", new string[] { keyValuePair.Key, null, text3 }))
						{
							int num = 0;
							while (num < 2 && dataRow3 == null)
							{
								foreach (object obj3 in schema2.Rows)
								{
									DataRow dataRow4 = (DataRow)obj3;
									if (num == 0 && (bool)dataRow4["PRIMARY_KEY"])
									{
										dataRow3 = dataRow4;
										break;
									}
									if (num == 1 && (bool)dataRow4["UNIQUE"])
									{
										dataRow3 = dataRow4;
										break;
									}
								}
								num++;
							}
							if (dataRow3 == null)
							{
								keyValuePair.Value.RemoveAt(i);
								i--;
							}
							else
							{
								using (DataTable schema3 = cnn.GetSchema("Tables", new string[] { keyValuePair.Key, null, text3 }))
								{
									int num2 = dictionary[keyValuePair.Key];
									int num3 = Convert.ToInt32(schema3.Rows[0]["TABLE_ROOTPAGE"]);
									int cursorForTable = stmt._sql.GetCursorForTable(stmt, num2, num3);
									using (DataTable schema4 = cnn.GetSchema("IndexColumns", new string[]
									{
										keyValuePair.Key,
										null,
										text3,
										(string)dataRow3["INDEX_NAME"]
									}))
									{
										SqliteKeyReader.KeyQuery keyQuery = null;
										List<string> list3 = new List<string>();
										for (int j = 0; j < schema4.Rows.Count; j++)
										{
											bool flag = true;
											foreach (object obj4 in schemaTable.Rows)
											{
												DataRow dataRow5 = (DataRow)obj4;
												if (!dataRow5.IsNull(SchemaTableColumn.BaseColumnName))
												{
													if ((string)dataRow5[SchemaTableColumn.BaseColumnName] == (string)schema4.Rows[j]["COLUMN_NAME"] && (string)dataRow5[SchemaTableColumn.BaseTableName] == text3 && (string)dataRow5[SchemaTableOptionalColumn.BaseCatalogName] == keyValuePair.Key)
													{
														schema4.Rows.RemoveAt(j);
														j--;
														flag = false;
														break;
													}
												}
											}
											if (flag)
											{
												list3.Add((string)schema4.Rows[j]["COLUMN_NAME"]);
											}
										}
										if ((string)dataRow3["INDEX_NAME"] != "sqlite_master_PK_" + text3 && list3.Count > 0)
										{
											string[] array = new string[list3.Count];
											list3.CopyTo(array);
											keyQuery = new SqliteKeyReader.KeyQuery(cnn, keyValuePair.Key, text3, array);
										}
										for (int k = 0; k < schema4.Rows.Count; k++)
										{
											string text4 = (string)schema4.Rows[k]["COLUMN_NAME"];
											list.Add(new SqliteKeyReader.KeyInfo
											{
												rootPage = num3,
												cursor = cursorForTable,
												database = num2,
												databaseName = keyValuePair.Key,
												tableName = text3,
												columnName = text4,
												query = keyQuery,
												column = k
											});
										}
									}
								}
							}
						}
					}
				}
			}
			this._keyInfo = new SqliteKeyReader.KeyInfo[list.Count];
			list.CopyTo(this._keyInfo);
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000144 RID: 324 RVA: 0x0000BAE0 File Offset: 0x00009CE0
		internal int Count
		{
			get
			{
				return (this._keyInfo != null) ? this._keyInfo.Length : 0;
			}
		}

		// Token: 0x06000145 RID: 325 RVA: 0x0000BAFB File Offset: 0x00009CFB
		internal void Sync(int i)
		{
			this.Sync();
			if (this._keyInfo[i].cursor == -1)
			{
				throw new InvalidCastException();
			}
		}

		// Token: 0x06000146 RID: 326 RVA: 0x0000BB20 File Offset: 0x00009D20
		internal void Sync()
		{
			if (this._isValid)
			{
				return;
			}
			SqliteKeyReader.KeyQuery keyQuery = null;
			for (int i = 0; i < this._keyInfo.Length; i++)
			{
				if (this._keyInfo[i].query == null || this._keyInfo[i].query != keyQuery)
				{
					keyQuery = this._keyInfo[i].query;
					if (keyQuery != null)
					{
						keyQuery.Sync(this._stmt._sql.GetRowIdForCursor(this._stmt, this._keyInfo[i].cursor));
					}
				}
			}
			this._isValid = true;
		}

		// Token: 0x06000147 RID: 327 RVA: 0x0000BBD0 File Offset: 0x00009DD0
		internal void Reset()
		{
			this._isValid = false;
			if (this._keyInfo == null)
			{
				return;
			}
			for (int i = 0; i < this._keyInfo.Length; i++)
			{
				if (this._keyInfo[i].query != null)
				{
					this._keyInfo[i].query.IsValid = false;
				}
			}
		}

		// Token: 0x06000148 RID: 328 RVA: 0x0000BC38 File Offset: 0x00009E38
		public void Dispose()
		{
			this._stmt = null;
			if (this._keyInfo == null)
			{
				return;
			}
			for (int i = 0; i < this._keyInfo.Length; i++)
			{
				if (this._keyInfo[i].query != null)
				{
					this._keyInfo[i].query.Dispose();
				}
			}
			this._keyInfo = null;
		}

		// Token: 0x06000149 RID: 329 RVA: 0x0000BCA4 File Offset: 0x00009EA4
		internal string GetDataTypeName(int i)
		{
			this.Sync();
			if (this._keyInfo[i].query != null)
			{
				return this._keyInfo[i].query._reader.GetDataTypeName(this._keyInfo[i].column);
			}
			return "integer";
		}

		// Token: 0x0600014A RID: 330 RVA: 0x0000BD00 File Offset: 0x00009F00
		internal Type GetFieldType(int i)
		{
			this.Sync();
			if (this._keyInfo[i].query != null)
			{
				return this._keyInfo[i].query._reader.GetFieldType(this._keyInfo[i].column);
			}
			return typeof(long);
		}

		// Token: 0x0600014B RID: 331 RVA: 0x0000BD60 File Offset: 0x00009F60
		internal string GetName(int i)
		{
			return this._keyInfo[i].columnName;
		}

		// Token: 0x0600014C RID: 332 RVA: 0x0000BD74 File Offset: 0x00009F74
		internal bool GetBoolean(int i)
		{
			this.Sync(i);
			if (this._keyInfo[i].query != null)
			{
				return this._keyInfo[i].query._reader.GetBoolean(this._keyInfo[i].column);
			}
			throw new InvalidCastException();
		}

		// Token: 0x0600014D RID: 333 RVA: 0x0000BDD0 File Offset: 0x00009FD0
		internal int GetInt32(int i)
		{
			this.Sync(i);
			if (this._keyInfo[i].query != null)
			{
				return this._keyInfo[i].query._reader.GetInt32(this._keyInfo[i].column);
			}
			long rowIdForCursor = this._stmt._sql.GetRowIdForCursor(this._stmt, this._keyInfo[i].cursor);
			if (rowIdForCursor == 0L)
			{
				throw new InvalidCastException();
			}
			return Convert.ToInt32(rowIdForCursor);
		}

		// Token: 0x0600014E RID: 334 RVA: 0x0000BE64 File Offset: 0x0000A064
		internal long GetInt64(int i)
		{
			this.Sync(i);
			if (this._keyInfo[i].query != null)
			{
				return this._keyInfo[i].query._reader.GetInt64(this._keyInfo[i].column);
			}
			long rowIdForCursor = this._stmt._sql.GetRowIdForCursor(this._stmt, this._keyInfo[i].cursor);
			if (rowIdForCursor == 0L)
			{
				throw new InvalidCastException();
			}
			return Convert.ToInt64(rowIdForCursor);
		}

		// Token: 0x0600014F RID: 335 RVA: 0x0000BEF8 File Offset: 0x0000A0F8
		internal string GetString(int i)
		{
			this.Sync(i);
			if (this._keyInfo[i].query != null)
			{
				return this._keyInfo[i].query._reader.GetString(this._keyInfo[i].column);
			}
			throw new InvalidCastException();
		}

		// Token: 0x06000150 RID: 336 RVA: 0x0000BF54 File Offset: 0x0000A154
		internal object GetValue(int i)
		{
			if (this._keyInfo[i].cursor == -1)
			{
				return DBNull.Value;
			}
			this.Sync(i);
			if (this._keyInfo[i].query != null)
			{
				return this._keyInfo[i].query._reader.GetValue(this._keyInfo[i].column);
			}
			if (this.IsDBNull(i))
			{
				return DBNull.Value;
			}
			return this.GetInt64(i);
		}

		// Token: 0x06000151 RID: 337 RVA: 0x0000BFE8 File Offset: 0x0000A1E8
		internal bool IsDBNull(int i)
		{
			if (this._keyInfo[i].cursor == -1)
			{
				return true;
			}
			this.Sync(i);
			if (this._keyInfo[i].query != null)
			{
				return this._keyInfo[i].query._reader.IsDBNull(this._keyInfo[i].column);
			}
			return this._stmt._sql.GetRowIdForCursor(this._stmt, this._keyInfo[i].cursor) == 0L;
		}

		// Token: 0x06000152 RID: 338 RVA: 0x0000C084 File Offset: 0x0000A284
		internal void AppendSchemaTable(DataTable tbl)
		{
			SqliteKeyReader.KeyQuery keyQuery = null;
			for (int i = 0; i < this._keyInfo.Length; i++)
			{
				if (this._keyInfo[i].query == null || this._keyInfo[i].query != keyQuery)
				{
					keyQuery = this._keyInfo[i].query;
					if (keyQuery == null)
					{
						DataRow dataRow = tbl.NewRow();
						dataRow[SchemaTableColumn.ColumnName] = this._keyInfo[i].columnName;
						dataRow[SchemaTableColumn.ColumnOrdinal] = tbl.Rows.Count;
						dataRow[SchemaTableColumn.ColumnSize] = 8;
						dataRow[SchemaTableColumn.NumericPrecision] = 255;
						dataRow[SchemaTableColumn.NumericScale] = 255;
						dataRow[SchemaTableColumn.ProviderType] = DbType.Int64;
						dataRow[SchemaTableColumn.IsLong] = false;
						dataRow[SchemaTableColumn.AllowDBNull] = false;
						dataRow[SchemaTableOptionalColumn.IsReadOnly] = false;
						dataRow[SchemaTableOptionalColumn.IsRowVersion] = false;
						dataRow[SchemaTableColumn.IsUnique] = false;
						dataRow[SchemaTableColumn.IsKey] = true;
						dataRow[SchemaTableColumn.DataType] = typeof(long);
						dataRow[SchemaTableOptionalColumn.IsHidden] = true;
						dataRow[SchemaTableColumn.BaseColumnName] = this._keyInfo[i].columnName;
						dataRow[SchemaTableColumn.IsExpression] = false;
						dataRow[SchemaTableColumn.IsAliased] = false;
						dataRow[SchemaTableColumn.BaseTableName] = this._keyInfo[i].tableName;
						dataRow[SchemaTableOptionalColumn.BaseCatalogName] = this._keyInfo[i].databaseName;
						dataRow[SchemaTableOptionalColumn.IsAutoIncrement] = true;
						dataRow["DataTypeName"] = "integer";
						tbl.Rows.Add(dataRow);
					}
					else
					{
						keyQuery.Sync(0L);
						using (DataTable schemaTable = keyQuery._reader.GetSchemaTable())
						{
							foreach (object obj in schemaTable.Rows)
							{
								DataRow dataRow2 = (DataRow)obj;
								object[] itemArray = dataRow2.ItemArray;
								DataRow dataRow3 = tbl.Rows.Add(itemArray);
								dataRow3[SchemaTableOptionalColumn.IsHidden] = true;
								dataRow3[SchemaTableColumn.ColumnOrdinal] = tbl.Rows.Count - 1;
							}
						}
					}
				}
			}
		}

		// Token: 0x04000097 RID: 151
		private SqliteKeyReader.KeyInfo[] _keyInfo;

		// Token: 0x04000098 RID: 152
		private SqliteStatement _stmt;

		// Token: 0x04000099 RID: 153
		private bool _isValid;

		// Token: 0x0200001F RID: 31
		private struct KeyInfo
		{
			// Token: 0x0400009A RID: 154
			internal string databaseName;

			// Token: 0x0400009B RID: 155
			internal string tableName;

			// Token: 0x0400009C RID: 156
			internal string columnName;

			// Token: 0x0400009D RID: 157
			internal int database;

			// Token: 0x0400009E RID: 158
			internal int rootPage;

			// Token: 0x0400009F RID: 159
			internal int cursor;

			// Token: 0x040000A0 RID: 160
			internal SqliteKeyReader.KeyQuery query;

			// Token: 0x040000A1 RID: 161
			internal int column;
		}

		// Token: 0x02000020 RID: 32
		private sealed class KeyQuery : IDisposable
		{
			// Token: 0x06000153 RID: 339 RVA: 0x0000C384 File Offset: 0x0000A584
			internal KeyQuery(SqliteConnection cnn, string database, string table, params string[] columns)
			{
				using (SqliteCommandBuilder sqliteCommandBuilder = new SqliteCommandBuilder())
				{
					this._command = cnn.CreateCommand();
					for (int i = 0; i < columns.Length; i++)
					{
						columns[i] = sqliteCommandBuilder.QuoteIdentifier(columns[i]);
					}
				}
				this._command.CommandText = string.Format("SELECT {0} FROM [{1}].[{2}] WHERE ROWID = ?", string.Join(",", columns), database, table);
				this._command.Parameters.AddWithValue(null, 0L);
			}

			// Token: 0x17000022 RID: 34
			// (set) Token: 0x06000154 RID: 340 RVA: 0x0000C42C File Offset: 0x0000A62C
			internal bool IsValid
			{
				set
				{
					if (value)
					{
						throw new ArgumentException();
					}
					if (this._reader != null)
					{
						this._reader.Dispose();
						this._reader = null;
					}
				}
			}

			// Token: 0x06000155 RID: 341 RVA: 0x0000C458 File Offset: 0x0000A658
			internal void Sync(long rowid)
			{
				this.IsValid = false;
				this._command.Parameters[0].Value = rowid;
				this._reader = this._command.ExecuteReader();
				this._reader.Read();
			}

			// Token: 0x06000156 RID: 342 RVA: 0x0000C4A8 File Offset: 0x0000A6A8
			public void Dispose()
			{
				this.IsValid = false;
				if (this._command != null)
				{
					this._command.Dispose();
				}
				this._command = null;
			}

			// Token: 0x040000A2 RID: 162
			private SqliteCommand _command;

			// Token: 0x040000A3 RID: 163
			internal SqliteDataReader _reader;
		}
	}
}
