using System;

namespace Mono.Net.Security
{
	// Token: 0x0200005F RID: 95
	internal class BufferOffsetSize2 : BufferOffsetSize
	{
		// Token: 0x06000119 RID: 281 RVA: 0x00004DBD File Offset: 0x00002FBD
		public BufferOffsetSize2(int size)
			: base(new byte[size], 0, 0)
		{
			this.InitialSize = size;
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00004DD4 File Offset: 0x00002FD4
		public void Reset()
		{
			this.Offset = (this.Size = 0);
			this.TotalBytes = 0;
			this.Buffer = new byte[this.InitialSize];
			this.Complete = false;
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00004E10 File Offset: 0x00003010
		public void MakeRoom(int size)
		{
			if (base.Remaining >= size)
			{
				return;
			}
			int num = size - base.Remaining;
			if (this.Offset == 0 && this.Size == 0)
			{
				this.Buffer = new byte[size];
				return;
			}
			byte[] array = new byte[this.Buffer.Length + num];
			this.Buffer.CopyTo(array, 0);
			this.Buffer = array;
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00004E71 File Offset: 0x00003071
		public void AppendData(byte[] buffer, int offset, int size)
		{
			this.MakeRoom(size);
			global::System.Buffer.BlockCopy(buffer, offset, this.Buffer, base.EndOffset, size);
			this.Size += size;
		}

		// Token: 0x040000E7 RID: 231
		public readonly int InitialSize;
	}
}
