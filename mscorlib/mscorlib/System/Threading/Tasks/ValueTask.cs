using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks.Sources;

namespace System.Threading.Tasks
{
	// Token: 0x0200029C RID: 668
	[AsyncMethodBuilder(typeof(AsyncValueTaskMethodBuilder))]
	[StructLayout(LayoutKind.Auto)]
	public readonly struct ValueTask : IEquatable<ValueTask>
	{
		// Token: 0x1700028E RID: 654
		// (get) Token: 0x0600186A RID: 6250 RVA: 0x0005D81E File Offset: 0x0005BA1E
		internal static Task CompletedTask
		{
			get
			{
				return Task.CompletedTask;
			}
		}

		// Token: 0x0600186B RID: 6251 RVA: 0x0005D825 File Offset: 0x0005BA25
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ValueTask(Task task)
		{
			if (task == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.task);
			}
			this._obj = task;
			this._continueOnCapturedContext = true;
			this._token = 0;
		}

		// Token: 0x0600186C RID: 6252 RVA: 0x0005D846 File Offset: 0x0005BA46
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ValueTask(IValueTaskSource source, short token)
		{
			if (source == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.source);
			}
			this._obj = source;
			this._token = token;
			this._continueOnCapturedContext = true;
		}

		// Token: 0x0600186D RID: 6253 RVA: 0x0005D867 File Offset: 0x0005BA67
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private ValueTask(object obj, short token, bool continueOnCapturedContext)
		{
			this._obj = obj;
			this._token = token;
			this._continueOnCapturedContext = continueOnCapturedContext;
		}

		// Token: 0x0600186E RID: 6254 RVA: 0x0005D87E File Offset: 0x0005BA7E
		public override int GetHashCode()
		{
			object obj = this._obj;
			if (obj == null)
			{
				return 0;
			}
			return obj.GetHashCode();
		}

		// Token: 0x0600186F RID: 6255 RVA: 0x0005D891 File Offset: 0x0005BA91
		public override bool Equals(object obj)
		{
			return obj is ValueTask && this.Equals((ValueTask)obj);
		}

		// Token: 0x06001870 RID: 6256 RVA: 0x0005D8A9 File Offset: 0x0005BAA9
		public bool Equals(ValueTask other)
		{
			return this._obj == other._obj && this._token == other._token;
		}

		// Token: 0x06001871 RID: 6257 RVA: 0x0005D8CC File Offset: 0x0005BACC
		public Task AsTask()
		{
			object obj = this._obj;
			Task task;
			if (obj != null)
			{
				if ((task = obj as Task) == null)
				{
					return this.GetTaskForValueTaskSource(Unsafe.As<IValueTaskSource>(obj));
				}
			}
			else
			{
				task = ValueTask.CompletedTask;
			}
			return task;
		}

		// Token: 0x06001872 RID: 6258 RVA: 0x0005D900 File Offset: 0x0005BB00
		private Task GetTaskForValueTaskSource(IValueTaskSource t)
		{
			ValueTaskSourceStatus status = t.GetStatus(this._token);
			if (status != ValueTaskSourceStatus.Pending)
			{
				try
				{
					t.GetResult(this._token);
					return ValueTask.CompletedTask;
				}
				catch (Exception ex)
				{
					if (status != ValueTaskSourceStatus.Canceled)
					{
						return Task.FromException(ex);
					}
					OperationCanceledException ex2 = ex as OperationCanceledException;
					if (ex2 != null)
					{
						Task<VoidTaskResult> task = new Task<VoidTaskResult>();
						task.TrySetCanceled(ex2.CancellationToken, ex2);
						return task;
					}
					return ValueTask.s_canceledTask;
				}
			}
			return new ValueTask.ValueTaskSourceAsTask(t, this._token);
		}

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06001873 RID: 6259 RVA: 0x0005D988 File Offset: 0x0005BB88
		public bool IsCompleted
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				object obj = this._obj;
				if (obj == null)
				{
					return true;
				}
				Task task = obj as Task;
				if (task != null)
				{
					return task.IsCompleted;
				}
				return Unsafe.As<IValueTaskSource>(obj).GetStatus(this._token) > ValueTaskSourceStatus.Pending;
			}
		}

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06001874 RID: 6260 RVA: 0x0005D9C8 File Offset: 0x0005BBC8
		public bool IsCompletedSuccessfully
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				object obj = this._obj;
				if (obj == null)
				{
					return true;
				}
				Task task = obj as Task;
				if (task != null)
				{
					return task.IsCompletedSuccessfully;
				}
				return Unsafe.As<IValueTaskSource>(obj).GetStatus(this._token) == ValueTaskSourceStatus.Succeeded;
			}
		}

		// Token: 0x06001875 RID: 6261 RVA: 0x0005DA08 File Offset: 0x0005BC08
		[StackTraceHidden]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal void ThrowIfCompletedUnsuccessfully()
		{
			object obj = this._obj;
			if (obj != null)
			{
				Task task = obj as Task;
				if (task != null)
				{
					TaskAwaiter.ValidateEnd(task);
					return;
				}
				Unsafe.As<IValueTaskSource>(obj).GetResult(this._token);
			}
		}

		// Token: 0x06001876 RID: 6262 RVA: 0x0005DA41 File Offset: 0x0005BC41
		public ValueTaskAwaiter GetAwaiter()
		{
			return new ValueTaskAwaiter(this);
		}

		// Token: 0x06001877 RID: 6263 RVA: 0x0005DA4E File Offset: 0x0005BC4E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ConfiguredValueTaskAwaitable ConfigureAwait(bool continueOnCapturedContext)
		{
			return new ConfiguredValueTaskAwaitable(new ValueTask(this._obj, this._token, continueOnCapturedContext));
		}

		// Token: 0x04000B98 RID: 2968
		private static readonly Task s_canceledTask = Task.FromCanceled(new CancellationToken(true));

		// Token: 0x04000B99 RID: 2969
		internal readonly object _obj;

		// Token: 0x04000B9A RID: 2970
		internal readonly short _token;

		// Token: 0x04000B9B RID: 2971
		internal readonly bool _continueOnCapturedContext;

		// Token: 0x0200029D RID: 669
		private sealed class ValueTaskSourceAsTask : Task<VoidTaskResult>
		{
			// Token: 0x06001879 RID: 6265 RVA: 0x0005DA79 File Offset: 0x0005BC79
			public ValueTaskSourceAsTask(IValueTaskSource source, short token)
			{
				this._token = token;
				this._source = source;
				source.OnCompleted(ValueTask.ValueTaskSourceAsTask.s_completionAction, this, token, ValueTaskSourceOnCompletedFlags.None);
			}

			// Token: 0x04000B9C RID: 2972
			private static readonly Action<object> s_completionAction = delegate(object state)
			{
				ValueTask.ValueTaskSourceAsTask valueTaskSourceAsTask = state as ValueTask.ValueTaskSourceAsTask;
				if (valueTaskSourceAsTask != null)
				{
					IValueTaskSource source = valueTaskSourceAsTask._source;
					if (source != null)
					{
						valueTaskSourceAsTask._source = null;
						ValueTaskSourceStatus status = source.GetStatus(valueTaskSourceAsTask._token);
						try
						{
							source.GetResult(valueTaskSourceAsTask._token);
							valueTaskSourceAsTask.TrySetResult(default(VoidTaskResult));
						}
						catch (Exception ex)
						{
							if (status == ValueTaskSourceStatus.Canceled)
							{
								OperationCanceledException ex2 = ex as OperationCanceledException;
								if (ex2 != null)
								{
									valueTaskSourceAsTask.TrySetCanceled(ex2.CancellationToken, ex2);
								}
								else
								{
									valueTaskSourceAsTask.TrySetCanceled(new CancellationToken(true));
								}
							}
							else
							{
								valueTaskSourceAsTask.TrySetException(ex);
							}
						}
						return;
					}
				}
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.state);
			};

			// Token: 0x04000B9D RID: 2973
			private IValueTaskSource _source;

			// Token: 0x04000B9E RID: 2974
			private readonly short _token;
		}
	}
}
