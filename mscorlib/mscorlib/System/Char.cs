using System;
using System.Globalization;
using System.Runtime.Versioning;

namespace System
{
	/// <summary>Represents a character as a UTF-16 code unit.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020000CE RID: 206
	[Serializable]
	public readonly struct Char : IComparable, IComparable<char>, IEquatable<char>, IConvertible
	{
		// Token: 0x06000545 RID: 1349 RVA: 0x000197CC File Offset: 0x000179CC
		private static bool IsLatin1(char ch)
		{
			return ch <= 'ÿ';
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x000197D9 File Offset: 0x000179D9
		private static bool IsAscii(char ch)
		{
			return ch <= '\u007f';
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x000197E3 File Offset: 0x000179E3
		private static UnicodeCategory GetLatin1UnicodeCategory(char ch)
		{
			return (UnicodeCategory)char.s_categoryForLatin1[(int)ch];
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000548 RID: 1352 RVA: 0x000197EC File Offset: 0x000179EC
		public override int GetHashCode()
		{
			return (int)(this | ((int)this << 16));
		}

		/// <summary>Returns a value that indicates whether this instance is equal to a specified object.</summary>
		/// <returns>true if <paramref name="obj" /> is an instance of <see cref="T:System.Char" /> and equals the value of this instance; otherwise, false.</returns>
		/// <param name="obj">An object to compare with this instance or null. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000549 RID: 1353 RVA: 0x000197F6 File Offset: 0x000179F6
		public override bool Equals(object obj)
		{
			return obj is char && this == (char)obj;
		}

		/// <summary>Returns a value that indicates whether this instance is equal to the specified <see cref="T:System.Char" /> object.</summary>
		/// <returns>true if the <paramref name="obj" /> parameter equals the value of this instance; otherwise, false.</returns>
		/// <param name="obj">An object to compare to this instance. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600054A RID: 1354 RVA: 0x0001980C File Offset: 0x00017A0C
		[NonVersionable]
		public bool Equals(char obj)
		{
			return this == obj;
		}

		/// <summary>Compares this instance to a specified object and indicates whether this instance precedes, follows, or appears in the same position in the sort order as the specified <see cref="T:System.Object" />.</summary>
		/// <returns>A signed number indicating the position of this instance in the sort order in relation to the <paramref name="value" /> parameter.Return Value Description Less than zero This instance precedes <paramref name="value" />. Zero This instance has the same position in the sort order as <paramref name="value" />. Greater than zero This instance follows <paramref name="value" />.-or- <paramref name="value" /> is null. </returns>
		/// <param name="value">An object to compare this instance to, or null. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> is not a <see cref="T:System.Char" /> object. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600054B RID: 1355 RVA: 0x00019813 File Offset: 0x00017A13
		public int CompareTo(object value)
		{
			if (value == null)
			{
				return 1;
			}
			if (!(value is char))
			{
				throw new ArgumentException("Object must be of type Char.");
			}
			return (int)(this - (char)value);
		}

		/// <summary>Compares this instance to a specified <see cref="T:System.Char" /> object and indicates whether this instance precedes, follows, or appears in the same position in the sort order as the specified <see cref="T:System.Char" /> object.</summary>
		/// <returns>A signed number indicating the position of this instance in the sort order in relation to the <paramref name="value" /> parameter.Return Value Description Less than zero This instance precedes <paramref name="value" />. Zero This instance has the same position in the sort order as <paramref name="value" />. Greater than zero This instance follows <paramref name="value" />. </returns>
		/// <param name="value">A <see cref="T:System.Char" /> object to compare. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600054C RID: 1356 RVA: 0x00019836 File Offset: 0x00017A36
		public int CompareTo(char value)
		{
			return (int)(this - value);
		}

		/// <summary>Converts the value of this instance to its equivalent string representation.</summary>
		/// <returns>The string representation of the value of this instance.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600054D RID: 1357 RVA: 0x0001983C File Offset: 0x00017A3C
		public override string ToString()
		{
			return char.ToString(this);
		}

		/// <summary>Converts the value of this instance to its equivalent string representation using the specified culture-specific format information.</summary>
		/// <returns>The string representation of the value of this instance as specified by <paramref name="provider" />.</returns>
		/// <param name="provider">(Reserved) An object that supplies culture-specific formatting information. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600054E RID: 1358 RVA: 0x0001983C File Offset: 0x00017A3C
		public string ToString(IFormatProvider provider)
		{
			return char.ToString(this);
		}

		/// <summary>Converts the specified Unicode character to its equivalent string representation.</summary>
		/// <returns>The string representation of the value of <paramref name="c" />.</returns>
		/// <param name="c">The Unicode character to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600054F RID: 1359 RVA: 0x00019845 File Offset: 0x00017A45
		public static string ToString(char c)
		{
			return string.CreateFromChar(c);
		}

		/// <summary>Converts the value of the specified string to its equivalent Unicode character.</summary>
		/// <returns>A Unicode character equivalent to the sole character in <paramref name="s" />.</returns>
		/// <param name="s">A string that contains a single character, or null. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null. </exception>
		/// <exception cref="T:System.FormatException">The length of <paramref name="s" /> is not 1. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000550 RID: 1360 RVA: 0x0001984D File Offset: 0x00017A4D
		public static char Parse(string s)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (s.Length != 1)
			{
				throw new FormatException("String must be exactly one character long.");
			}
			return s[0];
		}

		/// <summary>Converts the value of the specified string to its equivalent Unicode character. A return code indicates whether the conversion succeeded or failed.</summary>
		/// <returns>true if the <paramref name="s" /> parameter was converted successfully; otherwise, false.</returns>
		/// <param name="s">A string that contains a single character, or null. </param>
		/// <param name="result">When this method returns, contains a Unicode character equivalent to the sole character in <paramref name="s" />, if the conversion succeeded, or an undefined value if the conversion failed. The conversion fails if the <paramref name="s" /> parameter is null or the length of <paramref name="s" /> is not 1. This parameter is passed uninitialized. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000551 RID: 1361 RVA: 0x00019878 File Offset: 0x00017A78
		public static bool TryParse(string s, out char result)
		{
			result = '\0';
			if (s == null)
			{
				return false;
			}
			if (s.Length != 1)
			{
				return false;
			}
			result = s[0];
			return true;
		}

		/// <summary>Indicates whether the specified Unicode character is categorized as a decimal digit.</summary>
		/// <returns>true if <paramref name="c" /> is a decimal digit; otherwise, false.</returns>
		/// <param name="c">The Unicode character to evaluate. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000552 RID: 1362 RVA: 0x00019897 File Offset: 0x00017A97
		public static bool IsDigit(char c)
		{
			if (char.IsLatin1(c))
			{
				return c >= '0' && c <= '9';
			}
			return CharUnicodeInfo.GetUnicodeCategory(c) == UnicodeCategory.DecimalDigitNumber;
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x000198BA File Offset: 0x00017ABA
		internal static bool CheckLetter(UnicodeCategory uc)
		{
			return uc <= UnicodeCategory.OtherLetter;
		}

		/// <summary>Indicates whether the specified Unicode character is categorized as a Unicode letter. </summary>
		/// <returns>true if <paramref name="c" /> is a letter; otherwise, false.</returns>
		/// <param name="c">The Unicode character to evaluate. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000554 RID: 1364 RVA: 0x000198C3 File Offset: 0x00017AC3
		public static bool IsLetter(char c)
		{
			if (!char.IsLatin1(c))
			{
				return char.CheckLetter(CharUnicodeInfo.GetUnicodeCategory(c));
			}
			if (char.IsAscii(c))
			{
				c |= ' ';
				return c >= 'a' && c <= 'z';
			}
			return char.CheckLetter(char.GetLatin1UnicodeCategory(c));
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x00019903 File Offset: 0x00017B03
		private static bool IsWhiteSpaceLatin1(char c)
		{
			return c == ' ' || c - '\t' <= '\u0004' || c == '\u00a0' || c == '\u0085';
		}

		/// <summary>Indicates whether the specified Unicode character is categorized as white space.</summary>
		/// <returns>true if <paramref name="c" /> is white space; otherwise, false.</returns>
		/// <param name="c">The Unicode character to evaluate. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000556 RID: 1366 RVA: 0x00019923 File Offset: 0x00017B23
		public static bool IsWhiteSpace(char c)
		{
			if (char.IsLatin1(c))
			{
				return char.IsWhiteSpaceLatin1(c);
			}
			return CharUnicodeInfo.IsWhiteSpace(c);
		}

		/// <summary>Indicates whether the specified Unicode character is categorized as an uppercase letter.</summary>
		/// <returns>true if <paramref name="c" /> is an uppercase letter; otherwise, false.</returns>
		/// <param name="c">The Unicode character to evaluate. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000557 RID: 1367 RVA: 0x0001993A File Offset: 0x00017B3A
		public static bool IsUpper(char c)
		{
			if (!char.IsLatin1(c))
			{
				return CharUnicodeInfo.GetUnicodeCategory(c) == UnicodeCategory.UppercaseLetter;
			}
			if (char.IsAscii(c))
			{
				return c >= 'A' && c <= 'Z';
			}
			return char.GetLatin1UnicodeCategory(c) == UnicodeCategory.UppercaseLetter;
		}

		/// <summary>Indicates whether the specified Unicode character is categorized as a lowercase letter.</summary>
		/// <returns>true if <paramref name="c" /> is a lowercase letter; otherwise, false.</returns>
		/// <param name="c">The Unicode character to evaluate. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000558 RID: 1368 RVA: 0x0001996F File Offset: 0x00017B6F
		public static bool IsLower(char c)
		{
			if (!char.IsLatin1(c))
			{
				return CharUnicodeInfo.GetUnicodeCategory(c) == UnicodeCategory.LowercaseLetter;
			}
			if (char.IsAscii(c))
			{
				return c >= 'a' && c <= 'z';
			}
			return char.GetLatin1UnicodeCategory(c) == UnicodeCategory.LowercaseLetter;
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x000199A4 File Offset: 0x00017BA4
		internal static bool CheckPunctuation(UnicodeCategory uc)
		{
			return uc - UnicodeCategory.ConnectorPunctuation <= 6;
		}

		/// <summary>Indicates whether the specified Unicode character is categorized as a punctuation mark.</summary>
		/// <returns>true if <paramref name="c" /> is a punctuation mark; otherwise, false.</returns>
		/// <param name="c">The Unicode character to evaluate. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600055A RID: 1370 RVA: 0x000199B0 File Offset: 0x00017BB0
		public static bool IsPunctuation(char c)
		{
			if (char.IsLatin1(c))
			{
				return char.CheckPunctuation(char.GetLatin1UnicodeCategory(c));
			}
			return char.CheckPunctuation(CharUnicodeInfo.GetUnicodeCategory(c));
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x000199D1 File Offset: 0x00017BD1
		internal static bool CheckLetterOrDigit(UnicodeCategory uc)
		{
			return uc <= UnicodeCategory.OtherLetter || uc == UnicodeCategory.DecimalDigitNumber;
		}

		/// <summary>Indicates whether the specified Unicode character is categorized as a letter or a decimal digit.</summary>
		/// <returns>true if <paramref name="c" /> is a letter or a decimal digit; otherwise, false.</returns>
		/// <param name="c">The Unicode character to evaluate. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600055C RID: 1372 RVA: 0x000199DE File Offset: 0x00017BDE
		public static bool IsLetterOrDigit(char c)
		{
			if (char.IsLatin1(c))
			{
				return char.CheckLetterOrDigit(char.GetLatin1UnicodeCategory(c));
			}
			return char.CheckLetterOrDigit(CharUnicodeInfo.GetUnicodeCategory(c));
		}

		/// <summary>Converts the value of a specified Unicode character to its uppercase equivalent using specified culture-specific formatting information.</summary>
		/// <returns>The uppercase equivalent of <paramref name="c" />, modified according to <paramref name="culture" />, or the unchanged value of <paramref name="c" /> if <paramref name="c" /> is already uppercase, has no uppercase equivalent, or is not alphabetic.</returns>
		/// <param name="c">The Unicode character to convert. </param>
		/// <param name="culture">An object that supplies culture-specific casing rules. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="culture" /> is null. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600055D RID: 1373 RVA: 0x000199FF File Offset: 0x00017BFF
		public static char ToUpper(char c, CultureInfo culture)
		{
			if (culture == null)
			{
				throw new ArgumentNullException("culture");
			}
			return culture.TextInfo.ToUpper(c);
		}

		/// <summary>Converts the value of a Unicode character to its uppercase equivalent.</summary>
		/// <returns>The uppercase equivalent of <paramref name="c" />, or the unchanged value of <paramref name="c" /> if <paramref name="c" /> is already uppercase, has no uppercase equivalent, or is not alphabetic.</returns>
		/// <param name="c">The Unicode character to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600055E RID: 1374 RVA: 0x00019A1B File Offset: 0x00017C1B
		public static char ToUpper(char c)
		{
			return CultureInfo.CurrentCulture.TextInfo.ToUpper(c);
		}

		/// <summary>Converts the value of a Unicode character to its uppercase equivalent using the casing rules of the invariant culture.</summary>
		/// <returns>The uppercase equivalent of the <paramref name="c" /> parameter, or the unchanged value of <paramref name="c" />, if <paramref name="c" /> is already uppercase or not alphabetic.</returns>
		/// <param name="c">The Unicode character to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600055F RID: 1375 RVA: 0x00019A2D File Offset: 0x00017C2D
		public static char ToUpperInvariant(char c)
		{
			return CultureInfo.InvariantCulture.TextInfo.ToUpper(c);
		}

		/// <summary>Converts the value of a specified Unicode character to its lowercase equivalent using specified culture-specific formatting information.</summary>
		/// <returns>The lowercase equivalent of <paramref name="c" />, modified according to <paramref name="culture" />, or the unchanged value of <paramref name="c" />, if <paramref name="c" /> is already lowercase or not alphabetic.</returns>
		/// <param name="c">The Unicode character to convert. </param>
		/// <param name="culture">An object that supplies culture-specific casing rules. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="culture" /> is null. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000560 RID: 1376 RVA: 0x00019A3F File Offset: 0x00017C3F
		public static char ToLower(char c, CultureInfo culture)
		{
			if (culture == null)
			{
				throw new ArgumentNullException("culture");
			}
			return culture.TextInfo.ToLower(c);
		}

		/// <summary>Converts the value of a Unicode character to its lowercase equivalent.</summary>
		/// <returns>The lowercase equivalent of <paramref name="c" />, or the unchanged value of <paramref name="c" />, if <paramref name="c" /> is already lowercase or not alphabetic.</returns>
		/// <param name="c">The Unicode character to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000561 RID: 1377 RVA: 0x00019A5B File Offset: 0x00017C5B
		public static char ToLower(char c)
		{
			return CultureInfo.CurrentCulture.TextInfo.ToLower(c);
		}

		/// <summary>Converts the value of a Unicode character to its lowercase equivalent using the casing rules of the invariant culture.</summary>
		/// <returns>The lowercase equivalent of the <paramref name="c" /> parameter, or the unchanged value of <paramref name="c" />, if <paramref name="c" /> is already lowercase or not alphabetic.</returns>
		/// <param name="c">The Unicode character to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000562 RID: 1378 RVA: 0x00019A6D File Offset: 0x00017C6D
		public static char ToLowerInvariant(char c)
		{
			return CultureInfo.InvariantCulture.TextInfo.ToLower(c);
		}

		/// <summary>Returns the <see cref="T:System.TypeCode" /> for value type <see cref="T:System.Char" />.</summary>
		/// <returns>The enumerated constant, <see cref="F:System.TypeCode.Char" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000563 RID: 1379 RVA: 0x00019A7F File Offset: 0x00017C7F
		public TypeCode GetTypeCode()
		{
			return TypeCode.Char;
		}

		/// <summary>Note   This conversion is not supported. Attempting to do so throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported.</exception>
		// Token: 0x06000564 RID: 1380 RVA: 0x00019A82 File Offset: 0x00017C82
		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			throw new InvalidCastException(SR.Format("Invalid cast from '{0}' to '{1}'.", "Char", "Boolean"));
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToChar(System.IFormatProvider)" />.</summary>
		/// <returns>The value of the current <see cref="T:System.Char" /> object unchanged.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x06000565 RID: 1381 RVA: 0x00019A9D File Offset: 0x00017C9D
		char IConvertible.ToChar(IFormatProvider provider)
		{
			return this;
		}

		/// <summary> For a description of this member, see <see cref="M:System.IConvertible.ToSByte(System.IFormatProvider)" />.</summary>
		/// <returns>The converted value of the current <see cref="T:System.Char" /> object.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x06000566 RID: 1382 RVA: 0x00019AA1 File Offset: 0x00017CA1
		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			return Convert.ToSByte(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToByte(System.IFormatProvider)" />.</summary>
		/// <returns>The converted value of the current <see cref="T:System.Char" /> object.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x06000567 RID: 1383 RVA: 0x00019AAA File Offset: 0x00017CAA
		byte IConvertible.ToByte(IFormatProvider provider)
		{
			return Convert.ToByte(this);
		}

		/// <summary> For a description of this member, see <see cref="M:System.IConvertible.ToInt16(System.IFormatProvider)" />.</summary>
		/// <returns>The converted value of the current <see cref="T:System.Char" /> object.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x06000568 RID: 1384 RVA: 0x00019AB3 File Offset: 0x00017CB3
		short IConvertible.ToInt16(IFormatProvider provider)
		{
			return Convert.ToInt16(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToUInt16(System.IFormatProvider)" />.</summary>
		/// <returns>The converted value of the current <see cref="T:System.Char" /> object.</returns>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> object. (Specify null because the <paramref name="provider" /> parameter is ignored.)</param>
		// Token: 0x06000569 RID: 1385 RVA: 0x00019ABC File Offset: 0x00017CBC
		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			return Convert.ToUInt16(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToInt32(System.IFormatProvider)" />.</summary>
		/// <returns>The converted value of the current <see cref="T:System.Char" /> object.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x0600056A RID: 1386 RVA: 0x00019AC5 File Offset: 0x00017CC5
		int IConvertible.ToInt32(IFormatProvider provider)
		{
			return Convert.ToInt32(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToUInt32(System.IFormatProvider)" />.</summary>
		/// <returns>The converted value of the current <see cref="T:System.Char" /> object.</returns>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> object. (Specify null because the <paramref name="provider" /> parameter is ignored.)</param>
		// Token: 0x0600056B RID: 1387 RVA: 0x00019ACE File Offset: 0x00017CCE
		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			return Convert.ToUInt32(this);
		}

		/// <summary> For a description of this member, see <see cref="M:System.IConvertible.ToInt64(System.IFormatProvider)" />.</summary>
		/// <returns>The converted value of the current <see cref="T:System.Char" /> object.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x0600056C RID: 1388 RVA: 0x00019AD7 File Offset: 0x00017CD7
		long IConvertible.ToInt64(IFormatProvider provider)
		{
			return Convert.ToInt64(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToUInt64(System.IFormatProvider)" />.</summary>
		/// <returns>The converted value of the current <see cref="T:System.Char" /> object.</returns>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> object. (Specify null because the <paramref name="provider" /> parameter is ignored.)</param>
		// Token: 0x0600056D RID: 1389 RVA: 0x00019AE0 File Offset: 0x00017CE0
		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			return Convert.ToUInt64(this);
		}

		/// <summary>Note   This conversion is not supported. Attempting to do so throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>No value is returned.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported.</exception>
		// Token: 0x0600056E RID: 1390 RVA: 0x00019AE9 File Offset: 0x00017CE9
		float IConvertible.ToSingle(IFormatProvider provider)
		{
			throw new InvalidCastException(SR.Format("Invalid cast from '{0}' to '{1}'.", "Char", "Single"));
		}

		/// <summary>Note   This conversion is not supported. Attempting to do so throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>No value is returned.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported.</exception>
		// Token: 0x0600056F RID: 1391 RVA: 0x00019B04 File Offset: 0x00017D04
		double IConvertible.ToDouble(IFormatProvider provider)
		{
			throw new InvalidCastException(SR.Format("Invalid cast from '{0}' to '{1}'.", "Char", "Double"));
		}

		/// <summary>Note   This conversion is not supported. Attempting to do so throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>No value is returned.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported.</exception>
		// Token: 0x06000570 RID: 1392 RVA: 0x00019B1F File Offset: 0x00017D1F
		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			throw new InvalidCastException(SR.Format("Invalid cast from '{0}' to '{1}'.", "Char", "Decimal"));
		}

		/// <summary>Note   This conversion is not supported. Attempting to do so throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>No value is returned.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported.</exception>
		// Token: 0x06000571 RID: 1393 RVA: 0x00019B3A File Offset: 0x00017D3A
		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			throw new InvalidCastException(SR.Format("Invalid cast from '{0}' to '{1}'.", "Char", "DateTime"));
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToType(System.Type,System.IFormatProvider)" />.</summary>
		/// <returns>An object of the specified type.</returns>
		/// <param name="type">A <see cref="T:System.Type" /> object. </param>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> object. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is null.</exception>
		/// <exception cref="T:System.InvalidCastException">The value of the current <see cref="T:System.Char" /> object cannot be converted to the type specified by the <paramref name="type" /> parameter. </exception>
		// Token: 0x06000572 RID: 1394 RVA: 0x00019B55 File Offset: 0x00017D55
		object IConvertible.ToType(Type type, IFormatProvider provider)
		{
			return Convert.DefaultToType(this, type, provider);
		}

		/// <summary>Indicates whether the specified Unicode character is categorized as a control character.</summary>
		/// <returns>true if <paramref name="c" /> is a control character; otherwise, false.</returns>
		/// <param name="c">The Unicode character to evaluate. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000573 RID: 1395 RVA: 0x00019B65 File Offset: 0x00017D65
		public static bool IsControl(char c)
		{
			if (char.IsLatin1(c))
			{
				return char.GetLatin1UnicodeCategory(c) == UnicodeCategory.Control;
			}
			return CharUnicodeInfo.GetUnicodeCategory(c) == UnicodeCategory.Control;
		}

		/// <summary>Indicates whether the character at the specified position in a specified string is categorized as a decimal digit.</summary>
		/// <returns>true if the character at position <paramref name="index" /> in <paramref name="s" /> is a decimal digit; otherwise, false.</returns>
		/// <param name="s">A string. </param>
		/// <param name="index">The position of the character to evaluate in <paramref name="s" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than the last position in <paramref name="s" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000574 RID: 1396 RVA: 0x00019B84 File Offset: 0x00017D84
		public static bool IsDigit(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			char c = s[index];
			if (char.IsLatin1(c))
			{
				return c >= '0' && c <= '9';
			}
			return CharUnicodeInfo.GetUnicodeCategory(s, index) == UnicodeCategory.DecimalDigitNumber;
		}

		/// <summary>Indicates whether the character at the specified position in a specified string is categorized as a letter or a decimal digit.</summary>
		/// <returns>true if the character at position <paramref name="index" /> in <paramref name="s" /> is a letter or a decimal digit; otherwise, false.</returns>
		/// <param name="s">A string. </param>
		/// <param name="index">The position of the character to evaluate in <paramref name="s" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than the last position in <paramref name="s" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000575 RID: 1397 RVA: 0x00019BE0 File Offset: 0x00017DE0
		public static bool IsLetterOrDigit(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			char c = s[index];
			if (char.IsLatin1(c))
			{
				return char.CheckLetterOrDigit(char.GetLatin1UnicodeCategory(c));
			}
			return char.CheckLetterOrDigit(CharUnicodeInfo.GetUnicodeCategory(s, index));
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x00019C37 File Offset: 0x00017E37
		internal static bool CheckNumber(UnicodeCategory uc)
		{
			return uc - UnicodeCategory.DecimalDigitNumber <= 2;
		}

		/// <summary>Indicates whether the specified Unicode character is categorized as a number.</summary>
		/// <returns>true if <paramref name="c" /> is a number; otherwise, false.</returns>
		/// <param name="c">The Unicode character to evaluate. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000577 RID: 1399 RVA: 0x00019C42 File Offset: 0x00017E42
		public static bool IsNumber(char c)
		{
			if (!char.IsLatin1(c))
			{
				return char.CheckNumber(CharUnicodeInfo.GetUnicodeCategory(c));
			}
			if (char.IsAscii(c))
			{
				return c >= '0' && c <= '9';
			}
			return char.CheckNumber(char.GetLatin1UnicodeCategory(c));
		}

		/// <summary>Indicates whether the character at the specified position in a specified string is categorized as a number.</summary>
		/// <returns>true if the character at position <paramref name="index" /> in <paramref name="s" /> is a number; otherwise, false.</returns>
		/// <param name="s">A string. </param>
		/// <param name="index">The position of the character to evaluate in <paramref name="s" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than the last position in <paramref name="s" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000578 RID: 1400 RVA: 0x00019C7C File Offset: 0x00017E7C
		public static bool IsNumber(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			char c = s[index];
			if (!char.IsLatin1(c))
			{
				return char.CheckNumber(CharUnicodeInfo.GetUnicodeCategory(s, index));
			}
			if (char.IsAscii(c))
			{
				return c >= '0' && c <= '9';
			}
			return char.CheckNumber(char.GetLatin1UnicodeCategory(c));
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x00019CEB File Offset: 0x00017EEB
		internal static bool CheckSeparator(UnicodeCategory uc)
		{
			return uc - UnicodeCategory.SpaceSeparator <= 2;
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x00019CF7 File Offset: 0x00017EF7
		private static bool IsSeparatorLatin1(char c)
		{
			return c == ' ' || c == '\u00a0';
		}

		/// <summary>Indicates whether the specified Unicode character is categorized as a separator character.</summary>
		/// <returns>true if <paramref name="c" /> is a separator character; otherwise, false.</returns>
		/// <param name="c">The Unicode character to evaluate. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600057B RID: 1403 RVA: 0x00019D08 File Offset: 0x00017F08
		public static bool IsSeparator(char c)
		{
			if (char.IsLatin1(c))
			{
				return char.IsSeparatorLatin1(c);
			}
			return char.CheckSeparator(CharUnicodeInfo.GetUnicodeCategory(c));
		}

		/// <summary>Indicates whether the specified character has a surrogate code unit.</summary>
		/// <returns>true if <paramref name="c" /> is either a high surrogate or a low surrogate; otherwise, false.</returns>
		/// <param name="c">The Unicode character to evaluate. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600057C RID: 1404 RVA: 0x00019D24 File Offset: 0x00017F24
		public static bool IsSurrogate(char c)
		{
			return c >= '\ud800' && c <= '\udfff';
		}

		/// <summary>Indicates whether the character at the specified position in a specified string has a surrogate code unit.</summary>
		/// <returns>true if the character at position <paramref name="index" /> in <paramref name="s" /> is a either a high surrogate or a low surrogate; otherwise, false.</returns>
		/// <param name="s">A string. </param>
		/// <param name="index">The position of the character to evaluate in <paramref name="s" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than the last position in <paramref name="s" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600057D RID: 1405 RVA: 0x00019D3B File Offset: 0x00017F3B
		public static bool IsSurrogate(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			return char.IsSurrogate(s[index]);
		}

		/// <summary>Indicates whether the character at the specified position in a specified string is categorized as white space.</summary>
		/// <returns>true if the character at position <paramref name="index" /> in <paramref name="s" /> is white space; otherwise, false.</returns>
		/// <param name="s">A string. </param>
		/// <param name="index">The position of the character to evaluate in <paramref name="s" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than the last position in <paramref name="s" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600057E RID: 1406 RVA: 0x00019D6C File Offset: 0x00017F6C
		public static bool IsWhiteSpace(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (char.IsLatin1(s[index]))
			{
				return char.IsWhiteSpaceLatin1(s[index]);
			}
			return CharUnicodeInfo.IsWhiteSpace(s, index);
		}

		/// <summary>Categorizes a specified Unicode character into a group identified by one of the <see cref="T:System.Globalization.UnicodeCategory" /> values.</summary>
		/// <returns>A <see cref="T:System.Globalization.UnicodeCategory" /> value that identifies the group that contains <paramref name="c" />.</returns>
		/// <param name="c">The Unicode character to categorize. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600057F RID: 1407 RVA: 0x00019DBD File Offset: 0x00017FBD
		public static UnicodeCategory GetUnicodeCategory(char c)
		{
			if (char.IsLatin1(c))
			{
				return char.GetLatin1UnicodeCategory(c);
			}
			return CharUnicodeInfo.GetUnicodeCategory((int)c);
		}

		/// <summary>Categorizes the character at the specified position in a specified string into a group identified by one of the <see cref="T:System.Globalization.UnicodeCategory" /> values.</summary>
		/// <returns>A <see cref="T:System.Globalization.UnicodeCategory" /> enumerated constant that identifies the group that contains the character at position <paramref name="index" /> in <paramref name="s" />.</returns>
		/// <param name="s">A <see cref="T:System.String" />. </param>
		/// <param name="index">The character position in <paramref name="s" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than the last position in <paramref name="s" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000580 RID: 1408 RVA: 0x00019DD4 File Offset: 0x00017FD4
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
			if (char.IsLatin1(s[index]))
			{
				return char.GetLatin1UnicodeCategory(s[index]);
			}
			return CharUnicodeInfo.InternalGetUnicodeCategory(s, index);
		}

		/// <summary>Indicates whether the specified <see cref="T:System.Char" /> object is a high surrogate.</summary>
		/// <returns>true if the numeric value of the <paramref name="c" /> parameter ranges from U+D800 through U+DBFF; otherwise, false.</returns>
		/// <param name="c">The Unicode character to evaluate. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000581 RID: 1409 RVA: 0x00019E25 File Offset: 0x00018025
		public static bool IsHighSurrogate(char c)
		{
			return c >= '\ud800' && c <= '\udbff';
		}

		/// <summary>Indicates whether the <see cref="T:System.Char" /> object at the specified position in a string is a high surrogate.</summary>
		/// <returns>true if the numeric value of the specified character in the <paramref name="s" /> parameter ranges from U+D800 through U+DBFF; otherwise, false.</returns>
		/// <param name="s">A string. </param>
		/// <param name="index">The position of the character to evaluate in <paramref name="s" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is not a position within <paramref name="s" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000582 RID: 1410 RVA: 0x00019E3C File Offset: 0x0001803C
		public static bool IsHighSurrogate(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index < 0 || index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			return char.IsHighSurrogate(s[index]);
		}

		/// <summary>Indicates whether the specified <see cref="T:System.Char" /> object is a low surrogate.</summary>
		/// <returns>true if the numeric value of the <paramref name="c" /> parameter ranges from U+DC00 through U+DFFF; otherwise, false.</returns>
		/// <param name="c">The character to evaluate. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000583 RID: 1411 RVA: 0x00019E70 File Offset: 0x00018070
		public static bool IsLowSurrogate(char c)
		{
			return c >= '\udc00' && c <= '\udfff';
		}

		/// <summary>Indicates whether the two specified <see cref="T:System.Char" /> objects form a surrogate pair.</summary>
		/// <returns>true if the numeric value of the <paramref name="highSurrogate" /> parameter ranges from U+D800 through U+DBFF, and the numeric value of the <paramref name="lowSurrogate" /> parameter ranges from U+DC00 through U+DFFF; otherwise, false.</returns>
		/// <param name="highSurrogate">The character to evaluate as the high surrogate of a surrogate pair. </param>
		/// <param name="lowSurrogate">The character to evaluate as the low surrogate of a surrogate pair. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000584 RID: 1412 RVA: 0x00019E87 File Offset: 0x00018087
		public static bool IsSurrogatePair(char highSurrogate, char lowSurrogate)
		{
			return highSurrogate >= '\ud800' && highSurrogate <= '\udbff' && lowSurrogate >= '\udc00' && lowSurrogate <= '\udfff';
		}

		/// <summary>Converts the specified Unicode code point into a UTF-16 encoded string.</summary>
		/// <returns>A string consisting of one <see cref="T:System.Char" /> object or a surrogate pair of <see cref="T:System.Char" /> objects equivalent to the code point specified by the <paramref name="utf32" /> parameter.</returns>
		/// <param name="utf32">A 21-bit Unicode code point. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="utf32" /> is not a valid 21-bit Unicode code point ranging from U+0 through U+10FFFF, excluding the surrogate pair range from U+D800 through U+DFFF. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000585 RID: 1413 RVA: 0x00019EB0 File Offset: 0x000180B0
		public unsafe static string ConvertFromUtf32(int utf32)
		{
			if (utf32 < 0 || utf32 > 1114111 || (utf32 >= 55296 && utf32 <= 57343))
			{
				throw new ArgumentOutOfRangeException("utf32", "A valid UTF32 value is between 0x000000 and 0x10ffff, inclusive, and should not include surrogate codepoint values (0x00d800 ~ 0x00dfff).");
			}
			if (utf32 < 65536)
			{
				return char.ToString((char)utf32);
			}
			utf32 -= 65536;
			uint num = 0U;
			char* ptr = (char*)(&num);
			*ptr = (char)(utf32 / 1024 + 55296);
			ptr[1] = (char)(utf32 % 1024 + 56320);
			return new string(ptr, 0, 2);
		}

		/// <summary>Converts the value of a UTF-16 encoded surrogate pair into a Unicode code point.</summary>
		/// <returns>The 21-bit Unicode code point represented by the <paramref name="highSurrogate" /> and <paramref name="lowSurrogate" /> parameters.</returns>
		/// <param name="highSurrogate">A high surrogate code unit (that is, a code unit ranging from U+D800 through U+DBFF). </param>
		/// <param name="lowSurrogate">A low surrogate code unit (that is, a code unit ranging from U+DC00 through U+DFFF). </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="highSurrogate" /> is not in the range U+D800 through U+DBFF, or <paramref name="lowSurrogate" /> is not in the range U+DC00 through U+DFFF. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000586 RID: 1414 RVA: 0x00019F34 File Offset: 0x00018134
		public static int ConvertToUtf32(char highSurrogate, char lowSurrogate)
		{
			if (!char.IsHighSurrogate(highSurrogate))
			{
				throw new ArgumentOutOfRangeException("highSurrogate", "A valid high surrogate character is between 0xd800 and 0xdbff, inclusive.");
			}
			if (!char.IsLowSurrogate(lowSurrogate))
			{
				throw new ArgumentOutOfRangeException("lowSurrogate", "A valid low surrogate character is between 0xdc00 and 0xdfff, inclusive.");
			}
			return (int)((highSurrogate - '\ud800') * 'Ѐ' + (lowSurrogate - '\udc00')) + 65536;
		}

		// Token: 0x040002E1 RID: 737
		private readonly char m_value;

		// Token: 0x040002E2 RID: 738
		private static readonly byte[] s_categoryForLatin1 = new byte[]
		{
			14, 14, 14, 14, 14, 14, 14, 14, 14, 14,
			14, 14, 14, 14, 14, 14, 14, 14, 14, 14,
			14, 14, 14, 14, 14, 14, 14, 14, 14, 14,
			14, 14, 11, 24, 24, 24, 26, 24, 24, 24,
			20, 21, 24, 25, 24, 19, 24, 24, 8, 8,
			8, 8, 8, 8, 8, 8, 8, 8, 24, 24,
			25, 25, 25, 24, 24, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 20, 24, 21, 27, 18, 27, 1, 1, 1,
			1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
			1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
			1, 1, 1, 20, 25, 21, 25, 14, 14, 14,
			14, 14, 14, 14, 14, 14, 14, 14, 14, 14,
			14, 14, 14, 14, 14, 14, 14, 14, 14, 14,
			14, 14, 14, 14, 14, 14, 14, 14, 14, 14,
			11, 24, 26, 26, 26, 26, 28, 28, 27, 28,
			1, 22, 25, 19, 28, 27, 28, 25, 10, 10,
			27, 1, 28, 24, 27, 10, 1, 23, 10, 10,
			10, 24, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 25, 0, 0, 0, 0,
			0, 0, 0, 1, 1, 1, 1, 1, 1, 1,
			1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
			1, 1, 1, 1, 1, 1, 1, 25, 1, 1,
			1, 1, 1, 1, 1, 1
		};
	}
}
