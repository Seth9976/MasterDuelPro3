using System;
using System.Threading;
using UnityEngine.Events;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x02000180 RID: 384
	public class UnityEventHandlerAsyncEnumerable<T> : IUniTaskAsyncEnumerable<T>
	{
		// Token: 0x06000933 RID: 2355 RVA: 0x00028F21 File Offset: 0x00027121
		public UnityEventHandlerAsyncEnumerable(UnityEvent<T> unityEvent, CancellationToken cancellationToken)
		{
			this.unityEvent = unityEvent;
			this.cancellationToken1 = cancellationToken;
		}

		// Token: 0x06000934 RID: 2356 RVA: 0x00028F37 File Offset: 0x00027137
		public IUniTaskAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (this.cancellationToken1 == cancellationToken)
			{
				return new UnityEventHandlerAsyncEnumerable<T>.UnityEventHandlerAsyncEnumerator(this.unityEvent, this.cancellationToken1, CancellationToken.None);
			}
			return new UnityEventHandlerAsyncEnumerable<T>.UnityEventHandlerAsyncEnumerator(this.unityEvent, this.cancellationToken1, cancellationToken);
		}

		// Token: 0x040005EC RID: 1516
		private readonly UnityEvent<T> unityEvent;

		// Token: 0x040005ED RID: 1517
		private readonly CancellationToken cancellationToken1;

		// Token: 0x02000181 RID: 385
		private class UnityEventHandlerAsyncEnumerator : MoveNextSource, IUniTaskAsyncEnumerator<T>, IUniTaskAsyncDisposable
		{
			// Token: 0x06000935 RID: 2357 RVA: 0x00028F70 File Offset: 0x00027170
			public UnityEventHandlerAsyncEnumerator(UnityEvent<T> unityEvent, CancellationToken cancellationToken1, CancellationToken cancellationToken2)
			{
				this.unityEvent = unityEvent;
				this.cancellationToken1 = cancellationToken1;
				this.cancellationToken2 = cancellationToken2;
			}

			// Token: 0x17000068 RID: 104
			// (get) Token: 0x06000936 RID: 2358 RVA: 0x00028F8D File Offset: 0x0002718D
			// (set) Token: 0x06000937 RID: 2359 RVA: 0x00028F95 File Offset: 0x00027195
			public T Current { get; private set; }

			// Token: 0x06000938 RID: 2360 RVA: 0x00028FA0 File Offset: 0x000271A0
			public UniTask<bool> MoveNextAsync()
			{
				this.cancellationToken1.ThrowIfCancellationRequested();
				this.cancellationToken2.ThrowIfCancellationRequested();
				this.completionSource.Reset();
				if (this.unityAction == null)
				{
					this.unityAction = new UnityAction<T>(this.Invoke);
					this.unityEvent.AddListener(this.unityAction);
					if (this.cancellationToken1.CanBeCanceled)
					{
						this.registration1 = this.cancellationToken1.RegisterWithoutCaptureExecutionContext(UnityEventHandlerAsyncEnumerable<T>.UnityEventHandlerAsyncEnumerator.cancel1, this);
					}
					if (this.cancellationToken2.CanBeCanceled)
					{
						this.registration2 = this.cancellationToken2.RegisterWithoutCaptureExecutionContext(UnityEventHandlerAsyncEnumerable<T>.UnityEventHandlerAsyncEnumerator.cancel2, this);
					}
				}
				return new UniTask<bool>(this, this.completionSource.Version);
			}

			// Token: 0x06000939 RID: 2361 RVA: 0x00029052 File Offset: 0x00027252
			private void Invoke(T value)
			{
				this.Current = value;
				this.completionSource.TrySetResult(true);
			}

			// Token: 0x0600093A RID: 2362 RVA: 0x00029068 File Offset: 0x00027268
			private static void OnCanceled1(object state)
			{
				UnityEventHandlerAsyncEnumerable<T>.UnityEventHandlerAsyncEnumerator self = (UnityEventHandlerAsyncEnumerable<T>.UnityEventHandlerAsyncEnumerator)state;
				try
				{
					self.completionSource.TrySetCanceled(self.cancellationToken1);
				}
				finally
				{
					self.DisposeAsync().Forget();
				}
			}

			// Token: 0x0600093B RID: 2363 RVA: 0x000290AC File Offset: 0x000272AC
			private static void OnCanceled2(object state)
			{
				UnityEventHandlerAsyncEnumerable<T>.UnityEventHandlerAsyncEnumerator self = (UnityEventHandlerAsyncEnumerable<T>.UnityEventHandlerAsyncEnumerator)state;
				try
				{
					self.completionSource.TrySetCanceled(self.cancellationToken2);
				}
				finally
				{
					self.DisposeAsync().Forget();
				}
			}

			// Token: 0x0600093C RID: 2364 RVA: 0x000290F0 File Offset: 0x000272F0
			public UniTask DisposeAsync()
			{
				if (!this.isDisposed)
				{
					this.isDisposed = true;
					this.registration1.Dispose();
					this.registration2.Dispose();
					IDisposable disp = this.unityEvent as IDisposable;
					if (disp != null)
					{
						disp.Dispose();
					}
					this.unityEvent.RemoveListener(this.unityAction);
					this.completionSource.TrySetCanceled(default(CancellationToken));
				}
				return default(UniTask);
			}

			// Token: 0x040005EE RID: 1518
			private static readonly Action<object> cancel1 = new Action<object>(UnityEventHandlerAsyncEnumerable<T>.UnityEventHandlerAsyncEnumerator.OnCanceled1);

			// Token: 0x040005EF RID: 1519
			private static readonly Action<object> cancel2 = new Action<object>(UnityEventHandlerAsyncEnumerable<T>.UnityEventHandlerAsyncEnumerator.OnCanceled2);

			// Token: 0x040005F0 RID: 1520
			private readonly UnityEvent<T> unityEvent;

			// Token: 0x040005F1 RID: 1521
			private CancellationToken cancellationToken1;

			// Token: 0x040005F2 RID: 1522
			private CancellationToken cancellationToken2;

			// Token: 0x040005F3 RID: 1523
			private UnityAction<T> unityAction;

			// Token: 0x040005F4 RID: 1524
			private CancellationTokenRegistration registration1;

			// Token: 0x040005F5 RID: 1525
			private CancellationTokenRegistration registration2;

			// Token: 0x040005F6 RID: 1526
			private bool isDisposed;
		}
	}
}
