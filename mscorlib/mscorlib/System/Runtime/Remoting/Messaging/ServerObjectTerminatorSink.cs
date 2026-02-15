using System;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x0200049E RID: 1182
	internal class ServerObjectTerminatorSink : IMessageSink
	{
		// Token: 0x06002612 RID: 9746 RVA: 0x0009A3BF File Offset: 0x000985BF
		public ServerObjectTerminatorSink(IMessageSink nextSink)
		{
			this._nextSink = nextSink;
		}

		// Token: 0x06002613 RID: 9747 RVA: 0x0009A3D0 File Offset: 0x000985D0
		public IMessage SyncProcessMessage(IMessage msg)
		{
			ServerIdentity serverIdentity = (ServerIdentity)RemotingServices.GetMessageTargetIdentity(msg);
			serverIdentity.NotifyServerDynamicSinks(true, msg, false, false);
			IMessage message = this._nextSink.SyncProcessMessage(msg);
			serverIdentity.NotifyServerDynamicSinks(false, msg, false, false);
			return message;
		}

		// Token: 0x06002614 RID: 9748 RVA: 0x0009A40C File Offset: 0x0009860C
		public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
		{
			ServerIdentity serverIdentity = (ServerIdentity)RemotingServices.GetMessageTargetIdentity(msg);
			if (serverIdentity.HasServerDynamicSinks)
			{
				serverIdentity.NotifyServerDynamicSinks(true, msg, false, true);
				if (replySink != null)
				{
					replySink = new ServerObjectReplySink(serverIdentity, replySink);
				}
			}
			IMessageCtrl messageCtrl = this._nextSink.AsyncProcessMessage(msg, replySink);
			if (replySink == null)
			{
				serverIdentity.NotifyServerDynamicSinks(false, msg, true, true);
			}
			return messageCtrl;
		}

		// Token: 0x0400123C RID: 4668
		private IMessageSink _nextSink;
	}
}
