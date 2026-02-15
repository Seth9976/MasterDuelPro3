using System;

namespace Mono.Security
{
	// Token: 0x0200000D RID: 13
	internal sealed class BitConverterLE
	{
		// Token: 0x06000042 RID: 66 RVA: 0x00003518 File Offset: 0x00001718
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

		// Token: 0x06000043 RID: 67 RVA: 0x0000356D File Offset: 0x0000176D
		internal unsafe static byte[] GetBytes(int value)
		{
			return BitConverterLE.GetUIntBytes((byte*)(&value));
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00003577 File Offset: 0x00001777
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

		// Token: 0x06000045 RID: 69 RVA: 0x000035A0 File Offset: 0x000017A0
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

		// Token: 0x06000046 RID: 70 RVA: 0x000035F8 File Offset: 0x000017F8
		internal unsafe static int ToInt32(byte[] value, int startIndex)
		{
			int num;
			BitConverterLE.UIntFromBytes((byte*)(&num), value, startIndex);
			return num;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00003610 File Offset: 0x00001810
		internal unsafe static ushort ToUInt16(byte[] value, int startIndex)
		{
			ushort num;
			BitConverterLE.UShortFromBytes((byte*)(&num), value, startIndex);
			return num;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00003628 File Offset: 0x00001828
		internal unsafe static uint ToUInt32(byte[] value, int startIndex)
		{
			uint num;
			BitConverterLE.UIntFromBytes((byte*)(&num), value, startIndex);
			return num;
		}
	}
}
