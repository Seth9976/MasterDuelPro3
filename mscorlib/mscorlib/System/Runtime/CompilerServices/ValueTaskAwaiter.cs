using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Threading.Tasks.Sources;

namespace System.Runtime.CompilerServices
{
	// Token: 0x0200059B RID: 1435
	public readonly struct ValueTaskAwaiter : ICriticalNotifyCompletion, INotifyCompletion
	{
		// Token: 0x06002B11 RID: 11025 RVA: 0x000AACBA File Offset: 0x000A8EBA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal ValueTaskAwaiter(ValueTask value)
		{
			this._value = value;
		}

		// Token: 0x1700057F RID: 1407
		// (get) Token: 0x06002B12 RID: 11026 RVA: 0x000AACC3 File Offset: 0x000A8EC3
		public bool IsCompleted
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this._value.IsCompleted;
			}
		}

		// Token: 0x06002B13 RID: 11027 RVA: 0x000AACD0 File Offset: 0x000A8ED0
		[StackTraceHidden]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void GetResult()
		{
			this._value.ThrowIfCompletedUnsuccessfully();
		}

		// Token: 0x06002B14 RID: 11028 RVA: 0x000AACE0 File Offset: 0x000A8EE0
		public void OnCompleted(Action continuation)
		{
			object obj = this._value._obj;
			Task task = obj as Task;
			if (task != null)
			{
				task.GetAwaiter().OnCompleted(continuation);
				return;
			}
			if (obj != null)
			{
				Unsafe.As<IValueTaskSource>(obj).OnCompleted(ValueTaskAwaiter.s_invokeActionDelegate, continuation, this._value._token, ValueTaskSourceOnCompletedFlags.UseSchedulingContext | ValueTaskSourceOnCompletedFlags.FlowExecutionContext);
				return;
			}
			ValueTask.CompletedTask.GetAwaiter().OnCompleted(continuation);
		}

		// Token: 0x06002B15 RID: 11029 RVA: 0x000AAD48 File Offset: 0x000A8F48
		public void UnsafeOnCompleted(Action continuation)
		{
			object obj = this._value._obj;
			Task task = obj as Task;
			if (task != null)
			{
				task.GetAwaiter().UnsafeOnCompleted(continuation);
				return;
			}
			if (obj != null)
			{
				Unsafe.As<IValueTaskSource>(obj).OnCompleted(ValueTaskAwaiter.s_invokeActionDelegate, continuation, this._value._token, ValueTaskSourceOnCompletedFlags.UseSchedulingContext);
				return;
			}
			ValueTask.CompletedTask.GetAwaiter().UnsafeOnCompleted(continuation);
		}

		// Token: 0x040015D8 RID: 5592
		internal static readonly Action<object> s_invokeActionDelegate = delegate(object state)
		{
			Action action = state as Action;
			if (action == null)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.state);
				return;
			}
			action();
		};

		// Token: 0x040015D9 RID: 5593
		private readonly ValueTask _value;
	}
}
