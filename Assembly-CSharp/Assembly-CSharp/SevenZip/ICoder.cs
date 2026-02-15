using System;
using System.IO;

namespace SevenZip
{
	// Token: 0x02000188 RID: 392
	public interface ICoder
	{
		// Token: 0x0600058D RID: 1421
		void Code(Stream inStream, Stream outStream, long inSize, long outSize, ICodeProgress progress);
	}
}
