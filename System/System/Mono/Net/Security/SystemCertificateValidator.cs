using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Mono.Security.Interface;

namespace Mono.Net.Security
{
	// Token: 0x0200007D RID: 125
	internal static class SystemCertificateValidator
	{
		// Token: 0x060001F0 RID: 496 RVA: 0x00007F44 File Offset: 0x00006144
		static SystemCertificateValidator()
		{
			try
			{
				string environmentVariable = Environment.GetEnvironmentVariable("MONO_X509_REVOCATION_MODE");
				if (!string.IsNullOrEmpty(environmentVariable))
				{
					SystemCertificateValidator.revocation_mode = (X509RevocationMode)Enum.Parse(typeof(X509RevocationMode), environmentVariable, true);
				}
			}
			catch
			{
			}
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x00007FC8 File Offset: 0x000061C8
		internal static bool NeedsChain(MonoTlsSettings settings)
		{
			return !SystemCertificateValidator.is_macosx || (CertificateValidationHelper.SupportsX509Chain && (settings == null || !settings.SkipSystemValidators || settings.CallbackNeedsCertificateChain));
		}

		// Token: 0x04000174 RID: 372
		private static bool is_macosx = Environment.OSVersion.Platform != PlatformID.Win32NT && File.Exists("/System/Library/Frameworks/Security.framework/Security");

		// Token: 0x04000175 RID: 373
		private static X509RevocationMode revocation_mode = X509RevocationMode.NoCheck;

		// Token: 0x04000176 RID: 374
		private static X509KeyUsageFlags s_flags = X509KeyUsageFlags.KeyAgreement | X509KeyUsageFlags.KeyEncipherment | X509KeyUsageFlags.DigitalSignature;
	}
}
