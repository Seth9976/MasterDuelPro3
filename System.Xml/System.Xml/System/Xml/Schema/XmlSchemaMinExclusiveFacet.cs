using System;

namespace System.Xml.Schema
{
	/// <summary>Represents the minExclusive element from XML Schema as specified by the World Wide Web Consortium (W3C). This class can be used to specify a restriction on the minimum value of a simpleType element. The element value must be greater than the value of the minExclusive element.</summary>
	// Token: 0x020002D9 RID: 729
	public class XmlSchemaMinExclusiveFacet : XmlSchemaFacet
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaMinExclusiveFacet" /> class.</summary>
		// Token: 0x06002139 RID: 8505 RVA: 0x000C023D File Offset: 0x000BE43D
		public XmlSchemaMinExclusiveFacet()
		{
			base.FacetType = FacetType.MinExclusive;
		}
	}
}
