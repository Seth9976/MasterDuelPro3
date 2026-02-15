using System;

namespace System.IO.Compression
{
	// Token: 0x0200002D RID: 45
	internal enum ZipVersionNeededValues : ushort
	{
		// Token: 0x04000125 RID: 293
		Default = 10,
		// Token: 0x04000126 RID: 294
		ExplicitDirectory = 20,
		// Token: 0x04000127 RID: 295
		Deflate = 20,
		// Token: 0x04000128 RID: 296
		Deflate64,
		// Token: 0x04000129 RID: 297
		Zip64 = 45
	}
}
