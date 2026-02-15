using System;
using System.Runtime.Serialization;

namespace System.Globalization
{
	// Token: 0x020006B5 RID: 1717
	[Serializable]
	internal class GregorianCalendarHelper
	{
		// Token: 0x17000802 RID: 2050
		// (get) Token: 0x060035B6 RID: 13750 RVA: 0x000CFAF5 File Offset: 0x000CDCF5
		internal int MaxYear
		{
			get
			{
				return this.m_maxYear;
			}
		}

		// Token: 0x060035B7 RID: 13751 RVA: 0x000CFB00 File Offset: 0x000CDD00
		internal GregorianCalendarHelper(Calendar cal, EraInfo[] eraInfo)
		{
			this.m_Cal = cal;
			this.m_EraInfo = eraInfo;
			this.m_minDate = this.m_Cal.MinSupportedDateTime;
			this.m_maxYear = this.m_EraInfo[0].maxEraYear;
			this.m_minYear = this.m_EraInfo[0].minEraYear;
		}

		// Token: 0x060035B8 RID: 13752 RVA: 0x000CFB64 File Offset: 0x000CDD64
		private int GetYearOffset(int year, int era, bool throwOnError)
		{
			if (year < 0)
			{
				if (throwOnError)
				{
					throw new ArgumentOutOfRangeException("year", "Non-negative number required.");
				}
				return -1;
			}
			else
			{
				if (era == 0)
				{
					era = this.m_Cal.CurrentEraValue;
				}
				int i = 0;
				while (i < this.m_EraInfo.Length)
				{
					if (era == this.m_EraInfo[i].era)
					{
						if (year >= this.m_EraInfo[i].minEraYear)
						{
							if (year <= this.m_EraInfo[i].maxEraYear)
							{
								return this.m_EraInfo[i].yearOffset;
							}
							if (!AppContextSwitches.EnforceJapaneseEraYearRanges)
							{
								int num = year - this.m_EraInfo[i].maxEraYear;
								for (int j = i - 1; j >= 0; j--)
								{
									if (num <= this.m_EraInfo[j].maxEraYear)
									{
										return this.m_EraInfo[i].yearOffset;
									}
									num -= this.m_EraInfo[j].maxEraYear;
								}
							}
						}
						if (throwOnError)
						{
							throw new ArgumentOutOfRangeException("year", string.Format(CultureInfo.CurrentCulture, "Valid values are between {0} and {1}, inclusive.", this.m_EraInfo[i].minEraYear, this.m_EraInfo[i].maxEraYear));
						}
						break;
					}
					else
					{
						i++;
					}
				}
				if (throwOnError)
				{
					throw new ArgumentOutOfRangeException("era", "Era value was not valid.");
				}
				return -1;
			}
		}

		// Token: 0x060035B9 RID: 13753 RVA: 0x000CFC9C File Offset: 0x000CDE9C
		internal int GetGregorianYear(int year, int era)
		{
			return this.GetYearOffset(year, era, true) + year;
		}

		// Token: 0x060035BA RID: 13754 RVA: 0x000CFCA9 File Offset: 0x000CDEA9
		internal bool IsValidYear(int year, int era)
		{
			return this.GetYearOffset(year, era, false) >= 0;
		}

		// Token: 0x060035BB RID: 13755 RVA: 0x000CFCBC File Offset: 0x000CDEBC
		internal virtual int GetDatePart(long ticks, int part)
		{
			this.CheckTicksRange(ticks);
			int i = (int)(ticks / 864000000000L);
			int num = i / 146097;
			i -= num * 146097;
			int num2 = i / 36524;
			if (num2 == 4)
			{
				num2 = 3;
			}
			i -= num2 * 36524;
			int num3 = i / 1461;
			i -= num3 * 1461;
			int num4 = i / 365;
			if (num4 == 4)
			{
				num4 = 3;
			}
			if (part == 0)
			{
				return num * 400 + num2 * 100 + num3 * 4 + num4 + 1;
			}
			i -= num4 * 365;
			if (part == 1)
			{
				return i + 1;
			}
			int[] array = ((num4 == 3 && (num3 != 24 || num2 == 3)) ? GregorianCalendarHelper.DaysToMonth366 : GregorianCalendarHelper.DaysToMonth365);
			int num5 = i >> 6;
			while (i >= array[num5])
			{
				num5++;
			}
			if (part == 2)
			{
				return num5;
			}
			return i - array[num5 - 1] + 1;
		}

		// Token: 0x060035BC RID: 13756 RVA: 0x000CFDA4 File Offset: 0x000CDFA4
		internal static long GetAbsoluteDate(int year, int month, int day)
		{
			if (year >= 1 && year <= 9999 && month >= 1 && month <= 12)
			{
				int[] array = ((year % 4 == 0 && (year % 100 != 0 || year % 400 == 0)) ? GregorianCalendarHelper.DaysToMonth366 : GregorianCalendarHelper.DaysToMonth365);
				if (day >= 1 && day <= array[month] - array[month - 1])
				{
					int num = year - 1;
					return (long)(num * 365 + num / 4 - num / 100 + num / 400 + array[month - 1] + day - 1);
				}
			}
			throw new ArgumentOutOfRangeException(null, Environment.GetResourceString("Year, Month, and Day parameters describe an un-representable DateTime."));
		}

		// Token: 0x060035BD RID: 13757 RVA: 0x000CFE2F File Offset: 0x000CE02F
		internal static long DateToTicks(int year, int month, int day)
		{
			return GregorianCalendarHelper.GetAbsoluteDate(year, month, day) * 864000000000L;
		}

		// Token: 0x060035BE RID: 13758 RVA: 0x000CFE44 File Offset: 0x000CE044
		internal static long TimeToTicks(int hour, int minute, int second, int millisecond)
		{
			if (hour < 0 || hour >= 24 || minute < 0 || minute >= 60 || second < 0 || second >= 60)
			{
				throw new ArgumentOutOfRangeException(null, Environment.GetResourceString("Hour, Minute, and Second parameters describe an un-representable DateTime."));
			}
			if (millisecond < 0 || millisecond >= 1000)
			{
				throw new ArgumentOutOfRangeException("millisecond", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Valid values are between {0} and {1}, inclusive."), 0, 999));
			}
			return TimeSpan.TimeToTicks(hour, minute, second) + (long)millisecond * 10000L;
		}

		// Token: 0x060035BF RID: 13759 RVA: 0x000CFECC File Offset: 0x000CE0CC
		internal void CheckTicksRange(long ticks)
		{
			if (ticks < this.m_Cal.MinSupportedDateTime.Ticks || ticks > this.m_Cal.MaxSupportedDateTime.Ticks)
			{
				throw new ArgumentOutOfRangeException("time", string.Format(CultureInfo.InvariantCulture, Environment.GetResourceString("Specified time is not supported in this calendar. It should be between {0} (Gregorian date) and {1} (Gregorian date), inclusive."), this.m_Cal.MinSupportedDateTime, this.m_Cal.MaxSupportedDateTime));
			}
		}

		// Token: 0x060035C0 RID: 13760 RVA: 0x000CFF44 File Offset: 0x000CE144
		public int GetDayOfMonth(DateTime time)
		{
			return this.GetDatePart(time.Ticks, 3);
		}

		// Token: 0x060035C1 RID: 13761 RVA: 0x000CFF54 File Offset: 0x000CE154
		public DayOfWeek GetDayOfWeek(DateTime time)
		{
			this.CheckTicksRange(time.Ticks);
			return (DayOfWeek)((time.Ticks / 864000000000L + 1L) % 7L);
		}

		// Token: 0x060035C2 RID: 13762 RVA: 0x000CFF7C File Offset: 0x000CE17C
		public int GetDaysInMonth(int year, int month, int era)
		{
			year = this.GetGregorianYear(year, era);
			if (month < 1 || month > 12)
			{
				throw new ArgumentOutOfRangeException("month", Environment.GetResourceString("Month must be between one and twelve."));
			}
			int[] array = ((year % 4 == 0 && (year % 100 != 0 || year % 400 == 0)) ? GregorianCalendarHelper.DaysToMonth366 : GregorianCalendarHelper.DaysToMonth365);
			return array[month] - array[month - 1];
		}

		// Token: 0x060035C3 RID: 13763 RVA: 0x000CFFDB File Offset: 0x000CE1DB
		public int GetDaysInYear(int year, int era)
		{
			year = this.GetGregorianYear(year, era);
			if (year % 4 != 0 || (year % 100 == 0 && year % 400 != 0))
			{
				return 365;
			}
			return 366;
		}

		// Token: 0x060035C4 RID: 13764 RVA: 0x000D0008 File Offset: 0x000CE208
		public int GetEra(DateTime time)
		{
			long ticks = time.Ticks;
			for (int i = 0; i < this.m_EraInfo.Length; i++)
			{
				if (ticks >= this.m_EraInfo[i].ticks)
				{
					return this.m_EraInfo[i].era;
				}
			}
			throw new ArgumentOutOfRangeException(Environment.GetResourceString("Time value was out of era range."));
		}

		// Token: 0x17000803 RID: 2051
		// (get) Token: 0x060035C5 RID: 13765 RVA: 0x000D0060 File Offset: 0x000CE260
		public int[] Eras
		{
			get
			{
				if (this.m_eras == null)
				{
					this.m_eras = new int[this.m_EraInfo.Length];
					for (int i = 0; i < this.m_EraInfo.Length; i++)
					{
						this.m_eras[i] = this.m_EraInfo[i].era;
					}
				}
				return (int[])this.m_eras.Clone();
			}
		}

		// Token: 0x060035C6 RID: 13766 RVA: 0x000D00C0 File Offset: 0x000CE2C0
		public int GetMonth(DateTime time)
		{
			return this.GetDatePart(time.Ticks, 2);
		}

		// Token: 0x060035C7 RID: 13767 RVA: 0x000D00D0 File Offset: 0x000CE2D0
		public int GetMonthsInYear(int year, int era)
		{
			year = this.GetGregorianYear(year, era);
			return 12;
		}

		// Token: 0x060035C8 RID: 13768 RVA: 0x000D00E0 File Offset: 0x000CE2E0
		public int GetYear(DateTime time)
		{
			long ticks = time.Ticks;
			int datePart = this.GetDatePart(ticks, 0);
			for (int i = 0; i < this.m_EraInfo.Length; i++)
			{
				if (ticks >= this.m_EraInfo[i].ticks)
				{
					return datePart - this.m_EraInfo[i].yearOffset;
				}
			}
			throw new ArgumentException(Environment.GetResourceString("No Era was supplied."));
		}

		// Token: 0x060035C9 RID: 13769 RVA: 0x000D0140 File Offset: 0x000CE340
		public bool IsLeapYear(int year, int era)
		{
			year = this.GetGregorianYear(year, era);
			return year % 4 == 0 && (year % 100 != 0 || year % 400 == 0);
		}

		// Token: 0x060035CA RID: 13770 RVA: 0x000D0168 File Offset: 0x000CE368
		public DateTime ToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, int era)
		{
			year = this.GetGregorianYear(year, era);
			long num = GregorianCalendarHelper.DateToTicks(year, month, day) + GregorianCalendarHelper.TimeToTicks(hour, minute, second, millisecond);
			this.CheckTicksRange(num);
			return new DateTime(num);
		}

		// Token: 0x060035CB RID: 13771 RVA: 0x000D01A4 File Offset: 0x000CE3A4
		public int ToFourDigitYear(int year, int twoDigitYearMax)
		{
			if (year < 0)
			{
				throw new ArgumentOutOfRangeException("year", Environment.GetResourceString("Positive number required."));
			}
			if (year < 100)
			{
				int num = year % 100;
				return (twoDigitYearMax / 100 - ((num > twoDigitYearMax % 100) ? 1 : 0)) * 100 + num;
			}
			if (year < this.m_minYear || year > this.m_maxYear)
			{
				throw new ArgumentOutOfRangeException("year", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Valid values are between {0} and {1}, inclusive."), this.m_minYear, this.m_maxYear));
			}
			return year;
		}

		// Token: 0x04001CF7 RID: 7415
		internal static readonly int[] DaysToMonth365 = new int[]
		{
			0, 31, 59, 90, 120, 151, 181, 212, 243, 273,
			304, 334, 365
		};

		// Token: 0x04001CF8 RID: 7416
		internal static readonly int[] DaysToMonth366 = new int[]
		{
			0, 31, 60, 91, 121, 152, 182, 213, 244, 274,
			305, 335, 366
		};

		// Token: 0x04001CF9 RID: 7417
		[OptionalField(VersionAdded = 1)]
		internal int m_maxYear = 9999;

		// Token: 0x04001CFA RID: 7418
		[OptionalField(VersionAdded = 1)]
		internal int m_minYear;

		// Token: 0x04001CFB RID: 7419
		internal Calendar m_Cal;

		// Token: 0x04001CFC RID: 7420
		[OptionalField(VersionAdded = 1)]
		internal EraInfo[] m_EraInfo;

		// Token: 0x04001CFD RID: 7421
		[OptionalField(VersionAdded = 1)]
		internal int[] m_eras;

		// Token: 0x04001CFE RID: 7422
		[OptionalField(VersionAdded = 1)]
		internal DateTime m_minDate;
	}
}
