using System;
using System.IO;

namespace SevenZip.Compression.RangeCoder
{
	// Token: 0x0200018D RID: 397
	internal class Encoder
	{
		// Token: 0x06000591 RID: 1425 RVA: 0x0001A618 File Offset: 0x00018818
		public void SetStream(Stream stream)
		{
			this.Stream = stream;
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x0001A621 File Offset: 0x00018821
		public void ReleaseStream()
		{
			this.Stream = null;
		}

		// Token: 0x06000593 RID: 1427 RVA: 0x0001A62A File Offset: 0x0001882A
		public void Init()
		{
			this.StartPosition = this.Stream.Position;
			this.Low = 0UL;
			this.Range = uint.MaxValue;
			this._cacheSize = 1U;
			this._cache = 0;
		}

		// Token: 0x06000594 RID: 1428 RVA: 0x0001A65C File Offset: 0x0001885C
		public void FlushData()
		{
			for (int i = 0; i < 5; i++)
			{
				this.ShiftLow();
			}
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x0001A67B File Offset: 0x0001887B
		public void FlushStream()
		{
			this.Stream.Flush();
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x0001A688 File Offset: 0x00018888
		public void CloseStream()
		{
			this.Stream.Close();
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x0001A698 File Offset: 0x00018898
		public void Encode(uint start, uint size, uint total)
		{
			this.Low += (ulong)(start * (this.Range /= total));
			this.Range *= size;
			while (this.Range < 16777216U)
			{
				this.Range <<= 8;
				this.ShiftLow();
			}
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x0001A6F8 File Offset: 0x000188F8
		public void ShiftLow()
		{
			if ((uint)this.Low < 4278190080U || (uint)(this.Low >> 32) == 1U)
			{
				byte temp = this._cache;
				uint num;
				do
				{
					this.Stream.WriteByte((byte)((ulong)temp + (this.Low >> 32)));
					temp = byte.MaxValue;
					num = this._cacheSize - 1U;
					this._cacheSize = num;
				}
				while (num != 0U);
				this._cache = (byte)((uint)this.Low >> 24);
			}
			this._cacheSize += 1U;
			this.Low = (ulong)((ulong)((uint)this.Low) << 8);
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x0001A788 File Offset: 0x00018988
		public void EncodeDirectBits(uint v, int numTotalBits)
		{
			for (int i = numTotalBits - 1; i >= 0; i--)
			{
				this.Range >>= 1;
				if (((v >> i) & 1U) == 1U)
				{
					this.Low += (ulong)this.Range;
				}
				if (this.Range < 16777216U)
				{
					this.Range <<= 8;
					this.ShiftLow();
				}
			}
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x0001A7F4 File Offset: 0x000189F4
		public void EncodeBit(uint size0, int numTotalBits, uint symbol)
		{
			uint newBound = (this.Range >> numTotalBits) * size0;
			if (symbol == 0U)
			{
				this.Range = newBound;
			}
			else
			{
				this.Low += (ulong)newBound;
				this.Range -= newBound;
			}
			while (this.Range < 16777216U)
			{
				this.Range <<= 8;
				this.ShiftLow();
			}
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x0001A85B File Offset: 0x00018A5B
		public long GetProcessedSizeAdd()
		{
			return (long)((ulong)this._cacheSize + (ulong)this.Stream.Position - (ulong)this.StartPosition + 4UL);
		}

		// Token: 0x04000A37 RID: 2615
		public const uint kTopValue = 16777216U;

		// Token: 0x04000A38 RID: 2616
		private Stream Stream;

		// Token: 0x04000A39 RID: 2617
		public ulong Low;

		// Token: 0x04000A3A RID: 2618
		public uint Range;

		// Token: 0x04000A3B RID: 2619
		private uint _cacheSize;

		// Token: 0x04000A3C RID: 2620
		private byte _cache;

		// Token: 0x04000A3D RID: 2621
		private long StartPosition;
	}
}
