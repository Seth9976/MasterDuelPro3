using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace System.Runtime.CompilerServices
{
	/// <summary>Represents a builder for asynchronous methods that do not return a value.</summary>
	// Token: 0x020005A6 RID: 1446
	public struct AsyncVoidMethodBuilder
	{
		/// <summary>Creates an instance of the <see cref="T:System.Runtime.CompilerServices.AsyncVoidMethodBuilder" /> class.</summary>
		/// <returns>A new instance of the builder.</returns>
		// Token: 0x06002B3F RID: 11071 RVA: 0x000AB1F4 File Offset: 0x000A93F4
		public static AsyncVoidMethodBuilder Create()
		{
			SynchronizationContext currentNoFlow = SynchronizationContext.CurrentNoFlow;
			if (currentNoFlow != null)
			{
				currentNoFlow.OperationStarted();
			}
			return new AsyncVoidMethodBuilder
			{
				m_synchronizationContext = currentNoFlow
			};
		}

		/// <summary>Begins running the builder with the associated state machine.</summary>
		/// <param name="stateMachine">The state machine instance, passed by reference.</param>
		/// <typeparam name="TStateMachine">The type of the state machine.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stateMachine" /> is null.</exception>
		// Token: 0x06002B40 RID: 11072 RVA: 0x000AB224 File Offset: 0x000A9424
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
		// Token: 0x06002B41 RID: 11073 RVA: 0x000AB284 File Offset: 0x000A9484
		public void SetStateMachine(IAsyncStateMachine stateMachine)
		{
			this.m_coreState.SetStateMachine(stateMachine);
		}

		/// <summary>Schedules the state machine to proceed to the next action when the specified awaiter completes. This method can be called from partially trusted code.</summary>
		/// <param name="awaiter">The awaiter.</param>
		/// <param name="stateMachine">The state machine.</param>
		/// <typeparam name="TAwaiter">The type of the awaiter.</typeparam>
		/// <typeparam name="TStateMachine">The type of the state machine.</typeparam>
		// Token: 0x06002B42 RID: 11074 RVA: 0x000AB294 File Offset: 0x000A9494
		public void AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine) where TAwaiter : ICriticalNotifyCompletion where TStateMachine : IAsyncStateMachine
		{
			try
			{
				AsyncMethodBuilderCore.MoveNextRunner moveNextRunner = null;
				Action completionAction = this.m_coreState.GetCompletionAction(AsyncCausalityTracer.LoggingOn ? this.Task : null, ref moveNextRunner);
				if (this.m_coreState.m_stateMachine == null)
				{
					if (AsyncCausalityTracer.LoggingOn)
					{
						AsyncCausalityTracer.TraceOperationCreation(CausalityTraceLevel.Required, this.Task.Id, "Async: " + stateMachine.GetType().Name, 0UL);
					}
					this.m_coreState.PostBoxInitialization(stateMachine, moveNextRunner, null);
				}
				awaiter.UnsafeOnCompleted(completionAction);
			}
			catch (Exception ex)
			{
				AsyncMethodBuilderCore.ThrowAsync(ex, null);
			}
		}

		/// <summary>Marks the method builder as successfully completed.</summary>
		/// <exception cref="T:System.InvalidOperationException">The builder is not initialized.</exception>
		// Token: 0x06002B43 RID: 11075 RVA: 0x000AB344 File Offset: 0x000A9544
		public void SetResult()
		{
			if (AsyncCausalityTracer.LoggingOn)
			{
				AsyncCausalityTracer.TraceOperationCompletion(CausalityTraceLevel.Required, this.Task.Id, AsyncCausalityStatus.Completed);
			}
			if (this.m_synchronizationContext != null)
			{
				this.NotifySynchronizationContextOfCompletion();
			}
		}

		/// <summary>Binds an exception to the method builder.</summary>
		/// <param name="exception">The exception to bind.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="exception" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The builder is not initialized.</exception>
		// Token: 0x06002B44 RID: 11076 RVA: 0x000AB370 File Offset: 0x000A9570
		public void SetException(Exception exception)
		{
			if (exception == null)
			{
				throw new ArgumentNullException("exception");
			}
			if (AsyncCausalityTracer.LoggingOn)
			{
				AsyncCausalityTracer.TraceOperationCompletion(CausalityTraceLevel.Required, this.Task.Id, AsyncCausalityStatus.Error);
			}
			if (this.m_synchronizationContext != null)
			{
				try
				{
					AsyncMethodBuilderCore.ThrowAsync(exception, this.m_synchronizationContext);
					return;
				}
				finally
				{
					this.NotifySynchronizationContextOfCompletion();
				}
			}
			AsyncMethodBuilderCore.ThrowAsync(exception, null);
		}

		// Token: 0x06002B45 RID: 11077 RVA: 0x000AB3D8 File Offset: 0x000A95D8
		private void NotifySynchronizationContextOfCompletion()
		{
			try
			{
				this.m_synchronizationContext.OperationCompleted();
			}
			catch (Exception ex)
			{
				AsyncMethodBuilderCore.ThrowAsync(ex, null);
			}
		}

		// Token: 0x17000585 RID: 1413
		// (get) Token: 0x06002B46 RID: 11078 RVA: 0x000AB40C File Offset: 0x000A960C
		internal Task Task
		{
			get
			{
				if (this.m_task == null)
				{
					this.m_task = new Task();
				}
				return this.m_task;
			}
		}

		// Token: 0x040015E6 RID: 5606
		private SynchronizationContext m_synchronizationContext;

		// Token: 0x040015E7 RID: 5607
		private AsyncMethodBuilderCore m_coreState;

		// Token: 0x040015E8 RID: 5608
		private Task m_task;
	}
}
