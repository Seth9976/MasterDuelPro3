using System;
using Novell.Directory.Ldap.Rfc2251;

namespace Novell.Directory.Ldap
{
	// Token: 0x0200001B RID: 27
	public class LdapAbandonRequest : LdapMessage
	{
		// Token: 0x060000E4 RID: 228 RVA: 0x000048A8 File Offset: 0x00002AA8
		public LdapAbandonRequest(int id, LdapControl[] cont)
			: base(16, new RfcAbandonRequest(id), cont)
		{
		}
	}
}
