using System;

namespace Mono
{
	// Token: 0x0200000C RID: 12
	internal struct CFRange
	{
		// Token: 0x06000022 RID: 34 RVA: 0x00002369 File Offset: 0x00000569
		public CFRange(int loc, int len)
		{
			this.Location = (IntPtr)loc;
			this.Length = (IntPtr)len;
		}

		// Token: 0x04000017 RID: 23
		public IntPtr Location;

		// Token: 0x04000018 RID: 24
		public IntPtr Length;
	}
}
