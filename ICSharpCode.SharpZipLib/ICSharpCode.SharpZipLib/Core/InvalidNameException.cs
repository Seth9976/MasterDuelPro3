using System;
using System.Runtime.Serialization;

namespace ICSharpCode.SharpZipLib.Core
{
	// Token: 0x020000B6 RID: 182
	[Serializable]
	public class InvalidNameException : SharpZipBaseException
	{
		// Token: 0x0600059C RID: 1436 RVA: 0x0001A70C File Offset: 0x0001890C
		public InvalidNameException()
			: base("An invalid name was specified")
		{
		}

		// Token: 0x0600059D RID: 1437 RVA: 0x00002082 File Offset: 0x00000282
		public InvalidNameException(string message)
			: base(message)
		{
		}

		// Token: 0x0600059E RID: 1438 RVA: 0x0000208B File Offset: 0x0000028B
		public InvalidNameException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x0600059F RID: 1439 RVA: 0x00002095 File Offset: 0x00000295
		protected InvalidNameException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
