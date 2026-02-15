using System;

namespace System.Xml.Schema
{
	// Token: 0x02000260 RID: 608
	internal class Datatype_NOTATION : Datatype_anySimpleType
	{
		// Token: 0x06001C24 RID: 7204 RVA: 0x0009E6B8 File Offset: 0x0009C8B8
		internal override XmlValueConverter CreateValueConverter(XmlSchemaType schemaType)
		{
			return XmlMiscConverter.Create(schemaType);
		}

		// Token: 0x1700069F RID: 1695
		// (get) Token: 0x06001C25 RID: 7205 RVA: 0x0009EC2E File Offset: 0x0009CE2E
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return DatatypeImplementation.qnameFacetsChecker;
			}
		}

		// Token: 0x170006A0 RID: 1696
		// (get) Token: 0x06001C26 RID: 7206 RVA: 0x0009ED7D File Offset: 0x0009CF7D
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.Notation;
			}
		}

		// Token: 0x170006A1 RID: 1697
		// (get) Token: 0x06001C27 RID: 7207 RVA: 0x0003B3B1 File Offset: 0x000395B1
		public override XmlTokenizedType TokenizedType
		{
			get
			{
				return XmlTokenizedType.NOTATION;
			}
		}

		// Token: 0x170006A2 RID: 1698
		// (get) Token: 0x06001C28 RID: 7208 RVA: 0x0009DD7D File Offset: 0x0009BF7D
		internal override RestrictionFlags ValidRestrictionFlags
		{
			get
			{
				return RestrictionFlags.Length | RestrictionFlags.MinLength | RestrictionFlags.MaxLength | RestrictionFlags.Pattern | RestrictionFlags.Enumeration | RestrictionFlags.WhiteSpace;
			}
		}

		// Token: 0x170006A3 RID: 1699
		// (get) Token: 0x06001C29 RID: 7209 RVA: 0x0009ED81 File Offset: 0x0009CF81
		public override Type ValueType
		{
			get
			{
				return Datatype_NOTATION.atomicValueType;
			}
		}

		// Token: 0x170006A4 RID: 1700
		// (get) Token: 0x06001C2A RID: 7210 RVA: 0x0009ED88 File Offset: 0x0009CF88
		internal override Type ListValueType
		{
			get
			{
				return Datatype_NOTATION.listValueType;
			}
		}

		// Token: 0x170006A5 RID: 1701
		// (get) Token: 0x06001C2B RID: 7211 RVA: 0x0003A73C File Offset: 0x0003893C
		internal override XmlSchemaWhiteSpace BuiltInWhitespaceFacet
		{
			get
			{
				return XmlSchemaWhiteSpace.Collapse;
			}
		}

		// Token: 0x06001C2C RID: 7212 RVA: 0x0009ED90 File Offset: 0x0009CF90
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

		// Token: 0x06001C2D RID: 7213 RVA: 0x0009EE14 File Offset: 0x0009D014
		internal override void VerifySchemaValid(XmlSchemaObjectTable notations, XmlSchemaObject caller)
		{
			for (Datatype_NOTATION datatype_NOTATION = this; datatype_NOTATION != null; datatype_NOTATION = (Datatype_NOTATION)datatype_NOTATION.Base)
			{
				if (datatype_NOTATION.Restriction != null && (datatype_NOTATION.Restriction.Flags & RestrictionFlags.Enumeration) != (RestrictionFlags)0)
				{
					for (int i = 0; i < datatype_NOTATION.Restriction.Enumeration.Count; i++)
					{
						XmlQualifiedName xmlQualifiedName = (XmlQualifiedName)datatype_NOTATION.Restriction.Enumeration[i];
						if (!notations.Contains(xmlQualifiedName))
						{
							throw new XmlSchemaException("NOTATION cannot be used directly in a schema; only data types derived from NOTATION by specifying an enumeration value can be used in a schema. All enumeration facet values must match the name of a notation declared in the current schema.", caller);
						}
					}
					return;
				}
			}
			throw new XmlSchemaException("NOTATION cannot be used directly in a schema; only data types derived from NOTATION by specifying an enumeration value can be used in a schema. All enumeration facet values must match the name of a notation declared in the current schema.", caller);
		}

		// Token: 0x04000C36 RID: 3126
		private static readonly Type atomicValueType = typeof(XmlQualifiedName);

		// Token: 0x04000C37 RID: 3127
		private static readonly Type listValueType = typeof(XmlQualifiedName[]);
	}
}
