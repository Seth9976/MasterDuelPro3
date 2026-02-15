using System;

namespace System.Net.WebSockets
{
	/// <summary>An instance of this class represents the result of performing a single ReceiveAsync operation on a WebSocket.</summary>
	// Token: 0x020004E9 RID: 1257
	public class WebSocketReceiveResult
	{
		/// <summary>Creates an instance of the <see cref="T:System.Net.WebSockets.WebSocketReceiveResult" /> class.</summary>
		/// <param name="count">The number of bytes received.</param>
		/// <param name="messageType">The type of message that was received.</param>
		/// <param name="endOfMessage">Indicates whether this is the final message.</param>
		// Token: 0x06001EC5 RID: 7877 RVA: 0x00087300 File Offset: 0x00085500
		public WebSocketReceiveResult(int count, WebSocketMessageType messageType, bool endOfMessage)
			: this(count, messageType, endOfMessage, null, null)
		{
		}

		/// <summary>Creates an instance of the <see cref="T:System.Net.WebSockets.WebSocketReceiveResult" /> class.</summary>
		/// <param name="count">The number of bytes received.</param>
		/// <param name="messageType">The type of message that was received.</param>
		/// <param name="endOfMessage">Indicates whether this is the final message.</param>
		/// <param name="closeStatus">Indicates the <see cref="T:System.Net.WebSockets.WebSocketCloseStatus" /> of the connection.</param>
		/// <param name="closeStatusDescription">The description of <paramref name="closeStatus" />.</param>
		// Token: 0x06001EC6 RID: 7878 RVA: 0x00087320 File Offset: 0x00085520
		public WebSocketReceiveResult(int count, WebSocketMessageType messageType, bool endOfMessage, WebSocketCloseStatus? closeStatus, string closeStatusDescription)
		{
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			this.Count = count;
			this.EndOfMessage = endOfMessage;
			this.MessageType = messageType;
			this.CloseStatus = closeStatus;
			this.CloseStatusDescription = closeStatusDescription;
		}

		/// <summary>Indicates the number of bytes that the WebSocket received.</summary>
		/// <returns>Returns <see cref="T:System.Int32" />.</returns>
		// Token: 0x170006B4 RID: 1716
		// (get) Token: 0x06001EC7 RID: 7879 RVA: 0x0008735C File Offset: 0x0008555C
		public int Count { get; }

		/// <summary>Indicates whether the message has been received completely.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.</returns>
		// Token: 0x170006B5 RID: 1717
		// (get) Token: 0x06001EC8 RID: 7880 RVA: 0x00087364 File Offset: 0x00085564
		public bool EndOfMessage { get; }

		/// <summary>Indicates whether the current message is a UTF-8 message or a binary message.</summary>
		/// <returns>Returns <see cref="T:System.Net.WebSockets.WebSocketMessageType" />.</returns>
		// Token: 0x170006B6 RID: 1718
		// (get) Token: 0x06001EC9 RID: 7881 RVA: 0x0008736C File Offset: 0x0008556C
		public WebSocketMessageType MessageType { get; }

		/// <summary>Indicates the reason why the remote endpoint initiated the close handshake.</summary>
		/// <returns>Returns <see cref="T:System.Net.WebSockets.WebSocketCloseStatus" />.</returns>
		// Token: 0x170006B7 RID: 1719
		// (get) Token: 0x06001ECA RID: 7882 RVA: 0x00087374 File Offset: 0x00085574
		public WebSocketCloseStatus? CloseStatus { get; }

		/// <summary>Returns the optional description that describes why the close handshake has been initiated by the remote endpoint.</summary>
		/// <returns>Returns <see cref="T:System.String" />.</returns>
		// Token: 0x170006B8 RID: 1720
		// (get) Token: 0x06001ECB RID: 7883 RVA: 0x0008737C File Offset: 0x0008557C
		public string CloseStatusDescription { get; }
	}
}
