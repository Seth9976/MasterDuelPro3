using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x0200000C RID: 12
	internal struct IntRect
	{
		// Token: 0x06000032 RID: 50 RVA: 0x0000287B File Offset: 0x00000A7B
		public IntRect(long l, long t, long r, long b)
		{
			this.left = l;
			this.top = t;
			this.right = r;
			this.bottom = b;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x0000289A File Offset: 0x00000A9A
		public IntRect(IntRect ir)
		{
			this.left = ir.left;
			this.top = ir.top;
			this.right = ir.right;
			this.bottom = ir.bottom;
		}

		// Token: 0x04000019 RID: 25
		public long left;

		// Token: 0x0400001A RID: 26
		public long top;

		// Token: 0x0400001B RID: 27
		public long right;

		// Token: 0x0400001C RID: 28
		public long bottom;
	}
}
