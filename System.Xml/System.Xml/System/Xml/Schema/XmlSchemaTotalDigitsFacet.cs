using System;

namespace System.Xml.Schema
{
	/// <summary>Represents the totalDigits facet from XML Schema as specified by the World Wide Web Consortium (W3C). This class can be used to specify a restriction on the number of digits that can be entered for the value of a simpleType element. That value of totalDigits must be a positive integer.</summary>
	// Token: 0x020002DD RID: 733
	public class XmlSchemaTotalDigitsFacet : XmlSchemaNumericFacet
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaTotalDigitsFacet" /> class.</summary>
		// Token: 0x0600213D RID: 8509 RVA: 0x000C027B File Offset: 0x000BE47B
		public XmlSchemaTotalDigitsFacet()
		{
			base.FacetType = FacetType.TotalDigits;
		}
	}
}
