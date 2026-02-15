using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x02000093 RID: 147
	public class RfcSearchResultDone : RfcLdapResult
	{
		// Token: 0x0600048E RID: 1166 RVA: 0x0001170D File Offset: 0x0000F90D
		[CLSCompliant(false)]
		public RfcSearchResultDone(Asn1Decoder dec, Stream in_Renamed, int len)
			: base(dec, in_Renamed, len)
		{
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x00011718 File Offset: 0x0000F918
		public RfcSearchResultDone(Asn1Enumerated resultCode, RfcLdapDN matchedDN, RfcLdapString errorMessage, RfcReferral referral)
			: base(resultCode, matchedDN, errorMessage, referral)
		{
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x00013EBE File Offset: 0x000120BE
		public override Asn1Identifier getIdentifier()
		{
			return new Asn1Identifier(1, true, 5);
		}
	}
}
