using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace System.Runtime.CompilerServices
{
	// Token: 0x0200057A RID: 1402
	[StructLayout(LayoutKind.Auto)]
	public struct AsyncValueTaskMethodBuilder<TResult>
	{
		// Token: 0x06002AD1 RID: 10961 RVA: 0x000AA780 File Offset: 0x000A8980
		public static AsyncValueTaskMethodBuilder<TResult> Create()
		{
			return default(AsyncValueTaskMethodBuilder<TResult>);
		}

		// Token: 0x06002AD2 RID: 10962 RVA: 0x000AA796 File Offset: 0x000A8996
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Start<TStateMachine>(ref TStateMachine stateMachine) where TStateMachine : IAsyncStateMachine
		{
			this._methodBuilder.Start<TStateMachine>(ref stateMachine);
		}

		// Token: 0x06002AD3 RID: 10963 RVA: 0x000AA7A4 File Offset: 0x000A89A4
		public void SetStateMachine(IAsyncStateMachine stateMachine)
		{
			this._methodBuilder.SetStateMachine(stateMachine);
		}

		// Token: 0x06002AD4 RID: 10964 RVA: 0x000AA7B2 File Offset: 0x000A89B2
		public void SetResult(TResult result)
		{
			if (this._useBuilder)
			{
				this._methodBuilder.SetResult(result);
				return;
			}
			this._result = result;
			this._haveResult = true;
		}

		// Token: 0x06002AD5 RID: 10965 RVA: 0x000AA7D7 File Offset: 0x000A89D7
		public void SetException(Exception exception)
		{
			this._methodBuilder.SetException(exception);
		}

		// Token: 0x17000570 RID: 1392
		// (get) Token: 0x06002AD6 RID: 10966 RVA: 0x000AA7E5 File Offset: 0x000A89E5
		public ValueTask<TResult> Task
		{
			get
			{
				if (this._haveResult)
				{
					return new ValueTask<TResult>(this._result);
				}
				this._useBuilder = true;
				return new ValueTask<TResult>(this._methodBuilder.Task);
			}
		}

		// Token: 0x06002AD7 RID: 10967 RVA: 0x000AA812 File Offset: 0x000A8A12
		public void AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine) where TAwaiter : ICriticalNotifyCompletion where TStateMachine : IAsyncStateMachine
		{
			this._useBuilder = true;
			this._methodBuilder.AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(ref awaiter, ref stateMachine);
		}

		// Token: 0x040015C5 RID: 5573
		private AsyncTaskMethodBuilder<TResult> _methodBuilder;

		// Token: 0x040015C6 RID: 5574
		private TResult _result;

		// Token: 0x040015C7 RID: 5575
		private bool _haveResult;

		// Token: 0x040015C8 RID: 5576
		private bool _useBuilder;
	}
}
