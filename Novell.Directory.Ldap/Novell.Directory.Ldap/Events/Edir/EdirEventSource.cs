using System;

namespace Novell.Directory.Ldap.Events.Edir
{
	// Token: 0x020000D2 RID: 210
	public class EdirEventSource : LdapEventSource
	{
		// Token: 0x14000007 RID: 7
		// (add) Token: 0x06000529 RID: 1321 RVA: 0x000163C8 File Offset: 0x000145C8
		// (remove) Token: 0x0600052A RID: 1322 RVA: 0x000163E7 File Offset: 0x000145E7
		public event EdirEventSource.EdirEventHandler EdirEvent
		{
			add
			{
				this.edir_event = (EdirEventSource.EdirEventHandler)Delegate.Combine(this.edir_event, value);
				base.ListenerAdded();
			}
			remove
			{
				this.edir_event = (EdirEventSource.EdirEventHandler)Delegate.Remove(this.edir_event, value);
				base.ListenerRemoved();
			}
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x00016408 File Offset: 0x00014608
		protected override int GetListeners()
		{
			int num = 0;
			if (this.edir_event != null)
			{
				num = this.edir_event.GetInvocationList().Length;
			}
			return num;
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x0001642E File Offset: 0x0001462E
		public EdirEventSource(EdirEventSpecifier[] specifier, LdapConnection conn)
		{
			if (specifier == null || conn == null)
			{
				throw new ArgumentException("Null argument specified");
			}
			this.mRequestOperation = new MonitorEventRequest(specifier);
			this.mConnection = conn;
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x0001645C File Offset: 0x0001465C
		protected override void StartSearchAndPolling()
		{
			this.mQueue = this.mConnection.ExtendedOperation(this.mRequestOperation, null, null);
			int[] messageIDs = this.mQueue.MessageIDs;
			if (messageIDs.Length != 1)
			{
				throw new LdapException(null, 82, "Unable to Obtain Message Id");
			}
			base.StartEventPolling(this.mQueue, this.mConnection, messageIDs[0]);
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x000164B7 File Offset: 0x000146B7
		protected override void StopSearchAndPolling()
		{
			this.mConnection.Abandon(this.mQueue);
			base.StopEventPolling();
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x000164D0 File Offset: 0x000146D0
		protected override bool NotifyEventListeners(LdapMessage sourceMessage, EventClassifiers aClassification, int nType)
		{
			bool flag = false;
			if (this.edir_event != null && sourceMessage != null && sourceMessage.Type == 25 && sourceMessage is EdirEventIntermediateResponse)
			{
				this.edir_event(this, new EdirEventArgs(sourceMessage, EventClassifiers.CLASSIFICATION_EDIR_EVENT));
				flag = true;
			}
			return flag;
		}

		// Token: 0x04000464 RID: 1124
		protected EdirEventSource.EdirEventHandler edir_event;

		// Token: 0x04000465 RID: 1125
		protected LdapConnection mConnection;

		// Token: 0x04000466 RID: 1126
		protected MonitorEventRequest mRequestOperation;

		// Token: 0x04000467 RID: 1127
		protected LdapResponseQueue mQueue;

		// Token: 0x020000D3 RID: 211
		// (Invoke) Token: 0x06000531 RID: 1329
		public delegate void EdirEventHandler(object source, EdirEventArgs objEdirEventArgs);
	}
}
