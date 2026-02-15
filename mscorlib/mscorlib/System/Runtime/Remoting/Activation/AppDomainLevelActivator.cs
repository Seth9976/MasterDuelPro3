using System;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting.Activation
{
	// Token: 0x02000462 RID: 1122
	internal class AppDomainLevelActivator : IActivator
	{
		// Token: 0x060024A0 RID: 9376 RVA: 0x000964B0 File Offset: 0x000946B0
		public AppDomainLevelActivator(string activationUrl, IActivator next)
		{
			this._activationUrl = activationUrl;
			this._next = next;
		}

		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x060024A1 RID: 9377 RVA: 0x000964C6 File Offset: 0x000946C6
		public IActivator NextActivator
		{
			get
			{
				return this._next;
			}
		}

		// Token: 0x060024A2 RID: 9378 RVA: 0x000964D0 File Offset: 0x000946D0
		public IConstructionReturnMessage Activate(IConstructionCallMessage ctorCall)
		{
			IActivator activator = (IActivator)RemotingServices.Connect(typeof(IActivator), this._activationUrl);
			ctorCall.Activator = ctorCall.Activator.NextActivator;
			IConstructionReturnMessage constructionReturnMessage;
			try
			{
				constructionReturnMessage = activator.Activate(ctorCall);
			}
			catch (Exception ex)
			{
				return new ConstructionResponse(ex, ctorCall);
			}
			ObjRef objRef = (ObjRef)constructionReturnMessage.ReturnValue;
			if (RemotingServices.GetIdentityForUri(objRef.URI) != null)
			{
				throw new RemotingException("Inconsistent state during activation; there may be two proxies for the same object");
			}
			object obj;
			Identity orCreateClientIdentity = RemotingServices.GetOrCreateClientIdentity(objRef, null, out obj);
			RemotingServices.SetMessageTargetIdentity(ctorCall, orCreateClientIdentity);
			return constructionReturnMessage;
		}

		// Token: 0x0400119D RID: 4509
		private string _activationUrl;

		// Token: 0x0400119E RID: 4510
		private IActivator _next;
	}
}
