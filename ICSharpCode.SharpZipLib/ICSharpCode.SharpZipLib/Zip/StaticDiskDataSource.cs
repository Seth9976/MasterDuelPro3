using System;
using System.IO;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x02000035 RID: 53
	public class StaticDiskDataSource : IStaticDataSource
	{
		// Token: 0x060001CC RID: 460 RVA: 0x000086AD File Offset: 0x000068AD
		public StaticDiskDataSource(string fileName)
		{
			this.fileName_ = fileName;
		}

		// Token: 0x060001CD RID: 461 RVA: 0x000086BC File Offset: 0x000068BC
		public Stream GetSource()
		{
			return File.Open(this.fileName_, FileMode.Open, FileAccess.Read, FileShare.Read);
		}

		// Token: 0x0400011A RID: 282
		private readonly string fileName_;
	}
}
