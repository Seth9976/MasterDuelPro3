using System;
using System.Globalization;

namespace System
{
	// Token: 0x020000F4 RID: 244
	internal ref struct DateTimeResult
	{
		// Token: 0x0600082E RID: 2094 RVA: 0x000251D8 File Offset: 0x000233D8
		internal void Init(ReadOnlySpan<char> originalDateTimeString)
		{
			this.originalDateTimeString = originalDateTimeString;
			this.Year = -1;
			this.Month = -1;
			this.Day = -1;
			this.fraction = -1.0;
			this.era = -1;
		}

		// Token: 0x0600082F RID: 2095 RVA: 0x0002520C File Offset: 0x0002340C
		internal void SetDate(int year, int month, int day)
		{
			this.Year = year;
			this.Month = month;
			this.Day = day;
		}

		// Token: 0x06000830 RID: 2096 RVA: 0x00025223 File Offset: 0x00023423
		internal void SetBadFormatSpecifierFailure()
		{
			this.SetBadFormatSpecifierFailure(ReadOnlySpan<char>.Empty);
		}

		// Token: 0x06000831 RID: 2097 RVA: 0x00025230 File Offset: 0x00023430
		internal void SetBadFormatSpecifierFailure(ReadOnlySpan<char> failedFormatSpecifier)
		{
			this.failure = ParseFailureKind.FormatWithFormatSpecifier;
			this.failureMessageID = "Format specifier was invalid.";
			this.failedFormatSpecifier = failedFormatSpecifier;
		}

		// Token: 0x06000832 RID: 2098 RVA: 0x0002524B File Offset: 0x0002344B
		internal void SetBadDateTimeFailure()
		{
			this.failure = ParseFailureKind.FormatWithOriginalDateTime;
			this.failureMessageID = "String was not recognized as a valid DateTime.";
			this.failureMessageFormatArgument = null;
		}

		// Token: 0x06000833 RID: 2099 RVA: 0x00025266 File Offset: 0x00023466
		internal void SetFailure(ParseFailureKind failure, string failureMessageID)
		{
			this.failure = failure;
			this.failureMessageID = failureMessageID;
			this.failureMessageFormatArgument = null;
		}

		// Token: 0x06000834 RID: 2100 RVA: 0x0002527D File Offset: 0x0002347D
		internal void SetFailure(ParseFailureKind failure, string failureMessageID, object failureMessageFormatArgument)
		{
			this.failure = failure;
			this.failureMessageID = failureMessageID;
			this.failureMessageFormatArgument = failureMessageFormatArgument;
		}

		// Token: 0x06000835 RID: 2101 RVA: 0x00025294 File Offset: 0x00023494
		internal void SetFailure(ParseFailureKind failure, string failureMessageID, object failureMessageFormatArgument, string failureArgumentName)
		{
			this.failure = failure;
			this.failureMessageID = failureMessageID;
			this.failureMessageFormatArgument = failureMessageFormatArgument;
			this.failureArgumentName = failureArgumentName;
		}

		// Token: 0x040003BA RID: 954
		internal int Year;

		// Token: 0x040003BB RID: 955
		internal int Month;

		// Token: 0x040003BC RID: 956
		internal int Day;

		// Token: 0x040003BD RID: 957
		internal int Hour;

		// Token: 0x040003BE RID: 958
		internal int Minute;

		// Token: 0x040003BF RID: 959
		internal int Second;

		// Token: 0x040003C0 RID: 960
		internal double fraction;

		// Token: 0x040003C1 RID: 961
		internal int era;

		// Token: 0x040003C2 RID: 962
		internal ParseFlags flags;

		// Token: 0x040003C3 RID: 963
		internal TimeSpan timeZoneOffset;

		// Token: 0x040003C4 RID: 964
		internal Calendar calendar;

		// Token: 0x040003C5 RID: 965
		internal DateTime parsedDate;

		// Token: 0x040003C6 RID: 966
		internal ParseFailureKind failure;

		// Token: 0x040003C7 RID: 967
		internal string failureMessageID;

		// Token: 0x040003C8 RID: 968
		internal object failureMessageFormatArgument;

		// Token: 0x040003C9 RID: 969
		internal string failureArgumentName;

		// Token: 0x040003CA RID: 970
		internal ReadOnlySpan<char> originalDateTimeString;

		// Token: 0x040003CB RID: 971
		internal ReadOnlySpan<char> failedFormatSpecifier;
	}
}
