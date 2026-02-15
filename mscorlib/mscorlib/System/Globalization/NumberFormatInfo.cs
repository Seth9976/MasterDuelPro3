using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Threading;

namespace System.Globalization
{
	/// <summary>Provides culture-specific information for formatting and parsing numeric values. </summary>
	// Token: 0x020006B9 RID: 1721
	[ComVisible(true)]
	[Serializable]
	public sealed class NumberFormatInfo : ICloneable, IFormatProvider
	{
		/// <summary>Initializes a new writable instance of the <see cref="T:System.Globalization.NumberFormatInfo" /> class that is culture-independent (invariant).</summary>
		// Token: 0x06003602 RID: 13826 RVA: 0x000D0B7F File Offset: 0x000CED7F
		public NumberFormatInfo()
			: this(null)
		{
		}

		// Token: 0x06003603 RID: 13827 RVA: 0x000D0B88 File Offset: 0x000CED88
		[OnSerializing]
		private void OnSerializing(StreamingContext ctx)
		{
			if (this.numberDecimalSeparator != this.numberGroupSeparator)
			{
				this.validForParseAsNumber = true;
			}
			else
			{
				this.validForParseAsNumber = false;
			}
			if (this.numberDecimalSeparator != this.numberGroupSeparator && this.numberDecimalSeparator != this.currencyGroupSeparator && this.currencyDecimalSeparator != this.numberGroupSeparator && this.currencyDecimalSeparator != this.currencyGroupSeparator)
			{
				this.validForParseAsCurrency = true;
				return;
			}
			this.validForParseAsCurrency = false;
		}

		// Token: 0x06003604 RID: 13828 RVA: 0x00002C89 File Offset: 0x00000E89
		[OnDeserializing]
		private void OnDeserializing(StreamingContext ctx)
		{
		}

		// Token: 0x06003605 RID: 13829 RVA: 0x00002C89 File Offset: 0x00000E89
		[OnDeserialized]
		private void OnDeserialized(StreamingContext ctx)
		{
		}

		// Token: 0x06003606 RID: 13830 RVA: 0x000D0C14 File Offset: 0x000CEE14
		internal NumberFormatInfo(CultureData cultureData)
		{
			if (GlobalizationMode.Invariant)
			{
				this.m_isInvariant = true;
				return;
			}
			if (cultureData != null)
			{
				cultureData.GetNFIValues(this);
				if (cultureData.IsInvariantCulture)
				{
					this.m_isInvariant = true;
				}
			}
		}

		// Token: 0x06003607 RID: 13831 RVA: 0x000D0DA8 File Offset: 0x000CEFA8
		private void VerifyWritable()
		{
			if (this.isReadOnly)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Instance is read-only."));
			}
		}

		/// <summary>Gets a read-only <see cref="T:System.Globalization.NumberFormatInfo" /> object that is culture-independent (invariant).</summary>
		/// <returns>A read-only  object that is culture-independent (invariant).</returns>
		// Token: 0x1700080F RID: 2063
		// (get) Token: 0x06003608 RID: 13832 RVA: 0x000D0DC2 File Offset: 0x000CEFC2
		public static NumberFormatInfo InvariantInfo
		{
			get
			{
				if (NumberFormatInfo.invariantInfo == null)
				{
					NumberFormatInfo.invariantInfo = NumberFormatInfo.ReadOnly(new NumberFormatInfo
					{
						m_isInvariant = true
					});
				}
				return NumberFormatInfo.invariantInfo;
			}
		}

		/// <summary>Gets the <see cref="T:System.Globalization.NumberFormatInfo" /> associated with the specified <see cref="T:System.IFormatProvider" />.</summary>
		/// <returns>The <see cref="T:System.Globalization.NumberFormatInfo" /> associated with the specified <see cref="T:System.IFormatProvider" />.</returns>
		/// <param name="formatProvider">The <see cref="T:System.IFormatProvider" /> used to get the <see cref="T:System.Globalization.NumberFormatInfo" />.-or- null to get <see cref="P:System.Globalization.NumberFormatInfo.CurrentInfo" />. </param>
		// Token: 0x06003609 RID: 13833 RVA: 0x000D0DEC File Offset: 0x000CEFEC
		public static NumberFormatInfo GetInstance(IFormatProvider formatProvider)
		{
			CultureInfo cultureInfo = formatProvider as CultureInfo;
			if (cultureInfo != null && !cultureInfo.m_isInherited)
			{
				NumberFormatInfo numberFormatInfo = cultureInfo.numInfo;
				if (numberFormatInfo != null)
				{
					return numberFormatInfo;
				}
				return cultureInfo.NumberFormat;
			}
			else
			{
				NumberFormatInfo numberFormatInfo = formatProvider as NumberFormatInfo;
				if (numberFormatInfo != null)
				{
					return numberFormatInfo;
				}
				if (formatProvider != null)
				{
					numberFormatInfo = formatProvider.GetFormat(typeof(NumberFormatInfo)) as NumberFormatInfo;
					if (numberFormatInfo != null)
					{
						return numberFormatInfo;
					}
				}
				return NumberFormatInfo.CurrentInfo;
			}
		}

		/// <summary>Creates a shallow copy of the <see cref="T:System.Globalization.NumberFormatInfo" /> object.</summary>
		/// <returns>A new object copied from the original <see cref="T:System.Globalization.NumberFormatInfo" /> object.</returns>
		// Token: 0x0600360A RID: 13834 RVA: 0x000D0E4F File Offset: 0x000CF04F
		public object Clone()
		{
			NumberFormatInfo numberFormatInfo = (NumberFormatInfo)base.MemberwiseClone();
			numberFormatInfo.isReadOnly = false;
			return numberFormatInfo;
		}

		/// <summary>Gets or sets the number of decimal places to use in currency values.</summary>
		/// <returns>The number of decimal places to use in currency values. The default for <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is 2.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The property is being set to a value that is less than 0 or greater than 99. </exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> object is read-only. </exception>
		// Token: 0x17000810 RID: 2064
		// (get) Token: 0x0600360B RID: 13835 RVA: 0x000D0E63 File Offset: 0x000CF063
		public int CurrencyDecimalDigits
		{
			get
			{
				return this.currencyDecimalDigits;
			}
		}

		/// <summary>Gets or sets the string to use as the decimal separator in currency values.</summary>
		/// <returns>The string to use as the decimal separator in currency values. The default for <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is ".".</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is being set to null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> object is read-only. </exception>
		/// <exception cref="T:System.ArgumentException">The property is being set to an empty string.</exception>
		// Token: 0x17000811 RID: 2065
		// (get) Token: 0x0600360C RID: 13836 RVA: 0x000D0E6B File Offset: 0x000CF06B
		public string CurrencyDecimalSeparator
		{
			get
			{
				return this.currencyDecimalSeparator;
			}
		}

		/// <summary>Gets a value that indicates whether this <see cref="T:System.Globalization.NumberFormatInfo" /> object is read-only.</summary>
		/// <returns>true if the <see cref="T:System.Globalization.NumberFormatInfo" /> is read-only; otherwise, false.</returns>
		// Token: 0x17000812 RID: 2066
		// (get) Token: 0x0600360D RID: 13837 RVA: 0x000D0E73 File Offset: 0x000CF073
		public bool IsReadOnly
		{
			get
			{
				return this.isReadOnly;
			}
		}

		/// <summary>Gets or sets the number of digits in each group to the left of the decimal in currency values.</summary>
		/// <returns>The number of digits in each group to the left of the decimal in currency values. The default for <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is a one-dimensional array with only one element, which is set to 3.</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is being set to null. </exception>
		/// <exception cref="T:System.ArgumentException">The property is being set and the array contains an entry that is less than 0 or greater than 9.-or- The property is being set and the array contains an entry, other than the last entry, that is set to 0. </exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> object is read-only. </exception>
		// Token: 0x17000813 RID: 2067
		// (get) Token: 0x0600360E RID: 13838 RVA: 0x000D0E7B File Offset: 0x000CF07B
		public int[] CurrencyGroupSizes
		{
			get
			{
				return (int[])this.currencyGroupSizes.Clone();
			}
		}

		/// <summary>Gets or sets the number of digits in each group to the left of the decimal in numeric values.</summary>
		/// <returns>The number of digits in each group to the left of the decimal in numeric values. The default for <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is a one-dimensional array with only one element, which is set to 3.</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is being set to null. </exception>
		/// <exception cref="T:System.ArgumentException">The property is being set and the array contains an entry that is less than 0 or greater than 9.-or- The property is being set and the array contains an entry, other than the last entry, that is set to 0. </exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> object is read-only. </exception>
		// Token: 0x17000814 RID: 2068
		// (get) Token: 0x0600360F RID: 13839 RVA: 0x000D0E8D File Offset: 0x000CF08D
		public int[] NumberGroupSizes
		{
			get
			{
				return (int[])this.numberGroupSizes.Clone();
			}
		}

		/// <summary>Gets or sets the number of digits in each group to the left of the decimal in percent values. </summary>
		/// <returns>The number of digits in each group to the left of the decimal in percent values. The default for <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is a one-dimensional array with only one element, which is set to 3.</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is being set to null. </exception>
		/// <exception cref="T:System.ArgumentException">The property is being set and the array contains an entry that is less than 0 or greater than 9.-or- The property is being set and the array contains an entry, other than the last entry, that is set to 0. </exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> object is read-only. </exception>
		// Token: 0x17000815 RID: 2069
		// (get) Token: 0x06003610 RID: 13840 RVA: 0x000D0E9F File Offset: 0x000CF09F
		public int[] PercentGroupSizes
		{
			get
			{
				return (int[])this.percentGroupSizes.Clone();
			}
		}

		/// <summary>Gets or sets the string that separates groups of digits to the left of the decimal in currency values.</summary>
		/// <returns>The string that separates groups of digits to the left of the decimal in currency values. The default for <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is ",".</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is being set to null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> object is read-only. </exception>
		// Token: 0x17000816 RID: 2070
		// (get) Token: 0x06003611 RID: 13841 RVA: 0x000D0EB1 File Offset: 0x000CF0B1
		public string CurrencyGroupSeparator
		{
			get
			{
				return this.currencyGroupSeparator;
			}
		}

		/// <summary>Gets or sets the string to use as the currency symbol.</summary>
		/// <returns>The string to use as the currency symbol. The default for <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is "¤". </returns>
		/// <exception cref="T:System.ArgumentNullException">The property is being set to null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> object is read-only. </exception>
		// Token: 0x17000817 RID: 2071
		// (get) Token: 0x06003612 RID: 13842 RVA: 0x000D0EB9 File Offset: 0x000CF0B9
		public string CurrencySymbol
		{
			get
			{
				return this.currencySymbol;
			}
		}

		/// <summary>Gets a read-only <see cref="T:System.Globalization.NumberFormatInfo" /> that formats values based on the current culture.</summary>
		/// <returns>A read-only <see cref="T:System.Globalization.NumberFormatInfo" /> based on the culture of the current thread.</returns>
		// Token: 0x17000818 RID: 2072
		// (get) Token: 0x06003613 RID: 13843 RVA: 0x000D0EC4 File Offset: 0x000CF0C4
		public static NumberFormatInfo CurrentInfo
		{
			get
			{
				CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
				if (!currentCulture.m_isInherited)
				{
					NumberFormatInfo numInfo = currentCulture.numInfo;
					if (numInfo != null)
					{
						return numInfo;
					}
				}
				return (NumberFormatInfo)currentCulture.GetFormat(typeof(NumberFormatInfo));
			}
		}

		/// <summary>Gets or sets the string that represents the IEEE NaN (not a number) value.</summary>
		/// <returns>The string that represents the IEEE NaN (not a number) value. The default for <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is "NaN".</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is being set to null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> object is read-only. </exception>
		// Token: 0x17000819 RID: 2073
		// (get) Token: 0x06003614 RID: 13844 RVA: 0x000D0F07 File Offset: 0x000CF107
		// (set) Token: 0x06003615 RID: 13845 RVA: 0x000D0F0F File Offset: 0x000CF10F
		public string NaNSymbol
		{
			get
			{
				return this.nanSymbol;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("NaNSymbol", Environment.GetResourceString("String reference not set to an instance of a String."));
				}
				this.VerifyWritable();
				this.nanSymbol = value;
			}
		}

		/// <summary>Gets or sets the format pattern for negative currency values.</summary>
		/// <returns>The format pattern for negative currency values. The default for <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is 0, which represents "($n)", where "$" is the <see cref="P:System.Globalization.NumberFormatInfo.CurrencySymbol" /> and <paramref name="n" /> is a number.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The property is being set to a value that is less than 0 or greater than 15. </exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> object is read-only. </exception>
		// Token: 0x1700081A RID: 2074
		// (get) Token: 0x06003616 RID: 13846 RVA: 0x000D0F36 File Offset: 0x000CF136
		public int CurrencyNegativePattern
		{
			get
			{
				return this.currencyNegativePattern;
			}
		}

		/// <summary>Gets or sets the format pattern for negative numeric values.</summary>
		/// <returns>The format pattern for negative numeric values. The default for <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is 1, which represents "-n", where <paramref name="n" /> is a number.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The property is being set to a value that is less than 0 or greater than 4. </exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> object is read-only. </exception>
		// Token: 0x1700081B RID: 2075
		// (get) Token: 0x06003617 RID: 13847 RVA: 0x000D0F3E File Offset: 0x000CF13E
		public int NumberNegativePattern
		{
			get
			{
				return this.numberNegativePattern;
			}
		}

		/// <summary>Gets or sets the format pattern for positive percent values.</summary>
		/// <returns>The format pattern for positive percent values. The default for <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is 0, which represents "n %", where "%" is the <see cref="P:System.Globalization.NumberFormatInfo.PercentSymbol" /> and <paramref name="n" /> is a number.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The property is being set to a value that is less than 0 or greater than 3. </exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> object is read-only. </exception>
		// Token: 0x1700081C RID: 2076
		// (get) Token: 0x06003618 RID: 13848 RVA: 0x000D0F46 File Offset: 0x000CF146
		public int PercentPositivePattern
		{
			get
			{
				return this.percentPositivePattern;
			}
		}

		/// <summary>Gets or sets the format pattern for negative percent values.</summary>
		/// <returns>The format pattern for negative percent values. The default for <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is 0, which represents "-n %", where "%" is the <see cref="P:System.Globalization.NumberFormatInfo.PercentSymbol" /> and <paramref name="n" /> is a number.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The property is being set to a value that is less than 0 or greater than 11. </exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> object is read-only. </exception>
		// Token: 0x1700081D RID: 2077
		// (get) Token: 0x06003619 RID: 13849 RVA: 0x000D0F4E File Offset: 0x000CF14E
		public int PercentNegativePattern
		{
			get
			{
				return this.percentNegativePattern;
			}
		}

		/// <summary>Gets or sets the string that represents negative infinity.</summary>
		/// <returns>The string that represents negative infinity. The default for <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is "-Infinity".</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is being set to null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> object is read-only. </exception>
		// Token: 0x1700081E RID: 2078
		// (get) Token: 0x0600361A RID: 13850 RVA: 0x000D0F56 File Offset: 0x000CF156
		public string NegativeInfinitySymbol
		{
			get
			{
				return this.negativeInfinitySymbol;
			}
		}

		/// <summary>Gets or sets the string that denotes that the associated number is negative.</summary>
		/// <returns>The string that denotes that the associated number is negative. The default for <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is "-".</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is being set to null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> object is read-only. </exception>
		// Token: 0x1700081F RID: 2079
		// (get) Token: 0x0600361B RID: 13851 RVA: 0x000D0F5E File Offset: 0x000CF15E
		public string NegativeSign
		{
			get
			{
				return this.negativeSign;
			}
		}

		/// <summary>Gets or sets the number of decimal places to use in numeric values.</summary>
		/// <returns>The number of decimal places to use in numeric values. The default for <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is 2.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The property is being set to a value that is less than 0 or greater than 99. </exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> object is read-only. </exception>
		// Token: 0x17000820 RID: 2080
		// (get) Token: 0x0600361C RID: 13852 RVA: 0x000D0F66 File Offset: 0x000CF166
		public int NumberDecimalDigits
		{
			get
			{
				return this.numberDecimalDigits;
			}
		}

		/// <summary>Gets or sets the string to use as the decimal separator in numeric values.</summary>
		/// <returns>The string to use as the decimal separator in numeric values. The default for <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is ".".</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is being set to null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> object is read-only. </exception>
		/// <exception cref="T:System.ArgumentException">The property is being set to an empty string.</exception>
		// Token: 0x17000821 RID: 2081
		// (get) Token: 0x0600361D RID: 13853 RVA: 0x000D0F6E File Offset: 0x000CF16E
		public string NumberDecimalSeparator
		{
			get
			{
				return this.numberDecimalSeparator;
			}
		}

		/// <summary>Gets or sets the string that separates groups of digits to the left of the decimal in numeric values.</summary>
		/// <returns>The string that separates groups of digits to the left of the decimal in numeric values. The default for <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is ",".</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is being set to null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> object is read-only. </exception>
		// Token: 0x17000822 RID: 2082
		// (get) Token: 0x0600361E RID: 13854 RVA: 0x000D0F76 File Offset: 0x000CF176
		public string NumberGroupSeparator
		{
			get
			{
				return this.numberGroupSeparator;
			}
		}

		/// <summary>Gets or sets the format pattern for positive currency values.</summary>
		/// <returns>The format pattern for positive currency values. The default for <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is 0, which represents "$n", where "$" is the <see cref="P:System.Globalization.NumberFormatInfo.CurrencySymbol" /> and <paramref name="n" /> is a number.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The property is being set to a value that is less than 0 or greater than 3. </exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> object is read-only. </exception>
		// Token: 0x17000823 RID: 2083
		// (get) Token: 0x0600361F RID: 13855 RVA: 0x000D0F7E File Offset: 0x000CF17E
		public int CurrencyPositivePattern
		{
			get
			{
				return this.currencyPositivePattern;
			}
		}

		/// <summary>Gets or sets the string that represents positive infinity.</summary>
		/// <returns>The string that represents positive infinity. The default for <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is "Infinity".</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is being set to null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> object is read-only. </exception>
		// Token: 0x17000824 RID: 2084
		// (get) Token: 0x06003620 RID: 13856 RVA: 0x000D0F86 File Offset: 0x000CF186
		public string PositiveInfinitySymbol
		{
			get
			{
				return this.positiveInfinitySymbol;
			}
		}

		/// <summary>Gets or sets the string that denotes that the associated number is positive.</summary>
		/// <returns>The string that denotes that the associated number is positive. The default for <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is "+".</returns>
		/// <exception cref="T:System.ArgumentNullException">In a set operation, the value to be assigned is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> object is read-only. </exception>
		// Token: 0x17000825 RID: 2085
		// (get) Token: 0x06003621 RID: 13857 RVA: 0x000D0F8E File Offset: 0x000CF18E
		public string PositiveSign
		{
			get
			{
				return this.positiveSign;
			}
		}

		/// <summary>Gets or sets the number of decimal places to use in percent values. </summary>
		/// <returns>The number of decimal places to use in percent values. The default for <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is 2.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The property is being set to a value that is less than 0 or greater than 99. </exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> object is read-only. </exception>
		// Token: 0x17000826 RID: 2086
		// (get) Token: 0x06003622 RID: 13858 RVA: 0x000D0F96 File Offset: 0x000CF196
		public int PercentDecimalDigits
		{
			get
			{
				return this.percentDecimalDigits;
			}
		}

		/// <summary>Gets or sets the string to use as the decimal separator in percent values. </summary>
		/// <returns>The string to use as the decimal separator in percent values. The default for <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is ".".</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is being set to null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> object is read-only. </exception>
		/// <exception cref="T:System.ArgumentException">The property is being set to an empty string.</exception>
		// Token: 0x17000827 RID: 2087
		// (get) Token: 0x06003623 RID: 13859 RVA: 0x000D0F9E File Offset: 0x000CF19E
		public string PercentDecimalSeparator
		{
			get
			{
				return this.percentDecimalSeparator;
			}
		}

		/// <summary>Gets or sets the string that separates groups of digits to the left of the decimal in percent values. </summary>
		/// <returns>The string that separates groups of digits to the left of the decimal in percent values. The default for <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is ",".</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is being set to null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> object is read-only. </exception>
		// Token: 0x17000828 RID: 2088
		// (get) Token: 0x06003624 RID: 13860 RVA: 0x000D0FA6 File Offset: 0x000CF1A6
		public string PercentGroupSeparator
		{
			get
			{
				return this.percentGroupSeparator;
			}
		}

		/// <summary>Gets or sets the string to use as the percent symbol.</summary>
		/// <returns>The string to use as the percent symbol. The default for <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is "%".</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is being set to null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> object is read-only. </exception>
		// Token: 0x17000829 RID: 2089
		// (get) Token: 0x06003625 RID: 13861 RVA: 0x000D0FAE File Offset: 0x000CF1AE
		public string PercentSymbol
		{
			get
			{
				return this.percentSymbol;
			}
		}

		/// <summary>Gets or sets the string to use as the per mille symbol.</summary>
		/// <returns>The string to use as the per mille symbol. The default for <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is "‰", which is the Unicode character U+2030.</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is being set to null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> object is read-only. </exception>
		// Token: 0x1700082A RID: 2090
		// (get) Token: 0x06003626 RID: 13862 RVA: 0x000D0FB6 File Offset: 0x000CF1B6
		public string PerMilleSymbol
		{
			get
			{
				return this.perMilleSymbol;
			}
		}

		/// <summary>Gets an object of the specified type that provides a number formatting service.</summary>
		/// <returns>The current <see cref="T:System.Globalization.NumberFormatInfo" />, if <paramref name="formatType" /> is the same as the type of the current <see cref="T:System.Globalization.NumberFormatInfo" />; otherwise, null.</returns>
		/// <param name="formatType">The <see cref="T:System.Type" /> of the required formatting service. </param>
		// Token: 0x06003627 RID: 13863 RVA: 0x000D0FBE File Offset: 0x000CF1BE
		public object GetFormat(Type formatType)
		{
			if (!(formatType == typeof(NumberFormatInfo)))
			{
				return null;
			}
			return this;
		}

		/// <summary>Returns a read-only <see cref="T:System.Globalization.NumberFormatInfo" /> wrapper.</summary>
		/// <returns>A read-only <see cref="T:System.Globalization.NumberFormatInfo" /> wrapper around <paramref name="nfi" />.</returns>
		/// <param name="nfi">The <see cref="T:System.Globalization.NumberFormatInfo" /> to wrap. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="nfi" /> is null. </exception>
		// Token: 0x06003628 RID: 13864 RVA: 0x000D0FD5 File Offset: 0x000CF1D5
		public static NumberFormatInfo ReadOnly(NumberFormatInfo nfi)
		{
			if (nfi == null)
			{
				throw new ArgumentNullException("nfi");
			}
			if (nfi.IsReadOnly)
			{
				return nfi;
			}
			NumberFormatInfo numberFormatInfo = (NumberFormatInfo)nfi.MemberwiseClone();
			numberFormatInfo.isReadOnly = true;
			return numberFormatInfo;
		}

		// Token: 0x06003629 RID: 13865 RVA: 0x000D1004 File Offset: 0x000CF204
		internal static void ValidateParseStyleInteger(NumberStyles style)
		{
			if ((style & ~(NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowTrailingSign | NumberStyles.AllowParentheses | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent | NumberStyles.AllowCurrencySymbol | NumberStyles.AllowHexSpecifier)) != NumberStyles.None)
			{
				throw new ArgumentException(Environment.GetResourceString("An undefined NumberStyles value is being used."), "style");
			}
			if ((style & NumberStyles.AllowHexSpecifier) != NumberStyles.None && (style & ~(NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowHexSpecifier)) != NumberStyles.None)
			{
				throw new ArgumentException(Environment.GetResourceString("With the AllowHexSpecifier bit set in the enum bit field, the only other valid bits that can be combined into the enum value must be a subset of those in HexNumber."));
			}
		}

		// Token: 0x0600362A RID: 13866 RVA: 0x000D1051 File Offset: 0x000CF251
		internal static void ValidateParseStyleFloatingPoint(NumberStyles style)
		{
			if ((style & ~(NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowTrailingSign | NumberStyles.AllowParentheses | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent | NumberStyles.AllowCurrencySymbol | NumberStyles.AllowHexSpecifier)) != NumberStyles.None)
			{
				throw new ArgumentException(Environment.GetResourceString("An undefined NumberStyles value is being used."), "style");
			}
			if ((style & NumberStyles.AllowHexSpecifier) != NumberStyles.None)
			{
				throw new ArgumentException(Environment.GetResourceString("The number style AllowHexSpecifier is not supported on floating point data types."));
			}
		}

		// Token: 0x04001D0F RID: 7439
		private static volatile NumberFormatInfo invariantInfo;

		// Token: 0x04001D10 RID: 7440
		internal int[] numberGroupSizes = new int[] { 3 };

		// Token: 0x04001D11 RID: 7441
		internal int[] currencyGroupSizes = new int[] { 3 };

		// Token: 0x04001D12 RID: 7442
		internal int[] percentGroupSizes = new int[] { 3 };

		// Token: 0x04001D13 RID: 7443
		internal string positiveSign = "+";

		// Token: 0x04001D14 RID: 7444
		internal string negativeSign = "-";

		// Token: 0x04001D15 RID: 7445
		internal string numberDecimalSeparator = ".";

		// Token: 0x04001D16 RID: 7446
		internal string numberGroupSeparator = ",";

		// Token: 0x04001D17 RID: 7447
		internal string currencyGroupSeparator = ",";

		// Token: 0x04001D18 RID: 7448
		internal string currencyDecimalSeparator = ".";

		// Token: 0x04001D19 RID: 7449
		internal string currencySymbol = "¤";

		// Token: 0x04001D1A RID: 7450
		internal string ansiCurrencySymbol;

		// Token: 0x04001D1B RID: 7451
		internal string nanSymbol = "NaN";

		// Token: 0x04001D1C RID: 7452
		internal string positiveInfinitySymbol = "Infinity";

		// Token: 0x04001D1D RID: 7453
		internal string negativeInfinitySymbol = "-Infinity";

		// Token: 0x04001D1E RID: 7454
		internal string percentDecimalSeparator = ".";

		// Token: 0x04001D1F RID: 7455
		internal string percentGroupSeparator = ",";

		// Token: 0x04001D20 RID: 7456
		internal string percentSymbol = "%";

		// Token: 0x04001D21 RID: 7457
		internal string perMilleSymbol = "‰";

		// Token: 0x04001D22 RID: 7458
		[OptionalField(VersionAdded = 2)]
		internal string[] nativeDigits = new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };

		// Token: 0x04001D23 RID: 7459
		[OptionalField(VersionAdded = 1)]
		internal int m_dataItem;

		// Token: 0x04001D24 RID: 7460
		internal int numberDecimalDigits = 2;

		// Token: 0x04001D25 RID: 7461
		internal int currencyDecimalDigits = 2;

		// Token: 0x04001D26 RID: 7462
		internal int currencyPositivePattern;

		// Token: 0x04001D27 RID: 7463
		internal int currencyNegativePattern;

		// Token: 0x04001D28 RID: 7464
		internal int numberNegativePattern = 1;

		// Token: 0x04001D29 RID: 7465
		internal int percentPositivePattern;

		// Token: 0x04001D2A RID: 7466
		internal int percentNegativePattern;

		// Token: 0x04001D2B RID: 7467
		internal int percentDecimalDigits = 2;

		// Token: 0x04001D2C RID: 7468
		[OptionalField(VersionAdded = 2)]
		internal int digitSubstitution = 1;

		// Token: 0x04001D2D RID: 7469
		internal bool isReadOnly;

		// Token: 0x04001D2E RID: 7470
		[OptionalField(VersionAdded = 1)]
		internal bool m_useUserOverride;

		// Token: 0x04001D2F RID: 7471
		[OptionalField(VersionAdded = 2)]
		internal bool m_isInvariant;

		// Token: 0x04001D30 RID: 7472
		[OptionalField(VersionAdded = 1)]
		internal bool validForParseAsNumber = true;

		// Token: 0x04001D31 RID: 7473
		[OptionalField(VersionAdded = 1)]
		internal bool validForParseAsCurrency = true;

		// Token: 0x04001D32 RID: 7474
		private const NumberStyles InvalidNumberStyles = ~(NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowTrailingSign | NumberStyles.AllowParentheses | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent | NumberStyles.AllowCurrencySymbol | NumberStyles.AllowHexSpecifier);
	}
}
