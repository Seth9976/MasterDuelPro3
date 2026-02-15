using System;

namespace System.Reflection.Emit
{
	// Token: 0x02000668 RID: 1640
	internal struct ILExceptionBlock
	{
		// Token: 0x04001971 RID: 6513
		public const int CATCH = 0;

		// Token: 0x04001972 RID: 6514
		public const int FILTER = 1;

		// Token: 0x04001973 RID: 6515
		public const int FINALLY = 2;

		// Token: 0x04001974 RID: 6516
		public const int FAULT = 4;

		// Token: 0x04001975 RID: 6517
		public const int FILTER_START = -1;

		// Token: 0x04001976 RID: 6518
		internal Type extype;

		// Token: 0x04001977 RID: 6519
		internal int type;

		// Token: 0x04001978 RID: 6520
		internal int start;

		// Token: 0x04001979 RID: 6521
		internal int len;

		// Token: 0x0400197A RID: 6522
		internal int filter_offset;
	}
}
