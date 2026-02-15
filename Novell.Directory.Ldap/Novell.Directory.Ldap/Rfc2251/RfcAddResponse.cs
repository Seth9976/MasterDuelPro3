using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x02000069 RID: 105
	public class RfcAddResponse : RfcLdapResult
	{
		// Token: 0x060003C1 RID: 961 RVA: 0x0001170D File Offset: 0x0000F90D
		[CLSCompliant(false)]
		public RfcAddResponse(Asn1Decoder dec, Stream in_Renamed, int len)
			: base(dec, in_Renamed, len)
		{
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x00011718 File Offset: 0x0000F918
		public RfcAddResponse(Asn1Enumerated resultCode, RfcLdapDN matchedDN, RfcLdapString errorMessage, RfcReferral referral)
			: base(resultCode, matchedDN, errorMessage, referral)
		{
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x00011725 File Offset: 0x0000F925
		public override Asn1Identifier getIdentifier()
		{
			return new Asn1Identifier(1, true, 9);
		}
	}
}
