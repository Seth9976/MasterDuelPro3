using System;
using System.Runtime.InteropServices;

namespace System.Globalization
{
	/// <summary>Represents the Japanese calendar.</summary>
	// Token: 0x020006B8 RID: 1720
	[ComVisible(true)]
	[Serializable]
	public class JapaneseCalendar : Calendar
	{
		/// <summary>Gets the earliest date and time supported by the current <see cref="T:System.Globalization.JapaneseCalendar" /> object.</summary>
		/// <returns>The earliest date and time supported by the <see cref="T:System.Globalization.JapaneseCalendar" /> type, which is equivalent to the first moment of September 8, 1868 C.E. in the Gregorian calendar. </returns>
		// Token: 0x1700080A RID: 2058
		// (get) Token: 0x060035E9 RID: 13801 RVA: 0x000D07AE File Offset: 0x000CE9AE
		[ComVisible(false)]
		public override DateTime MinSupportedDateTime
		{
			get
			{
				return JapaneseCalendar.calendarMinValue;
			}
		}

		/// <summary>Gets the latest date and time supported by the current <see cref="T:System.Globalization.JapaneseCalendar" /> object.</summary>
		/// <returns>The latest date and time supported by the <see cref="T:System.Globalization.JapaneseCalendar" /> type, which is equivalent to the last moment of December 31, 9999 C.E. in the Gregorian calendar.</returns>
		// Token: 0x1700080B RID: 2059
		// (get) Token: 0x060035EA RID: 13802 RVA: 0x000CE6D5 File Offset: 0x000CC8D5
		[ComVisible(false)]
		public override DateTime MaxSupportedDateTime
		{
			get
			{
				return DateTime.MaxValue;
			}
		}

		// Token: 0x060035EB RID: 13803 RVA: 0x000D07B8 File Offset: 0x000CE9B8
		internal static EraInfo[] GetEraInfo()
		{
			if (JapaneseCalendar.japaneseEraInfo == null)
			{
				JapaneseCalendar.japaneseEraInfo = JapaneseCalendar.GetErasFromRegistry();
				if (JapaneseCalendar.japaneseEraInfo == null)
				{
					JapaneseCalendar.japaneseEraInfo = new EraInfo[]
					{
						new EraInfo(5, 2019, 5, 1, 2018, 1, 7981, "令和", "令", "R"),
						new EraInfo(4, 1989, 1, 8, 1988, 1, 31, "平成", "平", "H"),
						new EraInfo(3, 1926, 12, 25, 1925, 1, 64, "昭和", "昭", "S"),
						new EraInfo(2, 1912, 7, 30, 1911, 1, 15, "大正", "大", "T"),
						new EraInfo(1, 1868, 1, 1, 1867, 1, 45, "明治", "明", "M")
					};
				}
			}
			return JapaneseCalendar.japaneseEraInfo;
		}

		// Token: 0x060035EC RID: 13804 RVA: 0x000082D2 File Offset: 0x000064D2
		private static EraInfo[] GetErasFromRegistry()
		{
			return null;
		}

		// Token: 0x060035ED RID: 13805 RVA: 0x000D08C6 File Offset: 0x000CEAC6
		internal static Calendar GetDefaultInstance()
		{
			if (JapaneseCalendar.s_defaultInstance == null)
			{
				JapaneseCalendar.s_defaultInstance = new JapaneseCalendar();
			}
			return JapaneseCalendar.s_defaultInstance;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Globalization.JapaneseCalendar" /> class.</summary>
		/// <exception cref="T:System.TypeInitializationException">Unable to initialize a <see cref="T:System.Globalization.JapaneseCalendar" /> object because of missing culture information.</exception>
		// Token: 0x060035EE RID: 13806 RVA: 0x000D08E4 File Offset: 0x000CEAE4
		public JapaneseCalendar()
		{
			try
			{
				new CultureInfo("ja-JP");
			}
			catch (ArgumentException ex)
			{
				throw new TypeInitializationException(base.GetType().FullName, ex);
			}
			this.helper = new GregorianCalendarHelper(this, JapaneseCalendar.GetEraInfo());
		}

		// Token: 0x1700080C RID: 2060
		// (get) Token: 0x060035EF RID: 13807 RVA: 0x000194AE File Offset: 0x000176AE
		internal override int ID
		{
			get
			{
				return 3;
			}
		}

		/// <summary>Returns the number of days in the specified month in the specified year in the specified era.</summary>
		/// <returns>The number of days in the specified month in the specified year in the specified era.</returns>
		/// <param name="year">An integer that represents the year. </param>
		/// <param name="month">An integer from 1 to 12 that represents the month. </param>
		/// <param name="era">An integer that represents the era. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is outside the range supported by the calendar.-or- <paramref name="month" /> is outside the range supported by the calendar.-or- <paramref name="era" /> is outside the range supported by the calendar. </exception>
		// Token: 0x060035F0 RID: 13808 RVA: 0x000D0938 File Offset: 0x000CEB38
		public override int GetDaysInMonth(int year, int month, int era)
		{
			return this.helper.GetDaysInMonth(year, month, era);
		}

		/// <summary>Returns the number of days in the specified year in the specified era.</summary>
		/// <returns>The number of days in the specified year in the specified era.</returns>
		/// <param name="year">An integer that represents the year. </param>
		/// <param name="era">An integer that represents the era. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is outside the range supported by the calendar.-or- <paramref name="era" /> is outside the range supported by the calendar. </exception>
		// Token: 0x060035F1 RID: 13809 RVA: 0x000D0948 File Offset: 0x000CEB48
		public override int GetDaysInYear(int year, int era)
		{
			return this.helper.GetDaysInYear(year, era);
		}

		/// <summary>Returns the day of the month in the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>An integer from 1 to 31 that represents the day of the month in the specified <see cref="T:System.DateTime" />.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. </param>
		// Token: 0x060035F2 RID: 13810 RVA: 0x000D0957 File Offset: 0x000CEB57
		public override int GetDayOfMonth(DateTime time)
		{
			return this.helper.GetDayOfMonth(time);
		}

		/// <summary>Returns the day of the week in the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>A <see cref="T:System.DayOfWeek" /> value that represents the day of the week in the specified <see cref="T:System.DateTime" />.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. </param>
		// Token: 0x060035F3 RID: 13811 RVA: 0x000D0965 File Offset: 0x000CEB65
		public override DayOfWeek GetDayOfWeek(DateTime time)
		{
			return this.helper.GetDayOfWeek(time);
		}

		/// <summary>Returns the number of months in the specified year in the specified era.</summary>
		/// <returns>The return value is always 12.</returns>
		/// <param name="year">An integer that represents the year. </param>
		/// <param name="era">An integer that represents the era. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is outside the range supported by the calendar.-or- <paramref name="era" /> is outside the range supported by the calendar. </exception>
		// Token: 0x060035F4 RID: 13812 RVA: 0x000D0973 File Offset: 0x000CEB73
		public override int GetMonthsInYear(int year, int era)
		{
			return this.helper.GetMonthsInYear(year, era);
		}

		/// <summary>Returns the era in the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>An integer that represents the era in the specified <see cref="T:System.DateTime" />.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The resulting <see cref="T:System.DateTime" /> is outside the supported range. </exception>
		// Token: 0x060035F5 RID: 13813 RVA: 0x000D0982 File Offset: 0x000CEB82
		public override int GetEra(DateTime time)
		{
			return this.helper.GetEra(time);
		}

		/// <summary>Returns the month in the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>An integer from 1 to 12 that represents the month in the specified <see cref="T:System.DateTime" />.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. </param>
		// Token: 0x060035F6 RID: 13814 RVA: 0x000D0990 File Offset: 0x000CEB90
		public override int GetMonth(DateTime time)
		{
			return this.helper.GetMonth(time);
		}

		/// <summary>Returns the year in the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>An integer that represents the year in the specified <see cref="T:System.DateTime" />.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. </param>
		// Token: 0x060035F7 RID: 13815 RVA: 0x000D099E File Offset: 0x000CEB9E
		public override int GetYear(DateTime time)
		{
			return this.helper.GetYear(time);
		}

		/// <summary>Determines whether the specified year in the specified era is a leap year.</summary>
		/// <returns>true, if the specified year is a leap year; otherwise, false.</returns>
		/// <param name="year">An integer that represents the year. </param>
		/// <param name="era">An integer that represents the era. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is outside the range supported by the calendar.-or- <paramref name="era" /> is outside the range supported by the calendar. </exception>
		// Token: 0x060035F8 RID: 13816 RVA: 0x000D09AC File Offset: 0x000CEBAC
		public override bool IsLeapYear(int year, int era)
		{
			return this.helper.IsLeapYear(year, era);
		}

		/// <summary>Returns a <see cref="T:System.DateTime" /> that is set to the specified date and time in the specified era.</summary>
		/// <returns>The <see cref="T:System.DateTime" /> that is set to the specified date and time in the current era.</returns>
		/// <param name="year">An integer that represents the year. </param>
		/// <param name="month">An integer from 1 to 12 that represents the month. </param>
		/// <param name="day">An integer from 1 to 31 that represents the day. </param>
		/// <param name="hour">An integer from 0 to 23 that represents the hour. </param>
		/// <param name="minute">An integer from 0 to 59 that represents the minute. </param>
		/// <param name="second">An integer from 0 to 59 that represents the second. </param>
		/// <param name="millisecond">An integer from 0 to 999 that represents the millisecond. </param>
		/// <param name="era">An integer that represents the era. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is outside the range supported by the calendar.-or- <paramref name="month" /> is outside the range supported by the calendar.-or- <paramref name="day" /> is outside the range supported by the calendar.-or- <paramref name="hour" /> is less than zero or greater than 23.-or- <paramref name="minute" /> is less than zero or greater than 59.-or- <paramref name="second" /> is less than zero or greater than 59.-or- <paramref name="millisecond" /> is less than zero or greater than 999.-or- <paramref name="era" /> is outside the range supported by the calendar. </exception>
		// Token: 0x060035F9 RID: 13817 RVA: 0x000D09BC File Offset: 0x000CEBBC
		public override DateTime ToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, int era)
		{
			return this.helper.ToDateTime(year, month, day, hour, minute, second, millisecond, era);
		}

		/// <summary>Converts the specified year to a four-digit year by using the <see cref="P:System.Globalization.JapaneseCalendar.TwoDigitYearMax" /> property to determine the appropriate century.</summary>
		/// <returns>An integer that contains the four-digit representation of <paramref name="year" />.</returns>
		/// <param name="year">An integer (usually two digits) that represents the year to convert. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is outside the range supported by the calendar. </exception>
		// Token: 0x060035FA RID: 13818 RVA: 0x000D09E4 File Offset: 0x000CEBE4
		public override int ToFourDigitYear(int year)
		{
			if (year <= 0)
			{
				throw new ArgumentOutOfRangeException("year", Environment.GetResourceString("Positive number required."));
			}
			if (year > this.helper.MaxYear)
			{
				throw new ArgumentOutOfRangeException("year", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Valid values are between {0} and {1}, inclusive."), 1, this.helper.MaxYear));
			}
			return year;
		}

		/// <summary>Gets the list of eras in the <see cref="T:System.Globalization.JapaneseCalendar" />.</summary>
		/// <returns>An array of integers that represents the eras in the <see cref="T:System.Globalization.JapaneseCalendar" />.</returns>
		// Token: 0x1700080D RID: 2061
		// (get) Token: 0x060035FB RID: 13819 RVA: 0x000D0A4E File Offset: 0x000CEC4E
		public override int[] Eras
		{
			get
			{
				return this.helper.Eras;
			}
		}

		// Token: 0x060035FC RID: 13820 RVA: 0x000D0A5C File Offset: 0x000CEC5C
		internal static string[] EraNames()
		{
			EraInfo[] eraInfo = JapaneseCalendar.GetEraInfo();
			string[] array = new string[eraInfo.Length];
			for (int i = 0; i < eraInfo.Length; i++)
			{
				array[i] = eraInfo[eraInfo.Length - i - 1].eraName;
			}
			return array;
		}

		// Token: 0x060035FD RID: 13821 RVA: 0x000D0A98 File Offset: 0x000CEC98
		internal static string[] EnglishEraNames()
		{
			EraInfo[] eraInfo = JapaneseCalendar.GetEraInfo();
			string[] array = new string[eraInfo.Length];
			for (int i = 0; i < eraInfo.Length; i++)
			{
				array[i] = eraInfo[eraInfo.Length - i - 1].englishEraName;
			}
			return array;
		}

		// Token: 0x060035FE RID: 13822 RVA: 0x000D0AD4 File Offset: 0x000CECD4
		internal override bool IsValidYear(int year, int era)
		{
			return this.helper.IsValidYear(year, era);
		}

		/// <summary>Gets or sets the last year of a 100-year range that can be represented by a 2-digit year.</summary>
		/// <returns>The last year of a 100-year range that can be represented by a 2-digit year.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value specified in a set operation is less than 99. -or- The value specified in a set operation is greater than 8011 (or MaxSupportedDateTime.Year).</exception>
		/// <exception cref="T:System.InvalidOperationException">In a set operation, the current instance is read-only.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700080E RID: 2062
		// (get) Token: 0x060035FF RID: 13823 RVA: 0x000D0AE3 File Offset: 0x000CECE3
		// (set) Token: 0x06003600 RID: 13824 RVA: 0x000D0B08 File Offset: 0x000CED08
		public override int TwoDigitYearMax
		{
			get
			{
				if (this.twoDigitYearMax == -1)
				{
					this.twoDigitYearMax = Calendar.GetSystemTwoDigitYearSetting(this.ID, 99);
				}
				return this.twoDigitYearMax;
			}
			set
			{
				base.VerifyWritable();
				if (value < 99 || value > this.helper.MaxYear)
				{
					throw new ArgumentOutOfRangeException("year", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Valid values are between {0} and {1}, inclusive."), 99, this.helper.MaxYear));
				}
				this.twoDigitYearMax = value;
			}
		}

		// Token: 0x04001D0B RID: 7435
		internal static readonly DateTime calendarMinValue = new DateTime(1868, 9, 8);

		// Token: 0x04001D0C RID: 7436
		internal static volatile EraInfo[] japaneseEraInfo;

		// Token: 0x04001D0D RID: 7437
		internal static volatile Calendar s_defaultInstance;

		// Token: 0x04001D0E RID: 7438
		internal GregorianCalendarHelper helper;
	}
}
