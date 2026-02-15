using System;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using Mono.Net.Security.Private;
using Mono.Security.Interface;

namespace Mono.Net.Security
{
	// Token: 0x0200006A RID: 106
	internal class ChainValidationHelper : ICertificateValidator
	{
		// Token: 0x0600013F RID: 319 RVA: 0x00005705 File Offset: 0x00003905
		internal static ChainValidationHelper GetInternalValidator(SslStream owner, MobileTlsProvider provider, MonoTlsSettings settings)
		{
			if (settings == null)
			{
				return new ChainValidationHelper(owner, provider, null, false, null);
			}
			if (settings.CertificateValidator != null)
			{
				return (ChainValidationHelper)settings.CertificateValidator;
			}
			return new ChainValidationHelper(owner, provider, settings, false, null);
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00005734 File Offset: 0x00003934
		internal static ChainValidationHelper Create(MobileTlsProvider provider, ref MonoTlsSettings settings, MonoTlsStream stream)
		{
			ChainValidationHelper chainValidationHelper = new ChainValidationHelper(null, provider, settings, true, stream);
			settings = chainValidationHelper.settings;
			return chainValidationHelper;
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00005758 File Offset: 0x00003958
		private ChainValidationHelper(SslStream owner, MobileTlsProvider provider, MonoTlsSettings settings, bool cloneSettings, MonoTlsStream stream)
		{
			if (settings == null)
			{
				settings = MonoTlsSettings.CopyDefaultSettings();
			}
			if (cloneSettings)
			{
				settings = settings.CloneWithValidator(this);
			}
			if (provider == null)
			{
				provider = MonoTlsProviderFactory.GetProviderInternal();
			}
			this.provider = provider;
			this.settings = settings;
			this.tlsStream = stream;
			if (owner != null)
			{
				this.owner = new WeakReference<SslStream>(owner);
			}
			bool flag = false;
			if (settings != null)
			{
				this.certValidationCallback = ChainValidationHelper.GetValidationCallback(settings);
				this.certSelectionCallback = CallbackHelpers.MonoToInternal(settings.ClientCertificateSelectionCallback);
				flag = settings.UseServicePointManagerCallback ?? (stream != null);
			}
			if (stream != null)
			{
				this.request = stream.Request;
				if (this.certValidationCallback == null)
				{
					this.certValidationCallback = this.request.ServerCertValidationCallback;
				}
				if (this.certSelectionCallback == null)
				{
					this.certSelectionCallback = new LocalCertSelectionCallback(ChainValidationHelper.DefaultSelectionCallback);
				}
				if (settings == null)
				{
					flag = true;
				}
			}
			if (flag && this.certValidationCallback == null)
			{
				this.certValidationCallback = ServicePointManager.ServerCertValidationCallback;
			}
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00005854 File Offset: 0x00003A54
		private static ServerCertValidationCallback GetValidationCallback(MonoTlsSettings settings)
		{
			if (settings.RemoteCertificateValidationCallback == null)
			{
				return null;
			}
			return new ServerCertValidationCallback(delegate(object s, X509Certificate c, X509Chain ch, SslPolicyErrors e)
			{
				string text = null;
				SslStream sslStream = s as SslStream;
				if (sslStream != null)
				{
					text = sslStream.InternalTargetHost;
				}
				else
				{
					HttpWebRequest httpWebRequest = s as HttpWebRequest;
					if (httpWebRequest != null)
					{
						text = httpWebRequest.Host;
						if (!string.IsNullOrEmpty(text))
						{
							int num = text.IndexOf(':');
							if (num > 0)
							{
								text = text.Substring(0, num);
							}
						}
					}
				}
				return settings.RemoteCertificateValidationCallback(text, c, ch, (MonoSslPolicyErrors)e);
			});
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00005890 File Offset: 0x00003A90
		private static X509Certificate DefaultSelectionCallback(string targetHost, X509CertificateCollection localCertificates, X509Certificate remoteCertificate, string[] acceptableIssuers)
		{
			X509Certificate x509Certificate;
			if (localCertificates == null || localCertificates.Count == 0)
			{
				x509Certificate = null;
			}
			else
			{
				x509Certificate = localCertificates[0];
			}
			return x509Certificate;
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000144 RID: 324 RVA: 0x000058B5 File Offset: 0x00003AB5
		public MonoTlsSettings Settings
		{
			get
			{
				return this.settings;
			}
		}

		// Token: 0x06000145 RID: 325 RVA: 0x000058BD File Offset: 0x00003ABD
		public bool SelectClientCertificate(string targetHost, X509CertificateCollection localCertificates, X509Certificate remoteCertificate, string[] acceptableIssuers, out X509Certificate clientCertificate)
		{
			if (this.certSelectionCallback == null)
			{
				clientCertificate = null;
				return false;
			}
			clientCertificate = this.certSelectionCallback(targetHost, localCertificates, remoteCertificate, acceptableIssuers);
			return true;
		}

		// Token: 0x06000146 RID: 326 RVA: 0x000058E4 File Offset: 0x00003AE4
		public ValidationResult ValidateCertificate(string host, bool serverMode, X509Certificate leaf, X509Chain chain)
		{
			ValidationResult validationResult2;
			try
			{
				ValidationResult validationResult = this.ValidateChain(host, serverMode, leaf, chain, null, SslPolicyErrors.None);
				if (this.tlsStream != null)
				{
					this.tlsStream.CertificateValidationFailed = validationResult == null || !validationResult.Trusted || validationResult.UserDenied;
				}
				validationResult2 = validationResult;
			}
			catch
			{
				if (this.tlsStream != null)
				{
					this.tlsStream.CertificateValidationFailed = true;
				}
				throw;
			}
			return validationResult2;
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00005954 File Offset: 0x00003B54
		private ValidationResult ValidateChain(string host, bool server, X509Certificate leaf, X509Chain chain, X509CertificateCollection certs, SslPolicyErrors errors)
		{
			X509Chain x509Chain = chain;
			bool flag = chain == null;
			ValidationResult validationResult2;
			try
			{
				ValidationResult validationResult = this.ValidateChain(host, server, leaf, ref chain, certs, errors);
				if (chain != x509Chain)
				{
					flag = true;
				}
				validationResult2 = validationResult;
			}
			finally
			{
				if (flag && chain != null)
				{
					chain.Dispose();
				}
			}
			return validationResult2;
		}

		// Token: 0x06000148 RID: 328 RVA: 0x000059A4 File Offset: 0x00003BA4
		private ValidationResult ValidateChain(string host, bool server, X509Certificate leaf, ref X509Chain chain, X509CertificateCollection certs, SslPolicyErrors errors)
		{
			bool flag = false;
			bool flag2 = false;
			if (this.tlsStream != null)
			{
				this.request.ServicePoint.UpdateServerCertificate(leaf);
			}
			if (leaf == null)
			{
				errors |= SslPolicyErrors.RemoteCertificateNotAvailable;
				if (this.certValidationCallback != null)
				{
					flag2 = this.InvokeCallback(leaf, null, errors);
					flag = !flag2;
				}
				return new ValidationResult(flag2, flag, 0, new MonoSslPolicyErrors?((MonoSslPolicyErrors)errors));
			}
			if (!string.IsNullOrEmpty(host))
			{
				int num = host.IndexOf(':');
				if (num > 0)
				{
					host = host.Substring(0, num);
				}
			}
			ICertificatePolicy legacyCertificatePolicy = ServicePointManager.GetLegacyCertificatePolicy();
			int num2 = 0;
			bool flag3 = SystemCertificateValidator.NeedsChain(this.settings);
			if (!flag3 && this.certValidationCallback != null && (this.settings == null || this.settings.CallbackNeedsCertificateChain))
			{
				flag3 = true;
			}
			flag2 = this.provider.ValidateCertificate(this, host, server, certs, flag3, ref chain, ref errors, ref num2);
			if (num2 == 0 && errors != SslPolicyErrors.None)
			{
				num2 = -2146762485;
			}
			if (legacyCertificatePolicy != null && (!(legacyCertificatePolicy is DefaultCertificatePolicy) || this.certValidationCallback == null))
			{
				ServicePoint servicePoint = null;
				if (this.request != null)
				{
					servicePoint = this.request.ServicePointNoLock;
				}
				flag2 = legacyCertificatePolicy.CheckValidationResult(servicePoint, leaf, this.request, num2);
				flag = !flag2 && !(legacyCertificatePolicy is DefaultCertificatePolicy);
			}
			if (this.certValidationCallback != null)
			{
				flag2 = this.InvokeCallback(leaf, chain, errors);
				flag = !flag2;
			}
			return new ValidationResult(flag2, flag, num2, new MonoSslPolicyErrors?((MonoSslPolicyErrors)errors));
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00005AF8 File Offset: 0x00003CF8
		private bool InvokeCallback(X509Certificate leaf, X509Chain chain, SslPolicyErrors errors)
		{
			object obj = null;
			SslStream sslStream;
			if (this.request != null)
			{
				obj = this.request;
			}
			else if (this.owner != null && this.owner.TryGetTarget(out sslStream))
			{
				obj = sslStream;
			}
			return this.certValidationCallback.Invoke(obj, leaf, chain, errors);
		}

		// Token: 0x0400010C RID: 268
		private readonly WeakReference<SslStream> owner;

		// Token: 0x0400010D RID: 269
		private readonly MonoTlsSettings settings;

		// Token: 0x0400010E RID: 270
		private readonly MobileTlsProvider provider;

		// Token: 0x0400010F RID: 271
		private readonly ServerCertValidationCallback certValidationCallback;

		// Token: 0x04000110 RID: 272
		private readonly LocalCertSelectionCallback certSelectionCallback;

		// Token: 0x04000111 RID: 273
		private readonly MonoTlsStream tlsStream;

		// Token: 0x04000112 RID: 274
		private readonly HttpWebRequest request;
	}
}
