using System;
using System.Diagnostics;

namespace System.Threading.Tasks
{
	/// <summary>Represents an object that handles the low-level work of queuing tasks onto threads.</summary>
	// Token: 0x020002CA RID: 714
	[DebuggerDisplay("Id={Id}")]
	[DebuggerTypeProxy(typeof(TaskScheduler.SystemThreadingTasks_TaskSchedulerDebugView))]
	public abstract class TaskScheduler
	{
		/// <summary>Queues a <see cref="T:System.Threading.Tasks.Task" /> to the scheduler. </summary>
		/// <param name="task">The <see cref="T:System.Threading.Tasks.Task" /> to be queued.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="task" /> argument is null.</exception>
		// Token: 0x060019BD RID: 6589
		protected internal abstract void QueueTask(Task task);

		/// <summary>Determines whether the provided <see cref="T:System.Threading.Tasks.Task" /> can be executed synchronously in this call, and if it can, executes it.</summary>
		/// <returns>A Boolean value indicating whether the task was executed inline.</returns>
		/// <param name="task">The <see cref="T:System.Threading.Tasks.Task" /> to be executed.</param>
		/// <param name="taskWasPreviouslyQueued">A Boolean denoting whether or not task has previously been queued. If this parameter is True, then the task may have been previously queued (scheduled); if False, then the task is known not to have been queued, and this call is being made in order to execute the task inline without queuing it.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="task" /> argument is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <paramref name="task" /> was already executed.</exception>
		// Token: 0x060019BE RID: 6590
		protected abstract bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued);

		// Token: 0x060019BF RID: 6591 RVA: 0x00061C78 File Offset: 0x0005FE78
		internal bool TryRunInline(Task task, bool taskWasPreviouslyQueued)
		{
			TaskScheduler executingTaskScheduler = task.ExecutingTaskScheduler;
			if (executingTaskScheduler != this && executingTaskScheduler != null)
			{
				return executingTaskScheduler.TryRunInline(task, taskWasPreviouslyQueued);
			}
			StackGuard currentStackGuard;
			if (executingTaskScheduler == null || task.m_action == null || task.IsDelegateInvoked || task.IsCanceled || !(currentStackGuard = Task.CurrentStackGuard).TryBeginInliningScope())
			{
				return false;
			}
			bool flag = false;
			try
			{
				flag = this.TryExecuteTaskInline(task, taskWasPreviouslyQueued);
			}
			finally
			{
				currentStackGuard.EndInliningScope();
			}
			if (flag && !task.IsDelegateInvoked && !task.IsCanceled)
			{
				throw new InvalidOperationException("The TryExecuteTaskInline call to the underlying scheduler succeeded, but the task body was not invoked.");
			}
			return flag;
		}

		/// <summary>Attempts to dequeue a <see cref="T:System.Threading.Tasks.Task" /> that was previously queued to this scheduler.</summary>
		/// <returns>A Boolean denoting whether the <paramref name="task" /> argument was successfully dequeued.</returns>
		/// <param name="task">The <see cref="T:System.Threading.Tasks.Task" /> to be dequeued.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="task" /> argument is null.</exception>
		// Token: 0x060019C0 RID: 6592 RVA: 0x00033991 File Offset: 0x00031B91
		protected internal virtual bool TryDequeue(Task task)
		{
			return false;
		}

		// Token: 0x060019C1 RID: 6593 RVA: 0x00002C89 File Offset: 0x00000E89
		internal virtual void NotifyWorkItemProgress()
		{
		}

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x060019C2 RID: 6594 RVA: 0x0000C091 File Offset: 0x0000A291
		internal virtual bool RequiresAtomicStartTransition
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets the default <see cref="T:System.Threading.Tasks.TaskScheduler" /> instance that is provided by the .NET Framework.</summary>
		/// <returns>Returns the default <see cref="T:System.Threading.Tasks.TaskScheduler" /> instance.</returns>
		// Token: 0x170002BC RID: 700
		// (get) Token: 0x060019C4 RID: 6596 RVA: 0x00061D0C File Offset: 0x0005FF0C
		public static TaskScheduler Default
		{
			get
			{
				return TaskScheduler.s_defaultTaskScheduler;
			}
		}

		/// <summary>Gets the <see cref="T:System.Threading.Tasks.TaskScheduler" /> associated with the currently executing task.</summary>
		/// <returns>Returns the <see cref="T:System.Threading.Tasks.TaskScheduler" /> associated with the currently executing task.</returns>
		// Token: 0x170002BD RID: 701
		// (get) Token: 0x060019C5 RID: 6597 RVA: 0x00061D13 File Offset: 0x0005FF13
		public static TaskScheduler Current
		{
			get
			{
				return TaskScheduler.InternalCurrent ?? TaskScheduler.Default;
			}
		}

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x060019C6 RID: 6598 RVA: 0x00061D24 File Offset: 0x0005FF24
		internal static TaskScheduler InternalCurrent
		{
			get
			{
				Task internalCurrent = Task.InternalCurrent;
				if (internalCurrent == null || (internalCurrent.CreationOptions & TaskCreationOptions.HideScheduler) != TaskCreationOptions.None)
				{
					return null;
				}
				return internalCurrent.ExecutingTaskScheduler;
			}
		}

		/// <summary>Creates a <see cref="T:System.Threading.Tasks.TaskScheduler" /> associated with the current <see cref="T:System.Threading.SynchronizationContext" />.</summary>
		/// <returns>A <see cref="T:System.Threading.Tasks.TaskScheduler" /> associated with the current <see cref="T:System.Threading.SynchronizationContext" />, as determined by <see cref="P:System.Threading.SynchronizationContext.Current" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The current SynchronizationContext may not be used as a TaskScheduler.</exception>
		// Token: 0x060019C7 RID: 6599 RVA: 0x00061D4D File Offset: 0x0005FF4D
		public static TaskScheduler FromCurrentSynchronizationContext()
		{
			return new SynchronizationContextTaskScheduler();
		}

		/// <summary>Gets the unique ID for this <see cref="T:System.Threading.Tasks.TaskScheduler" />.</summary>
		/// <returns>Returns the unique ID for this <see cref="T:System.Threading.Tasks.TaskScheduler" />.</returns>
		// Token: 0x170002BF RID: 703
		// (get) Token: 0x060019C8 RID: 6600 RVA: 0x00061D54 File Offset: 0x0005FF54
		public int Id
		{
			get
			{
				if (this.m_taskSchedulerId == 0)
				{
					int num;
					do
					{
						num = Interlocked.Increment(ref TaskScheduler.s_taskSchedulerIdCounter);
					}
					while (num == 0);
					Interlocked.CompareExchange(ref this.m_taskSchedulerId, num, 0);
				}
				return this.m_taskSchedulerId;
			}
		}

		/// <summary>Attempts to execute the provided <see cref="T:System.Threading.Tasks.Task" /> on this scheduler.</summary>
		/// <returns>A Boolean that is true if <paramref name="task" /> was successfully executed, false if it was not. A common reason for execution failure is that the task had previously been executed or is in the process of being executed by another thread.</returns>
		/// <param name="task">A <see cref="T:System.Threading.Tasks.Task" /> object to be executed.</param>
		/// <exception cref="T:System.InvalidOperationException">The <paramref name="task" /> is not associated with this scheduler.</exception>
		// Token: 0x060019C9 RID: 6601 RVA: 0x00061D91 File Offset: 0x0005FF91
		protected bool TryExecuteTask(Task task)
		{
			if (task.ExecutingTaskScheduler != this)
			{
				throw new InvalidOperationException("ExecuteTask may not be called for a task which was previously queued to a different TaskScheduler.");
			}
			return task.ExecuteEntry(true);
		}

		// Token: 0x060019CA RID: 6602 RVA: 0x00061DB0 File Offset: 0x0005FFB0
		internal static void PublishUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs ueea)
		{
			using (LockHolder.Hold(TaskScheduler._unobservedTaskExceptionLockObject))
			{
				EventHandler<UnobservedTaskExceptionEventArgs> unobservedTaskException = TaskScheduler._unobservedTaskException;
				if (unobservedTaskException != null)
				{
					unobservedTaskException(sender, ueea);
				}
			}
		}

		// Token: 0x04000C2F RID: 3119
		private static readonly TaskScheduler s_defaultTaskScheduler = new ThreadPoolTaskScheduler();

		// Token: 0x04000C30 RID: 3120
		internal static int s_taskSchedulerIdCounter;

		// Token: 0x04000C31 RID: 3121
		private volatile int m_taskSchedulerId;

		// Token: 0x04000C32 RID: 3122
		private static EventHandler<UnobservedTaskExceptionEventArgs> _unobservedTaskException;

		// Token: 0x04000C33 RID: 3123
		private static readonly Lock _unobservedTaskExceptionLockObject = new Lock();

		// Token: 0x020002CB RID: 715
		internal sealed class SystemThreadingTasks_TaskSchedulerDebugView
		{
		}
	}
}
