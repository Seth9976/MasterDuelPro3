using System;
using System.Data.Common;
using System.Globalization;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Data.SqlTypes
{
	/// <summary>Represents a variable-length stream of characters to be stored in or retrieved from the database. <see cref="T:System.Data.SqlTypes.SqlString" /> has a different underlying data structure from its corresponding .NET Framework <see cref="T:System.String" /> data type.</summary>
	// Token: 0x020000CE RID: 206
	[XmlSchemaProvider("GetXsdType")]
	[Serializable]
	public struct SqlString : INullable, IComparable, IXmlSerializable
	{
		// Token: 0x06000AE9 RID: 2793 RVA: 0x0003D9BD File Offset: 0x0003BBBD
		private SqlString(bool fNull)
		{
			this.m_value = null;
			this.m_cmpInfo = null;
			this.m_lcid = 0;
			this.m_flag = SqlCompareOptions.None;
			this.m_fNotNull = false;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlString" /> structure using the specified string, locale id, and compare option values.</summary>
		/// <param name="data">The string to store. </param>
		/// <param name="lcid">Specifies the geographical locale and language for the new <see cref="T:System.Data.SqlTypes.SqlString" /> structure. </param>
		/// <param name="compareOptions">Specifies the compare options for the new <see cref="T:System.Data.SqlTypes.SqlString" /> structure. </param>
		// Token: 0x06000AEA RID: 2794 RVA: 0x0003D9E2 File Offset: 0x0003BBE2
		public SqlString(string data, int lcid, SqlCompareOptions compareOptions)
		{
			this.m_lcid = lcid;
			SqlString.ValidateSqlCompareOptions(compareOptions);
			this.m_flag = compareOptions;
			this.m_cmpInfo = null;
			if (data == null)
			{
				this.m_fNotNull = false;
				this.m_value = null;
				return;
			}
			this.m_fNotNull = true;
			this.m_value = data;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlString" /> structure using the specified string.</summary>
		/// <param name="data">The string to store. </param>
		// Token: 0x06000AEB RID: 2795 RVA: 0x0003DA1F File Offset: 0x0003BC1F
		public SqlString(string data)
		{
			this = new SqlString(data, CultureInfo.CurrentCulture.LCID, SqlString.s_iDefaultFlag);
		}

		// Token: 0x06000AEC RID: 2796 RVA: 0x0003DA38 File Offset: 0x0003BC38
		private SqlString(int lcid, SqlCompareOptions compareOptions, string data, CompareInfo cmpInfo)
		{
			this.m_lcid = lcid;
			SqlString.ValidateSqlCompareOptions(compareOptions);
			this.m_flag = compareOptions;
			if (data == null)
			{
				this.m_fNotNull = false;
				this.m_value = null;
				this.m_cmpInfo = null;
				return;
			}
			this.m_value = data;
			this.m_cmpInfo = cmpInfo;
			this.m_fNotNull = true;
		}

		/// <summary>Indicates whether this <see cref="T:System.Data.SqlTypes.SqlString" /> structure is null.</summary>
		/// <returns>true if <see cref="P:System.Data.SqlTypes.SqlString.Value" /> is <see cref="F:System.Data.SqlTypes.SqlString.Null" />. Otherwise, false.</returns>
		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06000AED RID: 2797 RVA: 0x0003DA88 File Offset: 0x0003BC88
		public bool IsNull
		{
			get
			{
				return !this.m_fNotNull;
			}
		}

		/// <summary>Gets the string that is stored in this <see cref="T:System.Data.SqlTypes.SqlString" /> structure. This property is read-only.</summary>
		/// <returns>The string that is stored.</returns>
		/// <exception cref="T:System.Data.SqlTypes.SqlNullValueException">The value of the string is <see cref="F:System.Data.SqlTypes.SqlString.Null" />. </exception>
		// Token: 0x1700019F RID: 415
		// (get) Token: 0x06000AEE RID: 2798 RVA: 0x0003DA93 File Offset: 0x0003BC93
		public string Value
		{
			get
			{
				if (!this.IsNull)
				{
					return this.m_value;
				}
				throw new SqlNullValueException();
			}
		}

		// Token: 0x06000AEF RID: 2799 RVA: 0x0003DAA9 File Offset: 0x0003BCA9
		private void SetCompareInfo()
		{
			if (this.m_cmpInfo == null)
			{
				this.m_cmpInfo = CultureInfo.GetCultureInfo(this.m_lcid).CompareInfo;
			}
		}

		/// <summary>Converts the <see cref="T:System.String" /> parameter to a <see cref="T:System.Data.SqlTypes.SqlString" />.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlString" /> that contains the value of the specified String.</returns>
		/// <param name="x">The <see cref="T:System.String" /> to be converted. </param>
		// Token: 0x06000AF0 RID: 2800 RVA: 0x0003DAC9 File Offset: 0x0003BCC9
		public static implicit operator SqlString(string x)
		{
			return new SqlString(x);
		}

		/// <summary>Converts a <see cref="T:System.Data.SqlTypes.SqlString" /> object to a <see cref="T:System.String" />.</summary>
		/// <returns>A <see cref="T:System.String" /> with the same value as this <see cref="T:System.Data.SqlTypes.SqlString" /> structure.</returns>
		// Token: 0x06000AF1 RID: 2801 RVA: 0x0003DAD1 File Offset: 0x0003BCD1
		public override string ToString()
		{
			if (!this.IsNull)
			{
				return this.m_value;
			}
			return SQLResource.NullString;
		}

		/// <summary>Concatenates the two specified <see cref="T:System.Data.SqlTypes.SqlString" /> structures.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlString" /> that contains the newly concatenated value representing the contents of the two <see cref="T:System.Data.SqlTypes.SqlString" /> parameters.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlString" />. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlString" />. </param>
		// Token: 0x06000AF2 RID: 2802 RVA: 0x0003DAE8 File Offset: 0x0003BCE8
		public static SqlString operator +(SqlString x, SqlString y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlString.Null;
			}
			if (x.m_lcid != y.m_lcid || x.m_flag != y.m_flag)
			{
				throw new SqlTypeException(SQLResource.ConcatDiffCollationMessage);
			}
			return new SqlString(x.m_lcid, x.m_flag, x.m_value + y.m_value, (x.m_cmpInfo == null) ? y.m_cmpInfo : x.m_cmpInfo);
		}

		// Token: 0x06000AF3 RID: 2803 RVA: 0x0003DB6C File Offset: 0x0003BD6C
		private static int StringCompare(SqlString x, SqlString y)
		{
			if (x.m_lcid != y.m_lcid || x.m_flag != y.m_flag)
			{
				throw new SqlTypeException(SQLResource.CompareDiffCollationMessage);
			}
			x.SetCompareInfo();
			y.SetCompareInfo();
			int num;
			if ((x.m_flag & SqlCompareOptions.BinarySort) != SqlCompareOptions.None)
			{
				num = SqlString.CompareBinary(x, y);
			}
			else if ((x.m_flag & SqlCompareOptions.BinarySort2) != SqlCompareOptions.None)
			{
				num = SqlString.CompareBinary2(x, y);
			}
			else
			{
				string value = x.m_value;
				string value2 = y.m_value;
				int i = value.Length;
				int num2 = value2.Length;
				while (i > 0)
				{
					if (value[i - 1] != ' ')
					{
						break;
					}
					i--;
				}
				while (num2 > 0 && value2[num2 - 1] == ' ')
				{
					num2--;
				}
				CompareOptions compareOptions = SqlString.CompareOptionsFromSqlCompareOptions(x.m_flag);
				num = x.m_cmpInfo.Compare(x.m_value, 0, i, y.m_value, 0, num2, compareOptions);
			}
			return num;
		}

		// Token: 0x06000AF4 RID: 2804 RVA: 0x0003DC60 File Offset: 0x0003BE60
		private static SqlBoolean Compare(SqlString x, SqlString y, EComparison ecExpectedResult)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlBoolean.Null;
			}
			int num = SqlString.StringCompare(x, y);
			bool flag;
			switch (ecExpectedResult)
			{
			case EComparison.LT:
				flag = num < 0;
				break;
			case EComparison.LE:
				flag = num <= 0;
				break;
			case EComparison.EQ:
				flag = num == 0;
				break;
			case EComparison.GE:
				flag = num >= 0;
				break;
			case EComparison.GT:
				flag = num > 0;
				break;
			default:
				return SqlBoolean.Null;
			}
			return new SqlBoolean(flag);
		}

		/// <summary>Performs a logical comparison of the two <see cref="T:System.Data.SqlTypes.SqlString" /> operands to determine whether they are equal.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the two instances are equal or <see cref="F:System.Data.SqlTypes.SqlBoolean.False" /> if the two instances are not equal. If either instance of <see cref="T:System.Data.SqlTypes.SqlString" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlString" />. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlString" />. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000AF5 RID: 2805 RVA: 0x0003DCE0 File Offset: 0x0003BEE0
		public static SqlBoolean operator ==(SqlString x, SqlString y)
		{
			return SqlString.Compare(x, y, EComparison.EQ);
		}

		// Token: 0x06000AF6 RID: 2806 RVA: 0x0003DCEA File Offset: 0x0003BEEA
		private static void ValidateSqlCompareOptions(SqlCompareOptions compareOptions)
		{
			if ((compareOptions & SqlString.s_iValidSqlCompareOptionMask) != compareOptions)
			{
				throw new ArgumentOutOfRangeException("compareOptions");
			}
		}

		/// <summary>Gets the <see cref="T:System.Globalization.CompareOptions" /> enumeration equilvalent of the specified <see cref="T:System.Data.SqlTypes.SqlCompareOptions" /> value.</summary>
		/// <returns>A CompareOptions value that corresponds to the SqlCompareOptions for this <see cref="T:System.Data.SqlTypes.SqlString" /> structure.</returns>
		/// <param name="compareOptions">A <see cref="T:System.Data.SqlTypes.SqlCompareOptions" /> value that describes the comparison options for this <see cref="T:System.Data.SqlTypes.SqlString" /> structure. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000AF7 RID: 2807 RVA: 0x0003DD04 File Offset: 0x0003BF04
		public static CompareOptions CompareOptionsFromSqlCompareOptions(SqlCompareOptions compareOptions)
		{
			CompareOptions compareOptions2 = CompareOptions.None;
			SqlString.ValidateSqlCompareOptions(compareOptions);
			if ((compareOptions & (SqlCompareOptions.BinarySort | SqlCompareOptions.BinarySort2)) != SqlCompareOptions.None)
			{
				throw ADP.ArgumentOutOfRange("compareOptions");
			}
			if ((compareOptions & SqlCompareOptions.IgnoreCase) != SqlCompareOptions.None)
			{
				compareOptions2 |= CompareOptions.IgnoreCase;
			}
			if ((compareOptions & SqlCompareOptions.IgnoreNonSpace) != SqlCompareOptions.None)
			{
				compareOptions2 |= CompareOptions.IgnoreNonSpace;
			}
			if ((compareOptions & SqlCompareOptions.IgnoreKanaType) != SqlCompareOptions.None)
			{
				compareOptions2 |= CompareOptions.IgnoreKanaType;
			}
			if ((compareOptions & SqlCompareOptions.IgnoreWidth) != SqlCompareOptions.None)
			{
				compareOptions2 |= CompareOptions.IgnoreWidth;
			}
			return compareOptions2;
		}

		// Token: 0x06000AF8 RID: 2808 RVA: 0x0003DD54 File Offset: 0x0003BF54
		private bool FBinarySort()
		{
			return !this.IsNull && (this.m_flag & (SqlCompareOptions.BinarySort | SqlCompareOptions.BinarySort2)) > SqlCompareOptions.None;
		}

		// Token: 0x06000AF9 RID: 2809 RVA: 0x0003DD70 File Offset: 0x0003BF70
		private static int CompareBinary(SqlString x, SqlString y)
		{
			byte[] bytes = SqlString.s_unicodeEncoding.GetBytes(x.m_value);
			byte[] bytes2 = SqlString.s_unicodeEncoding.GetBytes(y.m_value);
			int num = bytes.Length;
			int num2 = bytes2.Length;
			int num3 = ((num < num2) ? num : num2);
			int i;
			for (i = 0; i < num3; i++)
			{
				if (bytes[i] < bytes2[i])
				{
					return -1;
				}
				if (bytes[i] > bytes2[i])
				{
					return 1;
				}
			}
			i = num3;
			int num4 = 32;
			if (num < num2)
			{
				while (i < num2)
				{
					int num5 = (int)bytes2[i + 1] << (int)(8 + bytes2[i]);
					if (num5 != num4)
					{
						if (num4 <= num5)
						{
							return -1;
						}
						return 1;
					}
					else
					{
						i += 2;
					}
				}
			}
			else
			{
				while (i < num)
				{
					int num5 = (int)bytes[i + 1] << (int)(8 + bytes[i]);
					if (num5 != num4)
					{
						if (num5 <= num4)
						{
							return -1;
						}
						return 1;
					}
					else
					{
						i += 2;
					}
				}
			}
			return 0;
		}

		// Token: 0x06000AFA RID: 2810 RVA: 0x0003DE48 File Offset: 0x0003C048
		private static int CompareBinary2(SqlString x, SqlString y)
		{
			string value = x.m_value;
			string value2 = y.m_value;
			int length = value.Length;
			int length2 = value2.Length;
			int num = ((length < length2) ? length : length2);
			for (int i = 0; i < num; i++)
			{
				if (value[i] < value2[i])
				{
					return -1;
				}
				if (value[i] > value2[i])
				{
					return 1;
				}
			}
			char c = ' ';
			if (length < length2)
			{
				int i = num;
				while (i < length2)
				{
					if (value2[i] != c)
					{
						if (c <= value2[i])
						{
							return -1;
						}
						return 1;
					}
					else
					{
						i++;
					}
				}
			}
			else
			{
				int i = num;
				while (i < length)
				{
					if (value[i] != c)
					{
						if (value[i] <= c)
						{
							return -1;
						}
						return 1;
					}
					else
					{
						i++;
					}
				}
			}
			return 0;
		}

		/// <summary>Compares this <see cref="T:System.Data.SqlTypes.SqlString" /> object to the supplied <see cref="T:System.Object" /> and returns an indication of their relative values.</summary>
		/// <returns>A signed number that indicates the relative values of the instance and the object.Return Value Condition Less than zero This instance is less than the object. Zero This instance is the same as the object. Greater than zero This instance is greater than the object -or- The object is a null reference (Nothing in Visual Basic) </returns>
		/// <param name="value">The <see cref="T:System.Object" /> to be compared. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000AFB RID: 2811 RVA: 0x0003DF1C File Offset: 0x0003C11C
		public int CompareTo(object value)
		{
			if (value is SqlString)
			{
				SqlString sqlString = (SqlString)value;
				return this.CompareTo(sqlString);
			}
			throw ADP.WrongType(value.GetType(), typeof(SqlString));
		}

		/// <summary>Compares this <see cref="T:System.Data.SqlTypes.SqlString" /> instance to the supplied <see cref="T:System.Data.SqlTypes.SqlString" /> and returns an indication of their relative values.</summary>
		/// <returns>A signed number that indicates the relative values of the instance and the object.Return value Condition Less than zero This instance is less than the object. Zero This instance is the same as the object. Greater than zero This instance is greater than the object -or- The object is a null reference (Nothing in Visual Basic). </returns>
		/// <param name="value">The <see cref="T:System.Data.SqlTypes.SqlString" /> to be compared.</param>
		// Token: 0x06000AFC RID: 2812 RVA: 0x0003DF58 File Offset: 0x0003C158
		public int CompareTo(SqlString value)
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
				int num = SqlString.StringCompare(this, value);
				if (num < 0)
				{
					return -1;
				}
				if (num > 0)
				{
					return 1;
				}
				return 0;
			}
		}

		/// <summary>Compares the supplied object parameter to the <see cref="P:System.Data.SqlTypes.SqlString.Value" /> property of the <see cref="T:System.Data.SqlTypes.SqlString" /> object.</summary>
		/// <returns>Equals will return true if the object is an instance of <see cref="T:System.Data.SqlTypes.SqlString" /> and the two are equal; otherwise false.</returns>
		/// <param name="value">The object to be compared. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000AFD RID: 2813 RVA: 0x0003DFA0 File Offset: 0x0003C1A0
		public override bool Equals(object value)
		{
			if (!(value is SqlString))
			{
				return false;
			}
			SqlString sqlString = (SqlString)value;
			if (sqlString.IsNull || this.IsNull)
			{
				return sqlString.IsNull && this.IsNull;
			}
			return (this == sqlString).Value;
		}

		/// <summary>Gets the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000AFE RID: 2814 RVA: 0x0003DFF8 File Offset: 0x0003C1F8
		public override int GetHashCode()
		{
			if (this.IsNull)
			{
				return 0;
			}
			byte[] array;
			if (this.FBinarySort())
			{
				array = SqlString.s_unicodeEncoding.GetBytes(this.m_value.TrimEnd());
			}
			else
			{
				CompareInfo compareInfo;
				CompareOptions compareOptions;
				try
				{
					this.SetCompareInfo();
					compareInfo = this.m_cmpInfo;
					compareOptions = SqlString.CompareOptionsFromSqlCompareOptions(this.m_flag);
				}
				catch (ArgumentException)
				{
					compareInfo = CultureInfo.InvariantCulture.CompareInfo;
					compareOptions = CompareOptions.None;
				}
				array = compareInfo.GetSortKey(this.m_value.TrimEnd(), compareOptions).KeyData;
			}
			return SqlBinary.HashByteArray(array, array.Length);
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <returns>An XmlSchema.</returns>
		// Token: 0x06000AFF RID: 2815 RVA: 0x00011F10 File Offset: 0x00010110
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <param name="reader">XmlReader</param>
		// Token: 0x06000B00 RID: 2816 RVA: 0x0003E08C File Offset: 0x0003C28C
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			string attribute = reader.GetAttribute("nil", "http://www.w3.org/2001/XMLSchema-instance");
			if (attribute != null && XmlConvert.ToBoolean(attribute))
			{
				reader.ReadElementString();
				this.m_fNotNull = false;
				return;
			}
			this.m_value = reader.ReadElementString();
			this.m_fNotNull = true;
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <param name="writer">XmlWriter</param>
		// Token: 0x06000B01 RID: 2817 RVA: 0x0003E0D7 File Offset: 0x0003C2D7
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			if (this.IsNull)
			{
				writer.WriteAttributeString("xsi", "nil", "http://www.w3.org/2001/XMLSchema-instance", "true");
				return;
			}
			writer.WriteString(this.m_value);
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the specified <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</summary>
		/// <returns>A string value that indicates the XSD of the specified <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</returns>
		/// <param name="schemaSet">A <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</param>
		// Token: 0x06000B02 RID: 2818 RVA: 0x00038ABB File Offset: 0x00036CBB
		public static XmlQualifiedName GetXsdType(XmlSchemaSet schemaSet)
		{
			return new XmlQualifiedName("string", "http://www.w3.org/2001/XMLSchema");
		}

		// Token: 0x0400046B RID: 1131
		private string m_value;

		// Token: 0x0400046C RID: 1132
		private CompareInfo m_cmpInfo;

		// Token: 0x0400046D RID: 1133
		private int m_lcid;

		// Token: 0x0400046E RID: 1134
		private SqlCompareOptions m_flag;

		// Token: 0x0400046F RID: 1135
		private bool m_fNotNull;

		/// <summary>Represents a <see cref="T:System.DBNull" /> that can be assigned to this instance of the <see cref="T:System.Data.SqlTypes.SqlString" /> structure.</summary>
		// Token: 0x04000470 RID: 1136
		public static readonly SqlString Null = new SqlString(true);

		// Token: 0x04000471 RID: 1137
		internal static readonly UnicodeEncoding s_unicodeEncoding = new UnicodeEncoding();

		/// <summary>Specifies that <see cref="T:System.Data.SqlTypes.SqlString" /> comparisons should ignore case.</summary>
		// Token: 0x04000472 RID: 1138
		public static readonly int IgnoreCase = 1;

		/// <summary>Specifies that the string comparison must ignore the character width. </summary>
		// Token: 0x04000473 RID: 1139
		public static readonly int IgnoreWidth = 16;

		/// <summary>Specifies that the string comparison must ignore non-space combining characters, such as diacritics. </summary>
		// Token: 0x04000474 RID: 1140
		public static readonly int IgnoreNonSpace = 2;

		/// <summary>Specifies that the string comparison must ignore the Kana type. </summary>
		// Token: 0x04000475 RID: 1141
		public static readonly int IgnoreKanaType = 8;

		/// <summary>Specifies that sorts should be based on a characters numeric value instead of its alphabetical value.</summary>
		// Token: 0x04000476 RID: 1142
		public static readonly int BinarySort = 32768;

		/// <summary>Specifies that sorts should be based on a character's numeric value instead of its alphabetical value.</summary>
		// Token: 0x04000477 RID: 1143
		public static readonly int BinarySort2 = 16384;

		// Token: 0x04000478 RID: 1144
		private static readonly SqlCompareOptions s_iDefaultFlag = SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth;

		// Token: 0x04000479 RID: 1145
		private static readonly CompareOptions s_iValidCompareOptionMask = CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth;

		// Token: 0x0400047A RID: 1146
		internal static readonly SqlCompareOptions s_iValidSqlCompareOptionMask = SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreNonSpace | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth | SqlCompareOptions.BinarySort | SqlCompareOptions.BinarySort2;

		// Token: 0x0400047B RID: 1147
		internal static readonly int s_lcidUSEnglish = 1033;

		// Token: 0x0400047C RID: 1148
		private static readonly int s_lcidBinary = 33280;
	}
}
