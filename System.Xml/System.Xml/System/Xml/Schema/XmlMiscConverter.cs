using System;
using System.Xml.XPath;

namespace System.Xml.Schema
{
	// Token: 0x02000317 RID: 791
	internal class XmlMiscConverter : XmlBaseConverter
	{
		// Token: 0x060023D9 RID: 9177 RVA: 0x000C802C File Offset: 0x000C622C
		protected XmlMiscConverter(XmlSchemaType schemaType)
			: base(schemaType)
		{
		}

		// Token: 0x060023DA RID: 9178 RVA: 0x000C9C12 File Offset: 0x000C7E12
		public static XmlValueConverter Create(XmlSchemaType schemaType)
		{
			return new XmlMiscConverter(schemaType);
		}

		// Token: 0x060023DB RID: 9179 RVA: 0x000C9C1C File Offset: 0x000C7E1C
		public override string ToString(object value, IXmlNamespaceResolver nsResolver)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			Type type = value.GetType();
			if (type == XmlBaseConverter.ByteArrayType)
			{
				XmlTypeCode xmlTypeCode = base.TypeCode;
				if (xmlTypeCode == XmlTypeCode.HexBinary)
				{
					return XmlConvert.ToBinHexString((byte[])value);
				}
				if (xmlTypeCode == XmlTypeCode.Base64Binary)
				{
					return XmlBaseConverter.Base64BinaryToString((byte[])value);
				}
			}
			if (type == XmlBaseConverter.StringType)
			{
				return (string)value;
			}
			if (XmlBaseConverter.IsDerivedFrom(type, XmlBaseConverter.UriType) && base.TypeCode == XmlTypeCode.AnyUri)
			{
				return XmlBaseConverter.AnyUriToString((Uri)value);
			}
			if (type == XmlBaseConverter.TimeSpanType)
			{
				XmlTypeCode xmlTypeCode = base.TypeCode;
				if (xmlTypeCode == XmlTypeCode.Duration)
				{
					return XmlBaseConverter.DurationToString((TimeSpan)value);
				}
				if (xmlTypeCode == XmlTypeCode.YearMonthDuration)
				{
					return XmlBaseConverter.YearMonthDurationToString((TimeSpan)value);
				}
				if (xmlTypeCode == XmlTypeCode.DayTimeDuration)
				{
					return XmlBaseConverter.DayTimeDurationToString((TimeSpan)value);
				}
			}
			if (XmlBaseConverter.IsDerivedFrom(type, XmlBaseConverter.XmlQualifiedNameType))
			{
				XmlTypeCode xmlTypeCode = base.TypeCode;
				if (xmlTypeCode == XmlTypeCode.QName)
				{
					return XmlBaseConverter.QNameToString((XmlQualifiedName)value, nsResolver);
				}
				if (xmlTypeCode == XmlTypeCode.Notation)
				{
					return XmlBaseConverter.QNameToString((XmlQualifiedName)value, nsResolver);
				}
			}
			return (string)this.ChangeTypeWildcardDestination(value, XmlBaseConverter.StringType, nsResolver);
		}

		// Token: 0x060023DC RID: 9180 RVA: 0x000C9D3C File Offset: 0x000C7F3C
		public override object ChangeType(string value, Type destinationType, IXmlNamespaceResolver nsResolver)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (destinationType == XmlBaseConverter.ObjectType)
			{
				destinationType = base.DefaultClrType;
			}
			if (destinationType == XmlBaseConverter.ByteArrayType)
			{
				XmlTypeCode xmlTypeCode = base.TypeCode;
				if (xmlTypeCode == XmlTypeCode.HexBinary)
				{
					return XmlBaseConverter.StringToHexBinary(value);
				}
				if (xmlTypeCode == XmlTypeCode.Base64Binary)
				{
					return XmlBaseConverter.StringToBase64Binary(value);
				}
			}
			if (destinationType == XmlBaseConverter.XmlQualifiedNameType)
			{
				XmlTypeCode xmlTypeCode = base.TypeCode;
				if (xmlTypeCode == XmlTypeCode.QName)
				{
					return XmlBaseConverter.StringToQName(value, nsResolver);
				}
				if (xmlTypeCode == XmlTypeCode.Notation)
				{
					return XmlBaseConverter.StringToQName(value, nsResolver);
				}
			}
			if (destinationType == XmlBaseConverter.StringType)
			{
				return value;
			}
			if (destinationType == XmlBaseConverter.TimeSpanType)
			{
				XmlTypeCode xmlTypeCode = base.TypeCode;
				if (xmlTypeCode == XmlTypeCode.Duration)
				{
					return XmlBaseConverter.StringToDuration(value);
				}
				if (xmlTypeCode == XmlTypeCode.YearMonthDuration)
				{
					return XmlBaseConverter.StringToYearMonthDuration(value);
				}
				if (xmlTypeCode == XmlTypeCode.DayTimeDuration)
				{
					return XmlBaseConverter.StringToDayTimeDuration(value);
				}
			}
			if (destinationType == XmlBaseConverter.UriType && base.TypeCode == XmlTypeCode.AnyUri)
			{
				return XmlConvert.ToUri(value);
			}
			if (destinationType == XmlBaseConverter.XmlAtomicValueType)
			{
				return new XmlAtomicValue(base.SchemaType, value, nsResolver);
			}
			return this.ChangeTypeWildcardSource(value, destinationType, nsResolver);
		}

		// Token: 0x060023DD RID: 9181 RVA: 0x000C9E74 File Offset: 0x000C8074
		public override object ChangeType(object value, Type destinationType, IXmlNamespaceResolver nsResolver)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			Type type = value.GetType();
			if (destinationType == XmlBaseConverter.ObjectType)
			{
				destinationType = base.DefaultClrType;
			}
			if (destinationType == XmlBaseConverter.ByteArrayType)
			{
				if (type == XmlBaseConverter.ByteArrayType)
				{
					XmlTypeCode xmlTypeCode = base.TypeCode;
					if (xmlTypeCode == XmlTypeCode.HexBinary)
					{
						return (byte[])value;
					}
					if (xmlTypeCode == XmlTypeCode.Base64Binary)
					{
						return (byte[])value;
					}
				}
				if (type == XmlBaseConverter.StringType)
				{
					XmlTypeCode xmlTypeCode = base.TypeCode;
					if (xmlTypeCode == XmlTypeCode.HexBinary)
					{
						return XmlBaseConverter.StringToHexBinary((string)value);
					}
					if (xmlTypeCode == XmlTypeCode.Base64Binary)
					{
						return XmlBaseConverter.StringToBase64Binary((string)value);
					}
				}
			}
			if (destinationType == XmlBaseConverter.XmlQualifiedNameType)
			{
				if (type == XmlBaseConverter.StringType)
				{
					XmlTypeCode xmlTypeCode = base.TypeCode;
					if (xmlTypeCode == XmlTypeCode.QName)
					{
						return XmlBaseConverter.StringToQName((string)value, nsResolver);
					}
					if (xmlTypeCode == XmlTypeCode.Notation)
					{
						return XmlBaseConverter.StringToQName((string)value, nsResolver);
					}
				}
				if (XmlBaseConverter.IsDerivedFrom(type, XmlBaseConverter.XmlQualifiedNameType))
				{
					XmlTypeCode xmlTypeCode = base.TypeCode;
					if (xmlTypeCode == XmlTypeCode.QName)
					{
						return (XmlQualifiedName)value;
					}
					if (xmlTypeCode == XmlTypeCode.Notation)
					{
						return (XmlQualifiedName)value;
					}
				}
			}
			if (destinationType == XmlBaseConverter.StringType)
			{
				return this.ToString(value, nsResolver);
			}
			if (destinationType == XmlBaseConverter.TimeSpanType)
			{
				if (type == XmlBaseConverter.StringType)
				{
					XmlTypeCode xmlTypeCode = base.TypeCode;
					if (xmlTypeCode == XmlTypeCode.Duration)
					{
						return XmlBaseConverter.StringToDuration((string)value);
					}
					if (xmlTypeCode == XmlTypeCode.YearMonthDuration)
					{
						return XmlBaseConverter.StringToYearMonthDuration((string)value);
					}
					if (xmlTypeCode == XmlTypeCode.DayTimeDuration)
					{
						return XmlBaseConverter.StringToDayTimeDuration((string)value);
					}
				}
				if (type == XmlBaseConverter.TimeSpanType)
				{
					XmlTypeCode xmlTypeCode = base.TypeCode;
					if (xmlTypeCode == XmlTypeCode.Duration)
					{
						return (TimeSpan)value;
					}
					if (xmlTypeCode == XmlTypeCode.YearMonthDuration)
					{
						return (TimeSpan)value;
					}
					if (xmlTypeCode == XmlTypeCode.DayTimeDuration)
					{
						return (TimeSpan)value;
					}
				}
			}
			if (destinationType == XmlBaseConverter.UriType)
			{
				if (type == XmlBaseConverter.StringType && base.TypeCode == XmlTypeCode.AnyUri)
				{
					return XmlConvert.ToUri((string)value);
				}
				if (XmlBaseConverter.IsDerivedFrom(type, XmlBaseConverter.UriType) && base.TypeCode == XmlTypeCode.AnyUri)
				{
					return (Uri)value;
				}
			}
			if (destinationType == XmlBaseConverter.XmlAtomicValueType)
			{
				if (type == XmlBaseConverter.ByteArrayType)
				{
					XmlTypeCode xmlTypeCode = base.TypeCode;
					if (xmlTypeCode == XmlTypeCode.HexBinary)
					{
						return new XmlAtomicValue(base.SchemaType, value);
					}
					if (xmlTypeCode == XmlTypeCode.Base64Binary)
					{
						return new XmlAtomicValue(base.SchemaType, value);
					}
				}
				if (type == XmlBaseConverter.StringType)
				{
					return new XmlAtomicValue(base.SchemaType, (string)value, nsResolver);
				}
				if (type == XmlBaseConverter.TimeSpanType)
				{
					XmlTypeCode xmlTypeCode = base.TypeCode;
					if (xmlTypeCode == XmlTypeCode.Duration)
					{
						return new XmlAtomicValue(base.SchemaType, value);
					}
					if (xmlTypeCode == XmlTypeCode.YearMonthDuration)
					{
						return new XmlAtomicValue(base.SchemaType, value);
					}
					if (xmlTypeCode == XmlTypeCode.DayTimeDuration)
					{
						return new XmlAtomicValue(base.SchemaType, value);
					}
				}
				if (XmlBaseConverter.IsDerivedFrom(type, XmlBaseConverter.UriType) && base.TypeCode == XmlTypeCode.AnyUri)
				{
					return new XmlAtomicValue(base.SchemaType, value);
				}
				if (type == XmlBaseConverter.XmlAtomicValueType)
				{
					return (XmlAtomicValue)value;
				}
				if (XmlBaseConverter.IsDerivedFrom(type, XmlBaseConverter.XmlQualifiedNameType))
				{
					XmlTypeCode xmlTypeCode = base.TypeCode;
					if (xmlTypeCode == XmlTypeCode.QName)
					{
						return new XmlAtomicValue(base.SchemaType, value, nsResolver);
					}
					if (xmlTypeCode == XmlTypeCode.Notation)
					{
						return new XmlAtomicValue(base.SchemaType, value, nsResolver);
					}
				}
			}
			if (destinationType == XmlBaseConverter.XPathItemType && type == XmlBaseConverter.XmlAtomicValueType)
			{
				return (XmlAtomicValue)value;
			}
			if (destinationType == XmlBaseConverter.XPathItemType)
			{
				return (XPathItem)this.ChangeType(value, XmlBaseConverter.XmlAtomicValueType, nsResolver);
			}
			if (type == XmlBaseConverter.XmlAtomicValueType)
			{
				return ((XmlAtomicValue)value).ValueAs(destinationType, nsResolver);
			}
			return this.ChangeListType(value, destinationType, nsResolver);
		}

		// Token: 0x060023DE RID: 9182 RVA: 0x000CA239 File Offset: 0x000C8439
		private object ChangeTypeWildcardDestination(object value, Type destinationType, IXmlNamespaceResolver nsResolver)
		{
			if (value.GetType() == XmlBaseConverter.XmlAtomicValueType)
			{
				return ((XmlAtomicValue)value).ValueAs(destinationType, nsResolver);
			}
			return this.ChangeListType(value, destinationType, nsResolver);
		}

		// Token: 0x060023DF RID: 9183 RVA: 0x000CA264 File Offset: 0x000C8464
		private object ChangeTypeWildcardSource(object value, Type destinationType, IXmlNamespaceResolver nsResolver)
		{
			if (destinationType == XmlBaseConverter.XPathItemType)
			{
				return (XPathItem)this.ChangeType(value, XmlBaseConverter.XmlAtomicValueType, nsResolver);
			}
			return this.ChangeListType(value, destinationType, nsResolver);
		}
	}
}
