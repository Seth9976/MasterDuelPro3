using System;
using System.Runtime.Serialization;

namespace ICSharpCode.SharpZipLib.BZip2
{
	// Token: 0x020000C9 RID: 201
	[Serializable]
	public class BZip2Exception : SharpZipBaseException
	{
		// Token: 0x060005F9 RID: 1529 RVA: 0x000041DF File Offset: 0x000023DF
		public BZip2Exception()
		{
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x00002082 File Offset: 0x00000282
		public BZip2Exception(string message)
			: base(message)
		{
		}

		// Token: 0x060005FB RID: 1531 RVA: 0x0000208B File Offset: 0x0000028B
		public BZip2Exception(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x00002095 File Offset: 0x00000295
		protected BZip2Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
