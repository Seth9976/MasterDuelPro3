using System;
using System.IO;

namespace SevenZip.Compression.RangeCoder
{
	// Token: 0x0200000C RID: 12
	internal class Decoder
	{
		// Token: 0x0600001C RID: 28 RVA: 0x0000247C File Offset: 0x0000067C
		public void Init(Stream stream)
		{
			this.Stream = stream;
			this.Code = 0U;
			this.Range = uint.MaxValue;
			for (int i = 0; i < 5; i++)
			{
				this.Code = (this.Code << 8) | (uint)((byte)this.Stream.ReadByte());
			}
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000024CA File Offset: 0x000006CA
		public void ReleaseStream()
		{
			this.Stream = null;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000024D4 File Offset: 0x000006D4
		public void CloseStream()
		{
			this.Stream.Close();
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000024E4 File Offset: 0x000006E4
		public void Normalize()
		{
			while (this.Range < 16777216U)
			{
				this.Code = (this.Code << 8) | (uint)((byte)this.Stream.ReadByte());
				this.Range <<= 8;
			}
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002530 File Offset: 0x00000730
		public void Normalize2()
		{
			bool flag = this.Range < 16777216U;
			if (flag)
			{
				this.Code = (this.Code << 8) | (uint)((byte)this.Stream.ReadByte());
				this.Range <<= 8;
			}
		}

		// Token: 0x06000021 RID: 33 RVA: 0x0000257C File Offset: 0x0000077C
		public uint GetThreshold(uint total)
		{
			return this.Code / (this.Range /= total);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000025A6 File Offset: 0x000007A6
		public void Decode(uint start, uint size, uint total)
		{
			this.Code -= start * this.Range;
			this.Range *= size;
			this.Normalize();
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000025D4 File Offset: 0x000007D4
		public uint DecodeDirectBits(int numTotalBits)
		{
			uint num = this.Range;
			uint num2 = this.Code;
			uint num3 = 0U;
			for (int i = numTotalBits; i > 0; i--)
			{
				num >>= 1;
				uint num4 = num2 - num >> 31;
				num2 -= num & (num4 - 1U);
				num3 = (num3 << 1) | (1U - num4);
				bool flag = num < 16777216U;
				if (flag)
				{
					num2 = (num2 << 8) | (uint)((byte)this.Stream.ReadByte());
					num <<= 8;
				}
			}
			this.Range = num;
			this.Code = num2;
			return num3;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002660 File Offset: 0x00000860
		public uint DecodeBit(uint size0, int numTotalBits)
		{
			uint num = (this.Range >> numTotalBits) * size0;
			bool flag = this.Code < num;
			uint num2;
			if (flag)
			{
				num2 = 0U;
				this.Range = num;
			}
			else
			{
				num2 = 1U;
				this.Code -= num;
				this.Range -= num;
			}
			this.Normalize();
			return num2;
		}

		// Token: 0x0400001A RID: 26
		public const uint kTopValue = 16777216U;

		// Token: 0x0400001B RID: 27
		public uint Range;

		// Token: 0x0400001C RID: 28
		public uint Code;

		// Token: 0x0400001D RID: 29
		public Stream Stream;
	}
}
