using System;
using System.Runtime.InteropServices;

namespace Ionic.Zip
{
	// Token: 0x02000012 RID: 18
	[Guid("ebc25cf6-9120-4283-b972-0e5520d00009")]
	public class BadCrcException : ZipException
	{
		// Token: 0x06000053 RID: 83 RVA: 0x0000247B File Offset: 0x0000067B
		public BadCrcException()
		{
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002483 File Offset: 0x00000683
		public BadCrcException(string message)
			: base(message)
		{
		}
	}
}
