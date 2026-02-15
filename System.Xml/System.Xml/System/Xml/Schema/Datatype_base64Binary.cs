using System;

namespace System.Xml.Schema
{
	// Token: 0x02000252 RID: 594
	internal class Datatype_base64Binary : Datatype_anySimpleType
	{
		// Token: 0x06001BE3 RID: 7139 RVA: 0x0009E6B8 File Offset: 0x0009C8B8
		internal override XmlValueConverter CreateValueConverter(XmlSchemaType schemaType)
		{
			return XmlMiscConverter.Create(schemaType);
		}

		// Token: 0x17000678 RID: 1656
		// (get) Token: 0x06001BE4 RID: 7140 RVA: 0x0009EA3E File Offset: 0x0009CC3E
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return DatatypeImplementation.binaryFacetsChecker;
			}
		}

		// Token: 0x17000679 RID: 1657
		// (get) Token: 0x06001BE5 RID: 7141 RVA: 0x0009EAF4 File Offset: 0x0009CCF4
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.Base64Binary;
			}
		}

		// Token: 0x1700067A RID: 1658
		// (get) Token: 0x06001BE6 RID: 7142 RVA: 0x0009EAF8 File Offset: 0x0009CCF8
		public override Type ValueType
		{
			get
			{
				return Datatype_base64Binary.atomicValueType;
			}
		}

		// Token: 0x1700067B RID: 1659
		// (get) Token: 0x06001BE7 RID: 7143 RVA: 0x0009EAFF File Offset: 0x0009CCFF
		internal override Type ListValueType
		{
			get
			{
				return Datatype_base64Binary.listValueType;
			}
		}

		// Token: 0x1700067C RID: 1660
		// (get) Token: 0x06001BE8 RID: 7144 RVA: 0x0003A73C File Offset: 0x0003893C
		internal override XmlSchemaWhiteSpace BuiltInWhitespaceFacet
		{
			get
			{
				return XmlSchemaWhiteSpace.Collapse;
			}
		}

		// Token: 0x1700067D RID: 1661
		// (get) Token: 0x06001BE9 RID: 7145 RVA: 0x0009DD7D File Offset: 0x0009BF7D
		internal override RestrictionFlags ValidRestrictionFlags
		{
			get
			{
				return RestrictionFlags.Length | RestrictionFlags.MinLength | RestrictionFlags.MaxLength | RestrictionFlags.Pattern | RestrictionFlags.Enumeration | RestrictionFlags.WhiteSpace;
			}
		}

		// Token: 0x06001BEA RID: 7146 RVA: 0x0009EA57 File Offset: 0x0009CC57
		internal override int Compare(object value1, object value2)
		{
			return base.Compare((byte[])value1, (byte[])value2);
		}

		// Token: 0x06001BEB RID: 7147 RVA: 0x0009EB08 File Offset: 0x0009CD08
		internal override Exception TryParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, out object typedValue)
		{
			typedValue = null;
			Exception ex = DatatypeImplementation.binaryFacetsChecker.CheckLexicalFacets(ref s, this);
			if (ex == null)
			{
				byte[] array = null;
				try
				{
					array = Convert.FromBase64String(s);
				}
				catch (ArgumentException ex)
				{
					return ex;
				}
				catch (FormatException ex)
				{
					return ex;
				}
				ex = DatatypeImplementation.binaryFacetsChecker.CheckValueFacets(array, this);
				if (ex == null)
				{
					typedValue = array;
					return null;
				}
			}
			return ex;
		}

		// Token: 0x04000C30 RID: 3120
		private static readonly Type atomicValueType = typeof(byte[]);

		// Token: 0x04000C31 RID: 3121
		private static readonly Type listValueType = typeof(byte[][]);
	}
}
