using System;

namespace System.Transactions
{
	/// <summary>Provides additional information regarding a transaction.</summary>
	// Token: 0x0200000E RID: 14
	public class TransactionInformation
	{
		/// <summary>Gets the status of the transaction.</summary>
		/// <returns>A <see cref="T:System.Transactions.TransactionStatus" /> that contains the status of the transaction.</returns>
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000026 RID: 38 RVA: 0x00002346 File Offset: 0x00000546
		// (set) Token: 0x06000027 RID: 39 RVA: 0x0000234E File Offset: 0x0000054E
		public TransactionStatus Status
		{
			get
			{
				return this.status;
			}
			internal set
			{
				this.status = value;
			}
		}

		// Token: 0x0400001A RID: 26
		private TransactionStatus status;
	}
}
