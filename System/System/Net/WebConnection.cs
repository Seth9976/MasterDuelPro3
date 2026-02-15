using System;
using System.IO;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Mono.Net.Security;

namespace System.Net
{
	// Token: 0x02000439 RID: 1081
	internal class WebConnection : IDisposable
	{
		// Token: 0x17000603 RID: 1539
		// (get) Token: 0x06001B44 RID: 6980 RVA: 0x00076773 File Offset: 0x00074973
		public ServicePoint ServicePoint { get; }

		// Token: 0x06001B45 RID: 6981 RVA: 0x0007677B File Offset: 0x0007497B
		public WebConnection(ServicePoint sPoint)
		{
			this.ServicePoint = sPoint;
		}

		// Token: 0x06001B46 RID: 6982 RVA: 0x0007678A File Offset: 0x0007498A
		private bool CanReuse()
		{
			return !this.socket.Poll(0, SelectMode.SelectRead);
		}

		// Token: 0x06001B47 RID: 6983 RVA: 0x0007679C File Offset: 0x0007499C
		private bool CheckReusable()
		{
			if (this.socket != null && this.socket.Connected)
			{
				try
				{
					if (this.CanReuse())
					{
						return true;
					}
				}
				catch
				{
				}
				return false;
			}
			return false;
		}

		// Token: 0x06001B48 RID: 6984 RVA: 0x000767E4 File Offset: 0x000749E4
		private async Task Connect(WebOperation operation, CancellationToken cancellationToken)
		{
			IPHostEntry hostEntry = this.ServicePoint.HostEntry;
			if (hostEntry == null || hostEntry.AddressList.Length == 0)
			{
				throw WebConnection.GetException(this.ServicePoint.UsesProxy ? WebExceptionStatus.ProxyNameResolutionFailure : WebExceptionStatus.NameResolutionFailure, null);
			}
			Exception connectException = null;
			foreach (IPAddress ipaddress in hostEntry.AddressList)
			{
				operation.ThrowIfDisposed(cancellationToken);
				try
				{
					this.socket = new Socket(ipaddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
				}
				catch (Exception ex)
				{
					throw WebConnection.GetException(WebExceptionStatus.ConnectFailure, ex);
				}
				IPEndPoint ipendPoint = new IPEndPoint(ipaddress, this.ServicePoint.Address.Port);
				this.socket.NoDelay = !this.ServicePoint.UseNagleAlgorithm;
				try
				{
					this.ServicePoint.KeepAliveSetup(this.socket);
				}
				catch
				{
				}
				if (!this.ServicePoint.CallEndPointDelegate(this.socket, ipendPoint))
				{
					Socket socket = Interlocked.Exchange<Socket>(ref this.socket, null);
					if (socket != null)
					{
						socket.Close();
					}
				}
				else
				{
					try
					{
						operation.ThrowIfDisposed(cancellationToken);
						await Task.Factory.FromAsync<IPEndPoint>((IPEndPoint targetEndPoint, AsyncCallback callback, object state) => ((Socket)state).BeginConnect(targetEndPoint, callback, state), delegate(IAsyncResult asyncResult)
						{
							((Socket)asyncResult.AsyncState).EndConnect(asyncResult);
						}, ipendPoint, this.socket).ConfigureAwait(false);
					}
					catch (ObjectDisposedException)
					{
						throw;
					}
					catch (Exception ex2)
					{
						Socket socket2 = Interlocked.Exchange<Socket>(ref this.socket, null);
						if (socket2 != null)
						{
							socket2.Close();
						}
						connectException = WebConnection.GetException(WebExceptionStatus.ConnectFailure, ex2);
						goto IL_0220;
					}
					if (this.socket != null)
					{
						return;
					}
				}
				IL_0220:;
			}
			IPAddress[] array = null;
			if (connectException == null)
			{
				connectException = WebConnection.GetException(WebExceptionStatus.ConnectFailure, null);
			}
			throw connectException;
		}

		// Token: 0x06001B49 RID: 6985 RVA: 0x00076838 File Offset: 0x00074A38
		private async Task<bool> CreateStream(WebOperation operation, bool reused, CancellationToken cancellationToken)
		{
			bool flag;
			try
			{
				NetworkStream stream = new NetworkStream(this.socket, false);
				if (operation.Request.Address.Scheme == Uri.UriSchemeHttps)
				{
					if (!reused || this.monoTlsStream == null)
					{
						if (this.ServicePoint.UseConnect)
						{
							if (this.tunnel == null)
							{
								this.tunnel = new WebConnectionTunnel(operation.Request, this.ServicePoint.Address);
							}
							await this.tunnel.Initialize(stream, cancellationToken).ConfigureAwait(false);
							if (!this.tunnel.Success)
							{
								return false;
							}
						}
						this.monoTlsStream = new MonoTlsStream(operation.Request, stream);
						this.networkStream = await this.monoTlsStream.CreateStream(this.tunnel, cancellationToken).ConfigureAwait(false);
					}
					flag = true;
				}
				else
				{
					this.networkStream = stream;
					flag = true;
				}
			}
			catch (Exception ex)
			{
				ex = HttpWebRequest.FlattenException(ex);
				if (operation.Aborted || this.monoTlsStream == null)
				{
					throw WebConnection.GetException(WebExceptionStatus.ConnectFailure, ex);
				}
				throw WebConnection.GetException(this.monoTlsStream.ExceptionStatus, ex);
			}
			finally
			{
			}
			return flag;
		}

		// Token: 0x06001B4A RID: 6986 RVA: 0x00076894 File Offset: 0x00074A94
		internal async Task<WebRequestStream> InitConnection(WebOperation operation, CancellationToken cancellationToken)
		{
			bool flag = true;
			for (;;)
			{
				operation.ThrowIfClosedOrDisposed(cancellationToken);
				bool reused = this.CheckReusable();
				if (!reused)
				{
					this.CloseSocket();
					if (flag)
					{
						this.Reset();
					}
					try
					{
						await this.Connect(operation, cancellationToken).ConfigureAwait(false);
					}
					catch (Exception)
					{
						throw;
					}
				}
				ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter = this.CreateStream(operation, reused, cancellationToken).ConfigureAwait(false).GetAwaiter();
				if (!configuredTaskAwaiter.IsCompleted)
				{
					await configuredTaskAwaiter;
					ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
					configuredTaskAwaiter = configuredTaskAwaiter2;
					configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
				}
				if (configuredTaskAwaiter.GetResult())
				{
					goto IL_0180;
				}
				WebConnectionTunnel webConnectionTunnel = this.tunnel;
				if (((webConnectionTunnel != null) ? webConnectionTunnel.Challenge : null) == null)
				{
					break;
				}
				if (this.tunnel.CloseConnection)
				{
					this.CloseSocket();
				}
				flag = false;
			}
			throw WebConnection.GetException(WebExceptionStatus.ProtocolError, null);
			IL_0180:
			this.networkStream.ReadTimeout = operation.Request.ReadWriteTimeout;
			return new WebRequestStream(this, operation, this.networkStream, this.tunnel);
		}

		// Token: 0x06001B4B RID: 6987 RVA: 0x000768E8 File Offset: 0x00074AE8
		internal static WebException GetException(WebExceptionStatus status, Exception error)
		{
			if (error == null)
			{
				return new WebException(string.Format("Error: {0}", status), status);
			}
			WebException ex = error as WebException;
			if (ex != null)
			{
				return ex;
			}
			return new WebException(string.Format("Error: {0} ({1})", status, error.Message), status, WebExceptionInternalStatus.RequestFatal, error);
		}

		// Token: 0x06001B4C RID: 6988 RVA: 0x0007693C File Offset: 0x00074B3C
		internal static bool ReadLine(byte[] buffer, ref int start, int max, ref string output)
		{
			bool flag = false;
			StringBuilder stringBuilder = new StringBuilder();
			int num = 0;
			while (start < max)
			{
				int num2 = start;
				start = num2 + 1;
				num = (int)buffer[num2];
				if (num == 10)
				{
					if (stringBuilder.Length > 0 && stringBuilder[stringBuilder.Length - 1] == '\r')
					{
						StringBuilder stringBuilder2 = stringBuilder;
						num2 = stringBuilder2.Length;
						stringBuilder2.Length = num2 - 1;
					}
					flag = false;
					break;
				}
				if (flag)
				{
					StringBuilder stringBuilder3 = stringBuilder;
					num2 = stringBuilder3.Length;
					stringBuilder3.Length = num2 - 1;
					break;
				}
				if (num == 13)
				{
					flag = true;
				}
				stringBuilder.Append((char)num);
			}
			if (num != 10 && num != 13)
			{
				return false;
			}
			if (stringBuilder.Length == 0)
			{
				output = null;
				return num == 10 || num == 13;
			}
			if (flag)
			{
				StringBuilder stringBuilder4 = stringBuilder;
				int num2 = stringBuilder4.Length;
				stringBuilder4.Length = num2 - 1;
			}
			output = stringBuilder.ToString();
			return true;
		}

		// Token: 0x06001B4D RID: 6989 RVA: 0x00076A00 File Offset: 0x00074C00
		internal bool CanReuseConnection(WebOperation operation)
		{
			bool flag2;
			lock (this)
			{
				if (this.Closed || this.currentOperation != null)
				{
					flag2 = false;
				}
				else if (!this.NtlmAuthenticated)
				{
					flag2 = true;
				}
				else
				{
					NetworkCredential ntlmCredential = this.NtlmCredential;
					HttpWebRequest request = operation.Request;
					ICredentials credentials = ((request.Proxy == null || request.Proxy.IsBypassed(request.RequestUri)) ? request.Credentials : request.Proxy.Credentials);
					NetworkCredential networkCredential = ((credentials != null) ? credentials.GetCredential(request.RequestUri, "NTLM") : null);
					if (ntlmCredential == null || networkCredential == null || ntlmCredential.Domain != networkCredential.Domain || ntlmCredential.UserName != networkCredential.UserName || ntlmCredential.Password != networkCredential.Password)
					{
						flag2 = false;
					}
					else
					{
						bool unsafeAuthenticatedConnectionSharing = request.UnsafeAuthenticatedConnectionSharing;
						bool unsafeAuthenticatedConnectionSharing2 = this.UnsafeAuthenticatedConnectionSharing;
						flag2 = unsafeAuthenticatedConnectionSharing && unsafeAuthenticatedConnectionSharing == unsafeAuthenticatedConnectionSharing2;
					}
				}
			}
			return flag2;
		}

		// Token: 0x06001B4E RID: 6990 RVA: 0x00076B28 File Offset: 0x00074D28
		private bool PrepareSharingNtlm(WebOperation operation)
		{
			if (operation == null || !this.NtlmAuthenticated)
			{
				return true;
			}
			bool flag = false;
			NetworkCredential ntlmCredential = this.NtlmCredential;
			HttpWebRequest request = operation.Request;
			ICredentials credentials = ((request.Proxy == null || request.Proxy.IsBypassed(request.RequestUri)) ? request.Credentials : request.Proxy.Credentials);
			NetworkCredential networkCredential = ((credentials != null) ? credentials.GetCredential(request.RequestUri, "NTLM") : null);
			if (ntlmCredential == null || networkCredential == null || ntlmCredential.Domain != networkCredential.Domain || ntlmCredential.UserName != networkCredential.UserName || ntlmCredential.Password != networkCredential.Password)
			{
				flag = true;
			}
			if (!flag)
			{
				bool unsafeAuthenticatedConnectionSharing = request.UnsafeAuthenticatedConnectionSharing;
				bool unsafeAuthenticatedConnectionSharing2 = this.UnsafeAuthenticatedConnectionSharing;
				flag = !unsafeAuthenticatedConnectionSharing || unsafeAuthenticatedConnectionSharing != unsafeAuthenticatedConnectionSharing2;
			}
			return flag;
		}

		// Token: 0x06001B4F RID: 6991 RVA: 0x00076C0C File Offset: 0x00074E0C
		private void Reset()
		{
			lock (this)
			{
				this.tunnel = null;
				this.ResetNtlm();
			}
		}

		// Token: 0x06001B50 RID: 6992 RVA: 0x00076C50 File Offset: 0x00074E50
		private void Close(bool reset)
		{
			lock (this)
			{
				this.CloseSocket();
				if (reset)
				{
					this.Reset();
				}
			}
		}

		// Token: 0x06001B51 RID: 6993 RVA: 0x00076C94 File Offset: 0x00074E94
		private void CloseSocket()
		{
			lock (this)
			{
				if (this.networkStream != null)
				{
					try
					{
						this.networkStream.Dispose();
					}
					catch
					{
					}
					this.networkStream = null;
				}
				if (this.monoTlsStream != null)
				{
					try
					{
						this.monoTlsStream.Dispose();
					}
					catch
					{
					}
					this.monoTlsStream = null;
				}
				if (this.socket != null)
				{
					try
					{
						this.socket.Dispose();
					}
					catch
					{
					}
					this.socket = null;
				}
				this.monoTlsStream = null;
			}
		}

		// Token: 0x17000604 RID: 1540
		// (get) Token: 0x06001B52 RID: 6994 RVA: 0x00076D54 File Offset: 0x00074F54
		public bool Closed
		{
			get
			{
				return this.disposed != 0;
			}
		}

		// Token: 0x17000605 RID: 1541
		// (get) Token: 0x06001B53 RID: 6995 RVA: 0x00076D5F File Offset: 0x00074F5F
		public DateTime IdleSince
		{
			get
			{
				return this.idleSince;
			}
		}

		// Token: 0x06001B54 RID: 6996 RVA: 0x00076D68 File Offset: 0x00074F68
		public bool StartOperation(WebOperation operation, bool reused)
		{
			lock (this)
			{
				if (this.Closed)
				{
					return false;
				}
				if (Interlocked.CompareExchange<WebOperation>(ref this.currentOperation, operation, null) != null)
				{
					return false;
				}
				this.idleSince = DateTime.UtcNow + TimeSpan.FromDays(3650.0);
				if (reused && !this.PrepareSharingNtlm(operation))
				{
					this.Close(true);
				}
				operation.RegisterRequest(this.ServicePoint, this);
			}
			operation.Run();
			return true;
		}

		// Token: 0x06001B55 RID: 6997 RVA: 0x00076E04 File Offset: 0x00075004
		public bool Continue(WebOperation next)
		{
			lock (this)
			{
				if (this.Closed)
				{
					return false;
				}
				if (this.socket == null || !this.socket.Connected || !this.PrepareSharingNtlm(next))
				{
					this.Close(true);
					return false;
				}
				this.currentOperation = next;
				if (next == null)
				{
					return true;
				}
				next.RegisterRequest(this.ServicePoint, this);
			}
			next.Run();
			return true;
		}

		// Token: 0x06001B56 RID: 6998 RVA: 0x00076E94 File Offset: 0x00075094
		private void Dispose(bool disposing)
		{
			if (Interlocked.CompareExchange(ref this.disposed, 1, 0) != 0)
			{
				return;
			}
			this.Close(true);
		}

		// Token: 0x06001B57 RID: 6999 RVA: 0x00076EAD File Offset: 0x000750AD
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06001B58 RID: 7000 RVA: 0x00076EB6 File Offset: 0x000750B6
		private void ResetNtlm()
		{
			this.ntlm_authenticated = false;
			this.ntlm_credentials = null;
			this.unsafe_sharing = false;
		}

		// Token: 0x17000606 RID: 1542
		// (get) Token: 0x06001B59 RID: 7001 RVA: 0x00076ECD File Offset: 0x000750CD
		// (set) Token: 0x06001B5A RID: 7002 RVA: 0x00076ED5 File Offset: 0x000750D5
		internal bool NtlmAuthenticated
		{
			get
			{
				return this.ntlm_authenticated;
			}
			set
			{
				this.ntlm_authenticated = value;
			}
		}

		// Token: 0x17000607 RID: 1543
		// (get) Token: 0x06001B5B RID: 7003 RVA: 0x00076EDE File Offset: 0x000750DE
		// (set) Token: 0x06001B5C RID: 7004 RVA: 0x00076EE6 File Offset: 0x000750E6
		internal NetworkCredential NtlmCredential
		{
			get
			{
				return this.ntlm_credentials;
			}
			set
			{
				this.ntlm_credentials = value;
			}
		}

		// Token: 0x17000608 RID: 1544
		// (get) Token: 0x06001B5D RID: 7005 RVA: 0x00076EEF File Offset: 0x000750EF
		// (set) Token: 0x06001B5E RID: 7006 RVA: 0x00076EF7 File Offset: 0x000750F7
		internal bool UnsafeAuthenticatedConnectionSharing
		{
			get
			{
				return this.unsafe_sharing;
			}
			set
			{
				this.unsafe_sharing = value;
			}
		}

		// Token: 0x040011D8 RID: 4568
		private NetworkCredential ntlm_credentials;

		// Token: 0x040011D9 RID: 4569
		private bool ntlm_authenticated;

		// Token: 0x040011DA RID: 4570
		private bool unsafe_sharing;

		// Token: 0x040011DB RID: 4571
		private Stream networkStream;

		// Token: 0x040011DC RID: 4572
		private Socket socket;

		// Token: 0x040011DD RID: 4573
		private MonoTlsStream monoTlsStream;

		// Token: 0x040011DE RID: 4574
		private WebConnectionTunnel tunnel;

		// Token: 0x040011DF RID: 4575
		private int disposed;

		// Token: 0x040011E1 RID: 4577
		private DateTime idleSince;

		// Token: 0x040011E2 RID: 4578
		private WebOperation currentOperation;
	}
}
