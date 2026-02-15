using System;

namespace System.Xml.Schema
{
	// Token: 0x02000253 RID: 595
	internal class Datatype_anyURI : Datatype_anySimpleType
	{
		// Token: 0x06001BEE RID: 7150 RVA: 0x0009E6B8 File Offset: 0x0009C8B8
		internal override XmlValueConverter CreateValueConverter(XmlSchemaType schemaType)
		{
			return XmlMiscConverter.Create(schemaType);
		}

		// Token: 0x1700067E RID: 1662
		// (get) Token: 0x06001BEF RID: 7151 RVA: 0x0009E3D4 File Offset: 0x0009C5D4
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return DatatypeImplementation.stringFacetsChecker;
			}
		}

		// Token: 0x1700067F RID: 1663
		// (get) Token: 0x06001BF0 RID: 7152 RVA: 0x0009EB90 File Offset: 0x0009CD90
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.AnyUri;
			}
		}

		// Token: 0x17000680 RID: 1664
		// (get) Token: 0x06001BF1 RID: 7153 RVA: 0x0009EB94 File Offset: 0x0009CD94
		public override Type ValueType
		{
			get
			{
				return Datatype_anyURI.atomicValueType;
			}
		}

		// Token: 0x17000681 RID: 1665
		// (get) Token: 0x06001BF2 RID: 7154 RVA: 0x0000EFDF File Offset: 0x0000D1DF
		internal override bool HasValueFacets
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000682 RID: 1666
		// (get) Token: 0x06001BF3 RID: 7155 RVA: 0x0009EB9B File Offset: 0x0009CD9B
		internal override Type ListValueType
		{
			get
			{
				return Datatype_anyURI.listValueType;
			}
		}

		// Token: 0x17000683 RID: 1667
		// (get) Token: 0x06001BF4 RID: 7156 RVA: 0x0003A73C File Offset: 0x0003893C
		internal override XmlSchemaWhiteSpace BuiltInWhitespaceFacet
		{
			get
			{
				return XmlSchemaWhiteSpace.Collapse;
			}
		}

		// Token: 0x17000684 RID: 1668
		// (get) Token: 0x06001BF5 RID: 7157 RVA: 0x0009DD7D File Offset: 0x0009BF7D
		internal override RestrictionFlags ValidRestrictionFlags
		{
			get
			{
				return RestrictionFlags.Length | RestrictionFlags.MinLength | RestrictionFlags.MaxLength | RestrictionFlags.Pattern | RestrictionFlags.Enumeration | RestrictionFlags.WhiteSpace;
			}
		}

		// Token: 0x06001BF6 RID: 7158 RVA: 0x0009EBA2 File Offset: 0x0009CDA2
		internal override int Compare(object value1, object value2)
		{
			if (!((Uri)value1).Equals((Uri)value2))
			{
				return -1;
			}
			return 0;
		}

		// Token: 0x06001BF7 RID: 7159 RVA: 0x0009EBBC File Offset: 0x0009CDBC
		internal override Exception TryParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, out object typedValue)
		{
			typedValue = null;
			Exception ex = DatatypeImplementation.stringFacetsChecker.CheckLexicalFacets(ref s, this);
			if (ex == null)
			{
				Uri uri;
				ex = XmlConvert.TryToUri(s, out uri);
				if (ex == null)
				{
					string originalString = uri.OriginalString;
					ex = ((StringFacetsChecker)DatatypeImplementation.stringFacetsChecker).CheckValueFacets(originalString, this, false);
					if (ex == null)
					{
						typedValue = uri;
						return null;
					}
				}
			}
			return ex;
		}

		// Token: 0x04000C32 RID: 3122
		private static readonly Type atomicValueType = typeof(Uri);

		// Token: 0x04000C33 RID: 3123
		private static readonly Type listValueType = typeof(Uri[]);
	}
}
