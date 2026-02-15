using System;
using System.Data;
using System.Runtime.InteropServices;
using System.Threading;

namespace Mono.Data.Sqlite
{
	// Token: 0x02000002 RID: 2
	internal class SQLite3 : SQLiteBase
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		internal SQLite3(SQLiteDateFormats fmt)
			: base(fmt)
		{
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002059 File Offset: 0x00000259
		protected override void Dispose(bool bDisposing)
		{
			if (bDisposing)
			{
				this.Close();
			}
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002068 File Offset: 0x00000268
		internal override void Close()
		{
			if (this._sql != null)
			{
				if (this._usePool)
				{
					SQLiteBase.ResetConnection(this._sql);
					SqliteConnectionPool.Add(this._fileName, this._sql, this._poolVersion);
				}
				else
				{
					this._sql.Dispose();
				}
			}
			this._sql = null;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000004 RID: 4 RVA: 0x000020C4 File Offset: 0x000002C4
		internal override string Version
		{
			get
			{
				return SQLite3.SQLiteVersion;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000005 RID: 5 RVA: 0x000020CB File Offset: 0x000002CB
		internal static string SQLiteVersion
		{
			get
			{
				return SqliteConvert.UTF8ToString(UnsafeNativeMethods.sqlite3_libversion(), -1);
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000006 RID: 6 RVA: 0x000020D8 File Offset: 0x000002D8
		internal override int Changes
		{
			get
			{
				return UnsafeNativeMethods.sqlite3_changes(this._sql);
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000020EC File Offset: 0x000002EC
		internal override void Open(string strFilename, SQLiteOpenFlagsEnum flags, int maxPoolSize, bool usePool)
		{
			if (this._sql != null)
			{
				return;
			}
			this._usePool = usePool;
			if (usePool)
			{
				this._fileName = strFilename;
				this._sql = SqliteConnectionPool.Remove(strFilename, maxPoolSize, out this._poolVersion);
			}
			if (this._sql == null)
			{
				IntPtr intPtr;
				int num;
				try
				{
					num = UnsafeNativeMethods.sqlite3_open_v2(SqliteConvert.ToUTF8(strFilename), out intPtr, (int)flags, IntPtr.Zero);
				}
				catch (EntryPointNotFoundException ex)
				{
					Console.WriteLine("Your sqlite3 version is old - please upgrade to at least v3.5.0!");
					num = UnsafeNativeMethods.sqlite3_open(SqliteConvert.ToUTF8(strFilename), out intPtr);
				}
				if (num > 0)
				{
					throw new SqliteException(num, null);
				}
				this._sql = intPtr;
			}
			this._functionsArray = SqliteFunction.BindFunctions(this);
			this.SetTimeout(0);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000021B0 File Offset: 0x000003B0
		internal override void SetTimeout(int nTimeoutMS)
		{
			int num = UnsafeNativeMethods.sqlite3_busy_timeout(this._sql, nTimeoutMS);
			if (num > 0)
			{
				throw new SqliteException(num, this.SQLiteLastError());
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000021E4 File Offset: 0x000003E4
		internal override bool Step(SqliteStatement stmt)
		{
			Random random = null;
			uint tickCount = (uint)Environment.TickCount;
			uint num = (uint)(stmt._command._commandTimeout * 1000);
			int num2;
			int num3;
			for (;;)
			{
				num2 = UnsafeNativeMethods.sqlite3_step(stmt._sqlite_stmt);
				if (num2 == 100)
				{
					break;
				}
				if (num2 == 101)
				{
					return false;
				}
				if (num2 > 0)
				{
					num3 = this.Reset(stmt);
					if (num3 == 0)
					{
						goto Block_4;
					}
					if ((num3 == 6 || num3 == 5) && stmt._command != null)
					{
						if (random == null)
						{
							random = new Random();
						}
						if (Environment.TickCount - (int)tickCount > (int)num)
						{
							goto Block_8;
						}
						Thread.CurrentThread.Join(random.Next(1, 150));
					}
				}
			}
			return true;
			Block_4:
			throw new SqliteException(num2, this.SQLiteLastError());
			Block_8:
			throw new SqliteException(num3, this.SQLiteLastError());
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000022B4 File Offset: 0x000004B4
		internal override int Reset(SqliteStatement stmt)
		{
			int num = UnsafeNativeMethods.sqlite3_reset(stmt._sqlite_stmt);
			if (num == 17)
			{
				string text;
				using (SqliteStatement sqliteStatement = this.Prepare(null, stmt._sqlStatement, null, (uint)(stmt._command._commandTimeout * 1000), out text))
				{
					stmt._sqlite_stmt.Dispose();
					stmt._sqlite_stmt = sqliteStatement._sqlite_stmt;
					sqliteStatement._sqlite_stmt = null;
					stmt.BindParameters();
				}
				return -1;
			}
			if (num == 6 || num == 5)
			{
				return num;
			}
			if (num > 0)
			{
				throw new SqliteException(num, this.SQLiteLastError());
			}
			return 0;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000236C File Offset: 0x0000056C
		internal override string SQLiteLastError()
		{
			return SQLiteBase.SQLiteLastError(this._sql);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x0000237C File Offset: 0x0000057C
		internal override SqliteStatement Prepare(SqliteConnection cnn, string strSql, SqliteStatement previous, uint timeoutMS, out string strRemain)
		{
			IntPtr zero = IntPtr.Zero;
			IntPtr zero2 = IntPtr.Zero;
			int num = 0;
			int num2 = 17;
			int num3 = 0;
			byte[] array = SqliteConvert.ToUTF8(strSql);
			SqliteStatement sqliteStatement = null;
			Random random = null;
			uint tickCount = (uint)Environment.TickCount;
			GCHandle gchandle = GCHandle.Alloc(array, GCHandleType.Pinned);
			IntPtr intPtr = gchandle.AddrOfPinnedObject();
			SqliteStatement sqliteStatement2;
			try
			{
				while ((num2 == 17 || num2 == 6 || num2 == 5) && num3 < 3)
				{
					num2 = UnsafeNativeMethods.sqlite3_prepare(this._sql, intPtr, array.Length - 1, out zero, out zero2);
					num = -1;
					if (num2 == 17)
					{
						num3++;
					}
					else if (num2 == 1)
					{
						if (string.Compare(this.SQLiteLastError(), "near \"TYPES\": syntax error", StringComparison.OrdinalIgnoreCase) == 0)
						{
							int num4 = strSql.IndexOf(';');
							if (num4 == -1)
							{
								num4 = strSql.Length - 1;
							}
							string text = strSql.Substring(0, num4 + 1);
							strSql = strSql.Substring(num4 + 1);
							strRemain = string.Empty;
							while (sqliteStatement == null && strSql.Length > 0)
							{
								sqliteStatement = this.Prepare(cnn, strSql, previous, timeoutMS, out strRemain);
								strSql = strRemain;
							}
							if (sqliteStatement != null)
							{
								sqliteStatement.SetTypes(text);
							}
							return sqliteStatement;
						}
						if (!this._buildingSchema && string.Compare(this.SQLiteLastError(), 0, "no such table: TEMP.SCHEMA", 0, 26, StringComparison.OrdinalIgnoreCase) == 0)
						{
							strRemain = string.Empty;
							this._buildingSchema = true;
							try
							{
								ISQLiteSchemaExtensions isqliteSchemaExtensions = ((IServiceProvider)SqliteFactory.Instance).GetService(typeof(ISQLiteSchemaExtensions)) as ISQLiteSchemaExtensions;
								if (isqliteSchemaExtensions != null)
								{
									isqliteSchemaExtensions.BuildTempSchema(cnn);
								}
								while (sqliteStatement == null && strSql.Length > 0)
								{
									sqliteStatement = this.Prepare(cnn, strSql, previous, timeoutMS, out strRemain);
									strSql = strRemain;
								}
								return sqliteStatement;
							}
							finally
							{
								this._buildingSchema = false;
							}
						}
					}
					else if (num2 == 6 || num2 == 5)
					{
						if (random == null)
						{
							random = new Random();
						}
						if (Environment.TickCount - (int)tickCount > (int)timeoutMS)
						{
							throw new SqliteException(num2, this.SQLiteLastError());
						}
						Thread.CurrentThread.Join(random.Next(1, 150));
					}
				}
				if (num2 > 0)
				{
					throw new SqliteException(num2, this.SQLiteLastError());
				}
				strRemain = SqliteConvert.UTF8ToString(zero2, num);
				if (zero != IntPtr.Zero)
				{
					sqliteStatement = new SqliteStatement(this, zero, strSql.Substring(0, strSql.Length - strRemain.Length), previous);
				}
				sqliteStatement2 = sqliteStatement;
			}
			finally
			{
				gchandle.Free();
			}
			return sqliteStatement2;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002650 File Offset: 0x00000850
		internal override void Bind_Double(SqliteStatement stmt, int index, double value)
		{
			int num = UnsafeNativeMethods.sqlite3_bind_double(stmt._sqlite_stmt, index, value);
			if (num > 0)
			{
				throw new SqliteException(num, this.SQLiteLastError());
			}
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002684 File Offset: 0x00000884
		internal override void Bind_Int32(SqliteStatement stmt, int index, int value)
		{
			int num = UnsafeNativeMethods.sqlite3_bind_int(stmt._sqlite_stmt, index, value);
			if (num > 0)
			{
				throw new SqliteException(num, this.SQLiteLastError());
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000026B8 File Offset: 0x000008B8
		internal override void Bind_Int64(SqliteStatement stmt, int index, long value)
		{
			int num = UnsafeNativeMethods.sqlite3_bind_int64(stmt._sqlite_stmt, index, value);
			if (num > 0)
			{
				throw new SqliteException(num, this.SQLiteLastError());
			}
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000026EC File Offset: 0x000008EC
		internal override void Bind_Text(SqliteStatement stmt, int index, string value)
		{
			byte[] array = SqliteConvert.ToUTF8(value);
			int num = UnsafeNativeMethods.sqlite3_bind_text(stmt._sqlite_stmt, index, array, array.Length - 1, (IntPtr)(-1));
			if (num > 0)
			{
				throw new SqliteException(num, this.SQLiteLastError());
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002734 File Offset: 0x00000934
		internal override void Bind_DateTime(SqliteStatement stmt, int index, DateTime dt)
		{
			byte[] array = base.ToUTF8(dt);
			int num = UnsafeNativeMethods.sqlite3_bind_text(stmt._sqlite_stmt, index, array, array.Length - 1, (IntPtr)(-1));
			if (num > 0)
			{
				throw new SqliteException(num, this.SQLiteLastError());
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x0000277C File Offset: 0x0000097C
		internal override void Bind_Blob(SqliteStatement stmt, int index, byte[] blobData)
		{
			int num = UnsafeNativeMethods.sqlite3_bind_blob(stmt._sqlite_stmt, index, blobData, blobData.Length, (IntPtr)(-1));
			if (num > 0)
			{
				throw new SqliteException(num, this.SQLiteLastError());
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000027BC File Offset: 0x000009BC
		internal override void Bind_Null(SqliteStatement stmt, int index)
		{
			int num = UnsafeNativeMethods.sqlite3_bind_null(stmt._sqlite_stmt, index);
			if (num > 0)
			{
				throw new SqliteException(num, this.SQLiteLastError());
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000027EF File Offset: 0x000009EF
		internal override int Bind_ParamCount(SqliteStatement stmt)
		{
			return UnsafeNativeMethods.sqlite3_bind_parameter_count(stmt._sqlite_stmt);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002801 File Offset: 0x00000A01
		internal override string Bind_ParamName(SqliteStatement stmt, int index)
		{
			return SqliteConvert.UTF8ToString(UnsafeNativeMethods.sqlite3_bind_parameter_name(stmt._sqlite_stmt, index), -1);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x0000281A File Offset: 0x00000A1A
		internal override int ColumnCount(SqliteStatement stmt)
		{
			return UnsafeNativeMethods.sqlite3_column_count(stmt._sqlite_stmt);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x0000282C File Offset: 0x00000A2C
		internal override string ColumnName(SqliteStatement stmt, int index)
		{
			return SqliteConvert.UTF8ToString(UnsafeNativeMethods.sqlite3_column_name(stmt._sqlite_stmt, index), -1);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002845 File Offset: 0x00000A45
		internal override TypeAffinity ColumnAffinity(SqliteStatement stmt, int index)
		{
			return UnsafeNativeMethods.sqlite3_column_type(stmt._sqlite_stmt, index);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002858 File Offset: 0x00000A58
		internal override string ColumnType(SqliteStatement stmt, int index, out TypeAffinity nAffinity)
		{
			int num = -1;
			IntPtr intPtr = UnsafeNativeMethods.sqlite3_column_decltype(stmt._sqlite_stmt, index);
			nAffinity = this.ColumnAffinity(stmt, index);
			if (intPtr != IntPtr.Zero)
			{
				return SqliteConvert.UTF8ToString(intPtr, num);
			}
			string[] typeDefinitions = stmt.TypeDefinitions;
			if (typeDefinitions != null && index < typeDefinitions.Length && typeDefinitions[index] != null)
			{
				return typeDefinitions[index];
			}
			return string.Empty;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000028C2 File Offset: 0x00000AC2
		internal override string ColumnOriginalName(SqliteStatement stmt, int index)
		{
			return SqliteConvert.UTF8ToString(UnsafeNativeMethods.sqlite3_column_origin_name(stmt._sqlite_stmt, index), -1);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000028DB File Offset: 0x00000ADB
		internal override string ColumnDatabaseName(SqliteStatement stmt, int index)
		{
			return SqliteConvert.UTF8ToString(UnsafeNativeMethods.sqlite3_column_database_name(stmt._sqlite_stmt, index), -1);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000028F4 File Offset: 0x00000AF4
		internal override string ColumnTableName(SqliteStatement stmt, int index)
		{
			return SqliteConvert.UTF8ToString(UnsafeNativeMethods.sqlite3_column_table_name(stmt._sqlite_stmt, index), -1);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002910 File Offset: 0x00000B10
		internal override void ColumnMetaData(string dataBase, string table, string column, out string dataType, out string collateSequence, out bool notNull, out bool primaryKey, out bool autoIncrement)
		{
			int num = -1;
			int num2 = -1;
			IntPtr intPtr;
			IntPtr intPtr2;
			int num4;
			int num5;
			int num6;
			int num3 = UnsafeNativeMethods.sqlite3_table_column_metadata(this._sql, SqliteConvert.ToUTF8(dataBase), SqliteConvert.ToUTF8(table), SqliteConvert.ToUTF8(column), out intPtr, out intPtr2, out num4, out num5, out num6);
			if (num3 > 0)
			{
				throw new SqliteException(num3, this.SQLiteLastError());
			}
			dataType = SqliteConvert.UTF8ToString(intPtr, num);
			collateSequence = SqliteConvert.UTF8ToString(intPtr2, num2);
			notNull = num4 == 1;
			primaryKey = num5 == 1;
			autoIncrement = num6 == 1;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002994 File Offset: 0x00000B94
		internal override double GetDouble(SqliteStatement stmt, int index)
		{
			return UnsafeNativeMethods.sqlite3_column_double(stmt._sqlite_stmt, index);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000029B4 File Offset: 0x00000BB4
		internal override int GetInt32(SqliteStatement stmt, int index)
		{
			return UnsafeNativeMethods.sqlite3_column_int(stmt._sqlite_stmt, index);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000029C8 File Offset: 0x00000BC8
		internal override long GetInt64(SqliteStatement stmt, int index)
		{
			return UnsafeNativeMethods.sqlite3_column_int64(stmt._sqlite_stmt, index);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000029E8 File Offset: 0x00000BE8
		internal override string GetText(SqliteStatement stmt, int index)
		{
			return SqliteConvert.UTF8ToString(UnsafeNativeMethods.sqlite3_column_text(stmt._sqlite_stmt, index), -1);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002A01 File Offset: 0x00000C01
		internal override DateTime GetDateTime(SqliteStatement stmt, int index)
		{
			return base.ToDateTime(UnsafeNativeMethods.sqlite3_column_text(stmt._sqlite_stmt, index), -1);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002A1C File Offset: 0x00000C1C
		internal unsafe override long GetBytes(SqliteStatement stmt, int index, int nDataOffset, byte[] bDest, int nStart, int nLength)
		{
			int num = nLength;
			int num2 = UnsafeNativeMethods.sqlite3_column_bytes(stmt._sqlite_stmt, index);
			IntPtr intPtr = UnsafeNativeMethods.sqlite3_column_blob(stmt._sqlite_stmt, index);
			if (bDest == null)
			{
				return (long)num2;
			}
			if (num + nStart > bDest.Length)
			{
				num = bDest.Length - nStart;
			}
			if (num + nDataOffset > num2)
			{
				num = num2 - nDataOffset;
			}
			if (num > 0)
			{
				Marshal.Copy((IntPtr)((void*)((byte*)(void*)intPtr + nDataOffset)), bDest, nStart, num);
			}
			else
			{
				num = 0;
			}
			return (long)num;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002AA3 File Offset: 0x00000CA3
		internal override bool IsNull(SqliteStatement stmt, int index)
		{
			return this.ColumnAffinity(stmt, index) == TypeAffinity.Null;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002AB0 File Offset: 0x00000CB0
		internal override void CreateFunction(string strFunction, int nArgs, bool needCollSeq, SQLiteCallback func, SQLiteCallback funcstep, SQLiteFinalCallback funcfinal)
		{
			int num = UnsafeNativeMethods.sqlite3_create_function(this._sql, SqliteConvert.ToUTF8(strFunction), nArgs, 4, IntPtr.Zero, func, funcstep, funcfinal);
			if (num == 0)
			{
				num = UnsafeNativeMethods.sqlite3_create_function(this._sql, SqliteConvert.ToUTF8(strFunction), nArgs, 1, IntPtr.Zero, func, funcstep, funcfinal);
			}
			if (num > 0)
			{
				throw new SqliteException(num, this.SQLiteLastError());
			}
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002B20 File Offset: 0x00000D20
		internal override void CreateCollation(string strCollation, SQLiteCollation func, SQLiteCollation func16)
		{
			int num = UnsafeNativeMethods.sqlite3_create_collation(this._sql, SqliteConvert.ToUTF8(strCollation), 2, IntPtr.Zero, func16);
			if (num == 0)
			{
				UnsafeNativeMethods.sqlite3_create_collation(this._sql, SqliteConvert.ToUTF8(strCollation), 1, IntPtr.Zero, func);
			}
			if (num > 0)
			{
				throw new SqliteException(num, this.SQLiteLastError());
			}
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002B84 File Offset: 0x00000D84
		internal unsafe override long GetParamValueBytes(IntPtr p, int nDataOffset, byte[] bDest, int nStart, int nLength)
		{
			int num = nLength;
			int num2 = UnsafeNativeMethods.sqlite3_value_bytes(p);
			IntPtr intPtr = UnsafeNativeMethods.sqlite3_value_blob(p);
			if (bDest == null)
			{
				return (long)num2;
			}
			if (num + nStart > bDest.Length)
			{
				num = bDest.Length - nStart;
			}
			if (num + nDataOffset > num2)
			{
				num = num2 - nDataOffset;
			}
			if (num > 0)
			{
				Marshal.Copy((IntPtr)((void*)((byte*)(void*)intPtr + nDataOffset)), bDest, nStart, num);
			}
			else
			{
				num = 0;
			}
			return (long)num;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002BF4 File Offset: 0x00000DF4
		internal override double GetParamValueDouble(IntPtr ptr)
		{
			return UnsafeNativeMethods.sqlite3_value_double(ptr);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002C0C File Offset: 0x00000E0C
		internal override long GetParamValueInt64(IntPtr ptr)
		{
			return UnsafeNativeMethods.sqlite3_value_int64(ptr);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002C21 File Offset: 0x00000E21
		internal override string GetParamValueText(IntPtr ptr)
		{
			return SqliteConvert.UTF8ToString(UnsafeNativeMethods.sqlite3_value_text(ptr), -1);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002C2F File Offset: 0x00000E2F
		internal override TypeAffinity GetParamValueType(IntPtr ptr)
		{
			return UnsafeNativeMethods.sqlite3_value_type(ptr);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002C37 File Offset: 0x00000E37
		internal override void ReturnBlob(IntPtr context, byte[] value)
		{
			UnsafeNativeMethods.sqlite3_result_blob(context, value, value.Length, (IntPtr)(-1));
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002C49 File Offset: 0x00000E49
		internal override void ReturnDouble(IntPtr context, double value)
		{
			UnsafeNativeMethods.sqlite3_result_double(context, value);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002C52 File Offset: 0x00000E52
		internal override void ReturnError(IntPtr context, string value)
		{
			UnsafeNativeMethods.sqlite3_result_error(context, SqliteConvert.ToUTF8(value), value.Length);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002C66 File Offset: 0x00000E66
		internal override void ReturnInt64(IntPtr context, long value)
		{
			UnsafeNativeMethods.sqlite3_result_int64(context, value);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002C6F File Offset: 0x00000E6F
		internal override void ReturnNull(IntPtr context)
		{
			UnsafeNativeMethods.sqlite3_result_null(context);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002C78 File Offset: 0x00000E78
		internal override void ReturnText(IntPtr context, string value)
		{
			byte[] array = SqliteConvert.ToUTF8(value);
			UnsafeNativeMethods.sqlite3_result_text(context, SqliteConvert.ToUTF8(value), array.Length - 1, (IntPtr)(-1));
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002CA3 File Offset: 0x00000EA3
		internal override IntPtr AggregateContext(IntPtr context)
		{
			return UnsafeNativeMethods.sqlite3_aggregate_context(context, 1);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002CAC File Offset: 0x00000EAC
		internal override void SetPassword(byte[] passwordBytes)
		{
			int num = UnsafeNativeMethods.sqlite3_key(this._sql, passwordBytes, passwordBytes.Length);
			if (num > 0)
			{
				throw new SqliteException(num, this.SQLiteLastError());
			}
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002CE2 File Offset: 0x00000EE2
		internal override void SetUpdateHook(SQLiteUpdateCallback func)
		{
			UnsafeNativeMethods.sqlite3_update_hook(this._sql, func, IntPtr.Zero);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002CFB File Offset: 0x00000EFB
		internal override void SetCommitHook(SQLiteCommitCallback func)
		{
			UnsafeNativeMethods.sqlite3_commit_hook(this._sql, func, IntPtr.Zero);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002D14 File Offset: 0x00000F14
		internal override void SetRollbackHook(SQLiteRollbackCallback func)
		{
			UnsafeNativeMethods.sqlite3_rollback_hook(this._sql, func, IntPtr.Zero);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002D30 File Offset: 0x00000F30
		internal override object GetValue(SqliteStatement stmt, int index, SQLiteType typ)
		{
			if (this.IsNull(stmt, index))
			{
				return DBNull.Value;
			}
			TypeAffinity typeAffinity = typ.Affinity;
			Type type = null;
			if (typ.Type != DbType.Object)
			{
				type = SqliteConvert.SQLiteTypeToType(typ);
				typeAffinity = SqliteConvert.TypeToAffinity(type);
			}
			TypeAffinity typeAffinity2 = typeAffinity;
			switch (typeAffinity2)
			{
			case TypeAffinity.Int64:
				if (type == null)
				{
					return this.GetInt64(stmt, index);
				}
				return Convert.ChangeType(this.GetInt64(stmt, index), type, null);
			case TypeAffinity.Double:
				if (type == null)
				{
					return this.GetDouble(stmt, index);
				}
				return Convert.ChangeType(this.GetDouble(stmt, index), type, null);
			default:
				if (typeAffinity2 != TypeAffinity.DateTime)
				{
					return this.GetText(stmt, index);
				}
				return this.GetDateTime(stmt, index);
			case TypeAffinity.Blob:
			{
				if (typ.Type == DbType.Guid && typ.Affinity == TypeAffinity.Text)
				{
					return new Guid(this.GetText(stmt, index));
				}
				int num = (int)this.GetBytes(stmt, index, 0, null, 0, 0);
				byte[] array = new byte[num];
				this.GetBytes(stmt, index, 0, array, 0, num);
				if (typ.Type == DbType.Guid && num == 16)
				{
					return new Guid(array);
				}
				return array;
			}
			}
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002E77 File Offset: 0x00001077
		internal override int GetCursorForTable(SqliteStatement stmt, int db, int rootPage)
		{
			return -1;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002E7A File Offset: 0x0000107A
		internal override long GetRowIdForCursor(SqliteStatement stmt, int cursor)
		{
			return 0L;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002E7E File Offset: 0x0000107E
		internal override void GetIndexColumnExtendedInfo(string database, string index, string column, out int sortMode, out int onError, out string collationSequence)
		{
			sortMode = 0;
			onError = 2;
			collationSequence = "BINARY";
		}

		// Token: 0x04000001 RID: 1
		protected SqliteConnectionHandle _sql;

		// Token: 0x04000002 RID: 2
		protected string _fileName;

		// Token: 0x04000003 RID: 3
		protected bool _usePool;

		// Token: 0x04000004 RID: 4
		protected int _poolVersion;

		// Token: 0x04000005 RID: 5
		private bool _buildingSchema;

		// Token: 0x04000006 RID: 6
		protected SqliteFunction[] _functionsArray;
	}
}
