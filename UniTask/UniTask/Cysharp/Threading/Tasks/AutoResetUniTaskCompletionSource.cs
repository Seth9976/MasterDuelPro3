using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks.Sources;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x02000121 RID: 289
	public class AutoResetUniTaskCompletionSource : IUniTaskSource, IValueTaskSource, ITaskPoolNode<AutoResetUniTaskCompletionSource>, IPromise, IResolvePromise, IRejectPromise, ICancelPromise
	{
		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060006E2 RID: 1762 RVA: 0x000209DF File Offset: 0x0001EBDF
		public ref AutoResetUniTaskCompletionSource NextNode
		{
			get
			{
				return ref this.nextNode;
			}
		}

		// Token: 0x060006E3 RID: 1763 RVA: 0x000209E7 File Offset: 0x0001EBE7
		static AutoResetUniTaskCompletionSource()
		{
			TaskPool.RegisterSizeGetter(typeof(AutoResetUniTaskCompletionSource), () => AutoResetUniTaskCompletionSource.pool.Size);
		}

		// Token: 0x060006E4 RID: 1764 RVA: 0x000020BB File Offset: 0x000002BB
		private AutoResetUniTaskCompletionSource()
		{
		}

		// Token: 0x060006E5 RID: 1765 RVA: 0x00020A08 File Offset: 0x0001EC08
		[DebuggerHidden]
		public static AutoResetUniTaskCompletionSource Create()
		{
			AutoResetUniTaskCompletionSource result;
			if (!AutoResetUniTaskCompletionSource.pool.TryPop(out result))
			{
				result = new AutoResetUniTaskCompletionSource();
			}
			result.version = result.core.Version;
			return result;
		}

		// Token: 0x060006E6 RID: 1766 RVA: 0x00020A3C File Offset: 0x0001EC3C
		[DebuggerHidden]
		public static AutoResetUniTaskCompletionSource CreateFromCanceled(CancellationToken cancellationToken, out short token)
		{
			AutoResetUniTaskCompletionSource source = AutoResetUniTaskCompletionSource.Create();
			source.TrySetCanceled(cancellationToken);
			token = source.core.Version;
			return source;
		}

		// Token: 0x060006E7 RID: 1767 RVA: 0x00020A68 File Offset: 0x0001EC68
		[DebuggerHidden]
		public static AutoResetUniTaskCompletionSource CreateFromException(Exception exception, out short token)
		{
			AutoResetUniTaskCompletionSource source = AutoResetUniTaskCompletionSource.Create();
			source.TrySetException(exception);
			token = source.core.Version;
			return source;
		}

		// Token: 0x060006E8 RID: 1768 RVA: 0x00020A94 File Offset: 0x0001EC94
		[DebuggerHidden]
		public static AutoResetUniTaskCompletionSource CreateCompleted(out short token)
		{
			AutoResetUniTaskCompletionSource source = AutoResetUniTaskCompletionSource.Create();
			source.TrySetResult();
			token = source.core.Version;
			return source;
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060006E9 RID: 1769 RVA: 0x00020ABC File Offset: 0x0001ECBC
		public UniTask Task
		{
			[DebuggerHidden]
			get
			{
				return new UniTask(this, this.core.Version);
			}
		}

		// Token: 0x060006EA RID: 1770 RVA: 0x00020ACF File Offset: 0x0001ECCF
		[DebuggerHidden]
		public bool TrySetResult()
		{
			return this.version == this.core.Version && this.core.TrySetResult(AsyncUnit.Default);
		}

		// Token: 0x060006EB RID: 1771 RVA: 0x00020AF6 File Offset: 0x0001ECF6
		[DebuggerHidden]
		public bool TrySetCanceled(CancellationToken cancellationToken = default(CancellationToken))
		{
			return this.version == this.core.Version && this.core.TrySetCanceled(cancellationToken);
		}

		// Token: 0x060006EC RID: 1772 RVA: 0x00020B19 File Offset: 0x0001ED19
		[DebuggerHidden]
		public bool TrySetException(Exception exception)
		{
			return this.version == this.core.Version && this.core.TrySetException(exception);
		}

		// Token: 0x060006ED RID: 1773 RVA: 0x00020B3C File Offset: 0x0001ED3C
		[DebuggerHidden]
		public void GetResult(short token)
		{
			try
			{
				this.core.GetResult(token);
			}
			finally
			{
				this.TryReturn();
			}
		}

		// Token: 0x060006EE RID: 1774 RVA: 0x00020B70 File Offset: 0x0001ED70
		[DebuggerHidden]
		public UniTaskStatus GetStatus(short token)
		{
			return this.core.GetStatus(token);
		}

		// Token: 0x060006EF RID: 1775 RVA: 0x00020B7E File Offset: 0x0001ED7E
		[DebuggerHidden]
		public UniTaskStatus UnsafeGetStatus()
		{
			return this.core.UnsafeGetStatus();
		}

		// Token: 0x060006F0 RID: 1776 RVA: 0x00020B8B File Offset: 0x0001ED8B
		[DebuggerHidden]
		public void OnCompleted(Action<object> continuation, object state, short token)
		{
			this.core.OnCompleted(continuation, state, token);
		}

		// Token: 0x060006F1 RID: 1777 RVA: 0x00020B9B File Offset: 0x0001ED9B
		[DebuggerHidden]
		private bool TryReturn()
		{
			this.core.Reset();
			return AutoResetUniTaskCompletionSource.pool.TryPush(this);
		}

		// Token: 0x0400043F RID: 1087
		private static TaskPool<AutoResetUniTaskCompletionSource> pool;

		// Token: 0x04000440 RID: 1088
		private AutoResetUniTaskCompletionSource nextNode;

		// Token: 0x04000441 RID: 1089
		private UniTaskCompletionSourceCore<AsyncUnit> core;

		// Token: 0x04000442 RID: 1090
		private short version;
	}
}
