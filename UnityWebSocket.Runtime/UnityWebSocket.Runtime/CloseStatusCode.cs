using System;

namespace UnityWebSocket
{
	// Token: 0x02000005 RID: 5
	public enum CloseStatusCode : ushort
	{
		// Token: 0x0400000A RID: 10
		Unknown = 65534,
		// Token: 0x0400000B RID: 11
		Normal = 1000,
		// Token: 0x0400000C RID: 12
		Away,
		// Token: 0x0400000D RID: 13
		ProtocolError,
		// Token: 0x0400000E RID: 14
		UnsupportedData,
		// Token: 0x0400000F RID: 15
		Undefined,
		// Token: 0x04000010 RID: 16
		NoStatus,
		// Token: 0x04000011 RID: 17
		Abnormal,
		// Token: 0x04000012 RID: 18
		InvalidData,
		// Token: 0x04000013 RID: 19
		PolicyViolation,
		// Token: 0x04000014 RID: 20
		TooBig,
		// Token: 0x04000015 RID: 21
		MandatoryExtension,
		// Token: 0x04000016 RID: 22
		ServerError,
		// Token: 0x04000017 RID: 23
		TlsHandshakeFailure = 1015
	}
}
