using System;
using System.Runtime.InteropServices;

namespace System.Globalization
{
	/// <summary>Represents the Thai Buddhist calendar.</summary>
	// Token: 0x020006BD RID: 1725
	[ComVisible(true)]
	[Serializable]
	public class ThaiBuddhistCalendar : Calendar
	{
		/// <summary>Gets the earliest date and time supported by the <see cref="T:System.Globalization.ThaiBuddhistCalendar" /> class.</summary>
		/// <returns>The earliest date and time supported by the <see cref="T:System.Globalization.ThaiBuddhistCalendar" /> class, which is equivalent to the first moment of January 1, 0001 C.E. in the Gregorian calendar.</returns>
		// Token: 0x1700083A RID: 2106
		// (get) Token: 0x06003671 RID: 13937 RVA: 0x000CE6CE File Offset: 0x000CC8CE
		[ComVisible(false)]
		public override DateTime MinSupportedDateTime
		{
			get
			{
				return DateTime.MinValue;
			}
		}

		/// <summary>Gets the latest date and time supported by the <see cref="T:System.Globalization.ThaiBuddhistCalendar" /> class.</summary>
		/// <returns>The latest date and time supported by the <see cref="T:System.Globalization.ThaiBuddhistCalendar" /> class, which is equivalent to the last moment of December 31, 9999 C.E. in the Gregorian calendar.</returns>
		// Token: 0x1700083B RID: 2107
		// (get) Token: 0x06003672 RID: 13938 RVA: 0x000CE6D5 File Offset: 0x000CC8D5
		[ComVisible(false)]
		public override DateTime MaxSupportedDateTime
		{
			get
			{
				return DateTime.MaxValue;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Globalization.ThaiBuddhistCalendar" /> class.</summary>
		// Token: 0x06003673 RID: 13939 RVA: 0x000D2122 File Offset: 0x000D0322
		public ThaiBuddhistCalendar()
		{
			this.helper = new GregorianCalendarHelper(this, ThaiBuddhistCalendar.thaiBuddhistEraInfo);
		}

		// Token: 0x1700083C RID: 2108
		// (get) Token: 0x06003674 RID: 13940 RVA: 0x00027AAE File Offset: 0x00025CAE
		internal override int ID
		{
			get
			{
				return 7;
			}
		}

		/// <summary>Returns the number of days in the specified month in the specified year in the specified era.</summary>
		/// <returns>The number of days in the specified month in the specified year in the specified era.</returns>
		/// <param name="year">An integer that represents the year. </param>
		/// <param name="month">An integer from 1 to 12 that represents the month. </param>
		/// <param name="era">An integer that represents the era. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is outside the range supported by the calendar.-or- <paramref name="month" /> is outside the range supported by the calendar.-or- <paramref name="era" /> is outside the range supported by the calendar. </exception>
		// Token: 0x06003675 RID: 13941 RVA: 0x000D213B File Offset: 0x000D033B
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
		// Token: 0x06003676 RID: 13942 RVA: 0x000D214B File Offset: 0x000D034B
		public override int GetDaysInYear(int year, int era)
		{
			return this.helper.GetDaysInYear(year, era);
		}

		/// <summary>Returns the day of the month in the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>An integer from 1 to 31 that represents the day of the month in the specified <see cref="T:System.DateTime" />.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. </param>
		// Token: 0x06003677 RID: 13943 RVA: 0x000D215A File Offset: 0x000D035A
		public override int GetDayOfMonth(DateTime time)
		{
			return this.helper.GetDayOfMonth(time);
		}

		/// <summary>Returns the day of the week in the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>A <see cref="T:System.DayOfWeek" /> value that represents the day of the week in the specified <see cref="T:System.DateTime" />.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. </param>
		// Token: 0x06003678 RID: 13944 RVA: 0x000D2168 File Offset: 0x000D0368
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
		// Token: 0x06003679 RID: 13945 RVA: 0x000D2176 File Offset: 0x000D0376
		public override int GetMonthsInYear(int year, int era)
		{
			return this.helper.GetMonthsInYear(year, era);
		}

		/// <summary>Returns the era in the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>An integer that represents the era in the specified <see cref="T:System.DateTime" />.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. </param>
		// Token: 0x0600367A RID: 13946 RVA: 0x000D2185 File Offset: 0x000D0385
		public override int GetEra(DateTime time)
		{
			return this.helper.GetEra(time);
		}

		/// <summary>Returns the month in the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>An integer from 1 to 12 that represents the month in the specified <see cref="T:System.DateTime" />.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. </param>
		// Token: 0x0600367B RID: 13947 RVA: 0x000D2193 File Offset: 0x000D0393
		public override int GetMonth(DateTime time)
		{
			return this.helper.GetMonth(time);
		}

		/// <summary>Returns the year in the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>An integer that represents the year in the specified <see cref="T:System.DateTime" />.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. </param>
		// Token: 0x0600367C RID: 13948 RVA: 0x000D21A1 File Offset: 0x000D03A1
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
		// Token: 0x0600367D RID: 13949 RVA: 0x000D21AF File Offset: 0x000D03AF
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
		// Token: 0x0600367E RID: 13950 RVA: 0x000D21C0 File Offset: 0x000D03C0
		public override DateTime ToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, int era)
		{
			return this.helper.ToDateTime(year, month, day, hour, minute, second, millisecond, era);
		}

		/// <summary>Gets the list of eras in the <see cref="T:System.Globalization.ThaiBuddhistCalendar" /> class.</summary>
		/// <returns>An array that consists of a single element having a value that is always the current era.</returns>
		// Token: 0x1700083D RID: 2109
		// (get) Token: 0x0600367F RID: 13951 RVA: 0x000D21E5 File Offset: 0x000D03E5
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
		// Token: 0x1700083E RID: 2110
		// (get) Token: 0x06003680 RID: 13952 RVA: 0x000D21F2 File Offset: 0x000D03F2
		// (set) Token: 0x06003681 RID: 13953 RVA: 0x000D221C File Offset: 0x000D041C
		public override int TwoDigitYearMax
		{
			get
			{
				if (this.twoDigitYearMax == -1)
				{
					this.twoDigitYearMax = Calendar.GetSystemTwoDigitYearSetting(this.ID, 2572);
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

		/// <summary>Converts the specified year to a four-digit year by using the <see cref="P:System.Globalization.ThaiBuddhistCalendar.TwoDigitYearMax" /> property to determine the appropriate century.</summary>
		/// <returns>An integer that contains the four-digit representation of <paramref name="year" />.</returns>
		/// <param name="year">A two-digit or four-digit integer that represents the year to convert. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is outside the range supported by the calendar. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="\" />
		/// </PermissionSet>
		// Token: 0x06003682 RID: 13954 RVA: 0x000D227F File Offset: 0x000D047F
		public override int ToFourDigitYear(int year)
		{
			if (year < 0)
			{
				throw new ArgumentOutOfRangeException("year", Environment.GetResourceString("Non-negative number required."));
			}
			return this.helper.ToFourDigitYear(year, this.TwoDigitYearMax);
		}

		// Token: 0x04001D43 RID: 7491
		internal static EraInfo[] thaiBuddhistEraInfo = new EraInfo[]
		{
			new EraInfo(1, 1, 1, 1, -543, 544, 10542)
		};

		// Token: 0x04001D44 RID: 7492
		internal GregorianCalendarHelper helper;
	}
}
