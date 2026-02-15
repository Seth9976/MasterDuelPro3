using System;
using System.Runtime.InteropServices;

namespace Ionic.Zip
{
	// Token: 0x02000011 RID: 17
	[Guid("ebc25cf6-9120-4283-b972-0e5520d0000A")]
	public class BadReadException : ZipException
	{
		// Token: 0x06000050 RID: 80 RVA: 0x0000247B File Offset: 0x0000067B
		public BadReadException()
		{
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002483 File Offset: 0x00000683
		public BadReadException(string message)
			: base(message)
		{
		}

		// Token: 0x06000052 RID: 82 RVA: 0x0000248C File Offset: 0x0000068C
		public BadReadException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
