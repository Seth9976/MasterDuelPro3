using System;
using System.Diagnostics;

namespace System.Threading
{
	/// <summary>Represents a synchronization primitive that is signaled when its count reaches zero.</summary>
	// Token: 0x0200022A RID: 554
	[DebuggerDisplay("Initial Count={InitialCount}, Current Count={CurrentCount}")]
	public class CountdownEvent : IDisposable
	{
		/// <summary>Initializes a new instance of <see cref="T:System.Threading.CountdownEvent" /> class with the specified count.</summary>
		/// <param name="initialCount">The number of signals initially required to set the <see cref="T:System.Threading.CountdownEvent" />.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="initialCount" /> is less than 0.</exception>
		// Token: 0x06001490 RID: 5264 RVA: 0x000537D9 File Offset: 0x000519D9
		public CountdownEvent(int initialCount)
		{
			if (initialCount < 0)
			{
				throw new ArgumentOutOfRangeException("initialCount");
			}
			this._initialCount = initialCount;
			this._currentCount = initialCount;
			this._event = new ManualResetEventSlim();
			if (initialCount == 0)
			{
				this._event.Set();
			}
		}

		/// <summary>Gets the number of remaining signals required to set the event.</summary>
		/// <returns> The number of remaining signals required to set the event.</returns>
		// Token: 0x1700021E RID: 542
		// (get) Token: 0x06001491 RID: 5265 RVA: 0x0005381C File Offset: 0x00051A1C
		public int CurrentCount
		{
			get
			{
				int currentCount = this._currentCount;
				if (currentCount >= 0)
				{
					return currentCount;
				}
				return 0;
			}
		}

		/// <summary>Gets the numbers of signals initially required to set the event.</summary>
		/// <returns> The number of signals initially required to set the event.</returns>
		// Token: 0x1700021F RID: 543
		// (get) Token: 0x06001492 RID: 5266 RVA: 0x00053839 File Offset: 0x00051A39
		public int InitialCount
		{
			get
			{
				return this._initialCount;
			}
		}

		/// <summary>Determines whether the event is set.</summary>
		/// <returns>true if the event is set; otherwise, false.</returns>
		// Token: 0x17000220 RID: 544
		// (get) Token: 0x06001493 RID: 5267 RVA: 0x00053841 File Offset: 0x00051A41
		public bool IsSet
		{
			get
			{
				return this._currentCount <= 0;
			}
		}

		/// <summary>Gets a <see cref="T:System.Threading.WaitHandle" /> that is used to wait for the event to be set.</summary>
		/// <returns>A <see cref="T:System.Threading.WaitHandle" /> that is used to wait for the event to be set.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has already been disposed.</exception>
		// Token: 0x17000221 RID: 545
		// (get) Token: 0x06001494 RID: 5268 RVA: 0x00053851 File Offset: 0x00051A51
		public WaitHandle WaitHandle
		{
			get
			{
				this.ThrowIfDisposed();
				return this._event.WaitHandle;
			}
		}

		/// <summary>Releases all resources used by the current instance of the <see cref="T:System.Threading.CountdownEvent" /> class.</summary>
		// Token: 0x06001495 RID: 5269 RVA: 0x00053864 File Offset: 0x00051A64
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Threading.CountdownEvent" />, and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
		// Token: 0x06001496 RID: 5270 RVA: 0x00053873 File Offset: 0x00051A73
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				this._event.Dispose();
				this._disposed = true;
			}
		}

		/// <summary>Registers a signal with the <see cref="T:System.Threading.CountdownEvent" />, decrementing the value of <see cref="P:System.Threading.CountdownEvent.CurrentCount" />.</summary>
		/// <returns>true if the signal caused the count to reach zero and the event was set; otherwise, false.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has already been disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The current instance is already set.</exception>
		// Token: 0x06001497 RID: 5271 RVA: 0x0005388C File Offset: 0x00051A8C
		public bool Signal()
		{
			this.ThrowIfDisposed();
			if (this._currentCount <= 0)
			{
				throw new InvalidOperationException("Invalid attempt made to decrement the event's count below zero.");
			}
			int num = Interlocked.Decrement(ref this._currentCount);
			if (num == 0)
			{
				this._event.Set();
				return true;
			}
			if (num < 0)
			{
				throw new InvalidOperationException("Invalid attempt made to decrement the event's count below zero.");
			}
			return false;
		}

		/// <summary>Registers multiple signals with the <see cref="T:System.Threading.CountdownEvent" />, decrementing the value of <see cref="P:System.Threading.CountdownEvent.CurrentCount" /> by the specified amount.</summary>
		/// <returns>true if the signals caused the count to reach zero and the event was set; otherwise, false.</returns>
		/// <param name="signalCount">The number of signals to register.</param>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="signalCount" /> is less than 1.</exception>
		/// <exception cref="T:System.InvalidOperationException">The current instance is already set. -or- Or <paramref name="signalCount" /> is greater than <see cref="P:System.Threading.CountdownEvent.CurrentCount" />.</exception>
		// Token: 0x06001498 RID: 5272 RVA: 0x000538E4 File Offset: 0x00051AE4
		public bool Signal(int signalCount)
		{
			if (signalCount <= 0)
			{
				throw new ArgumentOutOfRangeException("signalCount");
			}
			this.ThrowIfDisposed();
			SpinWait spinWait = default(SpinWait);
			int currentCount;
			for (;;)
			{
				currentCount = this._currentCount;
				if (currentCount < signalCount)
				{
					break;
				}
				if (Interlocked.CompareExchange(ref this._currentCount, currentCount - signalCount, currentCount) == currentCount)
				{
					goto IL_0050;
				}
				spinWait.SpinOnce();
			}
			throw new InvalidOperationException("Invalid attempt made to decrement the event's count below zero.");
			IL_0050:
			if (currentCount == signalCount)
			{
				this._event.Set();
				return true;
			}
			return false;
		}

		/// <summary>Increments the <see cref="T:System.Threading.CountdownEvent" />'s current count by one.</summary>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has already been disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The current instance is already set.-or-<see cref="P:System.Threading.CountdownEvent.CurrentCount" /> is equal to or greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		// Token: 0x06001499 RID: 5273 RVA: 0x00053953 File Offset: 0x00051B53
		public void AddCount()
		{
			this.AddCount(1);
		}

		/// <summary>Attempts to increment <see cref="P:System.Threading.CountdownEvent.CurrentCount" /> by one.</summary>
		/// <returns>true if the increment succeeded; otherwise, false. If <see cref="P:System.Threading.CountdownEvent.CurrentCount" /> is already at zero, this method will return false.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has already been disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="P:System.Threading.CountdownEvent.CurrentCount" /> is equal to <see cref="F:System.Int32.MaxValue" />.</exception>
		// Token: 0x0600149A RID: 5274 RVA: 0x0005395C File Offset: 0x00051B5C
		public bool TryAddCount()
		{
			return this.TryAddCount(1);
		}

		/// <summary>Increments the <see cref="T:System.Threading.CountdownEvent" />'s current count by a specified value.</summary>
		/// <param name="signalCount">The value by which to increase <see cref="P:System.Threading.CountdownEvent.CurrentCount" />.</param>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="signalCount" /> is less than or equal to 0.</exception>
		/// <exception cref="T:System.InvalidOperationException">The current instance is already set.-or-<see cref="P:System.Threading.CountdownEvent.CurrentCount" /> is equal to or greater than <see cref="F:System.Int32.MaxValue" /> after count is incremented by <paramref name="signalCount." /></exception>
		// Token: 0x0600149B RID: 5275 RVA: 0x00053965 File Offset: 0x00051B65
		public void AddCount(int signalCount)
		{
			if (!this.TryAddCount(signalCount))
			{
				throw new InvalidOperationException("The event is already signaled and cannot be incremented.");
			}
		}

		/// <summary>Attempts to increment <see cref="P:System.Threading.CountdownEvent.CurrentCount" /> by a specified value.</summary>
		/// <returns>true if the increment succeeded; otherwise, false. If <see cref="P:System.Threading.CountdownEvent.CurrentCount" /> is already at zero this will return false.</returns>
		/// <param name="signalCount">The value by which to increase <see cref="P:System.Threading.CountdownEvent.CurrentCount" />.</param>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="signalCount" /> is less than or equal to 0.</exception>
		/// <exception cref="T:System.InvalidOperationException">The current instance is already set.-or-<see cref="P:System.Threading.CountdownEvent.CurrentCount" /> + <paramref name="signalCount" /> is equal to or greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		// Token: 0x0600149C RID: 5276 RVA: 0x0005397C File Offset: 0x00051B7C
		public bool TryAddCount(int signalCount)
		{
			if (signalCount <= 0)
			{
				throw new ArgumentOutOfRangeException("signalCount");
			}
			this.ThrowIfDisposed();
			SpinWait spinWait = default(SpinWait);
			for (;;)
			{
				int currentCount = this._currentCount;
				if (currentCount <= 0)
				{
					break;
				}
				if (currentCount > 2147483647 - signalCount)
				{
					goto Block_3;
				}
				if (Interlocked.CompareExchange(ref this._currentCount, currentCount + signalCount, currentCount) == currentCount)
				{
					return true;
				}
				spinWait.SpinOnce();
			}
			return false;
			Block_3:
			throw new InvalidOperationException("The increment operation would cause the CurrentCount to overflow.");
		}

		/// <summary>Resets the <see cref="P:System.Threading.CountdownEvent.CurrentCount" /> to the value of <see cref="P:System.Threading.CountdownEvent.InitialCount" />.</summary>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has already been disposed..</exception>
		// Token: 0x0600149D RID: 5277 RVA: 0x000539E6 File Offset: 0x00051BE6
		public void Reset()
		{
			this.Reset(this._initialCount);
		}

		/// <summary>Resets the <see cref="P:System.Threading.CountdownEvent.InitialCount" /> property to a specified value.</summary>
		/// <param name="count">The number of signals required to set the <see cref="T:System.Threading.CountdownEvent" />.</param>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has alread been disposed.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="count" /> is less than 0.</exception>
		// Token: 0x0600149E RID: 5278 RVA: 0x000539F4 File Offset: 0x00051BF4
		public void Reset(int count)
		{
			this.ThrowIfDisposed();
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			this._currentCount = count;
			this._initialCount = count;
			if (count == 0)
			{
				this._event.Set();
				return;
			}
			this._event.Reset();
		}

		/// <summary>Blocks the current thread until the <see cref="T:System.Threading.CountdownEvent" /> is set.</summary>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has already been disposed.</exception>
		// Token: 0x0600149F RID: 5279 RVA: 0x00053A40 File Offset: 0x00051C40
		public void Wait()
		{
			this.Wait(-1, default(CancellationToken));
		}

		/// <summary>Blocks the current thread until the <see cref="T:System.Threading.CountdownEvent" /> is set, while observing a <see cref="T:System.Threading.CancellationToken" />.</summary>
		/// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> to observe.</param>
		/// <exception cref="T:System.OperationCanceledException">
		///   <paramref name="cancellationToken" /> has been canceled.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has already been disposed. -or- The <see cref="T:System.Threading.CancellationTokenSource" /> that created <paramref name="cancellationToken" /> has already been disposed.</exception>
		// Token: 0x060014A0 RID: 5280 RVA: 0x00053A5E File Offset: 0x00051C5E
		public void Wait(CancellationToken cancellationToken)
		{
			this.Wait(-1, cancellationToken);
		}

		/// <summary>Blocks the current thread until the <see cref="T:System.Threading.CountdownEvent" /> is set, using a <see cref="T:System.TimeSpan" /> to measure the timeout.</summary>
		/// <returns>true if the <see cref="T:System.Threading.CountdownEvent" /> was set; otherwise, false.</returns>
		/// <param name="timeout">A <see cref="T:System.TimeSpan" /> that represents the number of milliseconds to wait, or a <see cref="T:System.TimeSpan" /> that represents -1 milliseconds to wait indefinitely.</param>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="timeout" /> is a negative number other than -1 milliseconds, which represents an infinite time-out -or- timeout is greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		// Token: 0x060014A1 RID: 5281 RVA: 0x00053A6C File Offset: 0x00051C6C
		public bool Wait(TimeSpan timeout)
		{
			long num = (long)timeout.TotalMilliseconds;
			if (num < -1L || num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("timeout");
			}
			return this.Wait((int)num, default(CancellationToken));
		}

		/// <summary>Blocks the current thread until the <see cref="T:System.Threading.CountdownEvent" /> is set, using a <see cref="T:System.TimeSpan" /> to measure the timeout, while observing a <see cref="T:System.Threading.CancellationToken" />.</summary>
		/// <returns>true if the <see cref="T:System.Threading.CountdownEvent" /> was set; otherwise, false.</returns>
		/// <param name="timeout">A <see cref="T:System.TimeSpan" /> that represents the number of milliseconds to wait, or a <see cref="T:System.TimeSpan" /> that represents -1 milliseconds to wait indefinitely.</param>
		/// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> to observe.</param>
		/// <exception cref="T:System.OperationCanceledException">
		///   <paramref name="cancellationToken" /> has been canceled.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has already been disposed. -or- The <see cref="T:System.Threading.CancellationTokenSource" /> that created <paramref name="cancellationToken" /> has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="timeout" /> is a negative number other than -1 milliseconds, which represents an infinite time-out -or- timeout is greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		// Token: 0x060014A2 RID: 5282 RVA: 0x00053AAC File Offset: 0x00051CAC
		public bool Wait(TimeSpan timeout, CancellationToken cancellationToken)
		{
			long num = (long)timeout.TotalMilliseconds;
			if (num < -1L || num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("timeout");
			}
			return this.Wait((int)num, cancellationToken);
		}

		/// <summary>Blocks the current thread until the <see cref="T:System.Threading.CountdownEvent" /> is set, using a 32-bit signed integer to measure the timeout.</summary>
		/// <returns>true if the <see cref="T:System.Threading.CountdownEvent" /> was set; otherwise, false.</returns>
		/// <param name="millisecondsTimeout">The number of milliseconds to wait, or <see cref="F:System.Threading.Timeout.Infinite" />(-1) to wait indefinitely.</param>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="millisecondsTimeout" /> is a negative number other than -1, which represents an infinite time-out.</exception>
		// Token: 0x060014A3 RID: 5283 RVA: 0x00053AE4 File Offset: 0x00051CE4
		public bool Wait(int millisecondsTimeout)
		{
			return this.Wait(millisecondsTimeout, default(CancellationToken));
		}

		/// <summary>Blocks the current thread until the <see cref="T:System.Threading.CountdownEvent" /> is set, using a 32-bit signed integer to measure the timeout, while observing a <see cref="T:System.Threading.CancellationToken" />.</summary>
		/// <returns>true if the <see cref="T:System.Threading.CountdownEvent" /> was set; otherwise, false.</returns>
		/// <param name="millisecondsTimeout">The number of milliseconds to wait, or <see cref="F:System.Threading.Timeout.Infinite" />(-1) to wait indefinitely.</param>
		/// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> to observe.</param>
		/// <exception cref="T:System.OperationCanceledException">
		///   <paramref name="cancellationToken" /> has been canceled.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has already been disposed. -or- The <see cref="T:System.Threading.CancellationTokenSource" /> that created <paramref name="cancellationToken" /> has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="millisecondsTimeout" /> is a negative number other than -1, which represents an infinite time-out.</exception>
		// Token: 0x060014A4 RID: 5284 RVA: 0x00053B04 File Offset: 0x00051D04
		public bool Wait(int millisecondsTimeout, CancellationToken cancellationToken)
		{
			if (millisecondsTimeout < -1)
			{
				throw new ArgumentOutOfRangeException("millisecondsTimeout");
			}
			this.ThrowIfDisposed();
			cancellationToken.ThrowIfCancellationRequested();
			bool flag = this.IsSet;
			if (!flag)
			{
				flag = this._event.Wait(millisecondsTimeout, cancellationToken);
			}
			return flag;
		}

		// Token: 0x060014A5 RID: 5285 RVA: 0x00053B46 File Offset: 0x00051D46
		private void ThrowIfDisposed()
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException("CountdownEvent");
			}
		}

		// Token: 0x04000A1C RID: 2588
		private int _initialCount;

		// Token: 0x04000A1D RID: 2589
		private volatile int _currentCount;

		// Token: 0x04000A1E RID: 2590
		private ManualResetEventSlim _event;

		// Token: 0x04000A1F RID: 2591
		private volatile bool _disposed;
	}
}
