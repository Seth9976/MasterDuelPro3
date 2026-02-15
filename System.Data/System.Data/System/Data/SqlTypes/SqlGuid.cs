using System;
using System.Data.Common;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Data.SqlTypes
{
	/// <summary>Represents a GUID to be stored in or retrieved from a database.</summary>
	// Token: 0x020000C7 RID: 199
	[XmlSchemaProvider("GetXsdType")]
	[Serializable]
	public struct SqlGuid : INullable, IComparable, IXmlSerializable
	{
		// Token: 0x06000A2E RID: 2606 RVA: 0x0003B95D File Offset: 0x00039B5D
		private SqlGuid(bool fNull)
		{
			this.m_value = null;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlGuid" /> structure using the specified <see cref="T:System.Guid" /> parameter.</summary>
		/// <param name="g">A <see cref="T:System.Guid" /></param>
		// Token: 0x06000A2F RID: 2607 RVA: 0x0003B966 File Offset: 0x00039B66
		public SqlGuid(Guid g)
		{
			this.m_value = g.ToByteArray();
		}

		/// <summary>Gets a Boolean value that indicates whether this <see cref="T:System.Data.SqlTypes.SqlGuid" /> structure is null.</summary>
		/// <returns>true if null. Otherwise, false.</returns>
		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000A30 RID: 2608 RVA: 0x0003B975 File Offset: 0x00039B75
		public bool IsNull
		{
			get
			{
				return this.m_value == null;
			}
		}

		/// <summary>Gets the value of the <see cref="T:System.Data.SqlTypes.SqlGuid" /> structure. This property is read-only.</summary>
		/// <returns>A <see cref="T:System.Guid" /> structure.</returns>
		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06000A31 RID: 2609 RVA: 0x0003B980 File Offset: 0x00039B80
		public Guid Value
		{
			get
			{
				if (this.IsNull)
				{
					throw new SqlNullValueException();
				}
				return new Guid(this.m_value);
			}
		}

		/// <summary>Converts the supplied <see cref="T:System.Guid" /> parameter to <see cref="T:System.Data.SqlTypes.SqlGuid" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlGuid" /> whose <see cref="P:System.Data.SqlTypes.SqlGuid.Value" /> is equal to the <see cref="T:System.Guid" /> parameter.</returns>
		/// <param name="x">A <see cref="T:System.Guid" />. </param>
		// Token: 0x06000A32 RID: 2610 RVA: 0x0003B99B File Offset: 0x00039B9B
		public static implicit operator SqlGuid(Guid x)
		{
			return new SqlGuid(x);
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlGuid" /> structure to a <see cref="T:System.String" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the string representation of the <see cref="T:System.Data.SqlTypes.SqlGuid" /> structure.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06000A33 RID: 2611 RVA: 0x0003B9A4 File Offset: 0x00039BA4
		public override string ToString()
		{
			if (this.IsNull)
			{
				return SQLResource.NullString;
			}
			Guid guid = new Guid(this.m_value);
			return guid.ToString();
		}

		// Token: 0x06000A34 RID: 2612 RVA: 0x0003B9DC File Offset: 0x00039BDC
		private static EComparison Compare(SqlGuid x, SqlGuid y)
		{
			int i = 0;
			while (i < SqlGuid.s_sizeOfGuid)
			{
				byte b = x.m_value[SqlGuid.s_rgiGuidOrder[i]];
				byte b2 = y.m_value[SqlGuid.s_rgiGuidOrder[i]];
				if (b != b2)
				{
					if (b >= b2)
					{
						return EComparison.GT;
					}
					return EComparison.LT;
				}
				else
				{
					i++;
				}
			}
			return EComparison.EQ;
		}

		/// <summary>Performs a logical comparison of two <see cref="T:System.Data.SqlTypes.SqlGuid" /> structures to determine whether they are equal.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the two instances are equal or <see cref="F:System.Data.SqlTypes.SqlBoolean.False" /> if the two instances are not equal. If either instance of <see cref="T:System.Data.SqlTypes.SqlGuid" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlGuid" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlGuid" /> structure. </param>
		// Token: 0x06000A35 RID: 2613 RVA: 0x0003BA24 File Offset: 0x00039C24
		public static SqlBoolean operator ==(SqlGuid x, SqlGuid y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(SqlGuid.Compare(x, y) == EComparison.EQ);
			}
			return SqlBoolean.Null;
		}

		/// <summary>Compares two instances of <see cref="T:System.Data.SqlTypes.SqlGuid" /> to determine whether the first is less than the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is less than the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlGuid" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlGuid" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlGuid" /> structure. </param>
		// Token: 0x06000A36 RID: 2614 RVA: 0x0003BA4D File Offset: 0x00039C4D
		public static SqlBoolean operator <(SqlGuid x, SqlGuid y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(SqlGuid.Compare(x, y) == EComparison.LT);
			}
			return SqlBoolean.Null;
		}

		/// <summary>Compares two instances of <see cref="T:System.Data.SqlTypes.SqlGuid" /> to determine whether the first is greater than the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is greater than the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlGuid" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlGuid" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlGuid" /> structure. </param>
		// Token: 0x06000A37 RID: 2615 RVA: 0x0003BA76 File Offset: 0x00039C76
		public static SqlBoolean operator >(SqlGuid x, SqlGuid y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(SqlGuid.Compare(x, y) == EComparison.GT);
			}
			return SqlBoolean.Null;
		}

		/// <summary>Compares this <see cref="T:System.Data.SqlTypes.SqlGuid" /> structure to the supplied object and returns an indication of their relative values. Compares more than the last 6 bytes, but treats the last 6 bytes as the most significant ones in comparisons.</summary>
		/// <returns>A signed number that indicates the relative values of the instance and the object.Return Value Condition Less than zero This instance is less than object. Zero This instance is the same as object. Greater than zero This instance is greater than object -or- object is a null reference (Nothing) </returns>
		/// <param name="value">The <see cref="T:System.Object" /> to be compared. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000A38 RID: 2616 RVA: 0x0003BAA0 File Offset: 0x00039CA0
		public int CompareTo(object value)
		{
			if (value is SqlGuid)
			{
				SqlGuid sqlGuid = (SqlGuid)value;
				return this.CompareTo(sqlGuid);
			}
			throw ADP.WrongType(value.GetType(), typeof(SqlGuid));
		}

		/// <summary>Compares this <see cref="T:System.Data.SqlTypes.SqlGuid" /> structure to the supplied <see cref="T:System.Data.SqlTypes.SqlGuid" /> and returns an indication of their relative values. Compares more than the last 6 bytes, but treats the last 6 bytes as the most significant ones in comparisons.</summary>
		/// <returns>A signed number that indicates the relative values of the instance and the object.Return Value Condition Less than zero This instance is less than object. Zero This instance is the same as object. Greater than zero This instance is greater than object -or- object is a null reference (Nothing). </returns>
		/// <param name="value">The <see cref="T:System.Data.SqlTypes.SqlGuid" /> to be compared.</param>
		// Token: 0x06000A39 RID: 2617 RVA: 0x0003BADC File Offset: 0x00039CDC
		public int CompareTo(SqlGuid value)
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

		/// <summary>Compares the supplied object parameter to the <see cref="P:System.Data.SqlTypes.SqlGuid.Value" /> property of the <see cref="T:System.Data.SqlTypes.SqlGuid" /> object.</summary>
		/// <returns>true if object is an instance of <see cref="T:System.Data.SqlTypes.SqlGuid" /> and the two are equal; otherwise false.</returns>
		/// <param name="value">The object to be compared. </param>
		// Token: 0x06000A3A RID: 2618 RVA: 0x0003BB34 File Offset: 0x00039D34
		public override bool Equals(object value)
		{
			if (!(value is SqlGuid))
			{
				return false;
			}
			SqlGuid sqlGuid = (SqlGuid)value;
			if (sqlGuid.IsNull || this.IsNull)
			{
				return sqlGuid.IsNull && this.IsNull;
			}
			return (this == sqlGuid).Value;
		}

		/// <summary>Returns the hash code of this <see cref="T:System.Data.SqlTypes.SqlGuid" /> structure.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06000A3B RID: 2619 RVA: 0x0003BB8C File Offset: 0x00039D8C
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
		// Token: 0x06000A3C RID: 2620 RVA: 0x00011F10 File Offset: 0x00010110
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <param name="reader">XmlReader </param>
		// Token: 0x06000A3D RID: 2621 RVA: 0x0003BBB8 File Offset: 0x00039DB8
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			string attribute = reader.GetAttribute("nil", "http://www.w3.org/2001/XMLSchema-instance");
			if (attribute != null && XmlConvert.ToBoolean(attribute))
			{
				reader.ReadElementString();
				this.m_value = null;
				return;
			}
			this.m_value = new Guid(reader.ReadElementString()).ToByteArray();
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <param name="writer">XmlWriter </param>
		// Token: 0x06000A3E RID: 2622 RVA: 0x0003BC09 File Offset: 0x00039E09
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			if (this.IsNull)
			{
				writer.WriteAttributeString("xsi", "nil", "http://www.w3.org/2001/XMLSchema-instance", "true");
				return;
			}
			writer.WriteString(XmlConvert.ToString(new Guid(this.m_value)));
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the specified <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</summary>
		/// <returns>A string value that indicates the XSD of the specified <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</returns>
		/// <param name="schemaSet">A <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</param>
		// Token: 0x06000A3F RID: 2623 RVA: 0x00038ABB File Offset: 0x00036CBB
		public static XmlQualifiedName GetXsdType(XmlSchemaSet schemaSet)
		{
			return new XmlQualifiedName("string", "http://www.w3.org/2001/XMLSchema");
		}

		// Token: 0x04000437 RID: 1079
		private static readonly int s_sizeOfGuid = 16;

		// Token: 0x04000438 RID: 1080
		private static readonly int[] s_rgiGuidOrder = new int[]
		{
			10, 11, 12, 13, 14, 15, 8, 9, 6, 7,
			4, 5, 0, 1, 2, 3
		};

		// Token: 0x04000439 RID: 1081
		private byte[] m_value;

		/// <summary>Represents a <see cref="T:System.DBNull" />  that can be assigned to this instance of the <see cref="T:System.Data.SqlTypes.SqlGuid" /> structure.</summary>
		// Token: 0x0400043A RID: 1082
		public static readonly SqlGuid Null = new SqlGuid(true);
	}
}
