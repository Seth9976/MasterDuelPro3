using System;

namespace Novell.Directory.Ldap.Utilclass
{
	// Token: 0x0200005E RID: 94
	public class ReferralInfo
	{
		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000372 RID: 882 RVA: 0x0000F836 File Offset: 0x0000DA36
		public virtual LdapUrl ReferralUrl
		{
			get
			{
				return this.referralUrl;
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000373 RID: 883 RVA: 0x0000F83E File Offset: 0x0000DA3E
		public virtual LdapConnection ReferralConnection
		{
			get
			{
				return this.conn;
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000374 RID: 884 RVA: 0x0000F846 File Offset: 0x0000DA46
		public virtual string[] ReferralList
		{
			get
			{
				return this.referralList;
			}
		}

		// Token: 0x06000375 RID: 885 RVA: 0x0000F84E File Offset: 0x0000DA4E
		public ReferralInfo(LdapConnection lc, string[] refList, LdapUrl refUrl)
		{
			this.conn = lc;
			this.referralUrl = refUrl;
			this.referralList = refList;
		}

		// Token: 0x04000218 RID: 536
		private LdapConnection conn;

		// Token: 0x04000219 RID: 537
		private LdapUrl referralUrl;

		// Token: 0x0400021A RID: 538
		private string[] referralList;
	}
}
