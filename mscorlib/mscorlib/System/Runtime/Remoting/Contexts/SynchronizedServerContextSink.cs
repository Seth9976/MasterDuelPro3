using System;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting.Contexts
{
	// Token: 0x02000450 RID: 1104
	internal class SynchronizedServerContextSink : IMessageSink
	{
		// Token: 0x0600245D RID: 9309 RVA: 0x00095370 File Offset: 0x00093570
		public SynchronizedServerContextSink(IMessageSink next, SynchronizationAttribute att)
		{
			this._att = att;
			this._next = next;
		}

		// Token: 0x0600245E RID: 9310 RVA: 0x00095386 File Offset: 0x00093586
		public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
		{
			this._att.AcquireLock();
			replySink = new SynchronizedContextReplySink(replySink, this._att, false);
			return this._next.AsyncProcessMessage(msg, replySink);
		}

		// Token: 0x0600245F RID: 9311 RVA: 0x000953B0 File Offset: 0x000935B0
		public IMessage SyncProcessMessage(IMessage msg)
		{
			this._att.AcquireLock();
			IMessage message;
			try
			{
				message = this._next.SyncProcessMessage(msg);
			}
			finally
			{
				this._att.ReleaseLock();
			}
			return message;
		}

		// Token: 0x04001184 RID: 4484
		private IMessageSink _next;

		// Token: 0x04001185 RID: 4485
		private SynchronizationAttribute _att;
	}
}
