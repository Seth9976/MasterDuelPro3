using System;
using System.Runtime.CompilerServices;

namespace System.Threading
{
	/// <summary>Represents a lock that is used to manage access to a resource, allowing multiple threads for reading or exclusive access for writing.</summary>
	// Token: 0x02000161 RID: 353
	public class ReaderWriterLockSlim : IDisposable
	{
		// Token: 0x06000B90 RID: 2960 RVA: 0x0002D93F File Offset: 0x0002BB3F
		private void InitializeThreadCounts()
		{
			this.upgradeLockOwnerId = -1;
			this.writeLockOwnerId = -1;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.ReaderWriterLockSlim" /> class with default property values.</summary>
		// Token: 0x06000B91 RID: 2961 RVA: 0x0002D94F File Offset: 0x0002BB4F
		public ReaderWriterLockSlim()
			: this(LockRecursionPolicy.NoRecursion)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.ReaderWriterLockSlim" /> class, specifying the lock recursion policy.</summary>
		/// <param name="recursionPolicy">One of the enumeration values that specifies the lock recursion policy. </param>
		// Token: 0x06000B92 RID: 2962 RVA: 0x0002D958 File Offset: 0x0002BB58
		public ReaderWriterLockSlim(LockRecursionPolicy recursionPolicy)
		{
			if (recursionPolicy == LockRecursionPolicy.SupportsRecursion)
			{
				this.fIsReentrant = true;
			}
			this.InitializeThreadCounts();
			this.fNoWaiters = true;
			this.lockID = Interlocked.Increment(ref ReaderWriterLockSlim.s_nextLockID);
		}

		// Token: 0x06000B93 RID: 2963 RVA: 0x0002D988 File Offset: 0x0002BB88
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static bool IsRWEntryEmpty(ReaderWriterCount rwc)
		{
			return rwc.lockID == 0L || (rwc.readercount == 0 && rwc.writercount == 0 && rwc.upgradecount == 0);
		}

		// Token: 0x06000B94 RID: 2964 RVA: 0x0002D9AF File Offset: 0x0002BBAF
		private bool IsRwHashEntryChanged(ReaderWriterCount lrwc)
		{
			return lrwc.lockID != this.lockID;
		}

		// Token: 0x06000B95 RID: 2965 RVA: 0x0002D9C4 File Offset: 0x0002BBC4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private ReaderWriterCount GetThreadRWCount(bool dontAllocate)
		{
			ReaderWriterCount next = ReaderWriterLockSlim.t_rwc;
			ReaderWriterCount readerWriterCount = null;
			while (next != null)
			{
				if (next.lockID == this.lockID)
				{
					return next;
				}
				if (!dontAllocate && readerWriterCount == null && ReaderWriterLockSlim.IsRWEntryEmpty(next))
				{
					readerWriterCount = next;
				}
				next = next.next;
			}
			if (dontAllocate)
			{
				return null;
			}
			if (readerWriterCount == null)
			{
				readerWriterCount = new ReaderWriterCount();
				readerWriterCount.next = ReaderWriterLockSlim.t_rwc;
				ReaderWriterLockSlim.t_rwc = readerWriterCount;
			}
			readerWriterCount.lockID = this.lockID;
			return readerWriterCount;
		}

		/// <summary>Tries to enter the lock in read mode.</summary>
		/// <exception cref="T:System.Threading.LockRecursionException">The <see cref="P:System.Threading.ReaderWriterLockSlim.RecursionPolicy" /> property is <see cref="F:System.Threading.LockRecursionPolicy.NoRecursion" /> and the current thread has already entered read mode. -or-The recursion number would exceed the capacity of the counter. This limit is so large that applications should never encounter it.</exception>
		// Token: 0x06000B96 RID: 2966 RVA: 0x0002DA31 File Offset: 0x0002BC31
		public void EnterReadLock()
		{
			this.TryEnterReadLock(-1);
		}

		/// <summary>Tries to enter the lock in read mode, with an optional integer time-out.</summary>
		/// <returns>true if the calling thread entered read mode, otherwise, false.</returns>
		/// <param name="millisecondsTimeout">The number of milliseconds to wait, or -1 (<see cref="F:System.Threading.Timeout.Infinite" />) to wait indefinitely.</param>
		/// <exception cref="T:System.Threading.LockRecursionException">The <see cref="P:System.Threading.ReaderWriterLockSlim.RecursionPolicy" /> property is <see cref="F:System.Threading.LockRecursionPolicy.NoRecursion" /> and the current thread has already entered the lock. -or-The recursion number would exceed the capacity of the counter. The limit is so large that applications should never encounter it.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of <paramref name="millisecondsTimeout" /> is negative, but it is not equal to <see cref="F:System.Threading.Timeout.Infinite" /> (-1), which is the only negative value allowed. </exception>
		// Token: 0x06000B97 RID: 2967 RVA: 0x0002DA3B File Offset: 0x0002BC3B
		public bool TryEnterReadLock(int millisecondsTimeout)
		{
			return this.TryEnterReadLock(new ReaderWriterLockSlim.TimeoutTracker(millisecondsTimeout));
		}

		// Token: 0x06000B98 RID: 2968 RVA: 0x0002DA49 File Offset: 0x0002BC49
		private bool TryEnterReadLock(ReaderWriterLockSlim.TimeoutTracker timeout)
		{
			return this.TryEnterReadLockCore(timeout);
		}

		// Token: 0x06000B99 RID: 2969 RVA: 0x0002DA54 File Offset: 0x0002BC54
		private bool TryEnterReadLockCore(ReaderWriterLockSlim.TimeoutTracker timeout)
		{
			if (this.fDisposed)
			{
				throw new ObjectDisposedException(null);
			}
			int managedThreadId = Thread.CurrentThread.ManagedThreadId;
			ReaderWriterCount readerWriterCount;
			if (!this.fIsReentrant)
			{
				if (managedThreadId == this.writeLockOwnerId)
				{
					throw new LockRecursionException(global::SR.GetString("A read lock may not be acquired with the write lock held in this mode."));
				}
				this.EnterMyLock();
				readerWriterCount = this.GetThreadRWCount(false);
				if (readerWriterCount.readercount > 0)
				{
					this.ExitMyLock();
					throw new LockRecursionException(global::SR.GetString("Recursive read lock acquisitions not allowed in this mode."));
				}
				if (managedThreadId == this.upgradeLockOwnerId)
				{
					readerWriterCount.readercount++;
					this.owners += 1U;
					this.ExitMyLock();
					return true;
				}
			}
			else
			{
				this.EnterMyLock();
				readerWriterCount = this.GetThreadRWCount(false);
				if (readerWriterCount.readercount > 0)
				{
					readerWriterCount.readercount++;
					this.ExitMyLock();
					return true;
				}
				if (managedThreadId == this.upgradeLockOwnerId)
				{
					readerWriterCount.readercount++;
					this.owners += 1U;
					this.ExitMyLock();
					this.fUpgradeThreadHoldingRead = true;
					return true;
				}
				if (managedThreadId == this.writeLockOwnerId)
				{
					readerWriterCount.readercount++;
					this.owners += 1U;
					this.ExitMyLock();
					return true;
				}
			}
			bool flag = true;
			int num = 0;
			while (this.owners >= 268435454U)
			{
				if (num < 20)
				{
					this.ExitMyLock();
					if (timeout.IsExpired)
					{
						return false;
					}
					num++;
					ReaderWriterLockSlim.SpinWait(num);
					this.EnterMyLock();
					if (this.IsRwHashEntryChanged(readerWriterCount))
					{
						readerWriterCount = this.GetThreadRWCount(false);
					}
				}
				else if (this.readEvent == null)
				{
					this.LazyCreateEvent(ref this.readEvent, false);
					if (this.IsRwHashEntryChanged(readerWriterCount))
					{
						readerWriterCount = this.GetThreadRWCount(false);
					}
				}
				else
				{
					flag = this.WaitOnEvent(this.readEvent, ref this.numReadWaiters, timeout, false);
					if (!flag)
					{
						return false;
					}
					if (this.IsRwHashEntryChanged(readerWriterCount))
					{
						readerWriterCount = this.GetThreadRWCount(false);
					}
				}
			}
			this.owners += 1U;
			readerWriterCount.readercount++;
			this.ExitMyLock();
			return flag;
		}

		/// <summary>Tries to enter the lock in write mode.</summary>
		/// <exception cref="T:System.Threading.LockRecursionException">The <see cref="P:System.Threading.ReaderWriterLockSlim.RecursionPolicy" /> property is <see cref="F:System.Threading.LockRecursionPolicy.NoRecursion" /> and the current thread has already entered the lock in any mode. -or-The current thread has entered read mode, so trying to enter the lock in write mode would create the possibility of a deadlock. -or-The recursion number would exceed the capacity of the counter. The limit is so large that applications should never encounter it.</exception>
		// Token: 0x06000B9A RID: 2970 RVA: 0x0002DC5C File Offset: 0x0002BE5C
		public void EnterWriteLock()
		{
			this.TryEnterWriteLock(-1);
		}

		/// <summary>Tries to enter the lock in write mode, with an optional time-out.</summary>
		/// <returns>true if the calling thread entered write mode, otherwise, false.</returns>
		/// <param name="millisecondsTimeout">The number of milliseconds to wait, or -1 (<see cref="F:System.Threading.Timeout.Infinite" />) to wait indefinitely.</param>
		/// <exception cref="T:System.Threading.LockRecursionException">The <see cref="P:System.Threading.ReaderWriterLockSlim.RecursionPolicy" /> property is <see cref="F:System.Threading.LockRecursionPolicy.NoRecursion" /> and the current thread has already entered the lock. -or-The current thread initially entered the lock in read mode, and therefore trying to enter write mode would create the possibility of a deadlock. -or-The recursion number would exceed the capacity of the counter. The limit is so large that applications should never encounter it.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of <paramref name="millisecondsTimeout" /> is negative, but it is not equal to <see cref="F:System.Threading.Timeout.Infinite" /> (-1), which is the only negative value allowed. </exception>
		// Token: 0x06000B9B RID: 2971 RVA: 0x0002DC66 File Offset: 0x0002BE66
		public bool TryEnterWriteLock(int millisecondsTimeout)
		{
			return this.TryEnterWriteLock(new ReaderWriterLockSlim.TimeoutTracker(millisecondsTimeout));
		}

		// Token: 0x06000B9C RID: 2972 RVA: 0x0002DC74 File Offset: 0x0002BE74
		private bool TryEnterWriteLock(ReaderWriterLockSlim.TimeoutTracker timeout)
		{
			return this.TryEnterWriteLockCore(timeout);
		}

		// Token: 0x06000B9D RID: 2973 RVA: 0x0002DC80 File Offset: 0x0002BE80
		private bool TryEnterWriteLockCore(ReaderWriterLockSlim.TimeoutTracker timeout)
		{
			if (this.fDisposed)
			{
				throw new ObjectDisposedException(null);
			}
			int managedThreadId = Thread.CurrentThread.ManagedThreadId;
			bool flag = false;
			ReaderWriterCount readerWriterCount;
			if (!this.fIsReentrant)
			{
				if (managedThreadId == this.writeLockOwnerId)
				{
					throw new LockRecursionException(global::SR.GetString("Recursive write lock acquisitions not allowed in this mode."));
				}
				if (managedThreadId == this.upgradeLockOwnerId)
				{
					flag = true;
				}
				this.EnterMyLock();
				readerWriterCount = this.GetThreadRWCount(true);
				if (readerWriterCount != null && readerWriterCount.readercount > 0)
				{
					this.ExitMyLock();
					throw new LockRecursionException(global::SR.GetString("Write lock may not be acquired with read lock held. This pattern is prone to deadlocks. Please ensure that read locks are released before taking a write lock. If an upgrade is necessary, use an upgrade lock in place of the read lock."));
				}
			}
			else
			{
				this.EnterMyLock();
				readerWriterCount = this.GetThreadRWCount(false);
				if (managedThreadId == this.writeLockOwnerId)
				{
					readerWriterCount.writercount++;
					this.ExitMyLock();
					return true;
				}
				if (managedThreadId == this.upgradeLockOwnerId)
				{
					flag = true;
				}
				else if (readerWriterCount.readercount > 0)
				{
					this.ExitMyLock();
					throw new LockRecursionException(global::SR.GetString("Write lock may not be acquired with read lock held. This pattern is prone to deadlocks. Please ensure that read locks are released before taking a write lock. If an upgrade is necessary, use an upgrade lock in place of the read lock."));
				}
			}
			int num = 0;
			while (!this.IsWriterAcquired())
			{
				if (flag)
				{
					uint numReaders = this.GetNumReaders();
					if (numReaders == 1U)
					{
						this.SetWriterAcquired();
					}
					else
					{
						if (numReaders != 2U || readerWriterCount == null)
						{
							goto IL_012E;
						}
						if (this.IsRwHashEntryChanged(readerWriterCount))
						{
							readerWriterCount = this.GetThreadRWCount(false);
						}
						if (readerWriterCount.readercount <= 0)
						{
							goto IL_012E;
						}
						this.SetWriterAcquired();
					}
					IL_01C6:
					if (this.fIsReentrant)
					{
						if (this.IsRwHashEntryChanged(readerWriterCount))
						{
							readerWriterCount = this.GetThreadRWCount(false);
						}
						readerWriterCount.writercount++;
					}
					this.ExitMyLock();
					this.writeLockOwnerId = managedThreadId;
					return true;
				}
				IL_012E:
				if (num < 20)
				{
					this.ExitMyLock();
					if (timeout.IsExpired)
					{
						return false;
					}
					num++;
					ReaderWriterLockSlim.SpinWait(num);
					this.EnterMyLock();
				}
				else if (flag)
				{
					if (this.waitUpgradeEvent == null)
					{
						this.LazyCreateEvent(ref this.waitUpgradeEvent, true);
					}
					else if (!this.WaitOnEvent(this.waitUpgradeEvent, ref this.numWriteUpgradeWaiters, timeout, true))
					{
						return false;
					}
				}
				else if (this.writeEvent == null)
				{
					this.LazyCreateEvent(ref this.writeEvent, true);
				}
				else if (!this.WaitOnEvent(this.writeEvent, ref this.numWriteWaiters, timeout, true))
				{
					return false;
				}
			}
			this.SetWriterAcquired();
			goto IL_01C6;
		}

		/// <summary>Tries to enter the lock in upgradeable mode.</summary>
		/// <exception cref="T:System.Threading.LockRecursionException">The <see cref="P:System.Threading.ReaderWriterLockSlim.RecursionPolicy" /> property is <see cref="F:System.Threading.LockRecursionPolicy.NoRecursion" /> and the current thread has already entered the lock in any mode. -or-The current thread has entered read mode, so trying to enter upgradeable mode would create the possibility of a deadlock. -or-The recursion number would exceed the capacity of the counter. The limit is so large that applications should never encounter it.</exception>
		// Token: 0x06000B9E RID: 2974 RVA: 0x0002DE88 File Offset: 0x0002C088
		public void EnterUpgradeableReadLock()
		{
			this.TryEnterUpgradeableReadLock(-1);
		}

		/// <summary>Tries to enter the lock in upgradeable mode, with an optional time-out.</summary>
		/// <returns>true if the calling thread entered upgradeable mode, otherwise, false.</returns>
		/// <param name="millisecondsTimeout">The number of milliseconds to wait, or -1 (<see cref="F:System.Threading.Timeout.Infinite" />) to wait indefinitely.</param>
		/// <exception cref="T:System.Threading.LockRecursionException">The <see cref="P:System.Threading.ReaderWriterLockSlim.RecursionPolicy" /> property is <see cref="F:System.Threading.LockRecursionPolicy.NoRecursion" /> and the current thread has already entered the lock. -or-The current thread initially entered the lock in read mode, and therefore trying to enter upgradeable mode would create the possibility of a deadlock. -or-The recursion number would exceed the capacity of the counter. The limit is so large that applications should never encounter it.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of <paramref name="millisecondsTimeout" /> is negative, but it is not equal to <see cref="F:System.Threading.Timeout.Infinite" /> (-1), which is the only negative value allowed. </exception>
		// Token: 0x06000B9F RID: 2975 RVA: 0x0002DE92 File Offset: 0x0002C092
		public bool TryEnterUpgradeableReadLock(int millisecondsTimeout)
		{
			return this.TryEnterUpgradeableReadLock(new ReaderWriterLockSlim.TimeoutTracker(millisecondsTimeout));
		}

		// Token: 0x06000BA0 RID: 2976 RVA: 0x0002DEA0 File Offset: 0x0002C0A0
		private bool TryEnterUpgradeableReadLock(ReaderWriterLockSlim.TimeoutTracker timeout)
		{
			return this.TryEnterUpgradeableReadLockCore(timeout);
		}

		// Token: 0x06000BA1 RID: 2977 RVA: 0x0002DEAC File Offset: 0x0002C0AC
		private bool TryEnterUpgradeableReadLockCore(ReaderWriterLockSlim.TimeoutTracker timeout)
		{
			if (this.fDisposed)
			{
				throw new ObjectDisposedException(null);
			}
			int managedThreadId = Thread.CurrentThread.ManagedThreadId;
			ReaderWriterCount readerWriterCount;
			if (!this.fIsReentrant)
			{
				if (managedThreadId == this.upgradeLockOwnerId)
				{
					throw new LockRecursionException(global::SR.GetString("Recursive upgradeable lock acquisitions not allowed in this mode."));
				}
				if (managedThreadId == this.writeLockOwnerId)
				{
					throw new LockRecursionException(global::SR.GetString("Upgradeable lock may not be acquired with write lock held in this mode. Acquiring Upgradeable lock gives the ability to read along with an option to upgrade to a writer."));
				}
				this.EnterMyLock();
				readerWriterCount = this.GetThreadRWCount(true);
				if (readerWriterCount != null && readerWriterCount.readercount > 0)
				{
					this.ExitMyLock();
					throw new LockRecursionException(global::SR.GetString("Upgradeable lock may not be acquired with read lock held."));
				}
			}
			else
			{
				this.EnterMyLock();
				readerWriterCount = this.GetThreadRWCount(false);
				if (managedThreadId == this.upgradeLockOwnerId)
				{
					readerWriterCount.upgradecount++;
					this.ExitMyLock();
					return true;
				}
				if (managedThreadId == this.writeLockOwnerId)
				{
					this.owners += 1U;
					this.upgradeLockOwnerId = managedThreadId;
					readerWriterCount.upgradecount++;
					if (readerWriterCount.readercount > 0)
					{
						this.fUpgradeThreadHoldingRead = true;
					}
					this.ExitMyLock();
					return true;
				}
				if (readerWriterCount.readercount > 0)
				{
					this.ExitMyLock();
					throw new LockRecursionException(global::SR.GetString("Upgradeable lock may not be acquired with read lock held."));
				}
			}
			int num = 0;
			while (this.upgradeLockOwnerId != -1 || this.owners >= 268435454U)
			{
				if (num < 20)
				{
					this.ExitMyLock();
					if (timeout.IsExpired)
					{
						return false;
					}
					num++;
					ReaderWriterLockSlim.SpinWait(num);
					this.EnterMyLock();
				}
				else if (this.upgradeEvent == null)
				{
					this.LazyCreateEvent(ref this.upgradeEvent, true);
				}
				else if (!this.WaitOnEvent(this.upgradeEvent, ref this.numUpgradeWaiters, timeout, false))
				{
					return false;
				}
			}
			this.owners += 1U;
			this.upgradeLockOwnerId = managedThreadId;
			if (this.fIsReentrant)
			{
				if (this.IsRwHashEntryChanged(readerWriterCount))
				{
					readerWriterCount = this.GetThreadRWCount(false);
				}
				readerWriterCount.upgradecount++;
			}
			this.ExitMyLock();
			return true;
		}

		/// <summary>Reduces the recursion count for read mode, and exits read mode if the resulting count is 0 (zero).</summary>
		/// <exception cref="T:System.Threading.SynchronizationLockException">The current thread has not entered the lock in read mode.</exception>
		// Token: 0x06000BA2 RID: 2978 RVA: 0x0002E08C File Offset: 0x0002C28C
		public void ExitReadLock()
		{
			this.EnterMyLock();
			ReaderWriterCount threadRWCount = this.GetThreadRWCount(true);
			if (threadRWCount == null || threadRWCount.readercount < 1)
			{
				this.ExitMyLock();
				throw new SynchronizationLockException(global::SR.GetString("The read lock is being released without being held."));
			}
			if (this.fIsReentrant)
			{
				if (threadRWCount.readercount > 1)
				{
					threadRWCount.readercount--;
					this.ExitMyLock();
					return;
				}
				if (Thread.CurrentThread.ManagedThreadId == this.upgradeLockOwnerId)
				{
					this.fUpgradeThreadHoldingRead = false;
				}
			}
			this.owners -= 1U;
			threadRWCount.readercount--;
			this.ExitAndWakeUpAppropriateWaiters();
		}

		/// <summary>Reduces the recursion count for write mode, and exits write mode if the resulting count is 0 (zero).</summary>
		/// <exception cref="T:System.Threading.SynchronizationLockException">The current thread has not entered the lock in write mode.</exception>
		// Token: 0x06000BA3 RID: 2979 RVA: 0x0002E12C File Offset: 0x0002C32C
		public void ExitWriteLock()
		{
			if (!this.fIsReentrant)
			{
				if (Thread.CurrentThread.ManagedThreadId != this.writeLockOwnerId)
				{
					throw new SynchronizationLockException(global::SR.GetString("The write lock is being released without being held."));
				}
				this.EnterMyLock();
			}
			else
			{
				this.EnterMyLock();
				ReaderWriterCount threadRWCount = this.GetThreadRWCount(false);
				if (threadRWCount == null)
				{
					this.ExitMyLock();
					throw new SynchronizationLockException(global::SR.GetString("The write lock is being released without being held."));
				}
				if (threadRWCount.writercount < 1)
				{
					this.ExitMyLock();
					throw new SynchronizationLockException(global::SR.GetString("The write lock is being released without being held."));
				}
				threadRWCount.writercount--;
				if (threadRWCount.writercount > 0)
				{
					this.ExitMyLock();
					return;
				}
			}
			this.ClearWriterAcquired();
			this.writeLockOwnerId = -1;
			this.ExitAndWakeUpAppropriateWaiters();
		}

		/// <summary>Reduces the recursion count for upgradeable mode, and exits upgradeable mode if the resulting count is 0 (zero).</summary>
		/// <exception cref="T:System.Threading.SynchronizationLockException">The current thread has not entered the lock in upgradeable mode.</exception>
		// Token: 0x06000BA4 RID: 2980 RVA: 0x0002E1E0 File Offset: 0x0002C3E0
		public void ExitUpgradeableReadLock()
		{
			if (!this.fIsReentrant)
			{
				if (Thread.CurrentThread.ManagedThreadId != this.upgradeLockOwnerId)
				{
					throw new SynchronizationLockException(global::SR.GetString("The upgradeable lock is being released without being held."));
				}
				this.EnterMyLock();
			}
			else
			{
				this.EnterMyLock();
				ReaderWriterCount threadRWCount = this.GetThreadRWCount(true);
				if (threadRWCount == null)
				{
					this.ExitMyLock();
					throw new SynchronizationLockException(global::SR.GetString("The upgradeable lock is being released without being held."));
				}
				if (threadRWCount.upgradecount < 1)
				{
					this.ExitMyLock();
					throw new SynchronizationLockException(global::SR.GetString("The upgradeable lock is being released without being held."));
				}
				threadRWCount.upgradecount--;
				if (threadRWCount.upgradecount > 0)
				{
					this.ExitMyLock();
					return;
				}
				this.fUpgradeThreadHoldingRead = false;
			}
			this.owners -= 1U;
			this.upgradeLockOwnerId = -1;
			this.ExitAndWakeUpAppropriateWaiters();
		}

		// Token: 0x06000BA5 RID: 2981 RVA: 0x0002E2A4 File Offset: 0x0002C4A4
		private void LazyCreateEvent(ref EventWaitHandle waitEvent, bool makeAutoResetEvent)
		{
			this.ExitMyLock();
			EventWaitHandle eventWaitHandle;
			if (makeAutoResetEvent)
			{
				eventWaitHandle = new AutoResetEvent(false);
			}
			else
			{
				eventWaitHandle = new ManualResetEvent(false);
			}
			this.EnterMyLock();
			if (waitEvent == null)
			{
				waitEvent = eventWaitHandle;
				return;
			}
			eventWaitHandle.Close();
		}

		// Token: 0x06000BA6 RID: 2982 RVA: 0x0002E2E0 File Offset: 0x0002C4E0
		private bool WaitOnEvent(EventWaitHandle waitEvent, ref uint numWaiters, ReaderWriterLockSlim.TimeoutTracker timeout, bool isWriteWaiter)
		{
			waitEvent.Reset();
			numWaiters += 1U;
			this.fNoWaiters = false;
			if (this.numWriteWaiters == 1U)
			{
				this.SetWritersWaiting();
			}
			if (this.numWriteUpgradeWaiters == 1U)
			{
				this.SetUpgraderWaiting();
			}
			bool flag = false;
			this.ExitMyLock();
			try
			{
				flag = waitEvent.WaitOne(timeout.RemainingMilliseconds);
			}
			finally
			{
				this.EnterMyLock();
				numWaiters -= 1U;
				if (this.numWriteWaiters == 0U && this.numWriteUpgradeWaiters == 0U && this.numUpgradeWaiters == 0U && this.numReadWaiters == 0U)
				{
					this.fNoWaiters = true;
				}
				if (this.numWriteWaiters == 0U)
				{
					this.ClearWritersWaiting();
				}
				if (this.numWriteUpgradeWaiters == 0U)
				{
					this.ClearUpgraderWaiting();
				}
				if (!flag)
				{
					if (isWriteWaiter)
					{
						this.ExitAndWakeUpAppropriateReadWaiters();
					}
					else
					{
						this.ExitMyLock();
					}
				}
			}
			return flag;
		}

		// Token: 0x06000BA7 RID: 2983 RVA: 0x0002E3B0 File Offset: 0x0002C5B0
		private void ExitAndWakeUpAppropriateWaiters()
		{
			if (this.fNoWaiters)
			{
				this.ExitMyLock();
				return;
			}
			this.ExitAndWakeUpAppropriateWaitersPreferringWriters();
		}

		// Token: 0x06000BA8 RID: 2984 RVA: 0x0002E3C8 File Offset: 0x0002C5C8
		private void ExitAndWakeUpAppropriateWaitersPreferringWriters()
		{
			uint numReaders = this.GetNumReaders();
			if (this.fIsReentrant && this.numWriteUpgradeWaiters > 0U && this.fUpgradeThreadHoldingRead && numReaders == 2U)
			{
				this.ExitMyLock();
				this.waitUpgradeEvent.Set();
				return;
			}
			if (numReaders == 1U && this.numWriteUpgradeWaiters > 0U)
			{
				this.ExitMyLock();
				this.waitUpgradeEvent.Set();
				return;
			}
			if (numReaders == 0U && this.numWriteWaiters > 0U)
			{
				this.ExitMyLock();
				this.writeEvent.Set();
				return;
			}
			this.ExitAndWakeUpAppropriateReadWaiters();
		}

		// Token: 0x06000BA9 RID: 2985 RVA: 0x0002E454 File Offset: 0x0002C654
		private void ExitAndWakeUpAppropriateReadWaiters()
		{
			if (this.numWriteWaiters != 0U || this.numWriteUpgradeWaiters != 0U || this.fNoWaiters)
			{
				this.ExitMyLock();
				return;
			}
			bool flag = this.numReadWaiters > 0U;
			bool flag2 = this.numUpgradeWaiters != 0U && this.upgradeLockOwnerId == -1;
			this.ExitMyLock();
			if (flag)
			{
				this.readEvent.Set();
			}
			if (flag2)
			{
				this.upgradeEvent.Set();
			}
		}

		// Token: 0x06000BAA RID: 2986 RVA: 0x0002E4C1 File Offset: 0x0002C6C1
		private bool IsWriterAcquired()
		{
			return (this.owners & 3221225471U) == 0U;
		}

		// Token: 0x06000BAB RID: 2987 RVA: 0x0002E4D2 File Offset: 0x0002C6D2
		private void SetWriterAcquired()
		{
			this.owners |= 2147483648U;
		}

		// Token: 0x06000BAC RID: 2988 RVA: 0x0002E4E6 File Offset: 0x0002C6E6
		private void ClearWriterAcquired()
		{
			this.owners &= 2147483647U;
		}

		// Token: 0x06000BAD RID: 2989 RVA: 0x0002E4FA File Offset: 0x0002C6FA
		private void SetWritersWaiting()
		{
			this.owners |= 1073741824U;
		}

		// Token: 0x06000BAE RID: 2990 RVA: 0x0002E50E File Offset: 0x0002C70E
		private void ClearWritersWaiting()
		{
			this.owners &= 3221225471U;
		}

		// Token: 0x06000BAF RID: 2991 RVA: 0x0002E522 File Offset: 0x0002C722
		private void SetUpgraderWaiting()
		{
			this.owners |= 536870912U;
		}

		// Token: 0x06000BB0 RID: 2992 RVA: 0x0002E536 File Offset: 0x0002C736
		private void ClearUpgraderWaiting()
		{
			this.owners &= 3758096383U;
		}

		// Token: 0x06000BB1 RID: 2993 RVA: 0x0002E54A File Offset: 0x0002C74A
		private uint GetNumReaders()
		{
			return this.owners & 268435455U;
		}

		// Token: 0x06000BB2 RID: 2994 RVA: 0x0002E558 File Offset: 0x0002C758
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void EnterMyLock()
		{
			if (Interlocked.CompareExchange(ref this.myLock, 1, 0) != 0)
			{
				this.EnterMyLockSpin();
			}
		}

		// Token: 0x06000BB3 RID: 2995 RVA: 0x0002E570 File Offset: 0x0002C770
		private void EnterMyLockSpin()
		{
			int processorCount = PlatformHelper.ProcessorCount;
			int num = 0;
			for (;;)
			{
				if (num < 10 && processorCount > 1)
				{
					Thread.SpinWait(20 * (num + 1));
				}
				else if (num < 15)
				{
					Thread.Sleep(0);
				}
				else
				{
					Thread.Sleep(1);
				}
				if (this.myLock == 0 && Interlocked.CompareExchange(ref this.myLock, 1, 0) == 0)
				{
					break;
				}
				num++;
			}
		}

		// Token: 0x06000BB4 RID: 2996 RVA: 0x0002E5CB File Offset: 0x0002C7CB
		private void ExitMyLock()
		{
			Volatile.Write(ref this.myLock, 0);
		}

		// Token: 0x06000BB5 RID: 2997 RVA: 0x0002E5D9 File Offset: 0x0002C7D9
		private static void SpinWait(int SpinCount)
		{
			if (SpinCount < 5 && PlatformHelper.ProcessorCount > 1)
			{
				Thread.SpinWait(20 * SpinCount);
				return;
			}
			if (SpinCount < 17)
			{
				Thread.Sleep(0);
				return;
			}
			Thread.Sleep(1);
		}

		/// <summary>Releases all resources used by the current instance of the <see cref="T:System.Threading.ReaderWriterLockSlim" /> class.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000BB6 RID: 2998 RVA: 0x0002E603 File Offset: 0x0002C803
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06000BB7 RID: 2999 RVA: 0x0002E60C File Offset: 0x0002C80C
		private void Dispose(bool disposing)
		{
			if (disposing && !this.fDisposed)
			{
				if (this.WaitingReadCount > 0 || this.WaitingUpgradeCount > 0 || this.WaitingWriteCount > 0)
				{
					throw new SynchronizationLockException(global::SR.GetString("The lock is being disposed while still being used. It either is being held by a thread and/or has active waiters waiting to acquire the lock."));
				}
				if (this.IsReadLockHeld || this.IsUpgradeableReadLockHeld || this.IsWriteLockHeld)
				{
					throw new SynchronizationLockException(global::SR.GetString("The lock is being disposed while still being used. It either is being held by a thread and/or has active waiters waiting to acquire the lock."));
				}
				if (this.writeEvent != null)
				{
					this.writeEvent.Close();
					this.writeEvent = null;
				}
				if (this.readEvent != null)
				{
					this.readEvent.Close();
					this.readEvent = null;
				}
				if (this.upgradeEvent != null)
				{
					this.upgradeEvent.Close();
					this.upgradeEvent = null;
				}
				if (this.waitUpgradeEvent != null)
				{
					this.waitUpgradeEvent.Close();
					this.waitUpgradeEvent = null;
				}
				this.fDisposed = true;
			}
		}

		/// <summary>Gets a value that indicates whether the current thread has entered the lock in read mode.</summary>
		/// <returns>true if the current thread has entered read mode; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x06000BB8 RID: 3000 RVA: 0x0002E6EC File Offset: 0x0002C8EC
		public bool IsReadLockHeld
		{
			get
			{
				return this.RecursiveReadCount > 0;
			}
		}

		/// <summary>Gets a value that indicates whether the current thread has entered the lock in upgradeable mode. </summary>
		/// <returns>true if the current thread has entered upgradeable mode; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06000BB9 RID: 3001 RVA: 0x0002E6FA File Offset: 0x0002C8FA
		public bool IsUpgradeableReadLockHeld
		{
			get
			{
				return this.RecursiveUpgradeCount > 0;
			}
		}

		/// <summary>Gets a value that indicates whether the current thread has entered the lock in write mode.</summary>
		/// <returns>true if the current thread has entered write mode; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06000BBA RID: 3002 RVA: 0x0002E708 File Offset: 0x0002C908
		public bool IsWriteLockHeld
		{
			get
			{
				return this.RecursiveWriteCount > 0;
			}
		}

		/// <summary>Gets the number of times the current thread has entered the lock in read mode, as an indication of recursion.</summary>
		/// <returns>0 (zero) if the current thread has not entered read mode, 1 if the thread has entered read mode but has not entered it recursively, or n if the thread has entered the lock recursively n - 1 times.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x06000BBB RID: 3003 RVA: 0x0002E718 File Offset: 0x0002C918
		public int RecursiveReadCount
		{
			get
			{
				int num = 0;
				ReaderWriterCount threadRWCount = this.GetThreadRWCount(true);
				if (threadRWCount != null)
				{
					num = threadRWCount.readercount;
				}
				return num;
			}
		}

		/// <summary>Gets the number of times the current thread has entered the lock in upgradeable mode, as an indication of recursion.</summary>
		/// <returns>0 if the current thread has not entered upgradeable mode, 1 if the thread has entered upgradeable mode but has not entered it recursively, or n if the thread has entered upgradeable mode recursively n - 1 times.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001EA RID: 490
		// (get) Token: 0x06000BBC RID: 3004 RVA: 0x0002E73C File Offset: 0x0002C93C
		public int RecursiveUpgradeCount
		{
			get
			{
				if (this.fIsReentrant)
				{
					int num = 0;
					ReaderWriterCount threadRWCount = this.GetThreadRWCount(true);
					if (threadRWCount != null)
					{
						num = threadRWCount.upgradecount;
					}
					return num;
				}
				if (Thread.CurrentThread.ManagedThreadId == this.upgradeLockOwnerId)
				{
					return 1;
				}
				return 0;
			}
		}

		/// <summary>Gets the number of times the current thread has entered the lock in write mode, as an indication of recursion.</summary>
		/// <returns>0 if the current thread has not entered write mode, 1 if the thread has entered write mode but has not entered it recursively, or n if the thread has entered write mode recursively n - 1 times.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001EB RID: 491
		// (get) Token: 0x06000BBD RID: 3005 RVA: 0x0002E77C File Offset: 0x0002C97C
		public int RecursiveWriteCount
		{
			get
			{
				if (this.fIsReentrant)
				{
					int num = 0;
					ReaderWriterCount threadRWCount = this.GetThreadRWCount(true);
					if (threadRWCount != null)
					{
						num = threadRWCount.writercount;
					}
					return num;
				}
				if (Thread.CurrentThread.ManagedThreadId == this.writeLockOwnerId)
				{
					return 1;
				}
				return 0;
			}
		}

		/// <summary>Gets the total number of threads that are waiting to enter the lock in read mode.</summary>
		/// <returns>The total number of threads that are waiting to enter read mode.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001EC RID: 492
		// (get) Token: 0x06000BBE RID: 3006 RVA: 0x0002E7BC File Offset: 0x0002C9BC
		public int WaitingReadCount
		{
			get
			{
				return (int)this.numReadWaiters;
			}
		}

		/// <summary>Gets the total number of threads that are waiting to enter the lock in upgradeable mode.</summary>
		/// <returns>The total number of threads that are waiting to enter upgradeable mode.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001ED RID: 493
		// (get) Token: 0x06000BBF RID: 3007 RVA: 0x0002E7C4 File Offset: 0x0002C9C4
		public int WaitingUpgradeCount
		{
			get
			{
				return (int)this.numUpgradeWaiters;
			}
		}

		/// <summary>Gets the total number of threads that are waiting to enter the lock in write mode.</summary>
		/// <returns>The total number of threads that are waiting to enter write mode.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001EE RID: 494
		// (get) Token: 0x06000BC0 RID: 3008 RVA: 0x0002E7CC File Offset: 0x0002C9CC
		public int WaitingWriteCount
		{
			get
			{
				return (int)this.numWriteWaiters;
			}
		}

		// Token: 0x04000390 RID: 912
		private bool fIsReentrant;

		// Token: 0x04000391 RID: 913
		private int myLock;

		// Token: 0x04000392 RID: 914
		private uint numWriteWaiters;

		// Token: 0x04000393 RID: 915
		private uint numReadWaiters;

		// Token: 0x04000394 RID: 916
		private uint numWriteUpgradeWaiters;

		// Token: 0x04000395 RID: 917
		private uint numUpgradeWaiters;

		// Token: 0x04000396 RID: 918
		private bool fNoWaiters;

		// Token: 0x04000397 RID: 919
		private int upgradeLockOwnerId;

		// Token: 0x04000398 RID: 920
		private int writeLockOwnerId;

		// Token: 0x04000399 RID: 921
		private EventWaitHandle writeEvent;

		// Token: 0x0400039A RID: 922
		private EventWaitHandle readEvent;

		// Token: 0x0400039B RID: 923
		private EventWaitHandle upgradeEvent;

		// Token: 0x0400039C RID: 924
		private EventWaitHandle waitUpgradeEvent;

		// Token: 0x0400039D RID: 925
		private static long s_nextLockID;

		// Token: 0x0400039E RID: 926
		private long lockID;

		// Token: 0x0400039F RID: 927
		[ThreadStatic]
		private static ReaderWriterCount t_rwc;

		// Token: 0x040003A0 RID: 928
		private bool fUpgradeThreadHoldingRead;

		// Token: 0x040003A1 RID: 929
		private uint owners;

		// Token: 0x040003A2 RID: 930
		private bool fDisposed;

		// Token: 0x02000162 RID: 354
		private struct TimeoutTracker
		{
			// Token: 0x06000BC1 RID: 3009 RVA: 0x0002E7D4 File Offset: 0x0002C9D4
			public TimeoutTracker(int millisecondsTimeout)
			{
				if (millisecondsTimeout < -1)
				{
					throw new ArgumentOutOfRangeException("millisecondsTimeout");
				}
				this.m_total = millisecondsTimeout;
				if (this.m_total != -1 && this.m_total != 0)
				{
					this.m_start = Environment.TickCount;
					return;
				}
				this.m_start = 0;
			}

			// Token: 0x170001EF RID: 495
			// (get) Token: 0x06000BC2 RID: 3010 RVA: 0x0002E810 File Offset: 0x0002CA10
			public int RemainingMilliseconds
			{
				get
				{
					if (this.m_total == -1 || this.m_total == 0)
					{
						return this.m_total;
					}
					int num = Environment.TickCount - this.m_start;
					if (num < 0 || num >= this.m_total)
					{
						return 0;
					}
					return this.m_total - num;
				}
			}

			// Token: 0x170001F0 RID: 496
			// (get) Token: 0x06000BC3 RID: 3011 RVA: 0x0002E859 File Offset: 0x0002CA59
			public bool IsExpired
			{
				get
				{
					return this.RemainingMilliseconds == 0;
				}
			}

			// Token: 0x040003A3 RID: 931
			private int m_total;

			// Token: 0x040003A4 RID: 932
			private int m_start;
		}
	}
}
