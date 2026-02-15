using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Represents the element element from XML Schema as specified by the World Wide Web Consortium (W3C). This class is the base class for all particle types and is used to describe an element in an XML document.</summary>
	// Token: 0x020002CE RID: 718
	public class XmlSchemaElement : XmlSchemaParticle
	{
		/// <summary>Gets or sets information to indicate if the element can be used in an instance document.</summary>
		/// <returns>If true, the element cannot appear in the instance document. The default is false.Optional.</returns>
		// Token: 0x170007E5 RID: 2021
		// (get) Token: 0x060020D5 RID: 8405 RVA: 0x000BFAD8 File Offset: 0x000BDCD8
		// (set) Token: 0x060020D6 RID: 8406 RVA: 0x000BFAE0 File Offset: 0x000BDCE0
		[DefaultValue(false)]
		[XmlAttribute("abstract")]
		public bool IsAbstract
		{
			get
			{
				return this.isAbstract;
			}
			set
			{
				this.isAbstract = value;
				this.hasAbstractAttribute = true;
			}
		}

		/// <summary>Gets or sets a Block derivation.</summary>
		/// <returns>The attribute used to block a type derivation. Default value is XmlSchemaDerivationMethod.None.Optional.</returns>
		// Token: 0x170007E6 RID: 2022
		// (get) Token: 0x060020D7 RID: 8407 RVA: 0x000BFAF0 File Offset: 0x000BDCF0
		// (set) Token: 0x060020D8 RID: 8408 RVA: 0x000BFAF8 File Offset: 0x000BDCF8
		[DefaultValue(XmlSchemaDerivationMethod.None)]
		[XmlAttribute("block")]
		public XmlSchemaDerivationMethod Block
		{
			get
			{
				return this.block;
			}
			set
			{
				this.block = value;
			}
		}

		/// <summary>Gets or sets the default value of the element if its content is a simple type or content of the element is textOnly.</summary>
		/// <returns>The default value for the element. The default is a null reference.Optional.</returns>
		// Token: 0x170007E7 RID: 2023
		// (get) Token: 0x060020D9 RID: 8409 RVA: 0x000BFB01 File Offset: 0x000BDD01
		// (set) Token: 0x060020DA RID: 8410 RVA: 0x000BFB09 File Offset: 0x000BDD09
		[DefaultValue(null)]
		[XmlAttribute("default")]
		public string DefaultValue
		{
			get
			{
				return this.defaultValue;
			}
			set
			{
				this.defaultValue = value;
			}
		}

		/// <summary>Gets or sets the Final property to indicate that no further derivations are allowed.</summary>
		/// <returns>The Final property. The default is XmlSchemaDerivationMethod.None.Optional.</returns>
		// Token: 0x170007E8 RID: 2024
		// (get) Token: 0x060020DB RID: 8411 RVA: 0x000BFB12 File Offset: 0x000BDD12
		// (set) Token: 0x060020DC RID: 8412 RVA: 0x000BFB1A File Offset: 0x000BDD1A
		[DefaultValue(XmlSchemaDerivationMethod.None)]
		[XmlAttribute("final")]
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

		/// <summary>Gets or sets the fixed value.</summary>
		/// <returns>The fixed value that is predetermined and unchangeable. The default is a null reference.Optional.</returns>
		// Token: 0x170007E9 RID: 2025
		// (get) Token: 0x060020DD RID: 8413 RVA: 0x000BFB23 File Offset: 0x000BDD23
		// (set) Token: 0x060020DE RID: 8414 RVA: 0x000BFB2B File Offset: 0x000BDD2B
		[DefaultValue(null)]
		[XmlAttribute("fixed")]
		public string FixedValue
		{
			get
			{
				return this.fixedValue;
			}
			set
			{
				this.fixedValue = value;
			}
		}

		/// <summary>Gets or sets the form for the element.</summary>
		/// <returns>The form for the element. The default is the <see cref="P:System.Xml.Schema.XmlSchema.ElementFormDefault" /> value.Optional.</returns>
		// Token: 0x170007EA RID: 2026
		// (get) Token: 0x060020DF RID: 8415 RVA: 0x000BFB34 File Offset: 0x000BDD34
		// (set) Token: 0x060020E0 RID: 8416 RVA: 0x000BFB3C File Offset: 0x000BDD3C
		[DefaultValue(XmlSchemaForm.None)]
		[XmlAttribute("form")]
		public XmlSchemaForm Form
		{
			get
			{
				return this.form;
			}
			set
			{
				this.form = value;
			}
		}

		/// <summary>Gets or sets the name of the element.</summary>
		/// <returns>The name of the element. The default is String.Empty.</returns>
		// Token: 0x170007EB RID: 2027
		// (get) Token: 0x060020E1 RID: 8417 RVA: 0x000BFB45 File Offset: 0x000BDD45
		// (set) Token: 0x060020E2 RID: 8418 RVA: 0x000BFB4D File Offset: 0x000BDD4D
		[DefaultValue("")]
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

		/// <summary>Gets or sets information that indicates if xsi:nil can occur in the instance data. Indicates if an explicit nil value can be assigned to the element.</summary>
		/// <returns>If nillable is true, this enables an instance of the element to have the nil attribute set to true. The nil attribute is defined as part of the XML Schema namespace for instances. The default is false.Optional.</returns>
		// Token: 0x170007EC RID: 2028
		// (get) Token: 0x060020E3 RID: 8419 RVA: 0x000BFB56 File Offset: 0x000BDD56
		// (set) Token: 0x060020E4 RID: 8420 RVA: 0x000BFB5E File Offset: 0x000BDD5E
		[DefaultValue(false)]
		[XmlAttribute("nillable")]
		public bool IsNillable
		{
			get
			{
				return this.isNillable;
			}
			set
			{
				this.isNillable = value;
				this.hasNillableAttribute = true;
			}
		}

		// Token: 0x170007ED RID: 2029
		// (get) Token: 0x060020E5 RID: 8421 RVA: 0x000BFB6E File Offset: 0x000BDD6E
		[XmlIgnore]
		internal bool HasNillableAttribute
		{
			get
			{
				return this.hasNillableAttribute;
			}
		}

		// Token: 0x170007EE RID: 2030
		// (get) Token: 0x060020E6 RID: 8422 RVA: 0x000BFB76 File Offset: 0x000BDD76
		[XmlIgnore]
		internal bool HasAbstractAttribute
		{
			get
			{
				return this.hasAbstractAttribute;
			}
		}

		/// <summary>Gets or sets the reference name of an element declared in this schema (or another schema indicated by the specified namespace).</summary>
		/// <returns>The reference name of the element.</returns>
		// Token: 0x170007EF RID: 2031
		// (get) Token: 0x060020E7 RID: 8423 RVA: 0x000BFB7E File Offset: 0x000BDD7E
		// (set) Token: 0x060020E8 RID: 8424 RVA: 0x000BFB86 File Offset: 0x000BDD86
		[XmlAttribute("ref")]
		public XmlQualifiedName RefName
		{
			get
			{
				return this.refName;
			}
			set
			{
				this.refName = ((value == null) ? XmlQualifiedName.Empty : value);
			}
		}

		/// <summary>Gets or sets the name of an element that is being substituted by this element.</summary>
		/// <returns>The qualified name of an element that is being substituted by this element.Optional.</returns>
		// Token: 0x170007F0 RID: 2032
		// (get) Token: 0x060020E9 RID: 8425 RVA: 0x000BFB9F File Offset: 0x000BDD9F
		// (set) Token: 0x060020EA RID: 8426 RVA: 0x000BFBA7 File Offset: 0x000BDDA7
		[XmlAttribute("substitutionGroup")]
		public XmlQualifiedName SubstitutionGroup
		{
			get
			{
				return this.substitutionGroup;
			}
			set
			{
				this.substitutionGroup = ((value == null) ? XmlQualifiedName.Empty : value);
			}
		}

		/// <summary>Gets or sets the name of a built-in data type defined in this schema or another schema indicated by the specified namespace.</summary>
		/// <returns>The name of the built-in data type.</returns>
		// Token: 0x170007F1 RID: 2033
		// (get) Token: 0x060020EB RID: 8427 RVA: 0x000BFBC0 File Offset: 0x000BDDC0
		// (set) Token: 0x060020EC RID: 8428 RVA: 0x000BFBC8 File Offset: 0x000BDDC8
		[XmlAttribute("type")]
		public XmlQualifiedName SchemaTypeName
		{
			get
			{
				return this.typeName;
			}
			set
			{
				this.typeName = ((value == null) ? XmlQualifiedName.Empty : value);
			}
		}

		/// <summary>Gets or sets the type of the element. This can either be a complex type or a simple type.</summary>
		/// <returns>The type of the element.</returns>
		// Token: 0x170007F2 RID: 2034
		// (get) Token: 0x060020ED RID: 8429 RVA: 0x000BFBE1 File Offset: 0x000BDDE1
		// (set) Token: 0x060020EE RID: 8430 RVA: 0x000BFBE9 File Offset: 0x000BDDE9
		[XmlElement("simpleType", typeof(XmlSchemaSimpleType))]
		[XmlElement("complexType", typeof(XmlSchemaComplexType))]
		public XmlSchemaType SchemaType
		{
			get
			{
				return this.type;
			}
			set
			{
				this.type = value;
			}
		}

		/// <summary>Gets the collection of constraints on the element.</summary>
		/// <returns>The collection of constraints.</returns>
		// Token: 0x170007F3 RID: 2035
		// (get) Token: 0x060020EF RID: 8431 RVA: 0x000BFBF2 File Offset: 0x000BDDF2
		[XmlElement("key", typeof(XmlSchemaKey))]
		[XmlElement("keyref", typeof(XmlSchemaKeyref))]
		[XmlElement("unique", typeof(XmlSchemaUnique))]
		public XmlSchemaObjectCollection Constraints
		{
			get
			{
				if (this.constraints == null)
				{
					this.constraints = new XmlSchemaObjectCollection();
				}
				return this.constraints;
			}
		}

		/// <summary>Gets the actual qualified name for the given element. </summary>
		/// <returns>The qualified name of the element. The post-compilation value of the QualifiedName property.</returns>
		// Token: 0x170007F4 RID: 2036
		// (get) Token: 0x060020F0 RID: 8432 RVA: 0x000BFC0D File Offset: 0x000BDE0D
		[XmlIgnore]
		public XmlQualifiedName QualifiedName
		{
			get
			{
				return this.qualifiedName;
			}
		}

		/// <summary>Gets an <see cref="T:System.Xml.Schema.XmlSchemaType" /> object representing the type of the element based on the <see cref="P:System.Xml.Schema.XmlSchemaElement.SchemaType" /> or <see cref="P:System.Xml.Schema.XmlSchemaElement.SchemaTypeName" /> values of the element.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSchemaType" /> object.</returns>
		// Token: 0x170007F5 RID: 2037
		// (get) Token: 0x060020F1 RID: 8433 RVA: 0x000BFC15 File Offset: 0x000BDE15
		[XmlIgnore]
		public XmlSchemaType ElementSchemaType
		{
			get
			{
				return this.elementType;
			}
		}

		/// <summary>Gets the post-compilation value of the Block property.</summary>
		/// <returns>The post-compilation value of the Block property. The default is the BlockDefault value on the schema element.</returns>
		// Token: 0x170007F6 RID: 2038
		// (get) Token: 0x060020F2 RID: 8434 RVA: 0x000BFC1D File Offset: 0x000BDE1D
		[XmlIgnore]
		public XmlSchemaDerivationMethod BlockResolved
		{
			get
			{
				return this.blockResolved;
			}
		}

		/// <summary>Gets the post-compilation value of the Final property.</summary>
		/// <returns>The post-compilation value of the Final property. Default value is the FinalDefault value on the schema element.</returns>
		// Token: 0x170007F7 RID: 2039
		// (get) Token: 0x060020F3 RID: 8435 RVA: 0x000BFC25 File Offset: 0x000BDE25
		[XmlIgnore]
		public XmlSchemaDerivationMethod FinalResolved
		{
			get
			{
				return this.finalResolved;
			}
		}

		// Token: 0x060020F4 RID: 8436 RVA: 0x000BFC2D File Offset: 0x000BDE2D
		internal void SetQualifiedName(XmlQualifiedName value)
		{
			this.qualifiedName = value;
		}

		// Token: 0x060020F5 RID: 8437 RVA: 0x000BFC36 File Offset: 0x000BDE36
		internal void SetElementType(XmlSchemaType value)
		{
			this.elementType = value;
		}

		// Token: 0x060020F6 RID: 8438 RVA: 0x000BFC3F File Offset: 0x000BDE3F
		internal void SetBlockResolved(XmlSchemaDerivationMethod value)
		{
			this.blockResolved = value;
		}

		// Token: 0x060020F7 RID: 8439 RVA: 0x000BFC48 File Offset: 0x000BDE48
		internal void SetFinalResolved(XmlSchemaDerivationMethod value)
		{
			this.finalResolved = value;
		}

		// Token: 0x170007F8 RID: 2040
		// (get) Token: 0x060020F8 RID: 8440 RVA: 0x000BFC51 File Offset: 0x000BDE51
		[XmlIgnore]
		internal bool HasDefault
		{
			get
			{
				return this.defaultValue != null && this.defaultValue.Length > 0;
			}
		}

		// Token: 0x170007F9 RID: 2041
		// (get) Token: 0x060020F9 RID: 8441 RVA: 0x000BFC6B File Offset: 0x000BDE6B
		internal bool HasConstraints
		{
			get
			{
				return this.constraints != null && this.constraints.Count > 0;
			}
		}

		// Token: 0x170007FA RID: 2042
		// (get) Token: 0x060020FA RID: 8442 RVA: 0x000BFC85 File Offset: 0x000BDE85
		// (set) Token: 0x060020FB RID: 8443 RVA: 0x000BFC8D File Offset: 0x000BDE8D
		internal bool IsLocalTypeDerivationChecked
		{
			get
			{
				return this.isLocalTypeDerivationChecked;
			}
			set
			{
				this.isLocalTypeDerivationChecked = value;
			}
		}

		// Token: 0x170007FB RID: 2043
		// (get) Token: 0x060020FC RID: 8444 RVA: 0x000BFC96 File Offset: 0x000BDE96
		// (set) Token: 0x060020FD RID: 8445 RVA: 0x000BFC9E File Offset: 0x000BDE9E
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

		// Token: 0x170007FC RID: 2044
		// (get) Token: 0x060020FE RID: 8446 RVA: 0x000BFCA7 File Offset: 0x000BDEA7
		// (set) Token: 0x060020FF RID: 8447 RVA: 0x000BFCAF File Offset: 0x000BDEAF
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

		// Token: 0x170007FD RID: 2045
		// (get) Token: 0x06002100 RID: 8448 RVA: 0x000BFCB8 File Offset: 0x000BDEB8
		[XmlIgnore]
		internal override string NameString
		{
			get
			{
				return this.qualifiedName.ToString();
			}
		}

		// Token: 0x06002101 RID: 8449 RVA: 0x000BFCC5 File Offset: 0x000BDEC5
		internal override XmlSchemaObject Clone()
		{
			return this.Clone(null);
		}

		// Token: 0x06002102 RID: 8450 RVA: 0x000BFCD0 File Offset: 0x000BDED0
		internal XmlSchemaObject Clone(XmlSchema parentSchema)
		{
			XmlSchemaElement xmlSchemaElement = (XmlSchemaElement)base.MemberwiseClone();
			xmlSchemaElement.refName = this.refName.Clone();
			xmlSchemaElement.substitutionGroup = this.substitutionGroup.Clone();
			xmlSchemaElement.typeName = this.typeName.Clone();
			xmlSchemaElement.qualifiedName = this.qualifiedName.Clone();
			XmlSchemaComplexType xmlSchemaComplexType = this.type as XmlSchemaComplexType;
			if (xmlSchemaComplexType != null && xmlSchemaComplexType.QualifiedName.IsEmpty)
			{
				xmlSchemaElement.type = (XmlSchemaType)xmlSchemaComplexType.Clone(parentSchema);
			}
			xmlSchemaElement.constraints = null;
			return xmlSchemaElement;
		}

		// Token: 0x04000F54 RID: 3924
		private bool isAbstract;

		// Token: 0x04000F55 RID: 3925
		private bool hasAbstractAttribute;

		// Token: 0x04000F56 RID: 3926
		private bool isNillable;

		// Token: 0x04000F57 RID: 3927
		private bool hasNillableAttribute;

		// Token: 0x04000F58 RID: 3928
		private bool isLocalTypeDerivationChecked;

		// Token: 0x04000F59 RID: 3929
		private XmlSchemaDerivationMethod block = XmlSchemaDerivationMethod.None;

		// Token: 0x04000F5A RID: 3930
		private XmlSchemaDerivationMethod final = XmlSchemaDerivationMethod.None;

		// Token: 0x04000F5B RID: 3931
		private XmlSchemaForm form;

		// Token: 0x04000F5C RID: 3932
		private string defaultValue;

		// Token: 0x04000F5D RID: 3933
		private string fixedValue;

		// Token: 0x04000F5E RID: 3934
		private string name;

		// Token: 0x04000F5F RID: 3935
		private XmlQualifiedName refName = XmlQualifiedName.Empty;

		// Token: 0x04000F60 RID: 3936
		private XmlQualifiedName substitutionGroup = XmlQualifiedName.Empty;

		// Token: 0x04000F61 RID: 3937
		private XmlQualifiedName typeName = XmlQualifiedName.Empty;

		// Token: 0x04000F62 RID: 3938
		private XmlSchemaType type;

		// Token: 0x04000F63 RID: 3939
		private XmlQualifiedName qualifiedName = XmlQualifiedName.Empty;

		// Token: 0x04000F64 RID: 3940
		private XmlSchemaType elementType;

		// Token: 0x04000F65 RID: 3941
		private XmlSchemaDerivationMethod blockResolved;

		// Token: 0x04000F66 RID: 3942
		private XmlSchemaDerivationMethod finalResolved;

		// Token: 0x04000F67 RID: 3943
		private XmlSchemaObjectCollection constraints;

		// Token: 0x04000F68 RID: 3944
		private SchemaElementDecl elementDecl;
	}
}
