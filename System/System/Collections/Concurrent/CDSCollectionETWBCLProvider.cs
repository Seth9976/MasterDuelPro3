using System;
using System.Diagnostics.Tracing;

namespace System.Collections.Concurrent
{
	// Token: 0x020002F5 RID: 757
	[EventSource(Name = "System.Collections.Concurrent.ConcurrentCollectionsEventSource", Guid = "35167F8E-49B2-4b96-AB86-435B59336B5E")]
	internal sealed class CDSCollectionETWBCLProvider : EventSource
	{
		// Token: 0x0600122F RID: 4655 RVA: 0x000524C6 File Offset: 0x000506C6
		private CDSCollectionETWBCLProvider()
		{
		}

		// Token: 0x06001230 RID: 4656 RVA: 0x000524CE File Offset: 0x000506CE
		[Event(4, Level = EventLevel.Verbose)]
		public void ConcurrentBag_TryTakeSteals()
		{
			if (base.IsEnabled(EventLevel.Verbose, EventKeywords.All))
			{
				base.WriteEvent(4);
			}
		}

		// Token: 0x06001231 RID: 4657 RVA: 0x000524E2 File Offset: 0x000506E2
		[Event(5, Level = EventLevel.Verbose)]
		public void ConcurrentBag_TryPeekSteals()
		{
			if (base.IsEnabled(EventLevel.Verbose, EventKeywords.All))
			{
				base.WriteEvent(5);
			}
		}

		// Token: 0x04000B3C RID: 2876
		public static CDSCollectionETWBCLProvider Log = new CDSCollectionETWBCLProvider();
	}
}
