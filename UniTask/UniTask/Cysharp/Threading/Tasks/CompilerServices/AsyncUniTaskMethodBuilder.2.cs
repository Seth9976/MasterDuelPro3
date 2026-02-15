using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Cysharp.Threading.Tasks.CompilerServices
{
	// Token: 0x0200025D RID: 605
	[StructLayout(LayoutKind.Auto)]
	public struct AsyncUniTaskMethodBuilder<T>
	{
		// Token: 0x06000D83 RID: 3459 RVA: 0x0002EDA4 File Offset: 0x0002CFA4
		[DebuggerHidden]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static AsyncUniTaskMethodBuilder<T> Create()
		{
			return default(AsyncUniTaskMethodBuilder<T>);
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000D84 RID: 3460 RVA: 0x0002EDBA File Offset: 0x0002CFBA
		public UniTask<T> Task
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
					return UniTask.FromException<T>(this.ex);
				}
				return UniTask.FromResult<T>(this.result);
			}
		}

		// Token: 0x06000D85 RID: 3461 RVA: 0x0002EDEF File Offset: 0x0002CFEF
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

		// Token: 0x06000D86 RID: 3462 RVA: 0x0002EE0D File Offset: 0x0002D00D
		[DebuggerHidden]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetResult(T result)
		{
			if (this.runnerPromise == null)
			{
				this.result = result;
				return;
			}
			this.runnerPromise.SetResult(result);
		}

		// Token: 0x06000D87 RID: 3463 RVA: 0x0002EE2B File Offset: 0x0002D02B
		[DebuggerHidden]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AwaitOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine) where TAwaiter : INotifyCompletion where TStateMachine : IAsyncStateMachine
		{
			if (this.runnerPromise == null)
			{
				AsyncUniTask<TStateMachine, T>.SetStateMachine(ref stateMachine, ref this.runnerPromise);
			}
			awaiter.OnCompleted(this.runnerPromise.MoveNext);
		}

		// Token: 0x06000D88 RID: 3464 RVA: 0x0002EE58 File Offset: 0x0002D058
		[DebuggerHidden]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine) where TAwaiter : ICriticalNotifyCompletion where TStateMachine : IAsyncStateMachine
		{
			if (this.runnerPromise == null)
			{
				AsyncUniTask<TStateMachine, T>.SetStateMachine(ref stateMachine, ref this.runnerPromise);
			}
			awaiter.UnsafeOnCompleted(this.runnerPromise.MoveNext);
		}

		// Token: 0x06000D89 RID: 3465 RVA: 0x0002ED96 File Offset: 0x0002CF96
		[DebuggerHidden]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Start<TStateMachine>(ref TStateMachine stateMachine) where TStateMachine : IAsyncStateMachine
		{
			stateMachine.MoveNext();
		}

		// Token: 0x06000D8A RID: 3466 RVA: 0x000030EE File Offset: 0x000012EE
		[DebuggerHidden]
		public void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}

		// Token: 0x040006CB RID: 1739
		private IStateMachineRunnerPromise<T> runnerPromise;

		// Token: 0x040006CC RID: 1740
		private Exception ex;

		// Token: 0x040006CD RID: 1741
		private T result;
	}
}
