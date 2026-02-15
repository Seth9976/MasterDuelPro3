using System;
using System.IO;

namespace SevenZip.Buffer
{
	// Token: 0x020001A6 RID: 422
	public class OutBuffer
	{
		// Token: 0x06000648 RID: 1608 RVA: 0x0001F475 File Offset: 0x0001D675
		public OutBuffer(uint bufferSize)
		{
			this.m_Buffer = new byte[bufferSize];
			this.m_BufferSize = bufferSize;
		}

		// Token: 0x06000649 RID: 1609 RVA: 0x0001F490 File Offset: 0x0001D690
		public void SetStream(Stream stream)
		{
			this.m_Stream = stream;
		}

		// Token: 0x0600064A RID: 1610 RVA: 0x0001F499 File Offset: 0x0001D699
		public void FlushStream()
		{
			this.m_Stream.Flush();
		}

		// Token: 0x0600064B RID: 1611 RVA: 0x0001F4A6 File Offset: 0x0001D6A6
		public void CloseStream()
		{
			this.m_Stream.Close();
		}

		// Token: 0x0600064C RID: 1612 RVA: 0x0001F4B3 File Offset: 0x0001D6B3
		public void ReleaseStream()
		{
			this.m_Stream = null;
		}

		// Token: 0x0600064D RID: 1613 RVA: 0x0001F4BC File Offset: 0x0001D6BC
		public void Init()
		{
			this.m_ProcessedSize = 0UL;
			this.m_Pos = 0U;
		}

		// Token: 0x0600064E RID: 1614 RVA: 0x0001F4D0 File Offset: 0x0001D6D0
		public void WriteByte(byte b)
		{
			byte[] buffer = this.m_Buffer;
			uint pos = this.m_Pos;
			this.m_Pos = pos + 1U;
			buffer[(int)pos] = b;
			if (this.m_Pos >= this.m_BufferSize)
			{
				this.FlushData();
			}
		}

		// Token: 0x0600064F RID: 1615 RVA: 0x0001F50A File Offset: 0x0001D70A
		public void FlushData()
		{
			if (this.m_Pos == 0U)
			{
				return;
			}
			this.m_Stream.Write(this.m_Buffer, 0, (int)this.m_Pos);
			this.m_Pos = 0U;
		}

		// Token: 0x06000650 RID: 1616 RVA: 0x0001F534 File Offset: 0x0001D734
		public ulong GetProcessedSize()
		{
			return this.m_ProcessedSize + (ulong)this.m_Pos;
		}

		// Token: 0x04000B0B RID: 2827
		private byte[] m_Buffer;

		// Token: 0x04000B0C RID: 2828
		private uint m_Pos;

		// Token: 0x04000B0D RID: 2829
		private uint m_BufferSize;

		// Token: 0x04000B0E RID: 2830
		private Stream m_Stream;

		// Token: 0x04000B0F RID: 2831
		private ulong m_ProcessedSize;
	}
}
