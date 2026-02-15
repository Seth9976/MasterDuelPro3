using System;

namespace System.IO
{
	/// <summary>Specifies changes to watch for in a file or folder.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000366 RID: 870
	[Flags]
	public enum NotifyFilters
	{
		/// <summary>The attributes of the file or folder.</summary>
		// Token: 0x04000CF7 RID: 3319
		Attributes = 4,
		/// <summary>The time the file or folder was created.</summary>
		// Token: 0x04000CF8 RID: 3320
		CreationTime = 64,
		/// <summary>The name of the directory.</summary>
		// Token: 0x04000CF9 RID: 3321
		DirectoryName = 2,
		/// <summary>The name of the file.</summary>
		// Token: 0x04000CFA RID: 3322
		FileName = 1,
		/// <summary>The date the file or folder was last opened.</summary>
		// Token: 0x04000CFB RID: 3323
		LastAccess = 32,
		/// <summary>The date the file or folder last had anything written to it.</summary>
		// Token: 0x04000CFC RID: 3324
		LastWrite = 16,
		/// <summary>The security settings of the file or folder.</summary>
		// Token: 0x04000CFD RID: 3325
		Security = 256,
		/// <summary>The size of the file or folder.</summary>
		// Token: 0x04000CFE RID: 3326
		Size = 8
	}
}
