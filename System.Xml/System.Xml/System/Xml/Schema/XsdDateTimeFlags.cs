using System;

namespace System.Xml.Schema
{
	// Token: 0x02000326 RID: 806
	[Flags]
	internal enum XsdDateTimeFlags
	{
		// Token: 0x04001164 RID: 4452
		DateTime = 1,
		// Token: 0x04001165 RID: 4453
		Time = 2,
		// Token: 0x04001166 RID: 4454
		Date = 4,
		// Token: 0x04001167 RID: 4455
		GYearMonth = 8,
		// Token: 0x04001168 RID: 4456
		GYear = 16,
		// Token: 0x04001169 RID: 4457
		GMonthDay = 32,
		// Token: 0x0400116A RID: 4458
		GDay = 64,
		// Token: 0x0400116B RID: 4459
		GMonth = 128,
		// Token: 0x0400116C RID: 4460
		XdrDateTimeNoTz = 256,
		// Token: 0x0400116D RID: 4461
		XdrDateTime = 512,
		// Token: 0x0400116E RID: 4462
		XdrTimeNoTz = 1024,
		// Token: 0x0400116F RID: 4463
		AllXsd = 255
	}
}
