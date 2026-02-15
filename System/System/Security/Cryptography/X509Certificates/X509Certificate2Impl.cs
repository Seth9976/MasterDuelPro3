using System;
using System.Collections.Generic;
using System.Text;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020001BF RID: 447
	internal abstract class X509Certificate2Impl : X509CertificateImpl
	{
		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x06000A8D RID: 2701
		public abstract IEnumerable<X509Extension> Extensions { get; }

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x06000A8E RID: 2702
		public abstract X500DistinguishedName IssuerName { get; }

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x06000A8F RID: 2703
		// (set) Token: 0x06000A90 RID: 2704
		public abstract AsymmetricAlgorithm PrivateKey { get; set; }

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x06000A91 RID: 2705
		public abstract string SignatureAlgorithm { get; }

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x06000A92 RID: 2706
		public abstract X500DistinguishedName SubjectName { get; }

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x06000A93 RID: 2707
		public abstract int Version { get; }

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x06000A94 RID: 2708
		internal abstract X509CertificateImplCollection IntermediateCertificates { get; }

		// Token: 0x06000A95 RID: 2709
		public abstract string GetNameInfo(X509NameType nameType, bool forIssuer);

		// Token: 0x06000A96 RID: 2710
		public abstract bool Verify(X509Certificate2 thisCertificate);

		// Token: 0x06000A97 RID: 2711
		public abstract void AppendPrivateKeyInfo(StringBuilder sb);

		// Token: 0x06000A98 RID: 2712 RVA: 0x000367EE File Offset: 0x000349EE
		public sealed override X509CertificateImpl CopyWithPrivateKey(RSA privateKey)
		{
			X509Certificate2Impl x509Certificate2Impl = (X509Certificate2Impl)this.Clone();
			x509Certificate2Impl.PrivateKey = privateKey;
			return x509Certificate2Impl;
		}
	}
}
