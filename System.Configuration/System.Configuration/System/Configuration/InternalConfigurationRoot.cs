using System;
using System.Configuration.Internal;

namespace System.Configuration
{
	// Token: 0x02000033 RID: 51
	internal class InternalConfigurationRoot : IInternalConfigRoot
	{
		// Token: 0x06000161 RID: 353 RVA: 0x00006490 File Offset: 0x00004690
		public void Init(IInternalConfigHost host, bool isDesignTime)
		{
			this.host = host;
			this.isDesignTime = isDesignTime;
		}

		// Token: 0x040000AE RID: 174
		private IInternalConfigHost host;

		// Token: 0x040000AF RID: 175
		private bool isDesignTime;
	}
}
