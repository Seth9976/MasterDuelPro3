using System;

namespace System.Net
{
	// Token: 0x02000393 RID: 915
	internal enum FtpOperation
	{
		// Token: 0x04000DF9 RID: 3577
		DownloadFile,
		// Token: 0x04000DFA RID: 3578
		ListDirectory,
		// Token: 0x04000DFB RID: 3579
		ListDirectoryDetails,
		// Token: 0x04000DFC RID: 3580
		UploadFile,
		// Token: 0x04000DFD RID: 3581
		UploadFileUnique,
		// Token: 0x04000DFE RID: 3582
		AppendFile,
		// Token: 0x04000DFF RID: 3583
		DeleteFile,
		// Token: 0x04000E00 RID: 3584
		GetDateTimestamp,
		// Token: 0x04000E01 RID: 3585
		GetFileSize,
		// Token: 0x04000E02 RID: 3586
		Rename,
		// Token: 0x04000E03 RID: 3587
		MakeDirectory,
		// Token: 0x04000E04 RID: 3588
		RemoveDirectory,
		// Token: 0x04000E05 RID: 3589
		PrintWorkingDirectory,
		// Token: 0x04000E06 RID: 3590
		Other
	}
}
