using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace System.Runtime.CompilerServices
{
	// Token: 0x02000579 RID: 1401
	[StructLayout(LayoutKind.Auto)]
	public struct AsyncValueTaskMethodBuilder
	{
		// Token: 0x06002ACA RID: 10954 RVA: 0x000AA6D4 File Offset: 0x000A88D4
		public static AsyncValueTaskMethodBuilder Create()
		{
			return default(AsyncValueTaskMethodBuilder);
		}

		// Token: 0x06002ACB RID: 10955 RVA: 0x000AA6EA File Offset: 0x000A88EA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Start<TStateMachine>(ref TStateMachine stateMachine) where TStateMachine : IAsyncStateMachine
		{
			this._methodBuilder.Start<TStateMachine>(ref stateMachine);
		}

		// Token: 0x06002ACC RID: 10956 RVA: 0x000AA6F8 File Offset: 0x000A88F8
		public void SetStateMachine(IAsyncStateMachine stateMachine)
		{
			this._methodBuilder.SetStateMachine(stateMachine);
		}

		// Token: 0x06002ACD RID: 10957 RVA: 0x000AA706 File Offset: 0x000A8906
		public void SetResult()
		{
			if (this._useBuilder)
			{
				this._methodBuilder.SetResult();
				return;
			}
			this._haveResult = true;
		}

		// Token: 0x06002ACE RID: 10958 RVA: 0x000AA723 File Offset: 0x000A8923
		public void SetException(Exception exception)
		{
			this._methodBuilder.SetException(exception);
		}

		// Token: 0x1700056F RID: 1391
		// (get) Token: 0x06002ACF RID: 10959 RVA: 0x000AA734 File Offset: 0x000A8934
		public ValueTask Task
		{
			get
			{
				if (this._haveResult)
				{
					return default(ValueTask);
				}
				this._useBuilder = true;
				return new ValueTask(this._methodBuilder.Task);
			}
		}

		// Token: 0x06002AD0 RID: 10960 RVA: 0x000AA76A File Offset: 0x000A896A
		public void AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine) where TAwaiter : ICriticalNotifyCompletion where TStateMachine : IAsyncStateMachine
		{
			this._useBuilder = true;
			this._methodBuilder.AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(ref awaiter, ref stateMachine);
		}

		// Token: 0x040015C2 RID: 5570
		private AsyncTaskMethodBuilder _methodBuilder;

		// Token: 0x040015C3 RID: 5571
		private bool _haveResult;

		// Token: 0x040015C4 RID: 5572
		private bool _useBuilder;
	}
}
