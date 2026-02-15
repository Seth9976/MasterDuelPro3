using System;

namespace System.IO.Compression
{
	/// <summary>Specifies values for interacting with zip archive entries.</summary>
	// Token: 0x02000021 RID: 33
	public enum ZipArchiveMode
	{
		/// <summary>Only reading archive entries is permitted.</summary>
		// Token: 0x040000DA RID: 218
		Read,
		/// <summary>Only creating new archive entries is permitted.</summary>
		// Token: 0x040000DB RID: 219
		Create,
		/// <summary>Both read and write operations are permitted for archive entries.</summary>
		// Token: 0x040000DC RID: 220
		Update
	}
}
