using System;
using System.Globalization;
using System.Runtime.Versioning;

namespace System
{
	/// <summary>Represents a 32-bit signed integer.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200010F RID: 271
	[Serializable]
	public readonly struct Int32 : IComparable, IConvertible, IFormattable, IComparable<int>, IEquatable<int>, ISpanFormattable
	{
		/// <summary>Compares this instance to a specified object and returns an indication of their relative values.</summary>
		/// <returns>A signed number indicating the relative values of this instance and <paramref name="value" />.Return Value Description Less than zero This instance is less than <paramref name="value" />. Zero This instance is equal to <paramref name="value" />. Greater than zero This instance is greater than <paramref name="value" />.-or- <paramref name="value" /> is null. </returns>
		/// <param name="value">An object to compare, or null. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> is not an <see cref="T:System.Int32" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060008C7 RID: 2247 RVA: 0x00027B4C File Offset: 0x00025D4C
		public int CompareTo(object value)
		{
			if (value == null)
			{
				return 1;
			}
			if (!(value is int))
			{
				throw new ArgumentException("Object must be of type Int32.");
			}
			int num = (int)value;
			if (this < num)
			{
				return -1;
			}
			if (this > num)
			{
				return 1;
			}
			return 0;
		}

		/// <summary>Compares this instance to a specified 32-bit signed integer and returns an indication of their relative values.</summary>
		/// <returns>A signed number indicating the relative values of this instance and <paramref name="value" />.Return Value Description Less than zero This instance is less than <paramref name="value" />. Zero This instance is equal to <paramref name="value" />. Greater than zero This instance is greater than <paramref name="value" />. </returns>
		/// <param name="value">An integer to compare. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060008C8 RID: 2248 RVA: 0x00027B87 File Offset: 0x00025D87
		public int CompareTo(int value)
		{
			if (this < value)
			{
				return -1;
			}
			if (this > value)
			{
				return 1;
			}
			return 0;
		}

		/// <summary>Returns a value indicating whether this instance is equal to a specified object.</summary>
		/// <returns>true if <paramref name="obj" /> is an instance of <see cref="T:System.Int32" /> and equals the value of this instance; otherwise, false.</returns>
		/// <param name="obj">An object to compare with this instance. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060008C9 RID: 2249 RVA: 0x00027B98 File Offset: 0x00025D98
		public override bool Equals(object obj)
		{
			return obj is int && this == (int)obj;
		}

		/// <summary>Returns a value indicating whether this instance is equal to a specified <see cref="T:System.Int32" /> value. </summary>
		/// <returns>true if <paramref name="obj" /> has the same value as this instance; otherwise, false.</returns>
		/// <param name="obj">An <see cref="T:System.Int32" /> value to compare to this instance.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060008CA RID: 2250 RVA: 0x00027BAE File Offset: 0x00025DAE
		[NonVersionable]
		public bool Equals(int obj)
		{
			return this == obj;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060008CB RID: 2251 RVA: 0x00027BB5 File Offset: 0x00025DB5
		public override int GetHashCode()
		{
			return this;
		}

		/// <summary>Converts the numeric value of this instance to its equivalent string representation.</summary>
		/// <returns>The string representation of the value of this instance, consisting of a negative sign if the value is negative, and a sequence of digits ranging from 0 to 9 with no leading zeroes.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060008CC RID: 2252 RVA: 0x00027BB9 File Offset: 0x00025DB9
		public override string ToString()
		{
			return Number.FormatInt32(this, null, null);
		}

		/// <summary>Converts the numeric value of this instance to its equivalent string representation, using the specified format.</summary>
		/// <returns>The string representation of the value of this instance as specified by <paramref name="format" />.</returns>
		/// <param name="format">A standard or custom numeric format string.</param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="format" /> is invalid or not supported. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060008CD RID: 2253 RVA: 0x00027BC9 File Offset: 0x00025DC9
		public string ToString(string format)
		{
			return Number.FormatInt32(this, format, null);
		}

		/// <summary>Converts the numeric value of this instance to its equivalent string representation using the specified culture-specific format information.</summary>
		/// <returns>The string representation of the value of this instance as specified by <paramref name="provider" />.</returns>
		/// <param name="provider">An object that supplies culture-specific formatting information. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060008CE RID: 2254 RVA: 0x00027BD9 File Offset: 0x00025DD9
		public string ToString(IFormatProvider provider)
		{
			return Number.FormatInt32(this, null, provider);
		}

		/// <summary>Converts the numeric value of this instance to its equivalent string representation using the specified format and culture-specific format information.</summary>
		/// <returns>The string representation of the value of this instance as specified by <paramref name="format" /> and <paramref name="provider" />.</returns>
		/// <param name="format">A standard or custom numeric format string.</param>
		/// <param name="provider">An object that supplies culture-specific formatting information. </param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="format" /> is invalid or not supported.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060008CF RID: 2255 RVA: 0x00027BE9 File Offset: 0x00025DE9
		public string ToString(string format, IFormatProvider provider)
		{
			return Number.FormatInt32(this, format, provider);
		}

		// Token: 0x060008D0 RID: 2256 RVA: 0x00027BF9 File Offset: 0x00025DF9
		public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format = default(ReadOnlySpan<char>), IFormatProvider provider = null)
		{
			return Number.TryFormatInt32(this, format, provider, destination, out charsWritten);
		}

		/// <summary>Converts the string representation of a number to its 32-bit signed integer equivalent.</summary>
		/// <returns>A 32-bit signed integer equivalent to the number contained in <paramref name="s" />.</returns>
		/// <param name="s">A string containing a number to convert. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> is not in the correct format. </exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="s" /> represents a number less than <see cref="F:System.Int32.MinValue" /> or greater than <see cref="F:System.Int32.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060008D1 RID: 2257 RVA: 0x00027C07 File Offset: 0x00025E07
		public static int Parse(string s)
		{
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			return Number.ParseInt32(s, NumberStyles.Integer, NumberFormatInfo.CurrentInfo);
		}

		/// <summary>Converts the string representation of a number in a specified style to its 32-bit signed integer equivalent.</summary>
		/// <returns>A 32-bit signed integer equivalent to the number specified in <paramref name="s" />.</returns>
		/// <param name="s">A string containing a number to convert. </param>
		/// <param name="style">A bitwise combination of the enumeration values that indicates the style elements that can be present in <paramref name="s" />. A typical value to specify is <see cref="F:System.Globalization.NumberStyles.Integer" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="style" /> is not a <see cref="T:System.Globalization.NumberStyles" /> value. -or-<paramref name="style" /> is not a combination of <see cref="F:System.Globalization.NumberStyles.AllowHexSpecifier" /> and <see cref="F:System.Globalization.NumberStyles.HexNumber" /> values.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> is not in a format compliant with <paramref name="style" />. </exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="s" /> represents a number less than <see cref="F:System.Int32.MinValue" /> or greater than <see cref="F:System.Int32.MaxValue" />. -or-<paramref name="s" /> includes non-zero, fractional digits.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060008D2 RID: 2258 RVA: 0x00027C24 File Offset: 0x00025E24
		public static int Parse(string s, NumberStyles style)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			return Number.ParseInt32(s, style, NumberFormatInfo.CurrentInfo);
		}

		/// <summary>Converts the string representation of a number in a specified culture-specific format to its 32-bit signed integer equivalent.</summary>
		/// <returns>A 32-bit signed integer equivalent to the number specified in <paramref name="s" />.</returns>
		/// <param name="s">A string containing a number to convert. </param>
		/// <param name="provider">An object that supplies culture-specific formatting information about <paramref name="s" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> is not of the correct format. </exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="s" /> represents a number less than <see cref="F:System.Int32.MinValue" /> or greater than <see cref="F:System.Int32.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060008D3 RID: 2259 RVA: 0x00027C47 File Offset: 0x00025E47
		public static int Parse(string s, IFormatProvider provider)
		{
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			return Number.ParseInt32(s, NumberStyles.Integer, NumberFormatInfo.GetInstance(provider));
		}

		/// <summary>Converts the string representation of a number in a specified style and culture-specific format to its 32-bit signed integer equivalent.</summary>
		/// <returns>A 32-bit signed integer equivalent to the number specified in <paramref name="s" />.</returns>
		/// <param name="s">A string containing a number to convert. </param>
		/// <param name="style">A bitwise combination of enumeration values that indicates the style elements that can be present in <paramref name="s" />. A typical value to specify is <see cref="F:System.Globalization.NumberStyles.Integer" />.</param>
		/// <param name="provider">An object that supplies culture-specific information about the format of <paramref name="s" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="style" /> is not a <see cref="T:System.Globalization.NumberStyles" /> value. -or-<paramref name="style" /> is not a combination of <see cref="F:System.Globalization.NumberStyles.AllowHexSpecifier" /> and <see cref="F:System.Globalization.NumberStyles.HexNumber" /> values.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> is not in a format compliant with <paramref name="style" />. </exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="s" /> represents a number less than <see cref="F:System.Int32.MinValue" /> or greater than <see cref="F:System.Int32.MaxValue" />. -or-<paramref name="s" /> includes non-zero, fractional digits.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060008D4 RID: 2260 RVA: 0x00027C65 File Offset: 0x00025E65
		public static int Parse(string s, NumberStyles style, IFormatProvider provider)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			return Number.ParseInt32(s, style, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x060008D5 RID: 2261 RVA: 0x00027C89 File Offset: 0x00025E89
		public static int Parse(ReadOnlySpan<char> s, NumberStyles style = NumberStyles.Integer, IFormatProvider provider = null)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			return Number.ParseInt32(s, style, NumberFormatInfo.GetInstance(provider));
		}

		/// <summary>Converts the string representation of a number to its 32-bit signed integer equivalent. A return value indicates whether the conversion succeeded.</summary>
		/// <returns>true if <paramref name="s" /> was converted successfully; otherwise, false.</returns>
		/// <param name="s">A string containing a number to convert. </param>
		/// <param name="result">When this method returns, contains the 32-bit signed integer value equivalent to the number contained in <paramref name="s" />, if the conversion succeeded, or zero if the conversion failed. The conversion fails if the <paramref name="s" /> parameter is null, is not of the correct format, or represents a number less than <see cref="F:System.Int32.MinValue" /> or greater than <see cref="F:System.Int32.MaxValue" />. This parameter is passed uninitialized. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060008D6 RID: 2262 RVA: 0x00027C9E File Offset: 0x00025E9E
		public static bool TryParse(string s, out int result)
		{
			if (s == null)
			{
				result = 0;
				return false;
			}
			return Number.TryParseInt32(s, NumberStyles.Integer, NumberFormatInfo.CurrentInfo, out result);
		}

		/// <summary>Converts the string representation of a number in a specified style and culture-specific format to its 32-bit signed integer equivalent. A return value indicates whether the conversion succeeded.</summary>
		/// <returns>true if <paramref name="s" /> was converted successfully; otherwise, false.</returns>
		/// <param name="s">A string containing a number to convert. The string is interpreted using the style specified by <paramref name="style" />.</param>
		/// <param name="style">A bitwise combination of enumeration values that indicates the style elements that can be present in <paramref name="s" />. A typical value to specify is <see cref="F:System.Globalization.NumberStyles.Integer" />.</param>
		/// <param name="provider">An object that supplies culture-specific formatting information about <paramref name="s" />. </param>
		/// <param name="result">When this method returns, contains the 32-bit signed integer value equivalent to the number contained in <paramref name="s" />, if the conversion succeeded, or zero if the conversion failed. The conversion fails if the <paramref name="s" /> parameter is null, is not in a format compliant with <paramref name="style" />, or represents a number less than <see cref="F:System.Int32.MinValue" /> or greater than <see cref="F:System.Int32.MaxValue" />. This parameter is passed uninitialized. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="style" /> is not a <see cref="T:System.Globalization.NumberStyles" /> value. -or-<paramref name="style" /> is not a combination of <see cref="F:System.Globalization.NumberStyles.AllowHexSpecifier" /> and <see cref="F:System.Globalization.NumberStyles.HexNumber" /> values.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060008D7 RID: 2263 RVA: 0x00027CBA File Offset: 0x00025EBA
		public static bool TryParse(string s, NumberStyles style, IFormatProvider provider, out int result)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			if (s == null)
			{
				result = 0;
				return false;
			}
			return Number.TryParseInt32(s, style, NumberFormatInfo.GetInstance(provider), out result);
		}

		// Token: 0x060008D8 RID: 2264 RVA: 0x00027CDD File Offset: 0x00025EDD
		public static bool TryParse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider provider, out int result)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			return Number.TryParseInt32(s, style, NumberFormatInfo.GetInstance(provider), out result);
		}

		/// <summary>Returns the <see cref="T:System.TypeCode" /> for value type <see cref="T:System.Int32" />.</summary>
		/// <returns>The enumerated constant, <see cref="F:System.TypeCode.Int32" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060008D9 RID: 2265 RVA: 0x00027CF3 File Offset: 0x00025EF3
		public TypeCode GetTypeCode()
		{
			return TypeCode.Int32;
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToBoolean(System.IFormatProvider)" />. </summary>
		/// <returns>true if the value of the current instance is not zero; otherwise, false.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x060008DA RID: 2266 RVA: 0x00027CF7 File Offset: 0x00025EF7
		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			return Convert.ToBoolean(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToChar(System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.Char" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x060008DB RID: 2267 RVA: 0x00027D00 File Offset: 0x00025F00
		char IConvertible.ToChar(IFormatProvider provider)
		{
			return Convert.ToChar(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToSByte(System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, converted to an <see cref="T:System.SByte" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x060008DC RID: 2268 RVA: 0x00027D09 File Offset: 0x00025F09
		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			return Convert.ToSByte(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToByte(System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.Byte" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x060008DD RID: 2269 RVA: 0x00027D12 File Offset: 0x00025F12
		byte IConvertible.ToByte(IFormatProvider provider)
		{
			return Convert.ToByte(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToInt16(System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, converted to an <see cref="T:System.Int16" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x060008DE RID: 2270 RVA: 0x00027D1B File Offset: 0x00025F1B
		short IConvertible.ToInt16(IFormatProvider provider)
		{
			return Convert.ToInt16(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToUInt16(System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.UInt16" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x060008DF RID: 2271 RVA: 0x00027D24 File Offset: 0x00025F24
		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			return Convert.ToUInt16(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToInt32(System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, unchanged.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x060008E0 RID: 2272 RVA: 0x00027BB5 File Offset: 0x00025DB5
		int IConvertible.ToInt32(IFormatProvider provider)
		{
			return this;
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToUInt32(System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.UInt32" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x060008E1 RID: 2273 RVA: 0x00027D2D File Offset: 0x00025F2D
		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			return Convert.ToUInt32(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToInt64(System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, converted to an <see cref="T:System.Int64" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x060008E2 RID: 2274 RVA: 0x00027D36 File Offset: 0x00025F36
		long IConvertible.ToInt64(IFormatProvider provider)
		{
			return Convert.ToInt64(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToUInt64(System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.UInt64" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x060008E3 RID: 2275 RVA: 0x00027D3F File Offset: 0x00025F3F
		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			return Convert.ToUInt64(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToSingle(System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.Single" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x060008E4 RID: 2276 RVA: 0x00027D48 File Offset: 0x00025F48
		float IConvertible.ToSingle(IFormatProvider provider)
		{
			return Convert.ToSingle(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToDouble(System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.Double" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x060008E5 RID: 2277 RVA: 0x00027D51 File Offset: 0x00025F51
		double IConvertible.ToDouble(IFormatProvider provider)
		{
			return Convert.ToDouble(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToDecimal(System.IFormatProvider)" />.</summary>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.Decimal" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x060008E6 RID: 2278 RVA: 0x00027D5A File Offset: 0x00025F5A
		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			return Convert.ToDecimal(this);
		}

		/// <summary>This conversion is not supported. Attempting to use this method throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		/// <exception cref="T:System.InvalidCastException">In all cases.</exception>
		// Token: 0x060008E7 RID: 2279 RVA: 0x00027D63 File Offset: 0x00025F63
		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			throw new InvalidCastException(SR.Format("Invalid cast from '{0}' to '{1}'.", "Int32", "DateTime"));
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToType(System.Type,System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, converted to <paramref name="type" />.</returns>
		/// <param name="type">The type to which to convert this <see cref="T:System.Int32" /> value.</param>
		/// <param name="provider">An object that provides information about the format of the returned value.</param>
		// Token: 0x060008E8 RID: 2280 RVA: 0x00027D7E File Offset: 0x00025F7E
		object IConvertible.ToType(Type type, IFormatProvider provider)
		{
			return Convert.DefaultToType(this, type, provider);
		}

		// Token: 0x0400042E RID: 1070
		private readonly int m_value;
	}
}
