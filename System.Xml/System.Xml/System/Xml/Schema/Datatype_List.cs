using System;
using System.Collections;

namespace System.Xml.Schema
{
	// Token: 0x02000237 RID: 567
	internal class Datatype_List : Datatype_anySimpleType
	{
		// Token: 0x06001B47 RID: 6983 RVA: 0x0009DBD8 File Offset: 0x0009BDD8
		internal override XmlValueConverter CreateValueConverter(XmlSchemaType schemaType)
		{
			XmlSchemaType xmlSchemaType = null;
			XmlSchemaComplexType xmlSchemaComplexType = schemaType as XmlSchemaComplexType;
			XmlSchemaSimpleType xmlSchemaSimpleType;
			if (xmlSchemaComplexType != null)
			{
				do
				{
					xmlSchemaSimpleType = xmlSchemaComplexType.BaseXmlSchemaType as XmlSchemaSimpleType;
					if (xmlSchemaSimpleType != null)
					{
						break;
					}
					xmlSchemaComplexType = xmlSchemaComplexType.BaseXmlSchemaType as XmlSchemaComplexType;
					if (xmlSchemaComplexType == null)
					{
						break;
					}
				}
				while (xmlSchemaComplexType != XmlSchemaComplexType.AnyType);
			}
			else
			{
				xmlSchemaSimpleType = schemaType as XmlSchemaSimpleType;
			}
			if (xmlSchemaSimpleType != null)
			{
				XmlSchemaSimpleTypeList xmlSchemaSimpleTypeList;
				for (;;)
				{
					xmlSchemaSimpleTypeList = xmlSchemaSimpleType.Content as XmlSchemaSimpleTypeList;
					if (xmlSchemaSimpleTypeList != null)
					{
						break;
					}
					xmlSchemaSimpleType = xmlSchemaSimpleType.BaseXmlSchemaType as XmlSchemaSimpleType;
					if (xmlSchemaSimpleType == null || xmlSchemaSimpleType == DatatypeImplementation.AnySimpleType)
					{
						goto IL_006D;
					}
				}
				xmlSchemaType = xmlSchemaSimpleTypeList.BaseItemType;
			}
			IL_006D:
			if (xmlSchemaType == null)
			{
				xmlSchemaType = DatatypeImplementation.GetSimpleTypeFromTypeCode(schemaType.Datatype.TypeCode);
			}
			return XmlListConverter.Create(xmlSchemaType.ValueConverter);
		}

		// Token: 0x06001B48 RID: 6984 RVA: 0x0009DC71 File Offset: 0x0009BE71
		internal Datatype_List(DatatypeImplementation type, int minListSize)
		{
			this.itemType = type;
			this.minListSize = minListSize;
		}

		// Token: 0x06001B49 RID: 6985 RVA: 0x0009DC88 File Offset: 0x0009BE88
		internal override int Compare(object value1, object value2)
		{
			Array array = (Array)value1;
			Array array2 = (Array)value2;
			if (array.Length != array2.Length)
			{
				return -1;
			}
			XmlAtomicValue[] array3 = array as XmlAtomicValue[];
			if (array3 != null)
			{
				XmlAtomicValue[] array4 = array2 as XmlAtomicValue[];
				for (int i = 0; i < array3.Length; i++)
				{
					XmlSchemaType xmlType = array3[i].XmlType;
					if (xmlType != array4[i].XmlType || !xmlType.Datatype.IsEqual(array3[i].TypedValue, array4[i].TypedValue))
					{
						return -1;
					}
				}
				return 0;
			}
			for (int j = 0; j < array.Length; j++)
			{
				if (this.itemType.Compare(array.GetValue(j), array2.GetValue(j)) != 0)
				{
					return -1;
				}
			}
			return 0;
		}

		// Token: 0x17000628 RID: 1576
		// (get) Token: 0x06001B4A RID: 6986 RVA: 0x0009DD47 File Offset: 0x0009BF47
		public override Type ValueType
		{
			get
			{
				return this.ListValueType;
			}
		}

		// Token: 0x17000629 RID: 1577
		// (get) Token: 0x06001B4B RID: 6987 RVA: 0x0009DD4F File Offset: 0x0009BF4F
		public override XmlTokenizedType TokenizedType
		{
			get
			{
				return this.itemType.TokenizedType;
			}
		}

		// Token: 0x1700062A RID: 1578
		// (get) Token: 0x06001B4C RID: 6988 RVA: 0x0009DD5C File Offset: 0x0009BF5C
		internal override Type ListValueType
		{
			get
			{
				return this.itemType.ListValueType;
			}
		}

		// Token: 0x1700062B RID: 1579
		// (get) Token: 0x06001B4D RID: 6989 RVA: 0x0009DD69 File Offset: 0x0009BF69
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return DatatypeImplementation.listFacetsChecker;
			}
		}

		// Token: 0x1700062C RID: 1580
		// (get) Token: 0x06001B4E RID: 6990 RVA: 0x0009DD70 File Offset: 0x0009BF70
		public override XmlTypeCode TypeCode
		{
			get
			{
				return this.itemType.TypeCode;
			}
		}

		// Token: 0x1700062D RID: 1581
		// (get) Token: 0x06001B4F RID: 6991 RVA: 0x0009DD7D File Offset: 0x0009BF7D
		internal override RestrictionFlags ValidRestrictionFlags
		{
			get
			{
				return RestrictionFlags.Length | RestrictionFlags.MinLength | RestrictionFlags.MaxLength | RestrictionFlags.Pattern | RestrictionFlags.Enumeration | RestrictionFlags.WhiteSpace;
			}
		}

		// Token: 0x1700062E RID: 1582
		// (get) Token: 0x06001B50 RID: 6992 RVA: 0x0009DD81 File Offset: 0x0009BF81
		internal DatatypeImplementation ItemType
		{
			get
			{
				return this.itemType;
			}
		}

		// Token: 0x06001B51 RID: 6993 RVA: 0x0009DD8C File Offset: 0x0009BF8C
		internal override Exception TryParseValue(object value, XmlNameTable nameTable, IXmlNamespaceResolver namespaceResolver, out object typedValue)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			string text = value as string;
			typedValue = null;
			if (text != null)
			{
				return this.TryParseValue(text, nameTable, namespaceResolver, out typedValue);
			}
			Exception ex;
			try
			{
				object obj = this.ValueConverter.ChangeType(value, this.ValueType, namespaceResolver);
				Array array = obj as Array;
				bool hasLexicalFacets = this.itemType.HasLexicalFacets;
				bool hasValueFacets = this.itemType.HasValueFacets;
				FacetsChecker facetsChecker = this.itemType.FacetsChecker;
				XmlValueConverter valueConverter = this.itemType.ValueConverter;
				for (int i = 0; i < array.Length; i++)
				{
					object value2 = array.GetValue(i);
					if (hasLexicalFacets)
					{
						string text2 = (string)valueConverter.ChangeType(value2, typeof(string), namespaceResolver);
						ex = facetsChecker.CheckLexicalFacets(ref text2, this.itemType);
						if (ex != null)
						{
							return ex;
						}
					}
					if (hasValueFacets)
					{
						ex = facetsChecker.CheckValueFacets(value2, this.itemType);
						if (ex != null)
						{
							return ex;
						}
					}
				}
				if (this.HasLexicalFacets)
				{
					string text3 = (string)this.ValueConverter.ChangeType(obj, typeof(string), namespaceResolver);
					ex = DatatypeImplementation.listFacetsChecker.CheckLexicalFacets(ref text3, this);
					if (ex != null)
					{
						return ex;
					}
				}
				if (this.HasValueFacets)
				{
					ex = DatatypeImplementation.listFacetsChecker.CheckValueFacets(obj, this);
					if (ex != null)
					{
						return ex;
					}
				}
				typedValue = obj;
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

		// Token: 0x06001B52 RID: 6994 RVA: 0x0009DF50 File Offset: 0x0009C150
		internal override Exception TryParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, out object typedValue)
		{
			typedValue = null;
			Exception ex = DatatypeImplementation.listFacetsChecker.CheckLexicalFacets(ref s, this);
			if (ex == null)
			{
				ArrayList arrayList = new ArrayList();
				object obj2;
				if (this.itemType.Variety == XmlSchemaDatatypeVariety.Union)
				{
					string[] array = XmlConvert.SplitString(s);
					for (int i = 0; i < array.Length; i++)
					{
						object obj;
						ex = this.itemType.TryParseValue(array[i], nameTable, nsmgr, out obj);
						if (ex != null)
						{
							return ex;
						}
						XsdSimpleValue xsdSimpleValue = (XsdSimpleValue)obj;
						arrayList.Add(new XmlAtomicValue(xsdSimpleValue.XmlType, xsdSimpleValue.TypedValue, nsmgr));
					}
					obj2 = arrayList.ToArray(typeof(XmlAtomicValue));
				}
				else
				{
					string[] array2 = XmlConvert.SplitString(s);
					for (int j = 0; j < array2.Length; j++)
					{
						ex = this.itemType.TryParseValue(array2[j], nameTable, nsmgr, out typedValue);
						if (ex != null)
						{
							return ex;
						}
						arrayList.Add(typedValue);
					}
					obj2 = arrayList.ToArray(this.itemType.ValueType);
				}
				if (arrayList.Count < this.minListSize)
				{
					return new XmlSchemaException("The attribute value cannot be empty.", string.Empty);
				}
				ex = DatatypeImplementation.listFacetsChecker.CheckValueFacets(obj2, this);
				if (ex == null)
				{
					typedValue = obj2;
					return null;
				}
			}
			return ex;
		}

		// Token: 0x04000C19 RID: 3097
		private DatatypeImplementation itemType;

		// Token: 0x04000C1A RID: 3098
		private int minListSize;
	}
}
