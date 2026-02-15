using System;

namespace Org.Brotli.Dec
{
	// Token: 0x02000082 RID: 130
	internal sealed class RunningState
	{
		// Token: 0x040002F9 RID: 761
		internal const int Uninitialized = 0;

		// Token: 0x040002FA RID: 762
		internal const int BlockStart = 1;

		// Token: 0x040002FB RID: 763
		internal const int CompressedBlockStart = 2;

		// Token: 0x040002FC RID: 764
		internal const int MainLoop = 3;

		// Token: 0x040002FD RID: 765
		internal const int ReadMetadata = 4;

		// Token: 0x040002FE RID: 766
		internal const int CopyUncompressed = 5;

		// Token: 0x040002FF RID: 767
		internal const int InsertLoop = 6;

		// Token: 0x04000300 RID: 768
		internal const int CopyLoop = 7;

		// Token: 0x04000301 RID: 769
		internal const int CopyWrapBuffer = 8;

		// Token: 0x04000302 RID: 770
		internal const int Transform = 9;

		// Token: 0x04000303 RID: 771
		internal const int Finished = 10;

		// Token: 0x04000304 RID: 772
		internal const int Closed = 11;

		// Token: 0x04000305 RID: 773
		internal const int Write = 12;
	}
}
