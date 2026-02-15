using System;

namespace YgomGame.Mission
{
	// Token: 0x02000A28 RID: 2600
	public interface IMissionSelectorHistoryHandler
	{
		// Token: 0x170006E6 RID: 1766
		// (get) Token: 0x06004B6A RID: 19306
		bool isSelected { get; }

		// Token: 0x06004B6B RID: 19307
		void SaveSelectorHistory();

		// Token: 0x06004B6C RID: 19308
		bool TrySelectHistory();
	}
}
