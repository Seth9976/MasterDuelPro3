using System;
using System.Runtime.Serialization;

namespace ICSharpCode.SharpZipLib
{
	// Token: 0x02000004 RID: 4
	[Serializable]
	public class StreamUnsupportedException : StreamDecodingException
	{
		// Token: 0x06000009 RID: 9 RVA: 0x0000209F File Offset: 0x0000029F
		public StreamUnsupportedException()
			: base("Input stream is in a unsupported format")
		{
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000020AC File Offset: 0x000002AC
		public StreamUnsupportedException(string message)
			: base(message)
		{
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000020B5 File Offset: 0x000002B5
		public StreamUnsupportedException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000020BF File Offset: 0x000002BF
		protected StreamUnsupportedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x04000002 RID: 2
		private const string GenericMessage = "Input stream is in a unsupported format";
	}
}
