using System;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x02000458 RID: 1112
	internal class AsyncRequest
	{
		// Token: 0x06002489 RID: 9353 RVA: 0x000960B7 File Offset: 0x000942B7
		public AsyncRequest(IMessage msgRequest, IMessageSink replySink)
		{
			this.ReplySink = replySink;
			this.MsgRequest = msgRequest;
		}

		// Token: 0x04001197 RID: 4503
		internal IMessageSink ReplySink;

		// Token: 0x04001198 RID: 4504
		internal IMessage MsgRequest;
	}
}
