using System;
using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;

namespace Mono.Net.Security
{
	// Token: 0x02000077 RID: 119
	internal sealed class MonoSslClientAuthenticationOptions : MonoSslAuthenticationOptions
	{
		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060001BB RID: 443 RVA: 0x000075DC File Offset: 0x000057DC
		public SslClientAuthenticationOptions Options { get; }

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060001BC RID: 444 RVA: 0x000028AE File Offset: 0x00000AAE
		public override bool ServerMode
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060001BD RID: 445 RVA: 0x000075E4 File Offset: 0x000057E4
		public MonoSslClientAuthenticationOptions()
		{
			this.Options = new SslClientAuthenticationOptions();
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060001BE RID: 446 RVA: 0x000075F7 File Offset: 0x000057F7
		public override bool AllowRenegotiation
		{
			get
			{
				return this.Options.AllowRenegotiation;
			}
		}

		// Token: 0x1700005C RID: 92
		// (set) Token: 0x060001BF RID: 447 RVA: 0x00007604 File Offset: 0x00005804
		public override X509RevocationMode CertificateRevocationCheckMode
		{
			set
			{
				this.Options.CertificateRevocationCheckMode = value;
			}
		}

		// Token: 0x1700005D RID: 93
		// (set) Token: 0x060001C0 RID: 448 RVA: 0x00007612 File Offset: 0x00005812
		public override EncryptionPolicy EncryptionPolicy
		{
			set
			{
				this.Options.EncryptionPolicy = value;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060001C1 RID: 449 RVA: 0x00007620 File Offset: 0x00005820
		// (set) Token: 0x060001C2 RID: 450 RVA: 0x0000762D File Offset: 0x0000582D
		public override SslProtocols EnabledSslProtocols
		{
			get
			{
				return this.Options.EnabledSslProtocols;
			}
			set
			{
				this.Options.EnabledSslProtocols = value;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060001C3 RID: 451 RVA: 0x0000763B File Offset: 0x0000583B
		// (set) Token: 0x060001C4 RID: 452 RVA: 0x00007648 File Offset: 0x00005848
		public override string TargetHost
		{
			get
			{
				return this.Options.TargetHost;
			}
			set
			{
				this.Options.TargetHost = value;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060001C5 RID: 453 RVA: 0x00003132 File Offset: 0x00001332
		// (set) Token: 0x060001C6 RID: 454 RVA: 0x00003132 File Offset: 0x00001332
		public override bool ClientCertificateRequired
		{
			get
			{
				throw new NotSupportedException();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060001C7 RID: 455 RVA: 0x00007656 File Offset: 0x00005856
		// (set) Token: 0x060001C8 RID: 456 RVA: 0x00007663 File Offset: 0x00005863
		public override X509CertificateCollection ClientCertificates
		{
			get
			{
				return this.Options.ClientCertificates;
			}
			set
			{
				this.Options.ClientCertificates = value;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x00003132 File Offset: 0x00001332
		// (set) Token: 0x060001CA RID: 458 RVA: 0x00003132 File Offset: 0x00001332
		public override X509Certificate ServerCertificate
		{
			get
			{
				throw new NotSupportedException();
			}
			set
			{
				throw new NotSupportedException();
			}
		}
	}
}
