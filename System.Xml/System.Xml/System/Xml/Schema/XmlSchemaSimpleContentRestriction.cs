using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Represents the restriction element for simple content from XML Schema as specified by the World Wide Web Consortium (W3C). This class can be used to derive simple types by restriction. Such derivations can be used to restrict the range of values for the element to a subset of the values specified in the inherited simple type.</summary>
	// Token: 0x020002FE RID: 766
	public class XmlSchemaSimpleContentRestriction : XmlSchemaContent
	{
		/// <summary>Gets or sets the name of the built-in data type or simple type from which this type is derived.</summary>
		/// <returns>The name of the base type.</returns>
		// Token: 0x17000867 RID: 2151
		// (get) Token: 0x06002235 RID: 8757 RVA: 0x000C2E6F File Offset: 0x000C106F
		// (set) Token: 0x06002236 RID: 8758 RVA: 0x000C2E77 File Offset: 0x000C1077
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

		/// <summary>Gets or sets the simple type base value.</summary>
		/// <returns>The simple type base value.</returns>
		// Token: 0x17000868 RID: 2152
		// (get) Token: 0x06002237 RID: 8759 RVA: 0x000C2E90 File Offset: 0x000C1090
		// (set) Token: 0x06002238 RID: 8760 RVA: 0x000C2E98 File Offset: 0x000C1098
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
		// Token: 0x17000869 RID: 2153
		// (get) Token: 0x06002239 RID: 8761 RVA: 0x000C2EA1 File Offset: 0x000C10A1
		[XmlElement("minInclusive", typeof(XmlSchemaMinInclusiveFacet))]
		[XmlElement("minLength", typeof(XmlSchemaMinLengthFacet))]
		[XmlElement("maxLength", typeof(XmlSchemaMaxLengthFacet))]
		[XmlElement("pattern", typeof(XmlSchemaPatternFacet))]
		[XmlElement("enumeration", typeof(XmlSchemaEnumerationFacet))]
		[XmlElement("length", typeof(XmlSchemaLengthFacet))]
		[XmlElement("whiteSpace", typeof(XmlSchemaWhiteSpaceFacet))]
		[XmlElement("fractionDigits", typeof(XmlSchemaFractionDigitsFacet))]
		[XmlElement("totalDigits", typeof(XmlSchemaTotalDigitsFacet))]
		[XmlElement("minExclusive", typeof(XmlSchemaMinExclusiveFacet))]
		[XmlElement("maxInclusive", typeof(XmlSchemaMaxInclusiveFacet))]
		[XmlElement("maxExclusive", typeof(XmlSchemaMaxExclusiveFacet))]
		public XmlSchemaObjectCollection Facets
		{
			get
			{
				return this.facets;
			}
		}

		/// <summary>Gets the <see cref="T:System.Xml.Schema.XmlSchemaAttribute" /> and <see cref="T:System.Xml.Schema.XmlSchemaAttributeGroupRef" /> collection of attributes for the simple type.</summary>
		/// <returns>The collection of attributes for a simple type.</returns>
		// Token: 0x1700086A RID: 2154
		// (get) Token: 0x0600223A RID: 8762 RVA: 0x000C2EA9 File Offset: 0x000C10A9
		[XmlElement("attributeGroup", typeof(XmlSchemaAttributeGroupRef))]
		[XmlElement("attribute", typeof(XmlSchemaAttribute))]
		public XmlSchemaObjectCollection Attributes
		{
			get
			{
				return this.attributes;
			}
		}

		/// <summary>Gets or sets an <see cref="T:System.Xml.Schema.XmlSchemaAnyAttribute" /> to be used for the attribute value.</summary>
		/// <returns>The <see cref="T:System.Xml.Schema.XmlSchemaAnyAttribute" /> for the attribute value.Optional.</returns>
		// Token: 0x1700086B RID: 2155
		// (get) Token: 0x0600223B RID: 8763 RVA: 0x000C2EB1 File Offset: 0x000C10B1
		// (set) Token: 0x0600223C RID: 8764 RVA: 0x000C2EB9 File Offset: 0x000C10B9
		[XmlElement("anyAttribute")]
		public XmlSchemaAnyAttribute AnyAttribute
		{
			get
			{
				return this.anyAttribute;
			}
			set
			{
				this.anyAttribute = value;
			}
		}

		// Token: 0x0600223D RID: 8765 RVA: 0x000C2EC2 File Offset: 0x000C10C2
		internal void SetAttributes(XmlSchemaObjectCollection newAttributes)
		{
			this.attributes = newAttributes;
		}

		// Token: 0x04000FE7 RID: 4071
		private XmlQualifiedName baseTypeName = XmlQualifiedName.Empty;

		// Token: 0x04000FE8 RID: 4072
		private XmlSchemaSimpleType baseType;

		// Token: 0x04000FE9 RID: 4073
		private XmlSchemaObjectCollection facets = new XmlSchemaObjectCollection();

		// Token: 0x04000FEA RID: 4074
		private XmlSchemaObjectCollection attributes = new XmlSchemaObjectCollection();

		// Token: 0x04000FEB RID: 4075
		private XmlSchemaAnyAttribute anyAttribute;
	}
}
