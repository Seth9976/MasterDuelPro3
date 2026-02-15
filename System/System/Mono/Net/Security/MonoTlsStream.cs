using System;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;
using Mono.Net.Security.Private;
using Mono.Security.Interface;

namespace Mono.Net.Security
{
	// Token: 0x0200007A RID: 122
	internal class MonoTlsStream : IDisposable
	{
		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060001E5 RID: 485 RVA: 0x00007AF8 File Offset: 0x00005CF8
		internal HttpWebRequest Request
		{
			get
			{
				return this.request;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060001E6 RID: 486 RVA: 0x00007B00 File Offset: 0x00005D00
		internal WebExceptionStatus ExceptionStatus
		{
			get
			{
				return this.status;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060001E7 RID: 487 RVA: 0x00007B08 File Offset: 0x00005D08
		// (set) Token: 0x060001E8 RID: 488 RVA: 0x00007B10 File Offset: 0x00005D10
		internal bool CertificateValidationFailed { get; set; }

		// Token: 0x060001E9 RID: 489 RVA: 0x00007B1C File Offset: 0x00005D1C
		public MonoTlsStream(HttpWebRequest request, NetworkStream networkStream)
		{
			this.request = request;
			this.networkStream = networkStream;
			this.settings = request.TlsSettings;
			if (this.settings == null)
			{
				this.settings = MonoTlsSettings.CopyDefaultSettings();
			}
			if (this.settings.RemoteCertificateValidationCallback == null)
			{
				this.settings.RemoteCertificateValidationCallback = CallbackHelpers.PublicToMono(request.ServerCertificateValidationCallback);
			}
			this.provider = request.TlsProvider ?? MonoTlsProviderFactory.GetProviderInternal();
			this.status = WebExceptionStatus.SecureChannelFailure;
			ChainValidationHelper.Create(this.provider, ref this.settings, this);
		}

		// Token: 0x060001EA RID: 490 RVA: 0x00007BBC File Offset: 0x00005DBC
		internal async Task<Stream> CreateStream(WebConnectionTunnel tunnel, CancellationToken cancellationToken)
		{
			Socket socket = this.networkStream.InternalSocket;
			this.sslStream = new SslStream(this.networkStream, false, this.provider, this.settings);
			try
			{
				string text = this.request.Host;
				if (!string.IsNullOrEmpty(text))
				{
					int num = text.IndexOf(':');
					if (num > 0)
					{
						text = text.Substring(0, num);
					}
				}
				await this.sslStream.AuthenticateAsClientAsync(text, this.request.ClientCertificates, (SslProtocols)ServicePointManager.SecurityProtocol, ServicePointManager.CheckCertificateRevocationList).ConfigureAwait(false);
				this.status = WebExceptionStatus.Success;
				this.request.ServicePoint.UpdateClientCertificate(this.sslStream.LocalCertificate);
			}
			catch (Exception)
			{
				if (socket.CleanedUp)
				{
					this.status = WebExceptionStatus.RequestCanceled;
				}
				else if (this.CertificateValidationFailed)
				{
					this.status = WebExceptionStatus.TrustFailure;
				}
				else
				{
					this.status = WebExceptionStatus.SecureChannelFailure;
				}
				this.request.ServicePoint.UpdateClientCertificate(null);
				this.CloseSslStream();
				throw;
			}
			try
			{
				if (((tunnel != null) ? tunnel.Data : null) != null)
				{
					await this.sslStream.WriteAsync(tunnel.Data, 0, tunnel.Data.Length, cancellationToken).ConfigureAwait(false);
				}
			}
			catch
			{
				this.status = WebExceptionStatus.SendFailure;
				this.CloseSslStream();
				throw;
			}
			return this.sslStream;
		}

		// Token: 0x060001EB RID: 491 RVA: 0x00007C0F File Offset: 0x00005E0F
		public void Dispose()
		{
			this.CloseSslStream();
		}

		// Token: 0x060001EC RID: 492 RVA: 0x00007C18 File Offset: 0x00005E18
		private void CloseSslStream()
		{
			object obj = this.sslStreamLock;
			lock (obj)
			{
				if (this.sslStream != null)
				{
					this.sslStream.Dispose();
					this.sslStream = null;
				}
			}
		}

		// Token: 0x04000165 RID: 357
		private readonly MobileTlsProvider provider;

		// Token: 0x04000166 RID: 358
		private readonly NetworkStream networkStream;

		// Token: 0x04000167 RID: 359
		private readonly HttpWebRequest request;

		// Token: 0x04000168 RID: 360
		private readonly MonoTlsSettings settings;

		// Token: 0x04000169 RID: 361
		private SslStream sslStream;

		// Token: 0x0400016A RID: 362
		private readonly object sslStreamLock = new object();

		// Token: 0x0400016B RID: 363
		private WebExceptionStatus status;
	}
}
