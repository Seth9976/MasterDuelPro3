using System;

namespace System.Transactions
{
	/// <summary>Describes the current status of a distributed transaction.</summary>
	// Token: 0x02000012 RID: 18
	public enum TransactionStatus
	{
		/// <summary>The status of the transaction is unknown, because some participants must still be polled.</summary>
		// Token: 0x04000024 RID: 36
		Active,
		/// <summary>The transaction has been committed.</summary>
		// Token: 0x04000025 RID: 37
		Committed,
		/// <summary>The transaction has been rolled back.</summary>
		// Token: 0x04000026 RID: 38
		Aborted,
		/// <summary>The status of the transaction is unknown.</summary>
		// Token: 0x04000027 RID: 39
		InDoubt
	}
}
