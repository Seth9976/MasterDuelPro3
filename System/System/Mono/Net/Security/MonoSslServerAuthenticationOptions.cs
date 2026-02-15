using System;
using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;

namespace Mono.Net.Security
{
	// Token: 0x02000078 RID: 120
	internal sealed class MonoSslServerAuthenticationOptions : MonoSslAuthenticationOptions
	{
		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060001CB RID: 459 RVA: 0x00007671 File Offset: 0x00005871
		public SslServerAuthenticationOptions Options { get; }

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060001CC RID: 460 RVA: 0x00003BCC File Offset: 0x00001DCC
		public override bool ServerMode
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060001CD RID: 461 RVA: 0x00007679 File Offset: 0x00005879
		public MonoSslServerAuthenticationOptions()
		{
			this.Options = new SslServerAuthenticationOptions();
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060001CE RID: 462 RVA: 0x0000768C File Offset: 0x0000588C
		public override bool AllowRenegotiation
		{
			get
			{
				return this.Options.AllowRenegotiation;
			}
		}

		// Token: 0x17000066 RID: 102
		// (set) Token: 0x060001CF RID: 463 RVA: 0x00007699 File Offset: 0x00005899
		public override X509RevocationMode CertificateRevocationCheckMode
		{
			set
			{
				this.Options.CertificateRevocationCheckMode = value;
			}
		}

		// Token: 0x17000067 RID: 103
		// (set) Token: 0x060001D0 RID: 464 RVA: 0x000076A7 File Offset: 0x000058A7
		public override EncryptionPolicy EncryptionPolicy
		{
			set
			{
				this.Options.EncryptionPolicy = value;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060001D1 RID: 465 RVA: 0x000076B5 File Offset: 0x000058B5
		// (set) Token: 0x060001D2 RID: 466 RVA: 0x000076C2 File Offset: 0x000058C2
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

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x000076D0 File Offset: 0x000058D0
		// (set) Token: 0x060001D4 RID: 468 RVA: 0x000076DD File Offset: 0x000058DD
		public override bool ClientCertificateRequired
		{
			get
			{
				return this.Options.ClientCertificateRequired;
			}
			set
			{
				this.Options.ClientCertificateRequired = value;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060001D5 RID: 469 RVA: 0x00003132 File Offset: 0x00001332
		// (set) Token: 0x060001D6 RID: 470 RVA: 0x00003132 File Offset: 0x00001332
		public override string TargetHost
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

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060001D7 RID: 471 RVA: 0x000076EB File Offset: 0x000058EB
		// (set) Token: 0x060001D8 RID: 472 RVA: 0x000076F8 File Offset: 0x000058F8
		public override X509Certificate ServerCertificate
		{
			get
			{
				return this.Options.ServerCertificate;
			}
			set
			{
				this.Options.ServerCertificate = value;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060001D9 RID: 473 RVA: 0x00003132 File Offset: 0x00001332
		// (set) Token: 0x060001DA RID: 474 RVA: 0x00003132 File Offset: 0x00001332
		public override X509CertificateCollection ClientCertificates
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
