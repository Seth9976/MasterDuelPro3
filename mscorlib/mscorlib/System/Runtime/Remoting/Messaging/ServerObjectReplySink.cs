using System;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x0200049F RID: 1183
	internal class ServerObjectReplySink : IMessageSink
	{
		// Token: 0x06002615 RID: 9749 RVA: 0x0009A45D File Offset: 0x0009865D
		public ServerObjectReplySink(ServerIdentity identity, IMessageSink replySink)
		{
			this._replySink = replySink;
			this._identity = identity;
		}

		// Token: 0x06002616 RID: 9750 RVA: 0x0009A473 File Offset: 0x00098673
		public IMessage SyncProcessMessage(IMessage msg)
		{
			this._identity.NotifyServerDynamicSinks(false, msg, true, true);
			return this._replySink.SyncProcessMessage(msg);
		}

		// Token: 0x06002617 RID: 9751 RVA: 0x000339FF File Offset: 0x00031BFF
		public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0400123D RID: 4669
		private IMessageSink _replySink;

		// Token: 0x0400123E RID: 4670
		private ServerIdentity _identity;
	}
}
