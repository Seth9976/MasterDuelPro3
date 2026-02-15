using System;

namespace System.Threading
{
	// Token: 0x0200027F RID: 639
	internal class LockQueue
	{
		// Token: 0x060017BA RID: 6074 RVA: 0x0005BB45 File Offset: 0x00059D45
		public LockQueue(ReaderWriterLock rwlock)
		{
			this.rwlock = rwlock;
		}

		// Token: 0x060017BB RID: 6075 RVA: 0x0005BB54 File Offset: 0x00059D54
		public bool Wait(int timeout)
		{
			bool flag = false;
			bool flag3;
			try
			{
				lock (this)
				{
					this.lockCount++;
					Monitor.Exit(this.rwlock);
					flag = true;
					flag3 = Monitor.Wait(this, timeout);
				}
			}
			finally
			{
				if (flag)
				{
					Monitor.Enter(this.rwlock);
					this.lockCount--;
				}
			}
			return flag3;
		}

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x060017BC RID: 6076 RVA: 0x0005BBD8 File Offset: 0x00059DD8
		public bool IsEmpty
		{
			get
			{
				bool flag2;
				lock (this)
				{
					flag2 = this.lockCount == 0;
				}
				return flag2;
			}
		}

		// Token: 0x060017BD RID: 6077 RVA: 0x0005BC18 File Offset: 0x00059E18
		public void Pulse()
		{
			lock (this)
			{
				Monitor.Pulse(this);
			}
		}

		// Token: 0x04000B3A RID: 2874
		private ReaderWriterLock rwlock;

		// Token: 0x04000B3B RID: 2875
		private int lockCount;
	}
}
