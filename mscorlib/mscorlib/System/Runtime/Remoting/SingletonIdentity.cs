using System;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Lifetime;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting
{
	// Token: 0x02000425 RID: 1061
	internal class SingletonIdentity : ServerIdentity
	{
		// Token: 0x06002380 RID: 9088 RVA: 0x0009285B File Offset: 0x00090A5B
		public SingletonIdentity(string objectUri, Context context, Type objectType)
			: base(objectUri, context, objectType)
		{
		}

		// Token: 0x06002381 RID: 9089 RVA: 0x00092868 File Offset: 0x00090A68
		public MarshalByRefObject GetServerObject()
		{
			if (this._serverObject != null)
			{
				return this._serverObject;
			}
			lock (this)
			{
				if (this._serverObject == null)
				{
					MarshalByRefObject marshalByRefObject = (MarshalByRefObject)Activator.CreateInstance(this._objectType, true);
					base.AttachServerObject(marshalByRefObject, Context.DefaultContext);
					base.StartTrackingLifetime((ILease)marshalByRefObject.InitializeLifetimeService());
				}
			}
			return this._serverObject;
		}

		// Token: 0x06002382 RID: 9090 RVA: 0x000928EC File Offset: 0x00090AEC
		public override IMessage SyncObjectProcessMessage(IMessage msg)
		{
			MarshalByRefObject serverObject = this.GetServerObject();
			if (this._serverSink == null)
			{
				this._serverSink = this._context.CreateServerObjectSinkChain(serverObject, false);
			}
			return this._serverSink.SyncProcessMessage(msg);
		}

		// Token: 0x06002383 RID: 9091 RVA: 0x00092928 File Offset: 0x00090B28
		public override IMessageCtrl AsyncObjectProcessMessage(IMessage msg, IMessageSink replySink)
		{
			MarshalByRefObject serverObject = this.GetServerObject();
			if (this._serverSink == null)
			{
				this._serverSink = this._context.CreateServerObjectSinkChain(serverObject, false);
			}
			return this._serverSink.AsyncProcessMessage(msg, replySink);
		}
	}
}
