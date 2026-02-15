using System;
using Microsoft.Win32.SafeHandles;
using Mono;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020003CE RID: 974
	internal static class X509Helper
	{
		// Token: 0x170003AD RID: 941
		// (get) Token: 0x06002141 RID: 8513 RVA: 0x0008A52C File Offset: 0x0008872C
		private static ISystemCertificateProvider CertificateProvider
		{
			get
			{
				return DependencyInjector.SystemProvider.CertificateProvider;
			}
		}

		// Token: 0x06002142 RID: 8514 RVA: 0x0008A538 File Offset: 0x00088738
		public static X509CertificateImpl InitFromCertificate(X509Certificate cert)
		{
			return X509Helper.CertificateProvider.Import(cert, CertificateImportFlags.None);
		}

		// Token: 0x06002143 RID: 8515 RVA: 0x0008A546 File Offset: 0x00088746
		public static X509CertificateImpl InitFromCertificate(X509CertificateImpl impl)
		{
			if (impl == null)
			{
				return null;
			}
			return impl.Clone();
		}

		// Token: 0x06002144 RID: 8516 RVA: 0x0008A553 File Offset: 0x00088753
		public static bool IsValid(X509CertificateImpl impl)
		{
			return impl != null && impl.IsValid;
		}

		// Token: 0x06002145 RID: 8517 RVA: 0x0008A560 File Offset: 0x00088760
		internal static void ThrowIfContextInvalid(X509CertificateImpl impl)
		{
			if (!X509Helper.IsValid(impl))
			{
				throw X509Helper.GetInvalidContextException();
			}
		}

		// Token: 0x06002146 RID: 8518 RVA: 0x0008A570 File Offset: 0x00088770
		internal static Exception GetInvalidContextException()
		{
			return new CryptographicException(Locale.GetText("Certificate instance is empty."));
		}

		// Token: 0x06002147 RID: 8519 RVA: 0x0008A581 File Offset: 0x00088781
		public static X509CertificateImpl Import(byte[] rawData)
		{
			return X509Helper.CertificateProvider.Import(rawData, CertificateImportFlags.None);
		}

		// Token: 0x06002148 RID: 8520 RVA: 0x0008A58F File Offset: 0x0008878F
		public static X509CertificateImpl Import(byte[] rawData, SafePasswordHandle password, X509KeyStorageFlags keyStorageFlags)
		{
			return X509Helper.CertificateProvider.Import(rawData, password, keyStorageFlags, CertificateImportFlags.None);
		}
	}
}
