using System;
using System.Transactions;

namespace Mono.Data.Sqlite
{
	// Token: 0x02000015 RID: 21
	internal class SQLiteEnlistment : IEnlistmentNotification
	{
		// Token: 0x06000124 RID: 292 RVA: 0x0000A93C File Offset: 0x00008B3C
		internal SQLiteEnlistment(SqliteConnection cnn, Transaction scope)
		{
			this._transaction = cnn.BeginTransaction();
			this._scope = scope;
			this._disposeConnection = false;
			this._scope.EnlistVolatile(this, EnlistmentOptions.None);
		}

		// Token: 0x06000125 RID: 293 RVA: 0x0000A977 File Offset: 0x00008B77
		private void Cleanup(SqliteConnection cnn)
		{
			if (this._disposeConnection)
			{
				cnn.Dispose();
			}
			this._transaction = null;
			this._scope = null;
		}

		// Token: 0x06000126 RID: 294 RVA: 0x0000A998 File Offset: 0x00008B98
		public void Rollback(Enlistment enlistment)
		{
			SqliteConnection connection = this._transaction.Connection;
			connection._enlistment = null;
			try
			{
				this._transaction.Rollback();
				enlistment.Done();
			}
			finally
			{
				this.Cleanup(connection);
			}
		}

		// Token: 0x0400005E RID: 94
		internal SqliteTransaction _transaction;

		// Token: 0x0400005F RID: 95
		internal Transaction _scope;

		// Token: 0x04000060 RID: 96
		internal bool _disposeConnection;
	}
}
