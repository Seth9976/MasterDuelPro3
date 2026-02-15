using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Represents the simpleType element for simple content from XML Schema as specified by the World Wide Web Consortium (W3C). This class defines a simple type. Simple types can specify information and constraints for the value of attributes or elements with text-only content.</summary>
	// Token: 0x020002FF RID: 767
	public class XmlSchemaSimpleType : XmlSchemaType
	{
		/// <summary>Gets or sets one of <see cref="T:System.Xml.Schema.XmlSchemaSimpleTypeUnion" />, <see cref="T:System.Xml.Schema.XmlSchemaSimpleTypeList" />, or <see cref="T:System.Xml.Schema.XmlSchemaSimpleTypeRestriction" />.</summary>
		/// <returns>One of XmlSchemaSimpleTypeUnion, XmlSchemaSimpleTypeList, or XmlSchemaSimpleTypeRestriction.</returns>
		// Token: 0x1700086C RID: 2156
		// (get) Token: 0x06002240 RID: 8768 RVA: 0x000C2EFC File Offset: 0x000C10FC
		// (set) Token: 0x06002241 RID: 8769 RVA: 0x000C2F04 File Offset: 0x000C1104
		[XmlElement("restriction", typeof(XmlSchemaSimpleTypeRestriction))]
		[XmlElement("list", typeof(XmlSchemaSimpleTypeList))]
		[XmlElement("union", typeof(XmlSchemaSimpleTypeUnion))]
		public XmlSchemaSimpleTypeContent Content
		{
			get
			{
				return this.content;
			}
			set
			{
				this.content = value;
			}
		}

		// Token: 0x1700086D RID: 2157
		// (get) Token: 0x06002242 RID: 8770 RVA: 0x000C2F0D File Offset: 0x000C110D
		internal override XmlQualifiedName DerivedFrom
		{
			get
			{
				if (this.content == null)
				{
					return XmlQualifiedName.Empty;
				}
				if (this.content is XmlSchemaSimpleTypeRestriction)
				{
					return ((XmlSchemaSimpleTypeRestriction)this.content).BaseTypeName;
				}
				return XmlQualifiedName.Empty;
			}
		}

		// Token: 0x06002243 RID: 8771 RVA: 0x000C2F40 File Offset: 0x000C1140
		internal override XmlSchemaObject Clone()
		{
			XmlSchemaSimpleType xmlSchemaSimpleType = (XmlSchemaSimpleType)base.MemberwiseClone();
			if (this.content != null)
			{
				xmlSchemaSimpleType.Content = (XmlSchemaSimpleTypeContent)this.content.Clone();
			}
			return xmlSchemaSimpleType;
		}

		// Token: 0x04000FEC RID: 4076
		private XmlSchemaSimpleTypeContent content;
	}
}
