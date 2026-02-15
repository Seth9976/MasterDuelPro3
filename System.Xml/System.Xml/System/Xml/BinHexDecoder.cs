using System;
using System.Runtime.CompilerServices;

namespace System.Xml
{
	// Token: 0x0200000F RID: 15
	internal class BinHexDecoder : IncrementalReadDecoder
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600002F RID: 47 RVA: 0x00002CE7 File Offset: 0x00000EE7
		internal override int DecodedCount
		{
			get
			{
				return this.curIndex - this.startIndex;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000030 RID: 48 RVA: 0x00002CF6 File Offset: 0x00000EF6
		internal override bool IsFull
		{
			get
			{
				return this.curIndex == this.endIndex;
			}
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002D08 File Offset: 0x00000F08
		internal unsafe override int Decode(char[] chars, int startPos, int len)
		{
			if (chars == null)
			{
				throw new ArgumentNullException("chars");
			}
			if (len < 0)
			{
				throw new ArgumentOutOfRangeException("len");
			}
			if (startPos < 0)
			{
				throw new ArgumentOutOfRangeException("startPos");
			}
			if (chars.Length - startPos < len)
			{
				throw new ArgumentOutOfRangeException("len");
			}
			if (len == 0)
			{
				return 0;
			}
			int num;
			int num2;
			fixed (char* ptr = &chars[startPos])
			{
				char* ptr2 = ptr;
				fixed (byte* ptr3 = &this.buffer[this.curIndex])
				{
					byte* ptr4 = ptr3;
					BinHexDecoder.Decode(ptr2, ptr2 + len, ptr4, ptr4 + (this.endIndex - this.curIndex), ref this.hasHalfByteCached, ref this.cachedHalfByte, out num, out num2);
				}
			}
			this.curIndex += num2;
			return num;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002DBC File Offset: 0x00000FBC
		internal unsafe override int Decode(string str, int startPos, int len)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			if (len < 0)
			{
				throw new ArgumentOutOfRangeException("len");
			}
			if (startPos < 0)
			{
				throw new ArgumentOutOfRangeException("startPos");
			}
			if (str.Length - startPos < len)
			{
				throw new ArgumentOutOfRangeException("len");
			}
			if (len == 0)
			{
				return 0;
			}
			int num;
			int num2;
			fixed (string text = str)
			{
				char* ptr = text;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				fixed (byte* ptr2 = &this.buffer[this.curIndex])
				{
					byte* ptr3 = ptr2;
					BinHexDecoder.Decode(ptr + startPos, ptr + startPos + len, ptr3, ptr3 + (this.endIndex - this.curIndex), ref this.hasHalfByteCached, ref this.cachedHalfByte, out num, out num2);
				}
			}
			this.curIndex += num2;
			return num;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002E85 File Offset: 0x00001085
		internal override void Reset()
		{
			this.hasHalfByteCached = false;
			this.cachedHalfByte = 0;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002E95 File Offset: 0x00001095
		internal override void SetNextOutputBuffer(Array buffer, int index, int count)
		{
			this.buffer = (byte[])buffer;
			this.startIndex = index;
			this.curIndex = index;
			this.endIndex = index + count;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002EBC File Offset: 0x000010BC
		public unsafe static byte[] Decode(char[] chars, bool allowOddChars)
		{
			if (chars == null)
			{
				throw new ArgumentNullException("chars");
			}
			int num = chars.Length;
			if (num == 0)
			{
				return new byte[0];
			}
			byte[] array = new byte[(num + 1) / 2];
			bool flag = false;
			byte b = 0;
			int num3;
			fixed (char* ptr = &chars[0])
			{
				char* ptr2 = ptr;
				fixed (byte* ptr3 = &array[0])
				{
					byte* ptr4 = ptr3;
					int num2;
					BinHexDecoder.Decode(ptr2, ptr2 + num, ptr4, ptr4 + array.Length, ref flag, ref b, out num2, out num3);
				}
			}
			if (flag && !allowOddChars)
			{
				throw new XmlException("'{0}' is not a valid BinHex text sequence. The sequence must contain an even number of characters.", new string(chars));
			}
			if (num3 < array.Length)
			{
				byte[] array2 = new byte[num3];
				Array.Copy(array, 0, array2, 0, num3);
				array = array2;
			}
			return array;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002F68 File Offset: 0x00001168
		private unsafe static void Decode(char* pChars, char* pCharsEndPos, byte* pBytes, byte* pBytesEndPos, ref bool hasHalfByteCached, ref byte cachedHalfByte, out int charsDecoded, out int bytesDecoded)
		{
			char* ptr = pChars;
			byte* ptr2 = pBytes;
			XmlCharType instance = XmlCharType.Instance;
			while (ptr < pCharsEndPos && ptr2 < pBytesEndPos)
			{
				char c = *(ptr++);
				byte b;
				if (c >= 'a' && c <= 'f')
				{
					b = (byte)(c - 'a' + '\n');
				}
				else if (c >= 'A' && c <= 'F')
				{
					b = (byte)(c - 'A' + '\n');
				}
				else if (c >= '0' && c <= '9')
				{
					b = (byte)(c - '0');
				}
				else
				{
					if ((instance.charProperties[(int)c] & 1) == 0)
					{
						throw new XmlException("'{0}' is not a valid BinHex text sequence.", new string(pChars, 0, (int)((long)(pCharsEndPos - pChars))));
					}
					continue;
				}
				if (hasHalfByteCached)
				{
					*(ptr2++) = (byte)(((int)cachedHalfByte << 4) + (int)b);
					hasHalfByteCached = false;
				}
				else
				{
					cachedHalfByte = b;
					hasHalfByteCached = true;
				}
			}
			bytesDecoded = (int)((long)(ptr2 - pBytes));
			charsDecoded = (int)((long)(ptr - pChars));
		}

		// Token: 0x0400002A RID: 42
		private byte[] buffer;

		// Token: 0x0400002B RID: 43
		private int startIndex;

		// Token: 0x0400002C RID: 44
		private int curIndex;

		// Token: 0x0400002D RID: 45
		private int endIndex;

		// Token: 0x0400002E RID: 46
		private bool hasHalfByteCached;

		// Token: 0x0400002F RID: 47
		private byte cachedHalfByte;
	}
}
