using System;

namespace System.Transactions
{
	/// <summary>Describes an interface that a resource manager should implement to provide two phase commit notification callbacks for the transaction manager upon enlisting for participation.</summary>
	// Token: 0x02000006 RID: 6
	public interface IEnlistmentNotification
	{
		/// <summary>Notifies an enlisted object that a transaction is being rolled back (aborted).</summary>
		/// <param name="enlistment">A <see cref="T:System.Transactions.Enlistment" /> object used to send a response to the transaction manager.</param>
		// Token: 0x06000008 RID: 8
		void Rollback(Enlistment enlistment);
	}
}
