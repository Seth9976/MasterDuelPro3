using System;
using System.Globalization;

namespace System.Numerics
{
	/// <summary>Represents an arbitrarily large signed integer.</summary>
	// Token: 0x02000003 RID: 3
	[Serializable]
	public readonly struct BigInteger : IFormattable, IComparable, IComparable<BigInteger>, IEquatable<BigInteger>
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Numerics.BigInteger" /> structure using a 32-bit signed integer value.</summary>
		/// <param name="value">A 32-bit signed integer.</param>
		// Token: 0x06000002 RID: 2 RVA: 0x0000205E File Offset: 0x0000025E
		public BigInteger(int value)
		{
			if (value == -2147483648)
			{
				this = BigInteger.s_bnMinInt;
				return;
			}
			this._sign = value;
			this._bits = null;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Numerics.BigInteger" /> structure using an unsigned 32-bit integer value.</summary>
		/// <param name="value">An unsigned 32-bit integer value.</param>
		// Token: 0x06000003 RID: 3 RVA: 0x00002082 File Offset: 0x00000282
		[CLSCompliant(false)]
		public BigInteger(uint value)
		{
			if (value <= 2147483647U)
			{
				this._sign = (int)value;
				this._bits = null;
				return;
			}
			this._sign = 1;
			this._bits = new uint[1];
			this._bits[0] = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Numerics.BigInteger" /> structure using a 64-bit signed integer value.</summary>
		/// <param name="value">A 64-bit signed integer.</param>
		// Token: 0x06000004 RID: 4 RVA: 0x000020B8 File Offset: 0x000002B8
		public BigInteger(long value)
		{
			if (-2147483648L < value && value <= 2147483647L)
			{
				this._sign = (int)value;
				this._bits = null;
				return;
			}
			if (value == -2147483648L)
			{
				this = BigInteger.s_bnMinInt;
				return;
			}
			ulong num;
			if (value < 0L)
			{
				num = (ulong)(-(ulong)value);
				this._sign = -1;
			}
			else
			{
				num = (ulong)value;
				this._sign = 1;
			}
			if (num <= (ulong)(-1))
			{
				this._bits = new uint[1];
				this._bits[0] = (uint)num;
				return;
			}
			this._bits = new uint[2];
			this._bits[0] = (uint)num;
			this._bits[1] = (uint)(num >> 32);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Numerics.BigInteger" /> structure with an unsigned 64-bit integer value.</summary>
		/// <param name="value">An unsigned 64-bit integer.</param>
		// Token: 0x06000005 RID: 5 RVA: 0x00002158 File Offset: 0x00000358
		[CLSCompliant(false)]
		public BigInteger(ulong value)
		{
			if (value <= 2147483647UL)
			{
				this._sign = (int)value;
				this._bits = null;
				return;
			}
			if (value <= (ulong)(-1))
			{
				this._sign = 1;
				this._bits = new uint[1];
				this._bits[0] = (uint)value;
				return;
			}
			this._sign = 1;
			this._bits = new uint[2];
			this._bits[0] = (uint)value;
			this._bits[1] = (uint)(value >> 32);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Numerics.BigInteger" /> structure using a single-precision floating-point value.</summary>
		/// <param name="value">A single-precision floating-point value.</param>
		/// <exception cref="T:System.OverflowException">The value of <paramref name="value" /> is <see cref="F:System.Single.NaN" />.-or-The value of <paramref name="value" /> is <see cref="F:System.Single.NegativeInfinity" />.-or-The value of <paramref name="value" /> is <see cref="F:System.Single.PositiveInfinity" />.</exception>
		// Token: 0x06000006 RID: 6 RVA: 0x000021CB File Offset: 0x000003CB
		public BigInteger(float value)
		{
			this = new BigInteger((double)value);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Numerics.BigInteger" /> structure using a double-precision floating-point value.</summary>
		/// <param name="value">A double-precision floating-point value.</param>
		/// <exception cref="T:System.OverflowException">The value of <paramref name="value" /> is <see cref="F:System.Double.NaN" />.-or-The value of <paramref name="value" /> is <see cref="F:System.Double.NegativeInfinity" />.-or-The value of <paramref name="value" /> is <see cref="F:System.Double.PositiveInfinity" />.</exception>
		// Token: 0x06000007 RID: 7 RVA: 0x000021D8 File Offset: 0x000003D8
		public BigInteger(double value)
		{
			if (!double.IsFinite(value))
			{
				if (double.IsInfinity(value))
				{
					throw new OverflowException("BigInteger cannot represent infinity.");
				}
				throw new OverflowException("The value is not a number.");
			}
			else
			{
				this._sign = 0;
				this._bits = null;
				int num;
				int num2;
				ulong num3;
				bool flag;
				NumericsHelpers.GetDoubleParts(value, out num, out num2, out num3, out flag);
				if (num3 == 0UL)
				{
					this = BigInteger.Zero;
					return;
				}
				if (num2 <= 0)
				{
					if (num2 <= -64)
					{
						this = BigInteger.Zero;
						return;
					}
					this = num3 >> -num2;
					if (num < 0)
					{
						this._sign = -this._sign;
						return;
					}
				}
				else if (num2 <= 11)
				{
					this = num3 << num2;
					if (num < 0)
					{
						this._sign = -this._sign;
						return;
					}
				}
				else
				{
					num3 <<= 11;
					num2 -= 11;
					int num4 = (num2 - 1) / 32 + 1;
					int num5 = num4 * 32 - num2;
					this._bits = new uint[num4 + 2];
					this._bits[num4 + 1] = (uint)(num3 >> num5 + 32);
					this._bits[num4] = (uint)(num3 >> num5);
					if (num5 > 0)
					{
						this._bits[num4 - 1] = (uint)num3 << 32 - num5;
					}
					this._sign = num;
				}
				return;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Numerics.BigInteger" /> structure using a <see cref="T:System.Decimal" /> value.</summary>
		/// <param name="value">A decimal number.</param>
		// Token: 0x06000008 RID: 8 RVA: 0x00002314 File Offset: 0x00000514
		public BigInteger(decimal value)
		{
			int[] bits = decimal.GetBits(decimal.Truncate(value));
			int num = 3;
			while (num > 0 && bits[num - 1] == 0)
			{
				num--;
			}
			if (num == 0)
			{
				this = BigInteger.s_bnZeroInt;
				return;
			}
			if (num == 1 && bits[0] > 0)
			{
				this._sign = bits[0];
				this._sign *= (((bits[3] & int.MinValue) != 0) ? (-1) : 1);
				this._bits = null;
				return;
			}
			this._bits = new uint[num];
			this._bits[0] = (uint)bits[0];
			if (num > 1)
			{
				this._bits[1] = (uint)bits[1];
			}
			if (num > 2)
			{
				this._bits[2] = (uint)bits[2];
			}
			this._sign = (((bits[3] & int.MinValue) != 0) ? (-1) : 1);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Numerics.BigInteger" /> structure using the values in a byte array.</summary>
		/// <param name="value">An array of byte values in little-endian order.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		// Token: 0x06000009 RID: 9 RVA: 0x000023D0 File Offset: 0x000005D0
		[CLSCompliant(false)]
		public BigInteger(byte[] value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			this = new BigInteger(new ReadOnlySpan<byte>(value), false, false);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000023F0 File Offset: 0x000005F0
		public unsafe BigInteger(ReadOnlySpan<byte> value, bool isUnsigned = false, bool isBigEndian = false)
		{
			int num = value.Length;
			bool flag;
			if (num > 0)
			{
				byte b = (isBigEndian ? (*value[0]) : (*value[num - 1]));
				flag = (b & 128) != 0 && !isUnsigned;
				if (b == 0)
				{
					if (isBigEndian)
					{
						int num2 = 1;
						while (num2 < num && *value[num2] == 0)
						{
							num2++;
						}
						value = value.Slice(num2);
						num = value.Length;
					}
					else
					{
						num -= 2;
						while (num >= 0 && *value[num] == 0)
						{
							num--;
						}
						num++;
					}
				}
			}
			else
			{
				flag = false;
			}
			if (num == 0)
			{
				this._sign = 0;
				this._bits = null;
				return;
			}
			if (num <= 4)
			{
				this._sign = (flag ? (-1) : 0);
				if (isBigEndian)
				{
					for (int i = 0; i < num; i++)
					{
						this._sign = (this._sign << 8) | (int)(*value[i]);
					}
				}
				else
				{
					for (int j = num - 1; j >= 0; j--)
					{
						this._sign = (this._sign << 8) | (int)(*value[j]);
					}
				}
				this._bits = null;
				if (this._sign < 0 && !flag)
				{
					this._bits = new uint[] { (uint)this._sign };
					this._sign = 1;
				}
				if (this._sign == -2147483648)
				{
					this = BigInteger.s_bnMinInt;
					return;
				}
			}
			else
			{
				int num3 = num % 4;
				int num4 = num / 4 + ((num3 == 0) ? 0 : 1);
				uint[] array = new uint[num4];
				int num5 = num - 1;
				int l;
				if (isBigEndian)
				{
					int k = num - 4;
					for (l = 0; l < num4 - ((num3 == 0) ? 0 : 1); l++)
					{
						for (int m = 0; m < 4; m++)
						{
							byte b2 = *value[k];
							array[l] = (array[l] << 8) | (uint)b2;
							k++;
						}
						k -= 8;
					}
				}
				else
				{
					int k = 3;
					for (l = 0; l < num4 - ((num3 == 0) ? 0 : 1); l++)
					{
						for (int n = 0; n < 4; n++)
						{
							byte b3 = *value[k];
							array[l] = (array[l] << 8) | (uint)b3;
							k--;
						}
						k += 8;
					}
				}
				if (num3 != 0)
				{
					if (flag)
					{
						array[num4 - 1] = uint.MaxValue;
					}
					if (isBigEndian)
					{
						for (int k = 0; k < num3; k++)
						{
							byte b4 = *value[k];
							array[l] = (array[l] << 8) | (uint)b4;
						}
					}
					else
					{
						for (int k = num5; k >= num - num3; k--)
						{
							byte b5 = *value[k];
							array[l] = (array[l] << 8) | (uint)b5;
						}
					}
				}
				if (flag)
				{
					NumericsHelpers.DangerousMakeTwosComplement(array);
					int num6 = array.Length - 1;
					while (num6 >= 0 && array[num6] == 0U)
					{
						num6--;
					}
					num6++;
					if (num6 == 1)
					{
						uint num7 = array[0];
						if (num7 == 1U)
						{
							this = BigInteger.s_bnMinusOneInt;
							return;
						}
						if (num7 == 2147483648U)
						{
							this = BigInteger.s_bnMinInt;
							return;
						}
						if (array[0] > 0U)
						{
							this._sign = (int)(uint.MaxValue * array[0]);
							this._bits = null;
							return;
						}
					}
					if (num6 != array.Length)
					{
						this._sign = -1;
						this._bits = new uint[num6];
						Array.Copy(array, 0, this._bits, 0, num6);
						return;
					}
					this._sign = -1;
					this._bits = array;
					return;
				}
				else
				{
					this._sign = 1;
					this._bits = array;
				}
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002752 File Offset: 0x00000952
		internal BigInteger(int n, uint[] rgu)
		{
			this._sign = n;
			this._bits = rgu;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002764 File Offset: 0x00000964
		internal BigInteger(uint[] value, bool negative)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			int num = value.Length;
			while (num > 0 && value[num - 1] == 0U)
			{
				num--;
			}
			if (num == 0)
			{
				this = BigInteger.s_bnZeroInt;
				return;
			}
			if (num == 1 && value[0] < 2147483648U)
			{
				this._sign = (int)(negative ? (-(int)value[0]) : value[0]);
				this._bits = null;
				if (this._sign == -2147483648)
				{
					this = BigInteger.s_bnMinInt;
					return;
				}
			}
			else
			{
				this._sign = (negative ? (-1) : 1);
				this._bits = new uint[num];
				Array.Copy(value, 0, this._bits, 0, num);
			}
		}

		/// <summary>Gets a value that represents the number 0 (zero).</summary>
		/// <returns>An integer whose value is 0 (zero).</returns>
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000D RID: 13 RVA: 0x0000280B File Offset: 0x00000A0B
		public static BigInteger Zero
		{
			get
			{
				return BigInteger.s_bnZeroInt;
			}
		}

		/// <summary>Gets a value that represents the number negative one (-1).</summary>
		/// <returns>An integer whose value is negative one (-1).</returns>
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000E RID: 14 RVA: 0x00002812 File Offset: 0x00000A12
		public static BigInteger MinusOne
		{
			get
			{
				return BigInteger.s_bnMinusOneInt;
			}
		}

		/// <summary>Indicates whether the value of the current <see cref="T:System.Numerics.BigInteger" /> object is <see cref="P:System.Numerics.BigInteger.Zero" />.</summary>
		/// <returns>true if the value of the <see cref="T:System.Numerics.BigInteger" /> object is <see cref="P:System.Numerics.BigInteger.Zero" />; otherwise, false.</returns>
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000F RID: 15 RVA: 0x00002819 File Offset: 0x00000A19
		public bool IsZero
		{
			get
			{
				return this._sign == 0;
			}
		}

		/// <summary>Converts the string representation of a number in a specified culture-specific format to its <see cref="T:System.Numerics.BigInteger" /> equivalent.</summary>
		/// <returns>A value that is equivalent to the number specified in the <paramref name="value" /> parameter.</returns>
		/// <param name="value">A string that contains a number to convert.</param>
		/// <param name="provider">An object that provides culture-specific formatting information about <paramref name="value" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is not in the correct format.</exception>
		// Token: 0x06000010 RID: 16 RVA: 0x00002824 File Offset: 0x00000A24
		public static BigInteger Parse(string value, IFormatProvider provider)
		{
			return BigInteger.Parse(value, NumberStyles.Integer, NumberFormatInfo.GetInstance(provider));
		}

		/// <summary>Converts the string representation of a number in a specified style and culture-specific format to its <see cref="T:System.Numerics.BigInteger" /> equivalent.</summary>
		/// <returns>A value that is equivalent to the number specified in the <paramref name="value" /> parameter.</returns>
		/// <param name="value">A string that contains a number to convert.</param>
		/// <param name="style">A bitwise combination of the enumeration values that specify the permitted format of <paramref name="value" />.</param>
		/// <param name="provider">An object that provides culture-specific formatting information about <paramref name="value" />.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="style" /> is not a <see cref="T:System.Globalization.NumberStyles" /> value.-or-<paramref name="style" /> includes the <see cref="F:System.Globalization.NumberStyles.AllowHexSpecifier" /> or <see cref="F:System.Globalization.NumberStyles.HexNumber" /> flag along with another value.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> does not comply with the input pattern specified by <paramref name="style" />.</exception>
		// Token: 0x06000011 RID: 17 RVA: 0x00002833 File Offset: 0x00000A33
		public static BigInteger Parse(string value, NumberStyles style, IFormatProvider provider)
		{
			return BigNumber.ParseBigInteger(value, style, NumberFormatInfo.GetInstance(provider));
		}

		/// <summary>Returns the hash code for the current <see cref="T:System.Numerics.BigInteger" /> object.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06000012 RID: 18 RVA: 0x00002844 File Offset: 0x00000A44
		public override int GetHashCode()
		{
			if (this._bits == null)
			{
				return this._sign;
			}
			int num = this._sign;
			int num2 = this._bits.Length;
			while (--num2 >= 0)
			{
				num = NumericsHelpers.CombineHash(num, (int)this._bits[num2]);
			}
			return num;
		}

		/// <summary>Returns a value that indicates whether the current instance and a specified object have the same value.</summary>
		/// <returns>true if the <paramref name="obj" /> parameter is a <see cref="T:System.Numerics.BigInteger" /> object or a type capable of implicit conversion to a <see cref="T:System.Numerics.BigInteger" /> value, and its value is equal to the value of the current <see cref="T:System.Numerics.BigInteger" /> object; otherwise, false.</returns>
		/// <param name="obj">The object to compare. </param>
		// Token: 0x06000013 RID: 19 RVA: 0x0000288A File Offset: 0x00000A8A
		public override bool Equals(object obj)
		{
			return obj is BigInteger && this.Equals((BigInteger)obj);
		}

		/// <summary>Returns a value that indicates whether the current instance and a signed 64-bit integer have the same value.</summary>
		/// <returns>true if the signed 64-bit integer and the current instance have the same value; otherwise, false.</returns>
		/// <param name="other">The signed 64-bit integer value to compare.</param>
		// Token: 0x06000014 RID: 20 RVA: 0x000028A4 File Offset: 0x00000AA4
		public bool Equals(long other)
		{
			if (this._bits == null)
			{
				return (long)this._sign == other;
			}
			int num;
			if (((long)this._sign ^ other) < 0L || (num = this._bits.Length) > 2)
			{
				return false;
			}
			ulong num2 = (ulong)((other < 0L) ? (-(ulong)other) : other);
			if (num == 1)
			{
				return (ulong)this._bits[0] == num2;
			}
			return NumericsHelpers.MakeUlong(this._bits[1], this._bits[0]) == num2;
		}

		/// <summary>Returns a value that indicates whether the current instance and a specified <see cref="T:System.Numerics.BigInteger" /> object have the same value.</summary>
		/// <returns>true if this <see cref="T:System.Numerics.BigInteger" /> object and <paramref name="other" /> have the same value; otherwise, false.</returns>
		/// <param name="other">The object to compare.</param>
		// Token: 0x06000015 RID: 21 RVA: 0x00002914 File Offset: 0x00000B14
		public bool Equals(BigInteger other)
		{
			if (this._sign != other._sign)
			{
				return false;
			}
			if (this._bits == other._bits)
			{
				return true;
			}
			if (this._bits == null || other._bits == null)
			{
				return false;
			}
			int num = this._bits.Length;
			return num == other._bits.Length && BigInteger.GetDiffLength(this._bits, other._bits, num) == 0;
		}

		/// <summary>Compares this instance to a signed 64-bit integer and returns an integer that indicates whether the value of this instance is less than, equal to, or greater than the value of the signed 64-bit integer.</summary>
		/// <returns>A signed integer value that indicates the relationship of this instance to <paramref name="other" />, as shown in the following table.Return valueDescriptionLess than zeroThe current instance is less than <paramref name="other" />.ZeroThe current instance equals <paramref name="other" />.Greater than zeroThe current instance is greater than <paramref name="other" />.</returns>
		/// <param name="other">The signed 64-bit integer to compare.</param>
		// Token: 0x06000016 RID: 22 RVA: 0x00002980 File Offset: 0x00000B80
		public int CompareTo(long other)
		{
			if (this._bits == null)
			{
				return ((long)this._sign).CompareTo(other);
			}
			int num;
			if (((long)this._sign ^ other) < 0L || (num = this._bits.Length) > 2)
			{
				return this._sign;
			}
			ulong num2 = (ulong)((other < 0L) ? (-(ulong)other) : other);
			ulong num3 = ((num == 2) ? NumericsHelpers.MakeUlong(this._bits[1], this._bits[0]) : ((ulong)this._bits[0]));
			return this._sign * num3.CompareTo(num2);
		}

		/// <summary>Compares this instance to a second <see cref="T:System.Numerics.BigInteger" /> and returns an integer that indicates whether the value of this instance is less than, equal to, or greater than the value of the specified object.</summary>
		/// <returns>A signed integer value that indicates the relationship of this instance to <paramref name="other" />, as shown in the following table.Return valueDescriptionLess than zeroThe current instance is less than <paramref name="other" />.ZeroThe current instance equals <paramref name="other" />.Greater than zeroThe current instance is greater than <paramref name="other" />.</returns>
		/// <param name="other">The object to compare.</param>
		// Token: 0x06000017 RID: 23 RVA: 0x00002A08 File Offset: 0x00000C08
		public int CompareTo(BigInteger other)
		{
			if ((this._sign ^ other._sign) < 0)
			{
				if (this._sign >= 0)
				{
					return 1;
				}
				return -1;
			}
			else if (this._bits == null)
			{
				if (other._bits != null)
				{
					return -other._sign;
				}
				if (this._sign < other._sign)
				{
					return -1;
				}
				if (this._sign <= other._sign)
				{
					return 0;
				}
				return 1;
			}
			else
			{
				int num;
				int num2;
				if (other._bits == null || (num = this._bits.Length) > (num2 = other._bits.Length))
				{
					return this._sign;
				}
				if (num < num2)
				{
					return -this._sign;
				}
				int diffLength = BigInteger.GetDiffLength(this._bits, other._bits, num);
				if (diffLength == 0)
				{
					return 0;
				}
				if (this._bits[diffLength - 1] >= other._bits[diffLength - 1])
				{
					return this._sign;
				}
				return -this._sign;
			}
		}

		/// <summary>Compares this instance to a specified object and returns an integer that indicates whether the value of this instance is less than, equal to, or greater than the value of the specified object.</summary>
		/// <returns>A signed integer that indicates the relationship of the current instance to the <paramref name="obj" /> parameter, as shown in the following table.Return valueDescriptionLess than zeroThe current instance is less than <paramref name="obj" />.ZeroThe current instance equals <paramref name="obj" />.Greater than zeroThe current instance is greater than <paramref name="obj" />, or the <paramref name="obj" /> parameter is null. </returns>
		/// <param name="obj">The object to compare.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="obj" /> is not a <see cref="T:System.Numerics.BigInteger" />. </exception>
		// Token: 0x06000018 RID: 24 RVA: 0x00002AD9 File Offset: 0x00000CD9
		public int CompareTo(object obj)
		{
			if (obj == null)
			{
				return 1;
			}
			if (!(obj is BigInteger))
			{
				throw new ArgumentException("The parameter must be a BigInteger.", "obj");
			}
			return this.CompareTo((BigInteger)obj);
		}

		/// <summary>Converts a <see cref="T:System.Numerics.BigInteger" /> value to a byte array.</summary>
		/// <returns>The value of the current <see cref="T:System.Numerics.BigInteger" /> object converted to an array of bytes.</returns>
		// Token: 0x06000019 RID: 25 RVA: 0x00002B04 File Offset: 0x00000D04
		public byte[] ToByteArray()
		{
			return this.ToByteArray(false, false);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002B10 File Offset: 0x00000D10
		public byte[] ToByteArray(bool isUnsigned = false, bool isBigEndian = false)
		{
			int num = 0;
			return this.TryGetBytes(BigInteger.GetBytesMode.AllocateArray, default(Span<byte>), isUnsigned, isBigEndian, ref num);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002B33 File Offset: 0x00000D33
		public bool TryWriteBytes(Span<byte> destination, out int bytesWritten, bool isUnsigned = false, bool isBigEndian = false)
		{
			bytesWritten = 0;
			if (this.TryGetBytes(BigInteger.GetBytesMode.Span, destination, isUnsigned, isBigEndian, ref bytesWritten) == null)
			{
				bytesWritten = 0;
				return false;
			}
			return true;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002B4C File Offset: 0x00000D4C
		internal bool TryWriteOrCountBytes(Span<byte> destination, out int bytesWritten, bool isUnsigned = false, bool isBigEndian = false)
		{
			bytesWritten = 0;
			return this.TryGetBytes(BigInteger.GetBytesMode.Span, destination, isUnsigned, isBigEndian, ref bytesWritten) != null;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002B60 File Offset: 0x00000D60
		private unsafe byte[] TryGetBytes(BigInteger.GetBytesMode mode, Span<byte> destination, bool isUnsigned, bool isBigEndian, ref int bytesWritten)
		{
			int sign = this._sign;
			if (sign == 0)
			{
				if (mode == BigInteger.GetBytesMode.AllocateArray)
				{
					return new byte[1];
				}
				if (mode == BigInteger.GetBytesMode.Count)
				{
					bytesWritten = 1;
					return null;
				}
				bytesWritten = 1;
				if (destination.Length != 0)
				{
					*destination[0] = 0;
					return BigInteger.s_success;
				}
				return null;
			}
			else
			{
				if (isUnsigned && sign < 0)
				{
					throw new OverflowException("Negative values do not have an unsigned representation.");
				}
				int num = 0;
				uint[] bits = this._bits;
				byte b;
				uint num2;
				if (bits == null)
				{
					b = ((sign < 0) ? byte.MaxValue : 0);
					num2 = (uint)sign;
				}
				else if (sign == -1)
				{
					b = byte.MaxValue;
					while (bits[num] == 0U)
					{
						num++;
					}
					num2 = ~bits[bits.Length - 1];
					if (bits.Length - 1 == num)
					{
						num2 += 1U;
					}
				}
				else
				{
					b = 0;
					num2 = bits[bits.Length - 1];
				}
				byte b2;
				int num3;
				if ((b2 = (byte)(num2 >> 24)) != b)
				{
					num3 = 3;
				}
				else if ((b2 = (byte)(num2 >> 16)) != b)
				{
					num3 = 2;
				}
				else if ((b2 = (byte)(num2 >> 8)) != b)
				{
					num3 = 1;
				}
				else
				{
					b2 = (byte)num2;
					num3 = 0;
				}
				bool flag = (b2 & 128) != (b & 128) && !isUnsigned;
				int num4 = num3 + 1 + (flag ? 1 : 0);
				if (bits != null)
				{
					num4 = checked(4 * (bits.Length - 1) + num4);
				}
				byte[] array;
				if (mode != BigInteger.GetBytesMode.AllocateArray)
				{
					if (mode == BigInteger.GetBytesMode.Count)
					{
						bytesWritten = num4;
						return null;
					}
					bytesWritten = num4;
					if (destination.Length < num4)
					{
						return null;
					}
					array = BigInteger.s_success;
				}
				else
				{
					destination = (array = new byte[num4]);
				}
				int num5 = (isBigEndian ? (num4 - 1) : 0);
				int num6 = (isBigEndian ? (-1) : 1);
				if (bits != null)
				{
					for (int i = 0; i < bits.Length - 1; i++)
					{
						uint num7 = bits[i];
						if (sign == -1)
						{
							num7 = ~num7;
							if (i <= num)
							{
								num7 += 1U;
							}
						}
						*destination[num5] = (byte)num7;
						num5 += num6;
						*destination[num5] = (byte)(num7 >> 8);
						num5 += num6;
						*destination[num5] = (byte)(num7 >> 16);
						num5 += num6;
						*destination[num5] = (byte)(num7 >> 24);
						num5 += num6;
					}
				}
				*destination[num5] = (byte)num2;
				if (num3 != 0)
				{
					num5 += num6;
					*destination[num5] = (byte)(num2 >> 8);
					if (num3 != 1)
					{
						num5 += num6;
						*destination[num5] = (byte)(num2 >> 16);
						if (num3 != 2)
						{
							num5 += num6;
							*destination[num5] = (byte)(num2 >> 24);
						}
					}
				}
				if (flag)
				{
					num5 += num6;
					*destination[num5] = b;
				}
				return array;
			}
		}

		/// <summary>Converts the numeric value of the current <see cref="T:System.Numerics.BigInteger" /> object to its equivalent string representation.</summary>
		/// <returns>The string representation of the current <see cref="T:System.Numerics.BigInteger" /> value.</returns>
		// Token: 0x0600001E RID: 30 RVA: 0x00002DE2 File Offset: 0x00000FE2
		public override string ToString()
		{
			return BigNumber.FormatBigInteger(this, null, NumberFormatInfo.CurrentInfo);
		}

		/// <summary>Converts the numeric value of the current <see cref="T:System.Numerics.BigInteger" /> object to its equivalent string representation by using the specified culture-specific formatting information.</summary>
		/// <returns>The string representation of the current <see cref="T:System.Numerics.BigInteger" /> value in the format specified by the <paramref name="provider" /> parameter.</returns>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		// Token: 0x0600001F RID: 31 RVA: 0x00002DF5 File Offset: 0x00000FF5
		public string ToString(IFormatProvider provider)
		{
			return BigNumber.FormatBigInteger(this, null, NumberFormatInfo.GetInstance(provider));
		}

		/// <summary>Converts the numeric value of the current <see cref="T:System.Numerics.BigInteger" /> object to its equivalent string representation by using the specified format and culture-specific format information.</summary>
		/// <returns>The string representation of the current <see cref="T:System.Numerics.BigInteger" /> value as specified by the <paramref name="format" /> and <paramref name="provider" /> parameters.</returns>
		/// <param name="format">A standard or custom numeric format string.</param>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="format" /> is not a valid format string.</exception>
		// Token: 0x06000020 RID: 32 RVA: 0x00002E09 File Offset: 0x00001009
		public string ToString(string format, IFormatProvider provider)
		{
			return BigNumber.FormatBigInteger(this, format, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002E20 File Offset: 0x00001020
		private static BigInteger Add(uint[] leftBits, int leftSign, uint[] rightBits, int rightSign)
		{
			bool flag = leftBits == null;
			bool flag2 = rightBits == null;
			if (flag && flag2)
			{
				return (long)leftSign + (long)rightSign;
			}
			if (flag)
			{
				return new BigInteger(BigIntegerCalculator.Add(rightBits, NumericsHelpers.Abs(leftSign)), leftSign < 0);
			}
			if (flag2)
			{
				return new BigInteger(BigIntegerCalculator.Add(leftBits, NumericsHelpers.Abs(rightSign)), leftSign < 0);
			}
			if (leftBits.Length < rightBits.Length)
			{
				return new BigInteger(BigIntegerCalculator.Add(rightBits, leftBits), leftSign < 0);
			}
			return new BigInteger(BigIntegerCalculator.Add(leftBits, rightBits), leftSign < 0);
		}

		/// <summary>Subtracts a <see cref="T:System.Numerics.BigInteger" /> value from another <see cref="T:System.Numerics.BigInteger" /> value.</summary>
		/// <returns>The result of subtracting <paramref name="right" /> from <paramref name="left" />.</returns>
		/// <param name="left">The value to subtract from (the minuend).</param>
		/// <param name="right">The value to subtract (the subtrahend).</param>
		// Token: 0x06000022 RID: 34 RVA: 0x00002EA4 File Offset: 0x000010A4
		public static BigInteger operator -(BigInteger left, BigInteger right)
		{
			if (left._sign < 0 != right._sign < 0)
			{
				return BigInteger.Add(left._bits, left._sign, right._bits, -1 * right._sign);
			}
			return BigInteger.Subtract(left._bits, left._sign, right._bits, right._sign);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002F04 File Offset: 0x00001104
		private static BigInteger Subtract(uint[] leftBits, int leftSign, uint[] rightBits, int rightSign)
		{
			bool flag = leftBits == null;
			bool flag2 = rightBits == null;
			if (flag && flag2)
			{
				return (long)leftSign - (long)rightSign;
			}
			if (flag)
			{
				return new BigInteger(BigIntegerCalculator.Subtract(rightBits, NumericsHelpers.Abs(leftSign)), leftSign >= 0);
			}
			if (flag2)
			{
				return new BigInteger(BigIntegerCalculator.Subtract(leftBits, NumericsHelpers.Abs(rightSign)), leftSign < 0);
			}
			if (BigIntegerCalculator.Compare(leftBits, rightBits) < 0)
			{
				return new BigInteger(BigIntegerCalculator.Subtract(rightBits, leftBits), leftSign >= 0);
			}
			return new BigInteger(BigIntegerCalculator.Subtract(leftBits, rightBits), leftSign < 0);
		}

		/// <summary>Defines an implicit conversion of an unsigned byte to a <see cref="T:System.Numerics.BigInteger" /> value.</summary>
		/// <returns>An object that contains the value of the <paramref name="value" /> parameter.</returns>
		/// <param name="value">The value to convert to a <see cref="T:System.Numerics.BigInteger" />.</param>
		// Token: 0x06000024 RID: 36 RVA: 0x00002F8E File Offset: 0x0000118E
		public static implicit operator BigInteger(byte value)
		{
			return new BigInteger((int)value);
		}

		/// <summary>Defines an implicit conversion of an 8-bit signed integer to a <see cref="T:System.Numerics.BigInteger" /> value.</summary>
		/// <returns>An object that contains the value of the <paramref name="value" /> parameter.</returns>
		/// <param name="value">The value to convert to a <see cref="T:System.Numerics.BigInteger" />.</param>
		// Token: 0x06000025 RID: 37 RVA: 0x00002F8E File Offset: 0x0000118E
		[CLSCompliant(false)]
		public static implicit operator BigInteger(sbyte value)
		{
			return new BigInteger((int)value);
		}

		/// <summary>Defines an implicit conversion of a signed 16-bit integer to a <see cref="T:System.Numerics.BigInteger" /> value.</summary>
		/// <returns>An object that contains the value of the <paramref name="value" /> parameter.</returns>
		/// <param name="value">The value to convert to a <see cref="T:System.Numerics.BigInteger" />.</param>
		// Token: 0x06000026 RID: 38 RVA: 0x00002F8E File Offset: 0x0000118E
		public static implicit operator BigInteger(short value)
		{
			return new BigInteger((int)value);
		}

		/// <summary>Defines an implicit conversion of a 16-bit unsigned integer to a <see cref="T:System.Numerics.BigInteger" /> value.</summary>
		/// <returns>An object that contains the value of the <paramref name="value" /> parameter.</returns>
		/// <param name="value">The value to convert to a <see cref="T:System.Numerics.BigInteger" />.</param>
		// Token: 0x06000027 RID: 39 RVA: 0x00002F8E File Offset: 0x0000118E
		[CLSCompliant(false)]
		public static implicit operator BigInteger(ushort value)
		{
			return new BigInteger((int)value);
		}

		/// <summary>Defines an implicit conversion of a signed 32-bit integer to a <see cref="T:System.Numerics.BigInteger" /> value.</summary>
		/// <returns>An object that contains the value of the <paramref name="value" /> parameter.</returns>
		/// <param name="value">The value to convert to a <see cref="T:System.Numerics.BigInteger" />.</param>
		// Token: 0x06000028 RID: 40 RVA: 0x00002F8E File Offset: 0x0000118E
		public static implicit operator BigInteger(int value)
		{
			return new BigInteger(value);
		}

		/// <summary>Defines an implicit conversion of a 32-bit unsigned integer to a <see cref="T:System.Numerics.BigInteger" /> value.</summary>
		/// <returns>An object that contains the value of the <paramref name="value" /> parameter.</returns>
		/// <param name="value">The value to convert to a <see cref="T:System.Numerics.BigInteger" />.</param>
		// Token: 0x06000029 RID: 41 RVA: 0x00002F96 File Offset: 0x00001196
		[CLSCompliant(false)]
		public static implicit operator BigInteger(uint value)
		{
			return new BigInteger(value);
		}

		/// <summary>Defines an implicit conversion of a signed 64-bit integer to a <see cref="T:System.Numerics.BigInteger" /> value.</summary>
		/// <returns>An object that contains the value of the <paramref name="value" /> parameter.</returns>
		/// <param name="value">The value to convert to a <see cref="T:System.Numerics.BigInteger" />.</param>
		// Token: 0x0600002A RID: 42 RVA: 0x00002F9E File Offset: 0x0000119E
		public static implicit operator BigInteger(long value)
		{
			return new BigInteger(value);
		}

		/// <summary>Defines an implicit conversion of a 64-bit unsigned integer to a <see cref="T:System.Numerics.BigInteger" /> value.</summary>
		/// <returns>An object that contains the value of the <paramref name="value" /> parameter.</returns>
		/// <param name="value">The value to convert to a <see cref="T:System.Numerics.BigInteger" />.</param>
		// Token: 0x0600002B RID: 43 RVA: 0x00002FA6 File Offset: 0x000011A6
		[CLSCompliant(false)]
		public static implicit operator BigInteger(ulong value)
		{
			return new BigInteger(value);
		}

		/// <summary>Defines an explicit conversion of a <see cref="T:System.Numerics.BigInteger" /> object to an unsigned byte value.</summary>
		/// <returns>An object that contains the value of the <paramref name="value" /> parameter.</returns>
		/// <param name="value">The value to convert to a <see cref="T:System.Byte" />.</param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than <see cref="F:System.Byte.MinValue" />. -or-<paramref name="value" /> is greater than <see cref="F:System.Byte.MaxValue" />.</exception>
		// Token: 0x0600002C RID: 44 RVA: 0x00002FAE File Offset: 0x000011AE
		public static explicit operator byte(BigInteger value)
		{
			return checked((byte)(int)value);
		}

		/// <summary>Defines an explicit conversion of a <see cref="T:System.Numerics.BigInteger" /> object to a signed 8-bit value.</summary>
		/// <returns>An object that contains the value of the <paramref name="value" /> parameter.</returns>
		/// <param name="value">The value to convert to a signed 8-bit value.</param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than <see cref="F:System.SByte.MinValue" />.-or-<paramref name="value" /> is greater than <see cref="F:System.SByte.MaxValue" />.</exception>
		// Token: 0x0600002D RID: 45 RVA: 0x00002FB7 File Offset: 0x000011B7
		[CLSCompliant(false)]
		public static explicit operator sbyte(BigInteger value)
		{
			return checked((sbyte)(int)value);
		}

		/// <summary>Defines an explicit conversion of a <see cref="T:System.Numerics.BigInteger" /> object to a 16-bit signed integer value.</summary>
		/// <returns>An object that contains the value of the <paramref name="value" /> parameter.</returns>
		/// <param name="value">The value to convert to a 16-bit signed integer.</param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than <see cref="F:System.Int16.MinValue" />.-or-<paramref name="value" /> is greater than <see cref="F:System.Int16.MaxValue" />.</exception>
		// Token: 0x0600002E RID: 46 RVA: 0x00002FC0 File Offset: 0x000011C0
		public static explicit operator short(BigInteger value)
		{
			return checked((short)(int)value);
		}

		/// <summary>Defines an explicit conversion of a <see cref="T:System.Numerics.BigInteger" /> object to an unsigned 16-bit integer value.</summary>
		/// <returns>An object that contains the value of the <paramref name="value" /> parameter</returns>
		/// <param name="value">The value to convert to an unsigned 16-bit integer.</param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than <see cref="F:System.UInt16.MinValue" />.-or-<paramref name="value" /> is greater than <see cref="F:System.UInt16.MaxValue" />. </exception>
		// Token: 0x0600002F RID: 47 RVA: 0x00002FC9 File Offset: 0x000011C9
		[CLSCompliant(false)]
		public static explicit operator ushort(BigInteger value)
		{
			return checked((ushort)(int)value);
		}

		/// <summary>Defines an explicit conversion of a <see cref="T:System.Numerics.BigInteger" /> object to a 32-bit signed integer value.</summary>
		/// <returns>An object that contains the value of the <paramref name="value" /> parameter.</returns>
		/// <param name="value">The value to convert to a 32-bit signed integer. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than <see cref="F:System.Int32.MinValue" />.-or-<paramref name="value" /> is greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		// Token: 0x06000030 RID: 48 RVA: 0x00002FD4 File Offset: 0x000011D4
		public static explicit operator int(BigInteger value)
		{
			if (value._bits == null)
			{
				return value._sign;
			}
			if (value._bits.Length > 1)
			{
				throw new OverflowException("Value was either too large or too small for an Int32.");
			}
			if (value._sign > 0)
			{
				return checked((int)value._bits[0]);
			}
			if (value._bits[0] > 2147483648U)
			{
				throw new OverflowException("Value was either too large or too small for an Int32.");
			}
			return (int)(-(int)value._bits[0]);
		}

		/// <summary>Defines an explicit conversion of a <see cref="T:System.Numerics.BigInteger" /> object to an unsigned 32-bit integer value.</summary>
		/// <returns>An object that contains the value of the <paramref name="value" /> parameter.</returns>
		/// <param name="value">The value to convert to an unsigned 32-bit integer.</param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than <see cref="F:System.UInt32.MinValue" />.-or-<paramref name="value" /> is greater than <see cref="F:System.UInt32.MaxValue" />.</exception>
		// Token: 0x06000031 RID: 49 RVA: 0x0000303C File Offset: 0x0000123C
		[CLSCompliant(false)]
		public static explicit operator uint(BigInteger value)
		{
			if (value._bits == null)
			{
				return checked((uint)value._sign);
			}
			if (value._bits.Length > 1 || value._sign < 0)
			{
				throw new OverflowException("Value was either too large or too small for a UInt32.");
			}
			return value._bits[0];
		}

		/// <summary>Defines an explicit conversion of a <see cref="T:System.Numerics.BigInteger" /> object to a 64-bit signed integer value.</summary>
		/// <returns>An object that contains the value of the <paramref name="value" /> parameter.</returns>
		/// <param name="value">The value to convert to a 64-bit signed integer.</param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than <see cref="F:System.Int64.MinValue" />.-or-<paramref name="value" /> is greater than <see cref="F:System.Int64.MaxValue" />.</exception>
		// Token: 0x06000032 RID: 50 RVA: 0x00003078 File Offset: 0x00001278
		public static explicit operator long(BigInteger value)
		{
			if (value._bits == null)
			{
				return (long)value._sign;
			}
			int num = value._bits.Length;
			if (num > 2)
			{
				throw new OverflowException("Value was either too large or too small for an Int64.");
			}
			ulong num2;
			if (num > 1)
			{
				num2 = NumericsHelpers.MakeUlong(value._bits[1], value._bits[0]);
			}
			else
			{
				num2 = (ulong)value._bits[0];
			}
			long num3 = (long)((value._sign > 0) ? num2 : (-(long)num2));
			if ((num3 > 0L && value._sign > 0) || (num3 < 0L && value._sign < 0))
			{
				return num3;
			}
			throw new OverflowException("Value was either too large or too small for an Int64.");
		}

		/// <summary>Defines an explicit conversion of a <see cref="T:System.Numerics.BigInteger" /> object to an unsigned 64-bit integer value.</summary>
		/// <returns>An object that contains the value of the <paramref name="value" /> parameter.</returns>
		/// <param name="value">The value to convert to an unsigned 64-bit integer.</param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than <see cref="F:System.UInt64.MinValue" />.-or-<paramref name="value" /> is greater than <see cref="F:System.UInt64.MaxValue" />. </exception>
		// Token: 0x06000033 RID: 51 RVA: 0x00003108 File Offset: 0x00001308
		[CLSCompliant(false)]
		public static explicit operator ulong(BigInteger value)
		{
			if (value._bits == null)
			{
				return checked((ulong)value._sign);
			}
			int num = value._bits.Length;
			if (num > 2 || value._sign < 0)
			{
				throw new OverflowException("Value was either too large or too small for a UInt64.");
			}
			if (num > 1)
			{
				return NumericsHelpers.MakeUlong(value._bits[1], value._bits[0]);
			}
			return (ulong)value._bits[0];
		}

		/// <summary>Defines an explicit conversion of a <see cref="T:System.Numerics.BigInteger" /> object to a single-precision floating-point value.</summary>
		/// <returns>An object that contains the closest possible representation of the <paramref name="value" /> parameter.</returns>
		/// <param name="value">The value to convert to a single-precision floating-point value.</param>
		// Token: 0x06000034 RID: 52 RVA: 0x00003167 File Offset: 0x00001367
		public static explicit operator float(BigInteger value)
		{
			return (float)(double)value;
		}

		/// <summary>Defines an explicit conversion of a <see cref="T:System.Numerics.BigInteger" /> object to a <see cref="T:System.Double" /> value.</summary>
		/// <returns>An object that contains the value of the <paramref name="value" /> parameter.</returns>
		/// <param name="value">The value to convert to a <see cref="T:System.Double" />.</param>
		// Token: 0x06000035 RID: 53 RVA: 0x00003174 File Offset: 0x00001374
		public static explicit operator double(BigInteger value)
		{
			int sign = value._sign;
			uint[] bits = value._bits;
			if (bits == null)
			{
				return (double)sign;
			}
			int num = bits.Length;
			if (num <= 32)
			{
				ulong num2 = (ulong)bits[num - 1];
				ulong num3 = (ulong)((num > 1) ? bits[num - 2] : 0U);
				ulong num4 = (ulong)((num > 2) ? bits[num - 3] : 0U);
				int num5 = NumericsHelpers.CbitHighZero((uint)num2);
				int num6 = (num - 2) * 32 - num5;
				ulong num7 = (num2 << 32 + num5) | (num3 << num5) | (num4 >> 32 - num5);
				return NumericsHelpers.GetDoubleFromParts(sign, num6, num7);
			}
			if (sign == 1)
			{
				return double.PositiveInfinity;
			}
			return double.NegativeInfinity;
		}

		/// <summary>Defines an explicit conversion of a <see cref="T:System.Numerics.BigInteger" /> object to a <see cref="T:System.Decimal" /> value.</summary>
		/// <returns>An object that contains the value of the <paramref name="value" /> parameter.</returns>
		/// <param name="value">The value to convert to a <see cref="T:System.Decimal" />.</param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than <see cref="F:System.Decimal.MinValue" />.-or-<paramref name="value" /> is greater than <see cref="F:System.Decimal.MaxValue" />.</exception>
		// Token: 0x06000036 RID: 54 RVA: 0x00003218 File Offset: 0x00001418
		public static explicit operator decimal(BigInteger value)
		{
			if (value._bits == null)
			{
				return value._sign;
			}
			int num = value._bits.Length;
			if (num > 3)
			{
				throw new OverflowException("Value was either too large or too small for a Decimal.");
			}
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			if (num > 2)
			{
				num4 = (int)value._bits[2];
			}
			if (num > 1)
			{
				num3 = (int)value._bits[1];
			}
			if (num > 0)
			{
				num2 = (int)value._bits[0];
			}
			return new decimal(num2, num3, num4, value._sign < 0, 0);
		}

		/// <summary>Shifts a <see cref="T:System.Numerics.BigInteger" /> value a specified number of bits to the left.</summary>
		/// <returns>A value that has been shifted to the left by the specified number of bits.</returns>
		/// <param name="value">The value whose bits are to be shifted.</param>
		/// <param name="shift">The number of bits to shift <paramref name="value" /> to the left.</param>
		// Token: 0x06000037 RID: 55 RVA: 0x00003290 File Offset: 0x00001490
		public static BigInteger operator <<(BigInteger value, int shift)
		{
			if (shift == 0)
			{
				return value;
			}
			if (shift == -2147483648)
			{
				return value >> int.MaxValue >> 1;
			}
			if (shift < 0)
			{
				return value >> -shift;
			}
			int num = shift / 32;
			int num2 = shift - num * 32;
			uint[] array;
			int num3;
			bool partsForBitManipulation = BigInteger.GetPartsForBitManipulation(ref value, out array, out num3);
			uint[] array2 = new uint[num3 + num + 1];
			if (num2 == 0)
			{
				for (int i = 0; i < num3; i++)
				{
					array2[i + num] = array[i];
				}
			}
			else
			{
				int num4 = 32 - num2;
				uint num5 = 0U;
				int j;
				for (j = 0; j < num3; j++)
				{
					uint num6 = array[j];
					array2[j + num] = (num6 << num2) | num5;
					num5 = num6 >> num4;
				}
				array2[j + num] = num5;
			}
			return new BigInteger(array2, partsForBitManipulation);
		}

		/// <summary>Shifts a <see cref="T:System.Numerics.BigInteger" /> value a specified number of bits to the right.</summary>
		/// <returns>A value that has been shifted to the right by the specified number of bits.</returns>
		/// <param name="value">The value whose bits are to be shifted.</param>
		/// <param name="shift">The number of bits to shift <paramref name="value" /> to the right.</param>
		// Token: 0x06000038 RID: 56 RVA: 0x0000335C File Offset: 0x0000155C
		public static BigInteger operator >>(BigInteger value, int shift)
		{
			if (shift == 0)
			{
				return value;
			}
			if (shift == -2147483648)
			{
				return value << int.MaxValue << 1;
			}
			if (shift < 0)
			{
				return value << -shift;
			}
			int num = shift / 32;
			int num2 = shift - num * 32;
			uint[] array;
			int num3;
			bool partsForBitManipulation = BigInteger.GetPartsForBitManipulation(ref value, out array, out num3);
			if (partsForBitManipulation)
			{
				if (shift >= 32 * num3)
				{
					return BigInteger.MinusOne;
				}
				uint[] array2 = new uint[num3];
				Array.Copy(array, 0, array2, 0, num3);
				array = array2;
				NumericsHelpers.DangerousMakeTwosComplement(array);
			}
			int num4 = num3 - num;
			if (num4 < 0)
			{
				num4 = 0;
			}
			uint[] array3 = new uint[num4];
			if (num2 == 0)
			{
				for (int i = num3 - 1; i >= num; i--)
				{
					array3[i - num] = array[i];
				}
			}
			else
			{
				int num5 = 32 - num2;
				uint num6 = 0U;
				for (int j = num3 - 1; j >= num; j--)
				{
					uint num7 = array[j];
					if (partsForBitManipulation && j == num3 - 1)
					{
						array3[j - num] = (num7 >> num2) | (uint.MaxValue << num5);
					}
					else
					{
						array3[j - num] = (num7 >> num2) | num6;
					}
					num6 = num7 << num5;
				}
			}
			if (partsForBitManipulation)
			{
				NumericsHelpers.DangerousMakeTwosComplement(array3);
			}
			return new BigInteger(array3, partsForBitManipulation);
		}

		/// <summary>Negates a specified BigInteger value. </summary>
		/// <returns>The result of the <paramref name="value" /> parameter multiplied by negative one (-1).</returns>
		/// <param name="value">The value to negate.</param>
		// Token: 0x06000039 RID: 57 RVA: 0x00003486 File Offset: 0x00001686
		public static BigInteger operator -(BigInteger value)
		{
			return new BigInteger(-value._sign, value._bits);
		}

		/// <summary>Adds the values of two specified <see cref="T:System.Numerics.BigInteger" /> objects.</summary>
		/// <returns>The sum of <paramref name="left" /> and <paramref name="right" />.</returns>
		/// <param name="left">The first value to add.</param>
		/// <param name="right">The second value to add.</param>
		// Token: 0x0600003A RID: 58 RVA: 0x0000349C File Offset: 0x0000169C
		public static BigInteger operator +(BigInteger left, BigInteger right)
		{
			if (left._sign < 0 != right._sign < 0)
			{
				return BigInteger.Subtract(left._bits, left._sign, right._bits, -1 * right._sign);
			}
			return BigInteger.Add(left._bits, left._sign, right._bits, right._sign);
		}

		/// <summary>Multiplies two specified <see cref="T:System.Numerics.BigInteger" /> values.</summary>
		/// <returns>The product of <paramref name="left" /> and <paramref name="right" />.</returns>
		/// <param name="left">The first value to multiply.</param>
		/// <param name="right">The second value to multiply.</param>
		// Token: 0x0600003B RID: 59 RVA: 0x000034FC File Offset: 0x000016FC
		public static BigInteger operator *(BigInteger left, BigInteger right)
		{
			bool flag = left._bits == null;
			bool flag2 = right._bits == null;
			if (flag && flag2)
			{
				return (long)left._sign * (long)right._sign;
			}
			if (flag)
			{
				return new BigInteger(BigIntegerCalculator.Multiply(right._bits, NumericsHelpers.Abs(left._sign)), (left._sign < 0) ^ (right._sign < 0));
			}
			if (flag2)
			{
				return new BigInteger(BigIntegerCalculator.Multiply(left._bits, NumericsHelpers.Abs(right._sign)), (left._sign < 0) ^ (right._sign < 0));
			}
			if (left._bits == right._bits)
			{
				return new BigInteger(BigIntegerCalculator.Square(left._bits), (left._sign < 0) ^ (right._sign < 0));
			}
			if (left._bits.Length < right._bits.Length)
			{
				return new BigInteger(BigIntegerCalculator.Multiply(right._bits, left._bits), (left._sign < 0) ^ (right._sign < 0));
			}
			return new BigInteger(BigIntegerCalculator.Multiply(left._bits, right._bits), (left._sign < 0) ^ (right._sign < 0));
		}

		/// <summary>Divides a specified <see cref="T:System.Numerics.BigInteger" /> value by another specified <see cref="T:System.Numerics.BigInteger" /> value by using integer division.</summary>
		/// <returns>The integral result of the division.</returns>
		/// <param name="dividend">The value to be divided.</param>
		/// <param name="divisor">The value to divide by.</param>
		/// <exception cref="T:System.DivideByZeroException">
		///   <paramref name="divisor" /> is 0 (zero).</exception>
		// Token: 0x0600003C RID: 60 RVA: 0x00003634 File Offset: 0x00001834
		public static BigInteger operator /(BigInteger dividend, BigInteger divisor)
		{
			bool flag = dividend._bits == null;
			bool flag2 = divisor._bits == null;
			if (flag && flag2)
			{
				return dividend._sign / divisor._sign;
			}
			if (flag)
			{
				return BigInteger.s_bnZeroInt;
			}
			if (flag2)
			{
				return new BigInteger(BigIntegerCalculator.Divide(dividend._bits, NumericsHelpers.Abs(divisor._sign)), (dividend._sign < 0) ^ (divisor._sign < 0));
			}
			if (dividend._bits.Length < divisor._bits.Length)
			{
				return BigInteger.s_bnZeroInt;
			}
			return new BigInteger(BigIntegerCalculator.Divide(dividend._bits, divisor._bits), (dividend._sign < 0) ^ (divisor._sign < 0));
		}

		/// <summary>Returns the remainder that results from division with two specified <see cref="T:System.Numerics.BigInteger" /> values.</summary>
		/// <returns>The remainder that results from the division.</returns>
		/// <param name="dividend">The value to be divided.</param>
		/// <param name="divisor">The value to divide by.</param>
		/// <exception cref="T:System.DivideByZeroException">
		///   <paramref name="divisor" /> is 0 (zero).</exception>
		// Token: 0x0600003D RID: 61 RVA: 0x000036EC File Offset: 0x000018EC
		public static BigInteger operator %(BigInteger dividend, BigInteger divisor)
		{
			bool flag = dividend._bits == null;
			bool flag2 = divisor._bits == null;
			if (flag && flag2)
			{
				return dividend._sign % divisor._sign;
			}
			if (flag)
			{
				return dividend;
			}
			if (flag2)
			{
				uint num = BigIntegerCalculator.Remainder(dividend._bits, NumericsHelpers.Abs(divisor._sign));
				return (long)((dividend._sign < 0) ? (ulong.MaxValue * (ulong)num) : ((ulong)num));
			}
			if (dividend._bits.Length < divisor._bits.Length)
			{
				return dividend;
			}
			return new BigInteger(BigIntegerCalculator.Remainder(dividend._bits, divisor._bits), dividend._sign < 0);
		}

		/// <summary>Returns a value that indicates whether a <see cref="T:System.Numerics.BigInteger" /> value is less than or equal to another <see cref="T:System.Numerics.BigInteger" /> value.</summary>
		/// <returns>true if <paramref name="left" /> is less than or equal to <paramref name="right" />; otherwise, false.</returns>
		/// <param name="left">The first value to compare.</param>
		/// <param name="right">The second value to compare.</param>
		// Token: 0x0600003E RID: 62 RVA: 0x0000378F File Offset: 0x0000198F
		public static bool operator <=(BigInteger left, BigInteger right)
		{
			return left.CompareTo(right) <= 0;
		}

		/// <summary>Returns a value that indicates whether two <see cref="T:System.Numerics.BigInteger" /> objects have different values.</summary>
		/// <returns>true if <paramref name="left" /> and <paramref name="right" /> are not equal; otherwise, false.</returns>
		/// <param name="left">The first value to compare.</param>
		/// <param name="right">The second value to compare.</param>
		// Token: 0x0600003F RID: 63 RVA: 0x0000379F File Offset: 0x0000199F
		public static bool operator !=(BigInteger left, BigInteger right)
		{
			return !left.Equals(right);
		}

		/// <summary>Returns a value that indicates whether a <see cref="T:System.Numerics.BigInteger" /> value is less than a 64-bit signed integer.</summary>
		/// <returns>true if <paramref name="left" /> is less than <paramref name="right" />; otherwise, false.</returns>
		/// <param name="left">The first value to compare.</param>
		/// <param name="right">The second value to compare.</param>
		// Token: 0x06000040 RID: 64 RVA: 0x000037AC File Offset: 0x000019AC
		public static bool operator <(BigInteger left, long right)
		{
			return left.CompareTo(right) < 0;
		}

		/// <summary>Returns a value that indicates whether a <see cref="T:System.Numerics.BigInteger" /> value is less than or equal to a 64-bit signed integer.</summary>
		/// <returns>true if <paramref name="left" /> is less than or equal to <paramref name="right" />; otherwise, false.</returns>
		/// <param name="left">The first value to compare.</param>
		/// <param name="right">The second value to compare.</param>
		// Token: 0x06000041 RID: 65 RVA: 0x000037B9 File Offset: 0x000019B9
		public static bool operator <=(BigInteger left, long right)
		{
			return left.CompareTo(right) <= 0;
		}

		/// <summary>Returns a value that indicates whether a <see cref="T:System.Numerics.BigInteger" /> value and a signed long integer value are equal.</summary>
		/// <returns>true if the <paramref name="left" /> and <paramref name="right" /> parameters have the same value; otherwise, false.</returns>
		/// <param name="left">The first value to compare.</param>
		/// <param name="right">The second value to compare.</param>
		// Token: 0x06000042 RID: 66 RVA: 0x000037C9 File Offset: 0x000019C9
		public static bool operator ==(BigInteger left, long right)
		{
			return left.Equals(right);
		}

		/// <summary>Returns a value that indicates whether a <see cref="T:System.Numerics.BigInteger" /> value and a 64-bit signed integer are not equal.</summary>
		/// <returns>true if <paramref name="left" /> and <paramref name="right" /> are not equal; otherwise, false.</returns>
		/// <param name="left">The first value to compare.</param>
		/// <param name="right">The second value to compare.</param>
		// Token: 0x06000043 RID: 67 RVA: 0x000037D3 File Offset: 0x000019D3
		public static bool operator !=(BigInteger left, long right)
		{
			return !left.Equals(right);
		}

		/// <summary>Returns a value that indicates whether a 64-bit signed integer is less than a <see cref="T:System.Numerics.BigInteger" /> value.</summary>
		/// <returns>true if <paramref name="left" /> is less than <paramref name="right" />; otherwise, false.</returns>
		/// <param name="left">The first value to compare.</param>
		/// <param name="right">The second value to compare.</param>
		// Token: 0x06000044 RID: 68 RVA: 0x000037E0 File Offset: 0x000019E0
		public static bool operator <(long left, BigInteger right)
		{
			return right.CompareTo(left) > 0;
		}

		/// <summary>Returns a value that indicates whether a 64-bit signed integer is less than or equal to a <see cref="T:System.Numerics.BigInteger" /> value.</summary>
		/// <returns>true if <paramref name="left" /> is less than or equal to <paramref name="right" />; otherwise, false.</returns>
		/// <param name="left">The first value to compare.</param>
		/// <param name="right">The second value to compare.</param>
		// Token: 0x06000045 RID: 69 RVA: 0x000037ED File Offset: 0x000019ED
		public static bool operator <=(long left, BigInteger right)
		{
			return right.CompareTo(left) >= 0;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00003800 File Offset: 0x00001A00
		private static bool GetPartsForBitManipulation(ref BigInteger x, out uint[] xd, out int xl)
		{
			if (x._bits == null)
			{
				if (x._sign < 0)
				{
					xd = new uint[] { (uint)(-(uint)x._sign) };
				}
				else
				{
					xd = new uint[] { (uint)x._sign };
				}
			}
			else
			{
				xd = x._bits;
			}
			xl = ((x._bits == null) ? 1 : x._bits.Length);
			return x._sign < 0;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x0000386C File Offset: 0x00001A6C
		internal static int GetDiffLength(uint[] rgu1, uint[] rgu2, int cu)
		{
			int num = cu;
			while (--num >= 0)
			{
				if (rgu1[num] != rgu2[num])
				{
					return num + 1;
				}
			}
			return 0;
		}

		// Token: 0x04000001 RID: 1
		internal readonly int _sign;

		// Token: 0x04000002 RID: 2
		internal readonly uint[] _bits;

		// Token: 0x04000003 RID: 3
		private static readonly BigInteger s_bnMinInt = new BigInteger(-1, new uint[] { 2147483648U });

		// Token: 0x04000004 RID: 4
		private static readonly BigInteger s_bnOneInt = new BigInteger(1);

		// Token: 0x04000005 RID: 5
		private static readonly BigInteger s_bnZeroInt = new BigInteger(0);

		// Token: 0x04000006 RID: 6
		private static readonly BigInteger s_bnMinusOneInt = new BigInteger(-1);

		// Token: 0x04000007 RID: 7
		private static readonly byte[] s_success = Array.Empty<byte>();

		// Token: 0x02000004 RID: 4
		private enum GetBytesMode
		{
			// Token: 0x04000009 RID: 9
			AllocateArray,
			// Token: 0x0400000A RID: 10
			Count,
			// Token: 0x0400000B RID: 11
			Span
		}
	}
}
