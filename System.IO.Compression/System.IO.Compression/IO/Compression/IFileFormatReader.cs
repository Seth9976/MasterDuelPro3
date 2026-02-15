using System;

namespace System.IO.Compression
{
	// Token: 0x02000010 RID: 16
	internal interface IFileFormatReader
	{
		// Token: 0x06000060 RID: 96
		bool ReadHeader(InputBuffer input);

		// Token: 0x06000061 RID: 97
		bool ReadFooter(InputBuffer input);

		// Token: 0x06000062 RID: 98
		void UpdateWithBytesRead(byte[] buffer, int offset, int bytesToCopy);

		// Token: 0x06000063 RID: 99
		void Validate();
	}
}
