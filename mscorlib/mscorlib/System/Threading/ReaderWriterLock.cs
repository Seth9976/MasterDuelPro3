using System;
using System.Collections;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;

namespace System.Threading
{
	/// <summary>Defines a lock that supports single writers and multiple readers. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000283 RID: 643
	[ComVisible(true)]
	public sealed class ReaderWriterLock : CriticalFinalizerObject
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.ReaderWriterLock" /> class.</summary>
		// Token: 0x060017EC RID: 6124 RVA: 0x0005C17C File Offset: 0x0005A37C
		public ReaderWriterLock()
		{
			this.writer_queue = new LockQueue(this);
			this.reader_locks = new Hashtable();
			GC.SuppressFinalize(this);
		}

		// Token: 0x060017ED RID: 6125 RVA: 0x0005C1A8 File Offset: 0x0005A3A8
		~ReaderWriterLock()
		{
		}

		/// <summary>Gets a value indicating whether the current thread holds a reader lock.</summary>
		/// <returns>true if the current thread holds a reader lock; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000284 RID: 644
		// (get) Token: 0x060017EE RID: 6126 RVA: 0x0005C1D0 File Offset: 0x0005A3D0
		public bool IsReaderLockHeld
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			get
			{
				bool flag2;
				lock (this)
				{
					flag2 = this.reader_locks.ContainsKey(Thread.CurrentThreadId);
				}
				return flag2;
			}
		}

		/// <summary>Gets a value indicating whether the current thread holds the writer lock.</summary>
		/// <returns>true if the current thread holds the writer lock; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000285 RID: 645
		// (get) Token: 0x060017EF RID: 6127 RVA: 0x0005C21C File Offset: 0x0005A41C
		public bool IsWriterLockHeld
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			get
			{
				bool flag2;
				lock (this)
				{
					flag2 = this.state < 0 && Thread.CurrentThreadId == this.writer_lock_owner;
				}
				return flag2;
			}
		}

		/// <summary>Gets the current sequence number.</summary>
		/// <returns>The current sequence number.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000286 RID: 646
		// (get) Token: 0x060017F0 RID: 6128 RVA: 0x0005C26C File Offset: 0x0005A46C
		public int WriterSeqNum
		{
			get
			{
				int num;
				lock (this)
				{
					num = this.seq_num;
				}
				return num;
			}
		}

		/// <summary>Acquires a reader lock, using an <see cref="T:System.Int32" /> value for the time-out.</summary>
		/// <param name="millisecondsTimeout">The time-out in milliseconds. </param>
		/// <exception cref="T:System.ApplicationException">
		///   <paramref name="millisecondsTimeout" /> expires before the lock request is granted. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060017F1 RID: 6129 RVA: 0x0005C2AC File Offset: 0x0005A4AC
		public void AcquireReaderLock(int millisecondsTimeout)
		{
			this.AcquireReaderLock(millisecondsTimeout, 1);
		}

		// Token: 0x060017F2 RID: 6130 RVA: 0x0005C2B8 File Offset: 0x0005A4B8
		private void AcquireReaderLock(int millisecondsTimeout, int initialLockCount)
		{
			lock (this)
			{
				if (this.HasWriterLock())
				{
					this.AcquireWriterLock(millisecondsTimeout, initialLockCount);
				}
				else
				{
					object obj = this.reader_locks[Thread.CurrentThreadId];
					if (obj == null)
					{
						this.readers++;
						try
						{
							if (this.state < 0 || !this.writer_queue.IsEmpty)
							{
								while (Monitor.Wait(this, millisecondsTimeout))
								{
									if (this.state >= 0)
									{
										goto IL_007B;
									}
								}
								throw new ApplicationException("Timeout expired");
							}
							IL_007B:;
						}
						finally
						{
							this.readers--;
						}
						this.reader_locks[Thread.CurrentThreadId] = initialLockCount;
						this.state += initialLockCount;
					}
					else
					{
						this.reader_locks[Thread.CurrentThreadId] = (int)obj + 1;
						this.state++;
					}
				}
			}
		}

		/// <summary>Acquires a reader lock, using a <see cref="T:System.TimeSpan" /> value for the time-out.</summary>
		/// <param name="timeout">A TimeSpan specifying the time-out period. </param>
		/// <exception cref="T:System.ApplicationException">
		///   <paramref name="timeout" /> expires before the lock request is granted. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="timeout" /> specifies a negative value other than -1 milliseconds. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060017F3 RID: 6131 RVA: 0x0005C3D4 File Offset: 0x0005A5D4
		public void AcquireReaderLock(TimeSpan timeout)
		{
			int num = this.CheckTimeout(timeout);
			this.AcquireReaderLock(num, 1);
		}

		/// <summary>Acquires the writer lock, using an <see cref="T:System.Int32" /> value for the time-out.</summary>
		/// <param name="millisecondsTimeout">The time-out in milliseconds. </param>
		/// <exception cref="T:System.ApplicationException">
		///   <paramref name="timeout" /> expires before the lock request is granted. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060017F4 RID: 6132 RVA: 0x0005C3F1 File Offset: 0x0005A5F1
		public void AcquireWriterLock(int millisecondsTimeout)
		{
			this.AcquireWriterLock(millisecondsTimeout, 1);
		}

		// Token: 0x060017F5 RID: 6133 RVA: 0x0005C3FC File Offset: 0x0005A5FC
		private void AcquireWriterLock(int millisecondsTimeout, int initialLockCount)
		{
			lock (this)
			{
				if (this.HasWriterLock())
				{
					this.state--;
				}
				else
				{
					if (this.state != 0 || !this.writer_queue.IsEmpty)
					{
						while (this.writer_queue.Wait(millisecondsTimeout))
						{
							if (this.state == 0)
							{
								goto IL_005A;
							}
						}
						throw new ApplicationException("Timeout expired");
					}
					IL_005A:
					this.state = -initialLockCount;
					this.writer_lock_owner = Thread.CurrentThreadId;
					this.seq_num++;
				}
			}
		}

		/// <summary>Acquires the writer lock, using a <see cref="T:System.TimeSpan" /> value for the time-out.</summary>
		/// <param name="timeout">The TimeSpan specifying the time-out period. </param>
		/// <exception cref="T:System.ApplicationException">
		///   <paramref name="timeout" /> expires before the lock request is granted. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="timeout" /> specifies a negative value other than -1 milliseconds. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060017F6 RID: 6134 RVA: 0x0005C4A0 File Offset: 0x0005A6A0
		public void AcquireWriterLock(TimeSpan timeout)
		{
			int num = this.CheckTimeout(timeout);
			this.AcquireWriterLock(num, 1);
		}

		/// <summary>Indicates whether the writer lock has been granted to any thread since the sequence number was obtained.</summary>
		/// <returns>true if the writer lock has been granted to any thread since the sequence number was obtained; otherwise, false.</returns>
		/// <param name="seqNum">The sequence number. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060017F7 RID: 6135 RVA: 0x0005C4C0 File Offset: 0x0005A6C0
		public bool AnyWritersSince(int seqNum)
		{
			bool flag2;
			lock (this)
			{
				flag2 = this.seq_num > seqNum;
			}
			return flag2;
		}

		/// <summary>Restores the lock status of the thread to what it was before <see cref="M:System.Threading.ReaderWriterLock.UpgradeToWriterLock(System.Int32)" /> was called.</summary>
		/// <param name="lockCookie">A <see cref="T:System.Threading.LockCookie" /> returned by <see cref="M:System.Threading.ReaderWriterLock.UpgradeToWriterLock(System.Int32)" />. </param>
		/// <exception cref="T:System.ApplicationException">The thread does not have the writer lock. </exception>
		/// <exception cref="T:System.NullReferenceException">The address of <paramref name="lockCookie" /> is a null pointer. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060017F8 RID: 6136 RVA: 0x0005C500 File Offset: 0x0005A700
		public void DowngradeFromWriterLock(ref LockCookie lockCookie)
		{
			lock (this)
			{
				if (!this.HasWriterLock())
				{
					throw new ApplicationException("The thread does not have the writer lock.");
				}
				if (lockCookie.WriterLocks != 0)
				{
					this.state++;
				}
				else
				{
					this.state = lockCookie.ReaderLocks;
					this.reader_locks[Thread.CurrentThreadId] = this.state;
					if (this.readers > 0)
					{
						Monitor.PulseAll(this);
					}
				}
			}
		}

		/// <summary>Releases the lock, regardless of the number of times the thread acquired the lock.</summary>
		/// <returns>A <see cref="T:System.Threading.LockCookie" /> value representing the released lock.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060017F9 RID: 6137 RVA: 0x0005C59C File Offset: 0x0005A79C
		public LockCookie ReleaseLock()
		{
			LockCookie lockCookie;
			lock (this)
			{
				lockCookie = this.GetLockCookie();
				if (lockCookie.WriterLocks != 0)
				{
					this.ReleaseWriterLock(lockCookie.WriterLocks);
				}
				else if (lockCookie.ReaderLocks != 0)
				{
					this.ReleaseReaderLock(lockCookie.ReaderLocks, lockCookie.ReaderLocks);
				}
			}
			return lockCookie;
		}

		/// <summary>Decrements the lock count.</summary>
		/// <exception cref="T:System.ApplicationException">The thread does not have any reader or writer locks. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060017FA RID: 6138 RVA: 0x0005C60C File Offset: 0x0005A80C
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public void ReleaseReaderLock()
		{
			lock (this)
			{
				if (!this.HasWriterLock())
				{
					if (this.state > 0)
					{
						object obj = this.reader_locks[Thread.CurrentThreadId];
						if (obj != null)
						{
							this.ReleaseReaderLock((int)obj, 1);
							return;
						}
					}
					throw new ApplicationException("The thread does not have any reader or writer locks.");
				}
				this.ReleaseWriterLock();
			}
		}

		// Token: 0x060017FB RID: 6139 RVA: 0x0005C68C File Offset: 0x0005A88C
		private void ReleaseReaderLock(int currentCount, int releaseCount)
		{
			int num = currentCount - releaseCount;
			if (num == 0)
			{
				this.reader_locks.Remove(Thread.CurrentThreadId);
			}
			else
			{
				this.reader_locks[Thread.CurrentThreadId] = num;
			}
			this.state -= releaseCount;
			if (this.state == 0 && !this.writer_queue.IsEmpty)
			{
				this.writer_queue.Pulse();
			}
		}

		/// <summary>Decrements the lock count on the writer lock.</summary>
		/// <exception cref="T:System.ApplicationException">The thread does not have the writer lock. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060017FC RID: 6140 RVA: 0x0005C700 File Offset: 0x0005A900
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public void ReleaseWriterLock()
		{
			lock (this)
			{
				if (!this.HasWriterLock())
				{
					throw new ApplicationException("The thread does not have the writer lock.");
				}
				this.ReleaseWriterLock(1);
			}
		}

		// Token: 0x060017FD RID: 6141 RVA: 0x0005C750 File Offset: 0x0005A950
		private void ReleaseWriterLock(int releaseCount)
		{
			this.state += releaseCount;
			if (this.state == 0)
			{
				if (this.readers > 0)
				{
					Monitor.PulseAll(this);
					return;
				}
				if (!this.writer_queue.IsEmpty)
				{
					this.writer_queue.Pulse();
				}
			}
		}

		/// <summary>Restores the lock status of the thread to what it was before calling <see cref="M:System.Threading.ReaderWriterLock.ReleaseLock" />.</summary>
		/// <param name="lockCookie">A <see cref="T:System.Threading.LockCookie" /> returned by <see cref="M:System.Threading.ReaderWriterLock.ReleaseLock" />. </param>
		/// <exception cref="T:System.NullReferenceException">The address of <paramref name="lockCookie" /> is a null pointer. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060017FE RID: 6142 RVA: 0x0005C790 File Offset: 0x0005A990
		public void RestoreLock(ref LockCookie lockCookie)
		{
			lock (this)
			{
				if (lockCookie.WriterLocks != 0)
				{
					this.AcquireWriterLock(-1, lockCookie.WriterLocks);
				}
				else if (lockCookie.ReaderLocks != 0)
				{
					this.AcquireReaderLock(-1, lockCookie.ReaderLocks);
				}
			}
		}

		/// <summary>Upgrades a reader lock to the writer lock, using an Int32 value for the time-out.</summary>
		/// <returns>A <see cref="T:System.Threading.LockCookie" /> value.</returns>
		/// <param name="millisecondsTimeout">The time-out in milliseconds. </param>
		/// <exception cref="T:System.ApplicationException">
		///   <paramref name="millisecondsTimeout" /> expires before the lock request is granted. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060017FF RID: 6143 RVA: 0x0005C7F4 File Offset: 0x0005A9F4
		public LockCookie UpgradeToWriterLock(int millisecondsTimeout)
		{
			LockCookie lockCookie;
			lock (this)
			{
				lockCookie = this.GetLockCookie();
				if (lockCookie.WriterLocks != 0)
				{
					this.state--;
					return lockCookie;
				}
				if (lockCookie.ReaderLocks != 0)
				{
					this.ReleaseReaderLock(lockCookie.ReaderLocks, lockCookie.ReaderLocks);
				}
			}
			this.AcquireWriterLock(millisecondsTimeout);
			return lockCookie;
		}

		/// <summary>Upgrades a reader lock to the writer lock, using a TimeSpan value for the time-out.</summary>
		/// <returns>A <see cref="T:System.Threading.LockCookie" /> value.</returns>
		/// <param name="timeout">The TimeSpan specifying the time-out period. </param>
		/// <exception cref="T:System.ApplicationException">
		///   <paramref name="timeout" /> expires before the lock request is granted. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="timeout" /> specifies a negative value other than -1 milliseconds. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001800 RID: 6144 RVA: 0x0005C870 File Offset: 0x0005AA70
		public LockCookie UpgradeToWriterLock(TimeSpan timeout)
		{
			int num = this.CheckTimeout(timeout);
			return this.UpgradeToWriterLock(num);
		}

		// Token: 0x06001801 RID: 6145 RVA: 0x0005C88C File Offset: 0x0005AA8C
		private LockCookie GetLockCookie()
		{
			LockCookie lockCookie = new LockCookie(Thread.CurrentThreadId);
			if (this.HasWriterLock())
			{
				lockCookie.WriterLocks = -this.state;
			}
			else
			{
				object obj = this.reader_locks[Thread.CurrentThreadId];
				if (obj != null)
				{
					lockCookie.ReaderLocks = (int)obj;
				}
			}
			return lockCookie;
		}

		// Token: 0x06001802 RID: 6146 RVA: 0x0005C8E4 File Offset: 0x0005AAE4
		private bool HasWriterLock()
		{
			return this.state < 0 && Thread.CurrentThreadId == this.writer_lock_owner;
		}

		// Token: 0x06001803 RID: 6147 RVA: 0x0005C8FE File Offset: 0x0005AAFE
		private int CheckTimeout(TimeSpan timeout)
		{
			int num = (int)timeout.TotalMilliseconds;
			if (num < -1)
			{
				throw new ArgumentOutOfRangeException("timeout", "Number must be either non-negative or -1");
			}
			return num;
		}

		// Token: 0x04000B41 RID: 2881
		private int seq_num = 1;

		// Token: 0x04000B42 RID: 2882
		private int state;

		// Token: 0x04000B43 RID: 2883
		private int readers;

		// Token: 0x04000B44 RID: 2884
		private int writer_lock_owner;

		// Token: 0x04000B45 RID: 2885
		private LockQueue writer_queue;

		// Token: 0x04000B46 RID: 2886
		private Hashtable reader_locks;
	}
}
