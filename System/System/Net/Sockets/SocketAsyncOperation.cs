using System;

namespace System.Net.Sockets
{
	/// <summary>The type of asynchronous socket operation most recently performed with this context object.</summary>
	// Token: 0x020004B8 RID: 1208
	public enum SocketAsyncOperation
	{
		/// <summary>None of the socket operations.</summary>
		// Token: 0x040014A6 RID: 5286
		None,
		/// <summary>A socket Accept operation. </summary>
		// Token: 0x040014A7 RID: 5287
		Accept,
		/// <summary>A socket Connect operation.</summary>
		// Token: 0x040014A8 RID: 5288
		Connect,
		/// <summary>A socket Disconnect operation.</summary>
		// Token: 0x040014A9 RID: 5289
		Disconnect,
		/// <summary>A socket Receive operation.</summary>
		// Token: 0x040014AA RID: 5290
		Receive,
		/// <summary>A socket ReceiveFrom operation.</summary>
		// Token: 0x040014AB RID: 5291
		ReceiveFrom,
		/// <summary>A socket ReceiveMessageFrom operation.</summary>
		// Token: 0x040014AC RID: 5292
		ReceiveMessageFrom,
		/// <summary>A socket Send operation.</summary>
		// Token: 0x040014AD RID: 5293
		Send,
		/// <summary>A socket SendPackets operation.</summary>
		// Token: 0x040014AE RID: 5294
		SendPackets,
		/// <summary>A socket SendTo operation.</summary>
		// Token: 0x040014AF RID: 5295
		SendTo
	}
}
