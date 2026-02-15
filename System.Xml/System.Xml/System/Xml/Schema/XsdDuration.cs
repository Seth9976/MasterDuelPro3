using System;
using System.Text;

namespace System.Xml.Schema
{
	// Token: 0x0200032B RID: 811
	internal struct XsdDuration
	{
		// Token: 0x060024EE RID: 9454 RVA: 0x000D14C8 File Offset: 0x000CF6C8
		public XsdDuration(bool isNegative, int years, int months, int days, int hours, int minutes, int seconds, int nanoseconds)
		{
			if (years < 0)
			{
				throw new ArgumentOutOfRangeException("years");
			}
			if (months < 0)
			{
				throw new ArgumentOutOfRangeException("months");
			}
			if (days < 0)
			{
				throw new ArgumentOutOfRangeException("days");
			}
			if (hours < 0)
			{
				throw new ArgumentOutOfRangeException("hours");
			}
			if (minutes < 0)
			{
				throw new ArgumentOutOfRangeException("minutes");
			}
			if (seconds < 0)
			{
				throw new ArgumentOutOfRangeException("seconds");
			}
			if (nanoseconds < 0 || nanoseconds > 999999999)
			{
				throw new ArgumentOutOfRangeException("nanoseconds");
			}
			this.years = years;
			this.months = months;
			this.days = days;
			this.hours = hours;
			this.minutes = minutes;
			this.seconds = seconds;
			this.nanoseconds = (uint)nanoseconds;
			if (isNegative)
			{
				this.nanoseconds |= 2147483648U;
			}
		}

		// Token: 0x060024EF RID: 9455 RVA: 0x000D1597 File Offset: 0x000CF797
		public XsdDuration(TimeSpan timeSpan)
		{
			this = new XsdDuration(timeSpan, XsdDuration.DurationType.Duration);
		}

		// Token: 0x060024F0 RID: 9456 RVA: 0x000D15A4 File Offset: 0x000CF7A4
		public XsdDuration(TimeSpan timeSpan, XsdDuration.DurationType durationType)
		{
			long ticks = timeSpan.Ticks;
			bool flag;
			ulong num;
			if (ticks < 0L)
			{
				flag = true;
				num = (ulong)(-(ulong)ticks);
			}
			else
			{
				flag = false;
				num = (ulong)ticks;
			}
			if (durationType == XsdDuration.DurationType.YearMonthDuration)
			{
				int num2 = (int)(num / 315360000000000UL);
				int num3 = (int)(num % 315360000000000UL / 25920000000000UL);
				if (num3 == 12)
				{
					num2++;
					num3 = 0;
				}
				this = new XsdDuration(flag, num2, num3, 0, 0, 0, 0, 0);
				return;
			}
			this.nanoseconds = (uint)(num % 10000000UL) * 100U;
			if (flag)
			{
				this.nanoseconds |= 2147483648U;
			}
			this.years = 0;
			this.months = 0;
			this.days = (int)(num / 864000000000UL);
			this.hours = (int)(num / 36000000000UL % 24UL);
			this.minutes = (int)(num / 600000000UL % 60UL);
			this.seconds = (int)(num / 10000000UL % 60UL);
		}

		// Token: 0x060024F1 RID: 9457 RVA: 0x000D1697 File Offset: 0x000CF897
		public XsdDuration(string s)
		{
			this = new XsdDuration(s, XsdDuration.DurationType.Duration);
		}

		// Token: 0x060024F2 RID: 9458 RVA: 0x000D16A4 File Offset: 0x000CF8A4
		public XsdDuration(string s, XsdDuration.DurationType durationType)
		{
			XsdDuration xsdDuration;
			Exception ex = XsdDuration.TryParse(s, durationType, out xsdDuration);
			if (ex != null)
			{
				throw ex;
			}
			this.years = xsdDuration.Years;
			this.months = xsdDuration.Months;
			this.days = xsdDuration.Days;
			this.hours = xsdDuration.Hours;
			this.minutes = xsdDuration.Minutes;
			this.seconds = xsdDuration.Seconds;
			this.nanoseconds = (uint)xsdDuration.Nanoseconds;
			if (xsdDuration.IsNegative)
			{
				this.nanoseconds |= 2147483648U;
			}
		}

		// Token: 0x170008A8 RID: 2216
		// (get) Token: 0x060024F3 RID: 9459 RVA: 0x000D1736 File Offset: 0x000CF936
		public bool IsNegative
		{
			get
			{
				return (this.nanoseconds & 2147483648U) > 0U;
			}
		}

		// Token: 0x170008A9 RID: 2217
		// (get) Token: 0x060024F4 RID: 9460 RVA: 0x000D1747 File Offset: 0x000CF947
		public int Years
		{
			get
			{
				return this.years;
			}
		}

		// Token: 0x170008AA RID: 2218
		// (get) Token: 0x060024F5 RID: 9461 RVA: 0x000D174F File Offset: 0x000CF94F
		public int Months
		{
			get
			{
				return this.months;
			}
		}

		// Token: 0x170008AB RID: 2219
		// (get) Token: 0x060024F6 RID: 9462 RVA: 0x000D1757 File Offset: 0x000CF957
		public int Days
		{
			get
			{
				return this.days;
			}
		}

		// Token: 0x170008AC RID: 2220
		// (get) Token: 0x060024F7 RID: 9463 RVA: 0x000D175F File Offset: 0x000CF95F
		public int Hours
		{
			get
			{
				return this.hours;
			}
		}

		// Token: 0x170008AD RID: 2221
		// (get) Token: 0x060024F8 RID: 9464 RVA: 0x000D1767 File Offset: 0x000CF967
		public int Minutes
		{
			get
			{
				return this.minutes;
			}
		}

		// Token: 0x170008AE RID: 2222
		// (get) Token: 0x060024F9 RID: 9465 RVA: 0x000D176F File Offset: 0x000CF96F
		public int Seconds
		{
			get
			{
				return this.seconds;
			}
		}

		// Token: 0x170008AF RID: 2223
		// (get) Token: 0x060024FA RID: 9466 RVA: 0x000D1777 File Offset: 0x000CF977
		public int Nanoseconds
		{
			get
			{
				return (int)(this.nanoseconds & 2147483647U);
			}
		}

		// Token: 0x060024FB RID: 9467 RVA: 0x000D1785 File Offset: 0x000CF985
		public TimeSpan ToTimeSpan()
		{
			return this.ToTimeSpan(XsdDuration.DurationType.Duration);
		}

		// Token: 0x060024FC RID: 9468 RVA: 0x000D1790 File Offset: 0x000CF990
		public TimeSpan ToTimeSpan(XsdDuration.DurationType durationType)
		{
			TimeSpan timeSpan;
			Exception ex = this.TryToTimeSpan(durationType, out timeSpan);
			if (ex != null)
			{
				throw ex;
			}
			return timeSpan;
		}

		// Token: 0x060024FD RID: 9469 RVA: 0x000D17AD File Offset: 0x000CF9AD
		internal Exception TryToTimeSpan(out TimeSpan result)
		{
			return this.TryToTimeSpan(XsdDuration.DurationType.Duration, out result);
		}

		// Token: 0x060024FE RID: 9470 RVA: 0x000D17B8 File Offset: 0x000CF9B8
		internal Exception TryToTimeSpan(XsdDuration.DurationType durationType, out TimeSpan result)
		{
			Exception ex = null;
			ulong num = 0UL;
			checked
			{
				try
				{
					if (durationType != XsdDuration.DurationType.DayTimeDuration)
					{
						num += ((ulong)this.years + (ulong)this.months / 12UL) * 365UL;
						num += (ulong)this.months % 12UL * 30UL;
					}
					if (durationType != XsdDuration.DurationType.YearMonthDuration)
					{
						num += (ulong)this.days;
						num *= 24UL;
						num += (ulong)this.hours;
						num *= 60UL;
						num += (ulong)this.minutes;
						num *= 60UL;
						num += (ulong)this.seconds;
						num *= 10000000UL;
						num += (ulong)this.Nanoseconds / 100UL;
					}
					else
					{
						num *= 864000000000UL;
					}
					if (this.IsNegative)
					{
						if (num == 9223372036854775808UL)
						{
							result = new TimeSpan(long.MinValue);
						}
						else
						{
							result = new TimeSpan(0L - (long)num);
						}
					}
					else
					{
						result = new TimeSpan((long)num);
					}
					return null;
				}
				catch (OverflowException)
				{
					result = TimeSpan.MinValue;
					ex = new OverflowException(Res.GetString("Value '{0}' was either too large or too small for {1}.", new object[] { durationType, "TimeSpan" }));
				}
				return ex;
			}
		}

		// Token: 0x060024FF RID: 9471 RVA: 0x000D18F8 File Offset: 0x000CFAF8
		public override string ToString()
		{
			return this.ToString(XsdDuration.DurationType.Duration);
		}

		// Token: 0x06002500 RID: 9472 RVA: 0x000D1904 File Offset: 0x000CFB04
		internal string ToString(XsdDuration.DurationType durationType)
		{
			StringBuilder stringBuilder = new StringBuilder(20);
			if (this.IsNegative)
			{
				stringBuilder.Append('-');
			}
			stringBuilder.Append('P');
			if (durationType != XsdDuration.DurationType.DayTimeDuration)
			{
				if (this.years != 0)
				{
					stringBuilder.Append(XmlConvert.ToString(this.years));
					stringBuilder.Append('Y');
				}
				if (this.months != 0)
				{
					stringBuilder.Append(XmlConvert.ToString(this.months));
					stringBuilder.Append('M');
				}
			}
			if (durationType != XsdDuration.DurationType.YearMonthDuration)
			{
				if (this.days != 0)
				{
					stringBuilder.Append(XmlConvert.ToString(this.days));
					stringBuilder.Append('D');
				}
				if (this.hours != 0 || this.minutes != 0 || this.seconds != 0 || this.Nanoseconds != 0)
				{
					stringBuilder.Append('T');
					if (this.hours != 0)
					{
						stringBuilder.Append(XmlConvert.ToString(this.hours));
						stringBuilder.Append('H');
					}
					if (this.minutes != 0)
					{
						stringBuilder.Append(XmlConvert.ToString(this.minutes));
						stringBuilder.Append('M');
					}
					int num = this.Nanoseconds;
					if (this.seconds != 0 || num != 0)
					{
						stringBuilder.Append(XmlConvert.ToString(this.seconds));
						if (num != 0)
						{
							stringBuilder.Append('.');
							int length = stringBuilder.Length;
							stringBuilder.Length += 9;
							int num2 = stringBuilder.Length - 1;
							for (int i = num2; i >= length; i--)
							{
								int num3 = num % 10;
								stringBuilder[i] = (char)(num3 + 48);
								if (num2 == i && num3 == 0)
								{
									num2--;
								}
								num /= 10;
							}
							stringBuilder.Length = num2 + 1;
						}
						stringBuilder.Append('S');
					}
				}
				if (stringBuilder[stringBuilder.Length - 1] == 'P')
				{
					stringBuilder.Append("T0S");
				}
			}
			else if (stringBuilder[stringBuilder.Length - 1] == 'P')
			{
				stringBuilder.Append("0M");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06002501 RID: 9473 RVA: 0x000D1AF6 File Offset: 0x000CFCF6
		internal static Exception TryParse(string s, out XsdDuration result)
		{
			return XsdDuration.TryParse(s, XsdDuration.DurationType.Duration, out result);
		}

		// Token: 0x06002502 RID: 9474 RVA: 0x000D1B00 File Offset: 0x000CFD00
		internal static Exception TryParse(string s, XsdDuration.DurationType durationType, out XsdDuration result)
		{
			XsdDuration.Parts parts = XsdDuration.Parts.HasNone;
			result = default(XsdDuration);
			s = s.Trim();
			int length = s.Length;
			int num = 0;
			int i = 0;
			if (num < length)
			{
				if (s[num] == '-')
				{
					num++;
					result.nanoseconds = 2147483648U;
				}
				else
				{
					result.nanoseconds = 0U;
				}
				if (num < length && s[num++] == 'P')
				{
					int num2;
					if (XsdDuration.TryParseDigits(s, ref num, false, out num2, out i) == null)
					{
						if (num >= length)
						{
							goto IL_02B5;
						}
						if (s[num] == 'Y')
						{
							if (i == 0)
							{
								goto IL_02B5;
							}
							parts |= XsdDuration.Parts.HasYears;
							result.years = num2;
							if (++num == length)
							{
								goto IL_0298;
							}
							if (XsdDuration.TryParseDigits(s, ref num, false, out num2, out i) != null)
							{
								goto IL_02D8;
							}
							if (num >= length)
							{
								goto IL_02B5;
							}
						}
						if (s[num] == 'M')
						{
							if (i == 0)
							{
								goto IL_02B5;
							}
							parts |= XsdDuration.Parts.HasMonths;
							result.months = num2;
							if (++num == length)
							{
								goto IL_0298;
							}
							if (XsdDuration.TryParseDigits(s, ref num, false, out num2, out i) != null)
							{
								goto IL_02D8;
							}
							if (num >= length)
							{
								goto IL_02B5;
							}
						}
						if (s[num] == 'D')
						{
							if (i == 0)
							{
								goto IL_02B5;
							}
							parts |= XsdDuration.Parts.HasDays;
							result.days = num2;
							if (++num == length)
							{
								goto IL_0298;
							}
							if (XsdDuration.TryParseDigits(s, ref num, false, out num2, out i) != null)
							{
								goto IL_02D8;
							}
							if (num >= length)
							{
								goto IL_02B5;
							}
						}
						if (s[num] == 'T')
						{
							if (i != 0)
							{
								goto IL_02B5;
							}
							num++;
							if (XsdDuration.TryParseDigits(s, ref num, false, out num2, out i) != null)
							{
								goto IL_02D8;
							}
							if (num >= length)
							{
								goto IL_02B5;
							}
							if (s[num] == 'H')
							{
								if (i == 0)
								{
									goto IL_02B5;
								}
								parts |= XsdDuration.Parts.HasHours;
								result.hours = num2;
								if (++num == length)
								{
									goto IL_0298;
								}
								if (XsdDuration.TryParseDigits(s, ref num, false, out num2, out i) != null)
								{
									goto IL_02D8;
								}
								if (num >= length)
								{
									goto IL_02B5;
								}
							}
							if (s[num] == 'M')
							{
								if (i == 0)
								{
									goto IL_02B5;
								}
								parts |= XsdDuration.Parts.HasMinutes;
								result.minutes = num2;
								if (++num == length)
								{
									goto IL_0298;
								}
								if (XsdDuration.TryParseDigits(s, ref num, false, out num2, out i) != null)
								{
									goto IL_02D8;
								}
								if (num >= length)
								{
									goto IL_02B5;
								}
							}
							if (s[num] == '.')
							{
								num++;
								parts |= XsdDuration.Parts.HasSeconds;
								result.seconds = num2;
								if (XsdDuration.TryParseDigits(s, ref num, true, out num2, out i) != null)
								{
									goto IL_02D8;
								}
								if (i == 0)
								{
									num2 = 0;
								}
								while (i > 9)
								{
									num2 /= 10;
									i--;
								}
								while (i < 9)
								{
									num2 *= 10;
									i++;
								}
								result.nanoseconds |= (uint)num2;
								if (num >= length || s[num] != 'S')
								{
									goto IL_02B5;
								}
								if (++num == length)
								{
									goto IL_0298;
								}
							}
							else if (s[num] == 'S')
							{
								if (i == 0)
								{
									goto IL_02B5;
								}
								parts |= XsdDuration.Parts.HasSeconds;
								result.seconds = num2;
								if (++num == length)
								{
									goto IL_0298;
								}
							}
						}
						if (i != 0 || num != length)
						{
							goto IL_02B5;
						}
						IL_0298:
						if (parts != XsdDuration.Parts.HasNone)
						{
							if (durationType == XsdDuration.DurationType.DayTimeDuration)
							{
								if ((parts & (XsdDuration.Parts)3) != XsdDuration.Parts.HasNone)
								{
									goto IL_02B5;
								}
							}
							else if (durationType == XsdDuration.DurationType.YearMonthDuration && (parts & (XsdDuration.Parts)(-4)) != XsdDuration.Parts.HasNone)
							{
								goto IL_02B5;
							}
							return null;
						}
						goto IL_02B5;
					}
					IL_02D8:
					return new OverflowException(Res.GetString("Value '{0}' was either too large or too small for {1}.", new object[] { s, durationType }));
				}
			}
			IL_02B5:
			return new FormatException(Res.GetString("The string '{0}' is not a valid {1} value.", new object[] { s, durationType }));
		}

		// Token: 0x06002503 RID: 9475 RVA: 0x000D1E08 File Offset: 0x000D0008
		private static string TryParseDigits(string s, ref int offset, bool eatDigits, out int result, out int numDigits)
		{
			int num = offset;
			int length = s.Length;
			result = 0;
			numDigits = 0;
			while (offset < length && s[offset] >= '0' && s[offset] <= '9')
			{
				int num2 = (int)(s[offset] - '0');
				if (result > (2147483647 - num2) / 10)
				{
					if (!eatDigits)
					{
						return "Value '{0}' was either too large or too small for {1}.";
					}
					numDigits = offset - num;
					while (offset < length && s[offset] >= '0' && s[offset] <= '9')
					{
						offset++;
					}
					return null;
				}
				else
				{
					result = result * 10 + num2;
					offset++;
				}
			}
			numDigits = offset - num;
			return null;
		}

		// Token: 0x040011A6 RID: 4518
		private int years;

		// Token: 0x040011A7 RID: 4519
		private int months;

		// Token: 0x040011A8 RID: 4520
		private int days;

		// Token: 0x040011A9 RID: 4521
		private int hours;

		// Token: 0x040011AA RID: 4522
		private int minutes;

		// Token: 0x040011AB RID: 4523
		private int seconds;

		// Token: 0x040011AC RID: 4524
		private uint nanoseconds;

		// Token: 0x0200032C RID: 812
		private enum Parts
		{
			// Token: 0x040011AE RID: 4526
			HasNone,
			// Token: 0x040011AF RID: 4527
			HasYears,
			// Token: 0x040011B0 RID: 4528
			HasMonths,
			// Token: 0x040011B1 RID: 4529
			HasDays = 4,
			// Token: 0x040011B2 RID: 4530
			HasHours = 8,
			// Token: 0x040011B3 RID: 4531
			HasMinutes = 16,
			// Token: 0x040011B4 RID: 4532
			HasSeconds = 32
		}

		// Token: 0x0200032D RID: 813
		public enum DurationType
		{
			// Token: 0x040011B6 RID: 4534
			Duration,
			// Token: 0x040011B7 RID: 4535
			YearMonthDuration,
			// Token: 0x040011B8 RID: 4536
			DayTimeDuration
		}
	}
}
