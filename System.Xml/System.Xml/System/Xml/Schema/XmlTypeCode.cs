using System;

namespace System.Xml.Schema
{
	/// <summary>Represents the W3C XML Schema Definition Language (XSD) schema types.</summary>
	// Token: 0x02000310 RID: 784
	public enum XmlTypeCode
	{
		/// <summary>No type information.</summary>
		// Token: 0x04001056 RID: 4182
		None,
		/// <summary>An item such as a node or atomic value.</summary>
		// Token: 0x04001057 RID: 4183
		Item,
		/// <summary>This value supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		// Token: 0x04001058 RID: 4184
		Node,
		/// <summary>This value supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		// Token: 0x04001059 RID: 4185
		Document,
		/// <summary>This value supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		// Token: 0x0400105A RID: 4186
		Element,
		/// <summary>This value supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		// Token: 0x0400105B RID: 4187
		Attribute,
		/// <summary>This value supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		// Token: 0x0400105C RID: 4188
		Namespace,
		/// <summary>This value supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		// Token: 0x0400105D RID: 4189
		ProcessingInstruction,
		/// <summary>This value supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		// Token: 0x0400105E RID: 4190
		Comment,
		/// <summary>This value supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		// Token: 0x0400105F RID: 4191
		Text,
		/// <summary>Any atomic value of a union.</summary>
		// Token: 0x04001060 RID: 4192
		AnyAtomicType,
		/// <summary>An untyped atomic value.</summary>
		// Token: 0x04001061 RID: 4193
		UntypedAtomic,
		/// <summary>A W3C XML Schema xs:string type.</summary>
		// Token: 0x04001062 RID: 4194
		String,
		/// <summary>A W3C XML Schema xs:boolean type.</summary>
		// Token: 0x04001063 RID: 4195
		Boolean,
		/// <summary>A W3C XML Schema xs:decimal type.</summary>
		// Token: 0x04001064 RID: 4196
		Decimal,
		/// <summary>A W3C XML Schema xs:float type.</summary>
		// Token: 0x04001065 RID: 4197
		Float,
		/// <summary>A W3C XML Schema xs:double type.</summary>
		// Token: 0x04001066 RID: 4198
		Double,
		/// <summary>A W3C XML Schema xs:Duration type.</summary>
		// Token: 0x04001067 RID: 4199
		Duration,
		/// <summary>A W3C XML Schema xs:dateTime type.</summary>
		// Token: 0x04001068 RID: 4200
		DateTime,
		/// <summary>A W3C XML Schema xs:time type.</summary>
		// Token: 0x04001069 RID: 4201
		Time,
		/// <summary>A W3C XML Schema xs:date type.</summary>
		// Token: 0x0400106A RID: 4202
		Date,
		/// <summary>A W3C XML Schema xs:gYearMonth type.</summary>
		// Token: 0x0400106B RID: 4203
		GYearMonth,
		/// <summary>A W3C XML Schema xs:gYear type.</summary>
		// Token: 0x0400106C RID: 4204
		GYear,
		/// <summary>A W3C XML Schema xs:gMonthDay type.</summary>
		// Token: 0x0400106D RID: 4205
		GMonthDay,
		/// <summary>A W3C XML Schema xs:gDay type.</summary>
		// Token: 0x0400106E RID: 4206
		GDay,
		/// <summary>A W3C XML Schema xs:gMonth type.</summary>
		// Token: 0x0400106F RID: 4207
		GMonth,
		/// <summary>A W3C XML Schema xs:hexBinary type.</summary>
		// Token: 0x04001070 RID: 4208
		HexBinary,
		/// <summary>A W3C XML Schema xs:base64Binary type.</summary>
		// Token: 0x04001071 RID: 4209
		Base64Binary,
		/// <summary>A W3C XML Schema xs:anyURI type.</summary>
		// Token: 0x04001072 RID: 4210
		AnyUri,
		/// <summary>A W3C XML Schema xs:QName type.</summary>
		// Token: 0x04001073 RID: 4211
		QName,
		/// <summary>A W3C XML Schema xs:NOTATION type.</summary>
		// Token: 0x04001074 RID: 4212
		Notation,
		/// <summary>A W3C XML Schema xs:normalizedString type.</summary>
		// Token: 0x04001075 RID: 4213
		NormalizedString,
		/// <summary>A W3C XML Schema xs:token type.</summary>
		// Token: 0x04001076 RID: 4214
		Token,
		/// <summary>A W3C XML Schema xs:language type.</summary>
		// Token: 0x04001077 RID: 4215
		Language,
		/// <summary>A W3C XML Schema xs:NMTOKEN type.</summary>
		// Token: 0x04001078 RID: 4216
		NmToken,
		/// <summary>A W3C XML Schema xs:Name type.</summary>
		// Token: 0x04001079 RID: 4217
		Name,
		/// <summary>A W3C XML Schema xs:NCName type.</summary>
		// Token: 0x0400107A RID: 4218
		NCName,
		/// <summary>A W3C XML Schema xs:ID type.</summary>
		// Token: 0x0400107B RID: 4219
		Id,
		/// <summary>A W3C XML Schema xs:IDREF type.</summary>
		// Token: 0x0400107C RID: 4220
		Idref,
		/// <summary>A W3C XML Schema xs:ENTITY type.</summary>
		// Token: 0x0400107D RID: 4221
		Entity,
		/// <summary>A W3C XML Schema xs:integer type.</summary>
		// Token: 0x0400107E RID: 4222
		Integer,
		/// <summary>A W3C XML Schema xs:nonPositiveInteger type.</summary>
		// Token: 0x0400107F RID: 4223
		NonPositiveInteger,
		/// <summary>A W3C XML Schema xs:negativeInteger type.</summary>
		// Token: 0x04001080 RID: 4224
		NegativeInteger,
		/// <summary>A W3C XML Schema xs:long type.</summary>
		// Token: 0x04001081 RID: 4225
		Long,
		/// <summary>A W3C XML Schema xs:int type.</summary>
		// Token: 0x04001082 RID: 4226
		Int,
		/// <summary>A W3C XML Schema xs:short type.</summary>
		// Token: 0x04001083 RID: 4227
		Short,
		/// <summary>A W3C XML Schema xs:byte type.</summary>
		// Token: 0x04001084 RID: 4228
		Byte,
		/// <summary>A W3C XML Schema xs:nonNegativeInteger type.</summary>
		// Token: 0x04001085 RID: 4229
		NonNegativeInteger,
		/// <summary>A W3C XML Schema xs:unsignedLong type.</summary>
		// Token: 0x04001086 RID: 4230
		UnsignedLong,
		/// <summary>A W3C XML Schema xs:unsignedInt type.</summary>
		// Token: 0x04001087 RID: 4231
		UnsignedInt,
		/// <summary>A W3C XML Schema xs:unsignedShort type.</summary>
		// Token: 0x04001088 RID: 4232
		UnsignedShort,
		/// <summary>A W3C XML Schema xs:unsignedByte type.</summary>
		// Token: 0x04001089 RID: 4233
		UnsignedByte,
		/// <summary>A W3C XML Schema xs:positiveInteger type.</summary>
		// Token: 0x0400108A RID: 4234
		PositiveInteger,
		/// <summary>This value supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		// Token: 0x0400108B RID: 4235
		YearMonthDuration,
		/// <summary>This value supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		// Token: 0x0400108C RID: 4236
		DayTimeDuration
	}
}
