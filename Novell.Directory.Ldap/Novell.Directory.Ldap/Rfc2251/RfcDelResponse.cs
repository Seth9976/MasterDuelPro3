using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x02000079 RID: 121
	public class RfcDelResponse : RfcLdapResult
	{
		// Token: 0x06000402 RID: 1026 RVA: 0x0001170D File Offset: 0x0000F90D
		[CLSCompliant(false)]
		public RfcDelResponse(Asn1Decoder dec, Stream in_Renamed, int len)
			: base(dec, in_Renamed, len)
		{
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x00011718 File Offset: 0x0000F918
		public RfcDelResponse(Asn1Enumerated resultCode, RfcLdapDN matchedDN, RfcLdapString errorMessage, RfcReferral referral)
			: base(resultCode, matchedDN, errorMessage, referral)
		{
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x00011C56 File Offset: 0x0000FE56
		public override Asn1Identifier getIdentifier()
		{
			return new Asn1Identifier(1, true, 11);
		}
	}
}
