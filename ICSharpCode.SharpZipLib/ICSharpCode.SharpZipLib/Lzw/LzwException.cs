using System;
using System.Runtime.Serialization;

namespace ICSharpCode.SharpZipLib.Lzw
{
	// Token: 0x0200008C RID: 140
	[Serializable]
	public class LzwException : SharpZipBaseException
	{
		// Token: 0x060004B9 RID: 1209 RVA: 0x000041DF File Offset: 0x000023DF
		public LzwException()
		{
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x00002082 File Offset: 0x00000282
		public LzwException(string message)
			: base(message)
		{
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x0000208B File Offset: 0x0000028B
		public LzwException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x00002095 File Offset: 0x00000295
		protected LzwException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
