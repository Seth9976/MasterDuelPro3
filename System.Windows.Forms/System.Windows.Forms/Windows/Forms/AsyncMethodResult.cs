using System;
using System.Threading;

namespace System.Windows.Forms
{
	// Token: 0x02000011 RID: 17
	internal class AsyncMethodResult : IAsyncResult
	{
		// Token: 0x0600003E RID: 62 RVA: 0x00002D12 File Offset: 0x00000F12
		public AsyncMethodResult()
		{
			this.handle = new ManualResetEvent(false);
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600003F RID: 63 RVA: 0x00002D28 File Offset: 0x00000F28
		public virtual WaitHandle AsyncWaitHandle
		{
			get
			{
				WaitHandle waitHandle;
				lock (this)
				{
					waitHandle = this.handle;
				}
				return waitHandle;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00002D68 File Offset: 0x00000F68
		public object AsyncState
		{
			get
			{
				return this.state;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000041 RID: 65 RVA: 0x00002D70 File Offset: 0x00000F70
		public bool CompletedSynchronously
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00002D74 File Offset: 0x00000F74
		public bool IsCompleted
		{
			get
			{
				bool flag2;
				lock (this)
				{
					flag2 = this.completed;
				}
				return flag2;
			}
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002DB4 File Offset: 0x00000FB4
		public object EndInvoke()
		{
			lock (this)
			{
				if (this.completed)
				{
					if (this.exception == null)
					{
						return this.return_value;
					}
					throw this.exception;
				}
			}
			this.handle.WaitOne();
			if (this.exception != null)
			{
				throw this.exception;
			}
			return this.return_value;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002E2C File Offset: 0x0000102C
		public void Complete(object result)
		{
			lock (this)
			{
				this.completed = true;
				this.return_value = result;
				this.handle.Set();
			}
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002E7C File Offset: 0x0000107C
		public void CompleteWithException(Exception ex)
		{
			lock (this)
			{
				this.completed = true;
				this.exception = ex;
				this.handle.Set();
			}
		}

		// Token: 0x04000075 RID: 117
		private ManualResetEvent handle;

		// Token: 0x04000076 RID: 118
		private object state;

		// Token: 0x04000077 RID: 119
		private bool completed;

		// Token: 0x04000078 RID: 120
		private object return_value;

		// Token: 0x04000079 RID: 121
		private Exception exception;
	}
}
