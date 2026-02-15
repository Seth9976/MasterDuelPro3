using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net
{
	// Token: 0x02000450 RID: 1104
	internal class WebResponseStream : WebConnectionStream
	{
		// Token: 0x17000633 RID: 1587
		// (get) Token: 0x06001BFE RID: 7166 RVA: 0x0007A872 File Offset: 0x00078A72
		public WebRequestStream RequestStream { get; }

		// Token: 0x17000634 RID: 1588
		// (get) Token: 0x06001BFF RID: 7167 RVA: 0x0007A87A File Offset: 0x00078A7A
		// (set) Token: 0x06001C00 RID: 7168 RVA: 0x0007A882 File Offset: 0x00078A82
		public WebHeaderCollection Headers { get; private set; }

		// Token: 0x17000635 RID: 1589
		// (get) Token: 0x06001C01 RID: 7169 RVA: 0x0007A88B File Offset: 0x00078A8B
		// (set) Token: 0x06001C02 RID: 7170 RVA: 0x0007A893 File Offset: 0x00078A93
		public HttpStatusCode StatusCode { get; private set; }

		// Token: 0x17000636 RID: 1590
		// (get) Token: 0x06001C03 RID: 7171 RVA: 0x0007A89C File Offset: 0x00078A9C
		// (set) Token: 0x06001C04 RID: 7172 RVA: 0x0007A8A4 File Offset: 0x00078AA4
		public string StatusDescription { get; private set; }

		// Token: 0x17000637 RID: 1591
		// (get) Token: 0x06001C05 RID: 7173 RVA: 0x0007A8AD File Offset: 0x00078AAD
		// (set) Token: 0x06001C06 RID: 7174 RVA: 0x0007A8B5 File Offset: 0x00078AB5
		public Version Version { get; private set; }

		// Token: 0x17000638 RID: 1592
		// (get) Token: 0x06001C07 RID: 7175 RVA: 0x0007A8BE File Offset: 0x00078ABE
		// (set) Token: 0x06001C08 RID: 7176 RVA: 0x0007A8C6 File Offset: 0x00078AC6
		public bool KeepAlive { get; private set; }

		// Token: 0x06001C09 RID: 7177 RVA: 0x0007A8CF File Offset: 0x00078ACF
		public WebResponseStream(WebRequestStream request)
			: base(request.Connection, request.Operation)
		{
			this.RequestStream = request;
		}

		// Token: 0x17000639 RID: 1593
		// (get) Token: 0x06001C0A RID: 7178 RVA: 0x00003BCC File Offset: 0x00001DCC
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700063A RID: 1594
		// (get) Token: 0x06001C0B RID: 7179 RVA: 0x000028AE File Offset: 0x00000AAE
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700063B RID: 1595
		// (get) Token: 0x06001C0C RID: 7180 RVA: 0x0007A8F5 File Offset: 0x00078AF5
		// (set) Token: 0x06001C0D RID: 7181 RVA: 0x0007A8FD File Offset: 0x00078AFD
		private bool ChunkedRead { get; set; }

		// Token: 0x06001C0E RID: 7182 RVA: 0x0007A908 File Offset: 0x00078B08
		public override async Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
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
			if (Interlocked.CompareExchange(ref this.nestedRead, 1, 0) != 0)
			{
				throw new InvalidOperationException("Invalid nested call.");
			}
			WebCompletionSource completion = new WebCompletionSource();
			while (!cancellationToken.IsCancellationRequested)
			{
				WebCompletionSource webCompletionSource = Interlocked.CompareExchange<WebCompletionSource>(ref this.pendingRead, completion, null);
				if (webCompletionSource == null)
				{
					break;
				}
				await webCompletionSource.WaitForCompletion().ConfigureAwait(false);
			}
			int nbytes = 0;
			Exception throwMe = null;
			try
			{
				nbytes = await this.ProcessRead(buffer, offset, count, cancellationToken).ConfigureAwait(false);
			}
			catch (Exception ex)
			{
				throwMe = this.GetReadException(WebExceptionStatus.ReceiveFailure, ex, "ReadAsync");
			}
			object obj;
			if (throwMe != null)
			{
				obj = this.locker;
				lock (obj)
				{
					completion.TrySetException(throwMe);
					this.pendingRead = null;
					this.nestedRead = 0;
				}
				this.closed = true;
				base.Operation.Finish(false, throwMe);
				throw throwMe;
			}
			obj = this.locker;
			lock (obj)
			{
				completion.TrySetCompleted();
				this.pendingRead = null;
				this.nestedRead = 0;
			}
			if (nbytes <= 0 && !this.read_eof)
			{
				this.read_eof = true;
				if (!this.nextReadCalled && !this.nextReadCalled)
				{
					this.nextReadCalled = true;
					base.Operation.Finish(true, null);
				}
			}
			return nbytes;
		}

		// Token: 0x06001C0F RID: 7183 RVA: 0x0007A96C File Offset: 0x00078B6C
		private Task<int> ProcessRead(byte[] buffer, int offset, int size, CancellationToken cancellationToken)
		{
			if (this.read_eof)
			{
				return Task.FromResult<int>(0);
			}
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCanceled<int>(cancellationToken);
			}
			return HttpWebRequest.RunWithTimeout<int>((CancellationToken ct) => this.innerStream.ReadAsync(buffer, offset, size, ct), this.ReadTimeout, delegate
			{
				this.Operation.Abort();
				this.innerStream.Dispose();
			}, () => this.Operation.Aborted, cancellationToken);
		}

		// Token: 0x06001C10 RID: 7184 RVA: 0x0007A9EC File Offset: 0x00078BEC
		protected override bool TryReadFromBufferedContent(byte[] buffer, int offset, int count, out int result)
		{
			if (this.bufferedEntireContent)
			{
				BufferedReadStream bufferedReadStream = this.innerStream as BufferedReadStream;
				if (bufferedReadStream != null)
				{
					return bufferedReadStream.TryReadFromBuffer(buffer, offset, count, out result);
				}
			}
			result = 0;
			return false;
		}

		// Token: 0x1700063C RID: 1596
		// (get) Token: 0x06001C11 RID: 7185 RVA: 0x0007AA24 File Offset: 0x00078C24
		private bool ExpectContent
		{
			get
			{
				return !(base.Request.Method == "HEAD") && (this.StatusCode >= HttpStatusCode.OK && this.StatusCode != HttpStatusCode.NoContent) && this.StatusCode != HttpStatusCode.NotModified;
			}
		}

		// Token: 0x06001C12 RID: 7186 RVA: 0x0007AA78 File Offset: 0x00078C78
		private void Initialize(BufferOffsetSize buffer)
		{
			string text = this.Headers["Transfer-Encoding"];
			bool flag = text != null && text.IndexOf("chunked", StringComparison.OrdinalIgnoreCase) != -1;
			string text2 = this.Headers["Content-Length"];
			long num;
			if (!flag && !string.IsNullOrEmpty(text2))
			{
				if (!long.TryParse(text2, out num))
				{
					num = long.MaxValue;
				}
			}
			else
			{
				num = long.MaxValue;
			}
			string text3 = null;
			if (this.ExpectContent)
			{
				text3 = this.Headers["Transfer-Encoding"];
			}
			this.ChunkedRead = text3 != null && text3.IndexOf("chunked", StringComparison.OrdinalIgnoreCase) != -1;
			if (this.Version == HttpVersion.Version11 && this.RequestStream.KeepAlive)
			{
				this.KeepAlive = true;
				string text4 = this.Headers[base.ServicePoint.UsesProxy ? "Proxy-Connection" : "Connection"];
				if (text4 != null)
				{
					text4 = text4.ToLower();
					this.KeepAlive = text4.IndexOf("keep-alive", StringComparison.Ordinal) != -1;
					if (text4.IndexOf("close", StringComparison.Ordinal) != -1)
					{
						this.KeepAlive = false;
					}
				}
				if (!this.ChunkedRead && num == 9223372036854775807L)
				{
					this.KeepAlive = false;
				}
			}
			Stream stream;
			if (!this.ExpectContent || (!this.ChunkedRead && (long)buffer.Size >= num))
			{
				this.bufferedEntireContent = true;
				this.innerStream = new BufferedReadStream(base.Operation, null, buffer);
				stream = this.innerStream;
			}
			else if (buffer.Size > 0)
			{
				stream = new BufferedReadStream(base.Operation, this.RequestStream.InnerStream, buffer);
			}
			else
			{
				stream = this.RequestStream.InnerStream;
			}
			if (this.ChunkedRead)
			{
				this.innerStream = new MonoChunkStream(base.Operation, stream, this.Headers);
			}
			else if (!this.bufferedEntireContent)
			{
				if (num != 9223372036854775807L)
				{
					this.innerStream = new FixedSizeReadStream(base.Operation, stream, num);
				}
				else
				{
					this.innerStream = new BufferedReadStream(base.Operation, stream, null);
				}
			}
			string text5 = this.Headers["Content-Encoding"];
			if (text5 == "gzip" && (base.Request.AutomaticDecompression & DecompressionMethods.GZip) != DecompressionMethods.None)
			{
				this.innerStream = ContentDecodeStream.Create(base.Operation, this.innerStream, ContentDecodeStream.Mode.GZip);
				this.Headers.Remove(HttpRequestHeader.ContentEncoding);
			}
			else if (text5 == "deflate" && (base.Request.AutomaticDecompression & DecompressionMethods.Deflate) != DecompressionMethods.None)
			{
				this.innerStream = ContentDecodeStream.Create(base.Operation, this.innerStream, ContentDecodeStream.Mode.Deflate);
				this.Headers.Remove(HttpRequestHeader.ContentEncoding);
			}
			if (!this.ExpectContent)
			{
				this.nextReadCalled = true;
				base.Operation.Finish(true, null);
			}
		}

		// Token: 0x06001C13 RID: 7187 RVA: 0x0007AD50 File Offset: 0x00078F50
		private async Task<byte[]> ReadAllAsyncInner(CancellationToken cancellationToken)
		{
			long maximumSize = (long)HttpWebRequest.DefaultMaximumErrorResponseLength << 16;
			byte[] array;
			using (MemoryStream ms = new MemoryStream())
			{
				while (ms.Position < maximumSize)
				{
					cancellationToken.ThrowIfCancellationRequested();
					byte[] buffer = new byte[16384];
					int num = await this.ProcessRead(buffer, 0, buffer.Length, cancellationToken).ConfigureAwait(false);
					if (num < 0)
					{
						throw new IOException();
					}
					if (num == 0)
					{
						break;
					}
					ms.Write(buffer, 0, num);
					buffer = null;
				}
				array = ms.ToArray();
			}
			return array;
		}

		// Token: 0x06001C14 RID: 7188 RVA: 0x0007AD9C File Offset: 0x00078F9C
		internal async Task ReadAllAsync(bool resending, CancellationToken cancellationToken)
		{
			if (this.read_eof || this.bufferedEntireContent || this.nextReadCalled)
			{
				if (!this.nextReadCalled)
				{
					this.nextReadCalled = true;
					base.Operation.Finish(true, null);
				}
			}
			else
			{
				WebCompletionSource completion = new WebCompletionSource();
				CancellationTokenSource timeoutCts = new CancellationTokenSource();
				try
				{
					Task timeoutTask = Task.Delay(this.ReadTimeout, timeoutCts.Token);
					ConfiguredTaskAwaitable<Task>.ConfiguredTaskAwaiter configuredTaskAwaiter;
					do
					{
						cancellationToken.ThrowIfCancellationRequested();
						WebCompletionSource webCompletionSource = Interlocked.CompareExchange<WebCompletionSource>(ref this.pendingRead, completion, null);
						if (webCompletionSource == null)
						{
							goto IL_0147;
						}
						Task<object> task = webCompletionSource.WaitForCompletion();
						configuredTaskAwaiter = Task.WhenAny(new Task[] { task, timeoutTask }).ConfigureAwait(false).GetAwaiter();
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
					IL_0147:
					timeoutTask = null;
				}
				finally
				{
					timeoutCts.Cancel();
					timeoutCts.Dispose();
				}
				try
				{
					cancellationToken.ThrowIfCancellationRequested();
					if (this.read_eof || this.bufferedEntireContent)
					{
						return;
					}
					if (resending && !this.KeepAlive)
					{
						this.Close();
						return;
					}
					byte[] array = await this.ReadAllAsyncInner(cancellationToken).ConfigureAwait(false);
					BufferOffsetSize bufferOffsetSize = new BufferOffsetSize(array, 0, array.Length, false);
					this.innerStream = new BufferedReadStream(base.Operation, null, bufferOffsetSize);
					this.bufferedEntireContent = true;
					this.nextReadCalled = true;
					completion.TrySetCompleted();
				}
				catch (Exception ex)
				{
					completion.TrySetException(ex);
					throw;
				}
				finally
				{
					this.pendingRead = null;
				}
				base.Operation.Finish(true, null);
			}
		}

		// Token: 0x06001C15 RID: 7189 RVA: 0x0007ADEF File Offset: 0x00078FEF
		public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			return Task.FromException(new NotSupportedException("The stream does not support writing."));
		}

		// Token: 0x06001C16 RID: 7190 RVA: 0x0007AE00 File Offset: 0x00079000
		protected override void Close_internal(ref bool disposed)
		{
			if (!this.closed && !this.nextReadCalled)
			{
				this.nextReadCalled = true;
				if (this.read_eof || this.bufferedEntireContent)
				{
					disposed = true;
					WebReadStream webReadStream = this.innerStream;
					if (webReadStream != null)
					{
						webReadStream.Dispose();
					}
					this.innerStream = null;
					base.Operation.Finish(true, null);
					return;
				}
				this.closed = true;
				disposed = true;
				base.Operation.Finish(false, null);
			}
		}

		// Token: 0x06001C17 RID: 7191 RVA: 0x0007AE74 File Offset: 0x00079074
		private WebException GetReadException(WebExceptionStatus status, Exception error, string where)
		{
			error = base.GetException(error);
			string.Format("Error getting response stream ({0}): {1}", where, status);
			if (error == null)
			{
				return new WebException(string.Format("Error getting response stream ({0}): {1}", where, status), status);
			}
			WebException ex = error as WebException;
			if (ex != null)
			{
				return ex;
			}
			if (base.Operation.Aborted || error is OperationCanceledException || error is ObjectDisposedException)
			{
				return HttpWebRequest.CreateRequestAbortedException();
			}
			return new WebException(string.Format("Error getting response stream ({0}): {1} {2}", where, status, error.Message), status, WebExceptionInternalStatus.RequestFatal, error);
		}

		// Token: 0x06001C18 RID: 7192 RVA: 0x0007AF08 File Offset: 0x00079108
		internal async Task InitReadAsync(CancellationToken cancellationToken)
		{
			BufferOffsetSize buffer = new BufferOffsetSize(new byte[4096], false);
			ReadState state = ReadState.None;
			int position = 0;
			for (;;)
			{
				base.Operation.ThrowIfClosedOrDisposed(cancellationToken);
				int num = await this.RequestStream.InnerStream.ReadAsync(buffer.Buffer, buffer.Offset, buffer.Size, cancellationToken).ConfigureAwait(false);
				if (num == 0)
				{
					break;
				}
				if (num < 0)
				{
					goto Block_2;
				}
				buffer.Offset += num;
				buffer.Size -= num;
				if (state == ReadState.None)
				{
					try
					{
						int num2 = position;
						if (!this.GetResponse(buffer, ref position, ref state))
						{
							position = num2;
						}
					}
					catch (Exception ex)
					{
						throw this.GetReadException(WebExceptionStatus.ServerProtocolViolation, ex, "ReadDoneAsync4");
					}
				}
				if (state == ReadState.Aborted)
				{
					goto Block_4;
				}
				if (state == ReadState.Content)
				{
					goto Block_5;
				}
				int num3 = num * 2;
				if (num3 > buffer.Size)
				{
					byte[] array = new byte[buffer.Buffer.Length + num3];
					Buffer.BlockCopy(buffer.Buffer, 0, array, 0, buffer.Offset);
					buffer = new BufferOffsetSize(array, buffer.Offset, array.Length - buffer.Offset, false);
				}
				state = ReadState.None;
				position = 0;
			}
			throw this.GetReadException(WebExceptionStatus.ReceiveFailure, null, "ReadDoneAsync2");
			Block_2:
			throw this.GetReadException(WebExceptionStatus.ServerProtocolViolation, null, "ReadDoneAsync3");
			Block_4:
			throw this.GetReadException(WebExceptionStatus.RequestCanceled, null, "ReadDoneAsync5");
			Block_5:
			buffer.Size = buffer.Offset - position;
			buffer.Offset = position;
			try
			{
				this.Initialize(buffer);
			}
			catch (Exception ex2)
			{
				throw this.GetReadException(WebExceptionStatus.ReceiveFailure, ex2, "ReadDoneAsync6");
			}
		}

		// Token: 0x06001C19 RID: 7193 RVA: 0x0007AF54 File Offset: 0x00079154
		private bool GetResponse(BufferOffsetSize buffer, ref int pos, ref ReadState state)
		{
			string text = null;
			bool flag = false;
			bool flag2 = false;
			while (state != ReadState.Aborted)
			{
				if (state != ReadState.None)
				{
					goto IL_00F2;
				}
				if (!WebConnection.ReadLine(buffer.Buffer, ref pos, buffer.Offset, ref text))
				{
					return false;
				}
				if (text == null)
				{
					flag2 = true;
				}
				else
				{
					flag2 = false;
					state = ReadState.Status;
					string[] array = text.Split(' ', StringSplitOptions.None);
					if (array.Length < 2)
					{
						throw this.GetReadException(WebExceptionStatus.ServerProtocolViolation, null, "GetResponse");
					}
					if (string.Compare(array[0], "HTTP/1.1", true) == 0)
					{
						this.Version = HttpVersion.Version11;
						base.ServicePoint.SetVersion(HttpVersion.Version11);
					}
					else
					{
						this.Version = HttpVersion.Version10;
						base.ServicePoint.SetVersion(HttpVersion.Version10);
					}
					this.StatusCode = (HttpStatusCode)uint.Parse(array[1]);
					if (array.Length >= 3)
					{
						this.StatusDescription = string.Join(" ", array, 2, array.Length - 2);
					}
					else
					{
						this.StatusDescription = string.Empty;
					}
					if (pos >= buffer.Offset)
					{
						return true;
					}
					goto IL_00F2;
				}
				IL_027F:
				if (!flag2 && !flag)
				{
					throw this.GetReadException(WebExceptionStatus.ServerProtocolViolation, null, "GetResponse");
				}
				continue;
				IL_00F2:
				flag2 = false;
				if (state != ReadState.Status)
				{
					goto IL_027F;
				}
				state = ReadState.Headers;
				this.Headers = new WebHeaderCollection();
				List<string> list = new List<string>();
				bool flag3 = false;
				while (!flag3 && WebConnection.ReadLine(buffer.Buffer, ref pos, buffer.Offset, ref text))
				{
					if (text == null)
					{
						flag3 = true;
					}
					else if (text.Length > 0 && (text[0] == ' ' || text[0] == '\t'))
					{
						int num = list.Count - 1;
						if (num < 0)
						{
							break;
						}
						string text2 = list[num] + text;
						list[num] = text2;
					}
					else
					{
						list.Add(text);
					}
				}
				if (!flag3)
				{
					return false;
				}
				foreach (string text3 in list)
				{
					int num2 = text3.IndexOf(':');
					if (num2 == -1)
					{
						throw new ArgumentException("no colon found", "header");
					}
					string text4 = text3.Substring(0, num2);
					string text5 = text3.Substring(num2 + 1).Trim();
					if (WebHeaderCollection.AllowMultiValues(text4))
					{
						this.Headers.AddInternal(text4, text5);
					}
					else
					{
						this.Headers.SetInternal(text4, text5);
					}
				}
				if (this.StatusCode != HttpStatusCode.Continue)
				{
					state = ReadState.Content;
					return true;
				}
				base.ServicePoint.SendContinue = true;
				if (pos >= buffer.Offset)
				{
					return true;
				}
				if (base.Request.ExpectContinue)
				{
					base.Request.DoContinueDelegate((int)this.StatusCode, this.Headers);
					base.Request.ExpectContinue = false;
				}
				state = ReadState.None;
				flag = true;
				goto IL_027F;
			}
			throw this.GetReadException(WebExceptionStatus.RequestCanceled, null, "GetResponse");
		}

		// Token: 0x04001291 RID: 4753
		private WebReadStream innerStream;

		// Token: 0x04001292 RID: 4754
		private bool nextReadCalled;

		// Token: 0x04001293 RID: 4755
		private bool bufferedEntireContent;

		// Token: 0x04001294 RID: 4756
		private WebCompletionSource pendingRead;

		// Token: 0x04001295 RID: 4757
		private object locker = new object();

		// Token: 0x04001296 RID: 4758
		private int nestedRead;

		// Token: 0x04001297 RID: 4759
		private bool read_eof;
	}
}
