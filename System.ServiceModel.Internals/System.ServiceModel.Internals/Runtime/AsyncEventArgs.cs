using System;

namespace System.Runtime
{
	// Token: 0x02000007 RID: 7
	internal abstract class AsyncEventArgs : IAsyncEventArgs
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000E RID: 14 RVA: 0x0000229C File Offset: 0x0000049C
		public Exception Exception
		{
			get
			{
				return this.exception;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000F RID: 15 RVA: 0x000022A4 File Offset: 0x000004A4
		public object AsyncState
		{
			get
			{
				return this.asyncState;
			}
		}

		// Token: 0x17000005 RID: 5
		// (set) Token: 0x06000010 RID: 16 RVA: 0x000022AC File Offset: 0x000004AC
		private AsyncEventArgs.OperationState State
		{
			set
			{
				if (value != AsyncEventArgs.OperationState.PendingCompletion)
				{
					if (value - AsyncEventArgs.OperationState.CompletedSynchronously <= 1)
					{
						if (this.state != AsyncEventArgs.OperationState.PendingCompletion)
						{
							throw Fx.Exception.AsError(new InvalidOperationException(InternalSR.AsyncEventArgsCompletedTwice(base.GetType())));
						}
					}
				}
				else if (this.state == AsyncEventArgs.OperationState.PendingCompletion)
				{
					throw Fx.Exception.AsError(new InvalidOperationException(InternalSR.AsyncEventArgsCompletionPending(base.GetType())));
				}
				this.state = value;
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002314 File Offset: 0x00000514
		public void Complete(bool completedSynchronously)
		{
			this.Complete(completedSynchronously, null);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x0000231E File Offset: 0x0000051E
		public virtual void Complete(bool completedSynchronously, Exception exception)
		{
			this.exception = exception;
			if (completedSynchronously)
			{
				this.State = AsyncEventArgs.OperationState.CompletedSynchronously;
				return;
			}
			this.State = AsyncEventArgs.OperationState.CompletedAsynchronously;
			this.callback(this);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002345 File Offset: 0x00000545
		protected void SetAsyncState(AsyncEventArgsCallback callback, object state)
		{
			if (callback == null)
			{
				throw Fx.Exception.ArgumentNull("callback");
			}
			this.State = AsyncEventArgs.OperationState.PendingCompletion;
			this.asyncState = state;
			this.callback = callback;
		}

		// Token: 0x0400000D RID: 13
		private AsyncEventArgs.OperationState state;

		// Token: 0x0400000E RID: 14
		private object asyncState;

		// Token: 0x0400000F RID: 15
		private AsyncEventArgsCallback callback;

		// Token: 0x04000010 RID: 16
		private Exception exception;

		// Token: 0x02000008 RID: 8
		private enum OperationState
		{
			// Token: 0x04000012 RID: 18
			Created,
			// Token: 0x04000013 RID: 19
			PendingCompletion,
			// Token: 0x04000014 RID: 20
			CompletedSynchronously,
			// Token: 0x04000015 RID: 21
			CompletedAsynchronously
		}
	}
}
