using System;

namespace System.Xml.Schema
{
	/// <summary>Represents the pattern element from XML Schema as specified by the World Wide Web Consortium (W3C). This class can be used to specify a restriction on the value entered for a simpleType element.</summary>
	// Token: 0x020002D7 RID: 727
	public class XmlSchemaPatternFacet : XmlSchemaFacet
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaPatternFacet" /> class.</summary>
		// Token: 0x06002137 RID: 8503 RVA: 0x000C021F File Offset: 0x000BE41F
		public XmlSchemaPatternFacet()
		{
			base.FacetType = FacetType.Pattern;
		}
	}
}
