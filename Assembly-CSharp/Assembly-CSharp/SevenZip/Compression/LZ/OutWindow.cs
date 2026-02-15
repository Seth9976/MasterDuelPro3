using System;
using System.IO;

namespace SevenZip.Compression.LZ
{
	// Token: 0x020001A4 RID: 420
	public class OutWindow
	{
		// Token: 0x06000638 RID: 1592 RVA: 0x0001F0CE File Offset: 0x0001D2CE
		public void Create(uint windowSize)
		{
			if (this._windowSize != windowSize)
			{
				this._buffer = new byte[windowSize];
			}
			this._windowSize = windowSize;
			this._pos = 0U;
			this._streamPos = 0U;
		}

		// Token: 0x06000639 RID: 1593 RVA: 0x0001F0FA File Offset: 0x0001D2FA
		public void Init(Stream stream, bool solid)
		{
			this.ReleaseStream();
			this._stream = stream;
			if (!solid)
			{
				this._streamPos = 0U;
				this._pos = 0U;
				this.TrainSize = 0U;
			}
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x0001F124 File Offset: 0x0001D324
		public bool Train(Stream stream)
		{
			long len = stream.Length;
			uint size = ((len < (long)((ulong)this._windowSize)) ? ((uint)len) : this._windowSize);
			this.TrainSize = size;
			stream.Position = len - (long)((ulong)size);
			this._streamPos = (this._pos = 0U);
			while (size > 0U)
			{
				uint curSize = this._windowSize - this._pos;
				if (size < curSize)
				{
					curSize = size;
				}
				int numReadBytes = stream.Read(this._buffer, (int)this._pos, (int)curSize);
				if (numReadBytes == 0)
				{
					return false;
				}
				size -= (uint)numReadBytes;
				this._pos += (uint)numReadBytes;
				this._streamPos += (uint)numReadBytes;
				if (this._pos == this._windowSize)
				{
					this._streamPos = (this._pos = 0U);
				}
			}
			return true;
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x0001F1E5 File Offset: 0x0001D3E5
		public void ReleaseStream()
		{
			this.Flush();
			this._stream = null;
		}

		// Token: 0x0600063C RID: 1596 RVA: 0x0001F1F4 File Offset: 0x0001D3F4
		public void Flush()
		{
			uint size = this._pos - this._streamPos;
			if (size == 0U)
			{
				return;
			}
			this._stream.Write(this._buffer, (int)this._streamPos, (int)size);
			if (this._pos >= this._windowSize)
			{
				this._pos = 0U;
			}
			this._streamPos = this._pos;
		}

		// Token: 0x0600063D RID: 1597 RVA: 0x0001F24C File Offset: 0x0001D44C
		public void CopyBlock(uint distance, uint len)
		{
			uint pos = this._pos - distance - 1U;
			if (pos >= this._windowSize)
			{
				pos += this._windowSize;
			}
			while (len > 0U)
			{
				if (pos >= this._windowSize)
				{
					pos = 0U;
				}
				byte[] buffer = this._buffer;
				uint pos2 = this._pos;
				this._pos = pos2 + 1U;
				buffer[(int)pos2] = this._buffer[(int)pos++];
				if (this._pos >= this._windowSize)
				{
					this.Flush();
				}
				len -= 1U;
			}
		}

		// Token: 0x0600063E RID: 1598 RVA: 0x0001F2C4 File Offset: 0x0001D4C4
		public void PutByte(byte b)
		{
			byte[] buffer = this._buffer;
			uint pos = this._pos;
			this._pos = pos + 1U;
			buffer[(int)pos] = b;
			if (this._pos >= this._windowSize)
			{
				this.Flush();
			}
		}

		// Token: 0x0600063F RID: 1599 RVA: 0x0001F300 File Offset: 0x0001D500
		public byte GetByte(uint distance)
		{
			uint pos = this._pos - distance - 1U;
			if (pos >= this._windowSize)
			{
				pos += this._windowSize;
			}
			return this._buffer[(int)pos];
		}

		// Token: 0x04000AFE RID: 2814
		private byte[] _buffer;

		// Token: 0x04000AFF RID: 2815
		private uint _pos;

		// Token: 0x04000B00 RID: 2816
		private uint _windowSize;

		// Token: 0x04000B01 RID: 2817
		private uint _streamPos;

		// Token: 0x04000B02 RID: 2818
		private Stream _stream;

		// Token: 0x04000B03 RID: 2819
		public uint TrainSize;
	}
}
