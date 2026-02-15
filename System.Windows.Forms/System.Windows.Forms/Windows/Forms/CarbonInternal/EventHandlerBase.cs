using System;

namespace System.Windows.Forms.CarbonInternal
{
	// Token: 0x020003A3 RID: 931
	internal abstract class EventHandlerBase
	{
		// Token: 0x06001DFB RID: 7675 RVA: 0x00094C9B File Offset: 0x00092E9B
		public EventHandlerBase(XplatUICarbon driver)
		{
			this.Driver = driver;
		}

		// Token: 0x04001D38 RID: 7480
		internal XplatUICarbon Driver;
	}
}
