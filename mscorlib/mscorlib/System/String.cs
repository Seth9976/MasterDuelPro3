using System;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Text;

namespace System
{
	/// <summary>Represents text as a series of Unicode characters.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000098 RID: 152
	[Serializable]
	public sealed class String : IComparable, IEnumerable, IEnumerable<char>, IComparable<string>, IEquatable<string>, IConvertible, ICloneable
	{
		// Token: 0x060002D3 RID: 723 RVA: 0x00010A38 File Offset: 0x0000EC38
		private unsafe static int CompareOrdinalIgnoreCaseHelper(string strA, string strB)
		{
			int num = Math.Min(strA.Length, strB.Length);
			fixed (char* ptr = &strA._firstChar)
			{
				char* ptr2 = ptr;
				fixed (char* ptr3 = &strB._firstChar)
				{
					char* ptr4 = ptr3;
					char* ptr5 = ptr2;
					char* ptr6 = ptr4;
					while (num != 0)
					{
						int num2 = (int)(*ptr5);
						int num3 = (int)(*ptr6);
						if (num2 - 97 <= 25)
						{
							num2 -= 32;
						}
						if (num3 - 97 <= 25)
						{
							num3 -= 32;
						}
						if (num2 != num3)
						{
							return num2 - num3;
						}
						ptr5++;
						ptr6++;
						num--;
					}
					return strA.Length - strB.Length;
				}
			}
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x00010ACD File Offset: 0x0000ECCD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static bool EqualsHelper(string strA, string strB)
		{
			return SpanHelpers.SequenceEqual(Unsafe.As<char, byte>(strA.GetRawStringData()), Unsafe.As<char, byte>(strB.GetRawStringData()), (ulong)((long)strA.Length * 2L));
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x00010AF4 File Offset: 0x0000ECF4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static int CompareOrdinalHelper(string strA, int indexA, int countA, string strB, int indexB, int countB)
		{
			return SpanHelpers.SequenceCompareTo(Unsafe.Add<char>(strA.GetRawStringData(), indexA), countA, Unsafe.Add<char>(strB.GetRawStringData(), indexB), countB);
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x00010B18 File Offset: 0x0000ED18
		private unsafe static bool EqualsIgnoreCaseAsciiHelper(string strA, string strB)
		{
			int num = strA.Length;
			fixed (char* ptr = &strA._firstChar)
			{
				char* ptr2 = ptr;
				fixed (char* ptr3 = &strB._firstChar)
				{
					char* ptr4 = ptr3;
					char* ptr5 = ptr2;
					char* ptr6 = ptr4;
					while (num != 0)
					{
						int num2 = (int)(*ptr5);
						int num3 = (int)(*ptr6);
						if (num2 != num3 && ((num2 | 32) != (num3 | 32) || (num2 | 32) - 97 > 25))
						{
							return false;
						}
						ptr5++;
						ptr6++;
						num--;
					}
					return true;
				}
			}
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x00010B88 File Offset: 0x0000ED88
		private unsafe static int CompareOrdinalHelper(string strA, string strB)
		{
			int i = Math.Min(strA.Length, strB.Length);
			fixed (char* ptr = &strA._firstChar)
			{
				char* ptr2 = ptr;
				fixed (char* ptr3 = &strB._firstChar)
				{
					char* ptr4 = ptr3;
					char* ptr5 = ptr2;
					char* ptr6 = ptr4;
					if (ptr5[1] == ptr6[1])
					{
						i -= 2;
						ptr5 += 2;
						ptr6 += 2;
						while (i >= 12)
						{
							if (*(long*)ptr5 == *(long*)ptr6)
							{
								if (*(long*)(ptr5 + 4) == *(long*)(ptr6 + 4))
								{
									if (*(long*)(ptr5 + 8) == *(long*)(ptr6 + 8))
									{
										i -= 12;
										ptr5 += 12;
										ptr6 += 12;
										continue;
									}
									ptr5 += 4;
									ptr6 += 4;
								}
								ptr5 += 4;
								ptr6 += 4;
							}
							if (*(int*)ptr5 == *(int*)ptr6)
							{
								ptr5 += 2;
								ptr6 += 2;
							}
							IL_010E:
							if (*ptr5 != *ptr6)
							{
								return (int)(*ptr5 - *ptr6);
							}
							goto IL_011E;
						}
						while (i > 0)
						{
							if (*(int*)ptr5 != *(int*)ptr6)
							{
								goto IL_010E;
							}
							i -= 2;
							ptr5 += 2;
							ptr6 += 2;
						}
						return strA.Length - strB.Length;
					}
					IL_011E:
					return (int)(ptr5[1] - ptr6[1]);
				}
			}
		}

		/// <summary>Compares two specified <see cref="T:System.String" /> objects and returns an integer that indicates their relative position in the sort order.</summary>
		/// <returns>A 32-bit signed integer that indicates the lexical relationship between the two comparands.Value Condition Less than zero <paramref name="strA" /> is less than <paramref name="strB" />. Zero <paramref name="strA" /> equals <paramref name="strB" />. Greater than zero <paramref name="strA" /> is greater than <paramref name="strB" />. </returns>
		/// <param name="strA">The first string to compare. </param>
		/// <param name="strB">The second string to compare. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060002D8 RID: 728 RVA: 0x00010CBE File Offset: 0x0000EEBE
		public static int Compare(string strA, string strB)
		{
			return string.Compare(strA, strB, StringComparison.CurrentCulture);
		}

		/// <summary>Compares two specified <see cref="T:System.String" /> objects, ignoring or honoring their case, and returns an integer that indicates their relative position in the sort order.</summary>
		/// <returns>A 32-bit signed integer that indicates the lexical relationship between the two comparands.Value Condition Less than zero <paramref name="strA" /> is less than <paramref name="strB" />. Zero <paramref name="strA" /> equals <paramref name="strB" />. Greater than zero <paramref name="strA" /> is greater than <paramref name="strB" />. </returns>
		/// <param name="strA">The first string to compare. </param>
		/// <param name="strB">The second string to compare. </param>
		/// <param name="ignoreCase">true to ignore case during the comparison; otherwise, false.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060002D9 RID: 729 RVA: 0x00010CC8 File Offset: 0x0000EEC8
		public static int Compare(string strA, string strB, bool ignoreCase)
		{
			StringComparison stringComparison = (ignoreCase ? StringComparison.CurrentCultureIgnoreCase : StringComparison.CurrentCulture);
			return string.Compare(strA, strB, stringComparison);
		}

		/// <summary>Compares two specified <see cref="T:System.String" /> objects using the specified rules, and returns an integer that indicates their relative position in the sort order.</summary>
		/// <returns>A 32-bit signed integer that indicates the lexical relationship between the two comparands.Value Condition Less than zero <paramref name="strA" /> is less than <paramref name="strB" />. Zero <paramref name="strA" /> equals <paramref name="strB" />. Greater than zero <paramref name="strA" /> is greater than <paramref name="strB" />. </returns>
		/// <param name="strA">The first string to compare.</param>
		/// <param name="strB">The second string to compare. </param>
		/// <param name="comparisonType">One of the enumeration values that specifies the rules to use in the comparison. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="comparisonType" /> is not a <see cref="T:System.StringComparison" /> value. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <see cref="T:System.StringComparison" /> is not supported.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060002DA RID: 730 RVA: 0x00010CE8 File Offset: 0x0000EEE8
		public static int Compare(string strA, string strB, StringComparison comparisonType)
		{
			if (strA == strB)
			{
				string.CheckStringComparison(comparisonType);
				return 0;
			}
			if (strA == null)
			{
				string.CheckStringComparison(comparisonType);
				return -1;
			}
			if (strB == null)
			{
				string.CheckStringComparison(comparisonType);
				return 1;
			}
			switch (comparisonType)
			{
			case StringComparison.CurrentCulture:
				return CultureInfo.CurrentCulture.CompareInfo.Compare(strA, strB, CompareOptions.None);
			case StringComparison.CurrentCultureIgnoreCase:
				return CultureInfo.CurrentCulture.CompareInfo.Compare(strA, strB, CompareOptions.IgnoreCase);
			case StringComparison.InvariantCulture:
				return CompareInfo.Invariant.Compare(strA, strB, CompareOptions.None);
			case StringComparison.InvariantCultureIgnoreCase:
				return CompareInfo.Invariant.Compare(strA, strB, CompareOptions.IgnoreCase);
			case StringComparison.Ordinal:
				if (strA._firstChar != strB._firstChar)
				{
					return (int)(strA._firstChar - strB._firstChar);
				}
				return string.CompareOrdinalHelper(strA, strB);
			case StringComparison.OrdinalIgnoreCase:
				return CompareInfo.CompareOrdinalIgnoreCase(strA, 0, strA.Length, strB, 0, strB.Length);
			default:
				throw new ArgumentException("The string comparison type passed in is currently not supported.", "comparisonType");
			}
		}

		/// <summary>Compares two specified <see cref="T:System.String" /> objects using the specified comparison options and culture-specific information to influence the comparison, and returns an integer that indicates the relationship of the two strings to each other in the sort order.</summary>
		/// <returns>A 32-bit signed integer that indicates the lexical relationship between <paramref name="strA" /> and <paramref name="strB" />, as shown in the following tableValueConditionLess than zero<paramref name="strA" /> is less than <paramref name="strB" />.Zero<paramref name="strA" /> equals <paramref name="strB" />.Greater than zero<paramref name="strA" /> is greater than <paramref name="strB" />.</returns>
		/// <param name="strA">The first string to compare.  </param>
		/// <param name="strB">The second string to compare.</param>
		/// <param name="culture">The culture that supplies culture-specific comparison information.</param>
		/// <param name="options">Options to use when performing the comparison (such as ignoring case or symbols).  </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> is not a <see cref="T:System.Globalization.CompareOptions" /> value.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="culture" /> is null.</exception>
		// Token: 0x060002DB RID: 731 RVA: 0x00010DC2 File Offset: 0x0000EFC2
		public static int Compare(string strA, string strB, CultureInfo culture, CompareOptions options)
		{
			if (culture == null)
			{
				throw new ArgumentNullException("culture");
			}
			return culture.CompareInfo.Compare(strA, strB, options);
		}

		/// <summary>Compares two specified <see cref="T:System.String" /> objects, ignoring or honoring their case, and using culture-specific information to influence the comparison, and returns an integer that indicates their relative position in the sort order.</summary>
		/// <returns>A 32-bit signed integer that indicates the lexical relationship between the two comparands.Value Condition Less than zero <paramref name="strA" /> is less than <paramref name="strB" />. Zero <paramref name="strA" /> equals <paramref name="strB" />. Greater than zero <paramref name="strA" /> is greater than <paramref name="strB" />. </returns>
		/// <param name="strA">The first string to compare. </param>
		/// <param name="strB">The second string to compare. </param>
		/// <param name="ignoreCase">true to ignore case during the comparison; otherwise, false. </param>
		/// <param name="culture">An object that supplies culture-specific comparison information. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="culture" /> is null. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060002DC RID: 732 RVA: 0x00010DE0 File Offset: 0x0000EFE0
		public static int Compare(string strA, string strB, bool ignoreCase, CultureInfo culture)
		{
			CompareOptions compareOptions = (ignoreCase ? CompareOptions.IgnoreCase : CompareOptions.None);
			return string.Compare(strA, strB, culture, compareOptions);
		}

		/// <summary>Compares substrings of two specified <see cref="T:System.String" /> objects and returns an integer that indicates their relative position in the sort order.</summary>
		/// <returns>A 32-bit signed integer indicating the lexical relationship between the two comparands.Value Condition Less than zero The substring in <paramref name="strA" /> is less than the substring in <paramref name="strB" />. Zero The substrings are equal, or <paramref name="length" /> is zero. Greater than zero The substring in <paramref name="strA" /> is greater than the substring in <paramref name="strB" />. </returns>
		/// <param name="strA">The first string to use in the comparison. </param>
		/// <param name="indexA">The position of the substring within <paramref name="strA" />. </param>
		/// <param name="strB">The second string to use in the comparison. </param>
		/// <param name="indexB">The position of the substring within <paramref name="strB" />. </param>
		/// <param name="length">The maximum number of characters in the substrings to compare. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="indexA" /> is greater than <paramref name="strA" />.<see cref="P:System.String.Length" />.-or- <paramref name="indexB" /> is greater than <paramref name="strB" />.<see cref="P:System.String.Length" />.-or- <paramref name="indexA" />, <paramref name="indexB" />, or <paramref name="length" /> is negative. -or-Either <paramref name="indexA" /> or <paramref name="indexB" /> is null, and <paramref name="length" /> is greater than zero.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060002DD RID: 733 RVA: 0x00010DFE File Offset: 0x0000EFFE
		public static int Compare(string strA, int indexA, string strB, int indexB, int length)
		{
			return string.Compare(strA, indexA, strB, indexB, length, false);
		}

		/// <summary>Compares substrings of two specified <see cref="T:System.String" /> objects, ignoring or honoring their case, and returns an integer that indicates their relative position in the sort order.</summary>
		/// <returns>A 32-bit signed integer that indicates the lexical relationship between the two comparands.ValueCondition Less than zero The substring in <paramref name="strA" /> is less than the substring in <paramref name="strB" />. Zero The substrings are equal, or <paramref name="length" /> is zero. Greater than zero The substring in <paramref name="strA" /> is greater than the substring in <paramref name="strB" />. </returns>
		/// <param name="strA">The first string to use in the comparison. </param>
		/// <param name="indexA">The position of the substring within <paramref name="strA" />. </param>
		/// <param name="strB">The second string to use in the comparison. </param>
		/// <param name="indexB">The position of the substring within <paramref name="strB" />. </param>
		/// <param name="length">The maximum number of characters in the substrings to compare. </param>
		/// <param name="ignoreCase">true to ignore case during the comparison; otherwise, false.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="indexA" /> is greater than <paramref name="strA" />.<see cref="P:System.String.Length" />.-or- <paramref name="indexB" /> is greater than <paramref name="strB" />.<see cref="P:System.String.Length" />.-or- <paramref name="indexA" />, <paramref name="indexB" />, or <paramref name="length" /> is negative. -or-Either <paramref name="indexA" /> or <paramref name="indexB" /> is null, and <paramref name="length" /> is greater than zero.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060002DE RID: 734 RVA: 0x00010E0C File Offset: 0x0000F00C
		public static int Compare(string strA, int indexA, string strB, int indexB, int length, bool ignoreCase)
		{
			int num = length;
			int num2 = length;
			if (strA != null)
			{
				num = Math.Min(num, strA.Length - indexA);
			}
			if (strB != null)
			{
				num2 = Math.Min(num2, strB.Length - indexB);
			}
			CompareOptions compareOptions = (ignoreCase ? CompareOptions.IgnoreCase : CompareOptions.None);
			return CultureInfo.CurrentCulture.CompareInfo.Compare(strA, indexA, num, strB, indexB, num2, compareOptions);
		}

		/// <summary>Compares substrings of two specified <see cref="T:System.String" /> objects, ignoring or honoring their case and using culture-specific information to influence the comparison, and returns an integer that indicates their relative position in the sort order.</summary>
		/// <returns>An integer that indicates the lexical relationship between the two comparands.Value Condition Less than zero The substring in <paramref name="strA" /> is less than the substring in <paramref name="strB" />. Zero The substrings are equal, or <paramref name="length" /> is zero. Greater than zero The substring in <paramref name="strA" /> is greater than the substring in <paramref name="strB" />. </returns>
		/// <param name="strA">The first string to use in the comparison. </param>
		/// <param name="indexA">The position of the substring within <paramref name="strA" />. </param>
		/// <param name="strB">The second string to use in the comparison. </param>
		/// <param name="indexB">The position of the substring within <paramref name="strB" />. </param>
		/// <param name="length">The maximum number of characters in the substrings to compare. </param>
		/// <param name="ignoreCase">true to ignore case during the comparison; otherwise, false. </param>
		/// <param name="culture">An object that supplies culture-specific comparison information. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="indexA" /> is greater than <paramref name="strA" />.<see cref="P:System.String.Length" />.-or- <paramref name="indexB" /> is greater than <paramref name="strB" />.<see cref="P:System.String.Length" />.-or- <paramref name="indexA" />, <paramref name="indexB" />, or <paramref name="length" /> is negative. -or-Either <paramref name="strA" /> or <paramref name="strB" /> is null, and <paramref name="length" /> is greater than zero.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="culture" /> is null. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060002DF RID: 735 RVA: 0x00010E64 File Offset: 0x0000F064
		public static int Compare(string strA, int indexA, string strB, int indexB, int length, bool ignoreCase, CultureInfo culture)
		{
			CompareOptions compareOptions = (ignoreCase ? CompareOptions.IgnoreCase : CompareOptions.None);
			return string.Compare(strA, indexA, strB, indexB, length, culture, compareOptions);
		}

		/// <summary>Compares substrings of two specified <see cref="T:System.String" /> objects using the specified comparison options and culture-specific information to influence the comparison, and returns an integer that indicates the relationship of the two substrings to each other in the sort order.</summary>
		/// <returns>An integer that indicates the lexical relationship between the two substrings, as shown in the following table.ValueConditionLess than zeroThe substring in <paramref name="strA" /> is less than the substring in <paramref name="strB" />.ZeroThe substrings are equal or <paramref name="length" /> is zero.Greater than zeroThe substring in <paramref name="strA" /> is greater than the substring in <paramref name="strB" />.</returns>
		/// <param name="strA">The first string to use in the comparison.   </param>
		/// <param name="indexA">The starting position of the substring within <paramref name="strA" />.</param>
		/// <param name="strB">The second string to use in the comparison.</param>
		/// <param name="indexB">The starting position of the substring within <paramref name="strB" />.</param>
		/// <param name="length">The maximum number of characters in the substrings to compare.</param>
		/// <param name="culture">An object that supplies culture-specific comparison information.</param>
		/// <param name="options">Options to use when performing the comparison (such as ignoring case or symbols).  </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> is not a <see cref="T:System.Globalization.CompareOptions" /> value.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="indexA" /> is greater than <paramref name="strA" />.Length.-or-<paramref name="indexB" /> is greater than <paramref name="strB" />.Length.-or-<paramref name="indexA" />, <paramref name="indexB" />, or <paramref name="length" /> is negative.-or-Either <paramref name="strA" /> or <paramref name="strB" /> is null, and <paramref name="length" /> is greater than zero.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="culture" /> is null.</exception>
		// Token: 0x060002E0 RID: 736 RVA: 0x00010E88 File Offset: 0x0000F088
		public static int Compare(string strA, int indexA, string strB, int indexB, int length, CultureInfo culture, CompareOptions options)
		{
			if (culture == null)
			{
				throw new ArgumentNullException("culture");
			}
			int num = length;
			int num2 = length;
			if (strA != null)
			{
				num = Math.Min(num, strA.Length - indexA);
			}
			if (strB != null)
			{
				num2 = Math.Min(num2, strB.Length - indexB);
			}
			return culture.CompareInfo.Compare(strA, indexA, num, strB, indexB, num2, options);
		}

		/// <summary>Compares substrings of two specified <see cref="T:System.String" /> objects using the specified rules, and returns an integer that indicates their relative position in the sort order. </summary>
		/// <returns>A 32-bit signed integer that indicates the lexical relationship between the two comparands.Value Condition Less than zero The substring in the <paramref name="strA" /> parameter is less than the substring in the <paramref name="strB" /> parameter.Zero The substrings are equal, or the <paramref name="length" /> parameter is zero. Greater than zero The substring in <paramref name="strA" /> is greater than the substring in <paramref name="strB" />. </returns>
		/// <param name="strA">The first string to use in the comparison. </param>
		/// <param name="indexA">The position of the substring within <paramref name="strA" />. </param>
		/// <param name="strB">The second string to use in the comparison.</param>
		/// <param name="indexB">The position of the substring within <paramref name="strB" />. </param>
		/// <param name="length">The maximum number of characters in the substrings to compare. </param>
		/// <param name="comparisonType">One of the enumeration values that specifies the rules to use in the comparison. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="indexA" /> is greater than <paramref name="strA" />.<see cref="P:System.String.Length" />.-or- <paramref name="indexB" /> is greater than <paramref name="strB" />.<see cref="P:System.String.Length" />.-or- <paramref name="indexA" />, <paramref name="indexB" />, or <paramref name="length" /> is negative. -or-Either <paramref name="indexA" /> or <paramref name="indexB" /> is null, and <paramref name="length" /> is greater than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="comparisonType" /> is not a <see cref="T:System.StringComparison" /> value. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060002E1 RID: 737 RVA: 0x00010EE4 File Offset: 0x0000F0E4
		public static int Compare(string strA, int indexA, string strB, int indexB, int length, StringComparison comparisonType)
		{
			string.CheckStringComparison(comparisonType);
			if (strA == null || strB == null)
			{
				if (strA == strB)
				{
					return 0;
				}
				if (strA != null)
				{
					return 1;
				}
				return -1;
			}
			else
			{
				if (length < 0)
				{
					throw new ArgumentOutOfRangeException("length", "Length cannot be less than zero.");
				}
				if (indexA < 0 || indexB < 0)
				{
					throw new ArgumentOutOfRangeException((indexA < 0) ? "indexA" : "indexB", "Index was out of range. Must be non-negative and less than the size of the collection.");
				}
				if (strA.Length - indexA < 0 || strB.Length - indexB < 0)
				{
					throw new ArgumentOutOfRangeException((strA.Length - indexA < 0) ? "indexA" : "indexB", "Index was out of range. Must be non-negative and less than the size of the collection.");
				}
				if (length == 0 || (strA == strB && indexA == indexB))
				{
					return 0;
				}
				int num = Math.Min(length, strA.Length - indexA);
				int num2 = Math.Min(length, strB.Length - indexB);
				switch (comparisonType)
				{
				case StringComparison.CurrentCulture:
					return CultureInfo.CurrentCulture.CompareInfo.Compare(strA, indexA, num, strB, indexB, num2, CompareOptions.None);
				case StringComparison.CurrentCultureIgnoreCase:
					return CultureInfo.CurrentCulture.CompareInfo.Compare(strA, indexA, num, strB, indexB, num2, CompareOptions.IgnoreCase);
				case StringComparison.InvariantCulture:
					return CompareInfo.Invariant.Compare(strA, indexA, num, strB, indexB, num2, CompareOptions.None);
				case StringComparison.InvariantCultureIgnoreCase:
					return CompareInfo.Invariant.Compare(strA, indexA, num, strB, indexB, num2, CompareOptions.IgnoreCase);
				case StringComparison.Ordinal:
					return string.CompareOrdinalHelper(strA, indexA, num, strB, indexB, num2);
				case StringComparison.OrdinalIgnoreCase:
					return CompareInfo.CompareOrdinalIgnoreCase(strA, indexA, num, strB, indexB, num2);
				default:
					throw new ArgumentException("The string comparison type passed in is currently not supported.", "comparisonType");
				}
			}
		}

		/// <summary>Compares two specified <see cref="T:System.String" /> objects by evaluating the numeric values of the corresponding <see cref="T:System.Char" /> objects in each string.</summary>
		/// <returns>An integer that indicates the lexical relationship between the two comparands.ValueCondition Less than zero <paramref name="strA" /> is less than <paramref name="strB" />. Zero <paramref name="strA" /> and <paramref name="strB" /> are equal. Greater than zero <paramref name="strA" /> is greater than <paramref name="strB" />. </returns>
		/// <param name="strA">The first string to compare. </param>
		/// <param name="strB">The second string to compare. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060002E2 RID: 738 RVA: 0x00011043 File Offset: 0x0000F243
		public static int CompareOrdinal(string strA, string strB)
		{
			if (strA == strB)
			{
				return 0;
			}
			if (strA == null)
			{
				return -1;
			}
			if (strB == null)
			{
				return 1;
			}
			if (strA._firstChar != strB._firstChar)
			{
				return (int)(strA._firstChar - strB._firstChar);
			}
			return string.CompareOrdinalHelper(strA, strB);
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x00011078 File Offset: 0x0000F278
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static int CompareOrdinal(ReadOnlySpan<char> strA, ReadOnlySpan<char> strB)
		{
			return SpanHelpers.SequenceCompareTo(MemoryMarshal.GetReference<char>(strA), strA.Length, MemoryMarshal.GetReference<char>(strB), strB.Length);
		}

		/// <summary>Compares substrings of two specified <see cref="T:System.String" /> objects by evaluating the numeric values of the corresponding <see cref="T:System.Char" /> objects in each substring. </summary>
		/// <returns>A 32-bit signed integer that indicates the lexical relationship between the two comparands.ValueCondition Less than zero The substring in <paramref name="strA" /> is less than the substring in <paramref name="strB" />. Zero The substrings are equal, or <paramref name="length" /> is zero. Greater than zero The substring in <paramref name="strA" /> is greater than the substring in <paramref name="strB" />. </returns>
		/// <param name="strA">The first string to use in the comparison. </param>
		/// <param name="indexA">The starting index of the substring in <paramref name="strA" />. </param>
		/// <param name="strB">The second string to use in the comparison. </param>
		/// <param name="indexB">The starting index of the substring in <paramref name="strB" />. </param>
		/// <param name="length">The maximum number of characters in the substrings to compare. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="strA" /> is not null and <paramref name="indexA" /> is greater than <paramref name="strA" />.<see cref="P:System.String.Length" />.-or- <paramref name="strB" /> is not null and<paramref name="indexB" /> is greater than <paramref name="strB" />.<see cref="P:System.String.Length" />.-or- <paramref name="indexA" />, <paramref name="indexB" />, or <paramref name="length" /> is negative. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060002E4 RID: 740 RVA: 0x0001109C File Offset: 0x0000F29C
		public static int CompareOrdinal(string strA, int indexA, string strB, int indexB, int length)
		{
			if (strA == null || strB == null)
			{
				if (strA == strB)
				{
					return 0;
				}
				if (strA != null)
				{
					return 1;
				}
				return -1;
			}
			else
			{
				if (length < 0)
				{
					throw new ArgumentOutOfRangeException("length", "Count cannot be less than zero.");
				}
				if (indexA < 0 || indexB < 0)
				{
					throw new ArgumentOutOfRangeException((indexA < 0) ? "indexA" : "indexB", "Index was out of range. Must be non-negative and less than the size of the collection.");
				}
				int num = Math.Min(length, strA.Length - indexA);
				int num2 = Math.Min(length, strB.Length - indexB);
				if (num < 0 || num2 < 0)
				{
					throw new ArgumentOutOfRangeException((num < 0) ? "indexA" : "indexB", "Index was out of range. Must be non-negative and less than the size of the collection.");
				}
				if (length == 0 || (strA == strB && indexA == indexB))
				{
					return 0;
				}
				return string.CompareOrdinalHelper(strA, indexA, num, strB, indexB, num2);
			}
		}

		/// <summary>Compares this instance with a specified <see cref="T:System.Object" /> and indicates whether this instance precedes, follows, or appears in the same position in the sort order as the specified <see cref="T:System.Object" />.</summary>
		/// <returns>A 32-bit signed integer that indicates whether this instance precedes, follows, or appears in the same position in the sort order as the <paramref name="value" /> parameter.Value Condition Less than zero This instance precedes <paramref name="value" />. Zero This instance has the same position in the sort order as <paramref name="value" />. Greater than zero This instance follows <paramref name="value" />.-or- <paramref name="value" /> is null. </returns>
		/// <param name="value">An object that evaluates to a <see cref="T:System.String" />. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> is not a <see cref="T:System.String" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060002E5 RID: 741 RVA: 0x00011150 File Offset: 0x0000F350
		public int CompareTo(object value)
		{
			if (value == null)
			{
				return 1;
			}
			string text = value as string;
			if (text == null)
			{
				throw new ArgumentException("Object must be of type String.");
			}
			return this.CompareTo(text);
		}

		/// <summary>Compares this instance with a specified <see cref="T:System.String" /> object and indicates whether this instance precedes, follows, or appears in the same position in the sort order as the specified <see cref="T:System.String" />.</summary>
		/// <returns>A 32-bit signed integer that indicates whether this instance precedes, follows, or appears in the same position in the sort order as the <paramref name="value" /> parameter.Value Condition Less than zero This instance precedes <paramref name="strB" />. Zero This instance has the same position in the sort order as <paramref name="strB" />. Greater than zero This instance follows <paramref name="strB" />.-or- <paramref name="strB" /> is null. </returns>
		/// <param name="strB">The string to compare with this instance. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060002E6 RID: 742 RVA: 0x00010CBE File Offset: 0x0000EEBE
		public int CompareTo(string strB)
		{
			return string.Compare(this, strB, StringComparison.CurrentCulture);
		}

		/// <summary>Determines whether the end of this string instance matches the specified string.</summary>
		/// <returns>true if <paramref name="value" /> matches the end of this instance; otherwise, false.</returns>
		/// <param name="value">The string to compare to the substring at the end of this instance. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060002E7 RID: 743 RVA: 0x0001117E File Offset: 0x0000F37E
		public bool EndsWith(string value)
		{
			return this.EndsWith(value, StringComparison.CurrentCulture);
		}

		/// <summary>Determines whether the end of this string instance matches the specified string when compared using the specified comparison option.</summary>
		/// <returns>true if the <paramref name="value" /> parameter matches the end of this string; otherwise, false.</returns>
		/// <param name="value">The string to compare to the substring at the end of this instance. </param>
		/// <param name="comparisonType">One of the enumeration values that determines how this string and <paramref name="value" /> are compared. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="comparisonType" /> is not a <see cref="T:System.StringComparison" /> value.</exception>
		// Token: 0x060002E8 RID: 744 RVA: 0x00011188 File Offset: 0x0000F388
		public bool EndsWith(string value, StringComparison comparisonType)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (this == value)
			{
				string.CheckStringComparison(comparisonType);
				return true;
			}
			if (value.Length == 0)
			{
				string.CheckStringComparison(comparisonType);
				return true;
			}
			switch (comparisonType)
			{
			case StringComparison.CurrentCulture:
				return CultureInfo.CurrentCulture.CompareInfo.IsSuffix(this, value, CompareOptions.None);
			case StringComparison.CurrentCultureIgnoreCase:
				return CultureInfo.CurrentCulture.CompareInfo.IsSuffix(this, value, CompareOptions.IgnoreCase);
			case StringComparison.InvariantCulture:
				return CompareInfo.Invariant.IsSuffix(this, value, CompareOptions.None);
			case StringComparison.InvariantCultureIgnoreCase:
				return CompareInfo.Invariant.IsSuffix(this, value, CompareOptions.IgnoreCase);
			case StringComparison.Ordinal:
				return this.Length >= value.Length && string.CompareOrdinalHelper(this, this.Length - value.Length, value.Length, value, 0, value.Length) == 0;
			case StringComparison.OrdinalIgnoreCase:
				return this.Length >= value.Length && CompareInfo.CompareOrdinalIgnoreCase(this, this.Length - value.Length, value.Length, value, 0, value.Length) == 0;
			default:
				throw new ArgumentException("The string comparison type passed in is currently not supported.", "comparisonType");
			}
		}

		/// <summary>Determines whether the end of this string instance matches the specified string when compared using the specified culture.</summary>
		/// <returns>true if the <paramref name="value" /> parameter matches the end of this string; otherwise, false.</returns>
		/// <param name="value">The string to compare to the substring at the end of this instance. </param>
		/// <param name="ignoreCase">true to ignore case during the comparison; otherwise, false.</param>
		/// <param name="culture">Cultural information that determines how this instance and <paramref name="value" /> are compared. If <paramref name="culture" /> is null, the current culture is used.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060002E9 RID: 745 RVA: 0x0001129D File Offset: 0x0000F49D
		public bool EndsWith(string value, bool ignoreCase, CultureInfo culture)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return this == value || (culture ?? CultureInfo.CurrentCulture).CompareInfo.IsSuffix(this, value, ignoreCase ? CompareOptions.IgnoreCase : CompareOptions.None);
		}

		// Token: 0x060002EA RID: 746 RVA: 0x000112D0 File Offset: 0x0000F4D0
		public bool EndsWith(char value)
		{
			int length = this.Length;
			return length != 0 && this[length - 1] == value;
		}

		/// <summary>Determines whether this instance and a specified object, which must also be a <see cref="T:System.String" /> object, have the same value.</summary>
		/// <returns>true if <paramref name="obj" /> is a <see cref="T:System.String" /> and its value is the same as this instance; otherwise, false.</returns>
		/// <param name="obj">The string to compare to this instance. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060002EB RID: 747 RVA: 0x000112F8 File Offset: 0x0000F4F8
		public override bool Equals(object obj)
		{
			if (this == obj)
			{
				return true;
			}
			string text = obj as string;
			return text != null && this.Length == text.Length && string.EqualsHelper(this, text);
		}

		/// <summary>Determines whether this instance and another specified <see cref="T:System.String" /> object have the same value.</summary>
		/// <returns>true if the value of the <paramref name="value" /> parameter is the same as this instance; otherwise, false.</returns>
		/// <param name="value">The string to compare to this instance. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060002EC RID: 748 RVA: 0x0001132E File Offset: 0x0000F52E
		public bool Equals(string value)
		{
			return this == value || (value != null && this.Length == value.Length && string.EqualsHelper(this, value));
		}

		/// <summary>Determines whether this string and a specified <see cref="T:System.String" /> object have the same value. A parameter specifies the culture, case, and sort rules used in the comparison.</summary>
		/// <returns>true if the value of the <paramref name="value" /> parameter is the same as this string; otherwise, false.</returns>
		/// <param name="value">The string to compare to this instance.</param>
		/// <param name="comparisonType">One of the enumeration values that specifies how the strings will be compared. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="comparisonType" /> is not a <see cref="T:System.StringComparison" /> value. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060002ED RID: 749 RVA: 0x00011354 File Offset: 0x0000F554
		public bool Equals(string value, StringComparison comparisonType)
		{
			if (this == value)
			{
				string.CheckStringComparison(comparisonType);
				return true;
			}
			if (value == null)
			{
				string.CheckStringComparison(comparisonType);
				return false;
			}
			switch (comparisonType)
			{
			case StringComparison.CurrentCulture:
				return CultureInfo.CurrentCulture.CompareInfo.Compare(this, value, CompareOptions.None) == 0;
			case StringComparison.CurrentCultureIgnoreCase:
				return CultureInfo.CurrentCulture.CompareInfo.Compare(this, value, CompareOptions.IgnoreCase) == 0;
			case StringComparison.InvariantCulture:
				return CompareInfo.Invariant.Compare(this, value, CompareOptions.None) == 0;
			case StringComparison.InvariantCultureIgnoreCase:
				return CompareInfo.Invariant.Compare(this, value, CompareOptions.IgnoreCase) == 0;
			case StringComparison.Ordinal:
				return this.Length == value.Length && string.EqualsHelper(this, value);
			case StringComparison.OrdinalIgnoreCase:
				return this.Length == value.Length && CompareInfo.CompareOrdinalIgnoreCase(this, 0, this.Length, value, 0, value.Length) == 0;
			default:
				throw new ArgumentException("The string comparison type passed in is currently not supported.", "comparisonType");
			}
		}

		/// <summary>Determines whether two specified <see cref="T:System.String" /> objects have the same value.</summary>
		/// <returns>true if the value of <paramref name="a" /> is the same as the value of <paramref name="b" />; otherwise, false. If both <paramref name="a" /> and <paramref name="b" /> are null, the method returns true.</returns>
		/// <param name="a">The first string to compare, or null. </param>
		/// <param name="b">The second string to compare, or null. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060002EE RID: 750 RVA: 0x00011439 File Offset: 0x0000F639
		public static bool Equals(string a, string b)
		{
			return a == b || (a != null && b != null && a.Length == b.Length && string.EqualsHelper(a, b));
		}

		/// <summary>Determines whether two specified <see cref="T:System.String" /> objects have the same value. A parameter specifies the culture, case, and sort rules used in the comparison.</summary>
		/// <returns>true if the value of the <paramref name="a" /> parameter is equal to the value of the <paramref name="b" /> parameter; otherwise, false.</returns>
		/// <param name="a">The first string to compare, or null. </param>
		/// <param name="b">The second string to compare, or null. </param>
		/// <param name="comparisonType">One of the enumeration values that specifies the rules for the comparison. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="comparisonType" /> is not a <see cref="T:System.StringComparison" /> value. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060002EF RID: 751 RVA: 0x00011460 File Offset: 0x0000F660
		public static bool Equals(string a, string b, StringComparison comparisonType)
		{
			if (a == b)
			{
				string.CheckStringComparison(comparisonType);
				return true;
			}
			if (a == null || b == null)
			{
				string.CheckStringComparison(comparisonType);
				return false;
			}
			switch (comparisonType)
			{
			case StringComparison.CurrentCulture:
				return CultureInfo.CurrentCulture.CompareInfo.Compare(a, b, CompareOptions.None) == 0;
			case StringComparison.CurrentCultureIgnoreCase:
				return CultureInfo.CurrentCulture.CompareInfo.Compare(a, b, CompareOptions.IgnoreCase) == 0;
			case StringComparison.InvariantCulture:
				return CompareInfo.Invariant.Compare(a, b, CompareOptions.None) == 0;
			case StringComparison.InvariantCultureIgnoreCase:
				return CompareInfo.Invariant.Compare(a, b, CompareOptions.IgnoreCase) == 0;
			case StringComparison.Ordinal:
				return a.Length == b.Length && string.EqualsHelper(a, b);
			case StringComparison.OrdinalIgnoreCase:
				return a.Length == b.Length && CompareInfo.CompareOrdinalIgnoreCase(a, 0, a.Length, b, 0, b.Length) == 0;
			default:
				throw new ArgumentException("The string comparison type passed in is currently not supported.", "comparisonType");
			}
		}

		/// <summary>Determines whether two specified strings have the same value.</summary>
		/// <returns>true if the value of <paramref name="a" /> is the same as the value of <paramref name="b" />; otherwise, false.</returns>
		/// <param name="a">The first string to compare, or null. </param>
		/// <param name="b">The second string to compare, or null. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x060002F0 RID: 752 RVA: 0x00011548 File Offset: 0x0000F748
		public static bool operator ==(string a, string b)
		{
			return string.Equals(a, b);
		}

		/// <summary>Determines whether two specified strings have different values.</summary>
		/// <returns>true if the value of <paramref name="a" /> is different from the value of <paramref name="b" />; otherwise, false.</returns>
		/// <param name="a">The first string to compare, or null. </param>
		/// <param name="b">The second string to compare, or null. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x060002F1 RID: 753 RVA: 0x00011551 File Offset: 0x0000F751
		public static bool operator !=(string a, string b)
		{
			return !string.Equals(a, b);
		}

		/// <summary>Returns the hash code for this string.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060002F2 RID: 754 RVA: 0x0001155D File Offset: 0x0000F75D
		public override int GetHashCode()
		{
			return this.GetLegacyNonRandomizedHashCode();
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x00011565 File Offset: 0x0000F765
		public int GetHashCode(StringComparison comparisonType)
		{
			return StringComparer.FromComparison(comparisonType).GetHashCode(this);
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x00011574 File Offset: 0x0000F774
		internal unsafe int GetLegacyNonRandomizedHashCode()
		{
			fixed (char* ptr = &this._firstChar)
			{
				char* ptr2 = ptr;
				int num = 5381;
				int num2 = num;
				char* ptr3 = ptr2;
				int num3;
				while ((num3 = (int)(*ptr3)) != 0)
				{
					num = ((num << 5) + num) ^ num3;
					num3 = (int)ptr3[1];
					if (num3 == 0)
					{
						break;
					}
					num2 = ((num2 << 5) + num2) ^ num3;
					ptr3 += 2;
				}
				return num + num2 * 1566083941;
			}
		}

		/// <summary>Determines whether the beginning of this string instance matches the specified string.</summary>
		/// <returns>true if <paramref name="value" /> matches the beginning of this string; otherwise, false.</returns>
		/// <param name="value">The string to compare. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060002F5 RID: 757 RVA: 0x000115C8 File Offset: 0x0000F7C8
		public bool StartsWith(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return this.StartsWith(value, StringComparison.CurrentCulture);
		}

		/// <summary>Determines whether the beginning of this string instance matches the specified string when compared using the specified comparison option.</summary>
		/// <returns>true if this instance begins with <paramref name="value" />; otherwise, false.</returns>
		/// <param name="value">The string to compare. </param>
		/// <param name="comparisonType">One of the enumeration values that determines how this string and <paramref name="value" /> are compared. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="comparisonType" /> is not a <see cref="T:System.StringComparison" /> value.</exception>
		// Token: 0x060002F6 RID: 758 RVA: 0x000115E0 File Offset: 0x0000F7E0
		public bool StartsWith(string value, StringComparison comparisonType)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (this == value)
			{
				string.CheckStringComparison(comparisonType);
				return true;
			}
			if (value.Length == 0)
			{
				string.CheckStringComparison(comparisonType);
				return true;
			}
			switch (comparisonType)
			{
			case StringComparison.CurrentCulture:
				return CultureInfo.CurrentCulture.CompareInfo.IsPrefix(this, value, CompareOptions.None);
			case StringComparison.CurrentCultureIgnoreCase:
				return CultureInfo.CurrentCulture.CompareInfo.IsPrefix(this, value, CompareOptions.IgnoreCase);
			case StringComparison.InvariantCulture:
				return CompareInfo.Invariant.IsPrefix(this, value, CompareOptions.None);
			case StringComparison.InvariantCultureIgnoreCase:
				return CompareInfo.Invariant.IsPrefix(this, value, CompareOptions.IgnoreCase);
			case StringComparison.Ordinal:
				return this.Length >= value.Length && this._firstChar == value._firstChar && (value.Length == 1 || SpanHelpers.SequenceEqual(Unsafe.As<char, byte>(this.GetRawStringData()), Unsafe.As<char, byte>(value.GetRawStringData()), (ulong)((long)value.Length * 2L)));
			case StringComparison.OrdinalIgnoreCase:
				return this.Length >= value.Length && CompareInfo.CompareOrdinalIgnoreCase(this, 0, value.Length, value, 0, value.Length) == 0;
			default:
				throw new ArgumentException("The string comparison type passed in is currently not supported.", "comparisonType");
			}
		}

		/// <summary>Determines whether the beginning of this string instance matches the specified string when compared using the specified culture.</summary>
		/// <returns>true if the <paramref name="value" /> parameter matches the beginning of this string; otherwise, false.</returns>
		/// <param name="value">The string to compare. </param>
		/// <param name="ignoreCase">true to ignore case during the comparison; otherwise, false.</param>
		/// <param name="culture">Cultural information that determines how this string and <paramref name="value" /> are compared. If <paramref name="culture" /> is null, the current culture is used.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060002F7 RID: 759 RVA: 0x00011703 File Offset: 0x0000F903
		public bool StartsWith(string value, bool ignoreCase, CultureInfo culture)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return this == value || (culture ?? CultureInfo.CurrentCulture).CompareInfo.IsPrefix(this, value, ignoreCase ? CompareOptions.IgnoreCase : CompareOptions.None);
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x00011736 File Offset: 0x0000F936
		public bool StartsWith(char value)
		{
			return this.Length != 0 && this._firstChar == value;
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x0001174B File Offset: 0x0000F94B
		internal static void CheckStringComparison(StringComparison comparisonType)
		{
			if (comparisonType - StringComparison.CurrentCulture > 5)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.NotSupported_StringComparison, ExceptionArgument.comparisonType);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.String" /> class to the value indicated by an array of Unicode characters.</summary>
		/// <param name="value">An array of Unicode characters. </param>
		// Token: 0x060002FA RID: 762
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern String(char[] value);

		// Token: 0x060002FB RID: 763 RVA: 0x0001175C File Offset: 0x0000F95C
		private unsafe static string Ctor(char[] value)
		{
			if (value == null || value.Length == 0)
			{
				return string.Empty;
			}
			string text = string.FastAllocateString(value.Length);
			fixed (char* ptr = &text._firstChar)
			{
				char* ptr2 = ptr;
				fixed (char[] array = value)
				{
					char* ptr3;
					if (value == null || array.Length == 0)
					{
						ptr3 = null;
					}
					else
					{
						ptr3 = &array[0];
					}
					string.wstrcpy(ptr2, ptr3, value.Length);
					ptr = null;
				}
				return text;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.String" /> class to the value indicated by an array of Unicode characters, a starting character position within that array, and a length.</summary>
		/// <param name="value">An array of Unicode characters. </param>
		/// <param name="startIndex">The starting position within <paramref name="value" />. </param>
		/// <param name="length">The number of characters within <paramref name="value" /> to use. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> or <paramref name="length" /> is less than zero.-or- The sum of <paramref name="startIndex" /> and <paramref name="length" /> is greater than the number of elements in <paramref name="value" />. </exception>
		// Token: 0x060002FC RID: 764
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern String(char[] value, int startIndex, int length);

		// Token: 0x060002FD RID: 765 RVA: 0x000117B0 File Offset: 0x0000F9B0
		private unsafe static string Ctor(char[] value, int startIndex, int length)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (startIndex < 0)
			{
				throw new ArgumentOutOfRangeException("startIndex", "StartIndex cannot be less than zero.");
			}
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length", "Length cannot be less than zero.");
			}
			if (startIndex > value.Length - length)
			{
				throw new ArgumentOutOfRangeException("startIndex", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			if (length == 0)
			{
				return string.Empty;
			}
			string text = string.FastAllocateString(length);
			fixed (char* ptr = &text._firstChar)
			{
				char* ptr2 = ptr;
				fixed (char[] array = value)
				{
					char* ptr3;
					if (value == null || array.Length == 0)
					{
						ptr3 = null;
					}
					else
					{
						ptr3 = &array[0];
					}
					string.wstrcpy(ptr2, ptr3 + startIndex, length);
					ptr = null;
				}
				return text;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.String" /> class to the value indicated by a specified pointer to an array of Unicode characters.</summary>
		/// <param name="value">A pointer to a null-terminated array of Unicode characters. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The current process does not have read access to all the addressed characters.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> specifies an array that contains an invalid Unicode character, or <paramref name="value" /> specifies an address less than 64000.</exception>
		// Token: 0x060002FE RID: 766
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe extern String(char* value);

		// Token: 0x060002FF RID: 767 RVA: 0x0001184C File Offset: 0x0000FA4C
		private unsafe static string Ctor(char* ptr)
		{
			if (ptr == null)
			{
				return string.Empty;
			}
			int num = string.wcslen(ptr);
			if (num == 0)
			{
				return string.Empty;
			}
			string text = string.FastAllocateString(num);
			fixed (char* ptr2 = &text._firstChar)
			{
				string.wstrcpy(ptr2, ptr, num);
			}
			return text;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.String" /> class to the value indicated by a specified pointer to an array of Unicode characters, a starting character position within that array, and a length.</summary>
		/// <param name="value">A pointer to an array of Unicode characters. </param>
		/// <param name="startIndex">The starting position within <paramref name="value" />. </param>
		/// <param name="length">The number of characters within <paramref name="value" /> to use. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> or <paramref name="length" /> is less than zero, <paramref name="value" /> + <paramref name="startIndex" /> cause a pointer overflow, or the current process does not have read access to all the addressed characters.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> specifies an array that contains an invalid Unicode character, or <paramref name="value" /> + <paramref name="startIndex" /> specifies an address less than 64000.</exception>
		// Token: 0x06000300 RID: 768
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe extern String(char* value, int startIndex, int length);

		// Token: 0x06000301 RID: 769 RVA: 0x00011890 File Offset: 0x0000FA90
		private unsafe static string Ctor(char* ptr, int startIndex, int length)
		{
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length", "Length cannot be less than zero.");
			}
			if (startIndex < 0)
			{
				throw new ArgumentOutOfRangeException("startIndex", "StartIndex cannot be less than zero.");
			}
			char* ptr2 = ptr + startIndex;
			if (ptr2 < ptr)
			{
				throw new ArgumentOutOfRangeException("startIndex", "Pointer startIndex and length do not refer to a valid string.");
			}
			if (length == 0)
			{
				return string.Empty;
			}
			if (ptr == null)
			{
				throw new ArgumentOutOfRangeException("ptr", "Pointer startIndex and length do not refer to a valid string.");
			}
			string text = string.FastAllocateString(length);
			fixed (char* ptr3 = &text._firstChar)
			{
				string.wstrcpy(ptr3, ptr2, length);
			}
			return text;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.String" /> class to the value indicated by a pointer to an array of 8-bit signed integers.</summary>
		/// <param name="value">A pointer to a null-terminated array of 8-bit signed integers. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">A new instance of <see cref="T:System.String" /> could not be initialized using <paramref name="value" />, assuming <paramref name="value" /> is encoded in ANSI. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length of the new string to initialize, which is determined by the null termination character of <paramref name="value" />, is too large to allocate. </exception>
		/// <exception cref="T:System.AccessViolationException">
		///   <paramref name="value" /> specifies an invalid address.</exception>
		// Token: 0x06000302 RID: 770
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe extern String(sbyte* value);

		// Token: 0x06000303 RID: 771 RVA: 0x00011918 File Offset: 0x0000FB18
		private unsafe static string Ctor(sbyte* value)
		{
			if (value == null)
			{
				return string.Empty;
			}
			int num = new ReadOnlySpan<byte>((void*)value, int.MaxValue).IndexOf(0);
			if (num < 0)
			{
				throw new ArgumentException("The string must be null-terminated.");
			}
			return string.CreateStringForSByteConstructor((byte*)value, num);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.String" /> class to the value indicated by a specified pointer to an array of 8-bit signed integers, a starting position within that array, and a length.</summary>
		/// <param name="value">A pointer to an array of 8-bit signed integers. </param>
		/// <param name="startIndex">The starting position within <paramref name="value" />. </param>
		/// <param name="length">The number of characters within <paramref name="value" /> to use. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> or <paramref name="length" /> is less than zero. -or-The address specified by <paramref name="value" /> + <paramref name="startIndex" /> is too large for the current platform; that is, the address calculation overflowed. -or-The length of the new string to initialize is too large to allocate.</exception>
		/// <exception cref="T:System.ArgumentException">The address specified by <paramref name="value" /> + <paramref name="startIndex" /> is less than 64K.-or- A new instance of <see cref="T:System.String" /> could not be initialized using <paramref name="value" />, assuming <paramref name="value" /> is encoded in ANSI. </exception>
		/// <exception cref="T:System.AccessViolationException">
		///   <paramref name="value" />, <paramref name="startIndex" />, and <paramref name="length" /> collectively specify an invalid address.</exception>
		// Token: 0x06000304 RID: 772
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe extern String(sbyte* value, int startIndex, int length);

		// Token: 0x06000305 RID: 773 RVA: 0x0001195C File Offset: 0x0000FB5C
		private unsafe static string Ctor(sbyte* value, int startIndex, int length)
		{
			if (startIndex < 0)
			{
				throw new ArgumentOutOfRangeException("startIndex", "StartIndex cannot be less than zero.");
			}
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length", "Length cannot be less than zero.");
			}
			if (value == null)
			{
				if (length == 0)
				{
					return string.Empty;
				}
				throw new ArgumentNullException("value");
			}
			else
			{
				byte* ptr = (byte*)(value + startIndex);
				if (ptr < (byte*)value)
				{
					throw new ArgumentOutOfRangeException("value", "Pointer startIndex and length do not refer to a valid string.");
				}
				return string.CreateStringForSByteConstructor(ptr, length);
			}
		}

		// Token: 0x06000306 RID: 774 RVA: 0x000119C9 File Offset: 0x0000FBC9
		private unsafe static string CreateStringForSByteConstructor(byte* pb, int numBytes)
		{
			if (numBytes == 0)
			{
				return string.Empty;
			}
			return Encoding.UTF8.GetString(pb, numBytes);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.String" /> class to the value indicated by a specified pointer to an array of 8-bit signed integers, a starting position within that array, a length, and an <see cref="T:System.Text.Encoding" /> object.</summary>
		/// <param name="value">A pointer to an array of 8-bit signed integers. </param>
		/// <param name="startIndex">The starting position within <paramref name="value" />. </param>
		/// <param name="length">The number of characters within <paramref name="value" /> to use. </param>
		/// <param name="enc">An object that specifies how the array referenced by <paramref name="value" /> is encoded. If <paramref name="enc" /> is null, ANSI encoding is assumed.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> or <paramref name="length" /> is less than zero. -or-The address specified by <paramref name="value" /> + <paramref name="startIndex" /> is too large for the current platform; that is, the address calculation overflowed. -or-The length of the new string to initialize is too large to allocate.</exception>
		/// <exception cref="T:System.ArgumentException">The address specified by <paramref name="value" /> + <paramref name="startIndex" /> is less than 64K.-or- A new instance of <see cref="T:System.String" /> could not be initialized using <paramref name="value" />, assuming <paramref name="value" /> is encoded as specified by <paramref name="enc" />. </exception>
		/// <exception cref="T:System.AccessViolationException">
		///   <paramref name="value" />, <paramref name="startIndex" />, and <paramref name="length" /> collectively specify an invalid address.</exception>
		// Token: 0x06000307 RID: 775
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe extern String(sbyte* value, int startIndex, int length, Encoding enc);

		// Token: 0x06000308 RID: 776 RVA: 0x000119E0 File Offset: 0x0000FBE0
		private unsafe static string Ctor(sbyte* value, int startIndex, int length, Encoding enc)
		{
			if (enc == null)
			{
				return new string(value, startIndex, length);
			}
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length", "Non-negative number required.");
			}
			if (startIndex < 0)
			{
				throw new ArgumentOutOfRangeException("startIndex", "StartIndex cannot be less than zero.");
			}
			if (value == null)
			{
				if (length == 0)
				{
					return string.Empty;
				}
				throw new ArgumentNullException("value");
			}
			else
			{
				byte* ptr = (byte*)(value + startIndex);
				if (ptr < (byte*)value)
				{
					throw new ArgumentOutOfRangeException("startIndex", "Pointer startIndex and length do not refer to a valid string.");
				}
				return enc.GetString(new ReadOnlySpan<byte>((void*)ptr, length));
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.String" /> class to the value indicated by a specified Unicode character repeated a specified number of times.</summary>
		/// <param name="c">A Unicode character. </param>
		/// <param name="count">The number of times <paramref name="c" /> occurs. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="count" /> is less than zero. </exception>
		// Token: 0x06000309 RID: 777
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern String(char c, int count);

		// Token: 0x0600030A RID: 778 RVA: 0x00011A60 File Offset: 0x0000FC60
		private unsafe static string Ctor(char c, int count)
		{
			if (count > 0)
			{
				string text = string.FastAllocateString(count);
				fixed (char* ptr = &text._firstChar)
				{
					uint* ptr2 = (uint*)ptr;
					uint num = (uint)(((uint)c << 16) | c);
					uint* ptr3 = ptr2;
					if (count >= 4)
					{
						count -= 4;
						do
						{
							*ptr3 = num;
							ptr3[1] = num;
							ptr3 += 2;
							count -= 4;
						}
						while (count >= 0);
					}
					if ((count & 2) != 0)
					{
						*ptr3 = num;
						ptr3++;
					}
					if ((count & 1) != 0)
					{
						*(short*)ptr3 = (short)c;
					}
				}
				return text;
			}
			if (count == 0)
			{
				return string.Empty;
			}
			throw new ArgumentOutOfRangeException("count", "Count cannot be less than zero.");
		}

		// Token: 0x0600030B RID: 779
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern String(ReadOnlySpan<char> value);

		// Token: 0x0600030C RID: 780 RVA: 0x00011ADC File Offset: 0x0000FCDC
		private unsafe static string Ctor(ReadOnlySpan<char> value)
		{
			if (value.Length == 0)
			{
				return string.Empty;
			}
			string text = string.FastAllocateString(value.Length);
			fixed (char* ptr = &text._firstChar)
			{
				char* ptr2 = ptr;
				fixed (char* reference = MemoryMarshal.GetReference<char>(value))
				{
					char* ptr3 = reference;
					string.wstrcpy(ptr2, ptr3, value.Length);
					ptr = null;
				}
				return text;
			}
		}

		// Token: 0x0600030D RID: 781 RVA: 0x00011B2C File Offset: 0x0000FD2C
		public static string Create<TState>(int length, TState state, SpanAction<char, TState> action)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			if (length > 0)
			{
				string text = string.FastAllocateString(length);
				action(new Span<char>(text.GetRawStringData(), length), state);
				return text;
			}
			if (length == 0)
			{
				return string.Empty;
			}
			throw new ArgumentOutOfRangeException("length");
		}

		// Token: 0x0600030E RID: 782 RVA: 0x00011B7C File Offset: 0x0000FD7C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator ReadOnlySpan<char>(string value)
		{
			if (value == null)
			{
				return default(ReadOnlySpan<char>);
			}
			return new ReadOnlySpan<char>(value.GetRawStringData(), value.Length);
		}

		/// <summary>Returns a reference to this instance of <see cref="T:System.String" />.</summary>
		/// <returns>This instance of <see cref="T:System.String" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600030F RID: 783 RVA: 0x00002645 File Offset: 0x00000845
		public object Clone()
		{
			return this;
		}

		/// <summary>Creates a new instance of <see cref="T:System.String" /> with the same value as a specified <see cref="T:System.String" />.</summary>
		/// <returns>A new string with the same value as <paramref name="str" />.</returns>
		/// <param name="str">The string to copy. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="str" /> is null. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000310 RID: 784 RVA: 0x00011BA8 File Offset: 0x0000FDA8
		public unsafe static string Copy(string str)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			string text = string.FastAllocateString(str.Length);
			fixed (char* ptr = &text._firstChar)
			{
				char* ptr2 = ptr;
				fixed (char* ptr3 = &str._firstChar)
				{
					char* ptr4 = ptr3;
					string.wstrcpy(ptr2, ptr4, str.Length);
					ptr = null;
				}
				return text;
			}
		}

		/// <summary>Copies a specified number of characters from a specified position in this instance to a specified position in an array of Unicode characters.</summary>
		/// <param name="sourceIndex">The index of the first character in this instance to copy. </param>
		/// <param name="destination">An array of Unicode characters to which characters in this instance are copied. </param>
		/// <param name="destinationIndex">The index in <paramref name="destination" /> at which the copy operation begins. </param>
		/// <param name="count">The number of characters in this instance to copy to <paramref name="destination" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="destination" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="sourceIndex" />, <paramref name="destinationIndex" />, or <paramref name="count" /> is negative -or- <paramref name="count" /> is greater than the length of the substring from <paramref name="startIndex" /> to the end of this instance -or- <paramref name="count" /> is greater than the length of the subarray from <paramref name="destinationIndex" /> to the end of <paramref name="destination" /></exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000311 RID: 785 RVA: 0x00011BF4 File Offset: 0x0000FDF4
		public unsafe void CopyTo(int sourceIndex, char[] destination, int destinationIndex, int count)
		{
			if (destination == null)
			{
				throw new ArgumentNullException("destination");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Count cannot be less than zero.");
			}
			if (sourceIndex < 0)
			{
				throw new ArgumentOutOfRangeException("sourceIndex", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			if (count > this.Length - sourceIndex)
			{
				throw new ArgumentOutOfRangeException("sourceIndex", "Index and count must refer to a location within the string.");
			}
			if (destinationIndex > destination.Length - count || destinationIndex < 0)
			{
				throw new ArgumentOutOfRangeException("destinationIndex", "Index and count must refer to a location within the string.");
			}
			fixed (char* ptr = &this._firstChar)
			{
				char* ptr2 = ptr;
				fixed (char[] array = destination)
				{
					char* ptr3;
					if (destination == null || array.Length == 0)
					{
						ptr3 = null;
					}
					else
					{
						ptr3 = &array[0];
					}
					string.wstrcpy(ptr3 + destinationIndex, ptr2 + sourceIndex, count);
					ptr = null;
				}
				return;
			}
		}

		/// <summary>Copies the characters in this instance to a Unicode character array.</summary>
		/// <returns>A Unicode character array whose elements are the individual characters of this instance. If this instance is an empty string, the returned array is empty and has a zero length.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000312 RID: 786 RVA: 0x00011CAC File Offset: 0x0000FEAC
		public unsafe char[] ToCharArray()
		{
			if (this.Length == 0)
			{
				return Array.Empty<char>();
			}
			char[] array = new char[this.Length];
			fixed (char* ptr = &this._firstChar)
			{
				char* ptr2 = ptr;
				fixed (char* ptr3 = &array[0])
				{
					string.wstrcpy(ptr3, ptr2, this.Length);
					ptr = null;
				}
				return array;
			}
		}

		/// <summary>Copies the characters in a specified substring in this instance to a Unicode character array.</summary>
		/// <returns>A Unicode character array whose elements are the <paramref name="length" /> number of characters in this instance starting from character position <paramref name="startIndex" />.</returns>
		/// <param name="startIndex">The starting position of a substring in this instance. </param>
		/// <param name="length">The length of the substring in this instance. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> or <paramref name="length" /> is less than zero.-or- <paramref name="startIndex" /> plus <paramref name="length" /> is greater than the length of this instance. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000313 RID: 787 RVA: 0x00011CF8 File Offset: 0x0000FEF8
		public unsafe char[] ToCharArray(int startIndex, int length)
		{
			if (startIndex < 0 || startIndex > this.Length || startIndex > this.Length - length)
			{
				throw new ArgumentOutOfRangeException("startIndex", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			if (length > 0)
			{
				char[] array = new char[length];
				fixed (char* ptr = &this._firstChar)
				{
					char* ptr2 = ptr;
					fixed (char* ptr3 = &array[0])
					{
						string.wstrcpy(ptr3, ptr2 + startIndex, length);
						ptr = null;
					}
					return array;
				}
			}
			if (length == 0)
			{
				return Array.Empty<char>();
			}
			throw new ArgumentOutOfRangeException("length", "Index was out of range. Must be non-negative and less than the size of the collection.");
		}

		/// <summary>Indicates whether the specified string is null or an <see cref="F:System.String.Empty" /> string.</summary>
		/// <returns>true if the <paramref name="value" /> parameter is null or an empty string (""); otherwise, false.</returns>
		/// <param name="value">The string to test. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000314 RID: 788 RVA: 0x00011D76 File Offset: 0x0000FF76
		[NonVersionable]
		public static bool IsNullOrEmpty(string value)
		{
			return value == null || 0 >= value.Length;
		}

		/// <summary>Indicates whether a specified string is null, empty, or consists only of white-space characters.</summary>
		/// <returns>true if the <paramref name="value" /> parameter is null or <see cref="F:System.String.Empty" />, or if <paramref name="value" /> consists exclusively of white-space characters. </returns>
		/// <param name="value">The string to test.</param>
		// Token: 0x06000315 RID: 789 RVA: 0x00011D88 File Offset: 0x0000FF88
		public static bool IsNullOrWhiteSpace(string value)
		{
			if (value == null)
			{
				return true;
			}
			for (int i = 0; i < value.Length; i++)
			{
				if (!char.IsWhiteSpace(value[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000316 RID: 790 RVA: 0x00011DBC File Offset: 0x0000FFBC
		internal ref char GetRawStringData()
		{
			return ref this._firstChar;
		}

		// Token: 0x06000317 RID: 791 RVA: 0x00011DC4 File Offset: 0x0000FFC4
		internal unsafe static string CreateStringFromEncoding(byte* bytes, int byteLength, Encoding encoding)
		{
			int charCount = encoding.GetCharCount(bytes, byteLength, null);
			if (charCount == 0)
			{
				return string.Empty;
			}
			string text = string.FastAllocateString(charCount);
			fixed (char* ptr = &text._firstChar)
			{
				char* ptr2 = ptr;
				encoding.GetChars(bytes, byteLength, ptr2, charCount, null);
			}
			return text;
		}

		// Token: 0x06000318 RID: 792 RVA: 0x00011E03 File Offset: 0x00010003
		internal static string CreateFromChar(char c)
		{
			string text = string.FastAllocateString(1);
			text._firstChar = c;
			return text;
		}

		// Token: 0x06000319 RID: 793 RVA: 0x00011E12 File Offset: 0x00010012
		internal unsafe static void wstrcpy(char* dmem, char* smem, int charCount)
		{
			Buffer.Memmove((byte*)dmem, (byte*)smem, (uint)(charCount * 2));
		}

		/// <summary>Returns this instance of <see cref="T:System.String" />; no actual conversion is performed.</summary>
		/// <returns>The current string.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600031A RID: 794 RVA: 0x00002645 File Offset: 0x00000845
		public override string ToString()
		{
			return this;
		}

		/// <summary>Returns this instance of <see cref="T:System.String" />; no actual conversion is performed.</summary>
		/// <returns>The current string.</returns>
		/// <param name="provider">(Reserved) An object that supplies culture-specific formatting information. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600031B RID: 795 RVA: 0x00002645 File Offset: 0x00000845
		public string ToString(IFormatProvider provider)
		{
			return this;
		}

		/// <summary>Retrieves an object that can iterate through the individual characters in this string.</summary>
		/// <returns>An enumerator object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600031C RID: 796 RVA: 0x00011E1E File Offset: 0x0001001E
		public CharEnumerator GetEnumerator()
		{
			return new CharEnumerator(this);
		}

		// Token: 0x0600031D RID: 797 RVA: 0x00011E1E File Offset: 0x0001001E
		IEnumerator<char> IEnumerable<char>.GetEnumerator()
		{
			return new CharEnumerator(this);
		}

		/// <summary>Returns an enumerator that iterates through the current <see cref="T:System.String" /> object. </summary>
		/// <returns>An enumerator that can be used to iterate through the current string.</returns>
		// Token: 0x0600031E RID: 798 RVA: 0x00011E1E File Offset: 0x0001001E
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new CharEnumerator(this);
		}

		// Token: 0x0600031F RID: 799 RVA: 0x00011E28 File Offset: 0x00010028
		internal unsafe static int wcslen(char* ptr)
		{
			char* ptr2 = ptr;
			int num = IntPtr.Size - 1;
			while ((ptr2 & (uint)num) != 0U)
			{
				if (*ptr2 != '\0')
				{
					ptr2++;
				}
				else
				{
					IL_006E:
					int num2 = (int)((long)(ptr2 - ptr));
					if (ptr + num2 != ptr2)
					{
						throw new ArgumentException("The string must be null-terminated.");
					}
					return num2;
				}
			}
			for (;;)
			{
				if (((*(long*)ptr2 + 9223231297218904063L) | 9223231297218904063L) == -1L)
				{
					ptr2 += 4;
				}
				else
				{
					if (*ptr2 == '\0')
					{
						goto IL_006E;
					}
					if (ptr2[1] == '\0')
					{
						goto IL_006A;
					}
					if (ptr2[2] == '\0')
					{
						goto IL_0066;
					}
					if (ptr2[3] == '\0')
					{
						break;
					}
					ptr2 += 4;
				}
			}
			ptr2++;
			IL_0066:
			ptr2++;
			IL_006A:
			ptr2++;
			goto IL_006E;
		}

		/// <summary>Returns the <see cref="T:System.TypeCode" /> for class <see cref="T:System.String" />.</summary>
		/// <returns>The enumerated constant, <see cref="F:System.TypeCode.String" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000320 RID: 800 RVA: 0x00011EC0 File Offset: 0x000100C0
		public TypeCode GetTypeCode()
		{
			return TypeCode.String;
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToBoolean(System.IFormatProvider)" />. </summary>
		/// <returns>true if the value of the current string is <see cref="F:System.Boolean.TrueString" />; false if the value of the current string is <see cref="F:System.Boolean.FalseString" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		/// <exception cref="T:System.FormatException">The value of the current string is not <see cref="F:System.Boolean.TrueString" /> or <see cref="F:System.Boolean.FalseString" />.</exception>
		// Token: 0x06000321 RID: 801 RVA: 0x00011EC4 File Offset: 0x000100C4
		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			return Convert.ToBoolean(this, provider);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToChar(System.IFormatProvider)" />.</summary>
		/// <returns>The character at index 0 in the current <see cref="T:System.String" /> object.</returns>
		/// <param name="provider">An object that provides culture-specific formatting information. </param>
		// Token: 0x06000322 RID: 802 RVA: 0x00011ECD File Offset: 0x000100CD
		char IConvertible.ToChar(IFormatProvider provider)
		{
			return Convert.ToChar(this, provider);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToSByte(System.IFormatProvider)" />.</summary>
		/// <returns>The converted value of the current <see cref="T:System.String" /> object.</returns>
		/// <param name="provider">An object that provides culture-specific formatting information. </param>
		/// <exception cref="T:System.FormatException">The value of the current <see cref="T:System.String" /> object cannot be parsed. </exception>
		/// <exception cref="T:System.OverflowException">The value of the current <see cref="T:System.String" /> object is a number greater than <see cref="F:System.SByte.MaxValue" /> or less than <see cref="F:System.SByte.MinValue" />. </exception>
		// Token: 0x06000323 RID: 803 RVA: 0x00011ED6 File Offset: 0x000100D6
		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			return Convert.ToSByte(this, provider);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToByte(System.IFormatProvider)" />.</summary>
		/// <returns>The converted value of the current <see cref="T:System.String" /> object.</returns>
		/// <param name="provider">An object that provides culture-specific formatting information. </param>
		/// <exception cref="T:System.FormatException">The value of the current <see cref="T:System.String" /> object cannot be parsed. </exception>
		/// <exception cref="T:System.OverflowException">The value of the current <see cref="T:System.String" /> object is a number greater than <see cref="F:System.Byte.MaxValue" /> or less than <see cref="F:System.Byte.MinValue" />. </exception>
		// Token: 0x06000324 RID: 804 RVA: 0x00011EDF File Offset: 0x000100DF
		byte IConvertible.ToByte(IFormatProvider provider)
		{
			return Convert.ToByte(this, provider);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToInt16(System.IFormatProvider)" />.</summary>
		/// <returns>The converted value of the current <see cref="T:System.String" /> object.</returns>
		/// <param name="provider">An object that provides culture-specific formatting information. </param>
		/// <exception cref="T:System.FormatException">The value of the current <see cref="T:System.String" /> object cannot be parsed. </exception>
		/// <exception cref="T:System.OverflowException">The value of the current <see cref="T:System.String" /> object is a number greater than <see cref="F:System.Int16.MaxValue" /> or less than <see cref="F:System.Int16.MinValue" />.</exception>
		// Token: 0x06000325 RID: 805 RVA: 0x00011EE8 File Offset: 0x000100E8
		short IConvertible.ToInt16(IFormatProvider provider)
		{
			return Convert.ToInt16(this, provider);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToUInt16(System.IFormatProvider)" />.</summary>
		/// <returns>The converted value of the current <see cref="T:System.String" /> object.</returns>
		/// <param name="provider">An object that provides culture-specific formatting information. </param>
		/// <exception cref="T:System.FormatException">The value of the current <see cref="T:System.String" /> object cannot be parsed. </exception>
		/// <exception cref="T:System.OverflowException">The value of the current <see cref="T:System.String" /> object is a number greater than <see cref="F:System.UInt16.MaxValue" /> or less than <see cref="F:System.UInt16.MinValue" />.</exception>
		// Token: 0x06000326 RID: 806 RVA: 0x00011EF1 File Offset: 0x000100F1
		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			return Convert.ToUInt16(this, provider);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToInt32(System.IFormatProvider)" />.</summary>
		/// <returns>The converted value of the current <see cref="T:System.String" /> object.</returns>
		/// <param name="provider">An object that provides culture-specific formatting information. </param>
		// Token: 0x06000327 RID: 807 RVA: 0x00011EFA File Offset: 0x000100FA
		int IConvertible.ToInt32(IFormatProvider provider)
		{
			return Convert.ToInt32(this, provider);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToUInt32(System.IFormatProvider)" />.</summary>
		/// <returns>The converted value of the current <see cref="T:System.String" /> object.</returns>
		/// <param name="provider">An object that provides culture-specific formatting information. </param>
		/// <exception cref="T:System.FormatException">The value of the current <see cref="T:System.String" /> object cannot be parsed. </exception>
		/// <exception cref="T:System.OverflowException">The value of the current <see cref="T:System.String" /> object is a number greater <see cref="F:System.UInt32.MaxValue" /> or less than <see cref="F:System.UInt32.MinValue" /></exception>
		// Token: 0x06000328 RID: 808 RVA: 0x00011F03 File Offset: 0x00010103
		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			return Convert.ToUInt32(this, provider);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToInt64(System.IFormatProvider)" />.</summary>
		/// <returns>The converted value of the current <see cref="T:System.String" /> object.</returns>
		/// <param name="provider">An object that provides culture-specific formatting information. </param>
		// Token: 0x06000329 RID: 809 RVA: 0x00011F0C File Offset: 0x0001010C
		long IConvertible.ToInt64(IFormatProvider provider)
		{
			return Convert.ToInt64(this, provider);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToUInt64(System.IFormatProvider)" />.</summary>
		/// <returns>The converted value of the current <see cref="T:System.String" /> object.</returns>
		/// <param name="provider">An object that provides culture-specific formatting information. </param>
		// Token: 0x0600032A RID: 810 RVA: 0x00011F15 File Offset: 0x00010115
		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			return Convert.ToUInt64(this, provider);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToSingle(System.IFormatProvider)" />.</summary>
		/// <returns>The converted value of the current <see cref="T:System.String" /> object.</returns>
		/// <param name="provider">An object that provides culture-specific formatting information. </param>
		// Token: 0x0600032B RID: 811 RVA: 0x00011F1E File Offset: 0x0001011E
		float IConvertible.ToSingle(IFormatProvider provider)
		{
			return Convert.ToSingle(this, provider);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToDouble(System.IFormatProvider)" />.</summary>
		/// <returns>The converted value of the current <see cref="T:System.String" /> object.</returns>
		/// <param name="provider">An object that provides culture-specific formatting information. </param>
		/// <exception cref="T:System.FormatException">The value of the current <see cref="T:System.String" /> object cannot be parsed. </exception>
		/// <exception cref="T:System.OverflowException">The value of the current <see cref="T:System.String" /> object is a number less than <see cref="F:System.Double.MinValue" /> or greater than <see cref="F:System.Double.MaxValue" />. </exception>
		// Token: 0x0600032C RID: 812 RVA: 0x00011F27 File Offset: 0x00010127
		double IConvertible.ToDouble(IFormatProvider provider)
		{
			return Convert.ToDouble(this, provider);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToDecimal(System.IFormatProvider)" />.</summary>
		/// <returns>The converted value of the current <see cref="T:System.String" /> object.</returns>
		/// <param name="provider">An object that provides culture-specific formatting information. </param>
		/// <exception cref="T:System.FormatException">The value of the current <see cref="T:System.String" /> object cannot be parsed. </exception>
		/// <exception cref="T:System.OverflowException">The value of the current <see cref="T:System.String" /> object is a number less than <see cref="F:System.Decimal.MinValue" /> or than <see cref="F:System.Decimal.MaxValue" /> greater. </exception>
		// Token: 0x0600032D RID: 813 RVA: 0x00011F30 File Offset: 0x00010130
		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			return Convert.ToDecimal(this, provider);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToDateTime(System.IFormatProvider)" />.</summary>
		/// <returns>The converted value of the current <see cref="T:System.String" /> object.</returns>
		/// <param name="provider">An object that provides culture-specific formatting information. </param>
		// Token: 0x0600032E RID: 814 RVA: 0x00011F39 File Offset: 0x00010139
		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			return Convert.ToDateTime(this, provider);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToType(System.Type,System.IFormatProvider)" />.</summary>
		/// <returns>The converted value of the current <see cref="T:System.String" /> object.</returns>
		/// <param name="type">The type of the returned object. </param>
		/// <param name="provider">An object that provides culture-specific formatting information.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is null.</exception>
		/// <exception cref="T:System.InvalidCastException">The value of the current <see cref="T:System.String" /> object cannot be converted to the type specified by the <paramref name="type" /> parameter. </exception>
		// Token: 0x0600032F RID: 815 RVA: 0x00011F42 File Offset: 0x00010142
		object IConvertible.ToType(Type type, IFormatProvider provider)
		{
			return Convert.DefaultToType(this, type, provider);
		}

		/// <summary>Indicates whether this string is in Unicode normalization form C.</summary>
		/// <returns>true if this string is in normalization form C; otherwise, false.</returns>
		/// <exception cref="T:System.ArgumentException">The current instance contains invalid Unicode characters.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06000330 RID: 816 RVA: 0x00011F4C File Offset: 0x0001014C
		public bool IsNormalized()
		{
			return this.IsNormalized(NormalizationForm.FormC);
		}

		/// <summary>Indicates whether this string is in the specified Unicode normalization form.</summary>
		/// <returns>true if this string is in the normalization form specified by the <paramref name="normalizationForm" /> parameter; otherwise, false.</returns>
		/// <param name="normalizationForm">A Unicode normalization form. </param>
		/// <exception cref="T:System.ArgumentException">The current instance contains invalid Unicode characters.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06000331 RID: 817 RVA: 0x00011F55 File Offset: 0x00010155
		public bool IsNormalized(NormalizationForm normalizationForm)
		{
			return Normalization.IsNormalized(this, normalizationForm);
		}

		/// <summary>Returns a new string whose textual value is the same as this string, but whose binary representation is in Unicode normalization form C.</summary>
		/// <returns>A new, normalized string whose textual value is the same as this string, but whose binary representation is in normalization form C.</returns>
		/// <exception cref="T:System.ArgumentException">The current instance contains invalid Unicode characters.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06000332 RID: 818 RVA: 0x00011F5E File Offset: 0x0001015E
		public string Normalize()
		{
			return this.Normalize(NormalizationForm.FormC);
		}

		/// <summary>Returns a new string whose textual value is the same as this string, but whose binary representation is in the specified Unicode normalization form.</summary>
		/// <returns>A new string whose textual value is the same as this string, but whose binary representation is in the normalization form specified by the <paramref name="normalizationForm" /> parameter.</returns>
		/// <param name="normalizationForm">A Unicode normalization form. </param>
		/// <exception cref="T:System.ArgumentException">The current instance contains invalid Unicode characters.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06000333 RID: 819 RVA: 0x00011F67 File Offset: 0x00010167
		public string Normalize(NormalizationForm normalizationForm)
		{
			return Normalization.Normalize(this, normalizationForm);
		}

		// Token: 0x06000334 RID: 820 RVA: 0x00011F70 File Offset: 0x00010170
		private unsafe static void FillStringChecked(string dest, int destPos, string src)
		{
			if (src.Length > dest.Length - destPos)
			{
				throw new IndexOutOfRangeException();
			}
			fixed (char* ptr = &dest._firstChar)
			{
				char* ptr2 = ptr;
				fixed (char* ptr3 = &src._firstChar)
				{
					char* ptr4 = ptr3;
					string.wstrcpy(ptr2 + destPos, ptr4, src.Length);
				}
			}
		}

		/// <summary>Creates the string  representation of a specified object.</summary>
		/// <returns>The string representation of the value of <paramref name="arg0" />, or <see cref="F:System.String.Empty" /> if <paramref name="arg0" /> is null.</returns>
		/// <param name="arg0">The object to represent, or null. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000335 RID: 821 RVA: 0x00011FBD File Offset: 0x000101BD
		public static string Concat(object arg0)
		{
			if (arg0 == null)
			{
				return string.Empty;
			}
			return arg0.ToString();
		}

		/// <summary>Concatenates the string representations of two specified objects.</summary>
		/// <returns>The concatenated string representations of the values of <paramref name="arg0" /> and <paramref name="arg1" />.</returns>
		/// <param name="arg0">The first object to concatenate. </param>
		/// <param name="arg1">The second object to concatenate. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000336 RID: 822 RVA: 0x00011FCE File Offset: 0x000101CE
		public static string Concat(object arg0, object arg1)
		{
			if (arg0 == null)
			{
				arg0 = string.Empty;
			}
			if (arg1 == null)
			{
				arg1 = string.Empty;
			}
			return arg0.ToString() + arg1.ToString();
		}

		/// <summary>Concatenates the string representations of three specified objects.</summary>
		/// <returns>The concatenated string representations of the values of <paramref name="arg0" />, <paramref name="arg1" />, and <paramref name="arg2" />.</returns>
		/// <param name="arg0">The first object to concatenate. </param>
		/// <param name="arg1">The second object to concatenate. </param>
		/// <param name="arg2">The third object to concatenate. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000337 RID: 823 RVA: 0x00011FF5 File Offset: 0x000101F5
		public static string Concat(object arg0, object arg1, object arg2)
		{
			if (arg0 == null)
			{
				arg0 = string.Empty;
			}
			if (arg1 == null)
			{
				arg1 = string.Empty;
			}
			if (arg2 == null)
			{
				arg2 = string.Empty;
			}
			return arg0.ToString() + arg1.ToString() + arg2.ToString();
		}

		/// <summary>Concatenates the string representations of the elements in a specified <see cref="T:System.Object" /> array.</summary>
		/// <returns>The concatenated string representations of the values of the elements in <paramref name="args" />.</returns>
		/// <param name="args">An object array that contains the elements to concatenate. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="args" /> is null. </exception>
		/// <exception cref="T:System.OutOfMemoryException">Out of memory.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000338 RID: 824 RVA: 0x0001202C File Offset: 0x0001022C
		public static string Concat(params object[] args)
		{
			if (args == null)
			{
				throw new ArgumentNullException("args");
			}
			if (args.Length <= 1)
			{
				string text;
				if (args.Length != 0)
				{
					object obj = args[0];
					if ((text = ((obj != null) ? obj.ToString() : null)) == null)
					{
						return string.Empty;
					}
				}
				else
				{
					text = string.Empty;
				}
				return text;
			}
			string[] array = new string[args.Length];
			int num = 0;
			for (int i = 0; i < args.Length; i++)
			{
				object obj2 = args[i];
				string text2 = ((obj2 != null) ? obj2.ToString() : null) ?? string.Empty;
				array[i] = text2;
				num += text2.Length;
				if (num < 0)
				{
					throw new OutOfMemoryException();
				}
			}
			if (num == 0)
			{
				return string.Empty;
			}
			string text3 = string.FastAllocateString(num);
			int num2 = 0;
			foreach (string text4 in array)
			{
				string.FillStringChecked(text3, num2, text4);
				num2 += text4.Length;
			}
			return text3;
		}

		/// <summary>Concatenates the members of an <see cref="T:System.Collections.Generic.IEnumerable`1" /> implementation.</summary>
		/// <returns>The concatenated members in <paramref name="values" />.</returns>
		/// <param name="values">A collection object that implements the <see cref="T:System.Collections.Generic.IEnumerable`1" /> interface.</param>
		/// <typeparam name="T">The type of the members of <paramref name="values" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="values" /> is null. </exception>
		// Token: 0x06000339 RID: 825 RVA: 0x00012100 File Offset: 0x00010300
		public static string Concat<T>(IEnumerable<T> values)
		{
			if (values == null)
			{
				throw new ArgumentNullException("values");
			}
			if (typeof(T) == typeof(char))
			{
				using (IEnumerator<char> enumerator = Unsafe.As<IEnumerable<char>>(values).GetEnumerator())
				{
					if (!enumerator.MoveNext())
					{
						return string.Empty;
					}
					char c = enumerator.Current;
					if (!enumerator.MoveNext())
					{
						return string.CreateFromChar(c);
					}
					StringBuilder stringBuilder = StringBuilderCache.Acquire(16);
					stringBuilder.Append(c);
					do
					{
						c = enumerator.Current;
						stringBuilder.Append(c);
					}
					while (enumerator.MoveNext());
					return StringBuilderCache.GetStringAndRelease(stringBuilder);
				}
			}
			string text;
			using (IEnumerator<T> enumerator2 = values.GetEnumerator())
			{
				if (!enumerator2.MoveNext())
				{
					text = string.Empty;
				}
				else
				{
					T t = enumerator2.Current;
					string text2 = ((t != null) ? t.ToString() : null);
					if (!enumerator2.MoveNext())
					{
						text = text2 ?? string.Empty;
					}
					else
					{
						StringBuilder stringBuilder2 = StringBuilderCache.Acquire(16);
						stringBuilder2.Append(text2);
						do
						{
							t = enumerator2.Current;
							if (t != null)
							{
								stringBuilder2.Append(t.ToString());
							}
						}
						while (enumerator2.MoveNext());
						text = StringBuilderCache.GetStringAndRelease(stringBuilder2);
					}
				}
			}
			return text;
		}

		/// <summary>Concatenates the members of a constructed <see cref="T:System.Collections.Generic.IEnumerable`1" /> collection of type <see cref="T:System.String" />.</summary>
		/// <returns>The concatenated strings in <paramref name="values" />.</returns>
		/// <param name="values">A collection object that implements <see cref="T:System.Collections.Generic.IEnumerable`1" /> and whose generic type argument is <see cref="T:System.String" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="values" /> is null. </exception>
		// Token: 0x0600033A RID: 826 RVA: 0x0001227C File Offset: 0x0001047C
		public static string Concat(IEnumerable<string> values)
		{
			if (values == null)
			{
				throw new ArgumentNullException("values");
			}
			string text;
			using (IEnumerator<string> enumerator = values.GetEnumerator())
			{
				if (!enumerator.MoveNext())
				{
					text = string.Empty;
				}
				else
				{
					string text2 = enumerator.Current;
					if (!enumerator.MoveNext())
					{
						text = text2 ?? string.Empty;
					}
					else
					{
						StringBuilder stringBuilder = StringBuilderCache.Acquire(16);
						stringBuilder.Append(text2);
						do
						{
							stringBuilder.Append(enumerator.Current);
						}
						while (enumerator.MoveNext());
						text = StringBuilderCache.GetStringAndRelease(stringBuilder);
					}
				}
			}
			return text;
		}

		/// <summary>Concatenates two specified instances of <see cref="T:System.String" />.</summary>
		/// <returns>The concatenation of <paramref name="str0" /> and <paramref name="str1" />.</returns>
		/// <param name="str0">The first string to concatenate. </param>
		/// <param name="str1">The second string to concatenate. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600033B RID: 827 RVA: 0x00012314 File Offset: 0x00010514
		public static string Concat(string str0, string str1)
		{
			if (string.IsNullOrEmpty(str0))
			{
				if (string.IsNullOrEmpty(str1))
				{
					return string.Empty;
				}
				return str1;
			}
			else
			{
				if (string.IsNullOrEmpty(str1))
				{
					return str0;
				}
				int length = str0.Length;
				string text = string.FastAllocateString(length + str1.Length);
				string.FillStringChecked(text, 0, str0);
				string.FillStringChecked(text, length, str1);
				return text;
			}
		}

		/// <summary>Concatenates three specified instances of <see cref="T:System.String" />.</summary>
		/// <returns>The concatenation of <paramref name="str0" />, <paramref name="str1" />, and <paramref name="str2" />.</returns>
		/// <param name="str0">The first string to concatenate. </param>
		/// <param name="str1">The second string to concatenate. </param>
		/// <param name="str2">The third string to concatenate. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600033C RID: 828 RVA: 0x00012368 File Offset: 0x00010568
		public static string Concat(string str0, string str1, string str2)
		{
			if (string.IsNullOrEmpty(str0))
			{
				return str1 + str2;
			}
			if (string.IsNullOrEmpty(str1))
			{
				return str0 + str2;
			}
			if (string.IsNullOrEmpty(str2))
			{
				return str0 + str1;
			}
			string text = string.FastAllocateString(str0.Length + str1.Length + str2.Length);
			string.FillStringChecked(text, 0, str0);
			string.FillStringChecked(text, str0.Length, str1);
			string.FillStringChecked(text, str0.Length + str1.Length, str2);
			return text;
		}

		/// <summary>Concatenates four specified instances of <see cref="T:System.String" />.</summary>
		/// <returns>The concatenation of <paramref name="str0" />, <paramref name="str1" />, <paramref name="str2" />, and <paramref name="str3" />.</returns>
		/// <param name="str0">The first string to concatenate. </param>
		/// <param name="str1">The second string to concatenate. </param>
		/// <param name="str2">The third string to concatenate. </param>
		/// <param name="str3">The fourth string to concatenate. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600033D RID: 829 RVA: 0x000123E8 File Offset: 0x000105E8
		public static string Concat(string str0, string str1, string str2, string str3)
		{
			if (string.IsNullOrEmpty(str0))
			{
				return str1 + str2 + str3;
			}
			if (string.IsNullOrEmpty(str1))
			{
				return str0 + str2 + str3;
			}
			if (string.IsNullOrEmpty(str2))
			{
				return str0 + str1 + str3;
			}
			if (string.IsNullOrEmpty(str3))
			{
				return str0 + str1 + str2;
			}
			string text = string.FastAllocateString(str0.Length + str1.Length + str2.Length + str3.Length);
			string.FillStringChecked(text, 0, str0);
			string.FillStringChecked(text, str0.Length, str1);
			string.FillStringChecked(text, str0.Length + str1.Length, str2);
			string.FillStringChecked(text, str0.Length + str1.Length + str2.Length, str3);
			return text;
		}

		/// <summary>Concatenates the elements of a specified <see cref="T:System.String" /> array.</summary>
		/// <returns>The concatenated elements of <paramref name="values" />.</returns>
		/// <param name="values">An array of string instances. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="values" /> is null. </exception>
		/// <exception cref="T:System.OutOfMemoryException">Out of memory.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600033E RID: 830 RVA: 0x000124A0 File Offset: 0x000106A0
		public static string Concat(params string[] values)
		{
			if (values == null)
			{
				throw new ArgumentNullException("values");
			}
			if (values.Length <= 1)
			{
				string text;
				if (values.Length != 0)
				{
					if ((text = values[0]) == null)
					{
						return string.Empty;
					}
				}
				else
				{
					text = string.Empty;
				}
				return text;
			}
			long num = 0L;
			foreach (string text2 in values)
			{
				if (text2 != null)
				{
					num += (long)text2.Length;
				}
			}
			if (num > 2147483647L)
			{
				throw new OutOfMemoryException();
			}
			int num2 = (int)num;
			if (num2 == 0)
			{
				return string.Empty;
			}
			string text3 = string.FastAllocateString(num2);
			int num3 = 0;
			foreach (string text4 in values)
			{
				if (!string.IsNullOrEmpty(text4))
				{
					int length = text4.Length;
					if (length > num2 - num3)
					{
						num3 = -1;
						break;
					}
					string.FillStringChecked(text3, num3, text4);
					num3 += length;
				}
			}
			if (num3 != num2)
			{
				return string.Concat((string[])values.Clone());
			}
			return text3;
		}

		/// <summary>Replaces one or more format items in a specified string with the string representation of a specified object.</summary>
		/// <returns>A copy of <paramref name="format" /> in which any format items are replaced by the string representation of <paramref name="arg0" />.</returns>
		/// <param name="format">A composite format string. </param>
		/// <param name="arg0">The object to format. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> is null. </exception>
		/// <exception cref="T:System.FormatException">The format item in <paramref name="format" /> is invalid.-or- The index of a format item is not zero. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600033F RID: 831 RVA: 0x0001257F File Offset: 0x0001077F
		public static string Format(string format, object arg0)
		{
			return string.FormatHelper(null, format, new ParamsArray(arg0));
		}

		/// <summary>Replaces the format items in a specified string with the string representation of two specified objects.</summary>
		/// <returns>A copy of <paramref name="format" /> in which format items are replaced by the string representations of <paramref name="arg0" /> and <paramref name="arg1" />.</returns>
		/// <param name="format">A composite format string. </param>
		/// <param name="arg0">The first object to format. </param>
		/// <param name="arg1">The second object to format. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> is null. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="format" /> is invalid.-or- The index of a format item is not zero or one. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000340 RID: 832 RVA: 0x0001258E File Offset: 0x0001078E
		public static string Format(string format, object arg0, object arg1)
		{
			return string.FormatHelper(null, format, new ParamsArray(arg0, arg1));
		}

		/// <summary>Replaces the format items in a specified string with the string representation of three specified objects.</summary>
		/// <returns>A copy of <paramref name="format" /> in which the format items have been replaced by the string representations of <paramref name="arg0" />, <paramref name="arg1" />, and <paramref name="arg2" />.</returns>
		/// <param name="format">A composite format string.</param>
		/// <param name="arg0">The first object to format. </param>
		/// <param name="arg1">The second object to format. </param>
		/// <param name="arg2">The third object to format. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> is null. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="format" /> is invalid.-or- The index of a format item is less than zero, or greater than two. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000341 RID: 833 RVA: 0x0001259E File Offset: 0x0001079E
		public static string Format(string format, object arg0, object arg1, object arg2)
		{
			return string.FormatHelper(null, format, new ParamsArray(arg0, arg1, arg2));
		}

		/// <summary>Replaces the format item in a specified string with the string representation of a corresponding object in a specified array.</summary>
		/// <returns>A copy of <paramref name="format" /> in which the format items have been replaced by the string representation of the corresponding objects in <paramref name="args" />.</returns>
		/// <param name="format">A composite format string.</param>
		/// <param name="args">An object array that contains zero or more objects to format. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> or <paramref name="args" /> is null. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="format" /> is invalid.-or- The index of a format item is less than zero, or greater than or equal to the length of the <paramref name="args" /> array. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000342 RID: 834 RVA: 0x000125AF File Offset: 0x000107AF
		public static string Format(string format, params object[] args)
		{
			if (args == null)
			{
				throw new ArgumentNullException((format == null) ? "format" : "args");
			}
			return string.FormatHelper(null, format, new ParamsArray(args));
		}

		// Token: 0x06000343 RID: 835 RVA: 0x000125D6 File Offset: 0x000107D6
		public static string Format(IFormatProvider provider, string format, object arg0)
		{
			return string.FormatHelper(provider, format, new ParamsArray(arg0));
		}

		// Token: 0x06000344 RID: 836 RVA: 0x000125E5 File Offset: 0x000107E5
		public static string Format(IFormatProvider provider, string format, object arg0, object arg1)
		{
			return string.FormatHelper(provider, format, new ParamsArray(arg0, arg1));
		}

		// Token: 0x06000345 RID: 837 RVA: 0x000125F5 File Offset: 0x000107F5
		public static string Format(IFormatProvider provider, string format, object arg0, object arg1, object arg2)
		{
			return string.FormatHelper(provider, format, new ParamsArray(arg0, arg1, arg2));
		}

		/// <summary>Replaces the format items in a specified string with the string representations of corresponding objects in a specified array. A parameter supplies culture-specific formatting information.</summary>
		/// <returns>A copy of <paramref name="format" /> in which the format items have been replaced by the string representation of the corresponding objects in <paramref name="args" />.</returns>
		/// <param name="provider">An object that supplies culture-specific formatting information. </param>
		/// <param name="format">A composite format string. </param>
		/// <param name="args">An object array that contains zero or more objects to format. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> or <paramref name="args" /> is null. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="format" /> is invalid.-or- The index of a format item is less than zero, or greater than or equal to the length of the <paramref name="args" /> array. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000346 RID: 838 RVA: 0x00012607 File Offset: 0x00010807
		public static string Format(IFormatProvider provider, string format, params object[] args)
		{
			if (args == null)
			{
				throw new ArgumentNullException((format == null) ? "format" : "args");
			}
			return string.FormatHelper(provider, format, new ParamsArray(args));
		}

		// Token: 0x06000347 RID: 839 RVA: 0x0001262E File Offset: 0x0001082E
		private static string FormatHelper(IFormatProvider provider, string format, ParamsArray args)
		{
			if (format == null)
			{
				throw new ArgumentNullException("format");
			}
			return StringBuilderCache.GetStringAndRelease(StringBuilderCache.Acquire(format.Length + args.Length * 8).AppendFormatHelper(provider, format, args));
		}

		/// <summary>Returns a new string in which a specified string is inserted at a specified index position in this instance. </summary>
		/// <returns>A new string that is equivalent to this instance, but with <paramref name="value" /> inserted at position <paramref name="startIndex" />.</returns>
		/// <param name="startIndex">The zero-based index position of the insertion. </param>
		/// <param name="value">The string to insert. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is negative or greater than the length of this instance. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000348 RID: 840 RVA: 0x00012660 File Offset: 0x00010860
		public unsafe string Insert(int startIndex, string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (startIndex < 0 || startIndex > this.Length)
			{
				throw new ArgumentOutOfRangeException("startIndex");
			}
			int length = this.Length;
			int length2 = value.Length;
			if (length == 0)
			{
				return value;
			}
			if (length2 == 0)
			{
				return this;
			}
			string text = string.FastAllocateString(length + length2);
			fixed (char* ptr = &this._firstChar)
			{
				char* ptr2 = ptr;
				fixed (char* ptr3 = &value._firstChar)
				{
					char* ptr4 = ptr3;
					fixed (char* ptr5 = &text._firstChar)
					{
						char* ptr6 = ptr5;
						string.wstrcpy(ptr6, ptr2, startIndex);
						string.wstrcpy(ptr6 + startIndex, ptr4, length2);
						string.wstrcpy(ptr6 + startIndex + length2, ptr2 + startIndex, length - startIndex);
					}
				}
			}
			return text;
		}

		// Token: 0x06000349 RID: 841 RVA: 0x0001270E File Offset: 0x0001090E
		public static string Join(char separator, params string[] value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return string.Join(separator, value, 0, value.Length);
		}

		// Token: 0x0600034A RID: 842 RVA: 0x00012729 File Offset: 0x00010929
		public unsafe static string Join(char separator, params object[] values)
		{
			return string.JoinCore(&separator, 1, values);
		}

		// Token: 0x0600034B RID: 843 RVA: 0x00012735 File Offset: 0x00010935
		public unsafe static string Join<T>(char separator, IEnumerable<T> values)
		{
			return string.JoinCore<T>(&separator, 1, values);
		}

		// Token: 0x0600034C RID: 844 RVA: 0x00012741 File Offset: 0x00010941
		public unsafe static string Join(char separator, string[] value, int startIndex, int count)
		{
			return string.JoinCore(&separator, 1, value, startIndex, count);
		}

		/// <summary>Concatenates all the elements of a string array, using the specified separator between each element. </summary>
		/// <returns>A string that consists of the elements in <paramref name="value" /> delimited by the <paramref name="separator" /> string. If <paramref name="value" /> is an empty array, the method returns <see cref="F:System.String.Empty" />.</returns>
		/// <param name="separator">The string to use as a separator. <paramref name="separator" /> is included in the returned string only if <paramref name="value" /> has more than one element.</param>
		/// <param name="value">An array that contains the elements to concatenate. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600034D RID: 845 RVA: 0x0001274F File Offset: 0x0001094F
		public static string Join(string separator, params string[] value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return string.Join(separator, value, 0, value.Length);
		}

		/// <summary>Concatenates the elements of an object array, using the specified separator between each element.</summary>
		/// <returns>A string that consists of the elements of <paramref name="values" /> delimited by the <paramref name="separator" /> string. If <paramref name="values" /> is an empty array, the method returns <see cref="F:System.String.Empty" />.</returns>
		/// <param name="separator">The string to use as a separator. <paramref name="separator" /> is included in the returned string only if <paramref name="values" /> has more than one element.</param>
		/// <param name="values">An array that contains the elements to concatenate.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="values" /> is null. </exception>
		// Token: 0x0600034E RID: 846 RVA: 0x0001276C File Offset: 0x0001096C
		public unsafe static string Join(string separator, params object[] values)
		{
			separator = separator ?? string.Empty;
			fixed (char* ptr = &separator._firstChar)
			{
				return string.JoinCore(ptr, separator.Length, values);
			}
		}

		/// <summary>Concatenates the members of a collection, using the specified separator between each member.</summary>
		/// <returns>A string that consists of the members of <paramref name="values" /> delimited by the <paramref name="separator" /> string. If <paramref name="values" /> has no members, the method returns <see cref="F:System.String.Empty" />.</returns>
		/// <param name="separator">The string to use as a separator. <paramref name="separator" /> is included in the returned string only if <paramref name="values" /> has more than one element.</param>
		/// <param name="values">A collection that contains the objects to concatenate.</param>
		/// <typeparam name="T">The type of the members of <paramref name="values" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="values" /> is null. </exception>
		// Token: 0x0600034F RID: 847 RVA: 0x0001279C File Offset: 0x0001099C
		public unsafe static string Join<T>(string separator, IEnumerable<T> values)
		{
			separator = separator ?? string.Empty;
			fixed (char* ptr = &separator._firstChar)
			{
				return string.JoinCore<T>(ptr, separator.Length, values);
			}
		}

		/// <summary>Concatenates the members of a constructed <see cref="T:System.Collections.Generic.IEnumerable`1" /> collection of type <see cref="T:System.String" />, using the specified separator between each member.</summary>
		/// <returns>A string that consists of the members of <paramref name="values" /> delimited by the <paramref name="separator" /> string. If <paramref name="values" /> has no members, the method returns <see cref="F:System.String.Empty" />.</returns>
		/// <param name="separator">The string to use as a separator. <paramref name="separator" /> is included in the returned string only if <paramref name="values" /> has more than one element.</param>
		/// <param name="values">A collection that contains the strings to concatenate.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="values" /> is null. </exception>
		// Token: 0x06000350 RID: 848 RVA: 0x000127CC File Offset: 0x000109CC
		public static string Join(string separator, IEnumerable<string> values)
		{
			if (values == null)
			{
				throw new ArgumentNullException("values");
			}
			string text;
			using (IEnumerator<string> enumerator = values.GetEnumerator())
			{
				if (!enumerator.MoveNext())
				{
					text = string.Empty;
				}
				else
				{
					string text2 = enumerator.Current;
					if (!enumerator.MoveNext())
					{
						text = text2 ?? string.Empty;
					}
					else
					{
						StringBuilder stringBuilder = StringBuilderCache.Acquire(16);
						stringBuilder.Append(text2);
						do
						{
							stringBuilder.Append(separator);
							stringBuilder.Append(enumerator.Current);
						}
						while (enumerator.MoveNext());
						text = StringBuilderCache.GetStringAndRelease(stringBuilder);
					}
				}
			}
			return text;
		}

		/// <summary>Concatenates the specified elements of a string array, using the specified separator between each element. </summary>
		/// <returns>A string that consists of the strings in <paramref name="value" /> delimited by the <paramref name="separator" /> string. -or-<see cref="F:System.String.Empty" /> if <paramref name="count" /> is zero, <paramref name="value" /> has no elements, or <paramref name="separator" /> and all the elements of <paramref name="value" /> are <see cref="F:System.String.Empty" />.</returns>
		/// <param name="separator">The string to use as a separator. <paramref name="separator" /> is included in the returned string only if <paramref name="value" /> has more than one element.</param>
		/// <param name="value">An array that contains the elements to concatenate. </param>
		/// <param name="startIndex">The first element in <paramref name="value" /> to use. </param>
		/// <param name="count">The number of elements of <paramref name="value" /> to use. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> or <paramref name="count" /> is less than 0.-or- <paramref name="startIndex" /> plus <paramref name="count" /> is greater than the number of elements in <paramref name="value" />. </exception>
		/// <exception cref="T:System.OutOfMemoryException">Out of memory.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000351 RID: 849 RVA: 0x0001286C File Offset: 0x00010A6C
		public unsafe static string Join(string separator, string[] value, int startIndex, int count)
		{
			separator = separator ?? string.Empty;
			fixed (char* ptr = &separator._firstChar)
			{
				return string.JoinCore(ptr, separator.Length, value, startIndex, count);
			}
		}

		// Token: 0x06000352 RID: 850 RVA: 0x0001289C File Offset: 0x00010A9C
		private unsafe static string JoinCore(char* separator, int separatorLength, object[] values)
		{
			if (values == null)
			{
				throw new ArgumentNullException("values");
			}
			if (values.Length == 0)
			{
				return string.Empty;
			}
			object obj = values[0];
			string text = ((obj != null) ? obj.ToString() : null);
			if (values.Length == 1)
			{
				return text ?? string.Empty;
			}
			StringBuilder stringBuilder = StringBuilderCache.Acquire(16);
			stringBuilder.Append(text);
			for (int i = 1; i < values.Length; i++)
			{
				stringBuilder.Append(separator, separatorLength);
				object obj2 = values[i];
				if (obj2 != null)
				{
					stringBuilder.Append(obj2.ToString());
				}
			}
			return StringBuilderCache.GetStringAndRelease(stringBuilder);
		}

		// Token: 0x06000353 RID: 851 RVA: 0x00012924 File Offset: 0x00010B24
		private unsafe static string JoinCore<T>(char* separator, int separatorLength, IEnumerable<T> values)
		{
			if (values == null)
			{
				throw new ArgumentNullException("values");
			}
			string text;
			using (IEnumerator<T> enumerator = values.GetEnumerator())
			{
				if (!enumerator.MoveNext())
				{
					text = string.Empty;
				}
				else
				{
					T t = enumerator.Current;
					string text2 = ((t != null) ? t.ToString() : null);
					if (!enumerator.MoveNext())
					{
						text = text2 ?? string.Empty;
					}
					else
					{
						StringBuilder stringBuilder = StringBuilderCache.Acquire(16);
						stringBuilder.Append(text2);
						do
						{
							t = enumerator.Current;
							stringBuilder.Append(separator, separatorLength);
							if (t != null)
							{
								stringBuilder.Append(t.ToString());
							}
						}
						while (enumerator.MoveNext());
						text = StringBuilderCache.GetStringAndRelease(stringBuilder);
					}
				}
			}
			return text;
		}

		// Token: 0x06000354 RID: 852 RVA: 0x000129FC File Offset: 0x00010BFC
		private unsafe static string JoinCore(char* separator, int separatorLength, string[] value, int startIndex, int count)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (startIndex < 0)
			{
				throw new ArgumentOutOfRangeException("startIndex", "StartIndex cannot be less than zero.");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Count cannot be less than zero.");
			}
			if (startIndex > value.Length - count)
			{
				throw new ArgumentOutOfRangeException("startIndex", "Index and count must refer to a location within the buffer.");
			}
			if (count <= 1)
			{
				string text;
				if (count != 0)
				{
					if ((text = value[startIndex]) == null)
					{
						return string.Empty;
					}
				}
				else
				{
					text = string.Empty;
				}
				return text;
			}
			long num = (long)(count - 1) * (long)separatorLength;
			if (num > 2147483647L)
			{
				throw new OutOfMemoryException();
			}
			int num2 = (int)num;
			int i = startIndex;
			int num3 = startIndex + count;
			while (i < num3)
			{
				string text2 = value[i];
				if (text2 != null)
				{
					num2 += text2.Length;
					if (num2 < 0)
					{
						throw new OutOfMemoryException();
					}
				}
				i++;
			}
			string text3 = string.FastAllocateString(num2);
			int num4 = 0;
			int j = startIndex;
			int num5 = startIndex + count;
			while (j < num5)
			{
				string text4 = value[j];
				if (text4 != null)
				{
					int length = text4.Length;
					if (length > num2 - num4)
					{
						num4 = -1;
						break;
					}
					string.FillStringChecked(text3, num4, text4);
					num4 += length;
				}
				if (j < num5 - 1)
				{
					fixed (char* ptr = &text3._firstChar)
					{
						char* ptr2 = ptr;
						if (separatorLength == 1)
						{
							ptr2[num4] = *separator;
						}
						else
						{
							string.wstrcpy(ptr2 + num4, separator, separatorLength);
						}
					}
					num4 += separatorLength;
				}
				j++;
			}
			if (num4 != num2)
			{
				return string.JoinCore(separator, separatorLength, (string[])value.Clone(), startIndex, count);
			}
			return text3;
		}

		/// <summary>Returns a new string that right-aligns the characters in this instance by padding them with spaces on the left, for a specified total length.</summary>
		/// <returns>A new string that is equivalent to this instance, but right-aligned and padded on the left with as many spaces as needed to create a length of <paramref name="totalWidth" />. However, if <paramref name="totalWidth" /> is less than the length of this instance, the method returns a reference to the existing instance. If <paramref name="totalWidth" /> is equal to the length of this instance, the method returns a new string that is identical to this instance.</returns>
		/// <param name="totalWidth">The number of characters in the resulting string, equal to the number of original characters plus any additional padding characters. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="totalWidth" /> is less than zero. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000355 RID: 853 RVA: 0x00012B60 File Offset: 0x00010D60
		public string PadLeft(int totalWidth)
		{
			return this.PadLeft(totalWidth, ' ');
		}

		/// <summary>Returns a new string that right-aligns the characters in this instance by padding them on the left with a specified Unicode character, for a specified total length.</summary>
		/// <returns>A new string that is equivalent to this instance, but right-aligned and padded on the left with as many <paramref name="paddingChar" /> characters as needed to create a length of <paramref name="totalWidth" />. However, if <paramref name="totalWidth" /> is less than the length of this instance, the method returns a reference to the existing instance. If <paramref name="totalWidth" /> is equal to the length of this instance, the method returns a new string that is identical to this instance.</returns>
		/// <param name="totalWidth">The number of characters in the resulting string, equal to the number of original characters plus any additional padding characters. </param>
		/// <param name="paddingChar">A Unicode padding character. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="totalWidth" /> is less than zero. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000356 RID: 854 RVA: 0x00012B6C File Offset: 0x00010D6C
		public unsafe string PadLeft(int totalWidth, char paddingChar)
		{
			if (totalWidth < 0)
			{
				throw new ArgumentOutOfRangeException("totalWidth", "Non-negative number required.");
			}
			int length = this.Length;
			int num = totalWidth - length;
			if (num <= 0)
			{
				return this;
			}
			string text = string.FastAllocateString(totalWidth);
			fixed (char* ptr = &text._firstChar)
			{
				char* ptr2 = ptr;
				for (int i = 0; i < num; i++)
				{
					ptr2[i] = paddingChar;
				}
				fixed (char* ptr3 = &this._firstChar)
				{
					char* ptr4 = ptr3;
					string.wstrcpy(ptr2 + num, ptr4, length);
				}
			}
			return text;
		}

		/// <summary>Returns a new string that left-aligns the characters in this string by padding them with spaces on the right, for a specified total length.</summary>
		/// <returns>A new string that is equivalent to this instance, but left-aligned and padded on the right with as many spaces as needed to create a length of <paramref name="totalWidth" />. However, if <paramref name="totalWidth" /> is less than the length of this instance, the method returns a reference to the existing instance. If <paramref name="totalWidth" /> is equal to the length of this instance, the method returns a new string that is identical to this instance.</returns>
		/// <param name="totalWidth">The number of characters in the resulting string, equal to the number of original characters plus any additional padding characters. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="totalWidth" /> is less than zero. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000357 RID: 855 RVA: 0x00012BEE File Offset: 0x00010DEE
		public string PadRight(int totalWidth)
		{
			return this.PadRight(totalWidth, ' ');
		}

		/// <summary>Returns a new string that left-aligns the characters in this string by padding them on the right with a specified Unicode character, for a specified total length.</summary>
		/// <returns>A new string that is equivalent to this instance, but left-aligned and padded on the right with as many <paramref name="paddingChar" /> characters as needed to create a length of <paramref name="totalWidth" />.  However, if <paramref name="totalWidth" /> is less than the length of this instance, the method returns a reference to the existing instance. If <paramref name="totalWidth" /> is equal to the length of this instance, the method returns a new string that is identical to this instance.</returns>
		/// <param name="totalWidth">The number of characters in the resulting string, equal to the number of original characters plus any additional padding characters. </param>
		/// <param name="paddingChar">A Unicode padding character. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="totalWidth" /> is less than zero. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000358 RID: 856 RVA: 0x00012BFC File Offset: 0x00010DFC
		public unsafe string PadRight(int totalWidth, char paddingChar)
		{
			if (totalWidth < 0)
			{
				throw new ArgumentOutOfRangeException("totalWidth", "Non-negative number required.");
			}
			int length = this.Length;
			int num = totalWidth - length;
			if (num <= 0)
			{
				return this;
			}
			string text = string.FastAllocateString(totalWidth);
			fixed (char* ptr = &text._firstChar)
			{
				char* ptr2 = ptr;
				fixed (char* ptr3 = &this._firstChar)
				{
					char* ptr4 = ptr3;
					string.wstrcpy(ptr2, ptr4, length);
				}
				for (int i = 0; i < num; i++)
				{
					ptr2[length + i] = paddingChar;
				}
			}
			return text;
		}

		/// <summary>Returns a new string in which a specified number of characters in the current this instance beginning at a specified position have been deleted.</summary>
		/// <returns>A new string that is equivalent to this instance except for the removed characters.</returns>
		/// <param name="startIndex">The zero-based position to begin deleting characters. </param>
		/// <param name="count">The number of characters to delete. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Either <paramref name="startIndex" /> or <paramref name="count" /> is less than zero.-or- <paramref name="startIndex" /> plus <paramref name="count" /> specify a position outside this instance. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000359 RID: 857 RVA: 0x00012C7C File Offset: 0x00010E7C
		public unsafe string Remove(int startIndex, int count)
		{
			if (startIndex < 0)
			{
				throw new ArgumentOutOfRangeException("startIndex", "StartIndex cannot be less than zero.");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Count cannot be less than zero.");
			}
			int length = this.Length;
			if (count > length - startIndex)
			{
				throw new ArgumentOutOfRangeException("count", "Index and count must refer to a location within the string.");
			}
			if (count == 0)
			{
				return this;
			}
			int num = length - count;
			if (num == 0)
			{
				return string.Empty;
			}
			string text = string.FastAllocateString(num);
			fixed (char* ptr = &this._firstChar)
			{
				char* ptr2 = ptr;
				fixed (char* ptr3 = &text._firstChar)
				{
					char* ptr4 = ptr3;
					string.wstrcpy(ptr4, ptr2, startIndex);
					string.wstrcpy(ptr4 + startIndex, ptr2 + startIndex + count, num - startIndex);
				}
			}
			return text;
		}

		/// <summary>Returns a new string in which all the characters in the current instance, beginning at a specified position and continuing through the last position, have been deleted.</summary>
		/// <returns>A new string that is equivalent to this string except for the removed characters.</returns>
		/// <param name="startIndex">The zero-based position to begin deleting characters. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is less than zero.-or- <paramref name="startIndex" /> specifies a position that is not within this string. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600035A RID: 858 RVA: 0x00012D22 File Offset: 0x00010F22
		public string Remove(int startIndex)
		{
			if (startIndex < 0)
			{
				throw new ArgumentOutOfRangeException("startIndex", "StartIndex cannot be less than zero.");
			}
			if (startIndex >= this.Length)
			{
				throw new ArgumentOutOfRangeException("startIndex", "startIndex must be less than length of string.");
			}
			return this.Substring(0, startIndex);
		}

		// Token: 0x0600035B RID: 859 RVA: 0x00012D59 File Offset: 0x00010F59
		public string Replace(string oldValue, string newValue, bool ignoreCase, CultureInfo culture)
		{
			return this.ReplaceCore(oldValue, newValue, culture, ignoreCase ? CompareOptions.IgnoreCase : CompareOptions.None);
		}

		// Token: 0x0600035C RID: 860 RVA: 0x00012D6C File Offset: 0x00010F6C
		public string Replace(string oldValue, string newValue, StringComparison comparisonType)
		{
			switch (comparisonType)
			{
			case StringComparison.CurrentCulture:
				return this.ReplaceCore(oldValue, newValue, CultureInfo.CurrentCulture, CompareOptions.None);
			case StringComparison.CurrentCultureIgnoreCase:
				return this.ReplaceCore(oldValue, newValue, CultureInfo.CurrentCulture, CompareOptions.IgnoreCase);
			case StringComparison.InvariantCulture:
				return this.ReplaceCore(oldValue, newValue, CultureInfo.InvariantCulture, CompareOptions.None);
			case StringComparison.InvariantCultureIgnoreCase:
				return this.ReplaceCore(oldValue, newValue, CultureInfo.InvariantCulture, CompareOptions.IgnoreCase);
			case StringComparison.Ordinal:
				return this.Replace(oldValue, newValue);
			case StringComparison.OrdinalIgnoreCase:
				return this.ReplaceCore(oldValue, newValue, CultureInfo.InvariantCulture, CompareOptions.OrdinalIgnoreCase);
			default:
				throw new ArgumentException("The string comparison type passed in is currently not supported.", "comparisonType");
			}
		}

		// Token: 0x0600035D RID: 861 RVA: 0x00012E00 File Offset: 0x00011000
		private unsafe string ReplaceCore(string oldValue, string newValue, CultureInfo culture, CompareOptions options)
		{
			if (oldValue == null)
			{
				throw new ArgumentNullException("oldValue");
			}
			if (oldValue.Length == 0)
			{
				throw new ArgumentException("String cannot be of zero length.", "oldValue");
			}
			if (newValue == null)
			{
				newValue = string.Empty;
			}
			CultureInfo cultureInfo = culture ?? CultureInfo.CurrentCulture;
			StringBuilder stringBuilder = StringBuilderCache.Acquire(16);
			int num = 0;
			int num2 = 0;
			bool flag = false;
			CompareInfo compareInfo = cultureInfo.CompareInfo;
			for (;;)
			{
				int num3 = compareInfo.IndexOf(this, oldValue, num, this.Length - num, options, &num2);
				if (num3 >= 0)
				{
					stringBuilder.Append(this, num, num3 - num);
					stringBuilder.Append(newValue);
					num = num3 + num2;
					flag = true;
				}
				else
				{
					if (!flag)
					{
						break;
					}
					stringBuilder.Append(this, num, this.Length - num);
				}
				if (num3 < 0)
				{
					goto Block_7;
				}
			}
			StringBuilderCache.Release(stringBuilder);
			return this;
			Block_7:
			return StringBuilderCache.GetStringAndRelease(stringBuilder);
		}

		/// <summary>Returns a new string in which all occurrences of a specified Unicode character in this instance are replaced with another specified Unicode character.</summary>
		/// <returns>A string that is equivalent to this instance except that all instances of <paramref name="oldChar" /> are replaced with <paramref name="newChar" />. If <paramref name="oldChar" /> is not found in the current instance, the method returns the current instance unchanged. </returns>
		/// <param name="oldChar">The Unicode character to be replaced. </param>
		/// <param name="newChar">The Unicode character to replace all occurrences of <paramref name="oldChar" />. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600035E RID: 862 RVA: 0x00012EC0 File Offset: 0x000110C0
		public unsafe string Replace(char oldChar, char newChar)
		{
			if (oldChar == newChar)
			{
				return this;
			}
			int num = this.Length;
			fixed (char* ptr = &this._firstChar)
			{
				char* ptr2 = ptr;
				while (num > 0 && *ptr2 != oldChar)
				{
					num--;
					ptr2++;
				}
			}
			if (num == 0)
			{
				return this;
			}
			string text = string.FastAllocateString(this.Length);
			fixed (char* ptr = &this._firstChar)
			{
				char* ptr3 = ptr;
				fixed (char* ptr4 = &text._firstChar)
				{
					char* ptr5 = ptr4;
					int num2 = this.Length - num;
					if (num2 > 0)
					{
						string.wstrcpy(ptr5, ptr3, num2);
					}
					char* ptr6 = ptr3 + num2;
					char* ptr7 = ptr5 + num2;
					do
					{
						char c = *ptr6;
						if (c == oldChar)
						{
							c = newChar;
						}
						*ptr7 = c;
						num--;
						ptr6++;
						ptr7++;
					}
					while (num > 0);
				}
			}
			return text;
		}

		/// <summary>Returns a new string in which all occurrences of a specified string in the current instance are replaced with another specified string.</summary>
		/// <returns>A string that is equivalent to the current string except that all instances of <paramref name="oldValue" /> are replaced with <paramref name="newValue" />. If <paramref name="oldValue" /> is not found in the current instance, the method returns the current instance unchanged. </returns>
		/// <param name="oldValue">The string to be replaced. </param>
		/// <param name="newValue">The string to replace all occurrences of <paramref name="oldValue" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="oldValue" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="oldValue" /> is the empty string (""). </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600035F RID: 863 RVA: 0x00012F80 File Offset: 0x00011180
		public unsafe string Replace(string oldValue, string newValue)
		{
			if (oldValue == null)
			{
				throw new ArgumentNullException("oldValue");
			}
			if (oldValue.Length == 0)
			{
				throw new ArgumentException("String cannot be of zero length.", "oldValue");
			}
			if (newValue == null)
			{
				newValue = string.Empty;
			}
			Span<int> span = new Span<int>(stackalloc byte[(UIntPtr)512], 128);
			ValueListBuilder<int> valueListBuilder = new ValueListBuilder<int>(span);
			fixed (char* ptr = &this._firstChar)
			{
				char* ptr2 = ptr;
				int i = 0;
				int num = this.Length - oldValue.Length;
				IL_00B6:
				while (i <= num)
				{
					char* ptr3 = ptr2 + i;
					for (int j = 0; j < oldValue.Length; j++)
					{
						if (ptr3[j] != oldValue[j])
						{
							i++;
							goto IL_00B6;
						}
					}
					valueListBuilder.Append(i);
					i += oldValue.Length;
				}
			}
			if (valueListBuilder.Length == 0)
			{
				return this;
			}
			string text = this.ReplaceHelper(oldValue.Length, newValue, valueListBuilder.AsSpan());
			valueListBuilder.Dispose();
			return text;
		}

		// Token: 0x06000360 RID: 864 RVA: 0x00013074 File Offset: 0x00011274
		private unsafe string ReplaceHelper(int oldValueLength, string newValue, ReadOnlySpan<int> indices)
		{
			long num = (long)this.Length + (long)(newValue.Length - oldValueLength) * (long)indices.Length;
			if (num > 2147483647L)
			{
				throw new OutOfMemoryException();
			}
			string text = string.FastAllocateString((int)num);
			Span<char> span = new Span<char>(text.GetRawStringData(), text.Length);
			int num2 = 0;
			int num3 = 0;
			for (int i = 0; i < indices.Length; i++)
			{
				int num4 = *indices[i];
				int num5 = num4 - num2;
				if (num5 != 0)
				{
					this.AsSpan(num2, num5).CopyTo(span.Slice(num3));
					num3 += num5;
				}
				num2 = num4 + oldValueLength;
				newValue.AsSpan().CopyTo(span.Slice(num3));
				num3 += newValue.Length;
			}
			this.AsSpan(num2).CopyTo(span.Slice(num3));
			return text;
		}

		// Token: 0x06000361 RID: 865 RVA: 0x0001314B File Offset: 0x0001134B
		public string[] Split(char separator, StringSplitOptions options = StringSplitOptions.None)
		{
			return this.SplitInternal(new ReadOnlySpan<char>(ref separator, 1), int.MaxValue, options);
		}

		// Token: 0x06000362 RID: 866 RVA: 0x00013161 File Offset: 0x00011361
		public string[] Split(char separator, int count, StringSplitOptions options = StringSplitOptions.None)
		{
			return this.SplitInternal(new ReadOnlySpan<char>(ref separator, 1), count, options);
		}

		/// <summary>Returns a string array that contains the substrings in this instance that are delimited by elements of a specified Unicode character array.</summary>
		/// <returns>An array whose elements contain the substrings in this instance that are delimited by one or more characters in <paramref name="separator" />. For more information, see the Remarks section.</returns>
		/// <param name="separator">An array of Unicode characters that delimit the substrings in this instance, an empty array that contains no delimiters, or null. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000363 RID: 867 RVA: 0x00013173 File Offset: 0x00011373
		public string[] Split(params char[] separator)
		{
			return this.SplitInternal(separator, int.MaxValue, StringSplitOptions.None);
		}

		/// <summary>Returns a string array that contains the substrings in this instance that are delimited by elements of a specified Unicode character array. A parameter specifies the maximum number of substrings to return.</summary>
		/// <returns>An array whose elements contain the substrings in this instance that are delimited by one or more characters in <paramref name="separator" />. For more information, see the Remarks section.</returns>
		/// <param name="separator">An array of Unicode characters that delimit the substrings in this instance, an empty array that contains no delimiters, or null. </param>
		/// <param name="count">The maximum number of substrings to return. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="count" /> is negative. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000364 RID: 868 RVA: 0x00013187 File Offset: 0x00011387
		public string[] Split(char[] separator, int count)
		{
			return this.SplitInternal(separator, count, StringSplitOptions.None);
		}

		/// <summary>Returns a string array that contains the substrings in this string that are delimited by elements of a specified Unicode character array. A parameter specifies whether to return empty array elements.</summary>
		/// <returns>An array whose elements contain the substrings in this string that are delimited by one or more characters in <paramref name="separator" />. For more information, see the Remarks section.</returns>
		/// <param name="separator">An array of Unicode characters that delimit the substrings in this string, an empty array that contains no delimiters, or null. </param>
		/// <param name="options">
		///   <see cref="F:System.StringSplitOptions.RemoveEmptyEntries" /> to omit empty array elements from the array returned; or <see cref="F:System.StringSplitOptions.None" /> to include empty array elements in the array returned. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> is not one of the <see cref="T:System.StringSplitOptions" /> values.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000365 RID: 869 RVA: 0x00013197 File Offset: 0x00011397
		public string[] Split(char[] separator, StringSplitOptions options)
		{
			return this.SplitInternal(separator, int.MaxValue, options);
		}

		/// <summary>Returns a string array that contains the substrings in this string that are delimited by elements of a specified Unicode character array. Parameters specify the maximum number of substrings to return and whether to return empty array elements.</summary>
		/// <returns>An array whose elements contain the substrings in this string that are delimited by one or more characters in <paramref name="separator" />. For more information, see the Remarks section.</returns>
		/// <param name="separator">An array of Unicode characters that delimit the substrings in this string, an empty array that contains no delimiters, or null. </param>
		/// <param name="count">The maximum number of substrings to return. </param>
		/// <param name="options">
		///   <see cref="F:System.StringSplitOptions.RemoveEmptyEntries" /> to omit empty array elements from the array returned; or <see cref="F:System.StringSplitOptions.None" /> to include empty array elements in the array returned. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="count" /> is negative. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> is not one of the <see cref="T:System.StringSplitOptions" /> values.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000366 RID: 870 RVA: 0x000131AB File Offset: 0x000113AB
		public string[] Split(char[] separator, int count, StringSplitOptions options)
		{
			return this.SplitInternal(separator, count, options);
		}

		// Token: 0x06000367 RID: 871 RVA: 0x000131BC File Offset: 0x000113BC
		private unsafe string[] SplitInternal(ReadOnlySpan<char> separators, int count, StringSplitOptions options)
		{
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Count cannot be less than zero.");
			}
			if (options < StringSplitOptions.None || options > StringSplitOptions.RemoveEmptyEntries)
			{
				throw new ArgumentException(SR.Format("Illegal enum value: {0}.", options));
			}
			bool flag = options == StringSplitOptions.RemoveEmptyEntries;
			if (count == 0 || (flag && this.Length == 0))
			{
				return Array.Empty<string>();
			}
			if (count == 1)
			{
				return new string[] { this };
			}
			Span<int> span = new Span<int>(stackalloc byte[(UIntPtr)512], 128);
			ValueListBuilder<int> valueListBuilder = new ValueListBuilder<int>(span);
			this.MakeSeparatorList(separators, ref valueListBuilder);
			ReadOnlySpan<int> readOnlySpan = valueListBuilder.AsSpan();
			if (readOnlySpan.Length == 0)
			{
				return new string[] { this };
			}
			string[] array = (flag ? this.SplitOmitEmptyEntries(readOnlySpan, default(ReadOnlySpan<int>), 1, count) : this.SplitKeepEmptyEntries(readOnlySpan, default(ReadOnlySpan<int>), 1, count));
			valueListBuilder.Dispose();
			return array;
		}

		// Token: 0x06000368 RID: 872 RVA: 0x00013295 File Offset: 0x00011495
		public string[] Split(string separator, StringSplitOptions options = StringSplitOptions.None)
		{
			return this.SplitInternal(separator ?? string.Empty, null, int.MaxValue, options);
		}

		// Token: 0x06000369 RID: 873 RVA: 0x000132AE File Offset: 0x000114AE
		public string[] Split(string separator, int count, StringSplitOptions options = StringSplitOptions.None)
		{
			return this.SplitInternal(separator ?? string.Empty, null, count, options);
		}

		/// <summary>Returns a string array that contains the substrings in this string that are delimited by elements of a specified string array. A parameter specifies whether to return empty array elements.</summary>
		/// <returns>An array whose elements contain the substrings in this string that are delimited by one or more strings in <paramref name="separator" />. For more information, see the Remarks section.</returns>
		/// <param name="separator">An array of strings that delimit the substrings in this string, an empty array that contains no delimiters, or null. </param>
		/// <param name="options">
		///   <see cref="F:System.StringSplitOptions.RemoveEmptyEntries" /> to omit empty array elements from the array returned; or <see cref="F:System.StringSplitOptions.None" /> to include empty array elements in the array returned. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> is not one of the <see cref="T:System.StringSplitOptions" /> values.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600036A RID: 874 RVA: 0x000132C3 File Offset: 0x000114C3
		public string[] Split(string[] separator, StringSplitOptions options)
		{
			return this.SplitInternal(null, separator, int.MaxValue, options);
		}

		/// <summary>Returns a string array that contains the substrings in this string that are delimited by elements of a specified string array. Parameters specify the maximum number of substrings to return and whether to return empty array elements.</summary>
		/// <returns>An array whose elements contain the substrings in this string that are delimited by one or more strings in <paramref name="separator" />. For more information, see the Remarks section.</returns>
		/// <param name="separator">An array of strings that delimit the substrings in this string, an empty array that contains no delimiters, or null. </param>
		/// <param name="count">The maximum number of substrings to return. </param>
		/// <param name="options">
		///   <see cref="F:System.StringSplitOptions.RemoveEmptyEntries" /> to omit empty array elements from the array returned; or <see cref="F:System.StringSplitOptions.None" /> to include empty array elements in the array returned. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="count" /> is negative. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> is not one of the <see cref="T:System.StringSplitOptions" /> values.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600036B RID: 875 RVA: 0x000132D3 File Offset: 0x000114D3
		public string[] Split(string[] separator, int count, StringSplitOptions options)
		{
			return this.SplitInternal(null, separator, count, options);
		}

		// Token: 0x0600036C RID: 876 RVA: 0x000132E0 File Offset: 0x000114E0
		private unsafe string[] SplitInternal(string separator, string[] separators, int count, StringSplitOptions options)
		{
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Count cannot be less than zero.");
			}
			if (options < StringSplitOptions.None || options > StringSplitOptions.RemoveEmptyEntries)
			{
				throw new ArgumentException(SR.Format("Illegal enum value: {0}.", (int)options));
			}
			bool flag = options == StringSplitOptions.RemoveEmptyEntries;
			bool flag2 = separator != null;
			if (!flag2 && (separators == null || separators.Length == 0))
			{
				return this.SplitInternal(null, count, options);
			}
			if (count == 0 || (flag && this.Length == 0))
			{
				return Array.Empty<string>();
			}
			if (count == 1 || (flag2 && separator.Length == 0))
			{
				return new string[] { this };
			}
			if (flag2)
			{
				return this.SplitInternal(separator, count, options);
			}
			Span<int> span = new Span<int>(stackalloc byte[(UIntPtr)512], 128);
			ValueListBuilder<int> valueListBuilder = new ValueListBuilder<int>(span);
			Span<int> span2 = new Span<int>(stackalloc byte[(UIntPtr)512], 128);
			ValueListBuilder<int> valueListBuilder2 = new ValueListBuilder<int>(span2);
			this.MakeSeparatorList(separators, ref valueListBuilder, ref valueListBuilder2);
			ReadOnlySpan<int> readOnlySpan = valueListBuilder.AsSpan();
			ReadOnlySpan<int> readOnlySpan2 = valueListBuilder2.AsSpan();
			if (readOnlySpan.Length == 0)
			{
				return new string[] { this };
			}
			string[] array = (flag ? this.SplitOmitEmptyEntries(readOnlySpan, readOnlySpan2, 0, count) : this.SplitKeepEmptyEntries(readOnlySpan, readOnlySpan2, 0, count));
			valueListBuilder.Dispose();
			valueListBuilder2.Dispose();
			return array;
		}

		// Token: 0x0600036D RID: 877 RVA: 0x00013418 File Offset: 0x00011618
		private unsafe string[] SplitInternal(string separator, int count, StringSplitOptions options)
		{
			Span<int> span = new Span<int>(stackalloc byte[(UIntPtr)512], 128);
			ValueListBuilder<int> valueListBuilder = new ValueListBuilder<int>(span);
			this.MakeSeparatorList(separator, ref valueListBuilder);
			ReadOnlySpan<int> readOnlySpan = valueListBuilder.AsSpan();
			if (readOnlySpan.Length == 0)
			{
				return new string[] { this };
			}
			string[] array = ((options == StringSplitOptions.RemoveEmptyEntries) ? this.SplitOmitEmptyEntries(readOnlySpan, default(ReadOnlySpan<int>), separator.Length, count) : this.SplitKeepEmptyEntries(readOnlySpan, default(ReadOnlySpan<int>), separator.Length, count));
			valueListBuilder.Dispose();
			return array;
		}

		// Token: 0x0600036E RID: 878 RVA: 0x000134A0 File Offset: 0x000116A0
		private unsafe string[] SplitKeepEmptyEntries(ReadOnlySpan<int> sepList, ReadOnlySpan<int> lengthList, int defaultLength, int count)
		{
			int num = 0;
			int num2 = 0;
			count--;
			int num3 = ((sepList.Length < count) ? sepList.Length : count);
			string[] array = new string[num3 + 1];
			int num4 = 0;
			while (num4 < num3 && num < this.Length)
			{
				array[num2++] = this.Substring(num, *sepList[num4] - num);
				num = *sepList[num4] + (lengthList.IsEmpty ? defaultLength : (*lengthList[num4]));
				num4++;
			}
			if (num < this.Length && num3 >= 0)
			{
				array[num2] = this.Substring(num);
			}
			else if (num2 == num3)
			{
				array[num2] = string.Empty;
			}
			return array;
		}

		// Token: 0x0600036F RID: 879 RVA: 0x00013554 File Offset: 0x00011754
		private unsafe string[] SplitOmitEmptyEntries(ReadOnlySpan<int> sepList, ReadOnlySpan<int> lengthList, int defaultLength, int count)
		{
			int length = sepList.Length;
			int num = ((length < count) ? (length + 1) : count);
			string[] array = new string[num];
			int num2 = 0;
			int num3 = 0;
			int i = 0;
			while (i < length && num2 < this.Length)
			{
				if (*sepList[i] - num2 > 0)
				{
					array[num3++] = this.Substring(num2, *sepList[i] - num2);
				}
				num2 = *sepList[i] + (lengthList.IsEmpty ? defaultLength : (*lengthList[i]));
				if (num3 == count - 1)
				{
					while (i < length - 1)
					{
						if (num2 != *sepList[++i])
						{
							break;
						}
						num2 += (lengthList.IsEmpty ? defaultLength : (*lengthList[i]));
					}
					break;
				}
				i++;
			}
			if (num2 < this.Length)
			{
				array[num3++] = this.Substring(num2);
			}
			string[] array2 = array;
			if (num3 != num)
			{
				array2 = new string[num3];
				for (int j = 0; j < num3; j++)
				{
					array2[j] = array[j];
				}
			}
			return array2;
		}

		// Token: 0x06000370 RID: 880 RVA: 0x00013674 File Offset: 0x00011874
		private unsafe void MakeSeparatorList(ReadOnlySpan<char> separators, ref ValueListBuilder<int> sepListBuilder)
		{
			switch (separators.Length)
			{
			case 0:
			{
				for (int i = 0; i < this.Length; i++)
				{
					if (char.IsWhiteSpace(this[i]))
					{
						sepListBuilder.Append(i);
					}
				}
				return;
			}
			case 1:
			{
				char c = (char)(*separators[0]);
				for (int j = 0; j < this.Length; j++)
				{
					if (this[j] == c)
					{
						sepListBuilder.Append(j);
					}
				}
				return;
			}
			case 2:
			{
				char c = (char)(*separators[0]);
				char c2 = (char)(*separators[1]);
				for (int k = 0; k < this.Length; k++)
				{
					char c3 = this[k];
					if (c3 == c || c3 == c2)
					{
						sepListBuilder.Append(k);
					}
				}
				return;
			}
			case 3:
			{
				char c = (char)(*separators[0]);
				char c2 = (char)(*separators[1]);
				char c4 = (char)(*separators[2]);
				for (int l = 0; l < this.Length; l++)
				{
					char c5 = this[l];
					if (c5 == c || c5 == c2 || c5 == c4)
					{
						sepListBuilder.Append(l);
					}
				}
				return;
			}
			default:
			{
				string.ProbabilisticMap probabilisticMap = default(string.ProbabilisticMap);
				uint* ptr = (uint*)(&probabilisticMap);
				string.InitializeProbabilisticMap(ptr, separators);
				for (int m = 0; m < this.Length; m++)
				{
					char c6 = this[m];
					if (string.IsCharBitSet(ptr, (byte)c6) && string.IsCharBitSet(ptr, (byte)(c6 >> 8)) && separators.Contains(c6))
					{
						sepListBuilder.Append(m);
					}
				}
				return;
			}
			}
		}

		// Token: 0x06000371 RID: 881 RVA: 0x00013800 File Offset: 0x00011A00
		private void MakeSeparatorList(string separator, ref ValueListBuilder<int> sepListBuilder)
		{
			int length = separator.Length;
			for (int i = 0; i < this.Length; i++)
			{
				if (this[i] == separator[0] && length <= this.Length - i && (length == 1 || this.AsSpan(i, length).SequenceEqual(separator)))
				{
					sepListBuilder.Append(i);
					i += length - 1;
				}
			}
		}

		// Token: 0x06000372 RID: 882 RVA: 0x00013868 File Offset: 0x00011A68
		private void MakeSeparatorList(string[] separators, ref ValueListBuilder<int> sepListBuilder, ref ValueListBuilder<int> lengthListBuilder)
		{
			int num = separators.Length;
			for (int i = 0; i < this.Length; i++)
			{
				foreach (string text in separators)
				{
					if (!string.IsNullOrEmpty(text))
					{
						int length = text.Length;
						if (this[i] == text[0] && length <= this.Length - i && (length == 1 || this.AsSpan(i, length).SequenceEqual(text)))
						{
							sepListBuilder.Append(i);
							lengthListBuilder.Append(length);
							i += length - 1;
							break;
						}
					}
				}
			}
		}

		/// <summary>Retrieves a substring from this instance. The substring starts at a specified character position and continues to the end of the string.</summary>
		/// <returns>A string that is equivalent to the substring that begins at <paramref name="startIndex" /> in this instance, or <see cref="F:System.String.Empty" /> if <paramref name="startIndex" /> is equal to the length of this instance.</returns>
		/// <param name="startIndex">The zero-based starting character position of a substring in this instance. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is less than zero or greater than the length of this instance. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000373 RID: 883 RVA: 0x000138F5 File Offset: 0x00011AF5
		public string Substring(int startIndex)
		{
			return this.Substring(startIndex, this.Length - startIndex);
		}

		/// <summary>Retrieves a substring from this instance. The substring starts at a specified character position and has a specified length.</summary>
		/// <returns>A string that is equivalent to the substring of length <paramref name="length" /> that begins at <paramref name="startIndex" /> in this instance, or <see cref="F:System.String.Empty" /> if <paramref name="startIndex" /> is equal to the length of this instance and <paramref name="length" /> is zero.</returns>
		/// <param name="startIndex">The zero-based starting character position of a substring in this instance. </param>
		/// <param name="length">The number of characters in the substring. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> plus <paramref name="length" /> indicates a position not within this instance.-or- <paramref name="startIndex" /> or <paramref name="length" /> is less than zero. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000374 RID: 884 RVA: 0x00013908 File Offset: 0x00011B08
		public string Substring(int startIndex, int length)
		{
			if (startIndex < 0)
			{
				throw new ArgumentOutOfRangeException("startIndex", "StartIndex cannot be less than zero.");
			}
			if (startIndex > this.Length)
			{
				throw new ArgumentOutOfRangeException("startIndex", "startIndex cannot be larger than length of string.");
			}
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length", "Length cannot be less than zero.");
			}
			if (startIndex > this.Length - length)
			{
				throw new ArgumentOutOfRangeException("length", "Index and length must refer to a location within the string.");
			}
			if (length == 0)
			{
				return string.Empty;
			}
			if (startIndex == 0 && length == this.Length)
			{
				return this;
			}
			return this.InternalSubString(startIndex, length);
		}

		// Token: 0x06000375 RID: 885 RVA: 0x00013990 File Offset: 0x00011B90
		private unsafe string InternalSubString(int startIndex, int length)
		{
			string text = string.FastAllocateString(length);
			fixed (char* ptr = &text._firstChar)
			{
				char* ptr2 = ptr;
				fixed (char* ptr3 = &this._firstChar)
				{
					char* ptr4 = ptr3;
					string.wstrcpy(ptr2, ptr4 + startIndex, length);
				}
			}
			return text;
		}

		/// <summary>Returns a copy of this string converted to lowercase.</summary>
		/// <returns>A string in lowercase.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06000376 RID: 886 RVA: 0x000139C8 File Offset: 0x00011BC8
		public string ToLower()
		{
			return CultureInfo.CurrentCulture.TextInfo.ToLower(this);
		}

		/// <summary>Returns a copy of this string converted to lowercase, using the casing rules of the specified culture.</summary>
		/// <returns>The lowercase equivalent of the current string.</returns>
		/// <param name="culture">An object that supplies culture-specific casing rules. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="culture" /> is null. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06000377 RID: 887 RVA: 0x000139DA File Offset: 0x00011BDA
		public string ToLower(CultureInfo culture)
		{
			if (culture == null)
			{
				throw new ArgumentNullException("culture");
			}
			return culture.TextInfo.ToLower(this);
		}

		/// <summary>Returns a copy of this <see cref="T:System.String" /> object converted to lowercase using the casing rules of the invariant culture.</summary>
		/// <returns>The lowercase equivalent of the current string.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06000378 RID: 888 RVA: 0x000139F6 File Offset: 0x00011BF6
		public string ToLowerInvariant()
		{
			return CultureInfo.InvariantCulture.TextInfo.ToLower(this);
		}

		/// <summary>Returns a copy of this string converted to uppercase.</summary>
		/// <returns>The uppercase equivalent of the current string.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06000379 RID: 889 RVA: 0x00013A08 File Offset: 0x00011C08
		public string ToUpper()
		{
			return CultureInfo.CurrentCulture.TextInfo.ToUpper(this);
		}

		/// <summary>Returns a copy of this string converted to uppercase, using the casing rules of the specified culture.</summary>
		/// <returns>The uppercase equivalent of the current string.</returns>
		/// <param name="culture">An object that supplies culture-specific casing rules. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="culture" /> is null. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x0600037A RID: 890 RVA: 0x00013A1A File Offset: 0x00011C1A
		public string ToUpper(CultureInfo culture)
		{
			if (culture == null)
			{
				throw new ArgumentNullException("culture");
			}
			return culture.TextInfo.ToUpper(this);
		}

		/// <summary>Returns a copy of this <see cref="T:System.String" /> object converted to uppercase using the casing rules of the invariant culture.</summary>
		/// <returns>The uppercase equivalent of the current string.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x0600037B RID: 891 RVA: 0x00013A36 File Offset: 0x00011C36
		public string ToUpperInvariant()
		{
			return CultureInfo.InvariantCulture.TextInfo.ToUpper(this);
		}

		/// <summary>Removes all leading and trailing white-space characters from the current <see cref="T:System.String" /> object.</summary>
		/// <returns>The string that remains after all white-space characters are removed from the start and end of the current string.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600037C RID: 892 RVA: 0x00013A48 File Offset: 0x00011C48
		public string Trim()
		{
			return this.TrimWhiteSpaceHelper(string.TrimType.Both);
		}

		// Token: 0x0600037D RID: 893 RVA: 0x00013A51 File Offset: 0x00011C51
		public unsafe string Trim(char trimChar)
		{
			return this.TrimHelper(&trimChar, 1, string.TrimType.Both);
		}

		/// <summary>Removes all leading and trailing occurrences of a set of characters specified in an array from the current <see cref="T:System.String" /> object.</summary>
		/// <returns>The string that remains after all occurrences of the characters in the <paramref name="trimChars" /> parameter are removed from the start and end of the current string. If <paramref name="trimChars" /> is null or an empty array, white-space characters are removed instead.</returns>
		/// <param name="trimChars">An array of Unicode characters to remove, or null. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600037E RID: 894 RVA: 0x00013A60 File Offset: 0x00011C60
		public unsafe string Trim(params char[] trimChars)
		{
			if (trimChars == null || trimChars.Length == 0)
			{
				return this.TrimWhiteSpaceHelper(string.TrimType.Both);
			}
			fixed (char* ptr = &trimChars[0])
			{
				char* ptr2 = ptr;
				return this.TrimHelper(ptr2, trimChars.Length, string.TrimType.Both);
			}
		}

		// Token: 0x0600037F RID: 895 RVA: 0x00013A92 File Offset: 0x00011C92
		public string TrimStart()
		{
			return this.TrimWhiteSpaceHelper(string.TrimType.Head);
		}

		// Token: 0x06000380 RID: 896 RVA: 0x00013A9B File Offset: 0x00011C9B
		public unsafe string TrimStart(char trimChar)
		{
			return this.TrimHelper(&trimChar, 1, string.TrimType.Head);
		}

		/// <summary>Removes all leading occurrences of a set of characters specified in an array from the current <see cref="T:System.String" /> object.</summary>
		/// <returns>The string that remains after all occurrences of characters in the <paramref name="trimChars" /> parameter are removed from the start of the current string. If <paramref name="trimChars" /> is null or an empty array, white-space characters are removed instead.</returns>
		/// <param name="trimChars">An array of Unicode characters to remove, or null. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000381 RID: 897 RVA: 0x00013AA8 File Offset: 0x00011CA8
		public unsafe string TrimStart(params char[] trimChars)
		{
			if (trimChars == null || trimChars.Length == 0)
			{
				return this.TrimWhiteSpaceHelper(string.TrimType.Head);
			}
			fixed (char* ptr = &trimChars[0])
			{
				char* ptr2 = ptr;
				return this.TrimHelper(ptr2, trimChars.Length, string.TrimType.Head);
			}
		}

		// Token: 0x06000382 RID: 898 RVA: 0x00013ADA File Offset: 0x00011CDA
		public string TrimEnd()
		{
			return this.TrimWhiteSpaceHelper(string.TrimType.Tail);
		}

		// Token: 0x06000383 RID: 899 RVA: 0x00013AE3 File Offset: 0x00011CE3
		public unsafe string TrimEnd(char trimChar)
		{
			return this.TrimHelper(&trimChar, 1, string.TrimType.Tail);
		}

		/// <summary>Removes all trailing occurrences of a set of characters specified in an array from the current <see cref="T:System.String" /> object.</summary>
		/// <returns>The string that remains after all occurrences of the characters in the <paramref name="trimChars" /> parameter are removed from the end of the current string. If <paramref name="trimChars" /> is null or an empty array, Unicode white-space characters are removed instead.</returns>
		/// <param name="trimChars">An array of Unicode characters to remove, or null. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000384 RID: 900 RVA: 0x00013AF0 File Offset: 0x00011CF0
		public unsafe string TrimEnd(params char[] trimChars)
		{
			if (trimChars == null || trimChars.Length == 0)
			{
				return this.TrimWhiteSpaceHelper(string.TrimType.Tail);
			}
			fixed (char* ptr = &trimChars[0])
			{
				char* ptr2 = ptr;
				return this.TrimHelper(ptr2, trimChars.Length, string.TrimType.Tail);
			}
		}

		// Token: 0x06000385 RID: 901 RVA: 0x00013B24 File Offset: 0x00011D24
		private string TrimWhiteSpaceHelper(string.TrimType trimType)
		{
			int num = this.Length - 1;
			int num2 = 0;
			if (trimType != string.TrimType.Tail)
			{
				num2 = 0;
				while (num2 < this.Length && char.IsWhiteSpace(this[num2]))
				{
					num2++;
				}
			}
			if (trimType != string.TrimType.Head)
			{
				num = this.Length - 1;
				while (num >= num2 && char.IsWhiteSpace(this[num]))
				{
					num--;
				}
			}
			return this.CreateTrimmedString(num2, num);
		}

		// Token: 0x06000386 RID: 902 RVA: 0x00013B8C File Offset: 0x00011D8C
		private unsafe string TrimHelper(char* trimChars, int trimCharsLength, string.TrimType trimType)
		{
			int i = this.Length - 1;
			int j = 0;
			if (trimType != string.TrimType.Tail)
			{
				for (j = 0; j < this.Length; j++)
				{
					char c = this[j];
					int num = 0;
					while (num < trimCharsLength && trimChars[num] != c)
					{
						num++;
					}
					if (num == trimCharsLength)
					{
						break;
					}
				}
			}
			if (trimType != string.TrimType.Head)
			{
				for (i = this.Length - 1; i >= j; i--)
				{
					char c2 = this[i];
					int num2 = 0;
					while (num2 < trimCharsLength && trimChars[num2] != c2)
					{
						num2++;
					}
					if (num2 == trimCharsLength)
					{
						break;
					}
				}
			}
			return this.CreateTrimmedString(j, i);
		}

		// Token: 0x06000387 RID: 903 RVA: 0x00013C28 File Offset: 0x00011E28
		private string CreateTrimmedString(int start, int end)
		{
			int num = end - start + 1;
			if (num == this.Length)
			{
				return this;
			}
			if (num != 0)
			{
				return this.InternalSubString(start, num);
			}
			return string.Empty;
		}

		/// <summary>Returns a value indicating whether the specified <see cref="T:System.String" /> object occurs within this string.</summary>
		/// <returns>true if the <paramref name="value" /> parameter occurs within this string, or if <paramref name="value" /> is the empty string (""); otherwise, false.</returns>
		/// <param name="value">The string to seek. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000388 RID: 904 RVA: 0x00013C57 File Offset: 0x00011E57
		public bool Contains(string value)
		{
			return this.IndexOf(value, StringComparison.Ordinal) >= 0;
		}

		// Token: 0x06000389 RID: 905 RVA: 0x00013C67 File Offset: 0x00011E67
		public bool Contains(string value, StringComparison comparisonType)
		{
			return this.IndexOf(value, comparisonType) >= 0;
		}

		// Token: 0x0600038A RID: 906 RVA: 0x00013C77 File Offset: 0x00011E77
		public bool Contains(char value)
		{
			return this.IndexOf(value) != -1;
		}

		// Token: 0x0600038B RID: 907 RVA: 0x00013C86 File Offset: 0x00011E86
		public bool Contains(char value, StringComparison comparisonType)
		{
			return this.IndexOf(value, comparisonType) != -1;
		}

		/// <summary>Reports the zero-based index of the first occurrence of the specified Unicode character in this string.</summary>
		/// <returns>The zero-based index position of <paramref name="value" /> if that character is found, or -1 if it is not.</returns>
		/// <param name="value">A Unicode character to seek. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600038C RID: 908 RVA: 0x00013C96 File Offset: 0x00011E96
		public int IndexOf(char value)
		{
			return SpanHelpers.IndexOf(ref this._firstChar, value, this.Length);
		}

		/// <summary>Reports the zero-based index of the first occurrence of the specified Unicode character in this string. The search starts at a specified character position.</summary>
		/// <returns>The zero-based index position of <paramref name="value" /> if that character is found, or -1 if it is not.</returns>
		/// <param name="value">A Unicode character to seek. </param>
		/// <param name="startIndex">The search starting position. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is less than 0 (zero) or greater than the length of the string. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600038D RID: 909 RVA: 0x00013CAA File Offset: 0x00011EAA
		public int IndexOf(char value, int startIndex)
		{
			return this.IndexOf(value, startIndex, this.Length - startIndex);
		}

		// Token: 0x0600038E RID: 910 RVA: 0x00013CBC File Offset: 0x00011EBC
		public int IndexOf(char value, StringComparison comparisonType)
		{
			switch (comparisonType)
			{
			case StringComparison.CurrentCulture:
				return CultureInfo.CurrentCulture.CompareInfo.IndexOf(this, value, CompareOptions.None);
			case StringComparison.CurrentCultureIgnoreCase:
				return CultureInfo.CurrentCulture.CompareInfo.IndexOf(this, value, CompareOptions.IgnoreCase);
			case StringComparison.InvariantCulture:
				return CompareInfo.Invariant.IndexOf(this, value, CompareOptions.None);
			case StringComparison.InvariantCultureIgnoreCase:
				return CompareInfo.Invariant.IndexOf(this, value, CompareOptions.IgnoreCase);
			case StringComparison.Ordinal:
				return CompareInfo.Invariant.IndexOf(this, value, CompareOptions.Ordinal);
			case StringComparison.OrdinalIgnoreCase:
				return CompareInfo.Invariant.IndexOf(this, value, CompareOptions.OrdinalIgnoreCase);
			default:
				throw new ArgumentException("The string comparison type passed in is currently not supported.", "comparisonType");
			}
		}

		/// <summary>Reports the zero-based index of the first occurrence of the specified character in this instance. The search starts at a specified character position and examines a specified number of character positions.</summary>
		/// <returns>The zero-based index position of <paramref name="value" /> if that character is found, or -1 if it is not.</returns>
		/// <param name="value">A Unicode character to seek. </param>
		/// <param name="startIndex">The search starting position. </param>
		/// <param name="count">The number of character positions to examine. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="count" /> or <paramref name="startIndex" /> is negative.-or- <paramref name="startIndex" /> is greater than the length of this string.-or-<paramref name="count" /> is greater than the length of this string minus <paramref name="startIndex" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600038F RID: 911 RVA: 0x00013D60 File Offset: 0x00011F60
		public int IndexOf(char value, int startIndex, int count)
		{
			if (startIndex > this.Length)
			{
				throw new ArgumentOutOfRangeException("startIndex", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			if (count > this.Length - startIndex)
			{
				throw new ArgumentOutOfRangeException("count", "Count must be positive and count must refer to a location within the string/array/collection.");
			}
			int num = SpanHelpers.IndexOf(Unsafe.Add<char>(ref this._firstChar, startIndex), value, count);
			if (num != -1)
			{
				return num + startIndex;
			}
			return num;
		}

		/// <summary>Reports the zero-based index of the first occurrence in this instance of any character in a specified array of Unicode characters.</summary>
		/// <returns>The zero-based index position of the first occurrence in this instance where any character in <paramref name="anyOf" /> was found; -1 if no character in <paramref name="anyOf" /> was found.</returns>
		/// <param name="anyOf">A Unicode character array containing one or more characters to seek. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="anyOf" /> is null. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000390 RID: 912 RVA: 0x00013DBE File Offset: 0x00011FBE
		public int IndexOfAny(char[] anyOf)
		{
			return this.IndexOfAny(anyOf, 0, this.Length);
		}

		/// <summary>Reports the zero-based index of the first occurrence in this instance of any character in a specified array of Unicode characters. The search starts at a specified character position.</summary>
		/// <returns>The zero-based index position of the first occurrence in this instance where any character in <paramref name="anyOf" /> was found; -1 if no character in <paramref name="anyOf" /> was found.</returns>
		/// <param name="anyOf">A Unicode character array containing one or more characters to seek. </param>
		/// <param name="startIndex">The search starting position. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="anyOf" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is negative.-or- <paramref name="startIndex" /> is greater than the number of characters in this instance. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000391 RID: 913 RVA: 0x00013DCE File Offset: 0x00011FCE
		public int IndexOfAny(char[] anyOf, int startIndex)
		{
			return this.IndexOfAny(anyOf, startIndex, this.Length - startIndex);
		}

		/// <summary>Reports the zero-based index of the first occurrence in this instance of any character in a specified array of Unicode characters. The search starts at a specified character position and examines a specified number of character positions.</summary>
		/// <returns>The zero-based index position of the first occurrence in this instance where any character in <paramref name="anyOf" /> was found; -1 if no character in <paramref name="anyOf" /> was found.</returns>
		/// <param name="anyOf">A Unicode character array containing one or more characters to seek. </param>
		/// <param name="startIndex">The search starting position. </param>
		/// <param name="count">The number of character positions to examine. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="anyOf" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="count" /> or <paramref name="startIndex" /> is negative.-or- <paramref name="count" /> + <paramref name="startIndex" /> is greater than the number of characters in this instance. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000392 RID: 914 RVA: 0x00013DE0 File Offset: 0x00011FE0
		public int IndexOfAny(char[] anyOf, int startIndex, int count)
		{
			if (anyOf == null)
			{
				throw new ArgumentNullException("anyOf");
			}
			if (startIndex > this.Length)
			{
				throw new ArgumentOutOfRangeException("startIndex", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			if (count > this.Length - startIndex)
			{
				throw new ArgumentOutOfRangeException("count", "Count must be positive and count must refer to a location within the string/array/collection.");
			}
			if (anyOf.Length == 2)
			{
				return this.IndexOfAny(anyOf[0], anyOf[1], startIndex, count);
			}
			if (anyOf.Length == 3)
			{
				return this.IndexOfAny(anyOf[0], anyOf[1], anyOf[2], startIndex, count);
			}
			if (anyOf.Length > 3)
			{
				return this.IndexOfCharArray(anyOf, startIndex, count);
			}
			if (anyOf.Length == 1)
			{
				return this.IndexOf(anyOf[0], startIndex, count);
			}
			return -1;
		}

		// Token: 0x06000393 RID: 915 RVA: 0x00013E80 File Offset: 0x00012080
		private unsafe int IndexOfAny(char value1, char value2, int startIndex, int count)
		{
			fixed (char* ptr = &this._firstChar)
			{
				char* ptr2 = ptr;
				char* ptr3 = ptr2 + startIndex;
				while (count > 0)
				{
					char c = *ptr3;
					if (c == value1 || c == value2)
					{
						return (int)((long)(ptr3 - ptr2));
					}
					c = ptr3[1];
					if (c == value1 || c == value2)
					{
						if (count != 1)
						{
							return (int)((long)(ptr3 - ptr2)) + 1;
						}
						return -1;
					}
					else
					{
						ptr3 += 2;
						count -= 2;
					}
				}
				return -1;
			}
		}

		// Token: 0x06000394 RID: 916 RVA: 0x00013EE4 File Offset: 0x000120E4
		private unsafe int IndexOfAny(char value1, char value2, char value3, int startIndex, int count)
		{
			fixed (char* ptr = &this._firstChar)
			{
				char* ptr2 = ptr;
				char* ptr3 = ptr2 + startIndex;
				while (count > 0)
				{
					char c = *ptr3;
					if (c == value1 || c == value2 || c == value3)
					{
						return (int)((long)(ptr3 - ptr2));
					}
					ptr3++;
					count--;
				}
				return -1;
			}
		}

		// Token: 0x06000395 RID: 917 RVA: 0x00013F2C File Offset: 0x0001212C
		private unsafe int IndexOfCharArray(char[] anyOf, int startIndex, int count)
		{
			string.ProbabilisticMap probabilisticMap = default(string.ProbabilisticMap);
			uint* ptr = (uint*)(&probabilisticMap);
			string.InitializeProbabilisticMap(ptr, anyOf);
			fixed (char* ptr2 = &this._firstChar)
			{
				char* ptr3 = ptr2;
				char* ptr4 = ptr3 + startIndex;
				while (count > 0)
				{
					int num = (int)(*ptr4);
					if (string.IsCharBitSet(ptr, (byte)num) && string.IsCharBitSet(ptr, (byte)(num >> 8)) && string.ArrayContains((char)num, anyOf))
					{
						return (int)((long)(ptr4 - ptr3));
					}
					count--;
					ptr4++;
				}
				return -1;
			}
		}

		// Token: 0x06000396 RID: 918 RVA: 0x00013FA8 File Offset: 0x000121A8
		private unsafe static void InitializeProbabilisticMap(uint* charMap, ReadOnlySpan<char> anyOf)
		{
			bool flag = false;
			for (int i = 0; i < anyOf.Length; i++)
			{
				int num = (int)(*anyOf[i]);
				string.SetCharBit(charMap, (byte)num);
				num >>= 8;
				if (num == 0)
				{
					flag = true;
				}
				else
				{
					string.SetCharBit(charMap, (byte)num);
				}
			}
			if (flag)
			{
				*charMap |= 1U;
			}
		}

		// Token: 0x06000397 RID: 919 RVA: 0x00013FFC File Offset: 0x000121FC
		private static bool ArrayContains(char searchChar, char[] anyOf)
		{
			for (int i = 0; i < anyOf.Length; i++)
			{
				if (anyOf[i] == searchChar)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000398 RID: 920 RVA: 0x00014020 File Offset: 0x00012220
		private unsafe static bool IsCharBitSet(uint* charMap, byte value)
		{
			return (charMap[value & 7] & (1U << (value >> 3))) > 0U;
		}

		// Token: 0x06000399 RID: 921 RVA: 0x00014037 File Offset: 0x00012237
		private unsafe static void SetCharBit(uint* charMap, byte value)
		{
			charMap[value & 7] |= 1U << (value >> 3);
		}

		/// <summary>Reports the zero-based index of the first occurrence of the specified string in this instance.</summary>
		/// <returns>The zero-based index position of <paramref name="value" /> if that string is found, or -1 if it is not. If <paramref name="value" /> is <see cref="F:System.String.Empty" />, the return value is 0.</returns>
		/// <param name="value">The string to seek. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600039A RID: 922 RVA: 0x0001404D File Offset: 0x0001224D
		public int IndexOf(string value)
		{
			return this.IndexOf(value, StringComparison.CurrentCulture);
		}

		/// <summary>Reports the zero-based index of the first occurrence of the specified string in this instance. The search starts at a specified character position.</summary>
		/// <returns>The zero-based index position of <paramref name="value" /> if that string is found, or -1 if it is not. If <paramref name="value" /> is <see cref="F:System.String.Empty" />, the return value is <paramref name="startIndex" />.</returns>
		/// <param name="value">The string to seek. </param>
		/// <param name="startIndex">The search starting position. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is less than 0 (zero) or greater than the length of this string.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600039B RID: 923 RVA: 0x00014057 File Offset: 0x00012257
		public int IndexOf(string value, int startIndex)
		{
			return this.IndexOf(value, startIndex, StringComparison.CurrentCulture);
		}

		/// <summary>Reports the zero-based index of the first occurrence of the specified string in this instance. The search starts at a specified character position and examines a specified number of character positions.</summary>
		/// <returns>The zero-based index position of <paramref name="value" /> if that string is found, or -1 if it is not. If <paramref name="value" /> is <see cref="F:System.String.Empty" />, the return value is <paramref name="startIndex" />.</returns>
		/// <param name="value">The string to seek. </param>
		/// <param name="startIndex">The search starting position. </param>
		/// <param name="count">The number of character positions to examine. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="count" /> or <paramref name="startIndex" /> is negative.-or- <paramref name="startIndex" /> is greater than the length of this string.-or-<paramref name="count" /> is greater than the length of this string minus <paramref name="startIndex" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600039C RID: 924 RVA: 0x00014064 File Offset: 0x00012264
		public int IndexOf(string value, int startIndex, int count)
		{
			if (startIndex < 0 || startIndex > this.Length)
			{
				throw new ArgumentOutOfRangeException("startIndex", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			if (count < 0 || count > this.Length - startIndex)
			{
				throw new ArgumentOutOfRangeException("count", "Count must be positive and count must refer to a location within the string/array/collection.");
			}
			return this.IndexOf(value, startIndex, count, StringComparison.CurrentCulture);
		}

		/// <summary>Reports the zero-based index of the first occurrence of the specified string in the current <see cref="T:System.String" /> object. A parameter specifies the type of search to use for the specified string.</summary>
		/// <returns>The index position of the <paramref name="value" /> parameter if that string is found, or -1 if it is not. If <paramref name="value" /> is <see cref="F:System.String.Empty" />, the return value is 0.</returns>
		/// <param name="value">The string to seek. </param>
		/// <param name="comparisonType">One of the enumeration values that specifies the rules for the search. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="comparisonType" /> is not a valid <see cref="T:System.StringComparison" /> value.</exception>
		// Token: 0x0600039D RID: 925 RVA: 0x000140B7 File Offset: 0x000122B7
		public int IndexOf(string value, StringComparison comparisonType)
		{
			return this.IndexOf(value, 0, this.Length, comparisonType);
		}

		/// <summary>Reports the zero-based index of the first occurrence of the specified string in the current <see cref="T:System.String" /> object. Parameters specify the starting search position in the current string and the type of search to use for the specified string.</summary>
		/// <returns>The zero-based index position of the <paramref name="value" /> parameter if that string is found, or -1 if it is not. If <paramref name="value" /> is <see cref="F:System.String.Empty" />, the return value is <paramref name="startIndex" />.</returns>
		/// <param name="value">The string to seek. </param>
		/// <param name="startIndex">The search starting position. </param>
		/// <param name="comparisonType">One of the enumeration values that specifies the rules for the search. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is less than 0 (zero) or greater than the length of this string. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="comparisonType" /> is not a valid <see cref="T:System.StringComparison" /> value.</exception>
		// Token: 0x0600039E RID: 926 RVA: 0x000140C8 File Offset: 0x000122C8
		public int IndexOf(string value, int startIndex, StringComparison comparisonType)
		{
			return this.IndexOf(value, startIndex, this.Length - startIndex, comparisonType);
		}

		/// <summary>Reports the zero-based index of the first occurrence of the specified string in the current <see cref="T:System.String" /> object. Parameters specify the starting search position in the current string, the number of characters in the current string to search, and the type of search to use for the specified string.</summary>
		/// <returns>The zero-based index position of the <paramref name="value" /> parameter if that string is found, or -1 if it is not. If <paramref name="value" /> is <see cref="F:System.String.Empty" />, the return value is <paramref name="startIndex" />.</returns>
		/// <param name="value">The string to seek. </param>
		/// <param name="startIndex">The search starting position. </param>
		/// <param name="count">The number of character positions to examine. </param>
		/// <param name="comparisonType">One of the enumeration values that specifies the rules for the search. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="count" /> or <paramref name="startIndex" /> is negative.-or- <paramref name="startIndex" /> is greater than the length of this instance.-or-<paramref name="count" /> is greater than the length of this string minus <paramref name="startIndex" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="comparisonType" /> is not a valid <see cref="T:System.StringComparison" /> value.</exception>
		// Token: 0x0600039F RID: 927 RVA: 0x000140DC File Offset: 0x000122DC
		public int IndexOf(string value, int startIndex, int count, StringComparison comparisonType)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (startIndex < 0 || startIndex > this.Length)
			{
				throw new ArgumentOutOfRangeException("startIndex", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			if (count < 0 || startIndex > this.Length - count)
			{
				throw new ArgumentOutOfRangeException("count", "Count must be positive and count must refer to a location within the string/array/collection.");
			}
			switch (comparisonType)
			{
			case StringComparison.CurrentCulture:
				return CultureInfo.CurrentCulture.CompareInfo.IndexOf(this, value, startIndex, count, CompareOptions.None);
			case StringComparison.CurrentCultureIgnoreCase:
				return CultureInfo.CurrentCulture.CompareInfo.IndexOf(this, value, startIndex, count, CompareOptions.IgnoreCase);
			case StringComparison.InvariantCulture:
				return CompareInfo.Invariant.IndexOf(this, value, startIndex, count, CompareOptions.None);
			case StringComparison.InvariantCultureIgnoreCase:
				return CompareInfo.Invariant.IndexOf(this, value, startIndex, count, CompareOptions.IgnoreCase);
			case StringComparison.Ordinal:
				return CompareInfo.Invariant.IndexOfOrdinal(this, value, startIndex, count, false);
			case StringComparison.OrdinalIgnoreCase:
				return CompareInfo.Invariant.IndexOfOrdinal(this, value, startIndex, count, true);
			default:
				throw new ArgumentException("The string comparison type passed in is currently not supported.", "comparisonType");
			}
		}

		/// <summary>Reports the zero-based index position of the last occurrence of a specified Unicode character within this instance.</summary>
		/// <returns>The zero-based index position of <paramref name="value" /> if that character is found, or -1 if it is not.</returns>
		/// <param name="value">The Unicode character to seek. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060003A0 RID: 928 RVA: 0x000141CD File Offset: 0x000123CD
		public int LastIndexOf(char value)
		{
			return SpanHelpers.LastIndexOf(ref this._firstChar, value, this.Length);
		}

		/// <summary>Reports the zero-based index position of the last occurrence of a specified Unicode character within this instance. The search starts at a specified character position and proceeds backward toward the beginning of the string.</summary>
		/// <returns>The zero-based index position of <paramref name="value" /> if that character is found, or -1 if it is not found or if the current instance equals <see cref="F:System.String.Empty" />.</returns>
		/// <param name="value">The Unicode character to seek. </param>
		/// <param name="startIndex">The starting position of the search. The search proceeds from <paramref name="startIndex" /> toward the beginning of this instance.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The current instance does not equal <see cref="F:System.String.Empty" />, and <paramref name="startIndex" /> is less than zero or greater than or equal to the length of this instance.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060003A1 RID: 929 RVA: 0x000141E1 File Offset: 0x000123E1
		public int LastIndexOf(char value, int startIndex)
		{
			return this.LastIndexOf(value, startIndex, startIndex + 1);
		}

		/// <summary>Reports the zero-based index position of the last occurrence of the specified Unicode character in a substring within this instance. The search starts at a specified character position and proceeds backward toward the beginning of the string for a specified number of character positions.</summary>
		/// <returns>The zero-based index position of <paramref name="value" /> if that character is found, or -1 if it is not found or if the current instance equals <see cref="F:System.String.Empty" />.</returns>
		/// <param name="value">The Unicode character to seek. </param>
		/// <param name="startIndex">The starting position of the search. The search proceeds from <paramref name="startIndex" /> toward the beginning of this instance.</param>
		/// <param name="count">The number of character positions to examine. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The current instance does not equal <see cref="F:System.String.Empty" />, and <paramref name="startIndex" /> is less than zero or greater than or equal to the length of this instance.-or-The current instance does not equal <see cref="F:System.String.Empty" />, and <paramref name="startIndex" /> - <paramref name="count" /> + 1 is less than zero.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060003A2 RID: 930 RVA: 0x000141F0 File Offset: 0x000123F0
		public int LastIndexOf(char value, int startIndex, int count)
		{
			if (this.Length == 0)
			{
				return -1;
			}
			if (startIndex >= this.Length)
			{
				throw new ArgumentOutOfRangeException("startIndex", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			if (count > startIndex + 1)
			{
				throw new ArgumentOutOfRangeException("count", "Count must be positive and count must refer to a location within the string/array/collection.");
			}
			int num = startIndex + 1 - count;
			int num2 = SpanHelpers.LastIndexOf(Unsafe.Add<char>(ref this._firstChar, num), value, count);
			if (num2 != -1)
			{
				return num2 + num;
			}
			return num2;
		}

		/// <summary>Reports the zero-based index position of the last occurrence in this instance of one or more characters specified in a Unicode array.</summary>
		/// <returns>The index position of the last occurrence in this instance where any character in <paramref name="anyOf" /> was found; -1 if no character in <paramref name="anyOf" /> was found.</returns>
		/// <param name="anyOf">A Unicode character array containing one or more characters to seek. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="anyOf" /> is null. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060003A3 RID: 931 RVA: 0x00014259 File Offset: 0x00012459
		public int LastIndexOfAny(char[] anyOf)
		{
			return this.LastIndexOfAny(anyOf, this.Length - 1, this.Length);
		}

		/// <summary>Reports the zero-based index position of the last occurrence in this instance of one or more characters specified in a Unicode array. The search starts at a specified character position and proceeds backward toward the beginning of the string.</summary>
		/// <returns>The index position of the last occurrence in this instance where any character in <paramref name="anyOf" /> was found; -1 if no character in <paramref name="anyOf" /> was found or if the current instance equals <see cref="F:System.String.Empty" />.</returns>
		/// <param name="anyOf">A Unicode character array containing one or more characters to seek. </param>
		/// <param name="startIndex">The search starting position. The search proceeds from <paramref name="startIndex" /> toward the beginning of this instance.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="anyOf" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The current instance does not equal <see cref="F:System.String.Empty" />, and <paramref name="startIndex" /> specifies a position that is not within this instance. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060003A4 RID: 932 RVA: 0x00014270 File Offset: 0x00012470
		public int LastIndexOfAny(char[] anyOf, int startIndex)
		{
			return this.LastIndexOfAny(anyOf, startIndex, startIndex + 1);
		}

		/// <summary>Reports the zero-based index position of the last occurrence in this instance of one or more characters specified in a Unicode array. The search starts at a specified character position and proceeds backward toward the beginning of the string for a specified number of character positions.</summary>
		/// <returns>The index position of the last occurrence in this instance where any character in <paramref name="anyOf" /> was found; -1 if no character in <paramref name="anyOf" /> was found or if the current instance equals <see cref="F:System.String.Empty" />.</returns>
		/// <param name="anyOf">A Unicode character array containing one or more characters to seek. </param>
		/// <param name="startIndex">The search starting position. The search proceeds from <paramref name="startIndex" /> toward the beginning of this instance.</param>
		/// <param name="count">The number of character positions to examine. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="anyOf" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The current instance does not equal <see cref="F:System.String.Empty" />, and <paramref name="count" /> or <paramref name="startIndex" /> is negative.-or- The current instance does not equal <see cref="F:System.String.Empty" />, and <paramref name="startIndex" /> minus <paramref name="count" /> + 1 is less than zero. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060003A5 RID: 933 RVA: 0x00014280 File Offset: 0x00012480
		public int LastIndexOfAny(char[] anyOf, int startIndex, int count)
		{
			if (anyOf == null)
			{
				throw new ArgumentNullException("anyOf");
			}
			if (this.Length == 0)
			{
				return -1;
			}
			if (startIndex >= this.Length)
			{
				throw new ArgumentOutOfRangeException("startIndex", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			if (count < 0 || count - 1 > startIndex)
			{
				throw new ArgumentOutOfRangeException("count", "Count must be positive and count must refer to a location within the string/array/collection.");
			}
			if (anyOf.Length > 1)
			{
				return this.LastIndexOfCharArray(anyOf, startIndex, count);
			}
			if (anyOf.Length == 1)
			{
				return this.LastIndexOf(anyOf[0], startIndex, count);
			}
			return -1;
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x000142FC File Offset: 0x000124FC
		private unsafe int LastIndexOfCharArray(char[] anyOf, int startIndex, int count)
		{
			string.ProbabilisticMap probabilisticMap = default(string.ProbabilisticMap);
			uint* ptr = (uint*)(&probabilisticMap);
			string.InitializeProbabilisticMap(ptr, anyOf);
			fixed (char* ptr2 = &this._firstChar)
			{
				char* ptr3 = ptr2;
				char* ptr4 = ptr3 + startIndex;
				while (count > 0)
				{
					int num = (int)(*ptr4);
					if (string.IsCharBitSet(ptr, (byte)num) && string.IsCharBitSet(ptr, (byte)(num >> 8)) && string.ArrayContains((char)num, anyOf))
					{
						return (int)((long)(ptr4 - ptr3));
					}
					count--;
					ptr4--;
				}
				return -1;
			}
		}

		/// <summary>Reports the zero-based index position of the last occurrence of a specified string within this instance.</summary>
		/// <returns>The zero-based index position of <paramref name="value" /> if that string is found, or -1 if it is not. If <paramref name="value" /> is <see cref="F:System.String.Empty" />, the return value is the last index position in this instance.</returns>
		/// <param name="value">The string to seek. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060003A7 RID: 935 RVA: 0x00014376 File Offset: 0x00012576
		public int LastIndexOf(string value)
		{
			return this.LastIndexOf(value, this.Length - 1, this.Length, StringComparison.CurrentCulture);
		}

		/// <summary>Reports the zero-based index position of the last occurrence of a specified string within this instance. The search starts at a specified character position and proceeds backward toward the beginning of the string.</summary>
		/// <returns>The zero-based index position of <paramref name="value" /> if that string is found, or -1 if it is not found or if the current instance equals <see cref="F:System.String.Empty" />. If <paramref name="value" /> is <see cref="F:System.String.Empty" />, the return value is the smaller of <paramref name="startIndex" /> and the last index position in this instance.</returns>
		/// <param name="value">The string to seek. </param>
		/// <param name="startIndex">The search starting position. The search proceeds from <paramref name="startIndex" /> toward the beginning of this instance.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The current instance does not equal <see cref="F:System.String.Empty" />, and <paramref name="startIndex" /> is less than zero or greater than the length of the current instance. -or-The current instance equals <see cref="F:System.String.Empty" />, and <paramref name="startIndex" /> is greater than zero.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060003A8 RID: 936 RVA: 0x0001438E File Offset: 0x0001258E
		public int LastIndexOf(string value, int startIndex)
		{
			return this.LastIndexOf(value, startIndex, startIndex + 1, StringComparison.CurrentCulture);
		}

		/// <summary>Reports the zero-based index position of the last occurrence of a specified string within this instance. The search starts at a specified character position and proceeds backward toward the beginning of the string for a specified number of character positions.</summary>
		/// <returns>The zero-based index position of <paramref name="value" /> if that string is found, or -1 if it is not found or if the current instance equals <see cref="F:System.String.Empty" />. If <paramref name="value" /> is <see cref="F:System.String.Empty" />, the return value is the smaller of <paramref name="startIndex" /> and the last index position in this instance.</returns>
		/// <param name="value">The string to seek. </param>
		/// <param name="startIndex">The search starting position. The search proceeds from <paramref name="startIndex" /> toward the beginning of this instance.</param>
		/// <param name="count">The number of character positions to examine. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="count" /> is negative.-or-The current instance does not equal <see cref="F:System.String.Empty" />, and  <paramref name="startIndex" /> is negative.-or- The current instance does not equal <see cref="F:System.String.Empty" />, and <paramref name="startIndex" /> is greater than the length of this instance.-or-The current instance does not equal <see cref="F:System.String.Empty" />, and <paramref name="startIndex" /> - <paramref name="count" /> + 1 specifies a position that is not within this instance. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060003A9 RID: 937 RVA: 0x0001439C File Offset: 0x0001259C
		public int LastIndexOf(string value, int startIndex, int count)
		{
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Count must be positive and count must refer to a location within the string/array/collection.");
			}
			return this.LastIndexOf(value, startIndex, count, StringComparison.CurrentCulture);
		}

		/// <summary>Reports the zero-based index of the last occurrence of a specified string within the current <see cref="T:System.String" /> object. A parameter specifies the type of search to use for the specified string.</summary>
		/// <returns>The index position of the <paramref name="value" /> parameter if that string is found, or -1 if it is not. If <paramref name="value" /> is <see cref="F:System.String.Empty" />, the return value is the last index position in this instance.</returns>
		/// <param name="value">The string to seek. </param>
		/// <param name="comparisonType">One of the enumeration values that specifies the rules for the search. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="comparisonType" /> is not a valid <see cref="T:System.StringComparison" /> value.</exception>
		// Token: 0x060003AA RID: 938 RVA: 0x000143BC File Offset: 0x000125BC
		public int LastIndexOf(string value, StringComparison comparisonType)
		{
			return this.LastIndexOf(value, this.Length - 1, this.Length, comparisonType);
		}

		/// <summary>Reports the zero-based index of the last occurrence of a specified string within the current <see cref="T:System.String" /> object. The search starts at a specified character position and proceeds backward toward the beginning of the string. A parameter specifies the type of comparison to perform when searching for the specified string.</summary>
		/// <returns>The index position of the <paramref name="value" /> parameter if that string is found, or -1 if it is not found or if the current instance equals <see cref="F:System.String.Empty" />. If <paramref name="value" /> is <see cref="F:System.String.Empty" />, the return value is the smaller of <paramref name="startIndex" /> and the last index position in this instance.</returns>
		/// <param name="value">The string to seek. </param>
		/// <param name="startIndex">The search starting position. The search proceeds from <paramref name="startIndex" /> toward the beginning of this instance.</param>
		/// <param name="comparisonType">One of the enumeration values that specifies the rules for the search. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The current instance does not equal <see cref="F:System.String.Empty" />, and <paramref name="startIndex" /> is less than zero or greater than the length of the current instance. -or-The current instance equals <see cref="F:System.String.Empty" />, and <paramref name="startIndex" /> is greater than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="comparisonType" /> is not a valid <see cref="T:System.StringComparison" /> value.</exception>
		// Token: 0x060003AB RID: 939 RVA: 0x000143D4 File Offset: 0x000125D4
		public int LastIndexOf(string value, int startIndex, StringComparison comparisonType)
		{
			return this.LastIndexOf(value, startIndex, startIndex + 1, comparisonType);
		}

		/// <summary>Reports the zero-based index position of the last occurrence of a specified string within this instance. The search starts at a specified character position and proceeds backward toward the beginning of the string for the specified number of character positions. A parameter specifies the type of comparison to perform when searching for the specified string.</summary>
		/// <returns>The index position of the <paramref name="value" /> parameter if that string is found, or -1 if it is not found or if the current instance equals <see cref="F:System.String.Empty" />. If <paramref name="value" /> is <see cref="F:System.String.Empty" />, the return value is the smaller of <paramref name="startIndex" /> and the last index position in this instance.</returns>
		/// <param name="value">The string to seek. </param>
		/// <param name="startIndex">The search starting position. The search proceeds from <paramref name="startIndex" /> toward the beginning of this instance.</param>
		/// <param name="count">The number of character positions to examine. </param>
		/// <param name="comparisonType">One of the enumeration values that specifies the rules for the search. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="count" /> is negative.-or-The current instance does not equal <see cref="F:System.String.Empty" />, and <paramref name="startIndex" /> is negative.-or- The current instance does not equal <see cref="F:System.String.Empty" />, and <paramref name="startIndex" /> is greater than the length of this instance.-or-The current instance does not equal <see cref="F:System.String.Empty" />, and <paramref name="startIndex" /> + 1 - <paramref name="count" /> specifies a position that is not within this instance. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="comparisonType" /> is not a valid <see cref="T:System.StringComparison" /> value.</exception>
		// Token: 0x060003AC RID: 940 RVA: 0x000143E4 File Offset: 0x000125E4
		public int LastIndexOf(string value, int startIndex, int count, StringComparison comparisonType)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (this.Length == 0 && (startIndex == -1 || startIndex == 0))
			{
				if (value.Length != 0)
				{
					return -1;
				}
				return 0;
			}
			else
			{
				if (startIndex < 0 || startIndex > this.Length)
				{
					throw new ArgumentOutOfRangeException("startIndex", "Index was out of range. Must be non-negative and less than the size of the collection.");
				}
				if (startIndex == this.Length)
				{
					startIndex--;
					if (count > 0)
					{
						count--;
					}
				}
				if (count < 0 || startIndex - count + 1 < 0)
				{
					throw new ArgumentOutOfRangeException("count", "Count must be positive and count must refer to a location within the string/array/collection.");
				}
				if (value.Length == 0)
				{
					return startIndex;
				}
				switch (comparisonType)
				{
				case StringComparison.CurrentCulture:
					return CultureInfo.CurrentCulture.CompareInfo.LastIndexOf(this, value, startIndex, count, CompareOptions.None);
				case StringComparison.CurrentCultureIgnoreCase:
					return CultureInfo.CurrentCulture.CompareInfo.LastIndexOf(this, value, startIndex, count, CompareOptions.IgnoreCase);
				case StringComparison.InvariantCulture:
					return CompareInfo.Invariant.LastIndexOf(this, value, startIndex, count, CompareOptions.None);
				case StringComparison.InvariantCultureIgnoreCase:
					return CompareInfo.Invariant.LastIndexOf(this, value, startIndex, count, CompareOptions.IgnoreCase);
				case StringComparison.Ordinal:
					return CompareInfo.Invariant.LastIndexOfOrdinal(this, value, startIndex, count, false);
				case StringComparison.OrdinalIgnoreCase:
					return CompareInfo.Invariant.LastIndexOfOrdinal(this, value, startIndex, count, true);
				default:
					throw new ArgumentException("The string comparison type passed in is currently not supported.", "comparisonType");
				}
			}
		}

		/// <summary>Gets the number of characters in the current <see cref="T:System.String" /> object.</summary>
		/// <returns>The number of characters in the current string.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060003AD RID: 941 RVA: 0x0001450E File Offset: 0x0001270E
		public int Length
		{
			get
			{
				return this._stringLength;
			}
		}

		// Token: 0x060003AE RID: 942 RVA: 0x00014518 File Offset: 0x00012718
		internal unsafe int IndexOfUnchecked(string value, int startIndex, int count)
		{
			int length = value.Length;
			if (count < length)
			{
				return -1;
			}
			if (length == 0)
			{
				return startIndex;
			}
			fixed (char* ptr = &this._firstChar)
			{
				char* ptr2 = ptr;
				fixed (string text = value)
				{
					char* ptr3 = text;
					if (ptr3 != null)
					{
						ptr3 += RuntimeHelpers.OffsetToStringData / 2;
					}
					char* ptr4 = ptr2 + startIndex;
					char* ptr5 = ptr4 + count - length + 1;
					while (ptr4 != ptr5)
					{
						if (*ptr4 == *ptr3)
						{
							for (int i = 1; i < length; i++)
							{
								if (ptr4[i] != ptr3[i])
								{
									goto IL_007B;
								}
							}
							return (int)((long)(ptr4 - ptr2));
						}
						IL_007B:
						ptr4++;
					}
					ptr = null;
				}
				return -1;
			}
		}

		/// <summary>Concatenates the string representations of four specified objects and any objects specified in an optional variable length parameter list.</summary>
		/// <returns>The concatenated string representation of each value in the parameter list.</returns>
		/// <param name="arg0">The first object to concatenate. </param>
		/// <param name="arg1">The second object to concatenate. </param>
		/// <param name="arg2">The third object to concatenate. </param>
		/// <param name="arg3">The fourth object to concatenate.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060003AF RID: 943 RVA: 0x000145B3 File Offset: 0x000127B3
		[CLSCompliant(false)]
		public static string Concat(object arg0, object arg1, object arg2, object arg3, __arglist)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x000145BC File Offset: 0x000127BC
		internal unsafe int IndexOfUncheckedIgnoreCase(string value, int startIndex, int count)
		{
			int length = value.Length;
			if (count < length)
			{
				return -1;
			}
			if (length == 0)
			{
				return startIndex;
			}
			TextInfo textInfo = CultureInfo.InvariantCulture.TextInfo;
			fixed (char* ptr = &this._firstChar)
			{
				char* ptr2 = ptr;
				fixed (string text = value)
				{
					char* ptr3 = text;
					if (ptr3 != null)
					{
						ptr3 += RuntimeHelpers.OffsetToStringData / 2;
					}
					char* ptr4 = ptr2 + startIndex;
					char* ptr5 = ptr4 + count - length + 1;
					char c = textInfo.ToUpper(*ptr3);
					while (ptr4 != ptr5)
					{
						if (textInfo.ToUpper(*ptr4) == c)
						{
							for (int i = 1; i < length; i++)
							{
								if (textInfo.ToUpper(ptr4[i]) != textInfo.ToUpper(ptr3[i]))
								{
									goto IL_00A4;
								}
							}
							return (int)((long)(ptr4 - ptr2));
						}
						IL_00A4:
						ptr4++;
					}
					ptr = null;
				}
				return -1;
			}
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x00014684 File Offset: 0x00012884
		internal unsafe int LastIndexOfUnchecked(string value, int startIndex, int count)
		{
			int length = value.Length;
			if (count < length)
			{
				return -1;
			}
			if (length == 0)
			{
				return startIndex;
			}
			fixed (char* ptr = &this._firstChar)
			{
				char* ptr2 = ptr;
				fixed (string text = value)
				{
					char* ptr3 = text;
					if (ptr3 != null)
					{
						ptr3 += RuntimeHelpers.OffsetToStringData / 2;
					}
					char* ptr4 = ptr2 + startIndex;
					char* ptr5 = ptr4 - count + length - 1;
					char* ptr6 = ptr3 + length - 1;
					while (ptr4 != ptr5)
					{
						if (*ptr4 == *ptr6)
						{
							char* ptr7 = ptr4;
							while (ptr3 != ptr6)
							{
								ptr6--;
								ptr4--;
								if (*ptr4 != *ptr6)
								{
									ptr6 = ptr3 + length - 1;
									ptr4 = ptr7;
									goto IL_0092;
								}
							}
							return (int)((long)(ptr4 - ptr2));
						}
						IL_0092:
						ptr4--;
					}
					ptr = null;
				}
				return -1;
			}
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x00014738 File Offset: 0x00012938
		internal unsafe int LastIndexOfUncheckedIgnoreCase(string value, int startIndex, int count)
		{
			int length = value.Length;
			if (count < length)
			{
				return -1;
			}
			if (length == 0)
			{
				return startIndex;
			}
			TextInfo textInfo = CultureInfo.InvariantCulture.TextInfo;
			fixed (char* ptr = &this._firstChar)
			{
				char* ptr2 = ptr;
				fixed (string text = value)
				{
					char* ptr3 = text;
					if (ptr3 != null)
					{
						ptr3 += RuntimeHelpers.OffsetToStringData / 2;
					}
					char* ptr4 = ptr2 + startIndex;
					char* ptr5 = ptr4 - count + length - 1;
					char* ptr6 = ptr3 + length - 1;
					char c = textInfo.ToUpper(*ptr6);
					while (ptr4 != ptr5)
					{
						if (textInfo.ToUpper(*ptr4) == c)
						{
							char* ptr7 = ptr4;
							while (ptr3 != ptr6)
							{
								ptr6--;
								ptr4--;
								if (textInfo.ToUpper(*ptr4) != textInfo.ToUpper(*ptr6))
								{
									ptr6 = ptr3 + length - 1;
									ptr4 = ptr7;
									goto IL_00BB;
								}
							}
							return (int)((long)(ptr4 - ptr2));
						}
						IL_00BB:
						ptr4--;
					}
					ptr = null;
				}
				return -1;
			}
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x00014814 File Offset: 0x00012A14
		internal bool StartsWithOrdinalUnchecked(string value)
		{
			return this.Length >= value.Length && this._firstChar == value._firstChar && (value.Length == 1 || SpanHelpers.SequenceEqual(Unsafe.As<char, byte>(this.GetRawStringData()), Unsafe.As<char, byte>(value.GetRawStringData()), (ulong)((long)value.Length * 2L)));
		}

		// Token: 0x060003B4 RID: 948
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern string FastAllocateString(int length);

		// Token: 0x060003B5 RID: 949
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string InternalIsInterned(string str);

		// Token: 0x060003B6 RID: 950
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string InternalIntern(string str);

		// Token: 0x060003B7 RID: 951 RVA: 0x00014870 File Offset: 0x00012A70
		private unsafe static int FastCompareStringHelper(uint* strAChars, int countA, uint* strBChars, int countB)
		{
			char* ptr = (char*)strAChars;
			char* ptr2 = (char*)strBChars;
			char* ptr3 = ptr + Math.Min(countA, countB);
			while (ptr < ptr3)
			{
				if (*ptr != *ptr2)
				{
					return (int)(*ptr - *ptr2);
				}
				ptr++;
				ptr2++;
			}
			return countA - countB;
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x000148AC File Offset: 0x00012AAC
		private unsafe static void memset(byte* dest, int val, int len)
		{
			if (len < 8)
			{
				while (len != 0)
				{
					*dest = (byte)val;
					dest++;
					len--;
				}
				return;
			}
			if (val != 0)
			{
				val |= val << 8;
				val |= val << 16;
			}
			int num = dest & 3;
			if (num != 0)
			{
				num = 4 - num;
				len -= num;
				do
				{
					*dest = (byte)val;
					dest++;
					num--;
				}
				while (num != 0);
			}
			while (len >= 16)
			{
				*(int*)dest = val;
				*(int*)(dest + 4) = val;
				*(int*)(dest + (IntPtr)2 * 4) = val;
				*(int*)(dest + (IntPtr)3 * 4) = val;
				dest += 16;
				len -= 16;
			}
			while (len >= 4)
			{
				*(int*)dest = val;
				dest += 4;
				len -= 4;
			}
			while (len > 0)
			{
				*dest = (byte)val;
				dest++;
				len--;
			}
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x00014956 File Offset: 0x00012B56
		private unsafe static void memcpy(byte* dest, byte* src, int size)
		{
			Buffer.Memcpy(dest, src, size);
		}

		// Token: 0x060003BA RID: 954 RVA: 0x00014960 File Offset: 0x00012B60
		internal unsafe static void bzero(byte* dest, int len)
		{
			string.memset(dest, 0, len);
		}

		// Token: 0x060003BB RID: 955 RVA: 0x0001496A File Offset: 0x00012B6A
		internal unsafe static void bzero_aligned_1(byte* dest, int len)
		{
			*dest = 0;
		}

		// Token: 0x060003BC RID: 956 RVA: 0x0001496F File Offset: 0x00012B6F
		internal unsafe static void bzero_aligned_2(byte* dest, int len)
		{
			*(short*)dest = 0;
		}

		// Token: 0x060003BD RID: 957 RVA: 0x00014974 File Offset: 0x00012B74
		internal unsafe static void bzero_aligned_4(byte* dest, int len)
		{
			*(int*)dest = 0;
		}

		// Token: 0x060003BE RID: 958 RVA: 0x00014979 File Offset: 0x00012B79
		internal unsafe static void bzero_aligned_8(byte* dest, int len)
		{
			*(long*)dest = 0L;
		}

		// Token: 0x060003BF RID: 959 RVA: 0x0001497F File Offset: 0x00012B7F
		internal unsafe static void memcpy_aligned_1(byte* dest, byte* src, int size)
		{
			*dest = *src;
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x00014985 File Offset: 0x00012B85
		internal unsafe static void memcpy_aligned_2(byte* dest, byte* src, int size)
		{
			*(short*)dest = *(short*)src;
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x0001498B File Offset: 0x00012B8B
		internal unsafe static void memcpy_aligned_4(byte* dest, byte* src, int size)
		{
			*(int*)dest = *(int*)src;
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x00014991 File Offset: 0x00012B91
		internal unsafe static void memcpy_aligned_8(byte* dest, byte* src, int size)
		{
			*(long*)dest = *(long*)src;
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x00014997 File Offset: 0x00012B97
		private unsafe string CreateString(sbyte* value)
		{
			return string.Ctor(value);
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x0001499F File Offset: 0x00012B9F
		private unsafe string CreateString(sbyte* value, int startIndex, int length)
		{
			return string.Ctor(value, startIndex, length);
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x000149A9 File Offset: 0x00012BA9
		private unsafe string CreateString(char* value)
		{
			return string.Ctor(value);
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x000149B1 File Offset: 0x00012BB1
		private unsafe string CreateString(char* value, int startIndex, int length)
		{
			return string.Ctor(value, startIndex, length);
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x000149BB File Offset: 0x00012BBB
		private string CreateString(char[] val, int startIndex, int length)
		{
			return string.Ctor(val, startIndex, length);
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x000149C5 File Offset: 0x00012BC5
		private string CreateString(char[] val)
		{
			return string.Ctor(val);
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x000149CD File Offset: 0x00012BCD
		private string CreateString(char c, int count)
		{
			return string.Ctor(c, count);
		}

		// Token: 0x060003CA RID: 970 RVA: 0x000149D6 File Offset: 0x00012BD6
		private unsafe string CreateString(sbyte* value, int startIndex, int length, Encoding enc)
		{
			return string.Ctor(value, startIndex, length, enc);
		}

		// Token: 0x060003CB RID: 971 RVA: 0x000149E2 File Offset: 0x00012BE2
		private string CreateString(ReadOnlySpan<char> value)
		{
			return string.Ctor(value);
		}

		/// <summary>Gets the <see cref="T:System.Char" /> object at a specified position in the current <see cref="T:System.String" /> object.</summary>
		/// <returns>The object at position <paramref name="index" />.</returns>
		/// <param name="index">A position in the current string. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">
		///   <paramref name="index" /> is greater than or equal to the length of this object or less than zero. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700004C RID: 76
		[IndexerName("Chars")]
		public unsafe char this[int index]
		{
			[Intrinsic]
			get
			{
				if ((ulong)index >= (ulong)((long)this._stringLength))
				{
					ThrowHelper.ThrowIndexOutOfRangeException();
				}
				return *Unsafe.Add<char>(ref this._firstChar, index);
			}
		}

		/// <summary>Retrieves the system's reference to the specified <see cref="T:System.String" />.</summary>
		/// <returns>The system's reference to <paramref name="str" />, if it is interned; otherwise, a new reference to a string with the value of <paramref name="str" />.</returns>
		/// <param name="str">A string to search for in the intern pool. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="str" /> is null. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060003CD RID: 973 RVA: 0x00014A09 File Offset: 0x00012C09
		public static string Intern(string str)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			return string.InternalIntern(str);
		}

		/// <summary>Retrieves a reference to a specified <see cref="T:System.String" />.</summary>
		/// <returns>A reference to <paramref name="str" /> if it is in the common language runtime intern pool; otherwise, null.</returns>
		/// <param name="str">The string to search for in the intern pool. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="str" /> is null. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060003CE RID: 974 RVA: 0x00014A1F File Offset: 0x00012C1F
		public static string IsInterned(string str)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			return string.InternalIsInterned(str);
		}

		// Token: 0x060003CF RID: 975 RVA: 0x00014A38 File Offset: 0x00012C38
		private unsafe int LegacyStringGetHashCode()
		{
			int num = 5381;
			int num2 = num;
			fixed (string text = this)
			{
				char* ptr = text;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				char* ptr2 = ptr;
				int num3;
				while ((num3 = (int)(*ptr2)) != 0)
				{
					num = ((num << 5) + num) ^ num3;
					num3 = (int)ptr2[1];
					if (num3 == 0)
					{
						break;
					}
					num2 = ((num2 << 5) + num2) ^ num3;
					ptr2 += 2;
				}
			}
			return num + num2 * 1566083941;
		}

		// Token: 0x0400027A RID: 634
		private const int StackallocIntBufferSizeLimit = 128;

		// Token: 0x0400027B RID: 635
		private const int PROBABILISTICMAP_BLOCK_INDEX_MASK = 7;

		// Token: 0x0400027C RID: 636
		private const int PROBABILISTICMAP_BLOCK_INDEX_SHIFT = 3;

		// Token: 0x0400027D RID: 637
		private const int PROBABILISTICMAP_SIZE = 8;

		// Token: 0x0400027E RID: 638
		[NonSerialized]
		private int _stringLength;

		// Token: 0x0400027F RID: 639
		[NonSerialized]
		private char _firstChar;

		/// <summary>Represents the empty string. This field is read-only.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x04000280 RID: 640
		public static readonly string Empty;

		// Token: 0x02000099 RID: 153
		private enum TrimType
		{
			// Token: 0x04000282 RID: 642
			Head,
			// Token: 0x04000283 RID: 643
			Tail,
			// Token: 0x04000284 RID: 644
			Both
		}

		// Token: 0x0200009A RID: 154
		[StructLayout(LayoutKind.Explicit, Size = 32)]
		private struct ProbabilisticMap
		{
		}
	}
}
