using System;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting.Activation
{
	// Token: 0x02000464 RID: 1124
	[Serializable]
	internal class ContextLevelActivator : IActivator
	{
		// Token: 0x060024A6 RID: 9382 RVA: 0x0009657B File Offset: 0x0009477B
		public ContextLevelActivator(IActivator next)
		{
			this.m_NextActivator = next;
		}

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x060024A7 RID: 9383 RVA: 0x0009658A File Offset: 0x0009478A
		public IActivator NextActivator
		{
			get
			{
				return this.m_NextActivator;
			}
		}

		// Token: 0x060024A8 RID: 9384 RVA: 0x00096594 File Offset: 0x00094794
		public IConstructionReturnMessage Activate(IConstructionCallMessage ctorCall)
		{
			ServerIdentity serverIdentity = RemotingServices.CreateContextBoundObjectIdentity(ctorCall.ActivationType);
			RemotingServices.SetMessageTargetIdentity(ctorCall, serverIdentity);
			ConstructionCall constructionCall = ctorCall as ConstructionCall;
			if (constructionCall == null || !constructionCall.IsContextOk)
			{
				serverIdentity.Context = Context.CreateNewContext(ctorCall);
				Context context = Context.SwitchToContext(serverIdentity.Context);
				try
				{
					return this.m_NextActivator.Activate(ctorCall);
				}
				finally
				{
					Context.SwitchToContext(context);
				}
			}
			return this.m_NextActivator.Activate(ctorCall);
		}

		// Token: 0x0400119F RID: 4511
		private IActivator m_NextActivator;
	}
}
