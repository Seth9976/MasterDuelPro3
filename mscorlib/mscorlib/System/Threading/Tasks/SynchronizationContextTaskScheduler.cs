using System;

namespace System.Threading.Tasks
{
	// Token: 0x020002CC RID: 716
	internal sealed class SynchronizationContextTaskScheduler : TaskScheduler
	{
		// Token: 0x060019CC RID: 6604 RVA: 0x00061E14 File Offset: 0x00060014
		internal SynchronizationContextTaskScheduler()
		{
			SynchronizationContext synchronizationContext = SynchronizationContext.Current;
			if (synchronizationContext == null)
			{
				throw new InvalidOperationException("The current SynchronizationContext may not be used as a TaskScheduler.");
			}
			this.m_synchronizationContext = synchronizationContext;
		}

		// Token: 0x060019CD RID: 6605 RVA: 0x00061E42 File Offset: 0x00060042
		protected internal override void QueueTask(Task task)
		{
			this.m_synchronizationContext.Post(SynchronizationContextTaskScheduler.s_postCallback, task);
		}

		// Token: 0x060019CE RID: 6606 RVA: 0x00061E55 File Offset: 0x00060055
		protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
		{
			return SynchronizationContext.Current == this.m_synchronizationContext && base.TryExecuteTask(task);
		}

		// Token: 0x04000C34 RID: 3124
		private SynchronizationContext m_synchronizationContext;

		// Token: 0x04000C35 RID: 3125
		private static readonly SendOrPostCallback s_postCallback = delegate(object s)
		{
			((Task)s).ExecuteEntry(true);
		};
	}
}
