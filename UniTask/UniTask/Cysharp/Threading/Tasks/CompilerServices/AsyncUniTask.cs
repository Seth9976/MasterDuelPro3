using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks.Sources;

namespace Cysharp.Threading.Tasks.CompilerServices
{
	// Token: 0x02000266 RID: 614
	internal sealed class AsyncUniTask<TStateMachine> : IStateMachineRunnerPromise, IUniTaskSource, IValueTaskSource, ITaskPoolNode<AsyncUniTask<TStateMachine>> where TStateMachine : IAsyncStateMachine
	{
		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000DAF RID: 3503 RVA: 0x0002F06A File Offset: 0x0002D26A
		public Action MoveNext { get; }

		// Token: 0x06000DB0 RID: 3504 RVA: 0x0002F072 File Offset: 0x0002D272
		private AsyncUniTask()
		{
			this.MoveNext = new Action(this.Run);
		}

		// Token: 0x06000DB1 RID: 3505 RVA: 0x0002F08C File Offset: 0x0002D28C
		public static void SetStateMachine(ref TStateMachine stateMachine, ref IStateMachineRunnerPromise runnerPromiseFieldRef)
		{
			AsyncUniTask<TStateMachine> result;
			if (!AsyncUniTask<TStateMachine>.pool.TryPop(out result))
			{
				result = new AsyncUniTask<TStateMachine>();
			}
			runnerPromiseFieldRef = result;
			result.stateMachine = stateMachine;
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000DB2 RID: 3506 RVA: 0x0002F0BC File Offset: 0x0002D2BC
		public ref AsyncUniTask<TStateMachine> NextNode
		{
			get
			{
				return ref this.nextNode;
			}
		}

		// Token: 0x06000DB3 RID: 3507 RVA: 0x0002F0C4 File Offset: 0x0002D2C4
		static AsyncUniTask()
		{
			TaskPool.RegisterSizeGetter(typeof(AsyncUniTask<TStateMachine>), () => AsyncUniTask<TStateMachine>.pool.Size);
		}

		// Token: 0x06000DB4 RID: 3508 RVA: 0x0002F0E5 File Offset: 0x0002D2E5
		private void Return()
		{
			this.core.Reset();
			this.stateMachine = default(TStateMachine);
			AsyncUniTask<TStateMachine>.pool.TryPush(this);
		}

		// Token: 0x06000DB5 RID: 3509 RVA: 0x0002F10A File Offset: 0x0002D30A
		private bool TryReturn()
		{
			this.core.Reset();
			this.stateMachine = default(TStateMachine);
			return AsyncUniTask<TStateMachine>.pool.TryPush(this);
		}

		// Token: 0x06000DB6 RID: 3510 RVA: 0x0002F12E File Offset: 0x0002D32E
		[DebuggerHidden]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void Run()
		{
			this.stateMachine.MoveNext();
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000DB7 RID: 3511 RVA: 0x0002F141 File Offset: 0x0002D341
		public UniTask Task
		{
			[DebuggerHidden]
			get
			{
				return new UniTask(this, this.core.Version);
			}
		}

		// Token: 0x06000DB8 RID: 3512 RVA: 0x0002F154 File Offset: 0x0002D354
		[DebuggerHidden]
		public void SetResult()
		{
			this.core.TrySetResult(AsyncUnit.Default);
		}

		// Token: 0x06000DB9 RID: 3513 RVA: 0x0002F167 File Offset: 0x0002D367
		[DebuggerHidden]
		public void SetException(Exception exception)
		{
			this.core.TrySetException(exception);
		}

		// Token: 0x06000DBA RID: 3514 RVA: 0x0002F178 File Offset: 0x0002D378
		[DebuggerHidden]
		public void GetResult(short token)
		{
			try
			{
				this.core.GetResult(token);
			}
			finally
			{
				this.TryReturn();
			}
		}

		// Token: 0x06000DBB RID: 3515 RVA: 0x0002F1AC File Offset: 0x0002D3AC
		[DebuggerHidden]
		public UniTaskStatus GetStatus(short token)
		{
			return this.core.GetStatus(token);
		}

		// Token: 0x06000DBC RID: 3516 RVA: 0x0002F1BA File Offset: 0x0002D3BA
		[DebuggerHidden]
		public UniTaskStatus UnsafeGetStatus()
		{
			return this.core.UnsafeGetStatus();
		}

		// Token: 0x06000DBD RID: 3517 RVA: 0x0002F1C7 File Offset: 0x0002D3C7
		[DebuggerHidden]
		public void OnCompleted(Action<object> continuation, object state, short token)
		{
			this.core.OnCompleted(continuation, state, token);
		}

		// Token: 0x040006D6 RID: 1750
		private static TaskPool<AsyncUniTask<TStateMachine>> pool;

		// Token: 0x040006D8 RID: 1752
		private TStateMachine stateMachine;

		// Token: 0x040006D9 RID: 1753
		private UniTaskCompletionSourceCore<AsyncUnit> core;

		// Token: 0x040006DA RID: 1754
		private AsyncUniTask<TStateMachine> nextNode;
	}
}
