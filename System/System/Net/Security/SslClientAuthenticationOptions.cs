using System;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;

namespace System.Net.Security
{
	// Token: 0x020004EB RID: 1259
	public class SslClientAuthenticationOptions
	{
		// Token: 0x170006B9 RID: 1721
		// (get) Token: 0x06001ECC RID: 7884 RVA: 0x00087384 File Offset: 0x00085584
		public bool AllowRenegotiation
		{
			get
			{
				return this._allowRenegotiation;
			}
		}

		// Token: 0x170006BA RID: 1722
		// (get) Token: 0x06001ECD RID: 7885 RVA: 0x0008738C File Offset: 0x0008558C
		// (set) Token: 0x06001ECE RID: 7886 RVA: 0x00087394 File Offset: 0x00085594
		public string TargetHost { get; set; }

		// Token: 0x170006BB RID: 1723
		// (get) Token: 0x06001ECF RID: 7887 RVA: 0x0008739D File Offset: 0x0008559D
		// (set) Token: 0x06001ED0 RID: 7888 RVA: 0x000873A5 File Offset: 0x000855A5
		public X509CertificateCollection ClientCertificates { get; set; }

		// Token: 0x170006BC RID: 1724
		// (set) Token: 0x06001ED1 RID: 7889 RVA: 0x000873AE File Offset: 0x000855AE
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

		// Token: 0x170006BD RID: 1725
		// (set) Token: 0x06001ED2 RID: 7890 RVA: 0x000873DC File Offset: 0x000855DC
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

		// Token: 0x170006BE RID: 1726
		// (get) Token: 0x06001ED3 RID: 7891 RVA: 0x0008740A File Offset: 0x0008560A
		// (set) Token: 0x06001ED4 RID: 7892 RVA: 0x00087412 File Offset: 0x00085612
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

		// Token: 0x04001662 RID: 5730
		private EncryptionPolicy _encryptionPolicy;

		// Token: 0x04001663 RID: 5731
		private X509RevocationMode _checkCertificateRevocation;

		// Token: 0x04001664 RID: 5732
		private SslProtocols _enabledSslProtocols;

		// Token: 0x04001665 RID: 5733
		private bool _allowRenegotiation = true;
	}
}
