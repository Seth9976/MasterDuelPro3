using System;
using System.Globalization;
using System.Threading;

namespace System
{
	/// <summary>Represents a time zone.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000150 RID: 336
	[Obsolete("System.TimeZone has been deprecated.  Please investigate the use of System.TimeZoneInfo instead.")]
	[Serializable]
	public abstract class TimeZone
	{
		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000B67 RID: 2919 RVA: 0x000327FC File Offset: 0x000309FC
		private static object InternalSyncObject
		{
			get
			{
				if (TimeZone.s_InternalSyncObject == null)
				{
					object obj = new object();
					Interlocked.CompareExchange<object>(ref TimeZone.s_InternalSyncObject, obj, null);
				}
				return TimeZone.s_InternalSyncObject;
			}
		}

		/// <summary>Gets the time zone of the current computer.</summary>
		/// <returns>A <see cref="T:System.TimeZone" /> object that represents the current local time zone.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000B69 RID: 2921 RVA: 0x00032828 File Offset: 0x00030A28
		public static TimeZone CurrentTimeZone
		{
			get
			{
				TimeZone timeZone = TimeZone.currentTimeZone;
				if (timeZone == null)
				{
					object internalSyncObject = TimeZone.InternalSyncObject;
					lock (internalSyncObject)
					{
						if (TimeZone.currentTimeZone == null)
						{
							TimeZone.currentTimeZone = new CurrentSystemTimeZone();
						}
						timeZone = TimeZone.currentTimeZone;
					}
				}
				return timeZone;
			}
		}

		/// <summary>Returns the Coordinated Universal Time (UTC) offset for the specified local time.</summary>
		/// <returns>The Coordinated Universal Time (UTC) offset from <paramref name="time" />.</returns>
		/// <param name="time">A date and time value.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000B6A RID: 2922
		public abstract TimeSpan GetUtcOffset(DateTime time);

		/// <summary>Returns the local time that corresponds to a specified date and time value.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> object whose value is the local time that corresponds to <paramref name="time" />.</returns>
		/// <param name="time">A Coordinated Universal Time (UTC) time. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000B6B RID: 2923 RVA: 0x0003288C File Offset: 0x00030A8C
		public virtual DateTime ToLocalTime(DateTime time)
		{
			if (time.Kind == DateTimeKind.Local)
			{
				return time;
			}
			bool flag = false;
			long utcOffsetFromUniversalTime = ((CurrentSystemTimeZone)TimeZone.CurrentTimeZone).GetUtcOffsetFromUniversalTime(time, ref flag);
			return new DateTime(time.Ticks + utcOffsetFromUniversalTime, DateTimeKind.Local, flag);
		}

		/// <summary>Returns the daylight saving time period for a particular year.</summary>
		/// <returns>A <see cref="T:System.Globalization.DaylightTime" /> object that contains the start and end date for daylight saving time in <paramref name="year" />.</returns>
		/// <param name="year">The year that the daylight saving time period applies to. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is less than 1 or greater than 9999. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000B6C RID: 2924
		public abstract DaylightTime GetDaylightChanges(int year);

		// Token: 0x06000B6D RID: 2925 RVA: 0x000328CC File Offset: 0x00030ACC
		internal static TimeSpan CalculateUtcOffset(DateTime time, DaylightTime daylightTimes)
		{
			if (daylightTimes == null)
			{
				return TimeSpan.Zero;
			}
			if (time.Kind == DateTimeKind.Utc)
			{
				return TimeSpan.Zero;
			}
			DateTime dateTime = daylightTimes.Start + daylightTimes.Delta;
			DateTime end = daylightTimes.End;
			DateTime dateTime2;
			DateTime dateTime3;
			if (daylightTimes.Delta.Ticks > 0L)
			{
				dateTime2 = end - daylightTimes.Delta;
				dateTime3 = end;
			}
			else
			{
				dateTime2 = dateTime;
				dateTime3 = dateTime - daylightTimes.Delta;
			}
			bool flag = false;
			if (dateTime > end)
			{
				if (time >= dateTime || time < end)
				{
					flag = true;
				}
			}
			else if (time >= dateTime && time < end)
			{
				flag = true;
			}
			if (flag && time >= dateTime2 && time < dateTime3)
			{
				flag = time.IsAmbiguousDaylightSavingTime();
			}
			if (flag)
			{
				return daylightTimes.Delta;
			}
			return TimeSpan.Zero;
		}

		// Token: 0x06000B6E RID: 2926 RVA: 0x000329A5 File Offset: 0x00030BA5
		internal static void ClearCachedData()
		{
			TimeZone.currentTimeZone = null;
		}

		// Token: 0x04000497 RID: 1175
		private static volatile TimeZone currentTimeZone;

		// Token: 0x04000498 RID: 1176
		private static object s_InternalSyncObject;
	}
}
