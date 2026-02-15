using System;
using System.Diagnostics.Tracing;

namespace System.Collections.Concurrent
{
	// Token: 0x0200072C RID: 1836
	[EventSource(Name = "System.Collections.Concurrent.ConcurrentCollectionsEventSource", Guid = "35167F8E-49B2-4b96-AB86-435B59336B5E")]
	internal sealed class CDSCollectionETWBCLProvider : EventSource
	{
		// Token: 0x06003A2C RID: 14892 RVA: 0x000E28BB File Offset: 0x000E0ABB
		private CDSCollectionETWBCLProvider()
		{
		}

		// Token: 0x06003A2D RID: 14893 RVA: 0x000E28C3 File Offset: 0x000E0AC3
		[Event(3, Level = EventLevel.Warning)]
		public void ConcurrentDictionary_AcquiringAllLocks(int numOfBuckets)
		{
			if (base.IsEnabled(EventLevel.Warning, EventKeywords.All))
			{
				base.WriteEvent(3, numOfBuckets);
			}
		}

		// Token: 0x04001ED0 RID: 7888
		public static CDSCollectionETWBCLProvider Log = new CDSCollectionETWBCLProvider();
	}
}
