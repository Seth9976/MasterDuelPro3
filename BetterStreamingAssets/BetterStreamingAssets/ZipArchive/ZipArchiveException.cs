using System;

namespace Better.StreamingAssets.ZipArchive
{
	// Token: 0x02000012 RID: 18
	public class ZipArchiveException : Exception
	{
		// Token: 0x06000049 RID: 73 RVA: 0x0000331C File Offset: 0x0000151C
		public ZipArchiveException(string msg)
			: base(msg)
		{
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00003325 File Offset: 0x00001525
		public ZipArchiveException(string msg, Exception inner)
			: base(msg, inner)
		{
		}
	}
}
