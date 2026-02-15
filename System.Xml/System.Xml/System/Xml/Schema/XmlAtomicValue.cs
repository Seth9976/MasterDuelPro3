using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Xml.XPath;

namespace System.Xml.Schema
{
	/// <summary>Represents the typed value of a validated XML element or attribute. The <see cref="T:System.Xml.Schema.XmlAtomicValue" /> class cannot be inherited.</summary>
	// Token: 0x020002B1 RID: 689
	public sealed class XmlAtomicValue : XPathItem, ICloneable
	{
		// Token: 0x06001F8F RID: 8079 RVA: 0x000BD237 File Offset: 0x000BB437
		internal XmlAtomicValue(XmlSchemaType xmlType, bool value)
		{
			if (xmlType == null)
			{
				throw new ArgumentNullException("xmlType");
			}
			this.xmlType = xmlType;
			this.clrType = TypeCode.Boolean;
			this.unionVal.boolVal = value;
		}

		// Token: 0x06001F90 RID: 8080 RVA: 0x000BD267 File Offset: 0x000BB467
		internal XmlAtomicValue(XmlSchemaType xmlType, DateTime value)
		{
			if (xmlType == null)
			{
				throw new ArgumentNullException("xmlType");
			}
			this.xmlType = xmlType;
			this.clrType = TypeCode.DateTime;
			this.unionVal.dtVal = value;
		}

		// Token: 0x06001F91 RID: 8081 RVA: 0x000BD298 File Offset: 0x000BB498
		internal XmlAtomicValue(XmlSchemaType xmlType, double value)
		{
			if (xmlType == null)
			{
				throw new ArgumentNullException("xmlType");
			}
			this.xmlType = xmlType;
			this.clrType = TypeCode.Double;
			this.unionVal.dblVal = value;
		}

		// Token: 0x06001F92 RID: 8082 RVA: 0x000BD2C9 File Offset: 0x000BB4C9
		internal XmlAtomicValue(XmlSchemaType xmlType, int value)
		{
			if (xmlType == null)
			{
				throw new ArgumentNullException("xmlType");
			}
			this.xmlType = xmlType;
			this.clrType = TypeCode.Int32;
			this.unionVal.i32Val = value;
		}

		// Token: 0x06001F93 RID: 8083 RVA: 0x000BD2FA File Offset: 0x000BB4FA
		internal XmlAtomicValue(XmlSchemaType xmlType, long value)
		{
			if (xmlType == null)
			{
				throw new ArgumentNullException("xmlType");
			}
			this.xmlType = xmlType;
			this.clrType = TypeCode.Int64;
			this.unionVal.i64Val = value;
		}

		// Token: 0x06001F94 RID: 8084 RVA: 0x000BD32B File Offset: 0x000BB52B
		internal XmlAtomicValue(XmlSchemaType xmlType, string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (xmlType == null)
			{
				throw new ArgumentNullException("xmlType");
			}
			this.xmlType = xmlType;
			this.objVal = value;
		}

		// Token: 0x06001F95 RID: 8085 RVA: 0x000BD360 File Offset: 0x000BB560
		internal XmlAtomicValue(XmlSchemaType xmlType, string value, IXmlNamespaceResolver nsResolver)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (xmlType == null)
			{
				throw new ArgumentNullException("xmlType");
			}
			this.xmlType = xmlType;
			this.objVal = value;
			if (nsResolver != null && (this.xmlType.TypeCode == XmlTypeCode.QName || this.xmlType.TypeCode == XmlTypeCode.Notation))
			{
				string prefixFromQName = this.GetPrefixFromQName(value);
				this.nsPrefix = new XmlAtomicValue.NamespacePrefixForQName(prefixFromQName, nsResolver.LookupNamespace(prefixFromQName));
			}
		}

		// Token: 0x06001F96 RID: 8086 RVA: 0x000BD32B File Offset: 0x000BB52B
		internal XmlAtomicValue(XmlSchemaType xmlType, object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (xmlType == null)
			{
				throw new ArgumentNullException("xmlType");
			}
			this.xmlType = xmlType;
			this.objVal = value;
		}

		// Token: 0x06001F97 RID: 8087 RVA: 0x000BD3DC File Offset: 0x000BB5DC
		internal XmlAtomicValue(XmlSchemaType xmlType, object value, IXmlNamespaceResolver nsResolver)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (xmlType == null)
			{
				throw new ArgumentNullException("xmlType");
			}
			this.xmlType = xmlType;
			this.objVal = value;
			if (nsResolver != null && (this.xmlType.TypeCode == XmlTypeCode.QName || this.xmlType.TypeCode == XmlTypeCode.Notation))
			{
				string @namespace = (this.objVal as XmlQualifiedName).Namespace;
				this.nsPrefix = new XmlAtomicValue.NamespacePrefixForQName(nsResolver.LookupPrefix(@namespace), @namespace);
			}
		}

		/// <summary>For a description of this member, see <see cref="M:System.Xml.Schema.XmlAtomicValue.Clone" />.</summary>
		/// <returns>Returns a copy of this <see cref="T:System.Xml.Schema.XmlAtomicValue" /> object.</returns>
		// Token: 0x06001F98 RID: 8088 RVA: 0x00035F33 File Offset: 0x00034133
		object ICloneable.Clone()
		{
			return this;
		}

		/// <summary>Gets the <see cref="T:System.Xml.Schema.XmlSchemaType" /> for the validated XML element or attribute.</summary>
		/// <returns>The <see cref="T:System.Xml.Schema.XmlSchemaType" /> for the validated XML element or attribute.</returns>
		// Token: 0x17000754 RID: 1876
		// (get) Token: 0x06001F99 RID: 8089 RVA: 0x000BD45E File Offset: 0x000BB65E
		public override XmlSchemaType XmlType
		{
			get
			{
				return this.xmlType;
			}
		}

		/// <summary>Gets the Microsoft .NET Framework type of the validated XML element or attribute.</summary>
		/// <returns>The .NET Framework type of the validated XML element or attribute. The default value is <see cref="T:System.String" />.</returns>
		// Token: 0x17000755 RID: 1877
		// (get) Token: 0x06001F9A RID: 8090 RVA: 0x000BD466 File Offset: 0x000BB666
		public override Type ValueType
		{
			get
			{
				return this.xmlType.Datatype.ValueType;
			}
		}

		/// <summary>Gets the current validated XML element or attribute as a boxed object of the most appropriate Microsoft .NET Framework type according to its schema type.</summary>
		/// <returns>The current validated XML element or attribute as a boxed object of the most appropriate .NET Framework type.</returns>
		// Token: 0x17000756 RID: 1878
		// (get) Token: 0x06001F9B RID: 8091 RVA: 0x000BD478 File Offset: 0x000BB678
		public override object TypedValue
		{
			get
			{
				XmlValueConverter valueConverter = this.xmlType.ValueConverter;
				if (this.objVal == null)
				{
					TypeCode typeCode = this.clrType;
					if (typeCode <= TypeCode.Int32)
					{
						if (typeCode == TypeCode.Boolean)
						{
							return valueConverter.ChangeType(this.unionVal.boolVal, this.ValueType);
						}
						if (typeCode == TypeCode.Int32)
						{
							return valueConverter.ChangeType(this.unionVal.i32Val, this.ValueType);
						}
					}
					else
					{
						if (typeCode == TypeCode.Int64)
						{
							return valueConverter.ChangeType(this.unionVal.i64Val, this.ValueType);
						}
						if (typeCode == TypeCode.Double)
						{
							return valueConverter.ChangeType(this.unionVal.dblVal, this.ValueType);
						}
						if (typeCode == TypeCode.DateTime)
						{
							return valueConverter.ChangeType(this.unionVal.dtVal, this.ValueType);
						}
					}
				}
				return valueConverter.ChangeType(this.objVal, this.ValueType, this.nsPrefix);
			}
		}

		/// <summary>Gets the validated XML element or attribute's value as a <see cref="T:System.Boolean" />.</summary>
		/// <returns>The validated XML element or attribute's value as a <see cref="T:System.Boolean" />.</returns>
		/// <exception cref="T:System.FormatException">The validated XML element or attribute's value is not in the correct format for the <see cref="T:System.Boolean" /> type.</exception>
		/// <exception cref="T:System.InvalidCastException">The attempted cast to <see cref="T:System.Boolean" /> is not valid.</exception>
		// Token: 0x17000757 RID: 1879
		// (get) Token: 0x06001F9C RID: 8092 RVA: 0x000BD558 File Offset: 0x000BB758
		public override bool ValueAsBoolean
		{
			get
			{
				XmlValueConverter valueConverter = this.xmlType.ValueConverter;
				if (this.objVal == null)
				{
					TypeCode typeCode = this.clrType;
					if (typeCode <= TypeCode.Int32)
					{
						if (typeCode == TypeCode.Boolean)
						{
							return this.unionVal.boolVal;
						}
						if (typeCode == TypeCode.Int32)
						{
							return valueConverter.ToBoolean(this.unionVal.i32Val);
						}
					}
					else
					{
						if (typeCode == TypeCode.Int64)
						{
							return valueConverter.ToBoolean(this.unionVal.i64Val);
						}
						if (typeCode == TypeCode.Double)
						{
							return valueConverter.ToBoolean(this.unionVal.dblVal);
						}
						if (typeCode == TypeCode.DateTime)
						{
							return valueConverter.ToBoolean(this.unionVal.dtVal);
						}
					}
				}
				return valueConverter.ToBoolean(this.objVal);
			}
		}

		/// <summary>Gets the validated XML element or attribute's value as a <see cref="T:System.DateTime" />.</summary>
		/// <returns>The validated XML element or attribute's value as a <see cref="T:System.DateTime" />.</returns>
		/// <exception cref="T:System.FormatException">The validated XML element or attribute's value is not in the correct format for the <see cref="T:System.DateTime" /> type.</exception>
		/// <exception cref="T:System.InvalidCastException">The attempted cast to <see cref="T:System.DateTime" /> is not valid.</exception>
		// Token: 0x17000758 RID: 1880
		// (get) Token: 0x06001F9D RID: 8093 RVA: 0x000BD604 File Offset: 0x000BB804
		public override DateTime ValueAsDateTime
		{
			get
			{
				XmlValueConverter valueConverter = this.xmlType.ValueConverter;
				if (this.objVal == null)
				{
					TypeCode typeCode = this.clrType;
					if (typeCode <= TypeCode.Int32)
					{
						if (typeCode == TypeCode.Boolean)
						{
							return valueConverter.ToDateTime(this.unionVal.boolVal);
						}
						if (typeCode == TypeCode.Int32)
						{
							return valueConverter.ToDateTime(this.unionVal.i32Val);
						}
					}
					else
					{
						if (typeCode == TypeCode.Int64)
						{
							return valueConverter.ToDateTime(this.unionVal.i64Val);
						}
						if (typeCode == TypeCode.Double)
						{
							return valueConverter.ToDateTime(this.unionVal.dblVal);
						}
						if (typeCode == TypeCode.DateTime)
						{
							return this.unionVal.dtVal;
						}
					}
				}
				return valueConverter.ToDateTime(this.objVal);
			}
		}

		/// <summary>Gets the validated XML element or attribute's value as a <see cref="T:System.Double" />.</summary>
		/// <returns>The validated XML element or attribute's value as a <see cref="T:System.Double" />.</returns>
		/// <exception cref="T:System.FormatException">The validated XML element or attribute's value is not in the correct format for the <see cref="T:System.Double" /> type.</exception>
		/// <exception cref="T:System.InvalidCastException">The attempted cast to <see cref="T:System.Double" /> is not valid.</exception>
		/// <exception cref="T:System.OverflowException">The attempted cast resulted in an overflow.</exception>
		// Token: 0x17000759 RID: 1881
		// (get) Token: 0x06001F9E RID: 8094 RVA: 0x000BD6B0 File Offset: 0x000BB8B0
		public override double ValueAsDouble
		{
			get
			{
				XmlValueConverter valueConverter = this.xmlType.ValueConverter;
				if (this.objVal == null)
				{
					TypeCode typeCode = this.clrType;
					if (typeCode <= TypeCode.Int32)
					{
						if (typeCode == TypeCode.Boolean)
						{
							return valueConverter.ToDouble(this.unionVal.boolVal);
						}
						if (typeCode == TypeCode.Int32)
						{
							return valueConverter.ToDouble(this.unionVal.i32Val);
						}
					}
					else
					{
						if (typeCode == TypeCode.Int64)
						{
							return valueConverter.ToDouble(this.unionVal.i64Val);
						}
						if (typeCode == TypeCode.Double)
						{
							return this.unionVal.dblVal;
						}
						if (typeCode == TypeCode.DateTime)
						{
							return valueConverter.ToDouble(this.unionVal.dtVal);
						}
					}
				}
				return valueConverter.ToDouble(this.objVal);
			}
		}

		/// <summary>Gets the validated XML element or attribute's value as an <see cref="T:System.Int32" />.</summary>
		/// <returns>The validated XML element or attribute's value as an <see cref="T:System.Int32" />.</returns>
		/// <exception cref="T:System.FormatException">The validated XML element or attribute's value is not in the correct format for the <see cref="T:System.Int32" /> type.</exception>
		/// <exception cref="T:System.InvalidCastException">The attempted cast to <see cref="T:System.Int32" /> is not valid.</exception>
		/// <exception cref="T:System.OverflowException">The attempted cast resulted in an overflow.</exception>
		// Token: 0x1700075A RID: 1882
		// (get) Token: 0x06001F9F RID: 8095 RVA: 0x000BD75C File Offset: 0x000BB95C
		public override int ValueAsInt
		{
			get
			{
				XmlValueConverter valueConverter = this.xmlType.ValueConverter;
				if (this.objVal == null)
				{
					TypeCode typeCode = this.clrType;
					if (typeCode <= TypeCode.Int32)
					{
						if (typeCode == TypeCode.Boolean)
						{
							return valueConverter.ToInt32(this.unionVal.boolVal);
						}
						if (typeCode == TypeCode.Int32)
						{
							return this.unionVal.i32Val;
						}
					}
					else
					{
						if (typeCode == TypeCode.Int64)
						{
							return valueConverter.ToInt32(this.unionVal.i64Val);
						}
						if (typeCode == TypeCode.Double)
						{
							return valueConverter.ToInt32(this.unionVal.dblVal);
						}
						if (typeCode == TypeCode.DateTime)
						{
							return valueConverter.ToInt32(this.unionVal.dtVal);
						}
					}
				}
				return valueConverter.ToInt32(this.objVal);
			}
		}

		/// <summary>Gets the validated XML element or attribute's value as an <see cref="T:System.Int64" />.</summary>
		/// <returns>The validated XML element or attribute's value as an <see cref="T:System.Int64" />.</returns>
		/// <exception cref="T:System.FormatException">The validated XML element or attribute's value is not in the correct format for the <see cref="T:System.Int64" /> type.</exception>
		/// <exception cref="T:System.InvalidCastException">The attempted cast to <see cref="T:System.Int64" /> is not valid.</exception>
		/// <exception cref="T:System.OverflowException">The attempted cast resulted in an overflow.</exception>
		// Token: 0x1700075B RID: 1883
		// (get) Token: 0x06001FA0 RID: 8096 RVA: 0x000BD808 File Offset: 0x000BBA08
		public override long ValueAsLong
		{
			get
			{
				XmlValueConverter valueConverter = this.xmlType.ValueConverter;
				if (this.objVal == null)
				{
					TypeCode typeCode = this.clrType;
					if (typeCode <= TypeCode.Int32)
					{
						if (typeCode == TypeCode.Boolean)
						{
							return valueConverter.ToInt64(this.unionVal.boolVal);
						}
						if (typeCode == TypeCode.Int32)
						{
							return valueConverter.ToInt64(this.unionVal.i32Val);
						}
					}
					else
					{
						if (typeCode == TypeCode.Int64)
						{
							return this.unionVal.i64Val;
						}
						if (typeCode == TypeCode.Double)
						{
							return valueConverter.ToInt64(this.unionVal.dblVal);
						}
						if (typeCode == TypeCode.DateTime)
						{
							return valueConverter.ToInt64(this.unionVal.dtVal);
						}
					}
				}
				return valueConverter.ToInt64(this.objVal);
			}
		}

		/// <summary>Returns the validated XML element or attribute's value as the type specified using the <see cref="T:System.Xml.IXmlNamespaceResolver" /> object specified to resolve namespace prefixes.</summary>
		/// <returns>The value of the validated XML element or attribute as the type requested.</returns>
		/// <param name="type">The type to return the validated XML element or attribute's value as.</param>
		/// <param name="nsResolver">The <see cref="T:System.Xml.IXmlNamespaceResolver" /> object used to resolve namespace prefixes.</param>
		/// <exception cref="T:System.FormatException">The validated XML element or attribute's value is not in the correct format for the target type.</exception>
		/// <exception cref="T:System.InvalidCastException">The attempted cast is not valid.</exception>
		/// <exception cref="T:System.OverflowException">The attempted cast resulted in an overflow.</exception>
		// Token: 0x06001FA1 RID: 8097 RVA: 0x000BD8B4 File Offset: 0x000BBAB4
		public override object ValueAs(Type type, IXmlNamespaceResolver nsResolver)
		{
			XmlValueConverter valueConverter = this.xmlType.ValueConverter;
			if (type == typeof(XPathItem) || type == typeof(XmlAtomicValue))
			{
				return this;
			}
			if (this.objVal == null)
			{
				TypeCode typeCode = this.clrType;
				if (typeCode <= TypeCode.Int32)
				{
					if (typeCode == TypeCode.Boolean)
					{
						return valueConverter.ChangeType(this.unionVal.boolVal, type);
					}
					if (typeCode == TypeCode.Int32)
					{
						return valueConverter.ChangeType(this.unionVal.i32Val, type);
					}
				}
				else
				{
					if (typeCode == TypeCode.Int64)
					{
						return valueConverter.ChangeType(this.unionVal.i64Val, type);
					}
					if (typeCode == TypeCode.Double)
					{
						return valueConverter.ChangeType(this.unionVal.dblVal, type);
					}
					if (typeCode == TypeCode.DateTime)
					{
						return valueConverter.ChangeType(this.unionVal.dtVal, type);
					}
				}
			}
			return valueConverter.ChangeType(this.objVal, type, nsResolver);
		}

		/// <summary>Gets the string value of the validated XML element or attribute.</summary>
		/// <returns>The string value of the validated XML element or attribute.</returns>
		// Token: 0x1700075C RID: 1884
		// (get) Token: 0x06001FA2 RID: 8098 RVA: 0x000BD994 File Offset: 0x000BBB94
		public override string Value
		{
			get
			{
				XmlValueConverter valueConverter = this.xmlType.ValueConverter;
				if (this.objVal == null)
				{
					TypeCode typeCode = this.clrType;
					if (typeCode <= TypeCode.Int32)
					{
						if (typeCode == TypeCode.Boolean)
						{
							return valueConverter.ToString(this.unionVal.boolVal);
						}
						if (typeCode == TypeCode.Int32)
						{
							return valueConverter.ToString(this.unionVal.i32Val);
						}
					}
					else
					{
						if (typeCode == TypeCode.Int64)
						{
							return valueConverter.ToString(this.unionVal.i64Val);
						}
						if (typeCode == TypeCode.Double)
						{
							return valueConverter.ToString(this.unionVal.dblVal);
						}
						if (typeCode == TypeCode.DateTime)
						{
							return valueConverter.ToString(this.unionVal.dtVal);
						}
					}
				}
				return valueConverter.ToString(this.objVal, this.nsPrefix);
			}
		}

		/// <summary>Gets the string value of the validated XML element or attribute.</summary>
		/// <returns>The string value of the validated XML element or attribute.</returns>
		// Token: 0x06001FA3 RID: 8099 RVA: 0x0004CFE3 File Offset: 0x0004B1E3
		public override string ToString()
		{
			return this.Value;
		}

		// Token: 0x06001FA4 RID: 8100 RVA: 0x000BDA4C File Offset: 0x000BBC4C
		private string GetPrefixFromQName(string value)
		{
			int num2;
			int num = ValidateNames.ParseQName(value, 0, out num2);
			if (num == 0 || num != value.Length)
			{
				return null;
			}
			if (num2 != 0)
			{
				return value.Substring(0, num2);
			}
			return string.Empty;
		}

		// Token: 0x04000EC9 RID: 3785
		private XmlSchemaType xmlType;

		// Token: 0x04000ECA RID: 3786
		private object objVal;

		// Token: 0x04000ECB RID: 3787
		private TypeCode clrType;

		// Token: 0x04000ECC RID: 3788
		private XmlAtomicValue.Union unionVal;

		// Token: 0x04000ECD RID: 3789
		private XmlAtomicValue.NamespacePrefixForQName nsPrefix;

		// Token: 0x020002B2 RID: 690
		[StructLayout(LayoutKind.Explicit, Size = 8)]
		private struct Union
		{
			// Token: 0x04000ECE RID: 3790
			[FieldOffset(0)]
			public bool boolVal;

			// Token: 0x04000ECF RID: 3791
			[FieldOffset(0)]
			public double dblVal;

			// Token: 0x04000ED0 RID: 3792
			[FieldOffset(0)]
			public long i64Val;

			// Token: 0x04000ED1 RID: 3793
			[FieldOffset(0)]
			public int i32Val;

			// Token: 0x04000ED2 RID: 3794
			[FieldOffset(0)]
			public DateTime dtVal;
		}

		// Token: 0x020002B3 RID: 691
		private class NamespacePrefixForQName : IXmlNamespaceResolver
		{
			// Token: 0x06001FA5 RID: 8101 RVA: 0x000BDA82 File Offset: 0x000BBC82
			public NamespacePrefixForQName(string prefix, string ns)
			{
				this.ns = ns;
				this.prefix = prefix;
			}

			// Token: 0x06001FA6 RID: 8102 RVA: 0x000BDA98 File Offset: 0x000BBC98
			public string LookupNamespace(string prefix)
			{
				if (prefix == this.prefix)
				{
					return this.ns;
				}
				return null;
			}

			// Token: 0x06001FA7 RID: 8103 RVA: 0x000BDAB0 File Offset: 0x000BBCB0
			public string LookupPrefix(string namespaceName)
			{
				if (this.ns == namespaceName)
				{
					return this.prefix;
				}
				return null;
			}

			// Token: 0x06001FA8 RID: 8104 RVA: 0x000BDAC8 File Offset: 0x000BBCC8
			public IDictionary<string, string> GetNamespacesInScope(XmlNamespaceScope scope)
			{
				Dictionary<string, string> dictionary = new Dictionary<string, string>(1);
				dictionary[this.prefix] = this.ns;
				return dictionary;
			}

			// Token: 0x04000ED3 RID: 3795
			public string prefix;

			// Token: 0x04000ED4 RID: 3796
			public string ns;
		}
	}
}
