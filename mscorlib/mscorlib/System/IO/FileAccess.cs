using System;

namespace System.IO
{
	/// <summary>Defines constants for read, write, or read/write access to a file.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000796 RID: 1942
	[Flags]
	public enum FileAccess
	{
		/// <summary>Read access to the file. Data can be read from the file. Combine with Write for read/write access.</summary>
		// Token: 0x04001F78 RID: 8056
		Read = 1,
		/// <summary>Write access to the file. Data can be written to the file. Combine with Read for read/write access.</summary>
		// Token: 0x04001F79 RID: 8057
		Write = 2,
		/// <summary>Read and write access to the file. Data can be written to and read from the file.</summary>
		// Token: 0x04001F7A RID: 8058
		ReadWrite = 3
	}
}
