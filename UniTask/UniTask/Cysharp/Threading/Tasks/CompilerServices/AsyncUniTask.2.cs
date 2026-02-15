using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks.Sources;

namespace Cysharp.Threading.Tasks.CompilerServices
{
	// Token: 0x02000268 RID: 616
	internal sealed class AsyncUniTask<TStateMachine, T> : IStateMachineRunnerPromise<T>, IUniTaskSource<T>, IUniTaskSource, IValueTaskSource, IValueTaskSource<T>, ITaskPoolNode<AsyncUniTask<TStateMachine, T>> where TStateMachine : IAsyncStateMachine
	{
		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000DC1 RID: 3521 RVA: 0x0002F1EF File Offset: 0x0002D3EF
		public Action MoveNext { get; }

		// Token: 0x06000DC2 RID: 3522 RVA: 0x0002F1F7 File Offset: 0x0002D3F7
		private AsyncUniTask()
		{
			this.MoveNext = new Action(this.Run);
		}

		// Token: 0x06000DC3 RID: 3523 RVA: 0x0002F214 File Offset: 0x0002D414
		public static void SetStateMachine(ref TStateMachine stateMachine, ref IStateMachineRunnerPromise<T> runnerPromiseFieldRef)
		{
			AsyncUniTask<TStateMachine, T> result;
			if (!AsyncUniTask<TStateMachine, T>.pool.TryPop(out result))
			{
				result = new AsyncUniTask<TStateMachine, T>();
			}
			runnerPromiseFieldRef = result;
			result.stateMachine = stateMachine;
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000DC4 RID: 3524 RVA: 0x0002F244 File Offset: 0x0002D444
		public ref AsyncUniTask<TStateMachine, T> NextNode
		{
			get
			{
				return ref this.nextNode;
			}
		}

		// Token: 0x06000DC5 RID: 3525 RVA: 0x0002F24C File Offset: 0x0002D44C
		static AsyncUniTask()
		{
			TaskPool.RegisterSizeGetter(typeof(AsyncUniTask<TStateMachine, T>), () => AsyncUniTask<TStateMachine, T>.pool.Size);
		}

		// Token: 0x06000DC6 RID: 3526 RVA: 0x0002F26D File Offset: 0x0002D46D
		private void Return()
		{
			this.core.Reset();
			this.stateMachine = default(TStateMachine);
			AsyncUniTask<TStateMachine, T>.pool.TryPush(this);
		}

		// Token: 0x06000DC7 RID: 3527 RVA: 0x0002F292 File Offset: 0x0002D492
		private bool TryReturn()
		{
			this.core.Reset();
			this.stateMachine = default(TStateMachine);
			return AsyncUniTask<TStateMachine, T>.pool.TryPush(this);
		}

		// Token: 0x06000DC8 RID: 3528 RVA: 0x0002F2B6 File Offset: 0x0002D4B6
		[DebuggerHidden]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void Run()
		{
			this.stateMachine.MoveNext();
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000DC9 RID: 3529 RVA: 0x0002F2C9 File Offset: 0x0002D4C9
		public UniTask<T> Task
		{
			[DebuggerHidden]
			get
			{
				return new UniTask<T>(this, this.core.Version);
			}
		}

		// Token: 0x06000DCA RID: 3530 RVA: 0x0002F2DC File Offset: 0x0002D4DC
		[DebuggerHidden]
		public void SetResult(T result)
		{
			this.core.TrySetResult(result);
		}

		// Token: 0x06000DCB RID: 3531 RVA: 0x0002F2EB File Offset: 0x0002D4EB
		[DebuggerHidden]
		public void SetException(Exception exception)
		{
			this.core.TrySetException(exception);
		}

		// Token: 0x06000DCC RID: 3532 RVA: 0x0002F2FC File Offset: 0x0002D4FC
		[DebuggerHidden]
		public T GetResult(short token)
		{
			T result;
			try
			{
				result = this.core.GetResult(token);
			}
			finally
			{
				this.TryReturn();
			}
			return result;
		}

		// Token: 0x06000DCD RID: 3533 RVA: 0x0002F334 File Offset: 0x0002D534
		[DebuggerHidden]
		void IUniTaskSource.GetResult(short token)
		{
			this.GetResult(token);
		}

		// Token: 0x06000DCE RID: 3534 RVA: 0x0002F33E File Offset: 0x0002D53E
		[DebuggerHidden]
		public UniTaskStatus GetStatus(short token)
		{
			return this.core.GetStatus(token);
		}

		// Token: 0x06000DCF RID: 3535 RVA: 0x0002F34C File Offset: 0x0002D54C
		[DebuggerHidden]
		public UniTaskStatus UnsafeGetStatus()
		{
			return this.core.UnsafeGetStatus();
		}

		// Token: 0x06000DD0 RID: 3536 RVA: 0x0002F359 File Offset: 0x0002D559
		[DebuggerHidden]
		public void OnCompleted(Action<object> continuation, object state, short token)
		{
			this.core.OnCompleted(continuation, state, token);
		}

		// Token: 0x040006DC RID: 1756
		private static TaskPool<AsyncUniTask<TStateMachine, T>> pool;

		// Token: 0x040006DE RID: 1758
		private TStateMachine stateMachine;

		// Token: 0x040006DF RID: 1759
		private UniTaskCompletionSourceCore<T> core;

		// Token: 0x040006E0 RID: 1760
		private AsyncUniTask<TStateMachine, T> nextNode;
	}
}
