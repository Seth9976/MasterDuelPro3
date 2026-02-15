using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks.Sources;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x02000123 RID: 291
	public class AutoResetUniTaskCompletionSource<T> : IUniTaskSource<T>, IUniTaskSource, IValueTaskSource, IValueTaskSource<T>, ITaskPoolNode<AutoResetUniTaskCompletionSource<T>>, IPromise<T>, IResolvePromise<T>, IRejectPromise, ICancelPromise
	{
		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060006F5 RID: 1781 RVA: 0x00020BCB File Offset: 0x0001EDCB
		public ref AutoResetUniTaskCompletionSource<T> NextNode
		{
			get
			{
				return ref this.nextNode;
			}
		}

		// Token: 0x060006F6 RID: 1782 RVA: 0x00020BD3 File Offset: 0x0001EDD3
		static AutoResetUniTaskCompletionSource()
		{
			TaskPool.RegisterSizeGetter(typeof(AutoResetUniTaskCompletionSource<T>), () => AutoResetUniTaskCompletionSource<T>.pool.Size);
		}

		// Token: 0x060006F7 RID: 1783 RVA: 0x000020BB File Offset: 0x000002BB
		private AutoResetUniTaskCompletionSource()
		{
		}

		// Token: 0x060006F8 RID: 1784 RVA: 0x00020BF4 File Offset: 0x0001EDF4
		[DebuggerHidden]
		public static AutoResetUniTaskCompletionSource<T> Create()
		{
			AutoResetUniTaskCompletionSource<T> result;
			if (!AutoResetUniTaskCompletionSource<T>.pool.TryPop(out result))
			{
				result = new AutoResetUniTaskCompletionSource<T>();
			}
			result.version = result.core.Version;
			return result;
		}

		// Token: 0x060006F9 RID: 1785 RVA: 0x00020C28 File Offset: 0x0001EE28
		[DebuggerHidden]
		public static AutoResetUniTaskCompletionSource<T> CreateFromCanceled(CancellationToken cancellationToken, out short token)
		{
			AutoResetUniTaskCompletionSource<T> source = AutoResetUniTaskCompletionSource<T>.Create();
			source.TrySetCanceled(cancellationToken);
			token = source.core.Version;
			return source;
		}

		// Token: 0x060006FA RID: 1786 RVA: 0x00020C54 File Offset: 0x0001EE54
		[DebuggerHidden]
		public static AutoResetUniTaskCompletionSource<T> CreateFromException(Exception exception, out short token)
		{
			AutoResetUniTaskCompletionSource<T> source = AutoResetUniTaskCompletionSource<T>.Create();
			source.TrySetException(exception);
			token = source.core.Version;
			return source;
		}

		// Token: 0x060006FB RID: 1787 RVA: 0x00020C80 File Offset: 0x0001EE80
		[DebuggerHidden]
		public static AutoResetUniTaskCompletionSource<T> CreateFromResult(T result, out short token)
		{
			AutoResetUniTaskCompletionSource<T> source = AutoResetUniTaskCompletionSource<T>.Create();
			source.TrySetResult(result);
			token = source.core.Version;
			return source;
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060006FC RID: 1788 RVA: 0x00020CA9 File Offset: 0x0001EEA9
		public UniTask<T> Task
		{
			[DebuggerHidden]
			get
			{
				return new UniTask<T>(this, this.core.Version);
			}
		}

		// Token: 0x060006FD RID: 1789 RVA: 0x00020CBC File Offset: 0x0001EEBC
		[DebuggerHidden]
		public bool TrySetResult(T result)
		{
			return this.version == this.core.Version && this.core.TrySetResult(result);
		}

		// Token: 0x060006FE RID: 1790 RVA: 0x00020CDF File Offset: 0x0001EEDF
		[DebuggerHidden]
		public bool TrySetCanceled(CancellationToken cancellationToken = default(CancellationToken))
		{
			return this.version == this.core.Version && this.core.TrySetCanceled(cancellationToken);
		}

		// Token: 0x060006FF RID: 1791 RVA: 0x00020D02 File Offset: 0x0001EF02
		[DebuggerHidden]
		public bool TrySetException(Exception exception)
		{
			return this.version == this.core.Version && this.core.TrySetException(exception);
		}

		// Token: 0x06000700 RID: 1792 RVA: 0x00020D28 File Offset: 0x0001EF28
		[DebuggerHidden]
		public T GetResult(short token)
		{
			T result;
			try
			{
				result = this.core.GetResult(token);
			}
			finally
			{
				this.TryReturn();
			}
			return result;
		}

		// Token: 0x06000701 RID: 1793 RVA: 0x00020D60 File Offset: 0x0001EF60
		[DebuggerHidden]
		void IUniTaskSource.GetResult(short token)
		{
			this.GetResult(token);
		}

		// Token: 0x06000702 RID: 1794 RVA: 0x00020D6A File Offset: 0x0001EF6A
		[DebuggerHidden]
		public UniTaskStatus GetStatus(short token)
		{
			return this.core.GetStatus(token);
		}

		// Token: 0x06000703 RID: 1795 RVA: 0x00020D78 File Offset: 0x0001EF78
		[DebuggerHidden]
		public UniTaskStatus UnsafeGetStatus()
		{
			return this.core.UnsafeGetStatus();
		}

		// Token: 0x06000704 RID: 1796 RVA: 0x00020D85 File Offset: 0x0001EF85
		[DebuggerHidden]
		public void OnCompleted(Action<object> continuation, object state, short token)
		{
			this.core.OnCompleted(continuation, state, token);
		}

		// Token: 0x06000705 RID: 1797 RVA: 0x00020D95 File Offset: 0x0001EF95
		[DebuggerHidden]
		private bool TryReturn()
		{
			this.core.Reset();
			return AutoResetUniTaskCompletionSource<T>.pool.TryPush(this);
		}

		// Token: 0x04000444 RID: 1092
		private static TaskPool<AutoResetUniTaskCompletionSource<T>> pool;

		// Token: 0x04000445 RID: 1093
		private AutoResetUniTaskCompletionSource<T> nextNode;

		// Token: 0x04000446 RID: 1094
		private UniTaskCompletionSourceCore<T> core;

		// Token: 0x04000447 RID: 1095
		private short version;
	}
}
