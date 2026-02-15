using System;
using System.IO;

namespace SevenZip.Buffer
{
	// Token: 0x02000025 RID: 37
	public class OutBuffer
	{
		// Token: 0x060000CE RID: 206 RVA: 0x000082A0 File Offset: 0x000064A0
		public OutBuffer(uint bufferSize)
		{
			this.m_Buffer = new byte[bufferSize];
			this.m_BufferSize = bufferSize;
		}

		// Token: 0x060000CF RID: 207 RVA: 0x000082BD File Offset: 0x000064BD
		public void SetStream(Stream stream)
		{
			this.m_Stream = stream;
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x000082C7 File Offset: 0x000064C7
		public void FlushStream()
		{
			this.m_Stream.Flush();
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x000082D6 File Offset: 0x000064D6
		public void CloseStream()
		{
			this.m_Stream.Close();
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x000082E5 File Offset: 0x000064E5
		public void ReleaseStream()
		{
			this.m_Stream = null;
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x000082EF File Offset: 0x000064EF
		public void Init()
		{
			this.m_ProcessedSize = 0UL;
			this.m_Pos = 0U;
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00008304 File Offset: 0x00006504
		public void WriteByte(byte b)
		{
			byte[] buffer = this.m_Buffer;
			uint pos = this.m_Pos;
			this.m_Pos = pos + 1U;
			buffer[(int)pos] = b;
			bool flag = this.m_Pos >= this.m_BufferSize;
			if (flag)
			{
				this.FlushData();
			}
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00008348 File Offset: 0x00006548
		public void FlushData()
		{
			bool flag = this.m_Pos == 0U;
			if (!flag)
			{
				this.m_Stream.Write(this.m_Buffer, 0, (int)this.m_Pos);
				this.m_Pos = 0U;
			}
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00008388 File Offset: 0x00006588
		public ulong GetProcessedSize()
		{
			return this.m_ProcessedSize + (ulong)this.m_Pos;
		}

		// Token: 0x040000E9 RID: 233
		private byte[] m_Buffer;

		// Token: 0x040000EA RID: 234
		private uint m_Pos;

		// Token: 0x040000EB RID: 235
		private uint m_BufferSize;

		// Token: 0x040000EC RID: 236
		private Stream m_Stream;

		// Token: 0x040000ED RID: 237
		private ulong m_ProcessedSize;
	}
}
