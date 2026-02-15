using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Cysharp.Threading.Tasks.CompilerServices
{
	// Token: 0x0200025C RID: 604
	[StructLayout(LayoutKind.Auto)]
	public struct AsyncUniTaskMethodBuilder
	{
		// Token: 0x06000D7B RID: 3451 RVA: 0x0002ECC4 File Offset: 0x0002CEC4
		[DebuggerHidden]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static AsyncUniTaskMethodBuilder Create()
		{
			return default(AsyncUniTaskMethodBuilder);
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000D7C RID: 3452 RVA: 0x0002ECDA File Offset: 0x0002CEDA
		public UniTask Task
		{
			[DebuggerHidden]
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				if (this.runnerPromise != null)
				{
					return this.runnerPromise.Task;
				}
				if (this.ex != null)
				{
					return UniTask.FromException(this.ex);
				}
				return UniTask.CompletedTask;
			}
		}

		// Token: 0x06000D7D RID: 3453 RVA: 0x0002ED09 File Offset: 0x0002CF09
		[DebuggerHidden]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetException(Exception exception)
		{
			if (this.runnerPromise == null)
			{
				this.ex = exception;
				return;
			}
			this.runnerPromise.SetException(exception);
		}

		// Token: 0x06000D7E RID: 3454 RVA: 0x0002ED27 File Offset: 0x0002CF27
		[DebuggerHidden]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetResult()
		{
			if (this.runnerPromise != null)
			{
				this.runnerPromise.SetResult();
			}
		}

		// Token: 0x06000D7F RID: 3455 RVA: 0x0002ED3C File Offset: 0x0002CF3C
		[DebuggerHidden]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AwaitOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine) where TAwaiter : INotifyCompletion where TStateMachine : IAsyncStateMachine
		{
			if (this.runnerPromise == null)
			{
				AsyncUniTask<TStateMachine>.SetStateMachine(ref stateMachine, ref this.runnerPromise);
			}
			awaiter.OnCompleted(this.runnerPromise.MoveNext);
		}

		// Token: 0x06000D80 RID: 3456 RVA: 0x0002ED69 File Offset: 0x0002CF69
		[DebuggerHidden]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine) where TAwaiter : ICriticalNotifyCompletion where TStateMachine : IAsyncStateMachine
		{
			if (this.runnerPromise == null)
			{
				AsyncUniTask<TStateMachine>.SetStateMachine(ref stateMachine, ref this.runnerPromise);
			}
			awaiter.UnsafeOnCompleted(this.runnerPromise.MoveNext);
		}

		// Token: 0x06000D81 RID: 3457 RVA: 0x0002ED96 File Offset: 0x0002CF96
		[DebuggerHidden]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Start<TStateMachine>(ref TStateMachine stateMachine) where TStateMachine : IAsyncStateMachine
		{
			stateMachine.MoveNext();
		}

		// Token: 0x06000D82 RID: 3458 RVA: 0x000030EE File Offset: 0x000012EE
		[DebuggerHidden]
		public void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}

		// Token: 0x040006C9 RID: 1737
		private IStateMachineRunnerPromise runnerPromise;

		// Token: 0x040006CA RID: 1738
		private Exception ex;
	}
}
