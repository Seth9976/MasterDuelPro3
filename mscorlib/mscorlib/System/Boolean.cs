using System;
using System.Runtime.Versioning;

namespace System
{
	/// <summary>Represents a Boolean value.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020000CB RID: 203
	[Serializable]
	public readonly struct Boolean : IComparable, IConvertible, IComparable<bool>, IEquatable<bool>
	{
		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A hash code for the current <see cref="T:System.Boolean" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000506 RID: 1286 RVA: 0x000192FB File Offset: 0x000174FB
		public override int GetHashCode()
		{
			if (!this)
			{
				return 0;
			}
			return 1;
		}

		/// <summary>Converts the value of this instance to its equivalent string representation (either "True" or "False").</summary>
		/// <returns>
		///   <see cref="F:System.Boolean.TrueString" /> if the value of this instance is true, or <see cref="F:System.Boolean.FalseString" /> if the value of this instance is false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000507 RID: 1287 RVA: 0x00019304 File Offset: 0x00017504
		public override string ToString()
		{
			if (!this)
			{
				return "False";
			}
			return "True";
		}

		/// <summary>Converts the value of this instance to its equivalent string representation (either "True" or "False").</summary>
		/// <returns>
		///   <see cref="F:System.Boolean.TrueString" /> if the value of this instance is true, or <see cref="F:System.Boolean.FalseString" /> if the value of this instance is false.</returns>
		/// <param name="provider">(Reserved) An <see cref="T:System.IFormatProvider" /> object. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000508 RID: 1288 RVA: 0x00019315 File Offset: 0x00017515
		public string ToString(IFormatProvider provider)
		{
			return this.ToString();
		}

		/// <summary>Returns a value indicating whether this instance is equal to a specified object.</summary>
		/// <returns>true if <paramref name="obj" /> is a <see cref="T:System.Boolean" /> and has the same value as this instance; otherwise, false.</returns>
		/// <param name="obj">An object to compare to this instance. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000509 RID: 1289 RVA: 0x0001931D File Offset: 0x0001751D
		public override bool Equals(object obj)
		{
			return obj is bool && this == (bool)obj;
		}

		/// <summary>Returns a value indicating whether this instance is equal to a specified <see cref="T:System.Boolean" /> object.</summary>
		/// <returns>true if <paramref name="obj" /> has the same value as this instance; otherwise, false.</returns>
		/// <param name="obj">A <see cref="T:System.Boolean" /> value to compare to this instance.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600050A RID: 1290 RVA: 0x00019333 File Offset: 0x00017533
		[NonVersionable]
		public bool Equals(bool obj)
		{
			return this == obj;
		}

		/// <summary>Compares this instance to a specified object and returns an integer that indicates their relationship to one another.</summary>
		/// <returns>A signed integer that indicates the relative order of this instance and <paramref name="obj" />.Return Value Condition Less than zero This instance is false and <paramref name="obj" /> is true. Zero This instance and <paramref name="obj" /> are equal (either both are true or both are false). Greater than zero This instance is true and <paramref name="obj" /> is false.-or- <paramref name="obj" /> is null. </returns>
		/// <param name="obj">An object to compare to this instance, or null. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="obj" /> is not a <see cref="T:System.Boolean" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600050B RID: 1291 RVA: 0x0001933A File Offset: 0x0001753A
		public int CompareTo(object obj)
		{
			if (obj == null)
			{
				return 1;
			}
			if (!(obj is bool))
			{
				throw new ArgumentException("Object must be of type Boolean.");
			}
			if (this == (bool)obj)
			{
				return 0;
			}
			if (!this)
			{
				return -1;
			}
			return 1;
		}

		/// <summary>Compares this instance to a specified <see cref="T:System.Boolean" /> object and returns an integer that indicates their relationship to one another.</summary>
		/// <returns>A signed integer that indicates the relative values of this instance and <paramref name="value" />.Return Value Condition Less than zero This instance is false and <paramref name="value" /> is true. Zero This instance and <paramref name="value" /> are equal (either both are true or both are false). Greater than zero This instance is true and <paramref name="value" /> is false. </returns>
		/// <param name="value">A <see cref="T:System.Boolean" /> object to compare to this instance. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600050C RID: 1292 RVA: 0x00019367 File Offset: 0x00017567
		public int CompareTo(bool value)
		{
			if (this == value)
			{
				return 0;
			}
			if (!this)
			{
				return -1;
			}
			return 1;
		}

		/// <summary>Converts the specified string representation of a logical value to its <see cref="T:System.Boolean" /> equivalent, or throws an exception if the string is not equal to the value of <see cref="F:System.Boolean.TrueString" /> or <see cref="F:System.Boolean.FalseString" />.</summary>
		/// <returns>true if <paramref name="value" /> is equal to the value of the <see cref="F:System.Boolean.TrueString" /> field; false if <paramref name="value" /> is equal to the value of the <see cref="F:System.Boolean.FalseString" /> field.</returns>
		/// <param name="value">A string containing the value to convert. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is not equal to the value of the <see cref="F:System.Boolean.TrueString" /> or <see cref="F:System.Boolean.FalseString" /> field. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600050D RID: 1293 RVA: 0x00019377 File Offset: 0x00017577
		public static bool Parse(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return bool.Parse(value.AsSpan());
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x00019394 File Offset: 0x00017594
		public static bool Parse(ReadOnlySpan<char> value)
		{
			bool flag;
			if (!bool.TryParse(value, out flag))
			{
				throw new FormatException("String was not recognized as a valid Boolean.");
			}
			return flag;
		}

		/// <summary>Tries to convert the specified string representation of a logical value to its <see cref="T:System.Boolean" /> equivalent. A return value indicates whether the conversion succeeded or failed.</summary>
		/// <returns>true if <paramref name="value" /> was converted successfully; otherwise, false.</returns>
		/// <param name="value">A string containing the value to convert. </param>
		/// <param name="result">When this method returns, if the conversion succeeded, contains true if <paramref name="value" /> is equal to <see cref="F:System.Boolean.TrueString" /> or false if <paramref name="value" /> is equal to <see cref="F:System.Boolean.FalseString" />. If the conversion failed, contains false. The conversion fails if <paramref name="value" /> is null or is not equal to the value of either the <see cref="F:System.Boolean.TrueString" /> or <see cref="F:System.Boolean.FalseString" /> field.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600050F RID: 1295 RVA: 0x000193B7 File Offset: 0x000175B7
		public static bool TryParse(string value, out bool result)
		{
			if (value == null)
			{
				result = false;
				return false;
			}
			return bool.TryParse(value.AsSpan(), out result);
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x000193D0 File Offset: 0x000175D0
		public static bool TryParse(ReadOnlySpan<char> value, out bool result)
		{
			ReadOnlySpan<char> readOnlySpan = "True".AsSpan();
			if (readOnlySpan.EqualsOrdinalIgnoreCase(value))
			{
				result = true;
				return true;
			}
			ReadOnlySpan<char> readOnlySpan2 = "False".AsSpan();
			if (readOnlySpan2.EqualsOrdinalIgnoreCase(value))
			{
				result = false;
				return true;
			}
			value = bool.TrimWhiteSpaceAndNull(value);
			if (readOnlySpan.EqualsOrdinalIgnoreCase(value))
			{
				result = true;
				return true;
			}
			if (readOnlySpan2.EqualsOrdinalIgnoreCase(value))
			{
				result = false;
				return true;
			}
			result = false;
			return false;
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x00019438 File Offset: 0x00017638
		private unsafe static ReadOnlySpan<char> TrimWhiteSpaceAndNull(ReadOnlySpan<char> value)
		{
			int num = 0;
			while (num < value.Length && (char.IsWhiteSpace((char)(*value[num])) || *value[num] == 0))
			{
				num++;
			}
			int num2 = value.Length - 1;
			while (num2 >= num && (char.IsWhiteSpace((char)(*value[num2])) || *value[num2] == 0))
			{
				num2--;
			}
			return value.Slice(num, num2 - num + 1);
		}

		/// <summary>Returns the <see cref="T:System.TypeCode" /> for value type <see cref="T:System.Boolean" />.</summary>
		/// <returns>The enumerated constant, <see cref="F:System.TypeCode.Boolean" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000512 RID: 1298 RVA: 0x000194AE File Offset: 0x000176AE
		public TypeCode GetTypeCode()
		{
			return TypeCode.Boolean;
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToBoolean(System.IFormatProvider)" />. </summary>
		/// <returns>true or false.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x06000513 RID: 1299 RVA: 0x000194B1 File Offset: 0x000176B1
		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			return this;
		}

		/// <summary>This conversion is not supported. Attempting to use this method throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		/// <exception cref="T:System.InvalidCastException">You attempt to convert a <see cref="T:System.Boolean" /> value to a <see cref="T:System.Char" /> value. This conversion is not supported.</exception>
		// Token: 0x06000514 RID: 1300 RVA: 0x000194B5 File Offset: 0x000176B5
		char IConvertible.ToChar(IFormatProvider provider)
		{
			throw new InvalidCastException(SR.Format("Invalid cast from '{0}' to '{1}'.", "Boolean", "Char"));
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToSByte(System.IFormatProvider)" />. </summary>
		/// <returns>1 if this instance is true; otherwise, 0.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x06000515 RID: 1301 RVA: 0x000194D0 File Offset: 0x000176D0
		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			return Convert.ToSByte(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToByte(System.IFormatProvider)" />. </summary>
		/// <returns>1 if the value of this instance is true; otherwise, 0. </returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x06000516 RID: 1302 RVA: 0x000194D9 File Offset: 0x000176D9
		byte IConvertible.ToByte(IFormatProvider provider)
		{
			return Convert.ToByte(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToInt16(System.IFormatProvider)" />. </summary>
		/// <returns>1 if this instance is true; otherwise, 0.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x06000517 RID: 1303 RVA: 0x000194E2 File Offset: 0x000176E2
		short IConvertible.ToInt16(IFormatProvider provider)
		{
			return Convert.ToInt16(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToUInt16(System.IFormatProvider)" />. </summary>
		/// <returns>1 if this instance is true; otherwise, 0.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x06000518 RID: 1304 RVA: 0x000194EB File Offset: 0x000176EB
		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			return Convert.ToUInt16(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToInt32(System.IFormatProvider)" />. </summary>
		/// <returns>1 if this instance is true; otherwise, 0.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x06000519 RID: 1305 RVA: 0x000194F4 File Offset: 0x000176F4
		int IConvertible.ToInt32(IFormatProvider provider)
		{
			return Convert.ToInt32(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToUInt32(System.IFormatProvider)" />. </summary>
		/// <returns>1 if this instance is true; otherwise, 0.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x0600051A RID: 1306 RVA: 0x000194FD File Offset: 0x000176FD
		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			return Convert.ToUInt32(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToInt64(System.IFormatProvider)" />. </summary>
		/// <returns>1 if this instance is true; otherwise, 0.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x0600051B RID: 1307 RVA: 0x00019506 File Offset: 0x00017706
		long IConvertible.ToInt64(IFormatProvider provider)
		{
			return Convert.ToInt64(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToUInt64(System.IFormatProvider)" />. </summary>
		/// <returns>1 if this instance is true; otherwise, 0.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x0600051C RID: 1308 RVA: 0x0001950F File Offset: 0x0001770F
		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			return Convert.ToUInt64(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToSingle(System.IFormatProvider)" />..</summary>
		/// <returns>1 if this instance is true; otherwise, 0.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x0600051D RID: 1309 RVA: 0x00019518 File Offset: 0x00017718
		float IConvertible.ToSingle(IFormatProvider provider)
		{
			return Convert.ToSingle(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToDouble(System.IFormatProvider)" />..</summary>
		/// <returns>1 if this instance is true; otherwise, 0.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x0600051E RID: 1310 RVA: 0x00019521 File Offset: 0x00017721
		double IConvertible.ToDouble(IFormatProvider provider)
		{
			return Convert.ToDouble(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToDecimal(System.IFormatProvider)" />..</summary>
		/// <returns>1 if this instance is true; otherwise, 0.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x0600051F RID: 1311 RVA: 0x0001952A File Offset: 0x0001772A
		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			return Convert.ToDecimal(this);
		}

		/// <summary>This conversion is not supported. Attempting to use this method throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		/// <exception cref="T:System.InvalidCastException">You attempt to convert a <see cref="T:System.Boolean" /> value to a <see cref="T:System.DateTime" /> value. This conversion is not supported.</exception>
		// Token: 0x06000520 RID: 1312 RVA: 0x00019533 File Offset: 0x00017733
		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			throw new InvalidCastException(SR.Format("Invalid cast from '{0}' to '{1}'.", "Boolean", "DateTime"));
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToType(System.Type,System.IFormatProvider)" />. </summary>
		/// <returns>An object of the specified type, with a value that is equivalent to the value of this Boolean object.</returns>
		/// <param name="type">The desired type. </param>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> implementation that supplies culture-specific information about the format of the returned value.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is null. </exception>
		/// <exception cref="T:System.InvalidCastException">The requested type conversion is not supported. </exception>
		// Token: 0x06000521 RID: 1313 RVA: 0x0001954E File Offset: 0x0001774E
		object IConvertible.ToType(Type type, IFormatProvider provider)
		{
			return Convert.DefaultToType(this, type, provider);
		}

		// Token: 0x040002DC RID: 732
		private readonly bool m_value;

		/// <summary>Represents the Boolean value true as a string. This field is read-only.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x040002DD RID: 733
		public static readonly string TrueString = "True";

		/// <summary>Represents the Boolean value false as a string. This field is read-only.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x040002DE RID: 734
		public static readonly string FalseString = "False";
	}
}
