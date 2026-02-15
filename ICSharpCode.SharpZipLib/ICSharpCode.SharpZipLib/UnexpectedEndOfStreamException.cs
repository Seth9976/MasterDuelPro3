using System;
using System.Runtime.Serialization;

namespace ICSharpCode.SharpZipLib
{
	// Token: 0x02000005 RID: 5
	[Serializable]
	public class UnexpectedEndOfStreamException : StreamDecodingException
	{
		// Token: 0x0600000D RID: 13 RVA: 0x000020C9 File Offset: 0x000002C9
		public UnexpectedEndOfStreamException()
			: base("Input stream ended unexpectedly")
		{
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000020AC File Offset: 0x000002AC
		public UnexpectedEndOfStreamException(string message)
			: base(message)
		{
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000020B5 File Offset: 0x000002B5
		public UnexpectedEndOfStreamException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000020BF File Offset: 0x000002BF
		protected UnexpectedEndOfStreamException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x04000003 RID: 3
		private const string GenericMessage = "Input stream ended unexpectedly";
	}
}
