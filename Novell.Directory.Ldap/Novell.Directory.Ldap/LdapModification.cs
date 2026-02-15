using System;

namespace Novell.Directory.Ldap
{
	// Token: 0x0200003A RID: 58
	public class LdapModification
	{
		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600023A RID: 570 RVA: 0x00009A73 File Offset: 0x00007C73
		public virtual LdapAttribute Attribute
		{
			get
			{
				return this.attr;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x0600023B RID: 571 RVA: 0x00009A7B File Offset: 0x00007C7B
		public virtual int Op
		{
			get
			{
				return this.op;
			}
		}

		// Token: 0x0600023C RID: 572 RVA: 0x00009A83 File Offset: 0x00007C83
		public LdapModification(int op, LdapAttribute attr)
		{
			this.op = op;
			this.attr = attr;
		}

		// Token: 0x04000144 RID: 324
		private int op;

		// Token: 0x04000145 RID: 325
		private LdapAttribute attr;

		// Token: 0x04000146 RID: 326
		public const int ADD = 0;

		// Token: 0x04000147 RID: 327
		public const int DELETE = 1;

		// Token: 0x04000148 RID: 328
		public const int REPLACE = 2;
	}
}
