using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>The base class for all simple types and complex types.</summary>
	// Token: 0x02000306 RID: 774
	public class XmlSchemaType : XmlSchemaAnnotated
	{
		/// <summary>Returns an <see cref="T:System.Xml.Schema.XmlSchemaSimpleType" /> that represents the built-in simple type of the simple type that is specified by the qualified name.</summary>
		/// <returns>The <see cref="T:System.Xml.Schema.XmlSchemaSimpleType" /> that represents the built-in simple type.</returns>
		/// <param name="qualifiedName">The <see cref="T:System.Xml.XmlQualifiedName" /> of the simple type.</param>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Xml.XmlQualifiedName" /> parameter is null.</exception>
		// Token: 0x06002261 RID: 8801 RVA: 0x000C3156 File Offset: 0x000C1356
		public static XmlSchemaSimpleType GetBuiltInSimpleType(XmlQualifiedName qualifiedName)
		{
			if (qualifiedName == null)
			{
				throw new ArgumentNullException("qualifiedName");
			}
			return DatatypeImplementation.GetSimpleTypeFromXsdType(qualifiedName);
		}

		/// <summary>Returns an <see cref="T:System.Xml.Schema.XmlSchemaSimpleType" /> that represents the built-in simple type of the specified simple type.</summary>
		/// <returns>The <see cref="T:System.Xml.Schema.XmlSchemaSimpleType" /> that represents the built-in simple type.</returns>
		/// <param name="typeCode">One of the <see cref="T:System.Xml.Schema.XmlTypeCode" /> values representing the simple type.</param>
		// Token: 0x06002262 RID: 8802 RVA: 0x000C3172 File Offset: 0x000C1372
		public static XmlSchemaSimpleType GetBuiltInSimpleType(XmlTypeCode typeCode)
		{
			return DatatypeImplementation.GetSimpleTypeFromTypeCode(typeCode);
		}

		/// <summary>Returns an <see cref="T:System.Xml.Schema.XmlSchemaComplexType" /> that represents the built-in complex type of the complex type specified by qualified name.</summary>
		/// <returns>The <see cref="T:System.Xml.Schema.XmlSchemaComplexType" /> that represents the built-in complex type.</returns>
		/// <param name="qualifiedName">The <see cref="T:System.Xml.XmlQualifiedName" /> of the complex type.</param>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Xml.XmlQualifiedName" /> parameter is null.</exception>
		// Token: 0x06002263 RID: 8803 RVA: 0x000C317C File Offset: 0x000C137C
		public static XmlSchemaComplexType GetBuiltInComplexType(XmlQualifiedName qualifiedName)
		{
			if (qualifiedName == null)
			{
				throw new ArgumentNullException("qualifiedName");
			}
			if (qualifiedName.Equals(XmlSchemaComplexType.AnyType.QualifiedName))
			{
				return XmlSchemaComplexType.AnyType;
			}
			if (qualifiedName.Equals(XmlSchemaComplexType.UntypedAnyType.QualifiedName))
			{
				return XmlSchemaComplexType.UntypedAnyType;
			}
			return null;
		}

		/// <summary>Gets or sets the name of the type.</summary>
		/// <returns>The name of the type.</returns>
		// Token: 0x1700087A RID: 2170
		// (get) Token: 0x06002264 RID: 8804 RVA: 0x000C31CE File Offset: 0x000C13CE
		// (set) Token: 0x06002265 RID: 8805 RVA: 0x000C31D6 File Offset: 0x000C13D6
		[XmlAttribute("name")]
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		/// <summary>Gets or sets the final attribute of the type derivation that indicates if further derivations are allowed.</summary>
		/// <returns>One of the valid <see cref="T:System.Xml.Schema.XmlSchemaDerivationMethod" /> values. The default is <see cref="F:System.Xml.Schema.XmlSchemaDerivationMethod.None" />.</returns>
		// Token: 0x1700087B RID: 2171
		// (get) Token: 0x06002266 RID: 8806 RVA: 0x000C31DF File Offset: 0x000C13DF
		// (set) Token: 0x06002267 RID: 8807 RVA: 0x000C31E7 File Offset: 0x000C13E7
		[XmlAttribute("final")]
		[DefaultValue(XmlSchemaDerivationMethod.None)]
		public XmlSchemaDerivationMethod Final
		{
			get
			{
				return this.final;
			}
			set
			{
				this.final = value;
			}
		}

		/// <summary>Gets the qualified name for the type built from the Name attribute of this type. This is a post-schema-compilation property.</summary>
		/// <returns>The <see cref="T:System.Xml.XmlQualifiedName" /> for the type built from the Name attribute of this type.</returns>
		// Token: 0x1700087C RID: 2172
		// (get) Token: 0x06002268 RID: 8808 RVA: 0x000C31F0 File Offset: 0x000C13F0
		[XmlIgnore]
		public XmlQualifiedName QualifiedName
		{
			get
			{
				return this.qname;
			}
		}

		/// <summary>Gets the post-compilation value of the <see cref="P:System.Xml.Schema.XmlSchemaType.Final" /> property.</summary>
		/// <returns>The post-compilation value of the <see cref="P:System.Xml.Schema.XmlSchemaType.Final" /> property. The default is the finalDefault attribute value of the schema element.</returns>
		// Token: 0x1700087D RID: 2173
		// (get) Token: 0x06002269 RID: 8809 RVA: 0x000C31FA File Offset: 0x000C13FA
		[XmlIgnore]
		public XmlSchemaDerivationMethod FinalResolved
		{
			get
			{
				return this.finalResolved;
			}
		}

		/// <summary>Gets the post-compilation value for the base type of this schema type.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSchemaType" /> object representing the base type of this schema type.</returns>
		// Token: 0x1700087E RID: 2174
		// (get) Token: 0x0600226A RID: 8810 RVA: 0x000C3202 File Offset: 0x000C1402
		[XmlIgnore]
		public XmlSchemaType BaseXmlSchemaType
		{
			get
			{
				return this.baseSchemaType;
			}
		}

		/// <summary>Gets the post-compilation information on how this element was derived from its base type.</summary>
		/// <returns>One of the valid <see cref="T:System.Xml.Schema.XmlSchemaDerivationMethod" /> values.</returns>
		// Token: 0x1700087F RID: 2175
		// (get) Token: 0x0600226B RID: 8811 RVA: 0x000C320A File Offset: 0x000C140A
		[XmlIgnore]
		public XmlSchemaDerivationMethod DerivedBy
		{
			get
			{
				return this.derivedBy;
			}
		}

		/// <summary>Gets the post-compilation value for the data type of the complex type.</summary>
		/// <returns>The <see cref="T:System.Xml.Schema.XmlSchemaDatatype" /> post-schema-compilation value.</returns>
		// Token: 0x17000880 RID: 2176
		// (get) Token: 0x0600226C RID: 8812 RVA: 0x000C3212 File Offset: 0x000C1412
		[XmlIgnore]
		public XmlSchemaDatatype Datatype
		{
			get
			{
				return this.datatype;
			}
		}

		/// <summary>Gets or sets a value indicating if this type has a mixed content model. This property is only valid in a complex type.</summary>
		/// <returns>true if the type has a mixed content model; otherwise, false. The default is false.</returns>
		// Token: 0x17000881 RID: 2177
		// (get) Token: 0x0600226D RID: 8813 RVA: 0x0000C1F5 File Offset: 0x0000A3F5
		// (set) Token: 0x0600226E RID: 8814 RVA: 0x0000A558 File Offset: 0x00008758
		[XmlIgnore]
		public virtual bool IsMixed
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		/// <summary>Gets the <see cref="T:System.Xml.Schema.XmlTypeCode" /> of the type.</summary>
		/// <returns>One of the <see cref="T:System.Xml.Schema.XmlTypeCode" /> values.</returns>
		// Token: 0x17000882 RID: 2178
		// (get) Token: 0x0600226F RID: 8815 RVA: 0x000C321A File Offset: 0x000C141A
		[XmlIgnore]
		public XmlTypeCode TypeCode
		{
			get
			{
				if (this == XmlSchemaComplexType.AnyType)
				{
					return XmlTypeCode.Item;
				}
				if (this.datatype == null)
				{
					return XmlTypeCode.None;
				}
				return this.datatype.TypeCode;
			}
		}

		// Token: 0x17000883 RID: 2179
		// (get) Token: 0x06002270 RID: 8816 RVA: 0x000C323B File Offset: 0x000C143B
		[XmlIgnore]
		internal XmlValueConverter ValueConverter
		{
			get
			{
				if (this.datatype == null)
				{
					return XmlUntypedConverter.Untyped;
				}
				return this.datatype.ValueConverter;
			}
		}

		// Token: 0x17000884 RID: 2180
		// (get) Token: 0x06002271 RID: 8817 RVA: 0x000C3256 File Offset: 0x000C1456
		internal XmlSchemaContentType SchemaContentType
		{
			get
			{
				return this.contentType;
			}
		}

		// Token: 0x06002272 RID: 8818 RVA: 0x000C325E File Offset: 0x000C145E
		internal void SetQualifiedName(XmlQualifiedName value)
		{
			this.qname = value;
		}

		// Token: 0x06002273 RID: 8819 RVA: 0x000C3269 File Offset: 0x000C1469
		internal void SetFinalResolved(XmlSchemaDerivationMethod value)
		{
			this.finalResolved = value;
		}

		// Token: 0x06002274 RID: 8820 RVA: 0x000C3272 File Offset: 0x000C1472
		internal void SetBaseSchemaType(XmlSchemaType value)
		{
			this.baseSchemaType = value;
		}

		// Token: 0x06002275 RID: 8821 RVA: 0x000C327B File Offset: 0x000C147B
		internal void SetDerivedBy(XmlSchemaDerivationMethod value)
		{
			this.derivedBy = value;
		}

		// Token: 0x06002276 RID: 8822 RVA: 0x000C3284 File Offset: 0x000C1484
		internal void SetDatatype(XmlSchemaDatatype value)
		{
			this.datatype = value;
		}

		// Token: 0x17000885 RID: 2181
		// (get) Token: 0x06002277 RID: 8823 RVA: 0x000C328D File Offset: 0x000C148D
		// (set) Token: 0x06002278 RID: 8824 RVA: 0x000C3297 File Offset: 0x000C1497
		internal SchemaElementDecl ElementDecl
		{
			get
			{
				return this.elementDecl;
			}
			set
			{
				this.elementDecl = value;
			}
		}

		// Token: 0x17000886 RID: 2182
		// (get) Token: 0x06002279 RID: 8825 RVA: 0x000C32A2 File Offset: 0x000C14A2
		// (set) Token: 0x0600227A RID: 8826 RVA: 0x000C32AA File Offset: 0x000C14AA
		[XmlIgnore]
		internal XmlSchemaType Redefined
		{
			get
			{
				return this.redefined;
			}
			set
			{
				this.redefined = value;
			}
		}

		// Token: 0x17000887 RID: 2183
		// (get) Token: 0x0600227B RID: 8827 RVA: 0x000C32B3 File Offset: 0x000C14B3
		internal virtual XmlQualifiedName DerivedFrom
		{
			get
			{
				return XmlQualifiedName.Empty;
			}
		}

		// Token: 0x0600227C RID: 8828 RVA: 0x000C32BA File Offset: 0x000C14BA
		internal void SetContentType(XmlSchemaContentType value)
		{
			this.contentType = value;
		}

		/// <summary>Returns a value indicating if the derived schema type specified is derived from the base schema type specified</summary>
		/// <returns>true if the derived type is derived from the base type; otherwise, false.</returns>
		/// <param name="derivedType">The derived <see cref="T:System.Xml.Schema.XmlSchemaType" /> to test.</param>
		/// <param name="baseType">The base <see cref="T:System.Xml.Schema.XmlSchemaType" /> to test the derived <see cref="T:System.Xml.Schema.XmlSchemaType" /> against.</param>
		/// <param name="except">One of the <see cref="T:System.Xml.Schema.XmlSchemaDerivationMethod" /> values representing a type derivation method to exclude from testing.</param>
		// Token: 0x0600227D RID: 8829 RVA: 0x000C32C4 File Offset: 0x000C14C4
		public static bool IsDerivedFrom(XmlSchemaType derivedType, XmlSchemaType baseType, XmlSchemaDerivationMethod except)
		{
			if (derivedType == null || baseType == null)
			{
				return false;
			}
			if (derivedType == baseType)
			{
				return true;
			}
			if (baseType == XmlSchemaComplexType.AnyType)
			{
				return true;
			}
			XmlSchemaSimpleType xmlSchemaSimpleType;
			XmlSchemaSimpleType xmlSchemaSimpleType2;
			for (;;)
			{
				xmlSchemaSimpleType = derivedType as XmlSchemaSimpleType;
				xmlSchemaSimpleType2 = baseType as XmlSchemaSimpleType;
				if (xmlSchemaSimpleType2 != null && xmlSchemaSimpleType != null)
				{
					break;
				}
				if ((except & derivedType.DerivedBy) != XmlSchemaDerivationMethod.Empty)
				{
					return false;
				}
				derivedType = derivedType.BaseXmlSchemaType;
				if (derivedType == baseType)
				{
					return true;
				}
				if (derivedType == null)
				{
					return false;
				}
			}
			return xmlSchemaSimpleType2 == DatatypeImplementation.AnySimpleType || ((except & derivedType.DerivedBy) == XmlSchemaDerivationMethod.Empty && xmlSchemaSimpleType.Datatype.IsDerivedFrom(xmlSchemaSimpleType2.Datatype));
		}

		// Token: 0x0600227E RID: 8830 RVA: 0x000C3346 File Offset: 0x000C1546
		internal static bool IsDerivedFromDatatype(XmlSchemaDatatype derivedDataType, XmlSchemaDatatype baseDataType, XmlSchemaDerivationMethod except)
		{
			return DatatypeImplementation.AnySimpleType.Datatype == baseDataType || derivedDataType.IsDerivedFrom(baseDataType);
		}

		// Token: 0x17000888 RID: 2184
		// (get) Token: 0x0600227F RID: 8831 RVA: 0x000C335E File Offset: 0x000C155E
		// (set) Token: 0x06002280 RID: 8832 RVA: 0x000C3366 File Offset: 0x000C1566
		[XmlIgnore]
		internal override string NameAttribute
		{
			get
			{
				return this.Name;
			}
			set
			{
				this.Name = value;
			}
		}

		// Token: 0x04000FF9 RID: 4089
		private string name;

		// Token: 0x04000FFA RID: 4090
		private XmlSchemaDerivationMethod final = XmlSchemaDerivationMethod.None;

		// Token: 0x04000FFB RID: 4091
		private XmlSchemaDerivationMethod derivedBy;

		// Token: 0x04000FFC RID: 4092
		private XmlSchemaType baseSchemaType;

		// Token: 0x04000FFD RID: 4093
		private XmlSchemaDatatype datatype;

		// Token: 0x04000FFE RID: 4094
		private XmlSchemaDerivationMethod finalResolved;

		// Token: 0x04000FFF RID: 4095
		private volatile SchemaElementDecl elementDecl;

		// Token: 0x04001000 RID: 4096
		private volatile XmlQualifiedName qname = XmlQualifiedName.Empty;

		// Token: 0x04001001 RID: 4097
		private XmlSchemaType redefined;

		// Token: 0x04001002 RID: 4098
		private XmlSchemaContentType contentType;
	}
}
