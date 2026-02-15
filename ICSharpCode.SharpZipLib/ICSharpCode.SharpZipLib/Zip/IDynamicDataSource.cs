using System;
using System.IO;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x02000034 RID: 52
	public interface IDynamicDataSource
	{
		// Token: 0x060001CB RID: 459
		Stream GetSource(ZipEntry entry, string name);
	}
}
