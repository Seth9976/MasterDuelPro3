using System;
using System.IO;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x02000036 RID: 54
	public class DynamicDiskDataSource : IDynamicDataSource
	{
		// Token: 0x060001CE RID: 462 RVA: 0x000086CC File Offset: 0x000068CC
		public Stream GetSource(ZipEntry entry, string name)
		{
			Stream stream = null;
			if (name != null)
			{
				stream = File.Open(name, FileMode.Open, FileAccess.Read, FileShare.Read);
			}
			return stream;
		}
	}
}
