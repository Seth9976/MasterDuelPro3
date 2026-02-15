using System;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting
{
	// Token: 0x02000427 RID: 1063
	internal class DisposerReplySink : IMessageSink
	{
		// Token: 0x06002387 RID: 9095 RVA: 0x00092A01 File Offset: 0x00090C01
		public DisposerReplySink(IMessageSink next, IDisposable disposable)
		{
			this._next = next;
			this._disposable = disposable;
		}

		// Token: 0x06002388 RID: 9096 RVA: 0x00092A17 File Offset: 0x00090C17
		public IMessage SyncProcessMessage(IMessage msg)
		{
			this._disposable.Dispose();
			return this._next.SyncProcessMessage(msg);
		}

		// Token: 0x06002389 RID: 9097 RVA: 0x000339FF File Offset: 0x00031BFF
		public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
		{
			throw new NotSupportedException();
		}

		// Token: 0x04001129 RID: 4393
		private IMessageSink _next;

		// Token: 0x0400112A RID: 4394
		private IDisposable _disposable;
	}
}
