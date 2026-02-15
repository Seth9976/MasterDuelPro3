using System;
using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;

namespace Mono.Net.Security
{
	// Token: 0x02000076 RID: 118
	internal abstract class MonoSslAuthenticationOptions
	{
		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060001AB RID: 427
		public abstract bool ServerMode { get; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060001AC RID: 428
		public abstract bool AllowRenegotiation { get; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060001AD RID: 429
		// (set) Token: 0x060001AE RID: 430
		public abstract SslProtocols EnabledSslProtocols { get; set; }

		// Token: 0x17000052 RID: 82
		// (set) Token: 0x060001AF RID: 431
		public abstract EncryptionPolicy EncryptionPolicy { set; }

		// Token: 0x17000053 RID: 83
		// (set) Token: 0x060001B0 RID: 432
		public abstract X509RevocationMode CertificateRevocationCheckMode { set; }

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060001B1 RID: 433
		// (set) Token: 0x060001B2 RID: 434
		public abstract string TargetHost { get; set; }

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060001B3 RID: 435
		// (set) Token: 0x060001B4 RID: 436
		public abstract X509Certificate ServerCertificate { get; set; }

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060001B5 RID: 437
		// (set) Token: 0x060001B6 RID: 438
		public abstract X509CertificateCollection ClientCertificates { get; set; }

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060001B7 RID: 439
		// (set) Token: 0x060001B8 RID: 440
		public abstract bool ClientCertificateRequired { get; set; }

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060001B9 RID: 441 RVA: 0x000075D4 File Offset: 0x000057D4
		internal ServerCertSelectionCallback ServerCertSelectionDelegate { get; }
	}
}
