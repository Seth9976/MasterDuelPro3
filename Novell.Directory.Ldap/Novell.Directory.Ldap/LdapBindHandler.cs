using System;

namespace Novell.Directory.Ldap
{
	// Token: 0x02000023 RID: 35
	public interface LdapBindHandler : LdapReferralHandler
	{
		// Token: 0x06000130 RID: 304
		LdapConnection Bind(string[] ldapurl, LdapConnection conn);
	}
}
