using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Represents the restriction element for simple types from XML Schema as specified by the World Wide Web Consortium (W3C). This class can be used restricting simpleType element.</summary>
	// Token: 0x02000302 RID: 770
	public class XmlSchemaSimpleTypeRestriction : XmlSchemaSimpleTypeContent
	{
		/// <summary>Gets or sets the name of the qualified base type.</summary>
		/// <returns>The qualified name of the simple type restriction base type.</returns>
		// Token: 0x17000871 RID: 2161
		// (get) Token: 0x0600224D RID: 8781 RVA: 0x000C2FEC File Offset: 0x000C11EC
		// (set) Token: 0x0600224E RID: 8782 RVA: 0x000C2FF4 File Offset: 0x000C11F4
		[XmlAttribute("base")]
		public XmlQualifiedName BaseTypeName
		{
			get
			{
				return this.baseTypeName;
			}
			set
			{
				this.baseTypeName = ((value == null) ? XmlQualifiedName.Empty : value);
			}
		}

		/// <summary>Gets or sets information on the base type.</summary>
		/// <returns>The base type for the simpleType element.</returns>
		// Token: 0x17000872 RID: 2162
		// (get) Token: 0x0600224F RID: 8783 RVA: 0x000C300D File Offset: 0x000C120D
		// (set) Token: 0x06002250 RID: 8784 RVA: 0x000C3015 File Offset: 0x000C1215
		[XmlElement("simpleType", typeof(XmlSchemaSimpleType))]
		public XmlSchemaSimpleType BaseType
		{
			get
			{
				return this.baseType;
			}
			set
			{
				this.baseType = value;
			}
		}

		/// <summary>Gets or sets an Xml Schema facet. </summary>
		/// <returns>One of the following facet classes:<see cref="T:System.Xml.Schema.XmlSchemaLengthFacet" />, <see cref="T:System.Xml.Schema.XmlSchemaMinLengthFacet" />, <see cref="T:System.Xml.Schema.XmlSchemaMaxLengthFacet" />, <see cref="T:System.Xml.Schema.XmlSchemaPatternFacet" />, <see cref="T:System.Xml.Schema.XmlSchemaEnumerationFacet" />, <see cref="T:System.Xml.Schema.XmlSchemaMaxInclusiveFacet" />, <see cref="T:System.Xml.Schema.XmlSchemaMaxExclusiveFacet" />, <see cref="T:System.Xml.Schema.XmlSchemaMinInclusiveFacet" />, <see cref="T:System.Xml.Schema.XmlSchemaMinExclusiveFacet" />, <see cref="T:System.Xml.Schema.XmlSchemaFractionDigitsFacet" />, <see cref="T:System.Xml.Schema.XmlSchemaTotalDigitsFacet" />, <see cref="T:System.Xml.Schema.XmlSchemaWhiteSpaceFacet" />.</returns>
		// Token: 0x17000873 RID: 2163
		// (get) Token: 0x06002251 RID: 8785 RVA: 0x000C301E File Offset: 0x000C121E
		[XmlElement("length", typeof(XmlSchemaLengthFacet))]
		[XmlElement("minLength", typeof(XmlSchemaMinLengthFacet))]
		[XmlElement("whiteSpace", typeof(XmlSchemaWhiteSpaceFacet))]
		[XmlElement("maxLength", typeof(XmlSchemaMaxLengthFacet))]
		[XmlElement("enumeration", typeof(XmlSchemaEnumerationFacet))]
		[XmlElement("fractionDigits", typeof(XmlSchemaFractionDigitsFacet))]
		[XmlElement("maxInclusive", typeof(XmlSchemaMaxInclusiveFacet))]
		[XmlElement("pattern", typeof(XmlSchemaPatternFacet))]
		[XmlElement("maxExclusive", typeof(XmlSchemaMaxExclusiveFacet))]
		[XmlElement("minInclusive", typeof(XmlSchemaMinInclusiveFacet))]
		[XmlElement("totalDigits", typeof(XmlSchemaTotalDigitsFacet))]
		[XmlElement("minExclusive", typeof(XmlSchemaMinExclusiveFacet))]
		public XmlSchemaObjectCollection Facets
		{
			get
			{
				return this.facets;
			}
		}

		// Token: 0x06002252 RID: 8786 RVA: 0x000C3026 File Offset: 0x000C1226
		internal override XmlSchemaObject Clone()
		{
			XmlSchemaSimpleTypeRestriction xmlSchemaSimpleTypeRestriction = (XmlSchemaSimpleTypeRestriction)base.MemberwiseClone();
			xmlSchemaSimpleTypeRestriction.BaseTypeName = this.baseTypeName.Clone();
			return xmlSchemaSimpleTypeRestriction;
		}

		// Token: 0x04000FF0 RID: 4080
		private XmlQualifiedName baseTypeName = XmlQualifiedName.Empty;

		// Token: 0x04000FF1 RID: 4081
		private XmlSchemaSimpleType baseType;

		// Token: 0x04000FF2 RID: 4082
		private XmlSchemaObjectCollection facets = new XmlSchemaObjectCollection();
	}
}
