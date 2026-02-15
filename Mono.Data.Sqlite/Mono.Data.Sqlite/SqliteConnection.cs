using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.IO;
using System.Text;
using System.Transactions;

namespace Mono.Data.Sqlite
{
	// Token: 0x02000009 RID: 9
	public sealed class SqliteConnection : DbConnection, ICloneable
	{
		// Token: 0x060000C3 RID: 195 RVA: 0x00003D08 File Offset: 0x00001F08
		public SqliteConnection()
			: this(string.Empty)
		{
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00003D18 File Offset: 0x00001F18
		public SqliteConnection(string connectionString)
		{
			this._sql = null;
			this._connectionState = ConnectionState.Closed;
			this._connectionString = string.Empty;
			this._transactionLevel = 0;
			this._version = 0L;
			if (connectionString != null)
			{
				this.ConnectionString = connectionString;
			}
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00003D68 File Offset: 0x00001F68
		public SqliteConnection(SqliteConnection connection)
			: this(connection.ConnectionString)
		{
			if (connection.State == ConnectionState.Open)
			{
				this.Open();
				using (DataTable schema = connection.GetSchema("Catalogs"))
				{
					foreach (object obj in schema.Rows)
					{
						DataRow dataRow = (DataRow)obj;
						string text = dataRow[0].ToString();
						if (string.Compare(text, "main", true, CultureInfo.InvariantCulture) != 0 && string.Compare(text, "temp", true, CultureInfo.InvariantCulture) != 0)
						{
							using (SqliteCommand sqliteCommand = this.CreateCommand())
							{
								sqliteCommand.CommandText = string.Format(CultureInfo.InvariantCulture, "ATTACH DATABASE '{0}' AS [{1}]", new object[]
								{
									dataRow[1],
									dataRow[0]
								});
								sqliteCommand.ExecuteNonQuery();
							}
						}
					}
				}
			}
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00003EA8 File Offset: 0x000020A8
		public object Clone()
		{
			return new SqliteConnection(this);
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00003EB0 File Offset: 0x000020B0
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			if (disposing)
			{
				this.Close();
			}
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00003EC8 File Offset: 0x000020C8
		internal void OnStateChange(ConnectionState newState)
		{
			ConnectionState connectionState = this._connectionState;
			this._connectionState = newState;
			if (this.StateChange != null && connectionState != newState)
			{
				StateChangeEventArgs stateChangeEventArgs = new StateChangeEventArgs(connectionState, newState);
				this.StateChange(this, stateChangeEventArgs);
			}
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00003F0A File Offset: 0x0000210A
		public SqliteTransaction BeginTransaction()
		{
			return (SqliteTransaction)this.BeginDbTransaction(this._defaultIsolation);
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00003F20 File Offset: 0x00002120
		protected override DbTransaction BeginDbTransaction(global::System.Data.IsolationLevel isolationLevel)
		{
			if (this._connectionState != ConnectionState.Open)
			{
				throw new InvalidOperationException();
			}
			if (isolationLevel == global::System.Data.IsolationLevel.Unspecified)
			{
				isolationLevel = this._defaultIsolation;
			}
			if (isolationLevel != global::System.Data.IsolationLevel.Serializable && isolationLevel != global::System.Data.IsolationLevel.ReadCommitted)
			{
				throw new ArgumentException("isolationLevel");
			}
			return new SqliteTransaction(this, isolationLevel != global::System.Data.IsolationLevel.Serializable);
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00003F80 File Offset: 0x00002180
		public override void Close()
		{
			if (this._sql != null)
			{
				if (this._enlistment != null)
				{
					SqliteConnection sqliteConnection = new SqliteConnection();
					sqliteConnection._sql = this._sql;
					sqliteConnection._transactionLevel = this._transactionLevel;
					sqliteConnection._enlistment = this._enlistment;
					sqliteConnection._connectionState = this._connectionState;
					sqliteConnection._version = this._version;
					sqliteConnection._enlistment._transaction._cnn = sqliteConnection;
					sqliteConnection._enlistment._disposeConnection = true;
					this._sql = null;
					this._enlistment = null;
				}
				if (this._sql != null)
				{
					this._sql.Close();
				}
				this._sql = null;
				this._transactionLevel = 0;
			}
			this.OnStateChange(ConnectionState.Closed);
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000CC RID: 204 RVA: 0x0000403B File Offset: 0x0000223B
		// (set) Token: 0x060000CD RID: 205 RVA: 0x00004044 File Offset: 0x00002244
		[RefreshProperties(RefreshProperties.All)]
		[DefaultValue("")]
		[Editor("SQLite.Designer.SqliteConnectionStringEditor, SQLite.Designer, Version=1.0.36.0, Culture=neutral, PublicKeyToken=db937bc2d44ff139", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public override string ConnectionString
		{
			get
			{
				return this._connectionString;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException();
				}
				if (this._connectionState != ConnectionState.Closed)
				{
					throw new InvalidOperationException();
				}
				this._connectionString = value;
			}
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00004075 File Offset: 0x00002275
		public new SqliteCommand CreateCommand()
		{
			return new SqliteCommand(this);
		}

		// Token: 0x060000CF RID: 207 RVA: 0x0000407D File Offset: 0x0000227D
		protected override DbCommand CreateDbCommand()
		{
			return this.CreateCommand();
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00004088 File Offset: 0x00002288
		internal static void MapMonoKeyword(string[] arPiece, SortedList<string, string> ls)
		{
			string text = arPiece[0].ToLower(CultureInfo.InvariantCulture);
			string text2;
			string text3;
			if (text != null)
			{
				if (SqliteConnection.<>f__switch$map1 == null)
				{
					SqliteConnection.<>f__switch$map1 = new Dictionary<string, int>(1) { { "uri", 0 } };
				}
				int num;
				if (SqliteConnection.<>f__switch$map1.TryGetValue(text, out num))
				{
					if (num == 0)
					{
						text2 = "Data Source";
						text3 = SqliteConnection.MapMonoUriPath(arPiece[1]);
						goto IL_0076;
					}
				}
			}
			text2 = arPiece[0];
			text3 = arPiece[1];
			IL_0076:
			ls.Add(text2, text3);
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00004114 File Offset: 0x00002314
		internal static string MapMonoUriPath(string path)
		{
			if (path.StartsWith("file://"))
			{
				return path.Substring(7);
			}
			if (path.StartsWith("file:"))
			{
				return path.Substring(5);
			}
			if (path.StartsWith("/"))
			{
				return path;
			}
			throw new InvalidOperationException("Invalid connection string: invalid URI");
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00004170 File Offset: 0x00002370
		internal static string MapUriPath(string path)
		{
			if (path.StartsWith("file://"))
			{
				return path.Substring(7);
			}
			if (path.StartsWith("file:"))
			{
				return path.Substring(5);
			}
			if (path.StartsWith("/"))
			{
				return path;
			}
			throw new InvalidOperationException("Invalid connection string: invalid URI");
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x000041CC File Offset: 0x000023CC
		internal static SortedList<string, string> ParseConnectionString(string connectionString)
		{
			string text = connectionString.Replace(',', ';');
			SortedList<string, string> sortedList = new SortedList<string, string>(StringComparer.OrdinalIgnoreCase);
			string[] array = SqliteConvert.Split(text, ';');
			int num = array.Length;
			for (int i = 0; i < num; i++)
			{
				string[] array2 = SqliteConvert.Split(array[i], '=');
				if (array2.Length != 2)
				{
					throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, "Invalid ConnectionString format for parameter \"{0}\"", new object[] { (array2.Length <= 0) ? "null" : array2[0] }));
				}
				SqliteConnection.MapMonoKeyword(array2, sortedList);
			}
			return sortedList;
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x0000426C File Offset: 0x0000246C
		public override void EnlistTransaction(Transaction transaction)
		{
			if (this._transactionLevel > 0 && transaction != null)
			{
				throw new ArgumentException("Unable to enlist in transaction, a local transaction already exists");
			}
			if (this._enlistment != null && transaction != this._enlistment._scope)
			{
				throw new ArgumentException("Already enlisted in a transaction");
			}
			this._enlistment = new SQLiteEnlistment(this, transaction);
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x000042D8 File Offset: 0x000024D8
		internal static string FindKey(SortedList<string, string> items, string key, string defValue)
		{
			string text;
			if (items.TryGetValue(key, out text))
			{
				return text;
			}
			return defValue;
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x000042F8 File Offset: 0x000024F8
		public override void Open()
		{
			if (this._connectionState != ConnectionState.Closed)
			{
				throw new InvalidOperationException();
			}
			this.Close();
			SortedList<string, string> sortedList = SqliteConnection.ParseConnectionString(this._connectionString);
			if (Convert.ToInt32(SqliteConnection.FindKey(sortedList, "Version", "3"), CultureInfo.InvariantCulture) != 3)
			{
				throw new NotSupportedException("Only SQLite Version 3 is supported at this time");
			}
			string text = SqliteConnection.FindKey(sortedList, "Data Source", string.Empty);
			if (string.IsNullOrEmpty(text))
			{
				text = SqliteConnection.FindKey(sortedList, "Uri", string.Empty);
				if (string.IsNullOrEmpty(text))
				{
					throw new ArgumentException("Data Source cannot be empty.  Use :memory: to open an in-memory database");
				}
				text = SqliteConnection.MapUriPath(text);
			}
			if (string.Compare(text, ":MEMORY:", true, CultureInfo.InvariantCulture) == 0)
			{
				text = ":memory:";
			}
			else
			{
				text = this.ExpandFileName(text);
			}
			try
			{
				bool flag = SqliteConvert.ToBoolean(SqliteConnection.FindKey(sortedList, "Pooling", bool.FalseString));
				bool flag2 = SqliteConvert.ToBoolean(SqliteConnection.FindKey(sortedList, "UseUTF16Encoding", bool.FalseString));
				int num = Convert.ToInt32(SqliteConnection.FindKey(sortedList, "Max Pool Size", "100"));
				this._defaultTimeout = Convert.ToInt32(SqliteConnection.FindKey(sortedList, "Default Timeout", "30"), CultureInfo.CurrentCulture);
				this._defaultIsolation = (global::System.Data.IsolationLevel)((int)Enum.Parse(typeof(global::System.Data.IsolationLevel), SqliteConnection.FindKey(sortedList, "Default IsolationLevel", "Serializable"), true));
				if (this._defaultIsolation != global::System.Data.IsolationLevel.Serializable && this._defaultIsolation != global::System.Data.IsolationLevel.ReadCommitted)
				{
					throw new NotSupportedException("Invalid Default IsolationLevel specified");
				}
				SQLiteDateFormats sqliteDateFormats = (SQLiteDateFormats)((int)Enum.Parse(typeof(SQLiteDateFormats), SqliteConnection.FindKey(sortedList, "DateTimeFormat", "ISO8601"), true));
				if (flag2)
				{
					this._sql = new SQLite3_UTF16(sqliteDateFormats);
				}
				else
				{
					this._sql = new SQLite3(sqliteDateFormats);
				}
				SQLiteOpenFlagsEnum sqliteOpenFlagsEnum = SQLiteOpenFlagsEnum.None;
				if (!SqliteConvert.ToBoolean(SqliteConnection.FindKey(sortedList, "FailIfMissing", bool.FalseString)))
				{
					sqliteOpenFlagsEnum |= SQLiteOpenFlagsEnum.Create;
				}
				if (SqliteConvert.ToBoolean(SqliteConnection.FindKey(sortedList, "Read Only", bool.FalseString)))
				{
					sqliteOpenFlagsEnum |= SQLiteOpenFlagsEnum.ReadOnly;
				}
				else
				{
					sqliteOpenFlagsEnum |= SQLiteOpenFlagsEnum.ReadWrite;
				}
				this._sql.Open(text, sqliteOpenFlagsEnum, num, flag);
				this._binaryGuid = SqliteConvert.ToBoolean(SqliteConnection.FindKey(sortedList, "BinaryGUID", bool.TrueString));
				string text2 = SqliteConnection.FindKey(sortedList, "Password", null);
				if (!string.IsNullOrEmpty(text2))
				{
					this._sql.SetPassword(Encoding.UTF8.GetBytes(text2));
				}
				else if (this._password != null)
				{
					this._sql.SetPassword(this._password);
				}
				this._password = null;
				this._dataSource = Path.GetFileNameWithoutExtension(text);
				this.OnStateChange(ConnectionState.Open);
				this._version += 1L;
				using (SqliteCommand sqliteCommand = this.CreateCommand())
				{
					string text3;
					if (text != ":memory:")
					{
						text3 = SqliteConnection.FindKey(sortedList, "Page Size", "1024");
						if (Convert.ToInt32(text3, CultureInfo.InvariantCulture) != 1024)
						{
							sqliteCommand.CommandText = string.Format(CultureInfo.InvariantCulture, "PRAGMA page_size={0}", new object[] { text3 });
							sqliteCommand.ExecuteNonQuery();
						}
					}
					text3 = SqliteConnection.FindKey(sortedList, "Max Page Count", "0");
					if (Convert.ToInt32(text3, CultureInfo.InvariantCulture) != 0)
					{
						sqliteCommand.CommandText = string.Format(CultureInfo.InvariantCulture, "PRAGMA max_page_count={0}", new object[] { text3 });
						sqliteCommand.ExecuteNonQuery();
					}
					text3 = SqliteConnection.FindKey(sortedList, "Legacy Format", bool.FalseString);
					sqliteCommand.CommandText = string.Format(CultureInfo.InvariantCulture, "PRAGMA legacy_file_format={0}", new object[] { (!SqliteConvert.ToBoolean(text3)) ? "OFF" : "ON" });
					sqliteCommand.ExecuteNonQuery();
					text3 = SqliteConnection.FindKey(sortedList, "Synchronous", "Normal");
					if (string.Compare(text3, "Full", StringComparison.OrdinalIgnoreCase) != 0)
					{
						sqliteCommand.CommandText = string.Format(CultureInfo.InvariantCulture, "PRAGMA synchronous={0}", new object[] { text3 });
						sqliteCommand.ExecuteNonQuery();
					}
					text3 = SqliteConnection.FindKey(sortedList, "Cache Size", "2000");
					if (Convert.ToInt32(text3, CultureInfo.InvariantCulture) != 2000)
					{
						sqliteCommand.CommandText = string.Format(CultureInfo.InvariantCulture, "PRAGMA cache_size={0}", new object[] { text3 });
						sqliteCommand.ExecuteNonQuery();
					}
					text3 = SqliteConnection.FindKey(sortedList, "Journal Mode", "Delete");
					if (string.Compare(text3, "Default", StringComparison.OrdinalIgnoreCase) != 0)
					{
						sqliteCommand.CommandText = string.Format(CultureInfo.InvariantCulture, "PRAGMA journal_mode={0}", new object[] { text3 });
						sqliteCommand.ExecuteNonQuery();
					}
				}
				if (this._commitHandler != null)
				{
					this._sql.SetCommitHook(this._commitCallback);
				}
				if (this._updateHandler != null)
				{
					this._sql.SetUpdateHook(this._updateCallback);
				}
				if (this._rollbackHandler != null)
				{
					this._sql.SetRollbackHook(this._rollbackCallback);
				}
				if (Transaction.Current != null && SqliteConvert.ToBoolean(SqliteConnection.FindKey(sortedList, "Enlist", bool.TrueString)))
				{
					this.EnlistTransaction(Transaction.Current);
				}
			}
			catch (SqliteException)
			{
				this.Close();
				throw;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x000048A4 File Offset: 0x00002AA4
		public int DefaultTimeout
		{
			get
			{
				return this._defaultTimeout;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x000048AC File Offset: 0x00002AAC
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public override ConnectionState State
		{
			get
			{
				return this._connectionState;
			}
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x000048B4 File Offset: 0x00002AB4
		private string ExpandFileName(string sourceFile)
		{
			if (string.IsNullOrEmpty(sourceFile))
			{
				return sourceFile;
			}
			if (sourceFile.StartsWith("|DataDirectory|", StringComparison.OrdinalIgnoreCase))
			{
				string text = AppDomain.CurrentDomain.GetData("DataDirectory") as string;
				if (string.IsNullOrEmpty(text))
				{
					text = AppDomain.CurrentDomain.BaseDirectory;
				}
				if (sourceFile.Length > "|DataDirectory|".Length && (sourceFile["|DataDirectory|".Length] == Path.DirectorySeparatorChar || sourceFile["|DataDirectory|".Length] == Path.AltDirectorySeparatorChar))
				{
					sourceFile = sourceFile.Remove("|DataDirectory|".Length, 1);
				}
				sourceFile = Path.Combine(text, sourceFile.Substring("|DataDirectory|".Length));
			}
			sourceFile = Path.GetFullPath(sourceFile);
			return sourceFile;
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00004987 File Offset: 0x00002B87
		public override DataTable GetSchema(string collectionName)
		{
			return this.GetSchema(collectionName, new string[0]);
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00004998 File Offset: 0x00002B98
		public override DataTable GetSchema(string collectionName, string[] restrictionValues)
		{
			if (this._connectionState != ConnectionState.Open)
			{
				throw new InvalidOperationException();
			}
			string[] array = new string[5];
			if (restrictionValues == null)
			{
				restrictionValues = new string[0];
			}
			restrictionValues.CopyTo(array, 0);
			string text = collectionName.ToUpper(CultureInfo.InvariantCulture);
			switch (text)
			{
			case "METADATACOLLECTIONS":
				return SqliteConnection.Schema_MetaDataCollections();
			case "DATASOURCEINFORMATION":
				return this.Schema_DataSourceInformation();
			case "DATATYPES":
				return this.Schema_DataTypes();
			case "COLUMNS":
			case "TABLECOLUMNS":
				return this.Schema_Columns(array[0], array[2], array[3]);
			case "INDEXES":
				return this.Schema_Indexes(array[0], array[2], array[3]);
			case "TRIGGERS":
				return this.Schema_Triggers(array[0], array[2], array[3]);
			case "INDEXCOLUMNS":
				return this.Schema_IndexColumns(array[0], array[2], array[3], array[4]);
			case "TABLES":
				return this.Schema_Tables(array[0], array[2], array[3]);
			case "VIEWS":
				return this.Schema_Views(array[0], array[2]);
			case "VIEWCOLUMNS":
				return this.Schema_ViewColumns(array[0], array[2], array[3]);
			case "FOREIGNKEYS":
				return this.Schema_ForeignKeys(array[0], array[2], array[3]);
			case "CATALOGS":
				return this.Schema_Catalogs(array[0]);
			case "RESERVEDWORDS":
				return SqliteConnection.Schema_ReservedWords();
			}
			throw new NotSupportedException();
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00004BA4 File Offset: 0x00002DA4
		private static DataTable Schema_ReservedWords()
		{
			DataTable dataTable = new DataTable("MetaDataCollections");
			dataTable.Locale = CultureInfo.InvariantCulture;
			dataTable.Columns.Add("ReservedWord", typeof(string));
			dataTable.Columns.Add("MaximumVersion", typeof(string));
			dataTable.Columns.Add("MinimumVersion", typeof(string));
			dataTable.BeginLoadData();
			foreach (string text in SR.Keywords.Split(new char[] { ',' }))
			{
				DataRow dataRow = dataTable.NewRow();
				dataRow[0] = text;
				dataTable.Rows.Add(dataRow);
			}
			dataTable.AcceptChanges();
			dataTable.EndLoadData();
			return dataTable;
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00004C7C File Offset: 0x00002E7C
		private static DataTable Schema_MetaDataCollections()
		{
			DataTable dataTable = new DataTable("MetaDataCollections");
			dataTable.Locale = CultureInfo.InvariantCulture;
			dataTable.Columns.Add("CollectionName", typeof(string));
			dataTable.Columns.Add("NumberOfRestrictions", typeof(int));
			dataTable.Columns.Add("NumberOfIdentifierParts", typeof(int));
			dataTable.BeginLoadData();
			StringReader stringReader = new StringReader(SR.MetaDataCollections);
			dataTable.ReadXml(stringReader);
			stringReader.Close();
			dataTable.AcceptChanges();
			dataTable.EndLoadData();
			return dataTable;
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00004D1C File Offset: 0x00002F1C
		private DataTable Schema_DataSourceInformation()
		{
			DataTable dataTable = new DataTable("DataSourceInformation");
			dataTable.Locale = CultureInfo.InvariantCulture;
			dataTable.Columns.Add(DbMetaDataColumnNames.CompositeIdentifierSeparatorPattern, typeof(string));
			dataTable.Columns.Add(DbMetaDataColumnNames.DataSourceProductName, typeof(string));
			dataTable.Columns.Add(DbMetaDataColumnNames.DataSourceProductVersion, typeof(string));
			dataTable.Columns.Add(DbMetaDataColumnNames.DataSourceProductVersionNormalized, typeof(string));
			dataTable.Columns.Add(DbMetaDataColumnNames.GroupByBehavior, typeof(int));
			dataTable.Columns.Add(DbMetaDataColumnNames.IdentifierPattern, typeof(string));
			dataTable.Columns.Add(DbMetaDataColumnNames.IdentifierCase, typeof(int));
			dataTable.Columns.Add(DbMetaDataColumnNames.OrderByColumnsInSelect, typeof(bool));
			dataTable.Columns.Add(DbMetaDataColumnNames.ParameterMarkerFormat, typeof(string));
			dataTable.Columns.Add(DbMetaDataColumnNames.ParameterMarkerPattern, typeof(string));
			dataTable.Columns.Add(DbMetaDataColumnNames.ParameterNameMaxLength, typeof(int));
			dataTable.Columns.Add(DbMetaDataColumnNames.ParameterNamePattern, typeof(string));
			dataTable.Columns.Add(DbMetaDataColumnNames.QuotedIdentifierPattern, typeof(string));
			dataTable.Columns.Add(DbMetaDataColumnNames.QuotedIdentifierCase, typeof(int));
			dataTable.Columns.Add(DbMetaDataColumnNames.StatementSeparatorPattern, typeof(string));
			dataTable.Columns.Add(DbMetaDataColumnNames.StringLiteralPattern, typeof(string));
			dataTable.Columns.Add(DbMetaDataColumnNames.SupportedJoinOperators, typeof(int));
			dataTable.BeginLoadData();
			DataRow dataRow = dataTable.NewRow();
			dataRow.ItemArray = new object[]
			{
				null,
				"SQLite",
				this._sql.Version,
				this._sql.Version,
				3,
				"(^\\[\\p{Lo}\\p{Lu}\\p{Ll}_@#][\\p{Lo}\\p{Lu}\\p{Ll}\\p{Nd}@$#_]*$)|(^\\[[^\\]\\0]|\\]\\]+\\]$)|(^\\\"[^\\\"\\0]|\\\"\\\"+\\\"$)",
				1,
				false,
				"{0}",
				"@[\\p{Lo}\\p{Lu}\\p{Ll}\\p{Lm}_@#][\\p{Lo}\\p{Lu}\\p{Ll}\\p{Lm}\\p{Nd}\\uff3f_@#\\$]*(?=\\s+|$)",
				255,
				"^[\\p{Lo}\\p{Lu}\\p{Ll}\\p{Lm}_@#][\\p{Lo}\\p{Lu}\\p{Ll}\\p{Lm}\\p{Nd}\\uff3f_@#\\$]*(?=\\s+|$)",
				"(([^\\[]|\\]\\])*)",
				1,
				";",
				"'(([^']|'')*)'",
				15
			};
			dataTable.Rows.Add(dataRow);
			dataTable.AcceptChanges();
			dataTable.EndLoadData();
			return dataTable;
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00004FDC File Offset: 0x000031DC
		private DataTable Schema_Columns(string strCatalog, string strTable, string strColumn)
		{
			DataTable dataTable = new DataTable("Columns");
			dataTable.Locale = CultureInfo.InvariantCulture;
			dataTable.Columns.Add("TABLE_CATALOG", typeof(string));
			dataTable.Columns.Add("TABLE_SCHEMA", typeof(string));
			dataTable.Columns.Add("TABLE_NAME", typeof(string));
			dataTable.Columns.Add("COLUMN_NAME", typeof(string));
			dataTable.Columns.Add("COLUMN_GUID", typeof(Guid));
			dataTable.Columns.Add("COLUMN_PROPID", typeof(long));
			dataTable.Columns.Add("ORDINAL_POSITION", typeof(int));
			dataTable.Columns.Add("COLUMN_HASDEFAULT", typeof(bool));
			dataTable.Columns.Add("COLUMN_DEFAULT", typeof(string));
			dataTable.Columns.Add("COLUMN_FLAGS", typeof(long));
			dataTable.Columns.Add("IS_NULLABLE", typeof(bool));
			dataTable.Columns.Add("DATA_TYPE", typeof(string));
			dataTable.Columns.Add("TYPE_GUID", typeof(Guid));
			dataTable.Columns.Add("CHARACTER_MAXIMUM_LENGTH", typeof(int));
			dataTable.Columns.Add("CHARACTER_OCTET_LENGTH", typeof(int));
			dataTable.Columns.Add("NUMERIC_PRECISION", typeof(int));
			dataTable.Columns.Add("NUMERIC_SCALE", typeof(int));
			dataTable.Columns.Add("DATETIME_PRECISION", typeof(long));
			dataTable.Columns.Add("CHARACTER_SET_CATALOG", typeof(string));
			dataTable.Columns.Add("CHARACTER_SET_SCHEMA", typeof(string));
			dataTable.Columns.Add("CHARACTER_SET_NAME", typeof(string));
			dataTable.Columns.Add("COLLATION_CATALOG", typeof(string));
			dataTable.Columns.Add("COLLATION_SCHEMA", typeof(string));
			dataTable.Columns.Add("COLLATION_NAME", typeof(string));
			dataTable.Columns.Add("DOMAIN_CATALOG", typeof(string));
			dataTable.Columns.Add("DOMAIN_NAME", typeof(string));
			dataTable.Columns.Add("DESCRIPTION", typeof(string));
			dataTable.Columns.Add("PRIMARY_KEY", typeof(bool));
			dataTable.Columns.Add("EDM_TYPE", typeof(string));
			dataTable.Columns.Add("AUTOINCREMENT", typeof(bool));
			dataTable.Columns.Add("UNIQUE", typeof(bool));
			dataTable.BeginLoadData();
			if (string.IsNullOrEmpty(strCatalog))
			{
				strCatalog = "main";
			}
			string text = ((string.Compare(strCatalog, "temp", true, CultureInfo.InvariantCulture) != 0) ? "sqlite_master" : "sqlite_temp_master");
			using (SqliteCommand sqliteCommand = new SqliteCommand(string.Format(CultureInfo.InvariantCulture, "SELECT * FROM [{0}].[{1}] WHERE [type] LIKE 'table' OR [type] LIKE 'view'", new object[] { strCatalog, text }), this))
			{
				using (SqliteDataReader sqliteDataReader = sqliteCommand.ExecuteReader())
				{
					while (sqliteDataReader.Read())
					{
						if (!string.IsNullOrEmpty(strTable))
						{
							if (string.Compare(strTable, sqliteDataReader.GetString(2), true, CultureInfo.InvariantCulture) != 0)
							{
								continue;
							}
						}
						try
						{
							using (SqliteCommand sqliteCommand2 = new SqliteCommand(string.Format(CultureInfo.InvariantCulture, "SELECT * FROM [{0}].[{1}]", new object[]
							{
								strCatalog,
								sqliteDataReader.GetString(2)
							}), this))
							{
								using (SqliteDataReader sqliteDataReader2 = sqliteCommand2.ExecuteReader(CommandBehavior.SchemaOnly))
								{
									using (DataTable schemaTable = sqliteDataReader2.GetSchemaTable(true, true))
									{
										foreach (object obj in schemaTable.Rows)
										{
											DataRow dataRow = (DataRow)obj;
											if (string.Compare(dataRow[SchemaTableColumn.ColumnName].ToString(), strColumn, true, CultureInfo.InvariantCulture) == 0 || strColumn == null)
											{
												DataRow dataRow2 = dataTable.NewRow();
												dataRow2["NUMERIC_PRECISION"] = dataRow[SchemaTableColumn.NumericPrecision];
												dataRow2["NUMERIC_SCALE"] = dataRow[SchemaTableColumn.NumericScale];
												dataRow2["TABLE_NAME"] = sqliteDataReader.GetString(2);
												dataRow2["COLUMN_NAME"] = dataRow[SchemaTableColumn.ColumnName];
												dataRow2["TABLE_CATALOG"] = strCatalog;
												dataRow2["ORDINAL_POSITION"] = dataRow[SchemaTableColumn.ColumnOrdinal];
												dataRow2["COLUMN_HASDEFAULT"] = dataRow[SchemaTableOptionalColumn.DefaultValue] != DBNull.Value;
												dataRow2["COLUMN_DEFAULT"] = dataRow[SchemaTableOptionalColumn.DefaultValue];
												dataRow2["IS_NULLABLE"] = dataRow[SchemaTableColumn.AllowDBNull];
												dataRow2["DATA_TYPE"] = dataRow["DataTypeName"].ToString().ToLower(CultureInfo.InvariantCulture);
												dataRow2["EDM_TYPE"] = SqliteConvert.DbTypeToTypeName((DbType)((int)dataRow[SchemaTableColumn.ProviderType])).ToString().ToLower(CultureInfo.InvariantCulture);
												dataRow2["CHARACTER_MAXIMUM_LENGTH"] = dataRow[SchemaTableColumn.ColumnSize];
												dataRow2["TABLE_SCHEMA"] = dataRow[SchemaTableColumn.BaseSchemaName];
												dataRow2["PRIMARY_KEY"] = dataRow[SchemaTableColumn.IsKey];
												dataRow2["AUTOINCREMENT"] = dataRow[SchemaTableOptionalColumn.IsAutoIncrement];
												dataRow2["COLLATION_NAME"] = dataRow["CollationType"];
												dataRow2["UNIQUE"] = dataRow[SchemaTableColumn.IsUnique];
												dataTable.Rows.Add(dataRow2);
											}
										}
									}
								}
							}
						}
						catch (SqliteException)
						{
						}
					}
				}
			}
			dataTable.AcceptChanges();
			dataTable.EndLoadData();
			return dataTable;
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x0000577C File Offset: 0x0000397C
		private DataTable Schema_Indexes(string strCatalog, string strTable, string strIndex)
		{
			DataTable dataTable = new DataTable("Indexes");
			List<int> list = new List<int>();
			dataTable.Locale = CultureInfo.InvariantCulture;
			dataTable.Columns.Add("TABLE_CATALOG", typeof(string));
			dataTable.Columns.Add("TABLE_SCHEMA", typeof(string));
			dataTable.Columns.Add("TABLE_NAME", typeof(string));
			dataTable.Columns.Add("INDEX_CATALOG", typeof(string));
			dataTable.Columns.Add("INDEX_SCHEMA", typeof(string));
			dataTable.Columns.Add("INDEX_NAME", typeof(string));
			dataTable.Columns.Add("PRIMARY_KEY", typeof(bool));
			dataTable.Columns.Add("UNIQUE", typeof(bool));
			dataTable.Columns.Add("CLUSTERED", typeof(bool));
			dataTable.Columns.Add("TYPE", typeof(int));
			dataTable.Columns.Add("FILL_FACTOR", typeof(int));
			dataTable.Columns.Add("INITIAL_SIZE", typeof(int));
			dataTable.Columns.Add("NULLS", typeof(int));
			dataTable.Columns.Add("SORT_BOOKMARKS", typeof(bool));
			dataTable.Columns.Add("AUTO_UPDATE", typeof(bool));
			dataTable.Columns.Add("NULL_COLLATION", typeof(int));
			dataTable.Columns.Add("ORDINAL_POSITION", typeof(int));
			dataTable.Columns.Add("COLUMN_NAME", typeof(string));
			dataTable.Columns.Add("COLUMN_GUID", typeof(Guid));
			dataTable.Columns.Add("COLUMN_PROPID", typeof(long));
			dataTable.Columns.Add("COLLATION", typeof(short));
			dataTable.Columns.Add("CARDINALITY", typeof(decimal));
			dataTable.Columns.Add("PAGES", typeof(int));
			dataTable.Columns.Add("FILTER_CONDITION", typeof(string));
			dataTable.Columns.Add("INTEGRATED", typeof(bool));
			dataTable.Columns.Add("INDEX_DEFINITION", typeof(string));
			dataTable.BeginLoadData();
			if (string.IsNullOrEmpty(strCatalog))
			{
				strCatalog = "main";
			}
			string text = ((string.Compare(strCatalog, "temp", true, CultureInfo.InvariantCulture) != 0) ? "sqlite_master" : "sqlite_temp_master");
			using (SqliteCommand sqliteCommand = new SqliteCommand(string.Format(CultureInfo.InvariantCulture, "SELECT * FROM [{0}].[{1}] WHERE [type] LIKE 'table'", new object[] { strCatalog, text }), this))
			{
				using (SqliteDataReader sqliteDataReader = sqliteCommand.ExecuteReader())
				{
					while (sqliteDataReader.Read())
					{
						bool flag = false;
						list.Clear();
						if (!string.IsNullOrEmpty(strTable))
						{
							if (string.Compare(sqliteDataReader.GetString(2), strTable, true, CultureInfo.InvariantCulture) != 0)
							{
								continue;
							}
						}
						try
						{
							using (SqliteCommand sqliteCommand2 = new SqliteCommand(string.Format(CultureInfo.InvariantCulture, "PRAGMA [{0}].table_info([{1}])", new object[]
							{
								strCatalog,
								sqliteDataReader.GetString(2)
							}), this))
							{
								using (SqliteDataReader sqliteDataReader2 = sqliteCommand2.ExecuteReader())
								{
									while (sqliteDataReader2.Read())
									{
										if (sqliteDataReader2.GetInt32(5) == 1)
										{
											list.Add(sqliteDataReader2.GetInt32(0));
											if (string.Compare(sqliteDataReader2.GetString(2), "INTEGER", true, CultureInfo.InvariantCulture) == 0)
											{
												flag = true;
											}
										}
									}
								}
							}
						}
						catch (SqliteException)
						{
						}
						if (list.Count == 1 && flag)
						{
							DataRow dataRow = dataTable.NewRow();
							dataRow["TABLE_CATALOG"] = strCatalog;
							dataRow["TABLE_NAME"] = sqliteDataReader.GetString(2);
							dataRow["INDEX_CATALOG"] = strCatalog;
							dataRow["PRIMARY_KEY"] = true;
							dataRow["INDEX_NAME"] = string.Format(CultureInfo.InvariantCulture, "{1}_PK_{0}", new object[]
							{
								sqliteDataReader.GetString(2),
								text
							});
							dataRow["UNIQUE"] = true;
							if (string.Compare((string)dataRow["INDEX_NAME"], strIndex, true, CultureInfo.InvariantCulture) == 0 || strIndex == null)
							{
								dataTable.Rows.Add(dataRow);
							}
							list.Clear();
						}
						try
						{
							using (SqliteCommand sqliteCommand3 = new SqliteCommand(string.Format(CultureInfo.InvariantCulture, "PRAGMA [{0}].index_list([{1}])", new object[]
							{
								strCatalog,
								sqliteDataReader.GetString(2)
							}), this))
							{
								using (SqliteDataReader sqliteDataReader3 = sqliteCommand3.ExecuteReader())
								{
									while (sqliteDataReader3.Read())
									{
										if (string.Compare(sqliteDataReader3.GetString(1), strIndex, true, CultureInfo.InvariantCulture) == 0 || strIndex == null)
										{
											DataRow dataRow = dataTable.NewRow();
											dataRow["TABLE_CATALOG"] = strCatalog;
											dataRow["TABLE_NAME"] = sqliteDataReader.GetString(2);
											dataRow["INDEX_CATALOG"] = strCatalog;
											dataRow["INDEX_NAME"] = sqliteDataReader3.GetString(1);
											dataRow["UNIQUE"] = sqliteDataReader3.GetBoolean(2);
											dataRow["PRIMARY_KEY"] = false;
											using (SqliteCommand sqliteCommand4 = new SqliteCommand(string.Format(CultureInfo.InvariantCulture, "SELECT * FROM [{0}].[{2}] WHERE [type] LIKE 'index' AND [name] LIKE '{1}'", new object[]
											{
												strCatalog,
												sqliteDataReader3.GetString(1).Replace("'", "''"),
												text
											}), this))
											{
												using (SqliteDataReader sqliteDataReader4 = sqliteCommand4.ExecuteReader())
												{
													if (sqliteDataReader4.Read())
													{
														if (!sqliteDataReader4.IsDBNull(4))
														{
															dataRow["INDEX_DEFINITION"] = sqliteDataReader4.GetString(4);
														}
													}
												}
											}
											if (list.Count > 0 && sqliteDataReader3.GetString(1).StartsWith("sqlite_autoindex_" + sqliteDataReader.GetString(2), StringComparison.InvariantCultureIgnoreCase))
											{
												using (SqliteCommand sqliteCommand5 = new SqliteCommand(string.Format(CultureInfo.InvariantCulture, "PRAGMA [{0}].index_info([{1}])", new object[]
												{
													strCatalog,
													sqliteDataReader3.GetString(1)
												}), this))
												{
													using (SqliteDataReader sqliteDataReader5 = sqliteCommand5.ExecuteReader())
													{
														int num = 0;
														while (sqliteDataReader5.Read())
														{
															if (!list.Contains(sqliteDataReader5.GetInt32(1)))
															{
																num = 0;
																break;
															}
															num++;
														}
														if (num == list.Count)
														{
															dataRow["PRIMARY_KEY"] = true;
															list.Clear();
														}
													}
												}
											}
											dataTable.Rows.Add(dataRow);
										}
									}
								}
							}
						}
						catch (SqliteException)
						{
						}
					}
				}
			}
			dataTable.AcceptChanges();
			dataTable.EndLoadData();
			return dataTable;
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x0000608C File Offset: 0x0000428C
		private DataTable Schema_Triggers(string catalog, string table, string triggerName)
		{
			DataTable dataTable = new DataTable("Triggers");
			dataTable.Locale = CultureInfo.InvariantCulture;
			dataTable.Columns.Add("TABLE_CATALOG", typeof(string));
			dataTable.Columns.Add("TABLE_SCHEMA", typeof(string));
			dataTable.Columns.Add("TABLE_NAME", typeof(string));
			dataTable.Columns.Add("TRIGGER_NAME", typeof(string));
			dataTable.Columns.Add("TRIGGER_DEFINITION", typeof(string));
			dataTable.BeginLoadData();
			if (string.IsNullOrEmpty(table))
			{
				table = null;
			}
			if (string.IsNullOrEmpty(catalog))
			{
				catalog = "main";
			}
			string text = ((string.Compare(catalog, "temp", true, CultureInfo.InvariantCulture) != 0) ? "sqlite_master" : "sqlite_temp_master");
			using (SqliteCommand sqliteCommand = new SqliteCommand(string.Format(CultureInfo.InvariantCulture, "SELECT [type], [name], [tbl_name], [rootpage], [sql], [rowid] FROM [{0}].[{1}] WHERE [type] LIKE 'trigger'", new object[] { catalog, text }), this))
			{
				using (SqliteDataReader sqliteDataReader = sqliteCommand.ExecuteReader())
				{
					while (sqliteDataReader.Read())
					{
						if ((string.Compare(sqliteDataReader.GetString(1), triggerName, true, CultureInfo.InvariantCulture) == 0 || triggerName == null) && (table == null || string.Compare(table, sqliteDataReader.GetString(2), true, CultureInfo.InvariantCulture) == 0))
						{
							DataRow dataRow = dataTable.NewRow();
							dataRow["TABLE_CATALOG"] = catalog;
							dataRow["TABLE_NAME"] = sqliteDataReader.GetString(2);
							dataRow["TRIGGER_NAME"] = sqliteDataReader.GetString(1);
							dataRow["TRIGGER_DEFINITION"] = sqliteDataReader.GetString(4);
							dataTable.Rows.Add(dataRow);
						}
					}
				}
			}
			dataTable.AcceptChanges();
			dataTable.EndLoadData();
			return dataTable;
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x000062A4 File Offset: 0x000044A4
		private DataTable Schema_Tables(string strCatalog, string strTable, string strType)
		{
			DataTable dataTable = new DataTable("Tables");
			dataTable.Locale = CultureInfo.InvariantCulture;
			dataTable.Columns.Add("TABLE_CATALOG", typeof(string));
			dataTable.Columns.Add("TABLE_SCHEMA", typeof(string));
			dataTable.Columns.Add("TABLE_NAME", typeof(string));
			dataTable.Columns.Add("TABLE_TYPE", typeof(string));
			dataTable.Columns.Add("TABLE_ID", typeof(long));
			dataTable.Columns.Add("TABLE_ROOTPAGE", typeof(int));
			dataTable.Columns.Add("TABLE_DEFINITION", typeof(string));
			dataTable.BeginLoadData();
			if (string.IsNullOrEmpty(strCatalog))
			{
				strCatalog = "main";
			}
			string text = ((string.Compare(strCatalog, "temp", true, CultureInfo.InvariantCulture) != 0) ? "sqlite_master" : "sqlite_temp_master");
			using (SqliteCommand sqliteCommand = new SqliteCommand(string.Format(CultureInfo.InvariantCulture, "SELECT [type], [name], [tbl_name], [rootpage], [sql], [rowid] FROM [{0}].[{1}] WHERE [type] LIKE 'table'", new object[] { strCatalog, text }), this))
			{
				using (SqliteDataReader sqliteDataReader = sqliteCommand.ExecuteReader())
				{
					while (sqliteDataReader.Read())
					{
						string text2 = sqliteDataReader.GetString(0);
						if (string.Compare(sqliteDataReader.GetString(2), 0, "SQLITE_", 0, 7, true, CultureInfo.InvariantCulture) == 0)
						{
							text2 = "SYSTEM_TABLE";
						}
						if ((string.Compare(strType, text2, true, CultureInfo.InvariantCulture) == 0 || strType == null) && (string.Compare(sqliteDataReader.GetString(2), strTable, true, CultureInfo.InvariantCulture) == 0 || strTable == null))
						{
							DataRow dataRow = dataTable.NewRow();
							dataRow["TABLE_CATALOG"] = strCatalog;
							dataRow["TABLE_NAME"] = sqliteDataReader.GetString(2);
							dataRow["TABLE_TYPE"] = text2;
							dataRow["TABLE_ID"] = sqliteDataReader.GetInt64(5);
							dataRow["TABLE_ROOTPAGE"] = sqliteDataReader.GetInt32(3);
							dataRow["TABLE_DEFINITION"] = sqliteDataReader.GetString(4);
							dataTable.Rows.Add(dataRow);
						}
					}
				}
			}
			dataTable.AcceptChanges();
			dataTable.EndLoadData();
			return dataTable;
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00006554 File Offset: 0x00004754
		private DataTable Schema_Views(string strCatalog, string strView)
		{
			DataTable dataTable = new DataTable("Views");
			dataTable.Locale = CultureInfo.InvariantCulture;
			dataTable.Columns.Add("TABLE_CATALOG", typeof(string));
			dataTable.Columns.Add("TABLE_SCHEMA", typeof(string));
			dataTable.Columns.Add("TABLE_NAME", typeof(string));
			dataTable.Columns.Add("VIEW_DEFINITION", typeof(string));
			dataTable.Columns.Add("CHECK_OPTION", typeof(bool));
			dataTable.Columns.Add("IS_UPDATABLE", typeof(bool));
			dataTable.Columns.Add("DESCRIPTION", typeof(string));
			dataTable.Columns.Add("DATE_CREATED", typeof(DateTime));
			dataTable.Columns.Add("DATE_MODIFIED", typeof(DateTime));
			dataTable.BeginLoadData();
			if (string.IsNullOrEmpty(strCatalog))
			{
				strCatalog = "main";
			}
			string text = ((string.Compare(strCatalog, "temp", true, CultureInfo.InvariantCulture) != 0) ? "sqlite_master" : "sqlite_temp_master");
			using (SqliteCommand sqliteCommand = new SqliteCommand(string.Format(CultureInfo.InvariantCulture, "SELECT * FROM [{0}].[{1}] WHERE [type] LIKE 'view'", new object[] { strCatalog, text }), this))
			{
				using (SqliteDataReader sqliteDataReader = sqliteCommand.ExecuteReader())
				{
					while (sqliteDataReader.Read())
					{
						if (string.Compare(sqliteDataReader.GetString(1), strView, true, CultureInfo.InvariantCulture) == 0 || string.IsNullOrEmpty(strView))
						{
							string text2 = sqliteDataReader.GetString(4).Replace('\r', ' ').Replace('\n', ' ')
								.Replace('\t', ' ');
							int num = CultureInfo.InvariantCulture.CompareInfo.IndexOf(text2, " AS ", CompareOptions.IgnoreCase);
							if (num > -1)
							{
								text2 = text2.Substring(num + 4).Trim();
								DataRow dataRow = dataTable.NewRow();
								dataRow["TABLE_CATALOG"] = strCatalog;
								dataRow["TABLE_NAME"] = sqliteDataReader.GetString(2);
								dataRow["IS_UPDATABLE"] = false;
								dataRow["VIEW_DEFINITION"] = text2;
								dataTable.Rows.Add(dataRow);
							}
						}
					}
				}
			}
			dataTable.AcceptChanges();
			dataTable.EndLoadData();
			return dataTable;
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00006800 File Offset: 0x00004A00
		private DataTable Schema_Catalogs(string strCatalog)
		{
			DataTable dataTable = new DataTable("Catalogs");
			dataTable.Locale = CultureInfo.InvariantCulture;
			dataTable.Columns.Add("CATALOG_NAME", typeof(string));
			dataTable.Columns.Add("DESCRIPTION", typeof(string));
			dataTable.Columns.Add("ID", typeof(long));
			dataTable.BeginLoadData();
			using (SqliteCommand sqliteCommand = new SqliteCommand("PRAGMA database_list", this))
			{
				using (SqliteDataReader sqliteDataReader = sqliteCommand.ExecuteReader())
				{
					while (sqliteDataReader.Read())
					{
						if (string.Compare(sqliteDataReader.GetString(1), strCatalog, true, CultureInfo.InvariantCulture) == 0 || strCatalog == null)
						{
							DataRow dataRow = dataTable.NewRow();
							dataRow["CATALOG_NAME"] = sqliteDataReader.GetString(1);
							dataRow["DESCRIPTION"] = sqliteDataReader.GetString(2);
							dataRow["ID"] = sqliteDataReader.GetInt64(0);
							dataTable.Rows.Add(dataRow);
						}
					}
				}
			}
			dataTable.AcceptChanges();
			dataTable.EndLoadData();
			return dataTable;
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00006958 File Offset: 0x00004B58
		private DataTable Schema_DataTypes()
		{
			DataTable dataTable = new DataTable("DataTypes");
			dataTable.Locale = CultureInfo.InvariantCulture;
			dataTable.Columns.Add("TypeName", typeof(string));
			dataTable.Columns.Add("ProviderDbType", typeof(int));
			dataTable.Columns.Add("ColumnSize", typeof(long));
			dataTable.Columns.Add("CreateFormat", typeof(string));
			dataTable.Columns.Add("CreateParameters", typeof(string));
			dataTable.Columns.Add("DataType", typeof(string));
			dataTable.Columns.Add("IsAutoIncrementable", typeof(bool));
			dataTable.Columns.Add("IsBestMatch", typeof(bool));
			dataTable.Columns.Add("IsCaseSensitive", typeof(bool));
			dataTable.Columns.Add("IsFixedLength", typeof(bool));
			dataTable.Columns.Add("IsFixedPrecisionScale", typeof(bool));
			dataTable.Columns.Add("IsLong", typeof(bool));
			dataTable.Columns.Add("IsNullable", typeof(bool));
			dataTable.Columns.Add("IsSearchable", typeof(bool));
			dataTable.Columns.Add("IsSearchableWithLike", typeof(bool));
			dataTable.Columns.Add("IsLiteralSupported", typeof(bool));
			dataTable.Columns.Add("LiteralPrefix", typeof(string));
			dataTable.Columns.Add("LiteralSuffix", typeof(string));
			dataTable.Columns.Add("IsUnsigned", typeof(bool));
			dataTable.Columns.Add("MaximumScale", typeof(short));
			dataTable.Columns.Add("MinimumScale", typeof(short));
			dataTable.Columns.Add("IsConcurrencyType", typeof(bool));
			dataTable.BeginLoadData();
			StringReader stringReader = new StringReader(SR.DataTypes);
			dataTable.ReadXml(stringReader);
			stringReader.Close();
			dataTable.AcceptChanges();
			dataTable.EndLoadData();
			return dataTable;
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00006BFC File Offset: 0x00004DFC
		private DataTable Schema_IndexColumns(string strCatalog, string strTable, string strIndex, string strColumn)
		{
			DataTable dataTable = new DataTable("IndexColumns");
			List<KeyValuePair<int, string>> list = new List<KeyValuePair<int, string>>();
			dataTable.Locale = CultureInfo.InvariantCulture;
			dataTable.Columns.Add("CONSTRAINT_CATALOG", typeof(string));
			dataTable.Columns.Add("CONSTRAINT_SCHEMA", typeof(string));
			dataTable.Columns.Add("CONSTRAINT_NAME", typeof(string));
			dataTable.Columns.Add("TABLE_CATALOG", typeof(string));
			dataTable.Columns.Add("TABLE_SCHEMA", typeof(string));
			dataTable.Columns.Add("TABLE_NAME", typeof(string));
			dataTable.Columns.Add("COLUMN_NAME", typeof(string));
			dataTable.Columns.Add("ORDINAL_POSITION", typeof(int));
			dataTable.Columns.Add("INDEX_NAME", typeof(string));
			dataTable.Columns.Add("COLLATION_NAME", typeof(string));
			dataTable.Columns.Add("SORT_MODE", typeof(string));
			dataTable.Columns.Add("CONFLICT_OPTION", typeof(int));
			if (string.IsNullOrEmpty(strCatalog))
			{
				strCatalog = "main";
			}
			string text = ((string.Compare(strCatalog, "temp", true, CultureInfo.InvariantCulture) != 0) ? "sqlite_master" : "sqlite_temp_master");
			dataTable.BeginLoadData();
			using (SqliteCommand sqliteCommand = new SqliteCommand(string.Format(CultureInfo.InvariantCulture, "SELECT * FROM [{0}].[{1}] WHERE [type] LIKE 'table'", new object[] { strCatalog, text }), this))
			{
				using (SqliteDataReader sqliteDataReader = sqliteCommand.ExecuteReader())
				{
					while (sqliteDataReader.Read())
					{
						bool flag = false;
						list.Clear();
						if (!string.IsNullOrEmpty(strTable))
						{
							if (string.Compare(sqliteDataReader.GetString(2), strTable, true, CultureInfo.InvariantCulture) != 0)
							{
								continue;
							}
						}
						try
						{
							using (SqliteCommand sqliteCommand2 = new SqliteCommand(string.Format(CultureInfo.InvariantCulture, "PRAGMA [{0}].table_info([{1}])", new object[]
							{
								strCatalog,
								sqliteDataReader.GetString(2)
							}), this))
							{
								using (SqliteDataReader sqliteDataReader2 = sqliteCommand2.ExecuteReader())
								{
									while (sqliteDataReader2.Read())
									{
										if (sqliteDataReader2.GetInt32(5) == 1)
										{
											list.Add(new KeyValuePair<int, string>(sqliteDataReader2.GetInt32(0), sqliteDataReader2.GetString(1)));
											if (string.Compare(sqliteDataReader2.GetString(2), "INTEGER", true, CultureInfo.InvariantCulture) == 0)
											{
												flag = true;
											}
										}
									}
								}
							}
						}
						catch (SqliteException)
						{
						}
						if (list.Count == 1 && flag)
						{
							DataRow dataRow = dataTable.NewRow();
							dataRow["CONSTRAINT_CATALOG"] = strCatalog;
							dataRow["CONSTRAINT_NAME"] = string.Format(CultureInfo.InvariantCulture, "{1}_PK_{0}", new object[]
							{
								sqliteDataReader.GetString(2),
								text
							});
							dataRow["TABLE_CATALOG"] = strCatalog;
							dataRow["TABLE_NAME"] = sqliteDataReader.GetString(2);
							dataRow["COLUMN_NAME"] = list[0].Value;
							dataRow["INDEX_NAME"] = dataRow["CONSTRAINT_NAME"];
							dataRow["ORDINAL_POSITION"] = 0;
							dataRow["COLLATION_NAME"] = "BINARY";
							dataRow["SORT_MODE"] = "ASC";
							dataRow["CONFLICT_OPTION"] = 2;
							if (string.IsNullOrEmpty(strIndex) || string.Compare(strIndex, (string)dataRow["INDEX_NAME"], true, CultureInfo.InvariantCulture) == 0)
							{
								dataTable.Rows.Add(dataRow);
							}
						}
						using (SqliteCommand sqliteCommand3 = new SqliteCommand(string.Format(CultureInfo.InvariantCulture, "SELECT * FROM [{0}].[{2}] WHERE [type] LIKE 'index' AND [tbl_name] LIKE '{1}'", new object[]
						{
							strCatalog,
							sqliteDataReader.GetString(2).Replace("'", "''"),
							text
						}), this))
						{
							using (SqliteDataReader sqliteDataReader3 = sqliteCommand3.ExecuteReader())
							{
								while (sqliteDataReader3.Read())
								{
									int num = 0;
									if (!string.IsNullOrEmpty(strIndex))
									{
										if (string.Compare(strIndex, sqliteDataReader3.GetString(1), true, CultureInfo.InvariantCulture) != 0)
										{
											continue;
										}
									}
									try
									{
										using (SqliteCommand sqliteCommand4 = new SqliteCommand(string.Format(CultureInfo.InvariantCulture, "PRAGMA [{0}].index_info([{1}])", new object[]
										{
											strCatalog,
											sqliteDataReader3.GetString(1)
										}), this))
										{
											using (SqliteDataReader sqliteDataReader4 = sqliteCommand4.ExecuteReader())
											{
												while (sqliteDataReader4.Read())
												{
													DataRow dataRow = dataTable.NewRow();
													dataRow["CONSTRAINT_CATALOG"] = strCatalog;
													dataRow["CONSTRAINT_NAME"] = sqliteDataReader3.GetString(1);
													dataRow["TABLE_CATALOG"] = strCatalog;
													dataRow["TABLE_NAME"] = sqliteDataReader3.GetString(2);
													dataRow["COLUMN_NAME"] = sqliteDataReader4.GetString(2);
													dataRow["INDEX_NAME"] = sqliteDataReader3.GetString(1);
													dataRow["ORDINAL_POSITION"] = num;
													int num2;
													int num3;
													string text2;
													this._sql.GetIndexColumnExtendedInfo(strCatalog, sqliteDataReader3.GetString(1), sqliteDataReader4.GetString(2), out num2, out num3, out text2);
													if (!string.IsNullOrEmpty(text2))
													{
														dataRow["COLLATION_NAME"] = text2;
													}
													dataRow["SORT_MODE"] = ((num2 != 0) ? "DESC" : "ASC");
													dataRow["CONFLICT_OPTION"] = num3;
													num++;
													if (string.IsNullOrEmpty(strColumn) || string.Compare(strColumn, dataRow["COLUMN_NAME"].ToString(), true, CultureInfo.InvariantCulture) == 0)
													{
														dataTable.Rows.Add(dataRow);
													}
												}
											}
										}
									}
									catch (SqliteException)
									{
									}
								}
							}
						}
					}
				}
			}
			dataTable.EndLoadData();
			dataTable.AcceptChanges();
			return dataTable;
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x0000736C File Offset: 0x0000556C
		private DataTable Schema_ViewColumns(string strCatalog, string strView, string strColumn)
		{
			DataTable dataTable = new DataTable("ViewColumns");
			dataTable.Locale = CultureInfo.InvariantCulture;
			dataTable.Columns.Add("VIEW_CATALOG", typeof(string));
			dataTable.Columns.Add("VIEW_SCHEMA", typeof(string));
			dataTable.Columns.Add("VIEW_NAME", typeof(string));
			dataTable.Columns.Add("VIEW_COLUMN_NAME", typeof(string));
			dataTable.Columns.Add("TABLE_CATALOG", typeof(string));
			dataTable.Columns.Add("TABLE_SCHEMA", typeof(string));
			dataTable.Columns.Add("TABLE_NAME", typeof(string));
			dataTable.Columns.Add("COLUMN_NAME", typeof(string));
			dataTable.Columns.Add("ORDINAL_POSITION", typeof(int));
			dataTable.Columns.Add("COLUMN_HASDEFAULT", typeof(bool));
			dataTable.Columns.Add("COLUMN_DEFAULT", typeof(string));
			dataTable.Columns.Add("COLUMN_FLAGS", typeof(long));
			dataTable.Columns.Add("IS_NULLABLE", typeof(bool));
			dataTable.Columns.Add("DATA_TYPE", typeof(string));
			dataTable.Columns.Add("CHARACTER_MAXIMUM_LENGTH", typeof(int));
			dataTable.Columns.Add("NUMERIC_PRECISION", typeof(int));
			dataTable.Columns.Add("NUMERIC_SCALE", typeof(int));
			dataTable.Columns.Add("DATETIME_PRECISION", typeof(long));
			dataTable.Columns.Add("CHARACTER_SET_CATALOG", typeof(string));
			dataTable.Columns.Add("CHARACTER_SET_SCHEMA", typeof(string));
			dataTable.Columns.Add("CHARACTER_SET_NAME", typeof(string));
			dataTable.Columns.Add("COLLATION_CATALOG", typeof(string));
			dataTable.Columns.Add("COLLATION_SCHEMA", typeof(string));
			dataTable.Columns.Add("COLLATION_NAME", typeof(string));
			dataTable.Columns.Add("PRIMARY_KEY", typeof(bool));
			dataTable.Columns.Add("EDM_TYPE", typeof(string));
			dataTable.Columns.Add("AUTOINCREMENT", typeof(bool));
			dataTable.Columns.Add("UNIQUE", typeof(bool));
			if (string.IsNullOrEmpty(strCatalog))
			{
				strCatalog = "main";
			}
			string text = ((string.Compare(strCatalog, "temp", true, CultureInfo.InvariantCulture) != 0) ? "sqlite_master" : "sqlite_temp_master");
			dataTable.BeginLoadData();
			using (SqliteCommand sqliteCommand = new SqliteCommand(string.Format(CultureInfo.InvariantCulture, "SELECT * FROM [{0}].[{1}] WHERE [type] LIKE 'view'", new object[] { strCatalog, text }), this))
			{
				using (SqliteDataReader sqliteDataReader = sqliteCommand.ExecuteReader())
				{
					while (sqliteDataReader.Read())
					{
						if (string.IsNullOrEmpty(strView) || string.Compare(strView, sqliteDataReader.GetString(2), true, CultureInfo.InvariantCulture) == 0)
						{
							using (SqliteCommand sqliteCommand2 = new SqliteCommand(string.Format(CultureInfo.InvariantCulture, "SELECT * FROM [{0}].[{1}]", new object[]
							{
								strCatalog,
								sqliteDataReader.GetString(2)
							}), this))
							{
								string text2 = sqliteDataReader.GetString(4).Replace('\r', ' ').Replace('\n', ' ')
									.Replace('\t', ' ');
								int i = CultureInfo.InvariantCulture.CompareInfo.IndexOf(text2, " AS ", CompareOptions.IgnoreCase);
								if (i >= 0)
								{
									text2 = text2.Substring(i + 4);
									using (SqliteCommand sqliteCommand3 = new SqliteCommand(text2, this))
									{
										using (SqliteDataReader sqliteDataReader2 = sqliteCommand2.ExecuteReader(CommandBehavior.SchemaOnly))
										{
											using (SqliteDataReader sqliteDataReader3 = sqliteCommand3.ExecuteReader(CommandBehavior.SchemaOnly))
											{
												using (DataTable schemaTable = sqliteDataReader2.GetSchemaTable(false, false))
												{
													using (DataTable schemaTable2 = sqliteDataReader3.GetSchemaTable(false, false))
													{
														for (i = 0; i < schemaTable2.Rows.Count; i++)
														{
															DataRow dataRow = schemaTable.Rows[i];
															DataRow dataRow2 = schemaTable2.Rows[i];
															if (string.Compare(dataRow[SchemaTableColumn.ColumnName].ToString(), strColumn, true, CultureInfo.InvariantCulture) == 0 || strColumn == null)
															{
																DataRow dataRow3 = dataTable.NewRow();
																dataRow3["VIEW_CATALOG"] = strCatalog;
																dataRow3["VIEW_NAME"] = sqliteDataReader.GetString(2);
																dataRow3["TABLE_CATALOG"] = strCatalog;
																dataRow3["TABLE_SCHEMA"] = dataRow2[SchemaTableColumn.BaseSchemaName];
																dataRow3["TABLE_NAME"] = dataRow2[SchemaTableColumn.BaseTableName];
																dataRow3["COLUMN_NAME"] = dataRow2[SchemaTableColumn.BaseColumnName];
																dataRow3["VIEW_COLUMN_NAME"] = dataRow[SchemaTableColumn.ColumnName];
																dataRow3["COLUMN_HASDEFAULT"] = dataRow[SchemaTableOptionalColumn.DefaultValue] != DBNull.Value;
																dataRow3["COLUMN_DEFAULT"] = dataRow[SchemaTableOptionalColumn.DefaultValue];
																dataRow3["ORDINAL_POSITION"] = dataRow[SchemaTableColumn.ColumnOrdinal];
																dataRow3["IS_NULLABLE"] = dataRow[SchemaTableColumn.AllowDBNull];
																dataRow3["DATA_TYPE"] = dataRow["DataTypeName"];
																dataRow3["EDM_TYPE"] = SqliteConvert.DbTypeToTypeName((DbType)((int)dataRow[SchemaTableColumn.ProviderType])).ToString().ToLower(CultureInfo.InvariantCulture);
																dataRow3["CHARACTER_MAXIMUM_LENGTH"] = dataRow[SchemaTableColumn.ColumnSize];
																dataRow3["TABLE_SCHEMA"] = dataRow[SchemaTableColumn.BaseSchemaName];
																dataRow3["PRIMARY_KEY"] = dataRow[SchemaTableColumn.IsKey];
																dataRow3["AUTOINCREMENT"] = dataRow[SchemaTableOptionalColumn.IsAutoIncrement];
																dataRow3["COLLATION_NAME"] = dataRow["CollationType"];
																dataRow3["UNIQUE"] = dataRow[SchemaTableColumn.IsUnique];
																dataTable.Rows.Add(dataRow3);
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
			dataTable.EndLoadData();
			dataTable.AcceptChanges();
			return dataTable;
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00007B80 File Offset: 0x00005D80
		private DataTable Schema_ForeignKeys(string strCatalog, string strTable, string strKeyName)
		{
			DataTable dataTable = new DataTable("ForeignKeys");
			dataTable.Locale = CultureInfo.InvariantCulture;
			dataTable.Columns.Add("CONSTRAINT_CATALOG", typeof(string));
			dataTable.Columns.Add("CONSTRAINT_SCHEMA", typeof(string));
			dataTable.Columns.Add("CONSTRAINT_NAME", typeof(string));
			dataTable.Columns.Add("TABLE_CATALOG", typeof(string));
			dataTable.Columns.Add("TABLE_SCHEMA", typeof(string));
			dataTable.Columns.Add("TABLE_NAME", typeof(string));
			dataTable.Columns.Add("CONSTRAINT_TYPE", typeof(string));
			dataTable.Columns.Add("IS_DEFERRABLE", typeof(bool));
			dataTable.Columns.Add("INITIALLY_DEFERRED", typeof(bool));
			dataTable.Columns.Add("FKEY_FROM_COLUMN", typeof(string));
			dataTable.Columns.Add("FKEY_FROM_ORDINAL_POSITION", typeof(int));
			dataTable.Columns.Add("FKEY_TO_CATALOG", typeof(string));
			dataTable.Columns.Add("FKEY_TO_SCHEMA", typeof(string));
			dataTable.Columns.Add("FKEY_TO_TABLE", typeof(string));
			dataTable.Columns.Add("FKEY_TO_COLUMN", typeof(string));
			if (string.IsNullOrEmpty(strCatalog))
			{
				strCatalog = "main";
			}
			string text = ((string.Compare(strCatalog, "temp", true, CultureInfo.InvariantCulture) != 0) ? "sqlite_master" : "sqlite_temp_master");
			dataTable.BeginLoadData();
			using (SqliteCommand sqliteCommand = new SqliteCommand(string.Format(CultureInfo.InvariantCulture, "SELECT * FROM [{0}].[{1}] WHERE [type] LIKE 'table'", new object[] { strCatalog, text }), this))
			{
				using (SqliteDataReader sqliteDataReader = sqliteCommand.ExecuteReader())
				{
					while (sqliteDataReader.Read())
					{
						if (!string.IsNullOrEmpty(strTable))
						{
							if (string.Compare(strTable, sqliteDataReader.GetString(2), true, CultureInfo.InvariantCulture) != 0)
							{
								continue;
							}
						}
						try
						{
							using (SqliteCommandBuilder sqliteCommandBuilder = new SqliteCommandBuilder())
							{
								using (SqliteCommand sqliteCommand2 = new SqliteCommand(string.Format(CultureInfo.InvariantCulture, "PRAGMA [{0}].foreign_key_list([{1}])", new object[]
								{
									strCatalog,
									sqliteDataReader.GetString(2)
								}), this))
								{
									using (SqliteDataReader sqliteDataReader2 = sqliteCommand2.ExecuteReader())
									{
										while (sqliteDataReader2.Read())
										{
											DataRow dataRow = dataTable.NewRow();
											dataRow["CONSTRAINT_CATALOG"] = strCatalog;
											dataRow["CONSTRAINT_NAME"] = string.Format(CultureInfo.InvariantCulture, "FK_{0}_{1}", new object[]
											{
												sqliteDataReader[2],
												sqliteDataReader2.GetInt32(0)
											});
											dataRow["TABLE_CATALOG"] = strCatalog;
											dataRow["TABLE_NAME"] = sqliteCommandBuilder.UnquoteIdentifier(sqliteDataReader.GetString(2));
											dataRow["CONSTRAINT_TYPE"] = "FOREIGN KEY";
											dataRow["IS_DEFERRABLE"] = false;
											dataRow["INITIALLY_DEFERRED"] = false;
											dataRow["FKEY_FROM_COLUMN"] = sqliteCommandBuilder.UnquoteIdentifier(sqliteDataReader2[3].ToString());
											dataRow["FKEY_TO_CATALOG"] = strCatalog;
											dataRow["FKEY_TO_TABLE"] = sqliteCommandBuilder.UnquoteIdentifier(sqliteDataReader2[2].ToString());
											dataRow["FKEY_TO_COLUMN"] = sqliteCommandBuilder.UnquoteIdentifier(sqliteDataReader2[4].ToString());
											dataRow["FKEY_FROM_ORDINAL_POSITION"] = sqliteDataReader2[1];
											if (string.IsNullOrEmpty(strKeyName) || string.Compare(strKeyName, dataRow["CONSTRAINT_NAME"].ToString(), true, CultureInfo.InvariantCulture) == 0)
											{
												dataTable.Rows.Add(dataRow);
											}
										}
									}
								}
							}
						}
						catch (SqliteException)
						{
						}
					}
				}
			}
			dataTable.EndLoadData();
			dataTable.AcceptChanges();
			return dataTable;
		}

		// Token: 0x0400001A RID: 26
		private ConnectionState _connectionState;

		// Token: 0x0400001B RID: 27
		private string _connectionString;

		// Token: 0x0400001C RID: 28
		internal int _transactionLevel;

		// Token: 0x0400001D RID: 29
		private global::System.Data.IsolationLevel _defaultIsolation;

		// Token: 0x0400001E RID: 30
		internal SQLiteEnlistment _enlistment;

		// Token: 0x0400001F RID: 31
		internal SQLiteBase _sql;

		// Token: 0x04000020 RID: 32
		private string _dataSource;

		// Token: 0x04000021 RID: 33
		private byte[] _password;

		// Token: 0x04000022 RID: 34
		private int _defaultTimeout = 30;

		// Token: 0x04000023 RID: 35
		internal bool _binaryGuid;

		// Token: 0x04000024 RID: 36
		internal long _version;

		// Token: 0x04000025 RID: 37
		private SQLiteUpdateCallback _updateCallback;

		// Token: 0x04000026 RID: 38
		private SQLiteCommitCallback _commitCallback;

		// Token: 0x04000027 RID: 39
		private SQLiteRollbackCallback _rollbackCallback;

		// Token: 0x04000028 RID: 40
		private SQLiteUpdateEventHandler _updateHandler;

		// Token: 0x04000029 RID: 41
		private SQLiteCommitHandler _commitHandler;

		// Token: 0x0400002A RID: 42
		private EventHandler _rollbackHandler;

		// Token: 0x0400002B RID: 43
		private StateChangeEventHandler StateChange;
	}
}
