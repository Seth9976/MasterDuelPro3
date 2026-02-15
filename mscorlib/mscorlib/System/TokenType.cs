using System;

namespace System
{
	// Token: 0x020000F6 RID: 246
	internal enum TokenType
	{
		// Token: 0x040003D6 RID: 982
		NumberToken = 1,
		// Token: 0x040003D7 RID: 983
		YearNumberToken,
		// Token: 0x040003D8 RID: 984
		Am,
		// Token: 0x040003D9 RID: 985
		Pm,
		// Token: 0x040003DA RID: 986
		MonthToken,
		// Token: 0x040003DB RID: 987
		EndOfString,
		// Token: 0x040003DC RID: 988
		DayOfWeekToken,
		// Token: 0x040003DD RID: 989
		TimeZoneToken,
		// Token: 0x040003DE RID: 990
		EraToken,
		// Token: 0x040003DF RID: 991
		DateWordToken,
		// Token: 0x040003E0 RID: 992
		UnknownToken,
		// Token: 0x040003E1 RID: 993
		HebrewNumber,
		// Token: 0x040003E2 RID: 994
		JapaneseEraToken,
		// Token: 0x040003E3 RID: 995
		TEraToken,
		// Token: 0x040003E4 RID: 996
		IgnorableSymbol,
		// Token: 0x040003E5 RID: 997
		SEP_Unk = 256,
		// Token: 0x040003E6 RID: 998
		SEP_End = 512,
		// Token: 0x040003E7 RID: 999
		SEP_Space = 768,
		// Token: 0x040003E8 RID: 1000
		SEP_Am = 1024,
		// Token: 0x040003E9 RID: 1001
		SEP_Pm = 1280,
		// Token: 0x040003EA RID: 1002
		SEP_Date = 1536,
		// Token: 0x040003EB RID: 1003
		SEP_Time = 1792,
		// Token: 0x040003EC RID: 1004
		SEP_YearSuff = 2048,
		// Token: 0x040003ED RID: 1005
		SEP_MonthSuff = 2304,
		// Token: 0x040003EE RID: 1006
		SEP_DaySuff = 2560,
		// Token: 0x040003EF RID: 1007
		SEP_HourSuff = 2816,
		// Token: 0x040003F0 RID: 1008
		SEP_MinuteSuff = 3072,
		// Token: 0x040003F1 RID: 1009
		SEP_SecondSuff = 3328,
		// Token: 0x040003F2 RID: 1010
		SEP_LocalTimeMark = 3584,
		// Token: 0x040003F3 RID: 1011
		SEP_DateOrOffset = 3840,
		// Token: 0x040003F4 RID: 1012
		RegularTokenMask = 255,
		// Token: 0x040003F5 RID: 1013
		SeparatorTokenMask = 65280
	}
}
