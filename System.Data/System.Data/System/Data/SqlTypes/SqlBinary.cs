using System;
using System.Data.Common;
using System.Globalization;
using System.Reflection;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Data.SqlTypes
{
	/// <summary>Represents a variable-length stream of binary data to be stored in or retrieved from a database. </summary>
	// Token: 0x020000BE RID: 190
	[DefaultMember("Item")]
	[XmlSchemaProvider("GetXsdType")]
	[Serializable]
	public struct SqlBinary : INullable, IComparable, IXmlSerializable
	{
		// Token: 0x06000940 RID: 2368 RVA: 0x0003784D File Offset: 0x00035A4D
		private SqlBinary(bool fNull)
		{
			this._value = null;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlBinary" /> structure, setting the <see cref="P:System.Data.SqlTypes.SqlBinary.Value" /> property to the contents of the supplied byte array.</summary>
		/// <param name="value">The byte array to be stored or retrieved. </param>
		// Token: 0x06000941 RID: 2369 RVA: 0x00037856 File Offset: 0x00035A56
		public SqlBinary(byte[] value)
		{
			if (value == null)
			{
				this._value = null;
				return;
			}
			this._value = new byte[value.Length];
			value.CopyTo(this._value, 0);
		}

		/// <summary>Indicates whether this <see cref="T:System.Data.SqlTypes.SqlBinary" /> structure is null. This property is read-only.</summary>
		/// <returns>true if null. Otherwise false.</returns>
		// Token: 0x17000174 RID: 372
		// (get) Token: 0x06000942 RID: 2370 RVA: 0x0003787E File Offset: 0x00035A7E
		public bool IsNull
		{
			get
			{
				return this._value == null;
			}
		}

		/// <summary>Gets the value of the <see cref="T:System.Data.SqlTypes.SqlBinary" /> structure. This property is read-only.</summary>
		/// <returns>The value of the <see cref="T:System.Data.SqlTypes.SqlBinary" /> structure.</returns>
		/// <exception cref="T:System.Data.SqlTypes.SqlNullValueException">The <see cref="P:System.Data.SqlTypes.SqlBinary.Value" /> property is read when the property contains <see cref="F:System.Data.SqlTypes.SqlBinary.Null" />. </exception>
		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000943 RID: 2371 RVA: 0x0003788C File Offset: 0x00035A8C
		public byte[] Value
		{
			get
			{
				if (this.IsNull)
				{
					throw new SqlNullValueException();
				}
				byte[] array = new byte[this._value.Length];
				this._value.CopyTo(array, 0);
				return array;
			}
		}

		/// <summary>Converts an array of bytes to a <see cref="T:System.Data.SqlTypes.SqlBinary" /> structure.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBinary" /> structure that represents the converted array of bytes.</returns>
		/// <param name="x">The array of bytes to be converted. </param>
		// Token: 0x06000944 RID: 2372 RVA: 0x000378C3 File Offset: 0x00035AC3
		public static implicit operator SqlBinary(byte[] x)
		{
			return new SqlBinary(x);
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlBinary" /> object to a string.</summary>
		/// <returns>A string that contains the <see cref="P:System.Data.SqlTypes.SqlBinary.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBinary" />. If the <see cref="P:System.Data.SqlTypes.SqlBinary.Value" /> is null the string will contain "null".</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06000945 RID: 2373 RVA: 0x000378CC File Offset: 0x00035ACC
		public override string ToString()
		{
			if (!this.IsNull)
			{
				return "SqlBinary(" + this._value.Length.ToString(CultureInfo.InvariantCulture) + ")";
			}
			return SQLResource.NullString;
		}

		// Token: 0x06000946 RID: 2374 RVA: 0x0003790C File Offset: 0x00035B0C
		private static EComparison PerformCompareByte(byte[] x, byte[] y)
		{
			int num = ((x.Length < y.Length) ? x.Length : y.Length);
			int i = 0;
			while (i < num)
			{
				if (x[i] != y[i])
				{
					if (x[i] < y[i])
					{
						return EComparison.LT;
					}
					return EComparison.GT;
				}
				else
				{
					i++;
				}
			}
			if (x.Length == y.Length)
			{
				return EComparison.EQ;
			}
			byte b = 0;
			if (x.Length < y.Length)
			{
				for (i = num; i < y.Length; i++)
				{
					if (y[i] != b)
					{
						return EComparison.LT;
					}
				}
			}
			else
			{
				for (i = num; i < x.Length; i++)
				{
					if (x[i] != b)
					{
						return EComparison.GT;
					}
				}
			}
			return EComparison.EQ;
		}

		/// <summary>Compares two <see cref="T:System.Data.SqlTypes.SqlBinary" /> structures to determine whether they are equal.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the two instances are equal or <see cref="F:System.Data.SqlTypes.SqlBoolean.False" /> if the two instances are not equal. If either instance of <see cref="T:System.Data.SqlTypes.SqlBinary" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlBinary" /> object. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlBinary" /> object. </param>
		// Token: 0x06000947 RID: 2375 RVA: 0x0003798D File Offset: 0x00035B8D
		public static SqlBoolean operator ==(SqlBinary x, SqlBinary y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlBoolean.Null;
			}
			return new SqlBoolean(SqlBinary.PerformCompareByte(x.Value, y.Value) == EComparison.EQ);
		}

		/// <summary>Compares two <see cref="T:System.Data.SqlTypes.SqlBinary" /> structures to determine whether the first is less than the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is less than the second instance. Otherwise <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlBinary" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlBinary" /> object. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlBinary" /> object. </param>
		// Token: 0x06000948 RID: 2376 RVA: 0x000379C2 File Offset: 0x00035BC2
		public static SqlBoolean operator <(SqlBinary x, SqlBinary y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlBoolean.Null;
			}
			return new SqlBoolean(SqlBinary.PerformCompareByte(x.Value, y.Value) == EComparison.LT);
		}

		/// <summary>Compares two <see cref="T:System.Data.SqlTypes.SqlBinary" /> structures to determine whether the first is greater than the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is greater than the second instance. Otherwise <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlBinary" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlBinary" /> object. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlBinary" /> object. </param>
		// Token: 0x06000949 RID: 2377 RVA: 0x000379F7 File Offset: 0x00035BF7
		public static SqlBoolean operator >(SqlBinary x, SqlBinary y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlBoolean.Null;
			}
			return new SqlBoolean(SqlBinary.PerformCompareByte(x.Value, y.Value) == EComparison.GT);
		}

		/// <summary>Compares this <see cref="T:System.Data.SqlTypes.SqlBinary" /> object to the supplied object and returns an indication of their relative values.</summary>
		/// <returns>A signed number that indicates the relative values of this <see cref="T:System.Data.SqlTypes.SqlBinary" /> structure and the object.Return value Condition Less than zero The value of this <see cref="T:System.Data.SqlTypes.SqlBinary" /> object is less than the object. Zero This <see cref="T:System.Data.SqlTypes.SqlBinary" /> object is the same as object. Greater than zero This <see cref="T:System.Data.SqlTypes.SqlBinary" /> object is greater than object.-or- The object is a null reference. </returns>
		/// <param name="value">The object to be compared to this <see cref="T:System.Data.SqlTypes.SqlBinary" /> structure. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600094A RID: 2378 RVA: 0x00037A2C File Offset: 0x00035C2C
		public int CompareTo(object value)
		{
			if (value is SqlBinary)
			{
				SqlBinary sqlBinary = (SqlBinary)value;
				return this.CompareTo(sqlBinary);
			}
			throw ADP.WrongType(value.GetType(), typeof(SqlBinary));
		}

		/// <summary>Compares this <see cref="T:System.Data.SqlTypes.SqlBinary" /> object to the supplied <see cref="T:System.Data.SqlTypes.SqlBinary" /> object and returns an indication of their relative values.</summary>
		/// <returns>A signed number that indicates the relative values of this <see cref="T:System.Data.SqlTypes.SqlBinary" /> structure and the object.Return value Condition Less than zero The value of this <see cref="T:System.Data.SqlTypes.SqlBinary" /> object is less than the object. Zero This <see cref="T:System.Data.SqlTypes.SqlBinary" /> object is the same as object. Greater than zero This <see cref="T:System.Data.SqlTypes.SqlBinary" /> object is greater than object.-or- The object is a null reference. </returns>
		/// <param name="value">The <see cref="T:System.Data.SqlTypes.SqlBinary" /> object to be compared to this <see cref="T:System.Data.SqlTypes.SqlBinary" /> structure. </param>
		// Token: 0x0600094B RID: 2379 RVA: 0x00037A68 File Offset: 0x00035C68
		public int CompareTo(SqlBinary value)
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

		/// <summary>Compares the supplied object parameter to the <see cref="P:System.Data.SqlTypes.SqlBinary.Value" /> property of the <see cref="T:System.Data.SqlTypes.SqlBinary" /> object.</summary>
		/// <returns>true if object is an instance of <see cref="T:System.Data.SqlTypes.SqlBinary" /> and the two are equal; otherwise false.</returns>
		/// <param name="value">The object to be compared. </param>
		// Token: 0x0600094C RID: 2380 RVA: 0x00037AC0 File Offset: 0x00035CC0
		public override bool Equals(object value)
		{
			if (!(value is SqlBinary))
			{
				return false;
			}
			SqlBinary sqlBinary = (SqlBinary)value;
			if (sqlBinary.IsNull || this.IsNull)
			{
				return sqlBinary.IsNull && this.IsNull;
			}
			return (this == sqlBinary).Value;
		}

		// Token: 0x0600094D RID: 2381 RVA: 0x00037B18 File Offset: 0x00035D18
		internal static int HashByteArray(byte[] rgbValue, int length)
		{
			if (length <= 0)
			{
				return 0;
			}
			int num = 0;
			for (int i = 0; i < length; i++)
			{
				int num2 = (num >> 28) & 255;
				num <<= 4;
				num = num ^ (int)rgbValue[i] ^ num2;
			}
			return num;
		}

		/// <summary>Returns the hash code for this <see cref="T:System.Data.SqlTypes.SqlBinary" /> structure.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x0600094E RID: 2382 RVA: 0x00037B54 File Offset: 0x00035D54
		public override int GetHashCode()
		{
			if (this.IsNull)
			{
				return 0;
			}
			int num = this._value.Length;
			while (num > 0 && this._value[num - 1] == 0)
			{
				num--;
			}
			return SqlBinary.HashByteArray(this._value, num);
		}

		/// <summary>For a description of this member, see <see cref="M:System.Xml.Serialization.IXmlSerializable.GetSchema" />.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.XMLSchema" /> instance.</returns>
		// Token: 0x0600094F RID: 2383 RVA: 0x00011F10 File Offset: 0x00010110
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		/// <summary>For a description of this member, see <see cref="M:System.Xml.Serialization.IXmlSerializable.ReadXml(System.Xml.XmlReader)" />.</summary>
		/// <param name="reader">A <see cref="T:System.Xml.XmlReader" />.</param>
		// Token: 0x06000950 RID: 2384 RVA: 0x00037B98 File Offset: 0x00035D98
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			string attribute = reader.GetAttribute("nil", "http://www.w3.org/2001/XMLSchema-instance");
			if (attribute != null && XmlConvert.ToBoolean(attribute))
			{
				reader.ReadElementString();
				this._value = null;
				return;
			}
			string text = reader.ReadElementString();
			if (text == null)
			{
				this._value = Array.Empty<byte>();
				return;
			}
			text = text.Trim();
			if (text.Length == 0)
			{
				this._value = Array.Empty<byte>();
				return;
			}
			this._value = Convert.FromBase64String(text);
		}

		/// <summary>For a description of this member, see <see cref="M:System.Xml.Serialization.IXmlSerializable.WriteXml(System.Xml.XmlWriter)" />.</summary>
		/// <param name="writer">A <see cref="T:System.Xml.XmlWriter" />.</param>
		// Token: 0x06000951 RID: 2385 RVA: 0x00037C0D File Offset: 0x00035E0D
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			if (this.IsNull)
			{
				writer.WriteAttributeString("xsi", "nil", "http://www.w3.org/2001/XMLSchema-instance", "true");
				return;
			}
			writer.WriteString(Convert.ToBase64String(this._value));
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the specified <see cref="T:System.Xml.Schema.XmlSchemaSet" />. </summary>
		/// <returns>A string that indicates the XSD of the specified <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</returns>
		/// <param name="schemaSet">An <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</param>
		// Token: 0x06000952 RID: 2386 RVA: 0x00037C43 File Offset: 0x00035E43
		public static XmlQualifiedName GetXsdType(XmlSchemaSet schemaSet)
		{
			return new XmlQualifiedName("base64Binary", "http://www.w3.org/2001/XMLSchema");
		}

		// Token: 0x040003BB RID: 955
		private byte[] _value;

		/// <summary>Represents a <see cref="T:System.DBNull" /> that can be assigned to this instance of the <see cref="T:System.Data.SqlTypes.SqlBinary" /> structure.</summary>
		// Token: 0x040003BC RID: 956
		public static readonly SqlBinary Null = new SqlBinary(true);
	}
}
