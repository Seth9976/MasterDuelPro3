using System;
using System.Data.Common;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Data.SqlTypes
{
	/// <summary>Represents a 32-bit signed integer to be stored in or retrieved from a database.</summary>
	// Token: 0x020000C9 RID: 201
	[XmlSchemaProvider("GetXsdType")]
	[Serializable]
	public struct SqlInt32 : INullable, IComparable, IXmlSerializable
	{
		// Token: 0x06000A5E RID: 2654 RVA: 0x0003C167 File Offset: 0x0003A367
		private SqlInt32(bool fNull)
		{
			this.m_fNotNull = false;
			this.m_value = 0;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure using the supplied integer value.</summary>
		/// <param name="value">The integer to be converted. </param>
		// Token: 0x06000A5F RID: 2655 RVA: 0x0003C177 File Offset: 0x0003A377
		public SqlInt32(int value)
		{
			this.m_value = value;
			this.m_fNotNull = true;
		}

		/// <summary>Indicates whether this <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure is null.</summary>
		/// <returns>This property is true if <see cref="P:System.Data.SqlTypes.SqlInt32.Value" /> is null. Otherwise, false.</returns>
		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06000A60 RID: 2656 RVA: 0x0003C187 File Offset: 0x0003A387
		public bool IsNull
		{
			get
			{
				return !this.m_fNotNull;
			}
		}

		/// <summary>Gets the value of this <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure. This property is read-only.</summary>
		/// <returns>An integer representing the value of this <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure.</returns>
		/// <exception cref="T:System.Data.SqlTypes.SqlNullValueException">The property contains <see cref="F:System.Data.SqlTypes.SqlInt32.Null" />. </exception>
		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06000A61 RID: 2657 RVA: 0x0003C192 File Offset: 0x0003A392
		public int Value
		{
			get
			{
				if (this.IsNull)
				{
					throw new SqlNullValueException();
				}
				return this.m_value;
			}
		}

		/// <summary>Converts the supplied integer to <see cref="T:System.Data.SqlTypes.SqlInt32" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure whose Value property is equal to the integer parameter.</returns>
		/// <param name="x">An integer value. </param>
		// Token: 0x06000A62 RID: 2658 RVA: 0x0003C1A8 File Offset: 0x0003A3A8
		public static implicit operator SqlInt32(int x)
		{
			return new SqlInt32(x);
		}

		/// <summary>Converts a <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure to a <see cref="T:System.String" />.</summary>
		/// <returns>A <see cref="T:System.String" /> structure equal to the value of this <see cref="T:System.Data.SqlTypes.SqlInt32" />.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06000A63 RID: 2659 RVA: 0x0003C1B0 File Offset: 0x0003A3B0
		public override string ToString()
		{
			if (!this.IsNull)
			{
				return this.m_value.ToString(null);
			}
			return SQLResource.NullString;
		}

		/// <summary>Negates the <see cref="P:System.Data.SqlTypes.SqlInt32.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlInt32" /> operand.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure that contains the negated value.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure. </param>
		// Token: 0x06000A64 RID: 2660 RVA: 0x0003C1CC File Offset: 0x0003A3CC
		public static SqlInt32 operator -(SqlInt32 x)
		{
			if (!x.IsNull)
			{
				return new SqlInt32(-x.m_value);
			}
			return SqlInt32.Null;
		}

		/// <summary>Computes the sum of the two specified <see cref="T:System.Data.SqlTypes.SqlInt32" /> structures.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt32.Value" /> property contains the sum of the specified <see cref="T:System.Data.SqlTypes.SqlInt32" /> structures.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure. </param>
		// Token: 0x06000A65 RID: 2661 RVA: 0x0003C1EC File Offset: 0x0003A3EC
		public static SqlInt32 operator +(SqlInt32 x, SqlInt32 y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlInt32.Null;
			}
			int num = x.m_value + y.m_value;
			if (SqlInt32.SameSignInt(x.m_value, y.m_value) && !SqlInt32.SameSignInt(x.m_value, num))
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return new SqlInt32(num);
		}

		/// <summary>Subtracts the second <see cref="T:System.Data.SqlTypes.SqlInt32" /> parameter from the first.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt32.Value" /> property contains the results of the subtraction.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure. </param>
		// Token: 0x06000A66 RID: 2662 RVA: 0x0003C254 File Offset: 0x0003A454
		public static SqlInt32 operator -(SqlInt32 x, SqlInt32 y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlInt32.Null;
			}
			int num = x.m_value - y.m_value;
			if (!SqlInt32.SameSignInt(x.m_value, y.m_value) && SqlInt32.SameSignInt(y.m_value, num))
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return new SqlInt32(num);
		}

		/// <summary>Computes the product of the two <see cref="T:System.Data.SqlTypes.SqlInt32" /> parameters.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt32.Value" /> contains the product of the two parameters.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure. </param>
		// Token: 0x06000A67 RID: 2663 RVA: 0x0003C2BC File Offset: 0x0003A4BC
		public static SqlInt32 operator *(SqlInt32 x, SqlInt32 y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlInt32.Null;
			}
			long num = (long)x.m_value * (long)y.m_value;
			long num2 = num & SqlInt32.s_lBitNotIntMax;
			if (num2 != 0L && num2 != SqlInt32.s_lBitNotIntMax)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return new SqlInt32((int)num);
		}

		/// <summary>Divides the first <see cref="T:System.Data.SqlTypes.SqlInt32" /> parameter from the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlInt32" /> whose <see cref="P:System.Data.SqlTypes.SqlInt32.Value" /> property contains the results of the division.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure. </param>
		// Token: 0x06000A68 RID: 2664 RVA: 0x0003C318 File Offset: 0x0003A518
		public static SqlInt32 operator /(SqlInt32 x, SqlInt32 y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlInt32.Null;
			}
			if (y.m_value == 0)
			{
				throw new DivideByZeroException(SQLResource.DivideByZeroMessage);
			}
			if ((long)x.m_value == SqlInt32.s_iIntMin && y.m_value == -1)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return new SqlInt32(x.m_value / y.m_value);
		}

		/// <summary>Converts the supplied <see cref="T:System.Data.SqlTypes.SqlByte" /> property to <see cref="T:System.Data.SqlTypes.SqlInt32" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt32.Value" /> property equals the <see cref="P:System.Data.SqlTypes.SqlByte.Value" /> property of the <see cref="T:System.Data.SqlTypes.SqlByte" /> parameter.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure. </param>
		// Token: 0x06000A69 RID: 2665 RVA: 0x0003C384 File Offset: 0x0003A584
		public static implicit operator SqlInt32(SqlByte x)
		{
			if (!x.IsNull)
			{
				return new SqlInt32((int)x.Value);
			}
			return SqlInt32.Null;
		}

		/// <summary>Converts the supplied <see cref="T:System.Data.SqlTypes.SqlInt16" /> to <see cref="T:System.Data.SqlTypes.SqlInt32" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt32.Value" /> property equals the <see cref="P:System.Data.SqlTypes.SqlInt16.Value" /> property of the <see cref="T:System.Data.SqlTypes.SqlInt16" /> parameter.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure. </param>
		// Token: 0x06000A6A RID: 2666 RVA: 0x0003C3A1 File Offset: 0x0003A5A1
		public static implicit operator SqlInt32(SqlInt16 x)
		{
			if (!x.IsNull)
			{
				return new SqlInt32((int)x.Value);
			}
			return SqlInt32.Null;
		}

		/// <summary>Converts the supplied <see cref="T:System.Data.SqlTypes.SqlInt64" /> to <see cref="T:System.Data.SqlTypes.SqlInt32" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt32.Value" /> property equals the <see cref="P:System.Data.SqlTypes.SqlInt64.Value" /> property of the SqlInt64 parameter.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure. </param>
		// Token: 0x06000A6B RID: 2667 RVA: 0x0003C3C0 File Offset: 0x0003A5C0
		public static explicit operator SqlInt32(SqlInt64 x)
		{
			if (x.IsNull)
			{
				return SqlInt32.Null;
			}
			long value = x.Value;
			if (value > 2147483647L || value < -2147483648L)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return new SqlInt32((int)value);
		}

		// Token: 0x06000A6C RID: 2668 RVA: 0x0003C408 File Offset: 0x0003A608
		private static bool SameSignInt(int x, int y)
		{
			return ((long)(x ^ y) & (long)((ulong)int.MinValue)) == 0L;
		}

		/// <summary>Performs a logical comparison of the two <see cref="T:System.Data.SqlTypes.SqlInt32" /> parameters to determine whether they are equal.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the two instances are equal or <see cref="F:System.Data.SqlTypes.SqlBoolean.False" /> if the two instances are not equal. If either instance of <see cref="T:System.Data.SqlTypes.SqlInt32" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure. </param>
		// Token: 0x06000A6D RID: 2669 RVA: 0x0003C419 File Offset: 0x0003A619
		public static SqlBoolean operator ==(SqlInt32 x, SqlInt32 y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value == y.m_value);
			}
			return SqlBoolean.Null;
		}

		/// <summary>Compares the two <see cref="T:System.Data.SqlTypes.SqlInt32" /> parameters to determine whether the first is less than the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is less than the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlInt32" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure. </param>
		// Token: 0x06000A6E RID: 2670 RVA: 0x0003C446 File Offset: 0x0003A646
		public static SqlBoolean operator <(SqlInt32 x, SqlInt32 y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value < y.m_value);
			}
			return SqlBoolean.Null;
		}

		/// <summary>Compares the two <see cref="T:System.Data.SqlTypes.SqlInt32" /> parameters to determine whether the first is greater than the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is greater than the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlInt32" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure. </param>
		// Token: 0x06000A6F RID: 2671 RVA: 0x0003C473 File Offset: 0x0003A673
		public static SqlBoolean operator >(SqlInt32 x, SqlInt32 y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value > y.m_value);
			}
			return SqlBoolean.Null;
		}

		/// <summary>Compares the two <see cref="T:System.Data.SqlTypes.SqlInt32" /> parameters to determine whether the first is less than the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is less than the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlInt32" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure. </param>
		// Token: 0x06000A70 RID: 2672 RVA: 0x0003C4A0 File Offset: 0x0003A6A0
		public static SqlBoolean LessThan(SqlInt32 x, SqlInt32 y)
		{
			return x < y;
		}

		/// <summary>Compares the two <see cref="T:System.Data.SqlTypes.SqlInt32" /> parameters to determine whether the first is greater than the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is greater than the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlInt32" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure. </param>
		// Token: 0x06000A71 RID: 2673 RVA: 0x0003C4A9 File Offset: 0x0003A6A9
		public static SqlBoolean GreaterThan(SqlInt32 x, SqlInt32 y)
		{
			return x > y;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure to <see cref="T:System.Data.SqlTypes.SqlDouble" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlDouble" /> structure equal to the value of this <see cref="T:System.Data.SqlTypes.SqlInt32" />.</returns>
		// Token: 0x06000A72 RID: 2674 RVA: 0x0003C4B2 File Offset: 0x0003A6B2
		public SqlDouble ToSqlDouble()
		{
			return this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure to <see cref="T:System.Data.SqlTypes.SqlInt64" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure equal to the value of this <see cref="T:System.Data.SqlTypes.SqlInt32" />.</returns>
		// Token: 0x06000A73 RID: 2675 RVA: 0x0003C4BF File Offset: 0x0003A6BF
		public SqlInt64 ToSqlInt64()
		{
			return this;
		}

		/// <summary>Compares this <see cref="T:System.Data.SqlTypes.SqlInt32" /> instance to the supplied <see cref="T:System.Object" /> and returns an indication of their relative values.</summary>
		/// <returns>A signed number that indicates the relative values of the instance and the object.Return value Condition Less than zero This instance is less than the object. Zero This instance is the same as the object. Greater than zero This instance is greater than the object -or- The object is a null reference (Nothing in Visual Basic). </returns>
		/// <param name="value">The <see cref="T:System.Object" /> to be compared. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000A74 RID: 2676 RVA: 0x0003C4CC File Offset: 0x0003A6CC
		public int CompareTo(object value)
		{
			if (value is SqlInt32)
			{
				SqlInt32 sqlInt = (SqlInt32)value;
				return this.CompareTo(sqlInt);
			}
			throw ADP.WrongType(value.GetType(), typeof(SqlInt32));
		}

		/// <summary>Compares this <see cref="T:System.Data.SqlTypes.SqlInt32" /> instance to the supplied <see cref="T:System.Data.SqlTypes.SqlInt32" /> and returns an indication of their relative values.</summary>
		/// <returns>A signed number that indicates the relative values of the instance and the object.Return value Condition Less than zero This instance is less than the object. Zero This instance is the same as the object. Greater than zero This instance is greater than the object -or- The object is a null reference (Nothing in Visual Basic) </returns>
		/// <param name="value">The <see cref="T:System.Data.SqlTypes.SqlInt32" /> to be compared.</param>
		// Token: 0x06000A75 RID: 2677 RVA: 0x0003C508 File Offset: 0x0003A708
		public int CompareTo(SqlInt32 value)
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

		/// <summary>Compares the supplied object parameter to the <see cref="P:System.Data.SqlTypes.SqlInt32.Value" /> property of the <see cref="T:System.Data.SqlTypes.SqlInt32" /> object.</summary>
		/// <returns>true if object is an instance of <see cref="T:System.Data.SqlTypes.SqlInt32" /> and the two are equal; otherwise false.</returns>
		/// <param name="value">The object to be compared. </param>
		// Token: 0x06000A76 RID: 2678 RVA: 0x0003C560 File Offset: 0x0003A760
		public override bool Equals(object value)
		{
			if (!(value is SqlInt32))
			{
				return false;
			}
			SqlInt32 sqlInt = (SqlInt32)value;
			if (sqlInt.IsNull || this.IsNull)
			{
				return sqlInt.IsNull && this.IsNull;
			}
			return (this == sqlInt).Value;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06000A77 RID: 2679 RVA: 0x0003C5B8 File Offset: 0x0003A7B8
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
		// Token: 0x06000A78 RID: 2680 RVA: 0x00011F10 File Offset: 0x00010110
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <param name="reader">XmlReader </param>
		// Token: 0x06000A79 RID: 2681 RVA: 0x0003C5E0 File Offset: 0x0003A7E0
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			string attribute = reader.GetAttribute("nil", "http://www.w3.org/2001/XMLSchema-instance");
			if (attribute != null && XmlConvert.ToBoolean(attribute))
			{
				reader.ReadElementString();
				this.m_fNotNull = false;
				return;
			}
			this.m_value = XmlConvert.ToInt32(reader.ReadElementString());
			this.m_fNotNull = true;
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <param name="writer">XmlWriter</param>
		// Token: 0x06000A7A RID: 2682 RVA: 0x0003C630 File Offset: 0x0003A830
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
		/// <param name="schemaSet">An <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</param>
		// Token: 0x06000A7B RID: 2683 RVA: 0x0003C666 File Offset: 0x0003A866
		public static XmlQualifiedName GetXsdType(XmlSchemaSet schemaSet)
		{
			return new XmlQualifiedName("int", "http://www.w3.org/2001/XMLSchema");
		}

		// Token: 0x04000442 RID: 1090
		private bool m_fNotNull;

		// Token: 0x04000443 RID: 1091
		private int m_value;

		// Token: 0x04000444 RID: 1092
		private static readonly long s_iIntMin = -2147483648L;

		// Token: 0x04000445 RID: 1093
		private static readonly long s_lBitNotIntMax = -2147483648L;

		/// <summary>Represents a <see cref="T:System.DBNull" /> that can be assigned to this instance of the <see cref="T:System.Data.SqlTypes.SqlInt32" /> class.</summary>
		// Token: 0x04000446 RID: 1094
		public static readonly SqlInt32 Null = new SqlInt32(true);

		/// <summary>Represents a zero value that can be assigned to the <see cref="P:System.Data.SqlTypes.SqlInt32.Value" /> property of an instance of the <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure.</summary>
		// Token: 0x04000447 RID: 1095
		public static readonly SqlInt32 Zero = new SqlInt32(0);

		/// <summary>A constant representing the smallest possible value of a <see cref="T:System.Data.SqlTypes.SqlInt32" />.</summary>
		// Token: 0x04000448 RID: 1096
		public static readonly SqlInt32 MinValue = new SqlInt32(int.MinValue);

		/// <summary>A constant representing the largest possible value of a <see cref="T:System.Data.SqlTypes.SqlInt32" />.</summary>
		// Token: 0x04000449 RID: 1097
		public static readonly SqlInt32 MaxValue = new SqlInt32(int.MaxValue);
	}
}
