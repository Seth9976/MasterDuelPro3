using System;
using System.Data.Common;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Data.SqlTypes
{
	/// <summary>Represents an integer value that is either 1 or 0 to be stored in or retrieved from a database.</summary>
	// Token: 0x020000BF RID: 191
	[XmlSchemaProvider("GetXsdType")]
	[Serializable]
	public struct SqlBoolean : INullable, IComparable, IXmlSerializable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure using the supplied Boolean value.</summary>
		/// <param name="value">The value for the new <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure; either true or false. </param>
		// Token: 0x06000954 RID: 2388 RVA: 0x00037C61 File Offset: 0x00035E61
		public SqlBoolean(bool value)
		{
			this.m_value = (value ? 2 : 1);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure using the specified integer value.</summary>
		/// <param name="value">The integer whose value is to be used for the new <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure. </param>
		// Token: 0x06000955 RID: 2389 RVA: 0x00037C70 File Offset: 0x00035E70
		public SqlBoolean(int value)
		{
			this = new SqlBoolean(value, false);
		}

		// Token: 0x06000956 RID: 2390 RVA: 0x00037C7A File Offset: 0x00035E7A
		private SqlBoolean(int value, bool fNull)
		{
			if (fNull)
			{
				this.m_value = 0;
				return;
			}
			this.m_value = ((value != 0) ? 2 : 1);
		}

		/// <summary>Indicates whether this <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure is null.</summary>
		/// <returns>true if the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure is null; otherwise false.</returns>
		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000957 RID: 2391 RVA: 0x00037C94 File Offset: 0x00035E94
		public bool IsNull
		{
			get
			{
				return this.m_value == 0;
			}
		}

		/// <summary>Gets the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure's value. This property is read-only.</summary>
		/// <returns>true if the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" />; otherwise false.</returns>
		/// <exception cref="T:System.Data.SqlTypes.SqlNullValueException">The property is set to null. </exception>
		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06000958 RID: 2392 RVA: 0x00037CA0 File Offset: 0x00035EA0
		public bool Value
		{
			get
			{
				byte value = this.m_value;
				if (value == 1)
				{
					return false;
				}
				if (value == 2)
				{
					return true;
				}
				throw new SqlNullValueException();
			}
		}

		/// <summary>Indicates whether the current <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" />.</summary>
		/// <returns>true if Value is True; otherwise, false.</returns>
		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06000959 RID: 2393 RVA: 0x00037CC5 File Offset: 0x00035EC5
		public bool IsTrue
		{
			get
			{
				return this.m_value == 2;
			}
		}

		/// <summary>Indicates whether the current <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> is <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />.</summary>
		/// <returns>true if Value is False; otherwise, false.</returns>
		// Token: 0x17000179 RID: 377
		// (get) Token: 0x0600095A RID: 2394 RVA: 0x00037CD0 File Offset: 0x00035ED0
		public bool IsFalse
		{
			get
			{
				return this.m_value == 1;
			}
		}

		/// <summary>Converts the supplied byte value to a <see cref="T:System.Data.SqlTypes.SqlBoolean" />.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> value that contains 0 or 1.</returns>
		/// <param name="x">A byte value to be converted to <see cref="T:System.Data.SqlTypes.SqlBoolean" />. </param>
		// Token: 0x0600095B RID: 2395 RVA: 0x00037CDB File Offset: 0x00035EDB
		public static implicit operator SqlBoolean(bool x)
		{
			return new SqlBoolean(x);
		}

		/// <summary>The true operator can be used to test the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> to determine whether it is true.</summary>
		/// <returns>Returns true if the supplied parameter is <see cref="T:System.Data.SqlTypes.SqlBoolean" /> is true, false otherwise.</returns>
		/// <param name="x">The <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure to be tested. </param>
		// Token: 0x0600095C RID: 2396 RVA: 0x00037CE3 File Offset: 0x00035EE3
		public static bool operator true(SqlBoolean x)
		{
			return x.IsTrue;
		}

		/// <summary>Computes the bitwise AND operation of two specified <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structures.</summary>
		/// <returns>The result of the logical AND operation.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure. </param>
		// Token: 0x0600095D RID: 2397 RVA: 0x00037CEC File Offset: 0x00035EEC
		public static SqlBoolean operator &(SqlBoolean x, SqlBoolean y)
		{
			if (x.m_value == 1 || y.m_value == 1)
			{
				return SqlBoolean.False;
			}
			if (x.m_value == 2 && y.m_value == 2)
			{
				return SqlBoolean.True;
			}
			return SqlBoolean.Null;
		}

		/// <summary>Computes the bitwise OR of its operands.</summary>
		/// <returns>The results of the logical OR operation.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure. </param>
		// Token: 0x0600095E RID: 2398 RVA: 0x00037D23 File Offset: 0x00035F23
		public static SqlBoolean operator |(SqlBoolean x, SqlBoolean y)
		{
			if (x.m_value == 2 || y.m_value == 2)
			{
				return SqlBoolean.True;
			}
			if (x.m_value == 1 && y.m_value == 1)
			{
				return SqlBoolean.False;
			}
			return SqlBoolean.Null;
		}

		/// <summary>Gets the value of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure as a byte.</summary>
		/// <returns>A byte representing the value of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure.</returns>
		// Token: 0x1700017A RID: 378
		// (get) Token: 0x0600095F RID: 2399 RVA: 0x00037D5A File Offset: 0x00035F5A
		public byte ByteValue
		{
			get
			{
				if (this.IsNull)
				{
					throw new SqlNullValueException();
				}
				if (this.m_value != 2)
				{
					return 0;
				}
				return 1;
			}
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure to a string.</summary>
		/// <returns>A string that contains the value of the <see cref="T:System.Data.SqlTypes.SqlBoolean" />. If the value is null, the string will contain "null".</returns>
		// Token: 0x06000960 RID: 2400 RVA: 0x00037D78 File Offset: 0x00035F78
		public override string ToString()
		{
			if (!this.IsNull)
			{
				return this.Value.ToString();
			}
			return SQLResource.NullString;
		}

		/// <summary>Compares two instances of <see cref="T:System.Data.SqlTypes.SqlBoolean" /> for equality.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the two instances are equal or <see cref="F:System.Data.SqlTypes.SqlBoolean.False" /> if the two instances are not equal. If either instance of <see cref="T:System.Data.SqlTypes.SqlBoolean" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlBoolean" />. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlBoolean" />. </param>
		// Token: 0x06000961 RID: 2401 RVA: 0x00037DA1 File Offset: 0x00035FA1
		public static SqlBoolean operator ==(SqlBoolean x, SqlBoolean y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value == y.m_value);
			}
			return SqlBoolean.Null;
		}

		/// <summary>Computes the bitwise AND operation of two specified <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structures.</summary>
		/// <returns>The result of the logical AND operation.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure. </param>
		// Token: 0x06000962 RID: 2402 RVA: 0x00037DCE File Offset: 0x00035FCE
		public static SqlBoolean And(SqlBoolean x, SqlBoolean y)
		{
			return x & y;
		}

		/// <summary>Performs a bitwise OR operation on the two specified <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structures.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure whose Value is the result of the bitwise OR operation.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure. </param>
		// Token: 0x06000963 RID: 2403 RVA: 0x00037DD7 File Offset: 0x00035FD7
		public static SqlBoolean Or(SqlBoolean x, SqlBoolean y)
		{
			return x | y;
		}

		/// <summary>Compares this <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure to a specified object and returns an indication of their relative values.</summary>
		/// <returns>A signed number that indicates the relative values of the instance and value.Value Description A negative integer This instance is less than <paramref name="value" />. Zero This instance is equal to <paramref name="value" />. A positive integer This instance is greater than <paramref name="value" />.-or- <paramref name="value" /> is a null reference (Nothing in Visual Basic). </returns>
		/// <param name="value">An object to compare, or a null reference (Nothing in Visual Basic). </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000964 RID: 2404 RVA: 0x00037DE0 File Offset: 0x00035FE0
		public int CompareTo(object value)
		{
			if (value is SqlBoolean)
			{
				SqlBoolean sqlBoolean = (SqlBoolean)value;
				return this.CompareTo(sqlBoolean);
			}
			throw ADP.WrongType(value.GetType(), typeof(SqlBoolean));
		}

		/// <summary>Compares this <see cref="T:System.Data.SqlTypes.SqlBoolean" /> object to the supplied <see cref="T:System.Data.SqlTypes.SqlBoolean" /> object and returns an indication of their relative values.</summary>
		/// <returns>A signed number that indicates the relative values of the instance and value.Value Description A negative integer This instance is less than <paramref name="value" />. Zero This instance is equal to <paramref name="value" />. A positive integer This instance is greater than <paramref name="value" />.-or- <paramref name="value" /> is a null reference (Nothing in Visual Basic). </returns>
		/// <param name="value">A <see cref="T:System.Data.SqlTypes.SqlBoolean" /><see cref="T:System.Data.SqlTypes.SqlBoolean" /> object to compare, or a null reference (Nothing in Visual Basic).  </param>
		// Token: 0x06000965 RID: 2405 RVA: 0x00037E1C File Offset: 0x0003601C
		public int CompareTo(SqlBoolean value)
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
				if (this.ByteValue < value.ByteValue)
				{
					return -1;
				}
				if (this.ByteValue > value.ByteValue)
				{
					return 1;
				}
				return 0;
			}
		}

		/// <summary>Compares the supplied object parameter to the <see cref="T:System.Data.SqlTypes.SqlBoolean" />.</summary>
		/// <returns>true if object is an instance of <see cref="T:System.Data.SqlTypes.SqlBoolean" /> and the two are equal; otherwise, false.</returns>
		/// <param name="value">The object to be compared. </param>
		// Token: 0x06000966 RID: 2406 RVA: 0x00037E6C File Offset: 0x0003606C
		public override bool Equals(object value)
		{
			if (!(value is SqlBoolean))
			{
				return false;
			}
			SqlBoolean sqlBoolean = (SqlBoolean)value;
			if (sqlBoolean.IsNull || this.IsNull)
			{
				return sqlBoolean.IsNull && this.IsNull;
			}
			return (this == sqlBoolean).Value;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06000967 RID: 2407 RVA: 0x00037EC4 File Offset: 0x000360C4
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
		// Token: 0x06000968 RID: 2408 RVA: 0x00011F10 File Offset: 0x00010110
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <param name="reader">XmlReader </param>
		// Token: 0x06000969 RID: 2409 RVA: 0x00037EEC File Offset: 0x000360EC
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			string attribute = reader.GetAttribute("nil", "http://www.w3.org/2001/XMLSchema-instance");
			if (attribute != null && XmlConvert.ToBoolean(attribute))
			{
				reader.ReadElementString();
				this.m_value = 0;
				return;
			}
			this.m_value = (XmlConvert.ToBoolean(reader.ReadElementString()) ? 2 : 1);
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <param name="writer">XmlWriter </param>
		// Token: 0x0600096A RID: 2410 RVA: 0x00037F3B File Offset: 0x0003613B
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			if (this.IsNull)
			{
				writer.WriteAttributeString("xsi", "nil", "http://www.w3.org/2001/XMLSchema-instance", "true");
				return;
			}
			writer.WriteString((this.m_value == 2) ? "true" : "false");
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the specified <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</summary>
		/// <returns>A string value that indicates the XSD of the specified <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</returns>
		/// <param name="schemaSet">A <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</param>
		// Token: 0x0600096B RID: 2411 RVA: 0x00037F7B File Offset: 0x0003617B
		public static XmlQualifiedName GetXsdType(XmlSchemaSet schemaSet)
		{
			return new XmlQualifiedName("boolean", "http://www.w3.org/2001/XMLSchema");
		}

		// Token: 0x040003BD RID: 957
		private byte m_value;

		/// <summary>Represents a true value that can be assigned to the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> property of an instance of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure.</summary>
		// Token: 0x040003BE RID: 958
		public static readonly SqlBoolean True = new SqlBoolean(true);

		/// <summary>Represents a false value that can be assigned to the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> property of an instance of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure.</summary>
		// Token: 0x040003BF RID: 959
		public static readonly SqlBoolean False = new SqlBoolean(false);

		/// <summary>Represents <see cref="T:System.DBNull" /> that can be assigned to this instance of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure.</summary>
		// Token: 0x040003C0 RID: 960
		public static readonly SqlBoolean Null = new SqlBoolean(0, true);

		/// <summary>Represents a zero value that can be assigned to the <see cref="P:System.Data.SqlTypes.SqlBoolean.ByteValue" /> property of an instance of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure.</summary>
		// Token: 0x040003C1 RID: 961
		public static readonly SqlBoolean Zero = new SqlBoolean(0);

		/// <summary>Represents a one value that can be assigned to the <see cref="P:System.Data.SqlTypes.SqlBoolean.ByteValue" /> property of an instance of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure.</summary>
		// Token: 0x040003C2 RID: 962
		public static readonly SqlBoolean One = new SqlBoolean(1);
	}
}
