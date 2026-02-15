using System;
using System.IO;

namespace SevenZip.Compression.LZ
{
	// Token: 0x020001A0 RID: 416
	internal interface IInWindowStream
	{
		// Token: 0x06000613 RID: 1555
		void SetStream(Stream inStream);

		// Token: 0x06000614 RID: 1556
		void Init();

		// Token: 0x06000615 RID: 1557
		void ReleaseStream();

		// Token: 0x06000616 RID: 1558
		byte GetIndexByte(int index);

		// Token: 0x06000617 RID: 1559
		uint GetMatchLen(int index, uint distance, uint limit);

		// Token: 0x06000618 RID: 1560
		uint GetNumAvailableBytes();
	}
}
