using System;

namespace System.IO.Compression
{
	// Token: 0x02000017 RID: 23
	internal sealed class OutputBuffer
	{
		// Token: 0x0600008C RID: 140 RVA: 0x00004A62 File Offset: 0x00002C62
		internal void UpdateBuffer(byte[] output)
		{
			this._byteBuffer = output;
			this._pos = 0;
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600008D RID: 141 RVA: 0x00004A72 File Offset: 0x00002C72
		internal int BytesWritten
		{
			get
			{
				return this._pos;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600008E RID: 142 RVA: 0x00004A7A File Offset: 0x00002C7A
		internal int FreeBytes
		{
			get
			{
				return this._byteBuffer.Length - this._pos;
			}
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00004A8C File Offset: 0x00002C8C
		internal void WriteUInt16(ushort value)
		{
			byte[] byteBuffer = this._byteBuffer;
			int num = this._pos;
			this._pos = num + 1;
			byteBuffer[num] = (byte)value;
			byte[] byteBuffer2 = this._byteBuffer;
			num = this._pos;
			this._pos = num + 1;
			byteBuffer2[num] = (byte)(value >> 8);
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00004AD0 File Offset: 0x00002CD0
		internal void WriteBits(int n, uint bits)
		{
			this._bitBuf |= bits << this._bitCount;
			this._bitCount += n;
			if (this._bitCount >= 16)
			{
				byte[] byteBuffer = this._byteBuffer;
				int num = this._pos;
				this._pos = num + 1;
				byteBuffer[num] = (byte)this._bitBuf;
				byte[] byteBuffer2 = this._byteBuffer;
				num = this._pos;
				this._pos = num + 1;
				byteBuffer2[num] = (byte)(this._bitBuf >> 8);
				this._bitCount -= 16;
				this._bitBuf >>= 16;
			}
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00004B6C File Offset: 0x00002D6C
		internal void FlushBits()
		{
			while (this._bitCount >= 8)
			{
				byte[] byteBuffer = this._byteBuffer;
				int num = this._pos;
				this._pos = num + 1;
				byteBuffer[num] = (byte)this._bitBuf;
				this._bitCount -= 8;
				this._bitBuf >>= 8;
			}
			if (this._bitCount > 0)
			{
				byte[] byteBuffer2 = this._byteBuffer;
				int num = this._pos;
				this._pos = num + 1;
				byteBuffer2[num] = (byte)this._bitBuf;
				this._bitBuf = 0U;
				this._bitCount = 0;
			}
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00004BF5 File Offset: 0x00002DF5
		internal void WriteBytes(byte[] byteArray, int offset, int count)
		{
			if (this._bitCount == 0)
			{
				Array.Copy(byteArray, offset, this._byteBuffer, this._pos, count);
				this._pos += count;
				return;
			}
			this.WriteBytesUnaligned(byteArray, offset, count);
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00004C2C File Offset: 0x00002E2C
		private void WriteBytesUnaligned(byte[] byteArray, int offset, int count)
		{
			for (int i = 0; i < count; i++)
			{
				byte b = byteArray[offset + i];
				this.WriteByteUnaligned(b);
			}
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00004C52 File Offset: 0x00002E52
		private void WriteByteUnaligned(byte b)
		{
			this.WriteBits(8, (uint)b);
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000095 RID: 149 RVA: 0x00004C5C File Offset: 0x00002E5C
		internal int BitsInBuffer
		{
			get
			{
				return this._bitCount / 8 + 1;
			}
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00004C68 File Offset: 0x00002E68
		internal OutputBuffer.BufferState DumpState()
		{
			return new OutputBuffer.BufferState(this._pos, this._bitBuf, this._bitCount);
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00004C81 File Offset: 0x00002E81
		internal void RestoreState(OutputBuffer.BufferState state)
		{
			this._pos = state._pos;
			this._bitBuf = state._bitBuf;
			this._bitCount = state._bitCount;
		}

		// Token: 0x0400008D RID: 141
		private byte[] _byteBuffer;

		// Token: 0x0400008E RID: 142
		private int _pos;

		// Token: 0x0400008F RID: 143
		private uint _bitBuf;

		// Token: 0x04000090 RID: 144
		private int _bitCount;

		// Token: 0x02000018 RID: 24
		internal readonly struct BufferState
		{
			// Token: 0x06000098 RID: 152 RVA: 0x00004CA7 File Offset: 0x00002EA7
			internal BufferState(int pos, uint bitBuf, int bitCount)
			{
				this._pos = pos;
				this._bitBuf = bitBuf;
				this._bitCount = bitCount;
			}

			// Token: 0x04000091 RID: 145
			internal readonly int _pos;

			// Token: 0x04000092 RID: 146
			internal readonly uint _bitBuf;

			// Token: 0x04000093 RID: 147
			internal readonly int _bitCount;
		}
	}
}
