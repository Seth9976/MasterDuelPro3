using System;

namespace Novell.Directory.Ldap
{
	// Token: 0x02000021 RID: 33
	public interface LdapAuthHandler : LdapReferralHandler
	{
		// Token: 0x0600012C RID: 300
		LdapAuthProvider getAuthProvider(string host, int port);
	}
}
