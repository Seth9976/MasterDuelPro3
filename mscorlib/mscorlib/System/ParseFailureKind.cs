using System;

namespace System
{
	// Token: 0x020000F2 RID: 242
	internal enum ParseFailureKind
	{
		// Token: 0x040003A2 RID: 930
		None,
		// Token: 0x040003A3 RID: 931
		ArgumentNull,
		// Token: 0x040003A4 RID: 932
		Format,
		// Token: 0x040003A5 RID: 933
		FormatWithParameter,
		// Token: 0x040003A6 RID: 934
		FormatWithOriginalDateTime,
		// Token: 0x040003A7 RID: 935
		FormatWithFormatSpecifier,
		// Token: 0x040003A8 RID: 936
		FormatWithOriginalDateTimeAndParameter,
		// Token: 0x040003A9 RID: 937
		FormatBadDateTimeCalendar
	}
}
