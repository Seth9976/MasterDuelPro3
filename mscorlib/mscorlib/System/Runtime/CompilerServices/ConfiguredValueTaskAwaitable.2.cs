using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Threading.Tasks.Sources;

namespace System.Runtime.CompilerServices
{
	// Token: 0x02000581 RID: 1409
	[StructLayout(LayoutKind.Auto)]
	public readonly struct ConfiguredValueTaskAwaitable<TResult>
	{
		// Token: 0x06002AE3 RID: 10979 RVA: 0x000AA9A1 File Offset: 0x000A8BA1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal ConfiguredValueTaskAwaitable(ValueTask<TResult> value)
		{
			this._value = value;
		}

		// Token: 0x06002AE4 RID: 10980 RVA: 0x000AA9AA File Offset: 0x000A8BAA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ConfiguredValueTaskAwaitable<TResult>.ConfiguredValueTaskAwaiter GetAwaiter()
		{
			return new ConfiguredValueTaskAwaitable<TResult>.ConfiguredValueTaskAwaiter(this._value);
		}

		// Token: 0x040015CB RID: 5579
		private readonly ValueTask<TResult> _value;

		// Token: 0x02000582 RID: 1410
		[StructLayout(LayoutKind.Auto)]
		public readonly struct ConfiguredValueTaskAwaiter : ICriticalNotifyCompletion, INotifyCompletion
		{
			// Token: 0x06002AE5 RID: 10981 RVA: 0x000AA9B7 File Offset: 0x000A8BB7
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			internal ConfiguredValueTaskAwaiter(ValueTask<TResult> value)
			{
				this._value = value;
			}

			// Token: 0x17000572 RID: 1394
			// (get) Token: 0x06002AE6 RID: 10982 RVA: 0x000AA9C0 File Offset: 0x000A8BC0
			public bool IsCompleted
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					return this._value.IsCompleted;
				}
			}

			// Token: 0x06002AE7 RID: 10983 RVA: 0x000AA9CD File Offset: 0x000A8BCD
			[StackTraceHidden]
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public TResult GetResult()
			{
				return this._value.Result;
			}

			// Token: 0x06002AE8 RID: 10984 RVA: 0x000AA9DC File Offset: 0x000A8BDC
			public void OnCompleted(Action continuation)
			{
				object obj = this._value._obj;
				Task<TResult> task = obj as Task<TResult>;
				if (task != null)
				{
					task.ConfigureAwait(this._value._continueOnCapturedContext).GetAwaiter().OnCompleted(continuation);
					return;
				}
				if (obj != null)
				{
					Unsafe.As<IValueTaskSource<TResult>>(obj).OnCompleted(ValueTaskAwaiter.s_invokeActionDelegate, continuation, this._value._token, ValueTaskSourceOnCompletedFlags.FlowExecutionContext | (this._value._continueOnCapturedContext ? ValueTaskSourceOnCompletedFlags.UseSchedulingContext : ValueTaskSourceOnCompletedFlags.None));
					return;
				}
				ValueTask.CompletedTask.ConfigureAwait(this._value._continueOnCapturedContext).GetAwaiter().OnCompleted(continuation);
			}

			// Token: 0x06002AE9 RID: 10985 RVA: 0x000AAA80 File Offset: 0x000A8C80
			public void UnsafeOnCompleted(Action continuation)
			{
				object obj = this._value._obj;
				Task<TResult> task = obj as Task<TResult>;
				if (task != null)
				{
					task.ConfigureAwait(this._value._continueOnCapturedContext).GetAwaiter().UnsafeOnCompleted(continuation);
					return;
				}
				if (obj != null)
				{
					Unsafe.As<IValueTaskSource<TResult>>(obj).OnCompleted(ValueTaskAwaiter.s_invokeActionDelegate, continuation, this._value._token, this._value._continueOnCapturedContext ? ValueTaskSourceOnCompletedFlags.UseSchedulingContext : ValueTaskSourceOnCompletedFlags.None);
					return;
				}
				ValueTask.CompletedTask.ConfigureAwait(this._value._continueOnCapturedContext).GetAwaiter().UnsafeOnCompleted(continuation);
			}

			// Token: 0x040015CC RID: 5580
			private readonly ValueTask<TResult> _value;
		}
	}
}
