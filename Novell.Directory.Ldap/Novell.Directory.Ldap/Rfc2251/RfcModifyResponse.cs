using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x0200008C RID: 140
	public class RfcModifyResponse : RfcLdapResult
	{
		// Token: 0x0600047C RID: 1148 RVA: 0x0001170D File Offset: 0x0000F90D
		[CLSCompliant(false)]
		public RfcModifyResponse(Asn1Decoder dec, Stream in_Renamed, int len)
			: base(dec, in_Renamed, len)
		{
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x00011718 File Offset: 0x0000F918
		public RfcModifyResponse(Asn1Enumerated resultCode, RfcLdapDN matchedDN, RfcLdapString errorMessage, RfcReferral referral)
			: base(resultCode, matchedDN, errorMessage, referral)
		{
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x00013DBD File Offset: 0x00011FBD
		public override Asn1Identifier getIdentifier()
		{
			return new Asn1Identifier(1, true, 7);
		}
	}
}
