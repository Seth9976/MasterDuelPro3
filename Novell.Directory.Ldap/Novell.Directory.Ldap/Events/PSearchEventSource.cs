using System;
using Novell.Directory.Ldap.Controls;

namespace Novell.Directory.Ldap.Events
{
	// Token: 0x020000C4 RID: 196
	public class PSearchEventSource : LdapEventSource
	{
		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06000503 RID: 1283 RVA: 0x00015C9C File Offset: 0x00013E9C
		// (remove) Token: 0x06000504 RID: 1284 RVA: 0x00015CBB File Offset: 0x00013EBB
		public event PSearchEventSource.SearchResultEventHandler SearchResultEvent
		{
			add
			{
				this.search_result_event = (PSearchEventSource.SearchResultEventHandler)Delegate.Combine(this.search_result_event, value);
				base.ListenerAdded();
			}
			remove
			{
				this.search_result_event = (PSearchEventSource.SearchResultEventHandler)Delegate.Remove(this.search_result_event, value);
				base.ListenerRemoved();
			}
		}

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x06000505 RID: 1285 RVA: 0x00015CDA File Offset: 0x00013EDA
		// (remove) Token: 0x06000506 RID: 1286 RVA: 0x00015CF9 File Offset: 0x00013EF9
		public event PSearchEventSource.SearchReferralEventHandler SearchReferralEvent
		{
			add
			{
				this.search_referral_event = (PSearchEventSource.SearchReferralEventHandler)Delegate.Combine(this.search_referral_event, value);
				base.ListenerAdded();
			}
			remove
			{
				this.search_referral_event = (PSearchEventSource.SearchReferralEventHandler)Delegate.Remove(this.search_referral_event, value);
				base.ListenerRemoved();
			}
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x00015D18 File Offset: 0x00013F18
		protected override int GetListeners()
		{
			int num = 0;
			if (this.search_result_event != null)
			{
				num = this.search_result_event.GetInvocationList().Length;
			}
			if (this.search_referral_event != null)
			{
				num += this.search_referral_event.GetInvocationList().Length;
			}
			return num;
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x00015D58 File Offset: 0x00013F58
		public PSearchEventSource(LdapConnection conn, string searchBase, int scope, string filter, string[] attrs, bool typesOnly, LdapSearchConstraints constraints, LdapEventType eventchangetype, bool changeonly)
		{
			if (conn == null || searchBase == null || filter == null || attrs == null)
			{
				throw new ArgumentException("Null argument specified");
			}
			this.mConnection = conn;
			this.mSearchBase = searchBase;
			this.mScope = scope;
			this.mFilter = filter;
			this.mAttrs = attrs;
			this.mTypesOnly = typesOnly;
			this.mEventChangeType = eventchangetype;
			if (constraints == null)
			{
				this.mSearchConstraints = new LdapSearchConstraints();
			}
			else
			{
				this.mSearchConstraints = constraints;
			}
			LdapPersistSearchControl ldapPersistSearchControl = new LdapPersistSearchControl((int)eventchangetype, changeonly, true, true);
			this.mSearchConstraints.setControls(ldapPersistSearchControl);
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x00015DEC File Offset: 0x00013FEC
		protected override void StartSearchAndPolling()
		{
			this.mQueue = this.mConnection.Search(this.mSearchBase, this.mScope, this.mFilter, this.mAttrs, this.mTypesOnly, null, this.mSearchConstraints);
			int[] messageIDs = this.mQueue.MessageIDs;
			if (messageIDs.Length != 1)
			{
				throw new LdapException(null, 82, "Unable to Obtain Message Id");
			}
			base.StartEventPolling(this.mQueue, this.mConnection, messageIDs[0]);
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x00015E64 File Offset: 0x00014064
		protected override void StopSearchAndPolling()
		{
			this.mConnection.Abandon(this.mQueue);
			base.StopEventPolling();
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x00015E80 File Offset: 0x00014080
		protected override bool NotifyEventListeners(LdapMessage sourceMessage, EventClassifiers aClassification, int nType)
		{
			bool flag = false;
			if (sourceMessage == null)
			{
				return flag;
			}
			int type = sourceMessage.Type;
			if (type != 4)
			{
				if (type != 5)
				{
					if (type == 19 && this.search_referral_event != null)
					{
						this.search_referral_event(this, new SearchReferralEventArgs(sourceMessage, aClassification, (LdapEventType)nType));
						flag = true;
					}
				}
				else
				{
					base.NotifyDirectoryListeners(new LdapEventArgs(sourceMessage, EventClassifiers.CLASSIFICATION_LDAP_PSEARCH, LdapEventType.LDAP_PSEARCH_ANY));
					flag = true;
				}
			}
			else if (this.search_result_event != null)
			{
				LdapEventType ldapEventType = LdapEventType.TYPE_UNKNOWN;
				foreach (LdapControl ldapControl in sourceMessage.Controls)
				{
					if (ldapControl is LdapEntryChangeControl)
					{
						ldapEventType = (LdapEventType)((LdapEntryChangeControl)ldapControl).ChangeType;
					}
				}
				this.search_result_event(this, new SearchResultEventArgs(sourceMessage, aClassification, ldapEventType));
				flag = true;
			}
			return flag;
		}

		// Token: 0x04000356 RID: 854
		protected PSearchEventSource.SearchResultEventHandler search_result_event;

		// Token: 0x04000357 RID: 855
		protected PSearchEventSource.SearchReferralEventHandler search_referral_event;

		// Token: 0x04000358 RID: 856
		protected LdapConnection mConnection;

		// Token: 0x04000359 RID: 857
		protected string mSearchBase;

		// Token: 0x0400035A RID: 858
		protected int mScope;

		// Token: 0x0400035B RID: 859
		protected string[] mAttrs;

		// Token: 0x0400035C RID: 860
		protected string mFilter;

		// Token: 0x0400035D RID: 861
		protected bool mTypesOnly;

		// Token: 0x0400035E RID: 862
		protected LdapSearchConstraints mSearchConstraints;

		// Token: 0x0400035F RID: 863
		protected LdapEventType mEventChangeType;

		// Token: 0x04000360 RID: 864
		protected LdapSearchQueue mQueue;

		// Token: 0x020000C5 RID: 197
		// (Invoke) Token: 0x0600050D RID: 1293
		public delegate void SearchResultEventHandler(object source, SearchResultEventArgs objArgs);

		// Token: 0x020000C6 RID: 198
		// (Invoke) Token: 0x06000511 RID: 1297
		public delegate void SearchReferralEventHandler(object source, SearchReferralEventArgs objArgs);
	}
}
