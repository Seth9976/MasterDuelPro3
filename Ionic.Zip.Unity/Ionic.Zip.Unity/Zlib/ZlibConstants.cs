using System;

namespace Ionic.Zlib
{
	// Token: 0x0200006E RID: 110
	public static class ZlibConstants
	{
		// Token: 0x040003CA RID: 970
		public const int WindowBitsMax = 15;

		// Token: 0x040003CB RID: 971
		public const int WindowBitsDefault = 15;

		// Token: 0x040003CC RID: 972
		public const int Z_OK = 0;

		// Token: 0x040003CD RID: 973
		public const int Z_STREAM_END = 1;

		// Token: 0x040003CE RID: 974
		public const int Z_NEED_DICT = 2;

		// Token: 0x040003CF RID: 975
		public const int Z_STREAM_ERROR = -2;

		// Token: 0x040003D0 RID: 976
		public const int Z_DATA_ERROR = -3;

		// Token: 0x040003D1 RID: 977
		public const int Z_BUF_ERROR = -5;

		// Token: 0x040003D2 RID: 978
		public const int WorkingBufferSizeDefault = 16384;

		// Token: 0x040003D3 RID: 979
		public const int WorkingBufferSizeMin = 1024;
	}
}
