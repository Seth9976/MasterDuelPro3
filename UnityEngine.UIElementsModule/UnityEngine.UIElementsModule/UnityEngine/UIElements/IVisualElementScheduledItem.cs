using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020004E2 RID: 1250
	public interface IVisualElementScheduledItem
	{
		// Token: 0x17000943 RID: 2371
		// (get) Token: 0x0600230C RID: 8972
		bool isActive { get; }

		// Token: 0x0600230D RID: 8973
		void Resume();

		// Token: 0x0600230E RID: 8974
		void Pause();

		// Token: 0x0600230F RID: 8975
		void ExecuteLater(long delayMs);

		// Token: 0x06002310 RID: 8976
		IVisualElementScheduledItem StartingIn(long delayMs);

		// Token: 0x06002311 RID: 8977
		IVisualElementScheduledItem Every(long intervalMs);

		// Token: 0x06002312 RID: 8978
		IVisualElementScheduledItem Until(Func<bool> stopCondition);
	}
}
