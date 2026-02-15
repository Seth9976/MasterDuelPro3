using System;

namespace Novell.Directory.Ldap.Events
{
	// Token: 0x020000C7 RID: 199
	public class SearchReferralEventArgs : LdapEventArgs
	{
		// Token: 0x06000514 RID: 1300 RVA: 0x00015F3A File Offset: 0x0001413A
		public SearchReferralEventArgs(LdapMessage sourceMessage, EventClassifiers aClassification, LdapEventType aType)
			: base(sourceMessage, EventClassifiers.CLASSIFICATION_LDAP_PSEARCH, LdapEventType.LDAP_PSEARCH_ANY)
		{
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x00015F46 File Offset: 0x00014146
		public string[] getUrls()
		{
			return ((LdapSearchResultReference)this.ldap_message).Referrals;
		}
	}
}
