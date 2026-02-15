using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Represents the attributeGroup element from the XML Schema as specified by the World Wide Web Consortium (W3C). AttributesGroups provides a mechanism to group a set of attribute declarations so that they can be incorporated as a group into complex type definitions.</summary>
	// Token: 0x020002BC RID: 700
	public class XmlSchemaAttributeGroup : XmlSchemaAnnotated
	{
		/// <summary>Gets or sets the name of the attribute group.</summary>
		/// <returns>The name of the attribute group.</returns>
		// Token: 0x1700079D RID: 1949
		// (get) Token: 0x0600202F RID: 8239 RVA: 0x000BE662 File Offset: 0x000BC862
		// (set) Token: 0x06002030 RID: 8240 RVA: 0x000BE66A File Offset: 0x000BC86A
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

		/// <summary>Gets the collection of attributes for the attribute group. Contains XmlSchemaAttribute and XmlSchemaAttributeGroupRef elements.</summary>
		/// <returns>The collection of attributes for the attribute group.</returns>
		// Token: 0x1700079E RID: 1950
		// (get) Token: 0x06002031 RID: 8241 RVA: 0x000BE673 File Offset: 0x000BC873
		[XmlElement("attributeGroup", typeof(XmlSchemaAttributeGroupRef))]
		[XmlElement("attribute", typeof(XmlSchemaAttribute))]
		public XmlSchemaObjectCollection Attributes
		{
			get
			{
				return this.attributes;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Xml.Schema.XmlSchemaAnyAttribute" /> component of the attribute group.</summary>
		/// <returns>The World Wide Web Consortium (W3C) anyAttribute element.</returns>
		// Token: 0x1700079F RID: 1951
		// (get) Token: 0x06002032 RID: 8242 RVA: 0x000BE67B File Offset: 0x000BC87B
		// (set) Token: 0x06002033 RID: 8243 RVA: 0x000BE683 File Offset: 0x000BC883
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

		/// <summary>Gets the qualified name of the attribute group.</summary>
		/// <returns>The qualified name of the attribute group.</returns>
		// Token: 0x170007A0 RID: 1952
		// (get) Token: 0x06002034 RID: 8244 RVA: 0x000BE68C File Offset: 0x000BC88C
		[XmlIgnore]
		public XmlQualifiedName QualifiedName
		{
			get
			{
				return this.qname;
			}
		}

		// Token: 0x170007A1 RID: 1953
		// (get) Token: 0x06002035 RID: 8245 RVA: 0x000BE694 File Offset: 0x000BC894
		[XmlIgnore]
		internal XmlSchemaObjectTable AttributeUses
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

		// Token: 0x170007A2 RID: 1954
		// (get) Token: 0x06002036 RID: 8246 RVA: 0x000BE6AF File Offset: 0x000BC8AF
		// (set) Token: 0x06002037 RID: 8247 RVA: 0x000BE6B7 File Offset: 0x000BC8B7
		[XmlIgnore]
		internal XmlSchemaAnyAttribute AttributeWildcard
		{
			get
			{
				return this.attributeWildcard;
			}
			set
			{
				this.attributeWildcard = value;
			}
		}

		/// <summary>Gets the redefined attribute group property from the XML Schema.</summary>
		/// <returns>The redefined attribute group property.</returns>
		// Token: 0x170007A3 RID: 1955
		// (get) Token: 0x06002038 RID: 8248 RVA: 0x000BE6C0 File Offset: 0x000BC8C0
		[XmlIgnore]
		public XmlSchemaAttributeGroup RedefinedAttributeGroup
		{
			get
			{
				return this.redefined;
			}
		}

		// Token: 0x170007A4 RID: 1956
		// (get) Token: 0x06002039 RID: 8249 RVA: 0x000BE6C0 File Offset: 0x000BC8C0
		// (set) Token: 0x0600203A RID: 8250 RVA: 0x000BE6C8 File Offset: 0x000BC8C8
		[XmlIgnore]
		internal XmlSchemaAttributeGroup Redefined
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

		// Token: 0x170007A5 RID: 1957
		// (get) Token: 0x0600203B RID: 8251 RVA: 0x000BE6D1 File Offset: 0x000BC8D1
		// (set) Token: 0x0600203C RID: 8252 RVA: 0x000BE6D9 File Offset: 0x000BC8D9
		[XmlIgnore]
		internal int SelfReferenceCount
		{
			get
			{
				return this.selfReferenceCount;
			}
			set
			{
				this.selfReferenceCount = value;
			}
		}

		// Token: 0x170007A6 RID: 1958
		// (get) Token: 0x0600203D RID: 8253 RVA: 0x000BE6E2 File Offset: 0x000BC8E2
		// (set) Token: 0x0600203E RID: 8254 RVA: 0x000BE6EA File Offset: 0x000BC8EA
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

		// Token: 0x0600203F RID: 8255 RVA: 0x000BE6F3 File Offset: 0x000BC8F3
		internal void SetQualifiedName(XmlQualifiedName value)
		{
			this.qname = value;
		}

		// Token: 0x06002040 RID: 8256 RVA: 0x000BE6FC File Offset: 0x000BC8FC
		internal override XmlSchemaObject Clone()
		{
			XmlSchemaAttributeGroup xmlSchemaAttributeGroup = (XmlSchemaAttributeGroup)base.MemberwiseClone();
			if (XmlSchemaComplexType.HasAttributeQNameRef(this.attributes))
			{
				xmlSchemaAttributeGroup.attributes = XmlSchemaComplexType.CloneAttributes(this.attributes);
				xmlSchemaAttributeGroup.attributeUses = null;
			}
			return xmlSchemaAttributeGroup;
		}

		// Token: 0x04000F0D RID: 3853
		private string name;

		// Token: 0x04000F0E RID: 3854
		private XmlSchemaObjectCollection attributes = new XmlSchemaObjectCollection();

		// Token: 0x04000F0F RID: 3855
		private XmlSchemaAnyAttribute anyAttribute;

		// Token: 0x04000F10 RID: 3856
		private XmlQualifiedName qname = XmlQualifiedName.Empty;

		// Token: 0x04000F11 RID: 3857
		private XmlSchemaAttributeGroup redefined;

		// Token: 0x04000F12 RID: 3858
		private XmlSchemaObjectTable attributeUses;

		// Token: 0x04000F13 RID: 3859
		private XmlSchemaAnyAttribute attributeWildcard;

		// Token: 0x04000F14 RID: 3860
		private int selfReferenceCount;
	}
}
