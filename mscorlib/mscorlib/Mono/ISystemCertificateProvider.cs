using System;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Win32.SafeHandles;

namespace Mono
{
	// Token: 0x02000030 RID: 48
	internal interface ISystemCertificateProvider
	{
		// Token: 0x06000061 RID: 97
		X509CertificateImpl Import(byte[] data, CertificateImportFlags importFlags = CertificateImportFlags.None);

		// Token: 0x06000062 RID: 98
		X509CertificateImpl Import(byte[] data, SafePasswordHandle password, X509KeyStorageFlags keyStorageFlags, CertificateImportFlags importFlags = CertificateImportFlags.None);

		// Token: 0x06000063 RID: 99
		X509CertificateImpl Import(X509Certificate cert, CertificateImportFlags importFlags = CertificateImportFlags.None);
	}
}
