using System;

namespace System.Xml.Schema
{
	// Token: 0x0200023C RID: 572
	internal class Datatype_string : Datatype_anySimpleType
	{
		// Token: 0x06001B75 RID: 7029 RVA: 0x0009E3CC File Offset: 0x0009C5CC
		internal override XmlValueConverter CreateValueConverter(XmlSchemaType schemaType)
		{
			return XmlStringConverter.Create(schemaType);
		}

		// Token: 0x17000640 RID: 1600
		// (get) Token: 0x06001B76 RID: 7030 RVA: 0x0000C1F5 File Offset: 0x0000A3F5
		internal override XmlSchemaWhiteSpace BuiltInWhitespaceFacet
		{
			get
			{
				return XmlSchemaWhiteSpace.Preserve;
			}
		}

		// Token: 0x17000641 RID: 1601
		// (get) Token: 0x06001B77 RID: 7031 RVA: 0x0009E3D4 File Offset: 0x0009C5D4
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return DatatypeImplementation.stringFacetsChecker;
			}
		}

		// Token: 0x17000642 RID: 1602
		// (get) Token: 0x06001B78 RID: 7032 RVA: 0x0000A2FC File Offset: 0x000084FC
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.String;
			}
		}

		// Token: 0x17000643 RID: 1603
		// (get) Token: 0x06001B79 RID: 7033 RVA: 0x0000C1F5 File Offset: 0x0000A3F5
		public override XmlTokenizedType TokenizedType
		{
			get
			{
				return XmlTokenizedType.CDATA;
			}
		}

		// Token: 0x17000644 RID: 1604
		// (get) Token: 0x06001B7A RID: 7034 RVA: 0x0009DD7D File Offset: 0x0009BF7D
		internal override RestrictionFlags ValidRestrictionFlags
		{
			get
			{
				return RestrictionFlags.Length | RestrictionFlags.MinLength | RestrictionFlags.MaxLength | RestrictionFlags.Pattern | RestrictionFlags.Enumeration | RestrictionFlags.WhiteSpace;
			}
		}

		// Token: 0x06001B7B RID: 7035 RVA: 0x0009E3DC File Offset: 0x0009C5DC
		internal override Exception TryParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, out object typedValue)
		{
			typedValue = null;
			Exception ex = DatatypeImplementation.stringFacetsChecker.CheckLexicalFacets(ref s, this);
			if (ex == null)
			{
				ex = DatatypeImplementation.stringFacetsChecker.CheckValueFacets(s, this);
				if (ex == null)
				{
					typedValue = s;
					return null;
				}
			}
			return ex;
		}
	}
}
