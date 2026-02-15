using System;
using System.Data.Common;
using System.Runtime.Serialization;

namespace Mono.Data.Sqlite
{
	// Token: 0x02000016 RID: 22
	[Serializable]
	public sealed class SqliteException : DbException
	{
		// Token: 0x06000127 RID: 295 RVA: 0x0000A9E8 File Offset: 0x00008BE8
		private SqliteException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x06000128 RID: 296 RVA: 0x0000A9F2 File Offset: 0x00008BF2
		public SqliteException(int errorCode, string extendedInformation)
			: base(SqliteException.GetStockErrorMessage(errorCode, extendedInformation))
		{
			this._errorCode = (SQLiteErrorCode)errorCode;
		}

		// Token: 0x06000129 RID: 297 RVA: 0x0000AA08 File Offset: 0x00008C08
		public SqliteException()
		{
		}

		// Token: 0x0600012B RID: 299 RVA: 0x0000AB14 File Offset: 0x00008D14
		private static string GetStockErrorMessage(int errorCode, string errorMessage)
		{
			if (errorMessage == null)
			{
				errorMessage = string.Empty;
			}
			if (errorMessage.Length > 0)
			{
				errorMessage = "\r\n" + errorMessage;
			}
			if (errorCode < 0 || errorCode >= SqliteException._errorMessages.Length)
			{
				errorCode = 1;
			}
			return SqliteException._errorMessages[errorCode] + errorMessage;
		}

		// Token: 0x04000061 RID: 97
		private SQLiteErrorCode _errorCode;

		// Token: 0x04000062 RID: 98
		private static string[] _errorMessages = new string[]
		{
			"SQLite OK", "SQLite error", "An internal logic error in SQLite", "Access permission denied", "Callback routine requested an abort", "The database file is locked", "A table in the database is locked", "malloc() failed", "Attempt to write a read-only database", "Operation terminated by sqlite3_interrupt()",
			"Some kind of disk I/O error occurred", "The database disk image is malformed", "Table or record not found", "Insertion failed because the database is full", "Unable to open the database file", "Database lock protocol error", "Database is empty", "The database schema changed", "Too much data for one row of a table", "Abort due to constraint violation",
			"Data type mismatch", "Library used incorrectly", "Uses OS features not supported on host", "Authorization denied", "Auxiliary database format error", "2nd parameter to sqlite3_bind() out of range", "File opened that is not a database file"
		};
	}
}
