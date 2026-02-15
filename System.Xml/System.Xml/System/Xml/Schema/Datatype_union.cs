using System;

namespace System.Xml.Schema
{
	// Token: 0x02000238 RID: 568
	internal class Datatype_union : Datatype_anySimpleType
	{
		// Token: 0x06001B53 RID: 6995 RVA: 0x0009E079 File Offset: 0x0009C279
		internal override XmlValueConverter CreateValueConverter(XmlSchemaType schemaType)
		{
			return XmlUnionConverter.Create(schemaType);
		}

		// Token: 0x06001B54 RID: 6996 RVA: 0x0009E081 File Offset: 0x0009C281
		internal Datatype_union(XmlSchemaSimpleType[] types)
		{
			this.types = types;
		}

		// Token: 0x06001B55 RID: 6997 RVA: 0x0009E090 File Offset: 0x0009C290
		internal override int Compare(object value1, object value2)
		{
			XsdSimpleValue xsdSimpleValue = value1 as XsdSimpleValue;
			XsdSimpleValue xsdSimpleValue2 = value2 as XsdSimpleValue;
			if (xsdSimpleValue == null || xsdSimpleValue2 == null)
			{
				return -1;
			}
			XmlSchemaType xmlType = xsdSimpleValue.XmlType;
			XmlSchemaType xmlType2 = xsdSimpleValue2.XmlType;
			if (xmlType == xmlType2)
			{
				return xmlType.Datatype.Compare(xsdSimpleValue.TypedValue, xsdSimpleValue2.TypedValue);
			}
			return -1;
		}

		// Token: 0x1700062F RID: 1583
		// (get) Token: 0x06001B56 RID: 6998 RVA: 0x0009E0DE File Offset: 0x0009C2DE
		public override Type ValueType
		{
			get
			{
				return Datatype_union.atomicValueType;
			}
		}

		// Token: 0x17000630 RID: 1584
		// (get) Token: 0x06001B57 RID: 6999 RVA: 0x0003CFC9 File Offset: 0x0003B1C9
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.AnyAtomicType;
			}
		}

		// Token: 0x17000631 RID: 1585
		// (get) Token: 0x06001B58 RID: 7000 RVA: 0x0009E0E5 File Offset: 0x0009C2E5
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return DatatypeImplementation.unionFacetsChecker;
			}
		}

		// Token: 0x17000632 RID: 1586
		// (get) Token: 0x06001B59 RID: 7001 RVA: 0x0009E0EC File Offset: 0x0009C2EC
		internal override Type ListValueType
		{
			get
			{
				return Datatype_union.listValueType;
			}
		}

		// Token: 0x17000633 RID: 1587
		// (get) Token: 0x06001B5A RID: 7002 RVA: 0x0009E0F3 File Offset: 0x0009C2F3
		internal override RestrictionFlags ValidRestrictionFlags
		{
			get
			{
				return RestrictionFlags.Pattern | RestrictionFlags.Enumeration;
			}
		}

		// Token: 0x17000634 RID: 1588
		// (get) Token: 0x06001B5B RID: 7003 RVA: 0x0009E0F7 File Offset: 0x0009C2F7
		internal XmlSchemaSimpleType[] BaseMemberTypes
		{
			get
			{
				return this.types;
			}
		}

		// Token: 0x06001B5C RID: 7004 RVA: 0x0009E100 File Offset: 0x0009C300
		internal bool HasAtomicMembers()
		{
			for (int i = 0; i < this.types.Length; i++)
			{
				if (this.types[i].Datatype.Variety == XmlSchemaDatatypeVariety.List)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001B5D RID: 7005 RVA: 0x0009E138 File Offset: 0x0009C338
		internal bool IsUnionBaseOf(DatatypeImplementation derivedType)
		{
			for (int i = 0; i < this.types.Length; i++)
			{
				if (derivedType.IsDerivedFrom(this.types[i].Datatype))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001B5E RID: 7006 RVA: 0x0009E170 File Offset: 0x0009C370
		internal override Exception TryParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, out object typedValue)
		{
			XmlSchemaSimpleType xmlSchemaSimpleType = null;
			typedValue = null;
			Exception ex = DatatypeImplementation.unionFacetsChecker.CheckLexicalFacets(ref s, this);
			if (ex == null)
			{
				for (int i = 0; i < this.types.Length; i++)
				{
					if (this.types[i].Datatype.TryParseValue(s, nameTable, nsmgr, out typedValue) == null)
					{
						xmlSchemaSimpleType = this.types[i];
						break;
					}
				}
				if (xmlSchemaSimpleType == null)
				{
					ex = new XmlSchemaException("The value '{0}' is not valid according to any of the memberTypes of the union.", s);
				}
				else
				{
					typedValue = new XsdSimpleValue(xmlSchemaSimpleType, typedValue);
					ex = DatatypeImplementation.unionFacetsChecker.CheckValueFacets(typedValue, this);
					if (ex == null)
					{
						return null;
					}
				}
			}
			return ex;
		}

		// Token: 0x06001B5F RID: 7007 RVA: 0x0009E200 File Offset: 0x0009C400
		internal override Exception TryParseValue(object value, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, out object typedValue)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			typedValue = null;
			string text = value as string;
			if (text != null)
			{
				return this.TryParseValue(text, nameTable, nsmgr, out typedValue);
			}
			object obj = null;
			XmlSchemaSimpleType xmlSchemaSimpleType = null;
			for (int i = 0; i < this.types.Length; i++)
			{
				if (this.types[i].Datatype.TryParseValue(value, nameTable, nsmgr, out obj) == null)
				{
					xmlSchemaSimpleType = this.types[i];
					break;
				}
			}
			Exception ex;
			if (obj != null)
			{
				try
				{
					if (this.HasLexicalFacets)
					{
						string text2 = (string)this.ValueConverter.ChangeType(obj, typeof(string), nsmgr);
						ex = DatatypeImplementation.unionFacetsChecker.CheckLexicalFacets(ref text2, this);
						if (ex != null)
						{
							return ex;
						}
					}
					typedValue = new XsdSimpleValue(xmlSchemaSimpleType, obj);
					if (this.HasValueFacets)
					{
						ex = DatatypeImplementation.unionFacetsChecker.CheckValueFacets(typedValue, this);
						if (ex != null)
						{
							return ex;
						}
					}
					return null;
				}
				catch (FormatException ex)
				{
				}
				catch (InvalidCastException ex)
				{
				}
				catch (OverflowException ex)
				{
				}
				catch (ArgumentException ex)
				{
				}
				return ex;
			}
			ex = new XmlSchemaException("The value '{0}' is not valid according to any of the memberTypes of the union.", value.ToString());
			return ex;
		}

		// Token: 0x04000C1B RID: 3099
		private static readonly Type atomicValueType = typeof(object);

		// Token: 0x04000C1C RID: 3100
		private static readonly Type listValueType = typeof(object[]);

		// Token: 0x04000C1D RID: 3101
		private XmlSchemaSimpleType[] types;
	}
}
