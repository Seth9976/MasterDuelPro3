using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace System.Globalization
{
	// Token: 0x020006C2 RID: 1730
	[StructLayout(LayoutKind.Sequential)]
	internal class CultureData
	{
		// Token: 0x060036B3 RID: 14003 RVA: 0x000D2B27 File Offset: 0x000D0D27
		private CultureData(string name)
		{
			this.sRealName = name;
		}

		// Token: 0x17000847 RID: 2119
		// (get) Token: 0x060036B4 RID: 14004 RVA: 0x000D2B38 File Offset: 0x000D0D38
		public static CultureData Invariant
		{
			get
			{
				if (CultureData.s_Invariant == null)
				{
					CultureData cultureData = new CultureData("");
					cultureData.sISO639Language = "iv";
					cultureData.sAM1159 = "AM";
					cultureData.sPM2359 = "PM";
					cultureData.sTimeSeparator = ":";
					cultureData.saLongTimes = new string[] { "HH:mm:ss" };
					cultureData.saShortTimes = new string[] { "HH:mm", "hh:mm tt", "H:mm", "h:mm tt" };
					cultureData.iFirstDayOfWeek = 0;
					cultureData.iFirstWeekOfYear = 0;
					cultureData.waCalendars = new int[] { 1 };
					cultureData.calendars = new CalendarData[23];
					cultureData.calendars[0] = CalendarData.Invariant;
					cultureData.iDefaultAnsiCodePage = 1252;
					cultureData.iDefaultOemCodePage = 437;
					cultureData.iDefaultMacCodePage = 10000;
					cultureData.iDefaultEbcdicCodePage = 37;
					cultureData.sListSeparator = ",";
					Interlocked.CompareExchange<CultureData>(ref CultureData.s_Invariant, cultureData, null);
				}
				return CultureData.s_Invariant;
			}
		}

		// Token: 0x060036B5 RID: 14005 RVA: 0x000D2C4C File Offset: 0x000D0E4C
		public static CultureData GetCultureData(string cultureName, bool useUserOverride)
		{
			CultureData cultureData;
			try
			{
				cultureData = new CultureInfo(cultureName, useUserOverride).m_cultureData;
			}
			catch
			{
				cultureData = null;
			}
			return cultureData;
		}

		// Token: 0x060036B6 RID: 14006 RVA: 0x000D2C80 File Offset: 0x000D0E80
		public static CultureData GetCultureData(string cultureName, bool useUserOverride, int datetimeIndex, int calendarId, int numberIndex, string iso2lang, int ansiCodePage, int oemCodePage, int macCodePage, int ebcdicCodePage, bool rightToLeft, string listSeparator)
		{
			if (string.IsNullOrEmpty(cultureName))
			{
				return CultureData.Invariant;
			}
			CultureData cultureData = new CultureData(cultureName);
			cultureData.fill_culture_data(datetimeIndex);
			cultureData.bUseOverrides = useUserOverride;
			cultureData.calendarId = calendarId;
			cultureData.numberIndex = numberIndex;
			cultureData.sISO639Language = iso2lang;
			cultureData.iDefaultAnsiCodePage = ansiCodePage;
			cultureData.iDefaultOemCodePage = oemCodePage;
			cultureData.iDefaultMacCodePage = macCodePage;
			cultureData.iDefaultEbcdicCodePage = ebcdicCodePage;
			cultureData.isRightToLeft = rightToLeft;
			cultureData.sListSeparator = listSeparator;
			return cultureData;
		}

		// Token: 0x060036B7 RID: 14007
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void fill_culture_data(int datetimeIndex);

		// Token: 0x060036B8 RID: 14008 RVA: 0x000D2CF8 File Offset: 0x000D0EF8
		public CalendarData GetCalendar(int calendarId)
		{
			int num = calendarId - 1;
			if (this.calendars == null)
			{
				this.calendars = new CalendarData[23];
			}
			CalendarData calendarData = this.calendars[num];
			if (calendarData == null)
			{
				calendarData = new CalendarData(this.sRealName, calendarId, this.bUseOverrides);
				this.calendars[num] = calendarData;
			}
			return calendarData;
		}

		// Token: 0x17000848 RID: 2120
		// (get) Token: 0x060036B9 RID: 14009 RVA: 0x000D2D47 File Offset: 0x000D0F47
		internal string[] LongTimes
		{
			get
			{
				return this.saLongTimes;
			}
		}

		// Token: 0x17000849 RID: 2121
		// (get) Token: 0x060036BA RID: 14010 RVA: 0x000D2D51 File Offset: 0x000D0F51
		internal string[] ShortTimes
		{
			get
			{
				return this.saShortTimes;
			}
		}

		// Token: 0x1700084A RID: 2122
		// (get) Token: 0x060036BB RID: 14011 RVA: 0x000D2D5B File Offset: 0x000D0F5B
		internal string SISO639LANGNAME
		{
			get
			{
				return this.sISO639Language;
			}
		}

		// Token: 0x1700084B RID: 2123
		// (get) Token: 0x060036BC RID: 14012 RVA: 0x000D2D63 File Offset: 0x000D0F63
		internal int IFIRSTDAYOFWEEK
		{
			get
			{
				return this.iFirstDayOfWeek;
			}
		}

		// Token: 0x1700084C RID: 2124
		// (get) Token: 0x060036BD RID: 14013 RVA: 0x000D2D6B File Offset: 0x000D0F6B
		internal int IFIRSTWEEKOFYEAR
		{
			get
			{
				return this.iFirstWeekOfYear;
			}
		}

		// Token: 0x1700084D RID: 2125
		// (get) Token: 0x060036BE RID: 14014 RVA: 0x000D2D73 File Offset: 0x000D0F73
		internal string SAM1159
		{
			get
			{
				return this.sAM1159;
			}
		}

		// Token: 0x1700084E RID: 2126
		// (get) Token: 0x060036BF RID: 14015 RVA: 0x000D2D7B File Offset: 0x000D0F7B
		internal string SPM2359
		{
			get
			{
				return this.sPM2359;
			}
		}

		// Token: 0x1700084F RID: 2127
		// (get) Token: 0x060036C0 RID: 14016 RVA: 0x000D2D83 File Offset: 0x000D0F83
		internal string TimeSeparator
		{
			get
			{
				return this.sTimeSeparator;
			}
		}

		// Token: 0x17000850 RID: 2128
		// (get) Token: 0x060036C1 RID: 14017 RVA: 0x000D2D8C File Offset: 0x000D0F8C
		internal int[] CalendarIds
		{
			get
			{
				if (this.waCalendars == null)
				{
					string text = this.sISO639Language;
					if (!(text == "ja"))
					{
						if (!(text == "zh"))
						{
							if (!(text == "he"))
							{
								this.waCalendars = new int[] { this.calendarId };
							}
							else
							{
								this.waCalendars = new int[] { this.calendarId, 8 };
							}
						}
						else
						{
							this.waCalendars = new int[] { this.calendarId, 4 };
						}
					}
					else
					{
						this.waCalendars = new int[] { this.calendarId, 3 };
					}
				}
				return this.waCalendars;
			}
		}

		// Token: 0x060036C2 RID: 14018 RVA: 0x000D2E4C File Offset: 0x000D104C
		internal CalendarId[] GetCalendarIds()
		{
			CalendarId[] array = new CalendarId[this.CalendarIds.Length];
			for (int i = 0; i < this.CalendarIds.Length; i++)
			{
				array[i] = (CalendarId)this.CalendarIds[i];
			}
			return array;
		}

		// Token: 0x17000851 RID: 2129
		// (get) Token: 0x060036C3 RID: 14019 RVA: 0x000D2E87 File Offset: 0x000D1087
		internal bool IsInvariantCulture
		{
			get
			{
				return string.IsNullOrEmpty(this.sRealName);
			}
		}

		// Token: 0x17000852 RID: 2130
		// (get) Token: 0x060036C4 RID: 14020 RVA: 0x000D2E94 File Offset: 0x000D1094
		internal string CultureName
		{
			get
			{
				return this.sRealName;
			}
		}

		// Token: 0x17000853 RID: 2131
		// (get) Token: 0x060036C5 RID: 14021 RVA: 0x000D2E9C File Offset: 0x000D109C
		internal string SCOMPAREINFO
		{
			get
			{
				return "";
			}
		}

		// Token: 0x17000854 RID: 2132
		// (get) Token: 0x060036C6 RID: 14022 RVA: 0x000D2E94 File Offset: 0x000D1094
		internal string STEXTINFO
		{
			get
			{
				return this.sRealName;
			}
		}

		// Token: 0x17000855 RID: 2133
		// (get) Token: 0x060036C7 RID: 14023 RVA: 0x000D2EA3 File Offset: 0x000D10A3
		internal int IDEFAULTANSICODEPAGE
		{
			get
			{
				return this.iDefaultAnsiCodePage;
			}
		}

		// Token: 0x17000856 RID: 2134
		// (get) Token: 0x060036C8 RID: 14024 RVA: 0x000D2EAB File Offset: 0x000D10AB
		internal bool IsRightToLeft
		{
			get
			{
				return this.isRightToLeft;
			}
		}

		// Token: 0x17000857 RID: 2135
		// (get) Token: 0x060036C9 RID: 14025 RVA: 0x000D2EB3 File Offset: 0x000D10B3
		internal string SLIST
		{
			get
			{
				return this.sListSeparator;
			}
		}

		// Token: 0x17000858 RID: 2136
		// (get) Token: 0x060036CA RID: 14026 RVA: 0x000D2EBB File Offset: 0x000D10BB
		internal bool UseUserOverride
		{
			get
			{
				return this.bUseOverrides;
			}
		}

		// Token: 0x060036CB RID: 14027 RVA: 0x000D2EC3 File Offset: 0x000D10C3
		internal string[] EraNames(int calendarId)
		{
			return this.GetCalendar(calendarId).saEraNames;
		}

		// Token: 0x060036CC RID: 14028 RVA: 0x000D2ED1 File Offset: 0x000D10D1
		internal string[] AbbrevEraNames(int calendarId)
		{
			return this.GetCalendar(calendarId).saAbbrevEraNames;
		}

		// Token: 0x060036CD RID: 14029 RVA: 0x000D2EDF File Offset: 0x000D10DF
		internal string[] AbbreviatedEnglishEraNames(int calendarId)
		{
			return this.GetCalendar(calendarId).saAbbrevEnglishEraNames;
		}

		// Token: 0x060036CE RID: 14030 RVA: 0x000D2EED File Offset: 0x000D10ED
		internal string[] ShortDates(int calendarId)
		{
			return this.GetCalendar(calendarId).saShortDates;
		}

		// Token: 0x060036CF RID: 14031 RVA: 0x000D2EFB File Offset: 0x000D10FB
		internal string[] LongDates(int calendarId)
		{
			return this.GetCalendar(calendarId).saLongDates;
		}

		// Token: 0x060036D0 RID: 14032 RVA: 0x000D2F09 File Offset: 0x000D1109
		internal string[] YearMonths(int calendarId)
		{
			return this.GetCalendar(calendarId).saYearMonths;
		}

		// Token: 0x060036D1 RID: 14033 RVA: 0x000D2F17 File Offset: 0x000D1117
		internal string[] DayNames(int calendarId)
		{
			return this.GetCalendar(calendarId).saDayNames;
		}

		// Token: 0x060036D2 RID: 14034 RVA: 0x000D2F25 File Offset: 0x000D1125
		internal string[] AbbreviatedDayNames(int calendarId)
		{
			return this.GetCalendar(calendarId).saAbbrevDayNames;
		}

		// Token: 0x060036D3 RID: 14035 RVA: 0x000D2F33 File Offset: 0x000D1133
		internal string[] MonthNames(int calendarId)
		{
			return this.GetCalendar(calendarId).saMonthNames;
		}

		// Token: 0x060036D4 RID: 14036 RVA: 0x000D2F41 File Offset: 0x000D1141
		internal string[] GenitiveMonthNames(int calendarId)
		{
			return this.GetCalendar(calendarId).saMonthGenitiveNames;
		}

		// Token: 0x060036D5 RID: 14037 RVA: 0x000D2F4F File Offset: 0x000D114F
		internal string[] AbbreviatedMonthNames(int calendarId)
		{
			return this.GetCalendar(calendarId).saAbbrevMonthNames;
		}

		// Token: 0x060036D6 RID: 14038 RVA: 0x000D2F5D File Offset: 0x000D115D
		internal string[] AbbreviatedGenitiveMonthNames(int calendarId)
		{
			return this.GetCalendar(calendarId).saAbbrevMonthGenitiveNames;
		}

		// Token: 0x060036D7 RID: 14039 RVA: 0x000D2F6B File Offset: 0x000D116B
		internal string[] LeapYearMonthNames(int calendarId)
		{
			return this.GetCalendar(calendarId).saLeapYearMonthNames;
		}

		// Token: 0x060036D8 RID: 14040 RVA: 0x000D2F79 File Offset: 0x000D1179
		internal string MonthDay(int calendarId)
		{
			return this.GetCalendar(calendarId).sMonthDay;
		}

		// Token: 0x060036D9 RID: 14041 RVA: 0x000D2F87 File Offset: 0x000D1187
		internal string DateSeparator(int calendarId)
		{
			if (calendarId == 3 && !AppContextSwitches.EnforceLegacyJapaneseDateParsing)
			{
				return "/";
			}
			return CultureData.GetDateSeparator(this.ShortDates(calendarId)[0]);
		}

		// Token: 0x060036DA RID: 14042 RVA: 0x000D2FA8 File Offset: 0x000D11A8
		private static string GetDateSeparator(string format)
		{
			return CultureData.GetSeparator(format, "dyM");
		}

		// Token: 0x060036DB RID: 14043 RVA: 0x000D2FB8 File Offset: 0x000D11B8
		private static string GetSeparator(string format, string timeParts)
		{
			int num = CultureData.IndexOfTimePart(format, 0, timeParts);
			if (num != -1)
			{
				char c = format[num];
				do
				{
					num++;
				}
				while (num < format.Length && format[num] == c);
				int num2 = num;
				if (num2 < format.Length)
				{
					int num3 = CultureData.IndexOfTimePart(format, num2, timeParts);
					if (num3 != -1)
					{
						return CultureData.UnescapeNlsString(format, num2, num3 - 1);
					}
				}
			}
			return string.Empty;
		}

		// Token: 0x060036DC RID: 14044 RVA: 0x000D301C File Offset: 0x000D121C
		private static int IndexOfTimePart(string format, int startIndex, string timeParts)
		{
			bool flag = false;
			for (int i = startIndex; i < format.Length; i++)
			{
				if (!flag && timeParts.IndexOf(format[i]) != -1)
				{
					return i;
				}
				char c = format[i];
				if (c != '\'')
				{
					if (c == '\\' && i + 1 < format.Length)
					{
						i++;
						char c2 = format[i];
						if (c2 != '\'' && c2 != '\\')
						{
							i--;
						}
					}
				}
				else
				{
					flag = !flag;
				}
			}
			return -1;
		}

		// Token: 0x060036DD RID: 14045 RVA: 0x000D3090 File Offset: 0x000D1290
		private static string UnescapeNlsString(string str, int start, int end)
		{
			StringBuilder stringBuilder = null;
			int num = start;
			while (num < str.Length && num <= end)
			{
				char c = str[num];
				if (c != '\'')
				{
					if (c != '\\')
					{
						if (stringBuilder != null)
						{
							stringBuilder.Append(str[num]);
						}
					}
					else
					{
						if (stringBuilder == null)
						{
							stringBuilder = new StringBuilder(str, start, num - start, str.Length);
						}
						num++;
						if (num < str.Length)
						{
							stringBuilder.Append(str[num]);
						}
					}
				}
				else if (stringBuilder == null)
				{
					stringBuilder = new StringBuilder(str, start, num - start, str.Length);
				}
				num++;
			}
			if (stringBuilder == null)
			{
				return str.Substring(start, end - start + 1);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060036DE RID: 14046 RVA: 0x00002645 File Offset: 0x00000845
		internal static string[] ReescapeWin32Strings(string[] array)
		{
			return array;
		}

		// Token: 0x060036DF RID: 14047 RVA: 0x00002645 File Offset: 0x00000845
		internal static string ReescapeWin32String(string str)
		{
			return str;
		}

		// Token: 0x060036E0 RID: 14048 RVA: 0x000D3138 File Offset: 0x000D1338
		private unsafe static int strlen(byte* s)
		{
			int num = 0;
			while (s[num] != 0)
			{
				num++;
			}
			return num;
		}

		// Token: 0x060036E1 RID: 14049 RVA: 0x000D3154 File Offset: 0x000D1354
		private unsafe static string idx2string(byte* data, int idx)
		{
			return Encoding.UTF8.GetString(data + idx, CultureData.strlen(data + idx));
		}

		// Token: 0x060036E2 RID: 14050 RVA: 0x000D316B File Offset: 0x000D136B
		private int[] create_group_sizes_array(int gs0, int gs1)
		{
			if (gs0 == -1)
			{
				return new int[0];
			}
			if (gs1 != -1)
			{
				return new int[] { gs0, gs1 };
			}
			return new int[] { gs0 };
		}

		// Token: 0x060036E3 RID: 14051 RVA: 0x000D3198 File Offset: 0x000D1398
		internal unsafe void GetNFIValues(NumberFormatInfo nfi)
		{
			if (!this.IsInvariantCulture)
			{
				CultureData.NumberFormatEntryManaged numberFormatEntryManaged = default(CultureData.NumberFormatEntryManaged);
				byte* ptr = CultureData.fill_number_data(this.numberIndex, ref numberFormatEntryManaged);
				nfi.currencyGroupSizes = this.create_group_sizes_array(numberFormatEntryManaged.currency_group_sizes0, numberFormatEntryManaged.currency_group_sizes1);
				nfi.numberGroupSizes = this.create_group_sizes_array(numberFormatEntryManaged.number_group_sizes0, numberFormatEntryManaged.number_group_sizes1);
				nfi.NaNSymbol = CultureData.idx2string(ptr, numberFormatEntryManaged.nan_symbol);
				nfi.currencyDecimalDigits = numberFormatEntryManaged.currency_decimal_digits;
				nfi.currencyDecimalSeparator = CultureData.idx2string(ptr, numberFormatEntryManaged.currency_decimal_separator);
				nfi.currencyGroupSeparator = CultureData.idx2string(ptr, numberFormatEntryManaged.currency_group_separator);
				nfi.currencyNegativePattern = numberFormatEntryManaged.currency_negative_pattern;
				nfi.currencyPositivePattern = numberFormatEntryManaged.currency_positive_pattern;
				nfi.currencySymbol = CultureData.idx2string(ptr, numberFormatEntryManaged.currency_symbol);
				nfi.negativeInfinitySymbol = CultureData.idx2string(ptr, numberFormatEntryManaged.negative_infinity_symbol);
				nfi.negativeSign = CultureData.idx2string(ptr, numberFormatEntryManaged.negative_sign);
				nfi.numberDecimalDigits = numberFormatEntryManaged.number_decimal_digits;
				nfi.numberDecimalSeparator = CultureData.idx2string(ptr, numberFormatEntryManaged.number_decimal_separator);
				nfi.numberGroupSeparator = CultureData.idx2string(ptr, numberFormatEntryManaged.number_group_separator);
				nfi.numberNegativePattern = numberFormatEntryManaged.number_negative_pattern;
				nfi.perMilleSymbol = CultureData.idx2string(ptr, numberFormatEntryManaged.per_mille_symbol);
				nfi.percentNegativePattern = numberFormatEntryManaged.percent_negative_pattern;
				nfi.percentPositivePattern = numberFormatEntryManaged.percent_positive_pattern;
				nfi.percentSymbol = CultureData.idx2string(ptr, numberFormatEntryManaged.percent_symbol);
				nfi.positiveInfinitySymbol = CultureData.idx2string(ptr, numberFormatEntryManaged.positive_infinity_symbol);
				nfi.positiveSign = CultureData.idx2string(ptr, numberFormatEntryManaged.positive_sign);
			}
			nfi.percentDecimalDigits = nfi.numberDecimalDigits;
			nfi.percentDecimalSeparator = nfi.numberDecimalSeparator;
			nfi.percentGroupSizes = nfi.numberGroupSizes;
			nfi.percentGroupSeparator = nfi.numberGroupSeparator;
		}

		// Token: 0x060036E4 RID: 14052
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern byte* fill_number_data(int index, ref CultureData.NumberFormatEntryManaged nfe);

		// Token: 0x04001D4E RID: 7502
		private string sAM1159;

		// Token: 0x04001D4F RID: 7503
		private string sPM2359;

		// Token: 0x04001D50 RID: 7504
		private string sTimeSeparator;

		// Token: 0x04001D51 RID: 7505
		private volatile string[] saLongTimes;

		// Token: 0x04001D52 RID: 7506
		private volatile string[] saShortTimes;

		// Token: 0x04001D53 RID: 7507
		private int iFirstDayOfWeek;

		// Token: 0x04001D54 RID: 7508
		private int iFirstWeekOfYear;

		// Token: 0x04001D55 RID: 7509
		private volatile int[] waCalendars;

		// Token: 0x04001D56 RID: 7510
		private CalendarData[] calendars;

		// Token: 0x04001D57 RID: 7511
		private string sISO639Language;

		// Token: 0x04001D58 RID: 7512
		private readonly string sRealName;

		// Token: 0x04001D59 RID: 7513
		private bool bUseOverrides;

		// Token: 0x04001D5A RID: 7514
		private int calendarId;

		// Token: 0x04001D5B RID: 7515
		private int numberIndex;

		// Token: 0x04001D5C RID: 7516
		private int iDefaultAnsiCodePage;

		// Token: 0x04001D5D RID: 7517
		private int iDefaultOemCodePage;

		// Token: 0x04001D5E RID: 7518
		private int iDefaultMacCodePage;

		// Token: 0x04001D5F RID: 7519
		private int iDefaultEbcdicCodePage;

		// Token: 0x04001D60 RID: 7520
		private bool isRightToLeft;

		// Token: 0x04001D61 RID: 7521
		private string sListSeparator;

		// Token: 0x04001D62 RID: 7522
		private static CultureData s_Invariant;

		// Token: 0x020006C3 RID: 1731
		internal struct NumberFormatEntryManaged
		{
			// Token: 0x04001D63 RID: 7523
			internal int currency_decimal_digits;

			// Token: 0x04001D64 RID: 7524
			internal int currency_decimal_separator;

			// Token: 0x04001D65 RID: 7525
			internal int currency_group_separator;

			// Token: 0x04001D66 RID: 7526
			internal int currency_group_sizes0;

			// Token: 0x04001D67 RID: 7527
			internal int currency_group_sizes1;

			// Token: 0x04001D68 RID: 7528
			internal int currency_negative_pattern;

			// Token: 0x04001D69 RID: 7529
			internal int currency_positive_pattern;

			// Token: 0x04001D6A RID: 7530
			internal int currency_symbol;

			// Token: 0x04001D6B RID: 7531
			internal int nan_symbol;

			// Token: 0x04001D6C RID: 7532
			internal int negative_infinity_symbol;

			// Token: 0x04001D6D RID: 7533
			internal int negative_sign;

			// Token: 0x04001D6E RID: 7534
			internal int number_decimal_digits;

			// Token: 0x04001D6F RID: 7535
			internal int number_decimal_separator;

			// Token: 0x04001D70 RID: 7536
			internal int number_group_separator;

			// Token: 0x04001D71 RID: 7537
			internal int number_group_sizes0;

			// Token: 0x04001D72 RID: 7538
			internal int number_group_sizes1;

			// Token: 0x04001D73 RID: 7539
			internal int number_negative_pattern;

			// Token: 0x04001D74 RID: 7540
			internal int per_mille_symbol;

			// Token: 0x04001D75 RID: 7541
			internal int percent_negative_pattern;

			// Token: 0x04001D76 RID: 7542
			internal int percent_positive_pattern;

			// Token: 0x04001D77 RID: 7543
			internal int percent_symbol;

			// Token: 0x04001D78 RID: 7544
			internal int positive_infinity_symbol;

			// Token: 0x04001D79 RID: 7545
			internal int positive_sign;
		}
	}
}
