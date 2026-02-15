using System;

namespace System
{
	/// <summary>Specifies the type of an object.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200015A RID: 346
	public enum TypeCode
	{
		/// <summary>A null reference.</summary>
		// Token: 0x040004AB RID: 1195
		Empty,
		/// <summary>A general type representing any reference or value type not explicitly represented by another TypeCode.</summary>
		// Token: 0x040004AC RID: 1196
		Object,
		/// <summary>A database null (column) value.</summary>
		// Token: 0x040004AD RID: 1197
		DBNull,
		/// <summary>A simple type representing Boolean values of true or false.</summary>
		// Token: 0x040004AE RID: 1198
		Boolean,
		/// <summary>An integral type representing unsigned 16-bit integers with values between 0 and 65535. The set of possible values for the <see cref="F:System.TypeCode.Char" /> type corresponds to the Unicode character set.</summary>
		// Token: 0x040004AF RID: 1199
		Char,
		/// <summary>An integral type representing signed 8-bit integers with values between -128 and 127.</summary>
		// Token: 0x040004B0 RID: 1200
		SByte,
		/// <summary>An integral type representing unsigned 8-bit integers with values between 0 and 255.</summary>
		// Token: 0x040004B1 RID: 1201
		Byte,
		/// <summary>An integral type representing signed 16-bit integers with values between -32768 and 32767.</summary>
		// Token: 0x040004B2 RID: 1202
		Int16,
		/// <summary>An integral type representing unsigned 16-bit integers with values between 0 and 65535.</summary>
		// Token: 0x040004B3 RID: 1203
		UInt16,
		/// <summary>An integral type representing signed 32-bit integers with values between -2147483648 and 2147483647.</summary>
		// Token: 0x040004B4 RID: 1204
		Int32,
		/// <summary>An integral type representing unsigned 32-bit integers with values between 0 and 4294967295.</summary>
		// Token: 0x040004B5 RID: 1205
		UInt32,
		/// <summary>An integral type representing signed 64-bit integers with values between -9223372036854775808 and 9223372036854775807.</summary>
		// Token: 0x040004B6 RID: 1206
		Int64,
		/// <summary>An integral type representing unsigned 64-bit integers with values between 0 and 18446744073709551615.</summary>
		// Token: 0x040004B7 RID: 1207
		UInt64,
		/// <summary>A floating point type representing values ranging from approximately 1.5 x 10 -45 to 3.4 x 10 38 with a precision of 7 digits.</summary>
		// Token: 0x040004B8 RID: 1208
		Single,
		/// <summary>A floating point type representing values ranging from approximately 5.0 x 10 -324 to 1.7 x 10 308 with a precision of 15-16 digits.</summary>
		// Token: 0x040004B9 RID: 1209
		Double,
		/// <summary>A simple type representing values ranging from 1.0 x 10 -28 to approximately 7.9 x 10 28 with 28-29 significant digits.</summary>
		// Token: 0x040004BA RID: 1210
		Decimal,
		/// <summary>A type representing a date and time value.</summary>
		// Token: 0x040004BB RID: 1211
		DateTime,
		/// <summary>A sealed class type representing Unicode character strings.</summary>
		// Token: 0x040004BC RID: 1212
		String = 18
	}
}
