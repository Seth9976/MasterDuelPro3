using System;
using System.Diagnostics;

namespace System.Threading
{
	/// <summary>Provides a slimmed down version of <see cref="T:System.Threading.ManualResetEvent" />.</summary>
	// Token: 0x0200022F RID: 559
	[DebuggerDisplay("Set = {IsSet}")]
	public class ManualResetEventSlim : IDisposable
	{
		/// <summary>Gets the underlying <see cref="T:System.Threading.WaitHandle" /> object for this <see cref="T:System.Threading.ManualResetEventSlim" />.</summary>
		/// <returns>The underlying <see cref="T:System.Threading.WaitHandle" /> event object fore this <see cref="T:System.Threading.ManualResetEventSlim" />.</returns>
		// Token: 0x17000226 RID: 550
		// (get) Token: 0x060014C2 RID: 5314 RVA: 0x00053E16 File Offset: 0x00052016
		public WaitHandle WaitHandle
		{
			get
			{
				this.ThrowIfDisposed();
				if (this.m_eventObj == null)
				{
					this.LazyInitializeEvent();
				}
				return this.m_eventObj;
			}
		}

		/// <summary>Gets whether the event is set.</summary>
		/// <returns>true if the event has is set; otherwise, false.</returns>
		// Token: 0x17000227 RID: 551
		// (get) Token: 0x060014C3 RID: 5315 RVA: 0x00053E37 File Offset: 0x00052037
		// (set) Token: 0x060014C4 RID: 5316 RVA: 0x00053E4E File Offset: 0x0005204E
		public bool IsSet
		{
			get
			{
				return ManualResetEventSlim.ExtractStatePortion(this.m_combinedState, int.MinValue) != 0;
			}
			private set
			{
				this.UpdateStateAtomically((value ? 1 : 0) << 31, int.MinValue);
			}
		}

		/// <summary>Gets the number of spin waits that will be occur before falling back to a kernel-based wait operation.</summary>
		/// <returns>Returns the number of spin waits that will be occur before falling back to a kernel-based wait operation.</returns>
		// Token: 0x17000228 RID: 552
		// (get) Token: 0x060014C5 RID: 5317 RVA: 0x00053E65 File Offset: 0x00052065
		// (set) Token: 0x060014C6 RID: 5318 RVA: 0x00053E7B File Offset: 0x0005207B
		public int SpinCount
		{
			get
			{
				return ManualResetEventSlim.ExtractStatePortionAndShiftRight(this.m_combinedState, 1073217536, 19);
			}
			private set
			{
				this.m_combinedState = (this.m_combinedState & -1073217537) | (value << 19);
			}
		}

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x060014C7 RID: 5319 RVA: 0x00053E98 File Offset: 0x00052098
		// (set) Token: 0x060014C8 RID: 5320 RVA: 0x00053EAD File Offset: 0x000520AD
		private int Waiters
		{
			get
			{
				return ManualResetEventSlim.ExtractStatePortionAndShiftRight(this.m_combinedState, 524287, 0);
			}
			set
			{
				if (value >= 524287)
				{
					throw new InvalidOperationException(string.Format("There are too many threads currently waiting on the event. A maximum of {0} waiting threads are supported.", 524287));
				}
				this.UpdateStateAtomically(value, 524287);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.ManualResetEventSlim" /> class with an initial state of nonsignaled.</summary>
		// Token: 0x060014C9 RID: 5321 RVA: 0x00053EDD File Offset: 0x000520DD
		public ManualResetEventSlim()
			: this(false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.ManualResetEventSlim" /> class with a Boolean value indicating whether to set the intial state to signaled.</summary>
		/// <param name="initialState">true to set the initial state signaled; false to set the initial state to nonsignaled.</param>
		// Token: 0x060014CA RID: 5322 RVA: 0x00053EE6 File Offset: 0x000520E6
		public ManualResetEventSlim(bool initialState)
		{
			this.Initialize(initialState, SpinWait.SpinCountforSpinBeforeWait);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.ManualResetEventSlim" /> class with a Boolean value indicating whether to set the intial state to signaled and a specified spin count.</summary>
		/// <param name="initialState">true to set the initial state to signaled; false to set the initial state to nonsignaled.</param>
		/// <param name="spinCount">The number of spin waits that will occur before falling back to a kernel-based wait operation.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="spinCount" /> is less than 0 or greater than the maximum allowed value.</exception>
		// Token: 0x060014CB RID: 5323 RVA: 0x00053EFC File Offset: 0x000520FC
		public ManualResetEventSlim(bool initialState, int spinCount)
		{
			if (spinCount < 0)
			{
				throw new ArgumentOutOfRangeException("spinCount");
			}
			if (spinCount > 2047)
			{
				throw new ArgumentOutOfRangeException("spinCount", string.Format("The spinCount argument must be in the range 0 to {0}, inclusive.", 2047));
			}
			this.Initialize(initialState, spinCount);
		}

		// Token: 0x060014CC RID: 5324 RVA: 0x00053F4D File Offset: 0x0005214D
		private void Initialize(bool initialState, int spinCount)
		{
			this.m_combinedState = (initialState ? int.MinValue : 0);
			this.SpinCount = (PlatformHelper.IsSingleProcessor ? 1 : spinCount);
		}

		// Token: 0x060014CD RID: 5325 RVA: 0x00053F74 File Offset: 0x00052174
		private void EnsureLockObjectCreated()
		{
			if (this.m_lock != null)
			{
				return;
			}
			object obj = new object();
			Interlocked.CompareExchange(ref this.m_lock, obj, null);
		}

		// Token: 0x060014CE RID: 5326 RVA: 0x00053FA0 File Offset: 0x000521A0
		private bool LazyInitializeEvent()
		{
			bool isSet = this.IsSet;
			ManualResetEvent manualResetEvent = new ManualResetEvent(isSet);
			if (Interlocked.CompareExchange<ManualResetEvent>(ref this.m_eventObj, manualResetEvent, null) != null)
			{
				manualResetEvent.Dispose();
				return false;
			}
			if (this.IsSet != isSet)
			{
				ManualResetEvent manualResetEvent2 = manualResetEvent;
				lock (manualResetEvent2)
				{
					if (this.m_eventObj == manualResetEvent)
					{
						manualResetEvent.Set();
					}
				}
			}
			return true;
		}

		/// <summary>Sets the state of the event to signaled, which allows one or more threads waiting on the event to proceed.</summary>
		// Token: 0x060014CF RID: 5327 RVA: 0x00054018 File Offset: 0x00052218
		public void Set()
		{
			this.Set(false);
		}

		// Token: 0x060014D0 RID: 5328 RVA: 0x00054024 File Offset: 0x00052224
		private void Set(bool duringCancellation)
		{
			this.IsSet = true;
			if (this.Waiters > 0)
			{
				object @lock = this.m_lock;
				lock (@lock)
				{
					Monitor.PulseAll(this.m_lock);
				}
			}
			ManualResetEvent eventObj = this.m_eventObj;
			if (eventObj != null && !duringCancellation)
			{
				ManualResetEvent manualResetEvent = eventObj;
				lock (manualResetEvent)
				{
					if (this.m_eventObj != null)
					{
						this.m_eventObj.Set();
					}
				}
			}
		}

		/// <summary>Sets the state of the event to nonsignaled, which causes threads to block.</summary>
		/// <exception cref="T:System.ObjectDisposedException">The object has already been disposed.</exception>
		// Token: 0x060014D1 RID: 5329 RVA: 0x000540C8 File Offset: 0x000522C8
		public void Reset()
		{
			this.ThrowIfDisposed();
			if (this.m_eventObj != null)
			{
				this.m_eventObj.Reset();
			}
			this.IsSet = false;
		}

		/// <summary>Blocks the current thread until the current <see cref="T:System.Threading.ManualResetEventSlim" /> is set.</summary>
		/// <exception cref="T:System.InvalidOperationException">The maximum number of waiters has been exceeded.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has already been disposed.</exception>
		// Token: 0x060014D2 RID: 5330 RVA: 0x000540F0 File Offset: 0x000522F0
		public void Wait()
		{
			this.Wait(-1, default(CancellationToken));
		}

		/// <summary>Blocks the current thread until the current <see cref="T:System.Threading.ManualResetEventSlim" /> receives a signal, while observing a <see cref="T:System.Threading.CancellationToken" />.</summary>
		/// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> to observe.</param>
		/// <exception cref="T:System.InvalidOperationException">The maximum number of waiters has been exceeded.</exception>
		/// <exception cref="T:System.OperationCanceledException">
		///   <paramref name="cancellationToken" /> was canceled.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has already been disposed or the <see cref="T:System.Threading.CancellationTokenSource" /> that created <paramref name="cancellationToken" /> has been disposed.</exception>
		// Token: 0x060014D3 RID: 5331 RVA: 0x0005410E File Offset: 0x0005230E
		public void Wait(CancellationToken cancellationToken)
		{
			this.Wait(-1, cancellationToken);
		}

		/// <summary>Blocks the current thread until the current <see cref="T:System.Threading.ManualResetEventSlim" /> is set, using a <see cref="T:System.TimeSpan" /> to measure the time interval.</summary>
		/// <returns>true if the <see cref="T:System.Threading.ManualResetEventSlim" /> was set; otherwise, false.</returns>
		/// <param name="timeout">A <see cref="T:System.TimeSpan" /> that represents the number of milliseconds to wait, or a <see cref="T:System.TimeSpan" /> that represents -1 milliseconds to wait indefinitely.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="timeout" /> is a negative number other than -1 milliseconds, which represents an infinite time-out -or- timeout is greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The maximum number of waiters has been exceeded.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has already been disposed.</exception>
		// Token: 0x060014D4 RID: 5332 RVA: 0x0005411C File Offset: 0x0005231C
		public bool Wait(TimeSpan timeout)
		{
			long num = (long)timeout.TotalMilliseconds;
			if (num < -1L || num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("timeout");
			}
			return this.Wait((int)num, default(CancellationToken));
		}

		/// <summary>Blocks the current thread until the current <see cref="T:System.Threading.ManualResetEventSlim" /> is set, using a <see cref="T:System.TimeSpan" /> to measure the time interval, while observing a <see cref="T:System.Threading.CancellationToken" />.</summary>
		/// <returns>true if the <see cref="T:System.Threading.ManualResetEventSlim" /> was set; otherwise, false.</returns>
		/// <param name="timeout">A <see cref="T:System.TimeSpan" /> that represents the number of milliseconds to wait, or a <see cref="T:System.TimeSpan" /> that represents -1 milliseconds to wait indefinitely.</param>
		/// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> to observe.</param>
		/// <exception cref="T:System.OperationCanceledException">
		///   <paramref name="cancellationToken" /> was canceled.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="timeout" /> is a negative number other than -1 milliseconds, which represents an infinite time-out -or- timeout is greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The maximum number of waiters has been exceeded. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has already been disposed or the <see cref="T:System.Threading.CancellationTokenSource" /> that created <paramref name="cancellationToken" /> has been disposed.</exception>
		// Token: 0x060014D5 RID: 5333 RVA: 0x0005415C File Offset: 0x0005235C
		public bool Wait(TimeSpan timeout, CancellationToken cancellationToken)
		{
			long num = (long)timeout.TotalMilliseconds;
			if (num < -1L || num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("timeout");
			}
			return this.Wait((int)num, cancellationToken);
		}

		/// <summary>Blocks the current thread until the current <see cref="T:System.Threading.ManualResetEventSlim" /> is set, using a 32-bit signed integer to measure the time interval.</summary>
		/// <returns>true if the <see cref="T:System.Threading.ManualResetEventSlim" /> was set; otherwise, false.</returns>
		/// <param name="millisecondsTimeout">The number of milliseconds to wait, or <see cref="F:System.Threading.Timeout.Infinite" />(-1) to wait indefinitely.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="millisecondsTimeout" /> is a negative number other than -1, which represents an infinite time-out.</exception>
		/// <exception cref="T:System.InvalidOperationException">The maximum number of waiters has been exceeded.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has already been disposed.</exception>
		// Token: 0x060014D6 RID: 5334 RVA: 0x00054194 File Offset: 0x00052394
		public bool Wait(int millisecondsTimeout)
		{
			return this.Wait(millisecondsTimeout, default(CancellationToken));
		}

		/// <summary>Blocks the current thread until the current <see cref="T:System.Threading.ManualResetEventSlim" /> is set, using a 32-bit signed integer to measure the time interval, while observing a <see cref="T:System.Threading.CancellationToken" />.</summary>
		/// <returns>true if the <see cref="T:System.Threading.ManualResetEventSlim" /> was set; otherwise, false.</returns>
		/// <param name="millisecondsTimeout">The number of milliseconds to wait, or <see cref="F:System.Threading.Timeout.Infinite" />(-1) to wait indefinitely.</param>
		/// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> to observe.</param>
		/// <exception cref="T:System.OperationCanceledException">
		///   <paramref name="cancellationToken" /> was canceled.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="millisecondsTimeout" /> is a negative number other than -1, which represents an infinite time-out.</exception>
		/// <exception cref="T:System.InvalidOperationException">The maximum number of waiters has been exceeded.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has already been disposed or the <see cref="T:System.Threading.CancellationTokenSource" /> that created <paramref name="cancellationToken" /> has been disposed.</exception>
		// Token: 0x060014D7 RID: 5335 RVA: 0x000541B4 File Offset: 0x000523B4
		public bool Wait(int millisecondsTimeout, CancellationToken cancellationToken)
		{
			this.ThrowIfDisposed();
			cancellationToken.ThrowIfCancellationRequested();
			if (millisecondsTimeout < -1)
			{
				throw new ArgumentOutOfRangeException("millisecondsTimeout");
			}
			if (!this.IsSet)
			{
				if (millisecondsTimeout == 0)
				{
					return false;
				}
				uint num = 0U;
				bool flag = false;
				int num2 = millisecondsTimeout;
				if (millisecondsTimeout != -1)
				{
					num = TimeoutHelper.GetTime();
					flag = true;
				}
				int spinCount = this.SpinCount;
				SpinWait spinWait = default(SpinWait);
				while (spinWait.Count < spinCount)
				{
					spinWait.SpinOnce(40);
					if (this.IsSet)
					{
						return true;
					}
					if (spinWait.Count >= 100 && spinWait.Count % 10 == 0)
					{
						cancellationToken.ThrowIfCancellationRequested();
					}
				}
				this.EnsureLockObjectCreated();
				using (cancellationToken.InternalRegisterWithoutEC(ManualResetEventSlim.s_cancellationTokenCallback, this))
				{
					object @lock = this.m_lock;
					lock (@lock)
					{
						while (!this.IsSet)
						{
							cancellationToken.ThrowIfCancellationRequested();
							if (flag)
							{
								num2 = TimeoutHelper.UpdateTimeOut(num, millisecondsTimeout);
								if (num2 <= 0)
								{
									return false;
								}
							}
							this.Waiters++;
							if (this.IsSet)
							{
								int waiters = this.Waiters;
								this.Waiters = waiters - 1;
								return true;
							}
							try
							{
								if (!Monitor.Wait(this.m_lock, num2))
								{
									return false;
								}
							}
							finally
							{
								this.Waiters--;
							}
						}
					}
				}
				return true;
			}
			return true;
		}

		/// <summary>Releases all resources used by the current instance of the <see cref="T:System.Threading.ManualResetEventSlim" /> class.</summary>
		// Token: 0x060014D8 RID: 5336 RVA: 0x00054338 File Offset: 0x00052538
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Threading.ManualResetEventSlim" />, and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
		// Token: 0x060014D9 RID: 5337 RVA: 0x00054348 File Offset: 0x00052548
		protected virtual void Dispose(bool disposing)
		{
			if ((this.m_combinedState & 1073741824) != 0)
			{
				return;
			}
			this.m_combinedState |= 1073741824;
			if (disposing)
			{
				ManualResetEvent eventObj = this.m_eventObj;
				if (eventObj != null)
				{
					ManualResetEvent manualResetEvent = eventObj;
					lock (manualResetEvent)
					{
						eventObj.Dispose();
						this.m_eventObj = null;
					}
				}
			}
		}

		// Token: 0x060014DA RID: 5338 RVA: 0x000543C4 File Offset: 0x000525C4
		private void ThrowIfDisposed()
		{
			if ((this.m_combinedState & 1073741824) != 0)
			{
				throw new ObjectDisposedException("The event has been disposed.");
			}
		}

		// Token: 0x060014DB RID: 5339 RVA: 0x000543E4 File Offset: 0x000525E4
		private static void CancellationTokenCallback(object obj)
		{
			ManualResetEventSlim manualResetEventSlim = obj as ManualResetEventSlim;
			object @lock = manualResetEventSlim.m_lock;
			lock (@lock)
			{
				Monitor.PulseAll(manualResetEventSlim.m_lock);
			}
		}

		// Token: 0x060014DC RID: 5340 RVA: 0x00054434 File Offset: 0x00052634
		private void UpdateStateAtomically(int newBits, int updateBitsMask)
		{
			SpinWait spinWait = default(SpinWait);
			for (;;)
			{
				int combinedState = this.m_combinedState;
				int num = (combinedState & ~updateBitsMask) | newBits;
				if (Interlocked.CompareExchange(ref this.m_combinedState, num, combinedState) == combinedState)
				{
					break;
				}
				spinWait.SpinOnce();
			}
		}

		// Token: 0x060014DD RID: 5341 RVA: 0x00054472 File Offset: 0x00052672
		private static int ExtractStatePortionAndShiftRight(int state, int mask, int rightBitShiftCount)
		{
			return (int)((uint)(state & mask) >> rightBitShiftCount);
		}

		// Token: 0x060014DE RID: 5342 RVA: 0x0005447C File Offset: 0x0005267C
		private static int ExtractStatePortion(int state, int mask)
		{
			return state & mask;
		}

		// Token: 0x04000A24 RID: 2596
		private const int DEFAULT_SPIN_SP = 1;

		// Token: 0x04000A25 RID: 2597
		private volatile object m_lock;

		// Token: 0x04000A26 RID: 2598
		private volatile ManualResetEvent m_eventObj;

		// Token: 0x04000A27 RID: 2599
		private volatile int m_combinedState;

		// Token: 0x04000A28 RID: 2600
		private const int SignalledState_BitMask = -2147483648;

		// Token: 0x04000A29 RID: 2601
		private const int SignalledState_ShiftCount = 31;

		// Token: 0x04000A2A RID: 2602
		private const int Dispose_BitMask = 1073741824;

		// Token: 0x04000A2B RID: 2603
		private const int SpinCountState_BitMask = 1073217536;

		// Token: 0x04000A2C RID: 2604
		private const int SpinCountState_ShiftCount = 19;

		// Token: 0x04000A2D RID: 2605
		private const int SpinCountState_MaxValue = 2047;

		// Token: 0x04000A2E RID: 2606
		private const int NumWaitersState_BitMask = 524287;

		// Token: 0x04000A2F RID: 2607
		private const int NumWaitersState_ShiftCount = 0;

		// Token: 0x04000A30 RID: 2608
		private const int NumWaitersState_MaxValue = 524287;

		// Token: 0x04000A31 RID: 2609
		private static Action<object> s_cancellationTokenCallback = new Action<object>(ManualResetEventSlim.CancellationTokenCallback);
	}
}
