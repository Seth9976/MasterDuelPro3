using System;

namespace System.Transactions
{
	/// <summary>Makes a code block transactional. This class cannot be inherited.</summary>
	// Token: 0x02000011 RID: 17
	public sealed class TransactionScope
	{
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600002E RID: 46 RVA: 0x0000242B File Offset: 0x0000062B
		internal bool IsComplete
		{
			get
			{
				return this.completed;
			}
		}

		// Token: 0x04000021 RID: 33
		private static TransactionOptions defaultOptions = new TransactionOptions(IsolationLevel.Serializable, TransactionManager.DefaultTimeout);

		// Token: 0x04000022 RID: 34
		private bool completed;
	}
}
