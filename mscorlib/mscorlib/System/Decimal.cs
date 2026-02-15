using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>Represents a decimal number.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000205 RID: 517
	[Serializable]
	[StructLayout(LayoutKind.Explicit)]
	public readonly struct Decimal : IFormattable, IComparable, IConvertible, IComparable<decimal>, IEquatable<decimal>, IDeserializationCallback, ISpanFormattable
	{
		// Token: 0x17000205 RID: 517
		// (get) Token: 0x0600139C RID: 5020 RVA: 0x0004FCD0 File Offset: 0x0004DED0
		internal uint High
		{
			get
			{
				return (uint)this.hi;
			}
		}

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x0600139D RID: 5021 RVA: 0x0004FCD8 File Offset: 0x0004DED8
		internal uint Low
		{
			get
			{
				return (uint)this.lo;
			}
		}

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x0600139E RID: 5022 RVA: 0x0004FCE0 File Offset: 0x0004DEE0
		internal uint Mid
		{
			get
			{
				return (uint)this.mid;
			}
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x0600139F RID: 5023 RVA: 0x0004FCE8 File Offset: 0x0004DEE8
		internal bool IsNegative
		{
			get
			{
				return this.flags < 0;
			}
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x060013A0 RID: 5024 RVA: 0x0004FCF3 File Offset: 0x0004DEF3
		internal int Scale
		{
			get
			{
				return (int)((byte)(this.flags >> 16));
			}
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x060013A1 RID: 5025 RVA: 0x0004FCFF File Offset: 0x0004DEFF
		private ulong Low64
		{
			get
			{
				if (!BitConverter.IsLittleEndian)
				{
					return ((ulong)this.Mid << 32) | (ulong)this.Low;
				}
				return this.ulomidLE;
			}
		}

		// Token: 0x060013A2 RID: 5026 RVA: 0x0004FD21 File Offset: 0x0004DF21
		private static ref decimal.DecCalc AsMutable(ref decimal d)
		{
			return Unsafe.As<decimal, decimal.DecCalc>(ref d);
		}

		// Token: 0x060013A3 RID: 5027 RVA: 0x0004FD29 File Offset: 0x0004DF29
		internal static uint DecDivMod1E9(ref decimal value)
		{
			return decimal.DecCalc.DecDivMod1E9(decimal.AsMutable(ref value));
		}

		/// <summary>Initializes a new instance of <see cref="T:System.Decimal" /> to the value of the specified 32-bit signed integer.</summary>
		/// <param name="value">The value to represent as a <see cref="T:System.Decimal" />. </param>
		// Token: 0x060013A4 RID: 5028 RVA: 0x0004FD36 File Offset: 0x0004DF36
		public Decimal(int value)
		{
			if (value >= 0)
			{
				this.flags = 0;
			}
			else
			{
				this.flags = int.MinValue;
				value = -value;
			}
			this.lo = value;
			this.mid = 0;
			this.hi = 0;
		}

		/// <summary>Initializes a new instance of <see cref="T:System.Decimal" /> to the value of the specified 32-bit unsigned integer.</summary>
		/// <param name="value">The value to represent as a <see cref="T:System.Decimal" />. </param>
		// Token: 0x060013A5 RID: 5029 RVA: 0x0004FD69 File Offset: 0x0004DF69
		[CLSCompliant(false)]
		public Decimal(uint value)
		{
			this.flags = 0;
			this.lo = (int)value;
			this.mid = 0;
			this.hi = 0;
		}

		/// <summary>Initializes a new instance of <see cref="T:System.Decimal" /> to the value of the specified 64-bit signed integer.</summary>
		/// <param name="value">The value to represent as a <see cref="T:System.Decimal" />. </param>
		// Token: 0x060013A6 RID: 5030 RVA: 0x0004FD87 File Offset: 0x0004DF87
		public Decimal(long value)
		{
			if (value >= 0L)
			{
				this.flags = 0;
			}
			else
			{
				this.flags = int.MinValue;
				value = -value;
			}
			this.lo = (int)value;
			this.mid = (int)(value >> 32);
			this.hi = 0;
		}

		/// <summary>Initializes a new instance of <see cref="T:System.Decimal" /> to the value of the specified 64-bit unsigned integer.</summary>
		/// <param name="value">The value to represent as a <see cref="T:System.Decimal" />. </param>
		// Token: 0x060013A7 RID: 5031 RVA: 0x0004FDC0 File Offset: 0x0004DFC0
		[CLSCompliant(false)]
		public Decimal(ulong value)
		{
			this.flags = 0;
			this.lo = (int)value;
			this.mid = (int)(value >> 32);
			this.hi = 0;
		}

		/// <summary>Initializes a new instance of <see cref="T:System.Decimal" /> to the value of the specified single-precision floating-point number.</summary>
		/// <param name="value">The value to represent as a <see cref="T:System.Decimal" />. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.Decimal.MaxValue" /> or less than <see cref="F:System.Decimal.MinValue" />.-or- <paramref name="value" /> is <see cref="F:System.Single.NaN" />, <see cref="F:System.Single.PositiveInfinity" />, or <see cref="F:System.Single.NegativeInfinity" />. </exception>
		// Token: 0x060013A8 RID: 5032 RVA: 0x0004FDE3 File Offset: 0x0004DFE3
		public Decimal(float value)
		{
			decimal.DecCalc.VarDecFromR4(value, decimal.AsMutable(ref this));
		}

		/// <summary>Initializes a new instance of <see cref="T:System.Decimal" /> to the value of the specified double-precision floating-point number.</summary>
		/// <param name="value">The value to represent as a <see cref="T:System.Decimal" />. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.Decimal.MaxValue" /> or less than <see cref="F:System.Decimal.MinValue" />.-or- <paramref name="value" /> is <see cref="F:System.Double.NaN" />, <see cref="F:System.Double.PositiveInfinity" />, or <see cref="F:System.Double.NegativeInfinity" />. </exception>
		// Token: 0x060013A9 RID: 5033 RVA: 0x0004FDF1 File Offset: 0x0004DFF1
		public Decimal(double value)
		{
			decimal.DecCalc.VarDecFromR8(value, decimal.AsMutable(ref this));
		}

		// Token: 0x060013AA RID: 5034 RVA: 0x0004FDFF File Offset: 0x0004DFFF
		private static bool IsValid(int flags)
		{
			return (flags & 2130771967) == 0 && (flags & 16711680) <= 1835008;
		}

		/// <summary>Initializes a new instance of <see cref="T:System.Decimal" /> to a decimal value represented in binary and contained in a specified array.</summary>
		/// <param name="bits">An array of 32-bit signed integers containing a representation of a decimal value. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="bits" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">The length of the <paramref name="bits" /> is not 4.-or- The representation of the decimal value in <paramref name="bits" /> is not valid. </exception>
		// Token: 0x060013AB RID: 5035 RVA: 0x0004FE20 File Offset: 0x0004E020
		public Decimal(int[] bits)
		{
			if (bits == null)
			{
				throw new ArgumentNullException("bits");
			}
			if (bits.Length == 4)
			{
				int num = bits[3];
				if (decimal.IsValid(num))
				{
					this.lo = bits[0];
					this.mid = bits[1];
					this.hi = bits[2];
					this.flags = num;
					return;
				}
			}
			throw new ArgumentException("Decimal byte array constructor requires an array of length four containing valid decimal bytes.");
		}

		/// <summary>Initializes a new instance of <see cref="T:System.Decimal" /> from parameters specifying the instance's constituent parts.</summary>
		/// <param name="lo">The low 32 bits of a 96-bit integer. </param>
		/// <param name="mid">The middle 32 bits of a 96-bit integer. </param>
		/// <param name="hi">The high 32 bits of a 96-bit integer. </param>
		/// <param name="isNegative">true to indicate a negative number; false to indicate a positive number. </param>
		/// <param name="scale">A power of 10 ranging from 0 to 28. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="scale" /> is greater than 28. </exception>
		// Token: 0x060013AC RID: 5036 RVA: 0x0004FE7C File Offset: 0x0004E07C
		public Decimal(int lo, int mid, int hi, bool isNegative, byte scale)
		{
			if (scale > 28)
			{
				throw new ArgumentOutOfRangeException("scale", "Decimal's scale value must be between 0 and 28, inclusive.");
			}
			this.lo = lo;
			this.mid = mid;
			this.hi = hi;
			this.flags = (int)scale << 16;
			if (isNegative)
			{
				this.flags |= int.MinValue;
			}
		}

		/// <summary>Runs when the deserialization of an object has been completed.</summary>
		/// <param name="sender">The object that initiated the callback. The functionality for this parameter is not currently implemented.</param>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">The <see cref="T:System.Decimal" /> object contains invalid or corrupted data.</exception>
		// Token: 0x060013AD RID: 5037 RVA: 0x0004FED5 File Offset: 0x0004E0D5
		void IDeserializationCallback.OnDeserialization(object sender)
		{
			if (!decimal.IsValid(this.flags))
			{
				throw new SerializationException("Value was either too large or too small for a Decimal.");
			}
		}

		// Token: 0x060013AE RID: 5038 RVA: 0x0004FEEF File Offset: 0x0004E0EF
		private Decimal(in decimal d, int flags)
		{
			this = d;
			this.flags = flags;
		}

		// Token: 0x060013AF RID: 5039 RVA: 0x0004FF04 File Offset: 0x0004E104
		internal static decimal Abs(ref decimal d)
		{
			return new decimal(in d, d.flags & int.MaxValue);
		}

		/// <summary>Adds two specified <see cref="T:System.Decimal" /> values.</summary>
		/// <returns>The sum of <paramref name="d1" /> and <paramref name="d2" />.</returns>
		/// <param name="d1">The first value to add. </param>
		/// <param name="d2">The second value to add. </param>
		/// <exception cref="T:System.OverflowException">The sum of <paramref name="d1" /> and <paramref name="d2" /> is less than <see cref="F:System.Decimal.MinValue" /> or greater than <see cref="F:System.Decimal.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060013B0 RID: 5040 RVA: 0x0004FF18 File Offset: 0x0004E118
		public static decimal Add(decimal d1, decimal d2)
		{
			decimal.DecCalc.DecAddSub(decimal.AsMutable(ref d1), decimal.AsMutable(ref d2), false);
			return d1;
		}

		/// <summary>Compares two specified <see cref="T:System.Decimal" /> values.</summary>
		/// <returns>A signed number indicating the relative values of <paramref name="d1" /> and <paramref name="d2" />.Return value Meaning Less than zero <paramref name="d1" /> is less than <paramref name="d2" />. Zero <paramref name="d1" /> and <paramref name="d2" /> are equal. Greater than zero <paramref name="d1" /> is greater than <paramref name="d2" />. </returns>
		/// <param name="d1">The first value to compare. </param>
		/// <param name="d2">The second value to compare. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060013B1 RID: 5041 RVA: 0x0004FF2F File Offset: 0x0004E12F
		public static int Compare(decimal d1, decimal d2)
		{
			return decimal.DecCalc.VarDecCmp(in d1, in d2);
		}

		/// <summary>Compares this instance to a specified object and returns a comparison of their relative values.</summary>
		/// <returns>A signed number indicating the relative values of this instance and <paramref name="value" />.Return value Meaning Less than zero This instance is less than <paramref name="value" />. Zero This instance is equal to <paramref name="value" />. Greater than zero This instance is greater than <paramref name="value" />.-or- <paramref name="value" /> is null. </returns>
		/// <param name="value">The object to compare with this instance, or null. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> is not a <see cref="T:System.Decimal" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060013B2 RID: 5042 RVA: 0x0004FF3C File Offset: 0x0004E13C
		public int CompareTo(object value)
		{
			if (value == null)
			{
				return 1;
			}
			if (!(value is decimal))
			{
				throw new ArgumentException("Object must be of type Decimal.");
			}
			decimal num = (decimal)value;
			return decimal.DecCalc.VarDecCmp(in this, in num);
		}

		/// <summary>Compares this instance to a specified <see cref="T:System.Decimal" /> object and returns a comparison of their relative values.</summary>
		/// <returns>A signed number indicating the relative values of this instance and <paramref name="value" />.Return value Meaning Less than zero This instance is less than <paramref name="value" />. Zero This instance is equal to <paramref name="value" />. Greater than zero This instance is greater than <paramref name="value" />. </returns>
		/// <param name="value">The object to compare with this instance.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060013B3 RID: 5043 RVA: 0x0004FF70 File Offset: 0x0004E170
		public int CompareTo(decimal value)
		{
			return decimal.DecCalc.VarDecCmp(in this, in value);
		}

		/// <summary>Divides two specified <see cref="T:System.Decimal" /> values.</summary>
		/// <returns>The result of dividing <paramref name="d1" /> by <paramref name="d2" />.</returns>
		/// <param name="d1">The dividend. </param>
		/// <param name="d2">The divisor. </param>
		/// <exception cref="T:System.DivideByZeroException">
		///   <paramref name="d2" /> is zero. </exception>
		/// <exception cref="T:System.OverflowException">The return value (that is, the quotient) is less than <see cref="F:System.Decimal.MinValue" /> or greater than <see cref="F:System.Decimal.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060013B4 RID: 5044 RVA: 0x0004FF7A File Offset: 0x0004E17A
		public static decimal Divide(decimal d1, decimal d2)
		{
			decimal.DecCalc.VarDecDiv(decimal.AsMutable(ref d1), decimal.AsMutable(ref d2));
			return d1;
		}

		/// <summary>Returns a value indicating whether this instance and a specified <see cref="T:System.Object" /> represent the same type and value.</summary>
		/// <returns>true if <paramref name="value" /> is a <see cref="T:System.Decimal" /> and equal to this instance; otherwise, false.</returns>
		/// <param name="value">The object to compare with this instance. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060013B5 RID: 5045 RVA: 0x0004FF90 File Offset: 0x0004E190
		public override bool Equals(object value)
		{
			if (value is decimal)
			{
				decimal num = (decimal)value;
				return decimal.DecCalc.VarDecCmp(in this, in num) == 0;
			}
			return false;
		}

		/// <summary>Returns a value indicating whether this instance and a specified <see cref="T:System.Decimal" /> object represent the same value.</summary>
		/// <returns>true if <paramref name="value" /> is equal to this instance; otherwise, false.</returns>
		/// <param name="value">An object to compare to this instance.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060013B6 RID: 5046 RVA: 0x0004FFB9 File Offset: 0x0004E1B9
		public bool Equals(decimal value)
		{
			return decimal.DecCalc.VarDecCmp(in this, in value) == 0;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060013B7 RID: 5047 RVA: 0x0004FFC6 File Offset: 0x0004E1C6
		public override int GetHashCode()
		{
			return decimal.DecCalc.GetHashCode(in this);
		}

		/// <summary>Converts the numeric value of this instance to its equivalent string representation.</summary>
		/// <returns>A string that represents the value of this instance.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060013B8 RID: 5048 RVA: 0x0004FFCE File Offset: 0x0004E1CE
		public override string ToString()
		{
			return Number.FormatDecimal(this, null, NumberFormatInfo.CurrentInfo);
		}

		/// <summary>Converts the numeric value of this instance to its equivalent string representation using the specified culture-specific format information.</summary>
		/// <returns>The string representation of the value of this instance as specified by <paramref name="provider" />.</returns>
		/// <param name="provider">An object that supplies culture-specific formatting information. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060013B9 RID: 5049 RVA: 0x0004FFE6 File Offset: 0x0004E1E6
		public string ToString(IFormatProvider provider)
		{
			return Number.FormatDecimal(this, null, NumberFormatInfo.GetInstance(provider));
		}

		/// <summary>Converts the numeric value of this instance to its equivalent string representation using the specified format and culture-specific format information.</summary>
		/// <returns>The string representation of the value of this instance as specified by <paramref name="format" /> and <paramref name="provider" />.</returns>
		/// <param name="format">A numeric format string (see Remarks).</param>
		/// <param name="provider">An object that supplies culture-specific formatting information. </param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="format" /> is invalid. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060013BA RID: 5050 RVA: 0x0004FFFF File Offset: 0x0004E1FF
		public string ToString(string format, IFormatProvider provider)
		{
			return Number.FormatDecimal(this, format, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x060013BB RID: 5051 RVA: 0x00050018 File Offset: 0x0004E218
		public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format = default(ReadOnlySpan<char>), IFormatProvider provider = null)
		{
			return Number.TryFormatDecimal(this, format, NumberFormatInfo.GetInstance(provider), destination, out charsWritten);
		}

		/// <summary>Converts the string representation of a number to its <see cref="T:System.Decimal" /> equivalent using the specified culture-specific format information.</summary>
		/// <returns>The <see cref="T:System.Decimal" /> number equivalent to the number contained in <paramref name="s" /> as specified by <paramref name="provider" />.</returns>
		/// <param name="s">The string representation of the number to convert. </param>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> that supplies culture-specific parsing information about <paramref name="s" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> is not of the correct format </exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="s" /> represents a number less than <see cref="F:System.Decimal.MinValue" /> or greater than <see cref="F:System.Decimal.MaxValue" /></exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060013BC RID: 5052 RVA: 0x0005002F File Offset: 0x0004E22F
		public static decimal Parse(string s, IFormatProvider provider)
		{
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			return Number.ParseDecimal(s, NumberStyles.Number, NumberFormatInfo.GetInstance(provider));
		}

		/// <summary>Converts the string representation of a number to its <see cref="T:System.Decimal" /> equivalent using the specified style and culture-specific format.</summary>
		/// <returns>The <see cref="T:System.Decimal" /> number equivalent to the number contained in <paramref name="s" /> as specified by <paramref name="style" /> and <paramref name="provider" />.</returns>
		/// <param name="s">The string representation of the number to convert. </param>
		/// <param name="style">A bitwise combination of <see cref="T:System.Globalization.NumberStyles" /> values that indicates the style elements that can be present in <paramref name="s" />. A typical value to specify is <see cref="F:System.Globalization.NumberStyles.Number" />.</param>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> object that supplies culture-specific information about the format of <paramref name="s" />. </param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> is not in the correct format. </exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="s" /> represents a number less than <see cref="F:System.Decimal.MinValue" /> or greater than <see cref="F:System.Decimal.MaxValue" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="style" /> is not a <see cref="T:System.Globalization.NumberStyles" /> value. -or-<paramref name="style" /> is the <see cref="F:System.Globalization.NumberStyles.AllowHexSpecifier" /> value.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060013BD RID: 5053 RVA: 0x0005004E File Offset: 0x0004E24E
		public static decimal Parse(string s, NumberStyles style, IFormatProvider provider)
		{
			NumberFormatInfo.ValidateParseStyleFloatingPoint(style);
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			return Number.ParseDecimal(s, style, NumberFormatInfo.GetInstance(provider));
		}

		/// <summary>Converts the string representation of a number to its <see cref="T:System.Decimal" /> equivalent using the specified style and culture-specific format. A return value indicates whether the conversion succeeded or failed.</summary>
		/// <returns>true if <paramref name="s" /> was converted successfully; otherwise, false.</returns>
		/// <param name="s">The string representation of the number to convert.</param>
		/// <param name="style">A bitwise combination of enumeration values that indicates the permitted format of <paramref name="s" />. A typical value to specify is <see cref="F:System.Globalization.NumberStyles.Number" />.</param>
		/// <param name="provider">An object that supplies culture-specific parsing information about <paramref name="s" />. </param>
		/// <param name="result">When this method returns, contains the <see cref="T:System.Decimal" /> number that is equivalent to the numeric value contained in <paramref name="s" />, if the conversion succeeded, or is zero if the conversion failed. The conversion fails if the <paramref name="s" /> parameter is null, is not in a format compliant with <paramref name="style" />, or represents a number less than <see cref="F:System.Decimal.MinValue" /> or greater than <see cref="F:System.Decimal.MaxValue" />. This parameter is passed uninitialized. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="style" /> is not a <see cref="T:System.Globalization.NumberStyles" /> value. -or-<paramref name="style" /> is the <see cref="F:System.Globalization.NumberStyles.AllowHexSpecifier" /> value.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060013BE RID: 5054 RVA: 0x00050072 File Offset: 0x0004E272
		public static bool TryParse(string s, NumberStyles style, IFormatProvider provider, out decimal result)
		{
			NumberFormatInfo.ValidateParseStyleFloatingPoint(style);
			if (s == null)
			{
				result = 0m;
				return false;
			}
			return Number.TryParseDecimal(s, style, NumberFormatInfo.GetInstance(provider), out result);
		}

		/// <summary>Converts the value of a specified instance of <see cref="T:System.Decimal" /> to its equivalent binary representation.</summary>
		/// <returns>A 32-bit signed integer array with four elements that contain the binary representation of <paramref name="d" />.</returns>
		/// <param name="d">The value to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060013BF RID: 5055 RVA: 0x00050099 File Offset: 0x0004E299
		public static int[] GetBits(decimal d)
		{
			return new int[] { d.lo, d.mid, d.hi, d.flags };
		}

		// Token: 0x060013C0 RID: 5056 RVA: 0x000500C8 File Offset: 0x0004E2C8
		internal static void GetBytes(in decimal d, byte[] buffer)
		{
			buffer[0] = (byte)d.lo;
			buffer[1] = (byte)(d.lo >> 8);
			buffer[2] = (byte)(d.lo >> 16);
			buffer[3] = (byte)(d.lo >> 24);
			buffer[4] = (byte)d.mid;
			buffer[5] = (byte)(d.mid >> 8);
			buffer[6] = (byte)(d.mid >> 16);
			buffer[7] = (byte)(d.mid >> 24);
			buffer[8] = (byte)d.hi;
			buffer[9] = (byte)(d.hi >> 8);
			buffer[10] = (byte)(d.hi >> 16);
			buffer[11] = (byte)(d.hi >> 24);
			buffer[12] = (byte)d.flags;
			buffer[13] = (byte)(d.flags >> 8);
			buffer[14] = (byte)(d.flags >> 16);
			buffer[15] = (byte)(d.flags >> 24);
		}

		// Token: 0x060013C1 RID: 5057 RVA: 0x0005019C File Offset: 0x0004E39C
		internal static readonly ref decimal Max(ref decimal d1, ref decimal d2)
		{
			if (decimal.DecCalc.VarDecCmp(in d1, in d2) < 0)
			{
				return ref d2;
			}
			return ref d1;
		}

		// Token: 0x060013C2 RID: 5058 RVA: 0x000501AB File Offset: 0x0004E3AB
		internal static readonly ref decimal Min(ref decimal d1, ref decimal d2)
		{
			if (decimal.DecCalc.VarDecCmp(in d1, in d2) >= 0)
			{
				return ref d2;
			}
			return ref d1;
		}

		/// <summary>Multiplies two specified <see cref="T:System.Decimal" /> values.</summary>
		/// <returns>The result of multiplying <paramref name="d1" /> and <paramref name="d2" />.</returns>
		/// <param name="d1">The multiplicand. </param>
		/// <param name="d2">The multiplier. </param>
		/// <exception cref="T:System.OverflowException">The return value is less than <see cref="F:System.Decimal.MinValue" /> or greater than <see cref="F:System.Decimal.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060013C3 RID: 5059 RVA: 0x000501BA File Offset: 0x0004E3BA
		public static decimal Multiply(decimal d1, decimal d2)
		{
			decimal.DecCalc.VarDecMul(decimal.AsMutable(ref d1), decimal.AsMutable(ref d2));
			return d1;
		}

		/// <summary>Returns the result of multiplying the specified <see cref="T:System.Decimal" /> value by negative one.</summary>
		/// <returns>A decimal number with the value of <paramref name="d" />, but the opposite sign.-or- Zero, if <paramref name="d" /> is zero.</returns>
		/// <param name="d">The value to negate. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060013C4 RID: 5060 RVA: 0x000501D0 File Offset: 0x0004E3D0
		public static decimal Negate(decimal d)
		{
			return new decimal(in d, d.flags ^ int.MinValue);
		}

		/// <summary>Rounds a <see cref="T:System.Decimal" /> value to a specified number of decimal places.</summary>
		/// <returns>The decimal number equivalent to <paramref name="d" /> rounded to <paramref name="decimals" /> number of decimal places.</returns>
		/// <param name="d">A decimal number to round. </param>
		/// <param name="decimals">A value from 0 to 28 that specifies the number of decimal places to round to. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="decimals" /> is not a value from 0 to 28. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060013C5 RID: 5061 RVA: 0x000501E5 File Offset: 0x0004E3E5
		public static decimal Round(decimal d, int decimals)
		{
			return decimal.Round(ref d, decimals, MidpointRounding.ToEven);
		}

		// Token: 0x060013C6 RID: 5062 RVA: 0x000501F0 File Offset: 0x0004E3F0
		private static decimal Round(ref decimal d, int decimals, MidpointRounding mode)
		{
			if (decimals > 28)
			{
				throw new ArgumentOutOfRangeException("decimals", "Decimal can only round to between 0 and 28 digits of precision.");
			}
			if (mode > MidpointRounding.AwayFromZero)
			{
				throw new ArgumentException(SR.Format("The value '{0}' is not valid for this usage of the type {1}.", mode, "MidpointRounding"), "mode");
			}
			int num = d.Scale - decimals;
			if (num > 0)
			{
				decimal.DecCalc.InternalRound(decimal.AsMutable(ref d), (uint)num, (decimal.DecCalc.RoundingMode)mode);
			}
			return d;
		}

		// Token: 0x060013C7 RID: 5063 RVA: 0x00050256 File Offset: 0x0004E456
		internal static int Sign(ref decimal d)
		{
			if ((d.lo | d.mid | d.hi) != 0)
			{
				return (d.flags >> 31) | 1;
			}
			return 0;
		}

		/// <summary>Converts the value of the specified <see cref="T:System.Decimal" /> to the equivalent 8-bit unsigned integer.</summary>
		/// <returns>An 8-bit unsigned integer equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The decimal number to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than <see cref="F:System.Byte.MinValue" /> or greater than <see cref="F:System.Byte.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060013C8 RID: 5064 RVA: 0x0005027C File Offset: 0x0004E47C
		public static byte ToByte(decimal value)
		{
			uint num;
			try
			{
				num = decimal.ToUInt32(value);
			}
			catch (OverflowException ex)
			{
				throw new OverflowException("Value was either too large or too small for an unsigned byte.", ex);
			}
			if (num != (uint)((byte)num))
			{
				throw new OverflowException("Value was either too large or too small for an unsigned byte.");
			}
			return (byte)num;
		}

		/// <summary>Converts the value of the specified <see cref="T:System.Decimal" /> to the equivalent 8-bit signed integer.</summary>
		/// <returns>An 8-bit signed integer equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The decimal number to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than <see cref="F:System.SByte.MinValue" /> or greater than <see cref="F:System.SByte.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060013C9 RID: 5065 RVA: 0x000502C4 File Offset: 0x0004E4C4
		[CLSCompliant(false)]
		public static sbyte ToSByte(decimal value)
		{
			int num;
			try
			{
				num = decimal.ToInt32(value);
			}
			catch (OverflowException ex)
			{
				throw new OverflowException("Value was either too large or too small for a signed byte.", ex);
			}
			if (num != (int)((sbyte)num))
			{
				throw new OverflowException("Value was either too large or too small for a signed byte.");
			}
			return (sbyte)num;
		}

		/// <summary>Converts the value of the specified <see cref="T:System.Decimal" /> to the equivalent 16-bit signed integer.</summary>
		/// <returns>A 16-bit signed integer equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The decimal number to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than <see cref="F:System.Int16.MinValue" /> or greater than <see cref="F:System.Int16.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060013CA RID: 5066 RVA: 0x0005030C File Offset: 0x0004E50C
		public static short ToInt16(decimal value)
		{
			int num;
			try
			{
				num = decimal.ToInt32(value);
			}
			catch (OverflowException ex)
			{
				throw new OverflowException("Value was either too large or too small for an Int16.", ex);
			}
			if (num != (int)((short)num))
			{
				throw new OverflowException("Value was either too large or too small for an Int16.");
			}
			return (short)num;
		}

		/// <summary>Converts the value of the specified <see cref="T:System.Decimal" /> to the equivalent double-precision floating-point number.</summary>
		/// <returns>A double-precision floating-point number equivalent to <paramref name="d" />.</returns>
		/// <param name="d">The decimal number to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060013CB RID: 5067 RVA: 0x00050354 File Offset: 0x0004E554
		public static double ToDouble(decimal d)
		{
			return decimal.DecCalc.VarR8FromDec(in d);
		}

		/// <summary>Converts the value of the specified <see cref="T:System.Decimal" /> to the equivalent 32-bit signed integer.</summary>
		/// <returns>A 32-bit signed integer equivalent to the value of <paramref name="d" />.</returns>
		/// <param name="d">The decimal number to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="d" /> is less than <see cref="F:System.Int32.MinValue" /> or greater than <see cref="F:System.Int32.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060013CC RID: 5068 RVA: 0x00050360 File Offset: 0x0004E560
		public static int ToInt32(decimal d)
		{
			decimal.Truncate(ref d);
			if ((d.hi | d.mid) == 0)
			{
				int num = d.lo;
				if (!d.IsNegative)
				{
					if (num >= 0)
					{
						return num;
					}
				}
				else
				{
					num = -num;
					if (num <= 0)
					{
						return num;
					}
				}
			}
			throw new OverflowException("Value was either too large or too small for an Int32.");
		}

		/// <summary>Converts the value of the specified <see cref="T:System.Decimal" /> to the equivalent 64-bit signed integer.</summary>
		/// <returns>A 64-bit signed integer equivalent to the value of <paramref name="d" />.</returns>
		/// <param name="d">The decimal number to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="d" /> is less than <see cref="F:System.Int64.MinValue" /> or greater than <see cref="F:System.Int64.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060013CD RID: 5069 RVA: 0x000503AC File Offset: 0x0004E5AC
		public static long ToInt64(decimal d)
		{
			decimal.Truncate(ref d);
			if (d.hi == 0)
			{
				long num = (long)d.Low64;
				if (!d.IsNegative)
				{
					if (num >= 0L)
					{
						return num;
					}
				}
				else
				{
					num = -num;
					if (num <= 0L)
					{
						return num;
					}
				}
			}
			throw new OverflowException("Value was either too large or too small for an Int64.");
		}

		/// <summary>Converts the value of the specified <see cref="T:System.Decimal" /> to the equivalent 16-bit unsigned integer.</summary>
		/// <returns>A 16-bit unsigned integer equivalent to the value of <paramref name="value" />.</returns>
		/// <param name="value">The decimal number to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.UInt16.MaxValue" /> or less than <see cref="F:System.UInt16.MinValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060013CE RID: 5070 RVA: 0x000503F4 File Offset: 0x0004E5F4
		[CLSCompliant(false)]
		public static ushort ToUInt16(decimal value)
		{
			uint num;
			try
			{
				num = decimal.ToUInt32(value);
			}
			catch (OverflowException ex)
			{
				throw new OverflowException("Value was either too large or too small for a UInt16.", ex);
			}
			if (num != (uint)((ushort)num))
			{
				throw new OverflowException("Value was either too large or too small for a UInt16.");
			}
			return (ushort)num;
		}

		/// <summary>Converts the value of the specified <see cref="T:System.Decimal" /> to the equivalent 32-bit unsigned integer.</summary>
		/// <returns>A 32-bit unsigned integer equivalent to the value of <paramref name="d" />.</returns>
		/// <param name="d">The decimal number to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="d" /> is negative or greater than <see cref="F:System.UInt32.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060013CF RID: 5071 RVA: 0x0005043C File Offset: 0x0004E63C
		[CLSCompliant(false)]
		public static uint ToUInt32(decimal d)
		{
			decimal.Truncate(ref d);
			if ((d.hi | d.mid) == 0)
			{
				uint low = d.Low;
				if (!d.IsNegative || low == 0U)
				{
					return low;
				}
			}
			throw new OverflowException("Value was either too large or too small for a UInt32.");
		}

		/// <summary>Converts the value of the specified <see cref="T:System.Decimal" /> to the equivalent 64-bit unsigned integer.</summary>
		/// <returns>A 64-bit unsigned integer equivalent to the value of <paramref name="d" />.</returns>
		/// <param name="d">The decimal number to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="d" /> is negative or greater than <see cref="F:System.UInt64.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060013D0 RID: 5072 RVA: 0x00050480 File Offset: 0x0004E680
		[CLSCompliant(false)]
		public static ulong ToUInt64(decimal d)
		{
			decimal.Truncate(ref d);
			if (d.hi == 0)
			{
				ulong low = d.Low64;
				if (!d.IsNegative || low == 0UL)
				{
					return low;
				}
			}
			throw new OverflowException("Value was either too large or too small for a UInt64.");
		}

		/// <summary>Converts the value of the specified <see cref="T:System.Decimal" /> to the equivalent single-precision floating-point number.</summary>
		/// <returns>A single-precision floating-point number equivalent to the value of <paramref name="d" />.</returns>
		/// <param name="d">The decimal number to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060013D1 RID: 5073 RVA: 0x000504BC File Offset: 0x0004E6BC
		public static float ToSingle(decimal d)
		{
			return decimal.DecCalc.VarR4FromDec(in d);
		}

		/// <summary>Returns the integral digits of the specified <see cref="T:System.Decimal" />; any fractional digits are discarded.</summary>
		/// <returns>The result of <paramref name="d" /> rounded toward zero, to the nearest whole number.</returns>
		/// <param name="d">The decimal number to truncate. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060013D2 RID: 5074 RVA: 0x000504C5 File Offset: 0x0004E6C5
		public static decimal Truncate(decimal d)
		{
			decimal.Truncate(ref d);
			return d;
		}

		// Token: 0x060013D3 RID: 5075 RVA: 0x000504D0 File Offset: 0x0004E6D0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void Truncate(ref decimal d)
		{
			int num = d.flags;
			if ((num & 16711680) != 0)
			{
				decimal.DecCalc.InternalRound(decimal.AsMutable(ref d), (uint)((byte)(num >> 16)), decimal.DecCalc.RoundingMode.Truncate);
			}
		}

		/// <summary>Defines an explicit conversion of an 8-bit unsigned integer to a <see cref="T:System.Decimal" />.</summary>
		/// <returns>The converted 8-bit unsigned integer.</returns>
		/// <param name="value">The 8-bit unsigned integer to convert. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x060013D4 RID: 5076 RVA: 0x000504FE File Offset: 0x0004E6FE
		public static implicit operator decimal(byte value)
		{
			return new decimal((uint)value);
		}

		/// <summary>Defines an explicit conversion of an 8-bit signed integer to a <see cref="T:System.Decimal" />.</summary>
		/// <returns>The converted 8-bit signed integer.</returns>
		/// <param name="value">The 8-bit signed integer to convert. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x060013D5 RID: 5077 RVA: 0x00050506 File Offset: 0x0004E706
		[CLSCompliant(false)]
		public static implicit operator decimal(sbyte value)
		{
			return new decimal((int)value);
		}

		/// <summary>Defines an explicit conversion of a 16-bit signed integer to a <see cref="T:System.Decimal" />.</summary>
		/// <returns>The converted 16-bit signed integer.</returns>
		/// <param name="value">The16-bit signed integer to convert. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x060013D6 RID: 5078 RVA: 0x00050506 File Offset: 0x0004E706
		public static implicit operator decimal(short value)
		{
			return new decimal((int)value);
		}

		/// <summary>Defines an explicit conversion of a 16-bit unsigned integer to a <see cref="T:System.Decimal" />.</summary>
		/// <returns>The converted 16-bit unsigned integer.</returns>
		/// <param name="value">The 16-bit unsigned integer to convert. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x060013D7 RID: 5079 RVA: 0x000504FE File Offset: 0x0004E6FE
		[CLSCompliant(false)]
		public static implicit operator decimal(ushort value)
		{
			return new decimal((uint)value);
		}

		/// <summary>Defines an explicit conversion of a Unicode character to a <see cref="T:System.Decimal" />.</summary>
		/// <returns>The converted Unicode character.</returns>
		/// <param name="value">The Unicode character to convert. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x060013D8 RID: 5080 RVA: 0x000504FE File Offset: 0x0004E6FE
		public static implicit operator decimal(char value)
		{
			return new decimal((uint)value);
		}

		/// <summary>Defines an explicit conversion of a 32-bit signed integer to a <see cref="T:System.Decimal" />.</summary>
		/// <returns>The converted 32-bit signed integer.</returns>
		/// <param name="value">The 32-bit signed integer to convert. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x060013D9 RID: 5081 RVA: 0x00050506 File Offset: 0x0004E706
		public static implicit operator decimal(int value)
		{
			return new decimal(value);
		}

		/// <summary>Defines an explicit conversion of a 32-bit unsigned integer to a <see cref="T:System.Decimal" />.</summary>
		/// <returns>The converted 32-bit unsigned integer.</returns>
		/// <param name="value">The 32-bit unsigned integer to convert. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x060013DA RID: 5082 RVA: 0x000504FE File Offset: 0x0004E6FE
		[CLSCompliant(false)]
		public static implicit operator decimal(uint value)
		{
			return new decimal(value);
		}

		/// <summary>Defines an explicit conversion of a 64-bit signed integer to a <see cref="T:System.Decimal" />.</summary>
		/// <returns>The converted 64-bit signed integer.</returns>
		/// <param name="value">The 64-bit signed integer to convert. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x060013DB RID: 5083 RVA: 0x0005050E File Offset: 0x0004E70E
		public static implicit operator decimal(long value)
		{
			return new decimal(value);
		}

		/// <summary>Defines an explicit conversion of a 64-bit unsigned integer to a <see cref="T:System.Decimal" />.</summary>
		/// <returns>The converted 64-bit unsigned integer.</returns>
		/// <param name="value">The 64-bit unsigned integer to convert. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x060013DC RID: 5084 RVA: 0x00050516 File Offset: 0x0004E716
		[CLSCompliant(false)]
		public static implicit operator decimal(ulong value)
		{
			return new decimal(value);
		}

		/// <summary>Defines an explicit conversion of a single-precision floating-point number to a <see cref="T:System.Decimal" />.</summary>
		/// <returns>The converted single-precision floating point number.</returns>
		/// <param name="value">The single-precision floating-point number to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than <see cref="F:System.Decimal.MinValue" /> or greater than <see cref="F:System.Decimal.MaxValue" />.-or- <paramref name="value" /> is <see cref="F:System.Single.NaN" />, <see cref="F:System.Single.PositiveInfinity" />, or <see cref="F:System.Single.NegativeInfinity" />. </exception>
		/// <filterpriority>3</filterpriority>
		// Token: 0x060013DD RID: 5085 RVA: 0x0005051E File Offset: 0x0004E71E
		public static explicit operator decimal(float value)
		{
			return new decimal(value);
		}

		/// <summary>Defines an explicit conversion of a double-precision floating-point number to a <see cref="T:System.Decimal" />.</summary>
		/// <returns>The converted double-precision floating point number.</returns>
		/// <param name="value">The double-precision floating-point number to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than <see cref="F:System.Decimal.MinValue" /> or greater than <see cref="F:System.Decimal.MaxValue" />.-or- <paramref name="value" /> is <see cref="F:System.Double.NaN" />, <see cref="F:System.Double.PositiveInfinity" />, or <see cref="F:System.Double.NegativeInfinity" />. </exception>
		/// <filterpriority>3</filterpriority>
		// Token: 0x060013DE RID: 5086 RVA: 0x00050526 File Offset: 0x0004E726
		public static explicit operator decimal(double value)
		{
			return new decimal(value);
		}

		/// <summary>Defines an explicit conversion of a <see cref="T:System.Decimal" /> to a 32-bit signed integer.</summary>
		/// <returns>A 32-bit signed integer that represents the converted <see cref="T:System.Decimal" />.</returns>
		/// <param name="value">The value to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than <see cref="F:System.Int32.MinValue" /> or greater than <see cref="F:System.Int32.MaxValue" />. </exception>
		/// <filterpriority>3</filterpriority>
		// Token: 0x060013DF RID: 5087 RVA: 0x0005052E File Offset: 0x0004E72E
		public static explicit operator int(decimal value)
		{
			return decimal.ToInt32(value);
		}

		/// <summary>Defines an explicit conversion of a <see cref="T:System.Decimal" /> to a 64-bit signed integer.</summary>
		/// <returns>A 64-bit signed integer that represents the converted <see cref="T:System.Decimal" />.</returns>
		/// <param name="value">The value to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than <see cref="F:System.Int64.MinValue" /> or greater than <see cref="F:System.Int64.MaxValue" />. </exception>
		/// <filterpriority>3</filterpriority>
		// Token: 0x060013E0 RID: 5088 RVA: 0x00050536 File Offset: 0x0004E736
		public static explicit operator long(decimal value)
		{
			return decimal.ToInt64(value);
		}

		/// <summary>Defines an explicit conversion of a <see cref="T:System.Decimal" /> to a 64-bit unsigned integer.</summary>
		/// <returns>A 64-bit unsigned integer that represents the converted <see cref="T:System.Decimal" />.</returns>
		/// <param name="value">The value to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is negative or greater than <see cref="F:System.UInt64.MaxValue" />. </exception>
		/// <filterpriority>3</filterpriority>
		// Token: 0x060013E1 RID: 5089 RVA: 0x0005053E File Offset: 0x0004E73E
		[CLSCompliant(false)]
		public static explicit operator ulong(decimal value)
		{
			return decimal.ToUInt64(value);
		}

		/// <summary>Defines an explicit conversion of a <see cref="T:System.Decimal" /> to a single-precision floating-point number.</summary>
		/// <returns>A single-precision floating-point number that represents the converted <see cref="T:System.Decimal" />.</returns>
		/// <param name="value">The value to convert. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x060013E2 RID: 5090 RVA: 0x00050546 File Offset: 0x0004E746
		public static explicit operator float(decimal value)
		{
			return decimal.ToSingle(value);
		}

		/// <summary>Defines an explicit conversion of a <see cref="T:System.Decimal" /> to a double-precision floating-point number.</summary>
		/// <returns>A double-precision floating-point number that represents the converted <see cref="T:System.Decimal" />.</returns>
		/// <param name="value">The value to convert. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x060013E3 RID: 5091 RVA: 0x0005054E File Offset: 0x0004E74E
		public static explicit operator double(decimal value)
		{
			return decimal.ToDouble(value);
		}

		/// <summary>Negates the value of the specified <see cref="T:System.Decimal" /> operand.</summary>
		/// <returns>The result of <paramref name="d" /> multiplied by negative one (-1).</returns>
		/// <param name="d">The value to negate. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x060013E4 RID: 5092 RVA: 0x000501D0 File Offset: 0x0004E3D0
		public static decimal operator -(decimal d)
		{
			return new decimal(in d, d.flags ^ int.MinValue);
		}

		/// <summary>Increments the <see cref="T:System.Decimal" /> operand by 1.</summary>
		/// <returns>The value of <paramref name="d" /> incremented by 1.</returns>
		/// <param name="d">The value to increment. </param>
		/// <exception cref="T:System.OverflowException">The return value is less than <see cref="F:System.Decimal.MinValue" /> or greater than <see cref="F:System.Decimal.MaxValue" />. </exception>
		/// <filterpriority>3</filterpriority>
		// Token: 0x060013E5 RID: 5093 RVA: 0x00050556 File Offset: 0x0004E756
		public static decimal operator ++(decimal d)
		{
			return decimal.Add(d, 1m);
		}

		/// <summary>Adds two specified <see cref="T:System.Decimal" /> values.</summary>
		/// <returns>The result of adding <paramref name="d1" /> and <paramref name="d2" />.</returns>
		/// <param name="d1">The first value to add. </param>
		/// <param name="d2">The second value to add. </param>
		/// <exception cref="T:System.OverflowException">The return value is less than <see cref="F:System.Decimal.MinValue" /> or greater than <see cref="F:System.Decimal.MaxValue" />. </exception>
		/// <filterpriority>3</filterpriority>
		// Token: 0x060013E6 RID: 5094 RVA: 0x0004FF18 File Offset: 0x0004E118
		public static decimal operator +(decimal d1, decimal d2)
		{
			decimal.DecCalc.DecAddSub(decimal.AsMutable(ref d1), decimal.AsMutable(ref d2), false);
			return d1;
		}

		/// <summary>Subtracts two specified <see cref="T:System.Decimal" /> values.</summary>
		/// <returns>The result of subtracting <paramref name="d2" /> from <paramref name="d1" />.</returns>
		/// <param name="d1">The minuend. </param>
		/// <param name="d2">The subtrahend. </param>
		/// <exception cref="T:System.OverflowException">The return value is less than <see cref="F:System.Decimal.MinValue" /> or greater than <see cref="F:System.Decimal.MaxValue" />. </exception>
		/// <filterpriority>3</filterpriority>
		// Token: 0x060013E7 RID: 5095 RVA: 0x00050563 File Offset: 0x0004E763
		public static decimal operator -(decimal d1, decimal d2)
		{
			decimal.DecCalc.DecAddSub(decimal.AsMutable(ref d1), decimal.AsMutable(ref d2), true);
			return d1;
		}

		/// <summary>Multiplies two specified <see cref="T:System.Decimal" /> values.</summary>
		/// <returns>The result of multiplying <paramref name="d1" /> by <paramref name="d2" />.</returns>
		/// <param name="d1">The first value to multiply. </param>
		/// <param name="d2">The second value to multiply. </param>
		/// <exception cref="T:System.OverflowException">The return value is less than <see cref="F:System.Decimal.MinValue" /> or greater than <see cref="F:System.Decimal.MaxValue" />. </exception>
		/// <filterpriority>3</filterpriority>
		// Token: 0x060013E8 RID: 5096 RVA: 0x000501BA File Offset: 0x0004E3BA
		public static decimal operator *(decimal d1, decimal d2)
		{
			decimal.DecCalc.VarDecMul(decimal.AsMutable(ref d1), decimal.AsMutable(ref d2));
			return d1;
		}

		/// <summary>Divides two specified <see cref="T:System.Decimal" /> values.</summary>
		/// <returns>The result of dividing <paramref name="d1" /> by <paramref name="d2" />.</returns>
		/// <param name="d1">The dividend. </param>
		/// <param name="d2">The divisor. </param>
		/// <exception cref="T:System.DivideByZeroException">
		///   <paramref name="d2" /> is zero. </exception>
		/// <exception cref="T:System.OverflowException">The return value is less than <see cref="F:System.Decimal.MinValue" /> or greater than <see cref="F:System.Decimal.MaxValue" />. </exception>
		/// <filterpriority>3</filterpriority>
		// Token: 0x060013E9 RID: 5097 RVA: 0x0004FF7A File Offset: 0x0004E17A
		public static decimal operator /(decimal d1, decimal d2)
		{
			decimal.DecCalc.VarDecDiv(decimal.AsMutable(ref d1), decimal.AsMutable(ref d2));
			return d1;
		}

		/// <summary>Returns a value that indicates whether two <see cref="T:System.Decimal" /> values are equal.</summary>
		/// <returns>true if <paramref name="d1" /> and <paramref name="d2" /> are equal; otherwise, false.</returns>
		/// <param name="d1">The first value to compare. </param>
		/// <param name="d2">The second value to compare. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x060013EA RID: 5098 RVA: 0x0005057A File Offset: 0x0004E77A
		public static bool operator ==(decimal d1, decimal d2)
		{
			return decimal.DecCalc.VarDecCmp(in d1, in d2) == 0;
		}

		/// <summary>Returns a value that indicates whether two <see cref="T:System.Decimal" /> objects have different values.</summary>
		/// <returns>true if <paramref name="d1" /> and <paramref name="d2" /> are not equal; otherwise, false.</returns>
		/// <param name="d1">The first value to compare. </param>
		/// <param name="d2">The second value to compare. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x060013EB RID: 5099 RVA: 0x00050588 File Offset: 0x0004E788
		public static bool operator !=(decimal d1, decimal d2)
		{
			return decimal.DecCalc.VarDecCmp(in d1, in d2) != 0;
		}

		/// <summary>Returns a value indicating whether a specified <see cref="T:System.Decimal" /> is less than another specified <see cref="T:System.Decimal" />.</summary>
		/// <returns>true if <paramref name="d1" /> is less than <paramref name="d2" />; otherwise, false.</returns>
		/// <param name="d1">The first value to compare. </param>
		/// <param name="d2">The second value to compare. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x060013EC RID: 5100 RVA: 0x00050596 File Offset: 0x0004E796
		public static bool operator <(decimal d1, decimal d2)
		{
			return decimal.DecCalc.VarDecCmp(in d1, in d2) < 0;
		}

		/// <summary>Returns a value indicating whether a specified <see cref="T:System.Decimal" /> is less than or equal to another specified <see cref="T:System.Decimal" />.</summary>
		/// <returns>true if <paramref name="d1" /> is less than or equal to <paramref name="d2" />; otherwise, false.</returns>
		/// <param name="d1">The first value to compare. </param>
		/// <param name="d2">The second value to compare. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x060013ED RID: 5101 RVA: 0x000505A4 File Offset: 0x0004E7A4
		public static bool operator <=(decimal d1, decimal d2)
		{
			return decimal.DecCalc.VarDecCmp(in d1, in d2) <= 0;
		}

		/// <summary>Returns a value indicating whether a specified <see cref="T:System.Decimal" /> is greater than another specified <see cref="T:System.Decimal" />.</summary>
		/// <returns>true if <paramref name="d1" /> is greater than <paramref name="d2" />; otherwise, false.</returns>
		/// <param name="d1">The first value to compare. </param>
		/// <param name="d2">The second value to compare. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x060013EE RID: 5102 RVA: 0x000505B5 File Offset: 0x0004E7B5
		public static bool operator >(decimal d1, decimal d2)
		{
			return decimal.DecCalc.VarDecCmp(in d1, in d2) > 0;
		}

		/// <summary>Returns a value indicating whether a specified <see cref="T:System.Decimal" /> is greater than or equal to another specified <see cref="T:System.Decimal" />.</summary>
		/// <returns>true if <paramref name="d1" /> is greater than or equal to <paramref name="d2" />; otherwise, false.</returns>
		/// <param name="d1">The first value to compare. </param>
		/// <param name="d2">The second value to compare. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x060013EF RID: 5103 RVA: 0x000505C3 File Offset: 0x0004E7C3
		public static bool operator >=(decimal d1, decimal d2)
		{
			return decimal.DecCalc.VarDecCmp(in d1, in d2) >= 0;
		}

		/// <summary>Returns the <see cref="T:System.TypeCode" /> for value type <see cref="T:System.Decimal" />.</summary>
		/// <returns>The enumerated constant <see cref="F:System.TypeCode.Decimal" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060013F0 RID: 5104 RVA: 0x000505D4 File Offset: 0x0004E7D4
		public TypeCode GetTypeCode()
		{
			return TypeCode.Decimal;
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToBoolean(System.IFormatProvider)" />.</summary>
		/// <returns>true if the value of the current instance is not zero; otherwise, false.</returns>
		/// <param name="provider">This parameter is ignored. </param>
		// Token: 0x060013F1 RID: 5105 RVA: 0x000505D8 File Offset: 0x0004E7D8
		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			return Convert.ToBoolean(this);
		}

		/// <summary>This conversion is not supported. Attempting to use this method throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>None. This conversion is not supported. </returns>
		/// <param name="provider">This parameter is ignored.</param>
		/// <exception cref="T:System.InvalidCastException">In all cases. </exception>
		// Token: 0x060013F2 RID: 5106 RVA: 0x000505E5 File Offset: 0x0004E7E5
		char IConvertible.ToChar(IFormatProvider provider)
		{
			throw new InvalidCastException(SR.Format("Invalid cast from '{0}' to '{1}'.", "Decimal", "Char"));
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToSByte(System.IFormatProvider)" />.</summary>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.SByte" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		/// <exception cref="T:System.OverflowException">The resulting integer value is less than <see cref="F:System.SByte.MinValue" /> or greater than <see cref="F:System.SByte.MaxValue" />. </exception>
		// Token: 0x060013F3 RID: 5107 RVA: 0x00050600 File Offset: 0x0004E800
		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			return Convert.ToSByte(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToByte(System.IFormatProvider)" />.</summary>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.Byte" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		/// <exception cref="T:System.OverflowException">The resulting integer value is less than <see cref="F:System.Byte.MinValue" /> or greater than <see cref="F:System.Byte.MaxValue" />. </exception>
		// Token: 0x060013F4 RID: 5108 RVA: 0x0005060D File Offset: 0x0004E80D
		byte IConvertible.ToByte(IFormatProvider provider)
		{
			return Convert.ToByte(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToInt16(System.IFormatProvider)" />.</summary>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.Int16" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		/// <exception cref="T:System.OverflowException">The resulting integer value is less than <see cref="F:System.Int16.MinValue" /> or greater than <see cref="F:System.Int16.MaxValue" />.</exception>
		// Token: 0x060013F5 RID: 5109 RVA: 0x0005061A File Offset: 0x0004E81A
		short IConvertible.ToInt16(IFormatProvider provider)
		{
			return Convert.ToInt16(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToUInt16(System.IFormatProvider)" />.</summary>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.UInt16" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		/// <exception cref="T:System.OverflowException">The resulting integer value is less than <see cref="F:System.UInt16.MinValue" /> or greater than <see cref="F:System.UInt16.MaxValue" />.</exception>
		// Token: 0x060013F6 RID: 5110 RVA: 0x00050627 File Offset: 0x0004E827
		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			return Convert.ToUInt16(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToInt32(System.IFormatProvider)" />.</summary>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.Int32" />.</returns>
		/// <param name="provider">The parameter is ignored.</param>
		/// <exception cref="T:System.OverflowException">The resulting integer value is less than <see cref="F:System.Int32.MinValue" /> or greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		// Token: 0x060013F7 RID: 5111 RVA: 0x00050634 File Offset: 0x0004E834
		int IConvertible.ToInt32(IFormatProvider provider)
		{
			return Convert.ToInt32(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToInt32(System.IFormatProvider)" />.</summary>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.UInt32" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		/// <exception cref="T:System.OverflowException">The resulting integer value is less than <see cref="F:System.UInt32.MinValue" /> or greater than <see cref="F:System.UInt32.MaxValue" />.</exception>
		// Token: 0x060013F8 RID: 5112 RVA: 0x00050641 File Offset: 0x0004E841
		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			return Convert.ToUInt32(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToInt64(System.IFormatProvider)" />.</summary>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.Int64" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		/// <exception cref="T:System.OverflowException">The resulting integer value is less than <see cref="F:System.Int64.MinValue" /> or greater than <see cref="F:System.Int64.MaxValue" />. </exception>
		// Token: 0x060013F9 RID: 5113 RVA: 0x0005064E File Offset: 0x0004E84E
		long IConvertible.ToInt64(IFormatProvider provider)
		{
			return Convert.ToInt64(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToInt64(System.IFormatProvider)" />.</summary>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.UInt64" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		/// <exception cref="T:System.OverflowException">The resulting integer value is less than <see cref="F:System.UInt64.MinValue" /> or greater than <see cref="F:System.UInt64.MaxValue" />.</exception>
		// Token: 0x060013FA RID: 5114 RVA: 0x0005065B File Offset: 0x0004E85B
		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			return Convert.ToUInt64(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToSingle(System.IFormatProvider)" />.</summary>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.Single" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x060013FB RID: 5115 RVA: 0x00050668 File Offset: 0x0004E868
		float IConvertible.ToSingle(IFormatProvider provider)
		{
			return Convert.ToSingle(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToDouble(System.IFormatProvider)" />.</summary>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.Double" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x060013FC RID: 5116 RVA: 0x00050675 File Offset: 0x0004E875
		double IConvertible.ToDouble(IFormatProvider provider)
		{
			return Convert.ToDouble(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToDecimal(System.IFormatProvider)" />.</summary>
		/// <returns>The value of the current instance, unchanged.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x060013FD RID: 5117 RVA: 0x00050682 File Offset: 0x0004E882
		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			return this;
		}

		/// <summary>This conversion is not supported. Attempting to use this method throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>None. This conversion is not supported. </returns>
		/// <param name="provider">This parameter is ignored.</param>
		/// <exception cref="T:System.InvalidCastException">In all cases.</exception>
		// Token: 0x060013FE RID: 5118 RVA: 0x0005068A File Offset: 0x0004E88A
		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			throw new InvalidCastException(SR.Format("Invalid cast from '{0}' to '{1}'.", "Decimal", "DateTime"));
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToType(System.Type,System.IFormatProvider)" />.</summary>
		/// <returns>The value of the current instance, converted to a <paramref name="type" />.</returns>
		/// <param name="type">The type to which to convert the value of this <see cref="T:System.Decimal" /> instance. </param>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> implementation that supplies culture-specific information about the format of the returned value.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is null. </exception>
		/// <exception cref="T:System.InvalidCastException">The requested type conversion is not supported. </exception>
		// Token: 0x060013FF RID: 5119 RVA: 0x000506A5 File Offset: 0x0004E8A5
		object IConvertible.ToType(Type type, IFormatProvider provider)
		{
			return Convert.DefaultToType(this, type, provider);
		}

		/// <summary>Represents the number zero (0).</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x040009B9 RID: 2489
		public const decimal Zero = 0m;

		/// <summary>Represents the number one (1).</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x040009BA RID: 2490
		public const decimal One = 1m;

		/// <summary>Represents the number negative one (-1).</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x040009BB RID: 2491
		public const decimal MinusOne = -1m;

		/// <summary>Represents the largest possible value of <see cref="T:System.Decimal" />. This field is constant and read-only.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x040009BC RID: 2492
		public const decimal MaxValue = 79228162514264337593543950335m;

		/// <summary>Represents the smallest possible value of <see cref="T:System.Decimal" />. This field is constant and read-only.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x040009BD RID: 2493
		public const decimal MinValue = -79228162514264337593543950335m;

		// Token: 0x040009BE RID: 2494
		[FieldOffset(0)]
		private readonly int flags;

		// Token: 0x040009BF RID: 2495
		[FieldOffset(4)]
		private readonly int hi;

		// Token: 0x040009C0 RID: 2496
		[FieldOffset(8)]
		private readonly int lo;

		// Token: 0x040009C1 RID: 2497
		[FieldOffset(12)]
		private readonly int mid;

		// Token: 0x040009C2 RID: 2498
		[NonSerialized]
		[FieldOffset(8)]
		private readonly ulong ulomidLE;

		// Token: 0x02000206 RID: 518
		[StructLayout(LayoutKind.Explicit)]
		private struct DecCalc
		{
			// Token: 0x1700020B RID: 523
			// (get) Token: 0x06001401 RID: 5121 RVA: 0x00050708 File Offset: 0x0004E908
			// (set) Token: 0x06001402 RID: 5122 RVA: 0x00050710 File Offset: 0x0004E910
			private uint High
			{
				get
				{
					return this.uhi;
				}
				set
				{
					this.uhi = value;
				}
			}

			// Token: 0x1700020C RID: 524
			// (get) Token: 0x06001403 RID: 5123 RVA: 0x00050719 File Offset: 0x0004E919
			// (set) Token: 0x06001404 RID: 5124 RVA: 0x00050721 File Offset: 0x0004E921
			private uint Low
			{
				get
				{
					return this.ulo;
				}
				set
				{
					this.ulo = value;
				}
			}

			// Token: 0x1700020D RID: 525
			// (get) Token: 0x06001405 RID: 5125 RVA: 0x0005072A File Offset: 0x0004E92A
			// (set) Token: 0x06001406 RID: 5126 RVA: 0x00050732 File Offset: 0x0004E932
			private uint Mid
			{
				get
				{
					return this.umid;
				}
				set
				{
					this.umid = value;
				}
			}

			// Token: 0x1700020E RID: 526
			// (get) Token: 0x06001407 RID: 5127 RVA: 0x0005073B File Offset: 0x0004E93B
			private bool IsNegative
			{
				get
				{
					return this.uflags < 0U;
				}
			}

			// Token: 0x1700020F RID: 527
			// (get) Token: 0x06001408 RID: 5128 RVA: 0x00050746 File Offset: 0x0004E946
			// (set) Token: 0x06001409 RID: 5129 RVA: 0x00050768 File Offset: 0x0004E968
			private ulong Low64
			{
				get
				{
					if (!BitConverter.IsLittleEndian)
					{
						return ((ulong)this.umid << 32) | (ulong)this.ulo;
					}
					return this.ulomidLE;
				}
				set
				{
					if (BitConverter.IsLittleEndian)
					{
						this.ulomidLE = value;
						return;
					}
					this.umid = (uint)(value >> 32);
					this.ulo = (uint)value;
				}
			}

			// Token: 0x0600140A RID: 5130 RVA: 0x0005078C File Offset: 0x0004E98C
			private unsafe static uint GetExponent(float f)
			{
				return (uint)((byte)(*(uint*)(&f) >> 23));
			}

			// Token: 0x0600140B RID: 5131 RVA: 0x00050796 File Offset: 0x0004E996
			private unsafe static uint GetExponent(double d)
			{
				return (uint)((ulong)(*(long*)(&d)) >> 52) & 2047U;
			}

			// Token: 0x0600140C RID: 5132 RVA: 0x0002DECE File Offset: 0x0002C0CE
			private static ulong UInt32x32To64(uint a, uint b)
			{
				return (ulong)a * (ulong)b;
			}

			// Token: 0x0600140D RID: 5133 RVA: 0x000507A8 File Offset: 0x0004E9A8
			private static void UInt64x64To128(ulong a, ulong b, ref decimal.DecCalc result)
			{
				ulong num = decimal.DecCalc.UInt32x32To64((uint)a, (uint)b);
				ulong num2 = decimal.DecCalc.UInt32x32To64((uint)a, (uint)(b >> 32));
				ulong num3 = decimal.DecCalc.UInt32x32To64((uint)(a >> 32), (uint)(b >> 32));
				num3 += num2 >> 32;
				num += (num2 <<= 32);
				if (num < num2)
				{
					num3 += 1UL;
				}
				num2 = decimal.DecCalc.UInt32x32To64((uint)(a >> 32), (uint)b);
				num3 += num2 >> 32;
				num += (num2 <<= 32);
				if (num < num2)
				{
					num3 += 1UL;
				}
				if (num3 > (ulong)(-1))
				{
					throw new OverflowException("Value was either too large or too small for a Decimal.");
				}
				result.Low64 = num;
				result.High = (uint)num3;
			}

			// Token: 0x0600140E RID: 5134 RVA: 0x0005083C File Offset: 0x0004EA3C
			private static uint Div96By32(ref decimal.DecCalc.Buf12 bufNum, uint den)
			{
				if (bufNum.U2 != 0U)
				{
					ulong num = bufNum.High64;
					ulong num2 = num / (ulong)den;
					bufNum.High64 = num2;
					num = (num - (ulong)((uint)num2 * den) << 32) | (ulong)bufNum.U0;
					if (num == 0UL)
					{
						return 0U;
					}
					uint num3 = (uint)(num / (ulong)den);
					bufNum.U0 = num3;
					return (uint)num - num3 * den;
				}
				else
				{
					ulong num = bufNum.Low64;
					if (num == 0UL)
					{
						return 0U;
					}
					ulong num2 = num / (ulong)den;
					bufNum.Low64 = num2;
					return (uint)(num - num2 * (ulong)den);
				}
			}

			// Token: 0x0600140F RID: 5135 RVA: 0x000508B0 File Offset: 0x0004EAB0
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			private static bool Div96ByConst(ref ulong high64, ref uint low, uint pow)
			{
				ulong num = high64 / (ulong)pow;
				uint num2 = (uint)(((high64 - num * (ulong)pow << 32) + (ulong)low) / (ulong)pow);
				if (low == num2 * pow)
				{
					high64 = num;
					low = num2;
					return true;
				}
				return false;
			}

			// Token: 0x06001410 RID: 5136 RVA: 0x000508E8 File Offset: 0x0004EAE8
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			private static void Unscale(ref uint low, ref ulong high64, ref int scale)
			{
				while ((byte)low == 0 && scale >= 8 && decimal.DecCalc.Div96ByConst(ref high64, ref low, 100000000U))
				{
					scale -= 8;
				}
				if ((low & 15U) == 0U && scale >= 4 && decimal.DecCalc.Div96ByConst(ref high64, ref low, 10000U))
				{
					scale -= 4;
				}
				if ((low & 3U) == 0U && scale >= 2 && decimal.DecCalc.Div96ByConst(ref high64, ref low, 100U))
				{
					scale -= 2;
				}
				if ((low & 1U) == 0U && scale >= 1 && decimal.DecCalc.Div96ByConst(ref high64, ref low, 10U))
				{
					scale--;
				}
			}

			// Token: 0x06001411 RID: 5137 RVA: 0x00050970 File Offset: 0x0004EB70
			private static uint Div96By64(ref decimal.DecCalc.Buf12 bufNum, ulong den)
			{
				uint u = bufNum.U2;
				if (u == 0U)
				{
					ulong num = bufNum.Low64;
					if (num < den)
					{
						return 0U;
					}
					uint num2 = (uint)(num / den);
					num -= (ulong)num2 * den;
					bufNum.Low64 = num;
					return num2;
				}
				else
				{
					uint num3 = (uint)(den >> 32);
					ulong num;
					uint num2;
					if (u >= num3)
					{
						num = bufNum.Low64;
						num -= den << 32;
						num2 = 0U;
						do
						{
							num2 -= 1U;
							num += den;
						}
						while (num >= den);
						bufNum.Low64 = num;
						return num2;
					}
					ulong high = bufNum.High64;
					if (high < (ulong)num3)
					{
						return 0U;
					}
					num2 = (uint)(high / (ulong)num3);
					num = (ulong)bufNum.U0 | (high - (ulong)(num2 * num3) << 32);
					ulong num4 = decimal.DecCalc.UInt32x32To64(num2, (uint)den);
					num -= num4;
					if (num > ~num4)
					{
						do
						{
							num2 -= 1U;
							num += den;
						}
						while (num >= den);
					}
					bufNum.Low64 = num;
					return num2;
				}
			}

			// Token: 0x06001412 RID: 5138 RVA: 0x00050A2C File Offset: 0x0004EC2C
			private static uint Div128By96(ref decimal.DecCalc.Buf16 bufNum, ref decimal.DecCalc.Buf12 bufDen)
			{
				ulong high = bufNum.High64;
				uint u = bufDen.U2;
				if (high < (ulong)u)
				{
					return 0U;
				}
				uint num = (uint)(high / (ulong)u);
				uint num2 = (uint)high - num * u;
				ulong num3 = decimal.DecCalc.UInt32x32To64(num, bufDen.U0);
				ulong num4 = decimal.DecCalc.UInt32x32To64(num, bufDen.U1);
				num4 += num3 >> 32;
				num3 = (ulong)((uint)num3) | (num4 << 32);
				num4 >>= 32;
				ulong num5 = bufNum.Low64;
				num5 -= num3;
				num2 -= (uint)num4;
				if (num5 > ~num3)
				{
					num2 -= 1U;
					if (num2 < ~(uint)num4)
					{
						goto IL_00B4;
					}
				}
				else if (num2 <= ~(uint)num4)
				{
					goto IL_00B4;
				}
				num3 = bufDen.Low64;
				do
				{
					num -= 1U;
					num5 += num3;
					num2 += u;
				}
				while ((num5 >= num3 || num2++ >= u) && num2 >= u);
				IL_00B4:
				bufNum.Low64 = num5;
				bufNum.U2 = num2;
				return num;
			}

			// Token: 0x06001413 RID: 5139 RVA: 0x00050B00 File Offset: 0x0004ED00
			private static uint IncreaseScale(ref decimal.DecCalc.Buf12 bufNum, uint power)
			{
				ulong num = decimal.DecCalc.UInt32x32To64(bufNum.U0, power);
				bufNum.U0 = (uint)num;
				num >>= 32;
				num += decimal.DecCalc.UInt32x32To64(bufNum.U1, power);
				bufNum.U1 = (uint)num;
				num >>= 32;
				num += decimal.DecCalc.UInt32x32To64(bufNum.U2, power);
				bufNum.U2 = (uint)num;
				return (uint)(num >> 32);
			}

			// Token: 0x06001414 RID: 5140 RVA: 0x00050B60 File Offset: 0x0004ED60
			private static void IncreaseScale64(ref decimal.DecCalc.Buf12 bufNum, uint power)
			{
				ulong num = decimal.DecCalc.UInt32x32To64(bufNum.U0, power);
				bufNum.U0 = (uint)num;
				num >>= 32;
				num += decimal.DecCalc.UInt32x32To64(bufNum.U1, power);
				bufNum.High64 = num;
			}

			// Token: 0x06001415 RID: 5141 RVA: 0x00050BA0 File Offset: 0x0004EDA0
			private unsafe static int ScaleResult(decimal.DecCalc.Buf24* bufRes, uint hiRes, int scale)
			{
				int num = 0;
				if (hiRes > 2U)
				{
					num = (int)(hiRes * 32U - 64U - 1U);
					num -= decimal.DecCalc.LeadingZeroCount(*(uint*)(bufRes + (ulong)hiRes * 4UL / (ulong)sizeof(decimal.DecCalc.Buf24)));
					num = (num * 77 >> 8) + 1;
					if (num > scale)
					{
						goto IL_01CC;
					}
				}
				if (num < scale - 28)
				{
					num = scale - 28;
				}
				if (num != 0)
				{
					scale -= num;
					uint num2 = 0U;
					uint num3 = 0U;
					for (;;)
					{
						num2 |= num3;
						uint num5;
						uint num4;
						switch (num)
						{
						case 1:
							num4 = decimal.DecCalc.DivByConst((uint*)bufRes, hiRes, out num5, out num3, 10U);
							break;
						case 2:
							num4 = decimal.DecCalc.DivByConst((uint*)bufRes, hiRes, out num5, out num3, 100U);
							break;
						case 3:
							num4 = decimal.DecCalc.DivByConst((uint*)bufRes, hiRes, out num5, out num3, 1000U);
							break;
						case 4:
							num4 = decimal.DecCalc.DivByConst((uint*)bufRes, hiRes, out num5, out num3, 10000U);
							break;
						case 5:
							num4 = decimal.DecCalc.DivByConst((uint*)bufRes, hiRes, out num5, out num3, 100000U);
							break;
						case 6:
							num4 = decimal.DecCalc.DivByConst((uint*)bufRes, hiRes, out num5, out num3, 1000000U);
							break;
						case 7:
							num4 = decimal.DecCalc.DivByConst((uint*)bufRes, hiRes, out num5, out num3, 10000000U);
							break;
						case 8:
							num4 = decimal.DecCalc.DivByConst((uint*)bufRes, hiRes, out num5, out num3, 100000000U);
							break;
						default:
							num4 = decimal.DecCalc.DivByConst((uint*)bufRes, hiRes, out num5, out num3, 1000000000U);
							break;
						}
						*(int*)(bufRes + (ulong)hiRes * 4UL / (ulong)sizeof(decimal.DecCalc.Buf24)) = (int)num5;
						if (num5 == 0U && hiRes != 0U)
						{
							hiRes -= 1U;
						}
						num -= 9;
						if (num <= 0)
						{
							if (hiRes > 2U)
							{
								if (scale == 0)
								{
									goto IL_01CC;
								}
								num = 1;
								scale--;
							}
							else
							{
								num4 >>= 1;
								if (num4 > num3 || (num4 >= num3 && ((*(uint*)bufRes & 1U) | num2) == 0U))
								{
									break;
								}
								uint num6 = *(uint*)bufRes + 1U;
								*(int*)bufRes = (int)num6;
								if (num6 != 0U)
								{
									break;
								}
								uint num7 = 0U;
								do
								{
									decimal.DecCalc.Buf24* ptr = bufRes + (ulong)(num7 += 1U) * 4UL / (ulong)sizeof(decimal.DecCalc.Buf24);
									num6 = *(uint*)ptr + 1U;
									*(int*)ptr = (int)num6;
								}
								while (num6 == 0U);
								if (num7 <= 2U)
								{
									break;
								}
								if (scale == 0)
								{
									goto IL_01CC;
								}
								hiRes = num7;
								num2 = 0U;
								num3 = 0U;
								num = 1;
								scale--;
							}
						}
					}
				}
				return scale;
				IL_01CC:
				throw new OverflowException("Value was either too large or too small for a Decimal.");
			}

			// Token: 0x06001416 RID: 5142 RVA: 0x00050D84 File Offset: 0x0004EF84
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			private unsafe static uint DivByConst(uint* result, uint hiRes, out uint quotient, out uint remainder, uint power)
			{
				uint num = result[(ulong)hiRes * 4UL / 4UL];
				remainder = num - (quotient = num / power) * power;
				for (uint num2 = hiRes - 1U; num2 >= 0U; num2 -= 1U)
				{
					ulong num3 = (ulong)result[(ulong)num2 * 4UL / 4UL] + ((ulong)remainder << 32);
					remainder = (uint)num3 - (result[(ulong)num2 * 4UL / 4UL] = (uint)(num3 / (ulong)power)) * power;
				}
				return power;
			}

			// Token: 0x06001417 RID: 5143 RVA: 0x00050DE8 File Offset: 0x0004EFE8
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			private static int LeadingZeroCount(uint value)
			{
				int num = 1;
				if ((value & 4294901760U) == 0U)
				{
					value <<= 16;
					num += 16;
				}
				if ((value & 4278190080U) == 0U)
				{
					value <<= 8;
					num += 8;
				}
				if ((value & 4026531840U) == 0U)
				{
					value <<= 4;
					num += 4;
				}
				if ((value & 3221225472U) == 0U)
				{
					value <<= 2;
					num += 2;
				}
				return num + ((int)value >> 31);
			}

			// Token: 0x06001418 RID: 5144 RVA: 0x00050E48 File Offset: 0x0004F048
			private static int OverflowUnscale(ref decimal.DecCalc.Buf12 bufQuo, int scale, bool sticky)
			{
				if (--scale < 0)
				{
					throw new OverflowException("Value was either too large or too small for a Decimal.");
				}
				bufQuo.U2 = 429496729U;
				ulong num = 25769803776UL + (ulong)bufQuo.U1;
				uint num2 = (uint)(num / 10UL);
				bufQuo.U1 = num2;
				ulong num3 = (num - (ulong)(num2 * 10U) << 32) + (ulong)bufQuo.U0;
				num2 = (uint)(num3 / 10UL);
				bufQuo.U0 = num2;
				uint num4 = (uint)(num3 - (ulong)(num2 * 10U));
				if (num4 > 5U || (num4 == 5U && (sticky || (bufQuo.U0 & 1U) != 0U)))
				{
					decimal.DecCalc.Add32To96(ref bufQuo, 1U);
				}
				return scale;
			}

			// Token: 0x06001419 RID: 5145 RVA: 0x00050ED8 File Offset: 0x0004F0D8
			private static int SearchScale(ref decimal.DecCalc.Buf12 bufQuo, int scale)
			{
				uint u = bufQuo.U2;
				ulong low = bufQuo.Low64;
				int num = 0;
				if (u <= 429496729U)
				{
					decimal.DecCalc.PowerOvfl[] powerOvflValues = decimal.DecCalc.PowerOvflValues;
					if (scale > 19)
					{
						num = 28 - scale;
						if (u < powerOvflValues[num - 1].Hi)
						{
							goto IL_00D1;
						}
					}
					else if (u < 4U || (u == 4U && low <= 5441186219426131129UL))
					{
						return 9;
					}
					if (u > 42949U)
					{
						if (u > 4294967U)
						{
							num = 2;
							if (u > 42949672U)
							{
								num--;
							}
						}
						else
						{
							num = 4;
							if (u > 429496U)
							{
								num--;
							}
						}
					}
					else if (u > 429U)
					{
						num = 6;
						if (u > 4294U)
						{
							num--;
						}
					}
					else
					{
						num = 8;
						if (u > 42U)
						{
							num--;
						}
					}
					if (u == powerOvflValues[num - 1].Hi && low > powerOvflValues[num - 1].MidLo)
					{
						num--;
					}
				}
				IL_00D1:
				if (num + scale < 0)
				{
					throw new OverflowException("Value was either too large or too small for a Decimal.");
				}
				return num;
			}

			// Token: 0x0600141A RID: 5146 RVA: 0x00050FC8 File Offset: 0x0004F1C8
			private static bool Add32To96(ref decimal.DecCalc.Buf12 bufNum, uint value)
			{
				if ((bufNum.Low64 += (ulong)value) < (ulong)value)
				{
					uint num = bufNum.U2 + 1U;
					bufNum.U2 = num;
					if (num == 0U)
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x0600141B RID: 5147 RVA: 0x00051000 File Offset: 0x0004F200
			internal unsafe static void DecAddSub(ref decimal.DecCalc d1, ref decimal.DecCalc d2, bool sign)
			{
				ulong num = d1.Low64;
				uint num2 = d1.High;
				uint num3 = d1.uflags;
				uint num4 = d2.uflags;
				uint num5 = num4 ^ num3;
				sign ^= (num5 & 2147483648U) > 0U;
				if ((num5 & 16711680U) != 0U)
				{
					uint num6 = num3;
					num3 = (num4 & 16711680U) | (num3 & 2147483648U);
					int i = (int)(num3 - num6) >> 16;
					if (i < 0)
					{
						i = -i;
						num3 = num6;
						if (sign)
						{
							num3 ^= 2147483648U;
						}
						num = d2.Low64;
						num2 = d2.High;
						d2 = d1;
					}
					ulong num10;
					if (num2 == 0U)
					{
						if (num <= (ulong)(-1))
						{
							if ((uint)num == 0U)
							{
								uint num7 = num3 & 2147483648U;
								if (sign)
								{
									num7 ^= 2147483648U;
								}
								d1 = d2;
								d1.uflags = (d2.uflags & 16711680U) | num7;
								return;
							}
							while (i > 9)
							{
								i -= 9;
								num = decimal.DecCalc.UInt32x32To64((uint)num, 1000000000U);
								if (num > (ulong)(-1))
								{
									goto IL_0106;
								}
							}
							num = decimal.DecCalc.UInt32x32To64((uint)num, decimal.DecCalc.s_powers10[i]);
							goto IL_0441;
						}
						do
						{
							IL_0106:
							uint num8 = 1000000000U;
							if (i < 9)
							{
								num8 = decimal.DecCalc.s_powers10[i];
							}
							ulong num9 = decimal.DecCalc.UInt32x32To64((uint)num, num8);
							num10 = decimal.DecCalc.UInt32x32To64((uint)(num >> 32), num8) + (num9 >> 32);
							num = (ulong)((uint)num9) + (num10 << 32);
							num2 = (uint)(num10 >> 32);
							if ((i -= 9) <= 0)
							{
								goto IL_0441;
							}
						}
						while (num2 == 0U);
					}
					do
					{
						uint num8 = 1000000000U;
						if (i < 9)
						{
							num8 = decimal.DecCalc.s_powers10[i];
						}
						ulong num9 = decimal.DecCalc.UInt32x32To64((uint)num, num8);
						num10 = decimal.DecCalc.UInt32x32To64((uint)(num >> 32), num8) + (num9 >> 32);
						num = (ulong)((uint)num9) + (num10 << 32);
						num10 >>= 32;
						num10 += decimal.DecCalc.UInt32x32To64(num2, num8);
						i -= 9;
						if (num10 > (ulong)(-1))
						{
							goto IL_01CF;
						}
						num2 = (uint)num10;
					}
					while (i > 0);
					goto IL_0441;
					IL_01CF:
					decimal.DecCalc.Buf24 buf;
					buf.Low64 = num;
					buf.Mid64 = num10;
					uint num11 = 3U;
					while (i > 0)
					{
						uint num8 = 1000000000U;
						if (i < 9)
						{
							num8 = decimal.DecCalc.s_powers10[i];
						}
						num10 = 0UL;
						uint* ptr = (uint*)(&buf);
						uint num12 = 0U;
						do
						{
							num10 += decimal.DecCalc.UInt32x32To64(ptr[(ulong)num12 * 4UL / 4UL], num8);
							ptr[(ulong)num12 * 4UL / 4UL] = (uint)num10;
							num12 += 1U;
							num10 >>= 32;
						}
						while (num12 <= num11);
						if ((uint)num10 != 0U)
						{
							ptr[(IntPtr)((ulong)(num11 += 1U) * 4UL)] = (uint)num10;
						}
						i -= 9;
					}
					num10 = buf.Low64;
					num = d2.Low64;
					uint u = buf.U2;
					num2 = d2.High;
					if (sign)
					{
						num = num10 - num;
						num2 = u - num2;
						if (num > num10)
						{
							num2 -= 1U;
							if (num2 < u)
							{
								goto IL_034C;
							}
						}
						else if (num2 <= u)
						{
							goto IL_034C;
						}
						uint* ptr2 = (uint*)(&buf);
						uint num13 = 3U;
						uint num14;
						do
						{
							uint* ptr3 = ptr2 + (IntPtr)((ulong)num13++ * 4UL);
							num14 = *ptr3;
							*ptr3 = num14 - 1U;
						}
						while (num14 == 0U);
						if (ptr2[(ulong)num11 * 4UL / 4UL] == 0U && (num11 -= 1U) <= 2U)
						{
							goto IL_04AA;
						}
					}
					else
					{
						num += num10;
						num2 += u;
						if (num < num10)
						{
							num2 += 1U;
							if (num2 > u)
							{
								goto IL_034C;
							}
						}
						else if (num2 >= u)
						{
							goto IL_034C;
						}
						uint* ptr4 = (uint*)(&buf);
						uint num15 = 3U;
						do
						{
							uint* ptr5 = ptr4 + (IntPtr)((ulong)num15++ * 4UL);
							uint num14 = *ptr5 + 1U;
							*ptr5 = num14;
							if (num14 != 0U)
							{
								goto IL_034C;
							}
						}
						while (num11 >= num15);
						ptr4[(ulong)num15 * 4UL / 4UL] = 1U;
						num11 = num15;
					}
					IL_034C:
					buf.Low64 = num;
					buf.U2 = num2;
					i = decimal.DecCalc.ScaleResult(&buf, num11, (int)((byte)(num3 >> 16)));
					num3 = (num3 & 4278255615U) | (uint)((uint)i << 16);
					num = buf.Low64;
					num2 = buf.U2;
					goto IL_04AA;
				}
				IL_0441:
				ulong num16 = num;
				uint num17 = num2;
				if (sign)
				{
					num = num16 - d2.Low64;
					num2 = num17 - d2.High;
					if (num > num16)
					{
						num2 -= 1U;
						if (num2 < num17)
						{
							goto IL_04AA;
						}
					}
					else if (num2 <= num17)
					{
						goto IL_04AA;
					}
					num3 ^= 2147483648U;
					num2 = ~num2;
					num = -num;
					if (num == 0UL)
					{
						num2 += 1U;
					}
				}
				else
				{
					num = num16 + d2.Low64;
					num2 = num17 + d2.High;
					if (num < num16)
					{
						num2 += 1U;
						if (num2 > num17)
						{
							goto IL_04AA;
						}
					}
					else if (num2 >= num17)
					{
						goto IL_04AA;
					}
					if ((num3 & 16711680U) == 0U)
					{
						throw new OverflowException("Value was either too large or too small for a Decimal.");
					}
					num3 -= 65536U;
					ulong num18 = (ulong)num2 + 4294967296UL;
					num2 = (uint)(num18 / 10UL);
					ulong num19 = (num18 - (ulong)(num2 * 10U) << 32) + (num >> 32);
					uint num20 = (uint)(num19 / 10UL);
					ulong num21 = (num19 - (ulong)(num20 * 10U) << 32) + (ulong)((uint)num);
					num = (ulong)num20;
					num <<= 32;
					num20 = (uint)(num21 / 10UL);
					num += (ulong)num20;
					num20 = (uint)num21 - num20 * 10U;
					if (num20 >= 5U && (num20 > 5U || (num & 1UL) != 0UL) && (num += 1UL) == 0UL)
					{
						num2 += 1U;
					}
				}
				IL_04AA:
				d1.uflags = num3;
				d1.High = num2;
				d1.Low64 = num;
			}

			// Token: 0x0600141C RID: 5148 RVA: 0x000514CC File Offset: 0x0004F6CC
			internal static int VarDecCmp(in decimal d1, in decimal d2)
			{
				if ((d2.Low | d2.Mid | d2.High) == 0U)
				{
					if ((d1.Low | d1.Mid | d1.High) == 0U)
					{
						return 0;
					}
					return (d1.flags >> 31) | 1;
				}
				else
				{
					if ((d1.Low | d1.Mid | d1.High) == 0U)
					{
						return -((d2.flags >> 31) | 1);
					}
					int num = (d1.flags >> 31) - (d2.flags >> 31);
					if (num != 0)
					{
						return num;
					}
					return decimal.DecCalc.VarDecCmpSub(in d1, in d2);
				}
			}

			// Token: 0x0600141D RID: 5149 RVA: 0x00051558 File Offset: 0x0004F758
			private static int VarDecCmpSub(in decimal d1, in decimal d2)
			{
				int flags = d2.flags;
				int num = (flags >> 31) | 1;
				int num2 = flags - d1.flags;
				ulong num3 = d1.Low64;
				uint num4 = d1.High;
				ulong num5 = d2.Low64;
				uint num6 = d2.High;
				if (num2 != 0)
				{
					num2 >>= 16;
					if (num2 < 0)
					{
						num2 = -num2;
						num = -num;
						ulong num7 = num3;
						num3 = num5;
						num5 = num7;
						uint num8 = num4;
						num4 = num6;
						num6 = num8;
					}
					for (;;)
					{
						uint num9 = ((num2 >= 9) ? 1000000000U : decimal.DecCalc.s_powers10[num2]);
						ulong num10 = decimal.DecCalc.UInt32x32To64((uint)num3, num9);
						ulong num11 = decimal.DecCalc.UInt32x32To64((uint)(num3 >> 32), num9) + (num10 >> 32);
						num3 = (ulong)((uint)num10) + (num11 << 32);
						num11 >>= 32;
						num11 += decimal.DecCalc.UInt32x32To64(num4, num9);
						if (num11 > (ulong)(-1))
						{
							break;
						}
						num4 = (uint)num11;
						if ((num2 -= 9) <= 0)
						{
							goto IL_00BC;
						}
					}
					return num;
				}
				IL_00BC:
				uint num12 = num4 - num6;
				if (num12 != 0U)
				{
					if (num12 > num4)
					{
						num = -num;
					}
					return num;
				}
				ulong num13 = num3 - num5;
				if (num13 == 0UL)
				{
					num = 0;
				}
				else if (num13 > num3)
				{
					num = -num;
				}
				return num;
			}

			// Token: 0x0600141E RID: 5150 RVA: 0x0005164C File Offset: 0x0004F84C
			internal unsafe static void VarDecMul(ref decimal.DecCalc d1, ref decimal.DecCalc d2)
			{
				int num = (int)((byte)(d1.uflags + d2.uflags >> 16));
				decimal.DecCalc.Buf24 buf;
				uint num6;
				if ((d1.High | d1.Mid) == 0U)
				{
					ulong num4;
					if ((d2.High | d2.Mid) == 0U)
					{
						ulong num2 = decimal.DecCalc.UInt32x32To64(d1.Low, d2.Low);
						if (num > 28)
						{
							if (num > 47)
							{
								goto IL_03CD;
							}
							num -= 29;
							ulong num3 = decimal.DecCalc.s_ulongPowers10[num];
							num4 = num2 / num3;
							ulong num5 = num2 - num4 * num3;
							num2 = num4;
							num3 >>= 1;
							if (num5 >= num3 && (num5 > num3 || ((uint)num2 & 1U) > 0U))
							{
								num2 += 1UL;
							}
							num = 28;
						}
						d1.Low64 = num2;
						d1.uflags = ((d2.uflags ^ d1.uflags) & 2147483648U) | (uint)((uint)num << 16);
						return;
					}
					num4 = decimal.DecCalc.UInt32x32To64(d1.Low, d2.Low);
					buf.U0 = (uint)num4;
					num4 = decimal.DecCalc.UInt32x32To64(d1.Low, d2.Mid) + (num4 >> 32);
					buf.U1 = (uint)num4;
					num4 >>= 32;
					if (d2.High != 0U)
					{
						num4 += decimal.DecCalc.UInt32x32To64(d1.Low, d2.High);
						if (num4 > (ulong)(-1))
						{
							buf.Mid64 = num4;
							num6 = 3U;
							goto IL_0381;
						}
					}
					if ((uint)num4 != 0U)
					{
						buf.U2 = (uint)num4;
						num6 = 2U;
						goto IL_0381;
					}
					num6 = 1U;
				}
				else if ((d2.High | d2.Mid) == 0U)
				{
					ulong num4 = decimal.DecCalc.UInt32x32To64(d2.Low, d1.Low);
					buf.U0 = (uint)num4;
					num4 = decimal.DecCalc.UInt32x32To64(d2.Low, d1.Mid) + (num4 >> 32);
					buf.U1 = (uint)num4;
					num4 >>= 32;
					if (d1.High != 0U)
					{
						num4 += decimal.DecCalc.UInt32x32To64(d2.Low, d1.High);
						if (num4 > (ulong)(-1))
						{
							buf.Mid64 = num4;
							num6 = 3U;
							goto IL_0381;
						}
					}
					if ((uint)num4 != 0U)
					{
						buf.U2 = (uint)num4;
						num6 = 2U;
						goto IL_0381;
					}
					num6 = 1U;
				}
				else
				{
					ulong num4 = decimal.DecCalc.UInt32x32To64(d1.Low, d2.Low);
					buf.U0 = (uint)num4;
					ulong num7 = decimal.DecCalc.UInt32x32To64(d1.Low, d2.Mid) + (num4 >> 32);
					num4 = decimal.DecCalc.UInt32x32To64(d1.Mid, d2.Low);
					num4 += num7;
					buf.U1 = (uint)num4;
					if (num4 < num7)
					{
						num7 = (num4 >> 32) | 4294967296UL;
					}
					else
					{
						num7 = num4 >> 32;
					}
					num4 = decimal.DecCalc.UInt32x32To64(d1.Mid, d2.Mid) + num7;
					if ((d1.High | d2.High) > 0U)
					{
						num7 = decimal.DecCalc.UInt32x32To64(d1.Low, d2.High);
						num4 += num7;
						uint num8 = 0U;
						if (num4 < num7)
						{
							num8 = 1U;
						}
						num7 = decimal.DecCalc.UInt32x32To64(d1.High, d2.Low);
						num4 += num7;
						buf.U2 = (uint)num4;
						if (num4 < num7)
						{
							num8 += 1U;
						}
						num7 = ((ulong)num8 << 32) | (num4 >> 32);
						num4 = decimal.DecCalc.UInt32x32To64(d1.Mid, d2.High);
						num4 += num7;
						num8 = 0U;
						if (num4 < num7)
						{
							num8 = 1U;
						}
						num7 = decimal.DecCalc.UInt32x32To64(d1.High, d2.Mid);
						num4 += num7;
						buf.U3 = (uint)num4;
						if (num4 < num7)
						{
							num8 += 1U;
						}
						num4 = ((ulong)num8 << 32) | (num4 >> 32);
						buf.High64 = decimal.DecCalc.UInt32x32To64(d1.High, d2.High) + num4;
						num6 = 5U;
					}
					else if (num4 != 0UL)
					{
						buf.Mid64 = num4;
						num6 = 3U;
					}
					else
					{
						num6 = 1U;
					}
				}
				uint* ptr = (uint*)(&buf);
				while (ptr[num6] == 0U)
				{
					if (num6 == 0U)
					{
						goto IL_03CD;
					}
					num6 -= 1U;
				}
				IL_0381:
				if (num6 > 2U || num > 28)
				{
					num = decimal.DecCalc.ScaleResult(&buf, num6, num);
				}
				d1.Low64 = buf.Low64;
				d1.High = buf.U2;
				d1.uflags = ((d2.uflags ^ d1.uflags) & 2147483648U) | (uint)((uint)num << 16);
				return;
				IL_03CD:
				d1 = default(decimal.DecCalc);
			}

			// Token: 0x0600141F RID: 5151 RVA: 0x00051A30 File Offset: 0x0004FC30
			internal static void VarDecFromR4(float input, out decimal.DecCalc result)
			{
				result = default(decimal.DecCalc);
				int num = (int)(decimal.DecCalc.GetExponent(input) - 126U);
				if (num < -94)
				{
					return;
				}
				if (num > 96)
				{
					throw new OverflowException("Value was either too large or too small for a Decimal.");
				}
				uint num2 = 0U;
				if (input < 0f)
				{
					input = -input;
					num2 = 2147483648U;
				}
				double num3 = (double)input;
				int num4 = 6 - (num * 19728 >> 16);
				if (num4 >= 0)
				{
					if (num4 > 28)
					{
						num4 = 28;
					}
					num3 *= decimal.DecCalc.s_doublePowers10[num4];
				}
				else if (num4 != -1 || num3 >= 10000000.0)
				{
					num3 /= decimal.DecCalc.s_doublePowers10[-num4];
				}
				else
				{
					num4 = 0;
				}
				if (num3 < 1000000.0 && num4 < 28)
				{
					num3 *= 10.0;
					num4++;
				}
				uint num5 = (uint)((int)num3);
				num3 -= (double)num5;
				if (num3 > 0.5 || (num3 == 0.5 && (num5 & 1U) != 0U))
				{
					num5 += 1U;
				}
				if (num5 == 0U)
				{
					return;
				}
				if (num4 < 0)
				{
					num4 = -num4;
					if (num4 < 10)
					{
						result.Low64 = decimal.DecCalc.UInt32x32To64(num5, decimal.DecCalc.s_powers10[num4]);
					}
					else if (num4 > 18)
					{
						decimal.DecCalc.UInt64x64To128(decimal.DecCalc.UInt32x32To64(num5, decimal.DecCalc.s_powers10[num4 - 18]), 1000000000000000000UL, ref result);
					}
					else
					{
						ulong num6 = decimal.DecCalc.UInt32x32To64(num5, decimal.DecCalc.s_powers10[num4 - 9]);
						ulong num7 = decimal.DecCalc.UInt32x32To64(1000000000U, (uint)(num6 >> 32));
						num6 = decimal.DecCalc.UInt32x32To64(1000000000U, (uint)num6);
						result.Low = (uint)num6;
						num7 += num6 >> 32;
						result.Mid = (uint)num7;
						num7 >>= 32;
						result.High = (uint)num7;
					}
				}
				else
				{
					int num8 = num4;
					if (num8 > 6)
					{
						num8 = 6;
					}
					if ((num5 & 15U) == 0U && num8 >= 4)
					{
						uint num9 = num5 / 10000U;
						if (num5 == num9 * 10000U)
						{
							num5 = num9;
							num4 -= 4;
							num8 -= 4;
						}
					}
					if ((num5 & 3U) == 0U && num8 >= 2)
					{
						uint num10 = num5 / 100U;
						if (num5 == num10 * 100U)
						{
							num5 = num10;
							num4 -= 2;
							num8 -= 2;
						}
					}
					if ((num5 & 1U) == 0U && num8 >= 1)
					{
						uint num11 = num5 / 10U;
						if (num5 == num11 * 10U)
						{
							num5 = num11;
							num4--;
						}
					}
					num2 |= (uint)((uint)num4 << 16);
					result.Low = num5;
				}
				result.uflags = num2;
			}

			// Token: 0x06001420 RID: 5152 RVA: 0x00051C68 File Offset: 0x0004FE68
			internal static void VarDecFromR8(double input, out decimal.DecCalc result)
			{
				result = default(decimal.DecCalc);
				int num = (int)(decimal.DecCalc.GetExponent(input) - 1022U);
				if (num < -94)
				{
					return;
				}
				if (num > 96)
				{
					throw new OverflowException("Value was either too large or too small for a Decimal.");
				}
				uint num2 = 0U;
				if (input < 0.0)
				{
					input = -input;
					num2 = 2147483648U;
				}
				double num3 = input;
				int num4 = 14 - (num * 19728 >> 16);
				if (num4 >= 0)
				{
					if (num4 > 28)
					{
						num4 = 28;
					}
					num3 *= decimal.DecCalc.s_doublePowers10[num4];
				}
				else if (num4 != -1 || num3 >= 1000000000000000.0)
				{
					num3 /= decimal.DecCalc.s_doublePowers10[-num4];
				}
				else
				{
					num4 = 0;
				}
				if (num3 < 100000000000000.0 && num4 < 28)
				{
					num3 *= 10.0;
					num4++;
				}
				ulong num5 = (ulong)((long)num3);
				num3 -= (double)num5;
				if (num3 > 0.5 || (num3 == 0.5 && (num5 & 1UL) != 0UL))
				{
					num5 += 1UL;
				}
				if (num5 == 0UL)
				{
					return;
				}
				if (num4 < 0)
				{
					num4 = -num4;
					if (num4 < 10)
					{
						uint num6 = decimal.DecCalc.s_powers10[num4];
						ulong num7 = decimal.DecCalc.UInt32x32To64((uint)num5, num6);
						ulong num8 = decimal.DecCalc.UInt32x32To64((uint)(num5 >> 32), num6);
						result.Low = (uint)num7;
						num8 += num7 >> 32;
						result.Mid = (uint)num8;
						num8 >>= 32;
						result.High = (uint)num8;
					}
					else
					{
						decimal.DecCalc.UInt64x64To128(num5, decimal.DecCalc.s_ulongPowers10[num4 - 1], ref result);
					}
				}
				else
				{
					int num9 = num4;
					if (num9 > 14)
					{
						num9 = 14;
					}
					if ((byte)num5 == 0 && num9 >= 8)
					{
						ulong num10 = num5 / 100000000UL;
						if ((uint)num5 == (uint)(num10 * 100000000UL))
						{
							num5 = num10;
							num4 -= 8;
							num9 -= 8;
						}
					}
					if (((uint)num5 & 15U) == 0U && num9 >= 4)
					{
						ulong num11 = num5 / 10000UL;
						if ((uint)num5 == (uint)(num11 * 10000UL))
						{
							num5 = num11;
							num4 -= 4;
							num9 -= 4;
						}
					}
					if (((uint)num5 & 3U) == 0U && num9 >= 2)
					{
						ulong num12 = num5 / 100UL;
						if ((uint)num5 == (uint)(num12 * 100UL))
						{
							num5 = num12;
							num4 -= 2;
							num9 -= 2;
						}
					}
					if (((uint)num5 & 1U) == 0U && num9 >= 1)
					{
						ulong num13 = num5 / 10UL;
						if ((uint)num5 == (uint)(num13 * 10UL))
						{
							num5 = num13;
							num4--;
						}
					}
					num2 |= (uint)((uint)num4 << 16);
					result.Low64 = num5;
				}
				result.uflags = num2;
			}

			// Token: 0x06001421 RID: 5153 RVA: 0x00051EAB File Offset: 0x000500AB
			internal static float VarR4FromDec(in decimal value)
			{
				return (float)decimal.DecCalc.VarR8FromDec(in value);
			}

			// Token: 0x06001422 RID: 5154 RVA: 0x00051EB4 File Offset: 0x000500B4
			internal static double VarR8FromDec(in decimal value)
			{
				double num = (value.Low64 + value.High * 1.8446744073709552E+19) / decimal.DecCalc.s_doublePowers10[value.Scale];
				if (value.IsNegative)
				{
					num = -num;
				}
				return num;
			}

			// Token: 0x06001423 RID: 5155 RVA: 0x00051EF8 File Offset: 0x000500F8
			internal static int GetHashCode(in decimal d)
			{
				if ((d.Low | d.Mid | d.High) == 0U)
				{
					return 0;
				}
				uint num = (uint)d.flags;
				if ((num & 16711680U) == 0U || (d.Low & 1U) != 0U)
				{
					return (int)(num ^ d.High ^ d.Mid ^ d.Low);
				}
				int num2 = (int)((byte)(num >> 16));
				uint low = d.Low;
				ulong num3 = ((ulong)d.High << 32) | (ulong)d.Mid;
				decimal.DecCalc.Unscale(ref low, ref num3, ref num2);
				num = (num & 4278255615U) | (uint)((uint)num2 << 16);
				return (int)(num ^ (uint)(num3 >> 32) ^ (uint)num3 ^ low);
			}

			// Token: 0x06001424 RID: 5156 RVA: 0x00051F94 File Offset: 0x00050194
			internal unsafe static void VarDecDiv(ref decimal.DecCalc d1, ref decimal.DecCalc d2)
			{
				int num = (int)((sbyte)(d1.uflags - d2.uflags >> 16));
				bool flag = false;
				decimal.DecCalc.Buf12 buf;
				if ((d2.High | d2.Mid) == 0U)
				{
					uint low = d2.Low;
					if (low == 0U)
					{
						throw new DivideByZeroException();
					}
					buf.Low64 = d1.Low64;
					buf.U2 = d1.High;
					uint num2 = decimal.DecCalc.Div96By32(ref buf, low);
					for (;;)
					{
						int num3;
						if (num2 == 0U)
						{
							if (num >= 0)
							{
								goto IL_03D2;
							}
							num3 = Math.Min(9, -num);
						}
						else
						{
							flag = true;
							if (num == 28 || (num3 = decimal.DecCalc.SearchScale(ref buf, num)) == 0)
							{
								break;
							}
						}
						uint num4 = decimal.DecCalc.s_powers10[num3];
						num += num3;
						if (decimal.DecCalc.IncreaseScale(ref buf, num4) != 0U)
						{
							goto IL_048A;
						}
						ulong num5 = decimal.DecCalc.UInt32x32To64(num2, num4);
						uint num6 = (uint)(num5 / (ulong)low);
						num2 = (uint)num5 - num6 * low;
						if (!decimal.DecCalc.Add32To96(ref buf, num6))
						{
							goto Block_11;
						}
					}
					uint num7 = num2 << 1;
					if (num7 < num2)
					{
						goto IL_0449;
					}
					if (num7 < low)
					{
						goto IL_03D2;
					}
					if (num7 > low)
					{
						goto IL_0449;
					}
					if ((buf.U0 & 1U) != 0U)
					{
						goto IL_0449;
					}
					goto IL_03D2;
					Block_11:
					num = decimal.DecCalc.OverflowUnscale(ref buf, num, num2 > 0U);
				}
				else
				{
					uint num7 = d2.High;
					if (num7 == 0U)
					{
						num7 = d2.Mid;
					}
					int num3 = decimal.DecCalc.LeadingZeroCount(num7);
					decimal.DecCalc.Buf16 buf2;
					buf2.Low64 = d1.Low64 << num3;
					buf2.High64 = (ulong)d1.Mid + ((ulong)d1.High << 32) >> 32 - num3;
					ulong num8 = d2.Low64 << num3;
					if (d2.High == 0U)
					{
						buf.U1 = decimal.DecCalc.Div96By64(ref *(decimal.DecCalc.Buf12*)(&buf2.U1), num8);
						buf.U0 = decimal.DecCalc.Div96By64(ref *(decimal.DecCalc.Buf12*)(&buf2), num8);
						for (;;)
						{
							if (buf2.Low64 == 0UL)
							{
								if (num >= 0)
								{
									goto IL_03D2;
								}
								num3 = Math.Min(9, -num);
							}
							else
							{
								flag = true;
								if (num == 28 || (num3 = decimal.DecCalc.SearchScale(ref buf, num)) == 0)
								{
									break;
								}
							}
							uint num4 = decimal.DecCalc.s_powers10[num3];
							num += num3;
							if (decimal.DecCalc.IncreaseScale(ref buf, num4) != 0U)
							{
								goto IL_048A;
							}
							decimal.DecCalc.IncreaseScale64(ref *(decimal.DecCalc.Buf12*)(&buf2), num4);
							num7 = decimal.DecCalc.Div96By64(ref *(decimal.DecCalc.Buf12*)(&buf2), num8);
							if (!decimal.DecCalc.Add32To96(ref buf, num7))
							{
								goto Block_22;
							}
						}
						ulong num9 = buf2.Low64;
						if (num9 < 0UL || (num9 <<= 1) > num8)
						{
							goto IL_0449;
						}
						if (num9 == num8 && (buf.U0 & 1U) != 0U)
						{
							goto IL_0449;
						}
						goto IL_03D2;
						Block_22:
						num = decimal.DecCalc.OverflowUnscale(ref buf, num, buf2.Low64 > 0UL);
					}
					else
					{
						decimal.DecCalc.Buf12 buf3;
						buf3.Low64 = num8;
						buf3.U2 = (uint)((ulong)d2.Mid + ((ulong)d2.High << 32) >> 32 - num3);
						buf.Low64 = (ulong)decimal.DecCalc.Div128By96(ref buf2, ref buf3);
						for (;;)
						{
							if ((buf2.Low64 | (ulong)buf2.U2) == 0UL)
							{
								if (num >= 0)
								{
									goto IL_03D2;
								}
								num3 = Math.Min(9, -num);
							}
							else
							{
								flag = true;
								if (num == 28 || (num3 = decimal.DecCalc.SearchScale(ref buf, num)) == 0)
								{
									break;
								}
							}
							uint num4 = decimal.DecCalc.s_powers10[num3];
							num += num3;
							if (decimal.DecCalc.IncreaseScale(ref buf, num4) != 0U)
							{
								goto IL_048A;
							}
							buf2.U3 = decimal.DecCalc.IncreaseScale(ref *(decimal.DecCalc.Buf12*)(&buf2), num4);
							num7 = decimal.DecCalc.Div128By96(ref buf2, ref buf3);
							if (!decimal.DecCalc.Add32To96(ref buf, num7))
							{
								goto Block_33;
							}
						}
						if (buf2.U2 < 0U)
						{
							goto IL_0449;
						}
						num7 = buf2.U1 >> 31;
						buf2.Low64 <<= 1;
						buf2.U2 = (buf2.U2 << 1) + num7;
						if (buf2.U2 > buf3.U2)
						{
							goto IL_0449;
						}
						if (buf2.U2 != buf3.U2)
						{
							goto IL_03D2;
						}
						if (buf2.Low64 > buf3.Low64)
						{
							goto IL_0449;
						}
						if (buf2.Low64 == buf3.Low64 && (buf.U0 & 1U) != 0U)
						{
							goto IL_0449;
						}
						goto IL_03D2;
						Block_33:
						num = decimal.DecCalc.OverflowUnscale(ref buf, num, (buf2.Low64 | buf2.High64) > 0UL);
					}
				}
				IL_03D2:
				if (flag)
				{
					uint u = buf.U0;
					ulong high = buf.High64;
					decimal.DecCalc.Unscale(ref u, ref high, ref num);
					d1.Low = u;
					d1.Mid = (uint)high;
					d1.High = (uint)(high >> 32);
				}
				else
				{
					d1.Low64 = buf.Low64;
					d1.High = buf.U2;
				}
				d1.uflags = ((d1.uflags ^ d2.uflags) & 2147483648U) | (uint)((uint)num << 16);
				return;
				IL_0449:
				ulong num10 = buf.Low64 + 1UL;
				buf.Low64 = num10;
				if (num10 != 0UL)
				{
					goto IL_03D2;
				}
				uint num11 = buf.U2 + 1U;
				buf.U2 = num11;
				if (num11 == 0U)
				{
					num = decimal.DecCalc.OverflowUnscale(ref buf, num, true);
					goto IL_03D2;
				}
				goto IL_03D2;
				IL_048A:
				throw new OverflowException("Value was either too large or too small for a Decimal.");
			}

			// Token: 0x06001425 RID: 5157 RVA: 0x00052438 File Offset: 0x00050638
			internal static void InternalRound(ref decimal.DecCalc d, uint scale, decimal.DecCalc.RoundingMode mode)
			{
				d.uflags -= scale << 16;
				uint num = 0U;
				uint num5;
				while (scale >= 9U)
				{
					scale -= 9U;
					uint num2 = d.uhi;
					uint num4;
					if (num2 == 0U)
					{
						ulong low = d.Low64;
						ulong num3 = low / 1000000000UL;
						d.Low64 = num3;
						num4 = (uint)(low - num3 * 1000000000UL);
					}
					else
					{
						num4 = num2 - (d.uhi = num2 / 1000000000U) * 1000000000U;
						num2 = d.umid;
						if ((num2 | num4) != 0U)
						{
							num4 = num2 - (d.umid = (uint)((((ulong)num4 << 32) | (ulong)num2) / 1000000000UL)) * 1000000000U;
						}
						num2 = d.ulo;
						if ((num2 | num4) != 0U)
						{
							num4 = num2 - (d.ulo = (uint)((((ulong)num4 << 32) | (ulong)num2) / 1000000000UL)) * 1000000000U;
						}
					}
					num5 = 1000000000U;
					if (scale == 0U)
					{
						IL_0194:
						if (mode != decimal.DecCalc.RoundingMode.Truncate)
						{
							if (mode == decimal.DecCalc.RoundingMode.ToEven)
							{
								num4 <<= 1;
								if ((num | (d.ulo & 1U)) != 0U)
								{
									num4 += 1U;
								}
								if (num5 >= num4)
								{
									return;
								}
							}
							else if (mode == decimal.DecCalc.RoundingMode.AwayFromZero)
							{
								num4 <<= 1;
								if (num5 > num4)
								{
									return;
								}
							}
							else if (mode == decimal.DecCalc.RoundingMode.Floor)
							{
								if ((num4 | num) == 0U)
								{
									return;
								}
								if (!d.IsNegative)
								{
									return;
								}
							}
							else if ((num4 | num) == 0U || d.IsNegative)
							{
								return;
							}
							ulong num6 = d.Low64 + 1UL;
							d.Low64 = num6;
							if (num6 == 0UL)
							{
								d.uhi += 1U;
							}
						}
						return;
					}
					num |= num4;
				}
				num5 = decimal.DecCalc.s_powers10[(int)scale];
				uint num7 = d.uhi;
				if (num7 == 0U)
				{
					ulong low2 = d.Low64;
					if (low2 != 0UL)
					{
						ulong num8 = low2 / (ulong)num5;
						d.Low64 = num8;
						uint num4 = (uint)(low2 - num8 * (ulong)num5);
						goto IL_0194;
					}
					if (mode > decimal.DecCalc.RoundingMode.Truncate)
					{
						uint num4 = 0U;
						goto IL_0194;
					}
					return;
				}
				else
				{
					uint num4 = num7 - (d.uhi = num7 / num5) * num5;
					num7 = d.umid;
					if ((num7 | num4) != 0U)
					{
						num4 = num7 - (d.umid = (uint)((((ulong)num4 << 32) | (ulong)num7) / (ulong)num5)) * num5;
					}
					num7 = d.ulo;
					if ((num7 | num4) != 0U)
					{
						num4 = num7 - (d.ulo = (uint)((((ulong)num4 << 32) | (ulong)num7) / (ulong)num5)) * num5;
						goto IL_0194;
					}
					goto IL_0194;
				}
			}

			// Token: 0x06001426 RID: 5158 RVA: 0x00052648 File Offset: 0x00050848
			internal static uint DecDivMod1E9(ref decimal.DecCalc value)
			{
				ulong num = ((ulong)value.uhi << 32) + (ulong)value.umid;
				ulong num2 = num / 1000000000UL;
				value.uhi = (uint)(num2 >> 32);
				value.umid = (uint)num2;
				ulong num3 = (num - (ulong)((uint)num2 * 1000000000U) << 32) + (ulong)value.ulo;
				uint num4 = (uint)(num3 / 1000000000UL);
				value.ulo = num4;
				return (uint)num3 - num4 * 1000000000U;
			}

			// Token: 0x040009C3 RID: 2499
			[FieldOffset(0)]
			private uint uflags;

			// Token: 0x040009C4 RID: 2500
			[FieldOffset(4)]
			private uint uhi;

			// Token: 0x040009C5 RID: 2501
			[FieldOffset(8)]
			private uint ulo;

			// Token: 0x040009C6 RID: 2502
			[FieldOffset(12)]
			private uint umid;

			// Token: 0x040009C7 RID: 2503
			[FieldOffset(8)]
			private ulong ulomidLE;

			// Token: 0x040009C8 RID: 2504
			private static readonly uint[] s_powers10 = new uint[] { 1U, 10U, 100U, 1000U, 10000U, 100000U, 1000000U, 10000000U, 100000000U, 1000000000U };

			// Token: 0x040009C9 RID: 2505
			private static readonly ulong[] s_ulongPowers10 = new ulong[]
			{
				10UL, 100UL, 1000UL, 10000UL, 100000UL, 1000000UL, 10000000UL, 100000000UL, 1000000000UL, 10000000000UL,
				100000000000UL, 1000000000000UL, 10000000000000UL, 100000000000000UL, 1000000000000000UL, 10000000000000000UL, 100000000000000000UL, 1000000000000000000UL, 10000000000000000000UL
			};

			// Token: 0x040009CA RID: 2506
			private static readonly double[] s_doublePowers10 = new double[]
			{
				1.0, 10.0, 100.0, 1000.0, 10000.0, 100000.0, 1000000.0, 10000000.0, 100000000.0, 1000000000.0,
				10000000000.0, 100000000000.0, 1000000000000.0, 10000000000000.0, 100000000000000.0, 1000000000000000.0, 10000000000000000.0, 1E+17, 1E+18, 1E+19,
				1E+20, 1E+21, 1E+22, 1E+23, 1E+24, 1E+25, 1E+26, 1E+27, 1E+28, 1E+29,
				1E+30, 1E+31, 1E+32, 1E+33, 1E+34, 1E+35, 1E+36, 1E+37, 1E+38, 1E+39,
				1E+40, 1E+41, 1E+42, 1E+43, 1E+44, 1E+45, 1E+46, 1E+47, 1E+48, 1E+49,
				1E+50, 1E+51, 1E+52, 1E+53, 1E+54, 1E+55, 1E+56, 1E+57, 1E+58, 1E+59,
				1E+60, 1E+61, 1E+62, 1E+63, 1E+64, 1E+65, 1E+66, 1E+67, 1E+68, 1E+69,
				1E+70, 1E+71, 1E+72, 1E+73, 1E+74, 1E+75, 1E+76, 1E+77, 1E+78, 1E+79,
				1E+80
			};

			// Token: 0x040009CB RID: 2507
			private static readonly decimal.DecCalc.PowerOvfl[] PowerOvflValues = new decimal.DecCalc.PowerOvfl[]
			{
				new decimal.DecCalc.PowerOvfl(429496729U, 2576980377U, 2576980377U),
				new decimal.DecCalc.PowerOvfl(42949672U, 4123168604U, 687194767U),
				new decimal.DecCalc.PowerOvfl(4294967U, 1271310319U, 2645699854U),
				new decimal.DecCalc.PowerOvfl(429496U, 3133608139U, 694066715U),
				new decimal.DecCalc.PowerOvfl(42949U, 2890341191U, 2216890319U),
				new decimal.DecCalc.PowerOvfl(4294U, 4154504685U, 2369172679U),
				new decimal.DecCalc.PowerOvfl(429U, 2133437386U, 4102387834U),
				new decimal.DecCalc.PowerOvfl(42U, 4078814305U, 410238783U)
			};

			// Token: 0x02000207 RID: 519
			internal enum RoundingMode
			{
				// Token: 0x040009CD RID: 2509
				ToEven,
				// Token: 0x040009CE RID: 2510
				AwayFromZero,
				// Token: 0x040009CF RID: 2511
				Truncate,
				// Token: 0x040009D0 RID: 2512
				Floor,
				// Token: 0x040009D1 RID: 2513
				Ceiling
			}

			// Token: 0x02000208 RID: 520
			private struct PowerOvfl
			{
				// Token: 0x06001428 RID: 5160 RVA: 0x000527E6 File Offset: 0x000509E6
				public PowerOvfl(uint hi, uint mid, uint lo)
				{
					this.Hi = hi;
					this.MidLo = ((ulong)mid << 32) + (ulong)lo;
				}

				// Token: 0x040009D2 RID: 2514
				public readonly uint Hi;

				// Token: 0x040009D3 RID: 2515
				public readonly ulong MidLo;
			}

			// Token: 0x02000209 RID: 521
			[StructLayout(LayoutKind.Explicit)]
			private struct Buf12
			{
				// Token: 0x17000210 RID: 528
				// (get) Token: 0x06001429 RID: 5161 RVA: 0x000527FD File Offset: 0x000509FD
				// (set) Token: 0x0600142A RID: 5162 RVA: 0x0005281F File Offset: 0x00050A1F
				public ulong Low64
				{
					get
					{
						if (!BitConverter.IsLittleEndian)
						{
							return ((ulong)this.U1 << 32) | (ulong)this.U0;
						}
						return this.ulo64LE;
					}
					set
					{
						if (BitConverter.IsLittleEndian)
						{
							this.ulo64LE = value;
							return;
						}
						this.U1 = (uint)(value >> 32);
						this.U0 = (uint)value;
					}
				}

				// Token: 0x17000211 RID: 529
				// (get) Token: 0x0600142B RID: 5163 RVA: 0x00052843 File Offset: 0x00050A43
				// (set) Token: 0x0600142C RID: 5164 RVA: 0x00052865 File Offset: 0x00050A65
				public ulong High64
				{
					get
					{
						if (!BitConverter.IsLittleEndian)
						{
							return ((ulong)this.U2 << 32) | (ulong)this.U1;
						}
						return this.uhigh64LE;
					}
					set
					{
						if (BitConverter.IsLittleEndian)
						{
							this.uhigh64LE = value;
							return;
						}
						this.U2 = (uint)(value >> 32);
						this.U1 = (uint)value;
					}
				}

				// Token: 0x040009D4 RID: 2516
				[FieldOffset(0)]
				public uint U0;

				// Token: 0x040009D5 RID: 2517
				[FieldOffset(4)]
				public uint U1;

				// Token: 0x040009D6 RID: 2518
				[FieldOffset(8)]
				public uint U2;

				// Token: 0x040009D7 RID: 2519
				[FieldOffset(0)]
				private ulong ulo64LE;

				// Token: 0x040009D8 RID: 2520
				[FieldOffset(4)]
				private ulong uhigh64LE;
			}

			// Token: 0x0200020A RID: 522
			[StructLayout(LayoutKind.Explicit)]
			private struct Buf16
			{
				// Token: 0x17000212 RID: 530
				// (get) Token: 0x0600142D RID: 5165 RVA: 0x00052889 File Offset: 0x00050A89
				// (set) Token: 0x0600142E RID: 5166 RVA: 0x000528AB File Offset: 0x00050AAB
				public ulong Low64
				{
					get
					{
						if (!BitConverter.IsLittleEndian)
						{
							return ((ulong)this.U1 << 32) | (ulong)this.U0;
						}
						return this.ulo64LE;
					}
					set
					{
						if (BitConverter.IsLittleEndian)
						{
							this.ulo64LE = value;
							return;
						}
						this.U1 = (uint)(value >> 32);
						this.U0 = (uint)value;
					}
				}

				// Token: 0x17000213 RID: 531
				// (get) Token: 0x0600142F RID: 5167 RVA: 0x000528CF File Offset: 0x00050ACF
				// (set) Token: 0x06001430 RID: 5168 RVA: 0x000528F1 File Offset: 0x00050AF1
				public ulong High64
				{
					get
					{
						if (!BitConverter.IsLittleEndian)
						{
							return ((ulong)this.U3 << 32) | (ulong)this.U2;
						}
						return this.uhigh64LE;
					}
					set
					{
						if (BitConverter.IsLittleEndian)
						{
							this.uhigh64LE = value;
							return;
						}
						this.U3 = (uint)(value >> 32);
						this.U2 = (uint)value;
					}
				}

				// Token: 0x040009D9 RID: 2521
				[FieldOffset(0)]
				public uint U0;

				// Token: 0x040009DA RID: 2522
				[FieldOffset(4)]
				public uint U1;

				// Token: 0x040009DB RID: 2523
				[FieldOffset(8)]
				public uint U2;

				// Token: 0x040009DC RID: 2524
				[FieldOffset(12)]
				public uint U3;

				// Token: 0x040009DD RID: 2525
				[FieldOffset(0)]
				private ulong ulo64LE;

				// Token: 0x040009DE RID: 2526
				[FieldOffset(8)]
				private ulong uhigh64LE;
			}

			// Token: 0x0200020B RID: 523
			[StructLayout(LayoutKind.Explicit)]
			private struct Buf24
			{
				// Token: 0x17000214 RID: 532
				// (get) Token: 0x06001431 RID: 5169 RVA: 0x00052915 File Offset: 0x00050B15
				// (set) Token: 0x06001432 RID: 5170 RVA: 0x00052937 File Offset: 0x00050B37
				public ulong Low64
				{
					get
					{
						if (!BitConverter.IsLittleEndian)
						{
							return ((ulong)this.U1 << 32) | (ulong)this.U0;
						}
						return this.ulo64LE;
					}
					set
					{
						if (BitConverter.IsLittleEndian)
						{
							this.ulo64LE = value;
							return;
						}
						this.U1 = (uint)(value >> 32);
						this.U0 = (uint)value;
					}
				}

				// Token: 0x17000215 RID: 533
				// (set) Token: 0x06001433 RID: 5171 RVA: 0x0005295B File Offset: 0x00050B5B
				public ulong Mid64
				{
					set
					{
						if (BitConverter.IsLittleEndian)
						{
							this.umid64LE = value;
							return;
						}
						this.U3 = (uint)(value >> 32);
						this.U2 = (uint)value;
					}
				}

				// Token: 0x17000216 RID: 534
				// (set) Token: 0x06001434 RID: 5172 RVA: 0x0005297F File Offset: 0x00050B7F
				public ulong High64
				{
					set
					{
						if (BitConverter.IsLittleEndian)
						{
							this.uhigh64LE = value;
							return;
						}
						this.U5 = (uint)(value >> 32);
						this.U4 = (uint)value;
					}
				}

				// Token: 0x040009DF RID: 2527
				[FieldOffset(0)]
				public uint U0;

				// Token: 0x040009E0 RID: 2528
				[FieldOffset(4)]
				public uint U1;

				// Token: 0x040009E1 RID: 2529
				[FieldOffset(8)]
				public uint U2;

				// Token: 0x040009E2 RID: 2530
				[FieldOffset(12)]
				public uint U3;

				// Token: 0x040009E3 RID: 2531
				[FieldOffset(16)]
				public uint U4;

				// Token: 0x040009E4 RID: 2532
				[FieldOffset(20)]
				public uint U5;

				// Token: 0x040009E5 RID: 2533
				[FieldOffset(0)]
				private ulong ulo64LE;

				// Token: 0x040009E6 RID: 2534
				[FieldOffset(8)]
				private ulong umid64LE;

				// Token: 0x040009E7 RID: 2535
				[FieldOffset(16)]
				private ulong uhigh64LE;
			}
		}
	}
}
