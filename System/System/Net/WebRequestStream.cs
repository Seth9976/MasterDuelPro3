using System;
using System.IO;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net
{
	// Token: 0x02000447 RID: 1095
	internal class WebRequestStream : WebConnectionStream
	{
		// Token: 0x06001BD7 RID: 7127 RVA: 0x00079190 File Offset: 0x00077390
		public WebRequestStream(WebConnection connection, WebOperation operation, Stream stream, WebConnectionTunnel tunnel)
			: base(connection, operation)
		{
			this.InnerStream = stream;
			this.allowBuffering = operation.Request.InternalAllowBuffering;
			this.sendChunked = operation.Request.SendChunked && operation.WriteBuffer == null;
			if (!this.sendChunked && this.allowBuffering && operation.WriteBuffer == null)
			{
				this.writeBuffer = new MemoryStream();
			}
			this.KeepAlive = base.Request.KeepAlive;
			if (((tunnel != null) ? tunnel.ProxyVersion : null) != null && ((tunnel != null) ? tunnel.ProxyVersion : null) != HttpVersion.Version11)
			{
				this.KeepAlive = false;
			}
		}

		// Token: 0x1700062D RID: 1581
		// (get) Token: 0x06001BD8 RID: 7128 RVA: 0x00079247 File Offset: 0x00077447
		internal Stream InnerStream { get; }

		// Token: 0x1700062E RID: 1582
		// (get) Token: 0x06001BD9 RID: 7129 RVA: 0x0007924F File Offset: 0x0007744F
		public bool KeepAlive { get; }

		// Token: 0x1700062F RID: 1583
		// (get) Token: 0x06001BDA RID: 7130 RVA: 0x000028AE File Offset: 0x00000AAE
		public override bool CanRead
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000630 RID: 1584
		// (get) Token: 0x06001BDB RID: 7131 RVA: 0x00003BCC File Offset: 0x00001DCC
		public override bool CanWrite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000631 RID: 1585
		// (get) Token: 0x06001BDC RID: 7132 RVA: 0x00079257 File Offset: 0x00077457
		internal bool HasWriteBuffer
		{
			get
			{
				return base.Operation.WriteBuffer != null || this.writeBuffer != null;
			}
		}

		// Token: 0x17000632 RID: 1586
		// (get) Token: 0x06001BDD RID: 7133 RVA: 0x00079271 File Offset: 0x00077471
		internal int WriteBufferLength
		{
			get
			{
				if (base.Operation.WriteBuffer != null)
				{
					return base.Operation.WriteBuffer.Size;
				}
				if (this.writeBuffer != null)
				{
					return (int)this.writeBuffer.Length;
				}
				return -1;
			}
		}

		// Token: 0x06001BDE RID: 7134 RVA: 0x000792A8 File Offset: 0x000774A8
		internal BufferOffsetSize GetWriteBuffer()
		{
			if (base.Operation.WriteBuffer != null)
			{
				return base.Operation.WriteBuffer;
			}
			if (this.writeBuffer == null || this.writeBuffer.Length == 0L)
			{
				return null;
			}
			return new BufferOffsetSize(this.writeBuffer.GetBuffer(), 0, (int)this.writeBuffer.Length, false);
		}

		// Token: 0x06001BDF RID: 7135 RVA: 0x00079304 File Offset: 0x00077504
		private async Task FinishWriting(CancellationToken cancellationToken)
		{
			if (Interlocked.CompareExchange(ref this.completeRequestWritten, 1, 0) == 0)
			{
				try
				{
					base.Operation.ThrowIfClosedOrDisposed(cancellationToken);
					if (this.sendChunked)
					{
						await this.WriteChunkTrailer_inner(cancellationToken).ConfigureAwait(false);
					}
				}
				catch (Exception ex)
				{
					base.Operation.CompleteRequestWritten(this, ex);
					throw;
				}
				finally
				{
				}
				base.Operation.CompleteRequestWritten(this, null);
			}
		}

		// Token: 0x06001BE0 RID: 7136 RVA: 0x00079350 File Offset: 0x00077550
		public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			int num = buffer.Length;
			if (offset < 0 || num < offset)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (count < 0 || num - offset < count)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCanceled(cancellationToken);
			}
			base.Operation.ThrowIfClosedOrDisposed(cancellationToken);
			if (base.Operation.WriteBuffer != null)
			{
				throw new InvalidOperationException();
			}
			WebCompletionSource webCompletionSource = new WebCompletionSource();
			if (Interlocked.CompareExchange<WebCompletionSource>(ref this.pendingWrite, webCompletionSource, null) != null)
			{
				throw new InvalidOperationException(SR.GetString("Cannot re-call BeginGetRequestStream/BeginGetResponse while a previous call is still in progress."));
			}
			return this.WriteAsyncInner(buffer, offset, count, webCompletionSource, cancellationToken);
		}

		// Token: 0x06001BE1 RID: 7137 RVA: 0x000793FC File Offset: 0x000775FC
		private async Task WriteAsyncInner(byte[] buffer, int offset, int size, WebCompletionSource completion, CancellationToken cancellationToken)
		{
			try
			{
				await this.ProcessWrite(buffer, offset, size, cancellationToken).ConfigureAwait(false);
				if (base.Request.ContentLength > 0L && this.totalWritten == base.Request.ContentLength)
				{
					await this.FinishWriting(cancellationToken);
				}
				this.pendingWrite = null;
				completion.TrySetCompleted();
			}
			catch (Exception ex)
			{
				this.KillBuffer();
				this.closed = true;
				ExceptionDispatchInfo exceptionDispatchInfo = base.Operation.CheckDisposed(cancellationToken);
				if (exceptionDispatchInfo != null)
				{
					ex = exceptionDispatchInfo.SourceException;
				}
				else if (ex is SocketException)
				{
					ex = new IOException("Error writing request", ex);
				}
				base.Operation.CompleteRequestWritten(this, ex);
				this.pendingWrite = null;
				completion.TrySetException(ex);
				if (exceptionDispatchInfo != null)
				{
					exceptionDispatchInfo.Throw();
				}
				throw;
			}
		}

		// Token: 0x06001BE2 RID: 7138 RVA: 0x0007946C File Offset: 0x0007766C
		private async Task ProcessWrite(byte[] buffer, int offset, int size, CancellationToken cancellationToken)
		{
			base.Operation.ThrowIfClosedOrDisposed(cancellationToken);
			if (this.sendChunked)
			{
				this.requestWritten = true;
				string text = string.Format("{0:X}\r\n", size);
				byte[] bytes = Encoding.ASCII.GetBytes(text);
				int num = 2 + size + bytes.Length;
				byte[] array = new byte[num];
				Buffer.BlockCopy(bytes, 0, array, 0, bytes.Length);
				Buffer.BlockCopy(buffer, offset, array, bytes.Length, size);
				Buffer.BlockCopy(WebRequestStream.crlf, 0, array, bytes.Length + size, WebRequestStream.crlf.Length);
				if (this.allowBuffering)
				{
					if (this.writeBuffer == null)
					{
						this.writeBuffer = new MemoryStream();
					}
					this.writeBuffer.Write(buffer, offset, size);
				}
				this.totalWritten += (long)size;
				buffer = array;
				offset = 0;
				size = num;
			}
			else
			{
				this.CheckWriteOverflow(base.Request.ContentLength, this.totalWritten, (long)size);
				if (this.allowBuffering)
				{
					if (this.writeBuffer == null)
					{
						this.writeBuffer = new MemoryStream();
					}
					this.writeBuffer.Write(buffer, offset, size);
					this.totalWritten += (long)size;
					if (base.Request.ContentLength <= 0L || this.totalWritten < base.Request.ContentLength)
					{
						return;
					}
					this.requestWritten = true;
					buffer = this.writeBuffer.GetBuffer();
					offset = 0;
					size = (int)this.totalWritten;
				}
				else
				{
					this.totalWritten += (long)size;
				}
			}
			await this.InnerStream.WriteAsync(buffer, offset, size, cancellationToken).ConfigureAwait(false);
		}

		// Token: 0x06001BE3 RID: 7139 RVA: 0x000794D0 File Offset: 0x000776D0
		private void CheckWriteOverflow(long contentLength, long totalWritten, long size)
		{
			if (contentLength == -1L)
			{
				return;
			}
			long num = contentLength - totalWritten;
			if (size > num)
			{
				this.KillBuffer();
				this.closed = true;
				ProtocolViolationException ex = new ProtocolViolationException("The number of bytes to be written is greater than the specified ContentLength.");
				base.Operation.CompleteRequestWritten(this, ex);
				throw ex;
			}
		}

		// Token: 0x06001BE4 RID: 7140 RVA: 0x00079514 File Offset: 0x00077714
		internal async Task Initialize(CancellationToken cancellationToken)
		{
			base.Operation.ThrowIfClosedOrDisposed(cancellationToken);
			if (base.Operation.WriteBuffer != null)
			{
				if (base.Operation.IsNtlmChallenge)
				{
					base.Request.InternalContentLength = 0L;
				}
				else
				{
					base.Request.InternalContentLength = (long)base.Operation.WriteBuffer.Size;
				}
			}
			await this.SetHeadersAsync(false, cancellationToken).ConfigureAwait(false);
			base.Operation.ThrowIfClosedOrDisposed(cancellationToken);
			if (base.Operation.WriteBuffer != null && !base.Operation.IsNtlmChallenge)
			{
				await this.WriteRequestAsync(cancellationToken);
				this.Close();
			}
		}

		// Token: 0x06001BE5 RID: 7141 RVA: 0x00079560 File Offset: 0x00077760
		private async Task SetHeadersAsync(bool setInternalLength, CancellationToken cancellationToken)
		{
			base.Operation.ThrowIfClosedOrDisposed(cancellationToken);
			if (!this.headersSent)
			{
				string method = base.Request.Method;
				bool flag = method == "GET" || method == "CONNECT" || method == "HEAD" || method == "TRACE";
				bool flag2 = method == "PROPFIND" || method == "PROPPATCH" || method == "MKCOL" || method == "COPY" || method == "MOVE" || method == "LOCK" || method == "UNLOCK";
				if (base.Operation.IsNtlmChallenge)
				{
					flag = true;
				}
				if (setInternalLength && !flag && this.HasWriteBuffer)
				{
					base.Request.InternalContentLength = (long)this.WriteBufferLength;
				}
				bool flag3 = !flag && (!this.HasWriteBuffer || base.Request.ContentLength > -1L);
				if (this.sendChunked || flag3 || flag || flag2)
				{
					this.headersSent = true;
					this.headers = base.Request.GetRequestHeaders();
					try
					{
						await this.InnerStream.WriteAsync(this.headers, 0, this.headers.Length, cancellationToken).ConfigureAwait(false);
						long contentLength = base.Request.ContentLength;
						if (!this.sendChunked && contentLength == 0L)
						{
							this.requestWritten = true;
						}
					}
					catch (Exception ex)
					{
						if (ex is WebException || ex is OperationCanceledException)
						{
							throw;
						}
						throw new WebException("Error writing headers", WebExceptionStatus.SendFailure, WebExceptionInternalStatus.RequestFatal, ex);
					}
				}
			}
		}

		// Token: 0x06001BE6 RID: 7142 RVA: 0x000795B4 File Offset: 0x000777B4
		internal async Task WriteRequestAsync(CancellationToken cancellationToken)
		{
			base.Operation.ThrowIfClosedOrDisposed(cancellationToken);
			if (!this.requestWritten)
			{
				this.requestWritten = true;
				if (!this.sendChunked && this.HasWriteBuffer)
				{
					BufferOffsetSize buffer = this.GetWriteBuffer();
					if (buffer != null && !base.Operation.IsNtlmChallenge && base.Request.ContentLength != -1L && base.Request.ContentLength < (long)buffer.Size)
					{
						this.closed = true;
						WebException ex = new WebException("Specified Content-Length is less than the number of bytes to write", null, WebExceptionStatus.ServerProtocolViolation, null);
						base.Operation.CompleteRequestWritten(this, ex);
						throw ex;
					}
					await this.SetHeadersAsync(true, cancellationToken).ConfigureAwait(false);
					base.Operation.ThrowIfClosedOrDisposed(cancellationToken);
					if (buffer != null && buffer.Size > 0)
					{
						await this.InnerStream.WriteAsync(buffer.Buffer, 0, buffer.Size, cancellationToken);
					}
					await this.FinishWriting(cancellationToken);
				}
			}
		}

		// Token: 0x06001BE7 RID: 7143 RVA: 0x00079600 File Offset: 0x00077800
		private async Task WriteChunkTrailer_inner(CancellationToken cancellationToken)
		{
			if (Interlocked.CompareExchange(ref this.chunkTrailerWritten, 1, 0) == 0)
			{
				base.Operation.ThrowIfClosedOrDisposed(cancellationToken);
				byte[] bytes = Encoding.ASCII.GetBytes("0\r\n\r\n");
				await this.InnerStream.WriteAsync(bytes, 0, bytes.Length, cancellationToken).ConfigureAwait(false);
			}
		}

		// Token: 0x06001BE8 RID: 7144 RVA: 0x0007964C File Offset: 0x0007784C
		private async Task WriteChunkTrailer()
		{
			CancellationTokenSource cts = new CancellationTokenSource();
			try
			{
				cts.CancelAfter(this.WriteTimeout);
				Task timeoutTask = Task.Delay(this.WriteTimeout, cts.Token);
				ConfiguredTaskAwaitable<Task>.ConfiguredTaskAwaiter configuredTaskAwaiter;
				do
				{
					WebCompletionSource webCompletionSource = new WebCompletionSource();
					WebCompletionSource webCompletionSource2 = Interlocked.CompareExchange<WebCompletionSource>(ref this.pendingWrite, webCompletionSource, null);
					if (webCompletionSource2 == null)
					{
						goto IL_010D;
					}
					Task<object> task = webCompletionSource2.WaitForCompletion();
					configuredTaskAwaiter = Task.WhenAny(new Task[] { timeoutTask, task }).ConfigureAwait(false).GetAwaiter();
					if (!configuredTaskAwaiter.IsCompleted)
					{
						await configuredTaskAwaiter;
						ConfiguredTaskAwaitable<Task>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
						configuredTaskAwaiter = configuredTaskAwaiter2;
						configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<Task>.ConfiguredTaskAwaiter);
					}
				}
				while (configuredTaskAwaiter.GetResult() != timeoutTask);
				throw new WebException("The operation has timed out.", WebExceptionStatus.Timeout);
				IL_010D:
				await this.WriteChunkTrailer_inner(cts.Token).ConfigureAwait(false);
				timeoutTask = null;
			}
			catch
			{
			}
			finally
			{
				this.pendingWrite = null;
				cts.Cancel();
				cts.Dispose();
			}
		}

		// Token: 0x06001BE9 RID: 7145 RVA: 0x0007968F File Offset: 0x0007788F
		internal void KillBuffer()
		{
			this.writeBuffer = null;
		}

		// Token: 0x06001BEA RID: 7146 RVA: 0x00079698 File Offset: 0x00077898
		public override Task<int> ReadAsync(byte[] buffer, int offset, int size, CancellationToken cancellationToken)
		{
			return Task.FromException<int>(new NotSupportedException("The stream does not support reading."));
		}

		// Token: 0x06001BEB RID: 7147 RVA: 0x000796A9 File Offset: 0x000778A9
		protected override bool TryReadFromBufferedContent(byte[] buffer, int offset, int count, out int result)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x06001BEC RID: 7148 RVA: 0x000796B0 File Offset: 0x000778B0
		protected override void Close_internal(ref bool disposed)
		{
			if (disposed)
			{
				return;
			}
			disposed = true;
			if (this.sendChunked)
			{
				this.WriteChunkTrailer().Wait();
				return;
			}
			if (!this.allowBuffering || this.requestWritten)
			{
				base.Operation.CompleteRequestWritten(this, null);
				return;
			}
			long contentLength = base.Request.ContentLength;
			if (!this.sendChunked && !base.Operation.IsNtlmChallenge && contentLength != -1L && this.totalWritten != contentLength)
			{
				IOException ex = new IOException("Cannot close the stream until all bytes are written");
				this.closed = true;
				disposed = true;
				WebException ex2 = new WebException("Request was cancelled.", WebExceptionStatus.RequestCanceled, WebExceptionInternalStatus.RequestFatal, ex);
				base.Operation.CompleteRequestWritten(this, ex2);
				throw ex2;
			}
			disposed = true;
			base.Operation.CompleteRequestWritten(this, null);
		}

		// Token: 0x0400124E RID: 4686
		private static byte[] crlf = new byte[] { 13, 10 };

		// Token: 0x0400124F RID: 4687
		private MemoryStream writeBuffer;

		// Token: 0x04001250 RID: 4688
		private bool requestWritten;

		// Token: 0x04001251 RID: 4689
		private bool allowBuffering;

		// Token: 0x04001252 RID: 4690
		private bool sendChunked;

		// Token: 0x04001253 RID: 4691
		private WebCompletionSource pendingWrite;

		// Token: 0x04001254 RID: 4692
		private long totalWritten;

		// Token: 0x04001255 RID: 4693
		private byte[] headers;

		// Token: 0x04001256 RID: 4694
		private bool headersSent;

		// Token: 0x04001257 RID: 4695
		private int completeRequestWritten;

		// Token: 0x04001258 RID: 4696
		private int chunkTrailerWritten;
	}
}
