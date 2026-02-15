using System;

namespace System.Xml.Schema
{
	/// <summary>Represents the validity of an XML item validated by the <see cref="T:System.Xml.Schema.XmlSchemaValidator" /> class.</summary>
	// Token: 0x0200030E RID: 782
	public enum XmlSchemaValidity
	{
		/// <summary>The validity of the XML item is not known.</summary>
		// Token: 0x0400104F RID: 4175
		NotKnown,
		/// <summary>The XML item is valid.</summary>
		// Token: 0x04001050 RID: 4176
		Valid,
		/// <summary>The XML item is invalid.</summary>
		// Token: 0x04001051 RID: 4177
		Invalid
	}
}
