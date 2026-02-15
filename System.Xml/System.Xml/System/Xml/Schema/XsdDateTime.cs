using System;
using System.Text;

namespace System.Xml.Schema
{
	// Token: 0x02000327 RID: 807
	internal struct XsdDateTime
	{
		// Token: 0x060024C8 RID: 9416 RVA: 0x000D00A4 File Offset: 0x000CE2A4
		public XsdDateTime(string text, XsdDateTimeFlags kinds)
		{
			this = default(XsdDateTime);
			XsdDateTime.Parser parser = default(XsdDateTime.Parser);
			if (!parser.Parse(text, kinds))
			{
				throw new FormatException(Res.GetString("The string '{0}' is not a valid {1} value.", new object[] { text, kinds }));
			}
			this.InitiateXsdDateTime(parser);
		}

		// Token: 0x060024C9 RID: 9417 RVA: 0x000D00F5 File Offset: 0x000CE2F5
		private XsdDateTime(XsdDateTime.Parser parser)
		{
			this = default(XsdDateTime);
			this.InitiateXsdDateTime(parser);
		}

		// Token: 0x060024CA RID: 9418 RVA: 0x000D0108 File Offset: 0x000CE308
		private void InitiateXsdDateTime(XsdDateTime.Parser parser)
		{
			this.dt = new DateTime(parser.year, parser.month, parser.day, parser.hour, parser.minute, parser.second);
			if (parser.fraction != 0)
			{
				this.dt = this.dt.AddTicks((long)parser.fraction);
			}
			this.extra = (uint)(((int)parser.typeCode << 24) | (XsdDateTime.DateTimeTypeCode)((int)parser.kind << 16) | (XsdDateTime.DateTimeTypeCode)(parser.zoneHour << 8) | (XsdDateTime.DateTimeTypeCode)parser.zoneMinute);
		}

		// Token: 0x060024CB RID: 9419 RVA: 0x000D0190 File Offset: 0x000CE390
		internal static bool TryParse(string text, XsdDateTimeFlags kinds, out XsdDateTime result)
		{
			XsdDateTime.Parser parser = default(XsdDateTime.Parser);
			if (!parser.Parse(text, kinds))
			{
				result = default(XsdDateTime);
				return false;
			}
			result = new XsdDateTime(parser);
			return true;
		}

		// Token: 0x060024CC RID: 9420 RVA: 0x000D01C8 File Offset: 0x000CE3C8
		public XsdDateTime(DateTime dateTime, XsdDateTimeFlags kinds)
		{
			this.dt = dateTime;
			XsdDateTime.DateTimeTypeCode dateTimeTypeCode = (XsdDateTime.DateTimeTypeCode)(Bits.LeastPosition((uint)kinds) - 1);
			int num = 0;
			int num2 = 0;
			DateTimeKind kind = dateTime.Kind;
			XsdDateTime.XsdDateTimeKind xsdDateTimeKind;
			if (kind != DateTimeKind.Unspecified)
			{
				if (kind != DateTimeKind.Utc)
				{
					TimeSpan utcOffset = TimeZoneInfo.Local.GetUtcOffset(dateTime);
					if (utcOffset.Ticks < 0L)
					{
						xsdDateTimeKind = XsdDateTime.XsdDateTimeKind.LocalWestOfZulu;
						num = -utcOffset.Hours;
						num2 = -utcOffset.Minutes;
					}
					else
					{
						xsdDateTimeKind = XsdDateTime.XsdDateTimeKind.LocalEastOfZulu;
						num = utcOffset.Hours;
						num2 = utcOffset.Minutes;
					}
				}
				else
				{
					xsdDateTimeKind = XsdDateTime.XsdDateTimeKind.Zulu;
				}
			}
			else
			{
				xsdDateTimeKind = XsdDateTime.XsdDateTimeKind.Unspecified;
			}
			this.extra = (uint)(((int)dateTimeTypeCode << 24) | (XsdDateTime.DateTimeTypeCode)((int)xsdDateTimeKind << 16) | (XsdDateTime.DateTimeTypeCode)(num << 8) | (XsdDateTime.DateTimeTypeCode)num2);
		}

		// Token: 0x060024CD RID: 9421 RVA: 0x000D025A File Offset: 0x000CE45A
		public XsdDateTime(DateTimeOffset dateTimeOffset)
		{
			this = new XsdDateTime(dateTimeOffset, XsdDateTimeFlags.DateTime);
		}

		// Token: 0x060024CE RID: 9422 RVA: 0x000D0264 File Offset: 0x000CE464
		public XsdDateTime(DateTimeOffset dateTimeOffset, XsdDateTimeFlags kinds)
		{
			this.dt = dateTimeOffset.DateTime;
			TimeSpan timeSpan = dateTimeOffset.Offset;
			XsdDateTime.DateTimeTypeCode dateTimeTypeCode = (XsdDateTime.DateTimeTypeCode)(Bits.LeastPosition((uint)kinds) - 1);
			XsdDateTime.XsdDateTimeKind xsdDateTimeKind;
			if (timeSpan.TotalMinutes < 0.0)
			{
				timeSpan = timeSpan.Negate();
				xsdDateTimeKind = XsdDateTime.XsdDateTimeKind.LocalWestOfZulu;
			}
			else if (timeSpan.TotalMinutes > 0.0)
			{
				xsdDateTimeKind = XsdDateTime.XsdDateTimeKind.LocalEastOfZulu;
			}
			else
			{
				xsdDateTimeKind = XsdDateTime.XsdDateTimeKind.Zulu;
			}
			this.extra = (uint)(((int)dateTimeTypeCode << 24) | (XsdDateTime.DateTimeTypeCode)((int)xsdDateTimeKind << 16) | (XsdDateTime.DateTimeTypeCode)(timeSpan.Hours << 8) | (XsdDateTime.DateTimeTypeCode)timeSpan.Minutes);
		}

		// Token: 0x1700089D RID: 2205
		// (get) Token: 0x060024CF RID: 9423 RVA: 0x000D02E6 File Offset: 0x000CE4E6
		private XsdDateTime.DateTimeTypeCode InternalTypeCode
		{
			get
			{
				return (XsdDateTime.DateTimeTypeCode)((this.extra & 4278190080U) >> 24);
			}
		}

		// Token: 0x1700089E RID: 2206
		// (get) Token: 0x060024D0 RID: 9424 RVA: 0x000D02F7 File Offset: 0x000CE4F7
		private XsdDateTime.XsdDateTimeKind InternalKind
		{
			get
			{
				return (XsdDateTime.XsdDateTimeKind)((this.extra & 16711680U) >> 16);
			}
		}

		// Token: 0x1700089F RID: 2207
		// (get) Token: 0x060024D1 RID: 9425 RVA: 0x000D0308 File Offset: 0x000CE508
		public int Year
		{
			get
			{
				return this.dt.Year;
			}
		}

		// Token: 0x170008A0 RID: 2208
		// (get) Token: 0x060024D2 RID: 9426 RVA: 0x000D0315 File Offset: 0x000CE515
		public int Month
		{
			get
			{
				return this.dt.Month;
			}
		}

		// Token: 0x170008A1 RID: 2209
		// (get) Token: 0x060024D3 RID: 9427 RVA: 0x000D0322 File Offset: 0x000CE522
		public int Day
		{
			get
			{
				return this.dt.Day;
			}
		}

		// Token: 0x170008A2 RID: 2210
		// (get) Token: 0x060024D4 RID: 9428 RVA: 0x000D032F File Offset: 0x000CE52F
		public int Hour
		{
			get
			{
				return this.dt.Hour;
			}
		}

		// Token: 0x170008A3 RID: 2211
		// (get) Token: 0x060024D5 RID: 9429 RVA: 0x000D033C File Offset: 0x000CE53C
		public int Minute
		{
			get
			{
				return this.dt.Minute;
			}
		}

		// Token: 0x170008A4 RID: 2212
		// (get) Token: 0x060024D6 RID: 9430 RVA: 0x000D0349 File Offset: 0x000CE549
		public int Second
		{
			get
			{
				return this.dt.Second;
			}
		}

		// Token: 0x170008A5 RID: 2213
		// (get) Token: 0x060024D7 RID: 9431 RVA: 0x000D0358 File Offset: 0x000CE558
		public int Fraction
		{
			get
			{
				return (int)(this.dt.Ticks - new DateTime(this.dt.Year, this.dt.Month, this.dt.Day, this.dt.Hour, this.dt.Minute, this.dt.Second).Ticks);
			}
		}

		// Token: 0x170008A6 RID: 2214
		// (get) Token: 0x060024D8 RID: 9432 RVA: 0x000D03C1 File Offset: 0x000CE5C1
		public int ZoneHour
		{
			get
			{
				return (int)((this.extra & 65280U) >> 8);
			}
		}

		// Token: 0x170008A7 RID: 2215
		// (get) Token: 0x060024D9 RID: 9433 RVA: 0x000D03D1 File Offset: 0x000CE5D1
		public int ZoneMinute
		{
			get
			{
				return (int)(this.extra & 255U);
			}
		}

		// Token: 0x060024DA RID: 9434 RVA: 0x000D03E0 File Offset: 0x000CE5E0
		public static implicit operator DateTime(XsdDateTime xdt)
		{
			XsdDateTime.DateTimeTypeCode internalTypeCode = xdt.InternalTypeCode;
			DateTime dateTime;
			if (internalTypeCode != XsdDateTime.DateTimeTypeCode.Time)
			{
				if (internalTypeCode - XsdDateTime.DateTimeTypeCode.GDay <= 1)
				{
					dateTime = new DateTime(DateTime.Now.Year, xdt.Month, xdt.Day);
				}
				else
				{
					dateTime = xdt.dt;
				}
			}
			else
			{
				DateTime now = DateTime.Now;
				TimeSpan timeSpan = new DateTime(now.Year, now.Month, now.Day) - new DateTime(xdt.Year, xdt.Month, xdt.Day);
				dateTime = xdt.dt.Add(timeSpan);
			}
			switch (xdt.InternalKind)
			{
			case XsdDateTime.XsdDateTimeKind.Zulu:
				dateTime = new DateTime(dateTime.Ticks, DateTimeKind.Utc);
				break;
			case XsdDateTime.XsdDateTimeKind.LocalWestOfZulu:
			{
				long num = dateTime.Ticks + new TimeSpan(xdt.ZoneHour, xdt.ZoneMinute, 0).Ticks;
				if (num > DateTime.MaxValue.Ticks)
				{
					num += TimeZoneInfo.Local.GetUtcOffset(dateTime).Ticks;
					if (num > DateTime.MaxValue.Ticks)
					{
						num = DateTime.MaxValue.Ticks;
					}
					return new DateTime(num, DateTimeKind.Local);
				}
				dateTime = new DateTime(num, DateTimeKind.Utc).ToLocalTime();
				break;
			}
			case XsdDateTime.XsdDateTimeKind.LocalEastOfZulu:
			{
				long num = dateTime.Ticks - new TimeSpan(xdt.ZoneHour, xdt.ZoneMinute, 0).Ticks;
				if (num < DateTime.MinValue.Ticks)
				{
					num += TimeZoneInfo.Local.GetUtcOffset(dateTime).Ticks;
					if (num < DateTime.MinValue.Ticks)
					{
						num = DateTime.MinValue.Ticks;
					}
					return new DateTime(num, DateTimeKind.Local);
				}
				dateTime = new DateTime(num, DateTimeKind.Utc).ToLocalTime();
				break;
			}
			}
			return dateTime;
		}

		// Token: 0x060024DB RID: 9435 RVA: 0x000D05B0 File Offset: 0x000CE7B0
		public static implicit operator DateTimeOffset(XsdDateTime xdt)
		{
			XsdDateTime.DateTimeTypeCode internalTypeCode = xdt.InternalTypeCode;
			DateTime dateTime;
			if (internalTypeCode != XsdDateTime.DateTimeTypeCode.Time)
			{
				if (internalTypeCode - XsdDateTime.DateTimeTypeCode.GDay <= 1)
				{
					dateTime = new DateTime(DateTime.Now.Year, xdt.Month, xdt.Day);
				}
				else
				{
					dateTime = xdt.dt;
				}
			}
			else
			{
				DateTime now = DateTime.Now;
				TimeSpan timeSpan = new DateTime(now.Year, now.Month, now.Day) - new DateTime(xdt.Year, xdt.Month, xdt.Day);
				dateTime = xdt.dt.Add(timeSpan);
			}
			DateTimeOffset dateTimeOffset;
			switch (xdt.InternalKind)
			{
			case XsdDateTime.XsdDateTimeKind.Zulu:
				dateTimeOffset = new DateTimeOffset(dateTime, new TimeSpan(0L));
				return dateTimeOffset;
			case XsdDateTime.XsdDateTimeKind.LocalWestOfZulu:
				dateTimeOffset = new DateTimeOffset(dateTime, new TimeSpan(-xdt.ZoneHour, -xdt.ZoneMinute, 0));
				return dateTimeOffset;
			case XsdDateTime.XsdDateTimeKind.LocalEastOfZulu:
				dateTimeOffset = new DateTimeOffset(dateTime, new TimeSpan(xdt.ZoneHour, xdt.ZoneMinute, 0));
				return dateTimeOffset;
			}
			dateTimeOffset = new DateTimeOffset(dateTime, TimeZoneInfo.Local.GetUtcOffset(dateTime));
			return dateTimeOffset;
		}

		// Token: 0x060024DC RID: 9436 RVA: 0x000D06D4 File Offset: 0x000CE8D4
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(64);
			switch (this.InternalTypeCode)
			{
			case XsdDateTime.DateTimeTypeCode.DateTime:
				this.PrintDate(stringBuilder);
				stringBuilder.Append('T');
				this.PrintTime(stringBuilder);
				break;
			case XsdDateTime.DateTimeTypeCode.Time:
				this.PrintTime(stringBuilder);
				break;
			case XsdDateTime.DateTimeTypeCode.Date:
				this.PrintDate(stringBuilder);
				break;
			case XsdDateTime.DateTimeTypeCode.GYearMonth:
			{
				char[] array = new char[XsdDateTime.Lzyyyy_MM];
				this.IntToCharArray(array, 0, this.Year, 4);
				array[XsdDateTime.Lzyyyy] = '-';
				this.ShortToCharArray(array, XsdDateTime.Lzyyyy_, this.Month);
				stringBuilder.Append(array);
				break;
			}
			case XsdDateTime.DateTimeTypeCode.GYear:
			{
				char[] array = new char[XsdDateTime.Lzyyyy];
				this.IntToCharArray(array, 0, this.Year, 4);
				stringBuilder.Append(array);
				break;
			}
			case XsdDateTime.DateTimeTypeCode.GMonthDay:
			{
				char[] array = new char[XsdDateTime.Lz__mm_dd];
				array[0] = '-';
				array[XsdDateTime.Lz_] = '-';
				this.ShortToCharArray(array, XsdDateTime.Lz__, this.Month);
				array[XsdDateTime.Lz__mm] = '-';
				this.ShortToCharArray(array, XsdDateTime.Lz__mm_, this.Day);
				stringBuilder.Append(array);
				break;
			}
			case XsdDateTime.DateTimeTypeCode.GDay:
			{
				char[] array = new char[XsdDateTime.Lz___dd];
				array[0] = '-';
				array[XsdDateTime.Lz_] = '-';
				array[XsdDateTime.Lz__] = '-';
				this.ShortToCharArray(array, XsdDateTime.Lz___, this.Day);
				stringBuilder.Append(array);
				break;
			}
			case XsdDateTime.DateTimeTypeCode.GMonth:
			{
				char[] array = new char[XsdDateTime.Lz__mm__];
				array[0] = '-';
				array[XsdDateTime.Lz_] = '-';
				this.ShortToCharArray(array, XsdDateTime.Lz__, this.Month);
				array[XsdDateTime.Lz__mm] = '-';
				array[XsdDateTime.Lz__mm_] = '-';
				stringBuilder.Append(array);
				break;
			}
			}
			this.PrintZone(stringBuilder);
			return stringBuilder.ToString();
		}

		// Token: 0x060024DD RID: 9437 RVA: 0x000D089C File Offset: 0x000CEA9C
		private void PrintDate(StringBuilder sb)
		{
			char[] array = new char[XsdDateTime.Lzyyyy_MM_dd];
			this.IntToCharArray(array, 0, this.Year, 4);
			array[XsdDateTime.Lzyyyy] = '-';
			this.ShortToCharArray(array, XsdDateTime.Lzyyyy_, this.Month);
			array[XsdDateTime.Lzyyyy_MM] = '-';
			this.ShortToCharArray(array, XsdDateTime.Lzyyyy_MM_, this.Day);
			sb.Append(array);
		}

		// Token: 0x060024DE RID: 9438 RVA: 0x000D0904 File Offset: 0x000CEB04
		private void PrintTime(StringBuilder sb)
		{
			char[] array = new char[XsdDateTime.LzHH_mm_ss];
			this.ShortToCharArray(array, 0, this.Hour);
			array[XsdDateTime.LzHH] = ':';
			this.ShortToCharArray(array, XsdDateTime.LzHH_, this.Minute);
			array[XsdDateTime.LzHH_mm] = ':';
			this.ShortToCharArray(array, XsdDateTime.LzHH_mm_, this.Second);
			sb.Append(array);
			int num = this.Fraction;
			if (num != 0)
			{
				int num2 = 7;
				while (num % 10 == 0)
				{
					num2--;
					num /= 10;
				}
				array = new char[num2 + 1];
				array[0] = '.';
				this.IntToCharArray(array, 1, num, num2);
				sb.Append(array);
			}
		}

		// Token: 0x060024DF RID: 9439 RVA: 0x000D09A8 File Offset: 0x000CEBA8
		private void PrintZone(StringBuilder sb)
		{
			switch (this.InternalKind)
			{
			case XsdDateTime.XsdDateTimeKind.Zulu:
				sb.Append('Z');
				return;
			case XsdDateTime.XsdDateTimeKind.LocalWestOfZulu:
			{
				char[] array = new char[XsdDateTime.Lz_zz_zz];
				array[0] = '-';
				this.ShortToCharArray(array, XsdDateTime.Lz_, this.ZoneHour);
				array[XsdDateTime.Lz_zz] = ':';
				this.ShortToCharArray(array, XsdDateTime.Lz_zz_, this.ZoneMinute);
				sb.Append(array);
				return;
			}
			case XsdDateTime.XsdDateTimeKind.LocalEastOfZulu:
			{
				char[] array = new char[XsdDateTime.Lz_zz_zz];
				array[0] = '+';
				this.ShortToCharArray(array, XsdDateTime.Lz_, this.ZoneHour);
				array[XsdDateTime.Lz_zz] = ':';
				this.ShortToCharArray(array, XsdDateTime.Lz_zz_, this.ZoneMinute);
				sb.Append(array);
				return;
			}
			default:
				return;
			}
		}

		// Token: 0x060024E0 RID: 9440 RVA: 0x000D0A66 File Offset: 0x000CEC66
		private void IntToCharArray(char[] text, int start, int value, int digits)
		{
			while (digits-- != 0)
			{
				text[start + digits] = (char)(value % 10 + 48);
				value /= 10;
			}
		}

		// Token: 0x060024E1 RID: 9441 RVA: 0x000D0A87 File Offset: 0x000CEC87
		private void ShortToCharArray(char[] text, int start, int value)
		{
			text[start] = (char)(value / 10 + 48);
			text[start + 1] = (char)(value % 10 + 48);
		}

		// Token: 0x04001170 RID: 4464
		private DateTime dt;

		// Token: 0x04001171 RID: 4465
		private uint extra;

		// Token: 0x04001172 RID: 4466
		private static readonly int Lzyyyy = "yyyy".Length;

		// Token: 0x04001173 RID: 4467
		private static readonly int Lzyyyy_ = "yyyy-".Length;

		// Token: 0x04001174 RID: 4468
		private static readonly int Lzyyyy_MM = "yyyy-MM".Length;

		// Token: 0x04001175 RID: 4469
		private static readonly int Lzyyyy_MM_ = "yyyy-MM-".Length;

		// Token: 0x04001176 RID: 4470
		private static readonly int Lzyyyy_MM_dd = "yyyy-MM-dd".Length;

		// Token: 0x04001177 RID: 4471
		private static readonly int Lzyyyy_MM_ddT = "yyyy-MM-ddT".Length;

		// Token: 0x04001178 RID: 4472
		private static readonly int LzHH = "HH".Length;

		// Token: 0x04001179 RID: 4473
		private static readonly int LzHH_ = "HH:".Length;

		// Token: 0x0400117A RID: 4474
		private static readonly int LzHH_mm = "HH:mm".Length;

		// Token: 0x0400117B RID: 4475
		private static readonly int LzHH_mm_ = "HH:mm:".Length;

		// Token: 0x0400117C RID: 4476
		private static readonly int LzHH_mm_ss = "HH:mm:ss".Length;

		// Token: 0x0400117D RID: 4477
		private static readonly int Lz_ = "-".Length;

		// Token: 0x0400117E RID: 4478
		private static readonly int Lz_zz = "-zz".Length;

		// Token: 0x0400117F RID: 4479
		private static readonly int Lz_zz_ = "-zz:".Length;

		// Token: 0x04001180 RID: 4480
		private static readonly int Lz_zz_zz = "-zz:zz".Length;

		// Token: 0x04001181 RID: 4481
		private static readonly int Lz__ = "--".Length;

		// Token: 0x04001182 RID: 4482
		private static readonly int Lz__mm = "--MM".Length;

		// Token: 0x04001183 RID: 4483
		private static readonly int Lz__mm_ = "--MM-".Length;

		// Token: 0x04001184 RID: 4484
		private static readonly int Lz__mm__ = "--MM--".Length;

		// Token: 0x04001185 RID: 4485
		private static readonly int Lz__mm_dd = "--MM-dd".Length;

		// Token: 0x04001186 RID: 4486
		private static readonly int Lz___ = "---".Length;

		// Token: 0x04001187 RID: 4487
		private static readonly int Lz___dd = "---dd".Length;

		// Token: 0x04001188 RID: 4488
		private static readonly XmlTypeCode[] typeCodes = new XmlTypeCode[]
		{
			XmlTypeCode.DateTime,
			XmlTypeCode.Time,
			XmlTypeCode.Date,
			XmlTypeCode.GYearMonth,
			XmlTypeCode.GYear,
			XmlTypeCode.GMonthDay,
			XmlTypeCode.GDay,
			XmlTypeCode.GMonth
		};

		// Token: 0x02000328 RID: 808
		private enum DateTimeTypeCode
		{
			// Token: 0x0400118A RID: 4490
			DateTime,
			// Token: 0x0400118B RID: 4491
			Time,
			// Token: 0x0400118C RID: 4492
			Date,
			// Token: 0x0400118D RID: 4493
			GYearMonth,
			// Token: 0x0400118E RID: 4494
			GYear,
			// Token: 0x0400118F RID: 4495
			GMonthDay,
			// Token: 0x04001190 RID: 4496
			GDay,
			// Token: 0x04001191 RID: 4497
			GMonth,
			// Token: 0x04001192 RID: 4498
			XdrDateTime
		}

		// Token: 0x02000329 RID: 809
		private enum XsdDateTimeKind
		{
			// Token: 0x04001194 RID: 4500
			Unspecified,
			// Token: 0x04001195 RID: 4501
			Zulu,
			// Token: 0x04001196 RID: 4502
			LocalWestOfZulu,
			// Token: 0x04001197 RID: 4503
			LocalEastOfZulu
		}

		// Token: 0x0200032A RID: 810
		private struct Parser
		{
			// Token: 0x060024E3 RID: 9443 RVA: 0x000D0C14 File Offset: 0x000CEE14
			public bool Parse(string text, XsdDateTimeFlags kinds)
			{
				this.text = text;
				this.length = text.Length;
				int num = 0;
				while (num < this.length && char.IsWhiteSpace(text[num]))
				{
					num++;
				}
				if (XsdDateTime.Parser.Test(kinds, XsdDateTimeFlags.DateTime | XsdDateTimeFlags.Date | XsdDateTimeFlags.XdrDateTimeNoTz | XsdDateTimeFlags.XdrDateTime) && this.ParseDate(num))
				{
					if (XsdDateTime.Parser.Test(kinds, XsdDateTimeFlags.DateTime) && this.ParseChar(num + XsdDateTime.Lzyyyy_MM_dd, 'T') && this.ParseTimeAndZoneAndWhitespace(num + XsdDateTime.Lzyyyy_MM_ddT))
					{
						this.typeCode = XsdDateTime.DateTimeTypeCode.DateTime;
						return true;
					}
					if (XsdDateTime.Parser.Test(kinds, XsdDateTimeFlags.Date) && this.ParseZoneAndWhitespace(num + XsdDateTime.Lzyyyy_MM_dd))
					{
						this.typeCode = XsdDateTime.DateTimeTypeCode.Date;
						return true;
					}
					if (XsdDateTime.Parser.Test(kinds, XsdDateTimeFlags.XdrDateTime) && (this.ParseZoneAndWhitespace(num + XsdDateTime.Lzyyyy_MM_dd) || (this.ParseChar(num + XsdDateTime.Lzyyyy_MM_dd, 'T') && this.ParseTimeAndZoneAndWhitespace(num + XsdDateTime.Lzyyyy_MM_ddT))))
					{
						this.typeCode = XsdDateTime.DateTimeTypeCode.XdrDateTime;
						return true;
					}
					if (XsdDateTime.Parser.Test(kinds, XsdDateTimeFlags.XdrDateTimeNoTz))
					{
						if (!this.ParseChar(num + XsdDateTime.Lzyyyy_MM_dd, 'T'))
						{
							this.typeCode = XsdDateTime.DateTimeTypeCode.XdrDateTime;
							return true;
						}
						if (this.ParseTimeAndWhitespace(num + XsdDateTime.Lzyyyy_MM_ddT))
						{
							this.typeCode = XsdDateTime.DateTimeTypeCode.XdrDateTime;
							return true;
						}
					}
				}
				if (XsdDateTime.Parser.Test(kinds, XsdDateTimeFlags.Time) && this.ParseTimeAndZoneAndWhitespace(num))
				{
					this.year = 1904;
					this.month = 1;
					this.day = 1;
					this.typeCode = XsdDateTime.DateTimeTypeCode.Time;
					return true;
				}
				if (XsdDateTime.Parser.Test(kinds, XsdDateTimeFlags.XdrTimeNoTz) && this.ParseTimeAndWhitespace(num))
				{
					this.year = 1904;
					this.month = 1;
					this.day = 1;
					this.typeCode = XsdDateTime.DateTimeTypeCode.Time;
					return true;
				}
				if (XsdDateTime.Parser.Test(kinds, XsdDateTimeFlags.GYearMonth | XsdDateTimeFlags.GYear) && this.Parse4Dig(num, ref this.year) && 1 <= this.year)
				{
					if (XsdDateTime.Parser.Test(kinds, XsdDateTimeFlags.GYearMonth) && this.ParseChar(num + XsdDateTime.Lzyyyy, '-') && this.Parse2Dig(num + XsdDateTime.Lzyyyy_, ref this.month) && 1 <= this.month && this.month <= 12 && this.ParseZoneAndWhitespace(num + XsdDateTime.Lzyyyy_MM))
					{
						this.day = 1;
						this.typeCode = XsdDateTime.DateTimeTypeCode.GYearMonth;
						return true;
					}
					if (XsdDateTime.Parser.Test(kinds, XsdDateTimeFlags.GYear) && this.ParseZoneAndWhitespace(num + XsdDateTime.Lzyyyy))
					{
						this.month = 1;
						this.day = 1;
						this.typeCode = XsdDateTime.DateTimeTypeCode.GYear;
						return true;
					}
				}
				if (XsdDateTime.Parser.Test(kinds, XsdDateTimeFlags.GMonthDay | XsdDateTimeFlags.GMonth) && this.ParseChar(num, '-') && this.ParseChar(num + XsdDateTime.Lz_, '-') && this.Parse2Dig(num + XsdDateTime.Lz__, ref this.month) && 1 <= this.month && this.month <= 12)
				{
					if (XsdDateTime.Parser.Test(kinds, XsdDateTimeFlags.GMonthDay) && this.ParseChar(num + XsdDateTime.Lz__mm, '-') && this.Parse2Dig(num + XsdDateTime.Lz__mm_, ref this.day) && 1 <= this.day && this.day <= DateTime.DaysInMonth(1904, this.month) && this.ParseZoneAndWhitespace(num + XsdDateTime.Lz__mm_dd))
					{
						this.year = 1904;
						this.typeCode = XsdDateTime.DateTimeTypeCode.GMonthDay;
						return true;
					}
					if (XsdDateTime.Parser.Test(kinds, XsdDateTimeFlags.GMonth) && (this.ParseZoneAndWhitespace(num + XsdDateTime.Lz__mm) || (this.ParseChar(num + XsdDateTime.Lz__mm, '-') && this.ParseChar(num + XsdDateTime.Lz__mm_, '-') && this.ParseZoneAndWhitespace(num + XsdDateTime.Lz__mm__))))
					{
						this.year = 1904;
						this.day = 1;
						this.typeCode = XsdDateTime.DateTimeTypeCode.GMonth;
						return true;
					}
				}
				if (XsdDateTime.Parser.Test(kinds, XsdDateTimeFlags.GDay) && this.ParseChar(num, '-') && this.ParseChar(num + XsdDateTime.Lz_, '-') && this.ParseChar(num + XsdDateTime.Lz__, '-') && this.Parse2Dig(num + XsdDateTime.Lz___, ref this.day) && 1 <= this.day && this.day <= DateTime.DaysInMonth(1904, 1) && this.ParseZoneAndWhitespace(num + XsdDateTime.Lz___dd))
				{
					this.year = 1904;
					this.month = 1;
					this.typeCode = XsdDateTime.DateTimeTypeCode.GDay;
					return true;
				}
				return false;
			}

			// Token: 0x060024E4 RID: 9444 RVA: 0x000D1044 File Offset: 0x000CF244
			private bool ParseDate(int start)
			{
				return this.Parse4Dig(start, ref this.year) && 1 <= this.year && this.ParseChar(start + XsdDateTime.Lzyyyy, '-') && this.Parse2Dig(start + XsdDateTime.Lzyyyy_, ref this.month) && 1 <= this.month && this.month <= 12 && this.ParseChar(start + XsdDateTime.Lzyyyy_MM, '-') && this.Parse2Dig(start + XsdDateTime.Lzyyyy_MM_, ref this.day) && 1 <= this.day && this.day <= DateTime.DaysInMonth(this.year, this.month);
			}

			// Token: 0x060024E5 RID: 9445 RVA: 0x000D10F5 File Offset: 0x000CF2F5
			private bool ParseTimeAndZoneAndWhitespace(int start)
			{
				return this.ParseTime(ref start) && this.ParseZoneAndWhitespace(start);
			}

			// Token: 0x060024E6 RID: 9446 RVA: 0x000D110D File Offset: 0x000CF30D
			private bool ParseTimeAndWhitespace(int start)
			{
				if (this.ParseTime(ref start))
				{
					while (start < this.length)
					{
						start++;
					}
					return start == this.length;
				}
				return false;
			}

			// Token: 0x060024E7 RID: 9447 RVA: 0x000D1134 File Offset: 0x000CF334
			private bool ParseTime(ref int start)
			{
				if (this.Parse2Dig(start, ref this.hour) && this.hour < 24 && this.ParseChar(start + XsdDateTime.LzHH, ':') && this.Parse2Dig(start + XsdDateTime.LzHH_, ref this.minute) && this.minute < 60 && this.ParseChar(start + XsdDateTime.LzHH_mm, ':') && this.Parse2Dig(start + XsdDateTime.LzHH_mm_, ref this.second) && this.second < 60)
				{
					start += XsdDateTime.LzHH_mm_ss;
					if (this.ParseChar(start, '.'))
					{
						this.fraction = 0;
						int num = 0;
						int num2 = 0;
						for (;;)
						{
							int num3 = start + 1;
							start = num3;
							if (num3 >= this.length)
							{
								break;
							}
							int num4 = (int)(this.text[start] - '0');
							if (9 < num4)
							{
								break;
							}
							if (num < 7)
							{
								this.fraction = this.fraction * 10 + num4;
							}
							else if (num == 7)
							{
								if (5 < num4)
								{
									num2 = 1;
								}
								else if (num4 == 5)
								{
									num2 = -1;
								}
							}
							else if (num2 < 0 && num4 != 0)
							{
								num2 = 1;
							}
							num++;
						}
						if (num < 7)
						{
							if (num == 0)
							{
								return false;
							}
							this.fraction *= XsdDateTime.Parser.Power10[7 - num];
						}
						else
						{
							if (num2 < 0)
							{
								num2 = this.fraction & 1;
							}
							this.fraction += num2;
						}
					}
					return true;
				}
				this.hour = 0;
				return false;
			}

			// Token: 0x060024E8 RID: 9448 RVA: 0x000D12A4 File Offset: 0x000CF4A4
			private bool ParseZoneAndWhitespace(int start)
			{
				if (start < this.length)
				{
					char c = this.text[start];
					if (c == 'Z' || c == 'z')
					{
						this.kind = XsdDateTime.XsdDateTimeKind.Zulu;
						start++;
					}
					else if (start + 5 < this.length && this.Parse2Dig(start + XsdDateTime.Lz_, ref this.zoneHour) && this.zoneHour <= 99 && this.ParseChar(start + XsdDateTime.Lz_zz, ':') && this.Parse2Dig(start + XsdDateTime.Lz_zz_, ref this.zoneMinute) && this.zoneMinute <= 99)
					{
						if (c == '-')
						{
							this.kind = XsdDateTime.XsdDateTimeKind.LocalWestOfZulu;
							start += XsdDateTime.Lz_zz_zz;
						}
						else if (c == '+')
						{
							this.kind = XsdDateTime.XsdDateTimeKind.LocalEastOfZulu;
							start += XsdDateTime.Lz_zz_zz;
						}
					}
				}
				while (start < this.length && char.IsWhiteSpace(this.text[start]))
				{
					start++;
				}
				return start == this.length;
			}

			// Token: 0x060024E9 RID: 9449 RVA: 0x000D139C File Offset: 0x000CF59C
			private bool Parse4Dig(int start, ref int num)
			{
				if (start + 3 < this.length)
				{
					int num2 = (int)(this.text[start] - '0');
					int num3 = (int)(this.text[start + 1] - '0');
					int num4 = (int)(this.text[start + 2] - '0');
					int num5 = (int)(this.text[start + 3] - '0');
					if (0 <= num2 && num2 < 10 && 0 <= num3 && num3 < 10 && 0 <= num4 && num4 < 10 && 0 <= num5 && num5 < 10)
					{
						num = ((num2 * 10 + num3) * 10 + num4) * 10 + num5;
						return true;
					}
				}
				return false;
			}

			// Token: 0x060024EA RID: 9450 RVA: 0x000D1434 File Offset: 0x000CF634
			private bool Parse2Dig(int start, ref int num)
			{
				if (start + 1 < this.length)
				{
					int num2 = (int)(this.text[start] - '0');
					int num3 = (int)(this.text[start + 1] - '0');
					if (0 <= num2 && num2 < 10 && 0 <= num3 && num3 < 10)
					{
						num = num2 * 10 + num3;
						return true;
					}
				}
				return false;
			}

			// Token: 0x060024EB RID: 9451 RVA: 0x000D148B File Offset: 0x000CF68B
			private bool ParseChar(int start, char ch)
			{
				return start < this.length && this.text[start] == ch;
			}

			// Token: 0x060024EC RID: 9452 RVA: 0x000D14A7 File Offset: 0x000CF6A7
			private static bool Test(XsdDateTimeFlags left, XsdDateTimeFlags right)
			{
				return (left & right) > (XsdDateTimeFlags)0;
			}

			// Token: 0x04001198 RID: 4504
			public XsdDateTime.DateTimeTypeCode typeCode;

			// Token: 0x04001199 RID: 4505
			public int year;

			// Token: 0x0400119A RID: 4506
			public int month;

			// Token: 0x0400119B RID: 4507
			public int day;

			// Token: 0x0400119C RID: 4508
			public int hour;

			// Token: 0x0400119D RID: 4509
			public int minute;

			// Token: 0x0400119E RID: 4510
			public int second;

			// Token: 0x0400119F RID: 4511
			public int fraction;

			// Token: 0x040011A0 RID: 4512
			public XsdDateTime.XsdDateTimeKind kind;

			// Token: 0x040011A1 RID: 4513
			public int zoneHour;

			// Token: 0x040011A2 RID: 4514
			public int zoneMinute;

			// Token: 0x040011A3 RID: 4515
			private string text;

			// Token: 0x040011A4 RID: 4516
			private int length;

			// Token: 0x040011A5 RID: 4517
			private static int[] Power10 = new int[] { -1, 10, 100, 1000, 10000, 100000, 1000000 };
		}
	}
}
