using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Represents the restriction element from XML Schema as specified by the World Wide Web Consortium (W3C). This class is for complex types with a complex content model derived by restriction. It restricts the contents of the complex type to a subset of the inherited complex type.</summary>
	// Token: 0x020002C5 RID: 709
	public class XmlSchemaComplexContentRestriction : XmlSchemaContent
	{
		/// <summary>Gets or sets the name of a complex type from which this type is derived by restriction.</summary>
		/// <returns>The name of the complex type from which this type is derived by restriction.</returns>
		// Token: 0x170007C0 RID: 1984
		// (get) Token: 0x06002079 RID: 8313 RVA: 0x000BEBF5 File Offset: 0x000BCDF5
		// (set) Token: 0x0600207A RID: 8314 RVA: 0x000BEBFD File Offset: 0x000BCDFD
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
		// Token: 0x170007C1 RID: 1985
		// (get) Token: 0x0600207B RID: 8315 RVA: 0x000BEC16 File Offset: 0x000BCE16
		// (set) Token: 0x0600207C RID: 8316 RVA: 0x000BEC1E File Offset: 0x000BCE1E
		[XmlElement("sequence", typeof(XmlSchemaSequence))]
		[XmlElement("all", typeof(XmlSchemaAll))]
		[XmlElement("choice", typeof(XmlSchemaChoice))]
		[XmlElement("group", typeof(XmlSchemaGroupRef))]
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

		/// <summary>Gets the collection of attributes for the complex type. Contains the <see cref="T:System.Xml.Schema.XmlSchemaAttribute" /> and <see cref="T:System.Xml.Schema.XmlSchemaAttributeGroupRef" /> elements.</summary>
		/// <returns>The collection of attributes for the complex type.</returns>
		// Token: 0x170007C2 RID: 1986
		// (get) Token: 0x0600207D RID: 8317 RVA: 0x000BEC27 File Offset: 0x000BCE27
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
		// Token: 0x170007C3 RID: 1987
		// (get) Token: 0x0600207E RID: 8318 RVA: 0x000BEC2F File Offset: 0x000BCE2F
		// (set) Token: 0x0600207F RID: 8319 RVA: 0x000BEC37 File Offset: 0x000BCE37
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

		// Token: 0x06002080 RID: 8320 RVA: 0x000BEC40 File Offset: 0x000BCE40
		internal void SetAttributes(XmlSchemaObjectCollection newAttributes)
		{
			this.attributes = newAttributes;
		}

		// Token: 0x04000F2B RID: 3883
		private XmlSchemaParticle particle;

		// Token: 0x04000F2C RID: 3884
		private XmlSchemaObjectCollection attributes = new XmlSchemaObjectCollection();

		// Token: 0x04000F2D RID: 3885
		private XmlSchemaAnyAttribute anyAttribute;

		// Token: 0x04000F2E RID: 3886
		private XmlQualifiedName baseTypeName = XmlQualifiedName.Empty;
	}
}
