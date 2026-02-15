using System;
using System.Runtime.Serialization;

namespace ICSharpCode.SharpZipLib.GZip
{
	// Token: 0x02000091 RID: 145
	[Serializable]
	public class GZipException : SharpZipBaseException
	{
		// Token: 0x060004D5 RID: 1237 RVA: 0x000041DF File Offset: 0x000023DF
		public GZipException()
		{
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x00002082 File Offset: 0x00000282
		public GZipException(string message)
			: base(message)
		{
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x0000208B File Offset: 0x0000028B
		public GZipException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x00002095 File Offset: 0x00000295
		protected GZipException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
