using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace System
{
	// Token: 0x020000E6 RID: 230
	internal static class DateTimeFormat
	{
		// Token: 0x0600079D RID: 1949 RVA: 0x0001E87C File Offset: 0x0001CA7C
		internal static void FormatDigits(StringBuilder outputBuffer, int value, int len)
		{
			DateTimeFormat.FormatDigits(outputBuffer, value, len, false);
		}

		// Token: 0x0600079E RID: 1950 RVA: 0x0001E888 File Offset: 0x0001CA88
		internal unsafe static void FormatDigits(StringBuilder outputBuffer, int value, int len, bool overrideLengthLimit)
		{
			if (!overrideLengthLimit && len > 2)
			{
				len = 2;
			}
			char* ptr = stackalloc char[(UIntPtr)32];
			char* ptr2 = ptr + 16;
			int num = value;
			do
			{
				*(--ptr2) = (char)(num % 10 + 48);
				num /= 10;
			}
			while (num != 0 && ptr2 != ptr);
			int num2 = (int)((long)(ptr + 16 - ptr2));
			while (num2 < len && ptr2 != ptr)
			{
				*(--ptr2) = '0';
				num2++;
			}
			outputBuffer.Append(ptr2, num2);
		}

		// Token: 0x0600079F RID: 1951 RVA: 0x0001E8F6 File Offset: 0x0001CAF6
		private static void HebrewFormatDigits(StringBuilder outputBuffer, int digits)
		{
			outputBuffer.Append(HebrewNumber.ToString(digits));
		}

		// Token: 0x060007A0 RID: 1952 RVA: 0x0001E908 File Offset: 0x0001CB08
		internal unsafe static int ParseRepeatPattern(ReadOnlySpan<char> format, int pos, char patternChar)
		{
			int length = format.Length;
			int num = pos + 1;
			while (num < length && *format[num] == (ushort)patternChar)
			{
				num++;
			}
			return num - pos;
		}

		// Token: 0x060007A1 RID: 1953 RVA: 0x0001E93A File Offset: 0x0001CB3A
		private static string FormatDayOfWeek(int dayOfWeek, int repeat, DateTimeFormatInfo dtfi)
		{
			if (repeat == 3)
			{
				return dtfi.GetAbbreviatedDayName((DayOfWeek)dayOfWeek);
			}
			return dtfi.GetDayName((DayOfWeek)dayOfWeek);
		}

		// Token: 0x060007A2 RID: 1954 RVA: 0x0001E94F File Offset: 0x0001CB4F
		private static string FormatMonth(int month, int repeatCount, DateTimeFormatInfo dtfi)
		{
			if (repeatCount == 3)
			{
				return dtfi.GetAbbreviatedMonthName(month);
			}
			return dtfi.GetMonthName(month);
		}

		// Token: 0x060007A3 RID: 1955 RVA: 0x0001E964 File Offset: 0x0001CB64
		private static string FormatHebrewMonthName(DateTime time, int month, int repeatCount, DateTimeFormatInfo dtfi)
		{
			if (dtfi.Calendar.IsLeapYear(dtfi.Calendar.GetYear(time)))
			{
				return dtfi.internalGetMonthName(month, MonthNameStyles.LeapYear, repeatCount == 3);
			}
			if (month >= 7)
			{
				month++;
			}
			if (repeatCount == 3)
			{
				return dtfi.GetAbbreviatedMonthName(month);
			}
			return dtfi.GetMonthName(month);
		}

		// Token: 0x060007A4 RID: 1956 RVA: 0x0001E9B4 File Offset: 0x0001CBB4
		internal unsafe static int ParseQuoteString(ReadOnlySpan<char> format, int pos, StringBuilder result)
		{
			int length = format.Length;
			int num = pos;
			char c = (char)(*format[pos++]);
			bool flag = false;
			while (pos < length)
			{
				char c2 = (char)(*format[pos++]);
				if (c2 == c)
				{
					flag = true;
					break;
				}
				if (c2 == '\\')
				{
					if (pos >= length)
					{
						throw new FormatException("Input string was not in a correct format.");
					}
					result.Append((char)(*format[pos++]));
				}
				else
				{
					result.Append(c2);
				}
			}
			if (!flag)
			{
				throw new FormatException(string.Format(CultureInfo.CurrentCulture, "Cannot find a matching quote character for the character '{0}'.", c));
			}
			return pos - num;
		}

		// Token: 0x060007A5 RID: 1957 RVA: 0x0001EA51 File Offset: 0x0001CC51
		internal unsafe static int ParseNextChar(ReadOnlySpan<char> format, int pos)
		{
			if (pos >= format.Length - 1)
			{
				return -1;
			}
			return (int)(*format[pos + 1]);
		}

		// Token: 0x060007A6 RID: 1958 RVA: 0x0001EA6C File Offset: 0x0001CC6C
		private unsafe static bool IsUseGenitiveForm(ReadOnlySpan<char> format, int index, int tokenLen, char patternToMatch)
		{
			int num = 0;
			int num2 = index - 1;
			while (num2 >= 0 && *format[num2] != (ushort)patternToMatch)
			{
				num2--;
			}
			if (num2 >= 0)
			{
				while (--num2 >= 0 && *format[num2] == (ushort)patternToMatch)
				{
					num++;
				}
				if (num <= 1)
				{
					return true;
				}
			}
			num2 = index + tokenLen;
			while (num2 < format.Length && *format[num2] != (ushort)patternToMatch)
			{
				num2++;
			}
			if (num2 < format.Length)
			{
				num = 0;
				while (++num2 < format.Length && *format[num2] == (ushort)patternToMatch)
				{
					num++;
				}
				if (num <= 1)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060007A7 RID: 1959 RVA: 0x0001EB0C File Offset: 0x0001CD0C
		private unsafe static StringBuilder FormatCustomized(DateTime dateTime, ReadOnlySpan<char> format, DateTimeFormatInfo dtfi, TimeSpan offset, StringBuilder result)
		{
			Calendar calendar = dtfi.Calendar;
			bool flag = false;
			if (result == null)
			{
				flag = true;
				result = StringBuilderCache.Acquire(16);
			}
			bool flag2 = !GlobalizationMode.Invariant && (ushort)calendar.ID == 8;
			bool flag3 = !GlobalizationMode.Invariant && (ushort)calendar.ID == 3;
			bool flag4 = true;
			int i = 0;
			while (i < format.Length)
			{
				char c = (char)(*format[i]);
				int num2;
				if (c <= 'K')
				{
					if (c <= '/')
					{
						if (c <= '%')
						{
							if (c != '"')
							{
								if (c != '%')
								{
									goto IL_0686;
								}
								int num = DateTimeFormat.ParseNextChar(format, i);
								if (num >= 0 && num != 37)
								{
									char c2 = (char)num;
									DateTimeFormat.FormatCustomized(dateTime, MemoryMarshal.CreateReadOnlySpan<char>(ref c2, 1), dtfi, offset, result);
									num2 = 2;
									goto IL_0693;
								}
								if (flag)
								{
									StringBuilderCache.Release(result);
								}
								throw new FormatException("Input string was not in a correct format.");
							}
						}
						else if (c != '\'')
						{
							if (c != '/')
							{
								goto IL_0686;
							}
							result.Append(dtfi.DateSeparator);
							num2 = 1;
							goto IL_0693;
						}
						num2 = DateTimeFormat.ParseQuoteString(format, i, result);
					}
					else if (c <= 'F')
					{
						if (c != ':')
						{
							if (c != 'F')
							{
								goto IL_0686;
							}
							goto IL_0209;
						}
						else
						{
							result.Append(dtfi.TimeSeparator);
							num2 = 1;
						}
					}
					else if (c != 'H')
					{
						if (c != 'K')
						{
							goto IL_0686;
						}
						num2 = 1;
						DateTimeFormat.FormatCustomizedRoundripTimeZone(dateTime, offset, result);
					}
					else
					{
						num2 = DateTimeFormat.ParseRepeatPattern(format, i, c);
						DateTimeFormat.FormatDigits(result, dateTime.Hour, num2);
					}
				}
				else if (c <= 'm')
				{
					if (c <= '\\')
					{
						if (c != 'M')
						{
							if (c != '\\')
							{
								goto IL_0686;
							}
							int num = DateTimeFormat.ParseNextChar(format, i);
							if (num < 0)
							{
								if (flag)
								{
									StringBuilderCache.Release(result);
								}
								throw new FormatException("Input string was not in a correct format.");
							}
							result.Append((char)num);
							num2 = 2;
						}
						else
						{
							num2 = DateTimeFormat.ParseRepeatPattern(format, i, c);
							int month = calendar.GetMonth(dateTime);
							if (num2 <= 2)
							{
								if (flag2 && !GlobalizationMode.Invariant)
								{
									DateTimeFormat.HebrewFormatDigits(result, month);
								}
								else
								{
									DateTimeFormat.FormatDigits(result, month, num2);
								}
							}
							else if (flag2 && !GlobalizationMode.Invariant)
							{
								result.Append(DateTimeFormat.FormatHebrewMonthName(dateTime, month, num2, dtfi));
							}
							else if ((dtfi.FormatFlags & DateTimeFormatFlags.UseGenitiveMonth) != DateTimeFormatFlags.None && num2 >= 4)
							{
								result.Append(dtfi.internalGetMonthName(month, DateTimeFormat.IsUseGenitiveForm(format, i, num2, 'd') ? MonthNameStyles.Genitive : MonthNameStyles.Regular, false));
							}
							else
							{
								result.Append(DateTimeFormat.FormatMonth(month, num2, dtfi));
							}
							flag4 = false;
						}
					}
					else
					{
						switch (c)
						{
						case 'd':
							num2 = DateTimeFormat.ParseRepeatPattern(format, i, c);
							if (num2 <= 2)
							{
								int dayOfMonth = calendar.GetDayOfMonth(dateTime);
								if (flag2 && !GlobalizationMode.Invariant)
								{
									DateTimeFormat.HebrewFormatDigits(result, dayOfMonth);
								}
								else
								{
									DateTimeFormat.FormatDigits(result, dayOfMonth, num2);
								}
							}
							else
							{
								int dayOfWeek = (int)calendar.GetDayOfWeek(dateTime);
								result.Append(DateTimeFormat.FormatDayOfWeek(dayOfWeek, num2, dtfi));
							}
							flag4 = false;
							break;
						case 'e':
							goto IL_0686;
						case 'f':
							goto IL_0209;
						case 'g':
							num2 = DateTimeFormat.ParseRepeatPattern(format, i, c);
							result.Append(dtfi.GetEraName(calendar.GetEra(dateTime)));
							break;
						case 'h':
						{
							num2 = DateTimeFormat.ParseRepeatPattern(format, i, c);
							int num3 = dateTime.Hour % 12;
							if (num3 == 0)
							{
								num3 = 12;
							}
							DateTimeFormat.FormatDigits(result, num3, num2);
							break;
						}
						default:
							if (c != 'm')
							{
								goto IL_0686;
							}
							num2 = DateTimeFormat.ParseRepeatPattern(format, i, c);
							DateTimeFormat.FormatDigits(result, dateTime.Minute, num2);
							break;
						}
					}
				}
				else if (c <= 't')
				{
					if (c != 's')
					{
						if (c != 't')
						{
							goto IL_0686;
						}
						num2 = DateTimeFormat.ParseRepeatPattern(format, i, c);
						if (num2 == 1)
						{
							if (dateTime.Hour < 12)
							{
								if (dtfi.AMDesignator.Length >= 1)
								{
									result.Append(dtfi.AMDesignator[0]);
								}
							}
							else if (dtfi.PMDesignator.Length >= 1)
							{
								result.Append(dtfi.PMDesignator[0]);
							}
						}
						else
						{
							result.Append((dateTime.Hour < 12) ? dtfi.AMDesignator : dtfi.PMDesignator);
						}
					}
					else
					{
						num2 = DateTimeFormat.ParseRepeatPattern(format, i, c);
						DateTimeFormat.FormatDigits(result, dateTime.Second, num2);
					}
				}
				else if (c != 'y')
				{
					if (c != 'z')
					{
						goto IL_0686;
					}
					num2 = DateTimeFormat.ParseRepeatPattern(format, i, c);
					DateTimeFormat.FormatCustomizedTimeZone(dateTime, offset, format, num2, flag4, result);
				}
				else
				{
					int year = calendar.GetYear(dateTime);
					num2 = DateTimeFormat.ParseRepeatPattern(format, i, c);
					if (flag3 && !AppContextSwitches.FormatJapaneseFirstYearAsANumber && year == 1 && i + num2 < format.Length - 1 && *format[i + num2] == 39 && *format[i + num2 + 1] == (ushort)"年"[0])
					{
						result.Append("元"[0]);
					}
					else if (dtfi.HasForceTwoDigitYears)
					{
						DateTimeFormat.FormatDigits(result, year, (num2 <= 2) ? num2 : 2);
					}
					else if (flag2 && !GlobalizationMode.Invariant)
					{
						DateTimeFormat.HebrewFormatDigits(result, year);
					}
					else if (num2 <= 2)
					{
						DateTimeFormat.FormatDigits(result, year % 100, num2);
					}
					else
					{
						string text = "D" + num2.ToString();
						result.Append(year.ToString(text, CultureInfo.InvariantCulture));
					}
					flag4 = false;
				}
				IL_0693:
				i += num2;
				continue;
				IL_0209:
				num2 = DateTimeFormat.ParseRepeatPattern(format, i, c);
				if (num2 > 7)
				{
					if (flag)
					{
						StringBuilderCache.Release(result);
					}
					throw new FormatException("Input string was not in a correct format.");
				}
				long num4 = dateTime.Ticks % 10000000L;
				num4 /= (long)Math.Pow(10.0, (double)(7 - num2));
				if (c == 'f')
				{
					result.Append(((int)num4).ToString(DateTimeFormat.fixedNumberFormats[num2 - 1], CultureInfo.InvariantCulture));
					goto IL_0693;
				}
				int num5 = num2;
				while (num5 > 0 && num4 % 10L == 0L)
				{
					num4 /= 10L;
					num5--;
				}
				if (num5 > 0)
				{
					result.Append(((int)num4).ToString(DateTimeFormat.fixedNumberFormats[num5 - 1], CultureInfo.InvariantCulture));
					goto IL_0693;
				}
				if (result.Length > 0 && result[result.Length - 1] == '.')
				{
					result.Remove(result.Length - 1, 1);
					goto IL_0693;
				}
				goto IL_0693;
				IL_0686:
				result.Append(c);
				num2 = 1;
				goto IL_0693;
			}
			return result;
		}

		// Token: 0x060007A8 RID: 1960 RVA: 0x0001F1C4 File Offset: 0x0001D3C4
		private static void FormatCustomizedTimeZone(DateTime dateTime, TimeSpan offset, ReadOnlySpan<char> format, int tokenLen, bool timeOnly, StringBuilder result)
		{
			if (offset == DateTimeFormat.NullOffset)
			{
				if (timeOnly && dateTime.Ticks < 864000000000L)
				{
					offset = TimeZoneInfo.GetLocalUtcOffset(DateTime.Now, TimeZoneInfoOptions.NoThrowOnInvalidTime);
				}
				else if (dateTime.Kind == DateTimeKind.Utc)
				{
					offset = TimeSpan.Zero;
				}
				else
				{
					offset = TimeZoneInfo.GetLocalUtcOffset(dateTime, TimeZoneInfoOptions.NoThrowOnInvalidTime);
				}
			}
			if (offset >= TimeSpan.Zero)
			{
				result.Append('+');
			}
			else
			{
				result.Append('-');
				offset = offset.Negate();
			}
			if (tokenLen <= 1)
			{
				result.AppendFormat(CultureInfo.InvariantCulture, "{0:0}", offset.Hours);
				return;
			}
			result.AppendFormat(CultureInfo.InvariantCulture, "{0:00}", offset.Hours);
			if (tokenLen >= 3)
			{
				result.AppendFormat(CultureInfo.InvariantCulture, ":{0:00}", offset.Minutes);
			}
		}

		// Token: 0x060007A9 RID: 1961 RVA: 0x0001F2B0 File Offset: 0x0001D4B0
		private static void FormatCustomizedRoundripTimeZone(DateTime dateTime, TimeSpan offset, StringBuilder result)
		{
			if (offset == DateTimeFormat.NullOffset)
			{
				DateTimeKind kind = dateTime.Kind;
				if (kind == DateTimeKind.Utc)
				{
					result.Append("Z");
					return;
				}
				if (kind != DateTimeKind.Local)
				{
					return;
				}
				offset = TimeZoneInfo.GetLocalUtcOffset(dateTime, TimeZoneInfoOptions.NoThrowOnInvalidTime);
			}
			if (offset >= TimeSpan.Zero)
			{
				result.Append('+');
			}
			else
			{
				result.Append('-');
				offset = offset.Negate();
			}
			DateTimeFormat.Append2DigitNumber(result, offset.Hours);
			result.Append(':');
			DateTimeFormat.Append2DigitNumber(result, offset.Minutes);
		}

		// Token: 0x060007AA RID: 1962 RVA: 0x0001F340 File Offset: 0x0001D540
		private static void Append2DigitNumber(StringBuilder result, int val)
		{
			result.Append((char)(48 + val / 10));
			result.Append((char)(48 + val % 10));
		}

		// Token: 0x060007AB RID: 1963 RVA: 0x0001F360 File Offset: 0x0001D560
		internal unsafe static string GetRealFormat(ReadOnlySpan<char> format, DateTimeFormatInfo dtfi)
		{
			char c = (char)(*format[0]);
			if (c > 'U')
			{
				if (c != 'Y')
				{
					switch (c)
					{
					case 'd':
						return dtfi.ShortDatePattern;
					case 'e':
						goto IL_015B;
					case 'f':
						return dtfi.LongDatePattern + " " + dtfi.ShortTimePattern;
					case 'g':
						return dtfi.GeneralShortTimePattern;
					default:
						switch (c)
						{
						case 'm':
							goto IL_010B;
						case 'n':
						case 'p':
						case 'q':
						case 'v':
						case 'w':
						case 'x':
							goto IL_015B;
						case 'o':
							goto IL_0114;
						case 'r':
							goto IL_011C;
						case 's':
							return dtfi.SortableDateTimePattern;
						case 't':
							return dtfi.ShortTimePattern;
						case 'u':
							return dtfi.UniversalSortableDateTimePattern;
						case 'y':
							break;
						default:
							goto IL_015B;
						}
						break;
					}
				}
				return dtfi.YearMonthPattern;
			}
			switch (c)
			{
			case 'D':
				return dtfi.LongDatePattern;
			case 'E':
				goto IL_015B;
			case 'F':
				return dtfi.FullDateTimePattern;
			case 'G':
				return dtfi.GeneralLongTimePattern;
			default:
				switch (c)
				{
				case 'M':
					break;
				case 'N':
				case 'P':
				case 'Q':
				case 'S':
					goto IL_015B;
				case 'O':
					goto IL_0114;
				case 'R':
					goto IL_011C;
				case 'T':
					return dtfi.LongTimePattern;
				case 'U':
					return dtfi.FullDateTimePattern;
				default:
					goto IL_015B;
				}
				break;
			}
			IL_010B:
			return dtfi.MonthDayPattern;
			IL_0114:
			return "yyyy'-'MM'-'dd'T'HH':'mm':'ss.fffffffK";
			IL_011C:
			return dtfi.RFC1123Pattern;
			IL_015B:
			throw new FormatException("Input string was not in a correct format.");
		}

		// Token: 0x060007AC RID: 1964 RVA: 0x0001F4D4 File Offset: 0x0001D6D4
		private unsafe static string ExpandPredefinedFormat(ReadOnlySpan<char> format, ref DateTime dateTime, ref DateTimeFormatInfo dtfi, ref TimeSpan offset)
		{
			char c = (char)(*format[0]);
			if (c <= 'R')
			{
				if (c != 'O')
				{
					if (c != 'R')
					{
						goto IL_015D;
					}
					goto IL_005C;
				}
			}
			else if (c != 'U')
			{
				switch (c)
				{
				case 'o':
					break;
				case 'p':
				case 'q':
				case 't':
					goto IL_015D;
				case 'r':
					goto IL_005C;
				case 's':
					dtfi = DateTimeFormatInfo.InvariantInfo;
					goto IL_015D;
				case 'u':
					if (offset != DateTimeFormat.NullOffset)
					{
						dateTime -= offset;
					}
					else if (dateTime.Kind == DateTimeKind.Local)
					{
						DateTimeFormat.InvalidFormatForLocal(format, dateTime);
					}
					dtfi = DateTimeFormatInfo.InvariantInfo;
					goto IL_015D;
				default:
					goto IL_015D;
				}
			}
			else
			{
				if (offset != DateTimeFormat.NullOffset)
				{
					throw new FormatException("Input string was not in a correct format.");
				}
				dtfi = (DateTimeFormatInfo)dtfi.Clone();
				if (dtfi.Calendar.GetType() != typeof(GregorianCalendar))
				{
					dtfi.Calendar = GregorianCalendar.GetDefaultInstance();
				}
				dateTime = dateTime.ToUniversalTime();
				goto IL_015D;
			}
			dtfi = DateTimeFormatInfo.InvariantInfo;
			goto IL_015D;
			IL_005C:
			if (offset != DateTimeFormat.NullOffset)
			{
				dateTime -= offset;
			}
			else if (dateTime.Kind == DateTimeKind.Local)
			{
				DateTimeFormat.InvalidFormatForLocal(format, dateTime);
			}
			dtfi = DateTimeFormatInfo.InvariantInfo;
			IL_015D:
			return DateTimeFormat.GetRealFormat(format, dtfi);
		}

		// Token: 0x060007AD RID: 1965 RVA: 0x0001F646 File Offset: 0x0001D846
		internal static string Format(DateTime dateTime, string format, IFormatProvider provider)
		{
			return DateTimeFormat.Format(dateTime, format, provider, DateTimeFormat.NullOffset);
		}

		// Token: 0x060007AE RID: 1966 RVA: 0x0001F658 File Offset: 0x0001D858
		internal unsafe static string Format(DateTime dateTime, string format, IFormatProvider provider, TimeSpan offset)
		{
			if (format != null && format.Length == 1)
			{
				char c = format[0];
				if (c <= 'R')
				{
					if (c != 'O')
					{
						if (c != 'R')
						{
							goto IL_0093;
						}
						goto IL_006E;
					}
				}
				else if (c != 'o')
				{
					if (c != 'r')
					{
						goto IL_0093;
					}
					goto IL_006E;
				}
				Span<char> span = new Span<char>(stackalloc byte[(UIntPtr)66], 33);
				int num;
				DateTimeFormat.TryFormatO(dateTime, offset, span, out num);
				return span.Slice(0, num).ToString();
				IL_006E:
				string text = string.FastAllocateString(29);
				int num2;
				DateTimeFormat.TryFormatR(dateTime, offset, new Span<char>(text.GetRawStringData(), text.Length), out num2);
				return text;
			}
			IL_0093:
			DateTimeFormatInfo instance = DateTimeFormatInfo.GetInstance(provider);
			return StringBuilderCache.GetStringAndRelease(DateTimeFormat.FormatStringBuilder(dateTime, format, instance, offset));
		}

		// Token: 0x060007AF RID: 1967 RVA: 0x0001F712 File Offset: 0x0001D912
		internal static bool TryFormat(DateTime dateTime, Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider provider)
		{
			return DateTimeFormat.TryFormat(dateTime, destination, out charsWritten, format, provider, DateTimeFormat.NullOffset);
		}

		// Token: 0x060007B0 RID: 1968 RVA: 0x0001F724 File Offset: 0x0001D924
		internal unsafe static bool TryFormat(DateTime dateTime, Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider provider, TimeSpan offset)
		{
			if (format.Length == 1)
			{
				char c = (char)(*format[0]);
				if (c <= 'R')
				{
					if (c != 'O')
					{
						if (c != 'R')
						{
							goto IL_0047;
						}
						goto IL_003C;
					}
				}
				else if (c != 'o')
				{
					if (c != 'r')
					{
						goto IL_0047;
					}
					goto IL_003C;
				}
				return DateTimeFormat.TryFormatO(dateTime, offset, destination, out charsWritten);
				IL_003C:
				return DateTimeFormat.TryFormatR(dateTime, offset, destination, out charsWritten);
			}
			IL_0047:
			DateTimeFormatInfo instance = DateTimeFormatInfo.GetInstance(provider);
			StringBuilder stringBuilder = DateTimeFormat.FormatStringBuilder(dateTime, format, instance, offset);
			bool flag = stringBuilder.Length <= destination.Length;
			if (flag)
			{
				stringBuilder.CopyTo(0, destination, stringBuilder.Length);
				charsWritten = stringBuilder.Length;
			}
			else
			{
				charsWritten = 0;
			}
			StringBuilderCache.Release(stringBuilder);
			return flag;
		}

		// Token: 0x060007B1 RID: 1969 RVA: 0x0001F7C4 File Offset: 0x0001D9C4
		private static StringBuilder FormatStringBuilder(DateTime dateTime, ReadOnlySpan<char> format, DateTimeFormatInfo dtfi, TimeSpan offset)
		{
			if (format.Length == 0)
			{
				bool flag = false;
				if (dateTime.Ticks < 864000000000L)
				{
					CalendarId calendarId = (CalendarId)dtfi.Calendar.ID;
					switch (calendarId)
					{
					case CalendarId.JAPAN:
					case CalendarId.TAIWAN:
					case CalendarId.HIJRI:
					case CalendarId.HEBREW:
						break;
					case CalendarId.KOREA:
					case CalendarId.THAI:
						goto IL_0062;
					default:
						if (calendarId != CalendarId.JULIAN && calendarId - CalendarId.PERSIAN > 1)
						{
							goto IL_0062;
						}
						break;
					}
					flag = true;
					dtfi = DateTimeFormatInfo.InvariantInfo;
				}
				IL_0062:
				if (offset == DateTimeFormat.NullOffset)
				{
					format = (flag ? "s" : "G");
				}
				else
				{
					format = (flag ? "yyyy'-'MM'-'ddTHH':'mm':'ss zzz" : dtfi.DateTimeOffsetPattern);
				}
			}
			if (format.Length == 1)
			{
				format = DateTimeFormat.ExpandPredefinedFormat(format, ref dateTime, ref dtfi, ref offset);
			}
			return DateTimeFormat.FormatCustomized(dateTime, format, dtfi, offset, null);
		}

		// Token: 0x060007B2 RID: 1970 RVA: 0x0001F898 File Offset: 0x0001DA98
		private unsafe static bool TryFormatO(DateTime dateTime, TimeSpan offset, Span<char> destination, out int charsWritten)
		{
			int num = 27;
			DateTimeKind dateTimeKind = DateTimeKind.Local;
			if (offset == DateTimeFormat.NullOffset)
			{
				dateTimeKind = dateTime.Kind;
				if (dateTimeKind == DateTimeKind.Local)
				{
					offset = TimeZoneInfo.Local.GetUtcOffset(dateTime);
					num += 6;
				}
				else if (dateTimeKind == DateTimeKind.Utc)
				{
					num++;
				}
			}
			else
			{
				num += 6;
			}
			if (destination.Length < num)
			{
				charsWritten = 0;
				return false;
			}
			charsWritten = num;
			ref char ptr = ref destination[26];
			DateTimeFormat.WriteFourDecimalDigits((uint)dateTime.Year, destination, 0);
			*destination[4] = '-';
			DateTimeFormat.WriteTwoDecimalDigits((uint)dateTime.Month, destination, 5);
			*destination[7] = '-';
			DateTimeFormat.WriteTwoDecimalDigits((uint)dateTime.Day, destination, 8);
			*destination[10] = 'T';
			DateTimeFormat.WriteTwoDecimalDigits((uint)dateTime.Hour, destination, 11);
			*destination[13] = ':';
			DateTimeFormat.WriteTwoDecimalDigits((uint)dateTime.Minute, destination, 14);
			*destination[16] = ':';
			DateTimeFormat.WriteTwoDecimalDigits((uint)dateTime.Second, destination, 17);
			*destination[19] = '.';
			DateTimeFormat.WriteDigits((ulong)((uint)(dateTime.Ticks % 10000000L)), destination.Slice(20, 7));
			if (dateTimeKind == DateTimeKind.Local)
			{
				char c;
				if (offset < default(TimeSpan))
				{
					c = '-';
					offset = TimeSpan.FromTicks(-offset.Ticks);
				}
				else
				{
					c = '+';
				}
				DateTimeFormat.WriteTwoDecimalDigits((uint)offset.Minutes, destination, 31);
				*destination[30] = ':';
				DateTimeFormat.WriteTwoDecimalDigits((uint)offset.Hours, destination, 28);
				*destination[27] = c;
			}
			else if (dateTimeKind == DateTimeKind.Utc)
			{
				*destination[27] = 'Z';
			}
			return true;
		}

		// Token: 0x060007B3 RID: 1971 RVA: 0x0001FA30 File Offset: 0x0001DC30
		private unsafe static bool TryFormatR(DateTime dateTime, TimeSpan offset, Span<char> destination, out int charsWritten)
		{
			if (28 >= destination.Length)
			{
				charsWritten = 0;
				return false;
			}
			if (offset != DateTimeFormat.NullOffset)
			{
				dateTime -= offset;
			}
			int num;
			int num2;
			int num3;
			dateTime.GetDatePart(out num, out num2, out num3);
			string text = DateTimeFormat.InvariantAbbreviatedDayNames[(int)dateTime.DayOfWeek];
			string text2 = DateTimeFormat.InvariantAbbreviatedMonthNames[num2 - 1];
			*destination[0] = text[0];
			*destination[1] = text[1];
			*destination[2] = text[2];
			*destination[3] = ',';
			*destination[4] = ' ';
			DateTimeFormat.WriteTwoDecimalDigits((uint)num3, destination, 5);
			*destination[7] = ' ';
			*destination[8] = text2[0];
			*destination[9] = text2[1];
			*destination[10] = text2[2];
			*destination[11] = ' ';
			DateTimeFormat.WriteFourDecimalDigits((uint)num, destination, 12);
			*destination[16] = ' ';
			DateTimeFormat.WriteTwoDecimalDigits((uint)dateTime.Hour, destination, 17);
			*destination[19] = ':';
			DateTimeFormat.WriteTwoDecimalDigits((uint)dateTime.Minute, destination, 20);
			*destination[22] = ':';
			DateTimeFormat.WriteTwoDecimalDigits((uint)dateTime.Second, destination, 23);
			*destination[25] = ' ';
			*destination[26] = 'G';
			*destination[27] = 'M';
			*destination[28] = 'T';
			charsWritten = 29;
			return true;
		}

		// Token: 0x060007B4 RID: 1972 RVA: 0x0001FBB4 File Offset: 0x0001DDB4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private unsafe static void WriteTwoDecimalDigits(uint value, Span<char> destination, int offset)
		{
			uint num = 48U + value;
			value /= 10U;
			*destination[offset + 1] = (char)(num - value * 10U);
			*destination[offset] = (char)(48U + value);
		}

		// Token: 0x060007B5 RID: 1973 RVA: 0x0001FBEC File Offset: 0x0001DDEC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private unsafe static void WriteFourDecimalDigits(uint value, Span<char> buffer, int startingIndex = 0)
		{
			uint num = 48U + value;
			value /= 10U;
			*buffer[startingIndex + 3] = (char)(num - value * 10U);
			num = 48U + value;
			value /= 10U;
			*buffer[startingIndex + 2] = (char)(num - value * 10U);
			num = 48U + value;
			value /= 10U;
			*buffer[startingIndex + 1] = (char)(num - value * 10U);
			*buffer[startingIndex] = (char)(48U + value);
		}

		// Token: 0x060007B6 RID: 1974 RVA: 0x0001FC60 File Offset: 0x0001DE60
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private unsafe static void WriteDigits(ulong value, Span<char> buffer)
		{
			for (int i = buffer.Length - 1; i >= 1; i--)
			{
				ulong num = 48UL + value;
				value /= 10UL;
				*buffer[i] = (char)(num - value * 10UL);
			}
			*buffer[0] = (char)(48UL + value);
		}

		// Token: 0x060007B7 RID: 1975 RVA: 0x00002C89 File Offset: 0x00000E89
		internal static void InvalidFormatForLocal(ReadOnlySpan<char> format, DateTime dateTime)
		{
		}

		// Token: 0x04000338 RID: 824
		internal static readonly TimeSpan NullOffset = TimeSpan.MinValue;

		// Token: 0x04000339 RID: 825
		internal static char[] allStandardFormats = new char[]
		{
			'd', 'D', 'f', 'F', 'g', 'G', 'm', 'M', 'o', 'O',
			'r', 'R', 's', 't', 'T', 'u', 'U', 'y', 'Y'
		};

		// Token: 0x0400033A RID: 826
		internal static readonly DateTimeFormatInfo InvariantFormatInfo = CultureInfo.InvariantCulture.DateTimeFormat;

		// Token: 0x0400033B RID: 827
		internal static readonly string[] InvariantAbbreviatedMonthNames = DateTimeFormat.InvariantFormatInfo.AbbreviatedMonthNames;

		// Token: 0x0400033C RID: 828
		internal static readonly string[] InvariantAbbreviatedDayNames = DateTimeFormat.InvariantFormatInfo.AbbreviatedDayNames;

		// Token: 0x0400033D RID: 829
		internal static string[] fixedNumberFormats = new string[] { "0", "00", "000", "0000", "00000", "000000", "0000000" };
	}
}
