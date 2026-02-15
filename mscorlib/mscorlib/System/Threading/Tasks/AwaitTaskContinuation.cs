using System;
using System.Runtime.CompilerServices;
using Internal.Runtime.Augments;

namespace System.Threading.Tasks
{
	// Token: 0x020002C6 RID: 710
	internal class AwaitTaskContinuation : TaskContinuation, IThreadPoolWorkItem
	{
		// Token: 0x06001991 RID: 6545 RVA: 0x00061528 File Offset: 0x0005F728
		internal AwaitTaskContinuation(Action action, bool flowExecutionContext)
		{
			this.m_action = action;
			if (flowExecutionContext)
			{
				this.m_capturedContext = ExecutionContext.Capture();
			}
		}

		// Token: 0x06001992 RID: 6546 RVA: 0x00061548 File Offset: 0x0005F748
		protected Task CreateTask(Action<object> action, object state, TaskScheduler scheduler)
		{
			return new Task(action, state, null, default(CancellationToken), TaskCreationOptions.None, InternalTaskOptions.QueuedByRuntime, scheduler);
		}

		// Token: 0x06001993 RID: 6547 RVA: 0x0006156D File Offset: 0x0005F76D
		internal override void Run(Task ignored, bool canInlineContinuationTask)
		{
			if (canInlineContinuationTask && AwaitTaskContinuation.IsValidLocationForInlining)
			{
				this.RunCallback(AwaitTaskContinuation.GetInvokeActionCallback(), this.m_action, ref Task.t_currentTask);
				return;
			}
			ThreadPool.UnsafeQueueCustomWorkItem(this, false);
		}

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x06001994 RID: 6548 RVA: 0x00061598 File Offset: 0x0005F798
		internal static bool IsValidLocationForInlining
		{
			get
			{
				SynchronizationContext synchronizationContext = SynchronizationContext.Current;
				if (synchronizationContext != null && synchronizationContext.GetType() != typeof(SynchronizationContext))
				{
					return false;
				}
				TaskScheduler internalCurrent = TaskScheduler.InternalCurrent;
				return internalCurrent == null || internalCurrent == TaskScheduler.Default;
			}
		}

		// Token: 0x06001995 RID: 6549 RVA: 0x000615DA File Offset: 0x0005F7DA
		void IThreadPoolWorkItem.ExecuteWorkItem()
		{
			if (this.m_capturedContext == null)
			{
				this.m_action();
				return;
			}
			ExecutionContext.Run(this.m_capturedContext, AwaitTaskContinuation.GetInvokeActionCallback(), this.m_action);
		}

		// Token: 0x06001996 RID: 6550 RVA: 0x00061606 File Offset: 0x0005F806
		private static void InvokeAction(object state)
		{
			((Action)state)();
		}

		// Token: 0x06001997 RID: 6551 RVA: 0x00061614 File Offset: 0x0005F814
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected static ContextCallback GetInvokeActionCallback()
		{
			ContextCallback contextCallback = AwaitTaskContinuation.s_invokeActionCallback;
			if (contextCallback == null)
			{
				contextCallback = (AwaitTaskContinuation.s_invokeActionCallback = new ContextCallback(AwaitTaskContinuation.InvokeAction));
			}
			return contextCallback;
		}

		// Token: 0x06001998 RID: 6552 RVA: 0x00061640 File Offset: 0x0005F840
		protected void RunCallback(ContextCallback callback, object state, ref Task currentTask)
		{
			Task task = currentTask;
			SynchronizationContext currentExplicit = SynchronizationContext.CurrentExplicit;
			try
			{
				if (task != null)
				{
					currentTask = null;
				}
				callback(state);
			}
			catch (Exception ex)
			{
				AwaitTaskContinuation.ThrowAsyncIfNecessary(ex);
			}
			finally
			{
				if (task != null)
				{
					currentTask = task;
				}
				SynchronizationContext.SetSynchronizationContext(currentExplicit);
			}
		}

		// Token: 0x06001999 RID: 6553 RVA: 0x00061698 File Offset: 0x0005F898
		internal static void RunOrScheduleAction(Action action, bool allowInlining, ref Task currentTask)
		{
			if (!allowInlining || !AwaitTaskContinuation.IsValidLocationForInlining)
			{
				AwaitTaskContinuation.UnsafeScheduleAction(action);
				return;
			}
			Task task = currentTask;
			try
			{
				if (task != null)
				{
					currentTask = null;
				}
				action();
			}
			catch (Exception ex)
			{
				AwaitTaskContinuation.ThrowAsyncIfNecessary(ex);
			}
			finally
			{
				if (task != null)
				{
					currentTask = task;
				}
			}
		}

		// Token: 0x0600199A RID: 6554 RVA: 0x000616F4 File Offset: 0x0005F8F4
		internal static void UnsafeScheduleAction(Action action)
		{
			ThreadPool.UnsafeQueueCustomWorkItem(new AwaitTaskContinuation(action, false), false);
		}

		// Token: 0x0600199B RID: 6555 RVA: 0x00061703 File Offset: 0x0005F903
		protected static void ThrowAsyncIfNecessary(Exception exc)
		{
			RuntimeAugments.ReportUnhandledException(exc);
		}

		// Token: 0x0600199C RID: 6556 RVA: 0x00002C89 File Offset: 0x00000E89
		public void MarkAborted(ThreadAbortException e)
		{
		}

		// Token: 0x04000C22 RID: 3106
		private readonly ExecutionContext m_capturedContext;

		// Token: 0x04000C23 RID: 3107
		protected readonly Action m_action;

		// Token: 0x04000C24 RID: 3108
		private static ContextCallback s_invokeActionCallback;
	}
}
