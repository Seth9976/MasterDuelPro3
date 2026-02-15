using System;
using System.Runtime.InteropServices;

namespace System.IO
{
	// Token: 0x02000360 RID: 864
	internal struct kevent : IDisposable
	{
		// Token: 0x06001550 RID: 5456 RVA: 0x0005B418 File Offset: 0x00059618
		public void Dispose()
		{
			if (this.udata != IntPtr.Zero)
			{
				Marshal.FreeHGlobal(this.udata);
			}
		}

		// Token: 0x04000CD4 RID: 3284
		public UIntPtr ident;

		// Token: 0x04000CD5 RID: 3285
		public EventFilter filter;

		// Token: 0x04000CD6 RID: 3286
		public EventFlags flags;

		// Token: 0x04000CD7 RID: 3287
		public FilterFlags fflags;

		// Token: 0x04000CD8 RID: 3288
		public IntPtr data;

		// Token: 0x04000CD9 RID: 3289
		public IntPtr udata;
	}
}
