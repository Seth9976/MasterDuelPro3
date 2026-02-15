using System;
using System.Runtime.CompilerServices;

namespace System.Threading.Tasks
{
	// Token: 0x020002B8 RID: 696
	internal class StackGuard
	{
		// Token: 0x06001969 RID: 6505 RVA: 0x00060D4A File Offset: 0x0005EF4A
		internal bool TryBeginInliningScope()
		{
			if (this.m_inliningDepth < 20 || RuntimeHelpers.TryEnsureSufficientExecutionStack())
			{
				this.m_inliningDepth++;
				return true;
			}
			return false;
		}

		// Token: 0x0600196A RID: 6506 RVA: 0x00060D6E File Offset: 0x0005EF6E
		internal void EndInliningScope()
		{
			this.m_inliningDepth--;
			if (this.m_inliningDepth < 0)
			{
				this.m_inliningDepth = 0;
			}
		}

		// Token: 0x04000C10 RID: 3088
		private int m_inliningDepth;
	}
}
