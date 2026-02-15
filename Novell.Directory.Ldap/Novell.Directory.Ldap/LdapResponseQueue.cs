using System;

namespace Novell.Directory.Ldap
{
	// Token: 0x02000042 RID: 66
	public class LdapResponseQueue : LdapMessageQueue
	{
		// Token: 0x06000274 RID: 628 RVA: 0x0000AD0B File Offset: 0x00008F0B
		internal LdapResponseQueue(MessageAgent agent)
			: base("LdapResponseQueue", agent)
		{
		}

		// Token: 0x06000275 RID: 629 RVA: 0x0000AD1C File Offset: 0x00008F1C
		public virtual void merge(LdapMessageQueue queue2)
		{
			LdapResponseQueue ldapResponseQueue = (LdapResponseQueue)queue2;
			this.agent.merge(ldapResponseQueue.MessageAgent);
		}
	}
}
