using System;
using System.Threading;
using System.Threading.Tasks.Sources;
using UnityEngine.Events;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x0200017D RID: 381
	public class AsyncUnityEventHandler<T> : IUniTaskSource<T>, IUniTaskSource, IValueTaskSource, IValueTaskSource<T>, IDisposable, IAsyncValueChangedEventHandler<T>, IAsyncEndEditEventHandler<T>, IAsyncEndTextSelectionEventHandler<T>, IAsyncTextSelectionEventHandler<T>, IAsyncDeselectEventHandler<T>, IAsyncSelectEventHandler<T>, IAsyncSubmitEventHandler<T>
	{
		// Token: 0x06000917 RID: 2327 RVA: 0x00028B0C File Offset: 0x00026D0C
		public AsyncUnityEventHandler(UnityEvent<T> unityEvent, CancellationToken cancellationToken, bool callOnce)
		{
			this.cancellationToken = cancellationToken;
			if (cancellationToken.IsCancellationRequested)
			{
				this.isDisposed = true;
				return;
			}
			this.action = new UnityAction<T>(this.Invoke);
			this.unityEvent = unityEvent;
			this.callOnce = callOnce;
			unityEvent.AddListener(this.action);
			if (cancellationToken.CanBeCanceled)
			{
				this.registration = cancellationToken.RegisterWithoutCaptureExecutionContext(AsyncUnityEventHandler<T>.cancellationCallback, this);
			}
		}

		// Token: 0x06000918 RID: 2328 RVA: 0x00028B7E File Offset: 0x00026D7E
		public UniTask<T> OnInvokeAsync()
		{
			this.core.Reset();
			if (this.isDisposed)
			{
				this.core.TrySetCanceled(this.cancellationToken);
			}
			return new UniTask<T>(this, this.core.Version);
		}

		// Token: 0x06000919 RID: 2329 RVA: 0x00028BB6 File Offset: 0x00026DB6
		private void Invoke(T result)
		{
			this.core.TrySetResult(result);
		}

		// Token: 0x0600091A RID: 2330 RVA: 0x00028BC5 File Offset: 0x00026DC5
		private static void CancellationCallback(object state)
		{
			((AsyncUnityEventHandler<T>)state).Dispose();
		}

		// Token: 0x0600091B RID: 2331 RVA: 0x00028BD4 File Offset: 0x00026DD4
		public void Dispose()
		{
			if (!this.isDisposed)
			{
				this.isDisposed = true;
				this.registration.Dispose();
				if (this.unityEvent != null)
				{
					IDisposable disp = this.unityEvent as IDisposable;
					if (disp != null)
					{
						disp.Dispose();
					}
					this.unityEvent.RemoveListener(this.action);
				}
				this.core.TrySetCanceled(default(CancellationToken));
			}
		}

		// Token: 0x0600091C RID: 2332 RVA: 0x00028C3E File Offset: 0x00026E3E
		UniTask<T> IAsyncValueChangedEventHandler<T>.OnValueChangedAsync()
		{
			return this.OnInvokeAsync();
		}

		// Token: 0x0600091D RID: 2333 RVA: 0x00028C3E File Offset: 0x00026E3E
		UniTask<T> IAsyncEndEditEventHandler<T>.OnEndEditAsync()
		{
			return this.OnInvokeAsync();
		}

		// Token: 0x0600091E RID: 2334 RVA: 0x00028C3E File Offset: 0x00026E3E
		UniTask<T> IAsyncEndTextSelectionEventHandler<T>.OnEndTextSelectionAsync()
		{
			return this.OnInvokeAsync();
		}

		// Token: 0x0600091F RID: 2335 RVA: 0x00028C3E File Offset: 0x00026E3E
		UniTask<T> IAsyncTextSelectionEventHandler<T>.OnTextSelectionAsync()
		{
			return this.OnInvokeAsync();
		}

		// Token: 0x06000920 RID: 2336 RVA: 0x00028C3E File Offset: 0x00026E3E
		UniTask<T> IAsyncDeselectEventHandler<T>.OnDeselectAsync()
		{
			return this.OnInvokeAsync();
		}

		// Token: 0x06000921 RID: 2337 RVA: 0x00028C3E File Offset: 0x00026E3E
		UniTask<T> IAsyncSelectEventHandler<T>.OnSelectAsync()
		{
			return this.OnInvokeAsync();
		}

		// Token: 0x06000922 RID: 2338 RVA: 0x00028C3E File Offset: 0x00026E3E
		UniTask<T> IAsyncSubmitEventHandler<T>.OnSubmitAsync()
		{
			return this.OnInvokeAsync();
		}

		// Token: 0x06000923 RID: 2339 RVA: 0x00028C48 File Offset: 0x00026E48
		T IUniTaskSource<T>.GetResult(short token)
		{
			T result;
			try
			{
				result = this.core.GetResult(token);
			}
			finally
			{
				if (this.callOnce)
				{
					this.Dispose();
				}
			}
			return result;
		}

		// Token: 0x06000924 RID: 2340 RVA: 0x00028C84 File Offset: 0x00026E84
		void IUniTaskSource.GetResult(short token)
		{
			((IUniTaskSource<T>)this).GetResult(token);
		}

		// Token: 0x06000925 RID: 2341 RVA: 0x00028C8E File Offset: 0x00026E8E
		UniTaskStatus IUniTaskSource.GetStatus(short token)
		{
			return this.core.GetStatus(token);
		}

		// Token: 0x06000926 RID: 2342 RVA: 0x00028C9C File Offset: 0x00026E9C
		UniTaskStatus IUniTaskSource.UnsafeGetStatus()
		{
			return this.core.UnsafeGetStatus();
		}

		// Token: 0x06000927 RID: 2343 RVA: 0x00028CA9 File Offset: 0x00026EA9
		void IUniTaskSource.OnCompleted(Action<object> continuation, object state, short token)
		{
			this.core.OnCompleted(continuation, state, token);
		}

		// Token: 0x040005D9 RID: 1497
		private static Action<object> cancellationCallback = new Action<object>(AsyncUnityEventHandler<T>.CancellationCallback);

		// Token: 0x040005DA RID: 1498
		private readonly UnityAction<T> action;

		// Token: 0x040005DB RID: 1499
		private readonly UnityEvent<T> unityEvent;

		// Token: 0x040005DC RID: 1500
		private CancellationToken cancellationToken;

		// Token: 0x040005DD RID: 1501
		private CancellationTokenRegistration registration;

		// Token: 0x040005DE RID: 1502
		private bool isDisposed;

		// Token: 0x040005DF RID: 1503
		private bool callOnce;

		// Token: 0x040005E0 RID: 1504
		private UniTaskCompletionSourceCore<T> core;
	}
}
