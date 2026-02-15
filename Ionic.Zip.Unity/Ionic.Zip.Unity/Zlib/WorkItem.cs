using System;

namespace Ionic.Zlib
{
	// Token: 0x0200005D RID: 93
	internal class WorkItem
	{
		// Token: 0x0600044E RID: 1102 RVA: 0x0001BCD0 File Offset: 0x00019ED0
		public WorkItem(int size, CompressionLevel compressLevel, CompressionStrategy strategy, int ix)
		{
			this.buffer = new byte[size];
			int num = size + (size / 32768 + 1) * 5 * 2;
			this.compressed = new byte[num];
			this.compressor = new ZlibCodec();
			this.compressor.InitializeDeflate(compressLevel, false);
			this.compressor.OutputBuffer = this.compressed;
			this.compressor.InputBuffer = this.buffer;
			this.index = ix;
		}

		// Token: 0x0400032B RID: 811
		public byte[] buffer;

		// Token: 0x0400032C RID: 812
		public byte[] compressed;

		// Token: 0x0400032D RID: 813
		public int crc;

		// Token: 0x0400032E RID: 814
		public int index;

		// Token: 0x0400032F RID: 815
		public int ordinal;

		// Token: 0x04000330 RID: 816
		public int inputBytesAvailable;

		// Token: 0x04000331 RID: 817
		public int compressedBytesAvailable;

		// Token: 0x04000332 RID: 818
		public ZlibCodec compressor;
	}
}
