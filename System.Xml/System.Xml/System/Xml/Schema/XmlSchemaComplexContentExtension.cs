using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Represents the extension element from XML Schema as specified by the World Wide Web Consortium (W3C). This class is for complex types with complex content model derived by extension. It extends the complex type by adding attributes or elements.</summary>
	// Token: 0x020002C4 RID: 708
	public class XmlSchemaComplexContentExtension : XmlSchemaContent
	{
		/// <summary>Gets or sets the name of the complex type from which this type is derived by extension.</summary>
		/// <returns>The name of the complex type from which this type is derived by extension.</returns>
		// Token: 0x170007BC RID: 1980
		// (get) Token: 0x06002070 RID: 8304 RVA: 0x000BEB83 File Offset: 0x000BCD83
		// (set) Token: 0x06002071 RID: 8305 RVA: 0x000BEB8B File Offset: 0x000BCD8B
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

		/// <summary>Gets or sets one of the <see cref="T:System.Xml.Schema.XmlSchemaGroupRef" />, <see cref="T:System.Xml.Schema.XmlSchemaChoice" />, <see cref="T:System.Xml.Schema.XmlSchemaAll" />, or <see cref="T:System.Xml.Schema.XmlSchemaSequence" /> classes.</summary>
		/// <returns>One of the <see cref="T:System.Xml.Schema.XmlSchemaGroupRef" />, <see cref="T:System.Xml.Schema.XmlSchemaChoice" />, <see cref="T:System.Xml.Schema.XmlSchemaAll" />, or <see cref="T:System.Xml.Schema.XmlSchemaSequence" /> classes.</returns>
		// Token: 0x170007BD RID: 1981
		// (get) Token: 0x06002072 RID: 8306 RVA: 0x000BEBA4 File Offset: 0x000BCDA4
		// (set) Token: 0x06002073 RID: 8307 RVA: 0x000BEBAC File Offset: 0x000BCDAC
		[XmlElement("group", typeof(XmlSchemaGroupRef))]
		[XmlElement("all", typeof(XmlSchemaAll))]
		[XmlElement("sequence", typeof(XmlSchemaSequence))]
		[XmlElement("choice", typeof(XmlSchemaChoice))]
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

		/// <summary>Gets the collection of attributes for the complex content. Contains <see cref="T:System.Xml.Schema.XmlSchemaAttribute" /> and <see cref="T:System.Xml.Schema.XmlSchemaAttributeGroupRef" /> elements.</summary>
		/// <returns>The collection of attributes for the complex content.</returns>
		// Token: 0x170007BE RID: 1982
		// (get) Token: 0x06002074 RID: 8308 RVA: 0x000BEBB5 File Offset: 0x000BCDB5
		[XmlElement("attributeGroup", typeof(XmlSchemaAttributeGroupRef))]
		[XmlElement("attribute", typeof(XmlSchemaAttribute))]
		public XmlSchemaObjectCollection Attributes
		{
			get
			{
				return this.attributes;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Xml.Schema.XmlSchemaAnyAttribute" /> component of the complex content model.</summary>
		/// <returns>The <see cref="T:System.Xml.Schema.XmlSchemaAnyAttribute" /> component of the complex content model.</returns>
		// Token: 0x170007BF RID: 1983
		// (get) Token: 0x06002075 RID: 8309 RVA: 0x000BEBBD File Offset: 0x000BCDBD
		// (set) Token: 0x06002076 RID: 8310 RVA: 0x000BEBC5 File Offset: 0x000BCDC5
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

		// Token: 0x06002077 RID: 8311 RVA: 0x000BEBCE File Offset: 0x000BCDCE
		internal void SetAttributes(XmlSchemaObjectCollection newAttributes)
		{
			this.attributes = newAttributes;
		}

		// Token: 0x04000F27 RID: 3879
		private XmlSchemaParticle particle;

		// Token: 0x04000F28 RID: 3880
		private XmlSchemaObjectCollection attributes = new XmlSchemaObjectCollection();

		// Token: 0x04000F29 RID: 3881
		private XmlSchemaAnyAttribute anyAttribute;

		// Token: 0x04000F2A RID: 3882
		private XmlQualifiedName baseTypeName = XmlQualifiedName.Empty;
	}
}
