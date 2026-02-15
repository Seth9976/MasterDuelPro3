using System;
using System.Runtime.ExceptionServices;

namespace Cysharp.Threading.Tasks.Internal
{
	// Token: 0x02000255 RID: 597
	internal sealed class AsyncSubject<T> : IObservable<T>, IObserver<T>
	{
		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000D53 RID: 3411 RVA: 0x0002E60B File Offset: 0x0002C80B
		public T Value
		{
			get
			{
				this.ThrowIfDisposed();
				if (!this.isStopped)
				{
					throw new InvalidOperationException("AsyncSubject is not completed yet");
				}
				if (this.lastError != null)
				{
					ExceptionDispatchInfo.Capture(this.lastError).Throw();
				}
				return this.lastValue;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000D54 RID: 3412 RVA: 0x0002E644 File Offset: 0x0002C844
		public bool HasObservers
		{
			get
			{
				return !(this.outObserver is EmptyObserver<T>) && !this.isStopped && !this.isDisposed;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000D55 RID: 3413 RVA: 0x0002E666 File Offset: 0x0002C866
		public bool IsCompleted
		{
			get
			{
				return this.isStopped;
			}
		}

		// Token: 0x06000D56 RID: 3414 RVA: 0x0002E670 File Offset: 0x0002C870
		public void OnCompleted()
		{
			object obj = this.observerLock;
			IObserver<T> old;
			T v;
			bool hv;
			lock (obj)
			{
				this.ThrowIfDisposed();
				if (this.isStopped)
				{
					return;
				}
				old = this.outObserver;
				this.outObserver = EmptyObserver<T>.Instance;
				this.isStopped = true;
				v = this.lastValue;
				hv = this.hasValue;
			}
			if (hv)
			{
				old.OnNext(v);
				old.OnCompleted();
				return;
			}
			old.OnCompleted();
		}

		// Token: 0x06000D57 RID: 3415 RVA: 0x0002E6FC File Offset: 0x0002C8FC
		public void OnError(Exception error)
		{
			if (error == null)
			{
				throw new ArgumentNullException("error");
			}
			object obj = this.observerLock;
			IObserver<T> old;
			lock (obj)
			{
				this.ThrowIfDisposed();
				if (this.isStopped)
				{
					return;
				}
				old = this.outObserver;
				this.outObserver = EmptyObserver<T>.Instance;
				this.isStopped = true;
				this.lastError = error;
			}
			old.OnError(error);
		}

		// Token: 0x06000D58 RID: 3416 RVA: 0x0002E77C File Offset: 0x0002C97C
		public void OnNext(T value)
		{
			object obj = this.observerLock;
			lock (obj)
			{
				this.ThrowIfDisposed();
				if (!this.isStopped)
				{
					this.hasValue = true;
					this.lastValue = value;
				}
			}
		}

		// Token: 0x06000D59 RID: 3417 RVA: 0x0002E7D4 File Offset: 0x0002C9D4
		public IDisposable Subscribe(IObserver<T> observer)
		{
			if (observer == null)
			{
				throw new ArgumentNullException("observer");
			}
			Exception ex = null;
			T v = default(T);
			bool hv = false;
			object obj = this.observerLock;
			lock (obj)
			{
				this.ThrowIfDisposed();
				if (!this.isStopped)
				{
					ListObserver<T> listObserver = this.outObserver as ListObserver<T>;
					if (listObserver != null)
					{
						this.outObserver = listObserver.Add(observer);
					}
					else
					{
						IObserver<T> current = this.outObserver;
						if (current is EmptyObserver<T>)
						{
							this.outObserver = observer;
						}
						else
						{
							this.outObserver = new ListObserver<T>(new ImmutableList<IObserver<T>>(new IObserver<T>[] { current, observer }));
						}
					}
					return new AsyncSubject<T>.Subscription(this, observer);
				}
				ex = this.lastError;
				v = this.lastValue;
				hv = this.hasValue;
			}
			if (ex != null)
			{
				observer.OnError(ex);
			}
			else if (hv)
			{
				observer.OnNext(v);
				observer.OnCompleted();
			}
			else
			{
				observer.OnCompleted();
			}
			return EmptyDisposable.Instance;
		}

		// Token: 0x06000D5A RID: 3418 RVA: 0x0002E8E0 File Offset: 0x0002CAE0
		public void Dispose()
		{
			object obj = this.observerLock;
			lock (obj)
			{
				this.isDisposed = true;
				this.outObserver = DisposedObserver<T>.Instance;
				this.lastError = null;
				this.lastValue = default(T);
			}
		}

		// Token: 0x06000D5B RID: 3419 RVA: 0x0002E940 File Offset: 0x0002CB40
		private void ThrowIfDisposed()
		{
			if (this.isDisposed)
			{
				throw new ObjectDisposedException("");
			}
		}

		// Token: 0x040006B9 RID: 1721
		private object observerLock = new object();

		// Token: 0x040006BA RID: 1722
		private T lastValue;

		// Token: 0x040006BB RID: 1723
		private bool hasValue;

		// Token: 0x040006BC RID: 1724
		private bool isStopped;

		// Token: 0x040006BD RID: 1725
		private bool isDisposed;

		// Token: 0x040006BE RID: 1726
		private Exception lastError;

		// Token: 0x040006BF RID: 1727
		private IObserver<T> outObserver = EmptyObserver<T>.Instance;

		// Token: 0x02000256 RID: 598
		private class Subscription : IDisposable
		{
			// Token: 0x06000D5D RID: 3421 RVA: 0x0002E973 File Offset: 0x0002CB73
			public Subscription(AsyncSubject<T> parent, IObserver<T> unsubscribeTarget)
			{
				this.parent = parent;
				this.unsubscribeTarget = unsubscribeTarget;
			}

			// Token: 0x06000D5E RID: 3422 RVA: 0x0002E994 File Offset: 0x0002CB94
			public void Dispose()
			{
				object obj = this.gate;
				lock (obj)
				{
					if (this.parent != null)
					{
						object observerLock = this.parent.observerLock;
						lock (observerLock)
						{
							ListObserver<T> listObserver = this.parent.outObserver as ListObserver<T>;
							if (listObserver != null)
							{
								this.parent.outObserver = listObserver.Remove(this.unsubscribeTarget);
							}
							else
							{
								this.parent.outObserver = EmptyObserver<T>.Instance;
							}
							this.unsubscribeTarget = null;
							this.parent = null;
						}
					}
				}
			}

			// Token: 0x040006C0 RID: 1728
			private readonly object gate = new object();

			// Token: 0x040006C1 RID: 1729
			private AsyncSubject<T> parent;

			// Token: 0x040006C2 RID: 1730
			private IObserver<T> unsubscribeTarget;
		}
	}
}
