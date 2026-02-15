using System;

namespace Novell.Directory.Ldap.Controls
{
	// Token: 0x020000E7 RID: 231
	public class LdapSortKey
	{
		// Token: 0x17000179 RID: 377
		// (get) Token: 0x060005AD RID: 1453 RVA: 0x000182A4 File Offset: 0x000164A4
		public virtual string Key
		{
			get
			{
				return this.key;
			}
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x060005AE RID: 1454 RVA: 0x000182AC File Offset: 0x000164AC
		public virtual bool Reverse
		{
			get
			{
				return this.reverse;
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x060005AF RID: 1455 RVA: 0x000182B4 File Offset: 0x000164B4
		public virtual string MatchRule
		{
			get
			{
				return this.matchRule;
			}
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x000182BC File Offset: 0x000164BC
		public LdapSortKey(string keyDescription)
		{
			this.matchRule = null;
			this.reverse = false;
			string text = keyDescription;
			if (text[0] == '-')
			{
				text = text.Substring(1);
				this.reverse = true;
			}
			int num = text.IndexOf(":");
			if (num != -1)
			{
				this.key = text.Substring(0, num);
				this.matchRule = text.Substring(num + 1);
				return;
			}
			this.key = text;
		}

		// Token: 0x060005B1 RID: 1457 RVA: 0x0001832E File Offset: 0x0001652E
		public LdapSortKey(string key, bool reverse)
			: this(key, reverse, null)
		{
		}

		// Token: 0x060005B2 RID: 1458 RVA: 0x00018339 File Offset: 0x00016539
		public LdapSortKey(string key, bool reverse, string matchRule)
		{
			this.key = key;
			this.reverse = reverse;
			this.matchRule = matchRule;
		}

		// Token: 0x040004C4 RID: 1220
		private string key;

		// Token: 0x040004C5 RID: 1221
		private bool reverse;

		// Token: 0x040004C6 RID: 1222
		private string matchRule;
	}
}
