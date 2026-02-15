using System;
using System.Data;
using System.Data.Common;

namespace Mono.Data.Sqlite
{
	// Token: 0x02000024 RID: 36
	public sealed class SqliteTransaction : DbTransaction
	{
		// Token: 0x0600018B RID: 395 RVA: 0x0000CF4C File Offset: 0x0000B14C
		internal SqliteTransaction(SqliteConnection connection, bool deferredLock)
		{
			this._cnn = connection;
			this._version = this._cnn._version;
			this._level = ((!deferredLock) ? IsolationLevel.Serializable : IsolationLevel.ReadCommitted);
			if (this._cnn._transactionLevel++ == 0)
			{
				try
				{
					using (SqliteCommand sqliteCommand = this._cnn.CreateCommand())
					{
						if (!deferredLock)
						{
							sqliteCommand.CommandText = "BEGIN IMMEDIATE";
						}
						else
						{
							sqliteCommand.CommandText = "BEGIN";
						}
						sqliteCommand.ExecuteNonQuery();
					}
				}
				catch (SqliteException)
				{
					this._cnn._transactionLevel--;
					this._cnn = null;
					throw;
				}
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600018C RID: 396 RVA: 0x0000D038 File Offset: 0x0000B238
		public SqliteConnection Connection
		{
			get
			{
				return this._cnn;
			}
		}

		// Token: 0x0600018D RID: 397 RVA: 0x0000D040 File Offset: 0x0000B240
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				lock (this)
				{
					if (this.IsValid(false))
					{
						this.Rollback();
					}
					this._cnn = null;
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600018E RID: 398 RVA: 0x0000D098 File Offset: 0x0000B298
		public override void Rollback()
		{
			this.IsValid(true);
			SqliteTransaction.IssueRollback(this._cnn);
			this._cnn._transactionLevel = 0;
			this._cnn = null;
		}

		// Token: 0x0600018F RID: 399 RVA: 0x0000D0CC File Offset: 0x0000B2CC
		internal static void IssueRollback(SqliteConnection cnn)
		{
			using (SqliteCommand sqliteCommand = cnn.CreateCommand())
			{
				sqliteCommand.CommandText = "ROLLBACK";
				sqliteCommand.ExecuteNonQuery();
			}
		}

		// Token: 0x06000190 RID: 400 RVA: 0x0000D114 File Offset: 0x0000B314
		internal bool IsValid(bool throwError)
		{
			if (this._cnn == null)
			{
				if (throwError)
				{
					throw new ArgumentNullException("No connection associated with this transaction");
				}
				return false;
			}
			else if (this._cnn._transactionLevel == 0)
			{
				if (throwError)
				{
					throw new SqliteException(21, "No transaction is active on this connection");
				}
				return false;
			}
			else if (this._cnn._version != this._version)
			{
				if (throwError)
				{
					throw new SqliteException(21, "The connection was closed and re-opened, changes were rolled back");
				}
				return false;
			}
			else
			{
				if (this._cnn.State == ConnectionState.Open)
				{
					return true;
				}
				if (throwError)
				{
					throw new SqliteException(21, "Connection was closed");
				}
				return false;
			}
		}

		// Token: 0x040000B7 RID: 183
		internal SqliteConnection _cnn;

		// Token: 0x040000B8 RID: 184
		internal long _version;

		// Token: 0x040000B9 RID: 185
		private IsolationLevel _level;
	}
}
