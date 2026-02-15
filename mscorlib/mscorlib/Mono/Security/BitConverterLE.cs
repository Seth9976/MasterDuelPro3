using System;

namespace Mono.Security
{
	// Token: 0x02000064 RID: 100
	internal sealed class BitConverterLE
	{
		// Token: 0x06000142 RID: 322 RVA: 0x00007D38 File Offset: 0x00005F38
		private unsafe static byte[] GetUIntBytes(byte* bytes)
		{
			if (BitConverter.IsLittleEndian)
			{
				return new byte[]
				{
					*bytes,
					bytes[1],
					bytes[2],
					bytes[3]
				};
			}
			return new byte[]
			{
				bytes[3],
				bytes[2],
				bytes[1],
				*bytes
			};
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00007D90 File Offset: 0x00005F90
		private unsafe static byte[] GetULongBytes(byte* bytes)
		{
			if (BitConverter.IsLittleEndian)
			{
				return new byte[]
				{
					*bytes,
					bytes[1],
					bytes[2],
					bytes[3],
					bytes[4],
					bytes[5],
					bytes[6],
					bytes[7]
				};
			}
			return new byte[]
			{
				bytes[7],
				bytes[6],
				bytes[5],
				bytes[4],
				bytes[3],
				bytes[2],
				bytes[1],
				*bytes
			};
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00007E1D File Offset: 0x0000601D
		internal unsafe static byte[] GetBytes(int value)
		{
			return BitConverterLE.GetUIntBytes((byte*)(&value));
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00007E1D File Offset: 0x0000601D
		internal unsafe static byte[] GetBytes(float value)
		{
			return BitConverterLE.GetUIntBytes((byte*)(&value));
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00007E27 File Offset: 0x00006027
		internal unsafe static byte[] GetBytes(double value)
		{
			return BitConverterLE.GetULongBytes((byte*)(&value));
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00007E31 File Offset: 0x00006031
		private unsafe static void UShortFromBytes(byte* dst, byte[] src, int startIndex)
		{
			if (BitConverter.IsLittleEndian)
			{
				*dst = src[startIndex];
				dst[1] = src[startIndex + 1];
				return;
			}
			*dst = src[startIndex + 1];
			dst[1] = src[startIndex];
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00007E58 File Offset: 0x00006058
		private unsafe static void UIntFromBytes(byte* dst, byte[] src, int startIndex)
		{
			if (BitConverter.IsLittleEndian)
			{
				*dst = src[startIndex];
				dst[1] = src[startIndex + 1];
				dst[2] = src[startIndex + 2];
				dst[3] = src[startIndex + 3];
				return;
			}
			*dst = src[startIndex + 3];
			dst[1] = src[startIndex + 2];
			dst[2] = src[startIndex + 1];
			dst[3] = src[startIndex];
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00007EB0 File Offset: 0x000060B0
		private unsafe static void ULongFromBytes(byte* dst, byte[] src, int startIndex)
		{
			if (BitConverter.IsLittleEndian)
			{
				for (int i = 0; i < 8; i++)
				{
					dst[i] = src[startIndex + i];
				}
				return;
			}
			for (int j = 0; j < 8; j++)
			{
				dst[j] = src[startIndex + (7 - j)];
			}
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00007EF4 File Offset: 0x000060F4
		internal unsafe static int ToInt32(byte[] value, int startIndex)
		{
			int num;
			BitConverterLE.UIntFromBytes((byte*)(&num), value, startIndex);
			return num;
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00007F0C File Offset: 0x0000610C
		internal unsafe static ushort ToUInt16(byte[] value, int startIndex)
		{
			ushort num;
			BitConverterLE.UShortFromBytes((byte*)(&num), value, startIndex);
			return num;
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00007F24 File Offset: 0x00006124
		internal unsafe static uint ToUInt32(byte[] value, int startIndex)
		{
			uint num;
			BitConverterLE.UIntFromBytes((byte*)(&num), value, startIndex);
			return num;
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00007F3C File Offset: 0x0000613C
		internal unsafe static float ToSingle(byte[] value, int startIndex)
		{
			float num;
			BitConverterLE.UIntFromBytes((byte*)(&num), value, startIndex);
			return num;
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00007F54 File Offset: 0x00006154
		internal unsafe static double ToDouble(byte[] value, int startIndex)
		{
			double num;
			BitConverterLE.ULongFromBytes((byte*)(&num), value, startIndex);
			return num;
		}
	}
}
