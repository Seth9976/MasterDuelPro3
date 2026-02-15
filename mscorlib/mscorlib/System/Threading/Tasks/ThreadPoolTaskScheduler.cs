using System;
using Internal.Runtime.Augments;
using Internal.Threading.Tasks.Tracing;

namespace System.Threading.Tasks
{
	// Token: 0x020002CF RID: 719
	internal sealed class ThreadPoolTaskScheduler : TaskScheduler
	{
		// Token: 0x060019D4 RID: 6612 RVA: 0x00061EAE File Offset: 0x000600AE
		internal ThreadPoolTaskScheduler()
		{
		}

		// Token: 0x060019D5 RID: 6613 RVA: 0x00061EB8 File Offset: 0x000600B8
		protected internal override void QueueTask(Task task)
		{
			if (TaskTrace.Enabled)
			{
				Task internalCurrent = Task.InternalCurrent;
				Task parent = task.m_parent;
				TaskTrace.TaskScheduled(base.Id, (internalCurrent == null) ? 0 : internalCurrent.Id, task.Id, (parent == null) ? 0 : parent.Id, (int)task.Options);
			}
			if ((task.Options & TaskCreationOptions.LongRunning) != TaskCreationOptions.None)
			{
				RuntimeThread runtimeThread = RuntimeThread.Create(ThreadPoolTaskScheduler.s_longRunningThreadWork, 0);
				runtimeThread.IsBackground = true;
				runtimeThread.Start(task);
				return;
			}
			bool flag = (task.Options & TaskCreationOptions.PreferFairness) > TaskCreationOptions.None;
			ThreadPool.UnsafeQueueCustomWorkItem(task, flag);
		}

		// Token: 0x060019D6 RID: 6614 RVA: 0x00061F40 File Offset: 0x00060140
		protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
		{
			if (taskWasPreviouslyQueued && !ThreadPool.TryPopCustomWorkItem(task))
			{
				return false;
			}
			bool flag = false;
			try
			{
				flag = task.ExecuteEntry(false);
			}
			finally
			{
				if (taskWasPreviouslyQueued)
				{
					this.NotifyWorkItemProgress();
				}
			}
			return flag;
		}

		// Token: 0x060019D7 RID: 6615 RVA: 0x00061F84 File Offset: 0x00060184
		protected internal override bool TryDequeue(Task task)
		{
			return ThreadPool.TryPopCustomWorkItem(task);
		}

		// Token: 0x060019D8 RID: 6616 RVA: 0x00061F8C File Offset: 0x0006018C
		internal override void NotifyWorkItemProgress()
		{
			ThreadPool.NotifyWorkItemProgress();
		}

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x060019D9 RID: 6617 RVA: 0x00033991 File Offset: 0x00031B91
		internal override bool RequiresAtomicStartTransition
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04000C39 RID: 3129
		private static readonly ParameterizedThreadStart s_longRunningThreadWork = delegate(object s)
		{
			((Task)s).ExecuteEntry(false);
		};
	}
}
