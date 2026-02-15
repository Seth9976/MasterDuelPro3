using System;
using System.Runtime.InteropServices;

namespace Ionic.Zip
{
	// Token: 0x02000013 RID: 19
	[Guid("ebc25cf6-9120-4283-b972-0e5520d00008")]
	public class SfxGenerationException : ZipException
	{
		// Token: 0x06000055 RID: 85 RVA: 0x0000247B File Offset: 0x0000067B
		public SfxGenerationException()
		{
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002483 File Offset: 0x00000683
		public SfxGenerationException(string message)
			: base(message)
		{
		}
	}
}
