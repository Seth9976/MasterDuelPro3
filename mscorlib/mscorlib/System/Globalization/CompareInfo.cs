using System;
using System.Buffers;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Threading;
using Mono.Globalization.Unicode;
using Unity;

namespace System.Globalization
{
	/// <summary>Implements a set of methods for culture-sensitive string comparisons.</summary>
	// Token: 0x0200068D RID: 1677
	[Serializable]
	public class CompareInfo : IDeserializationCallback
	{
		// Token: 0x06003472 RID: 13426 RVA: 0x000C8454 File Offset: 0x000C6654
		internal unsafe static int InvariantIndexOf(string source, string value, int startIndex, int count, bool ignoreCase)
		{
			char* ptr = source;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			char* ptr2 = value;
			if (ptr2 != null)
			{
				ptr2 += RuntimeHelpers.OffsetToStringData / 2;
			}
			int num = CompareInfo.InvariantFindString(ptr + startIndex, count, ptr2, value.Length, ignoreCase, true);
			if (num >= 0)
			{
				return num + startIndex;
			}
			return -1;
		}

		// Token: 0x06003473 RID: 13427 RVA: 0x000C84A8 File Offset: 0x000C66A8
		internal unsafe static int InvariantLastIndexOf(string source, string value, int startIndex, int count, bool ignoreCase)
		{
			char* ptr = source;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			char* ptr2 = value;
			if (ptr2 != null)
			{
				ptr2 += RuntimeHelpers.OffsetToStringData / 2;
			}
			int num = CompareInfo.InvariantFindString(ptr + (startIndex - count + 1), count, ptr2, value.Length, ignoreCase, false);
			if (num >= 0)
			{
				return num + startIndex - count + 1;
			}
			return -1;
		}

		// Token: 0x06003474 RID: 13428 RVA: 0x000C8504 File Offset: 0x000C6704
		private unsafe static int InvariantFindString(char* source, int sourceCount, char* value, int valueCount, bool ignoreCase, bool start)
		{
			if (valueCount == 0)
			{
				if (!start)
				{
					return sourceCount - 1;
				}
				return 0;
			}
			else
			{
				if (sourceCount < valueCount)
				{
					return -1;
				}
				if (start)
				{
					int num = sourceCount - valueCount;
					if (ignoreCase)
					{
						char c = CompareInfo.InvariantToUpper(*value);
						for (int i = 0; i <= num; i++)
						{
							if (CompareInfo.InvariantToUpper(source[i]) == c)
							{
								int j;
								for (j = 1; j < valueCount; j++)
								{
									char c2 = CompareInfo.InvariantToUpper(source[i + j]);
									char c3 = CompareInfo.InvariantToUpper(value[j]);
									if (c2 != c3)
									{
										break;
									}
								}
								if (j == valueCount)
								{
									return i;
								}
							}
						}
					}
					else
					{
						char c4 = *value;
						for (int i = 0; i <= num; i++)
						{
							if (source[i] == c4)
							{
								int j;
								for (j = 1; j < valueCount; j++)
								{
									char c5 = source[i + j];
									char c3 = value[j];
									if (c5 != c3)
									{
										break;
									}
								}
								if (j == valueCount)
								{
									return i;
								}
							}
						}
					}
				}
				else
				{
					int num = sourceCount - valueCount;
					if (ignoreCase)
					{
						char c6 = CompareInfo.InvariantToUpper(*value);
						for (int i = num; i >= 0; i--)
						{
							if (CompareInfo.InvariantToUpper(source[i]) == c6)
							{
								int j;
								for (j = 1; j < valueCount; j++)
								{
									char c7 = CompareInfo.InvariantToUpper(source[i + j]);
									char c3 = CompareInfo.InvariantToUpper(value[j]);
									if (c7 != c3)
									{
										break;
									}
								}
								if (j == valueCount)
								{
									return i;
								}
							}
						}
					}
					else
					{
						char c8 = *value;
						for (int i = num; i >= 0; i--)
						{
							if (source[i] == c8)
							{
								int j;
								for (j = 1; j < valueCount; j++)
								{
									char c9 = source[i + j];
									char c3 = value[j];
									if (c9 != c3)
									{
										break;
									}
								}
								if (j == valueCount)
								{
									return i;
								}
							}
						}
					}
				}
				return -1;
			}
		}

		// Token: 0x06003475 RID: 13429 RVA: 0x000C8678 File Offset: 0x000C6878
		private static char InvariantToUpper(char c)
		{
			if (c - 'a' > '\u0019')
			{
				return c;
			}
			return c - ' ';
		}

		// Token: 0x06003476 RID: 13430 RVA: 0x000C868C File Offset: 0x000C688C
		private unsafe SortKey InvariantCreateSortKey(string source, CompareOptions options)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if ((options & ~(CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth | CompareOptions.StringSort)) != CompareOptions.None)
			{
				throw new ArgumentException("Value of flags is invalid.", "options");
			}
			byte[] array;
			if (source.Length == 0)
			{
				array = Array.Empty<byte>();
			}
			else
			{
				array = new byte[source.Length * 2];
				fixed (string text = source)
				{
					char* ptr = text;
					if (ptr != null)
					{
						ptr += RuntimeHelpers.OffsetToStringData / 2;
					}
					byte[] array2;
					byte* ptr2;
					if ((array2 = array) == null || array2.Length == 0)
					{
						ptr2 = null;
					}
					else
					{
						ptr2 = &array2[0];
					}
					if ((options & (CompareOptions.IgnoreCase | CompareOptions.OrdinalIgnoreCase)) != CompareOptions.None)
					{
						short* ptr3 = (short*)ptr2;
						for (int i = 0; i < source.Length; i++)
						{
							ptr3[i] = (short)CompareInfo.InvariantToUpper(source[i]);
						}
					}
					else
					{
						Buffer.MemoryCopy((void*)ptr, (void*)ptr2, (long)array.Length, (long)array.Length);
					}
					array2 = null;
				}
			}
			return new SortKey(this.Name, source, options, array);
		}

		// Token: 0x06003477 RID: 13431 RVA: 0x000C8768 File Offset: 0x000C6968
		internal CompareInfo(CultureInfo culture)
		{
			this.m_name = culture._name;
			this.InitSort(culture);
		}

		/// <summary>Initializes a new <see cref="T:System.Globalization.CompareInfo" /> object that is associated with the culture with the specified name.</summary>
		/// <returns>A new <see cref="T:System.Globalization.CompareInfo" /> object associated with the culture with the specified identifier and using string comparison methods in the current <see cref="T:System.Reflection.Assembly" />.</returns>
		/// <param name="name">A string representing the culture name. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is an invalid culture name. </exception>
		// Token: 0x06003478 RID: 13432 RVA: 0x000C8783 File Offset: 0x000C6983
		public static CompareInfo GetCompareInfo(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			return CultureInfo.GetCultureInfo(name).CompareInfo;
		}

		// Token: 0x06003479 RID: 13433 RVA: 0x000C879E File Offset: 0x000C699E
		[OnDeserializing]
		private void OnDeserializing(StreamingContext ctx)
		{
			this.m_name = null;
		}

		/// <summary>Runs when the entire object graph has been deserialized.</summary>
		/// <param name="sender">The object that initiated the callback. </param>
		// Token: 0x0600347A RID: 13434 RVA: 0x000C87A7 File Offset: 0x000C69A7
		void IDeserializationCallback.OnDeserialization(object sender)
		{
			this.OnDeserialized();
		}

		// Token: 0x0600347B RID: 13435 RVA: 0x000C87A7 File Offset: 0x000C69A7
		[OnDeserialized]
		private void OnDeserialized(StreamingContext ctx)
		{
			this.OnDeserialized();
		}

		// Token: 0x0600347C RID: 13436 RVA: 0x000C87B0 File Offset: 0x000C69B0
		private void OnDeserialized()
		{
			if (this.m_name == null)
			{
				CultureInfo cultureInfo = CultureInfo.GetCultureInfo(this.culture);
				this.m_name = cultureInfo._name;
				return;
			}
			this.InitSort(CultureInfo.GetCultureInfo(this.m_name));
		}

		// Token: 0x0600347D RID: 13437 RVA: 0x000C87EF File Offset: 0x000C69EF
		[OnSerializing]
		private void OnSerializing(StreamingContext ctx)
		{
			this.culture = CultureInfo.GetCultureInfo(this.Name).LCID;
		}

		/// <summary>Gets the name of the culture used for sorting operations by this <see cref="T:System.Globalization.CompareInfo" /> object.</summary>
		/// <returns>The name of a culture.</returns>
		// Token: 0x170007AC RID: 1964
		// (get) Token: 0x0600347E RID: 13438 RVA: 0x000C8807 File Offset: 0x000C6A07
		public virtual string Name
		{
			get
			{
				if (this.m_name == "zh-CHT" || this.m_name == "zh-CHS")
				{
					return this.m_name;
				}
				return this._sortName;
			}
		}

		/// <summary>Compares two strings. </summary>
		/// <returns>A 32-bit signed integer indicating the lexical relationship between the two comparands.Value Condition zero The two strings are equal. less than zero <paramref name="string1" /> is less than <paramref name="string2" />. greater than zero <paramref name="string1" /> is greater than <paramref name="string2" />. </returns>
		/// <param name="string1">The first string to compare. </param>
		/// <param name="string2">The second string to compare. </param>
		// Token: 0x0600347F RID: 13439 RVA: 0x000C883A File Offset: 0x000C6A3A
		public virtual int Compare(string string1, string string2)
		{
			return this.Compare(string1, string2, CompareOptions.None);
		}

		/// <summary>Compares two strings using the specified <see cref="T:System.Globalization.CompareOptions" /> value.</summary>
		/// <returns>A 32-bit signed integer indicating the lexical relationship between the two comparands.Value Condition zero The two strings are equal. less than zero <paramref name="string1" /> is less than <paramref name="string2" />. greater than zero <paramref name="string1" /> is greater than <paramref name="string2" />. </returns>
		/// <param name="string1">The first string to compare. </param>
		/// <param name="string2">The second string to compare. </param>
		/// <param name="options">A value that defines how <paramref name="string1" /> and <paramref name="string2" /> should be compared. <paramref name="options" /> is either the enumeration value <see cref="F:System.Globalization.CompareOptions.Ordinal" />, or a bitwise combination of one or more of the following values: <see cref="F:System.Globalization.CompareOptions.IgnoreCase" />, <see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />, <see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />, <see cref="F:System.Globalization.CompareOptions.IgnoreWidth" />, <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />, and <see cref="F:System.Globalization.CompareOptions.StringSort" />.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> contains an invalid <see cref="T:System.Globalization.CompareOptions" /> value. </exception>
		// Token: 0x06003480 RID: 13440 RVA: 0x000C8848 File Offset: 0x000C6A48
		public virtual int Compare(string string1, string string2, CompareOptions options)
		{
			if (options == CompareOptions.OrdinalIgnoreCase)
			{
				return string.Compare(string1, string2, StringComparison.OrdinalIgnoreCase);
			}
			if ((options & CompareOptions.Ordinal) != CompareOptions.None)
			{
				if (options != CompareOptions.Ordinal)
				{
					throw new ArgumentException("CompareOption.Ordinal cannot be used with other options.", "options");
				}
				return string.CompareOrdinal(string1, string2);
			}
			else
			{
				if ((options & ~(CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth | CompareOptions.StringSort)) != CompareOptions.None)
				{
					throw new ArgumentException("Value of flags is invalid.", "options");
				}
				if (string1 == null)
				{
					if (string2 == null)
					{
						return 0;
					}
					return -1;
				}
				else
				{
					if (string2 == null)
					{
						return 1;
					}
					if (!GlobalizationMode.Invariant)
					{
						return this.internal_compare_switch(string1, 0, string1.Length, string2, 0, string2.Length, options);
					}
					if ((options & CompareOptions.IgnoreCase) != CompareOptions.None)
					{
						return CompareInfo.CompareOrdinalIgnoreCase(string1, string2);
					}
					return string.CompareOrdinal(string1, string2);
				}
			}
		}

		// Token: 0x06003481 RID: 13441 RVA: 0x000C88F4 File Offset: 0x000C6AF4
		internal int Compare(ReadOnlySpan<char> string1, string string2, CompareOptions options)
		{
			if (options == CompareOptions.OrdinalIgnoreCase)
			{
				return CompareInfo.CompareOrdinalIgnoreCase(string1, string2.AsSpan());
			}
			if ((options & CompareOptions.Ordinal) != CompareOptions.None)
			{
				if (options != CompareOptions.Ordinal)
				{
					throw new ArgumentException("CompareOption.Ordinal cannot be used with other options.", "options");
				}
				return string.CompareOrdinal(string1, string2.AsSpan());
			}
			else
			{
				if ((options & ~(CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth | CompareOptions.StringSort)) != CompareOptions.None)
				{
					throw new ArgumentException("Value of flags is invalid.", "options");
				}
				if (string2 == null)
				{
					return 1;
				}
				if (!GlobalizationMode.Invariant)
				{
					return this.CompareString(string1, string2, options);
				}
				if ((options & CompareOptions.IgnoreCase) == CompareOptions.None)
				{
					return string.CompareOrdinal(string1, string2.AsSpan());
				}
				return CompareInfo.CompareOrdinalIgnoreCase(string1, string2.AsSpan());
			}
		}

		// Token: 0x06003482 RID: 13442 RVA: 0x000C8991 File Offset: 0x000C6B91
		internal int CompareOptionIgnoreCase(ReadOnlySpan<char> string1, ReadOnlySpan<char> string2)
		{
			if (string1.Length == 0 || string2.Length == 0)
			{
				return string1.Length - string2.Length;
			}
			if (!GlobalizationMode.Invariant)
			{
				return this.CompareString(string1, string2, CompareOptions.IgnoreCase);
			}
			return CompareInfo.CompareOrdinalIgnoreCase(string1, string2);
		}

		/// <summary>Compares a section of one string with a section of another string using the specified <see cref="T:System.Globalization.CompareOptions" /> value.</summary>
		/// <returns>A 32-bit signed integer indicating the lexical relationship between the two comparands.Value Condition zero The two strings are equal. less than zero The specified section of <paramref name="string1" /> is less than the specified section of <paramref name="string2" />. greater than zero The specified section of <paramref name="string1" /> is greater than the specified section of <paramref name="string2" />. </returns>
		/// <param name="string1">The first string to compare. </param>
		/// <param name="offset1">The zero-based index of the character in <paramref name="string1" /> at which to start comparing. </param>
		/// <param name="length1">The number of consecutive characters in <paramref name="string1" /> to compare. </param>
		/// <param name="string2">The second string to compare. </param>
		/// <param name="offset2">The zero-based index of the character in <paramref name="string2" /> at which to start comparing. </param>
		/// <param name="length2">The number of consecutive characters in <paramref name="string2" /> to compare. </param>
		/// <param name="options">A value that defines how <paramref name="string1" /> and <paramref name="string2" /> should be compared. <paramref name="options" /> is either the enumeration value <see cref="F:System.Globalization.CompareOptions.Ordinal" />, or a bitwise combination of one or more of the following values: <see cref="F:System.Globalization.CompareOptions.IgnoreCase" />, <see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />, <see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />, <see cref="F:System.Globalization.CompareOptions.IgnoreWidth" />, <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />, and <see cref="F:System.Globalization.CompareOptions.StringSort" />.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset1" /> or <paramref name="length1" /> or <paramref name="offset2" /> or <paramref name="length2" /> is less than zero.-or- <paramref name="offset1" /> is greater than or equal to the number of characters in <paramref name="string1" />.-or- <paramref name="offset2" /> is greater than or equal to the number of characters in <paramref name="string2" />.-or- <paramref name="length1" /> is greater than the number of characters from <paramref name="offset1" /> to the end of <paramref name="string1" />.-or- <paramref name="length2" /> is greater than the number of characters from <paramref name="offset2" /> to the end of <paramref name="string2" />. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> contains an invalid <see cref="T:System.Globalization.CompareOptions" /> value. </exception>
		// Token: 0x06003483 RID: 13443 RVA: 0x000C89D0 File Offset: 0x000C6BD0
		public virtual int Compare(string string1, int offset1, int length1, string string2, int offset2, int length2, CompareOptions options)
		{
			if (options == CompareOptions.OrdinalIgnoreCase)
			{
				int num = string.Compare(string1, offset1, string2, offset2, (length1 < length2) ? length1 : length2, StringComparison.OrdinalIgnoreCase);
				if (length1 == length2 || num != 0)
				{
					return num;
				}
				if (length1 <= length2)
				{
					return -1;
				}
				return 1;
			}
			else
			{
				if (length1 < 0 || length2 < 0)
				{
					throw new ArgumentOutOfRangeException((length1 < 0) ? "length1" : "length2", "Positive number required.");
				}
				if (offset1 < 0 || offset2 < 0)
				{
					throw new ArgumentOutOfRangeException((offset1 < 0) ? "offset1" : "offset2", "Positive number required.");
				}
				if (offset1 > ((string1 == null) ? 0 : string1.Length) - length1)
				{
					throw new ArgumentOutOfRangeException("string1", "Offset and length must refer to a position in the string.");
				}
				if (offset2 > ((string2 == null) ? 0 : string2.Length) - length2)
				{
					throw new ArgumentOutOfRangeException("string2", "Offset and length must refer to a position in the string.");
				}
				if ((options & CompareOptions.Ordinal) != CompareOptions.None)
				{
					if (options != CompareOptions.Ordinal)
					{
						throw new ArgumentException("CompareOption.Ordinal cannot be used with other options.", "options");
					}
				}
				else if ((options & ~(CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth | CompareOptions.StringSort)) != CompareOptions.None)
				{
					throw new ArgumentException("Value of flags is invalid.", "options");
				}
				if (string1 == null)
				{
					if (string2 == null)
					{
						return 0;
					}
					return -1;
				}
				else
				{
					if (string2 == null)
					{
						return 1;
					}
					ReadOnlySpan<char> readOnlySpan = string1.AsSpan(offset1, length1);
					ReadOnlySpan<char> readOnlySpan2 = string2.AsSpan(offset2, length2);
					if (options == CompareOptions.Ordinal)
					{
						return string.CompareOrdinal(readOnlySpan, readOnlySpan2);
					}
					if (!GlobalizationMode.Invariant)
					{
						return this.internal_compare_switch(string1, offset1, length1, string2, offset2, length2, options);
					}
					if ((options & CompareOptions.IgnoreCase) != CompareOptions.None)
					{
						return CompareInfo.CompareOrdinalIgnoreCase(readOnlySpan, readOnlySpan2);
					}
					return string.CompareOrdinal(readOnlySpan, readOnlySpan2);
				}
			}
		}

		// Token: 0x06003484 RID: 13444 RVA: 0x000C8B40 File Offset: 0x000C6D40
		internal static int CompareOrdinalIgnoreCase(string strA, int indexA, int lengthA, string strB, int indexB, int lengthB)
		{
			return CompareInfo.CompareOrdinalIgnoreCase(strA.AsSpan(indexA, lengthA), strB.AsSpan(indexB, lengthB));
		}

		// Token: 0x06003485 RID: 13445 RVA: 0x000C8B5C File Offset: 0x000C6D5C
		internal unsafe static int CompareOrdinalIgnoreCase(ReadOnlySpan<char> strA, ReadOnlySpan<char> strB)
		{
			int num = Math.Min(strA.Length, strB.Length);
			int num2 = num;
			fixed (char* reference = MemoryMarshal.GetReference<char>(strA))
			{
				char* ptr = reference;
				fixed (char* reference2 = MemoryMarshal.GetReference<char>(strB))
				{
					char* ptr2 = reference2;
					char* ptr3 = ptr;
					char* ptr4 = ptr2;
					char c = (GlobalizationMode.Invariant ? char.MaxValue : '\u007f');
					while (num != 0 && *ptr3 <= c && *ptr4 <= c)
					{
						int num3 = (int)(*ptr3);
						int num4 = (int)(*ptr4);
						if (num3 == num4)
						{
							ptr3++;
							ptr4++;
							num--;
						}
						else
						{
							if (num3 - 97 <= 25)
							{
								num3 -= 32;
							}
							if (num4 - 97 <= 25)
							{
								num4 -= 32;
							}
							if (num3 != num4)
							{
								return num3 - num4;
							}
							ptr3++;
							ptr4++;
							num--;
						}
					}
					if (num == 0)
					{
						return strA.Length - strB.Length;
					}
					num2 -= num;
					return CompareInfo.CompareStringOrdinalIgnoreCase(ptr3, strA.Length - num2, ptr4, strB.Length - num2);
				}
			}
		}

		/// <summary>Determines whether the specified source string starts with the specified prefix using the specified <see cref="T:System.Globalization.CompareOptions" /> value.</summary>
		/// <returns>true if the length of <paramref name="prefix" /> is less than or equal to the length of <paramref name="source" /> and <paramref name="source" /> starts with <paramref name="prefix" />; otherwise, false.</returns>
		/// <param name="source">The string to search in. </param>
		/// <param name="prefix">The string to compare with the beginning of <paramref name="source" />. </param>
		/// <param name="options">A value that defines how <paramref name="source" /> and <paramref name="prefix" /> should be compared. <paramref name="options" /> is either the enumeration value <see cref="F:System.Globalization.CompareOptions.Ordinal" />, or a bitwise combination of one or more of the following values: <see cref="F:System.Globalization.CompareOptions.IgnoreCase" />, <see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />, <see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />, <see cref="F:System.Globalization.CompareOptions.IgnoreWidth" />, and <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null.-or- <paramref name="prefix" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> contains an invalid <see cref="T:System.Globalization.CompareOptions" /> value. </exception>
		// Token: 0x06003486 RID: 13446 RVA: 0x000C8C50 File Offset: 0x000C6E50
		public virtual bool IsPrefix(string source, string prefix, CompareOptions options)
		{
			if (source == null || prefix == null)
			{
				throw new ArgumentNullException((source == null) ? "source" : "prefix", "String reference not set to an instance of a String.");
			}
			if (prefix.Length == 0)
			{
				return true;
			}
			if (source.Length == 0)
			{
				return false;
			}
			if (options == CompareOptions.OrdinalIgnoreCase)
			{
				return source.StartsWith(prefix, StringComparison.OrdinalIgnoreCase);
			}
			if (options == CompareOptions.Ordinal)
			{
				return source.StartsWith(prefix, StringComparison.Ordinal);
			}
			if ((options & ~(CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth)) != CompareOptions.None)
			{
				throw new ArgumentException("Value of flags is invalid.", "options");
			}
			if (GlobalizationMode.Invariant)
			{
				return source.StartsWith(prefix, ((options & CompareOptions.IgnoreCase) != CompareOptions.None) ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal);
			}
			return this.StartsWith(source, prefix, options);
		}

		/// <summary>Determines whether the specified source string ends with the specified suffix using the specified <see cref="T:System.Globalization.CompareOptions" /> value.</summary>
		/// <returns>true if the length of <paramref name="suffix" /> is less than or equal to the length of <paramref name="source" /> and <paramref name="source" /> ends with <paramref name="suffix" />; otherwise, false.</returns>
		/// <param name="source">The string to search in. </param>
		/// <param name="suffix">The string to compare with the end of <paramref name="source" />. </param>
		/// <param name="options">A value that defines how <paramref name="source" /> and <paramref name="suffix" /> should be compared. <paramref name="options" /> is either the enumeration value <see cref="F:System.Globalization.CompareOptions.Ordinal" /> used by itself, or the bitwise combination of one or more of the following values: <see cref="F:System.Globalization.CompareOptions.IgnoreCase" />, <see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />, <see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />, <see cref="F:System.Globalization.CompareOptions.IgnoreWidth" />, and <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null.-or- <paramref name="suffix" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> contains an invalid <see cref="T:System.Globalization.CompareOptions" /> value. </exception>
		// Token: 0x06003487 RID: 13447 RVA: 0x000C8CEC File Offset: 0x000C6EEC
		public virtual bool IsSuffix(string source, string suffix, CompareOptions options)
		{
			if (source == null || suffix == null)
			{
				throw new ArgumentNullException((source == null) ? "source" : "suffix", "String reference not set to an instance of a String.");
			}
			if (suffix.Length == 0)
			{
				return true;
			}
			if (source.Length == 0)
			{
				return false;
			}
			if (options == CompareOptions.OrdinalIgnoreCase)
			{
				return source.EndsWith(suffix, StringComparison.OrdinalIgnoreCase);
			}
			if (options == CompareOptions.Ordinal)
			{
				return source.EndsWith(suffix, StringComparison.Ordinal);
			}
			if ((options & ~(CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth)) != CompareOptions.None)
			{
				throw new ArgumentException("Value of flags is invalid.", "options");
			}
			if (GlobalizationMode.Invariant)
			{
				return source.EndsWith(suffix, ((options & CompareOptions.IgnoreCase) != CompareOptions.None) ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal);
			}
			return this.EndsWith(source, suffix, options);
		}

		// Token: 0x06003488 RID: 13448 RVA: 0x000C8D86 File Offset: 0x000C6F86
		internal bool IsSuffix(ReadOnlySpan<char> source, ReadOnlySpan<char> suffix, CompareOptions options)
		{
			return this.EndsWith(source, suffix, options);
		}

		/// <summary>Searches for the specified character and returns the zero-based index of the first occurrence within the entire source string using the specified <see cref="T:System.Globalization.CompareOptions" /> value.</summary>
		/// <returns>The zero-based index of the first occurrence of <paramref name="value" />, if found, within <paramref name="source" />, using the specified comparison options; otherwise, -1. Returns 0 (zero) if <paramref name="value" /> is an ignorable character.</returns>
		/// <param name="source">The string to search. </param>
		/// <param name="value">The character to locate within <paramref name="source" />. </param>
		/// <param name="options">A value that defines how the strings should be compared. <paramref name="options" /> is either the enumeration value <see cref="F:System.Globalization.CompareOptions.Ordinal" />, or a bitwise combination of one or more of the following values: <see cref="F:System.Globalization.CompareOptions.IgnoreCase" />, <see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />, <see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />, <see cref="F:System.Globalization.CompareOptions.IgnoreWidth" />, and <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> contains an invalid <see cref="T:System.Globalization.CompareOptions" /> value. </exception>
		// Token: 0x06003489 RID: 13449 RVA: 0x000C8D91 File Offset: 0x000C6F91
		public virtual int IndexOf(string source, char value, CompareOptions options)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return this.IndexOf(source, value, 0, source.Length, options);
		}

		/// <summary>Searches for the specified substring and returns the zero-based index of the first occurrence within the entire source string using the specified <see cref="T:System.Globalization.CompareOptions" /> value.</summary>
		/// <returns>The zero-based index of the first occurrence of <paramref name="value" />, if found, within <paramref name="source" />, using the specified comparison options; otherwise, -1. Returns 0 (zero) if <paramref name="value" /> is an ignorable character.</returns>
		/// <param name="source">The string to search. </param>
		/// <param name="value">The string to locate within <paramref name="source" />. </param>
		/// <param name="options">A value that defines how <paramref name="source" /> and <paramref name="value" /> should be compared. <paramref name="options" /> is either the enumeration value <see cref="F:System.Globalization.CompareOptions.Ordinal" />, or a bitwise combination of one or more of the following values: <see cref="F:System.Globalization.CompareOptions.IgnoreCase" />, <see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />, <see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />, <see cref="F:System.Globalization.CompareOptions.IgnoreWidth" />, and <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null.-or- <paramref name="value" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> contains an invalid <see cref="T:System.Globalization.CompareOptions" /> value. </exception>
		// Token: 0x0600348A RID: 13450 RVA: 0x000C8DB1 File Offset: 0x000C6FB1
		public virtual int IndexOf(string source, string value, CompareOptions options)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return this.IndexOf(source, value, 0, source.Length, options);
		}

		/// <summary>Searches for the specified character and returns the zero-based index of the first occurrence within the section of the source string that starts at the specified index and contains the specified number of elements using the specified <see cref="T:System.Globalization.CompareOptions" /> value.</summary>
		/// <returns>The zero-based index of the first occurrence of <paramref name="value" />, if found, within the section of <paramref name="source" /> that starts at <paramref name="startIndex" /> and contains the number of elements specified by <paramref name="count" />, using the specified comparison options; otherwise, -1. Returns <paramref name="startIndex" /> if <paramref name="value" /> is an ignorable character.</returns>
		/// <param name="source">The string to search. </param>
		/// <param name="value">The character to locate within <paramref name="source" />. </param>
		/// <param name="startIndex">The zero-based starting index of the search. </param>
		/// <param name="count">The number of elements in the section to search. </param>
		/// <param name="options">A value that defines how <paramref name="source" /> and <paramref name="value" /> should be compared. <paramref name="options" /> is either the enumeration value <see cref="F:System.Globalization.CompareOptions.Ordinal" />, or a bitwise combination of one or more of the following values: <see cref="F:System.Globalization.CompareOptions.IgnoreCase" />, <see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />, <see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />, <see cref="F:System.Globalization.CompareOptions.IgnoreWidth" />, and <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is outside the range of valid indexes for <paramref name="source" />.-or- <paramref name="count" /> is less than zero.-or- <paramref name="startIndex" /> and <paramref name="count" /> do not specify a valid section in <paramref name="source" />. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> contains an invalid <see cref="T:System.Globalization.CompareOptions" /> value. </exception>
		// Token: 0x0600348B RID: 13451 RVA: 0x000C8DD4 File Offset: 0x000C6FD4
		public virtual int IndexOf(string source, char value, int startIndex, int count, CompareOptions options)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (startIndex < 0 || startIndex > source.Length)
			{
				throw new ArgumentOutOfRangeException("startIndex", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			if (count < 0 || startIndex > source.Length - count)
			{
				throw new ArgumentOutOfRangeException("count", "Count must be positive and count must refer to a location within the string/array/collection.");
			}
			if (source.Length == 0)
			{
				return -1;
			}
			if (options == CompareOptions.OrdinalIgnoreCase)
			{
				return source.IndexOf(value.ToString(), startIndex, count, StringComparison.OrdinalIgnoreCase);
			}
			if ((options & ~(CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth)) != CompareOptions.None && options != CompareOptions.Ordinal)
			{
				throw new ArgumentException("Value of flags is invalid.", "options");
			}
			if (GlobalizationMode.Invariant)
			{
				return this.IndexOfOrdinal(source, new string(value, 1), startIndex, count, (options & (CompareOptions.IgnoreCase | CompareOptions.OrdinalIgnoreCase)) > CompareOptions.None);
			}
			return this.IndexOfCore(source, new string(value, 1), startIndex, count, options, null);
		}

		/// <summary>Searches for the specified substring and returns the zero-based index of the first occurrence within the section of the source string that starts at the specified index and contains the specified number of elements using the specified <see cref="T:System.Globalization.CompareOptions" /> value.</summary>
		/// <returns>The zero-based index of the first occurrence of <paramref name="value" />, if found, within the section of <paramref name="source" /> that starts at <paramref name="startIndex" /> and contains the number of elements specified by <paramref name="count" />, using the specified comparison options; otherwise, -1. Returns <paramref name="startIndex" /> if <paramref name="value" /> is an ignorable character.</returns>
		/// <param name="source">The string to search. </param>
		/// <param name="value">The string to locate within <paramref name="source" />. </param>
		/// <param name="startIndex">The zero-based starting index of the search. </param>
		/// <param name="count">The number of elements in the section to search. </param>
		/// <param name="options">A value that defines how <paramref name="source" /> and <paramref name="value" /> should be compared. <paramref name="options" /> is either the enumeration value <see cref="F:System.Globalization.CompareOptions.Ordinal" />, or a bitwise combination of one or more of the following values: <see cref="F:System.Globalization.CompareOptions.IgnoreCase" />, <see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />, <see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />, <see cref="F:System.Globalization.CompareOptions.IgnoreWidth" />, and <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null.-or- <paramref name="value" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is outside the range of valid indexes for <paramref name="source" />.-or- <paramref name="count" /> is less than zero.-or- <paramref name="startIndex" /> and <paramref name="count" /> do not specify a valid section in <paramref name="source" />. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> contains an invalid <see cref="T:System.Globalization.CompareOptions" /> value. </exception>
		// Token: 0x0600348C RID: 13452 RVA: 0x000C8EAC File Offset: 0x000C70AC
		public virtual int IndexOf(string source, string value, int startIndex, int count, CompareOptions options)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (startIndex > source.Length)
			{
				throw new ArgumentOutOfRangeException("startIndex", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			if (source.Length == 0)
			{
				if (value.Length == 0)
				{
					return 0;
				}
				return -1;
			}
			else
			{
				if (startIndex < 0)
				{
					throw new ArgumentOutOfRangeException("startIndex", "Index was out of range. Must be non-negative and less than the size of the collection.");
				}
				if (count < 0 || startIndex > source.Length - count)
				{
					throw new ArgumentOutOfRangeException("count", "Count must be positive and count must refer to a location within the string/array/collection.");
				}
				if (options == CompareOptions.OrdinalIgnoreCase)
				{
					return this.IndexOfOrdinal(source, value, startIndex, count, true);
				}
				if ((options & ~(CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth)) != CompareOptions.None && options != CompareOptions.Ordinal)
				{
					throw new ArgumentException("Value of flags is invalid.", "options");
				}
				if (GlobalizationMode.Invariant)
				{
					return this.IndexOfOrdinal(source, value, startIndex, count, (options & (CompareOptions.IgnoreCase | CompareOptions.OrdinalIgnoreCase)) > CompareOptions.None);
				}
				return this.IndexOfCore(source, value, startIndex, count, options, null);
			}
		}

		// Token: 0x0600348D RID: 13453 RVA: 0x000C8F9C File Offset: 0x000C719C
		internal unsafe int IndexOf(string source, string value, int startIndex, int count, CompareOptions options, int* matchLengthPtr)
		{
			*matchLengthPtr = 0;
			if (source.Length == 0)
			{
				if (value.Length == 0)
				{
					return 0;
				}
				return -1;
			}
			else
			{
				if (startIndex >= source.Length)
				{
					return -1;
				}
				if (options == CompareOptions.OrdinalIgnoreCase)
				{
					int num = this.IndexOfOrdinal(source, value, startIndex, count, true);
					if (num >= 0)
					{
						*matchLengthPtr = value.Length;
					}
					return num;
				}
				if (GlobalizationMode.Invariant)
				{
					int num2 = this.IndexOfOrdinal(source, value, startIndex, count, (options & (CompareOptions.IgnoreCase | CompareOptions.OrdinalIgnoreCase)) > CompareOptions.None);
					if (num2 >= 0)
					{
						*matchLengthPtr = value.Length;
					}
					return num2;
				}
				return this.IndexOfCore(source, value, startIndex, count, options, matchLengthPtr);
			}
		}

		// Token: 0x0600348E RID: 13454 RVA: 0x000C9029 File Offset: 0x000C7229
		internal int IndexOfOrdinal(string source, string value, int startIndex, int count, bool ignoreCase)
		{
			if (GlobalizationMode.Invariant)
			{
				return CompareInfo.InvariantIndexOf(source, value, startIndex, count, ignoreCase);
			}
			return CompareInfo.IndexOfOrdinalCore(source, value, startIndex, count, ignoreCase);
		}

		/// <summary>Searches for the specified substring and returns the zero-based index of the last occurrence within the section of the source string that contains the specified number of elements and ends at the specified index using the specified <see cref="T:System.Globalization.CompareOptions" /> value.</summary>
		/// <returns>The zero-based index of the last occurrence of <paramref name="value" />, if found, within the section of <paramref name="source" /> that contains the number of elements specified by <paramref name="count" /> and that ends at <paramref name="startIndex" />, using the specified comparison options; otherwise, -1. Returns <paramref name="startIndex" /> if <paramref name="value" /> is an ignorable character. </returns>
		/// <param name="source">The string to search. </param>
		/// <param name="value">The string to locate within <paramref name="source" />. </param>
		/// <param name="startIndex">The zero-based starting index of the backward search. </param>
		/// <param name="count">The number of elements in the section to search. </param>
		/// <param name="options">A value that defines how <paramref name="source" /> and <paramref name="value" /> should be compared. <paramref name="options" /> is either the enumeration value <see cref="F:System.Globalization.CompareOptions.Ordinal" />, or a bitwise combination of one or more of the following values: <see cref="F:System.Globalization.CompareOptions.IgnoreCase" />, <see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />, <see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />, <see cref="F:System.Globalization.CompareOptions.IgnoreWidth" />, and <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null.-or- <paramref name="value" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is outside the range of valid indexes for <paramref name="source" />.-or- <paramref name="count" /> is less than zero.-or- <paramref name="startIndex" /> and <paramref name="count" /> do not specify a valid section in <paramref name="source" />. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> contains an invalid <see cref="T:System.Globalization.CompareOptions" /> value. </exception>
		// Token: 0x0600348F RID: 13455 RVA: 0x000C904C File Offset: 0x000C724C
		public virtual int LastIndexOf(string source, string value, int startIndex, int count, CompareOptions options)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if ((options & ~(CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth)) != CompareOptions.None && options != CompareOptions.Ordinal && options != CompareOptions.OrdinalIgnoreCase)
			{
				throw new ArgumentException("Value of flags is invalid.", "options");
			}
			if (source.Length == 0 && (startIndex == -1 || startIndex == 0))
			{
				if (value.Length != 0)
				{
					return -1;
				}
				return 0;
			}
			else
			{
				if (startIndex < 0 || startIndex > source.Length)
				{
					throw new ArgumentOutOfRangeException("startIndex", "Index was out of range. Must be non-negative and less than the size of the collection.");
				}
				if (startIndex == source.Length)
				{
					startIndex--;
					if (count > 0)
					{
						count--;
					}
					if (value.Length == 0 && count >= 0 && startIndex - count + 1 >= 0)
					{
						return startIndex;
					}
				}
				if (count < 0 || startIndex - count + 1 < 0)
				{
					throw new ArgumentOutOfRangeException("count", "Count must be positive and count must refer to a location within the string/array/collection.");
				}
				if (options == CompareOptions.OrdinalIgnoreCase)
				{
					return this.LastIndexOfOrdinal(source, value, startIndex, count, true);
				}
				if (GlobalizationMode.Invariant)
				{
					return CompareInfo.InvariantLastIndexOf(source, value, startIndex, count, (options & (CompareOptions.IgnoreCase | CompareOptions.OrdinalIgnoreCase)) > CompareOptions.None);
				}
				return this.LastIndexOfCore(source, value, startIndex, count, options);
			}
		}

		// Token: 0x06003490 RID: 13456 RVA: 0x000C9165 File Offset: 0x000C7365
		internal int LastIndexOfOrdinal(string source, string value, int startIndex, int count, bool ignoreCase)
		{
			if (GlobalizationMode.Invariant)
			{
				return CompareInfo.InvariantLastIndexOf(source, value, startIndex, count, ignoreCase);
			}
			return CompareInfo.LastIndexOfOrdinalCore(source, value, startIndex, count, ignoreCase);
		}

		/// <summary>Gets a <see cref="T:System.Globalization.SortKey" /> object for the specified string using the specified <see cref="T:System.Globalization.CompareOptions" /> value.</summary>
		/// <returns>The <see cref="T:System.Globalization.SortKey" /> object that contains the sort key for the specified string.</returns>
		/// <param name="source">The string for which a <see cref="T:System.Globalization.SortKey" /> object is obtained. </param>
		/// <param name="options">A bitwise combination of one or more of the following enumeration values that define how the sort key is calculated: <see cref="F:System.Globalization.CompareOptions.IgnoreCase" />, <see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />, <see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />, <see cref="F:System.Globalization.CompareOptions.IgnoreWidth" />, <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />, and <see cref="F:System.Globalization.CompareOptions.StringSort" />.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> contains an invalid <see cref="T:System.Globalization.CompareOptions" /> value. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06003491 RID: 13457 RVA: 0x000C9187 File Offset: 0x000C7387
		public virtual SortKey GetSortKey(string source, CompareOptions options)
		{
			if (GlobalizationMode.Invariant)
			{
				return this.InvariantCreateSortKey(source, options);
			}
			return this.CreateSortKey(source, options);
		}

		/// <summary>Determines whether the specified object is equal to the current <see cref="T:System.Globalization.CompareInfo" /> object.</summary>
		/// <returns>true if the specified object is equal to the current <see cref="T:System.Globalization.CompareInfo" />; otherwise, false.</returns>
		/// <param name="value">The object to compare with the current <see cref="T:System.Globalization.CompareInfo" />. </param>
		// Token: 0x06003492 RID: 13458 RVA: 0x000C91A4 File Offset: 0x000C73A4
		public override bool Equals(object value)
		{
			CompareInfo compareInfo = value as CompareInfo;
			return compareInfo != null && this.Name == compareInfo.Name;
		}

		/// <summary>Serves as a hash function for the current <see cref="T:System.Globalization.CompareInfo" /> for hashing algorithms and data structures, such as a hash table.</summary>
		/// <returns>A hash code for the current <see cref="T:System.Globalization.CompareInfo" />.</returns>
		// Token: 0x06003493 RID: 13459 RVA: 0x000C91CE File Offset: 0x000C73CE
		public override int GetHashCode()
		{
			return this.Name.GetHashCode();
		}

		// Token: 0x06003494 RID: 13460 RVA: 0x000C91DC File Offset: 0x000C73DC
		internal unsafe static int GetIgnoreCaseHash(string source)
		{
			if (source.Length == 0)
			{
				return source.GetHashCode();
			}
			char[] array = null;
			Span<char> span;
			if (source.Length <= 255)
			{
				span = new Span<char>(stackalloc byte[(UIntPtr)510], 255);
			}
			else
			{
				span = (array = ArrayPool<char>.Shared.Rent(source.Length));
			}
			Span<char> span2 = span;
			int num = source.AsSpan().ToUpperInvariant(span2);
			int num2 = Marvin.ComputeHash32(MemoryMarshal.AsBytes<char>(span2.Slice(0, num)), Marvin.DefaultSeed);
			if (array != null)
			{
				ArrayPool<char>.Shared.Return(array, false);
			}
			return num2;
		}

		// Token: 0x06003495 RID: 13461 RVA: 0x000C9270 File Offset: 0x000C7470
		internal int GetHashCodeOfString(string source, CompareOptions options)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if ((options & ~(CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth)) != CompareOptions.None)
			{
				throw new ArgumentException("Value of flags is invalid.", "options");
			}
			if (!GlobalizationMode.Invariant)
			{
				return this.GetHashCodeOfStringCore(source, options);
			}
			if ((options & CompareOptions.IgnoreCase) == CompareOptions.None)
			{
				return source.GetHashCode();
			}
			return CompareInfo.GetIgnoreCaseHash(source);
		}

		// Token: 0x06003496 RID: 13462 RVA: 0x000C92C3 File Offset: 0x000C74C3
		public virtual int GetHashCode(string source, CompareOptions options)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (options == CompareOptions.Ordinal)
			{
				return source.GetHashCode();
			}
			if (options == CompareOptions.OrdinalIgnoreCase)
			{
				return CompareInfo.GetIgnoreCaseHash(source);
			}
			return this.GetHashCodeOfString(source, options);
		}

		/// <summary>Returns a string that represents the current <see cref="T:System.Globalization.CompareInfo" /> object.</summary>
		/// <returns>A string that represents the current <see cref="T:System.Globalization.CompareInfo" /> object.</returns>
		// Token: 0x06003497 RID: 13463 RVA: 0x000C92F9 File Offset: 0x000C74F9
		public override string ToString()
		{
			return "CompareInfo - " + this.Name;
		}

		// Token: 0x170007AD RID: 1965
		// (get) Token: 0x06003498 RID: 13464 RVA: 0x000C930B File Offset: 0x000C750B
		private static bool UseManagedCollation
		{
			get
			{
				if (!CompareInfo.managedCollationChecked)
				{
					CompareInfo.managedCollation = Environment.internalGetEnvironmentVariable("MONO_DISABLE_MANAGED_COLLATION") != "yes" && MSCompatUnicodeTable.IsReady;
					CompareInfo.managedCollationChecked = true;
				}
				return CompareInfo.managedCollation;
			}
		}

		// Token: 0x06003499 RID: 13465 RVA: 0x000C9344 File Offset: 0x000C7544
		private ISimpleCollator GetCollator()
		{
			if (this.collator != null)
			{
				return this.collator;
			}
			if (CompareInfo.collators == null)
			{
				Interlocked.CompareExchange<Dictionary<string, ISimpleCollator>>(ref CompareInfo.collators, new Dictionary<string, ISimpleCollator>(StringComparer.Ordinal), null);
			}
			Dictionary<string, ISimpleCollator> dictionary = CompareInfo.collators;
			lock (dictionary)
			{
				if (!CompareInfo.collators.TryGetValue(this._sortName, out this.collator))
				{
					this.collator = new SimpleCollator(CultureInfo.GetCultureInfo(this.m_name));
					CompareInfo.collators[this._sortName] = this.collator;
				}
			}
			return this.collator;
		}

		// Token: 0x0600349A RID: 13466 RVA: 0x000C93F4 File Offset: 0x000C75F4
		private SortKey CreateSortKeyCore(string source, CompareOptions options)
		{
			if (CompareInfo.UseManagedCollation)
			{
				return this.GetCollator().GetSortKey(source, options);
			}
			return new SortKey(this.culture, source, options);
		}

		// Token: 0x0600349B RID: 13467 RVA: 0x000C9418 File Offset: 0x000C7618
		private int internal_index_switch(string s1, int sindex, int count, string s2, CompareOptions opt, bool first)
		{
			if (opt == CompareOptions.Ordinal)
			{
				if (!first)
				{
					return s1.LastIndexOfUnchecked(s2, sindex, count);
				}
				return s1.IndexOfUnchecked(s2, sindex, count);
			}
			else
			{
				if (!CompareInfo.UseManagedCollation)
				{
					return CompareInfo.internal_index(s1, sindex, count, s2, first);
				}
				return this.internal_index_managed(s1, sindex, count, s2, opt, first);
			}
		}

		// Token: 0x0600349C RID: 13468 RVA: 0x000C946B File Offset: 0x000C766B
		private int internal_compare_switch(string str1, int offset1, int length1, string str2, int offset2, int length2, CompareOptions options)
		{
			if (!CompareInfo.UseManagedCollation)
			{
				return CompareInfo.internal_compare(str1, offset1, length1, str2, offset2, length2, options);
			}
			return this.internal_compare_managed(str1, offset1, length1, str2, offset2, length2, options);
		}

		// Token: 0x0600349D RID: 13469 RVA: 0x000C9496 File Offset: 0x000C7696
		private int internal_compare_managed(string str1, int offset1, int length1, string str2, int offset2, int length2, CompareOptions options)
		{
			return this.GetCollator().Compare(str1, offset1, length1, str2, offset2, length2, options);
		}

		// Token: 0x0600349E RID: 13470 RVA: 0x000C94AE File Offset: 0x000C76AE
		private int internal_index_managed(string s1, int sindex, int count, string s2, CompareOptions opt, bool first)
		{
			if (!first)
			{
				return this.GetCollator().LastIndexOf(s1, s2, sindex, count, opt);
			}
			return this.GetCollator().IndexOf(s1, s2, sindex, count, opt);
		}

		// Token: 0x0600349F RID: 13471
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern int internal_compare_icall(char* str1, int length1, char* str2, int length2, CompareOptions options);

		// Token: 0x060034A0 RID: 13472 RVA: 0x000C94DC File Offset: 0x000C76DC
		private unsafe static int internal_compare(string str1, int offset1, int length1, string str2, int offset2, int length2, CompareOptions options)
		{
			char* ptr = str1;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			char* ptr2 = str2;
			if (ptr2 != null)
			{
				ptr2 += RuntimeHelpers.OffsetToStringData / 2;
			}
			return CompareInfo.internal_compare_icall(ptr + offset1, length1, ptr2 + offset2, length2, options);
		}

		// Token: 0x060034A1 RID: 13473
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern int internal_index_icall(char* source, int sindex, int count, char* value, int value_length, bool first);

		// Token: 0x060034A2 RID: 13474 RVA: 0x000C9520 File Offset: 0x000C7720
		private unsafe static int internal_index(string source, int sindex, int count, string value, bool first)
		{
			char* ptr = source;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			char* ptr2 = value;
			if (ptr2 != null)
			{
				ptr2 += RuntimeHelpers.OffsetToStringData / 2;
			}
			return CompareInfo.internal_index_icall(ptr, sindex, count, ptr2, (value != null) ? value.Length : 0, first);
		}

		// Token: 0x060034A3 RID: 13475 RVA: 0x000C9564 File Offset: 0x000C7764
		private void InitSort(CultureInfo culture)
		{
			this._sortName = culture.SortName;
		}

		// Token: 0x060034A4 RID: 13476 RVA: 0x000C9574 File Offset: 0x000C7774
		private unsafe static int CompareStringOrdinalIgnoreCase(char* pString1, int length1, char* pString2, int length2)
		{
			TextInfo textInfo = CultureInfo.InvariantCulture.TextInfo;
			int num = 0;
			while (num < length1 && num < length2 && textInfo.ToUpper(*pString1) == textInfo.ToUpper(*pString2))
			{
				num++;
				pString1++;
				pString2++;
			}
			if (num >= length1)
			{
				if (num >= length2)
				{
					return 0;
				}
				return -1;
			}
			else
			{
				if (num >= length2)
				{
					return 1;
				}
				return (int)(textInfo.ToUpper(*pString1) - textInfo.ToUpper(*pString2));
			}
		}

		// Token: 0x060034A5 RID: 13477 RVA: 0x000C95DB File Offset: 0x000C77DB
		internal static int IndexOfOrdinalCore(string source, string value, int startIndex, int count, bool ignoreCase)
		{
			if (!ignoreCase)
			{
				return source.IndexOfUnchecked(value, startIndex, count);
			}
			return source.IndexOfUncheckedIgnoreCase(value, startIndex, count);
		}

		// Token: 0x060034A6 RID: 13478 RVA: 0x000C95F4 File Offset: 0x000C77F4
		internal static int LastIndexOfOrdinalCore(string source, string value, int startIndex, int count, bool ignoreCase)
		{
			if (!ignoreCase)
			{
				return source.LastIndexOfUnchecked(value, startIndex, count);
			}
			return source.LastIndexOfUncheckedIgnoreCase(value, startIndex, count);
		}

		// Token: 0x060034A7 RID: 13479 RVA: 0x000C960D File Offset: 0x000C780D
		private int LastIndexOfCore(string source, string target, int startIndex, int count, CompareOptions options)
		{
			return this.internal_index_switch(source, startIndex, count, target, options, false);
		}

		// Token: 0x060034A8 RID: 13480 RVA: 0x000C961D File Offset: 0x000C781D
		private unsafe int IndexOfCore(string source, string target, int startIndex, int count, CompareOptions options, int* matchLengthPtr)
		{
			if (matchLengthPtr != null)
			{
				throw new NotImplementedException();
			}
			return this.internal_index_switch(source, startIndex, count, target, options, true);
		}

		// Token: 0x060034A9 RID: 13481 RVA: 0x000C963C File Offset: 0x000C783C
		private int CompareString(ReadOnlySpan<char> string1, string string2, CompareOptions options)
		{
			string text = new string(string1);
			return this.internal_compare_switch(text, 0, text.Length, string2, 0, string2.Length, options);
		}

		// Token: 0x060034AA RID: 13482 RVA: 0x000C9668 File Offset: 0x000C7868
		private int CompareString(ReadOnlySpan<char> string1, ReadOnlySpan<char> string2, CompareOptions options)
		{
			string text = new string(string1);
			string text2 = new string(string2);
			return this.internal_compare_switch(text, 0, text.Length, new string(text2), 0, text2.Length, options);
		}

		// Token: 0x060034AB RID: 13483 RVA: 0x000C96A4 File Offset: 0x000C78A4
		private SortKey CreateSortKey(string source, CompareOptions options)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if ((options & ~(CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth | CompareOptions.StringSort)) != CompareOptions.None)
			{
				throw new ArgumentException("Value of flags is invalid.", "options");
			}
			return this.CreateSortKeyCore(source, options);
		}

		// Token: 0x060034AC RID: 13484 RVA: 0x000C96D8 File Offset: 0x000C78D8
		private bool StartsWith(string source, string prefix, CompareOptions options)
		{
			if (CompareInfo.UseManagedCollation)
			{
				return this.GetCollator().IsPrefix(source, prefix, options);
			}
			return source.Length >= prefix.Length && this.Compare(source, 0, prefix.Length, prefix, 0, prefix.Length, options) == 0;
		}

		// Token: 0x060034AD RID: 13485 RVA: 0x000C9728 File Offset: 0x000C7928
		private bool EndsWith(string source, string suffix, CompareOptions options)
		{
			if (CompareInfo.UseManagedCollation)
			{
				return this.GetCollator().IsSuffix(source, suffix, options);
			}
			return source.Length >= suffix.Length && this.Compare(source, source.Length - suffix.Length, suffix.Length, suffix, 0, suffix.Length, options) == 0;
		}

		// Token: 0x060034AE RID: 13486 RVA: 0x000C9781 File Offset: 0x000C7981
		private bool EndsWith(ReadOnlySpan<char> source, ReadOnlySpan<char> suffix, CompareOptions options)
		{
			return this.EndsWith(new string(source), new string(suffix), options);
		}

		// Token: 0x060034AF RID: 13487 RVA: 0x000C9796 File Offset: 0x000C7996
		internal int GetHashCodeOfStringCore(string source, CompareOptions options)
		{
			return this.GetSortKey(source, options).GetHashCode();
		}

		// Token: 0x060034B1 RID: 13489 RVA: 0x000176B9 File Offset: 0x000158B9
		internal CompareInfo()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04001B77 RID: 7031
		private const CompareOptions ValidIndexMaskOffFlags = ~(CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth);

		// Token: 0x04001B78 RID: 7032
		private const CompareOptions ValidCompareMaskOffFlags = ~(CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth | CompareOptions.StringSort);

		// Token: 0x04001B79 RID: 7033
		private const CompareOptions ValidHashCodeOfStringMaskOffFlags = ~(CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth);

		// Token: 0x04001B7A RID: 7034
		private const CompareOptions ValidSortkeyCtorMaskOffFlags = ~(CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth | CompareOptions.StringSort);

		// Token: 0x04001B7B RID: 7035
		internal static readonly CompareInfo Invariant = CultureInfo.InvariantCulture.CompareInfo;

		// Token: 0x04001B7C RID: 7036
		[OptionalField(VersionAdded = 2)]
		private string m_name;

		// Token: 0x04001B7D RID: 7037
		[NonSerialized]
		private string _sortName;

		// Token: 0x04001B7E RID: 7038
		[OptionalField(VersionAdded = 3)]
		private SortVersion m_SortVersion;

		// Token: 0x04001B7F RID: 7039
		private int culture;

		// Token: 0x04001B80 RID: 7040
		[NonSerialized]
		private ISimpleCollator collator;

		// Token: 0x04001B81 RID: 7041
		private static Dictionary<string, ISimpleCollator> collators;

		// Token: 0x04001B82 RID: 7042
		private static bool managedCollation;

		// Token: 0x04001B83 RID: 7043
		private static bool managedCollationChecked;
	}
}
