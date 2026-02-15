using System;
using System.Buffers.Binary;
using System.Runtime.CompilerServices;

namespace System.Globalization
{
	/// <summary>Retrieves information about a Unicode character. This class cannot be inherited.</summary>
	// Token: 0x0200068C RID: 1676
	public static class CharUnicodeInfo
	{
		// Token: 0x06003463 RID: 13411 RVA: 0x000C8224 File Offset: 0x000C6424
		internal static int InternalConvertToUtf32(string s, int index)
		{
			if (index < s.Length - 1)
			{
				int num = (int)(s[index] - '\ud800');
				if (num <= 1023)
				{
					int num2 = (int)(s[index + 1] - '\udc00');
					if (num2 <= 1023)
					{
						return num * 1024 + num2 + 65536;
					}
				}
			}
			return (int)s[index];
		}

		// Token: 0x06003464 RID: 13412 RVA: 0x000C8284 File Offset: 0x000C6484
		internal static int InternalConvertToUtf32(string s, int index, out int charLength)
		{
			charLength = 1;
			if (index < s.Length - 1)
			{
				int num = (int)(s[index] - '\ud800');
				if (num <= 1023)
				{
					int num2 = (int)(s[index + 1] - '\udc00');
					if (num2 <= 1023)
					{
						charLength++;
						return num * 1024 + num2 + 65536;
					}
				}
			}
			return (int)s[index];
		}

		/// <summary>Gets the Unicode category of the specified character.</summary>
		/// <returns>A <see cref="T:System.Globalization.UnicodeCategory" /> value indicating the category of the specified character.</returns>
		/// <param name="ch">The Unicode character for which to get the Unicode category. </param>
		// Token: 0x06003465 RID: 13413 RVA: 0x000C82EA File Offset: 0x000C64EA
		public static UnicodeCategory GetUnicodeCategory(char ch)
		{
			return CharUnicodeInfo.GetUnicodeCategory((int)ch);
		}

		/// <summary>Gets the Unicode category of the character at the specified index of the specified string.</summary>
		/// <returns>A <see cref="T:System.Globalization.UnicodeCategory" /> value indicating the category of the character at the specified index of the specified string.</returns>
		/// <param name="s">The <see cref="T:System.String" /> containing the Unicode character for which to get the Unicode category. </param>
		/// <param name="index">The index of the Unicode character for which to get the Unicode category. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is outside the range of valid indexes in <paramref name="s" />. </exception>
		// Token: 0x06003466 RID: 13414 RVA: 0x000C82F2 File Offset: 0x000C64F2
		public static UnicodeCategory GetUnicodeCategory(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			return CharUnicodeInfo.InternalGetUnicodeCategory(s, index);
		}

		// Token: 0x06003467 RID: 13415 RVA: 0x000C831D File Offset: 0x000C651D
		public static UnicodeCategory GetUnicodeCategory(int codePoint)
		{
			return (UnicodeCategory)CharUnicodeInfo.InternalGetCategoryValue(codePoint, 0);
		}

		// Token: 0x06003468 RID: 13416 RVA: 0x000C8328 File Offset: 0x000C6528
		internal unsafe static byte InternalGetCategoryValue(int ch, int offset)
		{
			int num = (int)(*CharUnicodeInfo.CategoryLevel1Index[ch >> 9]);
			num = (int)Unsafe.ReadUnaligned<ushort>(Unsafe.AsRef<byte>(CharUnicodeInfo.CategoryLevel2Index[(num << 6) + ((ch >> 3) & 62)]));
			if (!BitConverter.IsLittleEndian)
			{
				num = (int)BinaryPrimitives.ReverseEndianness((ushort)num);
			}
			num = (int)(*CharUnicodeInfo.CategoryLevel3Index[(num << 4) + (ch & 15)]);
			return *CharUnicodeInfo.CategoriesValue[num * 2 + offset];
		}

		// Token: 0x06003469 RID: 13417 RVA: 0x000C83A3 File Offset: 0x000C65A3
		internal static UnicodeCategory InternalGetUnicodeCategory(string value, int index)
		{
			return CharUnicodeInfo.GetUnicodeCategory(CharUnicodeInfo.InternalConvertToUtf32(value, index));
		}

		// Token: 0x0600346A RID: 13418 RVA: 0x000C83B1 File Offset: 0x000C65B1
		internal static UnicodeCategory InternalGetUnicodeCategory(string str, int index, out int charLength)
		{
			return CharUnicodeInfo.GetUnicodeCategory(CharUnicodeInfo.InternalConvertToUtf32(str, index, out charLength));
		}

		// Token: 0x0600346B RID: 13419 RVA: 0x000C83C0 File Offset: 0x000C65C0
		internal static bool IsCombiningCategory(UnicodeCategory uc)
		{
			return uc == UnicodeCategory.NonSpacingMark || uc == UnicodeCategory.SpacingCombiningMark || uc == UnicodeCategory.EnclosingMark;
		}

		// Token: 0x0600346C RID: 13420 RVA: 0x000C83D0 File Offset: 0x000C65D0
		internal static bool IsWhiteSpace(string s, int index)
		{
			UnicodeCategory unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(s, index);
			return unicodeCategory - UnicodeCategory.SpaceSeparator <= 2;
		}

		// Token: 0x0600346D RID: 13421 RVA: 0x000C83F0 File Offset: 0x000C65F0
		internal static bool IsWhiteSpace(char c)
		{
			UnicodeCategory unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
			return unicodeCategory - UnicodeCategory.SpaceSeparator <= 2;
		}

		// Token: 0x170007A8 RID: 1960
		// (get) Token: 0x0600346E RID: 13422 RVA: 0x000C840E File Offset: 0x000C660E
		private unsafe static ReadOnlySpan<byte> CategoryLevel1Index
		{
			get
			{
				return new ReadOnlySpan<byte>((void*)(&<PrivateImplementationDetails>.B55F94CD2F415D0279D7A1AF2265C4D9A90CE47F8C900D5D09AD088796210838), 2176);
			}
		}

		// Token: 0x170007A9 RID: 1961
		// (get) Token: 0x0600346F RID: 13423 RVA: 0x000C841F File Offset: 0x000C661F
		private unsafe static ReadOnlySpan<byte> CategoryLevel2Index
		{
			get
			{
				return new ReadOnlySpan<byte>((void*)(&<PrivateImplementationDetails>.9086502742CE7F0595B57A4E5B32901FF4CF97959B92F7E91A435E4765AC1115), 5952);
			}
		}

		// Token: 0x170007AA RID: 1962
		// (get) Token: 0x06003470 RID: 13424 RVA: 0x000C8430 File Offset: 0x000C6630
		private unsafe static ReadOnlySpan<byte> CategoryLevel3Index
		{
			get
			{
				return new ReadOnlySpan<byte>((void*)(&<PrivateImplementationDetails>.56073E3CC3FC817690CC306D0DB7EA63EBCB0801359567CA44CA3D3B9BF63854), 10800);
			}
		}

		// Token: 0x170007AB RID: 1963
		// (get) Token: 0x06003471 RID: 13425 RVA: 0x000C8441 File Offset: 0x000C6641
		private unsafe static ReadOnlySpan<byte> CategoriesValue
		{
			get
			{
				return new ReadOnlySpan<byte>((void*)(&<PrivateImplementationDetails>.D6691EE5A533DE7E0859066942261B24D0C836D7EE016D2251377BFEE40FEA15), 172);
			}
		}
	}
}
