using System;

namespace System.Xml.Serialization
{
	/// <summary>Specifies various options to use when generating .NET Framework types for use with an XML Web Service.</summary>
	// Token: 0x02000144 RID: 324
	[Flags]
	public enum CodeGenerationOptions
	{
		/// <summary>Represents primitive types by fields and primitive types by <see cref="N:System" /> namespace types.</summary>
		// Token: 0x040007C1 RID: 1985
		[XmlIgnore]
		None = 0,
		/// <summary>Represents primitive types by properties.</summary>
		// Token: 0x040007C2 RID: 1986
		[XmlEnum("properties")]
		GenerateProperties = 1,
		/// <summary>Creates events for the asynchronous invocation of Web methods.</summary>
		// Token: 0x040007C3 RID: 1987
		[XmlEnum("newAsync")]
		GenerateNewAsync = 2,
		/// <summary>Creates Begin and End methods for the asynchronous invocation of Web methods.</summary>
		// Token: 0x040007C4 RID: 1988
		[XmlEnum("oldAsync")]
		GenerateOldAsync = 4,
		/// <summary>Generates explicitly ordered serialization code as specified through the Order property of the <see cref="T:System.Xml.Serialization.XmlAnyElementAttribute" />, <see cref="T:System.Xml.Serialization.XmlArrayAttribute" />, and <see cref="T:System.Xml.Serialization.XmlElementAttribute" /> attributes. </summary>
		// Token: 0x040007C5 RID: 1989
		[XmlEnum("order")]
		GenerateOrder = 8,
		/// <summary>Enables data binding.</summary>
		// Token: 0x040007C6 RID: 1990
		[XmlEnum("enableDataBinding")]
		EnableDataBinding = 16
	}
}
