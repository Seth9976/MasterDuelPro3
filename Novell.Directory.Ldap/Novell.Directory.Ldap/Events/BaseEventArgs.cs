using System;

namespace Novell.Directory.Ldap.Events
{
	// Token: 0x020000B9 RID: 185
	public class BaseEventArgs : EventArgs
	{
		// Token: 0x17000122 RID: 290
		// (get) Token: 0x060004D5 RID: 1237 RVA: 0x00015801 File Offset: 0x00013A01
		public LdapMessage ContianedEventInformation
		{
			get
			{
				return this.ldap_message;
			}
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x00015809 File Offset: 0x00013A09
		public BaseEventArgs(LdapMessage message)
		{
			this.ldap_message = message;
		}

		// Token: 0x04000337 RID: 823
		protected LdapMessage ldap_message;
	}
}
