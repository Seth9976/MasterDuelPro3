using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x020000A5 RID: 165
	[NullableContext(1)]
	[Nullable(0)]
	internal struct DateTimeParser
	{
		// Token: 0x0600054E RID: 1358 RVA: 0x0001CEB6 File Offset: 0x0001B0B6
		public bool Parse(char[] text, int startIndex, int length)
		{
			this._text = text;
			this._end = startIndex + length;
			return this.ParseDate(startIndex) && this.ParseChar(DateTimeParser.Lzyyyy_MM_dd + startIndex, 'T') && this.ParseTimeAndZoneAndWhitespace(DateTimeParser.Lzyyyy_MM_ddT + startIndex);
		}

		// Token: 0x0600054F RID: 1359 RVA: 0x0001CEF4 File Offset: 0x0001B0F4
		private bool ParseDate(int start)
		{
			return this.Parse4Digit(start, out this.Year) && 1 <= this.Year && this.ParseChar(start + DateTimeParser.Lzyyyy, '-') && this.Parse2Digit(start + DateTimeParser.Lzyyyy_, out this.Month) && 1 <= this.Month && this.Month <= 12 && this.ParseChar(start + DateTimeParser.Lzyyyy_MM, '-') && this.Parse2Digit(start + DateTimeParser.Lzyyyy_MM_, out this.Day) && 1 <= this.Day && this.Day <= DateTime.DaysInMonth(this.Year, this.Month);
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x0001CFA5 File Offset: 0x0001B1A5
		private bool ParseTimeAndZoneAndWhitespace(int start)
		{
			return this.ParseTime(ref start) && this.ParseZone(start);
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x0001CFBC File Offset: 0x0001B1BC
		private bool ParseTime(ref int start)
		{
			if (!this.Parse2Digit(start, out this.Hour) || this.Hour > 24 || !this.ParseChar(start + DateTimeParser.LzHH, ':') || !this.Parse2Digit(start + DateTimeParser.LzHH_, out this.Minute) || this.Minute >= 60 || !this.ParseChar(start + DateTimeParser.LzHH_mm, ':') || !this.Parse2Digit(start + DateTimeParser.LzHH_mm_, out this.Second) || this.Second >= 60 || (this.Hour == 24 && (this.Minute != 0 || this.Second != 0)))
			{
				return false;
			}
			start += DateTimeParser.LzHH_mm_ss;
			if (this.ParseChar(start, '.'))
			{
				this.Fraction = 0;
				int num = 0;
				for (;;)
				{
					int num2 = start + 1;
					start = num2;
					if (num2 >= this._end || num >= 7)
					{
						break;
					}
					int num3 = (int)(this._text[start] - '0');
					if (num3 < 0 || num3 > 9)
					{
						break;
					}
					this.Fraction = this.Fraction * 10 + num3;
					num++;
				}
				if (num < 7)
				{
					if (num == 0)
					{
						return false;
					}
					this.Fraction *= DateTimeParser.Power10[7 - num];
				}
				if (this.Hour == 24 && this.Fraction != 0)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x0001D0FC File Offset: 0x0001B2FC
		private bool ParseZone(int start)
		{
			if (start < this._end)
			{
				char c = this._text[start];
				if (c == 'Z' || c == 'z')
				{
					this.Zone = ParserTimeZone.Utc;
					start++;
				}
				else
				{
					if (start + 2 < this._end && this.Parse2Digit(start + DateTimeParser.Lz_, out this.ZoneHour) && this.ZoneHour <= 99)
					{
						if (c != '+')
						{
							if (c == '-')
							{
								this.Zone = ParserTimeZone.LocalWestOfUtc;
								start += DateTimeParser.Lz_zz;
							}
						}
						else
						{
							this.Zone = ParserTimeZone.LocalEastOfUtc;
							start += DateTimeParser.Lz_zz;
						}
					}
					if (start < this._end)
					{
						if (this.ParseChar(start, ':'))
						{
							start++;
							if (start + 1 < this._end && this.Parse2Digit(start, out this.ZoneMinute) && this.ZoneMinute <= 99)
							{
								start += 2;
							}
						}
						else if (start + 1 < this._end && this.Parse2Digit(start, out this.ZoneMinute) && this.ZoneMinute <= 99)
						{
							start += 2;
						}
					}
				}
			}
			return start == this._end;
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x0001D208 File Offset: 0x0001B408
		private bool Parse4Digit(int start, out int num)
		{
			if (start + 3 < this._end)
			{
				int num2 = (int)(this._text[start] - '0');
				int num3 = (int)(this._text[start + 1] - '0');
				int num4 = (int)(this._text[start + 2] - '0');
				int num5 = (int)(this._text[start + 3] - '0');
				if (0 <= num2 && num2 < 10 && 0 <= num3 && num3 < 10 && 0 <= num4 && num4 < 10 && 0 <= num5 && num5 < 10)
				{
					num = ((num2 * 10 + num3) * 10 + num4) * 10 + num5;
					return true;
				}
			}
			num = 0;
			return false;
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x0001D294 File Offset: 0x0001B494
		private bool Parse2Digit(int start, out int num)
		{
			if (start + 1 < this._end)
			{
				int num2 = (int)(this._text[start] - '0');
				int num3 = (int)(this._text[start + 1] - '0');
				if (0 <= num2 && num2 < 10 && 0 <= num3 && num3 < 10)
				{
					num = num2 * 10 + num3;
					return true;
				}
			}
			num = 0;
			return false;
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x0001D2E6 File Offset: 0x0001B4E6
		private bool ParseChar(int start, char ch)
		{
			return start < this._end && this._text[start] == ch;
		}

		// Token: 0x040003E1 RID: 993
		public int Year;

		// Token: 0x040003E2 RID: 994
		public int Month;

		// Token: 0x040003E3 RID: 995
		public int Day;

		// Token: 0x040003E4 RID: 996
		public int Hour;

		// Token: 0x040003E5 RID: 997
		public int Minute;

		// Token: 0x040003E6 RID: 998
		public int Second;

		// Token: 0x040003E7 RID: 999
		public int Fraction;

		// Token: 0x040003E8 RID: 1000
		public int ZoneHour;

		// Token: 0x040003E9 RID: 1001
		public int ZoneMinute;

		// Token: 0x040003EA RID: 1002
		public ParserTimeZone Zone;

		// Token: 0x040003EB RID: 1003
		private char[] _text;

		// Token: 0x040003EC RID: 1004
		private int _end;

		// Token: 0x040003ED RID: 1005
		private static readonly int[] Power10 = new int[] { -1, 10, 100, 1000, 10000, 100000, 1000000 };

		// Token: 0x040003EE RID: 1006
		private static readonly int Lzyyyy = "yyyy".Length;

		// Token: 0x040003EF RID: 1007
		private static readonly int Lzyyyy_ = "yyyy-".Length;

		// Token: 0x040003F0 RID: 1008
		private static readonly int Lzyyyy_MM = "yyyy-MM".Length;

		// Token: 0x040003F1 RID: 1009
		private static readonly int Lzyyyy_MM_ = "yyyy-MM-".Length;

		// Token: 0x040003F2 RID: 1010
		private static readonly int Lzyyyy_MM_dd = "yyyy-MM-dd".Length;

		// Token: 0x040003F3 RID: 1011
		private static readonly int Lzyyyy_MM_ddT = "yyyy-MM-ddT".Length;

		// Token: 0x040003F4 RID: 1012
		private static readonly int LzHH = "HH".Length;

		// Token: 0x040003F5 RID: 1013
		private static readonly int LzHH_ = "HH:".Length;

		// Token: 0x040003F6 RID: 1014
		private static readonly int LzHH_mm = "HH:mm".Length;

		// Token: 0x040003F7 RID: 1015
		private static readonly int LzHH_mm_ = "HH:mm:".Length;

		// Token: 0x040003F8 RID: 1016
		private static readonly int LzHH_mm_ss = "HH:mm:ss".Length;

		// Token: 0x040003F9 RID: 1017
		private static readonly int Lz_ = "-".Length;

		// Token: 0x040003FA RID: 1018
		private static readonly int Lz_zz = "-zz".Length;

		// Token: 0x040003FB RID: 1019
		private const short MaxFractionDigits = 7;
	}
}
