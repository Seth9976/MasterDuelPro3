using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Represents the redefine element from XML Schema as specified by the World Wide Web Consortium (W3C). This class can be used to allow simple and complex types, groups and attribute groups from external schema files to be redefined in the current schema. This class can also be used to provide versioning for the schema elements.</summary>
	// Token: 0x020002F9 RID: 761
	public class XmlSchemaRedefine : XmlSchemaExternal
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaRedefine" /> class.</summary>
		// Token: 0x060021EC RID: 8684 RVA: 0x000C1057 File Offset: 0x000BF257
		public XmlSchemaRedefine()
		{
			base.Compositor = Compositor.Redefine;
		}

		/// <summary>Gets the collection of the following classes: <see cref="T:System.Xml.Schema.XmlSchemaAnnotation" />, <see cref="T:System.Xml.Schema.XmlSchemaAttributeGroup" />, <see cref="T:System.Xml.Schema.XmlSchemaComplexType" />, <see cref="T:System.Xml.Schema.XmlSchemaSimpleType" />, and <see cref="T:System.Xml.Schema.XmlSchemaGroup" />.</summary>
		/// <returns>The elements contained within the redefine element.</returns>
		// Token: 0x1700084F RID: 2127
		// (get) Token: 0x060021ED RID: 8685 RVA: 0x000C1092 File Offset: 0x000BF292
		[XmlElement("simpleType", typeof(XmlSchemaSimpleType))]
		[XmlElement("annotation", typeof(XmlSchemaAnnotation))]
		[XmlElement("attributeGroup", typeof(XmlSchemaAttributeGroup))]
		[XmlElement("complexType", typeof(XmlSchemaComplexType))]
		[XmlElement("group", typeof(XmlSchemaGroup))]
		public XmlSchemaObjectCollection Items
		{
			get
			{
				return this.items;
			}
		}

		/// <summary>Gets the <see cref="T:System.Xml.Schema.XmlSchemaObjectTable" /> , for all attributes in the schema, which holds the post-compilation value of the AttributeGroups property.</summary>
		/// <returns>The <see cref="T:System.Xml.Schema.XmlSchemaObjectTable" /> for all attributes in the schema. The post-compilation value of the AttributeGroups property.</returns>
		// Token: 0x17000850 RID: 2128
		// (get) Token: 0x060021EE RID: 8686 RVA: 0x000C109A File Offset: 0x000BF29A
		[XmlIgnore]
		public XmlSchemaObjectTable AttributeGroups
		{
			get
			{
				return this.attributeGroups;
			}
		}

		/// <summary>Gets the <see cref="T:System.Xml.Schema.XmlSchemaObjectTable" />, for all simple and complex types in the schema, which holds the post-compilation value of the SchemaTypes property.</summary>
		/// <returns>The <see cref="T:System.Xml.Schema.XmlSchemaObjectTable" /> for all schema types in the schema. The post-compilation value of the SchemaTypes property.</returns>
		// Token: 0x17000851 RID: 2129
		// (get) Token: 0x060021EF RID: 8687 RVA: 0x000C10A2 File Offset: 0x000BF2A2
		[XmlIgnore]
		public XmlSchemaObjectTable SchemaTypes
		{
			get
			{
				return this.types;
			}
		}

		/// <summary>Gets the <see cref="T:System.Xml.Schema.XmlSchemaObjectTable" />, for all groups in the schema, which holds the post-compilation value of the Groups property.</summary>
		/// <returns>The <see cref="T:System.Xml.Schema.XmlSchemaObjectTable" /> for all groups in the schema. The post-compilation value of the Groups property.</returns>
		// Token: 0x17000852 RID: 2130
		// (get) Token: 0x060021F0 RID: 8688 RVA: 0x000C10AA File Offset: 0x000BF2AA
		[XmlIgnore]
		public XmlSchemaObjectTable Groups
		{
			get
			{
				return this.groups;
			}
		}

		// Token: 0x060021F1 RID: 8689 RVA: 0x000C10B2 File Offset: 0x000BF2B2
		internal override void AddAnnotation(XmlSchemaAnnotation annotation)
		{
			this.items.Add(annotation);
		}

		// Token: 0x04000FCA RID: 4042
		private XmlSchemaObjectCollection items = new XmlSchemaObjectCollection();

		// Token: 0x04000FCB RID: 4043
		private XmlSchemaObjectTable attributeGroups = new XmlSchemaObjectTable();

		// Token: 0x04000FCC RID: 4044
		private XmlSchemaObjectTable types = new XmlSchemaObjectTable();

		// Token: 0x04000FCD RID: 4045
		private XmlSchemaObjectTable groups = new XmlSchemaObjectTable();
	}
}
