using System;

namespace System.Drawing
{
	// Token: 0x02000077 RID: 119
	internal struct CarbonContext : IMacContext
	{
		// Token: 0x06000440 RID: 1088 RVA: 0x0000D6DF File Offset: 0x0000B8DF
		public CarbonContext(IntPtr port, IntPtr ctx, int width, int height)
		{
			this.port = port;
			this.ctx = ctx;
			this.width = width;
			this.height = height;
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x0000D6FE File Offset: 0x0000B8FE
		public void Synchronize()
		{
			MacSupport.CGContextSynchronize(this.ctx);
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x0000D70B File Offset: 0x0000B90B
		public void Release()
		{
			MacSupport.ReleaseContext(this.port, this.ctx);
		}

		// Token: 0x0400021D RID: 541
		public IntPtr port;

		// Token: 0x0400021E RID: 542
		public IntPtr ctx;

		// Token: 0x0400021F RID: 543
		public int width;

		// Token: 0x04000220 RID: 544
		public int height;
	}
}
