using System;
using System.Threading;

namespace System.Net
{
	// Token: 0x02000414 RID: 1044
	internal class HttpStreamAsyncResult : IAsyncResult
	{
		// Token: 0x06001A17 RID: 6679 RVA: 0x00070A0B File Offset: 0x0006EC0B
		public void Complete(Exception e)
		{
			this.Error = e;
			this.Complete();
		}

		// Token: 0x06001A18 RID: 6680 RVA: 0x00070A1C File Offset: 0x0006EC1C
		public void Complete()
		{
			object obj = this.locker;
			lock (obj)
			{
				if (!this.completed)
				{
					this.completed = true;
					if (this.handle != null)
					{
						this.handle.Set();
					}
					if (this.Callback != null)
					{
						this.Callback.BeginInvoke(this, null, null);
					}
				}
			}
		}

		// Token: 0x170005A5 RID: 1445
		// (get) Token: 0x06001A19 RID: 6681 RVA: 0x00070A94 File Offset: 0x0006EC94
		public object AsyncState
		{
			get
			{
				return this.State;
			}
		}

		// Token: 0x170005A6 RID: 1446
		// (get) Token: 0x06001A1A RID: 6682 RVA: 0x00070A9C File Offset: 0x0006EC9C
		public WaitHandle AsyncWaitHandle
		{
			get
			{
				object obj = this.locker;
				lock (obj)
				{
					if (this.handle == null)
					{
						this.handle = new ManualResetEvent(this.completed);
					}
				}
				return this.handle;
			}
		}

		// Token: 0x170005A7 RID: 1447
		// (get) Token: 0x06001A1B RID: 6683 RVA: 0x00070AF8 File Offset: 0x0006ECF8
		public bool CompletedSynchronously
		{
			get
			{
				return this.SynchRead == this.Count;
			}
		}

		// Token: 0x170005A8 RID: 1448
		// (get) Token: 0x06001A1C RID: 6684 RVA: 0x00070B08 File Offset: 0x0006ED08
		public bool IsCompleted
		{
			get
			{
				object obj = this.locker;
				bool flag2;
				lock (obj)
				{
					flag2 = this.completed;
				}
				return flag2;
			}
		}

		// Token: 0x040010B1 RID: 4273
		private object locker = new object();

		// Token: 0x040010B2 RID: 4274
		private ManualResetEvent handle;

		// Token: 0x040010B3 RID: 4275
		private bool completed;

		// Token: 0x040010B4 RID: 4276
		internal byte[] Buffer;

		// Token: 0x040010B5 RID: 4277
		internal int Offset;

		// Token: 0x040010B6 RID: 4278
		internal int Count;

		// Token: 0x040010B7 RID: 4279
		internal AsyncCallback Callback;

		// Token: 0x040010B8 RID: 4280
		internal object State;

		// Token: 0x040010B9 RID: 4281
		internal int SynchRead;

		// Token: 0x040010BA RID: 4282
		internal Exception Error;
	}
}
