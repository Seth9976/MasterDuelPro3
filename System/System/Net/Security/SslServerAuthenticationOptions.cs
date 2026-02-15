using System;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;

namespace System.Net.Security
{
	// Token: 0x020004EC RID: 1260
	public class SslServerAuthenticationOptions
	{
		// Token: 0x170006BF RID: 1727
		// (get) Token: 0x06001ED6 RID: 7894 RVA: 0x0008742A File Offset: 0x0008562A
		public bool AllowRenegotiation
		{
			get
			{
				return this._allowRenegotiation;
			}
		}

		// Token: 0x170006C0 RID: 1728
		// (get) Token: 0x06001ED7 RID: 7895 RVA: 0x00087432 File Offset: 0x00085632
		// (set) Token: 0x06001ED8 RID: 7896 RVA: 0x0008743A File Offset: 0x0008563A
		public bool ClientCertificateRequired { get; set; }

		// Token: 0x170006C1 RID: 1729
		// (get) Token: 0x06001ED9 RID: 7897 RVA: 0x00087443 File Offset: 0x00085643
		// (set) Token: 0x06001EDA RID: 7898 RVA: 0x0008744B File Offset: 0x0008564B
		public X509Certificate ServerCertificate { get; set; }

		// Token: 0x170006C2 RID: 1730
		// (get) Token: 0x06001EDB RID: 7899 RVA: 0x00087454 File Offset: 0x00085654
		// (set) Token: 0x06001EDC RID: 7900 RVA: 0x0008745C File Offset: 0x0008565C
		public SslProtocols EnabledSslProtocols
		{
			get
			{
				return this._enabledSslProtocols;
			}
			set
			{
				this._enabledSslProtocols = value;
			}
		}

		// Token: 0x170006C3 RID: 1731
		// (set) Token: 0x06001EDD RID: 7901 RVA: 0x00087465 File Offset: 0x00085665
		public X509RevocationMode CertificateRevocationCheckMode
		{
			set
			{
				if (value != X509RevocationMode.NoCheck && value != X509RevocationMode.Offline && value != X509RevocationMode.Online)
				{
					throw new ArgumentException(SR.Format("The specified value is not valid in the '{0}' enumeration.", "X509RevocationMode"), "value");
				}
				this._checkCertificateRevocation = value;
			}
		}

		// Token: 0x170006C4 RID: 1732
		// (set) Token: 0x06001EDE RID: 7902 RVA: 0x00087493 File Offset: 0x00085693
		public EncryptionPolicy EncryptionPolicy
		{
			set
			{
				if (value != EncryptionPolicy.RequireEncryption && value != EncryptionPolicy.AllowNoEncryption && value != EncryptionPolicy.NoEncryption)
				{
					throw new ArgumentException(SR.Format("The specified value is not valid in the '{0}' enumeration.", "EncryptionPolicy"), "value");
				}
				this._encryptionPolicy = value;
			}
		}

		// Token: 0x04001668 RID: 5736
		private X509RevocationMode _checkCertificateRevocation;

		// Token: 0x04001669 RID: 5737
		private SslProtocols _enabledSslProtocols;

		// Token: 0x0400166A RID: 5738
		private EncryptionPolicy _encryptionPolicy;

		// Token: 0x0400166B RID: 5739
		private bool _allowRenegotiation = true;
	}
}
