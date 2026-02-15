using System;
using System.Collections;
using System.Xml.XPath;

namespace System.Xml.Schema
{
	// Token: 0x02000312 RID: 786
	internal abstract class XmlBaseConverter : XmlValueConverter
	{
		// Token: 0x06002322 RID: 8994 RVA: 0x000C6F38 File Offset: 0x000C5138
		protected XmlBaseConverter(XmlSchemaType schemaType)
		{
			XmlSchemaDatatype datatype = schemaType.Datatype;
			while (schemaType != null && !(schemaType is XmlSchemaSimpleType))
			{
				schemaType = schemaType.BaseXmlSchemaType;
			}
			if (schemaType == null)
			{
				schemaType = XmlSchemaType.GetBuiltInSimpleType(datatype.TypeCode);
			}
			this.schemaType = schemaType;
			this.typeCode = schemaType.TypeCode;
			this.clrTypeDefault = schemaType.Datatype.ValueType;
		}

		// Token: 0x06002323 RID: 8995 RVA: 0x000C6F9C File Offset: 0x000C519C
		protected XmlBaseConverter(XmlTypeCode typeCode)
		{
			if (typeCode != XmlTypeCode.Item)
			{
				if (typeCode != XmlTypeCode.Node)
				{
					if (typeCode == XmlTypeCode.AnyAtomicType)
					{
						this.clrTypeDefault = XmlBaseConverter.XmlAtomicValueType;
					}
				}
				else
				{
					this.clrTypeDefault = XmlBaseConverter.XPathNavigatorType;
				}
			}
			else
			{
				this.clrTypeDefault = XmlBaseConverter.XPathItemType;
			}
			this.typeCode = typeCode;
		}

		// Token: 0x06002324 RID: 8996 RVA: 0x000C6FEA File Offset: 0x000C51EA
		protected XmlBaseConverter(XmlBaseConverter converterAtomic)
		{
			this.schemaType = converterAtomic.schemaType;
			this.typeCode = converterAtomic.typeCode;
			this.clrTypeDefault = Array.CreateInstance(converterAtomic.DefaultClrType, 0).GetType();
		}

		// Token: 0x06002325 RID: 8997 RVA: 0x000C7021 File Offset: 0x000C5221
		protected XmlBaseConverter(XmlBaseConverter converterAtomic, Type clrTypeDefault)
		{
			this.schemaType = converterAtomic.schemaType;
			this.typeCode = converterAtomic.typeCode;
			this.clrTypeDefault = clrTypeDefault;
		}

		// Token: 0x06002326 RID: 8998 RVA: 0x000C7048 File Offset: 0x000C5248
		public override bool ToBoolean(DateTime value)
		{
			return (bool)this.ChangeType(value, XmlBaseConverter.BooleanType, null);
		}

		// Token: 0x06002327 RID: 8999 RVA: 0x000C7061 File Offset: 0x000C5261
		public override bool ToBoolean(double value)
		{
			return (bool)this.ChangeType(value, XmlBaseConverter.BooleanType, null);
		}

		// Token: 0x06002328 RID: 9000 RVA: 0x000C707A File Offset: 0x000C527A
		public override bool ToBoolean(int value)
		{
			return (bool)this.ChangeType(value, XmlBaseConverter.BooleanType, null);
		}

		// Token: 0x06002329 RID: 9001 RVA: 0x000C7093 File Offset: 0x000C5293
		public override bool ToBoolean(long value)
		{
			return (bool)this.ChangeType(value, XmlBaseConverter.BooleanType, null);
		}

		// Token: 0x0600232A RID: 9002 RVA: 0x000C70AC File Offset: 0x000C52AC
		public override bool ToBoolean(string value)
		{
			return (bool)this.ChangeType(value, XmlBaseConverter.BooleanType, null);
		}

		// Token: 0x0600232B RID: 9003 RVA: 0x000C70AC File Offset: 0x000C52AC
		public override bool ToBoolean(object value)
		{
			return (bool)this.ChangeType(value, XmlBaseConverter.BooleanType, null);
		}

		// Token: 0x0600232C RID: 9004 RVA: 0x000C70C0 File Offset: 0x000C52C0
		public override DateTime ToDateTime(bool value)
		{
			return (DateTime)this.ChangeType(value, XmlBaseConverter.DateTimeType, null);
		}

		// Token: 0x0600232D RID: 9005 RVA: 0x000C70D9 File Offset: 0x000C52D9
		public override DateTime ToDateTime(DateTimeOffset value)
		{
			return (DateTime)this.ChangeType(value, XmlBaseConverter.DateTimeType, null);
		}

		// Token: 0x0600232E RID: 9006 RVA: 0x000C70F2 File Offset: 0x000C52F2
		public override DateTime ToDateTime(double value)
		{
			return (DateTime)this.ChangeType(value, XmlBaseConverter.DateTimeType, null);
		}

		// Token: 0x0600232F RID: 9007 RVA: 0x000C710B File Offset: 0x000C530B
		public override DateTime ToDateTime(int value)
		{
			return (DateTime)this.ChangeType(value, XmlBaseConverter.DateTimeType, null);
		}

		// Token: 0x06002330 RID: 9008 RVA: 0x000C7124 File Offset: 0x000C5324
		public override DateTime ToDateTime(long value)
		{
			return (DateTime)this.ChangeType(value, XmlBaseConverter.DateTimeType, null);
		}

		// Token: 0x06002331 RID: 9009 RVA: 0x000C713D File Offset: 0x000C533D
		public override DateTime ToDateTime(string value)
		{
			return (DateTime)this.ChangeType(value, XmlBaseConverter.DateTimeType, null);
		}

		// Token: 0x06002332 RID: 9010 RVA: 0x000C713D File Offset: 0x000C533D
		public override DateTime ToDateTime(object value)
		{
			return (DateTime)this.ChangeType(value, XmlBaseConverter.DateTimeType, null);
		}

		// Token: 0x06002333 RID: 9011 RVA: 0x000C7151 File Offset: 0x000C5351
		public override DateTimeOffset ToDateTimeOffset(DateTime value)
		{
			return (DateTimeOffset)this.ChangeType(value, XmlBaseConverter.DateTimeOffsetType, null);
		}

		// Token: 0x06002334 RID: 9012 RVA: 0x000C716A File Offset: 0x000C536A
		public override DateTimeOffset ToDateTimeOffset(string value)
		{
			return (DateTimeOffset)this.ChangeType(value, XmlBaseConverter.DateTimeOffsetType, null);
		}

		// Token: 0x06002335 RID: 9013 RVA: 0x000C716A File Offset: 0x000C536A
		public override DateTimeOffset ToDateTimeOffset(object value)
		{
			return (DateTimeOffset)this.ChangeType(value, XmlBaseConverter.DateTimeOffsetType, null);
		}

		// Token: 0x06002336 RID: 9014 RVA: 0x000C717E File Offset: 0x000C537E
		public override decimal ToDecimal(string value)
		{
			return (decimal)this.ChangeType(value, XmlBaseConverter.DecimalType, null);
		}

		// Token: 0x06002337 RID: 9015 RVA: 0x000C717E File Offset: 0x000C537E
		public override decimal ToDecimal(object value)
		{
			return (decimal)this.ChangeType(value, XmlBaseConverter.DecimalType, null);
		}

		// Token: 0x06002338 RID: 9016 RVA: 0x000C7192 File Offset: 0x000C5392
		public override double ToDouble(bool value)
		{
			return (double)this.ChangeType(value, XmlBaseConverter.DoubleType, null);
		}

		// Token: 0x06002339 RID: 9017 RVA: 0x000C71AB File Offset: 0x000C53AB
		public override double ToDouble(DateTime value)
		{
			return (double)this.ChangeType(value, XmlBaseConverter.DoubleType, null);
		}

		// Token: 0x0600233A RID: 9018 RVA: 0x000C71C4 File Offset: 0x000C53C4
		public override double ToDouble(int value)
		{
			return (double)this.ChangeType(value, XmlBaseConverter.DoubleType, null);
		}

		// Token: 0x0600233B RID: 9019 RVA: 0x000C71DD File Offset: 0x000C53DD
		public override double ToDouble(long value)
		{
			return (double)this.ChangeType(value, XmlBaseConverter.DoubleType, null);
		}

		// Token: 0x0600233C RID: 9020 RVA: 0x000C71F6 File Offset: 0x000C53F6
		public override double ToDouble(string value)
		{
			return (double)this.ChangeType(value, XmlBaseConverter.DoubleType, null);
		}

		// Token: 0x0600233D RID: 9021 RVA: 0x000C71F6 File Offset: 0x000C53F6
		public override double ToDouble(object value)
		{
			return (double)this.ChangeType(value, XmlBaseConverter.DoubleType, null);
		}

		// Token: 0x0600233E RID: 9022 RVA: 0x000C720A File Offset: 0x000C540A
		public override int ToInt32(bool value)
		{
			return (int)this.ChangeType(value, XmlBaseConverter.Int32Type, null);
		}

		// Token: 0x0600233F RID: 9023 RVA: 0x000C7223 File Offset: 0x000C5423
		public override int ToInt32(DateTime value)
		{
			return (int)this.ChangeType(value, XmlBaseConverter.Int32Type, null);
		}

		// Token: 0x06002340 RID: 9024 RVA: 0x000C723C File Offset: 0x000C543C
		public override int ToInt32(double value)
		{
			return (int)this.ChangeType(value, XmlBaseConverter.Int32Type, null);
		}

		// Token: 0x06002341 RID: 9025 RVA: 0x000C7255 File Offset: 0x000C5455
		public override int ToInt32(long value)
		{
			return (int)this.ChangeType(value, XmlBaseConverter.Int32Type, null);
		}

		// Token: 0x06002342 RID: 9026 RVA: 0x000C726E File Offset: 0x000C546E
		public override int ToInt32(string value)
		{
			return (int)this.ChangeType(value, XmlBaseConverter.Int32Type, null);
		}

		// Token: 0x06002343 RID: 9027 RVA: 0x000C726E File Offset: 0x000C546E
		public override int ToInt32(object value)
		{
			return (int)this.ChangeType(value, XmlBaseConverter.Int32Type, null);
		}

		// Token: 0x06002344 RID: 9028 RVA: 0x000C7282 File Offset: 0x000C5482
		public override long ToInt64(bool value)
		{
			return (long)this.ChangeType(value, XmlBaseConverter.Int64Type, null);
		}

		// Token: 0x06002345 RID: 9029 RVA: 0x000C729B File Offset: 0x000C549B
		public override long ToInt64(DateTime value)
		{
			return (long)this.ChangeType(value, XmlBaseConverter.Int64Type, null);
		}

		// Token: 0x06002346 RID: 9030 RVA: 0x000C72B4 File Offset: 0x000C54B4
		public override long ToInt64(double value)
		{
			return (long)this.ChangeType(value, XmlBaseConverter.Int64Type, null);
		}

		// Token: 0x06002347 RID: 9031 RVA: 0x000C72CD File Offset: 0x000C54CD
		public override long ToInt64(int value)
		{
			return (long)this.ChangeType(value, XmlBaseConverter.Int64Type, null);
		}

		// Token: 0x06002348 RID: 9032 RVA: 0x000C72E6 File Offset: 0x000C54E6
		public override long ToInt64(string value)
		{
			return (long)this.ChangeType(value, XmlBaseConverter.Int64Type, null);
		}

		// Token: 0x06002349 RID: 9033 RVA: 0x000C72E6 File Offset: 0x000C54E6
		public override long ToInt64(object value)
		{
			return (long)this.ChangeType(value, XmlBaseConverter.Int64Type, null);
		}

		// Token: 0x0600234A RID: 9034 RVA: 0x000C72FA File Offset: 0x000C54FA
		public override float ToSingle(double value)
		{
			return (float)this.ChangeType(value, XmlBaseConverter.SingleType, null);
		}

		// Token: 0x0600234B RID: 9035 RVA: 0x000C7313 File Offset: 0x000C5513
		public override float ToSingle(string value)
		{
			return (float)this.ChangeType(value, XmlBaseConverter.SingleType, null);
		}

		// Token: 0x0600234C RID: 9036 RVA: 0x000C7313 File Offset: 0x000C5513
		public override float ToSingle(object value)
		{
			return (float)this.ChangeType(value, XmlBaseConverter.SingleType, null);
		}

		// Token: 0x0600234D RID: 9037 RVA: 0x000C7327 File Offset: 0x000C5527
		public override string ToString(bool value)
		{
			return (string)this.ChangeType(value, XmlBaseConverter.StringType, null);
		}

		// Token: 0x0600234E RID: 9038 RVA: 0x000C7340 File Offset: 0x000C5540
		public override string ToString(DateTime value)
		{
			return (string)this.ChangeType(value, XmlBaseConverter.StringType, null);
		}

		// Token: 0x0600234F RID: 9039 RVA: 0x000C7359 File Offset: 0x000C5559
		public override string ToString(DateTimeOffset value)
		{
			return (string)this.ChangeType(value, XmlBaseConverter.StringType, null);
		}

		// Token: 0x06002350 RID: 9040 RVA: 0x000C7372 File Offset: 0x000C5572
		public override string ToString(decimal value)
		{
			return (string)this.ChangeType(value, XmlBaseConverter.StringType, null);
		}

		// Token: 0x06002351 RID: 9041 RVA: 0x000C738B File Offset: 0x000C558B
		public override string ToString(double value)
		{
			return (string)this.ChangeType(value, XmlBaseConverter.StringType, null);
		}

		// Token: 0x06002352 RID: 9042 RVA: 0x000C73A4 File Offset: 0x000C55A4
		public override string ToString(int value)
		{
			return (string)this.ChangeType(value, XmlBaseConverter.StringType, null);
		}

		// Token: 0x06002353 RID: 9043 RVA: 0x000C73BD File Offset: 0x000C55BD
		public override string ToString(long value)
		{
			return (string)this.ChangeType(value, XmlBaseConverter.StringType, null);
		}

		// Token: 0x06002354 RID: 9044 RVA: 0x000C73D6 File Offset: 0x000C55D6
		public override string ToString(float value)
		{
			return (string)this.ChangeType(value, XmlBaseConverter.StringType, null);
		}

		// Token: 0x06002355 RID: 9045 RVA: 0x000C73EF File Offset: 0x000C55EF
		public override string ToString(object value, IXmlNamespaceResolver nsResolver)
		{
			return (string)this.ChangeType(value, XmlBaseConverter.StringType, nsResolver);
		}

		// Token: 0x06002356 RID: 9046 RVA: 0x000C7403 File Offset: 0x000C5603
		public override string ToString(object value)
		{
			return this.ToString(value, null);
		}

		// Token: 0x06002357 RID: 9047 RVA: 0x000C740D File Offset: 0x000C560D
		public override object ChangeType(bool value, Type destinationType)
		{
			return this.ChangeType(value, destinationType, null);
		}

		// Token: 0x06002358 RID: 9048 RVA: 0x000C741D File Offset: 0x000C561D
		public override object ChangeType(DateTime value, Type destinationType)
		{
			return this.ChangeType(value, destinationType, null);
		}

		// Token: 0x06002359 RID: 9049 RVA: 0x000C742D File Offset: 0x000C562D
		public override object ChangeType(decimal value, Type destinationType)
		{
			return this.ChangeType(value, destinationType, null);
		}

		// Token: 0x0600235A RID: 9050 RVA: 0x000C743D File Offset: 0x000C563D
		public override object ChangeType(double value, Type destinationType)
		{
			return this.ChangeType(value, destinationType, null);
		}

		// Token: 0x0600235B RID: 9051 RVA: 0x000C744D File Offset: 0x000C564D
		public override object ChangeType(int value, Type destinationType)
		{
			return this.ChangeType(value, destinationType, null);
		}

		// Token: 0x0600235C RID: 9052 RVA: 0x000C745D File Offset: 0x000C565D
		public override object ChangeType(long value, Type destinationType)
		{
			return this.ChangeType(value, destinationType, null);
		}

		// Token: 0x0600235D RID: 9053 RVA: 0x000C746D File Offset: 0x000C566D
		public override object ChangeType(string value, Type destinationType, IXmlNamespaceResolver nsResolver)
		{
			return this.ChangeType(value, destinationType, nsResolver);
		}

		// Token: 0x0600235E RID: 9054 RVA: 0x000C7478 File Offset: 0x000C5678
		public override object ChangeType(object value, Type destinationType)
		{
			return this.ChangeType(value, destinationType, null);
		}

		// Token: 0x17000896 RID: 2198
		// (get) Token: 0x0600235F RID: 9055 RVA: 0x000C7483 File Offset: 0x000C5683
		protected XmlSchemaType SchemaType
		{
			get
			{
				return this.schemaType;
			}
		}

		// Token: 0x17000897 RID: 2199
		// (get) Token: 0x06002360 RID: 9056 RVA: 0x000C748B File Offset: 0x000C568B
		protected XmlTypeCode TypeCode
		{
			get
			{
				return this.typeCode;
			}
		}

		// Token: 0x17000898 RID: 2200
		// (get) Token: 0x06002361 RID: 9057 RVA: 0x000C7494 File Offset: 0x000C5694
		protected string XmlTypeName
		{
			get
			{
				XmlSchemaType baseXmlSchemaType = this.schemaType;
				if (baseXmlSchemaType != null)
				{
					while (baseXmlSchemaType.QualifiedName.IsEmpty)
					{
						baseXmlSchemaType = baseXmlSchemaType.BaseXmlSchemaType;
					}
					return XmlBaseConverter.QNameToString(baseXmlSchemaType.QualifiedName);
				}
				if (this.typeCode == XmlTypeCode.Node)
				{
					return "node";
				}
				if (this.typeCode == XmlTypeCode.AnyAtomicType)
				{
					return "xdt:anyAtomicType";
				}
				return "item";
			}
		}

		// Token: 0x17000899 RID: 2201
		// (get) Token: 0x06002362 RID: 9058 RVA: 0x000C74F1 File Offset: 0x000C56F1
		protected Type DefaultClrType
		{
			get
			{
				return this.clrTypeDefault;
			}
		}

		// Token: 0x06002363 RID: 9059 RVA: 0x000C74F9 File Offset: 0x000C56F9
		protected static bool IsDerivedFrom(Type derivedType, Type baseType)
		{
			while (derivedType != null)
			{
				if (derivedType == baseType)
				{
					return true;
				}
				derivedType = derivedType.BaseType;
			}
			return false;
		}

		// Token: 0x06002364 RID: 9060 RVA: 0x000C751C File Offset: 0x000C571C
		protected Exception CreateInvalidClrMappingException(Type sourceType, Type destinationType)
		{
			if (sourceType == destinationType)
			{
				return new InvalidCastException(Res.GetString("Xml type '{0}' does not support Clr type '{1}'.", new object[] { this.XmlTypeName, sourceType.Name }));
			}
			return new InvalidCastException(Res.GetString("Xml type '{0}' does not support a conversion from Clr type '{1}' to Clr type '{2}'.", new object[] { this.XmlTypeName, sourceType.Name, destinationType.Name }));
		}

		// Token: 0x06002365 RID: 9061 RVA: 0x000C758C File Offset: 0x000C578C
		protected static string QNameToString(XmlQualifiedName name)
		{
			if (name.Namespace.Length == 0)
			{
				return name.Name;
			}
			if (name.Namespace == "http://www.w3.org/2001/XMLSchema")
			{
				return "xs:" + name.Name;
			}
			if (name.Namespace == "http://www.w3.org/2003/11/xpath-datatypes")
			{
				return "xdt:" + name.Name;
			}
			return "{" + name.Namespace + "}" + name.Name;
		}

		// Token: 0x06002366 RID: 9062 RVA: 0x000C760E File Offset: 0x000C580E
		protected virtual object ChangeListType(object value, Type destinationType, IXmlNamespaceResolver nsResolver)
		{
			throw this.CreateInvalidClrMappingException(value.GetType(), destinationType);
		}

		// Token: 0x06002367 RID: 9063 RVA: 0x000C761D File Offset: 0x000C581D
		protected static byte[] StringToBase64Binary(string value)
		{
			return Convert.FromBase64String(XmlConvert.TrimString(value));
		}

		// Token: 0x06002368 RID: 9064 RVA: 0x000C762A File Offset: 0x000C582A
		protected static DateTime StringToDate(string value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.Date);
		}

		// Token: 0x06002369 RID: 9065 RVA: 0x000C7638 File Offset: 0x000C5838
		protected static DateTime StringToDateTime(string value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.DateTime);
		}

		// Token: 0x0600236A RID: 9066 RVA: 0x000C7648 File Offset: 0x000C5848
		protected static TimeSpan StringToDayTimeDuration(string value)
		{
			return new XsdDuration(value, XsdDuration.DurationType.DayTimeDuration).ToTimeSpan(XsdDuration.DurationType.DayTimeDuration);
		}

		// Token: 0x0600236B RID: 9067 RVA: 0x000C7668 File Offset: 0x000C5868
		protected static TimeSpan StringToDuration(string value)
		{
			return new XsdDuration(value, XsdDuration.DurationType.Duration).ToTimeSpan(XsdDuration.DurationType.Duration);
		}

		// Token: 0x0600236C RID: 9068 RVA: 0x000C7685 File Offset: 0x000C5885
		protected static DateTime StringToGDay(string value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.GDay);
		}

		// Token: 0x0600236D RID: 9069 RVA: 0x000C7694 File Offset: 0x000C5894
		protected static DateTime StringToGMonth(string value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.GMonth);
		}

		// Token: 0x0600236E RID: 9070 RVA: 0x000C76A6 File Offset: 0x000C58A6
		protected static DateTime StringToGMonthDay(string value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.GMonthDay);
		}

		// Token: 0x0600236F RID: 9071 RVA: 0x000C76B5 File Offset: 0x000C58B5
		protected static DateTime StringToGYear(string value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.GYear);
		}

		// Token: 0x06002370 RID: 9072 RVA: 0x000C76C4 File Offset: 0x000C58C4
		protected static DateTime StringToGYearMonth(string value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.GYearMonth);
		}

		// Token: 0x06002371 RID: 9073 RVA: 0x000C76D2 File Offset: 0x000C58D2
		protected static DateTimeOffset StringToDateOffset(string value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.Date);
		}

		// Token: 0x06002372 RID: 9074 RVA: 0x000C76E0 File Offset: 0x000C58E0
		protected static DateTimeOffset StringToDateTimeOffset(string value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.DateTime);
		}

		// Token: 0x06002373 RID: 9075 RVA: 0x000C76EE File Offset: 0x000C58EE
		protected static DateTimeOffset StringToGDayOffset(string value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.GDay);
		}

		// Token: 0x06002374 RID: 9076 RVA: 0x000C76FD File Offset: 0x000C58FD
		protected static DateTimeOffset StringToGMonthOffset(string value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.GMonth);
		}

		// Token: 0x06002375 RID: 9077 RVA: 0x000C770F File Offset: 0x000C590F
		protected static DateTimeOffset StringToGMonthDayOffset(string value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.GMonthDay);
		}

		// Token: 0x06002376 RID: 9078 RVA: 0x000C771E File Offset: 0x000C591E
		protected static DateTimeOffset StringToGYearOffset(string value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.GYear);
		}

		// Token: 0x06002377 RID: 9079 RVA: 0x000C772D File Offset: 0x000C592D
		protected static DateTimeOffset StringToGYearMonthOffset(string value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.GYearMonth);
		}

		// Token: 0x06002378 RID: 9080 RVA: 0x000C773C File Offset: 0x000C593C
		protected static byte[] StringToHexBinary(string value)
		{
			byte[] array;
			try
			{
				array = XmlConvert.FromBinHexString(XmlConvert.TrimString(value), false);
			}
			catch (XmlException ex)
			{
				throw new FormatException(ex.Message);
			}
			return array;
		}

		// Token: 0x06002379 RID: 9081 RVA: 0x000C7774 File Offset: 0x000C5974
		protected static XmlQualifiedName StringToQName(string value, IXmlNamespaceResolver nsResolver)
		{
			value = value.Trim();
			string text;
			string text2;
			try
			{
				ValidateNames.ParseQNameThrow(value, out text, out text2);
			}
			catch (XmlException ex)
			{
				throw new FormatException(ex.Message);
			}
			if (nsResolver == null)
			{
				throw new InvalidCastException(Res.GetString("The String '{0}' cannot be represented as an XmlQualifiedName.  A namespace for prefix '{1}' cannot be found.", new object[] { value, text }));
			}
			string text3 = nsResolver.LookupNamespace(text);
			if (text3 == null)
			{
				throw new InvalidCastException(Res.GetString("The String '{0}' cannot be represented as an XmlQualifiedName.  A namespace for prefix '{1}' cannot be found.", new object[] { value, text }));
			}
			return new XmlQualifiedName(text2, text3);
		}

		// Token: 0x0600237A RID: 9082 RVA: 0x000C7804 File Offset: 0x000C5A04
		protected static DateTime StringToTime(string value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.Time);
		}

		// Token: 0x0600237B RID: 9083 RVA: 0x000C7812 File Offset: 0x000C5A12
		protected static DateTimeOffset StringToTimeOffset(string value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.Time);
		}

		// Token: 0x0600237C RID: 9084 RVA: 0x000C7820 File Offset: 0x000C5A20
		protected static TimeSpan StringToYearMonthDuration(string value)
		{
			return new XsdDuration(value, XsdDuration.DurationType.YearMonthDuration).ToTimeSpan(XsdDuration.DurationType.YearMonthDuration);
		}

		// Token: 0x0600237D RID: 9085 RVA: 0x000C783D File Offset: 0x000C5A3D
		protected static string AnyUriToString(Uri value)
		{
			return value.OriginalString;
		}

		// Token: 0x0600237E RID: 9086 RVA: 0x000C7845 File Offset: 0x000C5A45
		protected static string Base64BinaryToString(byte[] value)
		{
			return Convert.ToBase64String(value);
		}

		// Token: 0x0600237F RID: 9087 RVA: 0x000C7850 File Offset: 0x000C5A50
		protected static string DateToString(DateTime value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.Date).ToString();
		}

		// Token: 0x06002380 RID: 9088 RVA: 0x000C7874 File Offset: 0x000C5A74
		protected static string DateTimeToString(DateTime value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.DateTime).ToString();
		}

		// Token: 0x06002381 RID: 9089 RVA: 0x000C7898 File Offset: 0x000C5A98
		protected static string DayTimeDurationToString(TimeSpan value)
		{
			return new XsdDuration(value, XsdDuration.DurationType.DayTimeDuration).ToString(XsdDuration.DurationType.DayTimeDuration);
		}

		// Token: 0x06002382 RID: 9090 RVA: 0x000C78B8 File Offset: 0x000C5AB8
		protected static string DurationToString(TimeSpan value)
		{
			return new XsdDuration(value, XsdDuration.DurationType.Duration).ToString(XsdDuration.DurationType.Duration);
		}

		// Token: 0x06002383 RID: 9091 RVA: 0x000C78D8 File Offset: 0x000C5AD8
		protected static string GDayToString(DateTime value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.GDay).ToString();
		}

		// Token: 0x06002384 RID: 9092 RVA: 0x000C78FC File Offset: 0x000C5AFC
		protected static string GMonthToString(DateTime value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.GMonth).ToString();
		}

		// Token: 0x06002385 RID: 9093 RVA: 0x000C7924 File Offset: 0x000C5B24
		protected static string GMonthDayToString(DateTime value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.GMonthDay).ToString();
		}

		// Token: 0x06002386 RID: 9094 RVA: 0x000C7948 File Offset: 0x000C5B48
		protected static string GYearToString(DateTime value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.GYear).ToString();
		}

		// Token: 0x06002387 RID: 9095 RVA: 0x000C796C File Offset: 0x000C5B6C
		protected static string GYearMonthToString(DateTime value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.GYearMonth).ToString();
		}

		// Token: 0x06002388 RID: 9096 RVA: 0x000C7990 File Offset: 0x000C5B90
		protected static string DateOffsetToString(DateTimeOffset value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.Date).ToString();
		}

		// Token: 0x06002389 RID: 9097 RVA: 0x000C79B4 File Offset: 0x000C5BB4
		protected static string DateTimeOffsetToString(DateTimeOffset value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.DateTime).ToString();
		}

		// Token: 0x0600238A RID: 9098 RVA: 0x000C79D8 File Offset: 0x000C5BD8
		protected static string GDayOffsetToString(DateTimeOffset value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.GDay).ToString();
		}

		// Token: 0x0600238B RID: 9099 RVA: 0x000C79FC File Offset: 0x000C5BFC
		protected static string GMonthOffsetToString(DateTimeOffset value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.GMonth).ToString();
		}

		// Token: 0x0600238C RID: 9100 RVA: 0x000C7A24 File Offset: 0x000C5C24
		protected static string GMonthDayOffsetToString(DateTimeOffset value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.GMonthDay).ToString();
		}

		// Token: 0x0600238D RID: 9101 RVA: 0x000C7A48 File Offset: 0x000C5C48
		protected static string GYearOffsetToString(DateTimeOffset value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.GYear).ToString();
		}

		// Token: 0x0600238E RID: 9102 RVA: 0x000C7A6C File Offset: 0x000C5C6C
		protected static string GYearMonthOffsetToString(DateTimeOffset value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.GYearMonth).ToString();
		}

		// Token: 0x0600238F RID: 9103 RVA: 0x000C7A90 File Offset: 0x000C5C90
		protected static string QNameToString(XmlQualifiedName qname, IXmlNamespaceResolver nsResolver)
		{
			if (nsResolver == null)
			{
				return "{" + qname.Namespace + "}" + qname.Name;
			}
			string text = nsResolver.LookupPrefix(qname.Namespace);
			if (text == null)
			{
				throw new InvalidCastException(Res.GetString("The QName '{0}' cannot be represented as a String.  A prefix for namespace '{1}' cannot be found.", new object[]
				{
					qname.ToString(),
					qname.Namespace
				}));
			}
			if (text.Length == 0)
			{
				return qname.Name;
			}
			return text + ":" + qname.Name;
		}

		// Token: 0x06002390 RID: 9104 RVA: 0x000C7B14 File Offset: 0x000C5D14
		protected static string TimeToString(DateTime value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.Time).ToString();
		}

		// Token: 0x06002391 RID: 9105 RVA: 0x000C7B38 File Offset: 0x000C5D38
		protected static string TimeOffsetToString(DateTimeOffset value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.Time).ToString();
		}

		// Token: 0x06002392 RID: 9106 RVA: 0x000C7B5C File Offset: 0x000C5D5C
		protected static string YearMonthDurationToString(TimeSpan value)
		{
			return new XsdDuration(value, XsdDuration.DurationType.YearMonthDuration).ToString(XsdDuration.DurationType.YearMonthDuration);
		}

		// Token: 0x06002393 RID: 9107 RVA: 0x000C7B79 File Offset: 0x000C5D79
		internal static DateTime DateTimeOffsetToDateTime(DateTimeOffset value)
		{
			return value.LocalDateTime;
		}

		// Token: 0x06002394 RID: 9108 RVA: 0x000C7B84 File Offset: 0x000C5D84
		internal static int DecimalToInt32(decimal value)
		{
			if (value < -2147483648m || value > 2147483647m)
			{
				string text = "Value '{0}' was either too large or too small for {1}.";
				object[] array = new string[]
				{
					XmlConvert.ToString(value),
					"Int32"
				};
				throw new OverflowException(Res.GetString(text, array));
			}
			return (int)value;
		}

		// Token: 0x06002395 RID: 9109 RVA: 0x000C7BE4 File Offset: 0x000C5DE4
		protected static long DecimalToInt64(decimal value)
		{
			if (value < -9223372036854775808m || value > 9223372036854775807m)
			{
				string text = "Value '{0}' was either too large or too small for {1}.";
				object[] array = new string[]
				{
					XmlConvert.ToString(value),
					"Int64"
				};
				throw new OverflowException(Res.GetString(text, array));
			}
			return (long)value;
		}

		// Token: 0x06002396 RID: 9110 RVA: 0x000C7C4C File Offset: 0x000C5E4C
		protected static ulong DecimalToUInt64(decimal value)
		{
			if (value < 0m || value > 18446744073709551615m)
			{
				string text = "Value '{0}' was either too large or too small for {1}.";
				object[] array = new string[]
				{
					XmlConvert.ToString(value),
					"UInt64"
				};
				throw new OverflowException(Res.GetString(text, array));
			}
			return (ulong)value;
		}

		// Token: 0x06002397 RID: 9111 RVA: 0x000C7CA4 File Offset: 0x000C5EA4
		protected static byte Int32ToByte(int value)
		{
			if (value < 0 || value > 255)
			{
				string text = "Value '{0}' was either too large or too small for {1}.";
				object[] array = new string[]
				{
					XmlConvert.ToString(value),
					"Byte"
				};
				throw new OverflowException(Res.GetString(text, array));
			}
			return (byte)value;
		}

		// Token: 0x06002398 RID: 9112 RVA: 0x000C7CE8 File Offset: 0x000C5EE8
		protected static short Int32ToInt16(int value)
		{
			if (value < -32768 || value > 32767)
			{
				string text = "Value '{0}' was either too large or too small for {1}.";
				object[] array = new string[]
				{
					XmlConvert.ToString(value),
					"Int16"
				};
				throw new OverflowException(Res.GetString(text, array));
			}
			return (short)value;
		}

		// Token: 0x06002399 RID: 9113 RVA: 0x000C7D30 File Offset: 0x000C5F30
		protected static sbyte Int32ToSByte(int value)
		{
			if (value < -128 || value > 127)
			{
				string text = "Value '{0}' was either too large or too small for {1}.";
				object[] array = new string[]
				{
					XmlConvert.ToString(value),
					"SByte"
				};
				throw new OverflowException(Res.GetString(text, array));
			}
			return (sbyte)value;
		}

		// Token: 0x0600239A RID: 9114 RVA: 0x000C7D74 File Offset: 0x000C5F74
		protected static ushort Int32ToUInt16(int value)
		{
			if (value < 0 || value > 65535)
			{
				string text = "Value '{0}' was either too large or too small for {1}.";
				object[] array = new string[]
				{
					XmlConvert.ToString(value),
					"UInt16"
				};
				throw new OverflowException(Res.GetString(text, array));
			}
			return (ushort)value;
		}

		// Token: 0x0600239B RID: 9115 RVA: 0x000C7DB8 File Offset: 0x000C5FB8
		protected static int Int64ToInt32(long value)
		{
			if (value < -2147483648L || value > 2147483647L)
			{
				string text = "Value '{0}' was either too large or too small for {1}.";
				object[] array = new string[]
				{
					XmlConvert.ToString(value),
					"Int32"
				};
				throw new OverflowException(Res.GetString(text, array));
			}
			return (int)value;
		}

		// Token: 0x0600239C RID: 9116 RVA: 0x000C7E04 File Offset: 0x000C6004
		protected static uint Int64ToUInt32(long value)
		{
			if (value < 0L || value > (long)((ulong)(-1)))
			{
				string text = "Value '{0}' was either too large or too small for {1}.";
				object[] array = new string[]
				{
					XmlConvert.ToString(value),
					"UInt32"
				};
				throw new OverflowException(Res.GetString(text, array));
			}
			return (uint)value;
		}

		// Token: 0x0600239D RID: 9117 RVA: 0x000C7E46 File Offset: 0x000C6046
		protected static DateTime UntypedAtomicToDateTime(string value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.AllXsd);
		}

		// Token: 0x0600239E RID: 9118 RVA: 0x000C7E58 File Offset: 0x000C6058
		protected static DateTimeOffset UntypedAtomicToDateTimeOffset(string value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.AllXsd);
		}

		// Token: 0x0400108D RID: 4237
		private XmlSchemaType schemaType;

		// Token: 0x0400108E RID: 4238
		private XmlTypeCode typeCode;

		// Token: 0x0400108F RID: 4239
		private Type clrTypeDefault;

		// Token: 0x04001090 RID: 4240
		protected static readonly Type ICollectionType = typeof(ICollection);

		// Token: 0x04001091 RID: 4241
		protected static readonly Type IEnumerableType = typeof(IEnumerable);

		// Token: 0x04001092 RID: 4242
		protected static readonly Type IListType = typeof(IList);

		// Token: 0x04001093 RID: 4243
		protected static readonly Type ObjectArrayType = typeof(object[]);

		// Token: 0x04001094 RID: 4244
		protected static readonly Type StringArrayType = typeof(string[]);

		// Token: 0x04001095 RID: 4245
		protected static readonly Type XmlAtomicValueArrayType = typeof(XmlAtomicValue[]);

		// Token: 0x04001096 RID: 4246
		protected static readonly Type DecimalType = typeof(decimal);

		// Token: 0x04001097 RID: 4247
		protected static readonly Type Int32Type = typeof(int);

		// Token: 0x04001098 RID: 4248
		protected static readonly Type Int64Type = typeof(long);

		// Token: 0x04001099 RID: 4249
		protected static readonly Type StringType = typeof(string);

		// Token: 0x0400109A RID: 4250
		protected static readonly Type XmlAtomicValueType = typeof(XmlAtomicValue);

		// Token: 0x0400109B RID: 4251
		protected static readonly Type ObjectType = typeof(object);

		// Token: 0x0400109C RID: 4252
		protected static readonly Type ByteType = typeof(byte);

		// Token: 0x0400109D RID: 4253
		protected static readonly Type Int16Type = typeof(short);

		// Token: 0x0400109E RID: 4254
		protected static readonly Type SByteType = typeof(sbyte);

		// Token: 0x0400109F RID: 4255
		protected static readonly Type UInt16Type = typeof(ushort);

		// Token: 0x040010A0 RID: 4256
		protected static readonly Type UInt32Type = typeof(uint);

		// Token: 0x040010A1 RID: 4257
		protected static readonly Type UInt64Type = typeof(ulong);

		// Token: 0x040010A2 RID: 4258
		protected static readonly Type XPathItemType = typeof(XPathItem);

		// Token: 0x040010A3 RID: 4259
		protected static readonly Type DoubleType = typeof(double);

		// Token: 0x040010A4 RID: 4260
		protected static readonly Type SingleType = typeof(float);

		// Token: 0x040010A5 RID: 4261
		protected static readonly Type DateTimeType = typeof(DateTime);

		// Token: 0x040010A6 RID: 4262
		protected static readonly Type DateTimeOffsetType = typeof(DateTimeOffset);

		// Token: 0x040010A7 RID: 4263
		protected static readonly Type BooleanType = typeof(bool);

		// Token: 0x040010A8 RID: 4264
		protected static readonly Type ByteArrayType = typeof(byte[]);

		// Token: 0x040010A9 RID: 4265
		protected static readonly Type XmlQualifiedNameType = typeof(XmlQualifiedName);

		// Token: 0x040010AA RID: 4266
		protected static readonly Type UriType = typeof(Uri);

		// Token: 0x040010AB RID: 4267
		protected static readonly Type TimeSpanType = typeof(TimeSpan);

		// Token: 0x040010AC RID: 4268
		protected static readonly Type XPathNavigatorType = typeof(XPathNavigator);
	}
}
