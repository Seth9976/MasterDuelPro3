using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace System.Runtime.CompilerServices
{
	/// <summary>Represents a builder for asynchronous methods that return a task.</summary>
	// Token: 0x020005A7 RID: 1447
	public struct AsyncTaskMethodBuilder
	{
		/// <summary>Creates an instance of the <see cref="T:System.Runtime.CompilerServices.AsyncTaskMethodBuilder" /> class.</summary>
		/// <returns>A new instance of the builder.</returns>
		// Token: 0x06002B47 RID: 11079 RVA: 0x000AB428 File Offset: 0x000A9628
		public static AsyncTaskMethodBuilder Create()
		{
			return default(AsyncTaskMethodBuilder);
		}

		/// <summary>Begins running the builder with the associated state machine.</summary>
		/// <param name="stateMachine">The state machine instance, passed by reference.</param>
		/// <typeparam name="TStateMachine">The type of the state machine.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stateMachine" /> is null.</exception>
		// Token: 0x06002B48 RID: 11080 RVA: 0x000AB440 File Offset: 0x000A9640
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
		// Token: 0x06002B49 RID: 11081 RVA: 0x000AB4A0 File Offset: 0x000A96A0
		public void SetStateMachine(IAsyncStateMachine stateMachine)
		{
			this.m_builder.SetStateMachine(stateMachine);
		}

		/// <summary>Schedules the state machine to proceed to the next action when the specified awaiter completes. This method can be called from partially trusted code.</summary>
		/// <param name="awaiter">The awaiter.</param>
		/// <param name="stateMachine">The state machine.</param>
		/// <typeparam name="TAwaiter">The type of the awaiter.</typeparam>
		/// <typeparam name="TStateMachine">The type of the state machine.</typeparam>
		// Token: 0x06002B4A RID: 11082 RVA: 0x000AB4AE File Offset: 0x000A96AE
		public void AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine) where TAwaiter : ICriticalNotifyCompletion where TStateMachine : IAsyncStateMachine
		{
			this.m_builder.AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(ref awaiter, ref stateMachine);
		}

		/// <summary>Gets the task for this builder.</summary>
		/// <returns>The task for this builder.</returns>
		/// <exception cref="T:System.InvalidOperationException">The builder is not initialized.</exception>
		// Token: 0x17000586 RID: 1414
		// (get) Token: 0x06002B4B RID: 11083 RVA: 0x000AB4BD File Offset: 0x000A96BD
		public Task Task
		{
			get
			{
				return this.m_builder.Task;
			}
		}

		/// <summary>Marks the task as successfully completed.</summary>
		/// <exception cref="T:System.InvalidOperationException">The task has already completed.-or-The builder is not initialized.</exception>
		// Token: 0x06002B4C RID: 11084 RVA: 0x000AB4CA File Offset: 0x000A96CA
		public void SetResult()
		{
			this.m_builder.SetResult(AsyncTaskMethodBuilder.s_cachedCompleted);
		}

		/// <summary>Marks the task as failed and binds the specified exception to the task.</summary>
		/// <param name="exception">The exception to bind to the task.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="exception" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The task has already completed.-or-The builder is not initialized.</exception>
		// Token: 0x06002B4D RID: 11085 RVA: 0x000AB4DC File Offset: 0x000A96DC
		public void SetException(Exception exception)
		{
			this.m_builder.SetException(exception);
		}

		// Token: 0x040015E9 RID: 5609
		private static readonly Task<VoidTaskResult> s_cachedCompleted = AsyncTaskMethodBuilder<VoidTaskResult>.s_defaultResultTask;

		// Token: 0x040015EA RID: 5610
		private AsyncTaskMethodBuilder<VoidTaskResult> m_builder;
	}
}
