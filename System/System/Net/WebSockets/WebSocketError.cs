using System;

namespace System.Net.WebSockets
{
	/// <summary>Contains the list of possible WebSocket errors.</summary>
	// Token: 0x020004E6 RID: 1254
	public enum WebSocketError
	{
		/// <summary>Indicates that there was no native error information for the exception.</summary>
		// Token: 0x04001646 RID: 5702
		Success,
		/// <summary>Indicates that a WebSocket frame with an unknown opcode was received.</summary>
		// Token: 0x04001647 RID: 5703
		InvalidMessageType,
		/// <summary>Indicates a general error.</summary>
		// Token: 0x04001648 RID: 5704
		Faulted,
		/// <summary>Indicates that an unknown native error occurred.</summary>
		// Token: 0x04001649 RID: 5705
		NativeError,
		/// <summary>Indicates that the incoming request was not a valid websocket request.</summary>
		// Token: 0x0400164A RID: 5706
		NotAWebSocket,
		/// <summary>Indicates that the client requested an unsupported version of the WebSocket protocol.</summary>
		// Token: 0x0400164B RID: 5707
		UnsupportedVersion,
		/// <summary>Indicates that the client requested an unsupported WebSocket subprotocol.</summary>
		// Token: 0x0400164C RID: 5708
		UnsupportedProtocol,
		/// <summary>Indicates an error occurred when parsing the HTTP headers during the opening handshake.</summary>
		// Token: 0x0400164D RID: 5709
		HeaderError,
		/// <summary>Indicates that the connection was terminated unexpectedly.</summary>
		// Token: 0x0400164E RID: 5710
		ConnectionClosedPrematurely,
		/// <summary>Indicates the WebSocket is an invalid state for the given operation (such as being closed or aborted).</summary>
		// Token: 0x0400164F RID: 5711
		InvalidState
	}
}
