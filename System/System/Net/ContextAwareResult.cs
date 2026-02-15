using System;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using System.Threading;

namespace System.Net
{
	// Token: 0x02000379 RID: 889
	internal class ContextAwareResult : LazyAsyncResult
	{
		// Token: 0x06001621 RID: 5665 RVA: 0x0005DF74 File Offset: 0x0005C174
		private void SafeCaptureIdentity()
		{
			this._windowsIdentity = WindowsIdentity.GetCurrent();
		}

		// Token: 0x06001622 RID: 5666 RVA: 0x0005DF81 File Offset: 0x0005C181
		private void CleanupInternal()
		{
			if (this._windowsIdentity != null)
			{
				this._windowsIdentity.Dispose();
				this._windowsIdentity = null;
			}
		}

		// Token: 0x06001623 RID: 5667 RVA: 0x0005DF9D File Offset: 0x0005C19D
		internal ContextAwareResult(object myObject, object myState, AsyncCallback myCallBack)
			: this(false, false, myObject, myState, myCallBack)
		{
		}

		// Token: 0x06001624 RID: 5668 RVA: 0x0005DFAA File Offset: 0x0005C1AA
		internal ContextAwareResult(bool captureIdentity, bool forceCaptureContext, object myObject, object myState, AsyncCallback myCallBack)
			: this(captureIdentity, forceCaptureContext, false, myObject, myState, myCallBack)
		{
		}

		// Token: 0x06001625 RID: 5669 RVA: 0x0005DFBA File Offset: 0x0005C1BA
		internal ContextAwareResult(bool captureIdentity, bool forceCaptureContext, bool threadSafeContextCopy, object myObject, object myState, AsyncCallback myCallBack)
			: base(myObject, myState, myCallBack)
		{
			if (forceCaptureContext)
			{
				this._flags = ContextAwareResult.StateFlags.CaptureContext;
			}
			if (captureIdentity)
			{
				this._flags |= ContextAwareResult.StateFlags.CaptureIdentity;
			}
			if (threadSafeContextCopy)
			{
				this._flags |= ContextAwareResult.StateFlags.ThreadSafeContextCopy;
			}
		}

		// Token: 0x06001626 RID: 5670 RVA: 0x0005DFF4 File Offset: 0x0005C1F4
		internal object StartPostingAsyncOp()
		{
			return this.StartPostingAsyncOp(true);
		}

		// Token: 0x06001627 RID: 5671 RVA: 0x0005DFFD File Offset: 0x0005C1FD
		internal object StartPostingAsyncOp(bool lockCapture)
		{
			if (base.InternalPeekCompleted)
			{
				NetEventSource.Fail(this, "Called on completed result.", "StartPostingAsyncOp");
			}
			this._lock = (lockCapture ? new object() : null);
			this._flags |= ContextAwareResult.StateFlags.PostBlockStarted;
			return this._lock;
		}

		// Token: 0x06001628 RID: 5672 RVA: 0x0005E03C File Offset: 0x0005C23C
		internal bool FinishPostingAsyncOp()
		{
			if ((this._flags & (ContextAwareResult.StateFlags.PostBlockStarted | ContextAwareResult.StateFlags.PostBlockFinished)) != ContextAwareResult.StateFlags.PostBlockStarted)
			{
				return false;
			}
			this._flags |= ContextAwareResult.StateFlags.PostBlockFinished;
			ExecutionContext executionContext = null;
			return this.CaptureOrComplete(ref executionContext, false);
		}

		// Token: 0x06001629 RID: 5673 RVA: 0x0005E071 File Offset: 0x0005C271
		protected override void Cleanup()
		{
			base.Cleanup();
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Info(this, null, "Cleanup");
			}
			this.CleanupInternal();
		}

		// Token: 0x0600162A RID: 5674 RVA: 0x0005E094 File Offset: 0x0005C294
		private bool CaptureOrComplete(ref ExecutionContext cachedContext, bool returnContext)
		{
			if ((this._flags & ContextAwareResult.StateFlags.PostBlockStarted) == ContextAwareResult.StateFlags.None)
			{
				NetEventSource.Fail(this, "Called without calling StartPostingAsyncOp.", "CaptureOrComplete");
			}
			bool flag = base.AsyncCallback != null || (this._flags & ContextAwareResult.StateFlags.CaptureContext) > ContextAwareResult.StateFlags.None;
			if ((this._flags & ContextAwareResult.StateFlags.CaptureIdentity) != ContextAwareResult.StateFlags.None && !base.InternalPeekCompleted && !flag)
			{
				if (NetEventSource.IsEnabled)
				{
					NetEventSource.Info(this, "starting identity capture", "CaptureOrComplete");
				}
				this.SafeCaptureIdentity();
			}
			if (flag && !base.InternalPeekCompleted)
			{
				if (NetEventSource.IsEnabled)
				{
					NetEventSource.Info(this, "starting capture", "CaptureOrComplete");
				}
				if (cachedContext == null)
				{
					cachedContext = ExecutionContext.Capture();
				}
				if (cachedContext != null)
				{
					if (!returnContext)
					{
						this._context = cachedContext;
						cachedContext = null;
					}
					else
					{
						this._context = cachedContext;
					}
				}
				if (NetEventSource.IsEnabled)
				{
					NetEventSource.Info(this, FormattableStringFactory.Create("_context:{0}", new object[] { this._context }), "CaptureOrComplete");
				}
			}
			else
			{
				if (NetEventSource.IsEnabled)
				{
					NetEventSource.Info(this, "Skipping capture", "CaptureOrComplete");
				}
				cachedContext = null;
				if (base.AsyncCallback != null && !base.CompletedSynchronously)
				{
					NetEventSource.Fail(this, "Didn't capture context, but didn't complete synchronously!", "CaptureOrComplete");
				}
			}
			if (base.CompletedSynchronously)
			{
				if (NetEventSource.IsEnabled)
				{
					NetEventSource.Info(this, "Completing synchronously", "CaptureOrComplete");
				}
				base.Complete(IntPtr.Zero);
				return true;
			}
			return false;
		}

		// Token: 0x0600162B RID: 5675 RVA: 0x0005E1E8 File Offset: 0x0005C3E8
		protected override void Complete(IntPtr userToken)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Info(this, FormattableStringFactory.Create("_context(set):{0} userToken:{1}", new object[]
				{
					this._context != null,
					userToken
				}), "Complete");
			}
			if ((this._flags & ContextAwareResult.StateFlags.PostBlockStarted) == ContextAwareResult.StateFlags.None)
			{
				base.Complete(userToken);
				return;
			}
			if (base.CompletedSynchronously)
			{
				return;
			}
			ExecutionContext context = this._context;
			if (userToken != IntPtr.Zero || context == null)
			{
				base.Complete(userToken);
				return;
			}
			ExecutionContext.Run(context, delegate(object s)
			{
				((ContextAwareResult)s).CompleteCallback();
			}, this);
		}

		// Token: 0x0600162C RID: 5676 RVA: 0x0005E295 File Offset: 0x0005C495
		private void CompleteCallback()
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Info(this, "Context set, calling callback.", "CompleteCallback");
			}
			base.Complete(IntPtr.Zero);
		}

		// Token: 0x04000D40 RID: 3392
		private WindowsIdentity _windowsIdentity;

		// Token: 0x04000D41 RID: 3393
		private volatile ExecutionContext _context;

		// Token: 0x04000D42 RID: 3394
		private object _lock;

		// Token: 0x04000D43 RID: 3395
		private ContextAwareResult.StateFlags _flags;

		// Token: 0x0200037A RID: 890
		[Flags]
		private enum StateFlags : byte
		{
			// Token: 0x04000D45 RID: 3397
			None = 0,
			// Token: 0x04000D46 RID: 3398
			CaptureIdentity = 1,
			// Token: 0x04000D47 RID: 3399
			CaptureContext = 2,
			// Token: 0x04000D48 RID: 3400
			ThreadSafeContextCopy = 4,
			// Token: 0x04000D49 RID: 3401
			PostBlockStarted = 8,
			// Token: 0x04000D4A RID: 3402
			PostBlockFinished = 16
		}
	}
}
