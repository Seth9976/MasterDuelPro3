using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks.Sources;

namespace Cysharp.Threading.Tasks.CompilerServices
{
	// Token: 0x02000264 RID: 612
	internal sealed class AsyncUniTaskVoid<TStateMachine> : IStateMachineRunner, ITaskPoolNode<AsyncUniTaskVoid<TStateMachine>>, IUniTaskSource, IValueTaskSource where TStateMachine : IAsyncStateMachine
	{
		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000DA1 RID: 3489 RVA: 0x0002EFAA File Offset: 0x0002D1AA
		public Action MoveNext { get; }

		// Token: 0x06000DA2 RID: 3490 RVA: 0x0002EFB2 File Offset: 0x0002D1B2
		public AsyncUniTaskVoid()
		{
			this.MoveNext = new Action(this.Run);
		}

		// Token: 0x06000DA3 RID: 3491 RVA: 0x0002EFCC File Offset: 0x0002D1CC
		public static void SetStateMachine(ref TStateMachine stateMachine, ref IStateMachineRunner runnerFieldRef)
		{
			AsyncUniTaskVoid<TStateMachine> result;
			if (!AsyncUniTaskVoid<TStateMachine>.pool.TryPop(out result))
			{
				result = new AsyncUniTaskVoid<TStateMachine>();
			}
			runnerFieldRef = result;
			result.stateMachine = stateMachine;
		}

		// Token: 0x06000DA4 RID: 3492 RVA: 0x0002EFFC File Offset: 0x0002D1FC
		static AsyncUniTaskVoid()
		{
			TaskPool.RegisterSizeGetter(typeof(AsyncUniTaskVoid<TStateMachine>), () => AsyncUniTaskVoid<TStateMachine>.pool.Size);
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000DA5 RID: 3493 RVA: 0x0002F01D File Offset: 0x0002D21D
		public ref AsyncUniTaskVoid<TStateMachine> NextNode
		{
			get
			{
				return ref this.nextNode;
			}
		}

		// Token: 0x06000DA6 RID: 3494 RVA: 0x0002F025 File Offset: 0x0002D225
		public void Return()
		{
			this.stateMachine = default(TStateMachine);
			AsyncUniTaskVoid<TStateMachine>.pool.TryPush(this);
		}

		// Token: 0x06000DA7 RID: 3495 RVA: 0x0002F03F File Offset: 0x0002D23F
		[DebuggerHidden]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void Run()
		{
			this.stateMachine.MoveNext();
		}

		// Token: 0x06000DA8 RID: 3496 RVA: 0x000030E1 File Offset: 0x000012E1
		UniTaskStatus IUniTaskSource.GetStatus(short token)
		{
			return UniTaskStatus.Pending;
		}

		// Token: 0x06000DA9 RID: 3497 RVA: 0x000030E1 File Offset: 0x000012E1
		UniTaskStatus IUniTaskSource.UnsafeGetStatus()
		{
			return UniTaskStatus.Pending;
		}

		// Token: 0x06000DAA RID: 3498 RVA: 0x000030EE File Offset: 0x000012EE
		void IUniTaskSource.OnCompleted(Action<object> continuation, object state, short token)
		{
		}

		// Token: 0x06000DAB RID: 3499 RVA: 0x000030EE File Offset: 0x000012EE
		void IUniTaskSource.GetResult(short token)
		{
		}

		// Token: 0x040006D1 RID: 1745
		private static TaskPool<AsyncUniTaskVoid<TStateMachine>> pool;

		// Token: 0x040006D2 RID: 1746
		private TStateMachine stateMachine;

		// Token: 0x040006D4 RID: 1748
		private AsyncUniTaskVoid<TStateMachine> nextNode;
	}
}
