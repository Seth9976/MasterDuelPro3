using System;

namespace System.IO.Compression
{
	/// <summary>Specifies values that indicate whether a compression operation emphasizes speed or compression size.</summary>
	// Token: 0x02000370 RID: 880
	public enum CompressionLevel
	{
		/// <summary>The compression operation should be optimally compressed, even if the operation takes a longer time to complete.</summary>
		// Token: 0x04000D2D RID: 3373
		Optimal,
		/// <summary>The compression operation should complete as quickly as possible, even if the resulting file is not optimally compressed.</summary>
		// Token: 0x04000D2E RID: 3374
		Fastest,
		/// <summary>No compression should be performed on the file.</summary>
		// Token: 0x04000D2F RID: 3375
		NoCompression
	}
}
