using System;
using System.IO;

namespace SevenZip.Compression.RangeCoder
{
	// Token: 0x0200000B RID: 11
	internal class Encoder
	{
		// Token: 0x06000010 RID: 16 RVA: 0x000021B7 File Offset: 0x000003B7
		public void SetStream(Stream stream)
		{
			this.Stream = stream;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000021C1 File Offset: 0x000003C1
		public void ReleaseStream()
		{
			this.Stream = null;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000021CB File Offset: 0x000003CB
		public void Init()
		{
			this.StartPosition = this.Stream.Position;
			this.Low = 0UL;
			this.Range = uint.MaxValue;
			this._cacheSize = 1U;
			this._cache = 0;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000021FC File Offset: 0x000003FC
		public void FlushData()
		{
			for (int i = 0; i < 5; i++)
			{
				this.ShiftLow();
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002221 File Offset: 0x00000421
		public void FlushStream()
		{
			this.Stream.Flush();
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002230 File Offset: 0x00000430
		public void CloseStream()
		{
			this.Stream.Close();
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002240 File Offset: 0x00000440
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

		// Token: 0x06000017 RID: 23 RVA: 0x000022A8 File Offset: 0x000004A8
		public void ShiftLow()
		{
			bool flag = (uint)this.Low < 4278190080U || (uint)(this.Low >> 32) == 1U;
			if (flag)
			{
				byte b = this._cache;
				uint num;
				do
				{
					this.Stream.WriteByte((byte)((ulong)b + (this.Low >> 32)));
					b = byte.MaxValue;
					num = this._cacheSize - 1U;
					this._cacheSize = num;
				}
				while (num > 0U);
				this._cache = (byte)((uint)this.Low >> 24);
			}
			this._cacheSize += 1U;
			this.Low = (ulong)((ulong)((uint)this.Low) << 8);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x0000234C File Offset: 0x0000054C
		public void EncodeDirectBits(uint v, int numTotalBits)
		{
			for (int i = numTotalBits - 1; i >= 0; i--)
			{
				this.Range >>= 1;
				bool flag = ((v >> i) & 1U) == 1U;
				if (flag)
				{
					this.Low += (ulong)this.Range;
				}
				bool flag2 = this.Range < 16777216U;
				if (flag2)
				{
					this.Range <<= 8;
					this.ShiftLow();
				}
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000023CC File Offset: 0x000005CC
		public void EncodeBit(uint size0, int numTotalBits, uint symbol)
		{
			uint num = (this.Range >> numTotalBits) * size0;
			bool flag = symbol == 0U;
			if (flag)
			{
				this.Range = num;
			}
			else
			{
				this.Low += (ulong)num;
				this.Range -= num;
			}
			while (this.Range < 16777216U)
			{
				this.Range <<= 8;
				this.ShiftLow();
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002444 File Offset: 0x00000644
		public long GetProcessedSizeAdd()
		{
			return (long)((ulong)this._cacheSize + (ulong)this.Stream.Position - (ulong)this.StartPosition + 4UL);
		}

		// Token: 0x04000013 RID: 19
		public const uint kTopValue = 16777216U;

		// Token: 0x04000014 RID: 20
		private Stream Stream;

		// Token: 0x04000015 RID: 21
		public ulong Low;

		// Token: 0x04000016 RID: 22
		public uint Range;

		// Token: 0x04000017 RID: 23
		private uint _cacheSize;

		// Token: 0x04000018 RID: 24
		private byte _cache;

		// Token: 0x04000019 RID: 25
		private long StartPosition;
	}
}
