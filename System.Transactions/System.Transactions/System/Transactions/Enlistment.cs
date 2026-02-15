using System;

namespace System.Transactions
{
	/// <summary>Facilitates communication between an enlisted transaction participant and the transaction manager during the final phase of the transaction.</summary>
	// Token: 0x02000004 RID: 4
	public class Enlistment
	{
		// Token: 0x06000005 RID: 5 RVA: 0x00002067 File Offset: 0x00000267
		internal Enlistment()
		{
			this.done = false;
		}

		/// <summary>Indicates that the transaction participant has completed its work.</summary>
		// Token: 0x06000006 RID: 6 RVA: 0x00002076 File Offset: 0x00000276
		public void Done()
		{
			this.done = true;
			this.InternalOnDone();
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002085 File Offset: 0x00000285
		internal virtual void InternalOnDone()
		{
		}

		// Token: 0x04000002 RID: 2
		internal bool done;
	}
}
