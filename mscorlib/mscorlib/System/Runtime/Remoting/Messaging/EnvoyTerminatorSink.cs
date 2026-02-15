using System;
using System.Threading;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000485 RID: 1157
	[Serializable]
	internal class EnvoyTerminatorSink : IMessageSink
	{
		// Token: 0x0600254C RID: 9548 RVA: 0x0009825B File Offset: 0x0009645B
		public IMessage SyncProcessMessage(IMessage msg)
		{
			return Thread.CurrentContext.GetClientContextSinkChain().SyncProcessMessage(msg);
		}

		// Token: 0x0600254D RID: 9549 RVA: 0x0009826D File Offset: 0x0009646D
		public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
		{
			return Thread.CurrentContext.GetClientContextSinkChain().AsyncProcessMessage(msg, replySink);
		}

		// Token: 0x040011F3 RID: 4595
		public static EnvoyTerminatorSink Instance = new EnvoyTerminatorSink();
	}
}
