using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>Represents a point in time, typically expressed as a date and time of day, relative to Coordinated Universal Time (UTC).</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000D6 RID: 214
	[Serializable]
	[StructLayout(LayoutKind.Auto)]
	public readonly struct DateTimeOffset : IComparable, IFormattable, IComparable<DateTimeOffset>, IEquatable<DateTimeOffset>, ISerializable, IDeserializationCallback, ISpanFormattable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.DateTimeOffset" /> structure using the specified number of ticks and offset.</summary>
		/// <param name="ticks">A date and time expressed as the number of 100-nanosecond intervals that have elapsed since 12:00:00 midnight on January 1, 0001.</param>
		/// <param name="offset">The time's offset from Coordinated Universal Time (UTC).</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="offset" /> is not specified in whole minutes.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <see cref="P:System.DateTimeOffset.UtcDateTime" /> property is earlier than <see cref="F:System.DateTimeOffset.MinValue" /> or later than <see cref="F:System.DateTimeOffset.MaxValue" />.-or-<paramref name="ticks" /> is less than DateTimeOffset.MinValue.Ticks or greater than DateTimeOffset.MaxValue.Ticks.-or-<paramref name="Offset" /> s less than -14 hours or greater than 14 hours.</exception>
		// Token: 0x06000716 RID: 1814 RVA: 0x0001D8D4 File Offset: 0x0001BAD4
		public DateTimeOffset(long ticks, TimeSpan offset)
		{
			this._offsetMinutes = DateTimeOffset.ValidateOffset(offset);
			DateTime dateTime = new DateTime(ticks);
			this._dateTime = DateTimeOffset.ValidateDate(dateTime, offset);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DateTimeOffset" /> structure using the specified <see cref="T:System.DateTime" /> value.</summary>
		/// <param name="dateTime">A date and time.   </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The Coordinated Universal Time (UTC) date and time that results from applying the offset is earlier than <see cref="F:System.DateTimeOffset.MinValue" />.-or-The UTC date and time that results from applying the offset is later than <see cref="F:System.DateTimeOffset.MaxValue" />.</exception>
		// Token: 0x06000717 RID: 1815 RVA: 0x0001D904 File Offset: 0x0001BB04
		public DateTimeOffset(DateTime dateTime)
		{
			TimeSpan localUtcOffset;
			if (dateTime.Kind != DateTimeKind.Utc)
			{
				localUtcOffset = TimeZoneInfo.GetLocalUtcOffset(dateTime, TimeZoneInfoOptions.NoThrowOnInvalidTime);
			}
			else
			{
				localUtcOffset = new TimeSpan(0L);
			}
			this._offsetMinutes = DateTimeOffset.ValidateOffset(localUtcOffset);
			this._dateTime = DateTimeOffset.ValidateDate(dateTime, localUtcOffset);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DateTimeOffset" /> structure using the specified <see cref="T:System.DateTime" /> value and offset.</summary>
		/// <param name="dateTime">A date and time.   </param>
		/// <param name="offset">The time's offset from Coordinated Universal Time (UTC).</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="dateTime.Kind" /> equals <see cref="F:System.DateTimeKind.Utc" /> and <paramref name="offset" /> does not equal zero.-or-<paramref name="dateTime.Kind" /> equals <see cref="F:System.DateTimeKind.Local" /> and <paramref name="offset" /> does not equal the offset of the system's local time zone.-or-<paramref name="offset" /> is not specified in whole minutes.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is less than -14 hours or greater than 14 hours.-or-<see cref="P:System.DateTimeOffset.UtcDateTime" /> is less than <see cref="F:System.DateTimeOffset.MinValue" /> or greater than <see cref="F:System.DateTimeOffset.MaxValue" />. </exception>
		// Token: 0x06000718 RID: 1816 RVA: 0x0001D948 File Offset: 0x0001BB48
		public DateTimeOffset(DateTime dateTime, TimeSpan offset)
		{
			if (dateTime.Kind == DateTimeKind.Local)
			{
				if (offset != TimeZoneInfo.GetLocalUtcOffset(dateTime, TimeZoneInfoOptions.NoThrowOnInvalidTime))
				{
					throw new ArgumentException("The UTC Offset of the local dateTime parameter does not match the offset argument.", "offset");
				}
			}
			else if (dateTime.Kind == DateTimeKind.Utc && offset != TimeSpan.Zero)
			{
				throw new ArgumentException("The UTC Offset for Utc DateTime instances must be 0.", "offset");
			}
			this._offsetMinutes = DateTimeOffset.ValidateOffset(offset);
			this._dateTime = DateTimeOffset.ValidateDate(dateTime, offset);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DateTimeOffset" /> structure using the specified year, month, day, hour, minute, second, and offset.</summary>
		/// <param name="year">The year (1 through 9999).</param>
		/// <param name="month">The month (1 through 12).</param>
		/// <param name="day">The day (1 through the number of days in <paramref name="month" />).</param>
		/// <param name="hour">The hours (0 through 23).   </param>
		/// <param name="minute">The minutes (0 through 59).</param>
		/// <param name="second">The seconds (0 through 59).</param>
		/// <param name="offset">The time's offset from Coordinated Universal Time (UTC).</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="offset" /> does not represent whole minutes.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is less than one or greater than 9999.-or-<paramref name="month" /> is less than one or greater than 12.-or-<paramref name="day" /> is less than one or greater than the number of days in <paramref name="month" />.-or-<paramref name="hour" /> is less than zero or greater than 23.-or-<paramref name="minute" /> is less than 0 or greater than 59.-or-<paramref name="second" /> is less than 0 or greater than 59.-or-<paramref name="offset" /> is less than -14 hours or greater than 14 hours.-or-The <see cref="P:System.DateTimeOffset.UtcDateTime" /> property is earlier than <see cref="F:System.DateTimeOffset.MinValue" /> or later than <see cref="F:System.DateTimeOffset.MaxValue" />.</exception>
		// Token: 0x06000719 RID: 1817 RVA: 0x0001D9BE File Offset: 0x0001BBBE
		public DateTimeOffset(int year, int month, int day, int hour, int minute, int second, TimeSpan offset)
		{
			this._offsetMinutes = DateTimeOffset.ValidateOffset(offset);
			this._dateTime = DateTimeOffset.ValidateDate(new DateTime(year, month, day, hour, minute, second), offset);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DateTimeOffset" /> structure using the specified year, month, day, hour, minute, second, millisecond, and offset.</summary>
		/// <param name="year">The year (1 through 9999).</param>
		/// <param name="month">The month (1 through 12).</param>
		/// <param name="day">The day (1 through the number of days in <paramref name="month" />).</param>
		/// <param name="hour">The hours (0 through 23).   </param>
		/// <param name="minute">The minutes (0 through 59).</param>
		/// <param name="second">The seconds (0 through 59).</param>
		/// <param name="millisecond">The milliseconds (0 through 999).</param>
		/// <param name="offset">The time's offset from Coordinated Universal Time (UTC).</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="offset" /> does not represent whole minutes.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is less than one or greater than 9999.-or-<paramref name="month" /> is less than one or greater than 12.-or-<paramref name="day" /> is less than one or greater than the number of days in <paramref name="month" />.-or-<paramref name="hour" /> is less than zero or greater than 23.-or-<paramref name="minute" /> is less than 0 or greater than 59.-or-<paramref name="second" /> is less than 0 or greater than 59.-or-<paramref name="millisecond" /> is less than 0 or greater than 999.-or-<paramref name="offset" /> is less than -14 or greater than 14.-or-The <see cref="P:System.DateTimeOffset.UtcDateTime" /> property is earlier than <see cref="F:System.DateTimeOffset.MinValue" /> or later than <see cref="F:System.DateTimeOffset.MaxValue" />.</exception>
		// Token: 0x0600071A RID: 1818 RVA: 0x0001D9E8 File Offset: 0x0001BBE8
		public DateTimeOffset(int year, int month, int day, int hour, int minute, int second, int millisecond, TimeSpan offset)
		{
			this._offsetMinutes = DateTimeOffset.ValidateOffset(offset);
			this._dateTime = DateTimeOffset.ValidateDate(new DateTime(year, month, day, hour, minute, second, millisecond), offset);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DateTimeOffset" /> structure using the specified year, month, day, hour, minute, second, millisecond, and offset of a specified calendar.</summary>
		/// <param name="year">The year.</param>
		/// <param name="month">The month (1 through 12).</param>
		/// <param name="day">The day (1 through the number of days in <paramref name="month" />).</param>
		/// <param name="hour">The hours (0 through 23).   </param>
		/// <param name="minute">The minutes (0 through 59).</param>
		/// <param name="second">The seconds (0 through 59).</param>
		/// <param name="millisecond">The milliseconds (0 through 999).</param>
		/// <param name="calendar">The calendar that is used to interpret <paramref name="year" />, <paramref name="month" />, and <paramref name="day" />.</param>
		/// <param name="offset">The time's offset from Coordinated Universal Time (UTC).</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="offset" /> does not represent whole minutes.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="calendar" /> cannot be null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is less than the <paramref name="calendar" /> parameter's MinSupportedDateTime.Year or greater than MaxSupportedDateTime.Year.-or-<paramref name="month" /> is either less than or greater than the number of months in <paramref name="year" /> in the <paramref name="calendar" />. -or-<paramref name="day" /> is less than one or greater than the number of days in <paramref name="month" />.-or-<paramref name="hour" /> is less than zero or greater than 23.-or-<paramref name="minute" /> is less than 0 or greater than 59.-or-<paramref name="second" /> is less than 0 or greater than 59.-or-<paramref name="millisecond" /> is less than 0 or greater than 999.-or-<paramref name="offset" /> is less than -14 hours or greater than 14 hours.-or-The <paramref name="year" />, <paramref name="month" />, and <paramref name="day" /> parameters cannot be represented as a date and time value.-or-The <see cref="P:System.DateTimeOffset.UtcDateTime" /> property is earlier than <see cref="F:System.DateTimeOffset.MinValue" /> or later than <see cref="F:System.DateTimeOffset.MaxValue" />.</exception>
		// Token: 0x0600071B RID: 1819 RVA: 0x0001DA14 File Offset: 0x0001BC14
		public DateTimeOffset(int year, int month, int day, int hour, int minute, int second, int millisecond, Calendar calendar, TimeSpan offset)
		{
			this._offsetMinutes = DateTimeOffset.ValidateOffset(offset);
			this._dateTime = DateTimeOffset.ValidateDate(new DateTime(year, month, day, hour, minute, second, millisecond, calendar), offset);
		}

		/// <summary>Gets a <see cref="T:System.DateTimeOffset" /> object that is set to the current date and time on the current computer, with the offset set to the local time's offset from Coordinated Universal Time (UTC).</summary>
		/// <returns>A <see cref="T:System.DateTimeOffset" /> object whose date and time is the current local time and whose offset is the local time zone's offset from Coordinated Universal Time (UTC).</returns>
		// Token: 0x17000088 RID: 136
		// (get) Token: 0x0600071C RID: 1820 RVA: 0x0001DA4D File Offset: 0x0001BC4D
		public static DateTimeOffset Now
		{
			get
			{
				return new DateTimeOffset(DateTime.Now);
			}
		}

		/// <summary>Gets a <see cref="T:System.DateTime" /> value that represents the date and time of the current <see cref="T:System.DateTimeOffset" /> object.</summary>
		/// <returns>The date and time of the current <see cref="T:System.DateTimeOffset" /> object.</returns>
		// Token: 0x17000089 RID: 137
		// (get) Token: 0x0600071D RID: 1821 RVA: 0x0001DA59 File Offset: 0x0001BC59
		public DateTime DateTime
		{
			get
			{
				return this.ClockDateTime;
			}
		}

		/// <summary>Gets a <see cref="T:System.DateTime" /> value that represents the Coordinated Universal Time (UTC) date and time of the current <see cref="T:System.DateTimeOffset" /> object.</summary>
		/// <returns>The Coordinated Universal Time (UTC) date and time of the current <see cref="T:System.DateTimeOffset" /> object.</returns>
		// Token: 0x1700008A RID: 138
		// (get) Token: 0x0600071E RID: 1822 RVA: 0x0001DA61 File Offset: 0x0001BC61
		public DateTime UtcDateTime
		{
			get
			{
				return DateTime.SpecifyKind(this._dateTime, DateTimeKind.Utc);
			}
		}

		/// <summary>Gets a <see cref="T:System.DateTime" /> value that represents the local date and time of the current <see cref="T:System.DateTimeOffset" /> object.</summary>
		/// <returns>The local date and time of the current <see cref="T:System.DateTimeOffset" /> object.</returns>
		// Token: 0x1700008B RID: 139
		// (get) Token: 0x0600071F RID: 1823 RVA: 0x0001DA70 File Offset: 0x0001BC70
		public DateTime LocalDateTime
		{
			get
			{
				return this.UtcDateTime.ToLocalTime();
			}
		}

		/// <summary>Converts the value of the current <see cref="T:System.DateTimeOffset" /> object to the date and time specified by an offset value.</summary>
		/// <returns>An object that is equal to the original <see cref="T:System.DateTimeOffset" /> object (that is, their <see cref="M:System.DateTimeOffset.ToUniversalTime" /> methods return identical points in time) but whose <see cref="P:System.DateTimeOffset.Offset" /> property is set to <paramref name="offset" />.</returns>
		/// <param name="offset">The offset to convert the <see cref="T:System.DateTimeOffset" /> value to.   </param>
		/// <exception cref="T:System.ArgumentException">The resulting <see cref="T:System.DateTimeOffset" /> object has a <see cref="P:System.DateTimeOffset.DateTime" /> value earlier than <see cref="F:System.DateTimeOffset.MinValue" />.-or-The resulting <see cref="T:System.DateTimeOffset" /> object has a <see cref="P:System.DateTimeOffset.DateTime" /> value later than <see cref="F:System.DateTimeOffset.MaxValue" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is less than -14 hours.-or-<paramref name="offset" /> is greater than 14 hours.</exception>
		// Token: 0x06000720 RID: 1824 RVA: 0x0001DA8C File Offset: 0x0001BC8C
		public DateTimeOffset ToOffset(TimeSpan offset)
		{
			return new DateTimeOffset((this._dateTime + offset).Ticks, offset);
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000721 RID: 1825 RVA: 0x0001DAB4 File Offset: 0x0001BCB4
		private DateTime ClockDateTime
		{
			get
			{
				return new DateTime((this._dateTime + this.Offset).Ticks, DateTimeKind.Unspecified);
			}
		}

		/// <summary>Gets the day of the month represented by the current <see cref="T:System.DateTimeOffset" /> object.</summary>
		/// <returns>The day component of the current <see cref="T:System.DateTimeOffset" /> object, expressed as a value between 1 and 31.</returns>
		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000722 RID: 1826 RVA: 0x0001DAE0 File Offset: 0x0001BCE0
		public int Day
		{
			get
			{
				return this.ClockDateTime.Day;
			}
		}

		/// <summary>Gets the hour component of the time represented by the current <see cref="T:System.DateTimeOffset" /> object.</summary>
		/// <returns>The hour component of the current <see cref="T:System.DateTimeOffset" /> object. This property uses a 24-hour clock; the value ranges from 0 to 23.</returns>
		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000723 RID: 1827 RVA: 0x0001DAFC File Offset: 0x0001BCFC
		public int Hour
		{
			get
			{
				return this.ClockDateTime.Hour;
			}
		}

		/// <summary>Gets the millisecond component of the time represented by the current <see cref="T:System.DateTimeOffset" /> object.</summary>
		/// <returns>The millisecond component of the current <see cref="T:System.DateTimeOffset" /> object, expressed as an integer between 0 and 999.</returns>
		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000724 RID: 1828 RVA: 0x0001DB18 File Offset: 0x0001BD18
		public int Millisecond
		{
			get
			{
				return this.ClockDateTime.Millisecond;
			}
		}

		/// <summary>Gets the minute component of the time represented by the current <see cref="T:System.DateTimeOffset" /> object.</summary>
		/// <returns>The minute component of the current <see cref="T:System.DateTimeOffset" /> object, expressed as an integer between 0 and 59.</returns>
		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000725 RID: 1829 RVA: 0x0001DB34 File Offset: 0x0001BD34
		public int Minute
		{
			get
			{
				return this.ClockDateTime.Minute;
			}
		}

		/// <summary>Gets the month component of the date represented by the current <see cref="T:System.DateTimeOffset" /> object.</summary>
		/// <returns>The month component of the current <see cref="T:System.DateTimeOffset" /> object, expressed as an integer between 1 and 12.</returns>
		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000726 RID: 1830 RVA: 0x0001DB50 File Offset: 0x0001BD50
		public int Month
		{
			get
			{
				return this.ClockDateTime.Month;
			}
		}

		/// <summary>Gets the time's offset from Coordinated Universal Time (UTC). </summary>
		/// <returns>The difference between the current <see cref="T:System.DateTimeOffset" /> object's time value and Coordinated Universal Time (UTC).</returns>
		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000727 RID: 1831 RVA: 0x0001DB6B File Offset: 0x0001BD6B
		public TimeSpan Offset
		{
			get
			{
				return new TimeSpan(0, (int)this._offsetMinutes, 0);
			}
		}

		/// <summary>Gets the second component of the clock time represented by the current <see cref="T:System.DateTimeOffset" /> object.</summary>
		/// <returns>The second component of the <see cref="T:System.DateTimeOffset" /> object, expressed as an integer value between 0 and 59.</returns>
		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000728 RID: 1832 RVA: 0x0001DB7C File Offset: 0x0001BD7C
		public int Second
		{
			get
			{
				return this.ClockDateTime.Second;
			}
		}

		/// <summary>Gets the number of ticks that represents the date and time of the current <see cref="T:System.DateTimeOffset" /> object in clock time.</summary>
		/// <returns>The number of ticks in the <see cref="T:System.DateTimeOffset" /> object's clock time.</returns>
		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000729 RID: 1833 RVA: 0x0001DB98 File Offset: 0x0001BD98
		public long Ticks
		{
			get
			{
				return this.ClockDateTime.Ticks;
			}
		}

		/// <summary>Gets the time of day for the current <see cref="T:System.DateTimeOffset" /> object.</summary>
		/// <returns>The time interval of the current date that has elapsed since midnight.</returns>
		// Token: 0x17000095 RID: 149
		// (get) Token: 0x0600072A RID: 1834 RVA: 0x0001DBB4 File Offset: 0x0001BDB4
		public TimeSpan TimeOfDay
		{
			get
			{
				return this.ClockDateTime.TimeOfDay;
			}
		}

		/// <summary>Gets the year component of the date represented by the current <see cref="T:System.DateTimeOffset" /> object.</summary>
		/// <returns>The year component of the current <see cref="T:System.DateTimeOffset" /> object, expressed as an integer value between 0 and 9999.</returns>
		// Token: 0x17000096 RID: 150
		// (get) Token: 0x0600072B RID: 1835 RVA: 0x0001DBD0 File Offset: 0x0001BDD0
		public int Year
		{
			get
			{
				return this.ClockDateTime.Year;
			}
		}

		/// <summary>Compares two <see cref="T:System.DateTimeOffset" /> objects and indicates whether the first is earlier than the second, equal to the second, or later than the second.</summary>
		/// <returns>A signed integer that indicates whether the value of the <paramref name="first" /> parameter is earlier than, later than, or the same time as the value of the <paramref name="second" /> parameter, as the following table shows.Return valueMeaningLess than zero<paramref name="first" /> is earlier than <paramref name="second" />.Zero<paramref name="first" /> is equal to <paramref name="second" />.Greater than zero<paramref name="first" /> is later than <paramref name="second" />.</returns>
		/// <param name="first">The first object to compare.</param>
		/// <param name="second">The second object to compare.</param>
		// Token: 0x0600072C RID: 1836 RVA: 0x0001DBEB File Offset: 0x0001BDEB
		public static int Compare(DateTimeOffset first, DateTimeOffset second)
		{
			return DateTime.Compare(first.UtcDateTime, second.UtcDateTime);
		}

		/// <summary>Compares the value of the current <see cref="T:System.DateTimeOffset" /> object with another object of the same type.</summary>
		/// <returns>A 32-bit signed integer that indicates whether the current <see cref="T:System.DateTimeOffset" /> object is less than, equal to, or greater than <paramref name="obj" />. The return values of the method are interpreted as follows:Return ValueDescriptionLess than zeroThe current <see cref="T:System.DateTimeOffset" /> object is less than (earlier than) <paramref name="obj" />.ZeroThe current <see cref="T:System.DateTimeOffset" /> object is equal to (the same point in time as) <paramref name="obj" />.Greater than zeroThe current <see cref="T:System.DateTimeOffset" /> object is greater than (later than) <paramref name="obj" />.</returns>
		/// <param name="obj">The object to compare with the current <see cref="T:System.DateTimeOffset" /> object.</param>
		// Token: 0x0600072D RID: 1837 RVA: 0x0001DC00 File Offset: 0x0001BE00
		int IComparable.CompareTo(object obj)
		{
			if (obj == null)
			{
				return 1;
			}
			if (!(obj is DateTimeOffset))
			{
				throw new ArgumentException("Object must be of type DateTimeOffset.");
			}
			DateTime utcDateTime = ((DateTimeOffset)obj).UtcDateTime;
			DateTime utcDateTime2 = this.UtcDateTime;
			if (utcDateTime2 > utcDateTime)
			{
				return 1;
			}
			if (utcDateTime2 < utcDateTime)
			{
				return -1;
			}
			return 0;
		}

		/// <summary>Compares the current <see cref="T:System.DateTimeOffset" /> object to a specified <see cref="T:System.DateTimeOffset" /> object and indicates whether the current object is earlier than, the same as, or later than the second <see cref="T:System.DateTimeOffset" /> object.</summary>
		/// <returns>A signed integer that indicates the relationship between the current <see cref="T:System.DateTimeOffset" /> object and <paramref name="other" />, as the following table shows.Return ValueDescriptionLess than zeroThe current <see cref="T:System.DateTimeOffset" /> object is earlier than <paramref name="other" />.ZeroThe current <see cref="T:System.DateTimeOffset" /> object is the same as <paramref name="other" />.Greater than zero.The current <see cref="T:System.DateTimeOffset" /> object is later than <paramref name="other" />.</returns>
		/// <param name="other">An object to compare with the current <see cref="T:System.DateTimeOffset" /> object.</param>
		// Token: 0x0600072E RID: 1838 RVA: 0x0001DC54 File Offset: 0x0001BE54
		public int CompareTo(DateTimeOffset other)
		{
			DateTime utcDateTime = other.UtcDateTime;
			DateTime utcDateTime2 = this.UtcDateTime;
			if (utcDateTime2 > utcDateTime)
			{
				return 1;
			}
			if (utcDateTime2 < utcDateTime)
			{
				return -1;
			}
			return 0;
		}

		/// <summary>Determines whether a <see cref="T:System.DateTimeOffset" /> object represents the same point in time as a specified object.</summary>
		/// <returns>true if the <paramref name="obj" /> parameter is a <see cref="T:System.DateTimeOffset" /> object and represents the same point in time as the current <see cref="T:System.DateTimeOffset" /> object; otherwise, false.</returns>
		/// <param name="obj">The object to compare to the current <see cref="T:System.DateTimeOffset" /> object.</param>
		// Token: 0x0600072F RID: 1839 RVA: 0x0001DC88 File Offset: 0x0001BE88
		public override bool Equals(object obj)
		{
			return obj is DateTimeOffset && this.UtcDateTime.Equals(((DateTimeOffset)obj).UtcDateTime);
		}

		/// <summary>Determines whether the current <see cref="T:System.DateTimeOffset" /> object represents the same point in time as a specified <see cref="T:System.DateTimeOffset" /> object.</summary>
		/// <returns>true if both <see cref="T:System.DateTimeOffset" /> objects have the same <see cref="P:System.DateTimeOffset.UtcDateTime" /> value; otherwise, false.</returns>
		/// <param name="other">An object to compare to the current <see cref="T:System.DateTimeOffset" /> object.   </param>
		// Token: 0x06000730 RID: 1840 RVA: 0x0001DCBC File Offset: 0x0001BEBC
		public bool Equals(DateTimeOffset other)
		{
			return this.UtcDateTime.Equals(other.UtcDateTime);
		}

		/// <summary>Converts the specified Windows file time to an equivalent local time.</summary>
		/// <returns>An object that represents the date and time of <paramref name="fileTime" /> with the offset set to the local time offset.</returns>
		/// <param name="fileTime">A Windows file time, expressed in ticks.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="filetime" /> is less than zero.-or-<paramref name="filetime" /> is greater than DateTimeOffset.MaxValue.Ticks.</exception>
		// Token: 0x06000731 RID: 1841 RVA: 0x0001DCDE File Offset: 0x0001BEDE
		public static DateTimeOffset FromFileTime(long fileTime)
		{
			return new DateTimeOffset(DateTime.FromFileTime(fileTime));
		}

		// Token: 0x06000732 RID: 1842 RVA: 0x0001DCEC File Offset: 0x0001BEEC
		public static DateTimeOffset FromUnixTimeMilliseconds(long milliseconds)
		{
			if (milliseconds < -62135596800000L || milliseconds > 253402300799999L)
			{
				throw new ArgumentOutOfRangeException("milliseconds", SR.Format("Valid values are between {0} and {1}, inclusive.", -62135596800000L, 253402300799999L));
			}
			return new DateTimeOffset(milliseconds * 10000L + 621355968000000000L, TimeSpan.Zero);
		}

		/// <summary>Runs when the deserialization of an object has been completed.</summary>
		/// <param name="sender">The object that initiated the callback. The functionality for this parameter is not currently implemented.</param>
		// Token: 0x06000733 RID: 1843 RVA: 0x0001DD60 File Offset: 0x0001BF60
		void IDeserializationCallback.OnDeserialization(object sender)
		{
			try
			{
				DateTimeOffset.ValidateOffset(this.Offset);
				DateTimeOffset.ValidateDate(this.ClockDateTime, this.Offset);
			}
			catch (ArgumentException ex)
			{
				throw new SerializationException("An error occurred while deserializing the object.  The serialized data is corrupt.", ex);
			}
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the data required to serialize the current <see cref="T:System.DateTimeOffset" /> object.</summary>
		/// <param name="info">The object to populate with data.</param>
		/// <param name="context">The destination for this serialization (see <see cref="T:System.Runtime.Serialization.StreamingContext" />).</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="info" /> parameter is null.</exception>
		// Token: 0x06000734 RID: 1844 RVA: 0x0001DDAC File Offset: 0x0001BFAC
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("DateTime", this._dateTime);
			info.AddValue("OffsetMinutes", this._offsetMinutes);
		}

		// Token: 0x06000735 RID: 1845 RVA: 0x0001DDE0 File Offset: 0x0001BFE0
		private DateTimeOffset(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			this._dateTime = (DateTime)info.GetValue("DateTime", typeof(DateTime));
			this._offsetMinutes = (short)info.GetValue("OffsetMinutes", typeof(short));
		}

		/// <summary>Returns the hash code for the current <see cref="T:System.DateTimeOffset" /> object.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06000736 RID: 1846 RVA: 0x0001DE3C File Offset: 0x0001C03C
		public override int GetHashCode()
		{
			return this.UtcDateTime.GetHashCode();
		}

		/// <summary>Converts the specified string representation of a date and time to its <see cref="T:System.DateTimeOffset" /> equivalent using the specified culture-specific format information.</summary>
		/// <returns>An object that is equivalent to the date and time that is contained in <paramref name="input" />, as specified by <paramref name="formatProvider" />.</returns>
		/// <param name="input">A string that contains a date and time to convert.   </param>
		/// <param name="formatProvider">An object that provides culture-specific format information about <paramref name="input" />.</param>
		/// <exception cref="T:System.ArgumentException">The offset is greater than 14 hours or less than -14 hours.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is null.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="input" /> does not contain a valid string representation of a date and time.-or-<paramref name="input" /> contains the string representation of an offset value without a date or time.</exception>
		// Token: 0x06000737 RID: 1847 RVA: 0x0001DE57 File Offset: 0x0001C057
		public static DateTimeOffset Parse(string input, IFormatProvider formatProvider)
		{
			if (input == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.input);
			}
			return DateTimeOffset.Parse(input, formatProvider, DateTimeStyles.None);
		}

		/// <summary>Converts the specified string representation of a date and time to its <see cref="T:System.DateTimeOffset" /> equivalent using the specified culture-specific format information and formatting style.</summary>
		/// <returns>An object that is equivalent to the date and time that is contained in <paramref name="input" /> as specified by <paramref name="formatProvider" /> and <paramref name="styles" />.</returns>
		/// <param name="input">A string that contains a date and time to convert.   </param>
		/// <param name="formatProvider">An object that provides culture-specific format information about <paramref name="input" />.</param>
		/// <param name="styles">A bitwise combination of enumeration values that indicates the permitted format of <paramref name="input" />. A typical value to specify is <see cref="F:System.Globalization.DateTimeStyles.None" />.   </param>
		/// <exception cref="T:System.ArgumentException">The offset is greater than 14 hours or less than -14 hours.-or-<paramref name="styles" /> is not a valid <see cref="T:System.Globalization.DateTimeStyles" /> value.-or-<paramref name="styles" /> includes an unsupported <see cref="T:System.Globalization.DateTimeStyles" /> value.-or-<paramref name="styles" /> includes <see cref="T:System.Globalization.DateTimeStyles" /> values that cannot be used together.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is null.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="input" /> does not contain a valid string representation of a date and time.-or-<paramref name="input" /> contains the string representation of an offset value without a date or time.</exception>
		// Token: 0x06000738 RID: 1848 RVA: 0x0001DE6C File Offset: 0x0001C06C
		public static DateTimeOffset Parse(string input, IFormatProvider formatProvider, DateTimeStyles styles)
		{
			styles = DateTimeOffset.ValidateStyles(styles, "styles");
			if (input == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.input);
			}
			TimeSpan timeSpan;
			return new DateTimeOffset(DateTimeParse.Parse(input, DateTimeFormatInfo.GetInstance(formatProvider), styles, out timeSpan).Ticks, timeSpan);
		}

		/// <summary>Converts the specified string representation of a date and time to its <see cref="T:System.DateTimeOffset" /> equivalent using the specified format, culture-specific format information, and style. The format of the string representation must match the specified format exactly.</summary>
		/// <returns>An object that is equivalent to the date and time that is contained in the <paramref name="input" /> parameter, as specified by the <paramref name="format" />, <paramref name="formatProvider" />, and <paramref name="styles" /> parameters.</returns>
		/// <param name="input">A string that contains a date and time to convert.</param>
		/// <param name="format">A format specifier that defines the expected format of <paramref name="input" />.</param>
		/// <param name="formatProvider">An object that supplies culture-specific formatting information about <paramref name="input" />.</param>
		/// <param name="styles">A bitwise combination of enumeration values that indicates the permitted format of <paramref name="input" />.</param>
		/// <exception cref="T:System.ArgumentException">The offset is greater than 14 hours or less than -14 hours.-or-The <paramref name="styles" /> parameter includes an unsupported value.-or-The <paramref name="styles" /> parameter contains <see cref="T:System.Globalization.DateTimeStyles" /> values that cannot be used together.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is null.-or-<paramref name="format" /> is null.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="input" /> is an empty string ("").-or-<paramref name="input" /> does not contain a valid string representation of a date and time.-or-<paramref name="format" /> is an empty string.-or-The hour component and the AM/PM designator in <paramref name="input" /> do not agree. </exception>
		// Token: 0x06000739 RID: 1849 RVA: 0x0001DEB4 File Offset: 0x0001C0B4
		public static DateTimeOffset ParseExact(string input, string format, IFormatProvider formatProvider, DateTimeStyles styles)
		{
			styles = DateTimeOffset.ValidateStyles(styles, "styles");
			if (input == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.input);
			}
			if (format == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.format);
			}
			TimeSpan timeSpan;
			return new DateTimeOffset(DateTimeParse.ParseExact(input, format, DateTimeFormatInfo.GetInstance(formatProvider), styles, out timeSpan).Ticks, timeSpan);
		}

		/// <summary>Converts the specified string representation of a date and time to its <see cref="T:System.DateTimeOffset" /> equivalent using the specified formats, culture-specific format information, and style. The format of the string representation must match one of the specified formats exactly.</summary>
		/// <returns>An object that is equivalent to the date and time that is contained in the <paramref name="input" /> parameter, as specified by the <paramref name="formats" />, <paramref name="formatProvider" />, and <paramref name="styles" /> parameters.</returns>
		/// <param name="input">A string that contains a date and time to convert.</param>
		/// <param name="formats">An array of format specifiers that define the expected formats of <paramref name="input" />.</param>
		/// <param name="formatProvider">An object that supplies culture-specific formatting information about <paramref name="input" />.</param>
		/// <param name="styles">A bitwise combination of enumeration values that indicates the permitted format of <paramref name="input" />.</param>
		/// <exception cref="T:System.ArgumentException">The offset is greater than 14 hours or less than -14 hours.-or-<paramref name="styles" /> includes an unsupported value.-or-The <paramref name="styles" /> parameter contains <see cref="T:System.Globalization.DateTimeStyles" /> values that cannot be used together.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is null.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="input" /> is an empty string ("").-or-<paramref name="input" /> does not contain a valid string representation of a date and time.-or-No element of <paramref name="formats" /> contains a valid format specifier.-or-The hour component and the AM/PM designator in <paramref name="input" /> do not agree. </exception>
		// Token: 0x0600073A RID: 1850 RVA: 0x0001DF0C File Offset: 0x0001C10C
		public static DateTimeOffset ParseExact(string input, string[] formats, IFormatProvider formatProvider, DateTimeStyles styles)
		{
			styles = DateTimeOffset.ValidateStyles(styles, "styles");
			if (input == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.input);
			}
			TimeSpan timeSpan;
			return new DateTimeOffset(DateTimeParse.ParseExactMultiple(input, formats, DateTimeFormatInfo.GetInstance(formatProvider), styles, out timeSpan).Ticks, timeSpan);
		}

		/// <summary>Converts the value of the current <see cref="T:System.DateTimeOffset" /> object to a Windows file time.</summary>
		/// <returns>The value of the current <see cref="T:System.DateTimeOffset" /> object, expressed as a Windows file time.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The resulting file time would represent a date and time before midnight on January 1, 1601 C.E. Coordinated Universal Time (UTC).</exception>
		// Token: 0x0600073B RID: 1851 RVA: 0x0001DF54 File Offset: 0x0001C154
		public long ToFileTime()
		{
			return this.UtcDateTime.ToFileTime();
		}

		/// <summary>Converts the value of the current <see cref="T:System.DateTimeOffset" /> object to its equivalent string representation.</summary>
		/// <returns>A string representation of a <see cref="T:System.DateTimeOffset" /> object that includes the offset appended at the end of the string.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The date and time is outside the range of dates supported by the calendar used by the current culture. </exception>
		// Token: 0x0600073C RID: 1852 RVA: 0x0001DF6F File Offset: 0x0001C16F
		public override string ToString()
		{
			return DateTimeFormat.Format(this.ClockDateTime, null, null, this.Offset);
		}

		/// <summary>Converts the value of the current <see cref="T:System.DateTimeOffset" /> object to its equivalent string representation using the specified culture-specific formatting information.</summary>
		/// <returns>A string representation of the value of the current <see cref="T:System.DateTimeOffset" /> object, as specified by <paramref name="formatProvider" />.</returns>
		/// <param name="formatProvider">An object that supplies culture-specific formatting information.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The date and time is outside the range of dates supported by the calendar used by <paramref name="formatProvider" />. </exception>
		// Token: 0x0600073D RID: 1853 RVA: 0x0001DF84 File Offset: 0x0001C184
		public string ToString(IFormatProvider formatProvider)
		{
			return DateTimeFormat.Format(this.ClockDateTime, null, formatProvider, this.Offset);
		}

		/// <summary>Converts the value of the current <see cref="T:System.DateTimeOffset" /> object to its equivalent string representation using the specified format and culture-specific format information.</summary>
		/// <returns>A string representation of the value of the current <see cref="T:System.DateTimeOffset" /> object, as specified by <paramref name="format" /> and <paramref name="provider" />.</returns>
		/// <param name="format">A format string.</param>
		/// <param name="formatProvider">An object that supplies culture-specific formatting information.</param>
		/// <exception cref="T:System.FormatException">The length of <paramref name="format" /> is one, and it is not one of the standard format specifier characters defined for <see cref="T:System.Globalization.DateTimeFormatInfo" />.-or-<paramref name="format" /> does not contain a valid custom format pattern. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The date and time is outside the range of dates supported by the calendar used by <paramref name="formatProvider" />. </exception>
		// Token: 0x0600073E RID: 1854 RVA: 0x0001DF99 File Offset: 0x0001C199
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return DateTimeFormat.Format(this.ClockDateTime, format, formatProvider, this.Offset);
		}

		// Token: 0x0600073F RID: 1855 RVA: 0x0001DFAE File Offset: 0x0001C1AE
		public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format = default(ReadOnlySpan<char>), IFormatProvider formatProvider = null)
		{
			return DateTimeFormat.TryFormat(this.ClockDateTime, destination, out charsWritten, format, formatProvider, this.Offset);
		}

		/// <summary>Converts the current <see cref="T:System.DateTimeOffset" /> object to a <see cref="T:System.DateTimeOffset" /> value that represents the Coordinated Universal Time (UTC).</summary>
		/// <returns>An object that represents the date and time of the current <see cref="T:System.DateTimeOffset" /> object converted to Coordinated Universal Time (UTC).</returns>
		// Token: 0x06000740 RID: 1856 RVA: 0x0001DFC6 File Offset: 0x0001C1C6
		public DateTimeOffset ToUniversalTime()
		{
			return new DateTimeOffset(this.UtcDateTime);
		}

		/// <summary>Tries to convert a specified string representation of a date and time to its <see cref="T:System.DateTimeOffset" /> equivalent, and returns a value that indicates whether the conversion succeeded.</summary>
		/// <returns>true if the <paramref name="input" /> parameter is successfully converted; otherwise, false.</returns>
		/// <param name="input">A string that contains a date and time to convert.</param>
		/// <param name="formatProvider">An object that provides culture-specific formatting information about <paramref name="input" />.</param>
		/// <param name="styles">A bitwise combination of enumeration values that indicates the permitted format of <paramref name="input" />. </param>
		/// <param name="result">When the method returns, contains the <see cref="T:System.DateTimeOffset" /> value equivalent to the date and time of <paramref name="input" />, if the conversion succeeded, or <see cref="F:System.DateTimeOffset.MinValue" />, if the conversion failed. The conversion fails if the <paramref name="input" /> parameter is null or does not contain a valid string representation of a date and time. This parameter is passed uninitialized.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="styles" /> includes an undefined <see cref="T:System.Globalization.DateTimeStyles" /> value.-or-<see cref="F:System.Globalization.DateTimeStyles.NoCurrentDateDefault" />  is not supported.-or-<paramref name="styles" /> includes mutually exclusive <see cref="T:System.Globalization.DateTimeStyles" /> values.</exception>
		// Token: 0x06000741 RID: 1857 RVA: 0x0001DFD4 File Offset: 0x0001C1D4
		public static bool TryParse(string input, IFormatProvider formatProvider, DateTimeStyles styles, out DateTimeOffset result)
		{
			styles = DateTimeOffset.ValidateStyles(styles, "styles");
			if (input == null)
			{
				result = default(DateTimeOffset);
				return false;
			}
			DateTime dateTime;
			TimeSpan timeSpan;
			bool flag = DateTimeParse.TryParse(input, DateTimeFormatInfo.GetInstance(formatProvider), styles, out dateTime, out timeSpan);
			result = new DateTimeOffset(dateTime.Ticks, timeSpan);
			return flag;
		}

		/// <summary>Converts the specified string representation of a date and time to its <see cref="T:System.DateTimeOffset" /> equivalent using the specified format, culture-specific format information, and style. The format of the string representation must match the specified format exactly.</summary>
		/// <returns>true if the <paramref name="input" /> parameter is successfully converted; otherwise, false.</returns>
		/// <param name="input">A string that contains a date and time to convert.</param>
		/// <param name="format">A format specifier that defines the required format of <paramref name="input" />.</param>
		/// <param name="formatProvider">An object that supplies culture-specific formatting information about <paramref name="input" />.</param>
		/// <param name="styles">A bitwise combination of enumeration values that indicates the permitted format of input. A typical value to specify is None.</param>
		/// <param name="result">When the method returns, contains the <see cref="T:System.DateTimeOffset" /> equivalent to the date and time of <paramref name="input" />, if the conversion succeeded, or <see cref="F:System.DateTimeOffset.MinValue" />, if the conversion failed. The conversion fails if the <paramref name="input" /> parameter is null, or does not contain a valid string representation of a date and time in the expected format defined by <paramref name="format" /> and <paramref name="provider" />. This parameter is passed uninitialized.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="styles" /> includes an undefined <see cref="T:System.Globalization.DateTimeStyles" /> value.-or-<see cref="F:System.Globalization.DateTimeStyles.NoCurrentDateDefault" />  is not supported.-or-<paramref name="styles" /> includes mutually exclusive <see cref="T:System.Globalization.DateTimeStyles" /> values.</exception>
		// Token: 0x06000742 RID: 1858 RVA: 0x0001E024 File Offset: 0x0001C224
		public static bool TryParseExact(string input, string format, IFormatProvider formatProvider, DateTimeStyles styles, out DateTimeOffset result)
		{
			styles = DateTimeOffset.ValidateStyles(styles, "styles");
			if (input == null || format == null)
			{
				result = default(DateTimeOffset);
				return false;
			}
			DateTime dateTime;
			TimeSpan timeSpan;
			bool flag = DateTimeParse.TryParseExact(input, format, DateTimeFormatInfo.GetInstance(formatProvider), styles, out dateTime, out timeSpan);
			result = new DateTimeOffset(dateTime.Ticks, timeSpan);
			return flag;
		}

		// Token: 0x06000743 RID: 1859 RVA: 0x0001E080 File Offset: 0x0001C280
		private static short ValidateOffset(TimeSpan offset)
		{
			long ticks = offset.Ticks;
			if (ticks % 600000000L != 0L)
			{
				throw new ArgumentException("Offset must be specified in whole minutes.", "offset");
			}
			if (ticks < -504000000000L || ticks > 504000000000L)
			{
				throw new ArgumentOutOfRangeException("offset", "Offset must be within plus or minus 14 hours.");
			}
			return (short)(offset.Ticks / 600000000L);
		}

		// Token: 0x06000744 RID: 1860 RVA: 0x0001E0E8 File Offset: 0x0001C2E8
		private static DateTime ValidateDate(DateTime dateTime, TimeSpan offset)
		{
			long num = dateTime.Ticks - offset.Ticks;
			if (num < 0L || num > 3155378975999999999L)
			{
				throw new ArgumentOutOfRangeException("offset", "The UTC time represented when the offset is applied must be between year 0 and 10,000.");
			}
			return new DateTime(num, DateTimeKind.Unspecified);
		}

		// Token: 0x06000745 RID: 1861 RVA: 0x0001E130 File Offset: 0x0001C330
		private static DateTimeStyles ValidateStyles(DateTimeStyles style, string parameterName)
		{
			if ((style & ~(DateTimeStyles.AllowLeadingWhite | DateTimeStyles.AllowTrailingWhite | DateTimeStyles.AllowInnerWhite | DateTimeStyles.NoCurrentDateDefault | DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeLocal | DateTimeStyles.AssumeUniversal | DateTimeStyles.RoundtripKind)) != DateTimeStyles.None)
			{
				throw new ArgumentException("An undefined DateTimeStyles value is being used.", parameterName);
			}
			if ((style & DateTimeStyles.AssumeLocal) != DateTimeStyles.None && (style & DateTimeStyles.AssumeUniversal) != DateTimeStyles.None)
			{
				throw new ArgumentException("The DateTimeStyles values AssumeLocal and AssumeUniversal cannot be used together.", parameterName);
			}
			if ((style & DateTimeStyles.NoCurrentDateDefault) != DateTimeStyles.None)
			{
				throw new ArgumentException("The DateTimeStyles value 'NoCurrentDateDefault' is not allowed when parsing DateTimeOffset.", parameterName);
			}
			style &= ~DateTimeStyles.RoundtripKind;
			style &= ~DateTimeStyles.AssumeLocal;
			return style;
		}

		/// <summary>Defines an implicit conversion of a <see cref="T:System.DateTime" /> object to a <see cref="T:System.DateTimeOffset" /> object.</summary>
		/// <returns>The converted object.</returns>
		/// <param name="dateTime">The object to convert. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The Coordinated Universal Time (UTC) date and time that results from applying the offset is earlier than <see cref="F:System.DateTimeOffset.MinValue" />.-or-The UTC date and time that results from applying the offset is later than <see cref="F:System.DateTimeOffset.MaxValue" />.</exception>
		// Token: 0x06000746 RID: 1862 RVA: 0x0001E18B File Offset: 0x0001C38B
		public static implicit operator DateTimeOffset(DateTime dateTime)
		{
			return new DateTimeOffset(dateTime);
		}

		/// <summary>Subtracts one <see cref="T:System.DateTimeOffset" /> object from another and yields a time interval.</summary>
		/// <returns>An object that represents the difference between <paramref name="left" /> and <paramref name="right" />.</returns>
		/// <param name="left">The minuend.   </param>
		/// <param name="right">The subtrahend.</param>
		// Token: 0x06000747 RID: 1863 RVA: 0x0001E193 File Offset: 0x0001C393
		public static TimeSpan operator -(DateTimeOffset left, DateTimeOffset right)
		{
			return left.UtcDateTime - right.UtcDateTime;
		}

		/// <summary>Determines whether two specified <see cref="T:System.DateTimeOffset" /> objects represent the same point in time.</summary>
		/// <returns>true if both <see cref="T:System.DateTimeOffset" /> objects have the same <see cref="P:System.DateTimeOffset.UtcDateTime" /> value; otherwise, false.</returns>
		/// <param name="left">The first object to compare.</param>
		/// <param name="right">The second object to compare.</param>
		// Token: 0x06000748 RID: 1864 RVA: 0x0001E1A8 File Offset: 0x0001C3A8
		public static bool operator ==(DateTimeOffset left, DateTimeOffset right)
		{
			return left.UtcDateTime == right.UtcDateTime;
		}

		/// <summary>Determines whether two specified <see cref="T:System.DateTimeOffset" /> objects refer to different points in time.</summary>
		/// <returns>true if <paramref name="left" /> and <paramref name="right" /> do not have the same <see cref="P:System.DateTimeOffset.UtcDateTime" /> value; otherwise, false.</returns>
		/// <param name="left">The first object to compare.</param>
		/// <param name="right">The second object to compare.</param>
		// Token: 0x06000749 RID: 1865 RVA: 0x0001E1BD File Offset: 0x0001C3BD
		public static bool operator !=(DateTimeOffset left, DateTimeOffset right)
		{
			return left.UtcDateTime != right.UtcDateTime;
		}

		/// <summary>Represents the earliest possible <see cref="T:System.DateTimeOffset" /> value. This field is read-only.</summary>
		// Token: 0x04000326 RID: 806
		public static readonly DateTimeOffset MinValue = new DateTimeOffset(0L, TimeSpan.Zero);

		/// <summary>Represents the greatest possible value of <see cref="T:System.DateTimeOffset" />. This field is read-only.</summary>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <see cref="F:System.DateTime.MaxValue" /> is outside the range of the current or specified culture's default calendar.</exception>
		// Token: 0x04000327 RID: 807
		public static readonly DateTimeOffset MaxValue = new DateTimeOffset(3155378975999999999L, TimeSpan.Zero);

		// Token: 0x04000328 RID: 808
		public static readonly DateTimeOffset UnixEpoch = new DateTimeOffset(621355968000000000L, TimeSpan.Zero);

		// Token: 0x04000329 RID: 809
		private readonly DateTime _dateTime;

		// Token: 0x0400032A RID: 810
		private readonly short _offsetMinutes;
	}
}
