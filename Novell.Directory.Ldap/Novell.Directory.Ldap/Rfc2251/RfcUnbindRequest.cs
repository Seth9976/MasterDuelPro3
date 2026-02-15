using System;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x02000097 RID: 151
	public class RfcUnbindRequest : Asn1Null, RfcRequest
	{
		// Token: 0x06000499 RID: 1177 RVA: 0x00013EF3 File Offset: 0x000120F3
		public override Asn1Identifier getIdentifier()
		{
			return new Asn1Identifier(1, false, 2);
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x00013EFD File Offset: 0x000120FD
		public RfcRequest dupRequest(string base_Renamed, string filter, bool request)
		{
			throw new LdapException("NO_DUP_REQUEST", new object[] { "unbind" }, 92, null);
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x0001169E File Offset: 0x0000F89E
		public string getRequestDN()
		{
			return null;
		}
	}
}
