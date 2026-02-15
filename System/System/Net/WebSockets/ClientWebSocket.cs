using System;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net.WebSockets
{
	/// <summary>Provides a client for connecting to WebSocket services.</summary>
	// Token: 0x020004D8 RID: 1240
	public sealed class ClientWebSocket : WebSocket
	{
		/// <summary>Creates an instance of the <see cref="T:System.Net.WebSockets.ClientWebSocket" /> class.</summary>
		// Token: 0x06001E6E RID: 7790 RVA: 0x00085938 File Offset: 0x00083B38
		public ClientWebSocket()
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Enter(this, null, ".ctor");
			}
			WebSocketHandle.CheckPlatformSupport();
			this._state = 0;
			this._options = new ClientWebSocketOptions
			{
				Proxy = ClientWebSocket.DefaultWebProxy.Instance
			};
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Exit(this, null, ".ctor");
			}
		}

		/// <summary>Gets the WebSocket options for the <see cref="T:System.Net.WebSockets.ClientWebSocket" /> instance.</summary>
		/// <returns>Returns <see cref="T:System.Net.WebSockets.ClientWebSocketOptions" />.The WebSocket options for the <see cref="T:System.Net.WebSockets.ClientWebSocket" /> instance.</returns>
		// Token: 0x170006A4 RID: 1700
		// (get) Token: 0x06001E6F RID: 7791 RVA: 0x00085993 File Offset: 0x00083B93
		public ClientWebSocketOptions Options
		{
			get
			{
				return this._options;
			}
		}

		/// <summary>Get the WebSocket state of the <see cref="T:System.Net.WebSockets.ClientWebSocket" /> instance.</summary>
		/// <returns>Returns <see cref="T:System.Net.WebSockets.WebSocketState" />.The WebSocket state of the <see cref="T:System.Net.WebSockets.ClientWebSocket" /> instance.</returns>
		// Token: 0x170006A5 RID: 1701
		// (get) Token: 0x06001E70 RID: 7792 RVA: 0x0008599C File Offset: 0x00083B9C
		public override WebSocketState State
		{
			get
			{
				if (WebSocketHandle.IsValid(this._innerWebSocket))
				{
					return this._innerWebSocket.State;
				}
				ClientWebSocket.InternalState state = (ClientWebSocket.InternalState)this._state;
				if (state == ClientWebSocket.InternalState.Created)
				{
					return WebSocketState.None;
				}
				if (state != ClientWebSocket.InternalState.Connecting)
				{
					return WebSocketState.Closed;
				}
				return WebSocketState.Connecting;
			}
		}

		/// <summary>Connect to a WebSocket server as an asynchronous operation.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
		/// <param name="uri">The URI of the WebSocket server to connect to.</param>
		/// <param name="cancellationToken">A cancellation token used to propagate notification that the  operation should be canceled.</param>
		// Token: 0x06001E71 RID: 7793 RVA: 0x000859D8 File Offset: 0x00083BD8
		public Task ConnectAsync(Uri uri, CancellationToken cancellationToken)
		{
			if (uri == null)
			{
				throw new ArgumentNullException("uri");
			}
			if (!uri.IsAbsoluteUri)
			{
				throw new ArgumentException("This operation is not supported for a relative URI.", "uri");
			}
			if (uri.Scheme != "ws" && uri.Scheme != "wss")
			{
				throw new ArgumentException("Only Uris starting with 'ws://' or 'wss://' are supported.", "uri");
			}
			ClientWebSocket.InternalState internalState = (ClientWebSocket.InternalState)Interlocked.CompareExchange(ref this._state, 1, 0);
			if (internalState == ClientWebSocket.InternalState.Disposed)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (internalState != ClientWebSocket.InternalState.Created)
			{
				throw new InvalidOperationException("The WebSocket has already been started.");
			}
			this._options.SetToReadOnly();
			return this.ConnectAsyncCore(uri, cancellationToken);
		}

		// Token: 0x06001E72 RID: 7794 RVA: 0x00085A8C File Offset: 0x00083C8C
		private async Task ConnectAsyncCore(Uri uri, CancellationToken cancellationToken)
		{
			this._innerWebSocket = WebSocketHandle.Create();
			try
			{
				if (Interlocked.CompareExchange(ref this._state, 2, 1) != 1)
				{
					throw new ObjectDisposedException(base.GetType().FullName);
				}
				await this._innerWebSocket.ConnectAsyncCore(uri, cancellationToken, this._options).ConfigureAwait(false);
			}
			catch (Exception ex)
			{
				if (NetEventSource.IsEnabled)
				{
					NetEventSource.Error(this, ex, "ConnectAsyncCore");
				}
				throw;
			}
		}

		/// <summary>Send data on <see cref="T:System.Net.WebSockets.ClientWebSocket" /> as an asynchronous operation.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
		/// <param name="buffer">The buffer containing the message to be sent.</param>
		/// <param name="messageType">Specifies whether the buffer is clear text or in a binary format.</param>
		/// <param name="endOfMessage">Specifies whether this is the final asynchronous send. Set to true if this is the final send; false otherwise.</param>
		/// <param name="cancellationToken">A cancellation token used to propagate notification that this  operation should be canceled.</param>
		// Token: 0x06001E73 RID: 7795 RVA: 0x00085ADF File Offset: 0x00083CDF
		public override Task SendAsync(ArraySegment<byte> buffer, WebSocketMessageType messageType, bool endOfMessage, CancellationToken cancellationToken)
		{
			this.ThrowIfNotConnected();
			return this._innerWebSocket.SendAsync(buffer, messageType, endOfMessage, cancellationToken);
		}

		/// <summary>Receive data on <see cref="T:System.Net.WebSockets.ClientWebSocket" /> as an asynchronous operation.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task`1" />.The task object representing the asynchronous operation.</returns>
		/// <param name="buffer">The buffer to receive the response.</param>
		/// <param name="cancellationToken">A cancellation token used to propagate notification that this  operation should be canceled.</param>
		// Token: 0x06001E74 RID: 7796 RVA: 0x00085AF7 File Offset: 0x00083CF7
		public override Task<WebSocketReceiveResult> ReceiveAsync(ArraySegment<byte> buffer, CancellationToken cancellationToken)
		{
			this.ThrowIfNotConnected();
			return this._innerWebSocket.ReceiveAsync(buffer, cancellationToken);
		}

		/// <summary>Close the output for the <see cref="T:System.Net.WebSockets.ClientWebSocket" /> instance as an asynchronous operation.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
		/// <param name="closeStatus">The WebSocket close status.</param>
		/// <param name="statusDescription">A description of the close status.</param>
		/// <param name="cancellationToken">A cancellation token used to propagate notification that this  operation should be canceled.</param>
		// Token: 0x06001E75 RID: 7797 RVA: 0x00085B0C File Offset: 0x00083D0C
		public override Task CloseOutputAsync(WebSocketCloseStatus closeStatus, string statusDescription, CancellationToken cancellationToken)
		{
			this.ThrowIfNotConnected();
			return this._innerWebSocket.CloseOutputAsync(closeStatus, statusDescription, cancellationToken);
		}

		/// <summary>Aborts the connection and cancels any pending IO operations.</summary>
		// Token: 0x06001E76 RID: 7798 RVA: 0x00085B22 File Offset: 0x00083D22
		public override void Abort()
		{
			if (this._state == 3)
			{
				return;
			}
			if (WebSocketHandle.IsValid(this._innerWebSocket))
			{
				this._innerWebSocket.Abort();
			}
			this.Dispose();
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Net.WebSockets.ClientWebSocket" /> instance.</summary>
		// Token: 0x06001E77 RID: 7799 RVA: 0x00085B4C File Offset: 0x00083D4C
		public override void Dispose()
		{
			if (Interlocked.Exchange(ref this._state, 3) == 3)
			{
				return;
			}
			if (WebSocketHandle.IsValid(this._innerWebSocket))
			{
				this._innerWebSocket.Dispose();
			}
		}

		// Token: 0x06001E78 RID: 7800 RVA: 0x00085B76 File Offset: 0x00083D76
		private void ThrowIfNotConnected()
		{
			if (this._state == 3)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (this._state != 2)
			{
				throw new InvalidOperationException("The WebSocket is not connected.");
			}
		}

		// Token: 0x040015E5 RID: 5605
		private readonly ClientWebSocketOptions _options;

		// Token: 0x040015E6 RID: 5606
		private WebSocketHandle _innerWebSocket;

		// Token: 0x040015E7 RID: 5607
		private int _state;

		// Token: 0x020004D9 RID: 1241
		private enum InternalState
		{
			// Token: 0x040015E9 RID: 5609
			Created,
			// Token: 0x040015EA RID: 5610
			Connecting,
			// Token: 0x040015EB RID: 5611
			Connected,
			// Token: 0x040015EC RID: 5612
			Disposed
		}

		// Token: 0x020004DA RID: 1242
		internal sealed class DefaultWebProxy : IWebProxy
		{
			// Token: 0x170006A6 RID: 1702
			// (get) Token: 0x06001E79 RID: 7801 RVA: 0x00085BA6 File Offset: 0x00083DA6
			public static ClientWebSocket.DefaultWebProxy Instance { get; } = new ClientWebSocket.DefaultWebProxy();

			// Token: 0x170006A7 RID: 1703
			// (get) Token: 0x06001E7A RID: 7802 RVA: 0x00003132 File Offset: 0x00001332
			public ICredentials Credentials
			{
				get
				{
					throw new NotSupportedException();
				}
			}

			// Token: 0x06001E7B RID: 7803 RVA: 0x00003132 File Offset: 0x00001332
			public Uri GetProxy(Uri destination)
			{
				throw new NotSupportedException();
			}

			// Token: 0x06001E7C RID: 7804 RVA: 0x00003132 File Offset: 0x00001332
			public bool IsBypassed(Uri host)
			{
				throw new NotSupportedException();
			}
		}
	}
}
