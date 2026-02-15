using System;
using System.Runtime.CompilerServices;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using Mono.Security.Interface;

namespace Mono.Net.Security
{
	// Token: 0x02000074 RID: 116
	internal abstract class MobileTlsContext : IDisposable
	{
		// Token: 0x06000189 RID: 393 RVA: 0x00007118 File Offset: 0x00005318
		protected MobileTlsContext(MobileAuthenticatedStream parent, MonoSslAuthenticationOptions options)
		{
			this.Parent = parent;
			this.Options = options;
			this.IsServer = options.ServerMode;
			this.EnabledProtocols = options.EnabledSslProtocols;
			if (options.ServerMode)
			{
				this.LocalServerCertificate = options.ServerCertificate;
				this.AskForClientCertificate = options.ClientCertificateRequired;
			}
			else
			{
				this.ClientCertificates = options.ClientCertificates;
				this.TargetHost = options.TargetHost;
				this.ServerName = options.TargetHost;
				if (!string.IsNullOrEmpty(this.ServerName))
				{
					int num = this.ServerName.IndexOf(':');
					if (num > 0)
					{
						this.ServerName = this.ServerName.Substring(0, num);
					}
				}
			}
			this.certificateValidator = ChainValidationHelper.GetInternalValidator(parent.SslStream, parent.Provider, parent.Settings);
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x0600018A RID: 394 RVA: 0x000071E6 File Offset: 0x000053E6
		internal MonoSslAuthenticationOptions Options { get; }

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x0600018B RID: 395 RVA: 0x000071EE File Offset: 0x000053EE
		internal MobileAuthenticatedStream Parent { get; }

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600018C RID: 396 RVA: 0x000071F6 File Offset: 0x000053F6
		public MonoTlsSettings Settings
		{
			get
			{
				return this.Parent.Settings;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600018D RID: 397
		public abstract bool IsAuthenticated { get; }

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600018E RID: 398 RVA: 0x00007203 File Offset: 0x00005403
		public bool IsServer { get; }

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x0600018F RID: 399 RVA: 0x0000720B File Offset: 0x0000540B
		internal string TargetHost { get; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000190 RID: 400 RVA: 0x00007213 File Offset: 0x00005413
		protected string ServerName { get; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000191 RID: 401 RVA: 0x0000721B File Offset: 0x0000541B
		protected bool AskForClientCertificate { get; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000192 RID: 402 RVA: 0x00007223 File Offset: 0x00005423
		protected SslProtocols EnabledProtocols { get; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000193 RID: 403 RVA: 0x0000722B File Offset: 0x0000542B
		protected X509CertificateCollection ClientCertificates { get; }

		// Token: 0x06000194 RID: 404 RVA: 0x00007234 File Offset: 0x00005434
		protected void GetProtocolVersions(out TlsProtocolCode? min, out TlsProtocolCode? max)
		{
			if ((this.EnabledProtocols & SslProtocols.Tls) != SslProtocols.None)
			{
				min = new TlsProtocolCode?(TlsProtocolCode.Tls10);
			}
			else if ((this.EnabledProtocols & SslProtocols.Tls11) != SslProtocols.None)
			{
				min = new TlsProtocolCode?(TlsProtocolCode.Tls11);
			}
			else if ((this.EnabledProtocols & SslProtocols.Tls12) != SslProtocols.None)
			{
				min = new TlsProtocolCode?(TlsProtocolCode.Tls12);
			}
			else
			{
				min = null;
			}
			if ((this.EnabledProtocols & SslProtocols.Tls12) != SslProtocols.None)
			{
				max = new TlsProtocolCode?(TlsProtocolCode.Tls12);
				return;
			}
			if ((this.EnabledProtocols & SslProtocols.Tls11) != SslProtocols.None)
			{
				max = new TlsProtocolCode?(TlsProtocolCode.Tls11);
				return;
			}
			if ((this.EnabledProtocols & SslProtocols.Tls) != SslProtocols.None)
			{
				max = new TlsProtocolCode?(TlsProtocolCode.Tls10);
				return;
			}
			max = null;
		}

		// Token: 0x06000195 RID: 405
		public abstract void StartHandshake();

		// Token: 0x06000196 RID: 406
		public abstract bool ProcessHandshake();

		// Token: 0x06000197 RID: 407
		public abstract void FinishHandshake();

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000198 RID: 408 RVA: 0x0000730C File Offset: 0x0000550C
		// (set) Token: 0x06000199 RID: 409 RVA: 0x00007314 File Offset: 0x00005514
		internal X509Certificate LocalServerCertificate { get; private set; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600019A RID: 410
		internal abstract X509Certificate LocalClientCertificate { get; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600019B RID: 411
		public abstract X509Certificate2 RemoteCertificate { get; }

		// Token: 0x0600019C RID: 412
		public abstract void Flush();

		// Token: 0x0600019D RID: 413
		[return: TupleElementNames(new string[] { "ret", "wantMore" })]
		public abstract ValueTuple<int, bool> Read(byte[] buffer, int offset, int count);

		// Token: 0x0600019E RID: 414
		[return: TupleElementNames(new string[] { "ret", "wantMore" })]
		public abstract ValueTuple<int, bool> Write(byte[] buffer, int offset, int count);

		// Token: 0x0600019F RID: 415
		public abstract void Shutdown();

		// Token: 0x060001A0 RID: 416
		public abstract bool PendingRenegotiation();

		// Token: 0x060001A1 RID: 417 RVA: 0x00007320 File Offset: 0x00005520
		protected bool ValidateCertificate(X509Certificate2 leaf, X509Chain chain)
		{
			ValidationResult validationResult = this.certificateValidator.ValidateCertificate(this.TargetHost, this.IsServer, leaf, chain);
			return validationResult != null && validationResult.Trusted && !validationResult.UserDenied;
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x00007360 File Offset: 0x00005560
		protected X509Certificate SelectServerCertificate(string serverIdentity)
		{
			if (this.Options.ServerCertSelectionDelegate != null)
			{
				this.LocalServerCertificate = this.Options.ServerCertSelectionDelegate(serverIdentity);
				if (this.LocalServerCertificate == null)
				{
					throw new AuthenticationException("The server mode SSL must use a certificate with the associated private key.");
				}
			}
			else if (this.Settings.ClientCertificateSelectionCallback != null)
			{
				X509CertificateCollection x509CertificateCollection = new X509CertificateCollection();
				x509CertificateCollection.Add(this.Options.ServerCertificate);
				this.LocalServerCertificate = this.Settings.ClientCertificateSelectionCallback(string.Empty, x509CertificateCollection, null, Array.Empty<string>());
			}
			else
			{
				this.LocalServerCertificate = this.Options.ServerCertificate;
			}
			if (this.LocalServerCertificate == null)
			{
				throw new NotSupportedException("The server mode SSL must use a certificate with the associated private key.");
			}
			return this.LocalServerCertificate;
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x00007418 File Offset: 0x00005618
		protected X509Certificate SelectClientCertificate(string[] acceptableIssuers)
		{
			if (this.Settings.DisallowUnauthenticatedCertificateRequest && !this.IsAuthenticated)
			{
				return null;
			}
			if (this.RemoteCertificate == null)
			{
				throw new TlsException(AlertDescription.InternalError, "Cannot request client certificate before receiving one from the server.");
			}
			X509Certificate x509Certificate;
			if (this.certificateValidator.SelectClientCertificate(this.TargetHost, this.ClientCertificates, this.IsAuthenticated ? this.RemoteCertificate : null, acceptableIssuers, out x509Certificate))
			{
				return x509Certificate;
			}
			if (this.ClientCertificates == null || this.ClientCertificates.Count == 0)
			{
				return null;
			}
			if (acceptableIssuers == null || acceptableIssuers.Length == 0)
			{
				return this.ClientCertificates[0];
			}
			for (int i = 0; i < this.ClientCertificates.Count; i++)
			{
				X509Certificate2 x509Certificate2 = this.ClientCertificates[i] as X509Certificate2;
				if (x509Certificate2 != null)
				{
					X509Chain x509Chain = null;
					try
					{
						x509Chain = new X509Chain();
						x509Chain.ChainPolicy.RevocationMode = X509RevocationMode.NoCheck;
						x509Chain.ChainPolicy.VerificationFlags = X509VerificationFlags.IgnoreInvalidName;
						x509Chain.Build(x509Certificate2);
						if (x509Chain.ChainElements.Count != 0)
						{
							for (int j = 0; j < x509Chain.ChainElements.Count; j++)
							{
								string issuer = x509Chain.ChainElements[j].Certificate.Issuer;
								if (Array.IndexOf<string>(acceptableIssuers, issuer) != -1)
								{
									return x509Certificate2;
								}
							}
						}
					}
					catch
					{
					}
					finally
					{
						if (x509Chain != null)
						{
							x509Chain.Reset();
						}
					}
				}
			}
			return null;
		}

		// Token: 0x060001A4 RID: 420
		public abstract void Renegotiate();

		// Token: 0x060001A5 RID: 421 RVA: 0x0000758C File Offset: 0x0000578C
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x00002FA0 File Offset: 0x000011A0
		protected virtual void Dispose(bool disposing)
		{
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x0000759C File Offset: 0x0000579C
		~MobileTlsContext()
		{
			this.Dispose(false);
		}

		// Token: 0x04000150 RID: 336
		private ChainValidationHelper certificateValidator;
	}
}
