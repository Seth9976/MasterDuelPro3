using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Threading.Tasks.Sources;
using Cysharp.Threading.Tasks.CompilerServices;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x02000114 RID: 276
	[global::System.Runtime.CompilerServices.AsyncMethodBuilder(typeof(AsyncUniTaskMethodBuilder<>))]
	[StructLayout(LayoutKind.Auto)]
	public readonly struct UniTask<T>
	{
		// Token: 0x060006B1 RID: 1713 RVA: 0x000201D5 File Offset: 0x0001E3D5
		[DebuggerHidden]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public UniTask(T result)
		{
			this.source = null;
			this.token = 0;
			this.result = result;
		}

		// Token: 0x060006B2 RID: 1714 RVA: 0x000201EC File Offset: 0x0001E3EC
		[DebuggerHidden]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public UniTask(IUniTaskSource<T> source, short token)
		{
			this.source = source;
			this.token = token;
			this.result = default(T);
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060006B3 RID: 1715 RVA: 0x00020208 File Offset: 0x0001E408
		public UniTaskStatus Status
		{
			[DebuggerHidden]
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				if (this.source != null)
				{
					return this.source.GetStatus(this.token);
				}
				return UniTaskStatus.Succeeded;
			}
		}

		// Token: 0x060006B4 RID: 1716 RVA: 0x00020225 File Offset: 0x0001E425
		[DebuggerHidden]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public UniTask<T>.Awaiter GetAwaiter()
		{
			return new UniTask<T>.Awaiter(in this);
		}

		// Token: 0x060006B5 RID: 1717 RVA: 0x0002022D File Offset: 0x0001E42D
		public UniTask<T> Preserve()
		{
			if (this.source == null)
			{
				return this;
			}
			return new UniTask<T>(new UniTask<T>.MemoizeSource(this.source), this.token);
		}

		// Token: 0x060006B6 RID: 1718 RVA: 0x00020254 File Offset: 0x0001E454
		public UniTask AsUniTask()
		{
			if (this.source == null)
			{
				return UniTask.CompletedTask;
			}
			if (this.source.GetStatus(this.token).IsCompletedSuccessfully())
			{
				this.source.GetResult(this.token);
				return UniTask.CompletedTask;
			}
			return new UniTask(this.source, this.token);
		}

		// Token: 0x060006B7 RID: 1719 RVA: 0x000202B0 File Offset: 0x0001E4B0
		public static implicit operator UniTask(UniTask<T> self)
		{
			return self.AsUniTask();
		}

		// Token: 0x060006B8 RID: 1720 RVA: 0x000202B9 File Offset: 0x0001E4B9
		public static implicit operator ValueTask<T>(in UniTask<T> self)
		{
			if (self.source == null)
			{
				return new ValueTask<T>(self.result);
			}
			return new ValueTask<T>(self.source, self.token);
		}

		// Token: 0x060006B9 RID: 1721 RVA: 0x000202E0 File Offset: 0x0001E4E0
		[return: TupleElementNames(new string[] { "IsCanceled", "Result" })]
		public UniTask<ValueTuple<bool, T>> SuppressCancellationThrow()
		{
			if (this.source == null)
			{
				return new UniTask<ValueTuple<bool, T>>(new ValueTuple<bool, T>(false, this.result));
			}
			return new UniTask<ValueTuple<bool, T>>(new UniTask<T>.IsCanceledSource(this.source), this.token);
		}

		// Token: 0x060006BA RID: 1722 RVA: 0x00020314 File Offset: 0x0001E514
		public override string ToString()
		{
			if (this.source != null)
			{
				return "(" + this.source.UnsafeGetStatus().ToString() + ")";
			}
			T t = this.result;
			if (t == null)
			{
				return null;
			}
			return t.ToString();
		}

		// Token: 0x0400042C RID: 1068
		private readonly IUniTaskSource<T> source;

		// Token: 0x0400042D RID: 1069
		private readonly T result;

		// Token: 0x0400042E RID: 1070
		private readonly short token;

		// Token: 0x02000115 RID: 277
		private sealed class IsCanceledSource : IUniTaskSource<ValueTuple<bool, T>>, IUniTaskSource, IValueTaskSource, IValueTaskSource<ValueTuple<bool, T>>
		{
			// Token: 0x060006BB RID: 1723 RVA: 0x00020376 File Offset: 0x0001E576
			[DebuggerHidden]
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public IsCanceledSource(IUniTaskSource<T> source)
			{
				this.source = source;
			}

			// Token: 0x060006BC RID: 1724 RVA: 0x00020388 File Offset: 0x0001E588
			[DebuggerHidden]
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public ValueTuple<bool, T> GetResult(short token)
			{
				if (this.source.GetStatus(token) == UniTaskStatus.Canceled)
				{
					return new ValueTuple<bool, T>(true, default(T));
				}
				T result = this.source.GetResult(token);
				return new ValueTuple<bool, T>(false, result);
			}

			// Token: 0x060006BD RID: 1725 RVA: 0x000203C8 File Offset: 0x0001E5C8
			[DebuggerHidden]
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			void IUniTaskSource.GetResult(short token)
			{
				this.GetResult(token);
			}

			// Token: 0x060006BE RID: 1726 RVA: 0x000203D2 File Offset: 0x0001E5D2
			[DebuggerHidden]
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public UniTaskStatus GetStatus(short token)
			{
				return this.source.GetStatus(token);
			}

			// Token: 0x060006BF RID: 1727 RVA: 0x000203E0 File Offset: 0x0001E5E0
			[DebuggerHidden]
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.source.UnsafeGetStatus();
			}

			// Token: 0x060006C0 RID: 1728 RVA: 0x000203ED File Offset: 0x0001E5ED
			[DebuggerHidden]
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.source.OnCompleted(continuation, state, token);
			}

			// Token: 0x0400042F RID: 1071
			private readonly IUniTaskSource<T> source;
		}

		// Token: 0x02000116 RID: 278
		private sealed class MemoizeSource : IUniTaskSource<T>, IUniTaskSource, IValueTaskSource, IValueTaskSource<T>
		{
			// Token: 0x060006C1 RID: 1729 RVA: 0x000203FD File Offset: 0x0001E5FD
			public MemoizeSource(IUniTaskSource<T> source)
			{
				this.source = source;
			}

			// Token: 0x060006C2 RID: 1730 RVA: 0x0002040C File Offset: 0x0001E60C
			public T GetResult(short token)
			{
				if (this.source == null)
				{
					if (this.exception != null)
					{
						this.exception.Throw();
					}
					return this.result;
				}
				T t;
				try
				{
					this.result = this.source.GetResult(token);
					this.status = UniTaskStatus.Succeeded;
					t = this.result;
				}
				catch (Exception ex)
				{
					this.exception = ExceptionDispatchInfo.Capture(ex);
					if (ex is OperationCanceledException)
					{
						this.status = UniTaskStatus.Canceled;
					}
					else
					{
						this.status = UniTaskStatus.Faulted;
					}
					throw;
				}
				finally
				{
					this.source = null;
				}
				return t;
			}

			// Token: 0x060006C3 RID: 1731 RVA: 0x000204AC File Offset: 0x0001E6AC
			void IUniTaskSource.GetResult(short token)
			{
				this.GetResult(token);
			}

			// Token: 0x060006C4 RID: 1732 RVA: 0x000204B6 File Offset: 0x0001E6B6
			public UniTaskStatus GetStatus(short token)
			{
				if (this.source == null)
				{
					return this.status;
				}
				return this.source.GetStatus(token);
			}

			// Token: 0x060006C5 RID: 1733 RVA: 0x000204D3 File Offset: 0x0001E6D3
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				if (this.source == null)
				{
					continuation(state);
					return;
				}
				this.source.OnCompleted(continuation, state, token);
			}

			// Token: 0x060006C6 RID: 1734 RVA: 0x000204F3 File Offset: 0x0001E6F3
			public UniTaskStatus UnsafeGetStatus()
			{
				if (this.source == null)
				{
					return this.status;
				}
				return this.source.UnsafeGetStatus();
			}

			// Token: 0x04000430 RID: 1072
			private IUniTaskSource<T> source;

			// Token: 0x04000431 RID: 1073
			private T result;

			// Token: 0x04000432 RID: 1074
			private ExceptionDispatchInfo exception;

			// Token: 0x04000433 RID: 1075
			private UniTaskStatus status;
		}

		// Token: 0x02000117 RID: 279
		public readonly struct Awaiter : ICriticalNotifyCompletion, INotifyCompletion
		{
			// Token: 0x060006C7 RID: 1735 RVA: 0x0002050F File Offset: 0x0001E70F
			[DebuggerHidden]
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public Awaiter(in UniTask<T> task)
			{
				this.task = task;
			}

			// Token: 0x1700004E RID: 78
			// (get) Token: 0x060006C8 RID: 1736 RVA: 0x0002051D File Offset: 0x0001E71D
			public bool IsCompleted
			{
				[DebuggerHidden]
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					return this.task.Status.IsCompleted();
				}
			}

			// Token: 0x060006C9 RID: 1737 RVA: 0x00020530 File Offset: 0x0001E730
			[DebuggerHidden]
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public T GetResult()
			{
				IUniTaskSource<T> s = this.task.source;
				if (s == null)
				{
					return this.task.result;
				}
				return s.GetResult(this.task.token);
			}

			// Token: 0x060006CA RID: 1738 RVA: 0x0002056C File Offset: 0x0001E76C
			[DebuggerHidden]
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public void OnCompleted(Action continuation)
			{
				IUniTaskSource<T> s = this.task.source;
				if (s == null)
				{
					continuation();
					return;
				}
				s.OnCompleted(AwaiterActions.InvokeContinuationDelegate, continuation, this.task.token);
			}

			// Token: 0x060006CB RID: 1739 RVA: 0x000205A8 File Offset: 0x0001E7A8
			[DebuggerHidden]
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public void UnsafeOnCompleted(Action continuation)
			{
				IUniTaskSource<T> s = this.task.source;
				if (s == null)
				{
					continuation();
					return;
				}
				s.OnCompleted(AwaiterActions.InvokeContinuationDelegate, continuation, this.task.token);
			}

			// Token: 0x060006CC RID: 1740 RVA: 0x000205E4 File Offset: 0x0001E7E4
			[DebuggerHidden]
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public void SourceOnCompleted(Action<object> continuation, object state)
			{
				IUniTaskSource<T> s = this.task.source;
				if (s == null)
				{
					continuation(state);
					return;
				}
				s.OnCompleted(continuation, state, this.task.token);
			}

			// Token: 0x04000434 RID: 1076
			private readonly UniTask<T> task;
		}
	}
}
