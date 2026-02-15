using System;
using System.Runtime.CompilerServices;

namespace ICSharpCode.SharpZipLib.Checksum
{
	// Token: 0x020000C4 RID: 196
	public sealed class Crc32 : IChecksum
	{
		// Token: 0x060005E3 RID: 1507 RVA: 0x0001B6CE File Offset: 0x000198CE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static uint ComputeCrc32(uint oldCrc, byte bval)
		{
			return Crc32.crcTable[(int)((oldCrc ^ (uint)bval) & 255U)] ^ (oldCrc >> 8);
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x0001B6E3 File Offset: 0x000198E3
		public Crc32()
		{
			this.Reset();
		}

		// Token: 0x060005E5 RID: 1509 RVA: 0x0001B6F1 File Offset: 0x000198F1
		public void Reset()
		{
			this.checkValue = Crc32.crcInit;
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x060005E6 RID: 1510 RVA: 0x0001B6FE File Offset: 0x000198FE
		public long Value
		{
			get
			{
				return (long)((ulong)(this.checkValue ^ Crc32.crcXor));
			}
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x0001B70D File Offset: 0x0001990D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Update(int bval)
		{
			checked
			{
				this.checkValue = Crc32.crcTable[(int)((IntPtr)(unchecked((ulong)this.checkValue ^ (ulong)((long)bval)) & 255UL))] ^ (this.checkValue >> 8);
			}
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x0001B736 File Offset: 0x00019936
		public void Update(byte[] buffer)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			this.Update(buffer, 0, buffer.Length);
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x0001B751 File Offset: 0x00019951
		public void Update(ArraySegment<byte> segment)
		{
			this.Update(segment.Array, segment.Offset, segment.Count);
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x0001B770 File Offset: 0x00019970
		private void Update(byte[] data, int offset, int count)
		{
			int num = count % 16;
			int num2 = offset + count - num;
			while (offset != num2)
			{
				this.checkValue = CrcUtilities.UpdateDataForReversedPoly(data, offset, Crc32.crcTable, this.checkValue);
				offset += 16;
			}
			if (num != 0)
			{
				this.SlowUpdateLoop(data, offset, num2 + num);
			}
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x0001B7BA File Offset: 0x000199BA
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void SlowUpdateLoop(byte[] data, int offset, int end)
		{
			while (offset != end)
			{
				this.Update((int)data[offset++]);
			}
		}

		// Token: 0x04000462 RID: 1122
		private static readonly uint crcInit = uint.MaxValue;

		// Token: 0x04000463 RID: 1123
		private static readonly uint crcXor = uint.MaxValue;

		// Token: 0x04000464 RID: 1124
		private static readonly uint[] crcTable = CrcUtilities.GenerateSlicingLookupTable(3988292384U, true);

		// Token: 0x04000465 RID: 1125
		private uint checkValue;
	}
}
