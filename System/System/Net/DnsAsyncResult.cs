using System;
using System.Threading;

namespace System.Net
{
	// Token: 0x02000405 RID: 1029
	internal class DnsAsyncResult : IAsyncResult
	{
		// Token: 0x0600197E RID: 6526 RVA: 0x0006D18E File Offset: 0x0006B38E
		public DnsAsyncResult(AsyncCallback cb, object state)
		{
			this.callback = cb;
			this.state = state;
		}

		// Token: 0x0600197F RID: 6527 RVA: 0x0006D1A4 File Offset: 0x0006B3A4
		public void SetCompleted(bool synch, IPHostEntry entry, Exception e)
		{
			this.synch = synch;
			this.entry = entry;
			this.exc = e;
			lock (this)
			{
				if (this.is_completed)
				{
					return;
				}
				this.is_completed = true;
				if (this.handle != null)
				{
					this.handle.Set();
				}
			}
			if (this.callback != null)
			{
				ThreadPool.QueueUserWorkItem(DnsAsyncResult.internal_cb, this);
			}
		}

		// Token: 0x06001980 RID: 6528 RVA: 0x0006D228 File Offset: 0x0006B428
		public void SetCompleted(bool synch, Exception e)
		{
			this.SetCompleted(synch, null, e);
		}

		// Token: 0x06001981 RID: 6529 RVA: 0x0006D233 File Offset: 0x0006B433
		public void SetCompleted(bool synch, IPHostEntry entry)
		{
			this.SetCompleted(synch, entry, null);
		}

		// Token: 0x06001982 RID: 6530 RVA: 0x0006D240 File Offset: 0x0006B440
		private static void CB(object _this)
		{
			DnsAsyncResult dnsAsyncResult = (DnsAsyncResult)_this;
			dnsAsyncResult.callback(dnsAsyncResult);
		}

		// Token: 0x17000578 RID: 1400
		// (get) Token: 0x06001983 RID: 6531 RVA: 0x0006D260 File Offset: 0x0006B460
		public object AsyncState
		{
			get
			{
				return this.state;
			}
		}

		// Token: 0x17000579 RID: 1401
		// (get) Token: 0x06001984 RID: 6532 RVA: 0x0006D268 File Offset: 0x0006B468
		public WaitHandle AsyncWaitHandle
		{
			get
			{
				lock (this)
				{
					if (this.handle == null)
					{
						this.handle = new ManualResetEvent(this.is_completed);
					}
				}
				return this.handle;
			}
		}

		// Token: 0x1700057A RID: 1402
		// (get) Token: 0x06001985 RID: 6533 RVA: 0x0006D2BC File Offset: 0x0006B4BC
		public Exception Exception
		{
			get
			{
				return this.exc;
			}
		}

		// Token: 0x1700057B RID: 1403
		// (get) Token: 0x06001986 RID: 6534 RVA: 0x0006D2C4 File Offset: 0x0006B4C4
		public IPHostEntry HostEntry
		{
			get
			{
				return this.entry;
			}
		}

		// Token: 0x1700057C RID: 1404
		// (get) Token: 0x06001987 RID: 6535 RVA: 0x0006D2CC File Offset: 0x0006B4CC
		public bool CompletedSynchronously
		{
			get
			{
				return this.synch;
			}
		}

		// Token: 0x1700057D RID: 1405
		// (get) Token: 0x06001988 RID: 6536 RVA: 0x0006D2D4 File Offset: 0x0006B4D4
		public bool IsCompleted
		{
			get
			{
				bool flag2;
				lock (this)
				{
					flag2 = this.is_completed;
				}
				return flag2;
			}
		}

		// Token: 0x04001033 RID: 4147
		private static WaitCallback internal_cb = new WaitCallback(DnsAsyncResult.CB);

		// Token: 0x04001034 RID: 4148
		private ManualResetEvent handle;

		// Token: 0x04001035 RID: 4149
		private bool synch;

		// Token: 0x04001036 RID: 4150
		private bool is_completed;

		// Token: 0x04001037 RID: 4151
		private AsyncCallback callback;

		// Token: 0x04001038 RID: 4152
		private object state;

		// Token: 0x04001039 RID: 4153
		private IPHostEntry entry;

		// Token: 0x0400103A RID: 4154
		private Exception exc;
	}
}
