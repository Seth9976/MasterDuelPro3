using System;

namespace System.Net.WebSockets
{
	/// <summary> Defines the different states a WebSockets instance can be in.</summary>
	// Token: 0x020004EA RID: 1258
	public enum WebSocketState
	{
		/// <summary>Reserved for future use.</summary>
		// Token: 0x0400165B RID: 5723
		None,
		/// <summary>The connection is negotiating the handshake with the remote endpoint.</summary>
		// Token: 0x0400165C RID: 5724
		Connecting,
		/// <summary>The initial state after the HTTP handshake has been completed.</summary>
		// Token: 0x0400165D RID: 5725
		Open,
		/// <summary>A close message was sent to the remote endpoint.</summary>
		// Token: 0x0400165E RID: 5726
		CloseSent,
		/// <summary>A close message was received from the remote endpoint.</summary>
		// Token: 0x0400165F RID: 5727
		CloseReceived,
		/// <summary>Indicates the WebSocket close handshake completed gracefully.</summary>
		// Token: 0x04001660 RID: 5728
		Closed,
		/// <summary>Reserved for future use.</summary>
		// Token: 0x04001661 RID: 5729
		Aborted
	}
}
