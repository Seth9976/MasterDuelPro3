using System;
using System.Runtime.Serialization;

namespace ICSharpCode.SharpZipLib
{
	// Token: 0x02000003 RID: 3
	[Serializable]
	public class StreamDecodingException : SharpZipBaseException
	{
		// Token: 0x06000005 RID: 5 RVA: 0x00002075 File Offset: 0x00000275
		public StreamDecodingException()
			: base("Input stream could not be decoded")
		{
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002082 File Offset: 0x00000282
		public StreamDecodingException(string message)
			: base(message)
		{
		}

		// Token: 0x06000007 RID: 7 RVA: 0x0000208B File Offset: 0x0000028B
		public StreamDecodingException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002095 File Offset: 0x00000295
		protected StreamDecodingException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x04000001 RID: 1
		private const string GenericMessage = "Input stream could not be decoded";
	}
}
