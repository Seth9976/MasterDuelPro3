using System;

namespace Org.Brotli.Dec
{
	// Token: 0x02000076 RID: 118
	[Serializable]
	internal class BrotliRuntimeException : Exception
	{
		// Token: 0x06000247 RID: 583 RVA: 0x0000763F File Offset: 0x0000583F
		internal BrotliRuntimeException(string message)
			: base(message)
		{
		}

		// Token: 0x06000248 RID: 584 RVA: 0x00007648 File Offset: 0x00005848
		internal BrotliRuntimeException(string message, Exception cause)
			: base(message, cause)
		{
		}
	}
}
