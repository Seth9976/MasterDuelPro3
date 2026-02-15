using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020004E3 RID: 1251
	public interface IVisualElementScheduler
	{
		// Token: 0x06002313 RID: 8979
		IVisualElementScheduledItem Execute(Action<TimerState> timerUpdateEvent);

		// Token: 0x06002314 RID: 8980
		IVisualElementScheduledItem Execute(Action updateEvent);
	}
}
