using System;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Contexts;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000480 RID: 1152
	internal class ClientContextTerminatorSink : IMessageSink
	{
		// Token: 0x0600252D RID: 9517 RVA: 0x00097D48 File Offset: 0x00095F48
		public ClientContextTerminatorSink(Context ctx)
		{
			this._context = ctx;
		}

		// Token: 0x0600252E RID: 9518 RVA: 0x00097D58 File Offset: 0x00095F58
		public IMessage SyncProcessMessage(IMessage msg)
		{
			Context.NotifyGlobalDynamicSinks(true, msg, true, false);
			this._context.NotifyDynamicSinks(true, msg, true, false);
			IMessage message;
			if (msg is IConstructionCallMessage)
			{
				message = ActivationServices.RemoteActivate((IConstructionCallMessage)msg);
			}
			else
			{
				message = RemotingServices.GetMessageTargetIdentity(msg).ChannelSink.SyncProcessMessage(msg);
			}
			Context.NotifyGlobalDynamicSinks(false, msg, true, false);
			this._context.NotifyDynamicSinks(false, msg, true, false);
			return message;
		}

		// Token: 0x0600252F RID: 9519 RVA: 0x00097DC0 File Offset: 0x00095FC0
		public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
		{
			if (this._context.HasDynamicSinks || Context.HasGlobalDynamicSinks)
			{
				Context.NotifyGlobalDynamicSinks(true, msg, true, true);
				this._context.NotifyDynamicSinks(true, msg, true, true);
				if (replySink != null)
				{
					replySink = new ClientContextReplySink(this._context, replySink);
				}
			}
			IMessageCtrl messageCtrl = RemotingServices.GetMessageTargetIdentity(msg).ChannelSink.AsyncProcessMessage(msg, replySink);
			if (replySink == null && (this._context.HasDynamicSinks || Context.HasGlobalDynamicSinks))
			{
				Context.NotifyGlobalDynamicSinks(false, msg, true, true);
				this._context.NotifyDynamicSinks(false, msg, true, true);
			}
			return messageCtrl;
		}

		// Token: 0x040011E8 RID: 4584
		private Context _context;
	}
}
