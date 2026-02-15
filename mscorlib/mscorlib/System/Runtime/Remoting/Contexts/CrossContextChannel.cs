using System;
using System.Runtime.Remoting.Messaging;
using System.Threading;

namespace System.Runtime.Remoting.Contexts
{
	// Token: 0x02000442 RID: 1090
	internal class CrossContextChannel : IMessageSink
	{
		// Token: 0x06002439 RID: 9273 RVA: 0x00094DFC File Offset: 0x00092FFC
		public IMessage SyncProcessMessage(IMessage msg)
		{
			ServerIdentity serverIdentity = (ServerIdentity)RemotingServices.GetMessageTargetIdentity(msg);
			Context context = null;
			if (Thread.CurrentContext != serverIdentity.Context)
			{
				context = Context.SwitchToContext(serverIdentity.Context);
			}
			IMessage message;
			try
			{
				Context.NotifyGlobalDynamicSinks(true, msg, false, false);
				Thread.CurrentContext.NotifyDynamicSinks(true, msg, false, false);
				message = serverIdentity.Context.GetServerContextSinkChain().SyncProcessMessage(msg);
				Context.NotifyGlobalDynamicSinks(false, msg, false, false);
				Thread.CurrentContext.NotifyDynamicSinks(false, msg, false, false);
			}
			catch (Exception ex)
			{
				message = new ReturnMessage(ex, (IMethodCallMessage)msg);
			}
			finally
			{
				if (context != null)
				{
					Context.SwitchToContext(context);
				}
			}
			return message;
		}

		// Token: 0x0600243A RID: 9274 RVA: 0x00094EAC File Offset: 0x000930AC
		public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
		{
			ServerIdentity serverIdentity = (ServerIdentity)RemotingServices.GetMessageTargetIdentity(msg);
			Context context = null;
			if (Thread.CurrentContext != serverIdentity.Context)
			{
				context = Context.SwitchToContext(serverIdentity.Context);
			}
			IMessageCtrl messageCtrl2;
			try
			{
				Context.NotifyGlobalDynamicSinks(true, msg, false, true);
				Thread.CurrentContext.NotifyDynamicSinks(true, msg, false, false);
				if (replySink != null)
				{
					replySink = new CrossContextChannel.ContextRestoreSink(replySink, context, msg);
				}
				IMessageCtrl messageCtrl = serverIdentity.AsyncObjectProcessMessage(msg, replySink);
				if (replySink == null)
				{
					Context.NotifyGlobalDynamicSinks(false, msg, false, false);
					Thread.CurrentContext.NotifyDynamicSinks(false, msg, false, false);
				}
				messageCtrl2 = messageCtrl;
			}
			catch (Exception ex)
			{
				if (replySink != null)
				{
					replySink.SyncProcessMessage(new ReturnMessage(ex, (IMethodCallMessage)msg));
				}
				messageCtrl2 = null;
			}
			finally
			{
				if (context != null)
				{
					Context.SwitchToContext(context);
				}
			}
			return messageCtrl2;
		}

		// Token: 0x02000443 RID: 1091
		private class ContextRestoreSink : IMessageSink
		{
			// Token: 0x0600243C RID: 9276 RVA: 0x00094F70 File Offset: 0x00093170
			public ContextRestoreSink(IMessageSink next, Context context, IMessage call)
			{
				this._next = next;
				this._context = context;
				this._call = call;
			}

			// Token: 0x0600243D RID: 9277 RVA: 0x00094F90 File Offset: 0x00093190
			public IMessage SyncProcessMessage(IMessage msg)
			{
				IMessage message;
				try
				{
					Context.NotifyGlobalDynamicSinks(false, msg, false, false);
					Thread.CurrentContext.NotifyDynamicSinks(false, msg, false, false);
					message = this._next.SyncProcessMessage(msg);
				}
				catch (Exception ex)
				{
					message = new ReturnMessage(ex, (IMethodCallMessage)this._call);
				}
				finally
				{
					if (this._context != null)
					{
						Context.SwitchToContext(this._context);
					}
				}
				return message;
			}

			// Token: 0x0600243E RID: 9278 RVA: 0x000339FF File Offset: 0x00031BFF
			public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
			{
				throw new NotSupportedException();
			}

			// Token: 0x0400117A RID: 4474
			private IMessageSink _next;

			// Token: 0x0400117B RID: 4475
			private Context _context;

			// Token: 0x0400117C RID: 4476
			private IMessage _call;
		}
	}
}
