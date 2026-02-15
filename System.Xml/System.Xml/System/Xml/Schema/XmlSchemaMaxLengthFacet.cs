using System;

namespace System.Xml.Schema
{
	/// <summary>Represents the maxLength element from XML Schema as specified by the World Wide Web Consortium (W3C). This class can be used to specify a restriction on the maximum length of the data value of a simpleType element. The length must be less than the value of the maxLength element.</summary>
	// Token: 0x020002D6 RID: 726
	public class XmlSchemaMaxLengthFacet : XmlSchemaNumericFacet
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaMaxLengthFacet" /> class.</summary>
		// Token: 0x06002136 RID: 8502 RVA: 0x000C0210 File Offset: 0x000BE410
		public XmlSchemaMaxLengthFacet()
		{
			base.FacetType = FacetType.MaxLength;
		}
	}
}
