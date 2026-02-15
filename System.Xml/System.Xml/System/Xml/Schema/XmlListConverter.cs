using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml.XPath;

namespace System.Xml.Schema
{
	// Token: 0x0200031C RID: 796
	internal class XmlListConverter : XmlBaseConverter
	{
		// Token: 0x06002425 RID: 9253 RVA: 0x000CBF3A File Offset: 0x000CA13A
		protected XmlListConverter(XmlBaseConverter atomicConverter)
			: base(atomicConverter)
		{
			this.atomicConverter = atomicConverter;
		}

		// Token: 0x06002426 RID: 9254 RVA: 0x000CBF4A File Offset: 0x000CA14A
		protected XmlListConverter(XmlBaseConverter atomicConverter, Type clrTypeDefault)
			: base(atomicConverter, clrTypeDefault)
		{
			this.atomicConverter = atomicConverter;
		}

		// Token: 0x06002427 RID: 9255 RVA: 0x000C802C File Offset: 0x000C622C
		protected XmlListConverter(XmlSchemaType schemaType)
			: base(schemaType)
		{
		}

		// Token: 0x06002428 RID: 9256 RVA: 0x000CBF5B File Offset: 0x000CA15B
		public static XmlValueConverter Create(XmlValueConverter atomicConverter)
		{
			if (atomicConverter == XmlUntypedConverter.Untyped)
			{
				return XmlUntypedConverter.UntypedList;
			}
			if (atomicConverter == XmlAnyConverter.Item)
			{
				return XmlAnyListConverter.ItemList;
			}
			if (atomicConverter == XmlAnyConverter.AnyAtomic)
			{
				return XmlAnyListConverter.AnyAtomicList;
			}
			return new XmlListConverter((XmlBaseConverter)atomicConverter);
		}

		// Token: 0x06002429 RID: 9257 RVA: 0x000CBF92 File Offset: 0x000CA192
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
			return this.ChangeListType(value, destinationType, nsResolver);
		}

		// Token: 0x0600242A RID: 9258 RVA: 0x000CBFC0 File Offset: 0x000CA1C0
		protected override object ChangeListType(object value, Type destinationType, IXmlNamespaceResolver nsResolver)
		{
			Type type = value.GetType();
			if (destinationType == XmlBaseConverter.ObjectType)
			{
				destinationType = base.DefaultClrType;
			}
			if (!(value is IEnumerable) || !this.IsListType(destinationType))
			{
				throw this.CreateInvalidClrMappingException(type, destinationType);
			}
			if (destinationType == XmlBaseConverter.StringType)
			{
				if (type == XmlBaseConverter.StringType)
				{
					return value;
				}
				return this.ListAsString((IEnumerable)value, nsResolver);
			}
			else
			{
				if (type == XmlBaseConverter.StringType)
				{
					value = this.StringAsList((string)value);
				}
				if (destinationType.IsArray)
				{
					Type elementType = destinationType.GetElementType();
					if (elementType == XmlBaseConverter.ObjectType)
					{
						return this.ToArray<object>(value, nsResolver);
					}
					if (type == destinationType)
					{
						return value;
					}
					if (elementType == XmlBaseConverter.BooleanType)
					{
						return this.ToArray<bool>(value, nsResolver);
					}
					if (elementType == XmlBaseConverter.ByteType)
					{
						return this.ToArray<byte>(value, nsResolver);
					}
					if (elementType == XmlBaseConverter.ByteArrayType)
					{
						return this.ToArray<byte[]>(value, nsResolver);
					}
					if (elementType == XmlBaseConverter.DateTimeType)
					{
						return this.ToArray<DateTime>(value, nsResolver);
					}
					if (elementType == XmlBaseConverter.DateTimeOffsetType)
					{
						return this.ToArray<DateTimeOffset>(value, nsResolver);
					}
					if (elementType == XmlBaseConverter.DecimalType)
					{
						return this.ToArray<decimal>(value, nsResolver);
					}
					if (elementType == XmlBaseConverter.DoubleType)
					{
						return this.ToArray<double>(value, nsResolver);
					}
					if (elementType == XmlBaseConverter.Int16Type)
					{
						return this.ToArray<short>(value, nsResolver);
					}
					if (elementType == XmlBaseConverter.Int32Type)
					{
						return this.ToArray<int>(value, nsResolver);
					}
					if (elementType == XmlBaseConverter.Int64Type)
					{
						return this.ToArray<long>(value, nsResolver);
					}
					if (elementType == XmlBaseConverter.SByteType)
					{
						return this.ToArray<sbyte>(value, nsResolver);
					}
					if (elementType == XmlBaseConverter.SingleType)
					{
						return this.ToArray<float>(value, nsResolver);
					}
					if (elementType == XmlBaseConverter.StringType)
					{
						return this.ToArray<string>(value, nsResolver);
					}
					if (elementType == XmlBaseConverter.TimeSpanType)
					{
						return this.ToArray<TimeSpan>(value, nsResolver);
					}
					if (elementType == XmlBaseConverter.UInt16Type)
					{
						return this.ToArray<ushort>(value, nsResolver);
					}
					if (elementType == XmlBaseConverter.UInt32Type)
					{
						return this.ToArray<uint>(value, nsResolver);
					}
					if (elementType == XmlBaseConverter.UInt64Type)
					{
						return this.ToArray<ulong>(value, nsResolver);
					}
					if (elementType == XmlBaseConverter.UriType)
					{
						return this.ToArray<Uri>(value, nsResolver);
					}
					if (elementType == XmlBaseConverter.XmlAtomicValueType)
					{
						return this.ToArray<XmlAtomicValue>(value, nsResolver);
					}
					if (elementType == XmlBaseConverter.XmlQualifiedNameType)
					{
						return this.ToArray<XmlQualifiedName>(value, nsResolver);
					}
					if (elementType == XmlBaseConverter.XPathItemType)
					{
						return this.ToArray<XPathItem>(value, nsResolver);
					}
					if (elementType == XmlBaseConverter.XPathNavigatorType)
					{
						return this.ToArray<XPathNavigator>(value, nsResolver);
					}
					throw this.CreateInvalidClrMappingException(type, destinationType);
				}
				else
				{
					if (type == base.DefaultClrType && type != XmlBaseConverter.ObjectArrayType)
					{
						return value;
					}
					return this.ToList(value, nsResolver);
				}
			}
		}

		// Token: 0x0600242B RID: 9259 RVA: 0x000CC28D File Offset: 0x000CA48D
		private bool IsListType(Type type)
		{
			return type == XmlBaseConverter.IListType || type == XmlBaseConverter.ICollectionType || type == XmlBaseConverter.IEnumerableType || type == XmlBaseConverter.StringType || type.IsArray;
		}

		// Token: 0x0600242C RID: 9260 RVA: 0x000CC2CC File Offset: 0x000CA4CC
		private T[] ToArray<T>(object list, IXmlNamespaceResolver nsResolver)
		{
			IList list2 = list as IList;
			if (list2 != null)
			{
				T[] array = new T[list2.Count];
				for (int i = 0; i < list2.Count; i++)
				{
					array[i] = (T)((object)this.atomicConverter.ChangeType(list2[i], typeof(T), nsResolver));
				}
				return array;
			}
			IEnumerable enumerable = list as IEnumerable;
			List<T> list3 = new List<T>();
			foreach (object obj in enumerable)
			{
				list3.Add((T)((object)this.atomicConverter.ChangeType(obj, typeof(T), nsResolver)));
			}
			return list3.ToArray();
		}

		// Token: 0x0600242D RID: 9261 RVA: 0x000CC3A4 File Offset: 0x000CA5A4
		private IList ToList(object list, IXmlNamespaceResolver nsResolver)
		{
			IList list2 = list as IList;
			if (list2 != null)
			{
				object[] array = new object[list2.Count];
				for (int i = 0; i < list2.Count; i++)
				{
					array[i] = this.atomicConverter.ChangeType(list2[i], XmlBaseConverter.ObjectType, nsResolver);
				}
				return array;
			}
			IEnumerable enumerable = list as IEnumerable;
			List<object> list3 = new List<object>();
			foreach (object obj in enumerable)
			{
				list3.Add(this.atomicConverter.ChangeType(obj, XmlBaseConverter.ObjectType, nsResolver));
			}
			return list3;
		}

		// Token: 0x0600242E RID: 9262 RVA: 0x000CC460 File Offset: 0x000CA660
		private List<string> StringAsList(string value)
		{
			return new List<string>(XmlConvert.SplitString(value));
		}

		// Token: 0x0600242F RID: 9263 RVA: 0x000CC470 File Offset: 0x000CA670
		private string ListAsString(IEnumerable list, IXmlNamespaceResolver nsResolver)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (object obj in list)
			{
				if (obj != null)
				{
					if (stringBuilder.Length != 0)
					{
						stringBuilder.Append(' ');
					}
					stringBuilder.Append(this.atomicConverter.ToString(obj, nsResolver));
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06002430 RID: 9264 RVA: 0x000CC4EC File Offset: 0x000CA6EC
		private new Exception CreateInvalidClrMappingException(Type sourceType, Type destinationType)
		{
			if (sourceType == destinationType)
			{
				return new InvalidCastException(Res.GetString("Xml type 'List of {0}' does not support Clr type '{1}'.", new object[] { base.XmlTypeName, sourceType.Name }));
			}
			return new InvalidCastException(Res.GetString("Xml type 'List of {0}' does not support a conversion from Clr type '{1}' to Clr type '{2}'.", new object[] { base.XmlTypeName, sourceType.Name, destinationType.Name }));
		}

		// Token: 0x040010B4 RID: 4276
		protected XmlValueConverter atomicConverter;
	}
}
