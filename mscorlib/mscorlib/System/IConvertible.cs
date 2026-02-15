using System;

namespace System
{
	/// <summary>Defines methods that convert the value of the implementing reference or value type to a common language runtime type that has an equivalent value.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000102 RID: 258
	[CLSCompliant(false)]
	public interface IConvertible
	{
		/// <summary>Returns the <see cref="T:System.TypeCode" /> for this instance.</summary>
		/// <returns>The enumerated constant that is the <see cref="T:System.TypeCode" /> of the class or value type that implements this interface.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000886 RID: 2182
		TypeCode GetTypeCode();

		/// <summary>Converts the value of this instance to an equivalent Boolean value using the specified culture-specific formatting information.</summary>
		/// <returns>A Boolean value equivalent to the value of this instance.</returns>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> interface implementation that supplies culture-specific formatting information. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000887 RID: 2183
		bool ToBoolean(IFormatProvider provider);

		/// <summary>Converts the value of this instance to an equivalent Unicode character using the specified culture-specific formatting information.</summary>
		/// <returns>A Unicode character equivalent to the value of this instance.</returns>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> interface implementation that supplies culture-specific formatting information. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000888 RID: 2184
		char ToChar(IFormatProvider provider);

		/// <summary>Converts the value of this instance to an equivalent 8-bit signed integer using the specified culture-specific formatting information.</summary>
		/// <returns>An 8-bit signed integer equivalent to the value of this instance.</returns>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> interface implementation that supplies culture-specific formatting information. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000889 RID: 2185
		sbyte ToSByte(IFormatProvider provider);

		/// <summary>Converts the value of this instance to an equivalent 8-bit unsigned integer using the specified culture-specific formatting information.</summary>
		/// <returns>An 8-bit unsigned integer equivalent to the value of this instance.</returns>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> interface implementation that supplies culture-specific formatting information. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600088A RID: 2186
		byte ToByte(IFormatProvider provider);

		/// <summary>Converts the value of this instance to an equivalent 16-bit signed integer using the specified culture-specific formatting information.</summary>
		/// <returns>An 16-bit signed integer equivalent to the value of this instance.</returns>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> interface implementation that supplies culture-specific formatting information. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600088B RID: 2187
		short ToInt16(IFormatProvider provider);

		/// <summary>Converts the value of this instance to an equivalent 16-bit unsigned integer using the specified culture-specific formatting information.</summary>
		/// <returns>An 16-bit unsigned integer equivalent to the value of this instance.</returns>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> interface implementation that supplies culture-specific formatting information. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600088C RID: 2188
		ushort ToUInt16(IFormatProvider provider);

		/// <summary>Converts the value of this instance to an equivalent 32-bit signed integer using the specified culture-specific formatting information.</summary>
		/// <returns>An 32-bit signed integer equivalent to the value of this instance.</returns>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> interface implementation that supplies culture-specific formatting information. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600088D RID: 2189
		int ToInt32(IFormatProvider provider);

		/// <summary>Converts the value of this instance to an equivalent 32-bit unsigned integer using the specified culture-specific formatting information.</summary>
		/// <returns>An 32-bit unsigned integer equivalent to the value of this instance.</returns>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> interface implementation that supplies culture-specific formatting information. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600088E RID: 2190
		uint ToUInt32(IFormatProvider provider);

		/// <summary>Converts the value of this instance to an equivalent 64-bit signed integer using the specified culture-specific formatting information.</summary>
		/// <returns>An 64-bit signed integer equivalent to the value of this instance.</returns>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> interface implementation that supplies culture-specific formatting information. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600088F RID: 2191
		long ToInt64(IFormatProvider provider);

		/// <summary>Converts the value of this instance to an equivalent 64-bit unsigned integer using the specified culture-specific formatting information.</summary>
		/// <returns>An 64-bit unsigned integer equivalent to the value of this instance.</returns>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> interface implementation that supplies culture-specific formatting information. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000890 RID: 2192
		ulong ToUInt64(IFormatProvider provider);

		/// <summary>Converts the value of this instance to an equivalent single-precision floating-point number using the specified culture-specific formatting information.</summary>
		/// <returns>A single-precision floating-point number equivalent to the value of this instance.</returns>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> interface implementation that supplies culture-specific formatting information. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000891 RID: 2193
		float ToSingle(IFormatProvider provider);

		/// <summary>Converts the value of this instance to an equivalent double-precision floating-point number using the specified culture-specific formatting information.</summary>
		/// <returns>A double-precision floating-point number equivalent to the value of this instance.</returns>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> interface implementation that supplies culture-specific formatting information. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000892 RID: 2194
		double ToDouble(IFormatProvider provider);

		/// <summary>Converts the value of this instance to an equivalent <see cref="T:System.Decimal" /> number using the specified culture-specific formatting information.</summary>
		/// <returns>A <see cref="T:System.Decimal" /> number equivalent to the value of this instance.</returns>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> interface implementation that supplies culture-specific formatting information. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000893 RID: 2195
		decimal ToDecimal(IFormatProvider provider);

		/// <summary>Converts the value of this instance to an equivalent <see cref="T:System.DateTime" /> using the specified culture-specific formatting information.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> instance equivalent to the value of this instance.</returns>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> interface implementation that supplies culture-specific formatting information. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000894 RID: 2196
		DateTime ToDateTime(IFormatProvider provider);

		/// <summary>Converts the value of this instance to an equivalent <see cref="T:System.String" /> using the specified culture-specific formatting information.</summary>
		/// <returns>A <see cref="T:System.String" /> instance equivalent to the value of this instance.</returns>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> interface implementation that supplies culture-specific formatting information. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000895 RID: 2197
		string ToString(IFormatProvider provider);

		/// <summary>Converts the value of this instance to an <see cref="T:System.Object" /> of the specified <see cref="T:System.Type" /> that has an equivalent value, using the specified culture-specific formatting information.</summary>
		/// <returns>An <see cref="T:System.Object" /> instance of type <paramref name="conversionType" /> whose value is equivalent to the value of this instance.</returns>
		/// <param name="conversionType">The <see cref="T:System.Type" /> to which the value of this instance is converted. </param>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> interface implementation that supplies culture-specific formatting information. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000896 RID: 2198
		object ToType(Type conversionType, IFormatProvider provider);
	}
}
