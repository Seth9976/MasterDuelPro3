using System;
using System.Runtime.InteropServices;

namespace AssetStudio
{
	// Token: 0x0200016E RID: 366
	[ComVisible(false)]
	internal static class HalfHelper
	{
		// Token: 0x060004EF RID: 1263 RVA: 0x00016854 File Offset: 0x00014A54
		private static uint ConvertMantissa(int i)
		{
			uint j = (uint)((uint)i << 13);
			uint e = 0U;
			while ((j & 8388608U) == 0U)
			{
				e -= 8388608U;
				j <<= 1;
			}
			j &= 4286578687U;
			e += 947912704U;
			return j | e;
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x00016894 File Offset: 0x00014A94
		private static uint[] GenerateMantissaTable()
		{
			uint[] mantissaTable = new uint[2048];
			mantissaTable[0] = 0U;
			for (int i = 1; i < 1024; i++)
			{
				mantissaTable[i] = HalfHelper.ConvertMantissa(i);
			}
			for (int j = 1024; j < 2048; j++)
			{
				mantissaTable[j] = (uint)(939524096 + (j - 1024 << 13));
			}
			return mantissaTable;
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x000168F4 File Offset: 0x00014AF4
		private static uint[] GenerateExponentTable()
		{
			uint[] exponentTable = new uint[64];
			exponentTable[0] = 0U;
			for (int i = 1; i < 31; i++)
			{
				exponentTable[i] = (uint)((uint)i << 23);
			}
			exponentTable[31] = 1199570944U;
			exponentTable[32] = 2147483648U;
			for (int j = 33; j < 63; j++)
			{
				exponentTable[j] = (uint)((ulong)int.MinValue + (ulong)((long)((long)(j - 32) << 23)));
			}
			exponentTable[63] = 3347054592U;
			return exponentTable;
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x00016960 File Offset: 0x00014B60
		private static ushort[] GenerateOffsetTable()
		{
			ushort[] offsetTable = new ushort[64];
			offsetTable[0] = 0;
			for (int i = 1; i < 32; i++)
			{
				offsetTable[i] = 1024;
			}
			offsetTable[32] = 0;
			for (int j = 33; j < 64; j++)
			{
				offsetTable[j] = 1024;
			}
			return offsetTable;
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x000169AC File Offset: 0x00014BAC
		private static ushort[] GenerateBaseTable()
		{
			ushort[] baseTable = new ushort[512];
			for (int i = 0; i < 256; i++)
			{
				sbyte e = (sbyte)(127 - i);
				if (e > 24)
				{
					baseTable[i | 0] = 0;
					baseTable[i | 256] = 32768;
				}
				else if (e > 14)
				{
					baseTable[i | 0] = (ushort)(1024 >> (int)(18 + e));
					baseTable[i | 256] = (ushort)((1024 >> (int)(18 + e)) | 32768);
				}
				else if (e >= -15)
				{
					baseTable[i | 0] = (ushort)(15 - e << 10);
					baseTable[i | 256] = (ushort)(((int)(15 - e) << 10) | 32768);
				}
				else if (e > -128)
				{
					baseTable[i | 0] = 31744;
					baseTable[i | 256] = 64512;
				}
				else
				{
					baseTable[i | 0] = 31744;
					baseTable[i | 256] = 64512;
				}
			}
			return baseTable;
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x00016A98 File Offset: 0x00014C98
		private static sbyte[] GenerateShiftTable()
		{
			sbyte[] shiftTable = new sbyte[512];
			for (int i = 0; i < 256; i++)
			{
				sbyte e = (sbyte)(127 - i);
				if (e > 24)
				{
					shiftTable[i | 0] = 24;
					shiftTable[i | 256] = 24;
				}
				else if (e > 14)
				{
					shiftTable[i | 0] = e - 1;
					shiftTable[i | 256] = e - 1;
				}
				else if (e >= -15)
				{
					shiftTable[i | 0] = 13;
					shiftTable[i | 256] = 13;
				}
				else if (e > -128)
				{
					shiftTable[i | 0] = 24;
					shiftTable[i | 256] = 24;
				}
				else
				{
					shiftTable[i | 0] = 13;
					shiftTable[i | 256] = 13;
				}
			}
			return shiftTable;
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x00016B47 File Offset: 0x00014D47
		public static float HalfToSingle(Half half)
		{
			return BitConverter.ToSingle(BitConverter.GetBytes(HalfHelper.mantissaTable[(int)(HalfHelper.offsetTable[half.value >> 10] + (half.value & 1023))] + HalfHelper.exponentTable[half.value >> 10]), 0);
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x00016B88 File Offset: 0x00014D88
		public static Half SingleToHalf(float single)
		{
			uint value = BitConverter.ToUInt32(BitConverter.GetBytes(single), 0);
			return Half.ToHalf((ushort)((uint)HalfHelper.baseTable[(int)((value >> 23) & 511U)] + ((value & 8388607U) >> (int)HalfHelper.shiftTable[(int)(value >> 23)])));
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x00016BCE File Offset: 0x00014DCE
		public static Half Negate(Half half)
		{
			return Half.ToHalf(half.value ^ 32768);
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x00016BE2 File Offset: 0x00014DE2
		public static Half Abs(Half half)
		{
			return Half.ToHalf(half.value & 32767);
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x00016BF6 File Offset: 0x00014DF6
		public static bool IsNaN(Half half)
		{
			return (half.value & 32767) > 31744;
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x00016C0B File Offset: 0x00014E0B
		public static bool IsInfinity(Half half)
		{
			return (half.value & 32767) == 31744;
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x00016C20 File Offset: 0x00014E20
		public static bool IsPositiveInfinity(Half half)
		{
			return half.value == 31744;
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x00016C2F File Offset: 0x00014E2F
		public static bool IsNegativeInfinity(Half half)
		{
			return half.value == 64512;
		}

		// Token: 0x04000996 RID: 2454
		private static uint[] mantissaTable = HalfHelper.GenerateMantissaTable();

		// Token: 0x04000997 RID: 2455
		private static uint[] exponentTable = HalfHelper.GenerateExponentTable();

		// Token: 0x04000998 RID: 2456
		private static ushort[] offsetTable = HalfHelper.GenerateOffsetTable();

		// Token: 0x04000999 RID: 2457
		private static ushort[] baseTable = HalfHelper.GenerateBaseTable();

		// Token: 0x0400099A RID: 2458
		private static sbyte[] shiftTable = HalfHelper.GenerateShiftTable();
	}
}
