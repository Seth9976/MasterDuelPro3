using System;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting
{
	// Token: 0x02000416 RID: 1046
	internal abstract class Identity
	{
		// Token: 0x060022CD RID: 8909 RVA: 0x0008F5A0 File Offset: 0x0008D7A0
		public Identity(string objectUri)
		{
			this._objectUri = objectUri;
		}

		// Token: 0x060022CE RID: 8910
		public abstract ObjRef CreateObjRef(Type requestedType);

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x060022CF RID: 8911 RVA: 0x0008F5AF File Offset: 0x0008D7AF
		// (set) Token: 0x060022D0 RID: 8912 RVA: 0x0008F5B7 File Offset: 0x0008D7B7
		public IMessageSink ChannelSink
		{
			get
			{
				return this._channelSink;
			}
			set
			{
				this._channelSink = value;
			}
		}

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x060022D1 RID: 8913 RVA: 0x0008F5C0 File Offset: 0x0008D7C0
		public IMessageSink EnvoySink
		{
			get
			{
				return this._envoySink;
			}
		}

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x060022D2 RID: 8914 RVA: 0x0008F5C8 File Offset: 0x0008D7C8
		// (set) Token: 0x060022D3 RID: 8915 RVA: 0x0008F5D0 File Offset: 0x0008D7D0
		public string ObjectUri
		{
			get
			{
				return this._objectUri;
			}
			set
			{
				this._objectUri = value;
			}
		}

		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x060022D4 RID: 8916 RVA: 0x0008F5D9 File Offset: 0x0008D7D9
		public bool IsConnected
		{
			get
			{
				return this._objectUri != null;
			}
		}

		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x060022D5 RID: 8917 RVA: 0x0008F5E4 File Offset: 0x0008D7E4
		// (set) Token: 0x060022D6 RID: 8918 RVA: 0x0008F5EC File Offset: 0x0008D7EC
		public bool Disposed
		{
			get
			{
				return this._disposed;
			}
			set
			{
				this._disposed = value;
			}
		}

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x060022D7 RID: 8919 RVA: 0x0008F5F5 File Offset: 0x0008D7F5
		public DynamicPropertyCollection ClientDynamicProperties
		{
			get
			{
				if (this._clientDynamicProperties == null)
				{
					this._clientDynamicProperties = new DynamicPropertyCollection();
				}
				return this._clientDynamicProperties;
			}
		}

		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x060022D8 RID: 8920 RVA: 0x0008F610 File Offset: 0x0008D810
		public DynamicPropertyCollection ServerDynamicProperties
		{
			get
			{
				if (this._serverDynamicProperties == null)
				{
					this._serverDynamicProperties = new DynamicPropertyCollection();
				}
				return this._serverDynamicProperties;
			}
		}

		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x060022D9 RID: 8921 RVA: 0x0008F62B File Offset: 0x0008D82B
		public bool HasServerDynamicSinks
		{
			get
			{
				return this._serverDynamicProperties != null && this._serverDynamicProperties.HasProperties;
			}
		}

		// Token: 0x060022DA RID: 8922 RVA: 0x0008F642 File Offset: 0x0008D842
		public void NotifyClientDynamicSinks(bool start, IMessage req_msg, bool client_site, bool async)
		{
			if (this._clientDynamicProperties != null && this._clientDynamicProperties.HasProperties)
			{
				this._clientDynamicProperties.NotifyMessage(start, req_msg, client_site, async);
			}
		}

		// Token: 0x060022DB RID: 8923 RVA: 0x0008F669 File Offset: 0x0008D869
		public void NotifyServerDynamicSinks(bool start, IMessage req_msg, bool client_site, bool async)
		{
			if (this._serverDynamicProperties != null && this._serverDynamicProperties.HasProperties)
			{
				this._serverDynamicProperties.NotifyMessage(start, req_msg, client_site, async);
			}
		}

		// Token: 0x040010E5 RID: 4325
		protected string _objectUri;

		// Token: 0x040010E6 RID: 4326
		protected IMessageSink _channelSink;

		// Token: 0x040010E7 RID: 4327
		protected IMessageSink _envoySink;

		// Token: 0x040010E8 RID: 4328
		private DynamicPropertyCollection _clientDynamicProperties;

		// Token: 0x040010E9 RID: 4329
		private DynamicPropertyCollection _serverDynamicProperties;

		// Token: 0x040010EA RID: 4330
		protected ObjRef _objRef;

		// Token: 0x040010EB RID: 4331
		private bool _disposed;
	}
}
