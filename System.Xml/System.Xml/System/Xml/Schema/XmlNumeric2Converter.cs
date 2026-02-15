using System;

namespace System.Xml.Schema
{
	// Token: 0x02000314 RID: 788
	internal class XmlNumeric2Converter : XmlBaseConverter
	{
		// Token: 0x060023B5 RID: 9141 RVA: 0x000C802C File Offset: 0x000C622C
		protected XmlNumeric2Converter(XmlSchemaType schemaType)
			: base(schemaType)
		{
		}

		// Token: 0x060023B6 RID: 9142 RVA: 0x000C8C04 File Offset: 0x000C6E04
		public static XmlValueConverter Create(XmlSchemaType schemaType)
		{
			return new XmlNumeric2Converter(schemaType);
		}

		// Token: 0x060023B7 RID: 9143 RVA: 0x000C8C0C File Offset: 0x000C6E0C
		public override double ToDouble(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (base.TypeCode == XmlTypeCode.Float)
			{
				return (double)XmlConvert.ToSingle(value);
			}
			return XmlConvert.ToDouble(value);
		}

		// Token: 0x060023B8 RID: 9144 RVA: 0x000C8C34 File Offset: 0x000C6E34
		public override double ToDouble(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			Type type = value.GetType();
			if (type == XmlBaseConverter.DoubleType)
			{
				return (double)value;
			}
			if (type == XmlBaseConverter.SingleType)
			{
				return (double)((float)value);
			}
			if (type == XmlBaseConverter.StringType)
			{
				return this.ToDouble((string)value);
			}
			if (type == XmlBaseConverter.XmlAtomicValueType)
			{
				return ((XmlAtomicValue)value).ValueAsDouble;
			}
			return (double)this.ChangeListType(value, XmlBaseConverter.DoubleType, null);
		}

		// Token: 0x060023B9 RID: 9145 RVA: 0x000C8CC4 File Offset: 0x000C6EC4
		public override float ToSingle(double value)
		{
			return (float)value;
		}

		// Token: 0x060023BA RID: 9146 RVA: 0x000C8CC9 File Offset: 0x000C6EC9
		public override float ToSingle(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (base.TypeCode == XmlTypeCode.Float)
			{
				return XmlConvert.ToSingle(value);
			}
			return (float)XmlConvert.ToDouble(value);
		}

		// Token: 0x060023BB RID: 9147 RVA: 0x000C8CF4 File Offset: 0x000C6EF4
		public override float ToSingle(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			Type type = value.GetType();
			if (type == XmlBaseConverter.DoubleType)
			{
				return (float)((double)value);
			}
			if (type == XmlBaseConverter.SingleType)
			{
				return (float)value;
			}
			if (type == XmlBaseConverter.StringType)
			{
				return this.ToSingle((string)value);
			}
			if (type == XmlBaseConverter.XmlAtomicValueType)
			{
				return (float)((XmlAtomicValue)value).ValueAs(XmlBaseConverter.SingleType);
			}
			return (float)this.ChangeListType(value, XmlBaseConverter.SingleType, null);
		}

		// Token: 0x060023BC RID: 9148 RVA: 0x000C8D8E File Offset: 0x000C6F8E
		public override string ToString(double value)
		{
			if (base.TypeCode == XmlTypeCode.Float)
			{
				return XmlConvert.ToString(this.ToSingle(value));
			}
			return XmlConvert.ToString(value);
		}

		// Token: 0x060023BD RID: 9149 RVA: 0x000C8DAF File Offset: 0x000C6FAF
		public override string ToString(float value)
		{
			if (base.TypeCode == XmlTypeCode.Float)
			{
				return XmlConvert.ToString(value);
			}
			return XmlConvert.ToString((double)value);
		}

		// Token: 0x060023BE RID: 9150 RVA: 0x000C8DCC File Offset: 0x000C6FCC
		public override string ToString(object value, IXmlNamespaceResolver nsResolver)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			Type type = value.GetType();
			if (type == XmlBaseConverter.DoubleType)
			{
				return this.ToString((double)value);
			}
			if (type == XmlBaseConverter.SingleType)
			{
				return this.ToString((float)value);
			}
			if (type == XmlBaseConverter.StringType)
			{
				return (string)value;
			}
			if (type == XmlBaseConverter.XmlAtomicValueType)
			{
				return ((XmlAtomicValue)value).Value;
			}
			return (string)this.ChangeListType(value, XmlBaseConverter.StringType, nsResolver);
		}

		// Token: 0x060023BF RID: 9151 RVA: 0x000C8E64 File Offset: 0x000C7064
		public override object ChangeType(double value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (destinationType == XmlBaseConverter.ObjectType)
			{
				destinationType = base.DefaultClrType;
			}
			if (destinationType == XmlBaseConverter.DoubleType)
			{
				return value;
			}
			if (destinationType == XmlBaseConverter.SingleType)
			{
				return (float)value;
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
			return this.ChangeListType(value, destinationType, null);
		}

		// Token: 0x060023C0 RID: 9152 RVA: 0x000C8F20 File Offset: 0x000C7120
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
			if (destinationType == XmlBaseConverter.DoubleType)
			{
				return this.ToDouble(value);
			}
			if (destinationType == XmlBaseConverter.SingleType)
			{
				return this.ToSingle(value);
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
			return this.ChangeListType(value, destinationType, nsResolver);
		}

		// Token: 0x060023C1 RID: 9153 RVA: 0x000C8FE4 File Offset: 0x000C71E4
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
			if (destinationType == XmlBaseConverter.DoubleType)
			{
				return this.ToDouble(value);
			}
			if (destinationType == XmlBaseConverter.SingleType)
			{
				return this.ToSingle(value);
			}
			if (destinationType == XmlBaseConverter.StringType)
			{
				return this.ToString(value, nsResolver);
			}
			if (destinationType == XmlBaseConverter.XmlAtomicValueType)
			{
				if (type == XmlBaseConverter.DoubleType)
				{
					return new XmlAtomicValue(base.SchemaType, (double)value);
				}
				if (type == XmlBaseConverter.SingleType)
				{
					return new XmlAtomicValue(base.SchemaType, value);
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
				if (type == XmlBaseConverter.DoubleType)
				{
					return new XmlAtomicValue(base.SchemaType, (double)value);
				}
				if (type == XmlBaseConverter.SingleType)
				{
					return new XmlAtomicValue(base.SchemaType, value);
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
			return this.ChangeListType(value, destinationType, nsResolver);
		}
	}
}
