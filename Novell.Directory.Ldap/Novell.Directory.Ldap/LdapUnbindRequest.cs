using System;
using Novell.Directory.Ldap.Rfc2251;

namespace Novell.Directory.Ldap
{
	// Token: 0x0200004C RID: 76
	public class LdapUnbindRequest : LdapMessage
	{
		// Token: 0x060002D1 RID: 721 RVA: 0x0000C102 File Offset: 0x0000A302
		public LdapUnbindRequest(LdapControl[] cont)
			: base(2, new RfcUnbindRequest(), cont)
		{
		}
	}
}
