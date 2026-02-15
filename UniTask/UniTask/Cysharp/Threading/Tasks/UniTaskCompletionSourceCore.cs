using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Threading;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x0200011F RID: 287
	[StructLayout(LayoutKind.Auto)]
	public struct UniTaskCompletionSourceCore<TResult>
	{
		// Token: 0x060006D4 RID: 1748 RVA: 0x00020688 File Offset: 0x0001E888
		[DebuggerHidden]
		public void Reset()
		{
			this.ReportUnhandledError();
			this.version += 1;
			this.completedCount = 0;
			this.result = default(TResult);
			this.error = null;
			this.hasUnhandledError = false;
			this.continuation = null;
			this.continuationState = null;
		}

		// Token: 0x060006D5 RID: 1749 RVA: 0x000206DC File Offset: 0x0001E8DC
		private void ReportUnhandledError()
		{
			if (this.hasUnhandledError)
			{
				try
				{
					OperationCanceledException oc = this.error as OperationCanceledException;
					if (oc != null)
					{
						UniTaskScheduler.PublishUnobservedTaskException(oc);
					}
					else
					{
						ExceptionHolder e = this.error as ExceptionHolder;
						if (e != null)
						{
							UniTaskScheduler.PublishUnobservedTaskException(e.GetException().SourceException);
						}
					}
				}
				catch
				{
				}
			}
		}

		// Token: 0x060006D6 RID: 1750 RVA: 0x0002073C File Offset: 0x0001E93C
		internal void MarkHandled()
		{
			this.hasUnhandledError = false;
		}

		// Token: 0x060006D7 RID: 1751 RVA: 0x00020748 File Offset: 0x0001E948
		[DebuggerHidden]
		public bool TrySetResult(TResult result)
		{
			if (Interlocked.Increment(ref this.completedCount) == 1)
			{
				this.result = result;
				if (this.continuation != null || Interlocked.CompareExchange<Action<object>>(ref this.continuation, UniTaskCompletionSourceCoreShared.s_sentinel, null) != null)
				{
					this.continuation(this.continuationState);
				}
				return true;
			}
			return false;
		}

		// Token: 0x060006D8 RID: 1752 RVA: 0x0002079C File Offset: 0x0001E99C
		[DebuggerHidden]
		public bool TrySetException(Exception error)
		{
			if (Interlocked.Increment(ref this.completedCount) == 1)
			{
				this.hasUnhandledError = true;
				if (error is OperationCanceledException)
				{
					this.error = error;
				}
				else
				{
					this.error = new ExceptionHolder(ExceptionDispatchInfo.Capture(error));
				}
				if (this.continuation != null || Interlocked.CompareExchange<Action<object>>(ref this.continuation, UniTaskCompletionSourceCoreShared.s_sentinel, null) != null)
				{
					this.continuation(this.continuationState);
				}
				return true;
			}
			return false;
		}

		// Token: 0x060006D9 RID: 1753 RVA: 0x00020810 File Offset: 0x0001EA10
		[DebuggerHidden]
		public bool TrySetCanceled(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (Interlocked.Increment(ref this.completedCount) == 1)
			{
				this.hasUnhandledError = true;
				this.error = new OperationCanceledException(cancellationToken);
				if (this.continuation != null || Interlocked.CompareExchange<Action<object>>(ref this.continuation, UniTaskCompletionSourceCoreShared.s_sentinel, null) != null)
				{
					this.continuation(this.continuationState);
				}
				return true;
			}
			return false;
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060006DA RID: 1754 RVA: 0x0002086D File Offset: 0x0001EA6D
		[DebuggerHidden]
		public short Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x060006DB RID: 1755 RVA: 0x00020875 File Offset: 0x0001EA75
		[DebuggerHidden]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public UniTaskStatus GetStatus(short token)
		{
			this.ValidateToken(token);
			if (this.continuation == null || this.completedCount == 0)
			{
				return UniTaskStatus.Pending;
			}
			if (this.error == null)
			{
				return UniTaskStatus.Succeeded;
			}
			if (!(this.error is OperationCanceledException))
			{
				return UniTaskStatus.Faulted;
			}
			return UniTaskStatus.Canceled;
		}

		// Token: 0x060006DC RID: 1756 RVA: 0x000208AA File Offset: 0x0001EAAA
		[DebuggerHidden]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public UniTaskStatus UnsafeGetStatus()
		{
			if (this.continuation == null || this.completedCount == 0)
			{
				return UniTaskStatus.Pending;
			}
			if (this.error == null)
			{
				return UniTaskStatus.Succeeded;
			}
			if (!(this.error is OperationCanceledException))
			{
				return UniTaskStatus.Faulted;
			}
			return UniTaskStatus.Canceled;
		}

		// Token: 0x060006DD RID: 1757 RVA: 0x000208D8 File Offset: 0x0001EAD8
		[DebuggerHidden]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public TResult GetResult(short token)
		{
			this.ValidateToken(token);
			if (this.completedCount == 0)
			{
				throw new InvalidOperationException("Not yet completed, UniTask only allow to use await.");
			}
			if (this.error == null)
			{
				return this.result;
			}
			this.hasUnhandledError = false;
			OperationCanceledException oce = this.error as OperationCanceledException;
			if (oce != null)
			{
				throw oce;
			}
			ExceptionHolder eh = this.error as ExceptionHolder;
			if (eh != null)
			{
				eh.GetException().Throw();
			}
			throw new InvalidOperationException("Critical: invalid exception type was held.");
		}

		// Token: 0x060006DE RID: 1758 RVA: 0x0002094C File Offset: 0x0001EB4C
		[DebuggerHidden]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void OnCompleted(Action<object> continuation, object state, short token)
		{
			if (continuation == null)
			{
				throw new ArgumentNullException("continuation");
			}
			this.ValidateToken(token);
			object oldContinuation = this.continuation;
			if (oldContinuation == null)
			{
				this.continuationState = state;
				oldContinuation = Interlocked.CompareExchange<Action<object>>(ref this.continuation, continuation, null);
			}
			if (oldContinuation != null)
			{
				if (oldContinuation != UniTaskCompletionSourceCoreShared.s_sentinel)
				{
					throw new InvalidOperationException("Already continuation registered, can not await twice or get Status after await.");
				}
				continuation(state);
			}
		}

		// Token: 0x060006DF RID: 1759 RVA: 0x000209AA File Offset: 0x0001EBAA
		[DebuggerHidden]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void ValidateToken(short token)
		{
			if (token != this.version)
			{
				throw new InvalidOperationException("Token version is not matched, can not await twice or get Status after await.");
			}
		}

		// Token: 0x04000437 RID: 1079
		private TResult result;

		// Token: 0x04000438 RID: 1080
		private object error;

		// Token: 0x04000439 RID: 1081
		private short version;

		// Token: 0x0400043A RID: 1082
		private bool hasUnhandledError;

		// Token: 0x0400043B RID: 1083
		private int completedCount;

		// Token: 0x0400043C RID: 1084
		private Action<object> continuation;

		// Token: 0x0400043D RID: 1085
		private object continuationState;
	}
}
