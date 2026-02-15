using System;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Specifies the type of name the X509 certificate contains.</summary>
	// Token: 0x020001B4 RID: 436
	public enum X509NameType
	{
		/// <summary>The simple name of a subject or issuer of an X509 certificate.</summary>
		// Token: 0x040007F1 RID: 2033
		SimpleName,
		/// <summary>The email address of the subject or issuer associated of an X509 certificate.</summary>
		// Token: 0x040007F2 RID: 2034
		EmailName,
		/// <summary>The UPN name of the subject or issuer of an X509 certificate.</summary>
		// Token: 0x040007F3 RID: 2035
		UpnName,
		/// <summary>The DNS name associated with the alternative name of either the subject or issuer of an X509 certificate.</summary>
		// Token: 0x040007F4 RID: 2036
		DnsName,
		/// <summary>The DNS name associated with the alternative name of either the subject or the issuer of an X.509 certificate.  This value is equivalent to the <see cref="F:System.Security.Cryptography.X509Certificates.X509NameType.DnsName" /> value.</summary>
		// Token: 0x040007F5 RID: 2037
		DnsFromAlternativeName,
		/// <summary>The URL address associated with the alternative name of either the subject or issuer of an X509 certificate.</summary>
		// Token: 0x040007F6 RID: 2038
		UrlName
	}
}
