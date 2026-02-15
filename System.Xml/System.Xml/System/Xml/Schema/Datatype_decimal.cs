using System;

namespace System.Xml.Schema
{
	// Token: 0x02000240 RID: 576
	internal class Datatype_decimal : Datatype_anySimpleType
	{
		// Token: 0x06001B9E RID: 7070 RVA: 0x0009E5EE File Offset: 0x0009C7EE
		internal override XmlValueConverter CreateValueConverter(XmlSchemaType schemaType)
		{
			return XmlNumeric10Converter.Create(schemaType);
		}

		// Token: 0x17000657 RID: 1623
		// (get) Token: 0x06001B9F RID: 7071 RVA: 0x0009E5F6 File Offset: 0x0009C7F6
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return Datatype_decimal.numeric10FacetsChecker;
			}
		}

		// Token: 0x17000658 RID: 1624
		// (get) Token: 0x06001BA0 RID: 7072 RVA: 0x00042EF5 File Offset: 0x000410F5
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.Decimal;
			}
		}

		// Token: 0x17000659 RID: 1625
		// (get) Token: 0x06001BA1 RID: 7073 RVA: 0x0009E5FD File Offset: 0x0009C7FD
		public override Type ValueType
		{
			get
			{
				return Datatype_decimal.atomicValueType;
			}
		}

		// Token: 0x1700065A RID: 1626
		// (get) Token: 0x06001BA2 RID: 7074 RVA: 0x0009E604 File Offset: 0x0009C804
		internal override Type ListValueType
		{
			get
			{
				return Datatype_decimal.listValueType;
			}
		}

		// Token: 0x1700065B RID: 1627
		// (get) Token: 0x06001BA3 RID: 7075 RVA: 0x0003A73C File Offset: 0x0003893C
		internal override XmlSchemaWhiteSpace BuiltInWhitespaceFacet
		{
			get
			{
				return XmlSchemaWhiteSpace.Collapse;
			}
		}

		// Token: 0x1700065C RID: 1628
		// (get) Token: 0x06001BA4 RID: 7076 RVA: 0x0009E60B File Offset: 0x0009C80B
		internal override RestrictionFlags ValidRestrictionFlags
		{
			get
			{
				return RestrictionFlags.Pattern | RestrictionFlags.Enumeration | RestrictionFlags.WhiteSpace | RestrictionFlags.MaxInclusive | RestrictionFlags.MaxExclusive | RestrictionFlags.MinInclusive | RestrictionFlags.MinExclusive | RestrictionFlags.TotalDigits | RestrictionFlags.FractionDigits;
			}
		}

		// Token: 0x06001BA5 RID: 7077 RVA: 0x0009E614 File Offset: 0x0009C814
		internal override int Compare(object value1, object value2)
		{
			return ((decimal)value1).CompareTo(value2);
		}

		// Token: 0x06001BA6 RID: 7078 RVA: 0x0009E630 File Offset: 0x0009C830
		internal override Exception TryParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, out object typedValue)
		{
			typedValue = null;
			Exception ex = Datatype_decimal.numeric10FacetsChecker.CheckLexicalFacets(ref s, this);
			if (ex == null)
			{
				decimal num;
				ex = XmlConvert.TryToDecimal(s, out num);
				if (ex == null)
				{
					ex = Datatype_decimal.numeric10FacetsChecker.CheckValueFacets(num, this);
					if (ex == null)
					{
						typedValue = num;
						return null;
					}
				}
			}
			return ex;
		}

		// Token: 0x04000C26 RID: 3110
		private static readonly Type atomicValueType = typeof(decimal);

		// Token: 0x04000C27 RID: 3111
		private static readonly Type listValueType = typeof(decimal[]);

		// Token: 0x04000C28 RID: 3112
		private static readonly FacetsChecker numeric10FacetsChecker = new Numeric10FacetsChecker(decimal.MinValue, decimal.MaxValue);
	}
}
