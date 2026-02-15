using System;
using System.Runtime.Remoting.Contexts;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000481 RID: 1153
	internal class ClientContextReplySink : IMessageSink
	{
		// Token: 0x06002530 RID: 9520 RVA: 0x00097E4B File Offset: 0x0009604B
		public ClientContextReplySink(Context ctx, IMessageSink replySink)
		{
			this._replySink = replySink;
			this._context = ctx;
		}

		// Token: 0x06002531 RID: 9521 RVA: 0x00097E61 File Offset: 0x00096061
		public IMessage SyncProcessMessage(IMessage msg)
		{
			Context.NotifyGlobalDynamicSinks(false, msg, true, true);
			this._context.NotifyDynamicSinks(false, msg, true, true);
			return this._replySink.SyncProcessMessage(msg);
		}

		// Token: 0x06002532 RID: 9522 RVA: 0x000339FF File Offset: 0x00031BFF
		public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
		{
			throw new NotSupportedException();
		}

		// Token: 0x040011E9 RID: 4585
		private IMessageSink _replySink;

		// Token: 0x040011EA RID: 4586
		private Context _context;
	}
}
