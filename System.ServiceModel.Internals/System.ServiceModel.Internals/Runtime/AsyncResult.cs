using System;
using System.Threading;

namespace System.Runtime
{
	// Token: 0x0200000C RID: 12
	internal abstract class AsyncResult : IAsyncResult
	{
		// Token: 0x0600001E RID: 30 RVA: 0x000023B2 File Offset: 0x000005B2
		protected AsyncResult(AsyncCallback callback, object state)
		{
			this.callback = callback;
			this.state = state;
			this.thisLock = new object();
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600001F RID: 31 RVA: 0x000023D3 File Offset: 0x000005D3
		public object AsyncState
		{
			get
			{
				return this.state;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000020 RID: 32 RVA: 0x000023DC File Offset: 0x000005DC
		public WaitHandle AsyncWaitHandle
		{
			get
			{
				if (this.manualResetEvent != null)
				{
					return this.manualResetEvent;
				}
				object obj = this.ThisLock;
				lock (obj)
				{
					if (this.manualResetEvent == null)
					{
						this.manualResetEvent = new ManualResetEvent(this.isCompleted);
					}
				}
				return this.manualResetEvent;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000021 RID: 33 RVA: 0x00002444 File Offset: 0x00000644
		public bool CompletedSynchronously
		{
			get
			{
				return this.completedSynchronously;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000022 RID: 34 RVA: 0x0000244C File Offset: 0x0000064C
		public bool IsCompleted
		{
			get
			{
				return this.isCompleted;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000023 RID: 35 RVA: 0x00002454 File Offset: 0x00000654
		protected Action<AsyncResult, Exception> OnCompleting { get; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000024 RID: 36 RVA: 0x0000245C File Offset: 0x0000065C
		private object ThisLock
		{
			get
			{
				return this.thisLock;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00002464 File Offset: 0x00000664
		protected Action<AsyncCallback, IAsyncResult> VirtualCallback { get; }

		// Token: 0x06000026 RID: 38 RVA: 0x0000246C File Offset: 0x0000066C
		protected void Complete(bool completedSynchronously)
		{
			if (this.isCompleted)
			{
				throw Fx.Exception.AsError(new InvalidOperationException(InternalSR.AsyncResultCompletedTwice(base.GetType())));
			}
			this.completedSynchronously = completedSynchronously;
			if (this.OnCompleting != null)
			{
				try
				{
					this.OnCompleting(this, this.exception);
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					this.exception = ex;
				}
			}
			if (completedSynchronously)
			{
				this.isCompleted = true;
			}
			else
			{
				object obj = this.ThisLock;
				lock (obj)
				{
					this.isCompleted = true;
					if (this.manualResetEvent != null)
					{
						this.manualResetEvent.Set();
					}
				}
			}
			if (this.callback != null)
			{
				try
				{
					if (this.VirtualCallback != null)
					{
						this.VirtualCallback(this.callback, this);
					}
					else
					{
						this.callback(this);
					}
				}
				catch (Exception ex2)
				{
					if (Fx.IsFatal(ex2))
					{
						throw;
					}
					throw Fx.Exception.AsError(new CallbackException("Async Callback Threw Exception", ex2));
				}
			}
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002594 File Offset: 0x00000794
		protected void Complete(bool completedSynchronously, Exception exception)
		{
			this.exception = exception;
			this.Complete(completedSynchronously);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000025A4 File Offset: 0x000007A4
		private static void AsyncCompletionWrapperCallback(IAsyncResult result)
		{
			if (result == null)
			{
				throw Fx.Exception.AsError(new InvalidOperationException("Invalid Null Async Result"));
			}
			if (result.CompletedSynchronously)
			{
				return;
			}
			AsyncResult asyncResult = (AsyncResult)result.AsyncState;
			if (!asyncResult.OnContinueAsyncCompletion(result))
			{
				return;
			}
			AsyncResult.AsyncCompletion nextCompletion = asyncResult.GetNextCompletion();
			if (nextCompletion == null)
			{
				AsyncResult.ThrowInvalidAsyncResult(result);
			}
			bool flag = false;
			Exception ex = null;
			try
			{
				flag = nextCompletion(result);
			}
			catch (Exception ex2)
			{
				if (Fx.IsFatal(ex2))
				{
					throw;
				}
				flag = true;
				ex = ex2;
			}
			if (flag)
			{
				asyncResult.Complete(false, ex);
			}
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002630 File Offset: 0x00000830
		protected virtual bool OnContinueAsyncCompletion(IAsyncResult result)
		{
			return true;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002633 File Offset: 0x00000833
		protected AsyncCallback PrepareAsyncCompletion(AsyncResult.AsyncCompletion callback)
		{
			if (this.beforePrepareAsyncCompletionAction != null)
			{
				this.beforePrepareAsyncCompletionAction();
			}
			this.nextAsyncCompletion = callback;
			if (AsyncResult.asyncCompletionWrapperCallback == null)
			{
				AsyncResult.asyncCompletionWrapperCallback = Fx.ThunkCallback(new AsyncCallback(AsyncResult.AsyncCompletionWrapperCallback));
			}
			return AsyncResult.asyncCompletionWrapperCallback;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002674 File Offset: 0x00000874
		protected bool SyncContinue(IAsyncResult result)
		{
			AsyncResult.AsyncCompletion asyncCompletion;
			return this.TryContinueHelper(result, out asyncCompletion) && asyncCompletion(result);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002698 File Offset: 0x00000898
		private bool TryContinueHelper(IAsyncResult result, out AsyncResult.AsyncCompletion callback)
		{
			if (result == null)
			{
				throw Fx.Exception.AsError(new InvalidOperationException("Invalid Null Async Result"));
			}
			callback = null;
			if (this.checkSyncValidationFunc != null)
			{
				if (!this.checkSyncValidationFunc(result))
				{
					return false;
				}
			}
			else if (!result.CompletedSynchronously)
			{
				return false;
			}
			callback = this.GetNextCompletion();
			if (callback == null)
			{
				AsyncResult.ThrowInvalidAsyncResult("Only call Check/SyncContinue once per async operation (once per PrepareAsyncCompletion).");
			}
			return true;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000026F9 File Offset: 0x000008F9
		private AsyncResult.AsyncCompletion GetNextCompletion()
		{
			AsyncResult.AsyncCompletion asyncCompletion = this.nextAsyncCompletion;
			this.nextAsyncCompletion = null;
			return asyncCompletion;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002708 File Offset: 0x00000908
		protected static void ThrowInvalidAsyncResult(IAsyncResult result)
		{
			throw Fx.Exception.AsError(new InvalidOperationException(InternalSR.InvalidAsyncResultImplementation(result.GetType())));
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002724 File Offset: 0x00000924
		protected static void ThrowInvalidAsyncResult(string debugText)
		{
			string text = "Invalid Async Result Implementation Generic";
			throw Fx.Exception.AsError(new InvalidOperationException(text));
		}

		// Token: 0x06000030 RID: 48 RVA: 0x0000274C File Offset: 0x0000094C
		protected static TAsyncResult End<TAsyncResult>(IAsyncResult result) where TAsyncResult : AsyncResult
		{
			if (result == null)
			{
				throw Fx.Exception.ArgumentNull("result");
			}
			TAsyncResult tasyncResult = result as TAsyncResult;
			if (tasyncResult == null)
			{
				throw Fx.Exception.Argument("result", "Invalid Async Result");
			}
			if (tasyncResult.endCalled)
			{
				throw Fx.Exception.AsError(new InvalidOperationException("Async Result Already Ended"));
			}
			tasyncResult.endCalled = true;
			if (!tasyncResult.isCompleted)
			{
				tasyncResult.AsyncWaitHandle.WaitOne();
			}
			if (tasyncResult.manualResetEvent != null)
			{
				tasyncResult.manualResetEvent.Close();
			}
			if (tasyncResult.exception != null)
			{
				throw Fx.Exception.AsError(tasyncResult.exception);
			}
			return tasyncResult;
		}

		// Token: 0x04000018 RID: 24
		private static AsyncCallback asyncCompletionWrapperCallback;

		// Token: 0x04000019 RID: 25
		private AsyncCallback callback;

		// Token: 0x0400001A RID: 26
		private bool completedSynchronously;

		// Token: 0x0400001B RID: 27
		private bool endCalled;

		// Token: 0x0400001C RID: 28
		private Exception exception;

		// Token: 0x0400001D RID: 29
		private bool isCompleted;

		// Token: 0x0400001E RID: 30
		private AsyncResult.AsyncCompletion nextAsyncCompletion;

		// Token: 0x0400001F RID: 31
		private object state;

		// Token: 0x04000020 RID: 32
		private Action beforePrepareAsyncCompletionAction;

		// Token: 0x04000021 RID: 33
		private Func<IAsyncResult, bool> checkSyncValidationFunc;

		// Token: 0x04000022 RID: 34
		private ManualResetEvent manualResetEvent;

		// Token: 0x04000023 RID: 35
		private object thisLock;

		// Token: 0x0200000D RID: 13
		// (Invoke) Token: 0x06000032 RID: 50
		protected delegate bool AsyncCompletion(IAsyncResult result);
	}
}
