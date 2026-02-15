using System;
using System.IO;

namespace SevenZip.Buffer
{
	// Token: 0x020001A5 RID: 421
	public class InBuffer
	{
		// Token: 0x06000641 RID: 1601 RVA: 0x0001F332 File Offset: 0x0001D532
		public InBuffer(uint bufferSize)
		{
			this.m_Buffer = new byte[bufferSize];
			this.m_BufferSize = bufferSize;
		}

		// Token: 0x06000642 RID: 1602 RVA: 0x0001F34D File Offset: 0x0001D54D
		public void Init(Stream stream)
		{
			this.m_Stream = stream;
			this.m_ProcessedSize = 0UL;
			this.m_Limit = 0U;
			this.m_Pos = 0U;
			this.m_StreamWasExhausted = false;
		}

		// Token: 0x06000643 RID: 1603 RVA: 0x0001F374 File Offset: 0x0001D574
		public bool ReadBlock()
		{
			if (this.m_StreamWasExhausted)
			{
				return false;
			}
			this.m_ProcessedSize += (ulong)this.m_Pos;
			int aNumProcessedBytes = this.m_Stream.Read(this.m_Buffer, 0, (int)this.m_BufferSize);
			this.m_Pos = 0U;
			this.m_Limit = (uint)aNumProcessedBytes;
			this.m_StreamWasExhausted = aNumProcessedBytes == 0;
			return !this.m_StreamWasExhausted;
		}

		// Token: 0x06000644 RID: 1604 RVA: 0x0001F3D9 File Offset: 0x0001D5D9
		public void ReleaseStream()
		{
			this.m_Stream = null;
		}

		// Token: 0x06000645 RID: 1605 RVA: 0x0001F3E4 File Offset: 0x0001D5E4
		public bool ReadByte(byte b)
		{
			if (this.m_Pos >= this.m_Limit && !this.ReadBlock())
			{
				return false;
			}
			byte[] buffer = this.m_Buffer;
			uint pos = this.m_Pos;
			this.m_Pos = pos + 1U;
			b = buffer[(int)pos];
			return true;
		}

		// Token: 0x06000646 RID: 1606 RVA: 0x0001F424 File Offset: 0x0001D624
		public byte ReadByte()
		{
			if (this.m_Pos >= this.m_Limit && !this.ReadBlock())
			{
				return byte.MaxValue;
			}
			byte[] buffer = this.m_Buffer;
			uint pos = this.m_Pos;
			this.m_Pos = pos + 1U;
			return buffer[(int)pos];
		}

		// Token: 0x06000647 RID: 1607 RVA: 0x0001F465 File Offset: 0x0001D665
		public ulong GetProcessedSize()
		{
			return this.m_ProcessedSize + (ulong)this.m_Pos;
		}

		// Token: 0x04000B04 RID: 2820
		private byte[] m_Buffer;

		// Token: 0x04000B05 RID: 2821
		private uint m_Pos;

		// Token: 0x04000B06 RID: 2822
		private uint m_Limit;

		// Token: 0x04000B07 RID: 2823
		private uint m_BufferSize;

		// Token: 0x04000B08 RID: 2824
		private Stream m_Stream;

		// Token: 0x04000B09 RID: 2825
		private bool m_StreamWasExhausted;

		// Token: 0x04000B0A RID: 2826
		private ulong m_ProcessedSize;
	}
}
