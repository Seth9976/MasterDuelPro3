using System;
using System.Globalization;

namespace System
{
	// Token: 0x020000F5 RID: 245
	internal struct ParsingInfo
	{
		// Token: 0x06000836 RID: 2102 RVA: 0x000252B3 File Offset: 0x000234B3
		internal void Init()
		{
			this.dayOfWeek = -1;
			this.timeMark = DateTimeParse.TM.NotSet;
		}

		// Token: 0x040003CC RID: 972
		internal Calendar calendar;

		// Token: 0x040003CD RID: 973
		internal int dayOfWeek;

		// Token: 0x040003CE RID: 974
		internal DateTimeParse.TM timeMark;

		// Token: 0x040003CF RID: 975
		internal bool fUseHour12;

		// Token: 0x040003D0 RID: 976
		internal bool fUseTwoDigitYear;

		// Token: 0x040003D1 RID: 977
		internal bool fAllowInnerWhite;

		// Token: 0x040003D2 RID: 978
		internal bool fAllowTrailingWhite;

		// Token: 0x040003D3 RID: 979
		internal bool fCustomNumberParser;

		// Token: 0x040003D4 RID: 980
		internal DateTimeParse.MatchNumberDelegate parseNumberDelegate;
	}
}
