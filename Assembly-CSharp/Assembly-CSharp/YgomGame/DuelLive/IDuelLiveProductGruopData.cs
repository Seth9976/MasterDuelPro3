using System;

namespace YgomGame.DuelLive
{
	// Token: 0x02000C5E RID: 3166
	public interface IDuelLiveProductGruopData
	{
		// Token: 0x17000946 RID: 2374
		// (get) Token: 0x06005A6D RID: 23149
		int groupId { get; }

		// Token: 0x17000947 RID: 2375
		// (get) Token: 0x06005A6E RID: 23150
		string labelTextId { get; }

		// Token: 0x17000948 RID: 2376
		// (get) Token: 0x06005A6F RID: 23151
		string labelText { get; }

		// Token: 0x06005A70 RID: 23152
		bool IsMatchProduct(IProductContext product);
	}
}
