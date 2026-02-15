using System;

namespace System.Xml.Schema
{
	// Token: 0x02000313 RID: 787
	internal class XmlNumeric10Converter : XmlBaseConverter
	{
		// Token: 0x060023A0 RID: 9120 RVA: 0x000C802C File Offset: 0x000C622C
		protected XmlNumeric10Converter(XmlSchemaType schemaType)
			: base(schemaType)
		{
		}

		// Token: 0x060023A1 RID: 9121 RVA: 0x000C8035 File Offset: 0x000C6235
		public static XmlValueConverter Create(XmlSchemaType schemaType)
		{
			return new XmlNumeric10Converter(schemaType);
		}

		// Token: 0x060023A2 RID: 9122 RVA: 0x000C803D File Offset: 0x000C623D
		public override decimal ToDecimal(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (base.TypeCode == XmlTypeCode.Decimal)
			{
				return XmlConvert.ToDecimal(value);
			}
			return XmlConvert.ToInteger(value);
		}

		// Token: 0x060023A3 RID: 9123 RVA: 0x000C8064 File Offset: 0x000C6264
		public override decimal ToDecimal(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			Type type = value.GetType();
			if (type == XmlBaseConverter.DecimalType)
			{
				return (decimal)value;
			}
			if (type == XmlBaseConverter.Int32Type)
			{
				return (int)value;
			}
			if (type == XmlBaseConverter.Int64Type)
			{
				return (long)value;
			}
			if (type == XmlBaseConverter.StringType)
			{
				return this.ToDecimal((string)value);
			}
			if (type == XmlBaseConverter.XmlAtomicValueType)
			{
				return (decimal)((XmlAtomicValue)value).ValueAs(XmlBaseConverter.DecimalType);
			}
			return (decimal)this.ChangeTypeWildcardDestination(value, XmlBaseConverter.DecimalType, null);
		}

		// Token: 0x060023A4 RID: 9124 RVA: 0x000C811B File Offset: 0x000C631B
		public override int ToInt32(long value)
		{
			return XmlBaseConverter.Int64ToInt32(value);
		}

		// Token: 0x060023A5 RID: 9125 RVA: 0x000C8123 File Offset: 0x000C6323
		public override int ToInt32(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (base.TypeCode == XmlTypeCode.Decimal)
			{
				return XmlBaseConverter.DecimalToInt32(XmlConvert.ToDecimal(value));
			}
			return XmlConvert.ToInt32(value);
		}

		// Token: 0x060023A6 RID: 9126 RVA: 0x000C8150 File Offset: 0x000C6350
		public override int ToInt32(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			Type type = value.GetType();
			if (type == XmlBaseConverter.DecimalType)
			{
				return XmlBaseConverter.DecimalToInt32((decimal)value);
			}
			if (type == XmlBaseConverter.Int32Type)
			{
				return (int)value;
			}
			if (type == XmlBaseConverter.Int64Type)
			{
				return XmlBaseConverter.Int64ToInt32((long)value);
			}
			if (type == XmlBaseConverter.StringType)
			{
				return this.ToInt32((string)value);
			}
			if (type == XmlBaseConverter.XmlAtomicValueType)
			{
				return ((XmlAtomicValue)value).ValueAsInt;
			}
			return (int)this.ChangeTypeWildcardDestination(value, XmlBaseConverter.Int32Type, null);
		}

		// Token: 0x060023A7 RID: 9127 RVA: 0x000C81FD File Offset: 0x000C63FD
		public override long ToInt64(int value)
		{
			return (long)value;
		}

		// Token: 0x060023A8 RID: 9128 RVA: 0x000C8201 File Offset: 0x000C6401
		public override long ToInt64(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (base.TypeCode == XmlTypeCode.Decimal)
			{
				return XmlBaseConverter.DecimalToInt64(XmlConvert.ToDecimal(value));
			}
			return XmlConvert.ToInt64(value);
		}

		// Token: 0x060023A9 RID: 9129 RVA: 0x000C8230 File Offset: 0x000C6430
		public override long ToInt64(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			Type type = value.GetType();
			if (type == XmlBaseConverter.DecimalType)
			{
				return XmlBaseConverter.DecimalToInt64((decimal)value);
			}
			if (type == XmlBaseConverter.Int32Type)
			{
				return (long)((int)value);
			}
			if (type == XmlBaseConverter.Int64Type)
			{
				return (long)value;
			}
			if (type == XmlBaseConverter.StringType)
			{
				return this.ToInt64((string)value);
			}
			if (type == XmlBaseConverter.XmlAtomicValueType)
			{
				return ((XmlAtomicValue)value).ValueAsLong;
			}
			return (long)this.ChangeTypeWildcardDestination(value, XmlBaseConverter.Int64Type, null);
		}

		// Token: 0x060023AA RID: 9130 RVA: 0x000C82D9 File Offset: 0x000C64D9
		public override string ToString(decimal value)
		{
			if (base.TypeCode == XmlTypeCode.Decimal)
			{
				return XmlConvert.ToString(value);
			}
			return XmlConvert.ToString(decimal.Truncate(value));
		}

		// Token: 0x060023AB RID: 9131 RVA: 0x000C82F7 File Offset: 0x000C64F7
		public override string ToString(int value)
		{
			return XmlConvert.ToString(value);
		}

		// Token: 0x060023AC RID: 9132 RVA: 0x000C82FF File Offset: 0x000C64FF
		public override string ToString(long value)
		{
			return XmlConvert.ToString(value);
		}

		// Token: 0x060023AD RID: 9133 RVA: 0x000C8308 File Offset: 0x000C6508
		public override string ToString(object value, IXmlNamespaceResolver nsResolver)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			Type type = value.GetType();
			if (type == XmlBaseConverter.DecimalType)
			{
				return this.ToString((decimal)value);
			}
			if (type == XmlBaseConverter.Int32Type)
			{
				return XmlConvert.ToString((int)value);
			}
			if (type == XmlBaseConverter.Int64Type)
			{
				return XmlConvert.ToString((long)value);
			}
			if (type == XmlBaseConverter.StringType)
			{
				return (string)value;
			}
			if (type == XmlBaseConverter.XmlAtomicValueType)
			{
				return ((XmlAtomicValue)value).Value;
			}
			return (string)this.ChangeTypeWildcardDestination(value, XmlBaseConverter.StringType, nsResolver);
		}

		// Token: 0x060023AE RID: 9134 RVA: 0x000C83B8 File Offset: 0x000C65B8
		public override object ChangeType(decimal value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (destinationType == XmlBaseConverter.ObjectType)
			{
				destinationType = base.DefaultClrType;
			}
			if (destinationType == XmlBaseConverter.DecimalType)
			{
				return value;
			}
			if (destinationType == XmlBaseConverter.Int32Type)
			{
				return XmlBaseConverter.DecimalToInt32(value);
			}
			if (destinationType == XmlBaseConverter.Int64Type)
			{
				return XmlBaseConverter.DecimalToInt64(value);
			}
			if (destinationType == XmlBaseConverter.StringType)
			{
				return this.ToString(value);
			}
			if (destinationType == XmlBaseConverter.XmlAtomicValueType)
			{
				return new XmlAtomicValue(base.SchemaType, value);
			}
			if (destinationType == XmlBaseConverter.XPathItemType)
			{
				return new XmlAtomicValue(base.SchemaType, value);
			}
			return this.ChangeTypeWildcardSource(value, destinationType, null);
		}

		// Token: 0x060023AF RID: 9135 RVA: 0x000C8498 File Offset: 0x000C6698
		public override object ChangeType(int value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (destinationType == XmlBaseConverter.ObjectType)
			{
				destinationType = base.DefaultClrType;
			}
			if (destinationType == XmlBaseConverter.DecimalType)
			{
				return value;
			}
			if (destinationType == XmlBaseConverter.Int32Type)
			{
				return value;
			}
			if (destinationType == XmlBaseConverter.Int64Type)
			{
				return (long)value;
			}
			if (destinationType == XmlBaseConverter.StringType)
			{
				return XmlConvert.ToString(value);
			}
			if (destinationType == XmlBaseConverter.XmlAtomicValueType)
			{
				return new XmlAtomicValue(base.SchemaType, value);
			}
			if (destinationType == XmlBaseConverter.XPathItemType)
			{
				return new XmlAtomicValue(base.SchemaType, value);
			}
			return this.ChangeTypeWildcardSource(value, destinationType, null);
		}

		// Token: 0x060023B0 RID: 9136 RVA: 0x000C8568 File Offset: 0x000C6768
		public override object ChangeType(long value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (destinationType == XmlBaseConverter.ObjectType)
			{
				destinationType = base.DefaultClrType;
			}
			if (destinationType == XmlBaseConverter.DecimalType)
			{
				return value;
			}
			if (destinationType == XmlBaseConverter.Int32Type)
			{
				return XmlBaseConverter.Int64ToInt32(value);
			}
			if (destinationType == XmlBaseConverter.Int64Type)
			{
				return value;
			}
			if (destinationType == XmlBaseConverter.StringType)
			{
				return XmlConvert.ToString(value);
			}
			if (destinationType == XmlBaseConverter.XmlAtomicValueType)
			{
				return new XmlAtomicValue(base.SchemaType, value);
			}
			if (destinationType == XmlBaseConverter.XPathItemType)
			{
				return new XmlAtomicValue(base.SchemaType, value);
			}
			return this.ChangeTypeWildcardSource(value, destinationType, null);
		}

		// Token: 0x060023B1 RID: 9137 RVA: 0x000C863C File Offset: 0x000C683C
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
			if (destinationType == XmlBaseConverter.DecimalType)
			{
				return this.ToDecimal(value);
			}
			if (destinationType == XmlBaseConverter.Int32Type)
			{
				return this.ToInt32(value);
			}
			if (destinationType == XmlBaseConverter.Int64Type)
			{
				return this.ToInt64(value);
			}
			if (destinationType == XmlBaseConverter.StringType)
			{
				return value;
			}
			if (destinationType == XmlBaseConverter.XmlAtomicValueType)
			{
				return new XmlAtomicValue(base.SchemaType, value);
			}
			if (destinationType == XmlBaseConverter.XPathItemType)
			{
				return new XmlAtomicValue(base.SchemaType, value);
			}
			return this.ChangeTypeWildcardSource(value, destinationType, nsResolver);
		}

		// Token: 0x060023B2 RID: 9138 RVA: 0x000C871C File Offset: 0x000C691C
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
			if (destinationType == XmlBaseConverter.DecimalType)
			{
				return this.ToDecimal(value);
			}
			if (destinationType == XmlBaseConverter.Int32Type)
			{
				return this.ToInt32(value);
			}
			if (destinationType == XmlBaseConverter.Int64Type)
			{
				return this.ToInt64(value);
			}
			if (destinationType == XmlBaseConverter.StringType)
			{
				return this.ToString(value, nsResolver);
			}
			if (destinationType == XmlBaseConverter.XmlAtomicValueType)
			{
				if (type == XmlBaseConverter.DecimalType)
				{
					return new XmlAtomicValue(base.SchemaType, value);
				}
				if (type == XmlBaseConverter.Int32Type)
				{
					return new XmlAtomicValue(base.SchemaType, (int)value);
				}
				if (type == XmlBaseConverter.Int64Type)
				{
					return new XmlAtomicValue(base.SchemaType, (long)value);
				}
				if (type == XmlBaseConverter.StringType)
				{
					return new XmlAtomicValue(base.SchemaType, (string)value);
				}
				if (type == XmlBaseConverter.XmlAtomicValueType)
				{
					return (XmlAtomicValue)value;
				}
			}
			if (destinationType == XmlBaseConverter.XPathItemType)
			{
				if (type == XmlBaseConverter.DecimalType)
				{
					return new XmlAtomicValue(base.SchemaType, value);
				}
				if (type == XmlBaseConverter.Int32Type)
				{
					return new XmlAtomicValue(base.SchemaType, (int)value);
				}
				if (type == XmlBaseConverter.Int64Type)
				{
					return new XmlAtomicValue(base.SchemaType, (long)value);
				}
				if (type == XmlBaseConverter.StringType)
				{
					return new XmlAtomicValue(base.SchemaType, (string)value);
				}
				if (type == XmlBaseConverter.XmlAtomicValueType)
				{
					return (XmlAtomicValue)value;
				}
			}
			if (destinationType == XmlBaseConverter.ByteType)
			{
				return XmlBaseConverter.Int32ToByte(this.ToInt32(value));
			}
			if (destinationType == XmlBaseConverter.Int16Type)
			{
				return XmlBaseConverter.Int32ToInt16(this.ToInt32(value));
			}
			if (destinationType == XmlBaseConverter.SByteType)
			{
				return XmlBaseConverter.Int32ToSByte(this.ToInt32(value));
			}
			if (destinationType == XmlBaseConverter.UInt16Type)
			{
				return XmlBaseConverter.Int32ToUInt16(this.ToInt32(value));
			}
			if (destinationType == XmlBaseConverter.UInt32Type)
			{
				return XmlBaseConverter.Int64ToUInt32(this.ToInt64(value));
			}
			if (destinationType == XmlBaseConverter.UInt64Type)
			{
				return XmlBaseConverter.DecimalToUInt64(this.ToDecimal(value));
			}
			if (type == XmlBaseConverter.ByteType)
			{
				return this.ChangeType((int)((byte)value), destinationType);
			}
			if (type == XmlBaseConverter.Int16Type)
			{
				return this.ChangeType((int)((short)value), destinationType);
			}
			if (type == XmlBaseConverter.SByteType)
			{
				return this.ChangeType((int)((sbyte)value), destinationType);
			}
			if (type == XmlBaseConverter.UInt16Type)
			{
				return this.ChangeType((int)((ushort)value), destinationType);
			}
			if (type == XmlBaseConverter.UInt32Type)
			{
				return this.ChangeType((long)((ulong)((uint)value)), destinationType);
			}
			if (type == XmlBaseConverter.UInt64Type)
			{
				return this.ChangeType((ulong)value, destinationType);
			}
			return this.ChangeListType(value, destinationType, nsResolver);
		}

		// Token: 0x060023B3 RID: 9139 RVA: 0x000C8A6C File Offset: 0x000C6C6C
		private object ChangeTypeWildcardDestination(object value, Type destinationType, IXmlNamespaceResolver nsResolver)
		{
			Type type = value.GetType();
			if (type == XmlBaseConverter.ByteType)
			{
				return this.ChangeType((int)((byte)value), destinationType);
			}
			if (type == XmlBaseConverter.Int16Type)
			{
				return this.ChangeType((int)((short)value), destinationType);
			}
			if (type == XmlBaseConverter.SByteType)
			{
				return this.ChangeType((int)((sbyte)value), destinationType);
			}
			if (type == XmlBaseConverter.UInt16Type)
			{
				return this.ChangeType((int)((ushort)value), destinationType);
			}
			if (type == XmlBaseConverter.UInt32Type)
			{
				return this.ChangeType((long)((ulong)((uint)value)), destinationType);
			}
			if (type == XmlBaseConverter.UInt64Type)
			{
				return this.ChangeType((ulong)value, destinationType);
			}
			return this.ChangeListType(value, destinationType, nsResolver);
		}

		// Token: 0x060023B4 RID: 9140 RVA: 0x000C8B34 File Offset: 0x000C6D34
		private object ChangeTypeWildcardSource(object value, Type destinationType, IXmlNamespaceResolver nsResolver)
		{
			if (destinationType == XmlBaseConverter.ByteType)
			{
				return XmlBaseConverter.Int32ToByte(this.ToInt32(value));
			}
			if (destinationType == XmlBaseConverter.Int16Type)
			{
				return XmlBaseConverter.Int32ToInt16(this.ToInt32(value));
			}
			if (destinationType == XmlBaseConverter.SByteType)
			{
				return XmlBaseConverter.Int32ToSByte(this.ToInt32(value));
			}
			if (destinationType == XmlBaseConverter.UInt16Type)
			{
				return XmlBaseConverter.Int32ToUInt16(this.ToInt32(value));
			}
			if (destinationType == XmlBaseConverter.UInt32Type)
			{
				return XmlBaseConverter.Int64ToUInt32(this.ToInt64(value));
			}
			if (destinationType == XmlBaseConverter.UInt64Type)
			{
				return XmlBaseConverter.DecimalToUInt64(this.ToDecimal(value));
			}
			return this.ChangeListType(value, destinationType, nsResolver);
		}
	}
}
