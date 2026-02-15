using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using Mono.Security.Interface;

namespace Mono.Net.Security
{
	// Token: 0x0200006C RID: 108
	internal abstract class MobileAuthenticatedStream : AuthenticatedStream, IDisposable
	{
		// Token: 0x0600014C RID: 332 RVA: 0x00005BA8 File Offset: 0x00003DA8
		public MobileAuthenticatedStream(Stream innerStream, bool leaveInnerStreamOpen, SslStream owner, MonoTlsSettings settings, MobileTlsProvider provider)
			: base(innerStream, leaveInnerStreamOpen)
		{
			this.SslStream = owner;
			this.Settings = settings;
			this.Provider = provider;
			this.readBuffer = new BufferOffsetSize2(16500);
			this.writeBuffer = new BufferOffsetSize2(16384);
			this.operation = MobileAuthenticatedStream.Operation.None;
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600014D RID: 333 RVA: 0x00005C19 File Offset: 0x00003E19
		public SslStream SslStream { get; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600014E RID: 334 RVA: 0x00005C21 File Offset: 0x00003E21
		public MonoTlsSettings Settings { get; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600014F RID: 335 RVA: 0x00005C29 File Offset: 0x00003E29
		public MobileTlsProvider Provider { get; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000150 RID: 336 RVA: 0x00005C31 File Offset: 0x00003E31
		// (set) Token: 0x06000151 RID: 337 RVA: 0x00005C39 File Offset: 0x00003E39
		internal string TargetHost { get; private set; }

		// Token: 0x06000152 RID: 338 RVA: 0x00005C44 File Offset: 0x00003E44
		internal void CheckThrow(bool authSuccessCheck, bool shutdownCheck = false)
		{
			if (this.lastException != null)
			{
				this.lastException.Throw();
			}
			if (authSuccessCheck && !this.IsAuthenticated)
			{
				throw new InvalidOperationException("This operation is only allowed using a successfully authenticated context.");
			}
			if (shutdownCheck && this.shutdown)
			{
				throw new InvalidOperationException("Write operations are not allowed after the channel was shutdown.");
			}
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00005C90 File Offset: 0x00003E90
		internal static Exception GetSSPIException(Exception e)
		{
			if (e is OperationCanceledException || e is IOException || e is ObjectDisposedException || e is AuthenticationException || e is NotSupportedException)
			{
				return e;
			}
			return new AuthenticationException("Authentication failed, see inner exception.", e);
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00005CC7 File Offset: 0x00003EC7
		internal static Exception GetIOException(Exception e, string message)
		{
			if (e is OperationCanceledException || e is IOException || e is ObjectDisposedException || e is AuthenticationException || e is NotSupportedException)
			{
				return e;
			}
			return new IOException(message, e);
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00005CFA File Offset: 0x00003EFA
		internal static Exception GetInternalError()
		{
			throw new InvalidOperationException("Internal error.");
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00005D06 File Offset: 0x00003F06
		internal static Exception GetInvalidNestedCallException()
		{
			throw new InvalidOperationException("Invalid nested call.");
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00005D14 File Offset: 0x00003F14
		internal ExceptionDispatchInfo SetException(Exception e)
		{
			ExceptionDispatchInfo exceptionDispatchInfo = ExceptionDispatchInfo.Capture(e);
			return Interlocked.CompareExchange<ExceptionDispatchInfo>(ref this.lastException, exceptionDispatchInfo, null) ?? exceptionDispatchInfo;
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00005D3C File Offset: 0x00003F3C
		public void AuthenticateAsClient(string targetHost, X509CertificateCollection clientCertificates, SslProtocols enabledSslProtocols, bool checkCertificateRevocation)
		{
			MonoSslClientAuthenticationOptions monoSslClientAuthenticationOptions = new MonoSslClientAuthenticationOptions
			{
				TargetHost = targetHost,
				ClientCertificates = clientCertificates,
				EnabledSslProtocols = enabledSslProtocols,
				CertificateRevocationCheckMode = (checkCertificateRevocation ? X509RevocationMode.Online : X509RevocationMode.NoCheck),
				EncryptionPolicy = EncryptionPolicy.RequireEncryption
			};
			Task task = this.ProcessAuthentication(true, monoSslClientAuthenticationOptions, CancellationToken.None);
			try
			{
				task.Wait();
			}
			catch (Exception ex)
			{
				throw HttpWebRequest.FlattenException(ex);
			}
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00005DA8 File Offset: 0x00003FA8
		public void AuthenticateAsServer(X509Certificate serverCertificate, bool clientCertificateRequired, SslProtocols enabledSslProtocols, bool checkCertificateRevocation)
		{
			MonoSslServerAuthenticationOptions monoSslServerAuthenticationOptions = new MonoSslServerAuthenticationOptions
			{
				ServerCertificate = serverCertificate,
				ClientCertificateRequired = clientCertificateRequired,
				EnabledSslProtocols = enabledSslProtocols,
				CertificateRevocationCheckMode = (checkCertificateRevocation ? X509RevocationMode.Online : X509RevocationMode.NoCheck),
				EncryptionPolicy = EncryptionPolicy.RequireEncryption
			};
			Task task = this.ProcessAuthentication(true, monoSslServerAuthenticationOptions, CancellationToken.None);
			try
			{
				task.Wait();
			}
			catch (Exception ex)
			{
				throw HttpWebRequest.FlattenException(ex);
			}
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00005E14 File Offset: 0x00004014
		public Task AuthenticateAsClientAsync(string targetHost, X509CertificateCollection clientCertificates, SslProtocols enabledSslProtocols, bool checkCertificateRevocation)
		{
			MonoSslClientAuthenticationOptions monoSslClientAuthenticationOptions = new MonoSslClientAuthenticationOptions
			{
				TargetHost = targetHost,
				ClientCertificates = clientCertificates,
				EnabledSslProtocols = enabledSslProtocols,
				CertificateRevocationCheckMode = (checkCertificateRevocation ? X509RevocationMode.Online : X509RevocationMode.NoCheck),
				EncryptionPolicy = EncryptionPolicy.RequireEncryption
			};
			return this.ProcessAuthentication(false, monoSslClientAuthenticationOptions, CancellationToken.None);
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00005E60 File Offset: 0x00004060
		private async Task ProcessAuthentication(bool runSynchronously, MonoSslAuthenticationOptions options, CancellationToken cancellationToken)
		{
			if (options.ServerMode)
			{
				if (options.ServerCertificate == null && options.ServerCertSelectionDelegate == null)
				{
					throw new ArgumentException("ServerCertificate");
				}
			}
			else
			{
				if (options.TargetHost == null)
				{
					throw new ArgumentException("TargetHost");
				}
				if (options.TargetHost.Length == 0)
				{
					options.TargetHost = "?" + Interlocked.Increment(ref MobileAuthenticatedStream.uniqueNameInteger).ToString(NumberFormatInfo.InvariantInfo);
				}
				this.TargetHost = options.TargetHost;
			}
			if (this.lastException != null)
			{
				this.lastException.Throw();
			}
			AsyncHandshakeRequest asyncHandshakeRequest = new AsyncHandshakeRequest(this, runSynchronously);
			if (Interlocked.CompareExchange<AsyncProtocolRequest>(ref this.asyncHandshakeRequest, asyncHandshakeRequest, null) != null)
			{
				throw MobileAuthenticatedStream.GetInvalidNestedCallException();
			}
			if (Interlocked.CompareExchange<AsyncProtocolRequest>(ref this.asyncReadRequest, asyncHandshakeRequest, null) != null)
			{
				throw MobileAuthenticatedStream.GetInvalidNestedCallException();
			}
			if (Interlocked.CompareExchange<AsyncProtocolRequest>(ref this.asyncWriteRequest, asyncHandshakeRequest, null) != null)
			{
				throw MobileAuthenticatedStream.GetInvalidNestedCallException();
			}
			AsyncProtocolResult asyncProtocolResult;
			try
			{
				object obj = this.ioLock;
				lock (obj)
				{
					if (this.xobileTlsContext != null)
					{
						throw new InvalidOperationException();
					}
					this.readBuffer.Reset();
					this.writeBuffer.Reset();
					this.xobileTlsContext = this.CreateContext(options);
				}
				try
				{
					asyncProtocolResult = await asyncHandshakeRequest.StartOperation(cancellationToken).ConfigureAwait(false);
				}
				catch (Exception ex)
				{
					asyncProtocolResult = new AsyncProtocolResult(this.SetException(MobileAuthenticatedStream.GetSSPIException(ex)));
				}
			}
			finally
			{
				object obj = this.ioLock;
				bool flag = false;
				try
				{
					Monitor.Enter(obj, ref flag);
					this.readBuffer.Reset();
					this.writeBuffer.Reset();
					this.asyncWriteRequest = null;
					this.asyncReadRequest = null;
					this.asyncHandshakeRequest = null;
				}
				finally
				{
					int num;
					if (num < 0 && flag)
					{
						Monitor.Exit(obj);
					}
				}
			}
			if (asyncProtocolResult.Error != null)
			{
				asyncProtocolResult.Error.Throw();
			}
		}

		// Token: 0x0600015C RID: 348
		protected abstract MobileTlsContext CreateContext(MonoSslAuthenticationOptions options);

		// Token: 0x0600015D RID: 349 RVA: 0x00005EBC File Offset: 0x000040BC
		public override int Read(byte[] buffer, int offset, int count)
		{
			AsyncReadRequest asyncReadRequest = new AsyncReadRequest(this, true, buffer, offset, count);
			return this.StartOperation(MobileAuthenticatedStream.OperationType.Read, asyncReadRequest, CancellationToken.None).Result;
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00005EE8 File Offset: 0x000040E8
		public override void Write(byte[] buffer, int offset, int count)
		{
			AsyncWriteRequest asyncWriteRequest = new AsyncWriteRequest(this, true, buffer, offset, count);
			this.StartOperation(MobileAuthenticatedStream.OperationType.Write, asyncWriteRequest, CancellationToken.None).Wait();
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00005F14 File Offset: 0x00004114
		public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			AsyncReadRequest asyncReadRequest = new AsyncReadRequest(this, false, buffer, offset, count);
			return this.StartOperation(MobileAuthenticatedStream.OperationType.Read, asyncReadRequest, cancellationToken);
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00005F38 File Offset: 0x00004138
		public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			AsyncWriteRequest asyncWriteRequest = new AsyncWriteRequest(this, false, buffer, offset, count);
			return this.StartOperation(MobileAuthenticatedStream.OperationType.Write, asyncWriteRequest, cancellationToken);
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00005F5C File Offset: 0x0000415C
		private async Task<int> StartOperation(MobileAuthenticatedStream.OperationType type, AsyncProtocolRequest asyncRequest, CancellationToken cancellationToken)
		{
			this.CheckThrow(true, type > MobileAuthenticatedStream.OperationType.Read);
			if (type == MobileAuthenticatedStream.OperationType.Read)
			{
				if (Interlocked.CompareExchange<AsyncProtocolRequest>(ref this.asyncReadRequest, asyncRequest, null) != null)
				{
					throw MobileAuthenticatedStream.GetInvalidNestedCallException();
				}
			}
			else if (type == MobileAuthenticatedStream.OperationType.Renegotiate)
			{
				if (Interlocked.CompareExchange<AsyncProtocolRequest>(ref this.asyncHandshakeRequest, asyncRequest, null) != null)
				{
					throw MobileAuthenticatedStream.GetInvalidNestedCallException();
				}
				if (Interlocked.CompareExchange<AsyncProtocolRequest>(ref this.asyncReadRequest, asyncRequest, null) != null)
				{
					throw MobileAuthenticatedStream.GetInvalidNestedCallException();
				}
				if (Interlocked.CompareExchange<AsyncProtocolRequest>(ref this.asyncWriteRequest, asyncRequest, null) != null)
				{
					throw MobileAuthenticatedStream.GetInvalidNestedCallException();
				}
			}
			else if (Interlocked.CompareExchange<AsyncProtocolRequest>(ref this.asyncWriteRequest, asyncRequest, null) != null)
			{
				throw MobileAuthenticatedStream.GetInvalidNestedCallException();
			}
			AsyncProtocolResult asyncProtocolResult;
			try
			{
				object obj = this.ioLock;
				lock (obj)
				{
					if (type == MobileAuthenticatedStream.OperationType.Read)
					{
						this.readBuffer.Reset();
					}
					else
					{
						this.writeBuffer.Reset();
					}
				}
				asyncProtocolResult = await asyncRequest.StartOperation(cancellationToken).ConfigureAwait(false);
			}
			catch (Exception ex)
			{
				asyncProtocolResult = new AsyncProtocolResult(this.SetException(MobileAuthenticatedStream.GetIOException(ex, asyncRequest.Name + " failed")));
			}
			finally
			{
				object obj = this.ioLock;
				bool flag = false;
				try
				{
					Monitor.Enter(obj, ref flag);
					if (type == MobileAuthenticatedStream.OperationType.Read)
					{
						this.readBuffer.Reset();
						this.asyncReadRequest = null;
					}
					else if (type == MobileAuthenticatedStream.OperationType.Renegotiate)
					{
						this.readBuffer.Reset();
						this.writeBuffer.Reset();
						this.asyncHandshakeRequest = null;
						this.asyncReadRequest = null;
						this.asyncWriteRequest = null;
					}
					else
					{
						this.writeBuffer.Reset();
						this.asyncWriteRequest = null;
					}
				}
				finally
				{
					int num;
					if (num < 0 && flag)
					{
						Monitor.Exit(obj);
					}
				}
			}
			if (asyncProtocolResult.Error != null)
			{
				asyncProtocolResult.Error.Throw();
			}
			return asyncProtocolResult.UserResult;
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00005FB8 File Offset: 0x000041B8
		internal int InternalRead(byte[] buffer, int offset, int size, out bool outWantMore)
		{
			int num;
			try
			{
				AsyncProtocolRequest asyncProtocolRequest = this.asyncHandshakeRequest ?? this.asyncReadRequest;
				ValueTuple<int, bool> valueTuple = this.InternalRead(asyncProtocolRequest, this.readBuffer, buffer, offset, size);
				int item = valueTuple.Item1;
				bool item2 = valueTuple.Item2;
				outWantMore = item2;
				num = item;
			}
			catch (Exception ex)
			{
				this.SetException(MobileAuthenticatedStream.GetIOException(ex, "InternalRead() failed"));
				outWantMore = false;
				num = -1;
			}
			return num;
		}

		// Token: 0x06000163 RID: 355 RVA: 0x0000602C File Offset: 0x0000422C
		private ValueTuple<int, bool> InternalRead(AsyncProtocolRequest asyncRequest, BufferOffsetSize internalBuffer, byte[] buffer, int offset, int size)
		{
			if (asyncRequest == null)
			{
				throw new InvalidOperationException();
			}
			if (internalBuffer.Size == 0 && !internalBuffer.Complete)
			{
				internalBuffer.Offset = (internalBuffer.Size = 0);
				asyncRequest.RequestRead(size);
				return new ValueTuple<int, bool>(0, true);
			}
			int num = Math.Min(internalBuffer.Size, size);
			Buffer.BlockCopy(internalBuffer.Buffer, internalBuffer.Offset, buffer, offset, num);
			internalBuffer.Offset += num;
			internalBuffer.Size -= num;
			return new ValueTuple<int, bool>(num, !internalBuffer.Complete && num < size);
		}

		// Token: 0x06000164 RID: 356 RVA: 0x000060C8 File Offset: 0x000042C8
		internal bool InternalWrite(byte[] buffer, int offset, int size)
		{
			bool flag;
			try
			{
				AsyncProtocolRequest asyncProtocolRequest;
				switch (this.operation)
				{
				case MobileAuthenticatedStream.Operation.Handshake:
				case MobileAuthenticatedStream.Operation.Renegotiate:
					asyncProtocolRequest = this.asyncHandshakeRequest;
					goto IL_0057;
				case MobileAuthenticatedStream.Operation.Read:
					asyncProtocolRequest = this.asyncReadRequest;
					if (this.xobileTlsContext.PendingRenegotiation())
					{
						goto IL_0057;
					}
					goto IL_0057;
				case MobileAuthenticatedStream.Operation.Write:
				case MobileAuthenticatedStream.Operation.Close:
					asyncProtocolRequest = this.asyncWriteRequest;
					goto IL_0057;
				}
				throw MobileAuthenticatedStream.GetInternalError();
				IL_0057:
				if (asyncProtocolRequest == null && this.operation != MobileAuthenticatedStream.Operation.Close)
				{
					throw MobileAuthenticatedStream.GetInternalError();
				}
				flag = this.InternalWrite(asyncProtocolRequest, this.writeBuffer, buffer, offset, size);
			}
			catch (Exception ex)
			{
				this.SetException(MobileAuthenticatedStream.GetIOException(ex, "InternalWrite() failed"));
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000165 RID: 357 RVA: 0x0000617C File Offset: 0x0000437C
		private bool InternalWrite(AsyncProtocolRequest asyncRequest, BufferOffsetSize2 internalBuffer, byte[] buffer, int offset, int size)
		{
			if (asyncRequest == null)
			{
				if (this.lastException != null)
				{
					return false;
				}
				if (Interlocked.Exchange(ref this.closeRequested, 1) == 0)
				{
					internalBuffer.Reset();
				}
				else if (internalBuffer.Remaining == 0)
				{
					throw new InvalidOperationException();
				}
			}
			internalBuffer.AppendData(buffer, offset, size);
			if (asyncRequest != null)
			{
				asyncRequest.RequestWrite();
			}
			return true;
		}

		// Token: 0x06000166 RID: 358 RVA: 0x000061D0 File Offset: 0x000043D0
		internal async Task<int> InnerRead(bool sync, int requestedSize, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			int len = Math.Min(this.readBuffer.Remaining, requestedSize);
			if (len == 0)
			{
				throw new InvalidOperationException();
			}
			Task<int> task;
			if (sync)
			{
				task = Task.Run<int>(() => this.InnerStream.Read(this.readBuffer.Buffer, this.readBuffer.EndOffset, len));
			}
			else
			{
				task = base.InnerStream.ReadAsync(this.readBuffer.Buffer, this.readBuffer.EndOffset, len, cancellationToken);
			}
			int num = await task.ConfigureAwait(false);
			if (num >= 0)
			{
				this.readBuffer.Size += num;
				this.readBuffer.TotalBytes += num;
			}
			if (num == 0)
			{
				this.readBuffer.Complete = true;
				if (this.readBuffer.TotalBytes > 0)
				{
					num = -1;
				}
			}
			return num;
		}

		// Token: 0x06000167 RID: 359 RVA: 0x0000622C File Offset: 0x0000442C
		internal async Task InnerWrite(bool sync, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			if (this.writeBuffer.Size != 0)
			{
				Task task;
				if (sync)
				{
					task = Task.Run(delegate
					{
						base.InnerStream.Write(this.writeBuffer.Buffer, this.writeBuffer.Offset, this.writeBuffer.Size);
					});
				}
				else
				{
					task = base.InnerStream.WriteAsync(this.writeBuffer.Buffer, this.writeBuffer.Offset, this.writeBuffer.Size);
				}
				await task.ConfigureAwait(false);
				this.writeBuffer.TotalBytes += this.writeBuffer.Size;
				BufferOffsetSize bufferOffsetSize = this.writeBuffer;
				BufferOffsetSize bufferOffsetSize2 = this.writeBuffer;
				int num = 0;
				bufferOffsetSize2.Size = num;
				bufferOffsetSize.Offset = num;
			}
		}

		// Token: 0x06000168 RID: 360 RVA: 0x00006280 File Offset: 0x00004480
		internal AsyncOperationStatus ProcessHandshake(AsyncOperationStatus status, bool renegotiate)
		{
			object obj = this.ioLock;
			AsyncOperationStatus asyncOperationStatus;
			lock (obj)
			{
				switch (this.operation)
				{
				case MobileAuthenticatedStream.Operation.None:
					if (renegotiate)
					{
						throw MobileAuthenticatedStream.GetInternalError();
					}
					this.operation = MobileAuthenticatedStream.Operation.Handshake;
					break;
				case MobileAuthenticatedStream.Operation.Handshake:
				case MobileAuthenticatedStream.Operation.Renegotiate:
					break;
				case MobileAuthenticatedStream.Operation.Authenticated:
					if (!renegotiate)
					{
						throw MobileAuthenticatedStream.GetInternalError();
					}
					this.operation = MobileAuthenticatedStream.Operation.Renegotiate;
					break;
				default:
					throw MobileAuthenticatedStream.GetInternalError();
				}
				switch (status)
				{
				case AsyncOperationStatus.Initialize:
					if (renegotiate)
					{
						this.xobileTlsContext.Renegotiate();
					}
					else
					{
						this.xobileTlsContext.StartHandshake();
					}
					asyncOperationStatus = AsyncOperationStatus.Continue;
					break;
				case AsyncOperationStatus.Continue:
				{
					AsyncOperationStatus asyncOperationStatus2 = AsyncOperationStatus.Continue;
					try
					{
						if (this.xobileTlsContext.ProcessHandshake())
						{
							this.xobileTlsContext.FinishHandshake();
							this.operation = MobileAuthenticatedStream.Operation.Authenticated;
							asyncOperationStatus2 = AsyncOperationStatus.Complete;
						}
					}
					catch (Exception ex)
					{
						this.SetException(MobileAuthenticatedStream.GetSSPIException(ex));
						base.Dispose();
						throw;
					}
					if (this.lastException != null)
					{
						this.lastException.Throw();
					}
					asyncOperationStatus = asyncOperationStatus2;
					break;
				}
				case AsyncOperationStatus.ReadDone:
					throw new IOException("Authentication failed because the remote party has closed the transport stream.");
				default:
					throw new InvalidOperationException();
				}
			}
			return asyncOperationStatus;
		}

		// Token: 0x06000169 RID: 361 RVA: 0x000063AC File Offset: 0x000045AC
		[return: TupleElementNames(new string[] { "ret", "wantMore" })]
		internal ValueTuple<int, bool> ProcessRead(BufferOffsetSize userBuffer)
		{
			object obj = this.ioLock;
			ValueTuple<int, bool> valueTuple2;
			lock (obj)
			{
				if (this.operation != MobileAuthenticatedStream.Operation.Authenticated)
				{
					throw MobileAuthenticatedStream.GetInternalError();
				}
				this.operation = MobileAuthenticatedStream.Operation.Read;
				ValueTuple<int, bool> valueTuple = this.xobileTlsContext.Read(userBuffer.Buffer, userBuffer.Offset, userBuffer.Size);
				if (this.lastException != null)
				{
					this.lastException.Throw();
				}
				this.operation = MobileAuthenticatedStream.Operation.Authenticated;
				valueTuple2 = valueTuple;
			}
			return valueTuple2;
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00006438 File Offset: 0x00004638
		[return: TupleElementNames(new string[] { "ret", "wantMore" })]
		internal ValueTuple<int, bool> ProcessWrite(BufferOffsetSize userBuffer)
		{
			object obj = this.ioLock;
			ValueTuple<int, bool> valueTuple2;
			lock (obj)
			{
				if (this.operation != MobileAuthenticatedStream.Operation.Authenticated)
				{
					throw MobileAuthenticatedStream.GetInternalError();
				}
				this.operation = MobileAuthenticatedStream.Operation.Write;
				ValueTuple<int, bool> valueTuple = this.xobileTlsContext.Write(userBuffer.Buffer, userBuffer.Offset, userBuffer.Size);
				if (this.lastException != null)
				{
					this.lastException.Throw();
				}
				this.operation = MobileAuthenticatedStream.Operation.Authenticated;
				valueTuple2 = valueTuple;
			}
			return valueTuple2;
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600016B RID: 363 RVA: 0x000064C4 File Offset: 0x000046C4
		public override bool IsAuthenticated
		{
			get
			{
				object obj = this.ioLock;
				bool flag2;
				lock (obj)
				{
					flag2 = this.xobileTlsContext != null && this.lastException == null && this.xobileTlsContext.IsAuthenticated;
				}
				return flag2;
			}
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00006520 File Offset: 0x00004720
		protected override void Dispose(bool disposing)
		{
			try
			{
				object obj = this.ioLock;
				lock (obj)
				{
					this.SetException(new ObjectDisposedException("MobileAuthenticatedStream"));
					if (this.xobileTlsContext != null)
					{
						this.xobileTlsContext.Dispose();
						this.xobileTlsContext = null;
					}
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x0600016D RID: 365 RVA: 0x0000659C File Offset: 0x0000479C
		public override void Flush()
		{
			base.InnerStream.Flush();
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600016E RID: 366 RVA: 0x000065AC File Offset: 0x000047AC
		public X509Certificate LocalCertificate
		{
			get
			{
				object obj = this.ioLock;
				X509Certificate internalLocalCertificate;
				lock (obj)
				{
					this.CheckThrow(true, false);
					internalLocalCertificate = this.InternalLocalCertificate;
				}
				return internalLocalCertificate;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600016F RID: 367 RVA: 0x000065F8 File Offset: 0x000047F8
		public X509Certificate InternalLocalCertificate
		{
			get
			{
				object obj = this.ioLock;
				X509Certificate x509Certificate;
				lock (obj)
				{
					this.CheckThrow(false, false);
					if (this.xobileTlsContext == null)
					{
						x509Certificate = null;
					}
					else
					{
						x509Certificate = (this.xobileTlsContext.IsServer ? this.xobileTlsContext.LocalServerCertificate : this.xobileTlsContext.LocalClientCertificate);
					}
				}
				return x509Certificate;
			}
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00003132 File Offset: 0x00001332
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00006670 File Offset: 0x00004870
		public override void SetLength(long value)
		{
			base.InnerStream.SetLength(value);
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000172 RID: 370 RVA: 0x0000667E File Offset: 0x0000487E
		public override bool CanRead
		{
			get
			{
				return this.IsAuthenticated && base.InnerStream.CanRead;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000173 RID: 371 RVA: 0x00006695 File Offset: 0x00004895
		public override bool CanTimeout
		{
			get
			{
				return base.InnerStream.CanTimeout;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000174 RID: 372 RVA: 0x000066A2 File Offset: 0x000048A2
		public override bool CanWrite
		{
			get
			{
				return (this.IsAuthenticated & base.InnerStream.CanWrite) && !this.shutdown;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000175 RID: 373 RVA: 0x000028AE File Offset: 0x00000AAE
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000176 RID: 374 RVA: 0x000066C3 File Offset: 0x000048C3
		public override long Length
		{
			get
			{
				return base.InnerStream.Length;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000177 RID: 375 RVA: 0x000066D0 File Offset: 0x000048D0
		// (set) Token: 0x06000178 RID: 376 RVA: 0x00003132 File Offset: 0x00001332
		public override long Position
		{
			get
			{
				return base.InnerStream.Position;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000179 RID: 377 RVA: 0x000066DD File Offset: 0x000048DD
		// (set) Token: 0x0600017A RID: 378 RVA: 0x000066EA File Offset: 0x000048EA
		public override int ReadTimeout
		{
			get
			{
				return base.InnerStream.ReadTimeout;
			}
			set
			{
				base.InnerStream.ReadTimeout = value;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x0600017B RID: 379 RVA: 0x000066F8 File Offset: 0x000048F8
		// (set) Token: 0x0600017C RID: 380 RVA: 0x00006705 File Offset: 0x00004905
		public override int WriteTimeout
		{
			get
			{
				return base.InnerStream.WriteTimeout;
			}
			set
			{
				base.InnerStream.WriteTimeout = value;
			}
		}

		// Token: 0x04000114 RID: 276
		private MobileTlsContext xobileTlsContext;

		// Token: 0x04000115 RID: 277
		private ExceptionDispatchInfo lastException;

		// Token: 0x04000116 RID: 278
		private AsyncProtocolRequest asyncHandshakeRequest;

		// Token: 0x04000117 RID: 279
		private AsyncProtocolRequest asyncReadRequest;

		// Token: 0x04000118 RID: 280
		private AsyncProtocolRequest asyncWriteRequest;

		// Token: 0x04000119 RID: 281
		private BufferOffsetSize2 readBuffer;

		// Token: 0x0400011A RID: 282
		private BufferOffsetSize2 writeBuffer;

		// Token: 0x0400011B RID: 283
		private object ioLock = new object();

		// Token: 0x0400011C RID: 284
		private int closeRequested;

		// Token: 0x0400011D RID: 285
		private bool shutdown;

		// Token: 0x0400011E RID: 286
		private MobileAuthenticatedStream.Operation operation;

		// Token: 0x0400011F RID: 287
		private static int uniqueNameInteger = 123;

		// Token: 0x04000124 RID: 292
		private static int nextId;

		// Token: 0x04000125 RID: 293
		internal readonly int ID = ++MobileAuthenticatedStream.nextId;

		// Token: 0x0200006D RID: 109
		private enum Operation
		{
			// Token: 0x04000127 RID: 295
			None,
			// Token: 0x04000128 RID: 296
			Handshake,
			// Token: 0x04000129 RID: 297
			Authenticated,
			// Token: 0x0400012A RID: 298
			Renegotiate,
			// Token: 0x0400012B RID: 299
			Read,
			// Token: 0x0400012C RID: 300
			Write,
			// Token: 0x0400012D RID: 301
			Close
		}

		// Token: 0x0200006E RID: 110
		private enum OperationType
		{
			// Token: 0x0400012F RID: 303
			Read,
			// Token: 0x04000130 RID: 304
			Write,
			// Token: 0x04000131 RID: 305
			Renegotiate,
			// Token: 0x04000132 RID: 306
			Shutdown
		}
	}
}
