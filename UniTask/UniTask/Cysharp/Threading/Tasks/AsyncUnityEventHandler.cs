using System;
using System.Threading;
using System.Threading.Tasks.Sources;
using UnityEngine.Events;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x0200017C RID: 380
	public class AsyncUnityEventHandler : IUniTaskSource, IValueTaskSource, IDisposable, IAsyncClickEventHandler
	{
		// Token: 0x0600090C RID: 2316 RVA: 0x00028968 File Offset: 0x00026B68
		public AsyncUnityEventHandler(UnityEvent unityEvent, CancellationToken cancellationToken, bool callOnce)
		{
			this.cancellationToken = cancellationToken;
			if (cancellationToken.IsCancellationRequested)
			{
				this.isDisposed = true;
				return;
			}
			this.action = new UnityAction(this.Invoke);
			this.unityEvent = unityEvent;
			this.callOnce = callOnce;
			unityEvent.AddListener(this.action);
			if (cancellationToken.CanBeCanceled)
			{
				this.registration = cancellationToken.RegisterWithoutCaptureExecutionContext(AsyncUnityEventHandler.cancellationCallback, this);
			}
		}

		// Token: 0x0600090D RID: 2317 RVA: 0x000289DA File Offset: 0x00026BDA
		public UniTask OnInvokeAsync()
		{
			this.core.Reset();
			if (this.isDisposed)
			{
				this.core.TrySetCanceled(this.cancellationToken);
			}
			return new UniTask(this, this.core.Version);
		}

		// Token: 0x0600090E RID: 2318 RVA: 0x00028A12 File Offset: 0x00026C12
		private void Invoke()
		{
			this.core.TrySetResult(AsyncUnit.Default);
		}

		// Token: 0x0600090F RID: 2319 RVA: 0x00028A25 File Offset: 0x00026C25
		private static void CancellationCallback(object state)
		{
			((AsyncUnityEventHandler)state).Dispose();
		}

		// Token: 0x06000910 RID: 2320 RVA: 0x00028A34 File Offset: 0x00026C34
		public void Dispose()
		{
			if (!this.isDisposed)
			{
				this.isDisposed = true;
				this.registration.Dispose();
				if (this.unityEvent != null)
				{
					this.unityEvent.RemoveListener(this.action);
				}
				this.core.TrySetCanceled(this.cancellationToken);
			}
		}

		// Token: 0x06000911 RID: 2321 RVA: 0x00028A86 File Offset: 0x00026C86
		UniTask IAsyncClickEventHandler.OnClickAsync()
		{
			return this.OnInvokeAsync();
		}

		// Token: 0x06000912 RID: 2322 RVA: 0x00028A90 File Offset: 0x00026C90
		void IUniTaskSource.GetResult(short token)
		{
			try
			{
				this.core.GetResult(token);
			}
			finally
			{
				if (this.callOnce)
				{
					this.Dispose();
				}
			}
		}

		// Token: 0x06000913 RID: 2323 RVA: 0x00028ACC File Offset: 0x00026CCC
		UniTaskStatus IUniTaskSource.GetStatus(short token)
		{
			return this.core.GetStatus(token);
		}

		// Token: 0x06000914 RID: 2324 RVA: 0x00028ADA File Offset: 0x00026CDA
		UniTaskStatus IUniTaskSource.UnsafeGetStatus()
		{
			return this.core.UnsafeGetStatus();
		}

		// Token: 0x06000915 RID: 2325 RVA: 0x00028AE7 File Offset: 0x00026CE7
		void IUniTaskSource.OnCompleted(Action<object> continuation, object state, short token)
		{
			this.core.OnCompleted(continuation, state, token);
		}

		// Token: 0x040005D1 RID: 1489
		private static Action<object> cancellationCallback = new Action<object>(AsyncUnityEventHandler.CancellationCallback);

		// Token: 0x040005D2 RID: 1490
		private readonly UnityAction action;

		// Token: 0x040005D3 RID: 1491
		private readonly UnityEvent unityEvent;

		// Token: 0x040005D4 RID: 1492
		private CancellationToken cancellationToken;

		// Token: 0x040005D5 RID: 1493
		private CancellationTokenRegistration registration;

		// Token: 0x040005D6 RID: 1494
		private bool isDisposed;

		// Token: 0x040005D7 RID: 1495
		private bool callOnce;

		// Token: 0x040005D8 RID: 1496
		private UniTaskCompletionSourceCore<AsyncUnit> core;
	}
}
