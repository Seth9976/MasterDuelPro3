using System;
using System.Runtime.Serialization;

namespace ICSharpCode.SharpZipLib.Tar
{
	// Token: 0x02000076 RID: 118
	[Serializable]
	public class TarException : SharpZipBaseException
	{
		// Token: 0x06000400 RID: 1024 RVA: 0x000041DF File Offset: 0x000023DF
		public TarException()
		{
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x00002082 File Offset: 0x00000282
		public TarException(string message)
			: base(message)
		{
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x0000208B File Offset: 0x0000028B
		public TarException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x00002095 File Offset: 0x00000295
		protected TarException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
