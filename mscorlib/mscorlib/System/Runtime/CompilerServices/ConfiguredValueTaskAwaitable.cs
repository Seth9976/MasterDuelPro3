using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Threading.Tasks.Sources;

namespace System.Runtime.CompilerServices
{
	// Token: 0x0200057F RID: 1407
	[StructLayout(LayoutKind.Auto)]
	public readonly struct ConfiguredValueTaskAwaitable
	{
		// Token: 0x06002ADC RID: 10972 RVA: 0x000AA828 File Offset: 0x000A8A28
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal ConfiguredValueTaskAwaitable(ValueTask value)
		{
			this._value = value;
		}

		// Token: 0x06002ADD RID: 10973 RVA: 0x000AA831 File Offset: 0x000A8A31
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter GetAwaiter()
		{
			return new ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter(this._value);
		}

		// Token: 0x040015C9 RID: 5577
		private readonly ValueTask _value;

		// Token: 0x02000580 RID: 1408
		[StructLayout(LayoutKind.Auto)]
		public readonly struct ConfiguredValueTaskAwaiter : ICriticalNotifyCompletion, INotifyCompletion
		{
			// Token: 0x06002ADE RID: 10974 RVA: 0x000AA83E File Offset: 0x000A8A3E
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			internal ConfiguredValueTaskAwaiter(ValueTask value)
			{
				this._value = value;
			}

			// Token: 0x17000571 RID: 1393
			// (get) Token: 0x06002ADF RID: 10975 RVA: 0x000AA847 File Offset: 0x000A8A47
			public bool IsCompleted
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					return this._value.IsCompleted;
				}
			}

			// Token: 0x06002AE0 RID: 10976 RVA: 0x000AA854 File Offset: 0x000A8A54
			[StackTraceHidden]
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public void GetResult()
			{
				this._value.ThrowIfCompletedUnsuccessfully();
			}

			// Token: 0x06002AE1 RID: 10977 RVA: 0x000AA864 File Offset: 0x000A8A64
			public void OnCompleted(Action continuation)
			{
				object obj = this._value._obj;
				Task task = obj as Task;
				if (task != null)
				{
					task.ConfigureAwait(this._value._continueOnCapturedContext).GetAwaiter().OnCompleted(continuation);
					return;
				}
				if (obj != null)
				{
					Unsafe.As<IValueTaskSource>(obj).OnCompleted(ValueTaskAwaiter.s_invokeActionDelegate, continuation, this._value._token, ValueTaskSourceOnCompletedFlags.FlowExecutionContext | (this._value._continueOnCapturedContext ? ValueTaskSourceOnCompletedFlags.UseSchedulingContext : ValueTaskSourceOnCompletedFlags.None));
					return;
				}
				ValueTask.CompletedTask.ConfigureAwait(this._value._continueOnCapturedContext).GetAwaiter().OnCompleted(continuation);
			}

			// Token: 0x06002AE2 RID: 10978 RVA: 0x000AA904 File Offset: 0x000A8B04
			public void UnsafeOnCompleted(Action continuation)
			{
				object obj = this._value._obj;
				Task task = obj as Task;
				if (task != null)
				{
					task.ConfigureAwait(this._value._continueOnCapturedContext).GetAwaiter().UnsafeOnCompleted(continuation);
					return;
				}
				if (obj != null)
				{
					Unsafe.As<IValueTaskSource>(obj).OnCompleted(ValueTaskAwaiter.s_invokeActionDelegate, continuation, this._value._token, this._value._continueOnCapturedContext ? ValueTaskSourceOnCompletedFlags.UseSchedulingContext : ValueTaskSourceOnCompletedFlags.None);
					return;
				}
				ValueTask.CompletedTask.ConfigureAwait(this._value._continueOnCapturedContext).GetAwaiter().UnsafeOnCompleted(continuation);
			}

			// Token: 0x040015CA RID: 5578
			private readonly ValueTask _value;
		}
	}
}
