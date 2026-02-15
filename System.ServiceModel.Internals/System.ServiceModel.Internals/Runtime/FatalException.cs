using System;
using System.Runtime.Serialization;

namespace System.Runtime
{
	// Token: 0x02000010 RID: 16
	[Serializable]
	internal class FatalException : SystemException
	{
		// Token: 0x06000042 RID: 66 RVA: 0x00002AD1 File Offset: 0x00000CD1
		public FatalException()
		{
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002AD9 File Offset: 0x00000CD9
		public FatalException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002AE3 File Offset: 0x00000CE3
		protected FatalException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
