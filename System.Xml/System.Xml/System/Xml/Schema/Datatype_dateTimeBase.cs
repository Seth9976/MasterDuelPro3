using System;

namespace System.Xml.Schema
{
	// Token: 0x02000244 RID: 580
	internal class Datatype_dateTimeBase : Datatype_anySimpleType
	{
		// Token: 0x06001BBA RID: 7098 RVA: 0x0009E874 File Offset: 0x0009CA74
		internal override XmlValueConverter CreateValueConverter(XmlSchemaType schemaType)
		{
			return XmlDateTimeConverter.Create(schemaType);
		}

		// Token: 0x17000665 RID: 1637
		// (get) Token: 0x06001BBB RID: 7099 RVA: 0x0009E87C File Offset: 0x0009CA7C
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return DatatypeImplementation.dateTimeFacetsChecker;
			}
		}

		// Token: 0x17000666 RID: 1638
		// (get) Token: 0x06001BBC RID: 7100 RVA: 0x0009E883 File Offset: 0x0009CA83
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.DateTime;
			}
		}

		// Token: 0x06001BBD RID: 7101 RVA: 0x0009E887 File Offset: 0x0009CA87
		internal Datatype_dateTimeBase(XsdDateTimeFlags dateTimeFlags)
		{
			this.dateTimeFlags = dateTimeFlags;
		}

		// Token: 0x17000667 RID: 1639
		// (get) Token: 0x06001BBE RID: 7102 RVA: 0x0009E896 File Offset: 0x0009CA96
		public override Type ValueType
		{
			get
			{
				return Datatype_dateTimeBase.atomicValueType;
			}
		}

		// Token: 0x17000668 RID: 1640
		// (get) Token: 0x06001BBF RID: 7103 RVA: 0x0009E89D File Offset: 0x0009CA9D
		internal override Type ListValueType
		{
			get
			{
				return Datatype_dateTimeBase.listValueType;
			}
		}

		// Token: 0x17000669 RID: 1641
		// (get) Token: 0x06001BC0 RID: 7104 RVA: 0x0003A73C File Offset: 0x0003893C
		internal override XmlSchemaWhiteSpace BuiltInWhitespaceFacet
		{
			get
			{
				return XmlSchemaWhiteSpace.Collapse;
			}
		}

		// Token: 0x1700066A RID: 1642
		// (get) Token: 0x06001BC1 RID: 7105 RVA: 0x0009E4C7 File Offset: 0x0009C6C7
		internal override RestrictionFlags ValidRestrictionFlags
		{
			get
			{
				return RestrictionFlags.Pattern | RestrictionFlags.Enumeration | RestrictionFlags.WhiteSpace | RestrictionFlags.MaxInclusive | RestrictionFlags.MaxExclusive | RestrictionFlags.MinInclusive | RestrictionFlags.MinExclusive;
			}
		}

		// Token: 0x06001BC2 RID: 7106 RVA: 0x0009E8A4 File Offset: 0x0009CAA4
		internal override int Compare(object value1, object value2)
		{
			DateTime dateTime = (DateTime)value1;
			DateTime dateTime2 = (DateTime)value2;
			if (dateTime.Kind == DateTimeKind.Unspecified || dateTime2.Kind == DateTimeKind.Unspecified)
			{
				return dateTime.CompareTo(dateTime2);
			}
			return dateTime.ToUniversalTime().CompareTo(dateTime2.ToUniversalTime());
		}

		// Token: 0x06001BC3 RID: 7107 RVA: 0x0009E8F0 File Offset: 0x0009CAF0
		internal override Exception TryParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, out object typedValue)
		{
			typedValue = null;
			Exception ex = DatatypeImplementation.dateTimeFacetsChecker.CheckLexicalFacets(ref s, this);
			if (ex == null)
			{
				XsdDateTime xsdDateTime;
				if (!XsdDateTime.TryParse(s, this.dateTimeFlags, out xsdDateTime))
				{
					ex = new FormatException(Res.GetString("The string '{0}' is not a valid {1} value.", new object[]
					{
						s,
						this.dateTimeFlags.ToString()
					}));
				}
				else
				{
					DateTime dateTime = DateTime.MinValue;
					try
					{
						dateTime = xsdDateTime;
					}
					catch (ArgumentException ex)
					{
						return ex;
					}
					ex = DatatypeImplementation.dateTimeFacetsChecker.CheckValueFacets(dateTime, this);
					if (ex == null)
					{
						typedValue = dateTime;
						return null;
					}
				}
			}
			return ex;
		}

		// Token: 0x04000C2B RID: 3115
		private static readonly Type atomicValueType = typeof(DateTime);

		// Token: 0x04000C2C RID: 3116
		private static readonly Type listValueType = typeof(DateTime[]);

		// Token: 0x04000C2D RID: 3117
		private XsdDateTimeFlags dateTimeFlags;
	}
}
