using System;

namespace System.Net.Sockets
{
	// Token: 0x020004C5 RID: 1221
	internal enum SocketOperation
	{
		// Token: 0x0400155F RID: 5471
		Accept,
		// Token: 0x04001560 RID: 5472
		Connect,
		// Token: 0x04001561 RID: 5473
		Receive,
		// Token: 0x04001562 RID: 5474
		ReceiveFrom,
		// Token: 0x04001563 RID: 5475
		Send,
		// Token: 0x04001564 RID: 5476
		SendTo,
		// Token: 0x04001565 RID: 5477
		RecvJustCallback,
		// Token: 0x04001566 RID: 5478
		SendJustCallback,
		// Token: 0x04001567 RID: 5479
		Disconnect,
		// Token: 0x04001568 RID: 5480
		AcceptReceive,
		// Token: 0x04001569 RID: 5481
		ReceiveGeneric,
		// Token: 0x0400156A RID: 5482
		SendGeneric
	}
}
