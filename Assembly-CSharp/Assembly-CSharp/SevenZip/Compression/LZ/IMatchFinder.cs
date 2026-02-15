using System;

namespace SevenZip.Compression.LZ
{
	// Token: 0x020001A1 RID: 417
	internal interface IMatchFinder : IInWindowStream
	{
		// Token: 0x06000619 RID: 1561
		void Create(uint historySize, uint keepAddBufferBefore, uint matchMaxLen, uint keepAddBufferAfter);

		// Token: 0x0600061A RID: 1562
		uint GetMatches(uint[] distances);

		// Token: 0x0600061B RID: 1563
		void Skip(uint num);
	}
}
