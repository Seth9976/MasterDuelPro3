using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Globalization
{
	/// <summary>Represents time in divisions, such as weeks, months, and years.</summary>
	// Token: 0x020006B1 RID: 1713
	[ComVisible(true)]
	[Serializable]
	public abstract class Calendar : ICloneable
	{
		/// <summary>Gets the earliest date and time supported by this <see cref="T:System.Globalization.Calendar" /> object.</summary>
		/// <returns>The earliest date and time supported by this calendar. The default is <see cref="F:System.DateTime.MinValue" />.</returns>
		// Token: 0x170007F5 RID: 2037
		// (get) Token: 0x0600356E RID: 13678 RVA: 0x000CE6CE File Offset: 0x000CC8CE
		[ComVisible(false)]
		public virtual DateTime MinSupportedDateTime
		{
			get
			{
				return DateTime.MinValue;
			}
		}

		/// <summary>Gets the latest date and time supported by this <see cref="T:System.Globalization.Calendar" /> object.</summary>
		/// <returns>The latest date and time supported by this calendar. The default is <see cref="F:System.DateTime.MaxValue" />.</returns>
		// Token: 0x170007F6 RID: 2038
		// (get) Token: 0x0600356F RID: 13679 RVA: 0x000CE6D5 File Offset: 0x000CC8D5
		[ComVisible(false)]
		public virtual DateTime MaxSupportedDateTime
		{
			get
			{
				return DateTime.MaxValue;
			}
		}

		// Token: 0x170007F7 RID: 2039
		// (get) Token: 0x06003571 RID: 13681 RVA: 0x000CE6F2 File Offset: 0x000CC8F2
		internal virtual int ID
		{
			get
			{
				return -1;
			}
		}

		// Token: 0x170007F8 RID: 2040
		// (get) Token: 0x06003572 RID: 13682 RVA: 0x000CE6F5 File Offset: 0x000CC8F5
		internal virtual int BaseCalendarID
		{
			get
			{
				return this.ID;
			}
		}

		/// <summary>Gets a value indicating whether this <see cref="T:System.Globalization.Calendar" /> object is read-only.</summary>
		/// <returns>true if this <see cref="T:System.Globalization.Calendar" /> object is read-only; otherwise, false.</returns>
		// Token: 0x170007F9 RID: 2041
		// (get) Token: 0x06003573 RID: 13683 RVA: 0x000CE6FD File Offset: 0x000CC8FD
		[ComVisible(false)]
		public bool IsReadOnly
		{
			get
			{
				return this.m_isReadOnly;
			}
		}

		/// <summary>Creates a new object that is a copy of the current <see cref="T:System.Globalization.Calendar" /> object.</summary>
		/// <returns>A new instance of <see cref="T:System.Object" /> that is the memberwise clone of the current <see cref="T:System.Globalization.Calendar" /> object.</returns>
		// Token: 0x06003574 RID: 13684 RVA: 0x000CE705 File Offset: 0x000CC905
		[ComVisible(false)]
		public virtual object Clone()
		{
			object obj = base.MemberwiseClone();
			((Calendar)obj).SetReadOnlyState(false);
			return obj;
		}

		/// <summary>Returns a read-only version of the specified <see cref="T:System.Globalization.Calendar" /> object.</summary>
		/// <returns>The <see cref="T:System.Globalization.Calendar" /> object specified by the <paramref name="calendar" /> parameter, if <paramref name="calendar" /> is read-only.-or-A read-only memberwise clone of the <see cref="T:System.Globalization.Calendar" /> object specified by <paramref name="calendar" />, if <paramref name="calendar" /> is not read-only.</returns>
		/// <param name="calendar">A <see cref="T:System.Globalization.Calendar" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="calendar" /> is null.</exception>
		// Token: 0x06003575 RID: 13685 RVA: 0x000CE719 File Offset: 0x000CC919
		[ComVisible(false)]
		public static Calendar ReadOnly(Calendar calendar)
		{
			if (calendar == null)
			{
				throw new ArgumentNullException("calendar");
			}
			if (calendar.IsReadOnly)
			{
				return calendar;
			}
			Calendar calendar2 = (Calendar)calendar.MemberwiseClone();
			calendar2.SetReadOnlyState(true);
			return calendar2;
		}

		// Token: 0x06003576 RID: 13686 RVA: 0x000CE745 File Offset: 0x000CC945
		internal void VerifyWritable()
		{
			if (this.m_isReadOnly)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Instance is read-only."));
			}
		}

		// Token: 0x06003577 RID: 13687 RVA: 0x000CE75F File Offset: 0x000CC95F
		internal void SetReadOnlyState(bool readOnly)
		{
			this.m_isReadOnly = readOnly;
		}

		// Token: 0x170007FA RID: 2042
		// (get) Token: 0x06003578 RID: 13688 RVA: 0x000CE768 File Offset: 0x000CC968
		internal virtual int CurrentEraValue
		{
			get
			{
				if (this.m_currentEraValue == -1)
				{
					this.m_currentEraValue = CalendarData.GetCalendarData(this.BaseCalendarID).iCurrentEra;
				}
				return this.m_currentEraValue;
			}
		}

		/// <summary>When overridden in a derived class, returns the day of the month in the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>A positive integer that represents the day of the month in the <paramref name="time" /> parameter.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. </param>
		// Token: 0x06003579 RID: 13689
		public abstract int GetDayOfMonth(DateTime time);

		/// <summary>When overridden in a derived class, returns the day of the week in the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>A <see cref="T:System.DayOfWeek" /> value that represents the day of the week in the <paramref name="time" /> parameter.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. </param>
		// Token: 0x0600357A RID: 13690
		public abstract DayOfWeek GetDayOfWeek(DateTime time);

		/// <summary>When overridden in a derived class, returns the number of days in the specified month, year, and era.</summary>
		/// <returns>The number of days in the specified month in the specified year in the specified era.</returns>
		/// <param name="year">An integer that represents the year. </param>
		/// <param name="month">A positive integer that represents the month. </param>
		/// <param name="era">An integer that represents the era. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is outside the range supported by the calendar.-or- <paramref name="month" /> is outside the range supported by the calendar.-or- <paramref name="era" /> is outside the range supported by the calendar. </exception>
		// Token: 0x0600357B RID: 13691
		public abstract int GetDaysInMonth(int year, int month, int era);

		/// <summary>When overridden in a derived class, returns the number of days in the specified year and era.</summary>
		/// <returns>The number of days in the specified year in the specified era.</returns>
		/// <param name="year">An integer that represents the year. </param>
		/// <param name="era">An integer that represents the era. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is outside the range supported by the calendar.-or- <paramref name="era" /> is outside the range supported by the calendar. </exception>
		// Token: 0x0600357C RID: 13692
		public abstract int GetDaysInYear(int year, int era);

		/// <summary>When overridden in a derived class, returns the era in the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>An integer that represents the era in <paramref name="time" />.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. </param>
		// Token: 0x0600357D RID: 13693
		public abstract int GetEra(DateTime time);

		/// <summary>When overridden in a derived class, gets the list of eras in the current calendar.</summary>
		/// <returns>An array of integers that represents the eras in the current calendar.</returns>
		// Token: 0x170007FB RID: 2043
		// (get) Token: 0x0600357E RID: 13694
		public abstract int[] Eras { get; }

		/// <summary>When overridden in a derived class, returns the month in the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>A positive integer that represents the month in <paramref name="time" />.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. </param>
		// Token: 0x0600357F RID: 13695
		public abstract int GetMonth(DateTime time);

		/// <summary>When overridden in a derived class, returns the number of months in the specified year in the specified era.</summary>
		/// <returns>The number of months in the specified year in the specified era.</returns>
		/// <param name="year">An integer that represents the year. </param>
		/// <param name="era">An integer that represents the era. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is outside the range supported by the calendar.-or- <paramref name="era" /> is outside the range supported by the calendar. </exception>
		// Token: 0x06003580 RID: 13696
		public abstract int GetMonthsInYear(int year, int era);

		/// <summary>When overridden in a derived class, returns the year in the specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>An integer that represents the year in <paramref name="time" />.</returns>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read. </param>
		// Token: 0x06003581 RID: 13697
		public abstract int GetYear(DateTime time);

		/// <summary>Determines whether the specified year in the current era is a leap year.</summary>
		/// <returns>true if the specified year is a leap year; otherwise, false.</returns>
		/// <param name="year">An integer that represents the year. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is outside the range supported by the calendar. </exception>
		// Token: 0x06003582 RID: 13698 RVA: 0x000CE78F File Offset: 0x000CC98F
		public virtual bool IsLeapYear(int year)
		{
			return this.IsLeapYear(year, 0);
		}

		/// <summary>When overridden in a derived class, determines whether the specified year in the specified era is a leap year.</summary>
		/// <returns>true if the specified year is a leap year; otherwise, false.</returns>
		/// <param name="year">An integer that represents the year. </param>
		/// <param name="era">An integer that represents the era. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is outside the range supported by the calendar.-or- <paramref name="era" /> is outside the range supported by the calendar. </exception>
		// Token: 0x06003583 RID: 13699
		public abstract bool IsLeapYear(int year, int era);

		/// <summary>Returns a <see cref="T:System.DateTime" /> that is set to the specified date and time in the current era.</summary>
		/// <returns>The <see cref="T:System.DateTime" /> that is set to the specified date and time in the current era.</returns>
		/// <param name="year">An integer that represents the year. </param>
		/// <param name="month">A positive integer that represents the month. </param>
		/// <param name="day">A positive integer that represents the day. </param>
		/// <param name="hour">An integer from 0 to 23 that represents the hour. </param>
		/// <param name="minute">An integer from 0 to 59 that represents the minute. </param>
		/// <param name="second">An integer from 0 to 59 that represents the second. </param>
		/// <param name="millisecond">An integer from 0 to 999 that represents the millisecond. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is outside the range supported by the calendar.-or- <paramref name="month" /> is outside the range supported by the calendar.-or- <paramref name="day" /> is outside the range supported by the calendar.-or- <paramref name="hour" /> is less than zero or greater than 23.-or- <paramref name="minute" /> is less than zero or greater than 59.-or- <paramref name="second" /> is less than zero or greater than 59.-or- <paramref name="millisecond" /> is less than zero or greater than 999. </exception>
		// Token: 0x06003584 RID: 13700 RVA: 0x000CE79C File Offset: 0x000CC99C
		public virtual DateTime ToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond)
		{
			return this.ToDateTime(year, month, day, hour, minute, second, millisecond, 0);
		}

		/// <summary>When overridden in a derived class, returns a <see cref="T:System.DateTime" /> that is set to the specified date and time in the specified era.</summary>
		/// <returns>The <see cref="T:System.DateTime" /> that is set to the specified date and time in the current era.</returns>
		/// <param name="year">An integer that represents the year. </param>
		/// <param name="month">A positive integer that represents the month. </param>
		/// <param name="day">A positive integer that represents the day. </param>
		/// <param name="hour">An integer from 0 to 23 that represents the hour. </param>
		/// <param name="minute">An integer from 0 to 59 that represents the minute. </param>
		/// <param name="second">An integer from 0 to 59 that represents the second. </param>
		/// <param name="millisecond">An integer from 0 to 999 that represents the millisecond. </param>
		/// <param name="era">An integer that represents the era. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is outside the range supported by the calendar.-or- <paramref name="month" /> is outside the range supported by the calendar.-or- <paramref name="day" /> is outside the range supported by the calendar.-or- <paramref name="hour" /> is less than zero or greater than 23.-or- <paramref name="minute" /> is less than zero or greater than 59.-or- <paramref name="second" /> is less than zero or greater than 59.-or- <paramref name="millisecond" /> is less than zero or greater than 999.-or- <paramref name="era" /> is outside the range supported by the calendar. </exception>
		// Token: 0x06003585 RID: 13701
		public abstract DateTime ToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, int era);

		// Token: 0x06003586 RID: 13702 RVA: 0x000CE7BC File Offset: 0x000CC9BC
		internal virtual bool TryToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, int era, out DateTime result)
		{
			result = DateTime.MinValue;
			bool flag;
			try
			{
				result = this.ToDateTime(year, month, day, hour, minute, second, millisecond, era);
				flag = true;
			}
			catch (ArgumentException)
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06003587 RID: 13703 RVA: 0x000CE80C File Offset: 0x000CCA0C
		internal virtual bool IsValidYear(int year, int era)
		{
			return year >= this.GetYear(this.MinSupportedDateTime) && year <= this.GetYear(this.MaxSupportedDateTime);
		}

		// Token: 0x06003588 RID: 13704 RVA: 0x000CE831 File Offset: 0x000CCA31
		internal virtual bool IsValidMonth(int year, int month, int era)
		{
			return this.IsValidYear(year, era) && month >= 1 && month <= this.GetMonthsInYear(year, era);
		}

		// Token: 0x06003589 RID: 13705 RVA: 0x000CE851 File Offset: 0x000CCA51
		internal virtual bool IsValidDay(int year, int month, int day, int era)
		{
			return this.IsValidMonth(year, month, era) && day >= 1 && day <= this.GetDaysInMonth(year, month, era);
		}

		/// <summary>Gets or sets the last year of a 100-year range that can be represented by a 2-digit year.</summary>
		/// <returns>The last year of a 100-year range that can be represented by a 2-digit year.</returns>
		/// <exception cref="T:System.InvalidOperationException">The current <see cref="T:System.Globalization.Calendar" /> object is read-only.</exception>
		// Token: 0x170007FC RID: 2044
		// (get) Token: 0x0600358A RID: 13706 RVA: 0x000CE875 File Offset: 0x000CCA75
		// (set) Token: 0x0600358B RID: 13707 RVA: 0x000CE87D File Offset: 0x000CCA7D
		public virtual int TwoDigitYearMax
		{
			get
			{
				return this.twoDigitYearMax;
			}
			set
			{
				this.VerifyWritable();
				this.twoDigitYearMax = value;
			}
		}

		/// <summary>Converts the specified year to a four-digit year by using the <see cref="P:System.Globalization.Calendar.TwoDigitYearMax" /> property to determine the appropriate century.</summary>
		/// <returns>An integer that contains the four-digit representation of <paramref name="year" />.</returns>
		/// <param name="year">A two-digit or four-digit integer that represents the year to convert. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is outside the range supported by the calendar. </exception>
		// Token: 0x0600358C RID: 13708 RVA: 0x000CE88C File Offset: 0x000CCA8C
		public virtual int ToFourDigitYear(int year)
		{
			if (year < 0)
			{
				throw new ArgumentOutOfRangeException("year", Environment.GetResourceString("Non-negative number required."));
			}
			if (year < 100)
			{
				return (this.TwoDigitYearMax / 100 - ((year > this.TwoDigitYearMax % 100) ? 1 : 0)) * 100 + year;
			}
			return year;
		}

		// Token: 0x0600358D RID: 13709 RVA: 0x000CE8D8 File Offset: 0x000CCAD8
		internal static long TimeToTicks(int hour, int minute, int second, int millisecond)
		{
			if (hour < 0 || hour >= 24 || minute < 0 || minute >= 60 || second < 0 || second >= 60)
			{
				throw new ArgumentOutOfRangeException(null, Environment.GetResourceString("Hour, Minute, and Second parameters describe an un-representable DateTime."));
			}
			if (millisecond < 0 || millisecond >= 1000)
			{
				throw new ArgumentOutOfRangeException("millisecond", string.Format(CultureInfo.InvariantCulture, Environment.GetResourceString("Valid values are between {0} and {1}, inclusive."), 0, 999));
			}
			return TimeSpan.TimeToTicks(hour, minute, second) + (long)millisecond * 10000L;
		}

		// Token: 0x0600358E RID: 13710 RVA: 0x000CE960 File Offset: 0x000CCB60
		internal static int GetSystemTwoDigitYearSetting(int CalID, int defaultYearValue)
		{
			int num = CalendarData.nativeGetTwoDigitYearMax(CalID);
			if (num < 0)
			{
				num = defaultYearValue;
			}
			return num;
		}

		// Token: 0x04001CD1 RID: 7377
		internal int m_currentEraValue = -1;

		// Token: 0x04001CD2 RID: 7378
		[OptionalField(VersionAdded = 2)]
		private bool m_isReadOnly;

		// Token: 0x04001CD3 RID: 7379
		internal int twoDigitYearMax = -1;
	}
}
