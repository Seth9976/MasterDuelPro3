using System;
using System.Threading;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x02000017 RID: 23
	public static class CancellationTokenExtensions
	{
		// Token: 0x06000089 RID: 137 RVA: 0x00003118 File Offset: 0x00001318
		public static CancellationToken ToCancellationToken(this UniTask task)
		{
			CancellationTokenSource cts = new CancellationTokenSource();
			CancellationTokenExtensions.ToCancellationTokenCore(task, cts).Forget();
			return cts.Token;
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00003140 File Offset: 0x00001340
		public static CancellationToken ToCancellationToken(this UniTask task, CancellationToken linkToken)
		{
			if (linkToken.IsCancellationRequested)
			{
				return linkToken;
			}
			if (!linkToken.CanBeCanceled)
			{
				return task.ToCancellationToken();
			}
			CancellationTokenSource cts = CancellationTokenSource.CreateLinkedTokenSource(new CancellationToken[] { linkToken });
			CancellationTokenExtensions.ToCancellationTokenCore(task, cts).Forget();
			return cts.Token;
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00003191 File Offset: 0x00001391
		public static CancellationToken ToCancellationToken<T>(this UniTask<T> task)
		{
			return task.AsUniTask().ToCancellationToken();
		}

		// Token: 0x0600008C RID: 140 RVA: 0x0000319F File Offset: 0x0000139F
		public static CancellationToken ToCancellationToken<T>(this UniTask<T> task, CancellationToken linkToken)
		{
			return task.AsUniTask().ToCancellationToken(linkToken);
		}

		// Token: 0x0600008D RID: 141 RVA: 0x000031B0 File Offset: 0x000013B0
		private static async UniTaskVoid ToCancellationTokenCore(UniTask task, CancellationTokenSource cts)
		{
			try
			{
				await task;
			}
			catch (Exception ex)
			{
				UniTaskScheduler.PublishUnobservedTaskException(ex);
			}
			cts.Cancel();
			cts.Dispose();
		}

		// Token: 0x0600008E RID: 142 RVA: 0x000031FC File Offset: 0x000013FC
		public static ValueTuple<UniTask, CancellationTokenRegistration> ToUniTask(this CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return new ValueTuple<UniTask, CancellationTokenRegistration>(UniTask.FromCanceled(cancellationToken), default(CancellationTokenRegistration));
			}
			UniTaskCompletionSource promise = new UniTaskCompletionSource();
			return new ValueTuple<UniTask, CancellationTokenRegistration>(promise.Task, cancellationToken.RegisterWithoutCaptureExecutionContext(CancellationTokenExtensions.cancellationTokenCallback, promise));
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00003244 File Offset: 0x00001444
		private static void Callback(object state)
		{
			((UniTaskCompletionSource)state).TrySetResult();
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00003252 File Offset: 0x00001452
		public static CancellationTokenAwaitable WaitUntilCanceled(this CancellationToken cancellationToken)
		{
			return new CancellationTokenAwaitable(cancellationToken);
		}

		// Token: 0x06000091 RID: 145 RVA: 0x0000325C File Offset: 0x0000145C
		public static CancellationTokenRegistration RegisterWithoutCaptureExecutionContext(this CancellationToken cancellationToken, Action callback)
		{
			bool restoreFlow = false;
			if (!ExecutionContext.IsFlowSuppressed())
			{
				ExecutionContext.SuppressFlow();
				restoreFlow = true;
			}
			CancellationTokenRegistration cancellationTokenRegistration;
			try
			{
				cancellationTokenRegistration = cancellationToken.Register(callback, false);
			}
			finally
			{
				if (restoreFlow)
				{
					ExecutionContext.RestoreFlow();
				}
			}
			return cancellationTokenRegistration;
		}

		// Token: 0x06000092 RID: 146 RVA: 0x000032A4 File Offset: 0x000014A4
		public static CancellationTokenRegistration RegisterWithoutCaptureExecutionContext(this CancellationToken cancellationToken, Action<object> callback, object state)
		{
			bool restoreFlow = false;
			if (!ExecutionContext.IsFlowSuppressed())
			{
				ExecutionContext.SuppressFlow();
				restoreFlow = true;
			}
			CancellationTokenRegistration cancellationTokenRegistration;
			try
			{
				cancellationTokenRegistration = cancellationToken.Register(callback, state, false);
			}
			finally
			{
				if (restoreFlow)
				{
					ExecutionContext.RestoreFlow();
				}
			}
			return cancellationTokenRegistration;
		}

		// Token: 0x06000093 RID: 147 RVA: 0x000032EC File Offset: 0x000014EC
		public static CancellationTokenRegistration AddTo(this IDisposable disposable, CancellationToken cancellationToken)
		{
			return cancellationToken.RegisterWithoutCaptureExecutionContext(CancellationTokenExtensions.disposeCallback, disposable);
		}

		// Token: 0x06000094 RID: 148 RVA: 0x000032FA File Offset: 0x000014FA
		private static void DisposeCallback(object state)
		{
			((IDisposable)state).Dispose();
		}

		// Token: 0x0400004D RID: 77
		private static readonly Action<object> cancellationTokenCallback = new Action<object>(CancellationTokenExtensions.Callback);

		// Token: 0x0400004E RID: 78
		private static readonly Action<object> disposeCallback = new Action<object>(CancellationTokenExtensions.DisposeCallback);
	}
}
