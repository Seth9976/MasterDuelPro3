using System;

namespace System.Xml.Schema
{
	// Token: 0x0200023F RID: 575
	internal class Datatype_double : Datatype_anySimpleType
	{
		// Token: 0x06001B93 RID: 7059 RVA: 0x0009E4A6 File Offset: 0x0009C6A6
		internal override XmlValueConverter CreateValueConverter(XmlSchemaType schemaType)
		{
			return XmlNumeric2Converter.Create(schemaType);
		}

		// Token: 0x17000651 RID: 1617
		// (get) Token: 0x06001B94 RID: 7060 RVA: 0x0009E4AE File Offset: 0x0009C6AE
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return DatatypeImplementation.numeric2FacetsChecker;
			}
		}

		// Token: 0x17000652 RID: 1618
		// (get) Token: 0x06001B95 RID: 7061 RVA: 0x0009E556 File Offset: 0x0009C756
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.Double;
			}
		}

		// Token: 0x17000653 RID: 1619
		// (get) Token: 0x06001B96 RID: 7062 RVA: 0x0009E55A File Offset: 0x0009C75A
		public override Type ValueType
		{
			get
			{
				return Datatype_double.atomicValueType;
			}
		}

		// Token: 0x17000654 RID: 1620
		// (get) Token: 0x06001B97 RID: 7063 RVA: 0x0009E561 File Offset: 0x0009C761
		internal override Type ListValueType
		{
			get
			{
				return Datatype_double.listValueType;
			}
		}

		// Token: 0x17000655 RID: 1621
		// (get) Token: 0x06001B98 RID: 7064 RVA: 0x0003A73C File Offset: 0x0003893C
		internal override XmlSchemaWhiteSpace BuiltInWhitespaceFacet
		{
			get
			{
				return XmlSchemaWhiteSpace.Collapse;
			}
		}

		// Token: 0x17000656 RID: 1622
		// (get) Token: 0x06001B99 RID: 7065 RVA: 0x0009E4C7 File Offset: 0x0009C6C7
		internal override RestrictionFlags ValidRestrictionFlags
		{
			get
			{
				return RestrictionFlags.Pattern | RestrictionFlags.Enumeration | RestrictionFlags.WhiteSpace | RestrictionFlags.MaxInclusive | RestrictionFlags.MaxExclusive | RestrictionFlags.MinInclusive | RestrictionFlags.MinExclusive;
			}
		}

		// Token: 0x06001B9A RID: 7066 RVA: 0x0009E568 File Offset: 0x0009C768
		internal override int Compare(object value1, object value2)
		{
			return ((double)value1).CompareTo(value2);
		}

		// Token: 0x06001B9B RID: 7067 RVA: 0x0009E584 File Offset: 0x0009C784
		internal override Exception TryParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, out object typedValue)
		{
			typedValue = null;
			Exception ex = DatatypeImplementation.numeric2FacetsChecker.CheckLexicalFacets(ref s, this);
			if (ex == null)
			{
				double num;
				ex = XmlConvert.TryToDouble(s, out num);
				if (ex == null)
				{
					ex = DatatypeImplementation.numeric2FacetsChecker.CheckValueFacets(num, this);
					if (ex == null)
					{
						typedValue = num;
						return null;
					}
				}
			}
			return ex;
		}

		// Token: 0x04000C24 RID: 3108
		private static readonly Type atomicValueType = typeof(double);

		// Token: 0x04000C25 RID: 3109
		private static readonly Type listValueType = typeof(double[]);
	}
}
