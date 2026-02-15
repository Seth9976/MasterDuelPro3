using System;

namespace System.IO
{
	/// <summary>Changes that might occur to a file or directory.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200036B RID: 875
	[Flags]
	public enum WatcherChangeTypes
	{
		/// <summary>The creation, deletion, change, or renaming of a file or folder.</summary>
		// Token: 0x04000D10 RID: 3344
		All = 15,
		/// <summary>The change of a file or folder. The types of changes include: changes to size, attributes, security settings, last write, and last access time.</summary>
		// Token: 0x04000D11 RID: 3345
		Changed = 4,
		/// <summary>The creation of a file or folder.</summary>
		// Token: 0x04000D12 RID: 3346
		Created = 1,
		/// <summary>The deletion of a file or folder.</summary>
		// Token: 0x04000D13 RID: 3347
		Deleted = 2,
		/// <summary>The renaming of a file or folder.</summary>
		// Token: 0x04000D14 RID: 3348
		Renamed = 8
	}
}
