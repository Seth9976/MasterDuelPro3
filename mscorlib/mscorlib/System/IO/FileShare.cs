using System;

namespace System.IO
{
	/// <summary>Contains constants for controlling the kind of access other <see cref="T:System.IO.FileStream" /> objects can have to the same file.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200079B RID: 1947
	[Flags]
	public enum FileShare
	{
		/// <summary>Declines sharing of the current file. Any request to open the file (by this process or another process) will fail until the file is closed.</summary>
		// Token: 0x04001F8F RID: 8079
		None = 0,
		/// <summary>Allows subsequent opening of the file for reading. If this flag is not specified, any request to open the file for reading (by this process or another process) will fail until the file is closed. However, even if this flag is specified, additional permissions might still be needed to access the file.</summary>
		// Token: 0x04001F90 RID: 8080
		Read = 1,
		/// <summary>Allows subsequent opening of the file for writing. If this flag is not specified, any request to open the file for writing (by this process or another process) will fail until the file is closed. However, even if this flag is specified, additional permissions might still be needed to access the file.</summary>
		// Token: 0x04001F91 RID: 8081
		Write = 2,
		/// <summary>Allows subsequent opening of the file for reading or writing. If this flag is not specified, any request to open the file for reading or writing (by this process or another process) will fail until the file is closed. However, even if this flag is specified, additional permissions might still be needed to access the file.</summary>
		// Token: 0x04001F92 RID: 8082
		ReadWrite = 3,
		/// <summary>Allows subsequent deleting of a file.</summary>
		// Token: 0x04001F93 RID: 8083
		Delete = 4,
		/// <summary>Makes the file handle inheritable by child processes. This is not directly supported by Win32.</summary>
		// Token: 0x04001F94 RID: 8084
		Inheritable = 16
	}
}
