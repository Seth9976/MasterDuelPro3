using System;
using System.IO;

namespace System.Data.SqlTypes
{
	// Token: 0x020000D3 RID: 211
	internal abstract class SqlStreamChars
	{
		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x06000B13 RID: 2835
		public abstract long Length { get; }

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06000B14 RID: 2836
		public abstract long Position { get; }

		// Token: 0x06000B15 RID: 2837
		public abstract int Read(char[] buffer, int offset, int count);

		// Token: 0x06000B16 RID: 2838
		public abstract long Seek(long offset, SeekOrigin origin);
	}
}
