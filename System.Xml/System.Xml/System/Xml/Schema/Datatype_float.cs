using System;

namespace System.Xml.Schema
{
	// Token: 0x0200023E RID: 574
	internal class Datatype_float : Datatype_anySimpleType
	{
		// Token: 0x06001B88 RID: 7048 RVA: 0x0009E4A6 File Offset: 0x0009C6A6
		internal override XmlValueConverter CreateValueConverter(XmlSchemaType schemaType)
		{
			return XmlNumeric2Converter.Create(schemaType);
		}

		// Token: 0x1700064B RID: 1611
		// (get) Token: 0x06001B89 RID: 7049 RVA: 0x0009E4AE File Offset: 0x0009C6AE
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return DatatypeImplementation.numeric2FacetsChecker;
			}
		}

		// Token: 0x1700064C RID: 1612
		// (get) Token: 0x06001B8A RID: 7050 RVA: 0x0009E4B5 File Offset: 0x0009C6B5
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.Float;
			}
		}

		// Token: 0x1700064D RID: 1613
		// (get) Token: 0x06001B8B RID: 7051 RVA: 0x0009E4B9 File Offset: 0x0009C6B9
		public override Type ValueType
		{
			get
			{
				return Datatype_float.atomicValueType;
			}
		}

		// Token: 0x1700064E RID: 1614
		// (get) Token: 0x06001B8C RID: 7052 RVA: 0x0009E4C0 File Offset: 0x0009C6C0
		internal override Type ListValueType
		{
			get
			{
				return Datatype_float.listValueType;
			}
		}

		// Token: 0x1700064F RID: 1615
		// (get) Token: 0x06001B8D RID: 7053 RVA: 0x0003A73C File Offset: 0x0003893C
		internal override XmlSchemaWhiteSpace BuiltInWhitespaceFacet
		{
			get
			{
				return XmlSchemaWhiteSpace.Collapse;
			}
		}

		// Token: 0x17000650 RID: 1616
		// (get) Token: 0x06001B8E RID: 7054 RVA: 0x0009E4C7 File Offset: 0x0009C6C7
		internal override RestrictionFlags ValidRestrictionFlags
		{
			get
			{
				return RestrictionFlags.Pattern | RestrictionFlags.Enumeration | RestrictionFlags.WhiteSpace | RestrictionFlags.MaxInclusive | RestrictionFlags.MaxExclusive | RestrictionFlags.MinInclusive | RestrictionFlags.MinExclusive;
			}
		}

		// Token: 0x06001B8F RID: 7055 RVA: 0x0009E4D0 File Offset: 0x0009C6D0
		internal override int Compare(object value1, object value2)
		{
			return ((float)value1).CompareTo(value2);
		}

		// Token: 0x06001B90 RID: 7056 RVA: 0x0009E4EC File Offset: 0x0009C6EC
		internal override Exception TryParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, out object typedValue)
		{
			typedValue = null;
			Exception ex = DatatypeImplementation.numeric2FacetsChecker.CheckLexicalFacets(ref s, this);
			if (ex == null)
			{
				float num;
				ex = XmlConvert.TryToSingle(s, out num);
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

		// Token: 0x04000C22 RID: 3106
		private static readonly Type atomicValueType = typeof(float);

		// Token: 0x04000C23 RID: 3107
		private static readonly Type listValueType = typeof(float[]);
	}
}
