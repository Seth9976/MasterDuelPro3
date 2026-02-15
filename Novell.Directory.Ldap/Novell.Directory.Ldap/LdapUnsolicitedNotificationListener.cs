using System;

namespace Novell.Directory.Ldap
{
	// Token: 0x0200004D RID: 77
	public interface LdapUnsolicitedNotificationListener
	{
		// Token: 0x060002D2 RID: 722
		void messageReceived(LdapExtendedResponse msg);
	}
}
