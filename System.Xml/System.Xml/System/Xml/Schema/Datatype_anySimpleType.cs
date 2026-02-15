using System;

namespace System.Xml.Schema
{
	// Token: 0x02000239 RID: 569
	internal class Datatype_anySimpleType : DatatypeImplementation
	{
		// Token: 0x06001B61 RID: 7009 RVA: 0x0009E358 File Offset: 0x0009C558
		internal override XmlValueConverter CreateValueConverter(XmlSchemaType schemaType)
		{
			return XmlUntypedConverter.Untyped;
		}

		// Token: 0x17000635 RID: 1589
		// (get) Token: 0x06001B62 RID: 7010 RVA: 0x0009D89E File Offset: 0x0009BA9E
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return DatatypeImplementation.miscFacetsChecker;
			}
		}

		// Token: 0x17000636 RID: 1590
		// (get) Token: 0x06001B63 RID: 7011 RVA: 0x0009E35F File Offset: 0x0009C55F
		public override Type ValueType
		{
			get
			{
				return Datatype_anySimpleType.atomicValueType;
			}
		}

		// Token: 0x17000637 RID: 1591
		// (get) Token: 0x06001B64 RID: 7012 RVA: 0x0003CFC9 File Offset: 0x0003B1C9
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.AnyAtomicType;
			}
		}

		// Token: 0x17000638 RID: 1592
		// (get) Token: 0x06001B65 RID: 7013 RVA: 0x0009E366 File Offset: 0x0009C566
		internal override Type ListValueType
		{
			get
			{
				return Datatype_anySimpleType.listValueType;
			}
		}

		// Token: 0x17000639 RID: 1593
		// (get) Token: 0x06001B66 RID: 7014 RVA: 0x0000A2FC File Offset: 0x000084FC
		public override XmlTokenizedType TokenizedType
		{
			get
			{
				return XmlTokenizedType.None;
			}
		}

		// Token: 0x1700063A RID: 1594
		// (get) Token: 0x06001B67 RID: 7015 RVA: 0x0000C1F5 File Offset: 0x0000A3F5
		internal override RestrictionFlags ValidRestrictionFlags
		{
			get
			{
				return (RestrictionFlags)0;
			}
		}

		// Token: 0x1700063B RID: 1595
		// (get) Token: 0x06001B68 RID: 7016 RVA: 0x0003A73C File Offset: 0x0003893C
		internal override XmlSchemaWhiteSpace BuiltInWhitespaceFacet
		{
			get
			{
				return XmlSchemaWhiteSpace.Collapse;
			}
		}

		// Token: 0x06001B69 RID: 7017 RVA: 0x0009E36D File Offset: 0x0009C56D
		internal override int Compare(object value1, object value2)
		{
			return string.Compare(value1.ToString(), value2.ToString(), StringComparison.Ordinal);
		}

		// Token: 0x06001B6A RID: 7018 RVA: 0x0009E381 File Offset: 0x0009C581
		internal override Exception TryParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, out object typedValue)
		{
			typedValue = XmlComplianceUtil.NonCDataNormalize(s);
			return null;
		}

		// Token: 0x04000C1E RID: 3102
		private static readonly Type atomicValueType = typeof(string);

		// Token: 0x04000C1F RID: 3103
		private static readonly Type listValueType = typeof(string[]);
	}
}
