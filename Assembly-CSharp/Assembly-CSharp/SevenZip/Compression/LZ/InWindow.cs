using System;
using System.IO;

namespace SevenZip.Compression.LZ
{
	// Token: 0x020001A3 RID: 419
	public class InWindow
	{
		// Token: 0x0600062B RID: 1579 RVA: 0x0001EDDC File Offset: 0x0001CFDC
		public void MoveBlock()
		{
			uint offset = this._bufferOffset + this._pos - this._keepSizeBefore;
			if (offset > 0U)
			{
				offset -= 1U;
			}
			uint numBytes = this._bufferOffset + this._streamPos - offset;
			for (uint i = 0U; i < numBytes; i += 1U)
			{
				this._bufferBase[(int)i] = this._bufferBase[(int)(offset + i)];
			}
			this._bufferOffset -= offset;
		}

		// Token: 0x0600062C RID: 1580 RVA: 0x0001EE44 File Offset: 0x0001D044
		public virtual void ReadBlock()
		{
			if (this._streamEndWasReached)
			{
				return;
			}
			for (;;)
			{
				int size = (int)(0U - this._bufferOffset + this._blockSize - this._streamPos);
				if (size == 0)
				{
					break;
				}
				int numReadBytes = this._stream.Read(this._bufferBase, (int)(this._bufferOffset + this._streamPos), size);
				if (numReadBytes == 0)
				{
					goto Block_3;
				}
				this._streamPos += (uint)numReadBytes;
				if (this._streamPos >= this._pos + this._keepSizeAfter)
				{
					this._posLimit = this._streamPos - this._keepSizeAfter;
				}
			}
			return;
			Block_3:
			this._posLimit = this._streamPos;
			if (this._bufferOffset + this._posLimit > this._pointerToLastSafePosition)
			{
				this._posLimit = this._pointerToLastSafePosition - this._bufferOffset;
			}
			this._streamEndWasReached = true;
		}

		// Token: 0x0600062D RID: 1581 RVA: 0x0001EF11 File Offset: 0x0001D111
		private void Free()
		{
			this._bufferBase = null;
		}

		// Token: 0x0600062E RID: 1582 RVA: 0x0001EF1C File Offset: 0x0001D11C
		public void Create(uint keepSizeBefore, uint keepSizeAfter, uint keepSizeReserv)
		{
			this._keepSizeBefore = keepSizeBefore;
			this._keepSizeAfter = keepSizeAfter;
			uint blockSize = keepSizeBefore + keepSizeAfter + keepSizeReserv;
			if (this._bufferBase == null || this._blockSize != blockSize)
			{
				this.Free();
				this._blockSize = blockSize;
				this._bufferBase = new byte[this._blockSize];
			}
			this._pointerToLastSafePosition = this._blockSize - keepSizeAfter;
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x0001EF7A File Offset: 0x0001D17A
		public void SetStream(Stream stream)
		{
			this._stream = stream;
		}

		// Token: 0x06000630 RID: 1584 RVA: 0x0001EF83 File Offset: 0x0001D183
		public void ReleaseStream()
		{
			this._stream = null;
		}

		// Token: 0x06000631 RID: 1585 RVA: 0x0001EF8C File Offset: 0x0001D18C
		public void Init()
		{
			this._bufferOffset = 0U;
			this._pos = 0U;
			this._streamPos = 0U;
			this._streamEndWasReached = false;
			this.ReadBlock();
		}

		// Token: 0x06000632 RID: 1586 RVA: 0x0001EFB0 File Offset: 0x0001D1B0
		public void MovePos()
		{
			this._pos += 1U;
			if (this._pos > this._posLimit)
			{
				if (this._bufferOffset + this._pos > this._pointerToLastSafePosition)
				{
					this.MoveBlock();
				}
				this.ReadBlock();
			}
		}

		// Token: 0x06000633 RID: 1587 RVA: 0x0001EFEF File Offset: 0x0001D1EF
		public byte GetIndexByte(int index)
		{
			checked
			{
				return this._bufferBase[(int)((IntPtr)(unchecked((ulong)(this._bufferOffset + this._pos) + (ulong)((long)index))))];
			}
		}

		// Token: 0x06000634 RID: 1588 RVA: 0x0001F00C File Offset: 0x0001D20C
		public uint GetMatchLen(int index, uint distance, uint limit)
		{
			if (this._streamEndWasReached && (ulong)this._pos + (ulong)((long)index) + (ulong)limit > (ulong)this._streamPos)
			{
				limit = this._streamPos - (uint)((ulong)this._pos + (ulong)((long)index));
			}
			distance += 1U;
			uint pby = this._bufferOffset + this._pos + (uint)index;
			uint i = 0U;
			while (i < limit && this._bufferBase[(int)(pby + i)] == this._bufferBase[(int)(pby + i - distance)])
			{
				i += 1U;
			}
			return i;
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x0001F085 File Offset: 0x0001D285
		public uint GetNumAvailableBytes()
		{
			return this._streamPos - this._pos;
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x0001F094 File Offset: 0x0001D294
		public void ReduceOffsets(int subValue)
		{
			this._bufferOffset += (uint)subValue;
			this._posLimit -= (uint)subValue;
			this._pos -= (uint)subValue;
			this._streamPos -= (uint)subValue;
		}

		// Token: 0x04000AF3 RID: 2803
		public byte[] _bufferBase;

		// Token: 0x04000AF4 RID: 2804
		private Stream _stream;

		// Token: 0x04000AF5 RID: 2805
		private uint _posLimit;

		// Token: 0x04000AF6 RID: 2806
		private bool _streamEndWasReached;

		// Token: 0x04000AF7 RID: 2807
		private uint _pointerToLastSafePosition;

		// Token: 0x04000AF8 RID: 2808
		public uint _bufferOffset;

		// Token: 0x04000AF9 RID: 2809
		public uint _blockSize;

		// Token: 0x04000AFA RID: 2810
		public uint _pos;

		// Token: 0x04000AFB RID: 2811
		private uint _keepSizeBefore;

		// Token: 0x04000AFC RID: 2812
		private uint _keepSizeAfter;

		// Token: 0x04000AFD RID: 2813
		public uint _streamPos;
	}
}
