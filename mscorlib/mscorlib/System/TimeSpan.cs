using System;
using System.Globalization;

namespace System
{
	/// <summary>Represents a time interval.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200014F RID: 335
	[Serializable]
	public readonly struct TimeSpan : IComparable, IComparable<TimeSpan>, IEquatable<TimeSpan>, IFormattable, ISpanFormattable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.TimeSpan" /> structure to the specified number of ticks.</summary>
		/// <param name="ticks">A time period expressed in 100-nanosecond units. </param>
		// Token: 0x06000B3A RID: 2874 RVA: 0x000322A3 File Offset: 0x000304A3
		public TimeSpan(long ticks)
		{
			this._ticks = ticks;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.TimeSpan" /> structure to a specified number of hours, minutes, and seconds.</summary>
		/// <param name="hours">Number of hours. </param>
		/// <param name="minutes">Number of minutes. </param>
		/// <param name="seconds">Number of seconds. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The parameters specify a <see cref="T:System.TimeSpan" /> value less than <see cref="F:System.TimeSpan.MinValue" /> or greater than <see cref="F:System.TimeSpan.MaxValue" />. </exception>
		// Token: 0x06000B3B RID: 2875 RVA: 0x000322AC File Offset: 0x000304AC
		public TimeSpan(int hours, int minutes, int seconds)
		{
			this._ticks = TimeSpan.TimeToTicks(hours, minutes, seconds);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.TimeSpan" /> structure to a specified number of days, hours, minutes, and seconds.</summary>
		/// <param name="days">Number of days. </param>
		/// <param name="hours">Number of hours. </param>
		/// <param name="minutes">Number of minutes. </param>
		/// <param name="seconds">Number of seconds. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The parameters specify a <see cref="T:System.TimeSpan" /> value less than <see cref="F:System.TimeSpan.MinValue" /> or greater than <see cref="F:System.TimeSpan.MaxValue" />. </exception>
		// Token: 0x06000B3C RID: 2876 RVA: 0x000322BC File Offset: 0x000304BC
		public TimeSpan(int days, int hours, int minutes, int seconds)
		{
			this = new TimeSpan(days, hours, minutes, seconds, 0);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.TimeSpan" /> strrructure to a specified number of days, hours, minutes, seconds, and milliseconds.</summary>
		/// <param name="days">Number of days. </param>
		/// <param name="hours">Number of hours. </param>
		/// <param name="minutes">Number of minutes. </param>
		/// <param name="seconds">Number of seconds. </param>
		/// <param name="milliseconds">Number of milliseconds. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The parameters specify a <see cref="T:System.TimeSpan" /> value less than <see cref="F:System.TimeSpan.MinValue" /> or greater than <see cref="F:System.TimeSpan.MaxValue" />. </exception>
		// Token: 0x06000B3D RID: 2877 RVA: 0x000322CC File Offset: 0x000304CC
		public TimeSpan(int days, int hours, int minutes, int seconds, int milliseconds)
		{
			long num = ((long)days * 3600L * 24L + (long)hours * 3600L + (long)minutes * 60L + (long)seconds) * 1000L + (long)milliseconds;
			if (num > 922337203685477L || num < -922337203685477L)
			{
				throw new ArgumentOutOfRangeException(null, "TimeSpan overflowed because the duration is too long.");
			}
			this._ticks = num * 10000L;
		}

		/// <summary>Gets the number of ticks that represent the value of the current <see cref="T:System.TimeSpan" /> structure.</summary>
		/// <returns>The number of ticks contained in this instance.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000B3E RID: 2878 RVA: 0x00032339 File Offset: 0x00030539
		public long Ticks
		{
			get
			{
				return this._ticks;
			}
		}

		/// <summary>Gets the days component of the time interval represented by the current <see cref="T:System.TimeSpan" /> structure.</summary>
		/// <returns>The day component of this instance. The return value can be positive or negative.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000B3F RID: 2879 RVA: 0x00032341 File Offset: 0x00030541
		public int Days
		{
			get
			{
				return (int)(this._ticks / 864000000000L);
			}
		}

		/// <summary>Gets the hours component of the time interval represented by the current <see cref="T:System.TimeSpan" /> structure.</summary>
		/// <returns>The hour component of the current <see cref="T:System.TimeSpan" /> structure. The return value ranges from -23 through 23.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000B40 RID: 2880 RVA: 0x00032354 File Offset: 0x00030554
		public int Hours
		{
			get
			{
				return (int)(this._ticks / 36000000000L % 24L);
			}
		}

		/// <summary>Gets the minutes component of the time interval represented by the current <see cref="T:System.TimeSpan" /> structure.</summary>
		/// <returns>The minute component of the current <see cref="T:System.TimeSpan" /> structure. The return value ranges from -59 through 59.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000B41 RID: 2881 RVA: 0x0003236B File Offset: 0x0003056B
		public int Minutes
		{
			get
			{
				return (int)(this._ticks / 600000000L % 60L);
			}
		}

		/// <summary>Gets the value of the current <see cref="T:System.TimeSpan" /> structure expressed in whole and fractional days.</summary>
		/// <returns>The total number of days represented by this instance.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000B42 RID: 2882 RVA: 0x0003237F File Offset: 0x0003057F
		public double TotalDays
		{
			get
			{
				return (double)this._ticks * 1.1574074074074074E-12;
			}
		}

		/// <summary>Gets the value of the current <see cref="T:System.TimeSpan" /> structure expressed in whole and fractional hours.</summary>
		/// <returns>The total number of hours represented by this instance.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000B43 RID: 2883 RVA: 0x00032392 File Offset: 0x00030592
		public double TotalHours
		{
			get
			{
				return (double)this._ticks * 2.7777777777777777E-11;
			}
		}

		/// <summary>Gets the value of the current <see cref="T:System.TimeSpan" /> structure expressed in whole and fractional milliseconds.</summary>
		/// <returns>The total number of milliseconds represented by this instance.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000B44 RID: 2884 RVA: 0x000323A8 File Offset: 0x000305A8
		public double TotalMilliseconds
		{
			get
			{
				double num = (double)this._ticks * 0.0001;
				if (num > 922337203685477.0)
				{
					return 922337203685477.0;
				}
				if (num < -922337203685477.0)
				{
					return -922337203685477.0;
				}
				return num;
			}
		}

		/// <summary>Gets the value of the current <see cref="T:System.TimeSpan" /> structure expressed in whole and fractional minutes.</summary>
		/// <returns>The total number of minutes represented by this instance.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000B45 RID: 2885 RVA: 0x000323F4 File Offset: 0x000305F4
		public double TotalMinutes
		{
			get
			{
				return (double)this._ticks * 1.6666666666666667E-09;
			}
		}

		/// <summary>Gets the value of the current <see cref="T:System.TimeSpan" /> structure expressed in whole and fractional seconds.</summary>
		/// <returns>The total number of seconds represented by this instance.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000B46 RID: 2886 RVA: 0x00032407 File Offset: 0x00030607
		public double TotalSeconds
		{
			get
			{
				return (double)this._ticks * 1E-07;
			}
		}

		/// <summary>Returns a new <see cref="T:System.TimeSpan" /> object whose value is the sum of the specified <see cref="T:System.TimeSpan" /> object and this instance.</summary>
		/// <returns>A new object that represents the value of this instance plus the value of <paramref name="ts" />.</returns>
		/// <param name="ts">The time interval to add.</param>
		/// <exception cref="T:System.OverflowException">The resulting <see cref="T:System.TimeSpan" /> is less than <see cref="F:System.TimeSpan.MinValue" /> or greater than <see cref="F:System.TimeSpan.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000B47 RID: 2887 RVA: 0x0003241C File Offset: 0x0003061C
		public TimeSpan Add(TimeSpan ts)
		{
			long num = this._ticks + ts._ticks;
			if (this._ticks >> 63 == ts._ticks >> 63 && this._ticks >> 63 != num >> 63)
			{
				throw new OverflowException("TimeSpan overflowed because the duration is too long.");
			}
			return new TimeSpan(num);
		}

		/// <summary>Compares two <see cref="T:System.TimeSpan" /> values and returns an integer that indicates whether the first value is shorter than, equal to, or longer than the second value.</summary>
		/// <returns>One of the following values.Value Description -1 <paramref name="t1" /> is shorter than <paramref name="t2" />. 0 <paramref name="t1" /> is equal to <paramref name="t2" />. 1 <paramref name="t1" /> is longer than <paramref name="t2" />. </returns>
		/// <param name="t1">The first time interval to compare. </param>
		/// <param name="t2">The second time interval to compare. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000B48 RID: 2888 RVA: 0x0003246B File Offset: 0x0003066B
		public static int Compare(TimeSpan t1, TimeSpan t2)
		{
			if (t1._ticks > t2._ticks)
			{
				return 1;
			}
			if (t1._ticks < t2._ticks)
			{
				return -1;
			}
			return 0;
		}

		/// <summary>Compares this instance to a specified object and returns an integer that indicates whether this instance is shorter than, equal to, or longer than the specified object.</summary>
		/// <returns>One of the following values.Value Description -1 This instance is shorter than <paramref name="value" />. 0 This instance is equal to <paramref name="value" />. 1 This instance is longer than <paramref name="value" />.-or- <paramref name="value" /> is null. </returns>
		/// <param name="value">An object to compare, or null. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> is not a <see cref="T:System.TimeSpan" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000B49 RID: 2889 RVA: 0x00032490 File Offset: 0x00030690
		public int CompareTo(object value)
		{
			if (value == null)
			{
				return 1;
			}
			if (!(value is TimeSpan))
			{
				throw new ArgumentException("Object must be of type TimeSpan.");
			}
			long ticks = ((TimeSpan)value)._ticks;
			if (this._ticks > ticks)
			{
				return 1;
			}
			if (this._ticks < ticks)
			{
				return -1;
			}
			return 0;
		}

		/// <summary>Compares this instance to a specified <see cref="T:System.TimeSpan" /> object and returns an integer that indicates whether this instance is shorter than, equal to, or longer than the <see cref="T:System.TimeSpan" /> object.</summary>
		/// <returns>A signed number indicating the relative values of this instance and <paramref name="value" />.Value Description A negative integer This instance is shorter than <paramref name="value" />. Zero This instance is equal to <paramref name="value" />. A positive integer This instance is longer than <paramref name="value" />. </returns>
		/// <param name="value">An object to compare to this instance.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000B4A RID: 2890 RVA: 0x000324D8 File Offset: 0x000306D8
		public int CompareTo(TimeSpan value)
		{
			long ticks = value._ticks;
			if (this._ticks > ticks)
			{
				return 1;
			}
			if (this._ticks < ticks)
			{
				return -1;
			}
			return 0;
		}

		/// <summary>Returns a <see cref="T:System.TimeSpan" /> that represents a specified number of days, where the specification is accurate to the nearest millisecond.</summary>
		/// <returns>An object that represents <paramref name="value" />.</returns>
		/// <param name="value">A number of days, accurate to the nearest millisecond. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than <see cref="F:System.TimeSpan.MinValue" /> or greater than <see cref="F:System.TimeSpan.MaxValue" />. -or-<paramref name="value" /> is <see cref="F:System.Double.PositiveInfinity" />.-or-<paramref name="value" /> is <see cref="F:System.Double.NegativeInfinity" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> is equal to <see cref="F:System.Double.NaN" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000B4B RID: 2891 RVA: 0x00032503 File Offset: 0x00030703
		public static TimeSpan FromDays(double value)
		{
			return TimeSpan.Interval(value, 86400000);
		}

		/// <summary>Returns a value indicating whether this instance is equal to a specified object.</summary>
		/// <returns>true if <paramref name="value" /> is a <see cref="T:System.TimeSpan" /> object that represents the same time interval as the current <see cref="T:System.TimeSpan" /> structure; otherwise, false.</returns>
		/// <param name="value">An object to compare with this instance. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000B4C RID: 2892 RVA: 0x00032510 File Offset: 0x00030710
		public override bool Equals(object value)
		{
			return value is TimeSpan && this._ticks == ((TimeSpan)value)._ticks;
		}

		/// <summary>Returns a value indicating whether this instance is equal to a specified <see cref="T:System.TimeSpan" /> object.</summary>
		/// <returns>true if <paramref name="obj" /> represents the same time interval as this instance; otherwise, false.</returns>
		/// <param name="obj">An object to compare with this instance. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000B4D RID: 2893 RVA: 0x0003252F File Offset: 0x0003072F
		public bool Equals(TimeSpan obj)
		{
			return this._ticks == obj._ticks;
		}

		/// <summary>Returns a hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000B4E RID: 2894 RVA: 0x0003253F File Offset: 0x0003073F
		public override int GetHashCode()
		{
			return (int)this._ticks ^ (int)(this._ticks >> 32);
		}

		/// <summary>Returns a <see cref="T:System.TimeSpan" /> that represents a specified number of hours, where the specification is accurate to the nearest millisecond.</summary>
		/// <returns>An object that represents <paramref name="value" />.</returns>
		/// <param name="value">A number of hours accurate to the nearest millisecond. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than <see cref="F:System.TimeSpan.MinValue" /> or greater than <see cref="F:System.TimeSpan.MaxValue" />. -or-<paramref name="value" /> is <see cref="F:System.Double.PositiveInfinity" />.-or-<paramref name="value" /> is <see cref="F:System.Double.NegativeInfinity" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> is equal to <see cref="F:System.Double.NaN" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000B4F RID: 2895 RVA: 0x00032553 File Offset: 0x00030753
		public static TimeSpan FromHours(double value)
		{
			return TimeSpan.Interval(value, 3600000);
		}

		// Token: 0x06000B50 RID: 2896 RVA: 0x00032560 File Offset: 0x00030760
		private static TimeSpan Interval(double value, int scale)
		{
			if (double.IsNaN(value))
			{
				throw new ArgumentException("TimeSpan does not accept floating point Not-a-Number values.");
			}
			double num = value * (double)scale + ((value >= 0.0) ? 0.5 : (-0.5));
			if (num > 922337203685477.0 || num < -922337203685477.0)
			{
				throw new OverflowException("TimeSpan overflowed because the duration is too long.");
			}
			return new TimeSpan((long)num * 10000L);
		}

		/// <summary>Returns a <see cref="T:System.TimeSpan" /> that represents a specified number of milliseconds.</summary>
		/// <returns>An object that represents <paramref name="value" />.</returns>
		/// <param name="value">A number of milliseconds. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than <see cref="F:System.TimeSpan.MinValue" /> or greater than <see cref="F:System.TimeSpan.MaxValue" />.-or-<paramref name="value" /> is <see cref="F:System.Double.PositiveInfinity" />.-or-<paramref name="value" /> is <see cref="F:System.Double.NegativeInfinity" />. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> is equal to <see cref="F:System.Double.NaN" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000B51 RID: 2897 RVA: 0x000325D7 File Offset: 0x000307D7
		public static TimeSpan FromMilliseconds(double value)
		{
			return TimeSpan.Interval(value, 1);
		}

		/// <summary>Returns a <see cref="T:System.TimeSpan" /> that represents a specified number of minutes, where the specification is accurate to the nearest millisecond.</summary>
		/// <returns>An object that represents <paramref name="value" />.</returns>
		/// <param name="value">A number of minutes, accurate to the nearest millisecond. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than <see cref="F:System.TimeSpan.MinValue" /> or greater than <see cref="F:System.TimeSpan.MaxValue" />.-or-<paramref name="value" /> is <see cref="F:System.Double.PositiveInfinity" />.-or-<paramref name="value" /> is <see cref="F:System.Double.NegativeInfinity" />. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> is equal to <see cref="F:System.Double.NaN" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000B52 RID: 2898 RVA: 0x000325E0 File Offset: 0x000307E0
		public static TimeSpan FromMinutes(double value)
		{
			return TimeSpan.Interval(value, 60000);
		}

		/// <summary>Returns a new <see cref="T:System.TimeSpan" /> object whose value is the negated value of this instance.</summary>
		/// <returns>A new object with the same numeric value as this instance, but with the opposite sign.</returns>
		/// <exception cref="T:System.OverflowException">The negated value of this instance cannot be represented by a <see cref="T:System.TimeSpan" />; that is, the value of this instance is <see cref="F:System.TimeSpan.MinValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000B53 RID: 2899 RVA: 0x000325ED File Offset: 0x000307ED
		public TimeSpan Negate()
		{
			if (this.Ticks == TimeSpan.MinValue.Ticks)
			{
				throw new OverflowException("Negating the minimum value of a twos complement number is invalid.");
			}
			return new TimeSpan(-this._ticks);
		}

		/// <summary>Returns a <see cref="T:System.TimeSpan" /> that represents a specified number of seconds, where the specification is accurate to the nearest millisecond.</summary>
		/// <returns>An object that represents <paramref name="value" />.</returns>
		/// <param name="value">A number of seconds, accurate to the nearest millisecond. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than <see cref="F:System.TimeSpan.MinValue" /> or greater than <see cref="F:System.TimeSpan.MaxValue" />.-or-<paramref name="value" /> is <see cref="F:System.Double.PositiveInfinity" />.-or-<paramref name="value" /> is <see cref="F:System.Double.NegativeInfinity" />. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> is equal to <see cref="F:System.Double.NaN" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000B54 RID: 2900 RVA: 0x00032618 File Offset: 0x00030818
		public static TimeSpan FromSeconds(double value)
		{
			return TimeSpan.Interval(value, 1000);
		}

		/// <summary>Returns a new <see cref="T:System.TimeSpan" /> object whose value is the difference between the specified <see cref="T:System.TimeSpan" /> object and this instance.</summary>
		/// <returns>A new time interval whose value is the result of the value of this instance minus the value of <paramref name="ts" />.</returns>
		/// <param name="ts">The time interval to be subtracted. </param>
		/// <exception cref="T:System.OverflowException">The return value is less than <see cref="F:System.TimeSpan.MinValue" /> or greater than <see cref="F:System.TimeSpan.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000B55 RID: 2901 RVA: 0x00032628 File Offset: 0x00030828
		public TimeSpan Subtract(TimeSpan ts)
		{
			long num = this._ticks - ts._ticks;
			if (this._ticks >> 63 != ts._ticks >> 63 && this._ticks >> 63 != num >> 63)
			{
				throw new OverflowException("TimeSpan overflowed because the duration is too long.");
			}
			return new TimeSpan(num);
		}

		/// <summary>Returns a <see cref="T:System.TimeSpan" /> that represents a specified time, where the specification is in units of ticks.</summary>
		/// <returns>An object that represents <paramref name="value" />.</returns>
		/// <param name="value">A number of ticks that represent a time. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000B56 RID: 2902 RVA: 0x00032677 File Offset: 0x00030877
		public static TimeSpan FromTicks(long value)
		{
			return new TimeSpan(value);
		}

		// Token: 0x06000B57 RID: 2903 RVA: 0x00032680 File Offset: 0x00030880
		internal static long TimeToTicks(int hour, int minute, int second)
		{
			long num = (long)hour * 3600L + (long)minute * 60L + (long)second;
			if (num > 922337203685L || num < -922337203685L)
			{
				throw new ArgumentOutOfRangeException(null, "TimeSpan overflowed because the duration is too long.");
			}
			return num * 10000000L;
		}

		/// <summary>Converts the string representation of a time interval to its <see cref="T:System.TimeSpan" /> equivalent. </summary>
		/// <returns>A time interval that corresponds to <paramref name="s" />.</returns>
		/// <param name="s">A string that specifies the time interval to convert. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> has an invalid format. </exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="s" /> represents a number that is less than <see cref="F:System.TimeSpan.MinValue" /> or greater than <see cref="F:System.TimeSpan.MaxValue" />.-or- At least one of the days, hours, minutes, or seconds components is outside its valid range. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000B58 RID: 2904 RVA: 0x000326CD File Offset: 0x000308CD
		public static TimeSpan Parse(string s)
		{
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.input);
			}
			return TimeSpanParse.Parse(s, null);
		}

		/// <summary>Converts the string representation of a time interval to its <see cref="T:System.TimeSpan" /> equivalent by using the specified culture-specific format information.</summary>
		/// <returns>A time interval that corresponds to <paramref name="input" />, as specified by <paramref name="formatProvider" />.</returns>
		/// <param name="input">A string that specifies the time interval to convert.</param>
		/// <param name="formatProvider">An object that supplies culture-specific formatting information.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is null. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="input" /> has an invalid format. </exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="input" /> represents a number that is less than <see cref="F:System.TimeSpan.MinValue" /> or greater than <see cref="F:System.TimeSpan.MaxValue" />.-or- At least one of the days, hours, minutes, or seconds components in <paramref name="input" /> is outside its valid range. </exception>
		// Token: 0x06000B59 RID: 2905 RVA: 0x000326E5 File Offset: 0x000308E5
		public static TimeSpan Parse(string input, IFormatProvider formatProvider)
		{
			if (input == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.input);
			}
			return TimeSpanParse.Parse(input, formatProvider);
		}

		/// <summary>Converts the value of the current <see cref="T:System.TimeSpan" /> object to its equivalent string representation.</summary>
		/// <returns>The string representation of the current <see cref="T:System.TimeSpan" /> value. </returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000B5A RID: 2906 RVA: 0x000326FD File Offset: 0x000308FD
		public override string ToString()
		{
			return TimeSpanFormat.Format(this, null, null);
		}

		/// <summary>Converts the value of the current <see cref="T:System.TimeSpan" /> object to its equivalent string representation by using the specified format and culture-specific formatting information.</summary>
		/// <returns>The string representation of the current <see cref="T:System.TimeSpan" /> value, as specified by <paramref name="format" /> and <paramref name="formatProvider" />.</returns>
		/// <param name="format">A standard or custom <see cref="T:System.TimeSpan" /> format string.</param>
		/// <param name="formatProvider">An object that supplies culture-specific formatting information.</param>
		/// <exception cref="T:System.FormatException">The <paramref name="format" /> parameter is not recognized or is not supported.</exception>
		// Token: 0x06000B5B RID: 2907 RVA: 0x0003270C File Offset: 0x0003090C
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return TimeSpanFormat.Format(this, format, formatProvider);
		}

		// Token: 0x06000B5C RID: 2908 RVA: 0x0003271B File Offset: 0x0003091B
		public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format = default(ReadOnlySpan<char>), IFormatProvider formatProvider = null)
		{
			return TimeSpanFormat.TryFormat(this, destination, out charsWritten, format, formatProvider);
		}

		/// <summary>Returns a <see cref="T:System.TimeSpan" /> whose value is the negated value of the specified instance.</summary>
		/// <returns>An object that has the same numeric value as this instance, but the opposite sign.</returns>
		/// <param name="t">The time interval to be negated. </param>
		/// <exception cref="T:System.OverflowException">The negated value of this instance cannot be represented by a <see cref="T:System.TimeSpan" />; that is, the value of this instance is <see cref="F:System.TimeSpan.MinValue" />. </exception>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06000B5D RID: 2909 RVA: 0x0003272D File Offset: 0x0003092D
		public static TimeSpan operator -(TimeSpan t)
		{
			if (t._ticks == TimeSpan.MinValue._ticks)
			{
				throw new OverflowException("Negating the minimum value of a twos complement number is invalid.");
			}
			return new TimeSpan(-t._ticks);
		}

		/// <summary>Subtracts a specified <see cref="T:System.TimeSpan" /> from another specified <see cref="T:System.TimeSpan" />.</summary>
		/// <returns>An object whose value is the result of the value of <paramref name="t1" /> minus the value of <paramref name="t2" />.</returns>
		/// <param name="t1">The minuend. </param>
		/// <param name="t2">The subtrahend. </param>
		/// <exception cref="T:System.OverflowException">The return value is less than <see cref="F:System.TimeSpan.MinValue" /> or greater than <see cref="F:System.TimeSpan.MaxValue" />. </exception>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06000B5E RID: 2910 RVA: 0x00032758 File Offset: 0x00030958
		public static TimeSpan operator -(TimeSpan t1, TimeSpan t2)
		{
			return t1.Subtract(t2);
		}

		/// <summary>Adds two specified <see cref="T:System.TimeSpan" /> instances.</summary>
		/// <returns>An object whose value is the sum of the values of <paramref name="t1" /> and <paramref name="t2" />.</returns>
		/// <param name="t1">The first time interval to add. </param>
		/// <param name="t2">The second time interval to add.</param>
		/// <exception cref="T:System.OverflowException">The resulting <see cref="T:System.TimeSpan" /> is less than <see cref="F:System.TimeSpan.MinValue" /> or greater than <see cref="F:System.TimeSpan.MaxValue" />. </exception>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06000B5F RID: 2911 RVA: 0x00032762 File Offset: 0x00030962
		public static TimeSpan operator +(TimeSpan t1, TimeSpan t2)
		{
			return t1.Add(t2);
		}

		/// <summary>Indicates whether two <see cref="T:System.TimeSpan" /> instances are equal.</summary>
		/// <returns>true if the values of <paramref name="t1" /> and <paramref name="t2" /> are equal; otherwise, false.</returns>
		/// <param name="t1">The first time interval to compare. </param>
		/// <param name="t2">The second time interval to compare. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06000B60 RID: 2912 RVA: 0x0003252F File Offset: 0x0003072F
		public static bool operator ==(TimeSpan t1, TimeSpan t2)
		{
			return t1._ticks == t2._ticks;
		}

		/// <summary>Indicates whether two <see cref="T:System.TimeSpan" /> instances are not equal.</summary>
		/// <returns>true if the values of <paramref name="t1" /> and <paramref name="t2" /> are not equal; otherwise, false.</returns>
		/// <param name="t1">The first time interval to compare.</param>
		/// <param name="t2">The second time interval to compare.</param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06000B61 RID: 2913 RVA: 0x0003276C File Offset: 0x0003096C
		public static bool operator !=(TimeSpan t1, TimeSpan t2)
		{
			return t1._ticks != t2._ticks;
		}

		/// <summary>Indicates whether a specified <see cref="T:System.TimeSpan" /> is less than another specified <see cref="T:System.TimeSpan" />.</summary>
		/// <returns>true if the value of <paramref name="t1" /> is less than the value of <paramref name="t2" />; otherwise, false.</returns>
		/// <param name="t1">The first time interval to compare.</param>
		/// <param name="t2">The second time interval to compare. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06000B62 RID: 2914 RVA: 0x0003277F File Offset: 0x0003097F
		public static bool operator <(TimeSpan t1, TimeSpan t2)
		{
			return t1._ticks < t2._ticks;
		}

		/// <summary>Indicates whether a specified <see cref="T:System.TimeSpan" /> is less than or equal to another specified <see cref="T:System.TimeSpan" />.</summary>
		/// <returns>true if the value of <paramref name="t1" /> is less than or equal to the value of <paramref name="t2" />; otherwise, false.</returns>
		/// <param name="t1">The first time interval to compare. </param>
		/// <param name="t2">The second time interval to compare. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06000B63 RID: 2915 RVA: 0x0003278F File Offset: 0x0003098F
		public static bool operator <=(TimeSpan t1, TimeSpan t2)
		{
			return t1._ticks <= t2._ticks;
		}

		/// <summary>Indicates whether a specified <see cref="T:System.TimeSpan" /> is greater than another specified <see cref="T:System.TimeSpan" />.</summary>
		/// <returns>true if the value of <paramref name="t1" /> is greater than the value of <paramref name="t2" />; otherwise, false.</returns>
		/// <param name="t1">The first time interval to compare. </param>
		/// <param name="t2">The second time interval to compare. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06000B64 RID: 2916 RVA: 0x000327A2 File Offset: 0x000309A2
		public static bool operator >(TimeSpan t1, TimeSpan t2)
		{
			return t1._ticks > t2._ticks;
		}

		/// <summary>Indicates whether a specified <see cref="T:System.TimeSpan" /> is greater than or equal to another specified <see cref="T:System.TimeSpan" />.</summary>
		/// <returns>true if the value of <paramref name="t1" /> is greater than or equal to the value of <paramref name="t2" />; otherwise, false.</returns>
		/// <param name="t1">The first time interval to compare.</param>
		/// <param name="t2">The second time interval to compare.</param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06000B65 RID: 2917 RVA: 0x000327B2 File Offset: 0x000309B2
		public static bool operator >=(TimeSpan t1, TimeSpan t2)
		{
			return t1._ticks >= t2._ticks;
		}

		/// <summary>Represents the zero <see cref="T:System.TimeSpan" /> value. This field is read-only.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x04000493 RID: 1171
		public static readonly TimeSpan Zero = new TimeSpan(0L);

		/// <summary>Represents the maximum <see cref="T:System.TimeSpan" /> value. This field is read-only.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x04000494 RID: 1172
		public static readonly TimeSpan MaxValue = new TimeSpan(long.MaxValue);

		/// <summary>Represents the minimum <see cref="T:System.TimeSpan" /> value. This field is read-only.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x04000495 RID: 1173
		public static readonly TimeSpan MinValue = new TimeSpan(long.MinValue);

		// Token: 0x04000496 RID: 1174
		internal readonly long _ticks;
	}
}
