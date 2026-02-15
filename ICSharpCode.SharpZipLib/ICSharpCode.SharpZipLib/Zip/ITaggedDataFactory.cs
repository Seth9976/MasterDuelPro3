using System;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x02000021 RID: 33
	internal interface ITaggedDataFactory
	{
		// Token: 0x060000EB RID: 235
		ITaggedData Create(short tag, byte[] data, int offset, int count);
	}
}
