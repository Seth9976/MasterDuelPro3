using System;

namespace System.Xml
{
	/// <summary>Specifies the type of validation to perform.</summary>
	// Token: 0x02000045 RID: 69
	public enum ValidationType
	{
		/// <summary>No validation is performed. This setting creates an XML 1.0 compliant non-validating parser.</summary>
		// Token: 0x04000169 RID: 361
		None,
		/// <summary>Validates if DTD or schema information is found.</summary>
		// Token: 0x0400016A RID: 362
		[Obsolete("Validation type should be specified as DTD or Schema.")]
		Auto,
		/// <summary>Validates according to the DTD.</summary>
		// Token: 0x0400016B RID: 363
		DTD,
		/// <summary>Validate according to XML-Data Reduced (XDR) schemas, including inline XDR schemas. XDR schemas are recognized using the x-schema namespace prefix or the <see cref="P:System.Xml.XmlValidatingReader.Schemas" /> property.</summary>
		// Token: 0x0400016C RID: 364
		[Obsolete("XDR Validation through XmlValidatingReader is obsoleted")]
		XDR,
		/// <summary>Validate according to XML Schema definition language (XSD) schemas, including inline XML Schemas. XML Schemas are associated with namespace URIs either by using the schemaLocation attribute or the provided Schemas property.</summary>
		// Token: 0x0400016D RID: 365
		Schema
	}
}
