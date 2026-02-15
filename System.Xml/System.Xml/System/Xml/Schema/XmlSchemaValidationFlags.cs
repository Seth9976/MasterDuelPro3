using System;

namespace System.Xml.Schema
{
	/// <summary>Specifies schema validation options used by the <see cref="T:System.Xml.Schema.XmlSchemaValidator" /> and <see cref="T:System.Xml.XmlReader" /> classes.</summary>
	// Token: 0x0200030A RID: 778
	[Flags]
	public enum XmlSchemaValidationFlags
	{
		/// <summary>Do not process identity constraints, inline schemas, schema location hints, or report schema validation warnings.</summary>
		// Token: 0x04001009 RID: 4105
		None = 0,
		/// <summary>Process inline schemas encountered during validation.</summary>
		// Token: 0x0400100A RID: 4106
		ProcessInlineSchema = 1,
		/// <summary>Process schema location hints (xsi:schemaLocation, xsi:noNamespaceSchemaLocation) encountered during validation.</summary>
		// Token: 0x0400100B RID: 4107
		ProcessSchemaLocation = 2,
		/// <summary>Report schema validation warnings encountered during validation.</summary>
		// Token: 0x0400100C RID: 4108
		ReportValidationWarnings = 4,
		/// <summary>Process identity constraints (xs:ID, xs:IDREF, xs:key, xs:keyref, xs:unique) encountered during validation.</summary>
		// Token: 0x0400100D RID: 4109
		ProcessIdentityConstraints = 8,
		/// <summary>Allow xml:* attributes even if they are not defined in the schema. The attributes will be validated based on their data type.</summary>
		// Token: 0x0400100E RID: 4110
		AllowXmlAttributes = 16
	}
}
