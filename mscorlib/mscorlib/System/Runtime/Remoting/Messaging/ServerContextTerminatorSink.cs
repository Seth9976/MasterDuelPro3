using System;
using System.Runtime.Remoting.Activation;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x0200049D RID: 1181
	internal class ServerContextTerminatorSink : IMessageSink
	{
		// Token: 0x0600260F RID: 9743 RVA: 0x0009A384 File Offset: 0x00098584
		public IMessage SyncProcessMessage(IMessage msg)
		{
			if (msg is IConstructionCallMessage)
			{
				return ActivationServices.CreateInstanceFromMessage((IConstructionCallMessage)msg);
			}
			return ((ServerIdentity)RemotingServices.GetMessageTargetIdentity(msg)).SyncObjectProcessMessage(msg);
		}

		// Token: 0x06002610 RID: 9744 RVA: 0x0009A3AB File Offset: 0x000985AB
		public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
		{
			return ((ServerIdentity)RemotingServices.GetMessageTargetIdentity(msg)).AsyncObjectProcessMessage(msg, replySink);
		}
	}
}
