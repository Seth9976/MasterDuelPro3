using System;
using System.Threading;
using UnityEngine;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x02000153 RID: 339
	public static class UniTaskScheduler
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x060007E0 RID: 2016 RVA: 0x00025CAC File Offset: 0x00023EAC
		// (remove) Token: 0x060007E1 RID: 2017 RVA: 0x00025CE0 File Offset: 0x00023EE0
		public static event Action<Exception> UnobservedTaskException;

		// Token: 0x060007E2 RID: 2018 RVA: 0x00025D13 File Offset: 0x00023F13
		private static void InvokeUnobservedTaskException(object state)
		{
			UniTaskScheduler.UnobservedTaskException((Exception)state);
		}

		// Token: 0x060007E3 RID: 2019 RVA: 0x00025D28 File Offset: 0x00023F28
		internal static void PublishUnobservedTaskException(Exception ex)
		{
			if (ex != null)
			{
				if (!UniTaskScheduler.PropagateOperationCanceledException && ex is OperationCanceledException)
				{
					return;
				}
				if (UniTaskScheduler.UnobservedTaskException != null)
				{
					if (!UniTaskScheduler.DispatchUnityMainThread || Thread.CurrentThread.ManagedThreadId == PlayerLoopHelper.MainThreadId)
					{
						UniTaskScheduler.UnobservedTaskException(ex);
						return;
					}
					PlayerLoopHelper.UnitySynchronizationContext.Post(UniTaskScheduler.handleExceptionInvoke, ex);
					return;
				}
				else
				{
					string msg = null;
					if (UniTaskScheduler.UnobservedExceptionWriteLogType != LogType.Exception)
					{
						msg = "UnobservedTaskException: " + ex.ToString();
					}
					switch (UniTaskScheduler.UnobservedExceptionWriteLogType)
					{
					case LogType.Error:
						Debug.LogError(msg);
						return;
					case LogType.Assert:
						break;
					case LogType.Warning:
						Debug.LogWarning(msg);
						return;
					case LogType.Log:
						Debug.Log(msg);
						return;
					case LogType.Exception:
						Debug.LogException(ex);
						break;
					default:
						return;
					}
				}
			}
		}

		// Token: 0x04000543 RID: 1347
		public static bool PropagateOperationCanceledException = false;

		// Token: 0x04000544 RID: 1348
		public static LogType UnobservedExceptionWriteLogType = LogType.Exception;

		// Token: 0x04000545 RID: 1349
		public static bool DispatchUnityMainThread = true;

		// Token: 0x04000546 RID: 1350
		private static readonly SendOrPostCallback handleExceptionInvoke = new SendOrPostCallback(UniTaskScheduler.InvokeUnobservedTaskException);
	}
}
