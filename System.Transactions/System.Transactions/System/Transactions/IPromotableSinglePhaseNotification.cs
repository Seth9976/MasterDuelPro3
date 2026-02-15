using System;

namespace System.Transactions
{
	/// <summary>Describes an object that acts as a commit delegate for a non-distributed transaction internal to a resource manager.</summary>
	// Token: 0x02000007 RID: 7
	public interface IPromotableSinglePhaseNotification
	{
		/// <summary>Notifies an enlisted object that the transaction is being rolled back.</summary>
		/// <param name="singlePhaseEnlistment">A <see cref="T:System.Transactions.SinglePhaseEnlistment" /> object used to send a response to the transaction manager.</param>
		// Token: 0x06000009 RID: 9
		void Rollback(SinglePhaseEnlistment singlePhaseEnlistment);
	}
}
