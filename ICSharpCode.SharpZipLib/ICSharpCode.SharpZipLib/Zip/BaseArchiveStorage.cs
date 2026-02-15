using System;
using System.IO;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x02000038 RID: 56
	public abstract class BaseArchiveStorage : IArchiveStorage
	{
		// Token: 0x060001D6 RID: 470 RVA: 0x000086E9 File Offset: 0x000068E9
		protected BaseArchiveStorage(FileUpdateMode updateMode)
		{
			this.updateMode_ = updateMode;
		}

		// Token: 0x060001D7 RID: 471
		public abstract Stream GetTemporaryOutput();

		// Token: 0x060001D8 RID: 472
		public abstract Stream ConvertTemporaryToFinal();

		// Token: 0x060001D9 RID: 473
		public abstract Stream MakeTemporaryCopy(Stream stream);

		// Token: 0x060001DA RID: 474
		public abstract Stream OpenForDirectUpdate(Stream stream);

		// Token: 0x060001DB RID: 475
		public abstract void Dispose();

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060001DC RID: 476 RVA: 0x000086F8 File Offset: 0x000068F8
		public FileUpdateMode UpdateMode
		{
			get
			{
				return this.updateMode_;
			}
		}

		// Token: 0x0400011B RID: 283
		private readonly FileUpdateMode updateMode_;
	}
}
