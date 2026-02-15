using System;

namespace System.IO.Compression
{
	// Token: 0x0200000F RID: 15
	internal interface IFileFormatWriter
	{
		// Token: 0x0600005D RID: 93
		byte[] GetHeader();

		// Token: 0x0600005E RID: 94
		void UpdateWithBytesRead(byte[] buffer, int offset, int bytesToCopy);

		// Token: 0x0600005F RID: 95
		byte[] GetFooter();
	}
}
