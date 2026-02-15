using System;
using System.Globalization;
using System.Text;

namespace System.Xml
{
	// Token: 0x02000014 RID: 20
	internal abstract class BinXmlDateTime
	{
		// Token: 0x06000049 RID: 73 RVA: 0x000036D1 File Offset: 0x000018D1
		private static void Write2Dig(StringBuilder sb, int val)
		{
			sb.Append((char)(48 + val / 10));
			sb.Append((char)(48 + val % 10));
		}

		// Token: 0x0600004A RID: 74 RVA: 0x000036F1 File Offset: 0x000018F1
		private static void Write4DigNeg(StringBuilder sb, int val)
		{
			if (val < 0)
			{
				val = -val;
				sb.Append('-');
			}
			BinXmlDateTime.Write2Dig(sb, val / 100);
			BinXmlDateTime.Write2Dig(sb, val % 100);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00003718 File Offset: 0x00001918
		private static void Write3Dec(StringBuilder sb, int val)
		{
			int num = val % 10;
			val /= 10;
			int num2 = val % 10;
			val /= 10;
			int num3 = val;
			sb.Append('.');
			sb.Append((char)(48 + num3));
			sb.Append((char)(48 + num2));
			sb.Append((char)(48 + num));
		}

		// Token: 0x0600004C RID: 76 RVA: 0x0000376A File Offset: 0x0000196A
		private static void WriteDate(StringBuilder sb, int yr, int mnth, int day)
		{
			BinXmlDateTime.Write4DigNeg(sb, yr);
			sb.Append('-');
			BinXmlDateTime.Write2Dig(sb, mnth);
			sb.Append('-');
			BinXmlDateTime.Write2Dig(sb, day);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00003793 File Offset: 0x00001993
		private static void WriteTime(StringBuilder sb, int hr, int min, int sec, int ms)
		{
			BinXmlDateTime.Write2Dig(sb, hr);
			sb.Append(':');
			BinXmlDateTime.Write2Dig(sb, min);
			sb.Append(':');
			BinXmlDateTime.Write2Dig(sb, sec);
			if (ms != 0)
			{
				BinXmlDateTime.Write3Dec(sb, ms);
			}
		}

		// Token: 0x0600004E RID: 78 RVA: 0x000037C8 File Offset: 0x000019C8
		private static void WriteTimeFullPrecision(StringBuilder sb, int hr, int min, int sec, int fraction)
		{
			BinXmlDateTime.Write2Dig(sb, hr);
			sb.Append(':');
			BinXmlDateTime.Write2Dig(sb, min);
			sb.Append(':');
			BinXmlDateTime.Write2Dig(sb, sec);
			if (fraction != 0)
			{
				int i = 7;
				while (fraction % 10 == 0)
				{
					i--;
					fraction /= 10;
				}
				char[] array = new char[i];
				while (i > 0)
				{
					i--;
					array[i] = (char)(fraction % 10 + 48);
					fraction /= 10;
				}
				sb.Append('.');
				sb.Append(array);
			}
		}

		// Token: 0x0600004F RID: 79 RVA: 0x0000384C File Offset: 0x00001A4C
		private static void WriteTimeZone(StringBuilder sb, TimeSpan zone)
		{
			bool flag = true;
			if (zone.Ticks < 0L)
			{
				flag = false;
				zone = zone.Negate();
			}
			BinXmlDateTime.WriteTimeZone(sb, flag, zone.Hours, zone.Minutes);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00003886 File Offset: 0x00001A86
		private static void WriteTimeZone(StringBuilder sb, bool negTimeZone, int hr, int min)
		{
			if (hr == 0 && min == 0)
			{
				sb.Append('Z');
				return;
			}
			sb.Append(negTimeZone ? '+' : '-');
			BinXmlDateTime.Write2Dig(sb, hr);
			sb.Append(':');
			BinXmlDateTime.Write2Dig(sb, min);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x000038C0 File Offset: 0x00001AC0
		private static void BreakDownXsdDateTime(long val, out int yr, out int mnth, out int day, out int hr, out int min, out int sec, out int ms)
		{
			if (val >= 0L)
			{
				long num = val / 4L;
				ms = (int)(num % 1000L);
				num /= 1000L;
				sec = (int)(num % 60L);
				num /= 60L;
				min = (int)(num % 60L);
				num /= 60L;
				hr = (int)(num % 24L);
				num /= 24L;
				day = (int)(num % 31L) + 1;
				num /= 31L;
				mnth = (int)(num % 12L) + 1;
				num /= 12L;
				yr = (int)(num - 9999L);
				if (yr >= -9999 && yr <= 9999)
				{
					return;
				}
			}
			throw new XmlException("Arithmetic Overflow.", null);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00003968 File Offset: 0x00001B68
		private static void BreakDownXsdDate(long val, out int yr, out int mnth, out int day, out bool negTimeZone, out int hr, out int min)
		{
			if (val >= 0L)
			{
				val /= 4L;
				int num = (int)(val % 1740L) - 840;
				long num2 = val / 1740L;
				if (negTimeZone = num < 0)
				{
					num = -num;
				}
				min = num % 60;
				hr = num / 60;
				day = (int)(num2 % 31L) + 1;
				num2 /= 31L;
				mnth = (int)(num2 % 12L) + 1;
				yr = (int)(num2 / 12L) - 9999;
				if (yr >= -9999 && yr <= 9999)
				{
					return;
				}
			}
			throw new XmlException("Arithmetic Overflow.", null);
		}

		// Token: 0x06000053 RID: 83 RVA: 0x000039FC File Offset: 0x00001BFC
		private static void BreakDownXsdTime(long val, out int hr, out int min, out int sec, out int ms)
		{
			if (val >= 0L)
			{
				val /= 4L;
				ms = (int)(val % 1000L);
				val /= 1000L;
				sec = (int)(val % 60L);
				val /= 60L;
				min = (int)(val % 60L);
				hr = (int)(val / 60L);
				if (0 <= hr && hr <= 23)
				{
					return;
				}
			}
			throw new XmlException("Arithmetic Overflow.", null);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00003A60 File Offset: 0x00001C60
		public static string XsdDateTimeToString(long val)
		{
			int num;
			int num2;
			int num3;
			int num4;
			int num5;
			int num6;
			int num7;
			BinXmlDateTime.BreakDownXsdDateTime(val, out num, out num2, out num3, out num4, out num5, out num6, out num7);
			StringBuilder stringBuilder = new StringBuilder(20);
			BinXmlDateTime.WriteDate(stringBuilder, num, num2, num3);
			stringBuilder.Append('T');
			BinXmlDateTime.WriteTime(stringBuilder, num4, num5, num6, num7);
			stringBuilder.Append('Z');
			return stringBuilder.ToString();
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00003AB8 File Offset: 0x00001CB8
		public static DateTime XsdDateTimeToDateTime(long val)
		{
			int num;
			int num2;
			int num3;
			int num4;
			int num5;
			int num6;
			int num7;
			BinXmlDateTime.BreakDownXsdDateTime(val, out num, out num2, out num3, out num4, out num5, out num6, out num7);
			return new DateTime(num, num2, num3, num4, num5, num6, num7, DateTimeKind.Utc);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00003AEC File Offset: 0x00001CEC
		public static string XsdDateToString(long val)
		{
			int num;
			int num2;
			int num3;
			bool flag;
			int num4;
			int num5;
			BinXmlDateTime.BreakDownXsdDate(val, out num, out num2, out num3, out flag, out num4, out num5);
			StringBuilder stringBuilder = new StringBuilder(20);
			BinXmlDateTime.WriteDate(stringBuilder, num, num2, num3);
			BinXmlDateTime.WriteTimeZone(stringBuilder, flag, num4, num5);
			return stringBuilder.ToString();
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00003B2C File Offset: 0x00001D2C
		public static DateTime XsdDateToDateTime(long val)
		{
			int num;
			int num2;
			int num3;
			bool flag;
			int num4;
			int num5;
			BinXmlDateTime.BreakDownXsdDate(val, out num, out num2, out num3, out flag, out num4, out num5);
			DateTime dateTime = new DateTime(num, num2, num3, 0, 0, 0, DateTimeKind.Utc);
			int num6 = (flag ? (-1) : 1) * (num4 * 60 + num5);
			return TimeZone.CurrentTimeZone.ToLocalTime(dateTime.AddMinutes((double)num6));
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00003B80 File Offset: 0x00001D80
		public static string XsdTimeToString(long val)
		{
			int num;
			int num2;
			int num3;
			int num4;
			BinXmlDateTime.BreakDownXsdTime(val, out num, out num2, out num3, out num4);
			StringBuilder stringBuilder = new StringBuilder(16);
			BinXmlDateTime.WriteTime(stringBuilder, num, num2, num3, num4);
			stringBuilder.Append('Z');
			return stringBuilder.ToString();
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00003BBC File Offset: 0x00001DBC
		public static DateTime XsdTimeToDateTime(long val)
		{
			int num;
			int num2;
			int num3;
			int num4;
			BinXmlDateTime.BreakDownXsdTime(val, out num, out num2, out num3, out num4);
			return new DateTime(1, 1, 1, num, num2, num3, num4, DateTimeKind.Utc);
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00003BE4 File Offset: 0x00001DE4
		public static string SqlDateTimeToString(int dateticks, uint timeticks)
		{
			DateTime dateTime = BinXmlDateTime.SqlDateTimeToDateTime(dateticks, timeticks);
			string text = ((dateTime.Millisecond != 0) ? "yyyy/MM/dd\\THH:mm:ss.ffff" : "yyyy/MM/dd\\THH:mm:ss");
			return dateTime.ToString(text, CultureInfo.InvariantCulture);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00003C1C File Offset: 0x00001E1C
		public static DateTime SqlDateTimeToDateTime(int dateticks, uint timeticks)
		{
			DateTime dateTime = new DateTime(1900, 1, 1);
			long num = (long)(timeticks / BinXmlDateTime.SQLTicksPerMillisecond + 0.5);
			return dateTime.Add(new TimeSpan((long)dateticks * 864000000000L + num * 10000L));
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00003C70 File Offset: 0x00001E70
		public static string SqlSmallDateTimeToString(short dateticks, ushort timeticks)
		{
			return BinXmlDateTime.SqlSmallDateTimeToDateTime(dateticks, timeticks).ToString("yyyy/MM/dd\\THH:mm:ss", CultureInfo.InvariantCulture);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00003C96 File Offset: 0x00001E96
		public static DateTime SqlSmallDateTimeToDateTime(short dateticks, ushort timeticks)
		{
			return BinXmlDateTime.SqlDateTimeToDateTime((int)dateticks, (uint)((int)timeticks * BinXmlDateTime.SQLTicksPerMinute));
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00003CA8 File Offset: 0x00001EA8
		public static DateTime XsdKatmaiDateToDateTime(byte[] data, int offset)
		{
			long katmaiDateTicks = BinXmlDateTime.GetKatmaiDateTicks(data, ref offset);
			return new DateTime(katmaiDateTicks);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00003CC4 File Offset: 0x00001EC4
		public static DateTime XsdKatmaiDateTimeToDateTime(byte[] data, int offset)
		{
			long katmaiTimeTicks = BinXmlDateTime.GetKatmaiTimeTicks(data, ref offset);
			long katmaiDateTicks = BinXmlDateTime.GetKatmaiDateTicks(data, ref offset);
			return new DateTime(katmaiDateTicks + katmaiTimeTicks);
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00003CEB File Offset: 0x00001EEB
		public static DateTime XsdKatmaiTimeToDateTime(byte[] data, int offset)
		{
			return BinXmlDateTime.XsdKatmaiDateTimeToDateTime(data, offset);
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00003CF4 File Offset: 0x00001EF4
		public static DateTime XsdKatmaiDateOffsetToDateTime(byte[] data, int offset)
		{
			return BinXmlDateTime.XsdKatmaiDateOffsetToDateTimeOffset(data, offset).LocalDateTime;
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00003D10 File Offset: 0x00001F10
		public static DateTime XsdKatmaiDateTimeOffsetToDateTime(byte[] data, int offset)
		{
			return BinXmlDateTime.XsdKatmaiDateTimeOffsetToDateTimeOffset(data, offset).LocalDateTime;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00003D2C File Offset: 0x00001F2C
		public static DateTime XsdKatmaiTimeOffsetToDateTime(byte[] data, int offset)
		{
			return BinXmlDateTime.XsdKatmaiTimeOffsetToDateTimeOffset(data, offset).LocalDateTime;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00003D48 File Offset: 0x00001F48
		public static DateTimeOffset XsdKatmaiDateOffsetToDateTimeOffset(byte[] data, int offset)
		{
			return BinXmlDateTime.XsdKatmaiDateTimeOffsetToDateTimeOffset(data, offset);
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00003D54 File Offset: 0x00001F54
		public static DateTimeOffset XsdKatmaiDateTimeOffsetToDateTimeOffset(byte[] data, int offset)
		{
			long katmaiTimeTicks = BinXmlDateTime.GetKatmaiTimeTicks(data, ref offset);
			long katmaiDateTicks = BinXmlDateTime.GetKatmaiDateTicks(data, ref offset);
			long katmaiTimeZoneTicks = BinXmlDateTime.GetKatmaiTimeZoneTicks(data, offset);
			return new DateTimeOffset(katmaiDateTicks + katmaiTimeTicks + katmaiTimeZoneTicks, new TimeSpan(katmaiTimeZoneTicks));
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00003D48 File Offset: 0x00001F48
		public static DateTimeOffset XsdKatmaiTimeOffsetToDateTimeOffset(byte[] data, int offset)
		{
			return BinXmlDateTime.XsdKatmaiDateTimeOffsetToDateTimeOffset(data, offset);
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00003D8C File Offset: 0x00001F8C
		public static string XsdKatmaiDateToString(byte[] data, int offset)
		{
			DateTime dateTime = BinXmlDateTime.XsdKatmaiDateToDateTime(data, offset);
			StringBuilder stringBuilder = new StringBuilder(10);
			BinXmlDateTime.WriteDate(stringBuilder, dateTime.Year, dateTime.Month, dateTime.Day);
			return stringBuilder.ToString();
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00003DC8 File Offset: 0x00001FC8
		public static string XsdKatmaiDateTimeToString(byte[] data, int offset)
		{
			DateTime dateTime = BinXmlDateTime.XsdKatmaiDateTimeToDateTime(data, offset);
			StringBuilder stringBuilder = new StringBuilder(33);
			BinXmlDateTime.WriteDate(stringBuilder, dateTime.Year, dateTime.Month, dateTime.Day);
			stringBuilder.Append('T');
			BinXmlDateTime.WriteTimeFullPrecision(stringBuilder, dateTime.Hour, dateTime.Minute, dateTime.Second, BinXmlDateTime.GetFractions(dateTime));
			return stringBuilder.ToString();
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00003E30 File Offset: 0x00002030
		public static string XsdKatmaiTimeToString(byte[] data, int offset)
		{
			DateTime dateTime = BinXmlDateTime.XsdKatmaiTimeToDateTime(data, offset);
			StringBuilder stringBuilder = new StringBuilder(16);
			BinXmlDateTime.WriteTimeFullPrecision(stringBuilder, dateTime.Hour, dateTime.Minute, dateTime.Second, BinXmlDateTime.GetFractions(dateTime));
			return stringBuilder.ToString();
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00003E74 File Offset: 0x00002074
		public static string XsdKatmaiDateOffsetToString(byte[] data, int offset)
		{
			DateTimeOffset dateTimeOffset = BinXmlDateTime.XsdKatmaiDateOffsetToDateTimeOffset(data, offset);
			StringBuilder stringBuilder = new StringBuilder(16);
			BinXmlDateTime.WriteDate(stringBuilder, dateTimeOffset.Year, dateTimeOffset.Month, dateTimeOffset.Day);
			BinXmlDateTime.WriteTimeZone(stringBuilder, dateTimeOffset.Offset);
			return stringBuilder.ToString();
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003EC0 File Offset: 0x000020C0
		public static string XsdKatmaiDateTimeOffsetToString(byte[] data, int offset)
		{
			DateTimeOffset dateTimeOffset = BinXmlDateTime.XsdKatmaiDateTimeOffsetToDateTimeOffset(data, offset);
			StringBuilder stringBuilder = new StringBuilder(39);
			BinXmlDateTime.WriteDate(stringBuilder, dateTimeOffset.Year, dateTimeOffset.Month, dateTimeOffset.Day);
			stringBuilder.Append('T');
			BinXmlDateTime.WriteTimeFullPrecision(stringBuilder, dateTimeOffset.Hour, dateTimeOffset.Minute, dateTimeOffset.Second, BinXmlDateTime.GetFractions(dateTimeOffset));
			BinXmlDateTime.WriteTimeZone(stringBuilder, dateTimeOffset.Offset);
			return stringBuilder.ToString();
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00003F34 File Offset: 0x00002134
		public static string XsdKatmaiTimeOffsetToString(byte[] data, int offset)
		{
			DateTimeOffset dateTimeOffset = BinXmlDateTime.XsdKatmaiTimeOffsetToDateTimeOffset(data, offset);
			StringBuilder stringBuilder = new StringBuilder(22);
			BinXmlDateTime.WriteTimeFullPrecision(stringBuilder, dateTimeOffset.Hour, dateTimeOffset.Minute, dateTimeOffset.Second, BinXmlDateTime.GetFractions(dateTimeOffset));
			BinXmlDateTime.WriteTimeZone(stringBuilder, dateTimeOffset.Offset);
			return stringBuilder.ToString();
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003F84 File Offset: 0x00002184
		private static long GetKatmaiDateTicks(byte[] data, ref int pos)
		{
			int num = pos;
			pos = num + 3;
			return (long)((int)data[num] | ((int)data[num + 1] << 8) | ((int)data[num + 2] << 16)) * 864000000000L;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00003FB8 File Offset: 0x000021B8
		private static long GetKatmaiTimeTicks(byte[] data, ref int pos)
		{
			int num = pos;
			byte b = data[num];
			num++;
			long num2;
			if (b <= 2)
			{
				num2 = (long)((int)data[num] | ((int)data[num + 1] << 8) | ((int)data[num + 2] << 16));
				pos = num + 3;
			}
			else if (b <= 4)
			{
				num2 = (long)((int)data[num] | ((int)data[num + 1] << 8) | ((int)data[num + 2] << 16));
				num2 |= (long)((long)((ulong)data[num + 3]) << 24);
				pos = num + 4;
			}
			else
			{
				if (b > 7)
				{
					throw new XmlException("Arithmetic Overflow.", null);
				}
				num2 = (long)((int)data[num] | ((int)data[num + 1] << 8) | ((int)data[num + 2] << 16));
				num2 |= (long)(((ulong)data[num + 3] << 24) | ((ulong)data[num + 4] << 32));
				pos = num + 5;
			}
			return num2 * (long)BinXmlDateTime.KatmaiTimeScaleMultiplicator[(int)b];
		}

		// Token: 0x0600006F RID: 111 RVA: 0x0000406B File Offset: 0x0000226B
		private static long GetKatmaiTimeZoneTicks(byte[] data, int pos)
		{
			return (long)((short)((int)data[pos] | ((int)data[pos + 1] << 8))) * 600000000L;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00004084 File Offset: 0x00002284
		private static int GetFractions(DateTime dt)
		{
			return (int)(dt.Ticks - new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second).Ticks);
		}

		// Token: 0x06000071 RID: 113 RVA: 0x000040D4 File Offset: 0x000022D4
		private static int GetFractions(DateTimeOffset dt)
		{
			return (int)(dt.Ticks - new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second).Ticks);
		}

		// Token: 0x04000089 RID: 137
		internal static int[] KatmaiTimeScaleMultiplicator = new int[] { 10000000, 1000000, 100000, 10000, 1000, 100, 10, 1 };

		// Token: 0x0400008A RID: 138
		private static readonly double SQLTicksPerMillisecond = 0.3;

		// Token: 0x0400008B RID: 139
		public static readonly int SQLTicksPerSecond = 300;

		// Token: 0x0400008C RID: 140
		public static readonly int SQLTicksPerMinute = BinXmlDateTime.SQLTicksPerSecond * 60;

		// Token: 0x0400008D RID: 141
		public static readonly int SQLTicksPerHour = BinXmlDateTime.SQLTicksPerMinute * 60;

		// Token: 0x0400008E RID: 142
		private static readonly int SQLTicksPerDay = BinXmlDateTime.SQLTicksPerHour * 24;
	}
}
