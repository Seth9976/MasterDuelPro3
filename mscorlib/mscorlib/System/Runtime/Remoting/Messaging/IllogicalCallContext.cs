using System;
using System.Collections;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000471 RID: 1137
	internal class IllogicalCallContext
	{
		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x060024D2 RID: 9426 RVA: 0x00096958 File Offset: 0x00094B58
		private Hashtable Datastore
		{
			get
			{
				if (this.m_Datastore == null)
				{
					this.m_Datastore = new Hashtable();
				}
				return this.m_Datastore;
			}
		}

		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x060024D3 RID: 9427 RVA: 0x00096973 File Offset: 0x00094B73
		// (set) Token: 0x060024D4 RID: 9428 RVA: 0x0009697B File Offset: 0x00094B7B
		internal object HostContext
		{
			get
			{
				return this.m_HostContext;
			}
			set
			{
				this.m_HostContext = value;
			}
		}

		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x060024D5 RID: 9429 RVA: 0x00096984 File Offset: 0x00094B84
		internal bool HasUserData
		{
			get
			{
				return this.m_Datastore != null && this.m_Datastore.Count > 0;
			}
		}

		// Token: 0x060024D6 RID: 9430 RVA: 0x0009699E File Offset: 0x00094B9E
		public void FreeNamedDataSlot(string name)
		{
			this.Datastore.Remove(name);
		}

		// Token: 0x060024D7 RID: 9431 RVA: 0x000969AC File Offset: 0x00094BAC
		public IllogicalCallContext CreateCopy()
		{
			IllogicalCallContext illogicalCallContext = new IllogicalCallContext();
			illogicalCallContext.HostContext = this.HostContext;
			if (this.HasUserData)
			{
				IDictionaryEnumerator enumerator = this.m_Datastore.GetEnumerator();
				while (enumerator.MoveNext())
				{
					illogicalCallContext.Datastore[(string)enumerator.Key] = enumerator.Value;
				}
			}
			return illogicalCallContext;
		}

		// Token: 0x040011B4 RID: 4532
		private Hashtable m_Datastore;

		// Token: 0x040011B5 RID: 4533
		private object m_HostContext;

		// Token: 0x02000472 RID: 1138
		internal struct Reader
		{
			// Token: 0x060024D9 RID: 9433 RVA: 0x00096A06 File Offset: 0x00094C06
			public Reader(IllogicalCallContext ctx)
			{
				this.m_ctx = ctx;
			}

			// Token: 0x040011B6 RID: 4534
			private IllogicalCallContext m_ctx;
		}
	}
}
