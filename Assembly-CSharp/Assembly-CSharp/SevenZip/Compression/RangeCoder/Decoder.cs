using System;
using System.IO;

namespace SevenZip.Compression.RangeCoder
{
	// Token: 0x0200018E RID: 398
	internal class Decoder
	{
		// Token: 0x0600059D RID: 1437 RVA: 0x0001A87C File Offset: 0x00018A7C
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

		// Token: 0x0600059E RID: 1438 RVA: 0x0001A8C5 File Offset: 0x00018AC5
		public void ReleaseStream()
		{
			this.Stream = null;
		}

		// Token: 0x0600059F RID: 1439 RVA: 0x0001A8CE File Offset: 0x00018ACE
		public void CloseStream()
		{
			this.Stream.Close();
		}

		// Token: 0x060005A0 RID: 1440 RVA: 0x0001A8DB File Offset: 0x00018ADB
		public void Normalize()
		{
			while (this.Range < 16777216U)
			{
				this.Code = (this.Code << 8) | (uint)((byte)this.Stream.ReadByte());
				this.Range <<= 8;
			}
		}

		// Token: 0x060005A1 RID: 1441 RVA: 0x0001A915 File Offset: 0x00018B15
		public void Normalize2()
		{
			if (this.Range < 16777216U)
			{
				this.Code = (this.Code << 8) | (uint)((byte)this.Stream.ReadByte());
				this.Range <<= 8;
			}
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x0001A950 File Offset: 0x00018B50
		public uint GetThreshold(uint total)
		{
			return this.Code / (this.Range /= total);
		}

		// Token: 0x060005A3 RID: 1443 RVA: 0x0001A975 File Offset: 0x00018B75
		public void Decode(uint start, uint size, uint total)
		{
			this.Code -= start * this.Range;
			this.Range *= size;
			this.Normalize();
		}

		// Token: 0x060005A4 RID: 1444 RVA: 0x0001A9A0 File Offset: 0x00018BA0
		public uint DecodeDirectBits(int numTotalBits)
		{
			uint range = this.Range;
			uint code = this.Code;
			uint result = 0U;
			for (int i = numTotalBits; i > 0; i--)
			{
				range >>= 1;
				uint t = code - range >> 31;
				code -= range & (t - 1U);
				result = (result << 1) | (1U - t);
				if (range < 16777216U)
				{
					code = (code << 8) | (uint)((byte)this.Stream.ReadByte());
					range <<= 8;
				}
			}
			this.Range = range;
			this.Code = code;
			return result;
		}

		// Token: 0x060005A5 RID: 1445 RVA: 0x0001AA14 File Offset: 0x00018C14
		public uint DecodeBit(uint size0, int numTotalBits)
		{
			uint newBound = (this.Range >> numTotalBits) * size0;
			uint symbol;
			if (this.Code < newBound)
			{
				symbol = 0U;
				this.Range = newBound;
			}
			else
			{
				symbol = 1U;
				this.Code -= newBound;
				this.Range -= newBound;
			}
			this.Normalize();
			return symbol;
		}

		// Token: 0x04000A3E RID: 2622
		public const uint kTopValue = 16777216U;

		// Token: 0x04000A3F RID: 2623
		public uint Range;

		// Token: 0x04000A40 RID: 2624
		public uint Code;

		// Token: 0x04000A41 RID: 2625
		public Stream Stream;
	}
}
