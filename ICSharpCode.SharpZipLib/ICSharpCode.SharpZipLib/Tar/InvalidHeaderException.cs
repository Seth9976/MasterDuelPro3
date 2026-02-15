using System;
using System.Runtime.Serialization;

namespace ICSharpCode.SharpZipLib.Tar
{
	// Token: 0x0200006A RID: 106
	[Serializable]
	public class InvalidHeaderException : TarException
	{
		// Token: 0x06000377 RID: 887 RVA: 0x0001176A File Offset: 0x0000F96A
		public InvalidHeaderException()
		{
		}

		// Token: 0x06000378 RID: 888 RVA: 0x00011772 File Offset: 0x0000F972
		public InvalidHeaderException(string message)
			: base(message)
		{
		}

		// Token: 0x06000379 RID: 889 RVA: 0x0001177B File Offset: 0x0000F97B
		public InvalidHeaderException(string message, Exception exception)
			: base(message, exception)
		{
		}

		// Token: 0x0600037A RID: 890 RVA: 0x00011785 File Offset: 0x0000F985
		protected InvalidHeaderException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
