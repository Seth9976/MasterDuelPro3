using System;

namespace System.Data
{
	/// <summary>Specifies the data type of a field, a property, or a Parameter object of a .NET Framework data provider.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200005E RID: 94
	public enum DbType
	{
		/// <summary>A variable-length stream of non-Unicode characters ranging between 1 and 8,000 characters.</summary>
		// Token: 0x040001CE RID: 462
		AnsiString,
		/// <summary>A variable-length stream of binary data ranging between 1 and 8,000 bytes.</summary>
		// Token: 0x040001CF RID: 463
		Binary,
		/// <summary>An 8-bit unsigned integer ranging in value from 0 to 255.</summary>
		// Token: 0x040001D0 RID: 464
		Byte,
		/// <summary>A simple type representing Boolean values of true or false.</summary>
		// Token: 0x040001D1 RID: 465
		Boolean,
		/// <summary>A currency value ranging from -2 63 (or -922,337,203,685,477.5808) to 2 63 -1 (or +922,337,203,685,477.5807) with an accuracy to a ten-thousandth of a currency unit.</summary>
		// Token: 0x040001D2 RID: 466
		Currency,
		/// <summary>A type representing a date value.</summary>
		// Token: 0x040001D3 RID: 467
		Date,
		/// <summary>A type representing a date and time value.</summary>
		// Token: 0x040001D4 RID: 468
		DateTime,
		/// <summary>A simple type representing values ranging from 1.0 x 10 -28 to approximately 7.9 x 10 28 with 28-29 significant digits.</summary>
		// Token: 0x040001D5 RID: 469
		Decimal,
		/// <summary>A floating point type representing values ranging from approximately 5.0 x 10 -324 to 1.7 x 10 308 with a precision of 15-16 digits.</summary>
		// Token: 0x040001D6 RID: 470
		Double,
		/// <summary>A globally unique identifier (or GUID).</summary>
		// Token: 0x040001D7 RID: 471
		Guid,
		/// <summary>An integral type representing signed 16-bit integers with values between -32768 and 32767.</summary>
		// Token: 0x040001D8 RID: 472
		Int16,
		/// <summary>An integral type representing signed 32-bit integers with values between -2147483648 and 2147483647.</summary>
		// Token: 0x040001D9 RID: 473
		Int32,
		/// <summary>An integral type representing signed 64-bit integers with values between -9223372036854775808 and 9223372036854775807.</summary>
		// Token: 0x040001DA RID: 474
		Int64,
		/// <summary>A general type representing any reference or value type not explicitly represented by another DbType value.</summary>
		// Token: 0x040001DB RID: 475
		Object,
		/// <summary>An integral type representing signed 8-bit integers with values between -128 and 127.</summary>
		// Token: 0x040001DC RID: 476
		SByte,
		/// <summary>A floating point type representing values ranging from approximately 1.5 x 10 -45 to 3.4 x 10 38 with a precision of 7 digits.</summary>
		// Token: 0x040001DD RID: 477
		Single,
		/// <summary>A type representing Unicode character strings.</summary>
		// Token: 0x040001DE RID: 478
		String,
		/// <summary>A type representing a SQL Server DateTime value. If you want to use a SQL Server time value, use <see cref="F:System.Data.SqlDbType.Time" />.</summary>
		// Token: 0x040001DF RID: 479
		Time,
		/// <summary>An integral type representing unsigned 16-bit integers with values between 0 and 65535.</summary>
		// Token: 0x040001E0 RID: 480
		UInt16,
		/// <summary>An integral type representing unsigned 32-bit integers with values between 0 and 4294967295.</summary>
		// Token: 0x040001E1 RID: 481
		UInt32,
		/// <summary>An integral type representing unsigned 64-bit integers with values between 0 and 18446744073709551615.</summary>
		// Token: 0x040001E2 RID: 482
		UInt64,
		/// <summary>A variable-length numeric value.</summary>
		// Token: 0x040001E3 RID: 483
		VarNumeric,
		/// <summary>A fixed-length stream of non-Unicode characters.</summary>
		// Token: 0x040001E4 RID: 484
		AnsiStringFixedLength,
		/// <summary>A fixed-length string of Unicode characters.</summary>
		// Token: 0x040001E5 RID: 485
		StringFixedLength,
		/// <summary>A parsed representation of an XML document or fragment.</summary>
		// Token: 0x040001E6 RID: 486
		Xml = 25,
		/// <summary>Date and time data. Date value range is from January 1,1 AD through December 31, 9999 AD. Time value range is 00:00:00 through 23:59:59.9999999 with an accuracy of 100 nanoseconds.</summary>
		// Token: 0x040001E7 RID: 487
		DateTime2,
		/// <summary>Date and time data with time zone awareness. Date value range is from January 1,1 AD through December 31, 9999 AD. Time value range is 00:00:00 through 23:59:59.9999999 with an accuracy of 100 nanoseconds. Time zone value range is -14:00 through +14:00. </summary>
		// Token: 0x040001E8 RID: 488
		DateTimeOffset
	}
}
