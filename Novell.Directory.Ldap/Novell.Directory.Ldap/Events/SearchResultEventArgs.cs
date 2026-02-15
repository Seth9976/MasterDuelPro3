using System;
using System.Text;

namespace Novell.Directory.Ldap.Events
{
	// Token: 0x020000C8 RID: 200
	public class SearchResultEventArgs : LdapEventArgs
	{
		// Token: 0x06000516 RID: 1302 RVA: 0x00015F58 File Offset: 0x00014158
		public SearchResultEventArgs(LdapMessage sourceMessage, EventClassifiers aClassification, LdapEventType aType)
			: base(sourceMessage, EventClassifiers.CLASSIFICATION_LDAP_PSEARCH, aType)
		{
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000517 RID: 1303 RVA: 0x00015F63 File Offset: 0x00014163
		public LdapEntry Entry
		{
			get
			{
				return ((LdapSearchResult)this.ldap_message).Entry;
			}
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x00015F78 File Offset: 0x00014178
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("[{0}:", base.GetType());
			stringBuilder.AppendFormat("(Classification={0})", this.eClassification);
			stringBuilder.AppendFormat("(Type={0})", this.getChangeTypeString());
			stringBuilder.AppendFormat("(EventInformation:{0})", this.getStringRepresentaionOfEventInformation());
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x00015FE8 File Offset: 0x000141E8
		private string getStringRepresentaionOfEventInformation()
		{
			StringBuilder stringBuilder = new StringBuilder();
			LdapSearchResult ldapSearchResult = (LdapSearchResult)this.ldap_message;
			stringBuilder.AppendFormat("(Entry={0})", ldapSearchResult.Entry);
			LdapControl[] controls = ldapSearchResult.Controls;
			if (controls != null)
			{
				stringBuilder.Append("(Controls=");
				int num = 0;
				foreach (LdapControl ldapControl in controls)
				{
					stringBuilder.AppendFormat("(Control{0}={1})", ++num, ldapControl.ToString());
				}
				stringBuilder.Append(")");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x00016080 File Offset: 0x00014280
		private string getChangeTypeString()
		{
			LdapEventType eType = this.eType;
			switch (eType)
			{
			case LdapEventType.LDAP_PSEARCH_ADD:
				return "ADD";
			case LdapEventType.LDAP_PSEARCH_DELETE:
				return "DELETE";
			case (LdapEventType)3:
				break;
			case LdapEventType.LDAP_PSEARCH_MODIFY:
				return "MODIFY";
			default:
				if (eType == LdapEventType.LDAP_PSEARCH_MODDN)
				{
					return "MODDN";
				}
				break;
			}
			return "No change type: " + this.eType.ToString();
		}
	}
}
