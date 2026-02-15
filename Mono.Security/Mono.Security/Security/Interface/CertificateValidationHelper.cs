using System;
using System.IO;

namespace Mono.Security.Interface
{
	// Token: 0x0200003C RID: 60
	public static class CertificateValidationHelper
	{
		// Token: 0x06000136 RID: 310 RVA: 0x000093C0 File Offset: 0x000075C0
		static CertificateValidationHelper()
		{
			if (File.Exists("/System/Library/Frameworks/Security.framework/Security"))
			{
				CertificateValidationHelper.noX509Chain = true;
				CertificateValidationHelper.supportsTrustAnchors = true;
				return;
			}
			CertificateValidationHelper.noX509Chain = false;
			CertificateValidationHelper.supportsTrustAnchors = false;
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000137 RID: 311 RVA: 0x000093E7 File Offset: 0x000075E7
		public static bool SupportsX509Chain
		{
			get
			{
				return !CertificateValidationHelper.noX509Chain;
			}
		}

		// Token: 0x040000BC RID: 188
		private static readonly bool noX509Chain;

		// Token: 0x040000BD RID: 189
		private static readonly bool supportsTrustAnchors;
	}
}
