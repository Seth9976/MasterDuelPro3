using System;
using System.Threading;

namespace System.Runtime.Remoting.Activation
{
	// Token: 0x02000463 RID: 1123
	[Serializable]
	internal class ConstructionLevelActivator : IActivator
	{
		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x060024A3 RID: 9379 RVA: 0x000082D2 File Offset: 0x000064D2
		public IActivator NextActivator
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060024A4 RID: 9380 RVA: 0x00096564 File Offset: 0x00094764
		public IConstructionReturnMessage Activate(IConstructionCallMessage msg)
		{
			return (IConstructionReturnMessage)Thread.CurrentContext.GetServerContextSinkChain().SyncProcessMessage(msg);
		}
	}
}
