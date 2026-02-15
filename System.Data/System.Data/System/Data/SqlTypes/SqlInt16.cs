using System;
using System.Data.Common;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Data.SqlTypes
{
	/// <summary>Represents a 16-bit signed integer to be stored in or retrieved from a database.</summary>
	// Token: 0x020000C8 RID: 200
	[XmlSchemaProvider("GetXsdType")]
	[Serializable]
	public struct SqlInt16 : INullable, IComparable, IXmlSerializable
	{
		// Token: 0x06000A41 RID: 2625 RVA: 0x0003BC6F File Offset: 0x00039E6F
		private SqlInt16(bool fNull)
		{
			this.m_fNotNull = false;
			this.m_value = 0;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure using the supplied short integer parameter.</summary>
		/// <param name="value">A short integer. </param>
		// Token: 0x06000A42 RID: 2626 RVA: 0x0003BC7F File Offset: 0x00039E7F
		public SqlInt16(short value)
		{
			this.m_value = value;
			this.m_fNotNull = true;
		}

		/// <summary>Indicates whether this <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure is null.</summary>
		/// <returns>true if null. Otherwise, false. For more information, see Handling Null Values (ADO.NET).</returns>
		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06000A43 RID: 2627 RVA: 0x0003BC8F File Offset: 0x00039E8F
		public bool IsNull
		{
			get
			{
				return !this.m_fNotNull;
			}
		}

		/// <summary>Gets the value of this instance of <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure. This property is read-only.</summary>
		/// <returns>A short integer representing the value of this <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure.</returns>
		// Token: 0x17000195 RID: 405
		// (get) Token: 0x06000A44 RID: 2628 RVA: 0x0003BC9A File Offset: 0x00039E9A
		public short Value
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

		/// <summary>Converts the supplied short integer to <see cref="T:System.Data.SqlTypes.SqlInt16" />.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure with the same value as the specified short integer.</returns>
		/// <param name="x">A short integer value. </param>
		// Token: 0x06000A45 RID: 2629 RVA: 0x0003BCB0 File Offset: 0x00039EB0
		public static implicit operator SqlInt16(short x)
		{
			return new SqlInt16(x);
		}

		/// <summary>Converts a <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure to <see cref="T:System.String" />.</summary>
		/// <returns>A <see cref="T:System.String" /> object representing the <see cref="P:System.Data.SqlTypes.SqlInt16.Value" /> of this instance of <see cref="T:System.Data.SqlTypes.SqlInt16" />.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06000A46 RID: 2630 RVA: 0x0003BCB8 File Offset: 0x00039EB8
		public override string ToString()
		{
			if (!this.IsNull)
			{
				return this.m_value.ToString(null);
			}
			return SQLResource.NullString;
		}

		/// <summary>The unary minus operator negates the <see cref="P:System.Data.SqlTypes.SqlInt16.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlInt16" /> operand.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure that contains the negated value.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure. </param>
		// Token: 0x06000A47 RID: 2631 RVA: 0x0003BCD4 File Offset: 0x00039ED4
		public static SqlInt16 operator -(SqlInt16 x)
		{
			if (!x.IsNull)
			{
				return new SqlInt16(-x.m_value);
			}
			return SqlInt16.Null;
		}

		/// <summary>Computes the sum of the two <see cref="T:System.Data.SqlTypes.SqlInt16" /> operands.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt16.Value" /> property contains the sum of the two <see cref="T:System.Data.SqlTypes.SqlInt16" /> operands.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure. </param>
		// Token: 0x06000A48 RID: 2632 RVA: 0x0003BCF4 File Offset: 0x00039EF4
		public static SqlInt16 operator +(SqlInt16 x, SqlInt16 y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlInt16.Null;
			}
			int num = (int)(x.m_value + y.m_value);
			if ((((num >> 15) ^ (num >> 16)) & 1) != 0)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return new SqlInt16((short)num);
		}

		/// <summary>Subtracts the second <see cref="T:System.Data.SqlTypes.SqlInt16" /> parameter from the first.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt16.Value" /> property contains the results of the subtraction.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure. </param>
		// Token: 0x06000A49 RID: 2633 RVA: 0x0003BD48 File Offset: 0x00039F48
		public static SqlInt16 operator -(SqlInt16 x, SqlInt16 y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlInt16.Null;
			}
			int num = (int)(x.m_value - y.m_value);
			if ((((num >> 15) ^ (num >> 16)) & 1) != 0)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return new SqlInt16((short)num);
		}

		/// <summary>Computes the product of the two <see cref="T:System.Data.SqlTypes.SqlInt16" /> parameters.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt16.Value" /> contains the product of the two parameters.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure. </param>
		// Token: 0x06000A4A RID: 2634 RVA: 0x0003BD9C File Offset: 0x00039F9C
		public static SqlInt16 operator *(SqlInt16 x, SqlInt16 y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlInt16.Null;
			}
			int num = (int)(x.m_value * y.m_value);
			int num2 = num & SqlInt16.s_MASKI2;
			if (num2 != 0 && num2 != SqlInt16.s_MASKI2)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return new SqlInt16((short)num);
		}

		/// <summary>Divides the first <see cref="T:System.Data.SqlTypes.SqlInt16" /> operand by the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlInt16" /> whose <see cref="P:System.Data.SqlTypes.SqlInt16.Value" /> property contains the results of the division.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure. </param>
		// Token: 0x06000A4B RID: 2635 RVA: 0x0003BDF4 File Offset: 0x00039FF4
		public static SqlInt16 operator /(SqlInt16 x, SqlInt16 y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlInt16.Null;
			}
			if (y.m_value == 0)
			{
				throw new DivideByZeroException(SQLResource.DivideByZeroMessage);
			}
			if (x.m_value == -32768 && y.m_value == -1)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return new SqlInt16(x.m_value / y.m_value);
		}

		/// <summary>Converts the supplied <see cref="T:System.Data.SqlTypes.SqlByte" /> structure to <see cref="T:System.Data.SqlTypes.SqlInt16" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt16.Value" /> property is equal to the <see cref="P:System.Data.SqlTypes.SqlByte.Value" /> property of the <see cref="T:System.Data.SqlTypes.SqlByte" /> parameter.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure. </param>
		// Token: 0x06000A4C RID: 2636 RVA: 0x0003BE60 File Offset: 0x0003A060
		public static implicit operator SqlInt16(SqlByte x)
		{
			if (!x.IsNull)
			{
				return new SqlInt16((short)x.Value);
			}
			return SqlInt16.Null;
		}

		/// <summary>Converts the supplied <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure to <see cref="T:System.Data.SqlTypes.SqlInt16" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt16.Value" /> property is equal to the <see cref="P:System.Data.SqlTypes.SqlInt64.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlInt64" /> parameter.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure. </param>
		// Token: 0x06000A4D RID: 2637 RVA: 0x0003BE80 File Offset: 0x0003A080
		public static explicit operator SqlInt16(SqlInt64 x)
		{
			if (x.IsNull)
			{
				return SqlInt16.Null;
			}
			long value = x.Value;
			if (value > 32767L || value < -32768L)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return new SqlInt16((short)value);
		}

		/// <summary>Performs a logical comparison of two <see cref="T:System.Data.SqlTypes.SqlInt16" /> structures to determine whether they are equal.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the two instances are equal or <see cref="F:System.Data.SqlTypes.SqlBoolean.False" /> if the two instances are not equal. If either instance of <see cref="T:System.Data.SqlTypes.SqlInt16" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure. </param>
		// Token: 0x06000A4E RID: 2638 RVA: 0x0003BEC8 File Offset: 0x0003A0C8
		public static SqlBoolean operator ==(SqlInt16 x, SqlInt16 y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value == y.m_value);
			}
			return SqlBoolean.Null;
		}

		/// <summary>Compares two instances of <see cref="T:System.Data.SqlTypes.SqlInt16" /> to determine whether the first is less than the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is less than the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlInt16" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure. </param>
		// Token: 0x06000A4F RID: 2639 RVA: 0x0003BEF5 File Offset: 0x0003A0F5
		public static SqlBoolean operator <(SqlInt16 x, SqlInt16 y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value < y.m_value);
			}
			return SqlBoolean.Null;
		}

		/// <summary>Compares two instances of <see cref="T:System.Data.SqlTypes.SqlInt16" /> to determine whether the first is greater than the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is greater than the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlInt16" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure. </param>
		// Token: 0x06000A50 RID: 2640 RVA: 0x0003BF22 File Offset: 0x0003A122
		public static SqlBoolean operator >(SqlInt16 x, SqlInt16 y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value > y.m_value);
			}
			return SqlBoolean.Null;
		}

		/// <summary>Compares two instances of <see cref="T:System.Data.SqlTypes.SqlInt16" /> to determine whether the first is less than the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is less than the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlInt16" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure. </param>
		// Token: 0x06000A51 RID: 2641 RVA: 0x0003BF4F File Offset: 0x0003A14F
		public static SqlBoolean LessThan(SqlInt16 x, SqlInt16 y)
		{
			return x < y;
		}

		/// <summary>Compares two instances of <see cref="T:System.Data.SqlTypes.SqlInt16" /> to determine whether the first is greater than the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is greater than the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlInt16" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure. </param>
		// Token: 0x06000A52 RID: 2642 RVA: 0x0003BF58 File Offset: 0x0003A158
		public static SqlBoolean GreaterThan(SqlInt16 x, SqlInt16 y)
		{
			return x > y;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure to <see cref="T:System.Data.SqlTypes.SqlDouble" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlDouble" /> structure whose Value equals the value of this <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure.</returns>
		// Token: 0x06000A53 RID: 2643 RVA: 0x0003BF61 File Offset: 0x0003A161
		public SqlDouble ToSqlDouble()
		{
			return this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure to <see cref="T:System.Data.SqlTypes.SqlInt64" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure whose Value equals the value of this <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure.</returns>
		// Token: 0x06000A54 RID: 2644 RVA: 0x0003BF6E File Offset: 0x0003A16E
		public SqlInt64 ToSqlInt64()
		{
			return this;
		}

		/// <summary>Compares this <see cref="T:System.Data.SqlTypes.SqlInt16" /> instance to the supplied <see cref="T:System.Object" /> and returns an indication of their relative values.</summary>
		/// <returns>A signed number that indicates the relative values of the instance and the object.Return value Condition Less than zero This instance is less than the object. Zero This instance is the same as the object. Greater than zero This instance is greater than the object -or- object is a null reference (Nothing in Visual Basic) </returns>
		/// <param name="value">The <see cref="T:System.Object" /> to be compared. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000A55 RID: 2645 RVA: 0x0003BF7C File Offset: 0x0003A17C
		public int CompareTo(object value)
		{
			if (value is SqlInt16)
			{
				SqlInt16 sqlInt = (SqlInt16)value;
				return this.CompareTo(sqlInt);
			}
			throw ADP.WrongType(value.GetType(), typeof(SqlInt16));
		}

		/// <summary>Compares this <see cref="T:System.Data.SqlTypes.SqlInt16" /> instance to the supplied <see cref="T:System.Data.SqlTypes.SqlInt16" /> and returns an indication of their relative values.</summary>
		/// <returns>A signed number that indicates the relative values of the instance and the object.Return value Condition Less than zero This instance is less than the object. Zero This instance is the same as the object. Greater than zero This instance is greater than the object -or- The object is a null reference (Nothing in Visual Basic) </returns>
		/// <param name="value">The <see cref="T:System.Data.SqlTypes.SqlInt16" /> to be compared.</param>
		// Token: 0x06000A56 RID: 2646 RVA: 0x0003BFB8 File Offset: 0x0003A1B8
		public int CompareTo(SqlInt16 value)
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

		/// <summary>Compares the specified object to the <see cref="P:System.Data.SqlTypes.SqlInt16.Value" /> property of the <see cref="T:System.Data.SqlTypes.SqlInt16" /> object.</summary>
		/// <returns>true if object is an instance of <see cref="T:System.Data.SqlTypes.SqlInt16" /> and the two are equal; otherwise false.</returns>
		/// <param name="value">The object to be compared. </param>
		// Token: 0x06000A57 RID: 2647 RVA: 0x0003C010 File Offset: 0x0003A210
		public override bool Equals(object value)
		{
			if (!(value is SqlInt16))
			{
				return false;
			}
			SqlInt16 sqlInt = (SqlInt16)value;
			if (sqlInt.IsNull || this.IsNull)
			{
				return sqlInt.IsNull && this.IsNull;
			}
			return (this == sqlInt).Value;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06000A58 RID: 2648 RVA: 0x0003C068 File Offset: 0x0003A268
		public override int GetHashCode()
		{
			if (!this.IsNull)
			{
				return this.Value.GetHashCode();
			}
			return 0;
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <returns>An XML schema consumed by .NET Framework.</returns>
		// Token: 0x06000A59 RID: 2649 RVA: 0x00011F10 File Offset: 0x00010110
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code. </summary>
		/// <param name="reader">XmlReader </param>
		// Token: 0x06000A5A RID: 2650 RVA: 0x0003C090 File Offset: 0x0003A290
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			string attribute = reader.GetAttribute("nil", "http://www.w3.org/2001/XMLSchema-instance");
			if (attribute != null && XmlConvert.ToBoolean(attribute))
			{
				reader.ReadElementString();
				this.m_fNotNull = false;
				return;
			}
			this.m_value = XmlConvert.ToInt16(reader.ReadElementString());
			this.m_fNotNull = true;
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <param name="writer">XmlWriter </param>
		// Token: 0x06000A5B RID: 2651 RVA: 0x0003C0E0 File Offset: 0x0003A2E0
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
		/// <returns>A <see cref="T:System.String" /> value that indicates the XSD of the specified <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</returns>
		/// <param name="schemaSet">An <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</param>
		// Token: 0x06000A5C RID: 2652 RVA: 0x0003C116 File Offset: 0x0003A316
		public static XmlQualifiedName GetXsdType(XmlSchemaSet schemaSet)
		{
			return new XmlQualifiedName("short", "http://www.w3.org/2001/XMLSchema");
		}

		// Token: 0x0400043B RID: 1083
		private bool m_fNotNull;

		// Token: 0x0400043C RID: 1084
		private short m_value;

		// Token: 0x0400043D RID: 1085
		private static readonly int s_MASKI2 = -32768;

		/// <summary>Represents a <see cref="T:System.DBNull" /> that can be assigned to this instance of the <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure.</summary>
		// Token: 0x0400043E RID: 1086
		public static readonly SqlInt16 Null = new SqlInt16(true);

		/// <summary>Represents a zero value that can be assigned to the <see cref="P:System.Data.SqlTypes.SqlInt16.Value" /> property of an instance of the <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure.</summary>
		// Token: 0x0400043F RID: 1087
		public static readonly SqlInt16 Zero = new SqlInt16(0);

		/// <summary>A constant representing the smallest possible value of a <see cref="T:System.Data.SqlTypes.SqlInt16" />.</summary>
		// Token: 0x04000440 RID: 1088
		public static readonly SqlInt16 MinValue = new SqlInt16(short.MinValue);

		/// <summary>A constant representing the largest possible value of a <see cref="T:System.Data.SqlTypes.SqlInt16" />.</summary>
		// Token: 0x04000441 RID: 1089
		public static readonly SqlInt16 MaxValue = new SqlInt16(short.MaxValue);
	}
}
