using System;

namespace System.Xml.Schema
{
	// Token: 0x0200023D RID: 573
	internal class Datatype_boolean : Datatype_anySimpleType
	{
		// Token: 0x06001B7D RID: 7037 RVA: 0x0009E415 File Offset: 0x0009C615
		internal override XmlValueConverter CreateValueConverter(XmlSchemaType schemaType)
		{
			return XmlBooleanConverter.Create(schemaType);
		}

		// Token: 0x17000645 RID: 1605
		// (get) Token: 0x06001B7E RID: 7038 RVA: 0x0009D89E File Offset: 0x0009BA9E
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return DatatypeImplementation.miscFacetsChecker;
			}
		}

		// Token: 0x17000646 RID: 1606
		// (get) Token: 0x06001B7F RID: 7039 RVA: 0x00043146 File Offset: 0x00041346
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.Boolean;
			}
		}

		// Token: 0x17000647 RID: 1607
		// (get) Token: 0x06001B80 RID: 7040 RVA: 0x0009E41D File Offset: 0x0009C61D
		public override Type ValueType
		{
			get
			{
				return Datatype_boolean.atomicValueType;
			}
		}

		// Token: 0x17000648 RID: 1608
		// (get) Token: 0x06001B81 RID: 7041 RVA: 0x0009E424 File Offset: 0x0009C624
		internal override Type ListValueType
		{
			get
			{
				return Datatype_boolean.listValueType;
			}
		}

		// Token: 0x17000649 RID: 1609
		// (get) Token: 0x06001B82 RID: 7042 RVA: 0x0003A73C File Offset: 0x0003893C
		internal override XmlSchemaWhiteSpace BuiltInWhitespaceFacet
		{
			get
			{
				return XmlSchemaWhiteSpace.Collapse;
			}
		}

		// Token: 0x1700064A RID: 1610
		// (get) Token: 0x06001B83 RID: 7043 RVA: 0x0009E42B File Offset: 0x0009C62B
		internal override RestrictionFlags ValidRestrictionFlags
		{
			get
			{
				return RestrictionFlags.Pattern | RestrictionFlags.WhiteSpace;
			}
		}

		// Token: 0x06001B84 RID: 7044 RVA: 0x0009E430 File Offset: 0x0009C630
		internal override int Compare(object value1, object value2)
		{
			return ((bool)value1).CompareTo(value2);
		}

		// Token: 0x06001B85 RID: 7045 RVA: 0x0009E44C File Offset: 0x0009C64C
		internal override Exception TryParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, out object typedValue)
		{
			typedValue = null;
			Exception ex = DatatypeImplementation.miscFacetsChecker.CheckLexicalFacets(ref s, this);
			if (ex == null)
			{
				bool flag;
				ex = XmlConvert.TryToBoolean(s, out flag);
				if (ex == null)
				{
					typedValue = flag;
					return null;
				}
			}
			return ex;
		}

		// Token: 0x04000C20 RID: 3104
		private static readonly Type atomicValueType = typeof(bool);

		// Token: 0x04000C21 RID: 3105
		private static readonly Type listValueType = typeof(bool[]);
	}
}
