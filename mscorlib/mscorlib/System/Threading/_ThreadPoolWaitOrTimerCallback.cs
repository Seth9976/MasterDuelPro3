using System;

namespace System.Threading
{
	// Token: 0x02000272 RID: 626
	internal class _ThreadPoolWaitOrTimerCallback
	{
		// Token: 0x06001715 RID: 5909 RVA: 0x0005A6E0 File Offset: 0x000588E0
		internal _ThreadPoolWaitOrTimerCallback(WaitOrTimerCallback waitOrTimerCallback, object state, bool compressStack, ref StackCrawlMark stackMark)
		{
			this._waitOrTimerCallback = waitOrTimerCallback;
			this._state = state;
			if (compressStack && !ExecutionContext.IsFlowSuppressed())
			{
				this._executionContext = ExecutionContext.Capture(ref stackMark, ExecutionContext.CaptureOptions.IgnoreSyncCtx | ExecutionContext.CaptureOptions.OptimizeDefaultCase);
			}
		}

		// Token: 0x06001716 RID: 5910 RVA: 0x0005A70E File Offset: 0x0005890E
		private static void WaitOrTimerCallback_Context_t(object state)
		{
			_ThreadPoolWaitOrTimerCallback.WaitOrTimerCallback_Context(state, true);
		}

		// Token: 0x06001717 RID: 5911 RVA: 0x0005A717 File Offset: 0x00058917
		private static void WaitOrTimerCallback_Context_f(object state)
		{
			_ThreadPoolWaitOrTimerCallback.WaitOrTimerCallback_Context(state, false);
		}

		// Token: 0x06001718 RID: 5912 RVA: 0x0005A720 File Offset: 0x00058920
		private static void WaitOrTimerCallback_Context(object state, bool timedOut)
		{
			_ThreadPoolWaitOrTimerCallback threadPoolWaitOrTimerCallback = (_ThreadPoolWaitOrTimerCallback)state;
			threadPoolWaitOrTimerCallback._waitOrTimerCallback(threadPoolWaitOrTimerCallback._state, timedOut);
		}

		// Token: 0x06001719 RID: 5913 RVA: 0x0005A748 File Offset: 0x00058948
		internal static void PerformWaitOrTimerCallback(object state, bool timedOut)
		{
			_ThreadPoolWaitOrTimerCallback threadPoolWaitOrTimerCallback = (_ThreadPoolWaitOrTimerCallback)state;
			if (threadPoolWaitOrTimerCallback._executionContext == null)
			{
				threadPoolWaitOrTimerCallback._waitOrTimerCallback(threadPoolWaitOrTimerCallback._state, timedOut);
				return;
			}
			using (ExecutionContext executionContext = threadPoolWaitOrTimerCallback._executionContext.CreateCopy())
			{
				if (timedOut)
				{
					ExecutionContext.Run(executionContext, _ThreadPoolWaitOrTimerCallback._ccbt, threadPoolWaitOrTimerCallback, true);
				}
				else
				{
					ExecutionContext.Run(executionContext, _ThreadPoolWaitOrTimerCallback._ccbf, threadPoolWaitOrTimerCallback, true);
				}
			}
		}

		// Token: 0x04000B11 RID: 2833
		private WaitOrTimerCallback _waitOrTimerCallback;

		// Token: 0x04000B12 RID: 2834
		private ExecutionContext _executionContext;

		// Token: 0x04000B13 RID: 2835
		private object _state;

		// Token: 0x04000B14 RID: 2836
		private static ContextCallback _ccbt = new ContextCallback(_ThreadPoolWaitOrTimerCallback.WaitOrTimerCallback_Context_t);

		// Token: 0x04000B15 RID: 2837
		private static ContextCallback _ccbf = new ContextCallback(_ThreadPoolWaitOrTimerCallback.WaitOrTimerCallback_Context_f);
	}
}
