using System;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting.Activation
{
	// Token: 0x02000469 RID: 1129
	internal class RemoteActivator : MarshalByRefObject, IActivator
	{
		// Token: 0x060024B4 RID: 9396 RVA: 0x00096688 File Offset: 0x00094888
		public IConstructionReturnMessage Activate(IConstructionCallMessage msg)
		{
			if (!RemotingConfiguration.IsActivationAllowed(msg.ActivationType))
			{
				throw new RemotingException("The type " + msg.ActivationTypeName + " is not allowed to be client activated");
			}
			object[] array = null;
			if (msg.ActivationType.IsContextful)
			{
				array = new object[]
				{
					new RemoteActivationAttribute(msg.ContextProperties)
				};
			}
			return new ConstructionResponse(RemotingServices.Marshal((MarshalByRefObject)Activator.CreateInstance(msg.ActivationType, msg.Args, array)), null, msg);
		}

		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x060024B5 RID: 9397 RVA: 0x000339FF File Offset: 0x00031BFF
		public IActivator NextActivator
		{
			get
			{
				throw new NotSupportedException();
			}
		}
	}
}
