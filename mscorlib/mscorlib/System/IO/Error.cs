using System;

namespace System.IO
{
	// Token: 0x02000795 RID: 1941
	internal static class Error
	{
		// Token: 0x06003D4F RID: 15695 RVA: 0x000EC2D1 File Offset: 0x000EA4D1
		internal static Exception GetStreamIsClosed()
		{
			return new ObjectDisposedException(null, "Cannot access a closed Stream.");
		}

		// Token: 0x06003D50 RID: 15696 RVA: 0x000EC2DE File Offset: 0x000EA4DE
		internal static Exception GetEndOfFile()
		{
			return new EndOfStreamException("Unable to read beyond the end of the stream.");
		}

		// Token: 0x06003D51 RID: 15697 RVA: 0x000EC2EA File Offset: 0x000EA4EA
		internal static Exception GetReadNotSupported()
		{
			return new NotSupportedException("Stream does not support reading.");
		}

		// Token: 0x06003D52 RID: 15698 RVA: 0x000EC2F6 File Offset: 0x000EA4F6
		internal static Exception GetWriteNotSupported()
		{
			return new NotSupportedException("Stream does not support writing.");
		}
	}
}
