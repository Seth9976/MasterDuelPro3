using System;

namespace Novell.Directory.Ldap.Events
{
	// Token: 0x020000BB RID: 187
	public class DirectoryExceptionEventArgs : BaseEventArgs
	{
		// Token: 0x17000124 RID: 292
		// (get) Token: 0x060004DA RID: 1242 RVA: 0x00015839 File Offset: 0x00013A39
		public LdapException LdapExceptionObject
		{
			get
			{
				return this.ldap_exception_object;
			}
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x00015841 File Offset: 0x00013A41
		public DirectoryExceptionEventArgs(LdapMessage message, LdapException ldapException)
			: base(message)
		{
			this.ldap_exception_object = ldapException;
		}

		// Token: 0x04000339 RID: 825
		protected LdapException ldap_exception_object;
	}
}
