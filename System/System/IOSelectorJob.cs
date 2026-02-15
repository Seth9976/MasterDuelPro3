using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace System
{
	// Token: 0x0200010A RID: 266
	[StructLayout(LayoutKind.Sequential)]
	internal class IOSelectorJob : IThreadPoolWorkItem
	{
		// Token: 0x0600054C RID: 1356 RVA: 0x0001BEC0 File Offset: 0x0001A0C0
		public IOSelectorJob(IOOperation operation, IOAsyncCallback callback, IOAsyncResult state)
		{
			this.operation = operation;
			this.callback = callback;
			this.state = state;
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x0001BEDD File Offset: 0x0001A0DD
		void IThreadPoolWorkItem.ExecuteWorkItem()
		{
			this.callback(this.state);
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x00002FA0 File Offset: 0x000011A0
		void IThreadPoolWorkItem.MarkAborted(ThreadAbortException tae)
		{
		}

		// Token: 0x0600054F RID: 1359 RVA: 0x0001BEF0 File Offset: 0x0001A0F0
		public void MarkDisposed()
		{
			this.state.CompleteDisposed();
		}

		// Token: 0x04000483 RID: 1155
		private IOOperation operation;

		// Token: 0x04000484 RID: 1156
		private IOAsyncCallback callback;

		// Token: 0x04000485 RID: 1157
		private IOAsyncResult state;
	}
}
