using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace System
{
	// Token: 0x02000109 RID: 265
	[StructLayout(LayoutKind.Sequential)]
	internal abstract class IOAsyncResult : IAsyncResult
	{
		// Token: 0x06000541 RID: 1345 RVA: 0x000026E5 File Offset: 0x000008E5
		protected IOAsyncResult()
		{
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x0001BD9F File Offset: 0x00019F9F
		protected void Init(AsyncCallback async_callback, object async_state)
		{
			this.async_callback = async_callback;
			this.async_state = async_state;
			this.completed = false;
			this.completed_synchronously = false;
			if (this.wait_handle != null)
			{
				this.wait_handle.Reset();
			}
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x0001BDD1 File Offset: 0x00019FD1
		protected IOAsyncResult(AsyncCallback async_callback, object async_state)
		{
			this.async_callback = async_callback;
			this.async_state = async_state;
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000544 RID: 1348 RVA: 0x0001BDE7 File Offset: 0x00019FE7
		public AsyncCallback AsyncCallback
		{
			get
			{
				return this.async_callback;
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000545 RID: 1349 RVA: 0x0001BDEF File Offset: 0x00019FEF
		public object AsyncState
		{
			get
			{
				return this.async_state;
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000546 RID: 1350 RVA: 0x0001BDF8 File Offset: 0x00019FF8
		public WaitHandle AsyncWaitHandle
		{
			get
			{
				WaitHandle waitHandle;
				lock (this)
				{
					if (this.wait_handle == null)
					{
						this.wait_handle = new ManualResetEvent(this.completed);
					}
					waitHandle = this.wait_handle;
				}
				return waitHandle;
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000547 RID: 1351 RVA: 0x0001BE50 File Offset: 0x0001A050
		// (set) Token: 0x06000548 RID: 1352 RVA: 0x0001BE58 File Offset: 0x0001A058
		public bool CompletedSynchronously
		{
			get
			{
				return this.completed_synchronously;
			}
			protected set
			{
				this.completed_synchronously = value;
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000549 RID: 1353 RVA: 0x0001BE61 File Offset: 0x0001A061
		// (set) Token: 0x0600054A RID: 1354 RVA: 0x0001BE6C File Offset: 0x0001A06C
		public bool IsCompleted
		{
			get
			{
				return this.completed;
			}
			protected set
			{
				this.completed = value;
				lock (this)
				{
					if (value && this.wait_handle != null)
					{
						this.wait_handle.Set();
					}
				}
			}
		}

		// Token: 0x0600054B RID: 1355
		internal abstract void CompleteDisposed();

		// Token: 0x0400047E RID: 1150
		private AsyncCallback async_callback;

		// Token: 0x0400047F RID: 1151
		private object async_state;

		// Token: 0x04000480 RID: 1152
		private ManualResetEvent wait_handle;

		// Token: 0x04000481 RID: 1153
		private bool completed_synchronously;

		// Token: 0x04000482 RID: 1154
		private bool completed;
	}
}
