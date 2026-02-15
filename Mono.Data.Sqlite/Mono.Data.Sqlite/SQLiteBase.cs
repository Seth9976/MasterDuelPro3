using System;

namespace Mono.Data.Sqlite
{
	// Token: 0x02000004 RID: 4
	internal abstract class SQLiteBase : SqliteConvert, IDisposable
	{
		// Token: 0x0600004A RID: 74 RVA: 0x00003089 File Offset: 0x00001289
		internal SQLiteBase(SQLiteDateFormats fmt)
			: base(fmt)
		{
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600004C RID: 76
		internal abstract string Version { get; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600004D RID: 77
		internal abstract int Changes { get; }

		// Token: 0x0600004E RID: 78
		internal abstract void Open(string strFilename, SQLiteOpenFlagsEnum flags, int maxPoolSize, bool usePool);

		// Token: 0x0600004F RID: 79
		internal abstract void Close();

		// Token: 0x06000050 RID: 80
		internal abstract void SetTimeout(int nTimeoutMS);

		// Token: 0x06000051 RID: 81
		internal abstract string SQLiteLastError();

		// Token: 0x06000052 RID: 82
		internal abstract SqliteStatement Prepare(SqliteConnection cnn, string strSql, SqliteStatement previous, uint timeoutMS, out string strRemain);

		// Token: 0x06000053 RID: 83
		internal abstract bool Step(SqliteStatement stmt);

		// Token: 0x06000054 RID: 84
		internal abstract int Reset(SqliteStatement stmt);

		// Token: 0x06000055 RID: 85
		internal abstract void Bind_Double(SqliteStatement stmt, int index, double value);

		// Token: 0x06000056 RID: 86
		internal abstract void Bind_Int32(SqliteStatement stmt, int index, int value);

		// Token: 0x06000057 RID: 87
		internal abstract void Bind_Int64(SqliteStatement stmt, int index, long value);

		// Token: 0x06000058 RID: 88
		internal abstract void Bind_Text(SqliteStatement stmt, int index, string value);

		// Token: 0x06000059 RID: 89
		internal abstract void Bind_Blob(SqliteStatement stmt, int index, byte[] blobData);

		// Token: 0x0600005A RID: 90
		internal abstract void Bind_DateTime(SqliteStatement stmt, int index, DateTime dt);

		// Token: 0x0600005B RID: 91
		internal abstract void Bind_Null(SqliteStatement stmt, int index);

		// Token: 0x0600005C RID: 92
		internal abstract int Bind_ParamCount(SqliteStatement stmt);

		// Token: 0x0600005D RID: 93
		internal abstract string Bind_ParamName(SqliteStatement stmt, int index);

		// Token: 0x0600005E RID: 94
		internal abstract int ColumnCount(SqliteStatement stmt);

		// Token: 0x0600005F RID: 95
		internal abstract string ColumnName(SqliteStatement stmt, int index);

		// Token: 0x06000060 RID: 96
		internal abstract TypeAffinity ColumnAffinity(SqliteStatement stmt, int index);

		// Token: 0x06000061 RID: 97
		internal abstract string ColumnType(SqliteStatement stmt, int index, out TypeAffinity nAffinity);

		// Token: 0x06000062 RID: 98
		internal abstract string ColumnOriginalName(SqliteStatement stmt, int index);

		// Token: 0x06000063 RID: 99
		internal abstract string ColumnDatabaseName(SqliteStatement stmt, int index);

		// Token: 0x06000064 RID: 100
		internal abstract string ColumnTableName(SqliteStatement stmt, int index);

		// Token: 0x06000065 RID: 101
		internal abstract void ColumnMetaData(string dataBase, string table, string column, out string dataType, out string collateSequence, out bool notNull, out bool primaryKey, out bool autoIncrement);

		// Token: 0x06000066 RID: 102
		internal abstract void GetIndexColumnExtendedInfo(string database, string index, string column, out int sortMode, out int onError, out string collationSequence);

		// Token: 0x06000067 RID: 103
		internal abstract double GetDouble(SqliteStatement stmt, int index);

		// Token: 0x06000068 RID: 104
		internal abstract int GetInt32(SqliteStatement stmt, int index);

		// Token: 0x06000069 RID: 105
		internal abstract long GetInt64(SqliteStatement stmt, int index);

		// Token: 0x0600006A RID: 106
		internal abstract string GetText(SqliteStatement stmt, int index);

		// Token: 0x0600006B RID: 107
		internal abstract long GetBytes(SqliteStatement stmt, int index, int nDataoffset, byte[] bDest, int nStart, int nLength);

		// Token: 0x0600006C RID: 108
		internal abstract DateTime GetDateTime(SqliteStatement stmt, int index);

		// Token: 0x0600006D RID: 109
		internal abstract bool IsNull(SqliteStatement stmt, int index);

		// Token: 0x0600006E RID: 110
		internal abstract void CreateCollation(string strCollation, SQLiteCollation func, SQLiteCollation func16);

		// Token: 0x0600006F RID: 111
		internal abstract void CreateFunction(string strFunction, int nArgs, bool needCollSeq, SQLiteCallback func, SQLiteCallback funcstep, SQLiteFinalCallback funcfinal);

		// Token: 0x06000070 RID: 112
		internal abstract IntPtr AggregateContext(IntPtr context);

		// Token: 0x06000071 RID: 113
		internal abstract long GetParamValueBytes(IntPtr ptr, int nDataOffset, byte[] bDest, int nStart, int nLength);

		// Token: 0x06000072 RID: 114
		internal abstract double GetParamValueDouble(IntPtr ptr);

		// Token: 0x06000073 RID: 115
		internal abstract long GetParamValueInt64(IntPtr ptr);

		// Token: 0x06000074 RID: 116
		internal abstract string GetParamValueText(IntPtr ptr);

		// Token: 0x06000075 RID: 117
		internal abstract TypeAffinity GetParamValueType(IntPtr ptr);

		// Token: 0x06000076 RID: 118
		internal abstract void ReturnBlob(IntPtr context, byte[] value);

		// Token: 0x06000077 RID: 119
		internal abstract void ReturnDouble(IntPtr context, double value);

		// Token: 0x06000078 RID: 120
		internal abstract void ReturnError(IntPtr context, string value);

		// Token: 0x06000079 RID: 121
		internal abstract void ReturnInt64(IntPtr context, long value);

		// Token: 0x0600007A RID: 122
		internal abstract void ReturnNull(IntPtr context);

		// Token: 0x0600007B RID: 123
		internal abstract void ReturnText(IntPtr context, string value);

		// Token: 0x0600007C RID: 124
		internal abstract void SetPassword(byte[] passwordBytes);

		// Token: 0x0600007D RID: 125
		internal abstract void SetUpdateHook(SQLiteUpdateCallback func);

		// Token: 0x0600007E RID: 126
		internal abstract void SetCommitHook(SQLiteCommitCallback func);

		// Token: 0x0600007F RID: 127
		internal abstract void SetRollbackHook(SQLiteRollbackCallback func);

		// Token: 0x06000080 RID: 128
		internal abstract int GetCursorForTable(SqliteStatement stmt, int database, int rootPage);

		// Token: 0x06000081 RID: 129
		internal abstract long GetRowIdForCursor(SqliteStatement stmt, int cursor);

		// Token: 0x06000082 RID: 130
		internal abstract object GetValue(SqliteStatement stmt, int index, SQLiteType typ);

		// Token: 0x06000083 RID: 131 RVA: 0x0000309E File Offset: 0x0000129E
		protected virtual void Dispose(bool bDisposing)
		{
		}

		// Token: 0x06000084 RID: 132 RVA: 0x000030A0 File Offset: 0x000012A0
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x000030A9 File Offset: 0x000012A9
		internal static string SQLiteLastError(SqliteConnectionHandle db)
		{
			return SqliteConvert.UTF8ToString(UnsafeNativeMethods.sqlite3_errmsg(db), -1);
		}

		// Token: 0x06000086 RID: 134 RVA: 0x000030BC File Offset: 0x000012BC
		internal static void FinalizeStatement(SqliteStatementHandle stmt)
		{
			object @lock = SQLiteBase._lock;
			lock (@lock)
			{
				int num = UnsafeNativeMethods.sqlite3_finalize(stmt);
				if (num > 0)
				{
					throw new SqliteException(num, null);
				}
			}
		}

		// Token: 0x06000087 RID: 135 RVA: 0x0000310C File Offset: 0x0000130C
		internal static void CloseConnection(SqliteConnectionHandle db)
		{
			object @lock = SQLiteBase._lock;
			lock (@lock)
			{
				SQLiteBase.ResetConnection(db);
				int num = UnsafeNativeMethods.sqlite3_close(db);
				if (num > 0)
				{
					throw new SqliteException(num, SQLiteBase.SQLiteLastError(db));
				}
			}
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00003168 File Offset: 0x00001368
		internal static void ResetConnection(SqliteConnectionHandle db)
		{
			object @lock = SQLiteBase._lock;
			lock (@lock)
			{
				IntPtr intPtr = IntPtr.Zero;
				do
				{
					intPtr = UnsafeNativeMethods.sqlite3_next_stmt(db, intPtr);
					if (intPtr != IntPtr.Zero)
					{
						UnsafeNativeMethods.sqlite3_reset(intPtr);
					}
				}
				while (intPtr != IntPtr.Zero);
				UnsafeNativeMethods.sqlite3_exec(db, SqliteConvert.ToUTF8("ROLLBACK"), IntPtr.Zero, IntPtr.Zero, out intPtr);
			}
		}

		// Token: 0x04000007 RID: 7
		internal static object _lock = new object();
	}
}
