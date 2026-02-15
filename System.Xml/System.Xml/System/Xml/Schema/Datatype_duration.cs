using System;

namespace System.Xml.Schema
{
	// Token: 0x02000241 RID: 577
	internal class Datatype_duration : Datatype_anySimpleType
	{
		// Token: 0x06001BA9 RID: 7081 RVA: 0x0009E6B8 File Offset: 0x0009C8B8
		internal override XmlValueConverter CreateValueConverter(XmlSchemaType schemaType)
		{
			return XmlMiscConverter.Create(schemaType);
		}

		// Token: 0x1700065D RID: 1629
		// (get) Token: 0x06001BAA RID: 7082 RVA: 0x0009E6C0 File Offset: 0x0009C8C0
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return DatatypeImplementation.durationFacetsChecker;
			}
		}

		// Token: 0x1700065E RID: 1630
		// (get) Token: 0x06001BAB RID: 7083 RVA: 0x0003B62F File Offset: 0x0003982F
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.Duration;
			}
		}

		// Token: 0x1700065F RID: 1631
		// (get) Token: 0x06001BAC RID: 7084 RVA: 0x0009E6C7 File Offset: 0x0009C8C7
		public override Type ValueType
		{
			get
			{
				return Datatype_duration.atomicValueType;
			}
		}

		// Token: 0x17000660 RID: 1632
		// (get) Token: 0x06001BAD RID: 7085 RVA: 0x0009E6CE File Offset: 0x0009C8CE
		internal override Type ListValueType
		{
			get
			{
				return Datatype_duration.listValueType;
			}
		}

		// Token: 0x17000661 RID: 1633
		// (get) Token: 0x06001BAE RID: 7086 RVA: 0x0003A73C File Offset: 0x0003893C
		internal override XmlSchemaWhiteSpace BuiltInWhitespaceFacet
		{
			get
			{
				return XmlSchemaWhiteSpace.Collapse;
			}
		}

		// Token: 0x17000662 RID: 1634
		// (get) Token: 0x06001BAF RID: 7087 RVA: 0x0009E4C7 File Offset: 0x0009C6C7
		internal override RestrictionFlags ValidRestrictionFlags
		{
			get
			{
				return RestrictionFlags.Pattern | RestrictionFlags.Enumeration | RestrictionFlags.WhiteSpace | RestrictionFlags.MaxInclusive | RestrictionFlags.MaxExclusive | RestrictionFlags.MinInclusive | RestrictionFlags.MinExclusive;
			}
		}

		// Token: 0x06001BB0 RID: 7088 RVA: 0x0009E6D8 File Offset: 0x0009C8D8
		internal override int Compare(object value1, object value2)
		{
			return ((TimeSpan)value1).CompareTo(value2);
		}

		// Token: 0x06001BB1 RID: 7089 RVA: 0x0009E6F4 File Offset: 0x0009C8F4
		internal override Exception TryParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, out object typedValue)
		{
			typedValue = null;
			if (s == null || s.Length == 0)
			{
				return new XmlSchemaException("The attribute value cannot be empty.", string.Empty);
			}
			Exception ex = DatatypeImplementation.durationFacetsChecker.CheckLexicalFacets(ref s, this);
			if (ex == null)
			{
				TimeSpan timeSpan;
				ex = XmlConvert.TryToTimeSpan(s, out timeSpan);
				if (ex == null)
				{
					ex = DatatypeImplementation.durationFacetsChecker.CheckValueFacets(timeSpan, this);
					if (ex == null)
					{
						typedValue = timeSpan;
						return null;
					}
				}
			}
			return ex;
		}

		// Token: 0x04000C29 RID: 3113
		private static readonly Type atomicValueType = typeof(TimeSpan);

		// Token: 0x04000C2A RID: 3114
		private static readonly Type listValueType = typeof(TimeSpan[]);
	}
}
