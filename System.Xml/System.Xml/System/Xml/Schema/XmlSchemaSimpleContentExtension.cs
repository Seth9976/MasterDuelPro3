using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Represents the extension element for simple content from XML Schema as specified by the World Wide Web Consortium (W3C). This class can be used to derive simple types by extension. Such derivations are used to extend the simple type content of the element by adding attributes.</summary>
	// Token: 0x020002FD RID: 765
	public class XmlSchemaSimpleContentExtension : XmlSchemaContent
	{
		/// <summary>Gets or sets the name of a built-in data type or simple type from which this type is extended.</summary>
		/// <returns>The base type name.</returns>
		// Token: 0x17000864 RID: 2148
		// (get) Token: 0x0600222E RID: 8750 RVA: 0x000C2E0E File Offset: 0x000C100E
		// (set) Token: 0x0600222F RID: 8751 RVA: 0x000C2E16 File Offset: 0x000C1016
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

		/// <summary>Gets the collection of <see cref="T:System.Xml.Schema.XmlSchemaAttribute" /> and <see cref="T:System.Xml.Schema.XmlSchemaAttributeGroupRef" />.</summary>
		/// <returns>The collection of attributes for the simpleType element.</returns>
		// Token: 0x17000865 RID: 2149
		// (get) Token: 0x06002230 RID: 8752 RVA: 0x000C2E2F File Offset: 0x000C102F
		[XmlElement("attribute", typeof(XmlSchemaAttribute))]
		[XmlElement("attributeGroup", typeof(XmlSchemaAttributeGroupRef))]
		public XmlSchemaObjectCollection Attributes
		{
			get
			{
				return this.attributes;
			}
		}

		/// <summary>Gets or sets the XmlSchemaAnyAttribute to be used for the attribute value.</summary>
		/// <returns>The XmlSchemaAnyAttribute.Optional.</returns>
		// Token: 0x17000866 RID: 2150
		// (get) Token: 0x06002231 RID: 8753 RVA: 0x000C2E37 File Offset: 0x000C1037
		// (set) Token: 0x06002232 RID: 8754 RVA: 0x000C2E3F File Offset: 0x000C103F
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

		// Token: 0x06002233 RID: 8755 RVA: 0x000C2E48 File Offset: 0x000C1048
		internal void SetAttributes(XmlSchemaObjectCollection newAttributes)
		{
			this.attributes = newAttributes;
		}

		// Token: 0x04000FE4 RID: 4068
		private XmlSchemaObjectCollection attributes = new XmlSchemaObjectCollection();

		// Token: 0x04000FE5 RID: 4069
		private XmlSchemaAnyAttribute anyAttribute;

		// Token: 0x04000FE6 RID: 4070
		private XmlQualifiedName baseTypeName = XmlQualifiedName.Empty;
	}
}
