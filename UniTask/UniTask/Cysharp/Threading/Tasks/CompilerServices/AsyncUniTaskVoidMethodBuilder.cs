using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Cysharp.Threading.Tasks.CompilerServices
{
	// Token: 0x0200025E RID: 606
	[StructLayout(LayoutKind.Auto)]
	public struct AsyncUniTaskVoidMethodBuilder
	{
		// Token: 0x06000D8B RID: 3467 RVA: 0x0002EE88 File Offset: 0x0002D088
		[DebuggerHidden]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static AsyncUniTaskVoidMethodBuilder Create()
		{
			return default(AsyncUniTaskVoidMethodBuilder);
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000D8C RID: 3468 RVA: 0x0002EEA0 File Offset: 0x0002D0A0
		public UniTaskVoid Task
		{
			[DebuggerHidden]
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return default(UniTaskVoid);
			}
		}

		// Token: 0x06000D8D RID: 3469 RVA: 0x0002EEB6 File Offset: 0x0002D0B6
		[DebuggerHidden]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetException(Exception exception)
		{
			if (this.runner != null)
			{
				this.runner.Return();
				this.runner = null;
			}
			UniTaskScheduler.PublishUnobservedTaskException(exception);
		}

		// Token: 0x06000D8E RID: 3470 RVA: 0x0002EED8 File Offset: 0x0002D0D8
		[DebuggerHidden]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetResult()
		{
			if (this.runner != null)
			{
				this.runner.Return();
				this.runner = null;
			}
		}

		// Token: 0x06000D8F RID: 3471 RVA: 0x0002EEF4 File Offset: 0x0002D0F4
		[DebuggerHidden]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AwaitOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine) where TAwaiter : INotifyCompletion where TStateMachine : IAsyncStateMachine
		{
			if (this.runner == null)
			{
				AsyncUniTaskVoid<TStateMachine>.SetStateMachine(ref stateMachine, ref this.runner);
			}
			awaiter.OnCompleted(this.runner.MoveNext);
		}

		// Token: 0x06000D90 RID: 3472 RVA: 0x0002EF21 File Offset: 0x0002D121
		[DebuggerHidden]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine) where TAwaiter : ICriticalNotifyCompletion where TStateMachine : IAsyncStateMachine
		{
			if (this.runner == null)
			{
				AsyncUniTaskVoid<TStateMachine>.SetStateMachine(ref stateMachine, ref this.runner);
			}
			awaiter.UnsafeOnCompleted(this.runner.MoveNext);
		}

		// Token: 0x06000D91 RID: 3473 RVA: 0x0002ED96 File Offset: 0x0002CF96
		[DebuggerHidden]
		public void Start<TStateMachine>(ref TStateMachine stateMachine) where TStateMachine : IAsyncStateMachine
		{
			stateMachine.MoveNext();
		}

		// Token: 0x06000D92 RID: 3474 RVA: 0x000030EE File Offset: 0x000012EE
		[DebuggerHidden]
		public void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}

		// Token: 0x040006CE RID: 1742
		private IStateMachineRunner runner;
	}
}
