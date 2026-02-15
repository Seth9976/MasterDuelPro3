using System;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Lifetime;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using System.Runtime.Remoting.Services;

namespace System.Runtime.Remoting
{
	// Token: 0x02000423 RID: 1059
	internal abstract class ServerIdentity : Identity
	{
		// Token: 0x0600236E RID: 9070 RVA: 0x000925F1 File Offset: 0x000907F1
		public ServerIdentity(string objectUri, Context context, Type objectType)
			: base(objectUri)
		{
			this._objectType = objectType;
			this._context = context;
		}

		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x0600236F RID: 9071 RVA: 0x00092608 File Offset: 0x00090808
		public Type ObjectType
		{
			get
			{
				return this._objectType;
			}
		}

		// Token: 0x06002370 RID: 9072 RVA: 0x00092610 File Offset: 0x00090810
		public void StartTrackingLifetime(ILease lease)
		{
			if (lease != null && lease.CurrentState == LeaseState.Null)
			{
				lease = null;
			}
			if (lease != null)
			{
				if (!(lease is Lease))
				{
					lease = new Lease();
				}
				this._lease = (Lease)lease;
				LifetimeServices.TrackLifetime(this);
			}
		}

		// Token: 0x06002371 RID: 9073 RVA: 0x00092644 File Offset: 0x00090844
		public virtual void OnLifetimeExpired()
		{
			this.DisposeServerObject();
		}

		// Token: 0x06002372 RID: 9074 RVA: 0x0009264C File Offset: 0x0009084C
		public override ObjRef CreateObjRef(Type requestedType)
		{
			if (this._objRef != null)
			{
				this._objRef.UpdateChannelInfo();
				return this._objRef;
			}
			if (requestedType == null)
			{
				requestedType = this._objectType;
			}
			this._objRef = new ObjRef();
			this._objRef.TypeInfo = new TypeInfo(requestedType);
			this._objRef.URI = this._objectUri;
			if (this._envoySink != null && !(this._envoySink is EnvoyTerminatorSink))
			{
				this._objRef.EnvoyInfo = new EnvoyInfo(this._envoySink);
			}
			return this._objRef;
		}

		// Token: 0x06002373 RID: 9075 RVA: 0x000926E4 File Offset: 0x000908E4
		public void AttachServerObject(MarshalByRefObject serverObject, Context context)
		{
			this.DisposeServerObject();
			this._context = context;
			this._serverObject = serverObject;
			if (RemotingServices.IsTransparentProxy(serverObject))
			{
				RealProxy realProxy = RemotingServices.GetRealProxy(serverObject);
				if (realProxy.ObjectIdentity == null)
				{
					realProxy.ObjectIdentity = this;
					return;
				}
			}
			else
			{
				if (this._objectType.IsContextful)
				{
					this._envoySink = context.CreateEnvoySink(serverObject);
				}
				this._serverObject.ObjectIdentity = this;
			}
		}

		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x06002374 RID: 9076 RVA: 0x0009274A File Offset: 0x0009094A
		public Lease Lease
		{
			get
			{
				return this._lease;
			}
		}

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x06002375 RID: 9077 RVA: 0x00092752 File Offset: 0x00090952
		// (set) Token: 0x06002376 RID: 9078 RVA: 0x0009275A File Offset: 0x0009095A
		public Context Context
		{
			get
			{
				return this._context;
			}
			set
			{
				this._context = value;
			}
		}

		// Token: 0x06002377 RID: 9079
		public abstract IMessage SyncObjectProcessMessage(IMessage msg);

		// Token: 0x06002378 RID: 9080
		public abstract IMessageCtrl AsyncObjectProcessMessage(IMessage msg, IMessageSink replySink);

		// Token: 0x06002379 RID: 9081 RVA: 0x00092763 File Offset: 0x00090963
		protected void DisposeServerObject()
		{
			if (this._serverObject != null)
			{
				object serverObject = this._serverObject;
				this._serverObject.ObjectIdentity = null;
				this._serverObject = null;
				this._serverSink = null;
				TrackingServices.NotifyDisconnectedObject(serverObject);
			}
		}

		// Token: 0x04001123 RID: 4387
		protected Type _objectType;

		// Token: 0x04001124 RID: 4388
		protected MarshalByRefObject _serverObject;

		// Token: 0x04001125 RID: 4389
		protected IMessageSink _serverSink;

		// Token: 0x04001126 RID: 4390
		protected Context _context;

		// Token: 0x04001127 RID: 4391
		protected Lease _lease;
	}
}
