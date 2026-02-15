using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Win32.SafeHandles;
using Mono.Net.Security;
using Mono.Security.Interface;

namespace Mono.Btls
{
	// Token: 0x020000A0 RID: 160
	internal class MonoBtlsContext : MobileTlsContext, IMonoBtlsBioMono
	{
		// Token: 0x0600027C RID: 636 RVA: 0x00009D98 File Offset: 0x00007F98
		public MonoBtlsContext(MobileAuthenticatedStream parent, MonoSslAuthenticationOptions options)
			: base(parent, options)
		{
			if (base.IsServer && base.LocalServerCertificate != null)
			{
				this.nativeServerCertificate = MonoBtlsContext.GetPrivateCertificate(base.LocalServerCertificate);
			}
		}

		// Token: 0x0600027D RID: 637 RVA: 0x00009DC4 File Offset: 0x00007FC4
		private static X509CertificateImplBtls GetPrivateCertificate(X509Certificate certificate)
		{
			X509CertificateImplBtls x509CertificateImplBtls = certificate.Impl as X509CertificateImplBtls;
			if (x509CertificateImplBtls != null)
			{
				return (X509CertificateImplBtls)x509CertificateImplBtls.Clone();
			}
			string text = Guid.NewGuid().ToString();
			X509CertificateImplBtls x509CertificateImplBtls2;
			using (SafePasswordHandle safePasswordHandle = new SafePasswordHandle(text))
			{
				x509CertificateImplBtls2 = new X509CertificateImplBtls(certificate.Export(X509ContentType.Pfx, text), safePasswordHandle, X509KeyStorageFlags.DefaultKeySet);
			}
			return x509CertificateImplBtls2;
		}

		// Token: 0x0600027E RID: 638 RVA: 0x00009E38 File Offset: 0x00008038
		private int VerifyCallback(MonoBtlsX509StoreCtx storeCtx)
		{
			int num;
			using (X509ChainImplBtls x509ChainImplBtls = new X509ChainImplBtls(storeCtx))
			{
				using (X509Chain x509Chain = new X509Chain(x509ChainImplBtls))
				{
					X509Certificate2 certificate = x509Chain.ChainElements[0].Certificate;
					bool flag = base.ValidateCertificate(certificate, x509Chain);
					this.certificateValidated = true;
					num = (flag ? 1 : 0);
				}
			}
			return num;
		}

		// Token: 0x0600027F RID: 639 RVA: 0x00009EB0 File Offset: 0x000080B0
		private int SelectCallback(string[] acceptableIssuers)
		{
			if (this.nativeClientCertificate != null)
			{
				return 1;
			}
			this.GetPeerCertificate();
			X509Certificate x509Certificate = base.SelectClientCertificate(acceptableIssuers);
			if (x509Certificate == null)
			{
				return 1;
			}
			this.nativeClientCertificate = MonoBtlsContext.GetPrivateCertificate(x509Certificate);
			this.clientCertificate = new X509Certificate(this.nativeClientCertificate);
			this.SetPrivateCertificate(this.nativeClientCertificate);
			return 1;
		}

		// Token: 0x06000280 RID: 640 RVA: 0x00009F04 File Offset: 0x00008104
		private int ServerNameCallback()
		{
			string serverName = this.ssl.GetServerName();
			X509Certificate x509Certificate = base.SelectServerCertificate(serverName);
			if (x509Certificate == null)
			{
				return 1;
			}
			this.nativeServerCertificate = MonoBtlsContext.GetPrivateCertificate(x509Certificate);
			this.SetPrivateCertificate(this.nativeServerCertificate);
			return 1;
		}

		// Token: 0x06000281 RID: 641 RVA: 0x00009F44 File Offset: 0x00008144
		public override void StartHandshake()
		{
			this.InitializeConnection();
			this.ssl = new MonoBtlsSsl(this.ctx);
			this.bio = new MonoBtlsBioMono(this);
			this.ssl.SetBio(this.bio);
			if (base.IsServer)
			{
				if (this.nativeServerCertificate != null)
				{
					this.SetPrivateCertificate(this.nativeServerCertificate);
				}
			}
			else
			{
				this.ssl.SetServerName(base.ServerName);
			}
			if (base.Options.AllowRenegotiation)
			{
				this.ssl.SetRenegotiateMode(MonoBtlsSslRenegotiateMode.FREELY);
			}
		}

		// Token: 0x06000282 RID: 642 RVA: 0x00009FD0 File Offset: 0x000081D0
		private void SetPrivateCertificate(X509CertificateImplBtls privateCert)
		{
			this.ssl.SetCertificate(privateCert.X509);
			this.ssl.SetPrivateKey(privateCert.NativePrivateKey);
			X509CertificateImplCollection intermediateCertificates = privateCert.IntermediateCertificates;
			if (intermediateCertificates == null)
			{
				X509Chain x509Chain = new X509Chain(false);
				x509Chain.ChainPolicy.RevocationMode = X509RevocationMode.NoCheck;
				x509Chain.Build(new X509Certificate2(privateCert.X509.GetRawData(MonoBtlsX509Format.DER), ""));
				X509ChainElementCollection chainElements = x509Chain.ChainElements;
				for (int i = 1; i < chainElements.Count; i++)
				{
					X509Certificate2 certificate = chainElements[i].Certificate;
					if (certificate.SubjectName.RawData.SequenceEqual(certificate.IssuerName.RawData))
					{
						return;
					}
					this.ssl.AddIntermediateCertificate(MonoBtlsX509.LoadFromData(certificate.RawData, MonoBtlsX509Format.DER));
				}
				return;
			}
			for (int j = 0; j < intermediateCertificates.Count; j++)
			{
				X509CertificateImplBtls x509CertificateImplBtls = (X509CertificateImplBtls)intermediateCertificates[j];
				this.ssl.AddIntermediateCertificate(x509CertificateImplBtls.X509);
			}
		}

		// Token: 0x06000283 RID: 643 RVA: 0x0000A0CC File Offset: 0x000082CC
		private static Exception GetException(MonoBtlsSslError status)
		{
			string text;
			int num;
			int error = MonoBtlsError.GetError(out text, out num);
			if (error == 0)
			{
				return new MonoBtlsException(status);
			}
			int errorReason = MonoBtlsError.GetErrorReason(error);
			if (errorReason > 0)
			{
				return new TlsException((AlertDescription)errorReason);
			}
			string errorString = MonoBtlsError.GetErrorString(error);
			string text2;
			if (text != null)
			{
				text2 = string.Format("{0} {1}\n  at {2}:{3}", new object[] { status, errorString, text, num });
			}
			else
			{
				text2 = string.Format("{0} {1}", status, errorString);
			}
			return new MonoBtlsException(text2);
		}

		// Token: 0x06000284 RID: 644 RVA: 0x0000A158 File Offset: 0x00008358
		public override bool ProcessHandshake()
		{
			bool flag = false;
			while (!flag)
			{
				MonoBtlsError.ClearError();
				MonoBtlsSslError monoBtlsSslError = this.DoProcessHandshake();
				if (monoBtlsSslError != MonoBtlsSslError.None)
				{
					if (monoBtlsSslError - MonoBtlsSslError.WantRead > 1)
					{
						this.ctx.CheckLastError("ProcessHandshake");
						throw MonoBtlsContext.GetException(monoBtlsSslError);
					}
					return false;
				}
				else if (this.connected)
				{
					flag = true;
				}
				else
				{
					this.connected = true;
				}
			}
			this.ssl.PrintErrors();
			return true;
		}

		// Token: 0x06000285 RID: 645 RVA: 0x0000A1BD File Offset: 0x000083BD
		private MonoBtlsSslError DoProcessHandshake()
		{
			if (this.connected)
			{
				return this.ssl.Handshake();
			}
			if (base.IsServer)
			{
				return this.ssl.Accept();
			}
			return this.ssl.Connect();
		}

		// Token: 0x06000286 RID: 646 RVA: 0x0000A1F2 File Offset: 0x000083F2
		public override void FinishHandshake()
		{
			this.InitializeSession();
			this.isAuthenticated = true;
		}

		// Token: 0x06000287 RID: 647 RVA: 0x0000A204 File Offset: 0x00008404
		private void InitializeConnection()
		{
			this.ctx = new MonoBtlsSslCtx();
			MonoBtlsProvider.SetupCertificateStore(this.ctx.CertificateStore, base.Settings, base.IsServer);
			if (!base.IsServer || base.AskForClientCertificate)
			{
				this.ctx.SetVerifyCallback(new MonoBtlsVerifyCallback(this.VerifyCallback), false);
			}
			if (!base.IsServer)
			{
				this.ctx.SetSelectCallback(new MonoBtlsSelectCallback(this.SelectCallback));
			}
			if (base.IsServer && (base.Options.ServerCertSelectionDelegate != null || base.Settings.ClientCertificateSelectionCallback != null))
			{
				this.ctx.SetServerNameCallback(new MonoBtlsServerNameCallback(this.ServerNameCallback));
			}
			this.ctx.SetVerifyParam(MonoBtlsProvider.GetVerifyParam(base.Settings, base.ServerName, base.IsServer));
			TlsProtocolCode? tlsProtocolCode;
			TlsProtocolCode? tlsProtocolCode2;
			base.GetProtocolVersions(out tlsProtocolCode, out tlsProtocolCode2);
			if (tlsProtocolCode != null)
			{
				this.ctx.SetMinVersion((int)tlsProtocolCode.Value);
			}
			if (tlsProtocolCode2 != null)
			{
				this.ctx.SetMaxVersion((int)tlsProtocolCode2.Value);
			}
			if (base.Settings != null && base.Settings.EnabledCiphers != null)
			{
				short[] array = new short[base.Settings.EnabledCiphers.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = (short)base.Settings.EnabledCiphers[i];
				}
				this.ctx.SetCiphers(array, true);
			}
			if (base.IsServer)
			{
				MonoTlsSettings settings = base.Settings;
				if (((settings != null) ? settings.ClientCertificateIssuers : null) != null)
				{
					this.ctx.SetClientCertificateIssuers(base.Settings.ClientCertificateIssuers);
				}
			}
		}

		// Token: 0x06000288 RID: 648 RVA: 0x0000A3A0 File Offset: 0x000085A0
		private void GetPeerCertificate()
		{
			if (this.remoteCertificate != null)
			{
				return;
			}
			using (MonoBtlsX509 peerCertificate = this.ssl.GetPeerCertificate())
			{
				if (peerCertificate != null)
				{
					this.remoteCertificate = MonoBtlsProvider.CreateCertificate(peerCertificate);
				}
			}
		}

		// Token: 0x06000289 RID: 649 RVA: 0x0000A3F0 File Offset: 0x000085F0
		private void InitializeSession()
		{
			this.GetPeerCertificate();
			if (base.IsServer && base.AskForClientCertificate && !this.certificateValidated && !base.ValidateCertificate(null, null))
			{
				throw new TlsException(AlertDescription.CertificateUnknown);
			}
			CipherSuiteCode cipherSuiteCode = (CipherSuiteCode)this.ssl.GetCipher();
			TlsProtocolCode tlsProtocolCode = (TlsProtocolCode)this.ssl.GetVersion();
			string serverName = this.ssl.GetServerName();
			this.connectionInfo = new MonoTlsConnectionInfo
			{
				CipherSuiteCode = cipherSuiteCode,
				ProtocolVersion = MonoBtlsContext.GetProtocol(tlsProtocolCode),
				PeerDomainName = serverName
			};
		}

		// Token: 0x0600028A RID: 650 RVA: 0x0000A478 File Offset: 0x00008678
		private static TlsProtocols GetProtocol(TlsProtocolCode protocol)
		{
			switch (protocol)
			{
			case TlsProtocolCode.Tls10:
				return TlsProtocols.Tls10;
			case TlsProtocolCode.Tls11:
				return TlsProtocols.Tls11;
			case TlsProtocolCode.Tls12:
				return TlsProtocols.Tls12;
			default:
				throw new NotSupportedException();
			}
		}

		// Token: 0x0600028B RID: 651 RVA: 0x0000A4AB File Offset: 0x000086AB
		public override void Flush()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600028C RID: 652 RVA: 0x0000A4B4 File Offset: 0x000086B4
		[return: TupleElementNames(new string[] { "ret", "wantMore" })]
		public override ValueTuple<int, bool> Read(byte[] buffer, int offset, int size)
		{
			IntPtr intPtr = Marshal.AllocHGlobal(size);
			if (intPtr == IntPtr.Zero)
			{
				throw new OutOfMemoryException();
			}
			ValueTuple<int, bool> valueTuple;
			try
			{
				MonoBtlsError.ClearError();
				MonoBtlsSslError monoBtlsSslError = this.ssl.Read(intPtr, ref size);
				if (monoBtlsSslError == MonoBtlsSslError.WantRead)
				{
					valueTuple = new ValueTuple<int, bool>(0, true);
				}
				else if (monoBtlsSslError == MonoBtlsSslError.ZeroReturn)
				{
					valueTuple = new ValueTuple<int, bool>(size, false);
				}
				else
				{
					if (monoBtlsSslError != MonoBtlsSslError.None)
					{
						throw MonoBtlsContext.GetException(monoBtlsSslError);
					}
					if (size > 0)
					{
						Marshal.Copy(intPtr, buffer, offset, size);
					}
					valueTuple = new ValueTuple<int, bool>(size, false);
				}
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
			}
			return valueTuple;
		}

		// Token: 0x0600028D RID: 653 RVA: 0x0000A548 File Offset: 0x00008748
		[return: TupleElementNames(new string[] { "ret", "wantMore" })]
		public override ValueTuple<int, bool> Write(byte[] buffer, int offset, int size)
		{
			IntPtr intPtr = Marshal.AllocHGlobal(size);
			if (intPtr == IntPtr.Zero)
			{
				throw new OutOfMemoryException();
			}
			ValueTuple<int, bool> valueTuple;
			try
			{
				MonoBtlsError.ClearError();
				Marshal.Copy(buffer, offset, intPtr, size);
				MonoBtlsSslError monoBtlsSslError = this.ssl.Write(intPtr, ref size);
				if (monoBtlsSslError == MonoBtlsSslError.WantWrite)
				{
					valueTuple = new ValueTuple<int, bool>(0, true);
				}
				else
				{
					if (monoBtlsSslError != MonoBtlsSslError.None)
					{
						throw MonoBtlsContext.GetException(monoBtlsSslError);
					}
					valueTuple = new ValueTuple<int, bool>(size, false);
				}
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
			}
			return valueTuple;
		}

		// Token: 0x0600028E RID: 654 RVA: 0x00003132 File Offset: 0x00001332
		public override void Renegotiate()
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600028F RID: 655 RVA: 0x0000A5C8 File Offset: 0x000087C8
		public override void Shutdown()
		{
			if (base.Settings == null || !base.Settings.SendCloseNotify)
			{
				this.ssl.SetQuietShutdown();
			}
			this.ssl.Shutdown();
		}

		// Token: 0x06000290 RID: 656 RVA: 0x0000A5F5 File Offset: 0x000087F5
		public override bool PendingRenegotiation()
		{
			return this.ssl.RenegotiatePending();
		}

		// Token: 0x06000291 RID: 657 RVA: 0x0000A604 File Offset: 0x00008804
		private void Dispose<T>(ref T disposable) where T : class, IDisposable
		{
			try
			{
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
			catch
			{
			}
			finally
			{
				disposable = default(T);
			}
		}

		// Token: 0x06000292 RID: 658 RVA: 0x0000A654 File Offset: 0x00008854
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					this.Dispose<MonoBtlsSsl>(ref this.ssl);
					this.Dispose<MonoBtlsSslCtx>(ref this.ctx);
					this.Dispose<X509Certificate2>(ref this.remoteCertificate);
					this.Dispose<X509CertificateImplBtls>(ref this.nativeServerCertificate);
					this.Dispose<X509CertificateImplBtls>(ref this.nativeClientCertificate);
					this.Dispose<X509Certificate>(ref this.clientCertificate);
					this.Dispose<MonoBtlsBio>(ref this.bio);
					this.Dispose<MonoBtlsBio>(ref this.errbio);
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x06000293 RID: 659 RVA: 0x0000A6E0 File Offset: 0x000088E0
		int IMonoBtlsBioMono.Read(byte[] buffer, int offset, int size, out bool wantMore)
		{
			return base.Parent.InternalRead(buffer, offset, size, out wantMore);
		}

		// Token: 0x06000294 RID: 660 RVA: 0x0000A6F2 File Offset: 0x000088F2
		bool IMonoBtlsBioMono.Write(byte[] buffer, int offset, int size)
		{
			return base.Parent.InternalWrite(buffer, offset, size);
		}

		// Token: 0x06000295 RID: 661 RVA: 0x00002FA0 File Offset: 0x000011A0
		void IMonoBtlsBioMono.Flush()
		{
		}

		// Token: 0x06000296 RID: 662 RVA: 0x00002FA0 File Offset: 0x000011A0
		void IMonoBtlsBioMono.Close()
		{
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000297 RID: 663 RVA: 0x0000A702 File Offset: 0x00008902
		public override bool IsAuthenticated
		{
			get
			{
				return this.isAuthenticated;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000298 RID: 664 RVA: 0x0000A70A File Offset: 0x0000890A
		internal override X509Certificate LocalClientCertificate
		{
			get
			{
				return this.clientCertificate;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000299 RID: 665 RVA: 0x0000A712 File Offset: 0x00008912
		public override X509Certificate2 RemoteCertificate
		{
			get
			{
				return this.remoteCertificate;
			}
		}

		// Token: 0x04000277 RID: 631
		private X509Certificate2 remoteCertificate;

		// Token: 0x04000278 RID: 632
		private X509Certificate clientCertificate;

		// Token: 0x04000279 RID: 633
		private X509CertificateImplBtls nativeServerCertificate;

		// Token: 0x0400027A RID: 634
		private X509CertificateImplBtls nativeClientCertificate;

		// Token: 0x0400027B RID: 635
		private MonoBtlsSslCtx ctx;

		// Token: 0x0400027C RID: 636
		private MonoBtlsSsl ssl;

		// Token: 0x0400027D RID: 637
		private MonoBtlsBio bio;

		// Token: 0x0400027E RID: 638
		private MonoBtlsBio errbio;

		// Token: 0x0400027F RID: 639
		private MonoTlsConnectionInfo connectionInfo;

		// Token: 0x04000280 RID: 640
		private bool certificateValidated;

		// Token: 0x04000281 RID: 641
		private bool isAuthenticated;

		// Token: 0x04000282 RID: 642
		private bool connected;
	}
}
