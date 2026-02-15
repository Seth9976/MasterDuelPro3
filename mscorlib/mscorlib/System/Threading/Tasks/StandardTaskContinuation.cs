using System;

namespace System.Threading.Tasks
{
	// Token: 0x020002C1 RID: 705
	internal class StandardTaskContinuation : TaskContinuation
	{
		// Token: 0x06001982 RID: 6530 RVA: 0x00061268 File Offset: 0x0005F468
		internal StandardTaskContinuation(Task task, TaskContinuationOptions options, TaskScheduler scheduler)
		{
			this.m_task = task;
			this.m_options = options;
			this.m_taskScheduler = scheduler;
			if (DebuggerSupport.LoggingOn)
			{
				CausalityTraceLevel causalityTraceLevel = CausalityTraceLevel.Required;
				Task task2 = this.m_task;
				string text = "Task.ContinueWith: ";
				Delegate action = task.m_action;
				DebuggerSupport.TraceOperationCreation(causalityTraceLevel, task2, text + ((action != null) ? action.ToString() : null), 0UL);
			}
			DebuggerSupport.AddToActiveTasks(this.m_task);
		}

		// Token: 0x06001983 RID: 6531 RVA: 0x000612CC File Offset: 0x0005F4CC
		internal override void Run(Task completedTask, bool bCanInlineContinuationTask)
		{
			TaskContinuationOptions options = this.m_options;
			bool flag = (completedTask.IsCompletedSuccessfully ? ((options & TaskContinuationOptions.NotOnRanToCompletion) == TaskContinuationOptions.None) : (completedTask.IsCanceled ? ((options & TaskContinuationOptions.NotOnCanceled) == TaskContinuationOptions.None) : ((options & TaskContinuationOptions.NotOnFaulted) == TaskContinuationOptions.None)));
			Task task = this.m_task;
			if (flag)
			{
				if (!task.IsCanceled && DebuggerSupport.LoggingOn)
				{
					DebuggerSupport.TraceOperationRelation(CausalityTraceLevel.Important, task, CausalityRelation.AssignDelegate);
				}
				task.m_taskScheduler = this.m_taskScheduler;
				if (bCanInlineContinuationTask && (options & TaskContinuationOptions.ExecuteSynchronously) != TaskContinuationOptions.None)
				{
					TaskContinuation.InlineIfPossibleOrElseQueue(task, true);
					return;
				}
				try
				{
					task.ScheduleAndStart(true);
					return;
				}
				catch (TaskSchedulerException)
				{
					return;
				}
			}
			task.InternalCancel(false);
		}

		// Token: 0x04000C18 RID: 3096
		internal readonly Task m_task;

		// Token: 0x04000C19 RID: 3097
		internal readonly TaskContinuationOptions m_options;

		// Token: 0x04000C1A RID: 3098
		private readonly TaskScheduler m_taskScheduler;
	}
}
