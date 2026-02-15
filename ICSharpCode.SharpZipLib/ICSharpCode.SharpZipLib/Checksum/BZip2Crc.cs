using System;
using System.Runtime.CompilerServices;

namespace ICSharpCode.SharpZipLib.Checksum
{
	// Token: 0x020000C3 RID: 195
	public sealed class BZip2Crc : IChecksum
	{
		// Token: 0x060005DA RID: 1498 RVA: 0x0001B5D8 File Offset: 0x000197D8
		public BZip2Crc()
		{
			this.Reset();
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x0001B5E6 File Offset: 0x000197E6
		public void Reset()
		{
			this.checkValue = uint.MaxValue;
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x060005DC RID: 1500 RVA: 0x0001B5EF File Offset: 0x000197EF
		public long Value
		{
			get
			{
				return (long)((ulong)(~(ulong)this.checkValue));
			}
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x0001B5F9 File Offset: 0x000197F9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Update(int bval)
		{
			this.checkValue = BZip2Crc.crcTable[(int)((byte)((ulong)((this.checkValue >> 24) & 255U) ^ (ulong)((long)bval)))] ^ (this.checkValue << 8);
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x0001B624 File Offset: 0x00019824
		public void Update(byte[] buffer)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			this.Update(buffer, 0, buffer.Length);
		}

		// Token: 0x060005DF RID: 1503 RVA: 0x0001B63F File Offset: 0x0001983F
		public void Update(ArraySegment<byte> segment)
		{
			this.Update(segment.Array, segment.Offset, segment.Count);
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x0001B65C File Offset: 0x0001985C
		private void Update(byte[] data, int offset, int count)
		{
			int num = count % 16;
			int num2 = offset + count - num;
			while (offset != num2)
			{
				this.checkValue = CrcUtilities.UpdateDataForNormalPoly(data, offset, BZip2Crc.crcTable, this.checkValue);
				offset += 16;
			}
			if (num != 0)
			{
				this.SlowUpdateLoop(data, offset, num2 + num);
			}
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x0001B6A6 File Offset: 0x000198A6
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void SlowUpdateLoop(byte[] data, int offset, int end)
		{
			while (offset != end)
			{
				this.Update((int)data[offset++]);
			}
		}

		// Token: 0x0400045F RID: 1119
		private const uint crcInit = 4294967295U;

		// Token: 0x04000460 RID: 1120
		private static readonly uint[] crcTable = CrcUtilities.GenerateSlicingLookupTable(79764919U, false);

		// Token: 0x04000461 RID: 1121
		private uint checkValue;
	}
}
