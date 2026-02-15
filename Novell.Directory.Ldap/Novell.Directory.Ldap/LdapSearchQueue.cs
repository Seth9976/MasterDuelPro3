using System;

namespace Novell.Directory.Ldap
{
	// Token: 0x02000046 RID: 70
	public class LdapSearchQueue : LdapMessageQueue
	{
		// Token: 0x060002B0 RID: 688 RVA: 0x0000B6C4 File Offset: 0x000098C4
		internal LdapSearchQueue(MessageAgent agent)
			: base("LdapSearchQueue", agent)
		{
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x0000B6D4 File Offset: 0x000098D4
		public virtual void merge(LdapMessageQueue queue2)
		{
			LdapSearchQueue ldapSearchQueue = (LdapSearchQueue)queue2;
			this.agent.merge(ldapSearchQueue.MessageAgent);
		}
	}
}
