using System;
using System.Runtime.Serialization;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x0200001B RID: 27
	[Serializable]
	public class ZipException : SharpZipBaseException
	{
		// Token: 0x060000C5 RID: 197 RVA: 0x000041DF File Offset: 0x000023DF
		public ZipException()
		{
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00002082 File Offset: 0x00000282
		public ZipException(string message)
			: base(message)
		{
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x0000208B File Offset: 0x0000028B
		public ZipException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00002095 File Offset: 0x00000295
		protected ZipException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
