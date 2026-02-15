using System;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x02000090 RID: 144
	public interface RfcResponse
	{
		// Token: 0x06000483 RID: 1155
		Asn1Enumerated getResultCode();

		// Token: 0x06000484 RID: 1156
		RfcLdapDN getMatchedDN();

		// Token: 0x06000485 RID: 1157
		RfcLdapString getErrorMessage();

		// Token: 0x06000486 RID: 1158
		RfcReferral getReferral();
	}
}
