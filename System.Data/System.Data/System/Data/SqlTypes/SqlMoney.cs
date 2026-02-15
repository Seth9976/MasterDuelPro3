using System;
using System.Data.Common;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Data.SqlTypes
{
	/// <summary>Represents a currency value ranging from -2 63 (or -922,337,203,685,477.5808) to 2 63 -1 (or +922,337,203,685,477.5807) with an accuracy to a ten-thousandth of currency unit to be stored in or retrieved from a database.</summary>
	// Token: 0x020000CB RID: 203
	[XmlSchemaProvider("GetXsdType")]
	[Serializable]
	public struct SqlMoney : INullable, IComparable, IXmlSerializable
	{
		// Token: 0x06000AA1 RID: 2721 RVA: 0x0003CE17 File Offset: 0x0003B017
		private SqlMoney(bool fNull)
		{
			this._fNotNull = false;
			this._value = 0L;
		}

		// Token: 0x06000AA2 RID: 2722 RVA: 0x0003CE28 File Offset: 0x0003B028
		internal SqlMoney(long value, int ignored)
		{
			this._value = value;
			this._fNotNull = true;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlMoney" /> class with the specified integer value.</summary>
		/// <param name="value">The monetary value to initialize. </param>
		// Token: 0x06000AA3 RID: 2723 RVA: 0x0003CE38 File Offset: 0x0003B038
		public SqlMoney(int value)
		{
			this._value = (long)value * SqlMoney.s_lTickBase;
			this._fNotNull = true;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlMoney" /> class with the specified long integer value.</summary>
		/// <param name="value">The monetary value to initialize. </param>
		// Token: 0x06000AA4 RID: 2724 RVA: 0x0003CE4F File Offset: 0x0003B04F
		public SqlMoney(long value)
		{
			if (value < SqlMoney.s_minLong || value > SqlMoney.s_maxLong)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			this._value = value * SqlMoney.s_lTickBase;
			this._fNotNull = true;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlMoney" /> class with the specified <see cref="T:System.Decimal" /> value.</summary>
		/// <param name="value">The monetary value to initialize. </param>
		// Token: 0x06000AA5 RID: 2725 RVA: 0x0003CE80 File Offset: 0x0003B080
		public SqlMoney(decimal value)
		{
			SqlDecimal sqlDecimal = new SqlDecimal(value);
			sqlDecimal.AdjustScale(SqlMoney.s_iMoneyScale - (int)sqlDecimal.Scale, true);
			if (sqlDecimal._data3 != 0U || sqlDecimal._data4 != 0U)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			bool isPositive = sqlDecimal.IsPositive;
			ulong num = (ulong)sqlDecimal._data1 + ((ulong)sqlDecimal._data2 << 32);
			if ((isPositive && num > 9223372036854775807UL) || (!isPositive && num > 9223372036854775808UL))
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			this._value = (long)(isPositive ? num : (-(long)num));
			this._fNotNull = true;
		}

		/// <summary>Returns a Boolean value that indicates whether this <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure is null.</summary>
		/// <returns>true if null. Otherwise, false.</returns>
		// Token: 0x1700019A RID: 410
		// (get) Token: 0x06000AA6 RID: 2726 RVA: 0x0003CF1E File Offset: 0x0003B11E
		public bool IsNull
		{
			get
			{
				return !this._fNotNull;
			}
		}

		/// <summary>Gets the monetary value of an instance of the <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure. This property is read-only.</summary>
		/// <returns>The monetary value of an instance of the <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure.</returns>
		/// <exception cref="T:System.Data.SqlTypes.SqlNullValueException">The property is set to null. </exception>
		// Token: 0x1700019B RID: 411
		// (get) Token: 0x06000AA7 RID: 2727 RVA: 0x0003CF29 File Offset: 0x0003B129
		public decimal Value
		{
			get
			{
				if (this._fNotNull)
				{
					return this.ToDecimal();
				}
				throw new SqlNullValueException();
			}
		}

		/// <summary>Converts the Value of this instance of <see cref="T:System.Data.SqlTypes.SqlMoney" /> as a <see cref="T:System.Decimal" /> structure.</summary>
		/// <returns>A <see cref="T:System.Decimal" /> structure whose value equals the <see cref="P:System.Data.SqlTypes.SqlMoney.Value" /> property of this <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure.</returns>
		// Token: 0x06000AA8 RID: 2728 RVA: 0x0003CF40 File Offset: 0x0003B140
		public decimal ToDecimal()
		{
			if (this.IsNull)
			{
				throw new SqlNullValueException();
			}
			bool flag = false;
			long num = this._value;
			if (this._value < 0L)
			{
				flag = true;
				num = -this._value;
			}
			return new decimal((int)num, (int)(num >> 32), 0, flag, (byte)SqlMoney.s_iMoneyScale);
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure to a <see cref="T:System.Double" />.</summary>
		/// <returns>A double with a value equal to this <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure.</returns>
		// Token: 0x06000AA9 RID: 2729 RVA: 0x0003CF8C File Offset: 0x0003B18C
		public double ToDouble()
		{
			return decimal.ToDouble(this.ToDecimal());
		}

		/// <summary>Converts the <see cref="T:System.Decimal" /> parameter to <see cref="T:System.Data.SqlTypes.SqlMoney" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure whose <see cref="P:System.Data.SqlTypes.SqlMoney.Value" /> equals the value of the <see cref="T:System.Decimal" /> parameter.</returns>
		/// <param name="x">The <see cref="T:System.Decimal" /> value to be converted. </param>
		// Token: 0x06000AAA RID: 2730 RVA: 0x0003CF99 File Offset: 0x0003B199
		public static implicit operator SqlMoney(decimal x)
		{
			return new SqlMoney(x);
		}

		/// <summary>This implicit operator converts the supplied <see cref="T:System.Int64" /> parameter to <see cref="T:System.Data.SqlTypes.SqlMoney" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure whose <see cref="P:System.Data.SqlTypes.SqlMoney.Value" /> property is equal to the value of the <see cref="T:System.Int64" /> parameter.</returns>
		/// <param name="x">The <see cref="T:System.Int64" /> structure to be converted. </param>
		// Token: 0x06000AAB RID: 2731 RVA: 0x0003CFA1 File Offset: 0x0003B1A1
		public static implicit operator SqlMoney(long x)
		{
			return new SqlMoney(new decimal(x));
		}

		/// <summary>Converts this instance of <see cref="T:System.Data.SqlTypes.SqlMoney" /> to string.</summary>
		/// <returns>A string whose value is the string representation of the value of this <see cref="T:System.Data.SqlTypes.SqlMoney" />.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06000AAC RID: 2732 RVA: 0x0003CFB0 File Offset: 0x0003B1B0
		public override string ToString()
		{
			if (this.IsNull)
			{
				return SQLResource.NullString;
			}
			return this.ToDecimal().ToString("#0.00##", null);
		}

		/// <summary>The unary minus operator negates the <see cref="T:System.Data.SqlTypes.SqlMoney" /> parameter.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure whose <see cref="P:System.Data.SqlTypes.SqlMoney.Value" /> contains the results of the negation.</returns>
		/// <param name="x">The <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure to be negated. </param>
		// Token: 0x06000AAD RID: 2733 RVA: 0x0003CFDF File Offset: 0x0003B1DF
		public static SqlMoney operator -(SqlMoney x)
		{
			if (x.IsNull)
			{
				return SqlMoney.Null;
			}
			if (x._value == SqlMoney.s_minLong)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return new SqlMoney(-x._value, 0);
		}

		/// <summary>Calculates the sum of the two <see cref="T:System.Data.SqlTypes.SqlMoney" /> parameters.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlMoney" /> stucture whose <see cref="P:System.Data.SqlTypes.SqlMoney.Value" /> contains the sum of the two <see cref="T:System.Data.SqlTypes.SqlMoney" /> parameters.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure. </param>
		// Token: 0x06000AAE RID: 2734 RVA: 0x0003D018 File Offset: 0x0003B218
		public static SqlMoney operator +(SqlMoney x, SqlMoney y)
		{
			SqlMoney sqlMoney;
			try
			{
				sqlMoney = ((x.IsNull || y.IsNull) ? SqlMoney.Null : new SqlMoney(checked(x._value + y._value), 0));
			}
			catch (OverflowException)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return sqlMoney;
		}

		/// <summary>The subtraction operator subtracts the second <see cref="T:System.Data.SqlTypes.SqlMoney" /> parameter from the first.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure that contains the results of the subtraction.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure. </param>
		// Token: 0x06000AAF RID: 2735 RVA: 0x0003D074 File Offset: 0x0003B274
		public static SqlMoney operator -(SqlMoney x, SqlMoney y)
		{
			SqlMoney sqlMoney;
			try
			{
				sqlMoney = ((x.IsNull || y.IsNull) ? SqlMoney.Null : new SqlMoney(checked(x._value - y._value), 0));
			}
			catch (OverflowException)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return sqlMoney;
		}

		/// <summary>The multiplicaion operator calculates the product of the two <see cref="T:System.Data.SqlTypes.SqlMoney" /> parameters.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure whose <see cref="P:System.Data.SqlTypes.SqlMoney.Value" /> contains the product of the multiplication.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure. </param>
		// Token: 0x06000AB0 RID: 2736 RVA: 0x0003D0D0 File Offset: 0x0003B2D0
		public static SqlMoney operator *(SqlMoney x, SqlMoney y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlMoney(decimal.Multiply(x.ToDecimal(), y.ToDecimal()));
			}
			return SqlMoney.Null;
		}

		/// <summary>The division operator divides the first <see cref="T:System.Data.SqlTypes.SqlMoney" /> parameter by the second.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure whose <see cref="P:System.Data.SqlTypes.SqlMoney.Value" /> contains the results of the division.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure. </param>
		// Token: 0x06000AB1 RID: 2737 RVA: 0x0003D102 File Offset: 0x0003B302
		public static SqlMoney operator /(SqlMoney x, SqlMoney y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlMoney(decimal.Divide(x.ToDecimal(), y.ToDecimal()));
			}
			return SqlMoney.Null;
		}

		/// <summary>This implicit operator converts the supplied <see cref="T:System.Data.SqlTypes.SqlByte" /> parameter to <see cref="T:System.Data.SqlTypes.SqlMoney" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure whose <see cref="P:System.Data.SqlTypes.SqlMoney.Value" /> property is equal to the <see cref="P:System.Data.SqlTypes.SqlByte.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlByte" /> parameter.</returns>
		/// <param name="x">The <see cref="T:System.Data.SqlTypes.SqlByte" /> structure to be converted. </param>
		// Token: 0x06000AB2 RID: 2738 RVA: 0x0003D134 File Offset: 0x0003B334
		public static implicit operator SqlMoney(SqlByte x)
		{
			if (!x.IsNull)
			{
				return new SqlMoney((int)x.Value);
			}
			return SqlMoney.Null;
		}

		/// <summary>This implicit operator converts the supplied <see cref="T:System.Data.SqlTypes.SqlInt16" /> parameter to <see cref="T:System.Data.SqlTypes.SqlMoney" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure whose <see cref="P:System.Data.SqlTypes.SqlMoney.Value" /> property equals the <see cref="P:System.Data.SqlTypes.SqlInt16.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlInt16" /> parameter.</returns>
		/// <param name="x">The <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure to be converted. </param>
		// Token: 0x06000AB3 RID: 2739 RVA: 0x0003D151 File Offset: 0x0003B351
		public static implicit operator SqlMoney(SqlInt16 x)
		{
			if (!x.IsNull)
			{
				return new SqlMoney((int)x.Value);
			}
			return SqlMoney.Null;
		}

		/// <summary>This implicit operator converts the supplied <see cref="T:System.Data.SqlTypes.SqlInt32" /> parameter to <see cref="T:System.Data.SqlTypes.SqlMoney" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure whose <see cref="P:System.Data.SqlTypes.SqlMoney.Value" /> property equals the <see cref="P:System.Data.SqlTypes.SqlInt32.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlInt32" /> parameter.</returns>
		/// <param name="x">The <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure to be converted. </param>
		// Token: 0x06000AB4 RID: 2740 RVA: 0x0003D16E File Offset: 0x0003B36E
		public static implicit operator SqlMoney(SqlInt32 x)
		{
			if (!x.IsNull)
			{
				return new SqlMoney(x.Value);
			}
			return SqlMoney.Null;
		}

		/// <summary>This implicit operator converts the supplied <see cref="T:System.Data.SqlTypes.SqlInt64" /> parameter to <see cref="T:System.Data.SqlTypes.SqlMoney" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure whose <see cref="P:System.Data.SqlTypes.SqlMoney.Value" /> property equals the <see cref="P:System.Data.SqlTypes.SqlInt64.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlInt64" /> parameter.</returns>
		/// <param name="x">The <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure to be converted. </param>
		// Token: 0x06000AB5 RID: 2741 RVA: 0x0003D18B File Offset: 0x0003B38B
		public static implicit operator SqlMoney(SqlInt64 x)
		{
			if (!x.IsNull)
			{
				return new SqlMoney(x.Value);
			}
			return SqlMoney.Null;
		}

		/// <summary>This operator converts the supplied <see cref="T:System.Data.SqlTypes.SqlDecimal" /> parameter to <see cref="T:System.Data.SqlTypes.SqlMoney" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure whose <see cref="P:System.Data.SqlTypes.SqlMoney.Value" /> property equals the <see cref="P:System.Data.SqlTypes.SqlDecimal.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlDecimal" /> parameter.</returns>
		/// <param name="x">The <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure to be converted. </param>
		// Token: 0x06000AB6 RID: 2742 RVA: 0x0003D1A8 File Offset: 0x0003B3A8
		public static explicit operator SqlMoney(SqlDecimal x)
		{
			if (!x.IsNull)
			{
				return new SqlMoney(x.Value);
			}
			return SqlMoney.Null;
		}

		/// <summary>Performs a logical comparison of the two <see cref="T:System.Data.SqlTypes.SqlMoney" /> parameters to determine whether they are equal.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the two instances are equal or <see cref="F:System.Data.SqlTypes.SqlBoolean.False" /> if the two instances are not equal. If either instance of <see cref="T:System.Data.SqlTypes.SqlMoney" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure. </param>
		// Token: 0x06000AB7 RID: 2743 RVA: 0x0003D1C5 File Offset: 0x0003B3C5
		public static SqlBoolean operator ==(SqlMoney x, SqlMoney y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x._value == y._value);
			}
			return SqlBoolean.Null;
		}

		/// <summary>Performs a logical comparison of the two <see cref="T:System.Data.SqlTypes.SqlMoney" /> parameters to determine whether the first is less than the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is less than the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlMoney" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure. </param>
		// Token: 0x06000AB8 RID: 2744 RVA: 0x0003D1F2 File Offset: 0x0003B3F2
		public static SqlBoolean operator <(SqlMoney x, SqlMoney y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x._value < y._value);
			}
			return SqlBoolean.Null;
		}

		/// <summary>Performs a logical comparison of the two <see cref="T:System.Data.SqlTypes.SqlMoney" /> parameters to determine whether the first is greater than the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is greater than the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlMoney" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure. </param>
		// Token: 0x06000AB9 RID: 2745 RVA: 0x0003D21F File Offset: 0x0003B41F
		public static SqlBoolean operator >(SqlMoney x, SqlMoney y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x._value > y._value);
			}
			return SqlBoolean.Null;
		}

		/// <summary>Performs a logical comparison of the two <see cref="T:System.Data.SqlTypes.SqlMoney" /> parameters to determine whether the first is less than the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is less than the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlMoney" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure. </param>
		// Token: 0x06000ABA RID: 2746 RVA: 0x0003D24C File Offset: 0x0003B44C
		public static SqlBoolean LessThan(SqlMoney x, SqlMoney y)
		{
			return x < y;
		}

		/// <summary>Performs a logical comparison of the two <see cref="T:System.Data.SqlTypes.SqlMoney" /> parameters to determine whether the first is greater than the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is greater than the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlMoney" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure. </param>
		// Token: 0x06000ABB RID: 2747 RVA: 0x0003D255 File Offset: 0x0003B455
		public static SqlBoolean GreaterThan(SqlMoney x, SqlMoney y)
		{
			return x > y;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure to <see cref="T:System.Data.SqlTypes.SqlDouble" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlDouble" /> equal to the value of this <see cref="T:System.Data.SqlTypes.SqlMoney" />.</returns>
		// Token: 0x06000ABC RID: 2748 RVA: 0x0003D25E File Offset: 0x0003B45E
		public SqlDouble ToSqlDouble()
		{
			return this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure to <see cref="T:System.Data.SqlTypes.SqlDecimal" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlDecimal" /> equal to the value of this <see cref="T:System.Data.SqlTypes.SqlMoney" />.</returns>
		// Token: 0x06000ABD RID: 2749 RVA: 0x0003D26B File Offset: 0x0003B46B
		public SqlDecimal ToSqlDecimal()
		{
			return this;
		}

		/// <summary>Compares this <see cref="T:System.Data.SqlTypes.SqlMoney" /> instance to the supplied <see cref="T:System.Object" /> and returns an indication of their relative values.</summary>
		/// <returns>A signed number that indicates the relative values of the instance and the object.Return value Condition Less than zero This instance is less than the object. Zero This instance is the same as the object. Greater than zero This instance is greater than the object -or- The object is a null reference (Nothing in Visual Basic) </returns>
		/// <param name="value">The <see cref="T:System.Object" /> to be compared. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000ABE RID: 2750 RVA: 0x0003D278 File Offset: 0x0003B478
		public int CompareTo(object value)
		{
			if (value is SqlMoney)
			{
				SqlMoney sqlMoney = (SqlMoney)value;
				return this.CompareTo(sqlMoney);
			}
			throw ADP.WrongType(value.GetType(), typeof(SqlMoney));
		}

		/// <summary>Compares this <see cref="T:System.Data.SqlTypes.SqlMoney" /> instance to the supplied <see cref="T:System.Data.SqlTypes.SqlMoney" /> and returns an indication of their relative values.</summary>
		/// <returns>A signed number that indicates the relative values of the instance and the object.Return value Condition Less than zero This instance is less than the object. Zero This instance is the same as the object. Greater than zero This instance is greater than the object -or- The object is a null reference (Nothing in Visual Basic) </returns>
		/// <param name="value">The <see cref="T:System.Data.SqlTypes.SqlMoney" /> to be compared.</param>
		// Token: 0x06000ABF RID: 2751 RVA: 0x0003D2B4 File Offset: 0x0003B4B4
		public int CompareTo(SqlMoney value)
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

		/// <summary>Compares the supplied object parameter to the <see cref="P:System.Data.SqlTypes.SqlMoney.Value" /> property of the <see cref="T:System.Data.SqlTypes.SqlMoney" /> object.</summary>
		/// <returns>Equals will return true if the object is an instance of <see cref="T:System.Data.SqlTypes.SqlMoney" /> and the two are equal; otherwise false.</returns>
		/// <param name="value">The object to be compared. </param>
		// Token: 0x06000AC0 RID: 2752 RVA: 0x0003D30C File Offset: 0x0003B50C
		public override bool Equals(object value)
		{
			if (!(value is SqlMoney))
			{
				return false;
			}
			SqlMoney sqlMoney = (SqlMoney)value;
			if (sqlMoney.IsNull || this.IsNull)
			{
				return sqlMoney.IsNull && this.IsNull;
			}
			return (this == sqlMoney).Value;
		}

		/// <summary>Gets the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06000AC1 RID: 2753 RVA: 0x0003D361 File Offset: 0x0003B561
		public override int GetHashCode()
		{
			if (!this.IsNull)
			{
				return this._value.GetHashCode();
			}
			return 0;
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <returns>An XmlSchema.</returns>
		// Token: 0x06000AC2 RID: 2754 RVA: 0x00011F10 File Offset: 0x00010110
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <param name="reader">XmlReader</param>
		// Token: 0x06000AC3 RID: 2755 RVA: 0x0003D378 File Offset: 0x0003B578
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			string attribute = reader.GetAttribute("nil", "http://www.w3.org/2001/XMLSchema-instance");
			if (attribute != null && XmlConvert.ToBoolean(attribute))
			{
				reader.ReadElementString();
				this._fNotNull = false;
				return;
			}
			SqlMoney sqlMoney = new SqlMoney(XmlConvert.ToDecimal(reader.ReadElementString()));
			this._fNotNull = sqlMoney._fNotNull;
			this._value = sqlMoney._value;
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <param name="writer">XmlWriter</param>
		// Token: 0x06000AC4 RID: 2756 RVA: 0x0003D3DA File Offset: 0x0003B5DA
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			if (this.IsNull)
			{
				writer.WriteAttributeString("xsi", "nil", "http://www.w3.org/2001/XMLSchema-instance", "true");
				return;
			}
			writer.WriteString(XmlConvert.ToString(this.ToDecimal()));
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the specified <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</summary>
		/// <returns>A string that indicates the XSD of the specified <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</returns>
		/// <param name="schemaSet">An <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</param>
		// Token: 0x06000AC5 RID: 2757 RVA: 0x0003B18F File Offset: 0x0003938F
		public static XmlQualifiedName GetXsdType(XmlSchemaSet schemaSet)
		{
			return new XmlQualifiedName("decimal", "http://www.w3.org/2001/XMLSchema");
		}

		// Token: 0x04000452 RID: 1106
		private bool _fNotNull;

		// Token: 0x04000453 RID: 1107
		private long _value;

		// Token: 0x04000454 RID: 1108
		internal static readonly int s_iMoneyScale = 4;

		// Token: 0x04000455 RID: 1109
		private static readonly long s_lTickBase = 10000L;

		// Token: 0x04000456 RID: 1110
		private static readonly double s_dTickBase = (double)SqlMoney.s_lTickBase;

		// Token: 0x04000457 RID: 1111
		private static readonly long s_minLong = long.MinValue / SqlMoney.s_lTickBase;

		// Token: 0x04000458 RID: 1112
		private static readonly long s_maxLong = long.MaxValue / SqlMoney.s_lTickBase;

		/// <summary>Represents a <see cref="T:System.DBNull" /> that can be assigned to this instance of the <see cref="T:System.Data.SqlTypes.SqlMoney" /> class.</summary>
		// Token: 0x04000459 RID: 1113
		public static readonly SqlMoney Null = new SqlMoney(true);

		/// <summary>Represents the zero value that can be assigned to the <see cref="P:System.Data.SqlTypes.SqlMoney.Value" /> property of an instance of the <see cref="T:System.Data.SqlTypes.SqlMoney" /> class.</summary>
		// Token: 0x0400045A RID: 1114
		public static readonly SqlMoney Zero = new SqlMoney(0);

		/// <summary>Represents the minimum value that can be assigned to <see cref="P:System.Data.SqlTypes.SqlMoney.Value" /> property of an instance of the <see cref="T:System.Data.SqlTypes.SqlMoney" /> class.</summary>
		// Token: 0x0400045B RID: 1115
		public static readonly SqlMoney MinValue = new SqlMoney(long.MinValue, 0);

		/// <summary>Represents the maximum value that can be assigned to the <see cref="P:System.Data.SqlTypes.SqlMoney.Value" /> property of an instance of the <see cref="T:System.Data.SqlTypes.SqlMoney" /> class.</summary>
		// Token: 0x0400045C RID: 1116
		public static readonly SqlMoney MaxValue = new SqlMoney(long.MaxValue, 0);
	}
}
