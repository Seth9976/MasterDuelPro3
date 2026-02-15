using System;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting.Contexts
{
	// Token: 0x0200044F RID: 1103
	internal class SynchronizedClientContextSink : IMessageSink
	{
		// Token: 0x0600245A RID: 9306 RVA: 0x000952C2 File Offset: 0x000934C2
		public SynchronizedClientContextSink(IMessageSink next, SynchronizationAttribute att)
		{
			this._att = att;
			this._next = next;
		}

		// Token: 0x0600245B RID: 9307 RVA: 0x000952D8 File Offset: 0x000934D8
		public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
		{
			if (this._att.IsReEntrant)
			{
				this._att.ReleaseLock();
				replySink = new SynchronizedContextReplySink(replySink, this._att, true);
			}
			return this._next.AsyncProcessMessage(msg, replySink);
		}

		// Token: 0x0600245C RID: 9308 RVA: 0x00095310 File Offset: 0x00093510
		public IMessage SyncProcessMessage(IMessage msg)
		{
			if (this._att.IsReEntrant)
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
				if (this._att.IsReEntrant)
				{
					this._att.AcquireLock();
				}
			}
			return message;
		}

		// Token: 0x04001182 RID: 4482
		private IMessageSink _next;

		// Token: 0x04001183 RID: 4483
		private SynchronizationAttribute _att;
	}
}
