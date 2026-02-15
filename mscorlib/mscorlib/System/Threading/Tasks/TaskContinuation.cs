using System;

namespace System.Threading.Tasks
{
	// Token: 0x020002C0 RID: 704
	internal abstract class TaskContinuation
	{
		// Token: 0x0600197F RID: 6527
		internal abstract void Run(Task completedTask, bool bCanInlineContinuationTask);

		// Token: 0x06001980 RID: 6528 RVA: 0x000611F4 File Offset: 0x0005F3F4
		protected static void InlineIfPossibleOrElseQueue(Task task, bool needsProtection)
		{
			if (needsProtection)
			{
				if (!task.MarkStarted())
				{
					return;
				}
			}
			else
			{
				task.m_stateFlags |= 65536;
			}
			try
			{
				if (!task.m_taskScheduler.TryRunInline(task, false))
				{
					task.m_taskScheduler.QueueTask(task);
				}
			}
			catch (Exception ex)
			{
				TaskSchedulerException ex2 = new TaskSchedulerException(ex);
				task.AddException(ex2);
				task.Finish(false);
			}
		}
	}
}
