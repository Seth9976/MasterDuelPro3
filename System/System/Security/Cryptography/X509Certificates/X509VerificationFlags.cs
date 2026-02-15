using System;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Specifies conditions under which verification of certificates in the X509 chain should be conducted.</summary>
	// Token: 0x020001B8 RID: 440
	[Flags]
	public enum X509VerificationFlags
	{
		/// <summary>No flags pertaining to verification are included.</summary>
		// Token: 0x04000804 RID: 2052
		NoFlag = 0,
		/// <summary>Ignore certificates in the chain that are not valid either because they have expired or they are not yet in effect when determining certificate validity.</summary>
		// Token: 0x04000805 RID: 2053
		IgnoreNotTimeValid = 1,
		/// <summary>Ignore that the certificate trust list (CTL) is not valid, for reasons such as the CTL has expired, when determining certificate verification.</summary>
		// Token: 0x04000806 RID: 2054
		IgnoreCtlNotTimeValid = 2,
		/// <summary>Ignore that the CA (certificate authority) certificate and the issued certificate have validity periods that are not nested when verifying the certificate. For example, the CA cert can be valid from January 1 to December 1 and the issued certificate from January 2 to December 2, which would mean the validity periods are not nested.</summary>
		// Token: 0x04000807 RID: 2055
		IgnoreNotTimeNested = 4,
		/// <summary>Ignore that the basic constraints are not valid when determining certificate verification.</summary>
		// Token: 0x04000808 RID: 2056
		IgnoreInvalidBasicConstraints = 8,
		/// <summary>Ignore that the chain cannot be verified due to an unknown certificate authority (CA).</summary>
		// Token: 0x04000809 RID: 2057
		AllowUnknownCertificateAuthority = 16,
		/// <summary>Ignore that the certificate was not issued for the current use when determining certificate verification.</summary>
		// Token: 0x0400080A RID: 2058
		IgnoreWrongUsage = 32,
		/// <summary>Ignore that the certificate has an invalid name when determining certificate verification.</summary>
		// Token: 0x0400080B RID: 2059
		IgnoreInvalidName = 64,
		/// <summary>Ignore that the certificate has invalid policy when determining certificate verification.</summary>
		// Token: 0x0400080C RID: 2060
		IgnoreInvalidPolicy = 128,
		/// <summary>Ignore that the end certificate (the user certificate) revocation is unknown when determining certificate verification.</summary>
		// Token: 0x0400080D RID: 2061
		IgnoreEndRevocationUnknown = 256,
		/// <summary>Ignore that the certificate trust list (CTL) signer revocation is unknown when determining certificate verification.</summary>
		// Token: 0x0400080E RID: 2062
		IgnoreCtlSignerRevocationUnknown = 512,
		/// <summary>Ignore that the certificate authority revocation is unknown when determining certificate verification.</summary>
		// Token: 0x0400080F RID: 2063
		IgnoreCertificateAuthorityRevocationUnknown = 1024,
		/// <summary>Ignore that the root revocation is unknown when determining certificate verification.</summary>
		// Token: 0x04000810 RID: 2064
		IgnoreRootRevocationUnknown = 2048,
		/// <summary>All flags pertaining to verification are included.</summary>
		// Token: 0x04000811 RID: 2065
		AllFlags = 4095
	}
}
