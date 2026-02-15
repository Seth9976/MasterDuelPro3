using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace Unity.IO.Archive
{
	// Token: 0x02000047 RID: 71
	[RequiredByNativeCode]
	[NativeHeader("Runtime/VirtualFileSystem/ArchiveFileSystem/ArchiveFileHandle.h")]
	public struct ArchiveFileInfo
	{
		// Token: 0x040000E2 RID: 226
		public string Filename;

		// Token: 0x040000E3 RID: 227
		public ulong FileSize;
	}
}
