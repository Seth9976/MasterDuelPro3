using System;
using System.IO;

namespace Ionic.BZip2
{
	// Token: 0x0200004B RID: 75
	internal class WorkItem
	{
		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x060003A0 RID: 928 RVA: 0x00014EB9 File Offset: 0x000130B9
		// (set) Token: 0x060003A1 RID: 929 RVA: 0x00014EC1 File Offset: 0x000130C1
		public BZip2Compressor Compressor { get; private set; }

		// Token: 0x060003A2 RID: 930 RVA: 0x00014ECA File Offset: 0x000130CA
		public WorkItem(int ix, int blockSize)
		{
			this.ms = new MemoryStream();
			this.bw = new BitWriter(this.ms);
			this.Compressor = new BZip2Compressor(this.bw, blockSize);
			this.index = ix;
		}

		// Token: 0x04000233 RID: 563
		public int index;

		// Token: 0x04000234 RID: 564
		public MemoryStream ms;

		// Token: 0x04000235 RID: 565
		public int ordinal;

		// Token: 0x04000236 RID: 566
		public BitWriter bw;
	}
}
