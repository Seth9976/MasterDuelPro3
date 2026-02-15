using System;

namespace System.Xml.Schema
{
	/// <summary>Represents the enumeration facet from XML Schema as specified by the World Wide Web Consortium (W3C). This class specifies a list of valid values for a simpleType element. Declaration is contained within a restriction declaration.</summary>
	// Token: 0x020002D8 RID: 728
	public class XmlSchemaEnumerationFacet : XmlSchemaFacet
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaEnumerationFacet" /> class.</summary>
		// Token: 0x06002138 RID: 8504 RVA: 0x000C022E File Offset: 0x000BE42E
		public XmlSchemaEnumerationFacet()
		{
			base.FacetType = FacetType.Enumeration;
		}
	}
}
