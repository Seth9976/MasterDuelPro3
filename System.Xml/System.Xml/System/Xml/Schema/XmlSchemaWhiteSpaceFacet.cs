using System;

namespace System.Xml.Schema
{
	/// <summary>Represents the World Wide Web Consortium (W3C) whiteSpace facet.</summary>
	// Token: 0x020002DF RID: 735
	public class XmlSchemaWhiteSpaceFacet : XmlSchemaFacet
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaWhiteSpaceFacet" /> class.</summary>
		// Token: 0x0600213F RID: 8511 RVA: 0x000C029B File Offset: 0x000BE49B
		public XmlSchemaWhiteSpaceFacet()
		{
			base.FacetType = FacetType.Whitespace;
		}
	}
}
