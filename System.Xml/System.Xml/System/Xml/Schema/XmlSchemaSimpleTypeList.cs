using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Represents the list element from XML Schema as specified by the World Wide Web Consortium (W3C). This class can be used to define a simpleType element as a list of values of a specified data type.</summary>
	// Token: 0x02000301 RID: 769
	public class XmlSchemaSimpleTypeList : XmlSchemaSimpleTypeContent
	{
		/// <summary>Gets or sets the name of a built-in data type or simpleType element defined in this schema (or another schema indicated by the specified namespace).</summary>
		/// <returns>The type name of the simple type list.</returns>
		// Token: 0x1700086E RID: 2158
		// (get) Token: 0x06002245 RID: 8773 RVA: 0x000C2F78 File Offset: 0x000C1178
		// (set) Token: 0x06002246 RID: 8774 RVA: 0x000C2F80 File Offset: 0x000C1180
		[XmlAttribute("itemType")]
		public XmlQualifiedName ItemTypeName
		{
			get
			{
				return this.itemTypeName;
			}
			set
			{
				this.itemTypeName = ((value == null) ? XmlQualifiedName.Empty : value);
			}
		}

		/// <summary>Gets or sets the simpleType element that is derived from the type specified by the base value.</summary>
		/// <returns>The item type for the simple type element.</returns>
		// Token: 0x1700086F RID: 2159
		// (get) Token: 0x06002247 RID: 8775 RVA: 0x000C2F99 File Offset: 0x000C1199
		// (set) Token: 0x06002248 RID: 8776 RVA: 0x000C2FA1 File Offset: 0x000C11A1
		[XmlElement("simpleType", typeof(XmlSchemaSimpleType))]
		public XmlSchemaSimpleType ItemType
		{
			get
			{
				return this.itemType;
			}
			set
			{
				this.itemType = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Xml.Schema.XmlSchemaSimpleType" /> representing the type of the simpleType element based on the <see cref="P:System.Xml.Schema.XmlSchemaSimpleTypeList.ItemType" /> and <see cref="P:System.Xml.Schema.XmlSchemaSimpleTypeList.ItemTypeName" /> values of the simple type.</summary>
		/// <returns>The <see cref="T:System.Xml.Schema.XmlSchemaSimpleType" /> representing the type of the simpleType element.</returns>
		// Token: 0x17000870 RID: 2160
		// (get) Token: 0x06002249 RID: 8777 RVA: 0x000C2FAA File Offset: 0x000C11AA
		// (set) Token: 0x0600224A RID: 8778 RVA: 0x000C2FB2 File Offset: 0x000C11B2
		[XmlIgnore]
		public XmlSchemaSimpleType BaseItemType
		{
			get
			{
				return this.baseItemType;
			}
			set
			{
				this.baseItemType = value;
			}
		}

		// Token: 0x0600224B RID: 8779 RVA: 0x000C2FBB File Offset: 0x000C11BB
		internal override XmlSchemaObject Clone()
		{
			XmlSchemaSimpleTypeList xmlSchemaSimpleTypeList = (XmlSchemaSimpleTypeList)base.MemberwiseClone();
			xmlSchemaSimpleTypeList.ItemTypeName = this.itemTypeName.Clone();
			return xmlSchemaSimpleTypeList;
		}

		// Token: 0x04000FED RID: 4077
		private XmlQualifiedName itemTypeName = XmlQualifiedName.Empty;

		// Token: 0x04000FEE RID: 4078
		private XmlSchemaSimpleType itemType;

		// Token: 0x04000FEF RID: 4079
		private XmlSchemaSimpleType baseItemType;
	}
}
