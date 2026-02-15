using System;
using System.Data.Common;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Data.SqlTypes
{
	/// <summary>Represents an 8-bit unsigned integer, in the range of 0 through 255, to be stored in or retrieved from a database. </summary>
	// Token: 0x020000C0 RID: 192
	[XmlSchemaProvider("GetXsdType")]
	[Serializable]
	public struct SqlByte : INullable, IComparable, IXmlSerializable
	{
		// Token: 0x0600096D RID: 2413 RVA: 0x00037FC6 File Offset: 0x000361C6
		private SqlByte(bool fNull)
		{
			this.m_fNotNull = false;
			this.m_value = 0;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlByte" /> structure using the specified byte value.</summary>
		/// <param name="value">A byte value to be stored in the <see cref="P:System.Data.SqlTypes.SqlByte.Value" /> property of the new <see cref="T:System.Data.SqlTypes.SqlByte" /> structure. </param>
		// Token: 0x0600096E RID: 2414 RVA: 0x00037FD6 File Offset: 0x000361D6
		public SqlByte(byte value)
		{
			this.m_value = value;
			this.m_fNotNull = true;
		}

		/// <summary>Gets a Boolean value that indicates whether this <see cref="T:System.Data.SqlTypes.SqlByte" /> structure is null.</summary>
		/// <returns>true if null. Otherwise, false.</returns>
		// Token: 0x1700017B RID: 379
		// (get) Token: 0x0600096F RID: 2415 RVA: 0x00037FE6 File Offset: 0x000361E6
		public bool IsNull
		{
			get
			{
				return !this.m_fNotNull;
			}
		}

		/// <summary>Gets the value of the <see cref="T:System.Data.SqlTypes.SqlByte" /> structure. This property is read-only </summary>
		/// <returns>The value of the <see cref="T:System.Data.SqlTypes.SqlByte" /> structure.</returns>
		// Token: 0x1700017C RID: 380
		// (get) Token: 0x06000970 RID: 2416 RVA: 0x00037FF1 File Offset: 0x000361F1
		public byte Value
		{
			get
			{
				if (this.m_fNotNull)
				{
					return this.m_value;
				}
				throw new SqlNullValueException();
			}
		}

		/// <summary>Converts the supplied byte value to a <see cref="T:System.Data.SqlTypes.SqlByte" />.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure whose <see cref="P:System.Data.SqlTypes.SqlByte.Value" /> property is equal to the supplied parameter.</returns>
		/// <param name="x">A byte value to be converted to <see cref="T:System.Data.SqlTypes.SqlByte" />. </param>
		// Token: 0x06000971 RID: 2417 RVA: 0x00038007 File Offset: 0x00036207
		public static implicit operator SqlByte(byte x)
		{
			return new SqlByte(x);
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlByte" /> structure to a <see cref="T:System.String" />.</summary>
		/// <returns>A string that contains the <see cref="P:System.Data.SqlTypes.SqlByte.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlByte" />. If the Value is null, the String will be a null string.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06000972 RID: 2418 RVA: 0x0003800F File Offset: 0x0003620F
		public override string ToString()
		{
			if (!this.IsNull)
			{
				return this.m_value.ToString(null);
			}
			return SQLResource.NullString;
		}

		/// <summary>Computes the sum of the two specified <see cref="T:System.Data.SqlTypes.SqlByte" /> structures.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlByte" /> whose <see cref="P:System.Data.SqlTypes.SqlByte.Value" /> property contains the sum of the two operands.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure. </param>
		// Token: 0x06000973 RID: 2419 RVA: 0x0003802C File Offset: 0x0003622C
		public static SqlByte operator +(SqlByte x, SqlByte y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlByte.Null;
			}
			int num = (int)(x.m_value + y.m_value);
			if ((num & SqlByte.s_iBitNotByteMax) != 0)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return new SqlByte((byte)num);
		}

		/// <summary>Subtracts the second <see cref="T:System.Data.SqlTypes.SqlByte" /> operand from the first.</summary>
		/// <returns>The results of subtracting the second <see cref="T:System.Data.SqlTypes.SqlByte" /> operand from the first.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure. </param>
		// Token: 0x06000974 RID: 2420 RVA: 0x0003807C File Offset: 0x0003627C
		public static SqlByte operator -(SqlByte x, SqlByte y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlByte.Null;
			}
			int num = (int)(x.m_value - y.m_value);
			if ((num & SqlByte.s_iBitNotByteMax) != 0)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return new SqlByte((byte)num);
		}

		/// <summary>Computes the product of the two <see cref="T:System.Data.SqlTypes.SqlByte" /> operands.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlByte" /> structure whose <see cref="P:System.Data.SqlTypes.SqlByte.Value" /> property contains the product of the multiplication.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure. </param>
		// Token: 0x06000975 RID: 2421 RVA: 0x000380CC File Offset: 0x000362CC
		public static SqlByte operator *(SqlByte x, SqlByte y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlByte.Null;
			}
			int num = (int)(x.m_value * y.m_value);
			if ((num & SqlByte.s_iBitNotByteMax) != 0)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return new SqlByte((byte)num);
		}

		/// <summary>Divides its first <see cref="T:System.Data.SqlTypes.SqlByte" /> operand by its second.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlByte" /> structure whose <see cref="P:System.Data.SqlTypes.SqlByte.Value" /> property contains the results of the division.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure. </param>
		// Token: 0x06000976 RID: 2422 RVA: 0x0003811A File Offset: 0x0003631A
		public static SqlByte operator /(SqlByte x, SqlByte y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlByte.Null;
			}
			if (y.m_value != 0)
			{
				return new SqlByte(x.m_value / y.m_value);
			}
			throw new DivideByZeroException(SQLResource.DivideByZeroMessage);
		}

		/// <summary>Converts the supplied <see cref="T:System.Data.SqlTypes.SqlInt64" /> to <see cref="T:System.Data.SqlTypes.SqlByte" />.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure whose <see cref="P:System.Data.SqlTypes.SqlByte.Value" /> property is equal to the <see cref="P:System.Data.SqlTypes.SqlInt64.Value" /> of the SqlInt64 parameter.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure. </param>
		// Token: 0x06000977 RID: 2423 RVA: 0x0003815C File Offset: 0x0003635C
		public static explicit operator SqlByte(SqlInt64 x)
		{
			if (x.IsNull)
			{
				return SqlByte.Null;
			}
			if (x.Value > 255L || x.Value < 0L)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			if (!x.IsNull)
			{
				return new SqlByte((byte)x.Value);
			}
			return SqlByte.Null;
		}

		/// <summary>Performs a logical comparison of two <see cref="T:System.Data.SqlTypes.SqlByte" /> structures to determine whether they are equal.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the two instances are equal or <see cref="F:System.Data.SqlTypes.SqlBoolean.False" /> if the two instances are not equal. If either instance of <see cref="T:System.Data.SqlTypes.SqlByte" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure. </param>
		// Token: 0x06000978 RID: 2424 RVA: 0x000381B9 File Offset: 0x000363B9
		public static SqlBoolean operator ==(SqlByte x, SqlByte y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value == y.m_value);
			}
			return SqlBoolean.Null;
		}

		/// <summary>Compares two instances of <see cref="T:System.Data.SqlTypes.SqlByte" /> to determine whether the first is less than the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is less than the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlByte" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure. </param>
		// Token: 0x06000979 RID: 2425 RVA: 0x000381E6 File Offset: 0x000363E6
		public static SqlBoolean operator <(SqlByte x, SqlByte y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value < y.m_value);
			}
			return SqlBoolean.Null;
		}

		/// <summary>Compares two instances of <see cref="T:System.Data.SqlTypes.SqlByte" /> to determine whether the first is greater than the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is greater than the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlByte" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure. </param>
		// Token: 0x0600097A RID: 2426 RVA: 0x00038213 File Offset: 0x00036413
		public static SqlBoolean operator >(SqlByte x, SqlByte y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value > y.m_value);
			}
			return SqlBoolean.Null;
		}

		/// <summary>Compares two instances of <see cref="T:System.Data.SqlTypes.SqlByte" /> to determine whether the first is less than the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is less than the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlByte" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure. </param>
		// Token: 0x0600097B RID: 2427 RVA: 0x00038240 File Offset: 0x00036440
		public static SqlBoolean LessThan(SqlByte x, SqlByte y)
		{
			return x < y;
		}

		/// <summary>Compares two instances of <see cref="T:System.Data.SqlTypes.SqlByte" /> to determine whether the first is greater than the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is greater than the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlByte" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure. </param>
		// Token: 0x0600097C RID: 2428 RVA: 0x00038249 File Offset: 0x00036449
		public static SqlBoolean GreaterThan(SqlByte x, SqlByte y)
		{
			return x > y;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlByte" /> structure to <see cref="T:System.Data.SqlTypes.SqlDouble" />.</summary>
		/// <returns>A SqlDouble structure with the same value as this <see cref="T:System.Data.SqlTypes.SqlByte" />.</returns>
		// Token: 0x0600097D RID: 2429 RVA: 0x00038252 File Offset: 0x00036452
		public SqlDouble ToSqlDouble()
		{
			return this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlByte" /> structure to <see cref="T:System.Data.SqlTypes.SqlInt64" />.</summary>
		/// <returns>A SqlInt64 structure who <see cref="P:System.Data.SqlTypes.SqlInt64.Value" /> equals the <see cref="P:System.Data.SqlTypes.SqlByte.Value" /> of this <see cref="T:System.Data.SqlTypes.SqlByte" />.</returns>
		// Token: 0x0600097E RID: 2430 RVA: 0x0003825F File Offset: 0x0003645F
		public SqlInt64 ToSqlInt64()
		{
			return this;
		}

		/// <summary>Compares this instance to the supplied <see cref="T:System.Object" /> and returns an indication of their relative values.</summary>
		/// <returns>A signed number that indicates the relative values of the instance and the object.Return Value Condition Less than zero This instance is less than the object. Zero This instance is the same as the object. Greater than zero This instance is greater than the object -or- The object is a null reference (Nothing in Visual Basic) </returns>
		/// <param name="value">The <see cref="T:System.Object" /> to be compared.</param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600097F RID: 2431 RVA: 0x0003826C File Offset: 0x0003646C
		public int CompareTo(object value)
		{
			if (value is SqlByte)
			{
				SqlByte sqlByte = (SqlByte)value;
				return this.CompareTo(sqlByte);
			}
			throw ADP.WrongType(value.GetType(), typeof(SqlByte));
		}

		/// <summary>Compares this instance to the supplied <see cref="T:System.Data.SqlTypes.SqlByte" /> object and returns an indication of their relative values.</summary>
		/// <returns>A signed number that indicates the relative values of the instance and the object.Return Value Condition Less than zero This instance is less than the object. Zero This instance is the same as the object. Greater than zero This instance is greater than the object -or- The object is a null reference (Nothing in Visual Basic) </returns>
		/// <param name="value">The <see cref="T:System.Data.SqlTypes.SqlByte" /> object to be compared.</param>
		// Token: 0x06000980 RID: 2432 RVA: 0x000382A8 File Offset: 0x000364A8
		public int CompareTo(SqlByte value)
		{
			if (this.IsNull)
			{
				if (!value.IsNull)
				{
					return -1;
				}
				return 0;
			}
			else
			{
				if (value.IsNull)
				{
					return 1;
				}
				if (this < value)
				{
					return -1;
				}
				if (this > value)
				{
					return 1;
				}
				return 0;
			}
		}

		/// <summary>Compares the supplied <see cref="T:System.Object" /> parameter to the <see cref="P:System.Data.SqlTypes.SqlByte.Value" /> property of the <see cref="T:System.Data.SqlTypes.SqlByte" /> object.</summary>
		/// <returns>true if object is an instance of <see cref="T:System.Data.SqlTypes.SqlByte" /> and the two are equal; otherwise false.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to be compared. </param>
		// Token: 0x06000981 RID: 2433 RVA: 0x00038300 File Offset: 0x00036500
		public override bool Equals(object value)
		{
			if (!(value is SqlByte))
			{
				return false;
			}
			SqlByte sqlByte = (SqlByte)value;
			if (sqlByte.IsNull || this.IsNull)
			{
				return sqlByte.IsNull && this.IsNull;
			}
			return (this == sqlByte).Value;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06000982 RID: 2434 RVA: 0x00038358 File Offset: 0x00036558
		public override int GetHashCode()
		{
			if (!this.IsNull)
			{
				return this.Value.GetHashCode();
			}
			return 0;
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <returns>An XmlSchema.</returns>
		// Token: 0x06000983 RID: 2435 RVA: 0x00011F10 File Offset: 0x00010110
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <param name="reader">XmlReader </param>
		// Token: 0x06000984 RID: 2436 RVA: 0x00038380 File Offset: 0x00036580
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			string attribute = reader.GetAttribute("nil", "http://www.w3.org/2001/XMLSchema-instance");
			if (attribute != null && XmlConvert.ToBoolean(attribute))
			{
				reader.ReadElementString();
				this.m_fNotNull = false;
				return;
			}
			this.m_value = XmlConvert.ToByte(reader.ReadElementString());
			this.m_fNotNull = true;
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <param name="writer">XmlWriter </param>
		// Token: 0x06000985 RID: 2437 RVA: 0x000383D0 File Offset: 0x000365D0
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			if (this.IsNull)
			{
				writer.WriteAttributeString("xsi", "nil", "http://www.w3.org/2001/XMLSchema-instance", "true");
				return;
			}
			writer.WriteString(XmlConvert.ToString(this.m_value));
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the specified <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</summary>
		/// <returns>A string value that indicates the XSD of the specified <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</returns>
		/// <param name="schemaSet">A <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</param>
		// Token: 0x06000986 RID: 2438 RVA: 0x00038406 File Offset: 0x00036606
		public static XmlQualifiedName GetXsdType(XmlSchemaSet schemaSet)
		{
			return new XmlQualifiedName("unsignedByte", "http://www.w3.org/2001/XMLSchema");
		}

		// Token: 0x040003C3 RID: 963
		private bool m_fNotNull;

		// Token: 0x040003C4 RID: 964
		private byte m_value;

		// Token: 0x040003C5 RID: 965
		private static readonly int s_iBitNotByteMax = -256;

		/// <summary>Represents a <see cref="T:System.DBNull" /> that can be assigned to this instance of the <see cref="T:System.Data.SqlTypes.SqlByte" /> structure.</summary>
		// Token: 0x040003C6 RID: 966
		public static readonly SqlByte Null = new SqlByte(true);

		/// <summary>Represents a zero value that can be assigned to the <see cref="P:System.Data.SqlTypes.SqlByte.Value" /> property of an instance of the <see cref="T:System.Data.SqlTypes.SqlByte" /> structure.</summary>
		// Token: 0x040003C7 RID: 967
		public static readonly SqlByte Zero = new SqlByte(0);

		/// <summary>A constant representing the smallest possible value of a <see cref="T:System.Data.SqlTypes.SqlByte" />.</summary>
		// Token: 0x040003C8 RID: 968
		public static readonly SqlByte MinValue = new SqlByte(0);

		/// <summary>A constant representing the largest possible value of a <see cref="T:System.Data.SqlTypes.SqlByte" />.</summary>
		// Token: 0x040003C9 RID: 969
		public static readonly SqlByte MaxValue = new SqlByte(byte.MaxValue);
	}
}
