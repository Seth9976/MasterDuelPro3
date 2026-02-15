using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using Microsoft.Win32.SafeHandles;
using Mono.Btls;
using Mono.Net.Security;
using Mono.Security.Interface;

namespace Mono
{
	// Token: 0x0200000F RID: 15
	internal class SystemCertificateProvider : ISystemCertificateProvider
	{
		// Token: 0x06000032 RID: 50 RVA: 0x00002508 File Offset: 0x00000708
		private static X509PalImpl GetX509Pal()
		{
			MonoTlsProvider monoTlsProvider = SystemCertificateProvider.provider;
			Guid? guid = ((monoTlsProvider != null) ? new Guid?(monoTlsProvider.ID) : null);
			Guid btlsId = Mono.Net.Security.MonoTlsProviderFactory.BtlsId;
			if (guid != null && (guid == null || guid.GetValueOrDefault() == btlsId))
			{
				return new X509PalImplBtls(SystemCertificateProvider.provider);
			}
			return new X509PalImplMono();
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002574 File Offset: 0x00000774
		private static void EnsureInitialized()
		{
			object obj = SystemCertificateProvider.syncRoot;
			lock (obj)
			{
				if (Interlocked.CompareExchange(ref SystemCertificateProvider.initialized, 1, 0) == 0)
				{
					SystemCertificateProvider.provider = Mono.Security.Interface.MonoTlsProviderFactory.GetProvider();
					SystemCertificateProvider.x509pal = SystemCertificateProvider.GetX509Pal();
				}
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000034 RID: 52 RVA: 0x000025D4 File Offset: 0x000007D4
		public X509PalImpl X509Pal
		{
			get
			{
				SystemCertificateProvider.EnsureInitialized();
				return SystemCertificateProvider.x509pal;
			}
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000025E0 File Offset: 0x000007E0
		public X509CertificateImpl Import(byte[] data, CertificateImportFlags importFlags = CertificateImportFlags.None)
		{
			if (data == null || data.Length == 0)
			{
				return null;
			}
			if ((importFlags & CertificateImportFlags.DisableNativeBackend) == CertificateImportFlags.None)
			{
				X509CertificateImpl x509CertificateImpl = this.X509Pal.Import(data);
				if (x509CertificateImpl != null)
				{
					return x509CertificateImpl;
				}
			}
			if ((importFlags & CertificateImportFlags.DisableAutomaticFallback) != CertificateImportFlags.None)
			{
				return null;
			}
			return this.X509Pal.ImportFallback(data);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002622 File Offset: 0x00000822
		X509CertificateImpl ISystemCertificateProvider.Import(byte[] data, SafePasswordHandle password, X509KeyStorageFlags keyStorageFlags, CertificateImportFlags importFlags)
		{
			return this.Import(data, password, keyStorageFlags, importFlags);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002630 File Offset: 0x00000830
		public X509Certificate2Impl Import(byte[] data, SafePasswordHandle password, X509KeyStorageFlags keyStorageFlags, CertificateImportFlags importFlags = CertificateImportFlags.None)
		{
			if (data == null || data.Length == 0)
			{
				return null;
			}
			if ((importFlags & CertificateImportFlags.DisableNativeBackend) == CertificateImportFlags.None)
			{
				X509Certificate2Impl x509Certificate2Impl = this.X509Pal.Import(data, password, keyStorageFlags);
				if (x509Certificate2Impl != null)
				{
					return x509Certificate2Impl;
				}
			}
			if ((importFlags & CertificateImportFlags.DisableAutomaticFallback) != CertificateImportFlags.None)
			{
				return null;
			}
			return this.X509Pal.ImportFallback(data, password, keyStorageFlags);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002678 File Offset: 0x00000878
		X509CertificateImpl ISystemCertificateProvider.Import(X509Certificate cert, CertificateImportFlags importFlags)
		{
			return this.Import(cert, importFlags);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002684 File Offset: 0x00000884
		public X509Certificate2Impl Import(X509Certificate cert, CertificateImportFlags importFlags = CertificateImportFlags.None)
		{
			if (cert.Impl == null)
			{
				return null;
			}
			X509Certificate2Impl x509Certificate2Impl = cert.Impl as X509Certificate2Impl;
			if (x509Certificate2Impl != null)
			{
				return (X509Certificate2Impl)x509Certificate2Impl.Clone();
			}
			if ((importFlags & CertificateImportFlags.DisableNativeBackend) == CertificateImportFlags.None)
			{
				x509Certificate2Impl = this.X509Pal.Import(cert);
				if (x509Certificate2Impl != null)
				{
					return x509Certificate2Impl;
				}
			}
			if ((importFlags & CertificateImportFlags.DisableAutomaticFallback) != CertificateImportFlags.None)
			{
				return null;
			}
			return this.X509Pal.ImportFallback(cert.GetRawCertData());
		}

		// Token: 0x0400001C RID: 28
		private static MonoTlsProvider provider;

		// Token: 0x0400001D RID: 29
		private static int initialized;

		// Token: 0x0400001E RID: 30
		private static X509PalImpl x509pal;

		// Token: 0x0400001F RID: 31
		private static object syncRoot = new object();
	}
}
