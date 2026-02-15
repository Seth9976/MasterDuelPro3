using System;
using System.Globalization;
using System.Runtime.Versioning;

namespace System
{
	/// <summary>Represents an 8-bit unsigned integer.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020000CC RID: 204
	[Serializable]
	public readonly struct Byte : IComparable, IConvertible, IFormattable, IComparable<byte>, IEquatable<byte>, ISpanFormattable
	{
		/// <summary>Compares this instance to a specified object and returns an indication of their relative values.</summary>
		/// <returns>A signed integer that indicates the relative order of this instance and <paramref name="value" />.Return Value Description Less than zero This instance is less than <paramref name="value" />. Zero This instance is equal to <paramref name="value" />. Greater than zero This instance is greater than <paramref name="value" />.-or- <paramref name="value" /> is null. </returns>
		/// <param name="value">An object to compare, or null. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> is not a <see cref="T:System.Byte" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000523 RID: 1315 RVA: 0x00019574 File Offset: 0x00017774
		public int CompareTo(object value)
		{
			if (value == null)
			{
				return 1;
			}
			if (!(value is byte))
			{
				throw new ArgumentException("Object must be of type Byte.");
			}
			return (int)(this - (byte)value);
		}

		/// <summary>Compares this instance to a specified 8-bit unsigned integer and returns an indication of their relative values.</summary>
		/// <returns>A signed integer that indicates the relative order of this instance and <paramref name="value" />.Return Value Description Less than zero This instance is less than <paramref name="value" />. Zero This instance is equal to <paramref name="value" />. Greater than zero This instance is greater than <paramref name="value" />. </returns>
		/// <param name="value">An 8-bit unsigned integer to compare. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000524 RID: 1316 RVA: 0x00019597 File Offset: 0x00017797
		public int CompareTo(byte value)
		{
			return (int)(this - value);
		}

		/// <summary>Returns a value indicating whether this instance is equal to a specified object.</summary>
		/// <returns>true if <paramref name="obj" /> is an instance of <see cref="T:System.Byte" /> and equals the value of this instance; otherwise, false.</returns>
		/// <param name="obj">An object to compare with this instance, or null. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000525 RID: 1317 RVA: 0x0001959D File Offset: 0x0001779D
		public override bool Equals(object obj)
		{
			return obj is byte && this == (byte)obj;
		}

		/// <summary>Returns a value indicating whether this instance and a specified <see cref="T:System.Byte" /> object represent the same value.</summary>
		/// <returns>true if <paramref name="obj" /> is equal to this instance; otherwise, false.</returns>
		/// <param name="obj">An object to compare to this instance.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000526 RID: 1318 RVA: 0x00019333 File Offset: 0x00017533
		[NonVersionable]
		public bool Equals(byte obj)
		{
			return this == obj;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A hash code for the current <see cref="T:System.Byte" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000527 RID: 1319 RVA: 0x000194B1 File Offset: 0x000176B1
		public override int GetHashCode()
		{
			return (int)this;
		}

		/// <summary>Converts the string representation of a number to its <see cref="T:System.Byte" /> equivalent.</summary>
		/// <returns>A byte value that is equivalent to the number contained in <paramref name="s" />.</returns>
		/// <param name="s">A string that contains a number to convert. The string is interpreted using the <see cref="F:System.Globalization.NumberStyles.Integer" /> style. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> is not of the correct format. </exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="s" /> represents a number less than <see cref="F:System.Byte.MinValue" /> or greater than <see cref="F:System.Byte.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000528 RID: 1320 RVA: 0x000195B3 File Offset: 0x000177B3
		public static byte Parse(string s)
		{
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			return byte.Parse(s, NumberStyles.Integer, NumberFormatInfo.CurrentInfo);
		}

		/// <summary>Converts the string representation of a number in a specified culture-specific format to its <see cref="T:System.Byte" /> equivalent.</summary>
		/// <returns>A byte value that is equivalent to the number contained in <paramref name="s" />.</returns>
		/// <param name="s">A string that contains a number to convert. The string is interpreted using the <see cref="F:System.Globalization.NumberStyles.Integer" /> style. </param>
		/// <param name="provider">An object that supplies culture-specific parsing information about <paramref name="s" />. If <paramref name="provider" /> is null, the thread current culture is used. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> is not of the correct format. </exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="s" /> represents a number less than <see cref="F:System.Byte.MinValue" /> or greater than <see cref="F:System.Byte.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000529 RID: 1321 RVA: 0x000195D0 File Offset: 0x000177D0
		public static byte Parse(string s, IFormatProvider provider)
		{
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			return byte.Parse(s, NumberStyles.Integer, NumberFormatInfo.GetInstance(provider));
		}

		/// <summary>Converts the string representation of a number in a specified style and culture-specific format to its <see cref="T:System.Byte" /> equivalent.</summary>
		/// <returns>A byte value that is equivalent to the number contained in <paramref name="s" />.</returns>
		/// <param name="s">A string that contains a number to convert. The string is interpreted using the style specified by <paramref name="style" />. </param>
		/// <param name="style">A bitwise combination of enumeration values that indicates the style elements that can be present in <paramref name="s" />. A typical value to specify is <see cref="F:System.Globalization.NumberStyles.Integer" />.</param>
		/// <param name="provider">An object that supplies culture-specific information about the format of <paramref name="s" />. If <paramref name="provider" /> is null, the thread current culture is used. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> is not of the correct format. </exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="s" /> represents a number less than <see cref="F:System.Byte.MinValue" /> or greater than <see cref="F:System.Byte.MaxValue" />. -or-<paramref name="s" /> includes non-zero, fractional digits.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="style" /> is not a <see cref="T:System.Globalization.NumberStyles" /> value. -or-<paramref name="style" /> is not a combination of <see cref="F:System.Globalization.NumberStyles.AllowHexSpecifier" /> and <see cref="F:System.Globalization.NumberStyles.HexNumber" /> values.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600052A RID: 1322 RVA: 0x000195EE File Offset: 0x000177EE
		public static byte Parse(string s, NumberStyles style, IFormatProvider provider)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			return byte.Parse(s, style, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x00019614 File Offset: 0x00017814
		private static byte Parse(ReadOnlySpan<char> s, NumberStyles style, NumberFormatInfo info)
		{
			int num = 0;
			try
			{
				num = Number.ParseInt32(s, style, info);
			}
			catch (OverflowException ex)
			{
				throw new OverflowException("Value was either too large or too small for an unsigned byte.", ex);
			}
			if (num < 0 || num > 255)
			{
				throw new OverflowException("Value was either too large or too small for an unsigned byte.");
			}
			return (byte)num;
		}

		/// <summary>Tries to convert the string representation of a number to its <see cref="T:System.Byte" /> equivalent, and returns a value that indicates whether the conversion succeeded.</summary>
		/// <returns>true if <paramref name="s" /> was converted successfully; otherwise, false. </returns>
		/// <param name="s">A string that contains a number to convert. The string is interpreted using the <see cref="F:System.Globalization.NumberStyles.Integer" /> style.</param>
		/// <param name="result">When this method returns, contains the <see cref="T:System.Byte" /> value equivalent to the number contained in <paramref name="s" /> if the conversion succeeded, or zero if the conversion failed. This parameter is passed uninitialized.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600052C RID: 1324 RVA: 0x00019664 File Offset: 0x00017864
		public static bool TryParse(string s, out byte result)
		{
			if (s == null)
			{
				result = 0;
				return false;
			}
			return byte.TryParse(s, NumberStyles.Integer, NumberFormatInfo.CurrentInfo, out result);
		}

		/// <summary>Converts the string representation of a number in a specified style and culture-specific format to its <see cref="T:System.Byte" /> equivalent. A return value indicates whether the conversion succeeded or failed.</summary>
		/// <returns>true if <paramref name="s" /> was converted successfully; otherwise, false.</returns>
		/// <param name="s">A string containing a number to convert. The string is interpreted using the style specified by <paramref name="style" />. </param>
		/// <param name="style">A bitwise combination of enumeration values that indicates the style elements that can be present in <paramref name="s" />. A typical value to specify is <see cref="F:System.Globalization.NumberStyles.Integer" />.</param>
		/// <param name="provider">An object that supplies culture-specific formatting information about <paramref name="s" />. If <paramref name="provider" /> is null, the thread current culture is used. </param>
		/// <param name="result">When this method returns, contains the 8-bit unsigned integer value equivalent to the number contained in <paramref name="s" /> if the conversion succeeded, or zero if the conversion failed. The conversion fails if the <paramref name="s" /> parameter is null, is not of the correct format, or represents a number less than <see cref="F:System.Byte.MinValue" /> or greater than <see cref="F:System.Byte.MaxValue" />. This parameter is passed uninitialized. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="style" /> is not a <see cref="T:System.Globalization.NumberStyles" /> value. -or-<paramref name="style" /> is not a combination of <see cref="F:System.Globalization.NumberStyles.AllowHexSpecifier" /> and <see cref="F:System.Globalization.NumberStyles.HexNumber" /> values.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600052D RID: 1325 RVA: 0x00019680 File Offset: 0x00017880
		public static bool TryParse(string s, NumberStyles style, IFormatProvider provider, out byte result)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			if (s == null)
			{
				result = 0;
				return false;
			}
			return byte.TryParse(s, style, NumberFormatInfo.GetInstance(provider), out result);
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x000196A4 File Offset: 0x000178A4
		private static bool TryParse(ReadOnlySpan<char> s, NumberStyles style, NumberFormatInfo info, out byte result)
		{
			result = 0;
			int num;
			if (!Number.TryParseInt32(s, style, info, out num))
			{
				return false;
			}
			if (num < 0 || num > 255)
			{
				return false;
			}
			result = (byte)num;
			return true;
		}

		/// <summary>Converts the value of the current <see cref="T:System.Byte" /> object to its equivalent string representation.</summary>
		/// <returns>The string representation of the value of this object, which consists of a sequence of digits that range from 0 to 9 with no leading zeroes.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600052F RID: 1327 RVA: 0x000196D5 File Offset: 0x000178D5
		public override string ToString()
		{
			return Number.FormatInt32((int)this, null, null);
		}

		/// <summary>Converts the value of the current <see cref="T:System.Byte" /> object to its equivalent string representation using the specified format.</summary>
		/// <returns>The string representation of the current <see cref="T:System.Byte" /> object, formatted as specified by the <paramref name="format" /> parameter.</returns>
		/// <param name="format">A numeric format string.</param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="format" /> includes an unsupported specifier. Supported format specifiers are listed in the Remarks section.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000530 RID: 1328 RVA: 0x000196E5 File Offset: 0x000178E5
		public string ToString(string format)
		{
			return Number.FormatInt32((int)this, format, null);
		}

		/// <summary>Converts the numeric value of the current <see cref="T:System.Byte" /> object to its equivalent string representation using the specified culture-specific formatting information.</summary>
		/// <returns>The string representation of the value of this object in the format specified by the <paramref name="provider" /> parameter.</returns>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000531 RID: 1329 RVA: 0x000196F5 File Offset: 0x000178F5
		public string ToString(IFormatProvider provider)
		{
			return Number.FormatInt32((int)this, null, provider);
		}

		/// <summary>Converts the value of the current <see cref="T:System.Byte" /> object to its equivalent string representation using the specified format and culture-specific formatting information.</summary>
		/// <returns>The string representation of the current <see cref="T:System.Byte" /> object, formatted as specified by the <paramref name="format" /> and <paramref name="provider" /> parameters.</returns>
		/// <param name="format">A standard or custom numeric format string.</param>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="format" /> includes an unsupported specifier. Supported format specifiers are listed in the Remarks section.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000532 RID: 1330 RVA: 0x00019705 File Offset: 0x00017905
		public string ToString(string format, IFormatProvider provider)
		{
			return Number.FormatInt32((int)this, format, provider);
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x00019715 File Offset: 0x00017915
		public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format = default(ReadOnlySpan<char>), IFormatProvider provider = null)
		{
			return Number.TryFormatInt32((int)this, format, provider, destination, out charsWritten);
		}

		/// <summary>Returns the <see cref="T:System.TypeCode" /> for value type <see cref="T:System.Byte" />.</summary>
		/// <returns>The enumerated constant, <see cref="F:System.TypeCode.Byte" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000534 RID: 1332 RVA: 0x00019723 File Offset: 0x00017923
		public TypeCode GetTypeCode()
		{
			return TypeCode.Byte;
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToBoolean(System.IFormatProvider)" />. </summary>
		/// <returns>true if the value of the current instance is not zero; otherwise, false.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x06000535 RID: 1333 RVA: 0x00019726 File Offset: 0x00017926
		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			return Convert.ToBoolean(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToChar(System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.Char" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x06000536 RID: 1334 RVA: 0x0001972F File Offset: 0x0001792F
		char IConvertible.ToChar(IFormatProvider provider)
		{
			return Convert.ToChar(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToSByte(System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, converted to an <see cref="T:System.SByte" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x06000537 RID: 1335 RVA: 0x00019738 File Offset: 0x00017938
		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			return Convert.ToSByte(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToByte(System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, unchanged.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x06000538 RID: 1336 RVA: 0x000194B1 File Offset: 0x000176B1
		byte IConvertible.ToByte(IFormatProvider provider)
		{
			return this;
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToInt16(System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, converted to an <see cref="T:System.Int16" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x06000539 RID: 1337 RVA: 0x00019741 File Offset: 0x00017941
		short IConvertible.ToInt16(IFormatProvider provider)
		{
			return Convert.ToInt16(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToUInt16(System.IFormatProvider)" />.</summary>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.UInt16" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x0600053A RID: 1338 RVA: 0x0001974A File Offset: 0x0001794A
		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			return Convert.ToUInt16(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToInt32(System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, converted to an <see cref="T:System.Int32" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x0600053B RID: 1339 RVA: 0x00019753 File Offset: 0x00017953
		int IConvertible.ToInt32(IFormatProvider provider)
		{
			return Convert.ToInt32(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToUInt32(System.IFormatProvider)" />.</summary>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.UInt32" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x0600053C RID: 1340 RVA: 0x0001975C File Offset: 0x0001795C
		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			return Convert.ToUInt32(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToInt64(System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, converted to an <see cref="T:System.Int64" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x0600053D RID: 1341 RVA: 0x00019765 File Offset: 0x00017965
		long IConvertible.ToInt64(IFormatProvider provider)
		{
			return Convert.ToInt64(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToUInt64(System.IFormatProvider)" />.</summary>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.UInt64" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x0600053E RID: 1342 RVA: 0x0001976E File Offset: 0x0001796E
		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			return Convert.ToUInt64(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToSingle(System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.Single" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x0600053F RID: 1343 RVA: 0x00019777 File Offset: 0x00017977
		float IConvertible.ToSingle(IFormatProvider provider)
		{
			return Convert.ToSingle(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToDouble(System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.Double" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x06000540 RID: 1344 RVA: 0x00019780 File Offset: 0x00017980
		double IConvertible.ToDouble(IFormatProvider provider)
		{
			return Convert.ToDouble(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToDecimal(System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.Decimal" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x06000541 RID: 1345 RVA: 0x00019789 File Offset: 0x00017989
		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			return Convert.ToDecimal(this);
		}

		/// <summary>This conversion is not supported. Attempting to use this method throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		/// <exception cref="T:System.InvalidCastException">In all cases.</exception>
		// Token: 0x06000542 RID: 1346 RVA: 0x00019792 File Offset: 0x00017992
		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			throw new InvalidCastException(SR.Format("Invalid cast from '{0}' to '{1}'.", "Byte", "DateTime"));
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToType(System.Type,System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, converted to <paramref name="type" />.</returns>
		/// <param name="type">The type to which to convert this <see cref="T:System.Byte" /> value. </param>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> implementation that supplies information about the format of the returned value.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is null. </exception>
		/// <exception cref="T:System.InvalidCastException">The requested type conversion is not supported. </exception>
		// Token: 0x06000543 RID: 1347 RVA: 0x000197AD File Offset: 0x000179AD
		object IConvertible.ToType(Type type, IFormatProvider provider)
		{
			return Convert.DefaultToType(this, type, provider);
		}

		// Token: 0x040002DF RID: 735
		private readonly byte m_value;
	}
}
