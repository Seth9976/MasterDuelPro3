using System;

namespace System.Xml.Schema
{
	// Token: 0x0200031D RID: 797
	internal class XmlUnionConverter : XmlBaseConverter
	{
		// Token: 0x06002431 RID: 9265 RVA: 0x000CC55C File Offset: 0x000CA75C
		protected XmlUnionConverter(XmlSchemaType schemaType)
			: base(schemaType)
		{
			while (schemaType.DerivedBy == XmlSchemaDerivationMethod.Restriction)
			{
				schemaType = schemaType.BaseXmlSchemaType;
			}
			XmlSchemaSimpleType[] baseMemberTypes = ((XmlSchemaSimpleTypeUnion)((XmlSchemaSimpleType)schemaType).Content).BaseMemberTypes;
			this.converters = new XmlValueConverter[baseMemberTypes.Length];
			for (int i = 0; i < baseMemberTypes.Length; i++)
			{
				this.converters[i] = baseMemberTypes[i].ValueConverter;
				if (baseMemberTypes[i].Datatype.Variety == XmlSchemaDatatypeVariety.List)
				{
					this.hasListMember = true;
				}
				else if (baseMemberTypes[i].Datatype.Variety == XmlSchemaDatatypeVariety.Atomic)
				{
					this.hasAtomicMember = true;
				}
			}
		}

		// Token: 0x06002432 RID: 9266 RVA: 0x000CC5F4 File Offset: 0x000CA7F4
		public static XmlValueConverter Create(XmlSchemaType schemaType)
		{
			return new XmlUnionConverter(schemaType);
		}

		// Token: 0x06002433 RID: 9267 RVA: 0x000CC5FC File Offset: 0x000CA7FC
		public override object ChangeType(object value, Type destinationType, IXmlNamespaceResolver nsResolver)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			Type type = value.GetType();
			if (type == XmlBaseConverter.XmlAtomicValueType && this.hasAtomicMember)
			{
				return ((XmlAtomicValue)value).ValueAs(destinationType, nsResolver);
			}
			if (type == XmlBaseConverter.XmlAtomicValueArrayType && this.hasListMember)
			{
				return XmlAnyListConverter.ItemList.ChangeType(value, destinationType, nsResolver);
			}
			if (!(type == XmlBaseConverter.StringType))
			{
				throw base.CreateInvalidClrMappingException(type, destinationType);
			}
			if (destinationType == XmlBaseConverter.StringType)
			{
				return value;
			}
			return ((XsdSimpleValue)base.SchemaType.Datatype.ParseValue((string)value, new NameTable(), nsResolver, true)).XmlType.ValueConverter.ChangeType((string)value, destinationType, nsResolver);
		}

		// Token: 0x040010B5 RID: 4277
		private XmlValueConverter[] converters;

		// Token: 0x040010B6 RID: 4278
		private bool hasAtomicMember;

		// Token: 0x040010B7 RID: 4279
		private bool hasListMember;
	}
}
