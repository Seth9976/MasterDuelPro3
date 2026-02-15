using System;
using Unity.Collections.LowLevel.Unsafe;

namespace Unity.Collections
{
	// Token: 0x020000E4 RID: 228
	[GenerateTestsForBurstCompatibility]
	public static class UTF8ArrayUnsafeUtility
	{
		// Token: 0x06000A1B RID: 2587 RVA: 0x0001E0E8 File Offset: 0x0001C2E8
		public unsafe static CopyError Copy(byte* dest, out int destLength, int destUTF8MaxLengthInBytes, char* src, int srcLength)
		{
			if (Unicode.Utf16ToUtf8(src, srcLength, dest, out destLength, destUTF8MaxLengthInBytes) == ConversionError.None)
			{
				return CopyError.None;
			}
			return CopyError.Truncation;
		}

		// Token: 0x06000A1C RID: 2588 RVA: 0x0001E0FC File Offset: 0x0001C2FC
		public unsafe static CopyError Copy(byte* dest, out ushort destLength, ushort destUTF8MaxLengthInBytes, char* src, int srcLength)
		{
			int temp;
			bool flag = Unicode.Utf16ToUtf8(src, srcLength, dest, out temp, (int)destUTF8MaxLengthInBytes) != ConversionError.None;
			destLength = (ushort)temp;
			if (!flag)
			{
				return CopyError.None;
			}
			return CopyError.Truncation;
		}

		// Token: 0x06000A1D RID: 2589 RVA: 0x0001E120 File Offset: 0x0001C320
		public unsafe static CopyError Copy(byte* dest, out int destLength, int destUTF8MaxLengthInBytes, byte* src, int srcLength)
		{
			int temp;
			bool flag = Unicode.Utf8ToUtf8(src, srcLength, dest, out temp, destUTF8MaxLengthInBytes) != ConversionError.None;
			destLength = temp;
			if (!flag)
			{
				return CopyError.None;
			}
			return CopyError.Truncation;
		}

		// Token: 0x06000A1E RID: 2590 RVA: 0x0001E144 File Offset: 0x0001C344
		public unsafe static CopyError Copy(byte* dest, out ushort destLength, ushort destUTF8MaxLengthInBytes, byte* src, ushort srcLength)
		{
			int temp;
			bool flag = Unicode.Utf8ToUtf8(src, (int)srcLength, dest, out temp, (int)destUTF8MaxLengthInBytes) != ConversionError.None;
			destLength = (ushort)temp;
			if (!flag)
			{
				return CopyError.None;
			}
			return CopyError.Truncation;
		}

		// Token: 0x06000A1F RID: 2591 RVA: 0x0001E166 File Offset: 0x0001C366
		public unsafe static CopyError Copy(char* dest, out int destLength, int destUCS2MaxLengthInChars, byte* src, int srcLength)
		{
			if (Unicode.Utf8ToUtf16(src, srcLength, dest, out destLength, destUCS2MaxLengthInChars) == ConversionError.None)
			{
				return CopyError.None;
			}
			return CopyError.Truncation;
		}

		// Token: 0x06000A20 RID: 2592 RVA: 0x0001E178 File Offset: 0x0001C378
		public unsafe static CopyError Copy(char* dest, out ushort destLength, ushort destUCS2MaxLengthInChars, byte* src, ushort srcLength)
		{
			int temp;
			bool flag = Unicode.Utf8ToUtf16(src, (int)srcLength, dest, out temp, (int)destUCS2MaxLengthInChars) != ConversionError.None;
			destLength = (ushort)temp;
			if (!flag)
			{
				return CopyError.None;
			}
			return CopyError.Truncation;
		}

		// Token: 0x06000A21 RID: 2593 RVA: 0x0001E19A File Offset: 0x0001C39A
		public unsafe static FormatError AppendUTF8Bytes(byte* dest, ref int destLength, int destCapacity, byte* src, int srcLength)
		{
			if (destLength + srcLength > destCapacity)
			{
				return FormatError.Overflow;
			}
			UnsafeUtility.MemCpy((void*)(dest + destLength), (void*)src, (long)srcLength);
			destLength += srcLength;
			return FormatError.None;
		}

		// Token: 0x06000A22 RID: 2594 RVA: 0x0001E1BC File Offset: 0x0001C3BC
		public unsafe static CopyError Append(byte* dest, ref ushort destLength, ushort destUTF8MaxLengthInBytes, byte* src, ushort srcLength)
		{
			int temp;
			bool flag = Unicode.Utf8ToUtf8(src, (int)srcLength, dest + destLength, out temp, (int)(destUTF8MaxLengthInBytes - destLength)) != ConversionError.None;
			destLength += (ushort)temp;
			if (!flag)
			{
				return CopyError.None;
			}
			return CopyError.Truncation;
		}

		// Token: 0x06000A23 RID: 2595 RVA: 0x0001E1E8 File Offset: 0x0001C3E8
		public unsafe static CopyError Append(byte* dest, ref ushort destLength, ushort destUTF8MaxLengthInBytes, char* src, int srcLength)
		{
			int temp;
			bool flag = Unicode.Utf16ToUtf8(src, srcLength, dest + destLength, out temp, (int)(destUTF8MaxLengthInBytes - destLength)) != ConversionError.None;
			destLength += (ushort)temp;
			if (!flag)
			{
				return CopyError.None;
			}
			return CopyError.Truncation;
		}

		// Token: 0x06000A24 RID: 2596 RVA: 0x0001E214 File Offset: 0x0001C414
		public unsafe static CopyError Append(char* dest, ref ushort destLength, ushort destUCS2MaxLengthInChars, byte* src, ushort srcLength)
		{
			int temp;
			bool flag = Unicode.Utf8ToUtf16(src, (int)srcLength, dest + destLength, out temp, (int)(destUCS2MaxLengthInChars - destLength)) != ConversionError.None;
			destLength += (ushort)temp;
			if (!flag)
			{
				return CopyError.None;
			}
			return CopyError.Truncation;
		}

		// Token: 0x06000A25 RID: 2597 RVA: 0x0001E244 File Offset: 0x0001C444
		public unsafe static int StrCmp(byte* utf8BufferA, int utf8LengthInBytesA, byte* utf8BufferB, int utf8LengthInBytesB)
		{
			int byteIndexA = 0;
			int byteIndexB = 0;
			UTF8ArrayUnsafeUtility.Comparison comparison;
			do
			{
				Unicode.Rune utf8RuneA;
				ConversionError utf8ErrorA = Unicode.Utf8ToUcs(out utf8RuneA, utf8BufferA, ref byteIndexA, utf8LengthInBytesA);
				Unicode.Rune utf8RuneB;
				ConversionError utf8ErrorB = Unicode.Utf8ToUcs(out utf8RuneB, utf8BufferB, ref byteIndexB, utf8LengthInBytesB);
				comparison = new UTF8ArrayUnsafeUtility.Comparison(utf8RuneA, utf8ErrorA, utf8RuneB, utf8ErrorB);
			}
			while (!comparison.terminates);
			return comparison.result;
		}

		// Token: 0x06000A26 RID: 2598 RVA: 0x0001E28C File Offset: 0x0001C48C
		internal unsafe static int StrCmp(byte* utf8BufferA, int utf8LengthInBytesA, Unicode.Rune* runeBufferB, int lengthInRunesB)
		{
			int charIndexA = 0;
			int charIndexB = 0;
			UTF8ArrayUnsafeUtility.Comparison comparison;
			do
			{
				Unicode.Rune utf16RuneA;
				ConversionError utf16ErrorA = Unicode.Utf8ToUcs(out utf16RuneA, utf8BufferA, ref charIndexA, utf8LengthInBytesA);
				Unicode.Rune runeB;
				ConversionError errorB = Unicode.UcsToUcs(out runeB, runeBufferB, ref charIndexB, lengthInRunesB);
				comparison = new UTF8ArrayUnsafeUtility.Comparison(utf16RuneA, utf16ErrorA, runeB, errorB);
			}
			while (!comparison.terminates);
			return comparison.result;
		}

		// Token: 0x06000A27 RID: 2599 RVA: 0x0001E2D4 File Offset: 0x0001C4D4
		public unsafe static int StrCmp(char* utf16BufferA, int utf16LengthInCharsA, char* utf16BufferB, int utf16LengthInCharsB)
		{
			int charIndexA = 0;
			int charIndexB = 0;
			UTF8ArrayUnsafeUtility.Comparison comparison;
			do
			{
				Unicode.Rune utf16RuneA;
				ConversionError utf16ErrorA = Unicode.Utf16ToUcs(out utf16RuneA, utf16BufferA, ref charIndexA, utf16LengthInCharsA);
				Unicode.Rune utf16RuneB;
				ConversionError utf16ErrorB = Unicode.Utf16ToUcs(out utf16RuneB, utf16BufferB, ref charIndexB, utf16LengthInCharsB);
				comparison = new UTF8ArrayUnsafeUtility.Comparison(utf16RuneA, utf16ErrorA, utf16RuneB, utf16ErrorB);
			}
			while (!comparison.terminates);
			return comparison.result;
		}

		// Token: 0x06000A28 RID: 2600 RVA: 0x0001E31B File Offset: 0x0001C51B
		public unsafe static bool EqualsUTF8Bytes(byte* aBytes, int aLength, byte* bBytes, int bLength)
		{
			return aLength == bLength && UTF8ArrayUnsafeUtility.StrCmp(aBytes, aLength, bBytes, bLength) == 0;
		}

		// Token: 0x06000A29 RID: 2601 RVA: 0x0001E330 File Offset: 0x0001C530
		public unsafe static int StrCmp(byte* utf8Buffer, int utf8LengthInBytes, char* utf16Buffer, int utf16LengthInChars)
		{
			int byteIndex = 0;
			int charIndex = 0;
			UTF8ArrayUnsafeUtility.Comparison comparison;
			do
			{
				Unicode.Rune utf8Rune;
				ConversionError utf8Error = Unicode.Utf8ToUcs(out utf8Rune, utf8Buffer, ref byteIndex, utf8LengthInBytes);
				Unicode.Rune utf16Rune;
				ConversionError utf16Error = Unicode.Utf16ToUcs(out utf16Rune, utf16Buffer, ref charIndex, utf16LengthInChars);
				comparison = new UTF8ArrayUnsafeUtility.Comparison(utf8Rune, utf8Error, utf16Rune, utf16Error);
			}
			while (!comparison.terminates);
			return comparison.result;
		}

		// Token: 0x06000A2A RID: 2602 RVA: 0x0001E377 File Offset: 0x0001C577
		public unsafe static int StrCmp(char* utf16Buffer, int utf16LengthInChars, byte* utf8Buffer, int utf8LengthInBytes)
		{
			return -UTF8ArrayUnsafeUtility.StrCmp(utf8Buffer, utf8LengthInBytes, utf16Buffer, utf16LengthInChars);
		}

		// Token: 0x020000E5 RID: 229
		internal struct Comparison
		{
			// Token: 0x06000A2B RID: 2603 RVA: 0x0001E384 File Offset: 0x0001C584
			public Comparison(Unicode.Rune runeA, ConversionError errorA, Unicode.Rune runeB, ConversionError errorB)
			{
				if (errorA != ConversionError.None)
				{
					runeA.value = 0;
				}
				if (errorB != ConversionError.None)
				{
					runeB.value = 0;
				}
				if (runeA.value != runeB.value)
				{
					this.result = runeA.value - runeB.value;
					this.terminates = true;
					return;
				}
				this.result = 0;
				this.terminates = runeA.value == 0 && runeB.value == 0;
			}

			// Token: 0x04000444 RID: 1092
			public bool terminates;

			// Token: 0x04000445 RID: 1093
			public int result;
		}
	}
}
