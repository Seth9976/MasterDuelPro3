using System;

namespace System.IO
{
	/// <summary>Specifies whether to search the current directory, or the current directory and all subdirectories. </summary>
	// Token: 0x020007C0 RID: 1984
	public enum SearchOption
	{
		/// <summary>Includes only the current directory in a search operation.</summary>
		// Token: 0x0400203C RID: 8252
		TopDirectoryOnly,
		/// <summary>Includes the current directory and all its subdirectories in a search operation. This option includes reparse points such as mounted drives and symbolic links in the search.</summary>
		// Token: 0x0400203D RID: 8253
		AllDirectories
	}
}
