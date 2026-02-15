using System;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Defines the status of an X509 chain.</summary>
	// Token: 0x020001B1 RID: 433
	[Flags]
	public enum X509ChainStatusFlags
	{
		/// <summary>Specifies that the X509 chain has no errors.</summary>
		// Token: 0x040007BB RID: 1979
		NoError = 0,
		/// <summary>Specifies that the X509 chain is not valid due to an invalid time value, such as a value that indicates an expired certificate.</summary>
		// Token: 0x040007BC RID: 1980
		NotTimeValid = 1,
		/// <summary>Deprecated. Specifies that the CA (certificate authority) certificate and the issued certificate have validity periods that are not nested. For example, the CA cert can be valid from January 1 to December 1 and the issued certificate from January 2 to December 2, which would mean the validity periods are not nested.</summary>
		// Token: 0x040007BD RID: 1981
		NotTimeNested = 2,
		/// <summary>Specifies that the X509 chain is invalid due to a revoked certificate.</summary>
		// Token: 0x040007BE RID: 1982
		Revoked = 4,
		/// <summary>Specifies that the X509 chain is invalid due to an invalid certificate signature.</summary>
		// Token: 0x040007BF RID: 1983
		NotSignatureValid = 8,
		/// <summary>Specifies that the key usage is not valid.</summary>
		// Token: 0x040007C0 RID: 1984
		NotValidForUsage = 16,
		/// <summary>Specifies that the X509 chain is invalid due to an untrusted root certificate.</summary>
		// Token: 0x040007C1 RID: 1985
		UntrustedRoot = 32,
		/// <summary>Specifies that it is not possible to determine whether the certificate has been revoked. This can be due to the certificate revocation list (CRL) being offline or unavailable.</summary>
		// Token: 0x040007C2 RID: 1986
		RevocationStatusUnknown = 64,
		/// <summary>Specifies that the X509 chain could not be built.</summary>
		// Token: 0x040007C3 RID: 1987
		Cyclic = 128,
		/// <summary>Specifies that the X509 chain is invalid due to an invalid extension.</summary>
		// Token: 0x040007C4 RID: 1988
		InvalidExtension = 256,
		/// <summary>Specifies that the X509 chain is invalid due to invalid policy constraints.</summary>
		// Token: 0x040007C5 RID: 1989
		InvalidPolicyConstraints = 512,
		/// <summary>Specifies that the X509 chain is invalid due to invalid basic constraints.</summary>
		// Token: 0x040007C6 RID: 1990
		InvalidBasicConstraints = 1024,
		/// <summary>Specifies that the X509 chain is invalid due to invalid name constraints.</summary>
		// Token: 0x040007C7 RID: 1991
		InvalidNameConstraints = 2048,
		/// <summary>Specifies that the certificate does not have a supported name constraint or has a name constraint that is unsupported.</summary>
		// Token: 0x040007C8 RID: 1992
		HasNotSupportedNameConstraint = 4096,
		/// <summary>Specifies that the certificate has an undefined name constraint.</summary>
		// Token: 0x040007C9 RID: 1993
		HasNotDefinedNameConstraint = 8192,
		/// <summary>Specifies that the certificate has an impermissible name constraint.</summary>
		// Token: 0x040007CA RID: 1994
		HasNotPermittedNameConstraint = 16384,
		/// <summary>Specifies that the X509 chain is invalid because a certificate has excluded a name constraint.</summary>
		// Token: 0x040007CB RID: 1995
		HasExcludedNameConstraint = 32768,
		/// <summary>Specifies that the X509 chain could not be built up to the root certificate.</summary>
		// Token: 0x040007CC RID: 1996
		PartialChain = 65536,
		/// <summary>Specifies that the certificate trust list (CTL) is not valid because of an invalid time value, such as one that indicates that the CTL has expired.</summary>
		// Token: 0x040007CD RID: 1997
		CtlNotTimeValid = 131072,
		/// <summary>Specifies that the certificate trust list (CTL) contains an invalid signature.</summary>
		// Token: 0x040007CE RID: 1998
		CtlNotSignatureValid = 262144,
		/// <summary>Specifies that the certificate trust list (CTL) is not valid for this use.</summary>
		// Token: 0x040007CF RID: 1999
		CtlNotValidForUsage = 524288,
		/// <summary>Specifies that the online certificate revocation list (CRL) the X509 chain relies on is currently offline.</summary>
		// Token: 0x040007D0 RID: 2000
		OfflineRevocation = 16777216,
		/// <summary>Specifies that there is no certificate policy extension in the certificate. This error would occur if a group policy has specified that all certificates must have a certificate policy.</summary>
		// Token: 0x040007D1 RID: 2001
		NoIssuanceChainPolicy = 33554432,
		// Token: 0x040007D2 RID: 2002
		ExplicitDistrust = 67108864,
		// Token: 0x040007D3 RID: 2003
		HasNotSupportedCriticalExtension = 134217728,
		// Token: 0x040007D4 RID: 2004
		HasWeakSignature = 1048576
	}
}
