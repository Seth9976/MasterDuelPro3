using System;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x02000067 RID: 103
	internal class RfcAbandonRequest : RfcMessageID, RfcRequest
	{
		// Token: 0x060003B7 RID: 951 RVA: 0x0001166D File Offset: 0x0000F86D
		public RfcAbandonRequest(int msgId)
			: base(msgId)
		{
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x00011676 File Offset: 0x0000F876
		public override Asn1Identifier getIdentifier()
		{
			return new Asn1Identifier(1, false, 16);
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x00011681 File Offset: 0x0000F881
		public RfcRequest dupRequest(string base_Renamed, string filter, bool reference)
		{
			throw new LdapException("NO_DUP_REQUEST", new object[] { "Abandon" }, 92, null);
		}

		// Token: 0x060003BA RID: 954 RVA: 0x0001169E File Offset: 0x0000F89E
		public string getRequestDN()
		{
			return null;
		}
	}
}
