using System;

namespace System.Globalization
{
	// Token: 0x020006A7 RID: 1703
	internal static class TimeSpanParse
	{
		// Token: 0x06003547 RID: 13639 RVA: 0x000CCF78 File Offset: 0x000CB178
		internal static long Pow10(int pow)
		{
			switch (pow)
			{
			case 0:
				return 1L;
			case 1:
				return 10L;
			case 2:
				return 100L;
			case 3:
				return 1000L;
			case 4:
				return 10000L;
			case 5:
				return 100000L;
			case 6:
				return 1000000L;
			case 7:
				return 10000000L;
			default:
				return (long)Math.Pow(10.0, (double)pow);
			}
		}

		// Token: 0x06003548 RID: 13640 RVA: 0x000CCFEC File Offset: 0x000CB1EC
		private static bool TryTimeToTicks(bool positive, TimeSpanParse.TimeSpanToken days, TimeSpanParse.TimeSpanToken hours, TimeSpanParse.TimeSpanToken minutes, TimeSpanParse.TimeSpanToken seconds, TimeSpanParse.TimeSpanToken fraction, out long result)
		{
			if (days._num > 10675199 || hours._num > 23 || minutes._num > 59 || seconds._num > 59 || fraction.IsInvalidFraction())
			{
				result = 0L;
				return false;
			}
			long num = ((long)days._num * 3600L * 24L + (long)hours._num * 3600L + (long)minutes._num * 60L + (long)seconds._num) * 1000L;
			if (num > 922337203685477L || num < -922337203685477L)
			{
				result = 0L;
				return false;
			}
			long num2 = (long)fraction._num;
			if (num2 != 0L)
			{
				long num3 = 1000000L;
				if (fraction._zeroes > 0)
				{
					long num4 = TimeSpanParse.Pow10(fraction._zeroes);
					num3 /= num4;
				}
				while (num2 < num3)
				{
					num2 *= 10L;
				}
			}
			result = num * 10000L + num2;
			if (positive && result < 0L)
			{
				result = 0L;
				return false;
			}
			return true;
		}

		// Token: 0x06003549 RID: 13641 RVA: 0x000CD0EC File Offset: 0x000CB2EC
		internal static TimeSpan Parse(ReadOnlySpan<char> input, IFormatProvider formatProvider)
		{
			TimeSpanParse.TimeSpanResult timeSpanResult = new TimeSpanParse.TimeSpanResult(true);
			TimeSpanParse.TryParseTimeSpan(input, TimeSpanParse.TimeSpanStandardStyles.Any, formatProvider, ref timeSpanResult);
			return timeSpanResult.parsedTimeSpan;
		}

		// Token: 0x0600354A RID: 13642 RVA: 0x000CD114 File Offset: 0x000CB314
		private static bool TryParseTimeSpan(ReadOnlySpan<char> input, TimeSpanParse.TimeSpanStandardStyles style, IFormatProvider formatProvider, ref TimeSpanParse.TimeSpanResult result)
		{
			input = input.Trim();
			if (input.IsEmpty)
			{
				return result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "String was not recognized as a valid TimeSpan.", null, null);
			}
			TimeSpanParse.TimeSpanTokenizer timeSpanTokenizer = new TimeSpanParse.TimeSpanTokenizer(input);
			TimeSpanParse.TimeSpanRawInfo timeSpanRawInfo = default(TimeSpanParse.TimeSpanRawInfo);
			timeSpanRawInfo.Init(DateTimeFormatInfo.GetInstance(formatProvider));
			TimeSpanParse.TimeSpanToken timeSpanToken = timeSpanTokenizer.GetNextToken();
			while (timeSpanToken._ttt != TimeSpanParse.TTT.End)
			{
				if (!timeSpanRawInfo.ProcessToken(ref timeSpanToken, ref result))
				{
					return result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "String was not recognized as a valid TimeSpan.", null, null);
				}
				timeSpanToken = timeSpanTokenizer.GetNextToken();
			}
			return TimeSpanParse.ProcessTerminalState(ref timeSpanRawInfo, style, ref result) || result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "String was not recognized as a valid TimeSpan.", null, null);
		}

		// Token: 0x0600354B RID: 13643 RVA: 0x000CD1B0 File Offset: 0x000CB3B0
		private static bool ProcessTerminalState(ref TimeSpanParse.TimeSpanRawInfo raw, TimeSpanParse.TimeSpanStandardStyles style, ref TimeSpanParse.TimeSpanResult result)
		{
			if (raw._lastSeenTTT == TimeSpanParse.TTT.Num)
			{
				TimeSpanParse.TimeSpanToken timeSpanToken = default(TimeSpanParse.TimeSpanToken);
				timeSpanToken._ttt = TimeSpanParse.TTT.Sep;
				if (!raw.ProcessToken(ref timeSpanToken, ref result))
				{
					return result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "String was not recognized as a valid TimeSpan.", null, null);
				}
			}
			switch (raw._numCount)
			{
			case 1:
				return TimeSpanParse.ProcessTerminal_D(ref raw, style, ref result);
			case 2:
				return TimeSpanParse.ProcessTerminal_HM(ref raw, style, ref result);
			case 3:
				return TimeSpanParse.ProcessTerminal_HM_S_D(ref raw, style, ref result);
			case 4:
				return TimeSpanParse.ProcessTerminal_HMS_F_D(ref raw, style, ref result);
			case 5:
				return TimeSpanParse.ProcessTerminal_DHMSF(ref raw, style, ref result);
			default:
				return result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "String was not recognized as a valid TimeSpan.", null, null);
			}
		}

		// Token: 0x0600354C RID: 13644 RVA: 0x000CD250 File Offset: 0x000CB450
		private static bool ProcessTerminal_DHMSF(ref TimeSpanParse.TimeSpanRawInfo raw, TimeSpanParse.TimeSpanStandardStyles style, ref TimeSpanParse.TimeSpanResult result)
		{
			if (raw._sepCount != 6)
			{
				return result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "String was not recognized as a valid TimeSpan.", null, null);
			}
			bool flag = (style & TimeSpanParse.TimeSpanStandardStyles.Invariant) > TimeSpanParse.TimeSpanStandardStyles.None;
			bool flag2 = (style & TimeSpanParse.TimeSpanStandardStyles.Localized) > TimeSpanParse.TimeSpanStandardStyles.None;
			bool flag3 = false;
			bool flag4 = false;
			if (flag)
			{
				if (raw.FullMatch(raw.PositiveInvariant))
				{
					flag4 = true;
					flag3 = true;
				}
				if (!flag4 && raw.FullMatch(raw.NegativeInvariant))
				{
					flag4 = true;
					flag3 = false;
				}
			}
			if (flag2)
			{
				if (!flag4 && raw.FullMatch(raw.PositiveLocalized))
				{
					flag4 = true;
					flag3 = true;
				}
				if (!flag4 && raw.FullMatch(raw.NegativeLocalized))
				{
					flag4 = true;
					flag3 = false;
				}
			}
			if (!flag4)
			{
				return result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "String was not recognized as a valid TimeSpan.", null, null);
			}
			long num;
			if (!TimeSpanParse.TryTimeToTicks(flag3, raw._numbers0, raw._numbers1, raw._numbers2, raw._numbers3, raw._numbers4, out num))
			{
				return result.SetFailure(TimeSpanParse.ParseFailureKind.Overflow, "The TimeSpan could not be parsed because at least one of the numeric components is out of range or contains too many digits.", null, null);
			}
			if (!flag3)
			{
				num = -num;
				if (num > 0L)
				{
					return result.SetFailure(TimeSpanParse.ParseFailureKind.Overflow, "The TimeSpan could not be parsed because at least one of the numeric components is out of range or contains too many digits.", null, null);
				}
			}
			result.parsedTimeSpan = new TimeSpan(num);
			return true;
		}

		// Token: 0x0600354D RID: 13645 RVA: 0x000CD34C File Offset: 0x000CB54C
		private static bool ProcessTerminal_HMS_F_D(ref TimeSpanParse.TimeSpanRawInfo raw, TimeSpanParse.TimeSpanStandardStyles style, ref TimeSpanParse.TimeSpanResult result)
		{
			if (raw._sepCount != 5 || (style & TimeSpanParse.TimeSpanStandardStyles.RequireFull) != TimeSpanParse.TimeSpanStandardStyles.None)
			{
				return result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "String was not recognized as a valid TimeSpan.", null, null);
			}
			bool flag = (style & TimeSpanParse.TimeSpanStandardStyles.Invariant) > TimeSpanParse.TimeSpanStandardStyles.None;
			bool flag2 = (style & TimeSpanParse.TimeSpanStandardStyles.Localized) > TimeSpanParse.TimeSpanStandardStyles.None;
			long num = 0L;
			bool flag3 = false;
			bool flag4 = false;
			bool flag5 = false;
			TimeSpanParse.TimeSpanToken timeSpanToken = new TimeSpanParse.TimeSpanToken(0);
			if (flag)
			{
				if (raw.FullHMSFMatch(raw.PositiveInvariant))
				{
					flag3 = true;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, timeSpanToken, raw._numbers0, raw._numbers1, raw._numbers2, raw._numbers3, out num);
					flag5 = flag5 || !flag4;
				}
				if (!flag4 && raw.FullDHMSMatch(raw.PositiveInvariant))
				{
					flag3 = true;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, raw._numbers0, raw._numbers1, raw._numbers2, raw._numbers3, timeSpanToken, out num);
					flag5 = flag5 || !flag4;
				}
				if (!flag4 && raw.FullAppCompatMatch(raw.PositiveInvariant))
				{
					flag3 = true;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, raw._numbers0, raw._numbers1, raw._numbers2, timeSpanToken, raw._numbers3, out num);
					flag5 = flag5 || !flag4;
				}
				if (!flag4 && raw.FullHMSFMatch(raw.NegativeInvariant))
				{
					flag3 = false;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, timeSpanToken, raw._numbers0, raw._numbers1, raw._numbers2, raw._numbers3, out num);
					flag5 = flag5 || !flag4;
				}
				if (!flag4 && raw.FullDHMSMatch(raw.NegativeInvariant))
				{
					flag3 = false;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, raw._numbers0, raw._numbers1, raw._numbers2, raw._numbers3, timeSpanToken, out num);
					flag5 = flag5 || !flag4;
				}
				if (!flag4 && raw.FullAppCompatMatch(raw.NegativeInvariant))
				{
					flag3 = false;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, raw._numbers0, raw._numbers1, raw._numbers2, timeSpanToken, raw._numbers3, out num);
					flag5 = flag5 || !flag4;
				}
			}
			if (flag2)
			{
				if (!flag4 && raw.FullHMSFMatch(raw.PositiveLocalized))
				{
					flag3 = true;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, timeSpanToken, raw._numbers0, raw._numbers1, raw._numbers2, raw._numbers3, out num);
					flag5 = flag5 || !flag4;
				}
				if (!flag4 && raw.FullDHMSMatch(raw.PositiveLocalized))
				{
					flag3 = true;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, raw._numbers0, raw._numbers1, raw._numbers2, raw._numbers3, timeSpanToken, out num);
					flag5 = flag5 || !flag4;
				}
				if (!flag4 && raw.FullAppCompatMatch(raw.PositiveLocalized))
				{
					flag3 = true;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, raw._numbers0, raw._numbers1, raw._numbers2, timeSpanToken, raw._numbers3, out num);
					flag5 = flag5 || !flag4;
				}
				if (!flag4 && raw.FullHMSFMatch(raw.NegativeLocalized))
				{
					flag3 = false;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, timeSpanToken, raw._numbers0, raw._numbers1, raw._numbers2, raw._numbers3, out num);
					flag5 = flag5 || !flag4;
				}
				if (!flag4 && raw.FullDHMSMatch(raw.NegativeLocalized))
				{
					flag3 = false;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, raw._numbers0, raw._numbers1, raw._numbers2, raw._numbers3, timeSpanToken, out num);
					flag5 = flag5 || !flag4;
				}
				if (!flag4 && raw.FullAppCompatMatch(raw.NegativeLocalized))
				{
					flag3 = false;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, raw._numbers0, raw._numbers1, raw._numbers2, timeSpanToken, raw._numbers3, out num);
					flag5 = flag5 || !flag4;
				}
			}
			if (flag4)
			{
				if (!flag3)
				{
					num = -num;
					if (num > 0L)
					{
						return result.SetFailure(TimeSpanParse.ParseFailureKind.Overflow, "The TimeSpan could not be parsed because at least one of the numeric components is out of range or contains too many digits.", null, null);
					}
				}
				result.parsedTimeSpan = new TimeSpan(num);
				return true;
			}
			if (!flag5)
			{
				return result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "String was not recognized as a valid TimeSpan.", null, null);
			}
			return result.SetFailure(TimeSpanParse.ParseFailureKind.Overflow, "The TimeSpan could not be parsed because at least one of the numeric components is out of range or contains too many digits.", null, null);
		}

		// Token: 0x0600354E RID: 13646 RVA: 0x000CD710 File Offset: 0x000CB910
		private static bool ProcessTerminal_HM_S_D(ref TimeSpanParse.TimeSpanRawInfo raw, TimeSpanParse.TimeSpanStandardStyles style, ref TimeSpanParse.TimeSpanResult result)
		{
			if (raw._sepCount != 4 || (style & TimeSpanParse.TimeSpanStandardStyles.RequireFull) != TimeSpanParse.TimeSpanStandardStyles.None)
			{
				return result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "String was not recognized as a valid TimeSpan.", null, null);
			}
			bool flag = (style & TimeSpanParse.TimeSpanStandardStyles.Invariant) > TimeSpanParse.TimeSpanStandardStyles.None;
			bool flag2 = (style & TimeSpanParse.TimeSpanStandardStyles.Localized) > TimeSpanParse.TimeSpanStandardStyles.None;
			bool flag3 = false;
			bool flag4 = false;
			bool flag5 = false;
			TimeSpanParse.TimeSpanToken timeSpanToken = new TimeSpanParse.TimeSpanToken(0);
			long num = 0L;
			if (flag)
			{
				if (raw.FullHMSMatch(raw.PositiveInvariant))
				{
					flag3 = true;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, timeSpanToken, raw._numbers0, raw._numbers1, raw._numbers2, timeSpanToken, out num);
					flag5 = flag5 || !flag4;
				}
				if (!flag4 && raw.FullDHMMatch(raw.PositiveInvariant))
				{
					flag3 = true;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, raw._numbers0, raw._numbers1, raw._numbers2, timeSpanToken, timeSpanToken, out num);
					flag5 = flag5 || !flag4;
				}
				if (!flag4 && raw.PartialAppCompatMatch(raw.PositiveInvariant))
				{
					flag3 = true;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, timeSpanToken, raw._numbers0, raw._numbers1, timeSpanToken, raw._numbers2, out num);
					flag5 = flag5 || !flag4;
				}
				if (!flag4 && raw.FullHMSMatch(raw.NegativeInvariant))
				{
					flag3 = false;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, timeSpanToken, raw._numbers0, raw._numbers1, raw._numbers2, timeSpanToken, out num);
					flag5 = flag5 || !flag4;
				}
				if (!flag4 && raw.FullDHMMatch(raw.NegativeInvariant))
				{
					flag3 = false;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, raw._numbers0, raw._numbers1, raw._numbers2, timeSpanToken, timeSpanToken, out num);
					flag5 = flag5 || !flag4;
				}
				if (!flag4 && raw.PartialAppCompatMatch(raw.NegativeInvariant))
				{
					flag3 = false;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, timeSpanToken, raw._numbers0, raw._numbers1, timeSpanToken, raw._numbers2, out num);
					flag5 = flag5 || !flag4;
				}
			}
			if (flag2)
			{
				if (!flag4 && raw.FullHMSMatch(raw.PositiveLocalized))
				{
					flag3 = true;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, timeSpanToken, raw._numbers0, raw._numbers1, raw._numbers2, timeSpanToken, out num);
					flag5 = flag5 || !flag4;
				}
				if (!flag4 && raw.FullDHMMatch(raw.PositiveLocalized))
				{
					flag3 = true;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, raw._numbers0, raw._numbers1, raw._numbers2, timeSpanToken, timeSpanToken, out num);
					flag5 = flag5 || !flag4;
				}
				if (!flag4 && raw.PartialAppCompatMatch(raw.PositiveLocalized))
				{
					flag3 = true;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, timeSpanToken, raw._numbers0, raw._numbers1, timeSpanToken, raw._numbers2, out num);
					flag5 = flag5 || !flag4;
				}
				if (!flag4 && raw.FullHMSMatch(raw.NegativeLocalized))
				{
					flag3 = false;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, timeSpanToken, raw._numbers0, raw._numbers1, raw._numbers2, timeSpanToken, out num);
					flag5 = flag5 || !flag4;
				}
				if (!flag4 && raw.FullDHMMatch(raw.NegativeLocalized))
				{
					flag3 = false;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, raw._numbers0, raw._numbers1, raw._numbers2, timeSpanToken, timeSpanToken, out num);
					flag5 = flag5 || !flag4;
				}
				if (!flag4 && raw.PartialAppCompatMatch(raw.NegativeLocalized))
				{
					flag3 = false;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, timeSpanToken, raw._numbers0, raw._numbers1, timeSpanToken, raw._numbers2, out num);
					flag5 = flag5 || !flag4;
				}
			}
			if (flag4)
			{
				if (!flag3)
				{
					num = -num;
					if (num > 0L)
					{
						return result.SetFailure(TimeSpanParse.ParseFailureKind.Overflow, "The TimeSpan could not be parsed because at least one of the numeric components is out of range or contains too many digits.", null, null);
					}
				}
				result.parsedTimeSpan = new TimeSpan(num);
				return true;
			}
			if (!flag5)
			{
				return result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "String was not recognized as a valid TimeSpan.", null, null);
			}
			return result.SetFailure(TimeSpanParse.ParseFailureKind.Overflow, "The TimeSpan could not be parsed because at least one of the numeric components is out of range or contains too many digits.", null, null);
		}

		// Token: 0x0600354F RID: 13647 RVA: 0x000CDA8C File Offset: 0x000CBC8C
		private static bool ProcessTerminal_HM(ref TimeSpanParse.TimeSpanRawInfo raw, TimeSpanParse.TimeSpanStandardStyles style, ref TimeSpanParse.TimeSpanResult result)
		{
			if (raw._sepCount != 3 || (style & TimeSpanParse.TimeSpanStandardStyles.RequireFull) != TimeSpanParse.TimeSpanStandardStyles.None)
			{
				return result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "String was not recognized as a valid TimeSpan.", null, null);
			}
			bool flag = (style & TimeSpanParse.TimeSpanStandardStyles.Invariant) > TimeSpanParse.TimeSpanStandardStyles.None;
			bool flag2 = (style & TimeSpanParse.TimeSpanStandardStyles.Localized) > TimeSpanParse.TimeSpanStandardStyles.None;
			bool flag3 = false;
			bool flag4 = false;
			if (flag)
			{
				if (raw.FullHMMatch(raw.PositiveInvariant))
				{
					flag4 = true;
					flag3 = true;
				}
				if (!flag4 && raw.FullHMMatch(raw.NegativeInvariant))
				{
					flag4 = true;
					flag3 = false;
				}
			}
			if (flag2)
			{
				if (!flag4 && raw.FullHMMatch(raw.PositiveLocalized))
				{
					flag4 = true;
					flag3 = true;
				}
				if (!flag4 && raw.FullHMMatch(raw.NegativeLocalized))
				{
					flag4 = true;
					flag3 = false;
				}
			}
			if (!flag4)
			{
				return result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "String was not recognized as a valid TimeSpan.", null, null);
			}
			long num = 0L;
			TimeSpanParse.TimeSpanToken timeSpanToken = new TimeSpanParse.TimeSpanToken(0);
			if (!TimeSpanParse.TryTimeToTicks(flag3, timeSpanToken, raw._numbers0, raw._numbers1, timeSpanToken, timeSpanToken, out num))
			{
				return result.SetFailure(TimeSpanParse.ParseFailureKind.Overflow, "The TimeSpan could not be parsed because at least one of the numeric components is out of range or contains too many digits.", null, null);
			}
			if (!flag3)
			{
				num = -num;
				if (num > 0L)
				{
					return result.SetFailure(TimeSpanParse.ParseFailureKind.Overflow, "The TimeSpan could not be parsed because at least one of the numeric components is out of range or contains too many digits.", null, null);
				}
			}
			result.parsedTimeSpan = new TimeSpan(num);
			return true;
		}

		// Token: 0x06003550 RID: 13648 RVA: 0x000CDB8C File Offset: 0x000CBD8C
		private static bool ProcessTerminal_D(ref TimeSpanParse.TimeSpanRawInfo raw, TimeSpanParse.TimeSpanStandardStyles style, ref TimeSpanParse.TimeSpanResult result)
		{
			if (raw._sepCount != 2 || (style & TimeSpanParse.TimeSpanStandardStyles.RequireFull) != TimeSpanParse.TimeSpanStandardStyles.None)
			{
				return result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "String was not recognized as a valid TimeSpan.", null, null);
			}
			bool flag = (style & TimeSpanParse.TimeSpanStandardStyles.Invariant) > TimeSpanParse.TimeSpanStandardStyles.None;
			bool flag2 = (style & TimeSpanParse.TimeSpanStandardStyles.Localized) > TimeSpanParse.TimeSpanStandardStyles.None;
			bool flag3 = false;
			bool flag4 = false;
			if (flag)
			{
				if (raw.FullDMatch(raw.PositiveInvariant))
				{
					flag4 = true;
					flag3 = true;
				}
				if (!flag4 && raw.FullDMatch(raw.NegativeInvariant))
				{
					flag4 = true;
					flag3 = false;
				}
			}
			if (flag2)
			{
				if (!flag4 && raw.FullDMatch(raw.PositiveLocalized))
				{
					flag4 = true;
					flag3 = true;
				}
				if (!flag4 && raw.FullDMatch(raw.NegativeLocalized))
				{
					flag4 = true;
					flag3 = false;
				}
			}
			if (!flag4)
			{
				return result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "String was not recognized as a valid TimeSpan.", null, null);
			}
			long num = 0L;
			TimeSpanParse.TimeSpanToken timeSpanToken = new TimeSpanParse.TimeSpanToken(0);
			if (!TimeSpanParse.TryTimeToTicks(flag3, raw._numbers0, timeSpanToken, timeSpanToken, timeSpanToken, timeSpanToken, out num))
			{
				return result.SetFailure(TimeSpanParse.ParseFailureKind.Overflow, "The TimeSpan could not be parsed because at least one of the numeric components is out of range or contains too many digits.", null, null);
			}
			if (!flag3)
			{
				num = -num;
				if (num > 0L)
				{
					return result.SetFailure(TimeSpanParse.ParseFailureKind.Overflow, "The TimeSpan could not be parsed because at least one of the numeric components is out of range or contains too many digits.", null, null);
				}
			}
			result.parsedTimeSpan = new TimeSpan(num);
			return true;
		}

		// Token: 0x020006A8 RID: 1704
		private enum ParseFailureKind : byte
		{
			// Token: 0x04001C83 RID: 7299
			None,
			// Token: 0x04001C84 RID: 7300
			ArgumentNull,
			// Token: 0x04001C85 RID: 7301
			Format,
			// Token: 0x04001C86 RID: 7302
			FormatWithParameter,
			// Token: 0x04001C87 RID: 7303
			Overflow
		}

		// Token: 0x020006A9 RID: 1705
		[Flags]
		private enum TimeSpanStandardStyles : byte
		{
			// Token: 0x04001C89 RID: 7305
			None = 0,
			// Token: 0x04001C8A RID: 7306
			Invariant = 1,
			// Token: 0x04001C8B RID: 7307
			Localized = 2,
			// Token: 0x04001C8C RID: 7308
			RequireFull = 4,
			// Token: 0x04001C8D RID: 7309
			Any = 3
		}

		// Token: 0x020006AA RID: 1706
		private enum TTT : byte
		{
			// Token: 0x04001C8F RID: 7311
			None,
			// Token: 0x04001C90 RID: 7312
			End,
			// Token: 0x04001C91 RID: 7313
			Num,
			// Token: 0x04001C92 RID: 7314
			Sep,
			// Token: 0x04001C93 RID: 7315
			NumOverflow
		}

		// Token: 0x020006AB RID: 1707
		private ref struct TimeSpanToken
		{
			// Token: 0x06003551 RID: 13649 RVA: 0x000CDC88 File Offset: 0x000CBE88
			public TimeSpanToken(TimeSpanParse.TTT type)
			{
				this = new TimeSpanParse.TimeSpanToken(type, 0, 0, default(ReadOnlySpan<char>));
			}

			// Token: 0x06003552 RID: 13650 RVA: 0x000CDCA8 File Offset: 0x000CBEA8
			public TimeSpanToken(int number)
			{
				this = new TimeSpanParse.TimeSpanToken(TimeSpanParse.TTT.Num, number, 0, default(ReadOnlySpan<char>));
			}

			// Token: 0x06003553 RID: 13651 RVA: 0x000CDCC7 File Offset: 0x000CBEC7
			public TimeSpanToken(TimeSpanParse.TTT type, int number, int leadingZeroes, ReadOnlySpan<char> separator)
			{
				this._ttt = type;
				this._num = number;
				this._zeroes = leadingZeroes;
				this._sep = separator;
			}

			// Token: 0x06003554 RID: 13652 RVA: 0x000CDCE8 File Offset: 0x000CBEE8
			public bool IsInvalidFraction()
			{
				return this._num > 9999999 || this._zeroes > 7 || (this._num != 0 && this._zeroes != 0 && (long)this._num >= 9999999L / TimeSpanParse.Pow10(this._zeroes - 1));
			}

			// Token: 0x04001C94 RID: 7316
			internal TimeSpanParse.TTT _ttt;

			// Token: 0x04001C95 RID: 7317
			internal int _num;

			// Token: 0x04001C96 RID: 7318
			internal int _zeroes;

			// Token: 0x04001C97 RID: 7319
			internal ReadOnlySpan<char> _sep;
		}

		// Token: 0x020006AC RID: 1708
		private ref struct TimeSpanTokenizer
		{
			// Token: 0x06003555 RID: 13653 RVA: 0x000CDD3F File Offset: 0x000CBF3F
			internal TimeSpanTokenizer(ReadOnlySpan<char> input)
			{
				this = new TimeSpanParse.TimeSpanTokenizer(input, 0);
			}

			// Token: 0x06003556 RID: 13654 RVA: 0x000CDD49 File Offset: 0x000CBF49
			internal TimeSpanTokenizer(ReadOnlySpan<char> input, int startPosition)
			{
				this._value = input;
				this._pos = startPosition;
			}

			// Token: 0x06003557 RID: 13655 RVA: 0x000CDD5C File Offset: 0x000CBF5C
			internal unsafe TimeSpanParse.TimeSpanToken GetNextToken()
			{
				int pos = this._pos;
				if (pos >= this._value.Length)
				{
					return new TimeSpanParse.TimeSpanToken(TimeSpanParse.TTT.End);
				}
				int num = (int)(*this._value[pos] - 48);
				if (num <= 9)
				{
					int num2 = 0;
					if (num == 0)
					{
						num2 = 1;
						int num4;
						for (;;)
						{
							int num3 = this._pos + 1;
							this._pos = num3;
							if (num3 >= this._value.Length || (num4 = (int)(*this._value[this._pos] - 48)) > 9)
							{
								break;
							}
							if (num4 != 0)
							{
								goto IL_0099;
							}
							num2++;
						}
						return new TimeSpanParse.TimeSpanToken(TimeSpanParse.TTT.Num, 0, num2, default(ReadOnlySpan<char>));
						IL_0099:
						num = num4;
					}
					do
					{
						int num3 = this._pos + 1;
						this._pos = num3;
						if (num3 >= this._value.Length)
						{
							goto IL_00F6;
						}
						int num5 = (int)(*this._value[this._pos] - 48);
						if (num5 > 9)
						{
							goto IL_00F6;
						}
						num = num * 10 + num5;
					}
					while (((long)num & (long)((ulong)(-268435456))) == 0L);
					return new TimeSpanParse.TimeSpanToken(TimeSpanParse.TTT.NumOverflow);
					IL_00F6:
					return new TimeSpanParse.TimeSpanToken(TimeSpanParse.TTT.Num, num, num2, default(ReadOnlySpan<char>));
				}
				int num6 = 1;
				for (;;)
				{
					int num3 = this._pos + 1;
					this._pos = num3;
					if (num3 >= this._value.Length || *this._value[this._pos] - 48 <= 9)
					{
						break;
					}
					num6++;
				}
				return new TimeSpanParse.TimeSpanToken(TimeSpanParse.TTT.Sep, 0, 0, this._value.Slice(pos, num6));
			}

			// Token: 0x04001C98 RID: 7320
			private ReadOnlySpan<char> _value;

			// Token: 0x04001C99 RID: 7321
			private int _pos;
		}

		// Token: 0x020006AD RID: 1709
		private ref struct TimeSpanRawInfo
		{
			// Token: 0x170007F0 RID: 2032
			// (get) Token: 0x06003558 RID: 13656 RVA: 0x000CDEC9 File Offset: 0x000CC0C9
			internal TimeSpanFormat.FormatLiterals PositiveInvariant
			{
				get
				{
					return TimeSpanFormat.PositiveInvariantFormatLiterals;
				}
			}

			// Token: 0x170007F1 RID: 2033
			// (get) Token: 0x06003559 RID: 13657 RVA: 0x000CDED0 File Offset: 0x000CC0D0
			internal TimeSpanFormat.FormatLiterals NegativeInvariant
			{
				get
				{
					return TimeSpanFormat.NegativeInvariantFormatLiterals;
				}
			}

			// Token: 0x170007F2 RID: 2034
			// (get) Token: 0x0600355A RID: 13658 RVA: 0x000CDED7 File Offset: 0x000CC0D7
			internal TimeSpanFormat.FormatLiterals PositiveLocalized
			{
				get
				{
					if (!this._posLocInit)
					{
						this._posLoc = default(TimeSpanFormat.FormatLiterals);
						this._posLoc.Init(this._fullPosPattern, false);
						this._posLocInit = true;
					}
					return this._posLoc;
				}
			}

			// Token: 0x170007F3 RID: 2035
			// (get) Token: 0x0600355B RID: 13659 RVA: 0x000CDF11 File Offset: 0x000CC111
			internal TimeSpanFormat.FormatLiterals NegativeLocalized
			{
				get
				{
					if (!this._negLocInit)
					{
						this._negLoc = default(TimeSpanFormat.FormatLiterals);
						this._negLoc.Init(this._fullNegPattern, false);
						this._negLocInit = true;
					}
					return this._negLoc;
				}
			}

			// Token: 0x0600355C RID: 13660 RVA: 0x000CDF4C File Offset: 0x000CC14C
			internal bool FullAppCompatMatch(TimeSpanFormat.FormatLiterals pattern)
			{
				return this._sepCount == 5 && this._numCount == 4 && this._literals0.EqualsOrdinal(pattern.Start) && this._literals1.EqualsOrdinal(pattern.DayHourSep) && this._literals2.EqualsOrdinal(pattern.HourMinuteSep) && this._literals3.EqualsOrdinal(pattern.AppCompatLiteral) && this._literals4.EqualsOrdinal(pattern.End);
			}

			// Token: 0x0600355D RID: 13661 RVA: 0x000CDFEC File Offset: 0x000CC1EC
			internal bool PartialAppCompatMatch(TimeSpanFormat.FormatLiterals pattern)
			{
				return this._sepCount == 4 && this._numCount == 3 && this._literals0.EqualsOrdinal(pattern.Start) && this._literals1.EqualsOrdinal(pattern.HourMinuteSep) && this._literals2.EqualsOrdinal(pattern.AppCompatLiteral) && this._literals3.EqualsOrdinal(pattern.End);
			}

			// Token: 0x0600355E RID: 13662 RVA: 0x000CE070 File Offset: 0x000CC270
			internal bool FullMatch(TimeSpanFormat.FormatLiterals pattern)
			{
				return this._sepCount == 6 && this._numCount == 5 && this._literals0.EqualsOrdinal(pattern.Start) && this._literals1.EqualsOrdinal(pattern.DayHourSep) && this._literals2.EqualsOrdinal(pattern.HourMinuteSep) && this._literals3.EqualsOrdinal(pattern.MinuteSecondSep) && this._literals4.EqualsOrdinal(pattern.SecondFractionSep) && this._literals5.EqualsOrdinal(pattern.End);
			}

			// Token: 0x0600355F RID: 13663 RVA: 0x000CE12C File Offset: 0x000CC32C
			internal bool FullDMatch(TimeSpanFormat.FormatLiterals pattern)
			{
				return this._sepCount == 2 && this._numCount == 1 && this._literals0.EqualsOrdinal(pattern.Start) && this._literals1.EqualsOrdinal(pattern.End);
			}

			// Token: 0x06003560 RID: 13664 RVA: 0x000CE180 File Offset: 0x000CC380
			internal bool FullHMMatch(TimeSpanFormat.FormatLiterals pattern)
			{
				return this._sepCount == 3 && this._numCount == 2 && this._literals0.EqualsOrdinal(pattern.Start) && this._literals1.EqualsOrdinal(pattern.HourMinuteSep) && this._literals2.EqualsOrdinal(pattern.End);
			}

			// Token: 0x06003561 RID: 13665 RVA: 0x000CE1EC File Offset: 0x000CC3EC
			internal bool FullDHMMatch(TimeSpanFormat.FormatLiterals pattern)
			{
				return this._sepCount == 4 && this._numCount == 3 && this._literals0.EqualsOrdinal(pattern.Start) && this._literals1.EqualsOrdinal(pattern.DayHourSep) && this._literals2.EqualsOrdinal(pattern.HourMinuteSep) && this._literals3.EqualsOrdinal(pattern.End);
			}

			// Token: 0x06003562 RID: 13666 RVA: 0x000CE270 File Offset: 0x000CC470
			internal bool FullHMSMatch(TimeSpanFormat.FormatLiterals pattern)
			{
				return this._sepCount == 4 && this._numCount == 3 && this._literals0.EqualsOrdinal(pattern.Start) && this._literals1.EqualsOrdinal(pattern.HourMinuteSep) && this._literals2.EqualsOrdinal(pattern.MinuteSecondSep) && this._literals3.EqualsOrdinal(pattern.End);
			}

			// Token: 0x06003563 RID: 13667 RVA: 0x000CE2F4 File Offset: 0x000CC4F4
			internal bool FullDHMSMatch(TimeSpanFormat.FormatLiterals pattern)
			{
				return this._sepCount == 5 && this._numCount == 4 && this._literals0.EqualsOrdinal(pattern.Start) && this._literals1.EqualsOrdinal(pattern.DayHourSep) && this._literals2.EqualsOrdinal(pattern.HourMinuteSep) && this._literals3.EqualsOrdinal(pattern.MinuteSecondSep) && this._literals4.EqualsOrdinal(pattern.End);
			}

			// Token: 0x06003564 RID: 13668 RVA: 0x000CE394 File Offset: 0x000CC594
			internal bool FullHMSFMatch(TimeSpanFormat.FormatLiterals pattern)
			{
				return this._sepCount == 5 && this._numCount == 4 && this._literals0.EqualsOrdinal(pattern.Start) && this._literals1.EqualsOrdinal(pattern.HourMinuteSep) && this._literals2.EqualsOrdinal(pattern.MinuteSecondSep) && this._literals3.EqualsOrdinal(pattern.SecondFractionSep) && this._literals4.EqualsOrdinal(pattern.End);
			}

			// Token: 0x06003565 RID: 13669 RVA: 0x000CE434 File Offset: 0x000CC634
			internal void Init(DateTimeFormatInfo dtfi)
			{
				this._lastSeenTTT = TimeSpanParse.TTT.None;
				this._tokenCount = 0;
				this._sepCount = 0;
				this._numCount = 0;
				this._fullPosPattern = dtfi.FullTimeSpanPositivePattern;
				this._fullNegPattern = dtfi.FullTimeSpanNegativePattern;
				this._posLocInit = false;
				this._negLocInit = false;
			}

			// Token: 0x06003566 RID: 13670 RVA: 0x000CE484 File Offset: 0x000CC684
			internal bool ProcessToken(ref TimeSpanParse.TimeSpanToken tok, ref TimeSpanParse.TimeSpanResult result)
			{
				switch (tok._ttt)
				{
				case TimeSpanParse.TTT.Num:
					if ((this._tokenCount == 0 && !this.AddSep(default(ReadOnlySpan<char>), ref result)) || !this.AddNum(tok, ref result))
					{
						return false;
					}
					break;
				case TimeSpanParse.TTT.Sep:
					if (!this.AddSep(tok._sep, ref result))
					{
						return false;
					}
					break;
				case TimeSpanParse.TTT.NumOverflow:
					return result.SetFailure(TimeSpanParse.ParseFailureKind.Overflow, "The TimeSpan could not be parsed because at least one of the numeric components is out of range or contains too many digits.", null, null);
				default:
					return result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "String was not recognized as a valid TimeSpan.", null, null);
				}
				this._lastSeenTTT = tok._ttt;
				return true;
			}

			// Token: 0x06003567 RID: 13671 RVA: 0x000CE518 File Offset: 0x000CC718
			private bool AddSep(ReadOnlySpan<char> sep, ref TimeSpanParse.TimeSpanResult result)
			{
				if (this._sepCount >= 6 || this._tokenCount >= 11)
				{
					return result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "String was not recognized as a valid TimeSpan.", null, null);
				}
				int sepCount = this._sepCount;
				this._sepCount = sepCount + 1;
				switch (sepCount)
				{
				case 0:
					this._literals0 = sep;
					break;
				case 1:
					this._literals1 = sep;
					break;
				case 2:
					this._literals2 = sep;
					break;
				case 3:
					this._literals3 = sep;
					break;
				case 4:
					this._literals4 = sep;
					break;
				default:
					this._literals5 = sep;
					break;
				}
				this._tokenCount++;
				return true;
			}

			// Token: 0x06003568 RID: 13672 RVA: 0x000CE5B8 File Offset: 0x000CC7B8
			private bool AddNum(TimeSpanParse.TimeSpanToken num, ref TimeSpanParse.TimeSpanResult result)
			{
				if (this._numCount >= 5 || this._tokenCount >= 11)
				{
					return result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "String was not recognized as a valid TimeSpan.", null, null);
				}
				int numCount = this._numCount;
				this._numCount = numCount + 1;
				switch (numCount)
				{
				case 0:
					this._numbers0 = num;
					break;
				case 1:
					this._numbers1 = num;
					break;
				case 2:
					this._numbers2 = num;
					break;
				case 3:
					this._numbers3 = num;
					break;
				default:
					this._numbers4 = num;
					break;
				}
				this._tokenCount++;
				return true;
			}

			// Token: 0x04001C9A RID: 7322
			internal TimeSpanParse.TTT _lastSeenTTT;

			// Token: 0x04001C9B RID: 7323
			internal int _tokenCount;

			// Token: 0x04001C9C RID: 7324
			internal int _sepCount;

			// Token: 0x04001C9D RID: 7325
			internal int _numCount;

			// Token: 0x04001C9E RID: 7326
			private TimeSpanFormat.FormatLiterals _posLoc;

			// Token: 0x04001C9F RID: 7327
			private TimeSpanFormat.FormatLiterals _negLoc;

			// Token: 0x04001CA0 RID: 7328
			private bool _posLocInit;

			// Token: 0x04001CA1 RID: 7329
			private bool _negLocInit;

			// Token: 0x04001CA2 RID: 7330
			private string _fullPosPattern;

			// Token: 0x04001CA3 RID: 7331
			private string _fullNegPattern;

			// Token: 0x04001CA4 RID: 7332
			internal TimeSpanParse.TimeSpanToken _numbers0;

			// Token: 0x04001CA5 RID: 7333
			internal TimeSpanParse.TimeSpanToken _numbers1;

			// Token: 0x04001CA6 RID: 7334
			internal TimeSpanParse.TimeSpanToken _numbers2;

			// Token: 0x04001CA7 RID: 7335
			internal TimeSpanParse.TimeSpanToken _numbers3;

			// Token: 0x04001CA8 RID: 7336
			internal TimeSpanParse.TimeSpanToken _numbers4;

			// Token: 0x04001CA9 RID: 7337
			internal ReadOnlySpan<char> _literals0;

			// Token: 0x04001CAA RID: 7338
			internal ReadOnlySpan<char> _literals1;

			// Token: 0x04001CAB RID: 7339
			internal ReadOnlySpan<char> _literals2;

			// Token: 0x04001CAC RID: 7340
			internal ReadOnlySpan<char> _literals3;

			// Token: 0x04001CAD RID: 7341
			internal ReadOnlySpan<char> _literals4;

			// Token: 0x04001CAE RID: 7342
			internal ReadOnlySpan<char> _literals5;
		}

		// Token: 0x020006AE RID: 1710
		private struct TimeSpanResult
		{
			// Token: 0x06003569 RID: 13673 RVA: 0x000CE64B File Offset: 0x000CC84B
			internal TimeSpanResult(bool throwOnFailure)
			{
				this.parsedTimeSpan = default(TimeSpan);
				this._throwOnFailure = throwOnFailure;
			}

			// Token: 0x0600356A RID: 13674 RVA: 0x000CE660 File Offset: 0x000CC860
			internal bool SetFailure(TimeSpanParse.ParseFailureKind kind, string resourceKey, object messageArgument = null, string argumentName = null)
			{
				if (!this._throwOnFailure)
				{
					return false;
				}
				string resourceString = SR.GetResourceString(resourceKey);
				switch (kind)
				{
				case TimeSpanParse.ParseFailureKind.ArgumentNull:
					throw new ArgumentNullException(argumentName, resourceString);
				case TimeSpanParse.ParseFailureKind.FormatWithParameter:
					throw new FormatException(SR.Format(resourceString, messageArgument));
				case TimeSpanParse.ParseFailureKind.Overflow:
					throw new OverflowException(resourceString);
				}
				throw new FormatException(resourceString);
			}

			// Token: 0x04001CAF RID: 7343
			internal TimeSpan parsedTimeSpan;

			// Token: 0x04001CB0 RID: 7344
			private readonly bool _throwOnFailure;
		}
	}
}
