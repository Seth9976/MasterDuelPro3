using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Unity;

namespace System.Transactions
{
	/// <summary>Represents a transaction.</summary>
	// Token: 0x0200000B RID: 11
	[Serializable]
	public class Transaction : IDisposable, ISerializable
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000B RID: 11 RVA: 0x0000208F File Offset: 0x0000028F
		internal List<IEnlistmentNotification> Volatiles
		{
			get
			{
				if (this.volatiles == null)
				{
					this.volatiles = new List<IEnlistmentNotification>();
				}
				return this.volatiles;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000C RID: 12 RVA: 0x000020AA File Offset: 0x000002AA
		internal List<ISinglePhaseNotification> Durables
		{
			get
			{
				if (this.durables == null)
				{
					this.durables = new List<ISinglePhaseNotification>();
				}
				return this.durables;
			}
		}

		/// <summary>Gets a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data required to serialize this transaction. </summary>
		/// <param name="serializationInfo">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data.</param>
		/// <param name="context">The destination (see <see cref="T:System.Runtime.Serialization.StreamingContext" /> ) for this serialization. </param>
		// Token: 0x0600000D RID: 13 RVA: 0x000020C5 File Offset: 0x000002C5
		[MonoTODO]
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets or sets the ambient transaction.</summary>
		/// <returns>A <see cref="T:System.Transactions.Transaction" /> that describes the current transaction.</returns>
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000E RID: 14 RVA: 0x000020CC File Offset: 0x000002CC
		public static Transaction Current
		{
			get
			{
				Transaction.EnsureIncompleteCurrentScope();
				return Transaction.CurrentInternal;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000F RID: 15 RVA: 0x000020D8 File Offset: 0x000002D8
		internal static Transaction CurrentInternal
		{
			get
			{
				return Transaction.ambient;
			}
		}

		/// <summary>Retrieves additional information about a transaction.</summary>
		/// <returns>A <see cref="T:System.Transactions.TransactionInformation" /> that contains additional information about the transaction.</returns>
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000010 RID: 16 RVA: 0x000020DF File Offset: 0x000002DF
		public TransactionInformation TransactionInformation
		{
			get
			{
				Transaction.EnsureIncompleteCurrentScope();
				return this.info;
			}
		}

		/// <summary>Releases the resources that are held by the object.</summary>
		// Token: 0x06000011 RID: 17 RVA: 0x000020EC File Offset: 0x000002EC
		public void Dispose()
		{
			if (this.TransactionInformation.Status == TransactionStatus.Active)
			{
				this.Rollback();
			}
		}

		/// <summary>Enlists a volatile resource manager that supports two phase commit to participate in a transaction.</summary>
		/// <returns>An <see cref="T:System.Transactions.Enlistment" /> object that describes the enlistment.</returns>
		/// <param name="enlistmentNotification">An object that implements the <see cref="T:System.Transactions.IEnlistmentNotification" /> interface to receive two phase commit notifications. </param>
		/// <param name="enlistmentOptions">
		///   <see cref="F:System.Transactions.EnlistmentOptions.EnlistDuringPrepareRequired" /> if the resource manager wants to perform additional work during the prepare phase.</param>
		// Token: 0x06000012 RID: 18 RVA: 0x00002101 File Offset: 0x00000301
		[MonoTODO("EnlistmentOptions being ignored")]
		public Enlistment EnlistVolatile(IEnlistmentNotification enlistmentNotification, EnlistmentOptions enlistmentOptions)
		{
			return this.EnlistVolatileInternal(enlistmentNotification, enlistmentOptions);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x0000210B File Offset: 0x0000030B
		private Enlistment EnlistVolatileInternal(IEnlistmentNotification notification, EnlistmentOptions options)
		{
			Transaction.EnsureIncompleteCurrentScope();
			this.Volatiles.Add(notification);
			return new Enlistment();
		}

		/// <summary>Determines whether this transaction and the specified object are equal.</summary>
		/// <returns>true if <paramref name="obj" /> and this transaction are identical; otherwise, false.</returns>
		/// <param name="obj">The object to compare with this instance.</param>
		// Token: 0x06000014 RID: 20 RVA: 0x00002123 File Offset: 0x00000323
		public override bool Equals(object obj)
		{
			return this.Equals(obj as Transaction);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002131 File Offset: 0x00000331
		private bool Equals(Transaction t)
		{
			return t == this || (t != null && this.level == t.level && this.info == t.info);
		}

		/// <summary>Tests whether two specified <see cref="T:System.Transactions.Transaction" /> instances are equivalent.</summary>
		/// <returns>true if <paramref name="x" /> and <paramref name="y" /> are equal; otherwise, false.</returns>
		/// <param name="x">The <see cref="T:System.Transactions.Transaction" /> instance that is to the left of the equality operator.</param>
		/// <param name="y">The <see cref="T:System.Transactions.Transaction" /> instance that is to the right of the equality operator.</param>
		// Token: 0x06000016 RID: 22 RVA: 0x0000215C File Offset: 0x0000035C
		public static bool operator ==(Transaction x, Transaction y)
		{
			if (x == null)
			{
				return y == null;
			}
			return x.Equals(y);
		}

		/// <summary>Returns a value that indicates whether two <see cref="T:System.Transactions.Transaction" /> instances are not equal.</summary>
		/// <returns>true if <paramref name="x" /> and <paramref name="y" /> are not equal; otherwise, false.</returns>
		/// <param name="x">The <see cref="T:System.Transactions.Transaction" /> instance that is to the left of the inequality operator.</param>
		/// <param name="y">The <see cref="T:System.Transactions.Transaction" /> instance that is to the right of the inequality operator.</param>
		// Token: 0x06000017 RID: 23 RVA: 0x0000216D File Offset: 0x0000036D
		public static bool operator !=(Transaction x, Transaction y)
		{
			return !(x == y);
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06000018 RID: 24 RVA: 0x00002179 File Offset: 0x00000379
		public override int GetHashCode()
		{
			return (int)(this.level ^ (IsolationLevel)this.info.GetHashCode() ^ (IsolationLevel)this.dependents.GetHashCode());
		}

		/// <summary>Rolls back (aborts) the transaction.</summary>
		// Token: 0x06000019 RID: 25 RVA: 0x00002199 File Offset: 0x00000399
		public void Rollback()
		{
			this.Rollback(null);
		}

		/// <summary>Rolls back (aborts) the transaction.</summary>
		/// <param name="e">An explanation of why a rollback occurred.</param>
		// Token: 0x0600001A RID: 26 RVA: 0x000021A2 File Offset: 0x000003A2
		public void Rollback(Exception e)
		{
			Transaction.EnsureIncompleteCurrentScope();
			this.Rollback(e, null);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000021B4 File Offset: 0x000003B4
		internal void Rollback(Exception ex, object abortingEnlisted)
		{
			if (this.aborted)
			{
				this.FireCompleted();
				return;
			}
			if (this.info.Status == TransactionStatus.Committed)
			{
				throw new TransactionException("Transaction has already been committed. Cannot accept any new work.");
			}
			this.innerException = ex;
			SinglePhaseEnlistment singlePhaseEnlistment = new SinglePhaseEnlistment();
			foreach (IEnlistmentNotification enlistmentNotification in this.Volatiles)
			{
				if (enlistmentNotification != abortingEnlisted)
				{
					enlistmentNotification.Rollback(singlePhaseEnlistment);
				}
			}
			List<ISinglePhaseNotification> list = this.Durables;
			if (list.Count > 0 && list[0] != abortingEnlisted)
			{
				list[0].Rollback(singlePhaseEnlistment);
			}
			if (this.pspe != null && this.pspe != abortingEnlisted)
			{
				this.pspe.Rollback(singlePhaseEnlistment);
			}
			this.Aborted = true;
			this.FireCompleted();
		}

		// Token: 0x17000006 RID: 6
		// (set) Token: 0x0600001C RID: 28 RVA: 0x00002294 File Offset: 0x00000494
		private bool Aborted
		{
			set
			{
				this.aborted = value;
				if (this.aborted)
				{
					this.info.Status = TransactionStatus.Aborted;
				}
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600001D RID: 29 RVA: 0x000022B1 File Offset: 0x000004B1
		internal TransactionScope Scope
		{
			get
			{
				return this.scope;
			}
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000022B9 File Offset: 0x000004B9
		private void FireCompleted()
		{
			if (this.TransactionCompletedInternal != null)
			{
				this.TransactionCompletedInternal(this, new TransactionEventArgs(this));
			}
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000022D5 File Offset: 0x000004D5
		private static void EnsureIncompleteCurrentScope()
		{
			if (Transaction.CurrentInternal == null)
			{
				return;
			}
			if (Transaction.CurrentInternal.Scope != null && Transaction.CurrentInternal.Scope.IsComplete)
			{
				throw new InvalidOperationException("The current TransactionScope is already complete");
			}
		}

		// Token: 0x06000020 RID: 32 RVA: 0x0000230D File Offset: 0x0000050D
		internal Transaction()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x0400000E RID: 14
		[ThreadStatic]
		private static Transaction ambient;

		// Token: 0x0400000F RID: 15
		private IsolationLevel level;

		// Token: 0x04000010 RID: 16
		private TransactionInformation info;

		// Token: 0x04000011 RID: 17
		private ArrayList dependents;

		// Token: 0x04000012 RID: 18
		private List<IEnlistmentNotification> volatiles;

		// Token: 0x04000013 RID: 19
		private List<ISinglePhaseNotification> durables;

		// Token: 0x04000014 RID: 20
		private IPromotableSinglePhaseNotification pspe;

		// Token: 0x04000015 RID: 21
		private bool aborted;

		// Token: 0x04000016 RID: 22
		private TransactionScope scope;

		// Token: 0x04000017 RID: 23
		private Exception innerException;

		// Token: 0x04000018 RID: 24
		[CompilerGenerated]
		private TransactionCompletedEventHandler TransactionCompletedInternal;
	}
}
