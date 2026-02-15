using System;
using System.Threading;
using UnityEngine.Events;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x0200017E RID: 382
	public class UnityEventHandlerAsyncEnumerable : IUniTaskAsyncEnumerable<AsyncUnit>
	{
		// Token: 0x06000929 RID: 2345 RVA: 0x00028CCC File Offset: 0x00026ECC
		public UnityEventHandlerAsyncEnumerable(UnityEvent unityEvent, CancellationToken cancellationToken)
		{
			this.unityEvent = unityEvent;
			this.cancellationToken1 = cancellationToken;
		}

		// Token: 0x0600092A RID: 2346 RVA: 0x00028CE2 File Offset: 0x00026EE2
		public IUniTaskAsyncEnumerator<AsyncUnit> GetAsyncEnumerator(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (this.cancellationToken1 == cancellationToken)
			{
				return new UnityEventHandlerAsyncEnumerable.UnityEventHandlerAsyncEnumerator(this.unityEvent, this.cancellationToken1, CancellationToken.None);
			}
			return new UnityEventHandlerAsyncEnumerable.UnityEventHandlerAsyncEnumerator(this.unityEvent, this.cancellationToken1, cancellationToken);
		}

		// Token: 0x040005E1 RID: 1505
		private readonly UnityEvent unityEvent;

		// Token: 0x040005E2 RID: 1506
		private readonly CancellationToken cancellationToken1;

		// Token: 0x0200017F RID: 383
		private class UnityEventHandlerAsyncEnumerator : MoveNextSource, IUniTaskAsyncEnumerator<AsyncUnit>, IUniTaskAsyncDisposable
		{
			// Token: 0x0600092B RID: 2347 RVA: 0x00028D1B File Offset: 0x00026F1B
			public UnityEventHandlerAsyncEnumerator(UnityEvent unityEvent, CancellationToken cancellationToken1, CancellationToken cancellationToken2)
			{
				this.unityEvent = unityEvent;
				this.cancellationToken1 = cancellationToken1;
				this.cancellationToken2 = cancellationToken2;
			}

			// Token: 0x17000067 RID: 103
			// (get) Token: 0x0600092C RID: 2348 RVA: 0x00028D38 File Offset: 0x00026F38
			public AsyncUnit Current
			{
				get
				{
					return default(AsyncUnit);
				}
			}

			// Token: 0x0600092D RID: 2349 RVA: 0x00028D50 File Offset: 0x00026F50
			public UniTask<bool> MoveNextAsync()
			{
				this.cancellationToken1.ThrowIfCancellationRequested();
				this.cancellationToken2.ThrowIfCancellationRequested();
				this.completionSource.Reset();
				if (this.unityAction == null)
				{
					this.unityAction = new UnityAction(this.Invoke);
					this.unityEvent.AddListener(this.unityAction);
					if (this.cancellationToken1.CanBeCanceled)
					{
						this.registration1 = this.cancellationToken1.RegisterWithoutCaptureExecutionContext(UnityEventHandlerAsyncEnumerable.UnityEventHandlerAsyncEnumerator.cancel1, this);
					}
					if (this.cancellationToken2.CanBeCanceled)
					{
						this.registration2 = this.cancellationToken2.RegisterWithoutCaptureExecutionContext(UnityEventHandlerAsyncEnumerable.UnityEventHandlerAsyncEnumerator.cancel2, this);
					}
				}
				return new UniTask<bool>(this, this.completionSource.Version);
			}

			// Token: 0x0600092E RID: 2350 RVA: 0x00028E02 File Offset: 0x00027002
			private void Invoke()
			{
				this.completionSource.TrySetResult(true);
			}

			// Token: 0x0600092F RID: 2351 RVA: 0x00028E14 File Offset: 0x00027014
			private static void OnCanceled1(object state)
			{
				UnityEventHandlerAsyncEnumerable.UnityEventHandlerAsyncEnumerator self = (UnityEventHandlerAsyncEnumerable.UnityEventHandlerAsyncEnumerator)state;
				try
				{
					self.completionSource.TrySetCanceled(self.cancellationToken1);
				}
				finally
				{
					self.DisposeAsync().Forget();
				}
			}

			// Token: 0x06000930 RID: 2352 RVA: 0x00028E58 File Offset: 0x00027058
			private static void OnCanceled2(object state)
			{
				UnityEventHandlerAsyncEnumerable.UnityEventHandlerAsyncEnumerator self = (UnityEventHandlerAsyncEnumerable.UnityEventHandlerAsyncEnumerator)state;
				try
				{
					self.completionSource.TrySetCanceled(self.cancellationToken2);
				}
				finally
				{
					self.DisposeAsync().Forget();
				}
			}

			// Token: 0x06000931 RID: 2353 RVA: 0x00028E9C File Offset: 0x0002709C
			public UniTask DisposeAsync()
			{
				if (!this.isDisposed)
				{
					this.isDisposed = true;
					this.registration1.Dispose();
					this.registration2.Dispose();
					this.unityEvent.RemoveListener(this.unityAction);
					this.completionSource.TrySetCanceled(default(CancellationToken));
				}
				return default(UniTask);
			}

			// Token: 0x040005E3 RID: 1507
			private static readonly Action<object> cancel1 = new Action<object>(UnityEventHandlerAsyncEnumerable.UnityEventHandlerAsyncEnumerator.OnCanceled1);

			// Token: 0x040005E4 RID: 1508
			private static readonly Action<object> cancel2 = new Action<object>(UnityEventHandlerAsyncEnumerable.UnityEventHandlerAsyncEnumerator.OnCanceled2);

			// Token: 0x040005E5 RID: 1509
			private readonly UnityEvent unityEvent;

			// Token: 0x040005E6 RID: 1510
			private CancellationToken cancellationToken1;

			// Token: 0x040005E7 RID: 1511
			private CancellationToken cancellationToken2;

			// Token: 0x040005E8 RID: 1512
			private UnityAction unityAction;

			// Token: 0x040005E9 RID: 1513
			private CancellationTokenRegistration registration1;

			// Token: 0x040005EA RID: 1514
			private CancellationTokenRegistration registration2;

			// Token: 0x040005EB RID: 1515
			private bool isDisposed;
		}
	}
}
