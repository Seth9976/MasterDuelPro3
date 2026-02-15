using System;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Win32.SafeHandles;

namespace Mono
{
	// Token: 0x02000012 RID: 18
	internal class X509PalImplMono : X509PalImpl
	{
		// Token: 0x06000044 RID: 68 RVA: 0x000027A2 File Offset: 0x000009A2
		public override X509CertificateImpl Import(byte[] data)
		{
			return base.ImportFallback(data);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x000027AB File Offset: 0x000009AB
		public override X509Certificate2Impl Import(byte[] data, SafePasswordHandle password, X509KeyStorageFlags keyStorageFlags)
		{
			return base.ImportFallback(data, password, keyStorageFlags);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x000027B6 File Offset: 0x000009B6
		public override X509Certificate2Impl Import(X509Certificate cert)
		{
			return null;
		}
	}
}
