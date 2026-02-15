using System;

namespace System.Threading.Tasks
{
	// Token: 0x020002B3 RID: 691
	internal sealed class CompletionActionInvoker : IThreadPoolWorkItem
	{
		// Token: 0x06001966 RID: 6502 RVA: 0x00060D21 File Offset: 0x0005EF21
		internal CompletionActionInvoker(ITaskCompletionAction action, Task completingTask)
		{
			this.m_action = action;
			this.m_completingTask = completingTask;
		}

		// Token: 0x06001967 RID: 6503 RVA: 0x00060D37 File Offset: 0x0005EF37
		void IThreadPoolWorkItem.ExecuteWorkItem()
		{
			this.m_action.Invoke(this.m_completingTask);
		}

		// Token: 0x06001968 RID: 6504 RVA: 0x00002C89 File Offset: 0x00000E89
		public void MarkAborted(ThreadAbortException e)
		{
		}

		// Token: 0x04000BEE RID: 3054
		private readonly ITaskCompletionAction m_action;

		// Token: 0x04000BEF RID: 3055
		private readonly Task m_completingTask;
	}
}
