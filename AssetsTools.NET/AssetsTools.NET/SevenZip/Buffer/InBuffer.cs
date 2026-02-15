using System;
using System.IO;

namespace SevenZip.Buffer
{
	// Token: 0x02000024 RID: 36
	public class InBuffer
	{
		// Token: 0x060000C7 RID: 199 RVA: 0x0000811A File Offset: 0x0000631A
		public InBuffer(uint bufferSize)
		{
			this.m_Buffer = new byte[bufferSize];
			this.m_BufferSize = bufferSize;
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00008137 File Offset: 0x00006337
		public void Init(Stream stream)
		{
			this.m_Stream = stream;
			this.m_ProcessedSize = 0UL;
			this.m_Limit = 0U;
			this.m_Pos = 0U;
			this.m_StreamWasExhausted = false;
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00008160 File Offset: 0x00006360
		public bool ReadBlock()
		{
			bool streamWasExhausted = this.m_StreamWasExhausted;
			bool flag;
			if (streamWasExhausted)
			{
				flag = false;
			}
			else
			{
				this.m_ProcessedSize += (ulong)this.m_Pos;
				int num = this.m_Stream.Read(this.m_Buffer, 0, (int)this.m_BufferSize);
				this.m_Pos = 0U;
				this.m_Limit = (uint)num;
				this.m_StreamWasExhausted = num == 0;
				flag = !this.m_StreamWasExhausted;
			}
			return flag;
		}

		// Token: 0x060000CA RID: 202 RVA: 0x000081CE File Offset: 0x000063CE
		public void ReleaseStream()
		{
			this.m_Stream = null;
		}

		// Token: 0x060000CB RID: 203 RVA: 0x000081D8 File Offset: 0x000063D8
		public bool ReadByte(byte b)
		{
			bool flag = this.m_Pos >= this.m_Limit;
			if (flag)
			{
				bool flag2 = !this.ReadBlock();
				if (flag2)
				{
					return false;
				}
			}
			byte[] buffer = this.m_Buffer;
			uint pos = this.m_Pos;
			this.m_Pos = pos + 1U;
			b = buffer[(int)pos];
			return true;
		}

		// Token: 0x060000CC RID: 204 RVA: 0x0000822C File Offset: 0x0000642C
		public byte ReadByte()
		{
			bool flag = this.m_Pos >= this.m_Limit;
			if (flag)
			{
				bool flag2 = !this.ReadBlock();
				if (flag2)
				{
					return byte.MaxValue;
				}
			}
			byte[] buffer = this.m_Buffer;
			uint pos = this.m_Pos;
			this.m_Pos = pos + 1U;
			return buffer[(int)pos];
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00008280 File Offset: 0x00006480
		public ulong GetProcessedSize()
		{
			return this.m_ProcessedSize + (ulong)this.m_Pos;
		}

		// Token: 0x040000E2 RID: 226
		private byte[] m_Buffer;

		// Token: 0x040000E3 RID: 227
		private uint m_Pos;

		// Token: 0x040000E4 RID: 228
		private uint m_Limit;

		// Token: 0x040000E5 RID: 229
		private uint m_BufferSize;

		// Token: 0x040000E6 RID: 230
		private Stream m_Stream;

		// Token: 0x040000E7 RID: 231
		private bool m_StreamWasExhausted;

		// Token: 0x040000E8 RID: 232
		private ulong m_ProcessedSize;
	}
}
