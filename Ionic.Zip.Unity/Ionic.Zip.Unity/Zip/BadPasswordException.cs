using System;
using System.Runtime.InteropServices;

namespace Ionic.Zip
{
	// Token: 0x02000010 RID: 16
	[Guid("ebc25cf6-9120-4283-b972-0e5520d0000B")]
	public class BadPasswordException : ZipException
	{
		// Token: 0x0600004D RID: 77 RVA: 0x0000247B File Offset: 0x0000067B
		public BadPasswordException()
		{
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002483 File Offset: 0x00000683
		public BadPasswordException(string message)
			: base(message)
		{
		}

		// Token: 0x0600004F RID: 79 RVA: 0x0000248C File Offset: 0x0000068C
		public BadPasswordException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
