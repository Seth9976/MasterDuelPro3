using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>Represents an instant in time, typically expressed as a date and time of day. </summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020000D4 RID: 212
	[Serializable]
	[StructLayout(LayoutKind.Auto)]
	public readonly struct DateTime : IComparable, IFormattable, IConvertible, IComparable<DateTime>, IEquatable<DateTime>, ISerializable, ISpanFormattable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.DateTime" /> structure to a specified number of ticks.</summary>
		/// <param name="ticks">A date and time expressed in the number of 100-nanosecond intervals that have elapsed since January 1, 0001 at 00:00:00.000 in the Gregorian calendar. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="ticks" /> is less than <see cref="F:System.DateTime.MinValue" /> or greater than <see cref="F:System.DateTime.MaxValue" />. </exception>
		// Token: 0x060006A4 RID: 1700 RVA: 0x0001C246 File Offset: 0x0001A446
		public DateTime(long ticks)
		{
			if (ticks < 0L || ticks > 3155378975999999999L)
			{
				throw new ArgumentOutOfRangeException("ticks", "Ticks must be between DateTime.MinValue.Ticks and DateTime.MaxValue.Ticks.");
			}
			this._dateData = (ulong)ticks;
		}

		// Token: 0x060006A5 RID: 1701 RVA: 0x0001C270 File Offset: 0x0001A470
		private DateTime(ulong dateData)
		{
			this._dateData = dateData;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DateTime" /> structure to a specified number of ticks and to Coordinated Universal Time (UTC) or local time.</summary>
		/// <param name="ticks">A date and time expressed in the number of 100-nanosecond intervals that have elapsed since January 1, 0001 at 00:00:00.000 in the Gregorian calendar. </param>
		/// <param name="kind">One of the enumeration values that indicates whether <paramref name="ticks" /> specifies a local time, Coordinated Universal Time (UTC), or neither.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="ticks" /> is less than <see cref="F:System.DateTime.MinValue" /> or greater than <see cref="F:System.DateTime.MaxValue" />. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="kind" /> is not one of the <see cref="T:System.DateTimeKind" /> values.</exception>
		// Token: 0x060006A6 RID: 1702 RVA: 0x0001C27C File Offset: 0x0001A47C
		public DateTime(long ticks, DateTimeKind kind)
		{
			if (ticks < 0L || ticks > 3155378975999999999L)
			{
				throw new ArgumentOutOfRangeException("ticks", "Ticks must be between DateTime.MinValue.Ticks and DateTime.MaxValue.Ticks.");
			}
			if (kind < DateTimeKind.Unspecified || kind > DateTimeKind.Local)
			{
				throw new ArgumentException("Invalid DateTimeKind value.", "kind");
			}
			this._dateData = (ulong)(ticks | ((long)kind << 62));
		}

		// Token: 0x060006A7 RID: 1703 RVA: 0x0001C2D0 File Offset: 0x0001A4D0
		internal DateTime(long ticks, DateTimeKind kind, bool isAmbiguousDst)
		{
			if (ticks < 0L || ticks > 3155378975999999999L)
			{
				throw new ArgumentOutOfRangeException("ticks", "Ticks must be between DateTime.MinValue.Ticks and DateTime.MaxValue.Ticks.");
			}
			this._dateData = (ulong)(ticks | (isAmbiguousDst ? (-4611686018427387904L) : long.MinValue));
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DateTime" /> structure to the specified year, month, and day.</summary>
		/// <param name="year">The year (1 through 9999). </param>
		/// <param name="month">The month (1 through 12). </param>
		/// <param name="day">The day (1 through the number of days in <paramref name="month" />). </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is less than 1 or greater than 9999.-or- <paramref name="month" /> is less than 1 or greater than 12.-or- <paramref name="day" /> is less than 1 or greater than the number of days in <paramref name="month" />. </exception>
		// Token: 0x060006A8 RID: 1704 RVA: 0x0001C31D File Offset: 0x0001A51D
		public DateTime(int year, int month, int day)
		{
			this._dateData = (ulong)DateTime.DateToTicks(year, month, day);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DateTime" /> structure to the specified year, month, and day for the specified calendar.</summary>
		/// <param name="year">The year (1 through the number of years in <paramref name="calendar" />). </param>
		/// <param name="month">The month (1 through the number of months in <paramref name="calendar" />). </param>
		/// <param name="day">The day (1 through the number of days in <paramref name="month" />). </param>
		/// <param name="calendar">The calendar that is used to interpret <paramref name="year" />, <paramref name="month" />, and <paramref name="day" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="calendar" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is not in the range supported by <paramref name="calendar" />.-or- <paramref name="month" /> is less than 1 or greater than the number of months in <paramref name="calendar" />.-or- <paramref name="day" /> is less than 1 or greater than the number of days in <paramref name="month" />. </exception>
		// Token: 0x060006A9 RID: 1705 RVA: 0x0001C32D File Offset: 0x0001A52D
		public DateTime(int year, int month, int day, Calendar calendar)
		{
			this = new DateTime(year, month, day, 0, 0, 0, calendar);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DateTime" /> structure to the specified year, month, day, hour, minute, and second.</summary>
		/// <param name="year">The year (1 through 9999). </param>
		/// <param name="month">The month (1 through 12). </param>
		/// <param name="day">The day (1 through the number of days in <paramref name="month" />). </param>
		/// <param name="hour">The hours (0 through 23). </param>
		/// <param name="minute">The minutes (0 through 59). </param>
		/// <param name="second">The seconds (0 through 59). </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is less than 1 or greater than 9999. -or- <paramref name="month" /> is less than 1 or greater than 12. -or- <paramref name="day" /> is less than 1 or greater than the number of days in <paramref name="month" />.-or- <paramref name="hour" /> is less than 0 or greater than 23. -or- <paramref name="minute" /> is less than 0 or greater than 59. -or- <paramref name="second" /> is less than 0 or greater than 59. </exception>
		// Token: 0x060006AA RID: 1706 RVA: 0x0001C33D File Offset: 0x0001A53D
		public DateTime(int year, int month, int day, int hour, int minute, int second)
		{
			this._dateData = (ulong)(DateTime.DateToTicks(year, month, day) + DateTime.TimeToTicks(hour, minute, second));
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DateTime" /> structure to the specified year, month, day, hour, minute, second, and Coordinated Universal Time (UTC) or local time.</summary>
		/// <param name="year">The year (1 through 9999). </param>
		/// <param name="month">The month (1 through 12). </param>
		/// <param name="day">The day (1 through the number of days in <paramref name="month" />). </param>
		/// <param name="hour">The hours (0 through 23). </param>
		/// <param name="minute">The minutes (0 through 59). </param>
		/// <param name="second">The seconds (0 through 59). </param>
		/// <param name="kind">One of the enumeration values that indicates whether <paramref name="year" />, <paramref name="month" />, <paramref name="day" />, <paramref name="hour" />, <paramref name="minute" /> and <paramref name="second" /> specify a local time, Coordinated Universal Time (UTC), or neither.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is less than 1 or greater than 9999. -or- <paramref name="month" /> is less than 1 or greater than 12. -or- <paramref name="day" /> is less than 1 or greater than the number of days in <paramref name="month" />.-or- <paramref name="hour" /> is less than 0 or greater than 23. -or- <paramref name="minute" /> is less than 0 or greater than 59. -or- <paramref name="second" /> is less than 0 or greater than 59. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="kind" /> is not one of the <see cref="T:System.DateTimeKind" /> values.</exception>
		// Token: 0x060006AB RID: 1707 RVA: 0x0001C35C File Offset: 0x0001A55C
		public DateTime(int year, int month, int day, int hour, int minute, int second, DateTimeKind kind)
		{
			if (kind < DateTimeKind.Unspecified || kind > DateTimeKind.Local)
			{
				throw new ArgumentException("Invalid DateTimeKind value.", "kind");
			}
			long num = DateTime.DateToTicks(year, month, day) + DateTime.TimeToTicks(hour, minute, second);
			this._dateData = (ulong)(num | ((long)kind << 62));
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DateTime" /> structure to the specified year, month, day, hour, minute, and second for the specified calendar.</summary>
		/// <param name="year">The year (1 through the number of years in <paramref name="calendar" />). </param>
		/// <param name="month">The month (1 through the number of months in <paramref name="calendar" />). </param>
		/// <param name="day">The day (1 through the number of days in <paramref name="month" />). </param>
		/// <param name="hour">The hours (0 through 23). </param>
		/// <param name="minute">The minutes (0 through 59). </param>
		/// <param name="second">The seconds (0 through 59). </param>
		/// <param name="calendar">The calendar that is used to interpret <paramref name="year" />, <paramref name="month" />, and <paramref name="day" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="calendar" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is not in the range supported by <paramref name="calendar" />.-or- <paramref name="month" /> is less than 1 or greater than the number of months in <paramref name="calendar" />.-or- <paramref name="day" /> is less than 1 or greater than the number of days in <paramref name="month" />.-or- <paramref name="hour" /> is less than 0 or greater than 23 -or- <paramref name="minute" /> is less than 0 or greater than 59. -or- <paramref name="second" /> is less than 0 or greater than 59. </exception>
		// Token: 0x060006AC RID: 1708 RVA: 0x0001C3A8 File Offset: 0x0001A5A8
		public DateTime(int year, int month, int day, int hour, int minute, int second, Calendar calendar)
		{
			if (calendar == null)
			{
				throw new ArgumentNullException("calendar");
			}
			this._dateData = (ulong)calendar.ToDateTime(year, month, day, hour, minute, second, 0).Ticks;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DateTime" /> structure to the specified year, month, day, hour, minute, second, and millisecond.</summary>
		/// <param name="year">The year (1 through 9999). </param>
		/// <param name="month">The month (1 through 12). </param>
		/// <param name="day">The day (1 through the number of days in <paramref name="month" />). </param>
		/// <param name="hour">The hours (0 through 23). </param>
		/// <param name="minute">The minutes (0 through 59). </param>
		/// <param name="second">The seconds (0 through 59). </param>
		/// <param name="millisecond">The milliseconds (0 through 999). </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is less than 1 or greater than 9999.-or- <paramref name="month" /> is less than 1 or greater than 12.-or- <paramref name="day" /> is less than 1 or greater than the number of days in <paramref name="month" />.-or- <paramref name="hour" /> is less than 0 or greater than 23.-or- <paramref name="minute" /> is less than 0 or greater than 59.-or- <paramref name="second" /> is less than 0 or greater than 59.-or- <paramref name="millisecond" /> is less than 0 or greater than 999. </exception>
		// Token: 0x060006AD RID: 1709 RVA: 0x0001C3E4 File Offset: 0x0001A5E4
		public DateTime(int year, int month, int day, int hour, int minute, int second, int millisecond)
		{
			if (millisecond < 0 || millisecond >= 1000)
			{
				throw new ArgumentOutOfRangeException("millisecond", SR.Format("Valid values are between {0} and {1}, inclusive.", 0, 999));
			}
			long num = DateTime.DateToTicks(year, month, day) + DateTime.TimeToTicks(hour, minute, second);
			num += (long)millisecond * 10000L;
			if (num < 0L || num > 3155378975999999999L)
			{
				throw new ArgumentException("Combination of arguments to the DateTime constructor is out of the legal range.");
			}
			this._dateData = (ulong)num;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DateTime" /> structure to the specified year, month, day, hour, minute, second, millisecond, and Coordinated Universal Time (UTC) or local time.</summary>
		/// <param name="year">The year (1 through 9999). </param>
		/// <param name="month">The month (1 through 12). </param>
		/// <param name="day">The day (1 through the number of days in <paramref name="month" />). </param>
		/// <param name="hour">The hours (0 through 23). </param>
		/// <param name="minute">The minutes (0 through 59). </param>
		/// <param name="second">The seconds (0 through 59). </param>
		/// <param name="millisecond">The milliseconds (0 through 999). </param>
		/// <param name="kind">One of the enumeration values that indicates whether <paramref name="year" />, <paramref name="month" />, <paramref name="day" />, <paramref name="hour" />, <paramref name="minute" />, <paramref name="second" />, and <paramref name="millisecond" /> specify a local time, Coordinated Universal Time (UTC), or neither.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is less than 1 or greater than 9999.-or- <paramref name="month" /> is less than 1 or greater than 12.-or- <paramref name="day" /> is less than 1 or greater than the number of days in <paramref name="month" />.-or- <paramref name="hour" /> is less than 0 or greater than 23.-or- <paramref name="minute" /> is less than 0 or greater than 59.-or- <paramref name="second" /> is less than 0 or greater than 59.-or- <paramref name="millisecond" /> is less than 0 or greater than 999. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="kind" /> is not one of the <see cref="T:System.DateTimeKind" /> values.</exception>
		// Token: 0x060006AE RID: 1710 RVA: 0x0001C46C File Offset: 0x0001A66C
		public DateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, DateTimeKind kind)
		{
			if (millisecond < 0 || millisecond >= 1000)
			{
				throw new ArgumentOutOfRangeException("millisecond", SR.Format("Valid values are between {0} and {1}, inclusive.", 0, 999));
			}
			if (kind < DateTimeKind.Unspecified || kind > DateTimeKind.Local)
			{
				throw new ArgumentException("Invalid DateTimeKind value.", "kind");
			}
			long num = DateTime.DateToTicks(year, month, day) + DateTime.TimeToTicks(hour, minute, second);
			num += (long)millisecond * 10000L;
			if (num < 0L || num > 3155378975999999999L)
			{
				throw new ArgumentException("Combination of arguments to the DateTime constructor is out of the legal range.");
			}
			this._dateData = (ulong)(num | ((long)kind << 62));
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DateTime" /> structure to the specified year, month, day, hour, minute, second, and millisecond for the specified calendar.</summary>
		/// <param name="year">The year (1 through the number of years in <paramref name="calendar" />). </param>
		/// <param name="month">The month (1 through the number of months in <paramref name="calendar" />). </param>
		/// <param name="day">The day (1 through the number of days in <paramref name="month" />). </param>
		/// <param name="hour">The hours (0 through 23). </param>
		/// <param name="minute">The minutes (0 through 59). </param>
		/// <param name="second">The seconds (0 through 59). </param>
		/// <param name="millisecond">The milliseconds (0 through 999). </param>
		/// <param name="calendar">The calendar that is used to interpret <paramref name="year" />, <paramref name="month" />, and <paramref name="day" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="calendar" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is not in the range supported by <paramref name="calendar" />.-or- <paramref name="month" /> is less than 1 or greater than the number of months in <paramref name="calendar" />.-or- <paramref name="day" /> is less than 1 or greater than the number of days in <paramref name="month" />.-or- <paramref name="hour" /> is less than 0 or greater than 23.-or- <paramref name="minute" /> is less than 0 or greater than 59.-or- <paramref name="second" /> is less than 0 or greater than 59.-or- <paramref name="millisecond" /> is less than 0 or greater than 999. </exception>
		// Token: 0x060006AF RID: 1711 RVA: 0x0001C514 File Offset: 0x0001A714
		public DateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, Calendar calendar)
		{
			if (calendar == null)
			{
				throw new ArgumentNullException("calendar");
			}
			if (millisecond < 0 || millisecond >= 1000)
			{
				throw new ArgumentOutOfRangeException("millisecond", SR.Format("Valid values are between {0} and {1}, inclusive.", 0, 999));
			}
			long num = calendar.ToDateTime(year, month, day, hour, minute, second, 0).Ticks;
			num += (long)millisecond * 10000L;
			if (num < 0L || num > 3155378975999999999L)
			{
				throw new ArgumentException("Combination of arguments to the DateTime constructor is out of the legal range.");
			}
			this._dateData = (ulong)num;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DateTime" /> structure to the specified year, month, day, hour, minute, second, millisecond, and Coordinated Universal Time (UTC) or local time for the specified calendar.</summary>
		/// <param name="year">The year (1 through the number of years in <paramref name="calendar" />). </param>
		/// <param name="month">The month (1 through the number of months in <paramref name="calendar" />). </param>
		/// <param name="day">The day (1 through the number of days in <paramref name="month" />). </param>
		/// <param name="hour">The hours (0 through 23). </param>
		/// <param name="minute">The minutes (0 through 59). </param>
		/// <param name="second">The seconds (0 through 59). </param>
		/// <param name="millisecond">The milliseconds (0 through 999). </param>
		/// <param name="calendar">The calendar that is used to interpret <paramref name="year" />, <paramref name="month" />, and <paramref name="day" />.</param>
		/// <param name="kind">One of the enumeration values that indicates whether <paramref name="year" />, <paramref name="month" />, <paramref name="day" />, <paramref name="hour" />, <paramref name="minute" />, <paramref name="second" />, and <paramref name="millisecond" /> specify a local time, Coordinated Universal Time (UTC), or neither.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="calendar" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is not in the range supported by <paramref name="calendar" />.-or- <paramref name="month" /> is less than 1 or greater than the number of months in <paramref name="calendar" />.-or- <paramref name="day" /> is less than 1 or greater than the number of days in <paramref name="month" />.-or- <paramref name="hour" /> is less than 0 or greater than 23.-or- <paramref name="minute" /> is less than 0 or greater than 59.-or- <paramref name="second" /> is less than 0 or greater than 59.-or- <paramref name="millisecond" /> is less than 0 or greater than 999. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="kind" /> is not one of the <see cref="T:System.DateTimeKind" /> values.</exception>
		// Token: 0x060006B0 RID: 1712 RVA: 0x0001C5B0 File Offset: 0x0001A7B0
		public DateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, Calendar calendar, DateTimeKind kind)
		{
			if (calendar == null)
			{
				throw new ArgumentNullException("calendar");
			}
			if (millisecond < 0 || millisecond >= 1000)
			{
				throw new ArgumentOutOfRangeException("millisecond", SR.Format("Valid values are between {0} and {1}, inclusive.", 0, 999));
			}
			if (kind < DateTimeKind.Unspecified || kind > DateTimeKind.Local)
			{
				throw new ArgumentException("Invalid DateTimeKind value.", "kind");
			}
			long num = calendar.ToDateTime(year, month, day, hour, minute, second, 0).Ticks;
			num += (long)millisecond * 10000L;
			if (num < 0L || num > 3155378975999999999L)
			{
				throw new ArgumentException("Combination of arguments to the DateTime constructor is out of the legal range.");
			}
			this._dateData = (ulong)(num | ((long)kind << 62));
		}

		// Token: 0x060006B1 RID: 1713 RVA: 0x0001C66C File Offset: 0x0001A86C
		private DateTime(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			bool flag = false;
			bool flag2 = false;
			long num = 0L;
			ulong num2 = 0UL;
			SerializationInfoEnumerator enumerator = info.GetEnumerator();
			while (enumerator.MoveNext())
			{
				string name = enumerator.Name;
				if (!(name == "ticks"))
				{
					if (name == "dateData")
					{
						num2 = Convert.ToUInt64(enumerator.Value, CultureInfo.InvariantCulture);
						flag2 = true;
					}
				}
				else
				{
					num = Convert.ToInt64(enumerator.Value, CultureInfo.InvariantCulture);
					flag = true;
				}
			}
			if (flag2)
			{
				this._dateData = num2;
			}
			else
			{
				if (!flag)
				{
					throw new SerializationException("Invalid serialized DateTime data. Unable to find 'ticks' or 'dateData'.");
				}
				this._dateData = (ulong)num;
			}
			long internalTicks = this.InternalTicks;
			if (internalTicks < 0L || internalTicks > 3155378975999999999L)
			{
				throw new SerializationException("Invalid serialized DateTime data. Ticks must be between DateTime.MinValue.Ticks and DateTime.MaxValue.Ticks.");
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060006B2 RID: 1714 RVA: 0x0001C73E File Offset: 0x0001A93E
		internal long InternalTicks
		{
			get
			{
				return (long)(this._dateData & 4611686018427387903UL);
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060006B3 RID: 1715 RVA: 0x0001C750 File Offset: 0x0001A950
		private ulong InternalKind
		{
			get
			{
				return this._dateData & 13835058055282163712UL;
			}
		}

		/// <summary>Returns a new <see cref="T:System.DateTime" /> that adds the value of the specified <see cref="T:System.TimeSpan" /> to the value of this instance.</summary>
		/// <returns>An object whose value is the sum of the date and time represented by this instance and the time interval represented by <paramref name="value" />.</returns>
		/// <param name="value">A positive or negative time interval. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The resulting <see cref="T:System.DateTime" /> is less than <see cref="F:System.DateTime.MinValue" /> or greater than <see cref="F:System.DateTime.MaxValue" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060006B4 RID: 1716 RVA: 0x0001C762 File Offset: 0x0001A962
		public DateTime Add(TimeSpan value)
		{
			return this.AddTicks(value._ticks);
		}

		// Token: 0x060006B5 RID: 1717 RVA: 0x0001C770 File Offset: 0x0001A970
		private DateTime Add(double value, int scale)
		{
			long num = (long)(value * (double)scale + ((value >= 0.0) ? 0.5 : (-0.5)));
			if (num <= -315537897600000L || num >= 315537897600000L)
			{
				throw new ArgumentOutOfRangeException("value", "Value to add was out of range.");
			}
			return this.AddTicks(num * 10000L);
		}

		/// <summary>Returns a new <see cref="T:System.DateTime" /> that adds the specified number of days to the value of this instance.</summary>
		/// <returns>An object whose value is the sum of the date and time represented by this instance and the number of days represented by <paramref name="value" />.</returns>
		/// <param name="value">A number of whole and fractional days. The <paramref name="value" /> parameter can be negative or positive. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The resulting <see cref="T:System.DateTime" /> is less than <see cref="F:System.DateTime.MinValue" /> or greater than <see cref="F:System.DateTime.MaxValue" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060006B6 RID: 1718 RVA: 0x0001C7DA File Offset: 0x0001A9DA
		public DateTime AddDays(double value)
		{
			return this.Add(value, 86400000);
		}

		/// <summary>Returns a new <see cref="T:System.DateTime" /> that adds the specified number of milliseconds to the value of this instance.</summary>
		/// <returns>An object whose value is the sum of the date and time represented by this instance and the number of milliseconds represented by <paramref name="value" />.</returns>
		/// <param name="value">A number of whole and fractional milliseconds. The <paramref name="value" /> parameter can be negative or positive. Note that this value is rounded to the nearest integer.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The resulting <see cref="T:System.DateTime" /> is less than <see cref="F:System.DateTime.MinValue" /> or greater than <see cref="F:System.DateTime.MaxValue" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060006B7 RID: 1719 RVA: 0x0001C7E8 File Offset: 0x0001A9E8
		public DateTime AddMilliseconds(double value)
		{
			return this.Add(value, 1);
		}

		/// <summary>Returns a new <see cref="T:System.DateTime" /> that adds the specified number of minutes to the value of this instance.</summary>
		/// <returns>An object whose value is the sum of the date and time represented by this instance and the number of minutes represented by <paramref name="value" />.</returns>
		/// <param name="value">A number of whole and fractional minutes. The <paramref name="value" /> parameter can be negative or positive. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The resulting <see cref="T:System.DateTime" /> is less than <see cref="F:System.DateTime.MinValue" /> or greater than <see cref="F:System.DateTime.MaxValue" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060006B8 RID: 1720 RVA: 0x0001C7F2 File Offset: 0x0001A9F2
		public DateTime AddMinutes(double value)
		{
			return this.Add(value, 60000);
		}

		/// <summary>Returns a new <see cref="T:System.DateTime" /> that adds the specified number of months to the value of this instance.</summary>
		/// <returns>An object whose value is the sum of the date and time represented by this instance and <paramref name="months" />.</returns>
		/// <param name="months">A number of months. The <paramref name="months" /> parameter can be negative or positive. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The resulting <see cref="T:System.DateTime" /> is less than <see cref="F:System.DateTime.MinValue" /> or greater than <see cref="F:System.DateTime.MaxValue" />.-or- <paramref name="months" /> is less than -120,000 or greater than 120,000. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060006B9 RID: 1721 RVA: 0x0001C800 File Offset: 0x0001AA00
		public DateTime AddMonths(int months)
		{
			if (months < -120000 || months > 120000)
			{
				throw new ArgumentOutOfRangeException("months", "Months value must be between +/-120000.");
			}
			int num;
			int num2;
			int num3;
			this.GetDatePart(out num, out num2, out num3);
			int num4 = num2 - 1 + months;
			if (num4 >= 0)
			{
				num2 = num4 % 12 + 1;
				num += num4 / 12;
			}
			else
			{
				num2 = 12 + (num4 + 1) % 12;
				num += (num4 - 11) / 12;
			}
			if (num < 1 || num > 9999)
			{
				throw new ArgumentOutOfRangeException("months", "The added or subtracted value results in an un-representable DateTime.");
			}
			int num5 = DateTime.DaysInMonth(num, num2);
			if (num3 > num5)
			{
				num3 = num5;
			}
			return new DateTime((ulong)((DateTime.DateToTicks(num, num2, num3) + this.InternalTicks % 864000000000L) | (long)this.InternalKind));
		}

		/// <summary>Returns a new <see cref="T:System.DateTime" /> that adds the specified number of seconds to the value of this instance.</summary>
		/// <returns>An object whose value is the sum of the date and time represented by this instance and the number of seconds represented by <paramref name="value" />.</returns>
		/// <param name="value">A number of whole and fractional seconds. The <paramref name="value" /> parameter can be negative or positive. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The resulting <see cref="T:System.DateTime" /> is less than <see cref="F:System.DateTime.MinValue" /> or greater than <see cref="F:System.DateTime.MaxValue" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060006BA RID: 1722 RVA: 0x0001C8B9 File Offset: 0x0001AAB9
		public DateTime AddSeconds(double value)
		{
			return this.Add(value, 1000);
		}

		/// <summary>Returns a new <see cref="T:System.DateTime" /> that adds the specified number of ticks to the value of this instance.</summary>
		/// <returns>An object whose value is the sum of the date and time represented by this instance and the time represented by <paramref name="value" />.</returns>
		/// <param name="value">A number of 100-nanosecond ticks. The <paramref name="value" /> parameter can be positive or negative. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The resulting <see cref="T:System.DateTime" /> is less than <see cref="F:System.DateTime.MinValue" /> or greater than <see cref="F:System.DateTime.MaxValue" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060006BB RID: 1723 RVA: 0x0001C8C8 File Offset: 0x0001AAC8
		public DateTime AddTicks(long value)
		{
			long internalTicks = this.InternalTicks;
			if (value > 3155378975999999999L - internalTicks || value < 0L - internalTicks)
			{
				throw new ArgumentOutOfRangeException("value", "The added or subtracted value results in an un-representable DateTime.");
			}
			return new DateTime((ulong)((internalTicks + value) | (long)this.InternalKind));
		}

		/// <summary>Returns a new <see cref="T:System.DateTime" /> that adds the specified number of years to the value of this instance.</summary>
		/// <returns>An object whose value is the sum of the date and time represented by this instance and the number of years represented by <paramref name="value" />.</returns>
		/// <param name="value">A number of years. The <paramref name="value" /> parameter can be negative or positive. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="value" /> or the resulting <see cref="T:System.DateTime" /> is less than <see cref="F:System.DateTime.MinValue" /> or greater than <see cref="F:System.DateTime.MaxValue" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060006BC RID: 1724 RVA: 0x0001C910 File Offset: 0x0001AB10
		public DateTime AddYears(int value)
		{
			if (value < -10000 || value > 10000)
			{
				throw new ArgumentOutOfRangeException("years", "Years value must be between +/-10000.");
			}
			return this.AddMonths(value * 12);
		}

		/// <summary>Compares two instances of <see cref="T:System.DateTime" /> and returns an integer that indicates whether the first instance is earlier than, the same as, or later than the second instance.</summary>
		/// <returns>A signed number indicating the relative values of <paramref name="t1" /> and <paramref name="t2" />.Value Type Condition Less than zero <paramref name="t1" /> is earlier than <paramref name="t2" />. Zero <paramref name="t1" /> is the same as <paramref name="t2" />. Greater than zero <paramref name="t1" /> is later than <paramref name="t2" />. </returns>
		/// <param name="t1">The first object to compare. </param>
		/// <param name="t2">The second object to compare. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060006BD RID: 1725 RVA: 0x0001C93C File Offset: 0x0001AB3C
		public static int Compare(DateTime t1, DateTime t2)
		{
			long internalTicks = t1.InternalTicks;
			long internalTicks2 = t2.InternalTicks;
			if (internalTicks > internalTicks2)
			{
				return 1;
			}
			if (internalTicks < internalTicks2)
			{
				return -1;
			}
			return 0;
		}

		/// <summary>Compares the value of this instance to a specified object that contains a specified <see cref="T:System.DateTime" /> value, and returns an integer that indicates whether this instance is earlier than, the same as, or later than the specified <see cref="T:System.DateTime" /> value.</summary>
		/// <returns>A signed number indicating the relative values of this instance and <paramref name="value" />.Value Description Less than zero This instance is earlier than <paramref name="value" />. Zero This instance is the same as <paramref name="value" />. Greater than zero This instance is later than <paramref name="value" />, or <paramref name="value" /> is null. </returns>
		/// <param name="value">A boxed object to compare, or null. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> is not a <see cref="T:System.DateTime" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060006BE RID: 1726 RVA: 0x0001C966 File Offset: 0x0001AB66
		public int CompareTo(object value)
		{
			if (value == null)
			{
				return 1;
			}
			if (!(value is DateTime))
			{
				throw new ArgumentException("Object must be of type DateTime.");
			}
			return DateTime.Compare(this, (DateTime)value);
		}

		/// <summary>Compares the value of this instance to a specified <see cref="T:System.DateTime" /> value and returns an integer that indicates whether this instance is earlier than, the same as, or later than the specified <see cref="T:System.DateTime" /> value.</summary>
		/// <returns>A signed number indicating the relative values of this instance and the <paramref name="value" /> parameter.Value Description Less than zero This instance is earlier than <paramref name="value" />. Zero This instance is the same as <paramref name="value" />. Greater than zero This instance is later than <paramref name="value" />. </returns>
		/// <param name="value">The object to compare to the current instance. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060006BF RID: 1727 RVA: 0x0001C991 File Offset: 0x0001AB91
		public int CompareTo(DateTime value)
		{
			return DateTime.Compare(this, value);
		}

		// Token: 0x060006C0 RID: 1728 RVA: 0x0001C9A0 File Offset: 0x0001ABA0
		private static long DateToTicks(int year, int month, int day)
		{
			if (year >= 1 && year <= 9999 && month >= 1 && month <= 12)
			{
				int[] array = (DateTime.IsLeapYear(year) ? DateTime.s_daysToMonth366 : DateTime.s_daysToMonth365);
				if (day >= 1 && day <= array[month] - array[month - 1])
				{
					int num = year - 1;
					return (long)(num * 365 + num / 4 - num / 100 + num / 400 + array[month - 1] + day - 1) * 864000000000L;
				}
			}
			throw new ArgumentOutOfRangeException(null, "Year, Month, and Day parameters describe an un-representable DateTime.");
		}

		// Token: 0x060006C1 RID: 1729 RVA: 0x0001CA24 File Offset: 0x0001AC24
		private static long TimeToTicks(int hour, int minute, int second)
		{
			if (hour >= 0 && hour < 24 && minute >= 0 && minute < 60 && second >= 0 && second < 60)
			{
				return TimeSpan.TimeToTicks(hour, minute, second);
			}
			throw new ArgumentOutOfRangeException(null, "Hour, Minute, and Second parameters describe an un-representable DateTime.");
		}

		/// <summary>Returns the number of days in the specified month and year.</summary>
		/// <returns>The number of days in <paramref name="month" /> for the specified <paramref name="year" />.For example, if <paramref name="month" /> equals 2 for February, the return value is 28 or 29 depending upon whether <paramref name="year" /> is a leap year.</returns>
		/// <param name="year">The year. </param>
		/// <param name="month">The month (a number ranging from 1 to 12). </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="month" /> is less than 1 or greater than 12.-or-<paramref name="year" /> is less than 1 or greater than 9999.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060006C2 RID: 1730 RVA: 0x0001CA58 File Offset: 0x0001AC58
		public static int DaysInMonth(int year, int month)
		{
			if (month < 1 || month > 12)
			{
				throw new ArgumentOutOfRangeException("month", "Month must be between one and twelve.");
			}
			int[] array = (DateTime.IsLeapYear(year) ? DateTime.s_daysToMonth366 : DateTime.s_daysToMonth365);
			return array[month] - array[month - 1];
		}

		// Token: 0x060006C3 RID: 1731 RVA: 0x0001CA9C File Offset: 0x0001AC9C
		internal static long DoubleDateToTicks(double value)
		{
			if (value >= 2958466.0 || value <= -657435.0)
			{
				throw new ArgumentException(" Not a legal OleAut date.");
			}
			long num = (long)(value * 86400000.0 + ((value >= 0.0) ? 0.5 : (-0.5)));
			if (num < 0L)
			{
				num -= num % 86400000L * 2L;
			}
			num += 59926435200000L;
			if (num < 0L || num >= 315537897600000L)
			{
				throw new ArgumentException("OleAut date did not convert to a DateTime correctly.");
			}
			return num * 10000L;
		}

		/// <summary>Returns a value indicating whether this instance is equal to a specified object.</summary>
		/// <returns>true if <paramref name="value" /> is an instance of <see cref="T:System.DateTime" /> and equals the value of this instance; otherwise, false.</returns>
		/// <param name="value">The object to compare to this instance. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060006C4 RID: 1732 RVA: 0x0001CB40 File Offset: 0x0001AD40
		public override bool Equals(object value)
		{
			return value is DateTime && this.InternalTicks == ((DateTime)value).InternalTicks;
		}

		/// <summary>Returns a value indicating whether the value of this instance is equal to the value of the specified <see cref="T:System.DateTime" /> instance.</summary>
		/// <returns>true if the <paramref name="value" /> parameter equals the value of this instance; otherwise, false.</returns>
		/// <param name="value">The object to compare to this instance. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060006C5 RID: 1733 RVA: 0x0001CB6D File Offset: 0x0001AD6D
		public bool Equals(DateTime value)
		{
			return this.InternalTicks == value.InternalTicks;
		}

		/// <summary>Deserializes a 64-bit binary value and recreates an original serialized <see cref="T:System.DateTime" /> object.</summary>
		/// <returns>An object that is equivalent to the <see cref="T:System.DateTime" /> object that was serialized by the <see cref="M:System.DateTime.ToBinary" /> method.</returns>
		/// <param name="dateData">A 64-bit signed integer that encodes the <see cref="P:System.DateTime.Kind" /> property in a 2-bit field and the <see cref="P:System.DateTime.Ticks" /> property in a 62-bit field. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="dateData" /> is less than <see cref="F:System.DateTime.MinValue" /> or greater than <see cref="F:System.DateTime.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060006C6 RID: 1734 RVA: 0x0001CB80 File Offset: 0x0001AD80
		public static DateTime FromBinary(long dateData)
		{
			if ((dateData & -9223372036854775808L) == 0L)
			{
				return DateTime.FromBinaryRaw(dateData);
			}
			long num = dateData & 4611686018427387903L;
			if (num > 4611685154427387904L)
			{
				num -= 4611686018427387904L;
			}
			bool flag = false;
			long num2;
			if (num < 0L)
			{
				num2 = TimeZoneInfo.GetLocalUtcOffset(DateTime.MinValue, TimeZoneInfoOptions.NoThrowOnInvalidTime).Ticks;
			}
			else if (num > 3155378975999999999L)
			{
				num2 = TimeZoneInfo.GetLocalUtcOffset(DateTime.MaxValue, TimeZoneInfoOptions.NoThrowOnInvalidTime).Ticks;
			}
			else
			{
				DateTime dateTime = new DateTime(num, DateTimeKind.Utc);
				bool flag2 = false;
				num2 = TimeZoneInfo.GetUtcOffsetFromUtc(dateTime, TimeZoneInfo.Local, out flag2, out flag).Ticks;
			}
			num += num2;
			if (num < 0L)
			{
				num += 864000000000L;
			}
			if (num < 0L || num > 3155378975999999999L)
			{
				throw new ArgumentException("The binary data must result in a DateTime with ticks between DateTime.MinValue.Ticks and DateTime.MaxValue.Ticks.", "dateData");
			}
			return new DateTime(num, DateTimeKind.Local, flag);
		}

		// Token: 0x060006C7 RID: 1735 RVA: 0x0001CC68 File Offset: 0x0001AE68
		internal static DateTime FromBinaryRaw(long dateData)
		{
			long num = dateData & 4611686018427387903L;
			if (num < 0L || num > 3155378975999999999L)
			{
				throw new ArgumentException("The binary data must result in a DateTime with ticks between DateTime.MinValue.Ticks and DateTime.MaxValue.Ticks.", "dateData");
			}
			return new DateTime((ulong)dateData);
		}

		/// <summary>Converts the specified Windows file time to an equivalent local time.</summary>
		/// <returns>An object that represents the local time equivalent of the date and time represented by the <paramref name="fileTime" /> parameter.</returns>
		/// <param name="fileTime">A Windows file time expressed in ticks. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="fileTime" /> is less than 0 or represents a time greater than <see cref="F:System.DateTime.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060006C8 RID: 1736 RVA: 0x0001CCA8 File Offset: 0x0001AEA8
		public static DateTime FromFileTime(long fileTime)
		{
			return DateTime.FromFileTimeUtc(fileTime).ToLocalTime();
		}

		/// <summary>Converts the specified Windows file time to an equivalent UTC time.</summary>
		/// <returns>An object that represents the UTC time equivalent of the date and time represented by the <paramref name="fileTime" /> parameter.</returns>
		/// <param name="fileTime">A Windows file time expressed in ticks. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="fileTime" /> is less than 0 or represents a time greater than <see cref="F:System.DateTime.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060006C9 RID: 1737 RVA: 0x0001CCC3 File Offset: 0x0001AEC3
		public static DateTime FromFileTimeUtc(long fileTime)
		{
			if (fileTime < 0L || fileTime > 2650467743999999999L)
			{
				throw new ArgumentOutOfRangeException("fileTime", "Not a valid Win32 FileTime.");
			}
			return new DateTime(fileTime + 504911232000000000L, DateTimeKind.Utc);
		}

		/// <summary>Returns a <see cref="T:System.DateTime" /> equivalent to the specified OLE Automation Date.</summary>
		/// <returns>An object that represents the same date and time as <paramref name="d" />.</returns>
		/// <param name="d">An OLE Automation Date value. </param>
		/// <exception cref="T:System.ArgumentException">The date is not a valid OLE Automation Date value. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060006CA RID: 1738 RVA: 0x0001CCF7 File Offset: 0x0001AEF7
		public static DateTime FromOADate(double d)
		{
			return new DateTime(DateTime.DoubleDateToTicks(d), DateTimeKind.Unspecified);
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the data needed to serialize the current <see cref="T:System.DateTime" /> object.</summary>
		/// <param name="info">The object to populate with data. </param>
		/// <param name="context">The destination for this serialization. (This parameter is not used; specify null.)</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is null.</exception>
		// Token: 0x060006CB RID: 1739 RVA: 0x0001CD05 File Offset: 0x0001AF05
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("ticks", this.InternalTicks);
			info.AddValue("dateData", this._dateData);
		}

		/// <summary>Indicates whether this instance of <see cref="T:System.DateTime" /> is within the daylight saving time range for the current time zone.</summary>
		/// <returns>true if the value of the <see cref="P:System.DateTime.Kind" /> property is <see cref="F:System.DateTimeKind.Local" /> or <see cref="F:System.DateTimeKind.Unspecified" /> and the value of this instance of <see cref="T:System.DateTime" /> is within the daylight saving time range for the local time zone; false if <see cref="P:System.DateTime.Kind" /> is <see cref="F:System.DateTimeKind.Utc" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060006CC RID: 1740 RVA: 0x0001CD37 File Offset: 0x0001AF37
		public bool IsDaylightSavingTime()
		{
			return this.Kind != DateTimeKind.Utc && TimeZoneInfo.Local.IsDaylightSavingTime(this, TimeZoneInfoOptions.NoThrowOnInvalidTime);
		}

		/// <summary>Creates a new <see cref="T:System.DateTime" /> object that has the same number of ticks as the specified <see cref="T:System.DateTime" />, but is designated as either local time, Coordinated Universal Time (UTC), or neither, as indicated by the specified <see cref="T:System.DateTimeKind" /> value.</summary>
		/// <returns>A new object that has the same number of ticks as the object represented by the <paramref name="value" /> parameter and the <see cref="T:System.DateTimeKind" /> value specified by the <paramref name="kind" /> parameter. </returns>
		/// <param name="value">A date and time. </param>
		/// <param name="kind">One of the enumeration values that indicates whether the new object represents local time, UTC, or neither.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060006CD RID: 1741 RVA: 0x0001CD55 File Offset: 0x0001AF55
		public static DateTime SpecifyKind(DateTime value, DateTimeKind kind)
		{
			return new DateTime(value.InternalTicks, kind);
		}

		/// <summary>Serializes the current <see cref="T:System.DateTime" /> object to a 64-bit binary value that subsequently can be used to recreate the <see cref="T:System.DateTime" /> object.</summary>
		/// <returns>A 64-bit signed integer that encodes the <see cref="P:System.DateTime.Kind" /> and <see cref="P:System.DateTime.Ticks" /> properties. </returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060006CE RID: 1742 RVA: 0x0001CD64 File Offset: 0x0001AF64
		public long ToBinary()
		{
			if (this.Kind == DateTimeKind.Local)
			{
				TimeSpan localUtcOffset = TimeZoneInfo.GetLocalUtcOffset(this, TimeZoneInfoOptions.NoThrowOnInvalidTime);
				long num = this.Ticks - localUtcOffset.Ticks;
				if (num < 0L)
				{
					num = 4611686018427387904L + num;
				}
				return num | long.MinValue;
			}
			return (long)this._dateData;
		}

		/// <summary>Gets the date component of this instance.</summary>
		/// <returns>A new object with the same date as this instance, and the time value set to 12:00:00 midnight (00:00:00).</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060006CF RID: 1743 RVA: 0x0001CDB9 File Offset: 0x0001AFB9
		public DateTime Date
		{
			get
			{
				long internalTicks = this.InternalTicks;
				return new DateTime((ulong)((internalTicks - internalTicks % 864000000000L) | (long)this.InternalKind));
			}
		}

		// Token: 0x060006D0 RID: 1744 RVA: 0x0001CDDC File Offset: 0x0001AFDC
		private int GetDatePart(int part)
		{
			int i = (int)(this.InternalTicks / 864000000000L);
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
			int[] array = ((num4 == 3 && (num3 != 24 || num2 == 3)) ? DateTime.s_daysToMonth366 : DateTime.s_daysToMonth365);
			int num5 = (i >> 5) + 1;
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

		// Token: 0x060006D1 RID: 1745 RVA: 0x0001CEC4 File Offset: 0x0001B0C4
		internal void GetDatePart(out int year, out int month, out int day)
		{
			int i = (int)(this.InternalTicks / 864000000000L);
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
			year = num * 400 + num2 * 100 + num3 * 4 + num4 + 1;
			i -= num4 * 365;
			int[] array = ((num4 == 3 && (num3 != 24 || num2 == 3)) ? DateTime.s_daysToMonth366 : DateTime.s_daysToMonth365);
			int num5 = (i >> 5) + 1;
			while (i >= array[num5])
			{
				num5++;
			}
			month = num5;
			day = i - array[num5 - 1] + 1;
		}

		/// <summary>Gets the day of the month represented by this instance.</summary>
		/// <returns>The day component, expressed as a value between 1 and 31.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060006D2 RID: 1746 RVA: 0x0001CF9E File Offset: 0x0001B19E
		public int Day
		{
			get
			{
				return this.GetDatePart(3);
			}
		}

		/// <summary>Gets the day of the week represented by this instance.</summary>
		/// <returns>An enumerated constant that indicates the day of the week of this <see cref="T:System.DateTime" /> value. </returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060006D3 RID: 1747 RVA: 0x0001CFA7 File Offset: 0x0001B1A7
		public DayOfWeek DayOfWeek
		{
			get
			{
				return (DayOfWeek)((this.InternalTicks / 864000000000L + 1L) % 7L);
			}
		}

		/// <summary>Gets the day of the year represented by this instance.</summary>
		/// <returns>The day of the year, expressed as a value between 1 and 366.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060006D4 RID: 1748 RVA: 0x0001CFC0 File Offset: 0x0001B1C0
		public int DayOfYear
		{
			get
			{
				return this.GetDatePart(1);
			}
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060006D5 RID: 1749 RVA: 0x0001CFCC File Offset: 0x0001B1CC
		public override int GetHashCode()
		{
			long internalTicks = this.InternalTicks;
			return (int)internalTicks ^ (int)(internalTicks >> 32);
		}

		/// <summary>Gets the hour component of the date represented by this instance.</summary>
		/// <returns>The hour component, expressed as a value between 0 and 23.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060006D6 RID: 1750 RVA: 0x0001CFE8 File Offset: 0x0001B1E8
		public int Hour
		{
			get
			{
				return (int)(this.InternalTicks / 36000000000L % 24L);
			}
		}

		// Token: 0x060006D7 RID: 1751 RVA: 0x0001CFFF File Offset: 0x0001B1FF
		internal bool IsAmbiguousDaylightSavingTime()
		{
			return this.InternalKind == 13835058055282163712UL;
		}

		/// <summary>Gets a value that indicates whether the time represented by this instance is based on local time, Coordinated Universal Time (UTC), or neither.</summary>
		/// <returns>One of the enumeration values that indicates what the current time represents. The default is <see cref="F:System.DateTimeKind.Unspecified" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060006D8 RID: 1752 RVA: 0x0001D014 File Offset: 0x0001B214
		public DateTimeKind Kind
		{
			get
			{
				ulong internalKind = this.InternalKind;
				if (internalKind == 0UL)
				{
					return DateTimeKind.Unspecified;
				}
				if (internalKind != 4611686018427387904UL)
				{
					return DateTimeKind.Local;
				}
				return DateTimeKind.Utc;
			}
		}

		/// <summary>Gets the milliseconds component of the date represented by this instance.</summary>
		/// <returns>The milliseconds component, expressed as a value between 0 and 999.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060006D9 RID: 1753 RVA: 0x0001D03E File Offset: 0x0001B23E
		public int Millisecond
		{
			get
			{
				return (int)(this.InternalTicks / 10000L % 1000L);
			}
		}

		/// <summary>Gets the minute component of the date represented by this instance.</summary>
		/// <returns>The minute component, expressed as a value between 0 and 59.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060006DA RID: 1754 RVA: 0x0001D055 File Offset: 0x0001B255
		public int Minute
		{
			get
			{
				return (int)(this.InternalTicks / 600000000L % 60L);
			}
		}

		/// <summary>Gets the month component of the date represented by this instance.</summary>
		/// <returns>The month component, expressed as a value between 1 and 12.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060006DB RID: 1755 RVA: 0x0001D069 File Offset: 0x0001B269
		public int Month
		{
			get
			{
				return this.GetDatePart(2);
			}
		}

		/// <summary>Gets a <see cref="T:System.DateTime" /> object that is set to the current date and time on this computer, expressed as the local time.</summary>
		/// <returns>An object whose value is the current local date and time.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060006DC RID: 1756 RVA: 0x0001D074 File Offset: 0x0001B274
		public static DateTime Now
		{
			get
			{
				DateTime utcNow = DateTime.UtcNow;
				bool flag = false;
				long ticks = TimeZoneInfo.GetDateTimeNowUtcOffsetFromUtc(utcNow, out flag).Ticks;
				long num = utcNow.Ticks + ticks;
				if (num > 3155378975999999999L)
				{
					return new DateTime(3155378975999999999L, DateTimeKind.Local);
				}
				if (num < 0L)
				{
					return new DateTime(0L, DateTimeKind.Local);
				}
				return new DateTime(num, DateTimeKind.Local, flag);
			}
		}

		/// <summary>Gets the seconds component of the date represented by this instance.</summary>
		/// <returns>The seconds component, expressed as a value between 0 and 59.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060006DD RID: 1757 RVA: 0x0001D0D7 File Offset: 0x0001B2D7
		public int Second
		{
			get
			{
				return (int)(this.InternalTicks / 10000000L % 60L);
			}
		}

		/// <summary>Gets the number of ticks that represent the date and time of this instance.</summary>
		/// <returns>The number of ticks that represent the date and time of this instance. The value is between DateTime.MinValue.Ticks and DateTime.MaxValue.Ticks.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060006DE RID: 1758 RVA: 0x0001D0EB File Offset: 0x0001B2EB
		public long Ticks
		{
			get
			{
				return this.InternalTicks;
			}
		}

		/// <summary>Gets the time of day for this instance.</summary>
		/// <returns>A time interval that represents the fraction of the day that has elapsed since midnight.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060006DF RID: 1759 RVA: 0x0001D0F3 File Offset: 0x0001B2F3
		public TimeSpan TimeOfDay
		{
			get
			{
				return new TimeSpan(this.InternalTicks % 864000000000L);
			}
		}

		/// <summary>Gets the year component of the date represented by this instance.</summary>
		/// <returns>The year, between 1 and 9999.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060006E0 RID: 1760 RVA: 0x0001D10A File Offset: 0x0001B30A
		public int Year
		{
			get
			{
				return this.GetDatePart(0);
			}
		}

		/// <summary>Returns an indication whether the specified year is a leap year.</summary>
		/// <returns>true if <paramref name="year" /> is a leap year; otherwise, false.</returns>
		/// <param name="year">A 4-digit year. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is less than 1 or greater than 9999.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060006E1 RID: 1761 RVA: 0x0001D113 File Offset: 0x0001B313
		public static bool IsLeapYear(int year)
		{
			if (year < 1 || year > 9999)
			{
				throw new ArgumentOutOfRangeException("year", "Year must be between 1 and 9999.");
			}
			return year % 4 == 0 && (year % 100 != 0 || year % 400 == 0);
		}

		/// <summary>Converts the string representation of a date and time to its <see cref="T:System.DateTime" /> equivalent by using culture-specific format information.</summary>
		/// <returns>An object that is equivalent to the date and time contained in <paramref name="s" /> as specified by <paramref name="provider" />.</returns>
		/// <param name="s">A string that contains a date and time to convert. </param>
		/// <param name="provider">An object that supplies culture-specific format information about <paramref name="s" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> does not contain a valid string representation of a date and time. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060006E2 RID: 1762 RVA: 0x0001D14A File Offset: 0x0001B34A
		public static DateTime Parse(string s, IFormatProvider provider)
		{
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			return DateTimeParse.Parse(s, DateTimeFormatInfo.GetInstance(provider), DateTimeStyles.None);
		}

		/// <summary>Converts the string representation of a date and time to its <see cref="T:System.DateTime" /> equivalent by using culture-specific format information and formatting style.</summary>
		/// <returns>An object that is equivalent to the date and time contained in <paramref name="s" />, as specified by <paramref name="provider" /> and <paramref name="styles" />.</returns>
		/// <param name="s">A string that contains a date and time to convert. </param>
		/// <param name="provider">An object that supplies culture-specific formatting information about <paramref name="s" />. </param>
		/// <param name="styles">A bitwise combination of the enumeration values that indicates the style elements that can be present in <paramref name="s" /> for the parse operation to succeed, and that defines how to interpret the parsed date in relation to the current time zone or the current date. A typical value to specify is <see cref="F:System.Globalization.DateTimeStyles.None" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> does not contain a valid string representation of a date and time. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="styles" /> contains an invalid combination of <see cref="T:System.Globalization.DateTimeStyles" /> values. For example, both <see cref="F:System.Globalization.DateTimeStyles.AssumeLocal" /> and <see cref="F:System.Globalization.DateTimeStyles.AssumeUniversal" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060006E3 RID: 1763 RVA: 0x0001D168 File Offset: 0x0001B368
		public static DateTime Parse(string s, IFormatProvider provider, DateTimeStyles styles)
		{
			DateTimeFormatInfo.ValidateStyles(styles, "styles");
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			return DateTimeParse.Parse(s, DateTimeFormatInfo.GetInstance(provider), styles);
		}

		/// <summary>Converts the specified string representation of a date and time to its <see cref="T:System.DateTime" /> equivalent using the specified format and culture-specific format information. The format of the string representation must match the specified format exactly.</summary>
		/// <returns>An object that is equivalent to the date and time contained in <paramref name="s" />, as specified by <paramref name="format" /> and <paramref name="provider" />.</returns>
		/// <param name="s">A string that contains a date and time to convert. </param>
		/// <param name="format">A format specifier that defines the required format of <paramref name="s" />. For more information, see the Remarks section. </param>
		/// <param name="provider">An object that supplies culture-specific format information about <paramref name="s" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> or <paramref name="format" /> is null. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> or <paramref name="format" /> is an empty string. -or- <paramref name="s" /> does not contain a date and time that corresponds to the pattern specified in <paramref name="format" />. -or-The hour component and the AM/PM designator in <paramref name="s" /> do not agree.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060006E4 RID: 1764 RVA: 0x0001D191 File Offset: 0x0001B391
		public static DateTime ParseExact(string s, string format, IFormatProvider provider)
		{
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			if (format == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.format);
			}
			return DateTimeParse.ParseExact(s, format, DateTimeFormatInfo.GetInstance(provider), DateTimeStyles.None);
		}

		/// <summary>Converts the specified string representation of a date and time to its <see cref="T:System.DateTime" /> equivalent using the specified format, culture-specific format information, and style. The format of the string representation must match the specified format exactly or an exception is thrown.</summary>
		/// <returns>An object that is equivalent to the date and time contained in <paramref name="s" />, as specified by <paramref name="format" />, <paramref name="provider" />, and <paramref name="style" />.</returns>
		/// <param name="s">A string containing a date and time to convert. </param>
		/// <param name="format">A format specifier that defines the required format of <paramref name="s" />. For more information, see the Remarks section. </param>
		/// <param name="provider">An object that supplies culture-specific formatting information about <paramref name="s" />. </param>
		/// <param name="style">A bitwise combination of the enumeration values that provides additional information about <paramref name="s" />, about style elements that may be present in <paramref name="s" />, or about the conversion from <paramref name="s" /> to a <see cref="T:System.DateTime" /> value. A typical value to specify is <see cref="F:System.Globalization.DateTimeStyles.None" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> or <paramref name="format" /> is null. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> or <paramref name="format" /> is an empty string. -or- <paramref name="s" /> does not contain a date and time that corresponds to the pattern specified in <paramref name="format" />. -or-The hour component and the AM/PM designator in <paramref name="s" /> do not agree.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="style" /> contains an invalid combination of <see cref="T:System.Globalization.DateTimeStyles" /> values. For example, both <see cref="F:System.Globalization.DateTimeStyles.AssumeLocal" /> and <see cref="F:System.Globalization.DateTimeStyles.AssumeUniversal" />.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060006E5 RID: 1765 RVA: 0x0001D1BF File Offset: 0x0001B3BF
		public static DateTime ParseExact(string s, string format, IFormatProvider provider, DateTimeStyles style)
		{
			DateTimeFormatInfo.ValidateStyles(style, "style");
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			if (format == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.format);
			}
			return DateTimeParse.ParseExact(s, format, DateTimeFormatInfo.GetInstance(provider), style);
		}

		/// <summary>Converts the specified string representation of a date and time to its <see cref="T:System.DateTime" /> equivalent using the specified array of formats, culture-specific format information, and style. The format of the string representation must match at least one of the specified formats exactly or an exception is thrown.</summary>
		/// <returns>An object that is equivalent to the date and time contained in <paramref name="s" />, as specified by <paramref name="formats" />, <paramref name="provider" />, and <paramref name="style" />.</returns>
		/// <param name="s">A string that contains a date and time to convert. </param>
		/// <param name="formats">An array of allowable formats of <paramref name="s" />. For more information, see the Remarks section. </param>
		/// <param name="provider">An object that supplies culture-specific format information about <paramref name="s" />. </param>
		/// <param name="style">A bitwise combination of enumeration values that indicates the permitted format of <paramref name="s" />. A typical value to specify is <see cref="F:System.Globalization.DateTimeStyles.None" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> or <paramref name="formats" /> is null. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> is an empty string. -or- an element of <paramref name="formats" /> is an empty string. -or- <paramref name="s" /> does not contain a date and time that corresponds to any element of <paramref name="formats" />. -or-The hour component and the AM/PM designator in <paramref name="s" /> do not agree.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="style" /> contains an invalid combination of <see cref="T:System.Globalization.DateTimeStyles" /> values. For example, both <see cref="F:System.Globalization.DateTimeStyles.AssumeLocal" /> and <see cref="F:System.Globalization.DateTimeStyles.AssumeUniversal" />.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060006E6 RID: 1766 RVA: 0x0001D1F8 File Offset: 0x0001B3F8
		public static DateTime ParseExact(string s, string[] formats, IFormatProvider provider, DateTimeStyles style)
		{
			DateTimeFormatInfo.ValidateStyles(style, "style");
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			return DateTimeParse.ParseExactMultiple(s, formats, DateTimeFormatInfo.GetInstance(provider), style);
		}

		/// <summary>Subtracts the specified date and time from this instance.</summary>
		/// <returns>A time interval that is equal to the date and time represented by this instance minus the date and time represented by <paramref name="value" />.</returns>
		/// <param name="value">The date and time value to subtract. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The result is less than <see cref="F:System.DateTime.MinValue" /> or greater than <see cref="F:System.DateTime.MaxValue" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060006E7 RID: 1767 RVA: 0x0001D222 File Offset: 0x0001B422
		public TimeSpan Subtract(DateTime value)
		{
			return new TimeSpan(this.InternalTicks - value.InternalTicks);
		}

		// Token: 0x060006E8 RID: 1768 RVA: 0x0001D238 File Offset: 0x0001B438
		private static double TicksToOADate(long value)
		{
			if (value == 0L)
			{
				return 0.0;
			}
			if (value < 864000000000L)
			{
				value += 599264352000000000L;
			}
			if (value < 31241376000000000L)
			{
				throw new OverflowException(" Not a legal OleAut date.");
			}
			long num = (value - 599264352000000000L) / 10000L;
			if (num < 0L)
			{
				long num2 = num % 86400000L;
				if (num2 != 0L)
				{
					num -= (86400000L + num2) * 2L;
				}
			}
			return (double)num / 86400000.0;
		}

		/// <summary>Converts the value of this instance to the equivalent OLE Automation date.</summary>
		/// <returns>A double-precision floating-point number that contains an OLE Automation date equivalent to the value of this instance.</returns>
		/// <exception cref="T:System.OverflowException">The value of this instance cannot be represented as an OLE Automation Date. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060006E9 RID: 1769 RVA: 0x0001D2C0 File Offset: 0x0001B4C0
		public double ToOADate()
		{
			return DateTime.TicksToOADate(this.InternalTicks);
		}

		/// <summary>Converts the value of the current <see cref="T:System.DateTime" /> object to a Windows file time.</summary>
		/// <returns>The value of the current <see cref="T:System.DateTime" /> object expressed as a Windows file time.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The resulting file time would represent a date and time before 12:00 midnight January 1, 1601 C.E. UTC. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060006EA RID: 1770 RVA: 0x0001D2D0 File Offset: 0x0001B4D0
		public long ToFileTime()
		{
			return this.ToUniversalTime().ToFileTimeUtc();
		}

		/// <summary>Converts the value of the current <see cref="T:System.DateTime" /> object to a Windows file time.</summary>
		/// <returns>The value of the current <see cref="T:System.DateTime" /> object expressed as a Windows file time.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The resulting file time would represent a date and time before 12:00 midnight January 1, 1601 C.E. UTC. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060006EB RID: 1771 RVA: 0x0001D2EC File Offset: 0x0001B4EC
		public long ToFileTimeUtc()
		{
			long num = (((this.InternalKind & 9223372036854775808UL) != 0UL) ? this.ToUniversalTime().InternalTicks : this.InternalTicks) - 504911232000000000L;
			if (num < 0L)
			{
				throw new ArgumentOutOfRangeException(null, "Not a valid Win32 FileTime.");
			}
			return num;
		}

		/// <summary>Converts the value of the current <see cref="T:System.DateTime" /> object to local time.</summary>
		/// <returns>An object whose <see cref="P:System.DateTime.Kind" /> property is <see cref="F:System.DateTimeKind.Local" />, and whose value is the local time equivalent to the value of the current <see cref="T:System.DateTime" /> object, or <see cref="F:System.DateTime.MaxValue" /> if the converted value is too large to be represented by a <see cref="T:System.DateTime" /> object, or <see cref="F:System.DateTime.MinValue" /> if the converted value is too small to be represented as a <see cref="T:System.DateTime" /> object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060006EC RID: 1772 RVA: 0x0001D33C File Offset: 0x0001B53C
		public DateTime ToLocalTime()
		{
			return this.ToLocalTime(false);
		}

		// Token: 0x060006ED RID: 1773 RVA: 0x0001D348 File Offset: 0x0001B548
		internal DateTime ToLocalTime(bool throwOnOverflow)
		{
			if (this.Kind == DateTimeKind.Local)
			{
				return this;
			}
			bool flag = false;
			bool flag2 = false;
			long ticks = TimeZoneInfo.GetUtcOffsetFromUtc(this, TimeZoneInfo.Local, out flag, out flag2).Ticks;
			long num = this.Ticks + ticks;
			if (num > 3155378975999999999L)
			{
				if (throwOnOverflow)
				{
					throw new ArgumentException("Specified argument was out of the range of valid values.");
				}
				return new DateTime(3155378975999999999L, DateTimeKind.Local);
			}
			else
			{
				if (num >= 0L)
				{
					return new DateTime(num, DateTimeKind.Local, flag2);
				}
				if (throwOnOverflow)
				{
					throw new ArgumentException("Specified argument was out of the range of valid values.");
				}
				return new DateTime(0L, DateTimeKind.Local);
			}
		}

		/// <summary>Converts the value of the current <see cref="T:System.DateTime" /> object to its equivalent short date string representation.</summary>
		/// <returns>A string that contains the short date string representation of the current <see cref="T:System.DateTime" /> object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060006EE RID: 1774 RVA: 0x0001D3DE File Offset: 0x0001B5DE
		public string ToShortDateString()
		{
			return DateTimeFormat.Format(this, "d", null);
		}

		/// <summary>Converts the value of the current <see cref="T:System.DateTime" /> object to its equivalent short time string representation.</summary>
		/// <returns>A string that contains the short time string representation of the current <see cref="T:System.DateTime" /> object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060006EF RID: 1775 RVA: 0x0001D3F1 File Offset: 0x0001B5F1
		public string ToShortTimeString()
		{
			return DateTimeFormat.Format(this, "t", null);
		}

		/// <summary>Converts the value of the current <see cref="T:System.DateTime" /> object to its equivalent string representation.</summary>
		/// <returns>A string representation of the value of the current <see cref="T:System.DateTime" /> object.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The date and time is outside the range of dates supported by the calendar used by the current culture. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060006F0 RID: 1776 RVA: 0x0001D404 File Offset: 0x0001B604
		public override string ToString()
		{
			return DateTimeFormat.Format(this, null, null);
		}

		/// <summary>Converts the value of the current <see cref="T:System.DateTime" /> object to its equivalent string representation using the specified format.</summary>
		/// <returns>A string representation of value of the current <see cref="T:System.DateTime" /> object as specified by <paramref name="format" />.</returns>
		/// <param name="format">A standard or custom date and time format string (see Remarks). </param>
		/// <exception cref="T:System.FormatException">The length of <paramref name="format" /> is 1, and it is not one of the format specifier characters defined for <see cref="T:System.Globalization.DateTimeFormatInfo" />.-or- <paramref name="format" /> does not contain a valid custom format pattern. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The date and time is outside the range of dates supported by the calendar used by the current culture.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060006F1 RID: 1777 RVA: 0x0001D413 File Offset: 0x0001B613
		public string ToString(string format)
		{
			return DateTimeFormat.Format(this, format, null);
		}

		/// <summary>Converts the value of the current <see cref="T:System.DateTime" /> object to its equivalent string representation using the specified culture-specific format information.</summary>
		/// <returns>A string representation of value of the current <see cref="T:System.DateTime" /> object as specified by <paramref name="provider" />.</returns>
		/// <param name="provider">An object that supplies culture-specific formatting information. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The date and time is outside the range of dates supported by the calendar used by <paramref name="provider" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060006F2 RID: 1778 RVA: 0x0001D422 File Offset: 0x0001B622
		public string ToString(IFormatProvider provider)
		{
			return DateTimeFormat.Format(this, null, provider);
		}

		/// <summary>Converts the value of the current <see cref="T:System.DateTime" /> object to its equivalent string representation using the specified format and culture-specific format information.</summary>
		/// <returns>A string representation of value of the current <see cref="T:System.DateTime" /> object as specified by <paramref name="format" /> and <paramref name="provider" />.</returns>
		/// <param name="format">A standard or custom date and time format string. </param>
		/// <param name="provider">An object that supplies culture-specific formatting information. </param>
		/// <exception cref="T:System.FormatException">The length of <paramref name="format" /> is 1, and it is not one of the format specifier characters defined for <see cref="T:System.Globalization.DateTimeFormatInfo" />.-or- <paramref name="format" /> does not contain a valid custom format pattern. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The date and time is outside the range of dates supported by the calendar used by <paramref name="provider" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060006F3 RID: 1779 RVA: 0x0001D431 File Offset: 0x0001B631
		public string ToString(string format, IFormatProvider provider)
		{
			return DateTimeFormat.Format(this, format, provider);
		}

		// Token: 0x060006F4 RID: 1780 RVA: 0x0001D440 File Offset: 0x0001B640
		public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format = default(ReadOnlySpan<char>), IFormatProvider provider = null)
		{
			return DateTimeFormat.TryFormat(this, destination, out charsWritten, format, provider);
		}

		/// <summary>Converts the value of the current <see cref="T:System.DateTime" /> object to Coordinated Universal Time (UTC).</summary>
		/// <returns>An object whose <see cref="P:System.DateTime.Kind" /> property is <see cref="F:System.DateTimeKind.Utc" />, and whose value is the UTC equivalent to the value of the current <see cref="T:System.DateTime" /> object, or <see cref="F:System.DateTime.MaxValue" /> if the converted value is too large to be represented by a <see cref="T:System.DateTime" /> object, or <see cref="F:System.DateTime.MinValue" /> if the converted value is too small to be represented by a <see cref="T:System.DateTime" /> object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060006F5 RID: 1781 RVA: 0x0001D452 File Offset: 0x0001B652
		public DateTime ToUniversalTime()
		{
			return TimeZoneInfo.ConvertTimeToUtc(this, TimeZoneInfoOptions.NoThrowOnInvalidTime);
		}

		/// <summary>Converts the specified string representation of a date and time to its <see cref="T:System.DateTime" /> equivalent using the specified culture-specific format information and formatting style, and returns a value that indicates whether the conversion succeeded.</summary>
		/// <returns>true if the <paramref name="s" /> parameter was converted successfully; otherwise, false.</returns>
		/// <param name="s">A string containing a date and time to convert. </param>
		/// <param name="provider">An object that supplies culture-specific formatting information about <paramref name="s" />. </param>
		/// <param name="styles">A bitwise combination of enumeration values that defines how to interpret the parsed date in relation to the current time zone or the current date. A typical value to specify is <see cref="F:System.Globalization.DateTimeStyles.None" />.</param>
		/// <param name="result">When this method returns, contains the <see cref="T:System.DateTime" /> value equivalent to the date and time contained in <paramref name="s" />, if the conversion succeeded, or <see cref="F:System.DateTime.MinValue" /> if the conversion failed. The conversion fails if the <paramref name="s" /> parameter is null, is an empty string (""), or does not contain a valid string representation of a date and time. This parameter is passed uninitialized. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="styles" /> is not a valid <see cref="T:System.Globalization.DateTimeStyles" /> value.-or-<paramref name="styles" /> contains an invalid combination of <see cref="T:System.Globalization.DateTimeStyles" /> values (for example, both <see cref="F:System.Globalization.DateTimeStyles.AssumeLocal" /> and <see cref="F:System.Globalization.DateTimeStyles.AssumeUniversal" />).</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="provider" /> is a neutral culture and cannot be used in a parsing operation.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060006F6 RID: 1782 RVA: 0x0001D460 File Offset: 0x0001B660
		public static bool TryParse(string s, IFormatProvider provider, DateTimeStyles styles, out DateTime result)
		{
			DateTimeFormatInfo.ValidateStyles(styles, "styles");
			if (s == null)
			{
				result = default(DateTime);
				return false;
			}
			return DateTimeParse.TryParse(s, DateTimeFormatInfo.GetInstance(provider), styles, out result);
		}

		/// <summary>Converts the specified string representation of a date and time to its <see cref="T:System.DateTime" /> equivalent using the specified format, culture-specific format information, and style. The format of the string representation must match the specified format exactly. The method returns a value that indicates whether the conversion succeeded.</summary>
		/// <returns>true if <paramref name="s" /> was converted successfully; otherwise, false.</returns>
		/// <param name="s">A string containing a date and time to convert. </param>
		/// <param name="format">The required format of <paramref name="s" />. See the Remarks section for more information. </param>
		/// <param name="provider">An object that supplies culture-specific formatting information about <paramref name="s" />. </param>
		/// <param name="style">A bitwise combination of one or more enumeration values that indicate the permitted format of <paramref name="s" />. </param>
		/// <param name="result">When this method returns, contains the <see cref="T:System.DateTime" /> value equivalent to the date and time contained in <paramref name="s" />, if the conversion succeeded, or <see cref="F:System.DateTime.MinValue" /> if the conversion failed. The conversion fails if either the <paramref name="s" /> or <paramref name="format" /> parameter is null, is an empty string, or does not contain a date and time that correspond to the pattern specified in <paramref name="format" />. This parameter is passed uninitialized. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="styles" /> is not a valid <see cref="T:System.Globalization.DateTimeStyles" /> value.-or-<paramref name="styles" /> contains an invalid combination of <see cref="T:System.Globalization.DateTimeStyles" /> values (for example, both <see cref="F:System.Globalization.DateTimeStyles.AssumeLocal" /> and <see cref="F:System.Globalization.DateTimeStyles.AssumeUniversal" />).</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060006F7 RID: 1783 RVA: 0x0001D48C File Offset: 0x0001B68C
		public static bool TryParseExact(string s, string format, IFormatProvider provider, DateTimeStyles style, out DateTime result)
		{
			DateTimeFormatInfo.ValidateStyles(style, "style");
			if (s == null || format == null)
			{
				result = default(DateTime);
				return false;
			}
			return DateTimeParse.TryParseExact(s, format, DateTimeFormatInfo.GetInstance(provider), style, out result);
		}

		/// <summary>Adds a specified time interval to a specified date and time, yielding a new date and time.</summary>
		/// <returns>An object that is the sum of the values of <paramref name="d" /> and <paramref name="t" />.</returns>
		/// <param name="d">The date and time value to add. </param>
		/// <param name="t">The time interval to add. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The resulting <see cref="T:System.DateTime" /> is less than <see cref="F:System.DateTime.MinValue" /> or greater than <see cref="F:System.DateTime.MaxValue" />. </exception>
		/// <filterpriority>3</filterpriority>
		// Token: 0x060006F8 RID: 1784 RVA: 0x0001D4C4 File Offset: 0x0001B6C4
		public static DateTime operator +(DateTime d, TimeSpan t)
		{
			long internalTicks = d.InternalTicks;
			long ticks = t._ticks;
			if (ticks > 3155378975999999999L - internalTicks || ticks < 0L - internalTicks)
			{
				throw new ArgumentOutOfRangeException("t", "The added or subtracted value results in an un-representable DateTime.");
			}
			return new DateTime((ulong)((internalTicks + ticks) | (long)d.InternalKind));
		}

		/// <summary>Subtracts a specified time interval from a specified date and time and returns a new date and time.</summary>
		/// <returns>An object whose value is the value of <paramref name="d" /> minus the value of <paramref name="t" />.</returns>
		/// <param name="d">The date and time value to subtract from. </param>
		/// <param name="t">The time interval to subtract. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The resulting <see cref="T:System.DateTime" /> is less than <see cref="F:System.DateTime.MinValue" /> or greater than <see cref="F:System.DateTime.MaxValue" />. </exception>
		/// <filterpriority>3</filterpriority>
		// Token: 0x060006F9 RID: 1785 RVA: 0x0001D518 File Offset: 0x0001B718
		public static DateTime operator -(DateTime d, TimeSpan t)
		{
			long internalTicks = d.InternalTicks;
			long ticks = t._ticks;
			if (internalTicks < ticks || internalTicks - 3155378975999999999L > ticks)
			{
				throw new ArgumentOutOfRangeException("t", "The added or subtracted value results in an un-representable DateTime.");
			}
			return new DateTime((ulong)((internalTicks - ticks) | (long)d.InternalKind));
		}

		/// <summary>Subtracts a specified date and time from another specified date and time and returns a time interval.</summary>
		/// <returns>The time interval between <paramref name="d1" /> and <paramref name="d2" />; that is, <paramref name="d1" /> minus <paramref name="d2" />.</returns>
		/// <param name="d1">The date and time value to subtract from (the minuend). </param>
		/// <param name="d2">The date and time value to subtract (the subtrahend). </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x060006FA RID: 1786 RVA: 0x0001D566 File Offset: 0x0001B766
		public static TimeSpan operator -(DateTime d1, DateTime d2)
		{
			return new TimeSpan(d1.InternalTicks - d2.InternalTicks);
		}

		/// <summary>Determines whether two specified instances of <see cref="T:System.DateTime" /> are equal.</summary>
		/// <returns>true if <paramref name="d1" /> and <paramref name="d2" /> represent the same date and time; otherwise, false.</returns>
		/// <param name="d1">The first object to compare. </param>
		/// <param name="d2">The second object to compare. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x060006FB RID: 1787 RVA: 0x0001D57C File Offset: 0x0001B77C
		public static bool operator ==(DateTime d1, DateTime d2)
		{
			return d1.InternalTicks == d2.InternalTicks;
		}

		/// <summary>Determines whether two specified instances of <see cref="T:System.DateTime" /> are not equal.</summary>
		/// <returns>true if <paramref name="d1" /> and <paramref name="d2" /> do not represent the same date and time; otherwise, false.</returns>
		/// <param name="d1">The first object to compare. </param>
		/// <param name="d2">The second object to compare. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x060006FC RID: 1788 RVA: 0x0001D58E File Offset: 0x0001B78E
		public static bool operator !=(DateTime d1, DateTime d2)
		{
			return d1.InternalTicks != d2.InternalTicks;
		}

		/// <summary>Determines whether one specified <see cref="T:System.DateTime" /> is earlier than another specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>true if <paramref name="t1" /> is less than <paramref name="t2" />; otherwise, false.</returns>
		/// <param name="t1">The first object to compare. </param>
		/// <param name="t2">The second object to compare. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x060006FD RID: 1789 RVA: 0x0001D5A3 File Offset: 0x0001B7A3
		public static bool operator <(DateTime t1, DateTime t2)
		{
			return t1.InternalTicks < t2.InternalTicks;
		}

		/// <summary>Determines whether one specified <see cref="T:System.DateTime" /> represents a date and time that is the same as or earlier than another specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>true if <paramref name="t1" /> is less than or equal to <paramref name="t2" />; otherwise, false.</returns>
		/// <param name="t1">The first object to compare. </param>
		/// <param name="t2">The second object to compare. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x060006FE RID: 1790 RVA: 0x0001D5B5 File Offset: 0x0001B7B5
		public static bool operator <=(DateTime t1, DateTime t2)
		{
			return t1.InternalTicks <= t2.InternalTicks;
		}

		/// <summary>Determines whether one specified <see cref="T:System.DateTime" /> is later than another specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>true if <paramref name="t1" /> is greater than <paramref name="t2" />; otherwise, false.</returns>
		/// <param name="t1">The first object to compare. </param>
		/// <param name="t2">The second object to compare. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x060006FF RID: 1791 RVA: 0x0001D5CA File Offset: 0x0001B7CA
		public static bool operator >(DateTime t1, DateTime t2)
		{
			return t1.InternalTicks > t2.InternalTicks;
		}

		/// <summary>Determines whether one specified <see cref="T:System.DateTime" /> represents a date and time that is the same as or later than another specified <see cref="T:System.DateTime" />.</summary>
		/// <returns>true if <paramref name="t1" /> is greater than or equal to <paramref name="t2" />; otherwise, false.</returns>
		/// <param name="t1">The first object to compare. </param>
		/// <param name="t2">The second object to compare. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06000700 RID: 1792 RVA: 0x0001D5DC File Offset: 0x0001B7DC
		public static bool operator >=(DateTime t1, DateTime t2)
		{
			return t1.InternalTicks >= t2.InternalTicks;
		}

		/// <summary>Returns the <see cref="T:System.TypeCode" /> for value type <see cref="T:System.DateTime" />.</summary>
		/// <returns>The enumerated constant, <see cref="F:System.TypeCode.DateTime" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000701 RID: 1793 RVA: 0x0001D5F1 File Offset: 0x0001B7F1
		public TypeCode GetTypeCode()
		{
			return TypeCode.DateTime;
		}

		/// <summary>This conversion is not supported. Attempting to use this method throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>The return value for this member is not used.</returns>
		/// <param name="provider">An object that implements the <see cref="T:System.IFormatProvider" /> interface. (This parameter is not used; specify null.)</param>
		/// <exception cref="T:System.InvalidCastException">In all cases.</exception>
		// Token: 0x06000702 RID: 1794 RVA: 0x0001D5F5 File Offset: 0x0001B7F5
		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			throw new InvalidCastException(SR.Format("Invalid cast from '{0}' to '{1}'.", "DateTime", "Boolean"));
		}

		/// <summary>This conversion is not supported. Attempting to use this method throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>The return value for this member is not used.</returns>
		/// <param name="provider">An object that implements the <see cref="T:System.IFormatProvider" /> interface. (This parameter is not used; specify null.)</param>
		/// <exception cref="T:System.InvalidCastException">In all cases.</exception>
		// Token: 0x06000703 RID: 1795 RVA: 0x0001D610 File Offset: 0x0001B810
		char IConvertible.ToChar(IFormatProvider provider)
		{
			throw new InvalidCastException(SR.Format("Invalid cast from '{0}' to '{1}'.", "DateTime", "Char"));
		}

		/// <summary>This conversion is not supported. Attempting to use this method throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>The return value for this member is not used.</returns>
		/// <param name="provider">An object that implements the <see cref="T:System.IFormatProvider" /> interface. (This parameter is not used; specify null.)</param>
		/// <exception cref="T:System.InvalidCastException">In all cases.</exception>
		// Token: 0x06000704 RID: 1796 RVA: 0x0001D62B File Offset: 0x0001B82B
		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			throw new InvalidCastException(SR.Format("Invalid cast from '{0}' to '{1}'.", "DateTime", "SByte"));
		}

		/// <summary>This conversion is not supported. Attempting to use this method throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>The return value for this member is not used.</returns>
		/// <param name="provider">An object that implements the <see cref="T:System.IFormatProvider" /> interface. (This parameter is not used; specify null.)</param>
		/// <exception cref="T:System.InvalidCastException">In all cases.</exception>
		// Token: 0x06000705 RID: 1797 RVA: 0x0001D646 File Offset: 0x0001B846
		byte IConvertible.ToByte(IFormatProvider provider)
		{
			throw new InvalidCastException(SR.Format("Invalid cast from '{0}' to '{1}'.", "DateTime", "Byte"));
		}

		/// <summary>This conversion is not supported. Attempting to use this method throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>The return value for this member is not used.</returns>
		/// <param name="provider">An object that implements the <see cref="T:System.IFormatProvider" /> interface. (This parameter is not used; specify null.)</param>
		/// <exception cref="T:System.InvalidCastException">In all cases.</exception>
		// Token: 0x06000706 RID: 1798 RVA: 0x0001D661 File Offset: 0x0001B861
		short IConvertible.ToInt16(IFormatProvider provider)
		{
			throw new InvalidCastException(SR.Format("Invalid cast from '{0}' to '{1}'.", "DateTime", "Int16"));
		}

		/// <summary>This conversion is not supported. Attempting to use this method throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>The return value for this member is not used.</returns>
		/// <param name="provider">An object that implements the <see cref="T:System.IFormatProvider" /> interface. (This parameter is not used; specify null.)</param>
		/// <exception cref="T:System.InvalidCastException">In all cases.</exception>
		// Token: 0x06000707 RID: 1799 RVA: 0x0001D67C File Offset: 0x0001B87C
		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			throw new InvalidCastException(SR.Format("Invalid cast from '{0}' to '{1}'.", "DateTime", "UInt16"));
		}

		/// <summary>This conversion is not supported. Attempting to use this method throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>The return value for this member is not used.</returns>
		/// <param name="provider">An object that implements the <see cref="T:System.IFormatProvider" /> interface. (This parameter is not used; specify null.)</param>
		/// <exception cref="T:System.InvalidCastException">In all cases.</exception>
		// Token: 0x06000708 RID: 1800 RVA: 0x0001D697 File Offset: 0x0001B897
		int IConvertible.ToInt32(IFormatProvider provider)
		{
			throw new InvalidCastException(SR.Format("Invalid cast from '{0}' to '{1}'.", "DateTime", "Int32"));
		}

		/// <summary>This conversion is not supported. Attempting to use this method throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>The return value for this member is not used.</returns>
		/// <param name="provider">An object that implements the <see cref="T:System.IFormatProvider" /> interface. (This parameter is not used; specify null.)</param>
		/// <exception cref="T:System.InvalidCastException">In all cases.</exception>
		// Token: 0x06000709 RID: 1801 RVA: 0x0001D6B2 File Offset: 0x0001B8B2
		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			throw new InvalidCastException(SR.Format("Invalid cast from '{0}' to '{1}'.", "DateTime", "UInt32"));
		}

		/// <summary>This conversion is not supported. Attempting to use this method throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>The return value for this member is not used.</returns>
		/// <param name="provider">An object that implements the <see cref="T:System.IFormatProvider" /> interface. (This parameter is not used; specify null.)</param>
		/// <exception cref="T:System.InvalidCastException">In all cases.</exception>
		// Token: 0x0600070A RID: 1802 RVA: 0x0001D6CD File Offset: 0x0001B8CD
		long IConvertible.ToInt64(IFormatProvider provider)
		{
			throw new InvalidCastException(SR.Format("Invalid cast from '{0}' to '{1}'.", "DateTime", "Int64"));
		}

		/// <summary>This conversion is not supported. Attempting to use this method throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>The return value for this member is not used.</returns>
		/// <param name="provider">An object that implements the <see cref="T:System.IFormatProvider" /> interface. (This parameter is not used; specify null.)</param>
		/// <exception cref="T:System.InvalidCastException">In all cases.</exception>
		// Token: 0x0600070B RID: 1803 RVA: 0x0001D6E8 File Offset: 0x0001B8E8
		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			throw new InvalidCastException(SR.Format("Invalid cast from '{0}' to '{1}'.", "DateTime", "UInt64"));
		}

		/// <summary>This conversion is not supported. Attempting to use this method throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>The return value for this member is not used.</returns>
		/// <param name="provider">An object that implements the <see cref="T:System.IFormatProvider" /> interface. (This parameter is not used; specify null.)</param>
		/// <exception cref="T:System.InvalidCastException">In all cases. </exception>
		// Token: 0x0600070C RID: 1804 RVA: 0x0001D703 File Offset: 0x0001B903
		float IConvertible.ToSingle(IFormatProvider provider)
		{
			throw new InvalidCastException(SR.Format("Invalid cast from '{0}' to '{1}'.", "DateTime", "Single"));
		}

		/// <summary>This conversion is not supported. Attempting to use this method throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>The return value for this member is not used.</returns>
		/// <param name="provider">An object that implements the <see cref="T:System.IFormatProvider" /> interface. (This parameter is not used; specify null.)</param>
		/// <exception cref="T:System.InvalidCastException">In all cases.</exception>
		// Token: 0x0600070D RID: 1805 RVA: 0x0001D71E File Offset: 0x0001B91E
		double IConvertible.ToDouble(IFormatProvider provider)
		{
			throw new InvalidCastException(SR.Format("Invalid cast from '{0}' to '{1}'.", "DateTime", "Double"));
		}

		/// <summary>This conversion is not supported. Attempting to use this method throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>The return value for this member is not used.</returns>
		/// <param name="provider">An object that implements the <see cref="T:System.IFormatProvider" /> interface. (This parameter is not used; specify null.)</param>
		/// <exception cref="T:System.InvalidCastException">In all cases.</exception>
		// Token: 0x0600070E RID: 1806 RVA: 0x0001D739 File Offset: 0x0001B939
		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			throw new InvalidCastException(SR.Format("Invalid cast from '{0}' to '{1}'.", "DateTime", "Decimal"));
		}

		/// <summary>Returns the current <see cref="T:System.DateTime" /> object.</summary>
		/// <returns>The current object.</returns>
		/// <param name="provider">An object that implements the <see cref="T:System.IFormatProvider" /> interface. (This parameter is not used; specify null.)</param>
		// Token: 0x0600070F RID: 1807 RVA: 0x0001D754 File Offset: 0x0001B954
		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			return this;
		}

		/// <summary>Converts the current <see cref="T:System.DateTime" /> object to an object of a specified type.</summary>
		/// <returns>An object of the type specified by the <paramref name="type" /> parameter, with a value equivalent to the current <see cref="T:System.DateTime" /> object.</returns>
		/// <param name="type">The desired type. </param>
		/// <param name="provider">An object that implements the <see cref="T:System.IFormatProvider" /> interface. (This parameter is not used; specify null.)</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is null. </exception>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported for the <see cref="T:System.DateTime" /> type.</exception>
		// Token: 0x06000710 RID: 1808 RVA: 0x0001D75C File Offset: 0x0001B95C
		object IConvertible.ToType(Type type, IFormatProvider provider)
		{
			return Convert.DefaultToType(this, type, provider);
		}

		// Token: 0x06000711 RID: 1809 RVA: 0x0001D770 File Offset: 0x0001B970
		internal static bool TryCreate(int year, int month, int day, int hour, int minute, int second, int millisecond, out DateTime result)
		{
			result = DateTime.MinValue;
			if (year < 1 || year > 9999 || month < 1 || month > 12)
			{
				return false;
			}
			int[] array = (DateTime.IsLeapYear(year) ? DateTime.s_daysToMonth366 : DateTime.s_daysToMonth365);
			if (day < 1 || day > array[month] - array[month - 1])
			{
				return false;
			}
			if (hour < 0 || hour >= 24 || minute < 0 || minute >= 60 || second < 0 || second >= 60)
			{
				return false;
			}
			if (millisecond < 0 || millisecond >= 1000)
			{
				return false;
			}
			long num = DateTime.DateToTicks(year, month, day) + DateTime.TimeToTicks(hour, minute, second);
			num += (long)millisecond * 10000L;
			if (num < 0L || num > 3155378975999999999L)
			{
				return false;
			}
			result = new DateTime(num, DateTimeKind.Unspecified);
			return true;
		}

		/// <summary>Gets a <see cref="T:System.DateTime" /> object that is set to the current date and time on this computer, expressed as the Coordinated Universal Time (UTC).</summary>
		/// <returns>An object whose value is the current UTC date and time.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000712 RID: 1810 RVA: 0x0001D83B File Offset: 0x0001BA3B
		public static DateTime UtcNow
		{
			get
			{
				return new DateTime((ulong)((DateTime.GetSystemTimeAsFileTime() + 504911232000000000L) | 4611686018427387904L));
			}
		}

		// Token: 0x06000713 RID: 1811
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern long GetSystemTimeAsFileTime();

		// Token: 0x06000714 RID: 1812 RVA: 0x0001D85B File Offset: 0x0001BA5B
		internal long ToBinaryRaw()
		{
			return (long)this._dateData;
		}

		// Token: 0x040002F3 RID: 755
		private const long TicksPerMillisecond = 10000L;

		// Token: 0x040002F4 RID: 756
		private const long TicksPerSecond = 10000000L;

		// Token: 0x040002F5 RID: 757
		private const long TicksPerMinute = 600000000L;

		// Token: 0x040002F6 RID: 758
		private const long TicksPerHour = 36000000000L;

		// Token: 0x040002F7 RID: 759
		private const long TicksPerDay = 864000000000L;

		// Token: 0x040002F8 RID: 760
		private const int MillisPerSecond = 1000;

		// Token: 0x040002F9 RID: 761
		private const int MillisPerMinute = 60000;

		// Token: 0x040002FA RID: 762
		private const int MillisPerHour = 3600000;

		// Token: 0x040002FB RID: 763
		private const int MillisPerDay = 86400000;

		// Token: 0x040002FC RID: 764
		private const int DaysPerYear = 365;

		// Token: 0x040002FD RID: 765
		private const int DaysPer4Years = 1461;

		// Token: 0x040002FE RID: 766
		private const int DaysPer100Years = 36524;

		// Token: 0x040002FF RID: 767
		private const int DaysPer400Years = 146097;

		// Token: 0x04000300 RID: 768
		private const int DaysTo1601 = 584388;

		// Token: 0x04000301 RID: 769
		private const int DaysTo1899 = 693593;

		// Token: 0x04000302 RID: 770
		internal const int DaysTo1970 = 719162;

		// Token: 0x04000303 RID: 771
		private const int DaysTo10000 = 3652059;

		// Token: 0x04000304 RID: 772
		internal const long MinTicks = 0L;

		// Token: 0x04000305 RID: 773
		internal const long MaxTicks = 3155378975999999999L;

		// Token: 0x04000306 RID: 774
		private const long MaxMillis = 315537897600000L;

		// Token: 0x04000307 RID: 775
		internal const long UnixEpochTicks = 621355968000000000L;

		// Token: 0x04000308 RID: 776
		private const long FileTimeOffset = 504911232000000000L;

		// Token: 0x04000309 RID: 777
		private const long DoubleDateOffset = 599264352000000000L;

		// Token: 0x0400030A RID: 778
		private const long OADateMinAsTicks = 31241376000000000L;

		// Token: 0x0400030B RID: 779
		private const double OADateMinAsDouble = -657435.0;

		// Token: 0x0400030C RID: 780
		private const double OADateMaxAsDouble = 2958466.0;

		// Token: 0x0400030D RID: 781
		private const int DatePartYear = 0;

		// Token: 0x0400030E RID: 782
		private const int DatePartDayOfYear = 1;

		// Token: 0x0400030F RID: 783
		private const int DatePartMonth = 2;

		// Token: 0x04000310 RID: 784
		private const int DatePartDay = 3;

		// Token: 0x04000311 RID: 785
		private static readonly int[] s_daysToMonth365 = new int[]
		{
			0, 31, 59, 90, 120, 151, 181, 212, 243, 273,
			304, 334, 365
		};

		// Token: 0x04000312 RID: 786
		private static readonly int[] s_daysToMonth366 = new int[]
		{
			0, 31, 60, 91, 121, 152, 182, 213, 244, 274,
			305, 335, 366
		};

		/// <summary>Represents the smallest possible value of <see cref="T:System.DateTime" />. This field is read-only.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x04000313 RID: 787
		public static readonly DateTime MinValue = new DateTime(0L, DateTimeKind.Unspecified);

		/// <summary>Represents the largest possible value of <see cref="T:System.DateTime" />. This field is read-only.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x04000314 RID: 788
		public static readonly DateTime MaxValue = new DateTime(3155378975999999999L, DateTimeKind.Unspecified);

		// Token: 0x04000315 RID: 789
		public static readonly DateTime UnixEpoch = new DateTime(621355968000000000L, DateTimeKind.Utc);

		// Token: 0x04000316 RID: 790
		private const ulong TicksMask = 4611686018427387903UL;

		// Token: 0x04000317 RID: 791
		private const ulong FlagsMask = 13835058055282163712UL;

		// Token: 0x04000318 RID: 792
		private const ulong LocalMask = 9223372036854775808UL;

		// Token: 0x04000319 RID: 793
		private const long TicksCeiling = 4611686018427387904L;

		// Token: 0x0400031A RID: 794
		private const ulong KindUnspecified = 0UL;

		// Token: 0x0400031B RID: 795
		private const ulong KindUtc = 4611686018427387904UL;

		// Token: 0x0400031C RID: 796
		private const ulong KindLocal = 9223372036854775808UL;

		// Token: 0x0400031D RID: 797
		private const ulong KindLocalAmbiguousDst = 13835058055282163712UL;

		// Token: 0x0400031E RID: 798
		private const int KindShift = 62;

		// Token: 0x0400031F RID: 799
		private const string TicksField = "ticks";

		// Token: 0x04000320 RID: 800
		private const string DateDataField = "dateData";

		// Token: 0x04000321 RID: 801
		private readonly ulong _dateData;
	}
}
