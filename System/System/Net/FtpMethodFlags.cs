using System;

namespace System.Net
{
	// Token: 0x02000394 RID: 916
	[Flags]
	internal enum FtpMethodFlags
	{
		// Token: 0x04000E08 RID: 3592
		None = 0,
		// Token: 0x04000E09 RID: 3593
		IsDownload = 1,
		// Token: 0x04000E0A RID: 3594
		IsUpload = 2,
		// Token: 0x04000E0B RID: 3595
		TakesParameter = 4,
		// Token: 0x04000E0C RID: 3596
		MayTakeParameter = 8,
		// Token: 0x04000E0D RID: 3597
		DoesNotTakeParameter = 16,
		// Token: 0x04000E0E RID: 3598
		ParameterIsDirectory = 32,
		// Token: 0x04000E0F RID: 3599
		ShouldParseForResponseUri = 64,
		// Token: 0x04000E10 RID: 3600
		HasHttpCommand = 128,
		// Token: 0x04000E11 RID: 3601
		MustChangeWorkingDirectoryToPath = 256
	}
}
