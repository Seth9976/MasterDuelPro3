using System;

namespace Mono.Net.Security
{
	// Token: 0x0200005E RID: 94
	internal class BufferOffsetSize
	{
		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000115 RID: 277 RVA: 0x00004D0F File Offset: 0x00002F0F
		public int EndOffset
		{
			get
			{
				return this.Offset + this.Size;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000116 RID: 278 RVA: 0x00004D1E File Offset: 0x00002F1E
		public int Remaining
		{
			get
			{
				return this.Buffer.Length - this.Offset - this.Size;
			}
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00004D38 File Offset: 0x00002F38
		public BufferOffsetSize(byte[] buffer, int offset, int size)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (size < 0 || offset + size > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("size");
			}
			this.Buffer = buffer;
			this.Offset = offset;
			this.Size = size;
			this.Complete = false;
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00004D9B File Offset: 0x00002F9B
		public override string ToString()
		{
			return string.Format("[BufferOffsetSize: {0} {1}]", this.Offset, this.Size);
		}

		// Token: 0x040000E2 RID: 226
		public byte[] Buffer;

		// Token: 0x040000E3 RID: 227
		public int Offset;

		// Token: 0x040000E4 RID: 228
		public int Size;

		// Token: 0x040000E5 RID: 229
		public int TotalBytes;

		// Token: 0x040000E6 RID: 230
		public bool Complete;
	}
}
