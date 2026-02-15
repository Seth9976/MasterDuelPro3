using System;

namespace System.Drawing
{
	// Token: 0x02000078 RID: 120
	internal class CocoaContext : IMacContext
	{
		// Token: 0x06000443 RID: 1091 RVA: 0x0000D71E File Offset: 0x0000B91E
		public CocoaContext(IntPtr focusHandle, IntPtr ctx, int width, int height)
		{
			this.focusHandle = focusHandle;
			this.ctx = ctx;
			this.width = width;
			this.height = height;
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x0000D743 File Offset: 0x0000B943
		public void Synchronize()
		{
			MacSupport.CGContextSynchronize(this.ctx);
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x0000D750 File Offset: 0x0000B950
		public void Release()
		{
			if (IntPtr.Zero != this.focusHandle)
			{
				MacSupport.CGContextFlush(this.ctx);
			}
			MacSupport.CGContextRestoreGState(this.ctx);
			if (IntPtr.Zero != this.focusHandle)
			{
				MacSupport.objc_msgSend(this.focusHandle, MacSupport.sel_registerName("unlockFocus"));
			}
		}

		// Token: 0x04000221 RID: 545
		public IntPtr focusHandle;

		// Token: 0x04000222 RID: 546
		public IntPtr ctx;

		// Token: 0x04000223 RID: 547
		public int width;

		// Token: 0x04000224 RID: 548
		public int height;
	}
}
