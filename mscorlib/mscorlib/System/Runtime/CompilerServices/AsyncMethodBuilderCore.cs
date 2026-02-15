using System;
using System.Diagnostics;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020005AA RID: 1450
	internal struct AsyncMethodBuilderCore
	{
		// Token: 0x06002B5C RID: 11100 RVA: 0x000ABA18 File Offset: 0x000A9C18
		public void SetStateMachine(IAsyncStateMachine stateMachine)
		{
			if (stateMachine == null)
			{
				throw new ArgumentNullException("stateMachine");
			}
			if (this.m_stateMachine != null)
			{
				throw new InvalidOperationException(Environment.GetResourceString("The builder was not properly initialized."));
			}
			this.m_stateMachine = stateMachine;
		}

		// Token: 0x06002B5D RID: 11101 RVA: 0x000ABA48 File Offset: 0x000A9C48
		internal Action GetCompletionAction(Task taskForTracing, ref AsyncMethodBuilderCore.MoveNextRunner runnerToInitialize)
		{
			Debugger.NotifyOfCrossThreadDependency();
			ExecutionContext executionContext = ExecutionContext.FastCapture();
			Action action;
			AsyncMethodBuilderCore.MoveNextRunner moveNextRunner;
			if (executionContext != null && executionContext.IsPreAllocatedDefault)
			{
				action = this.m_defaultContextAction;
				if (action != null)
				{
					return action;
				}
				moveNextRunner = new AsyncMethodBuilderCore.MoveNextRunner(executionContext, this.m_stateMachine);
				action = new Action(moveNextRunner.Run);
				if (taskForTracing != null)
				{
					action = (this.m_defaultContextAction = this.OutputAsyncCausalityEvents(taskForTracing, action));
				}
				else
				{
					this.m_defaultContextAction = action;
				}
			}
			else
			{
				moveNextRunner = new AsyncMethodBuilderCore.MoveNextRunner(executionContext, this.m_stateMachine);
				action = new Action(moveNextRunner.Run);
				if (taskForTracing != null)
				{
					action = this.OutputAsyncCausalityEvents(taskForTracing, action);
				}
			}
			if (this.m_stateMachine == null)
			{
				runnerToInitialize = moveNextRunner;
			}
			return action;
		}

		// Token: 0x06002B5E RID: 11102 RVA: 0x000ABAE4 File Offset: 0x000A9CE4
		private Action OutputAsyncCausalityEvents(Task innerTask, Action continuation)
		{
			return AsyncMethodBuilderCore.CreateContinuationWrapper(continuation, delegate
			{
				AsyncCausalityTracer.TraceSynchronousWorkStart(CausalityTraceLevel.Required, innerTask.Id, CausalitySynchronousWork.Execution);
				continuation();
				AsyncCausalityTracer.TraceSynchronousWorkCompletion(CausalityTraceLevel.Required, CausalitySynchronousWork.Execution);
			}, innerTask);
		}

		// Token: 0x06002B5F RID: 11103 RVA: 0x000ABB24 File Offset: 0x000A9D24
		internal void PostBoxInitialization(IAsyncStateMachine stateMachine, AsyncMethodBuilderCore.MoveNextRunner runner, Task builtTask)
		{
			if (builtTask != null)
			{
				if (AsyncCausalityTracer.LoggingOn)
				{
					AsyncCausalityTracer.TraceOperationCreation(CausalityTraceLevel.Required, builtTask.Id, "Async: " + stateMachine.GetType().Name, 0UL);
				}
				if (Task.s_asyncDebuggingEnabled)
				{
					Task.AddToActiveTasks(builtTask);
				}
			}
			this.m_stateMachine = stateMachine;
			this.m_stateMachine.SetStateMachine(this.m_stateMachine);
			runner.m_stateMachine = this.m_stateMachine;
		}

		// Token: 0x06002B60 RID: 11104 RVA: 0x000ABB90 File Offset: 0x000A9D90
		internal static void ThrowAsync(Exception exception, SynchronizationContext targetContext)
		{
			ExceptionDispatchInfo exceptionDispatchInfo = ExceptionDispatchInfo.Capture(exception);
			if (targetContext != null)
			{
				try
				{
					targetContext.Post(delegate(object state)
					{
						((ExceptionDispatchInfo)state).Throw();
					}, exceptionDispatchInfo);
					return;
				}
				catch (Exception ex)
				{
					exceptionDispatchInfo = ExceptionDispatchInfo.Capture(new AggregateException(new Exception[] { exception, ex }));
				}
			}
			if (!WindowsRuntimeMarshal.ReportUnhandledError(exceptionDispatchInfo.SourceException))
			{
				ThreadPool.QueueUserWorkItem(delegate(object state)
				{
					((ExceptionDispatchInfo)state).Throw();
				}, exceptionDispatchInfo);
			}
		}

		// Token: 0x06002B61 RID: 11105 RVA: 0x000ABC30 File Offset: 0x000A9E30
		internal static Action CreateContinuationWrapper(Action continuation, Action invokeAction, Task innerTask = null)
		{
			return new Action(new AsyncMethodBuilderCore.ContinuationWrapper(continuation, invokeAction, innerTask).Invoke);
		}

		// Token: 0x06002B62 RID: 11106 RVA: 0x000ABC48 File Offset: 0x000A9E48
		internal static Task TryGetContinuationTask(Action action)
		{
			if (action != null)
			{
				AsyncMethodBuilderCore.ContinuationWrapper continuationWrapper = action.Target as AsyncMethodBuilderCore.ContinuationWrapper;
				if (continuationWrapper != null)
				{
					return continuationWrapper.m_innerTask;
				}
			}
			return null;
		}

		// Token: 0x040015F1 RID: 5617
		internal IAsyncStateMachine m_stateMachine;

		// Token: 0x040015F2 RID: 5618
		internal Action m_defaultContextAction;

		// Token: 0x020005AB RID: 1451
		internal sealed class MoveNextRunner
		{
			// Token: 0x06002B63 RID: 11107 RVA: 0x000ABC6F File Offset: 0x000A9E6F
			internal MoveNextRunner(ExecutionContext context, IAsyncStateMachine stateMachine)
			{
				this.m_context = context;
				this.m_stateMachine = stateMachine;
			}

			// Token: 0x06002B64 RID: 11108 RVA: 0x000ABC88 File Offset: 0x000A9E88
			internal void Run()
			{
				if (this.m_context != null)
				{
					try
					{
						ContextCallback contextCallback = AsyncMethodBuilderCore.MoveNextRunner.s_invokeMoveNext;
						if (contextCallback == null)
						{
							contextCallback = (AsyncMethodBuilderCore.MoveNextRunner.s_invokeMoveNext = new ContextCallback(AsyncMethodBuilderCore.MoveNextRunner.InvokeMoveNext));
						}
						ExecutionContext.Run(this.m_context, contextCallback, this.m_stateMachine, true);
						return;
					}
					finally
					{
						this.m_context.Dispose();
					}
				}
				this.m_stateMachine.MoveNext();
			}

			// Token: 0x06002B65 RID: 11109 RVA: 0x000ABCF8 File Offset: 0x000A9EF8
			private static void InvokeMoveNext(object stateMachine)
			{
				((IAsyncStateMachine)stateMachine).MoveNext();
			}

			// Token: 0x040015F3 RID: 5619
			private readonly ExecutionContext m_context;

			// Token: 0x040015F4 RID: 5620
			internal IAsyncStateMachine m_stateMachine;

			// Token: 0x040015F5 RID: 5621
			private static ContextCallback s_invokeMoveNext;
		}

		// Token: 0x020005AC RID: 1452
		private class ContinuationWrapper
		{
			// Token: 0x06002B66 RID: 11110 RVA: 0x000ABD05 File Offset: 0x000A9F05
			internal ContinuationWrapper(Action continuation, Action invokeAction, Task innerTask)
			{
				if (innerTask == null)
				{
					innerTask = AsyncMethodBuilderCore.TryGetContinuationTask(continuation);
				}
				this.m_continuation = continuation;
				this.m_innerTask = innerTask;
				this.m_invokeAction = invokeAction;
			}

			// Token: 0x06002B67 RID: 11111 RVA: 0x000ABD2D File Offset: 0x000A9F2D
			internal void Invoke()
			{
				this.m_invokeAction();
			}

			// Token: 0x040015F6 RID: 5622
			internal readonly Action m_continuation;

			// Token: 0x040015F7 RID: 5623
			private readonly Action m_invokeAction;

			// Token: 0x040015F8 RID: 5624
			internal readonly Task m_innerTask;
		}
	}
}
