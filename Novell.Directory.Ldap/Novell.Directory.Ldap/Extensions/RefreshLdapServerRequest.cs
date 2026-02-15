using System;

namespace Novell.Directory.Ldap.Extensions
{
	// Token: 0x020000AF RID: 175
	public class RefreshLdapServerRequest : LdapExtendedOperation
	{
		// Token: 0x060004CB RID: 1227 RVA: 0x0001532C File Offset: 0x0001352C
		public RefreshLdapServerRequest()
			: base("2.16.840.1.113719.1.27.100.9", null)
		{
		}
	}
}
