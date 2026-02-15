using System;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting.Contexts
{
	// Token: 0x02000451 RID: 1105
	internal class SynchronizedContextReplySink : IMessageSink
	{
		// Token: 0x06002460 RID: 9312 RVA: 0x000953F4 File Offset: 0x000935F4
		public SynchronizedContextReplySink(IMessageSink next, SynchronizationAttribute att, bool newLock)
		{
			this._newLock = newLock;
			this._next = next;
			this._att = att;
		}

		// Token: 0x06002461 RID: 9313 RVA: 0x000339FF File Offset: 0x00031BFF
		public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002462 RID: 9314 RVA: 0x00095414 File Offset: 0x00093614
		public IMessage SyncProcessMessage(IMessage msg)
		{
			if (this._newLock)
			{
				this._att.AcquireLock();
			}
			else
			{
				this._att.ReleaseLock();
			}
			IMessage message;
			try
			{
				message = this._next.SyncProcessMessage(msg);
			}
			finally
			{
				if (this._newLock)
				{
					this._att.ReleaseLock();
				}
			}
			return message;
		}

		// Token: 0x04001186 RID: 4486
		private IMessageSink _next;

		// Token: 0x04001187 RID: 4487
		private bool _newLock;

		// Token: 0x04001188 RID: 4488
		private SynchronizationAttribute _att;
	}
}
