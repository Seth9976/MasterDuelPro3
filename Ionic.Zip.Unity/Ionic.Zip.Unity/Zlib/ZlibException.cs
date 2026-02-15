using System;
using System.Runtime.InteropServices;

namespace Ionic.Zlib
{
	// Token: 0x02000065 RID: 101
	[Guid("ebc25cf6-9120-4283-b972-0e5520d0000E")]
	public class ZlibException : Exception
	{
		// Token: 0x0600047A RID: 1146 RVA: 0x00002460 File Offset: 0x00000660
		public ZlibException()
		{
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x00002468 File Offset: 0x00000668
		public ZlibException(string s)
			: base(s)
		{
		}
	}
}
