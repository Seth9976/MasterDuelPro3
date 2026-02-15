using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Globalization
{
	// Token: 0x020006B2 RID: 1714
	[StructLayout(LayoutKind.Sequential)]
	internal class CalendarData
	{
		// Token: 0x0600358F RID: 13711 RVA: 0x000CE97B File Offset: 0x000CCB7B
		private CalendarData()
		{
		}

		// Token: 0x06003590 RID: 13712 RVA: 0x000CE990 File Offset: 0x000CCB90
		static CalendarData()
		{
			CalendarData calendarData = new CalendarData();
			calendarData.sNativeName = "Gregorian Calendar";
			calendarData.iTwoDigitYearMax = 2029;
			calendarData.iCurrentEra = 1;
			calendarData.saShortDates = new string[] { "MM/dd/yyyy", "yyyy-MM-dd" };
			calendarData.saLongDates = new string[] { "dddd, dd MMMM yyyy" };
			calendarData.saYearMonths = new string[] { "yyyy MMMM" };
			calendarData.sMonthDay = "MMMM dd";
			calendarData.saEraNames = new string[] { "A.D." };
			calendarData.saAbbrevEraNames = new string[] { "AD" };
			calendarData.saAbbrevEnglishEraNames = new string[] { "AD" };
			calendarData.saDayNames = new string[] { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };
			calendarData.saAbbrevDayNames = new string[] { "Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat" };
			calendarData.saSuperShortDayNames = new string[] { "Su", "Mo", "Tu", "We", "Th", "Fr", "Sa" };
			calendarData.saMonthNames = new string[]
			{
				"January",
				"February",
				"March",
				"April",
				"May",
				"June",
				"July",
				"August",
				"September",
				"October",
				"November",
				"December",
				string.Empty
			};
			calendarData.saAbbrevMonthNames = new string[]
			{
				"Jan",
				"Feb",
				"Mar",
				"Apr",
				"May",
				"Jun",
				"Jul",
				"Aug",
				"Sep",
				"Oct",
				"Nov",
				"Dec",
				string.Empty
			};
			calendarData.saMonthGenitiveNames = calendarData.saMonthNames;
			calendarData.saAbbrevMonthGenitiveNames = calendarData.saAbbrevMonthNames;
			calendarData.saLeapYearMonthNames = calendarData.saMonthNames;
			calendarData.bUseUserOverrides = false;
			CalendarData.Invariant = calendarData;
		}

		// Token: 0x06003591 RID: 13713 RVA: 0x000CED2C File Offset: 0x000CCF2C
		internal CalendarData(string localeName, int calendarId, bool bUseUserOverrides)
		{
			this.bUseUserOverrides = bUseUserOverrides;
			if (!CalendarData.nativeGetCalendarData(this, localeName, calendarId))
			{
				if (this.sNativeName == null)
				{
					this.sNativeName = string.Empty;
				}
				if (this.saShortDates == null)
				{
					this.saShortDates = CalendarData.Invariant.saShortDates;
				}
				if (this.saYearMonths == null)
				{
					this.saYearMonths = CalendarData.Invariant.saYearMonths;
				}
				if (this.saLongDates == null)
				{
					this.saLongDates = CalendarData.Invariant.saLongDates;
				}
				if (this.sMonthDay == null)
				{
					this.sMonthDay = CalendarData.Invariant.sMonthDay;
				}
				if (this.saEraNames == null)
				{
					this.saEraNames = CalendarData.Invariant.saEraNames;
				}
				if (this.saAbbrevEraNames == null)
				{
					this.saAbbrevEraNames = CalendarData.Invariant.saAbbrevEraNames;
				}
				if (this.saAbbrevEnglishEraNames == null)
				{
					this.saAbbrevEnglishEraNames = CalendarData.Invariant.saAbbrevEnglishEraNames;
				}
				if (this.saDayNames == null)
				{
					this.saDayNames = CalendarData.Invariant.saDayNames;
				}
				if (this.saAbbrevDayNames == null)
				{
					this.saAbbrevDayNames = CalendarData.Invariant.saAbbrevDayNames;
				}
				if (this.saSuperShortDayNames == null)
				{
					this.saSuperShortDayNames = CalendarData.Invariant.saSuperShortDayNames;
				}
				if (this.saMonthNames == null)
				{
					this.saMonthNames = CalendarData.Invariant.saMonthNames;
				}
				if (this.saAbbrevMonthNames == null)
				{
					this.saAbbrevMonthNames = CalendarData.Invariant.saAbbrevMonthNames;
				}
			}
			this.saShortDates = CultureData.ReescapeWin32Strings(this.saShortDates);
			this.saLongDates = CultureData.ReescapeWin32Strings(this.saLongDates);
			this.saYearMonths = CultureData.ReescapeWin32Strings(this.saYearMonths);
			this.sMonthDay = CultureData.ReescapeWin32String(this.sMonthDay);
			if ((ushort)calendarId == 4)
			{
				if (CultureInfo.IsTaiwanSku)
				{
					this.sNativeName = "中華民國曆";
				}
				else
				{
					this.sNativeName = string.Empty;
				}
			}
			if (this.saMonthGenitiveNames == null || string.IsNullOrEmpty(this.saMonthGenitiveNames[0]))
			{
				this.saMonthGenitiveNames = this.saMonthNames;
			}
			if (this.saAbbrevMonthGenitiveNames == null || string.IsNullOrEmpty(this.saAbbrevMonthGenitiveNames[0]))
			{
				this.saAbbrevMonthGenitiveNames = this.saAbbrevMonthNames;
			}
			if (this.saLeapYearMonthNames == null || string.IsNullOrEmpty(this.saLeapYearMonthNames[0]))
			{
				this.saLeapYearMonthNames = this.saMonthNames;
			}
			this.InitializeEraNames(localeName, calendarId);
			this.InitializeAbbreviatedEraNames(localeName, calendarId);
			if (!GlobalizationMode.Invariant && calendarId == 3)
			{
				this.saAbbrevEnglishEraNames = CalendarData.GetJapaneseEnglishEraNames();
			}
			else
			{
				this.saAbbrevEnglishEraNames = new string[] { "" };
			}
			this.iCurrentEra = this.saEraNames.Length;
		}

		// Token: 0x06003592 RID: 13714 RVA: 0x000CEFAC File Offset: 0x000CD1AC
		private void InitializeEraNames(string localeName, int calendarId)
		{
			switch ((ushort)calendarId)
			{
			case 1:
				if (this.saEraNames == null || this.saEraNames.Length == 0 || string.IsNullOrEmpty(this.saEraNames[0]))
				{
					this.saEraNames = new string[] { "A.D." };
					return;
				}
				return;
			case 2:
			case 13:
				this.saEraNames = new string[] { "A.D." };
				return;
			case 3:
			case 14:
				this.saEraNames = CalendarData.GetJapaneseEraNames();
				return;
			case 4:
				if (CultureInfo.IsTaiwanSku)
				{
					this.saEraNames = new string[] { "中華民國" };
					return;
				}
				this.saEraNames = new string[] { string.Empty };
				return;
			case 5:
				this.saEraNames = new string[] { "단기" };
				return;
			case 6:
			case 23:
				if (localeName == "dv-MV")
				{
					this.saEraNames = new string[] { "ހ\u07a8ޖ\u07b0ރ\u07a9" };
					return;
				}
				this.saEraNames = new string[] { "بعد الهجرة" };
				return;
			case 7:
				this.saEraNames = new string[] { "พ.ศ." };
				return;
			case 8:
				this.saEraNames = new string[] { "C.E." };
				return;
			case 9:
				this.saEraNames = new string[] { "ap. J.-C." };
				return;
			case 10:
			case 11:
			case 12:
				this.saEraNames = new string[] { "م" };
				return;
			case 22:
				if (this.saEraNames == null || this.saEraNames.Length == 0 || string.IsNullOrEmpty(this.saEraNames[0]))
				{
					this.saEraNames = new string[] { "ه.ش" };
					return;
				}
				return;
			}
			this.saEraNames = CalendarData.Invariant.saEraNames;
		}

		// Token: 0x06003593 RID: 13715 RVA: 0x000CF194 File Offset: 0x000CD394
		private static string[] GetJapaneseEraNames()
		{
			if (GlobalizationMode.Invariant)
			{
				throw new PlatformNotSupportedException();
			}
			return JapaneseCalendar.EraNames();
		}

		// Token: 0x06003594 RID: 13716 RVA: 0x000CF1A8 File Offset: 0x000CD3A8
		private static string[] GetJapaneseEnglishEraNames()
		{
			if (GlobalizationMode.Invariant)
			{
				throw new PlatformNotSupportedException();
			}
			return JapaneseCalendar.EnglishEraNames();
		}

		// Token: 0x06003595 RID: 13717 RVA: 0x000CF1BC File Offset: 0x000CD3BC
		private void InitializeAbbreviatedEraNames(string localeName, int calendarId)
		{
			CalendarId calendarId2 = (CalendarId)calendarId;
			if (calendarId2 <= CalendarId.JULIAN)
			{
				switch (calendarId2)
				{
				case CalendarId.GREGORIAN:
					if (this.saAbbrevEraNames == null || this.saAbbrevEraNames.Length == 0 || string.IsNullOrEmpty(this.saAbbrevEraNames[0]))
					{
						this.saAbbrevEraNames = new string[] { "AD" };
						return;
					}
					return;
				case CalendarId.GREGORIAN_US:
					break;
				case CalendarId.JAPAN:
					goto IL_0096;
				case CalendarId.TAIWAN:
					this.saAbbrevEraNames = new string[1];
					if (this.saEraNames[0].Length == 4)
					{
						this.saAbbrevEraNames[0] = this.saEraNames[0].Substring(2, 2);
						return;
					}
					this.saAbbrevEraNames[0] = this.saEraNames[0];
					return;
				case CalendarId.KOREA:
					goto IL_0159;
				case CalendarId.HIJRI:
					goto IL_00B0;
				default:
					if (calendarId2 != CalendarId.JULIAN)
					{
						goto IL_0159;
					}
					break;
				}
				this.saAbbrevEraNames = new string[] { "AD" };
				return;
			}
			if (calendarId2 != CalendarId.JAPANESELUNISOLAR)
			{
				if (calendarId2 != CalendarId.PERSIAN)
				{
					if (calendarId2 != CalendarId.UMALQURA)
					{
						goto IL_0159;
					}
					goto IL_00B0;
				}
				else
				{
					if (this.saAbbrevEraNames == null || this.saAbbrevEraNames.Length == 0 || string.IsNullOrEmpty(this.saAbbrevEraNames[0]))
					{
						this.saAbbrevEraNames = this.saEraNames;
						return;
					}
					return;
				}
			}
			IL_0096:
			if (GlobalizationMode.Invariant)
			{
				throw new PlatformNotSupportedException();
			}
			this.saAbbrevEraNames = this.saEraNames;
			return;
			IL_00B0:
			if (localeName == "dv-MV")
			{
				this.saAbbrevEraNames = new string[] { "ހ." };
				return;
			}
			this.saAbbrevEraNames = new string[] { "هـ" };
			return;
			IL_0159:
			this.saAbbrevEraNames = this.saEraNames;
		}

		// Token: 0x06003596 RID: 13718 RVA: 0x000CF32E File Offset: 0x000CD52E
		internal static CalendarData GetCalendarData(int calendarId)
		{
			return CultureInfo.GetCultureInfo(CalendarData.CalendarIdToCultureName(calendarId)).m_cultureData.GetCalendar(calendarId);
		}

		// Token: 0x06003597 RID: 13719 RVA: 0x000CF348 File Offset: 0x000CD548
		private static string CalendarIdToCultureName(int calendarId)
		{
			switch (calendarId)
			{
			case 2:
				return "fa-IR";
			case 3:
				return "ja-JP";
			case 4:
				return "zh-TW";
			case 5:
				return "ko-KR";
			case 6:
			case 10:
			case 23:
				return "ar-SA";
			case 7:
				return "th-TH";
			case 8:
				return "he-IL";
			case 9:
				return "ar-DZ";
			case 11:
			case 12:
				return "ar-IQ";
			}
			return "en-US";
		}

		// Token: 0x06003598 RID: 13720 RVA: 0x000CE6F2 File Offset: 0x000CC8F2
		public static int nativeGetTwoDigitYearMax(int calID)
		{
			return -1;
		}

		// Token: 0x06003599 RID: 13721 RVA: 0x000CF3F2 File Offset: 0x000CD5F2
		private static bool nativeGetCalendarData(CalendarData data, string localeName, int calendarId)
		{
			if (data.fill_calendar_data(localeName.ToLowerInvariant(), calendarId))
			{
				if ((ushort)calendarId == 8)
				{
					data.saMonthNames = CalendarData.HEBREW_MONTH_NAMES;
					data.saLeapYearMonthNames = CalendarData.HEBREW_LEAP_MONTH_NAMES;
				}
				return true;
			}
			return false;
		}

		// Token: 0x0600359A RID: 13722
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool fill_calendar_data(string localeName, int datetimeIndex);

		// Token: 0x04001CD4 RID: 7380
		internal const int MAX_CALENDARS = 23;

		// Token: 0x04001CD5 RID: 7381
		internal string sNativeName;

		// Token: 0x04001CD6 RID: 7382
		internal string[] saShortDates;

		// Token: 0x04001CD7 RID: 7383
		internal string[] saYearMonths;

		// Token: 0x04001CD8 RID: 7384
		internal string[] saLongDates;

		// Token: 0x04001CD9 RID: 7385
		internal string sMonthDay;

		// Token: 0x04001CDA RID: 7386
		internal string[] saEraNames;

		// Token: 0x04001CDB RID: 7387
		internal string[] saAbbrevEraNames;

		// Token: 0x04001CDC RID: 7388
		internal string[] saAbbrevEnglishEraNames;

		// Token: 0x04001CDD RID: 7389
		internal string[] saDayNames;

		// Token: 0x04001CDE RID: 7390
		internal string[] saAbbrevDayNames;

		// Token: 0x04001CDF RID: 7391
		internal string[] saSuperShortDayNames;

		// Token: 0x04001CE0 RID: 7392
		internal string[] saMonthNames;

		// Token: 0x04001CE1 RID: 7393
		internal string[] saAbbrevMonthNames;

		// Token: 0x04001CE2 RID: 7394
		internal string[] saMonthGenitiveNames;

		// Token: 0x04001CE3 RID: 7395
		internal string[] saAbbrevMonthGenitiveNames;

		// Token: 0x04001CE4 RID: 7396
		internal string[] saLeapYearMonthNames;

		// Token: 0x04001CE5 RID: 7397
		internal int iTwoDigitYearMax = 2029;

		// Token: 0x04001CE6 RID: 7398
		internal int iCurrentEra;

		// Token: 0x04001CE7 RID: 7399
		internal bool bUseUserOverrides;

		// Token: 0x04001CE8 RID: 7400
		internal static CalendarData Invariant;

		// Token: 0x04001CE9 RID: 7401
		private static string[] HEBREW_MONTH_NAMES = new string[]
		{
			"תשרי", "חשון", "כסלו", "טבת", "שבט", "אדר", "אדר ב", "ניסן", "אייר", "סיון",
			"תמוז", "אב", "אלול"
		};

		// Token: 0x04001CEA RID: 7402
		private static string[] HEBREW_LEAP_MONTH_NAMES = new string[]
		{
			"תשרי", "חשון", "כסלו", "טבת", "שבט", "אדר א", "אדר ב", "ניסן", "אייר", "סיון",
			"תמוז", "אב", "אלול"
		};
	}
}
