using System;

namespace System.Xml.Schema
{
	// Token: 0x02000254 RID: 596
	internal class Datatype_QName : Datatype_anySimpleType
	{
		// Token: 0x06001BFA RID: 7162 RVA: 0x0009E6B8 File Offset: 0x0009C8B8
		internal override XmlValueConverter CreateValueConverter(XmlSchemaType schemaType)
		{
			return XmlMiscConverter.Create(schemaType);
		}

		// Token: 0x17000685 RID: 1669
		// (get) Token: 0x06001BFB RID: 7163 RVA: 0x0009EC2E File Offset: 0x0009CE2E
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return DatatypeImplementation.qnameFacetsChecker;
			}
		}

		// Token: 0x17000686 RID: 1670
		// (get) Token: 0x06001BFC RID: 7164 RVA: 0x0009EC35 File Offset: 0x0009CE35
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.QName;
			}
		}

		// Token: 0x17000687 RID: 1671
		// (get) Token: 0x06001BFD RID: 7165 RVA: 0x0003CFC9 File Offset: 0x0003B1C9
		public override XmlTokenizedType TokenizedType
		{
			get
			{
				return XmlTokenizedType.QName;
			}
		}

		// Token: 0x17000688 RID: 1672
		// (get) Token: 0x06001BFE RID: 7166 RVA: 0x0009DD7D File Offset: 0x0009BF7D
		internal override RestrictionFlags ValidRestrictionFlags
		{
			get
			{
				return RestrictionFlags.Length | RestrictionFlags.MinLength | RestrictionFlags.MaxLength | RestrictionFlags.Pattern | RestrictionFlags.Enumeration | RestrictionFlags.WhiteSpace;
			}
		}

		// Token: 0x17000689 RID: 1673
		// (get) Token: 0x06001BFF RID: 7167 RVA: 0x0009EC39 File Offset: 0x0009CE39
		public override Type ValueType
		{
			get
			{
				return Datatype_QName.atomicValueType;
			}
		}

		// Token: 0x1700068A RID: 1674
		// (get) Token: 0x06001C00 RID: 7168 RVA: 0x0009EC40 File Offset: 0x0009CE40
		internal override Type ListValueType
		{
			get
			{
				return Datatype_QName.listValueType;
			}
		}

		// Token: 0x1700068B RID: 1675
		// (get) Token: 0x06001C01 RID: 7169 RVA: 0x0003A73C File Offset: 0x0003893C
		internal override XmlSchemaWhiteSpace BuiltInWhitespaceFacet
		{
			get
			{
				return XmlSchemaWhiteSpace.Collapse;
			}
		}

		// Token: 0x06001C02 RID: 7170 RVA: 0x0009EC48 File Offset: 0x0009CE48
		internal override Exception TryParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, out object typedValue)
		{
			typedValue = null;
			if (s == null || s.Length == 0)
			{
				return new XmlSchemaException("The attribute value cannot be empty.", string.Empty);
			}
			Exception ex = DatatypeImplementation.qnameFacetsChecker.CheckLexicalFacets(ref s, this);
			if (ex == null)
			{
				XmlQualifiedName xmlQualifiedName = null;
				try
				{
					string text;
					xmlQualifiedName = XmlQualifiedName.Parse(s, nsmgr, out text);
				}
				catch (ArgumentException ex)
				{
					return ex;
				}
				catch (XmlException ex)
				{
					return ex;
				}
				ex = DatatypeImplementation.qnameFacetsChecker.CheckValueFacets(xmlQualifiedName, this);
				if (ex == null)
				{
					typedValue = xmlQualifiedName;
					return null;
				}
			}
			return ex;
		}

		// Token: 0x04000C34 RID: 3124
		private static readonly Type atomicValueType = typeof(XmlQualifiedName);

		// Token: 0x04000C35 RID: 3125
		private static readonly Type listValueType = typeof(XmlQualifiedName[]);
	}
}
