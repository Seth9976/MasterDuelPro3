using System;
using System.Runtime.Serialization;

namespace System.Runtime
{
	// Token: 0x0200000E RID: 14
	[Serializable]
	internal class CallbackException : FatalException
	{
		// Token: 0x06000033 RID: 51 RVA: 0x00002822 File Offset: 0x00000A22
		public CallbackException()
		{
		}

		// Token: 0x06000034 RID: 52 RVA: 0x0000282A File Offset: 0x00000A2A
		public CallbackException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002834 File Offset: 0x00000A34
		protected CallbackException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
