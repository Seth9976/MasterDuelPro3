using System;

namespace System.Xml.Schema
{
	/// <summary>Defines the post-schema-validation infoset of a validated XML node.</summary>
	// Token: 0x02000284 RID: 644
	public interface IXmlSchemaInfo
	{
		/// <summary>Gets the <see cref="T:System.Xml.Schema.XmlSchemaValidity" /> value of this validated XML node.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSchemaValidity" /> value of this validated XML node.</returns>
		// Token: 0x170006DE RID: 1758
		// (get) Token: 0x06001D1F RID: 7455
		XmlSchemaValidity Validity { get; }

		/// <summary>Gets a value indicating if this validated XML node was set as the result of a default being applied during XML Schema Definition Language (XSD) schema validation.</summary>
		/// <returns>true if this validated XML node was set as the result of a default being applied during schema validation; otherwise, false.</returns>
		// Token: 0x170006DF RID: 1759
		// (get) Token: 0x06001D20 RID: 7456
		bool IsDefault { get; }

		/// <summary>Gets a value indicating if the value for this validated XML node is nil.</summary>
		/// <returns>true if the value for this validated XML node is nil; otherwise, false.</returns>
		// Token: 0x170006E0 RID: 1760
		// (get) Token: 0x06001D21 RID: 7457
		bool IsNil { get; }

		/// <summary>Gets the dynamic schema type for this validated XML node.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSchemaSimpleType" /> object that represents the dynamic schema type for this validated XML node.</returns>
		// Token: 0x170006E1 RID: 1761
		// (get) Token: 0x06001D22 RID: 7458
		XmlSchemaSimpleType MemberType { get; }

		/// <summary>Gets the static XML Schema Definition Language (XSD) schema type of this validated XML node.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSchemaType" /> of this validated XML node.</returns>
		// Token: 0x170006E2 RID: 1762
		// (get) Token: 0x06001D23 RID: 7459
		XmlSchemaType SchemaType { get; }

		/// <summary>Gets the compiled <see cref="T:System.Xml.Schema.XmlSchemaElement" /> that corresponds to this validated XML node.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSchemaElement" /> that corresponds to this validated XML node.</returns>
		// Token: 0x170006E3 RID: 1763
		// (get) Token: 0x06001D24 RID: 7460
		XmlSchemaElement SchemaElement { get; }

		/// <summary>Gets the compiled <see cref="T:System.Xml.Schema.XmlSchemaAttribute" /> that corresponds to this validated XML node.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSchemaAttribute" /> that corresponds to this validated XML node.</returns>
		// Token: 0x170006E4 RID: 1764
		// (get) Token: 0x06001D25 RID: 7461
		XmlSchemaAttribute SchemaAttribute { get; }
	}
}
