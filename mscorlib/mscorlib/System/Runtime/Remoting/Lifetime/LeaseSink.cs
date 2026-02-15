using System;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting.Lifetime
{
	// Token: 0x0200043A RID: 1082
	internal class LeaseSink : IMessageSink
	{
		// Token: 0x060023F5 RID: 9205 RVA: 0x000942B7 File Offset: 0x000924B7
		public LeaseSink(IMessageSink nextSink)
		{
			this._nextSink = nextSink;
		}

		// Token: 0x060023F6 RID: 9206 RVA: 0x000942C6 File Offset: 0x000924C6
		public IMessage SyncProcessMessage(IMessage msg)
		{
			this.RenewLease(msg);
			return this._nextSink.SyncProcessMessage(msg);
		}

		// Token: 0x060023F7 RID: 9207 RVA: 0x000942DB File Offset: 0x000924DB
		public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
		{
			this.RenewLease(msg);
			return this._nextSink.AsyncProcessMessage(msg, replySink);
		}

		// Token: 0x060023F8 RID: 9208 RVA: 0x000942F4 File Offset: 0x000924F4
		private void RenewLease(IMessage msg)
		{
			ILease lease = ((ServerIdentity)RemotingServices.GetMessageTargetIdentity(msg)).Lease;
			if (lease != null && lease.CurrentLeaseTime < lease.RenewOnCallTime)
			{
				lease.Renew(lease.RenewOnCallTime);
			}
		}

		// Token: 0x0400115B RID: 4443
		private IMessageSink _nextSink;
	}
}
