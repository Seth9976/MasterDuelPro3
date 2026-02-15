using System;
using System.IO;
using System.Text;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000004 RID: 4
	[VisibleToOtherModules(new string[] { "UnityEngine.UnityWebRequestWWWModule" })]
	internal class WWWTranscoder
	{
		// Token: 0x06000008 RID: 8 RVA: 0x000024E4 File Offset: 0x000006E4
		private static byte Hex2Byte(byte[] b, int offset)
		{
			byte result = 0;
			for (int i = offset; i < offset + 2; i++)
			{
				result *= 16;
				int d = (int)b[i];
				bool flag = d >= 48 && d <= 57;
				if (flag)
				{
					d -= 48;
				}
				else
				{
					bool flag2 = d >= 65 && d <= 75;
					if (flag2)
					{
						d -= 55;
					}
					else
					{
						bool flag3 = d >= 97 && d <= 102;
						if (flag3)
						{
							d -= 87;
						}
					}
				}
				bool flag4 = d > 15;
				if (flag4)
				{
					return 63;
				}
				result += (byte)d;
			}
			return result;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002589 File Offset: 0x00000789
		private static void Byte2Hex(byte b, byte[] hexChars, out byte byte0, out byte byte1)
		{
			byte0 = hexChars[b >> 4];
			byte1 = hexChars[(int)(b & 15)];
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000259C File Offset: 0x0000079C
		public static string DataEncode(string toEncode, Encoding e)
		{
			byte[] data = WWWTranscoder.Encode(e.GetBytes(toEncode), WWWTranscoder.urlEscapeChar, WWWTranscoder.dataSpace, WWWTranscoder.urlForbidden, false);
			return WWWForm.DefaultEncoding.GetString(data, 0, data.Length);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000025DC File Offset: 0x000007DC
		public static byte[] Encode(byte[] input, byte escapeChar, byte[] space, byte[] forbidden, bool uppercase)
		{
			byte[] array;
			using (MemoryStream memStream = new MemoryStream(input.Length * 2))
			{
				for (int i = 0; i < input.Length; i++)
				{
					bool flag = input[i] == 32;
					if (flag)
					{
						memStream.Write(space, 0, space.Length);
					}
					else
					{
						bool flag2 = input[i] < 32 || input[i] > 126 || WWWTranscoder.ByteArrayContains(forbidden, input[i]);
						if (flag2)
						{
							memStream.WriteByte(escapeChar);
							byte byte0;
							byte @byte;
							WWWTranscoder.Byte2Hex(input[i], uppercase ? WWWTranscoder.ucHexChars : WWWTranscoder.lcHexChars, out byte0, out @byte);
							memStream.WriteByte(byte0);
							memStream.WriteByte(@byte);
						}
						else
						{
							memStream.WriteByte(input[i]);
						}
					}
				}
				array = memStream.ToArray();
			}
			return array;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000026BC File Offset: 0x000008BC
		private static bool ByteArrayContains(byte[] array, byte b)
		{
			int arrayLength = array.Length;
			for (int i = 0; i < arrayLength; i++)
			{
				bool flag = array[i] == b;
				if (flag)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000026F8 File Offset: 0x000008F8
		public static byte[] URLDecode(byte[] toEncode)
		{
			return WWWTranscoder.Decode(toEncode, WWWTranscoder.urlEscapeChar, WWWTranscoder.urlSpace);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x0000271C File Offset: 0x0000091C
		private static bool ByteSubArrayEquals(byte[] array, int index, byte[] comperand)
		{
			bool flag = array.Length - index < comperand.Length;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				for (int i = 0; i < comperand.Length; i++)
				{
					bool flag3 = array[index + i] != comperand[i];
					if (flag3)
					{
						return false;
					}
				}
				flag2 = true;
			}
			return flag2;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x0000276C File Offset: 0x0000096C
		public static byte[] Decode(byte[] input, byte escapeChar, byte[] space)
		{
			byte[] array;
			using (MemoryStream memStream = new MemoryStream(input.Length))
			{
				for (int i = 0; i < input.Length; i++)
				{
					bool flag = WWWTranscoder.ByteSubArrayEquals(input, i, space);
					if (flag)
					{
						i += space.Length - 1;
						memStream.WriteByte(32);
					}
					else
					{
						bool flag2 = input[i] == escapeChar && i + 2 < input.Length;
						if (flag2)
						{
							i++;
							memStream.WriteByte(WWWTranscoder.Hex2Byte(input, i++));
						}
						else
						{
							memStream.WriteByte(input[i]);
						}
					}
				}
				array = memStream.ToArray();
			}
			return array;
		}

		// Token: 0x0400000A RID: 10
		private static byte[] ucHexChars = WWWForm.DefaultEncoding.GetBytes("0123456789ABCDEF");

		// Token: 0x0400000B RID: 11
		private static byte[] lcHexChars = WWWForm.DefaultEncoding.GetBytes("0123456789abcdef");

		// Token: 0x0400000C RID: 12
		private static byte urlEscapeChar = 37;

		// Token: 0x0400000D RID: 13
		private static byte[] urlSpace = new byte[] { 43 };

		// Token: 0x0400000E RID: 14
		private static byte[] dataSpace = WWWForm.DefaultEncoding.GetBytes("%20");

		// Token: 0x0400000F RID: 15
		private static byte[] urlForbidden = WWWForm.DefaultEncoding.GetBytes("@&;:<>=?\"'/\\!#%+$,{}|^[]`");

		// Token: 0x04000010 RID: 16
		private static byte qpEscapeChar = 61;

		// Token: 0x04000011 RID: 17
		private static byte[] qpSpace = new byte[] { 95 };

		// Token: 0x04000012 RID: 18
		private static byte[] qpForbidden = WWWForm.DefaultEncoding.GetBytes("&;=?\"'%+_");
	}
}
