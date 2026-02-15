using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Mono.Data.Sqlite
{
	// Token: 0x02000003 RID: 3
	internal class SQLite3_UTF16 : SQLite3
	{
		// Token: 0x0600003B RID: 59 RVA: 0x00002E90 File Offset: 0x00001090
		internal SQLite3_UTF16(SQLiteDateFormats fmt)
			: base(fmt)
		{
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002E99 File Offset: 0x00001099
		public override string ToString(IntPtr b, int nbytelen)
		{
			return SQLite3_UTF16.UTF16ToString(b, nbytelen);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002EA2 File Offset: 0x000010A2
		public static string UTF16ToString(IntPtr b, int nbytelen)
		{
			if (nbytelen == 0 || b == IntPtr.Zero)
			{
				return string.Empty;
			}
			if (nbytelen == -1)
			{
				return Marshal.PtrToStringUni(b);
			}
			return Marshal.PtrToStringUni(b, nbytelen / 2);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002ED8 File Offset: 0x000010D8
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
				if ((flags & SQLiteOpenFlagsEnum.Create) == SQLiteOpenFlagsEnum.None && !File.Exists(strFilename))
				{
					throw new SqliteException(14, strFilename);
				}
				IntPtr intPtr;
				int num = UnsafeNativeMethods.sqlite3_open16(strFilename, out intPtr);
				if (num > 0)
				{
					throw new SqliteException(num, null);
				}
				this._sql = intPtr;
			}
			this._functionsArray = SqliteFunction.BindFunctions(this);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002F71 File Offset: 0x00001171
		internal override void Bind_DateTime(SqliteStatement stmt, int index, DateTime dt)
		{
			this.Bind_Text(stmt, index, base.ToString(dt));
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002F84 File Offset: 0x00001184
		internal override void Bind_Text(SqliteStatement stmt, int index, string value)
		{
			int num = UnsafeNativeMethods.sqlite3_bind_text16(stmt._sqlite_stmt, index, value, value.Length * 2, (IntPtr)(-1));
			if (num > 0)
			{
				throw new SqliteException(num, this.SQLiteLastError());
			}
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002FC6 File Offset: 0x000011C6
		internal override DateTime GetDateTime(SqliteStatement stmt, int index)
		{
			return base.ToDateTime(this.GetText(stmt, index));
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002FD6 File Offset: 0x000011D6
		internal override string ColumnName(SqliteStatement stmt, int index)
		{
			return SQLite3_UTF16.UTF16ToString(UnsafeNativeMethods.sqlite3_column_name16(stmt._sqlite_stmt, index), -1);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002FEF File Offset: 0x000011EF
		internal override string GetText(SqliteStatement stmt, int index)
		{
			return SQLite3_UTF16.UTF16ToString(UnsafeNativeMethods.sqlite3_column_text16(stmt._sqlite_stmt, index), -1);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00003008 File Offset: 0x00001208
		internal override string ColumnOriginalName(SqliteStatement stmt, int index)
		{
			return SQLite3_UTF16.UTF16ToString(UnsafeNativeMethods.sqlite3_column_origin_name16(stmt._sqlite_stmt, index), -1);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00003021 File Offset: 0x00001221
		internal override string ColumnDatabaseName(SqliteStatement stmt, int index)
		{
			return SQLite3_UTF16.UTF16ToString(UnsafeNativeMethods.sqlite3_column_database_name16(stmt._sqlite_stmt, index), -1);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x0000303A File Offset: 0x0000123A
		internal override string ColumnTableName(SqliteStatement stmt, int index)
		{
			return SQLite3_UTF16.UTF16ToString(UnsafeNativeMethods.sqlite3_column_table_name16(stmt._sqlite_stmt, index), -1);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00003053 File Offset: 0x00001253
		internal override string GetParamValueText(IntPtr ptr)
		{
			return SQLite3_UTF16.UTF16ToString(UnsafeNativeMethods.sqlite3_value_text16(ptr), -1);
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00003061 File Offset: 0x00001261
		internal override void ReturnError(IntPtr context, string value)
		{
			UnsafeNativeMethods.sqlite3_result_error16(context, value, value.Length * 2);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00003072 File Offset: 0x00001272
		internal override void ReturnText(IntPtr context, string value)
		{
			UnsafeNativeMethods.sqlite3_result_text16(context, value, value.Length * 2, (IntPtr)(-1));
		}
	}
}
