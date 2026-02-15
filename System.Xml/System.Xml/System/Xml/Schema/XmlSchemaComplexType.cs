using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Represents the complexType element from XML Schema as specified by the World Wide Web Consortium (W3C). This class defines a complex type that determines the set of attributes and content of an element.</summary>
	// Token: 0x020002C6 RID: 710
	public class XmlSchemaComplexType : XmlSchemaType
	{
		// Token: 0x06002082 RID: 8322 RVA: 0x000BEC68 File Offset: 0x000BCE68
		static XmlSchemaComplexType()
		{
			XmlSchemaComplexType.untypedAnyType.SetQualifiedName(new XmlQualifiedName("untypedAny", "http://www.w3.org/2003/11/xpath-datatypes"));
			XmlSchemaComplexType.untypedAnyType.IsMixed = true;
			XmlSchemaComplexType.untypedAnyType.SetContentTypeParticle(XmlSchemaComplexType.anyTypeLax.ContentTypeParticle);
			XmlSchemaComplexType.untypedAnyType.SetContentType(XmlSchemaContentType.Mixed);
			XmlSchemaComplexType.untypedAnyType.ElementDecl = SchemaElementDecl.CreateAnyTypeElementDecl();
			XmlSchemaComplexType.untypedAnyType.ElementDecl.SchemaType = XmlSchemaComplexType.untypedAnyType;
			XmlSchemaComplexType.untypedAnyType.ElementDecl.ContentValidator = XmlSchemaComplexType.AnyTypeContentValidator;
		}

		// Token: 0x06002083 RID: 8323 RVA: 0x000BED10 File Offset: 0x000BCF10
		private static XmlSchemaComplexType CreateAnyType(XmlSchemaContentProcessing processContents)
		{
			XmlSchemaComplexType xmlSchemaComplexType = new XmlSchemaComplexType();
			xmlSchemaComplexType.SetQualifiedName(DatatypeImplementation.QnAnyType);
			XmlSchemaAny xmlSchemaAny = new XmlSchemaAny();
			xmlSchemaAny.MinOccurs = 0m;
			xmlSchemaAny.MaxOccurs = decimal.MaxValue;
			xmlSchemaAny.ProcessContents = processContents;
			xmlSchemaAny.BuildNamespaceList(null);
			xmlSchemaComplexType.SetContentTypeParticle(new XmlSchemaSequence
			{
				Items = { xmlSchemaAny }
			});
			xmlSchemaComplexType.SetContentType(XmlSchemaContentType.Mixed);
			xmlSchemaComplexType.ElementDecl = SchemaElementDecl.CreateAnyTypeElementDecl();
			xmlSchemaComplexType.ElementDecl.SchemaType = xmlSchemaComplexType;
			ParticleContentValidator particleContentValidator = new ParticleContentValidator(XmlSchemaContentType.Mixed);
			particleContentValidator.Start();
			particleContentValidator.OpenGroup();
			particleContentValidator.AddNamespaceList(xmlSchemaAny.NamespaceList, xmlSchemaAny);
			particleContentValidator.AddStar();
			particleContentValidator.CloseGroup();
			ContentValidator contentValidator = particleContentValidator.Finish(true);
			xmlSchemaComplexType.ElementDecl.ContentValidator = contentValidator;
			XmlSchemaAnyAttribute xmlSchemaAnyAttribute = new XmlSchemaAnyAttribute();
			xmlSchemaAnyAttribute.ProcessContents = processContents;
			xmlSchemaAnyAttribute.BuildNamespaceList(null);
			xmlSchemaComplexType.SetAttributeWildcard(xmlSchemaAnyAttribute);
			xmlSchemaComplexType.ElementDecl.AnyAttribute = xmlSchemaAnyAttribute;
			return xmlSchemaComplexType;
		}

		// Token: 0x170007C4 RID: 1988
		// (get) Token: 0x06002085 RID: 8325 RVA: 0x000BEE1E File Offset: 0x000BD01E
		[XmlIgnore]
		internal static XmlSchemaComplexType AnyType
		{
			get
			{
				return XmlSchemaComplexType.anyTypeLax;
			}
		}

		// Token: 0x170007C5 RID: 1989
		// (get) Token: 0x06002086 RID: 8326 RVA: 0x000BEE25 File Offset: 0x000BD025
		[XmlIgnore]
		internal static XmlSchemaComplexType UntypedAnyType
		{
			get
			{
				return XmlSchemaComplexType.untypedAnyType;
			}
		}

		// Token: 0x170007C6 RID: 1990
		// (get) Token: 0x06002087 RID: 8327 RVA: 0x000BEE2C File Offset: 0x000BD02C
		internal static ContentValidator AnyTypeContentValidator
		{
			get
			{
				return XmlSchemaComplexType.anyTypeLax.ElementDecl.ContentValidator;
			}
		}

		/// <summary>Gets or sets the information that determines if the complexType element can be used in the instance document.</summary>
		/// <returns>If true, an element cannot use this complexType element directly and must use a complex type that is derived from this complexType element. The default is false.Optional.</returns>
		// Token: 0x170007C7 RID: 1991
		// (get) Token: 0x06002088 RID: 8328 RVA: 0x000BEE3D File Offset: 0x000BD03D
		// (set) Token: 0x06002089 RID: 8329 RVA: 0x000BEE4A File Offset: 0x000BD04A
		[XmlAttribute("abstract")]
		[DefaultValue(false)]
		public bool IsAbstract
		{
			get
			{
				return (this.pvFlags & 4) > 0;
			}
			set
			{
				if (value)
				{
					this.pvFlags |= 4;
					return;
				}
				this.pvFlags = (byte)((int)this.pvFlags & -5);
			}
		}

		/// <summary>Gets or sets the block attribute.</summary>
		/// <returns>The block attribute prevents a complex type from being used in the specified type of derivation. The default is XmlSchemaDerivationMethod.None.Optional.</returns>
		// Token: 0x170007C8 RID: 1992
		// (get) Token: 0x0600208A RID: 8330 RVA: 0x000BEE6F File Offset: 0x000BD06F
		// (set) Token: 0x0600208B RID: 8331 RVA: 0x000BEE77 File Offset: 0x000BD077
		[XmlAttribute("block")]
		[DefaultValue(XmlSchemaDerivationMethod.None)]
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

		/// <summary>Gets or sets information that determines if the complex type has a mixed content model (markup within the content).</summary>
		/// <returns>true, if character data can appear between child elements of this complex type; otherwise, false. The default is false.Optional.</returns>
		// Token: 0x170007C9 RID: 1993
		// (get) Token: 0x0600208C RID: 8332 RVA: 0x000BEE80 File Offset: 0x000BD080
		// (set) Token: 0x0600208D RID: 8333 RVA: 0x000BEE8D File Offset: 0x000BD08D
		[XmlAttribute("mixed")]
		[DefaultValue(false)]
		public override bool IsMixed
		{
			get
			{
				return (this.pvFlags & 2) > 0;
			}
			set
			{
				if (value)
				{
					this.pvFlags |= 2;
					return;
				}
				this.pvFlags = (byte)((int)this.pvFlags & -3);
			}
		}

		/// <summary>Gets or sets the post-compilation <see cref="T:System.Xml.Schema.XmlSchemaContentModel" /> of this complex type.</summary>
		/// <returns>The content model type that is one of the <see cref="T:System.Xml.Schema.XmlSchemaSimpleContent" /> or <see cref="T:System.Xml.Schema.XmlSchemaComplexContent" /> classes.</returns>
		// Token: 0x170007CA RID: 1994
		// (get) Token: 0x0600208E RID: 8334 RVA: 0x000BEEB2 File Offset: 0x000BD0B2
		// (set) Token: 0x0600208F RID: 8335 RVA: 0x000BEEBA File Offset: 0x000BD0BA
		[XmlElement("complexContent", typeof(XmlSchemaComplexContent))]
		[XmlElement("simpleContent", typeof(XmlSchemaSimpleContent))]
		public XmlSchemaContentModel ContentModel
		{
			get
			{
				return this.contentModel;
			}
			set
			{
				this.contentModel = value;
			}
		}

		/// <summary>Gets or sets the compositor type as one of the <see cref="T:System.Xml.Schema.XmlSchemaGroupRef" />, <see cref="T:System.Xml.Schema.XmlSchemaChoice" />, <see cref="T:System.Xml.Schema.XmlSchemaAll" />, or <see cref="T:System.Xml.Schema.XmlSchemaSequence" /> classes.</summary>
		/// <returns>The compositor type.</returns>
		// Token: 0x170007CB RID: 1995
		// (get) Token: 0x06002090 RID: 8336 RVA: 0x000BEEC3 File Offset: 0x000BD0C3
		// (set) Token: 0x06002091 RID: 8337 RVA: 0x000BEECB File Offset: 0x000BD0CB
		[XmlElement("choice", typeof(XmlSchemaChoice))]
		[XmlElement("sequence", typeof(XmlSchemaSequence))]
		[XmlElement("group", typeof(XmlSchemaGroupRef))]
		[XmlElement("all", typeof(XmlSchemaAll))]
		public XmlSchemaParticle Particle
		{
			get
			{
				return this.particle;
			}
			set
			{
				this.particle = value;
			}
		}

		/// <summary>Gets the collection of attributes for the complex type.</summary>
		/// <returns>Contains <see cref="T:System.Xml.Schema.XmlSchemaAttribute" /> and <see cref="T:System.Xml.Schema.XmlSchemaAttributeGroupRef" /> classes.</returns>
		// Token: 0x170007CC RID: 1996
		// (get) Token: 0x06002092 RID: 8338 RVA: 0x000BEED4 File Offset: 0x000BD0D4
		[XmlElement("attribute", typeof(XmlSchemaAttribute))]
		[XmlElement("attributeGroup", typeof(XmlSchemaAttributeGroupRef))]
		public XmlSchemaObjectCollection Attributes
		{
			get
			{
				if (this.attributes == null)
				{
					this.attributes = new XmlSchemaObjectCollection();
				}
				return this.attributes;
			}
		}

		/// <summary>Gets or sets the value for the <see cref="T:System.Xml.Schema.XmlSchemaAnyAttribute" /> component of the complex type.</summary>
		/// <returns>The <see cref="T:System.Xml.Schema.XmlSchemaAnyAttribute" /> component of the complex type.</returns>
		// Token: 0x170007CD RID: 1997
		// (get) Token: 0x06002093 RID: 8339 RVA: 0x000BEEEF File Offset: 0x000BD0EF
		// (set) Token: 0x06002094 RID: 8340 RVA: 0x000BEEF7 File Offset: 0x000BD0F7
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

		/// <summary>Gets the content model of the complex type which holds the post-compilation value.</summary>
		/// <returns>The post-compilation value of the content model for the complex type.</returns>
		// Token: 0x170007CE RID: 1998
		// (get) Token: 0x06002095 RID: 8341 RVA: 0x000BEF00 File Offset: 0x000BD100
		[XmlIgnore]
		public XmlSchemaContentType ContentType
		{
			get
			{
				return base.SchemaContentType;
			}
		}

		/// <summary>Gets the particle that holds the post-compilation value of the <see cref="P:System.Xml.Schema.XmlSchemaComplexType.ContentType" /> particle.</summary>
		/// <returns>The particle for the content type. The post-compilation value of the <see cref="P:System.Xml.Schema.XmlSchemaComplexType.ContentType" /> particle.</returns>
		// Token: 0x170007CF RID: 1999
		// (get) Token: 0x06002096 RID: 8342 RVA: 0x000BEF08 File Offset: 0x000BD108
		[XmlIgnore]
		public XmlSchemaParticle ContentTypeParticle
		{
			get
			{
				return this.contentTypeParticle;
			}
		}

		/// <summary>Gets the value after the type has been compiled to the post-schema-validation information set (infoset). This value indicates how the type is enforced when xsi:type is used in the instance document.</summary>
		/// <returns>The post-schema-validated infoset value. The default is BlockDefault value on the schema element.</returns>
		// Token: 0x170007D0 RID: 2000
		// (get) Token: 0x06002097 RID: 8343 RVA: 0x000BEF10 File Offset: 0x000BD110
		[XmlIgnore]
		public XmlSchemaDerivationMethod BlockResolved
		{
			get
			{
				return this.blockResolved;
			}
		}

		/// <summary>Gets the collection of all the complied attributes of this complex type and its base types.</summary>
		/// <returns>The collection of all the attributes from this complex type and its base types. The post-compilation value of the AttributeUses property.</returns>
		// Token: 0x170007D1 RID: 2001
		// (get) Token: 0x06002098 RID: 8344 RVA: 0x000BEF18 File Offset: 0x000BD118
		[XmlIgnore]
		public XmlSchemaObjectTable AttributeUses
		{
			get
			{
				if (this.attributeUses == null)
				{
					this.attributeUses = new XmlSchemaObjectTable();
				}
				return this.attributeUses;
			}
		}

		/// <summary>Gets the post-compilation value for anyAttribute for this complex type and its base type(s).</summary>
		/// <returns>The post-compilation value of the anyAttribute element.</returns>
		// Token: 0x170007D2 RID: 2002
		// (get) Token: 0x06002099 RID: 8345 RVA: 0x000BEF33 File Offset: 0x000BD133
		[XmlIgnore]
		public XmlSchemaAnyAttribute AttributeWildcard
		{
			get
			{
				return this.attributeWildcard;
			}
		}

		// Token: 0x170007D3 RID: 2003
		// (get) Token: 0x0600209A RID: 8346 RVA: 0x000BEF3B File Offset: 0x000BD13B
		[XmlIgnore]
		internal XmlSchemaObjectTable LocalElements
		{
			get
			{
				if (this.localElements == null)
				{
					this.localElements = new XmlSchemaObjectTable();
				}
				return this.localElements;
			}
		}

		// Token: 0x0600209B RID: 8347 RVA: 0x000BEF56 File Offset: 0x000BD156
		internal void SetContentTypeParticle(XmlSchemaParticle value)
		{
			this.contentTypeParticle = value;
		}

		// Token: 0x0600209C RID: 8348 RVA: 0x000BEF5F File Offset: 0x000BD15F
		internal void SetBlockResolved(XmlSchemaDerivationMethod value)
		{
			this.blockResolved = value;
		}

		// Token: 0x0600209D RID: 8349 RVA: 0x000BEF68 File Offset: 0x000BD168
		internal void SetAttributeWildcard(XmlSchemaAnyAttribute value)
		{
			this.attributeWildcard = value;
		}

		// Token: 0x170007D4 RID: 2004
		// (set) Token: 0x0600209E RID: 8350 RVA: 0x000BEF71 File Offset: 0x000BD171
		internal bool HasWildCard
		{
			set
			{
				if (value)
				{
					this.pvFlags |= 1;
					return;
				}
				this.pvFlags = (byte)((int)this.pvFlags & -2);
			}
		}

		// Token: 0x170007D5 RID: 2005
		// (get) Token: 0x0600209F RID: 8351 RVA: 0x000BEF98 File Offset: 0x000BD198
		internal override XmlQualifiedName DerivedFrom
		{
			get
			{
				if (this.contentModel == null)
				{
					return XmlQualifiedName.Empty;
				}
				if (this.contentModel.Content is XmlSchemaComplexContentRestriction)
				{
					return ((XmlSchemaComplexContentRestriction)this.contentModel.Content).BaseTypeName;
				}
				if (this.contentModel.Content is XmlSchemaComplexContentExtension)
				{
					return ((XmlSchemaComplexContentExtension)this.contentModel.Content).BaseTypeName;
				}
				if (this.contentModel.Content is XmlSchemaSimpleContentRestriction)
				{
					return ((XmlSchemaSimpleContentRestriction)this.contentModel.Content).BaseTypeName;
				}
				if (this.contentModel.Content is XmlSchemaSimpleContentExtension)
				{
					return ((XmlSchemaSimpleContentExtension)this.contentModel.Content).BaseTypeName;
				}
				return XmlQualifiedName.Empty;
			}
		}

		// Token: 0x060020A0 RID: 8352 RVA: 0x000BF058 File Offset: 0x000BD258
		internal void SetAttributes(XmlSchemaObjectCollection newAttributes)
		{
			this.attributes = newAttributes;
		}

		// Token: 0x060020A1 RID: 8353 RVA: 0x000BF064 File Offset: 0x000BD264
		internal bool ContainsIdAttribute(bool findAll)
		{
			int num = 0;
			foreach (object obj in this.AttributeUses.Values)
			{
				XmlSchemaAttribute xmlSchemaAttribute = (XmlSchemaAttribute)obj;
				if (xmlSchemaAttribute.Use != XmlSchemaUse.Prohibited)
				{
					XmlSchemaDatatype datatype = xmlSchemaAttribute.Datatype;
					if (datatype != null && datatype.TypeCode == XmlTypeCode.Id)
					{
						num++;
						if (num > 1)
						{
							break;
						}
					}
				}
			}
			if (!findAll)
			{
				return num > 0;
			}
			return num > 1;
		}

		// Token: 0x060020A2 RID: 8354 RVA: 0x000BF0F4 File Offset: 0x000BD2F4
		internal override XmlSchemaObject Clone()
		{
			return this.Clone(null);
		}

		// Token: 0x060020A3 RID: 8355 RVA: 0x000BF100 File Offset: 0x000BD300
		internal XmlSchemaObject Clone(XmlSchema parentSchema)
		{
			XmlSchemaComplexType xmlSchemaComplexType = (XmlSchemaComplexType)base.MemberwiseClone();
			if (xmlSchemaComplexType.ContentModel != null)
			{
				XmlSchemaSimpleContent xmlSchemaSimpleContent = xmlSchemaComplexType.ContentModel as XmlSchemaSimpleContent;
				if (xmlSchemaSimpleContent != null)
				{
					XmlSchemaSimpleContent xmlSchemaSimpleContent2 = (XmlSchemaSimpleContent)xmlSchemaSimpleContent.Clone();
					XmlSchemaSimpleContentExtension xmlSchemaSimpleContentExtension = xmlSchemaSimpleContent.Content as XmlSchemaSimpleContentExtension;
					if (xmlSchemaSimpleContentExtension != null)
					{
						XmlSchemaSimpleContentExtension xmlSchemaSimpleContentExtension2 = (XmlSchemaSimpleContentExtension)xmlSchemaSimpleContentExtension.Clone();
						xmlSchemaSimpleContentExtension2.BaseTypeName = xmlSchemaSimpleContentExtension.BaseTypeName.Clone();
						xmlSchemaSimpleContentExtension2.SetAttributes(XmlSchemaComplexType.CloneAttributes(xmlSchemaSimpleContentExtension.Attributes));
						xmlSchemaSimpleContent2.Content = xmlSchemaSimpleContentExtension2;
					}
					else
					{
						XmlSchemaSimpleContentRestriction xmlSchemaSimpleContentRestriction = (XmlSchemaSimpleContentRestriction)xmlSchemaSimpleContent.Content;
						XmlSchemaSimpleContentRestriction xmlSchemaSimpleContentRestriction2 = (XmlSchemaSimpleContentRestriction)xmlSchemaSimpleContentRestriction.Clone();
						xmlSchemaSimpleContentRestriction2.BaseTypeName = xmlSchemaSimpleContentRestriction.BaseTypeName.Clone();
						xmlSchemaSimpleContentRestriction2.SetAttributes(XmlSchemaComplexType.CloneAttributes(xmlSchemaSimpleContentRestriction.Attributes));
						xmlSchemaSimpleContent2.Content = xmlSchemaSimpleContentRestriction2;
					}
					xmlSchemaComplexType.ContentModel = xmlSchemaSimpleContent2;
				}
				else
				{
					XmlSchemaComplexContent xmlSchemaComplexContent = (XmlSchemaComplexContent)xmlSchemaComplexType.ContentModel;
					XmlSchemaComplexContent xmlSchemaComplexContent2 = (XmlSchemaComplexContent)xmlSchemaComplexContent.Clone();
					XmlSchemaComplexContentExtension xmlSchemaComplexContentExtension = xmlSchemaComplexContent.Content as XmlSchemaComplexContentExtension;
					if (xmlSchemaComplexContentExtension != null)
					{
						XmlSchemaComplexContentExtension xmlSchemaComplexContentExtension2 = (XmlSchemaComplexContentExtension)xmlSchemaComplexContentExtension.Clone();
						xmlSchemaComplexContentExtension2.BaseTypeName = xmlSchemaComplexContentExtension.BaseTypeName.Clone();
						xmlSchemaComplexContentExtension2.SetAttributes(XmlSchemaComplexType.CloneAttributes(xmlSchemaComplexContentExtension.Attributes));
						if (XmlSchemaComplexType.HasParticleRef(xmlSchemaComplexContentExtension.Particle, parentSchema))
						{
							xmlSchemaComplexContentExtension2.Particle = XmlSchemaComplexType.CloneParticle(xmlSchemaComplexContentExtension.Particle, parentSchema);
						}
						xmlSchemaComplexContent2.Content = xmlSchemaComplexContentExtension2;
					}
					else
					{
						XmlSchemaComplexContentRestriction xmlSchemaComplexContentRestriction = xmlSchemaComplexContent.Content as XmlSchemaComplexContentRestriction;
						XmlSchemaComplexContentRestriction xmlSchemaComplexContentRestriction2 = (XmlSchemaComplexContentRestriction)xmlSchemaComplexContentRestriction.Clone();
						xmlSchemaComplexContentRestriction2.BaseTypeName = xmlSchemaComplexContentRestriction.BaseTypeName.Clone();
						xmlSchemaComplexContentRestriction2.SetAttributes(XmlSchemaComplexType.CloneAttributes(xmlSchemaComplexContentRestriction.Attributes));
						if (XmlSchemaComplexType.HasParticleRef(xmlSchemaComplexContentRestriction2.Particle, parentSchema))
						{
							xmlSchemaComplexContentRestriction2.Particle = XmlSchemaComplexType.CloneParticle(xmlSchemaComplexContentRestriction2.Particle, parentSchema);
						}
						xmlSchemaComplexContent2.Content = xmlSchemaComplexContentRestriction2;
					}
					xmlSchemaComplexType.ContentModel = xmlSchemaComplexContent2;
				}
			}
			else
			{
				if (XmlSchemaComplexType.HasParticleRef(xmlSchemaComplexType.Particle, parentSchema))
				{
					xmlSchemaComplexType.Particle = XmlSchemaComplexType.CloneParticle(xmlSchemaComplexType.Particle, parentSchema);
				}
				xmlSchemaComplexType.SetAttributes(XmlSchemaComplexType.CloneAttributes(xmlSchemaComplexType.Attributes));
			}
			xmlSchemaComplexType.ClearCompiledState();
			return xmlSchemaComplexType;
		}

		// Token: 0x060020A4 RID: 8356 RVA: 0x000BF320 File Offset: 0x000BD520
		private void ClearCompiledState()
		{
			this.attributeUses = null;
			this.localElements = null;
			this.attributeWildcard = null;
			this.contentTypeParticle = XmlSchemaParticle.Empty;
			this.blockResolved = XmlSchemaDerivationMethod.None;
		}

		// Token: 0x060020A5 RID: 8357 RVA: 0x000BF350 File Offset: 0x000BD550
		internal static XmlSchemaObjectCollection CloneAttributes(XmlSchemaObjectCollection attributes)
		{
			if (XmlSchemaComplexType.HasAttributeQNameRef(attributes))
			{
				XmlSchemaObjectCollection xmlSchemaObjectCollection = attributes.Clone();
				for (int i = 0; i < attributes.Count; i++)
				{
					XmlSchemaObject xmlSchemaObject = attributes[i];
					XmlSchemaAttributeGroupRef xmlSchemaAttributeGroupRef = xmlSchemaObject as XmlSchemaAttributeGroupRef;
					if (xmlSchemaAttributeGroupRef != null)
					{
						XmlSchemaAttributeGroupRef xmlSchemaAttributeGroupRef2 = (XmlSchemaAttributeGroupRef)xmlSchemaAttributeGroupRef.Clone();
						xmlSchemaAttributeGroupRef2.RefName = xmlSchemaAttributeGroupRef.RefName.Clone();
						xmlSchemaObjectCollection[i] = xmlSchemaAttributeGroupRef2;
					}
					else
					{
						XmlSchemaAttribute xmlSchemaAttribute = xmlSchemaObject as XmlSchemaAttribute;
						if (!xmlSchemaAttribute.RefName.IsEmpty || !xmlSchemaAttribute.SchemaTypeName.IsEmpty)
						{
							xmlSchemaObjectCollection[i] = xmlSchemaAttribute.Clone();
						}
					}
				}
				return xmlSchemaObjectCollection;
			}
			return attributes;
		}

		// Token: 0x060020A6 RID: 8358 RVA: 0x000BF3F8 File Offset: 0x000BD5F8
		private static XmlSchemaObjectCollection CloneGroupBaseParticles(XmlSchemaObjectCollection groupBaseParticles, XmlSchema parentSchema)
		{
			XmlSchemaObjectCollection xmlSchemaObjectCollection = groupBaseParticles.Clone();
			for (int i = 0; i < groupBaseParticles.Count; i++)
			{
				XmlSchemaParticle xmlSchemaParticle = (XmlSchemaParticle)groupBaseParticles[i];
				xmlSchemaObjectCollection[i] = XmlSchemaComplexType.CloneParticle(xmlSchemaParticle, parentSchema);
			}
			return xmlSchemaObjectCollection;
		}

		// Token: 0x060020A7 RID: 8359 RVA: 0x000BF43C File Offset: 0x000BD63C
		internal static XmlSchemaParticle CloneParticle(XmlSchemaParticle particle, XmlSchema parentSchema)
		{
			XmlSchemaGroupBase xmlSchemaGroupBase = particle as XmlSchemaGroupBase;
			if (xmlSchemaGroupBase != null)
			{
				XmlSchemaObjectCollection xmlSchemaObjectCollection = XmlSchemaComplexType.CloneGroupBaseParticles(xmlSchemaGroupBase.Items, parentSchema);
				XmlSchemaGroupBase xmlSchemaGroupBase2 = (XmlSchemaGroupBase)xmlSchemaGroupBase.Clone();
				xmlSchemaGroupBase2.SetItems(xmlSchemaObjectCollection);
				return xmlSchemaGroupBase2;
			}
			if (particle is XmlSchemaGroupRef)
			{
				XmlSchemaGroupRef xmlSchemaGroupRef = (XmlSchemaGroupRef)particle.Clone();
				xmlSchemaGroupRef.RefName = xmlSchemaGroupRef.RefName.Clone();
				return xmlSchemaGroupRef;
			}
			XmlSchemaElement xmlSchemaElement = particle as XmlSchemaElement;
			if (xmlSchemaElement != null && (!xmlSchemaElement.RefName.IsEmpty || !xmlSchemaElement.SchemaTypeName.IsEmpty || XmlSchemaComplexType.GetResolvedElementForm(parentSchema, xmlSchemaElement) == XmlSchemaForm.Qualified))
			{
				return (XmlSchemaElement)xmlSchemaElement.Clone(parentSchema);
			}
			return particle;
		}

		// Token: 0x060020A8 RID: 8360 RVA: 0x000BF4D4 File Offset: 0x000BD6D4
		private static XmlSchemaForm GetResolvedElementForm(XmlSchema parentSchema, XmlSchemaElement element)
		{
			if (element.Form == XmlSchemaForm.None && parentSchema != null)
			{
				return parentSchema.ElementFormDefault;
			}
			return element.Form;
		}

		// Token: 0x060020A9 RID: 8361 RVA: 0x000BF4F0 File Offset: 0x000BD6F0
		internal static bool HasParticleRef(XmlSchemaParticle particle, XmlSchema parentSchema)
		{
			XmlSchemaGroupBase xmlSchemaGroupBase = particle as XmlSchemaGroupBase;
			if (xmlSchemaGroupBase != null)
			{
				bool flag = false;
				int num = 0;
				while (num < xmlSchemaGroupBase.Items.Count && !flag)
				{
					XmlSchemaParticle xmlSchemaParticle = (XmlSchemaParticle)xmlSchemaGroupBase.Items[num++];
					if (xmlSchemaParticle is XmlSchemaGroupRef)
					{
						flag = true;
					}
					else
					{
						XmlSchemaElement xmlSchemaElement = xmlSchemaParticle as XmlSchemaElement;
						flag = (xmlSchemaElement != null && (!xmlSchemaElement.RefName.IsEmpty || !xmlSchemaElement.SchemaTypeName.IsEmpty || XmlSchemaComplexType.GetResolvedElementForm(parentSchema, xmlSchemaElement) == XmlSchemaForm.Qualified)) || XmlSchemaComplexType.HasParticleRef(xmlSchemaParticle, parentSchema);
					}
				}
				return flag;
			}
			return particle is XmlSchemaGroupRef;
		}

		// Token: 0x060020AA RID: 8362 RVA: 0x000BF58C File Offset: 0x000BD78C
		internal static bool HasAttributeQNameRef(XmlSchemaObjectCollection attributes)
		{
			for (int i = 0; i < attributes.Count; i++)
			{
				if (attributes[i] is XmlSchemaAttributeGroupRef)
				{
					return true;
				}
				XmlSchemaAttribute xmlSchemaAttribute = attributes[i] as XmlSchemaAttribute;
				if (!xmlSchemaAttribute.RefName.IsEmpty || !xmlSchemaAttribute.SchemaTypeName.IsEmpty)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x04000F2F RID: 3887
		private XmlSchemaDerivationMethod block = XmlSchemaDerivationMethod.None;

		// Token: 0x04000F30 RID: 3888
		private XmlSchemaContentModel contentModel;

		// Token: 0x04000F31 RID: 3889
		private XmlSchemaParticle particle;

		// Token: 0x04000F32 RID: 3890
		private XmlSchemaObjectCollection attributes;

		// Token: 0x04000F33 RID: 3891
		private XmlSchemaAnyAttribute anyAttribute;

		// Token: 0x04000F34 RID: 3892
		private XmlSchemaParticle contentTypeParticle = XmlSchemaParticle.Empty;

		// Token: 0x04000F35 RID: 3893
		private XmlSchemaDerivationMethod blockResolved;

		// Token: 0x04000F36 RID: 3894
		private XmlSchemaObjectTable localElements;

		// Token: 0x04000F37 RID: 3895
		private XmlSchemaObjectTable attributeUses;

		// Token: 0x04000F38 RID: 3896
		private XmlSchemaAnyAttribute attributeWildcard;

		// Token: 0x04000F39 RID: 3897
		private static XmlSchemaComplexType anyTypeLax = XmlSchemaComplexType.CreateAnyType(XmlSchemaContentProcessing.Lax);

		// Token: 0x04000F3A RID: 3898
		private static XmlSchemaComplexType anyTypeSkip = XmlSchemaComplexType.CreateAnyType(XmlSchemaContentProcessing.Skip);

		// Token: 0x04000F3B RID: 3899
		private static XmlSchemaComplexType untypedAnyType = new XmlSchemaComplexType();

		// Token: 0x04000F3C RID: 3900
		private byte pvFlags;
	}
}
