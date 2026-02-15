using System;
using System.IO;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x02000037 RID: 55
	public interface IArchiveStorage
	{
		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060001D0 RID: 464
		FileUpdateMode UpdateMode { get; }

		// Token: 0x060001D1 RID: 465
		Stream GetTemporaryOutput();

		// Token: 0x060001D2 RID: 466
		Stream ConvertTemporaryToFinal();

		// Token: 0x060001D3 RID: 467
		Stream MakeTemporaryCopy(Stream stream);

		// Token: 0x060001D4 RID: 468
		Stream OpenForDirectUpdate(Stream stream);

		// Token: 0x060001D5 RID: 469
		void Dispose();
	}
}
