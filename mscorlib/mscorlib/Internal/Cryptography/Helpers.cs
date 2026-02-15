using System;

namespace Internal.Cryptography
{
	// Token: 0x02000097 RID: 151
	internal static class Helpers
	{
		// Token: 0x060002CE RID: 718 RVA: 0x00010984 File Offset: 0x0000EB84
		public static byte[] CloneByteArray(this byte[] src)
		{
			if (src == null)
			{
				return null;
			}
			return (byte[])src.Clone();
		}

		// Token: 0x060002CF RID: 719 RVA: 0x00010996 File Offset: 0x0000EB96
		public static void WriteInt(uint i, byte[] arr, int offset)
		{
			arr[offset] = (byte)(i >> 24);
			arr[offset + 1] = (byte)(i >> 16);
			arr[offset + 2] = (byte)(i >> 8);
			arr[offset + 3] = (byte)i;
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x000109BC File Offset: 0x0000EBBC
		public static char[] ToHexArrayUpper(this byte[] bytes)
		{
			char[] array = new char[bytes.Length * 2];
			int num = 0;
			foreach (byte b in bytes)
			{
				array[num++] = Helpers.NibbleToHex((byte)(b >> 4));
				array[num++] = Helpers.NibbleToHex(b & 15);
			}
			return array;
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x00010A0F File Offset: 0x0000EC0F
		public static string ToHexStringUpper(this byte[] bytes)
		{
			return new string(bytes.ToHexArrayUpper());
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x00010A1C File Offset: 0x0000EC1C
		private static char NibbleToHex(byte b)
		{
			return (char)((b >= 0 && b <= 9) ? (48 + b) : (65 + (b - 10)));
		}
	}
}
