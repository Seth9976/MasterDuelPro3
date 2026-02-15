using System;
using ICSharpCode.SharpZipLib.Core;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x0200000B RID: 11
	public interface IEntryFactory
	{
		// Token: 0x06000053 RID: 83
		ZipEntry MakeFileEntry(string fileName);

		// Token: 0x06000054 RID: 84
		ZipEntry MakeFileEntry(string fileName, bool useFileSystem);

		// Token: 0x06000055 RID: 85
		ZipEntry MakeFileEntry(string fileName, string entryName, bool useFileSystem);

		// Token: 0x06000056 RID: 86
		ZipEntry MakeDirectoryEntry(string directoryName);

		// Token: 0x06000057 RID: 87
		ZipEntry MakeDirectoryEntry(string directoryName, bool useFileSystem);

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000058 RID: 88
		// (set) Token: 0x06000059 RID: 89
		INameTransform NameTransform { get; set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600005A RID: 90
		ZipEntryFactory.TimeSetting Setting { get; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600005B RID: 91
		DateTime FixedDateTime { get; }
	}
}
