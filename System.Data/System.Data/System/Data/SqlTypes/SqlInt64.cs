using System;
using System.Data.Common;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Data.SqlTypes
{
	/// <summary>Represents a 64-bit signed integer to be stored in or retrieved from a database.</summary>
	// Token: 0x020000CA RID: 202
	[XmlSchemaProvider("GetXsdType")]
	[Serializable]
	public struct SqlInt64 : INullable, IComparable, IXmlSerializable
	{
		// Token: 0x06000A7D RID: 2685 RVA: 0x0003C6CF File Offset: 0x0003A8CF
		private SqlInt64(bool fNull)
		{
			this.m_fNotNull = false;
			this.m_value = 0L;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure using the supplied long integer.</summary>
		/// <param name="value">A long integer. </param>
		// Token: 0x06000A7E RID: 2686 RVA: 0x0003C6E0 File Offset: 0x0003A8E0
		public SqlInt64(long value)
		{
			this.m_value = value;
			this.m_fNotNull = true;
		}

		/// <summary>Gets a Boolean value that indicates whether this <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure is null.</summary>
		/// <returns>true if null. Otherwise, false.</returns>
		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06000A7F RID: 2687 RVA: 0x0003C6F0 File Offset: 0x0003A8F0
		public bool IsNull
		{
			get
			{
				return !this.m_fNotNull;
			}
		}

		/// <summary>Gets the value of this <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure. This property is read-only.</summary>
		/// <returns>A long integer representing the value of this <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure.</returns>
		// Token: 0x17000199 RID: 409
		// (get) Token: 0x06000A80 RID: 2688 RVA: 0x0003C6FB File Offset: 0x0003A8FB
		public long Value
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

		/// <summary>Converts the long parameter to <see cref="T:System.Data.SqlTypes.SqlInt64" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt64.Value" /> equals the value of the long parameter.</returns>
		/// <param name="x">A long integer value. </param>
		// Token: 0x06000A81 RID: 2689 RVA: 0x0003C711 File Offset: 0x0003A911
		public static implicit operator SqlInt64(long x)
		{
			return new SqlInt64(x);
		}

		/// <summary>Converts this instance of <see cref="T:System.Data.SqlTypes.SqlInt64" /> to <see cref="T:System.String" />.</summary>
		/// <returns>A <see cref="T:System.String" /> representing the value of this <see cref="T:System.Data.SqlTypes.SqlInt64" />.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06000A82 RID: 2690 RVA: 0x0003C719 File Offset: 0x0003A919
		public override string ToString()
		{
			if (!this.IsNull)
			{
				return this.m_value.ToString(null);
			}
			return SQLResource.NullString;
		}

		/// <summary>The unary minus operator negates the <see cref="P:System.Data.SqlTypes.SqlInt64.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlInt64" /> operand.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt64.Value" /> is equal to the negated <see cref="P:System.Data.SqlTypes.SqlInt64.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlInt64" /> parameter.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure. </param>
		// Token: 0x06000A83 RID: 2691 RVA: 0x0003C735 File Offset: 0x0003A935
		public static SqlInt64 operator -(SqlInt64 x)
		{
			if (!x.IsNull)
			{
				return new SqlInt64(-x.m_value);
			}
			return SqlInt64.Null;
		}

		/// <summary>Computes the sum of the two <see cref="T:System.Data.SqlTypes.SqlInt64" /> parameters.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt64.Value" /> is equal to the sum of the two <see cref="T:System.Data.SqlTypes.SqlInt64" /> parameters.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure. </param>
		// Token: 0x06000A84 RID: 2692 RVA: 0x0003C754 File Offset: 0x0003A954
		public static SqlInt64 operator +(SqlInt64 x, SqlInt64 y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlInt64.Null;
			}
			long num = x.m_value + y.m_value;
			if (SqlInt64.SameSignLong(x.m_value, y.m_value) && !SqlInt64.SameSignLong(x.m_value, num))
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return new SqlInt64(num);
		}

		/// <summary>Subtracts the second <see cref="T:System.Data.SqlTypes.SqlInt64" /> parameter from the first.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt64.Value" /> property equals the results of the subtraction operation.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure. </param>
		// Token: 0x06000A85 RID: 2693 RVA: 0x0003C7BC File Offset: 0x0003A9BC
		public static SqlInt64 operator -(SqlInt64 x, SqlInt64 y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlInt64.Null;
			}
			long num = x.m_value - y.m_value;
			if (!SqlInt64.SameSignLong(x.m_value, y.m_value) && SqlInt64.SameSignLong(y.m_value, num))
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return new SqlInt64(num);
		}

		/// <summary>Computes the product of the two <see cref="T:System.Data.SqlTypes.SqlInt64" /> parameters.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt64.Value" /> is equal to the product of the two <see cref="T:System.Data.SqlTypes.SqlInt64" /> parameters.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure. </param>
		// Token: 0x06000A86 RID: 2694 RVA: 0x0003C824 File Offset: 0x0003AA24
		public static SqlInt64 operator *(SqlInt64 x, SqlInt64 y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlInt64.Null;
			}
			bool flag = false;
			long num = x.m_value;
			long num2 = y.m_value;
			long num3 = 0L;
			if (num < 0L)
			{
				flag = true;
				num = -num;
			}
			if (num2 < 0L)
			{
				flag = !flag;
				num2 = -num2;
			}
			long num4 = num & SqlInt64.s_lLowIntMask;
			long num5 = (num >> 32) & SqlInt64.s_lLowIntMask;
			long num6 = num2 & SqlInt64.s_lLowIntMask;
			long num7 = (num2 >> 32) & SqlInt64.s_lLowIntMask;
			if (num5 != 0L && num7 != 0L)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			long num8 = num4 * num6;
			if (num8 < 0L)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			if (num5 != 0L)
			{
				num3 = num5 * num6;
				if (num3 < 0L || num3 > 9223372036854775807L)
				{
					throw new OverflowException(SQLResource.ArithOverflowMessage);
				}
			}
			else if (num7 != 0L)
			{
				num3 = num4 * num7;
				if (num3 < 0L || num3 > 9223372036854775807L)
				{
					throw new OverflowException(SQLResource.ArithOverflowMessage);
				}
			}
			num8 += num3 << 32;
			if (num8 < 0L)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			if (flag)
			{
				num8 = -num8;
			}
			return new SqlInt64(num8);
		}

		/// <summary>Divides the first <see cref="T:System.Data.SqlTypes.SqlInt64" /> parameter by the second.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt64.Value" /> property contains the results of the division operation.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure. </param>
		// Token: 0x06000A87 RID: 2695 RVA: 0x0003C940 File Offset: 0x0003AB40
		public static SqlInt64 operator /(SqlInt64 x, SqlInt64 y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlInt64.Null;
			}
			if (y.m_value == 0L)
			{
				throw new DivideByZeroException(SQLResource.DivideByZeroMessage);
			}
			if (x.m_value == -9223372036854775808L && y.m_value == -1L)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return new SqlInt64(x.m_value / y.m_value);
		}

		/// <summary>Computes the remainder after dividing the first <see cref="T:System.Data.SqlTypes.SqlInt64" /> parameter by the second.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt64.Value" /> property contains the remainder.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure. </param>
		// Token: 0x06000A88 RID: 2696 RVA: 0x0003C9B0 File Offset: 0x0003ABB0
		public static SqlInt64 operator %(SqlInt64 x, SqlInt64 y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlInt64.Null;
			}
			if (y.m_value == 0L)
			{
				throw new DivideByZeroException(SQLResource.DivideByZeroMessage);
			}
			if (x.m_value == -9223372036854775808L && y.m_value == -1L)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return new SqlInt64(x.m_value % y.m_value);
		}

		/// <summary>Converts the supplied <see cref="T:System.Data.SqlTypes.SqlByte" /> parameter to <see cref="T:System.Data.SqlTypes.SqlInt64" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt64.Value" /> property equals the <see cref="P:System.Data.SqlTypes.SqlByte.Value" /> property of the <see cref="T:System.Data.SqlTypes.SqlByte" /> parameter.</returns>
		/// <param name="x">The <see cref="T:System.Data.SqlTypes.SqlByte" /> structure to be converted. </param>
		// Token: 0x06000A89 RID: 2697 RVA: 0x0003CA20 File Offset: 0x0003AC20
		public static implicit operator SqlInt64(SqlByte x)
		{
			if (!x.IsNull)
			{
				return new SqlInt64((long)((ulong)x.Value));
			}
			return SqlInt64.Null;
		}

		/// <summary>Converts the supplied <see cref="T:System.Data.SqlTypes.SqlInt16" /> parameter to <see cref="T:System.Data.SqlTypes.SqlInt64" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt64.Value" /> property equals the <see cref="P:System.Data.SqlTypes.SqlInt16.Value" /> property of the <see cref="T:System.Data.SqlTypes.SqlInt16" /> parameter.</returns>
		/// <param name="x">The <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure to be converted. </param>
		// Token: 0x06000A8A RID: 2698 RVA: 0x0003CA3E File Offset: 0x0003AC3E
		public static implicit operator SqlInt64(SqlInt16 x)
		{
			if (!x.IsNull)
			{
				return new SqlInt64((long)x.Value);
			}
			return SqlInt64.Null;
		}

		/// <summary>Converts the supplied <see cref="T:System.Data.SqlTypes.SqlInt32" /> parameter to <see cref="T:System.Data.SqlTypes.SqlInt64" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt64.Value" /> property equals the <see cref="P:System.Data.SqlTypes.SqlInt32.Value" /> property of the <see cref="T:System.Data.SqlTypes.SqlInt32" /> parameter.</returns>
		/// <param name="x">The <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure to be converted. </param>
		// Token: 0x06000A8B RID: 2699 RVA: 0x0003CA5C File Offset: 0x0003AC5C
		public static implicit operator SqlInt64(SqlInt32 x)
		{
			if (!x.IsNull)
			{
				return new SqlInt64((long)x.Value);
			}
			return SqlInt64.Null;
		}

		/// <summary>Converts the supplied <see cref="T:System.Data.SqlTypes.SqlDecimal" /> parameter to <see cref="T:System.Data.SqlTypes.SqlInt64" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt64.Value" /> is equal to the integer part of the <see cref="T:System.Data.SqlTypes.SqlDecimal" /> parameter.</returns>
		/// <param name="x">The <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure to be converted. </param>
		// Token: 0x06000A8C RID: 2700 RVA: 0x0003CA7C File Offset: 0x0003AC7C
		public static explicit operator SqlInt64(SqlDecimal x)
		{
			if (x.IsNull)
			{
				return SqlInt64.Null;
			}
			SqlDecimal sqlDecimal = x;
			sqlDecimal.AdjustScale((int)(-(int)sqlDecimal._bScale), false);
			if (sqlDecimal._bLen > 2)
			{
				throw new OverflowException(SQLResource.ConversionOverflowMessage);
			}
			long num2;
			if (sqlDecimal._bLen == 2)
			{
				ulong num = SqlDecimal.DWL(sqlDecimal._data1, sqlDecimal._data2);
				if (num > SqlDecimal.s_llMax && (sqlDecimal.IsPositive || num != 1UL + SqlDecimal.s_llMax))
				{
					throw new OverflowException(SQLResource.ConversionOverflowMessage);
				}
				num2 = (long)num;
			}
			else
			{
				num2 = (long)((ulong)sqlDecimal._data1);
			}
			if (!sqlDecimal.IsPositive)
			{
				num2 = -num2;
			}
			return new SqlInt64(num2);
		}

		// Token: 0x06000A8D RID: 2701 RVA: 0x0003CB1D File Offset: 0x0003AD1D
		private static bool SameSignLong(long x, long y)
		{
			return ((x ^ y) & long.MinValue) == 0L;
		}

		/// <summary>Performs a logical comparison of the two <see cref="T:System.Data.SqlTypes.SqlInt64" /> parameters to determine whether they are equal.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the two instances are equal or <see cref="F:System.Data.SqlTypes.SqlBoolean.False" /> if the two instances are not equal. If either instance of <see cref="T:System.Data.SqlTypes.SqlInt64" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure. </param>
		// Token: 0x06000A8E RID: 2702 RVA: 0x0003CB30 File Offset: 0x0003AD30
		public static SqlBoolean operator ==(SqlInt64 x, SqlInt64 y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value == y.m_value);
			}
			return SqlBoolean.Null;
		}

		/// <summary>Performs a logical comparison on the two <see cref="T:System.Data.SqlTypes.SqlInt64" /> parameters to determine whether the first is less than the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is less than the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlInt64" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure. </param>
		// Token: 0x06000A8F RID: 2703 RVA: 0x0003CB5D File Offset: 0x0003AD5D
		public static SqlBoolean operator <(SqlInt64 x, SqlInt64 y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value < y.m_value);
			}
			return SqlBoolean.Null;
		}

		/// <summary>Performs a logical comparison of the two <see cref="T:System.Data.SqlTypes.SqlInt64" /> parameters to determine whether the first is greater than the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is greater than the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlInt64" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure. </param>
		// Token: 0x06000A90 RID: 2704 RVA: 0x0003CB8A File Offset: 0x0003AD8A
		public static SqlBoolean operator >(SqlInt64 x, SqlInt64 y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value > y.m_value);
			}
			return SqlBoolean.Null;
		}

		/// <summary>Performs a logical comparison on the two <see cref="T:System.Data.SqlTypes.SqlInt64" /> parameters to determine whether the first is less than the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is less than the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlInt64" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure. </param>
		// Token: 0x06000A91 RID: 2705 RVA: 0x0003CBB7 File Offset: 0x0003ADB7
		public static SqlBoolean LessThan(SqlInt64 x, SqlInt64 y)
		{
			return x < y;
		}

		/// <summary>Performs a logical comparison of the two <see cref="T:System.Data.SqlTypes.SqlInt64" /> parameters to determine whether the first is greater than the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is greater than the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlInt64" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure. </param>
		// Token: 0x06000A92 RID: 2706 RVA: 0x0003CBC0 File Offset: 0x0003ADC0
		public static SqlBoolean GreaterThan(SqlInt64 x, SqlInt64 y)
		{
			return x > y;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure to <see cref="T:System.Data.SqlTypes.SqlByte" />.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure whose Value equals the Value of this <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure. </returns>
		// Token: 0x06000A93 RID: 2707 RVA: 0x0003CBC9 File Offset: 0x0003ADC9
		public SqlByte ToSqlByte()
		{
			return (SqlByte)this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure to <see cref="T:System.Data.SqlTypes.SqlDouble" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlDouble" /> equal to the value of this <see cref="T:System.Data.SqlTypes.SqlInt64" />.</returns>
		// Token: 0x06000A94 RID: 2708 RVA: 0x0003CBD6 File Offset: 0x0003ADD6
		public SqlDouble ToSqlDouble()
		{
			return this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure to <see cref="T:System.Data.SqlTypes.SqlInt16" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlInt16" /> equal to the value of this <see cref="T:System.Data.SqlTypes.SqlInt64" />.</returns>
		// Token: 0x06000A95 RID: 2709 RVA: 0x0003CBE3 File Offset: 0x0003ADE3
		public SqlInt16 ToSqlInt16()
		{
			return (SqlInt16)this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure to <see cref="T:System.Data.SqlTypes.SqlInt32" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlInt64" /> equal to the value of this <see cref="T:System.Data.SqlTypes.SqlInt64" />.</returns>
		// Token: 0x06000A96 RID: 2710 RVA: 0x0003CBF0 File Offset: 0x0003ADF0
		public SqlInt32 ToSqlInt32()
		{
			return (SqlInt32)this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure to <see cref="T:System.Data.SqlTypes.SqlDecimal" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlDecimal" /> equal to the value of this <see cref="T:System.Data.SqlTypes.SqlInt64" />.</returns>
		// Token: 0x06000A97 RID: 2711 RVA: 0x0003CBFD File Offset: 0x0003ADFD
		public SqlDecimal ToSqlDecimal()
		{
			return this;
		}

		/// <summary>Compares this <see cref="T:System.Data.SqlTypes.SqlInt64" /> instance to the supplied <see cref="T:System.Object" /> and returns an indication of their relative values.</summary>
		/// <returns>A signed number that indicates the relative values of the instance and the object.Return value Condition Less than zero This instance is less than the object. Zero This instance is the same as the object. Greater than zero This instance is greater than the object -or- The object is a null reference (Nothing in Visual Basic). </returns>
		/// <param name="value">The <see cref="T:System.Object" /> to be compared. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000A98 RID: 2712 RVA: 0x0003CC0C File Offset: 0x0003AE0C
		public int CompareTo(object value)
		{
			if (value is SqlInt64)
			{
				SqlInt64 sqlInt = (SqlInt64)value;
				return this.CompareTo(sqlInt);
			}
			throw ADP.WrongType(value.GetType(), typeof(SqlInt64));
		}

		/// <summary>Compares this <see cref="T:System.Data.SqlTypes.SqlInt64" /> instance to the supplied <see cref="T:System.Data.SqlTypes.SqlInt64" /> and returns an indication of their relative values.</summary>
		/// <returns>A signed number that indicates the relative values of the instance and the object.Return value Condition Less than zero This instance is less than the object. Zero This instance is the same as the object. Greater than zero This instance is greater than the object -or- The object is a null reference (Nothing in Visual Basic). </returns>
		/// <param name="value">The <see cref="T:System.Data.SqlTypes.SqlInt64" /> to be compared.</param>
		// Token: 0x06000A99 RID: 2713 RVA: 0x0003CC48 File Offset: 0x0003AE48
		public int CompareTo(SqlInt64 value)
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

		/// <summary>Compares the supplied object parameter to the <see cref="P:System.Data.SqlTypes.SqlInt64.Value" /> property of the <see cref="T:System.Data.SqlTypes.SqlInt64" /> object.</summary>
		/// <returns>true if object is an instance of <see cref="T:System.Data.SqlTypes.SqlInt64" /> and the two are equal; otherwise false.</returns>
		/// <param name="value">The object to be compared. </param>
		// Token: 0x06000A9A RID: 2714 RVA: 0x0003CCA0 File Offset: 0x0003AEA0
		public override bool Equals(object value)
		{
			if (!(value is SqlInt64))
			{
				return false;
			}
			SqlInt64 sqlInt = (SqlInt64)value;
			if (sqlInt.IsNull || this.IsNull)
			{
				return sqlInt.IsNull && this.IsNull;
			}
			return (this == sqlInt).Value;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06000A9B RID: 2715 RVA: 0x0003CCF8 File Offset: 0x0003AEF8
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
		// Token: 0x06000A9C RID: 2716 RVA: 0x00011F10 File Offset: 0x00010110
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <param name="reader">XmlReader</param>
		// Token: 0x06000A9D RID: 2717 RVA: 0x0003CD20 File Offset: 0x0003AF20
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			string attribute = reader.GetAttribute("nil", "http://www.w3.org/2001/XMLSchema-instance");
			if (attribute != null && XmlConvert.ToBoolean(attribute))
			{
				reader.ReadElementString();
				this.m_fNotNull = false;
				return;
			}
			this.m_value = XmlConvert.ToInt64(reader.ReadElementString());
			this.m_fNotNull = true;
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <param name="writer">XmlWriter</param>
		// Token: 0x06000A9E RID: 2718 RVA: 0x0003CD70 File Offset: 0x0003AF70
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
		/// <returns>A string that indicates the XSD of the specified <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</returns>
		/// <param name="schemaSet">An <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</param>
		// Token: 0x06000A9F RID: 2719 RVA: 0x0003CDA6 File Offset: 0x0003AFA6
		public static XmlQualifiedName GetXsdType(XmlSchemaSet schemaSet)
		{
			return new XmlQualifiedName("long", "http://www.w3.org/2001/XMLSchema");
		}

		// Token: 0x0400044A RID: 1098
		private bool m_fNotNull;

		// Token: 0x0400044B RID: 1099
		private long m_value;

		// Token: 0x0400044C RID: 1100
		private static readonly long s_lLowIntMask = (long)((ulong)(-1));

		// Token: 0x0400044D RID: 1101
		private static readonly long s_lHighIntMask = -4294967296L;

		/// <summary>Represents a <see cref="T:System.DBNull" /> that can be assigned to this instance of the <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure.</summary>
		// Token: 0x0400044E RID: 1102
		public static readonly SqlInt64 Null = new SqlInt64(true);

		/// <summary>Represents a zero value that can be assigned to the <see cref="P:System.Data.SqlTypes.SqlInt64.Value" /> property of an instance of the <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure.</summary>
		// Token: 0x0400044F RID: 1103
		public static readonly SqlInt64 Zero = new SqlInt64(0L);

		/// <summary>A constant representing the smallest possible value for a <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure.</summary>
		// Token: 0x04000450 RID: 1104
		public static readonly SqlInt64 MinValue = new SqlInt64(long.MinValue);

		/// <summary>A constant representing the largest possible value for a <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure.</summary>
		// Token: 0x04000451 RID: 1105
		public static readonly SqlInt64 MaxValue = new SqlInt64(long.MaxValue);
	}
}
