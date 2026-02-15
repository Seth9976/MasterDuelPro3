using System;
using System.Text;

namespace System.Xml
{
	// Token: 0x0200011C RID: 284
	internal class UTF16Decoder : Decoder
	{
		// Token: 0x06000ED0 RID: 3792 RVA: 0x0004B210 File Offset: 0x00049410
		public UTF16Decoder(bool bigEndian)
		{
			this.lastByte = -1;
			this.bigEndian = bigEndian;
		}

		// Token: 0x06000ED1 RID: 3793 RVA: 0x0004B226 File Offset: 0x00049426
		public override int GetCharCount(byte[] bytes, int index, int count)
		{
			return this.GetCharCount(bytes, index, count, false);
		}

		// Token: 0x06000ED2 RID: 3794 RVA: 0x0004B234 File Offset: 0x00049434
		public override int GetCharCount(byte[] bytes, int index, int count, bool flush)
		{
			int num = count + ((this.lastByte >= 0) ? 1 : 0);
			if (flush && num % 2 != 0)
			{
				throw new ArgumentException(Res.GetString("Invalid byte was found at index {0}.", new object[] { -1 }), null);
			}
			return num / 2;
		}

		// Token: 0x06000ED3 RID: 3795 RVA: 0x0004B280 File Offset: 0x00049480
		public override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex)
		{
			int charCount = this.GetCharCount(bytes, byteIndex, byteCount);
			if (this.lastByte >= 0)
			{
				if (byteCount == 0)
				{
					return charCount;
				}
				int num = (int)bytes[byteIndex++];
				byteCount--;
				chars[charIndex++] = (this.bigEndian ? ((char)((this.lastByte << 8) | num)) : ((char)((num << 8) | this.lastByte)));
				this.lastByte = -1;
			}
			if ((byteCount & 1) != 0)
			{
				this.lastByte = (int)bytes[byteIndex + --byteCount];
			}
			if (this.bigEndian == BitConverter.IsLittleEndian)
			{
				int num2 = byteIndex + byteCount;
				if (this.bigEndian)
				{
					while (byteIndex < num2)
					{
						int num3 = (int)bytes[byteIndex++];
						int num4 = (int)bytes[byteIndex++];
						chars[charIndex++] = (char)((num3 << 8) | num4);
					}
				}
				else
				{
					while (byteIndex < num2)
					{
						int num5 = (int)bytes[byteIndex++];
						int num6 = (int)bytes[byteIndex++];
						chars[charIndex++] = (char)((num6 << 8) | num5);
					}
				}
			}
			else
			{
				Buffer.BlockCopy(bytes, byteIndex, chars, charIndex * 2, byteCount);
			}
			return charCount;
		}

		// Token: 0x06000ED4 RID: 3796 RVA: 0x0004B37C File Offset: 0x0004957C
		public override void Convert(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex, int charCount, bool flush, out int bytesUsed, out int charsUsed, out bool completed)
		{
			charsUsed = 0;
			bytesUsed = 0;
			if (this.lastByte >= 0)
			{
				if (byteCount == 0)
				{
					completed = true;
					return;
				}
				int num = (int)bytes[byteIndex++];
				byteCount--;
				bytesUsed++;
				chars[charIndex++] = (this.bigEndian ? ((char)((this.lastByte << 8) | num)) : ((char)((num << 8) | this.lastByte)));
				charCount--;
				charsUsed++;
				this.lastByte = -1;
			}
			if (charCount * 2 < byteCount)
			{
				byteCount = charCount * 2;
				completed = false;
			}
			else
			{
				completed = true;
			}
			if (this.bigEndian == BitConverter.IsLittleEndian)
			{
				int i = byteIndex;
				int num2 = i + (byteCount & -2);
				if (this.bigEndian)
				{
					while (i < num2)
					{
						int num3 = (int)bytes[i++];
						int num4 = (int)bytes[i++];
						chars[charIndex++] = (char)((num3 << 8) | num4);
					}
				}
				else
				{
					while (i < num2)
					{
						int num5 = (int)bytes[i++];
						int num6 = (int)bytes[i++];
						chars[charIndex++] = (char)((num6 << 8) | num5);
					}
				}
			}
			else
			{
				Buffer.BlockCopy(bytes, byteIndex, chars, charIndex * 2, byteCount & -2);
			}
			charsUsed += byteCount / 2;
			bytesUsed += byteCount;
			if ((byteCount & 1) != 0)
			{
				this.lastByte = (int)bytes[byteIndex + byteCount - 1];
			}
		}

		// Token: 0x04000744 RID: 1860
		private bool bigEndian;

		// Token: 0x04000745 RID: 1861
		private int lastByte;
	}
}
