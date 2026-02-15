using System;
using System.Text;

namespace System.Xml
{
	// Token: 0x0200011D RID: 285
	internal class SafeAsciiDecoder : Decoder
	{
		// Token: 0x06000ED6 RID: 3798 RVA: 0x0000C1F8 File Offset: 0x0000A3F8
		public override int GetCharCount(byte[] bytes, int index, int count)
		{
			return count;
		}

		// Token: 0x06000ED7 RID: 3799 RVA: 0x0004B4C0 File Offset: 0x000496C0
		public override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex)
		{
			int i = byteIndex;
			int num = charIndex;
			while (i < byteIndex + byteCount)
			{
				chars[num++] = (char)bytes[i++];
			}
			return byteCount;
		}

		// Token: 0x06000ED8 RID: 3800 RVA: 0x0004B4EC File Offset: 0x000496EC
		public override void Convert(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex, int charCount, bool flush, out int bytesUsed, out int charsUsed, out bool completed)
		{
			if (charCount < byteCount)
			{
				byteCount = charCount;
				completed = false;
			}
			else
			{
				completed = true;
			}
			int i = byteIndex;
			int num = charIndex;
			int num2 = byteIndex + byteCount;
			while (i < num2)
			{
				chars[num++] = (char)bytes[i++];
			}
			charsUsed = byteCount;
			bytesUsed = byteCount;
		}
	}
}
