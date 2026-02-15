using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Threading.Tasks.Sources;

namespace System.Runtime.CompilerServices
{
	// Token: 0x0200059D RID: 1437
	public readonly struct ValueTaskAwaiter<TResult> : ICriticalNotifyCompletion, INotifyCompletion
	{
		// Token: 0x06002B1A RID: 11034 RVA: 0x000AADF9 File Offset: 0x000A8FF9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal ValueTaskAwaiter(ValueTask<TResult> value)
		{
			this._value = value;
		}

		// Token: 0x17000580 RID: 1408
		// (get) Token: 0x06002B1B RID: 11035 RVA: 0x000AAE02 File Offset: 0x000A9002
		public bool IsCompleted
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this._value.IsCompleted;
			}
		}

		// Token: 0x06002B1C RID: 11036 RVA: 0x000AAE0F File Offset: 0x000A900F
		[StackTraceHidden]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public TResult GetResult()
		{
			return this._value.Result;
		}

		// Token: 0x06002B1D RID: 11037 RVA: 0x000AAE1C File Offset: 0x000A901C
		public void OnCompleted(Action continuation)
		{
			object obj = this._value._obj;
			Task<TResult> task = obj as Task<TResult>;
			if (task != null)
			{
				task.GetAwaiter().OnCompleted(continuation);
				return;
			}
			if (obj != null)
			{
				Unsafe.As<IValueTaskSource<TResult>>(obj).OnCompleted(ValueTaskAwaiter.s_invokeActionDelegate, continuation, this._value._token, ValueTaskSourceOnCompletedFlags.UseSchedulingContext | ValueTaskSourceOnCompletedFlags.FlowExecutionContext);
				return;
			}
			ValueTask.CompletedTask.GetAwaiter().OnCompleted(continuation);
		}

		// Token: 0x06002B1E RID: 11038 RVA: 0x000AAE84 File Offset: 0x000A9084
		public void UnsafeOnCompleted(Action continuation)
		{
			object obj = this._value._obj;
			Task<TResult> task = obj as Task<TResult>;
			if (task != null)
			{
				task.GetAwaiter().UnsafeOnCompleted(continuation);
				return;
			}
			if (obj != null)
			{
				Unsafe.As<IValueTaskSource<TResult>>(obj).OnCompleted(ValueTaskAwaiter.s_invokeActionDelegate, continuation, this._value._token, ValueTaskSourceOnCompletedFlags.UseSchedulingContext);
				return;
			}
			ValueTask.CompletedTask.GetAwaiter().UnsafeOnCompleted(continuation);
		}

		// Token: 0x040015DB RID: 5595
		private readonly ValueTask<TResult> _value;
	}
}
