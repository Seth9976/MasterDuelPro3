using System;
using System.Threading;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x02000191 RID: 401
	public abstract class AsyncTriggerBase<T> : MonoBehaviour, IUniTaskAsyncEnumerable<T>
	{
		// Token: 0x06000A11 RID: 2577 RVA: 0x0002A946 File Offset: 0x00028B46
		private void Awake()
		{
			this.calledAwake = true;
		}

		// Token: 0x06000A12 RID: 2578 RVA: 0x0002A94F File Offset: 0x00028B4F
		private void OnDestroy()
		{
			if (this.calledDestroy)
			{
				return;
			}
			this.calledDestroy = true;
			this.triggerEvent.SetCompleted();
		}

		// Token: 0x06000A13 RID: 2579 RVA: 0x0002A96C File Offset: 0x00028B6C
		internal void AddHandler(ITriggerHandler<T> handler)
		{
			if (!this.calledAwake)
			{
				PlayerLoopHelper.AddAction(PlayerLoopTiming.Update, new AsyncTriggerBase<T>.AwakeMonitor(this));
			}
			this.triggerEvent.Add(handler);
		}

		// Token: 0x06000A14 RID: 2580 RVA: 0x0002A98E File Offset: 0x00028B8E
		internal void RemoveHandler(ITriggerHandler<T> handler)
		{
			if (!this.calledAwake)
			{
				PlayerLoopHelper.AddAction(PlayerLoopTiming.Update, new AsyncTriggerBase<T>.AwakeMonitor(this));
			}
			this.triggerEvent.Remove(handler);
		}

		// Token: 0x06000A15 RID: 2581 RVA: 0x0002A9B0 File Offset: 0x00028BB0
		protected void RaiseEvent(T value)
		{
			this.triggerEvent.SetResult(value);
		}

		// Token: 0x06000A16 RID: 2582 RVA: 0x0002A9BE File Offset: 0x00028BBE
		public IUniTaskAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default(CancellationToken))
		{
			return new AsyncTriggerBase<T>.AsyncTriggerEnumerator(this, cancellationToken);
		}

		// Token: 0x0400063F RID: 1599
		private TriggerEvent<T> triggerEvent;

		// Token: 0x04000640 RID: 1600
		protected internal bool calledAwake;

		// Token: 0x04000641 RID: 1601
		protected internal bool calledDestroy;

		// Token: 0x02000192 RID: 402
		private sealed class AsyncTriggerEnumerator : MoveNextSource, IUniTaskAsyncEnumerator<T>, IUniTaskAsyncDisposable, ITriggerHandler<T>
		{
			// Token: 0x06000A18 RID: 2584 RVA: 0x0002A9C7 File Offset: 0x00028BC7
			public AsyncTriggerEnumerator(AsyncTriggerBase<T> parent, CancellationToken cancellationToken)
			{
				this.parent = parent;
				this.cancellationToken = cancellationToken;
			}

			// Token: 0x06000A19 RID: 2585 RVA: 0x0002A9DD File Offset: 0x00028BDD
			public void OnCanceled(CancellationToken cancellationToken = default(CancellationToken))
			{
				this.completionSource.TrySetCanceled(cancellationToken);
			}

			// Token: 0x06000A1A RID: 2586 RVA: 0x0002A9EC File Offset: 0x00028BEC
			public void OnNext(T value)
			{
				this.Current = value;
				this.completionSource.TrySetResult(true);
			}

			// Token: 0x06000A1B RID: 2587 RVA: 0x00002993 File Offset: 0x00000B93
			public void OnCompleted()
			{
				this.completionSource.TrySetResult(false);
			}

			// Token: 0x06000A1C RID: 2588 RVA: 0x000029A2 File Offset: 0x00000BA2
			public void OnError(Exception ex)
			{
				this.completionSource.TrySetException(ex);
			}

			// Token: 0x06000A1D RID: 2589 RVA: 0x0002AA04 File Offset: 0x00028C04
			private static void CancellationCallback(object state)
			{
				AsyncTriggerBase<T>.AsyncTriggerEnumerator self = (AsyncTriggerBase<T>.AsyncTriggerEnumerator)state;
				self.DisposeAsync().Forget();
				self.completionSource.TrySetCanceled(self.cancellationToken);
			}

			// Token: 0x17000071 RID: 113
			// (get) Token: 0x06000A1E RID: 2590 RVA: 0x0002AA35 File Offset: 0x00028C35
			// (set) Token: 0x06000A1F RID: 2591 RVA: 0x0002AA3D File Offset: 0x00028C3D
			public T Current { get; private set; }

			// Token: 0x17000072 RID: 114
			// (get) Token: 0x06000A20 RID: 2592 RVA: 0x0002AA46 File Offset: 0x00028C46
			// (set) Token: 0x06000A21 RID: 2593 RVA: 0x0002AA4E File Offset: 0x00028C4E
			ITriggerHandler<T> ITriggerHandler<T>.Prev { get; set; }

			// Token: 0x17000073 RID: 115
			// (get) Token: 0x06000A22 RID: 2594 RVA: 0x0002AA57 File Offset: 0x00028C57
			// (set) Token: 0x06000A23 RID: 2595 RVA: 0x0002AA5F File Offset: 0x00028C5F
			ITriggerHandler<T> ITriggerHandler<T>.Next { get; set; }

			// Token: 0x06000A24 RID: 2596 RVA: 0x0002AA68 File Offset: 0x00028C68
			public UniTask<bool> MoveNextAsync()
			{
				this.cancellationToken.ThrowIfCancellationRequested();
				this.completionSource.Reset();
				if (!this.called)
				{
					this.called = true;
					this.parent.AddHandler(this);
					if (this.cancellationToken.CanBeCanceled)
					{
						this.registration = this.cancellationToken.RegisterWithoutCaptureExecutionContext(AsyncTriggerBase<T>.AsyncTriggerEnumerator.cancellationCallback, this);
					}
				}
				return new UniTask<bool>(this, this.completionSource.Version);
			}

			// Token: 0x06000A25 RID: 2597 RVA: 0x0002AADC File Offset: 0x00028CDC
			public UniTask DisposeAsync()
			{
				if (!this.isDisposed)
				{
					this.isDisposed = true;
					this.registration.Dispose();
					this.parent.RemoveHandler(this);
				}
				return default(UniTask);
			}

			// Token: 0x04000642 RID: 1602
			private static Action<object> cancellationCallback = new Action<object>(AsyncTriggerBase<T>.AsyncTriggerEnumerator.CancellationCallback);

			// Token: 0x04000643 RID: 1603
			private readonly AsyncTriggerBase<T> parent;

			// Token: 0x04000644 RID: 1604
			private CancellationToken cancellationToken;

			// Token: 0x04000645 RID: 1605
			private CancellationTokenRegistration registration;

			// Token: 0x04000646 RID: 1606
			private bool called;

			// Token: 0x04000647 RID: 1607
			private bool isDisposed;
		}

		// Token: 0x02000193 RID: 403
		private class AwakeMonitor : IPlayerLoopItem
		{
			// Token: 0x06000A27 RID: 2599 RVA: 0x0002AB2B File Offset: 0x00028D2B
			public AwakeMonitor(AsyncTriggerBase<T> trigger)
			{
				this.trigger = trigger;
			}

			// Token: 0x06000A28 RID: 2600 RVA: 0x0002AB3A File Offset: 0x00028D3A
			public bool MoveNext()
			{
				if (this.trigger.calledAwake)
				{
					return false;
				}
				if (this.trigger == null)
				{
					this.trigger.OnDestroy();
					return false;
				}
				return true;
			}

			// Token: 0x0400064B RID: 1611
			private readonly AsyncTriggerBase<T> trigger;
		}
	}
}
