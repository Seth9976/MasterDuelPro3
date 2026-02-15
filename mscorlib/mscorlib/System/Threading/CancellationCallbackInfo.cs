using System;

namespace System.Threading
{
	// Token: 0x0200023B RID: 571
	internal class CancellationCallbackInfo
	{
		// Token: 0x06001524 RID: 5412 RVA: 0x000551CC File Offset: 0x000533CC
		internal CancellationCallbackInfo(Action<object> callback, object stateForCallback, ExecutionContext targetExecutionContext, CancellationTokenSource cancellationTokenSource)
		{
			this.Callback = callback;
			this.StateForCallback = stateForCallback;
			this.TargetExecutionContext = targetExecutionContext;
			this.CancellationTokenSource = cancellationTokenSource;
		}

		// Token: 0x06001525 RID: 5413 RVA: 0x000551F4 File Offset: 0x000533F4
		internal void ExecuteCallback()
		{
			if (this.TargetExecutionContext != null)
			{
				ContextCallback contextCallback = CancellationCallbackInfo.s_executionContextCallback;
				if (contextCallback == null)
				{
					contextCallback = (CancellationCallbackInfo.s_executionContextCallback = new ContextCallback(CancellationCallbackInfo.ExecutionContextCallback));
				}
				ExecutionContext.Run(this.TargetExecutionContext, contextCallback, this);
				return;
			}
			CancellationCallbackInfo.ExecutionContextCallback(this);
		}

		// Token: 0x06001526 RID: 5414 RVA: 0x0005523C File Offset: 0x0005343C
		private static void ExecutionContextCallback(object obj)
		{
			CancellationCallbackInfo cancellationCallbackInfo = obj as CancellationCallbackInfo;
			cancellationCallbackInfo.Callback(cancellationCallbackInfo.StateForCallback);
		}

		// Token: 0x04000A5A RID: 2650
		internal readonly Action<object> Callback;

		// Token: 0x04000A5B RID: 2651
		internal readonly object StateForCallback;

		// Token: 0x04000A5C RID: 2652
		internal readonly ExecutionContext TargetExecutionContext;

		// Token: 0x04000A5D RID: 2653
		internal readonly CancellationTokenSource CancellationTokenSource;

		// Token: 0x04000A5E RID: 2654
		private static ContextCallback s_executionContextCallback;

		// Token: 0x0200023C RID: 572
		internal sealed class WithSyncContext : CancellationCallbackInfo
		{
			// Token: 0x06001527 RID: 5415 RVA: 0x00055261 File Offset: 0x00053461
			internal WithSyncContext(Action<object> callback, object stateForCallback, ExecutionContext targetExecutionContext, CancellationTokenSource cancellationTokenSource, SynchronizationContext targetSyncContext)
				: base(callback, stateForCallback, targetExecutionContext, cancellationTokenSource)
			{
				this.TargetSyncContext = targetSyncContext;
			}

			// Token: 0x04000A5F RID: 2655
			internal readonly SynchronizationContext TargetSyncContext;
		}
	}
}
