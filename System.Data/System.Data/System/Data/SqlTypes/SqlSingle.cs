using System;
using System.Data.Common;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Data.SqlTypes
{
	/// <summary>Represents a floating point number within the range of -3.40E +38 through 3.40E +38 to be stored in or retrieved from a database.</summary>
	// Token: 0x020000CC RID: 204
	[XmlSchemaProvider("GetXsdType")]
	[Serializable]
	public struct SqlSingle : INullable, IComparable, IXmlSerializable
	{
		// Token: 0x06000AC7 RID: 2759 RVA: 0x0003D49F File Offset: 0x0003B69F
		private SqlSingle(bool fNull)
		{
			this._fNotNull = false;
			this._value = 0f;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure.</summary>
		/// <param name="value">A floating point number which will be used as the <see cref="P:System.Data.SqlTypes.SqlSingle.Value" /> of the new <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure. </param>
		// Token: 0x06000AC8 RID: 2760 RVA: 0x0003D4B3 File Offset: 0x0003B6B3
		public SqlSingle(float value)
		{
			if (!float.IsFinite(value))
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			this._fNotNull = true;
			this._value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure using the supplied double parameter.</summary>
		/// <param name="value">A double value which will be used as the <see cref="P:System.Data.SqlTypes.SqlSingle.Value" /> of the new <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure. </param>
		// Token: 0x06000AC9 RID: 2761 RVA: 0x0003D4D6 File Offset: 0x0003B6D6
		public SqlSingle(double value)
		{
			this = new SqlSingle((float)value);
		}

		/// <summary>Indicates whether this <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure is null.</summary>
		/// <returns>true if null. Otherwise, false.</returns>
		// Token: 0x1700019C RID: 412
		// (get) Token: 0x06000ACA RID: 2762 RVA: 0x0003D4E0 File Offset: 0x0003B6E0
		public bool IsNull
		{
			get
			{
				return !this._fNotNull;
			}
		}

		/// <summary>Gets the value of this <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure. This property is read-only.</summary>
		/// <returns>A floating point value in the range -3.40E+38 through 3.40E+38.</returns>
		// Token: 0x1700019D RID: 413
		// (get) Token: 0x06000ACB RID: 2763 RVA: 0x0003D4EB File Offset: 0x0003B6EB
		public float Value
		{
			get
			{
				if (this._fNotNull)
				{
					return this._value;
				}
				throw new SqlNullValueException();
			}
		}

		/// <summary>Converts the specified floating point value to <see cref="T:System.Data.SqlTypes.SqlSingle" />.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure that contains the value of the specified float.</returns>
		/// <param name="x">The float value to be converted to <see cref="T:System.Data.SqlTypes.SqlSingle" />. </param>
		// Token: 0x06000ACC RID: 2764 RVA: 0x0003D501 File Offset: 0x0003B701
		public static implicit operator SqlSingle(float x)
		{
			return new SqlSingle(x);
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure to <see cref="T:System.String" />.</summary>
		/// <returns>A String object representing the value of this <see cref="T:System.Data.SqlTypes.SqlSingle" />.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06000ACD RID: 2765 RVA: 0x0003D509 File Offset: 0x0003B709
		public override string ToString()
		{
			if (!this.IsNull)
			{
				return this._value.ToString(null);
			}
			return SQLResource.NullString;
		}

		/// <summary>Negates the <see cref="P:System.Data.SqlTypes.SqlSingle.Value" /> of the specified <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure that contains the negated value.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure. </param>
		// Token: 0x06000ACE RID: 2766 RVA: 0x0003D525 File Offset: 0x0003B725
		public static SqlSingle operator -(SqlSingle x)
		{
			if (!x.IsNull)
			{
				return new SqlSingle(-x._value);
			}
			return SqlSingle.Null;
		}

		/// <summary>Computes the sum of the two specified <see cref="T:System.Data.SqlTypes.SqlSingle" /> structures.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure that contains the sum of the two specified <see cref="T:System.Data.SqlTypes.SqlSingle" /> structures.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure. </param>
		// Token: 0x06000ACF RID: 2767 RVA: 0x0003D542 File Offset: 0x0003B742
		public static SqlSingle operator +(SqlSingle x, SqlSingle y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlSingle.Null;
			}
			float num = x._value + y._value;
			if (float.IsInfinity(num))
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return new SqlSingle(num);
		}

		/// <summary>Subtracts the second <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure from the first.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure that contains the results of the subtraction.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure. </param>
		// Token: 0x06000AD0 RID: 2768 RVA: 0x0003D581 File Offset: 0x0003B781
		public static SqlSingle operator -(SqlSingle x, SqlSingle y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlSingle.Null;
			}
			float num = x._value - y._value;
			if (float.IsInfinity(num))
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return new SqlSingle(num);
		}

		/// <summary>Computes the product of the two specified <see cref="T:System.Data.SqlTypes.SqlSingle" /> structures.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure that contains the product of the multiplication.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure. </param>
		// Token: 0x06000AD1 RID: 2769 RVA: 0x0003D5C0 File Offset: 0x0003B7C0
		public static SqlSingle operator *(SqlSingle x, SqlSingle y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlSingle.Null;
			}
			float num = x._value * y._value;
			if (float.IsInfinity(num))
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return new SqlSingle(num);
		}

		/// <summary>Divides the first <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure by the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure that contains the results of the division.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure. </param>
		// Token: 0x06000AD2 RID: 2770 RVA: 0x0003D600 File Offset: 0x0003B800
		public static SqlSingle operator /(SqlSingle x, SqlSingle y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlSingle.Null;
			}
			if (y._value == 0f)
			{
				throw new DivideByZeroException(SQLResource.DivideByZeroMessage);
			}
			float num = x._value / y._value;
			if (float.IsInfinity(num))
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return new SqlSingle(num);
		}

		/// <summary>This implicit operator converts the <see cref="T:System.Data.SqlTypes.SqlByte" /> parameter to <see cref="T:System.Data.SqlTypes.SqlSingle" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure whose <see cref="P:System.Data.SqlTypes.SqlSingle.Value" /> property equals the <see cref="P:System.Data.SqlTypes.SqlByte.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlByte" /> parameter.</returns>
		/// <param name="x">The <see cref="T:System.Data.SqlTypes.SqlByte" /> to be converted. </param>
		// Token: 0x06000AD3 RID: 2771 RVA: 0x0003D662 File Offset: 0x0003B862
		public static implicit operator SqlSingle(SqlByte x)
		{
			if (!x.IsNull)
			{
				return new SqlSingle((float)x.Value);
			}
			return SqlSingle.Null;
		}

		/// <summary>Converts the supplied <see cref="T:System.Data.SqlTypes.SqlInt16" /> parameter to <see cref="T:System.Data.SqlTypes.SqlSingle" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure whose <see cref="P:System.Data.SqlTypes.SqlSingle.Value" /> is equal to the <see cref="P:System.Data.SqlTypes.SqlInt16.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlInt16" /> parameter.</returns>
		/// <param name="x">The <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure to be converted. </param>
		// Token: 0x06000AD4 RID: 2772 RVA: 0x0003D680 File Offset: 0x0003B880
		public static implicit operator SqlSingle(SqlInt16 x)
		{
			if (!x.IsNull)
			{
				return new SqlSingle((float)x.Value);
			}
			return SqlSingle.Null;
		}

		/// <summary>Converts the supplied <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure to <see cref="T:System.Data.SqlTypes.SqlSingle" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure whose <see cref="P:System.Data.SqlTypes.SqlSingle.Value" /> is equal to the <see cref="P:System.Data.SqlTypes.SqlInt32.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlInt32" /> parameter.</returns>
		/// <param name="x">The <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure to be converted. </param>
		// Token: 0x06000AD5 RID: 2773 RVA: 0x0003D69E File Offset: 0x0003B89E
		public static implicit operator SqlSingle(SqlInt32 x)
		{
			if (!x.IsNull)
			{
				return new SqlSingle((float)x.Value);
			}
			return SqlSingle.Null;
		}

		/// <summary>Converts the supplied <see cref="T:System.Data.SqlTypes.SqlInt64" /> parameter to <see cref="T:System.Data.SqlTypes.SqlSingle" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure whose <see cref="P:System.Data.SqlTypes.SqlSingle.Value" /> is equal to the <see cref="P:System.Data.SqlTypes.SqlInt64.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlInt64" /> parameter.</returns>
		/// <param name="x">The <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure to be converted. </param>
		// Token: 0x06000AD6 RID: 2774 RVA: 0x0003D6BC File Offset: 0x0003B8BC
		public static implicit operator SqlSingle(SqlInt64 x)
		{
			if (!x.IsNull)
			{
				return new SqlSingle((float)x.Value);
			}
			return SqlSingle.Null;
		}

		/// <summary>Converts the supplied <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure to <see cref="T:System.Data.SqlTypes.SqlSingle" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure whose <see cref="P:System.Data.SqlTypes.SqlSingle.Value" /> is equal to the <see cref="P:System.Data.SqlTypes.SqlMoney.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlMoney" /> parameter.</returns>
		/// <param name="x">The <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure to be converted. </param>
		// Token: 0x06000AD7 RID: 2775 RVA: 0x0003D6DA File Offset: 0x0003B8DA
		public static implicit operator SqlSingle(SqlMoney x)
		{
			if (!x.IsNull)
			{
				return new SqlSingle(x.ToDouble());
			}
			return SqlSingle.Null;
		}

		/// <summary>Converts the supplied <see cref="T:System.Data.SqlTypes.SqlDecimal" /> parameter to <see cref="T:System.Data.SqlTypes.SqlSingle" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure whose <see cref="P:System.Data.SqlTypes.SqlSingle.Value" /> is equal to the <see cref="P:System.Data.SqlTypes.SqlDecimal.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlDecimal" /> parameter.</returns>
		/// <param name="x">The <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure to be converted. </param>
		// Token: 0x06000AD8 RID: 2776 RVA: 0x0003D6F7 File Offset: 0x0003B8F7
		public static implicit operator SqlSingle(SqlDecimal x)
		{
			if (!x.IsNull)
			{
				return new SqlSingle(x.ToDouble());
			}
			return SqlSingle.Null;
		}

		/// <summary>Converts the supplied <see cref="T:System.Data.SqlTypes.SqlDouble" /> parameter to <see cref="T:System.Data.SqlTypes.SqlSingle" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure whose <see cref="P:System.Data.SqlTypes.SqlSingle.Value" /> is equal to the <see cref="P:System.Data.SqlTypes.SqlDouble.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlDouble" /> parameter.</returns>
		/// <param name="x">The <see cref="T:System.Data.SqlTypes.SqlDouble" /> parameter to be converted. </param>
		// Token: 0x06000AD9 RID: 2777 RVA: 0x0003D714 File Offset: 0x0003B914
		public static explicit operator SqlSingle(SqlDouble x)
		{
			if (!x.IsNull)
			{
				return new SqlSingle(x.Value);
			}
			return SqlSingle.Null;
		}

		/// <summary>Performs a logical comparison of the two SqlSingle parameters to determine whether they are equal.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the two instances are equal or <see cref="F:System.Data.SqlTypes.SqlBoolean.False" /> if the two instances are not equal. If either instance of <see cref="T:System.Data.SqlTypes.SqlSingle" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure. </param>
		// Token: 0x06000ADA RID: 2778 RVA: 0x0003D731 File Offset: 0x0003B931
		public static SqlBoolean operator ==(SqlSingle x, SqlSingle y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x._value == y._value);
			}
			return SqlBoolean.Null;
		}

		/// <summary>Performs a logical comparison of the two <see cref="T:System.Data.SqlTypes.SqlSingle" /> parameters to determine whether the first is less than the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is less than the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlSingle" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure. </param>
		// Token: 0x06000ADB RID: 2779 RVA: 0x0003D75E File Offset: 0x0003B95E
		public static SqlBoolean operator <(SqlSingle x, SqlSingle y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x._value < y._value);
			}
			return SqlBoolean.Null;
		}

		/// <summary>Performs a logical comparison of the two <see cref="T:System.Data.SqlTypes.SqlSingle" /> operands to determine whether the first is greater than the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is greater than the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlSingle" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure. </param>
		// Token: 0x06000ADC RID: 2780 RVA: 0x0003D78B File Offset: 0x0003B98B
		public static SqlBoolean operator >(SqlSingle x, SqlSingle y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x._value > y._value);
			}
			return SqlBoolean.Null;
		}

		/// <summary>Performs a logical comparison of the two <see cref="T:System.Data.SqlTypes.SqlSingle" /> parameters to determine whether the first is less than the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is less than the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlSingle" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure. </param>
		// Token: 0x06000ADD RID: 2781 RVA: 0x0003D7B8 File Offset: 0x0003B9B8
		public static SqlBoolean LessThan(SqlSingle x, SqlSingle y)
		{
			return x < y;
		}

		/// <summary>Performs a logical comparison of the two <see cref="T:System.Data.SqlTypes.SqlSingle" /> operands to determine whether the first is greater than the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is greater than the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlSingle" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure. </param>
		// Token: 0x06000ADE RID: 2782 RVA: 0x0003D7C1 File Offset: 0x0003B9C1
		public static SqlBoolean GreaterThan(SqlSingle x, SqlSingle y)
		{
			return x > y;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure to <see cref="T:System.Data.SqlTypes.SqlDouble" />.</summary>
		/// <returns>A new SqlDouble equal to the value of this <see cref="T:System.Data.SqlTypes.SqlSingle" />.</returns>
		// Token: 0x06000ADF RID: 2783 RVA: 0x0003D7CA File Offset: 0x0003B9CA
		public SqlDouble ToSqlDouble()
		{
			return this;
		}

		/// <summary>Compares this <see cref="T:System.Data.SqlTypes.SqlSingle" /> instance to the supplied <see cref="T:System.Object" /> and returns an indication of their relative values.</summary>
		/// <returns>A signed number that indicates the relative values of the instance and the object.Return value Condition Less than zero This instance is less than the object. Zero This instance is the same as the object. Greater than zero This instance is greater than the object -or- The object is a null reference (Nothing in Visual Basic) </returns>
		/// <param name="value">The <see cref="T:System.Object" /> to be compared. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000AE0 RID: 2784 RVA: 0x0003D7D8 File Offset: 0x0003B9D8
		public int CompareTo(object value)
		{
			if (value is SqlSingle)
			{
				SqlSingle sqlSingle = (SqlSingle)value;
				return this.CompareTo(sqlSingle);
			}
			throw ADP.WrongType(value.GetType(), typeof(SqlSingle));
		}

		/// <summary>Compares this <see cref="T:System.Data.SqlTypes.SqlSingle" /> instance to the supplied <see cref="T:System.Data.SqlTypes.SqlSingle" /> and returns an indication of their relative values.</summary>
		/// <returns>A signed number that indicates the relative values of the instance and the object.Return Value Condition Less than zero This instance is less than the object. Zero This instance is the same as the object. Greater than zero This instance is greater than the object -or- The object is a null reference (Nothing in Visual Basic) </returns>
		/// <param name="value">The <see cref="T:System.Data.SqlTypes.SqlSingle" /> to be compared.</param>
		// Token: 0x06000AE1 RID: 2785 RVA: 0x0003D814 File Offset: 0x0003BA14
		public int CompareTo(SqlSingle value)
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

		/// <summary>Compares the supplied object parameter to the <see cref="P:System.Data.SqlTypes.SqlSingle.Value" /> property of the <see cref="T:System.Data.SqlTypes.SqlSingle" /> object.</summary>
		/// <returns>true if the object is an instance of <see cref="T:System.Data.SqlTypes.SqlSingle" /> and the two are equal. Otherwise, false.</returns>
		/// <param name="value">The object to be compared. </param>
		// Token: 0x06000AE2 RID: 2786 RVA: 0x0003D86C File Offset: 0x0003BA6C
		public override bool Equals(object value)
		{
			if (!(value is SqlSingle))
			{
				return false;
			}
			SqlSingle sqlSingle = (SqlSingle)value;
			if (sqlSingle.IsNull || this.IsNull)
			{
				return sqlSingle.IsNull && this.IsNull;
			}
			return (this == sqlSingle).Value;
		}

		/// <summary>Gets the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06000AE3 RID: 2787 RVA: 0x0003D8C4 File Offset: 0x0003BAC4
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
		// Token: 0x06000AE4 RID: 2788 RVA: 0x00011F10 File Offset: 0x00010110
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <param name="reader">XmlReader </param>
		// Token: 0x06000AE5 RID: 2789 RVA: 0x0003D8EC File Offset: 0x0003BAEC
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			string attribute = reader.GetAttribute("nil", "http://www.w3.org/2001/XMLSchema-instance");
			if (attribute != null && XmlConvert.ToBoolean(attribute))
			{
				reader.ReadElementString();
				this._fNotNull = false;
				return;
			}
			this._value = XmlConvert.ToSingle(reader.ReadElementString());
			this._fNotNull = true;
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <param name="writer">XmlWriter</param>
		// Token: 0x06000AE6 RID: 2790 RVA: 0x0003D93C File Offset: 0x0003BB3C
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			if (this.IsNull)
			{
				writer.WriteAttributeString("xsi", "nil", "http://www.w3.org/2001/XMLSchema-instance", "true");
				return;
			}
			writer.WriteString(XmlConvert.ToString(this._value));
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the specified <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</summary>
		/// <returns>A string value that indicates the XSD of the specified <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</returns>
		/// <param name="schemaSet">A <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</param>
		// Token: 0x06000AE7 RID: 2791 RVA: 0x0003D972 File Offset: 0x0003BB72
		public static XmlQualifiedName GetXsdType(XmlSchemaSet schemaSet)
		{
			return new XmlQualifiedName("float", "http://www.w3.org/2001/XMLSchema");
		}

		// Token: 0x0400045D RID: 1117
		private bool _fNotNull;

		// Token: 0x0400045E RID: 1118
		private float _value;

		/// <summary>Represents a <see cref="T:System.DBNull" /> that can be assigned to this instance of the <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure.</summary>
		// Token: 0x0400045F RID: 1119
		public static readonly SqlSingle Null = new SqlSingle(true);

		/// <summary>Represents the zero value that can be assigned to the <see cref="P:System.Data.SqlTypes.SqlSingle.Value" /> property of an instance of the <see cref="T:System.Data.SqlTypes.SqlSingle" /> class.</summary>
		// Token: 0x04000460 RID: 1120
		public static readonly SqlSingle Zero = new SqlSingle(0f);

		/// <summary>Represents the minimum value that can be assigned to <see cref="P:System.Data.SqlTypes.SqlSingle.Value" /> property of an instance of the <see cref="T:System.Data.SqlTypes.SqlSingle" /> class.</summary>
		// Token: 0x04000461 RID: 1121
		public static readonly SqlSingle MinValue = new SqlSingle(float.MinValue);

		/// <summary>Represents the maximum value that can be assigned to the <see cref="P:System.Data.SqlTypes.SqlSingle.Value" /> property of an instance of the <see cref="T:System.Data.SqlTypes.SqlSingle" /> class.</summary>
		// Token: 0x04000462 RID: 1122
		public static readonly SqlSingle MaxValue = new SqlSingle(float.MaxValue);
	}
}
