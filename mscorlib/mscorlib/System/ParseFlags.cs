using System;

namespace System
{
	// Token: 0x020000F3 RID: 243
	[Flags]
	internal enum ParseFlags
	{
		// Token: 0x040003AB RID: 939
		HaveYear = 1,
		// Token: 0x040003AC RID: 940
		HaveMonth = 2,
		// Token: 0x040003AD RID: 941
		HaveDay = 4,
		// Token: 0x040003AE RID: 942
		HaveHour = 8,
		// Token: 0x040003AF RID: 943
		HaveMinute = 16,
		// Token: 0x040003B0 RID: 944
		HaveSecond = 32,
		// Token: 0x040003B1 RID: 945
		HaveTime = 64,
		// Token: 0x040003B2 RID: 946
		HaveDate = 128,
		// Token: 0x040003B3 RID: 947
		TimeZoneUsed = 256,
		// Token: 0x040003B4 RID: 948
		TimeZoneUtc = 512,
		// Token: 0x040003B5 RID: 949
		ParsedMonthName = 1024,
		// Token: 0x040003B6 RID: 950
		CaptureOffset = 2048,
		// Token: 0x040003B7 RID: 951
		YearDefault = 4096,
		// Token: 0x040003B8 RID: 952
		Rfc1123Pattern = 8192,
		// Token: 0x040003B9 RID: 953
		UtcSortPattern = 16384
	}
}
