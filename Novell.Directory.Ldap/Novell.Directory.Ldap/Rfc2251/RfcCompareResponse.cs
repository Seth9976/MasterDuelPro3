using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x02000075 RID: 117
	public class RfcCompareResponse : RfcLdapResult
	{
		// Token: 0x060003EC RID: 1004 RVA: 0x0001170D File Offset: 0x0000F90D
		[CLSCompliant(false)]
		public RfcCompareResponse(Asn1Decoder dec, Stream in_Renamed, int len)
			: base(dec, in_Renamed, len)
		{
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x00011718 File Offset: 0x0000F918
		public RfcCompareResponse(Asn1Enumerated resultCode, RfcLdapDN matchedDN, RfcLdapString errorMessage, RfcReferral referral)
			: base(resultCode, matchedDN, errorMessage, referral)
		{
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x00011A59 File Offset: 0x0000FC59
		public override Asn1Identifier getIdentifier()
		{
			return new Asn1Identifier(1, true, 15);
		}
	}
}
