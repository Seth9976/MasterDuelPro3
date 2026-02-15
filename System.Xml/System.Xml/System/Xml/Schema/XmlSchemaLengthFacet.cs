using System;

namespace System.Xml.Schema
{
	/// <summary>Represents the length facet from XML Schema as specified by the World Wide Web Consortium (W3C). This class can be used to specify a restriction on the length of a simpleType element on the data type.</summary>
	// Token: 0x020002D4 RID: 724
	public class XmlSchemaLengthFacet : XmlSchemaNumericFacet
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaLengthFacet" /> class.</summary>
		// Token: 0x06002134 RID: 8500 RVA: 0x000C01F2 File Offset: 0x000BE3F2
		public XmlSchemaLengthFacet()
		{
			base.FacetType = FacetType.Length;
		}
	}
}
