using System;

namespace System.Xml.Schema
{
	// Token: 0x02000251 RID: 593
	internal class Datatype_hexBinary : Datatype_anySimpleType
	{
		// Token: 0x06001BD8 RID: 7128 RVA: 0x0009E6B8 File Offset: 0x0009C8B8
		internal override XmlValueConverter CreateValueConverter(XmlSchemaType schemaType)
		{
			return XmlMiscConverter.Create(schemaType);
		}

		// Token: 0x17000672 RID: 1650
		// (get) Token: 0x06001BD9 RID: 7129 RVA: 0x0009EA3E File Offset: 0x0009CC3E
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return DatatypeImplementation.binaryFacetsChecker;
			}
		}

		// Token: 0x17000673 RID: 1651
		// (get) Token: 0x06001BDA RID: 7130 RVA: 0x0009EA45 File Offset: 0x0009CC45
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.HexBinary;
			}
		}

		// Token: 0x17000674 RID: 1652
		// (get) Token: 0x06001BDB RID: 7131 RVA: 0x0009EA49 File Offset: 0x0009CC49
		public override Type ValueType
		{
			get
			{
				return Datatype_hexBinary.atomicValueType;
			}
		}

		// Token: 0x17000675 RID: 1653
		// (get) Token: 0x06001BDC RID: 7132 RVA: 0x0009EA50 File Offset: 0x0009CC50
		internal override Type ListValueType
		{
			get
			{
				return Datatype_hexBinary.listValueType;
			}
		}

		// Token: 0x17000676 RID: 1654
		// (get) Token: 0x06001BDD RID: 7133 RVA: 0x0003A73C File Offset: 0x0003893C
		internal override XmlSchemaWhiteSpace BuiltInWhitespaceFacet
		{
			get
			{
				return XmlSchemaWhiteSpace.Collapse;
			}
		}

		// Token: 0x17000677 RID: 1655
		// (get) Token: 0x06001BDE RID: 7134 RVA: 0x0009DD7D File Offset: 0x0009BF7D
		internal override RestrictionFlags ValidRestrictionFlags
		{
			get
			{
				return RestrictionFlags.Length | RestrictionFlags.MinLength | RestrictionFlags.MaxLength | RestrictionFlags.Pattern | RestrictionFlags.Enumeration | RestrictionFlags.WhiteSpace;
			}
		}

		// Token: 0x06001BDF RID: 7135 RVA: 0x0009EA57 File Offset: 0x0009CC57
		internal override int Compare(object value1, object value2)
		{
			return base.Compare((byte[])value1, (byte[])value2);
		}

		// Token: 0x06001BE0 RID: 7136 RVA: 0x0009EA6C File Offset: 0x0009CC6C
		internal override Exception TryParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, out object typedValue)
		{
			typedValue = null;
			Exception ex = DatatypeImplementation.binaryFacetsChecker.CheckLexicalFacets(ref s, this);
			if (ex == null)
			{
				byte[] array = null;
				try
				{
					array = XmlConvert.FromBinHexString(s, false);
				}
				catch (ArgumentException ex)
				{
					return ex;
				}
				catch (XmlException ex)
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

		// Token: 0x04000C2E RID: 3118
		private static readonly Type atomicValueType = typeof(byte[]);

		// Token: 0x04000C2F RID: 3119
		private static readonly Type listValueType = typeof(byte[][]);
	}
}
