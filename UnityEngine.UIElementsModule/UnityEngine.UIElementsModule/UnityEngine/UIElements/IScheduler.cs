using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020002B4 RID: 692
	internal interface IScheduler
	{
		// Token: 0x060012B8 RID: 4792
		void Unschedule(ScheduledItem item);

		// Token: 0x060012B9 RID: 4793
		void Schedule(ScheduledItem item);

		// Token: 0x060012BA RID: 4794
		void UpdateScheduledEvents();
	}
}
