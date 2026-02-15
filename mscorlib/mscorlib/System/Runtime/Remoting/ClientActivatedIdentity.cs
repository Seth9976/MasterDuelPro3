using System;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting
{
	// Token: 0x02000424 RID: 1060
	internal class ClientActivatedIdentity : ServerIdentity
	{
		// Token: 0x0600237A RID: 9082 RVA: 0x00092792 File Offset: 0x00090992
		public ClientActivatedIdentity(string objectUri, Type objectType)
			: base(objectUri, null, objectType)
		{
		}

		// Token: 0x0600237B RID: 9083 RVA: 0x0009279D File Offset: 0x0009099D
		public MarshalByRefObject GetServerObject()
		{
			return this._serverObject;
		}

		// Token: 0x0600237C RID: 9084 RVA: 0x000927A5 File Offset: 0x000909A5
		public void SetClientProxy(MarshalByRefObject obj)
		{
			this._targetThis = obj;
		}

		// Token: 0x0600237D RID: 9085 RVA: 0x000927AE File Offset: 0x000909AE
		public override void OnLifetimeExpired()
		{
			base.OnLifetimeExpired();
			RemotingServices.DisposeIdentity(this);
		}

		// Token: 0x0600237E RID: 9086 RVA: 0x000927BC File Offset: 0x000909BC
		public override IMessage SyncObjectProcessMessage(IMessage msg)
		{
			if (this._serverSink == null)
			{
				bool flag = this._targetThis != null;
				this._serverSink = this._context.CreateServerObjectSinkChain(flag ? this._targetThis : this._serverObject, flag);
			}
			return this._serverSink.SyncProcessMessage(msg);
		}

		// Token: 0x0600237F RID: 9087 RVA: 0x0009280C File Offset: 0x00090A0C
		public override IMessageCtrl AsyncObjectProcessMessage(IMessage msg, IMessageSink replySink)
		{
			if (this._serverSink == null)
			{
				bool flag = this._targetThis != null;
				this._serverSink = this._context.CreateServerObjectSinkChain(flag ? this._targetThis : this._serverObject, flag);
			}
			return this._serverSink.AsyncProcessMessage(msg, replySink);
		}

		// Token: 0x04001128 RID: 4392
		private MarshalByRefObject _targetThis;
	}
}
