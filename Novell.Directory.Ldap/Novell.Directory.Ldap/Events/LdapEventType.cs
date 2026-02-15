using System;

namespace Novell.Directory.Ldap.Events
{
	// Token: 0x020000BE RID: 190
	public enum LdapEventType
	{
		// Token: 0x04000340 RID: 832
		TYPE_UNKNOWN = -1,
		// Token: 0x04000341 RID: 833
		LDAP_PSEARCH_ADD = 1,
		// Token: 0x04000342 RID: 834
		LDAP_PSEARCH_DELETE,
		// Token: 0x04000343 RID: 835
		LDAP_PSEARCH_MODIFY = 4,
		// Token: 0x04000344 RID: 836
		LDAP_PSEARCH_MODDN = 8,
		// Token: 0x04000345 RID: 837
		LDAP_PSEARCH_ANY = 15
	}
}
