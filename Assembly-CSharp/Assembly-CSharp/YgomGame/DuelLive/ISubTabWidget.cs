using System;

namespace YgomGame.DuelLive
{
	// Token: 0x02000C61 RID: 3169
	public interface ISubTabWidget
	{
		// Token: 0x1700095E RID: 2398
		// (get) Token: 0x06005A8D RID: 23181
		SubTabGroupWidget parentGroup { get; }

		// Token: 0x1700095F RID: 2399
		// (get) Token: 0x06005A8E RID: 23182
		DuelLiveTabWidget tabWidget { get; }
	}
}
