using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Represents the union element for simple types from XML Schema as specified by the World Wide Web Consortium (W3C). A union datatype can be used to specify the content of a simpleType. The value of the simpleType element must be any one of a set of alternative datatypes specified in the union. Union types are always derived types and must comprise at least two alternative datatypes.</summary>
	// Token: 0x02000303 RID: 771
	public class XmlSchemaSimpleTypeUnion : XmlSchemaSimpleTypeContent
	{
		/// <summary>Gets the collection of base types.</summary>
		/// <returns>The collection of simple type base values.</returns>
		// Token: 0x17000874 RID: 2164
		// (get) Token: 0x06002254 RID: 8788 RVA: 0x000C3062 File Offset: 0x000C1262
		[XmlElement("simpleType", typeof(XmlSchemaSimpleType))]
		public XmlSchemaObjectCollection BaseTypes
		{
			get
			{
				return this.baseTypes;
			}
		}

		/// <summary>Gets or sets the array of qualified member names of built-in data types or simpleType elements defined in this schema (or another schema indicated by the specified namespace).</summary>
		/// <returns>An array that consists of a list of members of built-in data types or simple types.</returns>
		// Token: 0x17000875 RID: 2165
		// (get) Token: 0x06002255 RID: 8789 RVA: 0x000C306A File Offset: 0x000C126A
		// (set) Token: 0x06002256 RID: 8790 RVA: 0x000C3072 File Offset: 0x000C1272
		[XmlAttribute("memberTypes")]
		public XmlQualifiedName[] MemberTypes
		{
			get
			{
				return this.memberTypes;
			}
			set
			{
				this.memberTypes = value;
			}
		}

		/// <summary>Gets an array of <see cref="T:System.Xml.Schema.XmlSchemaSimpleType" /> objects representing the type of the simpleType element based on the <see cref="P:System.Xml.Schema.XmlSchemaSimpleTypeUnion.BaseTypes" /> and <see cref="P:System.Xml.Schema.XmlSchemaSimpleTypeUnion.MemberTypes" /> values of the simple type.</summary>
		/// <returns>An array of <see cref="T:System.Xml.Schema.XmlSchemaSimpleType" /> objects representing the type of the simpleType element.</returns>
		// Token: 0x17000876 RID: 2166
		// (get) Token: 0x06002257 RID: 8791 RVA: 0x000C307B File Offset: 0x000C127B
		[XmlIgnore]
		public XmlSchemaSimpleType[] BaseMemberTypes
		{
			get
			{
				return this.baseMemberTypes;
			}
		}

		// Token: 0x06002258 RID: 8792 RVA: 0x000C3083 File Offset: 0x000C1283
		internal void SetBaseMemberTypes(XmlSchemaSimpleType[] baseMemberTypes)
		{
			this.baseMemberTypes = baseMemberTypes;
		}

		// Token: 0x06002259 RID: 8793 RVA: 0x000C308C File Offset: 0x000C128C
		internal override XmlSchemaObject Clone()
		{
			if (this.memberTypes != null && this.memberTypes.Length != 0)
			{
				XmlSchemaSimpleTypeUnion xmlSchemaSimpleTypeUnion = (XmlSchemaSimpleTypeUnion)base.MemberwiseClone();
				XmlQualifiedName[] array = new XmlQualifiedName[this.memberTypes.Length];
				for (int i = 0; i < this.memberTypes.Length; i++)
				{
					array[i] = this.memberTypes[i].Clone();
				}
				xmlSchemaSimpleTypeUnion.MemberTypes = array;
				return xmlSchemaSimpleTypeUnion;
			}
			return this;
		}

		// Token: 0x04000FF3 RID: 4083
		private XmlSchemaObjectCollection baseTypes = new XmlSchemaObjectCollection();

		// Token: 0x04000FF4 RID: 4084
		private XmlQualifiedName[] memberTypes;

		// Token: 0x04000FF5 RID: 4085
		private XmlSchemaSimpleType[] baseMemberTypes;
	}
}
