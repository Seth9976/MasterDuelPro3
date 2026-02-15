using System;
using System.Runtime.InteropServices;

namespace System.Globalization
{
	/// <summary>the Taiwan calendar.</summary>
	// Token: 0x020006BB RID: 1723
	[ComVisible(true)]
	[Serializable]
	public class TaiwanCalendar : Calendar
	{
		// Token: 0x06003637 RID: 13879 RVA: 0x000D127B File Offset: 0x000CF47B
		internal static Calendar GetDefaultInstance()
		{
			if (TaiwanCalendar.s_defaultInstance == null)
			{
				TaiwanCalendar.s_defaultInstance = new TaiwanCalendar();
			}
			return TaiwanCalendar.s_defaultInstance;
		}

		/// <summary>Gets the earliest date and time supported by the <see cref="T:System.Globalization.TaiwanCalendar" /> class.</summary>
		/// <returns>The earliest date and time supported by the <see cref="T:System.Globalization.TaiwanCalendar" /> class, which is equivalent to the first moment of January 1, 1912 C.E. in the Gregorian calendar.</returns>
		// Token: 0x1700082E RID: 2094
		// (get) Token: 0x06003638 RID: 13880 RVA: 0x000D1299 File Offset: 0x000CF499
		[ComVisible(false)]
		public override DateTime MinSupportedDateTime
		{
			get
			{
				return TaiwanCalendar.calendarMinValue;
			}
		}

		/// <summary>Gets the latest date and time supported by the <see cref="T:System.Globalization.TaiwanCalendar" /> class.</summary>
		/// <returns>The latest date and time supported by the <see cref="T:System.Globalization.TaiwanCalendar" /> class, which is equivalent to the last moment of December 31, 9999 C.E. in the Gregorian calendar.</returns>
		// Token: 0x1700082F RID: 2095
		// (get) Token: 0x06003639 RID: 13881 RVA: 0x000CE6D5 File Offset: 0x000CC8D5
		[ComVisible(false)]
		public override DateTime MaxSupportedDateTime
		{
			get
			{
				return DateTime.MaxValue;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Globalization.TaiwanCalendar" /> class.</summary>
		/// <exception cref="T:System.TypeInitializationException">Unable to initialize a <see cref="T:System.Globalization.TaiwanCalendar" /> object because of missing culture information.</exception>
		// Token: 0x0600363A RID: 13882 RVA: 0x000D12A0 File Offset: 0x000CF4A0
		public TaiwanCalendar()
		{
			try
			{
				new CultureInfo("zh-TW");
			}
			catch (ArgumentException ex)
			{
				throw new TypeInitializationException(base.GetType().FullName, ex);
			}
			this.helper = new GregorianCalendarHelper(this, TaiwanCalendar.taiwanEraInfo);
		}

		// Token: 0x17000830 RID: 2096
		// (get) Token: 0x0600363B RID: 13883 RVA: 0x00019A7F File Offset: 0x00017C7F
		internal override int ID
		{
			get
			{
				return 4;
			}
		}

		/// <summary>Returns the number of days in the specified month in the specified year in the specified era.</summary>
		/// <returns>The number of days in the specified month in the specified year in the specified era.</returns>
		/// <param name="year">An integer that represents the year. </param>
		/// <param name="month">An integer from 1 to 12 that represents the month. </param>
		/// <param name="era">An integer that represents the era. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is outside the range supported by the calendar.-or- <paramref name="month" /> is outside the range supported by the calendar.-or- <paramref name="era" /> is outside the range supported by the calendar. </exception>
		// Token: 0x0600363C RID: 13884 RVA: 0x000D12F4 File Offset: 0x000CF4F4
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
		// Token: 0x0600363D RID: 13885 RVA: 0x000D1304 File Offset: 0x000CF504
		public override int GetDaysInYear(int year, int era)
		{
			return this.helper.GetDaysInYear(year, era);
		}

		/// <summary>Returns the day of the month in the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>An integer from 1 to 31 that represents the day of the month in the specified <see cref="T:System.DateTime" />.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. </param>
		// Token: 0x0600363E RID: 13886 RVA: 0x000D1313 File Offset: 0x000CF513
		public override int GetDayOfMonth(DateTime time)
		{
			return this.helper.GetDayOfMonth(time);
		}

		/// <summary>Returns the day of the week in the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>A <see cref="T:System.DayOfWeek" /> value that represents the day of the week in the specified <see cref="T:System.DateTime" />.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. </param>
		// Token: 0x0600363F RID: 13887 RVA: 0x000D1321 File Offset: 0x000CF521
		public override DayOfWeek GetDayOfWeek(DateTime time)
		{
			return this.helper.GetDayOfWeek(time);
		}

		/// <summary>Returns the number of months in the specified year in the specified era.</summary>
		/// <returns>The number of months in the specified year in the specified era.</returns>
		/// <param name="year">An integer that represents the year. </param>
		/// <param name="era">An integer that represents the era. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is outside the range supported by the calendar.-or- <paramref name="era" /> is outside the range supported by the calendar. </exception>
		// Token: 0x06003640 RID: 13888 RVA: 0x000D132F File Offset: 0x000CF52F
		public override int GetMonthsInYear(int year, int era)
		{
			return this.helper.GetMonthsInYear(year, era);
		}

		/// <summary>Returns the era in the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>An integer that represents the era in the specified <see cref="T:System.DateTime" />.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. </param>
		// Token: 0x06003641 RID: 13889 RVA: 0x000D133E File Offset: 0x000CF53E
		public override int GetEra(DateTime time)
		{
			return this.helper.GetEra(time);
		}

		/// <summary>Returns the month in the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>An integer from 1 to 12 that represents the month in the specified <see cref="T:System.DateTime" />.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. </param>
		// Token: 0x06003642 RID: 13890 RVA: 0x000D134C File Offset: 0x000CF54C
		public override int GetMonth(DateTime time)
		{
			return this.helper.GetMonth(time);
		}

		/// <summary>Returns the year in the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>An integer that represents the year in the specified <see cref="T:System.DateTime" />.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. </param>
		// Token: 0x06003643 RID: 13891 RVA: 0x000D135A File Offset: 0x000CF55A
		public override int GetYear(DateTime time)
		{
			return this.helper.GetYear(time);
		}

		/// <summary>Determines whether the specified year in the specified era is a leap year.</summary>
		/// <returns>true if the specified year is a leap year; otherwise, false.</returns>
		/// <param name="year">An integer that represents the year. </param>
		/// <param name="era">An integer that represents the era. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is outside the range supported by the calendar.-or- <paramref name="era" /> is outside the range supported by the calendar. </exception>
		// Token: 0x06003644 RID: 13892 RVA: 0x000D1368 File Offset: 0x000CF568
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
		// Token: 0x06003645 RID: 13893 RVA: 0x000D1378 File Offset: 0x000CF578
		public override DateTime ToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, int era)
		{
			return this.helper.ToDateTime(year, month, day, hour, minute, second, millisecond, era);
		}

		/// <summary>Gets the list of eras in the <see cref="T:System.Globalization.TaiwanCalendar" />.</summary>
		/// <returns>An array that consists of a single element for which the value is always the current era.</returns>
		// Token: 0x17000831 RID: 2097
		// (get) Token: 0x06003646 RID: 13894 RVA: 0x000D139D File Offset: 0x000CF59D
		public override int[] Eras
		{
			get
			{
				return this.helper.Eras;
			}
		}

		/// <summary>Gets or sets the last year of a 100-year range that can be represented by a 2-digit year.</summary>
		/// <returns>The last year of a 100-year range that can be represented by a 2-digit year.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value specified in a set operation is less than 99. -or- The value specified in a set operation is greater than MaxSupportedDateTime.Year.</exception>
		/// <exception cref="T:System.InvalidOperationException">In a set operation, the current instance is read-only.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000832 RID: 2098
		// (get) Token: 0x06003647 RID: 13895 RVA: 0x000D0AE3 File Offset: 0x000CECE3
		// (set) Token: 0x06003648 RID: 13896 RVA: 0x000D13AC File Offset: 0x000CF5AC
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

		/// <summary>Converts the specified year to a four-digit year by using the <see cref="P:System.Globalization.TaiwanCalendar.TwoDigitYearMax" /> property to determine the appropriate century.</summary>
		/// <returns>An integer that contains the four-digit representation of <paramref name="year" />.</returns>
		/// <param name="year">A two-digit or four-digit integer that represents the year to convert. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is outside the range supported by the calendar. </exception>
		// Token: 0x06003649 RID: 13897 RVA: 0x000D1410 File Offset: 0x000CF610
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

		// Token: 0x04001D35 RID: 7477
		internal static EraInfo[] taiwanEraInfo = new EraInfo[]
		{
			new EraInfo(1, 1912, 1, 1, 1911, 1, 8088)
		};

		// Token: 0x04001D36 RID: 7478
		internal static volatile Calendar s_defaultInstance;

		// Token: 0x04001D37 RID: 7479
		internal GregorianCalendarHelper helper;

		// Token: 0x04001D38 RID: 7480
		internal static readonly DateTime calendarMinValue = new DateTime(1912, 1, 1);
	}
}
