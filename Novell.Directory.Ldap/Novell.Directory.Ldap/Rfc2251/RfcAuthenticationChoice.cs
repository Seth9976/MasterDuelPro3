using System;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x02000071 RID: 113
	public class RfcAuthenticationChoice : Asn1Choice
	{
		// Token: 0x060003D0 RID: 976 RVA: 0x000117C2 File Offset: 0x0000F9C2
		public RfcAuthenticationChoice(Asn1Tagged choice)
			: base(choice)
		{
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x000117CB File Offset: 0x0000F9CB
		[CLSCompliant(false)]
		public RfcAuthenticationChoice(string mechanism, sbyte[] credentials)
			: base(new Asn1Tagged(new Asn1Identifier(2, true, 3), new RfcSaslCredentials(new RfcLdapString(mechanism), (credentials != null) ? new Asn1OctetString(credentials) : null), false))
		{
		}
	}
}
