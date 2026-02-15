using System;

namespace YgomSystem.UI
{
	// Token: 0x0200059D RID: 1437
	public interface ILoadingIconHandler
	{
		// Token: 0x1700025C RID: 604
		// (get) Token: 0x06002D81 RID: 11649
		bool visible { get; }

		// Token: 0x14000025 RID: 37
		// (add) Token: 0x06002D82 RID: 11650
		// (remove) Token: 0x06002D83 RID: 11651
		event Action onReloadEvent;

		// Token: 0x06002D84 RID: 11652
		bool IsDone();
	}
}
