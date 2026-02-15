using System;

namespace Novell.Directory.Ldap
{
	// Token: 0x02000022 RID: 34
	public class LdapAuthProvider
	{
		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600012D RID: 301 RVA: 0x00005D27 File Offset: 0x00003F27
		public virtual string DN
		{
			get
			{
				return this.dn;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600012E RID: 302 RVA: 0x00005D2F File Offset: 0x00003F2F
		[CLSCompliant(false)]
		public virtual sbyte[] Password
		{
			get
			{
				return this.password;
			}
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00005D37 File Offset: 0x00003F37
		[CLSCompliant(false)]
		public LdapAuthProvider(string dn, sbyte[] password)
		{
			this.dn = dn;
			this.password = password;
		}

		// Token: 0x04000093 RID: 147
		private string dn;

		// Token: 0x04000094 RID: 148
		private sbyte[] password;
	}
}
