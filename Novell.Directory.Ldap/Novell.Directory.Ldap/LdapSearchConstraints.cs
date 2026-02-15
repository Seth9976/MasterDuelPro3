using System;
using System.Collections;

namespace Novell.Directory.Ldap
{
	// Token: 0x02000045 RID: 69
	public class LdapSearchConstraints : LdapConstraints
	{
		// Token: 0x060002A3 RID: 675 RVA: 0x0000B53B File Offset: 0x0000973B
		private void InitBlock()
		{
			this.dereference = 0;
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060002A4 RID: 676 RVA: 0x0000B544 File Offset: 0x00009744
		// (set) Token: 0x060002A5 RID: 677 RVA: 0x0000B54C File Offset: 0x0000974C
		public virtual int BatchSize
		{
			get
			{
				return this.batchSize;
			}
			set
			{
				this.batchSize = value;
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060002A6 RID: 678 RVA: 0x0000B555 File Offset: 0x00009755
		// (set) Token: 0x060002A7 RID: 679 RVA: 0x0000B55D File Offset: 0x0000975D
		public virtual int Dereference
		{
			get
			{
				return this.dereference;
			}
			set
			{
				this.dereference = value;
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060002A8 RID: 680 RVA: 0x0000B566 File Offset: 0x00009766
		// (set) Token: 0x060002A9 RID: 681 RVA: 0x0000B56E File Offset: 0x0000976E
		public virtual int MaxResults
		{
			get
			{
				return this.maxResults;
			}
			set
			{
				this.maxResults = value;
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060002AA RID: 682 RVA: 0x0000B577 File Offset: 0x00009777
		// (set) Token: 0x060002AB RID: 683 RVA: 0x0000B57F File Offset: 0x0000977F
		public virtual int ServerTimeLimit
		{
			get
			{
				return this.serverTimeLimit;
			}
			set
			{
				this.serverTimeLimit = value;
			}
		}

		// Token: 0x060002AC RID: 684 RVA: 0x0000B588 File Offset: 0x00009788
		public LdapSearchConstraints()
		{
			this.InitBlock();
		}

		// Token: 0x060002AD RID: 685 RVA: 0x0000B5A8 File Offset: 0x000097A8
		public LdapSearchConstraints(LdapConstraints cons)
			: base(cons.TimeLimit, cons.ReferralFollowing, cons.getReferralHandler(), cons.HopLimit)
		{
			this.InitBlock();
			LdapControl[] controls = cons.getControls();
			if (controls != null)
			{
				LdapControl[] array = new LdapControl[controls.Length];
				controls.CopyTo(array, 0);
				base.setControls(array);
			}
			Hashtable properties = cons.Properties;
			if (properties != null)
			{
				base.Properties = (Hashtable)properties.Clone();
			}
			if (cons is LdapSearchConstraints)
			{
				LdapSearchConstraints ldapSearchConstraints = (LdapSearchConstraints)cons;
				this.serverTimeLimit = ldapSearchConstraints.ServerTimeLimit;
				this.dereference = ldapSearchConstraints.Dereference;
				this.maxResults = ldapSearchConstraints.MaxResults;
				this.batchSize = ldapSearchConstraints.BatchSize;
			}
		}

		// Token: 0x060002AE RID: 686 RVA: 0x0000B668 File Offset: 0x00009868
		public LdapSearchConstraints(int msLimit, int serverTimeLimit, int dereference, int maxResults, bool doReferrals, int batchSize, LdapReferralHandler handler, int hop_limit)
			: base(msLimit, doReferrals, handler, hop_limit)
		{
			this.InitBlock();
			this.serverTimeLimit = serverTimeLimit;
			this.dereference = dereference;
			this.maxResults = maxResults;
			this.batchSize = batchSize;
		}

		// Token: 0x04000168 RID: 360
		private int dereference;

		// Token: 0x04000169 RID: 361
		private int serverTimeLimit;

		// Token: 0x0400016A RID: 362
		private int maxResults = 1000;

		// Token: 0x0400016B RID: 363
		private int batchSize = 1;

		// Token: 0x0400016C RID: 364
		private static object nameLock = new object();

		// Token: 0x0400016D RID: 365
		private static int lSConsNum;

		// Token: 0x0400016E RID: 366
		private string name;

		// Token: 0x0400016F RID: 367
		public const int DEREF_NEVER = 0;

		// Token: 0x04000170 RID: 368
		public const int DEREF_SEARCHING = 1;

		// Token: 0x04000171 RID: 369
		public const int DEREF_FINDING = 2;

		// Token: 0x04000172 RID: 370
		public const int DEREF_ALWAYS = 3;
	}
}
