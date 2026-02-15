using System;

namespace System.Globalization
{
	// Token: 0x02000692 RID: 1682
	[Flags]
	internal enum DateTimeFormatFlags
	{
		// Token: 0x04001B9E RID: 7070
		None = 0,
		// Token: 0x04001B9F RID: 7071
		UseGenitiveMonth = 1,
		// Token: 0x04001BA0 RID: 7072
		UseLeapYearMonth = 2,
		// Token: 0x04001BA1 RID: 7073
		UseSpacesInMonthNames = 4,
		// Token: 0x04001BA2 RID: 7074
		UseHebrewRule = 8,
		// Token: 0x04001BA3 RID: 7075
		UseSpacesInDayNames = 16,
		// Token: 0x04001BA4 RID: 7076
		UseDigitPrefixInTokens = 32,
		// Token: 0x04001BA5 RID: 7077
		NotInitialized = -1
	}
}
