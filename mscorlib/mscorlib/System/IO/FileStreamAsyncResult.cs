using System;
using System.Threading;

namespace System.IO
{
	// Token: 0x020007DC RID: 2012
	internal class FileStreamAsyncResult : IAsyncResult
	{
		// Token: 0x060040B4 RID: 16564 RVA: 0x000F94F7 File Offset: 0x000F76F7
		public FileStreamAsyncResult(AsyncCallback cb, object state)
		{
			this.state = state;
			this.realcb = cb;
			if (this.realcb != null)
			{
				this.cb = new AsyncCallback(FileStreamAsyncResult.CBWrapper);
			}
			this.wh = new ManualResetEvent(false);
		}

		// Token: 0x060040B5 RID: 16565 RVA: 0x000F9533 File Offset: 0x000F7733
		private static void CBWrapper(IAsyncResult ares)
		{
			((FileStreamAsyncResult)ares).realcb.BeginInvoke(ares, null, null);
		}

		// Token: 0x17000A58 RID: 2648
		// (get) Token: 0x060040B6 RID: 16566 RVA: 0x000F9549 File Offset: 0x000F7749
		public object AsyncState
		{
			get
			{
				return this.state;
			}
		}

		// Token: 0x17000A59 RID: 2649
		// (get) Token: 0x060040B7 RID: 16567 RVA: 0x000F9551 File Offset: 0x000F7751
		public bool CompletedSynchronously
		{
			get
			{
				return this.completedSynch;
			}
		}

		// Token: 0x17000A5A RID: 2650
		// (get) Token: 0x060040B8 RID: 16568 RVA: 0x000F9559 File Offset: 0x000F7759
		public WaitHandle AsyncWaitHandle
		{
			get
			{
				return this.wh;
			}
		}

		// Token: 0x17000A5B RID: 2651
		// (get) Token: 0x060040B9 RID: 16569 RVA: 0x000F9561 File Offset: 0x000F7761
		public bool IsCompleted
		{
			get
			{
				return this.completed;
			}
		}

		// Token: 0x040020E2 RID: 8418
		private object state;

		// Token: 0x040020E3 RID: 8419
		private bool completed;

		// Token: 0x040020E4 RID: 8420
		private ManualResetEvent wh;

		// Token: 0x040020E5 RID: 8421
		private AsyncCallback cb;

		// Token: 0x040020E6 RID: 8422
		private bool completedSynch;

		// Token: 0x040020E7 RID: 8423
		public int Count;

		// Token: 0x040020E8 RID: 8424
		public int OriginalCount;

		// Token: 0x040020E9 RID: 8425
		public int BytesRead;

		// Token: 0x040020EA RID: 8426
		private AsyncCallback realcb;
	}
}
