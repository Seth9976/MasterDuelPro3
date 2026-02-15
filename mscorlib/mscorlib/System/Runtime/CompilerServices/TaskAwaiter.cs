using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;
using Internal.Threading.Tasks.Tracing;

namespace System.Runtime.CompilerServices
{
	/// <summary>Provides an object that waits for the completion of an asynchronous task.</summary>
	// Token: 0x0200059E RID: 1438
	public readonly struct TaskAwaiter : ICriticalNotifyCompletion, INotifyCompletion
	{
		// Token: 0x06002B1F RID: 11039 RVA: 0x000AAEEB File Offset: 0x000A90EB
		internal TaskAwaiter(Task task)
		{
			this.m_task = task;
		}

		/// <summary>Gets a value that indicates whether the asynchronous task has completed.</summary>
		/// <returns>true if the task has completed; otherwise, false.</returns>
		/// <exception cref="T:System.NullReferenceException">The <see cref="T:System.Runtime.CompilerServices.TaskAwaiter" /> object was not properly initialized.</exception>
		// Token: 0x17000581 RID: 1409
		// (get) Token: 0x06002B20 RID: 11040 RVA: 0x000AAEF4 File Offset: 0x000A90F4
		public bool IsCompleted
		{
			get
			{
				return this.m_task.IsCompleted;
			}
		}

		/// <summary>Sets the action to perform when the <see cref="T:System.Runtime.CompilerServices.TaskAwaiter" /> object stops waiting for the asynchronous task to complete.</summary>
		/// <param name="continuation">The action to perform when the wait operation completes.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="continuation" /> is null.</exception>
		/// <exception cref="T:System.NullReferenceException">The <see cref="T:System.Runtime.CompilerServices.TaskAwaiter" /> object was not properly initialized.</exception>
		// Token: 0x06002B21 RID: 11041 RVA: 0x000AAF01 File Offset: 0x000A9101
		public void OnCompleted(Action continuation)
		{
			TaskAwaiter.OnCompletedInternal(this.m_task, continuation, true, true);
		}

		/// <summary>Schedules the continuation action for the asynchronous task that is associated with this awaiter.</summary>
		/// <param name="continuation">The action to invoke when the await operation completes.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="continuation" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The awaiter was not properly initialized.</exception>
		// Token: 0x06002B22 RID: 11042 RVA: 0x000AAF11 File Offset: 0x000A9111
		public void UnsafeOnCompleted(Action continuation)
		{
			TaskAwaiter.OnCompletedInternal(this.m_task, continuation, true, false);
		}

		/// <summary>Ends the wait for the completion of the asynchronous task.</summary>
		/// <exception cref="T:System.NullReferenceException">The <see cref="T:System.Runtime.CompilerServices.TaskAwaiter" /> object was not properly initialized.</exception>
		/// <exception cref="T:System.Threading.Tasks.TaskCanceledException">The task was canceled.</exception>
		/// <exception cref="T:System.Exception">The task completed in a <see cref="F:System.Threading.Tasks.TaskStatus.Faulted" /> state.</exception>
		// Token: 0x06002B23 RID: 11043 RVA: 0x000AAF21 File Offset: 0x000A9121
		[StackTraceHidden]
		public void GetResult()
		{
			TaskAwaiter.ValidateEnd(this.m_task);
		}

		// Token: 0x06002B24 RID: 11044 RVA: 0x000AAF2E File Offset: 0x000A912E
		[StackTraceHidden]
		internal static void ValidateEnd(Task task)
		{
			if (task.IsWaitNotificationEnabledOrNotRanToCompletion)
			{
				TaskAwaiter.HandleNonSuccessAndDebuggerNotification(task);
			}
		}

		// Token: 0x06002B25 RID: 11045 RVA: 0x000AAF40 File Offset: 0x000A9140
		[StackTraceHidden]
		private static void HandleNonSuccessAndDebuggerNotification(Task task)
		{
			if (!task.IsCompleted)
			{
				task.InternalWait(-1, default(CancellationToken));
			}
			task.NotifyDebuggerOfWaitCompletionIfNecessary();
			if (!task.IsCompletedSuccessfully)
			{
				TaskAwaiter.ThrowForNonSuccess(task);
			}
		}

		// Token: 0x06002B26 RID: 11046 RVA: 0x000AAF7C File Offset: 0x000A917C
		[StackTraceHidden]
		private static void ThrowForNonSuccess(Task task)
		{
			TaskStatus status = task.Status;
			if (status == TaskStatus.Canceled)
			{
				ExceptionDispatchInfo cancellationExceptionDispatchInfo = task.GetCancellationExceptionDispatchInfo();
				if (cancellationExceptionDispatchInfo != null)
				{
					cancellationExceptionDispatchInfo.Throw();
				}
				throw new TaskCanceledException(task);
			}
			if (status != TaskStatus.Faulted)
			{
				return;
			}
			ReadOnlyCollection<ExceptionDispatchInfo> exceptionDispatchInfos = task.GetExceptionDispatchInfos();
			if (exceptionDispatchInfos.Count > 0)
			{
				exceptionDispatchInfos[0].Throw();
				return;
			}
			throw task.Exception;
		}

		// Token: 0x06002B27 RID: 11047 RVA: 0x000AAFD3 File Offset: 0x000A91D3
		internal static void OnCompletedInternal(Task task, Action continuation, bool continueOnCapturedContext, bool flowExecutionContext)
		{
			if (continuation == null)
			{
				throw new ArgumentNullException("continuation");
			}
			if (TaskTrace.Enabled)
			{
				continuation = TaskAwaiter.OutputWaitEtwEvents(task, continuation);
			}
			task.SetContinuationForAwait(continuation, continueOnCapturedContext, flowExecutionContext);
		}

		// Token: 0x06002B28 RID: 11048 RVA: 0x000AAFFC File Offset: 0x000A91FC
		private static Action OutputWaitEtwEvents(Task task, Action continuation)
		{
			Task internalCurrent = Task.InternalCurrent;
			TaskTrace.TaskWaitBegin_Asynchronous((internalCurrent != null) ? internalCurrent.m_taskScheduler.Id : TaskScheduler.Default.Id, (internalCurrent != null) ? internalCurrent.Id : 0, task.Id);
			return delegate
			{
				if (TaskTrace.Enabled)
				{
					Task internalCurrent2 = Task.InternalCurrent;
					TaskTrace.TaskWaitEnd((internalCurrent2 != null) ? internalCurrent2.m_taskScheduler.Id : TaskScheduler.Default.Id, (internalCurrent2 != null) ? internalCurrent2.Id : 0, task.Id);
				}
				continuation();
			};
		}

		// Token: 0x040015DC RID: 5596
		internal readonly Task m_task;
	}
}
