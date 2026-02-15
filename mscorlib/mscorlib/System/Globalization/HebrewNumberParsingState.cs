using System;

namespace System.Globalization
{
	// Token: 0x0200069D RID: 1693
	internal enum HebrewNumberParsingState
	{
		// Token: 0x04001C3A RID: 7226
		InvalidHebrewNumber,
		// Token: 0x04001C3B RID: 7227
		NotHebrewDigit,
		// Token: 0x04001C3C RID: 7228
		FoundEndOfHebrewNumber,
		// Token: 0x04001C3D RID: 7229
		ContinueParsing
	}
}
