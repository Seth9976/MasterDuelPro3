using System;

namespace ICSharpCode.SharpZipLib.Lzw
{
	// Token: 0x0200008B RID: 139
	public sealed class LzwConstants
	{
		// Token: 0x060004B8 RID: 1208 RVA: 0x000080C2 File Offset: 0x000062C2
		private LzwConstants()
		{
		}

		// Token: 0x04000396 RID: 918
		public const int MAGIC = 8093;

		// Token: 0x04000397 RID: 919
		public const int MAX_BITS = 16;

		// Token: 0x04000398 RID: 920
		public const int BIT_MASK = 31;

		// Token: 0x04000399 RID: 921
		public const int EXTENDED_MASK = 32;

		// Token: 0x0400039A RID: 922
		public const int RESERVED_MASK = 96;

		// Token: 0x0400039B RID: 923
		public const int BLOCK_MODE_MASK = 128;

		// Token: 0x0400039C RID: 924
		public const int HDR_SIZE = 3;

		// Token: 0x0400039D RID: 925
		public const int INIT_BITS = 9;
	}
}
