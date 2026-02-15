using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace System.Runtime.CompilerServices
{
	/// <summary>Represents an object that waits for the completion of an asynchronous task and provides a parameter for the result.</summary>
	/// <typeparam name="TResult">The result for the task.</typeparam>
	// Token: 0x020005A0 RID: 1440
	public readonly struct TaskAwaiter<TResult> : ICriticalNotifyCompletion, INotifyCompletion
	{
		// Token: 0x06002B2B RID: 11051 RVA: 0x000AB0C3 File Offset: 0x000A92C3
		internal TaskAwaiter(Task<TResult> task)
		{
			this.m_task = task;
		}

		/// <summary>Gets a value that indicates whether the asynchronous task has completed.</summary>
		/// <returns>true if the task has completed; otherwise, false.</returns>
		/// <exception cref="T:System.NullReferenceException">The <see cref="T:System.Runtime.CompilerServices.TaskAwaiter`1" /> object was not properly initialized.</exception>
		// Token: 0x17000582 RID: 1410
		// (get) Token: 0x06002B2C RID: 11052 RVA: 0x000AB0CC File Offset: 0x000A92CC
		public bool IsCompleted
		{
			get
			{
				return this.m_task.IsCompleted;
			}
		}

		/// <summary>Sets the action to perform when the <see cref="T:System.Runtime.CompilerServices.TaskAwaiter`1" /> object stops waiting for the asynchronous task to complete.</summary>
		/// <param name="continuation">The action to perform when the wait operation completes.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="continuation" /> is null.</exception>
		/// <exception cref="T:System.NullReferenceException">The <see cref="T:System.Runtime.CompilerServices.TaskAwaiter`1" /> object was not properly initialized.</exception>
		// Token: 0x06002B2D RID: 11053 RVA: 0x000AB0D9 File Offset: 0x000A92D9
		public void OnCompleted(Action continuation)
		{
			TaskAwaiter.OnCompletedInternal(this.m_task, continuation, true, true);
		}

		/// <summary>Schedules the continuation action for the asynchronous task associated with this awaiter.</summary>
		/// <param name="continuation">The action to invoke when the await operation completes.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="continuation" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The awaiter was not properly initialized.</exception>
		// Token: 0x06002B2E RID: 11054 RVA: 0x000AB0E9 File Offset: 0x000A92E9
		public void UnsafeOnCompleted(Action continuation)
		{
			TaskAwaiter.OnCompletedInternal(this.m_task, continuation, true, false);
		}

		/// <summary>Ends the wait for the completion of the asynchronous task.</summary>
		/// <returns>The result of the completed task.</returns>
		/// <exception cref="T:System.NullReferenceException">The <see cref="T:System.Runtime.CompilerServices.TaskAwaiter`1" /> object was not properly initialized.</exception>
		/// <exception cref="T:System.Threading.Tasks.TaskCanceledException">The task was canceled.</exception>
		/// <exception cref="T:System.Exception">The task completed in a <see cref="F:System.Threading.Tasks.TaskStatus.Faulted" /> state.</exception>
		// Token: 0x06002B2F RID: 11055 RVA: 0x000AB0F9 File Offset: 0x000A92F9
		[StackTraceHidden]
		public TResult GetResult()
		{
			TaskAwaiter.ValidateEnd(this.m_task);
			return this.m_task.ResultOnSuccess;
		}

		// Token: 0x040015DF RID: 5599
		private readonly Task<TResult> m_task;
	}
}
