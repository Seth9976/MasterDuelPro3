using System;
using Novell.Directory.Ldap.Rfc2251;
using Novell.Directory.Ldap.Utilclass;

namespace Novell.Directory.Ldap
{
	// Token: 0x02000039 RID: 57
	public abstract class LdapMessageQueue
	{
		// Token: 0x1700006E RID: 110
		// (get) Token: 0x0600022F RID: 559 RVA: 0x00009964 File Offset: 0x00007B64
		internal virtual string DebugName
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000230 RID: 560 RVA: 0x0000996C File Offset: 0x00007B6C
		internal virtual MessageAgent MessageAgent
		{
			get
			{
				return this.agent;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000231 RID: 561 RVA: 0x00009974 File Offset: 0x00007B74
		public virtual int[] MessageIDs
		{
			get
			{
				return this.agent.MessageIDs;
			}
		}

		// Token: 0x06000232 RID: 562 RVA: 0x00009981 File Offset: 0x00007B81
		internal LdapMessageQueue(string myname, MessageAgent agent)
		{
			this.agent = agent;
		}

		// Token: 0x06000233 RID: 563 RVA: 0x0000999B File Offset: 0x00007B9B
		public virtual LdapMessage getResponse()
		{
			return this.getResponse(null);
		}

		// Token: 0x06000234 RID: 564 RVA: 0x000099A4 File Offset: 0x00007BA4
		public virtual LdapMessage getResponse(int msgid)
		{
			return this.getResponse(new Integer32(msgid));
		}

		// Token: 0x06000235 RID: 565 RVA: 0x000099B4 File Offset: 0x00007BB4
		private LdapMessage getResponse(Integer32 msgid)
		{
			object ldapMessage;
			if ((ldapMessage = this.agent.getLdapMessage(msgid)) == null)
			{
				return null;
			}
			if (ldapMessage is LdapResponse)
			{
				return (LdapMessage)ldapMessage;
			}
			RfcLdapMessage rfcLdapMessage = (RfcLdapMessage)ldapMessage;
			int type = rfcLdapMessage.Type;
			if (type <= 19)
			{
				if (type == 4)
				{
					return new LdapSearchResult(rfcLdapMessage);
				}
				if (type == 19)
				{
					return new LdapSearchResultReference(rfcLdapMessage);
				}
			}
			else
			{
				if (type == 24)
				{
					new ExtResponseFactory();
					return ExtResponseFactory.convertToExtendedResponse(rfcLdapMessage);
				}
				if (type == 25)
				{
					return IntermediateResponseFactory.convertToIntermediateResponse(rfcLdapMessage);
				}
			}
			return new LdapResponse(rfcLdapMessage);
		}

		// Token: 0x06000236 RID: 566 RVA: 0x00009A3E File Offset: 0x00007C3E
		public virtual bool isResponseReceived()
		{
			return this.agent.isResponseReceived();
		}

		// Token: 0x06000237 RID: 567 RVA: 0x00009A4B File Offset: 0x00007C4B
		public virtual bool isResponseReceived(int msgid)
		{
			return this.agent.isResponseReceived(msgid);
		}

		// Token: 0x06000238 RID: 568 RVA: 0x00009A59 File Offset: 0x00007C59
		public virtual bool isComplete(int msgid)
		{
			return this.agent.isComplete(msgid);
		}

		// Token: 0x04000140 RID: 320
		internal MessageAgent agent;

		// Token: 0x04000141 RID: 321
		internal string name = "";

		// Token: 0x04000142 RID: 322
		internal static object nameLock = new object();

		// Token: 0x04000143 RID: 323
		internal static int queueNum;
	}
}
