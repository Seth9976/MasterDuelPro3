using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Represents the attribute element from the XML Schema as specified by the World Wide Web Consortium (W3C). Attributes provide additional information for other document elements. The attribute tag is nested between the tags of a document's element for the schema. The XML document displays attributes as named items in the opening tag of an element.</summary>
	// Token: 0x020002BB RID: 699
	public class XmlSchemaAttribute : XmlSchemaAnnotated
	{
		/// <summary>Gets or sets the default value for the attribute.</summary>
		/// <returns>The default value for the attribute. The default is a null reference.Optional.</returns>
		// Token: 0x1700078F RID: 1935
		// (get) Token: 0x06002013 RID: 8211 RVA: 0x000BE4EB File Offset: 0x000BC6EB
		// (set) Token: 0x06002014 RID: 8212 RVA: 0x000BE4F3 File Offset: 0x000BC6F3
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

		/// <summary>Gets or sets the fixed value for the attribute.</summary>
		/// <returns>The fixed value for the attribute. The default is null.Optional.</returns>
		// Token: 0x17000790 RID: 1936
		// (get) Token: 0x06002015 RID: 8213 RVA: 0x000BE4FC File Offset: 0x000BC6FC
		// (set) Token: 0x06002016 RID: 8214 RVA: 0x000BE504 File Offset: 0x000BC704
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

		/// <summary>Gets or sets the form for the attribute.</summary>
		/// <returns>One of the <see cref="T:System.Xml.Schema.XmlSchemaForm" /> values. The default is the value of the <see cref="P:System.Xml.Schema.XmlSchema.AttributeFormDefault" /> of the schema element containing the attribute.Optional.</returns>
		// Token: 0x17000791 RID: 1937
		// (get) Token: 0x06002017 RID: 8215 RVA: 0x000BE50D File Offset: 0x000BC70D
		// (set) Token: 0x06002018 RID: 8216 RVA: 0x000BE515 File Offset: 0x000BC715
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

		/// <summary>Gets or sets the name of the attribute.</summary>
		/// <returns>The name of the attribute.</returns>
		// Token: 0x17000792 RID: 1938
		// (get) Token: 0x06002019 RID: 8217 RVA: 0x000BE51E File Offset: 0x000BC71E
		// (set) Token: 0x0600201A RID: 8218 RVA: 0x000BE526 File Offset: 0x000BC726
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

		/// <summary>Gets or sets the name of an attribute declared in this schema (or another schema indicated by the specified namespace).</summary>
		/// <returns>The name of the attribute declared.</returns>
		// Token: 0x17000793 RID: 1939
		// (get) Token: 0x0600201B RID: 8219 RVA: 0x000BE52F File Offset: 0x000BC72F
		// (set) Token: 0x0600201C RID: 8220 RVA: 0x000BE537 File Offset: 0x000BC737
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

		/// <summary>Gets or sets the name of the simple type defined in this schema (or another schema indicated by the specified namespace).</summary>
		/// <returns>The name of the simple type.</returns>
		// Token: 0x17000794 RID: 1940
		// (get) Token: 0x0600201D RID: 8221 RVA: 0x000BE550 File Offset: 0x000BC750
		// (set) Token: 0x0600201E RID: 8222 RVA: 0x000BE558 File Offset: 0x000BC758
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

		/// <summary>Gets or sets the attribute type to a simple type.</summary>
		/// <returns>The simple type defined in this schema.</returns>
		// Token: 0x17000795 RID: 1941
		// (get) Token: 0x0600201F RID: 8223 RVA: 0x000BE571 File Offset: 0x000BC771
		// (set) Token: 0x06002020 RID: 8224 RVA: 0x000BE579 File Offset: 0x000BC779
		[XmlElement("simpleType")]
		public XmlSchemaSimpleType SchemaType
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

		/// <summary>Gets or sets information about how the attribute is used.</summary>
		/// <returns>One of the following values: None, Prohibited, Optional, or Required. The default is Optional.Optional.</returns>
		// Token: 0x17000796 RID: 1942
		// (get) Token: 0x06002021 RID: 8225 RVA: 0x000BE582 File Offset: 0x000BC782
		// (set) Token: 0x06002022 RID: 8226 RVA: 0x000BE58A File Offset: 0x000BC78A
		[XmlAttribute("use")]
		[DefaultValue(XmlSchemaUse.None)]
		public XmlSchemaUse Use
		{
			get
			{
				return this.use;
			}
			set
			{
				this.use = value;
			}
		}

		/// <summary>Gets the qualified name for the attribute.</summary>
		/// <returns>The post-compilation value of the QualifiedName property.</returns>
		// Token: 0x17000797 RID: 1943
		// (get) Token: 0x06002023 RID: 8227 RVA: 0x000BE593 File Offset: 0x000BC793
		[XmlIgnore]
		public XmlQualifiedName QualifiedName
		{
			get
			{
				return this.qualifiedName;
			}
		}

		/// <summary>Gets an <see cref="T:System.Xml.Schema.XmlSchemaSimpleType" /> object representing the type of the attribute based on the <see cref="P:System.Xml.Schema.XmlSchemaAttribute.SchemaType" /> or <see cref="P:System.Xml.Schema.XmlSchemaAttribute.SchemaTypeName" /> of the attribute.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSchemaSimpleType" /> object.</returns>
		// Token: 0x17000798 RID: 1944
		// (get) Token: 0x06002024 RID: 8228 RVA: 0x000BE59B File Offset: 0x000BC79B
		[XmlIgnore]
		public XmlSchemaSimpleType AttributeSchemaType
		{
			get
			{
				return this.attributeType;
			}
		}

		// Token: 0x17000799 RID: 1945
		// (get) Token: 0x06002025 RID: 8229 RVA: 0x000BE5A3 File Offset: 0x000BC7A3
		[XmlIgnore]
		internal XmlSchemaDatatype Datatype
		{
			get
			{
				if (this.attributeType != null)
				{
					return this.attributeType.Datatype;
				}
				return null;
			}
		}

		// Token: 0x06002026 RID: 8230 RVA: 0x000BE5BA File Offset: 0x000BC7BA
		internal void SetQualifiedName(XmlQualifiedName value)
		{
			this.qualifiedName = value;
		}

		// Token: 0x06002027 RID: 8231 RVA: 0x000BE5C3 File Offset: 0x000BC7C3
		internal void SetAttributeType(XmlSchemaSimpleType value)
		{
			this.attributeType = value;
		}

		// Token: 0x1700079A RID: 1946
		// (get) Token: 0x06002028 RID: 8232 RVA: 0x000BE5CC File Offset: 0x000BC7CC
		// (set) Token: 0x06002029 RID: 8233 RVA: 0x000BE5D4 File Offset: 0x000BC7D4
		internal SchemaAttDef AttDef
		{
			get
			{
				return this.attDef;
			}
			set
			{
				this.attDef = value;
			}
		}

		// Token: 0x1700079B RID: 1947
		// (get) Token: 0x0600202A RID: 8234 RVA: 0x000BE5DD File Offset: 0x000BC7DD
		internal bool HasDefault
		{
			get
			{
				return this.defaultValue != null;
			}
		}

		// Token: 0x1700079C RID: 1948
		// (get) Token: 0x0600202B RID: 8235 RVA: 0x000BE5E8 File Offset: 0x000BC7E8
		// (set) Token: 0x0600202C RID: 8236 RVA: 0x000BE5F0 File Offset: 0x000BC7F0
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

		// Token: 0x0600202D RID: 8237 RVA: 0x000BE5F9 File Offset: 0x000BC7F9
		internal override XmlSchemaObject Clone()
		{
			XmlSchemaAttribute xmlSchemaAttribute = (XmlSchemaAttribute)base.MemberwiseClone();
			xmlSchemaAttribute.refName = this.refName.Clone();
			xmlSchemaAttribute.typeName = this.typeName.Clone();
			xmlSchemaAttribute.qualifiedName = this.qualifiedName.Clone();
			return xmlSchemaAttribute;
		}

		// Token: 0x04000F02 RID: 3842
		private string defaultValue;

		// Token: 0x04000F03 RID: 3843
		private string fixedValue;

		// Token: 0x04000F04 RID: 3844
		private string name;

		// Token: 0x04000F05 RID: 3845
		private XmlSchemaForm form;

		// Token: 0x04000F06 RID: 3846
		private XmlSchemaUse use;

		// Token: 0x04000F07 RID: 3847
		private XmlQualifiedName refName = XmlQualifiedName.Empty;

		// Token: 0x04000F08 RID: 3848
		private XmlQualifiedName typeName = XmlQualifiedName.Empty;

		// Token: 0x04000F09 RID: 3849
		private XmlQualifiedName qualifiedName = XmlQualifiedName.Empty;

		// Token: 0x04000F0A RID: 3850
		private XmlSchemaSimpleType type;

		// Token: 0x04000F0B RID: 3851
		private XmlSchemaSimpleType attributeType;

		// Token: 0x04000F0C RID: 3852
		private SchemaAttDef attDef;
	}
}
