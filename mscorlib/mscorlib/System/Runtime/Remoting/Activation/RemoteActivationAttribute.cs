using System;
using System.Collections;
using System.Runtime.Remoting.Contexts;

namespace System.Runtime.Remoting.Activation
{
	// Token: 0x02000468 RID: 1128
	internal class RemoteActivationAttribute : Attribute, IContextAttribute
	{
		// Token: 0x060024B1 RID: 9393 RVA: 0x00096614 File Offset: 0x00094814
		public RemoteActivationAttribute(IList contextProperties)
		{
			this._contextProperties = contextProperties;
		}

		// Token: 0x060024B2 RID: 9394 RVA: 0x00033991 File Offset: 0x00031B91
		public bool IsContextOK(Context ctx, IConstructionCallMessage ctor)
		{
			return false;
		}

		// Token: 0x060024B3 RID: 9395 RVA: 0x00096624 File Offset: 0x00094824
		public void GetPropertiesForNewContext(IConstructionCallMessage ctor)
		{
			if (this._contextProperties != null)
			{
				foreach (object obj in this._contextProperties)
				{
					ctor.ContextProperties.Add(obj);
				}
			}
		}

		// Token: 0x040011A0 RID: 4512
		private IList _contextProperties;
	}
}
