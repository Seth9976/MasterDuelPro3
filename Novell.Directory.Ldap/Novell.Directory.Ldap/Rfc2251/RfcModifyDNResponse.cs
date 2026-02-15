using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x0200008A RID: 138
	public class RfcModifyDNResponse : RfcLdapResult
	{
		// Token: 0x06000473 RID: 1139 RVA: 0x0001170D File Offset: 0x0000F90D
		[CLSCompliant(false)]
		public RfcModifyDNResponse(Asn1Decoder dec, Stream in_Renamed, int len)
			: base(dec, in_Renamed, len)
		{
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x00011718 File Offset: 0x0000F918
		public RfcModifyDNResponse(Asn1Enumerated resultCode, RfcLdapDN matchedDN, RfcLdapString errorMessage, RfcReferral referral)
			: base(resultCode, matchedDN, errorMessage, referral)
		{
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x00013D8C File Offset: 0x00011F8C
		public override Asn1Identifier getIdentifier()
		{
			return new Asn1Identifier(1, true, 13);
		}
	}
}
