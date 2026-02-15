using System;
using System.Data.Common;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Data.SqlTypes
{
	/// <summary>Represents a numeric value between - 10^38 +1 and 10^38 - 1, with fixed precision and scale. </summary>
	// Token: 0x020000C5 RID: 197
	[XmlSchemaProvider("GetXsdType")]
	[Serializable]
	public struct SqlDecimal : INullable, IComparable, IXmlSerializable
	{
		// Token: 0x060009C5 RID: 2501 RVA: 0x0003918C File Offset: 0x0003738C
		private byte CalculatePrecision()
		{
			int num;
			uint[] array;
			uint num2;
			if (this._data4 != 0U)
			{
				num = 33;
				array = SqlDecimal.s_decimalHelpersHiHi;
				num2 = this._data4;
			}
			else if (this._data3 != 0U)
			{
				num = 24;
				array = SqlDecimal.s_decimalHelpersHi;
				num2 = this._data3;
			}
			else if (this._data2 != 0U)
			{
				num = 15;
				array = SqlDecimal.s_decimalHelpersMid;
				num2 = this._data2;
			}
			else
			{
				num = 5;
				array = SqlDecimal.s_decimalHelpersLo;
				num2 = this._data1;
			}
			if (num2 < array[num])
			{
				num -= 2;
				if (num2 < array[num])
				{
					num -= 2;
					if (num2 < array[num])
					{
						num--;
					}
					else
					{
						num++;
					}
				}
				else
				{
					num++;
				}
			}
			else
			{
				num += 2;
				if (num2 < array[num])
				{
					num--;
				}
				else
				{
					num++;
				}
			}
			if (num2 >= array[num])
			{
				num++;
				if (num == 37 && num2 >= array[num])
				{
					num++;
				}
			}
			byte b = (byte)(num + 1);
			if (b > 1 && this.VerifyPrecision(b - 1))
			{
				b -= 1;
			}
			return Math.Max(b, this._bScale);
		}

		// Token: 0x060009C6 RID: 2502 RVA: 0x00039278 File Offset: 0x00037478
		private bool VerifyPrecision(byte precision)
		{
			int num = (int)(checked(precision - 1));
			if (this._data4 < SqlDecimal.s_decimalHelpersHiHi[num])
			{
				return true;
			}
			if (this._data4 == SqlDecimal.s_decimalHelpersHiHi[num])
			{
				if (this._data3 < SqlDecimal.s_decimalHelpersHi[num])
				{
					return true;
				}
				if (this._data3 == SqlDecimal.s_decimalHelpersHi[num])
				{
					if (this._data2 < SqlDecimal.s_decimalHelpersMid[num])
					{
						return true;
					}
					if (this._data2 == SqlDecimal.s_decimalHelpersMid[num] && this._data1 < SqlDecimal.s_decimalHelpersLo[num])
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060009C7 RID: 2503 RVA: 0x000392FC File Offset: 0x000374FC
		private SqlDecimal(bool fNull)
		{
			this._bLen = (this._bPrec = (this._bScale = 0));
			this._bStatus = 0;
			this._data1 = (this._data2 = (this._data3 = (this._data4 = SqlDecimal.s_uiZero)));
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure using the supplied <see cref="T:System.Decimal" /> value.</summary>
		/// <param name="value">The <see cref="T:System.Decimal" /> value to be stored as a <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. </param>
		// Token: 0x060009C8 RID: 2504 RVA: 0x00039350 File Offset: 0x00037550
		public SqlDecimal(decimal value)
		{
			this._bStatus = SqlDecimal.s_bNotNull;
			int[] bits = decimal.GetBits(value);
			uint num = (uint)bits[3];
			this._data1 = (uint)bits[0];
			this._data2 = (uint)bits[1];
			this._data3 = (uint)bits[2];
			this._data4 = SqlDecimal.s_uiZero;
			this._bStatus |= (((num & 2147483648U) == 2147483648U) ? SqlDecimal.s_bNegative : 0);
			if (this._data3 != 0U)
			{
				this._bLen = 3;
			}
			else if (this._data2 != 0U)
			{
				this._bLen = 2;
			}
			else
			{
				this._bLen = 1;
			}
			this._bScale = (byte)((int)(num & 16711680U) >> 16);
			this._bPrec = 0;
			this._bPrec = this.CalculatePrecision();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure using the supplied integer value.</summary>
		/// <param name="value">The supplied integer value which will the used as the value of the new <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. </param>
		// Token: 0x060009C9 RID: 2505 RVA: 0x0003940C File Offset: 0x0003760C
		public SqlDecimal(int value)
		{
			this._bStatus = SqlDecimal.s_bNotNull;
			uint num = (uint)value;
			if (value < 0)
			{
				this._bStatus |= SqlDecimal.s_bNegative;
				if (value != -2147483648)
				{
					num = (uint)(-(uint)value);
				}
			}
			this._data1 = num;
			this._data2 = (this._data3 = (this._data4 = SqlDecimal.s_uiZero));
			this._bLen = 1;
			this._bPrec = SqlDecimal.BGetPrecUI4(this._data1);
			this._bScale = 0;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure using the supplied long integer value.</summary>
		/// <param name="value">The supplied long integer value which will the used as the value of the new <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. </param>
		// Token: 0x060009CA RID: 2506 RVA: 0x0003948C File Offset: 0x0003768C
		public SqlDecimal(long value)
		{
			this._bStatus = SqlDecimal.s_bNotNull;
			ulong num = (ulong)value;
			if (value < 0L)
			{
				this._bStatus |= SqlDecimal.s_bNegative;
				if (value != -9223372036854775808L)
				{
					num = (ulong)(-(ulong)value);
				}
			}
			this._data1 = (uint)num;
			this._data2 = (uint)(num >> 32);
			this._data3 = (this._data4 = 0U);
			this._bLen = ((this._data2 == 0U) ? 1 : 2);
			this._bPrec = SqlDecimal.BGetPrecUI8(num);
			this._bScale = 0;
		}

		// Token: 0x060009CB RID: 2507 RVA: 0x00039518 File Offset: 0x00037718
		private SqlDecimal(uint[] rglData, byte bLen, byte bPrec, byte bScale, bool fPositive)
		{
			SqlDecimal.CheckValidPrecScale(bPrec, bScale);
			this._bLen = bLen;
			this._bPrec = bPrec;
			this._bScale = bScale;
			this._data1 = rglData[0];
			this._data2 = rglData[1];
			this._data3 = rglData[2];
			this._data4 = rglData[3];
			this._bStatus = SqlDecimal.s_bNotNull;
			if (!fPositive)
			{
				this._bStatus |= SqlDecimal.s_bNegative;
			}
			if (this.FZero())
			{
				this.SetPositive();
			}
		}

		/// <summary>Indicates whether this <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure is null.</summary>
		/// <returns>true if this <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure is null. Otherwise, false. </returns>
		// Token: 0x1700018B RID: 395
		// (get) Token: 0x060009CC RID: 2508 RVA: 0x00039597 File Offset: 0x00037797
		public bool IsNull
		{
			get
			{
				return (this._bStatus & SqlDecimal.s_bNullMask) == SqlDecimal.s_bIsNull;
			}
		}

		/// <summary>Gets the value of the <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. This property is read-only.</summary>
		/// <returns>A number in the range -79,228,162,514,264,337,593,543,950,335 through 79,228,162,514,162,514,264,337,593,543,950,335.</returns>
		// Token: 0x1700018C RID: 396
		// (get) Token: 0x060009CD RID: 2509 RVA: 0x000395AC File Offset: 0x000377AC
		public decimal Value
		{
			get
			{
				return this.ToDecimal();
			}
		}

		/// <summary>Indicates whether the <see cref="P:System.Data.SqlTypes.SqlDecimal.Value" /> of this <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure is greater than zero.</summary>
		/// <returns>true if the <see cref="P:System.Data.SqlTypes.SqlDecimal.Value" /> is assigned to null. Otherwise, false.</returns>
		// Token: 0x1700018D RID: 397
		// (get) Token: 0x060009CE RID: 2510 RVA: 0x000395B4 File Offset: 0x000377B4
		public bool IsPositive
		{
			get
			{
				if (this.IsNull)
				{
					throw new SqlNullValueException();
				}
				return (this._bStatus & SqlDecimal.s_bSignMask) == SqlDecimal.s_bPositive;
			}
		}

		// Token: 0x060009CF RID: 2511 RVA: 0x000395D7 File Offset: 0x000377D7
		private void SetPositive()
		{
			this._bStatus &= SqlDecimal.s_bReverseSignMask;
		}

		// Token: 0x060009D0 RID: 2512 RVA: 0x000395EC File Offset: 0x000377EC
		private void SetSignBit(bool fPositive)
		{
			this._bStatus = (fPositive ? (this._bStatus & SqlDecimal.s_bReverseSignMask) : (this._bStatus | SqlDecimal.s_bNegative));
		}

		/// <summary>Gets the number of decimal places to which <see cref="P:System.Data.SqlTypes.SqlDecimal.Value" /> is resolved.</summary>
		/// <returns>The number of decimal places to which the Value property is resolved.</returns>
		// Token: 0x1700018E RID: 398
		// (get) Token: 0x060009D1 RID: 2513 RVA: 0x00039612 File Offset: 0x00037812
		public byte Scale
		{
			get
			{
				if (this.IsNull)
				{
					throw new SqlNullValueException();
				}
				return this._bScale;
			}
		}

		/// <summary>Gets the binary representation of this <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure as an array of integers.</summary>
		/// <returns>An array of integers that contains the binary representation of this <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure.</returns>
		// Token: 0x1700018F RID: 399
		// (get) Token: 0x060009D2 RID: 2514 RVA: 0x00039628 File Offset: 0x00037828
		public int[] Data
		{
			get
			{
				if (this.IsNull)
				{
					throw new SqlNullValueException();
				}
				return new int[]
				{
					(int)this._data1,
					(int)this._data2,
					(int)this._data3,
					(int)this._data4
				};
			}
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure to <see cref="T:System.String" />.</summary>
		/// <returns>A new <see cref="T:System.String" /> object that contains the string representation of the <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure's <see cref="P:System.Data.SqlTypes.SqlDecimal.Value" /> property.</returns>
		// Token: 0x060009D3 RID: 2515 RVA: 0x00039664 File Offset: 0x00037864
		public override string ToString()
		{
			if (this.IsNull)
			{
				return SQLResource.NullString;
			}
			uint[] array = new uint[] { this._data1, this._data2, this._data3, this._data4 };
			int bLen = (int)this._bLen;
			char[] array2 = new char[(int)(SqlDecimal.s_NUMERIC_MAX_PRECISION + 1)];
			int i = 0;
			while (bLen > 1 || array[0] != 0U)
			{
				uint num;
				SqlDecimal.MpDiv1(array, ref bLen, SqlDecimal.s_ulBase10, out num);
				array2[i++] = SqlDecimal.ChFromDigit(num);
			}
			while (i <= (int)this._bScale)
			{
				array2[i++] = SqlDecimal.ChFromDigit(0U);
			}
			int num2 = 0;
			int num3 = 0;
			if (this._bScale > 0)
			{
				num2 = 1;
			}
			char[] array3;
			if (this.IsPositive)
			{
				array3 = new char[num2 + i];
			}
			else
			{
				array3 = new char[num2 + i + 1];
				array3[num3++] = '-';
			}
			while (i > 0)
			{
				if (i-- == (int)this._bScale)
				{
					array3[num3++] = '.';
				}
				array3[num3++] = array2[i];
			}
			return new string(array3);
		}

		/// <summary>Converts the <see cref="T:System.String" /> representation of a number to its <see cref="T:System.Data.SqlTypes.SqlDecimal" /> equivalent.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlDecimal" /> equivalent to the value that is contained in the specified <see cref="T:System.String" />.</returns>
		/// <param name="s">The String to be parsed. </param>
		// Token: 0x060009D4 RID: 2516 RVA: 0x00039774 File Offset: 0x00037974
		public static SqlDecimal Parse(string s)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (s == SQLResource.NullString)
			{
				return SqlDecimal.Null;
			}
			SqlDecimal @null = SqlDecimal.Null;
			char[] array = s.ToCharArray();
			int num = array.Length;
			int num2 = -1;
			int num3 = 0;
			@null._bPrec = 1;
			@null._bScale = 0;
			@null.SetToZero();
			while (num != 0 && array[num - 1] == ' ')
			{
				num--;
			}
			if (num == 0)
			{
				throw new FormatException(SQLResource.FormatMessage);
			}
			while (array[num3] == ' ')
			{
				num3++;
				num--;
			}
			if (array[num3] == '-')
			{
				@null.SetSignBit(false);
				num3++;
				num--;
			}
			else
			{
				@null.SetSignBit(true);
				if (array[num3] == '+')
				{
					num3++;
					num--;
				}
			}
			while (num > 2 && array[num3] == '0')
			{
				num3++;
				num--;
			}
			if (2 == num && '0' == array[num3] && '.' == array[num3 + 1])
			{
				array[num3] = '.';
				array[num3 + 1] = '0';
			}
			if (num == 0 || num > (int)(SqlDecimal.s_NUMERIC_MAX_PRECISION + 1))
			{
				throw new FormatException(SQLResource.FormatMessage);
			}
			while (num > 1 && array[num3] == '0')
			{
				num3++;
				num--;
			}
			int i;
			for (i = 0; i < num; i++)
			{
				char c = array[num3];
				num3++;
				if (c >= '0' && c <= '9')
				{
					c -= '0';
					@null.MultByULong(SqlDecimal.s_ulBase10);
					@null.AddULong((uint)c);
				}
				else
				{
					if (c != '.' || num2 >= 0)
					{
						throw new FormatException(SQLResource.FormatMessage);
					}
					num2 = i;
				}
			}
			if (num2 < 0)
			{
				@null._bPrec = (byte)i;
				@null._bScale = 0;
			}
			else
			{
				@null._bPrec = (byte)(i - 1);
				@null._bScale = (byte)((int)@null._bPrec - num2);
			}
			if (@null._bPrec > SqlDecimal.s_NUMERIC_MAX_PRECISION)
			{
				throw new FormatException(SQLResource.FormatMessage);
			}
			if (@null._bPrec == 0)
			{
				throw new FormatException(SQLResource.FormatMessage);
			}
			if (@null.FZero())
			{
				@null.SetPositive();
			}
			return @null;
		}

		/// <summary>Returns the a double equal to the contents of the <see cref="P:System.Data.SqlTypes.SqlDecimal.Value" /> property of this instance.</summary>
		/// <returns>The decimal representation of the <see cref="P:System.Data.SqlTypes.SqlDecimal.Value" /> property.</returns>
		// Token: 0x060009D5 RID: 2517 RVA: 0x00039970 File Offset: 0x00037B70
		public double ToDouble()
		{
			if (this.IsNull)
			{
				throw new SqlNullValueException();
			}
			double num = this._data4;
			num = num * (double)SqlDecimal.s_lInt32Base + this._data3;
			num = num * (double)SqlDecimal.s_lInt32Base + this._data2;
			num = num * (double)SqlDecimal.s_lInt32Base + this._data1;
			num /= Math.Pow(10.0, (double)this._bScale);
			if (!this.IsPositive)
			{
				return -num;
			}
			return num;
		}

		// Token: 0x060009D6 RID: 2518 RVA: 0x000399F8 File Offset: 0x00037BF8
		private decimal ToDecimal()
		{
			if (this.IsNull)
			{
				throw new SqlNullValueException();
			}
			if (this._data4 != 0U || this._bScale > 28)
			{
				throw new OverflowException(SQLResource.ConversionOverflowMessage);
			}
			return new decimal((int)this._data1, (int)this._data2, (int)this._data3, !this.IsPositive, this._bScale);
		}

		/// <summary>Converts the <see cref="T:System.Decimal" /> value to <see cref="T:System.Data.SqlTypes.SqlDecimal" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure whose <see cref="P:System.Data.SqlTypes.SqlDecimal.Value" /> property equals the value of the Decimal parameter.</returns>
		/// <param name="x">The <see cref="T:System.Decimal" /> value to be converted. </param>
		// Token: 0x060009D7 RID: 2519 RVA: 0x00039A56 File Offset: 0x00037C56
		public static implicit operator SqlDecimal(decimal x)
		{
			return new SqlDecimal(x);
		}

		/// <summary>Converts the supplied <see cref="T:System.Int64" /> structure to <see cref="T:System.Data.SqlTypes.SqlDecimal" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure whose <see cref="P:System.Data.SqlTypes.SqlDecimal.Value" /> property equals the value of the <see cref="T:System.Int64" /> parameter.</returns>
		/// <param name="x">The <see cref="T:System.Int64" /> structure to be converted.</param>
		// Token: 0x060009D8 RID: 2520 RVA: 0x00039A5E File Offset: 0x00037C5E
		public static implicit operator SqlDecimal(long x)
		{
			return new SqlDecimal(new decimal(x));
		}

		/// <summary>The unary minus operator negates the <see cref="T:System.Data.SqlTypes.SqlDecimal" /> parameter.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure whose value contains the results of the negation.</returns>
		/// <param name="x">The <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure to be negated. </param>
		// Token: 0x060009D9 RID: 2521 RVA: 0x00039A6C File Offset: 0x00037C6C
		public static SqlDecimal operator -(SqlDecimal x)
		{
			if (x.IsNull)
			{
				return SqlDecimal.Null;
			}
			SqlDecimal sqlDecimal = x;
			if (sqlDecimal.FZero())
			{
				sqlDecimal.SetPositive();
			}
			else
			{
				sqlDecimal.SetSignBit(!sqlDecimal.IsPositive);
			}
			return sqlDecimal;
		}

		/// <summary>Calculates the sum of the two <see cref="T:System.Data.SqlTypes.SqlDecimal" /> operators.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure whose <see cref="P:System.Data.SqlTypes.SqlDecimal.Value" /> property contains the sum.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. </param>
		// Token: 0x060009DA RID: 2522 RVA: 0x00039AB0 File Offset: 0x00037CB0
		public static SqlDecimal operator +(SqlDecimal x, SqlDecimal y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlDecimal.Null;
			}
			bool flag = true;
			bool flag2 = x.IsPositive;
			bool flag3 = y.IsPositive;
			int bScale = (int)x._bScale;
			int bScale2 = (int)y._bScale;
			int num = Math.Max((int)x._bPrec - bScale, (int)y._bPrec - bScale2);
			int num2 = Math.Max(bScale, bScale2);
			int num3 = num + num2 + 1;
			num3 = Math.Min((int)SqlDecimal.MaxPrecision, num3);
			if (num3 - num < num2)
			{
				num2 = num3 - num;
			}
			if (bScale != num2)
			{
				x.AdjustScale(num2 - bScale, true);
			}
			if (bScale2 != num2)
			{
				y.AdjustScale(num2 - bScale2, true);
			}
			if (!flag2)
			{
				flag2 = !flag2;
				flag3 = !flag3;
				flag = !flag;
			}
			int num4 = (int)x._bLen;
			int num5 = (int)y._bLen;
			uint[] array = new uint[] { x._data1, x._data2, x._data3, x._data4 };
			uint[] array2 = new uint[] { y._data1, y._data2, y._data3, y._data4 };
			byte b;
			if (flag3)
			{
				ulong num6 = 0UL;
				int num7 = 0;
				while (num7 < num4 || num7 < num5)
				{
					if (num7 < num4)
					{
						num6 += (ulong)array[num7];
					}
					if (num7 < num5)
					{
						num6 += (ulong)array2[num7];
					}
					array[num7] = (uint)num6;
					num6 >>= 32;
					num7++;
				}
				if (num6 != 0UL)
				{
					if (num7 == SqlDecimal.s_cNumeMax)
					{
						throw new OverflowException(SQLResource.ArithOverflowMessage);
					}
					array[num7] = (uint)num6;
					num7++;
				}
				b = (byte)num7;
			}
			else
			{
				int num8 = 0;
				if (x.LAbsCmp(y) < 0)
				{
					flag = !flag;
					uint[] array3 = array2;
					array2 = array;
					array = array3;
					num4 = num5;
					num5 = (int)x._bLen;
				}
				ulong num6 = SqlDecimal.s_ulInt32Base;
				int num7 = 0;
				while (num7 < num4 || num7 < num5)
				{
					if (num7 < num4)
					{
						num6 += (ulong)array[num7];
					}
					if (num7 < num5)
					{
						num6 -= (ulong)array2[num7];
					}
					array[num7] = (uint)num6;
					if (array[num7] != 0U)
					{
						num8 = num7;
					}
					num6 >>= 32;
					num6 += SqlDecimal.s_ulInt32BaseForMod;
					num7++;
				}
				b = (byte)(num8 + 1);
			}
			SqlDecimal sqlDecimal = new SqlDecimal(array, b, (byte)num3, (byte)num2, flag);
			if (sqlDecimal.FGt10_38() || sqlDecimal.CalculatePrecision() > SqlDecimal.s_NUMERIC_MAX_PRECISION)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			if (sqlDecimal.FZero())
			{
				sqlDecimal.SetPositive();
			}
			return sqlDecimal;
		}

		/// <summary>Calculates the results of subtracting the second <see cref="T:System.Data.SqlTypes.SqlDecimal" /> operand from the first.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure whose Value property contains the results of the subtraction.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. </param>
		// Token: 0x060009DB RID: 2523 RVA: 0x00039D25 File Offset: 0x00037F25
		public static SqlDecimal operator -(SqlDecimal x, SqlDecimal y)
		{
			return x + -y;
		}

		/// <summary>The multiplication operator computes the product of the two <see cref="T:System.Data.SqlTypes.SqlDecimal" /> parameters.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure whose <see cref="P:System.Data.SqlTypes.SqlDecimal.Value" /> property contains the product of the multiplication.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. </param>
		// Token: 0x060009DC RID: 2524 RVA: 0x00039D34 File Offset: 0x00037F34
		public static SqlDecimal operator *(SqlDecimal x, SqlDecimal y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlDecimal.Null;
			}
			int bLen = (int)y._bLen;
			int num = (int)(x._bScale + y._bScale);
			int num2 = num;
			int num3 = (int)(x._bPrec - x._bScale + (y._bPrec - y._bScale) + 1);
			int num4 = num2 + num3;
			if (num4 > (int)SqlDecimal.s_NUMERIC_MAX_PRECISION)
			{
				num4 = (int)SqlDecimal.s_NUMERIC_MAX_PRECISION;
			}
			if (num2 > (int)SqlDecimal.s_NUMERIC_MAX_PRECISION)
			{
				num2 = (int)SqlDecimal.s_NUMERIC_MAX_PRECISION;
			}
			num2 = Math.Min(num4 - num3, num2);
			num2 = Math.Max(num2, Math.Min(num, (int)SqlDecimal.s_cNumeDivScaleMin));
			int num5 = num2 - num;
			bool flag = x.IsPositive == y.IsPositive;
			uint[] array = new uint[] { x._data1, x._data2, x._data3, x._data4 };
			uint[] array2 = new uint[] { y._data1, y._data2, y._data3, y._data4 };
			uint[] array3 = new uint[9];
			int i = 0;
			for (int j = 0; j < (int)x._bLen; j++)
			{
				uint num6 = array[j];
				ulong num7 = 0UL;
				i = j;
				for (int k = 0; k < bLen; k++)
				{
					ulong num8 = num7 + (ulong)array3[i];
					ulong num9 = (ulong)array2[k];
					num7 = (ulong)num6 * num9;
					num7 += num8;
					if (num7 < num8)
					{
						num8 = SqlDecimal.s_ulInt32Base;
					}
					else
					{
						num8 = 0UL;
					}
					array3[i++] = (uint)num7;
					num7 = (num7 >> 32) + num8;
				}
				if (num7 != 0UL)
				{
					array3[i++] = (uint)num7;
				}
			}
			while (array3[i] == 0U && i > 0)
			{
				i--;
			}
			int num10 = i + 1;
			if (num5 != 0)
			{
				if (num5 < 0)
				{
					uint num11;
					uint num12;
					do
					{
						if (num5 <= -9)
						{
							num11 = SqlDecimal.s_rgulShiftBase[8];
							num5 += 9;
						}
						else
						{
							num11 = SqlDecimal.s_rgulShiftBase[-num5 - 1];
							num5 = 0;
						}
						SqlDecimal.MpDiv1(array3, ref num10, num11, out num12);
					}
					while (num5 != 0);
					if (num10 > SqlDecimal.s_cNumeMax)
					{
						throw new OverflowException(SQLResource.ArithOverflowMessage);
					}
					for (i = num10; i < SqlDecimal.s_cNumeMax; i++)
					{
						array3[i] = 0U;
					}
					SqlDecimal sqlDecimal = new SqlDecimal(array3, (byte)num10, (byte)num4, (byte)num2, flag);
					if (sqlDecimal.FGt10_38())
					{
						throw new OverflowException(SQLResource.ArithOverflowMessage);
					}
					if (num12 >= num11 / 2U)
					{
						sqlDecimal.AddULong(1U);
					}
					if (sqlDecimal.FZero())
					{
						sqlDecimal.SetPositive();
					}
					return sqlDecimal;
				}
				else
				{
					if (num10 > SqlDecimal.s_cNumeMax)
					{
						throw new OverflowException(SQLResource.ArithOverflowMessage);
					}
					for (i = num10; i < SqlDecimal.s_cNumeMax; i++)
					{
						array3[i] = 0U;
					}
					SqlDecimal sqlDecimal = new SqlDecimal(array3, (byte)num10, (byte)num4, (byte)num, flag);
					if (sqlDecimal.FZero())
					{
						sqlDecimal.SetPositive();
					}
					sqlDecimal.AdjustScale(num5, true);
					return sqlDecimal;
				}
			}
			else
			{
				if (num10 > SqlDecimal.s_cNumeMax)
				{
					throw new OverflowException(SQLResource.ArithOverflowMessage);
				}
				for (i = num10; i < SqlDecimal.s_cNumeMax; i++)
				{
					array3[i] = 0U;
				}
				SqlDecimal sqlDecimal = new SqlDecimal(array3, (byte)num10, (byte)num4, (byte)num2, flag);
				if (sqlDecimal.FGt10_38())
				{
					throw new OverflowException(SQLResource.ArithOverflowMessage);
				}
				if (sqlDecimal.FZero())
				{
					sqlDecimal.SetPositive();
				}
				return sqlDecimal;
			}
		}

		/// <summary>The division operator calculates the results of dividing the first <see cref="T:System.Data.SqlTypes.SqlDecimal" /> operand by the second.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure whose <see cref="P:System.Data.SqlTypes.SqlDecimal.Value" /> property contains the results of the division.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. </param>
		// Token: 0x060009DD RID: 2525 RVA: 0x0003A078 File Offset: 0x00038278
		public static SqlDecimal operator /(SqlDecimal x, SqlDecimal y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlDecimal.Null;
			}
			if (y.FZero())
			{
				throw new DivideByZeroException(SQLResource.DivideByZeroMessage);
			}
			bool flag = x.IsPositive == y.IsPositive;
			int num = Math.Max((int)(x._bScale + y._bPrec + 1), (int)SqlDecimal.s_cNumeDivScaleMin);
			int num2 = (int)(x._bPrec - x._bScale + y._bScale);
			int num3 = num + (int)x._bPrec + (int)y._bPrec + 1;
			int num4 = Math.Min(num, (int)SqlDecimal.s_cNumeDivScaleMin);
			num2 = Math.Min(num2, (int)SqlDecimal.s_NUMERIC_MAX_PRECISION);
			num3 = num2 + num;
			if (num3 > (int)SqlDecimal.s_NUMERIC_MAX_PRECISION)
			{
				num3 = (int)SqlDecimal.s_NUMERIC_MAX_PRECISION;
			}
			num = Math.Min(num3 - num2, num);
			num = Math.Max(num, num4);
			int num5 = num - (int)x._bScale + (int)y._bScale;
			x.AdjustScale(num5, true);
			uint[] array = new uint[] { x._data1, x._data2, x._data3, x._data4 };
			uint[] array2 = new uint[] { y._data1, y._data2, y._data3, y._data4 };
			uint[] array3 = new uint[SqlDecimal.s_cNumeMax + 1];
			uint[] array4 = new uint[SqlDecimal.s_cNumeMax];
			int num6;
			int num7;
			SqlDecimal.MpDiv(array, (int)x._bLen, array2, (int)y._bLen, array4, out num6, array3, out num7);
			SqlDecimal.ZeroToMaxLen(array4, num6);
			SqlDecimal sqlDecimal = new SqlDecimal(array4, (byte)num6, (byte)num3, (byte)num, flag);
			if (sqlDecimal.FZero())
			{
				sqlDecimal.SetPositive();
			}
			return sqlDecimal;
		}

		/// <summary>Converts the supplied <see cref="T:System.Data.SqlTypes.SqlByte" /> structure to <see cref="T:System.Data.SqlTypes.SqlDecimal" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure whose <see cref="P:System.Data.SqlTypes.SqlDecimal.Value" /> property equals the <see cref="P:System.Data.SqlTypes.SqlByte.Value" /> property of the <see cref="T:System.Data.SqlTypes.SqlByte" /> parameter.</returns>
		/// <param name="x">The <see cref="T:System.Data.SqlTypes.SqlByte" /> structure to be converted. </param>
		// Token: 0x060009DE RID: 2526 RVA: 0x0003A217 File Offset: 0x00038417
		public static implicit operator SqlDecimal(SqlByte x)
		{
			if (!x.IsNull)
			{
				return new SqlDecimal((int)x.Value);
			}
			return SqlDecimal.Null;
		}

		/// <summary>Converts the supplied <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure to <see cref="T:System.Data.SqlTypes.SqlDecimal" /></summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure whose <see cref="P:System.Data.SqlTypes.SqlDecimal.Value" /> property equals the <see cref="P:System.Data.SqlTypes.SqlInt16.Value" /> property of the <see cref="T:System.Data.SqlTypes.SqlInt16" /> parameter.</returns>
		/// <param name="x">The <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure to be converted. </param>
		// Token: 0x060009DF RID: 2527 RVA: 0x0003A234 File Offset: 0x00038434
		public static implicit operator SqlDecimal(SqlInt16 x)
		{
			if (!x.IsNull)
			{
				return new SqlDecimal((int)x.Value);
			}
			return SqlDecimal.Null;
		}

		/// <summary>Converts the supplied <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure to <see cref="T:System.Data.SqlTypes.SqlDecimal" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure whose <see cref="P:System.Data.SqlTypes.SqlDecimal.Value" /> property is equal to the <see cref="P:System.Data.SqlTypes.SqlDecimal.Value" /> property of the <see cref="T:System.Data.SqlTypes.SqlInt32" /> parameter.</returns>
		/// <param name="x">The <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure to be converted. </param>
		// Token: 0x060009E0 RID: 2528 RVA: 0x0003A251 File Offset: 0x00038451
		public static implicit operator SqlDecimal(SqlInt32 x)
		{
			if (!x.IsNull)
			{
				return new SqlDecimal(x.Value);
			}
			return SqlDecimal.Null;
		}

		/// <summary>Converts the supplied <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure to SqlDecimal.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure whose <see cref="P:System.Data.SqlTypes.SqlDecimal.Value" /> equals the <see cref="P:System.Data.SqlTypes.SqlInt64.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlInt64" /> parameter.</returns>
		/// <param name="x">The <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure to be converted. </param>
		// Token: 0x060009E1 RID: 2529 RVA: 0x0003A26E File Offset: 0x0003846E
		public static implicit operator SqlDecimal(SqlInt64 x)
		{
			if (!x.IsNull)
			{
				return new SqlDecimal(x.Value);
			}
			return SqlDecimal.Null;
		}

		/// <summary>Converts the <see cref="T:System.Data.SqlTypes.SqlMoney" /> operand to <see cref="T:System.Data.SqlTypes.SqlDecimal" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure whose <see cref="P:System.Data.SqlTypes.SqlDecimal.Value" /> equals the <see cref="P:System.Data.SqlTypes.SqlMoney.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlMoney" /> parameter.</returns>
		/// <param name="x">The <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure to be converted. </param>
		// Token: 0x060009E2 RID: 2530 RVA: 0x0003A28B File Offset: 0x0003848B
		public static implicit operator SqlDecimal(SqlMoney x)
		{
			if (!x.IsNull)
			{
				return new SqlDecimal(x.ToDecimal());
			}
			return SqlDecimal.Null;
		}

		// Token: 0x060009E3 RID: 2531 RVA: 0x0003A2A8 File Offset: 0x000384A8
		private static void ZeroToMaxLen(uint[] rgulData, int cUI4sCur)
		{
			switch (cUI4sCur)
			{
			case 1:
				rgulData[1] = (rgulData[2] = (rgulData[3] = 0U));
				return;
			case 2:
				rgulData[2] = (rgulData[3] = 0U);
				return;
			case 3:
				rgulData[3] = 0U;
				return;
			default:
				return;
			}
		}

		// Token: 0x060009E4 RID: 2532 RVA: 0x0003A2EA File Offset: 0x000384EA
		private bool FZero()
		{
			return this._data1 == 0U && this._bLen <= 1;
		}

		// Token: 0x060009E5 RID: 2533 RVA: 0x0003A304 File Offset: 0x00038504
		private bool FGt10_38()
		{
			return (ulong)this._data4 >= 1262177448UL && this._bLen == 4 && ((ulong)this._data4 > 1262177448UL || (ulong)this._data3 > 1518781562UL || ((ulong)this._data3 == 1518781562UL && (ulong)this._data2 >= 160047680UL));
		}

		// Token: 0x060009E6 RID: 2534 RVA: 0x0003A370 File Offset: 0x00038570
		private bool FGt10_38(uint[] rglData)
		{
			return (ulong)rglData[3] >= 1262177448UL && ((ulong)rglData[3] > 1262177448UL || (ulong)rglData[2] > 1518781562UL || ((ulong)rglData[2] == 1518781562UL && (ulong)rglData[1] >= 160047680UL));
		}

		// Token: 0x060009E7 RID: 2535 RVA: 0x0003A3C4 File Offset: 0x000385C4
		private static byte BGetPrecUI4(uint value)
		{
			int num;
			if (value < SqlDecimal.s_ulT4)
			{
				if (value < SqlDecimal.s_ulT2)
				{
					num = ((value >= SqlDecimal.s_ulT1) ? 2 : 1);
				}
				else
				{
					num = ((value >= SqlDecimal.s_ulT3) ? 4 : 3);
				}
			}
			else if (value < SqlDecimal.s_ulT8)
			{
				if (value < SqlDecimal.s_ulT6)
				{
					num = ((value >= SqlDecimal.s_ulT5) ? 6 : 5);
				}
				else
				{
					num = ((value >= SqlDecimal.s_ulT7) ? 8 : 7);
				}
			}
			else
			{
				num = ((value >= SqlDecimal.s_ulT9) ? 10 : 9);
			}
			return (byte)num;
		}

		// Token: 0x060009E8 RID: 2536 RVA: 0x0003A440 File Offset: 0x00038640
		private static byte BGetPrecUI8(ulong dwlVal)
		{
			int num2;
			if (dwlVal < (ulong)SqlDecimal.s_ulT8)
			{
				uint num = (uint)dwlVal;
				if (num < SqlDecimal.s_ulT4)
				{
					if (num < SqlDecimal.s_ulT2)
					{
						num2 = ((num >= SqlDecimal.s_ulT1) ? 2 : 1);
					}
					else
					{
						num2 = ((num >= SqlDecimal.s_ulT3) ? 4 : 3);
					}
				}
				else if (num < SqlDecimal.s_ulT6)
				{
					num2 = ((num >= SqlDecimal.s_ulT5) ? 6 : 5);
				}
				else
				{
					num2 = ((num >= SqlDecimal.s_ulT7) ? 8 : 7);
				}
			}
			else if (dwlVal < SqlDecimal.s_dwlT16)
			{
				if (dwlVal < SqlDecimal.s_dwlT12)
				{
					if (dwlVal < SqlDecimal.s_dwlT10)
					{
						num2 = ((dwlVal >= (ulong)SqlDecimal.s_ulT9) ? 10 : 9);
					}
					else
					{
						num2 = ((dwlVal >= SqlDecimal.s_dwlT11) ? 12 : 11);
					}
				}
				else if (dwlVal < SqlDecimal.s_dwlT14)
				{
					num2 = ((dwlVal >= SqlDecimal.s_dwlT13) ? 14 : 13);
				}
				else
				{
					num2 = ((dwlVal >= SqlDecimal.s_dwlT15) ? 16 : 15);
				}
			}
			else if (dwlVal < SqlDecimal.s_dwlT18)
			{
				num2 = ((dwlVal >= SqlDecimal.s_dwlT17) ? 18 : 17);
			}
			else
			{
				num2 = ((dwlVal >= SqlDecimal.s_dwlT19) ? 20 : 19);
			}
			return (byte)num2;
		}

		// Token: 0x060009E9 RID: 2537 RVA: 0x0003A548 File Offset: 0x00038748
		private void AddULong(uint ulAdd)
		{
			ulong num = (ulong)ulAdd;
			int bLen = (int)this._bLen;
			uint[] array = new uint[] { this._data1, this._data2, this._data3, this._data4 };
			int num2 = 0;
			for (;;)
			{
				num += (ulong)array[num2];
				array[num2] = (uint)num;
				num >>= 32;
				if (num == 0UL)
				{
					break;
				}
				num2++;
				if (num2 >= bLen)
				{
					goto Block_2;
				}
			}
			this.StoreFromWorkingArray(array);
			return;
			Block_2:
			if (num2 == SqlDecimal.s_cNumeMax)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			array[num2] = (uint)num;
			this._bLen += 1;
			if (this.FGt10_38(array))
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			this.StoreFromWorkingArray(array);
		}

		// Token: 0x060009EA RID: 2538 RVA: 0x0003A5F4 File Offset: 0x000387F4
		private void MultByULong(uint uiMultiplier)
		{
			int bLen = (int)this._bLen;
			ulong num = 0UL;
			uint[] array = new uint[] { this._data1, this._data2, this._data3, this._data4 };
			for (int i = 0; i < bLen; i++)
			{
				ulong num2 = (ulong)array[i] * (ulong)uiMultiplier;
				num += num2;
				if (num < num2)
				{
					num2 = SqlDecimal.s_ulInt32Base;
				}
				else
				{
					num2 = 0UL;
				}
				array[i] = (uint)num;
				num = (num >> 32) + num2;
			}
			if (num != 0UL)
			{
				if (bLen == SqlDecimal.s_cNumeMax)
				{
					throw new OverflowException(SQLResource.ArithOverflowMessage);
				}
				array[bLen] = (uint)num;
				this._bLen += 1;
			}
			if (this.FGt10_38(array))
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			this.StoreFromWorkingArray(array);
		}

		// Token: 0x060009EB RID: 2539 RVA: 0x0003A6B8 File Offset: 0x000388B8
		private uint DivByULong(uint iDivisor)
		{
			ulong num = (ulong)iDivisor;
			ulong num2 = 0UL;
			bool flag = true;
			if (num == 0UL)
			{
				throw new DivideByZeroException(SQLResource.DivideByZeroMessage);
			}
			uint[] array = new uint[] { this._data1, this._data2, this._data3, this._data4 };
			for (int i = (int)this._bLen; i > 0; i--)
			{
				num2 = (num2 << 32) + (ulong)array[i - 1];
				uint num3 = (uint)(num2 / num);
				array[i - 1] = num3;
				num2 %= num;
				if (flag && num3 == 0U)
				{
					this._bLen -= 1;
				}
				else
				{
					flag = false;
				}
			}
			this.StoreFromWorkingArray(array);
			if (flag)
			{
				this._bLen = 1;
			}
			return (uint)num2;
		}

		// Token: 0x060009EC RID: 2540 RVA: 0x0003A76C File Offset: 0x0003896C
		internal void AdjustScale(int digits, bool fRound)
		{
			bool flag = false;
			int i = digits;
			if (i + (int)this._bScale < 0)
			{
				throw new SqlTruncateException();
			}
			if (i + (int)this._bScale > (int)SqlDecimal.s_NUMERIC_MAX_PRECISION)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			byte b = (byte)(i + (int)this._bScale);
			byte b2 = (byte)Math.Min((int)SqlDecimal.s_NUMERIC_MAX_PRECISION, Math.Max(1, i + (int)this._bPrec));
			if (i > 0)
			{
				this._bScale = b;
				this._bPrec = b2;
				while (i > 0)
				{
					uint num;
					if (i >= 9)
					{
						num = SqlDecimal.s_rgulShiftBase[8];
						i -= 9;
					}
					else
					{
						num = SqlDecimal.s_rgulShiftBase[i - 1];
						i = 0;
					}
					this.MultByULong(num);
				}
			}
			else if (i < 0)
			{
				uint num;
				uint num2;
				do
				{
					if (i <= -9)
					{
						num = SqlDecimal.s_rgulShiftBase[8];
						i += 9;
					}
					else
					{
						num = SqlDecimal.s_rgulShiftBase[-i - 1];
						i = 0;
					}
					num2 = this.DivByULong(num);
				}
				while (i < 0);
				flag = num2 >= num / 2U;
				this._bScale = b;
				this._bPrec = b2;
			}
			if (flag && fRound)
			{
				this.AddULong(1U);
				return;
			}
			if (this.FZero())
			{
				this.SetPositive();
			}
		}

		// Token: 0x060009ED RID: 2541 RVA: 0x0003A888 File Offset: 0x00038A88
		private int LAbsCmp(SqlDecimal snumOp)
		{
			int bLen = (int)snumOp._bLen;
			int bLen2 = (int)this._bLen;
			if (bLen != bLen2)
			{
				if (bLen2 <= bLen)
				{
					return -1;
				}
				return 1;
			}
			else
			{
				uint[] array = new uint[] { this._data1, this._data2, this._data3, this._data4 };
				uint[] array2 = new uint[] { snumOp._data1, snumOp._data2, snumOp._data3, snumOp._data4 };
				int num = bLen - 1;
				while (array[num] == array2[num])
				{
					num--;
					if (num < 0)
					{
						return 0;
					}
				}
				if (array[num] <= array2[num])
				{
					return -1;
				}
				return 1;
			}
		}

		// Token: 0x060009EE RID: 2542 RVA: 0x0003A92C File Offset: 0x00038B2C
		private static void MpMove(uint[] rgulS, int ciulS, uint[] rgulD, out int ciulD)
		{
			ciulD = ciulS;
			for (int i = 0; i < ciulS; i++)
			{
				rgulD[i] = rgulS[i];
			}
		}

		// Token: 0x060009EF RID: 2543 RVA: 0x0003A94E File Offset: 0x00038B4E
		private static void MpSet(uint[] rgulD, out int ciulD, uint iulN)
		{
			ciulD = 1;
			rgulD[0] = iulN;
		}

		// Token: 0x060009F0 RID: 2544 RVA: 0x0003A957 File Offset: 0x00038B57
		private static void MpNormalize(uint[] rgulU, ref int ciulU)
		{
			while (ciulU > 1 && rgulU[ciulU - 1] == 0U)
			{
				ciulU--;
			}
		}

		// Token: 0x060009F1 RID: 2545 RVA: 0x0003A970 File Offset: 0x00038B70
		private static void MpMul1(uint[] piulD, ref int ciulD, uint iulX)
		{
			uint num = 0U;
			int i;
			for (i = 0; i < ciulD; i++)
			{
				ulong num2 = (ulong)piulD[i];
				ulong num3 = (ulong)num + num2 * (ulong)iulX;
				num = SqlDecimal.HI(num3);
				piulD[i] = SqlDecimal.LO(num3);
			}
			if (num != 0U)
			{
				piulD[i] = num;
				ciulD++;
			}
		}

		// Token: 0x060009F2 RID: 2546 RVA: 0x0003A9B8 File Offset: 0x00038BB8
		private static void MpDiv1(uint[] rgulU, ref int ciulU, uint iulD, out uint iulR)
		{
			uint num = 0U;
			ulong num2 = (ulong)iulD;
			int i = ciulU;
			while (i > 0)
			{
				i--;
				ulong num3 = ((ulong)num << 32) + (ulong)rgulU[i];
				rgulU[i] = (uint)(num3 / num2);
				num = (uint)(num3 - (ulong)rgulU[i] * num2);
			}
			iulR = num;
			SqlDecimal.MpNormalize(rgulU, ref ciulU);
		}

		// Token: 0x060009F3 RID: 2547 RVA: 0x0003A9FD File Offset: 0x00038BFD
		internal static ulong DWL(uint lo, uint hi)
		{
			return (ulong)lo + ((ulong)hi << 32);
		}

		// Token: 0x060009F4 RID: 2548 RVA: 0x0003AA07 File Offset: 0x00038C07
		private static uint HI(ulong x)
		{
			return (uint)(x >> 32);
		}

		// Token: 0x060009F5 RID: 2549 RVA: 0x0003AA0E File Offset: 0x00038C0E
		private static uint LO(ulong x)
		{
			return (uint)x;
		}

		// Token: 0x060009F6 RID: 2550 RVA: 0x0003AA14 File Offset: 0x00038C14
		private static void MpDiv(uint[] rgulU, int ciulU, uint[] rgulD, int ciulD, uint[] rgulQ, out int ciulQ, uint[] rgulR, out int ciulR)
		{
			if (ciulD == 1 && rgulD[0] == 0U)
			{
				ciulQ = (ciulR = 0);
				return;
			}
			if (ciulU == 1 && ciulD == 1)
			{
				SqlDecimal.MpSet(rgulQ, out ciulQ, rgulU[0] / rgulD[0]);
				SqlDecimal.MpSet(rgulR, out ciulR, rgulU[0] % rgulD[0]);
				return;
			}
			if (ciulD > ciulU)
			{
				SqlDecimal.MpMove(rgulU, ciulU, rgulR, out ciulR);
				SqlDecimal.MpSet(rgulQ, out ciulQ, 0U);
				return;
			}
			if (ciulU <= 2)
			{
				ulong num = SqlDecimal.DWL(rgulU[0], rgulU[1]);
				ulong num2 = (ulong)rgulD[0];
				if (ciulD > 1)
				{
					num2 += (ulong)rgulD[1] << 32;
				}
				ulong num3 = num / num2;
				rgulQ[0] = SqlDecimal.LO(num3);
				rgulQ[1] = SqlDecimal.HI(num3);
				ciulQ = ((SqlDecimal.HI(num3) != 0U) ? 2 : 1);
				num3 = num % num2;
				rgulR[0] = SqlDecimal.LO(num3);
				rgulR[1] = SqlDecimal.HI(num3);
				ciulR = ((SqlDecimal.HI(num3) != 0U) ? 2 : 1);
				return;
			}
			if (ciulD == 1)
			{
				SqlDecimal.MpMove(rgulU, ciulU, rgulQ, out ciulQ);
				uint num4;
				SqlDecimal.MpDiv1(rgulQ, ref ciulQ, rgulD[0], out num4);
				rgulR[0] = num4;
				ciulR = 1;
				return;
			}
			ciulQ = (ciulR = 0);
			if (rgulU != rgulR)
			{
				SqlDecimal.MpMove(rgulU, ciulU, rgulR, out ciulR);
			}
			ciulQ = ciulU - ciulD + 1;
			uint num5 = rgulD[ciulD - 1];
			rgulR[ciulU] = 0U;
			int num6 = ciulU;
			uint num7 = (uint)(SqlDecimal.s_ulInt32Base / ((ulong)num5 + 1UL));
			if (num7 > 1U)
			{
				SqlDecimal.MpMul1(rgulD, ref ciulD, num7);
				num5 = rgulD[ciulD - 1];
				SqlDecimal.MpMul1(rgulR, ref ciulR, num7);
			}
			uint num8 = rgulD[ciulD - 2];
			do
			{
				ulong num9 = SqlDecimal.DWL(rgulR[num6 - 1], rgulR[num6]);
				uint num10;
				if (num5 == rgulR[num6])
				{
					num10 = (uint)(SqlDecimal.s_ulInt32Base - 1UL);
				}
				else
				{
					num10 = (uint)(num9 / (ulong)num5);
				}
				ulong num11 = (ulong)num10;
				uint num12 = (uint)(num9 - num11 * (ulong)num5);
				while ((ulong)num8 * num11 > SqlDecimal.DWL(rgulR[num6 - 2], num12))
				{
					num10 -= 1U;
					if (num12 >= -num5)
					{
						break;
					}
					num12 += num5;
					num11 = (ulong)num10;
				}
				num9 = SqlDecimal.s_ulInt32Base;
				ulong num13 = 0UL;
				int i = 0;
				int num14 = num6 - ciulD;
				while (i < ciulD)
				{
					ulong num15 = (ulong)rgulD[i];
					num13 += (ulong)num10 * num15;
					num9 += (ulong)rgulR[num14] - (ulong)SqlDecimal.LO(num13);
					num13 = (ulong)SqlDecimal.HI(num13);
					rgulR[num14] = SqlDecimal.LO(num9);
					num9 = (ulong)SqlDecimal.HI(num9) + SqlDecimal.s_ulInt32Base - 1UL;
					i++;
					num14++;
				}
				num9 += (ulong)rgulR[num14] - num13;
				rgulR[num14] = SqlDecimal.LO(num9);
				rgulQ[num6 - ciulD] = num10;
				if (SqlDecimal.HI(num9) == 0U)
				{
					rgulQ[num6 - ciulD] = num10 - 1U;
					uint num16 = 0U;
					i = 0;
					num14 = num6 - ciulD;
					while (i < ciulD)
					{
						num9 = (ulong)rgulD[i] + (ulong)rgulR[num14] + (ulong)num16;
						num16 = SqlDecimal.HI(num9);
						rgulR[num14] = SqlDecimal.LO(num9);
						i++;
						num14++;
					}
					rgulR[num14] += num16;
				}
				num6--;
			}
			while (num6 >= ciulD);
			SqlDecimal.MpNormalize(rgulQ, ref ciulQ);
			ciulR = ciulD;
			SqlDecimal.MpNormalize(rgulR, ref ciulR);
			if (num7 > 1U)
			{
				uint num17;
				SqlDecimal.MpDiv1(rgulD, ref ciulD, num7, out num17);
				SqlDecimal.MpDiv1(rgulR, ref ciulR, num7, out num17);
			}
		}

		// Token: 0x060009F7 RID: 2551 RVA: 0x0003AD30 File Offset: 0x00038F30
		private EComparison CompareNm(SqlDecimal snumOp)
		{
			int num = (this.IsPositive ? 1 : (-1));
			int num2 = (snumOp.IsPositive ? 1 : (-1));
			if (num == num2)
			{
				SqlDecimal sqlDecimal = this;
				SqlDecimal sqlDecimal2 = snumOp;
				int num3 = (int)(this._bScale - snumOp._bScale);
				if (num3 < 0)
				{
					try
					{
						sqlDecimal.AdjustScale(-num3, true);
						goto IL_0078;
					}
					catch (OverflowException)
					{
						return (num > 0) ? EComparison.GT : EComparison.LT;
					}
				}
				if (num3 > 0)
				{
					try
					{
						sqlDecimal2.AdjustScale(num3, true);
					}
					catch (OverflowException)
					{
						return (num > 0) ? EComparison.LT : EComparison.GT;
					}
				}
				IL_0078:
				int num4 = sqlDecimal.LAbsCmp(sqlDecimal2);
				if (num4 == 0)
				{
					return EComparison.EQ;
				}
				if (num * num4 < 0)
				{
					return EComparison.LT;
				}
				return EComparison.GT;
			}
			if (num != 1)
			{
				return EComparison.LT;
			}
			return EComparison.GT;
		}

		// Token: 0x060009F8 RID: 2552 RVA: 0x0003ADF0 File Offset: 0x00038FF0
		private static void CheckValidPrecScale(byte bPrec, byte bScale)
		{
			if (bPrec < 1 || bPrec > SqlDecimal.MaxPrecision || bScale < 0 || bScale > SqlDecimal.MaxScale || bScale > bPrec)
			{
				throw new SqlTypeException(SQLResource.InvalidPrecScaleMessage);
			}
		}

		/// <summary>Performs a logical comparison of the two <see cref="T:System.Data.SqlTypes.SqlDecimal" /> operands to determine whether they are equal.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the two instances are equal or <see cref="F:System.Data.SqlTypes.SqlBoolean.False" /> if the two instances are not equal. If either instance of <see cref="T:System.Data.SqlTypes.SqlDecimal" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. </param>
		// Token: 0x060009F9 RID: 2553 RVA: 0x0003AE19 File Offset: 0x00039019
		public static SqlBoolean operator ==(SqlDecimal x, SqlDecimal y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.CompareNm(y) == EComparison.EQ);
			}
			return SqlBoolean.Null;
		}

		/// <summary>Performs a logical comparison of two <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structures to determine whether the first is less than the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is less than the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlDecimal" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. </param>
		// Token: 0x060009FA RID: 2554 RVA: 0x0003AE43 File Offset: 0x00039043
		public static SqlBoolean operator <(SqlDecimal x, SqlDecimal y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.CompareNm(y) == EComparison.LT);
			}
			return SqlBoolean.Null;
		}

		/// <summary>Performs a logical comparison of two <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structures to determine whether the first is greater than the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is less than the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlDecimal" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. </param>
		// Token: 0x060009FB RID: 2555 RVA: 0x0003AE6D File Offset: 0x0003906D
		public static SqlBoolean operator >(SqlDecimal x, SqlDecimal y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.CompareNm(y) == EComparison.GT);
			}
			return SqlBoolean.Null;
		}

		/// <summary>Performs a logical comparison of two <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structures to determine whether the first is less than the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is less than the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlDecimal" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. </param>
		// Token: 0x060009FC RID: 2556 RVA: 0x0003AE97 File Offset: 0x00039097
		public static SqlBoolean LessThan(SqlDecimal x, SqlDecimal y)
		{
			return x < y;
		}

		/// <summary>Performs a logical comparison of two <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structures to determine whether the first is greater than the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is less than the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlDecimal" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. </param>
		// Token: 0x060009FD RID: 2557 RVA: 0x0003AEA0 File Offset: 0x000390A0
		public static SqlBoolean GreaterThan(SqlDecimal x, SqlDecimal y)
		{
			return x > y;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure to <see cref="T:System.Data.SqlTypes.SqlDouble" />.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlDouble" /> structure with the same value as this instance of <see cref="T:System.Data.SqlTypes.SqlDecimal" />.</returns>
		// Token: 0x060009FE RID: 2558 RVA: 0x0003AEA9 File Offset: 0x000390A9
		public SqlDouble ToSqlDouble()
		{
			return this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure to <see cref="T:System.Data.SqlTypes.SqlInt64" />.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure with the same value as this instance of <see cref="T:System.Data.SqlTypes.SqlDecimal" />.</returns>
		// Token: 0x060009FF RID: 2559 RVA: 0x0003AEB6 File Offset: 0x000390B6
		public SqlInt64 ToSqlInt64()
		{
			return (SqlInt64)this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure to <see cref="T:System.Data.SqlTypes.SqlMoney" />.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure with the same value as this instance of <see cref="T:System.Data.SqlTypes.SqlDecimal" />.</returns>
		// Token: 0x06000A00 RID: 2560 RVA: 0x0003AEC3 File Offset: 0x000390C3
		public SqlMoney ToSqlMoney()
		{
			return (SqlMoney)this;
		}

		// Token: 0x06000A01 RID: 2561 RVA: 0x0003AED0 File Offset: 0x000390D0
		private static char ChFromDigit(uint uiDigit)
		{
			return (char)(uiDigit + 48U);
		}

		// Token: 0x06000A02 RID: 2562 RVA: 0x0003AED7 File Offset: 0x000390D7
		private void StoreFromWorkingArray(uint[] rguiData)
		{
			this._data1 = rguiData[0];
			this._data2 = rguiData[1];
			this._data3 = rguiData[2];
			this._data4 = rguiData[3];
		}

		// Token: 0x06000A03 RID: 2563 RVA: 0x0003AF00 File Offset: 0x00039100
		private void SetToZero()
		{
			this._bLen = 1;
			this._data1 = (this._data2 = (this._data3 = (this._data4 = 0U)));
			this._bStatus = SqlDecimal.s_bNotNull | SqlDecimal.s_bPositive;
		}

		/// <summary>Compares this <see cref="T:System.Data.SqlTypes.SqlDecimal" /> instance to the supplied <see cref="T:System.Object" /> and returns an indication of their relative values.</summary>
		/// <returns>A signed number that indicates the relative values of the instance and the object.Return Value Condition Less than zero This instance is less than the object. Zero This instance is the same as the object. Greater than zero This instance is greater than the object -or- The object is a null reference (Nothing in Visual Basic) </returns>
		/// <param name="value">The <see cref="T:System.Object" /> to be compared. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000A04 RID: 2564 RVA: 0x0003AF48 File Offset: 0x00039148
		public int CompareTo(object value)
		{
			if (value is SqlDecimal)
			{
				SqlDecimal sqlDecimal = (SqlDecimal)value;
				return this.CompareTo(sqlDecimal);
			}
			throw ADP.WrongType(value.GetType(), typeof(SqlDecimal));
		}

		/// <summary>Compares this <see cref="T:System.Data.SqlTypes.SqlDecimal" /> instance to the supplied <see cref="T:System.Data.SqlTypes.SqlDecimal" /> object and returns an indication of their relative values.</summary>
		/// <returns>A signed number that indicates the relative values of the instance and the object.Return value Condition Less than zero This instance is less than the object. Zero This instance is the same as the object. Greater than zero This instance is greater than the object -or- The object is a null reference (Nothing in Visual Basic) </returns>
		/// <param name="value">The <see cref="T:System.Data.SqlTypes.SqlDecimal" /> to be compared. </param>
		// Token: 0x06000A05 RID: 2565 RVA: 0x0003AF84 File Offset: 0x00039184
		public int CompareTo(SqlDecimal value)
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

		/// <summary>Compares the supplied <see cref="T:System.Object" /> parameter to the <see cref="P:System.Data.SqlTypes.SqlDecimal.Value" /> property of the <see cref="T:System.Data.SqlTypes.SqlDecimal" /> instance.</summary>
		/// <returns>true if object is an instance of <see cref="T:System.Data.SqlTypes.SqlDecimal" /> and the two are equal. Otherwise, false.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to be compared.</param>
		// Token: 0x06000A06 RID: 2566 RVA: 0x0003AFDC File Offset: 0x000391DC
		public override bool Equals(object value)
		{
			if (!(value is SqlDecimal))
			{
				return false;
			}
			SqlDecimal sqlDecimal = (SqlDecimal)value;
			if (sqlDecimal.IsNull || this.IsNull)
			{
				return sqlDecimal.IsNull && this.IsNull;
			}
			return (this == sqlDecimal).Value;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06000A07 RID: 2567 RVA: 0x0003B034 File Offset: 0x00039234
		public override int GetHashCode()
		{
			if (this.IsNull)
			{
				return 0;
			}
			SqlDecimal sqlDecimal = this;
			int num = (int)sqlDecimal.CalculatePrecision();
			sqlDecimal.AdjustScale((int)SqlDecimal.s_NUMERIC_MAX_PRECISION - num, true);
			int bLen = (int)sqlDecimal._bLen;
			int num2 = 0;
			int[] data = sqlDecimal.Data;
			for (int i = 0; i < bLen; i++)
			{
				int num3 = (num2 >> 28) & 255;
				num2 <<= 4;
				num2 = num2 ^ data[i] ^ num3;
			}
			return num2;
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <returns>An XmlSchema.</returns>
		// Token: 0x06000A08 RID: 2568 RVA: 0x00011F10 File Offset: 0x00010110
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <param name="reader">XmlReader </param>
		// Token: 0x06000A09 RID: 2569 RVA: 0x0003B0A8 File Offset: 0x000392A8
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			string attribute = reader.GetAttribute("nil", "http://www.w3.org/2001/XMLSchema-instance");
			if (attribute != null && XmlConvert.ToBoolean(attribute))
			{
				reader.ReadElementString();
				this._bStatus = SqlDecimal.s_bReverseNullMask & this._bStatus;
				return;
			}
			SqlDecimal sqlDecimal = SqlDecimal.Parse(reader.ReadElementString());
			this._bStatus = sqlDecimal._bStatus;
			this._bLen = sqlDecimal._bLen;
			this._bPrec = sqlDecimal._bPrec;
			this._bScale = sqlDecimal._bScale;
			this._data1 = sqlDecimal._data1;
			this._data2 = sqlDecimal._data2;
			this._data3 = sqlDecimal._data3;
			this._data4 = sqlDecimal._data4;
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <param name="writer">XmlWriter </param>
		// Token: 0x06000A0A RID: 2570 RVA: 0x0003B158 File Offset: 0x00039358
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			if (this.IsNull)
			{
				writer.WriteAttributeString("xsi", "nil", "http://www.w3.org/2001/XMLSchema-instance", "true");
				return;
			}
			writer.WriteString(this.ToString());
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the specified <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</summary>
		/// <returns>A string value that indicates the XSD of the specified <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</returns>
		/// <param name="schemaSet">A <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</param>
		// Token: 0x06000A0B RID: 2571 RVA: 0x0003B18F File Offset: 0x0003938F
		public static XmlQualifiedName GetXsdType(XmlSchemaSet schemaSet)
		{
			return new XmlQualifiedName("decimal", "http://www.w3.org/2001/XMLSchema");
		}

		// Token: 0x040003F5 RID: 1013
		internal byte _bStatus;

		// Token: 0x040003F6 RID: 1014
		internal byte _bLen;

		// Token: 0x040003F7 RID: 1015
		internal byte _bPrec;

		// Token: 0x040003F8 RID: 1016
		internal byte _bScale;

		// Token: 0x040003F9 RID: 1017
		internal uint _data1;

		// Token: 0x040003FA RID: 1018
		internal uint _data2;

		// Token: 0x040003FB RID: 1019
		internal uint _data3;

		// Token: 0x040003FC RID: 1020
		internal uint _data4;

		// Token: 0x040003FD RID: 1021
		private static readonly byte s_NUMERIC_MAX_PRECISION = 38;

		/// <summary>A constant representing the largest possible value for the <see cref="P:System.Data.SqlTypes.SqlDecimal.Precision" /> property.</summary>
		// Token: 0x040003FE RID: 1022
		public static readonly byte MaxPrecision = SqlDecimal.s_NUMERIC_MAX_PRECISION;

		/// <summary>A constant representing the maximum value for the <see cref="P:System.Data.SqlTypes.SqlDecimal.Scale" /> property.</summary>
		// Token: 0x040003FF RID: 1023
		public static readonly byte MaxScale = SqlDecimal.s_NUMERIC_MAX_PRECISION;

		// Token: 0x04000400 RID: 1024
		private static readonly byte s_bNullMask = 1;

		// Token: 0x04000401 RID: 1025
		private static readonly byte s_bIsNull = 0;

		// Token: 0x04000402 RID: 1026
		private static readonly byte s_bNotNull = 1;

		// Token: 0x04000403 RID: 1027
		private static readonly byte s_bReverseNullMask = ~SqlDecimal.s_bNullMask;

		// Token: 0x04000404 RID: 1028
		private static readonly byte s_bSignMask = 2;

		// Token: 0x04000405 RID: 1029
		private static readonly byte s_bPositive = 0;

		// Token: 0x04000406 RID: 1030
		private static readonly byte s_bNegative = 2;

		// Token: 0x04000407 RID: 1031
		private static readonly byte s_bReverseSignMask = ~SqlDecimal.s_bSignMask;

		// Token: 0x04000408 RID: 1032
		private static readonly uint s_uiZero = 0U;

		// Token: 0x04000409 RID: 1033
		private static readonly int s_cNumeMax = 4;

		// Token: 0x0400040A RID: 1034
		private static readonly long s_lInt32Base = 4294967296L;

		// Token: 0x0400040B RID: 1035
		private static readonly ulong s_ulInt32Base = 4294967296UL;

		// Token: 0x0400040C RID: 1036
		private static readonly ulong s_ulInt32BaseForMod = SqlDecimal.s_ulInt32Base - 1UL;

		// Token: 0x0400040D RID: 1037
		internal static readonly ulong s_llMax = 9223372036854775807UL;

		// Token: 0x0400040E RID: 1038
		private static readonly uint s_ulBase10 = 10U;

		// Token: 0x0400040F RID: 1039
		private static readonly double s_DUINT_BASE = (double)SqlDecimal.s_lInt32Base;

		// Token: 0x04000410 RID: 1040
		private static readonly double s_DUINT_BASE2 = SqlDecimal.s_DUINT_BASE * SqlDecimal.s_DUINT_BASE;

		// Token: 0x04000411 RID: 1041
		private static readonly double s_DUINT_BASE3 = SqlDecimal.s_DUINT_BASE2 * SqlDecimal.s_DUINT_BASE;

		// Token: 0x04000412 RID: 1042
		private static readonly double s_DMAX_NUME = 1E+38;

		// Token: 0x04000413 RID: 1043
		private static readonly uint s_DBL_DIG = 17U;

		// Token: 0x04000414 RID: 1044
		private static readonly byte s_cNumeDivScaleMin = 6;

		// Token: 0x04000415 RID: 1045
		private static readonly uint[] s_rgulShiftBase = new uint[] { 10U, 100U, 1000U, 10000U, 100000U, 1000000U, 10000000U, 100000000U, 1000000000U };

		// Token: 0x04000416 RID: 1046
		private static readonly uint[] s_decimalHelpersLo = new uint[]
		{
			10U, 100U, 1000U, 10000U, 100000U, 1000000U, 10000000U, 100000000U, 1000000000U, 1410065408U,
			1215752192U, 3567587328U, 1316134912U, 276447232U, 2764472320U, 1874919424U, 1569325056U, 2808348672U, 2313682944U, 1661992960U,
			3735027712U, 2990538752U, 4135583744U, 2701131776U, 1241513984U, 3825205248U, 3892314112U, 268435456U, 2684354560U, 1073741824U,
			2147483648U, 0U, 0U, 0U, 0U, 0U, 0U, 0U
		};

		// Token: 0x04000417 RID: 1047
		private static readonly uint[] s_decimalHelpersMid = new uint[]
		{
			0U, 0U, 0U, 0U, 0U, 0U, 0U, 0U, 0U, 2U,
			23U, 232U, 2328U, 23283U, 232830U, 2328306U, 23283064U, 232830643U, 2328306436U, 1808227885U,
			902409669U, 434162106U, 46653770U, 466537709U, 370409800U, 3704098002U, 2681241660U, 1042612833U, 1836193738U, 1182068202U,
			3230747430U, 2242703233U, 952195850U, 932023908U, 730304488U, 3008077584U, 16004768U, 160047680U
		};

		// Token: 0x04000418 RID: 1048
		private static readonly uint[] s_decimalHelpersHi = new uint[]
		{
			0U, 0U, 0U, 0U, 0U, 0U, 0U, 0U, 0U, 0U,
			0U, 0U, 0U, 0U, 0U, 0U, 0U, 0U, 0U, 5U,
			54U, 542U, 5421U, 54210U, 542101U, 5421010U, 54210108U, 542101086U, 1126043566U, 2670501072U,
			935206946U, 762134875U, 3326381459U, 3199043520U, 1925664130U, 2076772117U, 3587851993U, 1518781562U
		};

		// Token: 0x04000419 RID: 1049
		private static readonly uint[] s_decimalHelpersHiHi = new uint[]
		{
			0U, 0U, 0U, 0U, 0U, 0U, 0U, 0U, 0U, 0U,
			0U, 0U, 0U, 0U, 0U, 0U, 0U, 0U, 0U, 0U,
			0U, 0U, 0U, 0U, 0U, 0U, 0U, 0U, 1U, 12U,
			126U, 1262U, 12621U, 126217U, 1262177U, 12621774U, 126217744U, 1262177448U
		};

		// Token: 0x0400041A RID: 1050
		private static readonly byte[] s_rgCLenFromPrec = new byte[]
		{
			1, 1, 1, 1, 1, 1, 1, 1, 1, 2,
			2, 2, 2, 2, 2, 2, 2, 2, 2, 3,
			3, 3, 3, 3, 3, 3, 3, 3, 4, 4,
			4, 4, 4, 4, 4, 4, 4, 4
		};

		// Token: 0x0400041B RID: 1051
		private static readonly uint s_ulT1 = 10U;

		// Token: 0x0400041C RID: 1052
		private static readonly uint s_ulT2 = 100U;

		// Token: 0x0400041D RID: 1053
		private static readonly uint s_ulT3 = 1000U;

		// Token: 0x0400041E RID: 1054
		private static readonly uint s_ulT4 = 10000U;

		// Token: 0x0400041F RID: 1055
		private static readonly uint s_ulT5 = 100000U;

		// Token: 0x04000420 RID: 1056
		private static readonly uint s_ulT6 = 1000000U;

		// Token: 0x04000421 RID: 1057
		private static readonly uint s_ulT7 = 10000000U;

		// Token: 0x04000422 RID: 1058
		private static readonly uint s_ulT8 = 100000000U;

		// Token: 0x04000423 RID: 1059
		private static readonly uint s_ulT9 = 1000000000U;

		// Token: 0x04000424 RID: 1060
		private static readonly ulong s_dwlT10 = 10000000000UL;

		// Token: 0x04000425 RID: 1061
		private static readonly ulong s_dwlT11 = 100000000000UL;

		// Token: 0x04000426 RID: 1062
		private static readonly ulong s_dwlT12 = 1000000000000UL;

		// Token: 0x04000427 RID: 1063
		private static readonly ulong s_dwlT13 = 10000000000000UL;

		// Token: 0x04000428 RID: 1064
		private static readonly ulong s_dwlT14 = 100000000000000UL;

		// Token: 0x04000429 RID: 1065
		private static readonly ulong s_dwlT15 = 1000000000000000UL;

		// Token: 0x0400042A RID: 1066
		private static readonly ulong s_dwlT16 = 10000000000000000UL;

		// Token: 0x0400042B RID: 1067
		private static readonly ulong s_dwlT17 = 100000000000000000UL;

		// Token: 0x0400042C RID: 1068
		private static readonly ulong s_dwlT18 = 1000000000000000000UL;

		// Token: 0x0400042D RID: 1069
		private static readonly ulong s_dwlT19 = 10000000000000000000UL;

		/// <summary>Represents a <see cref="T:System.DBNull" /> that can be assigned to this instance of the <see cref="T:System.Data.SqlTypes.SqlDecimal" />class.</summary>
		// Token: 0x0400042E RID: 1070
		public static readonly SqlDecimal Null = new SqlDecimal(true);

		/// <summary>A constant representing the minimum value for a <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure.</summary>
		// Token: 0x0400042F RID: 1071
		public static readonly SqlDecimal MinValue = SqlDecimal.Parse("-99999999999999999999999999999999999999");

		/// <summary>A constant representing the maximum value of a <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure.</summary>
		// Token: 0x04000430 RID: 1072
		public static readonly SqlDecimal MaxValue = SqlDecimal.Parse("99999999999999999999999999999999999999");
	}
}
