using System;
using System.Threading;

namespace System.Windows.Forms
{
	// Token: 0x02000010 RID: 16
	internal class AsyncMethodData
	{
		// Token: 0x0400006F RID: 111
		public IntPtr Handle;

		// Token: 0x04000070 RID: 112
		public Delegate Method;

		// Token: 0x04000071 RID: 113
		public object[] Args;

		// Token: 0x04000072 RID: 114
		public AsyncMethodResult Result;

		// Token: 0x04000073 RID: 115
		public ExecutionContext Context;

		// Token: 0x04000074 RID: 116
		public SynchronizationContext SyncContext;
	}
}
