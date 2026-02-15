using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace System.Runtime.CompilerServices
{
	/// <summary>Represents a builder for asynchronous methods that returns a task and provides a parameter for the result.</summary>
	/// <typeparam name="TResult">The result to use to complete the task.</typeparam>
	// Token: 0x020005A8 RID: 1448
	public struct AsyncTaskMethodBuilder<TResult>
	{
		/// <summary>Creates an instance of the <see cref="T:System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1" /> class.</summary>
		/// <returns>A new instance of the builder.</returns>
		// Token: 0x06002B4F RID: 11087 RVA: 0x000AB4F8 File Offset: 0x000A96F8
		public static AsyncTaskMethodBuilder<TResult> Create()
		{
			return default(AsyncTaskMethodBuilder<TResult>);
		}

		/// <summary>Begins running the builder with the associated state machine.</summary>
		/// <param name="stateMachine">The state machine instance, passed by reference.</param>
		/// <typeparam name="TStateMachine">The type of the state machine.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stateMachine" /> is null.</exception>
		// Token: 0x06002B50 RID: 11088 RVA: 0x000AB510 File Offset: 0x000A9710
		[DebuggerStepThrough]
		public void Start<TStateMachine>(ref TStateMachine stateMachine) where TStateMachine : IAsyncStateMachine
		{
			if (stateMachine == null)
			{
				throw new ArgumentNullException("stateMachine");
			}
			ExecutionContextSwitcher executionContextSwitcher = default(ExecutionContextSwitcher);
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				ExecutionContext.EstablishCopyOnWriteScope(ref executionContextSwitcher);
				stateMachine.MoveNext();
			}
			finally
			{
				executionContextSwitcher.Undo();
			}
		}

		/// <summary>Associates the builder with the specified state machine.</summary>
		/// <param name="stateMachine">The state machine instance to associate with the builder.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stateMachine" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The state machine was previously set.</exception>
		// Token: 0x06002B51 RID: 11089 RVA: 0x000AB570 File Offset: 0x000A9770
		public void SetStateMachine(IAsyncStateMachine stateMachine)
		{
			this.m_coreState.SetStateMachine(stateMachine);
		}

		/// <summary>Schedules the state machine to proceed to the next action when the specified awaiter completes. This method can be called from partially trusted code.</summary>
		/// <param name="awaiter">The awaiter.</param>
		/// <param name="stateMachine">The state machine.</param>
		/// <typeparam name="TAwaiter">The type of the awaiter.</typeparam>
		/// <typeparam name="TStateMachine">The type of the state machine.</typeparam>
		// Token: 0x06002B52 RID: 11090 RVA: 0x000AB580 File Offset: 0x000A9780
		public void AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine) where TAwaiter : ICriticalNotifyCompletion where TStateMachine : IAsyncStateMachine
		{
			try
			{
				AsyncMethodBuilderCore.MoveNextRunner moveNextRunner = null;
				Action completionAction = this.m_coreState.GetCompletionAction(AsyncCausalityTracer.LoggingOn ? this.Task : null, ref moveNextRunner);
				if (this.m_coreState.m_stateMachine == null)
				{
					Task<TResult> task = this.Task;
					this.m_coreState.PostBoxInitialization(stateMachine, moveNextRunner, task);
				}
				awaiter.UnsafeOnCompleted(completionAction);
			}
			catch (Exception ex)
			{
				AsyncMethodBuilderCore.ThrowAsync(ex, null);
			}
		}

		/// <summary>Gets the task for this builder.</summary>
		/// <returns>The task for this builder.</returns>
		// Token: 0x17000587 RID: 1415
		// (get) Token: 0x06002B53 RID: 11091 RVA: 0x000AB600 File Offset: 0x000A9800
		public Task<TResult> Task
		{
			get
			{
				Task<TResult> task = this.m_task;
				if (task == null)
				{
					task = (this.m_task = new Task<TResult>());
				}
				return task;
			}
		}

		/// <summary>Marks the task as successfully completed.</summary>
		/// <param name="result">The result to use to complete the task.</param>
		/// <exception cref="T:System.InvalidOperationException">The task has already completed.</exception>
		// Token: 0x06002B54 RID: 11092 RVA: 0x000AB628 File Offset: 0x000A9828
		public void SetResult(TResult result)
		{
			Task<TResult> task = this.m_task;
			if (task == null)
			{
				this.m_task = AsyncTaskMethodBuilder<TResult>.GetTaskForResult(result);
				return;
			}
			if (AsyncCausalityTracer.LoggingOn)
			{
				AsyncCausalityTracer.TraceOperationCompletion(CausalityTraceLevel.Required, task.Id, AsyncCausalityStatus.Completed);
			}
			if (global::System.Threading.Tasks.Task.s_asyncDebuggingEnabled)
			{
				global::System.Threading.Tasks.Task.RemoveFromActiveTasks(task.Id);
			}
			if (!task.TrySetResult(result))
			{
				throw new InvalidOperationException(Environment.GetResourceString("An attempt was made to transition a task to a final state when it had already completed."));
			}
		}

		// Token: 0x06002B55 RID: 11093 RVA: 0x000AB68C File Offset: 0x000A988C
		internal void SetResult(Task<TResult> completedTask)
		{
			if (this.m_task == null)
			{
				this.m_task = completedTask;
				return;
			}
			this.SetResult(default(TResult));
		}

		/// <summary>Marks the task as failed and binds the specified exception to the task.</summary>
		/// <param name="exception">The exception to bind to the task.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="exception" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The task has already completed.</exception>
		// Token: 0x06002B56 RID: 11094 RVA: 0x000AB6B8 File Offset: 0x000A98B8
		public void SetException(Exception exception)
		{
			if (exception == null)
			{
				throw new ArgumentNullException("exception");
			}
			Task<TResult> task = this.m_task;
			if (task == null)
			{
				task = this.Task;
			}
			OperationCanceledException ex = exception as OperationCanceledException;
			if (!((ex != null) ? task.TrySetCanceled(ex.CancellationToken, ex) : task.TrySetException(exception)))
			{
				throw new InvalidOperationException(Environment.GetResourceString("An attempt was made to transition a task to a final state when it had already completed."));
			}
		}

		// Token: 0x06002B57 RID: 11095 RVA: 0x000AB718 File Offset: 0x000A9918
		internal static Task<TResult> GetTaskForResult(TResult result)
		{
			if (default(TResult) != null)
			{
				if (typeof(TResult) == typeof(bool))
				{
					return JitHelpers.UnsafeCast<Task<TResult>>(((bool)((object)result)) ? AsyncTaskCache.TrueTask : AsyncTaskCache.FalseTask);
				}
				if (typeof(TResult) == typeof(int))
				{
					int num = (int)((object)result);
					if (num < 9 && num >= -1)
					{
						return JitHelpers.UnsafeCast<Task<TResult>>(AsyncTaskCache.Int32Tasks[num - -1]);
					}
				}
				else if ((typeof(TResult) == typeof(uint) && (uint)((object)result) == 0U) || (typeof(TResult) == typeof(byte) && (byte)((object)result) == 0) || (typeof(TResult) == typeof(sbyte) && (sbyte)((object)result) == 0) || (typeof(TResult) == typeof(char) && (char)((object)result) == '\0') || (typeof(TResult) == typeof(long) && (long)((object)result) == 0L) || (typeof(TResult) == typeof(ulong) && (ulong)((object)result) == 0UL) || (typeof(TResult) == typeof(short) && (short)((object)result) == 0) || (typeof(TResult) == typeof(ushort) && (ushort)((object)result) == 0) || (typeof(TResult) == typeof(IntPtr) && (IntPtr)0 == (IntPtr)((object)result)) || (typeof(TResult) == typeof(UIntPtr) && (UIntPtr)0 == (UIntPtr)((object)result)))
				{
					return AsyncTaskMethodBuilder<TResult>.s_defaultResultTask;
				}
			}
			else if (result == null)
			{
				return AsyncTaskMethodBuilder<TResult>.s_defaultResultTask;
			}
			return new Task<TResult>(result);
		}

		// Token: 0x040015EB RID: 5611
		internal static readonly Task<TResult> s_defaultResultTask = AsyncTaskCache.CreateCacheableTask<TResult>(default(TResult));

		// Token: 0x040015EC RID: 5612
		private AsyncMethodBuilderCore m_coreState;

		// Token: 0x040015ED RID: 5613
		private Task<TResult> m_task;
	}
}
