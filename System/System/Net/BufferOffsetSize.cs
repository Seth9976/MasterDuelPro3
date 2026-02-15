using System;

namespace System.Net
{
	// Token: 0x020003C8 RID: 968
	internal class BufferOffsetSize
	{
		// Token: 0x0600182B RID: 6187 RVA: 0x00066918 File Offset: 0x00064B18
		internal BufferOffsetSize(byte[] buffer, int offset, int size, bool copyBuffer)
		{
			if (copyBuffer)
			{
				byte[] array = new byte[size];
				global::System.Buffer.BlockCopy(buffer, offset, array, 0, size);
				offset = 0;
				buffer = array;
			}
			this.Buffer = buffer;
			this.Offset = offset;
			this.Size = size;
		}

		// Token: 0x0600182C RID: 6188 RVA: 0x0006695B File Offset: 0x00064B5B
		internal BufferOffsetSize(byte[] buffer, bool copyBuffer)
			: this(buffer, 0, buffer.Length, copyBuffer)
		{
		}

		// Token: 0x04000F4A RID: 3914
		internal byte[] Buffer;

		// Token: 0x04000F4B RID: 3915
		internal int Offset;

		// Token: 0x04000F4C RID: 3916
		internal int Size;
	}
}
