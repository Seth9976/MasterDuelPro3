using System;
using System.Diagnostics;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;

namespace System.Threading
{
	/// <summary>Provides a mutual exclusion lock primitive where a thread trying to acquire the lock waits in a loop repeatedly checking until the lock becomes available.</summary>
	// Token: 0x0200024B RID: 587
	[ComVisible(false)]
	[DebuggerTypeProxy(typeof(SpinLock.SystemThreading_SpinLockDebugView))]
	[DebuggerDisplay("IsHeld = {IsHeld}")]
	public struct SpinLock
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.SpinLock" /> structure with the option to track thread IDs to improve debugging.</summary>
		/// <param name="enableThreadOwnerTracking">Whether to capture and use thread IDs for debugging purposes.</param>
		// Token: 0x06001581 RID: 5505 RVA: 0x00056936 File Offset: 0x00054B36
		public SpinLock(bool enableThreadOwnerTracking)
		{
			this.m_owner = 0;
			if (!enableThreadOwnerTracking)
			{
				this.m_owner |= int.MinValue;
			}
		}

		/// <summary>Acquires the lock in a reliable manner, such that even if an exception occurs within the method call, <paramref name="lockTaken" /> can be examined reliably to determine whether the lock was acquired.</summary>
		/// <param name="lockTaken">True if the lock is acquired; otherwise, false. <paramref name="lockTaken" /> must be initialized to false prior to calling this method.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="lockTaken" /> argument must be initialized to false prior to calling Enter.</exception>
		/// <exception cref="T:System.Threading.LockRecursionException">Thread ownership tracking is enabled, and the current thread has already acquired this lock.</exception>
		// Token: 0x06001582 RID: 5506 RVA: 0x0005695C File Offset: 0x00054B5C
		public void Enter(ref bool lockTaken)
		{
			Thread.BeginCriticalRegion();
			int owner = this.m_owner;
			if (lockTaken || (owner & -2147483647) != -2147483648 || Interlocked.CompareExchange(ref this.m_owner, owner | 1, owner, ref lockTaken) != owner)
			{
				this.ContinueTryEnter(-1, ref lockTaken);
			}
		}

		/// <summary>Attempts to acquire the lock in a reliable manner, such that even if an exception occurs within the method call, <paramref name="lockTaken" /> can be examined reliably to determine whether the lock was acquired.</summary>
		/// <param name="lockTaken">True if the lock is acquired; otherwise, false. <paramref name="lockTaken" /> must be initialized to false prior to calling this method.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="lockTaken" /> argument must be initialized to false prior to calling TryEnter.</exception>
		/// <exception cref="T:System.Threading.LockRecursionException">Thread ownership tracking is enabled, and the current thread has already acquired this lock.</exception>
		// Token: 0x06001583 RID: 5507 RVA: 0x000569A4 File Offset: 0x00054BA4
		public void TryEnter(ref bool lockTaken)
		{
			this.TryEnter(0, ref lockTaken);
		}

		/// <summary>Attempts to acquire the lock in a reliable manner, such that even if an exception occurs within the method call, <paramref name="lockTaken" /> can be examined reliably to determine whether the lock was acquired.</summary>
		/// <param name="timeout">A <see cref="T:System.TimeSpan" /> that represents the number of milliseconds to wait, or a <see cref="T:System.TimeSpan" /> that represents -1 milliseconds to wait indefinitely.</param>
		/// <param name="lockTaken">True if the lock is acquired; otherwise, false. <paramref name="lockTaken" /> must be initialized to false prior to calling this method.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="timeout" /> is a negative number other than -1 milliseconds, which represents an infinite time-out -or- timeout is greater than <see cref="F:System.Int32.MaxValue" /> milliseconds.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="lockTaken" /> argument must be initialized to false prior to calling TryEnter.</exception>
		/// <exception cref="T:System.Threading.LockRecursionException">Thread ownership tracking is enabled, and the current thread has already acquired this lock.</exception>
		// Token: 0x06001584 RID: 5508 RVA: 0x000569B0 File Offset: 0x00054BB0
		public void TryEnter(TimeSpan timeout, ref bool lockTaken)
		{
			long num = (long)timeout.TotalMilliseconds;
			if (num < -1L || num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("timeout", timeout, Environment.GetResourceString("The timeout must be a value between -1 and Int32.MaxValue, inclusive."));
			}
			this.TryEnter((int)timeout.TotalMilliseconds, ref lockTaken);
		}

		/// <summary>Attempts to acquire the lock in a reliable manner, such that even if an exception occurs within the method call, <paramref name="lockTaken" /> can be examined reliably to determine whether the lock was acquired.</summary>
		/// <param name="millisecondsTimeout">The number of milliseconds to wait, or <see cref="F:System.Threading.Timeout.Infinite" /> (-1) to wait indefinitely.</param>
		/// <param name="lockTaken">True if the lock is acquired; otherwise, false. <paramref name="lockTaken" /> must be initialized to false prior to calling this method.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="millisecondsTimeout" /> is a negative number other than -1, which represents an infinite time-out.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="lockTaken" /> argument must be initialized to false prior to calling TryEnter.</exception>
		/// <exception cref="T:System.Threading.LockRecursionException">Thread ownership tracking is enabled, and the current thread has already acquired this lock.</exception>
		// Token: 0x06001585 RID: 5509 RVA: 0x00056A00 File Offset: 0x00054C00
		public void TryEnter(int millisecondsTimeout, ref bool lockTaken)
		{
			Thread.BeginCriticalRegion();
			int owner = this.m_owner;
			if (((millisecondsTimeout < -1) | lockTaken) || (owner & -2147483647) != -2147483648 || Interlocked.CompareExchange(ref this.m_owner, owner | 1, owner, ref lockTaken) != owner)
			{
				this.ContinueTryEnter(millisecondsTimeout, ref lockTaken);
			}
		}

		// Token: 0x06001586 RID: 5510 RVA: 0x00056A50 File Offset: 0x00054C50
		private void ContinueTryEnter(int millisecondsTimeout, ref bool lockTaken)
		{
			Thread.EndCriticalRegion();
			if (lockTaken)
			{
				lockTaken = false;
				throw new ArgumentException(Environment.GetResourceString("The tookLock argument must be set to false before calling this method."));
			}
			if (millisecondsTimeout < -1)
			{
				throw new ArgumentOutOfRangeException("millisecondsTimeout", millisecondsTimeout, Environment.GetResourceString("The timeout must be a value between -1 and Int32.MaxValue, inclusive."));
			}
			uint num = 0U;
			if (millisecondsTimeout != -1 && millisecondsTimeout != 0)
			{
				num = TimeoutHelper.GetTime();
			}
			if (this.IsThreadOwnerTrackingEnabled)
			{
				this.ContinueTryEnterWithThreadTracking(millisecondsTimeout, num, ref lockTaken);
				return;
			}
			int num2 = int.MaxValue;
			int num3 = this.m_owner;
			if ((num3 & 1) == 0)
			{
				Thread.BeginCriticalRegion();
				if (Interlocked.CompareExchange(ref this.m_owner, num3 | 1, num3, ref lockTaken) == num3)
				{
					return;
				}
				Thread.EndCriticalRegion();
			}
			else if ((num3 & 2147483646) != SpinLock.MAXIMUM_WAITERS)
			{
				num2 = (Interlocked.Add(ref this.m_owner, 2) & 2147483646) >> 1;
			}
			if (millisecondsTimeout == 0 || (millisecondsTimeout != -1 && TimeoutHelper.UpdateTimeOut(num, millisecondsTimeout) <= 0))
			{
				this.DecrementWaiters();
				return;
			}
			int processorCount = PlatformHelper.ProcessorCount;
			if (num2 < processorCount)
			{
				int num4 = 1;
				for (int i = 1; i <= num2 * 100; i++)
				{
					Thread.SpinWait((num2 + i) * 100 * num4);
					if (num4 < processorCount)
					{
						num4++;
					}
					num3 = this.m_owner;
					if ((num3 & 1) == 0)
					{
						Thread.BeginCriticalRegion();
						int num5 = (((num3 & 2147483646) == 0) ? (num3 | 1) : ((num3 - 2) | 1));
						if (Interlocked.CompareExchange(ref this.m_owner, num5, num3, ref lockTaken) == num3)
						{
							return;
						}
						Thread.EndCriticalRegion();
					}
				}
			}
			if (millisecondsTimeout != -1 && TimeoutHelper.UpdateTimeOut(num, millisecondsTimeout) <= 0)
			{
				this.DecrementWaiters();
				return;
			}
			int num6 = 0;
			for (;;)
			{
				num3 = this.m_owner;
				if ((num3 & 1) == 0)
				{
					Thread.BeginCriticalRegion();
					int num7 = (((num3 & 2147483646) == 0) ? (num3 | 1) : ((num3 - 2) | 1));
					if (Interlocked.CompareExchange(ref this.m_owner, num7, num3, ref lockTaken) == num3)
					{
						break;
					}
					Thread.EndCriticalRegion();
				}
				if (num6 % 40 == 0)
				{
					Thread.Sleep(1);
				}
				else if (num6 % 10 == 0)
				{
					Thread.Sleep(0);
				}
				else
				{
					Thread.Yield();
				}
				if (num6 % 10 == 0 && millisecondsTimeout != -1 && TimeoutHelper.UpdateTimeOut(num, millisecondsTimeout) <= 0)
				{
					goto Block_25;
				}
				num6++;
			}
			return;
			Block_25:
			this.DecrementWaiters();
		}

		// Token: 0x06001587 RID: 5511 RVA: 0x00056C44 File Offset: 0x00054E44
		private void DecrementWaiters()
		{
			SpinWait spinWait = default(SpinWait);
			for (;;)
			{
				int owner = this.m_owner;
				if ((owner & 2147483646) == 0)
				{
					break;
				}
				if (Interlocked.CompareExchange(ref this.m_owner, owner - 2, owner) == owner)
				{
					return;
				}
				spinWait.SpinOnce();
			}
		}

		// Token: 0x06001588 RID: 5512 RVA: 0x00056C88 File Offset: 0x00054E88
		private void ContinueTryEnterWithThreadTracking(int millisecondsTimeout, uint startTime, ref bool lockTaken)
		{
			int num = 0;
			int managedThreadId = Thread.CurrentThread.ManagedThreadId;
			if (this.m_owner == managedThreadId)
			{
				throw new LockRecursionException(Environment.GetResourceString("The calling thread already holds the lock."));
			}
			SpinWait spinWait = default(SpinWait);
			for (;;)
			{
				spinWait.SpinOnce();
				if (this.m_owner == num)
				{
					Thread.BeginCriticalRegion();
					if (Interlocked.CompareExchange(ref this.m_owner, managedThreadId, num, ref lockTaken) == num)
					{
						break;
					}
					Thread.EndCriticalRegion();
				}
				if (millisecondsTimeout == 0 || (millisecondsTimeout != -1 && spinWait.NextSpinWillYield && TimeoutHelper.UpdateTimeOut(startTime, millisecondsTimeout) <= 0))
				{
					return;
				}
			}
		}

		/// <summary>Releases the lock.</summary>
		/// <exception cref="T:System.Threading.SynchronizationLockException">Thread ownership tracking is enabled, and the current thread is not the owner of this lock.</exception>
		// Token: 0x06001589 RID: 5513 RVA: 0x00056D0D File Offset: 0x00054F0D
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public void Exit()
		{
			if ((this.m_owner & -2147483648) == 0)
			{
				this.ExitSlowPath(true);
			}
			else
			{
				Interlocked.Decrement(ref this.m_owner);
			}
			Thread.EndCriticalRegion();
		}

		/// <summary>Releases the lock.</summary>
		/// <param name="useMemoryBarrier">A Boolean value that indicates whether a memory fence should be issued in order to immediately publish the exit operation to other threads.</param>
		/// <exception cref="T:System.Threading.SynchronizationLockException">Thread ownership tracking is enabled, and the current thread is not the owner of this lock.</exception>
		// Token: 0x0600158A RID: 5514 RVA: 0x00056D3C File Offset: 0x00054F3C
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public void Exit(bool useMemoryBarrier)
		{
			if ((this.m_owner & -2147483648) != 0 && !useMemoryBarrier)
			{
				int owner = this.m_owner;
				this.m_owner = owner & -2;
			}
			else
			{
				this.ExitSlowPath(useMemoryBarrier);
			}
			Thread.EndCriticalRegion();
		}

		// Token: 0x0600158B RID: 5515 RVA: 0x00056D80 File Offset: 0x00054F80
		private void ExitSlowPath(bool useMemoryBarrier)
		{
			bool flag = (this.m_owner & int.MinValue) == 0;
			if (flag && !this.IsHeldByCurrentThread)
			{
				throw new SynchronizationLockException(Environment.GetResourceString("The calling thread does not hold the lock."));
			}
			if (useMemoryBarrier)
			{
				if (flag)
				{
					Interlocked.Exchange(ref this.m_owner, 0);
					return;
				}
				Interlocked.Decrement(ref this.m_owner);
				return;
			}
			else
			{
				if (flag)
				{
					this.m_owner = 0;
					return;
				}
				int owner = this.m_owner;
				this.m_owner = owner & -2;
				return;
			}
		}

		/// <summary>Gets whether the lock is currently held by any thread.</summary>
		/// <returns>true if the lock is currently held by any thread; otherwise false.</returns>
		// Token: 0x17000240 RID: 576
		// (get) Token: 0x0600158C RID: 5516 RVA: 0x00056DFD File Offset: 0x00054FFD
		public bool IsHeld
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			get
			{
				if (this.IsThreadOwnerTrackingEnabled)
				{
					return this.m_owner != 0;
				}
				return (this.m_owner & 1) != 0;
			}
		}

		/// <summary>Gets whether the lock is held by the current thread.</summary>
		/// <returns>true if the lock is held by the current thread; otherwise false.</returns>
		/// <exception cref="T:System.InvalidOperationException">Thread ownership tracking is disabled.</exception>
		// Token: 0x17000241 RID: 577
		// (get) Token: 0x0600158D RID: 5517 RVA: 0x00056E20 File Offset: 0x00055020
		public bool IsHeldByCurrentThread
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			get
			{
				if (!this.IsThreadOwnerTrackingEnabled)
				{
					throw new InvalidOperationException(Environment.GetResourceString("Thread tracking is disabled."));
				}
				return (this.m_owner & int.MaxValue) == Thread.CurrentThread.ManagedThreadId;
			}
		}

		/// <summary>Gets whether thread ownership tracking is enabled for this instance.</summary>
		/// <returns>true if thread ownership tracking is enabled for this instance; otherwise false.</returns>
		// Token: 0x17000242 RID: 578
		// (get) Token: 0x0600158E RID: 5518 RVA: 0x00056E54 File Offset: 0x00055054
		public bool IsThreadOwnerTrackingEnabled
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			get
			{
				return (this.m_owner & int.MinValue) == 0;
			}
		}

		// Token: 0x04000A9A RID: 2714
		private volatile int m_owner;

		// Token: 0x04000A9B RID: 2715
		private const int SPINNING_FACTOR = 100;

		// Token: 0x04000A9C RID: 2716
		private const int SLEEP_ONE_FREQUENCY = 40;

		// Token: 0x04000A9D RID: 2717
		private const int SLEEP_ZERO_FREQUENCY = 10;

		// Token: 0x04000A9E RID: 2718
		private const int TIMEOUT_CHECK_FREQUENCY = 10;

		// Token: 0x04000A9F RID: 2719
		private const int LOCK_ID_DISABLE_MASK = -2147483648;

		// Token: 0x04000AA0 RID: 2720
		private const int LOCK_ANONYMOUS_OWNED = 1;

		// Token: 0x04000AA1 RID: 2721
		private const int WAITERS_MASK = 2147483646;

		// Token: 0x04000AA2 RID: 2722
		private const int ID_DISABLED_AND_ANONYMOUS_OWNED = -2147483647;

		// Token: 0x04000AA3 RID: 2723
		private const int LOCK_UNOWNED = 0;

		// Token: 0x04000AA4 RID: 2724
		private static int MAXIMUM_WAITERS = 2147483646;

		// Token: 0x0200024C RID: 588
		internal class SystemThreading_SpinLockDebugView
		{
			// Token: 0x06001590 RID: 5520 RVA: 0x00056E73 File Offset: 0x00055073
			public SystemThreading_SpinLockDebugView(SpinLock spinLock)
			{
				this.m_spinLock = spinLock;
			}

			// Token: 0x17000243 RID: 579
			// (get) Token: 0x06001591 RID: 5521 RVA: 0x00056E84 File Offset: 0x00055084
			public bool? IsHeldByCurrentThread
			{
				get
				{
					bool? flag;
					try
					{
						flag = new bool?(this.m_spinLock.IsHeldByCurrentThread);
					}
					catch (InvalidOperationException)
					{
						flag = null;
					}
					return flag;
				}
			}

			// Token: 0x17000244 RID: 580
			// (get) Token: 0x06001592 RID: 5522 RVA: 0x00056EC4 File Offset: 0x000550C4
			public int? OwnerThreadID
			{
				get
				{
					if (this.m_spinLock.IsThreadOwnerTrackingEnabled)
					{
						return new int?(this.m_spinLock.m_owner);
					}
					return null;
				}
			}

			// Token: 0x17000245 RID: 581
			// (get) Token: 0x06001593 RID: 5523 RVA: 0x00056EFA File Offset: 0x000550FA
			public bool IsHeld
			{
				get
				{
					return this.m_spinLock.IsHeld;
				}
			}

			// Token: 0x04000AA5 RID: 2725
			private SpinLock m_spinLock;
		}
	}
}
