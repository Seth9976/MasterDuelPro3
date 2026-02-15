using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Data.SqlTypes;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Xml;
using System.Xml.Serialization;

namespace System.Data.Common
{
	// Token: 0x0200011C RID: 284
	internal sealed class SqlUdtStorage : DataStorage
	{
		// Token: 0x06000EE0 RID: 3808 RVA: 0x0004D6EB File Offset: 0x0004B8EB
		public SqlUdtStorage(DataColumn column, Type type)
			: this(column, type, SqlUdtStorage.GetStaticNullForUdtType(type))
		{
		}

		// Token: 0x06000EE1 RID: 3809 RVA: 0x0004D6FC File Offset: 0x0004B8FC
		private SqlUdtStorage(DataColumn column, Type type, object nullValue)
			: base(column, type, nullValue, nullValue, typeof(ICloneable).IsAssignableFrom(type), DataStorage.GetStorageType(type))
		{
			this._implementsIXmlSerializable = typeof(IXmlSerializable).IsAssignableFrom(type);
			this._implementsIComparable = typeof(IComparable).IsAssignableFrom(type);
		}

		// Token: 0x06000EE2 RID: 3810 RVA: 0x0004D758 File Offset: 0x0004B958
		internal static object GetStaticNullForUdtType(Type type)
		{
			return SqlUdtStorage.s_typeToNull.GetOrAdd(type, delegate(Type t)
			{
				PropertyInfo property = type.GetProperty("Null", BindingFlags.Static | BindingFlags.Public);
				if (property != null)
				{
					return property.GetValue(null, null);
				}
				FieldInfo field = type.GetField("Null", BindingFlags.Static | BindingFlags.Public);
				if (field != null)
				{
					return field.GetValue(null);
				}
				throw ExceptionBuilder.INullableUDTwithoutStaticNull(type.AssemblyQualifiedName);
			});
		}

		// Token: 0x06000EE3 RID: 3811 RVA: 0x0004D78E File Offset: 0x0004B98E
		public override bool IsNull(int record)
		{
			return ((INullable)this._values[record]).IsNull;
		}

		// Token: 0x06000EE4 RID: 3812 RVA: 0x0003EAEE File Offset: 0x0003CCEE
		public override object Aggregate(int[] records, AggregateType kind)
		{
			throw ExceptionBuilder.AggregateException(kind, this._dataType);
		}

		// Token: 0x06000EE5 RID: 3813 RVA: 0x0004D7A2 File Offset: 0x0004B9A2
		public override int Compare(int recordNo1, int recordNo2)
		{
			return this.CompareValueTo(recordNo1, this._values[recordNo2]);
		}

		// Token: 0x06000EE6 RID: 3814 RVA: 0x0004D7B4 File Offset: 0x0004B9B4
		public override int CompareValueTo(int recordNo1, object value)
		{
			if (DBNull.Value == value)
			{
				value = this._nullValue;
			}
			if (this._implementsIComparable)
			{
				return ((IComparable)this._values[recordNo1]).CompareTo(value);
			}
			if (this._nullValue != value)
			{
				throw ExceptionBuilder.IComparableNotImplemented(this._dataType.AssemblyQualifiedName);
			}
			if (!((INullable)this._values[recordNo1]).IsNull)
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x06000EE7 RID: 3815 RVA: 0x0004D81E File Offset: 0x0004BA1E
		public override void Copy(int recordNo1, int recordNo2)
		{
			base.CopyBits(recordNo1, recordNo2);
			this._values[recordNo2] = this._values[recordNo1];
		}

		// Token: 0x06000EE8 RID: 3816 RVA: 0x0004D838 File Offset: 0x0004BA38
		public override object Get(int recordNo)
		{
			return this._values[recordNo];
		}

		// Token: 0x06000EE9 RID: 3817 RVA: 0x0004D844 File Offset: 0x0004BA44
		public override void Set(int recordNo, object value)
		{
			if (DBNull.Value == value)
			{
				this._values[recordNo] = this._nullValue;
				base.SetNullBit(recordNo, true);
				return;
			}
			if (value == null)
			{
				if (this._isValueType)
				{
					throw ExceptionBuilder.StorageSetFailed();
				}
				this._values[recordNo] = this._nullValue;
				base.SetNullBit(recordNo, true);
				return;
			}
			else
			{
				if (!this._dataType.IsInstanceOfType(value))
				{
					throw ExceptionBuilder.StorageSetFailed();
				}
				this._values[recordNo] = value;
				base.SetNullBit(recordNo, false);
				return;
			}
		}

		// Token: 0x06000EEA RID: 3818 RVA: 0x0004D8C0 File Offset: 0x0004BAC0
		public override void SetCapacity(int capacity)
		{
			object[] array = new object[capacity];
			if (this._values != null)
			{
				Array.Copy(this._values, 0, array, 0, Math.Min(capacity, this._values.Length));
			}
			this._values = array;
			base.SetCapacity(capacity);
		}

		// Token: 0x06000EEB RID: 3819 RVA: 0x0004D908 File Offset: 0x0004BB08
		[MethodImpl(MethodImplOptions.NoInlining)]
		public override object ConvertXmlToObject(string s)
		{
			if (this._implementsIXmlSerializable)
			{
				object obj = Activator.CreateInstance(this._dataType, true);
				using (XmlTextReader xmlTextReader = new XmlTextReader(new StringReader("<col>" + s + "</col>")))
				{
					((IXmlSerializable)obj).ReadXml(xmlTextReader);
				}
				return obj;
			}
			StringReader stringReader = new StringReader(s);
			return ObjectStorage.GetXmlSerializer(this._dataType).Deserialize(stringReader);
		}

		// Token: 0x06000EEC RID: 3820 RVA: 0x0004D988 File Offset: 0x0004BB88
		[MethodImpl(MethodImplOptions.NoInlining)]
		public override object ConvertXmlToObject(XmlReader xmlReader, XmlRootAttribute xmlAttrib)
		{
			if (xmlAttrib == null)
			{
				string text = xmlReader.GetAttribute("InstanceType", "urn:schemas-microsoft-com:xml-msdata");
				if (text == null)
				{
					string attribute = xmlReader.GetAttribute("InstanceType", "http://www.w3.org/2001/XMLSchema-instance");
					if (attribute != null)
					{
						text = XSDSchema.XsdtoClr(attribute).FullName;
					}
				}
				object obj = Activator.CreateInstance((text == null) ? this._dataType : Type.GetType(text), true);
				((IXmlSerializable)obj).ReadXml(xmlReader);
				return obj;
			}
			return ObjectStorage.GetXmlSerializer(this._dataType, xmlAttrib).Deserialize(xmlReader);
		}

		// Token: 0x06000EED RID: 3821 RVA: 0x0004DA04 File Offset: 0x0004BC04
		public override string ConvertObjectToXml(object value)
		{
			StringWriter stringWriter = new StringWriter(base.FormatProvider);
			if (this._implementsIXmlSerializable)
			{
				using (XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter))
				{
					((IXmlSerializable)value).WriteXml(xmlTextWriter);
					goto IL_0045;
				}
			}
			ObjectStorage.GetXmlSerializer(value.GetType()).Serialize(stringWriter, value);
			IL_0045:
			return stringWriter.ToString();
		}

		// Token: 0x06000EEE RID: 3822 RVA: 0x0004DA6C File Offset: 0x0004BC6C
		public override void ConvertObjectToXml(object value, XmlWriter xmlWriter, XmlRootAttribute xmlAttrib)
		{
			if (xmlAttrib == null)
			{
				((IXmlSerializable)value).WriteXml(xmlWriter);
				return;
			}
			ObjectStorage.GetXmlSerializer(this._dataType, xmlAttrib).Serialize(xmlWriter, value);
		}

		// Token: 0x06000EEF RID: 3823 RVA: 0x0003F4F1 File Offset: 0x0003D6F1
		protected override object GetEmptyStorage(int recordCount)
		{
			return new object[recordCount];
		}

		// Token: 0x06000EF0 RID: 3824 RVA: 0x0004DA91 File Offset: 0x0004BC91
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			((object[])store)[storeIndex] = this._values[record];
			nullbits.Set(storeIndex, this.IsNull(record));
		}

		// Token: 0x06000EF1 RID: 3825 RVA: 0x0004DAB3 File Offset: 0x0004BCB3
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this._values = (object[])store;
		}

		// Token: 0x040005B1 RID: 1457
		private object[] _values;

		// Token: 0x040005B2 RID: 1458
		private readonly bool _implementsIXmlSerializable;

		// Token: 0x040005B3 RID: 1459
		private readonly bool _implementsIComparable;

		// Token: 0x040005B4 RID: 1460
		private static readonly ConcurrentDictionary<Type, object> s_typeToNull = new ConcurrentDictionary<Type, object>();
	}
}
