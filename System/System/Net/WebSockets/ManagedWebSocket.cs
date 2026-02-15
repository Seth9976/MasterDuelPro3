using System;
using System.Buffers;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net.WebSockets
{
	// Token: 0x020004C6 RID: 1222
	internal sealed class ManagedWebSocket : WebSocket
	{
		// Token: 0x06001E22 RID: 7714 RVA: 0x00082C0D File Offset: 0x00080E0D
		public static ManagedWebSocket CreateFromConnectedStream(Stream stream, bool isServer, string subprotocol, TimeSpan keepAliveInterval)
		{
			return new ManagedWebSocket(stream, isServer, subprotocol, keepAliveInterval);
		}

		// Token: 0x170006A1 RID: 1697
		// (get) Token: 0x06001E23 RID: 7715 RVA: 0x00082C18 File Offset: 0x00080E18
		private object StateUpdateLock
		{
			get
			{
				return this._abortSource;
			}
		}

		// Token: 0x170006A2 RID: 1698
		// (get) Token: 0x06001E24 RID: 7716 RVA: 0x00082C20 File Offset: 0x00080E20
		private object ReceiveAsyncLock
		{
			get
			{
				return this._utf8TextState;
			}
		}

		// Token: 0x06001E25 RID: 7717 RVA: 0x00082C28 File Offset: 0x00080E28
		private ManagedWebSocket(Stream stream, bool isServer, string subprotocol, TimeSpan keepAliveInterval)
		{
			this._stream = stream;
			this._isServer = isServer;
			this._subprotocol = subprotocol;
			this._receiveBuffer = new byte[125];
			this._abortSource.Token.Register(delegate(object s)
			{
				ManagedWebSocket managedWebSocket = (ManagedWebSocket)s;
				object stateUpdateLock = managedWebSocket.StateUpdateLock;
				lock (stateUpdateLock)
				{
					WebSocketState state = managedWebSocket._state;
					if (state != WebSocketState.Closed && state != WebSocketState.Aborted)
					{
						managedWebSocket._state = ((state != WebSocketState.None && state != WebSocketState.Connecting) ? WebSocketState.Aborted : WebSocketState.Closed);
					}
				}
			}, this);
			if (keepAliveInterval > TimeSpan.Zero)
			{
				this._keepAliveTimer = new Timer(delegate(object s)
				{
					((ManagedWebSocket)s).SendKeepAliveFrameAsync();
				}, this, keepAliveInterval, keepAliveInterval);
			}
		}

		// Token: 0x06001E26 RID: 7718 RVA: 0x00082D28 File Offset: 0x00080F28
		public override void Dispose()
		{
			object stateUpdateLock = this.StateUpdateLock;
			lock (stateUpdateLock)
			{
				this.DisposeCore();
			}
		}

		// Token: 0x06001E27 RID: 7719 RVA: 0x00082D68 File Offset: 0x00080F68
		private void DisposeCore()
		{
			if (!this._disposed)
			{
				this._disposed = true;
				Timer keepAliveTimer = this._keepAliveTimer;
				if (keepAliveTimer != null)
				{
					keepAliveTimer.Dispose();
				}
				Stream stream = this._stream;
				if (stream != null)
				{
					stream.Dispose();
				}
				if (this._state < WebSocketState.Aborted)
				{
					this._state = WebSocketState.Closed;
				}
			}
		}

		// Token: 0x170006A3 RID: 1699
		// (get) Token: 0x06001E28 RID: 7720 RVA: 0x00082DB6 File Offset: 0x00080FB6
		public override WebSocketState State
		{
			get
			{
				return this._state;
			}
		}

		// Token: 0x06001E29 RID: 7721 RVA: 0x00082DC0 File Offset: 0x00080FC0
		public override Task SendAsync(ArraySegment<byte> buffer, WebSocketMessageType messageType, bool endOfMessage, CancellationToken cancellationToken)
		{
			if (messageType != WebSocketMessageType.Text && messageType != WebSocketMessageType.Binary)
			{
				throw new ArgumentException(SR.Format("The message type '{0}' is not allowed for the '{1}' operation. Valid message types are: '{2}, {3}'. To close the WebSocket, use the '{4}' operation instead. ", new object[] { "Close", "SendAsync", "Binary", "Text", "CloseOutputAsync" }), "messageType");
			}
			WebSocketValidate.ValidateArraySegment(buffer, "buffer");
			return this.SendPrivateAsync(buffer, messageType, endOfMessage, cancellationToken).AsTask();
		}

		// Token: 0x06001E2A RID: 7722 RVA: 0x00082E3C File Offset: 0x0008103C
		private ValueTask SendPrivateAsync(ReadOnlyMemory<byte> buffer, WebSocketMessageType messageType, bool endOfMessage, CancellationToken cancellationToken)
		{
			if (messageType != WebSocketMessageType.Text && messageType != WebSocketMessageType.Binary)
			{
				throw new ArgumentException(SR.Format("The message type '{0}' is not allowed for the '{1}' operation. Valid message types are: '{2}, {3}'. To close the WebSocket, use the '{4}' operation instead. ", new object[] { "Close", "SendAsync", "Binary", "Text", "CloseOutputAsync" }), "messageType");
			}
			try
			{
				WebSocketValidate.ThrowIfInvalidState(this._state, this._disposed, ManagedWebSocket.s_validSendStates);
			}
			catch (Exception ex)
			{
				return new ValueTask(Task.FromException(ex));
			}
			ManagedWebSocket.MessageOpcode messageOpcode = (this._lastSendWasFragment ? ManagedWebSocket.MessageOpcode.Continuation : ((messageType == WebSocketMessageType.Binary) ? ManagedWebSocket.MessageOpcode.Binary : ManagedWebSocket.MessageOpcode.Text));
			ValueTask valueTask = this.SendFrameAsync(messageOpcode, endOfMessage, buffer, cancellationToken);
			this._lastSendWasFragment = !endOfMessage;
			return valueTask;
		}

		// Token: 0x06001E2B RID: 7723 RVA: 0x00082EF4 File Offset: 0x000810F4
		public override Task<WebSocketReceiveResult> ReceiveAsync(ArraySegment<byte> buffer, CancellationToken cancellationToken)
		{
			WebSocketValidate.ValidateArraySegment(buffer, "buffer");
			Task<WebSocketReceiveResult> task2;
			try
			{
				WebSocketValidate.ThrowIfInvalidState(this._state, this._disposed, ManagedWebSocket.s_validReceiveStates);
				object receiveAsyncLock = this.ReceiveAsyncLock;
				lock (receiveAsyncLock)
				{
					this.ThrowIfOperationInProgress(this._lastReceiveAsync.IsCompleted, "ReceiveAsync");
					Task<WebSocketReceiveResult> task = this.ReceiveAsyncPrivate<ManagedWebSocket.WebSocketReceiveResultGetter, WebSocketReceiveResult>(buffer, cancellationToken, default(ManagedWebSocket.WebSocketReceiveResultGetter)).AsTask();
					this._lastReceiveAsync = task;
					task2 = task;
				}
			}
			catch (Exception ex)
			{
				task2 = Task.FromException<WebSocketReceiveResult>(ex);
			}
			return task2;
		}

		// Token: 0x06001E2C RID: 7724 RVA: 0x00082FA8 File Offset: 0x000811A8
		public override Task CloseOutputAsync(WebSocketCloseStatus closeStatus, string statusDescription, CancellationToken cancellationToken)
		{
			WebSocketValidate.ValidateCloseStatus(closeStatus, statusDescription);
			try
			{
				WebSocketValidate.ThrowIfInvalidState(this._state, this._disposed, ManagedWebSocket.s_validCloseOutputStates);
			}
			catch (Exception ex)
			{
				return Task.FromException(ex);
			}
			return this.SendCloseFrameAsync(closeStatus, statusDescription, cancellationToken);
		}

		// Token: 0x06001E2D RID: 7725 RVA: 0x00082FF8 File Offset: 0x000811F8
		public override void Abort()
		{
			this._abortSource.Cancel();
			this.Dispose();
		}

		// Token: 0x06001E2E RID: 7726 RVA: 0x0008300B File Offset: 0x0008120B
		private ValueTask SendFrameAsync(ManagedWebSocket.MessageOpcode opcode, bool endOfMessage, ReadOnlyMemory<byte> payloadBuffer, CancellationToken cancellationToken)
		{
			if (!cancellationToken.CanBeCanceled && this._sendFrameAsyncLock.Wait(0))
			{
				return this.SendFrameLockAcquiredNonCancelableAsync(opcode, endOfMessage, payloadBuffer);
			}
			return new ValueTask(this.SendFrameFallbackAsync(opcode, endOfMessage, payloadBuffer, cancellationToken));
		}

		// Token: 0x06001E2F RID: 7727 RVA: 0x00083040 File Offset: 0x00081240
		private ValueTask SendFrameLockAcquiredNonCancelableAsync(ManagedWebSocket.MessageOpcode opcode, bool endOfMessage, ReadOnlyMemory<byte> payloadBuffer)
		{
			ValueTask valueTask = default(ValueTask);
			bool flag = true;
			try
			{
				int num = this.WriteFrameToSendBuffer(opcode, endOfMessage, payloadBuffer.Span);
				valueTask = this._stream.WriteAsync(new ReadOnlyMemory<byte>(this._sendBuffer, 0, num), default(CancellationToken));
				if (valueTask.IsCompleted)
				{
					return valueTask;
				}
				flag = false;
			}
			catch (Exception ex)
			{
				return new ValueTask(Task.FromException((ex is OperationCanceledException) ? ex : ((this._state == WebSocketState.Aborted) ? ManagedWebSocket.CreateOperationCanceledException(ex, default(CancellationToken)) : new WebSocketException(WebSocketError.ConnectionClosedPrematurely, ex))));
			}
			finally
			{
				if (flag)
				{
					this.ReleaseSendBuffer();
					this._sendFrameAsyncLock.Release();
				}
			}
			return new ValueTask(this.WaitForWriteTaskAsync(valueTask));
		}

		// Token: 0x06001E30 RID: 7728 RVA: 0x0008311C File Offset: 0x0008131C
		private async Task WaitForWriteTaskAsync(ValueTask writeTask)
		{
			try
			{
				await writeTask.ConfigureAwait(false);
			}
			catch (Exception ex) when (!(ex is OperationCanceledException))
			{
				throw (this._state == WebSocketState.Aborted) ? ManagedWebSocket.CreateOperationCanceledException(ex, default(CancellationToken)) : new WebSocketException(WebSocketError.ConnectionClosedPrematurely, ex);
			}
			finally
			{
				this.ReleaseSendBuffer();
				this._sendFrameAsyncLock.Release();
			}
		}

		// Token: 0x06001E31 RID: 7729 RVA: 0x00083168 File Offset: 0x00081368
		private async Task SendFrameFallbackAsync(ManagedWebSocket.MessageOpcode opcode, bool endOfMessage, ReadOnlyMemory<byte> payloadBuffer, CancellationToken cancellationToken)
		{
			await this._sendFrameAsyncLock.WaitAsync().ConfigureAwait(false);
			try
			{
				int num = this.WriteFrameToSendBuffer(opcode, endOfMessage, payloadBuffer.Span);
				using (cancellationToken.Register(delegate(object s)
				{
					((ManagedWebSocket)s).Abort();
				}, this))
				{
					await this._stream.WriteAsync(new ReadOnlyMemory<byte>(this._sendBuffer, 0, num), cancellationToken).ConfigureAwait(false);
				}
				CancellationTokenRegistration cancellationTokenRegistration = default(CancellationTokenRegistration);
			}
			catch (Exception ex) when (!(ex is OperationCanceledException))
			{
				throw (this._state == WebSocketState.Aborted) ? ManagedWebSocket.CreateOperationCanceledException(ex, cancellationToken) : new WebSocketException(WebSocketError.ConnectionClosedPrematurely, ex);
			}
			finally
			{
				this.ReleaseSendBuffer();
				this._sendFrameAsyncLock.Release();
			}
		}

		// Token: 0x06001E32 RID: 7730 RVA: 0x000831CC File Offset: 0x000813CC
		private int WriteFrameToSendBuffer(ManagedWebSocket.MessageOpcode opcode, bool endOfMessage, ReadOnlySpan<byte> payloadBuffer)
		{
			this.AllocateSendBuffer(payloadBuffer.Length + 14);
			int? num = null;
			int num2;
			if (this._isServer)
			{
				num2 = ManagedWebSocket.WriteHeader(opcode, this._sendBuffer, payloadBuffer, endOfMessage, false);
			}
			else
			{
				num = new int?(ManagedWebSocket.WriteHeader(opcode, this._sendBuffer, payloadBuffer, endOfMessage, true));
				num2 = num.GetValueOrDefault() + 4;
			}
			if (payloadBuffer.Length > 0)
			{
				payloadBuffer.CopyTo(new Span<byte>(this._sendBuffer, num2, payloadBuffer.Length));
				if (num != null)
				{
					ManagedWebSocket.ApplyMask(new Span<byte>(this._sendBuffer, num2, payloadBuffer.Length), this._sendBuffer, num.Value, 0);
				}
			}
			return num2 + payloadBuffer.Length;
		}

		// Token: 0x06001E33 RID: 7731 RVA: 0x00083288 File Offset: 0x00081488
		private void SendKeepAliveFrameAsync()
		{
			if (this._sendFrameAsyncLock.Wait(0))
			{
				ValueTask valueTask = this.SendFrameLockAcquiredNonCancelableAsync(ManagedWebSocket.MessageOpcode.Ping, true, Memory<byte>.Empty);
				if (valueTask.IsCompletedSuccessfully)
				{
					valueTask.GetAwaiter().GetResult();
					return;
				}
				valueTask.AsTask().ContinueWith(delegate(Task p)
				{
					AggregateException exception = p.Exception;
				}, CancellationToken.None, TaskContinuationOptions.NotOnRanToCompletion | TaskContinuationOptions.NotOnCanceled | TaskContinuationOptions.ExecuteSynchronously, TaskScheduler.Default);
			}
		}

		// Token: 0x06001E34 RID: 7732 RVA: 0x0008330C File Offset: 0x0008150C
		private static int WriteHeader(ManagedWebSocket.MessageOpcode opcode, byte[] sendBuffer, ReadOnlySpan<byte> payload, bool endOfMessage, bool useMask)
		{
			sendBuffer[0] = (byte)opcode;
			if (endOfMessage)
			{
				int num = 0;
				sendBuffer[num] |= 128;
			}
			int num2;
			if (payload.Length <= 125)
			{
				sendBuffer[1] = (byte)payload.Length;
				num2 = 2;
			}
			else if (payload.Length <= 65535)
			{
				sendBuffer[1] = 126;
				sendBuffer[2] = (byte)(payload.Length / 256);
				sendBuffer[3] = (byte)payload.Length;
				num2 = 4;
			}
			else
			{
				sendBuffer[1] = 127;
				int num3 = payload.Length;
				for (int i = 9; i >= 2; i--)
				{
					sendBuffer[i] = (byte)num3;
					num3 /= 256;
				}
				num2 = 10;
			}
			if (useMask)
			{
				int num4 = 1;
				sendBuffer[num4] |= 128;
				ManagedWebSocket.WriteRandomMask(sendBuffer, num2);
			}
			return num2;
		}

		// Token: 0x06001E35 RID: 7733 RVA: 0x000833C5 File Offset: 0x000815C5
		private static void WriteRandomMask(byte[] buffer, int offset)
		{
			ManagedWebSocket.s_random.GetBytes(buffer, offset, 4);
		}

		// Token: 0x06001E36 RID: 7734 RVA: 0x000833D4 File Offset: 0x000815D4
		private ValueTask<TWebSocketReceiveResult> ReceiveAsyncPrivate<TWebSocketReceiveResultGetter, TWebSocketReceiveResult>(Memory<byte> payloadBuffer, CancellationToken cancellationToken, TWebSocketReceiveResultGetter resultGetter = default(TWebSocketReceiveResultGetter)) where TWebSocketReceiveResultGetter : struct, ManagedWebSocket.IWebSocketReceiveResultGetter<TWebSocketReceiveResult>
		{
			ManagedWebSocket.<ReceiveAsyncPrivate>d__61<TWebSocketReceiveResultGetter, TWebSocketReceiveResult> <ReceiveAsyncPrivate>d__;
			<ReceiveAsyncPrivate>d__.<>4__this = this;
			<ReceiveAsyncPrivate>d__.payloadBuffer = payloadBuffer;
			<ReceiveAsyncPrivate>d__.cancellationToken = cancellationToken;
			<ReceiveAsyncPrivate>d__.resultGetter = resultGetter;
			<ReceiveAsyncPrivate>d__.<>t__builder = AsyncValueTaskMethodBuilder<TWebSocketReceiveResult>.Create();
			<ReceiveAsyncPrivate>d__.<>1__state = -1;
			<ReceiveAsyncPrivate>d__.<>t__builder.Start<ManagedWebSocket.<ReceiveAsyncPrivate>d__61<TWebSocketReceiveResultGetter, TWebSocketReceiveResult>>(ref <ReceiveAsyncPrivate>d__);
			return <ReceiveAsyncPrivate>d__.<>t__builder.Task;
		}

		// Token: 0x06001E37 RID: 7735 RVA: 0x00083430 File Offset: 0x00081630
		private unsafe async Task HandleReceivedCloseAsync(ManagedWebSocket.MessageHeader header, CancellationToken cancellationToken)
		{
			object stateUpdateLock = this.StateUpdateLock;
			lock (stateUpdateLock)
			{
				this._receivedCloseFrame = true;
				if (this._state < WebSocketState.CloseReceived)
				{
					this._state = WebSocketState.CloseReceived;
				}
			}
			WebSocketCloseStatus closeStatus = WebSocketCloseStatus.NormalClosure;
			string closeStatusDescription = string.Empty;
			if (header.PayloadLength == 1L)
			{
				await this.CloseWithReceiveErrorAndThrowAsync(WebSocketCloseStatus.ProtocolError, WebSocketError.Faulted, null).ConfigureAwait(false);
			}
			else if (header.PayloadLength >= 2L)
			{
				if ((long)this._receiveBufferCount < header.PayloadLength)
				{
					await this.EnsureBufferContainsAsync((int)header.PayloadLength, cancellationToken, true).ConfigureAwait(false);
				}
				if (this._isServer)
				{
					ManagedWebSocket.ApplyMask(this._receiveBuffer.Span.Slice(this._receiveBufferOffset, (int)header.PayloadLength), header.Mask, 0);
				}
				closeStatus = (WebSocketCloseStatus)(((int)(*this._receiveBuffer.Span[this._receiveBufferOffset]) << 8) | (int)(*this._receiveBuffer.Span[this._receiveBufferOffset + 1]));
				if (!ManagedWebSocket.IsValidCloseStatus(closeStatus))
				{
					await this.CloseWithReceiveErrorAndThrowAsync(WebSocketCloseStatus.ProtocolError, WebSocketError.Faulted, null).ConfigureAwait(false);
				}
				if (header.PayloadLength > 2L)
				{
					int num = 0;
					try
					{
						closeStatusDescription = ManagedWebSocket.s_textEncoding.GetString(this._receiveBuffer.Span.Slice(this._receiveBufferOffset + 2, (int)header.PayloadLength - 2));
					}
					catch (DecoderFallbackException stateUpdateLock)
					{
						num = 1;
					}
					if (num == 1)
					{
						await this.CloseWithReceiveErrorAndThrowAsync(WebSocketCloseStatus.ProtocolError, WebSocketError.Faulted, (DecoderFallbackException)stateUpdateLock).ConfigureAwait(false);
					}
				}
				this.ConsumeFromBuffer((int)header.PayloadLength);
			}
			this._closeStatus = new WebSocketCloseStatus?(closeStatus);
			this._closeStatusDescription = closeStatusDescription;
			if (!this._isServer && this._sentCloseFrame)
			{
				await this.WaitForServerToCloseConnectionAsync(cancellationToken).ConfigureAwait(false);
			}
		}

		// Token: 0x06001E38 RID: 7736 RVA: 0x00083484 File Offset: 0x00081684
		private async Task WaitForServerToCloseConnectionAsync(CancellationToken cancellationToken)
		{
			ValueTask<int> valueTask = this._stream.ReadAsync(this._receiveBuffer, cancellationToken);
			if (!valueTask.IsCompletedSuccessfully)
			{
				using (CancellationTokenSource finalCts = new CancellationTokenSource(1000))
				{
					using (finalCts.Token.Register(delegate(object s)
					{
						((ManagedWebSocket)s).Abort();
					}, this))
					{
						try
						{
							await valueTask.ConfigureAwait(false);
						}
						catch
						{
						}
					}
					CancellationTokenRegistration cancellationTokenRegistration = default(CancellationTokenRegistration);
				}
				CancellationTokenSource finalCts = null;
			}
		}

		// Token: 0x06001E39 RID: 7737 RVA: 0x000834D0 File Offset: 0x000816D0
		private async Task HandleReceivedPingPongAsync(ManagedWebSocket.MessageHeader header, CancellationToken cancellationToken)
		{
			if (header.PayloadLength > 0L && (long)this._receiveBufferCount < header.PayloadLength)
			{
				await this.EnsureBufferContainsAsync((int)header.PayloadLength, cancellationToken, true).ConfigureAwait(false);
			}
			if (header.Opcode == ManagedWebSocket.MessageOpcode.Ping)
			{
				if (this._isServer)
				{
					ManagedWebSocket.ApplyMask(this._receiveBuffer.Span.Slice(this._receiveBufferOffset, (int)header.PayloadLength), header.Mask, 0);
				}
				await this.SendFrameAsync(ManagedWebSocket.MessageOpcode.Pong, true, this._receiveBuffer.Slice(this._receiveBufferOffset, (int)header.PayloadLength), default(CancellationToken)).ConfigureAwait(false);
			}
			if (header.PayloadLength > 0L)
			{
				this.ConsumeFromBuffer((int)header.PayloadLength);
			}
		}

		// Token: 0x06001E3A RID: 7738 RVA: 0x00083523 File Offset: 0x00081723
		private static bool IsValidCloseStatus(WebSocketCloseStatus closeStatus)
		{
			return closeStatus >= WebSocketCloseStatus.NormalClosure && closeStatus < (WebSocketCloseStatus)5000 && (closeStatus >= (WebSocketCloseStatus)3000 || (closeStatus - WebSocketCloseStatus.NormalClosure <= 3 || closeStatus - WebSocketCloseStatus.InvalidPayloadData <= 4));
		}

		// Token: 0x06001E3B RID: 7739 RVA: 0x00083558 File Offset: 0x00081758
		private async Task CloseWithReceiveErrorAndThrowAsync(WebSocketCloseStatus closeStatus, WebSocketError error, Exception innerException = null)
		{
			if (!this._sentCloseFrame)
			{
				await this.CloseOutputAsync(closeStatus, string.Empty, default(CancellationToken)).ConfigureAwait(false);
			}
			this._receiveBufferCount = 0;
			throw new WebSocketException(error, innerException);
		}

		// Token: 0x06001E3C RID: 7740 RVA: 0x000835B4 File Offset: 0x000817B4
		private unsafe bool TryParseMessageHeaderFromReceiveBuffer(out ManagedWebSocket.MessageHeader resultHeader)
		{
			ManagedWebSocket.MessageHeader messageHeader = default(ManagedWebSocket.MessageHeader);
			Span<byte> span = this._receiveBuffer.Span;
			messageHeader.Fin = (*span[this._receiveBufferOffset] & 128) > 0;
			bool flag = (*span[this._receiveBufferOffset] & 112) > 0;
			messageHeader.Opcode = (ManagedWebSocket.MessageOpcode)(*span[this._receiveBufferOffset] & 15);
			bool flag2 = (*span[this._receiveBufferOffset + 1] & 128) > 0;
			messageHeader.PayloadLength = (long)(*span[this._receiveBufferOffset + 1] & 127);
			this.ConsumeFromBuffer(2);
			if (messageHeader.PayloadLength == 126L)
			{
				messageHeader.PayloadLength = (long)(((int)(*span[this._receiveBufferOffset]) << 8) | (int)(*span[this._receiveBufferOffset + 1]));
				this.ConsumeFromBuffer(2);
			}
			else if (messageHeader.PayloadLength == 127L)
			{
				messageHeader.PayloadLength = 0L;
				for (int i = 0; i < 8; i++)
				{
					messageHeader.PayloadLength = (messageHeader.PayloadLength << 8) | (long)((ulong)(*span[this._receiveBufferOffset + i]));
				}
				this.ConsumeFromBuffer(8);
			}
			bool flag3 = flag;
			if (flag2)
			{
				if (!this._isServer)
				{
					flag3 = true;
				}
				messageHeader.Mask = ManagedWebSocket.CombineMaskBytes(span, this._receiveBufferOffset);
				this.ConsumeFromBuffer(4);
			}
			switch (messageHeader.Opcode)
			{
			case ManagedWebSocket.MessageOpcode.Continuation:
				if (this._lastReceiveHeader.Fin)
				{
					flag3 = true;
					goto IL_01CD;
				}
				goto IL_01CD;
			case ManagedWebSocket.MessageOpcode.Text:
			case ManagedWebSocket.MessageOpcode.Binary:
				if (!this._lastReceiveHeader.Fin)
				{
					flag3 = true;
					goto IL_01CD;
				}
				goto IL_01CD;
			case ManagedWebSocket.MessageOpcode.Close:
			case ManagedWebSocket.MessageOpcode.Ping:
			case ManagedWebSocket.MessageOpcode.Pong:
				if (messageHeader.PayloadLength > 125L || !messageHeader.Fin)
				{
					flag3 = true;
					goto IL_01CD;
				}
				goto IL_01CD;
			}
			flag3 = true;
			IL_01CD:
			resultHeader = messageHeader;
			return !flag3;
		}

		// Token: 0x06001E3D RID: 7741 RVA: 0x0008379C File Offset: 0x0008199C
		private async Task SendCloseFrameAsync(WebSocketCloseStatus closeStatus, string closeStatusDescription, CancellationToken cancellationToken)
		{
			byte[] buffer = null;
			try
			{
				int num = 2;
				if (string.IsNullOrEmpty(closeStatusDescription))
				{
					buffer = ArrayPool<byte>.Shared.Rent(num);
				}
				else
				{
					num += ManagedWebSocket.s_textEncoding.GetByteCount(closeStatusDescription);
					buffer = ArrayPool<byte>.Shared.Rent(num);
					ManagedWebSocket.s_textEncoding.GetBytes(closeStatusDescription, 0, closeStatusDescription.Length, buffer, 2);
				}
				ushort num2 = (ushort)closeStatus;
				buffer[0] = (byte)(num2 >> 8);
				buffer[1] = (byte)(num2 & 255);
				await this.SendFrameAsync(ManagedWebSocket.MessageOpcode.Close, true, new Memory<byte>(buffer, 0, num), cancellationToken).ConfigureAwait(false);
			}
			finally
			{
				if (buffer != null)
				{
					ArrayPool<byte>.Shared.Return(buffer, false);
				}
			}
			object stateUpdateLock = this.StateUpdateLock;
			lock (stateUpdateLock)
			{
				this._sentCloseFrame = true;
				if (this._state <= WebSocketState.CloseReceived)
				{
					this._state = WebSocketState.CloseSent;
				}
			}
			if (!this._isServer && this._receivedCloseFrame)
			{
				await this.WaitForServerToCloseConnectionAsync(cancellationToken).ConfigureAwait(false);
			}
		}

		// Token: 0x06001E3E RID: 7742 RVA: 0x000837F7 File Offset: 0x000819F7
		private void ConsumeFromBuffer(int count)
		{
			this._receiveBufferCount -= count;
			this._receiveBufferOffset += count;
		}

		// Token: 0x06001E3F RID: 7743 RVA: 0x00083818 File Offset: 0x00081A18
		private async Task EnsureBufferContainsAsync(int minimumRequiredBytes, CancellationToken cancellationToken, bool throwOnPrematureClosure = true)
		{
			if (this._receiveBufferCount < minimumRequiredBytes)
			{
				if (this._receiveBufferCount > 0)
				{
					this._receiveBuffer.Span.Slice(this._receiveBufferOffset, this._receiveBufferCount).CopyTo(this._receiveBuffer.Span);
				}
				this._receiveBufferOffset = 0;
				while (this._receiveBufferCount < minimumRequiredBytes)
				{
					int num = await this._stream.ReadAsync(this._receiveBuffer.Slice(this._receiveBufferCount, this._receiveBuffer.Length - this._receiveBufferCount), cancellationToken).ConfigureAwait(false);
					if (num <= 0)
					{
						this.ThrowIfEOFUnexpected(throwOnPrematureClosure);
						break;
					}
					this._receiveBufferCount += num;
				}
			}
		}

		// Token: 0x06001E40 RID: 7744 RVA: 0x00083873 File Offset: 0x00081A73
		private void ThrowIfEOFUnexpected(bool throwOnPrematureClosure)
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException("WebSocket");
			}
			if (throwOnPrematureClosure)
			{
				throw new WebSocketException(WebSocketError.ConnectionClosedPrematurely);
			}
		}

		// Token: 0x06001E41 RID: 7745 RVA: 0x00083892 File Offset: 0x00081A92
		private void AllocateSendBuffer(int minLength)
		{
			this._sendBuffer = ArrayPool<byte>.Shared.Rent(minLength);
		}

		// Token: 0x06001E42 RID: 7746 RVA: 0x000838A8 File Offset: 0x00081AA8
		private void ReleaseSendBuffer()
		{
			byte[] sendBuffer = this._sendBuffer;
			if (sendBuffer != null)
			{
				this._sendBuffer = null;
				ArrayPool<byte>.Shared.Return(sendBuffer, false);
			}
		}

		// Token: 0x06001E43 RID: 7747 RVA: 0x000838D2 File Offset: 0x00081AD2
		private static int CombineMaskBytes(Span<byte> buffer, int maskOffset)
		{
			return BitConverter.ToInt32(buffer.Slice(maskOffset));
		}

		// Token: 0x06001E44 RID: 7748 RVA: 0x000838E6 File Offset: 0x00081AE6
		private static int ApplyMask(Span<byte> toMask, byte[] mask, int maskOffset, int maskOffsetIndex)
		{
			return ManagedWebSocket.ApplyMask(toMask, ManagedWebSocket.CombineMaskBytes(mask, maskOffset), maskOffsetIndex);
		}

		// Token: 0x06001E45 RID: 7749 RVA: 0x000838FC File Offset: 0x00081AFC
		private unsafe static int ApplyMask(Span<byte> toMask, int mask, int maskIndex)
		{
			int num = maskIndex * 8;
			int num2 = (int)(((uint)mask >> num) | (uint)((uint)mask << 32 - num));
			int i = toMask.Length;
			if (i > 0)
			{
				fixed (byte* reference = MemoryMarshal.GetReference<byte>(toMask))
				{
					byte* ptr = reference;
					if (ptr % 4L == null)
					{
						while (i >= 4)
						{
							i -= 4;
							*(int*)ptr ^= num2;
							ptr += 4;
						}
					}
					if (i > 0)
					{
						byte* ptr2 = (byte*)(&mask);
						byte* ptr3 = ptr + i;
						while (ptr < ptr3)
						{
							byte* ptr4 = ptr++;
							*ptr4 ^= ptr2[maskIndex];
							maskIndex = (maskIndex + 1) & 3;
						}
					}
				}
			}
			return maskIndex;
		}

		// Token: 0x06001E46 RID: 7750 RVA: 0x00083987 File Offset: 0x00081B87
		private void ThrowIfOperationInProgress(bool operationCompleted, [CallerMemberName] string methodName = null)
		{
			if (!operationCompleted)
			{
				this.Abort();
				this.ThrowOperationInProgress(methodName);
			}
		}

		// Token: 0x06001E47 RID: 7751 RVA: 0x00083999 File Offset: 0x00081B99
		private void ThrowOperationInProgress(string methodName)
		{
			throw new InvalidOperationException(SR.Format("There is already one outstanding '{0}' call for this WebSocket instance. ReceiveAsync and SendAsync can be called simultaneously, but at most one outstanding operation for each of them is allowed at the same time.", methodName));
		}

		// Token: 0x06001E48 RID: 7752 RVA: 0x000839AB File Offset: 0x00081BAB
		private static Exception CreateOperationCanceledException(Exception innerException, CancellationToken cancellationToken = default(CancellationToken))
		{
			return new OperationCanceledException(new OperationCanceledException().Message, innerException, cancellationToken);
		}

		// Token: 0x06001E49 RID: 7753 RVA: 0x000839C0 File Offset: 0x00081BC0
		private unsafe static bool TryValidateUtf8(Span<byte> span, bool endOfMessage, ManagedWebSocket.Utf8MessageState state)
		{
			int i = 0;
			while (i < span.Length)
			{
				if (!state.SequenceInProgress)
				{
					state.SequenceInProgress = true;
					byte b = *span[i];
					i++;
					if ((b & 128) == 0)
					{
						state.AdditionalBytesExpected = 0;
						state.CurrentDecodeBits = (int)(b & 127);
						state.ExpectedValueMin = 0;
					}
					else
					{
						if ((b & 192) == 128)
						{
							return false;
						}
						if ((b & 224) == 192)
						{
							state.AdditionalBytesExpected = 1;
							state.CurrentDecodeBits = (int)(b & 31);
							state.ExpectedValueMin = 128;
						}
						else if ((b & 240) == 224)
						{
							state.AdditionalBytesExpected = 2;
							state.CurrentDecodeBits = (int)(b & 15);
							state.ExpectedValueMin = 2048;
						}
						else
						{
							if ((b & 248) != 240)
							{
								return false;
							}
							state.AdditionalBytesExpected = 3;
							state.CurrentDecodeBits = (int)(b & 7);
							state.ExpectedValueMin = 65536;
						}
					}
				}
				while (state.AdditionalBytesExpected > 0 && i < span.Length)
				{
					byte b2 = *span[i];
					if ((b2 & 192) != 128)
					{
						return false;
					}
					i++;
					state.AdditionalBytesExpected--;
					state.CurrentDecodeBits = (state.CurrentDecodeBits << 6) | (int)(b2 & 63);
					if (state.AdditionalBytesExpected == 1 && state.CurrentDecodeBits >= 864 && state.CurrentDecodeBits <= 895)
					{
						return false;
					}
					if (state.AdditionalBytesExpected == 2 && state.CurrentDecodeBits >= 272)
					{
						return false;
					}
				}
				if (state.AdditionalBytesExpected == 0)
				{
					state.SequenceInProgress = false;
					if (state.CurrentDecodeBits < state.ExpectedValueMin)
					{
						return false;
					}
				}
			}
			return !endOfMessage || !state.SequenceInProgress;
		}

		// Token: 0x0400156B RID: 5483
		private static readonly RandomNumberGenerator s_random = RandomNumberGenerator.Create();

		// Token: 0x0400156C RID: 5484
		private static readonly UTF8Encoding s_textEncoding = new UTF8Encoding(false, true);

		// Token: 0x0400156D RID: 5485
		private static readonly WebSocketState[] s_validSendStates = new WebSocketState[]
		{
			WebSocketState.Open,
			WebSocketState.CloseReceived
		};

		// Token: 0x0400156E RID: 5486
		private static readonly WebSocketState[] s_validReceiveStates = new WebSocketState[]
		{
			WebSocketState.Open,
			WebSocketState.CloseSent
		};

		// Token: 0x0400156F RID: 5487
		private static readonly WebSocketState[] s_validCloseOutputStates = new WebSocketState[]
		{
			WebSocketState.Open,
			WebSocketState.CloseReceived
		};

		// Token: 0x04001570 RID: 5488
		private static readonly WebSocketState[] s_validCloseStates = new WebSocketState[]
		{
			WebSocketState.Open,
			WebSocketState.CloseReceived,
			WebSocketState.CloseSent
		};

		// Token: 0x04001571 RID: 5489
		private static readonly Task<WebSocketReceiveResult> s_cachedCloseTask = Task.FromResult<WebSocketReceiveResult>(new WebSocketReceiveResult(0, WebSocketMessageType.Close, true));

		// Token: 0x04001572 RID: 5490
		private readonly Stream _stream;

		// Token: 0x04001573 RID: 5491
		private readonly bool _isServer;

		// Token: 0x04001574 RID: 5492
		private readonly string _subprotocol;

		// Token: 0x04001575 RID: 5493
		private readonly Timer _keepAliveTimer;

		// Token: 0x04001576 RID: 5494
		private readonly CancellationTokenSource _abortSource = new CancellationTokenSource();

		// Token: 0x04001577 RID: 5495
		private Memory<byte> _receiveBuffer;

		// Token: 0x04001578 RID: 5496
		private readonly ManagedWebSocket.Utf8MessageState _utf8TextState = new ManagedWebSocket.Utf8MessageState();

		// Token: 0x04001579 RID: 5497
		private readonly SemaphoreSlim _sendFrameAsyncLock = new SemaphoreSlim(1, 1);

		// Token: 0x0400157A RID: 5498
		private WebSocketState _state = WebSocketState.Open;

		// Token: 0x0400157B RID: 5499
		private bool _disposed;

		// Token: 0x0400157C RID: 5500
		private bool _sentCloseFrame;

		// Token: 0x0400157D RID: 5501
		private bool _receivedCloseFrame;

		// Token: 0x0400157E RID: 5502
		private WebSocketCloseStatus? _closeStatus;

		// Token: 0x0400157F RID: 5503
		private string _closeStatusDescription;

		// Token: 0x04001580 RID: 5504
		private ManagedWebSocket.MessageHeader _lastReceiveHeader = new ManagedWebSocket.MessageHeader
		{
			Opcode = ManagedWebSocket.MessageOpcode.Text,
			Fin = true
		};

		// Token: 0x04001581 RID: 5505
		private int _receiveBufferOffset;

		// Token: 0x04001582 RID: 5506
		private int _receiveBufferCount;

		// Token: 0x04001583 RID: 5507
		private int _receivedMaskOffsetOffset;

		// Token: 0x04001584 RID: 5508
		private byte[] _sendBuffer;

		// Token: 0x04001585 RID: 5509
		private bool _lastSendWasFragment;

		// Token: 0x04001586 RID: 5510
		private Task _lastReceiveAsync = Task.CompletedTask;

		// Token: 0x020004C7 RID: 1223
		private sealed class Utf8MessageState
		{
			// Token: 0x04001587 RID: 5511
			internal bool SequenceInProgress;

			// Token: 0x04001588 RID: 5512
			internal int AdditionalBytesExpected;

			// Token: 0x04001589 RID: 5513
			internal int ExpectedValueMin;

			// Token: 0x0400158A RID: 5514
			internal int CurrentDecodeBits;
		}

		// Token: 0x020004C8 RID: 1224
		private enum MessageOpcode : byte
		{
			// Token: 0x0400158C RID: 5516
			Continuation,
			// Token: 0x0400158D RID: 5517
			Text,
			// Token: 0x0400158E RID: 5518
			Binary,
			// Token: 0x0400158F RID: 5519
			Close = 8,
			// Token: 0x04001590 RID: 5520
			Ping,
			// Token: 0x04001591 RID: 5521
			Pong
		}

		// Token: 0x020004C9 RID: 1225
		[StructLayout(LayoutKind.Auto)]
		private struct MessageHeader
		{
			// Token: 0x04001592 RID: 5522
			internal ManagedWebSocket.MessageOpcode Opcode;

			// Token: 0x04001593 RID: 5523
			internal bool Fin;

			// Token: 0x04001594 RID: 5524
			internal long PayloadLength;

			// Token: 0x04001595 RID: 5525
			internal int Mask;
		}

		// Token: 0x020004CA RID: 1226
		private interface IWebSocketReceiveResultGetter<TResult>
		{
			// Token: 0x06001E4C RID: 7756
			TResult GetResult(int count, WebSocketMessageType messageType, bool endOfMessage, WebSocketCloseStatus? closeStatus, string closeDescription);
		}

		// Token: 0x020004CB RID: 1227
		private readonly struct WebSocketReceiveResultGetter : ManagedWebSocket.IWebSocketReceiveResultGetter<WebSocketReceiveResult>
		{
			// Token: 0x06001E4D RID: 7757 RVA: 0x00083C08 File Offset: 0x00081E08
			public WebSocketReceiveResult GetResult(int count, WebSocketMessageType messageType, bool endOfMessage, WebSocketCloseStatus? closeStatus, string closeDescription)
			{
				return new WebSocketReceiveResult(count, messageType, endOfMessage, closeStatus, closeDescription);
			}
		}
	}
}
