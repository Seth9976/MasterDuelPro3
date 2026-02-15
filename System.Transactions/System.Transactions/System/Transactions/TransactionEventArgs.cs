using System;

namespace System.Transactions
{
	/// <summary>Provides data for the following transaction events: <see cref="E:System.Transactions.TransactionManager.DistributedTransactionStarted" />, <see cref="E:System.Transactions.Transaction.TransactionCompleted" />.</summary>
	// Token: 0x0200000C RID: 12
	public class TransactionEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.TransactionEventArgs" /> class. </summary>
		// Token: 0x06000021 RID: 33 RVA: 0x00002314 File Offset: 0x00000514
		public TransactionEventArgs()
		{
		}

		// Token: 0x06000022 RID: 34 RVA: 0x0000231C File Offset: 0x0000051C
		internal TransactionEventArgs(Transaction transaction)
			: this()
		{
			this.transaction = transaction;
		}

		// Token: 0x04000019 RID: 25
		private Transaction transaction;
	}
}
