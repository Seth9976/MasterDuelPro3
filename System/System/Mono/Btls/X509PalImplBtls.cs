using System;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Win32.SafeHandles;
using Mono.Security.Interface;

namespace Mono.Btls
{
	// Token: 0x020000D5 RID: 213
	internal class X509PalImplBtls : X509PalImpl
	{
		// Token: 0x06000405 RID: 1029 RVA: 0x0000DA40 File Offset: 0x0000BC40
		public X509PalImplBtls(MonoTlsProvider provider)
		{
			this.Provider = (MonoBtlsProvider)provider;
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000406 RID: 1030 RVA: 0x0000DA54 File Offset: 0x0000BC54
		private MonoBtlsProvider Provider { get; }

		// Token: 0x06000407 RID: 1031 RVA: 0x0000DA5C File Offset: 0x0000BC5C
		public override X509CertificateImpl Import(byte[] data)
		{
			return this.Provider.GetNativeCertificate(data, null, X509KeyStorageFlags.DefaultKeySet);
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x0000DA6C File Offset: 0x0000BC6C
		public override X509Certificate2Impl Import(byte[] data, SafePasswordHandle password, X509KeyStorageFlags keyStorageFlags)
		{
			return this.Provider.GetNativeCertificate(data, password, keyStorageFlags);
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x0000DA7C File Offset: 0x0000BC7C
		public override X509Certificate2Impl Import(X509Certificate cert)
		{
			return this.Provider.GetNativeCertificate(cert);
		}
	}
}
