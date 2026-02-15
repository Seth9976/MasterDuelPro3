using System;
using System.Collections;
using System.Globalization;

namespace System
{
	// Token: 0x020000D2 RID: 210
	[Obsolete("System.CurrentSystemTimeZone has been deprecated.  Please investigate the use of System.TimeZoneInfo.Local instead.")]
	[Serializable]
	internal class CurrentSystemTimeZone : TimeZone
	{
		// Token: 0x06000687 RID: 1671 RVA: 0x0001BE8C File Offset: 0x0001A08C
		internal CurrentSystemTimeZone()
		{
			TimeZoneInfo local = TimeZoneInfo.Local;
			this.m_ticksOffset = local.BaseUtcOffset.Ticks;
			this.m_standardName = local.StandardName;
			this.m_daylightName = local.DaylightName;
		}

		// Token: 0x06000688 RID: 1672 RVA: 0x0001BEDC File Offset: 0x0001A0DC
		internal long GetUtcOffsetFromUniversalTime(DateTime time, ref bool isAmbiguousLocalDst)
		{
			TimeSpan timeSpan = new TimeSpan(this.m_ticksOffset);
			DaylightTime daylightChanges = this.GetDaylightChanges(time.Year);
			isAmbiguousLocalDst = false;
			if (daylightChanges == null || daylightChanges.Delta.Ticks == 0L)
			{
				return timeSpan.Ticks;
			}
			DateTime dateTime = daylightChanges.Start - timeSpan;
			DateTime dateTime2 = daylightChanges.End - timeSpan - daylightChanges.Delta;
			DateTime dateTime3;
			DateTime dateTime4;
			if (daylightChanges.Delta.Ticks > 0L)
			{
				dateTime3 = dateTime2 - daylightChanges.Delta;
				dateTime4 = dateTime2;
			}
			else
			{
				dateTime3 = dateTime;
				dateTime4 = dateTime - daylightChanges.Delta;
			}
			bool flag;
			if (dateTime > dateTime2)
			{
				flag = time < dateTime2 || time >= dateTime;
			}
			else
			{
				flag = time >= dateTime && time < dateTime2;
			}
			if (flag)
			{
				timeSpan += daylightChanges.Delta;
				if (time >= dateTime3 && time < dateTime4)
				{
					isAmbiguousLocalDst = true;
				}
			}
			return timeSpan.Ticks;
		}

		// Token: 0x06000689 RID: 1673 RVA: 0x0001BFE8 File Offset: 0x0001A1E8
		public override DateTime ToLocalTime(DateTime time)
		{
			if (time.Kind == DateTimeKind.Local)
			{
				return time;
			}
			bool flag = false;
			long utcOffsetFromUniversalTime = this.GetUtcOffsetFromUniversalTime(time, ref flag);
			long num = time.Ticks + utcOffsetFromUniversalTime;
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

		// Token: 0x0600068A RID: 1674 RVA: 0x0001C049 File Offset: 0x0001A249
		public override DaylightTime GetDaylightChanges(int year)
		{
			if (year < 1 || year > 9999)
			{
				throw new ArgumentOutOfRangeException("year", SR.Format("Valid values are between {0} and {1}, inclusive.", 1, 9999));
			}
			return this.GetCachedDaylightChanges(year);
		}

		// Token: 0x0600068B RID: 1675 RVA: 0x0001C084 File Offset: 0x0001A284
		private static DaylightTime CreateDaylightChanges(int year)
		{
			DaylightTime daylightTime = null;
			if (TimeZoneInfo.Local.SupportsDaylightSavingTime)
			{
				foreach (TimeZoneInfo.AdjustmentRule adjustmentRule in TimeZoneInfo.Local.GetAdjustmentRules())
				{
					if (adjustmentRule.DateStart.Year <= year && adjustmentRule.DateEnd.Year >= year && adjustmentRule.DaylightDelta != TimeSpan.Zero)
					{
						DateTime dateTime = TimeZoneInfo.TransitionTimeToDateTime(year, adjustmentRule.DaylightTransitionStart);
						DateTime dateTime2 = TimeZoneInfo.TransitionTimeToDateTime(year, adjustmentRule.DaylightTransitionEnd);
						TimeSpan daylightDelta = adjustmentRule.DaylightDelta;
						daylightTime = new DaylightTime(dateTime, dateTime2, daylightDelta);
						break;
					}
				}
			}
			if (daylightTime == null)
			{
				daylightTime = new DaylightTime(DateTime.MinValue, DateTime.MinValue, TimeSpan.Zero);
			}
			return daylightTime;
		}

		// Token: 0x0600068C RID: 1676 RVA: 0x0001C144 File Offset: 0x0001A344
		public override TimeSpan GetUtcOffset(DateTime time)
		{
			if (time.Kind == DateTimeKind.Utc)
			{
				return TimeSpan.Zero;
			}
			return new TimeSpan(TimeZone.CalculateUtcOffset(time, this.GetDaylightChanges(time.Year)).Ticks + this.m_ticksOffset);
		}

		// Token: 0x0600068D RID: 1677 RVA: 0x0001C188 File Offset: 0x0001A388
		private DaylightTime GetCachedDaylightChanges(int year)
		{
			object obj = year;
			if (!this.m_CachedDaylightChanges.Contains(obj))
			{
				DaylightTime daylightTime = CurrentSystemTimeZone.CreateDaylightChanges(year);
				Hashtable cachedDaylightChanges = this.m_CachedDaylightChanges;
				lock (cachedDaylightChanges)
				{
					if (!this.m_CachedDaylightChanges.Contains(obj))
					{
						this.m_CachedDaylightChanges.Add(obj, daylightTime);
					}
				}
			}
			return (DaylightTime)this.m_CachedDaylightChanges[obj];
		}

		// Token: 0x040002EE RID: 750
		private long m_ticksOffset;

		// Token: 0x040002EF RID: 751
		private string m_standardName;

		// Token: 0x040002F0 RID: 752
		private string m_daylightName;

		// Token: 0x040002F1 RID: 753
		private readonly Hashtable m_CachedDaylightChanges = new Hashtable();
	}
}
