using System;
using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net.WebSockets
{
	/// <summary>The WebSocket class allows applications to send and receive data after the WebSocket upgrade has completed.</summary>
	// Token: 0x020004E4 RID: 1252
	public abstract class WebSocket : IDisposable
	{
		/// <summary>Returns the current state of the WebSocket connection.</summary>
		/// <returns>Returns <see cref="T:System.Net.WebSockets.WebSocketState" />.</returns>
		// Token: 0x170006B2 RID: 1714
		// (get) Token: 0x06001EAF RID: 7855
		public abstract WebSocketState State { get; }

		/// <summary>Aborts the WebSocket connection and cancels any pending IO operations.</summary>
		// Token: 0x06001EB0 RID: 7856
		public abstract void Abort();

		/// <summary>Initiates or completes the close handshake defined in the WebSocket protocol specification section 7.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation. </returns>
		/// <param name="closeStatus">Indicates the reason for closing the WebSocket connection.</param>
		/// <param name="statusDescription">Allows applications to specify a human readable explanation as to why the connection is closed.</param>
		/// <param name="cancellationToken">The token that can be used to propagate notification that operations should be canceled.</param>
		// Token: 0x06001EB1 RID: 7857
		public abstract Task CloseOutputAsync(WebSocketCloseStatus closeStatus, string statusDescription, CancellationToken cancellationToken);

		/// <summary>Used to clean up unmanaged resources for ASP.NET and self-hosted implementations.</summary>
		// Token: 0x06001EB2 RID: 7858
		public abstract void Dispose();

		/// <summary>Receives data from the WebSocket connection asynchronously.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task`1" />.The task object representing the asynchronous operation. The <see cref="P:System.Threading.Tasks.Task`1.Result" /> property on the task object returns a <see cref="T:System.Byte" /> array containing the received data.</returns>
		/// <param name="buffer">References the application buffer that is the storage location for the received data.</param>
		/// <param name="cancellationToken">Propagate the notification that operations should be canceled.</param>
		// Token: 0x06001EB3 RID: 7859
		public abstract Task<WebSocketReceiveResult> ReceiveAsync(ArraySegment<byte> buffer, CancellationToken cancellationToken);

		/// <summary>Sends data over the WebSocket connection asynchronously.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation. </returns>
		/// <param name="buffer">The buffer to be sent over the connection.</param>
		/// <param name="messageType">Indicates whether the application is sending a binary or text message.</param>
		/// <param name="endOfMessage">Indicates whether the data in “buffer” is the last part of a message.</param>
		/// <param name="cancellationToken">The token that propagates the notification that operations should be canceled.</param>
		// Token: 0x06001EB4 RID: 7860
		public abstract Task SendAsync(ArraySegment<byte> buffer, WebSocketMessageType messageType, bool endOfMessage, CancellationToken cancellationToken);

		/// <summary>Gets the default WebSocket protocol keep-alive interval in milliseconds.</summary>
		/// <returns>Returns <see cref="T:System.TimeSpan" />.The default WebSocket protocol keep-alive interval in milliseconds. The typical value for this interval is 30 seconds.</returns>
		// Token: 0x170006B3 RID: 1715
		// (get) Token: 0x06001EB5 RID: 7861 RVA: 0x000870DA File Offset: 0x000852DA
		public static TimeSpan DefaultKeepAliveInterval
		{
			get
			{
				return TimeSpan.FromSeconds(30.0);
			}
		}

		/// <summary>This API supports the .NET Framework infrastructure and is not intended to be used directly from your code.Allows callers to create a client side WebSocket class which will use the WSPC for framing purposes.</summary>
		/// <returns>Returns <see cref="T:System.Net.WebSockets.WebSocket" />.</returns>
		/// <param name="innerStream">The connection to be used for IO operations.</param>
		/// <param name="subProtocol">The subprotocol accepted by the client.</param>
		/// <param name="receiveBufferSize">The size in bytes of the client WebSocket receive buffer.</param>
		/// <param name="sendBufferSize">The size in bytes of the client WebSocket send buffer.</param>
		/// <param name="keepAliveInterval">Determines how regularly a frame is sent over the connection as a keep-alive. Applies only when the connection is idle.</param>
		/// <param name="useZeroMaskingKey">Indicates whether a random key or a static key (just zeros) should be used for the WebSocket masking.</param>
		/// <param name="internalBuffer">Will be used as the internal buffer in the WPC. The size has to be at least 2 * ReceiveBufferSize + SendBufferSize + 256 + 20 (16 on 32-bit).</param>
		// Token: 0x06001EB6 RID: 7862 RVA: 0x000870EC File Offset: 0x000852EC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static WebSocket CreateClientWebSocket(Stream innerStream, string subProtocol, int receiveBufferSize, int sendBufferSize, TimeSpan keepAliveInterval, bool useZeroMaskingKey, ArraySegment<byte> internalBuffer)
		{
			if (innerStream == null)
			{
				throw new ArgumentNullException("innerStream");
			}
			if (!innerStream.CanRead || !innerStream.CanWrite)
			{
				throw new ArgumentException((!innerStream.CanRead) ? "The base stream is not readable." : "The base stream is not writeable.", "innerStream");
			}
			if (subProtocol != null)
			{
				WebSocketValidate.ValidateSubprotocol(subProtocol);
			}
			if (keepAliveInterval != Timeout.InfiniteTimeSpan && keepAliveInterval < TimeSpan.Zero)
			{
				throw new ArgumentOutOfRangeException("keepAliveInterval", keepAliveInterval, SR.Format("The argument must be a value greater than {0}.", 0));
			}
			if (receiveBufferSize <= 0 || sendBufferSize <= 0)
			{
				throw new ArgumentOutOfRangeException((receiveBufferSize <= 0) ? "receiveBufferSize" : "sendBufferSize", (receiveBufferSize <= 0) ? receiveBufferSize : sendBufferSize, SR.Format("The argument must be a value greater than {0}.", 0));
			}
			return ManagedWebSocket.CreateFromConnectedStream(innerStream, false, subProtocol, keepAliveInterval);
		}
	}
}
