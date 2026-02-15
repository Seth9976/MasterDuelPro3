using System;

namespace System.Threading
{
	// Token: 0x02000271 RID: 625
	internal sealed class QueueUserWorkItemCallback : IThreadPoolWorkItem
	{
		// Token: 0x0600170E RID: 5902 RVA: 0x0005A603 File Offset: 0x00058803
		internal QueueUserWorkItemCallback(WaitCallback waitCallback, object stateObj, bool compressStack, ref StackCrawlMark stackMark)
		{
			this.callback = waitCallback;
			this.state = stateObj;
			if (compressStack && !ExecutionContext.IsFlowSuppressed())
			{
				this.context = ExecutionContext.Capture(ref stackMark, ExecutionContext.CaptureOptions.IgnoreSyncCtx | ExecutionContext.CaptureOptions.OptimizeDefaultCase);
			}
		}

		// Token: 0x0600170F RID: 5903 RVA: 0x0005A631 File Offset: 0x00058831
		internal QueueUserWorkItemCallback(WaitCallback waitCallback, object stateObj, ExecutionContext ec)
		{
			this.callback = waitCallback;
			this.state = stateObj;
			this.context = ec;
		}

		// Token: 0x06001710 RID: 5904 RVA: 0x0005A64E File Offset: 0x0005884E
		void IThreadPoolWorkItem.ExecuteWorkItem()
		{
			if (this.context == null)
			{
				WaitCallback waitCallback = this.callback;
				this.callback = null;
				waitCallback(this.state);
				return;
			}
			ExecutionContext.Run(this.context, QueueUserWorkItemCallback.ccb, this, true);
		}

		// Token: 0x06001711 RID: 5905 RVA: 0x00002C89 File Offset: 0x00000E89
		void IThreadPoolWorkItem.MarkAborted(ThreadAbortException tae)
		{
		}

		// Token: 0x06001712 RID: 5906 RVA: 0x0005A684 File Offset: 0x00058884
		private static void WaitCallback_Context(object state)
		{
			QueueUserWorkItemCallback queueUserWorkItemCallback = (QueueUserWorkItemCallback)state;
			queueUserWorkItemCallback.callback(queueUserWorkItemCallback.state);
		}

		// Token: 0x04000B0D RID: 2829
		private WaitCallback callback;

		// Token: 0x04000B0E RID: 2830
		private ExecutionContext context;

		// Token: 0x04000B0F RID: 2831
		private object state;

		// Token: 0x04000B10 RID: 2832
		internal static ContextCallback ccb = new ContextCallback(QueueUserWorkItemCallback.WaitCallback_Context);
	}
}
